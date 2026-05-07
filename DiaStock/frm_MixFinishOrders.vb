
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixFinishOrders
    Dim intNoOfRecords As Integer
    Dim intCounter As Long
    Dim strFolderPath As String

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_PolishedReturns()
        Dim Rs As New ADODB.Recordset
        Dim vIssuePcs, vIssueCts, vPktCts As Double
        Dim vPktPcs As Double
        Dim vAvgPrice As Double
        Dim varParcelNo As String
        Dim mRej, vIssCts As Single
        Dim RejP As Integer
        Dim strSubject As String
        Dim intLostPcs As Integer
        Dim intExtPcs As Integer
        Dim vCharges As String
        Dim strSelect As String
        Dim strWhere As String
        Dim strOrder As String
        Dim intGrPcs As Integer

        Rs = New ADODB.Recordset
        If txtOrder.Text <> "" Then
            strSelect = "SELECT TOP (100) PERCENT dbo.tblMixReturns.ParNo, dbo.tblMixPacket.PktRefNo, dbo.tblMixPacket.Pktside, dbo.tblMixPacket.AParNo, dbo.tblMixPacket.AssortNo, " & _
                            "dbo.tblAssortList.MarketPrice, dbo.tblMixReturns.PktNo, SUM(dbo.tblMixReturns.RetPcsT + dbo.tblMixReturns.RetPcsB) AS RetPcs, SUM(dbo.tblMixReturns.RetCts) " & _
                            "AS RetCts, dbo.tblMixPacket.PktPcs, dbo.tblMixPacket.PktCts, dbo.tblMixReturns.Status, SUM(dbo.tblMixReturns.RejPcs) AS RejPcs, SUM(dbo.tblMixReturns.RejCts) " & _
                            "AS RejCts, SUM(dbo.tblMixReturns.LostPcs) AS LostPcs, SUM(dbo.tblMixReturns.LostCts) AS LostCts, SUM(dbo.tblMixReturns.BroPcs) AS Bro, " & _
                            "SUM(dbo.tblMixReturns.ExtPcs) AS Ext, dbo.tblMixPacket.Grp, dbo.tblOrders.Subject, dbo.tblOrdersDtls.CutChg, dbo.tblOrdersDtls.NLineNo, " & _
                            "dbo.tblOrdersDtls.GrCount, SUM(dbo.tblMixReturns.GrPcs) AS GrPcs, MAX(dbo.tblMixReturns.RetDate) AS RetDate, dbo.tblOrders.Niruref " & _
                      "FROM dbo.tblMixPacket INNER JOIN dbo.tblMixReturns ON dbo.tblMixPacket.PktOrdNo = dbo.tblMixReturns.ParNo AND dbo.tblMixPacket.PktNo = dbo.tblMixReturns.PktNo INNER JOIN " & _
                            "dbo.tblOrders ON dbo.tblMixPacket.PktOrdNo = dbo.tblOrders.OrderNo INNER JOIN " & _
                            "dbo.tblOrdersDtls ON dbo.tblMixPacket.PktRefNo = dbo.tblOrdersDtls.RefNo AND dbo.tblMixPacket.Pktside = dbo.tblOrdersDtls.Side AND dbo.tblMixPacket.PktOrdNo = dbo.tblOrdersDtls.OrderNo INNER JOIN " & _
                            "dbo.tblAssortList ON dbo.tblMixPacket.AssortNo = dbo.tblAssortList.Assortment "

            strWhere = "WHERE (dbo.tblMixReturns.Sec = 18) AND (dbo.tblMixReturns.Status = 0) AND (dbo.tblMixReturns.ParNo = '" & txtOrder.Text & "') "

            If cmbRef.Text <> "" Then
                strWhere = strWhere & " AND dbo.tblMixPacket.PktRefNo = '" & Replace(cmbRef.Text, "'", "''") & "' "
            End If
            If cmbSide.Text <> "" Then
                strWhere = strWhere & " AND dbo.tblMixPacket.Pktside = '" & cmbSide.Text & "' "
            End If

            strOrder = "GROUP BY dbo.tblMixReturns.ParNo, dbo.tblMixReturns.PktNo, dbo.tblMixPacket.PktRefNo, dbo.tblMixPacket.AParNo, dbo.tblMixPacket.PktCts, dbo.tblMixPacket.PktPcs, " & _
                            "dbo.tblMixPacket.AssortNo, dbo.tblMixReturns.Status, dbo.tblMixPacket.Pktside, dbo.tblMixPacket.Grp, dbo.tblOrders.Subject, dbo.tblOrdersDtls.CutChg, " & _
                            "dbo.tblAssortList.MarketPrice, dbo.tblOrdersDtls.NLineNo, dbo.tblOrdersDtls.GrCount, dbo.tblOrders.Niruref " & _
                       "HAVING(SUM(dbo.tblMixReturns.RetCts) > 0) And (SUM(dbo.tblMixReturns.RetPcsT + dbo.tblMixReturns.RetPcsB) > 0) " & _
                       "ORDER BY dbo.tblMixReturns.ParNo, dbo.tblMixPacket.PktRefNo, dbo.tblMixPacket.Pktside"

            mStrSQL = strSelect & strWhere & strOrder
        Else
            If chkAll.Checked = True Then
                strSelect = "SELECT TOP (100) PERCENT dbo.tblMixReturns.ParNo, dbo.tblMixPacket.PktRefNo, dbo.tblMixPacket.Pktside, dbo.tblMixPacket.AParNo, dbo.tblMixPacket.AssortNo, " & _
                            "dbo.tblAssortList.MarketPrice, dbo.tblMixReturns.PktNo, SUM(dbo.tblMixReturns.RetPcsT + dbo.tblMixReturns.RetPcsB) AS RetPcs, SUM(dbo.tblMixReturns.RetCts) " & _
                            "AS RetCts, dbo.tblMixPacket.PktPcs, dbo.tblMixPacket.PktCts, dbo.tblMixReturns.Status, SUM(dbo.tblMixReturns.RejPcs) AS RejPcs, SUM(dbo.tblMixReturns.RejCts) " & _
                            "AS RejCts, SUM(dbo.tblMixReturns.LostPcs) AS LostPcs, SUM(dbo.tblMixReturns.LostCts) AS LostCts, SUM(dbo.tblMixReturns.BroPcs) AS Bro, " & _
                            "SUM(dbo.tblMixReturns.ExtPcs) AS Ext, dbo.tblMixPacket.Grp, dbo.tblOrders.Subject, dbo.tblOrdersDtls.CutChg, dbo.tblOrdersDtls.NLineNo, " & _
                            "dbo.tblOrdersDtls.GrCount, SUM(dbo.tblMixReturns.GrPcs) AS GrPcs, MAX(dbo.tblMixReturns.RetDate) AS RetDate, dbo.tblOrders.Niruref " & _
                      "FROM dbo.tblMixPacket INNER JOIN dbo.tblMixReturns ON dbo.tblMixPacket.PktOrdNo = dbo.tblMixReturns.ParNo AND dbo.tblMixPacket.PktNo = dbo.tblMixReturns.PktNo INNER JOIN " & _
                            "dbo.tblOrders ON dbo.tblMixPacket.PktOrdNo = dbo.tblOrders.OrderNo INNER JOIN " & _
                            "dbo.tblOrdersDtls ON dbo.tblMixPacket.PktRefNo = dbo.tblOrdersDtls.RefNo AND dbo.tblMixPacket.Pktside = dbo.tblOrdersDtls.Side AND dbo.tblMixPacket.PktOrdNo = dbo.tblOrdersDtls.OrderNo INNER JOIN " & _
                            "dbo.tblAssortList ON dbo.tblMixPacket.AssortNo = dbo.tblAssortList.Assortment "

                strWhere = "WHERE (dbo.tblMixReturns.Sec = 18) AND (dbo.tblMixReturns.Status = 0) "

                strOrder = "GROUP BY dbo.tblMixReturns.ParNo, dbo.tblMixReturns.PktNo, dbo.tblMixPacket.PktRefNo, dbo.tblMixPacket.AParNo, dbo.tblMixPacket.PktCts, dbo.tblMixPacket.PktPcs, " & _
                                "dbo.tblMixPacket.AssortNo, dbo.tblMixReturns.Status, dbo.tblMixPacket.Pktside, dbo.tblMixPacket.Grp, dbo.tblOrders.Subject, dbo.tblOrdersDtls.CutChg, " & _
                                "dbo.tblAssortList.MarketPrice, dbo.tblOrdersDtls.NLineNo, dbo.tblOrdersDtls.GrCount, dbo.tblOrders.Niruref " & _
                           "HAVING(SUM(dbo.tblMixReturns.RetCts) > 0) And (SUM(dbo.tblMixReturns.RetPcsT + dbo.tblMixReturns.RetPcsB) > 0) " & _
                           "ORDER BY dbo.tblMixReturns.ParNo, dbo.tblMixPacket.PktRefNo, dbo.tblMixPacket.Pktside"

                mStrSQL = strSelect & strWhere & strOrder
            Else
                MsgBox("Please enter the Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        End If
        Rs.Open(mStrSQL, AdoCN, 1, 1)
        intNoOfRecords = Rs.RecordCount

        flxDetails.Visible = True
        flxDetails.Rows.Clear()

        ExpProgress.Minimum = 0
        ExpProgress.Visible = True
        ExpProgress.Maximum = intNoOfRecords
        intCounter = 0

        If Rs.RecordCount Then
            Rs.MoveFirst()
            While Not Rs.EOF
                intCounter = intCounter + 1
                intExtPcs = 0
                vCharges = Rs.Fields("CutChg").Value & ""

                strSubject = Rs.Fields("Subject").Value & ""

                vAvgPrice = Rs.Fields("MarketPrice").Value

                intLostPcs = 0
                'rsComSql = New ADODB.Recordset
                'rsComSql.Open("SELECT SUM(LostPcs) AS LostPcs FROM tblMixReturns WHERE ParNo = '" & Rs.Fields("ParNo").Value & "' AND PktNo = '" & Rs.Fields("PktNo").Value & "' AND LostStatus = 0 AND LostPcs > 0", AdoCN, 1, 1)
                'If rsComSql.RecordCount Then
                '    If Not IsDBNull(rsComSql.Fields("LostPcs").Value) Then
                '        intLostPcs = rsComSql.Fields("LostPcs").Value
                '    End If
                'End If
                'rsComSql = Nothing

                intGrPcs = 0
                rsComSql = New ADODB.Recordset
                'rsComSql.Open("SELECT SUM(GrPcs) AS GrPcs FROM dbo.tblMixReturns WHERE ParNo = '" & Rs.Fields("ParNo").Value & "' AND PktNo = '" & Rs.Fields("PktNo").Value & "' AND Sec = 15 AND GrPcs > 0", AdoCN, 1, 1)
                rsComSql.Open("SELECT GrPcs FROM dbo.VW_MixGrPcs WHERE ParNo = '" & Rs.Fields("ParNo").Value & "' AND PktNo = '" & Rs.Fields("PktNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("GrPcs").Value) Then
                        intGrPcs = rsComSql.Fields("GrPcs").Value
                    End If
                End If
                rsComSql = Nothing

                RejP = 0
                mRej = 0
                vIssCts = 0
                rsComSql = New ADODB.Recordset
                'mStrSQL = "SELECT Sum(RejPcs) As RejPcs, Sum(RejCts) As RejCts, Sum(RghCts) As RghCts, Sum(ExtPcs) As ExtPcs, SUM(LostPcs) AS LostPcs " & _
                '          "FROM dbo.tblMixReturns " & _
                '          "WHERE ParNo = '" & Rs.Fields("ParNo").Value & "' AND PktNo = '" & Rs.Fields("PktNo").Value & "'"

                mStrSQL = "SELECT RejPcs, RejCts, RghCts, ExtPcs, LostPcs " & _
                          "FROM dbo.VW_MIXEffectsNew " & _
                          "WHERE ParNo = '" & Rs.Fields("ParNo").Value & "' AND PktNo = '" & Rs.Fields("PktNo").Value & "'"
                rsComSql.Open(mStrSQL, AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("LostPcs").Value) Then
                        intLostPcs = rsComSql.Fields("LostPcs").Value
                    End If
                    If Not IsDBNull(rsComSql.Fields("RejPCs").Value) Then
                        intExtPcs = rsComSql.Fields("ExtPcs").Value
                        vPktCts = Format(Rs.Fields("PktCts").Value, "#0.000")

                        mRej = Format(rsComSql.Fields("RejCts").Value, "#0.000")

                        RejP = rsComSql.Fields("RejPCs").Value
                        vIssCts = vPktCts - mRej

                        vPktPcs = Rs.Fields("PktPcs").Value - RejP + intExtPcs
                        vIssueCts = Format((vIssCts / vPktPcs) * (Rs.Fields("RetPcs").Value + intLostPcs), "#0.000")
                        vIssuePcs = Rs.Fields("RetPcs").Value + intLostPcs
                    Else
                        vIssueCts = Format((Rs.Fields("PktCts").Value / Rs.Fields("PktPcs").Value) * (Rs.Fields("RetPcs").Value + intLostPcs), "#0.000")
                        vIssuePcs = Rs.Fields("RetPcs").Value + intLostPcs
                    End If
                Else
                    vIssueCts = Format((Rs.Fields("PktCts").Value / Rs.Fields("PktPcs").Value) * (Rs.Fields("RetPcs").Value + intLostPcs), "#0.000")
                    vIssuePcs = Rs.Fields("RetPcs").Value + intLostPcs
                End If
                rsComSql = Nothing

                varParcelNo = IIf(IsDBNull((Rs.Fields("AParNo").Value)), "-", (Rs.Fields("AParNo").Value))

                flxDetails.Rows.Add(Rs.Fields("ParNo").Value, strSubject, Rs.Fields("PktRefNo").Value, Rs.Fields("Pktside").Value,
                                    varParcelNo, Rs.Fields("AssortNo").Value, Rs.Fields("MarketPrice").Value, Rs.Fields("PktNo").Value, Rs.Fields("RetPcs").Value,
                                    Format(Rs.Fields("RetCts").Value, "#0.000"), Rs.Fields("PktPcs").Value, Format(Rs.Fields("PktCts").Value, "#0.000"), Format(vIssueCts, "#0.000"),
                                    vCharges, False, 0, RejP, Format(mRej, "#0.000"), Rs.Fields("LostPcs").Value, Format(Rs.Fields("LostCts").Value, "#0.000"),
                                    Rs.Fields("Bro").Value, Rs.Fields("Ext").Value, Rs.Fields("Grp").Value, Rs.Fields("NLineNo").Value,
                                    Rs.Fields("GrCount").Value, intGrPcs, Rs.Fields("NLineNo").Value, vIssuePcs, Format(Rs.Fields("RetDate").Value, "yyyy/MM/dd"), Rs.Fields("Niruref").Value)

                Rs.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        Rs = Nothing

        ExpProgress.Visible = False
        If intNoOfRecords = 0 Then
            MsgBox("No Records Found", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        Else
            ExpProgress.Value = 0
        End If
        intCounter = 0
        intNoOfRecords = 0

        txtPcs.Text = CalTotalPcs(flxDetails, 8)
        txtCts.Text = CalTotalCts(flxDetails, 9)
    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        If optNew.Checked = True Then
            If chkReject.Checked = True Then
                Load_RejectPackets()
            Else
                If chkLab.Checked = True Then
                    Load_LabPackets()
                Else
                    Load_PolishedReturns()
                End If
            End If
        Else
            If chkReject.Checked = True Then

            Else
                If chkLab.Checked = True Then

                Else
                    Load_SavedData()
                End If
            End If
        End If
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(14, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(14, intRow).Value = False
            Next
        End If
        If chkReject.Checked = True Then
            txtPcs.Text = CalTotalPcs(flxDetails, 16)
            txtCts.Text = CalTotalCts(flxDetails, 12)
        Else
            txtPcs.Text = CalTotalPcs(flxDetails, 8)
            txtCts.Text = CalTotalCts(flxDetails, 9)
        End If
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        flxDetails.Rows.Clear()
        txtOrder.Text = ""
        cmbRef.Text = ""
        cmbRef.Items.Clear()
        cmbSide.Text = ""
        cmbSide.Items.Clear()
        chkSelect.Checked = False
    End Sub

    Private Sub Load_SavedData()
        Dim strGrp As String
        Dim strType As String
        Dim strSelect As String
        Dim strWhere As String
        Dim strOrder As String

        strType = "A"

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        If txtOrder.Text <> "" Then
            strSelect = "SELECT * FROM dbo.tblMixFinishOrders "
            strWhere = "WHERE Status = '" & strType & "' AND OrderNo = '" & txtOrder.Text & "' "
            If cmbRef.Text <> "" Then
                strWhere = strWhere & " AND Reference = '" & cmbRef.Text & "' "
            End If
            If cmbSide.Text <> "" Then
                strWhere = strWhere & " AND Side = '" & cmbSide.Text & "' "
            End If
            strOrder = "ORDER BY OrderNo, Reference, PacketNo"

            mStrSQL = strSelect & strWhere & strOrder
        Else
            mStrSQL = "SELECT * FROM dbo.tblMixFinishOrders WHERE Status = '" & strType & "' ORDER BY OrderNo, Reference, PacketNo"
        End If
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                strGrp = ""
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT Grp FROM dbo.tblMixPacket WHERE PktOrdNo = '" & rsComSql.Fields("OrderNo").Value & "' AND PktNo = '" & rsComSql.Fields("PacketNo").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strGrp = rsComSql_1.Fields("Grp").Value
                End If
                rsComSql_1 = Nothing

                flxDetails.Rows.Add(rsComSql.Fields("OrderNo").Value,
                                    rsComSql.Fields("Subject").Value,
                                    rsComSql.Fields("Reference").Value,
                                    rsComSql.Fields("Side").Value,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("Assortment").Value,
                                    Format(rsComSql.Fields("AssPrice").Value, "#0.00"),
                                    rsComSql.Fields("PacketNo").Value,
                                    rsComSql.Fields("FinishedPcs").Value,
                                    Format(rsComSql.Fields("FinishedCts").Value, "#0.000"),
                                    rsComSql.Fields("PacketPcs").Value,
                                    Format(rsComSql.Fields("PacketCts").Value, "#0.000"),
                                    Format(rsComSql.Fields("IssueCts").Value, "#0.000"),
                                    rsComSql.Fields("RateCode").Value,
                                    rsComSql.Fields("Export").Value,
                                    rsComSql.Fields("RecordNo").Value, 0, 0, 0, 0, 0, 0,
                                    strGrp,
                                    rsComSql.Fields("NLineNo").Value,
                                    rsComSql.Fields("GrPcs").Value,
                                    rsComSql.Fields("GrPcs").Value,
                                    rsComSql.Fields("NLineNo2").Value,
                                    rsComSql.Fields("IssuePcs").Value)

                rsComSql.MoveNext()
            End While
        Else
            MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing

        txtPcs.Text = CalTotalPcs(flxDetails, 8)
        txtCts.Text = CalTotalCts(flxDetails, 9)
    End Sub

    Private Sub SaveData()
        Dim iRow As Integer
        Dim vRecordNo As Double
        Dim strType As String
        Dim dblRecord As Double

        If flxDetails.Rows.Count = 0 Then
            MsgBox("No Records to Save", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        ExpProgress.Minimum = 0
        ExpProgress.Visible = True
        For iRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(26, iRow).Value <> "" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT NLineNo FROM dbo.tblOrdersDtls WHERE NLineNo = '" & flxDetails.Item(26, iRow).Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    MsgBox("Invalid Line No. - " & flxDetails.Item(26, iRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing
            Else
                MsgBox("Line No. cannot be blank", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        strType = "A"

        If optNew.Checked = True Then

            rsComSql = New ADODB.Recordset
            mStrSQL = "SELECT MAX(RecordNo) AS RecordNo FROM dbo.tblMixFinishOrders"
            rsComSql.Open(mStrSQL, AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("RecordNo").Value) Then
                    vRecordNo = rsComSql.Fields("RecordNo").Value + 1
                Else
                    vRecordNo = 1
                End If
            End If
            rsComSql = Nothing

            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = flxDetails.Rows.Count
            dblRecord = 0
            For iRow = 0 To flxDetails.RowCount - 1
                If flxDetails.Item(14, iRow).Value = True Or flxDetails.Item(14, iRow).Value = 1 Then
                    mStrSQL = "INSERT INTO dbo.tblMixFinishOrders(OrderNo,Subject,Reference,Side,ParNo,Assortment,AssPrice,PacketNo,FinishedPcs,FinishedCts,PacketPcs,PacketCts," & _
                                "IssueCts,RateCode,Export,Status,AuditNo,RecordNo,ModifyBy,NLineNo,GrPcs,SysFinCts,NLineNo2,DoneBy,IssuePcs) " & _
                              "VALUES('" & flxDetails.Item(0, iRow).Value & "','" & flxDetails.Item(1, iRow).Value & "','" & Replace(flxDetails.Item(2, iRow).Value, "'", "''") & "','" & flxDetails.Item(3, iRow).Value & "'," & _
                                "'" & flxDetails.Item(4, iRow).Value & "','" & flxDetails.Item(5, iRow).Value & "','" & CDbl(flxDetails.Item(6, iRow).Value) & "','" & flxDetails.Item(7, iRow).Value & "'," & _
                                "'" & CDbl(flxDetails.Item(8, iRow).Value) & "','" & CDbl(flxDetails.Item(9, iRow).Value) & "','" & CDbl(flxDetails.Item(10, iRow).Value) & "','" & CDbl(flxDetails.Item(11, iRow).Value) & "'," & _
                                "'" & CDbl(flxDetails.Item(12, iRow).Value) & "','" & flxDetails.Item(13, iRow).Value & "',1,'" & strType & "',0,'" & vRecordNo & "','" & PBUser_EmpNo & "'," & _
                                "'" & flxDetails.Item(23, iRow).Value & "','" & CDbl(flxDetails.Item(25, iRow).Value) & "','" & CDbl(flxDetails.Item(9, iRow).Value) & "','" & flxDetails.Item(26, iRow).Value & "'," & _
                                "'" & PBUser_EmpNo & "','" & CDbl(flxDetails.Item(27, iRow).Value) & "')"

                    AdoCN.Execute(mStrSQL)
                    If chkReject.Checked = False Then
                        AdoCN.Execute("UPDATE dbo.tblMixReturns SET Status = 2 WHERE ParNo = '" & flxDetails.Item(0, iRow).Value & "' AND PktNo = '" & flxDetails.Item(7, iRow).Value & "' AND  Sec = 18 AND Status  = 0")
                    Else
                        AdoCN.Execute("UPDATE dbo.tblMixReturns SET Status = 1 WHERE ParNo = '" & flxDetails.Item(0, iRow).Value & "' AND PktNo = '" & flxDetails.Item(7, iRow).Value & "' AND Status = 0")
                    End If

                    vRecordNo = vRecordNo + 1
                End If
                dblRecord = dblRecord + 1
                ExpProgress.Value = dblRecord
            Next
            MsgBox("Order Verification Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

            flxDetails.Rows.Clear()
            txtPcs.Text = "0"
            txtCts.Text = "0"
            txtActFinCts.Text = "0"
            txtDiffCts.Text = "0"
            ExpProgress.Visible = False
        Else
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = flxDetails.Rows.Count
            dblRecord = 0
            For iRow = 0 To flxDetails.RowCount - 1
                If flxDetails.Item(14, iRow).Value = True Or flxDetails.Item(14, iRow).Value = 1 Then
                    mStrSQL = "UPDATE dbo.tblMixFinishOrders SET PacketPcs = " & CDbl(flxDetails.Item(10, iRow).Value) & ",PacketCts = " & CDbl(flxDetails.Item(11, iRow).Value) & ",FinishedPcs = " & CDbl(flxDetails.Item(8, iRow).Value) & ",FinishedCts = " & CDbl(flxDetails.Item(9, iRow).Value) & "," & _
                                    "IssueCts = " & CDbl(flxDetails.Item(12, iRow).Value) & ",ModifyBy = '" & PBUser_EmpNo & "',Export = 1 " & _
                              "WHERE RecordNo = " & CDbl(flxDetails.Item(15, iRow).Value) & " AND OrderNo = '" & flxDetails.Item(0, iRow).Value & "' AND Status = '" & strType & "'"
                    AdoCN.Execute(mStrSQL)
                End If
                dblRecord = dblRecord + 1
                ExpProgress.Value = dblRecord
            Next

            MsgBox("Order Verification Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

            flxDetails.Rows.Clear()
            txtPcs.Text = "0"
            txtCts.Text = "0"
            txtActFinCts.Text = "0"
            txtDiffCts.Text = "0"
            ExpProgress.Visible = False
        End If
    End Sub

    Private Sub SaveDataLab()
        Dim iRow As Integer
        Dim vRecordNo As Double
        Dim strType As String

        For iRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(26, iRow).Value <> "" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT NLineNo FROM tblOrdersDtls WHERE NLineNo = '" & flxDetails.Item(26, iRow).Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    MsgBox("Invalid Line No. - " & flxDetails.Item(26, iRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing
            Else
                MsgBox("Line No. cannot be blank", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        strType = "A"

        If optNew.Checked = True Then
            vRecordNo = 1
            rsComSql = New ADODB.Recordset
            mStrSQL = "SELECT MAX(RecordNo) AS RecordNo FROM tblMixFinishOrdersR"
            rsComSql.Open(mStrSQL, AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("RecordNo").Value) Then
                    vRecordNo = rsComSql.Fields("RecordNo").Value + 1
                End If
            End If
            rsComSql = Nothing

            For iRow = 0 To flxDetails.RowCount - 1
                If flxDetails.Item(14, iRow).Value = True Or flxDetails.Item(14, iRow).Value = 1 Then
                    mStrSQL = "INSERT INTO tblMixFinishOrdersR(OrderNo,Subject,Reference,Side,ParNo,Assortment,AssPrice,PacketNo,FinishedPcs,FinishedCts,PacketPcs,PacketCts," & _
                                "IssueCts,RateCode,Export,Status,AuditNo,RecordNo,ModifyBy,NLineNo,GrPcs,SysFinCts,NLineNo2,DoneBy,IssuePcs) " & _
                              "VALUES('" & flxDetails.Item(0, iRow).Value & "','" & flxDetails.Item(1, iRow).Value & "','" & flxDetails.Item(2, iRow).Value & "','" & flxDetails.Item(3, iRow).Value & "'," & _
                                "'" & flxDetails.Item(4, iRow).Value & "','" & flxDetails.Item(5, iRow).Value & "','" & CDbl(flxDetails.Item(6, iRow).Value) & "','" & flxDetails.Item(7, iRow).Value & "'," & _
                                "'" & CDbl(flxDetails.Item(8, iRow).Value) & "','" & CDbl(flxDetails.Item(9, iRow).Value) & "','" & CDbl(flxDetails.Item(10, iRow).Value) & "','" & CDbl(flxDetails.Item(11, iRow).Value) & "'," & _
                                "'" & CDbl(flxDetails.Item(12, iRow).Value) & "','" & flxDetails.Item(13, iRow).Value & "',1,'" & strType & "',0,'" & vRecordNo & "','" & PBUser_EmpNo & "'," & _
                                "'" & flxDetails.Item(23, iRow).Value & "','" & CDbl(flxDetails.Item(25, iRow).Value) & "','" & CDbl(flxDetails.Item(9, iRow).Value) & "','" & flxDetails.Item(26, iRow).Value & "'," & _
                                "'" & PBUser_EmpNo & "','" & CDbl(flxDetails.Item(27, iRow).Value) & "')"

                    AdoCN.Execute(mStrSQL)
                    AdoCN.Execute("UPDATE tblMixReturns SET RejStatus = 2 WHERE ID = " & CDbl(flxDetails.Item(15, iRow).Value) & "")

                    vRecordNo = vRecordNo + 1
                End If
            Next
            MsgBox("Order Verification Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

            flxDetails.Rows.Clear()
            txtPcs.Text = "0"
            txtCts.Text = "0"
            txtActFinCts.Text = "0"
            txtDiffCts.Text = "0"
        Else
            For iRow = 0 To flxDetails.RowCount - 1
                If flxDetails.Item(14, iRow).Value = True Or flxDetails.Item(14, iRow).Value = 1 Then
                    mStrSQL = "UPDATE tblMixFinishOrdersR SET PacketPcs = " & CDbl(flxDetails.Item(10, iRow).Value) & ",PacketCts = " & CDbl(flxDetails.Item(11, iRow).Value) & ",FinishedPcs = " & CDbl(flxDetails.Item(8, iRow).Value) & ",FinishedCts = " & CDbl(flxDetails.Item(9, iRow).Value) & "," & _
                                    "IssueCts = " & CDbl(flxDetails.Item(12, iRow).Value) & ",ModifyBy = '" & PBUser_EmpNo & "',Export = 1 " & _
                              "WHERE RecordNo = " & CDbl(flxDetails.Item(15, iRow).Value) & " AND OrderNo = '" & flxDetails.Item(0, iRow).Value & "' AND Status = '" & strType & "'"
                    AdoCN.Execute(mStrSQL)
                End If
            Next

            MsgBox("Order Verification Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

            flxDetails.Rows.Clear()
            txtPcs.Text = "0"
            txtCts.Text = "0"
            txtActFinCts.Text = "0"
            txtDiffCts.Text = "0"
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If chkReject.Checked = True Then
            SaveData()
        Else
            If chkLab.Checked = True Then
                SaveDataLab()
            Else
                SaveData()
            End If
        End If
    End Sub

    Private Sub Load_RejectPackets()
        flxDetails.Rows.Clear()

        ExpProgress.Minimum = 0
        ExpProgress.Visible = True
        intCounter = 0

        rsComSql = New ADODB.Recordset
        If chkAll.Checked = True Then
            mStrSQL = "SELECT TOP (100) PERCENT dbo.tblMixReturns.ParNo, dbo.tblMixReturns.PktNo, dbo.tblMixPacket.PktPcs, " & _
                        "SUM(dbo.tblMixReturns.RejPcs + dbo.tblMixReturns.BroPcs - dbo.tblMixReturns.ExtPcs) AS RejPcs, dbo.tblMixPacket.PktCts, " & _
                        "ROUND(SUM(dbo.tblMixReturns.RejCts),3) AS RejCts, ROUND(dbo.tblMixPacket.PktCts - SUM(dbo.tblMixReturns.RejCts), 3) AS DifCts, dbo.tblMixPacket.AssortNo, dbo.tblOrders.Subject," & _
                        "dbo.tblMixPacket.PktRefNo, dbo.tblMixPacket.Pktside, dbo.tblMixPacket.AParNo, dbo.tblAssortList.MarketPrice, dbo.tblMixPacket.Grp " & _
                      "FROM dbo.tblMixPacket INNER JOIN dbo.tblMixReturns ON dbo.tblMixPacket.PktOrdNo = dbo.tblMixReturns.ParNo AND dbo.tblMixPacket.PktNo = dbo.tblMixReturns.PktNo INNER JOIN " & _
                        "dbo.tblOrders ON dbo.tblMixPacket.PktOrdNo = dbo.tblOrders.OrderNo INNER JOIN dbo.tblAssortList ON dbo.tblMixPacket.AssortNo = dbo.tblAssortList.Assortment " & _
                      "WHERE (dbo.tblMixReturns.Status = 0) " & _
                      "GROUP BY dbo.tblMixReturns.ParNo, dbo.tblMixReturns.PktNo, dbo.tblMixPacket.PktPcs, dbo.tblMixPacket.PktCts, dbo.tblMixPacket.AssortNo, dbo.tblOrders.Subject, " & _
                        "dbo.tblMixPacket.PktRefNo , dbo.tblMixPacket.Pktside, dbo.tblMixPacket.AParNo, dbo.tblAssortList.MarketPrice, dbo.tblMixPacket.Grp " & _
                      "HAVING (dbo.tblMixPacket.PktPcs = SUM(dbo.tblMixReturns.RejPcs + dbo.tblMixReturns.BroPcs - dbo.tblMixReturns.ExtPcs)) AND " & _
                        "(ROUND(dbo.tblMixPacket.PktCts - SUM(dbo.tblMixReturns.RejCts), 3) > 0) " & _
                      "ORDER BY dbo.tblMixReturns.ParNo, dbo.tblMixReturns.PktNo"
        Else
            mStrSQL = "SELECT TOP (100) PERCENT dbo.tblMixReturns.ParNo, dbo.tblMixReturns.PktNo, dbo.tblMixPacket.PktPcs, " & _
                        "SUM(dbo.tblMixReturns.RejPcs + dbo.tblMixReturns.BroPcs - dbo.tblMixReturns.ExtPcs) AS RejPcs, dbo.tblMixPacket.PktCts, " & _
                        "ROUND(SUM(dbo.tblMixReturns.RejCts),3) AS RejCts, ROUND(dbo.tblMixPacket.PktCts - SUM(dbo.tblMixReturns.RejCts), 3) AS DifCts, dbo.tblMixPacket.AssortNo, dbo.tblOrders.Subject, " & _
                        "dbo.tblMixPacket.PktRefNo , dbo.tblMixPacket.Pktside, dbo.tblMixPacket.AParNo, dbo.tblAssortList.MarketPrice, dbo.tblMixPacket.Grp " & _
                      "FROM dbo.tblMixPacket INNER JOIN dbo.tblMixReturns ON dbo.tblMixPacket.PktOrdNo = dbo.tblMixReturns.ParNo AND dbo.tblMixPacket.PktNo = dbo.tblMixReturns.PktNo INNER JOIN " & _
                        "dbo.tblOrders ON dbo.tblMixPacket.PktOrdNo = dbo.tblOrders.OrderNo INNER JOIN dbo.tblAssortList ON dbo.tblMixPacket.AssortNo = dbo.tblAssortList.Assortment INNER JOIN " & _
                        "dbo.VW_MIXFinishNotExportOrd ON dbo.tblMixReturns.ParNo = dbo.VW_MIXFinishNotExportOrd.ParNo " & _
                      "WHERE (dbo.tblMixReturns.Status = 0) " & _
                      "GROUP BY dbo.tblMixReturns.ParNo, dbo.tblMixReturns.PktNo, dbo.tblMixPacket.PktPcs, dbo.tblMixPacket.PktCts, dbo.tblMixPacket.AssortNo, dbo.tblOrders.Subject, " & _
                        "dbo.tblMixPacket.PktRefNo, dbo.tblMixPacket.Pktside, dbo.tblMixPacket.AParNo, dbo.tblAssortList.MarketPrice, dbo.tblMixPacket.Grp " & _
                      "HAVING (dbo.tblMixPacket.PktPcs = SUM(dbo.tblMixReturns.RejPcs + dbo.tblMixReturns.BroPcs - dbo.tblMixReturns.ExtPcs)) AND " & _
                        "(ROUND(dbo.tblMixPacket.PktCts - SUM(dbo.tblMixReturns.RejCts), 3) > 0) " & _
                      "ORDER BY dbo.tblMixReturns.ParNo, dbo.tblMixReturns.PktNo"
        End If
        
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            ExpProgress.Maximum = rsComSql.RecordCount
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                intCounter = intCounter + 1

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT CutChg, NLineNo FROM tblOrdersDtls WHERE OrderNo = '" & rsComSql.Fields("ParNo").Value & "' AND RefNo = '" & Replace(rsComSql.Fields("PktRefNo").Value, "'", "''") & "' AND Side = '" & rsComSql.Fields("Pktside").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("Subject").Value,
                                    rsComSql.Fields("PktRefNo").Value,
                                    rsComSql.Fields("Pktside").Value,
                                    rsComSql.Fields("AParNo").Value,
                                    rsComSql.Fields("AssortNo").Value,
                                    Format(rsComSql.Fields("MarketPrice").Value, "#0.00"),
                                    rsComSql.Fields("PktNo").Value, "0", "0.000",
                                    rsComSql.Fields("PktPcs").Value,
                                    Format(rsComSql.Fields("PktCts").Value, "#0.000"),
                                    Format(rsComSql.Fields("DifCts").Value, "#0.000"),
                                    rsComSql_1.Fields("CutChg").Value, 0, 0,
                                    rsComSql.Fields("RejPcs").Value,
                                    Format(rsComSql.Fields("RejCts").Value, "#0.000"), 0, 0, 0, 0,
                                    rsComSql.Fields("Grp").Value,
                                    rsComSql_1.Fields("NLineNo").Value, 0, 0,
                                    rsComSql_1.Fields("NLineNo").Value, 0)
                End If
                rsComSql_1 = Nothing

                rsComSql.MoveNext()
                ExpProgress.Value = intCounter
            End While
        Else
            MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing

        ExpProgress.Visible = False
        ExpProgress.Value = 0
    End Sub

    Private Sub Load_LabPackets()
        flxDetails.Rows.Clear()

        ExpProgress.Minimum = 0
        ExpProgress.Visible = True
        intCounter = 0

        rsComSql = New ADODB.Recordset
        mStrSQL = "SELECT TOP (100) PERCENT dbo.tblMixReturns.ParNo, dbo.tblMixReturns.PktNo, dbo.tblMixReturns.Sec, dbo.tblMixReturns.EmpNo, dbo.tblMixReturns.RejPcs, dbo.tblMixReturns.RejCts, " & _
                        "dbo.tblMixReturns.RghCts, dbo.tblMixPacket.AssortNo, dbo.tblMixReturns.RejReason, dbo.tblMixReturns.RetDate, dbo.tblMixPacket.Grp, dbo.tblMixReturns.ID, dbo.tblMixPacket.PktRefNo, " & _
                        "dbo.tblMixPacket.Pktside, dbo.tblOrders.Subject, dbo.tblOrders.Niruref, dbo.tblAssortList.CurrentCost, dbo.tblAssortList.MarketPrice, dbo.tblOrdersDtls.MaxCost, dbo.tblOrdersDtls.MaxType, " & _
                        "dbo.tblMixPacket.PktPcs, dbo.tblMixPacket.PktCts, dbo.tblOrdersDtls.NLineNo, dbo.tblOrdersDtls.GrCount, dbo.tblOrdersDtls.CutChg " & _
                      "FROM dbo.tblMixReturns INNER JOIN dbo.tblMixPacket ON dbo.tblMixReturns.ParNo = dbo.tblMixPacket.PktOrdNo AND dbo.tblMixReturns.PktNo = dbo.tblMixPacket.PktNo INNER JOIN " & _
                        "dbo.tblOrders ON dbo.tblMixPacket.PktOrdNo = dbo.tblOrders.OrderNo INNER JOIN dbo.tblAssortList ON dbo.tblMixPacket.AssortNo = dbo.tblAssortList.Assortment INNER JOIN " & _
                        "dbo.tblOrdersDtls ON dbo.tblOrders.OrderNo = dbo.tblOrdersDtls.OrderNo AND dbo.tblMixPacket.PktRefNo = dbo.tblOrdersDtls.RefNo AND dbo.tblMixPacket.Pktside = dbo.tblOrdersDtls.Side " & _
                      "WHERE (dbo.tblMixReturns.RejStatus = 1) AND (dbo.tblMixReturns.RejPcs > 0) AND (dbo.tblMixReturns.RejReason = 'DFI Refer Reject') AND (dbo.tblMixReturns.Sec = 16) " & _
                      "ORDER BY dbo.tblMixReturns.ParNo, dbo.tblMixReturns.PktNo, dbo.tblMixReturns.Sec, dbo.tblMixReturns.RejReason"

        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            ExpProgress.Maximum = rsComSql.RecordCount
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                intCounter = intCounter + 1

                flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("Subject").Value,
                                    rsComSql.Fields("PktRefNo").Value,
                                    rsComSql.Fields("Pktside").Value,
                                    "",
                                    rsComSql.Fields("AssortNo").Value,
                                    Format(rsComSql.Fields("MarketPrice").Value, "#0.00"),
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("RejPcs").Value,
                                    rsComSql.Fields("RejCts").Value,
                                    rsComSql.Fields("PktPcs").Value,
                                    Format(rsComSql.Fields("PktCts").Value, "#0.000"),
                                    Format(rsComSql.Fields("RejCts").Value, "#0.000"),
                                    rsComSql.Fields("CutChg").Value, 0,
                                    rsComSql.Fields("ID").Value,
                                    rsComSql.Fields("RejPcs").Value,
                                    Format(rsComSql.Fields("RejCts").Value, "#0.000"), 0, 0, 0, 0,
                                    rsComSql.Fields("Grp").Value,
                                    rsComSql.Fields("NLineNo").Value,
                                    rsComSql.Fields("GrCount").Value, 0,
                                    rsComSql.Fields("NLineNo").Value, 0,
                                    Format(rsComSql.Fields("RetDate").Value, "yyyy/MM/dd"))

                rsComSql.MoveNext()
                ExpProgress.Value = intCounter
            End While
        Else
            MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing

        ExpProgress.Visible = False
        ExpProgress.Value = 0
    End Sub

    Private Sub optNew_CheckedChanged(sender As Object, e As EventArgs) Handles optNew.CheckedChanged
        flxDetails.Rows.Clear()
    End Sub

    Private Sub optEdit_CheckedChanged(sender As Object, e As EventArgs) Handles optEdit.CheckedChanged
        flxDetails.Rows.Clear()
    End Sub

    Private Sub chkReject_CheckedChanged(sender As Object) Handles chkReject.CheckedChanged
        flxDetails.Rows.Clear()
        If chkReject.Checked = True Then
            chkLab.Checked = False
        End If
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub txtOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrder.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            cmbRef.Items.Clear()
            cmbSide.Items.Clear()

            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT RefNo FROM tblOrdersDtls WHERE OrderNo = '" & CInt(txtOrder.Text) & "' GROUP BY RefNo ORDER BY RefNo", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                rsComSql_1.MoveFirst()
                While Not rsComSql_1.EOF
                    cmbRef.Items.Add(rsComSql_1.Fields("RefNo").Value)

                    rsComSql_1.MoveNext()
                End While
            End If
            rsComSql_1 = Nothing
        End If
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(14).EditedFormattedValue = True Then
                CalTotalPcs = CalTotalPcs + Val(flxSample.Item(intCalRow, intRow).Value)
            End If
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(14).EditedFormattedValue = True Then
                CalTotalCts = CalTotalCts + Val(flxSample.Item(intCalRow, intRow).Value)
            End If
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 14 Then
            If chkReject.Checked = False Then
                txtPcs.Text = CalTotalPcs(flxDetails, 8)
                txtCts.Text = CalTotalCts(flxDetails, 9)
            Else
                txtPcs.Text = CalTotalPcs(flxDetails, 16)
                txtCts.Text = CalTotalCts(flxDetails, 12)
            End If
        End If
    End Sub

    Private Sub flxDetails_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellValueChanged
        If chkReject.Checked = False Then
            txtPcs.Text = CalTotalPcs(flxDetails, 8)
            txtCts.Text = CalTotalCts(flxDetails, 9)
        Else
            txtPcs.Text = CalTotalPcs(flxDetails, 16)
            txtCts.Text = CalTotalCts(flxDetails, 12)
        End If
    End Sub

    Private Sub frm_MixFinishOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        If strDBName = "DiaStock" Then
            strFolderPath = "DiaSalesMix\"
        ElseIf strDBName = "DiaSales" Then
            strFolderPath = "DiaSalesMix\"
        Else
            strFolderPath = "DiaShareMix\"
        End If
    End Sub

    Private Sub cmbRef_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRef.SelectedIndexChanged
        cmbSide.Items.Clear()
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT Side FROM tblOrdersDtls WHERE OrderNo = '" & CInt(txtOrder.Text) & "' AND RefNo = '" & Replace(cmbRef.Text, "'", "''") & "' GROUP BY Side ORDER BY Side", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                cmbSide.Items.Add(rsComSql_1.Fields("Side").Value)

                rsComSql_1.MoveNext()
            End While
        End If
        rsComSql_1 = Nothing
    End Sub

    Private Sub txtActFinCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtActFinCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtActFinCts.Text)
        If Asc(e.KeyChar) = 13 Then
            If txtActFinCts.Text <> "" Then
                txtDiffCts.Text = Format(Math.Round(CDbl(txtActFinCts.Text) - CDbl(txtCts.Text), 3), "#0.000")
                EditFinishedCts()
            End If
        End If
    End Sub

    Private Sub EditFinishedCts()
        Dim intRow As Integer
        Dim intMaxRow As Integer
        Dim dblCurCts As Double

        If txtOrder.Text = "" Then Exit Sub
        If txtDiffCts.Text = "" Then Exit Sub
        If CDbl(txtDiffCts.Text) = 0 Then Exit Sub

        intMaxRow = 0
        dblCurCts = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(14, intRow).Value = True Or flxDetails.Item(14, intRow).Value = 1 Then
                If dblCurCts < CDbl(flxDetails.Item(9, intRow).Value) Then
                    dblCurCts = CDbl(flxDetails.Item(9, intRow).Value)
                    intMaxRow = intRow
                End If
            End If
        Next

        flxDetails.Item(9, intMaxRow).Value = Format(Math.Round(CSng(flxDetails.Item(9, intMaxRow).Value) + CSng(txtDiffCts.Text), 3), "#0.000")
    End Sub

    Private Sub chkLab_CheckedChanged(sender As Object) Handles chkLab.CheckedChanged
        flxDetails.Rows.Clear()
        If chkLab.Checked = True Then
            chkReject.Checked = False
        End If
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPackingLabel.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPackingList.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub
End Class