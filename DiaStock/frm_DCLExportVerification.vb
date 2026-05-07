
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLExportVerification
    Dim intNoOfRecords As Integer
    Dim intCounter As Long

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub frm_DCLExportVerification_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_DepartmentExp(cmbDepartment)
    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        If optNew.Checked = True Then
            Load_ExportInfo()
        Else
            Load_SavedData()
        End If
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim dblBaseCost As Double
        Dim dblAdjCost As Double
        Dim vRecordNo As Double

        If optNew.Checked = True Then
            mStrSQL = "SELECT MAX(RecordNo) AS RecordNo FROM tblExportVarification"
            rsComSql = New ADODB.Recordset
            rsComSql.Open(mStrSQL, AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("RecordNo").Value) Then
                    vRecordNo = rsComSql.Fields("RecordNo").Value + 1
                Else
                    vRecordNo = 1
                End If
            End If
            rsComSql = Nothing

            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = flxDetails.Rows.Count

            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(14, intRow).Value = True Then
                    If Len(flxDetails.Item(20, intRow).Value) <> 0 Then dblBaseCost = CDbl(flxDetails.Item(20, intRow).Value) Else dblBaseCost = 0
                    If Len(flxDetails.Item(21, intRow).Value) <> 0 Then dblAdjCost = CDbl(flxDetails.Item(21, intRow).Value) Else dblAdjCost = 0

                    AdoCN.Execute("INSERT INTO tblExportVarification(Department,Assortment,Price,Reference1,Reference2,ExpPcs,ExpCts,StCt,Charges,Request,RoughPcs,RoughCts,Yield,TransferedCts,Send1," & _
                                    "Status,AuditNo,RecordNo,OrderRef,ModifyBy,OrigAssort,InID,NLineNo,BasePrice,AdjPrice,APCUAssort,LotNo,OrderSide) " & _
                                  "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & CDbl(flxDetails.Item(2, intRow).Value) & ",'" & flxDetails.Item(3, intRow).Value & "'," & _
                                    "'" & flxDetails.Item(4, intRow).Value & "'," & CDbl(flxDetails.Item(5, intRow).Value) & "," & CDbl(flxDetails.Item(6, intRow).Value) & "," & CDbl(flxDetails.Item(7, intRow).Value) & "," & _
                                    "" & CDbl(flxDetails.Item(8, intRow).Value) & ",'" & flxDetails.Item(9, intRow).Value & "'," & CDbl(flxDetails.Item(10, intRow).Value) & "," & CDbl(flxDetails.Item(11, intRow).Value) & "," & _
                                    "'" & flxDetails.Item(12, intRow).Value & "'," & CDbl(flxDetails.Item(13, intRow).Value) & ",1,'A',0," & vRecordNo & "," & _
                                    "'" & Replace(flxDetails.Item(16, intRow).Value, "'", "''") & "','" & PBUser_EmpNo & "','" & flxDetails.Item(17, intRow).Value & "'," & CDbl(flxDetails.Item(18, intRow).Value) & ",'" & flxDetails.Item(19, intRow).Value & "'," & _
                                    "" & dblBaseCost & "," & dblAdjCost & ",'" & flxDetails.Item(22, intRow).Value & "','" & flxDetails.Item(23, intRow).Value & "','" & flxDetails.Item(24, intRow).Value & "')")

                    vRecordNo = vRecordNo + 1

                    Select Case cmbDepartment.Text
                        Case "Baguettes"
                            AdoCN.Execute("UPDATE tblBAGFinishParcels " & _
                                          "SET Status = 'E' " & _
                                          "WHERE Assortment = '" & flxDetails.Item(1, intRow).Value & "' " & _
                                            "AND Suppref = '" & flxDetails.Item(3, intRow).Value & "' " & _
                                            "AND DCLref = '" & flxDetails.Item(4, intRow).Value & "' " & _
                                            "AND Status = 'A'")

                        Case "Princess"
                            AdoCN.Execute("UPDATE tblPRFinishedParcels " & _
                                          "SET Status = 'E' " & _
                                          "WHERE Assortment = '" & flxDetails.Item(1, intRow).Value & "' " & _
                                            "AND SuppParNo = '" & flxDetails.Item(3, intRow).Value & "' " & _
                                            "AND DCLParNo = '" & flxDetails.Item(4, intRow).Value & "' " & _
                                            "AND Status = 'A'")

                        Case "Rounds"
                            AdoCN.Execute("UPDATE tblRndFinishParcels " & _
                                          "SET Status = 'E' " & _
                                          "WHERE Assortment = '" & flxDetails.Item(1, intRow).Value & "' " & _
                                            "AND SuppRef = '" & flxDetails.Item(3, intRow).Value & "' " & _
                                            "AND DCLRef = '" & flxDetails.Item(4, intRow).Value & "' " & _
                                            "AND Status = 'A'")

                        Case "Rounds4", "Opening", "Baguettes2", "Baguettes3", "Princess2", "Colombo Niru", "Emerald", "Carrer", "Emerald2", "Emerald3", "Lamour", "Davinci", "RoundsNLE", "Asscher", "Radiant"
                            AdoCN.Execute("UPDATE tblExtFinishParcels " & _
                                          "SET Status = 'E' " & _
                                          "WHERE Assortment = '" & flxDetails.Item(1, intRow).Value & "' " & _
                                            "AND Department = '" & cmbDepartment.Text & "' " & _
                                            "AND SuppRef = '" & flxDetails.Item(3, intRow).Value & "' " & _
                                            "AND DCLRef = '" & flxDetails.Item(4, intRow).Value & "' " & _
                                            "AND Status = 'A'")

                            If cmbDepartment.Text = "Princess2" Then
                                AdoCN.Execute("UPDATE tblGrading_PackingList " & _
                                              "SET OK = 1 " & _
                                              "WHERE Department = '" & cmbDepartment.Text & "' " & _
                                                "AND ParNo = '" & flxDetails.Item(4, intRow).Value & "' " & _
                                                "AND OK = 0")
                            End If

                        Case "Mix"
                            AdoCN.Execute("UPDATE tblMixExportOrders " & _
                                          "SET Status = 'E' " & _
                                          "WHERE Assortment = '" & flxDetails.Item(1, intRow).Value & "' " & _
                                            "AND OrderNo = '" & flxDetails.Item(3, intRow).Value & "' " & _
                                            "AND Reference = '" & Replace(flxDetails.Item(16, intRow).Value, "'", "''") & "' " & _
                                            "AND Status = 'A'")

                        Case "MixRefer"
                            AdoCN.Execute("UPDATE tblMixFinishOrdersR " & _
                                          "SET Status = 'E' " & _
                                          "WHERE Assortment = '" & flxDetails.Item(1, intRow).Value & "' " & _
                                            "AND OrderNo = '" & flxDetails.Item(3, intRow).Value & "' " & _
                                            "AND Reference = '" & flxDetails.Item(16, intRow).Value & "' " & _
                                            "AND Status = 'A'")

                        Case "KIT Box"
                            AdoCN.Execute("UPDATE tblMixExportOrders " & _
                                          "SET Status = 'E' " & _
                                          "WHERE Assortment = '" & flxDetails.Item(1, intRow).Value & "' " & _
                                            "AND OrderNo = '" & flxDetails.Item(3, intRow).Value & "' " & _
                                            "AND Reference = '" & flxDetails.Item(16, intRow).Value & "' " & _
                                            "AND Status = 'A'")

                        Case "SizeExports"
                            AdoCN.Execute("UPDATE tblExpReExports " & _
                                          "SET OK = 1 " & _
                                          "WHERE Assortment = '" & flxDetails.Item(1, intRow).Value & "' " & _
                                            "AND ParNo = '" & flxDetails.Item(4, intRow).Value & "' " & _
                                            "AND PackNo = " & CDbl(flxDetails.Item(18, intRow).Value) & " " & _
                                            "AND OK = 3")

                        Case "ProcessReject"
                            AdoCN.Execute("UPDATE tblExpRejExports " & _
                                          "SET OK = 1 " & _
                                          "WHERE ParNo = '" & flxDetails.Item(4, intRow).Value & "' " & _
                                            "AND OK = 0")

                        Case "GradingPCU_N"
                            AdoCN.Execute("UPDATE tblGrading_PackingListPCU " & _
                                          "SET OK = 1 " & _
                                          "WHERE Department = '" & cmbDepartment.Text & "' " & _
                                            "AND ParNo = '" & flxDetails.Item(4, intRow).Value & "' " & _
                                            "AND OrderNo = '" & flxDetails.Item(3, intRow).Value & "' " & _
                                            "AND RefNo = '" & flxDetails.Item(16, intRow).Value & "' " & _
                                            "AND PackNo = '" & flxDetails.Item(18, intRow).Value & "' " & _
                                            "AND OK = 0")

                            AdoCN.Execute("UPDATE tblPCUFinishOrders " & _
                                          "SET Status = 'E' " & _
                                          "WHERE OrderNo = '" & flxDetails.Item(3, intRow).Value & "' " & _
                                            "AND ParNo = '" & flxDetails.Item(4, intRow).Value & "' " & _
                                            "AND Reference = '" & flxDetails.Item(16, intRow).Value & "' " & _
                                            "AND (Status = 'A' OR Status = 'F')")

                        Case "Exports"
                            AdoCN.Execute("UPDATE tblAssortExportDetails " & _
                                          "SET Status = 1 " & _
                                          "WHERE Assortment = '" & flxDetails.Item(1, intRow).Value & "' " & _
                                            "AND ExpNo  = '" & flxDetails.Item(3, intRow).Value & "' " & _
                                            "AND (Export  = 1 OR Export  = 5)  " & _
                                            "AND Status = 0")

                        Case "Contract"
                            AdoCN.Execute("UPDATE dbo.tblParcelReturns " & _
                                          "SET OK = 1 " & _
                                          "WHERE ParcelNo  = '" & flxDetails.Item(4, intRow).Value & "' " & _
                                            "AND OK = 0")

                        Case "RoughSales"
                            AdoCN.Execute("UPDATE dbo.tblParcelRghSales " & _
                                          "SET OK = 1 " & _
                                          "WHERE ParcelNo  = '" & flxDetails.Item(4, intRow).Value & "' " & _
                                            "AND OK = 0")

                        Case "PolishBox"
                            AdoCN.Execute("UPDATE dbo.tblPOLSales " & _
                                          "SET OK = 1 " & _
                                          "WHERE SalesNo  = '" & flxDetails.Item(3, intRow).Value & "' AND Assortment2 = '" & flxDetails.Item(1, intRow).Value & "' " & _
                                            "AND OK = 0")

                        Case "PolishBoxTrf"
                            AdoCN.Execute("UPDATE dbo.tblPOLTransfer " & _
                                          "SET OK = 1 " & _
                                          "WHERE TransferNo  = '" & flxDetails.Item(3, intRow).Value & "' AND Assortment2 = '" & flxDetails.Item(1, intRow).Value & "' " & _
                                            "AND OK = 0")

                        Case "RoundsOrders"
                            AdoCN.Execute("UPDATE dbo.tblGrading_Box_Forever " & _
                                          "SET OK = 1 " & _
                                          "WHERE ParNo  = '" & flxDetails.Item(4, intRow).Value & "' " & _
                                            "AND OrderNo = '" & flxDetails.Item(3, intRow).Value & "' " & _
                                            "AND OK = 0")
                    End Select
                End If

                ExpProgress.Value = intRow + 1
                Application.DoEvents()
            Next
        Else
            If optEdit.Checked = True Then
                'For intRow = 0 To flxDetails.Rows.Count - 1

                'Next
            End If
        End If
        MsgBox("Successfully Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        txtOrder.Text = ""
        flxDetails.Rows.Clear()
        txtPcs.Text = "0"
        txtCts.Text = "0"
        txtRghPcs.Text = "0"
        txtRghCts.Text = "0"
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_SavedData()
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        mStrSQL = "SELECT Department,Assortment,Price,Reference1,Reference2,ExpPcs,ExpCts,StCt,Charges,Request," & _
                    "RoughPcs,RoughCts,Yield,TransferedCts,Send1,RecordNo,OrderRef,OrigAssort,InID,NLineNo,BasePrice,AdjPrice,APCUAssort,LotNo,OrderSide " & _
                  "FROM tblExportVarification WHERE (Status = 'A') AND Department ='" & cmbDepartment.Text & "'"
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            intNoOfRecords = rsComSql.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                intCounter = intCounter + 1
                flxDetails.Rows.Add(rsComSql.Fields("Department").Value, rsComSql.Fields("Assortment").Value, rsComSql.Fields("Price").Value,
                                    rsComSql.Fields("Reference1").Value, rsComSql.Fields("Reference2").Value, rsComSql.Fields("ExpPcs").Value,
                                    rsComSql.Fields("ExpCts").Value, rsComSql.Fields("StCt").Value, rsComSql.Fields("Charges").Value,
                                    rsComSql.Fields("Request").Value, rsComSql.Fields("RoughPcs").Value, rsComSql.Fields("RoughCts").Value,
                                    rsComSql.Fields("Yield").Value, rsComSql.Fields("TransferedCts").Value, rsComSql.Fields("Send1").Value,
                                    rsComSql.Fields("RecordNo").Value, rsComSql.Fields("OrderRef").Value, rsComSql.Fields("OrigAssort").Value,
                                    rsComSql.Fields("InID").Value, rsComSql.Fields("NLineNo").Value, rsComSql.Fields("BasePrice").Value,
                                    rsComSql.Fields("AdjPrice").Value, rsComSql.Fields("APCUAssort").Value, rsComSql.Fields("LotNo").Value,
                                    rsComSql.Fields("OrderSide").Value)
                rsComSql.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        rsComSql = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_ExportInfo()
        flxDetails.Rows.Clear()
        Select Case cmbDepartment.Text
            Case "Baguettes"
                Load_BaguettesExpInfo()
            Case "Baguettes2"
                Load_Baguettes2ExpInfo()
            Case "Baguettes3"
                Load_Baguettes3ExpInfo()
            Case "Carrer"
                Load_CarrerExpInfo()
            Case "Asscher"
                Load_AsscherExpInfo()
            Case "Radiant"
                Load_RadiantExpInfo()
            Case "Colombo Niru"
                Load_ColomboExpInfo()
            Case "Contract"
                Load_ContractExpInfo()
            Case "Davinci"
                Load_DavinciExpInfo()
            Case "Emerald"
                Load_EmeraldExpInfo()
            Case "Emerald2"
                Load_Emerald2ExpInfo()
            Case "Emerald3"
                Load_Emerald3ExpInfo()
            Case "Exports"
                Load_ExportsExpInfo()
            Case "Lamour"
                Load_LamourExpInfo()
            Case "Mix"
                Load_MixExpInfo()
            Case "MixRefer"
                Load_MixReferExpInfo()
            Case "KIT Box"
                Load_KitExpInfo()
            Case "Opening"
                Load_OpeningExpInfo()
            Case "PolishBox"
                Load_PolishBoxExpInfo()
            Case "PolishBoxTrf"
                Load_PolishBoxTrfExpInfo()
            Case "Precision"
                Load_PrecisionExpInfo()
            Case "Princess"
                Load_PrincessExpInfo()
            Case "Princess2"
                Load_Princess2ExpInfo()
            Case "ProcessReject"
                Load_ProcessRejectExpInfo()
            Case "RoughSales"
                Load_RoughSalesExpInfo()
            Case "Rounds"
                Load_RoundsExpInfo()
            Case "Rounds4"
                Load_Rounds4ExpInfo()
            Case "RoundsOrders"
                Load_RoundsOrdersExpInfo()
            Case "RoundsNLE"
                Load_RoundsNLEExpInfo()
            Case "GradingPCU_N"
                Load_GradingPCU_NExpInfo()
            Case "SizeExports"
                Load_SizeExportsExpInfo()
        End Select
    End Sub

    Private Sub Load_BaguettesExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim intFinPcs As Integer
        Dim dblFinCts As Double
        Dim intPCUPcs As Integer
        Dim dblPCUCts As Double
        Dim intRghPcs As Integer
        Dim dblRghCts As Double
        Dim dblAvgRghPcs As Double
        Dim dblAvgRgh As Double
        Dim dblApcuPcs As Double
        Dim dblApcuCts As Double
        Dim strLotNo As String

        mStrSQL = "SELECT * FROM VW_BAG_FinishedParcels ORDER BY ParcelNo"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                'If Rs.Fields("ParcelNo").Value = "JB1268C" Then
                '    MsgBox(Rs.Fields("ParcelNo").Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                'End If

                intCounter = intCounter + 1
                vRateValue = PFGetValueCharges(Rs.Fields("RateCode").Value)
                vRateValue = Math.Round(vRateValue, 2)
                vStByCt = Math.Round(Rs.Fields("RoughPcs").Value / Rs.Fields("RoughCts").Value, 2)
                vYield = Math.Round((Rs.Fields("FinishedCts").Value / Rs.Fields("IssueCts").Value) * 100, 2)
                If Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value > 0 Then
                    dblAvgRgh = (Rs.Fields("RoughCts").Value - Rs.Fields("PCUCts").Value) / (Rs.Fields("RoughPcs").Value - Rs.Fields("PcuPcs").Value)
                Else
                    dblAvgRgh = Rs.Fields("RoughCts").Value / Rs.Fields("RoughPcs").Value
                End If

                dblApcuPcs = 0
                dblApcuCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(RetPcs) AS RetPcs, SUM(RetCts) AS RetCts " & _
                              "FROM dbo.tblExpSizingReturns " & _
                              "WHERE (Department = 'Baguettes') AND (ParNo = '" & Rs.Fields("ParcelNo").Value & "')", AdoCN, 1, 1)
                If Not IsDBNull(rsComSql.Fields("RetPcs").Value) Then
                    dblApcuPcs = rsComSql.Fields("RetPcs").Value
                    dblApcuCts = rsComSql.Fields("RetCts").Value
                End If
                rsComSql = Nothing

                If strRight(Rs.Fields("ParcelNo").Value, 1) = "N" Or strRight(Rs.Fields("ParcelNo").Value, 1) = "V" Then
                    intFinPcs = Rs.Fields("FinishedPcs").Value
                    dblFinCts = Rs.Fields("FinishedCts").Value
                Else
                    intFinPcs = Rs.Fields("ActPcs").Value
                    dblFinCts = Rs.Fields("ActCts").Value
                End If
                dblFinCts = Math.Round(dblFinCts, 3)

                intPCUPcs = 0
                dblPCUCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_ExpRghIssues3 WHERE Department = 'Baguettes' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = rsComSql.Fields("Pcs").Value
                    dblPCUCts = rsComSql.Fields("Cts").Value
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_GradingRghIssuesAll WHERE Department = 'Baguettes' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = intPCUPcs + rsComSql.Fields("IssPcs").Value
                    dblPCUCts = dblPCUCts + rsComSql.Fields("IssCts").Value
                End If
                rsComSql = Nothing

                intRghPcs = 0
                dblRghCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(RghPcs) AS RghPcs, ROUND(SUM(RghCts), 3) AS RghCts FROM tblGrading_CheckingReturns " & _
                              "WHERE (ParNo = '" & Rs.Fields("ParcelNo").Value & "') AND (Department = 'Baguettes')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("RghPcs").Value) Then
                        intRghPcs = rsComSql.Fields("RghPcs").Value
                        dblRghCts = rsComSql.Fields("RghCts").Value
                    End If
                End If
                rsComSql = Nothing

                If strRight(Rs.Fields("ParcelNo").Value, 1) = "S" Then
                    'dblAvgRghPcs = Rs.Fields("ActPcs").Value
                    dblAvgRghPcs = Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value - intPCUPcs - intRghPcs
                    'dblAvgRgh = Math.Round(Rs.Fields("RoughCts").Value - dblApcuCts, 3)
                    dblAvgRgh = Math.Round(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, 3)
                Else
                    dblAvgRghPcs = Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value - intPCUPcs - intRghPcs
                    dblAvgRgh = Math.Round(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, 3)
                End If

                strLotNo = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT LotNo FROM tblImport " & _
                              "WHERE (SupParcelNo = '" & Rs.Fields("OrigParcelNo").Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strLotNo = rsComSql.Fields("LotNo").Value
                End If
                rsComSql = Nothing

                If intFinPcs > 0 Then
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        dblAvgRghPcs, Format(dblAvgRgh, "#0.000"), vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                Else
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        dblAvgRghPcs, Format(dblAvgRgh, "#0.000"), vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                End If

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_Baguettes2ExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim intFinPcs As Integer
        Dim dblFinCts As Double
        Dim intPCUPcs As Integer
        Dim dblPCUCts As Double
        Dim intRghPcs As Integer
        Dim dblRghCts As Double
        Dim dblAvgRgh As Double
        Dim strLotNo As String

        mStrSQL = "SELECT * FROM VW_BAG2_FinishedParcels ORDER BY ParcelNo"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1
                vRateValue = PFGetValueCharges(Rs.Fields("RateCode").Value)
                vRateValue = Math.Round(vRateValue, 2)
                vStByCt = Math.Round(Rs.Fields("RoughPcs").Value / Rs.Fields("RoughCts").Value, 2)
                vYield = Math.Round((Rs.Fields("FinishedCts").Value / Rs.Fields("IssueCts").Value) * 100, 2)
                If Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value > 0 Then
                    dblAvgRgh = (Rs.Fields("RoughCts").Value - Rs.Fields("PCUCts").Value) / (Rs.Fields("RoughPcs").Value - Rs.Fields("PcuPcs").Value)
                Else
                    dblAvgRgh = Rs.Fields("RoughCts").Value / Rs.Fields("RoughPcs").Value
                End If

                If strRight(Rs.Fields("ParcelNo").Value, 1) = "N" Or strRight(Rs.Fields("ParcelNo").Value, 1) = "V" Then
                    intFinPcs = Rs.Fields("FinishedPcs").Value
                    dblFinCts = Rs.Fields("FinishedCts").Value
                Else
                    intFinPcs = Rs.Fields("ActPcs").Value
                    dblFinCts = Rs.Fields("ActCts").Value
                End If
                dblFinCts = Math.Round(dblFinCts, 3)

                intPCUPcs = 0
                dblPCUCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_ExpRghIssues3 WHERE Department = 'Baguettes2' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = rsComSql.Fields("Pcs").Value
                    dblPCUCts = rsComSql.Fields("Cts").Value
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_GradingRghIssuesAll WHERE Department = 'Baguettes2' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = intPCUPcs + rsComSql.Fields("IssPcs").Value
                    dblPCUCts = dblPCUCts + rsComSql.Fields("IssCts").Value
                End If
                rsComSql = Nothing

                intRghPcs = 0
                dblRghCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(RghPcs) AS RghPcs, ROUND(SUM(RghCts), 3) AS RghCts FROM tblGrading_CheckingReturns " & _
                              "WHERE (ParNo = '" & Rs.Fields("ParcelNo").Value & "') AND (Department = 'Baguettes2')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("RghPcs").Value) Then
                        intRghPcs = rsComSql.Fields("RghPcs").Value
                        dblRghCts = rsComSql.Fields("RghCts").Value
                    End If
                End If
                rsComSql = Nothing

                strLotNo = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT LotNo FROM tblImport " & _
                              "WHERE (SupParcelNo = '" & Rs.Fields("OrigParcelNo").Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strLotNo = rsComSql.Fields("LotNo").Value
                End If
                rsComSql = Nothing

                If intFinPcs > 0 Then
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value - intPCUPcs - intRghPcs,
                                        Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                Else
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        intFinPcs, Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                End If

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_Baguettes3ExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim intFinPcs As Integer
        Dim dblFinCts As Double
        Dim intPCUPcs As Integer
        Dim dblPCUCts As Double
        Dim intRghPcs As Integer
        Dim dblRghCts As Double
        Dim dblAvgRgh As Double
        Dim strLotNo As String

        mStrSQL = "SELECT * FROM VW_BAG3_FinishedParcels ORDER BY ParcelNo"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1
                vRateValue = PFGetValueCharges(Rs.Fields("RateCode").Value)
                vRateValue = Math.Round(vRateValue, 2)
                vStByCt = Math.Round(Rs.Fields("RoughPcs").Value / Rs.Fields("RoughCts").Value, 2)
                vYield = Math.Round((Rs.Fields("FinishedCts").Value / Rs.Fields("IssueCts").Value) * 100, 2)
                If Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value > 0 Then
                    dblAvgRgh = (Rs.Fields("RoughCts").Value - Rs.Fields("PCUCts").Value) / (Rs.Fields("RoughPcs").Value - Rs.Fields("PcuPcs").Value)
                Else
                    dblAvgRgh = Rs.Fields("RoughCts").Value / Rs.Fields("RoughPcs").Value
                End If

                If strRight(Rs.Fields("ParcelNo").Value, 1) = "N" Or strRight(Rs.Fields("ParcelNo").Value, 1) = "V" Then
                    intFinPcs = Rs.Fields("FinishedPcs").Value
                    dblFinCts = Rs.Fields("FinishedCts").Value
                Else
                    intFinPcs = Rs.Fields("ActPcs").Value
                    dblFinCts = Rs.Fields("ActCts").Value
                End If
                dblFinCts = Math.Round(dblFinCts, 3)

                intPCUPcs = 0
                dblPCUCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_ExpRghIssues3 WHERE Department = 'Baguettes3' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = rsComSql.Fields("Pcs").Value
                    dblPCUCts = rsComSql.Fields("Cts").Value
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_GradingRghIssuesAll WHERE Department = 'Baguettes3' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = intPCUPcs + rsComSql.Fields("IssPcs").Value
                    dblPCUCts = dblPCUCts + rsComSql.Fields("IssCts").Value
                End If
                rsComSql = Nothing

                intRghPcs = 0
                dblRghCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(RghPcs) AS RghPcs, ROUND(SUM(RghCts), 3) AS RghCts FROM tblGrading_CheckingReturns " & _
                              "WHERE (ParNo = '" & Rs.Fields("ParcelNo").Value & "') AND (Department = 'Baguettes3')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("RghPcs").Value) Then
                        intRghPcs = rsComSql.Fields("RghPcs").Value
                        dblRghCts = rsComSql.Fields("RghCts").Value
                    End If
                End If
                rsComSql = Nothing

                strLotNo = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT LotNo FROM tblImport " & _
                              "WHERE (SupParcelNo = '" & Rs.Fields("OrigParcelNo").Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strLotNo = rsComSql.Fields("LotNo").Value
                End If
                rsComSql = Nothing

                If intFinPcs > 0 Then
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value - intPCUPcs - intRghPcs,
                                        Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                Else
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        intFinPcs, Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                End If

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_CarrerExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim intFinPcs As Integer
        Dim dblFinCts As Double
        Dim intPCUPcs As Integer
        Dim dblPCUCts As Double
        Dim intRghPcs As Integer
        Dim dblRghCts As Double
        Dim dblAvgRgh As Double
        Dim strLotNo As String
        Dim dblAvgRghPcs As Double

        mStrSQL = "SELECT * FROM VW_CR_FinishedParcels ORDER BY ParcelNo"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                'If Rs.Fields("ParcelNo").Value = "JC1897C" Then
                '    MsgBox(Rs.Fields("ParcelNo").Value)
                'End If


                intCounter = intCounter + 1
                vRateValue = PFGetValueCharges(Rs.Fields("RateCode").Value)
                vRateValue = Math.Round(vRateValue, 2)
                vStByCt = Math.Round(Rs.Fields("RoughPcs").Value / Rs.Fields("RoughCts").Value, 2)
                vYield = Math.Round((Rs.Fields("FinishedCts").Value / Rs.Fields("IssueCts").Value) * 100, 2)
                If Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value > 0 Then
                    dblAvgRgh = (Rs.Fields("RoughCts").Value - Rs.Fields("PCUCts").Value) / (Rs.Fields("RoughPcs").Value - Rs.Fields("PcuPcs").Value)
                Else
                    dblAvgRgh = Rs.Fields("RoughCts").Value / Rs.Fields("RoughPcs").Value
                End If

                If strRight(Rs.Fields("ParcelNo").Value, 1) = "N" Or strRight(Rs.Fields("ParcelNo").Value, 1) = "V" Then
                    intFinPcs = Rs.Fields("FinishedPcs").Value
                    dblFinCts = Rs.Fields("FinishedCts").Value
                Else
                    intFinPcs = Rs.Fields("ActPcs").Value
                    dblFinCts = Rs.Fields("ActCts").Value
                End If
                dblFinCts = Math.Round(dblFinCts, 3)

                intPCUPcs = 0
                dblPCUCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_ExpRghIssues3 WHERE Department = 'Carrer' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = rsComSql.Fields("Pcs").Value
                    dblPCUCts = rsComSql.Fields("Cts").Value
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_GradingRghIssuesAll WHERE Department = 'Carrer' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = intPCUPcs + rsComSql.Fields("IssPcs").Value
                    dblPCUCts = dblPCUCts + rsComSql.Fields("IssCts").Value
                End If
                rsComSql = Nothing

                intRghPcs = 0
                dblRghCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(RghPcs) AS RghPcs, ROUND(SUM(RghCts), 3) AS RghCts FROM tblGrading_CheckingReturns " & _
                              "WHERE (ParNo = '" & Rs.Fields("ParcelNo").Value & "') AND (Department = 'Carrer')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("RghPcs").Value) Then
                        intRghPcs = rsComSql.Fields("RghPcs").Value
                        dblRghCts = rsComSql.Fields("RghCts").Value
                    End If
                End If
                rsComSql = Nothing

                strLotNo = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT LotNo FROM tblImport " & _
                              "WHERE (SupParcelNo = '" & Rs.Fields("OrigParcelNo").Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strLotNo = rsComSql.Fields("LotNo").Value
                End If
                rsComSql = Nothing

                dblAvgRghPcs = Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value - intPCUPcs - intRghPcs

                If intFinPcs > 0 Then
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value - intPCUPcs - intRghPcs,
                                        Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                Else
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        dblAvgRghPcs, Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                End If

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_AsscherExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim intFinPcs As Integer
        Dim dblFinCts As Double
        Dim intPCUPcs As Integer
        Dim dblPCUCts As Double
        Dim intRghPcs As Integer
        Dim dblRghCts As Double
        Dim dblAvgRgh As Double
        Dim strLotNo As String

        mStrSQL = "SELECT * FROM VW_Ext_FinishedParcels WHERE Depart = 'Asscher' ORDER BY ParcelNo"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1
                vRateValue = PFGetValueCharges(Rs.Fields("RateCode").Value)
                vRateValue = Math.Round(vRateValue, 2)
                vStByCt = Math.Round(Rs.Fields("RoughPcs").Value / Rs.Fields("RoughCts").Value, 2)
                vYield = Math.Round((Rs.Fields("FinishedCts").Value / Rs.Fields("IssueCts").Value) * 100, 2)
                If Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value > 0 Then
                    dblAvgRgh = (Rs.Fields("RoughCts").Value - Rs.Fields("PCUCts").Value) / (Rs.Fields("RoughPcs").Value - Rs.Fields("PcuPcs").Value)
                Else
                    dblAvgRgh = Rs.Fields("RoughCts").Value / Rs.Fields("RoughPcs").Value
                End If

                If strRight(Rs.Fields("ParcelNo").Value, 1) = "N" Or strRight(Rs.Fields("ParcelNo").Value, 1) = "V" Then
                    intFinPcs = Rs.Fields("FinishedPcs").Value
                    dblFinCts = Rs.Fields("FinishedCts").Value
                Else
                    intFinPcs = Rs.Fields("ActPcs").Value
                    dblFinCts = Rs.Fields("ActCts").Value
                End If
                dblFinCts = Math.Round(dblFinCts, 3)

                intPCUPcs = 0
                dblPCUCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_ExpRghIssues3 WHERE Department = 'Asscher' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = rsComSql.Fields("Pcs").Value
                    dblPCUCts = rsComSql.Fields("Cts").Value
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_GradingRghIssuesAll WHERE Department = 'Asscher' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = intPCUPcs + rsComSql.Fields("IssPcs").Value
                    dblPCUCts = dblPCUCts + rsComSql.Fields("IssCts").Value
                End If
                rsComSql = Nothing

                intRghPcs = 0
                dblRghCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(RghPcs) AS RghPcs, ROUND(SUM(RghCts), 3) AS RghCts FROM tblGrading_CheckingReturns " & _
                              "WHERE (ParNo = '" & Rs.Fields("ParcelNo").Value & "') AND (Department = 'Asscher')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("RghPcs").Value) Then
                        intRghPcs = rsComSql.Fields("RghPcs").Value
                        dblRghCts = rsComSql.Fields("RghCts").Value
                    End If
                End If
                rsComSql = Nothing

                strLotNo = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT LotNo FROM tblImport " & _
                              "WHERE (SupParcelNo = '" & Rs.Fields("OrigParcelNo").Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strLotNo = rsComSql.Fields("LotNo").Value
                End If
                rsComSql = Nothing

                If intFinPcs > 0 Then
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value - intPCUPcs - intRghPcs,
                                        Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                Else
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        intFinPcs, Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                End If

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_RadiantExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim intFinPcs As Integer
        Dim dblFinCts As Double
        Dim intPCUPcs As Integer
        Dim dblPCUCts As Double
        Dim intRghPcs As Integer
        Dim dblRghCts As Double
        Dim dblAvgRgh As Double
        Dim strLotNo As String

        mStrSQL = "SELECT * FROM VW_Ext_FinishedParcels WHERE Depart = 'Radiant' ORDER BY ParcelNo"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1
                vRateValue = PFGetValueCharges(Rs.Fields("RateCode").Value)
                vRateValue = Math.Round(vRateValue, 2)
                vStByCt = Math.Round(Rs.Fields("RoughPcs").Value / Rs.Fields("RoughCts").Value, 2)
                vYield = Math.Round((Rs.Fields("FinishedCts").Value / Rs.Fields("IssueCts").Value) * 100, 2)
                If Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value > 0 Then
                    dblAvgRgh = (Rs.Fields("RoughCts").Value - Rs.Fields("PCUCts").Value) / (Rs.Fields("RoughPcs").Value - Rs.Fields("PcuPcs").Value)
                Else
                    dblAvgRgh = Rs.Fields("RoughCts").Value / Rs.Fields("RoughPcs").Value
                End If

                If strRight(Rs.Fields("ParcelNo").Value, 1) = "N" Or strRight(Rs.Fields("ParcelNo").Value, 1) = "V" Then
                    intFinPcs = Rs.Fields("FinishedPcs").Value
                    dblFinCts = Rs.Fields("FinishedCts").Value
                Else
                    intFinPcs = Rs.Fields("ActPcs").Value
                    dblFinCts = Rs.Fields("ActCts").Value
                End If
                dblFinCts = Math.Round(dblFinCts, 3)

                intPCUPcs = 0
                dblPCUCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_ExpRghIssues3 WHERE Department = 'Radiant' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = rsComSql.Fields("Pcs").Value
                    dblPCUCts = rsComSql.Fields("Cts").Value
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_GradingRghIssuesAll WHERE Department = 'Radiant' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = intPCUPcs + rsComSql.Fields("IssPcs").Value
                    dblPCUCts = dblPCUCts + rsComSql.Fields("IssCts").Value
                End If
                rsComSql = Nothing

                intRghPcs = 0
                dblRghCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(RghPcs) AS RghPcs, ROUND(SUM(RghCts), 3) AS RghCts FROM tblGrading_CheckingReturns " & _
                              "WHERE (ParNo = '" & Rs.Fields("ParcelNo").Value & "') AND (Department = 'Radiant')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("RghPcs").Value) Then
                        intRghPcs = rsComSql.Fields("RghPcs").Value
                        dblRghCts = rsComSql.Fields("RghCts").Value
                    End If
                End If
                rsComSql = Nothing

                strLotNo = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT LotNo FROM tblImport " & _
                              "WHERE (SupParcelNo = '" & Rs.Fields("OrigParcelNo").Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strLotNo = rsComSql.Fields("LotNo").Value
                End If
                rsComSql = Nothing

                If intFinPcs > 0 Then
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value - intPCUPcs - intRghPcs,
                                        Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                Else
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        intFinPcs, Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                End If

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_ColomboExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double
        Dim strLotNo As String
        Dim dblExpPcs As Double
        Dim dblExpCts As Double
        Dim dblRghPcs As Double
        Dim dblRghCts As Double
        Dim dblRefPcs As Double
        Dim dblRefCts As Double
        Dim dblRef3Pcs As Double
        Dim dblRef3Cts As Double
        Dim dblSynPcs As Double
        Dim dblSynCts As Double
        Dim strNewDCLRef As String
        Dim intClientPcs As Double
        Dim dblClientCts As Double

        Dim intParcelCount As Integer

        mStrSQL = "SELECT Assortment, AssPrice, SuppRef, DCLRef, SUM(FinishedPcs) AS FinishedPcs, SUM(FinishedCts) AS FinishedCts, RateCode, SUM(IssuePcs) AS IssuePcs, " & _
                        "SUM(IssueCts) AS IssueCts, Export, MAX(RecordNo) AS RecordNo " & _
                  "FROM tblExtFinishParcels " & _
                  "WHERE (Status = 'A') AND (Export = 1) AND (Department = 'Colombo Niru') " & _
                  "GROUP BY Assortment, AssPrice, SuppRef, DCLRef, RateCode, Export ORDER BY DCLRef"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1
                vRateValue = PFGetValueCharges(Rs.Fields("RateCode").Value)
                vRateValue = Math.Round(vRateValue, 2)
                vStByCt = Math.Round(Rs.Fields("IssuePcs").Value / Rs.Fields("IssueCts").Value, 2)
                vYield = Math.Round((Rs.Fields("FinishedCts").Value / Rs.Fields("IssueCts").Value) * 100, 2)

                'If Rs.Fields("DCLRef").Value = "TR4303CN" Then
                '    MsgBox(Rs.Fields("DCLRef").Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                'End If

                strLotNo = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT LotNo FROM tblImport " & _
                              "WHERE (SupParcelNo = '" & Rs.Fields("SuppRef").Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strLotNo = rsComSql.Fields("LotNo").Value
                End If
                rsComSql = Nothing

                dblRghPcs = Rs.Fields("IssuePcs").Value
                dblRghCts = Rs.Fields("IssueCts").Value

                dblExpPcs = 0
                dblExpCts = 0
                intParcelCount = 0
                rsComSql = New ADODB.Recordset
                'rsComSql.Open("SELECT SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts FROM dbo.VW_GradingPackingListCOLM WHERE (Assortment <> 'TREFER1' AND Assortment <> 'TREFER3' AND Assortment <> 'TSYNTHETIC') AND (LotNo = '" & strLotNo & "')", AdoCN, 1, 1)
                rsComSql.Open("SELECT PackNo, SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts FROM dbo.VW_GradingPackingListCOLM WHERE (Assortment <> 'TREFER1' AND Assortment <> 'TREFER3' AND Assortment <> 'TREFER4' AND Assortment <> 'TSYNTHETIC') AND (LotNo = '" & strLotNo & "') GROUP BY PackNo ORDER BY PackNo", AdoCN, 1, 1)
                If rsComSql.RecordCount = 1 Then
                    If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                        dblExpPcs = rsComSql.Fields("Pcs").Value
                        dblExpCts = rsComSql.Fields("Cts").Value
                    End If

                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                    Rs.Fields("SuppRef").Value, Rs.Fields("DCLRef").Value, dblExpPcs, Format(dblExpCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                    dblExpPcs, Format(dblExpCts, "#0.000"), vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                    intParcelCount = intParcelCount + 1
                Else
                    While Not rsComSql.EOF
                        If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                            dblExpPcs = rsComSql.Fields("Pcs").Value
                            dblExpCts = rsComSql.Fields("Cts").Value
                        End If

                        strNewDCLRef = Rs.Fields("DCLRef").Value
                        If intParcelCount > 0 Then
                            strNewDCLRef = Mid(Rs.Fields("DCLRef").Value, 1, 6) & "F" & strRight(Rs.Fields("DCLRef").Value, 1)
                        End If

                        flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("SuppRef").Value, strNewDCLRef, dblExpPcs, Format(dblExpCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        dblExpPcs, Format(dblExpCts, "#0.000"), vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)

                        intParcelCount = intParcelCount + 1
                        rsComSql.MoveNext()
                    End While

                End If
                'If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                '    dblExpPcs = rsComSql.Fields("Pcs").Value
                '    dblExpCts = rsComSql.Fields("Cts").Value
                'Else
                '    dblExpPcs = Rs.Fields("FinishedPcs").Value
                '    dblExpCts = Rs.Fields("FinishedCts").Value
                'End If
                rsComSql = Nothing

                If dblExpPcs > Rs.Fields("FinishedPcs").Value Then
                    dblExpPcs = Rs.Fields("FinishedPcs").Value
                    dblExpCts = Rs.Fields("FinishedCts").Value
                End If

                intClientPcs = 0
                dblClientCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(Pcs) AS IssPcs, ROUND(SUM(Cts), 3) AS IssCts " & _
                              "FROM tblGrading_Box_Forever " & _
                              "WHERE (ParNo + Grp = '" & Rs.Fields("DCLref").Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("IssPcs").Value) Then
                        intClientPcs = rsComSql.Fields("IssPcs").Value
                        dblClientCts = rsComSql.Fields("IssCts").Value
                    End If
                End If
                rsComSql = Nothing

                dblExpPcs = dblExpPcs - intClientPcs
                dblExpCts = dblExpCts - dblClientCts

                If dblExpCts > 0 Then
                    vStByCt = Math.Round(dblExpPcs / dblExpCts, 2)
                    vYield = Math.Round((dblExpCts / dblExpCts) * 100, 2)
                End If

                'Check Refer1 Pcs from the Lot
                dblRefPcs = 0
                dblRefCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts FROM dbo.VW_GradingPackingListCOLM WHERE (Assortment = 'TREFER1') AND (LotNo = '" & strLotNo & "')", AdoCN, 1, 1)
                If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                    dblRefPcs = rsComSql.Fields("Pcs").Value
                    dblRefCts = rsComSql.Fields("Cts").Value
                End If
                rsComSql = Nothing

                'Check Refer3 Pcs from the Lot
                dblRef3Pcs = 0
                dblRef3Cts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts FROM dbo.VW_GradingPackingListCOLM WHERE (Assortment = 'TREFER3') AND (LotNo = '" & strLotNo & "')", AdoCN, 1, 1)
                If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                    dblRef3Pcs = rsComSql.Fields("Pcs").Value
                    dblRef3Cts = rsComSql.Fields("Cts").Value
                End If
                rsComSql = Nothing

                'Check Synthetic Pcs from the Lot
                dblSynPcs = 0
                dblSynCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts FROM dbo.VW_GradingPackingListCOLM WHERE (Assortment = 'TSYNTHETIC') AND (LotNo = '" & strLotNo & "')", AdoCN, 1, 1)
                If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                    dblSynPcs = rsComSql.Fields("Pcs").Value
                    dblSynCts = rsComSql.Fields("Cts").Value
                End If
                rsComSql = Nothing

                dblRghPcs = dblRghPcs - (dblRefPcs + dblRef3Pcs + dblSynPcs + intClientPcs)
                dblRghCts = dblRghCts - (dblRefCts + dblRef3Cts + dblSynCts + dblClientCts)

                'flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                '                    Rs.Fields("SuppRef").Value, Rs.Fields("DCLRef").Value, dblExpPcs, Format(dblExpCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                '                    dblRghPcs, Format(dblRghCts, "#0.000"), vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)


                'Refer1
                If dblRefPcs > 0 Then
                    strNewDCLRef = Mid(Rs.Fields("DCLRef").Value, 1, 6) & "D" & strRight(Rs.Fields("DCLRef").Value, 1)
                    If dblExpCts > 0 Then
                        vStByCt = Math.Round(dblRefPcs / dblRefCts, 2)
                        vYield = Math.Round((dblRefCts / dblRefCts) * 100, 2)
                    End If

                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("SuppRef").Value, strNewDCLRef, dblRefPcs, Format(dblRefCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        dblRefPcs, Format(dblRefCts, "#0.000"), vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                End If

                'Refer3
                If dblRef3Pcs > 0 Then
                    strNewDCLRef = Mid(Rs.Fields("DCLRef").Value, 1, 6) & "G" & strRight(Rs.Fields("DCLRef").Value, 1)
                    If dblExpCts > 0 Then
                        vStByCt = Math.Round(dblRef3Pcs / dblRef3Cts, 2)
                        vYield = Math.Round((dblRef3Cts / dblRef3Cts) * 100, 2)
                    End If

                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("SuppRef").Value, strNewDCLRef, dblRef3Pcs, Format(dblRef3Cts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        dblRef3Pcs, Format(dblRef3Cts, "#0.000"), vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                End If

                'Synthetic
                If dblSynPcs > 0 Then
                    strNewDCLRef = Mid(Rs.Fields("DCLRef").Value, 1, 6) & "S" & strRight(Rs.Fields("DCLRef").Value, 1)
                    If dblExpCts > 0 Then
                        vStByCt = Math.Round(dblSynPcs / dblSynCts, 2)
                        vYield = Math.Round((dblSynCts / dblSynCts) * 100, 2)
                    End If

                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("SuppRef").Value, strNewDCLRef, dblSynPcs, Format(dblSynCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        dblSynPcs, Format(dblSynCts, "#0.000"), vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                End If

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_ContractExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim dblAvgCost As Double
        Dim strOrgAssort As String
        Dim strSupParNo As String
        Dim dblItemCost As Double
        Dim dblImportValue As Double
        Dim dblImportCts As Double
        Dim intSupCode As Integer
        Dim strRateCode As String
        Dim strLotNo As String

        mStrSQL = "SELECT TOP (100) PERCENT ParcelNo AS ParNo, SUM(PktPcs) AS Pcs, ROUND(SUM(PktCts), 3) AS Cts, ROUND(SUM(PktCts * Price), 2) AS Value, Ok " & _
                  "FROM dbo.tblParcelReturns " & _
                  "GROUP BY ParcelNo, Ok " & _
                  "HAVING (Ok = 0)" & _
                  "ORDER BY ParcelNo"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1
                If Rs.Fields("Pcs").Value > 0 Then
                    dblItemCost = 0
                    dblImportValue = 0
                    dblImportCts = 0
                    strOrgAssort = ""
                    strLotNo = ""
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT SupParcelNo, AssortmentNo, ItemCost, InvCts, SupplierCode, LotNo FROM tblImport WHERE DCLParcelNo = '" & Mid(Rs.Fields("ParNo").Value, 1, 6) & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        strSupParNo = rsComSql.Fields("SupParcelNo").Value
                        strOrgAssort = rsComSql.Fields("AssortmentNo").Value
                        dblItemCost = rsComSql.Fields("ItemCost").Value
                        dblImportValue = rsComSql.Fields("ItemCost").Value * rsComSql.Fields("INVCts").Value
                        dblImportCts = rsComSql.Fields("INVCts").Value
                        intSupCode = rsComSql.Fields("SupplierCode").Value
                        strLotNo = rsComSql.Fields("LotNo").Value
                    Else
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT SupParcelNo, AssortmentNo, ItemCost, InvCts, SupplierCode FROM tblDep_Trf WHERE DCLParcelNo = '" & Mid(Rs.Fields("ParNo").Value, 1, 6) & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            strSupParNo = rsComSql_1.Fields("SupParcelNo").Value
                            strOrgAssort = rsComSql_1.Fields("AssortmentNo").Value
                            dblItemCost = rsComSql_1.Fields("ItemCost").Value
                            dblImportValue = rsComSql_1.Fields("ItemCost").Value * rsComSql_1.Fields("INVCts").Value
                            dblImportCts = rsComSql_1.Fields("INVCts").Value
                            intSupCode = rsComSql_1.Fields("SupplierCode").Value
                        Else
                            rsComSql_2 = New ADODB.Recordset
                            rsComSql_2.Open("SELECT SupParcelNo, AssortmentNo, ItemCost, InvCts, SupplierCode, LotNo FROM tblImport WHERE DCLParcelNo = '" & Rs.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                            If rsComSql_2.RecordCount Then
                                strSupParNo = rsComSql_2.Fields("SupParcelNo").Value
                                strOrgAssort = rsComSql_2.Fields("AssortmentNo").Value
                                dblItemCost = rsComSql_2.Fields("ItemCost").Value
                                dblImportValue = rsComSql_2.Fields("ItemCost").Value * rsComSql_2.Fields("INVCts").Value
                                dblImportCts = rsComSql_2.Fields("INVCts").Value
                                intSupCode = rsComSql_2.Fields("SupplierCode").Value
                                strLotNo = rsComSql.Fields("LotNo").Value
                            Else
                                strSupParNo = Mid(Rs.Fields("ParNo").Value, 1, 6)
                            End If
                            rsComSql_2 = Nothing
                        End If
                        rsComSql_1 = Nothing
                    End If
                    rsComSql = Nothing

                    strRateCode = ""

                    dblAvgCost = dblItemCost
                    dblAvgCost = Rs.Fields("Value").Value / Rs.Fields("Cts").Value
                    dblAvgCost = Math.Round(dblAvgCost, 2)

                    vRateValue = PFGetValueCharges(strRateCode)
                    vStByCt = Math.Round(Rs.Fields("Pcs").Value / Rs.Fields("Cts").Value, 2)
                    vYield = Math.Round((Rs.Fields("Cts").Value / Rs.Fields("Cts").Value) * 100, 2)
                    dblAvgCost = Math.Round(dblAvgCost, 2)

                    If strLotNo = "" Then
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT LotNo FROM tblImport " & _
                                      "WHERE (SupParcelNo = '" & strSupParNo & "')", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            strLotNo = rsComSql.Fields("LotNo").Value
                        End If
                        rsComSql = Nothing
                    End If

                    flxDetails.Rows.Add(cmbDepartment.Text, strOrgAssort, dblAvgCost, strSupParNo, Rs.Fields("ParNo").Value, Rs.Fields("Pcs").Value,
                                    Format(Rs.Fields("Cts").Value, "#0.000"), vStByCt, vRateValue, strRateCode,
                                    Rs.Fields("Pcs").Value, Format(Rs.Fields("Cts").Value, "#0.000"), vYield, "0", False, "0", "0", strOrgAssort, "0", "0",
                                    dblItemCost, dblItemCost, "", strLotNo)
                End If

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_DavinciExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim intFinPcs As Integer
        Dim dblFinCts As Double
        Dim intPCUPcs As Integer
        Dim dblPCUCts As Double
        Dim intRghPcs As Integer
        Dim dblRghCts As Double
        Dim dblAvgRgh As Double
        Dim strLotNo As String

        mStrSQL = "SELECT * FROM VW_DV_FinishedParcels ORDER BY ParcelNo"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1
                vRateValue = PFGetValueCharges(Rs.Fields("RateCode").Value)
                vRateValue = Math.Round(vRateValue, 2)
                vStByCt = Math.Round(Rs.Fields("RoughPcs").Value / Rs.Fields("RoughCts").Value, 2)
                vYield = Math.Round((Rs.Fields("FinishedCts").Value / Rs.Fields("IssueCts").Value) * 100, 2)
                If Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value > 0 Then
                    dblAvgRgh = (Rs.Fields("RoughCts").Value - Rs.Fields("PCUCts").Value) / (Rs.Fields("RoughPcs").Value - Rs.Fields("PcuPcs").Value)
                Else
                    dblAvgRgh = Rs.Fields("RoughCts").Value / Rs.Fields("RoughPcs").Value
                End If

                If strRight(Rs.Fields("ParcelNo").Value, 1) = "N" Or strRight(Rs.Fields("ParcelNo").Value, 1) = "V" Then
                    intFinPcs = Rs.Fields("FinishedPcs").Value
                    dblFinCts = Rs.Fields("FinishedCts").Value
                Else
                    intFinPcs = Rs.Fields("ActPcs").Value
                    dblFinCts = Rs.Fields("ActCts").Value
                End If
                dblFinCts = Math.Round(dblFinCts, 3)

                intPCUPcs = 0
                dblPCUCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_ExpRghIssues3 WHERE Department = 'Davinci' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = rsComSql.Fields("Pcs").Value
                    dblPCUCts = rsComSql.Fields("Cts").Value
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_GradingRghIssuesAll WHERE Department = 'Davinci' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = intPCUPcs + rsComSql.Fields("IssPcs").Value
                    dblPCUCts = dblPCUCts + rsComSql.Fields("IssCts").Value
                End If
                rsComSql = Nothing

                intRghPcs = 0
                dblRghCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(RghPcs) AS RghPcs, ROUND(SUM(RghCts), 3) AS RghCts FROM tblGrading_CheckingReturns " & _
                              "WHERE (ParNo = '" & Rs.Fields("ParcelNo").Value & "') AND (Department = 'Davinci')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("RghPcs").Value) Then
                        intRghPcs = rsComSql.Fields("RghPcs").Value
                        dblRghCts = rsComSql.Fields("RghCts").Value
                    End If
                End If
                rsComSql = Nothing

                strLotNo = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT LotNo FROM tblImport " & _
                              "WHERE (SupParcelNo = '" & Rs.Fields("OrigParcelNo").Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strLotNo = rsComSql.Fields("LotNo").Value
                End If
                rsComSql = Nothing

                If intFinPcs > 0 Then
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value - intPCUPcs - intRghPcs,
                                        Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                Else
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        intFinPcs, Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                End If

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_EmeraldExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim intFinPcs As Integer
        Dim dblFinCts As Double
        Dim intPCUPcs As Integer
        Dim dblPCUCts As Double
        Dim intRghPcs As Integer
        Dim dblRghCts As Double
        Dim dblAvgRgh As Double
        Dim strLotNo As String

        mStrSQL = "SELECT * FROM VW_EME_FinishedParcels ORDER BY ParcelNo"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                'If Rs.Fields("ParcelNo").Value = "JE1472A" Then
                '    MsgBox(Rs.Fields("ParcelNo").Value)
                'End If

                intCounter = intCounter + 1
                vRateValue = PFGetValueCharges(Rs.Fields("RateCode").Value)
                vRateValue = Math.Round(vRateValue, 2)
                vStByCt = Math.Round(Rs.Fields("RoughPcs").Value / Rs.Fields("RoughCts").Value, 2)
                vYield = Math.Round((Rs.Fields("FinishedCts").Value / Rs.Fields("IssueCts").Value) * 100, 2)
                If Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value > 0 Then
                    dblAvgRgh = (Rs.Fields("RoughCts").Value - Rs.Fields("PCUCts").Value) / (Rs.Fields("RoughPcs").Value - Rs.Fields("PcuPcs").Value)
                Else
                    dblAvgRgh = Rs.Fields("RoughCts").Value / Rs.Fields("RoughPcs").Value
                End If

                If strRight(Rs.Fields("ParcelNo").Value, 1) = "N" Or strRight(Rs.Fields("ParcelNo").Value, 1) = "V" Or strRight(Rs.Fields("ParcelNo").Value, 1) = "H" Or strRight(Rs.Fields("ParcelNo").Value, 1) = "T" Then
                    intFinPcs = Rs.Fields("FinishedPcs").Value
                    dblFinCts = Rs.Fields("FinishedCts").Value
                Else
                    intFinPcs = Rs.Fields("ActPcs").Value
                    dblFinCts = Rs.Fields("ActCts").Value
                End If
                dblFinCts = Math.Round(dblFinCts, 3)

                intPCUPcs = 0
                dblPCUCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_ExpRghIssues3 WHERE Department = 'Emerald' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = rsComSql.Fields("Pcs").Value
                    dblPCUCts = rsComSql.Fields("Cts").Value
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_GradingRghIssuesAll WHERE Department = 'Emerald' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = intPCUPcs + rsComSql.Fields("IssPcs").Value
                    dblPCUCts = dblPCUCts + rsComSql.Fields("IssCts").Value
                End If
                rsComSql = Nothing

                intRghPcs = 0
                dblRghCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(RghPcs) AS RghPcs, ROUND(SUM(RghCts), 3) AS RghCts FROM tblGrading_CheckingReturns " & _
                              "WHERE (ParNo = '" & Rs.Fields("ParcelNo").Value & "') AND (Department = 'Emerald')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("RghPcs").Value) Then
                        intRghPcs = rsComSql.Fields("RghPcs").Value
                        dblRghCts = rsComSql.Fields("RghCts").Value
                    End If
                End If
                rsComSql = Nothing

                strLotNo = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT LotNo FROM tblImport " & _
                              "WHERE (SupParcelNo = '" & Rs.Fields("OrigParcelNo").Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strLotNo = rsComSql.Fields("LotNo").Value
                End If
                rsComSql = Nothing

                If intFinPcs > 0 Then
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value - intPCUPcs - intRghPcs,
                                        Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                Else
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        intFinPcs, Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                End If

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_Emerald2ExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim intFinPcs As Integer
        Dim dblFinCts As Double
        Dim intPCUPcs As Integer
        Dim dblPCUCts As Double
        Dim intRghPcs As Integer
        Dim dblRghCts As Double
        Dim dblAvgRgh As Double
        Dim strLotNo As String

        mStrSQL = "SELECT * FROM VW_EME2_FinishedParcels ORDER BY ParcelNo"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1
                vRateValue = PFGetValueCharges(Rs.Fields("RateCode").Value)
                vRateValue = Math.Round(vRateValue, 2)
                vStByCt = Math.Round(Rs.Fields("RoughPcs").Value / Rs.Fields("RoughCts").Value, 2)
                vYield = Math.Round((Rs.Fields("FinishedCts").Value / Rs.Fields("IssueCts").Value) * 100, 2)
                If Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value > 0 Then
                    dblAvgRgh = (Rs.Fields("RoughCts").Value - Rs.Fields("PCUCts").Value) / (Rs.Fields("RoughPcs").Value - Rs.Fields("PcuPcs").Value)
                Else
                    dblAvgRgh = Rs.Fields("RoughCts").Value / Rs.Fields("RoughPcs").Value
                End If

                If strRight(Rs.Fields("ParcelNo").Value, 1) = "N" Or strRight(Rs.Fields("ParcelNo").Value, 1) = "V" Then
                    intFinPcs = Rs.Fields("FinishedPcs").Value
                    dblFinCts = Rs.Fields("FinishedCts").Value
                Else
                    intFinPcs = Rs.Fields("ActPcs").Value
                    dblFinCts = Rs.Fields("ActCts").Value
                End If
                dblFinCts = Math.Round(dblFinCts, 3)

                intPCUPcs = 0
                dblPCUCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_ExpRghIssues3 WHERE Department = 'Emerald2' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = rsComSql.Fields("Pcs").Value
                    dblPCUCts = rsComSql.Fields("Cts").Value
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_GradingRghIssuesAll WHERE Department = 'Emerald2' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = intPCUPcs + rsComSql.Fields("IssPcs").Value
                    dblPCUCts = dblPCUCts + rsComSql.Fields("IssCts").Value
                End If
                rsComSql = Nothing

                intRghPcs = 0
                dblRghCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(RghPcs) AS RghPcs, ROUND(SUM(RghCts), 3) AS RghCts FROM tblGrading_CheckingReturns " & _
                              "WHERE (ParNo = '" & Rs.Fields("ParcelNo").Value & "') AND (Department = 'Emerald2')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("RghPcs").Value) Then
                        intRghPcs = rsComSql.Fields("RghPcs").Value
                        dblRghCts = rsComSql.Fields("RghCts").Value
                    End If
                End If
                rsComSql = Nothing

                strLotNo = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT LotNo FROM tblImport " & _
                              "WHERE (SupParcelNo = '" & Rs.Fields("OrigParcelNo").Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strLotNo = rsComSql.Fields("LotNo").Value
                End If
                rsComSql = Nothing

                If intFinPcs > 0 Then
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value - intPCUPcs - intRghPcs,
                                        Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                Else
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        intFinPcs, Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                End If

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_Emerald3ExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim intFinPcs As Integer
        Dim dblFinCts As Double
        Dim intPCUPcs As Integer
        Dim dblPCUCts As Double
        Dim intRghPcs As Integer
        Dim dblRghCts As Double
        Dim dblAvgRgh As Double
        Dim strLotNo As String

        mStrSQL = "SELECT * FROM VW_EME3_FinishedParcels ORDER BY ParcelNo"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1
                vRateValue = PFGetValueCharges(Rs.Fields("RateCode").Value)
                vRateValue = Math.Round(vRateValue, 2)
                vStByCt = Math.Round(Rs.Fields("RoughPcs").Value / Rs.Fields("RoughCts").Value, 2)
                vYield = Math.Round((Rs.Fields("FinishedCts").Value / Rs.Fields("IssueCts").Value) * 100, 2)
                If Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value > 0 Then
                    dblAvgRgh = (Rs.Fields("RoughCts").Value - Rs.Fields("PCUCts").Value) / (Rs.Fields("RoughPcs").Value - Rs.Fields("PcuPcs").Value)
                Else
                    dblAvgRgh = Rs.Fields("RoughCts").Value / Rs.Fields("RoughPcs").Value
                End If

                If strRight(Rs.Fields("ParcelNo").Value, 1) = "N" Or strRight(Rs.Fields("ParcelNo").Value, 1) = "V" Then
                    intFinPcs = Rs.Fields("FinishedPcs").Value
                    dblFinCts = Rs.Fields("FinishedCts").Value
                Else
                    intFinPcs = Rs.Fields("ActPcs").Value
                    dblFinCts = Rs.Fields("ActCts").Value
                End If
                dblFinCts = Math.Round(dblFinCts, 3)

                intPCUPcs = 0
                dblPCUCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_ExpRghIssues3 WHERE Department = 'Emerald3' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = rsComSql.Fields("Pcs").Value
                    dblPCUCts = rsComSql.Fields("Cts").Value
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_GradingRghIssuesAll WHERE Department = 'Emerald3' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = intPCUPcs + rsComSql.Fields("IssPcs").Value
                    dblPCUCts = dblPCUCts + rsComSql.Fields("IssCts").Value
                End If
                rsComSql = Nothing

                intRghPcs = 0
                dblRghCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(RghPcs) AS RghPcs, ROUND(SUM(RghCts), 3) AS RghCts FROM tblGrading_CheckingReturns " & _
                              "WHERE (ParNo = '" & Rs.Fields("ParcelNo").Value & "') AND (Department = 'Emerald3')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("RghPcs").Value) Then
                        intRghPcs = rsComSql.Fields("RghPcs").Value
                        dblRghCts = rsComSql.Fields("RghCts").Value
                    End If
                End If
                rsComSql = Nothing

                strLotNo = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT LotNo FROM tblImport " & _
                              "WHERE (SupParcelNo = '" & Rs.Fields("OrigParcelNo").Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strLotNo = rsComSql.Fields("LotNo").Value
                End If
                rsComSql = Nothing

                If intFinPcs > 0 Then
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value - intPCUPcs - intRghPcs,
                                        Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                Else
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        intFinPcs, Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                End If

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_ExportsExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim dblAvgCost As Double
        Dim dblBaseCost As Double

        mStrSQL = "SELECT TOP (100) PERCENT dbo.tblAssortExportDetails.ExpNo, dbo.tblAssortExportDetails.Assortment, dbo.tblAssortExportDetails.OrgAssort, SUM(dbo.tblAssortExportDetails.Pcs) AS Pcs, " & _
                      "ROUND(SUM(dbo.tblAssortExportDetails.Cts), 3) AS Cts, ROUND(SUM(dbo.tblAssortExportDetails.ActCts), 3) AS ActCts, dbo.tblAssortList.CurrentCost AS BaseCost, dbo.tblAssortList.StonePrice, dbo.tblAssortList.AvgCost, dbo.tblAssortList.AvgStonePrice " & _
                  "FROM dbo.tblAssortExportDetails INNER JOIN dbo.tblAssortList ON dbo.tblAssortExportDetails.Assortment = dbo.tblAssortList.Assortment " & _
                  "WHERE (dbo.tblAssortExportDetails.Status = 0) AND (dbo.tblAssortExportDetails.Export = 1 OR dbo.tblAssortExportDetails.Export = 5) " & _
                  "GROUP BY dbo.tblAssortExportDetails.ExpNo, dbo.tblAssortExportDetails.Assortment, dbo.tblAssortExportDetails.OrgAssort, dbo.tblAssortList.CurrentCost, dbo.tblAssortList.StonePrice, dbo.tblAssortList.AvgCost, dbo.tblAssortList.AvgStonePrice " & _
                  "ORDER BY dbo.tblAssortExportDetails.ExpNo, dbo.tblAssortExportDetails.Assortment"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1
                If Rs.Fields("Pcs").Value > 0 Then
                    dblAvgCost = 0
                    dblBaseCost = 0

                    If Mid(Rs.Fields("Assortment").Value, 1, 1) = "S" Then
                        dblBaseCost = Math.Round((Rs.Fields("StonePrice").Value * 1.3 * Rs.Fields("Pcs").Value) / (Rs.Fields("Cts").Value), 2)
                        'dblAvgCost = Math.Round((Rs.Fields("AvgStonePrice").Value * Rs.Fields("Pcs").Value) / (Rs.Fields("Cts").Value), 2)
                        dblAvgCost = Math.Round(Rs.Fields("AvgCost").Value, 2)
                    Else
                        dblBaseCost = Rs.Fields("BaseCost").Value
                        dblAvgCost = Math.Round(Rs.Fields("AvgCost").Value, 2)
                    End If

                    vRateValue = 0
                    vStByCt = Math.Round(Rs.Fields("Pcs").Value / Rs.Fields("Cts").Value, 2)
                    vYield = Math.Round((Rs.Fields("Cts").Value / Rs.Fields("Cts").Value) * 100, 2)

                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, dblAvgCost, Rs.Fields("ExpNo").Value, "00", Rs.Fields("Pcs").Value,
                                        Format(Rs.Fields("ActCts").Value, "#0.000"), vStByCt, vRateValue, "0", Rs.Fields("Pcs").Value, Format(Rs.Fields("Cts").Value, "#0.000"),
                                        vYield, "0", False, "0", "0", Rs.Fields("OrgAssort").Value, "0", "0", dblBaseCost, dblAvgCost)
                End If

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_SizeExportsExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim dblBaseCost As Double
        Dim dblAvgCost As Double
        Dim strSupParNo As String
        Dim strOrigAssort As String
        Dim strCategory As String

        Dim dblLotNo As Double
        Dim dblImpPrice As Double

        If Len(txtOrder.Text) = 0 Then
            mStrSQL = "SELECT TOP (100) PERCENT Assortment, ParNo, OrgAssort, SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts, ROUND(SUM(EstCts), 3) AS EstCts, AdjPrice, SupParNo, BasePrice AS MarketPrice, PackNo " & _
                      "FROM dbo.tblExpReExports " & _
                      "WHERE (OK = 3) " & _
                      "GROUP BY Assortment, ParNo, OrgAssort, OK, AdjPrice, SupParNo, BasePrice, PackNo " & _
                      "ORDER BY SupParNo, ParNo, Assortment"
        Else
            mStrSQL = "SELECT TOP (100) PERCENT Assortment, ParNo, OrgAssort, SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts, ROUND(SUM(EstCts), 3) AS EstCts, AdjPrice, SupParNo, BasePrice AS MarketPrice, PackNo " & _
                      "FROM dbo.tblExpReExports " & _
                      "WHERE (OK = 3) AND (SupParNo = '" & txtOrder.Text & "') " & _
                      "GROUP BY Assortment, ParNo, OrgAssort, OK, AdjPrice, SupParNo, BasePrice, PackNo " & _
                      "ORDER BY SupParNo, ParNo, Assortment"
        End If

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1
                If Rs.Fields("Pcs").Value > 0 Then
                    strOrigAssort = Rs.Fields("OrgAssort").Value
                    strSupParNo = Rs.Fields("SupParNo").Value
                    strCategory = ""
                    dblImpPrice = 0

                    If strSupParNo = "" Then
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT SupParcelNo, ItemCost FROM tblImport WHERE DCLParcelNo = '" & Rs.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            strSupParNo = rsComSql.Fields("SupParcelNo").Value
                            dblImpPrice = rsComSql.Fields("ItemCost").Value
                        End If
                        rsComSql = Nothing
                    End If

                    If strOrigAssort = "" Then
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT SupParcelNo,AssortmentNo, ItemCost FROM tblImport WHERE DCLParcelNo = '" & Rs.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            strSupParNo = rsComSql.Fields("SupParcelNo").Value
                            strOrigAssort = rsComSql.Fields("AssortmentNo").Value
                            dblImpPrice = rsComSql.Fields("ItemCost").Value
                        End If
                        rsComSql = Nothing
                    End If

                    If strSupParNo = "" Then
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT SupParcelNo,AssortmentNo FROM tblDep_Trf WHERE DCLParcelNo = '" & Mid(Rs.Fields("ParNo").Value, 1, 6) & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            strSupParNo = rsComSql.Fields("SupParcelNo").Value
                            strOrigAssort = rsComSql.Fields("AssortmentNo").Value
                        End If
                        rsComSql = Nothing
                    End If

                    dblLotNo = 0
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT Category,LotNo,ItemCost FROM tblImport WHERE SupParcelNo = '" & strSupParNo & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        strCategory = rsComSql.Fields("Category").Value
                        dblLotNo = rsComSql.Fields("LotNo").Value
                        dblImpPrice = rsComSql.Fields("ItemCost").Value
                    End If
                    rsComSql = Nothing

                    dblAvgCost = 0
                    vRateValue = 0
                    vStByCt = Math.Round(Rs.Fields("Pcs").Value / Rs.Fields("Cts").Value, 2)
                    vYield = Math.Round((Rs.Fields("Cts").Value / Rs.Fields("Cts").Value) * 100, 2)

                    If strCategory = "Purchased" Then
                        If Rs.Fields("MarketPrice").Value = 0 Then
                            dblBaseCost = Rs.Fields("MarketPrice").Value
                        Else
                            dblBaseCost = Rs.Fields("MarketPrice").Value
                        End If
                        If Rs.Fields("Cts").Value <> Rs.Fields("EstCts").Value Then
                            If Rs.Fields("EstCts").Value <> 0 Then
                                dblAvgCost = (dblBaseCost * Rs.Fields("EstCts").Value) / Rs.Fields("Cts").Value
                            Else
                                dblAvgCost = dblBaseCost
                            End If
                        Else
                            dblAvgCost = dblBaseCost
                        End If
                    Else
                        dblAvgCost = dblImpPrice
                        dblBaseCost = Rs.Fields("MarketPrice").Value
                    End If
                    dblBaseCost = Math.Round(dblBaseCost, 2)
                    dblAvgCost = Math.Round(dblAvgCost, 2)

                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, dblAvgCost, strSupParNo, Rs.Fields("ParNo").Value, Rs.Fields("Pcs").Value, _
                                        Format(Rs.Fields("Cts").Value, "#0.000"), vStByCt, vRateValue, "0", Rs.Fields("Pcs").Value, Format(Rs.Fields("Cts").Value, "#0.000"), _
                                        vYield, "0", False, "0", "0", strOrigAssort, Rs.Fields("PackNo").Value, "0", dblBaseCost, dblAvgCost, "", dblLotNo)
                End If

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_MixExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim dblAvgCost As Double

        mStrSQL = "SELECT TOP (100) PERCENT dbo.tblMixExportOrders.Assortment, MAX(dbo.tblMixExportOrders.AssPrice) AS AssPrice, dbo.tblMixExportOrders.OrderNo, " & _
                      "SUM(dbo.tblMixExportOrders.FinishedPcs) AS FinishedPcs, SUM(dbo.tblMixExportOrders.FinishedCts) AS FinishedCts, " & _
                      "SUM(dbo.tblMixExportOrders.IssuePcs) AS IssuePcs, SUM(dbo.tblMixExportOrders.IssueCts) AS IssueCts, MAX(dbo.tblMixExportOrders.RateCode) AS RateCode, " & _
                      "MAX(dbo.tblMixExportOrders.Export) AS Export, MAX(dbo.tblMixExportOrders.RecordNo) AS RecordNo, dbo.tblMixPacket.PktRefNo, dbo.tblMixPacket.Pktside, dbo.tblMixExportOrders.OrigAssort, " & _
                      "dbo.tblMixExportOrders.NLineNo, dbo.tblAssortList.MarketPrice, dbo.tblAssortList.CurrentCost, dbo.tblAssortList.AvgCost, SUM(dbo.tblMixExportOrders.GrPcs) AS GrPcs " & _
                  "FROM dbo.tblMixExportOrders INNER JOIN dbo.tblMixPacket ON dbo.tblMixExportOrders.OrderNo = dbo.tblMixPacket.PktOrdNo AND dbo.tblMixExportOrders.PacketNo = dbo.tblMixPacket.PktNo INNER JOIN " & _
                      "dbo.tblAssortList ON dbo.tblMixExportOrders.Assortment = dbo.tblAssortList.Assortment " & _
                  "WHERE (dbo.tblMixExportOrders.Status = 'A') AND (dbo.tblMixExportOrders.Export = 1) " & _
                  "GROUP BY dbo.tblMixExportOrders.Assortment, dbo.tblMixExportOrders.OrigAssort, dbo.tblMixExportOrders.OrderNo, " & _
                      "dbo.tblMixPacket.PktRefNo, dbo.tblMixPacket.Pktside, dbo.tblMixExportOrders.NLineNo, dbo.tblAssortList.MarketPrice, dbo.tblAssortList.CurrentCost, dbo.tblAssortList.AvgCost " & _
                  "ORDER BY dbo.tblMixExportOrders.OrderNo, dbo.tblMixExportOrders.Assortment"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1
                If Rs.Fields("IssuePcs").Value > 0 Then

                    vRateValue = PFGetValueCharges(Rs.Fields("RateCode").Value)
                    vStByCt = Math.Round(Rs.Fields("IssuePcs").Value / Rs.Fields("IssueCts").Value, 2)
                    vYield = Math.Round((Rs.Fields("FinishedCts").Value / Rs.Fields("IssueCts").Value) * 100, 2)
                    dblAvgCost = Rs.Fields("AssPrice").Value

                    'If Mid(Rs.Fields("Assortment").Value, 1, 2) = "VM" Or Mid(Rs.Fields("Assortment").Value, 1, 2) = "VP" Then
                    '    'rsComSql = New ADODB.Recordset
                    '    'rsComSql.Open("SELECT SUM(InCts * CurCost) / SUM(InCts) AS AvgPrice FROM tblAssortDetails WHERE (Assortment = '" & Rs.Fields("Assortment").Value & "')", AdoCN, 1, 1)
                    '    'If Not IsDBNull(rsComSql.Fields("AvgPrice").Value) Then
                    '    '    dblAvgCost = rsComSql.Fields("AvgPrice").Value
                    '    'End If
                    '    'rsComSql = Nothing
                    'End If

                    'rsComSql = New ADODB.Recordset
                    'rsComSql.Open("SELECT AvgCost FROM tblAssortList WHERE (Assortment = '" & Rs.Fields("Assortment").Value & "')", AdoCN, 1, 1)
                    'If rsComSql.RecordCount Then
                    '    dblAvgCost = rsComSql.Fields("AvgCost").Value
                    'End If
                    'rsComSql = Nothing
                    dblAvgCost = Math.Round(dblAvgCost, 2)

                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value, Rs.Fields("OrderNo").Value, "00", Rs.Fields("FinishedPcs").Value,
                                        Format(Rs.Fields("FinishedCts").Value, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        Rs.Fields("FinishedPcs").Value, Format(Rs.Fields("IssueCts").Value, "#0.000"), vYield, "0", False, "0", Rs.Fields("PktRefNo").Value, Rs.Fields("OrigAssort").Value, "0",
                                        Rs.Fields("NLineNo").Value, Rs.Fields("CurrentCost").Value, Rs.Fields("AvgCost").Value, "", "", Rs.Fields("Pktside").Value)
                End If

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_MixReferExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim dblAvgCost As Double

        mStrSQL = "SELECT TOP (100) PERCENT dbo.tblMixFinishOrdersR.OrderNo, dbo.tblMixFinishOrdersR.Reference, dbo.tblMixFinishOrdersR.Side, dbo.tblMixFinishOrdersR.Assortment, dbo.tblMixFinishOrdersR.AssPrice," & _
                    "dbo.tblMixFinishOrdersR.FinishedPcs, dbo.tblMixFinishOrdersR.FinishedCts, dbo.tblMixFinishOrdersR.PacketPcs, dbo.tblMixFinishOrdersR.PacketCts, dbo.tblMixFinishOrdersR.IssueCts," & _
                    "dbo.tblMixFinishOrdersR.RateCode, dbo.tblMixFinishOrdersR.Export, dbo.tblMixFinishOrdersR.Status, dbo.tblMixFinishOrdersR.RecordNo, dbo.tblMixFinishOrdersR.Subject," & _
                    "dbo.tblMixFinishOrdersR.NLineNo, dbo.tblAssortList.MarketPrice, dbo.tblAssortList.AvgCost " & _
                  "FROM dbo.tblMixFinishOrdersR INNER JOIN dbo.tblAssortList ON dbo.tblMixFinishOrdersR.Assortment = dbo.tblAssortList.Assortment " & _
                  "WHERE (dbo.tblMixFinishOrdersR.Status = 'A') " & _
                  "ORDER BY dbo.tblMixFinishOrdersR.OrderNo, dbo.tblMixFinishOrdersR.Assortment"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1
                If Rs.Fields("FinishedPcs").Value > 0 Then

                    vRateValue = PFGetValueCharges(Rs.Fields("RateCode").Value)
                    vStByCt = Math.Round(Rs.Fields("FinishedPcs").Value / Rs.Fields("IssueCts").Value, 2)
                    vYield = Math.Round((Rs.Fields("FinishedCts").Value / Rs.Fields("IssueCts").Value) * 100, 2)
                    dblAvgCost = Rs.Fields("MarketPrice").Value

                    dblAvgCost = Math.Round(dblAvgCost, 2)

                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value, Rs.Fields("OrderNo").Value, "00", Rs.Fields("FinishedPcs").Value,
                                        Format(Rs.Fields("FinishedCts").Value, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        Rs.Fields("FinishedPcs").Value, Format(Rs.Fields("IssueCts").Value, "#0.000"), vYield, "0", False, "0", Rs.Fields("Reference").Value, "APCU", "0",
                                        Rs.Fields("NLineNo").Value, Rs.Fields("MarketPrice").Value, Rs.Fields("AvgCost").Value, "", "", Rs.Fields("Side").Value)
                End If

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_KitExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim dblAvgCost As Double

        mStrSQL = "SELECT TOP (100) PERCENT dbo.tblMixExportOrders.Assortment, MAX(dbo.tblMixExportOrders.AssPrice) AS AssPrice, dbo.tblMixExportOrders.OrderNo, " & _
                      "SUM(dbo.tblMixExportOrders.FinishedPcs) AS FinishedPcs, SUM(dbo.tblMixExportOrders.FinishedCts) AS FinishedCts, " & _
                      "SUM(dbo.tblMixExportOrders.IssuePcs) AS IssuePcs, SUM(dbo.tblMixExportOrders.IssueCts) AS IssueCts, MAX(dbo.tblMixExportOrders.RateCode) AS RateCode, " & _
                      "MAX(dbo.tblMixExportOrders.Export) AS Export, MAX(dbo.tblMixExportOrders.RecordNo) AS RecordNo, dbo.tblMixPacket.PktRefNo, dbo.tblMixPacket.Pktside, dbo.tblMixExportOrders.OrigAssort, " & _
                      "dbo.tblMixExportOrders.NLineNo, dbo.tblAssortList.MarketPrice, dbo.tblAssortList.CurrentCost, dbo.tblAssortList.AvgCost, SUM(dbo.tblMixExportOrders.GrPcs) AS GrPcs " & _
                  "FROM dbo.tblMixExportOrders INNER JOIN dbo.tblMixPacket ON dbo.tblMixExportOrders.OrderNo = dbo.tblMixPacket.PktOrdNo AND dbo.tblMixExportOrders.PacketNo = dbo.tblMixPacket.PktNo INNER JOIN " & _
                      "dbo.tblAssortList ON dbo.tblMixExportOrders.Assortment = dbo.tblAssortList.Assortment " & _
                  "WHERE (dbo.tblMixExportOrders.Status = 'A') AND (dbo.tblMixExportOrders.Export = 1) " & _
                  "GROUP BY dbo.tblMixExportOrders.Assortment, dbo.tblMixExportOrders.OrigAssort, dbo.tblMixExportOrders.OrderNo, " & _
                      "dbo.tblMixPacket.PktRefNo, dbo.tblMixPacket.Pktside, dbo.tblMixExportOrders.NLineNo, dbo.tblAssortList.MarketPrice, dbo.tblAssortList.CurrentCost, dbo.tblAssortList.AvgCost " & _
                  "ORDER BY dbo.tblMixExportOrders.OrderNo, dbo.tblMixExportOrders.Assortment"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1
                If Rs.Fields("IssuePcs").Value > 0 Then

                    vRateValue = PFGetValueCharges(Rs.Fields("RateCode").Value)
                    vStByCt = Math.Round(Rs.Fields("IssuePcs").Value / Rs.Fields("IssueCts").Value, 2)
                    vYield = Math.Round((Rs.Fields("FinishedCts").Value / Rs.Fields("IssueCts").Value) * 100, 2)
                    dblAvgCost = Rs.Fields("AssPrice").Value

                    dblAvgCost = Math.Round(dblAvgCost, 2)

                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value, Rs.Fields("OrderNo").Value, "00", Rs.Fields("FinishedPcs").Value,
                                        Format(Rs.Fields("FinishedCts").Value, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        Rs.Fields("FinishedPcs").Value, Format(Rs.Fields("IssueCts").Value, "#0.000"), vYield, "0", False, "0", Rs.Fields("PktRefNo").Value, Rs.Fields("OrigAssort").Value, "0",
                                        Rs.Fields("NLineNo").Value, Rs.Fields("CurrentCost").Value, Rs.Fields("AvgCost").Value, "", "", Rs.Fields("Pktside").Value)
                End If

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_PrecisionExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim strOrgAssort As String
        Dim strSupParNo As String
        Dim strLotNo As String

        mStrSQL = "SELECT TOP 100 PERCENT Assortment, MAX(AssPrice) AS AssPrice, OrderNo, ParNo, " & _
                    "SUM(FinishedPcs) AS FinishedPcs, SUM(FinishedCts)AS FinishedCts, SUM(FinishedPcs) AS IssuePcs, " & _
                    "SUM(IssueCts) AS IssueCts, MAX(RateCode) AS RateCode, MAX(Export) AS Export, MAX(RecordNo)AS RecordNo, Reference, Side " & _
                  "FROM dbo.tblPCUFinishOrders " & _
                  "WHERE (Status = 'A') AND (Export = 1) " & _
                  "GROUP BY Assortment, OrderNo, ParNo, Reference, Side " & _
                  "HAVING (SUM(FinishedPcs) > 0) " & _
                  "ORDER BY OrderNo, ParNo"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1

                strLotNo = ""
                strSupParNo = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT LotNo FROM tblImport WHERE DCLParcelNo = '" & Rs.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strLotNo = rsComSql.Fields("LotNo").Value
                End If
                rsComSql = Nothing

                If strLotNo = "" Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT SupParcelNo FROM tblDep_Trf WHERE DCLParcelNo = '" & Rs.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        strSupParNo = rsComSql.Fields("SupParcelNo").Value
                    End If
                    rsComSql = Nothing

                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT LotNo FROM tblImport WHERE SupParcelNo = '" & strSupParNo & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        strLotNo = rsComSql.Fields("LotNo").Value
                    End If
                    rsComSql = Nothing
                End If

                vRateValue = PFGetValueCharges(Rs.Fields("RateCode").Value)
                vStByCt = Math.Round(Rs.Fields("IssuePcs").Value / Rs.Fields("IssueCts").Value, 2)
                vYield = Math.Round((Rs.Fields("FinishedCts").Value / Rs.Fields("IssueCts").Value) * 100, 2)

                strOrgAssort = ""
                If Mid(Rs.Fields("Assortment").Value, 1, 2) = "VM" Then
                    strOrgAssort = "VPCU"
                End If

                flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value, Rs.Fields("OrderNo").Value, Rs.Fields("ParNo").Value, Rs.Fields("FinishedPcs").Value,
                                    Format(Rs.Fields("FinishedCts").Value, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                    Rs.Fields("FinishedPcs").Value, Format(Rs.Fields("IssueCts").Value, "#0.000"), vYield, "0", False, "0", Rs.Fields("Reference").Value, strOrgAssort, "0",
                                    "0", "0", "0", "0", strLotNo, Rs.Fields("Side").Value)

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_ProcessRejectExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim dblInvCts As Double
        Dim dblAvgCost As Double
        Dim dblSelectCost As Double
        Dim dblImpValue As Double
        Dim strSupParNo As String
        Dim strOrigAssort As String
        Dim strLotNo As String
        Dim dblRejCts As Double

        mStrSQL = "SELECT ParNo, OrgAssort, PackNo, SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts, MAX(RateCode) AS RateCode " & _
                  "FROM dbo.tblExpRejExports  " & _
                  "WHERE (OK = 0) " & _
                  "GROUP BY ParNo, OrgAssort, PackNo " & _
                  "ORDER BY ParNo, OrgAssort"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1
                If Rs.Fields("Pcs").Value > 0 Then
                    'If Rs.Fields("ParNo").Value = "A547496" Then
                    '    MsgBox(Rs.Fields("ParNo").Value)
                    'End If

                    strOrigAssort = Rs.Fields("OrgAssort").Value
                    strSupParNo = ""
                    strLotNo = ""
                    dblRejCts = 0

                    rsComSql = New ADODB.Recordset
                    'rsComSql.Open("SELECT SupParcelNo, ItemCost, LotNo FROM tblImport WHERE DCLParcelNo = '" & Rs.Fields("ParNo").Value & "' AND AssortmentNo = '" & strOrigAssort & "'", AdoCN, 1, 1)
                    rsComSql.Open("SELECT SupParcelNo, ItemCost, LotNo, AssortmentNo,SelectCost,InvCts FROM tblImport WHERE DCLParcelNo = '" & Rs.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        strOrigAssort = rsComSql.Fields("AssortmentNo").Value
                        strSupParNo = rsComSql.Fields("SupParcelNo").Value
                        dblAvgCost = rsComSql.Fields("ItemCost").Value
                        strLotNo = rsComSql.Fields("LotNo").Value
                        dblSelectCost = rsComSql.Fields("SelectCost").Value
                        dblInvCts = rsComSql.Fields("InvCts").Value
                    Else
                        rsComSql_1 = New ADODB.Recordset
                        'rsComSql_1.Open("SELECT SupParcelNo, ItemCost, LotNo FROM tblImport WHERE SupParcelNo = '" & Rs.Fields("ParNo").Value & "' AND AssortmentNo = '" & strOrigAssort & "'", AdoCN, 1, 1)
                        rsComSql_1.Open("SELECT SupParcelNo, ItemCost, LotNo, AssortmentNo,SelectCost,InvCts FROM tblImport WHERE SupParcelNo = '" & Rs.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            strOrigAssort = rsComSql_1.Fields("AssortmentNo").Value
                            strSupParNo = rsComSql_1.Fields("SupParcelNo").Value
                            dblAvgCost = rsComSql_1.Fields("ItemCost").Value
                            strLotNo = rsComSql_1.Fields("LotNo").Value
                            dblSelectCost = rsComSql_1.Fields("SelectCost").Value
                            dblInvCts = rsComSql_1.Fields("InvCts").Value
                        End If
                        rsComSql_1 = Nothing
                    End If
                    rsComSql = Nothing

                    If dblSelectCost > 0 Then
                        dblImpValue = dblInvCts * dblAvgCost
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT SupParcelNo, ApcuValue FROM dbo.VW_DCLStockFantacy WHERE (SupParcelNo = '" & strSupParNo & "')", AdoCN, 1, 1)
                        If Not IsDBNull(rsComSql.Fields("ApcuValue").Value) Then
                            dblImpValue = dblImpValue - rsComSql.Fields("ApcuValue").Value
                        End If
                        rsComSql = Nothing

                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT ROUND(SUM(Cts), 3) AS Cts FROM tblExpRejExports WHERE (ParNo = '" & strSupParNo & "')", AdoCN, 1, 1)
                        If Not IsDBNull(rsComSql.Fields("Cts").Value) Then
                            dblRejCts = rsComSql.Fields("Cts").Value
                        End If

                        dblAvgCost = Math.Round(dblImpValue / dblRejCts, 2)
                    End If

                    vRateValue = PFGetValueCharges(Rs.Fields("RateCode").Value)
                    vRateValue = Math.Round(vRateValue, 2)
                    vStByCt = Math.Round(Rs.Fields("Pcs").Value / Rs.Fields("Cts").Value, 2)
                    vYield = Math.Round((Rs.Fields("Cts").Value / Rs.Fields("Cts").Value) * 100, 2)

                    flxDetails.Rows.Add(cmbDepartment.Text, strOrigAssort, dblAvgCost, strSupParNo, Rs.Fields("ParNo").Value, Rs.Fields("Pcs").Value,
                                        Format(Rs.Fields("Cts").Value, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        Rs.Fields("Pcs").Value, Format(Rs.Fields("Cts").Value, "#0.000"), vYield, "0", False, "0", "0", strOrigAssort, Rs.Fields("PackNo").Value,
                                        "0", "0", "0", "0", strLotNo)
                End If

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_LamourExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim intFinPcs As Integer
        Dim dblFinCts As Double
        Dim intPCUPcs As Integer
        Dim dblPCUCts As Double
        Dim intRghPcs As Integer
        Dim dblRghCts As Double
        Dim dblAvgRgh As Double
        Dim strLotNo As String

        mStrSQL = "SELECT * FROM VW_LAM_FinishedParcels ORDER BY ParcelNo"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1
                vRateValue = PFGetValueCharges(Rs.Fields("RateCode").Value)
                vRateValue = Math.Round(vRateValue, 2)
                vStByCt = Math.Round(Rs.Fields("RoughPcs").Value / Rs.Fields("RoughCts").Value, 2)
                vYield = Math.Round((Rs.Fields("FinishedCts").Value / Rs.Fields("IssueCts").Value) * 100, 2)
                If Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value > 0 Then
                    dblAvgRgh = (Rs.Fields("RoughCts").Value - Rs.Fields("PCUCts").Value) / (Rs.Fields("RoughPcs").Value - Rs.Fields("PcuPcs").Value)
                Else
                    dblAvgRgh = Rs.Fields("RoughCts").Value / Rs.Fields("RoughPcs").Value
                End If

                If strRight(Rs.Fields("ParcelNo").Value, 1) = "N" Or strRight(Rs.Fields("ParcelNo").Value, 1) = "V" Then
                    intFinPcs = Rs.Fields("FinishedPcs").Value
                    dblFinCts = Rs.Fields("FinishedCts").Value
                Else
                    intFinPcs = Rs.Fields("ActPcs").Value
                    dblFinCts = Rs.Fields("ActCts").Value
                End If
                dblFinCts = Math.Round(dblFinCts, 3)

                intPCUPcs = 0
                dblPCUCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_ExpRghIssues3 WHERE Department = 'Lamour' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = rsComSql.Fields("Pcs").Value
                    dblPCUCts = rsComSql.Fields("Cts").Value
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_GradingRghIssuesAll WHERE Department = 'Lamour' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = intPCUPcs + rsComSql.Fields("IssPcs").Value
                    dblPCUCts = dblPCUCts + rsComSql.Fields("IssCts").Value
                End If
                rsComSql = Nothing

                intRghPcs = 0
                dblRghCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(RghPcs) AS RghPcs, ROUND(SUM(RghCts), 3) AS RghCts FROM tblGrading_CheckingReturns " & _
                              "WHERE (ParNo = '" & Rs.Fields("ParcelNo").Value & "') AND (Department = 'Lamour')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("RghPcs").Value) Then
                        intRghPcs = rsComSql.Fields("RghPcs").Value
                        dblRghCts = rsComSql.Fields("RghCts").Value
                    End If
                End If
                rsComSql = Nothing

                strLotNo = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT LotNo FROM tblImport " & _
                              "WHERE (SupParcelNo = '" & Rs.Fields("OrigParcelNo").Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strLotNo = rsComSql.Fields("LotNo").Value
                End If
                rsComSql = Nothing

                If intFinPcs > 0 Then
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value - intPCUPcs - intRghPcs,
                                        Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                Else
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        intFinPcs, Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                End If

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_PolishBoxExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim dblBaseCost As Double
        Dim dblAvgCost As Double
        Dim dblCurCost As Double
        Dim strRateCode As String

        mStrSQL = "SELECT TOP (100) PERCENT SalesNo AS ParNo, Assortment2, DocID, Price, CompCode, SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts " & _
                  "FROM dbo.tblPOLSales " & _
                  "WHERE (OK = 0)" & _
                  "GROUP BY SalesNo, Assortment2, Price, DocID, CompCode " & _
                  "ORDER BY SalesNo, Assortment2, DocID"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1
                strRateCode = ""
                vRateValue = PFGetValueCharges(strRateCode)
                vRateValue = Math.Round(vRateValue, 2)
                vStByCt = Math.Round(Rs.Fields("Pcs").Value / Rs.Fields("Cts").Value, 2)
                vYield = Math.Round((Rs.Fields("Cts").Value / Rs.Fields("Cts").Value) * 100, 2)

                dblAvgCost = Format(Rs.Fields("Price").Value, "#0.00")
                dblBaseCost = Format(Rs.Fields("Price").Value, "#0.00")

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT PRICE FROM tblGrading_SizingList WHERE NAME = '" & Rs.Fields("Assortment2").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    dblBaseCost = rsComSql.Fields("Price").Value
                Else
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT MarketPrice FROM tblAssortList WHERE Assortment = '" & Rs.Fields("Assortment2").Value & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        dblBaseCost = rsComSql_1.Fields("MarketPrice").Value
                    Else
                        rsComSql_2 = New ADODB.Recordset
                        rsComSql_2.Open("SELECT ListCost FROM tblDCLPermanents WHERE ItemName = '" & Rs.Fields("Assortment2").Value & "'", AdoCN, 1, 1)
                        If rsComSql_2.RecordCount Then
                            dblBaseCost = rsComSql_2.Fields("ListCost").Value
                        End If
                        rsComSql_2 = Nothing
                    End If
                    rsComSql_1 = Nothing
                End If
                rsComSql = Nothing

                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT AvgCost, AvgCost2, CurCost, CurCost2 FROM tblDCLPermanents WHERE ItemName = '" & Rs.Fields("Assortment2").Value & "'", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    If Rs.Fields("CompCode").Value = "DCL" Then
                        dblAvgCost = rsComSql_2.Fields("AvgCost").Value
                        dblCurCost = rsComSql_2.Fields("CurCost").Value
                    Else
                        dblAvgCost = rsComSql_2.Fields("AvgCost2").Value
                        dblCurCost = rsComSql_2.Fields("CurCost2").Value
                    End If
                End If
                rsComSql_2 = Nothing

                flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment2").Value, dblCurCost,
                                    Rs.Fields("ParNo").Value, Rs.Fields("DocID").Value, Rs.Fields("Pcs").Value,
                                    Format(Rs.Fields("Cts").Value, "#0.000"), vStByCt, vRateValue, strRateCode,
                                    Rs.Fields("Pcs").Value, Format(Rs.Fields("Cts").Value, "#0.000"), vYield, "0", False,
                                    "0", "0", Rs.Fields("Assortment2").Value, "0", "0", dblBaseCost, dblAvgCost)

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_PolishBoxTrfExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim dblAvgCost As Double
        Dim dblCurCost As Double
        Dim strRateCode As String

        mStrSQL = "SELECT TOP (100) PERCENT TransferNo AS ParNo, Assortment2, DocID, Price, CompCode, SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts " & _
                  "FROM dbo.tblPOLTransfer " & _
                  "WHERE (OK = 0)" & _
                  "GROUP BY TransferNo, Assortment2, Price, DocID, CompCode " & _
                  "ORDER BY TransferNo, Assortment2, DocID"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1
                strRateCode = ""
                vRateValue = PFGetValueCharges(strRateCode)
                vRateValue = Math.Round(vRateValue, 2)
                vStByCt = Math.Round(Rs.Fields("Pcs").Value / Rs.Fields("Cts").Value, 2)
                vYield = Math.Round((Rs.Fields("Cts").Value / Rs.Fields("Cts").Value) * 100, 2)

                dblAvgCost = Format(Rs.Fields("Price").Value, "#0.00")

                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT AvgCost, AvgCost2, CurCost, CurCost2 FROM tblDCLPermanents WHERE ItemName = '" & Rs.Fields("Assortment2").Value & "'", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    If Rs.Fields("CompCode").Value = "DCL" Then
                        dblAvgCost = rsComSql_2.Fields("AvgCost").Value
                        dblCurCost = rsComSql_2.Fields("CurCost").Value
                    Else
                        dblAvgCost = rsComSql_2.Fields("AvgCost2").Value
                        dblCurCost = rsComSql_2.Fields("CurCost2").Value
                    End If
                End If
                rsComSql_2 = Nothing

                flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment2").Value, dblCurCost,
                                    Rs.Fields("ParNo").Value, Rs.Fields("DocID").Value, Rs.Fields("Pcs").Value,
                                    Format(Rs.Fields("Cts").Value, "#0.000"), vStByCt, vRateValue, strRateCode,
                                    Rs.Fields("Pcs").Value, Format(Rs.Fields("Cts").Value, "#0.000"), vYield, "0", False,
                                    "0", "0", Rs.Fields("Assortment2").Value, "0", "0", dblAvgCost, dblAvgCost)

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_OpeningExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim intFinPcs As Integer
        Dim dblFinCts As Double
        Dim intPCUPcs As Integer
        Dim dblPCUCts As Double
        Dim intRghPcs As Integer
        Dim dblRghCts As Double
        Dim dblAvgRgh As Double
        Dim strLotNo As String

        mStrSQL = "SELECT * FROM VW_OPE_FinishedParcels ORDER BY ParcelNo"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1
                vRateValue = PFGetValueCharges(Rs.Fields("RateCode").Value)
                vRateValue = Math.Round(vRateValue, 2)
                vStByCt = Math.Round(Rs.Fields("RoughPcs").Value / Rs.Fields("RoughCts").Value, 2)
                vYield = Math.Round((Rs.Fields("FinishedCts").Value / Rs.Fields("IssueCts").Value) * 100, 2)
                If Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value > 0 Then
                    dblAvgRgh = (Rs.Fields("RoughCts").Value - Rs.Fields("PCUCts").Value) / (Rs.Fields("RoughPcs").Value - Rs.Fields("PcuPcs").Value)
                Else
                    dblAvgRgh = Rs.Fields("RoughCts").Value / Rs.Fields("RoughPcs").Value
                End If

                If strRight(Rs.Fields("ParcelNo").Value, 1) = "N" Or strRight(Rs.Fields("ParcelNo").Value, 1) = "V" Then
                    intFinPcs = Rs.Fields("FinishedPcs").Value
                    dblFinCts = Rs.Fields("FinishedCts").Value
                Else
                    intFinPcs = Rs.Fields("ActPcs").Value
                    dblFinCts = Rs.Fields("ActCts").Value
                End If
                dblFinCts = Math.Round(dblFinCts, 3)

                intPCUPcs = 0
                dblPCUCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_ExpRghIssues3 WHERE Department = 'Opening' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = rsComSql.Fields("Pcs").Value
                    dblPCUCts = rsComSql.Fields("Cts").Value
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_GradingRghIssuesAll WHERE Department = 'Opening' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = intPCUPcs + rsComSql.Fields("IssPcs").Value
                    dblPCUCts = dblPCUCts + rsComSql.Fields("IssCts").Value
                End If
                rsComSql = Nothing

                intRghPcs = 0
                dblRghCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(RghPcs) AS RghPcs, ROUND(SUM(RghCts), 3) AS RghCts FROM tblGrading_CheckingReturns " & _
                              "WHERE (ParNo = '" & Rs.Fields("ParcelNo").Value & "') AND (Department = 'Opening')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("RghPcs").Value) Then
                        intRghPcs = rsComSql.Fields("RghPcs").Value
                        dblRghCts = rsComSql.Fields("RghCts").Value
                    End If
                End If
                rsComSql = Nothing

                strLotNo = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT LotNo FROM tblImport " & _
                              "WHERE (SupParcelNo = '" & Rs.Fields("OrigParcelNo").Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strLotNo = rsComSql.Fields("LotNo").Value
                End If
                rsComSql = Nothing

                If intFinPcs > 0 Then
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value - intPCUPcs - intRghPcs,
                                        Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                Else
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        intFinPcs, Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                End If

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_PrincessExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim intFinPcs As Integer
        Dim dblFinCts As Double
        Dim intPCUPcs As Integer
        Dim dblPCUCts As Double
        Dim intRghPcs As Integer
        Dim dblRghCts As Double
        Dim dblAvgRgh As Double
        Dim strLotNo As String

        mStrSQL = "SELECT * FROM VW_PR_FinishedParcels ORDER BY DCLParNo"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1
                vRateValue = PFGetValueCharges(Rs.Fields("RateCode").Value)
                vStByCt = Math.Round(Rs.Fields("IssuePcs").Value / Rs.Fields("IssueCts").Value, 2)
                vYield = Math.Round((Rs.Fields("FinishedCts").Value / Rs.Fields("IssueCts").Value) * 100, 2)
                If Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value > 0 Then
                    dblAvgRgh = (Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value) / (Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value)
                Else
                    dblAvgRgh = Rs.Fields("IssueCts").Value / Rs.Fields("IssuePcs").Value
                End If

                If strRight(Rs.Fields("DCLParNo").Value, 1) = "N" Or strRight(Rs.Fields("DCLParNo").Value, 1) = "V" Then
                    intFinPcs = Rs.Fields("FinishedPcs").Value
                    dblFinCts = Rs.Fields("FinishedCts").Value
                Else
                    intFinPcs = Rs.Fields("ActPcs").Value
                    dblFinCts = Rs.Fields("ActCts").Value
                End If
                dblFinCts = Math.Round(dblFinCts, 3)

                intPCUPcs = 0
                dblPCUCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_ExpRghIssues3 WHERE Department = 'Princess' AND ParNo = '" & Rs.Fields("DCLParNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = rsComSql.Fields("Pcs").Value
                    dblPCUCts = rsComSql.Fields("Cts").Value
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_GradingRghIssuesAll WHERE Department = 'Princess' AND ParNo = '" & Rs.Fields("DCLParNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = intPCUPcs + rsComSql.Fields("IssPcs").Value
                    dblPCUCts = dblPCUCts + rsComSql.Fields("IssCts").Value
                End If
                rsComSql = Nothing

                intRghPcs = 0
                dblRghCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(RghPcs) AS RghPcs, ROUND(SUM(RghCts), 3) AS RghCts FROM tblGrading_CheckingReturns " & _
                              "WHERE (ParNo = '" & Rs.Fields("DCLParNo").Value & "') AND (Department = 'Princess')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("RghPcs").Value) Then
                        intRghPcs = rsComSql.Fields("RghPcs").Value
                        dblRghCts = rsComSql.Fields("RghCts").Value
                    End If
                End If
                rsComSql = Nothing

                strLotNo = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT LotNo FROM tblImport " & _
                              "WHERE (SupParcelNo = '" & Rs.Fields("OrigParcelNo").Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strLotNo = rsComSql.Fields("LotNo").Value
                End If
                rsComSql = Nothing

                If intFinPcs > 0 Then
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AsstPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("DCLParNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value - intPCUPcs - intRghPcs,
                                        Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                Else
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AsstPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("DCLParNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        intFinPcs, Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                End If

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_Princess2ExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim intFinPcs As Integer
        Dim dblFinCts As Double
        Dim intPCUPcs As Integer
        Dim dblPCUCts As Double
        Dim intRghPcs As Integer
        Dim dblRghCts As Double
        Dim dblAvgRgh As Double
        Dim strLotNo As String

        mStrSQL = "SELECT * FROM VW_PR2_FinishedParcels ORDER BY ParcelNo"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1
                vRateValue = PFGetValueCharges(Rs.Fields("RateCode").Value)
                vRateValue = Math.Round(vRateValue, 2)
                vStByCt = Math.Round(Rs.Fields("RoughPcs").Value / Rs.Fields("RoughCts").Value, 2)
                vYield = Math.Round((Rs.Fields("FinishedCts").Value / Rs.Fields("IssueCts").Value) * 100, 2)
                If Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value > 0 Then
                    dblAvgRgh = (Rs.Fields("RoughCts").Value - Rs.Fields("PCUCts").Value) / (Rs.Fields("RoughPcs").Value - Rs.Fields("PcuPcs").Value)
                Else
                    dblAvgRgh = Rs.Fields("RoughCts").Value / Rs.Fields("RoughPcs").Value
                End If

                If strRight(Rs.Fields("ParcelNo").Value, 1) = "N" Or strRight(Rs.Fields("ParcelNo").Value, 1) = "V" Then
                    intFinPcs = Rs.Fields("FinishedPcs").Value
                    dblFinCts = Rs.Fields("FinishedCts").Value
                Else
                    intFinPcs = Rs.Fields("ActPcs").Value
                    dblFinCts = Rs.Fields("ActCts").Value
                End If
                dblFinCts = Math.Round(dblFinCts, 3)

                intPCUPcs = 0
                dblPCUCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_ExpRghIssues3 WHERE Department = 'Princess2' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = rsComSql.Fields("Pcs").Value
                    dblPCUCts = rsComSql.Fields("Cts").Value
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_GradingRghIssuesAll WHERE Department = 'Princess2' AND ParNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intPCUPcs = intPCUPcs + rsComSql.Fields("IssPcs").Value
                    dblPCUCts = dblPCUCts + rsComSql.Fields("IssCts").Value
                End If
                rsComSql = Nothing

                intRghPcs = 0
                dblRghCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(RghPcs) AS RghPcs, ROUND(SUM(RghCts), 3) AS RghCts FROM tblGrading_CheckingReturns " & _
                              "WHERE (ParNo = '" & Rs.Fields("ParcelNo").Value & "') AND (Department = 'Princess2')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("RghPcs").Value) Then
                        intRghPcs = rsComSql.Fields("RghPcs").Value
                        dblRghCts = rsComSql.Fields("RghCts").Value
                    End If
                End If
                rsComSql = Nothing

                strLotNo = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT LotNo FROM tblImport " & _
                              "WHERE (SupParcelNo = '" & Rs.Fields("OrigParcelNo").Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strLotNo = rsComSql.Fields("LotNo").Value
                End If
                rsComSql = Nothing

                If intFinPcs > 0 Then
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        Rs.Fields("IssuePcs").Value - Rs.Fields("PcuPcs").Value - intPCUPcs - intRghPcs,
                                        Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                Else
                    flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value,
                                        Rs.Fields("OrigParcelNo").Value, Rs.Fields("ParcelNo").Value, intFinPcs,
                                        Format(dblFinCts, "#0.000"), vStByCt, vRateValue, Rs.Fields("RateCode").Value,
                                        intFinPcs, Format(Rs.Fields("IssueCts").Value - Rs.Fields("PCUCts").Value - dblPCUCts - dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", "", "", "0", "", "0", "0", "", strLotNo)
                End If

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_RoughSalesExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim dblAvgCost As Double
        Dim strOrgAssort As String
        Dim strSupParNo As String
        Dim dblItemCost As Double
        Dim dblImportValue As Double
        Dim dblImportCts As Double
        Dim intSupCode As Integer
        Dim strRateCode As String
        Dim strLotNo As String

        'mStrSQL = "SELECT TOP (100) PERCENT ParcelNo AS ParNo, SUM(PktPcs) AS Pcs, ROUND(SUM(PktCts), 3) AS Cts, ROUND(SUM(PktCts * Price), 2) AS Value " & _
        '          "FROM dbo.tblParcelRghSales " & _
        '          "WHERE OK = 0 " & _
        '          "GROUP BY ParcelNo " & _
        '          "ORDER BY ParcelNo"

        mStrSQL = "SELECT TOP (100) PERCENT dbo.tblParcelRghSales.ParcelNo AS ParNo, SUM(dbo.tblParcelRghSales.PktPcs) AS Pcs, ROUND(SUM(dbo.tblParcelRghSales.PktCts), 3) AS Cts, " & _
                      "ROUND(SUM(dbo.tblParcelRghSales.PktCts * dbo.tblParcelRghSales.Price), 2) AS Value, dbo.tblParcel.IssuedPcs, dbo.tblParcel.IssuedCts, dbo.tblParcel.Charges " & _
                  "FROM dbo.tblParcelRghSales INNER JOIN dbo.tblParcel ON dbo.tblParcelRghSales.ParcelNo = dbo.tblParcel.GrpParNo " & _
                  "WHERE(dbo.tblParcelRghSales.OK = 0) " & _
                  "GROUP BY dbo.tblParcelRghSales.ParcelNo, dbo.tblParcel.IssuedPcs, dbo.tblParcel.IssuedCts, dbo.tblParcel.Charges " & _
                  "ORDER BY ParNo"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1
                If Rs.Fields("Pcs").Value > 0 Then
                    dblItemCost = 0
                    dblImportValue = 0
                    dblImportCts = 0
                    strOrgAssort = ""
                    strLotNo = ""
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT SupParcelNo, AssortmentNo, ItemCost, InvCts, SupplierCode, LotNo FROM tblImport WHERE DCLParcelNo = '" & Mid(Rs.Fields("ParNo").Value, 1, 6) & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        strSupParNo = rsComSql.Fields("SupParcelNo").Value
                        strOrgAssort = rsComSql.Fields("AssortmentNo").Value
                        dblItemCost = rsComSql.Fields("ItemCost").Value
                        dblImportValue = rsComSql.Fields("ItemCost").Value * rsComSql.Fields("INVCts").Value
                        dblImportCts = rsComSql.Fields("INVCts").Value
                        intSupCode = rsComSql.Fields("SupplierCode").Value
                        strLotNo = rsComSql.Fields("LotNo").Value
                    Else
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT SupParcelNo, AssortmentNo, ItemCost, InvCts, SupplierCode FROM tblDep_Trf WHERE DCLParcelNo = '" & Mid(Rs.Fields("ParNo").Value, 1, 6) & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            strSupParNo = rsComSql_1.Fields("SupParcelNo").Value
                            strOrgAssort = rsComSql_1.Fields("AssortmentNo").Value
                            dblItemCost = rsComSql_1.Fields("ItemCost").Value
                            dblImportValue = rsComSql_1.Fields("ItemCost").Value * rsComSql_1.Fields("INVCts").Value
                            dblImportCts = rsComSql_1.Fields("INVCts").Value
                            intSupCode = rsComSql_1.Fields("SupplierCode").Value

                            rsComSql_2 = New ADODB.Recordset
                            rsComSql_2.Open("SELECT LotNo FROM tblImport WHERE SupParcelNo = '" & strSupParNo & "'", AdoCN, 1, 1)
                            If rsComSql_2.RecordCount Then
                                strLotNo = rsComSql_2.Fields("LotNo").Value
                            End If
                            rsComSql_2 = Nothing
                        Else
                            rsComSql_2 = New ADODB.Recordset
                            rsComSql_2.Open("SELECT SupParcelNo, AssortmentNo, ItemCost, InvCts, SupplierCode, LotNo FROM tblImport WHERE DCLParcelNo = '" & Rs.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                            If rsComSql_2.RecordCount Then
                                strSupParNo = rsComSql_2.Fields("SupParcelNo").Value
                                strOrgAssort = rsComSql_2.Fields("AssortmentNo").Value
                                dblItemCost = rsComSql_2.Fields("ItemCost").Value
                                dblImportValue = rsComSql_2.Fields("ItemCost").Value * rsComSql_2.Fields("INVCts").Value
                                dblImportCts = rsComSql_2.Fields("INVCts").Value
                                intSupCode = rsComSql_2.Fields("SupplierCode").Value
                                strLotNo = rsComSql_2.Fields("LotNo").Value
                            Else
                                strSupParNo = Mid(Rs.Fields("ParNo").Value, 1, 6)
                            End If
                            rsComSql_2 = Nothing
                        End If
                        rsComSql_1 = Nothing
                    End If
                    rsComSql = Nothing

                    strRateCode = Rs.Fields("Charges").Value
                    dblAvgCost = dblItemCost

                    dblAvgCost = Rs.Fields("Value").Value / Rs.Fields("Cts").Value
                    dblAvgCost = Math.Round(dblAvgCost, 2)

                    vRateValue = PFGetValueCharges(strRateCode)
                    vStByCt = Math.Round(Rs.Fields("Pcs").Value / Rs.Fields("Cts").Value, 2)
                    vYield = Math.Round((Rs.Fields("Cts").Value / Rs.Fields("Cts").Value) * 100, 2)
                    dblAvgCost = Math.Round(dblAvgCost, 2)

                    flxDetails.Rows.Add(cmbDepartment.Text, strOrgAssort, dblAvgCost, strSupParNo, Rs.Fields("ParNo").Value, Rs.Fields("Pcs").Value,
                                        Format(Rs.Fields("Cts").Value, "#0.000"), vStByCt, vRateValue, strRateCode,
                                        Rs.Fields("IssuedPcs").Value, Format(Rs.Fields("IssuedCts").Value, "#0.000"), vYield, "0", False, "0", "0",
                                        strOrgAssort, "0", "0", dblItemCost, dblItemCost, "", strLotNo)
                End If

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_RoundsExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double
        Dim intPCUPcs As Integer
        Dim dblPCUCts As Double

        Dim intFinPcs As Integer
        Dim dblFinCts As Double
        Dim intRghPcs As Integer
        Dim dblRghCts As Double
        Dim strLotNo As String
        Dim intFirstTime As Integer

        mStrSQL = "SELECT Assortment, AssPrice, SuppRef, DCLRef, SUM(FinishedPcs) AS FinishedPcs, SUM(FinishedCts) AS FinishedCts, RateCode, SUM(IssuePcs) AS IssuePcs, " & _
                    "SUM(IssueCts) AS IssueCts, Export, MAX(RecordNo) AS RecordNo " & _
                  "FROM tblRndFinishParcels " & _
                  "WHERE (Status = 'A') AND (Export = 1) " & _
                  "GROUP BY Assortment, AssPrice, SuppRef, DCLRef, RateCode, Export ORDER BY DCLref"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1
                vRateValue = PFGetValueCharges(Rs.Fields("RateCode").Value)
                vStByCt = Math.Round(Rs.Fields("IssuePcs").Value / Rs.Fields("IssueCts").Value, 2)
                vYield = Math.Round((Rs.Fields("FinishedCts").Value / Rs.Fields("IssueCts").Value) * 100, 2)

                'If Rs.Fields("DCLref").Value = "JR3254" Then
                '    MsgBox(Rs.Fields("DCLref").Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                'End If

                intFirstTime = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT Reference2 FROM tblExportVarification WHERE Department = 'Rounds' AND Reference2 = '" & Rs.Fields("DCLref").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intFirstTime = 1
                End If
                rsComSql = Nothing

                intRghPcs = 0
                dblRghCts = 0
                If intFirstTime = 0 Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT SUM(Pcs) AS IssPcs, ROUND(SUM(Cts), 3) AS IssCts " & _
                                  "FROM tblGrading_Box_Forever " & _
                                  "WHERE (ParNo = '" & Rs.Fields("DCLref").Value & "') AND (Grp <> 'CN')", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If Not IsDBNull(rsComSql.Fields("IssPcs").Value) Then
                            intRghPcs = intRghPcs + rsComSql.Fields("IssPcs").Value
                            dblRghCts = dblRghCts + rsComSql.Fields("IssCts").Value
                        End If
                    End If
                    rsComSql = Nothing
                End If

                intFinPcs = 0
                dblFinCts = 0
                If intFirstTime = 0 Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT SUM(Pcs) AS IssPcs, SUM(Cts) AS IssCts " & _
                                  "FROM tblGrading_Box " & _
                                  "WHERE (ParNo = '" & Rs.Fields("DCLref").Value & "') AND (OK = 0) AND (Department = 'Rounds')", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If Not IsDBNull(rsComSql.Fields("IssPcs").Value) Then
                            intFinPcs = rsComSql.Fields("IssPcs").Value
                            dblFinCts = rsComSql.Fields("IssCts").Value
                        End If
                    End If
                    rsComSql = Nothing
                End If

                If intFinPcs = 0 Then
                    intFinPcs = Rs.Fields("FinishedPcs").Value
                End If
                If dblFinCts = 0 Then
                    dblFinCts = Rs.Fields("FinishedCts").Value
                End If

                strLotNo = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT LotNo FROM tblImport " & _
                              "WHERE (SupParcelNo = '" & Rs.Fields("SuppRef").Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strLotNo = rsComSql.Fields("LotNo").Value
                End If
                rsComSql = Nothing

                intPCUPcs = 0
                dblPCUCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(IssPcs) AS IssPcs, SUM(IssCts) AS IssCts FROM VW_GradingRghIssuesAll WHERE Department = 'Rounds' AND LEFT(ParNo, 6) = '" & Rs.Fields("DCLref").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("IssPcs").Value) Then
                        intPCUPcs = intPCUPcs + rsComSql.Fields("IssPcs").Value
                        dblPCUCts = dblPCUCts + rsComSql.Fields("IssCts").Value
                    End If
                End If
                rsComSql = Nothing

                flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value, Rs.Fields("SuppRef").Value,
                                    Rs.Fields("DCLref").Value, intFinPcs - intRghPcs, Format(Rs.Fields("FinishedCts").Value - dblRghCts, "#0.000"),
                                    vStByCt, vRateValue, Rs.Fields("RateCode").Value, Rs.Fields("IssuePcs").Value - intRghPcs - intPCUPcs,
                                    Format(Rs.Fields("IssueCts").Value - dblRghCts - dblPCUCts, "#0.000"), vYield, 0, False, "0", "", "", "0", "",
                                    "0", "0", "", strLotNo)

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_Rounds4ExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim dblRghPcs As Double
        Dim dblRghCts As Double
        Dim strLotNo As String

        mStrSQL = "SELECT Assortment, AssPrice, SuppRef, DCLRef, SUM(FinishedPcs) AS FinishedPcs, SUM(FinishedCts) AS FinishedCts, RateCode, SUM(IssuePcs) AS IssuePcs, " & _
                    "SUM(IssueCts) AS IssueCts, Export, MAX(RecordNo) AS RecordNo " & _
              "FROM tblExtFinishParcels " & _
              "WHERE (Status = 'A') AND (Export = 1) AND (Department = 'Rounds4') " & _
              "GROUP BY Assortment, AssPrice, SuppRef, DCLRef, RateCode, Export ORDER BY DCLref"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1

                dblRghPcs = Rs.Fields("IssuePcs").Value
                dblRghCts = Rs.Fields("IssueCts").Value

                vRateValue = PFGetValueCharges(Rs.Fields("RateCode").Value)
                vStByCt = Math.Round(Rs.Fields("IssuePcs").Value / Rs.Fields("IssueCts").Value, 2)
                vYield = Math.Round((Rs.Fields("FinishedCts").Value / Rs.Fields("IssueCts").Value) * 100, 2)

                strLotNo = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT LotNo FROM tblImport " & _
                              "WHERE (SupParcelNo = '" & Rs.Fields("SuppRef").Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strLotNo = rsComSql.Fields("LotNo").Value
                End If
                rsComSql = Nothing

                flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value, Rs.Fields("SuppRef").Value,
                                    Rs.Fields("DCLref").Value, Rs.Fields("FinishedPcs").Value, Format(Rs.Fields("FinishedCts").Value, "#0.000"),
                                    vStByCt, vRateValue, Rs.Fields("RateCode").Value, dblRghPcs, Format(dblRghCts, "#0.000"), vYield, 0,
                                    False, "0", "", "", "0", "", "0", "0", "", strLotNo)

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_RoundsNLEExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double

        Dim dblRghPcs As Double
        Dim dblRghCts As Double
        Dim strLotNo As String

        mStrSQL = "SELECT Assortment, AssPrice, SuppRef, DCLRef, SUM(FinishedPcs) AS FinishedPcs, SUM(FinishedCts) AS FinishedCts, RateCode, SUM(IssuePcs) AS IssuePcs, " & _
                    "SUM(IssueCts) AS IssueCts, Export, MAX(RecordNo) AS RecordNo " & _
              "FROM tblExtFinishParcels " & _
              "WHERE (Status = 'A') AND (Export = 1) AND (Department = 'RoundsNLE') " & _
              "GROUP BY Assortment, AssPrice, SuppRef, DCLRef, RateCode, Export ORDER BY DCLref"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1

                dblRghPcs = Rs.Fields("IssuePcs").Value
                dblRghCts = Rs.Fields("IssueCts").Value

                vRateValue = PFGetValueCharges(Rs.Fields("RateCode").Value)
                vStByCt = Math.Round(Rs.Fields("IssuePcs").Value / Rs.Fields("IssueCts").Value, 2)
                vYield = Math.Round((Rs.Fields("FinishedCts").Value / Rs.Fields("IssueCts").Value) * 100, 2)

                strLotNo = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT LotNo FROM tblImport " & _
                              "WHERE (SupParcelNo = '" & Rs.Fields("SuppRef").Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strLotNo = rsComSql.Fields("LotNo").Value
                End If
                rsComSql = Nothing

                flxDetails.Rows.Add(cmbDepartment.Text, Rs.Fields("Assortment").Value, Rs.Fields("AssPrice").Value, Rs.Fields("SuppRef").Value,
                                    Rs.Fields("DCLref").Value, Rs.Fields("FinishedPcs").Value, Format(Rs.Fields("FinishedCts").Value, "#0.000"),
                                    vStByCt, vRateValue, Rs.Fields("RateCode").Value, dblRghPcs, Format(dblRghCts, "#0.000"), vYield, 0,
                                    False, "0", "", "", "0", "", "0", "0", "", strLotNo)

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_RoundsOrdersExpInfo()
        Dim Rs As New ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double
        Dim strOrgAssort As String
        Dim strSupParNo As String
        Dim dblItemCost As Double
        Dim dblImportValue As Double
        Dim dblImportCts As Double
        Dim intSupCode As Integer
        Dim strLotNo As String

        mStrSQL = "SELECT * FROM dbo.VW_GradingBox_Forever ORDER BY OrderNo"

        Rs = New ADODB.Recordset
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1
                If Rs.Fields("ExpPcs").Value > 0 Then
                    'If Rs![ParNo] = "10197495" Then
                    '    MsgBox ""
                    'End If

                    dblItemCost = 0
                    dblImportValue = 0
                    dblImportCts = 0
                    strOrgAssort = ""
                    strSupParNo = ""
                    strLotNo = ""
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT SupParcelNo, AssortmentNo, ItemCost, InvCts, SupplierCode, LotNo FROM tblImport WHERE DCLParcelNo = '" & Rs.Fields("ParcelNo").Value & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        strSupParNo = rsComSql.Fields("SupParcelNo").Value
                        strOrgAssort = rsComSql.Fields("AssortmentNo").Value
                        dblItemCost = rsComSql.Fields("ItemCost").Value
                        dblImportCts = rsComSql.Fields("INVCts").Value
                        dblImportValue = dblItemCost * dblImportCts
                        intSupCode = rsComSql.Fields("SupplierCode").Value
                        strLotNo = rsComSql.Fields("LotNo").Value
                    Else
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT SupParcelNo, AssortmentNo, ItemCost, InvCts, SupplierCode FROM tblDep_Trf WHERE DCLParcelNo = '" & Rs.Fields("ParcelNo").Value & "' AND (Department = 'Rounds' OR Department = 'Colombo Niru')", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            strSupParNo = rsComSql_1.Fields("SupParcelNo").Value
                            strOrgAssort = rsComSql_1.Fields("AssortmentNo").Value
                            dblItemCost = rsComSql_1.Fields("ItemCost").Value
                            dblImportCts = rsComSql_1.Fields("INVCts").Value
                            dblImportValue = dblItemCost * dblImportCts
                            intSupCode = rsComSql_1.Fields("SupplierCode").Value
                        End If
                        rsComSql_1 = Nothing
                    End If
                    rsComSql = Nothing

                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT LotNo FROM tblImport WHERE (SupParcelNo = '" & strSupParNo & "')", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        strLotNo = rsComSql.Fields("LotNo").Value
                    End If
                    rsComSql = Nothing

                    dblItemCost = Rs.Fields("Value").Value / Rs.Fields("ExpCts").Value
                    dblItemCost = Math.Round(dblItemCost, 2)

                    vRateValue = Format(PFGetValueCharges(Rs.Fields("Charges").Value), "#0.#0")
                    vStByCt = Format(Val(Rs.Fields("ExpPcs").Value) / Val(Rs.Fields("ExpCts").Value), "#0.#0")
                    vYield = Format((Rs.Fields("ExpCts").Value / Rs.Fields("ExpCts").Value) * 100, "##0.#0")

                    flxDetails.Rows.Add(cmbDepartment.Text, strOrgAssort, dblItemCost, Rs.Fields("OrderNo").Value, Rs.Fields("ParcelNo").Value, Rs.Fields("ExpPcs").Value,
                                        Format(Rs.Fields("ExpCts").Value, "#0.000"), vStByCt, vRateValue, Rs.Fields("Charges").Value, Rs.Fields("ExpPcs").Value, Format(Rs.Fields("ExpCts").Value, "#0.000"),
                                        vYield, "0", False, "0", Rs.Fields("RefNo").Value, strOrgAssort, "0", Rs.Fields("NLineNo").Value, dblItemCost, dblItemCost, "", strLotNo)
                End If

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub cmbDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDepartment.SelectedIndexChanged
        flxDetails.Rows.Clear()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub Load_GradingPCU_NExpInfo()

        Dim Rs As ADODB.Recordset
        Dim vRateValue As Double
        Dim vStByCt As Double
        Dim vYield As Double
        Dim dblAvgCost As Double
        Dim dblHardCost As Double
        Dim strOrgAssort As String
        Dim strSupParNo As String
        Dim dblItemCost As Double
        Dim dblImportValue As Double
        Dim dblImportCts As Double
        Dim intSupCode As Integer
        Dim dblRghCts As Double
        Dim dblRejRghCts As Double
        Dim strLineNo As String
        Dim strParcelType As String
        Dim blnDirectRough As Boolean
        Dim blnPolishBox As Boolean
        Dim strRateCode As String
        Dim strFanAssort As String
        Dim strLotNo As String

        Rs = New ADODB.Recordset
        mStrSQL = "SELECT TOP (100) PERCENT Department, ParNo, PackNo, SUM(Pcs) AS Pcs, ROUND(SUM(ActCts), 3) AS Cts, SUM(ActCts * Price) AS Value, OrderNo, RefNo, Side, RateCode " & _
                  "FROM dbo.tblGrading_PackingListPCU " & _
                  "WHERE (OK = 0) " & _
                  "GROUP BY Department, ParNo, OrderNo, RefNo, Side, RateCode, PackNo " & _
                  "HAVING (Department = 'GradingPCU_N') " & _
                  "ORDER BY OrderNo, ParNo, RefNo, Side, RateCode"

        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount Then
            intNoOfRecords = Rs.RecordCount
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0
            Rs.MoveFirst()

            While Not Rs.EOF
                intCounter = intCounter + 1
                If Rs.Fields("Pcs").Value > 0 Then
                    'If Rs.Fields("ParNo").Value = "MP0051" Then
                    '    MsgBoxGT Rs![ParNo]
                    'End If

                    'If Rs.Fields("OrderNo").Value = "80463" Then
                    '    MsgBoxGT Rs![OrderNo]
                    'End If

                    blnDirectRough = False
                    blnPolishBox = False
                    dblItemCost = 0
                    dblImportValue = 0
                    dblImportCts = 0
                    strParcelType = ""
                    strSupParNo = ""
                    strOrgAssort = ""
                    dblHardCost = 0
                    strFanAssort = ""
                    strLotNo = ""
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT SupParcelNo, AssortmentNo, ItemCost, InvCts, SupplierCode, ParcelType, LotNo FROM tblImport WHERE DCLParcelNo = '" & Mid(Rs.Fields("ParNo").Value, 1, 6) & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        strSupParNo = rsComSql.Fields("SupParcelNo").Value
                        strOrgAssort = rsComSql.Fields("AssortmentNo").Value
                        dblItemCost = rsComSql.Fields("ItemCost").Value
                        dblImportValue = rsComSql.Fields("ItemCost").Value * rsComSql.Fields("INVCts").Value
                        dblImportCts = rsComSql.Fields("INVCts").Value
                        intSupCode = rsComSql.Fields("SupplierCode").Value
                        strParcelType = rsComSql.Fields("ParcelType").Value
                        strLotNo = rsComSql.Fields("LotNo").Value
                    Else
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT SupParcelNo, AssortmentNo, ItemCost, InvCts, SupplierCode, ParcelType FROM tblDep_Trf WHERE DCLParcelNo = '" & Mid(Rs.Fields("ParNo").Value, 1, 6) & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            strSupParNo = rsComSql_1.Fields("SupParcelNo").Value
                            strOrgAssort = rsComSql_1.Fields("AssortmentNo").Value
                            dblItemCost = rsComSql_1.Fields("ItemCost").Value
                            dblImportValue = rsComSql_1.Fields("ItemCost").Value * rsComSql_1.Fields("INVCts").Value
                            dblImportCts = rsComSql_1.Fields("INVCts").Value
                            intSupCode = rsComSql_1.Fields("SupplierCode").Value
                            strParcelType = rsComSql_1.Fields("ParcelType").Value

                            rsComSql_2 = New ADODB.Recordset
                            rsComSql_2.Open("SELECT LotNo FROM tblImport WHERE SupParcelNo = '" & strSupParNo & "'", AdoCN, 1, 1)
                            If rsComSql_2.RecordCount Then
                                strLotNo = rsComSql_2.Fields("LotNo").Value
                            End If
                            rsComSql_2 = Nothing
                        Else
                            rsComSql_2 = New ADODB.Recordset
                            rsComSql_2.Open("SELECT SupParcelNo, AssortmentNo, ItemCost, InvCts, SupplierCode, ParcelType, LotNo FROM tblImport WHERE DCLParcelNo = '" & Rs.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                            If rsComSql_2.RecordCount Then
                                strSupParNo = rsComSql_2.Fields("SupParcelNo").Value
                                strOrgAssort = rsComSql_2.Fields("AssortmentNo").Value
                                dblItemCost = rsComSql_2.Fields("ItemCost").Value
                                dblImportValue = rsComSql_2.Fields("ItemCost").Value * rsComSql_2.Fields("INVCts").Value
                                dblImportCts = rsComSql_2.Fields("INVCts").Value
                                intSupCode = rsComSql_2.Fields("SupplierCode").Value
                                strParcelType = rsComSql_2.Fields("ParcelType").Value
                                strLotNo = rsComSql_2.Fields("LotNo").Value
                            Else
                                rsComSql_2 = New ADODB.Recordset
                                rsComSql_2.Open("SELECT * FROM tblPCUStockIn WHERE ParNo = '" & Rs.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                                If rsComSql_2.RecordCount Then
                                    strSupParNo = rsComSql_2.Fields("ParNo").Value
                                    strOrgAssort = rsComSql_2.Fields("Assortment").Value
                                    strFanAssort = rsComSql_2.Fields("OrgAssort").Value
                                    strLotNo = rsComSql_2.Fields("ParNo").Value

                                    rsComSql_3 = New ADODB.Recordset
                                    rsComSql_3.Open("SELECT LotNo FROM tblImport WHERE SupParcelNo = '" & strSupParNo & "'", AdoCN, 1, 1)
                                    If rsComSql_3.RecordCount Then
                                        strLotNo = rsComSql_3.Fields("LotNo").Value
                                    End If
                                    rsComSql_3 = Nothing

                                    rsComSql_3 = New ADODB.Recordset
                                    rsComSql_3.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & rsComSql_2.Fields("Assortment").Value & "'", AdoCN, 1, 1)
                                    If rsComSql_3.RecordCount Then
                                        dblItemCost = rsComSql_3.Fields("MarketPrice").Value
                                    End If
                                    rsComSql_3 = Nothing

                                    rsComSql_3 = New ADODB.Recordset
                                    rsComSql_3.Open("SELECT * FROM tblPOLStockOut WHERE DocID = '" & Rs.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                                    If rsComSql_3.RecordCount Then
                                        dblItemCost = rsComSql_3.Fields("Price").Value
                                        blnPolishBox = True
                                    End If
                                    rsComSql_3 = Nothing

                                    If blnPolishBox = True And dblItemCost <= 0 Then
                                        strOrgAssort = strFanAssort
                                        rsComSql_3 = New ADODB.Recordset
                                        rsComSql_3.Open("SELECT ListCost,AvgCost FROM tblDCLPermanents WHERE ItemName = '" & strOrgAssort & "'", AdoCN, 1, 1)
                                        If rsComSql_3.RecordCount Then
                                            dblItemCost = rsComSql_3.Fields("ListCost").Value
                                            dblHardCost = rsComSql_3.Fields("AvgCost").Value
                                        End If
                                        rsComSql_3 = Nothing
                                    End If

                                    If dblItemCost = 0 Then
                                        rsComSql_3 = New ADODB.Recordset
                                        rsComSql_3.Open("SELECT ListCost,AvgCost FROM tblDCLPermanents WHERE ItemName = '" & rsComSql_2.Fields("Assortment").Value & "'", AdoCN, 1, 1)
                                        If rsComSql_3.RecordCount Then
                                            dblItemCost = rsComSql_3.Fields("ListCost").Value
                                            dblHardCost = rsComSql_3.Fields("AvgCost").Value
                                        End If
                                        rsComSql_3 = Nothing
                                    End If
                                    dblImportValue = 0
                                    dblImportCts = 0
                                    intSupCode = 1
                                    strParcelType = "Polished"
                                Else
                                    strSupParNo = Mid(Rs.Fields("ParNo").Value, 1, 6)
                                End If
                            End If
                            rsComSql_2 = Nothing
                        End If
                        rsComSql_1 = Nothing
                    End If
                    rsComSql = Nothing

                    dblAvgCost = 0
                    dblAvgCost = dblItemCost
                    dblAvgCost = Math.Round(Rs.Fields("Value").Value / Rs.Fields("Cts").Value, 2)
                    If dblHardCost <> 0 Then
                        dblAvgCost = dblHardCost
                    End If

                    If strParcelType = "Polished" Or strParcelType = "Rough" Then
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT SUM(Rgh_Pcs) AS RghPcs, ROUND(SUM(Rgh_Cts), 3) AS RghCts " & _
                                      "FROM tblGradingTrf " & _
                                      "WHERE (Department = 'GradingPCU_N') AND (ParcelNo = '" & Rs.Fields("ParNo").Value & "') AND " & _
                                            "(LEFT(PktNo, 1) <> 'P') AND (LEFT(PktNo, 1) <> 'Z') AND (OrderNo = '" & Rs.Fields("OrderNo").Value & "') AND (RefNo = '" & Rs.Fields("RefNo").Value & "') AND (Side = '" & Rs.Fields("Side").Value & "') ", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            If Not IsDBNull(rsComSql.Fields("RghCts").Value) Then
                                dblRghCts = (rsComSql.Fields("RghCts").Value / rsComSql.Fields("RghPcs").Value) * Rs.Fields("Pcs").Value
                            Else
                                dblRghCts = Rs.Fields("Cts").Value
                            End If
                        End If
                        rsComSql = Nothing
                    Else
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT SUM(IssuedPcs) AS RghPcs, ROUND(SUM(IssuedCts), 3) AS RghCts " & _
                                      "FROM tblParcel " & _
                                      "WHERE (Depart = 'Baguettes' OR Depart = 'Princess') AND (ParcelNo = '" & Rs.Fields("ParNo").Value & "') AND " & _
                                            "(Grp <> 'N') AND (ReIssue = 0) ", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            If Not IsDBNull(rsComSql.Fields("RghCts").Value) Then
                                dblRghCts = (rsComSql.Fields("RghCts").Value / rsComSql.Fields("RghPcs").Value) * Rs.Fields("Pcs").Value
                            Else
                                blnDirectRough = True
                                dblRghCts = Rs.Fields("Cts").Value
                            End If
                        End If
                        rsComSql = Nothing
                    End If

                    If blnDirectRough = True Then
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT SUM(Rgh_Pcs) AS RghPcs, ROUND(SUM(Rgh_Cts), 3) AS RghCts " & _
                                      "FROM tblGradingTrf " & _
                                      "WHERE (Department = 'GradingPCU_N') AND (ParcelNo = '" & Rs.Fields("ParNo").Value & "') AND " & _
                                            "(LEFT(PktNo, 1) <> 'P') AND (LEFT(PktNo, 1) <> 'Z') AND (OrderNo = '" & Rs.Fields("OrderNo").Value & "') AND (RefNo = '" & Rs.Fields("RefNo").Value & "') AND (Side = '" & Rs.Fields("Side").Value & "') ", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            If Not IsDBNull(rsComSql.Fields("RghCts").Value) Then
                                dblRghCts = (rsComSql.Fields("RghCts").Value / rsComSql.Fields("RghPcs").Value) * Rs.Fields("Pcs").Value
                            Else
                                dblRghCts = Rs.Fields("Cts").Value
                            End If
                        End If
                        rsComSql = Nothing
                    End If

                    dblRghCts = Math.Round(dblRghCts, 3)

                    dblRejRghCts = 0
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT ROUND(SUM(IssueCts), 3) AS RghCts " & _
                                  "FROM tblPCUFinishOrders " & _
                                  "WHERE (Status = 'A') AND (ParNo = '" & Rs.Fields("ParNo").Value & "') AND " & _
                                        "(OrderNo = '" & Rs.Fields("OrderNo").Value & "') AND (Reference = '" & Rs.Fields("RefNo").Value & "') AND (Side = '" & Rs.Fields("Side").Value & "') ", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If Not IsDBNull(rsComSql.Fields("RghCts").Value) Then
                            dblRejRghCts = rsComSql.Fields("RghCts").Value
                        End If
                    End If
                    rsComSql = Nothing

                    dblRghCts = Math.Round(dblRghCts + dblRejRghCts, 3)

                    If blnPolishBox = True Then
                        dblRghCts = 0
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT SUM(Rgh_Pcs) AS RghPcs, ROUND(SUM(Rgh_Cts), 3) AS RghCts " & _
                                      "FROM tblGradingTrf " & _
                                      "WHERE (Department = 'GradingPCU_N') AND (ParcelNo = '" & Rs.Fields("ParNo").Value & "') AND " & _
                                            "(LEFT(PktNo, 1) <> 'P') AND (LEFT(PktNo, 1) <> 'Z') AND (OrderNo = '" & Rs.Fields("OrderNo").Value & "') AND (RefNo = '" & Rs.Fields("RefNo").Value & "') AND (Side = '" & Rs.Fields("Side").Value & "') ", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            If Not IsDBNull(rsComSql.Fields("RghCts").Value) Then
                                dblRghCts = rsComSql.Fields("RghCts").Value
                            Else
                                dblRghCts = Rs.Fields("Cts").Value
                            End If
                        End If
                        rsComSql = Nothing

                        dblRejRghCts = 0
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT ROUND(SUM(dbo.tblGrading_CheckingReturns.RejCts), 3) AS RejCts " & _
                                      "FROM dbo.tblGradingTrf INNER JOIN dbo.tblGrading_CheckingReturns ON dbo.tblGradingTrf.Department = dbo.tblGrading_CheckingReturns.Department AND " & _
                                        "dbo.tblGradingTrf.ParcelNo = dbo.tblGrading_CheckingReturns.ParNo AND dbo.tblGradingTrf.PktNo = dbo.tblGrading_CheckingReturns.PktNo " & _
                                      "WHERE (dbo.tblGradingTrf.Department = 'GradingPCU_N') AND (dbo.tblGrading_CheckingReturns.ParNo = '" & Rs.Fields("ParNo").Value & "') AND " & _
                                        "(dbo.tblGradingTrf.OrderNo = '" & Rs.Fields("OrderNo").Value & "') AND (dbo.tblGradingTrf.RefNo = '" & Rs.Fields("RefNo").Value & "') AND (dbo.tblGradingTrf.Side = '" & Rs.Fields("Side").Value & "')", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            If Not IsDBNull(rsComSql.Fields("RejCts").Value) Then
                                dblRejRghCts = rsComSql.Fields("RejCts").Value
                            End If
                        End If
                        rsComSql = Nothing

                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT SUM(dbo.tblGrading_SizingReturns.RghPcs) AS RghPcs, ROUND(SUM(dbo.tblGrading_SizingReturns.RghCts), 3) AS RghCts " & _
                                      "FROM dbo.tblGrading_SizingReturns INNER JOIN dbo.tblGrading_SizingPacket ON dbo.tblGrading_SizingReturns.Department = dbo.tblGrading_SizingPacket.Department AND " & _
                                        "dbo.tblGrading_SizingReturns.ParNo = dbo.tblGrading_SizingPacket.ParNo And dbo.tblGrading_SizingReturns.PktNo = dbo.tblGrading_SizingPacket.PktNo " & _
                                      "WHERE (dbo.tblGrading_SizingReturns.Department = 'GradingPCU_N') AND (dbo.tblGrading_SizingReturns.ParNo = '" & Rs.Fields("ParNo").Value & "') AND " & _
                                        "(dbo.tblGrading_SizingPacket.OrderNo = '" & Rs.Fields("OrderNo").Value & "') AND (dbo.tblGrading_SizingPacket.RefNo = '" & Rs.Fields("RefNo").Value & "') AND (dbo.tblGrading_SizingPacket.Side = '" & Rs.Fields("Side").Value & "')", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            If Not IsDBNull(rsComSql.Fields("RghCts").Value) Then
                                dblRejRghCts = dblRejRghCts + rsComSql.Fields("RghCts").Value
                            End If
                        End If
                        rsComSql = Nothing

                        dblRghCts = Math.Round(dblRghCts - dblRejRghCts, 3)
                    End If

                    vStByCt = Math.Round(Rs.Fields("Pcs").Value / Rs.Fields("Cts").Value, 2)
                    vYield = Math.Round((Rs.Fields("Cts").Value / dblRghCts) * 100, 2)

                    If Rs.Fields("Department").Value = "Mix" Then
                        If intSupCode <> 23 Then
                            dblAvgCost = dblItemCost
                        End If
                    End If
                    dblAvgCost = Math.Round(dblAvgCost, 2)

                    strLineNo = ""
                    strRateCode = ""
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblNoneOrdersDtls WHERE OrderNo = '" & Rs.Fields("OrderNo").Value & "' AND RefNo = '" & Rs.Fields("RefNo").Value & "' AND Side = '" & Rs.Fields("Side").Value & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        strLineNo = rsComSql.Fields("NLineNo").Value
                        strRateCode = rsComSql.Fields("CutChg").Value
                    End If
                    rsComSql = Nothing

                    strRateCode = Rs.Fields("RateCode").Value

                    vRateValue = PFGetValueCharges(strRateCode)

                    If strParcelType = "Rough" Then
                        dblAvgCost = dblItemCost
                    Else
                        dblAvgCost = dblItemCost
                    End If

                    dblItemCost = Math.Round(dblItemCost, 2)

                    flxDetails.Rows.Add(cmbDepartment.Text, strOrgAssort, dblItemCost, Rs.Fields("OrderNo").Value, Rs.Fields("ParNo").Value, Rs.Fields("Pcs").Value,
                                        Format(Rs.Fields("Cts").Value, "#0.000"), vStByCt, vRateValue, strRateCode, Rs.Fields("Pcs").Value, Format(dblRghCts, "#0.000"),
                                        vYield, "0", False, "0", Rs.Fields("RefNo").Value, strOrgAssort, Rs.Fields("PackNo").Value, strLineNo, dblAvgCost, dblHardCost, strSupParNo, strLotNo, Rs.Fields("Side").Value)
                End If

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        
        Rs = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        cmbDepartment.Text = ""
        txtOrder.Text = ""
        optNew.Checked = True
        flxDetails.Rows.Clear()
        txtPcs.Text = "0"
        txtCts.Text = "0"
        txtRghPcs.Text = "0"
        txtRghCts.Text = "0"
        txtTotValue.Text = "0"
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(14, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(14, intRow).Value = False
            Next
        End If
        txtPcs.Text = CalTotalPcs(flxDetails, 5)
        txtCts.Text = CalTotalCts(flxDetails, 6)

        txtRghPcs.Text = CalTotalPcs(flxDetails, 10)
        txtRghCts.Text = CalTotalCts(flxDetails, 11)

        txtTotValue.Text = CalTotalValue(flxDetails, 11, 2)
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCol As Integer) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(14).EditedFormattedValue = True Or flxSample.Rows(intRow).Cells(14).EditedFormattedValue = 1 Then
                CalTotalPcs = CalTotalPcs + Val(flxSample.Item(intCol, intRow).Value)
            End If
        Next
    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCol As Integer) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(14).EditedFormattedValue = True Or flxSample.Rows(intRow).Cells(14).EditedFormattedValue = 1 Then
                CalTotalCts = CalTotalCts + Val(flxSample.Item(intCol, intRow).Value)
            End If
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Function CalTotalValue(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCol As Integer, ByVal intCol2 As Integer) As Double
        Dim intRow As Integer

        CalTotalValue = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(14).EditedFormattedValue = True Or flxSample.Rows(intRow).Cells(14).EditedFormattedValue = 1 Then
                CalTotalValue = CalTotalValue + Val(flxSample.Item(intCol, intRow).Value) * Val(flxSample.Item(intCol2, intRow).Value)
            End If
        Next
        CalTotalValue = Math.Round(CalTotalValue, 2)
    End Function

    Private Sub cmdCheck_Click(sender As Object, e As EventArgs) Handles cmdCheck.Click
        Dim intRow As Integer
        Dim intCol As Integer

        If chkSup.Checked = True Then
            intCol = 3
        Else
            intCol = 4
        End If

        If txtOrder.Text <> "" Then
            If flxDetails.Rows.Count > 0 Then
                If cmbDepartment.Text = "SizeExports" Then
                    For intRow = 0 To flxDetails.Rows.Count - 1
                        If UCase(flxDetails.Item(intCol, intRow).Value) = UCase(txtOrder.Text) Then
                            If optSRW.Checked = True Then
                                If UCase(Mid(flxDetails.Item(1, intRow).Value, 1, 3)) = "SRW" Then
                                    flxDetails.Item(14, intRow).Value = True
                                End If
                            ElseIf optSSW.Checked = True Then
                                If UCase(Mid(flxDetails.Item(1, intRow).Value, 1, 3)) = "SSW" Then
                                    flxDetails.Item(14, intRow).Value = True
                                End If
                            ElseIf optSRR.Checked = True Then
                                If UCase(Mid(flxDetails.Item(1, intRow).Value, 1, 3)) = "SRR" Then
                                    flxDetails.Item(14, intRow).Value = True
                                End If
                            ElseIf optARW.Checked = True Then
                                If UCase(Mid(flxDetails.Item(1, intRow).Value, 1, 3)) = "ARW" Then
                                    flxDetails.Item(14, intRow).Value = True
                                End If
                            Else
                                If UCase(Mid(flxDetails.Item(1, intRow).Value, 1, 2)) <> "AR" And UCase(Mid(flxDetails.Item(1, intRow).Value, 1, 2)) <> "SR" And UCase(Mid(flxDetails.Item(1, intRow).Value, 1, 2)) <> "SS" Then
                                    flxDetails.Item(14, intRow).Value = True
                                End If
                            End If
                        End If
                    Next
                Else
                    For intRow = 0 To flxDetails.Rows.Count - 1
                        If UCase(flxDetails.Item(intCol, intRow).Value) = UCase(txtOrder.Text) Then
                            flxDetails.Item(14, intRow).Value = True
                        End If
                    Next
                End If
                txtPcs.Text = CalTotalPcs(flxDetails, 5)
                txtCts.Text = CalTotalCts(flxDetails, 6)

                txtRghPcs.Text = CalTotalPcs(flxDetails, 10)
                txtRghCts.Text = CalTotalCts(flxDetails, 11)

                txtTotValue.Text = CalTotalValue(flxDetails, 11, 2)
            End If
            txtOrder.Text = ""
        End If
    End Sub

    Private Sub cmdUnCheck_Click(sender As Object, e As EventArgs) Handles cmdUnCheck.Click
        Dim intRow As Integer
        Dim intCol As Integer

        If chkSup.Checked = True Then
            intCol = 3
        Else
            intCol = 4
        End If

        If txtOrder.Text <> "" Then
            If flxDetails.Rows.Count > 0 Then
                If cmbDepartment.Text = "SizeExports" Then
                    For intRow = 0 To flxDetails.Rows.Count - 1
                        If UCase(flxDetails.Item(intCol, intRow).Value) = UCase(txtOrder.Text) Then
                            If optSRW.Checked = True Then
                                If UCase(Mid(flxDetails.Item(1, intRow).Value, 1, 3)) = "SRW" Then
                                    flxDetails.Item(14, intRow).Value = False
                                End If
                            ElseIf optSSW.Checked = True Then
                                If UCase(Mid(flxDetails.Item(1, intRow).Value, 1, 3)) = "SSW" Then
                                    flxDetails.Item(14, intRow).Value = False
                                End If
                            ElseIf optSRR.Checked = True Then
                                If UCase(Mid(flxDetails.Item(1, intRow).Value, 1, 3)) = "SRR" Then
                                    flxDetails.Item(14, intRow).Value = False
                                End If
                            ElseIf optARW.Checked = True Then
                                If UCase(Mid(flxDetails.Item(1, intRow).Value, 1, 3)) = "ARW" Then
                                    flxDetails.Item(14, intRow).Value = False
                                End If
                            Else
                                If UCase(Mid(flxDetails.Item(1, intRow).Value, 1, 2)) <> "AR" And UCase(Mid(flxDetails.Item(1, intRow).Value, 1, 2)) <> "SR" Then
                                    flxDetails.Item(14, intRow).Value = False
                                End If
                            End If
                        End If
                    Next
                Else
                    For intRow = 0 To flxDetails.Rows.Count - 1
                        If UCase(flxDetails.Item(intCol, intRow).Value) = UCase(txtOrder.Text) Then
                            flxDetails.Item(14, intRow).Value = False
                        End If
                    Next
                End If
                txtPcs.Text = CalTotalPcs(flxDetails, 5)
                txtCts.Text = CalTotalCts(flxDetails, 6)

                txtRghPcs.Text = CalTotalPcs(flxDetails, 10)
                txtRghCts.Text = CalTotalCts(flxDetails, 11)

                txtTotValue.Text = CalTotalValue(flxDetails, 11, 2)
            End If
            txtOrder.Text = ""
        End If
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        If chkSup.Checked = True Then
            txtOrder.Text = flxDetails.Item(3, flxDetails.CurrentRow.Index).Value
        Else
            txtOrder.Text = flxDetails.Item(4, flxDetails.CurrentRow.Index).Value
        End If
    End Sub

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 14 Then
            txtPcs.Text = CalTotalPcs(flxDetails, 5)
            txtCts.Text = CalTotalCts(flxDetails, 6)

            txtRghPcs.Text = CalTotalPcs(flxDetails, 10)
            txtRghCts.Text = CalTotalCts(flxDetails, 11)

            txtTotValue.Text = CalTotalValue(flxDetails, 11, 2)
        End If
    End Sub

    Private Sub optNew_CheckedChanged(sender As Object, e As EventArgs) Handles optNew.CheckedChanged
        txtOrder.Text = ""
        flxDetails.Rows.Clear()
        txtPcs.Text = "0"
        txtCts.Text = "0"

        txtRghPcs.Text = "0"
        txtRghCts.Text = "0"
    End Sub

    Private Sub optEdit_CheckedChanged(sender As Object, e As EventArgs) Handles optEdit.CheckedChanged
        txtOrder.Text = ""
        flxDetails.Rows.Clear()
        txtPcs.Text = "0"
        txtCts.Text = "0"

        txtRghPcs.Text = "0"
        txtRghCts.Text = "0"
    End Sub

    Private Sub flxDetails_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellValueChanged
        txtPcs.Text = CalTotalPcs(flxDetails, 5)
        txtCts.Text = CalTotalCts(flxDetails, 6)

        txtRghPcs.Text = CalTotalPcs(flxDetails, 10)
        txtRghCts.Text = CalTotalCts(flxDetails, 11)

        txtTotValue.Text = CalTotalValue(flxDetails, 11, 2)
    End Sub

    Private Sub txtPackNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPackNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub cmdCheck2_Click(sender As Object, e As EventArgs) Handles cmdCheck2.Click
        Dim intRow As Integer

        If cmbDepartment.Text = "SizeExports" Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(18, intRow).Value = txtPackNo.Text Then
                    flxDetails.Item(14, intRow).Value = True
                End If
            Next
        ElseIf cmbDepartment.Text = "ProcessRejects" Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(18, intRow).Value = txtPackNo.Text Then
                    flxDetails.Item(14, intRow).Value = True
                End If
            Next
        End If
        txtPcs.Text = CalTotalPcs(flxDetails, 5)
        txtCts.Text = CalTotalCts(flxDetails, 6)

        txtRghPcs.Text = CalTotalPcs(flxDetails, 10)
        txtRghCts.Text = CalTotalCts(flxDetails, 11)

        txtTotValue.Text = CalTotalValue(flxDetails, 11, 2)
    End Sub

    Private Sub cmdUnCheck2_Click(sender As Object, e As EventArgs) Handles cmdUnCheck2.Click
        Dim intRow As Integer

        If cmbDepartment.Text = "SizeExports" Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(18, intRow).Value = txtPackNo.Text Then
                    flxDetails.Item(14, intRow).Value = False
                End If
            Next
        ElseIf cmbDepartment.Text = "ProcessRejects" Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(18, intRow).Value = txtPackNo.Text Then
                    flxDetails.Item(14, intRow).Value = False
                End If
            Next
        End If
        txtPcs.Text = CalTotalPcs(flxDetails, 5)
        txtCts.Text = CalTotalCts(flxDetails, 6)

        txtRghPcs.Text = CalTotalPcs(flxDetails, 10)
        txtRghCts.Text = CalTotalCts(flxDetails, 11)

        txtTotValue.Text = CalTotalValue(flxDetails, 11, 2)
    End Sub
End Class