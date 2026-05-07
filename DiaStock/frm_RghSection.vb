
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RghSection
    Dim dblTotPcs As Double
    Dim dblWindowPcs As Double
    Dim dblImpValue As Double
    Dim dblEstValue As Double
    Dim intApproval As Integer
    Dim ICNo2 As String
    Dim issued As Boolean
    Dim strmsg As String
    Dim ParcelNo As String
    Dim PacketNo As String

    Private Sub frm_RghSection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Types()
        ClearFields()
    End Sub

    Private Sub Load_Types()
        cmbSection.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblRghSections ORDER BY SecCode", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbSection.Items.Add(rsComSql.Fields("SecName").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Datavalid = False
        Parcel = True
        Instring = UCase(InputBox("Enter Par/Pkt Number"))
        ParcelLen = Len(Instring)
        If ParcelLen > 9 Then
            Datavalid = True

            ParcelNo = Mid(Instring, 1, ParcelLen - 3)
            PacketNo = strRight(Instring, 3)
        End If

        If Datavalid = True Then
            txtParPkt.Text = ParcelNo & "/" & PacketNo
            cmdEmp.Enabled = True

            txtEmp.Text = ""
            ShowDetails()

            cmdEmp.Focus()
        Else
            txtParPkt.Text = ""
            txtEmp.Text = ""
            cmdEmp.Enabled = False
        End If
    End Sub

    Private Sub cmdEmp_Click(sender As Object, e As EventArgs) Handles cmdEmp.Click
        Datavalid = False
        Parcel = False
        Instring = UCase(InputBox("Enter " & "Emp No"))
        ParcelLen = Len(Instring)
        'If ParcelLen = 6 Then
        '    Datavalid = True

        '    rsComSql = New ADODB.Recordset
        '    mStrSQL = ("SELECT * FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & Trim(Instring) & "'")
        '    rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        '    ICNo = ""
        '    If Not rsComSql.EOF Then
        '        Datavalid = True
        '        ICNo = UCase(Trim(Instring))
        '    Else
        '        MsgBox("Invalid IC No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '        Datavalid = False
        '        ICNo = ""
        '        cmdEmp.Focus()
        '        Exit Sub
        '    End If
        '    rsComSql = Nothing
        '    txtEmp.Text = ICNo
        'Else
        '    MsgBox("Invalid IC No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '    Datavalid = False
        '    ICNo = ""
        '    cmdEmp.Focus()
        '    Exit Sub
        'End If

        If CheckEmployee(Trim(Instring)) = True Then
            Datavalid = True
            ICNo = UCase(Trim(Instring))
        Else
            MsgBox("Invalid IC No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Datavalid = False
            ICNo = ""
            cmdEmp.Focus()
            Exit Sub
        End If
        txtEmp.Text = ICNo

        If issued = True Then
            If Trim(ICNo2) <> Trim(ICNo) Then
                If MsgBox("IC Numbers do not match. Proceed Anyway?", MsgBoxStyle.Question + MsgBoxStyle.YesNo)  = MsgBoxResult.Yes Then
                    txtEmp.Text = ICNo
                    txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
                    txtRetTime.Text = Format(Date.Now, "HH:mm")
                    cmbType.Focus()
                Else
                    txtEmp.Text = ""
                    Datavalid = False
                    ICNo = ""
                End If
            Else
                txtEmp.Text = ICNo
                txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
                txtRetTime.Text = Format(Date.Now, "HH:mm")
                cmbType.Focus()
            End If
        Else
            txtEmp.Text = ICNo
            txtIssDate.Text = Format(Date.Now, "dd/MM/yyyy")
            txtIssTime.Text = Format(Date.Now, "HH:mm")
            cmdEmp.Focus()
        End If

    End Sub

    Private Sub ShowDetails()

        dblTotPcs = 0
        dblImpValue = 0
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT OrigParcelNo,IssuedPcs,IssuedCts,Approval FROM tblParcel WHERE GrpParNo = '" & ParcelNo & "' AND Depart = 'Rough Planning'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            dblTotPcs = rsComSql_1.Fields("IssuedPcs").Value
            intApproval = rsComSql_1.Fields("Approval").Value

            rsComSql_2 = New ADODB.Recordset
            rsComSql_2.Open("SELECT ItemCost FROM tblImport WHERE SupParcelNo = '" & rsComSql_1.Fields("OrigParcelNo").Value & "'", AdoCN, 1, 1)
            If rsComSql_2.RecordCount Then
                dblImpValue = Math.Round(rsComSql_1.Fields("IssuedCts").Value * rsComSql_2.Fields("ItemCost").Value, 2)
            End If
            rsComSql_2 = Nothing
        End If
        rsComSql_1 = Nothing

        dblWindowPcs = 0
        dblEstValue = 0
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktCts * PktPrice), 2) AS PktValue FROM tblRghPacket WHERE ParNo = '" & ParcelNo & "' AND PktType = 6", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            If Not IsDBNull(rsComSql_1.Fields("PktPcs").Value) Then
                dblWindowPcs = rsComSql_1.Fields("PktPcs").Value
                dblEstValue = rsComSql_1.Fields("PktValue").Value
            End If
        End If
        rsComSql_1 = Nothing

        If cmbSection.Text = "Admin2" Then
            If dblTotPcs > dblWindowPcs And intApproval = 0 Then
                MsgBox(dblTotPcs - dblWindowPcs & " pcs pending", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
                Exit Sub
            End If

            If dblImpValue > dblEstValue And intApproval = 0 Then
                MsgBox(dblImpValue - dblEstValue & " value lost. Get the approval to proceed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
                Exit Sub
            End If
        End If

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblRghIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = '" & cmbSection.SelectedIndex + 1 & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            PictureBox2.Visible = True
            txtIssPcs.Text = rsComSql_1.Fields("IssPcs").Value
            txtIssCts.Text = rsComSql_1.Fields("IssCts").Value
            ICNo2 = rsComSql_1.Fields("EmpNo").Value
            txtEmp.Text = rsComSql_1.Fields("EmpNo").Value
            txtIssDate.Text = Format(rsComSql_1.Fields("IssDate").Value, "dd/MM/yyyy")
            txtIssTime.Text = Format(rsComSql_1.Fields("IssTime").Value, "HH:mm")
            issued = True

            rsComSql_2 = New ADODB.Recordset
            rsComSql_2.Open("SELECT * FROM tblRghPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND PktType = '" & cmbSection.SelectedIndex + 1 & "'", AdoCN, 1, 1)
            If rsComSql_2.RecordCount Then
                txtBoiling.Text = "Boiling"
                txtColor.Text = rsComSql_2.Fields("PktColor").Value
                txtClarity.Text = rsComSql_2.Fields("PktClarity").Value
                txtFlo.Text = rsComSql_2.Fields("PktFlo").Value
                txtModel.Text = rsComSql_2.Fields("PktModel").Value
            End If
            rsComSql_2 = Nothing

        Else
            PictureBox1.Visible = False
            PictureBox2.Visible = True
            txtIssDate.Text = Format(Date.Now, "dd/MM/yyyy")
            txtIssTime.Text = Format(Date.Now, "HH:mm")
            txtIssPcs.Enabled = False
            txtIssCts.Enabled = False
            issued = False

            rsComSql_2 = New ADODB.Recordset
            rsComSql_2.Open("SELECT * FROM tblRghPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND PktType = '" & cmbSection.SelectedIndex + 1 & "'", AdoCN, 1, 1)
            If rsComSql_2.RecordCount Then
                txtIssPcs.Text = rsComSql_2.Fields("PktPcs").Value
                txtIssCts.Text = rsComSql_2.Fields("PktCts").Value
                txtBoiling.Text = "Boiling"
                txtColor.Text = rsComSql_2.Fields("PktColor").Value
                txtClarity.Text = rsComSql_2.Fields("PktClarity").Value
                txtFlo.Text = rsComSql_2.Fields("PktFlo").Value
                txtModel.Text = rsComSql_2.Fields("PktModel").Value
            Else
                MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
            End If
            rsComSql_2 = Nothing
            cmdEmp.Focus()
        End If
        rsComSql_1 = Nothing

        If issued = True Then
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblRghReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = '" & cmbSection.SelectedIndex + 1 & "'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                MsgBox("Packet Finished", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
            Else
                PictureBox1.Visible = True
                Load_RghTypes(cmbSection.SelectedIndex + 1)
                cmdEmp.Focus()
            End If
            rsComSql_1 = Nothing
        End If
    End Sub

    Private Sub Load_RghTypes(ByVal intSec As Integer)
        Dim rsGrdType As New ADODB.Recordset

        cmbType.Items.Clear()
        rsGrdType = New ADODB.Recordset
        rsGrdType.Open("SELECT * FROM tblRghTypes WHERE Sec = " & intSec & " ORDER BY Type", AdoCN, 1, 1)
        If rsGrdType.RecordCount Then
            rsGrdType.MoveFirst()
            While Not rsGrdType.EOF
                cmbType.Items.Add(rsGrdType.Fields("Type").Value)
                rsGrdType.MoveNext()
            End While
        End If
        rsGrdType = Nothing

    End Sub

    Private Sub ClearFields()
        txtParPkt.Text = ""
        txtEmp.Text = ""
        txtBoiling.Text = ""
        txtColor.Text = ""
        txtClarity.Text = ""
        txtFlo.Text = ""
        txtRetCts.Text = ""
        txtRetPcs.Text = ""
        txtIssCts.Text = ""
        txtIssPcs.Text = ""
        txtRej.Text = ""
        txtBro.Text = ""
        txtLost.Text = ""
        txtExt.Text = ""
        txtRep.Text = ""
        txtNoPay.Text = ""
        txtIssDate.Text = ""
        txtIssTime.Text = ""
        txtRetTime.Text = ""
        txtRetDate.Text = ""
        txtRejCts.Text = ""
        txtLostCts.Text = ""
        flxType.Rows.Clear()
        cmbType.Items.Clear()
        PictureBox1.Visible = False
        PictureBox2.Visible = False
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim intRow As Integer
        Dim intTotPcs As Double
        Dim dblTotCts As Double

        If cmbType.Text = "" Then
            MsgBox("Please check the input entries", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblRghTypes WHERE Sec = " & cmbSection.SelectedIndex + 1 & " AND Type = '" & cmbType.Text & "' ORDER BY Type", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Please check the input entries", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        If txtTypePcs.Text <> "" And txtTypeCts.Text <> "" Then
            If CDbl(txtTypePcs.Text) > 0 Then
                For intRow = 0 To flxType.Rows.Count - 1
                    If cmbType.Text = flxType.Item(0, intRow).Value Then
                        MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                Next

                intTotPcs = 0
                dblTotCts = 0
                For intRow = 0 To flxType.Rows.Count - 1
                    intTotPcs = intTotPcs + CDbl(flxType.Item(1, intRow).Value)
                    dblTotCts = dblTotCts + CDbl(flxType.Item(2, intRow).Value)
                Next

                If txtRetPcs.Text = "" Then txtRetPcs.Text = "0"
                If txtRetCts.Text = "" Then txtRetCts.Text = "0"
                If txtRej.Text = "" Then txtRej.Text = "0"
                If txtRejCts.Text = "" Then txtRejCts.Text = "0"
                If txtLost.Text = "" Then txtLost.Text = "0"
                If txtLostCts.Text = "" Then txtLostCts.Text = "0"
                If txtBro.Text = "" Then txtBro.Text = "0"
                If txtExt.Text = "" Then txtExt.Text = "0"
                If txtRep.Text = "" Then txtRep.Text = "0"
                If txtNoPay.Text = "" Then txtNoPay.Text = "0"

                If intTotPcs + CDbl(txtTypePcs.Text) + CDbl(txtRej.Text) + CDbl(txtLost.Text) - CDbl(txtExt.Text) > CInt(txtIssPcs.Text) Then
                    MsgBox("Pcs Invalid", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If Math.Round(dblTotCts + CDbl(txtTypeCts.Text) + CDbl(txtRejCts.Text) + CDbl(txtLostCts.Text), 3) > Math.Round(CDbl(txtIssCts.Text), 3) Then
                    MsgBox("Cts Invalid", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                flxType.Rows.Add(cmbType.Text,
                                 CInt(txtTypePcs.Text),
                                 Format(CDbl(txtTypeCts.Text), "#0.000"))

                txtRetPcs.Text = CDbl(txtRetPcs.Text) + CDbl(txtTypePcs.Text)
                txtRetCts.Text = Format(CDbl(txtRetCts.Text) + CDbl(txtTypeCts.Text), "#0.000")

                cmbType.Text = ""

                txtTypePcs.Text = ""
                txtTypeCts.Text = ""
            Else
                MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            MsgBox("Please check the input entries", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If

        cmbType.Focus()
    End Sub

    Private Sub txtTypePcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTypePcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtTypeCts.Focus()
        End If
    End Sub

    Private Sub txtRej_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRej.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub txtLost_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLost.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub txtBro_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBro.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub txtExt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtExt.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub txtRep_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRep.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub txtNoPay_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNoPay.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub txtRetCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRetCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRetCts.Text)
    End Sub

    Private Sub txtRejCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRejCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRejCts.Text)
    End Sub

    Private Sub txtLostCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLostCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtLostCts.Text)
    End Sub

    Private Sub cmbSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSection.SelectedIndexChanged
        ClearFields()
    End Sub

    Private Sub RecSave()
        Dim dataok As Boolean
        Dim stiss, stret As Integer
        Dim ciss, cret As Single

        dataok = True
        If issued Then
            If Len(txtRetDate.Text) < 2 Then Exit Sub
            stiss = CDbl(txtIssPcs.Text)
            stret = CDbl(txtRetPcs.Text) + CDbl(txtRej.Text) - CDbl(txtExt.Text) + CDbl(txtLost.Text)
            If stiss <> stret Then
                strmsg = "Stones issued " & stiss & "   Stones returned " & stret
                MsgBox(strmsg, MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
                If dataok = False Then Exit Sub
            End If
            ciss = CSng(txtIssCts.Text)
            cret = Math.Round(CSng(txtRetCts.Text) + CSng(txtRejCts.Text) + CSng(txtLostCts.Text), 3)
            If ciss <> cret Then
                strmsg = "Carets issued " & Format(ciss, "0#.###") & "   Carets returned " & Format(cret, "0#.###")
                MsgBox(strmsg, MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
                If dataok = False Then Exit Sub
            End If
        Else
            If Len(txtIssDate.Text) < 2 Then dataok = False
        End If
        If dataok Then DataSave() 'if data is ok, save the record
        cmdParPkt.Focus()

    End Sub

    Private Sub DataSave()

        dtpToday = GetToday()
        If issued = False Then
            'Issue packet
            AdoCN.Execute("INSERT INTO tblRghIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                          "VALUES('" & ParcelNo & "','" & PacketNo & "'," & cmbSection.SelectedIndex + 1 & ",'" & cmbSection.Text & "'," & cmbSection.SelectedIndex + 1 & ",'" & Mid(Trim(ICNo), 1, 6) & "'," & CDbl(txtIssPcs.Text) & "," & _
                            "" & CSng(txtIssCts.Text) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & txtIssTime.Text & "')")
        Else
            'Return Packet
            AdoCN.Execute("INSERT INTO tblRghReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcs,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs) " & _
                          "VALUES('" & ParcelNo & "','" & PacketNo & "','" & cmbSection.Text & "'," & cmbSection.SelectedIndex + 1 & "," & cmbSection.SelectedIndex + 1 & ",'" & Mid(Trim(ICNo), 1, 6) & "'," & CDbl(txtRetPcs.Text) & "," & _
                            "" & CSng(txtRetCts.Text) & "," & CInt(txtRej.Text) & "," & CSng(txtRejCts.Text) & "," & CInt(txtLost.Text) & "," & CSng(txtLostCts.Text) & "," & CInt(txtBro.Text) & "," & _
                            "" & CInt(txtRep.Text) & "," & CInt(txtNoPay.Text) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & txtRetTime.Text & "'," & CInt(txtExt.Text) & ")")

            Save_RghTypes(ParcelNo, PacketNo, cmbSection.SelectedIndex + 1)
        End If
        ClearFields()

    End Sub

    Private Sub Save_RghTypes(ByVal strParcelNo As String, ByVal strPktNo As String, ByVal intSection As Integer)
        Dim intRow As Integer

        AdoCN.Execute("DELETE FROM tblRghReturnDetails WHERE ParNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Sec = " & intSection & "")
        Select Case intSection
            Case 1
                For intRow = 0 To flxType.Rows.Count - 1
                    AdoCN.Execute("INSERT INTO tblRghReturnDetails(ParNo,PktNo,Sec,Boiling,Color,Clarity,Flo,Model,Pcs,Cts) " & _
                                  "VALUES('" & strParcelNo & "','" & strPktNo & "'," & intSection & ",'" & txtBoiling.Text & "','','','',''," & CDbl(flxType.Item(1, intRow).Value) & "," & CDbl(flxType.Item(2, intRow).Value) & ")")
                Next
            Case 2
                For intRow = 0 To flxType.Rows.Count - 1
                    AdoCN.Execute("INSERT INTO tblRghReturnDetails(ParNo,PktNo,Sec,Boiling,Color,Clarity,Flo,Model,Pcs,Cts) " & _
                                  "VALUES('" & strParcelNo & "','" & strPktNo & "'," & intSection & ",'" & txtBoiling.Text & "','" & flxType.Item(0, intRow).Value & "','','',''," & CDbl(flxType.Item(1, intRow).Value) & "," & CDbl(flxType.Item(2, intRow).Value) & ")")
                Next
            Case 3
                For intRow = 0 To flxType.Rows.Count - 1
                    AdoCN.Execute("INSERT INTO tblRghReturnDetails(ParNo,PktNo,Sec,Boiling,Color,Clarity,Flo,Model,Pcs,Cts) " & _
                                  "VALUES('" & strParcelNo & "','" & strPktNo & "'," & intSection & ",'" & txtBoiling.Text & "','" & txtColor.Text & "','','" & flxType.Item(0, intRow).Value & "',''," & CDbl(flxType.Item(1, intRow).Value) & "," & CDbl(flxType.Item(2, intRow).Value) & ")")
                Next
            Case 4
                For intRow = 0 To flxType.Rows.Count - 1
                    AdoCN.Execute("INSERT INTO tblRghReturnDetails(ParNo,PktNo,Sec,Boiling,Color,Clarity,Flo,Model,Pcs,Cts) " & _
                                  "VALUES('" & strParcelNo & "','" & strPktNo & "'," & intSection & ",'" & txtBoiling.Text & "','" & txtColor.Text & "','" & flxType.Item(0, intRow).Value & "','" & txtFlo.Text & "',''," & CDbl(flxType.Item(1, intRow).Value) & "," & CDbl(flxType.Item(2, intRow).Value) & ")")
                Next
            Case 5
                For intRow = 0 To flxType.Rows.Count - 1
                    AdoCN.Execute("INSERT INTO tblRghReturnDetails(ParNo,PktNo,Sec,Boiling,Color,Clarity,Flo,Model,Pcs,Cts) " & _
                                  "VALUES('" & strParcelNo & "','" & strPktNo & "'," & intSection & ",'" & txtBoiling.Text & "','" & txtColor.Text & "','" & txtClarity.Text & "','" & txtFlo.Text & "','" & flxType.Item(0, intRow).Value & "'," & CDbl(flxType.Item(1, intRow).Value) & "," & CDbl(flxType.Item(2, intRow).Value) & ")")
                Next
            Case 6
                For intRow = 0 To flxType.Rows.Count - 1
                    AdoCN.Execute("INSERT INTO tblRghReturnDetails(ParNo,PktNo,Sec,Boiling,Color,Clarity,Flo,Model,Pcs,Cts) " & _
                                  "VALUES('" & strParcelNo & "','" & strPktNo & "'," & intSection & ",'" & txtBoiling.Text & "','" & txtColor.Text & "','" & txtClarity.Text & "','" & txtFlo.Text & "','" & txtModel.Text & "'," & CDbl(flxType.Item(1, intRow).Value) & "," & CDbl(flxType.Item(2, intRow).Value) & ")")
                Next
        End Select
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        RecSave()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub txtRetPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRetPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmbType_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbType.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtTypePcs.Focus()
        End If
    End Sub

    Private Sub txtTypeCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTypeCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtTypeCts.Text)
        If Asc(e.KeyChar) = 13 Then
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub flxType_DoubleClick(sender As Object, e As EventArgs) Handles flxType.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse  = MsgBoxResult.Yes Then
            flxType.Rows.RemoveAt(flxType.CurrentRow.Index)
            txtRetPcs.Text = CalTotalPcs(flxType)
            txtRetCts.Text = CalTotalCts(flxType)
        End If
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(1, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(2, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function
End Class