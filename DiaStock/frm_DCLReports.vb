
Imports System.Reflection

Public Class frm_DCLReports

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRghParcelAnalysis.rpt"
        strReportPath = PBReportPath & "Rgh\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixReturnsTargetGrp.rpt"
        strReportPath = PBReportPath & "Mix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRprPktDetailsByShapeDetails.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button4_Click(sender As Object, e As EventArgs) Handles HazelDev_Button4.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLParcelTurnAround2.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button5_Click(sender As Object, e As EventArgs) Handles HazelDev_Button5.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptBagStock.rpt"
        strReportPath = PBReportPath & "Baguettes\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button6_Click(sender As Object, e As EventArgs) Handles HazelDev_Button6.Click
        objForm = New frm_DCLReportViewer
        mReportName = "CrptBagGrpParYield.rpt"
        strReportPath = PBReportPath & "Baguettes\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button8_Click(sender As Object, e As EventArgs) Handles HazelDev_Button8.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPRStock.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button7_Click(sender As Object, e As EventArgs) Handles HazelDev_Button7.Click
        objForm = New frm_DCLReportViewer
        mReportName = "CrptPRGrpParYield.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button10_Click(sender As Object, e As EventArgs) Handles HazelDev_Button10.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRndStock_Preperation2.rpt"
        strReportPath = PBReportPath & "RoundsFullFlow\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button9_Click(sender As Object, e As EventArgs) Handles HazelDev_Button9.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRndStock_Polishing2.rpt"
        strReportPath = PBReportPath & "RoundsFullFlow\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button11_Click(sender As Object, e As EventArgs) Handles HazelDev_Button11.Click
        objForm = New frm_DCLReportViewer
        mReportName = "CrptRndParYield.rpt"
        strReportPath = PBReportPath & "RoundsFullFlow\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button12_Click(sender As Object, e As EventArgs) Handles HazelDev_Button12.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRndStockinhand.rpt"
        strReportPath = PBReportPath & "RoundsFullFlow\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button16_Click(sender As Object, e As EventArgs) Handles HazelDev_Button16.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExtStock_Preperation.rpt"
        strReportPath = PBReportPath & "Ext\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button15_Click(sender As Object, e As EventArgs) Handles HazelDev_Button15.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExtStock_Polishing.rpt"
        strReportPath = PBReportPath & "Ext\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button14_Click(sender As Object, e As EventArgs) Handles HazelDev_Button14.Click
        objForm = New frm_DCLReportViewer
        mReportName = "CrptExtParYield.rpt"
        strReportPath = PBReportPath & "Ext\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button13_Click(sender As Object, e As EventArgs) Handles HazelDev_Button13.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExtStockinhand.rpt"
        strReportPath = PBReportPath & "Ext\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button17_Click(sender As Object, e As EventArgs) Handles HazelDev_Button17.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPCUStkOrdWiseGrpNone.rpt"
        strReportPath = PBReportPath & "Precision\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button18_Click(sender As Object, e As EventArgs) Handles HazelDev_Button18.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptOrdSummary.rpt"
        strReportPath = PBReportPath & "Precision\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button20_Click(sender As Object, e As EventArgs) Handles HazelDev_Button20.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPCUStock.rpt"
        strReportPath = PBReportPath & "Mix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button19_Click(sender As Object, e As EventArgs) Handles HazelDev_Button19.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXOrdSummary.rpt"
        strReportPath = PBReportPath & "Mix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button21_Click(sender As Object, e As EventArgs) Handles HazelDev_Button21.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLRghStoneDays.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button22_Click(sender As Object, e As EventArgs) Handles HazelDev_Button22.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLGradingStoneDays.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button23_Click(sender As Object, e As EventArgs) Handles HazelDev_Button23.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLStoneDaysEM.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button24_Click(sender As Object, e As EventArgs) Handles HazelDev_Button24.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLParcelTurnAround3.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button25_Click(sender As Object, e As EventArgs) Handles HazelDev_Button25.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixShipmentPlanSummary.rpt"
        strReportPath = PBReportPath & "Mix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button26_Click(sender As Object, e As EventArgs) Handles HazelDev_Button26.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixReturnsTargetAllDaysAnyDay.rpt"
        strReportPath = PBReportPath & "Mix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button27_Click(sender As Object, e As EventArgs) Handles HazelDev_Button27.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRprPktDetailsByShapeDetailsAssort.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button28_Click(sender As Object, e As EventArgs) Handles HazelDev_Button28.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXOrderPlanSummary.rpt"
        strReportPath = PBReportPath & "Mix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button29_Click(sender As Object, e As EventArgs) Handles HazelDev_Button29.Click
        Instring = InputBox("Please enter the Assortment", Me.Text)
        If Len(Instring) > 0 Then
            AdoCN.Execute("DELETE FROM tblDCLAssort")
            AdoCN.Execute("INSERT INTO tblDCLAssort(Assortment) VALUES('" & Instring & "')")

            objForm = New frm_DCLReportViewer
            mReportName = "crptRprPktDetailsByShapeDetailsAssort.rpt"
            strReportPath = PBReportPath & "Rpr\" & mReportName
            objForm.Show()
        End If
    End Sub

    Private Sub HazelDev_Button30_Click(sender As Object, e As EventArgs) Handles HazelDev_Button30.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLImportBreakdownAssort.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button31_Click(sender As Object, e As EventArgs) Handles HazelDev_Button31.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExtPacketFinishGrp.rpt"
        strReportPath = PBReportPath & "Ext\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button32_Click(sender As Object, e As EventArgs) Handles HazelDev_Button32.Click
        objForm = New frm_DCLReportViewer
        mReportName = "TRA_JourneyRoute.rpt"
        strReportPath = "\\" & strServerName & "\Payroll\REPORTS1\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button33_Click(sender As Object, e As EventArgs) Handles HazelDev_Button33.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLLotShipment.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button34_Click(sender As Object, e As EventArgs) Handles HazelDev_Button34.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLAJProd.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button35_Click(sender As Object, e As EventArgs) Handles HazelDev_Button35.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLRghStoneDaysLot.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button36_Click(sender As Object, e As EventArgs) Handles HazelDev_Button36.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLStoneDaysLot.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button37_Click(sender As Object, e As EventArgs) Handles HazelDev_Button37.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLGradingStoneDaysLot.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button38_Click(sender As Object, e As EventArgs) Handles HazelDev_Button38.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLStoneCtsDaysLot.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button39_Click(sender As Object, e As EventArgs) Handles HazelDev_Button39.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLAssortImpExp.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button40_Click(sender As Object, e As EventArgs) Handles HazelDev_Button40.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPackingList2019Schema.rpt"
        strReportPath = PBReportPath & "NiruStock\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button41_Click(sender As Object, e As EventArgs) Handles HazelDev_Button41.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPackingList2019Schema2.rpt"
        strReportPath = PBReportPath & "NiruStock\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button42_Click(sender As Object, e As EventArgs) Handles HazelDev_Button42.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLParcelTurnAround.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button43_Click(sender As Object, e As EventArgs) Handles HazelDev_Button43.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLLotShipment5.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button44_Click(sender As Object, e As EventArgs) Handles HazelDev_Button44.Click
        Dim col As New Generic.List(Of Type)
        Dim list() As System.Reflection.Assembly = AppDomain.CurrentDomain.GetAssemblies()

        For Each asm As Reflection.Assembly In list
            Dim types() As Type = asm.GetTypes()
            For Each t As Type In types
                If t.BaseType Is GetType(Windows.Forms.Form) Then
                    'col.Add(t)
                    flxDetails.Rows.Add(t.Name)
                End If
            Next
        Next
    End Sub

    Private Sub HazelDev_Button45_Click(sender As Object, e As EventArgs) Handles HazelDev_Button45.Click
        'ExportToExcel(flxDetails)
        'Dim strForm As Form
        'Dim strScreenName As String

        'rsComSql = New ADODB.Recordset
        'rsComSql.Open("SELECT * FROM tblSYS_UserScreens WHERE ScreenName = '' ORDER BY ID", AdoCN, 1, 1)
        'If rsComSql.RecordCount Then
        '    rsComSql.MoveFirst()
        '    While Not rsComSql.EOF
        '        strScreenName = rsComSql.Fields("FormName").Value
        '        Dim formName As String = strScreenName

        '        formName = [Assembly].GetEntryAssembly.GetName.Name & "." & formName
        '        strForm = DirectCast([Assembly].GetEntryAssembly.CreateInstance(formName), Form)

        '        strScreenName = strForm.Text
        '        AdoCN.Execute("UPDATE tblSYS_UserScreens SET ScreenName = '" & Replace(strScreenName, "/", " ") & "' WHERE ID = " & rsComSql.Fields("ID").Value & "")
        '        rsComSql.MoveNext()
        '    End While
        'End If
        'rsComSql = Nothing

    End Sub

    Private Sub frm_DCLReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub HazelDev_Button46_Click(sender As Object, e As EventArgs) Handles HazelDev_Button46.Click
        'Dim Generator As New MessagingToolkit.Barcode.BarcodeEncoder
        'Generator.BackColor = Color.White
        'Generator.LabelFont = New Font("Arial", 7, FontStyle.Regular)
        'Generator.IncludeLabel = True
        'Generator.CustomLabel = txttext.Text
        'Try
        '    pbQrCodeImg.Image = New Bitmap(Generator.Encode(MessagingToolkit.Barcode.BarcodeFormat.QRCode, txttext.Text))
        'Catch ex As Exception
        '    pbQrCodeImg.Image = Nothing
        'End Try
    End Sub

    Private Sub HazelDev_Button47_Click(sender As Object, e As EventArgs) Handles HazelDev_Button47.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAssortSummary.rpt"
        strReportPath = PBReportPath & "NiruStock\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button48_Click(sender As Object, e As EventArgs) Handles HazelDev_Button48.Click
        OpenFileDialog1.Filter = "PDF |*.pdf"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            'AxAcroPDF1.src = OpenFileDialog1.FileName
            Process.Start(OpenFileDialog1.FileName)
        End If

    End Sub

    Private Sub HazelDev_Button49_Click(sender As Object, e As EventArgs) Handles HazelDev_Button49.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLStockValueAll.rpt"
        strReportPath = PBReportPath & "DiaSalesPolishBox\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button50_Click(sender As Object, e As EventArgs) Handles HazelDev_Button50.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAssortStock.rpt"
        strReportPath = PBReportPath & "NiruStock\" & mReportName
        objForm.Show()
    End Sub
End Class