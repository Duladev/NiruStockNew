
Public Class frm_GRDReports3
    Dim strFolderPath As String

    Private Sub frm_ExpReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        If strDBName = "DiaStock" Then
            strFolderPath = "Grading\"
        Else
            strFolderPath = "DiaSalesGrading\"
        End If
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs)
        objForm = New frm_DCLReportViewer
        mReportName = "crptGrading_TransferDetails.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs)
        objForm = New frm_DCLReportViewer
        mReportName = "crptCheckingReturnsSummary.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListMainStickerRough.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button11_Click(sender As Object, e As EventArgs)
        objForm = New frm_DCLReportViewer
        mReportName = "crptCheckingReturnsGrp.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button12_Click(sender As Object, e As EventArgs) Handles HazelDev_Button12.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRghRejectsPackingList.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button4_Click(sender As Object, e As EventArgs) Handles HazelDev_Button4.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRghSalesPackingList.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button5_Click(sender As Object, e As EventArgs)
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingLabelsNotMixBundle.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button8_Click(sender As Object, e As EventArgs)
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpSizeTransferListMix.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button13_Click(sender As Object, e As EventArgs)
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpSizePackingLabels.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button14_Click(sender As Object, e As EventArgs) Handles HazelDev_Button14.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpRejPackingList.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button6_Click(sender As Object, e As EventArgs)
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingParcelSummary.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button7_Click(sender As Object, e As EventArgs) Handles HazelDev_Button7.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpPackingListStickerRej.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button10_Click(sender As Object, e As EventArgs) Handles HazelDev_Button10.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRghContractPackingList.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button8_Click_1(sender As Object, e As EventArgs) Handles HazelDev_Button8.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListBundle.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button9_Click(sender As Object, e As EventArgs) Handles HazelDev_Button9.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpRejPackingLables.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button1_Click_1(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingLabelsNotMixBundle.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button13_Click_1(sender As Object, e As EventArgs) Handles HazelDev_Button13.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingLabelsPCU.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button17_Click(sender As Object, e As EventArgs) Handles HazelDev_Button17.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingLabelsPCUSum.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button18_Click(sender As Object, e As EventArgs) Handles HazelDev_Button18.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingStickersPCU_N.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button5_Click_1(sender As Object, e As EventArgs) Handles HazelDev_Button5.Click
        objForm = New frm_DCLReportViewer
        mReportName = "GrdPKTSLEEVE_Full.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button2_Click_1(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingLabelsPCU_N.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button11_Click_1(sender As Object, e As EventArgs) Handles HazelDev_Button11.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListMainStickerPCU_N.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button6_Click_1(sender As Object, e As EventArgs) Handles HazelDev_Button6.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListPCU_NSum.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button16_Click(sender As Object, e As EventArgs) Handles HazelDev_Button16.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListPCU_NSummary.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button28_Click(sender As Object, e As EventArgs) Handles HazelDev_Button28.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListPCU_NSales.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button29_Click(sender As Object, e As EventArgs) Handles HazelDev_Button29.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingStickersPCU_NSum.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button15_Click(sender As Object, e As EventArgs) Handles HazelDev_Button15.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListPCU_N.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button23_Click(sender As Object, e As EventArgs) Handles HazelDev_Button23.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListSales.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button27_Click(sender As Object, e As EventArgs) Handles HazelDev_Button27.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListNotMix2.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button19_Click(sender As Object, e As EventArgs) Handles HazelDev_Button19.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingList.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button21_Click(sender As Object, e As EventArgs) Handles HazelDev_Button21.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPackingLabels.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button25_Click(sender As Object, e As EventArgs) Handles HazelDev_Button25.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPackingLabels2.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button26_Click(sender As Object, e As EventArgs) Handles HazelDev_Button26.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingLabelsNotMix2.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button20_Click(sender As Object, e As EventArgs) Handles HazelDev_Button20.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingLabelsLam.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button22_Click(sender As Object, e As EventArgs) Handles HazelDev_Button22.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListMixLam.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button24_Click(sender As Object, e As EventArgs) Handles HazelDev_Button24.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListMainStickerSales.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button30_Click(sender As Object, e As EventArgs) Handles HazelDev_Button30.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingLabelsSales.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button31_Click(sender As Object, e As EventArgs) Handles HazelDev_Button31.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListMainSticker.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button32_Click(sender As Object, e As EventArgs) Handles HazelDev_Button32.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListDirect.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button33_Click(sender As Object, e As EventArgs) Handles HazelDev_Button33.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListMainStickerMix.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button34_Click(sender As Object, e As EventArgs) Handles HazelDev_Button34.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingLabelsNotMixPolish.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button35_Click(sender As Object, e As EventArgs) Handles HazelDev_Button35.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingLabelsNotMix.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button36_Click(sender As Object, e As EventArgs) Handles HazelDev_Button36.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListMainStickerSalesSub.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button37_Click(sender As Object, e As EventArgs) Handles HazelDev_Button37.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingMainStickerSub.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button38_Click(sender As Object, e As EventArgs) Handles HazelDev_Button38.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListMix.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button39_Click(sender As Object, e As EventArgs) Handles HazelDev_Button39.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListSalesRnd.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button40_Click(sender As Object, e As EventArgs) Handles HazelDev_Button40.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingLabelsSalesRnd.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button41_Click(sender As Object, e As EventArgs) Handles HazelDev_Button41.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingMainStickerSubRnd.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button42_Click(sender As Object, e As EventArgs) Handles HazelDev_Button42.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingFMSticker.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button47_Click(sender As Object, e As EventArgs) Handles HazelDev_Button47.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLRndBi.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button43_Click(sender As Object, e As EventArgs) Handles HazelDev_Button43.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListPCU_NDetails.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button44_Click(sender As Object, e As EventArgs) Handles HazelDev_Button44.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListMainStickerPCU_N151.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button45_Click(sender As Object, e As EventArgs) Handles HazelDev_Button45.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPCUPackingList.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button46_Click(sender As Object, e As EventArgs) Handles HazelDev_Button46.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListSalesRndNY.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button48_Click(sender As Object, e As EventArgs) Handles HazelDev_Button48.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingLabelsSalesRndNY.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button49_Click(sender As Object, e As EventArgs) Handles HazelDev_Button49.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptProfitLossSum2021_2.rpt"
        strReportPath = PBReportPath & "NiruStock\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button50_Click(sender As Object, e As EventArgs) Handles HazelDev_Button50.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPCUPackingListDetails.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button51_Click(sender As Object, e As EventArgs) Handles HazelDev_Button51.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListSales3.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button52_Click(sender As Object, e As EventArgs) Handles HazelDev_Button52.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpRejPackingListDetails.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button53_Click(sender As Object, e As EventArgs) Handles HazelDev_Button53.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpRejPackingListSum.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button55_Click(sender As Object, e As EventArgs) Handles HazelDev_Button55.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListSalesRndHK.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button54_Click(sender As Object, e As EventArgs) Handles HazelDev_Button54.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingLabelsSalesRndHK.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button56_Click(sender As Object, e As EventArgs) Handles HazelDev_Button56.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListSales4.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button57_Click(sender As Object, e As EventArgs) Handles HazelDev_Button57.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLPolishBoxInSales2.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button58_Click(sender As Object, e As EventArgs) Handles HazelDev_Button58.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListSalesRndSum.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub
End Class