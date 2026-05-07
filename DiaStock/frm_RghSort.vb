
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_RghSort

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_Color()
        Dim rsGrdType As New ADODB.Recordset

        cmbColor.Items.Clear()
        rsGrdType = New ADODB.Recordset
        rsGrdType.Open("SELECT * FROM tblRghTypes WHERE Sec = 2 ORDER BY Type", AdoCN, 1, 1)
        If rsGrdType.RecordCount Then
            rsGrdType.MoveFirst()
            While Not rsGrdType.EOF
                cmbColor.Items.Add(rsGrdType.Fields("Type").Value)
                rsGrdType.MoveNext()
            End While
        End If
        rsGrdType = Nothing
    End Sub

    Private Sub Load_Flo()
        Dim rsGrdType As New ADODB.Recordset

        cmbFlo.Items.Clear()
        rsGrdType = New ADODB.Recordset
        rsGrdType.Open("SELECT * FROM tblRghTypes WHERE Sec = 3 ORDER BY Type", AdoCN, 1, 1)
        If rsGrdType.RecordCount Then
            rsGrdType.MoveFirst()
            While Not rsGrdType.EOF
                cmbFlo.Items.Add(rsGrdType.Fields("Type").Value)
                rsGrdType.MoveNext()
            End While
        End If
        rsGrdType = Nothing
    End Sub

    Private Sub Load_Clarity()
        Dim rsGrdType As New ADODB.Recordset

        cmbClarity.Items.Clear()
        rsGrdType = New ADODB.Recordset
        rsGrdType.Open("SELECT * FROM tblRghTypes WHERE Sec = 4 ORDER BY Type", AdoCN, 1, 1)
        If rsGrdType.RecordCount Then
            rsGrdType.MoveFirst()
            While Not rsGrdType.EOF
                cmbClarity.Items.Add(rsGrdType.Fields("Type").Value)
                rsGrdType.MoveNext()
            End While
        End If
        rsGrdType = Nothing
    End Sub

    Private Sub Load_Model()
        Dim rsGrdType As New ADODB.Recordset

        cmbModel.Items.Clear()
        rsGrdType = New ADODB.Recordset
        rsGrdType.Open("SELECT * FROM tblRghTypes WHERE Sec = 5 ORDER BY Type", AdoCN, 1, 1)
        If rsGrdType.RecordCount Then
            rsGrdType.MoveFirst()
            While Not rsGrdType.EOF
                cmbModel.Items.Add(rsGrdType.Fields("Type").Value)
                rsGrdType.MoveNext()
            End While
        End If
        rsGrdType = Nothing
    End Sub

    Private Sub frm_RghSort_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Color()
        Load_Flo()
        Load_Clarity()
        Load_Model()

        ClearFields()
    End Sub

    Private Sub ClearFields()
        txtParNo.Text = ""
        txtPktNo.Text = ""
        txtPktCts.Text = ""
        txtPktPcs.Text = ""
        txtPktCts.Text = ""
        txtAPcs.Text = ""
        txtACts.Text = ""
        flxDetails.Rows.Clear()
        txtSubPcs.Text = "0"
        txtSubCts.Text = "0"
        txtFilePath.Text = ""
    End Sub

    Private Sub GetNewPacket()
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT MAX(PktNo) AS MaxPktNo FROM tblRghSort WHERE ParNo = '" & txtParNo.Text & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            If Not IsDBNull(rsComSql_1.Fields("MaxPktNo").Value) Then
                txtPktNo.Text = Format(rsComSql_1.Fields("MaxPktNo").Value + 1, "000")
            Else
                txtPktNo.Text = "001"
            End If
        End If
        rsComSql_1 = Nothing
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtParNo.Text = UCase(txtParNo.Text)
            If ParcelFound(txtParNo.Text) = True Then
                txtParNo.Text = UCase(txtParNo.Text)
                GetNewPacket()
                txtPktPcs.Focus()
            Else
                MsgBox("Invalid Parcel", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
                txtParNo.Focus()
            End If
        End If
    End Sub

    Private Function ParcelFound(ByVal strParceNo As String) As Boolean

        ParcelFound = True
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblImport WHERE DCLParcelNo = '" & strParceNo & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount > 0 Then
            ParcelFound = True
        Else
            ParcelFound = False
        End If
        rsComSql_1 = Nothing

    End Function

    Private Sub txtPktPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtPktCts.Focus()
        End If
    End Sub

    Private Sub txtPktCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtPktCts.Text)
        If Asc(e.KeyChar) = 13 Then
            cmbColor.Focus()
        End If
    End Sub

    Private Sub cmbColor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbColor.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbClarity.Focus()
        End If
    End Sub

    Private Sub cmbClarity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbClarity.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbFlo.Focus()
        End If
    End Sub

    Private Sub cmbFlo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbFlo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbModel.Focus()
        End If
    End Sub

    Private Sub cmbModel_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbModel.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtPcs.Focus()
        End If
    End Sub

    Private Sub txtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtCts.Focus()
        End If
    End Sub

    Private Sub txtCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtCts.Text)
        If Asc(e.KeyChar) = 13 Then
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim intRow As Integer

        If cmbColor.Text = "" Then MsgBox("Invalid Color", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbFlo.Text = "" Then MsgBox("Invalid Flo", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbClarity.Text = "" Then MsgBox("Invalid Clarity", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbModel.Text = "" Then MsgBox("Invalid Model", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If txtPktPcs.Text = "" Then MsgBox("Invalid Pkt Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtPktCts.Text = "" Then MsgBox("Invalid Pkt Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If CDbl(txtPktPcs.Text) <= 0 Then MsgBox("Invalid Pkt Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtPktCts.Text) <= 0 Then MsgBox("Invalid Pkt Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If txtPcs.Text = "" Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtCts.Text = "" Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If CDbl(txtPcs.Text) <= 0 Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtCts.Text) <= 0 Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If CDbl(txtPcs.Text) + CDbl(txtSubPcs.Text) > CDbl(txtPktPcs.Text) Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtCts.Text) + CDbl(txtSubCts.Text) > CDbl(txtPktCts.Text) Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        For intRow = 0 To flxDetails.Rows.Count - 1
            If cmbColor.Text = flxDetails.Item(2, intRow).Value And cmbClarity.Text = flxDetails.Item(3, intRow).Value And _
                cmbFlo.Text = flxDetails.Item(4, intRow).Value And cmbModel.Text = flxDetails.Item(5, intRow).Value Then

                MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        flxDetails.Rows.Add(txtParNo.Text,
                            "Boiling",
                            cmbColor.Text,
                            cmbClarity.Text,
                            cmbFlo.Text,
                            cmbModel.Text,
                            txtPcs.Text,
                            txtCts.Text)

        txtSubPcs.Text = CalTotalPcs(flxDetails)
        txtSubCts.Text = CalTotalCts(flxDetails)

        cmbColor.Text = ""
        cmbFlo.Text = ""
        cmbClarity.Text = ""
        cmbModel.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        cmbColor.Focus()
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(6, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(7, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If flxDetails.Rows.Count > 0 Then
            cmbColor.Text = flxDetails.Item(2, flxDetails.CurrentRow.Index).Value
            cmbClarity.Text = flxDetails.Item(3, flxDetails.CurrentRow.Index).Value
            cmbFlo.Text = flxDetails.Item(4, flxDetails.CurrentRow.Index).Value
            cmbModel.Text = flxDetails.Item(5, flxDetails.CurrentRow.Index).Value
            txtPcs.Text = flxDetails.Item(6, flxDetails.CurrentRow.Index).Value
            txtCts.Text = flxDetails.Item(7, flxDetails.CurrentRow.Index).Value
        End If
    End Sub

    Private Sub flxDetails_DoubleClick(sender As Object, e As EventArgs) Handles flxDetails.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)

            txtSubPcs.Text = CalTotalPcs(flxDetails)
            txtSubCts.Text = CalTotalCts(flxDetails)
        End If
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        If ParcelFound(txtParNo.Text) = True Then
            If Len(txtPktNo.Text) <> 3 Then
                MsgBox("Invalid Packet No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtPktPcs.Text = "" Then
                MsgBox("Please enter the Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtPktCts.Text = "" Then
                MsgBox("Please enter the Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If CDbl(txtPktPcs.Text) <> CDbl(txtSubPcs.Text) Then
                MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If CDbl(txtPktCts.Text) <> CDbl(txtSubCts.Text) Then
                MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblRghSort WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                For intRow = 0 To flxDetails.Rows.Count - 1
                    'Finish Packet
                    AdoCN.Execute("INSERT INTO tblRghSort(ParNo, PktNo, PktPcs, PktCts, PktColor, PktClarity, PktFlo, PktModel, PktIss, PktPrice, Result) " & _
                                  "VALUES('" & UCase(txtParNo.Text) & "','" & txtPktNo.Text & "'," & CDbl(flxDetails.Item(6, intRow).Value) & "," & CDbl(flxDetails.Item(7, intRow).Value) & "," & _
                                    "'" & flxDetails.Item(2, intRow).Value & "','" & flxDetails.Item(3, intRow).Value & "','" & flxDetails.Item(4, intRow).Value & "','" & flxDetails.Item(5, intRow).Value & "'," & _
                                    "'" & Format(Date.Now, "MM/dd/yyyy") & "',0,'" & flxDetails.Item(8, intRow).Value & "')")

                Next
            Else
                MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            MsgBox("Details Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
        End If
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub cmdOpen_Click(sender As Object, e As EventArgs) Handles cmdOpen.Click
        flxDetails.Rows.Clear()
        txtPcs.Text = ""
        txtCts.Text = ""

        OpenFileDialog1.Filter = "All Excel Files|*.xls;*.xlsx"
        OpenFileDialog1.InitialDirectory = "C:\"
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName <> "" Then
            txtFilePath.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Load_Excel()
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim intRow As Integer
        Dim strResult As String

        strResult = ""
        If txtFilePath.Text = "" Then Exit Sub
        If Len(Dir(txtFilePath.Text)) > 0 Then
            xlApp = New Excel.Application
            xlWorkBook = xlApp.Workbooks.Open(txtFilePath.Text)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            flxDetails.Rows.Clear()
            For intRow = 2 To 10000
                If Len(xlWorkSheet.Cells(intRow, 1).value) <> 0 Then
                    strResult = "E"
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT FLOUROCENT, CLARUTY, COLOR, MODEL, RESULT " & _
                                  "FROM tblRghLogic " & _
                                  "WHERE (FLOUROCENT = '" & Trim(xlWorkSheet.Cells(intRow, 4).Value) & "') AND (CLARUTY = '" & Trim(xlWorkSheet.Cells(intRow, 3).Value) & "') AND (COLOR = '" & Trim(xlWorkSheet.Cells(intRow, 1).Value) & "') AND (MODEL = '" & Trim(xlWorkSheet.Cells(intRow, 2).Value) & "')", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        strResult = rsComSql.Fields("RESULT").Value
                    End If
                    rsComSql = Nothing

                    flxDetails.Rows.Add(txtParNo.Text,
                                        "Boiling",
                                        Trim(xlWorkSheet.Cells(intRow, 1).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 3).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 4).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 2).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 5).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 6).Value),
                                        strResult)

                Else
                    Exit For
                End If
            Next
            xlWorkSheet = Nothing
            xlWorkBook.Close()
            xlApp.Quit()

            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)

            txtSubPcs.Text = CalTotalPcs(flxDetails)
            txtSubCts.Text = CalTotalCts(flxDetails)

        End If
    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Load_Excel()
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        If strDBName = "DiaStock" Then
            mReportName = "crptRghSortLogic.rpt"
        Else
            mReportName = "crptRghSortLogicSales.rpt"
        End If
        strReportPath = PBReportPath & "Rgh\" & mReportName
        objForm.Show()
    End Sub
End Class