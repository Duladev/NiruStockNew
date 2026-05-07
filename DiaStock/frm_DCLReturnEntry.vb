
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLReturnEntry
    Dim ParcelNo As String
    Dim PacketNo As String
    Dim FirstInput As Date

    Private Sub ClearText()
        cmbSection.Text = ""
        flxDetails.Rows.Clear()
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0.000"
        txtEmp.Text = ""
        txtCount.Text = "0"
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
            Case "Davinci", "Emerald", "Opening"
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
        Dim dblPktCts As Double
        Dim strGroup As String
        Dim strEmpNo As String

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
                Case "Davinci", "Emerald", "Opening"
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

            strGroup = ""
            dblIssPcs = 0
            dblPktCts = 0
            strEmpNo = ""
            blnFound = False
            rsComSql = New ADODB.Recordset
            Select Case cmbDept.Text
                Case "Rounds"
                    rsComSql.Open("SELECT ParNo,Grp,IncUnit,PktCts FROM tblRndPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND DelDate IS NOT NULL AND AccDate IS NOT NULL", AdoCN, 1, 1)
                Case "Baguettes"
                    rsComSql.Open("SELECT ParNo,Grp,PktCts FROM tblBAGPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND DelDate IS NOT NULL AND AccDate IS NOT NULL", AdoCN, 1, 1)
                Case "Davinci", "Emerald", "Opening"
                    rsComSql.Open("SELECT ParNo,Grp,PktCts FROM tblExtPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND DelDate IS NOT NULL AND AccDate IS NOT NULL AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                Case "Princess"
                    rsComSql.Open("SELECT ParNo,'' AS Grp,PktCts FROM tblPRPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND DelDate IS NOT NULL AND AccDate IS NOT NULL", AdoCN, 1, 1)
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
                        rsComSql.Open("SELECT SecName FROM tblRndSections WHERE SecCode = " & cmbSection.SelectedIndex + 1 & " AND SecName <> 'No'", AdoCN, 1, 1)
                    Case "Baguettes"
                        rsComSql.Open("SELECT SecName FROM tblBAGSections WHERE SecCode = " & cmbSection.SelectedIndex + 1 & " AND SecName <> 'No'", AdoCN, 1, 1)
                    Case "Davinci", "Emerald", "Opening"
                        rsComSql.Open("SELECT SecName FROM tblExtSections WHERE SecCode = " & cmbSection.SelectedIndex + 1 & " AND SecName <> 'No' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                    Case "Princess"
                        rsComSql.Open("SELECT SecName FROM tblPRSections WHERE SecCode = " & cmbSection.SelectedIndex + 1 & " AND SecName <> 'No'", AdoCN, 1, 1)
                    Case "Precision"
                        rsComSql.Open("SELECT SecName FROM tblSections2 WHERE SecCode = " & cmbSection.SelectedIndex + 1 & " AND SecName <> 'No' AND Flow = 'Davinci'", AdoCN, 1, 1)
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
                        rsComSql.Open("SELECT IssPcsT, IssPcsB, IssCts, EmpNo FROM tblRndIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                    Case "Baguettes"
                        rsComSql.Open("SELECT IssPcsT, IssPcsB, IssCts, EmpNo FROM tblBAGIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                    Case "Davinci", "Emerald", "Opening"
                        rsComSql.Open("SELECT IssPcsT, IssPcsB, IssCts, EmpNo FROM tblExtIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & " AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                    Case "Princess"
                        rsComSql.Open("SELECT IssPcsC AS IssPcsT, IssPcsP AS IssPcsB, IssCtsP AS IssCts, EmpNo FROM tblPRIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                    Case "Precision"
                        rsComSql.Open("SELECT IssPcsT, IssPcsB, IssCts, EmpNo FROM tblIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                End Select
                If rsComSql.RecordCount Then
                    blnFound = True
                    dblRetPcs = rsComSql.Fields("IssPcsT").Value
                    dblRetPcsB = rsComSql.Fields("IssPcsB").Value
                    dblRetCts = rsComSql.Fields("IssCts").Value
                    strEmpNo = rsComSql.Fields("EmpNo").Value
                Else
                    blnFound = False
                End If
                rsComSql = Nothing
                If blnFound = False Then MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : cmdParPkt.Focus() : Exit Sub
            End If

            If blnFound = True Then
                rsComSql = New ADODB.Recordset
                Select Case cmbDept.Text
                    Case "Rounds"
                        rsComSql.Open("SELECT ParNo FROM tblRndReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                    Case "Baguettes"
                        rsComSql.Open("SELECT ParNo FROM tblBAGReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                    Case "Davinci", "Emerald", "Opening"
                        rsComSql.Open("SELECT ParNo FROM tblExtReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & " AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                    Case "Princess"
                        rsComSql.Open("SELECT ParNo FROM tblPRReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                    Case "Precision"
                        rsComSql.Open("SELECT ParNo FROM tblReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                End Select
                If rsComSql.RecordCount = 0 Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                rsComSql = Nothing
                If blnFound = False Then MsgBox("Already Returned", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : cmdParPkt.Focus() : Exit Sub
            End If

            If blnFound = True Then
                flxDetails.Rows.Add(ParcelNo,
                                    PacketNo,
                                    dblRetPcs,
                                    dblRetPcsB,
                                    Math.Round(dblRetCts, 3),
                                    strGroup,
                                    strEmpNo,
                                    Math.Round(dblRetCts, 3))

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
        Dim intGrdTrf As Integer

        intGrdTrf = 0
        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbSection.Text = "" Then MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbSection.SelectedIndex + 1 > 25 Then MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtEmp.Text = "" Then MsgBox("Invalid Emp No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        txtEmp.Text = UCase(txtEmp.Text)
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL3 WHERE FullEmpNo = '" & txtEmp.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Employee No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        For intRow = 0 To flxDetails.Rows.Count - 1
            If Len(flxDetails.Item(7, intRow).Value) <= 0 Then
                MsgBox("Invalid Return Cts - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(7, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Not IsNumeric(flxDetails.Item(7, intRow).Value) = True Then
                MsgBox("Invalid Return Cts - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(7, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CSng(flxDetails.Item(7, intRow).Value) <= 0 Then
                MsgBox("Invalid Return Cts - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(7, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Math.Round(CSng(flxDetails.Item(4, intRow).Value), 3) < Math.Round(CSng(flxDetails.Item(7, intRow).Value), 3) Then
                MsgBox("Invalid Return Cts - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(7, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        For intRow = 0 To flxDetails.Rows.Count - 1
            rsComSql = New ADODB.Recordset
            Select Case cmbDept.Text
                Case "Rounds"
                    rsComSql.Open("SELECT PktFlow FROM tblRndPacket WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                Case "Baguettes"
                    rsComSql.Open("SELECT PktFlow FROM tblBAGPacket WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                Case "Davinci", "Emerald", "Opening"
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
                    Case "Davinci", "Emerald", "Opening"
                        rsComSql_1.Open("SELECT SecCount FROM tblExtSections WHERE SecCode = " & cmbSection.SelectedIndex + 1 & " AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                    Case "Princess"
                        rsComSql_1.Open("SELECT SecCode AS SecCount FROM tblPRSections WHERE SecCode = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                    Case "Precision"
                        rsComSql_1.Open("SELECT SecCount FROM tblSections2 WHERE SecCode = " & cmbSection.SelectedIndex + 1 & " AND Flow = 'Davinci'", AdoCN, 1, 1)
                End Select
                If rsComSql_1.RecordCount Then
                    intSecCount = rsComSql_1.Fields("SecCount").Value
                End If
                rsComSql_1 = Nothing

                rsComSql_1 = New ADODB.Recordset
                Select Case cmbDept.Text
                    Case "Rounds"
                        rsComSql_1.Open("SELECT PktNo FROM tblRndReturns WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            mStrSQL = "INSERT INTO tblRndReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,Gra_Trf,MacPcs,MacCts,DoneBy) " & _
                                      "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & mFlow & "','" & intSecCount & "'," & cmbSection.SelectedIndex + 1 & ",'" & Trim(txtEmp.Text) & "'," & _
                                        "'" & CDbl(flxDetails.Item(2, intRow).Value) & "','" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & CSng(flxDetails.Item(7, intRow).Value) & "',0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,0,0,'" & PBUser_EmpNo & "')"

                            AdoCN.Execute(mStrSQL)
                        End If

                    Case "Baguettes"
                        rsComSql_1.Open("SELECT PktNo FROM tblBAGReturns WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            intGrdTrf = 0

                            mStrSQL = "INSERT INTO tblBAGReturns(OrderNo,ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,PCUCts,PCUPcs,PCUPCts,Gra_Trf,RejReason,BLostPcs,BLostCts,DoneBy) " & _
                                      "VALUES(1,'" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & mFlow & "','" & intSecCount & "','" & cmbSection.SelectedIndex + 1 & "','" & Trim(txtEmp.Text) & "','" & CDbl(flxDetails.Item(2, intRow).Value) & "'," & _
                                        "" & CDbl(flxDetails.Item(3, intRow).Value) & ",'" & CSng(flxDetails.Item(7, intRow).Value) & "',0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,0,0,'',0,0,'" & PBUser_EmpNo & "')"

                            AdoCN.Execute(mStrSQL)
                        End If

                    Case "Davinci", "Emerald", "Opening"
                        rsComSql_1.Open("SELECT PktNo FROM tblExtReturns WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 & " AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            mStrSQL = "INSERT INTO tblExtReturns(Department,ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,Gra_Trf,MacPcs,MacCts,DoneBy) " & _
                                      "VALUES('" & cmbDept.Text & "','" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & mFlow & "','" & intSecCount & "','" & cmbSection.SelectedIndex + 1 & "','" & Trim(txtEmp.Text) & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & _
                                        "" & CDbl(flxDetails.Item(3, intRow).Value) & "," & CSng(flxDetails.Item(7, intRow).Value) & ",0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,0,0,'" & PBUser_EmpNo & "')"

                            AdoCN.Execute(mStrSQL)
                        End If

                    Case "Princess"
                        rsComSql_1.Open("SELECT PktNo FROM tblPRreturns WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            mStrSQL = "INSERT INTO tblPRreturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsP,RetPcsC,RetCtsP,RetCtsC,RejPcs,RejCts,LostPcs,PCUPcs,PCUCts,PCUPCts,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RejReason,DoneBy) " & _
                                      "VALUES ('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & mFlow & "' ," & intSecCount & "," & (cmbSection.SelectedIndex) + 1 & ",'" & Trim(txtEmp.Text) & "' ," & CInt(flxDetails.Item(2, intRow).Value) & "," & _
                                          "" & CInt(flxDetails.Item(3, intRow).Value) & "," & CSng(flxDetails.Item(7, intRow).Value) & ",0,0,0,0,0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,'','" & PBUser_EmpNo & "')"

                            AdoCN.Execute(mStrSQL)
                        End If

                    Case "Precision"
                        rsComSql_1.Open("SELECT PktNo FROM tblReturns WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            mStrSQL = "INSERT INTO tblReturns(ParNo, PktNo, Flow, SecCount, Sec, EmpNo, RetPcsT, RetPcsB, RetCts, RejPcs, RejCts, LostPcs, LostCts, BroPcs, RepPcs, NopayPcs, RetDate, RetTime, ExtPcs, Status, RghCts, LRghCts, RejReason) " & _
                                      "VALUES ('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & mFlow & "'," & intSecCount & "," & cmbSection.SelectedIndex + 1 & ",'" & Trim(txtEmp.Text) & "'," & CInt(flxDetails.Item(2, intRow).Value) & "," & _
                                      "" & CInt(flxDetails.Item(3, intRow).Value) & "," & CSng(flxDetails.Item(7, intRow).Value) & ",0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,'')"

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

    Private Sub frm_DCLReturnEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        cmbDept.Items.Clear()
        cmbDept.Items.Add("Baguettes")
        cmbDept.Items.Add("Davinci")
        cmbDept.Items.Add("Emerald")
        cmbDept.Items.Add("Opening")
        cmbDept.Items.Add("Princess")
        cmbDept.Items.Add("Rounds")
        cmbDept.Items.Add("Precision")

        ClearText()
    End Sub
End Class