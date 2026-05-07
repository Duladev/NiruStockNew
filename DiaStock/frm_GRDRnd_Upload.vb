Imports System.Data.SqlClient
Imports System.IO

Public Class frm_GRDRnd_Upload


    '  FORM LOAD
    Private Sub frm_Grading_Upload_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.StartPosition = FormStartPosition.CenterScreen
            SetupGrid()
        Catch ex As Exception
            MsgBox("Error in Form_Load : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    '  SETUP GRID
    Private Sub SetupGrid()
        flxDetails.Columns.Clear()
        flxDetails.AutoGenerateColumns = False
        flxDetails.AllowUserToAddRows = False
        flxDetails.AllowUserToDeleteRows = False
        flxDetails.ReadOnly = True
        flxDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        flxDetails.BackgroundColor = System.Drawing.Color.White
        flxDetails.Font = New System.Drawing.Font("Tahoma", 8.25)
        flxDetails.ColumnHeadersDefaultCellStyle.BackColor =
            System.Drawing.Color.FromArgb(70, 130, 180)
        flxDetails.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White
        flxDetails.ColumnHeadersDefaultCellStyle.Font =
            New System.Drawing.Font("Fixedsys", 9)
        flxDetails.EnableHeadersVisualStyles = False
        flxDetails.RowTemplate.Height = 20

        Dim headers() As String = {"Parcel No", "Group", "Packet No", "Box No", "Pcs", "Cts", "FM"}
        Dim names() As String = {"ParNo", "Grp", "PktNo", "BoxNo", "Pcs", "Cts", "FM"}
        Dim widths() As Integer = {100, 100, 100, 100, 80, 90, 50}

        For idx As Integer = 0 To headers.Length - 1
            Dim col As New DataGridViewTextBoxColumn()
            col.HeaderText = headers(idx)
            col.Name = names(idx)
            col.Width = widths(idx)
            If idx >= 4 Then
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
            flxDetails.Columns.Add(col)
        Next
    End Sub

    '  SELECT FILE
    Private Sub cmdSelect_Click(sender As Object, e As EventArgs) Handles cmdSelect.Click
        flxDetails.Rows.Clear()
        Using dlg As New OpenFileDialog()
            dlg.InitialDirectory = "C:\"
            dlg.Filter = "Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx|All Files|*.*"
            dlg.Title = "Select Excel File"
            If dlg.ShowDialog() = DialogResult.OK Then
                txtBackupLocation.Text = dlg.FileName
            End If
        End Using
    End Sub


    '  LOAD FILE
    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        If txtBackupLocation.Text.Trim() = "" Then
            MessageBox.Show("Please select the Excel file", Me.Text,
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        If Not File.Exists(txtBackupLocation.Text.Trim()) Then
            MessageBox.Show("Invalid File Path", Me.Text,
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Upload_Excel()
    End Sub

    '  READ EXCEL AND GRID
    Private Sub Upload_Excel()
        Cursor.Current = Cursors.WaitCursor
        Try
            flxDetails.Rows.Clear()
            txtPcs.Text = "0"
            txtCts.Text = "0"

            Dim xlApp As New Microsoft.Office.Interop.Excel.Application()
            xlApp.Visible = False

            Dim wb As Microsoft.Office.Interop.Excel.Workbook =
                xlApp.Workbooks.Open(txtBackupLocation.Text.Trim())
            Dim ws As Microsoft.Office.Interop.Excel.Worksheet =
                CType(wb.Worksheets(1), Microsoft.Office.Interop.Excel.Worksheet)

            Dim dblPcs As Double = 0
            Dim dblCts As Double = 0

            For intRow As Integer = 2 To 1000
                Dim cellVal As Object = ws.Cells(intRow, 1).Value
                If cellVal Is Nothing OrElse cellVal.ToString().Trim() = "" Then Exit For

                Dim pktVal As Object = ws.Cells(intRow, 8).Value
                If pktVal IsNot Nothing AndAlso pktVal.ToString().Trim() <> "" Then

                    Dim parNo As String = ws.Cells(intRow, 1).Value?.ToString().Trim()
                    Dim boxNo As String = ws.Cells(intRow, 2).Value?.ToString().Trim()
                    Dim pcs As Double = Convert.ToDouble(If(ws.Cells(intRow, 3).Value IsNot Nothing, ws.Cells(intRow, 3).Value, 0))
                    Dim cts As Double = Convert.ToDouble(If(ws.Cells(intRow, 4).Value IsNot Nothing, ws.Cells(intRow, 4).Value, 0))
                    Dim pktNo As String = Format(CInt(ws.Cells(intRow, 8).Value), "000")
                    Dim grp As String = ws.Cells(intRow, 9).Value?.ToString().Trim()
                    Dim fm As String = ws.Cells(intRow, 14).Value?.ToString().Trim()

                    flxDetails.Rows.Add(parNo, grp, pktNo, boxNo,
                                        pcs.ToString(), Format(cts, "#0.000"), fm)

                    dblPcs += pcs
                    dblCts += cts
                End If

                Application.DoEvents()
            Next

            txtPcs.Text = dblPcs.ToString()
            txtCts.Text = Format(Math.Round(dblCts, 3), "#0.000")

            wb.Close(False)
            xlApp.Quit()
            System.Runtime.InteropServices.Marshal.ReleaseComObject(ws)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(wb)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp)

        Catch ex As Exception
            MsgBox("Error in Upload_Excel : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    '  SAVE 
    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Try
            If flxDetails.Rows.Count = 0 Then
                MessageBox.Show("No data to save. Please load the Excel file first.",
                                Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            If MessageBox.Show("Are you sure?", Me.Text,
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return

            Dim saved As Integer = 0
            Dim rsComSql As New ADODB.Recordset()

            For Each row As DataGridViewRow In flxDetails.Rows
                Dim parNo As String = row.Cells("ParNo").Value?.ToString().Trim().ToUpper()
                Dim grp As String = row.Cells("Grp").Value?.ToString().Trim()
                Dim pktNo As String = row.Cells("PktNo").Value?.ToString().Trim()
                Dim boxNo As String = row.Cells("BoxNo").Value?.ToString().Trim()
                Dim pcs As Double = Convert.ToDouble(row.Cells("Pcs").Value?.ToString())
                Dim cts As Double = Convert.ToDouble(row.Cells("Cts").Value?.ToString())
                Dim fm As String = row.Cells("FM").Value?.ToString().Trim()

                ' Duplicate check — skip if already exists
                rsComSql.Open(
                    "SELECT COUNT(*) AS CNT FROM tblGrading_RndBox " &
                    "WHERE ParNo='" & parNo & "' AND Grp='" & grp & "' AND PktNo='" & pktNo & "'",
                    AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                    ADODB.LockTypeEnum.adLockOptimistic)

                Dim alreadyExists As Boolean = CInt(rsComSql.Fields("CNT").Value) > 0
                rsComSql.Close()

                If alreadyExists Then Continue For

                ' INSERT
                rsComSql.Open(
                    "INSERT INTO tblGrading_RndBox(ParNo, Grp, PktNo, BoxNo, Pcs, Cts, FM, FM2) " &
                    "VALUES('" & parNo & "','" & grp & "','" & pktNo & "','" & boxNo & "'," &
                    pcs & "," & cts & ",'" & fm & "',0)",
                    AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                    ADODB.LockTypeEnum.adLockOptimistic)
                rsComSql.Close()

                saved += 1
            Next

            rsComSql = Nothing

            MessageBox.Show("Parcel Saved. " & saved & " row(s) inserted.", Me.Text,
                            MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MsgBox("Error in cmdSave_Click : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    '  EXIT
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

End Class