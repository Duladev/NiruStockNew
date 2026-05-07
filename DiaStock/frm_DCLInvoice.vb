
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLInvoice
    Dim strFolderPath As String
    Dim m_LotNo As Integer

    Private Sub Load_Company()
        cmbCompany.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT CompCode FROM tblCompany ORDER BY CompCode", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbCompany.Items.Add(rsComSql.Fields("CompCode").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub frm_DCLInvoice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        If strDBName = "DiaStock" Then
            strFolderPath = "ExportFin\"
        Else
            strFolderPath = "DiaSalesExportFin\"
        End If

        ClearFields()
        Load_Company()
        Load_Supplier()
        Load_Carrier()
        Load_ShipTo()
        Load_PayBy()
        Load_Description()

        optCom.Checked = True
    End Sub

    Private Sub ClearFields()
        txtInvoiceNo.Text = ""
        cmbCompany.Text = ""
        cmbSupplierCode.Text = ""
        txtSupCode.Text = ""
        cmbBuyer.Text = ""
        txtBuyerCode.Text = ""
        cmbCarrier.Text = ""
        cmbInsure.Text = ""
        cmbBank.Text = ""
        cmbShipTo.Text = ""
        cmbPayBy.Text = ""
        txtPPNo.Text = ""
        txtExpNo.Text = ""
        cmbType.Text = ""
        cmbCat.Text = ""
        cmbDescription.Text = ""
        cmbCategory.Text = ""

        txtUSD.Text = "0"
        txtFwdChg.Text = "0"
        txtIns.Text = "0"
        txtFrChg.Text = "0"

        chkCost.Checked = False
        chkMax.Checked = False

        txtSubTotal.Text = "0"
        txtTotal.Text = "0"
        txtTotPolVal.Text = "0"
        txtTotalPcs.Text = "0"
        txtTotalCts.Text = "0"
        txtNFE.Text = "0"
        txtLabor.Text = "0"

        dtpExpInvDate.Value = Date.Now

        flxDetails.Rows.Clear()
        flxBOI.Rows.Clear()

        txtDelInvNo.Text = ""

        chkTerms.Checked = True
        chkRussiaP.Checked = False
        m_LotNo = 1
    End Sub

    Private Sub Load_Supplier()
        cmbSupplierCode.Items.Clear()
        cmbBuyer.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT CompanyName FROM tblSuppliers ORDER BY CompanyName", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbSupplierCode.Items.Add(rsComSql.Fields("CompanyName").Value)
                cmbBuyer.Items.Add(rsComSql.Fields("CompanyName").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_Carrier()
        cmbCarrier.Items.Clear()
        cmbInsure.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Carrier FROM tblCarrier ORDER BY Carrier", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbCarrier.Items.Add(rsComSql.Fields("Carrier").Value)
                cmbInsure.Items.Add(rsComSql.Fields("Carrier").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Bank()
        cmbBank.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT BankName FROM tblBank WHERE CompCode = '" & cmbCompany.Text & "' ORDER BY BankName", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbBank.Items.Add(rsComSql.Fields("BankName").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_ShipTo()
        cmbShipTo.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT ShipDet FROM tblSuppliers GROUP BY ShipDet ORDER BY ShipDet", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbShipTo.Items.Add(rsComSql.Fields("ShipDet").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Description()
        cmbDescription.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Description FROM tblExportDesc GROUP BY Description ORDER BY Description", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbDescription.Items.Add(rsComSql.Fields("Description").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_PayBy()
        cmbPayBy.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Terms FROM tblExportTerms ORDER BY Seq", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbPayBy.Items.Add(rsComSql.Fields("Terms").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Function GetNextInvoiceNo() As Double
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(ExpInvNo) AS MaxNo FROM VW_DCLExportInvMaxAll WHERE Company = '" & cmbCompany.Text & "'", dbConn, 1, 1)
        If Not IsDBNull(rsComSql.Fields("MaxNo").Value) Then
            GetNextInvoiceNo = rsComSql.Fields("MaxNo").Value + 1
        Else
            GetNextInvoiceNo = 1
        End If
        rsComSql = Nothing

    End Function

    Private Sub cmbCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCompany.SelectedIndexChanged
        txtInvoiceNo.Text = GetNextInvoiceNo()
        Load_Bank()
    End Sub

    Private Sub txtUSD_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUSD.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtUSD.Text)
        If Asc(e.KeyChar) = 13 And txtUSD.Text <> "" Then
            txtUSD.Text = "0"
            txtFwdChg.Focus()
        End If
    End Sub

    Private Sub txtFwdChg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFwdChg.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtFwdChg.Text)
        If Asc(e.KeyChar) = 13 And txtFwdChg.Text <> "" Then
            txtFwdChg.Text = "0"
            txtIns.Focus()
        End If
    End Sub

    Private Sub txtIns_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIns.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtIns.Text)
        If Asc(e.KeyChar) = 13 And txtIns.Text <> "" Then
            txtIns.Text = "0"
            txtFrChg.Focus()
        End If
    End Sub

    Private Sub txtExpNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtExpNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 And txtExpNo.Text <> "" Then
            Load_ExportDetails()
        End If
    End Sub

    Private Sub Load_ExportDetails()
        Dim intRow As Integer
        Dim blnFound As Boolean
        Dim blnNext As Boolean
        Dim dblPolishValue As Double

        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(7, intRow).Value = txtExpNo.Text Then
                MsgBox("Costing No. already entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT DISTINCT dbo.tblCosting.ExportNo, dbo.tblImport.Category, dbo.tblImport.CompCode " & _
                      "FROM dbo.tblCosting LEFT OUTER JOIN dbo.tblImport ON dbo.tblCosting.LotID = dbo.tblImport.LotNo " & _
                      "WHERE (ExportNo = " & txtExpNo.Text & ") AND (dbo.tblCosting.Status = 'A') AND (dbo.tblImport.Category IS NOT NULL)", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            txtCompany.Text = rsComSql.Fields("CompCode").Value
            cmbCategory.Text = rsComSql.Fields("Category").Value & ""
        End If
        rsComSql = Nothing

        If cmbCategory.Text = "" Then
            cmbCategory.Text = "Purchased"
        End If

        blnFound = False
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT PackingListNo, PackingType, SUM(ExportPcs) AS ExportPcs, SUM(ExportCts) AS ExportCts, SUM(Labour + GrLabour + LabourE) AS Labour, SUM(NFEValue) AS NFEValue, SUM(RoughCts) AS RoughCts, SUM(RoughCts * HardCost) AS HardValue " & _
                      "FROM tblCosting WHERE (ExportNo = " & txtExpNo.Text & ") AND (Status = 'A') " & _
                      "GROUP BY PackingListNo, PackingType", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("ExportPcs").Value) Then
                While Not rsComSql.EOF
                    blnFound = True

                    dblPolishValue = 0
                    If cmbCategory.Text = "Purchased" Or cmbCategory.Text = "Consignment" Then
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT Department, LotID FROM dbo.tblCosting WHERE (ExportNo = " & txtExpNo.Text & ") AND (RIGHT(Reference2, 1) <> 'N') GROUP BY Department, LotID", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            rsComSql_1.MoveFirst()
                            While Not rsComSql_1.EOF
                                Select Case rsComSql_1.Fields("Department").Value
                                    Case "Rounds", "RoundsNLE", "Rounds3", "Rounds4"
                                        If chkCost.Checked = False Then
                                            rsComSql_2 = New ADODB.Recordset
                                            rsComSql_2.Open("SELECT ROUND(SUM(Cts * Price), 2) AS Value FROM dbo.tblGrading_PackingListCOLM WHERE (PackNo = '" & rsComSql.Fields("PackingListNo").Value & "') AND (Analyze = 0)", AdoCN, 1, 1)
                                            If Not IsDBNull(rsComSql_2.Fields("Value").Value) Then
                                                dblPolishValue = rsComSql_2.Fields("Value").Value
                                            End If
                                            rsComSql_2 = Nothing
                                        Else
                                            dblPolishValue = rsComSql.Fields("NFEValue").Value + rsComSql.Fields("Labour").Value
                                        End If
                                    Case "Baguettes", "Princess", "Emerald", "Opening", "Lamour", "Davinci", "Asscher", "Radiant", "Carrer"
                                        If chkCost.Checked = False Then
                                            rsComSql_2 = New ADODB.Recordset
                                            rsComSql_2.Open("SELECT ROUND(SUM(dbo.tblGrading_PackingListM.Cts * dbo.tblGrading_SizingList.PRICE), 2) AS Value " & _
                                                            "FROM dbo.tblGrading_PackingListM INNER JOIN dbo.tblGrading_SizingList ON dbo.tblGrading_PackingListM.Assortment = dbo.tblGrading_SizingList.NAME " & _
                                                            "WHERE (dbo.tblGrading_PackingListM.Analyze = 0) AND (dbo.tblGrading_PackingListM.PackNo = " & rsComSql.Fields("PackingListNo").Value & ")", AdoCN, 1, 1)
                                            If Not IsDBNull(rsComSql_2.Fields("Value").Value) Then
                                                dblPolishValue = rsComSql_2.Fields("Value").Value
                                            End If
                                            rsComSql_2 = Nothing
                                        Else
                                            dblPolishValue = rsComSql.Fields("NFEValue").Value + rsComSql.Fields("Labour").Value
                                        End If
                                    Case "SizeExports"
                                        dblPolishValue = rsComSql.Fields("NFEValue").Value

                                    Case "GradingPCU_N"
                                        If chkCost.Checked = True Then
                                            dblPolishValue = rsComSql.Fields("NFEValue").Value + rsComSql.Fields("Labour").Value
                                        Else
                                            If chkMax.Checked = True Then
                                                rsComSql_2 = New ADODB.Recordset
                                                rsComSql_2.Open("SELECT ROUND(SUM(MaxValue), 2) AS PolishValue " & _
                                                                "FROM dbo.tblCosting " & _
                                                                "WHERE (Department = 'GradingPCU_N') AND (ExportNo = '" & txtExpNo.Text & "')", AdoCN, 1, 1)
                                                If Not IsDBNull(rsComSql_2.Fields("PolishValue").Value) Then
                                                    dblPolishValue = rsComSql_2.Fields("PolishValue").Value
                                                End If
                                                rsComSql_2 = Nothing

                                                Exit While
                                            Else
                                                rsComSql_2 = New ADODB.Recordset
                                                rsComSql_2.Open("SELECT ROUND(SUM(tblGrading_PackingListPCU.ActCts * tblGrading_PackingListPCU.Price), 2) AS PolishValue, SUM(tblGrading_PackingListPCU.Pcs) AS Pcs " & _
                                                                "FROM tblGrading_Pack INNER JOIN tblGrading_PackingListPCU ON tblGrading_Pack.Department = tblGrading_PackingListPCU.Department AND " & _
                                                                    "tblGrading_Pack.PackNo = tblGrading_PackingListPCU.PackNo And tblGrading_Pack.ParNo = tblGrading_PackingListPCU.ParNo " & _
                                                                "WHERE (tblGrading_Pack.PackingListNo = " & rsComSql.Fields("PackingListNo").Value & ") AND (tblGrading_Pack.Department = 'GradingPCU_N')", AdoCN, 1, 1)
                                                If Not IsDBNull(rsComSql_2.Fields("PolishValue").Value) Then
                                                    dblPolishValue = rsComSql_2.Fields("PolishValue").Value
                                                End If
                                                rsComSql_2 = Nothing
                                            End If
                                        End If

                                    Case "PolishBox"
                                        If chkCost.Checked = True Then
                                            rsComSql_2 = New ADODB.Recordset
                                            rsComSql_2.Open("SELECT ROUND(SUM(ExportCts * CurCost), 3) AS PolishValue " & _
                                                            "FROM tblCosting  " & _
                                                            "WHERE (ExportNo = '" & txtExpNo.Text & "') AND (Department = 'PolishBox')", AdoCN, 1, 1)
                                            If Not IsDBNull(rsComSql_2.Fields("PolishValue").Value) Then
                                                dblPolishValue = rsComSql_2.Fields("PolishValue").Value
                                            End If
                                            rsComSql_2 = Nothing

                                        ElseIf chkMax.Checked = True Then
                                            rsComSql_2 = New ADODB.Recordset
                                            rsComSql_2.Open("SELECT ROUND(SUM(ExportCts * SalesRate), 3) AS PolishValue " & _
                                                            "FROM tblCosting  " & _
                                                            "WHERE (ExportNo = '" & txtExpNo.Text & "') AND (Department = 'PolishBox')", AdoCN, 1, 1)
                                            If Not IsDBNull(rsComSql_2.Fields("PolishValue").Value) Then
                                                dblPolishValue = rsComSql_2.Fields("PolishValue").Value
                                            End If
                                            rsComSql_2 = Nothing

                                        Else
                                            rsComSql_2 = New ADODB.Recordset
                                            rsComSql_2.Open("SELECT ROUND(SUM(ExportCts * BaseCost), 3) AS PolishValue " & _
                                                            "FROM tblCosting  " & _
                                                            "WHERE (ExportNo = '" & txtExpNo.Text & "') AND (Department = 'PolishBox')", AdoCN, 1, 1)
                                            If Not IsDBNull(rsComSql_2.Fields("PolishValue").Value) Then
                                                dblPolishValue = rsComSql_2.Fields("PolishValue").Value
                                            End If
                                            rsComSql_2 = Nothing
                                        End If
                                        Exit While

                                    Case "Mix"
                                        If chkCost.Checked = False Then
                                            If chkMax.Checked = True Then
                                                rsComSql_2 = New ADODB.Recordset
                                                rsComSql_2.Open("SELECT ROUND(SUM(MaxValue), 2) AS PolishValue " & _
                                                                "FROM dbo.tblCosting " & _
                                                                "WHERE (Department = 'Mix') AND (ExportNo = '" & txtExpNo.Text & "')", AdoCN, 1, 1)
                                                If Not IsDBNull(rsComSql_2.Fields("PolishValue").Value) Then
                                                    dblPolishValue = rsComSql_2.Fields("PolishValue").Value
                                                End If
                                                rsComSql_2 = Nothing
                                            Else
                                                rsComSql_2 = New ADODB.Recordset
                                                rsComSql_2.Open("SELECT ROUND(SUM(NFEValue + Labour + GrLabour + Margin), 2) AS PolishValue " & _
                                                                "FROM dbo.tblCosting " & _
                                                                "WHERE (Department = 'Mix') AND (ExportNo = '" & txtExpNo.Text & "')", AdoCN, 1, 1)
                                                If Not IsDBNull(rsComSql_2.Fields("PolishValue").Value) Then
                                                    dblPolishValue = rsComSql_2.Fields("PolishValue").Value
                                                End If
                                                rsComSql_2 = Nothing
                                            End If
                                        Else
                                            dblPolishValue = rsComSql.Fields("NFEValue").Value + rsComSql.Fields("Labour").Value
                                        End If

                                        Exit While

                                    Case "KIT Box"
                                        If chkCost.Checked = False Then
                                            If chkMax.Checked = True Then
                                                rsComSql_2 = New ADODB.Recordset
                                                rsComSql_2.Open("SELECT ROUND(SUM(MaxValue), 2) AS PolishValue " & _
                                                                "FROM dbo.tblCosting " & _
                                                                "WHERE (Department = 'KIT Box') AND (ExportNo = '" & txtExpNo.Text & "')", AdoCN, 1, 1)
                                                If Not IsDBNull(rsComSql_2.Fields("PolishValue").Value) Then
                                                    dblPolishValue = rsComSql_2.Fields("PolishValue").Value
                                                End If
                                                rsComSql_2 = Nothing
                                            Else
                                                rsComSql_2 = New ADODB.Recordset
                                                rsComSql_2.Open("SELECT ROUND(SUM(NFEValue + Labour + GrLabour + Margin), 2) AS PolishValue " & _
                                                                "FROM dbo.tblCosting " & _
                                                                "WHERE (Department = 'KIT Box') AND (ExportNo = '" & txtExpNo.Text & "')", AdoCN, 1, 1)
                                                If Not IsDBNull(rsComSql_2.Fields("PolishValue").Value) Then
                                                    dblPolishValue = rsComSql_2.Fields("PolishValue").Value
                                                End If
                                                rsComSql_2 = Nothing
                                            End If
                                        Else
                                            dblPolishValue = rsComSql.Fields("NFEValue").Value + rsComSql.Fields("Labour").Value
                                        End If

                                        Exit While

                                    Case "MixRefer"
                                        If chkCost.Checked = False Then
                                            If chkMax.Checked = True Then
                                                rsComSql_2 = New ADODB.Recordset
                                                rsComSql_2.Open("SELECT ROUND(SUM(MaxValue), 2) AS PolishValue " & _
                                                                "FROM dbo.tblCosting " & _
                                                                "WHERE (Department = 'MixRefer') AND (ExportNo = '" & txtExpNo.Text & "')", AdoCN, 1, 1)
                                                If Not IsDBNull(rsComSql_2.Fields("PolishValue").Value) Then
                                                    dblPolishValue = rsComSql_2.Fields("PolishValue").Value
                                                End If
                                                rsComSql_2 = Nothing
                                            Else
                                                rsComSql_2 = New ADODB.Recordset
                                                rsComSql_2.Open("SELECT ROUND(SUM(NFEValue + Labour + GrLabour + Margin), 2) AS PolishValue " & _
                                                                "FROM dbo.tblCosting " & _
                                                                "WHERE (Department = 'MixRefer') AND (ExportNo = '" & txtExpNo.Text & "')", AdoCN, 1, 1)
                                                If Not IsDBNull(rsComSql_2.Fields("PolishValue").Value) Then
                                                    dblPolishValue = rsComSql_2.Fields("PolishValue").Value
                                                End If
                                                rsComSql_2 = Nothing
                                            End If
                                        Else
                                            dblPolishValue = rsComSql.Fields("NFEValue").Value + rsComSql.Fields("Labour").Value
                                        End If

                                        Exit While
                                    Case "Exports"
                                        If chkCost.Checked = False Then
                                            rsComSql_2 = New ADODB.Recordset
                                            rsComSql_2.Open("SELECT ROUND(SUM(MaxValue), 2) AS PolishValue " & _
                                                            "FROM dbo.tblCosting " & _
                                                            "WHERE (Department = 'Exports') AND (ExportNo = '" & txtExpNo.Text & "')", AdoCN, 1, 1)
                                            If Not IsDBNull(rsComSql_2.Fields("PolishValue").Value) Then
                                                dblPolishValue = rsComSql_2.Fields("PolishValue").Value
                                            End If
                                            rsComSql_2 = Nothing
                                        Else
                                            dblPolishValue = rsComSql.Fields("NFEValue").Value + rsComSql.Fields("Labour").Value
                                        End If

                                        Exit While
                                End Select
                                rsComSql_1.MoveNext()
                            End While
                        End If
                        rsComSql_1 = Nothing
                    End If

                    If dblPolishValue = 0 Then
                        dblPolishValue = rsComSql.Fields("NFEValue").Value + rsComSql.Fields("Labour").Value
                    End If

                    flxDetails.Rows.Add(m_LotNo,
                                        rsComSql.Fields("PackingListNo").Value,
                                        rsComSql.Fields("ExportPcs").Value,
                                        Format(rsComSql.Fields("ExportCts").Value, "#0.000"),
                                        Format(rsComSql.Fields("Labour").Value, "#0.00"),
                                        Format(rsComSql.Fields("NFEValue").Value, "#0.00"),
                                        Format(rsComSql.Fields("NFEValue").Value + rsComSql.Fields("Labour").Value, "#0.00"),
                                        txtExpNo.Text,
                                        rsComSql.Fields("PackingType").Value,
                                        "0",
                                        Format(dblPolishValue, "#0.00"),
                                        Format(rsComSql.Fields("RoughCts").Value, "#0.000"), "",
                                        Format(rsComSql.Fields("HardValue").Value, "#0.00"))

                    rsComSql.MoveNext()
                    m_LotNo = m_LotNo + 1
                End While
            End If
        End If
        rsComSql = Nothing

        If blnFound = True Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT BOINo, SupInvoiceNo, ROUND(SUM(NFEValue), 2) AS NFEValue " & _
                          "FROM dbo.tblCosting " & _
                          "WHERE (ExportNo = " & txtExpNo.Text & ") AND (Status = 'A') " & _
                          "GROUP BY BOINo, SupInvoiceNo " & _
                          "ORDER BY BOINo, SupInvoiceNo", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    blnNext = False
                    For intRow = 0 To flxBOI.Rows.Count - 1
                        If flxBOI.Item(0, intRow).Value = rsComSql.Fields("BOINo").Value And flxBOI.Item(2, intRow).Value = rsComSql.Fields("SupInvoiceNo").Value Then
                            flxBOI.Item(1, intRow).Value = CDbl(flxBOI.Item(1, intRow).Value) + rsComSql.Fields("NFEValue").Value

                            blnNext = True
                        End If
                    Next
                    If blnNext = False Then
                        flxBOI.Rows.Add(rsComSql.Fields("BOINo").Value,
                                        rsComSql.Fields("NFEValue").Value,
                                        rsComSql.Fields("SupInvoiceNo").Value)
                    End If

                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing
        End If

        txtSubTotal.Text = CalTotalCts(flxDetails, 6)
        txtTotalPcs.Text = CalTotalPcs(flxDetails)
        txtTotalCts.Text = CalTotalCts(flxDetails, 3)
        txtNFE.Text = CalTotalCts(flxDetails, 5)
        txtLabor.Text = CalTotalCts(flxDetails, 4)
        txtTotal.Text = txtSubTotal.Text + CDbl(txtFwdChg.Text) + CDbl(txtFrChg.Text) + CDbl(txtIns.Text)
        txtTotPolVal.Text = CalTotalCts(flxDetails, 10)

        txtExpNo.Text = ""
        txtExpNo.Focus()
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(2, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCol As Integer) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(intCol, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub txtFrChg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFrChg.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtFrChg.Text)
        If Asc(e.KeyChar) = 13 And txtFrChg.Text <> "" Then
            txtFrChg.Text = "0"
        End If
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub txtInvoiceNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtInvoiceNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 And txtInvoiceNo.Text <> "" Then
            Load_InvoiceDetails()
        End If
    End Sub

    Private Sub Load_InvoiceDetails()
        flxDetails.Rows.Clear()
        flxBOI.Rows.Clear()
        m_LotNo = 1

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblExportHeader WHERE ExpInvNo = '" & txtInvoiceNo.Text & "' AND Company = '" & cmbCompany.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            txtFwdChg.Text = rsComSql.Fields("FowdChrge").Value
            txtUSD.Text = rsComSql.Fields("CurrDollRate").Value
            cmbPayBy.Text = rsComSql.Fields("PayBy").Value
            cmbCarrier.Text = rsComSql.Fields("CarriedBy").Value
            cmbInsure.Text = rsComSql.Fields("InsuredBy").Value
            txtPPNo.Text = rsComSql.Fields("PassportNo").Value
            txtFrChg.Text = rsComSql.Fields("Freight").Value
            cmbBank.Text = rsComSql.Fields("BankName").Value
            cmbType.Text = rsComSql.Fields("Type").Value
            dtpExpInvDate.Value = rsComSql.Fields("InvDate").Value
            cmbCat.Text = rsComSql.Fields("InvCat").Value
            cmbDescription.Text = rsComSql.Fields("Description").Value
            cmbCategory.Text = rsComSql.Fields("Category").Value

            If rsComSql.Fields("Terms").Value = 1 Then
                chkTerms.Checked = True
            Else
                chkTerms.Checked = False
            End If

            If rsComSql.Fields("Russian").Value = 1 Then
                chkRussiaP.Checked = True
            Else
                chkRussiaP.Checked = False
            End If

            If rsComSql.Fields("RussianR").Value = 1 Then
                chkRussiaR.Checked = True
            Else
                chkRussiaR.Checked = False
            End If

            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblExportDetails WHERE ExpInvNo = '" & txtInvoiceNo.Text & "' AND Company = '" & cmbCompany.Text & "' ORDER BY LotNo", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                rsComSql_1.MoveFirst()
                While Not rsComSql_1.EOF
                    flxDetails.Rows.Add(rsComSql_1.Fields("LotNo").Value,
                                        rsComSql_1.Fields("PackNo").Value,
                                        rsComSql_1.Fields("ExpPcs").Value,
                                        rsComSql_1.Fields("ExpCts").Value,
                                        rsComSql_1.Fields("Labour").Value,
                                        rsComSql_1.Fields("NFEValue").Value,
                                        rsComSql_1.Fields("Subtotal").Value,
                                        rsComSql_1.Fields("CostingNo").Value,
                                        rsComSql_1.Fields("Reference").Value,
                                        rsComSql_1.Fields("SalesRate").Value,
                                        rsComSql_1.Fields("PolValue").Value,
                                        rsComSql_1.Fields("RghCts").Value,
                                        rsComSql_1.Fields("ID").Value,
                                        rsComSql_1.Fields("HardValue").Value)

                    m_LotNo = rsComSql_1.Fields("LotNo").Value + 1
                    rsComSql_1.MoveNext()
                End While
            End If
            rsComSql_1 = Nothing

            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblExportBOI WHERE ExpInvNo = '" & txtInvoiceNo.Text & "' AND Company = '" & cmbCompany.Text & "' ORDER BY BOINo", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                rsComSql_1.MoveFirst()
                While Not rsComSql_1.EOF
                    flxBOI.Rows.Add(rsComSql_1.Fields("BOINo").Value,
                                    rsComSql_1.Fields("NFEValue").Value,
                                    rsComSql_1.Fields("SupRefNo").Value)

                    rsComSql_1.MoveNext()
                End While
            End If
            rsComSql_1 = Nothing
        End If
        rsComSql = Nothing

        txtSubTotal.Text = CalTotalCts(flxDetails, 6)
        txtTotalPcs.Text = CalTotalPcs(flxDetails)
        txtTotalCts.Text = CalTotalCts(flxDetails, 3)
        txtNFE.Text = CalTotalCts(flxDetails, 5)
        txtLabor.Text = CalTotalCts(flxDetails, 4)
        txtTotal.Text = txtSubTotal.Text + CDbl(txtFwdChg.Text) + CDbl(txtFrChg.Text) + CDbl(txtIns.Text)
        txtTotPolVal.Text = CalTotalCts(flxDetails, 10)
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim intMax As Integer

        If cmbCompany.Text = "" Then MsgBox("Invalid Company", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtInvoiceNo.Text = "" Then MsgBox("Invalid Invoice No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If cmbBank.Text = "" Then MsgBox("Invalid Bank", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbCarrier.Text = "" Then MsgBox("Invalid Ship via", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbInsure.Text = "" Then MsgBox("Invalid Insurance by", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If cmbType.Text = "" Then MsgBox("Invalid Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbCat.Text = "" Then MsgBox("Invalid Category", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbCategory.Text = "" Then MsgBox("Invalid Invoice Category", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbDescription.Text = "" Then MsgBox("Invalid Description", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        intMax = 0
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM VW_DCLExportInvMaxAll WHERE ExpInvNo = '" & txtInvoiceNo.Text & "' AND Company = '" & cmbCompany.Text & "'", dbConn, 1, 1)
        If rsComSql.RecordCount = 0 Then

            If cmbSupplierCode.Text = "" Then MsgBox("Invalid Customer", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If cmbBuyer.Text = "" Then MsgBox("Invalid Buyer", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

            If chkMax.Checked = True Then
                intMax = 1
            Else
                intMax = 0
            End If

            AdoCN.Execute("INSERT INTO tblExportHeader(ExpInvNo,CustCode,BuyerCode,InvDate,InvTotAmt,FowdChrge,CurrDollRate,PayBy,CarriedBy,PassportNo,Freight," & _
                            "Insurance,ShipTo,ExpYear,Status,DoneBy,ModifyBy,CompCode,Category,Company,SysDate,BankName,Terms,InsuredBy,Type,InvCat,Russian,RussianR,Description) " & _
                          "VALUES('" & txtInvoiceNo.Text & "','" & Val(txtSupCode.Text) & "','" & Val(txtBuyerCode.Text) & "','" & Format(dtpExpInvDate.Value, "MM/dd/yyyy") & "'," & _
                            "" & CDbl(txtTotal.Text) & "," & CDbl(txtFwdChg.Text) & "," & CDbl(txtUSD.Text) & ",'" & cmbPayBy.Text & "','" & cmbCarrier.Text & "'," & _
                            "'" & txtPPNo.Text & "'," & CDbl(txtFrChg.Text) & "," & CDbl(txtIns.Text) & ",'" & cmbShipTo.Text & "'," & _
                            "'" & Format(dtpExpInvDate.Value, "yyyy") & "','A','" & PBUser_ID & "','" & PBUser_EmpNo & "','" & cmbCompany.Text & "','" & cmbCategory.Text & "'," & _
                            "'" & cmbCompany.Text & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & cmbBank.Text & "'," & IIf(chkTerms.Checked = True, 1, 0) & ",'" & cmbInsure.Text & "'," & _
                            "'" & cmbType.Text & "','" & cmbCat.Text & "'," & IIf(chkRussiaP.Checked = True, 1, 0) & "," & IIf(chkRussiaR.Checked = True, 1, 0) & ",'" & cmbDescription.Text & "')")

            For intRow = 0 To flxDetails.Rows.Count - 1
                AdoCN.Execute("INSERT INTO tblExportDetails(ExpInvNo,LotNo,CostingNo,Reference,ExpPcs,ExpCts,Labour,NFEValue,SubTotal,Company,PackNo,SalesRate,PolValue,RghCts,HardValue) " & _
                              "VALUES('" & txtInvoiceNo.Text & "','" & CDbl(flxDetails.Item(0, intRow).Value) & "','" & CDbl(flxDetails.Item(7, intRow).Value) & "','" & Trim(flxDetails.Item(8, intRow).Value) & "'," & _
                                "'" & CDbl(flxDetails.Item(2, intRow).Value) & "','" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & CDbl(flxDetails.Item(4, intRow).Value) & "'," & _
                                "'" & CDbl(flxDetails.Item(5, intRow).Value) & "','" & CDbl(flxDetails.Item(6, intRow).Value) & "','" & cmbCompany.Text & "','" & CDbl(flxDetails.Item(1, intRow).Value) & "'," & _
                                "'" & CDbl(flxDetails.Item(9, intRow).Value) & "','" & CDbl(flxDetails.Item(10, intRow).Value) & "','" & CDbl(flxDetails.Item(11, intRow).Value) & "','" & CDbl(flxDetails.Item(13, intRow).Value) & "')")

                AdoCN.Execute("UPDATE tblCosting SET Status = 'E',InvType = " & intMax & " WHERE ExportNo = " & CDbl(flxDetails.Item(7, intRow).Value) & "")
            Next

            For intRow = 0 To flxBOI.Rows.Count - 1
                AdoCN.Execute("INSERT INTO tblExportBOI(ExpInvNo,BOINo,NFEValue,Company,SupRefNo) " & _
                              "VALUES('" & txtInvoiceNo.Text & "','" & flxBOI.Item(0, intRow).Value & "','" & CDbl(flxBOI.Item(1, intRow).Value) & "','" & cmbCompany.Text & "','" & flxBOI.Item(2, intRow).Value & "')")
            Next

            MsgBox("Invoice Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        Else
            PBResponse = MsgBox("Invoice No. already created. Do you want update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then

                If chkMax.Checked = True Then
                    intMax = 1
                Else
                    intMax = 0
                End If

                AdoCN.Execute("UPDATE tblExportHeader SET Terms = " & IIf(chkTerms.Checked = True, 1, 0) & ",FowdChrge = " & CDbl(txtFwdChg.Text) & ",Russian = " & IIf(chkRussiaP.Checked = True, 1, 0) & "," & _
                                "CurrDollRate = " & CDbl(txtUSD.Text) & ",PayBy = '" & cmbPayBy.Text & "',CarriedBy = '" & cmbCarrier.Text & "',PassportNo = '" & txtPPNo.Text & "',Category = '" & cmbCategory.Text & "'," & _
                                "Freight = " & CDbl(txtFrChg.Text) & ",BankName = '" & cmbBank.Text & "',InvDate = '" & Format(dtpExpInvDate.Value, "MM/dd/yyyy") & "',Description = '" & cmbDescription.Text & "'," & _
                                "InsuredBy = '" & cmbInsure.Text & "',Type = '" & cmbType.Text & "',InvCat = '" & cmbCat.Text & "',InvTotAmt = " & CDbl(txtTotal.Text) & ",RussianR = " & IIf(chkRussiaR.Checked = True, 1, 0) & " " & _
                              "WHERE ExpInvNo = '" & txtInvoiceNo.Text & "' AND Company = '" & cmbCompany.Text & "'")

                For intRow = 0 To flxDetails.Rows.Count - 1
                    If Len(flxDetails.Item(10, intRow).Value) = 0 Then
                        MsgBox("Invalid Polish Value", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If Not IsNumeric(flxDetails.Item(10, intRow).Value) = True Then
                        MsgBox("Invalid Polish Value", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If CDbl(flxDetails.Item(10, intRow).Value) <= 0 Then
                        MsgBox("Invalid Polish Value", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If

                    If Len(flxDetails.Item(1, intRow).Value) = 0 Then
                        MsgBox("Invalid Pack No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If Not IsNumeric(flxDetails.Item(1, intRow).Value) = True Then
                        MsgBox("Invalid Pack No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If CDbl(flxDetails.Item(1, intRow).Value) < 0 Then
                        MsgBox("Invalid Pack No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If

                    If Len(flxDetails.Item(2, intRow).Value) = 0 Then
                        MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If Not IsNumeric(flxDetails.Item(2, intRow).Value) = True Then
                        MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If CDbl(flxDetails.Item(2, intRow).Value) < 0 Then
                        MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If

                    If Len(flxDetails.Item(3, intRow).Value) = 0 Then
                        MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If Not IsNumeric(flxDetails.Item(3, intRow).Value) = True Then
                        MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If CDbl(flxDetails.Item(3, intRow).Value) < 0 Then
                        MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If

                    If Len(flxDetails.Item(4, intRow).Value) = 0 Then
                        MsgBox("Invalid Labour", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If Not IsNumeric(flxDetails.Item(4, intRow).Value) = True Then
                        MsgBox("Invalid Labour", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If CDbl(flxDetails.Item(4, intRow).Value) < 0 Then
                        MsgBox("Invalid Labour", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If

                    If Len(flxDetails.Item(5, intRow).Value) = 0 Then
                        MsgBox("Invalid NFE Value", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If Not IsNumeric(flxDetails.Item(5, intRow).Value) = True Then
                        MsgBox("Invalid NFE Value", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    'If CDbl(flxDetails.Item(5, intRow).Value) <= 0 Then
                    '    MsgBox("Invalid NFE Value", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    '    Exit Sub
                    'End If

                    If Len(flxDetails.Item(8, intRow).Value) = 0 Then
                        MsgBox("Invalid Pack Code", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If

                    flxDetails.Item(6, intRow).Value = Math.Round(CDbl(flxDetails.Item(4, intRow).Value) + CDbl(flxDetails.Item(5, intRow).Value), 2)
                Next

                For intRow = 0 To flxDetails.Rows.Count - 1
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblExportDetails WHERE ExpInvNo = '" & txtInvoiceNo.Text & "' AND Company = '" & cmbCompany.Text & "' AND CostingNo = '" & CDbl(flxDetails.Item(7, intRow).Value) & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount = 0 Then
                        AdoCN.Execute("INSERT INTO tblExportDetails(ExpInvNo,LotNo,CostingNo,Reference,ExpPcs,ExpCts,Labour,NFEValue,SubTotal,Company,PackNo,SalesRate,PolValue,RghCts,HardValue) " & _
                                      "VALUES('" & txtInvoiceNo.Text & "','" & CDbl(flxDetails.Item(0, intRow).Value) & "','" & CDbl(flxDetails.Item(7, intRow).Value) & "','" & Trim(flxDetails.Item(8, intRow).Value) & "'," & _
                                        "'" & CDbl(flxDetails.Item(2, intRow).Value) & "','" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & CDbl(flxDetails.Item(4, intRow).Value) & "'," & _
                                        "'" & CDbl(flxDetails.Item(5, intRow).Value) & "','" & CDbl(flxDetails.Item(6, intRow).Value) & "','" & cmbCompany.Text & "','" & CDbl(flxDetails.Item(1, intRow).Value) & "'," & _
                                        "'" & CDbl(flxDetails.Item(9, intRow).Value) & "','" & CDbl(flxDetails.Item(10, intRow).Value) & "','" & CDbl(flxDetails.Item(11, intRow).Value) & "','" & CDbl(flxDetails.Item(13, intRow).Value) & "')")

                        AdoCN.Execute("UPDATE tblCosting SET Status = 'E',InvType = " & intMax & " WHERE ExportNo = " & CDbl(flxDetails.Item(7, intRow).Value) & "")
                    Else
                        AdoCN.Execute("UPDATE tblExportDetails SET PolValue = " & CDbl(flxDetails.Item(10, intRow).Value) & "," & _
                                        "Labour = " & CDbl(flxDetails.Item(4, intRow).Value) & ", NFEValue = " & CDbl(flxDetails.Item(5, intRow).Value) & "," & _
                                        "SubTotal = " & CDbl(flxDetails.Item(6, intRow).Value) & ", HardValue = " & CDbl(flxDetails.Item(13, intRow).Value) & ", " & _
                                        "ExpCts = " & CDbl(flxDetails.Item(3, intRow).Value) & ", ExpPcs = " & CDbl(flxDetails.Item(2, intRow).Value) & ", " & _
                                        "PackNo = " & CDbl(flxDetails.Item(1, intRow).Value) & ", Reference = '" & flxDetails.Item(8, intRow).Value & "' " & _
                                      "WHERE ID = " & CDbl(flxDetails.Item(12, intRow).Value) & "")
                    End If
                Next
                For intRow = 0 To flxBOI.Rows.Count - 1
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblExportBOI WHERE ExpInvNo = '" & txtInvoiceNo.Text & "' AND Company = '" & cmbCompany.Text & "' AND SupRefNo = '" & flxBOI.Item(2, intRow).Value & "' AND BOINo = '" & flxBOI.Item(0, intRow).Value & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount = 0 Then
                        AdoCN.Execute("INSERT INTO tblExportBOI(ExpInvNo,BOINo,NFEValue,Company,SupRefNo) " & _
                                      "VALUES('" & txtInvoiceNo.Text & "','" & flxBOI.Item(0, intRow).Value & "','" & CDbl(flxBOI.Item(1, intRow).Value) & "','" & cmbCompany.Text & "','" & flxBOI.Item(2, intRow).Value & "')")
                    Else
                        AdoCN.Execute("UPDATE tblExportBOI SET NFEValue = '" & CDbl(flxBOI.Item(1, intRow).Value) & "' " & _
                                      "WHERE ExpInvNo = '" & txtInvoiceNo.Text & "' AND Company = '" & cmbCompany.Text & "' AND SupRefNo = '" & flxBOI.Item(2, intRow).Value & "' AND BOINo = '" & flxBOI.Item(0, intRow).Value & "'")
                    End If
                Next
                MsgBox("Invoice Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmbSupplierCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSupplierCode.SelectedIndexChanged
        Dim rsSup As New ADODB.Recordset

        rsSup = New ADODB.Recordset
        rsSup.Open("SELECT SupplierCode, Country FROM tblSuppliers " & _
                   "WHERE CompanyName = '" & cmbSupplierCode.Text & "'", AdoCN, 1, 1)
        If Not rsSup.EOF Then
            cmbShipTo.Text = rsSup.Fields("Country").Value
            txtSupCode.Text = rsSup.Fields("SupplierCode").Value
        End If
        rsSup = Nothing
    End Sub

    Private Sub cmbBuyer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBuyer.SelectedIndexChanged
        Dim rsSup As New ADODB.Recordset

        rsSup = New ADODB.Recordset
        rsSup.Open("SELECT SupplierCode FROM tblSuppliers WHERE CompanyName = '" & cmbBuyer.Text & "'", AdoCN, 1, 1)
        If Not rsSup.EOF Then
            txtBuyerCode.Text = rsSup.Fields("SupplierCode").Value
        End If
        rsSup = Nothing
    End Sub

    Private Sub txtDelInvNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDelInvNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        If cmbCompany.Text = "" Then MsgBox("Invalid Company", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtDelInvNo.Text = "" Then MsgBox("Invalid Invoice No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        PBResponse = MsgBox("Are you sure to Delete?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblExportHeader WHERE ExpInvNo = '" & CDbl(txtDelInvNo.Text) & "' AND Company = '" & cmbCompany.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblExportDetails WHERE ExpInvNo = '" & CDbl(txtDelInvNo.Text) & "' AND Company = '" & cmbCompany.Text & "' ORDER BY CostingNo", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    rsComSql_1.MoveFirst()
                    While Not rsComSql_1.EOF
                        AdoCN.Execute("UPDATE tblCosting SET Status = 'A' WHERE ExportNo = '" & rsComSql_1.Fields("CostingNo").Value & "'")

                        rsComSql_1.MoveNext()
                    End While
                End If
                rsComSql_1 = Nothing

                AdoCN.Execute("DELETE FROM tblExportDetails WHERE ExpInvNo = '" & CDbl(txtDelInvNo.Text) & "' AND Company = '" & cmbCompany.Text & "'")
                AdoCN.Execute("DELETE FROM tblExportBOI WHERE ExpInvNo = '" & CDbl(txtDelInvNo.Text) & "' AND Company = '" & cmbCompany.Text & "'")
                AdoCN.Execute("DELETE FROM tblExportHeader WHERE ExpInvNo = '" & CDbl(txtDelInvNo.Text) & "' AND Company = '" & cmbCompany.Text & "'")

                MsgBox("Invoice No. Deleted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtDelInvNo.Text = ""
            Else
                MsgBox("Invalid Invoice No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub cmdReport_Click(sender As Object, e As EventArgs) Handles cmdReport.Click
        Select Case True
            Case optCom.Checked
                mReportName = "crptExpInvoiceALL.rpt"
            Case optCons.Checked
                mReportName = "crptExpInvoiceConsign.rpt"
            Case optCons2.Checked
                mReportName = "crptExpInvoiceConsign2.rpt"
            Case optCons3.Checked
                mReportName = "crptExpInvoiceConsign3.rpt"
            Case optConRgh.Checked
                mReportName = "crptExpInvoiceContract.rpt"
            Case optConPol.Checked
                mReportName = "crptExpInvoiceContractPol.rpt"
            Case optRough.Checked
                mReportName = "crptExpInvoiceRghSales.rpt"
            Case Else
                mReportName = ""
        End Select
        If mReportName <> "" Then
            objForm = New frm_DCLReportViewer
            strReportPath = PBReportPath & strFolderPath & mReportName
            objForm.Show()
        End If
    End Sub

    Private Sub chkCost_CheckedChanged(sender As Object) Handles chkCost.CheckedChanged
        If chkCost.Checked = True Then
            chkMax.Checked = False
        End If
    End Sub

    Private Sub chkMax_CheckedChanged(sender As Object) Handles chkMax.CheckedChanged
        If chkMax.Checked = True Then
            chkCost.Checked = False
        End If
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub chkRussiaP_CheckedChanged(sender As Object) Handles chkRussiaP.CheckedChanged
        If chkRussiaP.Checked = True Then
            chkRussiaR.Checked = False
        End If
    End Sub

    Private Sub chkRussiaR_CheckedChanged(sender As Object) Handles chkRussiaR.CheckedChanged
        If chkRussiaR.Checked = True Then
            chkRussiaP.Checked = False
        End If
    End Sub
End Class