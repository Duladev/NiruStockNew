
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_SYSUserChange
    Private Sub ClearFields()
        txtPassword.Text = ""
        txtNewPassword.Text = ""
        txtConPassword.Text = ""
        txtPassword.Select()
    End Sub

    Private Sub frm_SYSUserChange_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtUserID.Text = PBUser_ID
        ClearFields()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub txtPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPassword.KeyPress
        If Asc(e.KeyChar) = 13 And txtPassword.Text <> "" Then
            txtNewPassword.Focus()
        End If
    End Sub

    Private Sub txtNewPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNewPassword.KeyPress
        If Asc(e.KeyChar) = 13 And txtNewPassword.Text <> "" Then
            txtConPassword.Focus()
        End If
    End Sub

    Private Sub Save()
        If txtPassword.Text = "" Then MsgBox("Enter the Old Password", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtNewPassword.Text = "" Then MsgBox("Enter the New Password", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtConPassword.Text = "" Then MsgBox("Confirm the Password", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtPassword.Text = txtNewPassword.Text Then MsgBox("Cannot enter the Old Password again", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtNewPassword.Text <> txtConPassword.Text Then MsgBox("Passwords not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        dtpToday = GetToday()

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblUserLogin WHERE UserName = '" & txtUserID.Text & "'", dbConn, 1, 1)
        If rsComSql.RecordCount > 0 Then
            PBUser_TablePassword = UCase(rsComSql.Fields("UserPws").Value)
        End If
        rsComSql = Nothing

        If PBUser_TablePassword <> UCase(Trim(txtPassword.Text)) Then
            MsgBox("Old Password is incorrect", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If ValidPassword(txtNewPassword.Text) Then

        Else
            MsgBox("Entered Password Is Weak", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        dbConn.BeginTrans()
        dbConn.Execute("UPDATE tblUserLogin SET UserPws = '" & Trim(txtNewPassword.Text) & "', ChangeDate = '" & Format(dtpToday, "MM/dd/yyyy") & "' WHERE UserName = '" & PBUser_ID & "'")
        dbConn.CommitTrans()

        MsgBox("Password Successfully Changed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Function ValidPassword(myPassword As String) As Boolean
        If myPassword.Length < 8 Then Return False
        If Not myPassword.Any(Function(c) Char.IsDigit(c)) Then Return False
        If Not myPassword.Any(Function(c) Char.IsLower(c)) Then Return False
        If Not myPassword.Any(Function(c) Char.IsUpper(c)) Then Return False
        Return True
    End Function
End Class