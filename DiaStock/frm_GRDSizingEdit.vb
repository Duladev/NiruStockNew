
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDSizingEdit

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            flxDetails.Rows.Clear()
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT TOP (100) PERCENT ParNo, PktNo, SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts " & _
                          "FROM dbo.tblGrading_SizingTypes " & _
                          "WHERE (OK = 0) AND (ParNo = '" & Trim(txtParNo.Text) & "') " & _
                          "GROUP BY ParNo, PktNo " & _
                          "ORDER BY PktNo", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                        rsComSql.Fields("PktNo").Value,
                                        rsComSql.Fields("Pcs").Value,
                                        Format(rsComSql.Fields("Cts").Value, "#0.000"), False)

                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub Delete()
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure to Delete?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(4, intRow).Value = "1" Then
                    AdoCN.Execute("DELETE FROM tblGrading_SizingReturns WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'")
                    AdoCN.Execute("DELETE FROM tblGrading_SizingTypes WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'")
                End If
            Next

            MsgBox("Sizing Return Deleted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
        End If
    End Sub

    Private Sub ClearFields()
        txtParNo.Text = ""
        flxDetails.Rows.Clear()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Delete()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub frm_GRDSizingEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub
End Class