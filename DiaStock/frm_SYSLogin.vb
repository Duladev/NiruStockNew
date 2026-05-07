
Imports Microsoft.Win32

Public Class frm_SYSLogin

    Private Sub frm_SYSLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbCompany.Text = "DiaStock"
        PBCompName = GetComputerName()
        PBDomainUserName = GetDomainUserName()
        strDBName = cmbCompany.Text
        OpenDB()
    End Sub

    Private Sub txtPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPassword.KeyPress
        On Error GoTo ErrorHandler
        If Asc(e.KeyChar) = 13 Then
            Login()
        End If
        Exit Sub
ErrorHandler:
        MsgBox(Err.Description, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        End
    End Sub

    Private Sub Login()
        PBUser_ID = Trim(txtUserID.Text)
        PBUser_TablePassword = ""
        intPasswordDays = 0

        If cmbCompany.Text <> "" Then
            strDBName = cmbCompany.Text
            OpenDB()
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblUserLogin WHERE UserName = '" & txtUserID.Text & "' AND Active = 1", dbConn, 1, 1)
            If rsComSql.RecordCount > 0 Then
                PBUser_TablePassword = UCase(rsComSql.Fields("UserPws").Value)
                If PBUser_TablePassword = UCase(Trim(txtPassword.Text)) Then
                    PBDesignation = rsComSql.Fields("Designation").Value
                    PBDepartment = rsComSql.Fields("Depart").Value
                    PBUser_Level = rsComSql.Fields("UserLevel").Value
                    PBUser_EmpNo = Trim(rsComSql.Fields("EmpId").Value)
                    intCheckAccess = rsComSql.Fields("Access").Value

                    intPasswordDays = DateDiff(DateInterval.Day, rsComSql.Fields("ChangeDate").Value, Now)

                    If PBUser_EmpNo <> "D06975" Then
                        If intPasswordDays > 90 Then
                            MsgBox("Password has been expired. Please change the Password", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        End If
                    End If

                    Me.Hide()
                    mdiMain.Show()
                Else
                    MsgBox("Invalid Password", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    txtPassword.Text = ""
                    txtPassword.Focus()
                End If
            Else
                MsgBox("Invalid User Name", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtUserID.Text = ""
                txtPassword.Text = ""
                txtUserID.Focus()
            End If
            rsComSql = Nothing
        Else
            MsgBox("Invalid Company", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub cmdLogin_Click(sender As Object, e As EventArgs) Handles cmdLogin.Click
        Login()
    End Sub

    Private Sub txtUserID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUserID.KeyPress
        On Error GoTo ErrorHandler
        If Asc(e.KeyChar) = 13 Then
            Login()
        End If
        Exit Sub
ErrorHandler:
        MsgBox(Err.Description, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
    End Sub

    Private Sub cmbCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCompany.SelectedIndexChanged
        txtUserID.Focus()
    End Sub
End Class