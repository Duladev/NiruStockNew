
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixIssueEntry
    Dim ParcelNo As String
    Dim PacketNo As String

    Private Sub ClearText()
        cmbSection.Text = ""
        flxDetails.Rows.Clear()
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0.000"
        txtEmp.Text = ""
        txtCount.Text = "0"
        chkLaser.Checked = False
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

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearText()
    End Sub

    Private Sub Load_Section()
        Dim rsSection As ADODB.Recordset

        cmbSection.Items.Clear()
        rsSection = New ADODB.Recordset
        rsSection.Open("SELECT * FROM tblMixSections WHERE SecCode <= 7 ORDER BY SecCode", AdoCN, 1, 1)
        If rsSection.RecordCount Then
            rsSection.MoveFirst()
            While Not rsSection.EOF
                cmbSection.Items.Add(rsSection.Fields("SecName").Value)
                rsSection.MoveNext()
            End While
        End If
        rsSection = Nothing
        cmbSection.SelectedIndex = 0
    End Sub

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Dim intRow As Integer
        Dim blnFound As Boolean
        Dim dblIssPcs As Double
        Dim dblRetPcs As Double
        Dim dblRetPcsB As Double
        Dim dblRetEffPcs As Double
        Dim dblRetCts As Double
        Dim strGroup As String
        Dim strFlow As String
        Dim intPrevSec As Integer

        Dim intRejectRep As Integer
        Dim dblPktPcs As Double
        Dim dblPktCts As Double

        If cmbSection.Text = "" Then MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbSection.SelectedIndex + 1 > 7 Then MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

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

            dblPktPcs = 0
            dblPktCts = 0
            strGroup = ""
            strFlow = ""
            dblIssPcs = 0
            intRejectRep = 0
            intPrevSec = cmbSection.SelectedIndex
            blnFound = False
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT PktOrdNo,Grp,PktPcs,PktCts,PktFlow,RejectRep FROM tblMixPacket WHERE PktOrdNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Ok = 1 AND Accept = 1", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                strGroup = rsComSql.Fields("Grp").Value
                strFlow = rsComSql.Fields("PktFlow").Value
                intRejectRep = rsComSql.Fields("RejectRep").Value
                dblPktPcs = rsComSql.Fields("PktPcs").Value
                dblPktCts = rsComSql.Fields("PktCts").Value
                blnFound = True
            Else
                blnFound = False
            End If
            rsComSql = Nothing
            If blnFound = False Then MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : cmdParPkt.Focus() : Exit Sub

            If strGroup = "" Then
                blnFound = False
            Else
                blnFound = True
            End If
            If blnFound = False Then MsgBox("Invalid Group", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : cmdParPkt.Focus() : Exit Sub

            If cmbSection.SelectedIndex + 1 = 3 Then
                If intRejectRep = 1 Then
                    blnFound = False
                Else
                    blnFound = True
                End If
                'rsComSql = New ADODB.Recordset
                'rsComSql.Open("SELECT PktOrdNo FROM tblMixPacket WHERE PktOrdNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND RejectRep = 1", AdoCN, 1, 1)
                'If rsComSql.RecordCount Then
                '    blnFound = False
                'Else
                '    blnFound = True
                'End If
                'rsComSql = Nothing
                If blnFound = False Then MsgBox("Invalid Packet (Opening)", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : cmdParPkt.Focus() : Exit Sub
            End If

            If blnFound = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SecName FROM tblMixSections WHERE SecCode = " & cmbSection.SelectedIndex + 1 & " AND SecName <> 'No'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                rsComSql = Nothing
                If blnFound = False Then cmdParPkt.Focus() : Exit Sub
            End If

            If blnFound = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT ParNo FROM tblMixIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
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
                    If cmbSection.SelectedIndex + 1 = 5 Then
                        intPrevSec = 3
                    Else
                        intPrevSec = cmbSection.SelectedIndex
                    End If
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
            dblRetEffPcs = 0
            dblRetCts = 0
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
                        If dblRetPcs + dblRetPcsB = 0 Then
                            MsgBox("Invalid Returns", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : cmdParPkt.Focus() : Exit Sub
                        End If
                    Else
                        blnFound = False
                    End If
                    rsComSql = Nothing
                    If blnFound = False Then cmdParPkt.Focus() : Exit Sub
                Else
                    dblRetPcs = dblPktPcs
                    dblRetCts = dblPktCts

                    'rsComSql = New ADODB.Recordset
                    'rsComSql.Open("SELECT PktPcs AS RetPcs, PktCts AS RetCts FROM tblMixPacket WHERE PktOrdNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "'", AdoCN, 1, 1)
                    'If rsComSql.RecordCount Then
                    '    dblRetPcs = rsComSql.Fields("RetPcs").Value
                    '    dblRetCts = rsComSql.Fields("RetCts").Value
                    '    blnFound = True
                    'Else
                    '    blnFound = False
                    'End If
                    'rsComSql = Nothing
                    'If blnFound = False Then MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : cmdParPkt.Focus() : Exit Sub
                End If
            End If

            If blnFound = True Then
                flxDetails.Rows.Add(ParcelNo,
                                    PacketNo,
                                    dblRetPcs,
                                    dblRetPcsB,
                                    Math.Round(dblRetCts, 3),
                                    strGroup,
                                    strFlow)

                txtTotPcs.Text = CalTotalPcs(flxDetails)
                txtTotCts.Text = CalTotalCts(flxDetails)
                txtCount.Text = flxDetails.Rows.Count

                cmdParPkt.Focus()
            End If
        End If
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim mFlow As String
        Dim intSecCount As Integer
        Dim intLaser As Integer

        If cmbSection.Text = "" Then MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbSection.SelectedIndex + 1 > 7 Then MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If txtEmp.Text = "" Then MsgBox("Invalid Emp No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        txtEmp.Text = UCase(txtEmp.Text)
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & txtEmp.Text & "' AND Pay = 1", AdoCN, 1, 1)
        'If cmbSection.SelectedIndex + 1 > 14 Then
        '    rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & txtEmp.Text & "' AND Pay = 1", AdoCN, 1, 1)
        'Else
        '    rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & txtEmp.Text & "' AND Pay = 1", AdoCN, 1, 1)
        '    'rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & txtEmp.Text & "' AND CATEGORY <> 'LEADERS' AND CATEGORY <> 'SUPERVISORY' AND Pay = 1", AdoCN, 1, 1)
        'End If
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Employee No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

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
            'rsComSql_1 = New ADODB.Recordset
            'rsComSql_1.Open("SELECT FullEmpNo, CATEGORY, Pay FROM VW_EMP_MASTER_SMALL2 WHERE (Pay = 1) AND (FullEmpNo = '" & txtEmp.Text & "')", AdoCN, 1, 1)
            ''rsComSql_1.Open("SELECT FullEmpNo, CATEGORY, Pay FROM VW_EMP_MASTER_SMALL2 WHERE (CATEGORY LIKE 'DIRECT%' OR CATEGORY LIKE 'TEMP%') AND (Pay = 1) AND (FullEmpNo = '" & txtEmp.Text & "')", AdoCN, 1, 1)
            'If rsComSql_1.RecordCount Then

            'End If
            'rsComSql_1 = Nothing

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

        If cmbSection.Text = "Groove" Then
            If chkLaser.Checked = True Then
                intLaser = 1
            Else
                intLaser = 0
            End If
        Else
            intLaser = 0
        End If

        For intRow = 0 To flxDetails.Rows.Count - 1
            mFlow = flxDetails.Item(6, intRow).Value

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
                AdoCN.Execute("INSERT INTO tblMixIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy,Laser) " & _
                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & cmbSection.SelectedIndex + 1 & "','" & mFlow & "','" & intSecCount & "','" & Trim(txtEmp.Text) & "'," & _
                                "'" & CDbl(flxDetails.Item(2, intRow).Value) & "','" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & CSng(flxDetails.Item(4, intRow).Value) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & PBUser_EmpNo & "'," & intLaser & ")")

            End If
            rsComSql_1 = Nothing

            'rsComSql = New ADODB.Recordset
            'rsComSql.Open("SELECT PktFlow FROM tblMixPacket WHERE PktOrdNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
            'If rsComSql.RecordCount Then


            'End If
            'rsComSql = Nothing
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

    Private Sub flxDetails_DoubleClick(sender As Object, e As EventArgs) Handles flxDetails.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)
            txtTotPcs.Text = CalTotalPcs(flxDetails)
            txtTotCts.Text = CalTotalCts(flxDetails)
            txtCount.Text = flxDetails.RowCount
        End If
    End Sub

    Private Sub cmbSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSection.SelectedIndexChanged
        flxDetails.Rows.Clear()
    End Sub

    Private Sub frm_MixIssueEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        ClearText()
        Load_Section()
    End Sub

    Private Sub cmdEmp_Click(sender As Object, e As EventArgs) Handles cmdEmp.Click
        Datavalid = False
        Parcel = False
        Instring = UCase(InputBox("Enter Emp No"))
        ParcelLen = Len(Instring)
        If ParcelLen = 6 Then
            Datavalid = True

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL2 WHERE (Pay = 1) AND (FullEmpNo = '" & Trim(Instring) & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                cmdEmp.Focus()
                Exit Sub
            End If
            rsComSql = Nothing
            ICNo = UCase(Trim(Instring))
            txtEmp.Text = ICNo
        Else
            MsgBox("Invalid IC No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Datavalid = False
            ICNo = ""
            cmdEmp.Focus()
            Exit Sub
        End If
    End Sub
End Class