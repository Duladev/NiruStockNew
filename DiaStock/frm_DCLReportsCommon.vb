Public Class frm_DCLReportsCommon

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLParcelAll.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        If strDBName = "DiaStock" Then
            mReportName = "DCLLostReport.rpt"
        ElseIf strDBName = "DiaSales" Then
            mReportName = "DCLLostReportSales.rpt"
        Else
            mReportName = "DCLLostReportShare.rpt"
        End If
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRghExportDetails.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button4_Click(sender As Object, e As EventArgs) Handles HazelDev_Button4.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRghExportDetailsLot.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button5_Click(sender As Object, e As EventArgs) Handles HazelDev_Button5.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLExportPlan.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button6_Click(sender As Object, e As EventArgs) Handles HazelDev_Button6.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLExportPlanDept.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button7_Click(sender As Object, e As EventArgs) Handles HazelDev_Button7.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLLotShipment.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button8_Click(sender As Object, e As EventArgs) Handles HazelDev_Button8.Click
        objForm = New frm_DCLReportViewer
        mReportName = "CrptRndGradingFinishValue.rpt"
        strReportPath = PBReportPath & "RoundsFullFlow\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button9_Click(sender As Object, e As EventArgs) Handles HazelDev_Button9.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptLamPlanning2.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button10_Click(sender As Object, e As EventArgs) Handles HazelDev_Button10.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptLamPlanning2Lot.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button11_Click(sender As Object, e As EventArgs) Handles HazelDev_Button11.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptLAMProdIssue.rpt"
        strReportPath = PBReportPath & "Ext\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button12_Click(sender As Object, e As EventArgs) Handles HazelDev_Button12.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRprPcuSizePlan.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button13_Click(sender As Object, e As EventArgs) Handles HazelDev_Button13.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDVPlanningLotShip.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button14_Click(sender As Object, e As EventArgs) Handles HazelDev_Button14.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDVPlanningLot.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button15_Click(sender As Object, e As EventArgs) Handles HazelDev_Button15.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRndPlanningLotShip.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button16_Click(sender As Object, e As EventArgs) Handles HazelDev_Button16.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLPolishProfit_NoCosting.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button17_Click(sender As Object, e As EventArgs) Handles HazelDev_Button17.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRndChart.rpt"
        strReportPath = PBReportPath & "RoundsFullFlow\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button18_Click(sender As Object, e As EventArgs) Handles HazelDev_Button18.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRprBrutingStock.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button19_Click(sender As Object, e As EventArgs) Handles HazelDev_Button19.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLStoneDaysEM.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button20_Click(sender As Object, e As EventArgs) Handles HazelDev_Button20.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLParcelAllSum.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button21_Click(sender As Object, e As EventArgs) Handles HazelDev_Button21.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLExports.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button22_Click(sender As Object, e As EventArgs) Handles HazelDev_Button22.Click
        objForm = New frm_DCLReportViewer
        If strDBName = "DiaStock" Then
            mReportName = "crptDCLImportInst.rpt"
        Else
            mReportName = "crptDCLImportInstSales.rpt"
        End If
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button23_Click(sender As Object, e As EventArgs) Handles HazelDev_Button23.Click
        objForm = New frm_DCLReportViewer
        If strDBName = "DiaStock" Then
            mReportName = "crptImportExport.rpt"
        Else
            mReportName = "crptImportExportSales.rpt"
        End If
        strReportPath = PBReportPath & "Precision\" & mReportName
        objForm.Show()
    End Sub

    Private Sub frm_DCLReportsCommon_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        If PBUser_EmpNo = "D06975" Then
            cmdTest.Visible = True
        Else
            cmdTest.Visible = False
        End If
    End Sub

    Private Sub cmdTest_Click(sender As Object, e As EventArgs) Handles cmdTest.Click
        'Dim intNumber As Integer
        'Dim random As New Random()
        'Insert_OrderPcs()

        'Insert_RprIssRet()

        'rsComSql = New ADODB.Recordset
        'rsComSql.Open("SELECT * FROM AssortListNew2 ORDER BY Assortment", AdoCN, 1, 1)
        'If rsComSql.RecordCount Then
        '    rsComSql.MoveFirst()
        '    While Not rsComSql.EOF
        '        AdoCN.Execute("UPDATE tblAssortList SET AvgCost = '" & rsComSql.Fields("Price").Value & "' WHERE Assortment = '" & rsComSql.Fields("Assortment").Value & "'")
        '        rsComSql.MoveNext()
        '    End While
        'End If

        'intNumber = random.Next(1, 100)
        'MessageBox.Show(intNumber)

        'rsComSql = New ADODB.Recordset
        'rsComSql.Open("select * from Comp81 order by Assortment, SizeRange", AdoCN, 1, 1)
        'If rsComSql.RecordCount Then
        '    rsComSql.MoveFirst()
        '    While Not rsComSql.EOF
        '        AdoCN.Execute("UPDATE tblPOLSales SET Pcs = '" & rsComSql.Fields("Pcs").Value & "',Cts = '" & rsComSql.Fields("Cts").Value & "' WHERE SalesNo = '" & rsComSql.Fields("SalesNo").Value & "' AND Assortment = '" & rsComSql.Fields("Assortment").Value & "' AND SizeRange = '" & rsComSql.Fields("SizeRange").Value & "'")
        '        AdoCN.Execute("UPDATE tblPOLStockOut SET Pcs = '" & rsComSql.Fields("Pcs").Value & "',Cts = '" & rsComSql.Fields("Cts").Value & "' WHERE DocID = '" & rsComSql.Fields("SalesNo").Value & "' AND Assortment = '" & rsComSql.Fields("Assortment").Value & "' AND SizeRange = '" & rsComSql.Fields("SizeRange").Value & "' AND Type = 'S'")

        '        rsComSql.MoveNext()
        '    End While
        'End If
        'rsComSql = Nothing
        'Insert_StockLines()

        'rsComSql = New ADODB.Recordset
        'rsComSql.Open("SELECT * FROM Comp10 ORDER BY ParNo", AdoCN, 1, 1)
        'If rsComSql.RecordCount Then
        '    rsComSql.MoveFirst()
        '    While Not rsComSql.EOF
        '        AdoCN.Execute("DELETE FROM tblImport WHERE SupParcelNo = '" & rsComSql.Fields("ParNo").Value & "'")
        '        AdoCN.Execute("DELETE FROM tblDep_Trf WHERE SupParcelNo = '" & rsComSql.Fields("ParNo").Value & "'")
        '        AdoCN.Execute("DELETE FROM tblExpPacket WHERE ParNo = '" & rsComSql.Fields("ParNo").Value & "'")
        '        AdoCN.Execute("DELETE FROM tblExpIssues WHERE ParNo = '" & rsComSql.Fields("ParNo").Value & "'")
        '        AdoCN.Execute("DELETE FROM tblExpReturns WHERE ParNo = '" & rsComSql.Fields("ParNo").Value & "'")
        '        AdoCN.Execute("DELETE FROM tblExpReturnDetails WHERE ParNo = '" & rsComSql.Fields("ParNo").Value & "'")
        '        AdoCN.Execute("DELETE FROM tblExpSizingPacket WHERE ParNo = '" & rsComSql.Fields("ParNo").Value & "'")
        '        AdoCN.Execute("DELETE FROM tblExpSizingIssues WHERE ParNo = '" & rsComSql.Fields("ParNo").Value & "'")
        '        AdoCN.Execute("DELETE FROM tblExpSizingReturns WHERE ParNo = '" & rsComSql.Fields("ParNo").Value & "'")
        '        AdoCN.Execute("DELETE FROM tblExpSizingTypes WHERE ParNo = '" & rsComSql.Fields("ParNo").Value & "'")

        '        'AdoCN.Execute("DELETE FROM tblExpStock WHERE ParNo = '" & rsComSql.Fields("ParNo").Value & "'")
        '        'AdoCN.Execute("DELETE FROM tblAssortDetails WHERE AssortBox = '" & rsComSql.Fields("ParNo").Value & "'")
        '        'AdoCN.Execute("DELETE FROM tblAssortOrigin WHERE SupParNo = '" & rsComSql.Fields("ParNo").Value & "'")

        '        rsComSql.MoveNext()
        '    End While
        'End If
        'rsComSql = Nothing
    End Sub

    Private Sub Insert_StockLines()
        Dim intBalPcs As Integer
        Dim intOutPcs As Integer
        Dim blnFound As Boolean

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT ID, ISINCODE, SELLDT, SELLQTY FROM tblStockOut ORDER BY ISINCODE, SELLDT", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                intBalPcs = rsComSql.Fields("SELLQTY").Value
                intOutPcs = 0

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.tblStockIn.ISINCODE, dbo.tblStockIn.ID, dbo.tblStockIn.BUYDT, dbo.tblStockIn.BUYQTY - ISNULL(dbo.VW_StockDetails.BUYQTY, 0) AS BUYQTY, dbo.tblStockIn.BUYPRICE " & _
                                "FROM dbo.tblStockIn LEFT OUTER JOIN dbo.VW_StockDetails ON dbo.tblStockIn.ID = dbo.VW_StockDetails.InID AND dbo.tblStockIn.ISINCODE = dbo.VW_StockDetails.ISINCODE " & _
                                "WHERE (dbo.tblStockIn.ISINCODE = '" & rsComSql.Fields("ISINCODE").Value & "') AND (dbo.tblStockIn.BUYQTY - ISNULL(dbo.VW_StockDetails.BUYQTY, 0) > 0) " & _
                                "ORDER BY dbo.tblStockIn.BUYDT", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    rsComSql_1.MoveFirst()
                    While Not rsComSql_1.EOF And intBalPcs > 0
                        If intBalPcs > 0 Then
                            If intBalPcs <= rsComSql_1.Fields("BUYQTY").Value Then
                                intOutPcs = intBalPcs

                                intBalPcs = 0
                                blnFound = True
                            Else
                                intOutPcs = rsComSql_1.Fields("BUYQTY").Value
                                intBalPcs = intBalPcs - intOutPcs
                                blnFound = True
                            End If

                            If blnFound = True Then
                                AdoCN.Execute("INSERT INTO tblStockDetails(ISINCODE, OutID, InID, BUYDT, BUYQTY, BUYPRICE) " & _
                                              "VALUES('" & rsComSql.Fields("ISINCODE").Value & "','" & rsComSql.Fields("ID").Value & "','" & rsComSql_1.Fields("ID").Value & "','" & rsComSql_1.Fields("BUYDT").Value & "'," & intOutPcs & ",'" & rsComSql_1.Fields("BUYPRICE").Value & "')")

                            End If
                        End If

                        rsComSql_1.MoveNext()
                    End While
                End If
                rsComSql_1 = Nothing

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Insert_OrderPcs()
        Dim intIndex As Integer
        Dim dtpProdDate As Date
        Dim intPcs As Integer
        Dim intBalPcs As Integer

        AdoCN.Execute("DELETE FROM tblMixFinishBal")
        AdoCN.Execute("DELETE FROM tblMixFinishDate")

        AdoCN.Execute("INSERT INTO tblMixFinishBal(OrderNo, Flow, DueDate, BalPcs) SELECT OrderNo, Flow, DueDate, TotPcs - ExpPcs AS BalPcs FROM VW_MIXSummary_StockOrder_Flow WHERE (TotPcs - ExpPcs > 0) ORDER BY OrderNo")

        For intIndex = 0 To 365
            dtpProdDate = DateAdd(DateInterval.Day, intIndex, Date.Now)
            dtpProdDate = CDate(Format(dtpProdDate, "MM/dd/yyyy"))

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblDCLHolidays WHERE Holiday = '" & Format(dtpProdDate, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                GoTo NextRecord
            End If
            rsComSql = Nothing

            intPcs = 550
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixFinishBal.OrderNo, dbo.tblMixFinishBal.Flow, dbo.tblMixFinishBal.BalPcs - ISNULL(dbo.VW_MixFinishDate.Pcs, 0) AS BalPcs " & _
                          "FROM dbo.tblMixFinishBal LEFT OUTER JOIN dbo.VW_MixFinishDate ON dbo.tblMixFinishBal.OrderNo = dbo.VW_MixFinishDate.OrderNo AND dbo.tblMixFinishBal.Flow = dbo.VW_MixFinishDate.Flow " & _
                          "WHERE (dbo.tblMixFinishBal.Flow = 'Polish') AND (dbo.tblMixFinishBal.BalPcs - ISNULL(dbo.VW_MixFinishDate.Pcs, 0) > 0) " & _
                          "ORDER BY dbo.tblMixFinishBal.DueDate, dbo.tblMixFinishBal.OrderNo", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    If intPcs <= 0 Then
                        GoTo NextRecord
                    End If
                    intBalPcs = rsComSql.Fields("BalPcs").Value

                    If intPcs >= intBalPcs Then
                        AdoCN.Execute("INSERT INTO tblMixFinishDate(OrderNo, Flow, Pcs, DDate) VALUES('" & rsComSql.Fields("OrderNo").Value & "','" & rsComSql.Fields("Flow").Value & "','" & intBalPcs & "','" & Format(dtpProdDate, "MM/dd/yyyy") & "')")
                        intPcs = intPcs - intBalPcs
                    Else
                        AdoCN.Execute("INSERT INTO tblMixFinishDate(OrderNo, Flow, Pcs, DDate) VALUES('" & rsComSql.Fields("OrderNo").Value & "','" & rsComSql.Fields("Flow").Value & "','" & intPcs & "','" & Format(dtpProdDate, "MM/dd/yyyy") & "')")
                        intPcs = 0
                    End If
                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing
