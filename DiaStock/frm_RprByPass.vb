
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RprByPass

    Dim strDepartment As String
    Dim ParcelNo As String
    Dim PacketNo As String

    Private Sub frm_RprByPass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        ClearText()
        Load_RprDepartments()
    End Sub

    Private Sub Load_RprDepartments()
        cmbDept.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT Department FROM dbo.tblRPrFlow WHERE Department LIKE 'RoughPlan%' OR Department LIKE 'RoughBruting%' GROUP BY Department ORDER BY Department", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbDept.Items.Add(rsComSql.Fields("Department").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub ClearText()
        flxDetails.Rows.Clear()
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0.000"
        txtEmpNo.Text = ""
        txtCount.Text = "0"
        txtParNo.Text = ""
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearText()
        cmbDept.Text = ""
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(2, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(3, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Dim intRow As Integer
        Dim blnFound As Boolean
        Dim dblRetPcs As Double
        Dim dblRetCts As Double
        Dim strIssEmp As String
        Dim strFlow As String
        Dim intCurSec As Integer
        Dim intNextSec As Integer

        If cmbDept.Text <> "" Then
            strDepartment = cmbDept.Text
        Else
            MsgBox("Please select the Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        Datavalid = False
        Parcel = True
        Instring = UCase(InputBox("Enter Par/Pkt Number"))
        ParcelLen = Len(Instring)
        If ParcelLen > 10 Then
            Datavalid = True

            ParcelNo = Mid(Instring, 1, ParcelLen - 4)
            PacketNo = strRight(Instring, 4)
        End If

        If cmbDept.Text = "RoughPlan" Or cmbDept.Text = "RoughPlan2" Then
            If opt20.Checked = True Then
                intCurSec = 4
                intNextSec = 5
            Else
                If opt20_3.Checked = True Then
                    intCurSec = 2
                    intNextSec = 3
                End If
                If opt20_4.Checked = True Then
                    intCurSec = 3
                    intNextSec = 4
                End If
                If opt5_4.Checked = True Then
                    intCurSec = 3
                    intNextSec = 4
                End If
                If opt6_4.Checked = True Then
                    intCurSec = 3
                    intNextSec = 4
                End If
                If opt20_7.Checked = True Then
                    intCurSec = 6
                    intNextSec = 7
                End If
                If opt8_2.Checked = True Then
                    intCurSec = 1
                    intNextSec = 2
                End If
                If opt20_10.Checked = True Then
                    intCurSec = 9
                    intNextSec = 10
                End If
                If opt20_11.Checked = True Then
                    intCurSec = 10
                    intNextSec = 11
                End If
                If opt20_12.Checked = True Then
                    intCurSec = 11
                    intNextSec = 12
                End If
                If opt20_14.Checked = True Then
                    intCurSec = 13
                    intNextSec = 14
                End If
                If opt8.Checked = True Then
                    intCurSec = 7
                    intNextSec = 8
                End If
            End If
        Else
            If opt20_2.Checked = True Or opt7_2.Checked = True Then
                intCurSec = 1
                intNextSec = 2
            Else
                intCurSec = 3
                intNextSec = 4
            End If
        End If

        If Datavalid = True Then
            If flxDetails.Rows.Count > 30 Then
                MsgBox("The Packet limit exceeds", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(0, intRow).Value = ParcelNo And flxDetails.Item(1, intRow).Value = PacketNo Then
                    MsgBox("Already Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    cmdParPkt.Focus()
                    Exit Sub
                End If
            Next

            blnFound = False
            strFlow = ""
            rsComSql = New ADODB.Recordset
            If strDepartment = "RoughBruting" Then
                rsComSql.Open("SELECT ParNo, PktFlow FROM tblRprPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & strDepartment & "' AND DelDate IS NOT NULL AND AccDate IS NOT NULL", AdoCN, 1, 1)
            Else
                rsComSql.Open("SELECT ParNo, PktFlow FROM tblRprPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & strDepartment & "' AND DelDate IS NOT NULL ", AdoCN, 1, 1)
            End If

            If rsComSql.RecordCount Then
                blnFound = True
                strFlow = rsComSql.Fields("PktFlow").Value
            Else
                blnFound = False
            End If
            rsComSql = Nothing
            If blnFound = False Then cmdParPkt.Focus() : Exit Sub

            If blnFound = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT ParNo FROM tblRPrIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & strDepartment & "' AND Sec = " & intNextSec & "", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                rsComSql = Nothing
                If blnFound = False Then cmdParPkt.Focus() : Exit Sub
            End If

            If blnFound = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT ParNo FROM tblRPrIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & strDepartment & "' AND Sec = " & intCurSec & "", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                rsComSql = Nothing
                If blnFound = False Then cmdParPkt.Focus() : Exit Sub
            End If

            dblRetPcs = 0
            dblRetCts = 0
            strIssEmp = ""
            If blnFound = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT ParNo, RetPcsT + RetPcsB AS RetPcs, RetCts FROM tblRPrReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & strDepartment & "' AND Sec = " & intCurSec & "", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    dblRetPcs = rsComSql.Fields("RetPcs").Value
                    dblRetCts = rsComSql.Fields("RetCts").Value
                    blnFound = True
                Else
                    blnFound = False
                End If
                rsComSql = Nothing
                If blnFound = False Then cmdParPkt.Focus() : Exit Sub
            End If

            If blnFound = True Then
                flxDetails.Rows.Add(ParcelNo,
                                    PacketNo,
                                    dblRetPcs,
                                    Math.Round(dblRetCts, 3),
                                    strIssEmp,
                                    strFlow)

                txtTotPcs.Text = CDbl(txtTotPcs.Text) + dblRetPcs
                txtTotCts.Text = Math.Round(CDbl(txtTotCts.Text) + dblRetCts, 3)
                txtCount.Text = CDbl(txtCount.Text) + 1

                'txtTotPcs.Text = CalTotalPcs(flxDetails)
                'txtTotCts.Text = CalTotalCts(flxDetails)
                'txtCount.Text = flxDetails.RowCount

                cmdParPkt.Focus()
            End If

        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim rs3 As ADODB.Recordset
        Dim intSec As Integer
        Dim strFlow As String
        Dim intStartSec As Integer
        Dim intMaxSec As Integer

        If Len(txtEmpNo.Text) <> 6 Then MsgBox("Invalid Emp No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & txtEmpNo.Text & "' AND Pay = 1", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Employee No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        Else
            txtEmpNo.Text = UCase(txtEmpNo.Text)
        End If
        rsComSql = Nothing

        'rsComSql_2 = New ADODB.Recordset
        'rsComSql_2.Open("SELECT EmpNo FROM tblRprReturns WHERE EmpNo = '" & txtEmpNo.Text & "' AND LostPcs > 0 AND Active = 1", AdoCN, 1, 1)
        'If rsComSql_2.RecordCount Then
        '    MsgBox("Have a Lost to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '    Exit Sub
        'End If
        'rsComSql_2 = Nothing

        rsComSql_2 = New ADODB.Recordset
        rsComSql_2.Open("SELECT EmpNo FROM VW_ALLLostEmpNo WHERE EmpNo = '" & txtEmpNo.Text & "'", AdoCN, 1, 1)
        If rsComSql_2.RecordCount Then
            MsgBox("Have a Lost to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql_2 = Nothing

        If intCheckIssDate = 1 Then
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT FullEmpNo, CATEGORY, Pay FROM VW_EMP_MASTER_SMALL2 WHERE (CATEGORY LIKE 'DIRECT%' OR CATEGORY LIKE 'TEMP%') AND (Pay = 1) AND (FullEmpNo = '" & txtEmpNo.Text & "')", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT TOP (100) PERCENT DATEDIFF(d, dbo.tblRprIssues.IssDate, GETDATE()) AS Days " & _
                                "FROM dbo.tblRprIssues INNER JOIN dbo.tblParcel ON dbo.tblRprIssues.ParNo = dbo.tblParcel.GrpParNo AND dbo.tblRprIssues.Department = dbo.tblParcel.Depart LEFT OUTER JOIN " & _
                                    "dbo.tblRprReturns ON dbo.tblRprIssues.Department = dbo.tblRprReturns.Department AND dbo.tblRprIssues.ParNo = dbo.tblRprReturns.ParNo AND dbo.tblRprIssues.PktNo = dbo.tblRprReturns.PktNo AND " & _
                                    "dbo.tblRprIssues.Sec = dbo.tblRprReturns.Sec " & _
                                "WHERE (dbo.tblParcel.Complete = 0) AND (dbo.tblRprReturns.PktNo IS NULL) AND (DATEDIFF(d, dbo.tblRprIssues.IssDate, GETDATE()) > '" & intDelayDays & "') AND (dbo.tblParcel.Depart = '" & cmbDept.Text & "') AND (dbo.tblRprIssues.EmpNo = '" & txtEmpNo.Text & "')", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    MsgBox("Have a Delayed Packet to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql_2 = Nothing
            End If
            rsComSql_1 = Nothing
        End If

        If cmbDept.Text <> "" Then
            strDepartment = cmbDept.Text
        Else
            MsgBox("Please select the Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If strDepartment = "RoughBruting" Then
            If opt3.Checked = False Then
                MsgBox("Invalid ByPass for Rough Bruting", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        End If

        If strDepartment = "RoughPlan" Or strDepartment = "RoughPlan2" Then
            If opt20.Checked = True Then
                intStartSec = 5
            End If
            If opt20_3.Checked = True Then
                intStartSec = 3
            End If
            If opt20_4.Checked = True Then
                intStartSec = 4
            End If
            If opt5_4.Checked = True Then
                intStartSec = 4
            End If
            If opt6_4.Checked = True Then
                intStartSec = 4
            End If
            If opt20_7.Checked = True Then
                intStartSec = 7
            End If
            If opt8_2.Checked = True Then
                intStartSec = 2
            End If
            If opt20_10.Checked = True Then
                intStartSec = 10
            End If
            If opt20_11.Checked = True Then
                intStartSec = 11
            End If
            If opt20_12.Checked = True Then
                intStartSec = 12
            End If
            If opt20_14.Checked = True Then
                intStartSec = 14
            End If
            If opt8.Checked = True Then
                intStartSec = 8
            End If
        Else
            If opt20_2.Checked = True Or opt7_2.Checked = True Then
                intStartSec = 2
            Else
                intStartSec = 4
            End If
        End If

        If opt7.Checked = True Or opt7_2.Checked = True Then
            intMaxSec = 7
        ElseIf opt5_4.Checked = True Then
            intMaxSec = 5
        ElseIf opt6_4.Checked = True Then
            intMaxSec = 6
        ElseIf opt20Issue.Checked = True Then
            intMaxSec = 20
        ElseIf opt3.Checked = True Then
            intMaxSec = 3
        ElseIf opt8_2.Checked = True Or opt8.Checked = True Then
            intMaxSec = 8
        Else
            intMaxSec = 20
        End If

        For intRow = 0 To flxDetails.Rows.Count - 1
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT PktFlow FROM tblRPrPacket WHERE Department = '" & strDepartment & "' AND ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                For intSecCount = intStartSec To intMaxSec
                    strFlow = flxDetails.Item(5, intRow).Value

                    rs3 = New ADODB.Recordset
                    rs3.Open("SELECT * FROM tblRprFlow WHERE Flow = '" & strFlow & "' AND Department = '" & strDepartment & "'", AdoCN, 1, 1)
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
                    If intSec > 20 Then Exit For
                    If intSec = 0 Then Exit For

                    'Issues
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT PktNo FROM tblRPrIssues WHERE Department = '" & strDepartment & "' AND ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & intSec & "", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount = 0 Then
                        AdoCN.Execute("INSERT INTO tblRPrIssues(Department,ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime) " & _
                                      "VALUES('" & strDepartment & "','" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & intSec & "','" & strFlow & "','" & intSecCount & "','" & txtEmpNo.Text & "','" & CDbl(flxDetails.Item(2, intRow).Value) & "',0,'" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")
                    End If
                    rsComSql_1 = Nothing

                    If intSec = 20 And opt20Issue.Checked = True Then GoTo NextRecord
                    'Returns
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT PktNo FROM tblRPrReturns WHERE Department = '" & strDepartment & "' AND ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & intSec & "", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount = 0 Then
                        AdoCN.Execute("INSERT INTO tblRPrReturns(Department,ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,UserName,CompName) " & _
                                      "VALUES('" & strDepartment & "','" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & strFlow & "','" & intSecCount & "','" & intSec & "','" & txtEmpNo.Text & "','" & CDbl(flxDetails.Item(2, intRow).Value) & "',0,'" & CDbl(flxDetails.Item(3, intRow).Value) & "',0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,'" & PBUser_EmpNo & "','" & PBCompName & "')")
                    End If
                    rsComSql_1 = Nothing
                Next
NextRecord:
            End If
            rsComSql = Nothing
        Next

        MsgBox("ByPassed Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        ClearText()
        'cmbDept.Text = ""
    End Sub

    Private Sub flxDetails_DoubleClick(sender As Object, e As EventArgs) Handles flxDetails.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)
            txtTotPcs.Text = CalTotalPcs(flxDetails)
            txtTotCts.Text = CalTotalCts(flxDetails)
            txtCount.Text = flxDetails.RowCount
        End If
    End Sub

    Private Sub cmbDept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDept.SelectedIndexChanged
        ClearText()
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        'If PBUser_EmpNo <> "D08426" Then MsgBox("Access Denied", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        flxDetails.Rows.Clear()
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0.000"
        txtCount.Text = "0"
        If txtParNo.Text <> "" And cmbDept.Text <> "" Then
            If Asc(e.KeyChar) = 13 Then
                txtParNo.Text = UCase(txtParNo.Text)
                rsComSql = New ADODB.Recordset
                If cmbDept.Text = "RoughPlan" Or cmbDept.Text = "RoughPlan2" Then
                    If opt20.Checked = True Then
                        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrPacket.Department, dbo.tblRPrPacket.ParNo, dbo.tblRPrPacket.PktNo, dbo.tblRPrReturns.RetPcsT + dbo.tblRPrReturns.RetPcsB AS RetPcs, dbo.tblRPrReturns.RetCts," & _
                                        "dbo.tblRPrPacket.PktFlow " & _
                                      "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrPacket.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrReturns.ParNo AND dbo.tblRPrPacket.PktNo = dbo.tblRPrReturns.PktNo LEFT OUTER JOIN " & _
                                        "dbo.VW_RprIssuesSec4 ON dbo.tblRPrReturns.Department = dbo.VW_RprIssuesSec4.Department AND dbo.tblRPrReturns.ParNo = dbo.VW_RprIssuesSec4.ParNo AND  " & _
                                        "dbo.tblRPrReturns.PktNo = dbo.VW_RprIssuesSec4.PktNo " & _
                                      "WHERE (dbo.tblRPrPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrReturns.Sec = 4) AND (dbo.VW_RprIssuesSec4.Sec IS NULL) " & _
                                      "ORDER BY dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)
                    Else
                        If opt20_3.Checked = True Then
                            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrPacket.Department, dbo.tblRPrPacket.ParNo, dbo.tblRPrPacket.PktNo, dbo.tblRPrReturns.RetPcsT + dbo.tblRPrReturns.RetPcsB AS RetPcs, dbo.tblRPrReturns.RetCts," & _
                                            "dbo.tblRPrPacket.PktFlow " & _
                                            "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrPacket.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrReturns.ParNo AND dbo.tblRPrPacket.PktNo = dbo.tblRPrReturns.PktNo LEFT OUTER JOIN " & _
                                            "dbo.VW_RprIssuesSec2 ON dbo.tblRPrReturns.Department = dbo.VW_RprIssuesSec2.Department AND dbo.tblRPrReturns.ParNo = dbo.VW_RprIssuesSec2.ParNo AND  " & _
                                            "dbo.tblRPrReturns.PktNo = dbo.VW_RprIssuesSec2.PktNo " & _
                                            "WHERE (dbo.tblRPrPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrReturns.Sec = 2) AND (dbo.VW_RprIssuesSec2.Sec IS NULL) " & _
                                            "ORDER BY dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)
                        End If
                        If opt20_4.Checked = True Or opt5_4.Checked = True Or opt6_4.Checked = True Then
                            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrPacket.Department, dbo.tblRPrPacket.ParNo, dbo.tblRPrPacket.PktNo, dbo.tblRPrReturns.RetPcsT + dbo.tblRPrReturns.RetPcsB AS RetPcs, dbo.tblRPrReturns.RetCts," & _
                                            "dbo.tblRPrPacket.PktFlow " & _
                                            "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrPacket.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrReturns.ParNo AND dbo.tblRPrPacket.PktNo = dbo.tblRPrReturns.PktNo LEFT OUTER JOIN " & _
                                            "dbo.VW_RprIssuesSec3 ON dbo.tblRPrReturns.Department = dbo.VW_RprIssuesSec3.Department AND dbo.tblRPrReturns.ParNo = dbo.VW_RprIssuesSec3.ParNo AND  " & _
                                            "dbo.tblRPrReturns.PktNo = dbo.VW_RprIssuesSec3.PktNo " & _
                                            "WHERE (dbo.tblRPrPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrReturns.Sec = 3) AND (dbo.VW_RprIssuesSec3.Sec IS NULL) " & _
                                            "ORDER BY dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)
                        End If
                        If opt20_7.Checked = True Then
                            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrPacket.Department, dbo.tblRPrPacket.ParNo, dbo.tblRPrPacket.PktNo, dbo.tblRPrReturns.RetPcsT + dbo.tblRPrReturns.RetPcsB AS RetPcs, dbo.tblRPrReturns.RetCts," & _
                                            "dbo.tblRPrPacket.PktFlow " & _
                                          "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrPacket.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrReturns.ParNo AND dbo.tblRPrPacket.PktNo = dbo.tblRPrReturns.PktNo LEFT OUTER JOIN " & _
                                            "dbo.VW_RprIssuesSec6 ON dbo.tblRPrReturns.Department = dbo.VW_RprIssuesSec6.Department AND dbo.tblRPrReturns.ParNo = dbo.VW_RprIssuesSec6.ParNo AND  " & _
                                            "dbo.tblRPrReturns.PktNo = dbo.VW_RprIssuesSec6.PktNo " & _
                                            "WHERE (dbo.tblRPrPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrReturns.Sec = 6) AND (dbo.VW_RprIssuesSec6.Sec IS NULL) " & _
                                            "ORDER BY dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)
                        End If
                        If opt8_2.Checked = True Then
                            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrPacket.Department, dbo.tblRPrPacket.ParNo, dbo.tblRPrPacket.PktNo, dbo.tblRPrReturns.RetPcsT + dbo.tblRPrReturns.RetPcsB AS RetPcs, dbo.tblRPrReturns.RetCts," & _
                                            "dbo.tblRPrPacket.PktFlow " & _
                                          "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrPacket.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrReturns.ParNo AND dbo.tblRPrPacket.PktNo = dbo.tblRPrReturns.PktNo LEFT OUTER JOIN " & _
                                            "dbo.VW_RprIssuesSec1New ON dbo.tblRPrReturns.Department = dbo.VW_RprIssuesSec1New.Department AND dbo.tblRPrReturns.ParNo = dbo.VW_RprIssuesSec1New.ParNo AND  " & _
                                            "dbo.tblRPrReturns.PktNo = dbo.VW_RprIssuesSec1New.PktNo " & _
                                            "WHERE (dbo.tblRPrPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrReturns.Sec = 1) AND (dbo.VW_RprIssuesSec1New.Sec IS NULL) " & _
                                            "ORDER BY dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)
                        End If
                        If opt20_10.Checked = True Then
                            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrPacket.Department, dbo.tblRPrPacket.ParNo, dbo.tblRPrPacket.PktNo, dbo.tblRPrReturns.RetPcsT + dbo.tblRPrReturns.RetPcsB AS RetPcs, dbo.tblRPrReturns.RetCts," & _
                                            "dbo.tblRPrPacket.PktFlow " & _
                                          "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrPacket.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrReturns.ParNo AND dbo.tblRPrPacket.PktNo = dbo.tblRPrReturns.PktNo LEFT OUTER JOIN " & _
                                            "dbo.VW_RprIssuesSec9 ON dbo.tblRPrReturns.Department = dbo.VW_RprIssuesSec9.Department AND dbo.tblRPrReturns.ParNo = dbo.VW_RprIssuesSec9.ParNo AND  " & _
                                            "dbo.tblRPrReturns.PktNo = dbo.VW_RprIssuesSec9.PktNo " & _
                                            "WHERE (dbo.tblRPrPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrReturns.Sec = 9) AND (dbo.VW_RprIssuesSec9.Sec IS NULL) " & _
                                            "ORDER BY dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)
                        End If
                        If opt20_11.Checked = True Then
                            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrPacket.Department, dbo.tblRPrPacket.ParNo, dbo.tblRPrPacket.PktNo, dbo.tblRPrReturns.RetPcsT + dbo.tblRPrReturns.RetPcsB AS RetPcs, dbo.tblRPrReturns.RetCts," & _
                                            "dbo.tblRPrPacket.PktFlow " & _
                                          "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrPacket.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrReturns.ParNo AND dbo.tblRPrPacket.PktNo = dbo.tblRPrReturns.PktNo LEFT OUTER JOIN " & _
                                            "dbo.VW_RprIssuesSec10 ON dbo.tblRPrReturns.Department = dbo.VW_RprIssuesSec10.Department AND dbo.tblRPrReturns.ParNo = dbo.VW_RprIssuesSec10.ParNo AND  " & _
                                            "dbo.tblRPrReturns.PktNo = dbo.VW_RprIssuesSec10.PktNo " & _
                                            "WHERE (dbo.tblRPrPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrReturns.Sec = 10) AND (dbo.VW_RprIssuesSec10.Sec IS NULL) " & _
                                            "ORDER BY dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)
                        End If
                        If opt20_12.Checked = True Then
                            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrPacket.Department, dbo.tblRPrPacket.ParNo, dbo.tblRPrPacket.PktNo, dbo.tblRPrReturns.RetPcsT + dbo.tblRPrReturns.RetPcsB AS RetPcs, dbo.tblRPrReturns.RetCts," & _
                                            "dbo.tblRPrPacket.PktFlow " & _
                                          "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrPacket.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrReturns.ParNo AND dbo.tblRPrPacket.PktNo = dbo.tblRPrReturns.PktNo LEFT OUTER JOIN " & _
                                            "dbo.VW_RprIssuesSec11 ON dbo.tblRPrReturns.Department = dbo.VW_RprIssuesSec11.Department AND dbo.tblRPrReturns.ParNo = dbo.VW_RprIssuesSec11.ParNo AND  " & _
                                            "dbo.tblRPrReturns.PktNo = dbo.VW_RprIssuesSec11.PktNo " & _
                                            "WHERE (dbo.tblRPrPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrReturns.Sec = 11) AND (dbo.VW_RprIssuesSec11.Sec IS NULL) " & _
                                            "ORDER BY dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)
                        End If
                        If opt20_14.Checked = True Then
                            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrPacket.Department, dbo.tblRPrPacket.ParNo, dbo.tblRPrPacket.PktNo, dbo.tblRPrReturns.RetPcsT + dbo.tblRPrReturns.RetPcsB AS RetPcs, dbo.tblRPrReturns.RetCts," & _
                                            "dbo.tblRPrPacket.PktFlow " & _
                                          "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrPacket.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrReturns.ParNo AND dbo.tblRPrPacket.PktNo = dbo.tblRPrReturns.PktNo LEFT OUTER JOIN " & _
                                            "dbo.VW_RprIssuesSec13 ON dbo.tblRPrReturns.Department = dbo.VW_RprIssuesSec13.Department AND dbo.tblRPrReturns.ParNo = dbo.VW_RprIssuesSec13.ParNo AND  " & _
                                            "dbo.tblRPrReturns.PktNo = dbo.VW_RprIssuesSec13.PktNo " & _
                                            "WHERE (dbo.tblRPrPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrReturns.Sec = 13) AND (dbo.VW_RprIssuesSec13.Sec IS NULL) " & _
                                            "ORDER BY dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)
                        End If
                        If opt8.Checked = True Then
                            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrPacket.Department, dbo.tblRPrPacket.ParNo, dbo.tblRPrPacket.PktNo, dbo.tblRPrReturns.RetPcsT + dbo.tblRPrReturns.RetPcsB AS RetPcs, dbo.tblRPrReturns.RetCts," & _
                                            "dbo.tblRPrPacket.PktFlow " & _
                                          "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrPacket.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrReturns.ParNo AND dbo.tblRPrPacket.PktNo = dbo.tblRPrReturns.PktNo LEFT OUTER JOIN " & _
                                            "dbo.VW_RprIssuesSec7 ON dbo.tblRPrReturns.Department = dbo.VW_RprIssuesSec7.Department AND dbo.tblRPrReturns.ParNo = dbo.VW_RprIssuesSec7.ParNo AND  " & _
                                            "dbo.tblRPrReturns.PktNo = dbo.VW_RprIssuesSec7.PktNo " & _
                                            "WHERE (dbo.tblRPrPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrReturns.Sec = 7) AND (dbo.VW_RprIssuesSec7.Sec IS NULL) " & _
                                            "ORDER BY dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)
                        End If
                    End If
                Else
                    If opt20_2.Checked = True Or opt7_2.Checked = True Then
                        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrPacket.Department, dbo.tblRPrPacket.ParNo, dbo.tblRPrPacket.PktNo, dbo.tblRPrReturns.RetPcsT + dbo.tblRPrReturns.RetPcsB AS RetPcs, dbo.tblRPrReturns.RetCts," & _
                                        "dbo.tblRPrPacket.PktFlow " & _
                                      "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrPacket.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrReturns.ParNo AND dbo.tblRPrPacket.PktNo = dbo.tblRPrReturns.PktNo LEFT OUTER JOIN " & _
                                        "dbo.VW_RprIssuesSec1New ON dbo.tblRPrReturns.Department = dbo.VW_RprIssuesSec1New.Department AND dbo.tblRPrReturns.ParNo = dbo.VW_RprIssuesSec1New.ParNo AND  " & _
                                        "dbo.tblRPrReturns.PktNo = dbo.VW_RprIssuesSec1New.PktNo " & _
                                        "WHERE (dbo.tblRPrPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrReturns.Sec = 1) AND (dbo.VW_RprIssuesSec1New.Sec IS NULL) " & _
                                        "ORDER BY dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)
                    Else
                        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrPacket.Department, dbo.tblRPrPacket.ParNo, dbo.tblRPrPacket.PktNo, dbo.tblRPrReturns.RetPcsT + dbo.tblRPrReturns.RetPcsB AS RetPcs," & _
                                        "dbo.tblRPrReturns.RetCts, dbo.tblRPrPacket.PktFlow " & _
                                      "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrPacket.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrReturns.ParNo AND " & _
                                        "dbo.tblRPrPacket.PktNo = dbo.tblRPrReturns.PktNo LEFT OUTER JOIN dbo.VW_RprIssuesSec3 ON dbo.tblRPrReturns.Department = dbo.VW_RprIssuesSec3.Department AND dbo.tblRPrReturns.ParNo = dbo.VW_RprIssuesSec3.ParNo AND " & _
                                        "dbo.tblRPrReturns.PktNo = dbo.VW_RprIssuesSec3.PktNo " & _
                                      "WHERE (dbo.tblRPrPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrReturns.Sec = 3) AND (dbo.VW_RprIssuesSec3.Sec IS NULL) " & _
                                      "ORDER BY dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)
                    End If
                End If
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                            rsComSql.Fields("PktNo").Value,
                                            rsComSql.Fields("RetPcs").Value,
                                            Math.Round(rsComSql.Fields("RetCts").Value, 3),
                                            "",
                                            rsComSql.Fields("PktFlow").Value)

                        txtTotPcs.Text = CDbl(txtTotPcs.Text) + rsComSql.Fields("RetPcs").Value
                        txtTotCts.Text = Math.Round(CDbl(txtTotCts.Text) + rsComSql.Fields("RetCts").Value, 3)
                        txtCount.Text = CDbl(txtCount.Text) + 1

                        rsComSql.MoveNext()
                    End While
                End If
                rsComSql = Nothing
            End If
        End If
    End Sub
End Class