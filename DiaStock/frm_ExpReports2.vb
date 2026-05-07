
Public Class frm_ExpReports2
    Dim strFolderPath As String

    Private Sub frm_ExpReports2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        If strDBName = "DiaStock" Then
            strFolderPath = "Export\"
        Else
            strFolderPath = "DiaSalesExport\"
        End If
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptTrnParcel_Value.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpParcelSummary.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExp_SizingStock.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button11_Click(sender As Object, e As EventArgs) Handles HazelDev_Button11.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpProductionInfo.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button12_Click(sender As Object, e As EventArgs) Handles HazelDev_Button12.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpReExportsByPar.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button4_Click(sender As Object, e As EventArgs) Handles HazelDev_Button4.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpEmpProdSizeSummary.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button5_Click(sender As Object, e As EventArgs) Handles HazelDev_Button5.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpProductionInfoSize.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button8_Click(sender As Object, e As EventArgs) Handles HazelDev_Button8.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptTrnParcel_Value_ParWiseOK.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button13_Click(sender As Object, e As EventArgs) Handles HazelDev_Button13.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExp_SizingStockMix.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button14_Click(sender As Object, e As EventArgs) Handles HazelDev_Button14.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpSizingFinishStock.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button6_Click(sender As Object, e As EventArgs) Handles HazelDev_Button6.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpOrderPlanMFG_SelectSum.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button7_Click(sender As Object, e As EventArgs) Handles HazelDev_Button7.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpSizingFinishStockAssort.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button10_Click(sender As Object, e As EventArgs) Handles HazelDev_Button10.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpPktInfo.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button9_Click(sender As Object, e As EventArgs) Handles HazelDev_Button9.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpPktInfoSizing.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button15_Click(sender As Object, e As EventArgs) Handles HazelDev_Button15.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptTrnParcel_Value_All.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button16_Click(sender As Object, e As EventArgs) Handles HazelDev_Button16.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpEmpProdSummary.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button17_Click(sender As Object, e As EventArgs) Handles HazelDev_Button17.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptTrnParcel_Value_Trf.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button18_Click(sender As Object, e As EventArgs) Handles HazelDev_Button18.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpAMS2.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button19_Click(sender As Object, e As EventArgs) Handles HazelDev_Button19.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptTrnParcel_Value_Trf_All.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button20_Click(sender As Object, e As EventArgs) Handles HazelDev_Button20.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExtPcuStock.rpt"
        strReportPath = PBReportPath & "Ext\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button21_Click(sender As Object, e As EventArgs) Handles HazelDev_Button21.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptBagPcuStock.rpt"
        strReportPath = PBReportPath & "Baguettes\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button22_Click(sender As Object, e As EventArgs) Handles HazelDev_Button22.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptBagPCUTaken4.rpt"
        strReportPath = PBReportPath & "Baguettes\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button23_Click(sender As Object, e As EventArgs) Handles HazelDev_Button23.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingAASummaryParcel.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button24_Click(sender As Object, e As EventArgs) Handles HazelDev_Button24.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpStockDiff.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button25_Click(sender As Object, e As EventArgs) Handles HazelDev_Button25.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpOrderPlanMFG_SelectSumActual.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button26_Click(sender As Object, e As EventArgs) Handles HazelDev_Button26.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptTrnParcel_Value_ParWiseOKSum.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button27_Click(sender As Object, e As EventArgs) Handles HazelDev_Button27.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpEmpProdDate.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button28_Click(sender As Object, e As EventArgs) Handles HazelDev_Button28.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpEmpProdSizeDate.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button29_Click(sender As Object, e As EventArgs) Handles HazelDev_Button29.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpInvValue.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button30_Click(sender As Object, e As EventArgs) Handles HazelDev_Button30.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExp_SizingStock2.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button31_Click(sender As Object, e As EventArgs) Handles HazelDev_Button31.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpStockSum.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button32_Click(sender As Object, e As EventArgs) Handles HazelDev_Button32.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpImportDetails.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button33_Click(sender As Object, e As EventArgs) Handles HazelDev_Button33.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpOrderPlanMFG_SelectSumDetails.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button34_Click(sender As Object, e As EventArgs) Handles HazelDev_Button34.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptTrnParcel_Value_ParWiseOK100.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button35_Click(sender As Object, e As EventArgs) Handles HazelDev_Button35.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingAASummaryDate.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button36_Click(sender As Object, e As EventArgs) Handles HazelDev_Button36.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAssortListName.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button37_Click(sender As Object, e As EventArgs) Handles HazelDev_Button37.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExp_SizingStockSum.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button38_Click(sender As Object, e As EventArgs) Handles HazelDev_Button38.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExp_SizingStockSumNew.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button39_Click(sender As Object, e As EventArgs) Handles HazelDev_Button39.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpSizingPendingBag.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button40_Click(sender As Object, e As EventArgs) Handles HazelDev_Button40.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpSizingPendingExt2.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button41_Click(sender As Object, e As EventArgs) Handles HazelDev_Button41.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpParcelCompare.rpt"
        strReportPath = PBReportPath & "Export\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button42_Click(sender As Object, e As EventArgs) Handles HazelDev_Button42.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpSizingBalance.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button43_Click(sender As Object, e As EventArgs) Handles HazelDev_Button43.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpReExportPack.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button44_Click(sender As Object, e As EventArgs) Handles HazelDev_Button44.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpFirstSizingStock.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button45_Click(sender As Object, e As EventArgs) Handles HazelDev_Button45.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpReExportPlan.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button46_Click(sender As Object, e As EventArgs) Handles HazelDev_Button46.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpSizingPlan.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button47_Click(sender As Object, e As EventArgs) Handles HazelDev_Button47.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGrading_TrfOpening.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button48_Click(sender As Object, e As EventArgs) Handles HazelDev_Button48.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpSortingPendingExt.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button49_Click(sender As Object, e As EventArgs) Handles HazelDev_Button49.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpSizingFinishStockAssortDetails.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub
End Class