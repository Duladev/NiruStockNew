
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_POLModify
    Private Sub ClearFields()
        txtPcs.Text = ""
        txtCts.Text = ""
        txtFilePath.Text = ""
        flxDetails.Rows.Clear()
        cmbCompCode.Text = ""
    End Sub

    Private Sub frm_POLModify_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        Dim dblAvgCost As Double
        Dim dblCurCost As Double
        Dim strOldSize As String
        Dim strNewSize As String

        Dim dblDiaCost As Double
        Dim dblDiaLabCost As Double
        Dim dblLabourCost As Double

        If txtFilePath.Text = "" Then Exit Sub
        If Len(Dir(txtFilePath.Text)) > 0 Then
            xlApp = New Excel.Application
            xlWorkBook = xlApp.Workbooks.Open(txtFilePath.Text)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            flxDetails.Rows.Clear()
            For intRow = 2 To 10000
                If Len(xlWorkSheet.Cells(intRow, 1).Value) <> 0 Then
                    dblAvgCost = 0
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & Trim(xlWorkSheet.Cells(intRow, 2).Value) & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If cmbCompCode.Text = "DCL" Then
                            dblAvgCost = rsComSql.Fields("AvgCost").Value
                            dblCurCost = rsComSql.Fields("CurCost").Value

                            dblDiaCost = rsComSql.Fields("DiaCost").Value
                            dblDiaLabCost = rsComSql.Fields("DiaCostLab").Value
                            dblLabourCost = rsComSql.Fields("LabourCost").Value
                        Else
                            dblAvgCost = rsComSql.Fields("AvgCost2").Value
                            dblCurCost = rsComSql.Fields("CurCost2").Value

                            dblDiaCost = rsComSql.Fields("DiaCost").Value
                            dblDiaLabCost = rsComSql.Fields("DiaCostLab").Value
                            dblLabourCost = rsComSql.Fields("LabourCost").Value
                        End If
                    End If
                    rsComSql = Nothing

                    strOldSize = Trim(xlWorkSheet.Cells(intRow, 6).Value)
                    strNewSize = Trim(xlWorkSheet.Cells(intRow, 7).Value)

                    If Len(strOldSize) = 0 Then
                        strOldSize = "0"
                    End If

                    If Len(strNewSize) = 0 Then
                        strNewSize = "0"
                    End If

                    flxDetails.Rows.Add(Trim(xlWorkSheet.Cells(intRow, 1).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 2).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 3).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 4).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 5).Value),
                                        dblAvgCost,
                                        Math.Round(dblAvgCost * CDbl(Trim(xlWorkSheet.Cells(intRow, 5).Value)), 2),
                                        strOldSize,
                                        strNewSize,
                                        dblCurCost,
                                        dblDiaCost,
                                        dblDiaLabCost,
                                        dblLabourCost)

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

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Load_Excel()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLModify.rpt"
        strReportPath = PBReportPath & strReportPath & mReportName
        objForm.Show()
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean
        Dim dblAvgCost As Double
        Dim dblCurCost As Double
        Dim dblStockValue As Double
        Dim dblCurValue As Double
        Dim dblStockCts As Double
        Dim intOutPcs As Double
        Dim intBalPcs As Double
        Dim blnFound As Boolean

        Dim dblDiaCost As Double
        Dim dblDiaLabCost As Double
        Dim dblLabourCost As Double

        Dim dblDiaValue As Double
        Dim dblDiaLabValue As Double
        Dim dblLabourValue As Double

        Dim strNewAssortment As String
        Dim strOldAssortment As String

        If cmbCompCode.Text = "" Then
            MsgBox("Invalid Company", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        blnSave = False
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse  = MsgBoxResult.Yes Then

            For intRow = 0 To flxDetails.Rows.Count - 1
                strOldAssortment = UCase(flxDetails.Item(1, intRow).Value)
                strNewAssortment = UCase(flxDetails.Item(2, intRow).Value)

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_POLStockBal2New WHERE Assortment = '" & strOldAssortment & "' AND Assortment2 = '" & strOldAssortment & "' AND CompCode = '" & cmbCompCode.Text & "' AND SizeRange = '" & flxDetails.Item(7, intRow).Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If rsComSql.Fields("Pcs").Value < CDbl(flxDetails.Item(3, intRow).Value) Then
                        MsgBox("Invalid Pcs - " & strOldAssortment, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If Math.Round(rsComSql.Fields("Cts").Value, 3) < CDbl(flxDetails.Item(4, intRow).Value) Then
                        MsgBox("Invalid Cts - " & strOldAssortment, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                Else
                    MsgBox("Invalid Old Assortment - " & strOldAssortment & "/" & flxDetails.Item(7, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & strNewAssortment & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    MsgBox("Invalid New Assortment - " & strNewAssortment, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing

                'rsComSql = New ADODB.Recordset
                'rsComSql.Open("SELECT * FROM tblPOLModify WHERE TrfID = '" & flxDetails.Item(0, intRow).Value & "'", AdoCN, 1, 1)
                'If rsComSql.RecordCount Then
                '    MsgBox("Doc ID Already used - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                '    Exit Sub
                'End If
                'rsComSql = Nothing
            Next

            For intRow = 0 To flxDetails.Rows.Count - 1
                blnSave = True

                strOldAssortment = UCase(flxDetails.Item(1, intRow).Value)
                strNewAssortment = UCase(flxDetails.Item(2, intRow).Value)

                If strOldAssortment <> strNewAssortment Then
                    dblAvgCost = 0
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & strNewAssortment & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If cmbCompCode.Text = "DCL" Then
                            dblAvgCost = rsComSql.Fields("AvgCost").Value
                            dblCurCost = rsComSql.Fields("CurCost").Value

                            dblDiaCost = rsComSql.Fields("DiaCost").Value
                            dblDiaLabCost = rsComSql.Fields("DiaCostLab").Value
                            dblLabourCost = rsComSql.Fields("LabourCost").Value

                        ElseIf cmbCompCode.Text = "NLE" Then
                            dblAvgCost = rsComSql.Fields("AvgCost2").Value
                            dblCurCost = rsComSql.Fields("CurCost2").Value

                            dblDiaCost = rsComSql.Fields("DiaCost").Value
                            dblDiaLabCost = rsComSql.Fields("DiaCostLab").Value
                            dblLabourCost = rsComSql.Fields("LabourCost").Value

                        Else
                            dblAvgCost = rsComSql.Fields("AvgCost3").Value
                            dblCurCost = rsComSql.Fields("CurCost3").Value

                            dblDiaCost = rsComSql.Fields("DiaCost").Value
                            dblDiaLabCost = rsComSql.Fields("DiaCostLab").Value
                            dblLabourCost = rsComSql.Fields("LabourCost").Value
                        End If
                    End If
                    rsComSql = Nothing

                    dblStockValue = 0
                    dblStockCts = 0
                    dblCurValue = 0
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT ROUND(SUM(Cts), 3) AS Cts FROM VW_POLStockBal2New WHERE Assortment = '" & strNewAssortment & "' AND CompCode = '" & cmbCompCode.Text & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If Not IsDBNull(rsComSql.Fields("Cts").Value) Then
                            dblStockCts = rsComSql.Fields("Cts").Value
                            dblStockValue = dblStockCts * dblAvgCost
                            dblCurValue = dblStockCts * dblCurCost
                            dblDiaValue = dblStockCts * dblDiaCost
                            dblDiaLabValue = dblStockCts * dblDiaLabCost
                            dblLabourValue = dblStockCts * dblLabourCost
                        End If
                    End If
                    rsComSql = Nothing

                    dblAvgCost = (dblStockValue + (CDbl(flxDetails.Item(4, intRow).Value) * CDbl(flxDetails.Item(5, intRow).Value))) / (dblStockCts + CDbl(flxDetails.Item(4, intRow).Value))
                    dblAvgCost = Math.Round(dblAvgCost, 2)

                    dblCurCost = (dblCurValue + (CDbl(flxDetails.Item(4, intRow).Value) * CDbl(flxDetails.Item(9, intRow).Value))) / (dblStockCts + CDbl(flxDetails.Item(4, intRow).Value))
                    dblCurCost = Math.Round(dblCurCost, 2)

                    dblDiaCost = (dblDiaValue + (CDbl(flxDetails.Item(4, intRow).Value) * CDbl(flxDetails.Item(10, intRow).Value))) / (dblStockCts + CDbl(flxDetails.Item(4, intRow).Value))
                    dblDiaCost = Math.Round(dblDiaCost, 2)

                    dblDiaLabCost = (dblDiaLabValue + (CDbl(flxDetails.Item(4, intRow).Value) * CDbl(flxDetails.Item(11, intRow).Value))) / (dblStockCts + CDbl(flxDetails.Item(4, intRow).Value))
                    dblDiaLabCost = Math.Round(dblDiaLabCost, 2)

                    dblLabourCost = (dblLabourValue + (CDbl(flxDetails.Item(4, intRow).Value) * CDbl(flxDetails.Item(12, intRow).Value))) / (dblStockCts + CDbl(flxDetails.Item(4, intRow).Value))
                    dblLabourCost = Math.Round(dblLabourCost, 2)
                End If

                AdoCN.Execute("INSERT INTO tblPOLModify(TrfID,NewAssort,OldAssort,Pcs,Cts,CompCode,OldSizeRange,NewSizeRange) " & _
                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & strNewAssortment & "','" & strOldAssortment & "'," & _
                                "" & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",'" & cmbCompCode.Text & "','" & flxDetails.Item(7, intRow).Value & "','" & flxDetails.Item(8, intRow).Value & "')")

                AdoCN.Execute("INSERT INTO tblPOLStockIn(SupParNo,Assortment,Assortment2,Pcs,Cts,Price,CompCode,SizeRange,DiaCost,LabourCost) " & _
                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & strNewAssortment & "','" & strNewAssortment & "'," & _
                                "" & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & "," & CDbl(flxDetails.Item(5, intRow).Value) & ",'" & cmbCompCode.Text & "','" & flxDetails.Item(8, intRow).Value & "'," & CDbl(flxDetails.Item(9, intRow).Value) & "," & CDbl(flxDetails.Item(12, intRow).Value) & ")")

                AdoCN.Execute("INSERT INTO tblPOLStockOut(Assortment,Assortment2,Pcs,Cts,Price,DocID,CompCode,SizeRange,Type) " & _
                              "VALUES('" & strOldAssortment & "','" & strOldAssortment & "'," & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & "," & _
                                "" & CDbl(flxDetails.Item(5, intRow).Value) & ",'" & flxDetails.Item(0, intRow).Value & "','" & cmbCompCode.Text & "','" & flxDetails.Item(7, intRow).Value & "','C')")

                If strOldAssortment <> strNewAssortment Then
                    'Origin Entry
                    intOutPcs = 0
                    intBalPcs = CInt(flxDetails.Item(3, intRow).Value)
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM VW_POLStockBalOrigin WHERE Assortment = '" & strOldAssortment & "' AND CompCode = '" & cmbCompCode.Text & "' AND BalPcs > 0 ORDER BY SysDateTime", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        rsComSql.MoveFirst()
                        While Not rsComSql.EOF And intBalPcs > 0
                            If intBalPcs > 0 Then
                                blnFound = False
                                If intBalPcs <= rsComSql.Fields("BalPcs").Value Then
                                    intOutPcs = intBalPcs

                                    intBalPcs = 0
                                    blnFound = True
                                Else
                                    intOutPcs = rsComSql.Fields("BalPcs").Value
                                    intBalPcs = intBalPcs - intOutPcs
                                    blnFound = True
                                End If
                                If blnFound = True Then
                                    AdoCN.Execute("INSERT INTO tblPOLStockOutOrigin(RefNo,Assortment,SupParNo,Origin,Pcs,EntDate,BoxInDate,Type,CompCode) " & _
                                                  "VALUES('Change','" & strOldAssortment & "','" & rsComSql.Fields("SupParNo").Value & "','" & rsComSql.Fields("Origin").Value & "'," & intOutPcs & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(rsComSql.Fields("SysDateTime").Value, "MM/dd/yyyy") & "','C','" & cmbCompCode.Text & "')")

                                    'Insert Stock In Origin
                                    AdoCN.Execute("INSERT INTO tblPOLStockInOrigin(Assortment,Origin,SupParNo,Pcs,EntDate,CompCode) " & _
                                                  "VALUES('" & strNewAssortment & "','" & rsComSql.Fields("Origin").Value & "','" & rsComSql.Fields("SupParNo").Value & "'," & intOutPcs & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & cmbCompCode.Text & "')")
                                End If
                            End If
                            rsComSql.MoveNext()
                        End While
                    Else
                        AdoCN.Execute("INSERT INTO tblPOLStockOutOrigin(RefNo,Assortment,SupParNo,Origin,Pcs,EntDate,BoxInDate,Type,CompCode) " & _
                                      "VALUES('Change','" & strOldAssortment & "','X900003','De Beers'," & intBalPcs & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "MM/dd/yyyy") & "','C','" & cmbCompCode.Text & "')")

                        'Insert Stock In Origin
                        AdoCN.Execute("INSERT INTO tblPOLStockInOrigin(Assortment,Origin,SupParNo,Pcs,EntDate,CompCode) " & _
                                      "VALUES('" & strNewAssortment & "','De Beers','X900003'," & intBalPcs & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & cmbCompCode.Text & "')")
                    End If
                    rsComSql = Nothing
                End If

                If strOldAssortment <> strNewAssortment Then
                    If cmbCompCode.Text = "DCL" Then
                        AdoCN.Execute("UPDATE tblDCLPermanents SET AvgCost = " & dblAvgCost & ",CurCost = " & dblCurCost & ",DiaCost = " & dblDiaCost & ",DiaCostLab = " & dblDiaLabCost & ",LabourCost = " & dblLabourCost & " WHERE ItemName = '" & strNewAssortment & "'")

                    ElseIf cmbCompCode.Text = "NLE" Then
                        AdoCN.Execute("UPDATE tblDCLPermanents SET AvgCost2 = " & dblAvgCost & ",CurCost2 = " & dblCurCost & ",DiaCost = " & dblDiaCost & ",DiaCostLab = " & dblDiaLabCost & ",LabourCost = " & dblLabourCost & " WHERE ItemName = '" & strNewAssortment & "'")

                    Else
                        AdoCN.Execute("UPDATE tblDCLPermanents SET AvgCost3 = " & dblAvgCost & ",CurCost3 = " & dblCurCost & ",DiaCost = " & dblDiaCost & ",DiaCostLab = " & dblDiaLabCost & ",LabourCost = " & dblLabourCost & " WHERE ItemName = '" & strNewAssortment & "'")
                    End If
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
End Class