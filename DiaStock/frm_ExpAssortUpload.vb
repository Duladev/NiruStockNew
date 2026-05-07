
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_ExpAssortUpload
    Dim strFolderPath As String

    Private Sub cmdOpen_Click(sender As Object, e As EventArgs) Handles cmdOpen.Click
        flxDetails.Rows.Clear()
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
        Dim intRow As Integer

        If txtFilePath.Text = "" Then
            MsgBox("Please select the Excel File", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If Len(Dir(txtFilePath.Text)) > 0 Then

            xlApp = New Excel.Application
            xlWorkBook = xlApp.Workbooks.Open(txtFilePath.Text)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            flxDetails.Rows.Clear()

            For intRow = 2 To 10000
                If xlWorkSheet.Cells(intRow, 1).Value = "" Then Exit For

                flxDetails.Rows.Add(Trim(xlWorkSheet.Cells(intRow, 1).Value))
            Next
            xlWorkSheet = Nothing
            xlWorkBook.Close()
            xlApp.Quit()

            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)

            MsgBox("Assortment List Loaded", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
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

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean

        AdoCN.Execute("DELETE FROM tblDCLAssort")
        For intRow = 0 To flxDetails.Rows.Count - 1
            blnSave = True
            AdoCN.Execute("INSERT INTO tblDCLAssort(Assortment) VALUES('" & flxDetails.Item(0, intRow).Value & "')")
        Next
        If blnSave = True Then
            txtFilePath.Text = ""
            flxDetails.Rows.Clear()

            objForm = New frm_DCLReportViewer
            mReportName = "crptExpSizingFinishStockAssortNew.rpt"
            strReportPath = PBReportPath & strFolderPath & mReportName
            objForm.Show()
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub frm_ExpAssortUpload_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        If strDBName = "DiaStock" Then
            strFolderPath = "Export\"
        Else
            strFolderPath = "DiaSalesExport\"
        End If
    End Sub
End Class