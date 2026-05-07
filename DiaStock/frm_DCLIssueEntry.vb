
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLIssueEntry
    Dim ParcelNo As String
    Dim PacketNo As String
    Dim FirstInput As Date

    Private Sub frm_DCLIssueEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        cmbDept.Items.Clear()
        cmbDept.Items.Add("Asscher")
        cmbDept.Items.Add("Baguettes")
        cmbDept.Items.Add("Davinci")
        cmbDept.Items.Add("Emerald")
        cmbDept.Items.Add("Opening")
        cmbDept.Items.Add("Princess")
        cmbDept.Items.Add("Rounds")
        cmbDept.Items.Add("Precision")

        ClearText()
    End Sub

    Private Sub ClearText()
        cmbSection.Text = ""
        flxDetails.Rows.Clear()
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0.000"
        txtEmp.Text = ""
        txtCount.Text = "0"
        txtGroup.Text = ""
        txtRghCts.Text = "0.000"
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
            CalTotalRghCts = CalTotalRghCts + Val(flxSample.Item(7, intRow).Value)
        Next
        CalTotalRghCts = Math.Round(CalTotalRghCts, 3)
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
        Select Case cmbDept.Text
            Case "Rounds"
                rsSection.Open("SELECT * FROM tblRndSections ORDER BY SecCode", AdoCN, 1, 1)
            Case "Baguettes"
                rsSection.Open("SELECT * FROM tblBAGSections ORDER BY SecCode", AdoCN, 1, 1)
            Case "Davinci", "Emerald", "Opening", "Asscher"
                rsSection.Open("SELECT * FROM tblExtSections WHERE Department = '" & cmbDept.Text & "' ORDER BY SecCode", AdoCN, 1, 1)
            Case "Princess"
                rsSection.Open("SELECT * FROM tblPRSections ORDER BY SecCode", AdoCN, 1, 1)
            Case "Precision"
                rsSection.Open("SELECT * FROM tblSections2 WHERE Flow = 'RndSize' ORDER BY SecCode", AdoCN, 1, 1)
        End Select
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

    Private Sub cmbDept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDept.SelectedIndexChanged
        If cmbDept.Text <> "" Then
            Load_Section()
            flxDetails.Rows.Clear()
            txtTotPcs.Text = CalTotalPcs(flxDetails)
            txtTotCts.Text = CalTotalCts(flxDetails)
            txtRghCts.Text = CalTotalRghCts(flxDetails)
            txtCount.Text = flxDetails.Rows.Count
        End If
    End Sub

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Dim intRow As Integer
        Dim blnFound As Boolean
        Dim dblIssPcs As Double
        Dim dblRetPcs As Double
        Dim dblRetPcsB As Double
        Dim dblRetEffPcs As Double
        Dim dblRetCts As Double
        Dim dblPktCts As Double
        Dim strGroup As String
        Dim strUnit As String

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbSection.Text = "" Then MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbSection.SelectedIndex + 1 > 25 Then MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        Datavalid = False
        Parcel = True
        Instring = UCase(InputBox("Enter Par/Pkt Number"))
        ParcelLen = Len(Instring)
        If ParcelLen >= 8 Then
            Datavalid = True

            Select Case cmbDept.Text
                Case "Rounds"
                    If ParcelLen = 11 Then
                        ParcelNo = Mid(Instring, 1, ParcelLen - 3)
                        PacketNo = strRight(Instring, 3)
                    Else
                        ParcelNo = Mid(Instring, 1, ParcelLen - 4)
                        PacketNo = strRight(Instring, 4)
                    End If
                Case "Baguettes"
                    ParcelNo = Mid(Instring, 1, ParcelLen - 4)
                    PacketNo = strRight(Instring, 4)
                Case "Davinci", "Emerald", "Opening", "Asscher"
                    If ParcelLen = 10 Then
                        ParcelNo = Mid(Instring, 1, ParcelLen - 3)
                        PacketNo = strRight(Instring, 3)
                    Else
                        ParcelNo = Mid(Instring, 1, ParcelLen - 4)
                        PacketNo = strRight(Instring, 4)
                    End If
                Case "Princess", "Precision"
                    ParcelNo = Mid(Instring, 1, ParcelLen - 3)
                    PacketNo = strRight(Instring, 3)
            End Select

        End If

        If Datavalid = True Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(0, intRow).Value = ParcelNo And flxDetails.Item(1, intRow).Value = PacketNo Then
                    MsgBox("Already Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    cmdParPkt.Focus()
                    Exit Sub
                End If
            Next

            Select Case cmbDept.Text
                Case "Rounds"
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT ParNo FROM tblDCLBlockPar WHERE ParNo = '" & ParcelNo & "' AND Department = 'Rounds'", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        MsgBox("This Parcel is Blocked", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql_2 = Nothing
            End Select

            strGroup = ""
            strUnit = ""
            dblIssPcs = 0
            dblPktCts = 0
            blnFound = False
            rsComSql = New ADODB.Recordset
            Select Case cmbDept.Text
                Case "Rounds"
                    rsComSql.Open("SELECT ParNo,Grp,IncUnit,PktCts FROM tblRndPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND DelDate IS NOT NULL AND AccDate IS NOT NULL", AdoCN, 1, 1)
                Case "Baguettes"
                    rsComSql.Open("SELECT ParNo,Grp,PktCts FROM tblBAGPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND DelDate IS NOT NULL AND AccDate IS NOT NULL", AdoCN, 1, 1)
                Case "Davinci", "Emerald", "Opening", "Asscher"
                    rsComSql.Open("SELECT ParNo,Grp,PktCts FROM tblExtPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND DelDate IS NOT NULL AND AccDate IS NOT NULL AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                Case "Princess"
                    rsComSql.Open("SELECT ParNo,'' AS Grp,PktCts FROM tblPRPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND DelDate IS NOT NULL AND AccDate IS NOT NULL", AdoCN, 1, 1)
                Case "Precision"
                    rsComSql.Open("SELECT PktOrdNo AS ParNo,Grp,PktCts FROM tblPacket WHERE PktOrdNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "'", AdoCN, 1, 1)
            End Select
            If rsComSql.RecordCount Then
                If cmbDept.Text = "Rounds" Then
                    strUnit = rsComSql.Fields("IncUnit").Value
                End If
                strGroup = rsComSql.Fields("Grp").Value
                dblPktCts = rsComSql.Fields("PktCts").Value
                blnFound = True
            Else
                blnFound = False
            End If
            rsComSql = Nothing
            If blnFound = False Then MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : cmdParPkt.Focus() : Exit Sub

            If blnFound = True Then
                rsComSql = New ADODB.Recordset
                Select Case cmbDept.Text
                    Case "Rounds"
                        rsComSql.Open("SELECT SecName FROM tblRndSections WHERE SecCode = " & cmbSection.SelectedIndex + 1 & " AND SecName <> 'No'", AdoCN, 1, 1)
                    Case "Baguettes"
                        rsComSql.Open("SELECT SecName FROM tblBAGSections WHERE SecCode = " & cmbSection.SelectedIndex + 1 & " AND SecName <> 'No'", AdoCN, 1, 1)
                    Case "Davinci", "Emerald", "Opening", "Asscher"
                        rsComSql.Open("SELECT SecName FROM tblExtSections WHERE SecCode = " & cmbSection.SelectedIndex + 1 & " AND SecName <> 'No' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                    Case "Princess"
                        rsComSql.Open("SELECT SecName FROM tblPRSections WHERE SecCode = " & cmbSection.SelectedIndex + 1 & " AND SecName <> 'No'", AdoCN, 1, 1)
                    Case "Precision"
                        rsComSql.Open("SELECT SecName FROM tblSections2 WHERE SecCode = " & cmbSection.SelectedIndex + 1 & " AND SecName <> 'No' AND Flow = 'RndSize'", AdoCN, 1, 1)
                End Select
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
                Select Case cmbDept.Text
                    Case "Rounds"
                        rsComSql.Open("SELECT ParNo FROM tblRndIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                    Case "Baguettes"
                        rsComSql.Open("SELECT ParNo FROM tblBAGIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                    Case "Davinci", "Emerald", "Opening", "Asscher"
                        rsComSql.Open("SELECT ParNo FROM tblExtIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & " AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                    Case "Princess"
                        rsComSql.Open("SELECT ParNo FROM tblPRIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                    Case "Precision"
                        rsComSql.Open("SELECT ParNo FROM tblIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                End Select
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
                    Select Case cmbDept.Text
                        Case "Rounds"
                            rsComSql.Open("SELECT ParNo, IssPcsT + IssPcsB AS IssPcs FROM tblRndIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex & "", AdoCN, 1, 1)
                        Case "Baguettes"
                            rsComSql.Open("SELECT ParNo, IssPcsT + IssPcsB AS IssPcs FROM tblBAGIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex & "", AdoCN, 1, 1)
                        Case "Davinci", "Opening", "Asscher"
                            rsComSql.Open("SELECT ParNo, IssPcsT + IssPcsB AS IssPcs FROM tblExtIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex & " AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                        Case "Emerald"
                            If cmbSection.SelectedIndex + 1 = 10 Then
                                rsComSql.Open("SELECT ParNo, IssPcsT + IssPcsB AS IssPcs FROM tblExtIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = 7 AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                            Else
                                rsComSql.Open("SELECT ParNo, IssPcsT + IssPcsB AS IssPcs FROM tblExtIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex & " AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                            End If
                        Case "Princess"
                            rsComSql.Open("SELECT ParNo, IssPcsC + IssPcsP AS IssPcs FROM tblPRIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex & "", AdoCN, 1, 1)
                        Case "Precision"
                            If cmbSection.SelectedIndex + 1 = 14 Then
                                rsComSql.Open("SELECT ParNo, IssPcsT + IssPcsB AS IssPcs FROM tblIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = 14", AdoCN, 1, 1)
                            Else
                                rsComSql.Open("SELECT ParNo, IssPcsT + IssPcsB AS IssPcs FROM tblIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex & "", AdoCN, 1, 1)
                            End If
                    End Select
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
                    rsComSql = New ADODB.Recordset
                    Select Case cmbDept.Text
                        Case "Rounds"
                            rsComSql.Open("SELECT SUM(RetPcsT) AS RetPcsT,SUM(RetPcsB) AS RetPcsB,ROUND(SUM(RetCts), 3) AS RetCts, SUM(RejPcs + LostPcs - ExtPcs + MacPcs) AS EffPcs FROM tblRndReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex & "", AdoCN, 1, 1)
                        Case "Baguettes"
                            rsComSql.Open("SELECT SUM(RetPcsT) AS RetPcsT,SUM(RetPcsB - PCUPcs) AS RetPcsB,ROUND(SUM(RetCts - PCUPCts), 3) AS RetCts, SUM(RejPcs + LostPcs - ExtPcs + PCUPcs) AS EffPcs FROM tblBAGReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex & "", AdoCN, 1, 1)
                        Case "Davinci", "Opening", "Asscher"
                            rsComSql.Open("SELECT SUM(RetPcsT) AS RetPcsT,SUM(RetPcsB) AS RetPcsB,ROUND(SUM(RetCts), 3) AS RetCts, SUM(RejPcs + LostPcs - ExtPcs + MacPcs) AS EffPcs FROM tblExtReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex & " AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                        Case "Emerald"
                            If cmbSection.SelectedIndex + 1 = 10 Then
                                rsComSql.Open("SELECT SUM(RetPcsT) AS RetPcsT,SUM(RetPcsB) AS RetPcsB,ROUND(SUM(RetCts), 3) AS RetCts, SUM(RejPcs + LostPcs - ExtPcs + MacPcs) AS EffPcs FROM tblExtReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = 7 AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                            Else
                                rsComSql.Open("SELECT SUM(RetPcsT) AS RetPcsT,SUM(RetPcsB) AS RetPcsB,ROUND(SUM(RetCts), 3) AS RetCts, SUM(RejPcs + LostPcs - ExtPcs + MacPcs) AS EffPcs FROM tblExtReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex & " AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                            End If
                        Case "Princess"
                            rsComSql.Open("SELECT SUM(RetPcsC) AS RetPcsT,SUM(RetPcsP) AS RetPcsB,ROUND(SUM(RetCtsC + RetCtsP - PCUPCts), 3) AS RetCts, SUM(RejPcs + LostPcs - ExtPcs + PCUPcs) AS EffPcs FROM tblPRReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex & "", AdoCN, 1, 1)
                        Case "Precision"
                            If cmbSection.SelectedIndex + 1 = 14 Then
                                rsComSql.Open("SELECT SUM(RetPcsT) AS RetPcsT,SUM(RetPcsB) AS RetPcsB,ROUND(SUM(RetCts), 3) AS RetCts, SUM(RejPcs + LostPcs - ExtPcs) AS EffPcs FROM tblReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = 12", AdoCN, 1, 1)
                            Else
                                rsComSql.Open("SELECT SUM(RetPcsT) AS RetPcsT,SUM(RetPcsB) AS RetPcsB,ROUND(SUM(RetCts), 3) AS RetCts, SUM(RejPcs + LostPcs - ExtPcs) AS EffPcs FROM tblReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex & "", AdoCN, 1, 1)
                            End If
                    End Select
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
                    Select Case cmbDept.Text
                        Case "Rounds"
                            rsComSql.Open("SELECT PktPcs AS RetPcs, PktCts AS RetCts FROM tblRndPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "'", AdoCN, 1, 1)
                        Case "Baguettes"
                            rsComSql.Open("SELECT PktPcs AS RetPcs, PktCts AS RetCts FROM tblBAGPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "'", AdoCN, 1, 1)
                        Case "Davinci", "Emerald", "Opening", "Asscher"
                            rsComSql.Open("SELECT PktPcs AS RetPcs, PktCts AS RetCts FROM tblExtPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                        Case "Princess"
                            rsComSql.Open("SELECT PktPcs AS RetPcs, PktCts AS RetCts FROM tblPRPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "'", AdoCN, 1, 1)
                        Case "Precision"
                            rsComSql.Open("SELECT PktPcs AS RetPcs, PktCts AS RetCts FROM tblPacket WHERE PktOrdNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "'", AdoCN, 1, 1)
                    End Select
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
                                    strUnit,
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

    Private Sub Save()
        Dim intRow As Integer
        Dim mFlow As String
        Dim intSecCount As Integer
        Dim intGrdTrf As Integer

        intGrdTrf = 0
        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbSection.Text = "" Then MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbSection.SelectedIndex + 1 > 25 Then MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If cmbDept.Text = "Rounds" Then
            If cmbSection.SelectedIndex + 1 = 15 Or cmbSection.SelectedIndex + 1 = 16 Or cmbSection.SelectedIndex + 1 = 17 Or cmbSection.SelectedIndex + 1 = 18 Then
                If txtGroup.Text = "" Then MsgBox("Invalid Group", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
                If UCase(txtGroup.Text) = "A" Or UCase(txtGroup.Text) = "B" Or UCase(txtGroup.Text) = "C" Or UCase(txtGroup.Text) = "D" Or UCase(txtGroup.Text) = "E" Or UCase(txtGroup.Text) = "R" Or UCase(txtGroup.Text) = "I" Or UCase(txtGroup.Text) = "S" Then

                Else
                    MsgBox("Invalid Group", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
        End If

        If cmbDept.Text = "Opening" Then
            If cmbSection.SelectedIndex + 1 = 1 Or cmbSection.SelectedIndex + 1 = 2 Or cmbSection.SelectedIndex + 1 = 3 Or cmbSection.SelectedIndex + 1 = 4 Then
                If txtGroup.Text <> "" Then
                    If UCase(txtGroup.Text) = "D" Or UCase(txtGroup.Text) = "K" Or UCase(txtGroup.Text) = "L" Then

                    Else
                        MsgBox("Invalid Group", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If
            End If
        End If

        'If cmbDept.Text = "Emerald" Then
        '    If cmbSection.SelectedIndex + 1 = 1 Or cmbSection.SelectedIndex + 1 = 2 Or cmbSection.SelectedIndex + 1 = 3 Or cmbSection.SelectedIndex + 1 = 4 Then
        '        If txtGroup.Text <> "" Then
        '            If UCase(txtGroup.Text) = "D" Or UCase(txtGroup.Text) = "K" Or UCase(txtGroup.Text) = "L" Then

        '            Else
        '                MsgBox("Invalid Group", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '                Exit Sub
        '            End If
        '        End If
        '    End If
        'End If

        If txtEmp.Text = "" Then MsgBox("Invalid Emp No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        txtEmp.Text = UCase(txtEmp.Text)
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL3 WHERE FullEmpNo = '" & txtEmp.Text & "' AND Pay = 1", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Employee No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        dtpToday = GetToday()

        rsComSql_2 = New ADODB.Recordset
        rsComSql_2.Open("SELECT EmpNo FROM VW_ALLLostEmpNo WHERE EmpNo = '" & txtEmp.Text & "'", AdoCN, 1, 1)
        If rsComSql_2.RecordCount Then
            MsgBox("Have a Lost to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql_2 = Nothing

        'Check Lost Pcs for the Issue Employee
        Select Case cmbDept.Text
            Case "Rounds"
                'rsComSql_2 = New ADODB.Recordset
                'rsComSql_2.Open("SELECT EmpNo FROM tblRndReturns WHERE EmpNo = '" & txtEmp.Text & "' AND LostPcs > 0 AND Active = 1", AdoCN, 1, 1)
                'If rsComSql_2.RecordCount Then
                '    MsgBox("Have a Lost to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                '    Exit Sub
                'End If
                'rsComSql_2 = Nothing

                If intCheckIssDate = 1 Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT FullEmpNo, CATEGORY, Pay FROM VW_EMP_MASTER_SMALL3 WHERE (CATEGORY LIKE 'DIRECT%' OR CATEGORY LIKE 'TEMP%') AND (Pay = 1) AND (FullEmpNo = '" & txtEmp.Text & "')", AdoCN, 1, 1)
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

            Case "Baguettes"
                'rsComSql_2 = New ADODB.Recordset
                'rsComSql_2.Open("SELECT EmpNo FROM tblBAGReturns WHERE EmpNo = '" & txtEmp.Text & "' AND LostPcs > 0 AND Active = 1", AdoCN, 1, 1)
                'If rsComSql_2.RecordCount Then
                '    MsgBox("Have a Lost to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                '    Exit Sub
                'End If
                'rsComSql_2 = Nothing

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

            Case "Davinci", "Emerald", "Opening", "Asscher"
                'rsComSql_2 = New ADODB.Recordset
                'rsComSql_2.Open("SELECT EmpNo FROM tblExtReturns WHERE EmpNo = '" & txtEmp.Text & "' AND LostPcs > 0 AND Active = 1 AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                'If rsComSql_2.RecordCount Then
                '    MsgBox("Have a Lost to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                '    Exit Sub
                'End If
                'rsComSql_2 = Nothing

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

            Case "Princess"
                'rsComSql_2 = New ADODB.Recordset
                'rsComSql_2.Open("SELECT EmpNo FROM tblPRReturns WHERE EmpNo = '" & txtEmp.Text & "' AND LostPcs > 0 AND Active = 1", AdoCN, 1, 1)
                'If rsComSql_2.RecordCount Then
                '    MsgBox("Have a Lost to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                '    Exit Sub
                'End If
                'rsComSql_2 = Nothing

                If intCheckIssDate = 1 Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT FullEmpNo, CATEGORY, Pay FROM VW_EMP_MASTER_SMALL5 WHERE (CATEGORY LIKE 'DIRECT%' OR CATEGORY LIKE 'TEMP%') AND (Pay = 1) AND (FullEmpNo = '" & txtEmp.Text & "')", AdoCN, 1, 1)
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

            Case "Precision"
                'rsComSql_2 = New ADODB.Recordset
                'rsComSql_2.Open("SELECT EmpNo FROM tblReturns WHERE EmpNo = '" & txtEmp.Text & "' AND LostPcs > 0 AND Active = 1", AdoCN, 1, 1)
                'If rsComSql_2.RecordCount Then
                '    MsgBox("Have a Lost to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                '    Exit Sub
                'End If
                'rsComSql_2 = Nothing

                If intCheckIssDate = 1 Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT FullEmpNo, CATEGORY, Pay FROM VW_EMP_MASTER_SMALL3 WHERE (Pay = 1) AND (FullEmpNo = '" & txtEmp.Text & "')", AdoCN, 1, 1)
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
        End Select

        For intRow = 0 To flxDetails.Rows.Count - 1
            rsComSql = New ADODB.Recordset
            Select Case cmbDept.Text
                Case "Rounds"
                    rsComSql.Open("SELECT PktFlow FROM tblRndPacket WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                Case "Baguettes"
                    rsComSql.Open("SELECT PktFlow FROM tblBAGPacket WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                Case "Davinci", "Emerald", "Opening", "Asscher"
                    rsComSql.Open("SELECT PktFlow FROM tblExtPacket WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                Case "Princess"
                    rsComSql.Open("SELECT PktFlow FROM tblPRPacket WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                Case "Precision"
                    rsComSql.Open("SELECT PktFlow FROM tblPacket WHERE PktOrdNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
            End Select
            If rsComSql.RecordCount Then
                mFlow = rsComSql.Fields("PktFlow").Value

                intSecCount = cmbSection.SelectedIndex + 1
                rsComSql_1 = New ADODB.Recordset
                Select Case cmbDept.Text
                    Case "Rounds"
                        rsComSql_1.Open("SELECT SecCount FROM tblRndSections WHERE SecCode = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                    Case "Baguettes"
                        rsComSql_1.Open("SELECT SecCount FROM tblBAGSections WHERE SecCode = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                    Case "Davinci", "Emerald", "Opening", "Asscher"
                        rsComSql_1.Open("SELECT SecCount FROM tblExtSections WHERE SecCode = " & cmbSection.SelectedIndex + 1 & " AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                    Case "Princess"
                        rsComSql_1.Open("SELECT SecCode AS SecCount FROM tblPRSections WHERE SecCode = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                    Case "Precision"
                        rsComSql_1.Open("SELECT SecCount FROM tblSections2 WHERE SecCode = " & cmbSection.SelectedIndex + 1 & " AND Flow = 'RndSize'", AdoCN, 1, 1)
                End Select
                If rsComSql_1.RecordCount Then
                    intSecCount = rsComSql_1.Fields("SecCount").Value
                End If
                rsComSql_1 = Nothing

                rsComSql_1 = New ADODB.Recordset
                Select Case cmbDept.Text
                    Case "Rounds"
                        rsComSql_1.Open("SELECT PktNo FROM tblRndIssues WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            mStrSQL = "INSERT INTO tblRndIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                      "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & cmbSection.SelectedIndex + 1 & "," & _
                                        "'" & mFlow & "'," & intSecCount & ",'" & Trim(txtEmp.Text) & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & _
                                        "" & CSng(flxDetails.Item(4, intRow).Value) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "')"

                            AdoCN.Execute(mStrSQL)

                            If cmbSection.SelectedIndex + 1 = 15 Or cmbSection.SelectedIndex + 1 = 16 Or cmbSection.SelectedIndex + 1 = 17 Or cmbSection.SelectedIndex + 1 = 18 Then
                                AdoCN.Execute("UPDATE tblRndPacket SET Grp = '" & UCase(txtGroup.Text) & "' WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'")
                            End If

                        End If

                    Case "Baguettes"
                        rsComSql_1.Open("SELECT PktNo FROM tblBAGIssues WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            intGrdTrf = 0
                            'If strRight(flxDetails.Item(0, intRow).Value, 1) = "C" And cmbSection.SelectedIndex + 1 = 4 Then
                            '    intGrdTrf = 1
                            'End If

                            mStrSQL = "INSERT INTO tblBAGIssues(OrderNo,ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy,GrdTrf) " & _
                                      "VALUES(1,'" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & cmbSection.SelectedIndex + 1 & "," & _
                                        "'" & mFlow & "'," & intSecCount & ",'" & Trim(txtEmp.Text) & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & _
                                        "" & CSng(flxDetails.Item(4, intRow).Value) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "'," & intGrdTrf & ")"

                            AdoCN.Execute(mStrSQL)
                        End If

                    Case "Davinci", "Emerald", "Opening", "Asscher"
                        rsComSql_1.Open("SELECT PktNo FROM tblExtIssues WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 & " AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            mStrSQL = "INSERT INTO tblExtIssues(Department,ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                      "VALUES('" & cmbDept.Text & "','" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & cmbSection.SelectedIndex + 1 & "," & _
                                        "'" & mFlow & "'," & intSecCount & ",'" & Trim(txtEmp.Text) & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & _
                                        "" & CSng(flxDetails.Item(4, intRow).Value) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "')"

                            AdoCN.Execute(mStrSQL)

                            If cmbDept.Text = "Opening" Or cmbDept.Text = "Emerald" Then
                                If txtGroup.Text <> "" Then
                                    If cmbSection.SelectedIndex + 1 = 1 Or cmbSection.SelectedIndex + 1 = 2 Or cmbSection.SelectedIndex + 1 = 3 Or cmbSection.SelectedIndex + 1 = 4 Then
                                        AdoCN.Execute("UPDATE tblExtPacket SET Grp = '" & UCase(txtGroup.Text) & "' WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Department = '" & cmbDept.Text & "'")
                                    End If
                                End If
                            End If
                        End If

                    Case "Princess"
                        rsComSql_1.Open("SELECT PktNo FROM tblPRIssues WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            mStrSQL = "INSERT INTO tblPRIssues(ParNo,PktNo,Flow,EmpNo,IssPcsC,IssPcsP,IssCtsP,IssCtsC,IssDate,IssTime,Sec,SecCount,DoneBy) " & _
                                      "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & mFlow & "','" & Trim(txtEmp.Text) & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & _
                                        "" & CDbl(flxDetails.Item(3, intRow).Value) & "," & CSng(flxDetails.Item(4, intRow).Value) & ",0,'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "'," & _
                                        "" & cmbSection.SelectedIndex + 1 & "," & intSecCount & ",'" & PBUser_EmpNo & "')"

                            AdoCN.Execute(mStrSQL)
                        End If

                    Case "Precision"
                        rsComSql_1.Open("SELECT PktNo FROM tblIssues WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            mStrSQL = "INSERT INTO tblIssues(ParNo,PktNo,Flow,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,Sec,SecCount) " & _
                                      "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & mFlow & "','" & Trim(txtEmp.Text) & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & CSng(flxDetails.Item(4, intRow).Value) & "," & _
                                        "'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "'," & cmbSection.SelectedIndex + 1 & "," & intSecCount & ")"

                            AdoCN.Execute(mStrSQL)
                        End If
                End Select
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

    Private Sub flxDetails_DoubleClick(sender As Object, e As EventArgs) Handles flxDetails.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)
            txtTotPcs.Text = CalTotalPcs(flxDetails)
            txtTotCts.Text = CalTotalCts(flxDetails)
            txtRghCts.Text = CalTotalRghCts(flxDetails)
            txtCount.Text = flxDetails.RowCount
        End If
    End Sub

    Private Sub cmbSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSection.SelectedIndexChanged
        flxDetails.Rows.Clear()
    End Sub

    Private Sub cmdEmp_Click(sender As Object, e As EventArgs) Handles cmdEmp.Click
        'Datavalid = False
        'Parcel = False
        'Instring = UCase(InputBox("Enter Emp No"))
        'ParcelLen = Len(Instring)
        'If ParcelLen = 6 Then
        '    Datavalid = True

        '    rsComSql = New ADODB.Recordset
        '    rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL2 WHERE (Pay = 1) AND (FullEmpNo = '" & Trim(Instring) & "')", AdoCN, 1, 1)
        '    If rsComSql.RecordCount = 0 Then
        '        MsgBox("Invalid Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '        cmdEmp.Focus()
        '        Exit Sub
        '    End If
        '    rsComSql = Nothing
        '    ICNo = UCase(Trim(Instring))
        '    txtEmp.Text = ICNo
        'Else
        '    MsgBox("Invalid IC No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '    Datavalid = False
        '    ICNo = ""
        '    cmdEmp.Focus()
        '    Exit Sub
        'End If

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
    End Sub

    Private Sub cmdEmpCancel_Click(sender As Object, e As EventArgs) Handles cmdEmpCancel.Click
        txtEmp2.Text = ""
        pnlEmp.Visible = False
    End Sub
End Class