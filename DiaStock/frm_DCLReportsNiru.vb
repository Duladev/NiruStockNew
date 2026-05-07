Public Class frm_DCLReportsNiru

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLImportBreakdownAssortAll.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLImportBreakdownAssortAll_Period.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button4_Click(sender As Object, e As EventArgs) Handles HazelDev_Button4.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLPolishProfit.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button5_Click(sender As Object, e As EventArgs) Handles HazelDev_Button5.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPCUStock.rpt"
        If strDBName = "DiaShare" Then
            strReportPath = PBReportPath & "DiaShareMix\" & mReportName
        Else
            strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        End If
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button6_Click(sender As Object, e As EventArgs) Handles HazelDev_Button6.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPCUStockV.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLStockValue.rpt"
        strReportPath = PBReportPath & "DiaSalesPolishBox\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button7_Click(sender As Object, e As EventArgs) Handles HazelDev_Button7.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLStockFantacy2Comp.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button8_Click(sender As Object, e As EventArgs) Handles HazelDev_Button8.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLProfitLossLotAll.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button9_Click(sender As Object, e As EventArgs) Handles HazelDev_Button9.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLPolishProfitParcel.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button10_Click(sender As Object, e As EventArgs) Handles HazelDev_Button10.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRprProdSummaryLot2.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button11_Click(sender As Object, e As EventArgs) Handles HazelDev_Button11.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRprProdSummaryLotSum.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button12_Click(sender As Object, e As EventArgs) Handles HazelDev_Button12.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRprPacketDetailsRounds.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button13_Click(sender As Object, e As EventArgs) Handles HazelDev_Button13.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRndFinishAssortDetails.rpt"
        strReportPath = PBReportPath & "RoundsFullFlow\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button14_Click(sender As Object, e As EventArgs) Handles HazelDev_Button14.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRprForecastAllNew.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button15_Click(sender As Object, e As EventArgs) Handles HazelDev_Button15.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixCosting.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button16_Click(sender As Object, e As EventArgs) Handles HazelDev_Button16.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLCostingOrder.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button17_Click(sender As Object, e As EventArgs) Handles HazelDev_Button17.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLCostingOrderSales.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button18_Click(sender As Object, e As EventArgs) Handles HazelDev_Button18.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLPolishProfitLot.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button19_Click(sender As Object, e As EventArgs) Handles HazelDev_Button19.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLPolishProfitLotInvSummary.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button20_Click(sender As Object, e As EventArgs) Handles HazelDev_Button20.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLProfitLossLotAllItemNameSearch.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button21_Click(sender As Object, e As EventArgs) Handles HazelDev_Button21.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLPolishProfitDCL.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button22_Click(sender As Object, e As EventArgs) Handles HazelDev_Button22.Click
        Dim Instring As String

        Instring = UCase(InputBox("Enter Access Code", "Authorized Password"))
        If Instring = "REV1981" Then
            objForm = New frm_DCLReportViewer
            mReportName = "crptDCLCostingSumNew.rpt"
            strReportPath = PBReportPath & "GroupNiru\" & mReportName
            objForm.Show()
        End If
    End Sub

    Private Sub HazelDev_Button23_Click(sender As Object, e As EventArgs) Handles HazelDev_Button23.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXStkOrdWiseGrp2017SumPlanRef.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button24_Click(sender As Object, e As EventArgs) Handles HazelDev_Button24.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXStkOrdWiseGrp2017SumPlanGrp.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button25_Click(sender As Object, e As EventArgs) Handles HazelDev_Button25.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXStkOrdWiseGrp2017Sum-TMP2.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button26_Click(sender As Object, e As EventArgs) Handles HazelDev_Button26.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLProfitLossAssortDetails.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button27_Click(sender As Object, e As EventArgs) Handles HazelDev_Button27.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLPolishProfitDCLOrig.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button28_Click(sender As Object, e As EventArgs) Handles HazelDev_Button28.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLProfitLossAssortList.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button29_Click(sender As Object, e As EventArgs) Handles HazelDev_Button29.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLLotPlanValue.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button30_Click(sender As Object, e As EventArgs) Handles HazelDev_Button30.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXStkOrdWiseGrp2017SumType.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button31_Click(sender As Object, e As EventArgs) Handles HazelDev_Button31.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPCUStockSize2.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button32_Click(sender As Object, e As EventArgs) Handles HazelDev_Button32.Click
        objForm = New frm_DCLReportViewer
        mReportName = "CrptMIXRevFinanceSum.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button33_Click(sender As Object, e As EventArgs) Handles HazelDev_Button33.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixExports.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button34_Click(sender As Object, e As EventArgs) Handles HazelDev_Button34.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLLotSchedule.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button35_Click(sender As Object, e As EventArgs) Handles HazelDev_Button35.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLProfitLossLotAllImp.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button36_Click(sender As Object, e As EventArgs) Handles HazelDev_Button36.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLCostingLabourSum.rpt"
        strReportPath = PBReportPath & "ExportFin\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button37_Click(sender As Object, e As EventArgs) Handles HazelDev_Button37.Click
        objForm = New frm_DCLReportViewer
        mReportName = "CrptPCURevFinanceSum.rpt"
        strReportPath = PBReportPath & "Precision\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button38_Click(sender As Object, e As EventArgs) Handles HazelDev_Button38.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLStockValueNew.rpt"
        strReportPath = PBReportPath & "DiaSalesPolishBox\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button39_Click(sender As Object, e As EventArgs) Handles HazelDev_Button39.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpSizingFinishStockAssort2.rpt"
        If strDBName = "DiaStock" Then
            strReportPath = PBReportPath & "Export\" & mReportName
        Else
            strReportPath = PBReportPath & "DiaSalesExport\" & mReportName
        End If
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button40_Click(sender As Object, e As EventArgs) Handles HazelDev_Button40.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLProfitLossLotAllImp_Item.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button41_Click(sender As Object, e As EventArgs) Handles HazelDev_Button41.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLProfitLossLotAllImpR.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button42_Click(sender As Object, e As EventArgs) Handles HazelDev_Button42.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRndProdSum.rpt"
        strReportPath = PBReportPath & "RoundsFullFlow\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button43_Click(sender As Object, e As EventArgs) Handles HazelDev_Button43.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptFanProdSum.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button44_Click(sender As Object, e As EventArgs) Handles HazelDev_Button44.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptARSumDays.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button45_Click(sender As Object, e As EventArgs) Handles HazelDev_Button45.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLPolishProfitPlanComp.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button46_Click(sender As Object, e As EventArgs) Handles HazelDev_Button46.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRndLotSummary.rpt"
        strReportPath = PBReportPath & "RoundsFullFlow\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button47_Click(sender As Object, e As EventArgs) Handles HazelDev_Button47.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLStockInRounds.rpt"
        strReportPath = PBReportPath & "DiaSalesPolishBox\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button48_Click(sender As Object, e As EventArgs) Handles HazelDev_Button48.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLProfitLossLotAllImpMilinda.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button49_Click(sender As Object, e As EventArgs) Handles HazelDev_Button49.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixOrderBalStockPcs.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button50_Click(sender As Object, e As EventArgs) Handles HazelDev_Button50.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPCUStockSize4.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button51_Click(sender As Object, e As EventArgs) Handles HazelDev_Button51.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixOrderBalPcs2.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button52_Click(sender As Object, e As EventArgs) Handles HazelDev_Button52.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixStockPcs2.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button53_Click(sender As Object, e As EventArgs) Handles HazelDev_Button53.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixCostingOrigin2.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button54_Click(sender As Object, e As EventArgs) Handles HazelDev_Button54.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixCostingMaxCost.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button55_Click(sender As Object, e As EventArgs) Handles HazelDev_Button55.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXStkOrdWiseGrp2018.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button56_Click(sender As Object, e As EventArgs) Handles HazelDev_Button56.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixCostingOriginClient.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button57_Click(sender As Object, e As EventArgs) Handles HazelDev_Button57.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixExportsTrend.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button58_Click(sender As Object, e As EventArgs) Handles HazelDev_Button58.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixOrderSubjectPcs.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button59_Click(sender As Object, e As EventArgs) Handles HazelDev_Button59.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLLotAnalysis.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button60_Click(sender As Object, e As EventArgs) Handles HazelDev_Button60.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLProfitLossLotAllImpRBoss.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button61_Click(sender As Object, e As EventArgs) Handles HazelDev_Button61.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLAssort2019.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button62_Click(sender As Object, e As EventArgs) Handles HazelDev_Button62.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXOrdSummaryDueDate.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button63_Click(sender As Object, e As EventArgs) Handles HazelDev_Button63.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixMargin.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button64_Click(sender As Object, e As EventArgs) Handles HazelDev_Button64.Click
        Dim Instring As String

        Instring = UCase(InputBox("Enter Access Code", "Authorized Password"))
        If Instring = "REV1981" Then
            objForm = New frm_DCLReportViewer
            If strDBName = "DiaStock" Then
                mReportName = "crptDCLCostingFull.rpt"
            Else
                mReportName = "crptDCLCostingFullSales.rpt"
            End If
            strReportPath = PBReportPath & "GroupNiru\" & mReportName
            objForm.Show()
        End If
    End Sub

    Private Sub HazelDev_Button65_Click(sender As Object, e As EventArgs) Handles HazelDev_Button65.Click
        Dim Instring As String

        Instring = UCase(InputBox("Enter Access Code", "Authorized Password"))
        If Instring = "REV1981" Then
            objForm = New frm_DCLReportViewer
            If strDBName = "DiaStock" Then
                mReportName = "crptDCLCostingFinanceRgh.rpt"
            Else
                mReportName = "crptDCLCostingFinance.rpt"
            End If
            strReportPath = PBReportPath & "GroupNiru\" & mReportName
            objForm.Show()
        End If
    End Sub

    Private Sub HazelDev_Button66_Click(sender As Object, e As EventArgs) Handles HazelDev_Button66.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRghExportDetailsFinance.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button67_Click(sender As Object, e As EventArgs) Handles HazelDev_Button67.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLImportBreakdownAssortDate.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button68_Click(sender As Object, e As EventArgs) Handles HazelDev_Button68.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLStockFantacy2CompBoth.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button70_Click(sender As Object, e As EventArgs) Handles HazelDev_Button70.Click
        objForm = New frm_DCLReportViewer
        If strDBName = "DiaStock" Then
            mReportName = "crptDCLApcuIn.rpt"
        Else
            mReportName = "crptDCLApcuInSales.rpt"
        End If
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button71_Click(sender As Object, e As EventArgs) Handles HazelDev_Button71.Click
        objForm = New frm_DCLReportViewer
        If strDBName = "DiaStock" Then
            mReportName = "crptDCLApcuOut.rpt"
        Else
            mReportName = "crptDCLApcuOutSales.rpt"
        End If
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button72_Click(sender As Object, e As EventArgs) Handles HazelDev_Button72.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLParcelProLossBI.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button73_Click(sender As Object, e As EventArgs) Handles HazelDev_Button73.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRndExportCat.rpt"
        strReportPath = PBReportPath & "RoundsFullFlow\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button74_Click(sender As Object, e As EventArgs) Handles HazelDev_Button74.Click
        objForm = New frm_DCLReportViewer
        If strDBName = "DiaStock" Then
            mReportName = "crptDCLCostingFullLot.rpt"
        Else
            mReportName = "crptDCLCostingFullLotSales.rpt"
        End If
        strReportPath = PBReportPath & "GroupNiru\" & mReportName

        objForm.Show()
    End Sub

    Private Sub HazelDev_Button75_Click(sender As Object, e As EventArgs) Handles HazelDev_Button75.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRndProdSumBI.rpt"
        strReportPath = PBReportPath & "RoundsFullFlow\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button76_Click(sender As Object, e As EventArgs) Handles HazelDev_Button76.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRndExportCat2.rpt"
        strReportPath = PBReportPath & "RoundsFullFlow\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button77_Click(sender As Object, e As EventArgs) Handles HazelDev_Button77.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpExportCat.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button78_Click(sender As Object, e As EventArgs) Handles HazelDev_Button78.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpExportCat2.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button79_Click(sender As Object, e As EventArgs) Handles HazelDev_Button79.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLLotYield.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button80_Click(sender As Object, e As EventArgs) Handles HazelDev_Button80.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLCostingBI.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button81_Click(sender As Object, e As EventArgs) Handles HazelDev_Button81.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpExportCat4.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button82_Click(sender As Object, e As EventArgs) Handles HazelDev_Button82.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLPolishBoxInSales.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button83_Click(sender As Object, e As EventArgs) Handles HazelDev_Button83.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLPlanSum.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button84_Click(sender As Object, e As EventArgs) Handles HazelDev_Button84.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPCUStockChart.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub frm_DCLReportsNiru_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub HazelDev_Button85_Click(sender As Object, e As EventArgs) Handles HazelDev_Button85.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAssortList.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button86_Click(sender As Object, e As EventArgs) Handles HazelDev_Button86.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixExportOrigin2019.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button87_Click(sender As Object, e As EventArgs) Handles HazelDev_Button87.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixExportOrigin2019_Sum.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button88_Click(sender As Object, e As EventArgs) Handles HazelDev_Button88.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCL_ALL_Summary.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button89_Click(sender As Object, e As EventArgs) Handles HazelDev_Button89.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLImportPlanSum.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button90_Click(sender As Object, e As EventArgs) Handles HazelDev_Button90.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixExportOrigin2019_Sum2.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button91_Click(sender As Object, e As EventArgs) Handles HazelDev_Button91.Click
        objForm = New frm_DCLReportViewer
        mReportName = "CrptPCURevFinance.rpt"
        strReportPath = PBReportPath & "Precision\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button92_Click(sender As Object, e As EventArgs) Handles HazelDev_Button92.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixCostingMaxCost2.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button93_Click(sender As Object, e As EventArgs) Handles HazelDev_Button93.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLCostingFinanceOrders.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button94_Click(sender As Object, e As EventArgs) Handles HazelDev_Button94.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixCostingMaxCost3.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button95_Click(sender As Object, e As EventArgs) Handles HazelDev_Button95.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixCostingMaxCost4.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button96_Click(sender As Object, e As EventArgs) Handles HazelDev_Button96.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixCostingMaxCost5.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button97_Click(sender As Object, e As EventArgs) Handles HazelDev_Button97.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixExportOrigin2020.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button98_Click(sender As Object, e As EventArgs) Handles HazelDev_Button98.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixCostingMaxCost5Client.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button99_Click(sender As Object, e As EventArgs) Handles HazelDev_Button99.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixCloseStock.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button100_Click(sender As Object, e As EventArgs) Handles HazelDev_Button100.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLStock2020_2.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button101_Click(sender As Object, e As EventArgs) Handles HazelDev_Button101.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLProfitLossLotAllImpRCurLot.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button102_Click(sender As Object, e As EventArgs) Handles HazelDev_Button102.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLCloseStock.rpt"
        strReportPath = PBReportPath & "DiaSalesPolishBox\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button103_Click(sender As Object, e As EventArgs) Handles HazelDev_Button103.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLAARoughStock.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button104_Click(sender As Object, e As EventArgs) Handles HazelDev_Button104.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLAAPolishStock.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button105_Click(sender As Object, e As EventArgs) Handles HazelDev_Button105.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLAAFullStock.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button106_Click(sender As Object, e As EventArgs) Handles HazelDev_Button106.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixCostingMaxCost5ClientDetails.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button107_Click(sender As Object, e As EventArgs) Handles HazelDev_Button107.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPCUStockSize6.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button108_Click(sender As Object, e As EventArgs) Handles HazelDev_Button108.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLACImportsNotes.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button109_Click(sender As Object, e As EventArgs) Handles HazelDev_Button109.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLACApcuNotes.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button110_Click(sender As Object, e As EventArgs) Handles HazelDev_Button110.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLACPolBoxNotes.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button111_Click(sender As Object, e As EventArgs) Handles HazelDev_Button111.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLADFullStock.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button112_Click(sender As Object, e As EventArgs) Handles HazelDev_Button112.Click
        objForm2 = New frm_DCLReportViewer2
        mReportName = "PAY_AttendSummaryDeptOnly.rpt"
        mRecordSelectionFormula = "{PRT_AttendSummary.DDate} = Date('" & Format(Date.Now, "yyyy,MM,dd") & "')"
        strReportPath = "\\" & strServerName & "\Payroll\REPORTS1\" & mReportName
        objForm2.Show()
    End Sub

    Private Sub HazelDev_Button113_Click(sender As Object, e As EventArgs) Handles HazelDev_Button113.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixReturns2020Dept.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button114_Click(sender As Object, e As EventArgs) Handles HazelDev_Button114.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLSalesOrigin.rpt"
        strReportPath = PBReportPath & "DiaSalesPolishBox\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button116_Click(sender As Object, e As EventArgs) Handles HazelDev_Button116.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPCUFantacySchemaTraceF3Sum.rpt"
        strReportPath = PBReportPath & "Precision\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button115_Click(sender As Object, e As EventArgs) Handles HazelDev_Button115.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixFantacySchema.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button117_Click(sender As Object, e As EventArgs) Handles HazelDev_Button117.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLSalesOriginFantacy.rpt"
        strReportPath = PBReportPath & "DiaSalesPolishBox\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button118_Click(sender As Object, e As EventArgs) Handles HazelDev_Button118.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLAEImports.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button119_Click(sender As Object, e As EventArgs) Handles HazelDev_Button119.Click
        objForm = New frm_DCLReportViewer
        If strDBName = "DiaStock" Then
            mReportName = "crptDCLFantacySchemaParcelStock.rpt"
        Else
            mReportName = "crptDCLFantacySchemaParcel.rpt"
        End If
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button120_Click(sender As Object, e As EventArgs) Handles HazelDev_Button120.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLFantacySchemaFM.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button121_Click(sender As Object, e As EventArgs) Handles HazelDev_Button121.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPCUFantacySchemaNew.rpt"
        strReportPath = PBReportPath & "Precision\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button122_Click(sender As Object, e As EventArgs) Handles HazelDev_Button122.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLFantacySchemaParcelNFE.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button123_Click(sender As Object, e As EventArgs) Handles HazelDev_Button123.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLRndBi.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button124_Click(sender As Object, e As EventArgs) Handles HazelDev_Button124.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLStockValueFin.rpt"
        strReportPath = PBReportPath & "DiaSalesPolishBox\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button125_Click(sender As Object, e As EventArgs) Handles HazelDev_Button125.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLStock2020_2Pol.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button126_Click(sender As Object, e As EventArgs) Handles HazelDev_Button126.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLSalesFantacy.rpt"
        strReportPath = PBReportPath & "DiaSalesPolishBox\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button127_Click(sender As Object, e As EventArgs) Handles HazelDev_Button127.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLFantacySchemaApcuImport.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button128_Click(sender As Object, e As EventArgs) Handles HazelDev_Button128.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLLotList.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button129_Click(sender As Object, e As EventArgs) Handles HazelDev_Button129.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLSalesFantacySum.rpt"
        strReportPath = PBReportPath & "DiaSalesPolishBox\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button130_Click(sender As Object, e As EventArgs) Handles HazelDev_Button130.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLFantacySchemaPolImport.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button131_Click(sender As Object, e As EventArgs) Handles HazelDev_Button131.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLStockFantacy2CompBothFilter.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button132_Click(sender As Object, e As EventArgs) Handles HazelDev_Button132.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLFantacySchemaColombo.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button133_Click(sender As Object, e As EventArgs) Handles HazelDev_Button133.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLFantacySchemaColReAssort.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button134_Click(sender As Object, e As EventArgs) Handles HazelDev_Button134.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixExportOrigin2019_SumWOSL.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button136_Click(sender As Object, e As EventArgs) Handles HazelDev_Button136.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLACApcuNotesAll.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button135_Click(sender As Object, e As EventArgs) Handles HazelDev_Button135.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLACPolBoxNotesAll.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button137_Click(sender As Object, e As EventArgs) Handles HazelDev_Button137.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLFantacySchemaFMTrace.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button138_Click(sender As Object, e As EventArgs) Handles HazelDev_Button138.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPCUFantacySchemaTrace.rpt"
        strReportPath = PBReportPath & "Precision\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button139_Click(sender As Object, e As EventArgs) Handles HazelDev_Button139.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixExportOrigin2020WSL.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button140_Click(sender As Object, e As EventArgs) Handles HazelDev_Button140.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLFantacySchemaOrderImport.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button141_Click(sender As Object, e As EventArgs) Handles HazelDev_Button141.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixExpOrigin.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button143_Click(sender As Object, e As EventArgs) Handles HazelDev_Button143.Click
        objForm = New frm_DCLReportViewer
        If strDBName = "DiaStock" Then
            mReportName = "crptDCLImportsAll.rpt"
        Else
            mReportName = "crptDCLImportsAllSales.rpt"
        End If
        strReportPath = PBReportPath & "GroupNiru\" & mReportName

        objForm.Show()
    End Sub

    Private Sub HazelDev_Button142_Click(sender As Object, e As EventArgs) Handles HazelDev_Button142.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLFantacySchemaPolTrfImport.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button144_Click(sender As Object, e As EventArgs) Handles HazelDev_Button144.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLProfitLossLotAllImpR2021.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button145_Click(sender As Object, e As EventArgs) Handles HazelDev_Button145.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLFantacySchemaFMTraceT.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button146_Click(sender As Object, e As EventArgs) Handles HazelDev_Button146.Click
        objForm = New frm_DCLReportViewer
        If strDBName = "DiaStock" Then
            mReportName = "crptDCLFantacySchemaParcelStockF.rpt"
        Else
            mReportName = "crptDCLFantacySchemaParcel.rpt"
        End If
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button147_Click(sender As Object, e As EventArgs) Handles HazelDev_Button147.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLOriginTraceLot.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button148_Click(sender As Object, e As EventArgs) Handles HazelDev_Button148.Click
        objForm = New frm_DCLReportViewer
        If strDBName = "DiaStock" Then
            mReportName = "crptDCLCostingInvoiceComp.rpt"
        Else
            mReportName = "crptDCLCostingInvoiceCompSales.rpt"
        End If
        strReportPath = PBReportPath & "GroupNiru\" & mReportName

        objForm.Show()
    End Sub

    Private Sub HazelDev_Button149_Click(sender As Object, e As EventArgs) Handles HazelDev_Button149.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPCUFantacySchemaTraceF.rpt"
        strReportPath = PBReportPath & "Precision\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button150_Click(sender As Object, e As EventArgs) Handles HazelDev_Button150.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLLotImpExpOriginal.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button151_Click(sender As Object, e As EventArgs) Handles HazelDev_Button151.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLSalesFantacyNY.rpt"
        strReportPath = PBReportPath & "DiaSalesPolishBox\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button152_Click(sender As Object, e As EventArgs) Handles HazelDev_Button152.Click
        objForm = New frm_DCLReportViewer
        mReportName = "CrptMIXRevFinance.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button153_Click(sender As Object, e As EventArgs) Handles HazelDev_Button153.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXStkOrdWiseGrp2017SumPlanAuto.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button154_Click(sender As Object, e As EventArgs) Handles HazelDev_Button154.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRghImportMasterLot.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button155_Click(sender As Object, e As EventArgs) Handles HazelDev_Button155.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLFantacySchemaColReAssortNY.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button156_Click(sender As Object, e As EventArgs) Handles HazelDev_Button156.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLFantacySchemaRounds.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button157_Click(sender As Object, e As EventArgs) Handles HazelDev_Button157.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLSalesOriginFantacyDetails.rpt"
        strReportPath = PBReportPath & "DiaSalesPolishBox\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button158_Click(sender As Object, e As EventArgs) Handles HazelDev_Button158.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLFantacySchemaCN.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button159_Click(sender As Object, e As EventArgs) Handles HazelDev_Button159.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixCostingMaxCost6.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button160_Click(sender As Object, e As EventArgs) Handles HazelDev_Button160.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLBagBi.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button161_Click(sender As Object, e As EventArgs) Handles HazelDev_Button161.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPCUFantacySchemaTraceF2.rpt"
        strReportPath = PBReportPath & "Precision\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button162_Click(sender As Object, e As EventArgs) Handles HazelDev_Button162.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLFantacySchemaPolImportN.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button163_Click(sender As Object, e As EventArgs) Handles HazelDev_Button163.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPCUFantacySchemaTraceF3.rpt"
        strReportPath = PBReportPath & "Precision\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button69_Click(sender As Object, e As EventArgs) Handles HazelDev_Button69.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpInvSummaryFin.rpt"
        strReportPath = PBReportPath & "ExportFin\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button164_Click(sender As Object, e As EventArgs) Handles HazelDev_Button164.Click
        objForm = New frm_DCLReportViewer
        mReportName = "CrptMIXRevFinanceSumRefer.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button165_Click(sender As Object, e As EventArgs) Handles HazelDev_Button165.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRghExportDetailsParcel.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button166_Click(sender As Object, e As EventArgs) Handles HazelDev_Button166.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLFantacySchemaParcelDirect.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button167_Click(sender As Object, e As EventArgs) Handles HazelDev_Button167.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixFantacySchemaNew.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button168_Click(sender As Object, e As EventArgs) Handles HazelDev_Button168.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLProfitLossLotAllImpRMaster.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button169_Click(sender As Object, e As EventArgs) Handles HazelDev_Button169.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLFantacySchemaPolImportCost.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button170_Click(sender As Object, e As EventArgs) Handles HazelDev_Button170.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptBBCostingValue.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button171_Click(sender As Object, e As EventArgs) Handles HazelDev_Button171.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptBBCostingValueItem.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button172_Click(sender As Object, e As EventArgs) Handles HazelDev_Button172.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPCUStock.rpt"
        strReportPath = PBReportPath & "Mix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button173_Click(sender As Object, e As EventArgs) Handles HazelDev_Button173.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptKITFantacySchemaNew.rpt"
        strReportPath = PBReportPath & "KIT\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button174_Click(sender As Object, e As EventArgs) Handles HazelDev_Button174.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLProfitLossLotAllType.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button175_Click(sender As Object, e As EventArgs) Handles HazelDev_Button175.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLProfitLossLotAllImpRImportDate.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button176_Click(sender As Object, e As EventArgs) Handles HazelDev_Button176.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLLotAssortList.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button177_Click(sender As Object, e As EventArgs) Handles HazelDev_Button177.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLCostingLabourProcessRej.rpt"
        strReportPath = PBReportPath & "DiaSalesExportFin\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button178_Click(sender As Object, e As EventArgs) Handles HazelDev_Button178.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLStockBalPack.rpt"
        strReportPath = PBReportPath & "DiaSalesPolishBox\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button179_Click(sender As Object, e As EventArgs) Handles HazelDev_Button179.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixFantacySchemaNew2.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button180_Click(sender As Object, e As EventArgs) Handles HazelDev_Button180.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptApcuBoxPerc.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button181_Click(sender As Object, e As EventArgs) Handles HazelDev_Button181.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLBoxPerc.rpt"
        strReportPath = PBReportPath & "DiaSalesPolishBox\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button182_Click(sender As Object, e As EventArgs) Handles HazelDev_Button182.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLStockInPerc.rpt"
        strReportPath = PBReportPath & "DiaSalesPolishBox\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button183_Click(sender As Object, e As EventArgs) Handles HazelDev_Button183.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLSalesOriginFantacy2.rpt"
        strReportPath = PBReportPath & "DiaSalesPolishBox\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button184_Click(sender As Object, e As EventArgs) Handles HazelDev_Button184.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLSalesOriginFantacyLot.rpt"
        strReportPath = PBReportPath & "DiaSalesPolishBox\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button185_Click(sender As Object, e As EventArgs) Handles HazelDev_Button185.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLSalesOriginFantacyLot2.rpt"
        strReportPath = PBReportPath & "DiaSalesPolishBox\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button186_Click(sender As Object, e As EventArgs) Handles HazelDev_Button186.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLSalesFantacyHK.rpt"
        strReportPath = PBReportPath & "DiaSalesPolishBox\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button187_Click(sender As Object, e As EventArgs) Handles HazelDev_Button187.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRprPlanDetails2.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button188_Click(sender As Object, e As EventArgs) Handles HazelDev_Button188.Click
        'Instring = UCase(InputBox("Enter Year Month", "Authorized Password"))
        'If Instring = "111202307" Then
        '    Process.Start("\\192.168.2.6\Production Reports\111202307.pdf")
        'ElseIf Instring = "111202308" Then
        '    Process.Start("\\192.168.2.6\Production Reports\111202308.pdf")
        'Else
        '    objForm = New frm_DCLReportViewer
        '    mReportName = "crptMixExportOrigin2019_SumOriginalTrace.rpt"
        '    strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        '    objForm.Show()
        'End If

        objForm = New frm_DCLReportViewer
        mReportName = "crptMixExportOrigin2019_SumOriginalTrace.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button189_Click(sender As Object, e As EventArgs) Handles HazelDev_Button189.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLPacket4Plus.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button190_Click(sender As Object, e As EventArgs) Handles HazelDev_Button190.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLGrading4Plus.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button191_Click(sender As Object, e As EventArgs) Handles HazelDev_Button191.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPCUFantacySchemaTraceF_1.rpt"
        strReportPath = PBReportPath & "Precision\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button192_Click(sender As Object, e As EventArgs) Handles HazelDev_Button192.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAssortHistory.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button193_Click(sender As Object, e As EventArgs) Handles HazelDev_Button193.Click
        Dim Instring As String

        Instring = UCase(InputBox("Enter Access Code", "Authorized Password"))
        If Instring = "REV1981" Then
            objForm = New frm_DCLReportViewer
            If strDBName = "DiaStock" Then
                mReportName = "crptDCLCostingFull.rpt"
            Else
                mReportName = "crptDCLCostingFullSalesAssort.rpt"
            End If
            strReportPath = PBReportPath & "GroupNiru\" & mReportName
            objForm.Show()
        End If
    End Sub

    Private Sub HazelDev_Button194_Click(sender As Object, e As EventArgs) Handles HazelDev_Button194.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRndAssortList.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button195_Click(sender As Object, e As EventArgs) Handles HazelDev_Button195.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRndAssortListFancy.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button196_Click(sender As Object, e As EventArgs) Handles HazelDev_Button196.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLLotImpExp.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button197_Click(sender As Object, e As EventArgs) Handles HazelDev_Button197.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixExportOrigin2023.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button198_Click(sender As Object, e As EventArgs) Handles HazelDev_Button198.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixExportOrigin2023Order.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button199_Click(sender As Object, e As EventArgs) Handles HazelDev_Button199.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixLotNameSearch.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button200_Click(sender As Object, e As EventArgs) Handles HazelDev_Button200.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLFantacySchemaColReAssortLot.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button201_Click(sender As Object, e As EventArgs) Handles HazelDev_Button201.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixLotNameSearchDetails.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button202_Click(sender As Object, e As EventArgs) Handles HazelDev_Button202.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPackingList.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button203_Click(sender As Object, e As EventArgs) Handles HazelDev_Button203.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixExportOrigin2023OrderDate.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button204_Click(sender As Object, e As EventArgs) Handles HazelDev_Button204.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPacketTraceOGL.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button205_Click(sender As Object, e As EventArgs) Handles HazelDev_Button205.Click
        objForm = New frm_DCLReportViewer
        If strDBName = "DiaStock" Then
            mReportName = "crptDCLLog.rpt"
        Else
            mReportName = "crptDCLLogSales.rpt"
        End If
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button206_Click(sender As Object, e As EventArgs) Handles HazelDev_Button206.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixBoxTrace.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button207_Click(sender As Object, e As EventArgs) Handles HazelDev_Button207.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLSalesFantacy.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button208_Click(sender As Object, e As EventArgs) Handles HazelDev_Button208.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLSalesOriginFantacy.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button209_Click(sender As Object, e As EventArgs) Handles HazelDev_Button209.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLSalesFantacyHK.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button210_Click(sender As Object, e As EventArgs) Handles HazelDev_Button210.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLSalesFantacyNY.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button211_Click(sender As Object, e As EventArgs) Handles HazelDev_Button211.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLFantacySchemaRounds.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button212_Click(sender As Object, e As EventArgs) Handles HazelDev_Button212.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLFantacySchemaColReAssort.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button214_Click(sender As Object, e As EventArgs) Handles HazelDev_Button214.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPCUFantacySchemaTraceF3.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button213_Click(sender As Object, e As EventArgs) Handles HazelDev_Button213.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPCUFantacySchemaTraceF_1.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button215_Click(sender As Object, e As EventArgs) Handles HazelDev_Button215.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPCUFantacySchemaTraceF3Sum.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button216_Click(sender As Object, e As EventArgs) Handles HazelDev_Button216.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLFantacySchemaParcel.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button217_Click(sender As Object, e As EventArgs) Handles HazelDev_Button217.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixFantacySchemaNew2.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button218_Click(sender As Object, e As EventArgs) Handles HazelDev_Button218.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPCUFantacySchemaTraceF3Actual.rpt"
        strReportPath = PBReportPath & "Precision\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button219_Click(sender As Object, e As EventArgs) Handles HazelDev_Button219.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLProfitLossLotAllImpRAll.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button220_Click(sender As Object, e As EventArgs) Handles HazelDev_Button220.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixBoxTrace2.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button221_Click(sender As Object, e As EventArgs) Handles HazelDev_Button221.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLOriginBNSalesSum.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button223_Click(sender As Object, e As EventArgs) Handles HazelDev_Button223.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixExportOrigin2019_SumOriginalTraceCountry.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button224_Click(sender As Object, e As EventArgs) Handles HazelDev_Button224.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpAssortFind.rpt"
        strReportPath = PBReportPath & "Export\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button225_Click(sender As Object, e As EventArgs) Handles HazelDev_Button225.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixExportOrigin2019_SumOriginalTraceCts.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button226_Click(sender As Object, e As EventArgs) Handles HazelDev_Button226.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixExportOrigin2019_SumOriginalTraceCountryCts.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button227_Click(sender As Object, e As EventArgs) Handles HazelDev_Button227.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLFantacySchemaPolImportA.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button228_Click(sender As Object, e As EventArgs) Handles HazelDev_Button228.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLFantacySchemaPolImportANot.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button229_Click(sender As Object, e As EventArgs) Handles HazelDev_Button229.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLLotListA.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button230_Click(sender As Object, e As EventArgs) Handles HazelDev_Button230.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLLotListANot.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button231_Click(sender As Object, e As EventArgs) Handles HazelDev_Button231.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixFantacySchemaDirect.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button222_Click(sender As Object, e As EventArgs) Handles HazelDev_Button222.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLSalesNetSuite.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button232_Click(sender As Object, e As EventArgs) Handles HazelDev_Button232.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLSalesOriginNetSuite.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button233_Click(sender As Object, e As EventArgs) Handles HazelDev_Button233.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLSchemaColReAssortNetSuite.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button234_Click(sender As Object, e As EventArgs) Handles HazelDev_Button234.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLSchemaColReAssortOriginNetSuite.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button235_Click(sender As Object, e As EventArgs) Handles HazelDev_Button235.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPCUNetSuiteSchemaF3.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button236_Click(sender As Object, e As EventArgs) Handles HazelDev_Button236.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPCUNetSuiteTraceSchemaF3.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button237_Click(sender As Object, e As EventArgs) Handles HazelDev_Button237.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPCUNetSuiteSchemaF1.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button238_Click(sender As Object, e As EventArgs) Handles HazelDev_Button238.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPCUNetSuiteTraceSchemaF1.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button239_Click(sender As Object, e As EventArgs) Handles HazelDev_Button239.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptNSPurchases.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button240_Click(sender As Object, e As EventArgs) Handles HazelDev_Button240.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptNSSales.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button241_Click(sender As Object, e As EventArgs) Handles HazelDev_Button241.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLStock2020_2All.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button242_Click(sender As Object, e As EventArgs) Handles HazelDev_Button242.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixExportOrigin2019_SumOriginalTraceSupplierCts.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button243_Click(sender As Object, e As EventArgs) Handles HazelDev_Button243.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptNSExportSchema.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button244_Click(sender As Object, e As EventArgs) Handles HazelDev_Button244.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptNSTraceability.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button245_Click(sender As Object, e As EventArgs) Handles HazelDev_Button245.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptNSPurchases2.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button246_Click(sender As Object, e As EventArgs) Handles HazelDev_Button246.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixSchema178.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button247_Click(sender As Object, e As EventArgs) Handles HazelDev_Button247.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLSalesTrend.rpt"
        strReportPath = PBReportPath & "DiaSalesPolishBox\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button248_Click(sender As Object, e As EventArgs) Handles HazelDev_Button248.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingRoundsTrend.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button249_Click(sender As Object, e As EventArgs) Handles HazelDev_Button249.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListCOLMValue.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button250_Click(sender As Object, e As EventArgs) Handles HazelDev_Button250.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLPermanents.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button251_Click(sender As Object, e As EventArgs) Handles HazelDev_Button251.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListCOLM_JV.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button252_Click(sender As Object, e As EventArgs) Handles HazelDev_Button252.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLSalesOriginNetSuiteNew.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button253_Click(sender As Object, e As EventArgs) Handles HazelDev_Button253.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLAEImports2026.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button254_Click(sender As Object, e As EventArgs) Handles HazelDev_Button254.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPCUNetSuiteSchemaF3Order.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button255_Click(sender As Object, e As EventArgs) Handles HazelDev_Button255.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPCUNetSuiteTraceSchemaF1New.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button256_Click(sender As Object, e As EventArgs) Handles HazelDev_Button256.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPCUNetSuiteTraceSchemaF3New.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button257_Click(sender As Object, e As EventArgs) Handles HazelDev_Button257.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLSchemaColReAssortOriginNetSuiteNew.rpt"
        strReportPath = PBReportPath & "NetSuite\" & mReportName
        objForm.Show()
    End Sub
End Class