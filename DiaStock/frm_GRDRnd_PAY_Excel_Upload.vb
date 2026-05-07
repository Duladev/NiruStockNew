Imports System.IO
Imports System.Data.SqlClient
Public Class frm_GRDRnd_PAY_Excel_Upload

    Private strSuppCode As String = ""


    '  FORM LOAD

    Private Sub frmPAY_Excel_Upload_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.StartPosition = FormStartPosition.CenterScreen
            SetupGrid()
            Load_Hotel_Name()
            Load_Suppliers()
            dtpInvDate.Value = Date.Now
            ProgressBar1.Value = 0
        Catch ex As Exception
            MsgBox("Error in Form_Load : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub


    '  SETUP GRID

    Private Sub SetupGrid()
        flxOT.Columns.Clear()
        flxOT.AutoGenerateColumns = False
        flxOT.AllowUserToAddRows = False
        flxOT.AllowUserToDeleteRows = False
        flxOT.ReadOnly = True
        flxOT.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        flxOT.BackgroundColor = System.Drawing.Color.White
        flxOT.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(224, 224, 224)

        Dim headers() As String = {
            "#", "Rgh Price", "Rgh Pcs", "Rgh Cts", "Labour",
            "Pol Price", "Pol Pcs", "Pol Cts", "Parcel No", "Assort No",
            "Size1", "Description", "Parcel ID", "OSPONo", "Sup Par No",
            "Lot ID", "Remarks"
        }
        Dim widths() As Integer = {
            35, 85, 70, 75, 70,
            85, 70, 75, 100, 90,
            60, 100, 90, 80, 90,
            70, 90
        }

        For idx As Integer = 0 To headers.Length - 1
            Dim col As New DataGridViewTextBoxColumn()
            col.HeaderText = headers(idx)
            col.Name = "Col" & idx
            col.Width = widths(idx)
            flxOT.Columns.Add(col)
        Next
    End Sub


    '  LOAD COMPANIES

    Private Sub Load_Hotel_Name()
        Dim rs As New ADODB.Recordset
        Try
            cmbHotel.Items.Clear()
            rs.Open("SELECT WAN_CODE, WAN_NAME FROM tblGrading_RndWAN_LOCA ORDER BY WAN_NAME",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

            Do While Not rs.EOF
                Dim code As String = rs.Fields("WAN_CODE").Value.ToString().Trim()
                Dim name As String = rs.Fields("WAN_NAME").Value.ToString().Trim()

                If Prod_WK_ID = "XX" Then
                    cmbHotel.Items.Add(New CompanyItem(code, name))
                ElseIf Prod_WK_ID = code Then
                    Dim item As New CompanyItem(code, name)
                    cmbHotel.Items.Add(item)
                    cmbHotel.SelectedItem = item
                End If
                rs.MoveNext()
            Loop

            If cmbHotel.Items.Count = 0 Then
                If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
                rs.Open("SELECT WAN_CODE, WAN_NAME FROM tblGrading_RndWAN_LOCA ORDER BY WAN_NAME",
                        AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
                Do While Not rs.EOF
                    cmbHotel.Items.Add(New CompanyItem(
                        rs.Fields("WAN_CODE").Value.ToString().Trim(),
                        rs.Fields("WAN_NAME").Value.ToString().Trim()))
                    rs.MoveNext()
                Loop
            End If

        Catch ex As Exception
            MsgBox("Error No : " & ex.HResult &
                   vbCrLf & "Description : " & ex.Message &
                   vbCrLf & "Function : Load Company Name",
                   MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub


    '  LOAD SUPPLIERS

    Private Sub Load_Suppliers()
        Dim rs As New ADODB.Recordset
        Try
            cmbSupplier.Items.Clear()
            rs.Open(
                "SELECT SupplierCode, SupplierName FROM tblGrading_RndSupplier ORDER BY SupplierName",
                AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

            Do While Not rs.EOF
                cmbSupplier.Items.Add(New SupplierItem(
                    rs.Fields("SupplierCode").Value.ToString().Trim(),
                    rs.Fields("SupplierName").Value.ToString().Trim()))
                rs.MoveNext()
            Loop

        Catch ex As Exception
            MsgBox("Error in Load_Suppliers : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub


    '  COMPANY SELECTION

    Private Sub cmbHotel_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles cmbHotel.SelectedIndexChanged
        If cmbHotel.SelectedItem IsNot Nothing Then
            Dim selected As CompanyItem = CType(cmbHotel.SelectedItem, CompanyItem)
            Prod_WK_ID = selected.Code
        End If
        flxOT.Rows.Clear()
    End Sub


    '  SUPPLIER SELECTION

    Private Sub cmbSupplier_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles cmbSupplier.SelectedIndexChanged
        If cmbSupplier.SelectedItem IsNot Nothing Then
            strSuppCode = CType(cmbSupplier.SelectedItem, SupplierItem).Code
        End If
    End Sub


    '  UPLOAD EXCEL

    Private Sub Upload_Excel()
        Cursor.Current = Cursors.WaitCursor
        Try
            flxOT.Rows.Clear()
            txtError.Text = ""
            txtCount.Text = "0"
            txtInvoice.Text = ""

            Dim xlApp As New Microsoft.Office.Interop.Excel.Application()
            xlApp.Visible = False

            Dim wb As Microsoft.Office.Interop.Excel.Workbook =
                xlApp.Workbooks.Open(txtBackupLocation.Text.Trim())
            Dim ws As Microsoft.Office.Interop.Excel.Worksheet =
                CType(wb.Worksheets(1), Microsoft.Office.Interop.Excel.Worksheet)

            ProgressBar1.Value = 0
            ProgressBar1.Maximum = 100

            txtInvoice.Text = ws.Cells(2, 1).Value?.ToString().Trim()

            Dim dblCount As Double = 0

            For intRow As Integer = 4 To 104
                Dim cellVal As Object = ws.Cells(intRow, 5).Value
                If cellVal Is Nothing OrElse cellVal.ToString().Trim() = "" Then Exit For

                dblCount += 1

                Dim labour As String = "0"
                Dim raw = ws.Cells(intRow, 18).Value?.ToString().Trim()
                If IsNumeric(raw) Then labour = raw

                flxOT.Rows.Add(
                    dblCount.ToString(),
                    Format(Convert.ToDouble(ws.Cells(intRow, 10).Value), "###,##0.00"),
                    ws.Cells(intRow, 8).Value?.ToString().Trim(),
                    Format(Convert.ToDouble(ws.Cells(intRow, 9).Value), "#0.000"),
                    labour,
                    Format(Convert.ToDouble(ws.Cells(intRow, 21).Value), "###,##0.00"),
                    ws.Cells(intRow, 15).Value?.ToString().Trim(),
                    Format(Convert.ToDouble(ws.Cells(intRow, 19).Value), "#0.000"),
                    ws.Cells(intRow, 6).Value?.ToString().Trim(),
                    ws.Cells(intRow, 5).Value?.ToString().Trim(),
                    Format(Convert.ToDouble(ws.Cells(intRow, 16).Value), "#0.00"),
                    ws.Cells(intRow, 35).Value?.ToString().Trim(),
                    ws.Cells(intRow, 36).Value?.ToString().Trim(),
                    ws.Cells(intRow, 37).Value?.ToString().Trim(),
                    ws.Cells(intRow, 1).Value?.ToString().Trim(),
                    ws.Cells(intRow, 7).Value?.ToString().Trim(),
                    ws.Cells(intRow, 2).Value?.ToString().Trim()
                )

                ProgressBar1.Value = CInt(Math.Min(dblCount, 100))
                Application.DoEvents()
            Next

            txtCount.Text = dblCount.ToString()
            ProgressBar1.Value = 0

            wb.Close(False)
            xlApp.Quit()
            System.Runtime.InteropServices.Marshal.ReleaseComObject(ws)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(wb)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp)

        Catch ex As Exception
            txtError.Text = "Error reading Excel: " & ex.Message
            MsgBox("Error in Upload_Excel : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub


    '  SAFE DOUBLE CONVERSION

    Private Function ToDouble(val As Object) As Double
        If val Is Nothing OrElse val.ToString().Trim() = "" Then Return 0
        Dim raw As String = val.ToString().Replace(",", "").Trim()
        If IsNumeric(raw) Then Return Convert.ToDouble(raw)
        Return 0
    End Function


    '  NEW BUTTON

    Private Sub cmdNew_Click_1(sender As Object, e As EventArgs) Handles cmdNew.Click
        txtCount.Text = ""
        txtInvoice.Text = ""
        cmbSupplier.SelectedIndex = -1
        flxOT.Rows.Clear()
        txtBackupLocation.Text = ""
        dtpInvDate.Value = Date.Now
        ProgressBar1.Value = 0
        txtError.Text = ""
    End Sub


    '  SAVE (Upload Button)

    Private Sub cmdSave_Click_1(sender As Object, e As EventArgs) Handles cmdSave.Click
        Try
            ' ── Basic validation
            If cmbHotel.SelectedItem Is Nothing OrElse cmbSupplier.SelectedItem Is Nothing Then
                MessageBox.Show("Please select the Supplier", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If flxOT.Rows.Count = 0 Then
                MessageBox.Show("No data to upload. Please load the Excel file first.", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If flxOT.Rows(0).Cells("Col8").Value?.ToString().Trim() = "" Then
                MessageBox.Show("Invalid Parcel No.", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            Dim confirm = MessageBox.Show(
                "You are going to upload invoice details of " &
                txtInvoice.Text.Trim() & ". Are you sure?",
                Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.No Then Return

            ' ── Duplicate invoice check
            Dim rsDup As New ADODB.Recordset()
            Try
                rsDup.Open(
                    "SELECT COUNT(*) AS CNT FROM tblGrading_RndInvoice " &
                    "WHERE InvoiceNo='" & txtInvoice.Text.Trim() & "'",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic,
                    ADODB.LockTypeEnum.adLockReadOnly)

                If CInt(rsDup.Fields("CNT").Value) > 0 Then
                    rsDup.Close()
                    MessageBox.Show(
                        "Invoice already exists. Please contact the System Administrator to update.",
                        Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If
            Finally
                If rsDup.State = ADODB.ObjectStateEnum.adStateOpen Then rsDup.Close()
                rsDup = Nothing
            End Try

            Dim invNo As String = txtInvoice.Text.Trim().Replace("'", "''")
            Dim invDate As String = dtpInvDate.Value.ToString("MM/dd/yyyy")
            Dim impDate As String = Date.Now.ToString("MM/dd/yyyy")


            Dim cmd As New ADODB.Command()
            cmd.ActiveConnection = AdoCN

            For Each row As DataGridViewRow In flxOT.Rows
                If row.IsNewRow Then Continue For

                Dim parNo As String = row.Cells("Col8").Value?.ToString().Trim().Replace("'", "''")
                Dim assortNo As String = row.Cells("Col9").Value?.ToString().Trim().Replace("'", "''")
                Dim desc As String = row.Cells("Col11").Value?.ToString().Trim().Replace("'", "''")
                Dim parcelID As String = row.Cells("Col12").Value?.ToString().Trim().Replace("'", "''")
                Dim ospoNo As String = row.Cells("Col13").Value?.ToString().Trim().Replace("'", "''")
                Dim supParNo As String = row.Cells("Col14").Value?.ToString().Trim().Replace("'", "''")
                Dim lotID As String = row.Cells("Col15").Value?.ToString().Trim().Replace("'", "''")
                Dim remarks As String = row.Cells("Col16").Value?.ToString().Trim().Replace("'", "''")

                cmd.CommandText =
                    "INSERT INTO tblGrading_RndInvoice(" &
                    "InvoiceNo,InvoiceDate,RghPrice,RghPcs,RhgCts,Labour," &
                    "PolPrice,PolPcs,PolCts,Supplier,ParcelNo,AssortNo,Size1,GrpName," &
                    "ImportDate,TransferDate,Checked,Repair,Export,ActPcs,ActCts," &
                    "Description,ParcelID,OSPONo,SupParNo,LotID,Remarks) " &
                    "VALUES(" &
                    "'" & invNo & "','" & invDate & "'," &
                    ToDouble(row.Cells("Col1").Value) & "," &
                    ToDouble(row.Cells("Col2").Value) & "," &
                    ToDouble(row.Cells("Col3").Value) & "," &
                    ToDouble(row.Cells("Col4").Value) & "," &
                    ToDouble(row.Cells("Col5").Value) & "," &
                    ToDouble(row.Cells("Col6").Value) & "," &
                    ToDouble(row.Cells("Col7").Value) & "," &
                    "'" & strSuppCode & "','" & parNo & "','" & assortNo & "'," &
                    ToDouble(row.Cells("Col10").Value) & ",''," &
                    "'" & impDate & "','" & invDate & "',0,0,0," &
                    ToDouble(row.Cells("Col6").Value) & "," &
                    ToDouble(row.Cells("Col7").Value) & "," &
                    "'" & desc & "','" & parcelID & "','" & ospoNo & "'," &
                    "'" & supParNo & "','" & lotID & "','" & remarks & "')"

                cmd.Execute()

                Log_book("Invoice", "New", invNo,
                         "New Entry Parcel No. - " & parNo &
                         ", Assort No. - " & assortNo)
            Next

            cmd = Nothing

            MessageBox.Show("Excel Upload Successfully Completed", Me.Text,
                            MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MsgBox("Error in cmdSave_Click : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub


    '  LOAD FILE BUTTON

    Private Sub cmdLoad_Click_1(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Try
            If cmbHotel.SelectedItem Is Nothing Then
                MessageBox.Show("Please select a Company", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If txtBackupLocation.Text.Trim() = "" Then
                MessageBox.Show("Please select the Excel file", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If Not File.Exists(txtBackupLocation.Text.Trim()) Then
                MessageBox.Show("Invalid File Path", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            Upload_Excel()
        Catch ex As Exception
            MsgBox("Error in cmdLoad_Click : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub


    '  EXIT BUTTON

    Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub


    '  SELECT FILE BUTTON

    Private Sub cmdSelect_Click_1(sender As Object, e As EventArgs) Handles cmdSelect.Click
        If cmbHotel.SelectedItem Is Nothing Then
            MessageBox.Show("Please select a Company", Me.Text,
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        flxOT.Rows.Clear()
        Using dlg As New OpenFileDialog()
            dlg.InitialDirectory = "C:\"
            dlg.Filter = "Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx|All Files (*.*)|*.*"
            dlg.Title = "Select Excel File"
            If dlg.ShowDialog() = DialogResult.OK Then
                txtBackupLocation.Text = dlg.FileName
            End If
        End Using
    End Sub

End Class


'  HELPER CLASSES

Public Class SupplierItem
    Public Property Code As String
    Public Property Name As String
    Public Sub New(code As String, name As String)
        Me.Code = code
        Me.Name = name
    End Sub
    Public Overrides Function ToString() As String
        Return Name
    End Function
End Class