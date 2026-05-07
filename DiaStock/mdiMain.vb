
Imports System.Windows.Forms
Imports Microsoft.Win32
Imports System.AppDomain

Public Class mdiMain
    Private Sub mdiMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        End
    End Sub

    Private Sub ImportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportToolStripMenuItem.Click
        frm_DCLImport.Show()
    End Sub

    Private Sub mdiMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim keyName As String = Registry.CurrentUser.ToString() & "\Control Panel\International"
        Dim valueName As String = "sShortDate"
        Dim s As String = Registry.GetValue(keyName, valueName, String.Empty).ToString()
        Dim dtpModDate As Date
        Dim dtpNewDate As Date
        Dim strAppPath As String

        strCurDateFormat = s

        strAppPath = CurrentDomain.BaseDirectory()
        dtpModDate = IO.File.GetLastWriteTime(strAppPath & "\DiaStock.exe")
        dtpNewDate = IO.File.GetLastWriteTime("\\" & strServerName & "\ProductionSys\Release\DiaStock.exe")

        If dtpNewDate > dtpModDate Then
            MsgBox("New update is available", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If

        Me.Text = "D i a  S o l u t i o n  6 4  -  " & "(02/05/2026) Ver: 1.0"
        'strFileName = PBReportPath & "\Home.bmp"
        strFileName = strAppPath & "\Home.bmp"
        ToolStripStatusLabel2.Text = "| " & strDBName & " | " & PBUser_ID

        Load_Parameters()

        'PBResponse = Dir(strFileName)
        'If Len(PBResponse) > 0 Then
        '    Me.BackgroundImage = System.Drawing.Image.FromFile(strFileName)
        'Me.BackgroundImageLayout = ImageLayout.Stretch
        'End If

        'If PBUser_EmpNo = "D06975" Or PBUser_EmpNo = "D05155" Then
        '    MixShipmentPalnActualToolStripMenuItem.Visible = True
        'Else
        '    MixShipmentPalnActualToolStripMenuItem.Visible = False
        'End If
    End Sub

    Private Sub DepartmentTransferToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DepartmentTransferToolStripMenuItem.Click
        frm_DCLDeptTrans.Show()
    End Sub

    Private Sub ParcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ParcelToolStripMenuItem.Click
        frm_DCLParcel.Show()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub RghPacketToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RghPacketToolStripMenuItem.Click
        frm_RghPacket.Show()
    End Sub

    Private Sub RghSectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RghSectionToolStripMenuItem.Click
        frm_RghSection.Show()
    End Sub

    Private Sub RprPacketToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RprPacketToolStripMenuItem.Click
        frm_RprPacket.Show()
    End Sub

    Private Sub RprPacketAutoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RprPacketAutoToolStripMenuItem.Click
        frm_RprPacketAuto.Show()
    End Sub

    Private Sub RprPacketTransferToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RprPacketTransferToolStripMenuItem.Click
        frm_RprPacketTransfer.Show()
    End Sub

    Private Sub RprSectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RprSectionToolStripMenuItem.Click
        frm_RprSection.Show()
    End Sub

    Private Sub ReportsToolStripMenuItem1_Click(sender As Object, e As EventArgs)
        If PBUser_Level = 1 Then
            frm_DCLReports.Show()
        Else
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub PacketToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PacketToolStripMenuItem.Click
        frm_BagPacket.Show()
    End Sub

    Private Sub IssueReturnToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IssueReturnToolStripMenuItem.Click
        frm_BagSection.Show()
    End Sub

    Private Sub PacketToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PacketToolStripMenuItem1.Click
        frm_PrPacket.Show()
    End Sub

    Private Sub PacketToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles PacketToolStripMenuItem2.Click
        frm_RndPacket.Show()
    End Sub

    Private Sub IssueReturnToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles IssueReturnToolStripMenuItem1.Click
        frm_PrSection.Show()
    End Sub

    Private Sub IssueReturnToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles IssueReturnToolStripMenuItem2.Click
        frm_RndSection.Show()
    End Sub

    Private Sub PacketToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles PacketToolStripMenuItem3.Click
        frm_ExtPacket.Show()
    End Sub

    Private Sub ReportsToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ReportsToolStripMenuItem2.Click
        frm_PrReports.Show()
    End Sub

    Private Sub ReportsToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ReportsToolStripMenuItem3.Click
        frm_BagReports.Show()
    End Sub

    Private Sub ReportsToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ReportsToolStripMenuItem4.Click
        frm_RndReports.Show()
    End Sub

    Private Sub IssueReturnToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles IssueReturnToolStripMenuItem3.Click
        frm_ExtSection.Show()
    End Sub

    Private Sub RprEntryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RprEntryToolStripMenuItem.Click
        frm_RprEntry.Show()
    End Sub

    Private Sub OrderEnrtyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrderEnrtyToolStripMenuItem.Click
        frm_MixOrder.Show()
    End Sub

    Private Sub ReportsToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles ReportsToolStripMenuItem.Click
        frm_RghReports.Show()
    End Sub

    Private Sub OrderEntryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrderEntryToolStripMenuItem.Click
        frm_PCUOrder.Show()
    End Sub

    Private Sub PacketIssueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PacketIssueToolStripMenuItem.Click
        frm_RghPacketIssue.Show()
    End Sub

    Private Sub NIRUReportsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        If PBUser_Level = 1 Then
            frm_DCLReportsNiru.Show()
        Else
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub DepartmentTransferLotToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DepartmentTransferLotToolStripMenuItem.Click
        frm_DCLDeptTransLot.Show()
    End Sub

    Private Sub NIRUStockUploadToolStripMenuItem_Click(sender As Object, e As EventArgs)
        frm_NiruStockUpload.Show()
    End Sub

    Private Sub GradingFinishToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GradingFinishToolStripMenuItem.Click
        frm_GRDFinish.Show()
    End Sub

    Private Sub RprByPassToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RprByPassToolStripMenuItem.Click
        frm_RprByPass.Show()
    End Sub

    Private Sub RprByPassOneToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RprByPassOneToolStripMenuItem.Click
        frm_RprByPassOne.Show()
    End Sub

    Private Sub ReportsToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles ReportsToolStripMenuItem5.Click
        frm_ExtReports.Show()
    End Sub

    Private Sub IssueEntryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IssueEntryToolStripMenuItem.Click
        frm_DCLIssueEntry.Show()
    End Sub

    Private Sub ImportEditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportEditToolStripMenuItem.Click
        frm_DCLImportEdit.Show()
    End Sub

    Private Sub ImportPriceEditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportPriceEditToolStripMenuItem.Click
        If PBUser_Level = 1 Then
            frm_DCLImportPriceEdit.Show()
        Else
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub FinishParcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FinishParcelToolStripMenuItem.Click
        frm_DCLFinishParcel.Show()
    End Sub

    Private Sub SupplierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupplierToolStripMenuItem.Click
        frm_DCLSupplier.Show()
    End Sub

    Private Sub ByPassEntryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ByPassEntryToolStripMenuItem.Click
        frm_DCLByPass.Show()
    End Sub

    Private Sub RprOperationByPassToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RprOperationByPassToolStripMenuItem.Click
        frm_RprByPassOpr.Show()
    End Sub

    Private Sub PacketToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles PacketToolStripMenuItem4.Click
        frm_PCUPacket.Show()
    End Sub

    Private Sub IssueReturnToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles IssueReturnToolStripMenuItem4.Click
        frm_PCUSection.Show()
    End Sub

    Private Sub ReportsToolStripMenuItem6_Click(sender As Object, e As EventArgs) Handles ReportsToolStripMenuItem6.Click
        frm_PCUReports.Show()
    End Sub

    Private Sub EditReturnsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditReturnsToolStripMenuItem.Click
        frm_PCUEditReturns.Show()
    End Sub

    Private Sub OrdersVerificationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrdersVerificationToolStripMenuItem.Click
        frm_PCUFinishOrders.Show()
    End Sub

    Private Sub CompleteOrdersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompleteOrdersToolStripMenuItem.Click
        frm_PCUCompleteOrders.Show()
    End Sub

    Private Sub FinalRepairToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FinalRepairToolStripMenuItem.Click
        frm_DCLFinalRepair.Show()
    End Sub

    Private Sub PacketToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles PacketToolStripMenuItem5.Click
        frm_MixPacket.Show()
    End Sub

    Private Sub IssueReturnToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles IssueReturnToolStripMenuItem5.Click
        frm_MixSection.Show()
    End Sub

    Private Sub EditReturnsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EditReturnsToolStripMenuItem1.Click
        frm_MixEditReturns.Show()
    End Sub

    Private Sub ReportsToolStripMenuItem7_Click(sender As Object, e As EventArgs) Handles ReportsToolStripMenuItem7.Click
        frm_MixReports.Show()
    End Sub

    Private Sub OrdersVerificationToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles OrdersVerificationToolStripMenuItem1.Click
        frm_MixFinishOrders.Show()
    End Sub

    Private Sub ReportsToolStripMenuItem8_Click(sender As Object, e As EventArgs)
        frm_DCLReportsCommon.Show()
    End Sub

    Private Sub SolverToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SolverToolStripMenuItem.Click
        frm_MixSolver.Show()
    End Sub

    Private Sub InternalIssuesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InternalIssuesToolStripMenuItem.Click
        frm_MixIntIssues.Show()
    End Sub

    Private Sub ConfirmOrdersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConfirmOrdersToolStripMenuItem.Click
        If PBUser_Level = 1 Then
            frm_MixConfirmOrders.Show()
        Else
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub CheckingTransferToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckingTransferToolStripMenuItem.Click
        frm_DCLCheckIn.Show()
    End Sub

    Private Sub ShipmentPlanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShipmentPlanToolStripMenuItem.Click
        frm_MixShipmentPlan.Show()
    End Sub

    Private Sub ParcelAnalysisToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ParcelAnalysisToolStripMenuItem.Click
        frm_RndParcelAnalysis.Show()
    End Sub

    Private Sub FantacySalesSchemaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FantacySalesSchemaToolStripMenuItem.Click
        If PBUser_Level = 1 Then
            frm_DCLFantacySalesSchema.Show()
        Else
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub MixAssortmentHistoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MixAssortmentHistoryToolStripMenuItem.Click
        frm_MixAssortHistory.Show()
    End Sub

    Private Sub MixAssortmentStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MixAssortmentStockToolStripMenuItem.Click
        frm_MixAssortStock.Show()
    End Sub

    Private Sub MixAssortmentExportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MixAssortmentExportToolStripMenuItem.Click
        frm_MixAssortExport.Show()
    End Sub

    Private Sub MixAssortmentRejectProcessToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MixAssortmentRejectProcessToolStripMenuItem.Click
        frm_MixAssortReject.Show()
    End Sub

    Private Sub AcceptanceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AcceptanceToolStripMenuItem.Click
        frm_GRDAccept.Show()
    End Sub

    Private Sub ParcelToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ParcelToolStripMenuItem1.Click
        frm_GRDParcel.Show()
    End Sub

    Private Sub RepairToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RepairToolStripMenuItem.Click
        frm_GRDRepair.Show()
    End Sub

    Private Sub MixStoneRequirementToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MixStoneRequirementToolStripMenuItem.Click
        frm_MixStoneReq.Show()
    End Sub

    Private Sub PacketEntryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PacketEntryToolStripMenuItem.Click
        frm_GRDSizingPacket.Show()
    End Sub

    Private Sub BoilingCheckingToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles BoilingCheckingToolStripMenuItem1.Click
        frm_GRDBoiling.Show()
    End Sub

    Private Sub IssueReturnToolStripMenuItem8_Click(sender As Object, e As EventArgs) Handles IssueReturnToolStripMenuItem8.Click
        frm_GRDSection.Show()
    End Sub

    Private Sub IssueReturnToolStripMenuItem7_Click(sender As Object, e As EventArgs) Handles IssueReturnToolStripMenuItem7.Click
        frm_GRDSizingSection.Show()
    End Sub

    Private Sub RprBrutingRoundsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RprBrutingRoundsToolStripMenuItem.Click
        frm_RprBrutingRounds.Show()
    End Sub

    Private Sub IssueReturnToolStripMenuItem6_Click(sender As Object, e As EventArgs) Handles IssueReturnToolStripMenuItem6.Click
        frm_ExpSection.Show()
    End Sub

    Private Sub PacketEntryToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PacketEntryToolStripMenuItem1.Click
        frm_ExpSizingPacket.Show()
    End Sub

    Private Sub IssueReturnToolStripMenuItem9_Click(sender As Object, e As EventArgs) Handles IssueReturnToolStripMenuItem9.Click
        frm_ExpSizingSection.Show()
    End Sub

    Private Sub TransferToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransferToolStripMenuItem.Click
        frm_ExpSizingTransfer.Show()
    End Sub

    Private Sub RoughTransferToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RoughTransferToolStripMenuItem.Click
        frm_GRDRghIssues.Show()
    End Sub

    Private Sub RoughIssuesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RoughIssuesToolStripMenuItem.Click
        frm_ExpRghIssues.Show()
    End Sub

    Private Sub ParcelCompleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ParcelCompleteToolStripMenuItem.Click
        frm_DCLParcelComplete.Show()
    End Sub

    Private Sub SalesTransferToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalesTransferToolStripMenuItem.Click
        frm_POLTransfer.Show()
    End Sub

    Private Sub ModifyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModifyToolStripMenuItem.Click
        frm_POLModify.Show()
    End Sub

    Private Sub ExportSummaryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportSummaryToolStripMenuItem.Click
        frm_GRDExportSummary.Show()
    End Sub

    Private Sub ExportSummaryModifyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportSummaryModifyToolStripMenuItem.Click
        frm_GRDExportSummaryModify.Show()
    End Sub

    Private Sub ExportToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ExportToolStripMenuItem1.Click
        frm_GRDExportSummaryExport.Show()
    End Sub

    Private Sub PackingListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PackingListToolStripMenuItem.Click
        frm_DCLPackingList.Show()
    End Sub

    Private Sub LotApprovalToolStripMenuItem_Click(sender As Object, e As EventArgs)
        frm_DCLLotApproval.Show()
    End Sub

    Private Sub LotApprovalAcceptToolStripMenuItem_Click(sender As Object, e As EventArgs)
        If PBUser_Level = 1 Then
            frm_DCLLotApprovalAcc.Show()
        Else
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub ChangePasswordToolStripMenuItem_Click(sender As Object, e As EventArgs)
        frm_SYSUserChange.Show()
    End Sub

    Private Sub VerifyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VerifyToolStripMenuItem.Click
        frm_ExpSizingVerify.Show()
    End Sub

    Private Sub ExportToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ExportToolStripMenuItem2.Click
        frm_ExpSizingExport.Show()
    End Sub

    Private Sub ReportsToolStripMenuItem9_Click(sender As Object, e As EventArgs) Handles ReportsToolStripMenuItem9.Click
        frm_ExpReports.Show()
    End Sub

    Private Sub AcceptanceToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AcceptanceToolStripMenuItem1.Click
        frm_DCLPacketAccept.Show()
    End Sub

    Private Sub ExportVerificationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportVerificationToolStripMenuItem.Click
        If PBUser_Level = 1 Then
            frm_DCLExportVerification.Show()
        Else
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub RprParcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RprParcelToolStripMenuItem.Click
        frm_RprParcel.Show()
    End Sub

    Private Sub MixStoneReuirementNewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MixStoneReuirementNewToolStripMenuItem.Click
        frm_MixStoneReqNew.Show()
    End Sub

    Private Sub CostingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CostingToolStripMenuItem.Click
        If PBUser_Level = 1 Then
            frm_DCLCosting.Show()
        Else
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub CommonToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CommonToolStripMenuItem.Click
        frm_GRDReports.Show()
    End Sub

    Private Sub StockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StockToolStripMenuItem.Click
        If PBUser_EmpNo <> "D06975" Then
            Instring = UCase(InputBox("Enter Access Code", "Authorized Password"))
            If Instring = "TOYOTA123" Then
                frm_GRDReports2.Show()
            End If
        Else
            frm_GRDReports2.Show()
        End If
    End Sub

    Private Sub PackingListToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PackingListToolStripMenuItem1.Click
        If PBUser_EmpNo <> "D06975" Then
            Instring = UCase(InputBox("Enter Access Code", "Authorized Password"))
            If Instring = "EXP@ADMIN" Then
                frm_GRDReports3.Show()
            End If
        Else
            frm_GRDReports3.Show()
        End If
    End Sub

    Private Sub TempBoxTransferToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TempBoxTransferToolStripMenuItem.Click
        frm_POMTransfer.Show()
    End Sub

    Private Sub TempBoxModifyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TempBoxModifyToolStripMenuItem.Click
        frm_POMModify.Show()
    End Sub

    Private Sub RejectsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RejectsToolStripMenuItem.Click
        frm_DCLRghRejects.Show()
    End Sub

    Private Sub PackagePCUToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PackagePCUToolStripMenuItem.Click
        frm_GRDExportSummaryPCU.Show()
    End Sub

    Private Sub ExportPlanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportPlanToolStripMenuItem.Click
        frm_DCLExportPlan.Show()
    End Sub

    Private Sub CostingEditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CostingEditToolStripMenuItem.Click
        If PBUser_Level = 1 Then
            frm_DCLCostingEdit.Show()
        Else
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub InvoiceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InvoiceToolStripMenuItem.Click
        If PBUser_Level = 1 Then
            frm_DCLInvoice.Show()
        Else
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub MixTransferToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles MixTransferToolStripMenuItem1.Click
        frm_GRDMixTransfer.Show()
    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        frm_DCLDashboard.Show()
    End Sub

    Private Sub PacketGroupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PacketGroupToolStripMenuItem.Click
        frm_MixPacketGrp.Show()
    End Sub

    Private Sub PacketVerifyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PacketVerifyToolStripMenuItem.Click
        If PBUser_EmpNo <> "D06975" Then
            Instring = UCase(InputBox("Enter Access Code", "Authorized Password"))
            If Instring = "EXPPASS" Then
                frm_MixPacketVerify.Show()
            End If
        Else
            frm_MixPacketVerify.Show()
        End If
    End Sub

    Private Sub OrderGroupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrderGroupToolStripMenuItem.Click
        frm_MixOrderGrp.Show()
    End Sub

    Private Sub StockAcceptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StockAcceptToolStripMenuItem.Click
        frm_MixStockAccept.Show()
    End Sub

    Private Sub OrderSolverToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrderSolverToolStripMenuItem.Click
        frm_MixOrderSolver.Show()
    End Sub

    Private Sub MixRejectVerificationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MixRejectVerificationToolStripMenuItem.Click
        frm_MixAssortRejectVerify.Show()
    End Sub

    Private Sub MixAssortmentModifyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MixAssortmentModifyToolStripMenuItem.Click
        If PBUser_EmpNo = "D06975" Then
            frm_MixAssortModify.Show()
        Else
            Instring = UCase(InputBox("Enter Access Code", "Authorized Password"))
            If Instring = "KARAN5502" Then
                frm_MixAssortModify.Show()
            End If
        End If
    End Sub

    Private Sub MixPackingUploadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MixPackingUploadToolStripMenuItem.Click
        frm_MixPackingUpload.Show()
    End Sub

    Private Sub FantacyAnalyzerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FantacyAnalyzerToolStripMenuItem.Click
        If PBUser_Level = 1 Then
            frm_DCLFantacyAnalyzer.Show()
        Else
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub FantacySchemaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FantacySchemaToolStripMenuItem.Click
        If PBUser_Level = 1 Then
            frm_DCLFantacySchema.Show()
        Else
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub IncentiveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IncentiveToolStripMenuItem.Click
        frm_HRDIncentiveNew.Show()
    End Sub

    Private Sub MixShipmentPalnActualToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MixShipmentPalnActualToolStripMenuItem.Click
        frm_MixShipmentPlan2.Show()
    End Sub

    Private Sub Reports2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Reports2ToolStripMenuItem.Click
        frm_ExpReports2.Show()
    End Sub

    Private Sub WeightLossToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WeightLossToolStripMenuItem.Click
        frm_DCLWeightLoss.Show()
    End Sub

    Private Sub ApprovalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ApprovalToolStripMenuItem.Click
        If PBUser_Level = 1 Then
            frm_ExpSizingApproval.Show()
        Else
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub OrderPlanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrderPlanToolStripMenuItem.Click
        frm_ExpOrderPlan.Show()
    End Sub

    Private Sub BoilingReturnToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BoilingReturnToolStripMenuItem.Click
        frm_GRDBoilingReturn.Show()
    End Sub

    Private Sub CheckingIssueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckingIssueToolStripMenuItem.Click
        frm_GRDCheckingIssue.Show()
    End Sub

    Private Sub AssortmentUploadToolStripMenuItem_Click(sender As Object, e As EventArgs)
        If PBUser_Level = 1 Then
            frm_DCLAssortUpload.Show()
        Else
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click
        frm_GRDSizingEdit.Show()
    End Sub

    Private Sub BulkIssueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BulkIssueToolStripMenuItem.Click
        frm_GRDSizingIssue.Show()
    End Sub

    Private Sub ParcelSummaryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ParcelSummaryToolStripMenuItem.Click
        frm_DCLParcelSummary.Show()
    End Sub

    Private Sub PacketVerifyToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PacketVerifyToolStripMenuItem1.Click
        frm_ExpPacket.Show()
    End Sub

    Private Sub FinishOrdersEditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FinishOrdersEditToolStripMenuItem.Click
        frm_MixFinishOrdersEdit.Show()
    End Sub

    Private Sub RejectDetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RejectDetailsToolStripMenuItem.Click
        frm_DCLRejectDetails.Show()
    End Sub

    Private Sub TurnOverToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TurnOverToolStripMenuItem.Click
        If PBUser_Level = 1 Then
            frm_DCLTurnOver.Show()
        Else
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub EditReturnsToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles EditReturnsToolStripMenuItem2.Click
        If PBUser_EmpNo <> "D06975" Then
            Instring = UCase(InputBox("Enter Access Code", "Authorized Password"))
            If Instring = "CT123" Then
                frm_DCLEditReturns.Show()
            End If
        Else
            frm_DCLEditReturns.Show()
        End If
    End Sub

    Private Sub MixTransferToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MixTransferToolStripMenuItem.Click
        frm_RghMixTrf.Show()
    End Sub

    Private Sub RejectBoxToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RejectBoxToolStripMenuItem.Click
        frm_ExpRejectBox.Show()
    End Sub

    Private Sub PacketEditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PacketEditToolStripMenuItem.Click
        frm_RndPacketEdit.Show()
    End Sub

    Private Sub BundleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BundleToolStripMenuItem.Click
        frm_GRDBundle.Show()
    End Sub

    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        frm_DCLRghRejectsPack.Show()
    End Sub

    Private Sub ImportStickerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportStickerToolStripMenuItem.Click
        frm_DCLImportSticker.Show()
    End Sub

    Private Sub AssortmentModifyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AssortmentModifyToolStripMenuItem.Click
        frm_ExpAssortModify.Show()
    End Sub

    Private Sub RoundsUploadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RoundsUploadToolStripMenuItem.Click
        If PBUser_Level = 1 Then
            frm_RndUpload.Show()
        Else
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub ModifyAssortmentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModifyAssortmentToolStripMenuItem.Click
        frm_POLModifyAssort.Show()
    End Sub

    Private Sub UserRightsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        If PBUser_EmpNo = "D06975" Then
            frm_SYSUserRights.Show()
        End If
    End Sub

    Private Sub GrooveVerificationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GrooveVerificationToolStripMenuItem.Click
        frm_MixGrooveVerification.Show()
    End Sub

    Private Sub ParcelSelectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ParcelSelectionToolStripMenuItem.Click
        frm_DCLParcelSelection.Show()
    End Sub

    Private Sub EmployeeProductionToolStripMenuItem_Click(sender As Object, e As EventArgs)
        frm_DCLEmpProduction.Show()
    End Sub

    Private Sub IncentiveToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles IncentiveToolStripMenuItem1.Click
        frm_MixIncentive.Show()
    End Sub

    Private Sub IncentiveToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles IncentiveToolStripMenuItem2.Click
        frm_BagIncentive.Show()
    End Sub

    Private Sub IncentiveToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles IncentiveToolStripMenuItem3.Click
        frm_RndIncentive.Show()
    End Sub

    Private Sub PacketPolishingEditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PacketPolishingEditToolStripMenuItem.Click
        frm_RndPacketEditPol.Show()
    End Sub

    Private Sub RprIncentiveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RprIncentiveToolStripMenuItem.Click
        frm_RprIncentive.Show()
    End Sub

    Private Sub EmployeeIssuesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EmployeeIssuesToolStripMenuItem.Click
        frm_MixEmpIssues.Show()
    End Sub

    Private Sub MixAssortmentRejectTransferToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MixAssortmentRejectTransferToolStripMenuItem.Click
        frm_MixAssortRejectTransfer.Show()
    End Sub

    Private Sub SendToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SendToolStripMenuItem.Click
        frm_ExpSizingSend.Show()
    End Sub

    Private Sub MixRejectSendToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MixRejectSendToolStripMenuItem.Click
        frm_MixAssortRejectSend.Show()
    End Sub

    Private Sub MixApprovalSRWToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MixApprovalSRWToolStripMenuItem.Click
        If PBUser_EmpNo = "D06975" Then
            frm_MixApprovalSRWNew.Show()
        Else
            Instring = UCase(InputBox("Enter Access Code", "Authorized Password"))
            If Instring = "KB123" Then
                frm_MixApprovalSRWNew.Show()
            End If
        End If
    End Sub

    Private Sub TempOrderEntryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TempOrderEntryToolStripMenuItem.Click
        frm_MixOrderT.Show()
    End Sub

    Private Sub PacketPrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PacketPrintToolStripMenuItem.Click
        frm_MixPacketPrint.Show()
    End Sub

    Private Sub RepairReturnToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RepairReturnToolStripMenuItem.Click
        frm_MixRepairReturn.Show()
    End Sub

    Private Sub RoughTransferToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RoughTransferToolStripMenuItem1.Click
        frm_BagTransfer.Show()
    End Sub

    Private Sub IssueEntryToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles IssueEntryToolStripMenuItem1.Click
        frm_MixIssueEntry.Show()
    End Sub

    Private Sub APCUAssortmentUpdateToolStripMenuItem_Click(sender As Object, e As EventArgs)
        If PBUser_Level = 1 Then
            frm_MixAssortUpdate.Show()
        Else
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub RprPriceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RprPriceToolStripMenuItem.Click
        frm_RprPrice.Show()
    End Sub

    Private Sub EmployeeProductionToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EmployeeProductionToolStripMenuItem1.Click
        frm_DCLEmpProduction.Show()
    End Sub

    Private Sub ByPassEntryToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ByPassEntryToolStripMenuItem1.Click
        frm_MixByPass.Show()
    End Sub

    Private Sub ForeverMarkToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ForeverMarkToolStripMenuItem.Click
        frm_GRDBoxFM.Show()
    End Sub

    Private Sub RoundsDetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RoundsDetailsToolStripMenuItem.Click
        frm_GRDRoundsDetails.Show()
    End Sub

    Private Sub PackageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PackageToolStripMenuItem.Click
        frm_GRDExportSummaryRnd.Show()
    End Sub

    Private Sub MixPackageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MixPackageToolStripMenuItem.Click
        frm_GRDExportSummaryModifyRnd.Show()
    End Sub

    Private Sub BulkIssueToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles BulkIssueToolStripMenuItem1.Click
        frm_ExpSizingIssue.Show()
    End Sub

    Private Sub GradingTransferToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GradingTransferToolStripMenuItem.Click
        frm_GRDFinishRounds.Show()
    End Sub

    Private Sub GradingAcceptanceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GradingAcceptanceToolStripMenuItem.Click
        frm_GRDAcceptRounds.Show()
    End Sub

    Private Sub PacketAccpetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PacketAccpetToolStripMenuItem.Click
        frm_MixPacketAccept.Show()
    End Sub

    Private Sub SizeRangeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SizeRangeToolStripMenuItem.Click
        frm_RndSectionDetails.Show()
    End Sub

    Private Sub BagAcceptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BagAcceptToolStripMenuItem.Click
        frm_GRDAcceptBag.Show()
    End Sub

    Private Sub BagSendToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BagSendToolStripMenuItem.Click
        frm_GRDAcceptBagSend.Show()
    End Sub

    Private Sub LabEntryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LabEntryToolStripMenuItem.Click
        frm_DCLLab.Show()
    End Sub

    Private Sub MonthEndProcessToolStripMenuItem_Click(sender As Object, e As EventArgs)
        If PBUser_Level = 1 Then
            frm_DCLMonthEnd.Show()
        Else
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub ParcelOpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ParcelOpenToolStripMenuItem.Click
        frm_DCLParcelOpen.Show()
    End Sub

    Private Sub RoughSortingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RoughSortingToolStripMenuItem.Click
        frm_RghSort.Show()
    End Sub

    Private Sub RejectEditorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RejectEditorToolStripMenuItem.Click
        If PBUser_EmpNo <> "D06975" Then
            Instring = UCase(InputBox("Enter Access Code", "Authorized Password"))
            If Instring = "CT123" Then
                frm_MixRejectEditor.Show()
            End If
        Else
            frm_MixRejectEditor.Show()
        End If
    End Sub

    Private Sub RoughSortingPriceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RoughSortingPriceToolStripMenuItem.Click
        frm_RghSortPrice.Show()
    End Sub

    Private Sub FinishReturnsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FinishReturnsToolStripMenuItem.Click
        frm_MixFinishReturns.Show()
    End Sub

    Private Sub RepairProcessToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RepairProcessToolStripMenuItem.Click
        frm_MixSectionRep.Show()
    End Sub

    Private Sub ReturnsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturnsToolStripMenuItem.Click
        frm_MixFinishReturns18.Show()
    End Sub

    Private Sub ReturnsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ReturnsToolStripMenuItem1.Click
        frm_MixFinishReturns15.Show()
    End Sub

    Private Sub BoilingProcessToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles BoilingProcessToolStripMenuItem1.Click
        frm_MixSectionBoil.Show()
    End Sub

    Private Sub BoilingIssueToolStripMenuItem1_Click_1(sender As Object, e As EventArgs) Handles BoilingIssueToolStripMenuItem1.Click
        frm_MixBoilingIssue.Show()
    End Sub

    Private Sub BoilingIssueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BoilingIssueToolStripMenuItem.Click
        frm_MixBoiling.Show()
    End Sub

    Private Sub BoilingReturnToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles BoilingReturnToolStripMenuItem1.Click
        frm_MixBoilingReturn.Show()
    End Sub

    Private Sub ManulaBoilingIssueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ManulaBoilingIssueToolStripMenuItem.Click
        frm_MixBoilingManual.Show()
    End Sub

    Private Sub ManualBoilingReturnToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ManualBoilingReturnToolStripMenuItem.Click
        frm_MixBoilingReturnManual.Show()
    End Sub

    Private Sub AssessmentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AssessmentToolStripMenuItem.Click
        frm_HRDAssessment.Show()
    End Sub

    Private Sub LostFoundToolStripMenuItem_Click(sender As Object, e As EventArgs)
        frm_DCLFound.Show()
    End Sub

    Private Sub ReturnEntryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturnEntryToolStripMenuItem.Click
        frm_DCLReturnEntry.Show()
    End Sub

    Private Sub PacketOpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PacketOpenToolStripMenuItem.Click
        frm_MixPacketOpen.Show()
    End Sub

    Private Sub MixPackingPrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MixPackingPrintToolStripMenuItem.Click
        frm_MixPackingPrint.Show()
    End Sub

    Private Sub HRReportsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HRReportsToolStripMenuItem.Click
        If PBUser_EmpNo <> "D06975" Then
            Instring = UCase(InputBox("Enter Access Code", "Authorized Password"))
            If Instring = "HR789" Then
                frm_HRDReports.Show()
            End If
        Else
            frm_HRDReports.Show()
        End If
    End Sub

    Private Sub HalfIssuesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HalfIssuesToolStripMenuItem.Click
        frm_MixHalfIssues15.Show()
    End Sub

    Private Sub ReturnsToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ReturnsToolStripMenuItem2.Click
        frm_MixFinishReturns16.Show()
    End Sub

    Private Sub LotIDSearchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LotIDSearchToolStripMenuItem.Click
        frm_DCLLotIDSearch.Show()
    End Sub

    Private Sub LabEntryToolStripMenuItem1_Click(sender As Object, e As EventArgs)
        frm_LabEntry.Show()
    End Sub

    Private Sub LineNoSearchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LineNoSearchToolStripMenuItem.Click
        frm_DCLLineNoSearch.Show()
    End Sub

    Private Sub CheckingDetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckingDetailsToolStripMenuItem.Click
        frm_GRDCheckingDetails.Show()
    End Sub

    Private Sub ParcelFinishToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ParcelFinishToolStripMenuItem.Click
        frm_DCLParcelFinish.Show()
    End Sub

    Private Sub CostingDeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CostingDeleteToolStripMenuItem.Click
        If PBUser_Level = 1 Then
            frm_DCLCostingDelete.Show()
        Else
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub RoughSortingModelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RoughSortingModelToolStripMenuItem.Click
        frm_RghSortModel.Show()
    End Sub

    Private Sub OrderEntryToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles OrderEntryToolStripMenuItem1.Click
        frm_KITOrder.Show()
    End Sub

    Private Sub PacketEntryToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles PacketEntryToolStripMenuItem2.Click
        frm_KITPacket.Show()
    End Sub

    Private Sub ReportsToolStripMenuItem11_Click(sender As Object, e As EventArgs) Handles ReportsToolStripMenuItem11.Click
        frm_KITReports.Show()
    End Sub

    Private Sub OrdersVerificationToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles OrdersVerificationToolStripMenuItem2.Click
        frm_KITFinishOrders.Show()
    End Sub

    Private Sub ConfirmOrdersToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ConfirmOrdersToolStripMenuItem1.Click
        frm_KITConfirmOrders.Show()
    End Sub

    Private Sub FinanceReportsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        frm_DCLReportsFinance.Show()
    End Sub

    Private Sub MixAssortmentRejectAcceptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MixAssortmentRejectAcceptToolStripMenuItem.Click
        frm_MixAssortRejectAccept.Show()
    End Sub

    Private Sub LabCertificatesToolStripMenuItem_Click(sender As Object, e As EventArgs)
        frm_LabCertify.Show()
    End Sub

    Private Sub MixCostingEditorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MixCostingEditorToolStripMenuItem.Click
        frm_DCLCostingEditMix.Show()
    End Sub

    Private Sub LineNoEditNewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LineNoEditNewToolStripMenuItem.Click
        frm_DCLCostingEditMixNew.Show()
    End Sub

    Private Sub OfferEntryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OfferEntryToolStripMenuItem.Click
        frm_MixOrderO.Show()
    End Sub

    Private Sub OfferUploadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OfferUploadToolStripMenuItem.Click
        frm_MixOfferUpload.Show()
    End Sub

    Private Sub OrderUploadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrderUploadToolStripMenuItem.Click
        frm_MixOrderUpload.Show()
    End Sub

    Private Sub OrderApprovalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrderApprovalToolStripMenuItem.Click
        frm_MixOrderApproval.Show()
    End Sub

    Private Sub RejectDetailsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RejectDetailsToolStripMenuItem1.Click
        frm_ExpRejects.Show()
    End Sub

    Private Sub ImportOGLUploadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportOGLUploadToolStripMenuItem.Click
        frm_DCLImportOGL.Show()
    End Sub

    Private Sub ImportCusDecUploadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportCusDecUploadToolStripMenuItem.Click
        frm_DCLImportCusDec.Show()
    End Sub

    Private Sub ImportPDFUploadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportPDFUploadToolStripMenuItem.Click
        frm_DCLImportScan.Show()
    End Sub

    Private Sub ImportCusDecVerificationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportCusDecVerificationToolStripMenuItem.Click
        frm_DCLImportCusDecVerify.Show()
    End Sub

    Private Sub OrderGroupNewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrderGroupNewToolStripMenuItem.Click
        frm_MixOrderGrp2.Show()
    End Sub

    Private Sub RprFinishTransferToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RprFinishTransferToolStripMenuItem.Click
        frm_RprFinishTransfer.Show()
    End Sub

    Private Sub EditReturns16ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditReturns16ToolStripMenuItem.Click
        frm_MixEditReturns16.Show()
    End Sub

    Private Sub RprUploadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RprUploadToolStripMenuItem.Click
        frm_RprUpload.Show()
    End Sub

    Private Sub FirstSizingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FirstSizingToolStripMenuItem.Click
        frm_ExpAssortSizing.Show()
    End Sub

    Private Sub PacketEntryToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles PacketEntryToolStripMenuItem3.Click
        frm_ExpAssortPacket.Show()
    End Sub

    Private Sub PriceUpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PriceUpdateToolStripMenuItem.Click
        frm_ExpAssortPriceUpdate.Show()
    End Sub

    Private Sub PlanValueUpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PlanValueUpdateToolStripMenuItem.Click
        frm_RprPlanValue.Show()
    End Sub

    Private Sub GatePassToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GatePassToolStripMenuItem.Click
        If PBUser_EmpNo <> "D06975" Then
            Instring = UCase(InputBox("Enter Access Code", "Authorized Password"))
            If Instring = "123@DCL" Then
                frm_DCLGatePass.Show()
            End If
        Else
            frm_DCLGatePass.Show()
        End If
    End Sub

    Private Sub ModifyToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ModifyToolStripMenuItem1.Click
        frm_GRDSizingModify.Show()
    End Sub

    Private Sub MixConfirmOrdersEditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MixConfirmOrdersEditToolStripMenuItem.Click
        frm_MixConfirmOrdersEdit.Show()
    End Sub

    Private Sub OrderDrawingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrderDrawingToolStripMenuItem.Click
        If PBUser_EmpNo = "D06975" Or PBUser_EmpNo = "D05502" Then
            frm_MixOrderImage.Show()
        Else
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub RprAdminToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RprAdminToolStripMenuItem.Click
        frm_RprPacketAdmin.Show()
    End Sub

    Private Sub TraceUploadToolStripMenuItem_Click(sender As Object, e As EventArgs)
        frm_DCLTraceUpload.Show()
    End Sub

    Private Sub BoxingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BoxingToolStripMenuItem.Click
        frm_GRDBox.Show()
    End Sub

    Private Sub RprIssueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RprIssueToolStripMenuItem.Click
        frm_RprIssueEntry.Show()
    End Sub

    Private Sub LBReportsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LBReportsToolStripMenuItem.Click
        frm_RndReportsLB.Show()
    End Sub

    Private Sub AssortmentUploadToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AssortmentUploadToolStripMenuItem1.Click
        frm_ExpAssortUpload.Show()
    End Sub

    Private Sub UserNamesToolStripMenuItem_Click(sender As Object, e As EventArgs)
        If PBUser_EmpNo = "D06975" Then
            frm_SYSUserNames.Show()
        End If
    End Sub

    Private Sub PerformanceKPIToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PerformanceKPIToolStripMenuItem.Click
        frm_HRDPerformance.Show()
    End Sub

    Private Sub PacketGroupToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PacketGroupToolStripMenuItem1.Click
        frm_PCUPacketGrp.Show()
    End Sub

    Private Sub HistoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HistoryToolStripMenuItem.Click
        frm_POLHistory.Show()
    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub SystemToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SystemToolStripMenuItem.Click

    End Sub

    Private Sub mnuPacket_Click(sender As Object, e As EventArgs) Handles mnuPacket.Click
        frm_GRDRnd_Packet.Show()
    End Sub

    Private Sub SizingPacketEntry_Click(sender As Object, e As EventArgs) Handles SizingPacketEntry.Click
        frm_GRDRnd_SizingPacket.Show()
    End Sub

    Private Sub Parcel_Click(sender As Object, e As EventArgs) Handles Parcel.Click
        frm_GRDRnd_Parcels.Show()
    End Sub

    Private Sub ImportUploadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportUploadToolStripMenuItem.Click
        frm_GRDRnd_PAY_Excel_Upload.Show()
    End Sub

    Private Sub PackingUploadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PackingUploadToolStripMenuItem.Click
        frm_GRDRnd_Upload.Show()
    End Sub

    Private Sub InvoicesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InvoicesToolStripMenuItem.Click
        frm_GRDRnd_Niru_Invoice.Show()
    End Sub

    Private Sub UploadDetailsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UploadDetailsToolStripMenuItem.Click
        frm_GRDRnd_PacketUpload.Show()
    End Sub

    Private Sub PackingListToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles PackingListToolStripMenuItem2.Click
        frm_GRDRnd_Export.Show()
    End Sub

    Private Sub PackageToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PackageToolStripMenuItem1.Click
        frm_GRDRnd_ExportSummary.Show()
    End Sub

    Private Sub MixPackagesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MixPackagesToolStripMenuItem.Click
        frm_GRDRnd_ExportSummaryModify.Show()
    End Sub

    Private Sub BundleModuleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BundleModuleToolStripMenuItem.Click
        frm_GRDRnd_Bundle.Show()
    End Sub

    Private Sub BunldePackageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BunldePackageToolStripMenuItem.Click
        frm_GRDRnd_ExportSummaryBundle.Show()
    End Sub

    Private Sub IssueReturnToolStripMenuItem10_Click(sender As Object, e As EventArgs) Handles IssueReturnToolStripMenuItem10.Click
        frm_GRDRnd_Boiling.Show()
    End Sub

    Private Sub SizingIssuereturnToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SizingIssuereturnToolStripMenuItem.Click
        frm_GRDRnd_Sizing.Show()
    End Sub

    Private Sub AssortmentCalculatorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AssortmentCalculatorToolStripMenuItem.Click
        frm_GRDRnd_AssortComp.Show()
    End Sub

    Private Sub ReportsToolStripMenuItem12_Click(sender As Object, e As EventArgs) Handles ReportsToolStripMenuItem12.Click
        frm_GRDRnd_Reports.Show()
    End Sub

    Private Sub CompanyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompanyToolStripMenuItem.Click
        frm_GRDRnd_HOT_Company.Show()
    End Sub
End Class
