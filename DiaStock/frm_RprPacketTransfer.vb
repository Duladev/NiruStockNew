
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RprPacketTransfer
    Dim ParcelNo As String
    Dim PacketNo As String

    Private Sub frm_RprPacketTransfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        cmbDept.Items.Clear()
        cmbNewDept.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT DISTINCT Department FROM dbo.tblRPrFlow ORDER BY Department", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbDept.Items.Add(rsComSql.Fields("Department").Value)
                cmbNewDept.Items.Add(rsComSql.Fields("Department").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        Load_Model()
    End Sub

    Private Sub Load_Model()
        Dim rsGrdType As New ADODB.Recordset

        cmbModel.Items.Clear()
        rsGrdType = New ADODB.Recordset
        rsGrdType.Open("SELECT * FROM tblRghTypes WHERE Sec = 5 ORDER BY Type", AdoCN, 1, 1)
        If rsGrdType.RecordCount Then
            rsGrdType.MoveFirst()
            While Not rsGrdType.EOF
                cmbModel.Items.Add(rsGrdType.Fields("Type").Value)
                rsGrdType.MoveNext()
            End While
        End If
        rsGrdType = Nothing
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        Dim intTotPcs As Integer
        Dim dblTotCts As Double

        flxDetails.Rows.Clear()
        If txtParNo.Text <> "" And cmbDept.Text <> "" Then
            If Asc(e.KeyChar) = 13 Then
                txtParNo.Text = UCase(txtParNo.Text)
                intTotPcs = 0
                dblTotCts = 0
                rsComSql = New ADODB.Recordset
                If cmbModel.Text = "" Then
                    If cmbDept.Text = "Sawing" Then
                        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblLZPacket.PktNo, SUM(dbo.tblLZReturns.ActPcs) AS RetPcs, ROUND(SUM(dbo.tblLZReturns.RetCts), 3) AS RetCts " & _
                                      "FROM dbo.tblLZPacket INNER JOIN dbo.tblLZReturns ON dbo.tblLZPacket.ParNo = dbo.tblLZReturns.ParNo AND dbo.tblLZPacket.PktNo = dbo.tblLZReturns.PktNo " & _
                                      "WHERE (dbo.tblLZReturns.Sec = 14) AND (dbo.tblLZPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblLZPacket.Trf = 0) " & _
                                      "GROUP BY dbo.tblLZPacket.PktNo " & _
                                      "ORDER BY dbo.tblLZPacket.PktNo", AdoCN, 1, 1)

                    ElseIf cmbDept.Text = "RoughSawing" Or cmbDept.Text = "RoughBoil" Then
                        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrPacket.PktNo, SUM(dbo.tblRPrReturns.ActPcs) AS RetPcs, ROUND(SUM(dbo.tblRPrReturns.RetCts), 3) AS RetCts " & _
                                      "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrPacket.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrReturns.ParNo AND " & _
                                            "dbo.tblRPrPacket.PktNo = dbo.tblRPrReturns.PktNo " & _
                                      "WHERE (dbo.tblRPrReturns.Sec = 20) AND (dbo.tblRPrPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrPacket.Trf = 0) " & _
                                      "GROUP BY dbo.tblRPrPacket.PktNo " & _
                                      "ORDER BY dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)

                    ElseIf cmbDept.Text = "RoughSawing2" Or cmbDept.Text = "RoughSawing3" Or cmbDept.Text = "RoughSawing4" Or cmbDept.Text = "RoughSawing5" Or cmbDept.Text = "RoughSawing6" Or cmbDept.Text = "RoughSawingS" Then
                        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrPacket.PktNo, SUM(dbo.tblRPrReturns.ActPcs + dbo.tblRPrReturns.RetPcsT + dbo.tblRPrReturns.RetPcsB) AS RetPcs, ROUND(SUM(dbo.tblRPrReturns.RetCts), 3) AS RetCts " & _
                                      "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrPacket.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrReturns.ParNo AND " & _
                                            "dbo.tblRPrPacket.PktNo = dbo.tblRPrReturns.PktNo " & _
                                      "WHERE (dbo.tblRPrReturns.Sec = 20) AND (dbo.tblRPrPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrPacket.Trf = 0) " & _
                                      "GROUP BY dbo.tblRPrPacket.PktNo " & _
                                      "ORDER BY dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)
                    Else
                        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrPacket.PktNo, SUM(dbo.tblRPrReturns.RetPcsT + dbo.tblRPrReturns.RetPcsB) AS RetPcs, ROUND(SUM(dbo.tblRPrReturns.RetCts), 3) AS RetCts " & _
                                      "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrPacket.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrReturns.ParNo AND " & _
                                            "dbo.tblRPrPacket.PktNo = dbo.tblRPrReturns.PktNo " & _
                                      "WHERE (dbo.tblRPrReturns.Sec = 20) AND (dbo.tblRPrPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrPacket.Trf = 0) AND " & _
                                        "(BagPcs + PrPcs + RndPcs + OthPcs + SmallPcs + EmPcs + PcuPcs = 0) " & _
                                      "GROUP BY dbo.tblRPrPacket.PktNo " & _
                                      "ORDER BY dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)
                    End If
                Else
                    If cmbDept.Text = "Sawing" Then
                        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblLZPacket.PktNo, SUM(dbo.tblLZReturns.ActPcs) AS RetPcs, ROUND(SUM(dbo.tblLZReturns.RetCts), 3) AS RetCts " & _
                                      "FROM dbo.tblLZPacket INNER JOIN dbo.tblLZReturns ON dbo.tblLZPacket.ParNo = dbo.tblLZReturns.ParNo AND dbo.tblLZPacket.PktNo = dbo.tblLZReturns.PktNo " & _
                                      "WHERE (dbo.tblLZReturns.Sec = 14) AND (dbo.tblLZPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblLZPacket.Trf = 0) " & _
                                      "GROUP BY dbo.tblLZPacket.PktNo " & _
                                      "ORDER BY dbo.tblLZPacket.PktNo", AdoCN, 1, 1)

                    ElseIf cmbDept.Text = "RoughSawing" Or cmbDept.Text = "RoughBoil" Then
                        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrPacket.PktNo, SUM(dbo.tblRPrReturns.ActPcs) AS RetPcs, ROUND(SUM(dbo.tblRPrReturns.RetCts), 3) AS RetCts " & _
                                      "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrPacket.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrReturns.ParNo AND " & _
                                            "dbo.tblRPrPacket.PktNo = dbo.tblRPrReturns.PktNo " & _
                                      "WHERE (dbo.tblRPrReturns.Sec = 20) AND (dbo.tblRPrPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrPacket.Trf = 0) AND (dbo.tblRPrPacket.Model = '" & cmbModel.Text & "') " & _
                                      "GROUP BY dbo.tblRPrPacket.PktNo " & _
                                      "ORDER BY dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)

                    ElseIf cmbDept.Text = "RoughSawing2" Or cmbDept.Text = "RoughSawing3" Or cmbDept.Text = "RoughSawing4" Or cmbDept.Text = "RoughSawing5" Or cmbDept.Text = "RoughSawing6" Or cmbDept.Text = "RoughSawingS" Then
                        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrPacket.PktNo, SUM(dbo.tblRPrReturns.ActPcs + dbo.tblRPrReturns.RetPcsT + dbo.tblRPrReturns.RetPcsB) AS RetPcs, ROUND(SUM(dbo.tblRPrReturns.RetCts), 3) AS RetCts " & _
                                      "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrPacket.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrReturns.ParNo AND " & _
                                            "dbo.tblRPrPacket.PktNo = dbo.tblRPrReturns.PktNo " & _
                                      "WHERE (dbo.tblRPrReturns.Sec = 20) AND (dbo.tblRPrPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrPacket.Trf = 0) AND (dbo.tblRPrPacket.Model = '" & cmbModel.Text & "') " & _
                                      "GROUP BY dbo.tblRPrPacket.PktNo " & _
                                      "ORDER BY dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)
                    Else
                        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrPacket.PktNo, SUM(dbo.tblRPrReturns.RetPcsT + dbo.tblRPrReturns.RetPcsB) AS RetPcs, ROUND(SUM(dbo.tblRPrReturns.RetCts), 3) AS RetCts " & _
                                      "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrPacket.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrReturns.ParNo AND " & _
                                            "dbo.tblRPrPacket.PktNo = dbo.tblRPrReturns.PktNo " & _
                                      "WHERE (dbo.tblRPrReturns.Sec = 20) AND (dbo.tblRPrPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrPacket.Trf = 0) AND " & _
                                        "(BagPcs + PrPcs + RndPcs + OthPcs + SmallPcs + EmPcs + PcuPcs = 0) AND (dbo.tblRPrPacket.Model = '" & cmbModel.Text & "') " & _
                                      "GROUP BY dbo.tblRPrPacket.PktNo " & _
                                      "ORDER BY dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)
                    End If
                End If
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("RetPcs").Value) Then
                        rsComSql.MoveFirst()
                        While Not rsComSql.EOF
                            If rsComSql.Fields("RetPcs").Value > 0 Then
                                flxDetails.Rows.Add(rsComSql.Fields("PktNo").Value,
                                                    rsComSql.Fields("RetPcs").Value,
                                                    rsComSql.Fields("RetCts").Value,
                                                    txtParNo.Text)

                                intTotPcs = intTotPcs + rsComSql.Fields("RetPcs").Value
                                dblTotCts = dblTotCts + rsComSql.Fields("RetCts").Value
                            End If

                            rsComSql.MoveNext()
                        End While
                    End If
                End If
                rsComSql = Nothing

                txtTotPcs.Text = intTotPcs
                txtTotCts.Text = dblTotCts
                txtCount.Text = flxDetails.RowCount

            End If
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

    Private Sub cmbNewDept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNewDept.SelectedIndexChanged
        Dim intTrfPcs As Integer
        Dim dblTrfCts As Double

        intTrfPcs = 0
        dblTrfCts = 0
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT SUM(NewACTPcs) AS NewACTPcs, ROUND(SUM(NewACTCts), 3) AS NewACTCts " & _
                      "FROM dbo.tblDep_Trf " & _
                      "WHERE (Department = '" & cmbNewDept.Text & "') AND (DCLParcelNo = '" & Mid(txtParNo.Text, 1, 6) & "')", AdoCN, 1, 1)
        If Not IsDBNull(rsComSql.Fields("NewACTPcs").Value) Then
            intTrfPcs = rsComSql.Fields("NewACTPcs").Value
            dblTrfCts = rsComSql.Fields("NewACTCts").Value
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktCts), 3) AS PktCts FROM tblRPrPacket WHERE ParNo LIKE '" & Mid(txtParNo.Text, 1, 6) & "' + '%' AND Department = '" & cmbNewDept.Text & "'", AdoCN, 1, 1)
        If Not IsDBNull(rsComSql.Fields("PktPcs").Value) Then
            intTrfPcs = intTrfPcs - rsComSql.Fields("PktPcs").Value
            dblTrfCts = dblTrfCts - rsComSql.Fields("PktCts").Value
        End If
        rsComSql = Nothing

        txtBalPcs.Text = intTrfPcs
        txtBalCts.Text = Math.Round(dblTrfCts, 3)
    End Sub

    Private Sub cmdSelect_Click(sender As Object, e As EventArgs) Handles cmdSelect.Click
        Dim intIndex As Integer

        If cmbNewDept.Text <> "" Then
            If txtParNo.Text = "" Then Exit Sub

            flxSelected.Rows.Clear()
            'For intIndex = 0 To flxDetails.Rows.Count - 1
            '    rsComSql = New ADODB.Recordset
            '    rsComSql.Open("SELECT PktNo FROM tblRPrPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & flxDetails.Item(0, intIndex).Value & "' AND Department = '" & cmbNewDept.Text & "'", AdoCN, 1, 1)
            '    If rsComSql.RecordCount Then
            '        MsgBox("Already Entered - " & flxDetails.Item(0, intIndex).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '        Exit Sub
            '    End If
            '    rsComSql = Nothing
            'Next

            For intIndex = 0 To flxDetails.Rows.Count - 1
                flxSelected.Rows.Add(flxDetails.Item(0, intIndex).Value,
                                         flxDetails.Item(1, intIndex).Value,
                                         flxDetails.Item(2, intIndex).Value,
                                         flxDetails.Item(0, intIndex).Value)

                txtPktNo.Text = ""
            Next

            flxDetails.Rows.Clear()
            txtTotPcs.Text = CalTotalPcs(flxDetails)
            txtTotCts.Text = CalTotalCts(flxDetails)
            txtSelPcs.Text = CalTotalPcs(flxSelected)
            txtSelCts.Text = CalTotalCts(flxSelected)
        End If
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        Dim intRow As Integer

        If cmbNewDept.Text <> "" Then

            For intRow = 0 To flxSelected.Rows.Count - 1
                If flxDetails.Item(0, flxDetails.CurrentRow.Index).Value = flxSelected.Item(0, intRow).Value Then
                    MsgBox("Already Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Next

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblRPrPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & flxDetails.Item(0, flxDetails.CurrentRow.Index).Value & "' AND Department = '" & cmbNewDept.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                flxSelected.Rows.Add(flxDetails.Item(0, flxDetails.CurrentRow.Index).Value,
                                     flxDetails.Item(1, flxDetails.CurrentRow.Index).Value,
                                     flxDetails.Item(2, flxDetails.CurrentRow.Index).Value,
                                     flxDetails.Item(0, flxDetails.CurrentRow.Index).Value)
            Else
                MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)
            txtTotPcs.Text = CalTotalPcs(flxDetails)
            txtTotCts.Text = CalTotalCts(flxDetails)
            txtSelPcs.Text = CalTotalPcs(flxSelected)
            txtSelCts.Text = CalTotalCts(flxSelected)

            txtPktNo.Text = ""
        End If
    End Sub

    Private Sub flxSelected_DoubleClick(sender As Object, e As EventArgs) Handles flxSelected.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse  = MsgBoxResult.Yes Then
            flxSelected.Rows.RemoveAt(flxSelected.CurrentRow.Index)
            txtSelPcs.Text = CalTotalPcs(flxSelected)
            txtSelCts.Text = CalTotalCts(flxSelected)
        End If
    End Sub

    Private Sub SavePacket()
        Dim intRow As Integer
        Dim strFlow As String
        Dim dblTotPcs As Double
        Dim dblWindowPcs As Double
        Dim dblImpValue As Double
        Dim dblEstValue As Double
        Dim intApproval As Integer
        Dim dblPerc As Double

        If cmbDept.Text = cmbNewDept.Text Then
            MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If CInt(txtSelPcs.Text) > CInt(txtBalPcs.Text) Then
            MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        'If Mid(cmbNewDept.Text, 1, 11) = "RoughSawing" Then
        '    If txtParNo.Text = "DS1391A" Or txtParNo.Text = "JS1293A" Then
        '        MsgBox("Parcel Blocked", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '        Exit Sub
        '    End If
        'End If

        dblTotPcs = 0
        dblImpValue = 0
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT OrigParcelNo,IssuedPcs,IssuedCts,Approval FROM tblParcel WHERE GrpParNo = '" & txtParNo.Text & "' AND Depart = 'Rough Planning'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            dblTotPcs = rsComSql_1.Fields("IssuedPcs").Value
            intApproval = rsComSql_1.Fields("Approval").Value

            rsComSql_2 = New ADODB.Recordset
            rsComSql_2.Open("SELECT ItemCost, HardCost FROM tblImport WHERE SupParcelNo = '" & rsComSql_1.Fields("OrigParcelNo").Value & "'", AdoCN, 1, 1)
            If rsComSql_2.RecordCount Then
                If rsComSql_2.Fields("HardCost").Value > 0 Then
                    dblImpValue = Math.Round(rsComSql_1.Fields("IssuedCts").Value * rsComSql_2.Fields("HardCost").Value, 2)
                Else
                    dblImpValue = Math.Round(rsComSql_1.Fields("IssuedCts").Value * rsComSql_2.Fields("ItemCost").Value, 2)
                End If
            End If
            rsComSql_2 = Nothing
        End If
        rsComSql_1 = Nothing

        dblWindowPcs = 0
        dblEstValue = 0
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktCts * PktPrice), 2) AS PktValue FROM tblRghPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktType = 6", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            If Not IsDBNull(rsComSql_1.Fields("PktPcs").Value) Then
                dblWindowPcs = rsComSql_1.Fields("PktPcs").Value
                dblEstValue = rsComSql_1.Fields("PktValue").Value
            End If
        End If
        rsComSql_1 = Nothing

        If dblTotPcs > dblWindowPcs And intApproval = 0 Then
            MsgBox(dblTotPcs - dblWindowPcs & " pcs pending", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearText()
            Exit Sub
        End If

        If dblImpValue > 0 Then
            dblPerc = 0
            dblPerc = ((dblEstValue - dblImpValue) / dblImpValue) * 100

            If dblPerc < -10 And intApproval = 0 Then
                If dblImpValue > dblEstValue And intApproval = 0 Then
                    MsgBox(dblImpValue - dblEstValue & " value lost. Get the approval to proceed", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                    ClearText()
                    Exit Sub
                End If
            End If
        End If

        strFlow = ""
        rsComSql_2 = New ADODB.Recordset
        rsComSql_2.Open("SELECT Flow FROM tblRPrFlow WHERE Department = '" & cmbNewDept.Text & "'", AdoCN, 1, 1)
        If rsComSql_2.RecordCount Then
            strFlow = rsComSql_2.Fields("Flow").Value
        End If
        rsComSql_2 = Nothing

        dtpToday = GetToday()
        For intRow = 0 To flxSelected.Rows.Count - 1
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblRPrPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & flxSelected.Item(0, intRow).Value & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT PktNo FROM tblRPrPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & flxSelected.Item(0, intRow).Value & "' AND Department = '" & cmbNewDept.Text & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount = 0 Then
                    AdoCN.Execute("INSERT INTO tblRPrPacket(Department,ParNo,PktNo,PktPcs,PktCts,Assortment,PktColor,PktIss,PktFlow,PktClarity,PktCut,FinCts,EstYld,Flo,Tension,MainPkt,RefPktNo,Model,DoneBy) " & _
                                  "VALUES('" & cmbNewDept.Text & "','" & txtParNo.Text & "','" & flxSelected.Item(0, intRow).Value & "'," & CInt(flxSelected.Item(1, intRow).Value) & "," & CDbl(flxSelected.Item(2, intRow).Value) & "," & _
                                    "'" & rsComSql.Fields("Assortment").Value & "','" & rsComSql.Fields("PktColor").Value & "','" & Format(dtpToday, "MM/dd/yyyy") & "','" & strFlow & "','" & rsComSql.Fields("PktClarity").Value & "'," & _
                                    "'" & rsComSql.Fields("PktCut").Value & "'," & rsComSql.Fields("FinCts").Value & "," & rsComSql.Fields("EstYld").Value & ",'" & rsComSql.Fields("Flo").Value & "'," & _
                                    "" & rsComSql.Fields("Tension").Value & ",'" & rsComSql.Fields("MainPkt").Value & "','" & flxSelected.Item(3, intRow).Value & "','" & rsComSql.Fields("Model").Value & "','" & PBUser_EmpNo & "')")

                    AdoCN.Execute("UPDATE tblRPrPacket SET ID = ID2 WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & flxSelected.Item(0, intRow).Value & "' AND Department = '" & cmbNewDept.Text & "'")
                    AdoCN.Execute("UPDATE tblRPrPacket SET Trf = 1 WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & flxSelected.Item(0, intRow).Value & "' AND Department = '" & cmbDept.Text & "'")
                End If
                rsComSql_1 = Nothing
            End If
            rsComSql = Nothing
        Next

        MsgBox("Packets Transfered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        ClearText()
    End Sub

    Private Sub ClearText()
        cmbDept.Text = ""
        cmbNewDept.Text = ""
        txtParNo.Text = ""
        flxDetails.Rows.Clear()
        flxSelected.Rows.Clear()
        txtTotPcs.Text = ""
        txtTotCts.Text = ""
        txtSelPcs.Text = ""
        txtSelCts.Text = ""
        txtBalPcs.Text = ""
        txtBalCts.Text = ""
        cmbModel.Text = ""
        txtCount.Text = ""
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearText()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        SavePacket()
    End Sub

    Private Sub ShowPacket()
        Dim intRow As Integer

        If Len(txtPktNo.Text) = 4 Then
            If cmbDept.Text = "" Then Exit Sub
            If txtParNo.Text = "" Then Exit Sub

            For intRow = 0 To flxDetails.Rows.Count - 1
                If txtParNo.Text = flxDetails.Item(3, intRow).Value Then

                Else
                    MsgBox("Invalid Parcel", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Next

            For intRow = 0 To flxDetails.Rows.Count - 1
                If txtPktNo.Text = flxDetails.Item(0, intRow).Value Then
                    MsgBox("Already Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Next

            rsComSql = New ADODB.Recordset
            If cmbDept.Text = "Sawing" Then
                rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblLZPacket.PktNo, SUM(dbo.tblLZReturns.ActPcs) AS RetPcs, ROUND(SUM(dbo.tblLZReturns.RetCts), 3) AS RetCts " & _
                              "FROM dbo.tblLZPacket INNER JOIN dbo.tblLZReturns ON dbo.tblLZPacket.ParNo = dbo.tblLZReturns.ParNo AND dbo.tblLZPacket.PktNo = dbo.tblLZReturns.PktNo " & _
                              "WHERE (dbo.tblLZReturns.Sec = 14) AND (dbo.tblLZPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblLZPacket.Trf = 0) AND (dbo.tblLZPacket.PktNo = '" & txtPktNo.Text & "') " & _
                              "GROUP BY dbo.tblLZPacket.PktNo " & _
                              "ORDER BY dbo.tblLZPacket.PktNo", AdoCN, 1, 1)

            ElseIf cmbDept.Text = "RoughSawing" Then
                rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrPacket.PktNo, SUM(dbo.tblRPrReturns.ActPcs) AS RetPcs, ROUND(SUM(dbo.tblRPrReturns.RetCts), 3) AS RetCts " & _
                              "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrPacket.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrReturns.ParNo AND " & _
                                    "dbo.tblRPrPacket.PktNo = dbo.tblRPrReturns.PktNo " & _
                              "WHERE (dbo.tblRPrReturns.Sec = 20) AND (dbo.tblRPrPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrPacket.Trf = 0) AND (dbo.tblRPrPacket.PktNo = '" & txtPktNo.Text & "') " & _
                              "GROUP BY dbo.tblRPrPacket.PktNo " & _
                              "ORDER BY dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)

            ElseIf cmbDept.Text = "RoughSawing" Or cmbDept.Text = "RoughSawing2" Or cmbDept.Text = "RoughSawing3" Or cmbDept.Text = "RoughSawing4" Or cmbDept.Text = "RoughSawing5" Or cmbDept.Text = "RoughSawing6" Or cmbDept.Text = "RoughSawingS" Then
                rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrPacket.PktNo, SUM(dbo.tblRPrReturns.RetPcsT + dbo.tblRPrReturns.RetPcsB + dbo.tblRPrReturns.ActPcs) AS RetPcs, ROUND(SUM(dbo.tblRPrReturns.RetCts), 3) AS RetCts " & _
                              "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrPacket.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrReturns.ParNo AND " & _
                                    "dbo.tblRPrPacket.PktNo = dbo.tblRPrReturns.PktNo " & _
                              "WHERE (dbo.tblRPrReturns.Sec = 20) AND (dbo.tblRPrPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrPacket.Trf = 0) AND (dbo.tblRPrPacket.PktNo = '" & txtPktNo.Text & "') " & _
                              "GROUP BY dbo.tblRPrPacket.PktNo " & _
                              "ORDER BY dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)

            Else
                rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrPacket.PktNo, SUM(dbo.tblRPrReturns.RetPcsT + dbo.tblRPrReturns.RetPcsB) AS RetPcs, ROUND(SUM(dbo.tblRPrReturns.RetCts), 3) AS RetCts " & _
                              "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrPacket.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrReturns.ParNo AND " & _
                                    "dbo.tblRPrPacket.PktNo = dbo.tblRPrReturns.PktNo " & _
                              "WHERE (dbo.tblRPrReturns.Sec = 20) AND (dbo.tblRPrPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrPacket.Trf = 0) AND (dbo.tblRPrPacket.PktNo = '" & txtPktNo.Text & "') AND " & _
                                "(BagPcs + PrPcs + RndPcs + OthPcs + SmallPcs + EmPcs + PcuPcs = 0) " & _
                              "GROUP BY dbo.tblRPrPacket.PktNo " & _
                              "ORDER BY dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)
            End If
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("RetPcs").Value) Then
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        If rsComSql.Fields("RetPcs").Value > 0 Then
                            flxDetails.Rows.Add(rsComSql.Fields("PktNo").Value,
                                            rsComSql.Fields("RetPcs").Value,
                                            rsComSql.Fields("RetCts").Value,
                                            txtParNo.Text)
                        End If
                        
                        rsComSql.MoveNext()
                    End While
                End If
            End If
            rsComSql = Nothing
            txtTotPcs.Text = CalTotalPcs(flxDetails)
            txtTotCts.Text = CalTotalCts(flxDetails)
            txtCount.Text = flxDetails.RowCount
        End If
    End Sub

    Private Sub txtPktNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            ShowPacket()
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

End Class