NextRecord:
        Next

        For intIndex = 0 To 365
            dtpProdDate = DateAdd(DateInterval.Day, intIndex, Date.Now)
            dtpProdDate = CDate(Format(dtpProdDate, "MM/dd/yyyy"))

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblDCLHolidays WHERE Holiday = '" & Format(dtpProdDate, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                GoTo NextRecord2
            End If
            rsComSql = Nothing

            intPcs = 100
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixFinishBal.OrderNo, dbo.tblMixFinishBal.Flow, dbo.tblMixFinishBal.BalPcs - ISNULL(dbo.VW_MixFinishDate.Pcs, 0) AS BalPcs " & _
                          "FROM dbo.tblMixFinishBal LEFT OUTER JOIN dbo.VW_MixFinishDate ON dbo.tblMixFinishBal.OrderNo = dbo.VW_MixFinishDate.OrderNo AND dbo.tblMixFinishBal.Flow = dbo.VW_MixFinishDate.Flow " & _
                          "WHERE (dbo.tblMixFinishBal.Flow = 'Precision') AND (dbo.tblMixFinishBal.BalPcs - ISNULL(dbo.VW_MixFinishDate.Pcs, 0) > 0) " & _
                          "ORDER BY dbo.tblMixFinishBal.DueDate, dbo.tblMixFinishBal.OrderNo", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    If intPcs <= 0 Then
                        GoTo NextRecord2
                    End If
                    intBalPcs = rsComSql.Fields("BalPcs").Value

                    If intPcs >= intBalPcs Then
                        AdoCN.Execute("INSERT INTO tblMixFinishDate(OrderNo, Flow, Pcs, DDate) VALUES('" & rsComSql.Fields("OrderNo").Value & "','" & rsComSql.Fields("Flow").Value & "','" & intBalPcs & "','" & Format(dtpProdDate, "MM/dd/yyyy") & "')")
                        intPcs = intPcs - intBalPcs
                    Else
                        AdoCN.Execute("INSERT INTO tblMixFinishDate(OrderNo, Flow, Pcs, DDate) VALUES('" & rsComSql.Fields("OrderNo").Value & "','" & rsComSql.Fields("Flow").Value & "','" & intPcs & "','" & Format(dtpProdDate, "MM/dd/yyyy") & "')")
                        intPcs = 0
                    End If
                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing
