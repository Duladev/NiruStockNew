Public Class frm_PrReports

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPRProdFinish.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPRProductionInfo.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        objForm = New frm_DCLReportViewer
        mReportName = "CrptPRGrpParYield.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button4_Click(sender As Object, e As EventArgs) Handles HazelDev_Button4.Click
        objForm = New frm_DCLReportViewer
        mReportName = "CrptPRGradingFinish.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button5_Click(sender As Object, e As EventArgs) Handles HazelDev_Button5.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPRPktInfoDetailsByDate.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button6_Click(sender As Object, e As EventArgs) Handles HazelDev_Button6.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPRStock.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button8_Click(sender As Object, e As EventArgs) Handles HazelDev_Button8.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPRPktInfo.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button7_Click(sender As Object, e As EventArgs) Handles HazelDev_Button7.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPREmpProdSummary.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button9_Click(sender As Object, e As EventArgs) Handles HazelDev_Button9.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPRModIss.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button10_Click(sender As Object, e As EventArgs) Handles HazelDev_Button10.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPRStock_Summary.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button11_Click(sender As Object, e As EventArgs) Handles HazelDev_Button11.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPRStockinhand.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button12_Click(sender As Object, e As EventArgs) Handles HazelDev_Button12.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPRProdAdminIss_DateWise.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button13_Click(sender As Object, e As EventArgs) Handles HazelDev_Button13.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPRTransitParcels.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub frm_PrReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub HazelDev_Button14_Click(sender As Object, e As EventArgs) Handles HazelDev_Button14.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPrHourlyProd.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button15_Click(sender As Object, e As EventArgs) Handles HazelDev_Button15.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPREmpProdSummary_Grade.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button16_Click(sender As Object, e As EventArgs) Handles HazelDev_Button16.Click
        objForm = New frm_DCLReportViewer
        mReportName = "CrptPRRevFinance.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button17_Click(sender As Object, e As EventArgs) Handles HazelDev_Button17.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPRStockCompact.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button18_Click(sender As Object, e As EventArgs) Handles HazelDev_Button18.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPRParcelSummary2.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button19_Click(sender As Object, e As EventArgs) Handles HazelDev_Button19.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPRReturnsCts.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button20_Click(sender As Object, e As EventArgs) Handles HazelDev_Button20.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPREmpProd.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button21_Click(sender As Object, e As EventArgs) Handles HazelDev_Button21.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPRStockOrder.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button22_Click(sender As Object, e As EventArgs) Handles HazelDev_Button22.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPRPCUTaken3.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button23_Click(sender As Object, e As EventArgs) Handles HazelDev_Button23.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPRCumYield.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button24_Click(sender As Object, e As EventArgs) Handles HazelDev_Button24.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptFanProdSumPr.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button25_Click(sender As Object, e As EventArgs) Handles HazelDev_Button25.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptFanProdSumPrDetails.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button26_Click(sender As Object, e As EventArgs) Handles HazelDev_Button26.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPRStockinhandDelay.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button27_Click(sender As Object, e As EventArgs) Handles HazelDev_Button27.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPRIncentiveUnits.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub
End Class