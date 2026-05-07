
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_PCUEditReturns

    Private Sub txtOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrder.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If Len(txtOrder.Text) = 5 Then
                txtPktNo.Focus()
            End If
        End If
    End Sub

    Private Sub txtPktNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If Len(txtPktNo.Text) = 3 Then
                flxDetails.Rows.Clear()
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblReturns WHERE ParNo = '" & txtOrder.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = 18 AND Status = 0 ORDER BY EmpNo", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        flxDetails.Rows.Add(rsComSql.Fields("RetPcsB").Value,
                                            Format(rsComSql.Fields("RetCts").Value, "#0.00"),
                                            rsComSql.Fields("EmpNo").Value)
                        rsComSql.MoveNext()
                    End While
                    flxDetails.Focus()
                Else
                    MsgBox("Invalid Entry", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    txtPktNo.Focus()
                End If
                rsComSql = Nothing
            End If
        End If
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        txtPcs.Text = flxDetails.Item(0, flxDetails.CurrentRow.Index).Value
        txtCts.Text = flxDetails.Item(1, flxDetails.CurrentRow.Index).Value
        txtEmp.Text = flxDetails.Item(2, flxDetails.CurrentRow.Index).Value
    End Sub

    Private Sub ClearFields()
        txtOrder.Text = ""
        txtPktNo.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtEmp.Text = ""
        flxDetails.Rows.Clear()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If txtOrder.Text <> "" And txtPktNo.Text <> "" And txtPcs.Text <> "" And txtCts.Text <> "" And txtEmp.Text <> "" Then
            If CInt(txtPcs.Text) > 0 And CDbl(txtCts.Text) > 0 Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblReturns WHERE ParNo = '" & txtOrder.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = 18 AND Status = 0 AND EmpNo = '" & txtEmp.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If rsComSql.Fields("RetPcsB").Value >= CInt(txtPcs.Text) And rsComSql.Fields("RetCts").Value >= CDbl(txtCts.Text) Then
                        AdoCN.Execute("UPDATE tblReturns SET RetPcsB = " & CInt(txtPcs.Text) & ",RetCts = " & CDbl(txtCts.Text) & " WHERE ParNo = '" & txtOrder.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = 18 AND Status = 0 AND EmpNo = '" & txtEmp.Text & "'")

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
    End Sub

    Private Sub txtCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtCts.Text)
    End Sub

    Private Sub Load_Orders()
        flxParcel.Rows.Clear()
        rsComSql = New ADODB.Recordset
        mStrSQL = "SELECT TOP (100) PERCENT dbo.tblPCUFinishOrders.OrderNo, SUM(dbo.tblPCUFinishOrders.FinishedPcs) AS Pcs,SUM(dbo.tblPCUFinishOrders.FinishedCts) AS Cts, dbo.tblNoneOrders.Subject " & _
                 "FROM dbo.tblPCUFinishOrders INNER JOIN dbo.tblNoneOrders ON dbo.tblPCUFinishOrders.OrderNo = dbo.tblNoneOrders.OrderNo " & _
                 "WHERE (dbo.tblPCUFinishOrders.Status = 'A') " & _
                 "GROUP BY dbo.tblPCUFinishOrders.OrderNo, dbo.tblNoneOrders.Subject " & _
                 "ORDER BY dbo.tblPCUFinishOrders.OrderNo"
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxParcel.Rows.Add(rsComSql.Fields("OrderNo").Value,
                                   rsComSql.Fields("Subject").Value,
                                   rsComSql.Fields("Pcs").Value,
                                   Format(rsComSql.Fields("Cts").Value, "#0.000"), 0)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub frm_PCUEditReturns_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Orders()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxParcel.RowCount - 1
                flxParcel.Item(4, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxParcel.RowCount - 1
                flxParcel.Item(4, intRow).Value = False
            Next
        End If
    End Sub

    Private Sub cmdAE_Click(sender As Object, e As EventArgs) Handles cmdAE.Click
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            For intRow = 0 To flxParcel.RowCount - 1
                If flxParcel.Item(4, intRow).Value = True Or flxParcel.Item(4, intRow).Value = 1 Then
                    AdoCN.Execute("UPDATE tblPCUFinishOrders SET Status = 'E' " & _
                                  "WHERE Status = 'A' AND OrderNo = '" & flxParcel.Item(0, intRow).Value & "'")
                End If
            Next
            Load_Orders()
        End If
    End Sub
End Class