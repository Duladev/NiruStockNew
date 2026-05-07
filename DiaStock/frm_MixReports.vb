
Public Class frm_MixReports

    Dim strFolderPath As String

    Private Sub frm_MixReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        If strDBName = "DiaStock" Then
            strFolderPath = "DiaSalesMix\"
        ElseIf strDBName = "DiaSales" Then
            strFolderPath = "DiaSalesMix\"
        Else
            strFolderPath = "DiaShareMix\"
        End If
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixModIss.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixRej.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXInRepairs.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button4_Click(sender As Object, e As EventArgs) Handles HazelDev_Button4.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXFinishIssues.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button5_Click(sender As Object, e As EventArgs) Handles HazelDev_Button5.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixEmpProduction.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button6_Click(sender As Object, e As EventArgs) Handles HazelDev_Button6.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXGrpProdReport.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button7_Click(sender As Object, e As EventArgs) Handles HazelDev_Button7.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXEmpProdSummary.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button8_Click(sender As Object, e As EventArgs) Handles HazelDev_Button8.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXProductionInfo.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button9_Click(sender As Object, e As EventArgs) Handles HazelDev_Button9.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXStkOrdWiseGrp.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button10_Click(sender As Object, e As EventArgs) Handles HazelDev_Button10.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXPlanOrd_Ord_Ref.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button11_Click(sender As Object, e As EventArgs) Handles HazelDev_Button11.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXAssortDetails.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button12_Click(sender As Object, e As EventArgs) Handles HazelDev_Button12.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXProductionInfo_CheckEmp.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button13_Click(sender As Object, e As EventArgs) Handles HazelDev_Button13.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXStkClientordWise.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button14_Click(sender As Object, e As EventArgs) Handles HazelDev_Button14.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXPktInfo.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button15_Click(sender As Object, e As EventArgs) Handles HazelDev_Button15.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXAssortSIH.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button16_Click(sender As Object, e As EventArgs) Handles HazelDev_Button16.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixRejects.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button17_Click(sender As Object, e As EventArgs) Handles HazelDev_Button17.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXRejectDetails.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button18_Click(sender As Object, e As EventArgs) Handles HazelDev_Button18.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXExpGrpWise.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button19_Click(sender As Object, e As EventArgs) Handles HazelDev_Button19.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXStkOrdWiseGrp2017SumPlan.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button20_Click(sender As Object, e As EventArgs) Handles HazelDev_Button20.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXStkOrdWiseGrp2017Sum.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button21_Click(sender As Object, e As EventArgs) Handles HazelDev_Button21.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixTrf.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button22_Click(sender As Object, e As EventArgs) Handles HazelDev_Button22.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPktIssType.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button23_Click(sender As Object, e As EventArgs) Handles HazelDev_Button23.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXEmpProdSummaryAll.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button24_Click(sender As Object, e As EventArgs) Handles HazelDev_Button24.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXProdSummary.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button25_Click(sender As Object, e As EventArgs) Handles HazelDev_Button25.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXOrderReport.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button26_Click(sender As Object, e As EventArgs) Handles HazelDev_Button26.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXProductionInfoBoth.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button27_Click(sender As Object, e As EventArgs) Handles HazelDev_Button27.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixShipmentBalanceNew.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button28_Click(sender As Object, e As EventArgs) Handles HazelDev_Button28.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXStkPktWiseShip.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button29_Click(sender As Object, e As EventArgs) Handles HazelDev_Button29.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixShipmentBalance.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button30_Click(sender As Object, e As EventArgs) Handles HazelDev_Button30.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXOrderPlanSummary.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button31_Click(sender As Object, e As EventArgs) Handles HazelDev_Button31.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXGrpProdQltyReport.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button32_Click(sender As Object, e As EventArgs) Handles HazelDev_Button32.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXStkOrdWiseGrp2017SumPlanGrp.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button33_Click(sender As Object, e As EventArgs) Handles HazelDev_Button33.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXStkOrdWiseGrp2017.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button34_Click(sender As Object, e As EventArgs) Handles HazelDev_Button34.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPacketDetailsNew.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button35_Click(sender As Object, e As EventArgs) Handles HazelDev_Button35.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXProductionInfo_CheckEmpNew.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button36_Click(sender As Object, e As EventArgs) Handles HazelDev_Button36.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXStkOrdWiseGrp2017SumPlanRef.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button37_Click(sender As Object, e As EventArgs) Handles HazelDev_Button37.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXStockInHand.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button38_Click(sender As Object, e As EventArgs) Handles HazelDev_Button38.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixOrderSummary.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button39_Click(sender As Object, e As EventArgs) Handles HazelDev_Button39.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXRejectDetailsBoth.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button40_Click(sender As Object, e As EventArgs) Handles HazelDev_Button40.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixOrdRfSd_ExpSummuryBOTH.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button41_Click(sender As Object, e As EventArgs) Handles HazelDev_Button41.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXOrdSummary.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button42_Click(sender As Object, e As EventArgs) Handles HazelDev_Button42.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixFinishOrdSummary.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button43_Click(sender As Object, e As EventArgs) Handles HazelDev_Button43.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAssortByDate.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button44_Click(sender As Object, e As EventArgs) Handles HazelDev_Button44.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXRepairDetails.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button45_Click(sender As Object, e As EventArgs) Handles HazelDev_Button45.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixGrovProd.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button46_Click(sender As Object, e As EventArgs) Handles HazelDev_Button46.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixCheckProd.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button47_Click(sender As Object, e As EventArgs) Handles HazelDev_Button47.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXFinishIssuesDate.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button48_Click(sender As Object, e As EventArgs) Handles HazelDev_Button48.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixEmpReturns.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button49_Click(sender As Object, e As EventArgs) Handles HazelDev_Button49.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixVerifiedPkt.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button50_Click(sender As Object, e As EventArgs) Handles HazelDev_Button50.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXNotExport.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button51_Click(sender As Object, e As EventArgs) Handles HazelDev_Button51.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXStkOrdWiseGrpSummary.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button52_Click(sender As Object, e As EventArgs) Handles HazelDev_Button52.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXStkOrdWiseGrpSummaryType.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button53_Click(sender As Object, e As EventArgs) Handles HazelDev_Button53.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixAnalysisRejDetails_Period.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button54_Click(sender As Object, e As EventArgs) Handles HazelDev_Button54.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixGradingFinishedOrdersPCU.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button55_Click(sender As Object, e As EventArgs) Handles HazelDev_Button55.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixOrderWiseCostAll.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button56_Click(sender As Object, e As EventArgs) Handles HazelDev_Button56.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixOrderMaxCost.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button57_Click(sender As Object, e As EventArgs) Handles HazelDev_Button57.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixOrderMaxCostSum.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button58_Click(sender As Object, e As EventArgs) Handles HazelDev_Button58.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixOrderSummaryPeriod.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button59_Click(sender As Object, e As EventArgs) Handles HazelDev_Button59.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixOrderDetails.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button60_Click(sender As Object, e As EventArgs) Handles HazelDev_Button60.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixConfirmOrders.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button61_Click(sender As Object, e As EventArgs) Handles HazelDev_Button61.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixConfirmOrdersDetails.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button62_Click(sender As Object, e As EventArgs) Handles HazelDev_Button62.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixOrderWiseCost.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button63_Click(sender As Object, e As EventArgs) Handles HazelDev_Button63.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXAssortStockTake.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button64_Click(sender As Object, e As EventArgs) Handles HazelDev_Button64.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXAssortStockTake3.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button65_Click(sender As Object, e As EventArgs) Handles HazelDev_Button65.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixRepairStock.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button66_Click(sender As Object, e As EventArgs) Handles HazelDev_Button66.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPacketIssuesMaxCost.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button67_Click(sender As Object, e As EventArgs) Handles HazelDev_Button67.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixRepairIssRet.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button68_Click(sender As Object, e As EventArgs) Handles HazelDev_Button68.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixShipmentBalance2020.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button69_Click(sender As Object, e As EventArgs) Handles HazelDev_Button69.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixShipmentBalance2020Niru.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button70_Click(sender As Object, e As EventArgs) Handles HazelDev_Button70.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixOrdRfSd_ExpSummary.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button71_Click(sender As Object, e As EventArgs) Handles HazelDev_Button71.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixShipmentBalance2020_1.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button72_Click(sender As Object, e As EventArgs) Handles HazelDev_Button72.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixVerifiedPktGrp.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button73_Click(sender As Object, e As EventArgs) Handles HazelDev_Button73.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixVerifiedPktGrp2.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button74_Click(sender As Object, e As EventArgs) Handles HazelDev_Button74.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPCUStockSize2.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button75_Click(sender As Object, e As EventArgs) Handles HazelDev_Button75.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPCUStockSize4.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button76_Click(sender As Object, e As EventArgs) Handles HazelDev_Button76.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPacketValue.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button77_Click(sender As Object, e As EventArgs) Handles HazelDev_Button77.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXStkOrdWiseGrpSummary2020.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button78_Click(sender As Object, e As EventArgs) Handles HazelDev_Button78.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixReturnsTime.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button79_Click(sender As Object, e As EventArgs) Handles HazelDev_Button79.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXEmpProdSummaryEMP.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button80_Click(sender As Object, e As EventArgs) Handles HazelDev_Button80.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPending18.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button81_Click(sender As Object, e As EventArgs) Handles HazelDev_Button81.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXInRepairsGr.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button82_Click(sender As Object, e As EventArgs) Handles HazelDev_Button82.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixBoilingIssRet.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button83_Click(sender As Object, e As EventArgs) Handles HazelDev_Button83.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixBoilingStock.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button84_Click(sender As Object, e As EventArgs) Handles HazelDev_Button84.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXAssortSizeTypeAN.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button85_Click(sender As Object, e As EventArgs) Handles HazelDev_Button85.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixRghIss.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button86_Click(sender As Object, e As EventArgs) Handles HazelDev_Button86.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXPacketingSIH.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button87_Click(sender As Object, e As EventArgs) Handles HazelDev_Button87.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXStkOrdWiseGrp2017SumPlanGrpPcu1.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button88_Click(sender As Object, e As EventArgs) Handles HazelDev_Button88.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixReturnsFlow.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button89_Click(sender As Object, e As EventArgs) Handles HazelDev_Button89.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixRejectConverts.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button90_Click(sender As Object, e As EventArgs) Handles HazelDev_Button90.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixOrderWiseCostShipDate.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button91_Click(sender As Object, e As EventArgs) Handles HazelDev_Button91.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPacketIssRej.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button92_Click(sender As Object, e As EventArgs) Handles HazelDev_Button92.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXFinishIssues16.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button93_Click(sender As Object, e As EventArgs) Handles HazelDev_Button93.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixReturns16Details.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button94_Click(sender As Object, e As EventArgs) Handles HazelDev_Button94.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXNotExportOrder.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button95_Click(sender As Object, e As EventArgs) Handles HazelDev_Button95.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixReturns16DetailsDate.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button96_Click(sender As Object, e As EventArgs) Handles HazelDev_Button96.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixOrderWiseCostMulti.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button97_Click(sender As Object, e As EventArgs) Handles HazelDev_Button97.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXGrpProdSumReport.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button98_Click(sender As Object, e As EventArgs) Handles HazelDev_Button98.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXIncentiveAllEmp.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button99_Click(sender As Object, e As EventArgs) Handles HazelDev_Button99.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXIncentiveDeptWise.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button100_Click(sender As Object, e As EventArgs) Handles HazelDev_Button100.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXEmpProdSumGrp.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button101_Click(sender As Object, e As EventArgs) Handles HazelDev_Button101.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixRejectConvertsValue.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button102_Click(sender As Object, e As EventArgs) Handles HazelDev_Button102.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixCostingLab.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button103_Click(sender As Object, e As EventArgs) Handles HazelDev_Button103.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixCostingLabCertify.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button104_Click(sender As Object, e As EventArgs) Handles HazelDev_Button104.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXPktNotStock.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button105_Click(sender As Object, e As EventArgs) Handles HazelDev_Button105.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXStkOrdWiseRef.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button106_Click(sender As Object, e As EventArgs) Handles HazelDev_Button106.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixRejectReasons.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button107_Click(sender As Object, e As EventArgs) Handles HazelDev_Button107.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPacketIssRejProd.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button108_Click(sender As Object, e As EventArgs) Handles HazelDev_Button108.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixReturns18.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button109_Click(sender As Object, e As EventArgs) Handles HazelDev_Button109.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixFinishOrderGrp.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button110_Click(sender As Object, e As EventArgs) Handles HazelDev_Button110.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPacketIssRejProdNew.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button111_Click(sender As Object, e As EventArgs) Handles HazelDev_Button111.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixEmpProductionHR.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button112_Click(sender As Object, e As EventArgs) Handles HazelDev_Button112.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixShipmentPlanP1P2.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button113_Click(sender As Object, e As EventArgs) Handles HazelDev_Button113.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXStkOrdWiseGrpSummaryOld.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button114_Click(sender As Object, e As EventArgs) Handles HazelDev_Button114.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixFantacySchemaSum.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button115_Click(sender As Object, e As EventArgs) Handles HazelDev_Button115.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXGrpProdQltyReport18.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button116_Click(sender As Object, e As EventArgs) Handles HazelDev_Button116.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixCheckProdClient.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button117_Click(sender As Object, e As EventArgs) Handles HazelDev_Button117.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXPlanOrd_Ord_Ref2.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button118_Click(sender As Object, e As EventArgs) Handles HazelDev_Button118.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixReturnsEmp.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button119_Click(sender As Object, e As EventArgs) Handles HazelDev_Button119.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixOrderDetailsPlan.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button120_Click(sender As Object, e As EventArgs) Handles HazelDev_Button120.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXAssortSizeTypeNew.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button121_Click(sender As Object, e As EventArgs) Handles HazelDev_Button121.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPacketValueDetails.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button122_Click(sender As Object, e As EventArgs) Handles HazelDev_Button122.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPacketIssuesCat.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button123_Click(sender As Object, e As EventArgs) Handles HazelDev_Button123.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixBoxInCat.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button124_Click(sender As Object, e As EventArgs) Handles HazelDev_Button124.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixRejectValue2.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button125_Click(sender As Object, e As EventArgs) Handles HazelDev_Button125.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXAssortSizeTypeIn.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button126_Click(sender As Object, e As EventArgs) Handles HazelDev_Button126.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXAssortSizeTypeOut.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button127_Click(sender As Object, e As EventArgs) Handles HazelDev_Button127.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPacketNoGrp.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button128_Click(sender As Object, e As EventArgs) Handles HazelDev_Button128.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXAssortStockTake4.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button129_Click(sender As Object, e As EventArgs) Handles HazelDev_Button129.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixAssortConvertDetails.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button130_Click(sender As Object, e As EventArgs) Handles HazelDev_Button130.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixProdIssues.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button131_Click(sender As Object, e As EventArgs) Handles HazelDev_Button131.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixReturns18NotVerified.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button132_Click(sender As Object, e As EventArgs) Handles HazelDev_Button132.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXStockInHandDelay.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button133_Click(sender As Object, e As EventArgs) Handles HazelDev_Button133.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixIncentiveUnits.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button134_Click(sender As Object, e As EventArgs) Handles HazelDev_Button134.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixOrderPacketDetails.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button135_Click(sender As Object, e As EventArgs) Handles HazelDev_Button135.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixIncentiveUnitsTarget.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button136_Click(sender As Object, e As EventArgs) Handles HazelDev_Button136.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixStoneReqDetailsLenWidYMSum10NewAutoKaran.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button137_Click(sender As Object, e As EventArgs) Handles HazelDev_Button137.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixStoneReqDetailsLenWidYMPlanDate.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub
End Class