
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_POMTransfer
    Private Sub ClearFields()
        txtPcs.Text = ""
        txtCts.Text = ""
        txtFilePath.Text = ""
        flxDetails.Rows.Clear()
        txtTrfNo.Text = GetNewTrfNo
        cmbCompCode.Text = ""
    End Sub

    Private Function GetNewTrfNo()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(TransferNo) AS MaxNo FROM tblPOMTransfer", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("MaxNo").Value) Then
                GetNewTrfNo = rsComSql.Fields("MaxNo").Value + 1
            Else
                GetNewTrfNo = 1
            End If
        Else
            GetNewTrfNo = 1
        End If
        rsComSql = Nothing
        Return GetNewTrfNo
    End Function

    Private Sub frm_POMTransfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        cmbCompCode.Items.Add("DCL")
        cmbCompCode.Items.Add("NLE")

        If strDBName = "DiaStock" Then
            strReportPath = "PolishBox\"
        Else
            strReportPath = "DiaSalesPolishBox\"
        End If

        ClearFields()
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

        If txtFilePath.Text = "" Then Exit Sub
        If Len(Dir(txtFilePath.Text)) > 0 Then
            xlApp = New Excel.Application
            xlWorkBook = xlApp.Workbooks.Open(txtFilePath.Text)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            flxDetails.Rows.Clear()
            For intRow = 2 To 10000
                If Len(xlWorkSheet.Cells(intRow, 1).value) <> 0 Then
                    flxDetails.Rows.Add(Trim(xlWorkSheet.Cells(intRow, 1).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 2).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 3).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 4).Value))

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

            txtPcs.Text = CalTotalPcs(flxDetails)
            txtCts.Text = CalTotalCts(flxDetails)

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

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(1, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(2, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean

        If cmbCompCode.Text = "" Then
            MsgBox("Invalid Company", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        PBResponse = MsgBox("Are you sure to Transfer to the Main Polish Box?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse  = MsgBoxResult.Yes Then

            For intRow = 0 To flxDetails.Rows.Count - 1
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_POMStockBal WHERE Assortment = '" & flxDetails.Item(0, intRow).Value & "' AND ImportNo = '" & flxDetails.Item(3, intRow).Value & "' AND CompCode = '" & cmbCompCode.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If rsComSql.Fields("BalPcs").Value < CDbl(flxDetails.Item(1, intRow).Value) Then
                        MsgBox("Invalid Pcs - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If Math.Round(rsComSql.Fields("BalCts").Value, 3) < CDbl(flxDetails.Item(2, intRow).Value) Then
                        MsgBox("Invalid Cts - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                Else
                    MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing
            Next

            For intRow = 0 To flxDetails.Rows.Count - 1
                blnSave = True
                AdoCN.Execute("INSERT INTO tblPOMTransfer(TransferNo,ImportNo,Assortment,Assortment2,Pcs,Cts,Price,DocID,CompCode) " & _
                              "VALUES(" & Val(txtTrfNo.Text) & ",'" & flxDetails.Item(3, intRow).Value & "','" & UCase(flxDetails.Item(0, intRow).Value) & "','" & UCase(flxDetails.Item(0, intRow).Value) & "'," & CInt(flxDetails.Item(1, intRow).Value) & "," & _
                                "" & CDbl(flxDetails.Item(2, intRow).Value) & ",0,'','" & cmbCompCode.Text & "')")

                AdoCN.Execute("INSERT INTO tblPOMStockOut(ImportNo,Assortment,Assortment2,Pcs,Cts,Price,DocID,CompCode) " & _
                              "VALUES('" & flxDetails.Item(3, intRow).Value & "','" & UCase(flxDetails.Item(0, intRow).Value) & "','" & UCase(flxDetails.Item(0, intRow).Value) & "'," & CInt(flxDetails.Item(1, intRow).Value) & "," & _
                                "" & CDbl(flxDetails.Item(2, intRow).Value) & ",0,'','" & cmbCompCode.Text & "')")

                AdoCN.Execute("INSERT INTO tblPOLStockIn(SupParNo,Assortment,Assortment2,Pcs,Cts,Price,CompCode) " & _
                              "VALUES('" & flxDetails.Item(3, intRow).Value & "','" & UCase(flxDetails.Item(0, intRow).Value) & "','" & UCase(flxDetails.Item(0, intRow).Value) & "'," & _
                                "" & CInt(flxDetails.Item(1, intRow).Value) & "," & CDbl(flxDetails.Item(2, intRow).Value) & ",0,'" & cmbCompCode.Text & "')")
            Next

            If blnSave = True Then
                MsgBox("Transfered to Main Polish Box", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
            End If
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Load_Excel()
    End Sub
End Class