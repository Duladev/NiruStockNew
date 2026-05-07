
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RprBrutingRounds
    Dim ParcelNo As String
    Dim PacketNo As String

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ClearFields()
        cmbDept.Text = ""
        txtParNo.Text = ""
        txtPktNo.Text = ""
        txtNewParNo.Text = ""
        txtTotPcs.Text = ""
        txtTotCts.Text = ""
        txtSelPcs.Text = ""
        txtSelCts.Text = ""
        txtOrgParNo.Text = ""
        txtAssortment.Text = ""
        txtCategory.Text = ""
        flxDetails.Rows.Clear()
        flxSelected.Rows.Clear()
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

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub Load_FinishedPackets()
        Dim intTotPcs As Integer
        Dim dblTotCts As Double

        flxDetails.Rows.Clear()
        If txtParNo.Text <> "" And txtNewParNo.Text <> "" Then
            txtParNo.Text = UCase(Trim(txtParNo.Text))
            txtNewParNo.Text = UCase(Trim(txtNewParNo.Text))
            intTotPcs = 0
            dblTotCts = 0
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrPacket.ParNo, dbo.tblRPrPacket.PktNo, dbo.tblRPrPacket.PktColor, dbo.tblRPrPacket.PktClarity, dbo.tblRPrPacket.PktCut, " & _
                            "dbo.tblRPrReturns.RetPcsT + dbo.tblRPrReturns.RetPcsB AS RetPcs, dbo.tblRPrReturns.RetCts, dbo.tblRPrPacket.FinCts, dbo.tblRPrPacket.PktCts,  " & _
                            "dbo.tblRPrPacket.PktID, dbo.tblRPrPacket.PktIDNew, dbo.tblRPrPacket.Value, dbo.tblRPrPacket.Sieve, dbo.tblRPrPacket.Tension, dbo.tblRPrPacket.PktSize, dbo.tblRPrPacket.Flo, dbo.tblRPrPacket.StoneNo, dbo.tblRPrPacket.Width " & _
                          "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrPacket.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrReturns.ParNo AND  " & _
                            "dbo.tblRPrPacket.PktNo = dbo.tblRPrReturns.PktNo " & _
                          "WHERE (dbo.tblRPrPacket.Department = 'RoughBruting') AND (dbo.tblRPrReturns.Sec = 20) AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrPacket.Trf = 0) " & _
                          "ORDER BY dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("RetPcs").Value) Then
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        If rsComSql.Fields("RetPcs").Value > 0 Then
                            flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                                rsComSql.Fields("PktNo").Value,
                                                rsComSql.Fields("RetPcs").Value,
                                                rsComSql.Fields("RetCts").Value,
                                                rsComSql.Fields("FinCts").Value,
                                                rsComSql.Fields("Sieve").Value,
                                                rsComSql.Fields("Value").Value,
                                                rsComSql.Fields("PktID").Value,
                                                rsComSql.Fields("PktColor").Value,
                                                rsComSql.Fields("PktClarity").Value,
                                                rsComSql.Fields("PktCut").Value,
                                                rsComSql.Fields("PktCts").Value,
                                                rsComSql.Fields("Tension").Value,
                                                rsComSql.Fields("PktSize").Value,
                                                rsComSql.Fields("Flo").Value,
                                                rsComSql.Fields("PktIDNew").Value,
                                                rsComSql.Fields("StoneNo").Value,
                                                rsComSql.Fields("Width").Value)

                            intTotPcs = intTotPcs + rsComSql.Fields("RetPcs").Value
                            dblTotCts = dblTotCts + rsComSql.Fields("RetCts").Value
                        End If

                        rsComSql.MoveNext()
                    End While
                End If
            End If
            rsComSql = Nothing

            txtTotPcs.Text = intTotPcs
            txtTotCts.Text = Math.Round(dblTotCts, 3)
        End If
    End Sub

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Datavalid = False
        Parcel = True
        Instring = UCase(InputBox("Enter Par/Pkt Number"))
        ParcelLen = Len(Instring)
        If ParcelLen > 10 Then
            Datavalid = True

            ParcelNo = Mid(Instring, 1, ParcelLen - 4)
            PacketNo = strRight(Instring, 4)
        End If

        If Datavalid = True Then
            txtParNo.Text = ParcelNo
            txtPktNo.Text = PacketNo

            ShowPacket()
        Else
            MsgBox("Invalid Parcel No./Packet No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub ShowPacket()
        Dim intRow As Integer

        If Len(txtPktNo.Text) = 4 Then
            If txtNewParNo.Text = "" Then Exit Sub
            If txtParNo.Text = "" Then Exit Sub

            txtNewParNo.Text = UCase(txtNewParNo.Text)

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblParcel WHERE GrpParNo = '" & txtNewParNo.Text & "' AND Depart = '" & cmbDept.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount <= 0 Then
                MsgBox("Invalid Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            For intRow = 0 To flxDetails.Rows.Count - 1
                If txtParNo.Text = flxDetails.Item(3, intRow).Value Then

                Else
                    MsgBox("Invalid Parcel", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Next

            For intRow = 0 To flxDetails.Rows.Count - 1
                If txtPktNo.Text = flxDetails.Item(1, intRow).Value Then
                    MsgBox("Already Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Next

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrPacket.ParNo, dbo.tblRPrPacket.PktNo, dbo.tblRPrPacket.PktColor, dbo.tblRPrPacket.PktClarity, dbo.tblRPrPacket.PktCut, " & _
                                "dbo.tblRPrReturns.RetPcsT + dbo.tblRPrReturns.RetPcsB AS RetPcs, dbo.tblRPrReturns.RetCts, dbo.tblRPrPacket.FinCts, dbo.tblRPrPacket.PktCts,  " & _
                                "dbo.tblRPrPacket.PktID, dbo.tblRPrPacket.PktIDNew, dbo.tblRPrPacket.Value, dbo.tblRPrPacket.Sieve, dbo.tblRPrPacket.Tension, dbo.tblRPrPacket.PktSize, dbo.tblRPrPacket.Flo, dbo.tblRPrPacket.StoneNo, dbo.tblRPrPacket.Width " & _
                              "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrPacket.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrReturns.ParNo AND  " & _
                                "dbo.tblRPrPacket.PktNo = dbo.tblRPrReturns.PktNo " & _
                              "WHERE (dbo.tblRPrPacket.Department = 'RoughBruting') AND (dbo.tblRPrReturns.Sec = 20) AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrPacket.Trf = 0) AND (dbo.tblRPrPacket.PktNo = '" & txtPktNo.Text & "') " & _
                              "ORDER BY dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("RetPcs").Value) Then
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        If rsComSql.Fields("RetPcs").Value > 0 Then
                            flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                                rsComSql.Fields("PktNo").Value,
                                                rsComSql.Fields("RetPcs").Value,
                                                rsComSql.Fields("RetCts").Value,
                                                rsComSql.Fields("FinCts").Value,
                                                rsComSql.Fields("Sieve").Value,
                                                rsComSql.Fields("Value").Value,
                                                rsComSql.Fields("PktID").Value,
                                                rsComSql.Fields("PktColor").Value,
                                                rsComSql.Fields("PktClarity").Value,
                                                rsComSql.Fields("PktCut").Value,
                                                rsComSql.Fields("PktCts").Value,
                                                rsComSql.Fields("Tension").Value,
                                                rsComSql.Fields("PktSize").Value,
                                                rsComSql.Fields("Flo").Value,
                                                rsComSql.Fields("PktIDNew").Value,
                                                rsComSql.Fields("StoneNo").Value,
                                                rsComSql.Fields("Width").Value)
                        End If

                        rsComSql.MoveNext()
                    End While
                End If
            End If
            rsComSql = Nothing
            txtTotPcs.Text = CalTotalPcs(flxDetails)
            txtTotCts.Text = CalTotalCts(flxDetails)
        End If
    End Sub

    Private Sub txtPktNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            ShowPacket()
        End If
    End Sub

    Private Sub frm_RprBrutingRounds_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        ClearFields()
        Load_DepartmentProd(cmbDept)
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        Dim intRow As Integer
        Dim dblSize As Double
        Dim strUnit As String

        If txtNewParNo.Text = "" Then Exit Sub
        If txtOrgParNo.Text = "" Then Exit Sub
        If txtAssortment.Text = "" Then Exit Sub
        If txtCategory.Text = "" Then Exit Sub

        For intRow = 0 To flxSelected.Rows.Count - 1
            If flxDetails.Item(1, flxDetails.CurrentRow.Index).Value = flxSelected.Item(1, intRow).Value Then
                MsgBox("Already Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        dblSize = Math.Round(CDbl(flxDetails.Item(2, flxDetails.CurrentRow.Index).Value) / CDbl(flxDetails.Item(3, flxDetails.CurrentRow.Index).Value), 3)
        dblSize = Math.Round(dblSize, 3)

        strUnit = ""
        Select Case cmbDept.Text
            Case "Rounds"
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblRndIncentiveCat WHERE FromSize <= " & dblSize & " AND ToSize >= " & dblSize & " AND Category = '" & txtCategory.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strUnit = Trim(rsComSql.Fields("Unit").Value)
                End If
                rsComSql = Nothing

            Case "Baguettes"
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblBAGIncentiveCat WHERE FromSize <= " & dblSize & " AND ToSize >= " & dblSize & "", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strUnit = Trim(rsComSql.Fields("Unit").Value)
                End If
                rsComSql = Nothing

            Case "Emerald"
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblExtIncentiveCat WHERE Department = '" & cmbDept.Text & "' AND FromSize <= " & dblSize & " AND ToSize >= " & dblSize & "", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strUnit = Trim(rsComSql.Fields("Unit").Value)
                End If
                rsComSql = Nothing

        End Select

        If txtParNo.Text = txtNewParNo.Text Then
            flxSelected.Rows.Add(txtNewParNo.Text,
                                 flxDetails.Item(1, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(2, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(3, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(4, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(5, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(6, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(7, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(8, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(9, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(10, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(1, flxDetails.CurrentRow.Index).Value,
                                 strUnit,
                                 flxDetails.Item(11, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(12, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(0, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(13, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(14, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(15, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(16, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(17, flxDetails.CurrentRow.Index).Value)
        Else
            flxSelected.Rows.Add(txtNewParNo.Text,
                                 flxDetails.Item(1, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(2, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(3, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(4, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(5, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(6, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(7, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(8, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(9, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(10, flxDetails.CurrentRow.Index).Value,
                                 "",
                                 strUnit,
                                 flxDetails.Item(11, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(12, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(0, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(13, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(14, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(15, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(16, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(17, flxDetails.CurrentRow.Index).Value)
        End If
        

        flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)
        txtTotPcs.Text = CalTotalPcs(flxDetails)
        txtTotCts.Text = CalTotalCts(flxDetails)
        txtSelPcs.Text = CalTotalPcs(flxSelected)
        txtSelCts.Text = CalTotalCts(flxSelected)

        txtPktNo.Text = ""
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        SavePacket()
    End Sub

    Private Sub SavePacket()
        Dim intRow As Integer
        Dim dblExtYld As Double

        Dim dblTrfPcs As Double
        Dim dblTrfCts As Double

        Dim dblIssPcs As Double
        Dim dblIssCts As Double

        If CDbl(txtSelPcs.Text) <= 0 Then
            MsgBox("No Records Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblParcel WHERE GrpParNo = '" & txtNewParNo.Text & "' AND Depart = '" & cmbDept.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount <= 0 Then
            MsgBox("Invalid Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If Mid(txtParNo.Text, 3, 4) <> Mid(txtNewParNo.Text, 3, 4) Then
            MsgBox("Parcel No. Mismatch", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        dblTrfPcs = 0
        dblTrfCts = 0
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT SUM(NewACTPcs) AS Pcs, ROUND(SUM(NewACTCts), 3) AS Cts " & _
                      "FROM tblDep_Trf WHERE (DCLParcelNo = '" & Mid(txtNewParNo.Text, 1, 6) & "') AND (Department = '" & cmbDept.Text & "')", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                dblTrfPcs = rsComSql.Fields("Pcs").Value
                dblTrfCts = rsComSql.Fields("Cts").Value
            End If
        End If
        rsComSql = Nothing

        dblIssPcs = 0
        dblIssCts = 0

        Select Case cmbDept.Text
            Case "Rounds"
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(dbo.tblRndPacket.PktPcs) AS PktPcs, ROUND(SUM(dbo.tblRndPacket.PktCts), 3) AS PktCts " & _
                              "FROM dbo.tblRndPacket INNER JOIN dbo.tblParcel ON dbo.tblRndPacket.ParNo = dbo.tblParcel.GrpParNo " & _
                              "WHERE (dbo.tblParcel.ReIssue = 0) AND (dbo.tblParcel.Depart = '" & cmbDept.Text & "') AND " & _
                                    "(LEFT(dbo.tblRndPacket.ParNo, 6) = '" & Mid(txtNewParNo.Text, 1, 6) & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("PktPcs").Value) Then
                        dblIssPcs = rsComSql.Fields("PktPcs").Value
                        dblIssCts = rsComSql.Fields("PktCts").Value
                    End If
                End If
                rsComSql = Nothing

            Case "Baguettes"
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(dbo.tblBAGPacket.PktPcs) AS PktPcs, ROUND(SUM(dbo.tblBAGPacket.PktCts), 3) AS PktCts " & _
                              "FROM dbo.tblBAGPacket INNER JOIN dbo.tblParcel ON dbo.tblBAGPacket.ParNo = dbo.tblParcel.GrpParNo " & _
                              "WHERE (dbo.tblParcel.ReIssue = 0) AND (dbo.tblParcel.Depart = '" & cmbDept.Text & "') AND " & _
                                    "(LEFT(dbo.tblBAGPacket.ParNo, 6) = '" & Mid(txtNewParNo.Text, 1, 6) & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("PktPcs").Value) Then
                        dblIssPcs = rsComSql.Fields("PktPcs").Value
                        dblIssCts = rsComSql.Fields("PktCts").Value
                    End If
                End If
                rsComSql = Nothing

            Case "Emerald"
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(dbo.tblExtPacket.PktPcs) AS PktPcs, ROUND(SUM(dbo.tblExtPacket.PktCts), 3) AS PktCts " & _
                              "FROM dbo.tblExtPacket INNER JOIN dbo.tblParcel ON dbo.tblExtPacket.ParNo = dbo.tblParcel.GrpParNo AND dbo.tblExtPacket.Department = dbo.tblParcel.Depart " & _
                              "WHERE (dbo.tblParcel.ReIssue = 0) AND (dbo.tblParcel.Depart = '" & cmbDept.Text & "') AND " & _
                                "(LEFT(dbo.tblExtPacket.ParNo, 6) = '" & Mid(txtNewParNo.Text, 1, 6) & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("PktPcs").Value) Then
                        dblIssPcs = rsComSql.Fields("PktPcs").Value
                        dblIssCts = rsComSql.Fields("PktCts").Value
                    End If
                End If
                rsComSql = Nothing
        End Select

        If dblTrfPcs - dblIssPcs < CDbl(txtSelPcs.Text) Then
            MsgBox("Invalid Transfer Pcs : " & dblTrfPcs - dblIssPcs, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If Math.Round(dblTrfCts - dblIssCts, 3) < CDbl(txtSelCts.Text) Then
            MsgBox("Invalid Transfer Cts : " & Math.Round(dblTrfCts - dblIssCts, 3), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        For intRow = 0 To flxSelected.Rows.Count - 1
            If flxSelected.Item(11, intRow).Value = "" Then
                MsgBox("Please Get the Packet Numbers", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            Select Case cmbDept.Text
                Case "Rounds"
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT PktNo FROM tblRndPacket WHERE ParNo = '" & txtNewParNo.Text & "' AND PktNo = '" & flxSelected.Item(11, intRow).Value & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        MsgBox("Already Saved - " & flxSelected.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql = Nothing

                Case "Baguettes"
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT PktNo FROM tblBAGPacket WHERE ParNo = '" & txtNewParNo.Text & "' AND PktNo = '" & flxSelected.Item(11, intRow).Value & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        MsgBox("Already Saved - " & flxSelected.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql = Nothing

                Case "Emerald"
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT PktNo FROM tblExtPacket WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtNewParNo.Text & "' AND PktNo = '" & flxSelected.Item(11, intRow).Value & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        MsgBox("Already Saved - " & flxSelected.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql = Nothing
            End Select
            
        Next

        dtpToday = GetToday()

        For intRow = 0 To flxSelected.Rows.Count - 1
            dblExtYld = Math.Round((CDbl(flxSelected.Item(4, intRow).Value) / CDbl(flxSelected.Item(3, intRow).Value)) * 100, 2)

            Select Case cmbDept.Text
                Case "Rounds"
                    AdoCN.Execute("INSERT INTO tblRndPacket(ParNo,PktNo,PktPcs,PktCts,PktOrgCts,PktOrdNo,PktRefNo,Pktside,AssortNo,PktFlow," & _
                                        "Grp,OrgParNo,PktIss,Sieve,PktColor,PktID,Clarity,PktCut,FinCts,PlanVal,EstYld,PktCategory,IncUnit,Model," & _
                                        "Girdling,Crown,Diameter,RevPoint,FinCut,ActDiameter,Tension,Mistake,PlanValAdj,Opt,doneBy,doneFrom,BrutParNo,BrutPktNo,PktSize,Flo,PktIDNew,StoneNo) " & _
                                  "VALUES('" & txtNewParNo.Text & "','" & flxSelected.Item(11, intRow).Value & "'," & CDbl(flxSelected.Item(2, intRow).Value) & "," & CDbl(flxSelected.Item(3, intRow).Value) & "," & CDbl(flxSelected.Item(13, intRow).Value) & ",'1'," & _
                                        "1,'N','" & txtAssortment.Text & "','AllRoundsBBlo','" & Trim(UCase(strRight(txtNewParNo.Text, 1))) & "','" & txtOrgParNo.Text & "'," & _
                                        "'" & Format(dtpToday, "MM/dd/yyyy") & "','" & flxSelected.Item(5, intRow).Value & "','" & flxSelected.Item(8, intRow).Value & "'," & CDbl(flxSelected.Item(7, intRow).Value) & "," & _
                                        "'" & flxSelected.Item(9, intRow).Value & "','" & flxSelected.Item(10, intRow).Value & "'," & CDbl(flxSelected.Item(4, intRow).Value) & "," & CDbl(flxSelected.Item(6, intRow).Value) & "," & dblExtYld & "," & _
                                        "'" & txtCategory.Text & "','" & flxSelected.Item(12, intRow).Value & "','-','','','0',0,'',0," & CDbl(flxSelected.Item(14, intRow).Value) & ",'',0,'','" & PBUser_EmpNo & "','" & PBCompName & "'," & _
                                        "'" & flxSelected.Item(15, intRow).Value & "','" & flxSelected.Item(1, intRow).Value & "','" & flxSelected.Item(16, intRow).Value & "','" & flxSelected.Item(17, intRow).Value & "'," & CDbl(flxSelected.Item(18, intRow).Value) & ",'" & flxSelected.Item(19, intRow).Value & "')")

                    AdoCN.Execute("UPDATE tblRPrPacket SET Trf = 1 WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & flxSelected.Item(1, intRow).Value & "' AND Department = 'RoughBruting'")

                Case "Baguettes"
                    AdoCN.Execute("INSERT INTO tblBAGPacket(ParNo,PktNo,PktPcs,PktCts,PktOrgCts,PktOrdNo,PktRefNo,Pktside,AssortNo,PktFlow," & _
                                        "Grp,AParNo,PktIss,PktSize,PktColor,PktID,Clarity,PktCut,FinCts,PlanVal,EstYld,Length,SizeRange,Width,IncUnit,PktIDNew,StoneNo) " & _
                                  "VALUES('" & txtNewParNo.Text & "','" & flxSelected.Item(11, intRow).Value & "'," & CDbl(flxSelected.Item(2, intRow).Value) & "," & CDbl(flxSelected.Item(3, intRow).Value) & "," & CDbl(flxSelected.Item(13, intRow).Value) & ",'1'," & _
                                        "'1','N','" & txtAssortment.Text & "','Rough','" & strRight(txtNewParNo.Text, 1) & "','" & txtOrgParNo.Text & "'," & _
                                        "'" & Format(dtpToday, "MM/dd/yyyy") & "','" & flxSelected.Item(5, intRow).Value & "','" & flxSelected.Item(8, intRow).Value & "'," & CDbl(flxSelected.Item(7, intRow).Value) & "," & _
                                        "'" & flxSelected.Item(9, intRow).Value & "','" & flxSelected.Item(10, intRow).Value & "'," & CDbl(flxSelected.Item(4, intRow).Value) & "," & CDbl(flxSelected.Item(6, intRow).Value) & "," & dblExtYld & ",0," & _
                                        "'" & flxSelected.Item(5, intRow).Value & "','" & flxSelected.Item(20, intRow).Value & "','" & flxSelected.Item(12, intRow).Value & "'," & CDbl(flxSelected.Item(18, intRow).Value) & ",'" & flxSelected.Item(19, intRow).Value & "')")

                    AdoCN.Execute("UPDATE tblRPrPacket SET Trf = 1 WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & flxSelected.Item(1, intRow).Value & "' AND Department = 'RoughBruting'")

                Case "Emerald"
                    AdoCN.Execute("INSERT INTO tblExtPacket(Department,ParNo,PktNo,PktPcs,PktCts,PktOrgCts,PktOrdNo,PktRefNo,Pktside,AssortNo,PktFlow," & _
                                        "Grp,OrgParNo,PktIss,Sieve,PktColor,PktID,Clarity,PktCut,FinCts,PlanVal,EstYld,PktCategory,IncUnit,Width,Tension,Height,Flo,PktIDNew,StoneNo) " & _
                                  "VALUES('" & cmbDept.Text & "','" & txtNewParNo.Text & "','" & flxSelected.Item(11, intRow).Value & "'," & CDbl(flxSelected.Item(2, intRow).Value) & "," & CDbl(flxSelected.Item(3, intRow).Value) & "," & CDbl(flxSelected.Item(13, intRow).Value) & ",'1'," & _
                                        "'1','N','" & txtAssortment.Text & "','Emerald','" & strRight(txtNewParNo.Text, 1) & "','" & txtOrgParNo.Text & "'," & _
                                        "'" & Format(dtpToday, "MM/dd/yyyy") & "','" & flxSelected.Item(5, intRow).Value & "','" & flxSelected.Item(8, intRow).Value & "'," & CDbl(flxSelected.Item(7, intRow).Value) & "," & _
                                        "'" & flxSelected.Item(9, intRow).Value & "','" & flxSelected.Item(10, intRow).Value & "'," & CDbl(flxSelected.Item(4, intRow).Value) & "," & CDbl(flxSelected.Item(6, intRow).Value) & "," & dblExtYld & "," & _
                                        "'" & txtCategory.Text & "','" & flxSelected.Item(12, intRow).Value & "','" & flxSelected.Item(20, intRow).Value & "','" & CDbl(flxSelected.Item(14, intRow).Value) & "','0','" & flxSelected.Item(17, intRow).Value & "'," & CDbl(flxSelected.Item(18, intRow).Value) & ",'" & flxSelected.Item(19, intRow).Value & "')")

                    AdoCN.Execute("UPDATE tblRPrPacket SET Trf = 1 WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & flxSelected.Item(1, intRow).Value & "' AND Department = 'RoughBruting'")
            End Select
            
        Next

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblParcel WHERE GrpParNo = '" & txtNewParNo.Text & "' AND Depart = '" & cmbDept.Text & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            Select Case cmbDept.Text
                Case "Rounds"
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktOrgCts), 3) AS PktCts FROM tblRndPacket WHERE (ParNo = '" & txtNewParNo.Text & "')", AdoCN, 1, 1)
                    If Not IsDBNull(rsComSql_2.Fields("PktPcs").Value) Then
                        If rsComSql_1.Fields("IssuedPcs").Value < rsComSql_2.Fields("PktPcs").Value Then
                            AdoCN.Execute("UPDATE tblParcel SET IssuedPcs = " & rsComSql_2.Fields("PktPcs").Value & ",RghPcs = " & rsComSql_2.Fields("PktPcs").Value & ", IssuedCts = " & rsComSql_2.Fields("PktCts").Value & ", RghCts = " & rsComSql_2.Fields("PktCts").Value & " " & _
                                          "WHERE GrpParNo = '" & txtNewParNo.Text & "' AND Depart = '" & cmbDept.Text & "'")
                        End If
                    End If
                    rsComSql_2 = Nothing

                Case "Baguettes"
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktOrgCts), 3) AS PktCts FROM tblBAGPacket WHERE (ParNo = '" & txtNewParNo.Text & "')", AdoCN, 1, 1)
                    If Not IsDBNull(rsComSql_2.Fields("PktPcs").Value) Then
                        If rsComSql_1.Fields("IssuedPcs").Value < rsComSql_2.Fields("PktPcs").Value Then
                            AdoCN.Execute("UPDATE tblParcel SET IssuedPcs = " & rsComSql_2.Fields("PktPcs").Value & ",RghPcs = " & rsComSql_2.Fields("PktPcs").Value & ", IssuedCts = " & rsComSql_2.Fields("PktCts").Value & ", RghCts = " & rsComSql_2.Fields("PktCts").Value & " " & _
                                          "WHERE GrpParNo = '" & txtNewParNo.Text & "' AND Depart = '" & cmbDept.Text & "'")
                        End If
                    End If
                    rsComSql_2 = Nothing

                Case "Emerald"
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktOrgCts), 3) AS PktCts FROM tblExtPacket WHERE (Department = '" & cmbDept.Text & "') AND (ParNo = '" & txtNewParNo.Text & "')", AdoCN, 1, 1)
                    If Not IsDBNull(rsComSql_2.Fields("PktPcs").Value) Then
                        If rsComSql_1.Fields("IssuedPcs").Value < rsComSql_2.Fields("PktPcs").Value Then
                            AdoCN.Execute("UPDATE tblParcel SET IssuedPcs = " & rsComSql_2.Fields("PktPcs").Value & ",RghPcs = " & rsComSql_2.Fields("PktPcs").Value & ", IssuedCts = " & rsComSql_2.Fields("PktCts").Value & ", RghCts = " & rsComSql_2.Fields("PktCts").Value & " " & _
                                          "WHERE GrpParNo = '" & txtNewParNo.Text & "' AND Depart = '" & cmbDept.Text & "'")
                        End If
                    End If
                    rsComSql_2 = Nothing
            End Select
            
        End If
        rsComSql_1 = Nothing

        MsgBox("Packets Transfered to Production", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        ClearFields()
    End Sub

    Private Sub txtNewParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNewParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If cmbDept.Text <> "" Then
                txtNewParNo.Text = UCase(Trim(txtNewParNo.Text))
                txtParNo.Text = txtNewParNo.Text

                rsComSql_6 = New ADODB.Recordset
                rsComSql_6.Open("SELECT OrigParcelNo, Assortment, Category FROM tblParcel WHERE GrpParNo = '" & txtNewParNo.Text & "' AND Depart = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                If rsComSql_6.RecordCount Then
                    txtOrgParNo.Text = rsComSql_6.Fields("OrigParcelNo").Value
                    txtAssortment.Text = rsComSql_6.Fields("Assortment").Value
                    txtCategory.Text = rsComSql_6.Fields("Category").Value

                    txtParNo.Focus()
                    Load_FinishedPackets()
                Else
                    MsgBox("Invalid Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql_6 = Nothing
            End If
        End If
    End Sub

    Private Sub flxSelected_DoubleClick(sender As Object, e As EventArgs) Handles flxSelected.DoubleClick
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse  = MsgBoxResult.Yes Then
            flxSelected.Rows.RemoveAt(flxSelected.CurrentRow.Index)
            txtSelPcs.Text = CalTotalPcs(flxSelected)
            txtSelCts.Text = CalTotalCts(flxSelected)

            For intRow = 0 To flxSelected.Rows.Count - 1
                flxSelected.Item(11, intRow).Value = ""
            Next
        End If
    End Sub

    Private Sub cmdSelect_Click(sender As Object, e As EventArgs) Handles cmdSelect.Click
        Dim intRow As Integer
        Dim dblSize As Double
        Dim strUnit As String

        If txtNewParNo.Text = "" Then Exit Sub
        If txtOrgParNo.Text = "" Then Exit Sub
        If txtAssortment.Text = "" Then Exit Sub
        If txtCategory.Text = "" Then Exit Sub

        If Len(txtParNo.Text) = 8 Then
            If Len(txtNewParNo.Text) = 8 Then
                If txtParNo.Text <> txtNewParNo.Text Then
                    MsgBox("Parcels not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
        End If

        flxSelected.Rows.Clear()
        For intRow = 0 To flxDetails.Rows.Count - 1
            dblSize = Math.Round(CDbl(flxDetails.Item(2, intRow).Value) / CDbl(flxDetails.Item(3, intRow).Value), 3)
            dblSize = Math.Round(dblSize, 3)

            strUnit = ""
            Select Case cmbDept.Text
                Case "Rounds"
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblRndIncentiveCat WHERE FromSize <= " & dblSize & " AND ToSize >= " & dblSize & " AND Category = '" & txtCategory.Text & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        strUnit = Trim(rsComSql.Fields("Unit").Value)
                    End If
                    rsComSql = Nothing

                Case "Baguettes"
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblBAGIncentiveCat WHERE FromSize <= " & dblSize & " AND ToSize >= " & dblSize & "", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        strUnit = Trim(rsComSql.Fields("Unit").Value)
                    End If
                    rsComSql = Nothing

                Case "Emerald"
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblExtIncentiveCat WHERE Department = '" & cmbDept.Text & "' AND FromSize <= " & dblSize & " AND ToSize >= " & dblSize & "", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        strUnit = Trim(rsComSql.Fields("Unit").Value)
                    End If
                    rsComSql = Nothing

            End Select

            If txtParNo.Text = txtNewParNo.Text Then
                flxSelected.Rows.Add(txtNewParNo.Text,
                                     flxDetails.Item(1, intRow).Value,
                                     flxDetails.Item(2, intRow).Value,
                                     flxDetails.Item(3, intRow).Value,
                                     flxDetails.Item(4, intRow).Value,
                                     flxDetails.Item(5, intRow).Value,
                                     flxDetails.Item(6, intRow).Value,
                                     flxDetails.Item(7, intRow).Value,
                                     flxDetails.Item(8, intRow).Value,
                                     flxDetails.Item(9, intRow).Value,
                                     flxDetails.Item(10, intRow).Value,
                                     flxDetails.Item(1, intRow).Value,
                                     strUnit,
                                     flxDetails.Item(11, intRow).Value,
                                     flxDetails.Item(12, intRow).Value,
                                     flxDetails.Item(0, intRow).Value,
                                     flxDetails.Item(13, intRow).Value,
                                     flxDetails.Item(14, intRow).Value,
                                     flxDetails.Item(15, intRow).Value,
                                     flxDetails.Item(16, intRow).Value,
                                     flxDetails.Item(17, intRow).Value)
            Else
                flxSelected.Rows.Add(txtNewParNo.Text,
                                     flxDetails.Item(1, intRow).Value,
                                     flxDetails.Item(2, intRow).Value,
                                     flxDetails.Item(3, intRow).Value,
                                     flxDetails.Item(4, intRow).Value,
                                     flxDetails.Item(5, intRow).Value,
                                     flxDetails.Item(6, intRow).Value,
                                     flxDetails.Item(7, intRow).Value,
                                     flxDetails.Item(8, intRow).Value,
                                     flxDetails.Item(9, intRow).Value,
                                     flxDetails.Item(10, intRow).Value,
                                     "",
                                     strUnit,
                                     flxDetails.Item(11, intRow).Value,
                                     flxDetails.Item(12, intRow).Value,
                                     flxDetails.Item(0, intRow).Value,
                                     flxDetails.Item(13, intRow).Value,
                                     flxDetails.Item(14, intRow).Value,
                                     flxDetails.Item(15, intRow).Value,
                                     flxDetails.Item(16, intRow).Value.
                                     flxDetails.Item(17, intRow).Value)
            End If

        Next

        flxDetails.Rows.Clear()
        txtTotPcs.Text = CalTotalPcs(flxDetails)
        txtTotCts.Text = CalTotalCts(flxDetails)
        txtSelPcs.Text = CalTotalPcs(flxSelected)
        txtSelCts.Text = CalTotalCts(flxSelected)

        txtPktNo.Text = ""
    End Sub

    Private Sub cmdGetPktNo_Click(sender As Object, e As EventArgs) Handles cmdGetPktNo.Click
        Dim intRow As Integer
        Dim strPktNo As String

        If CDbl(txtSelPcs.Text) <= 0 Then
            MsgBox("No Records Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblParcel WHERE GrpParNo = '" & txtNewParNo.Text & "' AND Depart = '" & cmbDept.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount <= 0 Then
            MsgBox("Invalid Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If Len(txtParNo.Text) = 8 Then
            If Len(txtNewParNo.Text) = 8 Then
                If txtParNo.Text <> txtNewParNo.Text Then
                    MsgBox("Parcels not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
        End If

        If Mid(txtParNo.Text, 3, 4) <> Mid(txtNewParNo.Text, 3, 4) Then
            MsgBox("Parcel No. Mismatch", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtParNo.Text = txtNewParNo.Text Then
            
        Else
            strPktNo = "0000"
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT MAX(CONVERT(numeric, PktNo)) AS MaxPkt FROM tblRndPacket WHERE ParNo = '" & txtNewParNo.Text & "'", AdoCN, 1, 1)
            If Not IsDBNull(rsComSql.Fields("MaxPkt").Value) Then
                strPktNo = rsComSql.Fields("MaxPkt").Value
            End If
            rsComSql = Nothing

            For intRow = 0 To flxSelected.Rows.Count - 1
                strPktNo = Format(CDbl(strPktNo) + 1, "0000")
                flxSelected.Item(11, intRow).Value = strPktNo
            Next
        End If
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub
End Class