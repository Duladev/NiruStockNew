
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RndSection
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
    Dim mRghcts As Single
    Dim PCsPkt As Integer
    Dim AvgCtspkt As Double
    Dim ParcelNo As String
    Dim PacketNo As String
    Dim FirstInput As Date

    Private Sub Load_Section()
        Dim recsection As ADODB.Recordset

        cmbSection.Items.Clear()
        recsection = New ADODB.Recordset
        recsection.Open("SELECT * FROM tblRndsections ORDER BY SecCode", AdoCN, 1, 1)
        If recsection.RecordCount Then
            recsection.MoveFirst()
            Do
                If PBDesignation = "Big Stone" Then
                    cmbSection.Items.Add(recsection.Fields("SecName2").Value)
                Else
                    cmbSection.Items.Add(recsection.Fields("SecName").Value)
                End If
                recsection.MoveNext()
            Loop Until recsection.EOF
        End If
        cmbSection.SelectedIndex = 0
        Section = 1

    End Sub

    Private Sub frm_RndSection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Section()
        Load_RejReasons()
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

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Datavalid = False
        Parcel = True
        Instring = UCase(InputBox("Enter Par/Pkt Number"))
        ParcelLen = Len(Instring)
        If ParcelLen = 11 Then
            Datavalid = True

            ParcelNo = Mid(Instring, 1, ParcelLen - 3)
            PacketNo = strRight(Instring, 3)
        ElseIf ParcelLen = 12 Then
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
            cmdParPkt.Focus()
        End If
    End Sub

    Private Sub cmdEmp_Click(sender As Object, e As EventArgs) Handles cmdEmp.Click
        Datavalid = False
        Parcel = False
        Instring = UCase(InputBox("Enter Emp No"))
        ParcelLen = Len(Instring)

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
                If MsgBox("IC Numbers do not match. Proceed Anyway?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes Then
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

        'pnlEmp.Visible = True
        'txtEmp2.Text = ""
        'txtEmp2.Focus()
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
        txtGroup.Text = ""
        txtRej.Text = ""
        txtBro.Text = ""
        txtLost.Text = ""
        txtExt.Text = ""
        txtRep.Text = ""
        txtNoPay.Text = ""
        txtMacPcs.Text = "0"
        txtMacRghCts.Text = "0"
        txtMacCts.Text = "0"
        txtGles.Text = "0"
        frmnew = True
        txtIssDate.Text = ""
        txtIssTime.Text = ""
        txtRetDate.Text = Format(Date.Now, "yyyy/MM/dd")
        txtRetTime.Text = Format(Date.Now, "HH:mm:ss")
        txtRejCts.Text = ""
        txtLostCts.Text = ""
        cmbRejReason.Text = ""
        flxDetails.Rows.Clear()
        txtTotBag.Text = "0"
        txtTotTap.Text = "0"
        txtTotCts.Text = "0.000"
        PictureBox1.Visible = False
        PictureBox2.Visible = False
        txtRejRgh.Text = ""
        txtLstRgh.Text = ""
        cmbRejReason.Visible = False
        pnlEmp.Visible = False
        cmdParPkt.Focus()
    End Sub

    Private Sub ShowDetails()
        Dim mfldname As String
        Dim Rs As ADODB.Recordset
        Dim rs2 As ADODB.Recordset
        Dim rs3 As ADODB.Recordset
        Dim rs4 As ADODB.Recordset
        Dim rs5 As ADODB.Recordset

        Dim mIssPcs, mRetPcs, mFlowCount As Long

        cmdEmp.Focus()

        mStrSQL = "SELECT * FROM tblRndPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND DelDate IS NOT NULL AND AccDate IS NOT NULL"
        rs2 = New ADODB.Recordset
        rs2.Open(mStrSQL, AdoCN, 1, 1)
        If rs2.RecordCount > 0 Then
            Caretspkt = rs2.Fields("PktCts").Value
            mRghcts = rs2.Fields("PktOrgCts").Value
            PCsPkt = rs2.Fields("PktPcs").Value
            mFlow = rs2.Fields("PktFlow").Value
            txtGroup.Text = rs2.Fields("Grp").Value
            If (rs2.Fields("PktPcs").Value) <> 0 Then
                AvgCtspkt = (mRghcts / PCsPkt)
                txtRejRgh.Text = Math.Round(AvgCtspkt, 3)
                txtLstRgh.Text = Math.Round(AvgCtspkt, 3)
            End If

            If Trim(rs2.Fields("Grp").Value) = "" Then
                MsgBox("Invalid Group", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Else
            GoTo GoOut
        End If

        'rsChk25 = New ADODB.Recordset
        'rsChk25.Open("SELECT ParNo FROM tblRndReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = 25", AdoCN, 1, 1)
        'If rsChk25.RecordCount Then
        '    PictureBox1.Visible = False
        '    PictureBox2.Visible = False

        '    MsgBox("Completed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '    ClearFields()
        '    Exit Sub
        'End If
        'rsChk25 = Nothing

        'To find out the parcel FLOW count
        rs5 = New ADODB.Recordset
        rs5.Open("SELECT * FROM tblRndFlow WHERE Flow = '" & mFlow & "'", AdoCN, 1, 1)
        mFlowCount = rs5.Fields("FlowSections").Value
        'rs5 = Nothing

        'Find out last issued or returned section
        Rs = New ADODB.Recordset
        Rs.Open("SELECT TOP 1 tblRndissues.Sec, tblRndissues.SecCount, tblRndissues.ParNo, tblRndissues.PktNo, tblRndReturns.RetCts, tblRndissues.IssDate, tblRndissues.IssTime, tblRndReturns.RetDate, tblRndReturns.RetTime, " & _
                    "tblRndissues.EmpNo AS EmpIss, tblRndReturns.EmpNo, tblRndissues.IssPcsT, tblRndissues.IssPcsB, tblRndissues.IssCts, tblRndReturns.RetPcsT, tblRndReturns.RetPcsB, tblRndReturns.RejPcs , tblRndReturns.BroPcs, " & _
                    "tblRndReturns.LostPcs, tblRndReturns.ExtPcs, tblRndReturns.NopayPcs, tblRndReturns.RepPcs " & _
                "FROM tblRndissues LEFT OUTER JOIN tblRndReturns ON tblRndissues.Sec = tblRndReturns.Sec AND tblRndissues.ParNo = tblRndReturns.ParNo AND tblRndissues.PktNo = tblRndReturns.PktNo " & _
                "WHERE (tblRndissues.ParNo = '" & ParcelNo & "') AND (tblRndissues.PktNo = '" & PacketNo & "') " & _
                "ORDER BY tblRndissues.Seccount DESC", AdoCN, 1, 1)


        If Rs.RecordCount > 0 Then
            'To find out last Issued/Returned Section

            chkByPass.Checked = False
            cmdByPass.Enabled = False
            txtSection.ReadOnly = False
            txtSection.Text = ""

            frmnew = False

            If Not IsDBNull(Rs.Fields("RetCts").Value) Then
                'To Find everything has returned
                Section = Rs.Fields("Sec").Value

                'Get section Return entered last
                rs4 = New ADODB.Recordset

                'mStrSQL = "SELECT TOP 1 tblRndissues.Sec, tblRndissues.ParNo, tblRndissues.PktNo, SUM(tblRndReturns.RetCts) AS SumOfRetCts, " & _
                '                "tblRndissues.IssPcsT, tblRndissues.IssPcsB, tblRndissues.IssCts,SUM(tblRndReturns.RetPcsT) AS SumOfRetPcsT, " & _
                '                "SUM(tblRndReturns.RetPcsB) AS SumOfRetPcsB, SUM(tblRndReturns.RejPcs) AS SumOfRej,SUM(tblRndReturns.BroPcs) AS SumOfBro, " & _
                '                "SUM(tblRndReturns.LostPcs) AS SumOfLost, SUM(tblRndReturns.ExtPcs) AS SumOfExt, SUM(tblRndReturns.NopayPcs)AS SumOfNopay, " & _
                '                "SUM(tblRndReturns.RepPcs) AS SumOfRepair, SUM(tblRndReturns.LostPcs) AS SumOfLost, SUM(tblRndReturns.MacPcs) AS SumOfMac " & _
                '            "FROM tblRndissues INNER JOIN tblRndReturns ON tblRndissues.PktNo = tblRndReturns.PktNo AND tblRndissues.ParNo = tblRndReturns.ParNo AND tblRndissues.Sec = tblRndReturns.Sec " & _
                '            "WHERE (tblRndissues.Sec = '" & Section & "') AND (tblRndissues.ParNo = '" & ParcelNo & "') AND (tblRndissues.PktNo = '" & PacketNo & "') " & _
                '            "GROUP BY tblRndissues.Sec, tblRndissues.ParNo, tblRndissues.PktNo, tblRndissues.IssPcsT, tblRndissues.IssPcsB, tblRndissues.IssCts " & _
                '            "ORDER BY tblRndissues.Sec DESC"

                mStrSQL = "SELECT TOP (1) SUM(RetCts) AS SumOfRetCts, SUM(RetPcsT) AS SumOfRetPcsT, SUM(RetPcsB) AS SumOfRetPcsB, SUM(RejPcs) AS SumOfRej, SUM(BroPcs) AS SumOfBro, " & _
                                "SUM(LostPcs) AS SumOfLost, SUM(ExtPcs) AS SumOfExt, SUM(NopayPcs) AS SumOfNopay, SUM(RepPcs) AS SumOfRepair, SUM(LostPcs) AS Expr1, SUM(MacPcs) AS SumOfMac " & _
                          "FROM dbo.tblRndReturns " & _
                          "WHERE (Sec = '" & Section & "') AND (ParNo = '" & ParcelNo & "') AND (PktNo = '" & PacketNo & "')"

                rs4.Open(mStrSQL, AdoCN, 1, 1)

                'mIssPcs = rs4.Fields("IssPcsT").Value + rs4.Fields("IssPcsB").Value
                mIssPcs = Rs.Fields("IssPcsT").Value + Rs.Fields("IssPcsB").Value
                mRetPcs = rs4.Fields("SumOfRetPcsT").Value + rs4.Fields("SumOfRetPcsB").Value + rs4.Fields("SumOfRej").Value + rs4.Fields("SumOfLost").Value + rs4.Fields("SumOfMac").Value - rs4.Fields("SumOfExt").Value
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
                    rs3.Open("SELECT * FROM VWRndTotalRetEmp WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' And Sec = '" & Section & "' ORDER BY Sec", AdoCN, 1, 1)
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
                    rs3.Open("SELECT SUM(RetPcsT) AS TotTap, SUM(RetPcsB) AS TotBag, ROUND(SUM(RetCts), 3) AS RetCts FROM dbo.tblRndReturns WHERE ParNo = '" & ParcelNo & "' And PktNo = '" & PacketNo & "' And Sec = '" & Section & "'", AdoCN, 1, 1)
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
                    If Rs.Fields("SecCount").Value < mFlowCount Then     'To check relevant flow is completed.

                        issued = False
                        PictureBox1.Visible = False
                        PictureBox2.Visible = True
                        mfldname = "Flsec" & Rs.Fields("seccount").Value + 1
                        Section = rs5.Fields(mfldname).Value
                        cmbSection.SelectedIndex = Section - 1

                        Section = Rs.Fields("sec").Value

                        rs4 = New ADODB.Recordset
                        Dim sqlStringm As String
                        '============================ Done ====================================
                        'sqlStringm = "SELECT TOP 1 tblRndissues.Sec, tblRndissues.ParNo, tblRndissues.PktNo, SUM(tblRndReturns.RetCts) AS SumOfRetCts,tblRndissues.EmpNo, tblRndissues.IssPcsT, tblRndissues.IssPcsB, " & _
                        '                "tblRndissues.IssCts,SUM(tblRndReturns.RetPcsT) AS SumOfRetPcsT, SUM(tblRndReturns.RetPcsB) AS SumOfRetPcsB, SUM(tblRndReturns.RejPcs) AS SumOfRej,SUM(tblRndReturns.BroPcs) AS SumOfBro, " & _
                        '                "SUM(tblRndReturns.LostPcs) AS SumOfLost, SUM(tblRndReturns.ExtPcs) AS SumOfExt, SUM(tblRndReturns.NopayPcs)AS SumOfNopay, SUM(tblRndReturns.RepPcs) AS SumOfRepair " & _
                        '             "FROM tblRndissues INNER JOIN tblRndReturns ON tblRndissues.PktNo = tblRndReturns.PktNo AND tblRndissues.ParNo = tblRndReturns.ParNo AND tblRndissues.Sec = tblRndReturns.Sec " & _
                        '             "WHERE (tblRndissues.Sec = '" & Section & "') AND (tblRndissues.ParNo = '" & ParcelNo & "') AND (tblRndissues.PktNo = '" & PacketNo & "') " & _
                        '             "GROUP BY tblRndissues.Sec, tblRndissues.ParNo, tblRndissues.PktNo,tblRndissues.EmpNo, tblRndissues.IssPcsT, tblRndissues.IssPcsB, tblRndissues.IssCts " & _
                        '             "ORDER BY tblRndissues.Sec DESC"

                        sqlStringm = "SELECT TOP (1) SUM(RetCts) AS SumOfRetCts, SUM(RetPcsT) AS SumOfRetPcsT, SUM(RetPcsB) AS SumOfRetPcsB " & _
                                      "FROM dbo.tblRndReturns " & _
                                      "WHERE (Sec = '" & Section & "') AND (ParNo = '" & ParcelNo & "') AND (PktNo = '" & PacketNo & "')"

                        rs4.Open(sqlStringm, AdoCN, 1, 1)
                        '================================================================

                        txtIssTap.Text = rs4.Fields("SumOfRetPcsT").Value
                        txtIssBag.Text = rs4.Fields("SumOfRetPcsB").Value
                        txtIssCts.Text = Format(rs4.Fields("SumOfRetCts").Value, "#0.0##")


                        txtIssTap.Enabled = False
                        txtIssBag.Enabled = False
                        txtIssCts.Enabled = False

                        Section = Rs.Fields("seccount").Value

                        Rs.Close()

                    Else
                        'Section 25 completed. Packet finished
                        PictureBox1.Visible = False
                        PictureBox2.Visible = False

                        MsgBox("Completed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Temp)
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
                txtIssTap.Text = Rs.Fields("IssPcsT").Value
                txtIssBag.Text = Rs.Fields("IssPcsB").Value
                txtIssCts.Text = Format(CDbl(Rs.Fields("IssCts").Value), "#0.0##")
                txtIssTap.Enabled = False
                txtIssBag.Enabled = False
                txtIssCts.Enabled = False
            End If

        Else                            'Section Issue/Return entries not found. New issue
            frmnew = True
            issued = False
            PictureBox2.Visible = True
            Rs.Close()
            Section = 0
            cmbSection.SelectedIndex = rs5.Fields("Flsec1").Value - 1
            txtIssBag.Text = "0"
            txtIssTap.Text = rs2.Fields("PktPcs").Value
            txtIssCts.Text = Format(Caretspkt, "#0.0##")
            cmdEmp.Focus()  ' get ready to scan IC NO
        End If

        rs2.Close()
        Exit Sub
GoOut:
        txtParPkt.Text = ""
        cmdParPkt.Focus()
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

    Private Sub RecSave()
        Dim dataok As Boolean
        Dim stiss, stret As Integer
        Dim ciss, cret As Single

        dataok = True

        If Trim(ICNo) = "" Then
            MsgBox("Invalid Employee No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If issued Then
            If txtEmp.Text = "" Then
                MsgBox("Invalid Employee No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Len(txtRetDate.Text) < 2 Then Exit Sub

            If CInt(txtRetTap.Text) + CInt(txtRetBag.Text) + CInt(txtRej.Text) - CInt(txtExt.Text) + CInt(txtLost.Text) + CInt(txtMacPcs.Text) <= 0 Then
                strmsg = "Invalid Pcs"
                MsgBox(strmsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
                If dataok = False Then Exit Sub
            End If

            If CSng(txtRetCts.Text) + CSng(txtLostCts.Text) + CSng(txtRejCts.Text) + CSng(txtMacCts.Text) <= 0 Then
                strmsg = "Invalid Cts"
                MsgBox(strmsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
                If dataok = False Then Exit Sub
            End If

            stiss = CInt(txtIssTap.Text) + CInt(txtIssBag.Text)   'issued pcs
            stret = CInt(txtTotBag.Text) + CInt(txtTotTap.Text) + CInt(txtRetTap.Text) + CInt(txtRetBag.Text) + CInt(txtRej.Text) - CInt(txtExt.Text) + CInt(txtLost.Text) + CInt(txtMacPcs.Text) 'Ret pcs
            If stiss < stret Then
                strmsg = "Stones issued " & stiss & "   Stones returned " & stret
                MsgBox(strmsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
                If dataok = False Then Exit Sub
            End If
            ciss = CSng(txtIssCts.Text)    'cts issued
            cret = CSng(txtTotCts.Text) + CSng(txtRetCts.Text) + CSng(txtLostCts.Text) + CSng(txtRejCts.Text)    'cts ret
            If ciss < cret Then
                strmsg = "Carats issued " & Format(ciss, "##.###") & "   Carats returned " & Format(cret, "##.###")
                MsgBox(strmsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
                If dataok = False Then Exit Sub
            End If

            If CInt(txtRetTap.Text) + CInt(txtRetBag.Text) > 0 Then
                If CSng(txtRetCts.Text) <= 0 Then
                    MsgBox("Invalid Return Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
        Else
            If txtEmp.Text = "" Then
                MsgBox("Invalid Employee No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            rsComSql_2 = New ADODB.Recordset
            rsComSql_2.Open("SELECT EmpNo FROM tblRndReturns WHERE EmpNo = '" & txtEmp.Text & "' AND LostPcs > 0 AND Active = 1", AdoCN, 1, 1)
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
                    rsComSql_2.Open("SELECT TOP (100) PERCENT DATEDIFF(d, dbo.tblRndIssues.IssDate, GETDATE()) AS Days " & _
                                    "FROM dbo.tblRndIssues INNER JOIN dbo.tblParcel ON dbo.tblRndIssues.ParNo = dbo.tblParcel.GrpParNo LEFT OUTER JOIN " & _
                                        "dbo.tblRndReturns ON dbo.tblRndIssues.ParNo = dbo.tblRndReturns.ParNo AND dbo.tblRndIssues.PktNo = dbo.tblRndReturns.PktNo AND dbo.tblRndIssues.Sec = dbo.tblRndReturns.Sec " & _
                                    "WHERE (dbo.tblParcel.Complete = 0) AND (dbo.tblParcel.Depart = 'Rounds') AND (dbo.tblRndReturns.PktNo IS NULL) AND (DATEDIFF(d, dbo.tblRndIssues.IssDate, GETDATE()) > '" & intDelayDays & "') AND (dbo.tblRndIssues.EmpNo = '" & txtEmp.Text & "')", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        MsgBox("Have a Delayed Packet to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql_2 = Nothing

                    If intCheckPastIssues = 1 Then
                        dtpToday = GetToday()
                        rsComSql_2 = New ADODB.Recordset
                        rsComSql_2.Open("SELECT TOP (100) PERCENT dbo.tblRndIssues.IssDate " & _
                                        "FROM dbo.tblRndIssues INNER JOIN dbo.tblParcel ON dbo.tblRndIssues.ParNo = dbo.tblParcel.GrpParNo LEFT OUTER JOIN " & _
                                            "dbo.tblRndReturns ON dbo.tblRndIssues.ParNo = dbo.tblRndReturns.ParNo AND dbo.tblRndIssues.PktNo = dbo.tblRndReturns.PktNo AND dbo.tblRndIssues.Sec = dbo.tblRndReturns.Sec " & _
                                        "WHERE (dbo.tblRndReturns.PktNo IS NULL) AND (dbo.tblParcel.Complete = 0) AND (dbo.tblParcel.Depart = 'Rounds') AND (dbo.tblRndIssues.EmpNo = '" & txtEmp.Text & "') AND (dbo.tblRndIssues.IssDate < '" & Format(dtpToday, "MM/dd/yyyy") & "')", AdoCN, 1, 1)
                        If rsComSql_2.RecordCount Then
                            MsgBox("Have a Past Packet to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                        rsComSql_2 = Nothing
                    End If
                    
                End If
                rsComSql_1 = Nothing
            End If

            rsComSql_2 = New ADODB.Recordset
            rsComSql_2.Open("SELECT ParNo FROM tblDCLBlockPar WHERE ParNo = '" & ParcelNo & "' AND Department = 'Rounds'", AdoCN, 1, 1)
            If rsComSql_2.RecordCount Then
                MsgBox("This Parcel is Blocked", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql_2 = Nothing

            If Len(txtIssDate.Text) < 2 Then dataok = False
            If Section = 0 Then             'if model and issued pcs/cts =0 then dont accept
                If Not CSng(txtIssCts.Text) > 0 Then dataok = False
                If Not CInt(txtIssTap.Text) + CInt(txtIssBag.Text) > 0 Then dataok = False
            End If
            If txtMacPcs.Text <> "" Then
                If txtMacPcs.Text > 0 Then
                    txtMacRghCts.Text = Math.Round((AvgCtspkt * txtMacPcs.Text), 3)
                Else
                    txtMacRghCts.Text = "0"
                End If
            End If
        End If

        If dataok = True Then
            DataSave()
        End If
        cmdParPkt.Focus()

    End Sub

    Private Sub DataSave()
        Dim intActive As Integer

        intActive = 0
        dtpToday = GetToday()
        If issued = False Then
            'Issue packet
            AdoCN.Execute("INSERT INTO tblRndIssues(ParNo,PktNo,Flow,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,Sec,SecCount,DoneBy) " & _
                          "VALUES('" & ParcelNo & "','" & PacketNo & "','" & mFlow & "','" & Mid(Trim(ICNo), 1, 6) & "','" & CInt(txtIssTap.Text) & "'," & _
                                 "'" & CInt(txtIssBag.Text) & "','" & CDbl(txtIssCts.Text) & "','" & Format(dtpToday, "MM/dd/yyyy") & "'," & _
                                 "'" & Format(Date.Now, "HH:mm:ss") & "','" & cmbSection.SelectedIndex + 1 & "','" & Section + 1 & "','" & PBUser_EmpNo & "')")
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

            AdoCN.Execute("INSERT INTO tblRndReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts," & _
                            "BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,MacPcs,MacCts,MacRghCts,RejReason,GlsPcs,DoneBy,Active) " & _
                          "VALUES ('" & ParcelNo & "','" & PacketNo & "','" & mFlow & "' ," & Section & "," & cmbSection.SelectedIndex + 1 & ",'" & Mid(Trim(ICNo), 1, 6) & "'" & _
                                "," & CInt(txtRetTap.Text) & "," & CInt(txtRetBag.Text) & "," & CDbl(txtRetCts.Text) & "," & CInt(txtRej.Text) & "," & CDbl(txtRejCts.Text) & "," & CInt(txtLost.Text) & "" & _
                                "," & CDbl(txtLostCts.Text) & "," & CInt(txtBro.Text) & "," & CInt(txtRep.Text) & "," & CInt(txtNoPay.Text) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "'" & _
                                ",'" & Format(Date.Now, "HH:mm:ss") & "'," & CInt(txtExt.Text) & ",0," & CDbl(txtRejRgh.Text) & "," & CDbl(txtLstRgh.Text) & "," & CInt(txtMacPcs.Text) & "," & _
                                "" & CDbl(txtMacCts.Text) & "," & CDbl(txtMacRghCts.Text) & ",'" & cmbRejReason.Text & "'," & CInt(txtGles.Text) & ",'" & PBUser_EmpNo & "','" & intActive & "')")

        End If
        ClearFields()
        Exit Sub

RETRY:
        MsgBox("Please re-enter the packet details")         'If retry failed, enter details again
        ClearFields()
        Exit Sub
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Recsave()
    End Sub

    Private Sub txtRetTap_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRetTap.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtRetTap.Text = "" Then
                txtRetTap.Text = "0"
            End If
            txtRetBag.Text = "0"
            txtRetCts.Focus()
        End If
    End Sub

    Private Sub txtRetBag_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRetBag.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtRetBag.Text = "" Then
                txtRetBag.Text = "0"
            End If
            txtRetCts.Focus()
        End If
    End Sub

    Private Sub txtRej_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRej.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If CInt(txtRej.Text) > 0 Then
                txtRejRgh.Text = Math.Round((AvgCtspkt * CInt(txtRej.Text)), 2)
                cmbRejReason.Visible = True
            Else
                txtRejRgh.Text = "0"
            End If
            txtRejCts.Focus()
        End If
    End Sub

    Private Sub cmdGetDetails_Click(sender As Object, e As EventArgs) Handles cmdGetDetails.Click
        txtRetTap.Text = txtIssTap.Text
        txtRetBag.Text = txtIssBag.Text
        txtRetCts.Text = txtIssCts.Text

        ICNo = Trim(txtEmp.Text)
        txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
        txtRetTime.Text = Format(Date.Now, "HH:mm")

        Fill_Data()
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
            Rs1.Open("SELECT * FROM tblRndPacket WHERE ParNo = '" & ParcelNo & "' And PktNo = '" & PacketNo & "'", AdoCN, 1, 1)
            If Rs1.RecordCount Then
                dblIssPcs = Rs1.Fields("PktPcs").Value
                dblIssCts = Rs1.Fields("PktCts").Value
                strFlow = Rs1.Fields("PktFlow").Value
                dblIssCts = Math.Round(dblIssCts, 3)

                rs2 = New ADODB.Recordset
                rs2.Open("SELECT * FROM tblRndIssues WHERE ParNo = '" & ParcelNo & "' And PktNo = '" & PacketNo & "' ORDER BY Sec DESC", AdoCN, 1, 1)
                If rs2.RecordCount = 0 Then
                    intSec = 1
                    For intSecCount = 1 To CInt(txtSection.Text)
                        rs3 = New ADODB.Recordset
                        rs3.Open("SELECT * FROM tblRndFlow WHERE Flow = '" & strFlow & "'", AdoCN, 1, 1)
                        If rs3.RecordCount Then
                            intSec = rs3.Fields("Flsec" & intSecCount).Value
                        Else
                            intSec = intSecCount
                        End If
                        rs3 = Nothing
                        If intSec > CInt(txtSection.Text) Then Exit For
                        If intSec = 0 Then Exit For
                        'Issues
                        AdoCN.Execute("INSERT INTO tblRndIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                       "VALUES('" & ParcelNo & "','" & PacketNo & "','" & intSec & "','" & strFlow & "','" & intSecCount & "','" & txtEmp.Text & "','" & dblIssPcs & "',0,'" & dblIssCts & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & PBUser_EmpNo & "')")

                        'Returns
                        AdoCN.Execute("INSERT INTO tblRndReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,Gra_Trf,MacPcs,MacCts,DoneBy) " & _
                                       "VALUES('" & ParcelNo & "','" & PacketNo & "','" & strFlow & "','" & intSecCount & "','" & intSec & "','" & txtEmp.Text & "','" & dblIssPcs & "',0,'" & dblIssCts & "',0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,0,0,'" & PBUser_EmpNo & "')")
                    Next
                    MsgBox("Successfully By Passed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Else
                    rs4 = New ADODB.Recordset
                    rs4.Open("SELECT * FROM VW_RndRealReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & rs2.Fields("Sec").Value & "", AdoCN, 1, 1)
                    If rs4.RecordCount Then
                        If rs2.Fields("IssPcsT").Value + rs2.Fields("IssPcsB").Value = rs4.Fields("SumOfRetPcst").Value + rs4.Fields("SumOfRetPcsB").Value + rs4.Fields("SumOfRej").Value + rs4.Fields("SumOfLost").Value - rs4.Fields("SumOfExt").Value Then
                            dblRetPCsT = rs4.Fields("SumOfRetPcst").Value
                            dblRetPcsB = rs4.Fields("SumOfRetPcsB").Value
                            dblRetCts = rs4.Fields("SumOfRetCts").Value
                            dblRetCts = Math.Round(dblRetCts, 3)
                            For intSecCount = rs2.Fields("Seccount").Value + 1 To CInt(txtSection.Text)
                                rs3 = New ADODB.Recordset
                                rs3.Open("SELECT * FROM tblRndFlow WHERE Flow = '" & strFlow & "'", AdoCN, 1, 1)
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
                                AdoCN.Execute("INSERT INTO tblRndIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                              "VALUES('" & ParcelNo & "','" & PacketNo & "','" & intSec & "','" & strFlow & "','" & intSecCount & "','" & txtEmp.Text & "','" & dblRetPCsT & "'," & dblRetPcsB & ",'" & dblRetCts & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & PBUser_EmpNo & "')")

                                'Returns
                                AdoCN.Execute("INSERT INTO tblRndReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,Gra_Trf,MacPcs,MacCts,DoneBy) " & _
                                              "VALUES('" & ParcelNo & "','" & PacketNo & "','" & strFlow & "','" & intSecCount & "','" & intSec & "','" & txtEmp.Text & "','" & dblRetPCsT & "'," & dblRetPcsB & ",'" & dblRetCts & "',0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,0,0,'" & PBUser_EmpNo & "')")
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

    Private Sub txtSection_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSection.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub cmdGetDetails2_Click(sender As Object, e As EventArgs) Handles cmdGetDetails2.Click
        txtRetTap.Text = txtIssTap.Text
        txtRetBag.Text = txtIssBag.Text
        txtRetCts.Text = txtIssCts.Text

        ICNo = Trim(txtEmp.Text)
        txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
        txtRetTime.Text = Format(Date.Now, "HH:mm")

        Fill_Data()
    End Sub

    Private Sub txtEmp2_GotFocus(sender As Object, e As EventArgs) Handles txtEmp2.GotFocus
        FirstInput = Nothing
        txtEmp.Text = ""
    End Sub

    Private Sub txtEmp2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEmp2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If CheckEmployee(Trim(txtEmp2.Text)) = True Then
                Datavalid = True
                ICNo = UCase(Trim(txtEmp2.Text))
            Else
                MsgBox("Invalid IC No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Datavalid = False
                ICNo = ""
                txtEmp2.Focus()
                Exit Sub
            End If
            txtEmp.Text = ICNo
            pnlEmp.Visible = False

            If issued = True Then
                If Trim(ICNo2) <> Trim(ICNo) Then
                    If MsgBox("IC Numbers do not match. Proceed Anyway?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes Then
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

        End If
    End Sub

    Private Sub txtEmp2_KeyUp(sender As Object, e As KeyEventArgs) Handles txtEmp2.KeyUp
        Dim TimeDiff As Integer
        Dim TimeDiff1 As TimeSpan

        If Asc(e.KeyCode) <> 9 And Asc(e.KeyCode) <> 13 Then
            If FirstInput = Nothing Then
                FirstInput = Now()
            Else
                'TimeDiff = DateDiff(DateInterval.Second, FirstInput, Now())
                TimeDiff1 = Now() - FirstInput
                TimeDiff = TimeDiff1.Milliseconds
            End If

            If TimeDiff > 500 Then
                MsgBox("Please use the Barcode scanner", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                txtEmp.Text = ""
                FirstInput = Nothing
            End If

        End If
    End Sub

    Private Sub txtEmp2_LostFocus(sender As Object, e As EventArgs) Handles txtEmp2.LostFocus
        FirstInput = Nothing
    End Sub

    Private Sub cmdEmpCancel_Click(sender As Object, e As EventArgs) Handles cmdEmpCancel.Click
        pnlEmp.Visible = False
    End Sub
End Class