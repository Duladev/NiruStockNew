
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDSection
    Dim issued As Boolean
    Dim Checked As Boolean
    Dim ICNo2 As String
    Dim Section As Integer
    Dim strMsg As String

    Private Sub Load_Section()
        Dim recsection As ADODB.Recordset

        cmbSection.Items.Clear()
        recsection = New ADODB.Recordset
        recsection.Open("SELECT * FROM tblGrading_Sections2 WHERE SecCode = 1 ORDER BY SecCode", AdoCN, 1, 1)
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
        rsGrdType.Open("SELECT * FROM tblGrading_Types WHERE Sec = " & intSec & " ORDER BY Seq", AdoCN, 1, 1)
        If rsGrdType.RecordCount Then
            rsGrdType.MoveFirst()
            While Not rsGrdType.EOF
                cmbType1.Items.Add(rsGrdType.Fields("Type").Value)
                rsGrdType.MoveNext()
            End While
        End If
        rsGrdType = Nothing

    End Sub

    Private Sub frm_GRDSection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        txtLostPcs.Text = ""
        txtLostCts.Text = ""
        txtIssDate.Text = ""
        txtIssTime.Text = ""
        txtRetTime.Text = ""
        txtRetDate.Text = ""

        txtRepPcs.Text = ""
        txtRepCts.Text = ""
        txtRghPcs.Text = ""
        txtRghCts.Text = ""

        cmbSection.SelectedIndex = 0

        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"

        cmbType1.Text = ""
        txtTypePcs.Text = ""
        txtTypeCts.Text = ""
        flxType.Rows.Clear()

        flxReturns.Rows.Clear()

        PictureBox1.Visible = False
        PictureBox2.Visible = False

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
        rsComSql_1.Open("SELECT * FROM tblGrading_CheckingReturns WHERE ParNo = '" & strParceNo & "' AND Department = '" & strDept & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount > 0 Then
            ParcelFound = True
        Else
            ParcelFound = False
        End If
        rsComSql_1 = Nothing
        Return ParcelFound
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

    Private Sub Load_ParcelDetails()
        Dim intIssPcsB As Integer
        Dim intIssPcsC As Integer
        Dim intLastSec As Integer

        issued = True
        Checked = False
        intIssPcsB = 0
        intIssPcsC = 0

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblGrading_Issues WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "' ORDER BY ID DESC", AdoCN, 1, 1)
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
        rsComSql_1.Open("SELECT * FROM tblGrading_Returns WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "' ORDER BY ID DESC", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            intLastSec = rsComSql_1.Fields("Sec").Value
            MsgBox("Packet Finished", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
            Exit Sub
        End If
        rsComSql_1 = Nothing

        If issued = True And Checked = True Then
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblGrading_Returns WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "' AND Sec = '" & Section & "'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                If intIssPcsC = rsComSql_1.Fields("RetPcs").Value + rsComSql_1.Fields("RepPcs").Value + rsComSql_1.Fields("LostPcs").Value + rsComSql_1.Fields("RejPcs").Value Then
                    If Section <> 1 Then
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
                rsComSql_1.Open("SELECT * FROM tblGrading_Issues WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "' AND Sec = '" & Section & "'", AdoCN, 1, 1)
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
                    rsComSql_2.Open("SELECT * FROM tblGrading_ReturnDetails WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "' AND Sec = " & intLastSec & " ORDER BY ID", AdoCN, 1, 1)
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
        rsComSql_1.Open("SELECT * FROM tblGrading_CheckingReturns WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "' AND Trf = 0 ORDER BY Sec DESC", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            txtIssTap.Text = rsComSql_1.Fields("ExPcs").Value + rsComSql_1.Fields("VgPcs").Value + rsComSql_1.Fields("BlPcs").Value + rsComSql_1.Fields("ScPcs").Value + rsComSql_1.Fields("PsPcs").Value + rsComSql_1.Fields("SzPcs").Value + rsComSql_1.Fields("OkPcs").Value
            txtIssCts.Text = Format(rsComSql_1.Fields("ExCts").Value + rsComSql_1.Fields("VgCts").Value + rsComSql_1.Fields("BlCts").Value + rsComSql_1.Fields("ScCts").Value + rsComSql_1.Fields("PsCts").Value + rsComSql_1.Fields("SzCts").Value + rsComSql_1.Fields("OkCts").Value, "#0.000")

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

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim intRow As Integer
        Dim intTotPcs As Integer
        Dim dblTotCts As Double

        If cmbType1.Text = "" Then
            MsgBox("Please check the input entries", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblGrading_Types WHERE Sec = " & Section & " AND Type = '" & cmbType1.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Please check the input entries", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        If txtTypePcs.Text <> "" And txtTypeCts.Text <> "" Then
            If CInt(txtTypePcs.Text) > 0 Then

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

    Private Sub RecSave()
        Dim dataok As Boolean
        Dim stiss, stret As Integer
        Dim ciss, cret As Single
        Dim intRow As Integer
        Dim intTotPcs As Integer
        Dim dblTotCts As Double

        dataok = True
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblGrading_CheckingReturns WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
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
            If txtRej.Text = "" Then txtRej.Text = "0"
            If txtRejCts.Text = "" Then txtRejCts.Text = "0"
            If txtLostPcs.Text = "" Then txtLostPcs.Text = "0"
            If txtLostCts.Text = "" Then txtLostCts.Text = "0"
            If txtRepPcs.Text = "" Then txtRepPcs.Text = "0"
            If txtRepCts.Text = "" Then txtRepCts.Text = "0"
            If txtRghPcs.Text = "" Then txtRghPcs.Text = "0"
            If txtRghCts.Text = "" Then txtRghCts.Text = "0"

            If txtRetTap.Text = "" Then txtRetTap.Text = "0"
            If txtRetCts.Text = "" Then txtRetCts.Text = "0"

            stiss = CInt(txtIssTap.Text)
            stret = CInt(txtRetTap.Text) + CInt(txtRej.Text) + CInt(txtLostPcs.Text) + CInt(txtRepPcs.Text) + CInt(txtRghPcs.Text)
            If stiss <> stret Then
                strMsg = "Stones Issued " & stiss & "   Stones Returned " & stret
                MsgBox(strMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
                If dataok = False Then Exit Sub
            End If

            ciss = Math.Round(CSng(txtIssCts.Text), 3)
            cret = Math.Round(CSng(txtRetCts.Text) + CSng(txtRejCts.Text) + CSng(txtLostCts.Text) + CSng(txtRepCts.Text) + CSng(txtRghCts.Text), 3)

            If ciss < cret Then
                If Math.Round(cret - ciss, 3) > 0.004 Then
                    strMsg = "Carets issued " & Format(ciss, "##.###") & "   Carets returned " & Format(cret, "##.###")
                    MsgBox(strMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    dataok = False
                    If dataok = False Then Exit Sub
                End If
            End If

            If Section < 3 Then
                intTotPcs = 0
                dblTotCts = 0
                For intRow = 0 To flxType.Rows.Count - 1
                    intTotPcs = intTotPcs + CInt(flxType.Item(1, intRow).Value)
                    dblTotCts = dblTotCts + CDbl(flxType.Item(2, intRow).Value)
                Next

                If CInt(txtIssTap.Text) <> intTotPcs + CInt(txtRepPcs.Text) + CInt(txtRej.Text) + CInt(txtLostPcs.Text) + CInt(txtRghPcs.Text) Then
                    MsgBox("Pcs not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    dataok = False
                    If dataok = False Then Exit Sub
                End If
                dblTotCts = Math.Round(dblTotCts, 3)
                If Math.Round(CDbl(txtIssCts.Text), 3) - Math.Round(Math.Round(dblTotCts, 3) + Math.Round(Val(txtRepCts.Text), 3) + Math.Round(Val(txtRejCts.Text), 3) + Math.Round(Val(txtLostCts.Text), 3) + Math.Round(Val(txtRghCts.Text), 3), 3) > 0.04 Or Math.Round(CDbl(txtIssCts.Text), 3) - Math.Round(Math.Round(dblTotCts, 3) + Math.Round(Val(txtRepCts.Text), 3) + Math.Round(Val(txtRejCts.Text), 3) + Math.Round(Val(txtLostCts.Text), 3) + Math.Round(Val(txtRghCts.Text), 3), 3) < -0.04 Then
                    MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    dataok = False
                    If dataok = False Then Exit Sub
                End If
            End If

        Else
            If Len(txtIssDate.Text) < 2 Then dataok = False
        End If

        If dataok = True Then
            DataSave()
        End If

    End Sub

    Private Sub Save_GradingTypes(ByVal strParcelNo As String, ByVal strPktNo As String, ByVal intSection As Integer)
        Dim intRow As Integer

        AdoCN.Execute("DELETE FROM tblGrading_ReturnDetails WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Sec = " & intSection & "")
        For intRow = 0 To flxType.Rows.Count - 1
            AdoCN.Execute("INSERT INTO tblGrading_ReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                          "VALUES('" & cmbDept.Text & "','" & strParcelNo & "','" & strPktNo & "'," & intSection & ",'" & flxType.Item(0, intRow).Value & "'," & CInt(flxType.Item(1, intRow).Value) & "," & CDbl(flxType.Item(2, intRow).Value) & ")")
        Next

    End Sub

    Private Sub DataSave()
        Dim strSupParcelNo As String
        Dim strDCLParcelNo As String
        Dim strAssortmentNo As String

        If issued = True And Checked = False Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_Issues WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblGrading_Issues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                              "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "'," & cmbSection.SelectedIndex + 1 & ",'" & Mid(Trim(txtEmp.Text), 1, 6) & "','" & CInt(txtIssTap.Text) & "'," & _
                                     "'" & CDbl(txtIssCts.Text) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")
            Else
                MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            rsComSql = Nothing

        ElseIf issued = True And Checked = True Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_Returns WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblGrading_Returns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts,RghPcs,RghCts) " & _
                              "VALUES ('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "'," & cmbSection.SelectedIndex + 1 & ",'" & Mid(Trim(txtEmp.Text), 1, 6) & "'" & _
                                    "," & CInt(txtRetTap.Text) & "," & CDbl(txtRetCts.Text) & "," & CInt(txtLostPcs.Text) & "," & CDbl(txtLostCts.Text) & "," & _
                                    "" & CInt(txtRepPcs.Text) & "," & CDbl(txtRepCts.Text) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "'," & CInt(txtRej.Text) & "," & CDbl(txtRejCts.Text) & "," & CInt(txtRghPcs.Text) & "," & CDbl(txtRghCts.Text) & ")")

                If CInt(txtRghPcs.Text) > 0 And (cmbDept.Text = "Rounds Direct" Or cmbDept.Text = "Rounds") Then
                    strSupParcelNo = ""
                    strDCLParcelNo = ""
                    strAssortmentNo = ""
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT SupParcelNo, DclParcelNo, AssortmentNo FROM tblDep_Trf WHERE DCLParcelNo = '" & Mid(txtParNo.Text, 1, 6) & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        strSupParcelNo = rsComSql_1.Fields("SupParcelNo").Value
                        strDCLParcelNo = rsComSql_1.Fields("DclParcelNo").Value
                        strAssortmentNo = rsComSql_1.Fields("AssortmentNo").Value
                    End If
                    rsComSql_1 = Nothing

                    'AdoCN.Execute("INSERT INTO tblDep_Rec(Department,AssortmentNo,SupParcelNo,DCLParcelNo,Pcs,Cts,RghCts) " & _
                    '              "VALUES('" & cmbDept.Text & "','" & strAssortmentNo & "','" & strSupParcelNo & "'," & _
                    '                "'" & strDCLParcelNo & "'," & CDbl(txtRghPcs.Text) & "," & CDbl(txtRghCts.Text) & "," & CDbl(txtRghCts.Text) & ")")

                    If cmbDept.Text = "Rounds" Then
                        AdoCN.Execute("INSERT INTO tblGrading_RghIssues(Department,ParNo,Assortment,IssPcs,IssCts,IssDate,IssTime,Type,PktNo) " & _
                                      "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & strAssortmentNo & "'," & CInt(txtRghPcs.Text) & "," & _
                                            "" & CDbl(txtRghCts.Text) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','R','" & txtPktNo.Text & "')")
                    End If
                    
                End If

                Save_GradingTypes(txtParNo.Text, txtPktNo.Text, cmbSection.SelectedIndex + 1)
            Else
                MsgBox("Already entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            rsComSql = Nothing
        End If
        ClearFields()

    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        RecSave()
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
            txtRej.Focus()
            txtRej.Text = "0"
            txtRejCts.Text = "0"
            txtLostPcs.Text = "0"
            txtLostCts.Text = "0"
            txtRepPcs.Text = "0"
            txtRepCts.Text = "0"
            txtRghPcs.Text = "0"
            txtRghCts.Text = "0"
        End If
    End Sub

    Private Sub txtRej_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRej.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtRejCts.Focus()
        End If
    End Sub

    Private Sub txtRejCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRejCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRetCts.Text)
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
            txtRghPcs.Focus()
        End If
    End Sub

    Private Sub txtRghPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRghPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtRghCts.Focus()
        End If
    End Sub

    Private Sub txtRghCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRghCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRghCts.Text)
        If Asc(e.KeyChar) = 13 Then
            cmbType1.Focus()
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

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Dim intPos As Integer

        Instring = UCase(InputBox("Enter Par/Pkt Number"))
        If Len(Instring) > 0 Then
            txtParPkt.Text = Trim(Instring)
            ParcelLen = Len(Instring)
            Select Case cmbDept.Text
                Case "Rounds"
                    If ParcelLen = 11 Then
                        txtParNo.Text = Mid(Instring, 1, ParcelLen - 3)
                        txtPktNo.Text = strRight(Instring, 3)
                    Else
                        txtParNo.Text = Mid(Instring, 1, ParcelLen - 4)
                        txtPktNo.Text = strRight(Instring, 4)
                    End If
                Case Else
                    intPos = InStr(1, txtParPkt.Text, "/")
                    If intPos > 0 Then
                        txtParNo.Text = Mid(txtParPkt.Text, 1, intPos - 1)
                        txtPktNo.Text = Mid(txtParPkt.Text, intPos + 1, Len(txtParPkt.Text) - intPos)
                        txtPktNo.Focus()
                    End If
            End Select
            Load_Packet()
            
        Else
            txtParPkt.Text = ""
        End If
    End Sub
End Class