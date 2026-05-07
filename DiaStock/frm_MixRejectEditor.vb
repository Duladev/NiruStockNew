
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixRejectEditor

    Private Sub frm_MixRejectEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtParNo.Text <> "" Then
                txtParNo.Text = UCase(Trim(txtParNo.Text))
                flxDetails.Rows.Clear()
                txtPktNo.Text = ""
                txtPktNo.Focus()
            End If
        End If
    End Sub

    Private Sub txtPktNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtPktNo.Text <> "" Then
                txtPktNo.Text = UCase(Trim(txtPktNo.Text))
                flxDetails.Rows.Clear()
                rsComSql = New ADODB.Recordset
                mStrSQL = "SELECT ID,ParNo,PktNo,Sec,EmpNo,RetPcsT + RetPcsB AS RetPcs,RetCts,RetDate,RetTime,RejPcs,RejCts,LostPcs " & _
                          "FROM tblMixReturns WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND RejPcs > 0 AND (RejStatus = 1 OR RejStatus = 2) ORDER BY Sec"
                rsComSql.Open(mStrSQL, AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM tblMixRejectDetails WHERE ReturnID = " & rsComSql.Fields("ID").Value & "", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            flxDetails.Rows.Add(rsComSql.Fields("ID").Value,
                                                rsComSql.Fields("ParNo").Value,
                                                rsComSql.Fields("PktNo").Value,
                                                rsComSql.Fields("Sec").Value,
                                                rsComSql.Fields("RetPcs").Value,
                                                rsComSql.Fields("RetCts").Value,
                                                rsComSql.Fields("EmpNo").Value,
                                                Format(rsComSql.Fields("RetDate").Value, "yyyy/MM/dd"),
                                                Format(rsComSql.Fields("RetTime").Value, "hh:mm:ss tt"),
                                                rsComSql.Fields("RejPcs").Value,
                                                rsComSql.Fields("RejCts").Value,
                                                rsComSql.Fields("RejCts").Value,
                                                rsComSql.Fields("LostPcs").Value)
                        End If
                        rsComSql_1 = Nothing

                        rsComSql.MoveNext()
                    End While
                Else
                    MsgBox("No Returns", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    txtPktNo.Focus()
                    Exit Sub
                End If
                rsComSql = Nothing
            End If
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        If txtParNo.Text = "" Then Exit Sub
        If txtPktNo.Text = "" Then Exit Sub

        PBResponse = MsgBox("Are you sure to Edit?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If Not IsNumeric(flxDetails.Item(11, intRow).Value) = True Then
                    MsgBox("Invalid Rej Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Next

            For intRow = 0 To flxDetails.Rows.Count - 1
                If CDbl(flxDetails.Item(10, intRow).Value) <> CDbl(flxDetails.Item(11, intRow).Value) Then
                    mStrSQL = "UPDATE tblMixReturns SET RejCts = " & CDbl(flxDetails.Item(11, intRow).Value) & " WHERE ID = '" & flxDetails.Item(0, intRow).Value & "'"
                    AdoCN.Execute(mStrSQL)

                    mStrSQL = "UPDATE tblMixRejectDetails SET Cts = " & CDbl(flxDetails.Item(11, intRow).Value) & ",ModifiedBy = '" & PBUser_EmpNo & "' WHERE ReturnID = '" & flxDetails.Item(0, intRow).Value & "'"
                    AdoCN.Execute(mStrSQL)
                End If
            Next

            MsgBox("Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

            txtPktNo.Text = ""
            txtPktNo.Focus()
            flxDetails.Rows.Clear()
        End If

    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub
End Class