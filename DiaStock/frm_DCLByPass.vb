
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLByPass
    Dim ParcelNo As String
    Dim PacketNo As String
    Dim FirstInput As Date

    Private Sub frm_RndEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        cmbDept.Text = ""
        cmbDept.Items.Add("Baguettes")
        cmbDept.Items.Add("Emerald")
        cmbDept.Items.Add("Opening")
        cmbDept.Items.Add("Rounds")
        cmbDept.Items.Add("Precision")
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
            Case "Davinci", "Opening", "Emerald"
                rsSection.Open("SELECT * FROM tblExtSections WHERE Department = '" & cmbDept.Text & "' ORDER BY SecCode", AdoCN, 1, 1)
            Case "Precision"
                rsSection.Open("SELECT * FROM tblSections2 WHERE Flow = 'RndSize' ORDER BY SecCode", AdoCN, 1, 1)
        End Select
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
        txtGroup.Text = ""
        chkFinish.Checked = False
        chkPrepFin.Checked = False
        chkGirdling.Checked = False
        chkTop.Checked = False
        chkBru.Checked = False
        chkBoiling.Checked = False
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearText()
    End Sub

    Private Sub cmbDept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDept.SelectedIndexChanged
        If cmbDept.Text <> "" Then
            Load_Section()
            flxDetails.Rows.Clear()
            txtTotPcs.Text = CalTotalPcs(flxDetails)
            txtTotCts.Text = CalTotalCts(flxDetails)
            txtCount.Text = flxDetails.Rows.Count
        End If
    End Sub

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Dim intRow As Integer
        Dim blnFound As Boolean
        Dim dblIssPcs As Double
        Dim dblRetPcs As Double
        Dim dblRetPcsB As Double
        Dim dblRetCts As Double
        Dim strGroup As String
        Dim dblPktCts As Double
        Dim intNextSec As Integer
        Dim intPrevSec As Integer

        intNextSec = cmbSection.SelectedIndex + 1
        intPrevSec = cmbSection.SelectedIndex

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbSection.Text = "" Then MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If intNextSec > 25 Then MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

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
                Case "Davinci", "Opening", "Emerald", "Precision"
                    If ParcelLen = 10 Then
                        ParcelNo = Mid(Instring, 1, ParcelLen - 3)
                        PacketNo = strRight(Instring, 3)
                    Else
                        ParcelNo = Mid(Instring, 1, ParcelLen - 4)
                        PacketNo = strRight(Instring, 4)
                    End If
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
            blnFound = False
            rsComSql = New ADODB.Recordset
            Select Case cmbDept.Text
                Case "Rounds"
                    rsComSql.Open("SELECT ParNo,Grp,PktCts FROM tblRndPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND DelDate IS NOT NULL AND AccDate IS NOT NULL", AdoCN, 1, 1)
                Case "Baguettes"
                    rsComSql.Open("SELECT ParNo,Grp,PktCts FROM tblBAGPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND DelDate IS NOT NULL AND AccDate IS NOT NULL", AdoCN, 1, 1)
                Case "Davinci", "Opening", "Emerald"
                    rsComSql.Open("SELECT ParNo,Grp,PktCts FROM tblExtPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND DelDate IS NOT NULL AND AccDate IS NOT NULL AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                Case "Precision"
                    rsComSql.Open("SELECT PktOrdNo AS ParNo,Grp,PktCts FROM tblPacket WHERE PktOrdNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "'", AdoCN, 1, 1)
            End Select
            If rsComSql.RecordCount Then
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
                        rsComSql.Open("SELECT SecName FROM tblRndSections WHERE SecCode = " & intNextSec & " AND SecName <> 'No'", AdoCN, 1, 1)
                    Case "Baguettes"
                        rsComSql.Open("SELECT SecName FROM tblBAGSections WHERE SecCode = " & intNextSec & " AND SecName <> 'No'", AdoCN, 1, 1)
                    Case "Davinci", "Opening", "Emerald"
                        rsComSql.Open("SELECT SecName FROM tblExtSections WHERE SecCode = " & intNextSec & " AND SecName <> 'No' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                    Case "Precision"
                        rsComSql.Open("SELECT SecName FROM tblSections2 WHERE SecCode = " & intNextSec & " AND SecName <> 'No' AND Flow = 'RndSize'", AdoCN, 1, 1)
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
                If intNextSec > 1 Then
                    rsComSql = New ADODB.Recordset
                    Select Case cmbDept.Text
                        Case "Rounds"
                            rsComSql.Open("SELECT SecCode FROM tblRndSections WHERE SecCode < " & intNextSec & " AND SecName <> 'No' ORDER BY SecCode DESC", AdoCN, 1, 1)
                        Case "Baguettes"
                            rsComSql.Open("SELECT SecCode FROM tblBAGSections WHERE SecCode < " & intNextSec & " AND SecName <> 'No' ORDER BY SecCode DESC", AdoCN, 1, 1)
                        Case "Davinci", "Opening", "Emerald"
                            rsComSql.Open("SELECT SecCode FROM tblExtSections WHERE SecCode < " & intNextSec & " AND SecName <> 'No' AND Department = '" & cmbDept.Text & "' ORDER BY SecCode DESC", AdoCN, 1, 1)
                        Case "Precision"
                            rsComSql.Open("SELECT SecCode FROM tblSections2 WHERE SecCode < " & intNextSec & " AND SecName <> 'No' AND Flow = 'RndSize' ORDER BY SecCode DESC", AdoCN, 1, 1)
                    End Select
                    If rsComSql.RecordCount Then
                        rsComSql.MoveFirst()
                        intPrevSec = rsComSql.Fields("SecCode").Value
                    End If
                    rsComSql = Nothing
                End If
            End If

            If blnFound = True Then
                rsComSql = New ADODB.Recordset
                Select Case cmbDept.Text
                    Case "Rounds"
                        rsComSql.Open("SELECT ParNo FROM tblRndIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & intNextSec & "", AdoCN, 1, 1)
                    Case "Baguettes"
                        rsComSql.Open("SELECT ParNo FROM tblBAGIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & intNextSec & "", AdoCN, 1, 1)
                    Case "Davinci", "Opening", "Emerald"
                        rsComSql.Open("SELECT ParNo FROM tblExtIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & intNextSec & " AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                    Case "Precision"
                        rsComSql.Open("SELECT ParNo FROM tblIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & intNextSec & "", AdoCN, 1, 1)
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
                            rsComSql.Open("SELECT ParNo, IssPcsT + IssPcsB AS IssPcs FROM tblRndIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & intPrevSec & "", AdoCN, 1, 1)
                        Case "Baguettes"
                            rsComSql.Open("SELECT ParNo, IssPcsT + IssPcsB AS IssPcs FROM tblBAGIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & intPrevSec & "", AdoCN, 1, 1)
                        Case "Davinci", "Opening", "Emerald"
                            rsComSql.Open("SELECT ParNo, IssPcsT + IssPcsB AS IssPcs FROM tblExtIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & intPrevSec & " AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                        Case "Precision"
                            rsComSql.Open("SELECT ParNo, IssPcsT + IssPcsB AS IssPcs FROM tblIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & intPrevSec & "", AdoCN, 1, 1)
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
            dblRetCts = 0
            If blnFound = True Then
                If cmbSection.SelectedIndex + 1 > 1 Then
                    rsComSql = New ADODB.Recordset
                    Select Case cmbDept.Text
                        Case "Rounds"
                            rsComSql.Open("SELECT SUM(RetPcsT) AS RetPcsT,SUM(RetPcsB - RejPcs - LostPcs + ExtPcs - MacPcs) AS RetPcsB,ROUND(SUM(RetCts), 3) AS RetCts FROM tblRndReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & intPrevSec & "", AdoCN, 1, 1)
                        Case "Baguettes"
                            rsComSql.Open("SELECT SUM(RetPcsT) AS RetPcsT,SUM(RetPcsB - RejPcs - PCUPcs - LostPcs + ExtPcs) AS RetPcsB,ROUND(SUM(RetCts - PCUPCts), 3) AS RetCts FROM tblBAGReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & intPrevSec & "", AdoCN, 1, 1)
                        Case "Davinci", "Opening", "Emerald"
                            rsComSql.Open("SELECT SUM(RetPcsT) AS RetPcsT,SUM(RetPcsB - RejPcs - LostPcs + ExtPcs - MacPcs) AS RetPcsB,ROUND(SUM(RetCts), 3) AS RetCts FROM tblExtReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & intPrevSec & " AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                        Case "Precision"
                            rsComSql.Open("SELECT SUM(RetPcsT) AS RetPcsT,SUM(RetPcsB - RejPcs - LostPcs + ExtPcs) AS RetPcsB,ROUND(SUM(RetCts), 3) AS RetCts FROM tblReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & intPrevSec & "", AdoCN, 1, 1)
                    End Select
                    If rsComSql.RecordCount Then
                        dblRetPcs = rsComSql.Fields("RetPcsT").Value
                        dblRetPcsB = rsComSql.Fields("RetPcsB").Value
                        dblRetCts = rsComSql.Fields("RetCts").Value
                        blnFound = True

                        If dblIssPcs <> dblRetPcs + dblRetPcsB Then
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
                        Case "Davinci", "Opening", "Emerald"
                            rsComSql.Open("SELECT PktPcs AS RetPcs, PktCts AS RetCts FROM tblExtPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
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
        Dim intSec As Integer

        Dim dblRghPcs As Double
        Dim dblRghCts As Double

        Dim dblTrfPcs As Double
        Dim dblTrfCts As Double

        Dim strGroup As String
        Dim BatchNo As Double

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbSection.Text = "" Then MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbSection.SelectedIndex + 1 > 25 Then MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If cmbDept.Text = "Rounds" Then
            If cmbSection.SelectedIndex + 1 = 15 Then
                If txtGroup.Text = "" Then MsgBox("Invalid Group", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
                If UCase(txtGroup.Text) = "A" Or UCase(txtGroup.Text) = "B" Or UCase(txtGroup.Text) = "C" Or UCase(txtGroup.Text) = "D" Or UCase(txtGroup.Text) = "E" Or UCase(txtGroup.Text) = "F" Or UCase(txtGroup.Text) = "R" Or UCase(txtGroup.Text) = "I" Then

                Else
                    MsgBox("Invalid Group", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
        End If

        dblRghPcs = 0
        dblRghCts = 0
        dblTrfPcs = 0
        dblTrfCts = 0
        strGroup = ""

        If txtEmp.Text = "" Then MsgBox("Invalid Emp No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        txtEmp.Text = UCase(txtEmp.Text)
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL3 WHERE FullEmpNo = '" & txtEmp.Text & "' AND Pay = 1", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Employee No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        For intRow = 0 To flxDetails.Rows.Count - 1
            rsComSql = New ADODB.Recordset
            Select Case cmbDept.Text
                Case "Rounds"
                    rsComSql.Open("SELECT PktFlow FROM tblRndPacket WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                Case "Baguettes"
                    rsComSql.Open("SELECT PktFlow FROM tblBAGPacket WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                Case "Davinci", "Opening", "Emerald"
                    rsComSql.Open("SELECT PktFlow FROM tblExtPacket WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
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
                    Case "Davinci", "Opening", "Emerald"
                        rsComSql_1.Open("SELECT SecCount FROM tblExtSections WHERE SecCode = " & cmbSection.SelectedIndex + 1 & " AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                    Case "Precision"
                        rsComSql_1.Open("SELECT SecCount FROM tblSections2 WHERE SecCode = " & cmbSection.SelectedIndex + 1 & " AND Flow = 'RndSize'", AdoCN, 1, 1)
                End Select
                If rsComSql_1.RecordCount Then
                    intSecCount = rsComSql_1.Fields("SecCount").Value
                End If
                rsComSql_1 = Nothing


                dtpToday = GetToday()

                rsComSql_1 = New ADODB.Recordset
                Select Case cmbDept.Text
                    Case "Rounds"
                        If cmbSection.SelectedIndex + 1 = 19 And chkFinish.Checked = True Then
                            For intSec = 0 To 6
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT PktNo FROM tblRndIssues WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 + intSec & "", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount = 0 Then
                                    mStrSQL = "INSERT INTO tblRndIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & cmbSection.SelectedIndex + 1 + intSec & "," & _
                                                "'" & mFlow & "'," & intSecCount + intSec & ",'" & Trim(txtEmp.Text) & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & _
                                                "" & CSng(flxDetails.Item(4, intRow).Value) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "')"

                                    AdoCN.Execute(mStrSQL)

                                    mStrSQL = "INSERT INTO tblRndReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,Gra_Trf,MacPcs,MacCts,DoneBy) " & _
                                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & mFlow & "','" & intSecCount + intSec & "'," & cmbSection.SelectedIndex + 1 + intSec & ",'" & Trim(txtEmp.Text) & "'," & _
                                                "'" & CDbl(flxDetails.Item(2, intRow).Value) & "','" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & CSng(flxDetails.Item(4, intRow).Value) & "',0,0,0,0,0,0,0,'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,0,0,'" & PBUser_EmpNo & "')"

                                    AdoCN.Execute(mStrSQL)

                                End If
                                rsComSql_1 = Nothing
                            Next

                        ElseIf cmbSection.SelectedIndex + 1 = 16 And chkFinish.Checked = True Then
                            For intSec = 0 To 9
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT PktNo FROM tblRndIssues WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 + intSec & "", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount = 0 Then
                                    mStrSQL = "INSERT INTO tblRndIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & cmbSection.SelectedIndex + 1 + intSec & "," & _
                                                "'" & mFlow & "'," & intSecCount + intSec & ",'" & Trim(txtEmp.Text) & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & _
                                                "" & CSng(flxDetails.Item(4, intRow).Value) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "')"

                                    AdoCN.Execute(mStrSQL)

                                    mStrSQL = "INSERT INTO tblRndReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,Gra_Trf,MacPcs,MacCts,DoneBy) " & _
                                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & mFlow & "','" & intSecCount + intSec & "'," & cmbSection.SelectedIndex + 1 + intSec & ",'" & Trim(txtEmp.Text) & "'," & _
                                                "'" & CDbl(flxDetails.Item(2, intRow).Value) & "','" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & CSng(flxDetails.Item(4, intRow).Value) & "',0,0,0,0,0,0,0,'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,0,0,'" & PBUser_EmpNo & "')"

                                    AdoCN.Execute(mStrSQL)

                                End If
                                rsComSql_1 = Nothing
                            Next

                        ElseIf cmbSection.SelectedIndex + 1 = 24 And chkFinish.Checked = True Then
                            For intSec = 0 To 1
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT PktNo FROM tblRndIssues WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 + intSec & "", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount = 0 Then
                                    mStrSQL = "INSERT INTO tblRndIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & cmbSection.SelectedIndex + 1 + intSec & "," & _
                                                "'" & mFlow & "'," & intSecCount + intSec & ",'" & Trim(txtEmp.Text) & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & _
                                                "" & CSng(flxDetails.Item(4, intRow).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(dtpToday, "HH:mm:ss") & "','" & PBUser_EmpNo & "')"

                                    AdoCN.Execute(mStrSQL)

                                    mStrSQL = "INSERT INTO tblRndReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,Gra_Trf,MacPcs,MacCts,DoneBy) " & _
                                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & mFlow & "','" & intSecCount + intSec & "'," & cmbSection.SelectedIndex + 1 + intSec & ",'" & Trim(txtEmp.Text) & "'," & _
                                                "'" & CDbl(flxDetails.Item(2, intRow).Value) & "','" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & CSng(flxDetails.Item(4, intRow).Value) & "',0,0,0,0,0,0,0,'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,0,0,'" & PBUser_EmpNo & "')"

                                    AdoCN.Execute(mStrSQL)

                                End If
                                rsComSql_1 = Nothing
                            Next

                        ElseIf cmbSection.SelectedIndex + 1 = 19 And chkBoiling.Checked = True Then
                            For intSec = 0 To 6
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT PktNo FROM tblRndIssues WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 + intSec & "", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount = 0 Then
                                    mStrSQL = "INSERT INTO tblRndIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & cmbSection.SelectedIndex + 1 + intSec & "," & _
                                                "'" & mFlow & "'," & intSecCount + intSec & ",'" & Trim(txtEmp.Text) & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & _
                                                "" & CSng(flxDetails.Item(4, intRow).Value) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "')"

                                    AdoCN.Execute(mStrSQL)

                                    mStrSQL = "INSERT INTO tblRndReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,Gra_Trf,MacPcs,MacCts,DoneBy) " & _
                                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & mFlow & "','" & intSecCount + intSec & "'," & cmbSection.SelectedIndex + 1 + intSec & ",'" & Trim(txtEmp.Text) & "'," & _
                                                "'" & CDbl(flxDetails.Item(2, intRow).Value) & "','" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & CSng(flxDetails.Item(4, intRow).Value) & "',0,0,0,0,0,0,0,'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,0,0,'" & PBUser_EmpNo & "')"

                                    AdoCN.Execute(mStrSQL)

                                End If
                                rsComSql_1 = Nothing
                            Next

                            BatchNo = max_BatchNo()

                            dblTrfPcs = CDbl(flxDetails.Item(2, intRow).Value) + CDbl(flxDetails.Item(3, intRow).Value)
                            dblTrfCts = Math.Round(CSng(flxDetails.Item(4, intRow).Value), 3)

                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT PktPcs, PktCts, Grp FROM tblRndPacket WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount Then
                                dblRghPcs = rsComSql_1.Fields("PktPcs").Value
                                dblRghCts = Math.Round(rsComSql_1.Fields("PktCts").Value, 3)
                                strGroup = rsComSql_1.Fields("Grp").Value
                            End If
                            rsComSql_1 = Nothing

                            Dep_Grading_Trf(cmbDept.Text, BatchNo, flxDetails.Item(0, intRow).Value, flxDetails.Item(1, intRow).Value, dblTrfPcs, dblTrfCts, dblRghPcs, dblRghCts, strGroup)
                            ReturnTablesUpdation(cmbDept.Text, flxDetails.Item(0, intRow).Value, flxDetails.Item(1, intRow).Value)
                            GradingAcceptations(flxDetails.Item(0, intRow).Value, flxDetails.Item(1, intRow).Value, 0, 0)

                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT * FROM tblGrading_BoilingIssues WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount = 0 Then
                                'Boiling Issues
                                AdoCN.Execute("INSERT INTO tblGrading_BoilingIssues(Department,ParNo,PktNo,EmpNo,IssPcs,IssCts,IssDate,IssTime,UserName,Remarks,Grp) " & _
                                              "VALUES('" & cmbDept.Text & "','" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & Trim(txtEmp.Text) & "'," & dblTrfPcs & "," & _
                                                     "" & dblTrfCts & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "','','" & strGroup & "')")

                                rsComSql_2 = New ADODB.Recordset
                                rsComSql_2.Open("SELECT * FROM tblGrading_Parcel WHERE ParNo = '" & Mid(flxDetails.Item(0, intRow).Value, 1, 6) & "' AND Dept = 'Grading'", AdoCN, 1, 1)
                                If rsComSql_2.RecordCount = 0 Then
                                    AdoCN.Execute("INSERT INTO tblGrading_Parcel(ParNo,Complete,Dept) VALUES('" & Mid(flxDetails.Item(0, intRow).Value, 1, 6) & "',0,'Grading')")
                                End If
                                rsComSql_2 = Nothing

                            End If
                            rsComSql_1 = Nothing

                        ElseIf cmbSection.SelectedIndex + 1 = 24 And chkBoiling.Checked = True Then
                            For intSec = 0 To 1
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT PktNo FROM tblRndIssues WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 + intSec & "", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount = 0 Then
                                    mStrSQL = "INSERT INTO tblRndIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & cmbSection.SelectedIndex + 1 + intSec & "," & _
                                                "'" & mFlow & "'," & intSecCount + intSec & ",'" & PBUser_EmpNo & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & _
                                                "" & CSng(flxDetails.Item(4, intRow).Value) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "')"

                                    AdoCN.Execute(mStrSQL)

                                    mStrSQL = "INSERT INTO tblRndReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,Gra_Trf,MacPcs,MacCts,DoneBy) " & _
                                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & mFlow & "','" & intSecCount + intSec & "'," & cmbSection.SelectedIndex + 1 + intSec & ",'" & PBUser_EmpNo & "'," & _
                                                "'" & CDbl(flxDetails.Item(2, intRow).Value) & "','" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & CSng(flxDetails.Item(4, intRow).Value) & "',0,0,0,0,0,0,0,'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,0,0,'" & PBUser_EmpNo & "')"

                                    AdoCN.Execute(mStrSQL)

                                End If
                                rsComSql_1 = Nothing
                            Next


                            BatchNo = max_BatchNo()

                            dblTrfPcs = CDbl(flxDetails.Item(2, intRow).Value) + CDbl(flxDetails.Item(3, intRow).Value)
                            dblTrfCts = Math.Round(CSng(flxDetails.Item(4, intRow).Value), 3)

                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT PktPcs, PktCts, Grp FROM tblRndPacket WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount Then
                                dblRghPcs = rsComSql_1.Fields("PktPcs").Value
                                dblRghCts = Math.Round(rsComSql_1.Fields("PktCts").Value, 3)
                                strGroup = rsComSql_1.Fields("Grp").Value
                            End If
                            rsComSql_1 = Nothing

                            Dep_Grading_Trf(cmbDept.Text, BatchNo, flxDetails.Item(0, intRow).Value, flxDetails.Item(1, intRow).Value, dblTrfPcs, dblTrfCts, dblRghPcs, dblRghCts, strGroup)
                            ReturnTablesUpdation(cmbDept.Text, flxDetails.Item(0, intRow).Value, flxDetails.Item(1, intRow).Value)
                            GradingAcceptations(flxDetails.Item(0, intRow).Value, flxDetails.Item(1, intRow).Value, 0, 0)

                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT * FROM tblGrading_BoilingIssues WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount = 0 Then
                                'Boiling Issues
                                AdoCN.Execute("INSERT INTO tblGrading_BoilingIssues(Department,ParNo,PktNo,EmpNo,IssPcs,IssCts,IssDate,IssTime,UserName,Remarks,Grp) " & _
                                              "VALUES('" & cmbDept.Text & "','" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & Trim(txtEmp.Text) & "'," & dblTrfPcs & "," & _
                                                     "" & dblTrfCts & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "','','" & strGroup & "')")

                                rsComSql_2 = New ADODB.Recordset
                                rsComSql_2.Open("SELECT * FROM tblGrading_Parcel WHERE ParNo = '" & Mid(flxDetails.Item(0, intRow).Value, 1, 6) & "' AND Dept = 'Grading'", AdoCN, 1, 1)
                                If rsComSql_2.RecordCount = 0 Then
                                    AdoCN.Execute("INSERT INTO tblGrading_Parcel(ParNo,Complete,Dept) VALUES('" & Mid(flxDetails.Item(0, intRow).Value, 1, 6) & "',0,'Grading')")
                                End If
                                rsComSql_2 = Nothing

                            End If
                            rsComSql_1 = Nothing

                        ElseIf cmbSection.SelectedIndex + 1 = 12 And chkPrepFin.Checked = True Then
                            For intSec = 0 To 2
                                If cmbSection.SelectedIndex + 1 + intSec <> 13 Then
                                    If intSec = 2 Then
                                        intSecCount = intSecCount - 1
                                    End If
                                    rsComSql_1 = New ADODB.Recordset
                                    rsComSql_1.Open("SELECT PktNo FROM tblRndIssues WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 + intSec & "", AdoCN, 1, 1)
                                    If rsComSql_1.RecordCount = 0 Then
                                        mStrSQL = "INSERT INTO tblRndIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                                  "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & cmbSection.SelectedIndex + 1 + intSec & "," & _
                                                    "'" & mFlow & "'," & intSecCount + intSec & ",'" & Trim(txtEmp.Text) & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & _
                                                    "" & CSng(flxDetails.Item(4, intRow).Value) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "')"

                                        AdoCN.Execute(mStrSQL)

                                        mStrSQL = "INSERT INTO tblRndReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,Gra_Trf,MacPcs,MacCts,DoneBy) " & _
                                                  "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & mFlow & "','" & intSecCount + intSec & "'," & cmbSection.SelectedIndex + 1 + intSec & ",'" & Trim(txtEmp.Text) & "'," & _
                                                    "'" & CDbl(flxDetails.Item(2, intRow).Value) & "','" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & CSng(flxDetails.Item(4, intRow).Value) & "',0,0,0,0,0,0,0,'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,0,0,'" & PBUser_EmpNo & "')"

                                        AdoCN.Execute(mStrSQL)

                                    End If
                                    rsComSql_1 = Nothing
                                End If

                            Next

                        ElseIf cmbSection.SelectedIndex + 1 = 2 And chkGirdling.Checked = True Then
                            For intSec = 0 To 6
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT PktNo FROM tblRndIssues WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 + intSec & "", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount = 0 Then
                                    mStrSQL = "INSERT INTO tblRndIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & cmbSection.SelectedIndex + 1 + intSec & "," & _
                                                "'" & mFlow & "'," & intSecCount + intSec & ",'" & Trim(txtEmp.Text) & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & _
                                                "" & CSng(flxDetails.Item(4, intRow).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(dtpToday, "HH:mm:ss") & "','" & PBUser_EmpNo & "')"

                                    AdoCN.Execute(mStrSQL)

                                    mStrSQL = "INSERT INTO tblRndReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,Gra_Trf,MacPcs,MacCts,DoneBy) " & _
                                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & mFlow & "','" & intSecCount + intSec & "'," & cmbSection.SelectedIndex + 1 + intSec & ",'" & Trim(txtEmp.Text) & "'," & _
                                                "'" & CDbl(flxDetails.Item(2, intRow).Value) & "','" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & CSng(flxDetails.Item(4, intRow).Value) & "',0,0,0,0,0,0,0,'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,0,0,'" & PBUser_EmpNo & "')"

                                    AdoCN.Execute(mStrSQL)

                                End If
                                rsComSql_1 = Nothing
                            Next

                        ElseIf cmbSection.SelectedIndex + 1 = 19 And chkTop.Checked = True Then
                            For intSec = 0 To 3
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT PktNo FROM tblRndIssues WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 + intSec & "", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount = 0 Then
                                    mStrSQL = "INSERT INTO tblRndIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & cmbSection.SelectedIndex + 1 + intSec & "," & _
                                                "'" & mFlow & "'," & intSecCount + intSec & ",'" & Trim(txtEmp.Text) & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & _
                                                "" & CSng(flxDetails.Item(4, intRow).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(dtpToday, "HH:mm:ss") & "','" & PBUser_EmpNo & "')"

                                    AdoCN.Execute(mStrSQL)

                                    mStrSQL = "INSERT INTO tblRndReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,Gra_Trf,MacPcs,MacCts,DoneBy) " & _
                                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & mFlow & "','" & intSecCount + intSec & "'," & cmbSection.SelectedIndex + 1 + intSec & ",'" & Trim(txtEmp.Text) & "'," & _
                                                "'" & CDbl(flxDetails.Item(2, intRow).Value) & "','" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & CSng(flxDetails.Item(4, intRow).Value) & "',0,0,0,0,0,0,0,'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,0,0,'" & PBUser_EmpNo & "')"

                                    AdoCN.Execute(mStrSQL)

                                End If
                                rsComSql_1 = Nothing
                            Next

                        ElseIf cmbSection.SelectedIndex + 1 = 2 And chkBru.Checked = True Then
                            For intSec = 0 To 2
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT PktNo FROM tblRndIssues WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 + intSec & "", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount = 0 Then
                                    mStrSQL = "INSERT INTO tblRndIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & cmbSection.SelectedIndex + 1 + intSec & "," & _
                                                "'" & mFlow & "'," & intSecCount + intSec & ",'" & Trim(txtEmp.Text) & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & _
                                                "" & CSng(flxDetails.Item(4, intRow).Value) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "')"

                                    AdoCN.Execute(mStrSQL)

                                    mStrSQL = "INSERT INTO tblRndReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,Gra_Trf,MacPcs,MacCts,DoneBy) " & _
                                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & mFlow & "','" & intSecCount + intSec & "'," & cmbSection.SelectedIndex + 1 + intSec & ",'" & Trim(txtEmp.Text) & "'," & _
                                                "'" & CDbl(flxDetails.Item(2, intRow).Value) & "','" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & CSng(flxDetails.Item(4, intRow).Value) & "',0,0,0,0,0,0,0,'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,0,0,'" & PBUser_EmpNo & "')"

                                    AdoCN.Execute(mStrSQL)

                                End If
                                rsComSql_1 = Nothing
                            Next

                        ElseIf cmbSection.SelectedIndex + 1 = 3 And chkBru.Checked = True Then
                            For intSec = 0 To 1
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT PktNo FROM tblRndIssues WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 + intSec & "", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount = 0 Then
                                    mStrSQL = "INSERT INTO tblRndIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & cmbSection.SelectedIndex + 1 + intSec & "," & _
                                                "'" & mFlow & "'," & intSecCount + intSec & ",'" & Trim(txtEmp.Text) & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & _
                                                "" & CSng(flxDetails.Item(4, intRow).Value) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "')"

                                    AdoCN.Execute(mStrSQL)

                                    mStrSQL = "INSERT INTO tblRndReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,Gra_Trf,MacPcs,MacCts,DoneBy) " & _
                                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & mFlow & "','" & intSecCount + intSec & "'," & cmbSection.SelectedIndex + 1 + intSec & ",'" & Trim(txtEmp.Text) & "'," & _
                                                "'" & CDbl(flxDetails.Item(2, intRow).Value) & "','" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & CSng(flxDetails.Item(4, intRow).Value) & "',0,0,0,0,0,0,0,'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,0,0,'" & PBUser_EmpNo & "')"

                                    AdoCN.Execute(mStrSQL)

                                End If
                                rsComSql_1 = Nothing
                            Next

                        Else
                            rsComSql_1.Open("SELECT PktNo FROM tblRndIssues WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount = 0 Then
                                mStrSQL = "INSERT INTO tblRndIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                          "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & cmbSection.SelectedIndex + 1 & "," & _
                                            "'" & mFlow & "'," & intSecCount & ",'" & Trim(txtEmp.Text) & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & _
                                            "" & CSng(flxDetails.Item(4, intRow).Value) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "')"

                                AdoCN.Execute(mStrSQL)

                                mStrSQL = "INSERT INTO tblRndReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,Gra_Trf,MacPcs,MacCts,DoneBy) " & _
                                          "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & mFlow & "','" & intSecCount & "'," & cmbSection.SelectedIndex + 1 & ",'" & Trim(txtEmp.Text) & "'," & _
                                            "'" & CDbl(flxDetails.Item(2, intRow).Value) & "','" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & CSng(flxDetails.Item(4, intRow).Value) & "',0,0,0,0,0,0,0,'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,0,0,'" & PBUser_EmpNo & "')"

                                AdoCN.Execute(mStrSQL)

                                If cmbSection.SelectedIndex + 1 = 15 Then
                                    AdoCN.Execute("UPDATE tblRndPacket SET Grp = '" & UCase(txtGroup.Text) & "' WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'")
                                End If

                            End If
                        End If

                    Case "Baguettes"
                        If cmbSection.SelectedIndex + 1 = 8 And chkFinish.Checked = True Then
                            For intSec = 0 To 2
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT PktNo FROM tblBAGIssues WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 + intSec & "", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount = 0 Then
                                    mStrSQL = "INSERT INTO tblBAGIssues(OrderNo,ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                              "VALUES(1,'" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & cmbSection.SelectedIndex + 1 + intSec & "," & _
                                                "'" & mFlow & "'," & intSecCount + intSec & ",'" & Trim(txtEmp.Text) & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & _
                                                "" & CSng(flxDetails.Item(4, intRow).Value) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "')"

                                    AdoCN.Execute(mStrSQL)

                                    mStrSQL = "INSERT INTO tblBAGReturns(OrderNo,ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,PCUCts,PCUPcs,PCUPCts,Gra_Trf,RejReason,BLostPcs,BLostCts,DoneBy) " & _
                                              "VALUES(1,'" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & mFlow & "','" & intSecCount + intSec & "','" & cmbSection.SelectedIndex + 1 + intSec & "','" & Trim(txtEmp.Text) & "','" & CDbl(flxDetails.Item(2, intRow).Value) & "'," & _
                                                "" & CDbl(flxDetails.Item(3, intRow).Value) & ",'" & CSng(flxDetails.Item(4, intRow).Value) & "',0,0,0,0,0,0,0,'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,0,0,'',0,0,'" & PBUser_EmpNo & "')"

                                    AdoCN.Execute(mStrSQL)
                                End If
                                rsComSql_1 = Nothing
                            Next
                        Else
                            rsComSql_1.Open("SELECT PktNo FROM tblBAGIssues WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount = 0 Then
                                mStrSQL = "INSERT INTO tblBAGIssues(OrderNo,ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                          "VALUES(1,'" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & cmbSection.SelectedIndex + 1 & "," & _
                                            "'" & mFlow & "'," & intSecCount & ",'" & Trim(txtEmp.Text) & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & _
                                            "" & CSng(flxDetails.Item(4, intRow).Value) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "')"

                                AdoCN.Execute(mStrSQL)

                                mStrSQL = "INSERT INTO tblBAGReturns(OrderNo,ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,PCUCts,PCUPcs,PCUPCts,Gra_Trf,RejReason,BLostPcs,BLostCts,DoneBy) " & _
                                          "VALUES(1,'" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & mFlow & "','" & intSecCount & "','" & cmbSection.SelectedIndex + 1 & "','" & Trim(txtEmp.Text) & "','" & CDbl(flxDetails.Item(2, intRow).Value) & "'," & _
                                            "" & CDbl(flxDetails.Item(3, intRow).Value) & ",'" & CSng(flxDetails.Item(4, intRow).Value) & "',0,0,0,0,0,0,0,'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,0,0,'',0,0,'" & PBUser_EmpNo & "')"

                                AdoCN.Execute(mStrSQL)
                            End If
                        End If
                        

                    Case "Davinci", "Opening", "Emerald"
                        rsComSql_1.Open("SELECT PktNo FROM tblExtIssues WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 & " AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            mStrSQL = "INSERT INTO tblExtIssues(Department,ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                      "VALUES('" & cmbDept.Text & "','" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & cmbSection.SelectedIndex + 1 & "," & _
                                        "'" & mFlow & "'," & intSecCount & ",'" & Trim(txtEmp.Text) & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & _
                                        "" & CSng(flxDetails.Item(4, intRow).Value) & ",'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "')"

                            AdoCN.Execute(mStrSQL)

                            mStrSQL = "INSERT INTO tblExtReturns(Department,ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,Gra_Trf,MacPcs,MacCts,DoneBy) " & _
                                      "VALUES('" & cmbDept.Text & "','" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & mFlow & "','" & intSecCount & "','" & cmbSection.SelectedIndex + 1 & "','" & Trim(txtEmp.Text) & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & _
                                      "" & CDbl(flxDetails.Item(3, intRow).Value) & "," & CSng(flxDetails.Item(4, intRow).Value) & ",0,0,0,0,0,0,0,'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,0,0,'" & PBUser_EmpNo & "')"

                            AdoCN.Execute(mStrSQL)
                        End If

                    Case "Precision"
                        rsComSql_1.Open("SELECT PktNo FROM tblIssues WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            mStrSQL = "INSERT INTO tblIssues(ParNo,PktNo,Flow,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,Sec,SecCount) " & _
                                      "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & mFlow & "','" & Trim(txtEmp.Text) & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & CSng(flxDetails.Item(4, intRow).Value) & "," & _
                                        "'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "'," & cmbSection.SelectedIndex + 1 & "," & intSecCount & ")"

                            AdoCN.Execute(mStrSQL)

                            mStrSQL = "INSERT INTO tblReturns(ParNo, PktNo, Flow, SecCount, Sec, EmpNo, RetPcsT, RetPcsB, RetCts, RejPcs, RejCts, LostPcs, LostCts, BroPcs, RepPcs, NopayPcs, RetDate, RetTime, ExtPcs, Status, RghCts, LRghCts, RejReason) " & _
                                      "VALUES ('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & mFlow & "'," & intSecCount & "," & cmbSection.SelectedIndex + 1 & ",'" & Trim(txtEmp.Text) & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & CSng(flxDetails.Item(4, intRow).Value) & "" & _
                                        ",0,0,0,0,0,0,0,'" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,'')"

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

    Private Sub cmbSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSection.SelectedIndexChanged
        flxDetails.Rows.Clear()
        txtTotPcs.Text = CalTotalPcs(flxDetails)
        txtTotCts.Text = CalTotalCts(flxDetails)
        txtCount.Text = flxDetails.Rows.Count
    End Sub

    Private Sub chkFinish_CheckedChanged(sender As Object) Handles chkFinish.CheckedChanged
        If chkFinish.Checked = True Then
            chkPrepFin.Checked = False
            chkGirdling.Checked = False
            chkTop.Checked = False
            chkBru.Checked = False
            chkBoiling.Checked = False
        End If
    End Sub

    Private Sub chkPrepFin_CheckedChanged(sender As Object) Handles chkPrepFin.CheckedChanged
        If chkPrepFin.Checked = True Then
            chkFinish.Checked = False
            chkGirdling.Checked = False
            chkTop.Checked = False
            chkBru.Checked = False
            chkBoiling.Checked = False
        End If
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

    Private Sub chkGirdling_CheckedChanged(sender As Object) Handles chkGirdling.CheckedChanged
        If chkGirdling.Checked = True Then
            chkFinish.Checked = False
            chkPrepFin.Checked = False
            chkTop.Checked = False
            chkBru.Checked = False
            chkBoiling.Checked = False
        End If
    End Sub

    Private Sub chkTop_CheckedChanged(sender As Object) Handles chkTop.CheckedChanged
        If chkTop.Checked = True Then
            chkFinish.Checked = False
            chkPrepFin.Checked = False
            chkGirdling.Checked = False
            chkBru.Checked = False
            chkBoiling.Checked = False
        End If
    End Sub

    Private Sub chkBru_CheckedChanged(sender As Object) Handles chkBru.CheckedChanged
        If chkBru.Checked = True Then
            chkFinish.Checked = False
            chkPrepFin.Checked = False
            chkGirdling.Checked = False
            chkTop.Checked = False
            chkBoiling.Checked = False
        End If
    End Sub

    Private Sub chkBoiling_CheckedChanged(sender As Object) Handles chkBoiling.CheckedChanged
        If chkBoiling.Checked = True Then
            chkFinish.Checked = False
            chkPrepFin.Checked = False
            chkGirdling.Checked = False
            chkTop.Checked = False
            chkBru.Checked = False
        End If
    End Sub
End Class