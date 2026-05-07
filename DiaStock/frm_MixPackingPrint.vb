
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixPackingPrint

    Private Sub frm_MixPackingPrint_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub txtPackNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPackNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtPackNo.Text <> "" Then
                Load_PackingList()
            End If
        End If
    End Sub

    Private Sub Load_PackingList()
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblMixPackingListPrint WHERE PackNo = " & CDbl(txtPackNo.Text) & " ORDER BY PktSerialNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("PktSerialNo").Value,
                                    rsComSql.Fields("Client").Value,
                                    rsComSql.Fields("Subject").Value,
                                    rsComSql.Fields("StnRef").Value,
                                    rsComSql.Fields("PackPcs").Value,
                                    rsComSql.Fields("PackCts").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        Get_SerialNo()
        txtClient.Focus()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Get_SerialNo()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(PktSerialNo) AS MaxNo FROM tblMixPackingListPrint", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If IsDBNull(rsComSql.Fields("MaxNo").Value) Then
                txtIndex.Text = "1"
            Else
                txtIndex.Text = rsComSql.Fields("MaxNo").Value + 1
            End If
        Else
            txtIndex.Text = "1"
        End If
        rsComSql = Nothing
    End Sub

    Private Sub txtClient_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtClient.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtSubject.Focus()
        End If
    End Sub

    Private Sub txtSubject_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSubject.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtRef.Focus()
        End If
    End Sub

    Private Sub txtRef_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRef.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtPcs.Focus()
        End If
    End Sub

    Private Sub txtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtCts.Focus()
        End If
    End Sub

    Private Sub txtCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtCts.Text)
        If Asc(e.KeyChar) = 13 Then
            cmdSave.Focus()
        End If
    End Sub

    Private Sub Save()
        If txtPackNo.Text = "" Then MsgBox("Invalid Packing List No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtIndex.Text = "" Then MsgBox("Invalid Index No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtClient.Text = "" Then MsgBox("Invalid Client", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtSubject.Text = "" Then MsgBox("Invalid Subject", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtRef.Text = "" Then MsgBox("Invalid Stone Ref", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtPcs.Text = "" Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtPcs.Text) <= 0 Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtCts.Text = "" Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtCts.Text) <= 0 Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblMixPackingListPrint WHERE PackNo = " & CDbl(txtPackNo.Text) & " AND PktSerialNo = " & CDbl(txtIndex.Text) & "", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            AdoCN.Execute("INSERT INTO tblMixPackingListPrint(PackNo,PackDate,PktSerialNo,Client,Subject,StnRef,PackPcs,PackCts) " & _
                          "VALUES(" & CDbl(txtPackNo.Text) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "'," & CDbl(txtIndex.Text) & ",'" & txtClient.Text & "'," & _
                            "'" & txtSubject.Text & "','" & txtRef.Text & "'," & CDbl(txtPcs.Text) & "," & CDbl(txtCts.Text) & ")")
        Else
            PBResponse = MsgBox("Already Entered. Do you want to modify?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                AdoCN.Execute("UPDATE tblMixPackingListPrint SET Client = '" & txtClient.Text & "',Subject = '" & txtSubject.Text & "',StnRef = '" & txtRef.Text & "',PackPcs = " & CDbl(txtPcs.Text) & ",PackCts = " & CDbl(txtCts.Text) & " " & _
                              "WHERE PackNo = " & CDbl(txtPackNo.Text) & " AND PktSerialNo = " & CDbl(txtIndex.Text) & "")
            End If
        End If
        rsComSql = Nothing

        Load_PackingList()
        txtRef.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
    End Sub

    Private Sub ClearFields()
        txtPackNo.Text = ""
        txtIndex.Text = ""
        txtClient.Text = ""
        txtSubject.Text = ""
        txtRef.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        flxDetails.Rows.Clear()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        txtIndex.Text = flxDetails.Item(0, flxDetails.CurrentRow.Index).Value
        txtClient.Text = flxDetails.Item(1, flxDetails.CurrentRow.Index).Value
        txtSubject.Text = flxDetails.Item(2, flxDetails.CurrentRow.Index).Value
        txtRef.Text = flxDetails.Item(3, flxDetails.CurrentRow.Index).Value
        txtPcs.Text = flxDetails.Item(4, flxDetails.CurrentRow.Index).Value
        txtCts.Text = flxDetails.Item(5, flxDetails.CurrentRow.Index).Value
    End Sub
End Class