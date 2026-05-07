

Public Class frm_RndReportsLB

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRprStock.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRprEmpFinish.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRprStockinhand.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button4_Click(sender As Object, e As EventArgs) Handles HazelDev_Button4.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRprIssuesBrut.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button5_Click(sender As Object, e As EventArgs) Handles HazelDev_Button5.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRprEmpProd.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button6_Click(sender As Object, e As EventArgs) Handles HazelDev_Button6.Click
        objForm = New frm_DCLReportViewer
        mReportName = "CrptRprPktInfo.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button7_Click(sender As Object, e As EventArgs) Handles HazelDev_Button7.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptSYS_Stock-RPR2019LB.rpt"
        strReportPath = PBReportPath & "Rough\SYSSTOCK\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button8_Click(sender As Object, e As EventArgs) Handles HazelDev_Button8.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRprStockinhand4.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button9_Click(sender As Object, e As EventArgs) Handles HazelDev_Button9.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRprBrutingCompare.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button10_Click(sender As Object, e As EventArgs) Handles HazelDev_Button10.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRprTransitParcels.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button11_Click(sender As Object, e As EventArgs) Handles HazelDev_Button11.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRndPacketNotIssue.rpt"
        strReportPath = PBReportPath & "RoundsFullFlow\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button12_Click(sender As Object, e As EventArgs) Handles HazelDev_Button12.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptProdAdminIss_DateWise.rpt"
        strReportPath = PBReportPath & "RoundsFullFlow\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button13_Click(sender As Object, e As EventArgs) Handles HazelDev_Button13.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRndStock_PolishingCompact.rpt"
        strReportPath = PBReportPath & "RoundsFullFlow\" & mReportName
        objForm.Show()
    End Sub

    Private Sub frm_RndReportsLB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub HazelDev_Button14_Click(sender As Object, e As EventArgs) Handles HazelDev_Button14.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRprStock2.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub
End Class