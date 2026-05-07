
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RprParcel

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtParNo.Text <> "" Then
                If Len(txtParNo.Text) = 7 Then
                    txtParNo.Text = UCase(txtParNo.Text)

                    Load_ParcelDetails()
                End If
            Else
                MsgBox("Pls re-enter Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtParNo.Focus()
            End If
        End If
    End Sub

    Private Sub Load_ParcelDetails()
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT Depart, GrpParNo, IssuedPcs, IssuedCts, Complete, IssueFinish " & _
                      "FROM dbo.tblParcel " & _
                      "WHERE (Depart LIKE 'Rough%' OR Depart = 'Sawing') AND (GrpParNo = '" & txtParNo.Text & "') " & _
                      "ORDER BY Depart", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("Depart").Value,
                                    rsComSql.Fields("GrpParNo").Value,
                                    rsComSql.Fields("IssuedPcs").Value,
                                    Format(rsComSql.Fields("IssuedCts").Value, "#0.000"),
                                    rsComSql.Fields("Complete").Value,
                                    rsComSql.Fields("IssueFinish").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub chkComp_CheckedChanged(sender As Object) Handles chkComp.CheckedChanged
        Dim intRow As Integer

        If chkComp.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(4, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(4, intRow).Value = False
            Next
        End If
    End Sub

    Private Sub chkFin_CheckedChanged(sender As Object) Handles chkFin.CheckedChanged
        Dim intRow As Integer

        If chkFin.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(5, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(5, intRow).Value = False
            Next
        End If
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim intFinish As Integer

        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(4, intRow).Value = True Or flxDetails.Item(4, intRow).Value = 1 Then
                intFinish = 1
            Else
                intFinish = 0
            End If

            AdoCN.Execute("UPDATE tblParcel SET Complete = " & intFinish & " " & _
                          "WHERE Depart = '" & flxDetails.Item(0, intRow).Value & "' AND GrpParNo = '" & flxDetails.Item(1, intRow).Value & "'")
        Next

        MsgBox("Parcels Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        txtParNo.Text = ""
        chkComp.Checked = False
        chkFin.Checked = False
        flxDetails.Rows.Clear()

    End Sub

    Private Sub IssueFinish()
        Dim intRow As Integer
        Dim intFinish As Integer

        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(5, intRow).Value = True Or flxDetails.Item(5, intRow).Value = 1 Then
                intFinish = 1
            Else
                intFinish = 0
            End If

            AdoCN.Execute("UPDATE tblParcel SET IssueFinish = " & intFinish & " " & _
                          "WHERE Depart = '" & flxDetails.Item(0, intRow).Value & "' AND GrpParNo = '" & flxDetails.Item(1, intRow).Value & "'")
        Next

        MsgBox("Parcels Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        txtParNo.Text = ""
        chkComp.Checked = False
        chkFin.Checked = False
        flxDetails.Rows.Clear()

    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdFinish_Click(sender As Object, e As EventArgs) Handles cmdFinish.Click
        IssueFinish()
    End Sub

    Private Sub frm_RprParcel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub
End Class