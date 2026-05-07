
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixEditReturns16

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
                txtPcs.Text = "0"
                txtId100.Text = "0"
                txtAms2.Text = "0"
                txtDfi.Text = "0"
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblMixReturns WHERE ParNo = '" & txtOrder.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = 16", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    txtPcs.Text = rsComSql.Fields("RetPcsB").Value + rsComSql.Fields("RetPcsT").Value
                    txtId100.Text = rsComSql.Fields("GiaPcs").Value
                    txtAms2.Text = rsComSql.Fields("AmsPcs").Value
                    txtDfi.Text = rsComSql.Fields("LabPcs").Value
                    txtId100.Focus()
                Else
                    MsgBox("Invalid Entry", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    ClearFields()
                    txtOrder.Focus()
                End If
                rsComSql = Nothing
            End If
        End If
    End Sub

    Private Sub ClearFields()
        txtOrder.Text = ""
        txtPktNo.Text = ""
        txtPcs.Text = ""
        txtId100.Text = ""
        txtAms2.Text = ""
        txtDfi.Text = ""
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If txtOrder.Text <> "" And txtPktNo.Text <> "" And txtPcs.Text <> "" And txtId100.Text <> "" And txtAms2.Text <> "" Then
            If CInt(txtPcs.Text) = CInt(txtId100.Text) + CInt(txtAms2.Text) + CInt(txtDfi.Text) Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblMixReturns WHERE ParNo = '" & txtOrder.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = 16", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If rsComSql.Fields("RetPcsB").Value + rsComSql.Fields("RetPcsT").Value = CInt(txtId100.Text) + CInt(txtAms2.Text) + CInt(txtDfi.Text) Then
                        AdoCN.Execute("UPDATE tblMixReturns SET GiaPcs = " & CInt(txtId100.Text) & ",AmsPcs = " & CInt(txtAms2.Text) & ",LabPcs = " & CInt(txtDfi.Text) & " WHERE ID = '" & rsComSql.Fields("ID").Value & "'")

                        MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

                        ClearFields()
                    Else
                        MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    End If
                Else
                    MsgBox("Invalid Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                End If
                rsComSql = Nothing
            Else
                MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            MsgBox("Details cannot be blank", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub txtId100_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtId100.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub txtAms2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAms2.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub frm_MixEditReturns16_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub txtDfi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDfi.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub
End Class