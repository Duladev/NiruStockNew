
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLFinalRepair

    Private Sub frm_DCLFinalRepair_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_DepartmentProd(cmbDept)
    End Sub

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Dim intPos As Integer
        Dim Instring As String

        Instring = UCase(InputBox("Enter Par/Pkt Number", "Final Repair"))
        If Len(Instring) > 0 Then
            txtParPkt.Text = Trim(Instring)
            If cmbDept.Text = "Rounds" Then
                txtParNo.Text = Mid(txtParPkt.Text, 1, 8)
                txtPktNo.Text = strRight(txtParPkt.Text, 5)
            Else
                intPos = InStr(1, txtParPkt.Text, "/")
                txtParNo.Text = Mid(txtParPkt.Text, 1, intPos - 1)
                txtPktNo.Text = Mid(txtParPkt.Text, intPos + 1, Len(txtParPkt.Text) - intPos)
            End If

            txtPktNo.Focus()

            Show_Details()
        Else
            txtParPkt.Text = ""
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Function ParcelFound(ByVal strDept As String, ByVal strParcelNo As String) As Boolean
        ParcelFound = True
        rsComSql = New ADODB.Recordset
        Select Case strDept
            Case "Baguettes"
                rsComSql.Open("SELECT * FROM tblBAGPacket WHERE ParNo = '" & strParcelNo & "'", AdoCN, 1, 1)
            Case "Princess"
                rsComSql.Open("SELECT * FROM tblPRPacket WHERE ParNo = '" & strParcelNo & "'", AdoCN, 1, 1)
            Case "Rounds"
                rsComSql.Open("SELECT * FROM tblRndPacket WHERE ParNo = '" & strParcelNo & "'", AdoCN, 1, 1)
            Case "Rounds3", "RoundsNLE", "Emerald", "Lamour", "Davinci", "Carrer", "Asscher", "Radiant", "Opening"
                rsComSql.Open("SELECT * FROM tblExtPacket WHERE ParNo = '" & strParcelNo & "' AND Department = '" & strDept & "'", AdoCN, 1, 1)
            Case "Mix"
                rsComSql.Open("SELECT * FROM tblMixPacket WHERE PktOrdNo = '" & strParcelNo & "'", AdoCN, 1, 1)
            Case "Precision"
                rsComSql.Open("SELECT * FROM tblPacket WHERE PktOrdNo = '" & strParcelNo & "'", AdoCN, 1, 1)
            Case "GradingPCU_N"
                rsComSql.Open("SELECT * FROM tblGrading_RepairParcels WHERE Department = '" & strDept & "' AND ParNo = '" & strParcelNo & "'", AdoCN, 1, 1)
            Case Else
                MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ParcelFound = False
                Exit Function
        End Select
        If rsComSql.RecordCount Then
            ParcelFound = True
        Else
            ParcelFound = False
        End If
        rsComSql = Nothing

    End Function

    Private Function PacketFound(ByVal strDept As String, ByVal strParcelNo As String, ByVal strPacketNo As String) As Boolean
        PacketFound = True
        rsComSql = New ADODB.Recordset
        Select Case strDept
            Case "Baguettes", "Princess", "Rounds", "Rounds3", "RoundsNLE", "Emerald", "Lamour", "Davinci", "Carrer", "GradingPCU_N", "Asscher", "Radiant"
                rsComSql.Open("SELECT Pcs, Cts FROM tblGrading_RepairParcels WHERE Department = '" & strDept & "' AND ParNo = '" & strParcelNo & "' AND PktNo = '" & strPacketNo & "'", AdoCN, 1, 1)
            Case "Mix"
                rsComSql.Open("SELECT IssPcsT + IssPcsB AS Pcs, IssCts AS Cts FROM tblMixIssues WHERE ParNo = '" & strParcelNo & "' AND PktNo = '" & strPacketNo & "' AND Sec = 14", AdoCN, 1, 1)
            Case "Precision"
                rsComSql.Open("SELECT IssPcsT + IssPcsB AS Pcs, IssCts AS Cts FROM tblPacket WHERE ParNo = '" & strParcelNo & "' AND PktNo = '" & strPacketNo & "' AND Sec = 14", AdoCN, 1, 1)
            Case Else
                MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                PacketFound = False
                Exit Function
        End Select
        If rsComSql.RecordCount Then
            txtPktPcs.Text = rsComSql.Fields("Pcs").Value
            txtPktCts.Text = rsComSql.Fields("Cts").Value
            PacketFound = True
        Else
            PacketFound = False
        End If
        rsComSql = Nothing

    End Function

    Private Sub Load_Packets()
        flxDetails.Rows.Clear()
        If cmbDept.Text <> "" Then
            txtParNo.Text = UCase(txtParNo.Text)
            'If ParcelFound(cmbDept.Text, txtParNo.Text) = True Then

            'Else
            '    MsgBox("Invalid Department/Parcel", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            'End If

            rsComSql = New ADODB.Recordset
            If txtParNo.Text <> "" Then
                rsComSql.Open("SELECT ParNo, PktNo, Pcs, Cts, RepReason, PktNo2 " & _
                              "FROM tblGrading_RepairParcels " & _
                              "WHERE (Trf = 0) AND (OK = 0) AND (Department = '" & cmbDept.Text & "') AND (ParNo = '" & txtParNo.Text & "') AND (LEFT(PktNo, 1) = 'R' OR LEFT(PktNo, 1) = 'U') " & _
                              "ORDER BY PktNo", AdoCN, 1, 1)
            Else
                rsComSql.Open("SELECT ParNo, PktNo, Pcs, Cts, RepReason, PktNo2 " & _
                              "FROM tblGrading_RepairParcels " & _
                              "WHERE (Trf = 0) AND (OK = 0) AND (Department = '" & cmbDept.Text & "') AND (LEFT(PktNo, 1) = 'R' OR LEFT(PktNo, 1) = 'U') " & _
                              "ORDER BY ParNo, PktNo", AdoCN, 1, 1)
            End If
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                        rsComSql.Fields("PktNo").Value,
                                        rsComSql.Fields("Pcs").Value,
                                        rsComSql.Fields("Cts").Value,
                                        rsComSql.Fields("RepReason").Value,
                                        False,
                                        rsComSql.Fields("PktNo2").Value)

                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Load_Packets()
    End Sub

    Private Sub Show_Details()
        txtPktNo.Text = UCase(txtPktNo.Text)
        If txtParNo.Text <> "" And txtPktNo.Text <> "" Then
            txtParNo.Text = UCase(txtParNo.Text)
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblFinalRepPacket WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                txtPktPcs.Text = rsComSql_1.Fields("PktPcs").Value
                txtPktCts.Text = rsComSql_1.Fields("Pktcts").Value

                rsComSql_3 = New ADODB.Recordset
                rsComSql_3.Open("SELECT * FROM tblGrading_RepairParcels WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                If rsComSql_3.RecordCount Then
                    txtReason.Text = rsComSql_3.Fields("RepReason").Value
                    txtGroup.Text = rsComSql_3.Fields("Grp").Value
                    txtOrgPkt.Text = rsComSql_3.Fields("PktNo2").Value
                End If
                rsComSql_3 = Nothing

                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT * FROM tblFinalRepIssues WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    txtIssPcs.Text = rsComSql_2.Fields("IssPcs").Value
                    txtIssCts.Text = rsComSql_2.Fields("IssCts").Value
                    txtEmpNo.Text = UCase(rsComSql_2.Fields("EmpNo").Value)

                    rsComSql_3 = New ADODB.Recordset
                    rsComSql_3.Open("SELECT SUM(RetPcs + RejPcs + LostPcs) AS RetPcs, ROUND(SUM(RetCts), 3) AS RetCts FROM tblFinalRepReturns WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                    If rsComSql_3.RecordCount Then
                        If Not IsDBNull(rsComSql_3.Fields("RetPcs").Value) Then
                            txtFinPcs.Text = rsComSql_3.Fields("RetPcs").Value
                            txtFinCts.Text = rsComSql_3.Fields("RetCts").Value
                        Else
                            txtFinPcs.Text = "0"
                            txtFinCts.Text = "0"
                        End If

                        If CInt(txtFinPcs.Text) = CInt(txtIssPcs.Text) Then
                            MsgBox("Repair Finished", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            ClearFields()
                            Exit Sub
                        Else
                            cmdSave.Enabled = False
                            cmdIssSave.Enabled = False
                            cmdRetSave.Enabled = True
                            txtRetPcs.ReadOnly = False
                            txtRetCts.ReadOnly = False
                            txtEmpNo.Focus()
                        End If
                    Else
                        txtRetPcs.Focus()
                    End If
                    rsComSql_3 = Nothing

                Else
                    txtIssPcs.Text = rsComSql_1.Fields("PktPcs").Value
                    txtIssCts.Text = rsComSql_1.Fields("Pktcts").Value
                    cmdSave.Enabled = False
                    cmdIssSave.Enabled = True
                    cmdRetSave.Enabled = False
                    txtRetPcs.ReadOnly = True
                    txtRetCts.ReadOnly = True
                    txtEmpNo.Focus()
                End If
                rsComSql_2 = Nothing
            Else
                If ParcelFound(cmbDept.Text, txtParNo.Text) = True Then
                    If PacketFound(cmbDept.Text, txtParNo.Text, txtPktNo.Text) = True Then
                        cmdSave.Enabled = True
                        cmdIssSave.Enabled = False
                        cmdRetSave.Enabled = False
                        txtRetPcs.ReadOnly = True
                        txtRetCts.ReadOnly = True
                    Else
                        MsgBox("Invalid Packet No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        'txtParNo.Text = ""
                        txtPktNo.Text = ""
                        txtPktNo.Focus()
                    End If
                Else
                    MsgBox("Invalid Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    txtParNo.Text = ""
                    txtPktNo.Text = ""
                    txtParNo.Focus()
                End If
            End If
            rsComSql_1 = Nothing
        End If
    End Sub

    Private Sub ClearFields()
        txtParPkt.Text = ""
        txtPktNo.Text = ""
        txtPktPcs.Text = ""
        txtPktCts.Text = ""
        txtIssPcs.Text = ""
        txtIssCts.Text = ""
        txtRetPcs.Text = ""
        txtRetCts.Text = ""
        txtRejPcs.Text = ""
        txtRejCts.Text = ""
        txtLostPcs.Text = ""
        txtLostCts.Text = ""
        txtFinPcs.Text = ""
        txtFinCts.Text = ""
        txtEmpNo.Text = ""
        txtReason.Text = ""
        txtGroup.Text = ""
        txtOrgPkt.Text = ""
        txtRetPcs.ReadOnly = True
        txtRetCts.ReadOnly = True
        cmdIssSave.Enabled = False
        cmdRetSave.Enabled = False
    End Sub

    Private Sub Accept()
        Dim intRow As Integer
        Dim dataok As Boolean

        If cmbDept.Text <> "" Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(5, intRow).Value = True Or flxDetails.Item(5, intRow).Value = 1 Then
                    If chkIssue.Checked = True Then
                        dataok = True
                        If txtEmpNo.Text = "" Then
                            MsgBox("Invalid Emp. No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            dataok = False
                        End If
                        If dataok = False Then Exit Sub

                        rsComSql_1 = New ADODB.Recordset
                        mStrSQL = ("SELECT * FROM VW_EMP_MASTER_SMALL3 WHERE FullEmpNo = '" & txtEmpNo.Text & "'")
                        rsComSql_1.Open(mStrSQL, AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            MsgBox("Invalid Emp. No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            txtEmpNo.Focus()
                            dataok = False
                        End If
                        rsComSql_1 = Nothing
                        If dataok = False Then Exit Sub
                    End If

                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblFinalRepPacket WHERE (Department = '" & cmbDept.Text & "') AND (ParNo = '" & flxDetails.Item(0, intRow).Value & "') AND (PktNo = '" & flxDetails.Item(1, intRow).Value & "')", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        AdoCN.Execute("INSERT INTO tblFinalRepPacket(Department,ParNo,PktNo,PktPcs,PktCts,PktIss,IncUnit) " & _
                                      "VALUES('" & cmbDept.Text & "','" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & CInt(flxDetails.Item(2, intRow).Value) & "'," & _
                                             "'" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','')")

                        AdoCN.Execute("UPDATE tblGrading_RepairParcels SET Trf = 1 WHERE (Department = '" & cmbDept.Text & "') AND (ParNo = '" & flxDetails.Item(0, intRow).Value & "') AND (PktNo = '" & flxDetails.Item(1, intRow).Value & "')")

                        If flxDetails.Item(4, intRow).Value = "Boiling" Then
                            AdoCN.Execute("INSERT INTO tblFinalRepIssues(Department,ParNo,PktNo,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                          "VALUES('" & cmbDept.Text & "','" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','D06975','" & CInt(flxDetails.Item(2, intRow).Value) & "'," & _
                                               "'" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")

                            AdoCN.Execute("INSERT INTO tblFinalRepReturns(Department,ParNo,PktNo,EmpNo,RetPcs,RetCts,RetDate,RetTime,ActCts) " & _
                                          "VALUES('" & cmbDept.Text & "','" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','D06975','" & CInt(flxDetails.Item(2, intRow).Value) & "'," & _
                                               "'" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & CDbl(flxDetails.Item(3, intRow).Value) & "')")
                        End If

                        If chkIssue.Checked = True Then
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT * FROM tblFinalRepIssues WHERE (Department = '" & cmbDept.Text & "') AND (ParNo = '" & flxDetails.Item(0, intRow).Value & "') AND (PktNo = '" & flxDetails.Item(1, intRow).Value & "')", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount = 0 Then
                                AdoCN.Execute("INSERT INTO tblFinalRepIssues(Department,ParNo,PktNo,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                              "VALUES('" & cmbDept.Text & "','" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & txtEmpNo.Text & "','" & CInt(flxDetails.Item(2, intRow).Value) & "'," & _
                                                   "'" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")
                            End If
                            rsComSql_1 = Nothing

                        End If
                    End If
                    rsComSql = Nothing
                End If
            Next
            MsgBox("Successfully Accepted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            flxDetails.Rows.Clear()
            txtEmpNo.Text = ""

        End If
    End Sub

    Private Sub RecSave()
        Dim dataok As Boolean

        dataok = True
        If cmbDept.Text = "" Then
            MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

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

        If txtPktPcs.Text = "" Then
            MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If txtPktCts.Text = "" Then
            MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If CInt(txtPktPcs.Text) <= 0 Then
            MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If CSng(txtPktCts.Text) <= 0 Then
            MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If dataok = True Then
            DataSave()
        End If

    End Sub

    Private Sub DataSave()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblFinalRepPacket WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            AdoCN.Execute("INSERT INTO tblFinalRepPacket(Department,ParNo,PktNo,PktPcs,PktCts,PktIss,IncUnit) " & _
                          "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "','" & CInt(txtPktPcs.Text) & "'," & _
                                 "'" & CDbl(txtPktCts.Text) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','')")

            MsgBox("Packet Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        Else
            MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing
        ClearFields()

    End Sub

    Private Sub RecSaveIssues()
        Dim dataok As Boolean

        dataok = True
        If cmbDept.Text = "" Then
            MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

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

        If txtIssPcs.Text = "" Then
            MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If txtIssCts.Text = "" Then
            MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If txtEmpNo.Text = "" Then
            MsgBox("Invalid Emp. No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        rsComSql = New ADODB.Recordset
        mStrSQL = ("SELECT * FROM VW_EMP_MASTER_SMALL3 WHERE FullEmpNo = '" & txtEmpNo.Text & "'")
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Emp. No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            txtEmpNo.Focus()
            dataok = False
        End If
        rsComSql = Nothing
        If dataok = False Then Exit Sub

        If CInt(txtIssPcs.Text) <= 0 Then
            MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If CSng(txtIssCts.Text) <= 0 Then
            MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If dataok = True Then
            DataSaveIssues()
        End If

    End Sub

    Private Sub DataSaveIssues()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblFinalRepIssues WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            AdoCN.Execute("INSERT INTO tblFinalRepIssues(Department,ParNo,PktNo,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                          "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "','" & txtEmpNo.Text & "','" & CInt(txtIssPcs.Text) & "'," & _
                                 "'" & CDbl(txtIssCts.Text) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:mm") & "')")

            AdoCN.Execute("UPDATE tblGrading_RepairParcels SET Trf = 1 WHERE (Department = '" & cmbDept.Text & "') AND (ParNo = '" & txtParNo.Text & "') AND (PktNo = '" & txtPktNo.Text & "')")

            MsgBox("Packet Issued", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        Else
            MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing
        ClearFields()

    End Sub

    Private Sub RecSaveReturns()
        Dim dataok As Boolean

        dataok = True
        If cmbDept.Text = "" Then
            MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

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

        If txtRetPcs.Text = "" Then
            MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If txtRetCts.Text = "" Then
            MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If txtRejPcs.Text = "" Then
            MsgBox("Invalid Reject Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If txtRejCts.Text = "" Then
            MsgBox("Invalid Reject Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If txtLostPcs.Text = "" Then
            MsgBox("Invalid Lost Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If txtLostCts.Text = "" Then
            MsgBox("Invalid Lost Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If txtEmpNo.Text = "" Then
            MsgBox("Invalid Emp. No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        rsComSql = New ADODB.Recordset
        mStrSQL = ("SELECT * FROM VW_EMP_MASTER_SMALL3 WHERE FullEmpNo = '" & txtEmpNo.Text & "'")
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Emp. No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            txtEmpNo.Focus()
            dataok = False
        End If
        rsComSql = Nothing
        If dataok = False Then Exit Sub

        If CInt(txtRetPcs.Text) + CInt(txtRejPcs.Text) + CInt(txtLostPcs.Text) <= 0 Then
            MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If CSng(txtRetCts.Text) + CSng(txtRejCts.Text) + CSng(txtLostCts.Text) <= 0 Then
            MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If CInt(txtRetPcs.Text) + CInt(txtRejPcs.Text) + CInt(txtLostPcs.Text) <> CInt(txtIssPcs.Text) Then
            MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If txtRejPcs.Text = "" Then txtRejPcs.Text = "0"
        If txtRejCts.Text = "" Then txtRejCts.Text = "0"
        If txtLostPcs.Text = "" Then txtLostPcs.Text = "0"
        If txtLostCts.Text = "" Then txtLostCts.Text = "0"

        'If cmbDept.Text = "Rounds" Then
        '    If Math.Round(CSng(txtRetCts.Text) + CSng(txtRejCts.Text) + CSng(txtLostCts.Text), 3) <> Math.Round(CSng(txtIssCts.Text), 3) Then
        '        MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '        dataok = False
        '    End If
        'Else
        '    If Math.Round(CSng(txtRetCts.Text) + CSng(txtRejCts.Text) + CSng(txtLostCts.Text), 3) > Math.Round(CSng(txtIssCts.Text), 3) Then
        '        MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '        dataok = False
        '    End If
        'End If
        If Math.Round(CSng(txtRetCts.Text) + CSng(txtRejCts.Text) + CSng(txtLostCts.Text), 3) > Math.Round(CSng(txtIssCts.Text), 3) Then
            MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If dataok = True Then
            DataSaveReturns()
        End If

    End Sub

    Private Sub DataSaveReturns()
        Dim intPktLen As Integer
        Dim strNewPktNo As String
        Dim dblMaxID As Double
        Dim dtpIssDate As Date
        Dim intHours As Integer
        Dim intDays As Integer
        Dim dtpCheckDate As Date

        strNewPktNo = ""
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblFinalRepReturns WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            If intCheckRepairDate = 1 Then
                If cmbDept.Text = "Rounds" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblFinalRepIssues WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        dtpIssDate = Format(rsComSql_1.Fields("IssDate").Value, "MM/dd/yyyy") & " " & Format(rsComSql_1.Fields("IssTime").Value, "HH:MM")
                    End If
                    rsComSql_1 = Nothing

                    intHours = DateDiff("H", dtpIssDate, Now)
                    intDays = DateDiff("D", dtpIssDate, Now)

                    For intIndex = 0 To intDays
                        dtpCheckDate = DateAdd("D", intIndex, Format(dtpIssDate, "MM/dd/yyyy"))

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM VW_EMP_HOLIDAYS WHERE HDate = '" & Format(dtpCheckDate, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            intHours = intHours - 24
                        End If
                        rsComSql_1 = Nothing
                    Next

                    If intHours > 24 Then
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM tblGrading_RepairParcels WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            AdoCN.Execute("UPDATE tblRndReturns SET NopayPcs = RetPcsT + RetPcsB WHERE ParNo = '" & txtParNo.Text & "'  AND PktNo = '" & rsComSql_1.Fields("PktNo2").Value & "'")
                        End If
                        rsComSql_1 = Nothing
                    End If
                End If
            End If
            

            AdoCN.Execute("INSERT INTO tblFinalRepReturns(Department,ParNo,PktNo,EmpNo,RetPcs,RetCts,RetDate,RetTime,ActCts,RejPcs,RejCts,LostPcs,LostCts) " & _
                          "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "','" & txtEmpNo.Text & "','" & CInt(txtRetPcs.Text) & "'," & _
                                 "'" & CDbl(txtRetCts.Text) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & CDbl(txtRetCts.Text) & "'," & _
                                 "'" & CInt(txtRejPcs.Text) & "','" & CDbl(txtRejCts.Text) & "','" & CInt(txtLostPcs.Text) & "','" & CDbl(txtLostCts.Text) & "')")

            AdoCN.Execute("UPDATE tblGrading_RepairParcels SET Trf = 1 WHERE (Department = '" & cmbDept.Text & "') AND (ParNo = '" & txtParNo.Text & "') AND (PktNo = '" & txtPktNo.Text & "')")

            If chkBoling.Checked = True Then
                AdoCN.Execute("UPDATE tblFinalRepReturns SET Trf = 1, TrfDate = '" & Format(Date.Now, "MM/dd/yyyy") & "' WHERE (Department = '" & cmbDept.Text & "') AND (Trf = 0) AND (ParNo = '" & txtParNo.Text & "') AND (PktNo = '" & txtPktNo.Text & "')")

                intPktLen = 4
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT MAX(LEN(PktNo)) AS PktLen FROM tblGrading_RepairParcelsA WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND LEFT(PktNo, 1) = 'P'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("PktLen").Value) Then
                        intPktLen = rsComSql_1.Fields("PktLen").Value
                    End If
                End If
                rsComSql_1 = Nothing

                strNewPktNo = "P0001"
                If intPktLen = 4 Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT MAX(RIGHT(PktNo,3)) AS MaxPktNo FROM tblGrading_RepairParcelsA WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND LEFT(PktNo, 1) = 'P' AND LEN(PktNo) = 4", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        If Not IsDBNull(rsComSql_1.Fields("MaxPktNo").Value) Then
                            strNewPktNo = "P" & Format(rsComSql_1.Fields("MaxPktNo").Value + 1, "000")
                        Else
                            strNewPktNo = "P001"
                        End If
                    End If
                    rsComSql_1 = Nothing
                Else
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT MAX(RIGHT(PktNo,4)) AS MaxPktNo FROM tblGrading_RepairParcelsA WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND LEFT(PktNo, 1) = 'P' AND LEN(PktNo) = 5", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        If Not IsDBNull(rsComSql_1.Fields("MaxPktNo").Value) Then
                            strNewPktNo = "P" & Format(rsComSql_1.Fields("MaxPktNo").Value + 1, "0000")
                        Else
                            strNewPktNo = "P0001"
                        End If
                    End If
                    rsComSql_1 = Nothing
                End If

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblGrading_RepairParcelsA WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & strNewPktNo & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount = 0 Then
                    AdoCN.Execute("INSERT INTO tblGrading_RepairParcelsA(Department,ParNo,PktNo,Pcs,Cts,IssDate,IssTime,OK,Grp,PktNo2) " & _
                                  "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & strNewPktNo & "'," & CDbl(txtRetPcs.Text) & "," & CDbl(txtRetCts.Text) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,'" & UCase(txtGroup.Text) & "','" & txtOrgPkt.Text & "')")

                    AdoCN.Execute("UPDATE tblGrading_RepairParcels SET OK = 1 WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'")

                    Dep_Grading_Trf(cmbDept.Text, 9996, txtParNo.Text, strNewPktNo, txtRetPcs.Text, txtRetCts.Text, txtPktPcs.Text, txtPktCts.Text, UCase(txtGroup.Text))

                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT MAX(ID) AS MaxID FROM tblGradingTrf_Diff", AdoCN, 1, 1)
                    dblMaxID = rsComSql_1.Fields("MaxID").Value
                    rsComSql_1 = Nothing

                    GradingAcceptations(txtParNo.Text, strNewPktNo, dblMaxID, 0)

                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblGrading_BoilingIssues WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & strNewPktNo & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount = 0 Then
                        'Boiling Issues
                        AdoCN.Execute("INSERT INTO tblGrading_BoilingIssues(Department,ParNo,PktNo,EmpNo,IssPcs,IssCts,IssDate,IssTime,UserName,Remarks,Grp) " & _
                                      "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & strNewPktNo & "','D08877'," & CInt(txtRetPcs.Text) & "," & _
                                             "" & CDbl(txtRetCts.Text) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "','','" & Trim(txtGroup.Text) & "')")

                        'Boiling Returns
                        AdoCN.Execute("INSERT INTO tblGrading_BoilingReturns(Department,ParNo,PktNo,EmpNo,RetPcs,RetCts, " & _
                                            "LostPcs,LostCts,RejPcs,RejCts,RetDate,RetTime,UserName,Trf,Grp) " & _
                                      "VALUES ('" & cmbDept.Text & "','" & txtParNo.Text & "','" & strNewPktNo & "','" & PBUser_EmpNo & "'," & CInt(txtRetPcs.Text) & "," & CDbl(txtRetCts.Text) & ",0,0,0" & _
                                            ",0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "',0,'" & Trim(txtGroup.Text) & "')")

                        'Checking Issues
                        AdoCN.Execute("INSERT INTO tblGrading_CheckingIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime,UserName,Grp) " & _
                                      "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & strNewPktNo & "',3,'" & PBUser_EmpNo & "','" & CInt(txtRetPcs.Text) & "'," & _
                                           "'" & CDbl(txtRetCts.Text) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "','" & Trim(txtGroup.Text) & "')")
                    End If
                    rsComSql_1 = Nothing
                End If
                rsComSql_1 = Nothing
            End If

            If chkBoling.Checked = True Then
                MsgBox("Packet Returned and Issued to Checking - " & strNewPktNo, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Else
                MsgBox("Packet Returned", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing
        ClearFields()

    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        RecSave()
    End Sub

    Private Sub cmdIssSave_Click(sender As Object, e As EventArgs) Handles cmdIssSave.Click
        RecSaveIssues()
    End Sub

    Private Sub cmdRetSave_Click(sender As Object, e As EventArgs) Handles cmdRetSave.Click
        RecSaveReturns()
    End Sub

    Private Sub cmdAccept_Click(sender As Object, e As EventArgs) Handles cmdAccept.Click
        Accept()
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(5, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(5, intRow).Value = False
            Next
        End If
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptFinalRepMovement.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptFinalRepairStockSum.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptFinalRepairStockEmp.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button4_Click(sender As Object, e As EventArgs) Handles HazelDev_Button4.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptFinalRepairTransfer.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button5_Click(sender As Object, e As EventArgs) Handles HazelDev_Button5.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptFinalRepairStock.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtParNo.Text = UCase(txtParNo.Text)
            If ParcelFound(cmbDept.Text, txtParNo.Text) = True Then
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
            Show_Details()
        End If
    End Sub

    Private Sub txtEmpNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEmpNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If Len(txtEmpNo.Text) = 6 Then
                If cmdIssSave.Enabled = True Then
                    cmdIssSave.Focus()
                Else
                    If cmdRetSave.Enabled = True Then
                        txtRetPcs.Focus()
                    End If
                End If
            Else
                txtEmpNo.Focus()
            End If
        End If
    End Sub

    Private Sub txtRetPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRetPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtRetCts.Focus()
        End If
    End Sub

    Private Sub txtRetCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRetCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRetCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtRejPcs.Text = "0"
            txtRejCts.Text = "0"
            txtLostPcs.Text = "0"
            txtLostCts.Text = "0"
            cmdRetSave.Focus()
            'txtRejPcs.Focus()
        End If
    End Sub

    Private Sub txtRejPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRejPcs.KeyPress
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
            cmdRetSave.Focus()
        End If
    End Sub

    Private Sub cmbDept_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbDept.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtParNo.Focus()
        End If
    End Sub

    Private Sub HazelDev_Button6_Click(sender As Object, e As EventArgs) Handles HazelDev_Button6.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptFinalRepairStockPCUN.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button7_Click(sender As Object, e As EventArgs) Handles HazelDev_Button7.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptFinalRepairStockEmpDelay.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub
End Class