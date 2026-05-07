
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLParcelOpen
    Private Sub Load_Parcels()
        If txtSupParNo.Text = "" Then MsgBox("Invalid Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        If chkHide.Checked = False Then
            rsComSql.Open("SELECT TOP (100) PERCENT Depart, GrpParNo, IssuedPcs, IssuedCts, Complete, OrigParcelNo " & _
                      "FROM dbo.tblParcel " & _
                      "WHERE (Depart NOT LIKE 'Rough%' OR Depart = 'RoughSales' OR Depart = 'Rough Dept') AND (Complete = 1) AND (Hide = 0) AND (OrigParcelNo = '" & txtSupParNo.Text & "') " & _
                      "ORDER BY Depart, GrpParNo", AdoCN, 1, 1)
        Else
            rsComSql.Open("SELECT TOP (100) PERCENT Depart, GrpParNo, IssuedPcs, IssuedCts, Complete, OrigParcelNo " & _
                      "FROM dbo.tblParcel " & _
                      "WHERE (Depart NOT LIKE 'Rough%' OR Depart = 'RoughSales' OR Depart = 'Rough Dept') AND (Complete = 1) AND (Hide = 1) AND (OrigParcelNo = '" & txtSupParNo.Text & "') " & _
                      "ORDER BY Depart, GrpParNo", AdoCN, 1, 1)
        End If

        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("Depart").Value,
                                    rsComSql.Fields("GrpParNo").Value,
                                    rsComSql.Fields("IssuedPcs").Value,
                                    Format(rsComSql.Fields("IssuedCts").Value, "#0.000"),
                                    False,
                                    rsComSql.Fields("OrigParcelNo").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        Load_Parcels()
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(4, intRow).Value = True Then
                AdoCN.Execute("UPDATE tblParcel SET Complete = 0 " & _
                              "WHERE Depart = '" & flxDetails.Item(0, intRow).Value & "' AND GrpParNo = '" & flxDetails.Item(1, intRow).Value & "'")
            End If
        Next

        MsgBox("Parcels Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        flxDetails.Rows.Clear()
        txtSupParNo.Text = ""
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub frm_DCLParcelOpen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub
End Class