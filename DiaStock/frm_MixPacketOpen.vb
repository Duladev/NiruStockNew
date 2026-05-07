
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixPacketOpen

    Private Sub Load_Packets()

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblMixPacket WHERE PktOrdNo = '" & txtOrderNo.Text & "' AND Ok = 1 AND Accept = 1 AND RejectRep = 1 ORDER BY PktNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("PktOrdNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("PktPcs").Value,
                                    rsComSql.Fields("PktCts").Value,
                                    rsComSql.Fields("Grp").Value,
                                    rsComSql.Fields("PktFlow").Value, False)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub txtOrderNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrderNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtOrderNo.Text <> "" Then
                If Len(txtOrderNo.Text) = 6 Then
                    Load_Packets()
                Else
                    MsgBox("Invalid Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    txtOrderNo.Focus()
                End If
            Else
                MsgBox("Invalid Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtOrderNo.Focus()
            End If
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If txtOrderNo.Text <> "" Then
                For intRow = 0 To flxDetails.Rows.Count - 1
                    If flxDetails.Item(6, intRow).Value = True Then
                        AdoCN.Execute("UPDATE tblMixPacket SET RejectRep = 0 " & _
                                      "WHERE PktOrdNo = '" & flxDetails.Item(0, intRow).Value & "' AND " & _
                                            "PktNo = '" & flxDetails.Item(1, intRow).Value & "'")
                    End If
                Next

                MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtOrderNo.Text = ""
                flxDetails.Rows.Clear()
            Else
                MsgBox("Invalid Order No./Group", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        End If
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub frm_MixPacketOpen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub
End Class