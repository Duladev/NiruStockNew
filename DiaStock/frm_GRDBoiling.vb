
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDBoiling
    Dim strType As String
    Dim strGroup As String
    Dim issued As Boolean
    Dim Checked As Boolean
    Dim ICNo2 As String
    Dim Section As Integer

    Private Sub frm_GRDBoiling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Department(cmbDept)
        Load_Section()
        Load_Clarity()

        Load_GradingTypes(cmbColor2, 1)
        Load_GradingTypes(cmbClarity2, 4)
        Load_GradingTypes(cmbMake, 2)
        Load_GradingTypes(cmbCut, 5)
        Load_GradingTypes(cmbSymm, 6)
        Load_GradingTypes(cmbPol, 7)

        PictureBox1.Visible = False
        PictureBox2.Visible = False
        PictureBox3.Visible = False
        PictureBox4.Visible = False

        ClearFields()
    End Sub

    Private Sub Load_Labels()
        Select Case strType
            Case "Rounds", "Niru", "Rounds3", "Rounds4", "Rounds6", "Rounds7", "RoundsNLE"
                lbl001.Text = "Niru"
                lbl002.Text = "Commercial"
                lbl003.Text = "Black"
                lbl004.Text = "Passable"
                lbl005.Text = "3 EX"
                lbl006.Text = "Transfer"
                lbl007.Text = "Export"

                lbl006.Visible = False
                lbl007.Visible = False
                lbl008.Visible = False

                txtSzPcs.Visible = False
                txtSzCts.Visible = False

                txtOkPcs.Visible = False
                txtOkCts.Visible = False

                txtVRepPcs.Visible = False
                txtVRepCts.Visible = False

            Case "Baguettes", "Princess", "Princess2", "Mix", "GradingMix", "GradingPCU", "Emerald", "Emerald2", "Emerald3", "Opening", "Lamour", "Davinci", "Baguettes2", "Baguettes3", "Carrer", "Asscher", "Radiant"
                lbl001.Text = "Clean"
                lbl002.Text = "VS"
                lbl003.Text = "SI"
                lbl004.Text = "P1"
                lbl005.Text = "P2"
                lbl006.Text = "Sizing"
                lbl007.Text = "OK"

                lbl006.Visible = True
                lbl007.Visible = True
                lbl008.Visible = True

                txtSzPcs.Visible = True
                txtSzCts.Visible = True

                txtOkPcs.Visible = True
                txtOkCts.Visible = True

                txtVRepPcs.Visible = True
                txtVRepCts.Visible = True

            Case "Rounds Direct"
                If cmbSection.SelectedIndex + 1 = 2 Then
                    lbl001.Text = "FL2"
                    lbl002.Text = "FL1"
                    lbl003.Text = "NFL"
                    lbl004.Text = ""
                    lbl005.Text = ""
                    lbl006.Text = ""
                    lbl007.Text = ""

                    lbl006.Visible = False
                    lbl007.Visible = False
                    lbl008.Visible = False

                    txtSzPcs.Visible = False
                    txtSzCts.Visible = False

                    txtOkPcs.Visible = False
                    txtOkCts.Visible = False

                    txtVRepPcs.Visible = False
                    txtVRepCts.Visible = False

                ElseIf cmbSection.SelectedIndex + 1 = 3 Then
                    lbl001.Text = "Niru"
                    lbl002.Text = "VG"
                    lbl003.Text = "Good"
                    lbl004.Text = "Fair"
                    lbl005.Text = "NCHK"
                    lbl006.Text = ""
                    lbl007.Text = ""

                    lbl006.Visible = False
                    lbl007.Visible = False
                    lbl008.Visible = False

                    txtSzPcs.Visible = False
                    txtSzCts.Visible = False

                    txtOkPcs.Visible = False
                    txtOkCts.Visible = False

                    txtVRepPcs.Visible = False
                    txtVRepCts.Visible = False
                End If
            Case Else
                lbl001.Text = "Niru"
                lbl002.Text = "Commercial"
                lbl003.Text = "Black"
                lbl004.Text = "Passable"
                lbl005.Text = "3 EX"
                lbl006.Text = "Transfer"
                lbl007.Text = "Export"

                lbl006.Visible = False
                lbl007.Visible = False
                lbl008.Visible = False

                txtSzPcs.Visible = False
                txtSzCts.Visible = False

                txtOkPcs.Visible = False
                txtOkCts.Visible = False

                txtVRepPcs.Visible = False
                txtVRepCts.Visible = False
        End Select
    End Sub

    Private Sub Load_Section()
        Dim recsection As ADODB.Recordset

        cmbSection.Items.Clear()
        recsection = New ADODB.Recordset
        recsection.Open("SELECT * FROM tblGrading_Sections ORDER BY SecCode", AdoCN, 1, 1)
        If recsection.RecordCount Then
            recsection.MoveFirst()
            Do
                cmbSection.Items.Add(recsection.Fields("SecName").Value)
                recsection.MoveNext()
            Loop Until recsection.EOF
        End If
        recsection = Nothing
        cmbSection.SelectedIndex = 0

    End Sub

    Private Sub Load_RepairList()
        Dim rsRepair As New ADODB.Recordset

        cmbRepair.Items.Clear()
        rsRepair = New ADODB.Recordset
        If cmbDept.Text <> "Direct Import" Then
            Select Case cmbDept.Text
                Case "Rounds", "Niru", "Rounds3", "Rounds4", "Rounds6", "Rounds7", "Rounds Direct", "RoundsNLE", "Davinci"
                    rsRepair.Open("SELECT * FROM tblGrading_RepairList WHERE Department = 'Rounds' ORDER BY Reason", AdoCN, 1, 1)
                Case "Baguettes", "Baguettes2", "Baguettes3", "Princess", "Princess2", "Emerald", "Emerald2", "Emerald3", "Opening", "Lamour", "Carrer", "Asscher", "Radiant"
                    rsRepair.Open("SELECT * FROM tblGrading_RepairList WHERE Department = 'Baguettes' ORDER BY Reason", AdoCN, 1, 1)
                Case "Mix"
                    rsRepair.Open("SELECT * FROM tblGrading_RepairList WHERE Department = 'Baguettes' ORDER BY Reason", AdoCN, 1, 1)
                Case "GradingPCU_N"
                    rsRepair.Open("SELECT TOP (100) PERCENT Reason FROM dbo.tblGrading_RepairList GROUP BY Reason ORDER BY Reason", AdoCN, 1, 1)
                Case "GradingMix", "GradingPCU", "Grading Checking"
                    rsRepair.Open("SELECT * FROM tblGrading_RepairList WHERE Department = 'Baguettes' ORDER BY Reason", AdoCN, 1, 1)
            End Select
        Else
            rsRepair.Open("SELECT * FROM tblGrading_RepairList WHERE Department = '" & strType & "' ORDER BY Reason", AdoCN, 1, 1)
        End If

        If rsRepair.RecordCount Then
            rsRepair.MoveFirst()
            While Not rsRepair.EOF
                cmbRepair.Items.Add(rsRepair.Fields("Reason").Value)
                rsRepair.MoveNext()
            End While
        End If
        rsRepair = Nothing

    End Sub

    Private Function ParcelFound(ByVal strDept As String, ByVal strParceNo As String) As Boolean

        rsComSql_1 = New ADODB.Recordset
        Select Case strDept
            Case "Princess"
                If Len(txtParNo.Text) <> 7 Then
                    ParcelFound = False
                    Exit Function
                End If
                rsComSql_1.Open("SELECT * FROM tblPRPacket WHERE ParNo = '" & strParceNo & "'", AdoCN, 1, 1)
            Case "Baguettes"
                If Len(txtParNo.Text) <> 7 Then
                    ParcelFound = False
                    Exit Function
                End If
                rsComSql_1.Open("SELECT * FROM tblBAGPacket WHERE ParNo = '" & strParceNo & "'", AdoCN, 1, 1)
            Case "Rounds"
                If Len(txtParNo.Text) <> 8 Then
                    ParcelFound = False
                    Exit Function
                End If
                rsComSql_1.Open("SELECT * FROM tblRndPacket WHERE ParNo = '" & strParceNo & "'", AdoCN, 1, 1)
            Case "Niru"
                If Len(txtParNo.Text) <> 8 Then
                    ParcelFound = False
                    Exit Function
                End If
                rsComSql_1.Open("SELECT * FROM tblNiruPacket WHERE ParNo = '" & strParceNo & "'", AdoCN, 1, 1)
            Case "Rounds3", "Rounds4", "Rounds6", "Rounds7", "RoundsNLE", "Emerald", "Emerald2", "Emerald3", "Opening", "Lamour", "Davinci", "Princess2", "Baguettes2", "Baguettes3", "Carrer", "Asscher", "Radiant"
                If Len(txtParNo.Text) < 7 Then
                    ParcelFound = False
                    Exit Function
                End If
                rsComSql_1.Open("SELECT * FROM tblExtPacket WHERE ParNo = '" & strParceNo & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
            Case "Direct Import", "Rounds Direct", "Mix", "GradingMix", "GradingPCU", "GradingPCU_N", "Grading Checking"
                rsComSql_1.Open("SELECT * FROM tblGradingTrf WHERE ParcelNo = '" & strParceNo & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
            Case "Grading Export"
                rsComSql_1.Open("SELECT * FROM tblRghIssues WHERE ParNo = '" & strParceNo & "' AND SecName = 'Grading Export'", AdoCN, 1, 1)
            Case Else
                ParcelFound = False
                Exit Function
        End Select

        If rsComSql_1.RecordCount Then
            ParcelFound = True
            If cmbDept.Text = "Direct Import" Or cmbDept.Text = "Grading Checking" Or cmbDept.Text = "Grading Export" Or cmbDept.Text = "GradingPCU_N" Then
                strType = "Baguettes"
            Else
                strType = cmbDept.Text
            End If
        Else
            ParcelFound = False
        End If
        rsComSql_1 = Nothing

    End Function

    Private Function CheckBigStone(ByVal strDepartment As String, ByVal strParNo As String, ByVal strPktNo As String) As Boolean

        CheckBigStone = False
        rsComSql = New ADODB.Recordset
        Select Case strDepartment
            Case "Princess"
                rsComSql.Open("SELECT * FROM tblPRPacket WHERE ParNo = '" & strParNo & "' AND PktNo = '" & strPktNo & "'", AdoCN, 1, 1)
            Case "Baguettes"
                rsComSql.Open("SELECT * FROM tblBAGPacket WHERE ParNo = '" & strParNo & "' AND PktNo = '" & strPktNo & "'", AdoCN, 1, 1)
            Case "Rounds"
                rsComSql.Open("SELECT * FROM tblRndPacket WHERE ParNo = '" & strParNo & "' AND PktNo = '" & strPktNo & "'", AdoCN, 1, 1)
            Case "Niru"
                rsComSql.Open("SELECT * FROM tblNiruPacket WHERE ParNo = '" & strParNo & "' AND PktNo = '" & strPktNo & "'", AdoCN, 1, 1)
            Case "Rounds3", "Rounds4", "RoundsNLE", "Emerald", "Opening", "Lamour", "Davinci", "Carrer", "Baguettes2", "Asscher", "Radiant"
                rsComSql.Open("SELECT * FROM tblExtPacket WHERE ParNo = '" & strParNo & "' AND PktNo = '" & strPktNo & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
            Case "Direct Import"
                rsComSql.Open("SELECT * FROM tblGradingTrf WHERE ParcelNo = '" & strParNo & "' AND PktNo = '" & strPktNo & "' AND Department = 'Direct Import'", AdoCN, 1, 1)
            Case Else
                CheckBigStone = False
                Exit Function
        End Select
        If rsComSql.RecordCount Then
            If rsComSql.Fields("PktID").Value > 0 Then
                CheckBigStone = True
            End If
            If strDepartment = "Rounds" Or strDepartment = "Baguettes" Or strDepartment = "Baguettes2" Or strDepartment = "Princess" Or strDepartment = "Emerald" Or strDepartment = "Opening" Or strDepartment = "Carrer" Or strDepartment = "Asscher" Or strDepartment = "Davinci" Or strDepartment = "Lamour" Or strDepartment = "Radiant" Then
                If Mid(strPktNo, 1, 1) <> "P" Then
                    CheckBigStone = True
                End If
            End If
        End If
        rsComSql = Nothing

    End Function

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtParNo.Text = UCase(txtParNo.Text)
            If ParcelFound(cmbDept.Text, txtParNo.Text) = True Then
                GetNewPacket()
                txtPktNo.Focus()
            Else
                MsgBox("Department and Parcel No. not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
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

        If cmbSection.SelectedIndex = 0 And issued = True And Checked = False Then
            If Trim(ICNo2) <> Trim(ICNo) Then
                If MsgBox("IC Numbers do not match. Proceed Anyway?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes Then
                    txtEmp.Text = ICNo
                    txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
                    txtRetTime.Text = Format(Date.Now, "HH:mm")
                    txtRetTap.Focus()
                Else
                    txtEmp.Text = ""
                    Datavalid = False
                    ICNo = ""
                End If
            Else
                txtEmp.Text = ICNo
                txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
                txtRetTime.Text = Format(Date.Now, "HH:mm")
                txtRetTap.Focus()
            End If
        ElseIf cmbSection.SelectedIndex >= 1 And issued = True And Checked = True Then
            If Trim(ICNo2) <> Trim(ICNo) Then
                If MsgBox("IC Numbers do not match. Proceed Anyway?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes Then
                    txtEmp.Text = ICNo
                    txtChkDate.Text = Format(Date.Now, "dd/MM/yyyy")
                    txtChkTime.Text = Format(Date.Now, "HH:mm")
                    txtExPcs.Focus()
                Else
                    txtEmp.Text = ""
                    Datavalid = False
                    ICNo = ""
                End If
            Else
                txtEmp.Text = ICNo
                txtChkDate.Text = Format(Date.Now, "dd/MM/yyyy")
                txtChkTime.Text = Format(Date.Now, "HH:mm")
                txtExPcs.Focus()
            End If
        Else
            txtEmp.Text = ICNo
            txtIssDate.Text = Format(Date.Now, "dd/MM/yyyy")
            txtIssTime.Text = Format(Date.Now, "HH:mm")
            txtIssTap.Focus()
        End If
    End Sub

    Private Sub GetNewPacket()
        Dim dblIssPcs As Double
        Dim dblIssCts As Double

        If CheckBigStone(cmbDept.Text, txtParNo.Text, txtPktNo.Text) = True Or cmbDept.Text = "GradingPCU_N" Then
            cmbSection.SelectedIndex = 0
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT Grp, SUM(Trf_Pcs) AS Trf_Pcs, ROUND(SUM(Trf_Cts), 3) AS Trf_Cts FROM dbo.tblGradingTrf WHERE (Department = '" & cmbDept.Text & "') AND (ParcelNo = '" & txtParNo.Text & "') AND (PktNo = '" & txtPktNo.Text & "') AND (Status = 1) AND (Opening = 0) GROUP BY Grp", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                txtIssTap.Text = rsComSql.Fields("Trf_Pcs").Value
                txtIssCts.Text = Math.Round(rsComSql.Fields("Trf_Cts").Value, 3)
                txtGroup.Text = UCase(rsComSql.Fields("Grp").Value)
            Else
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts FROM tblExpSizingPlan WHERE (Department = '" & cmbDept.Text & "') AND (ParNo = '" & txtParNo.Text & "') AND (OK = 1) AND (PktNo2 = '" & txtPktNo.Text & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                        txtIssTap.Text = rsComSql_1.Fields("Pcs").Value
                        txtIssCts.Text = Math.Round(rsComSql_1.Fields("Cts").Value, 3)
                        txtGroup.Text = strRight(UCase(txtParNo.Text), 1)
                    Else
                        MsgBox("Packet is not Accepted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    End If
                Else
                    MsgBox("Packet is not Accepted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                End If
                rsComSql_1 = Nothing
            End If
            rsComSql = Nothing

            'txtIssTap.Enabled = False
            'txtIssCts.Enabled = False
        Else
            cmbSection.SelectedIndex = 0
            If UCase(Mid(txtPktNo.Text, 1, 1)) <> "P" And UCase(Mid(txtPktNo.Text, 1, 1)) <> "K" And UCase(Mid(txtPktNo.Text, 1, 1)) <> "J" And UCase(Mid(txtPktNo.Text, 1, 1)) <> "V" And UCase(Mid(txtPktNo.Text, 1, 1)) <> "Z" And UCase(Mid(txtPktNo.Text, 1, 1)) <> "G" And UCase(Mid(txtPktNo.Text, 1, 1)) <> "L" Then
                'rsComSql_1 = New ADODB.Recordset
                'rsComSql_1.Open("SELECT MAX(PktNo) AS MaxPktNo FROM tblGrading_BoilingIssues WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND LEFT(PktNo,1) <> 'P' AND LEFT(PktNo,1) <> 'K' AND LEFT(PktNo,1) <> 'J' AND LEFT(PktNo,1) <> 'V' AND LEFT(PktNo,1) <> 'Z' AND LEFT(PktNo,1) <> 'G' AND LEFT(PktNo,1) <> 'L'", AdoCN, 1, 1)
                'If rsComSql_1.RecordCount Then
                '    If Not IsDBNull(rsComSql_1.Fields("MaxPktNo").Value) Then
                '        txtPktNo.Text = Format(rsComSql_1.Fields("MaxPktNo").Value + 1, "000")
                '    Else
                '        txtPktNo.Text = "001"
                '    End If
                'End If
                'rsComSql_1 = Nothing

                txtIssTap.Text = ""
                txtIssCts.Text = ""
                txtGroup.Text = ""
                txtIssTap.Enabled = True
                txtIssCts.Enabled = True

            ElseIf UCase(Mid(txtPktNo.Text, 1, 1)) = "P" Then
                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT * FROM tblGradingTrf WHERE Department = '" & cmbDept.Text & "' AND ParcelNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Status = 1 AND Opening = 1", AdoCN, 1, 1)
                If rsComSql_2.RecordCount = 0 Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblGrading_RepairParcelsA WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        txtIssTap.Text = rsComSql_1.Fields("Pcs").Value
                        txtIssCts.Text = rsComSql_1.Fields("Cts").Value
                        txtGroup.Text = rsComSql_1.Fields("Grp").Value
                        txtOrgPkt.Text = rsComSql_1.Fields("PktNo2").Value

                        txtIssTap.Enabled = False
                        txtIssCts.Enabled = False
                    End If
                    rsComSql_1 = Nothing
                Else
                    MsgBox("1st Sizing Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql_2 = Nothing

            ElseIf UCase(Mid(txtPktNo.Text, 1, 1)) = "V" Then
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblGrading_RepairParcelsA WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    txtIssTap.Text = rsComSql_1.Fields("Pcs").Value
                    txtIssCts.Text = rsComSql_1.Fields("Cts").Value
                    txtGroup.Text = rsComSql_1.Fields("Grp").Value

                    txtIssTap.Enabled = False
                    txtIssCts.Enabled = False
                End If
                rsComSql_1 = Nothing

            ElseIf UCase(Mid(txtPktNo.Text, 1, 1)) = "Z" Then
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblGrading_RepairParcelsA WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    txtIssTap.Text = rsComSql_1.Fields("Pcs").Value
                    txtIssCts.Text = rsComSql_1.Fields("Cts").Value
                    txtGroup.Text = rsComSql_1.Fields("Grp").Value

                    txtIssTap.Enabled = False
                    txtIssCts.Enabled = False
                End If
                rsComSql_1 = Nothing

            ElseIf UCase(Mid(txtPktNo.Text, 1, 1)) = "K" Then
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT SUM(RejPcs) AS RejPcs, ROUND(SUM(RejCts), 3) AS RejCts FROM tblExpSizingReturns WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND LEFT(PktNo, 1) = 'K' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("RejPcs").Value) Then
                        dblIssPcs = 0
                        dblIssCts = 0
                        rsComSql_2 = New ADODB.Recordset
                        rsComSql_2.Open("SELECT  SUM(IssPcs) AS IssPcs, ROUND(SUM(IssCts), 3) AS IssCts FROM tblGrading_BoilingIssues WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND LEFT(PktNo, 1) = 'K' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                        If rsComSql_2.RecordCount Then
                            If Not IsDBNull(rsComSql_2.Fields("IssPcs").Value) Then
                                dblIssPcs = rsComSql_2.Fields("IssPcs").Value
                                dblIssCts = rsComSql_2.Fields("IssCts").Value
                            End If
                        End If
                        rsComSql_2 = Nothing

                        txtIssTap.Text = rsComSql_1.Fields("RejPcs").Value - dblIssPcs
                        txtIssCts.Text = Math.Round(rsComSql_1.Fields("RejCts").Value - dblIssCts, 3)

                        txtIssTap.Enabled = False
                        txtIssCts.Enabled = False
                    End If
                End If
                rsComSql_1 = Nothing

            ElseIf UCase(Mid(txtPktNo.Text, 1, 1)) = "J" Then
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT SUM(RejPcs) AS RejPcs, ROUND(SUM(RejCts), 3) AS RejCts FROM tblExpSizingReturns WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND LEFT(PktNo, 1) = 'J'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("RejPcs").Value) Then
                        txtIssTap.Text = rsComSql_1.Fields("RejPcs").Value
                        txtIssCts.Text = rsComSql_1.Fields("RejCts").Value

                        txtIssTap.Enabled = False
                        txtIssCts.Enabled = False
                    End If
                End If
                rsComSql_1 = Nothing

            ElseIf UCase(Mid(txtPktNo.Text, 1, 1)) = "L" Then
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT SUM(Trf_Pcs) AS Trf_Pcs, ROUND(SUM(Trf_Cts), 3) AS Trf_Cts FROM tblGradingTrf WHERE ParcelNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("Trf_Pcs").Value) Then
                        txtIssTap.Text = rsComSql_1.Fields("Trf_Pcs").Value
                        txtIssCts.Text = rsComSql_1.Fields("Trf_Cts").Value

                        txtIssTap.Enabled = False
                        txtIssCts.Enabled = False
                    End If
                End If
                rsComSql_1 = Nothing
            End If
        End If

        PictureBox2.Visible = True
        PictureBox1.Visible = False
        PictureBox3.Visible = False
        PictureBox4.Visible = False

        issued = False
        Checked = False

    End Sub

    Private Sub Load_ParcelDetails()
        Dim intIssPcsB As Integer
        Dim intIssPcsC As Integer

        issued = False
        Checked = False
        intIssPcsB = 0
        intIssPcsC = 0

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblGrading_BoilingIssues WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            issued = True
            intIssPcsB = rsComSql_1.Fields("IssPcs").Value
            cmbSection.SelectedIndex = 0
        Else
            issued = False
        End If
        rsComSql_1 = Nothing

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblGrading_CheckingIssues WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "' ORDER BY ID DESC", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            Checked = True
            intIssPcsC = rsComSql_1.Fields("IssPcs").Value
            cmbSection.SelectedIndex = rsComSql_1.Fields("Sec").Value - 1
            Section = rsComSql_1.Fields("Sec").Value

            If Mid(txtPktNo.Text, 1, 1) = "P" Then
                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT * FROM tblGrading_RepairParcelsA WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    txtOrgPkt.Text = rsComSql_2.Fields("PktNo2").Value
                End If
                rsComSql_2 = Nothing
            End If
        Else
            Checked = False
        End If
        rsComSql_1 = Nothing

        If issued = False And Checked = False Then
            PictureBox2.Visible = True
            txtIssDate.Text = Format(Date.Now, "dd/MM/yyyy")
            txtIssTime.Text = Format(Date.Now, "HH:mm")
            txtIssTap.Enabled = True
            txtIssCts.Enabled = True
            If UCase(Mid(txtPktNo.Text, 1, 1)) <> "S" Then
                GetNewPacket()
            Else
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblGrading_RepairReturns WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    txtIssTap.Text = rsComSql_1.Fields("RepPcs").Value
                    txtIssCts.Text = rsComSql_1.Fields("RepCts").Value
                    txtIssTap.Enabled = False
                    txtIssCts.Enabled = False
                Else
                    MsgBox("Invalid Repair Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    ClearFields()
                End If
                rsComSql_1 = Nothing
            End If

        ElseIf issued = True And Checked = False Then
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblGrading_BoilingIssues WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                txtIssTap.Text = rsComSql_1.Fields("IssPcs").Value
                txtIssCts.Text = rsComSql_1.Fields("IssCts").Value
                txtRemarks.Text = rsComSql_1.Fields("Remarks").Value
                txtGroup.Text = rsComSql_1.Fields("Grp").Value
                txtIssTap.Enabled = False
                txtIssCts.Enabled = False
                txtIssDate.Text = Format(rsComSql_1.Fields("IssDate").Value, "dd/MM/yyyy")
                txtIssTime.Text = Format(rsComSql_1.Fields("IssTime").Value, "HH:mm")
                ICNo2 = rsComSql_1.Fields("EmpNo").Value
                txtEmp.Text = rsComSql_1.Fields("EmpNo").Value
                PictureBox2.Visible = True
                PictureBox1.Visible = True
                PictureBox3.Visible = False
                PictureBox4.Visible = False

                cmdEmp.Focus()
            End If
            rsComSql_1 = Nothing

            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblGrading_BoilingReturns WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                If rsComSql_1.Fields("Trf").Value = 0 Then
                    If intIssPcsB = rsComSql_1.Fields("RetPcs").Value + rsComSql_1.Fields("LostPcs").Value + rsComSql_1.Fields("RejPcs").Value - rsComSql_1.Fields("ExtPcs").Value Then
                        PictureBox2.Visible = True
                        PictureBox1.Visible = False
                        PictureBox3.Visible = False
                        PictureBox4.Visible = False
                        txtIssDate.Text = Format(Date.Now, "dd/MM/yyyy")
                        txtIssTime.Text = Format(Date.Now, "HH:mm")
                        txtIssTap.Text = rsComSql_1.Fields("RetPcs").Value
                        txtIssCts.Text = rsComSql_1.Fields("RetCts").Value
                        txtGroup.Text = rsComSql_1.Fields("Grp").Value
                        txtIssTap.Enabled = False
                        txtIssCts.Enabled = False
                        ICNo2 = ""
                        txtEmp.Text = ""
                        cmbSection.SelectedIndex = 1
                        Section = cmbSection.SelectedIndex + 1
                        cmdEmp.Focus()
                    End If
                Else
                    rsComSql_4 = New ADODB.Recordset
                    rsComSql_4.Open("SELECT * FROM tblExpReturns WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = 3", AdoCN, 1, 1)
                    If rsComSql_4.RecordCount Then
                        If rsComSql_4.Fields("RejPcs").Value > 0 Then
                            PictureBox2.Visible = True
                            PictureBox1.Visible = False
                            PictureBox3.Visible = False
                            PictureBox4.Visible = False
                            txtIssDate.Text = Format(Date.Now, "dd/mm/yyyy")
                            txtIssTime.Text = Format(Date.Now, "HH:mm")
                            txtIssTap.Text = rsComSql_4.Fields("RejPcs").Value
                            txtIssCts.Text = rsComSql_4.Fields("RejCts").Value
                            txtIssTap.Enabled = False
                            txtIssCts.Enabled = False
                            ICNo2 = ""
                            txtEmp.Text = ""
                            cmbSection.SelectedIndex = 1
                            Section = cmbSection.SelectedIndex + 1
                            cmdEmp.Focus()
                        Else
                            MsgBox("Transfered to PCU. No Rejects", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            ClearFields()
                            Exit Sub
                        End If
                    Else
                        MsgBox("Transfered to PCU", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        ClearFields()
                        Exit Sub
                    End If
                    rsComSql_4 = Nothing

                End If
            End If
            rsComSql_1 = Nothing

        ElseIf issued = True And Checked = True Then
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblGrading_CheckingReturns WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "' AND Sec = '" & Section & "'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                If strType <> "Baguettes" And strType <> "Princess" And strType <> "Mix" And strType <> "GradingMix" And strType <> "Emerald" And strType <> "Opening" And strType <> "Lamour" And strType <> "Davinci" And strType <> "Carrer" And strType <> "Asscher" And strType <> "Radiant" Then
                    If intIssPcsC = rsComSql_1.Fields("ExPcs").Value + rsComSql_1.Fields("VgPcs").Value + rsComSql_1.Fields("BlPcs").Value + rsComSql_1.Fields("PsPcs").Value + rsComSql_1.Fields("ScPcs").Value + rsComSql_1.Fields("RepPcs").Value + rsComSql_1.Fields("LostPcs").Value + rsComSql_1.Fields("RejPcs").Value Then
                        If Section = 2 Then
                            txtIssTap.Text = rsComSql_1.Fields("ExPcs").Value + rsComSql_1.Fields("VgPcs").Value + rsComSql_1.Fields("BlPcs").Value + rsComSql_1.Fields("PsPcs").Value + rsComSql_1.Fields("ScPcs").Value
                            txtIssCts.Text = rsComSql_1.Fields("ExCts").Value + rsComSql_1.Fields("VgCts").Value + rsComSql_1.Fields("BlCts").Value + rsComSql_1.Fields("PsCts").Value + rsComSql_1.Fields("ScCts").Value
                            txtGroup.Text = rsComSql_1.Fields("Grp").Value
                            txtIssTap.Enabled = False
                            txtIssCts.Enabled = False
                            txtIssDate.Text = Format(Date.Now, "dd/mm/yyyy")
                            txtIssTime.Text = Format(Date.Now, "HH:mm")
                            ICNo2 = ""
                            txtEmp.Text = ""
                            PictureBox2.Visible = True
                            PictureBox1.Visible = False
                            PictureBox3.Visible = False
                            PictureBox4.Visible = False
                            cmbSection.SelectedIndex = Section
                            Section = Section + 1
                            cmdEmp.Focus()
                            Checked = False
                        Else
                            MsgBox("Checking Done", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            ClearFields()
                        End If
                    End If
                Else
                    If intIssPcsC = rsComSql_1.Fields("ExPcs").Value + rsComSql_1.Fields("VgPcs").Value + rsComSql_1.Fields("BlPcs").Value + rsComSql_1.Fields("PsPcs").Value + rsComSql_1.Fields("ScPcs").Value + rsComSql_1.Fields("RepPcs").Value + rsComSql_1.Fields("LostPcs").Value + rsComSql_1.Fields("RejPcs").Value Then
                        MsgBox("Checking Done", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        ClearFields()
                    End If
                End If
            Else
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblGrading_CheckingIssues WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "' AND Sec = '" & Section & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    txtIssTap.Text = rsComSql_1.Fields("IssPcs").Value
                    txtIssCts.Text = rsComSql_1.Fields("IssCts").Value
                    txtGroup.Text = rsComSql_1.Fields("Grp").Value
                    txtIssTap.Enabled = False
                    txtIssCts.Enabled = False
                    txtIssDate.Text = Format(rsComSql_1.Fields("IssDate").Value, "dd/MM/yyyy")
                    txtIssTime.Text = Format(rsComSql_1.Fields("IssTime").Value, "HH:mm")
                    ICNo2 = rsComSql_1.Fields("EmpNo").Value
                    txtEmp.Text = rsComSql_1.Fields("EmpNo").Value
                    PictureBox2.Visible = True
                    PictureBox1.Visible = False
                    PictureBox3.Visible = True
                    If cmbDept.Text = "Rounds" Then
                        PictureBox4.Visible = True
                    Else
                        PictureBox4.Visible = False
                    End If
                    Load_RepairList()
                    cmdEmp.Focus()
                End If
                rsComSql_1 = Nothing
            End If
            rsComSql_1 = Nothing
        End If

        Load_Labels()

    End Sub

    Private Sub Load_Clarity()
        Dim recClarity As ADODB.Recordset

        cmbClarity.Items.Clear()
        recClarity = New ADODB.Recordset
        recClarity.Open("SELECT * FROM tblGrading_Clarity ORDER BY Clarity", AdoCN, 1, 1)
        If recClarity.RecordCount Then
            recClarity.MoveFirst()
            While Not recClarity.EOF
                cmbClarity.Items.Add(recClarity.Fields("Clarity").Value)
                recClarity.MoveNext()
            End While
        End If
        cmbClarity.SelectedIndex = 0
    End Sub

    Private Sub Load_GradingTypes(ByVal cmbSample As System.Windows.Forms.ComboBox, ByVal intSec As Integer)
        Dim rsGrdType As New ADODB.Recordset

        cmbSample.Items.Clear()
        rsGrdType = New ADODB.Recordset
        rsGrdType.Open("SELECT * FROM tblGrading_RndTypes WHERE Sec = " & intSec & " ORDER BY Type", AdoCN, 1, 1)
        If rsGrdType.RecordCount Then
            rsGrdType.MoveFirst()
            While Not rsGrdType.EOF
                cmbSample.Items.Add(rsGrdType.Fields("Type").Value)
                rsGrdType.MoveNext()
            End While
        End If
        rsGrdType = Nothing

    End Sub

    Private Sub ClearFields()

        txtParNo.Text = ""
        txtPktNo.Text = ""
        txtOrgPkt.Text = ""
        txtEmp.Text = ""
        txtEmp2.Text = ""
        txtRetCts.Text = ""
        txtRetTap.Text = ""
        txtIssCts.Text = ""
        txtIssTap.Text = ""
        txtRemarks.Text = ""
        txtRej.Text = ""
        txtRejCts.Text = ""
        txtLost.Text = ""
        txtLostCts.Text = ""
        txtExtPcs.Text = ""
        txtIssDate.Text = ""
        txtIssTime.Text = ""
        txtRetTime.Text = ""
        txtRetDate.Text = ""

        txtExPcs.Text = ""
        txtExCts.Text = ""
        txtVgPcs.Text = ""
        txtVgCts.Text = ""
        txtBlPcs.Text = ""
        txtBlCts.Text = ""
        txtPsPcs.Text = ""
        txtPsCts.Text = ""
        txtScPcs.Text = ""
        txtScCts.Text = ""
        txtRepPcs.Text = ""
        txtRepCts.Text = ""
        txtLostPcs2.Text = ""
        txtLostCts2.Text = ""
        txtRejPcs2.Text = ""
        txtRejCts2.Text = ""
        txtChkDate.Text = ""
        txtChkTime.Text = ""

        txtSzPcs.Text = ""
        txtSzCts.Text = ""
        txtOkPcs.Text = ""
        txtOkCts.Text = ""
        txtVRepPcs.Text = ""
        txtVRepCts.Text = ""
        txtRghPcs.Text = ""
        txtRghCts.Text = ""

        cmbSection.SelectedIndex = 0

        cmbRepair.Text = ""
        txtRepPcs2.Text = ""
        txtRepCts2.Text = ""
        flxRepair.Rows.Clear()

        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"

        txtCount.Text = "0"
        chkTrf.Checked = False
        chkTrf2.Checked = False

        cmbClarity.Text = ""
        chkColor.Checked = False

        txtGroup.Text = ""

        PictureBox1.Visible = False
        PictureBox2.Visible = False
        PictureBox3.Visible = False
        PictureBox4.Visible = False

        cmbColor2.Text = ""
        cmbClarity2.Text = ""
        cmbMake.Text = ""
        txtDiameter.Text = ""
        txtPcs2.Text = ""
        txtCts2.Text = ""
        txtTotPcs2.Text = ""
        txtTotCts2.Text = ""
        flxDetails.Rows.Clear()

        cmbCut.Text = "N/A"
        cmbSymm.Text = "N/A"
        cmbPol.Text = "N/A"

        txtMCPcs.Text = ""

        cmdParPkt.Focus()
    End Sub

    Private Sub flxRepair_DoubleClick(sender As Object, e As EventArgs) Handles flxRepair.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            txtTotPcs.Text = CDbl(txtTotPcs.Text) - CDbl(flxRepair.Item(1, flxRepair.CurrentRow.Index).Value)
            txtTotCts.Text = Format(CDbl(txtTotCts.Text) - CDbl(flxRepair.Item(2, flxRepair.CurrentRow.Index).Value), "#0.000")
            txtCount.Text = CDbl(txtCount.Text) - 1
            flxRepair.Rows.RemoveAt(flxRepair.CurrentRow.Index)
        End If
    End Sub

    Private Sub RecSave()
        Dim dataok As Boolean
        Dim stiss, stret As Integer
        Dim ciss, cret As Single
        Dim intRow As Integer
        Dim intTotPcs As Double
        Dim dblTotCts As Double

        Dim intTrfPcs As Double
        Dim intIssPcs As Double
        Dim dblTrfCts As Double
        Dim dblIssCts As Double

        Dim strMsg As String

        If cmbSection.SelectedIndex = 0 And issued = False Then
            dataok = True
            If CDbl(txtIssTap.Text) <= 0 Then
                MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub

            If UCase(Mid(txtPktNo.Text, 1, 1)) <> "S" Then
                dataok = True

                If UCase(Mid(txtPktNo.Text, 1, 1)) = "P" Or UCase(Mid(txtPktNo.Text, 1, 1)) = "V" Or UCase(Mid(txtPktNo.Text, 1, 1)) = "Z" Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblGradingTrf WHERE ParcelNo = '" & txtParNo.Text & "' AND Status = 1 AND Department = '" & cmbDept.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        MsgBox("Not Accepted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        dataok = False
                    End If
                    rsComSql = Nothing
                    If dataok = False Then Exit Sub
                End If

                intTrfPcs = 0
                dblTrfCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(Trf_Pcs) AS Pcs, ROUND(SUM(Trf_Cts), 3) AS Cts FROM tblGradingTrf WHERE ParcelNo = '" & txtParNo.Text & "' AND Status = 1 AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                        intTrfPcs = rsComSql.Fields("Pcs").Value
                        dblTrfCts = rsComSql.Fields("Cts").Value
                    End If
                End If
                rsComSql = Nothing

                intIssPcs = 0
                dblIssCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(IssPcs) AS Pcs, ROUND(SUM(IssCts), 3) AS Cts FROM tblGrading_BoilingIssues WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                        intIssPcs = rsComSql.Fields("Pcs").Value
                        dblIssCts = rsComSql.Fields("Cts").Value
                    End If
                End If
                rsComSql = Nothing

                If intTrfPcs < intIssPcs + CDbl(txtIssTap.Text) Then
                    MsgBox("Invalid Transfer Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    dataok = False
                End If
                If dataok = False Then Exit Sub
            End If
        End If

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
        If dataok = False Then Exit Sub

        If Len(Trim(txtEmp.Text)) <> 6 Then
            MsgBox("Invalid Emp. No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        'MC CHecking Emp Pcs
        If txtEmp2.Text <> "" Then
            txtEmp2.Text = UCase(txtEmp2.Text)

            If Len(Trim(txtEmp2.Text)) <> 6 Then
                MsgBox("Invalid MC Checker", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub

            If CheckEmployee(txtEmp2.Text) = False Then
                MsgBox("Invalid MC Checker", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub

            If txtMCPcs.Text = "" Then
                MsgBox("Invalid MC Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub

            If CDbl(txtMCPcs.Text) <= 0 Then
                MsgBox("Invalid MC Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub

            If CDbl(txtMCPcs.Text) > CDbl(txtIssTap.Text) Then
                MsgBox("Invalid MC Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub
        Else
            If txtMCPcs.Text <> "" Then
                If CDbl(txtMCPcs.Text) <> 0 Then
                    MsgBox("Invalid MC Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    dataok = False
                End If
            End If
            If dataok = False Then Exit Sub
        End If

        If cmbDept.Text = "Rounds" Or cmbDept.Text = "Niru" Or cmbDept.Text = "Rounds3" Or cmbDept.Text = "RoundsNLE" Then
            If Len(txtParNo.Text) <> 8 Then
                MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub

        ElseIf cmbDept.Text = "Direct Import" Then
            If Len(txtParNo.Text) < 6 Then
                MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub

        ElseIf cmbDept.Text = "Rounds Direct" Then
            If Len(txtParNo.Text) < 6 Then
                MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub

        ElseIf cmbDept.Text = "GradingMix" Then
            If Len(txtParNo.Text) < 6 Then
                MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub

        ElseIf cmbDept.Text = "GradingPCU" Or cmbDept.Text = "GradingPCU_N" Then
            If Len(txtParNo.Text) < 5 Then
                MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub

        ElseIf cmbDept.Text = "Grading Checking" Then
            If Len(txtParNo.Text) < 6 Then
                MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub

        ElseIf cmbDept.Text = "Mix" Then
            If Len(txtParNo.Text) < 5 Then
                MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub
        Else
            If Len(txtParNo.Text) <> 7 Then
                MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub
        End If

        If cmbDept.Text = "Rounds" Then
            If txtGroup.Text = "" Then
                MsgBox("Invalid Group", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
        End If
        If dataok = False Then Exit Sub

        If txtPktNo.Text = "" Then
            MsgBox("Invalid Packet No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If cmbSection.SelectedIndex = 0 And issued = True Then
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

            If CDbl(txtIssTap.Text) <= 0 Then
                MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub

            If CSng(txtIssCts.Text) <= 0 Then
                MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub

            If Len(txtRetDate.Text) < 2 Then Exit Sub
            stiss = CDbl(txtIssTap.Text)
            stret = CDbl(txtRetTap.Text) + CDbl(txtRej.Text) + CDbl(txtLost.Text) - CDbl(txtExtPcs.Text)
            If stiss <> stret Then
                strMsg = "Stones Issued " & stiss & "   Stones Returned " & stret
                MsgBox(strMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
                If dataok = False Then Exit Sub
            End If

            ciss = CSng(txtIssCts.Text)
            cret = CSng(txtRetCts.Text) + CSng(txtRejCts.Text) + CSng(txtLostCts.Text)
            If ciss + 0.003 < cret Then
                strMsg = "Carats issued " & Format(ciss, "##.###") & "   Carats returned " & Format(cret, "##.###")
                MsgBox(strMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
                If dataok = False Then Exit Sub
            End If

        ElseIf cmbSection.SelectedIndex >= 1 And issued = True And Checked = True Then

            If cmbDept.Text = "Rounds" Then
                If txtGroup.Text = "" Then
                    MsgBox("Invalid Group", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    dataok = False
                End If
            End If
            If dataok = False Then Exit Sub

            If txtExPcs.Text = "" Then txtExPcs.Text = "0"
            If txtExCts.Text = "" Then txtExCts.Text = "0"
            If txtVgPcs.Text = "" Then txtVgPcs.Text = "0"
            If txtVgCts.Text = "" Then txtVgCts.Text = "0"
            If txtBlPcs.Text = "" Then txtBlPcs.Text = "0"
            If txtBlCts.Text = "" Then txtBlCts.Text = "0"
            If txtPsPcs.Text = "" Then txtPsPcs.Text = "0"
            If txtPsCts.Text = "" Then txtPsCts.Text = "0"
            If txtScPcs.Text = "" Then txtScPcs.Text = "0"
            If txtScCts.Text = "" Then txtScCts.Text = "0"
            If txtRepPcs.Text = "" Then txtRepPcs.Text = "0"
            If txtRepCts.Text = "" Then txtRepCts.Text = "0"
            If txtRejPcs2.Text = "" Then txtRejPcs2.Text = "0"
            If txtRejCts2.Text = "" Then txtRejCts2.Text = "0"
            If txtLostPcs2.Text = "" Then txtLostPcs2.Text = "0"
            If txtLostCts2.Text = "" Then txtLostCts2.Text = "0"
            If txtSzPcs.Text = "" Then txtSzPcs.Text = "0"
            If txtSzCts.Text = "" Then txtSzCts.Text = "0"
            If txtOkPcs.Text = "" Then txtOkPcs.Text = "0"
            If txtOkCts.Text = "" Then txtOkCts.Text = "0"
            If txtVRepPcs.Text = "" Then txtVRepPcs.Text = "0"
            If txtVRepCts.Text = "" Then txtVRepCts.Text = "0"
            If txtRghPcs.Text = "" Then txtRghPcs.Text = "0"
            If txtRghCts.Text = "" Then txtRghCts.Text = "0"
            If txtMCPcs.Text = "" Then txtMCPcs.Text = "0"

            stiss = CDbl(txtIssTap.Text)
            stret = CDbl(txtExPcs.Text) + CDbl(txtVgPcs.Text) + CDbl(txtBlPcs.Text) + CDbl(txtPsPcs.Text) + CDbl(txtScPcs.Text) + CDbl(txtRepPcs.Text) + CDbl(txtLostPcs2.Text) + CDbl(txtRejPcs2.Text) + CDbl(txtSzPcs.Text) + CDbl(txtOkPcs.Text) + CDbl(txtVRepPcs.Text) + CDbl(txtRghPcs.Text)
            If stiss <> stret Then
                strMsg = "Stones Issued " & stiss & "   Stones Returned " & stret
                MsgBox(strMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
                If dataok = False Then Exit Sub
            End If

            ciss = CSng(txtIssCts.Text)
            cret = CSng(txtExCts.Text) + CSng(txtVgCts.Text) + CSng(txtBlCts.Text) + CSng(txtPsCts.Text) + CSng(txtScCts.Text) + CSng(txtSzCts.Text) + CSng(txtOkCts.Text) + CSng(txtRepCts.Text) + CSng(txtLostCts2.Text) + CSng(txtRejCts2.Text) + CSng(txtVRepCts.Text) + CSng(txtRghCts.Text)
            If ciss - cret > 0.003 Or cret - ciss > 0.003 Then
                strMsg = "Carats issued " & Format(ciss, "##.###") & "   Carats returned " & Format(cret, "##.###")
                MsgBox(strMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
                If dataok = False Then Exit Sub
            End If

            intTotPcs = 0
            dblTotCts = 0
            For intRow = 0 To flxRepair.Rows.Count - 1
                intTotPcs = intTotPcs + CDbl(flxRepair.Item(1, intRow).Value)
                dblTotCts = dblTotCts + CDbl(flxRepair.Item(2, intRow).Value)
            Next

            If CDbl(txtRepPcs.Text) <> intTotPcs Then
                MsgBox("Repair Pcs not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
                If dataok = False Then Exit Sub
            End If

            If CDbl(txtRepCts.Text) <> Math.Round(dblTotCts, 3) Then
                MsgBox("Repair Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
                If dataok = False Then Exit Sub
            End If

            If Mid(cmbDept.Text, 1, 6) = "Rounds" Then
                If strRight(txtParNo.Text, 1) = "Z" Or strRight(txtParNo.Text, 1) = "P" Or strRight(txtParNo.Text, 1) = "T" Or strRight(txtParNo.Text, 1) = "H" Then
                    If CDbl(txtIssTap.Text) = 1 Then
                        If cmbClarity.Text = "" Then
                            MsgBox("Please enter the Clarity", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            dataok = False
                            If dataok = False Then Exit Sub
                        End If
                    Else
                        cmbClarity.Text = ""
                    End If
                Else
                    cmbClarity.Text = ""
                End If
            Else
                cmbClarity.Text = ""
            End If

        Else
            If Len(txtIssDate.Text) < 2 Then dataok = False
        End If

        If dataok = True Then
            DataSave()
        End If

    End Sub

    Private Sub DataSave()
        Dim strSupParcelNo As String
        Dim strDCLParcelNo As String
        Dim strAssortmentNo As String
        Dim intAMS2 As Integer
        Dim intRetPcs As Integer

        intAMS2 = 0
        If cmbSection.SelectedIndex = 0 And issued = False And Checked = False Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_BoilingIssues WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblGrading_BoilingIssues(Department,ParNo,PktNo,EmpNo,IssPcs,IssCts,IssDate,IssTime,UserName,Remarks,Grp) " & _
                              "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "','" & Mid(Trim(txtEmp.Text), 1, 6) & "','" & CDbl(txtIssTap.Text) & "'," & _
                                     "'" & CDbl(txtIssCts.Text) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "','" & txtRemarks.Text & "','" & UCase(txtGroup.Text) & "')")

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblGrading_Parcel WHERE ParNo = '" & Mid(txtParNo.Text, 1, 6) & "' AND Dept = 'Grading'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount = 0 Then
                    AdoCN.Execute("INSERT INTO tblGrading_Parcel(ParNo,Complete,Dept) VALUES('" & Mid(txtParNo.Text, 1, 6) & "',0,'Grading')")
                End If
                rsComSql_1 = Nothing

                MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Else
                MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            rsComSql = Nothing

        ElseIf cmbSection.SelectedIndex = 0 And issued = True And Checked = False Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_BoilingIssues WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_BoilingReturns WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblGrading_BoilingReturns(Department,ParNo,PktNo,EmpNo,RetPcs,RetCts, " & _
                                    "LostPcs,LostCts,RejPcs,RejCts,RetDate,RetTime,UserName,Trf,ExtPcs,Grp) " & _
                              "VALUES ('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "','" & Mid(Trim(txtEmp.Text), 1, 6) & "'" & _
                                    "," & CDbl(txtRetTap.Text) & "," & CDbl(txtRetCts.Text) & "," & CDbl(txtLost.Text) & "," & CDbl(txtLostCts.Text) & "," & CDbl(txtRej.Text) & "" & _
                                    "," & CDbl(txtRejCts.Text) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "',0," & CDbl(txtExtPcs.Text) & ",'" & UCase(txtGroup.Text) & "')")

                If CDbl(txtRej.Text) > 0 Then
                    strSupParcelNo = ""
                    strDCLParcelNo = ""
                    strAssortmentNo = ""
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT SupParcelNo,DclParcelNo,AssortmentNo FROM tblDep_Trf WHERE DCLParcelNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        strSupParcelNo = rsComSql_1.Fields("SupParcelNo").Value
                        strDCLParcelNo = rsComSql_1.Fields("DclParcelNo").Value
                        strAssortmentNo = rsComSql_1.Fields("AssortmentNo").Value
                    End If
                    rsComSql_1 = Nothing

                    If strSupParcelNo = "" Then
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT SupParcelNo, DCLParcelNo, AssortmentNo FROM tblDep_Trf WHERE DCLParcelNo = '" & Mid(txtParNo.Text, 1, 6) & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            strSupParcelNo = rsComSql_1.Fields("SupParcelNo").Value
                            strDCLParcelNo = rsComSql_1.Fields("DclParcelNo").Value
                            strAssortmentNo = rsComSql_1.Fields("AssortmentNo").Value
                        End If
                        rsComSql_1 = Nothing
                    End If

                    AdoCN.Execute("INSERT INTO tblDep_Rec(Department,AssortmentNo,SupParcelNo,DCLParcelNo,Pcs,Cts,RghCts) " & _
                                  "VALUES('" & cmbDept.Text & "','" & strAssortmentNo & "','" & strSupParcelNo & "'," & _
                                        "'" & strDCLParcelNo & "'," & CDbl(txtRej.Text) & "," & CDbl(txtRejCts.Text) & "," & CDbl(txtIssCts.Text) & ")")
                End If

                If chkTrf.Checked = True Then
                    If cmbDept.Text = "Baguettes2" Or cmbDept.Text = "Baguettes3" Or cmbDept.Text = "Princess2" Or cmbDept.Text = "Emerald2" Or cmbDept.Text = "Emerald3" Then
                        intAMS2 = 0
                    Else
                        intAMS2 = 1
                    End If
                    AdoCN.Execute("INSERT INTO tblExpPacket(Department,ParNo,PktNo,PktPcs,PktCts,PktType,PktDate,AMS2,YAH) " & _
                                  "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "'," & CDbl(txtRetTap.Text) & "," & CDbl(txtRetCts.Text) & ",'','" & Format(Date.Now, "MM/dd/yyyy") & "'," & intAMS2 & "," & intAMS2 & ")")

                    AdoCN.Execute("UPDATE tblGrading_BoilingReturns SET Trf = 1 WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'")
                    MsgBox("Saved and Transfered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Else
                    MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                End If
            Else
                MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            rsComSql = Nothing

        ElseIf cmbSection.SelectedIndex >= 1 And issued = True And Checked = False Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_BoilingReturns WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            If cmbDept.Text = "Rounds" Then
                If cmbSection.SelectedIndex + 1 = 2 Then
                    MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Else
                If cmbSection.SelectedIndex + 1 = 3 Then
                    MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_CheckingIssues WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblGrading_CheckingIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime,UserName,Grp) " & _
                              "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "'," & cmbSection.SelectedIndex + 1 & ",'" & Mid(Trim(txtEmp.Text), 1, 6) & "','" & CDbl(txtIssTap.Text) & "'," & _
                                     "'" & CDbl(txtIssCts.Text) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "','" & UCase(txtGroup.Text) & "')")
                MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Else
                MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            rsComSql = Nothing

        ElseIf cmbSection.SelectedIndex >= 1 And issued = True And Checked = True Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_CheckingIssues WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            If cmbDept.Text = "Rounds" Then
                If cmbSection.SelectedIndex + 1 = 2 Then
                    MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Else
                If cmbSection.SelectedIndex + 1 = 3 Then
                    MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            If chkTrf2.Checked = True And chkColor.Checked = False And cmbDept.Text = "Opening" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblExpPacket WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    MsgBox("Already Transfered to Sorting", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing
            End If

            If cmbDept.Text = "Rounds" Then
                If txtTotPcs2.Text = "" Then txtTotPcs2.Text = "0"

                intRetPcs = CDbl(txtExPcs.Text) + CDbl(txtVgPcs.Text) + CDbl(txtBlPcs.Text) + CDbl(txtPsPcs.Text) + CDbl(txtScPcs.Text) + CDbl(txtSzPcs.Text) + CDbl(txtOkPcs.Text) + CDbl(txtVRepPcs.Text)

                If intRetPcs <> CDbl(txtTotPcs2.Text) Then
                    MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_CheckingReturns WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblGrading_CheckingReturns(Department,ParNo,PktNo,Sec,EmpNo,ExPcs,ExCts,VgPcs,VgCts," & _
                                    "BlPcs,BlCts,PsPcs,PsCts,ScPcs,ScCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts,UserName,SzPcs,SzCts,OkPcs,OkCts,VRepPcs,VRepCts,RghPcs,RghCts,Clarity,Grp,EmpNo2,MCPcs) " & _
                              "VALUES ('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "'," & cmbSection.SelectedIndex + 1 & ",'" & Mid(Trim(txtEmp.Text), 1, 6) & "'," & CDbl(txtExPcs.Text) & "," & CDbl(txtExCts.Text) & "," & CDbl(txtVgPcs.Text) & "," & CDbl(txtVgCts.Text) & "," & _
                                    "" & CDbl(txtBlPcs.Text) & "," & CDbl(txtBlCts.Text) & "," & CDbl(txtPsPcs.Text) & "," & CDbl(txtPsCts.Text) & "," & CDbl(txtScPcs.Text) & "," & CDbl(txtScCts.Text) & "," & CDbl(txtLostPcs2.Text) & "," & CDbl(txtLostCts2.Text) & "," & _
                                    "" & CDbl(txtRepPcs.Text) & "," & CDbl(txtRepCts.Text) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "'," & CDbl(txtRejPcs2.Text) & "," & CDbl(txtRejCts2.Text) & ",'" & PBUser_EmpNo & "'," & _
                                    "" & CDbl(txtSzPcs.Text) & "," & CDbl(txtSzCts.Text) & "," & CDbl(txtOkPcs.Text) & "," & CDbl(txtOkCts.Text) & "," & CDbl(txtVRepPcs.Text) & "," & CDbl(txtVRepCts.Text) & "," & CDbl(txtRghPcs.Text) & "," & CDbl(txtRghCts.Text) & ",'" & Trim(cmbClarity.Text) & "'," & _
                                    "'" & UCase(txtGroup.Text) & "','" & Mid(Trim(txtEmp2.Text), 1, 6) & "'," & CDbl(txtMCPcs.Text) & ")")

                If cmbDept.Text = "GradingMix" And CDbl(txtRejPcs2.Text) > 0 Then
                    AdoCN.Execute("INSERT INTO tblExpPacket(Department,ParNo,PktNo,PktPcs,PktCts,PktType,PktDate,AMS2,YAH) " & _
                                  "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "'," & CDbl(txtRejPcs2.Text) & "," & CDbl(txtRejCts2.Text) & ",'','" & Format(Date.Now, "MM/dd/yyyy") & "',1,1)")
                End If

                If CDbl(txtRghPcs.Text) > 0 Then
                    strSupParcelNo = ""
                    strDCLParcelNo = ""
                    strAssortmentNo = ""
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT SupParcelNo, DCLParcelNo, AssortmentNo FROM tblDep_Trf WHERE DCLParcelNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        strSupParcelNo = rsComSql_1.Fields("SupParcelNo").Value
                        strDCLParcelNo = rsComSql_1.Fields("DCLParcelNo").Value
                        strAssortmentNo = rsComSql_1.Fields("AssortmentNo").Value
                    End If
                    rsComSql_1 = Nothing

                    If strSupParcelNo = "" Then
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT SupParcelNo, DCLParcelNo, AssortmentNo FROM tblDep_Trf WHERE DCLParcelNo = '" & Mid(txtParNo.Text, 1, 6) & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            strSupParcelNo = rsComSql_1.Fields("SupParcelNo").Value
                            strDCLParcelNo = rsComSql_1.Fields("DCLParcelNo").Value
                            strAssortmentNo = rsComSql_1.Fields("AssortmentNo").Value
                        End If
                        rsComSql_1 = Nothing
                    End If

                    AdoCN.Execute("INSERT INTO tblDep_Rec(Department,AssortmentNo,SupParcelNo,DCLParcelNo,Pcs,Cts,RghCts) " & _
                                  "VALUES('" & cmbDept.Text & "','" & strAssortmentNo & "','" & strSupParcelNo & "'," & _
                                        "'" & strDCLParcelNo & "'," & CDbl(txtRghPcs.Text) & "," & CDbl(txtRghCts.Text) & "," & CDbl(txtRghCts.Text) & ")")
                End If

                If cmbDept.Text = "Rounds" Then
                    If Mid(txtPktNo.Text, 1, 1) = "P" Then
                        Save_CheckingDetails(txtParNo.Text, txtPktNo.Text, txtOrgPkt.Text)
                    Else
                        Save_CheckingDetails(txtParNo.Text, txtPktNo.Text, "")
                    End If
                End If

                If chkColor.Checked = True And chkTrf2.Checked = False Then
                    'If chkColor.Checked = True And Mid(cmbDept.Text, 1, 10) = "GradingPCU" Then
                    If cmbSection.SelectedIndex + 1 = 2 Then
                        'Color Issue
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM tblGrading_Issues WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = 1", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            AdoCN.Execute("INSERT INTO tblGrading_Issues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                          "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "',1,'D08411','" & CInt(txtExPcs.Text) & "'," & _
                                                 "'" & CDbl(txtExCts.Text) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")
                        End If
                        rsComSql_1 = Nothing

                        'Color Return
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM tblGrading_Returns WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = 1", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            AdoCN.Execute("INSERT INTO tblGrading_Returns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts,RghPcs,RghCts) " & _
                                          "VALUES ('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "',1,'D08411'" & _
                                                "," & CInt(txtExPcs.Text) & "," & CDbl(txtExCts.Text) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0)")
                        End If
                        rsComSql_1 = Nothing

                        'Color Return Detail
                        AdoCN.Execute("DELETE FROM tblGrading_ReturnDetails WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = 1")
                        AdoCN.Execute("INSERT INTO tblGrading_ReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                                      "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "',1,'MIX COLOR'," & CInt(txtExPcs.Text) & "," & CDbl(txtExCts.Text) & ")")
                    End If
                End If

                'If chkTrf2.Checked = True And chkColor.Checked = False Then
                '    AdoCN.Execute("INSERT INTO tblExpPacket(Department,ParNo,PktNo,PktPcs,PktCts,PktType,PktDate,AMS2,YAH) " & _
                '                  "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "'," & CDbl(txtExPcs.Text) & "," & CDbl(txtExCts.Text) & ",'','" & Format(Date.Now, "MM/dd/yyyy") & "',1,1)")

                '    AdoCN.Execute("UPDATE tblGrading_CheckingReturns SET Trf = 1 WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'")
                'End If

                If Mid(txtPktNo.Text, 1, 1) = "P" Then
                    Save_RepairDetails(txtParNo.Text, txtPktNo.Text, cmbSection.SelectedIndex + 1, txtOrgPkt.Text)
                Else
                    Save_RepairDetails(txtParNo.Text, txtPktNo.Text, cmbSection.SelectedIndex + 1, "")
                End If

                MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Else
                MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            rsComSql = Nothing

        End If
        ClearFields()
        txtParNo.Focus()

    End Sub

    Private Sub Save_RepairDetails(ByVal strParcelNo As String, ByVal strPktNo As String, ByVal intSection As Integer, ByVal strOrgPkt As String)
        Dim intRow As Integer
        Dim strRepairNo As String
        Dim strRepNo As String

        AdoCN.Execute("DELETE FROM tblGrading_RepairDetails WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Sec = " & intSection & "")
        For intRow = 0 To flxRepair.Rows.Count - 1
            strRepairNo = GetNewRepair(flxRepair.Item(3, intRow).Value)
            AdoCN.Execute("INSERT INTO tblGrading_RepairDetails(Department,RepNo,ParNo,PktNo,Sec,RepPcs,RepCts,RepReason,Grp) " & _
                          "VALUES('" & cmbDept.Text & "','" & strRepairNo & "','" & strParcelNo & "','" & strPktNo & "'," & intSection & "," & CDbl(flxRepair.Item(1, intRow).Value) & "," & CDbl(flxRepair.Item(2, intRow).Value) & ",'" & flxRepair.Item(0, intRow).Value & "','" & UCase(txtGroup.Text) & "')")

            If cmbDept.Text = "Rounds" Or cmbDept.Text = "Princess" Or cmbDept.Text = "Baguettes" Or cmbDept.Text = "Emerald" Or cmbDept.Text = "Opening" Or cmbDept.Text = "Lamour" Or cmbDept.Text = "Davinci" Or cmbDept.Text = "Carrer" Or cmbDept.Text = "Asscher" Or cmbDept.Text = "Radiant" Or cmbDept.Text = "GradingPCU_N" Then
                strRepNo = GetNewRepairPkt(strParcelNo)
                If Len(strOrgPkt) > 0 Then
                    AdoCN.Execute("INSERT INTO tblGrading_RepairParcels(Department,ParNo,PktNo,Pcs,Cts,IssDate,IssTime,OK,PktNo2,RepReason,Grp) " & _
                                  "VALUES('" & cmbDept.Text & "','" & strParcelNo & "','" & strRepNo & "'," & CInt(flxRepair.Item(1, intRow).Value) & "," & CDbl(flxRepair.Item(2, intRow).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,'" & strOrgPkt & "','" & flxRepair.Item(0, intRow).Value & "','" & UCase(txtGroup.Text) & "')")
                Else
                    AdoCN.Execute("INSERT INTO tblGrading_RepairParcels(Department,ParNo,PktNo,Pcs,Cts,IssDate,IssTime,OK,PktNo2,RepReason,Grp) " & _
                                  "VALUES('" & cmbDept.Text & "','" & strParcelNo & "','" & strRepNo & "'," & CInt(flxRepair.Item(1, intRow).Value) & "," & CDbl(flxRepair.Item(2, intRow).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,'" & strPktNo & "','" & flxRepair.Item(0, intRow).Value & "','" & UCase(txtGroup.Text) & "')")
                End If

            End If
        Next

    End Sub

    Private Sub Save_CheckingDetails(ByVal strParcelNo As String, ByVal strPktNo As String, ByVal strOrgPkt As String)
        Dim intRow As Integer

        AdoCN.Execute("DELETE FROM tblGrading_CheckingDetails WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "'")
        For intRow = 0 To flxDetails.Rows.Count - 1
            If Len(strOrgPkt) > 0 Then
                AdoCN.Execute("INSERT INTO tblGrading_CheckingDetails(Department,ParNo,PktNo,Color,Clarity,Make,Diameter,Pcs,Cts,OrgPktNo,Cut,Symmetry,Polish,Assortment,Price) " & _
                              "VALUES('" & cmbDept.Text & "','" & strParcelNo & "','" & strPktNo & "','" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(2, intRow).Value & "'," & CDbl(flxDetails.Item(3, intRow).Value) & "," & CInt(flxDetails.Item(4, intRow).Value) & "," & CDbl(flxDetails.Item(5, intRow).Value) & ",'" & strOrgPkt & "','" & flxDetails.Item(6, intRow).Value & "','" & flxDetails.Item(7, intRow).Value & "','" & flxDetails.Item(8, intRow).Value & "','" & flxDetails.Item(9, intRow).Value & "','" & CDbl(flxDetails.Item(10, intRow).Value) & "')")
            Else
                AdoCN.Execute("INSERT INTO tblGrading_CheckingDetails(Department,ParNo,PktNo,Color,Clarity,Make,Diameter,Pcs,Cts,OrgPktNo,Cut,Symmetry,Polish,Assortment,Price) " & _
                              "VALUES('" & cmbDept.Text & "','" & strParcelNo & "','" & strPktNo & "','" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(2, intRow).Value & "'," & CDbl(flxDetails.Item(3, intRow).Value) & "," & CInt(flxDetails.Item(4, intRow).Value) & "," & CDbl(flxDetails.Item(5, intRow).Value) & ",'" & strPktNo & "','" & flxDetails.Item(6, intRow).Value & "','" & flxDetails.Item(7, intRow).Value & "','" & flxDetails.Item(8, intRow).Value & "','" & flxDetails.Item(9, intRow).Value & "','" & CDbl(flxDetails.Item(10, intRow).Value) & "')")
            End If
        Next

    End Sub

    Private Function GetNewRepair(ByVal strRepCode As String) As String

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT MAX(CONVERT(NUMERIC,RIGHT(RepNo,8))) AS MaxRepNo FROM tblGrading_RepairDetails WHERE LEFT(RepNo,2) = '" & strRepCode & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            If Not IsDBNull(rsComSql_1.Fields("MaxRepNo").Value) Then
                GetNewRepair = strRepCode & Format(rsComSql_1.Fields("MaxRepNo").Value + 1, "00000000")
            Else
                GetNewRepair = strRepCode & "00000001"
            End If
        Else
            GetNewRepair = strRepCode & "00000001"
        End If
        rsComSql_1 = Nothing

    End Function

    Private Function GetNewRepairPkt(ByVal strParcelNo As String) As String

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT MAX(CONVERT(NUMERIC,RIGHT(PktNo,4))) AS MaxRepNo FROM tblGrading_RepairParcels WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & strParcelNo & "' AND LEFT(PktNo, 1) = 'R'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            If Not IsDBNull(rsComSql_1.Fields("MaxRepNo").Value) Then
                GetNewRepairPkt = "R" & Format(rsComSql_1.Fields("MaxRepNo").Value + 1, "0000")
            Else
                GetNewRepairPkt = "R" & "0001"
            End If
        Else
            GetNewRepairPkt = "R" & "0001"
        End If
        rsComSql_1 = Nothing

    End Function

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
            txtLost.Text = "0"
            txtLostCts.Text = "0"
            txtExtPcs.Text = "0"
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
            txtLost.Focus()
        End If
    End Sub

    Private Sub txtLost_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLost.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtLostCts.Focus()
        End If
    End Sub

    Private Sub txtLostCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLostCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtLostCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtExtPcs.Focus()
        End If
    End Sub

    Private Sub txtExtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtExtPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub txtExPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtExPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtExCts.Focus()
        End If
    End Sub

    Private Sub txtExCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtExCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtExCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtVgPcs.Focus()
        End If
    End Sub

    Private Sub txtVgPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtVgPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtVgCts.Focus()
        End If
    End Sub

    Private Sub txtVgCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtVgCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtVgCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtBlPcs.Focus()
        End If
    End Sub

    Private Sub txtBlPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBlPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtBlCts.Focus()
        End If
    End Sub

    Private Sub txtBlCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBlCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtBlCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtPsPcs.Focus()
        End If
    End Sub

    Private Sub txtPsPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPsPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtPsCts.Focus()
        End If
    End Sub

    Private Sub txtPsCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPsCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtPsCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtScPcs.Focus()
        End If
    End Sub

    Private Sub txtScCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtScCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtScCts.Text)
        If Asc(e.KeyChar) = 13 Then
            If cmbDept.Text = "Rounds" Then
                txtRepPcs.Focus()
            Else
                txtSzPcs.Focus()
            End If
        End If
    End Sub

    Private Sub txtScPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtScPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtScCts.Focus()
        End If
    End Sub

    Private Sub txtSzPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSzPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtSzCts.Focus()
        End If
    End Sub

    Private Sub txtSzCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSzCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtSzCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtOkPcs.Focus()
        End If
    End Sub

    Private Sub txtOkPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOkPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtOkCts.Focus()
        End If
    End Sub

    Private Sub txtOkCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOkCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtOkCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtVRepPcs.Focus()
        End If
    End Sub

    Private Sub txtVRepPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtVRepPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtVRepCts.Focus()
        End If
    End Sub

    Private Sub txtVRepCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtVRepCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtVRepCts.Text)
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
            txtLostPcs2.Focus()
        End If
    End Sub

    Private Sub txtLostPcs2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLostPcs2.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtLostCts2.Focus()
        End If
    End Sub

    Private Sub txtLostCts2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLostCts2.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtLostCts2.Text)
        If Asc(e.KeyChar) = 13 Then
            txtRejPcs2.Focus()
        End If
    End Sub

    Private Sub txtRejPcs2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRejPcs2.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtRejCts2.Focus()
        End If
    End Sub

    Private Sub txtRejCts2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRejCts2.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRejCts2.Text)
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
            cmbRepair.Focus()
        End If
    End Sub

    Private Sub cmbRepair_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbRepair.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtRepPcs2.Focus()
        End If
    End Sub

    Private Sub txtRepPcs2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRepPcs2.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtRepCts2.Focus()
        End If
    End Sub

    Private Sub txtRepCts2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRepCts2.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRepCts2.Text)
        If Asc(e.KeyChar) = 13 Then
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim intRow As Integer
        Dim intTotPcs As Integer
        Dim dblTotCts As Double
        Dim rsRepNo As New ADODB.Recordset
        Dim strRepCode As String

        If cmbRepair.Text <> "" And txtRepPcs.Text <> "" And txtRepCts.Text <> "" And txtRepPcs2.Text <> "" And txtRepCts2.Text <> "" Then
            If CDbl(txtRepPcs2.Text) > 0 Then
                'For intRow = 0 To flxRepair.Rows.Count - 1
                '    If cmbRepair.Text = flxRepair.Item(0, intRow).Value Then
                '        MsgBox("Repair Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                '        Exit Sub
                '    End If
                'Next

                intTotPcs = 0
                dblTotCts = 0
                For intRow = 0 To flxRepair.Rows.Count - 1
                    intTotPcs = intTotPcs + CDbl(flxRepair.Item(1, intRow).Value)
                    dblTotCts = dblTotCts + CDbl(flxRepair.Item(2, intRow).Value)
                Next

                If intTotPcs + CDbl(txtRepPcs2.Text) > CDbl(txtRepPcs.Text) Then
                    MsgBox("Repair Pcs Invalid", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If Math.Round(dblTotCts + CDbl(txtRepCts2.Text), 3) > Math.Round(CDbl(txtRepCts.Text), 3) Then
                    MsgBox("Repair Cts Invalid", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                rsRepNo = New ADODB.Recordset
                rsRepNo.Open("SELECT * FROM tblGrading_RepairList WHERE Reason = '" & cmbRepair.Text & "'", AdoCN, 1, 1)
                If rsRepNo.RecordCount Then
                    strRepCode = rsRepNo.Fields("PktString").Value
                Else
                    MsgBox("Invalid Repair Reason", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsRepNo = Nothing

                flxRepair.Rows.Add(cmbRepair.Text,
                                   txtRepPcs2.Text,
                                   Format(CDbl(txtRepCts2.Text), "#0.000"),
                                   strRepCode)

                txtTotPcs.Text = CDbl(txtTotPcs.Text) + CDbl(txtRepPcs2.Text)
                txtTotCts.Text = Format(CDbl(txtTotCts.Text) + CDbl(txtRepCts2.Text), "#0.000")

                txtCount.Text = CDbl(txtCount.Text) + 1

                cmbRepair.Text = ""
                txtRepPcs2.Text = ""
                txtRepCts2.Text = ""
            Else
                MsgBox("Invalid Repair Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            MsgBox("Please check the input entries", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        cmbRepair.Focus()
    End Sub

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Instring = UCase(InputBox("Enter Par/Pkt Number"))
        ParcelLen = Len(Instring)
        If ParcelLen >= 10 Then
            Select Case cmbDept.Text
                Case "Baguettes"
                    txtParNo.Text = Mid(Instring, 1, ParcelLen - 4)
                    txtPktNo.Text = strRight(Instring, 4)
                Case "Rounds"
                    If ParcelLen = 11 Then
                        txtParNo.Text = Mid(Instring, 1, ParcelLen - 3)
                        txtPktNo.Text = strRight(Instring, 3)
                    Else
                        txtParNo.Text = Mid(Instring, 1, ParcelLen - 4)
                        txtPktNo.Text = strRight(Instring, 4)
                    End If
                Case Else
                    If ParcelLen = 10 Then
                        txtParNo.Text = Mid(Instring, 1, ParcelLen - 3)
                        txtPktNo.Text = strRight(Instring, 3)
                    Else
                        txtParNo.Text = Mid(Instring, 1, ParcelLen - 4)
                        txtPktNo.Text = strRight(Instring, 4)
                    End If
            End Select

            txtPktNo.Focus()
            Load_Packet()
        Else
            txtParPkt.Text = ""
            cmdParPkt.Focus()
        End If
    End Sub

    Private Sub txtIssTap_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIssTap.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtIssCts.Focus()
        End If
    End Sub

    Private Sub txtIssCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIssCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtIssCts.Text)
    End Sub

    Private Sub cmbColor2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbColor2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbClarity2.Focus()
        End If
    End Sub

    Private Sub cmbClarity2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbClarity2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbMake.Focus()
        End If
    End Sub

    Private Sub txtDiameter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDiameter.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtDiameter.Text)
        If Asc(e.KeyChar) = 13 Then
            cmbCut.Focus()
        End If
    End Sub

    Private Sub txtPcs2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs2.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtCts2.Focus()
        End If
    End Sub

    Private Sub cmdAdd2_Click(sender As Object, e As EventArgs) Handles cmdAdd2.Click
        Dim intTotPcs As Integer
        Dim intRetPcs As Integer

        Dim dblTotCts As Double
        Dim dblRetCts As Double

        Dim strCode As String
        Dim strAssortment As String
        Dim dblPrice As Double

        If cmbColor2.Text <> "" And cmbClarity2.Text <> "" And cmbMake.Text <> "" And txtDiameter.Text <> "" And txtPcs2.Text <> "" And txtCts2.Text <> "" And cmbCut.Text <> "" And cmbSymm.Text <> "" And cmbPol.Text <> "" Then

            intTotPcs = 0
            dblTotCts = 0
            For intRow = 0 To flxDetails.Rows.Count - 1
                intTotPcs = intTotPcs + CDbl(flxDetails.Item(4, intRow).Value)
                dblTotCts = dblTotCts + CDbl(flxDetails.Item(5, intRow).Value)
            Next

            If txtExPcs.Text = "" Then txtExPcs.Text = "0"
            If txtExCts.Text = "" Then txtExCts.Text = "0"
            If txtVgPcs.Text = "" Then txtVgPcs.Text = "0"
            If txtVgCts.Text = "" Then txtVgCts.Text = "0"
            If txtBlPcs.Text = "" Then txtBlPcs.Text = "0"
            If txtBlCts.Text = "" Then txtBlCts.Text = "0"
            If txtPsPcs.Text = "" Then txtPsPcs.Text = "0"
            If txtPsCts.Text = "" Then txtPsCts.Text = "0"
            If txtScPcs.Text = "" Then txtScPcs.Text = "0"
            If txtScCts.Text = "" Then txtScCts.Text = "0"
            If txtRepPcs.Text = "" Then txtRepPcs.Text = "0"
            If txtRepCts.Text = "" Then txtRepCts.Text = "0"
            If txtRejPcs2.Text = "" Then txtRejPcs2.Text = "0"
            If txtRejCts2.Text = "" Then txtRejCts2.Text = "0"
            If txtLostPcs2.Text = "" Then txtLostPcs2.Text = "0"
            If txtLostCts2.Text = "" Then txtLostCts2.Text = "0"
            If txtSzPcs.Text = "" Then txtSzPcs.Text = "0"
            If txtSzCts.Text = "" Then txtSzCts.Text = "0"
            If txtOkPcs.Text = "" Then txtOkPcs.Text = "0"
            If txtOkCts.Text = "" Then txtOkCts.Text = "0"
            If txtVRepPcs.Text = "" Then txtVRepPcs.Text = "0"
            If txtVRepCts.Text = "" Then txtVRepCts.Text = "0"
            If txtRghPcs.Text = "" Then txtRghPcs.Text = "0"
            If txtRghCts.Text = "" Then txtRghCts.Text = "0"

            intRetPcs = CDbl(txtExPcs.Text) + CDbl(txtVgPcs.Text) + CDbl(txtBlPcs.Text) + CDbl(txtPsPcs.Text) + CDbl(txtScPcs.Text) + CDbl(txtSzPcs.Text) + CDbl(txtOkPcs.Text) + CDbl(txtVRepPcs.Text)
            dblRetCts = CDbl(txtExCts.Text) + CDbl(txtVgCts.Text) + CDbl(txtBlCts.Text) + CDbl(txtPsCts.Text) + CDbl(txtScCts.Text) + CDbl(txtSzCts.Text) + CDbl(txtOkCts.Text) + CDbl(txtVRepCts.Text)

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_RndTypes WHERE Sec = 1 AND Type = '" & cmbColor2.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Color", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_RndTypes WHERE Sec = 4 AND Type = '" & cmbClarity2.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Clarity", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_RndTypes WHERE Sec = 2 AND Type = '" & cmbMake.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Make", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_RndTypes WHERE Sec = 5 AND Type = '" & cmbCut.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Cut", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_RndTypes WHERE Sec = 6 AND Type = '" & cmbSymm.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Symmetry", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_RndTypes WHERE Sec = 7 AND Type = '" & cmbPol.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Polish", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            If CDbl(txtDiameter.Text) <= 0 Then
                MsgBox("Invalid Diameter", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(txtPcs2.Text) <= 0 Then
                MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(txtCts2.Text) <= 0 Then
                MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If intTotPcs + CDbl(txtPcs2.Text) > intRetPcs Then
                MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If dblTotCts + CDbl(txtCts2.Text) > dblRetCts Then
                MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            strCode = ""
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_RndSizingCodes WHERE Color = '" & cmbColor2.Text & "' AND Clarity = '" & cmbClarity2.Text & "' AND Make = '" & cmbMake.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                strCode = rsComSql.Fields("Code").Value
            End If
            rsComSql = Nothing

            strAssortment = ""
            dblPrice = 0
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT TOP (100) PERCENT DiaSales.dbo.tblDCLPermanents.ItemName, DiaSales.dbo.tblDCLPermanents.ListCost " & _
                          "FROM DiaSales.dbo.tblDCLPermanents INNER JOIN dbo.tblGrading_RndSizeList ON DiaSales.dbo.tblDCLPermanents.ItemName = dbo.tblGrading_RndSizeList.AssortNo " & _
                          "WHERE (DiaSales.dbo.tblDCLPermanents.LengthFrom <= '" & CDbl(txtDiameter.Text) & "') AND (DiaSales.dbo.tblDCLPermanents.LengthTo >= '" & CDbl(txtDiameter.Text) & "') AND (DiaSales.dbo.tblDCLPermanents.MainAssort = '" & strCode & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                strAssortment = rsComSql.Fields("ItemName").Value
                dblPrice = rsComSql.Fields("ListCost").Value
            End If
            rsComSql = Nothing

            flxDetails.Rows.Add(cmbColor2.Text,
                                cmbClarity2.Text,
                                cmbMake.Text,
                                txtDiameter.Text,
                                txtPcs2.Text,
                                txtCts2.Text,
                                cmbCut.Text,
                                cmbSymm.Text,
                                cmbPol.Text,
                                strAssortment,
                                dblPrice)

            txtTotPcs2.Text = intTotPcs + CDbl(txtPcs2.Text)
            txtTotCts2.Text = dblTotCts + CDbl(txtCts2.Text)

            cmbColor2.Text = ""
            cmbClarity2.Text = ""
            cmbMake.Text = ""
            txtDiameter.Text = ""
            txtPcs2.Text = ""
            txtCts2.Text = ""
            cmbCut.Text = "N/A"
            cmbSymm.Text = "N/A"
            cmbPol.Text = "N/A"
        End If
    End Sub

    Private Sub flxDetails_DoubleClick(sender As Object, e As EventArgs) Handles flxDetails.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            txtTotPcs2.Text = CDbl(txtTotPcs2.Text) - CDbl(flxDetails.Item(4, flxDetails.CurrentRow.Index).Value)
            txtTotCts2.Text = CDbl(txtTotCts2.Text) - CDbl(flxDetails.Item(5, flxDetails.CurrentRow.Index).Value)
            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)
        End If
    End Sub

    Private Sub cmbMake_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbMake.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtDiameter.Focus()
        End If
    End Sub

    Private Sub txtCts2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCts2.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtCts2.Text)
        If Asc(e.KeyChar) = 13 Then
            cmdAdd2.Focus()
        End If
    End Sub

    Private Sub cmbCut_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbCut.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbSymm.Focus()
        End If
    End Sub

    Private Sub cmbSymm_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSymm.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbPol.Focus()
        End If
    End Sub

    Private Sub cmbPol_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbPol.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtPcs2.Focus()
        End If
    End Sub

    Private Sub txtMCPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMCPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub
End Class