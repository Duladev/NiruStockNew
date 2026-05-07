
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDReports2
    Dim strFolderPath As String
    Dim intCounter As Long

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
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPktInfoColor.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button11_Click(sender As Object, e As EventArgs)
        objForm = New frm_DCLReportViewer
        mReportName = "crptCheckingReturnsGrp.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button12_Click(sender As Object, e As EventArgs) Handles HazelDev_Button12.Click
        Instring = UCase(InputBox("Enter the Parcel No.", "Packet Information"))
        If Instring <> "" Then
            If Len(Instring) = 6 Then
                Cursor = Cursors.WaitCursor
                Insert_PacketInfo(Trim(Mid(Instring, 1, 6)))
                Cursor = Cursors.Default

                objForm = New frm_DCLReportViewer
                mReportName = "crptGrading_PacketInfo.rpt"
                strReportPath = PBReportPath & "Grading\" & mReportName
                objForm.Show()
            Else
                MsgBox("Invalid Parcel", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        End If
    End Sub

    Private Sub HazelDev_Button4_Click(sender As Object, e As EventArgs) Handles HazelDev_Button4.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGrading_StockBalanceFULL.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button5_Click(sender As Object, e As EventArgs)
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingLabelsNotMixBundle.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button8_Click(sender As Object, e As EventArgs)
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpSizeTransferListMix.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button13_Click(sender As Object, e As EventArgs)
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpSizePackingLabels.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button14_Click(sender As Object, e As EventArgs) Handles HazelDev_Button14.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPackingListLAM.rpt"
        strReportPath = PBReportPath & "Export\" & mReportName
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
        mReportName = "crptCheckingStk.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button10_Click(sender As Object, e As EventArgs) Handles HazelDev_Button10.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptBoilingIssue_Rcpt.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button8_Click_1(sender As Object, e As EventArgs) Handles HazelDev_Button8.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptCheckingReturnsDetails.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button9_Click(sender As Object, e As EventArgs) Handles HazelDev_Button9.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPCUSummary.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button1_Click_1(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptBoilingStk.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button2_Click_1(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptCheckingIssue_Rcpt.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button5_Click_1(sender As Object, e As EventArgs) Handles HazelDev_Button5.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMainParcelSummary.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button6_Click_1(sender As Object, e As EventArgs) Handles HazelDev_Button6.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGrading_StockTransfer.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button11_Click_1(sender As Object, e As EventArgs) Handles HazelDev_Button11.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPackingList.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()

    End Sub

    Private Sub HazelDev_Button13_Click_1(sender As Object, e As EventArgs) Handles HazelDev_Button13.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPackingList_Detail.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button15_Click(sender As Object, e As EventArgs) Handles HazelDev_Button15.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGrading_EmpProduction.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button16_Click(sender As Object, e As EventArgs) Handles HazelDev_Button16.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPktInfoSize.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button17_Click(sender As Object, e As EventArgs) Handles HazelDev_Button17.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingOrderStock.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button18_Click(sender As Object, e As EventArgs) Handles HazelDev_Button18.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingRghRejects.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()

    End Sub

    Private Sub HazelDev_Button19_Click(sender As Object, e As EventArgs) Handles HazelDev_Button19.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOMStockValue.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button20_Click(sender As Object, e As EventArgs) Handles HazelDev_Button20.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingRep_PktLableDate.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button21_Click(sender As Object, e As EventArgs) Handles HazelDev_Button21.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGrading_SizingFinishBalance.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button22_Click(sender As Object, e As EventArgs) Handles HazelDev_Button22.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRepairIssueDetails.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub Insert_PacketInfo(ByVal strParNo As String)

        AdoCN.Execute("DELETE FROM tblGrading_PacketInfo")

        'Boiling Issues
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblGrading_BoilingIssues WHERE ParNo LIKE '" & strParNo & "' + '%' ORDER BY ParNo,PktNo", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then

            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = rsComSql_1.RecordCount
            intCounter = 0

            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                intCounter = intCounter + 1

                AdoCN.Execute("INSERT INTO tblGrading_PacketInfo(Department,ParNo,PktNo,Sec,IssPcs,IssCts,EmpNo,Remarks) " & _
                              "VALUES('" & rsComSql_1.Fields("Department").Value & "','" & rsComSql_1.Fields("ParNo").Value & "','" & rsComSql_1.Fields("PktNo").Value & "',0," & rsComSql_1.Fields("IssPcs").Value & "," & rsComSql_1.Fields("IssCts").Value & ",'" & rsComSql_1.Fields("EmpNo").Value & "','" & rsComSql_1.Fields("Remarks").Value & "')")

                'Grading Transfer Accepts
                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT SUM(Trf_Pcs) AS TotPcs FROM tblGradingTrf WHERE (ParcelNo = '" & rsComSql_1.Fields("ParNo").Value & "') AND (Status = 1)", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    If Not IsDBNull(rsComSql_2.Fields("TotPcs").Value) Then
                        AdoCN.Execute("UPDATE tblGrading_PacketInfo SET ImpPcs = " & rsComSql_2.Fields("TotPcs").Value & " WHERE ParNo = '" & rsComSql_1.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql_1.Fields("PktNo").Value & "' AND Sec = 0")
                    End If
                End If
                rsComSql_2 = Nothing

                'Boiling Returns
                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT * FROM tblGrading_BoilingReturns WHERE ParNo = '" & rsComSql_1.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql_1.Fields("PktNo").Value & "'", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    AdoCN.Execute("UPDATE tblGrading_PacketInfo SET OkPcs = " & rsComSql_2.Fields("RetPcs").Value & ",OkCts = " & rsComSql_2.Fields("RetCts").Value & ",LostPcs = " & rsComSql_2.Fields("LostPcs").Value & ",LostCts = " & rsComSql_2.Fields("LostCts").Value & ",RejPcs = " & rsComSql_2.Fields("RejPcs").Value & ",RejCts = " & rsComSql_2.Fields("RejCts").Value & " WHERE ParNo = '" & rsComSql_1.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql_1.Fields("PktNo").Value & "' AND Sec = 0")
                End If
                rsComSql_2 = Nothing

                'Checking Issues
                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT * FROM tblGrading_CheckingIssues WHERE ParNo = '" & rsComSql_1.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql_1.Fields("PktNo").Value & "' ORDER BY Sec", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    rsComSql_2.MoveFirst()
                    While Not rsComSql_2.EOF
                        AdoCN.Execute("INSERT INTO tblGrading_PacketInfo(Department,ParNo,PktNo,Sec,IssPcs,IssCts,EmpNo) " & _
                                      "VALUES('" & rsComSql_1.Fields("Department").Value & "','" & rsComSql_1.Fields("ParNo").Value & "','" & rsComSql_1.Fields("PktNo").Value & "'," & rsComSql_2.Fields("Sec").Value & "," & rsComSql_2.Fields("IssPcs").Value & "," & rsComSql_2.Fields("IssCts").Value & ",'" & rsComSql_2.Fields("EmpNo").Value & "')")

                        'Checking Returns
                        rsComSql_3 = New ADODB.Recordset
                        rsComSql_3.Open("SELECT * FROM tblGrading_CheckingReturns WHERE ParNo = '" & rsComSql_1.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql_1.Fields("PktNo").Value & "' AND Sec = " & rsComSql_2.Fields("Sec").Value & "", AdoCN, 1, 1)
                        If rsComSql_3.RecordCount Then
                            AdoCN.Execute("UPDATE tblGrading_PacketInfo " & _
                                          "SET OkPcs = " & rsComSql_3.Fields("ExPcs").Value + rsComSql_3.Fields("VgPcs").Value + rsComSql_3.Fields("BlPcs").Value + rsComSql_3.Fields("ScPcs").Value + rsComSql_3.Fields("PsPcs").Value + rsComSql_3.Fields("SzPcs").Value + rsComSql_3.Fields("OkPcs").Value & "," & _
                                              "OkCts = " & rsComSql_3.Fields("ExCts").Value + rsComSql_3.Fields("VgCts").Value + rsComSql_3.Fields("BlCts").Value + rsComSql_3.Fields("ScCts").Value + rsComSql_3.Fields("PsCts").Value + rsComSql_3.Fields("SzCts").Value + rsComSql_3.Fields("OKCTS").Value & "," & _
                                              "LostPcs = " & rsComSql_3.Fields("LostPcs").Value & ",LostCts = " & rsComSql_3.Fields("LostCts").Value & "," & _
                                              "RejPcs = " & rsComSql_3.Fields("RejPcs").Value & ",RejCts = " & rsComSql_3.Fields("RejCts").Value & "," & _
                                              "RepPcs = " & rsComSql_3.Fields("RepPcs").Value & ",RepCts = " & rsComSql_3.Fields("RepCts").Value & "," & _
                                              "VRepPcs = " & rsComSql_3.Fields("VRepPcs").Value & ",VRepCts = " & rsComSql_3.Fields("VRepCts").Value & "," & _
                                              "RetDate = '" & Format(rsComSql_3.Fields("RetDate").Value, "MM/dd/yyyy") & "' " & _
                                          "WHERE ParNo = '" & rsComSql_1.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql_1.Fields("PktNo").Value & "' AND Sec = '" & rsComSql_3.Fields("Sec").Value & "'")
                        End If
                        rsComSql_3 = Nothing

                        rsComSql_2.MoveNext()
                    End While
                End If
                rsComSql_2 = Nothing

                rsComSql_1.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        rsComSql_1 = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub HazelDev_Button23_Click(sender As Object, e As EventArgs) Handles HazelDev_Button23.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGrading_StockBalanceFULL-RND.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button24_Click(sender As Object, e As EventArgs) Handles HazelDev_Button24.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAYAllSum.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button25_Click(sender As Object, e As EventArgs) Handles HazelDev_Button25.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptParcelAnalysisMS.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button26_Click(sender As Object, e As EventArgs) Handles HazelDev_Button26.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGrdGradingMix.rpt"
        strReportPath = PBReportPath & "DiaSalesGrading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button27_Click(sender As Object, e As EventArgs) Handles HazelDev_Button27.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingRoundsPackingList.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button28_Click(sender As Object, e As EventArgs) Handles HazelDev_Button28.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptBoilingCum.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button29_Click(sender As Object, e As EventArgs) Handles HazelDev_Button29.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGrading_CheckingBalance.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button30_Click(sender As Object, e As EventArgs) Handles HazelDev_Button30.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGrading_RndTransfer.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button31_Click(sender As Object, e As EventArgs) Handles HazelDev_Button31.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGrading_RndLotAnalysis.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button32_Click(sender As Object, e As EventArgs) Handles HazelDev_Button32.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptBoilingReturnsDetails.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button33_Click(sender As Object, e As EventArgs) Handles HazelDev_Button33.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingParcelTime.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button34_Click(sender As Object, e As EventArgs) Handles HazelDev_Button34.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPCUOrderBalanceRef.rpt"
        strReportPath = PBReportPath & "Precision\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button35_Click(sender As Object, e As EventArgs) Handles HazelDev_Button35.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingParcelTimeDate.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button36_Click(sender As Object, e As EventArgs) Handles HazelDev_Button36.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGrading_RghIssues.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button37_Click(sender As Object, e As EventArgs) Handles HazelDev_Button37.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGrading_RghIssuesRnd.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button38_Click(sender As Object, e As EventArgs) Handles HazelDev_Button38.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPktInfoSizeOrder.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button39_Click(sender As Object, e As EventArgs) Handles HazelDev_Button39.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingRndFinishDetails.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button40_Click(sender As Object, e As EventArgs) Handles HazelDev_Button40.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingRndFinishValue.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button41_Click(sender As Object, e As EventArgs) Handles HazelDev_Button41.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingRndFinishValue3.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button42_Click(sender As Object, e As EventArgs) Handles HazelDev_Button42.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGrading_StockBalanceFULLBox.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button43_Click(sender As Object, e As EventArgs) Handles HazelDev_Button43.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingBoilingPendingBag.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button44_Click(sender As Object, e As EventArgs) Handles HazelDev_Button44.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingBoilingPendingExt.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button45_Click(sender As Object, e As EventArgs) Handles HazelDev_Button45.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGrading_ExportBalance.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button46_Click(sender As Object, e As EventArgs) Handles HazelDev_Button46.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptCheckingReturnsEmpWise.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button47_Click(sender As Object, e As EventArgs) Handles HazelDev_Button47.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingOpenBal.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button48_Click(sender As Object, e As EventArgs) Handles HazelDev_Button48.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingOpenPacket.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button49_Click(sender As Object, e As EventArgs) Handles HazelDev_Button49.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingRndFinishValueRgh.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button50_Click(sender As Object, e As EventArgs) Handles HazelDev_Button50.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingRndFinishValueDate.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button51_Click(sender As Object, e As EventArgs) Handles HazelDev_Button51.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingBoilingStock.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button52_Click(sender As Object, e As EventArgs) Handles HazelDev_Button52.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGrading_CheckingMC.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button53_Click(sender As Object, e As EventArgs) Handles HazelDev_Button53.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingCheckingStock.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button54_Click(sender As Object, e As EventArgs) Handles HazelDev_Button54.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingSizingStock.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button55_Click(sender As Object, e As EventArgs) Handles HazelDev_Button55.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingSizingNotTrfStock.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button56_Click(sender As Object, e As EventArgs) Handles HazelDev_Button56.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExtStockOpeGrp.rpt"
        strReportPath = PBReportPath & "Ext\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button57_Click(sender As Object, e As EventArgs) Handles HazelDev_Button57.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListCOLMLot.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button58_Click(sender As Object, e As EventArgs) Handles HazelDev_Button58.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingRep_PktLableDate2.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button59_Click(sender As Object, e As EventArgs) Handles HazelDev_Button59.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingRndFinishValue5.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button60_Click(sender As Object, e As EventArgs) Handles HazelDev_Button60.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingRghIssues.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub
End Class