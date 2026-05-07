
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_PCUSection

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

    Private Sub Load_Section()
        Dim recsection As ADODB.Recordset

        cmbSection.Items.Clear()
        recsection = New ADODB.Recordset
        recsection.Open("SELECT * FROM tblSections ORDER BY SecCode", AdoCN, 1, 1)
        If recsection.RecordCount Then
            recsection.MoveFirst()
            Do
                If PBDesignation = "Baguettes" Then
                    cmbSection.Items.Add(recsection.Fields("BagSec").Value)
                ElseIf PBDesignation = "Ashal" Then
                    cmbSection.Items.Add(recsection.Fields("BagSec2").Value)
                ElseIf PBDesignation = "Rounds" Then
                    cmbSection.Items.Add(recsection.Fields("MrqSec").Value)
                ElseIf PBDesignation = "Emerald" Then
                    cmbSection.Items.Add(recsection.Fields("EmSec").Value)
                ElseIf PBDesignation = "RndPolish" Then
                    cmbSection.Items.Add(recsection.Fields("RndPolish").Value)
                ElseIf PBDesignation = "Pear" Then
                    cmbSection.Items.Add(recsection.Fields("PearSec").Value)
                ElseIf PBDesignation = "RndSize" Then
                    cmbSection.Items.Add(recsection.Fields("RndSize").Value)
                ElseIf PBDesignation = "Ashok" Then
                    cmbSection.Items.Add(recsection.Fields("Ashok").Value)
                ElseIf PBDesignation = "Princess" Then
                    cmbSection.Items.Add(recsection.Fields("PR").Value)
                Else
                    cmbSection.Items.Add(recsection.Fields("SecName").Value)
                End If
                recsection.MoveNext()
            Loop Until recsection.EOF
        End If
        cmbSection.SelectedIndex = 0
        Section = 1

    End Sub

    Private Sub Load_SectionFlow(ByVal strFlow As String)
        Dim recsection As ADODB.Recordset

        cmbSection.Items.Clear()
        recsection = New ADODB.Recordset
        recsection.Open("SELECT * FROM tblSections2 WHERE Flow = '" & strFlow & "' ORDER BY SecCode", AdoCN, 1, 1)
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

    Private Sub frm_PCUSection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        ClearFields()
        Load_Section()
        Load_RejReasons()
    End Sub

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Datavalid = False
        Parcel = True
        Instring = UCase(InputBox("Enter Par/Pkt Number"))
        ParcelLen = Len(Instring)
        If ParcelLen = 8 Then
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

    Private Sub ClearFields()
        txtFlow.Text = ""
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
        frmnew = True
        txtIssDate.Text = ""
        txtIssTime.Text = ""
        txtRetDate.Text = Format(Date.Now, "yyyy/MM/dd")
        txtRetTime.Text = Format(Date.Now, "HH:mm:ss")
        txtRejCts.Text = ""
        txtLostCts.Text = ""
        cmbRejReason.Text = ""
        txtYield1.Text = ""
        txtYield2.Text = ""
        flxDetails.Rows.Clear()
        txtTotBag.Text = "0"
        txtTotTap.Text = "0"
        txtTotCts.Text = "0.000"
        PictureBox1.Visible = False
        PictureBox2.Visible = False
        cmdParPkt.Focus()
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
        Dim rs3 As ADODB.Recordset
        Dim rs4 As ADODB.Recordset
        Dim rs5 As ADODB.Recordset
        Dim mIssPcs, mRetPcs, mFlowCount As Long

        cmdEmp.Focus()

        mStrSQL = "SELECT * FROM dbo.tblPacket WHERE (PktOrdNo = '" & ParcelNo & "') AND (PktNo = '" & PacketNo & "')"
        rs2 = New ADODB.Recordset
        rs2.Open(mStrSQL, AdoCN, 1, 1)
        If rs2.RecordCount > 0 Then
            Caretspkt = rs2.Fields("PktCts").Value
            PCsPkt = rs2.Fields("PktPcs").Value
            If (rs2.Fields("PktPcs").Value) <> 0 Then
                AvgCtspkt = Math.Round((Caretspkt / PCsPkt), 3)
            End If
        Else
            GoTo GoOut
        End If

        mStrSQL = "SELECT * FROM tblPacket WHERE PktOrdNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "'"
        Rs1 = New ADODB.Recordset
        Rs1.Open(mStrSQL, AdoCN, 1, 1)
        mFlow = Rs1.Fields("PktFlow").Value
        txtGroup.Text = Rs1.Fields("Grp").Value
        txtFlow.Text = mFlow

        If txtGroup.Text = "" Then
            MsgBox("Invalid Group", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        Load_SectionFlow(mFlow)

        mStrSQL = "SELECT * FROM tblFlow WHERE Flow = '" & mFlow & "'"
        rs5 = New ADODB.Recordset
        rs5.Open(mStrSQL, AdoCN, 1, 1)
        mFlowCount = rs5.Fields("FlowSections").Value

        Rs = New ADODB.Recordset
        mStrSQL = "SELECT TOP (100) PERCENT dbo.tblIssues.Sec, dbo.tblIssues.SecCount, dbo.tblIssues.ParNo, dbo.tblIssues.PktNo, dbo.tblReturns.RetCts, dbo.tblIssues.IssDate, " & _
                    "dbo.tblIssues.IssTime, dbo.tblReturns.RetDate, dbo.tblReturns.RetTime, dbo.tblIssues.EmpNo AS EmpIss, dbo.tblReturns.EmpNo, dbo.tblIssues.IssPcsT,  " & _
                    "dbo.tblIssues.IssPcsB, dbo.tblIssues.IssCts, dbo.tblReturns.RetPcsT, dbo.tblReturns.RetPcsB, dbo.tblReturns.RejPcs, dbo.tblReturns.BroPcs, dbo.tblReturns.LostPcs, " & _
                    "dbo.tblReturns.ExtPcs, dbo.tblReturns.NopayPcs, dbo.tblReturns.RepPcs " & _
               "FROM dbo.tblIssues LEFT OUTER JOIN dbo.tblReturns ON dbo.tblIssues.Sec = dbo.tblReturns.Sec AND dbo.tblIssues.ParNo = dbo.tblReturns.ParNo AND dbo.tblIssues.PktNo = dbo.tblReturns.PktNo " & _
               "WHERE (dbo.tblIssues.ParNo = '" & ParcelNo & "') AND (dbo.tblIssues.PktNo = '" & PacketNo & "') " & _
               "ORDER BY dbo.tblIssues.SecCount DESC"
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        If Rs.RecordCount > 0 Then

            frmnew = False
            If Not IsDBNull(Rs.Fields("RetCts").Value) Then

                Section = Rs.Fields("Sec").Value

                Dim mSecret As String
                rs4 = New ADODB.Recordset

                mStrSQL = "SELECT TOP (100) PERCENT dbo.tblIssues.Sec, dbo.tblIssues.ParNo, dbo.tblIssues.PktNo, SUM(dbo.tblReturns.RetCts) AS SumOfRetCts, dbo.tblIssues.IssPcsT, " & _
                            "dbo.tblIssues.IssPcsB, dbo.tblIssues.IssCts, SUM(dbo.tblReturns.RetPcsT) AS SumOfRetPcsT, SUM(dbo.tblReturns.RetPcsB) AS SumOfRetPcsB,  " & _
                            "SUM(dbo.tblReturns.RejPcs) AS SumOfRej, SUM(dbo.tblReturns.BroPcs) AS SumOfBro, SUM(dbo.tblReturns.LostPcs) AS SumOfLost, SUM(dbo.tblReturns.ExtPcs)  " & _
                            "AS SumOfExt, SUM(dbo.tblReturns.NopayPcs) AS SumOfNopay, SUM(dbo.tblReturns.RepPcs) AS SumOfRepair " & _
                          "FROM dbo.tblIssues INNER JOIN dbo.tblReturns ON dbo.tblIssues.PktNo = dbo.tblReturns.PktNo AND dbo.tblIssues.ParNo = dbo.tblReturns.ParNo AND dbo.tblIssues.Sec = dbo.tblReturns.Sec " & _
                          "GROUP BY dbo.tblIssues.Sec, dbo.tblIssues.ParNo, dbo.tblIssues.PktNo, dbo.tblIssues.IssPcsT, dbo.tblIssues.IssPcsB, dbo.tblIssues.IssCts " & _
                          "HAVING (dbo.tblIssues.Sec = " & Section & ") AND (dbo.tblIssues.ParNo = '" & ParcelNo & "') AND (dbo.tblIssues.PktNo = '" & PacketNo & "') " & _
                          "ORDER BY dbo.tblIssues.Sec DESC"
                rs4.Open(mStrSQL, AdoCN, 1, 1)

                mIssPcs = rs4.Fields("IssPcsT").Value + rs4.Fields("IssPcsB").Value
                mRetPcs = rs4.Fields("SumOfRetPcsT").Value + rs4.Fields("SumOfRetPcsB").Value + rs4.Fields("SumOfRej").Value + rs4.Fields("SumOfLost").Value + rs4.Fields("SumOfBro").Value - rs4.Fields("SumOfExt").Value
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
                    rs3.Open("SELECT * FROM VWTotalRetEmp WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' And Sec = '" & Section & "' ORDER BY Sec", AdoCN, 1, 1)
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
                    rs3.Open("SELECT SUM(RetPcsT) AS TotTap, SUM(RetPcsB) AS TotBag, ROUND(SUM(RetCts), 3) AS RetCts " & _
                             "FROM dbo.tblReturns WHERE ParNo = '" & ParcelNo & "' And PktNo = '" & PacketNo & "' And Sec = '" & Section & "'", AdoCN, 1, 1)
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

                        mStrSQL = "SELECT TOP (100) PERCENT dbo.tblIssues.Sec, dbo.tblIssues.ParNo, dbo.tblIssues.PktNo, SUM(dbo.tblReturns.RetCts) AS SumOfRetCts, dbo.tblIssues.EmpNo, " & _
                                    "dbo.tblIssues.IssPcsT, dbo.tblIssues.IssPcsB, dbo.tblIssues.IssCts, SUM(dbo.tblReturns.RetPcsT) AS SumOfRetPcsT, SUM(dbo.tblReturns.RetPcsB) " & _
                                    "AS SumOfRetPcsB, SUM(dbo.tblReturns.RejPcs) AS SumOfRej, SUM(dbo.tblReturns.BroPcs) AS SumOfBro, SUM(dbo.tblReturns.LostPcs) AS SumOfLost, " & _
                                    "SUM(dbo.tblReturns.ExtPcs) AS SumOfExt, SUM(dbo.tblReturns.NopayPcs) AS SumOfNopay, SUM(dbo.tblReturns.RepPcs) AS SumOfRepair " & _
                                  "FROM dbo.tblIssues INNER JOIN dbo.tblReturns ON dbo.tblIssues.PktNo = dbo.tblReturns.PktNo AND dbo.tblIssues.ParNo = dbo.tblReturns.ParNo AND dbo.tblIssues.Sec = dbo.tblReturns.Sec " & _
                                  "GROUP BY dbo.tblIssues.Sec, dbo.tblIssues.ParNo, dbo.tblIssues.PktNo, dbo.tblIssues.EmpNo, dbo.tblIssues.IssPcsT, dbo.tblIssues.IssPcsB, dbo.tblIssues.IssCts " & _
                                  "HAVING (dbo.tblIssues.Sec = " & Section & ") AND (dbo.tblIssues.ParNo = '" & ParcelNo & "') AND (dbo.tblIssues.PktNo = '" & PacketNo & "') " & _
                                  "ORDER BY dbo.tblIssues.Sec DESC"
                        rs4.Open(mStrSQL, AdoCN, 1, 1)

                        txtIssTap.Text = rs4.Fields("SumOfRetPcsT").Value
                        txtIssBag.Text = rs4.Fields("SumOfRetPcsB").Value
                        txtIssCts.Text = Format(rs4.Fields("SumOfRetCts").Value, "#0.000")

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

        If issued Then
            If Len(txtRetDate.Text) < 2 Then Exit Sub

            stiss = CInt(txtIssTap.Text) + CInt(txtIssBag.Text)
            stret = CInt(txtTotBag.Text) + CInt(txtTotTap.Text) + CInt(txtRetTap.Text) + CInt(txtRetBag.Text) + CInt(txtRej.Text) - CInt(txtExt.Text) + CInt(txtLost.Text) + CInt(txtBro.Text)

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
        Else
            If Len(txtIssDate.Text) < 2 Then dataok = False
            '**********************
            If Section = 0 Then
                If Not CSng(txtIssCts.Text) > 0 Then dataok = False
                If Not CInt(txtIssTap.Text) > 0 Then dataok = False
            End If
            '**********************

            'rsComSql_2 = New ADODB.Recordset
            'rsComSql_2.Open("SELECT EmpNo FROM tblReturns WHERE EmpNo = '" & txtEmp.Text & "' AND LostPcs > 0 AND Active = 1", AdoCN, 1, 1)
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
                rsComSql_1.Open("SELECT FullEmpNo, CATEGORY, Pay FROM VW_EMP_MASTER_SMALL2 WHERE (Pay = 1) AND (FullEmpNo = '" & txtEmp.Text & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT dbo.tblIssues.EmpNo, dbo.tblIssues.IssDate, DATEDIFF(d, dbo.tblIssues.IssDate, GETDATE()) AS Days " & _
                                    "FROM dbo.tblIssues INNER JOIN dbo.tblNoneOrders ON dbo.tblIssues.ParNo = dbo.tblNoneOrders.OrderNo LEFT OUTER JOIN " & _
                                        "dbo.VW_PCUReturns ON dbo.tblIssues.ParNo = dbo.VW_PCUReturns.ParNo AND dbo.tblIssues.PktNo = dbo.VW_PCUReturns.PktNo AND dbo.tblIssues.Sec = dbo.VW_PCUReturns.Sec " & _
                                    "WHERE (dbo.tblIssues.EmpNo = '" & txtEmp.Text & "') AND (dbo.tblNoneOrders.Complete = N'N') AND (dbo.tblIssues.IssPcsT + dbo.tblIssues.IssPcsB - ISNULL(dbo.VW_PCUReturns.RetPcs, 0) > 0) AND (DATEDIFF(d, dbo.tblIssues.IssDate, GETDATE()) > '" & intDelayDays & "')", AdoCN, 1, 1)

                    If rsComSql_2.RecordCount Then
                        MsgBox("Have a Delayed Packet to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql_2 = Nothing
                End If
                rsComSql_1 = Nothing
            End If
        End If

        If dataok Then DataSave()
        cmdParPkt.Focus()

    End Sub

    Private Sub DataSave()
        Dim intActive As Integer

        intActive = 0

        dtpToday = GetToday()
        If issued = False Then
            'Issue packet
            mStrSQL = "INSERT INTO tblIssues(ParNo,PktNo,Flow,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,Sec,SecCount) " & _
                      "VALUES('" & ParcelNo & "','" & PacketNo & "','" & mFlow & "','" & ICNo & "'," & CInt(txtIssTap.Text) & "," & CInt(txtIssBag.Text) & "," & CSng(txtIssCts.Text) & "," & _
                        "'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "'," & cmbSection.SelectedIndex + 1 & "," & Section + 1 & ")"

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

            mStrSQL = "INSERT INTO tblReturns(ParNo, PktNo, Flow, SecCount, Sec, EmpNo, RetPcsT, RetPcsB, RetCts, RejPcs, RejCts, LostPcs, LostCts, BroPcs, RepPcs, NopayPcs, RetDate, RetTime, ExtPcs, Status, RghCts, LRghCts, RejReason, Active) " & _
                      "VALUES ('" & ParcelNo & "','" & PacketNo & "','" & mFlow & "'," & Section & "," & cmbSection.SelectedIndex + 1 & ",'" & ICNo & "'," & CInt(txtRetTap.Text) & "," & CInt(txtRetBag.Text) & "," & CSng(txtRetCts.Text) & "" & _
                        "," & CInt(txtRej.Text) & "," & CSng(txtRejCts.Text) & "," & CInt(txtLost.Text) & "," & CSng(txtLostCts.Text) & "," & CInt(txtBro.Text) & "," & CInt(txtRep.Text) & "," & CInt(txtNoPay.Text) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "'" & _
                        ",'" & Format(Date.Now, "HH:mm:ss") & "'," & CInt(txtExt.Text) & ",0," & CSng(txtRejRgh.Text) & "," & CSng(txtLstRgh.Text) & ",'" & cmbRejReason.Text & "','" & intActive & "')"

            AdoCN.Execute(mStrSQL)
        End If
        ClearFields()
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
            Rs1.Open("SELECT * FROM tblPacket WHERE PktOrdNo = '" & ParcelNo & "' And PktNo = '" & PacketNo & "'", AdoCN, 1, 1)
            If Rs1.RecordCount Then
                dblIssPcs = Rs1.Fields("PktPcs").Value
                dblIssCts = Rs1.Fields("PktCts").Value
                strFlow = Rs1.Fields("PktFlow").Value
                dblIssCts = Math.Round(dblIssCts, 3)

                rs2 = New ADODB.Recordset
                rs2.Open("SELECT * FROM tblIssues WHERE ParNo = '" & ParcelNo & "' And PktNo = '" & PacketNo & "' ORDER BY Sec DESC", AdoCN, 1, 1)
                If rs2.RecordCount = 0 Then
                    intSec = 1
                    For intSecCount = 1 To CInt(txtSection.Text)
                        rs3 = New ADODB.Recordset
                        rs3.Open("SELECT * FROM tblFlow WHERE Flow = '" & strFlow & "'", AdoCN, 1, 1)
                        If rs3.RecordCount Then
                            intSec = rs3.Fields("Flsec" & intSecCount).Value
                        Else
                            intSec = intSecCount
                        End If
                        rs3 = Nothing
                        If intSec > CInt(txtSection.Text) Then Exit For
                        If intSec = 0 Then Exit For
                        'Issues
                        AdoCN.Execute("INSERT INTO tblIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime) " & _
                                      "VALUES('" & ParcelNo & "','" & PacketNo & "','" & intSec & "','" & strFlow & "','" & intSecCount & "','" & txtEmp.Text & "',0,'" & dblIssPcs & "','" & dblIssCts & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "')")
                        'Returns
                        AdoCN.Execute("INSERT INTO tblReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate) " & _
                                      "VALUES('" & ParcelNo & "','" & PacketNo & "','" & strFlow & "','" & intSecCount & "','" & intSec & "','" & txtEmp.Text & "',0,'" & dblIssPcs & "','" & dblIssCts & "',0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "')")
                    Next
                    MsgBox("Successfully By Passed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Else
                    rs4 = New ADODB.Recordset
                    rs4.Open("SELECT * FROM VW_PCURealReturn WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & rs2.Fields("Sec").Value & "", AdoCN, 1, 1)
                    If rs4.RecordCount Then
                        If rs2.Fields("IssPcsT").Value + rs2.Fields("IssPcsB").Value = rs4.Fields("RETT").Value + rs4.Fields("RETB").Value + rs4.Fields("Rej").Value + rs4.Fields("Lost").Value - rs4.Fields("Ext").Value Then
                            dblRetPCsT = rs4.Fields("RETT").Value
                            dblRetPcsB = rs4.Fields("RETB").Value
                            dblRetCts = rs4.Fields("RETCts").Value
                            dblRetCts = Math.Round(dblRetCts, 3)
                            For intSecCount = rs2.Fields("Seccount").Value + 1 To CInt(txtSection.Text)
                                rs3 = New ADODB.Recordset
                                rs3.Open("SELECT * FROM tblFlow WHERE Flow = '" & strFlow & "'", AdoCN, 1, 1)
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
                                AdoCN.Execute("INSERT INTO tblIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime) " & _
                                              "VALUES('" & ParcelNo & "','" & PacketNo & "','" & intSec & "','" & strFlow & "','" & intSecCount & "','" & txtEmp.Text & "','" & dblRetPCsT & "','" & dblRetPcsB & "','" & dblRetCts & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "')")
                                'Returns
                                AdoCN.Execute("INSERT INTO tblReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate) " & _
                                              "VALUES('" & ParcelNo & "','" & PacketNo & "','" & strFlow & "','" & intSecCount & "','" & intSec & "','" & txtEmp.Text & "','" & dblRetPCsT & "','" & dblRetPcsB & "','" & dblRetCts & "',0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "')")
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
            txtRep.Focus()
        End If
    End Sub

    Private Sub txtRep_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRep.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtNoPay.Focus()
        End If
    End Sub

    Private Sub txtNoPay_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNoPay.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
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
End Class