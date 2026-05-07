
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_ExpSizingSection
    Dim issued As Boolean
    Dim Checked As Boolean
    Dim ICNo2 As String
    Dim Section As Integer
    Dim strMsg As String

    Private Sub frm_ExpSizingSection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        PictureBox1.Visible = False
        PictureBox2.Visible = False

        Load_Section()
        Load_Department(cmbDept)
        ClearFields()
    End Sub

    Private Sub Load_Section()
        Dim recsection As ADODB.Recordset

        cmbSection.Items.Clear()
        recsection = New ADODB.Recordset
        recsection.Open("SELECT * FROM tblExpSections WHERE Seq > 4 ORDER BY Seq", AdoCN, 1, 1)
        If recsection.RecordCount Then
            recsection.MoveFirst()
            While Not recsection.EOF
                cmbSection.Items.Add(recsection.Fields("SecName").Value)
                recsection.MoveNext()
            End While
        End If
        recsection = Nothing
        cmbSection.SelectedIndex = 0
    End Sub

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Dim intPos As Integer

        Instring = UCase(InputBox("Enter Par/Pkt Number"))
        If Len(Instring) > 0 Then
            txtParPkt.Text = Trim(Instring)
            intPos = InStr(1, txtParPkt.Text, "/")
            If intPos > 0 Then
                txtParNo.Text = Mid(txtParPkt.Text, 1, intPos - 1)
                txtPktNo.Text = Mid(txtParPkt.Text, intPos + 1, Len(txtParPkt.Text) - intPos)
                txtPktNo.Focus()

                Load_Packet()
            End If
        Else
            txtParPkt.Text = ""
        End If
    End Sub

    Private Sub Load_Packet()
        If txtParNo.Text <> "" And txtPktNo.Text <> "" Then
            txtParNo.Text = UCase(txtParNo.Text)
            txtPktNo.Text = UCase(txtPktNo.Text)
            If ParcelFound(cmbDept.Text, txtParNo.Text) = True Then
                Load_ParcelDetails()
                cmdEmp.Focus()
            Else
                MsgBox("Department and Parcel No. not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtParNo.Text = ""
                txtPktNo.Text = ""
                txtParNo.Focus()
            End If
        End If
    End Sub

    Private Sub cmdEmp_Click(sender As Object, e As EventArgs) Handles cmdEmp.Click
        Datavalid = False
        Parcel = False
        Instring = UCase(InputBox("Enter Emp No"))
        ParcelLen = Len(Instring)
        If ParcelLen = 6 Then
            Datavalid = True

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL3 WHERE (Pay = 1) AND (FullEmpNo = '" & Trim(Instring) & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                cmdEmp.Focus()
                Exit Sub
            End If
            rsComSql = Nothing
            ICNo = UCase(Trim(Instring))
            txtEmp.Text = ICNo
            cmbAssortType.Focus()
        Else
            MsgBox("Invalid IC No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Datavalid = False
            ICNo = ""
            cmdEmp.Focus()
            Exit Sub
        End If

        If issued = True And Checked = True Then
            If Trim(ICNo2) <> Trim(ICNo) Then
                If MsgBox("IC Numbers do not match. Proceed Anyway?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes Then
                    txtEmp.Text = ICNo
                    txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
                    txtRetTime.Text = Format(Date.Now, "HH:mm")
                    cmbAssortType.Focus()
                Else
                    txtEmp.Text = ""
                    Datavalid = False
                    ICNo = ""
                End If
            Else
                txtEmp.Text = ICNo
                txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
                txtRetTime.Text = Format(Date.Now, "HH:mm")
                If Section < 5 Then
                    cmbAssortType.Focus()
                Else
                    txtRetTap.Focus()
                End If
            End If
        ElseIf issued = True And Checked = False Then
            txtEmp.Text = ICNo
            txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
            txtRetTime.Text = Format(Date.Now, "HH:mm")
            cmbAssortType.Focus()
        Else
            txtEmp.Text = ICNo
            txtIssDate.Text = Format(Date.Now, "dd/MM/yyyy")
            txtIssTime.Text = Format(Date.Now, "HH:mm")
            cmbAssortType.Focus()
        End If
    End Sub

    Private Function ParcelFound(ByVal strDept As String, ByVal strParceNo As String) As Boolean
        ParcelFound = True
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblExpSizingPacket WHERE ParNo = '" & strParceNo & "' AND Department = '" & strDept & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount > 0 Then
            ParcelFound = True
        Else
            ParcelFound = False
        End If
        rsComSql_1 = Nothing
        Return ParcelFound
    End Function

    Private Sub ClearFields()

        txtParNo.Text = ""
        txtPktNo.Text = ""
        txtClarity.Text = ""
        txtEmp.Text = ""
        txtRetCts.Text = ""
        txtRetTap.Text = ""
        txtIssCts.Text = ""
        txtIssTap.Text = ""
        txtRej.Text = ""
        txtLostPcs.Text = ""
        txtIssDate.Text = ""
        txtIssTime.Text = ""
        txtRetTime.Text = ""
        txtRetDate.Text = ""
        txtRejCts.Text = ""
        txtLostCts.Text = ""

        txtRepPcs.Text = ""
        txtRepCts.Text = ""

        txtIncPcs.Text = "0"
        txtIncCts.Text = "0"
        txtNutPcs.Text = "0"
        txtNutCts.Text = "0"
        txtSizePcs.Text = "0"
        txtSizeCts.Text = "0"
        txtColPcs.Text = "0"
        txtColCts.Text = "0"

        cmbSection.SelectedIndex = 0

        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        txtTotEstCts.Text = "0"

        PictureBox1.Visible = False
        PictureBox2.Visible = False

        flxType.Rows.Clear()

        cmbAssortType.Text = ""
        txtAssortment.Text = ""
        txtMainAssortment.Text = ""
        txtPlanAssort.Text = ""

        lblTotPcs.Text = "0"
        lblTotCts.Text = "0"

        txtEstCts.Text = ""
        txtPlanValue.Text = ""
        txtValue.Text = "0"

        txtEmpNo.Text = ""
        txtEmpPcs.Text = ""
        txtNotOkPcs.Text = ""
        txtSize2Minus.Text = ""
        flxEmp.Rows.Clear()
        txtOrigin.Text = ""
        txtOCode.Text = ""

        chkCont.Checked = False
    End Sub

    Private Function CalTotalValue() As Double
        Dim intRow As Integer

        CalTotalValue = 0
        For intRow = 0 To flxType.Rows.Count - 1
            CalTotalValue = CalTotalValue + (CDbl(flxType.Item(2, intRow).Value) * CDbl(flxType.Item(3, intRow).Value))
        Next
        CalTotalValue = Math.Round(CalTotalValue, 2)
        Return CalTotalValue
    End Function

    Private Sub GetNewPacket()

        cmbSection.SelectedIndex = 0
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblExpSizingPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            txtIssTap.Text = rsComSql_1.Fields("PktPcs").Value
            txtIssCts.Text = Format(CDbl(rsComSql_1.Fields("PktCts").Value), "#0.000")

            PictureBox2.Visible = True
            PictureBox1.Visible = False

            issued = True
            Checked = False
        Else
            MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
            PictureBox2.Visible = False
            PictureBox1.Visible = False

            issued = False
            Checked = False
            txtParNo.Focus()
        End If
        rsComSql_1 = Nothing

    End Sub

    Private Sub Load_ParcelDetails()
        Dim intIssPcsB As Integer
        Dim intIssPcsC As Integer
        Dim intLastSec As Integer

        issued = True
        Checked = False
        intIssPcsB = 0
        intIssPcsC = 0

        txtOrigin.Text = ""
        txtOCode.Text = ""
        If cmbDept.Text = "Mix" Then
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT Origin FROM dbo.tblImport WHERE SupParcelNo = '" & txtParNo.Text & "'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                txtOrigin.Text = rsComSql_1.Fields("Origin").Value
            End If
            rsComSql_1 = Nothing
        Else
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT dbo.tblImportOGL.MiningCompany " & _
                            "FROM dbo.tblImport INNER JOIN dbo.tblParcel ON dbo.tblImport.SupParcelNo = dbo.tblParcel.OrigParcelNo INNER JOIN " & _
                                "dbo.tblImportOGL ON dbo.tblImport.NewLotNo = dbo.tblImportOGL.MasterLotID " & _
                            "WHERE (dbo.tblParcel.GrpParNo = '" & txtParNo.Text & "') AND (dbo.tblParcel.Depart = '" & cmbDept.Text & "')", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                txtOrigin.Text = rsComSql_1.Fields("MiningCompany").Value
            End If
            rsComSql_1 = Nothing
        End If

        If txtOrigin.Text <> "" Then
            If cmbDept.Text = "Mix" Then
                Select Case txtOrigin.Text
                    Case "De Beers"
                        txtOCode.Text = "ADT"
                    Case "Rio Tinto"
                        txtOCode.Text = "ART"
                    Case "Arctic Canadian"
                        txtOCode.Text = "AAC"
                    Case "Debswana"
                        txtOCode.Text = "AOD"
                    Case "Stargems Group"
                        txtOCode.Text = "ASG"
                End Select
            Else
                Select txtOrigin.Text
                    Case "DTC"
                        txtOCode.Text = "ADT"
                    Case "Rio Tinto"
                        txtOCode.Text = "ART"
                    Case "Arctic Canadian Diamond Company Ltd."
                        txtOCode.Text = "AAC"
                    Case "Debswana"
                        txtOCode.Text = "AOD"
                    Case "Stargems Group"
                        txtOCode.Text = "ASG"
                End Select
            End If
        End If

        If txtOCode.Text <> "" Then
            cmbAssortType.Text = txtOCode.Text & "01-"
        Else
            cmbAssortType.Text = ""
        End If

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblExpSizingPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            txtClarity.Text = rsComSql_1.Fields("ReturnType").Value

            cmbAssortType.Items.Clear()
            rsComSql_4 = New ADODB.Recordset
            rsComSql_4.Open("SELECT * FROM tblExpSizingAssort WHERE Clarity = '" & txtClarity.Text & "' ORDER BY AssortCode", AdoCN, 1, 1)
            If rsComSql_4.RecordCount Then
                rsComSql_4.MoveFirst()
                While Not rsComSql_4.EOF
                    cmbAssortType.Items.Add(rsComSql_4.Fields("AssortCode").Value)
                    rsComSql_4.MoveNext()
                End While
            End If
            rsComSql_4 = Nothing
        Else
            MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql_1 = Nothing

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblExpSizingIssues WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "' ORDER BY ID DESC", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            Checked = True
            intIssPcsC = rsComSql_1.Fields("IssPcs").Value
            cmbSection.SelectedIndex = rsComSql_1.Fields("Sec").Value - 1
            Section = rsComSql_1.Fields("Sec").Value
        Else
            PictureBox2.Visible = True
            txtIssDate.Text = Format(Date.Now, "dd/MM/yyyy")
            txtIssTime.Text = Format(Date.Now, "HH:mm")
            txtIssTap.Enabled = False
            txtIssCts.Enabled = False
            GetNewPacket()
        End If
        rsComSql_1 = Nothing

        'Plan Value
        txtPlanValue.Text = "0"
        If cmbDept.Text = "Baguettes" Then
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT dbo.tblExpSizingPacket.Department, dbo.tblExpSizingPacket.ParNo, dbo.tblExpSizingPacket.PktNo, dbo.tblExpSizingPacket.PktNo2, dbo.tblBAGPacket.PlanVal, dbo.tblBAGPacket.PktPcs, dbo.tblBAGPacket.PktCts " & _
                            "FROM dbo.tblExpSizingPacket INNER JOIN dbo.tblBAGPacket ON dbo.tblExpSizingPacket.ParNo = dbo.tblBAGPacket.ParNo AND dbo.tblExpSizingPacket.ReturnType = dbo.tblBAGPacket.PktNo " & _
                            "WHERE (dbo.tblExpSizingPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblExpSizingPacket.PktNo = '" & txtPktNo.Text & "')", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                txtPlanValue.Text = Math.Round(rsComSql_1.Fields("PlanVal").Value / rsComSql_1.Fields("PktPcs").Value, 2)
            End If
            rsComSql_1 = Nothing
        End If

        If cmbDept.Text = "Princess" Then
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT dbo.tblExpSizingPacket.Department, dbo.tblExpSizingPacket.ParNo, dbo.tblExpSizingPacket.PktNo, dbo.tblExpSizingPacket.PktNo2, dbo.tblPRPacket.PlanVal, dbo.tblPRPacket.PktPcs, dbo.tblPRPacket.PktCts " & _
                            "FROM dbo.tblExpSizingPacket INNER JOIN dbo.tblPRPacket ON dbo.tblExpSizingPacket.ParNo = dbo.tblPRPacket.ParNo AND dbo.tblExpSizingPacket.ReturnType = dbo.tblPRPacket.PktNo " & _
                            "WHERE (dbo.tblExpSizingPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblExpSizingPacket.PktNo = '" & txtPktNo.Text & "')", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                txtPlanValue.Text = Math.Round(rsComSql_1.Fields("PlanVal").Value / rsComSql_1.Fields("PktPcs").Value, 2)
            End If
            rsComSql_1 = Nothing
        End If

        If cmbDept.Text = "Carrer" Or cmbDept.Text = "Opening" Or cmbDept.Text = "Radiant" Or cmbDept.Text = "Asscher" Then
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT dbo.tblExpSizingPacket.Department, dbo.tblExpSizingPacket.ParNo, dbo.tblExpSizingPacket.PktNo, dbo.tblExpSizingPacket.ReturnType, dbo.tblExtPacket.PlanVal, dbo.tblExtPacket.PktPcs, dbo.tblExtPacket.PktCts " & _
                            "FROM dbo.tblExpSizingPacket INNER JOIN dbo.tblExtPacket ON dbo.tblExpSizingPacket.ParNo = dbo.tblExtPacket.ParNo AND dbo.tblExpSizingPacket.ReturnType = dbo.tblExtPacket.PktNo AND dbo.tblExpSizingPacket.Department = dbo.tblExtPacket.Department " & _
                            "WHERE (dbo.tblExpSizingPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblExpSizingPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblExpSizingPacket.PktNo = '" & txtPktNo.Text & "')", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                txtPlanValue.Text = Math.Round(rsComSql_1.Fields("PlanVal").Value / rsComSql_1.Fields("PktPcs").Value, 2)
            End If
            rsComSql_1 = Nothing
        End If

        If Section = 6 Then
            txtRetTap.ReadOnly = False
            txtRetCts.ReadOnly = False
        Else
            txtRetTap.ReadOnly = True
            txtRetCts.ReadOnly = True
        End If

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblExpSizingReturns WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "' ORDER BY ID DESC", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            intLastSec = rsComSql_1.Fields("Sec").Value
            MsgBox("Packet Finished", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
            Exit Sub
        End If
        rsComSql_1 = Nothing

        If issued = True And Checked = True Then
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblExpSizingReturns WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "' AND Sec = '" & Section & "'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                If intIssPcsC = rsComSql_1.Fields("RetPcs").Value + rsComSql_1.Fields("RepPcs").Value + rsComSql_1.Fields("LostPcs").Value + rsComSql_1.Fields("RejPcs").Value Then
                    If Section <> 2 Then
                        txtIssTap.Text = rsComSql_1.Fields("RetPcs").Value
                        txtIssCts.Text = rsComSql_1.Fields("RetCts").Value
                        txtIssTap.ReadOnly = False
                        txtIssCts.ReadOnly = False
                        txtIssDate.Text = Format(Date.Now, "dd/MM/yyyy")
                        txtIssTime.Text = Format(Date.Now, "HH:mm")
                        ICNo2 = ""
                        txtEmp.Text = ""
                        PictureBox2.Visible = True
                        PictureBox1.Visible = False
                        cmbSection.SelectedIndex = Section
                        Section = Section + 1
                        cmdEmp.Focus()
                        Checked = False

                    Else
                        MsgBox("Packet Finished", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        ClearFields()
                    End If
                End If
            Else
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblExpSizingIssues WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "' AND Sec = '" & Section & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    txtIssTap.Text = rsComSql_1.Fields("IssPcs").Value
                    txtIssCts.Text = rsComSql_1.Fields("IssCts").Value
                    txtIssTap.Enabled = False
                    txtIssCts.Enabled = False
                    txtIssDate.Text = Format(rsComSql_1.Fields("IssDate").Value, "dd/MM/yyyy")
                    txtIssTime.Text = Format(rsComSql_1.Fields("IssTime").Value, "HH:mm")
                    ICNo2 = rsComSql_1.Fields("EmpNo").Value
                    txtEmp.Text = rsComSql_1.Fields("EmpNo").Value

                    PictureBox2.Visible = True
                    PictureBox1.Visible = True
                    cmdEmp.Focus()
                End If
                rsComSql_1 = Nothing
            End If
            rsComSql_1 = Nothing
        End If
    End Sub

    Private Sub cmbAssortType_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbAssortType.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbAssortType.Text = UCase(cmbAssortType.Text)
            txtAssortment.Focus()
        End If
    End Sub

    Private Sub txtAssortment_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAssortment.KeyPress
        Dim strAssortment As String

        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If cmbAssortType.Text = "" Then
                MsgBox("Select the Assortment Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            strAssortment = cmbAssortType.Text & Trim(txtAssortment.Text)

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & strAssortment & "' AND Active = 1", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                'If Mid(strAssortment, 1, 1) = "A" Then
                '    rsComSql_1 = New ADODB.Recordset
                '    rsComSql_1.Open("SELECT * FROM tblExpSizingAssort WHERE Clarity = '" & txtClarity.Text & "' AND AssortCode = '" & Mid(strAssortment, 1, 3) & "'", AdoCN, 1, 1)
                '    If rsComSql_1.RecordCount = 0 Then
                '        MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                '        Exit Sub
                '    End If
                '    rsComSql_1 = Nothing
                'End If
                txtTypePcs.Focus()
            Else
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & strAssortment & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    txtTypePcs.Focus()
                Else
                    MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql_1 = Nothing
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub txtMainAssortment_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMainAssortment.KeyPress
        Dim strAssortment As String

        If Asc(e.KeyChar) = 13 Then
            txtMainAssortment.Text = UCase(Trim(txtMainAssortment.Text))
            strAssortment = txtMainAssortment.Text

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & strAssortment & "' AND Active = 1", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Mid(strAssortment, 1, 1) = "A" Then
                    'rsComSql_1 = New ADODB.Recordset
                    'rsComSql_1.Open("SELECT * FROM tblExpSizingAssort WHERE Clarity = '" & txtClarity.Text & "' AND AssortCode = '" & Mid(strAssortment, 1, 3) & "'", AdoCN, 1, 1)
                    'If rsComSql_1.RecordCount = 0 Then
                    '    MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    '    Exit Sub
                    'End If
                    'rsComSql_1 = Nothing
                End If
                txtTypePcs.Focus()
            Else
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & strAssortment & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    txtTypePcs.Focus()
                Else
                    MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql_1 = Nothing
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub txtPlanAssort_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPlanAssort.KeyPress
        Dim strPlanAssortment As String

        If Asc(e.KeyChar) = 13 Then
            txtPlanAssort.Text = UCase(Trim(txtPlanAssort.Text))
            strPlanAssortment = txtPlanAssort.Text

            If txtPlanAssort.Text <> "" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & strPlanAssortment & "' AND Active = 1", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Mid(strPlanAssortment, 1, 1) = "A" Then

                    End If
                    cmdAdd.Focus()
                Else
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & strPlanAssortment & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        cmdAdd.Focus()
                    Else
                        MsgBox("Invalid Plan Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql_1 = Nothing
                End If
                rsComSql = Nothing
            Else
                cmdAdd.Focus()
            End If
        End If
    End Sub

    Private Sub txtTypePcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTypePcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtTypeCts.Focus()
        End If
    End Sub

    Private Sub txtTypeCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTypeCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtTypeCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtEstCts.Text = txtTypeCts.Text
            txtEstCts.Focus()
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim intRow As Integer
        Dim intTotPcs As Integer
        Dim dblTotCts As Double
        Dim dblTotEstCts As Double
        Dim strAssortment As String
        Dim strPlanAssortment As String

        Dim dblPrice As Double

        Dim dblPlnPrice As Double
        Dim dblStonePrice As Double

        strAssortment = ""
        strPlanAssortment = ""
        If txtRetDate.Text = "" Then
            MsgBox("Please Enter the Employee No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If cmbAssortType.Text = "" And txtMainAssortment.Text = "" Then
            MsgBox("Please select the Assortment Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtAssortment.Text <> "" And txtMainAssortment.Text <> "" Then
            MsgBox("Invalid Assortment Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtAssortment.Text = "" And txtMainAssortment.Text = "" Then
            MsgBox("Please enter the Assortment Size", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtAssortment.Text <> "" And txtMainAssortment.Text <> "" Then
            MsgBox("Invalid Assortment Size", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If cmbAssortType.Text <> "" And txtAssortment.Text <> "" Then
            strAssortment = cmbAssortType.Text & txtAssortment.Text
        Else
            If txtMainAssortment.Text <> "" Then
                strAssortment = UCase(Trim(txtMainAssortment.Text))
            End If
        End If
        If strAssortment = "" Then
            MsgBox("Please check the input entries", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        strPlanAssortment = UCase(txtPlanAssort.Text)

        strAssortment = UCase(strAssortment)
        strPlanAssortment = UCase(strPlanAssortment)

        dblPrice = 0
        dblStonePrice = 0

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & Trim(strAssortment) & "' AND Active = 1", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            dblStonePrice = rsComSql.Fields("StonePrice").Value
            dblPrice = rsComSql.Fields("MarketPrice").Value

            If rsComSql.Fields("Origin").Value <> "" Then
                If txtOCode.Text <> Mid(strAssortment, 1, 3) Then
                    MsgBox("Invalid Assortment Origin", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
        Else
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & strAssortment & "'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                dblPrice = rsComSql_1.Fields("ListCost").Value
            Else
                MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql_1 = Nothing
        End If
        rsComSql = Nothing

        If Mid(strAssortment, 1, 1) = "S" Or Mid(strAssortment, 1, 1) = "R" Or Mid(strAssortment, 7, 1) = "R" Or Mid(strAssortment, 7, 1) = "S" Then
            If txtPlanValue.Text <> "" Then
                If strRight(txtParNo.Text, 1) = "C" Then
                    dblStonePrice = CDbl(txtPlanValue.Text) * 1.15
                Else
                    dblStonePrice = CDbl(txtPlanValue.Text)
                End If
                dblPrice = Math.Round((dblStonePrice * CInt(txtTypePcs.Text)) / CDbl(txtTypeCts.Text), 2)
            End If
        End If

        dblPlnPrice = 0

        If txtPlanAssort.Text <> "" Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & Trim(strPlanAssortment) & "' AND Active = 1", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                dblPlnPrice = rsComSql.Fields("MarketPrice").Value
            Else
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & strPlanAssortment & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    dblPlnPrice = rsComSql_1.Fields("ListCost").Value
                Else
                    MsgBox("Invalid Plan Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql_1 = Nothing
            End If
            rsComSql = Nothing
        End If

        If txtTypePcs.Text <> "" And txtTypeCts.Text <> "" Then
            If CInt(txtTypePcs.Text) > 0 Then
                'For intRow = 0 To flxType.Rows.Count - 1
                '    If strAssortment = flxType.Item(0, intRow).Value Then
                '        MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                '        Exit Sub
                '    End If
                'Next

                intTotPcs = 0
                dblTotCts = 0
                dblTotEstCts = 0
                For intRow = 0 To flxType.Rows.Count - 1
                    intTotPcs = intTotPcs + CInt(flxType.Item(1, intRow).Value)
                    dblTotCts = dblTotCts + CDbl(flxType.Item(2, intRow).Value)
                    dblTotEstCts = dblTotEstCts + CDbl(flxType.Item(6, intRow).Value)
                Next

                If txtRetTap.Text = "" Then txtRetTap.Text = "0"
                If txtRetCts.Text = "" Then txtRetCts.Text = "0"
                If txtRej.Text = "" Then txtRej.Text = "0"
                If txtRejCts.Text = "" Then txtRejCts.Text = "0"
                If txtLostPcs.Text = "" Then txtLostPcs.Text = "0"
                If txtLostCts.Text = "" Then txtLostCts.Text = "0"
                If txtRepPcs.Text = "" Then txtRepPcs.Text = "0"
                If txtRepCts.Text = "" Then txtRepCts.Text = "0"
                If txtIncPcs.Text = "" Then txtIncPcs.Text = "0"
                If txtIncCts.Text = "" Then txtIncCts.Text = "0"
                If txtNutPcs.Text = "" Then txtNutPcs.Text = "0"
                If txtNutCts.Text = "" Then txtNutCts.Text = "0"
                If txtSizePcs.Text = "" Then txtSizePcs.Text = "0"
                If txtSizeCts.Text = "" Then txtSizeCts.Text = "0"
                If txtColPcs.Text = "" Then txtColPcs.Text = "0"
                If txtColCts.Text = "" Then txtColCts.Text = "0"
                If txtNotOkPcs.Text = "" Then txtNotOkPcs.Text = "0"
                If txtSize2Minus.Text = "" Then txtSize2Minus.Text = "0"

                txtRej.Text = CInt(txtIncPcs.Text) + CInt(txtNutPcs.Text) + CInt(txtSizePcs.Text) + CInt(txtColPcs.Text)
                txtRejCts.Text = Math.Round(CSng(txtIncCts.Text) + CSng(txtNutCts.Text) + CSng(txtSizeCts.Text) + CSng(txtColCts.Text), 3)

                If intTotPcs + CInt(txtTypePcs.Text) + CInt(txtRej.Text) + CInt(txtLostPcs.Text) + CInt(txtRepPcs.Text) > CInt(txtIssTap.Text) Then
                    MsgBox("Pcs Invalid", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If Math.Round(dblTotCts + CDbl(txtTypeCts.Text) + CDbl(txtRejCts.Text) + CDbl(txtLostCts.Text) + CDbl(txtRepCts.Text), 3) > Math.Round(CDbl(txtIssCts.Text), 3) + 0.15 Then
                    MsgBox("Cts Invalid", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If Math.Round(dblTotEstCts + CDbl(txtEstCts.Text) + CDbl(txtRejCts.Text) + CDbl(txtLostCts.Text) + CDbl(txtRepCts.Text), 3) > Math.Round(CDbl(txtIssCts.Text), 3) + 0.15 Then
                    MsgBox("Est Cts Invalid", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                'If Math.Round(dblTotCts + CDbl(txtTypeCts.Text) + CDbl(txtRejCts.Text) + CDbl(txtLostCts.Text) + CDbl(txtRepCts.Text), 3) < Math.Round(CDbl(txtIssCts.Text), 3) - 0.35 Then
                '    MsgBox("Cts Invalid", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                '    Exit Sub
                'End If

                flxType.Rows.Add(strAssortment,
                                 txtTypePcs.Text,
                                 Format(CDbl(txtTypeCts.Text), "#0.000"),
                                 Format(dblPrice, "#0.00"),
                                 strPlanAssortment,
                                 Format(dblPlnPrice, "#0.00"),
                                 Format(CDbl(txtEstCts.Text), "#0.000"), 1)

                txtTotPcs.Text = CInt(txtTotPcs.Text) + CInt(txtTypePcs.Text)
                txtTotCts.Text = Format(CDbl(txtTotCts.Text) + CDbl(txtTypeCts.Text), "#0.000")
                txtTotEstCts.Text = Format(CDbl(txtTotEstCts.Text) + CDbl(txtEstCts.Text), "#0.000")

                txtRetTap.Text = txtTotPcs.Text
                txtRetCts.Text = txtTotCts.Text

                CalculateTotals()

                txtAssortment.Text = ""
                txtPlanAssort.Text = ""

                txtTypePcs.Text = ""
                txtTypeCts.Text = ""
                txtEstCts.Text = ""

                txtValue.Text = CalTotalValue()
            Else
                MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            MsgBox("Please check the input entries", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If

        txtAssortment.Focus()
    End Sub

    Private Sub CalculateTotals()
        If txtRetTap.Text = "" Then txtRetTap.Text = "0"
        If txtRetCts.Text = "" Then txtRetCts.Text = "0"
        If txtRej.Text = "" Then txtRej.Text = "0"
        If txtRejCts.Text = "" Then txtRejCts.Text = "0"
        If txtLostPcs.Text = "" Then txtLostPcs.Text = "0"
        If txtLostCts.Text = "" Then txtLostCts.Text = "0"
        If txtRepPcs.Text = "" Then txtRepPcs.Text = "0"
        If txtRepCts.Text = "" Then txtRepCts.Text = "0"

        If txtIncPcs.Text = "" Then txtIncPcs.Text = "0"
        If txtIncCts.Text = "" Then txtIncCts.Text = "0"
        If txtNutPcs.Text = "" Then txtNutPcs.Text = "0"
        If txtNutCts.Text = "" Then txtNutCts.Text = "0"
        If txtSizePcs.Text = "" Then txtSizePcs.Text = "0"
        If txtSizeCts.Text = "" Then txtSizeCts.Text = "0"
        If txtColPcs.Text = "" Then txtColPcs.Text = "0"
        If txtColCts.Text = "" Then txtColCts.Text = "0"
        If txtNotOkPcs.Text = "" Then txtNotOkPcs.Text = "0"
        If txtSize2Minus.Text = "" Then txtSize2Minus.Text = "0"

        txtRej.Text = CInt(txtIncPcs.Text) + CInt(txtNutPcs.Text) + CInt(txtSizePcs.Text) + CInt(txtColPcs.Text)
        txtRejCts.Text = Math.Round(CSng(txtIncCts.Text) + CSng(txtNutCts.Text) + CSng(txtSizeCts.Text) + CSng(txtColCts.Text), 3)

        lblTotPcs.Text = CInt(txtRetTap.Text) + CInt(txtRej.Text) + CInt(txtLostPcs.Text) + CInt(txtRepPcs.Text)
        lblTotCts.Text = Format(CDbl(txtRetCts.Text) + CDbl(txtRejCts.Text) + CDbl(txtLostCts.Text) + CDbl(txtRepCts.Text), "#0.000")
    End Sub

    Private Sub flxType_DoubleClick(sender As Object, e As EventArgs) Handles flxType.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            txtTotPcs.Text = CDbl(txtTotPcs.Text) - CDbl(flxType.Item(1, flxType.CurrentRow.Index).Value)
            txtTotCts.Text = Format(CDbl(txtTotCts.Text) - CDbl(flxType.Item(2, flxType.CurrentRow.Index).Value), "#0.000")
            txtTotEstCts.Text = Format(CDbl(txtTotEstCts.Text) - CDbl(flxType.Item(6, flxType.CurrentRow.Index).Value), "#0.000")

            txtRetTap.Text = txtTotPcs.Text
            txtRetCts.Text = txtTotCts.Text
            flxType.Rows.RemoveAt(flxType.CurrentRow.Index)
            CalculateTotals()
        End If
    End Sub

    Private Sub RecSave()
        Dim dataok As Boolean
        Dim stiss, stret As Integer
        Dim intRow As Integer
        Dim intTotPcs As Integer
        Dim dblTotCts As Double
        Dim dblPerc As Double
        Dim dblPlanValue As Double

        dataok = True
        If cmbDept.Text = "" Then
            MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If txtEmp.Text = "" Then
            MsgBox("Invalid Emp. No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If

        If Len(Trim(txtEmp.Text)) <> 6 Then
            MsgBox("Invalid Emp. No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If

        If txtParNo.Text = "" Then
            MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If txtPktNo.Text = "" Then
            MsgBox("Invalid Packet No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        dataok = True
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblExpSizingPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount = 0 Then
            MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
            dataok = False
        End If
        rsComSql_1 = Nothing
        If dataok = False Then Exit Sub

        If issued = True And Checked = False Then
            If txtIssTap.Text = "" Then
                MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub

            If txtIssCts.Text = "" Then
                MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub

            If CInt(txtIssTap.Text) <= 0 Then
                MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub

            If CSng(txtIssCts.Text) <= 0 Then
                MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub

            If Len(txtIssDate.Text) < 2 Then Exit Sub


        ElseIf issued = True And Checked = True Then
            If txtRetTap.Text = "" Then txtRetTap.Text = "0"
            If txtRetCts.Text = "" Then txtRetCts.Text = "0"
            If txtRej.Text = "" Then txtRej.Text = "0"
            If txtRejCts.Text = "" Then txtRejCts.Text = "0"
            If txtLostPcs.Text = "" Then txtLostPcs.Text = "0"
            If txtLostCts.Text = "" Then txtLostCts.Text = "0"
            If txtRepPcs.Text = "" Then txtRepPcs.Text = "0"
            If txtRepCts.Text = "" Then txtRepCts.Text = "0"
            If txtIncPcs.Text = "" Then txtIncPcs.Text = "0"
            If txtIncCts.Text = "" Then txtIncCts.Text = "0"
            If txtNutPcs.Text = "" Then txtNutPcs.Text = "0"
            If txtNutCts.Text = "" Then txtNutCts.Text = "0"
            If txtSizePcs.Text = "" Then txtSizePcs.Text = "0"
            If txtSizeCts.Text = "" Then txtSizeCts.Text = "0"
            If txtColPcs.Text = "" Then txtColPcs.Text = "0"
            If txtColCts.Text = "" Then txtColCts.Text = "0"
            If txtNotOkPcs.Text = "" Then txtNotOkPcs.Text = "0"
            If txtSize2Minus.Text = "" Then txtSize2Minus.Text = "0"

            txtRej.Text = CInt(txtIncPcs.Text) + CInt(txtNutPcs.Text) + CInt(txtSizePcs.Text) + CInt(txtColPcs.Text)
            txtRejCts.Text = Math.Round(CSng(txtIncCts.Text) + CSng(txtNutCts.Text) + CSng(txtSizeCts.Text) + CSng(txtColCts.Text), 3)

            stiss = CInt(txtIssTap.Text)
            stret = CInt(txtRetTap.Text) + CInt(txtRej.Text) + CInt(txtLostPcs.Text) + CInt(txtRepPcs.Text)
            If stiss <> stret Then
                strMsg = "Stones Issued " & stiss & "   Stones Returned " & stret
                MsgBox(strMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
                If dataok = False Then Exit Sub
            End If

            'ciss = CSng(txtIssCts.Text)
            'cret = Math.Round(CSng(txtRetCts.Text) + CSng(txtRejCts.Text) + CSng(txtLostCts.Text) + CSng(txtRepCts.Text), 3)
            'If ciss < cret Then
            '    strMsg = "Carets issued " & Format(ciss, "##.###") & "   Carets returned " & Format(cret, "##.###")
            '    MsgBox(strMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '    dataok = False
            '    If dataok = False Then Exit Sub
            'End If

            If Section < 6 Then
                intTotPcs = 0
                dblTotCts = 0
                For intRow = 0 To flxType.Rows.Count - 1
                    intTotPcs = intTotPcs + CInt(flxType.Item(1, intRow).Value)
                    dblTotCts = dblTotCts + CDbl(flxType.Item(2, intRow).Value)
                Next

                If CInt(txtIssTap.Text) <> intTotPcs + CInt(txtRepPcs.Text) + CInt(txtRej.Text) + CInt(txtLostPcs.Text) Then
                    MsgBox("Pcs not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    dataok = False
                    If dataok = False Then Exit Sub
                End If
                If cmbDept.Text = "Mix" Then
                    If CDbl(txtIssCts.Text) <> Math.Round(Math.Round(dblTotCts, 3) + CSng(txtRepCts.Text) + CSng(txtRejCts.Text) + CSng(txtLostCts.Text), 3) Then
                        MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        dataok = False
                        If dataok = False Then Exit Sub
                    End If
                Else
                    If CDbl(txtIssCts.Text) + 0.05 < Math.Round(Math.Round(dblTotCts, 3) + CSng(txtRepCts.Text) + CSng(txtRejCts.Text) + CSng(txtLostCts.Text), 3) Then
                        MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        dataok = False
                        If dataok = False Then Exit Sub
                    End If
                    If CDbl(txtIssCts.Text) - 0.05 > Math.Round(Math.Round(dblTotCts, 3) + CSng(txtRepCts.Text) + CSng(txtRejCts.Text) + CSng(txtLostCts.Text), 3) Then
                        MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        dataok = False
                        If dataok = False Then Exit Sub
                    End If
                End If

                intTotPcs = 0
                For intRow = 0 To flxEmp.Rows.Count - 1
                    intTotPcs = intTotPcs + CDbl(flxEmp.Item(1, intRow).Value)
                Next

                If intTotPcs = 0 Then
                    flxEmp.Rows.Clear()
                    flxEmp.Rows.Add(txtEmp.Text, txtIssTap.Text)
                Else
                    If CInt(txtIssTap.Text) <> intTotPcs Then
                        MsgBox("Pcs not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        dataok = False
                        If dataok = False Then Exit Sub
                    End If
                End If

                For intRow = 0 To flxType.Rows.Count - 1
                    If (CInt(flxType.Item(1, intRow).Value) > 0 And CDbl(flxType.Item(2, intRow).Value) <= 0) Or (CInt(flxType.Item(1, intRow).Value) <= 0 And CDbl(flxType.Item(2, intRow).Value) > 0) Then
                        MsgBox("Invalid Data in " & flxType.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        dataok = False
                        If dataok = False Then Exit Sub
                    End If
                Next

                'Check Plan Value
                dblPerc = 0
                dblPlanValue = CDbl(txtPlanValue.Text) * CDbl(txtTotPcs.Text)
                If chkCont.Checked = False Then
                    If dblPlanValue > CDbl(txtValue.Text) Then
                        dblPerc = Math.Round((CDbl(txtValue.Text) / dblPlanValue) * 100, 2)
                        If dblPerc < 96 Then
                            MsgBox("Plan Value not Achieved - " & dblPerc & "%", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                    End If
                End If

                If CInt(txtNotOkPcs.Text) > CInt(txtRetTap.Text) Then
                    MsgBox("Invalid Not OK Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    dataok = False
                End If
                If dataok = False Then Exit Sub

                If CInt(txtSize2Minus.Text) > CInt(txtRetTap.Text) Then
                    MsgBox("Invalid Size -2 Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    dataok = False
                End If
                If dataok = False Then Exit Sub
            End If

        Else
            If Len(txtIssDate.Text) < 2 Then dataok = False
        End If

        If dataok = True Then
            DataSave()
        End If

    End Sub

    Private Sub DataSave()
        Dim strOPktNo As String

        dtpToday = GetToday()

        If issued = True And Checked = False Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblExpSizingIssues WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblExpSizingIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                              "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & UCase(txtPktNo.Text) & "'," & cmbSection.SelectedIndex + 1 & ",'" & Mid(Trim(txtEmp.Text), 1, 6) & "','" & CInt(txtIssTap.Text) & "'," & _
                                     "'" & CDbl(txtIssCts.Text) & "','" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")
            Else
                MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            rsComSql = Nothing

        ElseIf issued = True And Checked = True Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblExpSizingReturns WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblExpSizingReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts,SizePcs,SizeCts,ColPcs,ColCts,IncPcs,IncCts,NutPcs,NutCts,NotOkPcs,Size2MPcs) " & _
                              "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & UCase(txtPktNo.Text) & "'," & cmbSection.SelectedIndex + 1 & ",'" & Mid(Trim(txtEmp.Text), 1, 6) & "'," & CInt(txtRetTap.Text) & "," & CDbl(txtRetCts.Text) & "," & _
                                "" & CInt(txtLostPcs.Text) & "," & CDbl(txtLostCts.Text) & "," & CInt(txtRepPcs.Text) & "," & CDbl(txtRepCts.Text) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "'," & CInt(txtRej.Text) & "," & CDbl(txtRejCts.Text) & "," & _
                                "" & CInt(txtSizePcs.Text) & "," & CDbl(txtSizeCts.Text) & "," & CInt(txtColPcs.Text) & "," & CDbl(txtColCts.Text) & "," & CInt(txtIncPcs.Text) & "," & CDbl(txtIncCts.Text) & "," & CInt(txtNutPcs.Text) & "," & CDbl(txtNutCts.Text) & "," & CInt(txtNotOkPcs.Text) & "," & CInt(txtSize2Minus.Text) & ")")

                If CInt(txtRej.Text) > 0 Then
                    Call Dep_Grading_Trf(cmbDept.Text, 9995, txtParNo.Text, UCase(txtPktNo.Text), txtRej.Text, txtRejCts.Text, txtRej.Text, txtRejCts.Text, "")

                    strOPktNo = txtPktNo.Text
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT ReturnType FROM tblExpSizingPacket WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        strOPktNo = rsComSql_1.Fields("ReturnType").Value
                    End If
                    rsComSql_1 = Nothing

                    AdoCN.Execute("UPDATE tblGradingTrf SET OrderNo = '" & strOPktNo & "' WHERE Department = '" & cmbDept.Text & "' AND ParcelNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'")
                End If

                If CInt(txtRepPcs.Text) > 0 Then
                    'rsComSql_1 = New Recordset
                    'If cmbDept = "Mix" Then
                    '    rsComSql_1.Open("SELECT * FROM tblImport WHERE SupParcelNo = '" & txtParNo & "' AND SupplierRefNo NOT LIKE 'LCL%'", AdoCN,1,1)
                    'Else
                    '    rsComSql_1.Open("SELECT * FROM tblDep_Trf WHERE DCLParcelNo = '" & Left(txtParNo, 6) & "' AND Department = '" & cmbDept & "'", AdoCN,1,1)
                    'End If
                    'If rsComSql_1.RecordCount Then
                    '    AdoCN.Execute("INSERT INTO tblDep_Rec(Department,AssortmentNo,SupParcelNo,DCLParcelNo,Pcs,Cts,RghCts) " & _
                    '                  "VALUES('" & cmbDept.Text & "','" & rsComSql_1![AssortmentNo] & "','" & rsComSql_1![SupParcelNo] & "'," & _
                    '                    "'" & rsComSql_1![DclParcelNo] & "'," & CDbl(txtRepPcs.Text) & "," & CDbl(txtRepCts.Text) & "," & CDbl(txtRepCts.Text) & ")")

                    'End If
                    'rsComSql_1 = Nothing
                End If

                Save_GradingTypes(UCase(txtParNo.Text), UCase(txtPktNo.Text), cmbSection.SelectedIndex + 1)
                Save_GradingEmp(UCase(txtParNo.Text), UCase(txtPktNo.Text), cmbSection.SelectedIndex + 1)
            Else
                MsgBox("Already entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            rsComSql = Nothing
        End If
        ClearFields()

    End Sub

    Private Sub Save_GradingTypes(ByVal strParcelNo As String, ByVal strPktNo As String, ByVal intSection As Integer)
        Dim intRow As Integer

        AdoCN.Execute("DELETE FROM tblExpSizingTypes WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Sec = " & intSection & "")
        For intRow = 0 To flxType.Rows.Count - 1
            If flxType.Item(1, intRow).Value > 0 Then
                AdoCN.Execute("INSERT INTO tblExpSizingTypes(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts,OK,BasePrice,PlanAssort,PlanBasePrice,EstCts) " & _
                              "VALUES('" & cmbDept.Text & "','" & strParcelNo & "','" & strPktNo & "'," & intSection & ",'" & flxType.Item(0, intRow).Value & "'," & _
                                "" & CInt(flxType.Item(1, intRow).Value) & "," & CDbl(flxType.Item(2, intRow).Value) & ",0," & CDbl(flxType.Item(3, intRow).Value) & "," & _
                                "'" & flxType.Item(4, intRow).Value & "'," & CDbl(flxType.Item(5, intRow).Value) & "," & CDbl(flxType.Item(6, intRow).Value) & ")")
            End If
        Next

    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        RecSave()
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtParNo.Text = UCase(txtParNo.Text)
            If ParcelFound(cmbDept.Text, txtParNo.Text) = True Then
                cmbSection.SelectedIndex = 0
                txtPktNo.Text = ""
                txtPktNo.Focus()
            Else
                MsgBox("Invalid Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtParNo.Text = ""
                txtPktNo.Text = ""
                txtParNo.Focus()
            End If
        End If
    End Sub

    Private Sub txtPktNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Load_Packet()
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub txtRej_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRej.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            CalculateTotals()
            txtRejCts.Focus()
        End If
    End Sub

    Private Sub txtRejCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRejCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRejCts.Text)
        If Asc(e.KeyChar) = 13 Then
            CalculateTotals()
            txtLostPcs.Focus()
        End If
    End Sub

    Private Sub txtLostPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLostPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            CalculateTotals()
            txtLostCts.Focus()
        End If
    End Sub

    Private Sub txtLostCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLostCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtLostCts.Text)
        If Asc(e.KeyChar) = 13 Then
            CalculateTotals()
            txtRepPcs.Focus()
        End If
    End Sub

    Private Sub txtRepPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRepPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            CalculateTotals()
            txtRepCts.Focus()
        End If
    End Sub

    Private Sub txtRepCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRepCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRepCts.Text)
        If Asc(e.KeyChar) = 13 Then
            CalculateTotals()
        End If
    End Sub

    Private Sub txtNonPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSizePcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            CalculateTotals()
            txtSizeCts.Focus()
        End If
    End Sub

    Private Sub txtNonCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSizeCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtSizeCts.Text)
        If Asc(e.KeyChar) = 13 Then
            CalculateTotals
            txtColPcs.Focus()
        End If
    End Sub

    Private Sub txtRefPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtColPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            CalculateTotals
            txtColCts.Focus()
        End If
    End Sub

    Private Sub txtRefCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtColCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtColCts.Text)
        If Asc(e.KeyChar) = 13 Then
            CalculateTotals
            txtIncPcs.Focus()
        End If
    End Sub

    Private Sub txtIncPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIncPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            CalculateTotals
            txtIncCts.Focus()
        End If
    End Sub

    Private Sub txtIncCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIncCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtIncCts.Text)
        If Asc(e.KeyChar) = 13 Then
            CalculateTotals
            txtNutPcs.Focus()
        End If
    End Sub

    Private Sub txtBurnPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNutPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            CalculateTotals
            txtNutCts.Focus()
        End If
    End Sub

    Private Sub txtBurnCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNutCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtNutCts.Text)
        If Asc(e.KeyChar) = 13 Then
            CalculateTotals
        End If
    End Sub

    Private Sub txtEstCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEstCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtEstCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtPlanAssort.Focus()
        End If
    End Sub

    Private Sub txtEmpNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEmpNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtEmpPcs.Focus()
        End If
    End Sub

    Private Sub txtEmpPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEmpPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            cmdEmpAdd.Focus()
        End If
    End Sub

    Private Sub cmdEmpAdd_Click(sender As Object, e As EventArgs) Handles cmdEmpAdd.Click
        Dim intRow As Integer
        Dim dblTotPcs As Double

        If txtEmpNo.Text <> "" And txtEmpPcs.Text <> "" Then
            If Len(txtEmpNo.Text) <> 6 Then MsgBox("Invalid Emp No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL3 WHERE (DepartmentName LIKE 'GRADING%' OR DepartmentName LIKE 'PCU%') AND (Pay = 1) AND (FullEmpNo = '" & txtEmpNo.Text & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                cmdEmp.Focus()
                Exit Sub
            End If
            rsComSql = Nothing

            txtEmpNo.Text = UCase(txtEmpNo.Text)
            If CDbl(txtEmpPcs.Text) > 0 Then
                For intRow = 1 To flxEmp.Rows.Count - 1
                    If txtEmpNo.Text = flxEmp.Item(0, intRow).Value Then
                        MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                Next

                dblTotPcs = 0
                For intRow = 0 To flxEmp.Rows.Count - 1
                    dblTotPcs = dblTotPcs + CDbl(flxEmp.Item(1, intRow).Value)
                Next

                If CDbl(txtIssTap.Text) >= dblTotPcs + CDbl(txtEmpPcs.Text) Then
                    flxEmp.Rows.Add(txtEmpNo.Text,
                                    txtEmpPcs.Text)

                    txtEmpNo.Text = ""
                    txtEmpPcs.Text = ""

                    txtEmpNo.Focus()
                Else
                    MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Else
                MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            MsgBox("Invalid Emp. No./Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub flxEmp_DoubleClick(sender As Object, e As EventArgs) Handles flxEmp.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxEmp.Rows.RemoveAt(flxEmp.CurrentRow.Index)
        End If
    End Sub

    Private Sub Save_GradingEmp(ByVal strParcelNo As String, ByVal strPktNo As String, ByVal intSection As Integer)
        Dim intRow As Integer

        dtpToday = GetToday()
        AdoCN.Execute("DELETE FROM tblExpSizingReturnsEmp WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Sec = " & intSection & "")
        For intRow = 0 To flxEmp.Rows.Count - 1
            AdoCN.Execute("INSERT INTO tblExpSizingReturnsEmp(Department,ParNo,PktNo,Sec,EmpNo,Pcs,RetDate) " & _
                          "VALUES('" & cmbDept.Text & "','" & strParcelNo & "','" & strPktNo & "'," & intSection & ",'" & flxEmp.Item(0, intRow).Value & "'," & CDbl(flxEmp.Item(1, intRow).Value) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "')")
        Next

    End Sub

    Private Sub txtNotOkPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNotOkPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub txtSize2Minus_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSize2Minus.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub
End Class