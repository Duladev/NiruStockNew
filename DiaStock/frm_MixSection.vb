
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixSection
    Dim recIssues, recReturns, rstIssues, rstReturns As ADODB.Recordset
    Dim OK, Temp As Object
    Dim mGrp, strmsg, mFlow As String
    Dim Section As Integer
    Dim Caretspkt, Carets As Single
    Dim ICNo2 As String
    Dim issued, frmnew As Boolean
    Dim recno As Long
    Dim Instring$
    Dim Instring1 As String
    Dim PCsPkt As Integer
    Dim AvgCtspkt As Single
    Dim ParcelNo As String
    Dim PacketNo As String
    Dim strSupParcelNo As String
    Dim strOrigin As String

    Private Sub Load_Section()
        Dim recsection As ADODB.Recordset

        cmbSection.Items.Clear()
        recsection = New ADODB.Recordset
        recsection.Open("SELECT * FROM tblMixSections ORDER BY SecCode", AdoCN, 1, 1)
        If recsection.RecordCount Then
            recsection.MoveFirst()
            Do
                If PBDesignation = "Princess" Then
                    cmbSection.Items.Add(recsection.Fields("SecName2").Value)
                ElseIf PBDesignation = "Baguettes" Then
                    cmbSection.Items.Add(recsection.Fields("SecName3").Value)
                Else
                    cmbSection.Items.Add(recsection.Fields("SecName").Value)
                End If
                recsection.MoveNext()
            Loop Until recsection.EOF
        End If
        cmbSection.SelectedIndex = 0
        Section = 1

    End Sub

    Private Sub Load_RejReasons()
        cmbRejReason.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblMixRejReasons ORDER BY RejReason", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbRejReason.Items.Add(rsComSql.Fields("RejReason").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_RepReasons()
        cmbRepReason.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblMixRepReasons ORDER BY RepReason", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbRepReason.Items.Add(rsComSql.Fields("RepReason").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub ClearFields()
        txtParPkt.Text = ""
        txtEmp.Text = ""
        txtRetCts.Text = ""
        txtRetTap.Text = ""
        txtRetBag.Text = ""
        txtIssCts.Text = ""
        txtIssTap.Text = ""
        txtIssBag.Text = ""
        txtRej.Text = ""
        txtBro.Text = ""
        txtLost.Text = ""
        txtExt.Text = ""
        txtRep.Text = ""
        txtRepPcs.Text = ""
        txtNoPay.Text = ""
        frmnew = True
        txtIssDate.Text = ""
        txtIssTime.Text = ""
        txtRetDate.Text = Format(Date.Now, "yyyy/MM/dd")
        txtRetTime.Text = Format(Date.Now, "HH:mm:ss")
        txtRejCts.Text = ""
        txtLostCts.Text = ""
        txtGrPcs.Text = ""
        txtIncPcs.Text = ""
        cmbRejReason.Text = ""
        cmbRepReason.Text = "Repair"
        txtYield1.Text = ""
        txtYield2.Text = ""
        flxDetails.Rows.Clear()
        flxRepair.Rows.Clear()
        txtTotBag.Text = "0"
        txtTotTap.Text = "0"
        txtTotCts.Text = "0.000"
        txtBalCts.Text = "0.000"
        txtAssortment.Text = ""
        cmbEmpNo.Text = ""
        txtGrHeight.Text = "0"
        PictureBox1.Visible = False
        PictureBox2.Visible = False
        txtClient.Text = ""
        txtID100.Text = ""
        txtAms2.Text = ""
        txtDfi.Text = ""
        txtLabID.Text = "0"
        txtFinCts.Text = ""
        chkLaser.Checked = False
        txtGroup.Text = ""
        cmdParPkt.Focus()
    End Sub

    Private Sub frm_MixSection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        ClearFields()
        Load_Section()
        Load_RejReasons()
        Load_RepReasons()
        Load_EmpNo()
    End Sub

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Datavalid = False
        Parcel = True
        Instring = UCase(InputBox("Enter Par/Pkt Number"))
        ParcelLen = Len(Instring)
        If ParcelLen = 9 Then
            Datavalid = True

            ParcelNo = Mid(Instring, 1, ParcelLen - 3)
            PacketNo = strRight(Instring, 3)

        ElseIf ParcelLen = 10 Then
            Datavalid = True

            ParcelNo = Mid(Instring, 1, ParcelLen - 4)
            PacketNo = strRight(Instring, 4)
        End If

        If Datavalid = True Then
            txtParPkt.Text = ParcelNo & "/" & PacketNo
            cmdEmp.Enabled = True
            cmdEmp.Focus()
            txtEmp.Text = ""
            ShowDetails()
        Else
            txtParPkt.Text = ""
            txtEmp.Text = ""
            cmdEmp.Enabled = False
        End If
    End Sub

    Private Sub cmdEmp_Click(sender As Object, e As EventArgs) Handles cmdEmp.Click
        Datavalid = False
        Parcel = False
        Instring = UCase(InputBox("Enter Emp No"))
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
        Else
            txtEmp.Text = ICNo
            txtIssDate.Text = Format(Date.Now, "dd/MM/yyyy")
            txtIssTime.Text = Format(Date.Now, "HH:mm")
            cmdEmp.Focus()
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ShowDetails()
        Dim mfldname As String
        Dim Rs As ADODB.Recordset
        Dim rs2 As ADODB.Recordset
        Dim rs3 As ADODB.Recordset
        Dim rs4 As ADODB.Recordset
        Dim rs5 As ADODB.Recordset
        Dim mIssPcs, mRetPcs, mFlowCount As Long
        Dim strGroup As String
        Dim intSection As Integer

        cmdEmp.Focus()
        chkReject.Checked = False

        mStrSQL = "SELECT * FROM dbo.tblMixPacket " & _
                  "WHERE (PktOrdNo = '" & ParcelNo & "') AND (PktNo = '" & PacketNo & "') AND (Ok = 1) AND (Accept = 1)"
        rs2 = New ADODB.Recordset
        rs2.Open(mStrSQL, AdoCN, 1, 1)
        If rs2.RecordCount > 0 Then
            strGroup = Trim(rs2.Fields("Grp").Value)

            If rs2.Fields("RejectRep").Value = 1 Then
                chkReject.Checked = True
            End If

            If strGroup = "" Then
                MsgBox("Invalid Group", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            txtGroup.Text = strGroup
            Caretspkt = rs2.Fields("PktCts").Value
            PCsPkt = rs2.Fields("PktPcs").Value
            If (rs2.Fields("PktPcs").Value) <> 0 Then
                AvgCtspkt = Math.Round((Caretspkt / PCsPkt), 3)
                'txtRejRgh.Text = AvgCtspkt
                'txtLstRgh.Text = AvgCtspkt
            End If

            chkGroove.Checked = False
            rs5 = New ADODB.Recordset
            rs5.Open("SELECT OrderNo FROM tblOrdersDtls WHERE OrderNo = '" & ParcelNo & "' AND RefNo = '" & Replace(rs2.Fields("PktRefNo").Value, "'", "''") & "' AND Side = '" & rs2.Fields("PktSide").Value & "' AND Groove = 1", AdoCN, 1, 1)
            If rs5.RecordCount Then
                chkGroove.Checked = True
            End If
            rs5 = Nothing

            txtClient.Text = ""
            rs5 = New ADODB.Recordset
            rs5.Open("SELECT Niruref FROM tblOrders WHERE OrderNo = '" & ParcelNo & "'", AdoCN, 1, 1)
            If rs5.RecordCount Then
                txtClient.Text = rs5.Fields("Niruref").Value
            End If
            rs5 = Nothing

            mFlow = rs2.Fields("PktFlow").Value
            txtAssortment.Text = rs2.Fields("AssortNo").Value
        Else
            MsgBox("Packet is not Verified/Accepted", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        mStrSQL = "SELECT * FROM tblMixFlow WHERE Flow = '" & mFlow & "'"
        rs5 = New ADODB.Recordset
        rs5.Open(mStrSQL, AdoCN, 1, 1)
        If rs5.RecordCount Then
            mFlowCount = rs5.Fields("FlowSections").Value
        Else
            MsgBox("Invalid Flow : " & mFlow, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        Rs = New ADODB.Recordset
        Rs.Open("SELECT tblMixIssues.Sec, tblMixIssues.SecCount, tblMixIssues.ParNo, tblMixIssues.PktNo, tblMixReturns.RetCts, tblMixIssues.IssDate, tblMixIssues.IssTime, " & _
                    "tblMixReturns.RetDate, tblMixReturns.RetTime, tblMixIssues.EmpNo AS EmpIss, tblMixReturns.EmpNo, tblMixIssues.IssPcsT, tblMixIssues.IssPcsB, tblMixIssues.IssCts, " & _
                    "tblMixReturns.RetPcsT, tblMixReturns.RetPcsB, tblMixReturns.RejPcs, tblMixReturns.BroPcs, tblMixReturns.LostPcs, tblMixReturns.ExtPcs, tblMixReturns.NopayPcs, tblMixReturns.RepPcs " & _
                "FROM tblMixIssues LEFT OUTER JOIN tblMixReturns ON tblMixIssues.Sec = tblMixReturns.Sec AND tblMixIssues.ParNo = tblMixReturns.ParNo AND tblMixIssues.PktNo = tblMixReturns.PktNo " & _
                "WHERE (tblMixIssues.ParNo = '" & ParcelNo & "') AND (tblMixIssues.PktNo = '" & PacketNo & "') " & _
                "ORDER BY tblMixIssues.Seccount DESC", AdoCN, 1, 1)

        If Rs.RecordCount > 0 Then

            frmnew = False
            If Not IsDBNull(Rs.Fields("RetCts").Value) Then

                Section = Rs.Fields("Sec").Value

                Dim mSecret As String
                rs4 = New ADODB.Recordset

                mStrSQL = "SELECT tblMixIssues.Sec, tblMixIssues.ParNo, tblMixIssues.PktNo, SUM(tblMixReturns.RetCts) AS SumOfRetCts, tblMixIssues.IssPcsT, tblMixIssues.IssPcsB, " & _
                            "tblMixIssues.IssCts,SUM(tblMixReturns.RetPcsT) AS SumOfRetPcsT, SUM(tblMixReturns.RetPcsB) AS SumOfRetPcsB, SUM(tblMixReturns.RejPcs) AS SumOfRej," & _
                            "SUM(tblMixReturns.BroPcs) AS SumOfBro, SUM(tblMixReturns.LostPcs) AS SumOfLost, SUM(tblMixReturns.ExtPcs) AS SumOfExt, SUM(tblMixReturns.NopayPcs)AS SumOfNopay, " & _
                            "SUM(tblMixReturns.RepPcs) AS SumOfRepair " & _
                         "FROM tblMixIssues INNER JOIN tblMixReturns ON tblMixIssues.PktNo = tblMixReturns.PktNo AND tblMixIssues.ParNo = tblMixReturns.ParNo AND tblMixIssues.Sec = tblMixReturns.Sec " & _
                         "GROUP BY tblMixIssues.Sec, tblMixIssues.ParNo, tblMixIssues.PktNo, tblMixIssues.IssPcsT, tblMixIssues.IssPcsB, tblMixIssues.IssCts " & _
                         "HAVING (tblMixIssues.Sec = '" & Section & "') AND (tblMixIssues.ParNo = '" & ParcelNo & "') AND (tblMixIssues.PktNo = '" & PacketNo & "') " & _
                         "ORDER BY tblMixIssues.Sec DESC"

                rs4.Open(mStrSQL, AdoCN, 1, 1)

                mIssPcs = rs4.Fields("IssPcsT").Value + rs4.Fields("IssPcsB").Value
                mRetPcs = rs4.Fields("SumOfRetPcsT").Value + rs4.Fields("SumOfRetPcsB").Value + rs4.Fields("SumOfRej").Value + rs4.Fields("SumOfLost").Value + rs4.Fields("SumOfBro").Value - rs4.Fields("SumOfExt").Value
                rs4 = Nothing
                '.....................
                If mIssPcs <> mRetPcs Then
                    'Me.Close()
                    'Exit Sub

                    txtIssDate.Text = Format(Rs.Fields("IssDate").Value, "dd/MM/yyyy")
                    txtIssTime.Text = Format(Rs.Fields("IssTime").Value, "HH:mm")

                    ICNo2 = Rs.Fields("EmpIss").Value
                    txtEmp.Text = Rs.Fields("EmpIss").Value
                    cmbSection.SelectedIndex = Section - 1
                    txtIssTap.Text = Rs.Fields("IssPcsT").Value
                    txtIssBag.Text = Rs.Fields("IssPcsB").Value
                    txtIssCts.Text = Format(Rs.Fields("IssCts").Value, "#0.000")
                    txtIssTap.Enabled = False
                    txtIssBag.Enabled = False
                    txtIssCts.Enabled = False

                    flxDetails.Rows.Clear()
                    rs3 = New ADODB.Recordset
                    rs3.Open("SELECT * FROM VWMixTotalRetEmp WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' And Sec = '" & Section & "' ORDER BY Sec", AdoCN, 1, 1)
                    If Rs.RecordCount Then
                        rs3.MoveFirst()
                        While Not rs3.EOF
                            flxDetails.Rows.Add(rs3.Fields("EmpNo").Value,
                                                Format(rs3.Fields("RetDate").Value, "yyyy/MM/dd"),
                                                rs3.Fields("sumofrett").Value,
                                                rs3.Fields("sumofretb").Value,
                                                Format(rs3.Fields("sumofcts").Value, "#0.000"),
                                                rs3.Fields("sumofrejp").Value,
                                                rs3.Fields("sumofrejc").Value,
                                                rs3.Fields("sumoflostp").Value,
                                                rs3.Fields("sumofbro").Value,
                                                rs3.Fields("SumofExt").Value)

                            rs3.MoveNext()
                        End While
                    End If
                    rs3 = Nothing

                    txtTotBag.Text = "0"
                    txtTotTap.Text = "0"
                    txtTotCts.Text = "0.000"
                    txtBalCts.Text = "0.000"
                    rs3 = New ADODB.Recordset
                    rs3.Open("SELECT SUM(RetPcsT) AS TotTap, SUM(RetPcsB + RejPcs + LostPcs + BroPcs - ExtPcs) AS TotBag, ROUND(SUM(RetCts + RejCts), 3) AS RetCts " & _
                             "FROM dbo.tblMixReturns WHERE ParNo = '" & ParcelNo & "' And PktNo = '" & PacketNo & "' And Sec = '" & Section & "'", AdoCN, 1, 1)
                    If rs3.RecordCount Then
                        If Not IsDBNull(rs3.Fields("TotTap").Value) Then
                            txtTotTap.Text = rs3.Fields("TotTap").Value
                            txtTotBag.Text = rs3.Fields("TotBag").Value
                            txtTotCts.Text = Format(rs3.Fields("RetCts").Value, "#0.000")
                            txtBalCts.Text = Format(CDbl(txtIssCts.Text) - rs3.Fields("RetCts").Value, "#0.000")
                        End If
                    End If
                    rs3 = Nothing

                    If Section = 14 Or Section = 18 Then
                        cmbEmpNo.Text = "D05502"
                    End If

                    issued = True
                    PictureBox1.Visible = True
                    PictureBox2.Visible = True
                Else
                    If Rs.Fields("SecCount").Value < mFlowCount Then
                        intSection = Section
                        If CheckUrgent(ParcelNo) = False Then
                            MsgBox("Packet is not urgent", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                            ClearFields()
                            PictureBox1.Visible = False
                            PictureBox2.Visible = False
                            Exit Sub
                        End If

                        issued = False
                        PictureBox1.Visible = False
                        PictureBox2.Visible = True
                        mfldname = "Flsec" & Rs.Fields("seccount").Value + 1
                        Section = rs5.Fields(mfldname).Value
                        cmbSection.SelectedIndex = Section - 1

                        Section = Rs.Fields("sec").Value

                        mSecret = ""
                        rs4 = New ADODB.Recordset

                        mStrSQL = "SELECT SUM(RetCts) AS SumOfRetCts, SUM(RetPcsT) AS SumOfRetPcsT, SUM(RetPcsB) AS SumOfRetPcsB " & _
                                  "FROM dbo.tblMixReturns " & _
                                  "WHERE (ParNo = '" & ParcelNo & "') AND (PktNo = '" & PacketNo & "') AND (Sec = '" & Section & "') "

                        'mStrSQL = "SELECT tblMixIssues.Sec, tblMixIssues.ParNo, tblMixIssues.PktNo, SUM(tblMixReturns.RetCts) AS SumOfRetCts,tblMixIssues.EmpNo, " & _
                        '            "tblMixIssues.IssPcsT, tblMixIssues.IssPcsB, tblMixIssues.IssCts,SUM(tblMixReturns.RetPcsT) AS SumOfRetPcsT, SUM(tblMixReturns.RetPcsB) AS SumOfRetPcsB, " & _
                        '            "SUM(tblMixReturns.RejPcs) AS SumOfRej,SUM(tblMixReturns.BroPcs) AS SumOfBro, SUM(tblMixReturns.LostPcs) AS SumOfLost, SUM(tblMixReturns.ExtPcs) AS SumOfExt, " & _
                        '            "SUM(tblMixReturns.NopayPcs)AS SumOfNopay, SUM(tblMixReturns.RepPcs) AS SumOfRepair " & _
                        '         "FROM tblMixIssues INNER JOIN tblMixReturns ON tblMixIssues.PktNo = tblMixReturns.PktNo AND tblMixIssues.ParNo = tblMixReturns.ParNo AND tblMixIssues.Sec = tblMixReturns.Sec " & _
                        '         "GROUP BY tblMixIssues.Sec, tblMixIssues.ParNo, tblMixIssues.PktNo,tblMixIssues.EmpNo, tblMixIssues.IssPcsT, tblMixIssues.IssPcsB, tblMixIssues.IssCts " & _
                        '         "HAVING (tblMixIssues.Sec = '" & Section & "') AND (tblMixIssues.ParNo = '" & ParcelNo & "') AND (tblMixIssues.PktNo = '" & PacketNo & "') " & _
                        '         "ORDER BY tblMixIssues.Sec DESC"


                        rs4.Open(mStrSQL, AdoCN, 1, 1)

                        txtIssTap.Text = rs4.Fields("SumOfRetPcsT").Value
                        txtIssBag.Text = rs4.Fields("SumOfRetPcsB").Value
                        txtIssCts.Text = Format(rs4.Fields("SumOfRetCts").Value, "#0.000")

                        txtIssTap.Enabled = False
                        txtIssBag.Enabled = False
                        txtIssCts.Enabled = False

                        Section = Rs.Fields("seccount").Value

                        Rs = Nothing
                        rs4 = Nothing

                        If intSection = 14 And chkGroove.Checked = True Then
                            MsgBox("This is for Groove", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        End If
                    Else
                        PictureBox1.Visible = False
                        PictureBox2.Visible = False

                        MsgBox("Completed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Rs = Nothing
                        ClearFields()

                    End If
                End If

            Else
                'Section return
                issued = True
                PictureBox1.Visible = True
                PictureBox2.Visible = True

                recno = Rs.Fields(0).Value
                txtIssDate.Text = Format(Rs.Fields("IssDate").Value, "dd/MM/yyyy")
                txtIssTime.Text = Format(Rs.Fields("IssTime").Value, "HH:mm")

                mfldname = "Flsec" & Rs.Fields("seccount").Value
                Section = rs5.Fields(mfldname).Value
                cmbSection.SelectedIndex = Section - 1

                If Section = 14 Or Section = 18 Then
                    cmbEmpNo.Text = "D05502"
                End If

                Section = Rs.Fields("seccount").Value

                ICNo2 = Rs.Fields("EmpIss").Value
                txtEmp.Text = Rs.Fields("EmpIss").Value

                txtIssTap.Text = Rs.Fields("IssPcsT").Value
                txtIssBag.Text = Rs.Fields("IssPcsB").Value
                txtIssCts.Text = Format(Rs.Fields("IssCts").Value, "#0.000")
                txtIssTap.Enabled = False
                txtIssBag.Enabled = False
                txtIssCts.Enabled = False
            End If

        Else
            'Section Issue/Return entries not found. New issue
            intSection = Section
            If CheckUrgent(ParcelNo) = False Then
                MsgBox("Packet is not urgent", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
                PictureBox1.Visible = False
                PictureBox2.Visible = False
                Exit Sub
            End If

            frmnew = True
            issued = False
            PictureBox2.Visible = True
            Rs = Nothing
            Section = 0
            cmbSection.SelectedIndex = Section
            txtIssTap.Text = rs2.Fields("PktPcs").Value
            txtIssBag.Text = "0"
            txtIssCts.Text = Format(Caretspkt, "#0.000")
            cmdEmp.Focus()
        End If
        rs2 = Nothing
        rs5 = Nothing

        Exit Sub
GoOut:

    End Sub

    Private Function CheckUrgent(ByVal strParcelNo As String) As Boolean
        'Dim dtpShipmentDate As Date
        'Dim dtpShipmentDateAd As Date

        'Dim dtpToday As Date

        CheckUrgent = True

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT OrderNo FROM tblPlaneOrders WHERE OrderNo = '" & strParcelNo & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            CheckUrgent = True
        Else
            CheckUrgent = False
        End If
        rsComSql_1 = Nothing

        'dtpShipmentDate = Format(Date.Now, "yyyy/MM/dd")
        'dtpToday = Format(Date.Now, "yyyy/MM/dd")
        'rsComSql_1 = New ADODB.Recordset
        'rsComSql_1.Open("SELECT OrderDate FROM tblPlaneOrders WHERE OrderDate >= '" & Format(dtpToday, "yyyy/MM/dd") & "' ORDER BY OrderDate", AdoCN, 1, 1)
        'If rsComSql_1.RecordCount Then
        '    rsComSql_1.MoveFirst()
        '    dtpShipmentDate = rsComSql_1.Fields("OrderDate").Value
        'End If
        'rsComSql_1 = Nothing

        'dtpShipmentDateAd = DateAdd("D", 5, Format(dtpShipmentDate, "yyyy/MM/dd"))

        'rsComSql_1 = New ADODB.Recordset
        'rsComSql_1.Open("SELECT Urgent FROM tblOrders WHERE OrderNo = '" & strParcelNo & "' AND Urgent = 1", AdoCN, 1, 1)
        'If rsComSql_1.RecordCount Then
        '    CheckUrgent = True
        'Else
        '    CheckUrgent = False
        'End If
        'rsComSql_1 = Nothing

        'If CheckUrgent = True Then
        '    Exit Function
        'End If

        'rsComSql_1 = New ADODB.Recordset
        'rsComSql_1.Open("SELECT OrderNo FROM tblPlaneOrders WHERE OrderDate = '" & Format(dtpShipmentDate, "yyyy/MM/dd") & "' AND OrderNo = '" & strParcelNo & "'", AdoCN, 1, 1)
        'If rsComSql_1.RecordCount = 0 Then
        '    rsComSql_2 = New ADODB.Recordset
        '    rsComSql_2.Open("SELECT TOP (100) PERCENT OrderNo, DueDate, Complete " & _
        '             "FROM dbo.tblOrders " & _
        '             "WHERE (Complete = 'N') AND (DueDate <= '" & Format(dtpShipmentDateAd, "yyyy/MM/dd") & "') AND (OrderNo = '" & strParcelNo & "')", AdoCN, 1, 1)
        '    If rsComSql_2.RecordCount = 0 Then
        '        dtpShipmentDateAd = DateAdd("D", 75, Format(dtpShipmentDate, "yyyy/MM/dd"))
        '        rsComSql_3 = New ADODB.Recordset
        '        rsComSql_3.Open("SELECT TOP (100) PERCENT OrderNo, DueDate, Complete " & _
        '                 "FROM dbo.tblOrders " & _
        '                 "WHERE (Complete = 'N') AND (DueDate <= '" & Format(dtpShipmentDateAd, "yyyy/MM/dd") & "') AND (OrderNo = '" & strParcelNo & "')", AdoCN, 1, 1)
        '        If rsComSql_3.RecordCount = 0 Then
        '            CheckUrgent = False
        '        End If
        '        rsComSql_3 = Nothing
        '    End If
        '    rsComSql_2 = Nothing
        'End If
        'rsComSql_1 = Nothing

    End Function

    Private Sub cmdByPass_Click(sender As Object, e As EventArgs) Handles cmdByPass.Click
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            ByPassSection()
            txtSection.Text = ""
            ClearFields()
        End If
    End Sub

    Private Sub ByPassSection()
        Dim Rs1 As ADODB.Recordset
        Dim rs2 As ADODB.Recordset
        Dim rs3 As ADODB.Recordset
        Dim rs4 As ADODB.Recordset
        Dim rs5 As ADODB.Recordset
        Dim dblIssPcs As Double
        Dim dblIssCts As Double
        Dim strFlow As String
        Dim intSec As Integer
        Dim intSecCount As Integer
        Dim dblRetPCsT As Double
        Dim dblRetPcsB As Double
        Dim dblRetCts As Double
        Dim intGroove As Integer

        intGroove = 0
        If txtSection.Text <> "" Then
            If txtParPkt.Text = "" Then MsgBox("Please enter the Parcel No/Packet No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If txtEmp.Text = "" Then MsgBox("Please enter the Employee No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

            Rs1 = New ADODB.Recordset
            Rs1.Open("SELECT * FROM tblMixPacket WHERE PktOrdNo = '" & ParcelNo & "' And PktNo = '" & PacketNo & "'", AdoCN, 1, 1)
            If Rs1.RecordCount Then
                dblIssPcs = Rs1.Fields("PktPcs").Value
                dblIssCts = Rs1.Fields("PktCts").Value
                strFlow = Rs1.Fields("PktFlow").Value
                dblIssCts = Math.Round(dblIssCts, 3)

                rs5 = New ADODB.Recordset
                rs5.Open("SELECT * FROM tblOrdersDtls WHERE OrderNo = '" & ParcelNo & "' AND RefNo = '" & Replace(Rs1.Fields("PktRefNo").Value, "'", "''") & "' AND Side = '" & Rs1.Fields("PktSide").Value & "' AND Groove = 1", AdoCN, 1, 1)
                If rs5.RecordCount Then
                    intGroove = 1
                End If
                rs5 = Nothing

                If intGroove = 1 Then
                    If CInt(txtSection.Text) > 14 Then
                        MsgBox("This is a Groove packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If

                If Rs1.Fields("RejectRep").Value = 1 Then
                    MsgBox("This is a Opening packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                rs2 = New ADODB.Recordset
                rs2.Open("SELECT * FROM tblMixIssues WHERE ParNo = '" & ParcelNo & "' And PktNo = '" & PacketNo & "' ORDER BY Sec DESC", AdoCN, 1, 1)
                If rs2.RecordCount = 0 Then
                    intSec = 1

                    For intSecCount = 1 To CInt(txtSection.Text)
                        rs3 = New ADODB.Recordset
                        rs3.Open("SELECT * FROM tblMixFlow WHERE Flow = '" & strFlow & "'", AdoCN, 1, 1)
                        If rs3.RecordCount Then
                            intSec = rs3.Fields("Flsec" & intSecCount).Value
                        Else
                            intSec = intSecCount
                        End If
                        rs3 = Nothing
                        If intSec > CInt(txtSection.Text) Then Exit For
                        If intSec = 0 Then Exit For
                        'Issues
                        AdoCN.Execute("INSERT INTO tblMixIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                      "VALUES('" & ParcelNo & "','" & PacketNo & "','" & intSec & "','" & strFlow & "','" & intSecCount & "','" & txtEmp.Text & "',0,'" & dblIssPcs & "','" & dblIssCts & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & PBUser_EmpNo & "')")
                        'Returns
                        AdoCN.Execute("INSERT INTO tblMixReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,RejStatus,RejReason,DoneBy) " & _
                                      "VALUES('" & ParcelNo & "','" & PacketNo & "','" & strFlow & "','" & intSecCount & "','" & intSec & "','" & txtEmp.Text & "',0,'" & dblIssPcs & "','" & dblIssCts & "',0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,'-','" & PBUser_EmpNo & "')")
                    Next
                    MsgBox("Successfully By Passed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Else
                    rs4 = New ADODB.Recordset
                    rs4.Open("SELECT * FROM VW_MixRealReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & rs2.Fields("Sec").Value & "", AdoCN, 1, 1)
                    If rs4.RecordCount Then
                        If rs2.Fields("IssPcsT").Value + rs2.Fields("IssPcsB").Value = rs4.Fields("SumOfRetPcsT").Value + rs4.Fields("SumOfRetPcsB").Value + rs4.Fields("SumOfRej").Value + rs4.Fields("SumOfLost").Value - rs4.Fields("SumOfExt").Value Then
                            dblRetPCsT = rs4.Fields("SumOfRetPcsT").Value
                            dblRetPcsB = rs4.Fields("SumOfRetPcsB").Value
                            dblRetCts = rs4.Fields("SumOfRetCts").Value
                            dblRetCts = Math.Round(dblRetCts, 3)

                            For intSecCount = rs2.Fields("Seccount").Value + 1 To CInt(txtSection.Text)
                                rs3 = New ADODB.Recordset
                                rs3.Open("SELECT * FROM tblMixFlow WHERE Flow = '" & strFlow & "'", AdoCN, 1, 1)
                                If rs3.RecordCount Then
                                    If rs3.Fields("FlowSections").Value >= intSecCount Then
                                        intSec = rs3.Fields("Flsec" & intSecCount).Value
                                    Else
                                        Exit For
                                    End If
                                Else
                                    intSec = intSecCount
                                End If
                                rs3 = Nothing
                                If intSec > CInt(txtSection.Text) Then Exit For
                                If intSec = 0 Then Exit For

                                'Issues
                                AdoCN.Execute("INSERT INTO tblMixIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                              "VALUES('" & ParcelNo & "','" & PacketNo & "','" & intSec & "','" & strFlow & "','" & intSecCount & "','" & txtEmp.Text & "','" & dblRetPCsT & "','" & dblRetPcsB & "','" & dblRetCts & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & PBUser_EmpNo & "')")
                                'Returns
                                AdoCN.Execute("INSERT INTO tblMixReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,RejStatus,RejReason,DoneBy) " & _
                                              "VALUES('" & ParcelNo & "','" & PacketNo & "','" & strFlow & "','" & intSecCount & "','" & intSec & "','" & txtEmp.Text & "','" & dblRetPCsT & "','" & dblRetPcsB & "','" & dblRetCts & "',0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,'-','" & PBUser_EmpNo & "')")
                            Next
                            MsgBox("Successfully By Passed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Else
                            MsgBox("Not Fully Returned", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        End If
                    Else
                        MsgBox("Already Issued", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    End If
                    rs4 = Nothing
                End If
                rs2 = Nothing
            End If
            Rs1 = Nothing
        End If
    End Sub

    Private Sub ByPassLemata()
        Dim Rs1 As ADODB.Recordset
        Dim rs2 As ADODB.Recordset
        Dim strFlow As String
        Dim dblRetPCsT As Double
        Dim dblRetPcsB As Double
        Dim dblRetCts As Double

        If txtParPkt.Text = "" Then MsgBox("Please enter the Parcel No/Packet No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtEmp.Text = "" Then MsgBox("Please enter the Employee No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        Rs1 = New ADODB.Recordset
        Rs1.Open("SELECT * FROM tblMixPacket WHERE PktOrdNo = '" & ParcelNo & "' And PktNo = '" & PacketNo & "'", AdoCN, 1, 1)
        If Rs1.RecordCount Then
            strFlow = Rs1.Fields("PktFlow").Value

            rs2 = New ADODB.Recordset
            rs2.Open("SELECT * FROM tblMixIssues WHERE ParNo = '" & ParcelNo & "' And PktNo = '" & PacketNo & "' AND Sec = 7", AdoCN, 1, 1)
            If rs2.RecordCount Then
                MsgBox("Already Issued to LEMATA", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rs2 = Nothing

            rs2 = New ADODB.Recordset
            rs2.Open("SELECT * FROM tblMixReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = 7", AdoCN, 1, 1)
            If rs2.RecordCount Then
                MsgBox("Already Returned from LEMATA", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rs2 = Nothing

            rs2 = New ADODB.Recordset
            rs2.Open("SELECT SUM(RetPcsT) AS RetPcsT,SUM(RetPcsB) AS RetPcsB,ROUND(SUM(RetCts), 3) AS RetCts FROM tblMixReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = 6", AdoCN, 1, 1)
            If rs2.RecordCount Then
                dblRetPCsT = rs2.Fields("RetPcsT").Value
                dblRetPcsB = rs2.Fields("RetPcsB").Value
                dblRetCts = rs2.Fields("RetCts").Value
                dblRetCts = Math.Round(dblRetCts, 3)
            End If
            rs2 = Nothing

            'Issues
            AdoCN.Execute("INSERT INTO tblMixIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                          "VALUES('" & ParcelNo & "','" & PacketNo & "',7,'" & strFlow & "',6,'D06975'," & dblRetPCsT & ",'" & dblRetPcsB & "','" & dblRetCts & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & PBUser_EmpNo & "')")

            'Returns
            AdoCN.Execute("INSERT INTO tblMixReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,RejStatus,RejReason,DoneBy) " & _
                          "VALUES('" & ParcelNo & "','" & PacketNo & "','" & strFlow & "',6,7,'D06975'," & dblRetPCsT & ",'" & dblRetPcsB & "','" & dblRetCts & "',0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,'-','" & PBUser_EmpNo & "')")

            MsgBox("Successfully By Passed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        End If
        Rs1 = Nothing
    End Sub

    Private Sub RecSave()
        Dim dataok As Boolean
        Dim stiss, stret As Integer
        Dim ciss, cret As Single
        Dim ChkString As String

        dataok = True
        ChkString = ""
        If issued Then
            If Len(txtRetDate.Text) < 2 Then Exit Sub 'check parcel/packet
            stiss = CInt(txtIssTap.Text) + CInt(txtIssBag.Text)   'issued pcs
            stret = CInt(txtTotBag.Text) + CInt(txtTotTap.Text) + CInt(txtRetTap.Text) + CInt(txtRetBag.Text) + CInt(txtRej.Text) - CInt(txtExt.Text) + CInt(txtLost.Text) + CInt(txtBro.Text) 'Ret pcs
            
            If stiss < stret Then
                dataok = False
                strmsg = "Stones issued " & stiss & "   Stones returned " & stret
                If MsgBox(strmsg, MsgBoxStyle.Question + MsgBoxStyle.OkCancel, Me.Text) = vbOK Then
                    ChkString = UCase(InputBox("Enter Access Code", "Authorized Password"))
                    If ChkString = "DIA08STKP" Then
                        dataok = True
                    Else
                        dataok = False
                    End If
                Else
                    dataok = False
                End If
                If dataok = False Then Exit Sub
            End If

            ciss = CSng(txtIssCts.Text)
            cret = CSng(txtTotCts.Text) + CSng(txtRetCts.Text) + CSng(txtRejCts.Text) + CSng(txtLostCts.Text)

            If ciss < cret Then
                dataok = False
                strmsg = "Carats issued " & Format(ciss, "##.###") & "   Carats returned " & Format(cret, "##.###")

                If MsgBox(strmsg, MsgBoxStyle.Question + MsgBoxStyle.OkCancel, Me.Text) = vbOK Then
                    ChkString = UCase(InputBox("Enter Access Code", "Authorized Password"))
                    If ChkString = "DIA08STKC" Then
                        dataok = True
                    Else
                        dataok = False
                    End If
                Else
                    dataok = False
                End If
                If dataok = False Then Exit Sub
            End If

            If stret <= 0 Then
                MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If cret <= 0 Then
                MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CInt(txtRetTap.Text) + CInt(txtRetBag.Text) > 0 Then
                If CSng(txtRetCts.Text) <= 0 Then
                    MsgBox("Invalid Return Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
            If txtNoPay.Text = "" Then
                MsgBox("Invalid No Pay Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If CInt(txtRep.Text) > CInt(txtRetTap.Text) + CInt(txtRetBag.Text) Then
                MsgBox("Invalid Repair Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Temp)
                Exit Sub
            End If

            If cmbSection.SelectedIndex + 1 = 16 Then
                If txtID100.Text = "" Then
                    MsgBox("Invalid ID100 Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If txtAms2.Text = "" Then
                    MsgBox("Invalid AMS2 Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If txtDfi.Text = "" Then
                    MsgBox("Invalid DFI Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If CInt(txtID100.Text) + CInt(txtAms2.Text) + CInt(txtDfi.Text) <> CInt(txtRetTap.Text) + CInt(txtRetBag.Text) Then
                    MsgBox("Invalid ID100/AMS2/DFI Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Else
                txtID100.Text = "0"
                txtAms2.Text = "0"
                txtDfi.Text = "0"
            End If

            'If CInt(txtNoPay.Text) >= CInt(txtIssTap.Text) + CInt(txtIssBag.Text) Then
            '    MsgBox("Invalid No Pay Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '    Exit Sub
            'End If
        Else
            rsComSql_2 = New ADODB.Recordset
            rsComSql_2.Open("SELECT EmpNo FROM tblMixReturns WHERE EmpNo = '" & txtEmp.Text & "' AND LostPcs > 0 AND Active = 1", AdoCN, 1, 1)
            If rsComSql_2.RecordCount Then
                MsgBox("Have a Lost to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql_2 = Nothing

            'rsComSql_2 = New ADODB.Recordset
            'rsComSql_2.Open("SELECT EmpNo FROM VW_ALLLostEmpNo WHERE EmpNo = '" & txtEmp.Text & "'", dbConn, 1, 1)
            'If rsComSql_2.RecordCount Then
            '    MsgBox("Have a Lost to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '    Exit Sub
            'End If
            'rsComSql_2 = Nothing

            If intCheckIssDate = 1 Then
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT FullEmpNo, CATEGORY, Pay FROM VW_EMP_MASTER_SMALL2 WHERE (Pay = 1) AND (FullEmpNo = '" & txtEmp.Text & "')", AdoCN, 1, 1)
                'rsComSql_1.Open("SELECT FullEmpNo, CATEGORY, Pay FROM VW_EMP_MASTER_SMALL2 WHERE (CATEGORY LIKE 'DIRECT%' OR CATEGORY LIKE 'TEMP%') AND (Pay = 1) AND (FullEmpNo = '" & txtEmp.Text & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    rsComSql_2 = New ADODB.Recordset
                    'rsComSql_2.Open("SELECT TOP (100) PERCENT DATEDIFF(d, dbo.tblMixIssues.IssDate, GETDATE()) AS Days " & _
                    '                "FROM dbo.tblMixIssues INNER JOIN dbo.tblOrders ON dbo.tblMixIssues.ParNo = dbo.tblOrders.OrderNo LEFT OUTER JOIN " & _
                    '                    "dbo.tblMixReturns ON dbo.tblMixIssues.ParNo = dbo.tblMixReturns.ParNo AND dbo.tblMixIssues.PktNo = dbo.tblMixReturns.PktNo AND dbo.tblMixIssues.Sec = dbo.tblMixReturns.Sec " & _
                    '                "WHERE (dbo.tblMixReturns.PktNo IS NULL) AND (DATEDIFF(d, dbo.tblMixIssues.IssDate, GETDATE()) > '" & intDelayDays & "') AND (dbo.tblOrders.Complete = N'N') AND (dbo.tblMixIssues.EmpNo = '" & txtEmp.Text & "')", AdoCN, 1, 1)

                    rsComSql_2.Open("SELECT dbo.tblMixIssues.EmpNo, dbo.tblMixIssues.IssDate, DATEDIFF(d, dbo.tblMixIssues.IssDate, GETDATE()) AS Days " & _
                                    "FROM dbo.tblMixIssues INNER JOIN dbo.tblOrders ON dbo.tblMixIssues.ParNo = dbo.tblOrders.OrderNo LEFT OUTER JOIN " & _
                                        "dbo.VW_MixReturns ON dbo.tblMixIssues.ParNo = dbo.VW_MixReturns.ParNo AND dbo.tblMixIssues.PktNo = dbo.VW_MixReturns.PktNo AND dbo.tblMixIssues.Sec = dbo.VW_MixReturns.Sec " & _
                                    "WHERE (dbo.tblMixIssues.EmpNo = '" & txtEmp.Text & "') AND (dbo.tblOrders.Complete = N'N') AND (dbo.tblMixIssues.IssPcsT + dbo.tblMixIssues.IssPcsB - ISNULL(dbo.VW_MixReturns.RetPcs, 0) > 0) AND (DATEDIFF(d, dbo.tblMixIssues.IssDate, GETDATE()) > '" & intDelayDays & "')", AdoCN, 1, 1)

                    If rsComSql_2.RecordCount Then
                        MsgBox("Have a Delayed Packet to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql_2 = Nothing
                End If
                rsComSql_1 = Nothing
            End If

            If Len(txtIssDate.Text) < 2 Then dataok = False
            '**********************
            If Section = 0 Then
                'if model and issued pcs/cts =0 then dont accept
                If Not CSng(txtIssCts.Text) > 0 Then dataok = False
                If Not (CInt(txtIssTap.Text) + CInt(txtIssBag.Text)) > 0 Then dataok = False
            End If
            '**********************
        End If
        If dataok = True Then
            DataSave()     'if data is ok, save the record
        End If
        cmdParPkt.Focus()

    End Sub

    Private Sub DataSave()
        Dim intRejStatus As Integer
        Dim strEmpNo As String
        Dim blnEmpCheck As Boolean
        Dim blnCheck As Boolean
        Dim dtpIssDate As Date
        Dim intHours As Integer
        Dim intDays As Integer
        Dim intIndex As Integer
        Dim dtpCheckDate As Date
        Dim dblMixReturnID As Double
        Dim dblMixRejectID As Double
        Dim intRow As Integer
        Dim dtpIssDate14 As Date
        Dim dblRepPcs As Double
        Dim dblRetPcs As Double
        Dim dblIssPcs As Double
        Dim dblOKPcs As Double
        Dim dblNoPayPcs As Double
        Dim dblDiffHours As Double
        Dim strEquipment As String
        Dim intDiffDays As Integer
        Dim intLaser As Integer
        Dim dblListPrice As Double
        Dim dblAvgPrice As Double
        Dim strNextPktNo As String
        Dim intSec As Integer
        Dim intSecCount As Integer
        Dim intActive As Integer

        intActive = 0
        dblListPrice = 0
        dblAvgPrice = 0

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT PktOrdNo,PktNo FROM tblMixPacket WHERE PktOrdNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Ok = 1", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Please check the packet details", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT dbo.tblOrdersDtls.IncenCat, dbo.tblUnits.Flow AS UnitFlow, dbo.tblOrdersDtls.Flow " & _
                      "FROM dbo.tblMixPacket INNER JOIN dbo.tblOrdersDtls ON dbo.tblMixPacket.PktOrdNo = dbo.tblOrdersDtls.OrderNo AND dbo.tblMixPacket.PktRefNo = dbo.tblOrdersDtls.RefNo AND  " & _
                        "dbo.tblMixPacket.Pktside = dbo.tblOrdersDtls.Side INNER JOIN dbo.tblUnits ON dbo.tblOrdersDtls.IncenCat = dbo.tblUnits.Unit " & _
                      "WHERE (dbo.tblMixPacket.PktOrdNo = '" & ParcelNo & "') AND (dbo.tblMixPacket.PktNo = '" & PacketNo & "')", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If rsComSql.Fields("UnitFlow").Value <> rsComSql.Fields("Flow").Value Then
                MsgBox("Invalid Incentive Unit " & rsComSql.Fields("IncenCat").Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        End If
        rsComSql = Nothing

        If issued = False Then
            'Issue
            strEmpNo = Mid(Trim(ICNo), 1, 6)
            blnEmpCheck = False
            blnCheck = False

            If cmbSection.SelectedIndex + 1 = 3 Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT PktOrdNo,PktNo FROM tblMixPacket WHERE PktOrdNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND RejectRep = 1", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    MsgBox("Packet is Blocked for Opening", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing
            End If

            If blnEmpCheck = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblMixIssues WHERE IssDate = '" & Format(Date.Now, "MM/dd/yyyy") & "' AND EmpNo = '" & strEmpNo & "' AND Sec < 14", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    blnCheck = False
                Else
                    blnCheck = True
                End If
                rsComSql = Nothing

                If blnCheck = True Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM VW_MixFinishIssuesPending WHERE RetEmpNo = '" & strEmpNo & "' ORDER BY IssDate, IssTime", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        rsComSql.MoveFirst()
                        dtpIssDate = Format(rsComSql.Fields("IssDate").Value, "MM/dd/yyyy") & " " & Format(rsComSql.Fields("IssTime").Value, "HH:MM:SS")

                        intHours = DateDiff("H", dtpIssDate, Now)

                        If intHours > 48 Then
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

                            If intHours > 48 Then
                                MsgBox("Order No. : " & rsComSql.Fields("ParNo").Value & " Pkt No. :" & rsComSql.Fields("PktNo").Value & " is Final Repair pending", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                Exit Sub
                            End If
                        End If
                    End If
                    rsComSql = Nothing
                End If
            End If

            If Len(Trim(ICNo)) <> 6 Then
                MsgBox("Invalid Emp No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            rsComSql = New ADODB.Recordset
            If cmbSection.SelectedIndex + 1 > 14 Then
                rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & Trim(ICNo) & "' AND Pay = 1", AdoCN, 1, 1)
            Else
                rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & Trim(ICNo) & "' AND Pay = 1", AdoCN, 1, 1)
                'rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & Trim(ICNo) & "' AND CATEGORY <> 'LEADERS' AND CATEGORY <> 'SUPERVISORY' AND Pay = 1", AdoCN, 1, 1)
            End If
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Emp No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            If txtIssCts.Text = "" Then
                MsgBox("Invalid Issue Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CSng(txtIssCts.Text) <= 0 Then
                MsgBox("Invalid Issue Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If cmbSection.Text = "Groove" Then
                If chkLaser.Checked = True Then
                    intLaser = 1
                Else
                    intLaser = 0
                End If
            Else
                intLaser = 0
            End If

            mStrSQL = "INSERT INTO tblMixIssues(ParNo,PktNo,Flow,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,Sec,SecCount,Groove,DoneBy,Laser) " & _
                      "VALUES('" & ParcelNo & "','" & PacketNo & "','" & mFlow & "','" & Mid(Trim(ICNo), 1, 6) & "'," & CInt(txtIssTap.Text) & "," & CInt(txtIssBag.Text) & "," & CSng(txtIssCts.Text) & "," & _
                        "'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "'," & cmbSection.SelectedIndex + 1 & "," & Section + 1 & "," & IIf(chkGroove.Checked = True, 1, 0) & ",'" & PBUser_EmpNo & "'," & intLaser & ")"

            AdoCN.Execute(mStrSQL)
        Else
            'Return
            If CInt(txtRej.Text) > 0 Then
                intRejStatus = 1
                If cmbRejReason.Text = "" Then
                    MsgBox("Please enter the Reject Reason", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblMixRejReasons WHERE RejReason = '" & Trim(cmbRejReason.Text) & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    MsgBox("Invalid Reject Reason", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing

                If txtAssortment.Text = "" Then
                    MsgBox("Please select the Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Else
                intRejStatus = 0
            End If

            If CInt(txtLost.Text) > 0 Then
                If txtAssortment.Text = "" Then
                    MsgBox("Please select the Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            If CInt(txtBro.Text) > 0 Then
                If txtAssortment.Text = "" Then
                    MsgBox("Please select the Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            If Len(Trim(ICNo)) <> 6 Then
                MsgBox("Invalid Emp No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            rsComSql = New ADODB.Recordset
            If cmbSection.SelectedIndex + 1 > 14 Then
                rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & Trim(ICNo) & "' AND Pay = 1", AdoCN, 1, 1)
            Else
                rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & Trim(ICNo) & "' AND Pay = 1", AdoCN, 1, 1)
                'rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & Trim(ICNo) & "' AND CATEGORY <> 'LEADERS' AND CATEGORY <> 'SUPERVISORY' AND Pay = 1", AdoCN, 1, 1)
            End If
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Emp No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            If txtRetCts.Text = "" Then
                MsgBox("Invalid Return Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CInt(txtRetBag.Text) + CInt(txtRetTap.Text) > 0 Then
                If CSng(txtRetCts.Text) <= 0 Then
                    MsgBox("Invalid Return Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If Len(Trim(cmbEmpNo.Text)) <> 6 Then
                    MsgBox("Invalid Checking Emp No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & Trim(cmbEmpNo.Text) & "' AND Pay = 1", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    MsgBox("Invalid Checking Emp No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing
            End If

            If CInt(txtRej.Text) > 0 Then
                If CSng(txtRejCts.Text) <= 0 Then
                    MsgBox("Invalid Reject Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            If cmbSection.Text = "ProdFinish" Or cmbSection.Text = "Finished" Then
                If Len(cmbEmpNo.Text) = 0 Then
                    MsgBox("Invalid Checking Emp No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            If cmbSection.Text = "ProdFinish" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT dbo.tblMixIssuesRep.ParNo, dbo.tblMixIssuesRep.PktNo, dbo.tblMixIssuesRep.Sec, dbo.tblMixIssuesRep.IssPcs - ISNULL(dbo.tblMixReturnsRep.RetPcs, 0) AS BalPcs, " & _
                                "dbo.tblMixIssuesRep.EmpNo, dbo.tblMixIssuesRep.IssDate, dbo.tblMixIssuesRep.IssTime " & _
                              "FROM dbo.tblMixIssuesRep LEFT OUTER JOIN dbo.tblMixReturnsRep ON dbo.tblMixIssuesRep.ID = dbo.tblMixReturnsRep.IssueID AND dbo.tblMixIssuesRep.ParNo = dbo.tblMixReturnsRep.ParNo AND " & _
                                "dbo.tblMixIssuesRep.PktNo = dbo.tblMixReturnsRep.PktNo AND dbo.tblMixIssuesRep.Sec = dbo.tblMixReturnsRep.Sec " & _
                              "WHERE (dbo.tblMixIssuesRep.ParNo = '" & ParcelNo & "') AND (dbo.tblMixIssuesRep.PktNo = '" & PacketNo & "') AND (dbo.tblMixIssuesRep.IssPcs - ISNULL(dbo.tblMixReturnsRep.RetPcs, 0) > 0)", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("BalPcs").Value) Then
                        If rsComSql.Fields("BalPcs").Value > 0 Then
                            MsgBox("Please Complete the Repair Process", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                    End If
                End If
                rsComSql = Nothing
            End If

            If cmbSection.Text = "Groove" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT dbo.tblMixIssuesBoil.ParNo, dbo.tblMixIssuesBoil.PktNo, dbo.tblMixIssuesBoil.Sec, dbo.tblMixIssuesBoil.IssPcs - ISNULL(dbo.tblMixReturnsBoil.RetPcs, 0) AS BalPcs, " & _
                                "dbo.tblMixIssuesBoil.EmpNo, dbo.tblMixIssuesBoil.IssDate, dbo.tblMixIssuesBoil.IssTime " & _
                              "FROM dbo.tblMixIssuesBoil LEFT OUTER JOIN dbo.tblMixReturnsBoil ON dbo.tblMixIssuesBoil.ID = dbo.tblMixReturnsBoil.IssueID AND dbo.tblMixIssuesBoil.ParNo = dbo.tblMixReturnsBoil.ParNo AND " & _
                                "dbo.tblMixIssuesBoil.PktNo = dbo.tblMixReturnsBoil.PktNo AND dbo.tblMixIssuesBoil.Sec = dbo.tblMixReturnsBoil.Sec " & _
                              "WHERE (dbo.tblMixIssuesBoil.ParNo = '" & ParcelNo & "') AND (dbo.tblMixIssuesBoil.PktNo = '" & PacketNo & "') AND (dbo.tblMixIssuesBoil.IssPcs - ISNULL(dbo.tblMixReturnsBoil.RetPcs, 0) > 0)", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("BalPcs").Value) Then
                        If rsComSql.Fields("BalPcs").Value > 0 Then
                            MsgBox("Please Complete the Boiling Process", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                    End If
                End If
                rsComSql = Nothing
            End If

            If cmbSection.Text = "Groove" Then
                If Len(txtGrPcs.Text) = 0 Then
                    MsgBox("Invalid Groove Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If CInt(txtGrPcs.Text) > CInt(txtRetBag.Text) + CInt(txtRetTap.Text) Then
                    MsgBox("Invalid Groove Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If Len(cmbEmpNo.Text) = 0 Then
                    MsgBox("Invalid Checking Emp No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If Len(txtGrHeight.Text) = 0 Then
                    MsgBox("Invalid Groove Height", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Else
                txtGrPcs.Text = "0"
            End If

            'If cmbSection.Text = "Finished" Then
            '    If txtClient.Text = "CLIENT NO 111" Then
            '        If CDbl(txtIssTap.Text) + CDbl(txtIssBag.Text) <> CDbl(txtRetTap.Text) + CDbl(txtRetBag.Text) + CDbl(txtRej.Text) + CDbl(txtLost.Text) + CDbl(txtBro.Text) - CDbl(txtExt.Text) Then
            '            MsgBox("Invalid Return Pcs for Client 111", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '            Exit Sub
            '        End If
            '    End If
            'End If

            If Len(txtIncPcs.Text) = 0 Then
                MsgBox("Invalid Incentive Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CInt(txtIncPcs.Text) > CInt(txtRej.Text) Then
                MsgBox("Invalid Incentive Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Len(txtLabID.Text) = 0 Then
                txtLabID.Text = "0"
            End If

            'Prod Finish - Repair
            If (cmbSection.SelectedIndex) + 1 = 14 Then
                dblRepPcs = 0
                dblRetPcs = 0
                dblDiffHours = 0
                dblOKPcs = 0
                dblNoPayPcs = 0
                dblIssPcs = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(RepPcs) AS RepPcs, SUM(RetPcsT + RetPcsB) AS RetPcs FROM tblMixReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = 14", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("RepPcs").Value) Then
                        dblRepPcs = rsComSql.Fields("RepPcs").Value
                        dblRetPcs = rsComSql.Fields("RetPcs").Value
                    End If
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                'rsComSql.Open("SELECT CAST(IssDate as DATETIME) + CAST(IssTime AS TIME) AS DateTime1 FROM tblMixIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = 14", AdoCN, 1, 1)
                rsComSql.Open("SELECT IssDate + IssTime AS DateTime1 FROM tblMixIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = 14", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    dtpIssDate14 = rsComSql.Fields("DateTime1").Value
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                'rsComSql.Open("SELECT CAST(SendDate as DATETIME) + CAST(SendTime AS TIME) AS DateTime1 FROM tblMixIssuesRep WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = 1 AND OK = 1 ORDER BY ID", AdoCN, 1, 1)
                rsComSql.Open("SELECT SendDate + SendTime AS DateTime1 FROM tblMixIssuesRep WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = 1 AND OK = 1 ORDER BY ID", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    dtpIssDate14 = rsComSql.Fields("DateTime1").Value
                End If
                rsComSql = Nothing

                dblDiffHours = DateDiff(DateInterval.Hour, dtpIssDate14, Date.Now)
                If dblDiffHours > 54 Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM VW_EMP_HOLIDAYS WHERE HDate >= '" & Format(dtpIssDate14, "MM/dd/yyyy") & "' AND HDate <= '" & Format(Date.Now, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
                    dblDiffHours = dblDiffHours - (24 * rsComSql_1.RecordCount)
                    rsComSql_1 = Nothing

                    If dblDiffHours > 54 Then
                        dblOKPcs = CDbl(txtRetBag.Text) + CDbl(txtRetTap.Text)
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM tblMixReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec < 14 AND RetPcsT + RetPcsB > 0 ORDER BY ID", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            rsComSql_1.MoveFirst()
                            While Not rsComSql_1.EOF
                                rsComSql_2 = New ADODB.Recordset
                                rsComSql_2.Open("SELECT IssPcsT + IssPcsB AS IssPcs FROM tblMixIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & rsComSql_1.Fields("Sec").Value & "", AdoCN, 1, 1)
                                If rsComSql_2.RecordCount Then
                                    dblIssPcs = rsComSql_2.Fields("IssPcs").Value
                                End If
                                rsComSql_2 = Nothing

                                dblNoPayPcs = Math.Round(((rsComSql_1.Fields("RetPcsT").Value + rsComSql_1.Fields("RetPcsB").Value) / dblIssPcs) * dblOKPcs, 1)

                                AdoCN.Execute("UPDATE tblMixReturns SET NopayPcs1 = NopayPcs1 + " & dblNoPayPcs & " WHERE ID = " & rsComSql_1.Fields("ID").Value & "")

                                rsComSql_1.MoveNext()
                            End While
                        End If
                        rsComSql_1 = Nothing
                    End If
                End If

                intDiffDays = DateDiff(DateInterval.Day, CDate(Format(dtpIssDate14, "yyyy/MM/dd")), CDate(Format(Date.Now, "yyyy/MM/dd")))
                dblDiffHours = DateDiff(DateInterval.Hour, dtpIssDate14, Date.Now)
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM VW_EMP_HOLIDAYS WHERE HDate >= '" & Format(dtpIssDate14, "MM/dd/yyyy") & "' AND HDate <= '" & Format(Date.Now, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
                intDiffDays = intDiffDays - rsComSql_1.RecordCount
                dblDiffHours = dblDiffHours - (24 * rsComSql_1.RecordCount)
                rsComSql_1 = Nothing

                If dblDiffHours >= 48 Then
                    AdoCN.Execute("UPDATE dbo.tblMixPacket SET NewGrp = 'D' WHERE (PktOrdNo = '" & ParcelNo & "') AND (PktNo = '" & PacketNo & "')")
                End If
            End If

            strEquipment = ""
            If (cmbSection.SelectedIndex) + 1 = 16 Then
                strEquipment = "ID100"
            End If

            If CDbl(txtLost.Text) > 0 Then
                intActive = 1
            End If

            mStrSQL = "INSERT INTO tblMixReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,RejStatus,RejReason,Groove,GrPcs,IncPcs,ChkEmpNo,DoneBy,GiaPcs,AmsPcs,LabPcs,Equipment,LabID,Active) " & _
                      "VALUES ('" & ParcelNo & "','" & PacketNo & "','" & mFlow & "'," & Section & "," & cmbSection.SelectedIndex + 1 & ",'" & Mid(Trim(ICNo), 1, 6) & "'," & CInt(txtRetTap.Text) & "," & CInt(txtRetBag.Text) & "," & CSng(txtRetCts.Text) & "," & CInt(txtRej.Text) & "," & _
                        "" & CSng(txtRejCts.Text) & "," & CInt(txtLost.Text) & "," & CSng(txtLostCts.Text) & "," & CInt(txtBro.Text) & "," & CInt(txtRep.Text) & "," & CInt(txtNoPay.Text) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "'," & CInt(txtExt.Text) & ",0," & _
                        "" & CSng(txtRejRgh.Text) & "," & CSng(txtLstRgh.Text) & "," & intRejStatus & ",'" & cmbRejReason.Text & "'," & IIf(chkGroove.Checked = True, 1, 0) & "," & CInt(txtGrPcs.Text) & "," & CInt(txtIncPcs.Text) & ",'" & UCase(cmbEmpNo.Text) & "','" & PBUser_EmpNo & "'," & CInt(txtID100.Text) & "," & _
                        "" & CInt(txtAms2.Text) & "," & CInt(txtDfi.Text) & ",'" & strEquipment & "'," & CDbl(txtLabID.Text) & ",'" & intActive & "')"

            AdoCN.Execute(mStrSQL)

            mStrSQL = "UPDATE tblMixPacket SET GrHeight = " & CDbl(txtGrHeight.Text) & " WHERE (PktOrdNo = '" & ParcelNo & "') AND (PktNo = '" & PacketNo & "')"
            AdoCN.Execute(mStrSQL)

            dblMixReturnID = 0
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT MAX(ID) AS MaxID FROM tblMixReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("MaxID").Value) Then
                    dblMixReturnID = rsComSql.Fields("MaxID").Value
                End If
            End If
            rsComSql = Nothing

            'Angle Lemata If By Pass Lemata
            If (cmbSection.SelectedIndex) + 1 = 13 Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblMixReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = 7", AdoCN, 1, 1)
                If rsComSql.RecordCount = 1 Then
                    If rsComSql.Fields("EmpNo").Value = "D06975" Then
                        AdoCN.Execute("UPDATE tblMixReturns SET EmpNo = '" & Mid(Trim(ICNo), 1, 6) & "', RetDate = '" & Format(Date.Now, "MM/dd/yyyy") & "',RetTime = '" & Format(Date.Now, "HH:mm") & "' " & _
                                      "WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = 7")
                    End If
                End If
                rsComSql = Nothing
            End If

            If CInt(txtRej.Text) > 0 Then
                AdoCN.Execute("INSERT INTO tblMixRejectDetails(ParNo,PktNo,Sec,Pcs,Cts,Assortment,RejDate,Type,ReturnID) " & _
                              "VALUES('" & ParcelNo & "','" & PacketNo & "'," & (cmbSection.SelectedIndex) + 1 & "," & CInt(txtRej.Text) & "," & CSng(txtRejCts.Text) & ",'" & txtAssortment.Text & "','" & Format(Date.Now, "MM/dd/yyyy") & "','R','" & dblMixReturnID & "')")
                Insert_Effect("R", dblMixReturnID, CDbl(txtRej.Text))
                If CInt(txtExt.Text) > 0 Then
                    AdoCN.Execute("INSERT INTO tblMixRejectDetails(ParNo,PktNo,Sec,Pcs,Cts,Assortment,RejDate,Type,ReturnID) " & _
                                  "VALUES('" & ParcelNo & "','" & PacketNo & "'," & (cmbSection.SelectedIndex) + 1 & "," & CInt(txtRej.Text) * -1 & "," & CSng(txtRejCts.Text) * -1 & ",'" & txtAssortment.Text & "','" & Format(Date.Now, "MM/dd/yyyy") & "','E','" & dblMixReturnID & "')")
                End If

                'Auto Reject and Packet Entry
                If cmbRejReason.Text = "Issue Again" And CInt((cmbSection.SelectedIndex) + 1) = 18 Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT MarketPrice, AvgCost FROM tblAssortList WHERE Assortment = '" & txtAssortment.Text & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        dblListPrice = rsComSql.Fields("MarketPrice").Value
                        dblAvgPrice = rsComSql.Fields("AvgCost").Value
                    End If
                    rsComSql = Nothing

                    AdoCN.Execute("UPDATE tblMixReturns SET RejStatus = 3 WHERE ID = '" & dblMixReturnID & "'")

                    AdoCN.Execute("INSERT INTO tblMixRejects(ParNo,PktNo,Pcs,Cts,Assortment,OrgAssort,Price,InID,ImportNo,RejDate,OldAssort,Stock,Export,OK,SupParNo,Origin,DoneBy,ProdRejDate,Sec,Reason) " & _
                                  "VALUES('" & ParcelNo & "','" & PacketNo & "'," & CInt(txtRej.Text) & "," & CDbl(txtRejCts.Text) & "," & _
                                    "'" & txtAssortment.Text & "','APCU'," & dblAvgPrice & ",0,1,'" & Format(Date.Now, "MM/dd/yyyy") & "'," & _
                                    "'" & txtAssortment.Text & "',1,0,1,'" & strSupParcelNo & "','" & strOrigin & "','" & PBUser_EmpNo & "'," & _
                                    "'" & Format(Date.Now, "MM/dd/yyyy") & "'," & CInt((cmbSection.SelectedIndex) + 1) & ",'" & cmbRejReason.Text & "')")

                    dblMixRejectID = 0
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT MAX(ID) AS MaxID FROM tblMixRejects WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If Not IsDBNull(rsComSql.Fields("MaxID").Value) Then
                            dblMixRejectID = rsComSql.Fields("MaxID").Value
                        End If
                    End If
                    rsComSql = Nothing

                    'Insert Assortment In
                    AdoCN.Execute("INSERT INTO tblAssortDetails(ImportNo,OrgAssort,Assortment,AssortBox,DDate,InPcs,InCts,AvgCost,BaseCost,CurCost,RejInPcs,RejInCts,RejAvgCost,RejBaseCost,RejCurCost,Type) " & _
                                  "VALUES(1,'APCU','" & txtAssortment.Text & "','" & dblMixRejectID & "','" & Format(Date.Now, "MM/dd/yyyy") & "'," & _
                                    "" & CInt(txtRej.Text) & "," & CDbl(txtRejCts.Text) & "," & dblAvgPrice & "," & dblListPrice & "," & dblListPrice & ",0,0,0,0,0,'K')")

                    'Insert Assort Origin
                    AdoCN.Execute("INSERT INTO tblAssortOrigin(Assortment,Origin,SupParNo,Pcs,EntDate) VALUES('" & txtAssortment.Text & "','" & strOrigin & "','" & strSupParcelNo & "'," & CInt(txtRej.Text) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "')")

                    'Packet Entry
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblMixPacket WHERE PktOrdNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        'Internal Issue
                        AdoCN.Execute("INSERT INTO tblMixIntIssues(Assortment,Pcs,Cts,EmpNo,IssDate,IssTime,EmpNo2,OK) " & _
                                      "VALUES('" & txtAssortment.Text & "'," & CInt(txtRej.Text) & "," & CDbl(txtRejCts.Text) & ",'" & PBUser_EmpNo & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & PBUser_EmpNo & "',1) ")

                        'New Packet No.
                        strNextPktNo = "0001"
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT MAX(CONVERT(NUMERIC, PktNo)) AS MaxPktNo FROM tblMixPacket WHERE PktOrdNo = '" & ParcelNo & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            If Not IsDBNull(rsComSql_1.Fields("MaxPktNo").Value) Then
                                strNextPktNo = Format(CDbl(rsComSql_1.Fields("MaxPktNo").Value) + 1, "0000")
                            End If
                        End If
                        rsComSql_1 = Nothing

                        'New Packet Entry
                        AdoCN.Execute("INSERT INTO tblMixPacket(ParNo,PktNo,PktPcs,PktCts,PktOrdNo,PktRefNo,Pktside,AssortNo,PktFlow,Grp,AParNo,PktIss,Remarks,OK,RejectRep,Special,IssEmpNo,Sample,DoneBy,Ams,Export,NewGrp,Accept,AcceptDate,AcceptTime,AcceptBy,DelDate,DelEmp,DelBy,PktPrint,ReIssue) " & _
                                  "VALUES('','" & strNextPktNo & "','" & CDbl(txtRej.Text) & "','" & CDbl(txtRejCts.Text) & "','" & ParcelNo & "'," & _
                                    "'" & Replace(rsComSql.Fields("PktRefNo").Value, "'", "''") & "','" & rsComSql.Fields("Pktside").Value & "','" & txtAssortment.Text & "','" & rsComSql.Fields("PktFlow").Value & "','" & UCase(rsComSql.Fields("Grp").Value) & "'," & _
                                    "'00','" & Format(Date.Now, "MM/dd/yyyy") & "','" & rsComSql.Fields("Remarks").Value & "',1,0,'" & rsComSql.Fields("Special").Value & "','" & rsComSql.Fields("IssEmpNo").Value & "'," & _
                                    "0,'" & PBUser_EmpNo & "',1,0,'" & UCase(rsComSql.Fields("NewGrp").Value) & "',1,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & PBUser_EmpNo & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & PBUser_EmpNo & "','" & PBUser_ID & "',1,1)")

                        'New Packet Detail
                        AdoCN.Execute("INSERT INTO tblMixPacketDetails(ParNo,PktNo,Pcs,Cts,Assortment,OrgAssort,EntDate,Type) " & _
                                      "VALUES('" & ParcelNo & "','" & strNextPktNo & "'," & CDbl(txtRej.Text) & "," & CDbl(txtRejCts.Text) & ",'" & txtAssortment.Text & "','APCU','" & Format(Date.Now, "MM/dd/yyyy") & "','P')")

                        'New Packet Origin
                        AdoCN.Execute("INSERT INTO tblMixPacketOrigin(ParNo,PktNo,Assortment,SupParNo,Origin,Pcs,EntDate,BoxInDate) " & _
                                      "VALUES('" & ParcelNo & "','" & strNextPktNo & "','" & txtAssortment.Text & "','" & strSupParcelNo & "','" & strOrigin & "'," & CDbl(txtRej.Text) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "MM/dd/yyyy") & "')")

                        'New Packet Issues and Returns By Pass
                        For intSecCount = 1 To 18
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT * FROM tblMixFlow WHERE Flow = '" & rsComSql.Fields("PktFlow").Value & "'", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount Then
                                intSec = rsComSql_1.Fields("Flsec" & intSecCount).Value
                            Else
                                intSec = intSecCount
                            End If
                            rsComSql_1 = Nothing
                            If intSec > 18 Then Exit For
                            If intSec = 0 Then Exit For

                            'Issues
                            AdoCN.Execute("INSERT INTO tblMixIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                          "VALUES('" & ParcelNo & "','" & strNextPktNo & "','" & intSec & "','" & rsComSql.Fields("PktFlow").Value & "','" & intSecCount & "','" & PBUser_EmpNo & "',0,'" & CDbl(txtRej.Text) & "','" & CDbl(txtRejCts.Text) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & PBUser_EmpNo & "')")
                            'Returns
                            If intSec = 16 Then
                                AdoCN.Execute("INSERT INTO tblMixReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,RejStatus,RejReason,DoneBy,GiaPcs,Equipment) " & _
                                              "VALUES('" & ParcelNo & "','" & strNextPktNo & "','" & rsComSql.Fields("PktFlow").Value & "','" & intSecCount & "','" & intSec & "','" & txtEmp.Text & "',0,'" & CDbl(txtRej.Text) & "','" & CDbl(txtRejCts.Text) & "',0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,'-','" & PBUser_EmpNo & "','" & CDbl(txtRej.Text) & "','ID100')")
                            Else
                                AdoCN.Execute("INSERT INTO tblMixReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,RejStatus,RejReason,DoneBy) " & _
                                              "VALUES('" & ParcelNo & "','" & strNextPktNo & "','" & rsComSql.Fields("PktFlow").Value & "','" & intSecCount & "','" & intSec & "','" & txtEmp.Text & "',0,'" & CDbl(txtRej.Text) & "','" & CDbl(txtRejCts.Text) & "',0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,'-','" & PBUser_EmpNo & "')")
                            End If
                            
                        Next

                        MsgBox("New Packet - " & strNextPktNo, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    End If
                    rsComSql = Nothing
                End If
            End If

            If CInt(txtLost.Text) > 0 Then
                Insert_Effect("L", dblMixReturnID, CDbl(txtLost.Text))
                AdoCN.Execute("INSERT INTO tblMixRejectDetails(ParNo,PktNo,Sec,Pcs,Cts,Assortment,RejDate,Type,ReturnID) " & _
                              "VALUES('" & ParcelNo & "','" & PacketNo & "'," & (cmbSection.SelectedIndex) + 1 & "," & CInt(txtLost.Text) & "," & CSng(txtLostCts.Text) & ",'" & txtAssortment.Text & "','" & Format(Date.Now, "MM/dd/yyyy") & "','L','" & dblMixReturnID & "')")
            End If

            If CInt(txtBro.Text) > 0 Then
                Insert_Effect("B", dblMixReturnID, CDbl(txtBro.Text))
                AdoCN.Execute("INSERT INTO tblMixRejectDetails(ParNo,PktNo,Sec,Pcs,Cts,Assortment,RejDate,Type,ReturnID) " & _
                              "VALUES('" & ParcelNo & "','" & PacketNo & "'," & (cmbSection.SelectedIndex) + 1 & "," & CInt(txtBro.Text) & "," & CSng(txtLostCts.Text) & ",'" & txtAssortment.Text & "','" & Format(Date.Now, "MM/dd/yyyy") & "','B','" & dblMixReturnID & "')")
            End If

            If CInt(txtRep.Text) > 0 Then
                For intRow = 0 To flxRepair.Rows.Count - 1
                    AdoCN.Execute("INSERT INTO tblMixRepairDetails(ParNo,PktNo,Sec,RepPcs,RepReason,RepDate) " & _
                                  "VALUES('" & ParcelNo & "','" & PacketNo & "'," & (cmbSection.SelectedIndex) + 1 & "," & CInt(flxRepair.Item(1, intRow).Value) & ",'" & flxRepair.Item(0, intRow).Value & "','" & Format(Date.Now, "MM/dd/yyyy") & "')")
                Next
            End If
        End If
        ClearFields()
        Exit Sub
RETRY:
        MsgBox("Please Re-Enter the Packet Details", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        ClearFields()
        Exit Sub
    End Sub

    Private Sub Insert_Effect(ByVal strType As String, ByVal dblRetID As Double, ByVal dblEffPcs As Double)
        Dim dblRejPcs As Double
        Dim dblBalPcs As Double

        dblRejPcs = dblEffPcs
        dblBalPcs = 0
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT ParNo, PktNo, SupParNo, Origin, Pcs, BoxInDate, Assortment " & _
                      "FROM dbo.tblMixPacketOrigin " & _
                      "WHERE (ParNo = '" & ParcelNo & "') AND (PktNo = '" & PacketNo & "') " & _
                      "ORDER BY BoxInDate DESC", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF And dblRejPcs > 0
                If dblRejPcs > 0 Then
                    dblBalPcs = rsComSql.Fields("Pcs").Value
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT SUM(Pcs) AS Pcs " & _
                                    "FROM dbo.tblMixRejectOrigin " & _
                                    "WHERE (ParNo = '" & ParcelNo & "') AND (PktNo = '" & PacketNo & "') AND (Origin = '" & rsComSql.Fields("Origin").Value & "') AND (SupParNo = '" & rsComSql.Fields("SupParNo").Value & "')", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                            dblBalPcs = dblBalPcs - rsComSql_1.Fields("Pcs").Value
                        End If
                    End If
                    rsComSql_1 = Nothing

                    If dblBalPcs > 0 Then
                        If dblRejPcs <= dblBalPcs Then
                            dblBalPcs = dblRejPcs
                            dblRejPcs = 0
                        Else
                            dblRejPcs = dblRejPcs - dblBalPcs
                        End If
                        If strType = "E" Then
                            dblBalPcs = dblBalPcs * (-1)
                        End If

                        strSupParcelNo = rsComSql.Fields("SupParNo").Value
                        strOrigin = rsComSql.Fields("Origin").Value
                        AdoCN.Execute("INSERT INTO tblMixRejectOrigin(ParNo,PktNo,Origin,SupParNo,Sec,Pcs,Assortment,RejDate,Type,RetID) " & _
                                      "VALUES('" & ParcelNo & "','" & PacketNo & "','" & rsComSql.Fields("Origin").Value & "','" & rsComSql.Fields("SupParNo").Value & "'," & _
                                        "" & (cmbSection.SelectedIndex) + 1 & "," & dblBalPcs & ",'" & rsComSql.Fields("Assortment").Value & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & strType & "'," & dblRetID & ")")
                    End If
                End If
                rsComSql.MoveNext()
            End While
        Else
            AdoCN.Execute("INSERT INTO tblMixRejectOrigin(ParNo,PktNo,Origin,SupParNo,Sec,Pcs,Assortment,RejDate,Type,RetID) " & _
                          "VALUES('" & ParcelNo & "','" & PacketNo & "','De Beers','X900003'," & (cmbSection.SelectedIndex) + 1 & "," & _
                          "" & dblEffPcs & ",'" & txtAssortment.Text & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & strType & "'," & dblRetID & ")")
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        RecSave()
    End Sub

    Private Sub txtRetTap_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRetTap.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtRetTap.Text = "" Then
                txtRetTap.Text = "0"
            End If
            txtRetBag.Focus()
        End If
    End Sub

    Private Sub txtRetBag_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRetBag.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If Len(txtRetBag.Text) = 0 Then
                txtRetBag.Text = "0"
            End If
            If CDbl(txtRetBag.Text) > 0 Then
                txtFinCts.Text = Math.Round((CDbl(txtIssCts.Text) / (CDbl(txtIssTap.Text) + CDbl(txtIssBag.Text))) * CDbl(txtRetBag.Text), 3)
            End If
            txtRetCts.Focus()
        End If
    End Sub

    Private Sub txtRetCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRetCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRetCts.Text)
        If Asc(e.KeyChar) = 13 Then
            Fill_Data()
        End If
    End Sub

    Private Sub Fill_Data()
        Dim Yld As Single
        Dim wst As Single

        txtRej.Text = "0"
        txtRejCts.Text = "0"
        txtLost.Text = "0"
        txtLostCts.Text = "0"
        txtRep.Text = "0"
        txtBro.Text = "0"
        txtExt.Text = "0"
        txtNoPay.Text = "0"
        txtRejRgh.Text = "0"
        txtLstRgh.Text = "0"
        txtGrPcs.Text = "0"
        txtIncPcs.Text = "0"
        txtID100.Text = "0"
        txtAms2.Text = "0"
        txtDfi.Text = "0"

        If Not IsNumeric(txtRetCts.Text) Then
            txtRetCts.Text = "0"
            txtRej.Focus()
        Else
            txtYield1.Text = Format(((CDbl(txtRetCts.Text) + CDbl(txtTotCts.Text)) / Caretspkt) * 100, "#0.00")
            Yld = txtYield1.Text
            wst = CSng(txtIssCts.Text) - (CSng(txtRetCts.Text) + CDbl(txtTotCts.Text))
            txtYield2.Text = Format((wst / Caretspkt) * 100, "#0.00")
            txtRej.Focus()
        End If
    End Sub

    Private Sub txtRej_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRej.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If CInt(txtRej.Text) > 0 Then
                txtRejRgh.Text = Math.Round((AvgCtspkt * CInt(txtRej.Text)), 3)
                cmbRejReason.Visible = True
                txtRejCts.Focus()
            Else
                txtRejRgh.Text = "0"
            End If
            txtRejCts.Focus()
        End If
    End Sub

    Private Sub Load_EmpNo()
        cmbEmpNo.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblMixEmpNo ORDER BY EmpNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbEmpNo.Items.Add(rsComSql.Fields("EmpNo").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub txtRejCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRejCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRejCts.Text)
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
            txtBro.Focus()
        End If
    End Sub

    Private Sub txtBro_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBro.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtExt.Focus()
        End If
    End Sub

    Private Sub txtExt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtExt.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtNoPay.Focus()
        End If
    End Sub

    Private Sub txtNoPay_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNoPay.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtGrPcs.Focus()
        End If
    End Sub

    Private Sub txtIncPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIncPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub txtGrPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGrPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtIncPcs.Focus()
        End If
    End Sub

    Private Sub cmdGetDetails_Click(sender As Object, e As EventArgs) Handles cmdGetDetails.Click
        txtRetTap.Text = txtIssTap.Text
        txtRetBag.Text = txtIssBag.Text
        txtRetCts.Text = txtIssCts.Text

        ICNo = Trim(txtEmp.Text)
        txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
        txtRetTime.Text = Format(Date.Now, "HH:mm")

        If cmbSection.Text = "ProdFinish" Or cmbSection.Text = "Finished" Then
            cmbEmpNo.Text = "D05502"
        End If

        Fill_Data()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdByPassL_Click(sender As Object, e As EventArgs) Handles cmdByPassL.Click
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            ByPassLemata()
            ClearFields()
        End If
    End Sub

    Private Sub txtGrHeight_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGrHeight.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtGrHeight.Text)
    End Sub

    Private Sub txtRepPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRepPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        If cmbRepReason.Text <> "" Then
            If txtRepPcs.Text <> "" Then
                If CInt(txtRepPcs.Text) > 0 Then
                    If Len(txtRep.Text) = 0 Then
                        txtRep.Text = "0"
                    End If
                    If CInt(txtRep.Text) + CInt(txtRepPcs.Text) > CInt(txtRetTap.Text) + CInt(txtRetBag.Text) Then
                        MsgBox("Invalid Repair Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Temp)
                        Exit Sub
                    End If
                    flxRepair.Rows.Add(cmbRepReason.Text,
                                       txtRepPcs.Text)
                    txtRep.Text = CalTotalPcs(flxRepair)

                    txtRepPcs.Text = ""
                    cmbRepReason.Text = ""
                    cmbRepReason.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub flxRepair_DoubleClick(sender As Object, e As EventArgs) Handles flxRepair.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse  = MsgBoxResult.Yes Then
            flxRepair.Rows.RemoveAt(flxRepair.CurrentRow.Index)
            txtRep.Text = CalTotalPcs(flxRepair)
        End If
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(1, intRow).Value)
        Next

    End Function

    Private Sub cmbRepReason_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbRepReason.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtRepPcs.Focus()
        End If
    End Sub

    Private Sub txtID100_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtID100.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub txtAms2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAms2.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub txtDfi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDfi.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub txtLabID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLabID.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub
End Class