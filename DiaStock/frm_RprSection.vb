
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RprSection
    Dim recIssues, recReturns, rstIssues, rstReturns As ADODB.Recordset
    Dim OK, Temp As Object
    Dim mGrp, strmsg, mFlow As String
    Dim Section As Integer
    Dim Caretspkt, Carets As Single
    Dim AvgCtspkt As Single
    Dim PCsPkt As Integer
    Dim ICNo2 As String
    Dim issued, frmnew As Boolean
    Dim recno As Long
    Dim Instring$
    Dim Instring1 As String
    Dim ChkString As String
    Dim mRghcts As Single
    Dim dtpStartTime As Date
    Dim dtpStartDate As Date
    Dim ParcelNo As String
    Dim PacketNo As String

    Private Sub frm_RprSection_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F5
                RecSave()
        End Select
    End Sub

    Private Sub frm_RprSection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        ClearFields()
        Load_RprDepts()
        Load_Shape()
        Load_Col()
        Load_Clarity()
        Load_Cut()
        Load_Machine()
        Load_Size()
    End Sub

    Private Sub ClearFields()
        txtParPkt.Text = ""
        txtEmp.Text = ""
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
        frmnew = True
        txtIssDate.Text = ""
        txtIssTime.Text = ""
        txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
        txtRetTime.Text = Format(Date.Now, "HH:mm:ss")
        txtRejCts.Text = ""
        txtRejRgh.Text = ""
        txtLstRgh.Text = ""
        txtLostCts.Text = ""
        txtLabPcs.Text = ""
        txtActPcs.Text = ""
        txtGlPcs.Text = ""
        txtYield1.Text = ""
        txtYield2.Text = ""
        flxDetails.Rows.Clear()
        cmbShape.Text = ""
        txtRghPcs.Text = ""
        txtRghCts.Text = ""
        txtFinCts.Text = ""
        txtValue.Text = ""
        cmbColor.Text = ""
        cmbClarity.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtPlanValue.Text = ""
        txtModel.Text = ""
        cmbCut3.Text = "Very Good"
        txtLen.Text = ""
        txtWid.Text = ""
        cmbRep.Text = ""
        cmbMachine.Text = "0"
        chkNight.Checked = False
        PictureBox1.Visible = False
        PictureBox2.Visible = False
        txtFinCount.Text = ""
        txtBalCount.Text = ""
        cmbSize.Text = ""
        txtStoneNo.Text = ""
        txtInput.Text = ""
    End Sub

    Private Sub cmbDept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDept.SelectedIndexChanged
        Load_Section()
        If Mid(cmbDept.Text, 1, 9) = "RoughPlan" Then
            pnlDetails.Visible = True
        Else
            pnlDetails.Visible = False
        End If
    End Sub

    Private Sub Load_Size()
        Dim rstCut As ADODB.Recordset

        cmbSize.Items.Clear()
        rstCut = New ADODB.Recordset
        rstCut.Open("SELECT * FROM tblRgfSize ORDER BY SizeDec", AdoCN, 1, 1)
        If rstCut.RecordCount Then
            rstCut.MoveFirst()
            Do While Not rstCut.EOF
                cmbSize.Items.Add(rstCut.Fields("SizeDec").Value)
                rstCut.MoveNext()
            Loop
        End If
        rstCut = Nothing
    End Sub

    Private Sub Load_RprDepts()
        cmbDept.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT DISTINCT Department FROM dbo.tblRPrFlow ORDER BY Department", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbDept.Items.Add(rsComSql.Fields("Department").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Section()
        Dim recsection As ADODB.Recordset

        cmbSection.Items.Clear()
        recsection = New ADODB.Recordset
        recsection.Open("SELECT * FROM tblRprSections WHERE Department = '" & cmbDept.Text & "' ORDER BY secCode", AdoCN, 1, 1)
        If recsection.RecordCount Then
            recsection.MoveFirst()
            Do
                cmbSection.Items.Add(recsection.Fields("SecName").Value)
                recsection.MoveNext()
            Loop Until recsection.EOF
        End If
        recsection = Nothing
        cmbSection.SelectedIndex = 0
        Section = 1

    End Sub

    Private Sub Load_Shape()
        Dim rstCut As ADODB.Recordset

        cmbShape.Items.Clear()
        rstCut = New ADODB.Recordset
        rstCut.Open("SELECT * FROM tblRPrShape ORDER BY Shape", AdoCN, 1, 1)
        If rstCut.RecordCount Then
            rstCut.MoveFirst()
            Do While Not rstCut.EOF
                cmbShape.Items.Add(rstCut.Fields("Shape").Value)
                rstCut.MoveNext()
            Loop
        End If
        rstCut = Nothing
    End Sub

    Private Sub Load_Col()
        Dim rstcol As ADODB.Recordset

        cmbColor.Items.Clear()
        rstcol = New ADODB.Recordset
        rstcol.Open("SELECT ColorCode FROM tblRPrColor ORDER BY ColorCode", AdoCN, 1, 1)
        If rstcol.RecordCount Then
            rstcol.MoveFirst()
            Do While Not rstcol.EOF
                cmbColor.Items.Add(rstcol.Fields("ColorCode").Value)
                rstcol.MoveNext()
            Loop
        End If
        rstcol = Nothing
    End Sub

    Private Sub Load_Clarity()
        Dim rstClarity As ADODB.Recordset

        cmbClarity.Items.Clear()
        rstClarity = New ADODB.Recordset
        rstClarity.Open("SELECT ClarityCode FROM tblRPrClarity ORDER BY ClarityCode", AdoCN, 1, 1)
        If rstClarity.RecordCount Then
            rstClarity.MoveFirst()
            Do While Not rstClarity.EOF
                cmbClarity.Items.Add(rstClarity.Fields("ClarityCode").Value)
                rstClarity.MoveNext()
            Loop
        End If
        rstClarity = Nothing
    End Sub

    Private Sub Load_Machine()
        Dim intRow As Integer

        cmbMachine.Items.Clear()
        For introws = 1 To 9
            cmbMachine.Items.Add(intRow)
        Next
    End Sub

    Private Sub Load_Cut()
        cmbCut3.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Cut FROM tblRPrCut ORDER BY Cut", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbCut3.Items.Add(rsComSql.Fields("Cut").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Dim rsGetDate As ADODB.Recordset

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

            rsGetDate = New ADODB.Recordset
            If strCurDateFormat = "MM/dd/yyyy" Or strCurDateFormat = "M/d/yyyy" Then
                rsGetDate.Open("SELECT CONVERT(VARCHAR(11),GETDATE(),101) AS Date1, CONVERT(VARCHAR(10),GETDATE(),108) AS Time1", AdoCN, 1, 1)
            Else
                rsGetDate.Open("SELECT CONVERT(VARCHAR(11),GETDATE(),103) AS Date1, CONVERT(VARCHAR(10),GETDATE(),108) AS Time1", AdoCN, 1, 1)
            End If

            dtpStartDate = CDate(rsGetDate.Fields("Date1").Value)
            dtpStartTime = rsGetDate.Fields("Time1").Value
            rsGetDate = Nothing
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
                If MsgBox("IC Numbers do not match. Proceed Anyway?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)  = MsgBoxResult.Yes Then
                    txtEmp.Text = ICNo
                    txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
                    txtRetTime.Text = Format(Date.Now, "HH:mm")
                    txtRetPcs.Focus()

                    If Mid(cmbDept.Text, 1, 11) = "RoughPlanAS" Then

                    End If
                Else
                    txtEmp.Text = ""
                    Datavalid = False
                    ICNo = ""
                End If
            Else
                txtEmp.Text = ICNo
                txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
                txtRetTime.Text = Format(Date.Now, "HH:mm")

                If Mid(cmbDept.Text, 1, 11) = "RoughPlanAS" And cmbSection.Text = "FinishPlan" Then
                    cmdGetDetails.Focus()
                Else
                    txtRetPcs.Focus()
                End If
            End If
        Else
            txtEmp.Text = ICNo
            txtIssDate.Text = Format(Date.Now, "dd/MM/yyyy")
            txtIssTime.Text = Format(Date.Now, "HH:mm")
            cmdEmp.Focus()
        End If
    End Sub

    Private Sub ShowDetails()
        Dim strsql, mfldname As String
        Dim rs4 As ADODB.Recordset
        Dim rs5 As ADODB.Recordset
        Dim rs2 As ADODB.Recordset
        Dim Rs As ADODB.Recordset
        Dim mIssPcs, mRetPcs, mFlowCount As Long
        Dim sqlString As String

        cmdEmp.Focus()

        If cmbDept.Text = "RoughBruting" Then
            strsql = "SELECT * FROM tblRPrPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & cmbDept.Text & "' AND DelDate IS NOT NULL AND AccDate IS NOT NULL"
        Else
            strsql = "SELECT * FROM tblRPrPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & cmbDept.Text & "' AND DelDate IS NOT NULL"
        End If

        rs2 = New ADODB.Recordset
        rs2.Open(strsql, AdoCN, 1, 1)
        If rs2.RecordCount > 0 Then
            Caretspkt = rs2.Fields("PktCts").Value
            mRghcts = rs2.Fields("PktCts").Value
            mFlow = rs2.Fields("PktFlow").Value
            txtModel.Text = rs2.Fields("Model").Value

            PCsPkt = rs2.Fields("PktPcs").Value
            If (rs2.Fields("PktPcs").Value) <> 0 Then
                AvgCtspkt = Math.Round(mRghcts / PCsPkt, 3)
                txtRejRgh.Text = AvgCtspkt
                txtLstRgh.Text = AvgCtspkt
            End If
        Else
            GoTo GoOut
        End If

        'To find out the parcel FLOW count
        rs5 = New ADODB.Recordset
        rs5.Open("SELECT * FROM tblRprFlow WHERE Flow = '" & mFlow & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
        If rs5.RecordCount Then
            mFlowCount = rs5.Fields("FlowSections").Value
        Else
            MsgBox("Invalid Flow", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Temp)
            Exit Sub
        End If

        'Plan Value
        txtPlanValue.Text = ""
        txtFinCount.Text = ""
        txtBalCount.Text = ""
        If Mid(cmbDept.Text, 1, 11) = "RoughPlanAS" Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(Value) AS Value FROM dbo.tblRPrPacketDetails WHERE (Department = N'RoughPlan') AND (ParNo = '" & ParcelNo & "') AND (PktNo = '" & PacketNo & "')", AdoCN, 1, 1)
            If Not IsDBNull(rsComSql.Fields("Value").Value) Then
                txtPlanValue.Text = rsComSql.Fields("Value").Value
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT COUNT(PktNo) AS PktCount FROM dbo.tblRPrPacket WHERE (Department = 'RoughPlan') AND (ParNo = '" & ParcelNo & "')", AdoCN, 1, 1)
            If Not IsDBNull(rsComSql.Fields("PktCount").Value) Then
                txtFinCount.Text = rsComSql.Fields("PktCount").Value
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT COUNT(dbo.tblRPrPacket.PktNo) AS BalCount " & _
                          "FROM dbo.tblRPrReturnDetails RIGHT OUTER JOIN dbo.tblRPrPacket ON dbo.tblRPrReturnDetails.ParNo = dbo.tblRPrPacket.ParNo AND dbo.tblRPrReturnDetails.PktNo = dbo.tblRPrPacket.PktNo " & _
                          "WHERE (dbo.tblRPrReturnDetails.PktNo IS NULL) AND (dbo.tblRPrPacket.ParNo = '" & ParcelNo & "') AND (dbo.tblRPrPacket.Department = 'RoughPlan')", AdoCN, 1, 1)
            If Not IsDBNull(rsComSql.Fields("BalCount").Value) Then
                txtBalCount.Text = rsComSql.Fields("BalCount").Value
            End If
            rsComSql = Nothing

            txtFinCount.Text = CDbl(txtFinCount.Text) - CDbl(txtBalCount.Text)
        End If

        'Find out last issued or returned section
        Rs = New ADODB.Recordset
        Rs.Open("SELECT tblRPrIssues.Sec, tblRPrIssues.SecCount, tblRPrIssues.ParNo, tblRPrIssues.PktNo, tblRPrReturns.RetCts, tblRPrIssues.IssDate, tblRPrIssues.IssTime, " & _
                    "tblRPrReturns.RetDate, tblRPrReturns.RetTime,  tblRPrIssues.EmpNo AS EmpIss, tblRPrReturns.EmpNo, tblRPrIssues.IssPcsT, tblRPrIssues.IssPcsB, tblRPrIssues.IssCts, " & _
                    "tblRPrReturns.RetPcsT, tblRPrReturns.RetPcsB, tblRPrReturns.RejPcs , tblRPrReturns.BroPcs, tblRPrReturns.LostPcs, tblRPrReturns.ExtPcs, tblRPrReturns.NopayPcs, tblRPrReturns.RepPcs " & _
                "FROM dbo.tblRPrIssues LEFT OUTER JOIN dbo.tblRPrReturns ON dbo.tblRPrIssues.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrIssues.Sec = dbo.tblRPrReturns.Sec AND " & _
                    "dbo.tblRPrIssues.ParNo = dbo.tblRPrReturns.ParNo AND dbo.tblRPrIssues.PktNo = dbo.tblRPrReturns.PktNo " & _
                "WHERE (tblRPrIssues.ParNo = '" & ParcelNo & "') AND (tblRPrIssues.PktNo = '" & PacketNo & "') AND (tblRPrIssues.Department = '" & cmbDept.Text & "') " & _
                "ORDER BY tblRPrIssues.Seccount DESC", AdoCN, 1, 1)

        If Rs.RecordCount > 0 Then
            'To find out last Issued/Returned Section

            chkByPass.Checked = False
            cmdByPass.Enabled = False
            txtSection.ReadOnly = True
            txtSection.Text = ""

            frmnew = False
            Rs.MoveFirst()
            If Not IsDBNull(Rs.Fields("RetCts").Value) Then
                'To Find everything has returned
                Section = Rs.Fields("Sec").Value

                'Get section Return entered last
                rs4 = New ADODB.Recordset

                sqlString = "SELECT tblRPrIssues.Sec, tblRPrIssues.ParNo, tblRPrIssues.PktNo, SUM(tblRPrReturns.RetCts) AS SumOfRetCts, tblRPrIssues.IssPcsT, tblRPrIssues.IssPcsB, tblRPrIssues.IssCts,SUM(tblRPrReturns.RetPcsT) AS SumOfRetPcsT, " & _
                                "SUM(tblRPrReturns.RetPcsB) AS SumOfRetPcsB, SUM(tblRPrReturns.RejPcs) AS SumOfRej,SUM(tblRPrReturns.BroPcs) AS SumOfBro, SUM(tblRPrReturns.LostPcs) AS SumOfLost, SUM(tblRPrReturns.ExtPcs) AS SumOfExt, SUM(tblRPrReturns.NopayPcs)AS SumOfNopay, " & _
                                "SUM(tblRPrReturns.RepPcs) AS SumOfRepair " & _
                            "FROM dbo.tblRPrIssues INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrIssues.PktNo = dbo.tblRPrReturns.PktNo AND dbo.tblRPrIssues.ParNo = dbo.tblRPrReturns.ParNo AND " & _
                                "dbo.tblRPrIssues.Sec = dbo.tblRPrReturns.Sec And dbo.tblRPrIssues.department = dbo.tblRPrReturns.department " & _
                            "WHERE (tblRPrIssues.Sec = '" & Section & "') AND (tblRPrIssues.ParNo = '" & ParcelNo & "') AND (tblRPrIssues.PktNo = '" & PacketNo & "') AND (tblRPrIssues.Department = '" & cmbDept.Text & "') " & _
                            "GROUP BY tblRPrIssues.Sec, tblRPrIssues.ParNo, tblRPrIssues.PktNo, tblRPrIssues.IssPcsT, tblRPrIssues.IssPcsB, tblRPrIssues.IssCts " & _
                            "ORDER BY tblRPrIssues.Sec DESC"
                rs4.Open(sqlString, AdoCN, 1, 1)

                mIssPcs = rs4.Fields("IssPcsT").Value + rs4.Fields("IssPcsB").Value
                mRetPcs = rs4.Fields("SumOfRetPcsT").Value + rs4.Fields("SumOfRetPcsB").Value + rs4.Fields("SumOfRej").Value + rs4.Fields("SumOfBro").Value + rs4.Fields("SumOfLost").Value - rs4.Fields("SumOfExt").Value
                rs4.Close()

                If mIssPcs <> mRetPcs Then
                    Me.Close()
                    Exit Sub
                Else

                    If Rs.Fields("SecCount").Value < mFlowCount Then     'To check relevant flow is completed.

                        issued = False
                        PictureBox1.Visible = False
                        PictureBox2.Visible = True
                        mfldname = "Flsec" & Rs.Fields("SecCount").Value + 1
                        Section = rs5.Fields(mfldname).Value
                        cmbSection.SelectedIndex = Section - 1

                        Section = Rs.Fields("Sec").Value

                        rs4 = New ADODB.Recordset
                        Dim sqlStringm As String

                        sqlStringm = "SELECT SUM(RetCts) AS SumOfRetCts, SUM(RetPcsT) AS SumOfRetPcsT " & _
                                     "FROM dbo.tblRPrReturns " & _
                                     "WHERE (ParNo = '" & ParcelNo & "') AND (PktNo = '" & PacketNo & "') AND (Sec = '" & Section & "') AND (Department = '" & cmbDept.Text & "') "

                        'sqlStringm = "SELECT tblRPrIssues.Sec, tblRPrIssues.ParNo, tblRPrIssues.PktNo, SUM(tblRPrReturns.RetCts) AS SumOfRetCts,tblRPrIssues.EmpNo, tblRPrIssues.IssPcsT, tblRPrIssues.IssPcsB, tblRPrIssues.IssCts," & _
                        '                "SUM(tblRPrReturns.RetPcsT) AS SumOfRetPcsT, SUM(tblRPrReturns.RetPcsB) AS SumOfRetPcsB, SUM(tblRPrReturns.RejPcs) AS SumOfRej,SUM(tblRPrReturns.BroPcs) AS SumOfBro, SUM(tblRPrReturns.LostPcs) AS SumOfLost, " & _
                        '                "SUM(tblRPrReturns.ExtPcs) AS SumOfExt, SUM(tblRPrReturns.NopayPcs)AS SumOfNopay, SUM(tblRPrReturns.RepPcs) AS SumOfRepair " & _
                        '             "FROM dbo.tblRPrIssues INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrIssues.PktNo = dbo.tblRPrReturns.PktNo AND dbo.tblRPrIssues.ParNo = dbo.tblRPrReturns.ParNo AND " & _
                        '                "dbo.tblRPrIssues.Sec = dbo.tblRPrReturns.Sec And dbo.tblRPrIssues.department = dbo.tblRPrReturns.department " & _
                        '             "WHERE (tblRPrIssues.Sec = '" & Section & "') AND (tblRPrIssues.ParNo = '" & ParcelNo & "') AND (tblRPrIssues.PktNo = '" & PacketNo & "') AND (tblRPrIssues.Department = '" & cmbDept.Text & "') " & _
                        '             "GROUP BY tblRPrIssues.Sec, tblRPrIssues.ParNo, tblRPrIssues.PktNo,tblRPrIssues.EmpNo, tblRPrIssues.IssPcsT, tblRPrIssues.IssPcsB, tblRPrIssues.IssCts " & _
                        '             "ORDER BY tblRPrIssues.Sec DESC"
                        rs4.Open(sqlStringm, AdoCN, 1, 1)

                        If rs4.Fields("SumOfRetPcsT").Value = 0 Then
                            PictureBox1.Visible = False
                            PictureBox2.Visible = False

                            MsgBox("Packet Closed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Temp)
                            Rs.Close()
                            ClearFields()
                        Else
                            txtIssPcs.Text = rs4.Fields("SumOfRetPcsT").Value
                            txtIssCts.Text = Format(rs4.Fields("SumOfRetCts").Value, "#0.0##")

                            txtIssPcs.Enabled = False
                            txtIssCts.Enabled = False

                            Section = Rs.Fields("SecCount").Value

                            Rs.Close()
                        End If
                    Else
                        PictureBox1.Visible = False
                        PictureBox2.Visible = False

                        MsgBox("Packet Completed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Temp)
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

                mfldname = "Flsec" & Rs.Fields("SecCount").Value
                Section = rs5.Fields(mfldname).Value
                cmbSection.SelectedIndex = Section - 1

                Section = Rs.Fields("SecCount").Value

                ICNo2 = Rs.Fields("EmpIss").Value
                txtEmp.Text = Rs.Fields("EmpIss").Value
                txtIssPcs.Text = Rs.Fields("IssPcsT").Value
                txtIssCts.Text = Format(Rs.Fields("IssCts").Value, "#0.0##")
                txtIssPcs.Enabled = False
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
            txtIssPcs.Text = rs2.Fields("PktPcs").Value
            txtIssCts.Text = Format(Caretspkt, "#0.0##")
            cmdEmp.Focus()
        End If

        rs2.Close()
        Exit Sub
GoOut:
        MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
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

            If Mid(cmbDept.Text, 1, 11) = "RoughSawing" Then
                If CDbl(txtSection.Text) > 7 Then
                    MsgBox("You cannot Bypass to this section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            Rs1 = New ADODB.Recordset
            Rs1.Open("SELECT * FROM tblRPrPacket WHERE ParNo = '" & ParcelNo & "' And PktNo = '" & PacketNo & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
            If Rs1.RecordCount Then
                dblIssPcs = Rs1.Fields("PktPcs").Value
                dblIssCts = Rs1.Fields("PktCts").Value
                strFlow = Rs1.Fields("PktFlow").Value
                dblIssCts = Math.Round(dblIssCts, 3)

                rs2 = New ADODB.Recordset
                rs2.Open("SELECT * FROM tblRPrIssues WHERE ParNo = '" & ParcelNo & "' And PktNo = '" & PacketNo & "' AND Department = '" & cmbDept.Text & "' ORDER BY Sec DESC", AdoCN, 1, 1)
                If rs2.RecordCount = 0 Then
                    intSec = 1
                    For intSecCount = 1 To CInt(txtSection.Text)
                        rs3 = New ADODB.Recordset
                        rs3.Open("SELECT * FROM tblRprFlow WHERE Flow = '" & strFlow & "'  AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                        If rs3.RecordCount Then
                            intSec = rs3.Fields("Flsec" & intSecCount).Value
                        Else
                            intSec = intSecCount
                        End If
                        rs3 = Nothing
                        If intSec > CInt(txtSection.Text) Then Exit For
                        'Issues
                        AdoCN.Execute("INSERT INTO tblRPrIssues(Department,ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime) " & _
                                      "VALUES('" & cmbDept.Text & "','" & ParcelNo & "','" & PacketNo & "','" & intSec & "','" & strFlow & "','" & intSecCount & "','" & txtEmp.Text & "','" & dblIssPcs & "',0,'" & dblIssCts & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "')")

                        'Returns
                        AdoCN.Execute("INSERT INTO tblRPrReturns(Department,ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,UserName,CompName) " & _
                                      "VALUES('" & cmbDept.Text & "','" & ParcelNo & "','" & PacketNo & "','" & strFlow & "','" & intSecCount & "','" & intSec & "','" & txtEmp.Text & "','" & dblIssPcs & "',0,'" & dblIssCts & "',0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0,0,0,'" & PBUser_EmpNo & "','" & PBCompName & "')")
                    Next
                    MsgBox("Successfully By Passed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Else
                    rs4 = New ADODB.Recordset
                    rs4.Open("SELECT * FROM VW_RprRealReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & rs2.Fields("Sec").Value & " AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                    If rs4.RecordCount Then
                        If rs2.Fields("IssPcsT").Value + rs2.Fields("IssPcsB").Value = rs4.Fields("SumOfRetPcsT").Value + rs4.Fields("SumOfRetPcsB").Value + rs4.Fields("SumOfRej").Value + rs4.Fields("SumOfLost").Value - rs4.Fields("SumOfExt").Value Then
                            dblRetPCsT = rs4.Fields("SumOfRetPcsT").Value
                            dblRetPCsB = rs4.Fields("SumOfRetPcsB").Value
                            dblRetCts = rs4.Fields("SumOfRetCts").Value
                            dblRetCts = Math.Round(dblRetCts, 3)
                            For intSecCount = rs2.Fields("Seccount").Value + 1 To CInt(txtSection.Text)
                                rs3 = New ADODB.Recordset
                                rs3.Open("SELECT * FROM tblRprFlow WHERE Flow = '" & strFlow & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
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
                                AdoCN.Execute("INSERT INTO tblRPrIssues(Department,ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime) " & _
                                              "VALUES('" & cmbDept.Text & "','" & ParcelNo & "','" & PacketNo & "','" & intSec & "','" & strFlow & "','" & intSecCount & "','" & txtEmp.Text & "','" & dblRetPCsT & "',0,'" & dblRetCts & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "')")

                                'Returns
                                AdoCN.Execute("INSERT INTO tblRPrReturns(Department,ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,UserName,CompName) " & _
                                              "VALUES('" & cmbDept.Text & "','" & ParcelNo & "','" & PacketNo & "','" & strFlow & "','" & intSecCount & "','" & intSec & "','" & txtEmp.Text & "','" & dblRetPCsT & "',0,'" & dblRetCts & "',0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0,0,0,'" & PBUser_EmpNo & "','" & PBCompName & "')")
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

    Private Sub cmdByPass_Click(sender As Object, e As EventArgs) Handles cmdByPass.Click
        ByPassSection()
        txtSection.Text = ""
        txtSection.ReadOnly = True
        chkByPass.Checked = False
        cmdByPass.Enabled = False
        ClearFields()
    End Sub

    Private Sub RecSave()
        Dim dataok As Boolean
        Dim stiss, stret As Integer
        Dim ciss, cret As Single
        Dim dblFinCts As Double
        Dim dblWastage As Double

        dataok = True

        If Len(Trim(ICNo)) <> 6 Then
            MsgBox("Invalid Emp. No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        'rsComSql = New ADODB.Recordset
        'rsComSql.Open("SELECT MAX(DATEDIFF(D, dbo.tblRPrIssues.IssDate, GETDATE())) AS Days " & _
        '              "FROM dbo.tblRPrIssues INNER JOIN dbo.tblRPrPacket ON dbo.tblRPrIssues.Department = dbo.tblRPrPacket.Department AND dbo.tblRPrIssues.ParNo = dbo.tblRPrPacket.ParNo AND  " & _
        '                    "dbo.tblRPrIssues.PktNo = dbo.tblRPrPacket.PktNo LEFT OUTER JOIN " & _
        '                    "dbo.tblRPrReturns ON dbo.tblRPrIssues.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrIssues.ParNo = dbo.tblRPrReturns.ParNo AND  " & _
        '                    "dbo.tblRPrIssues.PktNo = dbo.tblRPrReturns.PktNo And dbo.tblRPrIssues.Sec = dbo.tblRPrReturns.Sec " & _
        '              "WHERE (dbo.tblRPrReturns.Department IS NULL) AND (dbo.tblRPrIssues.EmpNo = '" & Trim(ICNo) & "')", AdoCN, 1, 1)
        'If Not IsDBNull(rsComSql.Fields("Days").Value) Then
        '    If rsComSql.Fields("Days").Value > 6 Then
        '        PBResponse = MsgBox("Expire Packets Found. Do you want to Proceed?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        '        If PBResponse  = MsgBoxResult.Yes Then

        '        Else
        '            Exit Sub
        '        End If
        '    End If
        'End If
        'rsComSql = Nothing

        If issued Then
            If txtActPcs.Text = "" Then txtActPcs.Text = "0"
            If txtGlPcs.Text = "" Then txtGlPcs.Text = "0"

            txtPcs.Text = CalTotalPcs(flxDetails)
            txtCts.Text = CalTotalCts(flxDetails)

            If (cmbDept.Text = "RoughPlan" Or cmbDept.Text = "RoughPlan2" Or cmbDept.Text = "RoughPlanAS" Or cmbDept.Text = "RoughPlanAS2" Or cmbDept.Text = "RoughPlanAS3" Or cmbDept.Text = "RoughPlanAS4" Or cmbDept.Text = "RoughPlanAS5" Or cmbDept.Text = "RoughPlanAS6") And cmbSection.Text = "FinishPlan" Then
                If (CInt(txtRetPcs.Text)) <> CInt(txtPcs.Text) Then
                    MsgBox("Invalid Production Selection Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If Math.Round(CSng(txtRetCts.Text), 3) <> Math.Round(CSng(txtCts.Text), 3) Then
                    MsgBox("Invalid Production Selection Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            If cmbSection.Text <> "FinishPlan" Then
                If CInt(txtPcs.Text) <> 0 Then
                    MsgBox("Invalid Production Selection Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If CSng(txtCts.Text) <> 0 Then
                    MsgBox("Invalid Production Selection Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            If cmbDept.Text = "RoughSawing" Or cmbDept.Text = "RoughSawing2" Or cmbDept.Text = "RoughSawing3" Or cmbDept.Text = "RoughSawing4" Or cmbDept.Text = "RoughSawing5" Or cmbDept.Text = "RoughSawing6" Or cmbDept.Text = "RoughSawingS" Then
                If cmbSection.Text = "TS" Then
                    If CInt(txtRetPcs.Text) * 3 < CInt(txtLabPcs.Text) Then
                        MsgBox("Invalid Labour Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                Else
                    If CInt(txtRetPcs.Text) < CInt(txtLabPcs.Text) Then
                        MsgBox("Invalid Labour Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If
            Else
                If CInt(txtRetPcs.Text) < CInt(txtLabPcs.Text) Then
                    MsgBox("Invalid Labour Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            'Machine No
            If cmbMachine.Text = "" Then
                MsgBox("Invalid Machine No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If cmbDept.Text = "RoughBruting" Then
                If cmbSection.Text = "Bruting" Then
                    If CInt(cmbMachine.Text) <= 0 Then
                        MsgBox("Invalid Machine No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                Else
                    cmbMachine.Text = "0"
                End If
            End If
            If Mid(cmbDept.Text, 1, 11) = "RoughSawing" Then
                If cmbSection.Text = "Table/Lazer1" Then
                    If CInt(cmbMachine.Text) <= 0 Then
                        MsgBox("Invalid Machine No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                Else
                    cmbMachine.Text = "0"
                End If
            End If

            If txtActPcs.Text = "" Then
                MsgBox("Invalid Actual Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub

            If CInt(txtRep.Text) > 0 And cmbRep.Text = "" Then
                MsgBox("Invalid Repair Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CInt(txtRetPcs.Text) < CInt(txtRep.Text) Then
                MsgBox("Invalid Repair Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If cmbDept.Text = "RoughSawing" Or cmbDept.Text = "RoughSawing2" Or cmbDept.Text = "RoughSawing3" Or cmbDept.Text = "RoughSawing4" Or cmbDept.Text = "RoughSawing5" Or cmbDept.Text = "RoughSawing6" Or cmbDept.Text = "RoughSawingS" Then
                If cmbSection.Text = "FinishSawing" Then
                    If CInt(txtActPcs.Text) < 0 Then
                        MsgBox("Invalid Actual Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        dataok = False
                    End If
                    If dataok = False Then Exit Sub
                    If CInt(txtRetPcs.Text) > 0 Then
                        If CInt(txtActPcs.Text) > 5 And CInt(txtActPcs.Text) < 0 Then
                            MsgBox("Invalid Actual Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            dataok = False
                        End If
                    End If
                    If dataok = False Then Exit Sub
                End If
            End If

            If cmbDept.Text = "RoughBoil" Then
                If cmbSection.Text = "FinishBL" Then
                    If CInt(txtActPcs.Text) = 0 Then
                        MsgBox("Invalid Actual Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        dataok = False
                    End If
                    If dataok = False Then Exit Sub
                End If
            End If

            If Len(txtRetDate.Text) < 2 Then Exit Sub
            stiss = CInt(txtIssPcs.Text)
            stret = (CInt(txtRetPcs.Text) + CInt(txtRej.Text) + CInt(txtBro.Text) - CInt(txtExt.Text) + CInt(txtLost.Text))  'Ret pcs
            If stiss <> stret Then
                strmsg = "Stones issued " & stiss & "   Stones returned " & stret
                MsgBox(strmsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
                If dataok = False Then Exit Sub
            End If

            ciss = CSng(txtIssCts.Text)
            cret = CSng(txtRetCts.Text) + CSng(txtRejCts.Text)
            If ciss < cret Then
                strmsg = "Carats issued " & Format(ciss, "##.###") & "   Carats returned " & Format(cret, "##.###")
                MsgBox(strmsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If

            If UCase(PBUser_ID) <> "MANJULA" Then
                If Mid(cmbDept.Text, 1, 11) = "RoughSawing" Or Mid(cmbDept.Text, 1, 8) = "RoughOpr" Or Mid(cmbDept.Text, 1, 7) = "RoughWO" Or Mid(cmbDept.Text, 1, 7) = "RoughTS" Then
                    dblWastage = 0
                    dblFinCts = 0
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblRprSections WHERE Department = '" & cmbDept.Text & "' AND SecCode = '" & cmbSection.SelectedIndex + 1 & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        dblWastage = rsComSql.Fields("Wastage").Value
                    End If
                    rsComSql = Nothing

                    dblFinCts = (Caretspkt * dblWastage) / 100
                    dblFinCts = Math.Round(dblFinCts, 3)

                    If dblFinCts < Math.Round(CDbl(txtIssCts.Text) - (CDbl(txtRetCts.Text) + CDbl(txtLostCts.Text)), 3) Then
                        If Mid(cmbDept.Text, 1, 11) = "RoughSawing" Then
                            MsgBox("Wastage is high. Please check", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                        If Mid(cmbDept.Text, 1, 8) = "RoughOpr" Or Mid(cmbDept.Text, 1, 7) = "RoughWO" Or Mid(cmbDept.Text, 1, 7) = "RoughTS" Then
                            PBResponse = MsgBox("Wastage is high. Are you sure to continue?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
                            If PBResponse  = MsgBoxResult.Yes Then

                            Else
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If
        Else
            If Len(txtIssDate.Text) < 2 Then dataok = False

            If Section = 0 Then
                If Not CSng(txtIssCts.Text) > 0 Then dataok = False
                If Not CInt(txtIssPcs.Text) > 0 Then dataok = False
            End If

            'rsComSql_2 = New ADODB.Recordset
            'rsComSql_2.Open("SELECT EmpNo FROM tblRprReturns WHERE EmpNo = '" & txtEmp.Text & "' AND LostPcs > 0 AND Active = 1", AdoCN, 1, 1)
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
                    rsComSql_2.Open("SELECT TOP (100) PERCENT DATEDIFF(d, dbo.tblRprIssues.IssDate, GETDATE()) AS Days " & _
                                    "FROM dbo.tblRprIssues INNER JOIN dbo.tblParcel ON dbo.tblRprIssues.ParNo = dbo.tblParcel.GrpParNo AND dbo.tblRprIssues.Department = dbo.tblParcel.Depart LEFT OUTER JOIN " & _
                                        "dbo.tblRprReturns ON dbo.tblRprIssues.Department = dbo.tblRprReturns.Department AND dbo.tblRprIssues.ParNo = dbo.tblRprReturns.ParNo AND dbo.tblRprIssues.PktNo = dbo.tblRprReturns.PktNo AND " & _
                                        "dbo.tblRprIssues.Sec = dbo.tblRprReturns.Sec " & _
                                    "WHERE (dbo.tblParcel.Complete = 0) AND (dbo.tblRprReturns.PktNo IS NULL) AND (DATEDIFF(d, dbo.tblRprIssues.IssDate, GETDATE()) > '" & intDelayDays & "') AND (dbo.tblParcel.Depart = '" & cmbDept.Text & "') AND (dbo.tblRprIssues.EmpNo = '" & txtEmp.Text & "')", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        MsgBox("Have a Delayed Packet to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql_2 = Nothing
                End If
                rsComSql_1 = Nothing
            End If

        End If

        If dataok = True Then
            DataSave()
        End If
        cmdParPkt.Focus()

    End Sub

    Private Sub DataSave()
        Dim rst As ADODB.Recordset
        Dim intRow As Integer
        Dim intActive As Integer

        dtpToday = GetToday()
        intActive = 0

        'Issue packet
        If issued = False Then
            rst = New ADODB.Recordset
            mStrSQL = "INSERT INTO tblRPrIssues(Department,ParNo,PktNo,Flow,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,Sec,SecCount,DayShift) " & _
                      "VALUES('" & cmbDept.Text & "','" & ParcelNo & "','" & PacketNo & "','" & mFlow & "','" & Mid(Trim(ICNo), 1, 6) & "'," & CInt(txtIssPcs.Text) & "," & _
                        "0," & CSng(txtIssCts.Text) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "'," & (cmbSection.SelectedIndex) + 1 & "," & Section + 1 & "," & IIf(chkNight.Checked = True, 1, 0) & ")"

            rst.Open(mStrSQL, AdoCN, 1, 1)
            rst = Nothing
        Else
            If CDbl(txtLost.Text) > 0 Then
                intActive = 1
            End If

            'Return Packet
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT ParNo FROM tblRPrReturns WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & (cmbSection.SelectedIndex) + 1 & "", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                rst = New ADODB.Recordset
                mStrSQL = "INSERT INTO tblRPrReturns(Department,ParNo,PktNo,Flow,Sec,SecCount,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts," & _
                            "BagPcs,BagCts,PrPcs,PrCts,RndPcs,RndCts,OthPcs,OthCts,LabPcs,SmallPcs,SmallCts,ActPcs,GlPcs,StartTime,BagVal,PrVal,RndVal,OthVal,SmallVal,EmPcs,EmCts,EmVal,PcuPcs,PcuCts,PcuVal,LamPcs,LamCts,LamVal,DvPcs,DvCts,DvVal,UserName,CompName," & _
                            "BagFCts,PrFCts,RndFCts,OthFCts,SmallFCts,EmFCts,PcuFCts,LamFCts,DvFCts,RepType,MacNo,Active) " & _
                          "VALUES ('" & cmbDept.Text & "','" & ParcelNo & "','" & PacketNo & "','" & mFlow & "'," & (cmbSection.SelectedIndex) + 1 & "," & Section & ",'" & Mid(Trim(ICNo), 1, 6) & "'," & CInt(txtRetPcs.Text) & "," & _
                            "0," & CDbl(txtRetCts.Text) & "," & CInt(txtRej.Text) & "," & CDbl(txtRejCts.Text) & "," & CInt(txtLost.Text) & "," & CDbl(txtLostCts.Text) & "," & CInt(txtBro.Text) & "," & CInt(txtRep.Text) & "," & _
                            "" & CInt(txtNoPay.Text) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "'," & CInt(txtExt.Text) & ",0," & CDbl(txtRejRgh.Text) & "," & CDbl(txtLstRgh.Text) & ",0,0," & _
                            "0,0,0,0,0,0," & CInt(txtLabPcs.Text) & ",0,0," & CInt(txtActPcs.Text) & "," & CSng(txtGlPcs.Text) & ",GetDate(),0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,'" & PBUser_EmpNo & "','" & PBCompName & "',0,0,0,0,0,0,0,0,0,'" & cmbRep.Text & "','" & CInt(cmbMachine.Text) & "','" & intActive & "')"

                rst.Open(mStrSQL, AdoCN, 1, 1)

                'AdoCN.BeginTrans()
                'AdoCN.Execute(mStrSQL)
                'AdoCN.CommitTrans()
                rst = Nothing
            End If
            rsComSql = Nothing

            If CInt(txtPcs.Text) > 0 Then
                If cmbSection.Text = "FinishPlan" Then
                    AdoCN.Execute("DELETE FROM tblRPrReturnDetails WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "'")

                    For intRow = 0 To flxDetails.Rows.Count - 1
                        mStrSQL = "INSERT INTO tblRPrReturnDetails(Department,ParNo,PktNo,Shape,Pcs,RghCts,FinCts,Value,Color,Clarity,Cut,Length,Width,Size,StoneNo) " & _
                                  "VALUES('" & cmbDept.Text & "','" & ParcelNo & "','" & PacketNo & "','" & flxDetails.Item(0, intRow).Value & "'," & CDbl(flxDetails.Item(1, intRow).Value) & "," & _
                                        "" & CDbl(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & _
                                        "" & CDbl(flxDetails.Item(4, intRow).Value) & ",'" & flxDetails.Item(5, intRow).Value & "','" & flxDetails.Item(6, intRow).Value & "','" & flxDetails.Item(7, intRow).Value & "'," & _
                                        "" & CDbl(flxDetails.Item(8, intRow).Value) & "," & CDbl(flxDetails.Item(9, intRow).Value) & ",'" & flxDetails.Item(10, intRow).Value & "','" & UCase(flxDetails.Item(11, intRow).Value) & "')"
                        AdoCN.Execute(mStrSQL)
                    Next

                    AdoCN.Execute("UPDATE tblRPrPacket SET Trf = 1 WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "'")
                End If
            End If
        End If

        ClearFields()

    End Sub

    Private Sub txtRetPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRetPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtRetCts.Focus()
        End If
    End Sub

    Private Sub txtRej_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRej.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtRejCts.Focus()
        End If
    End Sub

    Private Sub txtLost_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLost.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtLostCts.Focus()
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
        If Asc(e.KeyChar) = 13 Then
            txtActPcs.Focus()
        End If
    End Sub

    Private Sub txtActPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtActPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtGlPcs.Focus()
        End If
    End Sub

    Private Sub txtGlPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGlPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then

        End If
    End Sub

    Private Sub txtLabPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLabPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            cmbMachine.Focus()
        End If
    End Sub

    Private Sub txtRejCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRejCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRejCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtLost.Focus()
        End If
    End Sub

    Private Sub txtLostCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLostCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtLostCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtBro.Focus()
        End If
    End Sub

    Private Sub txtRetCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRetCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRetCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtRej.Text = "0"
            txtRejCts.Text = "0"
            txtLost.Text = "0"
            txtLostCts.Text = "0"
            txtBro.Text = "0"
            txtExt.Text = "0"
            txtRep.Text = "0"
            txtNoPay.Text = "0"
            txtRejRgh.Text = "0"
            txtLstRgh.Text = "0"
            txtLabPcs.Text = "0"
            txtActPcs.Text = "0"
            txtLabPcs.Focus()
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

    Private Sub txtSection_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSection.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        RecSave()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub FillData()
        txtRetPcs.Text = txtIssPcs.Text
        txtRetCts.Text = txtIssCts.Text

        If cmbDept.Text = "RoughBruting" Then
            ICNo = Trim(txtEmp.Text)
            txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
            txtRetTime.Text = Format(Date.Now, "HH:mm")
        End If

        txtRej.Text = "0"
        txtRejCts.Text = "0"
        txtLost.Text = "0"
        txtLostCts.Text = "0"
        txtBro.Text = "0"
        txtExt.Text = "0"
        txtRep.Text = "0"
        txtNoPay.Text = "0"
        txtRejRgh.Text = "0"
        txtLstRgh.Text = "0"
        txtLabPcs.Text = "0"
        txtActPcs.Text = "0"
        txtGlPcs.Text = "0"
        txtLabPcs.Focus()

        If cmbDept.Text = "RoughSawing" Then
            If cmbSection.Text = "FinishSawing" Then
                If CDbl(txtRetPcs.Text) <= 3 Then
                    txtActPcs.Text = CDbl(txtRetPcs.Text) + 1
                End If
            End If
        End If
        If Mid(cmbDept.Text, 1, 11) = "RoughPlanAS" And cmbSection.Text = "FinishPlan" Then
            cmbShape.Focus()
        End If
    End Sub

    Private Sub cmdGetDetails_Click(sender As Object, e As EventArgs) Handles cmdGetDetails.Click
        FillData()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub txtRetCts_LostFocus(sender As Object, e As EventArgs) Handles txtRetCts.LostFocus
        If Len(txtRetCts.Text) = 0 Then
            txtRetCts.Text = "0"
            txtRetCts.Focus()
        Else
            txtYield2.Text = Format((txtRetCts.Text / mRghcts) * 100, "#0.00")
            txtYield1.Text = Format(((txtIssCts.Text - txtRetCts.Text) / mRghcts) * 100, "#0.00")
        End If
    End Sub

    Private Sub txtRghCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRghCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRghCts.Text)
        If Asc(e.KeyChar) = 13 Then
            If cmbShape.Text = "Small" Or cmbShape.Text = "SmallRounds" Then
                txtFinCts.Text = txtRghCts.Text
                txtValue.Text = "0.001"
                txtValue.Focus()
            Else
                txtFinCts.Focus()
            End If
        End If
    End Sub

    Private Sub txtFinCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFinCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtFinCts.Text)
        If Asc(e.KeyChar) = 13 Then
            If cmbShape.Text = "PCU" Or cmbShape.Text = "Orders" Then
                cmbSize.Focus()
            Else
                txtValue.Focus()
            End If
        End If
    End Sub

    Private Sub txtValue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtValue.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If cmbShape.Text = "Small" Or cmbShape.Text = "SmallRounds" Then
                cmbColor.Text = "H"
                cmbClarity.Text = "SI2"
                txtLen.Text = "0"
                txtWid.Text = "0"

                txtStoneNo.Focus()
            Else
                cmbColor.Focus()
            End If
        End If
    End Sub

    Private Sub cmbColor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbColor.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If cmbShape.Text = "PCU" Then
                txtStoneNo.Focus()
            Else
                cmbClarity.Focus()
            End If
        End If
    End Sub

    Private Sub cmbClarity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbClarity.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbCut3.Focus()
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

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim dblPlanValue As Double

        Dim strLength As String

        Dim strCode As String
        Dim strColor As String
        Dim strClarity As String
        Dim strCut As String

        Dim dblPcs As Double
        Dim dblCts As Double
        Dim dblOrigValue As Double

        If Mid(cmbDept.Text, 1, 11) <> "RoughPlanAS" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbShape.Text = "" Then MsgBox("Invalid Shape", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtRghPcs.Text = "" Then MsgBox("Invalid Rgh Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtRghCts.Text = "" Then MsgBox("Invalid Rgh Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtFinCts.Text = "" Then MsgBox("Invalid Fin Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtValue.Text = "" Then MsgBox("Invalid Value", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbColor.Text = "" Then MsgBox("Invalid Color", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbClarity.Text = "" Then MsgBox("Invalid Clarity", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbCut3.Text = "" Then MsgBox("Invalid Cut", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtLen.Text = "" Then MsgBox("Invalid Length", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtWid.Text = "" Then MsgBox("Invalid Width", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtStoneNo.Text = "" Then MsgBox("Invalid Stone No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If CDbl(txtRghPcs.Text) <= 0 Then MsgBox("Invalid Rgh Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtRghCts.Text) <= 0 Then MsgBox("Invalid Rgh Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtFinCts.Text) <= 0 Then MsgBox("Invalid Fin Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtValue.Text) <= 0 Then MsgBox("Invalid Value", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If cmbShape.Text = "Rounds" Or cmbShape.Text = "Small" Or cmbShape.Text = "SmallRounds" Then

        ElseIf cmbShape.Text = "PCU" Then
            If cmbSize.Text = "" Then MsgBox("Invalid Size", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If cmbSize.Text = "0" Then MsgBox("Invalid Size", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblRgfSize WHERE SizeDec = '" & cmbSize.Text & "' AND Dept = 'PCU'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Shape", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

        ElseIf cmbShape.Text = "Orders" Then
            If cmbSize.Text = "" Then MsgBox("Invalid Size", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If cmbSize.Text = "0" Then MsgBox("Invalid Size", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblRgfSize WHERE SizeDec = '" & cmbSize.Text & "' AND Dept = 'INT'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Shape", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

        Else
            If CDbl(txtLen.Text) <= 0 Then MsgBox("Invalid Length. Cannot be zero", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If CDbl(txtWid.Text) <= 0 Then MsgBox("Invalid Width. Cannot be zero", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        End If

        If CDbl(txtLen.Text) < CDbl(txtWid.Text) Then MsgBox("Invalid Length/Width.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        txtPcs.Text = CalTotalPcs(flxDetails)
        txtCts.Text = CalTotalCts(flxDetails)

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblRPrShape WHERE Shape = '" & cmbShape.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Shape", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblRPrColor WHERE ColorCode = '" & cmbColor.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Color", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblRPrClarity WHERE ClarityCode = '" & cmbClarity.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Clarity", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblRPrCut WHERE Cut = '" & cmbCut3.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Cut", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        If Mid(cmbShape.Text, 1, 5) <> "Small" And cmbShape.Text <> "Rejects" And cmbShape.Text = "Export" Then
            If CDbl(txtRghPcs.Text) <> 1 Then
                MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        End If

        If CDbl(txtRetPcs.Text) < CDbl(txtPcs.Text) + CDbl(txtRghPcs.Text) Then
            MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If Math.Round(CDbl(txtRetCts.Text), 3) < Math.Round(CDbl(txtCts.Text) + CDbl(txtRghCts.Text), 3) Then
            MsgBox("Invalid Rgh Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If Math.Round(CDbl(txtRghCts.Text), 3) < Math.Round(CDbl(txtFinCts.Text), 3) Then
            MsgBox("Invalid Fin Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        dblPlanValue = 0
        If cmbShape.Text = "Baguettes" Then
            dblPlanValue = 0
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT AVG(dbo.VW_BAGAssort2020.ListCost) AS ListCost " & _
                            "FROM dbo.VW_BAGAssort2020 INNER JOIN dbo.tblRPrCodes ON dbo.VW_BAGAssort2020.Color = dbo.tblRPrCodes.SysCode INNER JOIN " & _
                                "dbo.tblRPrCodes AS tblRPrCodes_1 ON dbo.VW_BAGAssort2020.Clarity = tblRPrCodes_1.SysCode " & _
                            "WHERE (dbo.tblRPrCodes.SysName = '" & Trim(cmbColor.Text) & "') AND (tblRPrCodes_1.SysName = '" & Trim(cmbClarity.Text) & "') AND (dbo.VW_BAGAssort2020.LengthFrom <= '" & txtLen.Text & "') AND (dbo.VW_BAGAssort2020.LengthTo >= '" & txtLen.Text & "') AND (dbo.VW_BAGAssort2020.WidthFrom <= '" & txtWid.Text & "')  " & _
                                "AND (dbo.VW_BAGAssort2020.WidthTo >= '" & txtWid.Text & "')", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                If Not IsDBNull(rsComSql_1.Fields("ListCost").Value) Then
                    dblPlanValue = Math.Round(rsComSql_1.Fields("ListCost").Value * CDbl(txtFinCts.Text), 0)
                    txtValue.Text = dblPlanValue
                Else
                    dblPlanValue = 0
                    txtValue.Text = dblPlanValue
                End If
            End If
            rsComSql_1 = Nothing

        ElseIf cmbShape.Text = "Princess" Then
            dblPlanValue = 0
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT AVG(dbo.VW_PRAssort2020.ListCost) AS ListCost " & _
                            "FROM dbo.VW_PRAssort2020 INNER JOIN dbo.tblRPrCodes ON dbo.VW_PRAssort2020.Color = dbo.tblRPrCodes.SysCode INNER JOIN " & _
                                "dbo.tblRPrCodes AS tblRPrCodes_1 ON dbo.VW_PRAssort2020.Clarity = tblRPrCodes_1.SysCode " & _
                            "WHERE (dbo.tblRPrCodes.SysName = '" & Trim(cmbColor.Text) & "') AND (tblRPrCodes_1.SysName = '" & Trim(cmbClarity.Text) & "') AND (dbo.VW_PRAssort2020.LengthFrom <= '" & txtLen.Text & "') AND (dbo.VW_PRAssort2020.LengthTo >= '" & txtLen.Text & "')", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                If Not IsDBNull(rsComSql_1.Fields("ListCost").Value) Then
                    dblPlanValue = Math.Round(rsComSql_1.Fields("ListCost").Value * CDbl(txtFinCts.Text), 0)
                    txtValue.Text = dblPlanValue
                Else
                    dblPlanValue = 0
                    txtValue.Text = dblPlanValue
                End If
            End If
            rsComSql_1 = Nothing

        ElseIf cmbShape.Text = "Rounds" Then
            strLength = txtLen.Text
            strCut = cmbCut3.Text
            strColor = cmbColor.Text
            strClarity = cmbClarity.Text
            dblPcs = CDbl(txtRghPcs.Text)
            dblCts = CDbl(txtRghCts.Text)
            dblOrigValue = CDbl(txtValue.Text)

            dblPlanValue = 0
            strCode = ""

            If CDbl(strLength) < 4.7 Then
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT Code, Cut, Color, Clarity " & _
                                "FROM dbo.VW_RndPriceListCode " & _
                                "WHERE (Color = '" & strColor & "') AND (Clarity = '" & strClarity & "') AND (Cut = '" & strCut & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strCode = rsComSql_1.Fields("Code").Value
                End If
                rsComSql_1 = Nothing

                If strCode = "" Then
                    'rsComSql_1 = New ADODB.Recordset
                    'rsComSql_1.Open("SELECT Code, Cut, Color, Clarity " & _
                    '                "FROM dbo.VW_RndPriceListCode " & _
                    '                "WHERE (Color = '" & strColor & "') AND (Clarity = '" & strClarity & "') AND (Cut = 'Very o')", AdoCN, 1, 1)
                    'If rsComSql_1.RecordCount Then
                    '    strCode = rsComSql_1.Fields("Code").Value
                    'End If
                    'rsComSql_1 = Nothing
                End If

                If strCode <> "" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT FromLen, ToLen, [EXIDEAL-DEF-NON-IFVVS2], [EXIDEAL-G-NON-IFVVS21], [EX-DEF-NON-IFVVS2], [EX-G-NON-IFVVS2], [VG-IFVVS-DEF], [VG-IFVVS-G], [D-G/VS1], [D-G/VS2], [D-G/SI1], [D-G/I2], [D-H/SI3], [D-H/I1], [H/VVS], [H/VS], " & _
                                        "[H/SI1], [H/SI2], [I/IF-VS], [I/SI-SI3], [J/IF-VS2], [J/SI1-I1], [KL/IF-SI1], [KL/SI2-I1], [MN/IF-I3], [TLB/IF-SI1], [TLB/SI2-I1], [DI/I2-13], [I/I1] " & _
                                    "FROM dbo.VW_RndPriceList2 " & _
                                    "WHERE (FromLen <= '" & strLength & "') AND (ToLen > '" & strLength & "')", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        dblPlanValue = IIf(Not IsDBNull(rsComSql_1.Fields(strCode).Value), rsComSql_1.Fields(strCode).Value, 0)
                        dblPlanValue = Math.Round(dblPlanValue * CDbl(txtFinCts.Text), 0)
                    Else
                        dblPlanValue = 0
                        txtValue.Text = dblPlanValue
                    End If
                    rsComSql_1 = Nothing
                End If

            Else
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT Code, Cut, Color, Clarity " & _
                                "FROM dbo.VW_RndPriceListCodeL " & _
                                "WHERE (Color = '" & strColor & "') AND (Clarity = '" & strClarity & "') AND (Cut = '" & strCut & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strCode = rsComSql_1.Fields("Code").Value
                End If
                rsComSql_1 = Nothing

                If strCode = "" Then
                    'rsComSql_1 = New ADODB.Recordset
                    'rsComSql_1.Open("SELECT Code, Cut, Color, Clarity " & _
                    '                "FROM dbo.VW_RndPriceListCodeL " & _
                    '                "WHERE (Color = '" & strColor & "') AND (Clarity = '" & strClarity & "') AND (Cut = 'Very Good')", AdoCN, 1, 1)
                    'If rsComSql_1.RecordCount Then
                    '    strCode = rsComSql_1.Fields("Code").Value
                    'End If
                    'rsComSql_1 = Nothing
                End If

                If strCode <> "" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT FromLen, ToLen, [EXIDEAL-D-NON-IF], [EXIDEAL-D-NON-VVS1], [EXIDEAL-D-NON-VVS2], [EXIDEAL-E-NON-IF], [EXIDEAL-E-NON-VVS1], [EXIDEAL-E-NON-VVS2], [EXIDEAL-F-NON-IF], " & _
                                        "[EXIDEAL-F-NON-VVS1], [EXIDEAL-F-NON-VVS2], [EXIDEAL-G-NON-IF], [EXIDEAL-G-NON-VVS1], [EXIDEAL-G-NON-VVS2], [D/VS1], [E/VS1], [F/VS1], [G/VS1], [D/VS2], [E/VS2], [F/VS2], [G/VS2], [D/SI1], [E/SI1], [F/SI1], [G/SI1], [D/SI2], " & _
                                        "[E/SI2], [F/SI2], [G/SI2], [D-H/SI3], [D-H/I1], [H/IF], [H/VVS1], [H/VVS2], [H/VS1], [H/VS2], [H/SI1], [H/SI2], [I/IF], [I/VVS1], [I/VVS2], [I/VS1], [I/VS2], [I/SI1], [I/SI2], [I/SI3], [J/IF-VS2], [J/SI1-I1], [KL/IF-SI1], [KL/SI2-I1], [MN/IF-I3], [TLB/IF-SI1], " & _
                                        "[TLB/SI2-I1], [DI/I2-13], [I/I1] " & _
                                    "FROM dbo.VW_RndPriceListL " & _
                                    "WHERE (FromLen <= '" & strLength & "') AND (ToLen > '" & strLength & "')", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        dblPlanValue = IIf(Not IsDBNull(rsComSql_1.Fields(strCode).Value), rsComSql_1.Fields(strCode).Value, 0)
                        dblPlanValue = Math.Round(dblPlanValue * CDbl(txtFinCts.Text), 0)
                    Else
                        dblPlanValue = 0
                        txtValue.Text = dblPlanValue
                    End If
                    rsComSql_1 = Nothing
                End If
            End If
            If dblPlanValue <> 0 Then
                txtValue.Text = dblPlanValue
            End If

        ElseIf cmbShape.Text = "PCU" Then
            dblPlanValue = 0

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT Pcs, Value FROM tblRPrPacketDetails WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Shape = '" & cmbShape.Text & "' AND Size = '" & cmbSize.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                dblPlanValue = Math.Round((rsComSql.Fields("Value").Value / rsComSql.Fields("Pcs").Value) * CDbl(txtRghPcs.Text), 0)
            Else
                dblPlanValue = Math.Round(CDbl(txtValue.Text), 0)
            End If
            rsComSql = Nothing
            txtValue.Text = dblPlanValue
        End If

        flxDetails.Rows.Add(cmbShape.Text,
                            txtRghPcs.Text,
                            txtRghCts.Text,
                            txtFinCts.Text,
                            txtValue.Text,
                            UCase(cmbColor.Text),
                            UCase(cmbClarity.Text),
                            UCase(cmbCut3.Text),
                            txtLen.Text,
                            txtWid.Text,
                            cmbSize.Text,
                            UCase(txtStoneNo.Text))

        txtPcs.Text = CalTotalPcs(flxDetails)
        txtCts.Text = CalTotalCts(flxDetails)

        cmbShape.Text = ""
        txtRghPcs.Text = ""
        txtRghCts.Text = ""
        txtFinCts.Text = ""
        txtValue.Text = ""
        cmbColor.Text = ""
        cmbClarity.Text = ""
        cmbCut3.Text = "Very Good"
        txtLen.Text = ""
        txtWid.Text = ""
        cmbSize.Text = ""
        txtStoneNo.Text = ""
        cmbShape.Focus()
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        cmbShape.Text = flxDetails.Item(0, flxDetails.CurrentRow.Index).Value
        txtRghPcs.Text = flxDetails.Item(1, flxDetails.CurrentRow.Index).Value
        txtRghCts.Text = flxDetails.Item(2, flxDetails.CurrentRow.Index).Value
        txtFinCts.Text = flxDetails.Item(3, flxDetails.CurrentRow.Index).Value
        txtValue.Text = flxDetails.Item(4, flxDetails.CurrentRow.Index).Value
        cmbColor.Text = flxDetails.Item(5, flxDetails.CurrentRow.Index).Value
        cmbClarity.Text = flxDetails.Item(6, flxDetails.CurrentRow.Index).Value
        cmbCut3.Text = flxDetails.Item(7, flxDetails.CurrentRow.Index).Value
        txtLen.Text = flxDetails.Item(8, flxDetails.CurrentRow.Index).Value
        txtWid.Text = flxDetails.Item(9, flxDetails.CurrentRow.Index).Value
        txtStoneNo.Text = flxDetails.Item(11, flxDetails.CurrentRow.Index).Value
    End Sub

    Private Sub flxDetails_DoubleClick(sender As Object, e As EventArgs) Handles flxDetails.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)

            txtPcs.Text = CalTotalPcs(flxDetails)
            txtCts.Text = CalTotalCts(flxDetails)
        End If
    End Sub

    Private Sub cmbShape_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbShape.KeyDown
        Select Case e.KeyCode
            Case Keys.F5
                RecSave()
        End Select
    End Sub

    Private Sub cmbShape_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbShape.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If Mid(cmbShape.Text, 1, 5) <> "Small" Then
                txtRghPcs.Text = "1"
                txtRghCts.Focus()
            Else
                txtRghPcs.Focus()
            End If
            If cmbShape.Text = "PCU" Or cmbShape.Text = "PCU2" Then
                cmbClarity.Text = "IF"
            End If
            If cmbShape.Text <> "PCU" Then
                cmbSize.Text = "0"
            End If
        End If
    End Sub

    Private Sub txtRghPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRghPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtRghCts.Focus()
        End If
    End Sub

    Private Sub cmbCut3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbCut3.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtLen.Focus()
        End If
    End Sub

    Private Sub txtLen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLen.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtLen.Text)
        If Asc(e.KeyChar) = 13 Then
            If cmbShape.Text = "Rounds" Or cmbShape.Text = "Princess" Or cmbShape.Text = "Asscher" Or cmbShape.Text = "Carrer" Then
                txtWid.Text = txtLen.Text
            End If
            txtWid.Focus()
        End If
    End Sub

    Private Sub txtWid_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWid.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtWid.Text)
        If Asc(e.KeyChar) = 13 Then
            txtStoneNo.Focus()
        End If
    End Sub

    Private Sub cmbMachine_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbMachine.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtRej.Focus()
        End If
    End Sub

    Private Sub cmbSize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSize.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If cmbShape.Text = "Orders" Then
                txtValue.Focus()
            Else
                cmbColor.Focus()
            End If
        End If
    End Sub

    Private Sub cmbSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSize.SelectedIndexChanged
        If cmbSize.Text <> "" Then
            If cmbShape.Text = "PCU" Or cmbShape.Text = "Other" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblRgfSize WHERE SizeDec = '" & cmbSize.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If txtFinCts.Text <> "" Then
                        If cmbShape.Text = "Other" Then
                            txtValue.Text = Math.Round(rsComSql.Fields("Price2").Value * CDbl(txtFinCts.Text), 0)
                        Else
                            txtValue.Text = rsComSql.Fields("Price2").Value
                        End If

                        txtLen.Text = "0"
                        txtWid.Text = "0"
                    Else
                        txtValue.Text = "0"
                    End If
                Else
                    txtValue.Text = "0"
                End If
                rsComSql = Nothing
            Else
                txtValue.Focus()
            End If
        End If
    End Sub

    Private Sub cmdConvert_Click(sender As Object, e As EventArgs) Handles cmdConvert.Click
        Dim adata As Array
        Dim nX As Integer

        adata = ListAsArray(Trim(txtInput.Text), ",")

        For nX = LBound(adata) To UBound(adata)
            If nX = 2 Then
                cmbCut3.Text = adata(nX)
            End If
            If nX = 4 Then
                txtRghCts.Text = adata(nX)
            End If
            If nX = 5 Then
                txtFinCts.Text = adata(nX)
            End If
            If nX = 6 Then
                txtLen.Text = adata(nX)
            End If
            If nX = 7 Then
                txtWid.Text = adata(nX)
            End If
            If nX = 8 Then
                cmbShape.Text = adata(nX)
            End If
            If nX = 9 Then
                cmbColor.Text = adata(nX)
            End If
            If nX = 10 Then
                cmbClarity.Text = adata(nX)
            End If
            If nX = 11 Then
                txtValue.Text = adata(nX)
            End If
        Next nX
        txtInput.Text = ""
    End Sub

    Private Sub txtStoneNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtStoneNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmdAdd.Focus()
        End If
    End Sub
End Class