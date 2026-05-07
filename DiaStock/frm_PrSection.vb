
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_PrSection
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
        recsection.Open("SELECT * FROM tblPRSections ORDER BY SecCode", AdoCN, 1, 1)
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

    Private Sub frm_PrSection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        ClearFields()
        Load_Section()
        Load_RejReasons()
    End Sub

    Private Sub ClearFields()
        txtParPkt.Text = ""
        txtEmp.Text = ""
        txtRetCtsP.Text = ""
        txtRetP.Text = ""
        txtRetC.Text = ""
        txtIssCtsP.Text = ""
        txtIssP.Text = ""
        txtIssC.Text = ""
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
        txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
        txtRetTime.Text = Format(Date.Now, "HH:mm:ss")
        txtRejCts.Text = ""
        txtLostCts.Text = ""
        cmbRejReason.Text = ""
        txtYield1.Text = ""
        txtYield2.Text = ""
        PictureBox1.Visible = False
        PictureBox2.Visible = False
        flxDetails.Rows.Clear()
        txtTotBag.Text = "0"
        txtTotTap.Text = "0"
        txtTotCts.Text = "0.000"
        cmdParPkt.Focus()
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
                If MsgBox("IC Numbers do not match. Proceed Anyway?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)  = MsgBoxResult.Yes Then
                    txtEmp.Text = ICNo
                    txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
                    txtRetTime.Text = Format(Date.Now, "HH:mm")
                    txtRetP.Focus()
                Else
                    txtEmp.Text = ""
                    Datavalid = False
                    ICNo = ""
                End If
            Else
                txtEmp.Text = ICNo
                txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
                txtRetTime.Text = Format(Date.Now, "HH:mm")
                txtRetP.Focus()
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

    Private Sub ShowDetails()
        Dim mfldname As String
        Dim Rs As ADODB.Recordset
        Dim Rs1 As ADODB.Recordset
        Dim rs2 As ADODB.Recordset
        Dim Rs3 As ADODB.Recordset
        Dim rs4 As ADODB.Recordset
        Dim rs5 As ADODB.Recordset

        Dim mIssPcs, mRetPcs, mFlowCount As Long

        cmdEmp.Focus()

        mStrSQL = "SELECT tblPRPacket.*, VW_PRPCU_Wt.PCUCts AS PCUCts, VW_PRPCU_Wt.PCUPCts AS PCUPCts FROM tblPRPacket LEFT OUTER JOIN VW_PRPCU_Wt ON tblPRPacket.PktNo = VW_PRPCU_Wt.PktNo AND tblPRPacket.ParNo = VW_PRPCU_Wt.ParNo " & _
                 "WHERE (tblPRPacket.ParNo = '" & ParcelNo & "') AND (tblPRPacket.PktNo = '" & PacketNo & "') AND (tblPRPacket.DelDate IS NOT NULL) AND (tblPRPacket.AccDate IS NOT NULL)"
        rs2 = New ADODB.Recordset
        rs2.Open(mStrSQL, AdoCN, 1, 1)
        If rs2.RecordCount > 0 Then
            If IsDBNull(rs2.Fields("PCUCts").Value) = True Then
                Caretspkt = rs2.Fields("PktCts").Value
            Else
                Caretspkt = rs2.Fields("PktCts").Value - rs2.Fields("PCUCts").Value
            End If
        Else
            GoTo GoOut
        End If

        mStrSQL = "SELECT * FROM tblPRPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "'"
        Rs1 = New ADODB.Recordset
        Rs1.Open(mStrSQL, AdoCN, 1, 1)
        mFlow = Rs1.Fields("PktFlow").Value


        mStrSQL = "SELECT * FROM tblPRFlow WHERE Flow = '" & mFlow & "'"
        rs5 = New ADODB.Recordset
        rs5.Open(mStrSQL, AdoCN, 1, 1)
        mFlowCount = rs5.Fields("FlowSections").Value

        Rs = New ADODB.Recordset
        mStrSQL = "SELECT tblPRissues.Sec, tblPRissues.SecCount, tblPRissues.ParNo, tblPRissues.PktNo, tblPRReturns.RetCtsC, tblPRReturns.RetCtsP, tblPRissues.IssDate, tblPRissues.IssTime, tblPRReturns.RetDate, tblPRReturns.RetTime," & _
                "tblPRissues.EmpNo AS EmpIss, tblPRReturns.EmpNo, tblPRissues.IssPcsC, tblPRissues.IssPcsP, tblPRissues.IssCtsC, tblPRissues.IssCtsP, tblPRReturns.RetPcsC, tblPRReturns.RetPcsP, tblPRReturns.RejPcs , tblPRReturns.PCUPcs, " & _
                "tblPRReturns.PCUCts, tblPRReturns.BroPcs, tblPRReturns.LostPcs, tblPRReturns.ExtPcs, tblPRReturns.NopayPcs, tblPRReturns.RepPcs " & _
               "FROM tblPRissues LEFT OUTER JOIN tblPRReturns ON tblPRissues.Sec = tblPRReturns.Sec AND tblPRissues.ParNo = tblPRReturns.ParNo AND tblPRissues.PktNo = tblPRReturns.PktNo " & _
               "WHERE (tblPRissues.ParNo = '" & ParcelNo & "') AND (tblPRissues.PktNo = '" & PacketNo & "') " & _
               "ORDER BY tblPRissues.Seccount DESC"
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount > 0 Then

            frmnew = False
            If Not IsDBNull(Rs.Fields("RetCtsP").Value) And Not IsDBNull(Rs.Fields("RetCtsC").Value) Then

                Section = Rs.Fields("Sec").Value

                Dim mSecret As String
                rs4 = New ADODB.Recordset

                mStrSQL = "SELECT tblPRIssues.Sec,  tblPRIssues.ParNo,  tblPRIssues.PktNo,  tblPRIssues.IssPcsP,  tblPRIssues.IssPcsC, SUM(tblPRReturns.RetPcsC) AS RetPcsC, SUM(tblPRReturns.RetPcsP) AS RetPcsP, SUM(tblPRReturns.RejPcs) AS SumOfRej, " & _
                            "SUM(tblPRReturns.BroPcs) AS SumOfBro, SUM(tblPRReturns.LostPcs) AS SumOfLost, SUM(tblPRReturns.ExtPcs) AS SumOfExt, SUM(tblPRReturns.PCUPcs) AS SumOfPCUp, SUM(tblPRReturns.PCUPcs) AS SumOfPCUc, " & _
                            "SUM(tblPRReturns.NopayPcs) AS SumOfNopay, SUM(tblPRReturns.RepPcs) AS SumOfRepair " & _
                          "FROM tblPRIssues INNER JOIN tblPRReturns ON  tblPRIssues.PktNo =  tblPRReturns.PktNo AND  tblPRIssues.ParNo =  tblPRReturns.ParNo AND tblPRIssues.Sec = tblPRReturns.Sec " & _
                          "GROUP BY  tblPRIssues.Sec,  tblPRIssues.ParNo,  tblPRIssues.PktNo,  tblPRIssues.IssPcsP,  tblPRIssues.IssPcsC,  tblPRIssues.IssCtsP, tblPRIssues.IssCtsC " & _
                          "HAVING (tblPRIssues.ParNo = '" & ParcelNo & "') AND (tblPRIssues.PktNo = '" & PacketNo & "') AND ( tblPRIssues.Sec = '" & Section & "') " & _
                          "ORDER BY tblPRIssues.Sec DESC"
                rs4.Open(mStrSQL, AdoCN, 1, 1)

                mIssPcs = rs4.Fields("IssPcsP").Value + rs4.Fields("IssPcsC").Value
                mRetPcs = rs4.Fields("RetPcsP").Value + rs4.Fields("RetPcsC").Value + rs4.Fields("SumOfRej").Value + rs4.Fields("SumOfLost").Value + rs4.Fields("SumOfBro").Value - rs4.Fields("SumOfExt").Value
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
                    txtIssP.Text = Rs.Fields("IssPcsP").Value
                    txtIssC.Text = Rs.Fields("IssPcsC").Value
                    txtIssCtsP.Text = Format(Rs.Fields("IssCtsP").Value, "#0.000")
                    txtIssCtsC.Text = Format(Rs.Fields("IssCtsC").Value, "#0.000")
                    txtIssP.Enabled = False
                    txtIssC.Enabled = False
                    txtIssCtsP.Enabled = False
                    txtIssCtsC.Enabled = False

                    rs3 = New ADODB.Recordset
                    Rs3.Open("SELECT * FROM VWPRTotalRetEmp WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' And Sec = '" & Section & "' ORDER BY Sec", AdoCN, 1, 1)
                    If Rs.RecordCount Then
                        rs3.MoveFirst()
                        While Not rs3.EOF
                            flxDetails.Rows.Add(Rs3.Fields("EmpNo").Value,
                                                Format(Rs3.Fields("RetDate").Value, "yyyy/MM/dd"),
                                                Rs3.Fields("sumofrett").Value,
                                                Rs3.Fields("sumofretb").Value,
                                                Format(Rs3.Fields("RetCtsC").Value, "#0.000"),
                                                Format(Rs3.Fields("RetCtsP").Value, "#0.000"),
                                                Rs3.Fields("sumofrejp").Value,
                                                Rs3.Fields("sumofrejc").Value,
                                                Rs3.Fields("sumoflostp").Value,
                                                Rs3.Fields("sumofbro").Value,
                                                Rs3.Fields("SumofExt").Value)

                            rs3.MoveNext()
                        End While
                    End If
                    rs3 = Nothing

                    txtTotBag.Text = "0"
                    txtTotTap.Text = "0"
                    txtTotCts.Text = "0.000"

                    rs3 = New ADODB.Recordset
                    Rs3.Open("SELECT SUM(RetPcsC) AS TotTap, SUM(RetPcsP) AS TotBag, ROUND(SUM(RetCtsC + RetCtsP), 3) AS RetCts FROM dbo.tblPRReturns WHERE ParNo = '" & ParcelNo & "' And PktNo = '" & PacketNo & "' And Sec = '" & Section & "'", AdoCN, 1, 1)
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

                        mSecret = ""
                        rs4 = New ADODB.Recordset

                        mStrSQL = "SELECT tblPRissues.Sec, tblPRissues.ParNo, tblPRissues.PktNo, SUM(tblPRReturns.RetCtsP+tblPRReturns.RetCtsC) AS SumOfRetCts,tblPRissues.EmpNo, tblPRissues.IssPcsP, tblPRissues.IssPcsC, SUM(tblPRReturns.RetPcsP) AS SumOfRetPcsP, SUM(tblPRReturns.RetPcsC) AS SumOfRetPcsC, SUM(tblPRReturns.RetCtsC) AS SumOfRetCtsC," & _
                                    "SUM(tblPRReturns.RetCtsP) AS SumOfRetCtsP,SUM(tblPRReturns.RejPcs) AS SumOfRej,SUM(tblPRReturns.BroPcs) AS SumOfBro, SUM(tblPRReturns.LostPcs) AS SumOfLost, SUM(tblPRReturns.ExtPcs) AS SumOfExt, SUM(tblPRReturns.NopayPcs)AS SumOfNopay, SUM(tblPRReturns.PCUPcs) AS SumOfPCUp, SUM(tblPRReturns.PCUCts) AS SumOfPCUr, SUM(tblPRReturns.PCUPCts) AS SumOfPCUc, SUM(tblPRReturns.RepPcs) AS SumOfRepair " & _
                                  "FROM tblPRIssues INNER JOIN tblPRReturns ON tblPRissues.PktNo = tblPRReturns.PktNo AND tblPRissues.ParNo = tblPRReturns.ParNo AND tblPRissues.Sec = tblPRReturns.Sec " & _
                                  "GROUP BY tblPRissues.Sec, tblPRissues.ParNo, tblPRissues.PktNo,tblPRissues.EmpNo, tblPRissues.IssPcsP, tblPRissues.IssPcsC, tblPRissues.IssCtsP,tblPRissues.IssCtsC " & _
                                  "HAVING (tblPRissues.Sec = '" & Section & "') AND (tblPRIssues.ParNo = '" & ParcelNo & "') AND (tblPRIssues.PktNo = '" & PacketNo & "') " & _
                                  "ORDER BY tblPRIssues.Sec DESC"
                        rs4.Open(mStrSQL, AdoCN, 1, 1)

                        If IsDBNull(rs4.Fields("SumOfPCUp").Value) = True Then
                            txtIssP.Text = rs4.Fields("SumOfRetPcsP").Value + rs4.Fields("SumOfRetPcsC").Value
                        Else
                            txtIssP.Text = (rs4.Fields("SumOfRetPcsP").Value + rs4.Fields("SumOfRetPcsC").Value) - rs4.Fields("SumOfPCUp").Value
                        End If
                        txtIssC.Text = "0"

                        If IsDBNull(rs4.Fields("SumOfPCUr").Value) = True Then
                            txtIssCtsP.Text = CSng(Format(rs4.Fields("SumOfRetCtsP").Value, "#0.0##")) + CSng(Format(rs4.Fields("SumOfRetCtsC").Value, "#0.0##"))
                        Else
                            txtIssCtsP.Text = CSng((Format(rs4.Fields("SumOfRetCtsP").Value, "#0.0##")) + CSng(Format(rs4.Fields("SumOfRetCtsC").Value, "#0.0##"))) - CSng(Format(rs4.Fields("SumOfPCUc").Value, "#0.0##"))
                            txtIssCtsP.Text = Format(CSng(txtIssCtsP.Text), "#0.0##")
                        End If
                        txtIssCtsC.Text = 0

                        txtIssP.Enabled = False
                        txtIssC.Enabled = False
                        txtIssCtsP.Enabled = False
                        txtIssCtsC.Enabled = False

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

                Section = Rs.Fields("seccount").Value

                ICNo2 = Rs.Fields("EmpIss").Value
                txtEmp.Text = Rs.Fields("EmpIss").Value

                txtIssP.Text = Rs.Fields("IssPcsP").Value
                txtIssC.Text = Rs.Fields("IssPcsC").Value
                txtIssCtsP.Text = Format(Rs.Fields("IssCtsP").Value, "#0.000")
                txtIssCtsC.Text = Format(Rs.Fields("IssCtsC").Value, "#0.000")
                txtIssP.Enabled = False
                txtIssC.Enabled = False
                txtIssCtsP.Enabled = False
                txtIssCtsC.Enabled = False
            End If

        Else
            'Section Issue/Return entries not found. New issue
            frmnew = True
            issued = False
            PictureBox2.Visible = True
            Rs.Close()
            Section = 0
            cmbSection.SelectedIndex = Section
            txtIssP.Text = rs2.Fields("PktPcs").Value
            txtIssC.Text = "0"
            txtIssCtsP.Text = Format(Caretspkt, "#0.000")
            txtIssCtsC.Text = "0"
            cmdEmp.Focus()  ' get ready to scan IC NO
        End If
        rs2.Close()

        Exit Sub
GoOut:

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

        If issued = True Then
            If Len(txtRetDate.Text) < 2 Then Exit Sub

            stiss = CInt(txtIssC.Text) + CInt(txtIssP.Text)
            stret = CInt(txtTotBag.Text) + CInt(txtTotTap.Text) + CInt(txtRetP.Text) + CInt(txtRetC.Text) + CInt(txtRej.Text) - CInt(txtExt.Text) + CInt(txtLost.Text) + CInt(txtBro.Text)

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

            ciss = CSng(txtIssCtsP.Text) + CSng(txtIssCtsC.Text)
            cret = CSng(txtTotCts.Text) + CSng(txtRetCtsP.Text) + CSng(txtRetCtsC.Text)

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

            End If
        Else
            'rsComSql_2 = New ADODB.Recordset
            'rsComSql_2.Open("SELECT EmpNo FROM tblPRReturns WHERE EmpNo = '" & txtEmp.Text & "' AND LostPcs > 0 AND Active = 1", AdoCN, 1, 1)
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
                rsComSql_1.Open("SELECT FullEmpNo, CATEGORY, Pay FROM VW_EMP_MASTER_SMALL2 WHERE (CATEGORY LIKE 'DIRECT%' OR CATEGORY LIKE 'TEMP%') AND (Pay = 1) AND (FullEmpNo = '" & txtEmp.Text & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT TOP (100) PERCENT DATEDIFF(d, dbo.tblPRIssues.IssDate, GETDATE()) AS Days " & _
                                    "FROM dbo.tblPRIssues INNER JOIN dbo.tblParcel ON dbo.tblPRIssues.ParNo = dbo.tblParcel.GrpParNo LEFT OUTER JOIN " & _
                                        "dbo.tblPRReturns ON dbo.tblPRIssues.ParNo = dbo.tblPRReturns.ParNo AND dbo.tblPRIssues.PktNo = dbo.tblPRReturns.PktNo AND dbo.tblPRIssues.Sec = dbo.tblPRReturns.Sec " & _
                                    "WHERE (dbo.tblParcel.Complete = 0) AND (dbo.tblParcel.Depart = 'Princess') AND (dbo.tblPRReturns.PktNo IS NULL) AND (DATEDIFF(d, dbo.tblPRIssues.IssDate, GETDATE()) > '" & intDelayDays & "') AND (dbo.tblPRIssues.EmpNo = '" & txtEmp.Text & "')", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        MsgBox("Have a Delayed Packet to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql_2 = Nothing

                    If intCheckPastIssues = 1 Then
                        dtpToday = GetToday()
                        rsComSql_2 = New ADODB.Recordset
                        rsComSql_2.Open("SELECT TOP (100) PERCENT dbo.tblPRIssues.IssDate " & _
                                        "FROM dbo.tblPRIssues INNER JOIN dbo.tblParcel ON dbo.tblPRIssues.ParNo = dbo.tblParcel.GrpParNo LEFT OUTER JOIN " & _
                                            "dbo.tblPRReturns ON dbo.tblPRIssues.ParNo = dbo.tblPRReturns.ParNo AND dbo.tblPRIssues.PktNo = dbo.tblPRReturns.PktNo AND dbo.tblPRIssues.Sec = dbo.tblPRReturns.Sec " & _
                                        "WHERE (dbo.tblPRReturns.PktNo IS NULL) AND (dbo.tblParcel.Complete = 0) AND (dbo.tblParcel.Depart = 'Princess') AND (dbo.tblPRIssues.EmpNo = '" & txtEmp.Text & "') AND (dbo.tblPRIssues.IssDate < '" & Format(dtpToday, "MM/dd/yyyy") & "')", AdoCN, 1, 1)
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
            If Section = 0 Then             'if model and issued pcs/cts =0 then dont accept
                If Not (CSng(txtIssCtsP.Text) + CSng(txtIssCtsC.Text)) > 0 Then dataok = False
                If Not (CInt(txtIssC.Text) + CInt(txtIssP.Text)) > 0 Then dataok = False
                '**********************
            End If
        End If
        If dataok Then DataSave() 'if data is ok, save the record
        cmdParPkt.Focus()
    End Sub

    Private Sub DataSave()
        Dim intActive As Integer

        intActive = 0
        dtpToday = GetToday()
        If issued = False Then
            'Issue packet
            mStrSQL = "INSERT INTO tblPRIssues(ParNo,PktNo,Flow,EmpNo,IssPcsP,IssPcsC,IssCtsP,IssCtsC,IssDate,IssTime,Sec,SecCount,DoneBy) " & _
                      "VALUES('" & ParcelNo & "','" & PacketNo & "','" & mFlow & "','" & ICNo & "'," & CInt(txtIssP.Text) & "," & _
                        "" & CInt(txtIssC.Text) & "," & CSng(txtIssCtsP.Text) & "," & CSng(txtIssCtsC.Text) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "'," & _
                        "" & cmbSection.SelectedIndex + 1 & "," & Section + 1 & ",'" & PBUser_EmpNo & "')"

            AdoCN.Execute(mStrSQL)
        Else
            'Return Packet
            If CInt(txtRej.Text) > 0 Then
                If cmbRejReason.Text = "" Then
                    MsgBox("Please enter the Reject Reason", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            If CDbl(txtLost.Text) > 0 Then
                intActive = 1
            End If

            mStrSQL = "INSERT INTO tblPRreturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsP,RetPcsC,RetCtsP,RetCtsC,RejPcs,RejCts,LostPcs,PCUPcs,PCUCts,PCUPCts,LostCts,BroPcs,RepPcs, " & _
                        "NopayPcs,RetDate,RetTime,ExtPcs,Status,RejReason,DoneBy,Active) " & _
                      "VALUES ('" & ParcelNo & "','" & PacketNo & "','" & mFlow & "' ," & Section & "," & (cmbSection.SelectedIndex) + 1 & ",'" & Mid(Trim(ICNo), 1, 6) & "' ," & CInt(txtRetP.Text) & "," & _
                      "" & CInt(txtRetC.Text) & "," & CSng(txtRetCtsP.Text) & "," & CSng(txtRetCtsC.Text) & "," & CInt(txtRej.Text) & "," & CSng(txtRejCts.Text) & "," & CInt(txtLost.Text) & "," & CInt(txtPCUPcs.Text) & "," & CSng(txtPCUCts.Text) & "," & CSng(txtPRetCts.Text) & "," & _
                      "" & CSng(txtLostCts.Text) & "," & CInt(txtBro.Text) & "," & CInt(txtRep.Text) & "," & CInt(txtNoPay.Text) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "'," & CInt(txtExt.Text) & ",0,'" & cmbRejReason.Text & "','" & PBUser_EmpNo & "','" & intActive & "')"

            AdoCN.Execute(mStrSQL)


        End If
        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        RecSave()
    End Sub

    Private Sub txtRetP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRetP.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtRetCtsP.Focus()
        End If
    End Sub

    Private Sub txtRetCtsP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRetCtsP.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRetCtsP.Text)
        If Asc(e.KeyChar) = 13 Then
            Fill_Data()
        End If   
    End Sub

    Private Sub Fill_Data()
        Dim Yld As Single
        Dim wst As Single

        txtRej.Focus()
        txtRej.Text = "0"
        txtRejCts.Text = "0"
        txtLost.Text = "0"
        txtLostCts.Text = "0"
        txtBro.Text = "0"
        txtExt.Text = "0"
        txtRep.Text = "0"
        txtNoPay.Text = "0"

        txtRetC.Text = "0"
        txtRetCtsC.Text = "0"

        If Not IsNumeric(txtRetCtsP.Text) Then
            txtRetCtsP.Text = "0"
            txtRej.Focus()
        Else
            txtYield1.Text = Format(((CDbl(txtRetCtsP.Text) + CDbl(txtRetCtsC.Text)) / Caretspkt) * 100, "#0.00")
            Yld = txtYield1.Text
            wst = (CSng(txtIssCtsP.Text) + CSng(txtIssCtsC.Text)) - (CSng(txtRetCtsP.Text) + CSng(txtRetCtsC.Text))
            txtYield2.Text = Format(((wst) / Caretspkt) * 100, "#0.00")
            txtRej.Focus()
        End If
    End Sub

    Private Sub cmdByPass_Click(sender As Object, e As EventArgs) Handles cmdByPass.Click
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse  = MsgBoxResult.Yes Then
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
        Dim dblRetPCsB As Double
        Dim dblRetCts As Double

        If txtSection.Text <> "" Then
            If txtParPkt.Text = "" Then MsgBox("Please enter the Parcel No/Packet No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If txtEmp.Text = "" Then MsgBox("Please enter the Employee No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

            Rs1 = New ADODB.Recordset
            Rs1.Open("SELECT * FROM tblPRPacket WHERE ParNo = '" & ParcelNo & "' And PktNo = '" & PacketNo & "'", AdoCN, 1, 1)
            If Rs1.RecordCount Then
                dblIssPcs = Rs1.Fields("PktPcs").Value
                dblIssCts = Rs1.Fields("PktCts").Value
                strFlow = Rs1.Fields("PktFlow").Value
                dblIssCts = Math.Round(dblIssCts, 3)

                rs2 = New ADODB.Recordset
                rs2.Open("SELECT * FROM tblPRIssues WHERE ParNo = '" & ParcelNo & "' And PktNo = '" & PacketNo & "' ORDER BY Sec DESC", AdoCN, 1, 1)
                If rs2.RecordCount = 0 Then
                    intSec = 1
                    For intSecCount = 1 To CInt(txtSection.Text)
                        rs3 = New ADODB.Recordset
                        rs3.Open("SELECT * FROM tblPRFlow WHERE Flow = '" & strFlow & "'", AdoCN, 1, 1)
                        If rs3.RecordCount Then
                            intSec = rs3.Fields("Flsec" & intSecCount).Value
                        Else
                            intSec = intSecCount
                        End If
                        rs3 = Nothing
                        If intSec > CInt(txtSection.Text) Then Exit For
                        If intSec = 0 Then Exit For

                        'Issues
                        AdoCN.Execute("INSERT INTO tblPRIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsC,IssPcsP,IssCtsC,IssCtsP,IssDate,IssTime,DoneBy) " & _
                                      "VALUES('" & ParcelNo & "','" & PacketNo & "','" & intSec & "','" & strFlow & "','" & intSecCount & "','" & txtEmp.Text & "',0,'" & dblIssPcs & "',0,'" & dblIssCts & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & PBUser_EmpNo & "')")

                        'Returns
                        AdoCN.Execute("INSERT INTO tblPRReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsC,RetPcsP,RetCtsC,RetCtsP,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,PCUCts,PCUPcs,PCUPCts,Gra_Trf,RejReason,BLostPcs,BLostCts,DoneBy) " & _
                                      "VALUES('" & ParcelNo & "','" & PacketNo & "','" & strFlow & "','" & intSecCount & "','" & intSec & "','" & txtEmp.Text & "',0,'" & dblIssPcs & "',0,'" & dblIssCts & "',0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0,0,0,0,0,'',0,0,'" & PBUser_EmpNo & "')")
                    Next
                    MsgBox("Successfully By Passed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Else
                    rs4 = New ADODB.Recordset
                    rs4.Open("SELECT * FROM VW_PRRealReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & rs2.Fields("Sec").Value & "", AdoCN, 1, 1)
                    If rs4.RecordCount Then
                        If rs2.Fields("IssPcsC").Value + rs2.Fields("IssPcsP").Value = rs4.Fields("SumOfRetPcst").Value + rs4.Fields("SumOfRetPcsB").Value + rs4.Fields("SumOfRej").Value + rs4.Fields("SumOfLost").Value - rs4.Fields("SumOfExt").Value Then
                            dblRetPCsT = rs4.Fields("SumOfRetPcsT").Value
                            dblRetPCsB = rs4.Fields("SumOfRetPcsB").Value - rs4.Fields("PCUPCs").Value
                            dblRetCts = rs4.Fields("SumOfRetCts").Value - rs4.Fields("PCUCts").Value
                            dblRetCts = Math.Round(dblRetCts, 3)
                            For intSecCount = rs2.Fields("Seccount").Value + 1 To CInt(txtSection.Text)
                                rs3 = New ADODB.Recordset
                                rs3.Open("SELECT * FROM tblPRFlow WHERE Flow = '" & strFlow & "'", AdoCN, 1, 1)
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
                                AdoCN.Execute("INSERT INTO tblPRIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsC,IssPcsP,IssCtsC,IssCtsP,IssDate,IssTime,DoneBy) " & _
                                              "VALUES('" & ParcelNo & "','" & PacketNo & "','" & intSec & "','" & strFlow & "','" & intSecCount & "','" & txtEmp.Text & "','" & dblRetPCsT & "','" & dblRetPCsB & "',0,'" & dblRetCts & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & PBUser_EmpNo & "')")

                                'Returns
                                AdoCN.Execute("INSERT INTO tblPRReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsC,RetPcsP,RetCtsC,RetCtsP,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,PCUCts,PCUPcs,PCUPCts,Gra_Trf,RejReason,BLostPcs,BLostCts,DoneBy) " & _
                                              "VALUES('" & ParcelNo & "','" & PacketNo & "','" & strFlow & "','" & intSecCount & "','" & intSec & "','" & txtEmp.Text & "','" & dblRetPCsT & "','" & dblRetPCsB & "',0,'" & dblRetCts & "',0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0,0,0,0,0,'',0,0,'" & PBUser_EmpNo & "')")
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

    Private Sub cmdGetDetails_Click(sender As Object, e As EventArgs) Handles cmdGetDetails.Click
        txtRetP.Text = txtIssP.Text
        txtRetC.Text = txtIssC.Text
        txtRetCtsP.Text = txtIssCtsP.Text
        txtRetCtsC.Text = txtIssCtsC.Text

        ICNo = Trim(txtEmp.Text)
        txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
        txtRetTime.Text = Format(Date.Now, "HH:mm")

        Fill_Data()
    End Sub

    Private Sub txtPRetCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPRetCts.KeyPress
        Dim Yld As Single

        e.Handled = NumericOnly(Asc(e.KeyChar), txtPRetCts.Text)
        If Asc(e.KeyChar) = 13 Then
            Yld = Math.Round(((CDbl(txtRetCtsP.Text) + CDbl(txtRetCtsC.Text)) / Caretspkt) * 100, 2)
            txtPCUCts.Text = Math.Round((100 / Yld) * CDbl(txtPRetCts.Text), 3)
        End If
    End Sub
End Class