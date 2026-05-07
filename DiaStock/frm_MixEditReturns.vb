
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixEditReturns

    Private Sub txtOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrder.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If Len(txtOrder.Text) = 6 Then
                txtPktNo.Focus()
            End If
        End If
    End Sub

    Private Sub txtPktNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If Len(txtPktNo.Text) >= 3 Then
                flxFinish.Rows.Clear()
                txtRetPcs.Text = "0"
                txtRetCts.Text = "0"
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblMixReturns WHERE ParNo = '" & txtOrder.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = 18 AND Status = 0 ORDER BY EmpNo", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        flxFinish.Rows.Add(rsComSql.Fields("RetPcsB").Value,
                                            Format(rsComSql.Fields("RetCts").Value, "#0.00"),
                                            rsComSql.Fields("EmpNo").Value)

                        txtRetPcs.Text = CDbl(txtRetPcs.Text) + rsComSql.Fields("RetPcsB").Value
                        txtRetCts.Text = Format(CDbl(txtRetCts.Text) + rsComSql.Fields("RetCts").Value, "#0.000")
                        rsComSql.MoveNext()
                    End While
                    flxFinish.Focus()
                Else
                    MsgBox("Invalid Entry", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    txtPktNo.Focus()
                End If
                rsComSql = Nothing
            End If
        End If
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxFinish.CellClick
        txtPcs.Text = flxFinish.Item(0, flxFinish.CurrentRow.Index).Value
        txtCts.Text = flxFinish.Item(1, flxFinish.CurrentRow.Index).Value
        txtEmp.Text = flxFinish.Item(2, flxFinish.CurrentRow.Index).Value
    End Sub

    Private Sub ClearFields()
        'txtOrder.Text = ""
        txtPktNo.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtRetPcs.Text = "0"
        txtRetCts.Text = "0"
        txtEmp.Text = ""
        flxFinish.Rows.Clear()
        txtOrder2.Text = ""
        txtFinish.Text = "0"
        txtVerify.Text = "0"

        txtOrder3.Text = ""
        txtPktNo3.Text = ""
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If txtOrder.Text <> "" And txtPktNo.Text <> "" And txtPcs.Text <> "" And txtCts.Text <> "" And txtEmp.Text <> "" Then
            If CInt(txtPcs.Text) > 0 And CDbl(txtCts.Text) > 0 Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblMixReturns WHERE ParNo = '" & txtOrder.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = 18 AND Status = 0 AND EmpNo = '" & txtEmp.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If rsComSql.Fields("RetPcsB").Value >= CInt(txtPcs.Text) And rsComSql.Fields("RetCts").Value >= CDbl(txtCts.Text) Then
                        AdoCN.Execute("UPDATE tblMixReturns SET RetPcsB = " & CInt(txtPcs.Text) & ",RetCts = " & CDbl(txtCts.Text) & " WHERE ParNo = '" & txtOrder.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = 18 AND Status = 0 AND EmpNo = '" & txtEmp.Text & "'")

                        MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

                        ClearFields()
                    Else
                        MsgBox("Invalid Pcs & Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    End If
                Else
                    MsgBox("Invalid Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                End If
                rsComSql = Nothing
            Else
                MsgBox("Invalid Pcs/Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            MsgBox("Order No. cannot be blank", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub txtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtPcs.Text <> "" Then
                txtCts.Text = Format((CDbl(txtRetCts.Text) / CDbl(txtRetPcs.Text)) * CDbl(txtPcs.Text), "#0.000")
            End If
        End If
    End Sub

    Private Sub txtCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtCts.Text)
    End Sub

    Private Sub Load_Orders()
        Dim intIndex As Integer

        flxParcel.Rows.Clear()
        intIndex = 1
        rsComSql = New ADODB.Recordset

        'mStrSQL = "SELECT TOP (100) PERCENT dbo.tblMixFinishOrders.OrderNo, SUM(dbo.tblMixFinishOrders.FinishedPcs) AS Pcs, SUM(dbo.tblMixFinishOrders.SysFinCts) AS Cts, SUM(dbo.tblMixFinishOrders.FinishedCts) " & _
        '                "AS ActCts, dbo.tblOrders.Subject, dbo.tblOrders.Niruref, dbo.tblOrders.NorderNo, dbo.tblOrders.OrderItem, dbo.tblOrders.Subject AS Subject2, dbo.tblOrders.NorderNo AS NorderNo2, " & _
        '                "dbo.tblOrders.OrderItem AS OrderItem2, dbo.tblOrders.COMMANDE AS COMMANDE2 " & _
        '          "FROM dbo.tblMixFinishOrders INNER JOIN dbo.tblOrders ON dbo.tblMixFinishOrders.OrderNo = dbo.tblOrders.OrderNo INNER JOIN " & _
        '                "dbo.tblOrdersDtls ON dbo.tblMixFinishOrders.NLineNo2 = dbo.tblOrdersDtls.NLineNo AND dbo.tblOrders.OrderNo = dbo.tblOrdersDtls.OrderNo " & _
        '          "WHERE (dbo.tblMixFinishOrders.Status = 'A') " & _
        '          "GROUP BY dbo.tblMixFinishOrders.OrderNo, dbo.tblOrders.Subject, dbo.tblOrders.Niruref, dbo.tblOrders.NorderNo, dbo.tblOrders.OrderItem, dbo.tblOrders.COMMANDE " & _
        '          "ORDER BY dbo.tblMixFinishOrders.OrderNo"

        mStrSQL = "SELECT TOP (100) PERCENT dbo.tblMixFinishOrders.OrderNo, SUM(dbo.tblMixFinishOrders.FinishedPcs) AS Pcs, SUM(dbo.tblMixFinishOrders.SysFinCts) AS Cts, SUM(dbo.tblMixFinishOrders.FinishedCts) " & _
                      "AS ActCts, dbo.tblOrders.Subject, dbo.tblOrders.Niruref, dbo.tblOrders.NorderNo, dbo.tblOrders.OrderItem, tblOrders_1.Subject AS Subject2, tblOrders_1.NorderNo AS NorderNo2, " & _
                      "tblOrders_1.OrderItem AS OrderItem2, tblOrders_1.COMMANDE AS COMMANDE2, tblOrders_1.KITNo " & _
                  "FROM dbo.tblMixFinishOrders INNER JOIN dbo.tblOrders ON dbo.tblMixFinishOrders.OrderNo = dbo.tblOrders.OrderNo INNER JOIN " & _
                      "dbo.tblOrdersDtls ON dbo.tblMixFinishOrders.NLineNo2 = dbo.tblOrdersDtls.NLineNo INNER JOIN " & _
                      "dbo.tblOrders AS tblOrders_1 ON dbo.tblOrdersDtls.OrderNo = tblOrders_1.OrderNo " & _
                  "WHERE (dbo.tblMixFinishOrders.Status = 'A') " & _
                  "GROUP BY dbo.tblMixFinishOrders.OrderNo, dbo.tblOrders.Subject, dbo.tblOrders.Niruref, dbo.tblOrders.NorderNo, dbo.tblOrders.OrderItem, tblOrders_1.Subject, tblOrders_1.NorderNo, tblOrders_1.OrderItem, " & _
                       "tblOrders_1.COMMANDE, tblOrders_1.KITNo " & _
                  "ORDER BY dbo.tblMixFinishOrders.OrderNo"

        'mStrSQL = "SELECT TOP (100) PERCENT dbo.tblMixFinishOrders.OrderNo, SUM(dbo.tblMixFinishOrders.FinishedPcs) AS Pcs,SUM(dbo.tblMixFinishOrders.SysFinCts) AS Cts, " & _
        '            "SUM(dbo.tblMixFinishOrders.FinishedCts) AS ActCts, dbo.tblOrders.Subject, dbo.tblOrders.Niruref, dbo.tblOrders.NorderNo, dbo.tblOrders.OrderItem " & _
        '          "FROM dbo.tblMixFinishOrders INNER JOIN dbo.tblOrders ON dbo.tblMixFinishOrders.OrderNo = dbo.tblOrders.OrderNo " & _
        '          "WHERE (dbo.tblMixFinishOrders.Status = 'A') " & _
        '          "GROUP BY dbo.tblMixFinishOrders.OrderNo, dbo.tblOrders.Subject, dbo.tblOrders.Niruref, dbo.tblOrders.NorderNo, dbo.tblOrders.OrderItem " & _
        '          "ORDER BY dbo.tblMixFinishOrders.OrderNo"
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxParcel.Rows.Add(intIndex,
                                   rsComSql.Fields("OrderNo").Value,
                                   rsComSql.Fields("Subject").Value,
                                   rsComSql.Fields("Pcs").Value,
                                   Format(rsComSql.Fields("Cts").Value, "#0.000"),
                                   Format(rsComSql.Fields("ActCts").Value, "#0.000"),
                                   Format(rsComSql.Fields("Cts").Value - rsComSql.Fields("ActCts").Value, "#0.000"), True,
                                   rsComSql.Fields("Niruref").Value,
                                   rsComSql.Fields("NorderNo").Value,
                                   rsComSql.Fields("OrderItem").Value,
                                   rsComSql.Fields("NorderNo2").Value,
                                   rsComSql.Fields("OrderItem2").Value,
                                   rsComSql.Fields("COMMANDE2").Value,
                                   rsComSql.Fields("Subject2").Value,
                                   rsComSql.Fields("KITNo").Value)

                intIndex = intIndex + 1
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
        Calculate()
    End Sub

    Private Sub Load_FinishOrders()
        Dim intIndex As Integer

        flxOrder.Rows.Clear()
        intIndex = 1
        rsComSql = New ADODB.Recordset
        mStrSQL = "SELECT TOP (100) PERCENT dbo.tblMixFinishOrders.OrderNo, dbo.tblMixFinishOrders.PacketNo, SUM(dbo.tblMixFinishOrders.FinishedPcs) AS Pcs, SUM(dbo.tblMixFinishOrders.SysFinCts) AS Cts, " & _
                      "SUM(dbo.tblMixFinishOrders.FinishedCts) AS ActCts, dbo.tblOrders.Subject, dbo.tblOrders.Niruref, dbo.tblOrders.NorderNo, dbo.tblOrders.OrderItem " & _
                  "FROM dbo.tblMixFinishOrders INNER JOIN dbo.tblOrders ON dbo.tblMixFinishOrders.OrderNo = dbo.tblOrders.OrderNo " & _
                  "WHERE (dbo.tblMixFinishOrders.Status = 'A') AND (dbo.tblMixFinishOrders.FinishedPcs > 0) " & _
                  "GROUP BY dbo.tblMixFinishOrders.OrderNo, dbo.tblMixFinishOrders.PacketNo, dbo.tblOrders.Subject, dbo.tblOrders.Niruref, dbo.tblOrders.NorderNo, dbo.tblOrders.OrderItem " & _
                  "ORDER BY dbo.tblMixFinishOrders.OrderNo, dbo.tblMixFinishOrders.PacketNo"
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxOrder.Rows.Add(rsComSql.Fields("OrderNo").Value,
                                  rsComSql.Fields("Subject").Value,
                                  rsComSql.Fields("PacketNo").Value,
                                  rsComSql.Fields("Pcs").Value,
                                  Format(rsComSql.Fields("Cts").Value, "#0.000"),
                                  False,
                                  rsComSql.Fields("Niruref").Value,
                                  rsComSql.Fields("NorderNo").Value,
                                  rsComSql.Fields("OrderItem").Value)

                intIndex = intIndex + 1
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub frm_MixEditReturns_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Orders()
        Load_FinishOrders()
        chkSelect.Checked = True
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxParcel.RowCount - 1
                flxParcel.Item(7, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxParcel.RowCount - 1
                flxParcel.Item(7, intRow).Value = False
            Next
        End If
    End Sub

    Private Sub cmdAE_Click(sender As Object, e As EventArgs) Handles cmdAE.Click
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            For intRow = 0 To flxParcel.RowCount - 1
                If flxParcel.Item(7, intRow).Value = True Or flxParcel.Item(7, intRow).Value = 1 Then
                    AdoCN.Execute("UPDATE tblMixFinishOrders SET Status = 'E' " & _
                                  "WHERE Status = 'A' AND OrderNo = '" & flxParcel.Item(1, intRow).Value & "'")
                End If
            Next
            Load_Orders()
        End If
    End Sub

    Private Sub Calculate()
        Dim intRow As Integer
        Dim vPcs As Double
        Dim vCts As Double
        Dim vACts As Double

        vPcs = 0
        vCts = 0
        vACts = 0
        For intRow = 0 To flxParcel.Rows.Count - 1
            If flxParcel.Item(7, intRow).Value = True Or flxParcel.Item(7, intRow).Value = "1" Then
                vPcs = vPcs + flxParcel.Item(3, intRow).Value
                vCts = Format(vCts + CDbl(flxParcel.Item(4, intRow).Value), "#0.#00")
                vACts = Format(vACts + CDbl(flxParcel.Item(5, intRow).Value), "#0.#00")
            End If
        Next
        txtTotPcs.Text = vPcs
        txtTotCts.Text = vCts
        txtTotActCts.Text = vACts
    End Sub

    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        If txtOrder2.Text <> "" Then
            PBResponse = MsgBox("Are you sure to clear this Order?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                AdoCN.Execute("UPDATE tblMixReturns SET Status = 0 WHERE ParNo = '" & txtOrder2.Text & "' AND Sec = 18 AND Status = 2")
                AdoCN.Execute("UPDATE tblMixPacketDetails SET Ok = 0 WHERE ParNo = '" & txtOrder2.Text & "' AND Ok = 2")
                AdoCN.Execute("DELETE FROM tblMixFinishOrders WHERE OrderNo = '" & txtOrder2.Text & "' AND Status = 'A'")

                MsgBox("Order No. " & txtOrder2.Text & " cleared", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

                ClearFields()
            End If
        End If
    End Sub

    Private Sub txtOrder2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrder2.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtOrder2.Text <> "" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(RetPcsT + RetPcsB) AS RetPcs FROM tblMixReturns WHERE ParNo = '" & txtOrder2.Text & "' AND Sec = 18 AND Status = 2", AdoCN, 1, 1)
                If Not IsDBNull(rsComSql.Fields("RetPcs").Value) Then
                    txtFinish.Text = rsComSql.Fields("RetPcs").Value
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(FinishedPcs) AS RetPcs FROM tblMixFinishOrders WHERE OrderNo = '" & txtOrder2.Text & "' AND Status = 'A'", AdoCN, 1, 1)
                If Not IsDBNull(rsComSql.Fields("RetPcs").Value) Then
                    txtVerify.Text = rsComSql.Fields("RetPcs").Value
                End If
                rsComSql = Nothing
            End If
        End If       
    End Sub

    Private Sub txtOrder3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrder3.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub txtPktNo3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktNo3.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub cmdClear3_Click(sender As Object, e As EventArgs) Handles cmdClear3.Click
        If txtOrder3.Text <> "" And txtPktNo3.Text <> "" Then
            PBResponse = MsgBox("Are you sure to clear this Packet?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblMixReturns WHERE ParNo = '" & Trim(txtOrder3.Text) & "' AND PktNo = '" & Trim(txtPktNo3.Text) & "' AND Sec = 18 AND Status = 2", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    AdoCN.Execute("UPDATE tblMixReturns SET Status = 0 WHERE ParNo = '" & Trim(txtOrder3.Text) & "' AND PktNo = '" & Trim(txtPktNo3.Text) & "' AND Sec = 18 AND Status = 2")
                    AdoCN.Execute("UPDATE tblMixPacketDetails SET Ok = 0 WHERE ParNo = '" & Trim(txtOrder3.Text) & "' AND PktNo = '" & Trim(txtPktNo3.Text) & "' AND Ok = 2")
                    AdoCN.Execute("DELETE FROM tblMixFinishOrders WHERE OrderNo = '" & Trim(txtOrder3.Text) & "' AND PacketNo = '" & Trim(txtPktNo3.Text) & "' AND  Status = 'A'")

                    MsgBox("Order No. " & txtOrder3.Text & "/" & txtPktNo3.Text & " cleared", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Else
                    MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                End If
                rsComSql = Nothing
                ClearFields()
            End If
        End If
    End Sub

    Private Sub flxParcel_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxParcel.CellContentClick
        Calculate()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxParcel)
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        If txtOrder.Text <> "" And txtPktNo.Text <> "" Then
            PBResponse = MsgBox("Are you sure to Delete?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblMixReturns WHERE ParNo = '" & Trim(txtOrder.Text) & "' AND PktNo = '" & Trim(txtPktNo.Text) & "' AND Sec = 18 AND Status = 0 ORDER BY ID", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        If rsComSql.Fields("RejPcs").Value > 0 Then
                            AdoCN.Execute("UPDATE tblMixReturns SET RetPcsT = 0, RetPcsB = 0, RetCts = 0 WHERE ID = '" & rsComSql.Fields("ID").Value & "'")
                        Else
                            AdoCN.Execute("DELETE FROM tblMixReturns WHERE ID = '" & rsComSql.Fields("ID").Value & "'")
                        End If
                        rsComSql.MoveNext()
                    End While
                End If
                rsComSql = Nothing


                MsgBox("Deleted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

                ClearFields()
            End If
        Else
            MsgBox("Order No./Packet No. cannot be blank", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        Load_Orders()
        Load_FinishOrders()
        chkSelect.Checked = True
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        ExportToExcel(flxOrder)
    End Sub

    Private Sub cmdClear2_Click(sender As Object, e As EventArgs) Handles cmdClear2.Click
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            For intRow = 0 To flxOrder.RowCount - 1
                If flxOrder.Item(5, intRow).Value = True Or flxOrder.Item(5, intRow).Value = 1 Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblMixReturns WHERE ParNo = '" & flxOrder.Item(0, intRow).Value & "' AND PktNo = '" & flxOrder.Item(2, intRow).Value & "' AND Sec = 18 AND Status = 2", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        AdoCN.Execute("UPDATE tblMixReturns SET Status = 0 WHERE ParNo = '" & flxOrder.Item(0, intRow).Value & "' AND PktNo = '" & flxOrder.Item(2, intRow).Value & "' AND Sec = 18 AND Status = 2")
                        AdoCN.Execute("UPDATE tblMixPacketDetails SET Ok = 0 WHERE ParNo = '" & flxOrder.Item(0, intRow).Value & "' AND PktNo = '" & flxOrder.Item(2, intRow).Value & "' AND Ok = 2")
                        AdoCN.Execute("DELETE FROM tblMixFinishOrders WHERE OrderNo = '" & flxOrder.Item(0, intRow).Value & "' AND PacketNo = '" & flxOrder.Item(2, intRow).Value & "' AND  Status = 'A'")
                    End If
                    rsComSql = Nothing
                End If
            Next
            Load_FinishOrders()
        End If
    End Sub
End Class