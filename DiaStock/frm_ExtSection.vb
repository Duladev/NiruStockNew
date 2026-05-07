
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_ExtSection
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
    Dim strDepartment As String
    Dim mRghcts As Single
    Dim PCsPkt As Integer
    Dim AvgCtspkt As Double
    Dim ParcelNo As String
    Dim PacketNo As String

    Private Sub Load_ExtDept()
        Dim rstDept As ADODB.Recordset

        cmbDept.Items.Clear()
        rstDept = New ADODB.Recordset
        rstDept.Open("SELECT DISTINCT Department FROM tblExtFlow ORDER BY Department", AdoCN, 1, 1)
        If rstDept.RecordCount Then
            rstDept.MoveFirst()
            Do While Not rstDept.EOF
                cmbDept.Items.Add(rstDept.Fields("Department").Value)
                rstDept.MoveNext()
            Loop
        End If
        rstDept = Nothing
    End Sub

    Private Sub frm_ExtSection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        ClearFields()
        Load_ExtDept()
        Load_RejReasons()
    End Sub

    Private Sub Load_Section()
        Dim recsection As ADODB.Recordset

        cmbSection.Items.Clear()
        recsection = New ADODB.Recordset
        recsection.Open("SELECT * FROM tblExtSections WHERE Department = '" & strDepartment & "' ORDER BY SecCode", AdoCN, 1, 1)
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

    Private Sub cmbDept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDept.SelectedIndexChanged
        strDepartment = cmbDept.Text
        Load_Section()
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
        If ParcelLen = 10 Then
            Datavalid = True

            ParcelNo = Mid(Instring, 1, ParcelLen - 3)
            PacketNo = strRight(Instring, 3)
        Else
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
                If MsgBox("IC Numbers do not match. Proceed Anyway?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
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
        txtMacCts.Text = "0"
        frmnew = True
        txtIssDate.Text = ""
        txtIssTime.Text = ""
        txtRetTime.Text = Format(Date.Now, "yyyy/MM/dd")
        txtRetDate.Text = Format(Date.Now, "HH:mm:ss")
        txtRejCts.Text = ""
        txtLostCts.Text = ""
        cmbRejReason.Text = ""
        PictureBox1.Visible = False
        PictureBox2.Visible = False
        txtRejRgh.Text = ""
        txtLstRgh.Text = ""
        txtYield1.Text = ""
        txtYield2.Text = ""
        flxDetails.Rows.Clear()
        txtTotBag.Text = "0"
        txtTotTap.Text = "0"
        txtTotCts.Text = "0.000"
        txtNotOkPcs.Text = ""
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
        Dim rsChk25 As ADODB.Recordset

        cmdEmp.Focus()

        mStrSQL = "SELECT * FROM tblExtPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND DelDate IS NOT NULL AND AccDate IS NOT NULL AND Department = '" & strDepartment & "'"
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
        Else
            GoTo GoOut
        End If

        rsChk25 = New ADODB.Recordset
        rsChk25.Open("SELECT ParNo FROM tblExtReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = 25 AND Department = '" & strDepartment & "'", AdoCN, 1, 1)
        If rsChk25.RecordCount Then
            PictureBox1.Visible = False
            PictureBox2.Visible = False

            MsgBox("Completed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
            Exit Sub
        End If
        rsChk25 = Nothing

        'To find out the parcel FLOW count
        rs5 = New ADODB.Recordset
        rs5.Open("SELECT * FROM tblExtFlow WHERE Flow = '" & mFlow & "' AND Department = '" & strDepartment & "'", AdoCN, 1, 1)
        mFlowCount = rs5.Fields("FlowSections").Value

        'Find out last issued or returned section
        Rs = New ADODB.Recordset
        Rs.Open("SELECT tblExtIssues.Sec, tblExtIssues.SecCount, tblExtIssues.ParNo, tblExtIssues.PktNo, tblExtReturns.RetCts, tblExtIssues.IssDate, tblExtIssues.IssTime, " & _
                    "tblExtReturns.RetDate, tblExtReturns.RetTime,  tblExtIssues.EmpNo AS EmpIss, tblExtReturns.EmpNo, tblExtIssues.IssPcsT, tblExtIssues.IssPcsB, tblExtIssues.IssCts, " & _
                    "tblExtReturns.RetPcsT, tblExtReturns.RetPcsB, tblExtReturns.RejPcs , tblExtReturns.BroPcs, tblExtReturns.LostPcs, tblExtReturns.ExtPcs, tblExtReturns.NopayPcs, tblExtReturns.RepPcs " & _
                "FROM dbo.tblExtIssues LEFT OUTER JOIN dbo.tblExtReturns ON dbo.tblExtIssues.Department = dbo.tblExtReturns.Department AND dbo.tblExtIssues.Sec = dbo.tblExtReturns.Sec AND " & _
                    "dbo.tblExtIssues.ParNo = dbo.tblExtReturns.ParNo AND dbo.tblExtIssues.PktNo = dbo.tblExtReturns.PktNo " & _
                "WHERE (tblExtIssues.ParNo = '" & ParcelNo & "') AND (tblExtIssues.PktNo = '" & PacketNo & "') AND (tblExtIssues.Department = '" & strDepartment & "') " & _
                "ORDER BY tblExtIssues.Seccount DESC", AdoCN, 1, 1)

        If Rs.RecordCount > 0 Then
            'To find out last Issued/Returned Section

            chkByPass.Checked = False
            cmdByPass.Enabled = False
            txtSection.ReadOnly = True
            txtSection.Text = ""

            frmnew = False

            If Not IsDBNull(Rs.Fields("RetCts").Value) Then
                Section = Rs.Fields("Sec").Value

                'Get section Return entered last
                rs4 = New ADODB.Recordset
                mStrSQL = "SELECT tblExtIssues.Sec, tblExtIssues.ParNo, tblExtIssues.PktNo, SUM(tblExtReturns.RetCts) AS SumOfRetCts, tblExtIssues.IssPcsT, tblExtIssues.IssPcsB, tblExtIssues.IssCts,SUM(tblExtReturns.RetPcsT) AS SumOfRetPcsT, " & _
                                "SUM(tblExtReturns.RetPcsB) AS SumOfRetPcsB, SUM(tblExtReturns.RejPcs) AS SumOfRej,SUM(tblExtReturns.BroPcs) AS SumOfBro, SUM(tblExtReturns.LostPcs) AS SumOfLost, SUM(tblExtReturns.ExtPcs) AS SumOfExt, SUM(tblExtReturns.NopayPcs)AS SumOfNopay, " & _
                                "SUM(tblExtReturns.RepPcs) AS SumOfRepair, SUM(tblExtReturns.MacPcs) AS SumOfMac " & _
                            "FROM dbo.tblExtIssues INNER JOIN dbo.tblExtReturns ON dbo.tblExtIssues.PktNo = dbo.tblExtReturns.PktNo AND dbo.tblExtIssues.ParNo = dbo.tblExtReturns.ParNo AND " & _
                                "dbo.tblExtIssues.Sec = dbo.tblExtReturns.Sec And dbo.tblExtIssues.department = dbo.tblExtReturns.department " & _
                            "WHERE (tblExtIssues.Sec = '" & Section & "') AND (tblExtIssues.ParNo = '" & ParcelNo & "') AND (tblExtIssues.PktNo = '" & PacketNo & "') AND (tblExtIssues.Department = '" & strDepartment & "') " & _
                            "GROUP BY tblExtIssues.Sec, tblExtIssues.ParNo, tblExtIssues.PktNo, tblExtIssues.IssPcsT, tblExtIssues.IssPcsB, tblExtIssues.IssCts " & _
                            "ORDER BY tblExtIssues.Sec DESC"
                rs4.Open(mStrSQL, AdoCN, 1, 1)

                mIssPcs = rs4.Fields("IssPcsT").Value + rs4.Fields("IssPcsB").Value
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

                    rs3 = New ADODB.Recordset
                    rs3.Open("SELECT * FROM VWExtTotalRetEmp WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' And Sec = '" & Section & "' ORDER BY Sec", AdoCN, 1, 1)
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
                    rs3.Open("SELECT SUM(RetPcsT) AS TotTap, SUM(RetPcsB) AS TotBag, ROUND(SUM(RetCts), 3) AS RetCts FROM dbo.tblExtReturns WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & ParcelNo & "' And PktNo = '" & PacketNo & "' And Sec = '" & Section & "'", AdoCN, 1, 1)
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

                        sqlStringm = "SELECT SUM(RetCts) AS SumOfRetCts, SUM(RetPcsT) AS SumOfRetPcsT, SUM(RetPcsB) AS SumOfRetPcsB " & _
                                     "FROM dbo.tblExtReturns " & _
                                     "WHERE (ParNo = '" & ParcelNo & "') AND (PktNo = '" & PacketNo & "') AND (Sec = '" & Section & "') AND (Department = '" & strDepartment & "') "

                        'sqlStringm = "SELECT tblExtIssues.Sec, tblExtIssues.ParNo, tblExtIssues.PktNo, SUM(tblExtReturns.RetCts) AS SumOfRetCts,tblExtIssues.EmpNo, tblExtIssues.IssPcsT, tblExtIssues.IssPcsB, tblExtIssues.IssCts," & _
                        '                "SUM(tblExtReturns.RetPcsT) AS SumOfRetPcsT, SUM(tblExtReturns.RetPcsB) AS SumOfRetPcsB, SUM(tblExtReturns.RejPcs) AS SumOfRej,SUM(tblExtReturns.BroPcs) AS SumOfBro, SUM(tblExtReturns.LostPcs) AS SumOfLost, " & _
                        '                "SUM(tblExtReturns.ExtPcs) AS SumOfExt, SUM(tblExtReturns.NopayPcs)AS SumOfNopay, SUM(tblExtReturns.RepPcs) AS SumOfRepair " & _
                        '             "FROM dbo.tblExtIssues INNER JOIN dbo.tblExtReturns ON dbo.tblExtIssues.PktNo = dbo.tblExtReturns.PktNo AND dbo.tblExtIssues.ParNo = dbo.tblExtReturns.ParNo AND " & _
                        '                "dbo.tblExtIssues.Sec = dbo.tblExtReturns.Sec And dbo.tblExtIssues.department = dbo.tblExtReturns.department " & _
                        '             "WHERE (tblExtIssues.Sec = '" & Section & "') AND (tblExtIssues.ParNo = '" & ParcelNo & "') AND (tblExtIssues.PktNo = '" & PacketNo & "') AND (tblExtIssues.Department = '" & strDepartment & "') " & _
                        '             "GROUP BY tblExtIssues.Sec, tblExtIssues.ParNo, tblExtIssues.PktNo,tblExtIssues.EmpNo, tblExtIssues.IssPcsT, tblExtIssues.IssPcsB, tblExtIssues.IssCts " & _
                        '             "ORDER BY tblExtIssues.Sec DESC"
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

                        MsgBox("completed")
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

        Else
            'Section Issue/Return entries not found. New issue
            frmnew = True
            issued = False
            PictureBox2.Visible = True
            Rs.Close()
            Section = 0
            cmbSection.SelectedIndex = rs5.Fields("Flsec1").Value - 1
            txtIssBag.Text = "0"
            txtIssTap.Text = rs2.Fields("PktPcs").Value
            txtIssCts.Text = Format(Caretspkt, "#0.0##")
            cmdEmp.Focus()
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

        If txtRetBag.Text = "" Then txtRetBag.Text = 0
        If txtNotOkPcs.Text = "" Then txtNotOkPcs.Text = 0
        dataok = True

        If Trim(ICNo) = "" Then
            MsgBox("Invalid Employee No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If issued Then
            If Len(txtRetDate.Text) < 2 Then Exit Sub
            stiss = CInt(txtIssTap.Text) + CInt(txtIssBag.Text)
            stret = CInt(txtTotBag.Text) + CInt(txtTotTap.Text) + CInt(txtRetTap.Text) + CInt(txtRetBag.Text) + CInt(txtMacPcs.Text) + CInt(txtRej.Text) - CInt(txtExt.Text) + CInt(txtLost.Text)
            If stiss < stret Then
                strmsg = "Stones issued " & stiss & "   Stones returned " & stret
                If MsgBox(strmsg, MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                    ChkString = UCase(InputBox("Enter " & "Access Code", "Authorized Password"))
                    If ChkString = "STK08PCUP" Then
                        dataok = True
                    Else
                        dataok = False
                    End If
                Else
                    dataok = False
                End If
                If dataok = False Then Exit Sub
            End If

            If CInt(txtNotOkPcs.Text) > CInt(txtRetTap.Text) + CInt(txtRetBag.Text) Then
                MsgBox("Invalid Not OK Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub

            If cmbSection.SelectedIndex + 1 = 25 Then

            Else
                txtNotOkPcs.Text = 0
            End If

            ciss = CSng(txtIssCts.Text)
            cret = CSng(txtTotCts.Text) + CSng(txtRetCts.Text) + CSng(txtMacCts.Text) + CSng(txtRejCts.Text) 'cts ret
            If ciss < cret Then
                strmsg = "Carats issued " & Format(ciss, "##.###") & "   Carats returned " & Format(cret, "##.###")
                If MsgBox(strmsg, MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                    ChkString = UCase(InputBox("Enter " & "Access Code", "Authorized Password"))
                    If ChkString = "rad90" Then
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
            'rsComSql_2.Open("SELECT EmpNo FROM tblExtReturns WHERE EmpNo = '" & txtEmp.Text & "' AND LostPcs > 0 AND Active = 1 AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
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
                    rsComSql_2.Open("SELECT TOP (100) PERCENT DATEDIFF(d, dbo.tblExtIssues.IssDate, GETDATE()) AS Days " & _
                                    "FROM dbo.tblExtIssues INNER JOIN dbo.tblParcel ON dbo.tblExtIssues.ParNo = dbo.tblParcel.GrpParNo AND dbo.tblExtIssues.Department = dbo.tblParcel.Depart LEFT OUTER JOIN " & _
                                        "dbo.tblExtReturns ON dbo.tblExtIssues.Department = dbo.tblExtReturns.Department AND dbo.tblExtIssues.ParNo = dbo.tblExtReturns.ParNo AND dbo.tblExtIssues.PktNo = dbo.tblExtReturns.PktNo AND " & _
                                        "dbo.tblExtIssues.Sec = dbo.tblExtReturns.Sec " & _
                                    "WHERE (dbo.tblParcel.Complete = 0) AND (dbo.tblExtReturns.PktNo IS NULL) AND (DATEDIFF(d, dbo.tblExtIssues.IssDate, GETDATE()) > '" & intDelayDays & "') AND (dbo.tblParcel.Depart = '" & cmbDept.Text & "') AND (dbo.tblExtIssues.EmpNo = '" & txtEmp.Text & "')", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        MsgBox("Have a Delayed Packet to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql_2 = Nothing

                    If intCheckPastIssues = 1 Then
                        dtpToday = GetToday()
                        rsComSql_2 = New ADODB.Recordset
                        rsComSql_2.Open("SELECT TOP (100) PERCENT dbo.tblExtIssues.IssDate " & _
                                        "FROM dbo.tblExtIssues INNER JOIN dbo.tblParcel ON dbo.tblExtIssues.ParNo = dbo.tblParcel.GrpParNo LEFT OUTER JOIN dbo.tblExtReturns ON dbo.tblExtIssues.ParNo = dbo.tblExtReturns.ParNo AND dbo.tblExtIssues.PktNo = dbo.tblExtReturns.PktNo AND dbo.tblExtIssues.Sec = dbo.tblExtReturns.Sec " & _
                                        "WHERE (dbo.tblExtReturns.PktNo IS NULL) AND (dbo.tblParcel.Complete = 0) AND (dbo.tblParcel.Depart = '" & cmbDept.Text & "') AND (dbo.tblExtIssues.EmpNo = '" & txtEmp.Text & "') AND (dbo.tblExtIssues.IssDate < '" & Format(dtpToday, "MM/dd/yyyy") & "')", AdoCN, 1, 1)
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
                If Not (CInt(txtIssTap.Text) + CInt(txtIssBag.Text)) > 0 Then dataok = False
            End If
            '**********************
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
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT ParNo FROM tblExtIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = '" & cmbSection.SelectedIndex + 1 & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                mStrSQL = "INSERT INTO tblExtIssues(Department,ParNo,PktNo,Flow,Sec,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                          "VALUES('" & strDepartment & "','" & ParcelNo & "','" & PacketNo & "','" & mFlow & "'," & cmbSection.SelectedIndex + 1 & "," & _
                            "" & Section + 1 & ",'" & Mid(Trim(ICNo), 1, 6) & "'," & CInt(txtIssTap.Text) & "," & CInt(txtIssBag.Text) & "," & _
                            "" & CSng(txtIssCts.Text) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "')"

                AdoCN.Execute(mStrSQL)
            End If
            rsComSql = Nothing
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

            mStrSQL = "INSERT INTO tblExtReturns(Department,ParNo,PktNo,Flow,Sec,SecCount,EmpNo,RetPcsT,RetPcsB," & _
                        "RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,MacPcs,MacCts,RejReason,DoneBy,NotOkPcs,Active) " & _
                      "VALUES ('" & strDepartment & "','" & ParcelNo & "','" & PacketNo & "','" & mFlow & "'," & cmbSection.SelectedIndex + 1 & "," & _
                        "" & Section & ",'" & Mid(Trim(ICNo), 1, 6) & "'," & CInt(txtRetTap.Text) & "," & CInt(txtRetBag.Text) & "," & CDbl(txtRetCts.Text) & "," & CInt(txtRej.Text) & "," & _
                        "" & CDbl(txtRejCts.Text) & "," & CInt(txtLost.Text) & "," & CDbl(txtLostCts.Text) & "," & CInt(txtBro.Text) & "," & CInt(txtRep.Text) & "," & CInt(txtNoPay.Text) & "," & _
                        "'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "'," & CInt(txtExt.Text) & ",0," & CDbl(txtRejRgh.Text) & "," & CDbl(txtLstRgh.Text) & "," & _
                        "" & CDbl(txtMacPcs.Text) & "," & CDbl(txtMacCts.Text) & ",'" & cmbRejReason.Text & "','" & PBUser_EmpNo & "'," & CInt(txtNotOkPcs.Text) & ",'" & intActive & "')"

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
        txtMacPcs.Text = "0"
        txtMacCts.Text = "0"
        txtNotOkPcs.Text = "0"

        If Not IsNumeric(txtRetCts.Text) Then
            txtRetCts.Text = "0"
            txtRej.Focus()
        Else
            txtYield1.Text = Format(((CDbl(txtRetCts.Text)) / Caretspkt) * 100, "#0.00")
            Yld = txtYield1.Text
            wst = (CSng(txtIssCts.Text)) - (CSng(txtRetCts.Text))
            txtYield2.Text = Format(((wst) / Caretspkt) * 100, "#0.00")
            txtRej.Focus()
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
            Rs1.Open("SELECT * FROM tblExtPacket WHERE ParNo = '" & ParcelNo & "' And PktNo = '" & PacketNo & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
            If Rs1.RecordCount Then
                dblIssPcs = Rs1.Fields("PktPcs").Value
                dblIssCts = Rs1.Fields("PktCts").Value
                strFlow = Rs1.Fields("PktFlow").Value
                dblIssCts = Math.Round(dblIssCts, 3)

                rs2 = New ADODB.Recordset
                rs2.Open("SELECT * FROM tblExtIssues WHERE ParNo = '" & ParcelNo & "' And PktNo = '" & PacketNo & "' AND Department = '" & cmbDept.Text & "' ORDER BY Sec DESC", AdoCN, 1, 1)
                If rs2.RecordCount = 0 Then
                    intSec = 1
                    For intSecCount = 1 To CInt(txtSection.Text)
                        rs3 = New ADODB.Recordset
                        rs3.Open("SELECT * FROM tblExtFlow WHERE Flow = '" & strFlow & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                        If rs3.RecordCount Then
                            intSec = rs3.Fields("Flsec" & intSecCount).Value
                        Else
                            intSec = intSecCount
                        End If
                        rs3 = Nothing
                        If intSec > CInt(txtSection.Text) Then Exit For
                        If intSec = 0 Then Exit For
                        'Issues
                        AdoCN.Execute("INSERT INTO tblExtIssues(Department,ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                       "VALUES('" & cmbDept.Text & "','" & ParcelNo & "','" & PacketNo & "','" & intSec & "','" & strFlow & "','" & intSecCount & "','" & txtEmp.Text & "','" & dblIssPcs & "',0,'" & dblIssCts & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & PBUser_EmpNo & "')")

                        'Returns
                        AdoCN.Execute("INSERT INTO tblExtReturns(Department,ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,Gra_Trf,MacPcs,MacCts,DoneBy) " & _
                                       "VALUES('" & cmbDept.Text & "','" & ParcelNo & "','" & PacketNo & "','" & strFlow & "','" & intSecCount & "','" & intSec & "','" & txtEmp.Text & "','" & dblIssPcs & "',0,'" & dblIssCts & "',0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,0,0,'" & PBUser_EmpNo & "')")
                    Next
                    MsgBox("Successfully By Passed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Else
                    rs4 = New ADODB.Recordset
                    rs4.Open("SELECT * FROM VW_ExtRealReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & rs2.Fields("Sec").Value & " AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                    If rs4.RecordCount Then
                        If rs2.Fields("IssPcsT").Value + rs2.Fields("IssPcsB").Value = rs4.Fields("SumOfRetPcst").Value + rs4.Fields("SumOfRetPcsB").Value + rs4.Fields("SumOfRej").Value + rs4.Fields("SumOfLost").Value - rs4.Fields("SumOfExt").Value Then
                            dblRetPCsT = rs4.Fields("SumOfRetPcst").Value
                            dblRetPcsB = rs4.Fields("SumOfRetPcsB").Value
                            dblRetCts = rs4.Fields("SumOfRetCts").Value
                            dblRetCts = Math.Round(dblRetCts, 3)
                            For intSecCount = rs2.Fields("Seccount").Value + 1 To CInt(txtSection.Text)
                                rs3 = New ADODB.Recordset
                                rs3.Open("SELECT * FROM tblExtFlow WHERE Flow = '" & strFlow & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
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
                                AdoCN.Execute("INSERT INTO tblExtIssues(Department,ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                              "VALUES('" & cmbDept.Text & "','" & ParcelNo & "','" & PacketNo & "','" & intSec & "','" & strFlow & "','" & intSecCount & "','" & txtEmp.Text & "','" & dblRetPCsT & "',0,'" & dblRetCts & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & PBUser_EmpNo & "')")

                                'Returns
                                AdoCN.Execute("INSERT INTO tblExtReturns(Department,ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,Gra_Trf,MacPcs,MacCts,DoneBy) " & _
                                              "VALUES('" & cmbDept.Text & "','" & ParcelNo & "','" & PacketNo & "','" & strFlow & "','" & intSecCount & "','" & intSec & "','" & txtEmp.Text & "','" & dblRetPCsT & "',0,'" & dblRetCts & "',0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,0,0,'" & PBUser_EmpNo & "')")
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
        txtRetTap.Text = txtIssTap.Text
        txtRetBag.Text = txtIssBag.Text
        txtRetCts.Text = txtIssCts.Text

        ICNo = Trim(txtEmp.Text)
        txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
        txtRetTime.Text = Format(Date.Now, "HH:mm")

        Fill_Data()
    End Sub

    Private Sub txtSection_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSection.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
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

    Private Sub txtNotOkPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNotOkPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub
End Class