
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_DCLAssortUpload

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
        Dim dblPrice As Double

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

                dblPrice = -1
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT PRICE FROM tblGrading_SizingList WHERE NAME = '" & Trim(xlWorkSheet.Cells(intRow, 1).Value) & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    dblPrice = rsComSql.Fields("PRICE").Value
                End If
                rsComSql = Nothing

                If dblPrice = -1 Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT ListCost FROM tblDCLPermanents WHERE ItemName = '" & Trim(xlWorkSheet.Cells(intRow, 1).Value) & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        dblPrice = rsComSql.Fields("ListCost").Value
                    End If
                    rsComSql = Nothing
                End If

                If dblPrice = -1 Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT Price FROM tblGrading_RndSizeList WHERE AssortNo = '" & Trim(xlWorkSheet.Cells(intRow, 1).Value) & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        dblPrice = rsComSql.Fields("Price").Value
                    End If
                    rsComSql = Nothing
                End If

                flxDetails.Rows.Add(Trim(xlWorkSheet.Cells(intRow, 1).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 2).Value),
                                    dblPrice)

                If CDbl(Trim(xlWorkSheet.Cells(intRow, 2).Value)) <> dblPrice Then
                    flxDetails.Rows(flxDetails.Rows.Count - 1).Cells(0).Style.BackColor = Color.Yellow
                Else
                    'flxDetails.Rows(flxDetails.Rows.Count - 1).Cells(0).Style.BackColor = Color.White
                End If
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
        Dim strDataBase As String

        If dbConnNiru.State = 1 Then
            dbConnNiru.Close()
        End If
        dbConnNiru.ConnectionString = "Provider=SQLOLEDB;Data Source='" & strServerName & "';Connect Timeout=60;Initial Catalog=NiruStock;Integrated Security=SSPI"
        dbConnNiru.Open()

        If dbConnOther.State = 1 Then
            dbConnOther.Close()
        End If
        If strDBName = "DiaStock" Then
            strDataBase = "DiaSales"
        Else
            strDataBase = "DiaStock"
        End If
        dbConnOther.ConnectionString = "Provider=SQLOLEDB;Data Source='" & strServerName & "';Connect Timeout=60;Initial Catalog=" & strDataBase & ";Integrated Security=SSPI"
        dbConnOther.Open()

        For intRow = 0 To flxDetails.Rows.Count - 1
            blnSave = True
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & flxDetails.Item(0, intRow).Value & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                AdoCN.Execute("UPDATE tblDCLPermanents SET ListCost = " & CDbl(flxDetails.Item(1, intRow).Value) & ",DocDate = '" & Format(Date.Now, "MM/dd/yyyy") & "' WHERE ItemName = '" & flxDetails.Item(0, intRow).Value & "'")
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & flxDetails.Item(0, intRow).Value & "'", dbConnOther, 1, 1)
            If rsComSql.RecordCount Then
                dbConnOther.Execute("UPDATE tblDCLPermanents SET ListCost = " & CDbl(flxDetails.Item(1, intRow).Value) & ",DocDate = '" & Format(Date.Now, "MM/dd/yyyy") & "' WHERE ItemName = '" & flxDetails.Item(0, intRow).Value & "'")
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_SizingList WHERE NAME = '" & flxDetails.Item(0, intRow).Value & "'", dbConnOther, 1, 1)
            If rsComSql.RecordCount Then
                dbConnOther.Execute("UPDATE tblGrading_SizingList SET PRICE = " & CDbl(flxDetails.Item(1, intRow).Value) & " WHERE NAME = '" & flxDetails.Item(0, intRow).Value & "'")
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_SizingList WHERE NAME = '" & flxDetails.Item(0, intRow).Value & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                AdoCN.Execute("UPDATE tblGrading_SizingList SET PRICE = " & CDbl(flxDetails.Item(1, intRow).Value) & " WHERE NAME = '" & flxDetails.Item(0, intRow).Value & "'")
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & flxDetails.Item(0, intRow).Value & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                AdoCN.Execute("UPDATE tblAssortList SET MarketPrice = " & CDbl(flxDetails.Item(1, intRow).Value) & " WHERE Assortment = '" & flxDetails.Item(0, intRow).Value & "'")
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & flxDetails.Item(0, intRow).Value & "'", dbConnOther, 1, 1)
            If rsComSql.RecordCount Then
                dbConnOther.Execute("UPDATE tblAssortList SET MarketPrice = " & CDbl(flxDetails.Item(1, intRow).Value) & " WHERE Assortment = '" & flxDetails.Item(0, intRow).Value & "'")
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_RndSizeList WHERE AssortNo = '" & flxDetails.Item(0, intRow).Value & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                AdoCN.Execute("UPDATE tblGrading_RndSizeList SET Price = " & CDbl(flxDetails.Item(1, intRow).Value) & " WHERE AssortNo = '" & flxDetails.Item(0, intRow).Value & "'")
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_RndSizeList WHERE AssortNo = '" & flxDetails.Item(0, intRow).Value & "'", dbConnOther, 1, 1)
            If rsComSql.RecordCount Then
                dbConnOther.Execute("UPDATE tblGrading_RndSizeList SET Price = " & CDbl(flxDetails.Item(1, intRow).Value) & " WHERE AssortNo = '" & flxDetails.Item(0, intRow).Value & "'")
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_SizeListNew WHERE AssortNo = '" & flxDetails.Item(0, intRow).Value & "'", dbConnNiru, 1, 1)
            If rsComSql.RecordCount Then
                dbConnNiru.Execute("UPDATE tblGrading_SizeListNew SET PRICE = " & CDbl(flxDetails.Item(1, intRow).Value) & " WHERE AssortNo = '" & flxDetails.Item(0, intRow).Value & "'")
            End If
            rsComSql = Nothing
        Next
        If blnSave = True Then
            MsgBox("Assortment Prices Updated Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            txtFilePath.Text = ""
            flxDetails.Rows.Clear()
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

    Private Sub frm_DCLAssortUpload_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub
End Class