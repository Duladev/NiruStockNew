
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_PCUPacketGrp
    Private Sub Load_Flow()

        cmbFlow.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblFlow ORDER BY Flow", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            Do While Not rsComSql.EOF
                cmbFlow.Items.Add(rsComSql.Fields("Flow").Value)
                rsComSql.MoveNext()
            Loop
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Packets()

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblPacket WHERE PktOrdNo = '" & txtOrderNo.Text & "' ORDER BY PktNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                'rsComSql_1 = New ADODB.Recordset
                'rsComSql_1.Open("SELECT PktNo FROM tblMixReturns WHERE ParNo = '" & rsComSql.Fields("PktOrdNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "' AND Sec = 7", AdoCN, 1, 1)
                'If rsComSql_1.RecordCount = 0 Then

                'End If
                'rsComSql_1 = Nothing

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
                If Len(txtOrderNo.Text) = 5 Then
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

    Private Sub SaveFlow()
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If txtOrderNo.Text <> "" And cmbFlow.Text <> "" Then
                For intRow = 0 To flxDetails.Rows.Count - 1
                    If flxDetails.Item(6, intRow).Value = True Then
                        AdoCN.Execute("UPDATE tblPacket SET PktFlow = '" & cmbFlow.Text & "' " & _
                                      "WHERE PktOrdNo = '" & flxDetails.Item(0, intRow).Value & "' AND " & _
                                            "PktNo = '" & flxDetails.Item(1, intRow).Value & "'")
                    End If
                Next

                MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtOrderNo.Text = ""
                txtGroup.Text = ""
                cmbFlow.Text = ""
                flxDetails.Rows.Clear()
            Else
                MsgBox("Invalid Order No./Flow", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        SaveFlow()
    End Sub

    Private Sub SaveGroup()
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If txtOrderNo.Text <> "" And txtGroup.Text <> "" Then
                For intRow = 0 To flxDetails.Rows.Count - 1
                    If flxDetails.Item(6, intRow).Value = True Then
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT Grp FROM tblPacket WHERE PktOrdNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            AdoCN.Execute("UPDATE tblPacket SET Grp = '" & UCase(txtGroup.Text) & "' " & _
                                          "WHERE PktOrdNo = '" & flxDetails.Item(0, intRow).Value & "' AND " & _
                                                "PktNo = '" & flxDetails.Item(1, intRow).Value & "'")
                        End If

                    End If
                Next

                MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtOrderNo.Text = ""
                txtGroup.Text = ""
                cmbFlow.Text = ""
                flxDetails.Rows.Clear()
            Else
                MsgBox("Invalid Order No./Group", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        End If
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        SaveGroup()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub frm_PCUPacketGrp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Flow()
    End Sub
End Class