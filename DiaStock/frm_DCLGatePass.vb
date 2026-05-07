
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLGatePass

    Private Sub frm_DCLGatePass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Dept()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub GetData()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT SURNAME,INITIALS,WanCode,DEACTIVATE,DepartmentName FROM VW_EMP_MASTER_SMALL5 WHERE FullEmpNo = '" & txtEmpNo.Text & "' AND Pay = 1", dbConn, 1, 1)
        If rsComSql.RecordCount = 1 Then
            txtEmpName.Text = rsComSql.Fields("SURNAME").Value & " " & rsComSql.Fields("INITIALS").Value
            txtDepartment.Text = rsComSql.Fields("DepartmentName").Value

            'dtpOutTime.Value = Date.Now
            cmdAdd.Focus()
        Else
            MsgBox("Invalid Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            txtEmpNo.Text = ""
            txtEmpName.Text = ""
            txtDepartment.Text = ""
            txtEmpNo.Focus()
            Exit Sub
        End If
        rsComSql = Nothing
    End Sub

    Private Sub txtEmpNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEmpNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If Len(txtEmpNo.Text) = 6 Then
                txtEmpNo.Text = UCase(txtEmpNo.Text)
                GetData()
            Else
                MsgBox("Employee No. should contain six(6) characters", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtEmpNo.Text = ""
                txtEmpName.Text = ""
                txtDepartment.Text = ""
                txtEmpNo.Focus()
            End If
        End If
    End Sub

    Private Sub Load_Dept()
        cmbDeptFrom.Items.Clear()
        cmbDeptTo.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT DepartmentName FROM dbo.VW_EMP_MASTER_SMALL5 WHERE(Pay = 1) GROUP BY DepartmentName ORDER BY DepartmentName", dbConn, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbDeptFrom.Items.Add(rsComSql.Fields("DepartmentName").Value)
                cmbDeptTo.Items.Add(rsComSql.Fields("DepartmentName").Value)

                rsComSql.MoveNext()
            End While
        End If
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim dblPassNo As Double
        Dim blnSave As Boolean

        If cmbReason.Text = "" Then MsgBox("Invalid Reason", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbDeptFrom.Text = "" Then MsgBox("Invalid From Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbDeptTo.Text = "" Then MsgBox("Invalid To Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtPcs.Text = "" Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtPcs.Text) < 0 Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtCts.Text = "" Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtCts.Text) < 0 Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        blnSave = False

        dblPassNo = 0
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(PassNo) AS PassNo FROM tblDCLGatePass", dbConn, 1, 1)
        If Not IsDBNull(rsComSql.Fields("PassNo").Value) Then
            dblPassNo = rsComSql.Fields("PassNo").Value + 1
        Else
            dblPassNo = 1
        End If
        rsComSql = Nothing

        For intRow = 0 To flxDept.Rows.Count - 1
            blnSave = True
            dbConn.Execute("INSERT INTO tblDCLGatePass(PassNo, DDate, DTime, Reason, EmpNo, DeptFrom, DeptTo, CompName, UserName, Pcs, Cts) " & _
                           "VALUES(" & dblPassNo & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "'," & _
                                "'" & cmbReason.Text & "','" & flxDept.Item(1, intRow).Value & "','" & cmbDeptFrom.Text & "','" & cmbDeptTo.Text & "','" & PBCompName & "','" & PBUser_ID & "','" & CDbl(txtPcs.Text) & "','" & CDbl(txtCts.Text) & "')")
        Next
        
        If blnSave = True Then
            MsgBox("Gate Pass No. " & dblPassNo & " Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If

        ClearAll()
    End Sub

    Private Sub ClearAll()
        txtEmpNo.Text = ""
        txtEmpName.Text = ""
        txtDepartment.Text = ""
        cmbReason.Text = ""
        cmbDeptFrom.Text = ""
        cmbDeptTo.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        flxDept.Rows.Clear()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearAll()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLGatePass.rpt"
        strReportPath = PBReportPath & "HR\" & mReportName
        objForm.Show()
    End Sub

    Private Sub txtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub txtCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtCts.Text)
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim intRow As Integer
        Dim intIndex As Integer

        If txtEmpNo.Text <> "" And txtEmpName.Text <> "" And txtDepartment.Text <> "" Then
            If CheckEmployee(Trim(txtEmpNo.Text)) = True Then

            Else
                MsgBox("Invalid Emp No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtEmpNo.Focus()
            End If

            For intRow = 0 To flxDept.Rows.Count - 1
                If flxDept.Item(1, intRow).Value = txtEmpNo.Text Then
                    MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    txtEmpNo.Text = ""
                    txtEmpName.Text = ""
                    txtDepartment.Text = ""
                    txtEmpNo.Focus()
                    Exit Sub
                End If
            Next

            intIndex = flxDept.Rows.Count

            flxDept.Rows.Add(intIndex + 1,
                             UCase(txtEmpNo.Text),
                             txtDepartment.Text)

            txtEmpNo.Text = ""
            txtEmpName.Text = ""
            txtDepartment.Text = ""
            txtEmpNo.Focus()
        End If
    End Sub

    Private Sub flxDept_DoubleClick(sender As Object, e As EventArgs) Handles flxDept.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = vbYes Then
            flxDept.Rows.RemoveAt(flxDept.CurrentRow.Index)
        End If
    End Sub
End Class