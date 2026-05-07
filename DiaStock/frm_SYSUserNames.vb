
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_SYSUserNames
    Dim ValidateOk As Boolean

    Private Sub ClearFields()
        txtUserName.Text = ""
        txtUserID.Text = ""
        txtFullName.Text = ""
        cmbUserLevel.Text = ""
        chkActive.Checked = False
    End Sub

    Private Sub Load_Users()
        flxDept.Rows.Clear()

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblUserLogin ORDER BY UserName", dbConn, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDept.Rows.Add(rsComSql.Fields("UserName").Value,
                                 rsComSql.Fields("EmpId").Value,
                                 rsComSql.Fields("FulName").Value,
                                 rsComSql.Fields("UserLevel").Value,
                                 rsComSql.Fields("Active").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ValidateFields()
        ValidateOk = True
        With txtUserID
            If Trim(.Text) = "" Then _
                MsgBox("Please enter the Emp No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) _
                : .Text = "" _
                : .Focus() _
                : ValidateOk = False _
                : Exit Sub
        End With

        With txtUserName
            If Trim(.Text) = "" Then _
                MsgBox("Please enter the User Name", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) _
                : .Text = "" _
                : .Focus() _
                : ValidateOk = False _
                : Exit Sub
        End With

        With txtFullName
            If Trim(.Text) = "" Then _
                MsgBox("Please enter the Full Name", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) _
                : .Text = "" _
                : .Focus() _
                : ValidateOk = False _
                : Exit Sub
        End With

        With cmbUserLevel
            If Trim(.Text) = "" Then _
                MsgBox("Please enter the User Level", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) _
                : .Text = "" _
                : .Focus() _
                : ValidateOk = False _
                : Exit Sub
        End With

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblUserLogin WHERE UserName = '" & Trim(txtUserName.Text) & "'", dbConn, 1, 1)
        If rsComSql_1.RecordCount Then
            PBResponse = MsgBox("User Name already Exists. Do you want to Update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then

            Else
                ValidateOk = False
                Exit Sub
            End If
        End If
        rsComSql_1 = Nothing

    End Sub

    Private Sub Save()
        On Error GoTo TrappError
        Dim intAllow As Integer

        ValidateFields()
        If ValidateOk = True Then
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblUserLogin WHERE UserName = '" & Trim(txtUserName.Text) & "'", dbConn, 1, 1)
            If rsComSql_1.RecordCount = 0 Then
                dbConn.Execute("INSERT INTO tblUserLogin(FulName, EmpId, UserName, UserPws, Depart, Designation, UEmail, UserContact, UserLevel, Status, UserPrivileges, CreatedDate, Access, ChangeDate, Active) " & _
                               "VALUES('" & Trim(txtFullName.Text) & "','" & txtUserID.Text & "','" & txtUserName.Text & "','1234','','','','','" & cmbUserLevel.Text & "','A','','" & Date.Now & "'," & IIf(chkActive.Checked = True, 1, 0) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "',1)")
            Else
                dbConn.Execute("UPDATE tblUserLogin SET UserLevel = " & cmbUserLevel.Text & ",Active = " & IIf(chkActive.Checked = True, 1, 0) & " WHERE UserName = '" & Trim(txtUserName.Text) & "'")
            End If
            rsComSql_1 = Nothing

            MsgBox("Successfully Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Load_Users()
            ClearFields()
        End If
        Exit Sub
TrappError:
        MsgBox(Err.Description, vbInformation + vbOKOnly, Me.Text)
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub frm_SYSUserNames_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Users()
    End Sub

    Private Sub flxDept_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDept.CellClick
        txtUserName.Text = flxDept.Item(0, flxDept.CurrentRow.Index).Value
        txtUserID.Text = flxDept.Item(1, flxDept.CurrentRow.Index).Value
        txtFullName.Text = flxDept.Item(2, flxDept.CurrentRow.Index).Value
        cmbUserLevel.Text = flxDept.Item(3, flxDept.CurrentRow.Index).Value
        If flxDept.Item(4, flxDept.CurrentRow.Index).Value = 1 Then
            chkActive.Checked = True
        Else
            chkActive.Checked = False
        End If
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub
End Class