
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RprEntry
    Dim ParcelNo As String
    Dim PacketNo As String

    Private Sub Load_RprDepartments()
        cmbDept.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT Department FROM dbo.tblRPrFlow GROUP BY Department ORDER BY Department", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbDept.Items.Add(rsComSql.Fields("Department").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub frm_RprEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_RprDepartments()
        ClearText()
    End Sub

    Private Sub cmbDept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDept.SelectedIndexChanged
        Load_Section()
    End Sub

    Private Sub Load_Section()
        Dim rsSection As ADODB.Recordset

        cmbSection.Items.Clear()
        rsSection = New ADODB.Recordset
        rsSection.Open("SELECT * FROM tblRprSections WHERE Department = '" & cmbDept.Text & "' ORDER BY secCode", AdoCN, 1, 1)
        If rsSection.RecordCount Then
            rsSection.MoveFirst()
            Do
                cmbSection.Items.Add(rsSection.Fields("SecName").Value)
                rsSection.MoveNext()
            Loop Until rsSection.EOF
        End If
        rsSection = Nothing
        cmbSection.SelectedIndex = 0
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_Packets()
        Dim intCurSec As Integer

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbSection.Text = "" Then MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbSection.SelectedIndex + 1 > 14 And cmbSection.SelectedIndex + 1 < 20 Then MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If optReturn.Checked = True Then
            intCurSec = cmbSection.SelectedIndex + 1
            txtParNo.Text = UCase(Trim(txtParNo.Text))

            flxDetails.Rows.Clear()
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrIssues.ParNo, dbo.tblRPrIssues.PktNo, dbo.tblRPrIssues.IssPcsT + dbo.tblRPrIssues.IssPcsB AS IssPcs, dbo.tblRPrIssues.IssCts, dbo.tblRPrIssues.Flow, dbo.tblRPrIssues.EmpNo " & _
                          "FROM dbo.tblRPrIssues LEFT OUTER JOIN dbo.tblRPrReturns ON dbo.tblRPrIssues.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrIssues.ParNo = dbo.tblRPrReturns.ParNo AND  " & _
                                "dbo.tblRPrIssues.PktNo = dbo.tblRPrReturns.PktNo And dbo.tblRPrIssues.Sec = dbo.tblRPrReturns.Sec " & _
                          "WHERE (dbo.tblRPrIssues.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrIssues.Department = '" & cmbDept.Text & "') AND (dbo.tblRPrIssues.Sec = " & intCurSec & ") AND (dbo.tblRPrReturns.ParNo IS NULL) " & _
                          "ORDER BY dbo.tblRPrIssues.PktNo", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                        rsComSql.Fields("PktNo").Value,
                                        rsComSql.Fields("IssPcs").Value,
                                        Math.Round(rsComSql.Fields("IssCts").Value, 3),
                                        rsComSql.Fields("EmpNo").Value,
                                        rsComSql.Fields("Flow").Value,
                                        False)

                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing

            txtTotPcs.Text = CalTotalPcs(flxDetails)
            txtTotCts.Text = CalTotalCts(flxDetails)
            txtCount.Text = flxDetails.RowCount
        End If
    End Sub

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Dim intRow As Integer
        Dim blnFound As Boolean
        Dim dblRetPcs As Double
        Dim dblRetCts As Double
        Dim strIssEmp As String
        Dim strFlow As String

        Dim intCurSec As Integer
        Dim intPreSec As Integer

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbSection.Text = "" Then MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbSection.SelectedIndex + 1 > 14 And cmbSection.SelectedIndex + 1 < 20 Then MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        Datavalid = False
        Parcel = True
        Instring = UCase(InputBox("Enter Par/Pkt Number"))
        ParcelLen = Len(Instring)
        If ParcelLen > 10 Then
            Datavalid = True

            ParcelNo = Mid(Instring, 1, ParcelLen - 4)
            PacketNo = strRight(Instring, 4)
        End If

        intCurSec = cmbSection.SelectedIndex + 1
        If intCurSec = 20 Then
            If Mid(cmbDept.Text, 1, 11) = "RoughSawing" Then
                intPreSec = 12
            Else
                intPreSec = 7
            End If
        Else
            intPreSec = intCurSec - 1
        End If

        If Datavalid = True Then
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
            If cmbDept.Text = "RoughBruting" Then
                rsComSql.Open("SELECT ParNo, PktFlow FROM tblRprPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & cmbDept.Text & "' AND DelDate IS NOT NULL AND AccDate IS NOT NULL", AdoCN, 1, 1)
            Else
                rsComSql.Open("SELECT ParNo, PktFlow FROM tblRprPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & cmbDept.Text & "' AND DelDate IS NOT NULL ", AdoCN, 1, 1)
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
                If optIssue.Checked = True Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT SecName FROM tblRprSections WHERE Department = '" & cmbDept.Text & "' AND SecCode = " & intCurSec & " AND SecName <> 'No'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        blnFound = True
                    Else
                        blnFound = False
                    End If
                    rsComSql = Nothing
                    If blnFound = False Then cmdParPkt.Focus() : Exit Sub
                Else
                    If optReturn.Checked = True Then
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT SecName FROM tblRprSections WHERE Department = '" & cmbDept.Text & "' AND SecCode = " & intCurSec & " AND SecName <> 'No' AND NoChange = 1", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            blnFound = True
                        Else
                            blnFound = False
                        End If
                        rsComSql = Nothing
                        If blnFound = False Then cmdParPkt.Focus() : Exit Sub
                    Else
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT SecName FROM tblRprSections WHERE Department = '" & cmbDept.Text & "' AND SecCode = " & intCurSec & " AND SecName <> 'No'", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            blnFound = True
                        Else
                            blnFound = False
                        End If
                        rsComSql = Nothing
                        If blnFound = False Then cmdParPkt.Focus() : Exit Sub
                    End If
                    
                End If
            End If

            If blnFound = True Then
                If optIssue.Checked = True Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT ParNo FROM tblRPrIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & cmbDept.Text & "' AND Sec = " & intCurSec & "", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        blnFound = True
                    Else
                        blnFound = False
                    End If
                    rsComSql = Nothing
                    If blnFound = False Then cmdParPkt.Focus() : Exit Sub
                Else
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT ParNo FROM tblRPrIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & cmbDept.Text & "' AND Sec = " & intCurSec & "", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        blnFound = False
                    Else
                        blnFound = True
                    End If
                    rsComSql = Nothing
                    If blnFound = False Then cmdParPkt.Focus() : Exit Sub
                End If
            End If

            If blnFound = True Then
                If optIssue.Checked = True Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT ParNo FROM tblRPrIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & cmbDept.Text & "' AND Sec = " & intPreSec & "", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        blnFound = True
                    Else
                        blnFound = False
                    End If
                    rsComSql = Nothing
                    If blnFound = False Then cmdParPkt.Focus() : Exit Sub
                Else
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT ParNo FROM tblRPrReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & cmbDept.Text & "' AND Sec = " & intCurSec & "", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        blnFound = False
                    Else
                        blnFound = True
                    End If
                    rsComSql = Nothing
                    If blnFound = False Then cmdParPkt.Focus() : Exit Sub
                End If
            End If

            dblRetPcs = 0
            dblRetCts = 0
            strIssEmp = ""
            If blnFound = True Then
                If optIssue.Checked = True Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT ParNo, RetPcsT + RetPcsB AS RetPcs, RetCts FROM tblRPrReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & cmbDept.Text & "' AND Sec = " & intPreSec & "", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        dblRetPcs = rsComSql.Fields("RetPcs").Value
                        dblRetCts = rsComSql.Fields("RetCts").Value
                        blnFound = True
                    Else
                        blnFound = False
                    End If
                    rsComSql = Nothing
                    If blnFound = False Then cmdParPkt.Focus() : Exit Sub
                Else
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT ParNo, IssPcsT + IssPcsB AS IssPcs, IssCts, EmpNo FROM tblRPrIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & cmbDept.Text & "' AND Sec = " & intCurSec & "", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        dblRetPcs = rsComSql.Fields("IssPcs").Value
                        dblRetCts = rsComSql.Fields("IssCts").Value
                        strIssEmp = Trim(rsComSql.Fields("EmpNo").Value)
                        blnFound = True
                    Else
                        blnFound = False
                    End If
                    rsComSql = Nothing
                    If blnFound = False Then cmdParPkt.Focus() : Exit Sub
                End If
            End If

            If blnFound = True Then
                flxDetails.Rows.Add(ParcelNo,
                                    PacketNo,
                                    dblRetPcs,
                                    Math.Round(dblRetCts, 3),
                                    strIssEmp,
                                    strFlow,
                                    True)

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

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(6).EditedFormattedValue = True Then
                CalTotalPcs = CalTotalPcs + Val(flxSample.Item(2, intRow).Value)
            End If
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(6).EditedFormattedValue = True Then
                CalTotalCts = CalTotalCts + Val(flxSample.Item(3, intRow).Value)
            End If
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Function CalTotalCount(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCount = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(6).EditedFormattedValue = True Then
                CalTotalCount = CalTotalCount + 1
            End If
        Next
    End Function

    Private Sub ClearText()
        'cmbDept.Text = ""
        'cmbSection.Text = ""
        'cmbSection.Items.Clear()
        flxDetails.Rows.Clear()
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0.000"
        txtEmpNo.Text = ""
        txtCount.Text = "0"
        txtParNo.Text = ""
        chkNight.Checked = False
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearText()
    End Sub

    Private Sub flxDetails_DoubleClick(sender As Object, e As EventArgs) Handles flxDetails.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse  = MsgBoxResult.Yes Then
            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)
            txtTotPcs.Text = CalTotalPcs(flxDetails)
            txtTotCts.Text = CalTotalCts(flxDetails)
            txtCount.Text = flxDetails.RowCount
        End If
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim intSecCount As Integer

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbSection.Text = "" Then MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbSection.SelectedIndex + 1 > 14 And cmbSection.SelectedIndex + 1 < 20 Then MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If cmbSection.SelectedIndex + 1 = 20 Then
            If Mid(cmbDept.Text, 1, 11) = "RoughSawing" Then
                intSecCount = 13
            Else
                intSecCount = 8
            End If
        Else
            intSecCount = cmbSection.SelectedIndex + 1
        End If

        If optIssue.Checked = True Then
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
        End If

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

        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(6, intRow).Value = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT PktFlow FROM tblRPrPacket WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If optIssue.Checked = True Then
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT PktNo FROM tblRPrIssues WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            mStrSQL = "INSERT INTO tblRPrIssues(Department,ParNo,PktNo,Flow,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,Sec,SecCount,DayShift) " & _
                                      "VALUES('" & cmbDept.Text & "','" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(5, intRow).Value & "','" & Mid(Trim(txtEmpNo.Text), 1, 6) & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & _
                                        "0," & CSng(flxDetails.Item(3, intRow).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "'," & cmbSection.SelectedIndex + 1 & "," & intSecCount & "," & IIf(chkNight.Checked = True, 1, 0) & ")"

                            AdoCN.Execute(mStrSQL)
                        End If
                        rsComSql_1 = Nothing
                    Else
                        If optReturn.Checked = True Then
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT PktNo FROM tblRPrReturns WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount = 0 Then
                                mStrSQL = "INSERT INTO tblRPrReturns(Department,ParNo,PktNo,Flow,Sec,SecCount,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts," & _
                                            "BagPcs,BagCts,PrPcs,PrCts,RndPcs,RndCts,OthPcs,OthCts,LabPcs,SmallPcs,SmallCts,ActPcs,GlPcs,StartTime,BagVal,PrVal,RndVal,OthVal,SmallVal,EmPcs,EmCts,EmVal,PcuPcs,PcuCts,PcuVal,UserName,CompName) " & _
                                          "VALUES ('" & cmbDept.Text & "','" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(5, intRow).Value & "'," & cmbSection.SelectedIndex + 1 & "," & intSecCount & ",'" & Mid(Trim(flxDetails.Item(4, intRow).Value), 1, 6) & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & _
                                            "0," & CDbl(flxDetails.Item(3, intRow).Value) & ",0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & " " & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,0,0,0,0,0,0,0,'" & PBUser_EmpNo & "','" & PBCompName & "')"

                                AdoCN.Execute(mStrSQL)
                            End If
                            rsComSql_1 = Nothing
                        Else
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT PktNo FROM tblRPrReturns WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount = 0 Then
                                If optDelete.Checked = True Then
                                    mStrSQL = "DELETE FROM tblRPrIssues WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 & ""

                                    AdoCN.Execute(mStrSQL)
                                End If
                            End If
                            rsComSql_1 = Nothing
                        End If
                    End If

                End If
                rsComSql = Nothing
            End If
        Next

        MsgBox("Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        ClearText()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub optIssue_CheckedChanged(sender As Object, e As EventArgs) Handles optIssue.CheckedChanged
        flxDetails.Rows.Clear()
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0.000"
        txtCount.Text = "0"
    End Sub

    Private Sub optReturn_CheckedChanged(sender As Object, e As EventArgs) Handles optReturn.CheckedChanged
        flxDetails.Rows.Clear()
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0.000"
        txtCount.Text = "0"
    End Sub

    Private Sub cmbSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSection.SelectedIndexChanged
        flxDetails.Rows.Clear()
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0.000"
        txtEmpNo.Text = ""
        txtCount.Text = "0"
    End Sub

    Private Sub optDelete_CheckedChanged(sender As Object, e As EventArgs) Handles optDelete.CheckedChanged
        flxDetails.Rows.Clear()
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0.000"
        txtCount.Text = "0"
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtParNo.Text <> "" Then
                If Len(txtParNo.Text) >= 7 Then
                    Load_Packets()
                End If
            End If
        End If
    End Sub

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 6 Then
            txtTotPcs.Text = CalTotalPcs(flxDetails)
            txtTotCts.Text = CalTotalCts(flxDetails)
            txtCount.Text = CalTotalCount(flxDetails)
        End If
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(6, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(6, intRow).Value = False
            Next
        End If

        txtTotPcs.Text = CalTotalPcs(flxDetails)
        txtTotCts.Text = CalTotalCts(flxDetails)
        txtCount.Text = CalTotalCount(flxDetails)
    End Sub
End Class