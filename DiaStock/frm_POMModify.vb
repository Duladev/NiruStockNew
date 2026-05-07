
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_POMModify
    Private Sub ClearFields()
        txtPcs.Text = ""
        txtCts.Text = ""
        txtFilePath.Text = ""
        flxDetails.Rows.Clear()
        txtPcs.Text = ""
        txtCts.Text = ""
        txtTotValue.Text = ""
        txtImportNo.Text = ""
        txtImpValue.Text = "0"
        cmbCompCode.Text = ""
        txtTotAdjValue.Text = "0"
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
        Dim dblAvgCost As Double

        If txtFilePath.Text = "" Then Exit Sub
        If Len(Dir(txtFilePath.Text)) > 0 Then
            xlApp = New Excel.Application
            xlWorkBook = xlApp.Workbooks.Open(txtFilePath.Text)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            flxDetails.Rows.Clear()
            For intRow = 2 To 10000
                If Len(xlWorkSheet.Cells(intRow, 1).value) <> 0 Then
                    dblAvgCost = 0
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & Trim(xlWorkSheet.Cells(intRow, 2).Value) & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If cmbCompCode.Text = "DCL" Then
                            dblAvgCost = rsComSql.Fields("AvgCost").Value
                        Else
                            dblAvgCost = rsComSql.Fields("AvgCost2").Value
                        End If
                    End If
                    rsComSql = Nothing

                    flxDetails.Rows.Add(Trim(xlWorkSheet.Cells(intRow, 1).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 2).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 3).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 4).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 5).Value),
                                        dblAvgCost,
                                        Math.Round(dblAvgCost * CDbl(Trim(xlWorkSheet.Cells(intRow, 5).Value)), 2),
                                        Trim(xlWorkSheet.Cells(intRow, 6).Value))

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
            txtTotValue.Text = CalTotalValue(flxDetails)
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
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(3, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(4, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Function CalTotalValue(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalValue = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalValue = CalTotalValue + Val(flxSample.Item(6, intRow).Value)
        Next
        CalTotalValue = Math.Round(CalTotalValue, 2)
    End Function

    Private Function CalTotalAdjValue(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalAdjValue = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalAdjValue = CalTotalAdjValue + Val(flxSample.Item(10, intRow).Value)
        Next
        CalTotalAdjValue = Math.Round(CalTotalAdjValue, 2)
    End Function

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Load_Excel()
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean
        Dim dblAvgCost As Double
        Dim dblStockValue As Double
        Dim dblStockCts As Double

        If cmbCompCode.Text = "" Then
            MsgBox("Invalid Company", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        blnSave = False
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse  = MsgBoxResult.Yes Then

            For intRow = 0 To flxDetails.Rows.Count - 1
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_POMStockBal WHERE Assortment = '" & flxDetails.Item(1, intRow).Value & "' AND CompCode = '" & cmbCompCode.Text & "' AND ImportNo = '" & flxDetails.Item(7, intRow).Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If rsComSql.Fields("BalPcs").Value < CDbl(flxDetails.Item(3, intRow).Value) Then
                        MsgBox("Invalid Pcs - " & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If Math.Round(rsComSql.Fields("BalCts").Value, 3) < CDbl(flxDetails.Item(4, intRow).Value) Then
                        MsgBox("Invalid Cts - " & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                Else
                    MsgBox("Invalid Old Assortment - " & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    MsgBox("Invalid New Assortment - " & flxDetails.Item(2, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblPOMModify WHERE TrfID = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    MsgBox("Doc ID Already used - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing
            Next

            For intRow = 0 To flxDetails.Rows.Count - 1
                blnSave = True

                dblAvgCost = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If cmbCompCode.Text = "DCL" Then
                        dblAvgCost = rsComSql.Fields("AvgCost").Value
                    Else
                        dblAvgCost = rsComSql.Fields("AvgCost2").Value
                    End If
                End If
                rsComSql = Nothing

                dblStockValue = 0
                dblStockCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_POLStockBal2 WHERE Assortment = '" & flxDetails.Item(2, intRow).Value & "' AND CompCode = '" & cmbCompCode.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("Cts").Value) Then
                        dblStockCts = rsComSql.Fields("Cts").Value
                        dblStockValue = rsComSql.Fields("Cts").Value * dblAvgCost
                    End If
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(BalCts) AS Cts, SUM(BalCts * AvgCost) AS Value " & _
                                "FROM VW_POMStockBal " & _
                                "WHERE (Assortment = '" & flxDetails.Item(2, intRow).Value & "') AND CompCode = '" & cmbCompCode.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("Cts").Value) Then
                        dblStockCts = dblStockCts + rsComSql.Fields("Cts").Value
                        dblStockValue = dblStockValue + rsComSql.Fields("Value").Value
                    End If
                End If
                rsComSql = Nothing

                dblAvgCost = (dblStockValue + (CDbl(flxDetails.Item(4, intRow).Value) * CDbl(flxDetails.Item(5, intRow).Value))) / (dblStockCts + CDbl(flxDetails.Item(4, intRow).Value))
                dblAvgCost = Math.Round(dblAvgCost, 2)

                AdoCN.Execute("INSERT INTO tblPOMModify(TrfID, NewAssort, OldAssort, Pcs, Cts, CompCode) " & _
                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & UCase(flxDetails.Item(2, intRow).Value) & "','" & UCase(flxDetails.Item(1, intRow).Value) & "'," & _
                                "" & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",'" & cmbCompCode.Text & "')")

                AdoCN.Execute("INSERT INTO tblPOMStockIn(ImportNo,SupParNo,Assortment,Assortment2,Pcs,Cts,Price,CompCode) " & _
                              "VALUES('" & flxDetails.Item(7, intRow).Value & "','" & flxDetails.Item(0, intRow).Value & "','" & UCase(flxDetails.Item(2, intRow).Value) & "','" & UCase(flxDetails.Item(2, intRow).Value) & "'," & _
                                "" & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & "," & CDbl(flxDetails.Item(5, intRow).Value) & ",'" & cmbCompCode.Text & "')")

                AdoCN.Execute("INSERT INTO tblPOMStockOut(ImportNo,Assortment,Assortment2,Pcs,Cts,Price,DocID,CompCode) " & _
                              "VALUES('" & flxDetails.Item(7, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & CInt(flxDetails.Item(3, intRow).Value) & "," & _
                                "" & CDbl(flxDetails.Item(4, intRow).Value) & "," & CDbl(flxDetails.Item(5, intRow).Value) & ",'" & flxDetails.Item(0, intRow).Value & "','" & cmbCompCode.Text & "')")

                If cmbCompCode.Text = "DCL" Then
                    AdoCN.Execute("UPDATE tblDCLPermanents SET AvgCost = " & dblAvgCost & " " & _
                                  "WHERE ItemName = '" & flxDetails.Item(2, intRow).Value & "'")
                Else
                    AdoCN.Execute("UPDATE tblDCLPermanents SET AvgCost2 = " & dblAvgCost & " " & _
                                  "WHERE ItemName = '" & flxDetails.Item(2, intRow).Value & "'")
                End If
            Next
        End If

        If blnSave = True Then
            MsgBox("Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub frm_POMModify_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub txtImportNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtImportNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtImpValue.Text = "0"
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(ItemCost * INVCts) AS TotVal FROM tblImport WHERE ImportNo = '" & Trim(txtImportNo.Text) & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("TotVal").Value) Then
                    txtImpValue.Text = Math.Round(rsComSql.Fields("TotVal").Value, 2)
                End If
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub cmdAnalyze_Click(sender As Object, e As EventArgs) Handles cmdAnalyze.Click
        Dim intRow As Integer
        Dim blnSave As Boolean
        Dim dblProfit As Double

        blnSave = False
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse  = MsgBoxResult.Yes Then
            dblProfit = CDbl(txtTotValue.Text) - CDbl(txtImpValue.Text)

            For intRow = 0 To flxDetails.Rows.Count - 1
                blnSave = True
                flxDetails.Item(8, intRow).Value = Math.Round((CDbl(flxDetails.Item(4, intRow).Value / CDbl(txtCts.Text)) * 100), 2)
                flxDetails.Item(9, intRow).Value = Math.Round((CDbl(flxDetails.Item(8, intRow).Value) * dblProfit) / 100, 2)
                flxDetails.Item(10, intRow).Value = Math.Round(CDbl(flxDetails.Item(6, intRow).Value) - CDbl(flxDetails.Item(9, intRow).Value), 2)
                flxDetails.Item(5, intRow).Value = Math.Round(CDbl(flxDetails.Item(10, intRow).Value) / CDbl(flxDetails.Item(4, intRow).Value), 2)
            Next

            txtTotAdjValue.Text = CalTotalAdjValue(flxDetails)

            If blnSave = True Then
                MsgBox("Analyzed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        End If
    End Sub
End Class