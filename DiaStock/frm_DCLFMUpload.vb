
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_DCLFMUpload

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
                If Len(xlWorkSheet.Cells(intRow, 1).Value) = 0 Then Exit For

                flxDetails.Rows.Add(xlWorkSheet.Cells(intRow, 1).Value,
                                    xlWorkSheet.Cells(intRow, 2).Value,
                                    xlWorkSheet.Cells(intRow, 3).Value,
                                    xlWorkSheet.Cells(intRow, 4).Value,
                                    xlWorkSheet.Cells(intRow, 5).Value,
                                    xlWorkSheet.Cells(intRow, 6).Value,
                                    Format(xlWorkSheet.Cells(intRow, 7).Value, "#0.000"),
                                    xlWorkSheet.Cells(intRow, 8).Value,
                                    xlWorkSheet.Cells(intRow, 9).Value,
                                    xlWorkSheet.Cells(intRow, 10).Value,
                                    xlWorkSheet.Cells(intRow, 11).Value,
                                    xlWorkSheet.Cells(intRow, 12).Value,
                                    xlWorkSheet.Cells(intRow, 13).Value)
            Next
            xlWorkSheet = Nothing
            xlWorkBook.Close()
            xlApp.Quit()

            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)

            MsgBox("Stone List Loaded", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
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

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean

        blnSave = False
        For intRow = 0 To flxDetails.Rows.Count - 1
            AdoCN.Execute("DELETE FROM tblGrading_Box_Forever WHERE ParNo = '" & flxDetails.Item(1, intRow).Value & "'")
        Next
        For intRow = 0 To flxDetails.Rows.Count - 1
            blnSave = True
            AdoCN.Execute("INSERT INTO tblGrading_Box_Forever(ParNo,Grp,PktNo,BoxNo,Pcs,Cts,FM,Assortment,Price,Color,Clarity,OrderNo,OK) " & _
                          "VALUES('" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(2, intRow).Value & "','" & flxDetails.Item(3, intRow).Value & "'," & _
                            "'" & flxDetails.Item(4, intRow).Value & "','" & flxDetails.Item(5, intRow).Value & "','" & flxDetails.Item(6, intRow).Value & "',1," & _
                            "'" & flxDetails.Item(8, intRow).Value & "','" & flxDetails.Item(9, intRow).Value & "','" & flxDetails.Item(10, intRow).Value & "'," & _
                            "'" & flxDetails.Item(11, intRow).Value & "','" & flxDetails.Item(12, intRow).Value & "',0)")
        Next
        If blnSave = True Then
            MsgBox("Updated Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            txtFilePath.Text = ""
            flxDetails.Rows.Clear()
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub frm_DCLFMUpload_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub
End Class