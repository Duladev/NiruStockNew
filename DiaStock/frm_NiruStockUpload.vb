
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_NiruStockUpload

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ClearFields()
        flxDept.Rows.Clear()
        txtFilePath.Text = ""
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdOpen_Click(sender As Object, e As EventArgs) Handles cmdOpen.Click
        flxDept.Rows.Clear()
        OpenFileDialog1.Filter = "All Excel Files|*.xls;*.xlsx"
        OpenFileDialog1.InitialDirectory = "C:\"
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName <> "" Then
            txtFilePath.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Load_Excel()
        On Error GoTo ErrorHandler

        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim intRow, m_LotNo As Integer

        If txtFilePath.Text = "" Then
            MsgBox("Please select the Excel File", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If Len(Dir(txtFilePath.Text)) > 0 Then
            xlApp = New Excel.Application
            xlWorkBook = xlApp.Workbooks.Open(txtFilePath.Text)
            xlWorkSheet = xlWorkBook.Worksheets(1)

            flxDept.Rows.Clear()
            m_LotNo = 1
            For intRow = 4 To 10000
                If Trim(xlWorkSheet.Cells(intRow, 1).Value) = "" Then Exit For
                If Trim(xlWorkSheet.Cells(intRow, 1).Value) <> "" Then

                    flxDept.Rows.Add(m_LotNo,
                                    Trim(xlWorkSheet.Cells(intRow, 1).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 2).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 3).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 4).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 5).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 6).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 7).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 8).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 9).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 10).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 11).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 12).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 13).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 14).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 15).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 16).Value))

                    m_LotNo = m_LotNo + 1
                End If

            Next
            xlWorkSheet = Nothing
            xlWorkBook.Close()
            xlApp.Quit()

            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)

            MsgBox("Excel File Loading Completed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        End If
        Exit Sub
ErrorHandler:
        MsgBox(Err.Description, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
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

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub Save()
        On Error GoTo ErrorHandler
        Dim I As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then

            If flxDept.RowCount = 0 Then
                MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            AdoCN.Execute("DELETE FROM tblNIRUTotalCts")

            For I = 0 To flxDept.Rows.Count - 1
                mStrSQL = "INSERT INTO tblNIRUTotalCts(IndexNo,LotName,DCLCts,DCLMCts,DCLTCts,HKCts,HKMCts,HKTCts,ILCts,ILMCts,ILTCts,INDCts,INDMCts,INDTCts,NYCts,NYMCts,NYTCts) " & _
                          "VALUES(" & CInt(flxDept.Item(0, I).Value) & ",'" & Trim(flxDept.Item(1, I).Value) & "'," & CDbl(flxDept.Item(2, I).Value) & "," & CDbl(flxDept.Item(3, I).Value) & "," & _
                            "" & CDbl(flxDept.Item(4, I).Value) & "," & CDbl(flxDept.Item(5, I).Value) & "," & CDbl(flxDept.Item(6, I).Value) & "," & CDbl(flxDept.Item(7, I).Value) & "," & _
                            "" & CDbl(flxDept.Item(8, I).Value) & "," & CDbl(flxDept.Item(9, I).Value) & "," & CDbl(flxDept.Item(10, I).Value) & "," & CDbl(flxDept.Item(11, I).Value) & "," & _
                            "" & CDbl(flxDept.Item(12, I).Value) & "," & CDbl(flxDept.Item(13, I).Value) & "," & CDbl(flxDept.Item(14, I).Value) & "," & CDbl(flxDept.Item(15, I).Value) & "," & _
                            "" & CDbl(flxDept.Item(16, I).Value) & ")"

                AdoCN.Execute(mStrSQL)
            Next

            MsgBox("NIRU Stock Uploaded Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

            ClearFields()
        End If

        Exit Sub
ErrorHandler:
        MsgBox(Err.Description, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
    End Sub

    Private Sub cmdReport_Click(sender As Object, e As EventArgs) Handles cmdReport.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptNIRUTotalStk.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub frm_NiruStockUpload_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class