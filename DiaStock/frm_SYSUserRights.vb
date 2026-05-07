
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_SYSUserRights
    Dim ValidateOk As Boolean

    Private Sub Load_Users()
        cmbUser.Items.Clear()
        Dim dtLoading As New DataTable("Process")

        dtLoading.Columns.Add("Name", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("Code", System.Type.GetType("System.String"))

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblUserLogin ORDER BY UserName", dbConn, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                Dim dr As DataRow
                dr = dtLoading.NewRow

                dr("Name") = rsComSql.Fields("UserName").Value
                dr("Code") = rsComSql.Fields("EmpId").Value
                dtLoading.Rows.Add(dr)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        cmbUser.SelectedIndex = -1
        cmbUser.Items.Clear()
        cmbUser.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        cmbUser.SourceDataString = New String(1) {"Name", "Code"}
        cmbUser.SourceDataTable = dtLoading
    End Sub

    Private Sub frm_SYSUserRights_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Users()
        Load_Screens()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmbUser_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbUser.SelectedIndexChanged
        If Not cmbUser.SelectedItem Is Nothing Then
            txtEmpNo.Text = cmbUser.SelectedItem.Col2

            Load_UserDetails()
        Else
            txtEmpNo.Text = ""
        End If
    End Sub

    Private Sub Load_UserDetails()
        Dim index As Integer

        If cmbUser.Text <> "" Then
            For index = 0 To flxDept.Rows.Count - 1
                flxDept.Item(0, index).Value = False
            Next

            For index = 0 To flxDept.Rows.Count - 1
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblSYS_UserRights WHERE EmpNo = '" & Trim(txtEmpNo.Text) & "' AND ScreenName = '" & flxDept.Item(2, index).Value & "' AND Allow = 1", dbConn, 1, 1)
                If rsComSql_1.RecordCount Then
                    flxDept.Item(0, index).Value = True
                End If
                rsComSql_1 = Nothing
            Next
        End If
    End Sub

    Private Sub chkAll_CheckedChanged(sender As Object) Handles chkAll.CheckedChanged
        Dim index As Integer

        If chkAll.Checked = True Then
            For index = 0 To flxDept.Rows.Count - 1
                flxDept.Item(0, index).Value = True
            Next
        Else
            For index = 0 To flxDept.Rows.Count - 1
                flxDept.Item(0, index).Value = False
            Next
        End If
    End Sub

    Private Sub Load_Screens()

        flxDept.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblSYS_UserScreens ORDER BY GroupName, ScreenName", dbConn, 1, 1)
        If rsComSql.RecordCount Then
            While Not rsComSql.EOF
                flxDept.Rows.Add(False,
                                 rsComSql.Fields("GroupName").Value,
                                 rsComSql.Fields("ScreenName").Value,
                                 rsComSql.Fields("FormName").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub ValidateFields()
        ValidateOk = True
        With cmbUser
            If Trim(.Text) = "" Then _
                MsgBox("Please select an User", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) _
                : .Text = "" _
                : .Focus() _
                : ValidateOk = False _
                : Exit Sub
        End With
    End Sub

    Private Sub Save()
        On Error GoTo TrappError
        Dim intAllow As Integer

        ValidateFields()
        If ValidateOk = True Then
            dbConn.Execute("UPDATE tblUserLogin SET Access = 1 WHERE EmpId = '" & Trim(txtEmpNo.Text) & "'")

            For index = 0 To flxDept.Rows.Count - 1
                If flxDept.Item(0, index).Value = True Then
                    intAllow = 1

                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblSYS_UserRights WHERE EmpNo = '" & Trim(txtEmpNo.Text) & "' AND ScreenName = '" & flxDept.Item(2, index).Value & "'", dbConn, 1, 1)
                    If rsComSql_1.RecordCount = 0 Then
                        dbConn.Execute("INSERT INTO tblSYS_UserRights(EmpNo,ScreenName,FormName,Allow) " & _
                                       "VALUES('" & Trim(txtEmpNo.Text) & "','" & flxDept.Item(2, index).Value & "','" & flxDept.Item(3, index).Value & "'," & intAllow & ")")
                    Else
                        dbConn.Execute("UPDATE tblSYS_UserRights SET Allow = " & intAllow & " WHERE EmpNo = '" & Trim(txtEmpNo.Text) & "' AND ScreenName = '" & flxDept.Item(2, index).Value & "'")
                    End If
                    rsComSql_1 = Nothing
                Else
                    intAllow = 0

                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblSYS_UserRights WHERE EmpNo = '" & Trim(txtEmpNo.Text) & "' AND ScreenName = '" & flxDept.Item(2, index).Value & "'", dbConn, 1, 1)
                    If rsComSql_1.RecordCount Then
                        dbConn.Execute("DELETE FROM tblSYS_UserRights WHERE EmpNo = '" & Trim(txtEmpNo.Text) & "' AND ScreenName = '" & flxDept.Item(2, index).Value & "'")
                    End If
                    rsComSql_1 = Nothing
                End If

                
            Next

            MsgBox("Successfully Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

            cmbUser.Text = ""
            txtEmpNo.Text = ""
            For index = 0 To flxDept.Rows.Count - 1
                flxDept.Item(0, index).Value = False
            Next
        End If
        Exit Sub
TrappError:
        MsgBox(Err.Description, vbInformation + vbOKOnly, Me.Text)
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub
End Class