NextRecord2:
        Next

        MsgBox("Completed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
    End Sub

    Private Sub Insert_RprIssRet()
        Dim intIndex As Integer

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrPacket.Department, dbo.tblRPrPacket.ParNo, dbo.tblRPrPacket.PktNo, MAX(dbo.tblRPrIssues.Sec) AS Sec, dbo.tblRPrPacket.PktPcs, dbo.tblRPrPacket.PktCts, MAX(dbo.tblRPrIssues.IssDate) AS IssDate " & _
                       "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrIssues ON dbo.tblRPrPacket.Department = dbo.tblRPrIssues.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrIssues.ParNo AND dbo.tblRPrPacket.PktNo = dbo.tblRPrIssues.PktNo INNER JOIN " & _
                            "dbo.tblParcel ON dbo.tblRPrPacket.Department = dbo.tblParcel.Depart AND dbo.tblRPrPacket.ParNo = dbo.tblParcel.GrpParNo " & _
                        "WHERE (dbo.tblParcel.Complete = 0) AND (dbo.tblParcel.IssueFinish = 0) " & _
                        "GROUP BY dbo.tblRPrPacket.Department, dbo.tblRPrPacket.ParNo, dbo.tblRPrPacket.PktNo, dbo.tblRPrPacket.PktPcs, dbo.tblRPrPacket.PktCts " & _
                        "HAVING (dbo.tblRPrPacket.Department = 'RoughPlan') AND (MAX(dbo.tblRPrIssues.Sec) > 1) AND (MAX(dbo.tblRPrIssues.Sec) < 20) " & _
                        "ORDER BY dbo.tblRPrPacket.ParNo, dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                AdoCN.Execute("UPDATE tblRPrIssues SET Sec = 9, SecCount = 9 WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND ParNo = '" & rsComSql.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "' AND Sec = 2")
                AdoCN.Execute("UPDATE tblRPrReturns SET Sec = 9, SecCount = 9 WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND ParNo = '" & rsComSql.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "' AND Sec = 2")

                AdoCN.Execute("UPDATE tblRPrIssues SET Sec = 10, SecCount = 10 WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND ParNo = '" & rsComSql.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "' AND Sec = 3")
                AdoCN.Execute("UPDATE tblRPrReturns SET Sec = 10, SecCount = 10 WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND ParNo = '" & rsComSql.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "' AND Sec = 3")

                AdoCN.Execute("UPDATE tblRPrIssues SET Sec = 11, SecCount = 11 WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND ParNo = '" & rsComSql.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "' AND Sec = 4")
                AdoCN.Execute("UPDATE tblRPrReturns SET Sec = 11, SecCount = 11 WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND ParNo = '" & rsComSql.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "' AND Sec = 4")

                AdoCN.Execute("UPDATE tblRPrIssues SET Sec = 12, SecCount = 12 WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND ParNo = '" & rsComSql.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "' AND Sec = 5")
                AdoCN.Execute("UPDATE tblRPrReturns SET Sec = 12, SecCount = 12 WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND ParNo = '" & rsComSql.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "' AND Sec = 5")

                AdoCN.Execute("UPDATE tblRPrIssues SET Sec = 13, SecCount = 13 WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND ParNo = '" & rsComSql.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "' AND Sec = 6")
                AdoCN.Execute("UPDATE tblRPrReturns SET Sec = 13, SecCount = 13 WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND ParNo = '" & rsComSql.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "' AND Sec = 6")

                AdoCN.Execute("UPDATE tblRPrIssues SET Sec = 14, SecCount = 14 WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND ParNo = '" & rsComSql.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "' AND Sec = 7")
                AdoCN.Execute("UPDATE tblRPrReturns SET Sec = 14, SecCount = 14 WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND ParNo = '" & rsComSql.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "' AND Sec = 7")

                For intIndex = 2 To 8
                    'Issues
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT PktNo FROM tblRPrIssues WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND ParNo = '" & rsComSql.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "' AND Sec = " & intIndex & "", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount = 0 Then
                        AdoCN.Execute("INSERT INTO tblRPrIssues(Department,ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime) " & _
                                      "VALUES('" & rsComSql.Fields("Department").Value & "','" & rsComSql.Fields("ParNo").Value & "','" & rsComSql.Fields("PktNo").Value & "','" & intIndex & "','PlanFlow','" & intIndex & "','D06975','" & rsComSql.Fields("PktPcs").Value & "',0,'" & rsComSql.Fields("PktCts").Value & "','" & Format(rsComSql.Fields("IssDate").Value, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")
                    End If
                    rsComSql_1 = Nothing

                    'Returns
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT PktNo FROM tblRPrReturns WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND ParNo = '" & rsComSql.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "' AND Sec = " & intIndex & "", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount = 0 Then
                        AdoCN.Execute("INSERT INTO tblRPrReturns(Department,ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,UserName,CompName) " & _
                                      "VALUES('" & rsComSql.Fields("Department").Value & "','" & rsComSql.Fields("ParNo").Value & "','" & rsComSql.Fields("PktNo").Value & "','PlanFlow','" & intIndex & "','" & intIndex & "','D06975','" & rsComSql.Fields("PktPcs").Value & "',0,'" & rsComSql.Fields("PktCts").Value & "',0,0,0,0,0,0,0,'" & Format(rsComSql.Fields("IssDate").Value, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,'" & PBUser_EmpNo & "','" & PBCompName & "')")
                    End If
                    rsComSql_1 = Nothing
                Next
                

                rsComSql.MoveNext()
            End While
        End If
    End Sub

    Private Sub HazelDev_Button24_Click(sender As Object, e As EventArgs) Handles HazelDev_Button24.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLAssort2019.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button25_Click(sender As Object, e As EventArgs) Handles HazelDev_Button25.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLPacketColor.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button26_Click(sender As Object, e As EventArgs) Handles HazelDev_Button26.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLPacketClarity.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button27_Click(sender As Object, e As EventArgs) Handles HazelDev_Button27.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLAssort2019_2.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button28_Click(sender As Object, e As EventArgs) Handles HazelDev_Button28.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLCostingRough.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button29_Click(sender As Object, e As EventArgs) Handles HazelDev_Button29.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLStoneDaysLot.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button30_Click(sender As Object, e As EventArgs) Handles HazelDev_Button30.Click
        objForm = New frm_DCLReportViewer
        If strDBName = "DiaStock" Then
            mReportName = "crptDCLLostDetails.rpt"
        Else
            mReportName = "crptDCLLostDetailsSales.rpt"
        End If
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button31_Click(sender As Object, e As EventArgs) Handles HazelDev_Button31.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLRghStoneDaysLot.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button32_Click(sender As Object, e As EventArgs) Handles HazelDev_Button32.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLGradingStoneDaysLot.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button33_Click(sender As Object, e As EventArgs) Handles HazelDev_Button33.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLStoneCtsDaysLot.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button34_Click(sender As Object, e As EventArgs) Handles HazelDev_Button34.Click
        objForm = New frm_DCLReportViewer
        If strDBName = "DiaStock" Then
            mReportName = "crptRghBoxStock.rpt"
        Else
            mReportName = "crptRghBoxStockSales.rpt"
        End If
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button35_Click(sender As Object, e As EventArgs) Handles HazelDev_Button35.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAssortListNew.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button36_Click(sender As Object, e As EventArgs) Handles HazelDev_Button36.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLParcelTurnAround2.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button37_Click(sender As Object, e As EventArgs) Handles HazelDev_Button37.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptFanProdSum.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button38_Click(sender As Object, e As EventArgs) Handles HazelDev_Button38.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRndProdSum.rpt"
        strReportPath = PBReportPath & "RoundsFullFlow\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button39_Click(sender As Object, e As EventArgs) Handles HazelDev_Button39.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptFanProdSum_All.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button40_Click(sender As Object, e As EventArgs) Handles HazelDev_Button40.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptFinalRepairProd.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button41_Click(sender As Object, e As EventArgs) Handles HazelDev_Button41.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLLotPlanValue.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button42_Click(sender As Object, e As EventArgs) Handles HazelDev_Button42.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingAASummary.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button43_Click(sender As Object, e As EventArgs) Handles HazelDev_Button43.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingABSummary.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button44_Click(sender As Object, e As EventArgs) Handles HazelDev_Button44.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingAASummarySearch.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button45_Click(sender As Object, e As EventArgs) Handles HazelDev_Button45.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRghExportDetailsReIssue.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button46_Click(sender As Object, e As EventArgs) Handles HazelDev_Button46.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGrading_RndLotAnalysis2020.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button47_Click(sender As Object, e As EventArgs) Handles HazelDev_Button47.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLStockValueFinSize.rpt"
        strReportPath = PBReportPath & "DiaSalesPolishBox\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button48_Click(sender As Object, e As EventArgs) Handles HazelDev_Button48.Click
        objForm = New frm_DCLReportViewer
        'If strDBName = "DiaStock" Then
        '    mReportName = "crptDCLLostDetails_Centre.rpt"
        'Else
        '    mReportName = "crptDCLLostDetailsSales_Centre.rpt"
        'End If
        mReportName = "crptDCLLost-DeductionHRDept.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button49_Click(sender As Object, e As EventArgs) Handles HazelDev_Button49.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRghExportDetailsPCURgh.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button50_Click(sender As Object, e As EventArgs) Handles HazelDev_Button50.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRndProdSum_All.rpt"
        strReportPath = PBReportPath & "RoundsFullFlow\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button51_Click(sender As Object, e As EventArgs) Handles HazelDev_Button51.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRprLotAchieve.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button52_Click(sender As Object, e As EventArgs) Handles HazelDev_Button52.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLLost-FoundDetailsHRDept.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button53_Click(sender As Object, e As EventArgs) Handles HazelDev_Button53.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLLostDetails_AllPar_OrdLostRejBro.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button54_Click(sender As Object, e As EventArgs) Handles HazelDev_Button54.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLLost-MonthlyReport.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button55_Click(sender As Object, e As EventArgs) Handles HazelDev_Button55.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLStock2020_2.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button56_Click(sender As Object, e As EventArgs) Handles HazelDev_Button56.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLStock2020_2Pol.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button57_Click(sender As Object, e As EventArgs) Handles HazelDev_Button57.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAmsLabExcel.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button58_Click(sender As Object, e As EventArgs) Handles HazelDev_Button58.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingAASummaryNew.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button59_Click(sender As Object, e As EventArgs) Handles HazelDev_Button59.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptFanProdSum2.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button60_Click(sender As Object, e As EventArgs) Handles HazelDev_Button60.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLStock2020_2.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button61_Click(sender As Object, e As EventArgs) Handles HazelDev_Button61.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRghExportDetailsPCURgh.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button62_Click(sender As Object, e As EventArgs) Handles HazelDev_Button62.Click
        Dim intAlrosa As Integer
        Dim intNurba As Integer
        Dim intTotAlrosa As Integer
        Dim intDeBeers As Integer
        Dim intEkati As Integer
        Dim intVenatia As Integer
        Dim intDiavik As Integer
        Dim Rnd As Random = New Random

        intTotAlrosa = 30
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM AuctionMixDis ORDER BY ID", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                If UCase(Mid(rsComSql.Fields("ItemName").Value, 1, 5)) = "UGADI" Then
                    intAlrosa = 0
                    intNurba = 0
                    intDeBeers = 0
                    intDiavik = 0
                    intVenatia = Rnd.Next(10, 55)
                    intEkati = 100 - intVenatia
                Else
                    intAlrosa = Rnd.Next(20, 25)
                    If intTotAlrosa > 35 Then
                        intTotAlrosa = 30
                    End If
                    intNurba = intTotAlrosa - intAlrosa
                    intDeBeers = Rnd.Next(10, 15)
                    intEkati = Rnd.Next(25, 30)
                    intVenatia = Rnd.Next(20, 25)
                    If intAlrosa + intNurba + intDeBeers + intEkati + intVenatia >= 100 Then
                        intVenatia = 20
                        intEkati = 25
                    End If
                    intTotAlrosa = intTotAlrosa + 1

                    intDiavik = 100 - (intAlrosa + intNurba + intDeBeers + intEkati + intVenatia)
                End If

                AdoCN.Execute("UPDATE AuctionMixDis SET alrosa = " & intAlrosa & ",nurba = " & intNurba & ",deebeer = " & intDeBeers & "," & _
                              "ekaticanada = " & intEkati & ",venatiasouthafrica = " & intVenatia & ",diavikcanada = " & intDiavik & " WHERE ID = '" & rsComSql.Fields("ID").Value & "'")

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub HazelDev_Button63_Click(sender As Object, e As EventArgs) Handles HazelDev_Button63.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAssortList.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button64_Click(sender As Object, e As EventArgs) Handles HazelDev_Button64.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAssortSummary.rpt"
        strReportPath = PBReportPath & "NiruStock\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button65_Click(sender As Object, e As EventArgs) Handles HazelDev_Button65.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixStock.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button66_Click(sender As Object, e As EventArgs) Handles HazelDev_Button66.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLStock2020_2Pol.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button67_Click(sender As Object, e As EventArgs) Handles HazelDev_Button67.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptEMPAttendance.rpt"
        strReportPath = PBReportPath & "HR\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button68_Click(sender As Object, e As EventArgs) Handles HazelDev_Button68.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLGradSum.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button69_Click(sender As Object, e As EventArgs) Handles HazelDev_Button69.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLGradSumRnd.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button70_Click(sender As Object, e As EventArgs) Handles HazelDev_Button70.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLParcelTurnAround3.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button71_Click(sender As Object, e As EventArgs) Handles HazelDev_Button71.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptEmpHours.rpt"
        strReportPath = PBReportPath & "HR\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button72_Click(sender As Object, e As EventArgs) Handles HazelDev_Button72.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptEMPAttendanceSum.rpt"
        strReportPath = PBReportPath & "HR\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button73_Click(sender As Object, e As EventArgs) Handles HazelDev_Button73.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLLost-FoundDetailsHRDept2.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button74_Click(sender As Object, e As EventArgs) Handles HazelDev_Button74.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLLostDetails_AllPar_OrdRejBro_All2.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button75_Click(sender As Object, e As EventArgs) Handles HazelDev_Button75.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLStock2020_2All.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button76_Click(sender As Object, e As EventArgs) Handles HazelDev_Button76.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLLostAllEmps.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button78_Click(sender As Object, e As EventArgs) Handles HazelDev_Button78.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLLostDetailsPeriodic.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button77_Click(sender As Object, e As EventArgs) Handles HazelDev_Button77.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLFoundDetailsPeriodic.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button79_Click(sender As Object, e As EventArgs) Handles HazelDev_Button79.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLIncentiveUnits.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button80_Click(sender As Object, e As EventArgs) Handles HazelDev_Button80.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLRelAllEmps.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button81_Click(sender As Object, e As EventArgs) Handles HazelDev_Button81.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingAASummaryNewPkt.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button82_Click(sender As Object, e As EventArgs) Handles HazelDev_Button82.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRghExportDetails2.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button83_Click(sender As Object, e As EventArgs) Handles HazelDev_Button83.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptBagUpgrade.rpt"
        strReportPath = PBReportPath & "HR\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button84_Click(sender As Object, e As EventArgs) Handles HazelDev_Button84.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPRUpgrade.rpt"
        strReportPath = PBReportPath & "HR\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button85_Click(sender As Object, e As EventArgs) Handles HazelDev_Button85.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRndUpgrade.rpt"
        strReportPath = PBReportPath & "HR\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button86_Click(sender As Object, e As EventArgs) Handles HazelDev_Button86.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixUpgrade.rpt"
        strReportPath = PBReportPath & "HR\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button87_Click(sender As Object, e As EventArgs) Handles HazelDev_Button87.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixUpgradeGr.rpt"
        strReportPath = PBReportPath & "HR\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button88_Click(sender As Object, e As EventArgs) Handles HazelDev_Button88.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRprUpgrade.rpt"
        strReportPath = PBReportPath & "HR\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button89_Click(sender As Object, e As EventArgs) Handles HazelDev_Button89.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLUpgradeProdNonProd.rpt"
        strReportPath = PBReportPath & "HR\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button90_Click(sender As Object, e As EventArgs) Handles HazelDev_Button90.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLUpgradeNonProd.rpt"
        strReportPath = PBReportPath & "HR\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button91_Click(sender As Object, e As EventArgs) Handles HazelDev_Button91.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixUpgradeXY.rpt"
        strReportPath = PBReportPath & "HR\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button92_Click(sender As Object, e As EventArgs) Handles HazelDev_Button92.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLUpgradeProdNonProdLB.rpt"
        strReportPath = PBReportPath & "HR\" & mReportName
        objForm.Show()
    End Sub
End Class