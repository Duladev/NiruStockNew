
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_RndUpload

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdOpen_Click(sender As Object, e As EventArgs) Handles cmdOpen.Click
        flxExcel.Rows.Clear()
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
        Dim dblPrice As Double

        If txtFilePath.Text = "" Then
            MsgBox("Please select the Excel File", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If Len(Dir(txtFilePath.Text)) > 0 Then

            xlApp = New Excel.Application
            xlWorkBook = xlApp.Workbooks.Open(txtFilePath.Text)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            flxExcel.Rows.Clear()

            For intRow = 2 To 10000
                If Len(xlWorkSheet.Cells(intRow, 1).Value) = 0 Then Exit For

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT LotNo FROM tblImport WHERE LotNo = '" & Trim(xlWorkSheet.Cells(intRow, 1).Value) & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    MsgBox("Invalid Lot No. - " & Trim(xlWorkSheet.Cells(intRow, 1).Value), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing

                dblPrice = CDbl(Trim(xlWorkSheet.Cells(intRow, 5).Value))
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT ListCost FROM tblDCLPermanents WHERE ItemName = '" & Trim(xlWorkSheet.Cells(intRow, 2).Value) & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    dblPrice = rsComSql.Fields("ListCost").Value
                End If
                rsComSql = Nothing

                flxExcel.Rows.Add(Trim(xlWorkSheet.Cells(intRow, 1).Value),
                                  Trim(xlWorkSheet.Cells(intRow, 2).Value),
                                  Trim(xlWorkSheet.Cells(intRow, 3).Value),
                                  Trim(xlWorkSheet.Cells(intRow, 4).Value),
                                  dblPrice,
                                  Math.Round(CDbl(Trim(xlWorkSheet.Cells(intRow, 4).Value)) * dblPrice, 2),
                                  Trim(xlWorkSheet.Cells(intRow, 7).Value),
                                  Trim(xlWorkSheet.Cells(intRow, 8).Value))

            Next
            xlWorkSheet = Nothing
            xlWorkBook.Close()
            xlApp.Quit()

            txtPcs.Text = CalTotalPcs(flxExcel)
            txtCts.Text = CalTotalCts(flxExcel)

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

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(2, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(3, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub frm_RndUpload_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        optDcl.Checked = True
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean
        Dim intType As Integer

        If optDcl.Checked = True Then
            intType = 0
        Else
            If optImpex.Checked = True Then
                intType = 1
            Else
                If optCol.Checked = True Then
                    intType = 2
                End If
            End If
        End If

        AdoCN.Execute("DELETE FROM tblGrading_PackingListCOLM WHERE LotNo = '" & flxExcel.Item(0, 0).Value & "' AND Type = " & intType & "")
        For intRow = 0 To flxExcel.Rows.Count - 1
            blnSave = True
            AdoCN.Execute("INSERT INTO tblGrading_PackingListCOLM(Department,LotNo,Assortment,Pcs,Cts,Price,PackNo,Type,Price2,SizeRange) " & _
                          "VALUES('Colombo Niru','" & flxExcel.Item(0, intRow).Value & "','" & flxExcel.Item(1, intRow).Value & "'," & _
                            "" & CDbl(flxExcel.Item(2, intRow).Value) & "," & CDbl(flxExcel.Item(3, intRow).Value) & "," & CDbl(flxExcel.Item(4, intRow).Value) & "," & _
                            "" & CDbl(flxExcel.Item(6, intRow).Value) & "," & intType & "," & CDbl(flxExcel.Item(4, intRow).Value) & ",'" & flxExcel.Item(7, intRow).Value & "')")
        Next
        If blnSave = True Then
            MsgBox("Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            txtFilePath.Text = ""
            flxExcel.Rows.Clear()
            txtPcs.Text = ""
            txtCts.Text = ""
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Load_Excel()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ExportToExcel(flxExcel)
    End Sub

    Private Sub Delete()
        Dim intType As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If MsgBoxResult.Yes Then
            If txtLotNo.Text = "" Then MsgBox("Invalid Lot No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

            If optDcl.Checked = True Then
                intType = 0
            Else
                If optImpex.Checked = True Then
                    intType = 1
                Else
                    If optCol.Checked = True Then
                        intType = 2
                    End If
                End If
            End If

            AdoCN.Execute("DELETE FROM tblGrading_PackingListCOLM WHERE LotNo = '" & txtLotNo.Text & "' AND Type = " & intType & "")

            txtLotNo.Text = ""
        End If

    End Sub

    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        Delete()
    End Sub
End Class