
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_BagSection
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
    Dim mOrd As Integer
    Dim ParcelNo As String
    Dim PacketNo As String

    Private Sub Load_Section()
        Dim recsection As ADODB.Recordset

        cmbSection.Items.Clear()
        recsection = New ADODB.Recordset
        recsection.Open("SELECT * FROM tblBAGSections ORDER BY SecCode", AdoCN, 1, 1)
        If recsection.RecordCount Then
            recsection.MoveFirst()
            Do
                cmbSection.Items.Add(recsection.Fields("SecName").Value)
                recsection.MoveNext()
            Loop Until recsection.EOF
        End If
        cmbSection.SelectedIndex = 0
        Section = 1

    End Sub

    Private Sub frm_BagSection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        ClearFields()
        Load_Section()
        Load_RejReasons()
        Load_Size()
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
        txtNoPay.Text = ""
        txtPCUPcs.Text = "0"
        txtPCUCts.Text = "0"
        txtPRetCts.Text = "0"
        frmnew = True
        txtIssDate.Text = ""
        txtIssTime.Text = ""
        txtRetDate.Text = Format(Date.Now, "yyyy/MM/dd")
        txtRetTime.Text = Format(Date.Now, "HH:mm:ss")
        txtRejCts.Text = ""
        txtLostCts.Text = ""
        cmbRejReason.Text = ""
        cmbLength.Text = ""
        cmbLength.Visible = False
        txtYield1.Text = ""
        txtYield2.Text = ""
        flxDetails.Rows.Clear()
        txtTotBag.Text = "0"
        txtTotTap.Text = "0"
        txtTotCts.Text = "0.000"
        PictureBox1.Visible = False
        PictureBox2.Visible = False
        txtGrdPcs.Text = "0"
        txtGrdCts.Text = "0"
        chkGrading.Checked = False
        cmdParPkt.Focus()
    End Sub

    Private Sub ShowDetails()
        Dim mfldname As String
        Dim Rs As ADODB.Recordset
        Dim Rs1 As ADODB.Recordset
        Dim rs2 As ADODB.Recordset
        Dim rs3 As ADODB.Recordset
        Dim rs4 As ADODB.Recordset
        Dim rs5 As ADODB.Recordset
        Dim mIssPcs, mRetPcs, mFlowCount As Long

        cmdEmp.Focus()

        mStrSQL = "SELECT dbo.tblBAGPacket.*, dbo.VW_BAGPCU_Wt.PCUCts AS PCUCts FROM dbo.tblBAGPacket LEFT OUTER JOIN dbo.VW_BAGPCU_Wt ON dbo.tblBAGPacket.PktNo = dbo.VW_BAGPCU_Wt.PktNo AND dbo.tblBAGPacket.ParNo = dbo.VW_BAGPCU_Wt.ParNo " & _
                 "WHERE (dbo.tblBAGPacket.ParNo = '" & ParcelNo & "') AND (dbo.tblBAGPacket.PktNo = '" & PacketNo & "') AND (dbo.tblBAGPacket.DelDate IS NOT NULL) AND (dbo.tblBAGPacket.AccDate IS NOT NULL)"
        rs2 = New ADODB.Recordset
        rs2.Open(mStrSQL, AdoCN, 1, 1)
        If rs2.RecordCount > 0 Then
            If IsDBNull(rs2.Fields("PCUCts").Value) = True Then
                Caretspkt = rs2.Fields("PktCts").Value
            Else
                Caretspkt = rs2.Fields("PktCts").Value - rs2.Fields("PCUCts").Value
            End If
            mOrd = rs2.Fields("PktOrdNo").Value
        Else
            GoTo GoOut
        End If

        mStrSQL = "SELECT * FROM tblBAGPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "'"
        Rs1 = New ADODB.Recordset
        Rs1.Open(mStrSQL, AdoCN, 1, 1)
        mFlow = Rs1.Fields("PktFlow").Value

        mStrSQL = "SELECT * FROM tblBAGFlow WHERE Flow = '" & mFlow & "'"
        rs5 = New ADODB.Recordset
        rs5.Open(mStrSQL, AdoCN, 1, 1)
        mFlowCount = rs5.Fields("FlowSections").Value

        Rs = New ADODB.Recordset
        mStrSQL = "SELECT tblBAGissues.Sec,tblBAGissues.SecCount,tblBAGissues.ParNo,tblBAGissues.PktNo,tblBAGReturns.RetCts,tblBAGissues.IssDate,tblBAGissues.IssTime, " & _
                    "tblBAGReturns.RetDate,tblBAGReturns.RetTime,tblBAGissues.EmpNo AS EmpIss,tblBAGReturns.EmpNo,tblBAGissues.IssPcsT,tblBAGissues.IssPcsB,tblBAGissues.IssCts," & _
                    "tblBAGReturns.RetPcsT,tblBAGReturns.RetPcsB,tblBAGReturns.RejPcs,tblBAGReturns.PCUPcs,tblBAGReturns.PCUCts,tblBAGReturns.BroPcs,tblBAGReturns.LostPcs," & _
                    "tblBAGReturns.ExtPcs,tblBAGReturns.NopayPcs,tblBAGReturns.RepPcs,tblBAGissues.GrdTrf " & _
               "FROM tblBAGissues LEFT OUTER JOIN tblBAGReturns ON tblBAGissues.Sec = tblBAGReturns.Sec AND tblBAGissues.ParNo = tblBAGReturns.ParNo AND tblBAGissues.PktNo = tblBAGReturns.PktNo " & _
               "WHERE (tblBAGissues.ParNo = '" & ParcelNo & "') AND (tblBAGissues.PktNo = '" & PacketNo & "') ORDER BY tblBAGissues.Seccount DESC"
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount > 0 Then

            frmnew = False
            If Not IsDBNull(Rs.Fields("RetCts").Value) Then

                Section = Rs.Fields("Sec").Value

                rs4 = New ADODB.Recordset

                mStrSQL = "SELECT tblBAGissues.Sec, tblBAGissues.ParNo, tblBAGissues.PktNo, SUM(tblBAGReturns.RetCts) AS SumOfRetCts, tblBAGissues.IssPcsT, tblBAGissues.IssPcsB, tblBAGissues.IssCts,SUM(tblBAGReturns.RetPcsT) AS SumOfRetPcsT, " & _
                            "SUM(tblBAGReturns.RetPcsB) AS SumOfRetPcsB, SUM(tblBAGReturns.RejPcs) AS SumOfRej, SUM(tblBAGReturns.PCUPcs) AS SumOfPCUp, SUM(tblBAGReturns.PCUPcs) AS SumOfPCUc, SUM(tblBAGReturns.BroPcs) AS SumOfBro, SUM(tblBAGReturns.LostPcs) AS SumOfLost, " & _
                            "SUM(tblBAGReturns.ExtPcs) AS SumOfExt, SUM(tblBAGReturns.NopayPcs)AS SumOfNopay, SUM(tblBAGReturns.RepPcs) AS SumOfRepair " & _
                          "FROM tblBAGissues INNER JOIN tblBAGReturns ON tblBAGissues.PktNo = tblBAGReturns.PktNo AND tblBAGissues.ParNo = tblBAGReturns.ParNo AND tblBAGissues.Sec = tblBAGReturns.Sec " & _
                          "GROUP BY tblBAGissues.Sec, tblBAGissues.ParNo, tblBAGissues.PktNo, tblBAGissues.IssPcsT, tblBAGissues.IssPcsB, tblBAGissues.IssCts " & _
                          "HAVING (tblBAGissues.Sec = '" & Section & "') AND (tblBAGissues.ParNo = '" & ParcelNo & "') AND (tblBAGissues.PktNo = '" & PacketNo & "') " & _
                          "ORDER BY tblBAGissues.Sec DESC"
                rs4.Open(mStrSQL, AdoCN, 1, 1)

                mIssPcs = rs4.Fields("IssPcsT").Value + rs4.Fields("IssPcsB").Value
                mRetPcs = rs4.Fields("SumOfRetPcsT").Value + rs4.Fields("SumOfRetPcsB").Value + rs4.Fields("SumOfRej").Value + rs4.Fields("SumOfLost").Value - rs4.Fields("SumOfExt").Value
                rs4.Close()
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
                    rs3.Open("SELECT * FROM VWBAGTotalRetEmp WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' And Sec = '" & Section & "' ORDER BY Sec", AdoCN, 1, 1)
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

                    rs3 = New ADODB.Recordset
                    rs3.Open("SELECT SUM(RetPcsT) AS TotTap, SUM(RetPcsB) AS TotBag, ROUND(SUM(RetCts), 3) AS RetCts FROM dbo.tblBAGReturns WHERE ParNo = '" & ParcelNo & "' And PktNo = '" & PacketNo & "' And Sec = '" & Section & "'", AdoCN, 1, 1)
                    If rs3.RecordCount Then
                        If Not IsDBNull(rs3.Fields("TotTap").Value) Then
                            txtTotTap.Text = rs3.Fields("TotTap").Value
                            txtTotBag.Text = rs3.Fields("TotBag").Value
                            txtTotCts.Text = Format(rs3.Fields("RetCts").Value, "#0.000")
                        End If
                    End If
                    rs3 = Nothing

                    issued = True
                    PictureBox1.Visible = True
                    PictureBox2.Visible = True
                Else
                    If Rs.Fields("SecCount").Value < mFlowCount Then
                        issued = False
                        PictureBox1.Visible = False
                        PictureBox2.Visible = True
                        mfldname = "Flsec" & Rs.Fields("seccount").Value + 1
                        Section = rs5.Fields(mfldname).Value
                        cmbSection.SelectedIndex = Section - 1

                        Section = Rs.Fields("sec").Value

                        rs4 = New ADODB.Recordset

                        mStrSQL = "SELECT SUM(RetCts) AS SumOfRetCts, SUM(RetPcsT) AS SumOfRetPcsT, SUM(RetPcsB) AS SumOfRetPcsB, SUM(PCUPcs) AS SumOfPCUp, SUM(PCUCts) AS SumOfPCUr, " & _
                                    "SUM(PCUPCts) AS SumOfPCUc " & _
                                  "FROM tblBAGreturns " & _
                                  "WHERE (Sec = '" & Section & "') AND (ParNo = '" & ParcelNo & "') AND (PktNo = '" & PacketNo & "')"

                        'mStrSQL = "SELECT tblBAGissues.Sec, tblBAGissues.ParNo, tblBAGissues.PktNo, SUM(tblBAGReturns.RetCts) AS SumOfRetCts,tblBAGissues.EmpNo, tblBAGissues.IssPcsT, tblBAGissues.IssPcsB, tblBAGissues.IssCts," & _
                        '            "SUM(tblBAGReturns.RetPcsT) AS SumOfRetPcsT, SUM(tblBAGReturns.RetPcsB) AS SumOfRetPcsB, SUM(tblBAGReturns.RejPcs) AS SumOfRej, SUM(tblBAGReturns.PCUPcs) AS SumOfPCUp, SUM(tblBAGReturns.PCUCts) AS SumOfPCUr, " & _
                        '            "SUM(tblBAGReturns.PCUPCts) AS SumOfPCUc, SUM(tblBAGReturns.BroPcs) AS SumOfBro, SUM(tblBAGreturns.LostPcs) AS SumOfLost,  SUM(tblBAGreturns.ExtPcs) AS SumOfExt, SUM(tblBAGreturns.NopayPcs)AS SumOfNopay, " & _
                        '            "SUM(tblBAGreturns.RepPcs) AS SumOfRepair " & _
                        '          "FROM tblBAGissues INNER JOIN tblBAGreturns ON tblBAGissues.PktNo = tblBAGreturns.PktNo AND tblBAGissues.ParNo = tblBAGreturns.ParNo AND tblBAGissues.Sec = tblBAGreturns.Sec " & _
                        '          "GROUP BY tblBAGissues.Sec, tblBAGissues.ParNo, tblBAGissues.PktNo,tblBAGissues.EmpNo, tblBAGissues.IssPcsT, tblBAGissues.IssPcsB, tblBAGissues.IssCts " & _
                        '          "HAVING (tblBAGissues.Sec = '" & Section & "') AND (tblBAGissues.ParNo = '" & ParcelNo & "') AND (tblBAGissues.PktNo = '" & PacketNo & "') " & _
                        '          "ORDER BY tblBAGissues.Sec DESC"

                        rs4.Open(mStrSQL, AdoCN, 1, 1)

                        If rs4.Fields("SumOfRetPcsT").Value > rs4.Fields("SumOfPCUp").Value Then
                            txtIssTap.Text = rs4.Fields("SumOfRetPcsT").Value - rs4.Fields("SumOfPCUp").Value
                            txtIssBag.Text = rs4.Fields("SumOfRetPcsB").Value
                        Else
                            If rs4.Fields("SumOfRetPcsB").Value > rs4.Fields("SumOfPCUp").Value Then
                                txtIssBag.Text = rs4.Fields("SumOfRetPcsB").Value - rs4.Fields("SumOfPCUp").Value
                                txtIssTap.Text = rs4.Fields("SumOfRetPcsT").Value
                            End If
                        End If

                        txtIssCts.Text = Format(rs4.Fields("SumOfRetCts").Value, "#0.000") - Format(rs4.Fields("SumOfPCUc").Value, "#0.000")

                        txtIssTap.Enabled = False
                        txtIssBag.Enabled = False
                        txtIssCts.Enabled = False

                        Section = Rs.Fields("seccount").Value

                        Rs.Close()
                    Else
                        PictureBox1.Visible = False
                        PictureBox2.Visible = False

                        MsgBox("Completed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Rs.Close()
                        ClearFields()

                    End If
                End If

            Else
                If Rs.Fields("GrdTrf").Value = 1 Then
                    PictureBox1.Visible = False
                    PictureBox2.Visible = False

                    MsgBox("Packet Transfered to Grading", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Rs.Close()
                    ClearFields()

                    Exit Sub
                ElseIf Rs.Fields("GrdTrf").Value = 2 Then
                    PictureBox1.Visible = False
                    PictureBox2.Visible = False

                    MsgBox("Packet still in Grading", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Rs.Close()
                    ClearFields()

                    Exit Sub
                End If

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

                If Section = 4 Then
                    cmbLength.Visible = True
                Else
                    cmbLength.Visible = False
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
            frmnew = True
            issued = False
            PictureBox2.Visible = True
            Rs.Close()
            Section = 0
            cmbSection.SelectedIndex = Section
            txtIssTap.Text = rs2.Fields("PktPcs").Value
            txtIssBag.Text = "0"
            txtIssCts.Text = Format(Caretspkt, "#0.000")
            cmdEmp.Focus()  'get ready to scan IC NO
        End If
        rs2.Close()

        Exit Sub
GoOut:

    End Sub

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Datavalid = False
        Parcel = True
        Instring = UCase(InputBox("Enter Par/Pkt Number"))
        ParcelLen = Len(Instring)
        If ParcelLen > 9 Then
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

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub RecSave()
        Dim dataok As Boolean
        Dim stiss, stret As Integer
        Dim ciss, cret As Single
        Dim ChkString As String

        dataok = True
        ChkString = ""

        If Trim(ICNo) = "" Then
            MsgBox("Invalid Employee No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If issued Then
            If Len(txtRetDate.Text) < 2 Then Exit Sub

            stiss = CInt(txtIssTap.Text) + CInt(txtIssBag.Text)
            stret = CInt(txtTotBag.Text) + CInt(txtTotTap.Text) + CInt(txtRetTap.Text) + CInt(txtRetBag.Text) + CInt(txtRej.Text) - CInt(txtExt.Text) + CInt(txtLost.Text) + CInt(txtBro.Text)

            If stiss < stret Then
                dataok = False
                strmsg = "Stones issued " & stiss & "   Stones returned " & stret
                If MsgBox(strmsg, MsgBoxStyle.Question + MsgBoxStyle.OkCancel, Me.Text) = MsgBoxResult.Ok Then
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

            If stiss <> stret Then
                strmsg = "Stones issued " & stiss & "   Stones returned " & stret & ". Are you sure?"
                PBResponse = MsgBox(strmsg, MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
                If PBResponse = MsgBoxResult.Yes Then

                Else
                    dataok = False
                    Exit Sub
                End If
            End If

            ciss = CSng(txtIssCts.Text)
            cret = CSng(txtTotCts.Text) + CSng(txtRetCts.Text)

            If ciss < cret Then
                dataok = False
                strmsg = "Carats issued " & Format(ciss, "##.###") & "   Carats returned " & Format(cret, "##.###")
                If MsgBox(strmsg, MsgBoxStyle.Question + MsgBoxStyle.OkCancel, Me.Text) = MsgBoxResult.Ok Then
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

            If txtGrdPcs.Text <> "" Then
                If CInt(txtGrdPcs.Text) > CInt(txtRetBag.Text) + CInt(txtRetTap.Text) Then
                    MsgBox("Invalid Grading Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Temp)
                    Exit Sub
                End If
                If CSng(txtGrdCts.Text) > CSng(txtRetCts.Text) Then
                    MsgBox("Invalid Grading Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Temp)
                    Exit Sub
                End If
            End If

            'If cmbSection.SelectedIndex + 1 = 3 And strRight(ParcelNo, 1) = "S" Then
            '    If CInt(txtGrdPcs.Text) <= 0 Then
            '        MsgBox("Invalid Grading Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Temp)
            '        Exit Sub
            '    End If
            'End If

            If CInt(txtRej.Text) + CInt(txtBro.Text) > 0 Then
                PBResponse = MsgBox("You are entering a Reject/Broken. Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
                If PBResponse = MsgBoxResult.Yes Then

                Else
                    dataok = False
                    Exit Sub
                End If
            End If
        Else
            'rsComSql_2 = New ADODB.Recordset
            'rsComSql_2.Open("SELECT EmpNo FROM tblBAGReturns WHERE EmpNo = '" & txtEmp.Text & "' AND LostPcs > 0 AND Active = 1", AdoCN, 1, 1)
            'If rsComSql_2.RecordCount Then
            '    MsgBox("Have a Lost to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '    Exit Sub
            'End If
            'rsComSql_2 = Nothing

            rsComSql_2 = New ADODB.Recordset
            rsComSql_2.Open("SELECT EmpNo FROM VW_ALLLostEmpNo WHERE EmpNo = '" & txtEmp.Text & "'", AdoCN, 1, 1)
            If rsComSql_2.RecordCount Then
                MsgBox("Have a Lost to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql_2 = Nothing

            If intCheckIssDate = 1 Then
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT FullEmpNo, CATEGORY, Pay FROM VW_EMP_MASTER_SMALL5 WHERE (CATEGORY LIKE 'DIRECT%' OR CATEGORY LIKE 'TEMP%') AND (Pay = 1) AND (FullEmpNo = '" & txtEmp.Text & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT TOP (100) PERCENT DATEDIFF(d, dbo.tblBAGIssues.IssDate, GETDATE()) AS Days " & _
                                    "FROM dbo.tblBAGIssues INNER JOIN dbo.tblParcel ON dbo.tblBAGIssues.ParNo = dbo.tblParcel.GrpParNo LEFT OUTER JOIN " & _
                                        "dbo.tblBAGReturns ON dbo.tblBAGIssues.ParNo = dbo.tblBAGReturns.ParNo AND dbo.tblBAGIssues.PktNo = dbo.tblBAGReturns.PktNo AND dbo.tblBAGIssues.Sec = dbo.tblBAGReturns.Sec " & _
                                    "WHERE (dbo.tblBAGReturns.PktNo IS NULL) AND (dbo.tblParcel.Complete = 0) AND (dbo.tblParcel.Depart = 'Baguettes') AND (DATEDIFF(d, dbo.tblBAGIssues.IssDate, GETDATE()) > '" & intDelayDays & "') AND (dbo.tblBAGIssues.EmpNo = '" & txtEmp.Text & "')", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        MsgBox("Have a Delayed Packet to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql_2 = Nothing

                    If intCheckPastIssues = 1 Then
                        dtpToday = GetToday()
                        rsComSql_2 = New ADODB.Recordset
                        rsComSql_2.Open("SELECT TOP (100) PERCENT dbo.tblBAGIssues.IssDate " & _
                                        "FROM dbo.tblBAGIssues INNER JOIN dbo.tblParcel ON dbo.tblBAGIssues.ParNo = dbo.tblParcel.GrpParNo LEFT OUTER JOIN " & _
                                            "dbo.tblBAGReturns ON dbo.tblBAGIssues.ParNo = dbo.tblBAGReturns.ParNo AND dbo.tblBAGIssues.PktNo = dbo.tblBAGReturns.PktNo AND dbo.tblBAGIssues.Sec = dbo.tblBAGReturns.Sec " & _
                                        "WHERE (dbo.tblBAGReturns.PktNo IS NULL) AND (dbo.tblParcel.Complete = 0) AND (dbo.tblParcel.Depart = 'Baguettes') AND (dbo.tblBAGIssues.EmpNo = '" & txtEmp.Text & "') AND (dbo.tblBAGIssues.IssDate < '" & Format(dtpToday, "MM/dd/yyyy") & "')", AdoCN, 1, 1)
                        If rsComSql_2.RecordCount Then
                            MsgBox("Have a Past Packet to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                        rsComSql_2 = Nothing
                    End If
                    
                End If
                rsComSql_1 = Nothing
            End If

            If Len(txtIssDate.Text) < 2 Then dataok = False
            '**********************
            If Section = 0 Then
                If Not CSng(txtIssCts.Text) > 0 Then dataok = False
                If Not CInt(txtIssTap.Text) > 0 Then dataok = False
            End If
            '**********************
        End If

        If dataok = True Then DataSave()
        cmdParPkt.Focus()

    End Sub

    Private Sub DataSave()
        Dim strGrPktNo As String
        Dim intGrdTrf As Integer
        Dim intActive As Integer

        intActive = 0

        dtpToday = GetToday()
        intGrdTrf = 0
        If issued = False Then
            'Issue packet
            'If strRight(ParcelNo, 1) = "C" And cmbSection.SelectedIndex + 1 = 4 Then
            '    intGrdTrf = 1
            'End If

            mStrSQL = "INSERT INTO tblBAGIssues(OrderNo,ParNo,PktNo,Flow,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,Sec,SecCount,DoneBy,GrdTrf) " & _
                      "VALUES('" & mOrd & "','" & ParcelNo & "','" & PacketNo & "','" & mFlow & "','" & ICNo & "'," & CInt(txtIssTap.Text) & "," & _
                        "" & CInt(txtIssBag.Text) & "," & CSng(txtIssCts.Text) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "'," & _
                        "" & cmbSection.SelectedIndex + 1 & "," & Section + 1 & ",'" & PBUser_EmpNo & "'," & intGrdTrf & ")"

            AdoCN.Execute(mStrSQL)

            If cmbSection.SelectedIndex + 1 = 4 Then
                strGrPktNo = "L" & PacketNo
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT ID FROM tblExpRghTypes WHERE Department = 'Baguettes' AND ParNo = '" & ParcelNo & "' AND PktNo = '" & strGrPktNo & "' AND Type = 'L' AND OK = 0", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    AdoCN.Execute("UPDATE tblExpRghTypes SET OK = 1 WHERE ID = '" & rsComSql.Fields("ID").Value & "'")
                End If
                rsComSql = Nothing
            End If
        Else
            'Return Packet
            If CInt(txtRej.Text) > 0 Then
                If cmbRejReason.Text = "" Then
                    MsgBox("Please enter the Reject Reason", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            If CInt(txtPCUPcs.Text) > 0 Then
                If cmbRejReason.Text = "" Then
                    MsgBox("Please enter the Reject Reason", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            If CDbl(txtLost.Text) > 0 Then
                intActive = 1
            End If

            mStrSQL = "INSERT INTO tblBAGReturns(OrderNo,ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,PCUPcs,PCUCts,PCUPCts,LostCts,BroPcs,RepPcs, " & _
                        "NopayPcs,RetDate,RetTime,ExtPcs,Status,RejReason,GrdPcs,GrdCts,DoneBy,Active) " & _
                      "VALUES ('" & mOrd & "','" & ParcelNo & "','" & PacketNo & "','" & mFlow & "' ," & Section & "," & (cmbSection.SelectedIndex) + 1 & ",'" & Mid(Trim(ICNo), 1, 6) & "' ," & CInt(txtRetTap.Text) & "," & _
                        "" & CInt(txtRetBag.Text) & "," & CSng(txtRetCts.Text) & "," & CInt(txtRej.Text) & "," & CSng(txtRejCts.Text) & "," & CInt(txtLost.Text) & "," & CInt(txtPCUPcs.Text) & "," & CSng(txtPCUCts.Text) & "," & CSng(txtPRetCts.Text) & "," & _
                        "" & CSng(txtLostCts.Text) & "," & CInt(txtBro.Text) & "," & CInt(txtRep.Text) & "," & CInt(txtNoPay.Text) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "'," & CInt(txtExt.Text) & ",0," & _
                        "'" & cmbRejReason.Text & "'," & CInt(txtGrdPcs.Text) & "," & CSng(txtGrdCts.Text) & ",'" & PBUser_EmpNo & "','" & intActive & "')"

            AdoCN.Execute(mStrSQL)

            If cmbSection.SelectedIndex + 1 = 4 Then
                If CInt(txtRetTap.Text) + CInt(txtRetBag.Text) = 1 And cmbLength.Text <> "" Then
                    AdoCN.Execute("INSERT INTO tblBAGPacketDetails(ParNo,PktNo,Pcs,Cts,Length,Width) " & _
                                  "VALUES('" & ParcelNo & "','" & PacketNo & "'," & CInt(txtRetTap.Text) + CInt(txtRetBag.Text) & "," & CDbl(txtRetCts.Text) & ",'" & cmbLength.Text & "','')")
                End If
            End If
            If cmbSection.SelectedIndex + 1 = 3 Then
                If CInt(txtGrdPcs.Text) > 0 Then
                    Dep_Grading_Trf("Baguettes", 8787, ParcelNo, "L" & PacketNo, txtGrdPcs.Text, txtGrdCts.Text, txtGrdPcs.Text, txtGrdCts.Text, strRight(ParcelNo, 1))
                End If
            End If

        End If
        ClearFields()
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

    Private Sub Load_Size()
        cmbLength.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblBAGSize ORDER BY Size", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbLength.Items.Add(rsComSql.Fields("Size").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub txtRetCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRetCts.KeyPress
        Dim Yld As Single
        Dim wst As Single

        e.Handled = NumericOnly(Asc(e.KeyChar), txtRetCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtRej.Text = "0"
            txtRejCts.Text = "0"
            txtLost.Text = "0"
            txtLostCts.Text = "0"
            txtRep.Text = "0"
            txtBro.Text = "0"
            txtExt.Text = "0"
            txtNoPay.Text = "0"
            txtGrdPcs.Text = "0"
            txtGrdCts.Text = "0"

            If Not IsNumeric(txtRetCts.Text) Then
                txtRetCts.Text = "0"
                txtRej.Focus()
            Else
                txtYield1.Text = Format(((CDbl(txtRetCts.Text) + CDbl(txtTotCts.Text)) / Caretspkt) * 100, "#0.00")
                Yld = txtYield1.Text
                wst = CSng(txtIssCts.Text) - (CSng(txtRetCts.Text) + CDbl(txtTotCts.Text))
                txtYield2.Text = Format((wst / Caretspkt) * 100, "#0.00")
            End If
        End If
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
            txtRetCts.Focus()
        End If
    End Sub

    Private Sub cmdByPass_Click(sender As Object, e As EventArgs) Handles cmdByPass.Click
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            ByPassSection()
            txtSection.Text = ""
            txtSection.ReadOnly = True
            chkByPass.Checked = False
            cmdByPass.Enabled = False
            ClearFields()
        End If
    End Sub

    Private Sub ByPassSection()
        Dim Rs1 As ADODB.Recordset
        Dim rs2 As ADODB.Recordset
        Dim rs3 As ADODB.Recordset
        Dim rs4 As ADODB.Recordset
        Dim dblIssPcs As Double
        Dim dblIssCts As Double
        Dim strFlow As String
        Dim intSec As Integer
        Dim intSecCount As Integer
        Dim dblRetPCsT As Double
        Dim dblRetPcsB As Double
        Dim dblRetCts As Double

        If txtSection.Text <> "" Then
            If txtParPkt.Text = "" Then MsgBox("Please enter the Parcel No/Packet No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If txtEmp.Text = "" Then MsgBox("Please enter the Employee No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

            Rs1 = New ADODB.Recordset
            Rs1.Open("SELECT * FROM tblBAGPacket WHERE ParNo = '" & ParcelNo & "' And PktNo = '" & PacketNo & "'", AdoCN, 1, 1)
            If Rs1.RecordCount Then
                dblIssPcs = Rs1.Fields("PktPcs").Value
                dblIssCts = Rs1.Fields("PktCts").Value
                strFlow = Rs1.Fields("PktFlow").Value
                dblIssCts = Math.Round(dblIssCts, 3)

                rs2 = New ADODB.Recordset
                rs2.Open("SELECT * FROM tblBAGIssues WHERE ParNo = '" & ParcelNo & "' And PktNo = '" & PacketNo & "' ORDER BY Sec DESC", AdoCN, 1, 1)
                If rs2.RecordCount = 0 Then
                    intSec = 1
                    For intSecCount = 1 To CInt(txtSection.Text)
                        rs3 = New ADODB.Recordset
                        rs3.Open("SELECT * FROM tblBAGFlow WHERE Flow = '" & strFlow & "'", AdoCN, 1, 1)
                        If rs3.RecordCount Then
                            intSec = rs3.Fields("Flsec" & intSecCount).Value
                        Else
                            intSec = intSecCount
                        End If
                        rs3 = Nothing
                        If intSec > CInt(txtSection.Text) Then Exit For
                        If intSec = 0 Then Exit For
                        'Issues
                        AdoCN.Execute("INSERT INTO tblBAGIssues(OrderNo,ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                      "VALUES(1,'" & ParcelNo & "','" & PacketNo & "'," & intSec & ",'" & strFlow & "'," & intSecCount & ",'" & txtEmp.Text & "'," & dblIssPcs & ",0," & dblIssCts & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "')")

                        'Returns
                        AdoCN.Execute("INSERT INTO tblBAGReturns(OrderNo,ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,PCUCts,PCUPcs,PCUPCts,Gra_Trf,RejReason,BLostPcs,BLostCts,DoneBy) " & _
                                      "VALUES(1,'" & ParcelNo & "','" & PacketNo & "','" & strFlow & "','" & intSecCount & "','" & intSec & "','" & txtEmp.Text & "','" & dblIssPcs & "',0,'" & dblIssCts & "',0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,0,0,'',0,0,'" & PBUser_EmpNo & "')")
                    Next
                    MsgBox("Successfully By Passed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Else
                    rs4 = New ADODB.Recordset
                    rs4.Open("SELECT * FROM VW_BAGRealReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & rs2.Fields("Sec").Value & "", AdoCN, 1, 1)
                    If rs4.RecordCount Then
                        If rs2.Fields("IssPcsT").Value + rs2.Fields("IssPcsB").Value = rs4.Fields("SumOfRetPcst").Value + rs4.Fields("SumOfRetPcsB").Value + rs4.Fields("SumOfRej").Value + rs4.Fields("SumOfLost").Value - rs4.Fields("SumOfExt").Value Then
                            dblRetPCsT = rs4.Fields("SumOfRetPcst").Value
                            dblRetPcsB = rs4.Fields("SumOfRetPcsB").Value
                            dblRetCts = rs4.Fields("SumOfRetCts").Value
                            dblRetCts = Math.Round(dblRetCts, 3)
                            For intSecCount = rs2.Fields("Seccount").Value + 1 To CInt(txtSection.Text)
                                rs3 = New ADODB.Recordset
                                rs3.Open("SELECT * FROM tblBAGFlow WHERE Flow = '" & strFlow & "'", AdoCN, 1, 1)
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
                                AdoCN.Execute("INSERT INTO tblBAGIssues(OrderNo,ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                              "VALUES(1,'" & ParcelNo & "','" & PacketNo & "','" & intSec & "','" & strFlow & "','" & intSecCount & "','" & txtEmp.Text & "','" & dblRetPCsT & "','" & dblRetPcsB & "','" & dblRetCts & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "')")

                                'Returns
                                AdoCN.Execute("INSERT INTO tblBAGReturns(OrderNo,ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,PCUCts,PCUPcs,PCUPCts,Gra_Trf,RejReason,BLostPcs,BLostCts,DoneBy) " & _
                                              "VALUES(1,'" & ParcelNo & "','" & PacketNo & "','" & strFlow & "','" & intSecCount & "','" & intSec & "','" & txtEmp.Text & "','" & dblRetPCsT & "','" & dblRetPcsB & "','" & dblRetCts & "',0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,0,0,'',0,0,'" & PBUser_EmpNo & "')")

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

    Private Sub chkByPass_CheckedChanged(sender As Object) Handles chkByPass.CheckedChanged
        If chkByPass.Checked = True Then
            txtSection.ReadOnly = False
            cmdByPass.Enabled = True
        Else
            txtSection.ReadOnly = True
            cmdByPass.Enabled = False
        End If
    End Sub

    Private Sub txtPRetCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPRetCts.KeyPress
        Dim Yld As Single

        e.Handled = NumericOnly(Asc(e.KeyChar), txtPRetCts.Text)
        If Asc(e.KeyChar) = 13 Then
            Yld = Math.Round((CDbl(txtRetCts.Text) / Caretspkt) * 100, 2)
            txtPCUCts.Text = Math.Round((100 / Yld) * CDbl(txtPRetCts.Text), 3)
        End If
    End Sub

    Private Sub txtRej_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRej.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtRejCts.Focus()
        End If
    End Sub

    Private Sub txtGrdPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGrdPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtGrdCts.Focus()
        End If
    End Sub

    Private Sub txtGrdCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGrdCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtGrdCts.Text)
    End Sub

    Private Sub chkGrading_CheckedChanged(sender As Object) Handles chkGrading.CheckedChanged
        If chkGrading.Checked = True Then
            txtGrdPcs.Text = CInt(txtRetBag.Text) + CInt(txtRetTap.Text)
            txtGrdCts.Text = txtRetCts.Text
        Else
            txtGrdPcs.Text = "0"
            txtGrdCts.Text = "0"
        End If
    End Sub
End Class