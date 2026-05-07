
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLCosting

    Private Sub frm_DCLCosting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_DepartmentExp(cmbDepartment)
        Load_Client()

        dtpInvDate.Value = Date.Now
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmbDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDepartment.SelectedIndexChanged
        cmbReference.Items.Clear()
        cmbReference.Text = ""
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Reference1 FROM tblExportVarification WHERE Status = 'A' AND Department = '" & cmbDepartment.Text & "' GROUP BY Reference1 ORDER BY Reference1", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbReference.Items.Add(rsComSql.Fields("Reference1").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        AdoCN.Execute("DELETE FROM tblCostingTemp")
        AdoCN.Execute("DELETE FROM tblCostingTemp2")
    End Sub

    Private Sub cmbReference_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbReference.SelectedIndexChanged
        If txtExportNo.Text <> "" Then
            If txtMargin.Text = "" Then
                MsgBox("Please enter the Margin", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtMargin.Focus()
            End If
            If IsNumeric(txtMargin.Text) = False Then
                MsgBox("Invalid Margin", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtMargin.Focus()
            End If

            Select Case cmbDepartment.Text
                Case "Baguettes", "Princess", "Emerald", "Opening", "Lamour", "Davinci", "Carrer", "Baguettes2", "Baguettes3", "Princess2", "Emerald2", "Emerald3", "Rounds4", "RoundsNLE", "Asscher", "Radiant"
                    If chkReturn.Checked = False Then
                        Load_FancyRecords(cmbReference.Text)
                    Else
                        Load_FancyRecordsReturn(cmbReference.Text)
                    End If
                Case "Rounds"
                    If chkReturn.Checked = False Then
                        Load_RoundsRecords(cmbReference.Text)
                    Else
                        Load_RoundsRecordsReturn(cmbReference.Text)
                    End If
                Case "RoundsOrders"
                    Load_RoundsOrdersRecords()
                Case "Colombo Niru"
                    Load_ColomboNiruRecords()
                Case "Contract"
                    Load_ContractRecords()
                Case "Precision"
                    Load_PrecisionRecords()
                Case "SizeExports"
                    Load_SizeExportsRecords(cmbReference.Text)
                Case "ProcessReject"
                    Load_ProcessRejectRecords(cmbReference.Text)
                Case "RoughSales"
                    Load_RoughSalesRecords()
                Case "PolishBox", "PolishBoxTrf"
                    If chkNew.Checked = True Then
                        Load_PolishBoxRecordsNew()
                    Else
                        Load_PolishBoxRecords()
                    End If
                Case "Exports"
                    Load_ExportRecords()
                Case "GradingPCU_N"
                    Load_GradingPCU_NRecords(cmbReference.Text)
                Case "Mix"
                    Load_MixRecords(cmbReference.Text)
                Case "KIT Box"
                    Load_KitRecords(cmbReference.Text)
                Case "MixRefer"
                    Load_MixRecords(cmbReference.Text)
                Case Else
                    MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End Select
            txtPcs.Text = CalTotalPcs(flxDetails, 7)
            txtCts.Text = CalTotalCts(flxDetails, 8)
            txtTotPcs.Text = CalTotalPcs(flxDetails, 12)
            txtTotCts.Text = CalTotalCts(flxDetails, 13)
            txtLabour.Text = CalTotalCts(flxDetails, 17)
            txtGrLabour.Text = CalTotalCts(flxDetails, 44)
            txtTotValue.Text = CalTotalValue(flxDetails, 18)
        Else
            MsgBox("Please enter the Export No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            txtExportNo.Focus()
        End If
    End Sub

    Private Sub Load_FancyRecords(ByVal strReference As String)
        Dim rsExpVary As New ADODB.Recordset
        Dim rstImpInfo As New ADODB.Recordset
        Dim rstExpInfo As New ADODB.Recordset
        Dim rstCheckAssort As New ADODB.Recordset
        Dim vLabour, vNFE, vCost, vSubTotal As Double
        Dim sMFG, sImpDate, sBOINo As String
        Dim vImpInvNo As Integer
        Dim vExpPcs, vCurrPcs As Integer
        Dim vMasterPcs As Integer
        Dim vMasterCts As Double
        Dim vSendPcs As Integer
        Dim vSendCts As Double
        Dim vItmCost As Double
        Dim vSupParNo As String
        Dim strLotID As String
        Dim dblApcuPcs As Double
        Dim dblApcuCts As Double
        Dim dblImpPrice As Double

        mStrSQL = ""
        rsExpVary = New ADODB.Recordset
        mStrSQL = "SELECT Assortment, Price, Reference1, Reference2, ExpPcs, ExpCts, StCt, Charges, Request, RoughPcs, RoughCts, Yield, Status, OrderRef, (ExpPcs * Charges) AS Labour " & _
                  "FROM dbo.tblExportVarification WHERE (Status = 'A') AND Department = '" & cmbDepartment.Text & "' AND Reference1 = '" & strReference & "' ORDER BY Reference2"
        rsExpVary.Open(mStrSQL, AdoCN, 1, 1)
        If rsExpVary.RecordCount Then
            rsExpVary.MoveFirst()
            While Not rsExpVary.EOF
                vLabour = 0
                vCurrPcs = 0
                vExpPcs = 0
                vMasterCts = 0
                vMasterPcs = 0
                vItmCost = 0
                vSupParNo = ""
                sMFG = ""
                sImpDate = ""
                sBOINo = ""
                strLotID = ""
                dblImpPrice = 0

                rstCheckAssort = New ADODB.Recordset
                mStrSQL = "SELECT ParcelType FROM tblImport WHERE (AssortmentNo = '" & rsExpVary.Fields("Assortment").Value & "')"
                rstCheckAssort.Open(mStrSQL, AdoCN, 1, 1)
                If Not rstCheckAssort.EOF Then
                    rstExpInfo = New ADODB.Recordset
                    mStrSQL = "SELECT SUM(RoughPcs) AS SendPcs, SUM(RoughCts) AS SendCts From tblCosting WHERE Reference1 = '" & rsExpVary.Fields("Reference1").Value & "'"
                    rstExpInfo.Open(mStrSQL, AdoCN, 1, 1)
                    If Not rstExpInfo.EOF Then
                        vSendPcs = rsExpVary.Fields("RoughPcs").Value + IIf(IsDBNull((rstExpInfo.Fields("SendPcs").Value)), "0", (rstExpInfo.Fields("SendPcs").Value))
                        vSendCts = rsExpVary.Fields("RoughCts").Value + IIf(IsDBNull((rstExpInfo.Fields("SendCts").Value)), "0", (rstExpInfo.Fields("SendCts").Value))
                    End If
                    rstExpInfo = Nothing
                End If
                rstCheckAssort = Nothing

                rstCheckAssort = New ADODB.Recordset
                mStrSQL = "SELECT ParcelType FROM tblImport WHERE (AssortmentNo = '" & Trim(rsExpVary.Fields("Assortment").Value) & "')"
                rstCheckAssort.Open(mStrSQL, AdoCN, 1, 1)
                If rstCheckAssort.RecordCount Then
                    rstImpInfo = New ADODB.Recordset
                    mStrSQL = "SELECT SupplierRefNo,CompanyRefNo,InvoiceDate,AssortmentNo,ACTPcs,ACtCts,RemPcs,RemCts,BOINo,SupParcelNo,DclParcelNo,ItemCost,LotNo,ImpPrice " & _
                              "FROM tblImport WHERE (SupParcelNo = '" & rsExpVary.Fields("Reference1").Value & "') ORDER BY InvoiceDate"
                    rstImpInfo.Open(mStrSQL, AdoCN, 1, 1)
                    If rstImpInfo.RecordCount Then
                        vMasterPcs = vMasterPcs + Val(rstImpInfo.Fields("ACTPcs").Value)
                        vMasterCts = vMasterCts + Val(rstImpInfo.Fields("ACTCts").Value)
                        sMFG = rstImpInfo.Fields("SupplierRefNo").Value
                        sBOINo = rstImpInfo.Fields("BOINo").Value
                        sImpDate = rstImpInfo.Fields("InvoiceDate").Value
                        vImpInvNo = rstImpInfo.Fields("CompanyRefNo").Value
                        vItmCost = rstImpInfo.Fields("ItemCost").Value
                        dblImpPrice = rstImpInfo.Fields("ImpPrice").Value
                        vSupParNo = rstImpInfo.Fields("SupParcelNo").Value
                        strLotID = rstImpInfo.Fields("LotNo").Value
                    End If
                    rstImpInfo = Nothing
                End If
                rstCheckAssort = Nothing

                dblApcuPcs = 0
                dblApcuCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(RetPcs) AS RetPcs, SUM(RetCts) AS RetCts " & _
                              "FROM dbo.tblExpSizingReturns " & _
                              "WHERE (Department = '" & cmbDepartment.Text & "') AND (ParNo = '" & rsExpVary("Reference2").Value & "')", AdoCN, 1, 1)
                If Not IsDBNull(rsComSql.Fields("RetPcs").Value) Then
                    dblApcuPcs = rsComSql.Fields("RetPcs").Value
                    dblApcuCts = rsComSql.Fields("RetCts").Value
                End If
                rsComSql = Nothing

                dblExtLabour = 0
                vLabour = PFGetLabourCharges(rsExpVary.Fields("Request").Value, rsExpVary.Fields("ExpPcs").Value + dblApcuPcs, rsExpVary.Fields("RoughCts").Value + dblApcuCts)
                vNFE = Math.Round(rsExpVary.Fields("Price").Value * Math.Round(rsExpVary.Fields("RoughCts").Value, 3), 2)

                vSubTotal = vLabour + vNFE + dblExtLabour
                vSubTotal = Math.Round(vSubTotal, 2)
                If rsExpVary.Fields("ExpCts").Value <> 0 Then
                    vCost = Format(Val(vSubTotal / rsExpVary.Fields("ExpCts").Value), "#0.#0")
                Else
                    vCost = 0
                End If

                vMasterPcs = rsExpVary.Fields("RoughPcs").Value + dblApcuPcs
                vMasterCts = rsExpVary.Fields("RoughCts").Value + dblApcuCts

                flxDetails.Rows.Add(cmbDepartment.Text, rsExpVary.Fields("Assortment").Value, sMFG, sImpDate, vImpInvNo, rsExpVary.Fields("Reference1").Value,
                                    rsExpVary.Fields("Reference2").Value, rsExpVary("RoughPcs").Value, Format(rsExpVary("RoughCts").Value, "#0.000"),
                                    rsExpVary.Fields("Price").Value, Format(dtpInvDate.Value, "MM/dd/yyyy"), txtExportNo.Text, rsExpVary.Fields("ExpPcs").Value, Format(rsExpVary.Fields("ExpCts").Value, "#0.000"),
                                    Math.Round(rsExpVary.Fields("StCt").Value, 2), rsExpVary.Fields("Charges").Value, rsExpVary.Fields("Yield").Value, vLabour, vNFE, vCost, vSubTotal,
                                    vMasterPcs - rsExpVary.Fields("RoughPcs").Value, Format(vMasterCts - rsExpVary.Fields("RoughCts").Value, "#0.000"), vMasterPcs, Format(vMasterCts, "#0.000"),
                                    vSendPcs, Format(vSendCts, "#0.#00"), "", "", "", "", cmbDepartment.Text, sBOINo, "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", strLotID)

                Select Case cmbDepartment.Text
                    Case "Baguettes2", "Baguettes3", "Princess2", "Emerald2", "Emerald3", "Rounds4"
                        dblExtLabour = 0
                End Select
                flxDetails.Item(56, flxDetails.Rows.Count - 1).Value = dblExtLabour
                flxDetails.Item(57, flxDetails.Rows.Count - 1).Value = "0"
                flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = Math.Round((rsExpVary.Fields("Price").Value / vItmCost) * dblImpPrice, 2)

                rsExpVary.MoveNext()
            End While
        End If
        rsExpVary = Nothing
    End Sub

    Private Sub Load_FancyRecordsReturn(ByVal strReference As String)
        Dim rsExpVary As New ADODB.Recordset
        Dim rstImpInfo As New ADODB.Recordset
        Dim rstExpInfo As New ADODB.Recordset
        Dim rstCheckAssort As New ADODB.Recordset
        Dim vLabour, vNFE, vCost, vSubTotal As Double
        Dim sMFG, sImpDate, sBOINo As String
        Dim vImpInvNo As Integer
        Dim vExpPcs, vCurrPcs As Integer
        Dim vMasterPcs As Integer
        Dim vMasterCts As Double
        Dim vSendPcs As Integer
        Dim vSendCts As Double
        Dim vItmCost As Double
        Dim vSupParNo As String
        Dim strLotID As String
        Dim dblImpPrice As Double

        mStrSQL = ""
        rsExpVary = New ADODB.Recordset
        mStrSQL = "SELECT Assortment, SUM(Price) AS Price, Reference1, MIN(Reference2) AS Reference2, SUM(ExpPcs) AS ExpPcs, SUM(ExpCts) AS ExpCts, SUM(StCt) AS StCt, SUM(Charges) AS Charges, " & _
                    "MIN(Request) AS Request, SUM(RoughPcs) AS RoughPcs, SUM(RoughCts) AS RoughCts, SUM(ExpCts) / SUM(RoughCts) AS Yield, Status, OrderRef " & _
                  "FROM tblExportVarification " & _
                  "WHERE Department = '" & cmbDepartment.Text & "' AND Reference1 = '" & strReference & "' AND (RIGHT(Reference2, 1) = 'N' OR RIGHT(Reference2, 1) = 'V') " & _
                  "GROUP BY Assortment, Reference1, Charges, Status, OrderRef HAVING (Status = 'A') "

        rsExpVary.Open(mStrSQL, AdoCN, 1, 1)

        If rsExpVary.RecordCount > 1 Then
            mStrSQL = "SELECT Assortment,Price,Reference1, Reference2, ExpPcs, ExpCts, StCt,Charges, Request, RoughPcs, RoughCts, Yield, Status,OrderRef, (ExpPcs * Charges) / 2 AS Labour " & _
                      "FROM dbo.tblExportVarification " & _
                      "WHERE (Status = 'A') AND Department = '" & cmbDepartment.Text & "' AND Reference1 = '" & strReference & "' AND (RIGHT(Reference2, 1) = 'N' OR RIGHT(Reference2, 1) = 'V') " & _
                      "ORDER BY Assortment"
        Else
            mStrSQL = "SELECT Assortment, AVG(Price) AS Price, Reference1, Reference2,SUM(ExpPcs) AS ExpPcs, SUM(ExpCts) AS ExpCts, AVG(StCt) AS StCt, AVG(Charges) AS Charges," & _
                        "MAX(Request) AS Request, SUM(RoughPcs) AS RoughPcs, SUM(RoughCts) AS RoughCts,MAX(Yield) AS Yield, Status, OrderRef, SUM(ExpPcs * Charges) / 2 As Labour " & _
                      "FROM dbo.tblExportVarification " & _
                      "WHERE (Department = '" & cmbDepartment.Text & "') AND Reference1 = '" & strReference & "' AND (RIGHT(Reference2, 1) = 'N' OR RIGHT(Reference2, 1) = 'V') " & _
                      "GROUP BY Assortment, Reference1, Reference2, Status, OrderRef " & _
                      "HAVING (Status = 'A') ORDER BY Assortment"
        End If
        rsExpVary = Nothing

        rsExpVary = New ADODB.Recordset
        rsExpVary.Open(mStrSQL, AdoCN, 1, 1)
        Do While Not rsExpVary.EOF
            vLabour = 0
            vCurrPcs = 0
            vExpPcs = 0
            vMasterCts = 0
            vMasterPcs = 0
            vItmCost = 0
            vSupParNo = ""
            sMFG = ""
            sImpDate = ""
            sBOINo = ""
            strLotID = ""
            dblImpPrice = 0

            rstCheckAssort = New ADODB.Recordset
            mStrSQL = "SELECT ParcelType FROM tblImport WHERE (AssortmentNo = '" & rsExpVary.Fields("Assortment").Value & "')"
            rstCheckAssort.Open(mStrSQL, AdoCN, 1, 1)
            If Not rstCheckAssort.EOF Then
                rstExpInfo = New ADODB.Recordset
                mStrSQL = "SELECT SUM(RoughPcs) AS SendPcs, SUM(RoughCts) AS SendCts From tblCosting WHERE Reference1 = '" & rsExpVary.Fields("Reference1").Value & "'"
                rstExpInfo.Open(mStrSQL, AdoCN, 1, 1)
                If Not rstExpInfo.EOF Then
                    vSendPcs = rsExpVary.Fields("RoughPcs").Value + IIf(IsDBNull((rstExpInfo.Fields("SendPcs").Value)), "0", (rstExpInfo.Fields("SendPcs").Value))
                    vSendCts = rsExpVary.Fields("RoughCts").Value + IIf(IsDBNull((rstExpInfo.Fields("SendCts").Value)), "0", (rstExpInfo.Fields("SendCts").Value))
                End If
                rstExpInfo = Nothing
            End If
            rstCheckAssort = Nothing

            rstCheckAssort = New ADODB.Recordset
            mStrSQL = "SELECT ParcelType FROM tblImport WHERE (AssortmentNo = '" & rsExpVary.Fields("Assortment").Value & "')"
            rstCheckAssort.Open(mStrSQL, AdoCN, 1, 1)
            If rstCheckAssort.RecordCount Then
                rstImpInfo = New ADODB.Recordset
                mStrSQL = "SELECT SupplierRefNo, CompanyRefNo,InvoiceDate, AssortmentNo, ACTPcs,ACtCts, RemPcs, RemCts,BOINo, SupParcelNo, DclParcelNo, ItemCost, LotNo, ImpPrice " & _
                          "FROM tblImport WHERE (SupParcelNo = '" & rsExpVary.Fields("Reference1").Value & "') ORDER BY InvoiceDate"
                rstImpInfo.Open(mStrSQL, AdoCN, 1, 1)
                If rstImpInfo.RecordCount Then
                    sMFG = rstImpInfo.Fields("SupplierRefNo").Value
                    sBOINo = rstImpInfo.Fields("BOINo").Value
                    sImpDate = rstImpInfo.Fields("InvoiceDate").Value
                    vImpInvNo = rstImpInfo.Fields("CompanyRefNo").Value
                    vItmCost = rstImpInfo.Fields("ItemCost").Value
                    vSupParNo = rstImpInfo.Fields("SupParcelNo").Value
                    strLotID = rstImpInfo.Fields("LotNo").Value
                    dblImpPrice = rstImpInfo.Fields("ImpPrice").Value
                End If
                rstImpInfo = Nothing
            End If
            rstCheckAssort = Nothing

            dblExtLabour = 0
            vLabour = PFGetLabourCharges(rsExpVary.Fields("Request").Value, rsExpVary.Fields("ExpPcs").Value, rsExpVary.Fields("RoughCts").Value)
            vNFE = Math.Round(rsExpVary.Fields("Price").Value * Math.Round(rsExpVary.Fields("RoughCts").Value, 3), 2)

            vSubTotal = vLabour + vNFE + dblExtLabour
            vSubTotal = Math.Round(vSubTotal, 2)
            If rsExpVary.Fields("ExpCts").Value <> 0 Then
                vCost = Format(Val(vSubTotal / rsExpVary.Fields("ExpCts").Value), "#0.#0")
            Else
                vCost = 0
            End If

            vMasterPcs = rsExpVary("RoughPcs").Value
            vMasterCts = rsExpVary("RoughCts").Value

            flxDetails.Rows.Add(cmbDepartment.Text, rsExpVary.Fields("Assortment").Value, sMFG, sImpDate, vImpInvNo, rsExpVary.Fields("Reference1").Value,
                                rsExpVary.Fields("Reference2").Value, rsExpVary("RoughPcs").Value, Format(rsExpVary("RoughCts").Value, "#0.000"),
                                rsExpVary.Fields("Price").Value, Format(dtpInvDate.Value, "MM/dd/yyyy"), txtExportNo.Text, rsExpVary.Fields("ExpPcs").Value, Format(rsExpVary.Fields("ExpCts").Value, "#0.000"),
                                Math.Round(rsExpVary.Fields("StCt").Value, 2), rsExpVary.Fields("Charges").Value, rsExpVary.Fields("Yield").Value, vLabour, vNFE, vCost, vSubTotal,
                                "0", "0", vMasterPcs, Format(vMasterCts, "#0.000"), vSendPcs, Format(vSendCts, "#0.#00"), "", "", "", "", cmbDepartment.Text, sBOINo, "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", strLotID)

            flxDetails.Item(56, flxDetails.Rows.Count - 1).Value = dblExtLabour
            flxDetails.Item(57, flxDetails.Rows.Count - 1).Value = "0"
            flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = Math.Round((rsExpVary.Fields("Price").Value / vItmCost) * dblImpPrice, 2)

            rsExpVary.MoveNext()
        Loop
        rsExpVary = Nothing
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub Load_RoundsRecords(ByVal strReference As String)
        Dim rsExpVary As New ADODB.Recordset
        Dim rstImpInfo As New ADODB.Recordset
        Dim rstExpInfo As New ADODB.Recordset
        Dim rstCheckAssort As New ADODB.Recordset
        Dim vLabour, vNFE, vCost, vSubTotal As Double
        Dim sMFG, sImpDate, sBOINo As String
        Dim vImpInvNo As Integer
        Dim vExpPcs, vCurrPcs As Integer
        Dim vMasterPcs As Integer
        Dim vMasterCts As Double
        Dim vSendPcs As Integer
        Dim vSendCts As Double
        Dim vItmCost As Double
        Dim vSupParNo As String
        Dim strLotID As String
        Dim strItem As String
        Dim intPCUPcs As Integer
        Dim dblPCUCts As Double
        Dim dblImpPrice As Double

        Dim dblNewPcs As Double
        Dim dblNewCts As Double
        Dim dblRghCts As Double

        mStrSQL = ""
        rsExpVary = New ADODB.Recordset
        mStrSQL = "SELECT Assortment, SUM(Price) AS Price, Reference1, MIN(Reference2) AS Reference2, SUM(ExpPcs) AS ExpPcs, SUM(ExpCts) AS ExpCts, SUM(StCt) AS StCt, SUM(Charges) AS Charges, " & _
                    "MIN(Request) AS Request, SUM(RoughPcs) AS RoughPcs, SUM(RoughCts) AS RoughCts, Status, OrderRef " & _
                  "FROM tblExportVarification " & _
                  "WHERE Department = '" & cmbDepartment.Text & "' AND Reference1 = '" & strReference & "' AND RIGHT(Reference2, 1) <> 'N' AND RIGHT(Reference2, 1) <> 'V' " & _
                  "GROUP BY Assortment, Reference1, Charges, Status, OrderRef HAVING (Status = 'A') "

        rsExpVary.Open(mStrSQL, AdoCN, 1, 1)

        If rsExpVary.RecordCount > 1 Then
            mStrSQL = "SELECT Assortment, Price,Reference1, Reference2, ExpPcs, ExpCts, StCt,Charges, Request, RoughPcs, RoughCts, Yield, Status,OrderRef, (ExpPcs * Charges) AS Labour " & _
                      "FROM dbo.tblExportVarification " & _
                      "WHERE (Status = 'A') AND Department = '" & cmbDepartment.Text & "' AND Reference1 = '" & strReference & "' AND RIGHT(Reference2, 1) <> 'N' AND RIGHT(Reference2, 1) <> 'V' " & _
                      "ORDER BY Assortment"
        Else
            mStrSQL = "SELECT Assortment, AVG(Price) AS Price, Reference1, Reference2 AS Reference2,SUM(ExpPcs) AS ExpPcs, SUM(ExpCts) AS ExpCts, AVG(StCt) AS StCt, AVG(Charges) AS Charges," & _
                        "MAX(Request) AS Request, SUM(RoughPcs) AS RoughPcs, SUM(RoughCts) AS RoughCts,MAX(Yield) AS Yield,Status, OrderRef, SUM(ExpPcs * Charges) As Labour " & _
                      "FROM dbo.tblExportVarification " & _
                      "WHERE (Department = '" & cmbDepartment.Text & "') AND Reference1 = '" & strReference & "' AND RIGHT(Reference2, 1) <> 'N' AND RIGHT(Reference2, 1) <> 'V' " & _
                      "GROUP BY Assortment,Reference1,Reference2,Status,OrderRef " & _
                      "HAVING (Status = 'A') ORDER BY Assortment"
        End If
        rsExpVary = Nothing

        rsExpVary = New ADODB.Recordset
        rsExpVary.Open(mStrSQL, AdoCN, 1, 1)
        Do While Not rsExpVary.EOF
            vLabour = 0
            vCurrPcs = 0
            vExpPcs = 0
            vMasterCts = 0
            vMasterPcs = 0
            vItmCost = 0
            vSupParNo = ""
            sMFG = ""
            sImpDate = ""
            sBOINo = ""
            strLotID = ""
            strItem = ""
            dblImpPrice = 0

            rstCheckAssort = New ADODB.Recordset
            mStrSQL = "SELECT ParcelType FROM tblImport WHERE (AssortmentNo = '" & rsExpVary.Fields("Assortment").Value & "')"
            rstCheckAssort.Open(mStrSQL, AdoCN, 1, 1)
            If Not rstCheckAssort.EOF Then
                rstExpInfo = New ADODB.Recordset
                mStrSQL = "SELECT SUM(RoughPcs) AS SendPcs, SUM(RoughCts) AS SendCts From tblCosting WHERE Reference1 = '" & rsExpVary.Fields("Reference1").Value & "'"
                rstExpInfo.Open(mStrSQL, AdoCN, 1, 1)
                If Not rstExpInfo.EOF Then
                    vSendPcs = rsExpVary.Fields("RoughPcs").Value + IIf(IsDBNull((rstExpInfo.Fields("SendPcs").Value)), "0", (rstExpInfo.Fields("SendPcs").Value))
                    vSendCts = rsExpVary.Fields("RoughCts").Value + IIf(IsDBNull((rstExpInfo.Fields("SendCts").Value)), "0", (rstExpInfo.Fields("SendCts").Value))
                End If
                rstExpInfo = Nothing
            End If
            rstCheckAssort = Nothing

            rstCheckAssort = New ADODB.Recordset
            mStrSQL = "SELECT ParcelType FROM tblImport WHERE (AssortmentNo = '" & rsExpVary.Fields("Assortment").Value & "')"
            rstCheckAssort.Open(mStrSQL, AdoCN, 1, 1)
            If rstCheckAssort.RecordCount Then
                rstImpInfo = New ADODB.Recordset
                mStrSQL = "SELECT SupplierRefNo, CompanyRefNo,InvoiceDate, AssortmentNo, ACTPcs,ACtCts, RemPcs, RemCts,BOINo, SupParcelNo, DclParcelNo, ItemCost, LotNo, Article, ImpPrice " & _
                          "FROM tblImport WHERE (SupParcelNo = '" & rsExpVary.Fields("Reference1").Value & "') ORDER BY InvoiceDate"
                rstImpInfo.Open(mStrSQL, AdoCN, 1, 1)
                If rstImpInfo.RecordCount Then
                    vMasterPcs = vMasterPcs + Val(rstImpInfo.Fields("ACTPcs").Value)
                    vMasterCts = vMasterCts + Val(rstImpInfo.Fields("ACTCts").Value)
                    sMFG = rstImpInfo.Fields("SupplierRefNo").Value
                    sBOINo = rstImpInfo.Fields("BOINo").Value
                    sImpDate = rstImpInfo.Fields("InvoiceDate").Value
                    vImpInvNo = rstImpInfo.Fields("CompanyRefNo").Value
                    vItmCost = rstImpInfo.Fields("ItemCost").Value
                    vSupParNo = rstImpInfo.Fields("SupParcelNo").Value
                    strItem = rstImpInfo.Fields("Article").Value & ""
                    strLotID = rstImpInfo.Fields("LotNo").Value
                    dblImpPrice = rstImpInfo.Fields("ImpPrice").Value
                End If
                rstImpInfo = Nothing
            End If
            rstCheckAssort = Nothing

            dblExtLabour = 0
            vLabour = PFGetLabourCharges(rsExpVary.Fields("Request").Value, rsExpVary.Fields("ExpPcs").Value, rsExpVary.Fields("RoughCts").Value)
            vNFE = Math.Round(rsExpVary.Fields("Price").Value * Math.Round(rsExpVary.Fields("RoughCts").Value, 3), 2)

            vSubTotal = vLabour + vNFE + dblExtLabour
            vSubTotal = Math.Round(vSubTotal, 2)
            If rsExpVary.Fields("ExpCts").Value <> 0 Then
                vCost = Format(Val(vSubTotal / rsExpVary.Fields("ExpCts").Value), "#0.#0")
            Else
                vCost = 0
            End If

            intPCUPcs = 0
            dblPCUCts = 0
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM VW_GradingRghIssuesAll WHERE Department = 'Rounds' AND LEFT(ParNo, 6) = '" & rsExpVary.Fields("Reference2").Value & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                intPCUPcs = rsComSql.Fields("IssPcs").Value
                dblPCUCts = rsComSql.Fields("IssCts").Value
            End If
            rsComSql = Nothing

            vMasterPcs = rsExpVary.Fields("RoughPcs").Value + intPCUPcs
            vMasterCts = rsExpVary.Fields("RoughCts").Value + dblPCUCts

            If chkPack.Checked = True Then
                'Check Packing List Count
                dblNewPcs = 0
                dblNewCts = 0
                dblRghCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT PackNo, SUM(Pcs) AS Pcs, SUM(Cts) AS Cts FROM dbo.VW_GradingPackingListCOLM WHERE (LotNo = '" & strLotID & "') GROUP BY PackNo ORDER BY PackNo", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                            dblNewPcs = rsComSql.Fields("Pcs").Value
                            dblNewCts = rsComSql.Fields("Cts").Value
                            dblRghCts = Math.Round((rsExpVary("RoughCts").Value / rsExpVary("RoughPcs").Value) * rsComSql.Fields("Pcs").Value, 3)
                        End If

                        dblExtLabour = 0
                        vLabour = PFGetLabourCharges(rsExpVary.Fields("Request").Value, dblNewPcs, dblRghCts)
                        vNFE = Math.Round(rsExpVary.Fields("Price").Value * Math.Round(dblRghCts, 3), 2)

                        vSubTotal = vLabour + vNFE + dblExtLabour
                        vSubTotal = Math.Round(vSubTotal, 2)
                        If dblNewCts <> 0 Then
                            vCost = Format(Val(vSubTotal / dblNewCts), "#0.#0")
                        Else
                            vCost = 0
                        End If

                        flxDetails.Rows.Add(cmbDepartment.Text, rsExpVary.Fields("Assortment").Value, sMFG, sImpDate, vImpInvNo, rsExpVary.Fields("Reference1").Value,
                                        Mid(rsExpVary.Fields("Reference2").Value, 1, 6), dblNewPcs, Format(dblRghCts, "#0.000"),
                                        rsExpVary.Fields("Price").Value, Format(dtpInvDate.Value, "MM/dd/yyyy"), txtExportNo.Text, dblNewPcs, Format(dblNewCts, "#0.000"),
                                        Math.Round(rsExpVary.Fields("StCt").Value, 2), rsExpVary.Fields("Charges").Value, rsExpVary.Fields("Yield").Value, vLabour, vNFE, vCost, vSubTotal,
                                        "0", "0", vMasterPcs, Format(vMasterCts, "#0.000"), vSendPcs, Format(vSendCts, "#0.#00"), "", "", "", "", cmbDepartment.Text, sBOINo, "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", strItem, "0", strLotID)

                        flxDetails.Item(56, flxDetails.Rows.Count - 1).Value = dblExtLabour
                        flxDetails.Item(57, flxDetails.Rows.Count - 1).Value = "0"
                        flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = Math.Round((rsExpVary.Fields("Price").Value / vItmCost) * dblImpPrice, 2)

                        rsComSql.MoveNext()
                    End While
                End If
                rsComSql = Nothing
            Else
                flxDetails.Rows.Add(cmbDepartment.Text, rsExpVary.Fields("Assortment").Value, sMFG, sImpDate, vImpInvNo, rsExpVary.Fields("Reference1").Value,
                                Mid(rsExpVary.Fields("Reference2").Value, 1, 6), rsExpVary("RoughPcs").Value, Format(rsExpVary("RoughCts").Value, "#0.000"),
                                rsExpVary.Fields("Price").Value, Format(dtpInvDate.Value, "MM/dd/yyyy"), txtExportNo.Text, rsExpVary.Fields("ExpPcs").Value, Format(rsExpVary.Fields("ExpCts").Value, "#0.000"),
                                Math.Round(rsExpVary.Fields("StCt").Value, 2), rsExpVary.Fields("Charges").Value, rsExpVary.Fields("Yield").Value, vLabour, vNFE, vCost, vSubTotal,
                                "0", "0", vMasterPcs, Format(vMasterCts, "#0.000"), vSendPcs, Format(vSendCts, "#0.#00"), "", "", "", "", cmbDepartment.Text, sBOINo, "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", strItem, "0", strLotID)

                flxDetails.Item(56, flxDetails.Rows.Count - 1).Value = dblExtLabour
                flxDetails.Item(57, flxDetails.Rows.Count - 1).Value = "0"
                flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = Math.Round((rsExpVary.Fields("Price").Value / vItmCost) * dblImpPrice, 2)
            End If

            rsExpVary.MoveNext()
        Loop
        rsExpVary = Nothing
    End Sub

    Private Sub Load_RoundsRecordsReturn(ByVal strReference As String)
        Dim rsExpVary As New ADODB.Recordset
        Dim rstImpInfo As New ADODB.Recordset
        Dim rstExpInfo As New ADODB.Recordset
        Dim rstCheckAssort As New ADODB.Recordset
        Dim vLabour, vNFE, vCost, vSubTotal As Double
        Dim sMFG, sImpDate, sBOINo As String
        Dim vImpInvNo As Integer
        Dim vExpPcs, vCurrPcs As Integer
        Dim vMasterPcs As Integer
        Dim vMasterCts As Double
        Dim vSendPcs As Integer
        Dim vSendCts As Double
        Dim vItmCost As Double
        Dim vSupParNo As String
        Dim strLotID As String
        Dim strItem As String
        Dim dblImpPrice As Double

        mStrSQL = ""
        rsExpVary = New ADODB.Recordset
        mStrSQL = "SELECT Assortment, SUM(Price) AS Price, Reference1, MIN(Reference2) AS Reference2, " & _
                    "SUM(ExpPcs) AS ExpPcs, SUM(ExpCts) AS ExpCts, SUM(StCt) AS StCt, SUM(Charges) AS Charges, " & _
                    "MIN(Request) AS Request, SUM(RoughPcs) AS RoughPcs, SUM(RoughCts) AS RoughCts, SUM(ExpCts) / SUM(RoughCts) AS Yield, Status, OrderRef " & _
                  "FROM tblExportVarification " & _
                  "WHERE Department = '" & cmbDepartment.Text & "' AND Reference1 = '" & strReference & "' AND (RIGHT(Reference2, 1) = 'N' OR RIGHT(Reference2, 1) = 'V') " & _
                  "GROUP BY Assortment, Reference1, Charges, Status, OrderRef HAVING (Status = 'A') "

        rsExpVary.Open(mStrSQL, AdoCN, 1, 1)

        If rsExpVary.RecordCount > 1 Then
            mStrSQL = "SELECT Assortment, Price,Reference1, Reference2, ExpPcs, ExpCts, StCt,Charges, Request, RoughPcs, RoughCts, Yield, Status,OrderRef, (ExpPcs * Charges) / 2 AS Labour " & _
                      "FROM dbo.tblExportVarification " & _
                      "WHERE (Status = 'A') AND Department = '" & cmbDepartment.Text & "' AND Reference1 = '" & strReference & "' AND (RIGHT(Reference2, 1) = 'N' OR RIGHT(Reference2, 1) = 'V') " & _
                      "ORDER BY Assortment"
        Else
            mStrSQL = "SELECT Assortment, AVG(Price) AS Price, Reference1, Reference2,SUM(ExpPcs) AS ExpPcs, SUM(ExpCts) AS ExpCts, AVG(StCt) AS StCt, AVG(Charges) AS Charges," & _
                        "MAX(Request) AS Request, SUM(RoughPcs) AS RoughPcs, SUM(RoughCts) AS RoughCts,MAX(Yield) AS Yield, Status, OrderRef, SUM(ExpPcs * Charges) / 2 As Labour " & _
                      "FROM dbo.tblExportVarification " & _
                      "WHERE (Department = '" & cmbDepartment.Text & "') AND Reference1 = '" & strReference & "' AND (RIGHT(Reference2, 1) = 'N' OR RIGHT(Reference2, 1) = 'V') " & _
                      "GROUP BY Assortment, Reference1, Reference2, Status, OrderRef " & _
                      "HAVING (Status = 'A') ORDER BY Assortment"
        End If
        rsExpVary = Nothing

        rsExpVary = New ADODB.Recordset
        rsExpVary.Open(mStrSQL, AdoCN, 1, 1)
        Do While Not rsExpVary.EOF
            vLabour = 0
            vCurrPcs = 0
            vExpPcs = 0
            vMasterCts = 0
            vMasterPcs = 0
            vItmCost = 0
            vSupParNo = ""
            sMFG = ""
            sImpDate = ""
            sBOINo = ""
            strLotID = ""
            strItem = ""
            dblImpPrice = 0

            rstCheckAssort = New ADODB.Recordset
            mStrSQL = "SELECT ParcelType FROM tblImport WHERE (AssortmentNo = '" & rsExpVary.Fields("Assortment").Value & "')"
            rstCheckAssort.Open(mStrSQL, AdoCN, 1, 1)
            If Not rstCheckAssort.EOF Then
                rstExpInfo = New ADODB.Recordset
                mStrSQL = "SELECT SUM(RoughPcs) AS SendPcs, SUM(RoughCts) AS SendCts From tblCosting WHERE Reference1 = '" & rsExpVary.Fields("Reference1").Value & "'"
                rstExpInfo.Open(mStrSQL, AdoCN, 1, 1)
                If Not rstExpInfo.EOF Then
                    vSendPcs = rsExpVary.Fields("RoughPcs").Value + IIf(IsDBNull((rstExpInfo.Fields("SendPcs").Value)), "0", (rstExpInfo.Fields("SendPcs").Value))
                    vSendCts = rsExpVary.Fields("RoughCts").Value + IIf(IsDBNull((rstExpInfo.Fields("SendCts").Value)), "0", (rstExpInfo.Fields("SendCts").Value))
                End If
                rstExpInfo = Nothing
            End If
            rstCheckAssort = Nothing

            rstCheckAssort = New ADODB.Recordset
            mStrSQL = "SELECT ParcelType FROM tblImport WHERE (AssortmentNo = '" & rsExpVary.Fields("Assortment").Value & "')"
            rstCheckAssort.Open(mStrSQL, AdoCN, 1, 1)
            If rstCheckAssort.RecordCount Then
                rstImpInfo = New ADODB.Recordset
                mStrSQL = "SELECT SupplierRefNo, CompanyRefNo,InvoiceDate, AssortmentNo, ACTPcs,ACtCts, RemPcs, RemCts,BOINo, SupParcelNo, DclParcelNo, ItemCost, LotNo, Article, ImpPrice " & _
                          "FROM tblImport WHERE (SupParcelNo = '" & rsExpVary.Fields("Reference1").Value & "') ORDER BY InvoiceDate"
                rstImpInfo.Open(mStrSQL, AdoCN, 1, 1)
                If rstImpInfo.RecordCount Then
                    sMFG = rstImpInfo.Fields("SupplierRefNo").Value
                    sBOINo = rstImpInfo.Fields("BOINo").Value
                    sImpDate = rstImpInfo.Fields("InvoiceDate").Value
                    vImpInvNo = rstImpInfo.Fields("CompanyRefNo").Value
                    vItmCost = rstImpInfo.Fields("ItemCost").Value
                    vSupParNo = rstImpInfo.Fields("SupParcelNo").Value
                    strItem = rstImpInfo.Fields("Article").Value & ""
                    strLotID = rstImpInfo.Fields("LotNo").Value
                    dblImpPrice = rstImpInfo.Fields("ImpPrice").Value
                End If
                rstImpInfo = Nothing
            End If
            rstCheckAssort = Nothing

            dblExtLabour = 0
            vLabour = PFGetLabourCharges(rsExpVary.Fields("Request").Value, rsExpVary.Fields("ExpPcs").Value, rsExpVary.Fields("RoughCts").Value)
            vNFE = Math.Round(rsExpVary.Fields("Price").Value * Math.Round(rsExpVary.Fields("RoughCts").Value, 3), 2)

            vSubTotal = vLabour + vNFE + dblExtLabour
            vSubTotal = Math.Round(vSubTotal, 2)
            If rsExpVary.Fields("ExpCts").Value <> 0 Then
                vCost = Format(Val(vSubTotal / rsExpVary.Fields("ExpCts").Value), "#0.#0")
            Else
                vCost = 0
            End If

            vMasterPcs = rsExpVary("RoughPcs").Value
            vMasterCts = rsExpVary("RoughCts").Value

            flxDetails.Rows.Add(cmbDepartment.Text, rsExpVary.Fields("Assortment").Value, sMFG, sImpDate, vImpInvNo, rsExpVary.Fields("Reference1").Value,
                                rsExpVary.Fields("Reference2").Value, rsExpVary("RoughPcs").Value, Format(rsExpVary("RoughCts").Value, "#0.000"),
                                rsExpVary.Fields("Price").Value, Format(dtpInvDate.Value, "MM/dd/yyyy"), txtExportNo.Text, rsExpVary.Fields("ExpPcs").Value, Format(rsExpVary.Fields("ExpCts").Value, "#0.000"),
                                Math.Round(rsExpVary.Fields("StCt").Value, 2), rsExpVary.Fields("Charges").Value, rsExpVary.Fields("Yield").Value, vLabour, vNFE, vCost, vSubTotal,
                                "0", "0", vMasterPcs, Format(vMasterCts, "#0.000"), vSendPcs, Format(vSendCts, "#0.#00"), "", "", "", "", cmbDepartment.Text, sBOINo, "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", strItem, "0", strLotID)

            flxDetails.Item(56, flxDetails.Rows.Count - 1).Value = dblExtLabour
            flxDetails.Item(57, flxDetails.Rows.Count - 1).Value = "0"
            flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = Math.Round((rsExpVary.Fields("Price").Value / vItmCost) * dblImpPrice, 2)

            rsExpVary.MoveNext()
        Loop
        rsExpVary = Nothing
    End Sub

    Private Sub Load_RoundsOrdersRecords()
        Dim rsExpVary As New ADODB.Recordset
        Dim rstImpInfo As New ADODB.Recordset
        Dim rstExpInfo As New ADODB.Recordset
        Dim rstCheckAssort As New ADODB.Recordset
        Dim vLabour, vNFE, vCost, vSubTotal As Double
        Dim sMFG, sImpDate, sBOINo As String
        Dim vImpInvNo As Integer
        Dim vMasterPcs As Integer
        Dim vMasterCts As Double
        Dim vSendPcs As Integer
        Dim vSendCts As Double
        Dim vItmCost As Double
        Dim vSupParNo As String
        Dim strLotID As String
        Dim strItem As String

        Dim sClientID, sOrderSubject As String

        Dim intOutPcs As Integer
        Dim dblOutCts As Double
        Dim intBalPcs As Integer
        Dim dblBalCts As Double
        Dim strOrigAssort As String
        Dim dblBaseCost As Double
        Dim dblAdjCost As Double
        Dim dblCurCost As Double
        Dim strSupParcelNo As String

        Dim dblOrderItem As Double
        Dim strCommande As String
        Dim strNiruOrder As String

        Dim dblImpPrice As Double

        mStrSQL = ""
        rsExpVary = New ADODB.Recordset
        mStrSQL = "SELECT Assortment,Price,Reference1,Reference2,ExpPcs,ExpCts,StCt,Charges,Request,RoughPcs,RoughCts,Yield,Status,OrderRef,OrigAssort,InID,NLineNo, BasePrice, AdjPrice " & _
                  "FROM dbo.tblExportVarification " & _
                  "WHERE (Status = 'A') AND Reference1 = '" & cmbReference.Text & "' AND Department = '" & cmbDepartment.Text & "' " & _
                  "ORDER BY Assortment"

        rsExpVary.Open(mStrSQL, AdoCN, 1, 1)
        Do While Not rsExpVary.EOF
            intOutPcs = 0
            dblOutCts = 0
            intBalPcs = rsExpVary.Fields("ExpPcs").Value
            dblBalCts = Math.Round(rsExpVary.Fields("ExpCts").Value, 3)
            vMasterPcs = 0
            vMasterCts = 0
            dblBaseCost = 0
            dblAdjCost = 0
            dblCurCost = 0
            strSupParcelNo = ""
            sMFG = ""
            sBOINo = ""
            sImpDate = ""
            strLotID = ""
            strItem = ""
            dblBaseCost = rsExpVary.Fields("BasePrice").Value
            dblAdjCost = rsExpVary.Fields("AdjPrice").Value

            strNiruOrder = ""
            dblOrderItem = 0
            strCommande = ""

            rstExpInfo = New ADODB.Recordset
            mStrSQL = "SELECT Assortment, SUM(RoughPcs) AS SendPcs, SUM(RoughCts) AS SendCts " & _
                     "FROM tblCosting GROUP BY Assortment HAVING Assortment = '" & rsExpVary.Fields("Assortment").Value & "'"
            rstExpInfo.Open(mStrSQL, AdoCN, 1, 1)
            If Not rstExpInfo.EOF Then
                vSendPcs = rstExpInfo.Fields("SendPcs").Value
                vSendCts = rstExpInfo.Fields("SendCts").Value
            End If
            rstExpInfo = Nothing

            sClientID = ""
            sOrderSubject = ""
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT OrderNo, NorderNo, OrderItem, COMMANDE, Subject, Niruref FROM dbo.tblNoneOrders WHERE OrderNo = '" & rsExpVary.Fields("Reference1").Value & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                strNiruOrder = rsComSql.Fields("NorderNo").Value
                dblOrderItem = rsComSql.Fields("OrderItem").Value
                strCommande = rsComSql.Fields("COMMANDE").Value
                sOrderSubject = rsComSql.Fields("Subject").Value
                sClientID = rsComSql.Fields("Niruref").Value
            End If
            rsComSql = Nothing

            rstCheckAssort = New ADODB.Recordset
            mStrSQL = "SELECT ParcelType FROM tblImport WHERE (SupParcelNo = '" & rsExpVary.Fields("Reference2").Value & "')"
            rstCheckAssort.Open(mStrSQL, AdoCN, 1, 1)
            If Not rstCheckAssort.EOF Then
                rstExpInfo = New ADODB.Recordset
                mStrSQL = "SELECT SupplierRefNo, CompanyRefNo,InvoiceDate, AssortmentNo, ACTPcs,ACtCts, RemPcs, RemCts,BOINo, SupParcelNo, DclParcelNo, ItemCost, LotNo, ImpPrice " & _
                            "FROM tblImport WHERE (SupParcelNo = '" & rsExpVary.Fields("Reference2").Value & "') " & _
                            "ORDER BY InvoiceDate"
                rstImpInfo.Open(mStrSQL, AdoCN, 1, 1)
                If Not rstImpInfo.EOF Then
                    vMasterPcs = vMasterPcs + Val(rstImpInfo.Fields("ACTPcs").Value)
                    vMasterCts = vMasterCts + Val(rstImpInfo.Fields("ACTCts").Value)
                    sMFG = rstImpInfo.Fields("SupplierRefNo").Value
                    sBOINo = rstImpInfo.Fields("BOINo").Value
                    sImpDate = rstImpInfo.Fields("InvoiceDate").Value
                    vImpInvNo = rstImpInfo.Fields("CompanyRefNo").Value
                    vItmCost = rstImpInfo.Fields("ItemCost").Value
                    vSupParNo = rstImpInfo.Fields("SupParcelNo").Value
                    strItem = rstImpInfo.Fields("Article").Value & ""
                    strLotID = rstImpInfo.Fields("LotNo").Value
                    dblImpPrice = rstImpInfo.Fields("ImpPrice").Value
                End If
            Else
                rstCheckAssort = New ADODB.Recordset
                mStrSQL = "SELECT ParcelType FROM tblImport WHERE (SupParcelNo = '" & rsExpVary.Fields("OrigAssort").Value & "')"
                rstCheckAssort.Open(mStrSQL, AdoCN, 1, 1)

                If Not rstCheckAssort.EOF Then
                    rstExpInfo = New ADODB.Recordset
                    mStrSQL = "SELECT SupplierRefNo, CompanyRefNo,InvoiceDate, AssortmentNo, ACTPcs,ACtCts, RemPcs, RemCts,BOINo, SupParcelNo, DclParcelNo, ItemCost, LotNo, ImpPrice " & _
                                "FROM tblImport WHERE (SupParcelNo = '" & rsExpVary.Fields("OrigAssort").Value & "') " & _
                                "ORDER BY InvoiceDate"
                    rstImpInfo.Open(mStrSQL, AdoCN, 1, 1)

                    If Not rstImpInfo.EOF Then
                        vMasterPcs = vMasterPcs + Val(rstImpInfo.Fields("ACTPcs").Value)
                        vMasterCts = vMasterCts + Val(rstImpInfo.Fields("ACTCts").Value)
                        sMFG = rstImpInfo.Fields("SupplierRefNo").Value
                        sBOINo = rstImpInfo.Fields("BOINo").Value
                        sImpDate = rstImpInfo.Fields("InvoiceDate").Value
                        vImpInvNo = rstImpInfo.Fields("CompanyRefNo").Value
                        vItmCost = rstImpInfo.Fields("ItemCost").Value
                        vSupParNo = rstImpInfo.Fields("SupParcelNo").Value
                        strItem = rstImpInfo.Fields("Article").Value & ""
                        strLotID = rstImpInfo.Fields("LotNo").Value
                        dblImpPrice = rstImpInfo.Fields("ImpPrice").Value
                    End If
                Else
                    rstImpInfo = New ADODB.Recordset
                    mStrSQL = "SELECT SupplierRefNo, CompanyRefNo,InvoiceDate, AssortmentNo, ACTPcs,ACtCts, RemPcs, RemCts,BOINo, SupParcelNo, DclParcelNo, ItemCost, LotNo, ImpPrice " & _
                            "FROM tblImport WHERE (DCLParcelNo = '" & rsExpVary.Fields("Reference2").Value & "') " & _
                            "ORDER BY InvoiceDate"
                    rstImpInfo.Open(mStrSQL, AdoCN, 1, 1)
                    If Not rstImpInfo.EOF Then
                        vMasterPcs = vMasterPcs + Val(rstImpInfo.Fields("ACTPcs").Value)
                        vMasterCts = vMasterCts + Val(rstImpInfo.Fields("ACTCts").Value)
                        sMFG = rstImpInfo.Fields("SupplierRefNo").Value
                        sBOINo = rstImpInfo.Fields("BOINo").Value
                        sImpDate = rstImpInfo.Fields("InvoiceDate").Value
                        vImpInvNo = rstImpInfo.Fields("CompanyRefNo").Value
                        vItmCost = rstImpInfo.Fields("ItemCost").Value
                        vSupParNo = rstImpInfo.Fields("SupParcelNo").Value
                        strItem = rstImpInfo.Fields("Article").Value & ""
                        strLotID = rstImpInfo.Fields("LotNo").Value
                        dblImpPrice = rstImpInfo.Fields("ImpPrice").Value
                    End If
                End If
            End If

            If sMFG = "" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SupParcelNo FROM tblDep_Trf WHERE (Department = 'Rounds' OR Department = 'Colombo Niru') AND DCLParcelNo = '" & rsExpVary.Fields("Reference2").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    rstImpInfo = New ADODB.Recordset
                    mStrSQL = "SELECT SupplierRefNo, CompanyRefNo,InvoiceDate, AssortmentNo, ACTPcs,ACtCts, RemPcs, RemCts,BOINo, SupParcelNo, DclParcelNo, ItemCost, LotNo, ImpPrice " & _
                            "FROM tblImport WHERE (SupParcelNo = '" & rsComSql.Fields("SupParcelNo").Value & "') " & _
                            "ORDER BY InvoiceDate"
                    rstImpInfo.Open(mStrSQL, AdoCN, 1, 1)
                    If Not rstImpInfo.EOF Then
                        vMasterPcs = vMasterPcs + Val(rstImpInfo.Fields("ACTPcs").Value)
                        vMasterCts = vMasterCts + Val(rstImpInfo.Fields("ACTCts").Value)
                        sMFG = rstImpInfo.Fields("SupplierRefNo").Value
                        sBOINo = rstImpInfo.Fields("BOINo").Value
                        sImpDate = rstImpInfo.Fields("InvoiceDate").Value
                        vImpInvNo = rstImpInfo.Fields("CompanyRefNo").Value
                        vItmCost = rstImpInfo.Fields("ItemCost").Value
                        vSupParNo = rstImpInfo.Fields("SupParcelNo").Value
                        strLotID = rstImpInfo.Fields("LotNo").Value
                        dblImpPrice = rstImpInfo.Fields("ImpPrice").Value
                    End If
                End If
                rsComSql = Nothing
            End If

            strOrigAssort = rsExpVary.Fields("OrigAssort").Value

            dblExtLabour = 0
            vLabour = PFGetLabourCharges(rsExpVary.Fields("Request").Value, rsExpVary.Fields("ExpPcs").Value, rsExpVary.Fields("RoughCts").Value)
            vNFE = Math.Round(rsExpVary.Fields("Price").Value * Math.Round(rsExpVary.Fields("RoughCts").Value, 3), 2)

            vSubTotal = vLabour + vNFE + dblExtLabour
            vSubTotal = Math.Round(vSubTotal, 2)
            If rsExpVary.Fields("ExpCts").Value <> 0 Then
                vCost = Format(Val(vSubTotal / rsExpVary.Fields("ExpCts").Value), "#0.#0")
            Else
                vCost = 0
            End If

            vMasterPcs = rsExpVary("RoughPcs").Value
            vMasterCts = rsExpVary("RoughCts").Value

            flxDetails.Rows.Add(cmbDepartment.Text, rsExpVary.Fields("Assortment").Value, sMFG, sImpDate, vImpInvNo, rsExpVary.Fields("Reference1").Value,
                                rsExpVary.Fields("Reference2").Value, rsExpVary("RoughPcs").Value, Format(rsExpVary("RoughCts").Value, "#0.000"),
                                rsExpVary.Fields("Price").Value, Format(dtpInvDate.Value, "MM/dd/yyyy"), txtExportNo.Text, rsExpVary.Fields("ExpPcs").Value, Format(rsExpVary.Fields("ExpCts").Value, "#0.000"),
                                Math.Round(rsExpVary.Fields("StCt").Value, 2), rsExpVary.Fields("Charges").Value, rsExpVary.Fields("Yield").Value, vLabour, vNFE, vCost, vSubTotal,
                                "0", "0", vMasterPcs, Format(vMasterCts, "#0.000"), vSendPcs, Format(vSendCts, "#0.#00"), sClientID, "", "", sOrderSubject, strOrigAssort, sBOINo, "0", dblBaseCost,
                                dblAdjCost, strSupParcelNo, rsExpVary.Fields("NLineNo").Value, strNiruOrder, dblOrderItem, strCommande, "", "0", strItem, "0", strLotID)

            flxDetails.Item(56, flxDetails.Rows.Count - 1).Value = dblExtLabour
            flxDetails.Item(57, flxDetails.Rows.Count - 1).Value = "0"
            flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = Math.Round((rsExpVary.Fields("Price").Value / vItmCost) * dblImpPrice, 2)

            rsExpVary.MoveNext()
        Loop
        rsExpVary = Nothing
    End Sub

    Private Sub Load_ColomboNiruRecords()
        Dim rsExpVary As New ADODB.Recordset
        Dim rstImpInfo As New ADODB.Recordset
        Dim rstExpInfo As New ADODB.Recordset
        Dim rstCheckAssort As New ADODB.Recordset
        Dim vLabour, vNFE, vCost, vSubTotal As Double
        Dim sMFG, sImpDate, sBOINo As String
        Dim vImpInvNo As Integer
        Dim vMasterPcs As Integer
        Dim vMasterCts As Double
        Dim vSendPcs As Integer
        Dim vSendCts As Double
        Dim vItmCost As Double
        Dim vSupParNo As String
        Dim strLotID As String
        Dim dblImpPrice As Double
        Dim strConRefNo As String

        mStrSQL = ""
        rsExpVary = New ADODB.Recordset
        If chkReturn.Checked = False Then
            mStrSQL = "SELECT Assortment, SUM(Price) AS Price, Reference1, MIN(Reference2) AS Reference2, SUM(ExpPcs) AS ExpPcs, SUM(ExpCts) AS ExpCts, " & _
                            "SUM(StCt) AS StCt, SUM(Charges) AS Charges, MIN(Request) AS Request, SUM(RoughPcs) AS RoughPcs, SUM(RoughCts) AS RoughCts, " & _
                            "SUM(ExpCts) / SUM(RoughCts) AS Yield, Status, OrderRef " & _
                      "FROM tblExportVarification WHERE Department = '" & cmbDepartment.Text & "' AND Reference1 = '" & cmbReference.Text & "' AND RIGHT(Reference2, 2) <> 'DN' AND RIGHT(Reference2, 2) <> 'SN' " & _
                      "GROUP BY Assortment, Reference1, Charges, Status, OrderRef HAVING (Status = 'A') "
        Else
            mStrSQL = "SELECT Assortment, SUM(Price) AS Price, Reference1, MIN(Reference2) AS Reference2, SUM(ExpPcs) AS ExpPcs, SUM(ExpCts) AS ExpCts, " & _
                            "SUM(StCt) AS StCt, SUM(Charges) AS Charges, MIN(Request) AS Request, SUM(RoughPcs) AS RoughPcs, SUM(RoughCts) AS RoughCts, " & _
                            "SUM(ExpCts) / SUM(RoughCts) AS Yield, Status, OrderRef " & _
                      "FROM tblExportVarification WHERE Department = '" & cmbDepartment.Text & "' AND Reference1 = '" & cmbReference.Text & "' AND (RIGHT(Reference2, 2) = 'DN' OR RIGHT(Reference2, 2) = 'SN') " & _
                      "GROUP BY Assortment, Reference1, Charges, Status, OrderRef HAVING (Status = 'A') "
        End If

        rsExpVary.Open(mStrSQL, AdoCN, 1, 1)

        If rsExpVary.RecordCount > 1 Then
            If chkReturn.Checked = False Then
                mStrSQL = "SELECT Assortment, Price,Reference1, Reference2, ExpPcs, ExpCts, StCt,Charges, Request, " & _
                                "RoughPcs, RoughCts, Yield, Status,OrderRef, (ExpPcs * Charges) AS Labour " & _
                          "FROM dbo.tblExportVarification " & _
                          "WHERE (Status = 'A') AND Department = '" & cmbDepartment.Text & "' AND Reference1 = '" & cmbReference.Text & "' AND RIGHT(Reference2, 2) <> 'DN' AND RIGHT(Reference2, 2) <> 'SN' ORDER BY Assortment Asc"
            Else
                mStrSQL = "SELECT Assortment, Price,Reference1, Reference2, ExpPcs, ExpCts, StCt,Charges, Request, " & _
                                "RoughPcs, RoughCts, Yield, Status,OrderRef, (ExpPcs * Charges) AS Labour " & _
                          "FROM dbo.tblExportVarification " & _
                          "WHERE (Status = 'A') AND Department = '" & cmbDepartment.Text & "' AND Reference1 = '" & cmbReference.Text & "' AND (RIGHT(Reference2, 2) = 'DN' OR RIGHT(Reference2, 2) = 'SN') ORDER BY Assortment Asc"
            End If
            
        Else
            If chkReturn.Checked = False Then
                mStrSQL = "SELECT Assortment, AVG(Price) AS Price, Reference1, Reference2 AS Reference2,SUM(ExpPcs) AS ExpPcs, SUM(ExpCts) AS ExpCts, AVG(StCt) AS StCt, " & _
                                "AVG(Charges)AS Charges,MAX(Request) AS Request, SUM(RoughPcs) AS RoughPcs, SUM(RoughCts) AS RoughCts,MAX(Yield) AS Yield, Status, OrderRef, " & _
                                "SUM(ExpPcs * Charges) As Labour " & _
                          "FROM dbo.tblExportVarification WHERE (Department = '" & cmbDepartment.Text & "') AND Reference1 = '" & cmbReference.Text & "' AND RIGHT(Reference2, 2) <> 'DN' AND RIGHT(Reference2, 2) <> 'SN' " & _
                          "GROUP BY Assortment, Reference1,Reference2, Status, OrderRef HAVING (Status = 'A') ORDER BY Assortment"
            Else
                mStrSQL = "SELECT Assortment, AVG(Price) AS Price, Reference1, Reference2 AS Reference2,SUM(ExpPcs) AS ExpPcs, SUM(ExpCts) AS ExpCts, AVG(StCt) AS StCt, " & _
                                "AVG(Charges)AS Charges,MAX(Request) AS Request, SUM(RoughPcs) AS RoughPcs, SUM(RoughCts) AS RoughCts,MAX(Yield) AS Yield, Status, OrderRef, " & _
                                "SUM(ExpPcs * Charges) As Labour " & _
                          "FROM dbo.tblExportVarification WHERE (Department = '" & cmbDepartment.Text & "') AND Reference1 = '" & cmbReference.Text & "' AND (RIGHT(Reference2, 2) = 'DN' OR RIGHT(Reference2, 2) = 'SN') " & _
                          "GROUP BY Assortment, Reference1,Reference2, Status, OrderRef HAVING (Status = 'A') ORDER BY Assortment"
            End If
            
        End If
        rsExpVary = Nothing

        rsExpVary = New ADODB.Recordset
        rsExpVary.Open(mStrSQL, AdoCN, 1, 1)
        Do While Not rsExpVary.EOF
            sMFG = ""
            sImpDate = ""
            sBOINo = ""
            strLotID = ""
            dblImpPrice = 0
            strConRefNo = ""

            rstCheckAssort = New ADODB.Recordset
            mStrSQL = "SELECT ParcelType FROM tblImport WHERE (AssortmentNo = '" & rsExpVary.Fields("Assortment").Value & "')"
            rstCheckAssort.Open(mStrSQL, AdoCN, 1, 1)
            If Not rstCheckAssort.EOF Then
                rstExpInfo = New ADODB.Recordset
                mStrSQL = "SELECT SUM(RoughPcs) AS SendPcs, SUM(RoughCts) AS SendCts From tblCosting WHERE Reference1 = '" & rsExpVary.Fields("Reference1").Value & "'"
                rstExpInfo.Open(mStrSQL, AdoCN, 1, 1)
                If Not rstExpInfo.EOF Then
                    vSendPcs = rsExpVary.Fields("RoughPcs").Value + IIf(IsDBNull((rstExpInfo.Fields("SendPcs").Value)), "0", (rstExpInfo.Fields("SendPcs").Value))
                    vSendCts = rsExpVary.Fields("RoughCts").Value + IIf(IsDBNull((rstExpInfo.Fields("SendCts").Value)), "0", (rstExpInfo.Fields("SendCts").Value))
                End If
                rstExpInfo = Nothing
            End If
            rstCheckAssort = Nothing

            rstCheckAssort = New ADODB.Recordset
            mStrSQL = "SELECT ParcelType FROM tblImport WHERE (SupParcelNo = '" & rsExpVary.Fields("Reference1").Value & "')"
            rstCheckAssort.Open(mStrSQL, AdoCN, 1, 1)
            If rstCheckAssort.RecordCount Then
                rstImpInfo = New ADODB.Recordset
                mStrSQL = "SELECT SupplierRefNo, CompanyRefNo,InvoiceDate, AssortmentNo, ACTPcs,ACtCts, RemPcs, RemCts,BOINo, SupParcelNo, DclParcelNo, ItemCost, LotNo, ImpPrice, ConRefNo " & _
                          "FROM tblImport WHERE (SupParcelNo = '" & rsExpVary.Fields("Reference1").Value & "') ORDER BY InvoiceDate"
                rstImpInfo.Open(mStrSQL, AdoCN, 1, 1)
                If rstImpInfo.RecordCount Then
                    sMFG = rstImpInfo.Fields("SupplierRefNo").Value
                    sBOINo = rstImpInfo.Fields("BOINo").Value
                    sImpDate = rstImpInfo.Fields("InvoiceDate").Value
                    vImpInvNo = rstImpInfo.Fields("CompanyRefNo").Value
                    vItmCost = rstImpInfo.Fields("ItemCost").Value
                    vSupParNo = rstImpInfo.Fields("SupParcelNo").Value
                    strLotID = rstImpInfo.Fields("LotNo").Value
                    dblImpPrice = rstImpInfo.Fields("ImpPrice").Value
                    strConRefNo = rstImpInfo.Fields("ConRefNo").Value
                Else
                    rsComSql = New ADODB.Recordset
                    mStrSQL = "SELECT SupplierRefNo, CompanyRefNo, InvoiceDate, AssortmentNo, ACTPcs,ACtCts, RemPcs, RemCts,BOINo, SupParcelNo, DclParcelNo, ItemCost " & _
                              "FROM tblDep_Trf WHERE (DCLParcelNo = '" & rsExpVary("Reference1").Value & "') " & _
                              "ORDER BY InvoiceDate"
                    rsComSql.Open(mStrSQL, AdoCN, 1, 1)
                    If Not rsComSql.EOF Then
                        sMFG = rsComSql("SupplierRefNo").Value
                        sBOINo = rsComSql("BOINo").Value
                        sImpDate = rsComSql("InvoiceDate").Value
                        vImpInvNo = rsComSql("CompanyRefNo").Value
                        vItmCost = rsComSql("ItemCost").Value
                        vSupParNo = rsComSql("SupParcelNo").Value
                    End If
                    rsComSql = Nothing
                End If
                rstImpInfo = Nothing
            End If
            rstCheckAssort = Nothing

            vMasterPcs = rsExpVary.Fields("ExpPcs").Value
            vMasterCts = rsExpVary.Fields("RoughCts").Value
            rstImpInfo = New ADODB.Recordset
            mStrSQL = "SELECT INVPcs, INVCts FROM VW_DCLImportsSalesCon WHERE (SupplierRefNo = '" & strConRefNo & "') AND (AssortmentNo = '" & rsExpVary.Fields("Assortment").Value & "')"
            rstImpInfo.Open(mStrSQL, AdoCN, 1, 1)
            If rstImpInfo.RecordCount Then
                vMasterPcs = rstImpInfo.Fields("INVPcs").Value
                vMasterCts = rstImpInfo.Fields("INVCts").Value
            End If
            rstImpInfo = Nothing

            dblExtLabour = 0
            vLabour = PFGetLabourCharges(rsExpVary.Fields("Request").Value, vMasterPcs, vMasterCts)
            vNFE = Math.Round(rsExpVary.Fields("Price").Value * Math.Round(rsExpVary.Fields("RoughCts").Value, 3), 2)

            vSubTotal = vLabour + vNFE + dblExtLabour
            vSubTotal = Math.Round(vSubTotal, 2)
            If rsExpVary.Fields("RoughCts").Value <> 0 Then
                vCost = Format(Val(vSubTotal / rsExpVary.Fields("RoughCts").Value), "#0.#0")
            Else
                vCost = 0
            End If

            flxDetails.Rows.Add(cmbDepartment.Text, rsExpVary.Fields("Assortment").Value, sMFG, sImpDate, vImpInvNo, rsExpVary.Fields("Reference1").Value,
                                rsExpVary.Fields("Reference2").Value, rsExpVary("RoughPcs").Value, Format(rsExpVary("RoughCts").Value, "#0.000"),
                                rsExpVary.Fields("Price").Value, Format(dtpInvDate.Value, "MM/dd/yyyy"), txtExportNo.Text, rsExpVary.Fields("ExpPcs").Value, Format(rsExpVary.Fields("ExpCts").Value, "#0.000"),
                                Math.Round(rsExpVary.Fields("StCt").Value, 2), rsExpVary.Fields("Charges").Value, rsExpVary.Fields("Yield").Value, vLabour, vNFE, vCost, vSubTotal,
                                "0", "0", vMasterPcs, Format(vMasterCts, "#0.000"), vSendPcs, Format(vSendCts, "#0.#00"), "", "", "", "", cmbDepartment.Text, sBOINo, "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", strLotID)

            flxDetails.Item(56, flxDetails.Rows.Count - 1).Value = dblExtLabour
            flxDetails.Item(57, flxDetails.Rows.Count - 1).Value = "0"
            If vItmCost <> 0 Then
                flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = Math.Round((rsExpVary.Fields("Price").Value / vItmCost) * dblImpPrice, 2)
            Else
                flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = "0"
            End If

            rsExpVary.MoveNext()
        Loop
        rsExpVary = Nothing
    End Sub

    Private Sub Load_ContractRecords()
        Dim rsExpVary As New ADODB.Recordset
        Dim rstImpInfo As New ADODB.Recordset
        Dim rstExpInfo As New ADODB.Recordset
        Dim rstCheckAssort As New ADODB.Recordset
        Dim vLabour, vNFE, vCost, vSubTotal As Double
        Dim sMFG, sImpDate, sBOINo As String
        Dim vImpInvNo As Integer
        Dim vMasterPcs As Integer
        Dim vMasterCts As Double
        Dim vSendPcs As Integer
        Dim vSendCts As Double
        Dim vItmCost As Double
        Dim strLotID As String
        Dim dblImpPrice As Double

        mStrSQL = ""
        rsExpVary = New ADODB.Recordset
        mStrSQL = "SELECT TOP (100) PERCENT Assortment, Price, Reference1, Reference2, SUM(ExpPcs) AS ExpPcs, ROUND(SUM(ExpCts), 3) AS ExpCts,ROUND(SUM(ExpPcs)/SUM(ExpCts), 2) AS StCt, " & _
                    "AVG(Charges) AS Charges, Request, SUM(RoughPcs) AS RoughPcs, ROUND(SUM(RoughCts), 3) AS RoughCts, Yield, Status, OrderRef " & _
                  "FROM dbo.tblExportVarification " & _
                  "WHERE (Department = '" & cmbDepartment.Text & "') " & _
                  "GROUP BY Assortment, Reference1, Reference2, Request, Yield, Status, OrderRef, Price " & _
                  "HAVING (Status = 'A') AND (Reference1 = '" & cmbReference.Text & "') " & _
                  "ORDER BY Assortment"

        rsExpVary.Open(mStrSQL, AdoCN, 1, 1)
        Do While Not rsExpVary.EOF
            sMFG = ""
            sImpDate = ""
            sBOINo = ""
            strLotID = ""
            dblImpPrice = 0

            rstCheckAssort = New ADODB.Recordset
            mStrSQL = "SELECT ParcelType FROM tblImport WHERE (AssortmentNo = '" & rsExpVary.Fields("Assortment").Value & "')"
            rstCheckAssort.Open(mStrSQL, AdoCN, 1, 1)
            If Not rstCheckAssort.EOF Then
                rstExpInfo = New ADODB.Recordset
                mStrSQL = "SELECT SUM(RoughPcs) AS SendPcs, SUM(RoughCts) AS SendCts From tblCosting WHERE Reference1 = '" & rsExpVary.Fields("Reference1").Value & "'"
                rstExpInfo.Open(mStrSQL, AdoCN, 1, 1)
                If Not rstExpInfo.EOF Then
                    vSendPcs = rsExpVary.Fields("RoughPcs").Value + IIf(IsDBNull((rstExpInfo.Fields("SendPcs").Value)), "0", (rstExpInfo.Fields("SendPcs").Value))
                    vSendCts = rsExpVary.Fields("RoughCts").Value + IIf(IsDBNull((rstExpInfo.Fields("SendCts").Value)), "0", (rstExpInfo.Fields("SendCts").Value))
                End If
                rstExpInfo = Nothing
            End If
            rstCheckAssort = Nothing

            rstCheckAssort = New ADODB.Recordset
            mStrSQL = "SELECT ParcelType FROM tblImport WHERE (AssortmentNo = '" & rsExpVary.Fields("Assortment").Value & "')"
            rstCheckAssort.Open(mStrSQL, AdoCN, 1, 1)
            If rstCheckAssort.RecordCount Then
                rstImpInfo = New ADODB.Recordset
                mStrSQL = "SELECT SupplierRefNo, CompanyRefNo,InvoiceDate, AssortmentNo, ACTPcs,ACtCts, RemPcs, RemCts,BOINo, SupParcelNo, DclParcelNo, ItemCost, LotNo, ImpPrice " & _
                          "FROM tblImport WHERE (SupParcelNo = '" & rsExpVary.Fields("Reference1").Value & "') ORDER BY InvoiceDate"
                rstImpInfo.Open(mStrSQL, AdoCN, 1, 1)
                If rstImpInfo.RecordCount Then
                    vMasterPcs = vMasterPcs + Val(rstImpInfo.Fields("ACTPcs").Value)
                    vMasterCts = vMasterCts + Val(rstImpInfo.Fields("ACTCts").Value)
                    sMFG = rstImpInfo.Fields("SupplierRefNo").Value
                    sBOINo = rstImpInfo.Fields("BOINo").Value
                    sImpDate = rstImpInfo.Fields("InvoiceDate").Value
                    vImpInvNo = rstImpInfo.Fields("CompanyRefNo").Value
                    vItmCost = rstImpInfo.Fields("ItemCost").Value
                    strLotID = rstImpInfo.Fields("LotNo").Value
                    dblImpPrice = rstImpInfo.Fields("ImpPrice").Value
                End If
                rstImpInfo = Nothing
            End If
            rstCheckAssort = Nothing

            dblExtLabour = 0
            vLabour = PFGetLabourCharges(rsExpVary.Fields("Request").Value, rsExpVary.Fields("ExpPcs").Value, rsExpVary.Fields("RoughCts").Value)
            vNFE = Math.Round(rsExpVary.Fields("Price").Value * Math.Round(rsExpVary.Fields("RoughCts").Value, 3), 2)

            vSubTotal = vLabour + vNFE + dblExtLabour
            vSubTotal = Math.Round(vSubTotal, 2)
            If rsExpVary.Fields("ExpCts").Value <> 0 Then
                vCost = Format(Val(vSubTotal / rsExpVary.Fields("ExpCts").Value), "#0.#0")
            Else
                vCost = 0
            End If

            flxDetails.Rows.Add(cmbDepartment.Text, rsExpVary.Fields("Assortment").Value, sMFG, sImpDate, vImpInvNo, rsExpVary.Fields("Reference1").Value,
                                rsExpVary.Fields("Reference2").Value, rsExpVary("RoughPcs").Value, Format(rsExpVary("RoughCts").Value, "#0.000"),
                                rsExpVary.Fields("Price").Value, Format(dtpInvDate.Value, "MM/dd/yyyy"), txtExportNo.Text, rsExpVary.Fields("ExpPcs").Value, Format(rsExpVary.Fields("ExpCts").Value, "#0.000"),
                                Math.Round(rsExpVary.Fields("StCt").Value, 2), rsExpVary.Fields("Charges").Value, rsExpVary.Fields("Yield").Value, vLabour, vNFE, vCost, vSubTotal,
                                "0", "0", vMasterPcs, Format(vMasterCts, "#0.000"), vSendPcs, Format(vSendCts, "#0.#00"), "", "", "", "", cmbDepartment.Text, sBOINo, "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", strLotID)

            flxDetails.Item(56, flxDetails.Rows.Count - 1).Value = dblExtLabour
            flxDetails.Item(57, flxDetails.Rows.Count - 1).Value = "0"
            flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = Math.Round((rsExpVary.Fields("Price").Value / vItmCost) * dblImpPrice, 2)

            rsExpVary.MoveNext()
        Loop
        rsExpVary = Nothing
    End Sub

    Private Sub Load_PrecisionRecords()
        Dim rsExpVary As New ADODB.Recordset
        Dim rstImpInfo As New ADODB.Recordset
        Dim rstExpInfo As New ADODB.Recordset
        Dim rstCheckAssort As New ADODB.Recordset
        Dim rstImp As New ADODB.Recordset
        Dim rstOrder As New ADODB.Recordset
        Dim vLabour As Double
        Dim vNFE As Double
        Dim vCost As Double
        Dim vSubTotal As Double
        Dim vGrLabour As Double
        Dim sMFG, sImpDate, sBOINo As String
        Dim vImpInvNo As Integer
        Dim vExpPcs, vCurrPcs As Integer
        Dim bSatisfied As Boolean
        Dim vMasterPcs As Long
        Dim vMasterCts As Double
        Dim vSendPcs As Integer
        Dim vSendCts As Double
        Dim sClientID, sOrderSubject, sOrderRef As String
        Dim sOrderSide As String
        Dim strLotID As String
        Dim strCategory As String
        Dim strLineNo As String
        Dim strNOrderNo As String
        Dim strOrderItem As String

        Dim vAvgPrice As Double
        Dim intGrCount As Integer
        Dim intGroove As Integer
        Dim dblImpPrice As Double
        Dim vItmCost As Double

        bSatisfied = False

        rsExpVary = New ADODB.Recordset
        mStrSQL = "SELECT Assortment,Price,Reference1, Reference2, ExpPcs, ExpCts, StCt,Charges, Request, RoughPcs, RoughCts, Yield, Status, OrderRef, OrderSide " & _
                  "FROM dbo.tblExportVarification " & _
                  "WHERE (Status = 'A') AND Reference1 = '" & cmbReference.Text & "' AND Department = 'Precision' " & _
                  "ORDER BY Assortment"
        rsExpVary.Open(mStrSQL, AdoCN, 1, 1)

        Do While Not rsExpVary.EOF
            strLotID = ""
            strCategory = ""
            strLineNo = ""
            intGrCount = 0
            intGroove = 0
            sMFG = ""
            sImpDate = ""
            sBOINo = ""
            sClientID = ""
            sOrderRef = ""
            sOrderSide = ""
            sOrderSubject = ""
            strNOrderNo = "0"
            strOrderItem = "0"
            dblImpPrice = 0
            vItmCost = 0

            '^^^^^^^^^^^^^^^^^^^^^ get order details ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            If Not cmbReference.Text = "Returns" Then
                rstOrder = New ADODB.Recordset
                mStrSQL = "SELECT tblNoneOrdersDtls.OrderNo, tblNoneOrders.Niruref, tblNoneOrdersDtls.Side, tblNoneOrdersDtls.RefNo,tblNoneOrders.Subject, tblNoneOrdersDtls.NLineNo, tblNoneOrders.NorderNo, tblNoneOrders.OrderItem, tblNoneOrdersDtls.GrCount, tblNoneOrdersDtls.GrDone " & _
                          "FROM tblNoneOrdersDtls INNER JOIN tblNoneOrders ON tblNoneOrdersDtls.OrderNo = tblNoneOrders.OrderNo " & _
                          "WHERE (tblNoneOrdersDtls.OrderNo = " & rsExpVary("Reference1").Value & ") AND (tblNoneOrdersDtls.RefNo = '" & rsExpVary("OrderRef").Value & "') AND (tblNoneOrdersDtls.Side = '" & rsExpVary("OrderSide").Value & "')"
                rstOrder.Open(mStrSQL, AdoCN, 1, 1)
                If rstOrder.RecordCount Then
                    If Not rstOrder.EOF Then
                        sClientID = rstOrder("Niruref").Value
                        sOrderRef = rsExpVary("OrderRef").Value
                        sOrderSide = rstOrder("Side").Value
                        sOrderSubject = rstOrder("Subject").Value
                        strLineNo = rstOrder("NLineNo").Value
                        strNOrderNo = rstOrder("NorderNo").Value
                        strOrderItem = rstOrder("OrderItem").Value
                        intGrCount = rstOrder("GrCount").Value
                        intGroove = rstOrder("GrDone").Value
                    End If
                End If
                rstOrder = Nothing
            End If
            '^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

            '############## get exported Pcs And Cts ####################
            rstCheckAssort = New ADODB.Recordset
            mStrSQL = "SELECT ParcelType FROM tblImport WHERE (AssortmentNo ='" & rsExpVary("Assortment").Value & "')"
            rstCheckAssort.Open(mStrSQL, AdoCN, 1, 1)
            If Not rstCheckAssort.EOF Then
                If UCase(rstCheckAssort("ParcelType").Value) = ("ROUGH") Then
                    rstExpInfo = New ADODB.Recordset
                    mStrSQL = "SELECT SUM(RoughPcs) AS SendPcs, SUM(RoughCts) AS SendCts From tblCosting WHERE Reference1 = '" & rsExpVary.Fields("Reference1").Value & "'"
                    rstExpInfo.Open(mStrSQL, AdoCN, 1, 1)
                    If Not rstExpInfo.EOF Then
                        vSendPcs = rsExpVary.Fields("RoughPcs").Value + IIf(IsDBNull((rstExpInfo.Fields("SendPcs").Value)), "0", (rstExpInfo.Fields("SendPcs").Value))
                        vSendCts = rsExpVary.Fields("RoughCts").Value + IIf(IsDBNull((rstExpInfo.Fields("SendCts").Value)), "0", (rstExpInfo.Fields("SendCts").Value))
                    End If
                    rstExpInfo = Nothing
                Else
                    rstExpInfo = New ADODB.Recordset
                    mStrSQL = "SELECT Assortment, SUM(RoughPcs) AS SendPcs, SUM(RoughCts) AS SendCts From tblCosting GROUP BY Assortment HAVING Assortment = '" & rsExpVary("Assortment").Value & "'"
                    rstExpInfo.Open(mStrSQL, AdoCN, 1, 1)
                    If Not rstExpInfo.EOF Then
                        vSendPcs = rstExpInfo("SendPcs").Value
                        vSendCts = rstExpInfo("SendCts").Value
                    End If
                    rstExpInfo = Nothing
                End If
            End If
            rstCheckAssort = Nothing
            '###########################################################################

            '@@@@@@@@@@@@@@@@@@@@@@@@@ get import informations @@@@@@@@@@@@@@@@@@@@@@@@@
            rstCheckAssort = New ADODB.Recordset
            mStrSQL = "SELECT ParcelType FROM tblImport WHERE (AssortmentNo ='" & rsExpVary("Assortment").Value & "')"
            rstCheckAssort.Open(mStrSQL, AdoCN, 1, 1)
            If Not rstCheckAssort.EOF Then
                rstImpInfo = New ADODB.Recordset
                If UCase(rstCheckAssort("ParcelType").Value) = "ROUGH" Then
                    mStrSQL = "SELECT SupplierRefNo, CompanyRefNo,InvoiceDate, AssortmentNo, ACTPcs,ACtCts, RemPcs, RemCts,BOINo,LotNo,Category,ImpPrice,ItemCost " & _
                              "FROM tblImport " & _
                              "WHERE (DCLParcelNo = '" & rsExpVary("Reference2").Value & "') " & _
                              "ORDER BY InvoiceDate"
                    rstImpInfo.Open(mStrSQL, AdoCN, 1, 1)
                Else
                    mStrSQL = "SELECT SupplierRefNo, CompanyRefNo,InvoiceDate, AssortmentNo, ACTPcs,ACtCts, RemPcs, RemCts,BOINo,LotNo,Category,ImpPrice,ItemCost " & _
                              "FROM tblImport " & _
                              "WHERE (SupParcelNo = '" & rsExpVary("Reference2").Value & "') " & _
                              "ORDER BY InvoiceDate"
                    rstImpInfo.Open(mStrSQL, AdoCN, 1, 1)

                End If
                If rstImpInfo.RecordCount = 0 Then
                    rstImpInfo = New ADODB.Recordset
                    mStrSQL = "SELECT SupParcelNo " & _
                              "FROM tblDep_Trf " & _
                              "WHERE (DCLParcelNo = '" & rsExpVary.Fields("Reference2").Value & "')"
                    rstImpInfo.Open(mStrSQL, AdoCN, 1, 1)

                    If rstImpInfo.RecordCount Then
                        rstImpInfo = New ADODB.Recordset
                        mStrSQL = "SELECT SupplierRefNo, CompanyRefNo,InvoiceDate, AssortmentNo, ACTPcs,ACtCts, RemPcs, RemCts,BOINo,LotNo,Category,ImpPrice,ItemCost " & _
                                  "FROM tblImport " & _
                                  "WHERE (SupParcelNo = '" & rstImpInfo.Fields("SupParcelNo").Value & "')"
                        rstImpInfo.Open(mStrSQL, AdoCN, 1, 1)
                    End If
                End If
            End If

            If rstCheckAssort.RecordCount Then
                rstImpInfo.MoveFirst()
                Do While Not rstImpInfo.EOF
                    vMasterPcs = vMasterPcs + Val(rstImpInfo.Fields("ACTPcs").Value)
                    vMasterCts = vMasterCts + Val(rstImpInfo.Fields("ACTCts").Value)
                    rstImpInfo.MoveNext()
                Loop

                rstImpInfo.MoveFirst()
                vExpPcs = rsExpVary("ExpPcs").Value
                Do While Not rstImpInfo.EOF

                    vCurrPcs = rstImpInfo("RemPcs").Value

                    sMFG = rstImpInfo.Fields("SupplierRefNo").Value
                    sBOINo = rstImpInfo.Fields("BOINo").Value
                    sImpDate = rstImpInfo.Fields("InvoiceDate").Value
                    vImpInvNo = rstImpInfo.Fields("CompanyRefNo").Value
                    strLotID = rstImpInfo.Fields("LotNo").Value
                    strCategory = rstImpInfo.Fields("Category").Value
                    dblImpPrice = rstImpInfo.Fields("ImpPrice").Value
                    vItmCost = rstImpInfo.Fields("ItemCost").Value
                    If bSatisfied = True Then
                        GoTo Show_Grid
                    Else
                        bSatisfied = False
                    End If

                    rstImpInfo.MoveNext()
                    vCurrPcs = 0

                Loop

            End If
            rstCheckAssort = Nothing
            '********************************************************

            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblPOLStockOut WHERE DocID = '" & rsExpVary.Fields("Reference2").Value & "'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                strCategory = "Purchased"
                rstImp = Nothing
                mStrSQL = "SELECT AvgCost FROM tblDCLPermanents WHERE (ItemName = '" & rsExpVary.Fields("Assortment").Value & "')"
                rstImp.Open(mStrSQL, AdoCN, 1, 1)
                If Not rstImp.EOF Then
                    vAvgPrice = rstImp.Fields("AvgCost").Value
                End If
                rstImp = Nothing
            End If
            rsComSql_1 = Nothing

Show_Grid:
            dblExtLabour = 0
            vLabour = PFGetLabourCharges(rsExpVary.Fields("Request").Value, rsExpVary.Fields("ExpPcs").Value, rsExpVary.Fields("RoughCts").Value)
            vNFE = Math.Round(rsExpVary.Fields("Price").Value * Math.Round(rsExpVary.Fields("RoughCts").Value, 3), 2)

            vGrLabour = rsExpVary.Fields("ExpPcs").Value * intGrCount * 5 * intGroove
            vSubTotal = vLabour + vGrLabour + vNFE + dblExtLabour
            vSubTotal = Format(vSubTotal, "#0.##0")
            vCost = Format(Val(vSubTotal / rsExpVary("ExpCts").Value), "#0.#0")

            flxDetails.Rows.Add(cmbDepartment.Text, rsExpVary.Fields("Assortment").Value, sMFG, sImpDate, vImpInvNo, rsExpVary.Fields("Reference1").Value,
                                rsExpVary.Fields("Reference2").Value, rsExpVary("RoughPcs").Value, Format(rsExpVary("RoughCts").Value, "#0.000"),
                                rsExpVary.Fields("Price").Value, Format(dtpInvDate.Value, "MM/dd/yyyy"), txtExportNo.Text, rsExpVary.Fields("ExpPcs").Value, Format(rsExpVary.Fields("ExpCts").Value, "#0.000"),
                                Math.Round(rsExpVary.Fields("StCt").Value, 2), rsExpVary.Fields("Charges").Value, rsExpVary.Fields("Yield").Value, vLabour, vNFE, vCost, vSubTotal,
                                "0", "0", vMasterPcs, Format(vMasterCts, "#0.000"), vSendPcs, Format(vSendCts, "#0.#00"), sClientID, sOrderRef, sOrderSide, sOrderSubject, cmbDepartment.Text,
                                sBOINo, "0", "0", "0", "0", strLineNo, strNOrderNo, strOrderItem, "0", "0", "0", "0", vGrLabour, strLotID, "0", "0", strCategory)

            flxDetails.Item(56, flxDetails.Rows.Count - 1).Value = dblExtLabour
            flxDetails.Item(57, flxDetails.Rows.Count - 1).Value = "0"
            flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = Math.Round((rsExpVary.Fields("Price").Value / vItmCost) * dblImpPrice, 2)

            rsExpVary.MoveNext()
        Loop
        rsExpVary = Nothing
    End Sub

    Private Sub Load_SizeExportsRecords(ByVal strReference As String)
        Dim rsExpVary As New ADODB.Recordset
        Dim rstImpInfo As New ADODB.Recordset
        Dim rstExpInfo As New ADODB.Recordset
        Dim rstCheckAssort As New ADODB.Recordset
        Dim vLabour, vNFE, vCost, vSubTotal As Double
        Dim sMFG, sImpDate, sBOINo As String
        Dim vImpInvNo As Integer
        Dim vMasterPcs As Integer
        Dim vMasterCts As Double
        Dim vSendPcs As Integer
        Dim vSendCts As Double
        Dim vItmCost As Double
        Dim strLotID As String
        Dim sClientID As String
        Dim strOrigAssort As String
        Dim strAssortBox As String
        Dim strNewAssort As String
        Dim dblPerStonePrice As Double
        Dim dblAvgCost As Double
        Dim dblBasePrice As Double
        Dim dblImpPrice As Double
        Dim strImportType As String

        mStrSQL = ""
        rsExpVary = New ADODB.Recordset
        mStrSQL = "SELECT Assortment,Price,Reference1,Reference2,ExpPcs,ExpCts,StCt,Charges,Request,RoughPcs,RoughCts,Yield,Status,OrderRef,OrigAssort,InID,NLineNo,BasePrice " & _
                  "FROM dbo.tblExportVarification " & _
                  "WHERE (Status = 'A') AND Reference1 = '" & strReference & "' AND Department = 'SizeExports' " & _
                  "ORDER BY Assortment"

        rsExpVary.Open(mStrSQL, AdoCN, 1, 1)
        Do While Not rsExpVary.EOF
            sMFG = ""
            sImpDate = ""
            sBOINo = ""
            strLotID = ""
            sClientID = ""
            dblPerStonePrice = 0
            dblAvgCost = 0
            dblBasePrice = rsExpVary.Fields("BasePrice").Value
            dblImpPrice = 0
            strImportType = ""

            rstCheckAssort = New ADODB.Recordset
            mStrSQL = "SELECT ParcelType FROM tblImport WHERE (AssortmentNo = '" & rsExpVary.Fields("Assortment").Value & "')"
            rstCheckAssort.Open(mStrSQL, AdoCN, 1, 1)
            If Not rstCheckAssort.EOF Then
                rstExpInfo = New ADODB.Recordset
                mStrSQL = "SELECT SUM(RoughPcs) AS SendPcs, SUM(RoughCts) AS SendCts From tblCosting WHERE Reference1 = '" & rsExpVary.Fields("Reference1").Value & "'"
                rstExpInfo.Open(mStrSQL, AdoCN, 1, 1)
                If Not rstExpInfo.EOF Then
                    If Not IsDBNull(rstExpInfo.Fields("SendPcs").Value) Then
                        vSendPcs = rstExpInfo.Fields("SendPcs").Value
                        vSendCts = rstExpInfo.Fields("SendCts").Value
                    End If
                End If
                rstExpInfo = Nothing
            End If
            rstCheckAssort = Nothing

            rstCheckAssort = New ADODB.Recordset
            mStrSQL = "SELECT ParcelType FROM tblImport WHERE (SupParcelNo = '" & rsExpVary.Fields("Reference1").Value & "')"
            rstCheckAssort.Open(mStrSQL, AdoCN, 1, 1)
            If rstCheckAssort.RecordCount Then
                rstImpInfo = New ADODB.Recordset
                mStrSQL = "SELECT SupplierRefNo, CompanyRefNo,InvoiceDate, AssortmentNo, ACTPcs,ACtCts, RemPcs, RemCts,BOINo, SupParcelNo, DclParcelNo, ItemCost, LotNo, Article, ImpPrice, Category " & _
                          "FROM tblImport WHERE (SupParcelNo = '" & rsExpVary.Fields("Reference1").Value & "') ORDER BY InvoiceDate"
                rstImpInfo.Open(mStrSQL, AdoCN, 1, 1)
                If rstImpInfo.RecordCount Then
                    vMasterPcs = vMasterPcs + Val(rstImpInfo.Fields("ACTPcs").Value)
                    vMasterCts = vMasterCts + Val(rstImpInfo.Fields("ACTCts").Value)
                    sMFG = rstImpInfo.Fields("SupplierRefNo").Value
                    sBOINo = rstImpInfo.Fields("BOINo").Value
                    sImpDate = rstImpInfo.Fields("InvoiceDate").Value
                    vImpInvNo = rstImpInfo.Fields("CompanyRefNo").Value
                    vItmCost = rstImpInfo.Fields("ItemCost").Value
                    strLotID = rstImpInfo.Fields("LotNo").Value
                    sClientID = rstImpInfo.Fields("Article").Value
                    dblImpPrice = rstImpInfo.Fields("ImpPrice").Value
                    strImportType = rstImpInfo.Fields("Category").Value
                End If
                rstImpInfo = Nothing
            End If
            rstCheckAssort = Nothing

            strOrigAssort = rsExpVary.Fields("OrigAssort").Value

            dblExtLabour = 0
            vLabour = PFGetLabourCharges(rsExpVary.Fields("Request").Value, rsExpVary.Fields("ExpPcs").Value, rsExpVary.Fields("RoughCts").Value)

            If strImportType = "Purchased" Then
                If Mid(rsExpVary("Assortment").Value, 1, 1) = "S" Or Mid(rsExpVary("Assortment").Value, 1, 1) = "R" Or Mid(rsExpVary("Assortment").Value, 7, 1) = "R" Or Mid(rsExpVary("Assortment").Value, 7, 1) = "S" Then
                    dblAvgCost = rsExpVary.Fields("Price").Value
                    vNFE = Math.Round(dblAvgCost * Math.Round(rsExpVary.Fields("RoughCts").Value, 3), 2)
                    dblBasePrice = rsExpVary.Fields("BasePrice").Value
                Else
                    dblAvgCost = rsExpVary.Fields("Price").Value
                    vNFE = Math.Round(dblAvgCost * Math.Round(rsExpVary.Fields("RoughCts").Value, 3), 2)
                    dblBasePrice = rsExpVary.Fields("BasePrice").Value
                End If
            Else
                dblAvgCost = rsExpVary.Fields("Price").Value
                vNFE = Math.Round(dblAvgCost * Math.Round(rsExpVary.Fields("RoughCts").Value, 3), 2)
                dblBasePrice = rsExpVary.Fields("BasePrice").Value

            End If


            'If strImportType = "Purchased" Then
            '    If strRight(rsExpVary.Fields("Reference2").Value, 1) = "S" Then
            '        dblAvgCost = rsExpVary.Fields("Price").Value
            '        vNFE = Math.Round(dblAvgCost * Math.Round(rsExpVary.Fields("RoughCts").Value, 3), 2)
            '        dblBasePrice = rsExpVary.Fields("BasePrice").Value

            '    ElseIf cmbDepartment.Text = "Opening" Then
            '        If Mid(rsExpVary("Assortment").Value, 1, 3) = "SRW" Then
            '            dblAvgCost = rsExpVary.Fields("Price").Value
            '            vNFE = Math.Round(dblAvgCost * Math.Round(rsExpVary.Fields("RoughCts").Value, 3), 2)
            '            dblBasePrice = rsExpVary.Fields("BasePrice").Value

            '        Else
            '            dblAvgCost = rsExpVary.Fields("Price").Value
            '            vNFE = Math.Round(dblAvgCost * Math.Round(rsExpVary.Fields("RoughCts").Value, 3), 2)
            '            dblBasePrice = rsExpVary.Fields("BasePrice").Value

            '        End If

            '    ElseIf strRight(rsExpVary.Fields("Reference2").Value, 1) = "C" Or strRight(rsExpVary.Fields("Reference2").Value, 1) = "L" Or strRight(rsExpVary.Fields("Reference2").Value, 1) = "D" Then
            '        If Mid(rsExpVary("Assortment").Value, 1, 3) = "SRW" Then
            '            dblAvgCost = rsExpVary.Fields("Price").Value
            '            vNFE = Math.Round(dblAvgCost * Math.Round(rsExpVary.Fields("RoughCts").Value, 3), 2)
            '            dblBasePrice = rsExpVary.Fields("BasePrice").Value

            '        Else
            '            dblAvgCost = rsExpVary.Fields("Price").Value
            '            vNFE = Math.Round(dblAvgCost * Math.Round(rsExpVary.Fields("RoughCts").Value, 3), 2)
            '            dblBasePrice = rsExpVary.Fields("BasePrice").Value

            '        End If
            '    Else
            '        If Mid(rsExpVary("Assortment").Value, 1, 1) = "S" Then
            '            dblPerStonePrice = 0
            '            rsComSql_1 = New ADODB.Recordset
            '            rsComSql_1.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & rsExpVary("Assortment").Value & "'", AdoCN, 1, 1)
            '            If rsComSql_1.RecordCount Then
            '                If rsComSql_1.Fields("StonePrice").Value <> 0 Then
            '                    dblPerStonePrice = rsComSql_1.Fields("StonePrice").Value
            '                End If
            '            End If
            '            rsComSql_1 = Nothing

            '            If dblPerStonePrice <> 0 Then
            '                vNFE = Math.Round(dblPerStonePrice * rsExpVary.Fields("ExpPcs").Value, 2)
            '                dblAvgCost = Math.Round(vNFE / rsExpVary.Fields("RoughCts").Value, 2)
            '                dblBasePrice = dblAvgCost
            '            Else
            '                dblAvgCost = rsExpVary.Fields("BasePrice").Value
            '                vNFE = Math.Round(dblAvgCost * rsExpVary.Fields("RoughCts").Value, 2)
            '                dblBasePrice = rsExpVary.Fields("BasePrice").Value
            '            End If
            '        Else
            '            dblAvgCost = rsExpVary.Fields("Price").Value
            '            vNFE = Math.Round(dblAvgCost * Math.Round(rsExpVary.Fields("RoughCts").Value, 3), 2)
            '            dblBasePrice = rsExpVary.Fields("BasePrice").Value
            '        End If
            '    End If  
            'Else
            '    dblAvgCost = rsExpVary.Fields("Price").Value
            '    vNFE = Math.Round(dblAvgCost * Math.Round(rsExpVary.Fields("RoughCts").Value, 3), 2)
            '    dblBasePrice = rsExpVary.Fields("BasePrice").Value
            'End If

            vSubTotal = vLabour + vNFE + dblExtLabour
            vSubTotal = Math.Round(vSubTotal, 2)
            If rsExpVary.Fields("ExpCts").Value <> 0 Then
                vCost = Format(Val(vSubTotal / rsExpVary.Fields("ExpCts").Value), "#0.#0")
            Else
                vCost = 0
            End If

            dblAvgCost = Math.Round(dblAvgCost, 2)
            dblBasePrice = Math.Round(dblBasePrice, 2)

            strAssortBox = rsExpVary.Fields("Assortment").Value
            strNewAssort = rsExpVary.Fields("Assortment").Value

            flxDetails.Rows.Add(cmbDepartment.Text, strAssortBox, sMFG, sImpDate, vImpInvNo, rsExpVary.Fields("Reference1").Value,
                                rsExpVary.Fields("Reference2").Value, rsExpVary("RoughPcs").Value, Format(rsExpVary("RoughCts").Value, "#0.000"),
                                dblAvgCost, Format(dtpInvDate.Value, "MM/dd/yyyy"), txtExportNo.Text, rsExpVary.Fields("ExpPcs").Value, Format(rsExpVary.Fields("ExpCts").Value, "#0.000"),
                                Math.Round(rsExpVary.Fields("StCt").Value, 2), rsExpVary.Fields("Charges").Value, rsExpVary.Fields("Yield").Value, vLabour, vNFE, vCost, vSubTotal,
                                "0", "0", vMasterPcs, Format(vMasterCts, "#0.000"), vSendPcs, Format(vSendCts, "#0.#00"), sClientID, "", "", "", strOrigAssort, sBOINo, rsExpVary.Fields("InID").Value, dblBasePrice, vItmCost, "0", "0", "0", "0", "0",
                                strNewAssort, "0", "0", "0", strLotID)

            flxDetails.Item(56, flxDetails.Rows.Count - 1).Value = dblExtLabour
            flxDetails.Item(57, flxDetails.Rows.Count - 1).Value = "0"
            flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = Math.Round((dblAvgCost / vItmCost) * dblImpPrice, 2)

            rsExpVary.MoveNext()
        Loop
        rsExpVary = Nothing
    End Sub

    Private Sub Load_ProcessRejectRecords(ByVal strReference As String)
        Dim rsExpVary As New ADODB.Recordset
        Dim rstImpInfo As New ADODB.Recordset
        Dim rstExpInfo As New ADODB.Recordset
        Dim rstCheckAssort As New ADODB.Recordset
        Dim vLabour, vNFE, vCost, vSubTotal As Double
        Dim sMFG, sImpDate, sBOINo As String
        Dim vImpInvNo As Integer
        Dim vMasterPcs As Integer
        Dim vMasterCts As Double
        Dim vSendPcs As Integer
        Dim vSendCts As Double
        Dim vItmCost As Double
        Dim strLotID As String
        Dim sClientID As String
        Dim strOrigAssort As String
        Dim dblImpPrice As Double

        mStrSQL = ""
        rsExpVary = New ADODB.Recordset
        mStrSQL = "SELECT Assortment,Price,Reference1,Reference2,ExpPcs,ExpCts,StCt,Charges,Request,RoughPcs,RoughCts,Yield,Status,OrderRef,OrigAssort,InID,NLineNo " & _
                  "FROM dbo.tblExportVarification " & _
                  "WHERE (Status = 'A') AND Reference1 = '" & strReference & "' AND Department = 'ProcessReject' " & _
                  "ORDER BY Assortment"

        rsExpVary.Open(mStrSQL, AdoCN, 1, 1)
        Do While Not rsExpVary.EOF
            sMFG = ""
            sImpDate = ""
            sBOINo = ""
            strLotID = ""
            sClientID = ""
            dblImpPrice = 0

            rstCheckAssort = New ADODB.Recordset
            mStrSQL = "SELECT ParcelType FROM tblImport WHERE (AssortmentNo = '" & rsExpVary.Fields("Assortment").Value & "')"
            rstCheckAssort.Open(mStrSQL, AdoCN, 1, 1)
            If Not rstCheckAssort.EOF Then
                rstExpInfo = New ADODB.Recordset
                mStrSQL = "SELECT SUM(RoughPcs) AS SendPcs, SUM(RoughCts) AS SendCts From tblCosting WHERE Reference1 = '" & rsExpVary.Fields("Reference1").Value & "'"
                rstExpInfo.Open(mStrSQL, AdoCN, 1, 1)
                If Not rstExpInfo.EOF Then
                    If Not IsDBNull(rstExpInfo.Fields("SendPcs").Value) Then
                        vSendPcs = rstExpInfo.Fields("SendPcs").Value
                        vSendCts = rstExpInfo.Fields("SendCts").Value
                    End If
                End If
                rstExpInfo = Nothing
            End If
            rstCheckAssort = Nothing

            rstCheckAssort = New ADODB.Recordset
            mStrSQL = "SELECT ParcelType FROM tblImport WHERE (SupParcelNo = '" & rsExpVary.Fields("Reference1").Value & "')"
            rstCheckAssort.Open(mStrSQL, AdoCN, 1, 1)
            If rstCheckAssort.RecordCount Then
                rstImpInfo = New ADODB.Recordset
                mStrSQL = "SELECT SupplierRefNo, CompanyRefNo,InvoiceDate, AssortmentNo, ACTPcs,ACtCts, RemPcs, RemCts,BOINo, SupParcelNo, DclParcelNo, ItemCost, LotNo, Article, ImpPrice " & _
                          "FROM tblImport WHERE (SupParcelNo = '" & rsExpVary.Fields("Reference1").Value & "') ORDER BY InvoiceDate"
                rstImpInfo.Open(mStrSQL, AdoCN, 1, 1)
                If rstImpInfo.RecordCount Then
                    vMasterPcs = vMasterPcs + Val(rstImpInfo.Fields("ACTPcs").Value)
                    vMasterCts = vMasterCts + Val(rstImpInfo.Fields("ACTCts").Value)
                    sMFG = rstImpInfo.Fields("SupplierRefNo").Value
                    sBOINo = rstImpInfo.Fields("BOINo").Value
                    sImpDate = rstImpInfo.Fields("InvoiceDate").Value
                    vImpInvNo = rstImpInfo.Fields("CompanyRefNo").Value
                    vItmCost = rstImpInfo.Fields("ItemCost").Value
                    strLotID = rstImpInfo.Fields("LotNo").Value
                    sClientID = rstImpInfo.Fields("Article").Value
                    dblImpPrice = rstImpInfo.Fields("ImpPrice").Value
                End If
                rstImpInfo = Nothing
            End If
            rstCheckAssort = Nothing

            strOrigAssort = rsExpVary.Fields("OrigAssort").Value

            dblExtLabour = 0
            vLabour = PFGetLabourCharges(rsExpVary.Fields("Request").Value, rsExpVary.Fields("ExpPcs").Value, rsExpVary.Fields("RoughCts").Value)
            vNFE = Math.Round(rsExpVary.Fields("Price").Value * Math.Round(rsExpVary.Fields("RoughCts").Value, 3), 2)

            vSubTotal = vLabour + vNFE + dblExtLabour
            vSubTotal = Math.Round(vSubTotal, 2)
            If rsExpVary.Fields("ExpCts").Value <> 0 Then
                vCost = Format(Val(vSubTotal / rsExpVary.Fields("ExpCts").Value), "#0.#0")
            Else
                vCost = 0
            End If

            flxDetails.Rows.Add(cmbDepartment.Text, rsExpVary.Fields("Assortment").Value, sMFG, sImpDate, vImpInvNo, rsExpVary.Fields("Reference1").Value,
                                rsExpVary.Fields("Reference2").Value, rsExpVary("RoughPcs").Value, Format(rsExpVary("RoughCts").Value, "#0.000"),
                                rsExpVary.Fields("Price").Value, Format(dtpInvDate.Value, "MM/dd/yyyy"), txtExportNo.Text, rsExpVary.Fields("ExpPcs").Value, Format(rsExpVary.Fields("ExpCts").Value, "#0.000"),
                                Math.Round(rsExpVary.Fields("StCt").Value, 2), rsExpVary.Fields("Charges").Value, rsExpVary.Fields("Yield").Value, vLabour, vNFE, vCost, vSubTotal,
                                "0", "0", vMasterPcs, Format(vMasterCts, "#0.000"), vSendPcs, Format(vSendCts, "#0.#00"), sClientID, "", "", "", strOrigAssort, sBOINo, "0", "0", "0", "0", "0", "0", "0", "0",
                                rsExpVary.Fields("Assortment").Value, "0", "0", "0", strLotID)

            flxDetails.Item(56, flxDetails.Rows.Count - 1).Value = dblExtLabour
            flxDetails.Item(57, flxDetails.Rows.Count - 1).Value = "0"
            flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = Math.Round((rsExpVary.Fields("Price").Value / vItmCost) * dblImpPrice, 2)

            rsExpVary.MoveNext()
        Loop
        rsExpVary = Nothing
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub ClearFields()
        cmbDepartment.Text = ""
        cmbReference.Text = ""
        cmbReference.Items.Clear()
        txtExportNo.Text = ""
        flxDetails.Rows.Clear()
        txtPack.Text = ""
        txtType.Text = ""
        txtCategory.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtTotPcs.Text = ""
        txtTotCts.Text = ""
        txtLabour.Text = ""
        txtGrLabour.Text = ""
        txtTotValue.Text = ""
        txtMargin.Text = "0"
        chkPack.Checked = False
        chkAsking97.Checked = False
        chkAsking100.Checked = False
        chkAsking105.Checked = False
        chkAsking985.Checked = False
        chkAsking80.Checked = False
        chkAsking70.Checked = False
        chkAsking75.Checked = False
        chkAsking50.Checked = False
        cmbClient.Text = ""
    End Sub

    Private Sub Load_SavedData()
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        mStrSQL = "SELECT tblCosting.*, ISNULL(dbo.tblAssortOK.OrgAssort, N'NO') AS OK " & _
                  "FROM dbo.tblCosting LEFT OUTER JOIN dbo.tblAssortOK ON dbo.tblCosting.CostingFor = dbo.tblAssortOK.OrgAssort " & _
                  "WHERE (tblCosting.ExportNo = " & txtExportNo.Text & ") ORDER BY tblCosting.ID"
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("Department").Value, rsComSql.Fields("Assortment").Value, rsComSql.Fields("SupInvoiceNo").Value, rsComSql.Fields("ImpDate").Value,
                                    rsComSql.Fields("ImportNo").Value, rsComSql.Fields("Reference1").Value, rsComSql.Fields("Reference2").Value, rsComSql.Fields("RoughPcs").Value,
                                    Math.Round(rsComSql.Fields("RoughCts").Value, 3), Format(rsComSql.Fields("Price").Value, "0.00"), rsComSql.Fields("ExportDate").Value, rsComSql.Fields("ExportNo").Value,
                                    rsComSql.Fields("ExportPcs").Value, Math.Round(rsComSql.Fields("ExportCts").Value, 3), Format(rsComSql.Fields("StCts").Value, "0.00"), rsComSql.Fields("vStone").Value,
                                    rsComSql.Fields("Yield").Value, Format(rsComSql.Fields("Labour").Value, "0.00"), Format(rsComSql.Fields("NFEValue").Value, "0.00"), Format(rsComSql.Fields("Cost").Value, "0.00"), Format(rsComSql.Fields("Totals").Value, "0.00"),
                                    rsComSql.Fields("BalancePcs").Value, Format(rsComSql.Fields("BalanceCts").Value, "0.000"), rsComSql.Fields("MasterPcs").Value, Format(rsComSql.Fields("MasterCts").Value, "0.000"),
                                    rsComSql.Fields("ShipedPcs").Value, Format(rsComSql.Fields("ShipedCts").Value, "0.000"), rsComSql.Fields("ClientID").Value, rsComSql.Fields("OrderRefrence").Value,
                                    rsComSql.Fields("OrderSide").Value, rsComSql.Fields("Subject").Value, rsComSql.Fields("CostingFor").Value, rsComSql.Fields("BOINo").Value, rsComSql.Fields("InID").Value,
                                    Format(rsComSql.Fields("BaseCost").Value, "0.00"), Format(rsComSql.Fields("CurCost").Value, "0.00"), rsComSql.Fields("SupParNo").Value, rsComSql.Fields("NLineNo").Value, rsComSql.Fields("NOrderNo").Value,
                                    rsComSql.Fields("OrderItem").Value, rsComSql.Fields("Commande").Value, rsComSql.Fields("NiruParcel").Value, rsComSql.Fields("OK").Value, rsComSql.Fields("Item").Value,
                                    Format(rsComSql.Fields("GrLabour").Value, "0.00"), rsComSql.Fields("LotID").Value, "", rsComSql.Fields("AssortValue").Value, rsComSql.Fields("Category").Value, rsComSql.Fields("SalesRate").Value,
                                    rsComSql.Fields("ID").Value, rsComSql.Fields("PackingListNo").Value, rsComSql.Fields("PackingType").Value, Format(rsComSql.Fields("RghLabour").Value, "0.00"), Format(rsComSql.Fields("AssLabour").Value, "0.00"),
                                    Format(rsComSql.Fields("Margin").Value, "0.00"), Format(rsComSql.Fields("LabourE").Value, "0.00"), Format(rsComSql.Fields("MaxValue").Value, "0.00"), Format(rsComSql.Fields("HardCost").Value, "0.00"))
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtPcs.Text = CalTotalPcs(flxDetails, 7)
        txtCts.Text = CalTotalCts(flxDetails, 8)
        txtTotPcs.Text = CalTotalPcs(flxDetails, 12)
        txtTotCts.Text = CalTotalCts(flxDetails, 13)
        txtLabour.Text = CalTotalCts(flxDetails, 17)
        txtGrLabour.Text = CalTotalCts(flxDetails, 44)
    End Sub

    Private Sub txtExportNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtExportNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtExportNo.Text <> "" Then
                Load_SavedData()
            End If
        End If
    End Sub

    Private Sub Load_RoughSalesRecords()
        Dim rsExpVary As New ADODB.Recordset
        Dim rstImpInfo As New ADODB.Recordset
        Dim rstExpInfo As New ADODB.Recordset
        Dim rstCheckAssort As New ADODB.Recordset
        Dim vLabour, vNFE, vCost, vSubTotal As Double
        Dim sMFG, sImpDate, sBOINo As String
        Dim vImpInvNo As Integer
        Dim vMasterPcs As Integer
        Dim vMasterCts As Double
        Dim vSendPcs As Integer
        Dim vSendCts As Double
        Dim vItmCost As Double
        Dim strLotID As String
        Dim dblImpPrice As Double

        mStrSQL = ""
        rsExpVary = New ADODB.Recordset
        mStrSQL = "SELECT TOP (100) PERCENT Assortment, Price, Reference1, Reference2, SUM(ExpPcs) AS ExpPcs, ROUND(SUM(ExpCts), 3) AS ExpCts,ROUND(SUM(ExpPcs)/SUM(ExpCts), 2) AS StCt, " & _
                    "AVG(Charges) AS Charges, Request, SUM(RoughPcs) AS RoughPcs, ROUND(SUM(RoughCts), 3) AS RoughCts, Yield, Status, OrderRef " & _
                  "FROM dbo.tblExportVarification " & _
                  "WHERE (Department = '" & cmbDepartment.Text & "') " & _
                  "GROUP BY Assortment, Reference1, Reference2, Request, Yield, Status, OrderRef, Price " & _
                  "HAVING (Status = 'A') AND (Reference1 = '" & cmbReference.Text & "') " & _
                  "ORDER BY Assortment"

        rsExpVary.Open(mStrSQL, AdoCN, 1, 1)
        Do While Not rsExpVary.EOF
            sMFG = ""
            sImpDate = ""
            sBOINo = ""
            strLotID = ""

            rstCheckAssort = New ADODB.Recordset
            mStrSQL = "SELECT ParcelType FROM tblImport WHERE (AssortmentNo = '" & rsExpVary.Fields("Assortment").Value & "')"
            rstCheckAssort.Open(mStrSQL, AdoCN, 1, 1)
            If Not rstCheckAssort.EOF Then
                rstExpInfo = New ADODB.Recordset
                mStrSQL = "SELECT SUM(RoughPcs) AS SendPcs, SUM(RoughCts) AS SendCts From tblCosting WHERE Reference1 = '" & rsExpVary.Fields("Reference1").Value & "'"
                rstExpInfo.Open(mStrSQL, AdoCN, 1, 1)
                If Not rstExpInfo.EOF Then
                    vSendPcs = rsExpVary.Fields("RoughPcs").Value + IIf(IsDBNull((rstExpInfo.Fields("SendPcs").Value)), "0", (rstExpInfo.Fields("SendPcs").Value))
                    vSendCts = rsExpVary.Fields("RoughCts").Value + IIf(IsDBNull((rstExpInfo.Fields("SendCts").Value)), "0", (rstExpInfo.Fields("SendCts").Value))
                End If
                rstExpInfo = Nothing
            End If
            rstCheckAssort = Nothing

            rstCheckAssort = New ADODB.Recordset
            mStrSQL = "SELECT ParcelType FROM tblImport WHERE (AssortmentNo = '" & rsExpVary.Fields("Assortment").Value & "')"
            rstCheckAssort.Open(mStrSQL, AdoCN, 1, 1)
            If rstCheckAssort.RecordCount Then
                rstImpInfo = New ADODB.Recordset
                mStrSQL = "SELECT SupplierRefNo, CompanyRefNo,InvoiceDate, AssortmentNo, ACTPcs,ACtCts, RemPcs, RemCts,BOINo, SupParcelNo, DclParcelNo, ItemCost, LotNo, ImpPrice " & _
                          "FROM tblImport WHERE (SupParcelNo = '" & rsExpVary.Fields("Reference1").Value & "') ORDER BY InvoiceDate"
                rstImpInfo.Open(mStrSQL, AdoCN, 1, 1)
                If rstImpInfo.RecordCount Then
                    vMasterPcs = vMasterPcs + Val(rstImpInfo.Fields("ACTPcs").Value)
                    vMasterCts = vMasterCts + Val(rstImpInfo.Fields("ACTCts").Value)
                    sMFG = rstImpInfo.Fields("SupplierRefNo").Value
                    sBOINo = rstImpInfo.Fields("BOINo").Value
                    sImpDate = rstImpInfo.Fields("InvoiceDate").Value
                    vImpInvNo = rstImpInfo.Fields("CompanyRefNo").Value
                    vItmCost = rstImpInfo.Fields("ItemCost").Value
                    strLotID = rstImpInfo.Fields("LotNo").Value
                    dblImpPrice = rstImpInfo.Fields("ImpPrice").Value
                End If
                rstImpInfo = Nothing
            End If
            rstCheckAssort = Nothing

            dblExtLabour = 0
            vLabour = PFGetLabourCharges(rsExpVary.Fields("Request").Value, rsExpVary.Fields("ExpPcs").Value, rsExpVary.Fields("RoughCts").Value)
            vNFE = Math.Round(rsExpVary.Fields("Price").Value * Math.Round(rsExpVary.Fields("RoughCts").Value, 3), 2)

            vSubTotal = vLabour + vNFE + dblExtLabour
            vSubTotal = Math.Round(vSubTotal, 2)
            If rsExpVary.Fields("ExpCts").Value <> 0 Then
                vCost = Format(Val(vSubTotal / rsExpVary.Fields("ExpCts").Value), "#0.#0")
            Else
                vCost = 0
            End If

            vMasterPcs = rsExpVary("RoughPcs").Value
            vMasterCts = rsExpVary("RoughCts").Value

            flxDetails.Rows.Add(cmbDepartment.Text, rsExpVary.Fields("Assortment").Value, sMFG, sImpDate, vImpInvNo, rsExpVary.Fields("Reference1").Value,
                                rsExpVary.Fields("Reference2").Value, rsExpVary("RoughPcs").Value, Format(rsExpVary("RoughCts").Value, "#0.000"),
                                rsExpVary.Fields("Price").Value, Format(dtpInvDate.Value, "MM/dd/yyyy"), txtExportNo.Text, rsExpVary.Fields("ExpPcs").Value, Format(rsExpVary.Fields("ExpCts").Value, "#0.000"),
                                Math.Round(rsExpVary.Fields("StCt").Value, 2), rsExpVary.Fields("Charges").Value, rsExpVary.Fields("Yield").Value, vLabour, vNFE, vCost, vSubTotal,
                                "0", "0", vMasterPcs, Format(vMasterCts, "#0.000"), vSendPcs, Format(vSendCts, "#0.#00"), "", "", "", "", cmbDepartment.Text, sBOINo, "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", strLotID)

            flxDetails.Item(56, flxDetails.Rows.Count - 1).Value = dblExtLabour
            flxDetails.Item(57, flxDetails.Rows.Count - 1).Value = "0"
            flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = Math.Round((rsExpVary.Fields("Price").Value / vItmCost) * dblImpPrice, 2)

            rsExpVary.MoveNext()
        Loop
        rsExpVary = Nothing
    End Sub

    Private Sub Load_PolishBoxRecords()
        Dim rsExpVary As New ADODB.Recordset
        Dim rstExpInfo As New ADODB.Recordset
        Dim vLabour, vNFE, vCost, vSubTotal As Double
        Dim sMFG, sImpDate, sBOINo As String
        Dim vImpInvNo As Integer
        Dim vExpPcs, vCurrPcs As Integer
        Dim vMasterPcs As Double
        Dim vMasterCts As Double
        Dim vSendPcs As Integer
        Dim vSendCts As Double

        Dim sClientID, sOrderSubject, sOrderRef As String
        Dim sOrderSide As String

        Dim intOutPcs As Integer
        Dim dblOutCts As Double
        Dim intBalPcs As Integer
        Dim dblBalCts As Double
        Dim strOrigAssort As String
        Dim dblBaseCost As Double
        Dim dblAdjCost As Double
        Dim dblCurCost As Double
        Dim strSupParcelNo As String

        Dim dblMixID As Double

        Dim strAssortBox As String

        Dim intIssPcs As Integer
        Dim dblIssCts As Double

        Dim strLotID As String
        Dim strCategory As String
        Dim dblSaleRate As Double
        Dim strCompCode As String

        rsExpVary = New ADODB.Recordset
        mStrSQL = "SELECT Assortment,Price,Reference1,Reference2,ExpPcs,ExpCts,StCt,Charges,Request,RoughPcs,RoughCts,Yield,Status,OrderRef,OrigAssort,InID,NLineNo,BasePrice,AdjPrice " & _
                  "FROM dbo.tblExportVarification " & _
                  "WHERE (Status = 'A') AND (Reference1 = '" & cmbReference.Text & "') AND (Department = '" & cmbDepartment.Text & "') " & _
                  "ORDER BY Reference1,Reference2"
        rsExpVary.Open(mStrSQL, AdoCN, 1, 1)

        Do While Not rsExpVary.EOF
            sClientID = ""
            sOrderSide = ""
            sOrderSubject = ""

            strCompCode = ""
            rsComSql = New ADODB.Recordset
            If cmbDepartment.Text = "PolishBox" Then
                rsComSql.Open("SELECT * FROM tblPOLSales WHERE SalesNo = '" & cmbReference.Text & "'", AdoCN, 1, 1)
            Else
                rsComSql.Open("SELECT * FROM tblPOLTransfer WHERE TransferNo = '" & cmbReference.Text & "'", AdoCN, 1, 1)
            End If
            If rsComSql.RecordCount Then
                strCompCode = rsComSql.Fields("CompCode").Value
            End If
            rsComSql = Nothing

            rstExpInfo = New ADODB.Recordset
            mStrSQL = "SELECT Assortment, SUM(RoughPcs) AS SendPcs, SUM(RoughCts) AS SendCts " & _
                      "FROM tblCosting GROUP BY Assortment HAVING Assortment = '" & rsExpVary("Assortment").Value & "'"
            rstExpInfo.Open(mStrSQL, AdoCN, 1, 1)
            If Not rstExpInfo.EOF Then
                vSendPcs = rstExpInfo("SendPcs").Value
                vSendCts = rstExpInfo("SendCts").Value
            End If
            rstExpInfo = Nothing

            intOutPcs = 0
            dblOutCts = 0
            sOrderRef = rsExpVary.Fields("OrderRef").Value
            intBalPcs = rsExpVary.Fields("ExpPcs").Value
            dblBalCts = Math.Round(rsExpVary.Fields("ExpCts").Value, 3)
            vMasterPcs = 0
            vMasterCts = 0
            dblCurCost = 0
            strSupParcelNo = ""
            dblBaseCost = rsExpVary.Fields("BasePrice").Value
            dblAdjCost = rsExpVary.Fields("AdjPrice").Value
            dblSaleRate = 0

            strOrigAssort = rsExpVary.Fields("Assortment").Value

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblPOLAssortMatch WHERE NewAssort = '" & strOrigAssort & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                strOrigAssort = rsComSql.Fields("OrigAssort").Value
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            If Mid(rsExpVary("Assortment").Value, 1, 1) = "U" Then
                rsComSql.Open("SELECT  TOP (100) PERCENT RecordID, Department, SystemRefNo, SupplierRefNo, CompanyRefNo, BOINo, InvoiceDate, RecievedDate, SupplierCode, ParcelType, " & _
                            "AssortmentNo, SupParcelNo, DCLParcelNo, INVPcs, INVCts, ACTPcs, ACtCts, ItemSize, Charges, ItemCost, RemPcs, RemCts, Status, DoneBy, ModifyBy, SysDateTime, " & _
                            "ImportNo, LotNo, Article, Remarks, Category, HardCost, CompCode, ItemName, Urgent, NewAssort, LocalInst, PlanFinDate, RghFinDate, SelectCost, " & _
                            "PolPcs AS TrfPcs, PolCts AS TrfCts FROM tblImport " & _
                          "WHERE PolPcs > 0 AND CompCode = '" & strCompCode & "' " & _
                          "ORDER BY SysDateTime", AdoCN, 1, 1)
            Else
                rsComSql.Open("SELECT  TOP (100) PERCENT RecordID, Department, SystemRefNo, SupplierRefNo, CompanyRefNo, BOINo, InvoiceDate, RecievedDate, SupplierCode, ParcelType, " & _
                            "AssortmentNo, SupParcelNo, DCLParcelNo, INVPcs, INVCts, ACTPcs, ACtCts, ItemSize, Charges, ItemCost, RemPcs, RemCts, Status, DoneBy, ModifyBy, SysDateTime, " & _
                            "ImportNo, LotNo, Article, Remarks, Category, HardCost, CompCode, ItemName, Urgent, NewAssort, LocalInst, PlanFinDate, RghFinDate, SelectCost, " & _
                            "PolPcs AS TrfPcs, PolCts AS TrfCts FROM tblImport " & _
                          "WHERE PolPcs > 0 AND CompCode = '" & strCompCode & "' " & _
                          "ORDER BY SysDateTime", AdoCN, 1, 1)
            End If
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF And intBalPcs > 0
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT SUM(Pcs) AS Pcs,ROUND(SUM(Cts), 2) AS Cts FROM tblCostingTemp2 WHERE SupParNo = '" & rsComSql.Fields("SupParcelNo").Value & "'", AdoCN, 1, 1)
                    If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                        intIssPcs = rsComSql_1.Fields("Pcs").Value
                        dblIssCts = rsComSql_1.Fields("Cts").Value
                    Else
                        intIssPcs = 0
                        dblIssCts = 0
                    End If
                    rsComSql_1 = Nothing

                    If rsComSql.Fields("TrfPcs").Value - intIssPcs > 0 Then
                        If rsComSql.Fields("TrfPcs").Value - intIssPcs >= intBalPcs Then

                            sMFG = rsComSql.Fields("SupplierRefNo").Value
                            sBOINo = rsComSql.Fields("BOINo").Value
                            sImpDate = rsComSql.Fields("InvoiceDate").Value
                            vImpInvNo = rsComSql.Fields("CompanyRefNo").Value
                            vMasterPcs = vMasterPcs + rsComSql.Fields("ActPcs").Value
                            vMasterCts = vMasterCts + rsComSql.Fields("ActCts").Value
                            dblBaseCost = rsExpVary.Fields("BasePrice").Value
                            dblCurCost = rsExpVary.Fields("Price").Value
                            dblAdjCost = rsExpVary.Fields("AdjPrice").Value
                            strSupParcelNo = rsComSql.Fields("SupParcelNo").Value
                            strLotID = rsComSql.Fields("LotNo").Value
                            strCategory = rsComSql.Fields("Category").Value

                            If dblBaseCost > dblCurCost Then
                                dblSaleRate = dblBaseCost
                            Else
                                dblSaleRate = dblCurCost
                            End If

                            dblSaleRate = dblSaleRate + (dblSaleRate * CDbl(txtMargin.Text) / 100)
                            'dblBaseCost = dblBaseCost + (dblBaseCost * CDbl(txtMargin.Text) / 100)
                            'dblCurCost = dblCurCost + (dblCurCost * CDbl(txtMargin.Text) / 100)

                            dblMixID = 0
                            strAssortBox = rsExpVary.Fields("Assortment").Value
                            AdoCN.Execute("INSERT INTO tblCostingTemp2(SupParNo,Pcs,Cts) VALUES('" & strSupParcelNo & "'," & intBalPcs & "," & Math.Round(dblBalCts, 3) & ")")

                            dblExtLabour = 0
                            vLabour = PFGetLabourCharges(rsExpVary.Fields("Request").Value, intBalPcs, Math.Round((rsExpVary.Fields("RoughCts").Value / rsExpVary.Fields("RoughPcs").Value) * intBalPcs, 3))
                            vNFE = Math.Round(dblCurCost * (rsExpVary.Fields("RoughCts").Value / rsExpVary.Fields("RoughPcs").Value) * intBalPcs, 2)

                            vSubTotal = Math.Round(vLabour + vNFE + dblExtLabour, 2)
                            vCost = Math.Round(vSubTotal / ((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * intBalPcs), 2)

                            flxDetails.Rows.Add(cmbDepartment.Text, rsExpVary("Assortment").Value, sMFG, sImpDate, vImpInvNo, rsExpVary.Fields("Reference1").Value, rsExpVary.Fields("Reference2").Value,
                                                intBalPcs, Math.Round((rsExpVary.Fields("RoughCts").Value / rsExpVary.Fields("RoughPcs").Value) * intBalPcs, 3), dblCurCost,
                                                dtpInvDate.Value, txtExportNo.Text, intBalPcs, Math.Round((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * intBalPcs, 3),
                                                rsExpVary.Fields("StCt").Value, rsExpVary.Fields("Charges").Value, rsExpVary.Fields("Yield").Value, vLabour, vNFE, vCost, vSubTotal, "0", "0",
                                                vMasterPcs, Math.Round(vMasterCts, 3), vSendPcs, Math.Round(vSendCts, 3), sClientID, sOrderRef, sOrderSide, sOrderSubject, strOrigAssort, sBOINo, dblMixID, Math.Round(dblBaseCost, 2),
                                                Math.Round(dblAdjCost, 2), strSupParcelNo, rsExpVary.Fields("NLineNo").Value, "0", "0", "", strAssortBox, "", "", intBalPcs * 0 * 5,
                                                strLotID, "0", "0", strCategory, Math.Round(dblSaleRate, 2))

                            flxDetails.Item(56, flxDetails.Rows.Count - 1).Value = dblExtLabour
                            flxDetails.Item(57, flxDetails.Rows.Count - 1).Value = "0"
                            flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = dblCurCost

                            intBalPcs = 0
                            dblBalCts = 0
                        Else
                            sMFG = rsComSql.Fields("SupplierRefNo").Value
                            sBOINo = rsComSql.Fields("BOINo").Value
                            sImpDate = rsComSql.Fields("InvoiceDate").Value
                            vImpInvNo = rsComSql.Fields("CompanyRefNo").Value
                            vMasterPcs = vMasterPcs + rsComSql.Fields("ActPcs").Value
                            vMasterCts = vMasterCts + rsComSql.Fields("ActCts").Value
                            dblBaseCost = rsExpVary.Fields("BasePrice").Value
                            dblCurCost = rsExpVary.Fields("Price").Value
                            dblAdjCost = rsExpVary.Fields("AdjPrice").Value
                            strSupParcelNo = rsComSql.Fields("SupParcelNo").Value
                            strLotID = rsComSql.Fields("LotNo").Value
                            strCategory = rsComSql.Fields("Category").Value

                            If dblBaseCost > dblCurCost Then
                                dblSaleRate = dblBaseCost
                            Else
                                dblSaleRate = dblCurCost
                            End If

                            dblSaleRate = dblSaleRate + (dblSaleRate * CDbl(txtMargin.Text) / 100)
                            'dblBaseCost = dblBaseCost + (dblBaseCost * CDbl(txtMargin.Text) / 100)
                            'dblCurCost = dblCurCost + (dblCurCost * CDbl(txtMargin.Text) / 100)

                            dblMixID = 0
                            strAssortBox = rsExpVary.Fields("Assortment").Value
                            AdoCN.Execute("INSERT INTO tblCostingTemp2(SupParNo,Pcs,Cts) VALUES('" & strSupParcelNo & "'," & rsComSql.Fields("TrfPcs").Value & "," & Math.Round(rsComSql.Fields("TrfCts").Value, 3) & ")")

                            dblExtLabour = 0
                            vLabour = PFGetLabourCharges(rsExpVary.Fields("Request").Value, (rsComSql.Fields("TrfPcs").Value - intIssPcs), Math.Round(((rsExpVary.Fields("RoughCts").Value / rsExpVary.Fields("RoughPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs)), 3))
                            vLabour = vLabour + ((rsComSql.Fields("TrfPcs").Value - intIssPcs) * 0 * 5)
                            vNFE = Math.Round(dblCurCost * (rsExpVary.Fields("RoughCts").Value / rsExpVary.Fields("RoughPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs), 2)

                            vSubTotal = Math.Round(vLabour + vNFE + dblExtLabour, 2)
                            vCost = Math.Round(vSubTotal / ((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs)), 2)

                            flxDetails.Rows.Add(cmbDepartment.Text, rsExpVary("Assortment").Value, sMFG, sImpDate, vImpInvNo, rsExpVary.Fields("Reference1").Value, rsExpVary.Fields("Reference2").Value,
                                                rsComSql.Fields("TrfPcs").Value - intIssPcs, Math.Round((rsExpVary.Fields("RoughCts").Value / rsExpVary.Fields("RoughPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs), 3), dblCurCost,
                                                dtpInvDate.Value, txtExportNo.Text, rsComSql.Fields("TrfPcs").Value - intIssPcs, Math.Round((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs), 3),
                                                rsExpVary.Fields("StCt").Value, rsExpVary.Fields("Charges").Value, rsExpVary.Fields("Yield").Value, vLabour, vNFE, vCost, vSubTotal, "0", "0",
                                                vMasterPcs, Math.Round(vMasterCts, 3), vSendPcs, Math.Round(vSendCts, 3), sClientID, sOrderRef, sOrderSide, sOrderSubject, strOrigAssort, sBOINo, dblMixID, Math.Round(dblBaseCost, 2),
                                                Math.Round(dblAdjCost, 2), strSupParcelNo, rsExpVary.Fields("NLineNo").Value, "0", "0", "", strAssortBox, "", "", intBalPcs * 0 * 5,
                                                strLotID, "0", "0", strCategory, Math.Round(dblSaleRate, 2))

                            flxDetails.Item(56, flxDetails.Rows.Count - 1).Value = dblExtLabour
                            flxDetails.Item(57, flxDetails.Rows.Count - 1).Value = "0"
                            flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = dblCurCost

                            intBalPcs = intBalPcs - (rsComSql.Fields("TrfPcs").Value - intIssPcs)
                            dblBalCts = dblBalCts - ((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs))
                            dblBalCts = Math.Round(dblBalCts, 3)

                        End If
                    End If
                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing

            GoTo nextrecord

            strOrigAssort = rsExpVary.Fields("OrigAssort").Value
Show_Grid:

nextrecord:
            rsExpVary.MoveNext()
            vLabour = 0
            vCurrPcs = 0
            vExpPcs = 0
            vMasterCts = 0
            vMasterPcs = 0
        Loop
        rsExpVary = Nothing
    End Sub

    Private Sub Load_PolishBoxRecordsNew()
        Dim rsExpVary As New ADODB.Recordset
        Dim rstExpInfo As New ADODB.Recordset
        Dim vLabour, vNFE, vCost, vSubTotal As Double
        Dim sMFG, sImpDate, sBOINo As String
        Dim vImpInvNo As Integer
        Dim vExpPcs, vCurrPcs As Integer
        Dim vMasterPcs As Double
        Dim vMasterCts As Double
        Dim vSendPcs As Integer
        Dim vSendCts As Double

        Dim sClientID, sOrderSubject, sOrderRef As String
        Dim sOrderSide As String

        Dim intOutPcs As Integer
        Dim dblOutCts As Double
        Dim intBalPcs As Integer
        Dim dblBalCts As Double
        Dim strOrigAssort As String
        Dim dblBaseCost As Double
        Dim dblAdjCost As Double
        Dim dblCurCost As Double
        Dim strSupParcelNo As String

        Dim dblMixID As Double

        Dim strAssortBox As String

        Dim intIssPcs As Integer
        Dim dblIssCts As Double

        Dim strLotID As String
        Dim strCategory As String
        Dim dblSaleRate As Double
        Dim strCompCode As String

        rsExpVary = New ADODB.Recordset
        mStrSQL = "SELECT Assortment,Price,Reference1,Reference2,ExpPcs,ExpCts,StCt,Charges,Request,RoughPcs,RoughCts,Yield,Status,OrderRef,OrigAssort,InID,NLineNo,BasePrice,AdjPrice " & _
                  "FROM dbo.tblExportVarification " & _
                  "WHERE (Status = 'A') AND (Reference1 = '" & cmbReference.Text & "') AND (Department = '" & cmbDepartment.Text & "') " & _
                  "ORDER BY Reference1,Reference2"
        rsExpVary.Open(mStrSQL, AdoCN, 1, 1)

        Do While Not rsExpVary.EOF
            sClientID = ""
            sOrderSide = ""
            sOrderSubject = ""

            strCompCode = ""
            rsComSql = New ADODB.Recordset
            If cmbDepartment.Text = "PolishBox" Then
                rsComSql.Open("SELECT * FROM tblPOLSales WHERE SalesNo = '" & cmbReference.Text & "'", AdoCN, 1, 1)
            Else
                rsComSql.Open("SELECT * FROM tblPOLTransfer WHERE TransferNo = '" & cmbReference.Text & "'", AdoCN, 1, 1)
            End If
            If rsComSql.RecordCount Then
                strCompCode = rsComSql.Fields("CompCode").Value
            End If
            rsComSql = Nothing

            rstExpInfo = New ADODB.Recordset
            mStrSQL = "SELECT Assortment, SUM(RoughPcs) AS SendPcs, SUM(RoughCts) AS SendCts " & _
                      "FROM tblCosting GROUP BY Assortment HAVING Assortment = '" & rsExpVary("Assortment").Value & "'"
            rstExpInfo.Open(mStrSQL, AdoCN, 1, 1)
            If Not rstExpInfo.EOF Then
                vSendPcs = rstExpInfo("SendPcs").Value
                vSendCts = rstExpInfo("SendCts").Value
            End If
            rstExpInfo = Nothing

            intOutPcs = 0
            dblOutCts = 0
            sOrderRef = rsExpVary.Fields("OrderRef").Value
            intBalPcs = rsExpVary.Fields("ExpPcs").Value
            dblBalCts = Math.Round(rsExpVary.Fields("ExpCts").Value, 3)
            vMasterPcs = 0
            vMasterCts = 0
            dblCurCost = 0
            strSupParcelNo = ""
            dblBaseCost = rsExpVary.Fields("BasePrice").Value
            dblAdjCost = rsExpVary.Fields("AdjPrice").Value
            dblSaleRate = 0

            strOrigAssort = rsExpVary.Fields("Assortment").Value

            'If strOrigAssort = "" Then
            '    MsgBox(strOrigAssort)
            'End If

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblPOLAssortMatch WHERE NewAssort = '" & strOrigAssort & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                strOrigAssort = rsComSql.Fields("OrigAssort").Value
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            If Mid(rsExpVary("Assortment").Value, 1, 1) = "U" Then
                rsComSql.Open("SELECT  TOP (100) PERCENT RecordID, Department, SystemRefNo, SupplierRefNo, CompanyRefNo, BOINo, InvoiceDate, RecievedDate, SupplierCode, ParcelType, " & _
                            "AssortmentNo, SupParcelNo, DCLParcelNo, INVPcs, INVCts, ACTPcs, ACtCts, ItemSize, Charges, ItemCost, RemPcs, RemCts, Status, DoneBy, ModifyBy, SysDateTime, " & _
                            "ImportNo, LotNo, Article, Remarks, Category, HardCost, CompCode, ItemName, Urgent, NewAssort, LocalInst, PlanFinDate, RghFinDate, SelectCost, " & _
                            "PolPcs AS TrfPcs, PolCts AS TrfCts FROM tblImport " & _
                          "WHERE ROUND(PolCts, 3) > 0 AND CompCode = '" & strCompCode & "' " & _
                          "ORDER BY SysDateTime", AdoCN, 1, 1)
            Else
                rsComSql.Open("SELECT  TOP (100) PERCENT RecordID, Department, SystemRefNo, SupplierRefNo, CompanyRefNo, BOINo, InvoiceDate, RecievedDate, SupplierCode, ParcelType, " & _
                            "AssortmentNo, SupParcelNo, DCLParcelNo, INVPcs, INVCts, ACTPcs, ACtCts, ItemSize, Charges, ItemCost, RemPcs, RemCts, Status, DoneBy, ModifyBy, SysDateTime, " & _
                            "ImportNo, LotNo, Article, Remarks, Category, HardCost, CompCode, ItemName, Urgent, NewAssort, LocalInst, PlanFinDate, RghFinDate, SelectCost, " & _
                            "PolPcs AS TrfPcs, PolCts AS TrfCts FROM tblImport " & _
                          "WHERE ROUND(PolCts, 3) > 0 AND CompCode = '" & strCompCode & "' " & _
                          "ORDER BY SysDateTime", AdoCN, 1, 1)
            End If
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF And dblBalCts > 0
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT SUM(Pcs) AS Pcs,ROUND(SUM(Cts), 2) AS Cts FROM tblCostingTemp2 WHERE SupParNo = '" & rsComSql.Fields("SupParcelNo").Value & "'", AdoCN, 1, 1)
                    If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                        intIssPcs = rsComSql_1.Fields("Pcs").Value
                        dblIssCts = rsComSql_1.Fields("Cts").Value
                    Else
                        intIssPcs = 0
                        dblIssCts = 0
                    End If
                    rsComSql_1 = Nothing

                    If rsComSql.Fields("TrfCts").Value - dblIssCts > 0 Then
                        If rsComSql.Fields("TrfCts").Value - dblIssCts >= dblBalCts Then

                            sMFG = rsComSql.Fields("SupplierRefNo").Value
                            sBOINo = rsComSql.Fields("BOINo").Value
                            sImpDate = rsComSql.Fields("InvoiceDate").Value
                            vImpInvNo = rsComSql.Fields("CompanyRefNo").Value
                            vMasterPcs = vMasterPcs + rsComSql.Fields("ActPcs").Value
                            vMasterCts = vMasterCts + rsComSql.Fields("ActCts").Value
                            dblBaseCost = rsExpVary.Fields("BasePrice").Value
                            dblCurCost = rsExpVary.Fields("Price").Value
                            dblAdjCost = rsExpVary.Fields("AdjPrice").Value
                            strSupParcelNo = rsComSql.Fields("SupParcelNo").Value
                            strLotID = rsComSql.Fields("LotNo").Value
                            strCategory = rsComSql.Fields("Category").Value

                            If dblBaseCost > dblCurCost Then
                                dblSaleRate = dblBaseCost
                            Else
                                dblSaleRate = dblCurCost
                            End If

                            dblSaleRate = dblSaleRate + (dblSaleRate * CDbl(txtMargin.Text) / 100)
                            'dblBaseCost = dblBaseCost + (dblBaseCost * CDbl(txtMargin.Text) / 100)
                            'dblCurCost = dblCurCost + (dblCurCost * CDbl(txtMargin.Text) / 100)

                            dblMixID = 0
                            strAssortBox = rsExpVary.Fields("Assortment").Value
                            AdoCN.Execute("INSERT INTO tblCostingTemp2(SupParNo,Pcs,Cts) VALUES('" & strSupParcelNo & "'," & intBalPcs & "," & Math.Round(dblBalCts, 3) & ")")

                            dblExtLabour = 0
                            vLabour = PFGetLabourCharges(rsExpVary.Fields("Request").Value, intBalPcs, Math.Round(dblBalCts, 3))
                            vNFE = Math.Round(dblCurCost * dblBalCts, 2)

                            vSubTotal = Math.Round(vLabour + vNFE + dblExtLabour, 2)
                            vCost = Math.Round(vSubTotal / dblBalCts, 2)

                            flxDetails.Rows.Add(cmbDepartment.Text, rsExpVary("Assortment").Value, sMFG, sImpDate, vImpInvNo, rsExpVary.Fields("Reference1").Value, rsExpVary.Fields("Reference2").Value,
                                                intBalPcs, Math.Round(dblBalCts, 3), dblCurCost,
                                                dtpInvDate.Value, txtExportNo.Text, intBalPcs, Math.Round(dblBalCts, 3),
                                                rsExpVary.Fields("StCt").Value, rsExpVary.Fields("Charges").Value, rsExpVary.Fields("Yield").Value, vLabour, vNFE, vCost, vSubTotal, "0", "0",
                                                vMasterPcs, Math.Round(vMasterCts, 3), vSendPcs, Math.Round(vSendCts, 3), sClientID, sOrderRef, sOrderSide, sOrderSubject, strOrigAssort, sBOINo, dblMixID, Math.Round(dblBaseCost, 2),
                                                Math.Round(dblAdjCost, 2), strSupParcelNo, rsExpVary.Fields("NLineNo").Value, "0", "0", "", strAssortBox, "", "", intBalPcs * 0 * 5,
                                                strLotID, "0", "0", strCategory, Math.Round(dblSaleRate, 2))

                            flxDetails.Item(56, flxDetails.Rows.Count - 1).Value = dblExtLabour
                            flxDetails.Item(57, flxDetails.Rows.Count - 1).Value = "0"
                            flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = dblCurCost

                            intBalPcs = 0
                            dblBalCts = 0
                        Else
                            sMFG = rsComSql.Fields("SupplierRefNo").Value
                            sBOINo = rsComSql.Fields("BOINo").Value
                            sImpDate = rsComSql.Fields("InvoiceDate").Value
                            vImpInvNo = rsComSql.Fields("CompanyRefNo").Value
                            vMasterPcs = vMasterPcs + rsComSql.Fields("ActPcs").Value
                            vMasterCts = vMasterCts + rsComSql.Fields("ActCts").Value
                            dblBaseCost = rsExpVary.Fields("BasePrice").Value
                            dblCurCost = rsExpVary.Fields("Price").Value
                            dblAdjCost = rsExpVary.Fields("AdjPrice").Value
                            strSupParcelNo = rsComSql.Fields("SupParcelNo").Value
                            strLotID = rsComSql.Fields("LotNo").Value
                            strCategory = rsComSql.Fields("Category").Value

                            If dblBaseCost > dblCurCost Then
                                dblSaleRate = dblBaseCost
                            Else
                                dblSaleRate = dblCurCost
                            End If

                            intOutPcs = Math.Round(((rsComSql.Fields("TrfCts").Value - dblIssCts) / dblBalCts) * intBalPcs, 0)

                            dblSaleRate = dblSaleRate + (dblSaleRate * CDbl(txtMargin.Text) / 100)
                            'dblBaseCost = dblBaseCost + (dblBaseCost * CDbl(txtMargin.Text) / 100)
                            'dblCurCost = dblCurCost + (dblCurCost * CDbl(txtMargin.Text) / 100)

                            dblMixID = 0
                            strAssortBox = rsExpVary.Fields("Assortment").Value
                            AdoCN.Execute("INSERT INTO tblCostingTemp2(SupParNo,Pcs,Cts) VALUES('" & strSupParcelNo & "'," & rsComSql.Fields("TrfPcs").Value - intIssPcs & "," & Math.Round(rsComSql.Fields("TrfCts").Value - dblIssCts, 3) & ")")

                            dblExtLabour = 0
                            vLabour = PFGetLabourCharges(rsExpVary.Fields("Request").Value, (rsComSql.Fields("TrfPcs").Value - intIssPcs), Math.Round(rsComSql.Fields("TrfCts").Value - dblIssCts, 3))
                            vLabour = vLabour + ((rsComSql.Fields("TrfPcs").Value - intIssPcs) * 0 * 5)
                            vNFE = Math.Round(dblCurCost * rsComSql.Fields("TrfCts").Value - dblIssCts, 2)

                            vSubTotal = Math.Round(vLabour + vNFE + dblExtLabour, 2)
                            vCost = Math.Round(vSubTotal / rsComSql.Fields("TrfCts").Value - dblIssCts, 2)

                            flxDetails.Rows.Add(cmbDepartment.Text, rsExpVary("Assortment").Value, sMFG, sImpDate, vImpInvNo, rsExpVary.Fields("Reference1").Value, rsExpVary.Fields("Reference2").Value,
                                                intOutPcs, Math.Round(rsComSql.Fields("TrfCts").Value - dblIssCts, 3), dblCurCost,
                                                dtpInvDate.Value, txtExportNo.Text, intOutPcs, Math.Round(rsComSql.Fields("TrfCts").Value - dblIssCts, 3),
                                                rsExpVary.Fields("StCt").Value, rsExpVary.Fields("Charges").Value, rsExpVary.Fields("Yield").Value, vLabour, vNFE, vCost, vSubTotal, "0", "0",
                                                vMasterPcs, Math.Round(vMasterCts, 3), vSendPcs, Math.Round(vSendCts, 3), sClientID, sOrderRef, sOrderSide, sOrderSubject, strOrigAssort, sBOINo, dblMixID, Math.Round(dblBaseCost, 2),
                                                Math.Round(dblAdjCost, 2), strSupParcelNo, rsExpVary.Fields("NLineNo").Value, "0", "0", "", strAssortBox, "", "", intBalPcs * 0 * 5,
                                                strLotID, "0", "0", strCategory, Math.Round(dblSaleRate, 2))

                            flxDetails.Item(56, flxDetails.Rows.Count - 1).Value = dblExtLabour
                            flxDetails.Item(57, flxDetails.Rows.Count - 1).Value = "0"
                            flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = dblCurCost

                            intBalPcs = intBalPcs - intOutPcs
                            dblBalCts = dblBalCts - (rsComSql.Fields("TrfCts").Value - dblIssCts)
                            dblBalCts = Math.Round(dblBalCts, 3)

                        End If
                    End If
                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing

            GoTo nextrecord

            strOrigAssort = rsExpVary.Fields("OrigAssort").Value
Show_Grid:

nextrecord:
            rsExpVary.MoveNext()
            vLabour = 0
            vCurrPcs = 0
            vExpPcs = 0
            vMasterCts = 0
            vMasterPcs = 0
        Loop
        rsExpVary = Nothing
    End Sub

    Private Sub txtPack_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPack.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtPack.Text <> "" Then
                Load_PackDetails()
            End If
        End If
    End Sub

    Private Sub Load_PackDetails()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblGrading_Pack WHERE PackingListNo = '" & txtPack.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            txtType.Text = rsComSql.Fields("PackType").Value & ""
            txtCategory.Text = rsComSql.Fields("Category").Value & ""
        Else
            txtType.Text = ""
            txtCategory.Text = ""
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_MixRecords(ByVal strReference As String)
        Dim rsExpVary As New ADODB.Recordset
        Dim rstExpInfo As New ADODB.Recordset
        Dim rstImpInfo As New ADODB.Recordset
        Dim rstCheckAssort As New ADODB.Recordset
        Dim rstOrder As New ADODB.Recordset
        Dim vLabour As Double
        Dim vGrLabour As Double
        Dim vNFE As Double
        Dim vCost As Double
        Dim vSubTotal As Double
        Dim sMFG, sImpDate, sBOINo As String
        Dim vImpInvNo As Double
        Dim vExpPcs, vCurrPcs As Integer
        Dim bSatisfied As Boolean

        Dim sClientID, sOrderSubject, sOrderRef As String
        Dim sOrderSide As String

        Dim dblRghCts As Double
        Dim intOutPcs As Integer
        Dim dblOutCts As Double
        Dim intBalPcs As Integer
        Dim dblBalCts As Double
        Dim strOrigAssort As String
        Dim dblBaseCost As Double
        Dim dblAdjCost As Double
        Dim dblCurCost As Double
        Dim dblAvgCost As Double
        Dim strSupParcelNo As String

        Dim dblMixID As Double
        Dim dblNiruOrderNo As Double
        Dim dblOrderItem As Double
        Dim strCommande As String
        Dim strAssortBox As String

        Dim intIssPcs As Integer
        Dim dblIssCts As Double

        Dim intGroove As Integer
        Dim intGrCount As Integer
        Dim intNiruOrderNo As Integer
        Dim intOrderItem As Integer
        'Dim intRow As Integer
        Dim strLotID As String
        Dim dblPerStonePrice As Double

        Dim strNewAssort As String

        Dim strMaxType As String
        Dim dblMaxCost As Double
        Dim dblMaxValue As Double
        Dim dblAskPrice As Double

        'For intRow = 0 To flxDetails.Rows.Count - 1
        '    If cmbReference.Text = flxDetails.Item(5, intRow).Value Then
        '        MsgBox("Already Added - " & cmbReference.Text, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '        Exit Sub
        '    End If
        'Next

        bSatisfied = False

        rsExpVary = New ADODB.Recordset
        mStrSQL = "SELECT Assortment,Price,Reference1, Reference2, ExpPcs, ExpCts, StCt,Charges, Request, RoughPcs, RoughCts, Yield, Status, OrderRef, OrderSide, OrigAssort, InID, NLineNo, BasePrice, AdjPrice " & _
                  "FROM dbo.tblExportVarification " & _
                  "WHERE (Status = 'A') AND Reference1 = '" & strReference & "' AND Department = '" & cmbDepartment.Text & "' " & _
                  "ORDER BY Assortment Asc"
        rsExpVary.Open(mStrSQL, AdoCN, 1, 1)

        Do While Not rsExpVary.EOF
            intNiruOrderNo = 0
            intOrderItem = 0
            sClientID = ""
            sOrderRef = ""
            sOrderSide = ""
            sOrderSubject = ""
            strCommande = ""
            strAssortBox = ""
            intGroove = 0
            intGrCount = 0
            strLotID = ""
            dblRghCts = 0
            strMaxType = "P"
            dblMaxCost = 0
            dblMaxValue = 0
            dblAskPrice = 0

            If Not cmbReference.Text = "Returns" Then
                rstOrder = New ADODB.Recordset
                mStrSQL = "SELECT tblOrdersDtls.OrderNo, tblOrders.Niruref, tblOrdersDtls.Side, tblOrdersDtls.RefNo,tblOrdersDtls.MaxType,tblOrdersDtls.MaxCost, " & _
                                "tblOrders.Subject,tblOrders.NorderNo,tblOrders.OrderItem,tblOrders.COMMANDE, tblOrdersDtls.GrCount, tblOrdersDtls.GrDone, tblOrdersDtls.AskingPrice " & _
                          "FROM tblOrdersDtls INNER JOIN tblOrders ON tblOrdersDtls.OrderNo = tblOrders.OrderNo " & _
                          "WHERE (tblOrdersDtls.OrderNo = " & rsExpVary("Reference1").Value & ") AND (tblOrdersDtls.RefNo = '" & Replace(rsExpVary("OrderRef").Value, "'", "''") & "') AND (tblOrdersDtls.Side = '" & rsExpVary("OrderSide").Value & "')"
                rstOrder.Open(mStrSQL, AdoCN, 1, 1)

                If rstOrder.RecordCount Then
                    sClientID = rstOrder.Fields("Niruref").Value
                    sOrderRef = rsExpVary.Fields("OrderRef").Value
                    sOrderSide = rstOrder.Fields("Side").Value
                    sOrderSubject = rstOrder.Fields("Subject").Value
                    dblNiruOrderNo = rstOrder.Fields("NorderNo").Value
                    dblOrderItem = rstOrder.Fields("OrderItem").Value
                    strCommande = rstOrder.Fields("COMMANDE").Value
                    intGrCount = rstOrder.Fields("GrCount").Value
                    intGroove = rstOrder.Fields("GrDone").Value
                    strMaxType = rstOrder.Fields("MaxType").Value
                    dblMaxCost = rstOrder.Fields("MaxCost").Value
                    dblAskPrice = rstOrder.Fields("AskingPrice").Value
                End If
                rstOrder = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT dbo.tblOrdersDtls.OrderNo, dbo.tblOrdersDtls.NLineNo, dbo.tblOrders.Subject, dbo.tblOrders.NorderNo, dbo.tblOrders.OrderItem, " & _
                                "dbo.tblOrders.COMMANDE, tblOrdersDtls.RefNo, tblOrdersDtls.Side, tblOrdersDtls.MaxType, tblOrdersDtls.MaxCost, tblOrdersDtls.AskingPrice " & _
                              "FROM dbo.tblOrdersDtls INNER JOIN dbo.tblOrders ON dbo.tblOrdersDtls.OrderNo = dbo.tblOrders.OrderNo " & _
                              "WHERE (dbo.tblOrdersDtls.NLineNo = '" & rsExpVary.Fields("NLineNo").Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    sOrderSubject = rsComSql.Fields("Subject").Value
                    dblNiruOrderNo = rsComSql.Fields("NorderNo").Value
                    dblOrderItem = rsComSql.Fields("OrderItem").Value
                    strCommande = rsComSql.Fields("COMMANDE").Value
                    sOrderSide = rsComSql.Fields("Side").Value
                    strMaxType = rsComSql.Fields("MaxType").Value
                    dblMaxCost = rsComSql.Fields("MaxCost").Value
                    dblAskPrice = rsComSql.Fields("AskingPrice").Value
                End If
                rsComSql = Nothing
            End If

            If chkAsking97.Checked = True Then
                dblMaxCost = Math.Ceiling(dblAskPrice * 0.97)
            ElseIf chkAsking100.Checked = True Then
                dblMaxCost = dblAskPrice
            ElseIf chkAsking105.Checked = True Then
                dblMaxCost = Math.Ceiling(dblAskPrice * 1.05)
            ElseIf chkAsking985.Checked = True Then
                dblMaxCost = Math.Ceiling(dblAskPrice * 0.985)
            ElseIf chkAsking80.Checked = True Then
                dblMaxCost = Math.Ceiling(dblAskPrice * 0.8)
            ElseIf chkAsking70.Checked = True Then
                dblMaxCost = Math.Ceiling(dblAskPrice * 0.7)
            ElseIf chkAsking75.Checked = True Then
                dblMaxCost = Math.Ceiling(dblAskPrice * 0.75)
            ElseIf chkAsking50.Checked = True Then
                dblMaxCost = Math.Ceiling(dblAskPrice * 0.5)
            ElseIf chkAsking65.Checked = True Then
                dblMaxCost = Math.Ceiling(dblAskPrice * 0.65)
            ElseIf chkAsking90.Checked = True Then
                dblMaxCost = Math.Ceiling(dblAskPrice * 0.9)
            ElseIf chkAsking85.Checked = True Then
                dblMaxCost = Math.Ceiling(dblAskPrice * 0.85)
            ElseIf chkAsking68.Checked = True Then
                dblMaxCost = Math.Ceiling(dblAskPrice * 0.68)
            ElseIf chkMaxCost.Checked = True Then
                dblMaxCost = dblMaxCost
            Else
                MsgBox("Please Select a Price Code", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            'rstExpInfo = New ADODB.Recordset
            'mStrSQL = "SELECT Assortment, SUM(RoughPcs) AS SendPcs, SUM(RoughCts) AS SendCts " & _
            '          "FROM tblCosting GROUP BY Assortment HAVING Assortment = '" & rsExpVary("Assortment").Value & "'"
            'rstExpInfo.Open(mStrSQL, AdoCN, 1, 1)
            'If Not rstExpInfo.EOF Then
            '    vSendPcs = rstExpInfo("SendPcs").Value
            '    vSendCts = rstExpInfo("SendCts").Value
            'End If
            'rstExpInfo = Nothing

            'If rsExpVary("Assortment") = "ANW3331" Then
            '    MsgBoxGT ""
            'End If

            intOutPcs = 0
            dblOutCts = 0
            intBalPcs = rsExpVary.Fields("ExpPcs").Value
            dblBalCts = Math.Round(rsExpVary.Fields("ExpCts").Value, 2)
            dblBaseCost = 0
            dblAdjCost = 0
            dblCurCost = 0
            dblAvgCost = 0
            strSupParcelNo = ""
            rsComSql = New ADODB.Recordset
            'If sClientID = "CLIENT NO 112" Then
            '    rsComSql.Open("SELECT * FROM tblImport WHERE TrfPcs > 0  AND AssortmentNo NOT LIKE 'AU%' ORDER BY SysDateTime", AdoCN, 1, 1)
            'Else
            '    rsComSql.Open("SELECT * FROM tblImport WHERE TrfPcs > 0  AND AssortmentNo LIKE 'AU%' ORDER BY SysDateTime", AdoCN, 1, 1)
            'End If
            rsComSql.Open("SELECT * FROM tblImport WHERE TrfPcs > 0 ORDER BY SysDateTime", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF And intBalPcs > 0
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT SUM(Pcs) AS Pcs,ROUND(SUM(Cts), 2) AS Cts FROM tblCostingTemp WHERE Assortment = '" & rsComSql.Fields("SupParcelNo").Value & "'", AdoCN, 1, 1)
                    If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                        intIssPcs = rsComSql_1.Fields("Pcs").Value
                        dblIssCts = rsComSql_1.Fields("Cts").Value
                    Else
                        intIssPcs = 0
                        dblIssCts = 0
                    End If
                    rsComSql_1 = Nothing

                    If rsComSql.Fields("TrfPcs").Value - intIssPcs > 0 Then
                        If rsComSql.Fields("TrfPcs").Value - intIssPcs >= intBalPcs Then

                            sMFG = rsComSql.Fields("SupplierRefNo").Value
                            sBOINo = rsComSql.Fields("BOINo").Value
                            sImpDate = rsComSql.Fields("InvoiceDate").Value
                            vImpInvNo = rsComSql.Fields("CompanyRefNo").Value
                            If Mid(rsExpVary("Assortment").Value, 1, 2) = "VM" Or Mid(rsExpVary("Assortment").Value, 1, 2) = "VP" Then
                                strOrigAssort = "VPCU"
                            Else
                                strOrigAssort = "APCU"
                            End If

                            'vMasterPcs = vMasterPcs + rsComSql.Fields("ActPcs").Value
                            'vMasterCts = vMasterCts + rsComSql.Fields("ActCts").Value
                            'vMasterCts = Math.Round(vMasterCts, 2)
                            dblBaseCost = rsExpVary.Fields("BasePrice").Value
                            dblCurCost = rsExpVary.Fields("AdjPrice").Value
                            dblAdjCost = rsExpVary.Fields("Price").Value
                            dblAvgCost = rsExpVary.Fields("AdjPrice").Value
                            strSupParcelNo = rsComSql.Fields("SupParcelNo").Value
                            strOrigAssort = rsExpVary.Fields("OrigAssort").Value
                            strLotID = rsComSql.Fields("LotNo").Value
                            dblRghCts = (rsExpVary.Fields("RoughCts").Value / rsExpVary.Fields("RoughPcs").Value * intBalPcs)

                            'If dblCurCost > dblBaseCost Then
                            '    dblAvgCost = dblCurCost
                            'Else
                            '    dblAvgCost = dblBaseCost
                            'End If

                            dblAvgCost = dblCurCost

                            dblMixID = 0
                            strAssortBox = ""
                            AdoCN.Execute("INSERT INTO tblCostingTemp(Assortment,Pcs,Cts) VALUES('" & strSupParcelNo & "'," & intBalPcs & "," & Math.Round(dblBalCts, 3) & ")")

                            dblExtLabour = 0
                            If Mid(rsExpVary("Assortment").Value, 1, 3) = "ANX" Or Mid(rsExpVary("Assortment").Value, 1, 4) = "AROY" Then
                                vLabour = 0
                            Else
                                vLabour = Math.Round(CDbl(intBalPcs * rsExpVary.Fields("Charges").Value), 2)
                            End If

                            vGrLabour = intBalPcs * intGrCount * 5 * intGroove

                            If Mid(rsExpVary("Assortment").Value, 1, 1) = "S" Then
                                dblPerStonePrice = 0
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & rsExpVary("Assortment").Value & "'", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount Then
                                    If rsComSql_1.Fields("StonePrice").Value <> 0 Then
                                        dblPerStonePrice = rsComSql_1.Fields("StonePrice").Value
                                    End If
                                End If
                                rsComSql_1 = Nothing

                                If dblPerStonePrice <> 0 Then
                                    vNFE = Math.Round(dblAvgCost * dblRghCts, 2)
                                    dblBaseCost = Math.Round(vNFE / dblRghCts, 2)
                                    dblCurCost = dblAvgCost
                                Else
                                    vNFE = Math.Round(dblAvgCost * dblRghCts, 2)
                                End If

                                vSubTotal = Math.Round(vLabour + vGrLabour + vNFE + dblExtLabour, 2)
                                If rsExpVary.Fields("ExpCts").Value <> 0 Then
                                    vCost = Math.Round(vSubTotal / ((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * intBalPcs), 2)
                                Else
                                    vCost = 0
                                End If
                            Else
                                vNFE = Math.Round(dblAvgCost * dblRghCts, 2)

                                vSubTotal = Math.Round(vLabour + vGrLabour + vNFE + dblExtLabour, 2)
                                If rsExpVary.Fields("ExpCts").Value <> 0 Then
                                    vCost = Math.Round(vSubTotal / ((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * intBalPcs), 2)
                                Else
                                    vCost = 0
                                End If
                            End If

                            strAssortBox = rsExpVary.Fields("Assortment").Value
                            strNewAssort = rsExpVary.Fields("Assortment").Value

                            If strMaxType = "P" Then
                                dblMaxValue = dblMaxCost * intBalPcs
                            Else
                                dblMaxValue = dblMaxCost * (rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * intBalPcs
                            End If
                            dblMaxValue = Math.Round(dblMaxValue, 2)

                            flxDetails.Rows.Add(cmbDepartment.Text, strAssortBox, sMFG, sImpDate, vImpInvNo, rsExpVary.Fields("Reference1").Value, rsExpVary.Fields("Reference2").Value, intBalPcs, Math.Round(dblRghCts, 3), Math.Round(dblAvgCost, 2),
                                                dtpInvDate.Value, txtExportNo.Text, intBalPcs, Math.Round((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * intBalPcs, 2),
                                                rsExpVary.Fields("StCt").Value, rsExpVary.Fields("Charges").Value, rsExpVary.Fields("Yield").Value, vLabour, vNFE, vCost, vSubTotal, "0", "0",
                                                0, 0, 0, 0, sClientID, sOrderRef, sOrderSide, sOrderSubject, strOrigAssort, sBOINo, dblMixID, Math.Round(dblBaseCost, 2),
                                                Math.Round(dblCurCost, 2), strSupParcelNo, rsExpVary.Fields("NLineNo").Value, dblNiruOrderNo, dblOrderItem, strCommande, strNewAssort, "", "", vGrLabour,
                                                strLotID)

                            flxDetails.Item(56, flxDetails.Rows.Count - 1).Value = dblExtLabour
                            flxDetails.Item(57, flxDetails.Rows.Count - 1).Value = dblMaxValue
                            flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = Math.Round(dblAvgCost, 2)

                            intBalPcs = 0
                            dblBalCts = 0
                        Else
                            sMFG = rsComSql.Fields("SupplierRefNo").Value
                            sBOINo = rsComSql.Fields("BOINo").Value
                            sImpDate = rsComSql.Fields("InvoiceDate").Value
                            vImpInvNo = rsComSql.Fields("CompanyRefNo").Value
                            If Mid(rsExpVary("Assortment").Value, 1, 2) = "VM" Or Mid(rsExpVary("Assortment").Value, 1, 2) = "VP" Then
                                strOrigAssort = "VPCU"
                            Else
                                strOrigAssort = "APCU"
                            End If
                            'vMasterPcs = vMasterPcs + rsComSql.Fields("ActPcs").Value
                            'vMasterCts = vMasterCts + rsComSql.Fields("ActCts").Value
                            'vMasterCts = Math.Round(vMasterCts, 2)
                            dblBaseCost = rsExpVary.Fields("BasePrice").Value
                            dblCurCost = rsExpVary.Fields("AdjPrice").Value
                            dblAdjCost = rsExpVary.Fields("Price").Value
                            dblAvgCost = rsExpVary.Fields("AdjPrice").Value
                            strSupParcelNo = rsComSql.Fields("SupParcelNo").Value
                            strOrigAssort = rsExpVary.Fields("OrigAssort").Value
                            strLotID = rsComSql.Fields("LotNo").Value
                            dblRghCts = (rsExpVary.Fields("RoughCts").Value / rsExpVary.Fields("RoughPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs)

                            'If dblCurCost > dblBaseCost Then
                            '    dblAvgCost = dblCurCost
                            'Else
                            '    dblAvgCost = dblBaseCost
                            'End If

                            dblAvgCost = dblCurCost

                            dblMixID = 0
                            strAssortBox = ""
                            AdoCN.Execute("INSERT INTO tblCostingTemp(Assortment,Pcs,Cts) VALUES('" & strSupParcelNo & "'," & rsComSql.Fields("TrfPcs").Value & "," & Math.Round(rsComSql.Fields("TrfCts").Value, 3) & ")")

                            dblExtLabour = 0
                            If Mid(rsExpVary("Assortment").Value, 1, 3) = "ANX" Or Mid(rsExpVary("Assortment").Value, 1, 4) = "AROY" Then
                                vLabour = 0
                            Else
                                vLabour = Math.Round(CDbl((rsComSql.Fields("TrfPcs").Value - intIssPcs) * rsExpVary.Fields("Charges").Value), 2)
                            End If

                            vGrLabour = (rsComSql.Fields("TrfPcs").Value - intIssPcs) * intGrCount * 5 * intGroove

                            If Mid(rsExpVary("Assortment").Value, 1, 1) = "S" Then
                                dblPerStonePrice = 0
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & rsExpVary("Assortment").Value & "'", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount Then
                                    If rsComSql_1.Fields("StonePrice").Value <> 0 Then
                                        dblPerStonePrice = rsComSql_1.Fields("StonePrice").Value
                                    End If
                                End If
                                rsComSql_1 = Nothing

                                If dblPerStonePrice <> 0 Then
                                    vNFE = Math.Round(dblAvgCost * dblRghCts, 2)
                                    dblBaseCost = Math.Round(vNFE / dblRghCts, 2)
                                    dblCurCost = dblAvgCost
                                Else
                                    vNFE = Math.Round(dblAvgCost * dblRghCts, 2)
                                End If

                                vSubTotal = Math.Round(vLabour + vGrLabour + vNFE + dblExtLabour, 2)
                                vCost = Math.Round(vSubTotal / ((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * intBalPcs), 2)
                            Else
                                vNFE = Math.Round(dblAvgCost * dblRghCts, 2)

                                vSubTotal = Math.Round(vLabour + vGrLabour + vNFE + dblExtLabour, 2)
                                vCost = Math.Round(vSubTotal / ((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs)), 2)
                            End If

                            strAssortBox = rsExpVary.Fields("Assortment").Value
                            strNewAssort = rsExpVary.Fields("Assortment").Value

                            If strMaxType = "P" Then
                                dblMaxValue = dblMaxCost * (rsComSql.Fields("TrfPcs").Value - intIssPcs)
                            Else
                                dblMaxValue = dblMaxCost * (rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs)
                            End If
                            dblMaxValue = Math.Round(dblMaxValue, 2)

                            flxDetails.Rows.Add(cmbDepartment.Text, strAssortBox, sMFG, sImpDate, vImpInvNo, rsExpVary.Fields("Reference1").Value, rsExpVary.Fields("Reference2").Value, rsComSql.Fields("TrfPcs").Value - intIssPcs, Math.Round(dblRghCts, 3), dblAvgCost,
                                                dtpInvDate.Value, txtExportNo.Text, rsComSql.Fields("TrfPcs").Value - intIssPcs, Math.Round((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs), 2),
                                                rsExpVary.Fields("StCt").Value, rsExpVary.Fields("Charges").Value, rsExpVary.Fields("Yield").Value, vLabour, vNFE, vCost, vSubTotal, "0", "0",
                                                0, 0, 0, 0, sClientID, sOrderRef, sOrderSide, sOrderSubject, strOrigAssort, sBOINo, dblMixID, Math.Round(dblBaseCost, 2),
                                                Math.Round(dblCurCost, 2), strSupParcelNo, rsExpVary.Fields("NLineNo").Value, dblNiruOrderNo, dblOrderItem, strCommande, strNewAssort, "", "", vGrLabour,
                                                strLotID)

                            flxDetails.Item(56, flxDetails.Rows.Count - 1).Value = dblExtLabour
                            flxDetails.Item(57, flxDetails.Rows.Count - 1).Value = dblMaxValue
                            flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = Math.Round(dblAvgCost, 2)

                            intBalPcs = intBalPcs - (rsComSql.Fields("TrfPcs").Value - intIssPcs)
                            dblBalCts = dblBalCts - Math.Round(((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs)), 2)
                            dblBalCts = Math.Round(dblBalCts, 2)

                        End If
                    End If
                    rsComSql.MoveNext()
                End While
            End If

            rsExpVary.MoveNext()
            vLabour = 0
            vCurrPcs = 0
            vExpPcs = 0
        Loop
        rsExpVary = Nothing
    End Sub

    Private Sub Load_KitRecords(ByVal strReference As String)
        Dim rsExpVary As New ADODB.Recordset
        Dim rstExpInfo As New ADODB.Recordset
        Dim rstImpInfo As New ADODB.Recordset
        Dim rstCheckAssort As New ADODB.Recordset
        Dim rstOrder As New ADODB.Recordset
        Dim vLabour As Double
        Dim vGrLabour As Double
        Dim vNFE As Double
        Dim vCost As Double
        Dim vSubTotal As Double
        Dim sMFG, sImpDate, sBOINo As String
        Dim vImpInvNo As Double
        Dim vExpPcs, vCurrPcs As Integer
        Dim bSatisfied As Boolean
        Dim vMasterPcs As Long
        Dim vMasterCts As Double
        Dim vSendPcs As Integer
        Dim vSendCts As Double

        Dim sClientID, sOrderSubject, sOrderRef As String
        Dim sOrderSide As String

        Dim dblRghCts As Double
        Dim intOutPcs As Integer
        Dim dblOutCts As Double
        Dim intBalPcs As Integer
        Dim dblBalCts As Double
        Dim strOrigAssort As String
        Dim dblBaseCost As Double
        Dim dblAdjCost As Double
        Dim dblCurCost As Double
        Dim dblAvgCost As Double
        Dim strSupParcelNo As String

        Dim dblMixID As Double
        Dim dblNiruOrderNo As Double
        Dim dblOrderItem As Double
        Dim strCommande As String
        Dim strAssortBox As String

        Dim intIssPcs As Integer
        Dim dblIssCts As Double

        Dim intGroove As Integer
        Dim intGrCount As Integer
        Dim intNiruOrderNo As Integer
        Dim intOrderItem As Integer
        Dim intRow As Integer
        Dim strLotID As String
        Dim dblPerStonePrice As Double

        Dim strNewAssort As String

        Dim strMaxType As String
        Dim dblMaxCost As Double
        Dim dblMaxValue As Double

        For intRow = 0 To flxDetails.Rows.Count - 1
            If cmbReference.Text = flxDetails.Item(5, intRow).Value Then
                MsgBox("Already Added - " & cmbReference.Text, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        bSatisfied = False

        rsExpVary = New ADODB.Recordset
        mStrSQL = "SELECT Assortment,Price,Reference1, Reference2, ExpPcs, ExpCts, StCt,Charges, Request, RoughPcs, RoughCts, Yield, Status, OrderRef, OrderSide, OrigAssort, InID, NLineNo, BasePrice, AdjPrice " & _
                  "FROM dbo.tblExportVarification " & _
                  "WHERE (Status = 'A') AND Reference1 = '" & strReference & "' AND Department = '" & cmbDepartment.Text & "' " & _
                  "ORDER BY Assortment Asc"
        rsExpVary.Open(mStrSQL, AdoCN, 1, 1)

        Do While Not rsExpVary.EOF
            intNiruOrderNo = 0
            intOrderItem = 0
            sClientID = ""
            sOrderRef = ""
            sOrderSide = ""
            sOrderSubject = ""
            strCommande = ""
            strAssortBox = ""
            intGroove = 0
            intGrCount = 0
            strLotID = ""
            dblRghCts = 0
            strMaxType = "P"
            dblMaxCost = 0
            dblMaxValue = 0

            If Not cmbReference.Text = "Returns" Then
                rstOrder = New ADODB.Recordset
                mStrSQL = "SELECT tblKITOrdersDtls.OrderNo, tblKITOrders.Niruref, tblKITOrdersDtls.Side, tblKITOrdersDtls.RefNo,tblKITOrdersDtls.MaxType,tblKITOrdersDtls.OutPrice, " & _
                                "tblKITOrders.Subject,tblKITOrders.NorderNo,tblKITOrders.OrderItem,tblKITOrders.COMMANDE, tblKITOrdersDtls.GrCount, tblKITOrdersDtls.GrDone,tblKITOrdersDtls.Remarks " & _
                          "FROM tblKITOrdersDtls INNER JOIN tblKITOrders ON tblKITOrdersDtls.OrderNo = tblKITOrders.OrderNo " & _
                          "WHERE (tblKITOrdersDtls.OrderNo = " & rsExpVary("Reference1").Value & ") AND (tblKITOrdersDtls.RefNo = '" & rsExpVary("OrderRef").Value & "') AND (tblKITOrdersDtls.Side = '" & rsExpVary("OrderSide").Value & "')"
                rstOrder.Open(mStrSQL, AdoCN, 1, 1)

                If rstOrder.RecordCount Then
                    sClientID = rstOrder.Fields("Niruref").Value
                    sOrderRef = rsExpVary.Fields("OrderRef").Value
                    sOrderSide = rstOrder.Fields("Side").Value
                    sOrderSubject = rstOrder.Fields("Remarks").Value
                    dblNiruOrderNo = rstOrder.Fields("NorderNo").Value
                    dblOrderItem = rstOrder.Fields("OrderItem").Value
                    strCommande = rstOrder.Fields("COMMANDE").Value
                    intGrCount = rstOrder.Fields("GrCount").Value
                    intGroove = rstOrder.Fields("GrDone").Value
                    strMaxType = rstOrder.Fields("MaxType").Value
                    dblMaxCost = rstOrder.Fields("OutPrice").Value
                End If
                rstOrder = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT dbo.tblKITOrdersDtls.OrderNo, dbo.tblKITOrdersDtls.NLineNo, dbo.tblKITOrders.Subject, dbo.tblKITOrders.NorderNo, dbo.tblKITOrders.OrderItem, " & _
                                "dbo.tblKITOrders.COMMANDE, tblKITOrdersDtls.RefNo, tblKITOrdersDtls.Side, tblKITOrdersDtls.MaxType, tblKITOrdersDtls.OutPrice,tblKITOrdersDtls.Remarks " & _
                              "FROM dbo.tblKITOrdersDtls INNER JOIN dbo.tblKITOrders ON dbo.tblKITOrdersDtls.OrderNo = dbo.tblKITOrders.OrderNo " & _
                              "WHERE (dbo.tblKITOrdersDtls.NLineNo = '" & rsExpVary.Fields("NLineNo").Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    sOrderSubject = rsComSql.Fields("Remarks").Value
                    dblNiruOrderNo = rsComSql.Fields("NorderNo").Value
                    dblOrderItem = rsComSql.Fields("OrderItem").Value
                    strCommande = rsComSql.Fields("COMMANDE").Value
                    sOrderSide = rsComSql.Fields("Side").Value
                    strMaxType = rsComSql.Fields("MaxType").Value
                    dblMaxCost = rsComSql.Fields("OutPrice").Value
                End If
                rsComSql = Nothing
            End If

            rstExpInfo = New ADODB.Recordset
            mStrSQL = "SELECT Assortment, SUM(RoughPcs) AS SendPcs, SUM(RoughCts) AS SendCts " & _
                      "FROM tblCosting GROUP BY Assortment HAVING Assortment = '" & rsExpVary("Assortment").Value & "'"
            rstExpInfo.Open(mStrSQL, AdoCN, 1, 1)
            If Not rstExpInfo.EOF Then
                vSendPcs = rstExpInfo("SendPcs").Value
                vSendCts = rstExpInfo("SendCts").Value
            End If
            rstExpInfo = Nothing

            'If rsExpVary("Assortment") = "ANW3331" Then
            '    MsgBoxGT ""
            'End If

            intOutPcs = 0
            dblOutCts = 0
            intBalPcs = rsExpVary.Fields("ExpPcs").Value
            dblBalCts = Math.Round(rsExpVary.Fields("ExpCts").Value, 3)
            vMasterPcs = 0
            vMasterCts = 0
            dblBaseCost = 0
            dblAdjCost = 0
            dblCurCost = 0
            dblAvgCost = 0
            strSupParcelNo = ""
            rsComSql = New ADODB.Recordset
            If strDBName = "DiaStock" Then
                rsComSql.Open("SELECT * FROM tblImport WHERE TrfPcs > 0 ORDER BY SysDateTime", AdoCN, 1, 1)
            Else
                If sClientID = "CLIENT NO 112" Then
                    rsComSql.Open("SELECT * FROM tblImport WHERE TrfPcs > 0  AND AssortmentNo NOT LIKE 'AU%' ORDER BY SysDateTime", AdoCN, 1, 1)
                Else
                    rsComSql.Open("SELECT * FROM tblImport WHERE TrfPcs > 0  AND AssortmentNo LIKE 'AU%' ORDER BY SysDateTime", AdoCN, 1, 1)
                End If
            End If
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF And intBalPcs > 0
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT SUM(Pcs) AS Pcs,ROUND(SUM(Cts), 2) AS Cts FROM tblCostingTemp WHERE Assortment = '" & rsComSql.Fields("SupParcelNo").Value & "'", AdoCN, 1, 1)
                    If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                        intIssPcs = rsComSql_1.Fields("Pcs").Value
                        dblIssCts = rsComSql_1.Fields("Cts").Value
                    Else
                        intIssPcs = 0
                        dblIssCts = 0
                    End If
                    rsComSql_1 = Nothing

                    If rsComSql.Fields("TrfPcs").Value - intIssPcs > 0 Then
                        If rsComSql.Fields("TrfPcs").Value - intIssPcs >= intBalPcs Then

                            sMFG = rsComSql.Fields("SupplierRefNo").Value
                            sBOINo = rsComSql.Fields("BOINo").Value
                            sImpDate = rsComSql.Fields("InvoiceDate").Value
                            vImpInvNo = rsComSql.Fields("CompanyRefNo").Value
                            If Mid(rsExpVary("Assortment").Value, 1, 2) = "VM" Or Mid(rsExpVary("Assortment").Value, 1, 2) = "VP" Then
                                strOrigAssort = "VPCU"
                            Else
                                strOrigAssort = "APCU"
                            End If
                            vMasterPcs = vMasterPcs + rsComSql.Fields("ActPcs").Value
                            vMasterCts = vMasterCts + rsComSql.Fields("ActCts").Value
                            vMasterCts = Math.Round(vMasterCts, 2)
                            dblBaseCost = rsExpVary.Fields("BasePrice").Value
                            dblCurCost = rsExpVary.Fields("AdjPrice").Value
                            dblAdjCost = rsExpVary.Fields("Price").Value
                            dblAvgCost = rsExpVary.Fields("AdjPrice").Value
                            strSupParcelNo = rsComSql.Fields("SupParcelNo").Value
                            strOrigAssort = rsExpVary.Fields("OrigAssort").Value
                            strLotID = rsComSql.Fields("LotNo").Value
                            dblRghCts = (rsExpVary.Fields("RoughCts").Value / rsExpVary.Fields("RoughPcs").Value * intBalPcs)

                            dblAvgCost = dblCurCost

                            dblMixID = 0
                            strAssortBox = ""
                            AdoCN.Execute("INSERT INTO tblCostingTemp(Assortment,Pcs,Cts) VALUES('" & strSupParcelNo & "'," & intBalPcs & "," & Math.Round(dblBalCts, 3) & ")")

                            dblExtLabour = 0
                            If Mid(rsExpVary("Assortment").Value, 1, 3) = "ANX" Then
                                vLabour = 0
                            Else
                                vLabour = Math.Round(CDbl(intBalPcs * rsExpVary.Fields("Charges").Value), 2)
                            End If

                            vGrLabour = intBalPcs * intGrCount * 5 * intGroove

                            If Mid(rsExpVary("Assortment").Value, 1, 1) = "S" Then
                                dblPerStonePrice = 0
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & rsExpVary("Assortment").Value & "'", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount Then
                                    If rsComSql_1.Fields("AvgStonePrice").Value <> 0 Then
                                        dblPerStonePrice = rsComSql_1.Fields("AvgStonePrice").Value
                                    End If
                                End If
                                rsComSql_1 = Nothing

                                If dblPerStonePrice <> 0 Then
                                    vNFE = Math.Round(dblPerStonePrice * intBalPcs, 2)
                                    dblAvgCost = Math.Round(vNFE / dblRghCts, 2)
                                    dblBaseCost = dblAvgCost
                                    dblCurCost = dblAvgCost
                                Else
                                    vNFE = Math.Round(dblAvgCost * dblRghCts, 2)
                                End If

                                vSubTotal = Math.Round(vLabour + vGrLabour + vNFE + dblExtLabour, 2)
                                If rsExpVary.Fields("ExpCts").Value <> 0 Then
                                    vCost = Math.Round(vSubTotal / ((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * intBalPcs), 2)
                                Else
                                    vCost = 0
                                End If
                            Else
                                vNFE = Math.Round(dblAvgCost * dblRghCts, 2)

                                vSubTotal = Math.Round(vLabour + vGrLabour + vNFE + dblExtLabour, 2)
                                If rsExpVary.Fields("ExpCts").Value <> 0 Then
                                    vCost = Math.Round(vSubTotal / ((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * intBalPcs), 2)
                                Else
                                    vCost = 0
                                End If
                            End If

                            strAssortBox = rsExpVary.Fields("Assortment").Value
                            strNewAssort = rsExpVary.Fields("Assortment").Value

                            If strMaxType = "P" Then
                                dblMaxValue = dblMaxCost * intBalPcs
                            Else
                                dblMaxValue = dblMaxCost * (rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * intBalPcs
                            End If
                            dblMaxValue = Math.Round(dblMaxValue, 2)

                            flxDetails.Rows.Add(cmbDepartment.Text, strAssortBox, sMFG, sImpDate, vImpInvNo, rsExpVary.Fields("Reference1").Value, rsExpVary.Fields("Reference2").Value, intBalPcs, Math.Round(dblRghCts, 3), Math.Round(dblAvgCost, 2),
                                                dtpInvDate.Value, txtExportNo.Text, intBalPcs, Math.Round((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * intBalPcs, 3),
                                                rsExpVary.Fields("StCt").Value, rsExpVary.Fields("Charges").Value, rsExpVary.Fields("Yield").Value, vLabour, vNFE, vCost, vSubTotal, "0", "0",
                                                vMasterPcs, Math.Round(vMasterCts, 3), vSendPcs, Math.Round(vSendCts, 3), sClientID, sOrderRef, sOrderSide, sOrderSubject, strOrigAssort, sBOINo, dblMixID, Math.Round(dblBaseCost, 2),
                                                Math.Round(dblCurCost, 2), strSupParcelNo, rsExpVary.Fields("NLineNo").Value, dblNiruOrderNo, dblOrderItem, strCommande, strNewAssort, "", "", vGrLabour,
                                                strLotID)

                            flxDetails.Item(56, flxDetails.Rows.Count - 1).Value = dblExtLabour
                            flxDetails.Item(57, flxDetails.Rows.Count - 1).Value = dblMaxValue
                            flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = Math.Round(dblAvgCost, 2)

                            intBalPcs = 0
                            dblBalCts = 0
                        Else
                            sMFG = rsComSql.Fields("SupplierRefNo").Value
                            sBOINo = rsComSql.Fields("BOINo").Value
                            sImpDate = rsComSql.Fields("InvoiceDate").Value
                            vImpInvNo = rsComSql.Fields("CompanyRefNo").Value
                            If Mid(rsExpVary("Assortment").Value, 1, 2) = "VM" Or Mid(rsExpVary("Assortment").Value, 1, 2) = "VP" Then
                                strOrigAssort = "VPCU"
                            Else
                                strOrigAssort = "APCU"
                            End If
                            vMasterPcs = vMasterPcs + rsComSql.Fields("ActPcs").Value
                            vMasterCts = vMasterCts + rsComSql.Fields("ActCts").Value
                            vMasterCts = Math.Round(vMasterCts, 2)
                            dblBaseCost = rsExpVary.Fields("BasePrice").Value
                            dblCurCost = rsExpVary.Fields("AdjPrice").Value
                            dblAdjCost = rsExpVary.Fields("Price").Value
                            dblAvgCost = rsExpVary.Fields("AdjPrice").Value
                            strSupParcelNo = rsComSql.Fields("SupParcelNo").Value
                            strOrigAssort = rsExpVary.Fields("OrigAssort").Value
                            strLotID = rsComSql.Fields("LotNo").Value
                            dblRghCts = (rsExpVary.Fields("RoughCts").Value / rsExpVary.Fields("RoughPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs)

                            dblAvgCost = dblCurCost

                            dblMixID = 0
                            strAssortBox = ""
                            AdoCN.Execute("INSERT INTO tblCostingTemp(Assortment,Pcs,Cts) VALUES('" & strSupParcelNo & "'," & rsComSql.Fields("TrfPcs").Value & "," & Math.Round(rsComSql.Fields("TrfCts").Value, 3) & ")")

                            dblExtLabour = 0
                            If Mid(rsExpVary("Assortment").Value, 1, 3) = "ANX" Then
                                vLabour = 0
                            Else
                                vLabour = Math.Round(CDbl((rsComSql.Fields("TrfPcs").Value - intIssPcs) * rsExpVary.Fields("Charges").Value), 2)
                            End If

                            vGrLabour = (rsComSql.Fields("TrfPcs").Value - intIssPcs) * intGrCount * 5 * intGroove

                            If Mid(rsExpVary("Assortment").Value, 1, 1) = "S" Then
                                dblPerStonePrice = 0
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & rsExpVary("Assortment").Value & "'", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount Then
                                    If rsComSql_1.Fields("AvgStonePrice").Value <> 0 Then
                                        dblPerStonePrice = rsComSql_1.Fields("AvgStonePrice").Value
                                    End If
                                End If
                                rsComSql_1 = Nothing

                                If dblPerStonePrice <> 0 Then
                                    vNFE = Math.Round(dblPerStonePrice * (rsComSql.Fields("TrfPcs").Value - intIssPcs), 2)
                                    dblAvgCost = Math.Round(vNFE / dblRghCts, 2)
                                    dblBaseCost = dblAvgCost
                                    dblCurCost = dblAvgCost
                                Else
                                    vNFE = Math.Round(dblAvgCost * dblRghCts, 2)
                                End If

                                vSubTotal = Math.Round(vLabour + vGrLabour + vNFE + dblExtLabour, 2)
                                vCost = Math.Round(vSubTotal / ((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * intBalPcs), 2)
                            Else
                                vNFE = Math.Round(dblAvgCost * dblRghCts, 2)

                                vSubTotal = Math.Round(vLabour + vGrLabour + vNFE + dblExtLabour, 2)
                                vCost = Math.Round(vSubTotal / ((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs)), 2)
                            End If

                            strAssortBox = rsExpVary.Fields("Assortment").Value
                            strNewAssort = rsExpVary.Fields("Assortment").Value

                            If strMaxType = "P" Then
                                dblMaxValue = dblMaxCost * (rsComSql.Fields("TrfPcs").Value - intIssPcs)
                            Else
                                dblMaxValue = dblMaxCost * (rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs)
                            End If
                            dblMaxValue = Math.Round(dblMaxValue, 2)

                            flxDetails.Rows.Add(cmbDepartment.Text, strAssortBox, sMFG, sImpDate, vImpInvNo, rsExpVary.Fields("Reference1").Value, rsExpVary.Fields("Reference2").Value, rsComSql.Fields("TrfPcs").Value - intIssPcs, Math.Round(dblRghCts, 3), dblAvgCost,
                                                dtpInvDate.Value, txtExportNo.Text, rsComSql.Fields("TrfPcs").Value - intIssPcs, Math.Round((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs), 3),
                                                rsExpVary.Fields("StCt").Value, rsExpVary.Fields("Charges").Value, rsExpVary.Fields("Yield").Value, vLabour, vNFE, vCost, vSubTotal, "0", "0",
                                                vMasterPcs, Math.Round(vMasterCts, 3), vSendPcs, Math.Round(vSendCts, 3), sClientID, sOrderRef, sOrderSide, sOrderSubject, strOrigAssort, sBOINo, dblMixID, Math.Round(dblBaseCost, 2),
                                                Math.Round(dblCurCost, 2), strSupParcelNo, rsExpVary.Fields("NLineNo").Value, dblNiruOrderNo, dblOrderItem, strCommande, strNewAssort, "", "", vGrLabour,
                                                strLotID)

                            flxDetails.Item(56, flxDetails.Rows.Count - 1).Value = dblExtLabour
                            flxDetails.Item(57, flxDetails.Rows.Count - 1).Value = dblMaxValue
                            flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = Math.Round(dblAvgCost, 2)

                            intBalPcs = intBalPcs - (rsComSql.Fields("TrfPcs").Value - intIssPcs)
                            dblBalCts = dblBalCts - Math.Round(((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs)), 2)
                            dblBalCts = Math.Round(dblBalCts, 2)

                        End If
                    End If
                    rsComSql.MoveNext()
                End While
            End If

            rsExpVary.MoveNext()
            vLabour = 0
            vCurrPcs = 0
            vExpPcs = 0
            vMasterCts = 0
            vMasterPcs = 0
        Loop
        rsExpVary = Nothing
    End Sub

    Private Sub Load_ExportRecords()
        Dim rsExpVary As New ADODB.Recordset
        Dim rstExpInfo As New ADODB.Recordset
        Dim rstImpInfo As New ADODB.Recordset
        Dim rstCheckAssort As New ADODB.Recordset
        Dim rstOrder As New ADODB.Recordset
        Dim vLabour, vNFE, vCost, vSubTotal As Double
        Dim sMFG, sImpDate, sBOINo As String
        Dim vImpInvNo As Integer
        Dim vExpPcs, vCurrPcs As Integer
        Dim vMasterPcs As Long
        Dim vMasterCts As Double
        Dim vSendPcs As Integer
        Dim vSendCts As Double
        Dim sClientID, sOrderSubject, sOrderRef As String
        Dim sOrderSide As String

        Dim intOutPcs As Integer
        Dim dblOutCts As Double
        Dim intBalPcs As Integer
        Dim dblBalCts As Double
        Dim strOrigAssort As String
        Dim dblBaseCost As Double
        Dim dblAdjCost As Double
        Dim dblCurCost As Double
        Dim strSupParcelNo As String

        Dim dblMixID As Double
        Dim dblNiruOrderNo As Double
        Dim dblOrderItem As Double
        Dim strCommande As String
        Dim strAssortBox As String
        Dim intNiruOrderNo As Integer
        Dim intOrderItem As Integer
        Dim intIssPcs As Integer
        Dim dblIssCts As Double
        Dim dblLotNo As Double
        Dim dblPerStonePrice As Double
        Dim dblPerStonePriceSales As Double
        Dim dblAvgCost As Double
        Dim dblMaxValue As Double

        rsExpVary = New ADODB.Recordset
        mStrSQL = "SELECT Assortment, Price, Reference1, Reference2, ExpPcs, ExpCts, StCt,Charges, Request, RoughPcs, RoughCts, Yield, Status,OrderRef, OrigAssort, InID, NLineNo, BasePrice, AdjPrice " & _
                  "FROM dbo.tblExportVarification " & _
                  "WHERE (Status = 'A') AND Reference1 = '" & cmbReference.Text & "' AND Department = 'Exports' " & _
                  "ORDER BY Assortment"
        rsExpVary.Open(mStrSQL, AdoCN, 1, 1)
        Do While Not rsExpVary.EOF
            intNiruOrderNo = 0
            intOrderItem = 0
            strCommande = ""
            strAssortBox = ""
            sClientID = ""
            sOrderRef = ""
            sOrderSide = ""
            sOrderSubject = ""

            If Not cmbReference.Text = "Exports" Then
                rstOrder = New ADODB.Recordset
                mStrSQL = "SELECT tblOrdersDtls.OrderNo, tblOrders.Niruref, tblOrdersDtls.Side, tblOrdersDtls.RefNo," & _
                                "tblOrders.Subject,tblOrders.NorderNo,tblOrders.OrderItem,tblOrders.COMMANDE " & _
                          "FROM tblOrdersDtls INNER JOIN tblOrders ON tblOrdersDtls.OrderNo = tblOrders.OrderNo " & _
                          "WHERE (tblOrdersDtls.OrderNo = " & rsExpVary("Reference1").Value & ") AND (tblOrdersDtls.RefNo = '" & rsExpVary("OrderRef").Value & "')"
                rstOrder.Open(mStrSQL, AdoCN, 1, 1)
                If Not rstOrder.EOF Then
                    sClientID = rstOrder.Fields("Niruref").Value
                    sOrderRef = rsExpVary.Fields("OrderRef").Value
                    sOrderSide = rstOrder.Fields("Side").Value
                    sOrderSubject = rstOrder.Fields("Subject").Value
                    dblNiruOrderNo = rstOrder.Fields("NorderNo").Value
                    dblOrderItem = rstOrder.Fields("OrderItem").Value
                    strCommande = rstOrder.Fields("COMMANDE").Value
                End If
                rstOrder = Nothing
            End If

            rstExpInfo = New ADODB.Recordset
            mStrSQL = "SELECT Assortment, SUM(RoughPcs) AS SendPcs, SUM(RoughCts) AS SendCts " & _
                      "FROM tblCosting GROUP BY Assortment HAVING Assortment = '" & rsExpVary("Assortment").Value & "'"
            rstExpInfo.Open(mStrSQL, AdoCN, 1, 1)
            If Not rstExpInfo.EOF Then
                vSendPcs = rstExpInfo("SendPcs").Value
                vSendCts = rstExpInfo("SendCts").Value
            End If
            rstExpInfo = Nothing

            intOutPcs = 0
            dblOutCts = 0
            intBalPcs = rsExpVary.Fields("ExpPcs").Value
            dblBalCts = Math.Round(rsExpVary.Fields("ExpCts").Value, 3)
            vMasterPcs = 0
            vMasterCts = 0
            dblBaseCost = 0
            dblAdjCost = 0
            dblCurCost = 0
            strSupParcelNo = ""
            dblLotNo = 0
            dblAvgCost = 0
            dblMaxValue = 0
            dblPerStonePriceSales = 0

            'If rsExpVary![Assortment] = "ANT3516" Then
            '    MsgBoxGT rsExpVary![Assortment]
            'End If

            rsComSql = New ADODB.Recordset
            If strDBName = "DiaStock" Then
                rsComSql.Open("SELECT * FROM tblImportCopy WHERE TrfPcs > 0 ORDER BY SysDateTime", AdoCN, 1, 1)
            Else
                rsComSql.Open("SELECT * FROM tblImport WHERE TrfPcs > 0 ORDER BY SysDateTime", AdoCN, 1, 1)
            End If
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF And intBalPcs > 0
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT SUM(Pcs) AS Pcs,ROUND(SUM(Cts), 2) AS Cts FROM tblCostingTemp WHERE Assortment = '" & rsComSql.Fields("AssortmentNo").Value & "'", AdoCN, 1, 1)
                    If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                        intIssPcs = rsComSql_1.Fields("Pcs").Value
                        dblIssCts = rsComSql_1.Fields("Cts").Value
                    Else
                        intIssPcs = 0
                        dblIssCts = 0
                    End If
                    rsComSql_1 = Nothing

                    If rsComSql.Fields("TrfPcs").Value - intIssPcs > 0 Then
                        If rsComSql.Fields("TrfPcs").Value - intIssPcs >= intBalPcs Then

                            sMFG = rsComSql.Fields("SupplierRefNo").Value
                            sBOINo = rsComSql.Fields("BOINo").Value
                            sImpDate = rsComSql.Fields("InvoiceDate").Value
                            vImpInvNo = rsComSql.Fields("CompanyRefNo").Value
                            If Mid(rsExpVary("Assortment").Value, 1, 2) = "VM" Or Mid(rsExpVary("Assortment").Value, 1, 2) = "VP" Then
                                strOrigAssort = "VPCU"
                            Else
                                strOrigAssort = "APCU"
                            End If
                            vMasterPcs = vMasterPcs + rsComSql.Fields("ActPcs").Value
                            vMasterCts = vMasterCts + rsComSql.Fields("ActCts").Value
                            vMasterCts = Math.Round(vMasterCts, 2)
                            dblBaseCost = rsExpVary.Fields("BasePrice").Value
                            dblCurCost = rsExpVary.Fields("AdjPrice").Value
                            dblAdjCost = rsExpVary.Fields("AdjPrice").Value
                            strSupParcelNo = rsComSql.Fields("SupParcelNo").Value
                            strOrigAssort = rsExpVary.Fields("OrigAssort").Value
                            dblLotNo = rsComSql.Fields("LotNo").Value

                            dblMixID = 0
                            strAssortBox = ""
                            AdoCN.Execute("INSERT INTO tblCostingTemp(Assortment,Pcs,Cts) VALUES('" & rsComSql.Fields("AssortmentNo").Value & "'," & intBalPcs & "," & Math.Round(dblBalCts, 3) & ")")

                            dblAvgCost = Math.Round(rsExpVary.Fields("Price").Value, 2)
                            dblExtLabour = 0
                            vLabour = Math.Round(CDbl(intBalPcs * rsExpVary.Fields("Charges").Value), 2)

                            If Mid(rsExpVary("Assortment").Value, 1, 1) = "S" Then
                                dblPerStonePrice = 0
                                dblPerStonePriceSales = 0
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & rsExpVary("Assortment").Value & "'", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount Then
                                    If rsComSql_1.Fields("AvgStonePrice").Value <> 0 Then
                                        dblPerStonePrice = rsComSql_1.Fields("AvgStonePrice").Value
                                    End If
                                    dblPerStonePriceSales = rsComSql_1.Fields("StonePrice").Value
                                End If
                                rsComSql_1 = Nothing

                                If dblPerStonePrice <> 0 Then
                                    vNFE = Math.Round(dblPerStonePriceSales * intBalPcs, 2)
                                    dblBaseCost = Math.Round(vNFE / ((rsExpVary.Fields("RoughCts").Value / rsExpVary.Fields("RoughPcs").Value) * intBalPcs), 2)

                                    vNFE = Math.Round(dblAvgCost * ((rsExpVary.Fields("RoughCts").Value / rsExpVary.Fields("RoughPcs").Value) * intBalPcs), 2)
                                    dblCurCost = dblAvgCost
                                Else
                                    vNFE = Math.Round(dblAvgCost * ((rsExpVary.Fields("RoughCts").Value / rsExpVary.Fields("RoughPcs").Value) * intBalPcs), 2)
                                End If

                                vSubTotal = Math.Round(vLabour + vNFE + dblExtLabour, 2)
                                If rsExpVary.Fields("ExpCts").Value <> 0 Then
                                    vCost = Math.Round(vSubTotal / ((rsExpVary.Fields("RoughCts").Value / rsExpVary.Fields("ExpPcs").Value) * intBalPcs), 2)
                                Else
                                    vCost = 0
                                End If
                                dblMaxValue = Math.Round(dblPerStonePriceSales * 1.3 * intBalPcs, 2)
                            Else
                                vNFE = Math.Round(dblAvgCost * ((rsExpVary.Fields("RoughCts").Value / rsExpVary.Fields("RoughPcs").Value) * intBalPcs), 2)

                                vSubTotal = Math.Round(vLabour + vNFE + dblExtLabour, 2)
                                If rsExpVary.Fields("ExpCts").Value <> 0 Then
                                    vCost = Math.Round(vSubTotal / ((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * intBalPcs), 2)
                                Else
                                    vCost = 0
                                End If
                                dblMaxValue = Math.Round(dblBaseCost * rsExpVary.Fields("RoughCts").Value, 2)
                            End If

                            flxDetails.Rows.Add(cmbDepartment.Text, rsExpVary.Fields("Assortment").Value, sMFG, sImpDate, vImpInvNo, rsExpVary.Fields("Reference1").Value, rsExpVary.Fields("Reference2").Value,
                                                intBalPcs, Math.Round((rsExpVary.Fields("RoughCts").Value / rsExpVary.Fields("RoughPcs").Value) * intBalPcs, 3), dblAvgCost,
                                                dtpInvDate.Value, txtExportNo.Text, intBalPcs, Math.Round((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * intBalPcs, 3),
                                                rsExpVary.Fields("StCt").Value, rsExpVary.Fields("Charges").Value, rsExpVary.Fields("Yield").Value, vLabour, vNFE, vCost, vSubTotal, "0", "0",
                                                vMasterPcs, vMasterCts, vSendPcs, vSendCts, sClientID, sOrderRef, sOrderSide, sOrderSubject, strOrigAssort, sBOINo, dblMixID, Math.Round(dblBaseCost, 2),
                                                Math.Round(dblCurCost, 2), strSupParcelNo, rsExpVary.Fields("NLineNo").Value, dblNiruOrderNo, dblOrderItem, strCommande, strAssortBox, "", "", "0",
                                                dblLotNo)

                            flxDetails.Item(56, flxDetails.Rows.Count - 1).Value = dblExtLabour
                            flxDetails.Item(57, flxDetails.Rows.Count - 1).Value = dblMaxValue
                            flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = dblAvgCost

                            intBalPcs = 0
                            dblBalCts = 0
                        Else
                            sMFG = rsComSql.Fields("SupplierRefNo").Value
                            sBOINo = rsComSql.Fields("BOINo").Value
                            sImpDate = rsComSql.Fields("InvoiceDate").Value
                            vImpInvNo = rsComSql.Fields("CompanyRefNo").Value
                            If Mid(rsExpVary("Assortment").Value, 1, 2) = "VM" Or Mid(rsExpVary("Assortment").Value, 1, 2) = "VP" Then
                                strOrigAssort = "VPCU"
                            Else
                                strOrigAssort = "APCU"
                            End If
                            vMasterPcs = vMasterPcs + rsComSql.Fields("ActPcs").Value
                            vMasterCts = vMasterCts + rsComSql.Fields("ActCts").Value
                            vMasterCts = Math.Round(vMasterCts, 2)
                            dblBaseCost = rsExpVary.Fields("BasePrice").Value
                            dblCurCost = rsExpVary.Fields("AdjPrice").Value
                            dblAdjCost = rsExpVary.Fields("AdjPrice").Value
                            strSupParcelNo = rsComSql.Fields("SupParcelNo").Value
                            strOrigAssort = rsExpVary.Fields("OrigAssort").Value
                            dblLotNo = rsComSql.Fields("LotNo").Value
                            dblAvgCost = Math.Round(rsExpVary.Fields("AdjPrice").Value, 2)

                            dblMixID = 0
                            strAssortBox = ""
                            AdoCN.Execute("INSERT INTO tblCostingTemp(Assortment,Pcs,Cts) VALUES('" & rsComSql.Fields("AssortmentNo").Value & "'," & rsComSql.Fields("TrfPcs").Value & "," & Math.Round(rsComSql.Fields("TrfCts").Value, 3) & ")")

                            dblExtLabour = 0
                            vLabour = Math.Round(CDbl((rsComSql.Fields("TrfPcs").Value - intIssPcs) * rsExpVary.Fields("Charges").Value), 2)

                            If Mid(rsExpVary("Assortment").Value, 1, 1) = "S" Then
                                dblPerStonePrice = 0
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & rsExpVary("Assortment").Value & "'", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount Then
                                    If rsComSql_1.Fields("AvgStonePrice").Value <> 0 Then
                                        dblPerStonePrice = rsComSql_1.Fields("AvgStonePrice").Value
                                    End If
                                End If
                                rsComSql_1 = Nothing

                                If dblPerStonePrice <> 0 Then
                                    vNFE = Math.Round(dblPerStonePrice * (rsComSql.Fields("TrfPcs").Value - intIssPcs), 2)
                                    dblBaseCost = Math.Round(vNFE / ((rsExpVary.Fields("RoughCts").Value / rsExpVary.Fields("RoughPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs)), 2)

                                    vNFE = Math.Round(dblAvgCost * rsExpVary.Fields("RoughCts").Value, 2)
                                    dblCurCost = dblAvgCost
                                Else
                                    vNFE = Math.Round(dblAvgCost * (rsExpVary.Fields("RoughCts").Value / rsExpVary.Fields("RoughPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs), 2)
                                End If

                                vSubTotal = Math.Round(vLabour + vNFE + dblExtLabour, 2)
                                vCost = Math.Round(vSubTotal / ((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * intBalPcs), 2)

                                dblMaxValue = Math.Round(dblPerStonePriceSales * 1.3 * (rsComSql.Fields("TrfPcs").Value - intIssPcs), 2)
                            Else
                                vNFE = Math.Round(dblAvgCost * ((rsExpVary.Fields("RoughCts").Value / rsExpVary.Fields("RoughPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs)), 2)

                                vSubTotal = Math.Round(vLabour + vNFE + dblExtLabour, 2)
                                vCost = Math.Round(vSubTotal / ((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs)), 2)
                                dblMaxValue = Math.Round(dblBaseCost * rsExpVary.Fields("RoughCts").Value, 2)
                            End If

                            flxDetails.Rows.Add(cmbDepartment.Text, rsExpVary.Fields("Assortment").Value, sMFG, sImpDate, vImpInvNo, rsExpVary.Fields("Reference1").Value, rsExpVary.Fields("Reference2").Value,
                                                rsComSql.Fields("TrfPcs").Value - intIssPcs, Math.Round((rsExpVary.Fields("RoughCts").Value / rsExpVary.Fields("RoughPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs), 3), dblAvgCost,
                                                dtpInvDate.Value, txtExportNo.Text, rsComSql.Fields("TrfPcs").Value - intIssPcs, Math.Round((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs), 3),
                                                rsExpVary.Fields("StCt").Value, rsExpVary.Fields("Charges").Value, rsExpVary.Fields("Yield").Value, vLabour, vNFE, vCost, vSubTotal, "0", "0",
                                                vMasterPcs, vMasterCts, vSendPcs, vSendCts, sClientID, sOrderRef, sOrderSide, sOrderSubject, strOrigAssort, sBOINo, dblMixID, Math.Round(dblBaseCost, 2),
                                                Math.Round(dblCurCost, 2), strSupParcelNo, rsExpVary.Fields("NLineNo").Value, dblNiruOrderNo, dblOrderItem, strCommande, strAssortBox, "", "", "0",
                                                dblLotNo)

                            flxDetails.Item(56, flxDetails.Rows.Count - 1).Value = dblExtLabour
                            flxDetails.Item(57, flxDetails.Rows.Count - 1).Value = dblMaxValue
                            flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = dblAvgCost

                            intBalPcs = intBalPcs - (rsComSql.Fields("TrfPcs").Value - intIssPcs)
                            dblBalCts = dblBalCts - ((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs))
                            dblBalCts = Math.Round(dblBalCts, 3)
                        End If
                    End If
                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing

            rsExpVary.MoveNext()
            vLabour = 0
            vCurrPcs = 0
            vExpPcs = 0
            vMasterCts = 0
            vMasterPcs = 0
        Loop
        rsExpVary = Nothing
    End Sub

    Private Sub Load_GradingPCU_NRecords(ByVal strReference As String)
        Dim rsExpVary As New ADODB.Recordset
        Dim rstImpInfo As New ADODB.Recordset
        Dim rstExpInfo As New ADODB.Recordset
        Dim rstCheckAssort As New ADODB.Recordset
        Dim rstOrder As New ADODB.Recordset
        Dim vLabour, vNFE, vCost, vSubTotal As Double
        Dim sMFG, sImpDate, sBOINo As String
        Dim vImpInvNo As Double
        Dim vExpPcs, vCurrPcs As Integer
        Dim bSatisfied As Boolean
        Dim vMasterPcs As Long
        Dim vMasterCts As Double
        Dim vSendPcs As Double
        Dim vSendCts As Double
        Dim vItmCost As Double

        Dim sClientID, sOrderSubject, sOrderRef As String
        Dim sOrderSide As String

        Dim intOutPcs As Integer
        Dim dblOutCts As Double
        Dim intBalPcs As Integer
        Dim dblBalCts As Double
        Dim strOrigAssort As String
        Dim dblBaseCost As Double
        Dim dblAdjCost As Double
        Dim dblCurCost As Double
        Dim strSupParcelNo As String

        Dim dblMixID As Double

        Dim strCommande As String
        Dim strAssortBox As String

        Dim strNiruOrder As String
        Dim dblOrderItem As Double
        Dim intIssPcs As Integer
        Dim dblIssCts As Double

        Dim intGrCount As Integer
        Dim strLotID As String
        Dim strCategory As String
        Dim sSaleRate As Double
        Dim dblImpPrice As Double
        Dim dblMaxCost As Double
        Dim dblMaxValue As Double
        Dim strMaxType As String

        bSatisfied = False
        rsExpVary = New ADODB.Recordset
        mStrSQL = "SELECT Assortment,Price,Reference1,Reference2,ExpPcs,ExpCts,StCt,Charges,Request,RoughPcs,RoughCts,Yield,Status,OrderRef,OrigAssort,InID,NLineNo,BasePrice,AdjPrice,OrderSide " & _
                  "FROM dbo.tblExportVarification " & _
                  "WHERE (Status = 'A') AND Reference1 = '" & strReference & "' AND Department = 'GradingPCU_N' " & _
                  "ORDER BY Assortment"
        rsExpVary.Open(mStrSQL, AdoCN, 1, 1)

        Do While Not rsExpVary.EOF
            rstExpInfo = New ADODB.Recordset
            mStrSQL = "SELECT Assortment, SUM(RoughPcs) AS SendPcs, SUM(RoughCts) AS SendCts " & _
                      "FROM tblCosting GROUP BY Assortment HAVING Assortment = '" & rsExpVary("Assortment").Value & "'"
            rstExpInfo.Open(mStrSQL, AdoCN, 1, 1)
            If Not rstExpInfo.EOF Then
                vSendPcs = rstExpInfo("SendPcs").Value
                vSendCts = rstExpInfo("SendCts").Value
            End If
            rstExpInfo = Nothing

            intOutPcs = 0
            dblOutCts = 0
            sOrderRef = rsExpVary.Fields("OrderRef").Value
            intBalPcs = rsExpVary.Fields("ExpPcs").Value
            dblBalCts = Math.Round(rsExpVary.Fields("ExpCts").Value, 3)
            sClientID = ""
            sOrderSide = rsExpVary.Fields("OrderSide").Value
            sOrderSubject = ""
            vMasterPcs = 0
            vMasterCts = 0
            dblBaseCost = 0
            dblAdjCost = 0
            dblCurCost = 0
            strSupParcelNo = ""
            dblBaseCost = rsExpVary.Fields("BasePrice").Value
            dblAdjCost = rsExpVary.Fields("AdjPrice").Value
            sSaleRate = 0
            sMFG = ""
            sImpDate = ""
            sBOINo = ""
            strAssortBox = ""
            strCategory = ""
            strLotID = ""
            strNiruOrder = ""
            dblOrderItem = 0
            strCommande = ""
            dblImpPrice = 0
            dblMaxCost = 0
            dblMaxValue = 0
            strMaxType = "C"

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT OrderNo, NorderNo, OrderItem, COMMANDE, Subject, SalesRate, Niruref FROM dbo.tblNoneOrders WHERE OrderNo = '" & rsExpVary("Reference1").Value & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                strNiruOrder = Replace(rsComSql.Fields("NorderNo").Value, vbTab, "")
                dblOrderItem = rsComSql.Fields("OrderItem").Value
                strCommande = rsComSql.Fields("COMMANDE").Value
                sOrderSubject = rsComSql.Fields("Subject").Value
                sClientID = rsComSql.Fields("Niruref").Value
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT OutPrice, MaxType FROM dbo.tblNoneOrdersDtls WHERE OrderNo = '" & rsExpVary("Reference1").Value & "' AND RefNo = '" & sOrderRef & "' AND Side = '" & sOrderSide & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                dblMaxCost = rsComSql.Fields("OutPrice").Value
                strMaxType = rsComSql.Fields("MaxType").Value
            End If
            rsComSql = Nothing

            rstCheckAssort = New ADODB.Recordset
            mStrSQL = "SELECT ParcelType FROM tblImport WHERE (SupParcelNo = '" & rsExpVary("Reference2").Value & "')"
            rstCheckAssort.Open(mStrSQL, AdoCN, 1, 1)
            If Not rstCheckAssort.EOF Then
                rstImpInfo = New ADODB.Recordset
                mStrSQL = "SELECT SupplierRefNo,CompanyRefNo,InvoiceDate,AssortmentNo,ACTPcs,ACtCts,RemPcs,RemCts,BOINo,SupParcelNo,DclParcelNo,ItemCost,LotNo,Category,ImpPrice " & _
                          "FROM tblImport WHERE (SupParcelNo = '" & rsExpVary("Reference2").Value & "') " & _
                          "ORDER BY InvoiceDate"
                rstImpInfo.Open(mStrSQL, AdoCN, 1, 1)

                If Not rstImpInfo.EOF Then
                    vMasterPcs = vMasterPcs + rstImpInfo("ACTPcs").Value
                    vMasterCts = vMasterCts + rstImpInfo("ACTCts").Value
                    sMFG = rstImpInfo("SupplierRefNo").Value
                    sBOINo = rstImpInfo("BOINo").Value
                    sImpDate = rstImpInfo("InvoiceDate").Value
                    vImpInvNo = rstImpInfo("CompanyRefNo").Value
                    vItmCost = rstImpInfo("ItemCost").Value
                    strSupParcelNo = rstImpInfo("SupParcelNo").Value
                    strLotID = rstImpInfo("LotNo").Value
                    strCategory = rstImpInfo("Category").Value
                    dblImpPrice = rstImpInfo("ImpPrice").Value
                End If
                rstImpInfo = Nothing

                'rsComSql = New ADODB.Recordset
                'rsComSql.Open("SELECT * FROM tblGrading_SizingList WHERE NAME = '" & rsExpVary("Assortment").Value & "'", AdoCN, 1, 1)
                'If rsComSql.RecordCount Then
                '    vItmCost = rsComSql("Price").Value
                'End If
                'rsComSql = Nothing

            Else
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblImport WHERE (DCLParcelNo = '" & rsExpVary("Reference2").Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    vMasterPcs = vMasterPcs + rsComSql("ACTPcs").Value
                    vMasterCts = vMasterCts + rsComSql("ACTCts").Value
                    sMFG = rsComSql("SupplierRefNo").Value
                    sBOINo = rsComSql("BOINo").Value
                    sImpDate = rsComSql("InvoiceDate").Value
                    vImpInvNo = rsComSql("CompanyRefNo").Value
                    vItmCost = rsComSql("ItemCost").Value
                    strSupParcelNo = rsComSql("SupParcelNo").Value
                    strLotID = rsComSql("LotNo").Value
                    strCategory = rsComSql("Category").Value
                    dblImpPrice = rsComSql("ImpPrice").Value

                    'rsComSql_1 = New ADODB.Recordset
                    'rsComSql_1.Open("SELECT * FROM tblGrading_SizingList WHERE NAME = '" & rsExpVary("Assortment").Value & "'", AdoCN, 1, 1)
                    'If rsComSql_1.RecordCount Then
                    '    vItmCost = rsComSql_1("Price").Value
                    'End If
                    'rsComSql_1 = Nothing

                Else
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblDep_Trf WHERE (DCLParcelNo = '" & rsExpVary("Reference2").Value & "') AND (LEFT(DCLParcelNo, 3) <> 'TRF') AND (SupplierRefNo <> '0')", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        strSupParcelNo = rsComSql("SupParcelNo").Value

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM tblImport WHERE (SupParcelNo = '" & strSupParcelNo & "')", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            vMasterPcs = vMasterPcs + rsComSql_1("ACTPcs").Value
                            vMasterCts = vMasterCts + rsComSql_1("ACTCts").Value
                            sMFG = rsComSql_1("SupplierRefNo").Value
                            sBOINo = rsComSql_1("BOINo").Value
                            sImpDate = rsComSql_1("InvoiceDate").Value
                            vImpInvNo = rsComSql_1("CompanyRefNo").Value
                            vItmCost = rsComSql_1("ItemCost").Value
                            strSupParcelNo = rsComSql_1("SupParcelNo").Value
                            strLotID = rsComSql_1("LotNo").Value
                            strCategory = rsComSql_1("Category").Value
                            dblImpPrice = rsComSql_1("ImpPrice").Value
                        End If
                        rsComSql_1 = Nothing

                    Else
                        rsComSql = New ADODB.Recordset
                        If Mid(rsExpVary("Assortment").Value, 1, 1) = "B" Or Mid(rsExpVary("Assortment").Value, 1, 1) = "C" Or Mid(rsExpVary("Assortment").Value, 1, 2) = "VE" Then
                            rsComSql.Open("SELECT  TOP (100) PERCENT RecordID, Department, SystemRefNo, SupplierRefNo, CompanyRefNo, BOINo, InvoiceDate, RecievedDate, SupplierCode, ParcelType, " & _
                                            "AssortmentNo, SupParcelNo, DCLParcelNo, INVPcs, INVCts, ACTPcs, ACtCts, ItemSize, Charges, ItemCost, RemPcs, RemCts, Status, DoneBy, ModifyBy, SysDateTime, " & _
                                            "ImportNo, LotNo, Article, Remarks, Category, HardCost, CompCode, ItemName, Urgent, NewAssort, LocalInst, PlanFinDate, RghFinDate, SelectCost, " & _
                                            "PolPcs AS TrfPcs, PolCts AS TrfCts, ImpPrice FROM tblImport " & _
                                          "WHERE PolPcs > 0 ORDER BY SysDateTime", AdoCN, 1, 1)
                        Else
                            rsComSql.Open("SELECT  TOP (100) PERCENT RecordID, Department, SystemRefNo, SupplierRefNo, CompanyRefNo, BOINo, InvoiceDate, RecievedDate, SupplierCode, ParcelType, " & _
                                            "AssortmentNo, SupParcelNo, DCLParcelNo, INVPcs, INVCts, ACTPcs, ACtCts, ItemSize, Charges, ItemCost, RemPcs, RemCts, Status, DoneBy, ModifyBy, SysDateTime, " & _
                                            "ImportNo, TrfPcs, TrfCts, LotNo, Article, Remarks, Category, HardCost, CompCode, ItemName, Urgent, NewAssort, LocalInst, PlanFinDate, RghFinDate, SelectCost, " & _
                                            "PolPcs, PolCts, ImpPrice FROM tblImport " & _
                                          "WHERE TrfPcs > 0 ORDER BY SysDateTime", AdoCN, 1, 1)
                        End If
                        If rsComSql.RecordCount Then
                            rsComSql.MoveFirst()
                            While Not rsComSql.EOF And intBalPcs > 0
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT SUM(Pcs) AS Pcs,ROUND(SUM(Cts), 2) AS Cts FROM tblCostingTemp WHERE Assortment = '" & rsComSql.Fields("AssortmentNo").Value & "'", AdoCN, 1, 1)
                                If Not IsDBNull(rsComSql_1("Pcs").Value) Then
                                    intIssPcs = rsComSql_1("Pcs").Value
                                    dblIssCts = rsComSql_1("Cts").Value
                                Else
                                    intIssPcs = 0
                                    dblIssCts = 0
                                End If

                                If rsComSql("TrfPcs").Value - intIssPcs > 0 Then
                                    If rsComSql("TrfPcs").Value - intIssPcs >= intBalPcs Then

                                        sMFG = rsComSql("SupplierRefNo").Value
                                        sBOINo = rsComSql("BOINo").Value
                                        sImpDate = rsComSql("InvoiceDate").Value
                                        vImpInvNo = rsComSql("CompanyRefNo").Value
                                        dblImpPrice = rsComSql("ImpPrice").Value
                                        If Mid(rsExpVary("Assortment").Value, 1, 1) = "B" Or Mid(rsExpVary("Assortment").Value, 1, 1) = "C" Or Mid(rsExpVary("Assortment").Value, 1, 2) = "VE" Then
                                            strOrigAssort = rsExpVary("Assortment").Value
                                        Else
                                            If Mid(rsExpVary("Assortment").Value, 1, 2) = "VM" Or Mid(rsExpVary("Assortment").Value, 1, 2) = "VP" Then
                                                strOrigAssort = "VPCU"
                                            Else
                                                strOrigAssort = "APCU"
                                            End If
                                        End If
                                        vMasterPcs = vMasterPcs + rsComSql("ActPcs").Value
                                        vMasterCts = Math.Round(vMasterCts + rsComSql("ActCts").Value, 2)
                                        dblBaseCost = rsExpVary("BasePrice").Value
                                        dblCurCost = rsExpVary("AdjPrice").Value
                                        dblAdjCost = rsExpVary("Price").Value
                                        strSupParcelNo = rsComSql("SupParcelNo").Value
                                        strLotID = rsComSql("LotNo").Value
                                        strCategory = rsComSql("Category").Value

                                        dblMixID = 0
                                        strAssortBox = rsComSql("AssortmentNo").Value
                                        AdoCN.Execute("INSERT INTO tblCostingTemp(Assortment,Pcs,Cts) VALUES('" & strAssortBox & "'," & intBalPcs & "," & Math.Round(dblBalCts, 3) & ")")

                                        dblExtLabour = 0
                                        vLabour = PFGetLabourCharges(rsExpVary("Request").Value, intBalPcs, Math.Round((rsExpVary("RoughCts").Value / rsExpVary("RoughPcs").Value) * intBalPcs, 3))
                                        vNFE = Math.Round(dblAdjCost * (rsExpVary("RoughCts").Value / rsExpVary("RoughPcs").Value) * intBalPcs, 2)

                                        vSubTotal = Math.Round(vLabour + vNFE + dblExtLabour, 2)
                                        vCost = Math.Round(vSubTotal / ((rsExpVary("ExpCts").Value / rsExpVary("ExpPcs").Value) * intBalPcs), 2)
                                        If dblMaxCost > 0 Then
                                            If strMaxType = "P" Then
                                                dblMaxValue = dblMaxCost * rsExpVary("ExpPcs").Value
                                            Else
                                                dblMaxValue = dblMaxCost * ((rsExpVary("RoughCts").Value / rsExpVary("RoughPcs").Value) * intBalPcs)
                                            End If
                                        Else
                                            dblMaxValue = vSubTotal
                                        End If

                                        flxDetails.Rows.Add(cmbDepartment.Text, rsExpVary.Fields("Assortment").Value, sMFG, sImpDate, vImpInvNo, rsExpVary.Fields("Reference1").Value, rsExpVary.Fields("Reference2").Value,
                                                        intBalPcs, Math.Round((rsExpVary.Fields("RoughCts").Value / rsExpVary.Fields("RoughPcs").Value) * intBalPcs, 3), rsExpVary.Fields("Price").Value,
                                                        dtpInvDate.Value, txtExportNo.Text, intBalPcs, Math.Round((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * intBalPcs, 3),
                                                        rsExpVary.Fields("StCt").Value, rsExpVary.Fields("Charges").Value, rsExpVary.Fields("Yield").Value, vLabour, vNFE, vCost, vSubTotal, "0", "0",
                                                        vMasterPcs, vMasterCts, vSendPcs, vSendCts, sClientID, sOrderRef, sOrderSide, sOrderSubject, strOrigAssort, sBOINo, dblMixID, Math.Round(dblBaseCost, 2),
                                                        Math.Round(dblCurCost, 2), strSupParcelNo, rsExpVary.Fields("NLineNo").Value, strNiruOrder, dblOrderItem, strCommande, strAssortBox, "", "", intBalPcs * intGrCount * 5,
                                                        strLotID, "0", "0", strCategory, sSaleRate)

                                        flxDetails.Item(56, flxDetails.Rows.Count - 1).Value = dblExtLabour
                                        flxDetails.Item(57, flxDetails.Rows.Count - 1).Value = dblMaxValue
                                        flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = Math.Round((rsExpVary.Fields("Price").Value / vItmCost) * dblImpPrice, 2)

                                        intBalPcs = 0
                                        dblBalCts = 0
                                    Else
                                        sMFG = rsComSql("SupplierRefNo").Value
                                        sBOINo = rsComSql("BOINo").Value
                                        sImpDate = rsComSql("InvoiceDate").Value
                                        vImpInvNo = rsComSql("CompanyRefNo").Value
                                        If Mid(rsExpVary("Assortment").Value, 1, 1) = "B" Or Mid(rsExpVary("Assortment").Value, 1, 1) = "C" Or Mid(rsExpVary("Assortment").Value, 1, 2) = "VE" Then
                                            strOrigAssort = rsExpVary("Assortment").Value
                                        Else
                                            If Mid(rsExpVary("Assortment").Value, 1, 2) = "VM" Or Mid(rsExpVary("Assortment").Value, 1, 2) = "VP" Then
                                                strOrigAssort = "VPCU"
                                            Else
                                                strOrigAssort = "APCU"
                                            End If
                                        End If
                                        vMasterPcs = vMasterPcs + rsComSql("ActPcs").Value
                                        vMasterCts = Math.Round(vMasterCts + rsComSql("ActCts").Value, 2)
                                        dblBaseCost = rsExpVary("BasePrice").Value
                                        dblCurCost = rsExpVary("AdjPrice").Value
                                        dblAdjCost = rsExpVary("Price").Value
                                        strSupParcelNo = rsComSql("SupParcelNo").Value
                                        strLotID = rsComSql("LotNo").Value
                                        strCategory = rsComSql("Category").Value
                                        dblImpPrice = rsComSql("ImpPrice").Value

                                        dblMixID = 0
                                        strAssortBox = rsComSql("AssortmentNo").Value
                                        AdoCN.Execute("INSERT INTO tblCostingTemp(Assortment,Pcs,Cts) VALUES('" & strAssortBox & "'," & rsComSql("TrfPcs").Value & "," & Math.Round(rsComSql("TrfCts").Value, 3) & ")")

                                        dblExtLabour = 0
                                        vLabour = PFGetLabourCharges(rsExpVary("Request").Value, rsComSql("TrfPcs").Value - intIssPcs, Math.Round((rsExpVary("RoughCts").Value / rsExpVary("RoughPcs").Value) * (rsComSql("TrfPcs").Value - intIssPcs), 3))
                                        vLabour = vLabour + ((rsComSql("TrfPcs").Value - intIssPcs) * intGrCount * 5)
                                        vNFE = Math.Round(dblAdjCost * (rsExpVary("RoughCts").Value / rsExpVary("RoughPcs").Value) * (rsComSql("TrfPcs").Value - intIssPcs), 2)

                                        vSubTotal = Math.Round(vLabour + vNFE + dblExtLabour, 2)
                                        vCost = Math.Round(vSubTotal / ((rsExpVary("ExpCts").Value / rsExpVary("ExpPcs").Value) * (rsComSql("TrfPcs").Value - intIssPcs)), 2)

                                        If dblMaxCost > 0 Then
                                            dblMaxValue = dblMaxCost * rsExpVary("ExpPcs").Value
                                        Else
                                            dblMaxValue = vSubTotal
                                        End If

                                        flxDetails.Rows.Add(cmbDepartment.Text, rsExpVary.Fields("Assortment").Value, sMFG, sImpDate, vImpInvNo, rsExpVary.Fields("Reference1").Value, rsExpVary.Fields("Reference2").Value,
                                                        rsComSql.Fields("TrfPcs").Value - intIssPcs, Math.Round((rsExpVary.Fields("RoughCts").Value / rsExpVary.Fields("RoughPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs), 3), rsExpVary.Fields("Price").Value,
                                                        dtpInvDate.Value, txtExportNo.Text, rsComSql.Fields("TrfPcs").Value - intIssPcs, Math.Round((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs), 3),
                                                        rsExpVary.Fields("StCt").Value, rsExpVary.Fields("Charges").Value, rsExpVary.Fields("Yield").Value, vLabour, vNFE, vCost, vSubTotal, "0", "0",
                                                        vMasterPcs, vMasterCts, vSendPcs, vSendCts, sClientID, sOrderRef, sOrderSide, sOrderSubject, strOrigAssort, sBOINo, dblMixID, Math.Round(dblBaseCost, 2),
                                                        Math.Round(dblCurCost, 2), strSupParcelNo, rsExpVary.Fields("NLineNo").Value, strNiruOrder, dblOrderItem, strCommande, strAssortBox, "", "", intBalPcs * intGrCount * 5,
                                                        strLotID, "0", "0", strCategory, sSaleRate)

                                        flxDetails.Item(56, flxDetails.Rows.Count - 1).Value = dblExtLabour
                                        flxDetails.Item(57, flxDetails.Rows.Count - 1).Value = dblMaxValue
                                        flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = Math.Round((rsExpVary.Fields("Price").Value / vItmCost) * dblImpPrice, 2)

                                        intBalPcs = intBalPcs - (rsComSql.Fields("TrfPcs").Value - intIssPcs)
                                        dblBalCts = dblBalCts - ((rsExpVary.Fields("ExpCts").Value / rsExpVary.Fields("ExpPcs").Value) * (rsComSql.Fields("TrfPcs").Value - intIssPcs))
                                        dblBalCts = Math.Round(dblBalCts, 3)

                                    End If
                                End If
                                rsComSql.MoveNext()
                            End While
                        End If

                        GoTo NextRecord
                    End If
                End If
            End If
            rstCheckAssort = Nothing

            strOrigAssort = rsExpVary("OrigAssort").Value

Show_Grid:
            dblExtLabour = 0
            vLabour = PFGetLabourCharges(rsExpVary("Request").Value, rsExpVary("ExpPcs").Value, rsExpVary.Fields("RoughCts").Value)
            vNFE = Math.Round(rsExpVary.Fields("Price").Value * rsExpVary.Fields("RoughCts").Value, 2)
            vSubTotal = Math.Round(vLabour + vNFE + dblExtLabour, 2)
            vCost = Math.Round(vSubTotal / rsExpVary.Fields("ExpCts").Value, 2)

            If dblMaxCost > 0 Then
                If strMaxType = "C" Then
                    dblMaxValue = dblMaxCost * rsExpVary.Fields("ExpCts").Value
                Else
                    dblMaxValue = dblMaxCost * rsExpVary.Fields("ExpPcs").Value
                End If
            Else
                dblMaxValue = vSubTotal
            End If

            flxDetails.Rows.Add(cmbDepartment.Text, rsExpVary.Fields("Assortment").Value, sMFG, sImpDate, vImpInvNo, rsExpVary.Fields("Reference1").Value, rsExpVary.Fields("Reference2").Value,
                                rsExpVary.Fields("RoughPcs").Value, Math.Round(rsExpVary.Fields("RoughCts").Value, 3), rsExpVary.Fields("Price").Value,
                                dtpInvDate.Value, txtExportNo.Text, rsExpVary("ExpPcs").Value, Math.Round(rsExpVary.Fields("ExpCts").Value, 3),
                                rsExpVary.Fields("StCt").Value, rsExpVary.Fields("Charges").Value, rsExpVary.Fields("Yield").Value, vLabour, vNFE, vCost, vSubTotal, "0", "0",
                                vMasterPcs, vMasterCts, vSendPcs, vSendCts, sClientID, sOrderRef, sOrderSide, sOrderSubject, strOrigAssort, sBOINo, dblMixID, Math.Round(dblBaseCost, 2),
                                Math.Round(dblCurCost, 2), strSupParcelNo, rsExpVary.Fields("NLineNo").Value, strNiruOrder, dblOrderItem, strCommande, strAssortBox, "", "", intBalPcs * intGrCount * 5,
                                strLotID, "0", "0", strCategory, sSaleRate)

            flxDetails.Item(56, flxDetails.Rows.Count - 1).Value = dblExtLabour
            flxDetails.Item(57, flxDetails.Rows.Count - 1).Value = Math.Round(dblMaxValue, 2)
            flxDetails.Item(58, flxDetails.Rows.Count - 1).Value = Math.Round((rsExpVary.Fields("Price").Value / vItmCost) * dblImpPrice, 2)

NextRecord:

            rsExpVary.MoveNext()
            vLabour = 0
            vCurrPcs = 0
            vExpPcs = 0
            vMasterCts = 0
            vMasterPcs = 0
        Loop
        rsExpVary = Nothing
    End Sub

    Private Sub Save()
        Dim iRow As Integer
        Dim rstExpVary As New ADODB.Recordset
        Dim dtpImportDate As Date

        If txtPack.Text = "" Then MsgBox("Invalid Packing List No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtType.Text = "" Then MsgBox("Invalid Pack Code", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtCategory.Text = "" Then MsgBox("Invalid Category", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        ExpProgress.Minimum = 0
        ExpProgress.Visible = True
        ExpProgress.Maximum = flxDetails.Rows.Count

        For iRow = 0 To flxDetails.Rows.Count - 1

            If Len(flxDetails.Item(3, iRow).Value) > 0 Then
                dtpImportDate = Format(CDate(flxDetails.Item(3, iRow).Value), "MM/dd/yyyy")
            Else
                dtpImportDate = Format(Date.Now, "MM/dd/yyyy")
            End If
            If Len(flxDetails.Item(44, iRow).Value) = 0 Then
                flxDetails.Item(44, iRow).Value = "0"
            End If
            If Len(flxDetails.Item(47, iRow).Value) = 0 Then
                flxDetails.Item(47, iRow).Value = "0"
            End If
            If Len(flxDetails.Item(49, iRow).Value) = 0 Then
                flxDetails.Item(49, iRow).Value = "0"
            End If

            If Len(flxDetails.Item(53, iRow).Value) = 0 Then
                flxDetails.Item(53, iRow).Value = "0"
            End If

            If Len(flxDetails.Item(54, iRow).Value) = 0 Then
                flxDetails.Item(54, iRow).Value = "0"
            End If

            If Len(flxDetails.Item(56, iRow).Value) = 0 Then
                flxDetails.Item(56, iRow).Value = "0"
            End If

            If Len(flxDetails.Item(57, iRow).Value) = 0 Then
                flxDetails.Item(57, iRow).Value = "0"
            End If

            If Len(flxDetails.Item(58, iRow).Value) = 0 Then
                flxDetails.Item(58, iRow).Value = flxDetails.Item(9, iRow).Value
            End If

            AdoCN.Execute("INSERT INTO tblCosting(DateCreated,Department,Assortment,SupInvoiceNo,ImpDate,ImportNo,Reference1,Reference2,RoughPcs,RoughCts,Price,ExportDate,ExportNo,ExportPcs,ExportCts," & _
                            "StCts,vStone,Yield,Labour,NFEValue,Cost,Totals,BalancePcs,BalanceCts,MasterPcs,MasterCts,ShipedPcs,ShipedCts,ClientID,OrderRefrence,OrderSide,Subject,CostingFor,eConfirm,BOINo,Status," & _
                            "DoneBy,ModifyBy,InID,BaseCost,CurCost,SupParNo,NLineNo,NOrderNo,OrderItem,Commande,NiruParcel,Item,GrLabour,LotID,AssortValue,Category,PackingListNo,PackingType,SalesRate,RghLabour,AssLabour,LabourE,MaxValue,HardCost) " & _
                          "VALUES('" & Format(dtpInvDate.Value, "MM/dd/yyyy") & "','" & flxDetails.Item(0, iRow).Value & "','" & flxDetails.Item(1, iRow).Value & "','" & flxDetails.Item(2, iRow).Value & "'," & _
                            "'" & dtpImportDate & "'," & CDbl(flxDetails.Item(4, iRow).Value) & ",'" & flxDetails.Item(5, iRow).Value & "','" & flxDetails.Item(6, iRow).Value & "'," & _
                            "" & CDbl(flxDetails.Item(7, iRow).Value) & "," & CDbl(flxDetails.Item(8, iRow).Value) & "," & CDbl(flxDetails.Item(9, iRow).Value) & ",'" & Format(dtpInvDate.Value, "MM/dd/yyyy") & "'," & _
                            "" & CDbl(flxDetails.Item(11, iRow).Value) & "," & CDbl(flxDetails.Item(12, iRow).Value) & "," & CDbl(flxDetails.Item(13, iRow).Value) & "," & CDbl(flxDetails.Item(14, iRow).Value) & "," & CDbl(flxDetails.Item(15, iRow).Value) & "," & _
                            "" & CDbl(flxDetails.Item(16, iRow).Value) & "," & CDbl(flxDetails.Item(17, iRow).Value) & "," & CDbl(flxDetails.Item(18, iRow).Value) & "," & CDbl(flxDetails.Item(19, iRow).Value) & "," & CDbl(flxDetails.Item(20, iRow).Value) & "," & _
                            "" & CDbl(flxDetails.Item(21, iRow).Value) & "," & CDbl(flxDetails.Item(22, iRow).Value) & "," & CDbl(flxDetails.Item(23, iRow).Value) & "," & CDbl(flxDetails.Item(24, iRow).Value) & "," & CDbl(flxDetails.Item(25, iRow).Value) & "," & _
                            "" & CDbl(flxDetails.Item(26, iRow).Value) & ",'" & flxDetails.Item(27, iRow).Value & "','" & Replace(flxDetails.Item(28, iRow).Value, "'", "''") & "','" & flxDetails.Item(29, iRow).Value & "','" & flxDetails.Item(30, iRow).Value & "','" & flxDetails.Item(31, iRow).Value & "'," & _
                            "1,'" & flxDetails.Item(32, iRow).Value & "','A','" & PBUser_ID & "','" & PBUser_ID & "'," & flxDetails.Item(33, iRow).Value & "," & flxDetails.Item(34, iRow).Value & "," & flxDetails.Item(35, iRow).Value & ",'" & flxDetails.Item(36, iRow).Value & "'," & _
                            "'" & flxDetails.Item(37, iRow).Value & "','" & flxDetails.Item(38, iRow).Value & "','" & flxDetails.Item(39, iRow).Value & "','" & Replace(flxDetails.Item(40, iRow).Value, "'", "") & "','" & Replace(flxDetails.Item(41, iRow).Value, "'", "") & "'," & _
                            "'" & flxDetails.Item(43, iRow).Value & "'," & CDbl(flxDetails.Item(44, iRow).Value) & ",'" & flxDetails.Item(45, iRow).Value & "'," & flxDetails.Item(47, iRow).Value & ",'" & txtCategory.Text & "','" & CInt(txtPack.Text) & "','" & txtType.Text & "'," & CDbl(flxDetails.Item(49, iRow).Value) & "," & _
                            "" & CDbl(flxDetails.Item(53, iRow).Value) & "," & CDbl(flxDetails.Item(54, iRow).Value) & "," & CDbl(flxDetails.Item(56, iRow).Value) & "," & CDbl(flxDetails.Item(57, iRow).Value) & "," & CDbl(flxDetails.Item(58, iRow).Value) & ")")

            'Export Verification Status updation to E
            If flxDetails.Item(0, iRow).Value = "Colombo Niru" Then
                AdoCN.Execute("UPDATE tblExportVarification " & _
                              "SET Status = 'E' " & _
                              "WHERE Reference2 = '" & flxDetails.Item(6, iRow).Value & "' AND " & _
                                    "Status = 'A' AND Department = '" & flxDetails.Item(0, iRow).Value & "'")

            ElseIf flxDetails.Item(0, iRow).Value = "PolishBox" Or flxDetails.Item(0, iRow).Value = "PolishBoxTrf" Then
                AdoCN.Execute("UPDATE tblExportVarification " & _
                              "SET Status = 'E' " & _
                              "WHERE Reference1 = '" & flxDetails.Item(5, iRow).Value & "' AND " & _
                                    "Status = 'A' AND Department = '" & flxDetails.Item(0, iRow).Value & "'")

            ElseIf flxDetails.Item(0, iRow).Value = "SizeExports" Then
                AdoCN.Execute("UPDATE tblExportVarification " & _
                              "SET Status = 'E' " & _
                              "WHERE Assortment = '" & flxDetails.Item(1, iRow).Value & "' AND " & _
                                    "Reference1 = '" & flxDetails.Item(5, iRow).Value & "' AND " & _
                                    "Status = 'A' AND InID = '" & flxDetails.Item(33, iRow).Value & "' AND " & _
                                    "Department = '" & flxDetails.Item(0, iRow).Value & "'")

            Else
                AdoCN.Execute("UPDATE tblExportVarification " & _
                              "SET Status = 'E' " & _
                              "WHERE Assortment = '" & flxDetails.Item(1, iRow).Value & "' AND " & _
                                    "Reference1 = '" & flxDetails.Item(5, iRow).Value & "' AND " & _
                                    "Status = 'A' AND Department = '" & flxDetails.Item(0, iRow).Value & "'")
            End If

            'Parcel Completion - Complete to 1
            If flxDetails.Item(0, iRow).Value = "Rounds" And strRight(flxDetails.Item(6, iRow).Value, 1) <> "N" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT GrpParNo FROM tblParcel WHERE Depart = '" & flxDetails.Item(0, iRow).Value & "' AND ParcelNo = '" & flxDetails.Item(6, iRow).Value & "' AND Grp <> 'N'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    AdoCN.Execute("UPDATE tblParcel SET Complete = 1 WHERE Depart = '" & flxDetails.Item(0, iRow).Value & "' AND ParcelNo = '" & flxDetails.Item(6, iRow).Value & "' AND Grp <> 'N'")
                    AdoCN.Execute("UPDATE tblParcel SET Complete = 1 WHERE Depart = 'RoughBruting' AND ParcelNo = '" & flxDetails.Item(6, iRow).Value & "'")
                End If
                rsComSql = Nothing
            Else
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT GrpParNo FROM tblParcel WHERE Depart = '" & flxDetails.Item(0, iRow).Value & "' AND GrpParNo = '" & flxDetails.Item(6, iRow).Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    AdoCN.Execute("UPDATE tblParcel SET Complete = 1 WHERE Depart = '" & flxDetails.Item(0, iRow).Value & "' AND GrpParNo = '" & flxDetails.Item(6, iRow).Value & "'")
                    AdoCN.Execute("UPDATE tblParcel SET Complete = 1 WHERE Depart = 'RoughBruting' AND GrpParNo = '" & flxDetails.Item(6, iRow).Value & "'")
                End If
                rsComSql = Nothing
            End If

            If cmbDepartment.Text = "SizeExports" Or cmbDepartment.Text = "GradingPCU" Then
                If flxDetails.Item(31, iRow).Value = "APCU" Or flxDetails.Item(31, iRow).Value = "VPCU" Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT Department, SupplierRefNo FROM tblImport WHERE SupplierRefNo = '" & flxDetails.Item(2, iRow).Value & "' AND SupplierRefNo NOT LIKE 'LCL%'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If rsComSql("Department").Value = "PCU" Then
                            AdoCN.Execute("UPDATE tblImport SET RemPcs = RemPcs - " & CDbl(flxDetails.Item(7, iRow).Value) & ",RemCts = RemCts - " & CDbl(flxDetails.Item(8, iRow).Value) & ",TrfPcs = TrfPcs - " & CDbl(flxDetails.Item(7, iRow).Value) & ",TrfCts = TrfCts - " & CDbl(flxDetails.Item(8, iRow).Value) & " WHERE SupParcelNo = '" & flxDetails.Item(36, iRow).Value & "' AND SupplierRefNo NOT LIKE 'LCL%'")
                        Else
                            AdoCN.Execute("UPDATE tblImport SET TrfPcs = TrfPcs - " & CDbl(flxDetails.Item(7, iRow).Value) & ",TrfCts = TrfCts - " & CDbl(flxDetails.Item(8, iRow).Value) & " WHERE SupParcelNo = '" & flxDetails.Item(36, iRow).Value & "' AND SupplierRefNo NOT LIKE 'LCL%'")
                        End If
                    End If
                    rsComSql = Nothing
                Else
                    AdoCN.Execute("UPDATE tblImport SET RemPcs = RemPcs - " & CDbl(flxDetails.Item(7, iRow).Value) & ",RemCts = RemCts - " & CDbl(flxDetails.Item(8, iRow).Value) & " WHERE SupParcelNo = '" & flxDetails.Item(5, iRow).Value & "'")
                End If

            ElseIf cmbDepartment.Text = "GradingPCU_N" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SupplierRefNo, Category FROM tblImport WHERE SupplierRefNo = '" & flxDetails.Item(2, iRow).Value & "' AND SupplierRefNo NOT LIKE 'LCL%'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If rsComSql("Category").Value = "Purchased" Then
                        AdoCN.Execute("UPDATE tblImport SET PolPcs = PolPcs - " & CDbl(flxDetails.Item(7, iRow).Value) & ",PolCts = PolCts - " & CDbl(flxDetails.Item(8, iRow).Value) & " WHERE SupParcelNo = '" & flxDetails.Item(36, iRow).Value & "' AND SupplierRefNo NOT LIKE 'LCL%'")
                    Else
                        AdoCN.Execute("UPDATE tblImport SET RemPcs = RemPcs - " & CDbl(flxDetails.Item(7, iRow).Value) & ",RemCts = RemCts - " & CDbl(flxDetails.Item(8, iRow).Value) & " WHERE SupParcelNo = '" & flxDetails.Item(36, iRow).Value & "' AND SupplierRefNo NOT LIKE 'LCL%'")
                    End If
                End If
                rsComSql = Nothing

            ElseIf cmbDepartment.Text = "PolishBox" Or cmbDepartment.Text = "PolishBoxTrf" Then
                AdoCN.Execute("UPDATE tblImport SET PolPcs = PolPcs - " & CDbl(flxDetails.Item(7, iRow).Value) & ",PolCts = PolCts - " & CDbl(flxDetails.Item(8, iRow).Value) & " WHERE SupParcelNo = '" & flxDetails.Item(36, iRow).Value & "' AND SupplierRefNo NOT LIKE 'LCL%'")

            ElseIf cmbDepartment.Text = "Grading" Then
                AdoCN.Execute("UPDATE tblImport SET RemPcs = RemPcs - " & CDbl(flxDetails.Item(7, iRow).Value) & ",RemCts = RemCts - " & CDbl(flxDetails.Item(8, iRow).Value) & " WHERE SupParcelNo = '" & flxDetails.Item(5, iRow).Value & "'")

            ElseIf cmbDepartment.Text = "Mix" Or cmbDepartment.Text = "GradingMix" Or cmbDepartment.Text = "Exports" Or cmbDepartment.Text = "KIT Box" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT Department, SupplierRefNo FROM tblImport WHERE SupplierRefNo = '" & flxDetails.Item(2, iRow).Value & "' AND SupplierRefNo NOT LIKE 'LCL%'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If rsComSql("Department").Value = "PCU" Then
                        AdoCN.Execute("UPDATE tblImport SET RemPcs = RemPcs - " & CDbl(flxDetails.Item(7, iRow).Value) & ",RemCts = RemCts - " & CDbl(flxDetails.Item(8, iRow).Value) & ",TrfPcs = TrfPcs - " & CDbl(flxDetails.Item(7, iRow).Value) & ",TrfCts = TrfCts - " & CDbl(flxDetails.Item(8, iRow).Value) & " WHERE SupParcelNo = '" & flxDetails.Item(36, iRow).Value & "'")
                    Else
                        AdoCN.Execute("UPDATE tblImport SET TrfPcs = TrfPcs - " & CDbl(flxDetails.Item(7, iRow).Value) & ",TrfCts = TrfCts - " & CDbl(flxDetails.Item(8, iRow).Value) & " WHERE SupParcelNo = '" & flxDetails.Item(36, iRow).Value & "'")
                    End If
                End If
                rsComSql = Nothing

            End If

            ExpProgress.Value = iRow + 1
            Application.DoEvents()
        Next
        ExpProgress.Visible = False
        MsgBox("Successfully Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub Load_Client()
        cmbClient.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT NiruCust FROM tblNiruRef ORDER BY NiruCust", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            Do While Not rsComSql.EOF
                cmbClient.Items.Add(rsComSql.Fields("NiruCust").Value)
                rsComSql.MoveNext()
            Loop
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdSelectAll_Click(sender As Object, e As EventArgs) Handles cmdSelectAll.Click
        Dim rsSelectAll As New ADODB.Recordset

        If txtExportNo.Text = "" Then
            MsgBox("Please enter the Export No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            txtExportNo.Focus()
        End If
        If txtMargin.Text = "" Then
            MsgBox("Please enter the Margin", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            txtMargin.Focus()
        End If
        If IsNumeric(txtMargin.Text) = False Then
            MsgBox("Invalid Margin", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            txtMargin.Focus()
        End If

        If cmbDepartment.Text = "Mix" Or cmbDepartment.Text = "ProcessReject" Or cmbDepartment.Text = "MixRefer" Then
            flxDetails.Rows.Clear()
            AdoCN.Execute("DELETE FROM tblCostingTemp")

            ExpProgress.Visible = True
            ExpProgress.Value = 0

            If cmbClient.Text = "" Then
                rsSelectAll = New ADODB.Recordset
                rsSelectAll.Open("SELECT Reference1 FROM tblExportVarification WHERE Status = 'A' AND Department = '" & cmbDepartment.Text & "' GROUP BY Reference1 ORDER BY Reference1", AdoCN, 1, 1)
                If rsSelectAll.RecordCount Then
                    rsSelectAll.MoveFirst()
                    ExpProgress.Maximum = rsSelectAll.RecordCount
                    While Not rsSelectAll.EOF
                        If cmbDepartment.Text = "Mix" Or cmbDepartment.Text = "MixRefer" Then
                            Load_MixRecords(rsSelectAll.Fields("Reference1").Value)
                        ElseIf cmbDepartment.Text = "ProcessReject" Then
                            Load_ProcessRejectRecords(rsSelectAll.Fields("Reference1").Value)
                        End If

                        ExpProgress.Value = ExpProgress.Value + 1
                        rsSelectAll.MoveNext()
                    End While
                End If
                rsSelectAll = Nothing
            Else
                rsSelectAll = New ADODB.Recordset
                rsSelectAll.Open("SELECT TOP (100) PERCENT dbo.tblExportVarification.Reference1 " & _
                                 "FROM dbo.tblExportVarification INNER JOIN dbo.tblOrders ON dbo.tblExportVarification.Reference1 = dbo.tblOrders.OrderNo " & _
                                 "WHERE (dbo.tblExportVarification.Status = 'A') AND (dbo.tblExportVarification.Department = 'Mix') AND (dbo.tblOrders.Niruref = '" & cmbClient.Text & "') " & _
                                 "GROUP BY dbo.tblExportVarification.Reference1 " & _
                                 "ORDER BY dbo.tblExportVarification.Reference1", AdoCN, 1, 1)
                If rsSelectAll.RecordCount Then
                    rsSelectAll.MoveFirst()
                    ExpProgress.Maximum = rsSelectAll.RecordCount
                    While Not rsSelectAll.EOF
                        If cmbDepartment.Text = "Mix" Then
                            Load_MixRecords(rsSelectAll.Fields("Reference1").Value)
                        End If

                        ExpProgress.Value = ExpProgress.Value + 1
                        rsSelectAll.MoveNext()
                    End While
                End If
                rsSelectAll = Nothing
            End If
            

            ExpProgress.Value = 0
            ExpProgress.Visible = False

            txtPcs.Text = CalTotalPcs(flxDetails, 7)
            txtCts.Text = CalTotalCts(flxDetails, 8)
            txtTotPcs.Text = CalTotalPcs(flxDetails, 12)
            txtTotCts.Text = CalTotalCts(flxDetails, 13)
            txtLabour.Text = CalTotalCts(flxDetails, 17)
            txtGrLabour.Text = CalTotalCts(flxDetails, 44)
            txtTotValue.Text = CalTotalCts(flxDetails, 18)

        ElseIf cmbDepartment.Text = "SizeExports" Then
            flxDetails.Rows.Clear()

            ExpProgress.Visible = True
            ExpProgress.Value = 0

            rsSelectAll = New ADODB.Recordset
            rsSelectAll.Open("SELECT Reference1 FROM tblExportVarification WHERE Status = 'A' AND Department = '" & cmbDepartment.Text & "' GROUP BY Reference1 ORDER BY Reference1", AdoCN, 1, 1)
            If rsSelectAll.RecordCount Then
                rsSelectAll.MoveFirst()
                ExpProgress.Maximum = rsSelectAll.RecordCount
                While Not rsSelectAll.EOF
                    Load_SizeExportsRecords(rsSelectAll.Fields("Reference1").Value)

                    ExpProgress.Value = ExpProgress.Value + 1
                    rsSelectAll.MoveNext()
                End While
            End If
            rsSelectAll = Nothing

            ExpProgress.Value = 0
            ExpProgress.Visible = False

            txtPcs.Text = CalTotalPcs(flxDetails, 7)
            txtCts.Text = CalTotalCts(flxDetails, 8)
            txtTotPcs.Text = CalTotalPcs(flxDetails, 12)
            txtTotCts.Text = CalTotalCts(flxDetails, 13)
            txtLabour.Text = CalTotalCts(flxDetails, 17)
            txtGrLabour.Text = CalTotalCts(flxDetails, 44)
            txtTotValue.Text = CalTotalCts(flxDetails, 18)

        ElseIf cmbDepartment.Text = "Rounds" Then
            flxDetails.Rows.Clear()

            ExpProgress.Visible = True
            ExpProgress.Value = 0

            rsSelectAll = New ADODB.Recordset
            rsSelectAll.Open("SELECT Reference1 FROM tblExportVarification WHERE Status = 'A' AND Department = '" & cmbDepartment.Text & "' GROUP BY Reference1 ORDER BY Reference1", AdoCN, 1, 1)
            If rsSelectAll.RecordCount Then
                rsSelectAll.MoveFirst()
                ExpProgress.Maximum = rsSelectAll.RecordCount
                While Not rsSelectAll.EOF
                    If chkReturn.Checked = True Then
                        Load_RoundsRecordsReturn(rsSelectAll.Fields("Reference1").Value)
                    Else
                        Load_RoundsRecords(rsSelectAll.Fields("Reference1").Value)
                    End If

                    ExpProgress.Value = ExpProgress.Value + 1
                    rsSelectAll.MoveNext()
                End While
            End If
            rsSelectAll = Nothing

            ExpProgress.Value = 0
            ExpProgress.Visible = False

            txtPcs.Text = CalTotalPcs(flxDetails, 7)
            txtCts.Text = CalTotalCts(flxDetails, 8)
            txtTotPcs.Text = CalTotalPcs(flxDetails, 12)
            txtTotCts.Text = CalTotalCts(flxDetails, 13)
            txtLabour.Text = CalTotalCts(flxDetails, 17)
            txtGrLabour.Text = CalTotalCts(flxDetails, 44)
            txtTotValue.Text = CalTotalCts(flxDetails, 18)

        ElseIf cmbDepartment.Text = "Baguettes" Or cmbDepartment.Text = "Princess" Or cmbDepartment.Text = "Emerald" Or cmbDepartment.Text = "Opening" Or _
                    cmbDepartment.Text = "Lamour" Or cmbDepartment.Text = "Davinci" Or cmbDepartment.Text = "Carrer" Or cmbDepartment.Text = "Baguettes2" Or _
                    cmbDepartment.Text = "Baguettes3" Or cmbDepartment.Text = "Princess2" Or cmbDepartment.Text = "Emerald2" Or cmbDepartment.Text = "Emerald3" Or _
                    cmbDepartment.Text = "Rounds4" Or cmbDepartment.Text = "RoundsNLE" Or cmbDepartment.Text = "Asscher" Or cmbDepartment.Text = "Radiant" Then

            flxDetails.Rows.Clear()

            ExpProgress.Visible = True
            ExpProgress.Value = 0

            rsSelectAll = New ADODB.Recordset
            rsSelectAll.Open("SELECT Reference1 FROM tblExportVarification WHERE Status = 'A' AND Department = '" & cmbDepartment.Text & "' GROUP BY Reference1 ORDER BY Reference1", AdoCN, 1, 1)
            If rsSelectAll.RecordCount Then
                rsSelectAll.MoveFirst()
                ExpProgress.Maximum = rsSelectAll.RecordCount
                While Not rsSelectAll.EOF
                    If chkReturn.Checked = True Then
                        Load_FancyRecordsReturn(rsSelectAll.Fields("Reference1").Value)
                    Else
                        Load_FancyRecords(rsSelectAll.Fields("Reference1").Value)
                    End If

                    ExpProgress.Value = ExpProgress.Value + 1
                    rsSelectAll.MoveNext()
                End While
            End If
            rsSelectAll = Nothing

            ExpProgress.Value = 0
            ExpProgress.Visible = False

            txtPcs.Text = CalTotalPcs(flxDetails, 7)
            txtCts.Text = CalTotalCts(flxDetails, 8)
            txtTotPcs.Text = CalTotalPcs(flxDetails, 12)
            txtTotCts.Text = CalTotalCts(flxDetails, 13)
            txtLabour.Text = CalTotalCts(flxDetails, 17)
            txtGrLabour.Text = CalTotalCts(flxDetails, 44)
            txtTotValue.Text = CalTotalCts(flxDetails, 18)

        ElseIf cmbDepartment.Text = "GradingPCU_N" Then
            flxDetails.Rows.Clear()

            ExpProgress.Visible = True
            ExpProgress.Value = 0

            rsSelectAll = New ADODB.Recordset
            rsSelectAll.Open("SELECT Reference1 FROM tblExportVarification WHERE Status = 'A' AND Department = '" & cmbDepartment.Text & "' GROUP BY Reference1 ORDER BY Reference1", AdoCN, 1, 1)
            If rsSelectAll.RecordCount Then
                rsSelectAll.MoveFirst()
                ExpProgress.Maximum = rsSelectAll.RecordCount
                While Not rsSelectAll.EOF
                    Load_GradingPCU_NRecords(rsSelectAll.Fields("Reference1").Value)

                    ExpProgress.Value = ExpProgress.Value + 1
                    rsSelectAll.MoveNext()
                End While
            End If
            rsSelectAll = Nothing

            ExpProgress.Value = 0
            ExpProgress.Visible = False

            txtPcs.Text = CalTotalPcs(flxDetails, 7)
            txtCts.Text = CalTotalCts(flxDetails, 8)
            txtTotPcs.Text = CalTotalPcs(flxDetails, 12)
            txtTotCts.Text = CalTotalCts(flxDetails, 13)
            txtLabour.Text = CalTotalCts(flxDetails, 17)
            txtGrLabour.Text = CalTotalCts(flxDetails, 44)
            txtTotValue.Text = CalTotalCts(flxDetails, 18)
        End If
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(intCalRow, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(intCalRow, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Function CalTotalValue(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Double
        Dim intRow As Integer

        CalTotalValue = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalValue = CalTotalValue + Val(flxSample.Item(intCalRow, intRow).Value)
        Next
        CalTotalValue = Math.Round(CalTotalValue, 2)
    End Function

    Private Sub txtMargin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMargin.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtMargin.Text)
    End Sub

    Private Sub chkAsking_CheckedChanged(sender As Object) Handles chkAsking97.CheckedChanged
        If chkAsking97.Checked = True Then
            chkAsking100.Checked = False
            chkAsking105.Checked = False
            chkAsking985.Checked = False
            chkMaxCost.Checked = False
            chkAsking80.Checked = False
            chkAsking70.Checked = False
            chkAsking75.Checked = False
            chkAsking50.Checked = False
            chkAsking65.Checked = False
            chkAsking90.Checked = False
            chkAsking85.Checked = False
            chkAsking68.Checked = False
        End If
    End Sub

    Private Sub chkAsking100_CheckedChanged(sender As Object) Handles chkAsking100.CheckedChanged
        If chkAsking100.Checked = True Then
            chkAsking97.Checked = False
            chkAsking105.Checked = False
            chkAsking985.Checked = False
            chkMaxCost.Checked = False
            chkAsking80.Checked = False
            chkAsking70.Checked = False
            chkAsking75.Checked = False
            chkAsking50.Checked = False
            chkAsking65.Checked = False
            chkAsking90.Checked = False
            chkAsking85.Checked = False
            chkAsking68.Checked = False
        End If
    End Sub

    Private Sub chkAsking105_CheckedChanged(sender As Object) Handles chkAsking105.CheckedChanged
        If chkAsking105.Checked = True Then
            chkAsking97.Checked = False
            chkAsking100.Checked = False
            chkAsking985.Checked = False
            chkMaxCost.Checked = False
            chkAsking80.Checked = False
            chkAsking70.Checked = False
            chkAsking75.Checked = False
            chkAsking50.Checked = False
            chkAsking65.Checked = False
            chkAsking65.Checked = False
            chkAsking90.Checked = False
            chkAsking85.Checked = False
            chkAsking68.Checked = False
        End If
    End Sub

    Private Sub chkAsking985_CheckedChanged(sender As Object) Handles chkAsking985.CheckedChanged
        If chkAsking985.Checked = True Then
            chkAsking97.Checked = False
            chkAsking100.Checked = False
            chkAsking105.Checked = False
            chkMaxCost.Checked = False
            chkAsking80.Checked = False
            chkAsking70.Checked = False
            chkAsking75.Checked = False
            chkAsking50.Checked = False
            chkAsking65.Checked = False
            chkAsking90.Checked = False
            chkAsking85.Checked = False
            chkAsking68.Checked = False
        End If
    End Sub

    Private Sub chkMaxCost_CheckedChanged(sender As Object) Handles chkMaxCost.CheckedChanged
        If chkMaxCost.Checked = True Then
            chkAsking97.Checked = False
            chkAsking100.Checked = False
            chkAsking105.Checked = False
            chkAsking985.Checked = False
            chkAsking80.Checked = False
            chkAsking70.Checked = False
            chkAsking50.Checked = False
            chkAsking65.Checked = False
            chkAsking90.Checked = False
            chkAsking85.Checked = False
            chkAsking68.Checked = False
        End If
    End Sub

    Private Sub chkAsking80_CheckedChanged(sender As Object) Handles chkAsking80.CheckedChanged
        If chkAsking80.Checked = True Then
            chkAsking97.Checked = False
            chkAsking100.Checked = False
            chkAsking105.Checked = False
            chkAsking985.Checked = False
            chkMaxCost.Checked = False
            chkAsking70.Checked = False
            chkAsking75.Checked = False
            chkAsking50.Checked = False
            chkAsking65.Checked = False
            chkAsking90.Checked = False
            chkAsking85.Checked = False
            chkAsking68.Checked = False
        End If
    End Sub

    Private Sub chkAsking70_CheckedChanged(sender As Object) Handles chkAsking70.CheckedChanged
        If chkAsking70.Checked = True Then
            chkAsking97.Checked = False
            chkAsking100.Checked = False
            chkAsking105.Checked = False
            chkAsking985.Checked = False
            chkMaxCost.Checked = False
            chkAsking80.Checked = False
            chkAsking75.Checked = False
            chkAsking50.Checked = False
            chkAsking65.Checked = False
            chkAsking90.Checked = False
            chkAsking85.Checked = False
            chkAsking68.Checked = False
        End If
    End Sub

    Private Sub chkAsking75_CheckedChanged(sender As Object) Handles chkAsking75.CheckedChanged
        If chkAsking75.Checked = True Then
            chkAsking97.Checked = False
            chkAsking100.Checked = False
            chkAsking105.Checked = False
            chkAsking985.Checked = False
            chkMaxCost.Checked = False
            chkAsking80.Checked = False
            chkAsking70.Checked = False
            chkAsking50.Checked = False
            chkAsking65.Checked = False
            chkAsking90.Checked = False
            chkAsking85.Checked = False
            chkAsking68.Checked = False
        End If
    End Sub

    Private Sub chkAsking50_CheckedChanged(sender As Object) Handles chkAsking50.CheckedChanged
        If chkAsking50.Checked = True Then
            chkAsking97.Checked = False
            chkAsking100.Checked = False
            chkAsking105.Checked = False
            chkAsking985.Checked = False
            chkMaxCost.Checked = False
            chkAsking80.Checked = False
            chkAsking70.Checked = False
            chkAsking75.Checked = False
            chkAsking65.Checked = False
            chkAsking90.Checked = False
            chkAsking85.Checked = False
            chkAsking68.Checked = False
        End If
    End Sub

    Private Sub chkAsking65_CheckedChanged(sender As Object) Handles chkAsking65.CheckedChanged
        If chkAsking65.Checked = True Then
            chkAsking97.Checked = False
            chkAsking100.Checked = False
            chkAsking105.Checked = False
            chkAsking985.Checked = False
            chkMaxCost.Checked = False
            chkAsking80.Checked = False
            chkAsking70.Checked = False
            chkAsking75.Checked = False
            chkAsking50.Checked = False
            chkAsking90.Checked = False
            chkAsking85.Checked = False
            chkAsking68.Checked = False
        End If
    End Sub

    Private Sub chkAsking90_CheckedChanged(sender As Object) Handles chkAsking90.CheckedChanged
        If chkAsking90.Checked = True Then
            chkAsking100.Checked = False
            chkAsking105.Checked = False
            chkAsking985.Checked = False
            chkMaxCost.Checked = False
            chkAsking80.Checked = False
            chkAsking70.Checked = False
            chkAsking75.Checked = False
            chkAsking50.Checked = False
            chkAsking65.Checked = False
            chkAsking97.Checked = False
            chkAsking85.Checked = False
            chkAsking68.Checked = False
        End If
    End Sub

    Private Sub chkAsking85_CheckedChanged(sender As Object) Handles chkAsking85.CheckedChanged
        If chkAsking85.Checked = True Then
            chkAsking97.Checked = False
            chkAsking100.Checked = False
            chkAsking105.Checked = False
            chkAsking985.Checked = False
            chkMaxCost.Checked = False
            chkAsking80.Checked = False
            chkAsking70.Checked = False
            chkAsking75.Checked = False
            chkAsking50.Checked = False
            chkAsking90.Checked = False
            chkAsking65.Checked = False
            chkAsking68.Checked = False
        End If
    End Sub

    Private Sub chkAsking68_CheckedChanged(sender As Object) Handles chkAsking68.CheckedChanged
        If chkAsking68.Checked = True Then
            chkAsking97.Checked = False
            chkAsking100.Checked = False
            chkAsking105.Checked = False
            chkAsking985.Checked = False
            chkMaxCost.Checked = False
            chkAsking80.Checked = False
            chkAsking70.Checked = False
            chkAsking75.Checked = False
            chkAsking50.Checked = False
            chkAsking90.Checked = False
            chkAsking65.Checked = False
            chkAsking85.Checked = False
        End If
    End Sub
End Class