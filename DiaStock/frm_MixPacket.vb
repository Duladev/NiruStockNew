
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixPacket
    Dim strFolderPath As String

    Private Sub ClearFields()
        txtPktNo.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        cmbRef.Text = ""
        cmbRef.Items.Clear()
        cmbFlow.Text = ""
        cmbSide.Text = ""
        cmbSide.Items.Clear()
        cmbAssort.Text = ""
        txtBalPcs.Text = "0"
        txtBalCts.Text = "0"
        txtLen.Text = "0"
        txtWid.Text = "0"
        txtGroup.Text = ""
        txtCost.Text = "0"
        txtValue.Text = "0"
        txtRemarks.Text = "0"
        txtRate.Text = "0"
        chkReject.Checked = False
        flxDetails.Rows.Clear()
        txtSubject.Text = ""
        cmbEmpNo.Items.Clear()
        cmbEmpNo.Text = ""
        txtOrderBalPcs.Text = ""
        txtSetPcs.Text = ""
        chkSample.Checked = False
        txtDept.Text = ""
        txtClient.Text = ""
        chkAms.Checked = False
        chkExport.Checked = False
        chkApproval.Checked = False
        chkSarine.Checked = False
    End Sub

    Private Sub ClearFields2()
        txtPktNo.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        cmbFlow.Text = ""
        cmbRef.Text = ""
        cmbSide.Text = ""
        cmbSide.Items.Clear()
        cmbAssort.Text = ""
        txtBalPcs.Text = "0"
        txtBalCts.Text = "0"
        txtLen.Text = "0"
        txtWid.Text = "0"
        txtGroup.Text = ""
        txtCost.Text = "0"
        txtValue.Text = "0"
        txtRemarks.Text = "0"
        txtRate.Text = "0"
        chkReject.Checked = False
        flxDetails.Rows.Clear()
        cmbEmpNo.Items.Clear()
        cmbEmpNo.Text = ""
        txtOrderBalPcs.Text = ""
        txtSetPcs.Text = ""
        chkSample.Checked = False
        chkAms.Checked = False
        chkExport.Checked = False
        chkApproval.Checked = False
        chkSarine.Checked = False
    End Sub

    Private Sub Load_OrderDetails()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT OrderNo,Subject,Subject2,Dept,Niruref FROM tblOrders WHERE OrderNo = '" & CInt(txtOrderNo.Text) & "' AND Complete = 'N'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            txtSubject.Text = rsComSql.Fields("Subject").Value & " " & rsComSql.Fields("Subject2").Value
            txtDept.Text = rsComSql.Fields("Dept").Value
            txtClient.Text = rsComSql.Fields("Niruref").Value

            cmbRef.Items.Clear()
            cmbSide.Items.Clear()
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT RefNo FROM tblOrdersDtls WHERE OrderNo = '" & CInt(txtOrderNo.Text) & "' GROUP BY RefNo ORDER BY RefNo", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                rsComSql_1.MoveFirst()
                While Not rsComSql_1.EOF
                    cmbRef.Items.Add(rsComSql_1.Fields("RefNo").Value)

                    rsComSql_1.MoveNext()
                End While
                cmbRef.SelectedIndex = 0
            End If
            rsComSql_1 = Nothing

            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT MAX(CONVERT(NUMERIC, PktNo)) AS MaxPktNo FROM tblMixPacket WHERE PktOrdNo = '" & CInt(txtOrderNo.Text) & "' AND LEN(PktNo) = 4", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                If Not IsDBNull(rsComSql_1.Fields("MaxPktNo").Value) Then
                    txtPktNo.Text = Format(CDbl(rsComSql_1.Fields("MaxPktNo").Value) + 1, "0000")
                Else
                    txtPktNo.Text = "0001"
                End If
            Else
                txtPktNo.Text = "0001"
            End If
            rsComSql_1 = Nothing

            cmbRef.Focus()
        Else
            MsgBox("Invalid Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
            txtOrderNo.Focus()
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_OrderDetails2()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT OrderNo,Subject,Subject2,Dept,Niruref FROM tblOrders WHERE OrderNo = '" & CInt(txtOrderNo.Text) & "' AND Complete = 'N'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            cmbRef.SelectedIndex = 0
            cmbSide.Items.Clear()

            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT MAX(CONVERT(NUMERIC, PktNo)) AS MaxPktNo FROM tblMixPacket WHERE PktOrdNo = '" & CInt(txtOrderNo.Text) & "' AND LEN(PktNo) = 4", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                If Not IsDBNull(rsComSql_1.Fields("MaxPktNo").Value) Then
                    txtPktNo.Text = Format(CDbl(rsComSql_1.Fields("MaxPktNo").Value) + 1, "0000")
                Else
                    txtPktNo.Text = "0001"
                End If
            Else
                txtPktNo.Text = "0001"
            End If
            rsComSql_1 = Nothing

            cmbRef.Focus()
        Else
            MsgBox("Invalid Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
            txtOrderNo.Focus()
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Packets()
        If txtOrderNo.Text <> "" Then
            flxDetails.Rows.Clear()
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblMixPacket WHERE PktOrdNo = '" & CInt(txtOrderNo.Text) & "' ORDER BY PktNo", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                rsComSql_1.MoveFirst()
                While Not rsComSql_1.EOF
                    flxDetails.Rows.Add(rsComSql_1.Fields("PktOrdNo").Value,
                                        rsComSql_1.Fields("PktNo").Value,
                                        rsComSql_1.Fields("PktRefNo").Value,
                                        rsComSql_1.Fields("Pktside").Value,
                                        UCase(rsComSql_1.Fields("Grp").Value),
                                        rsComSql_1.Fields("AssortNo").Value,
                                        rsComSql_1.Fields("PktFlow").Value,
                                        rsComSql_1.Fields("PktPcs").Value,
                                        Format(rsComSql_1.Fields("PktCts").Value, "#0.000"),
                                        Format(rsComSql_1.Fields("PktIss").Value, "yyyy/MM/dd"),
                                        rsComSql_1.Fields("IssEmpNo").Value)

                    rsComSql_1.MoveNext()
                End While
            End If
            rsComSql_1 = Nothing
        End If
    End Sub

    Private Sub txtOrderNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrderNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtOrderNo.Text <> "" Then
                If Len(txtOrderNo.Text) = 6 Then
                    ClearFields()
                    Load_OrderDetails()
                Else
                    MsgBox("Invalid Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    ClearFields()
                    txtOrderNo.Focus()
                End If
            Else
                MsgBox("Invalid Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
                txtOrderNo.Focus()
            End If
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmbRef_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbRef.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbSide.Focus()
        End If
    End Sub

    Private Sub cmbRef_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRef.SelectedIndexChanged
        cmbSide.Items.Clear()
        txtOrderBalPcs.Text = ""
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT Side FROM tblOrdersDtls WHERE OrderNo = '" & CInt(txtOrderNo.Text) & "' AND RefNo = '" & Replace(cmbRef.Text, "'", "''") & "' GROUP BY Side ORDER BY Side", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                cmbSide.Items.Add(rsComSql_1.Fields("Side").Value)

                rsComSql_1.MoveNext()
            End While
            cmbSide.SelectedIndex = 0
        End If
        rsComSql_1 = Nothing

        rsComSql_1 = New ADODB.Recordset
        mStrSQL = "SELECT * FROM VW_OrderRefNos WHERE OrderNo = '" & txtOrderNo.Text & "' AND RefNo = '" & Replace(cmbRef.Text, "'", "''") & "'"
        rsComSql_1.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            cmbFlow.Text = rsComSql_1.Fields("Flow").Value
        End If
        rsComSql_1 = Nothing

        rsComSql_1 = New ADODB.Recordset
        mStrSQL = "SELECT * FROM tblOrdersDtls WHERE OrderNo = '" & txtOrderNo.Text & "' AND RefNo = '" & Replace(cmbRef.Text, "'", "''") & "'"
        rsComSql_1.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            txtLen.Text = rsComSql_1.Fields("Length").Value
            txtWid.Text = rsComSql_1.Fields("Width").Value
            txtRate.Text = rsComSql_1.Fields("Cts").Value
        End If

        cmbSide.Focus()
    End Sub

    Private Sub Load_Assort()
        Dim rstAssort As ADODB.Recordset

        cmbAssort.Items.Clear()
        rstAssort = New ADODB.Recordset
        rstAssort.Open("SELECT DISTINCT Assortment FROM tblMixIntIssues WHERE IssDate = '" & Format(Date.Now, "MM/dd/yyyy") & "' AND OK = 1 ORDER BY Assortment", AdoCN, 1, 1)
        If rstAssort.RecordCount Then
            rstAssort.MoveFirst()
            While Not rstAssort.EOF
                cmbAssort.Items.Add(rstAssort.Fields("Assortment").Value)
                rstAssort.MoveNext()
            End While
        End If
        rstAssort = Nothing

    End Sub

    Private Sub frm_MixPacket_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        If strDBName = "DiaStock" Then
            strFolderPath = "Mix\"
        ElseIf strDBName = "DiaSales" Then
            strFolderPath = "DiaSalesMix\"
        Else
            strFolderPath = "DiaShareMix\"
        End If

        'If PBUser_Level <= 2 Then
        '    cmdDelete.Visible = True
        'Else
        '    cmdDelete.Visible = False
        'End If

        'If PBUser_EmpNo = "D06975" Or PBUser_EmpNo = "D09472" Or PBUser_EmpNo = "D07954" Or PBUser_EmpNo = "D06313" Or PBUser_EmpNo = "D10504" Then
        '    cmdDelete.Visible = True
        'Else
        '    cmdDelete.Visible = False
        'End If

        Load_Assort()
    End Sub

    Private Sub cmbAssort_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbAssort.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbAssort.Text = UCase(cmbAssort.Text)
            'If Mid(cmbAssort.Text, 1, 3) = "SRW" Or Mid(cmbAssort.Text, 1, 3) = "ARW" Then
            '    txtPcs.Text = "1"
            '    txtCts.Focus()
            'Else
            '    txtPcs.Focus()
            'End If
            txtPcs.Focus()
        End If
    End Sub

    Private Sub cmbAssort_LostFocus(sender As Object, e As EventArgs) Handles cmbAssort.LostFocus
        Dim intTotIssPcs As Integer
        Dim dblTotIssCts As Double
        Dim intTotPktPcs As Integer
        Dim dblTotPktCts As Double

        If cmbAssort.Text <> "" Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & cmbAssort.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                txtCost.Text = rsComSql.Fields("MarketPrice").Value
            Else
                txtBalPcs.Text = ""
                txtBalCts.Text = ""
            End If
            rsComSql = Nothing

            txtBalPcs.Text = "0"
            txtBalCts.Text = "0"

            intTotIssPcs = 0
            dblTotIssCts = 0
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(Pcs) AS Pcs,SUM(Cts) AS Cts FROM tblMixIntIssues WHERE IssDate = '" & Format(Date.Now, "MM/dd/yyyy") & "' AND Assortment = '" & cmbAssort.Text & "' AND OK = 1", AdoCN, 1, 1)
            If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                intTotIssPcs = rsComSql.Fields("Pcs").Value
            End If
            If Not IsDBNull(rsComSql.Fields("Cts").Value) Then
                dblTotIssCts = rsComSql.Fields("Cts").Value
                dblTotIssCts = Math.Round(dblTotIssCts, 3)
            End If
            rsComSql = Nothing

            intTotPktPcs = 0
            dblTotPktCts = 0
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(Pcs) AS TotPcs,SUM(Cts) AS TotCts FROM tblMixPacketDetails WHERE Assortment = '" & cmbAssort.Text & "' AND EntDate = '" & Format(Date.Now, "MM/dd/yyyy") & "' AND Type = 'P'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("TotPcs").Value) Then
                    intTotPktPcs = rsComSql.Fields("TotPcs").Value
                End If
                If Not IsDBNull(rsComSql.Fields("TotCts").Value) Then
                    dblTotPktCts = rsComSql.Fields("TotCts").Value
                    dblTotPktCts = Math.Round(dblTotPktCts, 3)
                End If
            End If
            rsComSql = Nothing

            txtBalPcs.Text = intTotIssPcs - intTotPktPcs
            txtBalCts.Text = Math.Round(Math.Round(dblTotIssCts, 3) - Math.Round(dblTotPktCts, 3), 3)

            cmbEmpNo.Text = ""
            cmbEmpNo.Items.Clear()
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT DISTINCT EmpNo2 FROM tblMixIntIssues WHERE IssDate = '" & Format(Date.Now, "MM/dd/yyyy") & "' AND Assortment = '" & cmbAssort.Text & "' AND OK = 1", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                cmbEmpNo.Text = rsComSql.Fields("EmpNo2").Value
                While Not rsComSql.EOF
                    cmbEmpNo.Items.Add(rsComSql.Fields("EmpNo2").Value)
                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing
        Else
            txtCost.Text = "0"
        End If
    End Sub

    Private Sub cmbSide_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSide.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbAssort.Focus()
        End If
    End Sub

    Private Sub txtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs.KeyPress
        Dim ChkPcsIn As Integer

        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            ChkPcsIn = 0
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(InCts - OutCts) AS Cts, SUM(InPcs - OutPcs) AS PCs " & _
                          "FROM VW_MixAssortInOutNew " & _
                          "WHERE Assortment = '" & cmbAssort.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount > 0 Then
                If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                    ChkPcsIn = rsComSql.Fields("Pcs").Value
                End If
            End If
            rsComSql = Nothing
            If CInt(txtPcs.Text) > ChkPcsIn Then
                txtPcs.Text = "0"
                MsgBox("Cannot Issue. Assortment Pcs not enough", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
            End If

            txtCts.Focus()
        End If
    End Sub

    Private Sub txtCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCts.KeyPress
        Dim ChkCtsIn As Double

        e.Handled = NumericOnly(Asc(e.KeyChar), txtCts.Text)
        If Asc(e.KeyChar) = 13 Then
            ChkCtsIn = 0
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(InCts - OutCts) AS Cts, SUM(InPcs - OutPcs) AS PCs " & _
                          "FROM VW_MixAssortInOutNew " & _
                          "WHERE Assortment = '" & cmbAssort.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount > 0 Then
                If Not IsDBNull(rsComSql.Fields("Cts").Value) Then
                    ChkCtsIn = Math.Round(rsComSql.Fields("Cts").Value, 3)
                End If
            End If
            rsComSql = Nothing

            If CDbl(txtCts.Text) > ChkCtsIn Then
                txtPcs.Text = "0"
                MsgBox("Cannot Issue. Assortment Cts not enough", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
            Else
                cmbEmpNo.Focus()
            End If
        End If
    End Sub

    Private Sub txtPktNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            LoadPacketDetails()
        End If
    End Sub

    Private Sub LoadPacketDetails()
        If txtOrderNo.Text <> "" And txtPktNo.Text <> "" Then
            If Len(txtOrderNo.Text) = 6 And Len(txtPktNo.Text) > 0 Then
                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT * FROM tblMixPacket WHERE PktOrdNo = '" & txtOrderNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                If rsComSql_2.RecordCount > 0 Then
                    txtPcs.Text = rsComSql_2.Fields("PktPcs").Value
                    txtCts.Text = rsComSql_2.Fields("PktCts").Value
                    cmbRef.Text = rsComSql_2.Fields("PktRefNo").Value
                    cmbSide.Text = rsComSql_2.Fields("Pktside").Value
                    cmbFlow.Text = rsComSql_2.Fields("PktFlow").Value
                    cmbAssort.Text = rsComSql_2.Fields("AssortNo").Value
                    If IsDBNull((rsComSql_2.Fields("Grp").Value)) = False Then
                        txtGroup.Text = UCase(rsComSql_2.Fields("Grp").Value)
                    Else
                        txtGroup.Text = ""
                    End If

                    If rsComSql_2.Fields("RejectRep").Value = 1 Then
                        chkReject.Checked = True
                    Else
                        chkReject.Checked = False
                    End If
                    If rsComSql_2.Fields("Sample").Value = 1 Then
                        chkSample.Checked = True
                    Else
                        chkSample.Checked = False
                    End If
                    If rsComSql_2.Fields("AMS").Value = 1 Then
                        chkAms.Checked = True
                    Else
                        chkAms.Checked = False
                    End If
                    If rsComSql_2.Fields("Export").Value = 1 Then
                        chkExport.Checked = True
                    Else
                        chkExport.Checked = False
                    End If
                    If rsComSql_2.Fields("Sarine").Value = 1 Then
                        chkSarine.Checked = True
                    Else
                        chkSarine.Checked = False
                    End If
                    txtRemarks.Text = rsComSql_2.Fields("Remarks").Value
                    cmbEmpNo.Text = rsComSql_2.Fields("IssEmpNo").Value
                End If
                rsComSql_2 = Nothing
            End If
        End If
    End Sub

    Private Sub Save()
        Dim rstPacket As ADODB.Recordset
        Dim intTotPcs As Integer
        Dim intIssPcs As Integer
        Dim intRejPcs As Integer
        Dim intExtPcs As Integer
        Dim intLostPcs As Integer

        Dim intAssortPcs As Integer
        Dim dblAssortCts As Double
        Dim strLength As String
        Dim strWidth As String
        Dim dblLength As Double
        Dim dblWidth As Double
        Dim strOrgAssort As String
        Dim intSpecial As Integer

        Dim dblMaxCost As Double
        Dim dblStoneCost As Double

        Dim intBalPcs As Integer
        Dim intOutPcs As Integer

        Dim intTotIssPcs As Integer
        Dim dblTotIssCts As Double

        Dim intTotPktPcs As Integer
        Dim dblTotPktCts As Double

        Dim blnFound As Boolean

        Dim dblOrdPcs As Double
        Dim dblRghPcs As Double
        'Dim dblRghPerc As Double
        Dim intApproval As Integer
        Dim dblCurPktPcs As Double
        Dim blnRough As Boolean

        Dim intReject As Integer

        Dim dblApprovedPcs As Double
        Dim dblTodayPcs As Double

        Dim intApproved As Integer
        'Dim dtpDueDate As Date

        Dim dblProfit As Double

        Dim blnShipmentPlan As Boolean
        Dim dblProfitMargin As Double

        If txtOrderNo.Text <> "" And txtPktNo.Text <> "" And txtPcs.Text <> "" And txtCts.Text <> "" Then

            dtpToday = GetToday()

            If CDbl(txtPcs.Text) <= 0 Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If CDbl(txtCts.Text) <= 0 Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

            If Len(txtPktNo.Text) < 3 Then MsgBox("Invalid Packet No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

            'If CDbl(txtPcs.Text) > 10 And Mid(cmbAssort.Text, 1, 1) <> "P" Then MsgBox("Pcs exceeding maximum limit of 10", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

            If chkAms.Checked = False Then MsgBox("Please check CVT/FL", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

            cmbAssort.Text = UCase(cmbAssort.Text)
            intSpecial = 0
            dblMaxCost = 0
            dblStoneCost = 0
            dblProfit = 0
            dblProfitMargin = 0

            intApproval = 0
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT Approval, DueDate FROM tblOrders WHERE OrderNo = '" & txtOrderNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            Else
                intApproval = rsComSql.Fields("Approval").Value
                'dtpDueDate = Format(rsComSql.Fields("DueDate").Value, "MM/dd/yyyy")
            End If
            rsComSql = Nothing

            blnShipmentPlan = False
            'Check the Shipment Plan Details
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT OrderNo FROM tblPlaneOrders WHERE OrderNo = '" & txtOrderNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                blnShipmentPlan = True
            Else
                'Check the Assortment
                If UCase(Mid(cmbAssort.Text, 1, 3)) = "ANA" Or UCase(Mid(cmbAssort.Text, 1, 3)) = "ANG" Or UCase(Mid(cmbAssort.Text, 1, 4)) = "AROY" Or UCase(Mid(cmbAssort.Text, 1, 4)) = "AREJ" Then

                Else
                    'Check the Assortment
                    If UCase(Mid(cmbAssort.Text, 1, 3)) = "SRW" And UCase(strRight(cmbAssort.Text, 1)) = "U" Then

                    Else
                        'Check the Assortment
                        If UCase(Mid(cmbAssort.Text, 1, 10)) = "ACLIENTREJ" Then

                        Else
                            'Check the Client
                            If txtClient.Text = "CLIENT NO 116" Or txtClient.Text = "CLIENT NO 212" Then

                            Else
                                'Check the Order Due Date
                                'If dtpDueDate > dtpMaxDueDate Then
                                '    MsgBox("Due Date is too far - " & Format(dtpDueDate, "yyyy/MM/dd"), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                '    Exit Sub
                                'End If
                            End If
                        End If
                    End If
                End If
            End If
            rsComSql = Nothing

            If txtClient.Text = "CLIENT NO 112" And blnShipmentPlan = False Then
                dblProfitMargin = 0.7
            Else
                dblProfitMargin = 0.75
            End If

            blnRough = False
            dblCurPktPcs = 0
            If Mid(cmbAssort.Text, 1, 3) = "ARW" Or Mid(cmbAssort.Text, 1, 1) = "S" Or Mid(cmbAssort.Text, 1, 3) = "ANA" Then
                blnRough = True
                dblCurPktPcs = CDbl(txtPcs.Text)
            End If

            intTotPcs = 0
            intIssPcs = 0
            intRejPcs = 0
            intLostPcs = 0
            intExtPcs = 0

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT MaxCost, Pcs, Sets FROM tblOrdersDtls WHERE OrderNo = '" & txtOrderNo.Text & "' AND RefNo = '" & Replace(cmbRef.Text, "'", "''") & "' AND Side = '" & cmbSide.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                dblMaxCost = rsComSql.Fields("MaxCost").Value
                intTotPcs = rsComSql.Fields("Pcs").Value * rsComSql.Fields("Sets").Value
            End If
            rsComSql = Nothing

            ''Old Approval
            'If Mid(cmbAssort.Text, 1, 1) = "S" Then
            '    rsComSql = New ADODB.Recordset
            '    rsComSql.Open("SELECT AvgCost FROM tblAssortList WHERE Assortment = '" & cmbAssort.Text & "'", AdoCN, 1, 1)
            '    If rsComSql.RecordCount Then
            '        dblStoneCost = ((rsComSql.Fields("AvgCost").Value * CDbl(txtCts.Text)) / CDbl(txtPcs.Text)) + 13
            '        If dblMaxCost * 1 < dblStoneCost Then
            '            rsComSql_1 = New ADODB.Recordset
            '            rsComSql_1.Open("SELECT Assortment FROM tblMixOrderAssort WHERE Assortment = '" & cmbAssort.Text & "' AND Subject = '" & txtSubject.Text & "' AND Ref = '" & Replace(cmbRef.Text, "'", "''") & "'", AdoCN, 1, 1)
            '            If rsComSql_1.RecordCount = 0 Then
            '                MsgBox("Cost is high for " & cmbAssort.Text & " " & dblMaxCost & "/" & dblStoneCost, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '                Exit Sub
            '            End If
            '            rsComSql_1 = Nothing
            '        End If
            '    Else
            '        MsgBox("Invalid Assortment - " & cmbAssort.Text, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '        Exit Sub
            '    End If
            '    rsComSql = Nothing
            'End If

            'If Mid(cmbAssort.Text, 1, 3) = "ARW" Then
            '    'rsComSql = New ADODB.Recordset
            '    'rsComSql.Open("SELECT * FROM tblOrdersSRW WHERE OrderNo = '" & txtOrderNo.Text & "'", AdoCN, 1, 1)
            '    'If rsComSql.RecordCount = 0 Then
            '    '    If Trim(txtDept.Text) = "PCU 2" Then
            '    '        If CInt(txtPcs.Text) > 5 Then
            '    '            MsgBox("Invalid Pcs for " & cmbAssort.Text, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '    '            Exit Sub
            '    '        End If
            '    '    Else
            '    '        If CInt(txtPcs.Text) > 2 Then
            '    '            MsgBox("Invalid Pcs for " & cmbAssort.Text, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '    '            Exit Sub
            '    '        End If
            '    '    End If
            '    'End If
            '    'rsComSql = Nothing

            '    If CDbl(txtPcs.Text) > 10 And intApproval = 0 Then
            '        MsgBox("Invalid Pcs for " & cmbAssort.Text, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '        Exit Sub
            '    End If
            'End If

            'If Mid(cmbAssort.Text, 1, 1) = "S" Then
            '    rsComSql = New ADODB.Recordset
            '    rsComSql.Open("SELECT * FROM tblOrdersSRW WHERE OrderNo = '" & txtOrderNo.Text & "'", AdoCN, 1, 1)
            '    If rsComSql.RecordCount = 0 Then
            '        If Trim(txtDept.Text) = "PCU 2" Then
            '            If CDbl(txtPcs.Text) > 10 And intApproval = 0 Then
            '                MsgBox("Invalid Pcs for " & cmbAssort.Text, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '                Exit Sub
            '            End If
            '        Else
            '            If CDbl(txtPcs.Text) > 10 And intApproval = 0 Then
            '                MsgBox("Invalid Pcs for " & cmbAssort.Text, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '                Exit Sub
            '            End If
            '        End If
            '    End If
            '    rsComSql = Nothing
            'End If

            'If Trim(txtDept.Text) = "PCU 1" Then
            '    If CDbl(txtPcs.Text) > CDbl(txtSetPcs.Text) Then
            '        MsgBox("Invalid Set Pcs for " & cmbAssort.Text, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '        Exit Sub
            '    End If
            'End If

            If strDBName = "DiaShare" Then
                If CDbl(txtPcs.Text) > 12 Then
                    MsgBox("Invalid Pcs for " & cmbAssort.Text, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Else
                If CDbl(txtPcs.Text) > CDbl(txtSetPcs.Text) Then
                    MsgBox("Invalid Set Pcs for " & cmbAssort.Text, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            'Client No 197 One Stone Packet Restriction
            If txtClient.Text = "CLIENT NO 197" Then
                If CDbl(txtPcs.Text) <> 1 Then
                    MsgBox("Invalid Packet Pcs for Client No 197", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            If Len(cmbEmpNo.Text) = 0 Then
                MsgBox("Invalid Issue Emp No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            rsComSql = New ADODB.Recordset
            mStrSQL = ("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & Mid(cmbEmpNo.Text, 1, 6) & "'")
            rsComSql.Open(mStrSQL, AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Issue Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            Else
                cmbEmpNo.Text = UCase(cmbEmpNo.Text)
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(PktPcs) AS TotPcs FROM tblMixPacket WHERE PktOrdNo = '" & txtOrderNo.Text & "' AND PktRefNo = '" & Replace(cmbRef.Text, "'", "''") & "' AND Pktside = '" & cmbSide.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("TotPcs").Value) Then
                    intIssPcs = rsComSql.Fields("TotPcs").Value
                End If
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM VW_MixRej WHERE OrderNo = '" & txtOrderNo.Text & "' AND RefNo = '" & Replace(cmbRef.Text, "'", "''") & "' AND Side = '" & cmbSide.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("RejPcs").Value) Then
                    intRejPcs = CInt(rsComSql.Fields("RejPcs").Value)
                End If
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM VW_MixLost WHERE OrderNo = '" & txtOrderNo.Text & "' AND RefNo = '" & Replace(cmbRef.Text, "'", "''") & "' AND Side = '" & cmbSide.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("LostPcs").Value) Then
                    intLostPcs = CInt(rsComSql.Fields("LostPcs").Value)
                End If
            End If
            rsComSql = Nothing

            intIssPcs = intIssPcs - (intRejPcs + intLostPcs) + intExtPcs

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(InPcs - OutPcs) AS TotPcs, SUM(InCts - OutCts) AS TotCts FROM VW_MixAssortInOutNew WHERE Assortment = '" & cmbAssort.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("TotPcs").Value) Then
                    intAssortPcs = rsComSql.Fields("TotPcs").Value
                    dblAssortCts = rsComSql.Fields("TotCts").Value
                    dblAssortCts = Math.Round(dblAssortCts, 3)
                End If
            End If
            rsComSql = Nothing

            If cmbFlow.Text = "" Then
                MsgBox("Invalid Flow", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            rstPacket = New ADODB.Recordset
            rstPacket.Open("SELECT * FROM tblMixPacket WHERE PktOrdNo = '" & Trim(txtOrderNo.Text) & "' AND PktNo = '" & Trim(txtPktNo.Text) & "'", AdoCN, 1, 1)
            If rstPacket.RecordCount = 0 Then

                'New Approval for Cost high stones
                dblApprovedPcs = 0
                dblTodayPcs = 0
                intApproved = 0
                If Mid(cmbAssort.Text, 1, 1) = "S" Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT MarketPrice, AvgCost FROM tblAssortList WHERE Assortment = '" & cmbAssort.Text & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If Mid(cmbAssort.Text, 1, 1) = "S" Then
                            dblStoneCost = Math.Round(((rsComSql.Fields("AvgCost").Value * CDbl(txtCts.Text)) / CDbl(txtPcs.Text)) + 13, 2)
                        Else
                            If rsComSql.Fields("AvgCost").Value > rsComSql.Fields("MarketPrice").Value Then
                                dblStoneCost = Math.Round(((rsComSql.Fields("AvgCost").Value * CDbl(txtCts.Text)) / CDbl(txtPcs.Text)) + 13, 2)
                            Else
                                dblStoneCost = Math.Round(((rsComSql.Fields("MarketPrice").Value * CDbl(txtCts.Text)) / CDbl(txtPcs.Text)) + 13, 2)
                            End If
                        End If

                        If dblMaxCost * dblProfitMargin < dblStoneCost Then
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT SUM(PktPcs) AS PktPcs FROM tblMixPacketApproval WHERE AssortNo = '" & cmbAssort.Text & "' AND PktOrdNo = '" & txtOrderNo.Text & "' AND PktRefNo = '" & Replace(cmbRef.Text, "'", "''") & "' AND Pktside = '" & cmbSide.Text & "' AND DDate = '" & Format(dtpToday, "MM/dd/yyyy") & "' AND Approve = 1", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount Then
                                If Not IsDBNull(rsComSql_1.Fields("PktPcs").Value) Then
                                    dblApprovedPcs = rsComSql_1.Fields("PktPcs").Value
                                End If
                            End If
                            rsComSql_1 = Nothing

                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT SUM(PktPcs) AS TotPcs FROM tblMixPacket WHERE AssortNo = '" & cmbAssort.Text & "' AND PktOrdNo = '" & txtOrderNo.Text & "' AND PktRefNo = '" & Replace(cmbRef.Text, "'", "''") & "' AND Pktside = '" & cmbSide.Text & "' AND PktIss = '" & Format(dtpToday, "MM/dd/yyyy") & "' AND Approved = 1", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount Then
                                If Not IsDBNull(rsComSql_1.Fields("TotPcs").Value) Then
                                    dblTodayPcs = rsComSql_1.Fields("TotPcs").Value
                                End If
                            End If
                            rsComSql_1 = Nothing

                            If dblTodayPcs + CDbl(txtPcs.Text) > dblApprovedPcs Then
                                dblProfit = Math.Round(((dblMaxCost - dblStoneCost) / dblMaxCost) * 100, 2)
                                PBResponse = MsgBox("Cost is high for " & cmbAssort.Text & " " & dblMaxCost & "/" & dblStoneCost & "/" & dblProfit & "%. Do you want to Request?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
                                If PBResponse = MsgBoxResult.Yes Then
                                    AdoCN.Execute("INSERT INTO tblMixPacketApproval(PktOrdNo, PktRefNo, Pktside, AssortNo, PktPcs, PktCts, AvgCost, MaxCost, DDate) " & _
                                                  "VALUES('" & txtOrderNo.Text & "','" & Replace(cmbRef.Text, "'", "''") & "','" & cmbSide.Text & "','" & cmbAssort.Text & "'," & _
                                                    "'" & CDbl(txtPcs.Text) & "','" & CDbl(txtCts.Text) & "','" & dblStoneCost & "','" & dblMaxCost & "','" & Format(dtpToday, "MM/dd/yyyy") & "')")

                                    MsgBox("Request Sent for Approval", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                    Exit Sub
                                Else
                                    Exit Sub
                                End If
                            Else
                                intApproved = 1
                            End If
                        End If
                    Else
                        MsgBox("Invalid Assortment - " & cmbAssort.Text, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql = Nothing
                Else
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT AvgCost FROM tblAssortList WHERE Assortment = '" & cmbAssort.Text & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        dblStoneCost = Math.Round(((rsComSql.Fields("AvgCost").Value * CDbl(txtCts.Text)) / CDbl(txtPcs.Text)) + 13, 2)
                        If dblMaxCost * dblProfitMargin < dblStoneCost Then
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT SUM(PktPcs) AS PktPcs FROM tblMixPacketApproval WHERE AssortNo = '" & cmbAssort.Text & "' AND PktOrdNo = '" & txtOrderNo.Text & "' AND PktRefNo = '" & Replace(cmbRef.Text, "'", "''") & "' AND Pktside = '" & cmbSide.Text & "' AND DDate = '" & Format(dtpToday, "MM/dd/yyyy") & "' AND Approve = 1", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount Then
                                If Not IsDBNull(rsComSql_1.Fields("PktPcs").Value) Then
                                    dblApprovedPcs = rsComSql_1.Fields("PktPcs").Value
                                End If
                            End If
                            rsComSql_1 = Nothing

                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT SUM(PktPcs) AS TotPcs FROM tblMixPacket WHERE AssortNo = '" & cmbAssort.Text & "' AND PktOrdNo = '" & txtOrderNo.Text & "' AND PktRefNo = '" & Replace(cmbRef.Text, "'", "''") & "' AND Pktside = '" & cmbSide.Text & "' AND PktIss = '" & Format(dtpToday, "MM/dd/yyyy") & "' AND Approved = 1", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount Then
                                If Not IsDBNull(rsComSql_1.Fields("TotPcs").Value) Then
                                    dblTodayPcs = rsComSql_1.Fields("TotPcs").Value
                                End If
                            End If
                            rsComSql_1 = Nothing

                            If dblTodayPcs + CDbl(txtPcs.Text) > dblApprovedPcs Then
                                dblProfit = Math.Round(((dblMaxCost - dblStoneCost) / dblMaxCost) * 100, 2)
                                PBResponse = MsgBox("Cost is high for " & cmbAssort.Text & " " & dblMaxCost & "/" & dblStoneCost & "/" & dblProfit & "%. Do you want to Request?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
                                If PBResponse = MsgBoxResult.Yes Then
                                    AdoCN.Execute("INSERT INTO tblMixPacketApproval(PktOrdNo, PktRefNo, Pktside, AssortNo, PktPcs, PktCts, AvgCost, MaxCost, DDate) " & _
                                                  "VALUES('" & txtOrderNo.Text & "','" & Replace(cmbRef.Text, "'", "''") & "','" & cmbSide.Text & "','" & cmbAssort.Text & "'," & _
                                                    "'" & CDbl(txtPcs.Text) & "','" & CDbl(txtCts.Text) & "','" & dblStoneCost & "','" & dblMaxCost & "','" & Format(dtpToday, "MM/dd/yyyy") & "')")

                                    MsgBox("Request Sent for Approval", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                    Exit Sub
                                Else
                                    Exit Sub
                                End If
                            Else
                                intApproved = 1
                            End If
                        End If
                    Else
                        MsgBox("Invalid Assortment - " & cmbAssort.Text, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql = Nothing
                End If

                If blnRough = True Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT Assortment FROM tblAssortBlock WHERE Assortment = '" & cmbAssort.Text & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        MsgBox("Assortment is Blocked", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql = Nothing

                    dblOrdPcs = 0
                    dblRghPcs = 0

                    ''Order Pcs
                    'rsComSql = New ADODB.Recordset
                    'rsComSql.Open("SELECT SUM(Pcs * Sets) AS TotPcs FROM tblOrdersDtls WHERE (OrderNo = '" & txtOrderNo.Text & "')", AdoCN, 1, 1)
                    'If rsComSql.RecordCount Then
                    '    If Not IsDBNull(rsComSql.Fields("TotPcs").Value) Then
                    '        dblOrdPcs = rsComSql.Fields("TotPcs").Value
                    '    End If
                    'End If
                    'rsComSql = Nothing

                    ''Issued Rough Pcs
                    'rsComSql = New ADODB.Recordset
                    'rsComSql.Open("SELECT SUM(PktPcs) AS TotPcs FROM tblMIXPacket WHERE (PktOrdNo = '" & txtOrderNo.Text & "') AND (LEFT(AssortNo, 3) = 'ARW' OR LEFT(AssortNo, 3) = 'SRW' OR LEFT(AssortNo, 3) = 'ANA')", AdoCN, 1, 1)
                    'If rsComSql.RecordCount Then
                    '    If Not IsDBNull(rsComSql.Fields("TotPcs").Value) Then
                    '        dblRghPcs = rsComSql.Fields("TotPcs").Value
                    '    End If
                    'End If
                    'rsComSql = Nothing

                    ''Rejected Rough Pcs
                    'rsComSql = New ADODB.Recordset
                    'rsComSql.Open("SELECT SUM(dbo.tblMixReturns.RejPcs + dbo.tblMixReturns.LostPcs) AS RejPcs " & _
                    '              "FROM dbo.tblMixPacket INNER JOIN dbo.tblMixReturns ON dbo.tblMixPacket.PktOrdNo = dbo.tblMixReturns.ParNo AND dbo.tblMixPacket.PktNo = dbo.tblMixReturns.PktNo " & _
                    '              "WHERE (dbo.tblMixPacket.PktOrdNo = '" & txtOrderNo.Text & "') AND (LEFT(dbo.tblMixPacket.AssortNo, 3) = 'ARW' OR LEFT(dbo.tblMixPacket.AssortNo, 3) = 'SRW' OR LEFT(dbo.tblMixPacket.AssortNo, 3) = 'ANA')", AdoCN, 1, 1)
                    'If rsComSql.RecordCount Then
                    '    If Not IsDBNull(rsComSql.Fields("RejPcs").Value) Then
                    '        dblRghPcs = dblRghPcs - rsComSql.Fields("RejPcs").Value
                    '    End If
                    'End If
                    'rsComSql = Nothing

                    'dblRghPerc = ((dblRghPcs + dblCurPktPcs) / dblOrdPcs) * 100
                    'dblRghPerc = Math.Round(dblRghPerc, 2)
                    'If dblRghPerc > 30 And intApproval = 0 Then
                    '    MsgBox("Rough pcs exceeds the limit - " & dblRghPerc & "%", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    '    Exit Sub
                    'End If
                End If

                strLength = Math.Round(CDbl(txtLen.Text), 2)
                strWidth = Math.Round(CDbl(txtWid.Text), 2)

                strLength = Val(strLength) * 10
                strWidth = Val(strWidth) * 10

                If Val(Mid(Trim(cmbAssort.Text), 4, 2)) < Val(strLength) + 0.5 Then
                    intSpecial = 1
                End If
                If Val(Mid(Trim(cmbAssort.Text), 6, 2)) < Val(strWidth) + 0.5 Then
                    intSpecial = 1
                End If

                If strDBName = "DiaShare" Then

                Else
                    If chkApproval.Checked = False Then
                        dblLength = 0
                        dblWidth = 0
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT LengthFrom, WidthFrom FROM tblAssortList WHERE Assortment = '" & cmbAssort.Text & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            dblLength = rsComSql.Fields("LengthFrom").Value
                            dblWidth = rsComSql.Fields("WidthFrom").Value
                        End If
                        rsComSql = Nothing

                        If dblLength > CDbl(txtLen.Text) + 0.6 Then
                            MsgBox("Length is high.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                        If dblWidth > CDbl(txtWid.Text) + 0.3 Then
                            MsgBox("Width is high.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                    End If
                End If

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT Assortment FROM tblMixIntIssues WHERE IssDate = '" & Format(Date.Now, "MM/dd/yyyy") & "' AND Assortment = '" & cmbAssort.Text & "' AND OK = 1", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    MsgBox("Assortment not Issued", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing

                intTotIssPcs = 0
                dblTotIssCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(Pcs) AS Pcs,SUM(Cts) AS Cts FROM tblMixIntIssues WHERE IssDate = '" & Format(Date.Now, "MM/dd/yyyy") & "' AND Assortment = '" & cmbAssort.Text & "' AND (OK = 1 OR OK = 2)", AdoCN, 1, 1)
                If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                    intTotIssPcs = rsComSql.Fields("Pcs").Value
                End If
                If Not IsDBNull(rsComSql.Fields("Cts").Value) Then
                    dblTotIssCts = rsComSql.Fields("Cts").Value
                    dblTotIssCts = Math.Round(dblTotIssCts, 3)
                End If
                rsComSql = Nothing

                intTotPktPcs = 0
                dblTotPktCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(Pcs) AS TotPcs,SUM(Cts) AS TotCts FROM tblMixPacketDetails WHERE Assortment = '" & cmbAssort.Text & "' AND EntDate = '" & Format(Date.Now, "MM/dd/yyyy") & "' AND Pcs > 0 AND Type = 'P'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("TotPcs").Value) Then
                        intTotPktPcs = intTotPktPcs + rsComSql.Fields("TotPcs").Value
                    End If
                    If Not IsDBNull(rsComSql.Fields("TotCts").Value) Then
                        dblTotPktCts = dblTotPktCts + rsComSql.Fields("TotCts").Value
                        dblTotPktCts = Math.Round(dblTotPktCts, 3)
                    End If
                End If
                rsComSql = Nothing

                If intTotIssPcs < intTotPktPcs + CInt(txtPcs.Text) Then
                    MsgBox("Pcs Exceeds the limit.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If dblTotIssCts < Math.Round(dblTotPktCts + CDbl(txtCts.Text), 3) Then
                    MsgBox("Cts Exceeds the limit.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If intTotPcs < intIssPcs + CInt(txtPcs.Text) Then
                    MsgBox("Pcs Exceeds the limit.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If intAssortPcs < CInt(txtPcs.Text) Then
                    MsgBox("Pcs Exceeds the Assortment Stock Balance.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If dblAssortCts < CDbl(txtCts.Text) Then
                    MsgBox("Cts Exceeds the Assortment Stock Balance.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If CInt(txtBalPcs.Text) = CInt(txtPcs.Text) Then
                    If CDbl(txtBalCts.Text) <> CDbl(txtCts.Text) Then
                        MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If

                If CDbl(txtBalCts.Text) = CDbl(txtCts.Text) Then
                    If CInt(txtBalPcs.Text) <> CInt(txtPcs.Text) Then
                        MsgBox("Pcs not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If

                'Packet Entry
                AdoCN.Execute("INSERT INTO tblMixPacket(ParNo,PktNo,PktPcs,PktCts,PktOrdNo,PktRefNo,Pktside,AssortNo,PktFlow,Grp,AParNo,PktIss,Remarks,OK,RejectRep,Special,IssEmpNo,Sample,DoneBy,Ams,Export,NewGrp,Sarine,Approved) " & _
                              "VALUES('','" & Trim(txtPktNo.Text) & "','" & CDbl(txtPcs.Text) & "','" & CDbl(txtCts.Text) & "','" & Trim(txtOrderNo.Text) & "'," & _
                                "'" & Replace(cmbRef.Text, "'", "''") & "','" & cmbSide.Text & "','" & cmbAssort.Text & "','" & cmbFlow.Text & "','" & UCase(txtGroup.Text) & "'," & _
                                "'00','" & Format(dtpToday, "MM/dd/yyyy") & "','" & txtRemarks.Text & "',0," & IIf(chkReject.Checked = True, 1, 0) & "," & intSpecial & ",'" & cmbEmpNo.Text & "'," & _
                                "" & IIf(chkSample.Checked = True, 1, 0) & ",'" & PBUser_EmpNo & "',1," & IIf(chkExport.Checked = True, 1, 0) & ",'" & UCase(txtGroup.Text) & "'," & IIf(chkSarine.Checked = True, 1, 0) & "," & intApproved & ")")

                'AMS Log Entry
                AdoCN.Execute("INSERT INTO tblAMS2Log(MacNo,SupParcelNo,Pcs,Cts,EmpNo,EmpNoEnt,ChkDate,ChkTime,PASS,REFER,SYNTHETIC,NONDIAMOND,PURGE,NOTCHECKED,Assortment) " & _
                              "VALUES('SHGr','" & Trim(txtOrderNo.Text) & Trim(txtPktNo.Text) & "'," & CDbl(txtPcs.Text) & "," & CDbl(txtCts.Text) & "," & _
                                "'" & PBUser_EmpNo & "','" & PBUser_EmpNo & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "'," & CDbl(txtPcs.Text) & ",0,0,0,0,0,'" & cmbAssort.Text & "')")

                'Packet Details Entry
                If Mid(cmbAssort.Text, 1, 2) = "VM" Then
                    strOrgAssort = "VPCU"
                Else
                    strOrgAssort = "APCU"
                End If
                AdoCN.Execute("INSERT INTO tblMixPacketDetails(ParNo,PktNo,Pcs,Cts,Assortment,OrgAssort,EntDate,Type) " & _
                              "VALUES('" & Trim(txtOrderNo.Text) & "','" & Trim(txtPktNo.Text) & "'," & CDbl(txtPcs.Text) & "," & CDbl(txtCts.Text) & ",'" & cmbAssort.Text & "','" & strOrgAssort & "','" & Format(Date.Now, "MM/dd/yyyy") & "','P')")

                'Origin Entry
                intOutPcs = 0
                intBalPcs = CInt(txtPcs.Text)
                rsComSql = New ADODB.Recordset
                If txtClient.Text = "CLIENT NO 112" Or txtClient.Text = "CLIENT NO 116" Then
                    rsComSql.Open("SELECT * FROM VW_MixOriginBal WHERE Assortment = '" & cmbAssort.Text & "' AND BalPcs > 0 ORDER BY Origin DESC", AdoCN, 1, 1)
                Else
                    rsComSql.Open("SELECT * FROM VW_MixOriginBal WHERE Assortment = '" & cmbAssort.Text & "' AND BalPcs > 0 ORDER BY Origin", AdoCN, 1, 1)
                End If
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF And intBalPcs > 0
                        If intBalPcs > 0 Then
                            blnFound = False
                            If intBalPcs <= rsComSql.Fields("BalPcs").Value Then
                                intOutPcs = intBalPcs

                                intBalPcs = 0
                                blnFound = True
                            Else
                                intOutPcs = rsComSql.Fields("BalPcs").Value
                                intBalPcs = intBalPcs - intOutPcs
                                blnFound = True
                            End If
                            If blnFound = True Then
                                AdoCN.Execute("INSERT INTO tblMixPacketOrigin(ParNo,PktNo,Assortment,SupParNo,Origin,Pcs,EntDate,BoxInDate) " & _
                                              "VALUES('" & Trim(txtOrderNo.Text) & "','" & Trim(txtPktNo.Text) & "','" & cmbAssort.Text & "','" & rsComSql.Fields("SupParNo").Value & "','" & rsComSql.Fields("Origin").Value & "'," & intOutPcs & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(rsComSql.Fields("SysDateTime").Value, "MM/dd/yyyy") & "')")

                            End If
                        End If
                        rsComSql.MoveNext()
                    End While
                Else
                    AdoCN.Execute("INSERT INTO tblMixPacketOrigin(ParNo,PktNo,Assortment,SupParNo,Origin,Pcs,EntDate,BoxInDate) " & _
                                  "VALUES('" & Trim(txtOrderNo.Text) & "','" & Trim(txtPktNo.Text) & "','" & cmbAssort.Text & "','X900003','De Beers'," & intBalPcs & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "MM/dd/yyyy") & "')")
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(Pcs) AS Pcs FROM dbo.tblMixPacketOrigin WHERE (ParNo = '" & Trim(txtOrderNo.Text) & "') AND (PktNo = '" & Trim(txtPktNo.Text) & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                        If CDbl(txtPcs.Text) > rsComSql.Fields("Pcs").Value Then
                            AdoCN.Execute("INSERT INTO tblMixPacketOrigin(ParNo,PktNo,Assortment,SupParNo,Origin,Pcs,EntDate,BoxInDate) " & _
                                          "VALUES('" & Trim(txtOrderNo.Text) & "','" & Trim(txtPktNo.Text) & "','" & cmbAssort.Text & "','X900003','De Beers'," & CInt(txtPcs.Text) - rsComSql.Fields("Pcs").Value & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "MM/dd/yyyy") & "')")
                        End If
                    End If
                End If
                rsComSql = Nothing
            Else
                If rstPacket.Fields("Accept").Value = 0 Then
                    If Len(txtGroup.Text) > 0 Then
                        MsgBox("Cannot put a Group without Accepting the packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If
                'AdoCN.Execute("UPDATE tblMixPacket SET PktFlow = '" & cmbFlow.Text & "',Grp = '" & UCase(txtGroup.Text) & "',Sample = " & IIf(chkSample.Checked = True, 1, 0) & " " & _
                '              "WHERE PktOrdNo = '" & Trim(txtOrderNo.Text) & "' AND PktNo = '" & Trim(txtPktNo.Text) & "'")

                intReject = rstPacket.Fields("RejectRep").Value

                If chkReject.Checked = False Then
                    If intReject = 1 Then
                        intReject = 2
                    End If
                Else
                    intReject = 1
                End If

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT PktNo FROM tblMixIssues WHERE ParNo = '" & rstPacket.Fields("PktOrdNo").Value & "' AND PktNo = '" & rstPacket.Fields("PktNo").Value & "' AND Sec = 14", AdoCN, 1, 1)
                If rsComSql_1.RecordCount = 0 Then
                    AdoCN.Execute("UPDATE tblMixPacket SET Grp = '" & UCase(txtGroup.Text) & "',NewGrp = '" & UCase(txtGroup.Text) & "',Sample = " & IIf(chkSample.Checked = True, 1, 0) & ",RejectRep = " & intReject & ",Export = " & IIf(chkExport.Checked = True, 1, 0) & ",Sarine = " & IIf(chkSarine.Checked = True, 1, 0) & " " & _
                                  "WHERE PktOrdNo = '" & Trim(txtOrderNo.Text) & "' AND PktNo = '" & Trim(txtPktNo.Text) & "'")
                Else
                    AdoCN.Execute("UPDATE tblMixPacket SET Sample = " & IIf(chkSample.Checked = True, 1, 0) & ",RejectRep = " & intReject & ",Export = " & IIf(chkExport.Checked = True, 1, 0) & ",Sarine = " & IIf(chkSarine.Checked = True, 1, 0) & " " & _
                                  "WHERE PktOrdNo = '" & Trim(txtOrderNo.Text) & "' AND PktNo = '" & Trim(txtPktNo.Text) & "'")
                End If
                rsComSql_1 = Nothing

            End If
            rstPacket = Nothing

            ClearFields2()
            txtOrderNo.Focus()
            Load_OrderDetails2()
        Else
            MsgBox("Please fill all the entries before save.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            txtOrderNo.Focus()
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        objForm = New frm_DCLReportViewer
        mReportName = "PKTSLEEVE_Full.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        txtOrderNo.Text = flxDetails.Item(0, flxDetails.CurrentRow.Index).Value
        txtPktNo.Text = flxDetails.Item(1, flxDetails.CurrentRow.Index).Value

        LoadPacketDetails()
    End Sub

    Private Sub cmbSide_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSide.SelectedIndexChanged
        Dim intTotPcs As Integer
        Dim intIssPcs As Integer

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblOrdersDtls WHERE OrderNo = '" & txtOrderNo.Text & "' AND RefNo = '" & Replace(cmbRef.Text, "'", "''") & "' AND Side = '" & cmbSide.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            intTotPcs = rsComSql.Fields("Pcs").Value * rsComSql.Fields("Sets").Value
            txtSetPcs.Text = rsComSql.Fields("Pcs").Value
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT SUM(PktPcs) AS TotPcs FROM tblMixPacket WHERE PktOrdNo = '" & txtOrderNo.Text & "' AND PktRefNo = '" & Replace(cmbRef.Text, "'", "''") & "' AND Pktside = '" & cmbSide.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("TotPcs").Value) Then
                intIssPcs = rsComSql.Fields("TotPcs").Value
            End If
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM VW_MixRej WHERE OrderNo = '" & txtOrderNo.Text & "' AND RefNo = '" & Replace(cmbRef.Text, "'", "''") & "' AND Side = '" & cmbSide.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("RejPcs").Value) Then
                intIssPcs = intIssPcs - CInt(rsComSql.Fields("RejPcs").Value)
            End If
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM VW_MixLost WHERE OrderNo = '" & txtOrderNo.Text & "' AND RefNo = '" & Replace(cmbRef.Text, "'", "''") & "' AND Side = '" & cmbSide.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("LostPcs").Value) Then
                intIssPcs = intIssPcs - CInt(rsComSql.Fields("LostPcs").Value)
            End If
        End If
        rsComSql = Nothing

        txtOrderBalPcs.Text = intTotPcs - intIssPcs
    End Sub

    Private Sub Delete()
        Dim rstPacket As ADODB.Recordset
        Dim dtpIssDate As Date

        If txtOrderNo.Text <> "" And txtPktNo.Text <> "" Then
            PBResponse = MsgBox("Are you sure to Delete this Packet?", MsgBoxStyle.Information + vbYesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                rstPacket = New ADODB.Recordset
                rstPacket.Open("SELECT * FROM tblMixPacket WHERE PktOrdNo = '" & txtOrderNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Ok = 0", AdoCN, 1, 1)
                If rstPacket.RecordCount Then
                    dtpIssDate = rstPacket.Fields("PktIss").Value

                    dtpToday = GetToday()

                    If dtpToday = dtpIssDate Then
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT * FROM tblMixIssues WHERE ParNo = '" & txtOrderNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount = 0 Then
                            AdoCN.Execute("DELETE FROM tblMixPacket WHERE PktOrdNo = '" & txtOrderNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'")
                            AdoCN.Execute("DELETE FROM tblMixPacketDetails WHERE ParNo = '" & txtOrderNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'")
                            AdoCN.Execute("DELETE FROM tblMixPacketOrigin WHERE ParNo = '" & txtOrderNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'")

                            Insert_Log("DELETE", "Mix", txtOrderNo.Text, txtPktNo.Text, 0)
                            MsgBox("Packet Deleted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            ClearFields()
                        Else
                            MsgBox("Already Issued", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        End If
                        rsComSql = Nothing
                    Else
                        MsgBox("Packet created on - " & dtpIssDate, MsgBoxStyle.Information + MsgBoxStyle.OkCancel)
                    End If
                Else
                    MsgBox("Invalid Packet or Already Verified", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                End If
                rstPacket = Nothing
            End If
        Else
            MsgBox("Please fill all the entries before Delete", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Delete()
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixRghIss.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXPacketingSIH.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Load_Packets()
    End Sub

    Private Sub cmbEmpNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbEmpNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            chkReject.Focus()
        End If
    End Sub

    Private Sub chkReject_KeyPress(sender As Object, e As KeyPressEventArgs) Handles chkReject.KeyPress
        If Asc(e.KeyChar) = 13 Then
            chkApproval.Focus()
        End If
    End Sub

    Private Sub chkApproval_KeyPress(sender As Object, e As KeyPressEventArgs) Handles chkApproval.KeyPress
        If Asc(e.KeyChar) = 13 Then
            chkAms.Focus()
        End If
    End Sub

    Private Sub chkAms_KeyPress(sender As Object, e As KeyPressEventArgs) Handles chkAms.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmdSave.Focus()
        End If
    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPacketApproval.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button4_Click(sender As Object, e As EventArgs) Handles HazelDev_Button4.Click
        objForm = New frm_DCLReportViewer
        mReportName = "PKTSLEEVE_Full_Image.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub
End Class