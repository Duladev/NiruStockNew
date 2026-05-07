
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixByPass
    Dim ParcelNo As String
    Dim PacketNo As String
    Dim FirstInput As Date

    Private Sub frm_RndEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Section()
    End Sub

    Private Sub Load_Section()
        Dim rsSection As ADODB.Recordset

        cmbSection.Items.Clear()
        rsSection = New ADODB.Recordset
        rsSection.Open("SELECT * FROM tblMixSections ORDER BY SecCode", AdoCN, 1, 1)
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

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(2, intRow).Value) + Val(flxSample.Item(3, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(4, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Function CalTotalRghCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalRghCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalRghCts = CalTotalRghCts + Val(flxSample.Item(6, intRow).Value)
        Next
        CalTotalRghCts = Math.Round(CalTotalRghCts, 3)
    End Function

    Private Sub ClearText()
        cmbSection.Text = ""
        flxDetails.Rows.Clear()
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0.000"
        txtRghCts.Text = "0.000"
        txtEmp.Text = ""
        txtCount.Text = "0"
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearText()
    End Sub

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Dim intRow As Integer
        Dim blnFound As Boolean
        Dim dblIssPcs As Double
        Dim dblRetPcs As Double
        Dim dblRetPcsB As Double
        Dim dblRetCts As Double
        Dim dblRetEffPcs As Double
        Dim strGroup As String
        Dim dblPktCts As Double
        Dim intNextSec As Integer
        Dim intPrevSec As Integer

        intNextSec = cmbSection.SelectedIndex + 1
        intPrevSec = cmbSection.SelectedIndex

        If cmbSection.Text = "" Then MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If intNextSec > 18 Then MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        Datavalid = False
        Parcel = True
        Instring = UCase(InputBox("Enter Par/Pkt Number"))
        ParcelLen = Len(Instring)

        If ParcelLen >= 9 Then
            Datavalid = True
            If ParcelLen = 9 Then
                Datavalid = True

                ParcelNo = Mid(Instring, 1, ParcelLen - 3)
                PacketNo = strRight(Instring, 3)

            ElseIf ParcelLen = 10 Then
                Datavalid = True

                ParcelNo = Mid(Instring, 1, ParcelLen - 4)
                PacketNo = strRight(Instring, 4)
            End If
        End If

        If Datavalid = True Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(0, intRow).Value = ParcelNo And flxDetails.Item(1, intRow).Value = PacketNo Then
                    MsgBox("Already Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    cmdParPkt.Focus()
                    Exit Sub
                End If
            Next

            strGroup = ""
            blnFound = False
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT PktOrdNo,Grp,PktCts FROM tblMixPacket WHERE PktOrdNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND OK = 1 AND Accept = 1", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                strGroup = rsComSql.Fields("Grp").Value
                dblPktCts = rsComSql.Fields("PktCts").Value
                blnFound = True
            Else
                blnFound = False
            End If
            rsComSql = Nothing
            If blnFound = False Then MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : cmdParPkt.Focus() : Exit Sub

            If Trim(strGroup) = "" Then
                blnFound = False
            Else
                blnFound = True
            End If
            If blnFound = False Then MsgBox("Invalid Group", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : cmdParPkt.Focus() : Exit Sub

            If blnFound = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SecName FROM tblMixSections WHERE SecCode = " & intNextSec & " AND SecName <> 'No'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                rsComSql = Nothing
                If blnFound = False Then cmdParPkt.Focus() : Exit Sub
            End If

            If blnFound = True Then
                If intNextSec > 1 Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT SecCode FROM tblMixSections WHERE SecCode < " & intNextSec & " AND SecName <> 'No' ORDER BY SecCode DESC", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        rsComSql.MoveFirst()
                        intPrevSec = rsComSql.Fields("SecCode").Value
                    End If
                    rsComSql = Nothing
                End If
            End If

            If blnFound = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT ParNo FROM tblMixIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & intNextSec & "", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                rsComSql = Nothing
                If blnFound = False Then MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : cmdParPkt.Focus() : Exit Sub
            End If

            If blnFound = True Then
                If cmbSection.SelectedIndex + 1 > 1 Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT ParNo, IssPcsT + IssPcsB AS IssPcs FROM tblMixIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & intPrevSec & "", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        blnFound = True
                        dblIssPcs = rsComSql.Fields("IssPcs").Value
                    Else
                        blnFound = False
                    End If
                    rsComSql = Nothing
                    If blnFound = False Then MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : cmdParPkt.Focus() : Exit Sub
                End If
            End If

            dblRetPcs = 0
            dblRetPcsB = 0
            dblRetCts = 0
            dblRetEffPcs = 0
            If blnFound = True Then
                If cmbSection.SelectedIndex + 1 > 1 Then
                    If cmbSection.SelectedIndex + 1 = 5 Then
                        intPrevSec = 3
                    Else
                        intPrevSec = cmbSection.SelectedIndex
                    End If

                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT SUM(RetPcsT) AS RetPcsT,SUM(RetPcsB) AS RetPcsB,ROUND(SUM(RetCts), 3) AS RetCts, SUM(RejPcs + LostPcs - ExtPcs) AS EffPcs FROM tblMixReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & intPrevSec & "", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        dblRetPcs = rsComSql.Fields("RetPcsT").Value
                        dblRetPcsB = rsComSql.Fields("RetPcsB").Value
                        dblRetEffPcs = rsComSql.Fields("EffPcs").Value
                        dblRetCts = rsComSql.Fields("RetCts").Value
                        blnFound = True

                        If dblIssPcs <> dblRetPcs + dblRetPcsB + dblRetEffPcs Then
                            MsgBox("Invalid Returns", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : cmdParPkt.Focus() : Exit Sub
                        End If
                    Else
                        blnFound = False
                    End If
                    rsComSql = Nothing
                    If blnFound = False Then cmdParPkt.Focus() : Exit Sub
                Else
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT PktPcs AS RetPcs, PktCts AS RetCts FROM tblMixPacket WHERE PktOrdNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        dblRetPcs = rsComSql.Fields("RetPcs").Value
                        dblRetCts = rsComSql.Fields("RetCts").Value
                        blnFound = True
                    Else
                        blnFound = False
                    End If
                    rsComSql = Nothing
                    If blnFound = False Then MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : cmdParPkt.Focus() : Exit Sub
                End If
            End If

            If blnFound = True Then
                flxDetails.Rows.Add(ParcelNo,
                                    PacketNo,
                                    dblRetPcs,
                                    dblRetPcsB,
                                    Math.Round(dblRetCts, 3),
                                    strGroup,
                                    Math.Round(dblPktCts, 3))

                'txtTotPcs.Text = CDbl(txtTotPcs.Text) + dblRetPcs
                'txtTotCts.Text = Math.Round(CDbl(txtTotCts.Text) + dblRetCts, 3)
                'txtCount.Text = CDbl(txtCount.Text) + 1

                txtTotPcs.Text = CalTotalPcs(flxDetails)
                txtTotCts.Text = CalTotalCts(flxDetails)
                txtRghCts.Text = CalTotalRghCts(flxDetails)
                txtCount.Text = flxDetails.Rows.Count

                cmdParPkt.Focus()
            End If
        End If
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

    Private Sub Save()
        Dim intRow As Integer
        Dim mFlow As String
        Dim intSecCount As Integer

        If cmbSection.Text = "" Then MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbSection.SelectedIndex + 1 > 18 Then MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If txtEmp.Text = "" Then MsgBox("Invalid Emp No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        txtEmp.Text = UCase(txtEmp.Text)
        rsComSql = New ADODB.Recordset
        If cmbSection.SelectedIndex + 1 > 14 Then
            rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL3 WHERE FullEmpNo = '" & txtEmp.Text & "' AND Pay = 1", AdoCN, 1, 1)
        Else
            rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL3 WHERE FullEmpNo = '" & txtEmp.Text & "' AND Pay = 1", AdoCN, 1, 1)
            'rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL5 WHERE FullEmpNo = '" & txtEmp.Text & "' AND CATEGORY <> 'LEADERS' AND CATEGORY <> 'SUPERVISORY' AND Pay = 1", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Employee No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        'rsComSql_2 = New ADODB.Recordset
        'rsComSql_2.Open("SELECT EmpNo FROM tblMixReturns WHERE EmpNo = '" & txtEmp.Text & "' AND LostPcs > 0 AND Active = 1", AdoCN, 1, 1)
        'If rsComSql_2.RecordCount Then
        '    MsgBox("Have a Lost to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '    Exit Sub
        'End If
        'rsComSql_2 = Nothing

        rsComSql_2 = New ADODB.Recordset
        rsComSql_2.Open("SELECT EmpNo FROM VW_ALLLostEmpNo WHERE EmpNo = '" & txtEmp.Text & "'", dbConn, 1, 1)
        If rsComSql_2.RecordCount Then
            MsgBox("Have a Lost to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql_2 = Nothing

        If intCheckIssDate = 1 Then
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT FullEmpNo, CATEGORY, Pay FROM VW_EMP_MASTER_SMALL3 WHERE (Pay = 1) AND (FullEmpNo = '" & txtEmp.Text & "')", AdoCN, 1, 1)
            'rsComSql_1.Open("SELECT FullEmpNo, CATEGORY, Pay FROM VW_EMP_MASTER_SMALL2 WHERE (CATEGORY LIKE 'DIRECT%' OR CATEGORY LIKE 'TEMP%') AND (Pay = 1) AND (FullEmpNo = '" & txtEmp.Text & "')", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT TOP (100) PERCENT DATEDIFF(d, dbo.tblMixIssues.IssDate, GETDATE()) AS Days " & _
                                "FROM dbo.tblMixIssues INNER JOIN dbo.tblOrders ON dbo.tblMixIssues.ParNo = dbo.tblOrders.OrderNo LEFT OUTER JOIN " & _
                                    "dbo.tblMixReturns ON dbo.tblMixIssues.ParNo = dbo.tblMixReturns.ParNo AND dbo.tblMixIssues.PktNo = dbo.tblMixReturns.PktNo AND dbo.tblMixIssues.Sec = dbo.tblMixReturns.Sec " & _
                                "WHERE (dbo.tblMixReturns.PktNo IS NULL) AND (DATEDIFF(d, dbo.tblMixIssues.IssDate, GETDATE()) > '" & intDelayDays & "') AND (dbo.tblOrders.Complete = N'N') AND (dbo.tblMixIssues.EmpNo = '" & txtEmp.Text & "')", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    MsgBox("Have a Delayed Packet to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql_2 = Nothing
            End If
            rsComSql_1 = Nothing
        End If

        For intRow = 0 To flxDetails.Rows.Count - 1
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT PktFlow FROM tblMixPacket WHERE PktOrdNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                mFlow = rsComSql.Fields("PktFlow").Value

                intSecCount = cmbSection.SelectedIndex + 1
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT SecCount, SecCount2 FROM tblMixSections WHERE SecCode = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If mFlow = "Polish" Then
                        intSecCount = rsComSql_1.Fields("SecCount2").Value
                    Else
                        intSecCount = rsComSql_1.Fields("SecCount").Value
                    End If

                End If
                rsComSql_1 = Nothing

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT PktNo FROM tblMixIssues WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                If rsComSql_1.RecordCount = 0 Then
                    AdoCN.Execute("INSERT INTO tblMixIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                  "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & cmbSection.SelectedIndex + 1 & "','" & mFlow & "','" & intSecCount & "','" & Trim(txtEmp.Text) & "','" & CDbl(flxDetails.Item(2, intRow).Value) & "','" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & CSng(flxDetails.Item(4, intRow).Value) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & PBUser_EmpNo & "')")

                    AdoCN.Execute("INSERT INTO tblMixReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,RejStatus,RejReason,Groove,GrPcs,IncPcs,ChkEmpNo,DoneBy) " & _
                                  "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & mFlow & "'," & intSecCount & "," & cmbSection.SelectedIndex + 1 & ",'" & Trim(txtEmp.Text) & "'," & CInt(flxDetails.Item(2, intRow).Value) & "," & CInt(flxDetails.Item(3, intRow).Value) & "," & CSng(flxDetails.Item(4, intRow).Value) & ",0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0,0,0,0,'',0,0,0,'','" & PBUser_EmpNo & "')")
                End If
                rsComSql_1 = Nothing

            End If
            rsComSql = Nothing
        Next

        MsgBox("Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        ClearText()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            Save()
        End If
    End Sub

    Private Sub cmbSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSection.SelectedIndexChanged
        flxDetails.Rows.Clear()
        txtTotPcs.Text = CalTotalPcs(flxDetails)
        txtTotCts.Text = CalTotalCts(flxDetails)
        txtCount.Text = flxDetails.Rows.Count
    End Sub

    Private Sub cmdEmp_Click(sender As Object, e As EventArgs) Handles cmdEmp.Click
        pnlEmp.Visible = True
        txtEmp2.Text = ""
        txtEmp2.Focus()
    End Sub

    Private Sub txtEmp2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEmp2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If CheckEmployee(Trim(txtEmp2.Text)) = True Then
                Datavalid = True
                txtEmp.Text = UCase(Trim(txtEmp2.Text))
            Else
                MsgBox("Invalid IC No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Datavalid = False
                txtEmp.Text = ""
                txtEmp2.Focus()
                Exit Sub
            End If
            txtEmp.Text = txtEmp2.Text
            pnlEmp.Visible = False
        End If
    End Sub

    Private Sub txtEmp2_KeyUp(sender As Object, e As KeyEventArgs) Handles txtEmp2.KeyUp
        Dim TimeDiff As Integer
        Dim TimeDiff1 As TimeSpan

        If Asc(e.KeyCode) <> 9 And Asc(e.KeyCode) <> 13 Then
            If PBUser_EmpNo <> "D06975" Then
                If FirstInput = Nothing Then
                    FirstInput = Now()
                Else
                    'TimeDiff = DateDiff(DateInterval.Second, FirstInput, Now())
                    TimeDiff1 = Now() - FirstInput
                    TimeDiff = TimeDiff1.Milliseconds
                End If

                If TimeDiff > 600 Then
                    MsgBox("Please use the Barcode scanner", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                    txtEmp.Text = ""
                    FirstInput = Nothing
                    pnlEmp.Visible = False
                    cmdEmp.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub cmdEmpCancel_Click(sender As Object, e As EventArgs) Handles cmdEmpCancel.Click
        txtEmp2.Text = ""
        pnlEmp.Visible = False
    End Sub
End Class