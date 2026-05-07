
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_GRDBoxFM
    Private Sub ClearFields()
        txtFilePath.Text = ""
        flxDetails.Rows.Clear()
        txtPcs.Text = ""
        txtCts.Text = ""
    End Sub

    Private Sub cmdOpen_Click(sender As Object, e As EventArgs) Handles cmdOpen.Click
        txtFilePath.Text = ""
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
        Dim dblPrice As Double
        Dim strAssortment As String

        If txtFilePath.Text = "" Then Exit Sub
        If Len(Dir(txtFilePath.Text)) > 0 Then
            xlApp = New Excel.Application
            xlWorkBook = xlApp.Workbooks.Open(txtFilePath.Text)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            flxDetails.Rows.Clear()
            For intRow = 2 To 10000
                If Len(xlWorkSheet.Cells(intRow, 1).value) <> 0 Then
                    dblPrice = 0

                    strAssortment = Trim(xlWorkSheet.Cells(intRow, 9).Value)

                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & strAssortment & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        dblPrice = rsComSql.Fields("ListCost").Value
                    Else
                        dblPrice = CDbl(Trim(xlWorkSheet.Cells(intRow, 10).Value))
                    End If
                    rsComSql = Nothing

                    flxDetails.Rows.Add(Trim(xlWorkSheet.Cells(intRow, 2).value),
                                        Trim(xlWorkSheet.Cells(intRow, 3).value),
                                        Trim(xlWorkSheet.Cells(intRow, 4).value),
                                        Trim(xlWorkSheet.Cells(intRow, 5).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 6).Value),
                                        Math.Round(CDbl(Trim(xlWorkSheet.Cells(intRow, 7).Value)), 3),
                                        strAssortment,
                                        Format(dblPrice, "#0.00"),
                                        Trim(xlWorkSheet.Cells(intRow, 11).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 12).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 13).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 15).Value))

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
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(4, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(5, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Load_Excel()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean

        blnSave = False
        For intRow = 0 To flxDetails.Rows.Count - 1
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_Box_Forever WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND Grp = '" & flxDetails.Item(1, intRow).Value & "' AND PktNo = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                MsgBox("Already Saved - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing
        Next

        For intRow = 0 To flxDetails.Rows.Count - 1
            blnSave = True
            AdoCN.Execute("INSERT INTO tblGrading_Box_Forever(ParNo,Grp,PktNo,BoxNo,Pcs,Cts,FM,Assortment,Price,Color,Clarity,OrderNo,OK,PackNo) " & _
                          "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(2, intRow).Value & "'," & _
                            "'" & CInt(flxDetails.Item(3, intRow).Value) & "'," & CInt(flxDetails.Item(4, intRow).Value) & "," & CDbl(flxDetails.Item(5, intRow).Value) & ",1," & _
                            "'" & flxDetails.Item(6, intRow).Value & "'," & CDbl(flxDetails.Item(7, intRow).Value) & ",'" & flxDetails.Item(8, intRow).Value & "'," & _
                            "'" & flxDetails.Item(9, intRow).Value & "','" & flxDetails.Item(10, intRow).Value & "',0," & CDbl(flxDetails.Item(11, intRow).Value) & ")")
        Next
        If blnSave = True Then
            MsgBox("Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub frm_GRDBoxFM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub Load_GradingBox()
        Dim dblPrice As Double
        Dim strAssortment As String
        Dim intBoxNo As Integer

        flxDetails.Rows.Clear()
        intBoxNo = 0
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT ID, ParNo, PktNo, Pcs, Cts, ReturnType6, Price, Color, Clarity, OrderNo, OrigParcelNo, DocID, OK " & _
                      "FROM VW_GradingBoxNew " & _
                      "WHERE (ParNo LIKE '" & txtParNo.Text & "%') " & _
                      "ORDER BY ParNo, PktNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                strAssortment = Trim(rsComSql.Fields("ReturnType6").Value)
                intBoxNo = intBoxNo + 1

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & strAssortment & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    dblPrice = rsComSql_1.Fields("ListCost").Value
                Else
                    dblPrice = CDbl(rsComSql.Fields("Price").Value)
                End If
                rsComSql_1 = Nothing

                flxDetails.Rows.Add(Mid(rsComSql.Fields("ParNo").Value, 1, 6),
                                    strRight(rsComSql.Fields("ParNo").Value, 2),
                                    rsComSql.Fields("PktNo").Value,
                                    intBoxNo,
                                    rsComSql.Fields("Pcs").Value,
                                    Math.Round(rsComSql.Fields("Cts").Value, 3),
                                    strAssortment,
                                    Format(dblPrice, "#0.00"),
                                    rsComSql.Fields("Color").Value,
                                    rsComSql.Fields("Clarity").Value,
                                    rsComSql.Fields("OrderNo").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtParNo.Text = UCase(txtParNo.Text)
            Load_GradingBox()
        End If

    End Sub
End Class