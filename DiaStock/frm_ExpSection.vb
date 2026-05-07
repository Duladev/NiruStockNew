
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_ExpSection
    Dim issued As Boolean
    Dim Checked As Boolean
    Dim ICNo2 As String
    Dim Section As Integer
    Dim strMsg As String

    Private Sub frm_ExpSection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        PictureBox1.Visible = False
        PictureBox2.Visible = False

        Load_Section()
        Load_Department(cmbDept)
        Load_GradingTypes(1)
        ClearFields()
    End Sub

    Private Sub Load_Section()
        Dim recsection As ADODB.Recordset

        cmbSection.Items.Clear()
        recsection = New ADODB.Recordset
        recsection.Open("SELECT * FROM tblExpSections WHERE Seq < 5 ORDER BY Seq", AdoCN, 1, 1)
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

    Private Sub Load_GradingTypes(ByVal intSec As Integer)
        Dim rsGrdType As New ADODB.Recordset

        cmbType1.Items.Clear()
        rsGrdType = New ADODB.Recordset
        rsGrdType.Open("SELECT * FROM tblExpTypes WHERE Sec = " & intSec & " ORDER BY Type", AdoCN, 1, 1)
        If rsGrdType.RecordCount Then
            rsGrdType.MoveFirst()
            While Not rsGrdType.EOF
                cmbType1.Items.Add(rsGrdType.Fields("Type").Value)
                rsGrdType.MoveNext()
            End While
        End If
        rsGrdType = Nothing

    End Sub

    Private Sub ClearFields()

        txtParNo.Text = ""
        txtPktNo.Text = ""
        txtEmp.Text = ""
        txtRetTap.Text = ""
        txtRetCts.Text = ""
        txtIssTap.Text = ""
        txtIssCts.Text = ""
        txtRej.Text = ""
        txtRejCts.Text = ""
        txtCRejPcs.Text = ""
        txtCRejCts.Text = ""
        txtLostPcs.Text = ""
        txtLostCts.Text = ""
        txtIssDate.Text = ""
        txtIssTime.Text = ""
        txtRetTime.Text = ""
        txtRetDate.Text = ""

        txtRepPcs.Text = ""
        txtRepCts.Text = ""

        txtColPcs.Text = ""
        txtColCts.Text = ""
        txtFloPcs.Text = ""
        txtFloCts.Text = ""
        txtIncPcs.Text = ""
        txtIncCts.Text = ""
        txtBurnPcs.Text = ""
        txtBurnCts.Text = ""
        txtNonPcs.Text = ""
        txtNonCts.Text = ""
        txtRefPcs.Text = ""
        txtRefCts.Text = ""

        cmbSection.SelectedIndex = 0
        chkRough.Checked = False

        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"

        cmbType1.Text = ""
        txtTypePcs.Text = ""
        txtTypeCts.Text = ""
        flxType.Rows.Clear()
        flxReturns.Rows.Clear()

        txtEmpNo.Text = ""
        txtEmpPcs.Text = ""
        flxEmp.Rows.Clear()

        PictureBox1.Visible = False

    End Sub

    Private Sub cmbSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSection.SelectedIndexChanged
        cmbType1.Text = ""
        flxType.Rows.Clear()
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        txtTypePcs.Text = ""
        txtTypeCts.Text = ""
    End Sub

    Private Function ParcelFound(ByVal strDept As String, ByVal strParceNo As String) As Boolean
        ParcelFound = True
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblExpPacket WHERE ParNo = '" & strParceNo & "' AND Department = '" & strDept & "' AND AMS2 = 1 AND YAH = 1", AdoCN, 1, 1)
        If rsComSql_1.RecordCount > 0 Then
            ParcelFound = True
        Else
            ParcelFound = False
        End If
        rsComSql_1 = Nothing
        Return ParcelFound
    End Function

    Private Function PacketFound(ByVal strDept As String, ByVal strParceNo As String, ByVal strPktNo As String) As Boolean
        PacketFound = True
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblExpPacket WHERE ParNo = '" & strParceNo & "' AND Department = '" & strDept & "' AND PktNo = '" & strPktNo & "' AND AMS2 = 1 AND YAH = 1", AdoCN, 1, 1)
        If rsComSql_1.RecordCount > 0 Then
            PacketFound = True
        Else
            PacketFound = False
        End If
        rsComSql_1 = Nothing
        Return PacketFound
    End Function

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

    Private Sub Load_Packet()
        If txtParNo.Text <> "" And txtPktNo.Text <> "" Then
            txtParNo.Text = UCase(txtParNo.Text)
            txtPktNo.Text = UCase(txtPktNo.Text)
            If PacketFound(cmbDept.Text, txtParNo.Text, txtPktNo.Text) = True Then
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

    Private Sub Load_ParcelDetails()
        Dim intIssPcsB As Integer
        Dim intIssPcsC As Integer
        Dim intLastSec As Integer

        issued = True
        Checked = False
        intIssPcsB = 0
        intIssPcsC = 0

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblExpIssues WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "' ORDER BY ID DESC", AdoCN, 1, 1)
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

        Load_GradingTypes(Section)

        If Section = 4 Then
            txtRetTap.ReadOnly = False
            txtRetCts.ReadOnly = False
        Else
            txtRetTap.ReadOnly = True
            txtRetCts.ReadOnly = True
        End If

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblExpReturns WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "' ORDER BY ID DESC", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            intLastSec = rsComSql_1.Fields("Sec").Value
        End If
        rsComSql_1 = Nothing

        If issued = True And Checked = True Then
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblExpReturns WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "' AND Sec = '" & Section & "'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                If intIssPcsC = rsComSql_1.Fields("RetPcs").Value + rsComSql_1.Fields("RepPcs").Value + rsComSql_1.Fields("LostPcs").Value + rsComSql_1.Fields("RejPcs").Value Then
                    If Section <> 3 Then
                        txtIssTap.Text = rsComSql_1.Fields("RetPcs").Value
                        txtIssCts.Text = rsComSql_1.Fields("RetCts").Value
                        txtIssTap.Enabled = False
                        txtIssCts.Enabled = False
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
                rsComSql_1.Open("SELECT * FROM tblExpIssues WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "' AND Sec = '" & Section & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    txtIssTap.Text = rsComSql_1.Fields("IssPcs").Value
                    txtIssCts.Text = rsComSql_1.Fields("IssCts").Value
                    txtIssTap.Enabled = False
                    txtIssCts.Enabled = False
                    txtIssDate.Text = Format(rsComSql_1.Fields("IssDate").Value, "dd/MM/yyyy")
                    txtIssTime.Text = Format(rsComSql_1.Fields("IssTime").Value, "HH:mm")
                    ICNo2 = rsComSql_1.Fields("EmpNo").Value
                    txtEmp.Text = rsComSql_1.Fields("EmpNo").Value

                    flxReturns.Rows.Clear()
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT * FROM tblExpReturnDetails WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "' AND Sec = " & intLastSec & " ORDER BY ID", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        rsComSql_2.MoveFirst()
                        While Not rsComSql_2.EOF
                            flxReturns.Rows.Add(rsComSql_2.Fields("ReturnType").Value,
                                                rsComSql_2.Fields("Pcs").Value,
                                                Format(rsComSql_2.Fields("Cts").Value, "#0.000"))

                            rsComSql_2.MoveNext()
                        End While
                    End If
                    rsComSql_2 = Nothing

                    PictureBox2.Visible = True
                    PictureBox1.Visible = True
                    cmdEmp.Focus()
                End If
                rsComSql_1 = Nothing
            End If
            rsComSql_1 = Nothing
        End If
    End Sub

    Private Sub GetNewPacket()

        cmbSection.SelectedIndex = 0
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblExpPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            txtIssTap.Text = rsComSql_1.Fields("PktPcs").Value
            txtIssCts.Text = Format(rsComSql_1.Fields("Pktcts").Value, "#0.000")

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

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        'Dim intPos As Integer

        Instring = UCase(InputBox("Enter Par/Pkt Number"))
        ParcelLen = Len(Instring)
        If ParcelLen > 9 Then
            Datavalid = True

            If ParcelLen = 10 Then
                txtParNo.Text = Mid(Instring, 1, ParcelLen - 3)
                txtPktNo.Text = strRight(Instring, 3)
            Else
                txtParNo.Text = Mid(Instring, 1, ParcelLen - 4)
                txtPktNo.Text = strRight(Instring, 4)
            End If

            txtPktNo.Focus()

            Load_Packet()
        Else
            txtParPkt.Text = ""
        End If

        'If Len(Instring) > 0 Then
        '    txtParPkt.Text = Trim(Instring)
        '    intPos = InStr(1, txtParPkt.Text, "/")
        '    If intPos > 0 Then
        '        txtParNo.Text = Mid(txtParPkt.Text, 1, intPos - 1)
        '        txtPktNo.Text = Mid(txtParPkt.Text, intPos + 1, Len(txtParPkt.Text) - intPos)
        '        txtPktNo.Focus()

        '        Load_Packet()
        '    End If
        'Else
        '    txtParPkt.Text = ""
        'End If
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
                    cmbType1.Focus()
                Else
                    txtRetTap.Focus()
                End If
            End If
        ElseIf issued = True And Checked = False Then
            txtEmp.Text = ICNo
            txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
            txtRetTime.Text = Format(Date.Now, "HH:mm")
        Else
            txtEmp.Text = ICNo
            txtIssDate.Text = Format(Date.Now, "dd/MM/yyyy")
            txtIssTime.Text = Format(Date.Now, "HH:mm")
            txtIssTap.Focus()
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub RecSave()
        Dim dataok As Boolean
        Dim stiss, stret As Integer
        Dim ciss, cret As Single
        Dim intRow As Integer
        Dim intTotPcs As Double
        Dim dblTotCts As Double

        dataok = True
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblExpPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount = 0 Then
            MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
            dataok = False
        End If
        rsComSql_1 = Nothing
        If dataok = False Then Exit Sub

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
            If txtColPcs.Text = "" Then txtColPcs.Text = "0"
            If txtColCts.Text = "" Then txtColCts.Text = "0"
            If txtFloPcs.Text = "" Then txtFloPcs.Text = "0"
            If txtFloCts.Text = "" Then txtFloCts.Text = "0"
            If txtIncPcs.Text = "" Then txtIncPcs.Text = "0"
            If txtIncCts.Text = "" Then txtIncCts.Text = "0"
            If txtBurnPcs.Text = "" Then txtBurnPcs.Text = "0"
            If txtBurnCts.Text = "" Then txtBurnCts.Text = "0"
            If txtNonPcs.Text = "" Then txtNonPcs.Text = "0"
            If txtNonCts.Text = "" Then txtNonCts.Text = "0"
            If txtRefPcs.Text = "" Then txtRefPcs.Text = "0"
            If txtRefCts.Text = "" Then txtRefCts.Text = "0"
            If txtCRejPcs.Text = "" Then txtCRejPcs.Text = "0"
            If txtCRejCts.Text = "" Then txtCRejCts.Text = "0"

            If chkRough.Checked = True Then
                txtRepPcs.Text = CInt(txtColPcs.Text) + CInt(txtFloPcs.Text) + CInt(txtIncPcs.Text) + CInt(txtBurnPcs.Text) + CInt(txtNonPcs.Text) + CInt(txtRefPcs.Text)
                txtRepCts.Text = Math.Round(CSng(txtColCts.Text) + CSng(txtFloCts.Text) + CSng(txtIncCts.Text) + CSng(txtBurnCts.Text) + CSng(txtNonCts.Text) + CSng(txtRefCts.Text), 3)
            Else
                txtRej.Text = CInt(txtColPcs.Text) + CInt(txtFloPcs.Text) + CInt(txtIncPcs.Text) + CInt(txtBurnPcs.Text) + CInt(txtNonPcs.Text) + CInt(txtRefPcs.Text)
                txtRejCts.Text = Math.Round(CSng(txtColCts.Text) + CSng(txtFloCts.Text) + CSng(txtIncCts.Text) + CSng(txtBurnCts.Text) + CSng(txtNonCts.Text) + CSng(txtRefCts.Text), 3)
            End If

            If cmbSection.SelectedIndex + 1 <> 3 Then
                If CInt(txtRej.Text) <> 0 Or CInt(txtRepPcs.Text) <> 0 Then
                    MsgBox("Invalid Rejects", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    dataok = False
                    If dataok = False Then Exit Sub
                End If
            End If

            stiss = CInt(txtIssTap.Text)
            stret = CInt(txtRetTap.Text) + CInt(txtRej.Text) + CInt(txtLostPcs.Text) + CInt(txtRepPcs.Text) + CInt(txtCRejPcs.Text)
            If stiss <> stret Then
                strMsg = "Stones Issued " & stiss & "   Stones Returned " & stret
                MsgBox(strMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
                If dataok = False Then Exit Sub
            End If

            ciss = Math.Round(CSng(txtIssCts.Text), 3)
            cret = Math.Round(CSng(txtRetCts.Text) + CSng(txtRejCts.Text) + CSng(txtLostCts.Text) + CSng(txtRepCts.Text) + CSng(txtCRejCts.Text), 3)
            If ciss < cret Then
                strMsg = "Carets issued " & Format(ciss, "##.###") & "   Carets returned " & Format(cret, "##.###")
                MsgBox(strMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
                If dataok = False Then Exit Sub
            End If

            If Section < 4 Then
                intTotPcs = 0
                dblTotCts = 0
                For intRow = 0 To flxType.Rows.Count - 1
                    intTotPcs = intTotPcs + CDbl(flxType.Item(1, intRow).Value)
                    dblTotCts = dblTotCts + CDbl(flxType.Item(2, intRow).Value)
                Next

                If CInt(txtIssTap.Text) <> intTotPcs + CInt(txtRepPcs.Text) + CInt(txtRej.Text) + CInt(txtLostPcs.Text) + CInt(txtCRejPcs.Text) Then
                    MsgBox("Pcs not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    dataok = False
                    If dataok = False Then Exit Sub
                End If
                dblTotCts = Math.Round(dblTotCts, 3)
                If Math.Round(CDbl(txtIssCts.Text), 3) <> Math.Round(Math.Round(dblTotCts, 3) + Math.Round(Val(txtRepCts.Text), 3) + Math.Round(Val(txtRejCts.Text), 3) + Math.Round(Val(txtLostCts.Text), 3) + Math.Round(Val(txtCRejCts.Text), 3), 3) Then
                    MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    dataok = False
                    If dataok = False Then Exit Sub
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
            End If

        Else
            If Len(txtIssDate.Text) < 2 Then dataok = False
        End If

        If dataok = True Then
            DataSave()
        End If

    End Sub

    Private Sub DataSave()
        Dim dblTrfPcs As Double
        Dim dblTrfCts As Double

        dtpToday = GetToday()
        If issued = True And Checked = False Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblExpIssues WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblExpIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                              "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & UCase(txtPktNo.Text) & "'," & cmbSection.SelectedIndex + 1 & ",'" & Mid(Trim(txtEmp.Text), 1, 6) & "'," & CInt(txtIssTap.Text) & "," & _
                                     "" & CDbl(txtIssCts.Text) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")
            Else
                MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            rsComSql = Nothing

        ElseIf issued = True And Checked = True Then
            If cmbDept.Text = "Mix" Then
                PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
                If PBResponse = MsgBoxResult.Yes Then
                    dblTrfPcs = CDbl(txtRepPcs.Text)
                    dblTrfCts = CDbl(txtRepCts.Text)

                    If CDbl(txtRej.Text) > 0 Then
                        MsgBox("Invalid Grading Transfer", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblExpReturns WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblExpReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts,ColPcs,ColCts,FloPcs,FloCts,IncPcs,IncCts,BurnPcs,BurnCts,NonPcs,NonCts,RefPcs,RefCts,CRejPcs,CRejCts) " & _
                              "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & UCase(txtPktNo.Text) & "'," & cmbSection.SelectedIndex + 1 & ",'" & Mid(Trim(txtEmp.Text), 1, 6) & "'," & _
                                    "" & CInt(txtRetTap.Text) & "," & CDbl(txtRetCts.Text) & "," & CInt(txtLostPcs.Text) & "," & CDbl(txtLostCts.Text) & "," & _
                                    "" & CInt(txtRepPcs.Text) & "," & CDbl(txtRepCts.Text) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "'," & CInt(txtRej.Text) & "," & CDbl(txtRejCts.Text) & "," & _
                                    "" & CInt(txtColPcs.Text) & "," & CDbl(txtColCts.Text) & "," & CInt(txtFloPcs.Text) & "," & CDbl(txtFloCts.Text) & "," & CInt(txtIncPcs.Text) & "," & CDbl(txtIncCts.Text) & "," & CInt(txtBurnPcs.Text) & "," & _
                                    "" & CDbl(txtBurnCts.Text) & "," & CInt(txtNonPcs.Text) & "," & CDbl(txtNonCts.Text) & "," & CInt(txtRefPcs.Text) & "," & CDbl(txtRefCts.Text) & "," & CInt(txtCRejPcs.Text) & "," & CDbl(txtCRejCts.Text) & ")")

                If cmbDept.Text = "Mix" And CInt(txtRej.Text) > 0 Then
                    Call Dep_Grading_Trf(cmbDept.Text, 9988, txtParNo.Text, txtPktNo.Text, txtRej.Text, txtRejCts.Text, txtRej.Text, txtRejCts.Text, "")
                End If

                If CInt(txtRepPcs.Text) > 0 Then
                    rsComSql_1 = New ADODB.Recordset
                    If cmbDept.Text = "Mix" Then
                        If Len(txtParNo.Text) > 8 Then
                            rsComSql_1.Open("SELECT * FROM tblImport WHERE SupParcelNo = '" & Mid(txtParNo.Text, 1, 8) & "' AND SupplierRefNo NOT LIKE 'LCL%'", AdoCN, 1, 1)
                        Else
                            rsComSql_1.Open("SELECT * FROM tblImport WHERE SupParcelNo = '" & txtParNo.Text & "' AND SupplierRefNo NOT LIKE 'LCL%'", AdoCN, 1, 1)
                        End If
                    Else
                        rsComSql_1.Open("SELECT dbo.tblImport.Department, dbo.tblImport.SystemRefNo, dbo.tblImport.SupplierRefNo, dbo.tblImport.CompanyRefNo, dbo.tblImport.BOINo, dbo.tblImport.InvoiceDate, " & _
                                    "dbo.tblImport.RecievedDate, dbo.tblImport.SupplierCode, dbo.tblImport.ParcelType, dbo.tblImport.AssortmentNo, dbo.tblImport.SupParcelNo, " & _
                                    "dbo.tblImport.DclParcelNo, dbo.tblImport.Charges, dbo.tblImport.ItemCost, dbo.tblImport.ImportNo, dbo.tblParcel.GrpParNo " & _
                            "FROM dbo.tblParcel INNER JOIN dbo.tblImport ON dbo.tblParcel.OrigParcelNo = dbo.tblImport.SupParcelNo " & _
                            "WHERE (dbo.tblParcel.Depart = '" & cmbDept.Text & "') AND (NOT (dbo.tblImport.SupplierRefNo LIKE N'LCL%')) AND (dbo.tblParcel.GrpParNo = '" & txtParNo.Text & "')", AdoCN, 1, 1)
                    End If
                    If rsComSql_1.RecordCount Then

                    End If
                    rsComSql_1 = Nothing
                End If
                Save_GradingTypes(txtParNo.Text, txtPktNo.Text, cmbSection.SelectedIndex + 1)
                Save_GradingEmp(txtParNo.Text, txtPktNo.Text, cmbSection.SelectedIndex + 1)
            Else
                MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            rsComSql = Nothing
        End If
        ClearFields()

    End Sub

    Private Sub Save_GradingTypes(ByVal strParcelNo As String, ByVal strPktNo As String, ByVal intSection As Integer)
        Dim intRow As Integer

        AdoCN.Execute("DELETE FROM tblExpReturnDetails WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Sec = " & intSection & "")
        For intRow = 0 To flxType.Rows.Count - 1
            AdoCN.Execute("INSERT INTO tblExpReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                          "VALUES('" & cmbDept.Text & "','" & strParcelNo & "','" & strPktNo & "'," & intSection & ",'" & flxType.Item(0, intRow).Value & "'," & CDbl(flxType.Item(1, intRow).Value) & "," & CDbl(flxType.Item(2, intRow).Value) & ")")
        Next

    End Sub

    Private Sub Save_GradingEmp(ByVal strParcelNo As String, ByVal strPktNo As String, ByVal intSection As Integer)
        Dim intRow As Integer

        dtpToday = GetToday()
        AdoCN.Execute("DELETE FROM tblExpReturnsEmp WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Sec = " & intSection & "")
        For intRow = 0 To flxEmp.Rows.Count - 1
            AdoCN.Execute("INSERT INTO tblExpReturnsEmp(Department,ParNo,PktNo,Sec,EmpNo,Pcs,RetDate) " & _
                          "VALUES('" & cmbDept.Text & "','" & strParcelNo & "','" & strPktNo & "'," & intSection & ",'" & flxEmp.Item(0, intRow).Value & "'," & CDbl(flxEmp.Item(1, intRow).Value) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "')")
        Next

    End Sub

    Private Sub chkRough_CheckedChanged(sender As Object, e As EventArgs) Handles chkRough.CheckedChanged
        If chkRough.Checked = True Then
            txtRepPcs.Text = txtRej.Text
            txtRepCts.Text = txtRejCts.Text
            txtRej.Text = "0"
            txtRejCts.Text = "0"
        Else
            txtRej.Text = txtRepPcs.Text
            txtRejCts.Text = txtRepCts.Text
            txtRepPcs.Text = "0"
            txtRepCts.Text = "0"
        End If
    End Sub

    Private Sub txtRetTap_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRetTap.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtRetCts.Focus()
        End If
    End Sub

    Private Sub txtRetCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRetCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRetCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtRej.Text = "0"
            txtRejCts.Text = "0"
            txtLostPcs.Text = "0"
            txtLostCts.Text = "0"
            txtRepPcs.Text = "0"
            txtRepCts.Text = "0"
            txtColPcs.Text = "0"
            txtColCts.Text = "0"
            txtFloPcs.Text = "0"
            txtFloCts.Text = "0"
            txtIncPcs.Text = "0"
            txtIncCts.Text = "0"
            txtBurnPcs.Text = "0"
            txtBurnCts.Text = "0"
            txtNonPcs.Text = "0"
            txtNonCts.Text = "0"
            txtRefPcs.Text = "0"
            txtRefCts.Text = "0"
            txtCRejPcs.Text = "0"
            txtCRejCts.Text = "0"
            txtRej.Focus()
        End If
    End Sub

    Private Sub txtRej_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRej.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtRejCts.Focus()
        End If
    End Sub

    Private Sub txtRejCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRejCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRejCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtLostPcs.Focus()
        End If
    End Sub

    Private Sub txtLostPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLostPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtLostCts.Focus()
        End If
    End Sub

    Private Sub txtLostCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLostCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtLostCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtRepPcs.Focus()
        End If
    End Sub

    Private Sub txtRepPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRepPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtRepCts.Focus()
        End If
    End Sub

    Private Sub txtRepCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRepCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRepCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtCRejPcs.Focus()
        End If
    End Sub

    Private Sub InsertZeroPcs()
        If txtColPcs.Text = "" Then txtColPcs.Text = "0"
        If txtFloPcs.Text = "" Then txtFloPcs.Text = "0"
        If txtIncPcs.Text = "" Then txtIncPcs.Text = "0"
        If txtBurnPcs.Text = "" Then txtBurnPcs.Text = "0"
        If txtNonPcs.Text = "" Then txtNonPcs.Text = "0"
        If txtRefPcs.Text = "" Then txtRefPcs.Text = "0"
    End Sub

    Private Sub InsertZeroCts()
        If txtColCts.Text = "" Then txtColCts.Text = "0"
        If txtFloCts.Text = "" Then txtFloCts.Text = "0"
        If txtIncCts.Text = "" Then txtIncCts.Text = "0"
        If txtBurnCts.Text = "" Then txtBurnCts.Text = "0"
        If txtNonCts.Text = "" Then txtNonCts.Text = "0"
        If txtRefCts.Text = "" Then txtRefCts.Text = "0"
    End Sub

    Private Sub txtColPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtColPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            InsertZeroPcs
            If chkRough.Checked = True Then
                txtRepPcs.Text = CInt(txtColPcs.Text) + CInt(txtFloPcs.Text) + CInt(txtIncPcs.Text) + CInt(txtBurnPcs.Text) + CInt(txtNonPcs.Text) + CInt(txtRefPcs.Text)
            Else
                txtRej.Text = CInt(txtColPcs.Text) + CInt(txtFloPcs.Text) + CInt(txtIncPcs.Text) + CInt(txtBurnPcs.Text) + CInt(txtNonPcs.Text) + CInt(txtRefPcs.Text)
            End If
            txtColCts.Focus()
        End If
    End Sub

    Private Sub txtColCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtColCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtColCts.Text)
        If Asc(e.KeyChar) = 13 Then
            InsertZeroCts
            If chkRough.Checked = True Then
                txtRepCts.Text = Math.Round(CSng(txtColCts.Text) + CSng(txtFloCts.Text) + CSng(txtIncCts.Text) + CSng(txtBurnCts.Text) + CSng(txtNonCts.Text) + CSng(txtRefCts.Text), 3)
            Else
                txtRejCts.Text = Math.Round(CSng(txtColCts.Text) + CSng(txtFloCts.Text) + CSng(txtIncCts.Text) + CSng(txtBurnCts.Text) + CSng(txtNonCts.Text) + CSng(txtRefCts.Text), 3)
            End If
            txtFloPcs.Focus()
        End If
    End Sub

    Private Sub txtFloPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFloPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            InsertZeroPcs
            If chkRough.Checked = True Then
                txtRepPcs.Text = CInt(txtColPcs.Text) + CInt(txtFloPcs.Text) + CInt(txtIncPcs.Text) + CInt(txtBurnPcs.Text) + CInt(txtNonPcs.Text) + CInt(txtRefPcs.Text)
            Else
                txtRej.Text = CInt(txtColPcs.Text) + CInt(txtFloPcs.Text) + CInt(txtIncPcs.Text) + CInt(txtBurnPcs.Text) + CInt(txtNonPcs.Text) + CInt(txtRefPcs.Text)
            End If
            txtFloCts.Focus()
        End If
    End Sub

    Private Sub txtFloCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFloCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtFloCts.Text)
        If Asc(e.KeyChar) = 13 Then
            InsertZeroCts
            If chkRough.Checked = True Then
                txtRepCts.Text = Math.Round(CSng(txtColCts.Text) + CSng(txtFloCts.Text) + CSng(txtIncCts.Text) + CSng(txtBurnCts.Text) + CSng(txtNonCts.Text) + CSng(txtRefCts.Text), 3)
            Else
                txtRejCts.Text = Math.Round(CSng(txtColCts.Text) + CSng(txtFloCts.Text) + CSng(txtIncCts.Text) + CSng(txtBurnCts.Text) + CSng(txtNonCts.Text) + CSng(txtRefCts.Text), 3)
            End If
            txtIncPcs.Focus()
        End If
    End Sub

    Private Sub txtIncPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIncPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            InsertZeroPcs
            If chkRough.Checked = True Then
                txtRepPcs.Text = CInt(txtColPcs.Text) + CInt(txtFloPcs.Text) + CInt(txtIncPcs.Text) + CInt(txtBurnPcs.Text) + CInt(txtNonPcs.Text) + CInt(txtRefPcs.Text)
            Else
                txtRej.Text = CInt(txtColPcs.Text) + CInt(txtFloPcs.Text) + CInt(txtIncPcs.Text) + CInt(txtBurnPcs.Text) + CInt(txtNonPcs.Text) + CInt(txtRefPcs.Text)
            End If
            txtIncCts.Focus()
        End If
    End Sub

    Private Sub txtIncCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIncCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtIncCts.Text)
        If Asc(e.KeyChar) = 13 Then
            InsertZeroCts
            If chkRough.Checked = True Then
                txtRepCts.Text = Math.Round(CSng(txtColCts.Text) + CSng(txtFloCts.Text) + CSng(txtIncCts.Text) + CSng(txtBurnCts.Text) + CSng(txtNonCts.Text) + CSng(txtRefCts.Text), 3)
            Else
                txtRejCts.Text = Math.Round(CSng(txtColCts.Text) + CSng(txtFloCts.Text) + CSng(txtIncCts.Text) + CSng(txtBurnCts.Text) + CSng(txtNonCts.Text) + CSng(txtRefCts.Text), 3)
            End If
            txtBurnPcs.Focus()
        End If
    End Sub

    Private Sub txtBurnPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBurnPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            InsertZeroPcs
            If chkRough.Checked = True Then
                txtRepPcs.Text = CInt(txtColPcs.Text) + CInt(txtFloPcs.Text) + CInt(txtIncPcs.Text) + CInt(txtBurnPcs.Text) + CInt(txtNonPcs.Text) + CInt(txtRefPcs.Text)
            Else
                txtRej.Text = CInt(txtColPcs.Text) + CInt(txtFloPcs.Text) + CInt(txtIncPcs.Text) + CInt(txtBurnPcs.Text) + CInt(txtNonPcs.Text) + CInt(txtRefPcs.Text)
            End If
            txtBurnCts.Focus()
        End If
    End Sub

    Private Sub txtBurnCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBurnCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtBurnCts.Text)
        If Asc(e.KeyChar) = 13 Then
            InsertZeroCts
            If chkRough.Checked = True Then
                txtRepCts.Text = Math.Round(CSng(txtColCts.Text) + CSng(txtFloCts.Text) + CSng(txtIncCts.Text) + CSng(txtBurnCts.Text) + CSng(txtNonCts.Text) + CSng(txtRefCts.Text), 3)
            Else
                txtRejCts.Text = Math.Round(CSng(txtColCts.Text) + CSng(txtFloCts.Text) + CSng(txtIncCts.Text) + CSng(txtBurnCts.Text) + CSng(txtNonCts.Text) + CSng(txtRefCts.Text), 3)
            End If
            txtNonPcs.Focus()
        End If
    End Sub

    Private Sub cmbType1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbType1.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtTypePcs.Focus()
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
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim intRow As Integer
        Dim intTotPcs As Integer
        Dim dblTotCts As Double

        If cmbType1.Text = "" Then
            MsgBox("Please check the input entries", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtTypePcs.Text <> "" And txtTypeCts.Text <> "" Then
            If CInt(txtTypePcs.Text) > 0 Then
                For intRow = 0 To flxType.Rows.Count - 1
                    If cmbType1.Text = flxType.Item(0, intRow).Value Then
                        MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                Next

                intTotPcs = 0
                dblTotCts = 0
                For intRow = 0 To flxType.Rows.Count - 1
                    intTotPcs = intTotPcs + CInt(flxType.Item(1, intRow).Value)
                    dblTotCts = dblTotCts + CDbl(flxType.Item(2, intRow).Value)
                Next

                If intTotPcs + CInt(txtTypePcs.Text) > CInt(txtIssTap.Text) Then
                    MsgBox("Pcs Invalid", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If Math.Round(dblTotCts + CDbl(txtTypeCts.Text), 3) > Math.Round(CDbl(txtIssCts.Text), 3) Then
                    MsgBox("Cts Invalid", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                flxType.Rows.Add(cmbType1.Text,
                                 txtTypePcs.Text,
                                 Format(CDbl(txtTypeCts.Text), "#0.000"))

                txtTotPcs.Text = CInt(txtTotPcs.Text) + CInt(txtTypePcs.Text)
                txtTotCts.Text = Format(CDbl(txtTotCts.Text) + CDbl(txtTypeCts.Text), "#0.000")

                txtRetTap.Text = txtTotPcs.Text
                txtRetCts.Text = txtTotCts.Text

                cmbType1.Text = ""

                txtTypePcs.Text = ""
                txtTypeCts.Text = ""
            Else
                MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            MsgBox("Please check the input entries", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If

        cmbType1.Focus()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        RecSave()
    End Sub

    Private Sub flxType_DoubleClick(sender As Object, e As EventArgs) Handles flxType.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            txtTotPcs.Text = CDbl(txtTotPcs.Text) - CDbl(flxType.Item(1, flxType.CurrentRow.Index).Value)
            txtTotCts.Text = Format(CDbl(txtTotCts.Text) - CDbl(flxType.Item(2, flxType.CurrentRow.Index).Value), "#0.000")
            txtRetTap.Text = txtTotPcs.Text
            txtRetCts.Text = txtTotCts.Text
            flxType.Rows.RemoveAt(flxType.CurrentRow.Index)
        End If
    End Sub

    Private Sub InsertClarity()
        Dim intSec As Integer

        If cmbDept.Text = "" Then
            MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtEmp.Text = "" Then
            MsgBox("Invalid Emp. No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If Len(Trim(txtEmp.Text)) <> 6 Then
            MsgBox("Invalid Emp. No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtParNo.Text = "" Then
            MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtPktNo.Text = "" Then
            MsgBox("Invalid Packet No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtIssTap.Text = "" Then
            MsgBox("Invalid Issue Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtIssCts.Text = "" Then
            MsgBox("Invalid Issue Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblExpPacket WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount = 0 Then
            MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql_1 = Nothing

        For intSec = 1 To 3
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblExpIssues WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = " & intSec & "", AdoCN, 1, 1)
            If rsComSql_1.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblExpIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                              "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "'," & intSec & ",'" & Mid(Trim(txtEmp.Text), 1, 6) & "'," & CInt(txtIssTap.Text) & "," & _
                                     "" & CDbl(txtIssCts.Text) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")
            Else
                MsgBox("Already Issued", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql_1 = Nothing

            If intSec < 3 Then
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblExpReturns WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = " & intSec & "", AdoCN, 1, 1)
                If rsComSql_1.RecordCount = 0 Then
                    AdoCN.Execute("INSERT INTO tblExpReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts,ColPcs,ColCts,FloPcs,FloCts,IncPcs,IncCts) " & _
                                  "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "'," & intSec & ",'" & Mid(Trim(txtEmp.Text), 1, 6) & "'," & _
                                        "" & CInt(txtIssTap.Text) & "," & CDbl(txtIssCts.Text) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,0,0,0,0)")
                End If
                rsComSql_1 = Nothing

                AdoCN.Execute("DELETE FROM tblExpReturnDetails WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = " & intSec & "")
                AdoCN.Execute("INSERT INTO tblExpReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                              "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "'," & intSec & ",'MIX'," & CInt(txtIssTap.Text) & "," & CDbl(txtIssCts.Text) & ")")

                AdoCN.Execute("DELETE FROM tblExpReturnsEmp WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = " & intSec & "")
                AdoCN.Execute("INSERT INTO tblExpReturnsEmp(Department,ParNo,PktNo,Sec,EmpNo,Pcs,RetDate) " & _
                                  "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "'," & intSec & ",'" & Mid(Trim(txtEmp.Text), 1, 6) & "'," & CInt(txtIssTap.Text) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "')")
            End If
        Next

        MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        ClearFields()

    End Sub

    Private Sub InsertSizing()
        Dim intSec As Integer
        Dim strAssortment As String
        Dim strSizingPktNo As String
        Dim dblPrice As Double
        Dim strOrderNo As String

        If cmbDept.Text = "" Then
            MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtEmp.Text = "" Then
            MsgBox("Invalid Emp. No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If Len(Trim(txtEmp.Text)) <> 6 Then
            MsgBox("Invalid Emp. No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtParNo.Text = "" Then
            MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtPktNo.Text = "" Then
            MsgBox("Invalid Packet No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtIssTap.Text = "" Then
            MsgBox("Invalid Issue Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtIssCts.Text = "" Then
            MsgBox("Invalid Issue Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        strOrderNo = ""
        strAssortment = ""
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblExpPacket WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount = 0 Then
            MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        Else
            strAssortment = Trim(rsComSql_1.Fields("AParNo").Value)
        End If
        rsComSql_1 = Nothing

        If Len(strAssortment) = 0 Then
            MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        dblPrice = 0
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT MarketPrice FROM tblAssortList WHERE Assortment = '" & strAssortment & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            dblPrice = rsComSql_1.Fields("MarketPrice").Value
        End If
        rsComSql_1 = Nothing

        For intSec = 1 To 3
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblExpIssues WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = " & intSec & "", AdoCN, 1, 1)
            If rsComSql_1.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblExpIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                              "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "'," & intSec & ",'" & Mid(Trim(txtEmp.Text), 1, 6) & "'," & CInt(txtIssTap.Text) & "," & _
                                     "" & CDbl(txtIssCts.Text) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")
            Else
                MsgBox("Already Issued", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql_1 = Nothing

            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblExpReturns WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = " & intSec & "", AdoCN, 1, 1)
            If rsComSql_1.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblExpReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts,ColPcs,ColCts,FloPcs,FloCts,IncPcs,IncCts) " & _
                              "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "'," & intSec & ",'" & Mid(Trim(txtEmp.Text), 1, 6) & "'," & _
                                    "" & CInt(txtIssTap.Text) & "," & CDbl(txtIssCts.Text) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,0,0,0,0)")
            End If
            rsComSql_1 = Nothing

            AdoCN.Execute("DELETE FROM tblExpReturnDetails WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = " & intSec & "")
            AdoCN.Execute("INSERT INTO tblExpReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                          "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "'," & intSec & ",'MIX'," & CInt(txtIssTap.Text) & "," & CDbl(txtIssCts.Text) & ")")
        Next

        strSizingPktNo = "K001"
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT MAX(RIGHT(PktNo, 3)) AS MaxPktNo FROM tblExpSizingPacket WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND LEFT(PktNo, 1) = 'K'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            If Not IsDBNull(rsComSql_1.Fields("MaxPktNo").Value) Then
                strSizingPktNo = "K" & Format(rsComSql_1.Fields("MaxPktNo").Value + 1, "000")
            Else
                strSizingPktNo = "K001"
            End If
        End If
        rsComSql_1 = Nothing

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblExpSizingPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & strSizingPktNo & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount = 0 Then
            AdoCN.Execute("INSERT INTO tblExpSizingPacket(Department, ParNo, PktNo, PktPcs, PktCts, ReturnType, PktNo2) " & _
                          "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & strSizingPktNo & "'," & CInt(txtIssTap.Text) & "," & CDbl(txtIssCts.Text) & ",'" & txtPktNo.Text & "','" & txtPktNo.Text & "')")

            AdoCN.Execute("INSERT INTO tblExpSizingIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                          "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & UCase(strSizingPktNo) & "',1,'" & Mid(Trim(txtEmp.Text), 1, 6) & "','" & CInt(txtIssTap.Text) & "'," & _
                                     "'" & CDbl(txtIssCts.Text) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")

            If chkSizeReturn.Checked = True Then
                If cmbDept.Text = "Opening" Then
                    AdoCN.Execute("INSERT INTO tblExpSizingReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts," & _
                                        "LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                                  "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & UCase(strSizingPktNo) & "',1,'" & Mid(Trim(txtEmp.Text), 1, 6) & "'" & _
                                        "," & CInt(txtIssTap.Text) & "," & CDbl(txtIssCts.Text) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0)")

                    AdoCN.Execute("DELETE FROM tblExpSizingTypes WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & strSizingPktNo & "' AND Sec = 1")

                    strOrderNo = ""
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT * FROM tblExtPacket WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        strOrderNo = rsComSql_2.Fields("Sieve").Value
                        dblPrice = Math.Round(rsComSql_2.Fields("PlanVal").Value / rsComSql_2.Fields("PktCts").Value, 2)
                    End If
                    rsComSql_2 = Nothing

                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT Assortment FROM tblNoneOrders WHERE OrderNo = '" & strOrderNo & "'", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        strAssortment = "R" & rsComSql_2.Fields("Assortment").Value
                    End If
                    rsComSql_2 = Nothing

                    AdoCN.Execute("INSERT INTO tblExpSizingTypes(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts,OK,BasePrice,PlanAssort,PlanBasePrice,EstCts) " & _
                                  "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & strSizingPktNo & "',1,'" & strAssortment & "'," & _
                                    "" & CInt(txtIssTap.Text) & "," & CDbl(txtIssCts.Text) & ",0," & dblPrice & ",'" & strOrderNo & "',0," & CDbl(txtIssCts.Text) & ")")
                End If
            End If

            'If cmbDept.Text <> "KIT Box" Then
            '    AdoCN.Execute("INSERT INTO tblExpSizingReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts," & _
            '                        "LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
            '                  "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & UCase(strSizingPktNo) & "',1,'" & Mid(Trim(txtEmp.Text), 1, 6) & "'" & _
            '                        "," & CInt(txtIssTap.Text) & "," & CDbl(txtIssCts.Text) & ",0,0," & _
            '                        "0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0)")

            '    AdoCN.Execute("DELETE FROM tblExpSizingTypes WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & strSizingPktNo & "' AND Sec = 1")

            '    AdoCN.Execute("INSERT INTO tblExpSizingTypes(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts,OK,BasePrice,PlanAssort,PlanBasePrice,EstCts) " & _
            '                  "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & strSizingPktNo & "',1,'" & strAssortment & "'," & _
            '                    "" & CInt(txtIssTap.Text) & "," & CDbl(txtIssCts.Text) & ",0," & dblPrice & ",'',0," & CDbl(txtIssCts.Text) & ")")
            'End If
            
        End If
        rsComSql_1 = Nothing

        MsgBox("Saved - " & strSizingPktNo, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        ClearFields()

    End Sub

    Private Sub cmdClarity_Click(sender As Object, e As EventArgs) Handles cmdClarity.Click
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            InsertClarity()
        End If
    End Sub

    Private Sub txtLotNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLotNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If cmbDept.Text = "Mix" Or cmbDept.Text = "Direct Import" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT DclParcelNo FROM tblImport WHERE LotNo = " & CDbl(txtLotNo.Text) & "", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    txtParNo.Text = rsComSql.Fields("DclParcelNo").Value
                    txtParNo.Focus()
                Else
                    txtParNo.Text = ""
                End If
                rsComSql = Nothing
            End If
        End If
    End Sub

    Private Sub cmdSizing_Click(sender As Object, e As EventArgs) Handles cmdSizing.Click
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            InsertSizing()
        End If
    End Sub

    Private Sub txtNonPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNonPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            InsertZeroPcs()
            If chkRough.Checked = True Then
                txtRepPcs.Text = CInt(txtColPcs.Text) + CInt(txtFloPcs.Text) + CInt(txtIncPcs.Text) + CInt(txtBurnPcs.Text) + CInt(txtNonPcs.Text) + CInt(txtRefPcs.Text)
            Else
                txtRej.Text = CInt(txtColPcs.Text) + CInt(txtFloPcs.Text) + CInt(txtIncPcs.Text) + CInt(txtBurnPcs.Text) + CInt(txtNonPcs.Text) + CInt(txtRefPcs.Text)
            End If
            txtNonCts.Focus()
        End If
    End Sub

    Private Sub txtNonCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNonCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtNonCts.Text)
        If Asc(e.KeyChar) = 13 Then
            InsertZeroCts()
            If chkRough.Checked = True Then
                txtRepCts.Text = Math.Round(CSng(txtColCts.Text) + CSng(txtFloCts.Text) + CSng(txtIncCts.Text) + CSng(txtBurnCts.Text) + CSng(txtNonCts.Text) + CSng(txtRefCts.Text), 3)
            Else
                txtRejCts.Text = Math.Round(CSng(txtColCts.Text) + CSng(txtFloCts.Text) + CSng(txtIncCts.Text) + CSng(txtBurnCts.Text) + CSng(txtNonCts.Text) + CSng(txtRefCts.Text), 3)
            End If
            txtRefPcs.Focus()
        End If
    End Sub

    Private Sub txtRefPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRefPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            InsertZeroPcs()
            If chkRough.Checked = True Then
                txtRepPcs.Text = CInt(txtColPcs.Text) + CInt(txtFloPcs.Text) + CInt(txtIncPcs.Text) + CInt(txtBurnPcs.Text) + CInt(txtNonPcs.Text) + CInt(txtRefPcs.Text)
            Else
                txtRej.Text = CInt(txtColPcs.Text) + CInt(txtFloPcs.Text) + CInt(txtIncPcs.Text) + CInt(txtBurnPcs.Text) + CInt(txtNonPcs.Text) + CInt(txtRefPcs.Text)
            End If
            txtRefCts.Focus()
        End If
    End Sub

    Private Sub txtRefCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRefCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRefCts.Text)
        If Asc(e.KeyChar) = 13 Then
            InsertZeroCts()
            If chkRough.Checked = True Then
                txtRepCts.Text = Math.Round(CSng(txtColCts.Text) + CSng(txtFloCts.Text) + CSng(txtIncCts.Text) + CSng(txtBurnCts.Text) + CSng(txtNonCts.Text) + CSng(txtRefCts.Text), 3)
            Else
                txtRejCts.Text = Math.Round(CSng(txtColCts.Text) + CSng(txtFloCts.Text) + CSng(txtIncCts.Text) + CSng(txtBurnCts.Text) + CSng(txtNonCts.Text) + CSng(txtRefCts.Text), 3)
            End If
            cmbType1.Focus()
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

    Private Sub txtCRejPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCRejPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtCRejCts.Focus()
        End If
    End Sub

    Private Sub txtCRejCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCRejCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtCRejCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtColPcs.Focus()
        End If
    End Sub
End Class