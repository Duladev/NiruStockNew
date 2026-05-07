
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_HRDPerformance
    Dim strYear As String
    Dim strMonth As String

    Private Sub Load_DeptInc()
        cmbDept.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT DepartmentName FROM dbo.VW_EMP_MASTER_SMALL2 WHERE (DEACTIVATE = 0) GROUP BY DepartmentName ORDER BY DepartmentName", dbConn, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbDept.Items.Add(rsComSql.Fields("DepartmentName").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_PayMonth()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT PAY_DATE FROM PAYROLL.dbo.PAY_SYS_PAR WHERE COMP_CODE = 'DC'", dbConn, 1, 1)
        If rsComSql.RecordCount = 1 Then
            dtpMonth.Value = Format(rsComSql.Fields("PAY_DATE").Value, "yyyy/MM")
            'dtpMonth.Value = Format(CDate("05/01/2019"), "yyyy/MM")
            strMonth = Format(dtpMonth.Value, "MM")
            strYear = Format(dtpMonth.Value, "yyyy")
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmbDept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDept.SelectedIndexChanged
        cmbGrp.Items.Clear()
        cmbSection.Items.Clear()

        cmbGrp.Text = ""
        cmbSection.Text = ""
        flxDetails.Rows.Clear()

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT TOP (100) PERCENT GRP_DESC FROM dbo.VW_EMP_MASTER_SMALL2 WHERE (DEACTIVATE = 0) AND (DepartmentName = '" & cmbDept.Text & "') GROUP BY GRP_DESC ORDER BY GRP_DESC", dbConn, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                cmbGrp.Items.Add(rsComSql_1.Fields("GRP_DESC").Value)

                rsComSql_1.MoveNext()
            End While
        End If
        rsComSql_1 = Nothing

        cmbGrp.Focus()
    End Sub

    Private Sub cmbGrp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbGrp.SelectedIndexChanged
        cmbSection.Items.Clear()

        cmbSection.Text = ""
        flxDetails.Rows.Clear()

        rsComSql_2 = New ADODB.Recordset
        rsComSql_2.Open("SELECT TOP (100) PERCENT SECTION_DESC FROM dbo.VW_EMP_MASTER_SMALL2 WHERE (DEACTIVATE = 0) AND (DepartmentName = '" & cmbDept.Text & "') AND (GRP_DESC = '" & cmbGrp.Text & "') GROUP BY SECTION_DESC ORDER BY SECTION_DESC", dbConn, 1, 1)
        If rsComSql_2.RecordCount Then
            rsComSql_2.MoveFirst()
            While Not rsComSql_2.EOF
                cmbSection.Items.Add(rsComSql_2.Fields("SECTION_DESC").Value)

                rsComSql_2.MoveNext()
            End While
        End If
        rsComSql_2 = Nothing

        cmbSection.Focus()
    End Sub

    Private Sub ClearFields()
        flxDetails.Rows.Clear()
        txtTotMarks.Text = ""
        txtTotMarks2.Text = ""
        txtAmount.Text = ""
        txtEmpNo.Text = ""
        txtName.Text = ""
        txtKpi.Text = ""
        txtWeightage.Text = ""
        txtTarget.Text = ""
        flxKpi.Rows.Clear()
        txtDept.Text = ""
        txtGrp.Text = ""
        txtSect.Text = ""
        txtDesig.Text = ""
        txtJoinDate.Text = ""
        txtCategory.Text = ""
        picImage.Image = Nothing
        flxOwnKpi.Rows.Clear()
        flxTargets.Rows.Clear()
        Load_Emp2()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxTargets)
    End Sub

    Private Sub frm_HRDAssessment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        'Load_DeptInc()
        Load_PayMonth()
        Load_Emp2()
        Load_OwnKpi()
        txtEmpNo.Focus()
    End Sub

    Private Sub Load_Emp()
        'Dim intIndex As Integer

        'If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        'If cmbGrp.Text = "" Then MsgBox("Invalid Group", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        'flxDetails.Rows.Clear()
        'intIndex = 1
        'rsComSql_2 = New ADODB.Recordset
        'If cmbSection.Text = "" Then
        '    rsComSql_2.Open("SELECT TOP (100) PERCENT FullEmpNo, Name, DepartmentName, GRP_DESC, SECTION_DESC, CATEGORY, GRADE, DESIGNATION FROM dbo.VW_EMP_MASTER_SMALL2 WHERE (DEACTIVATE = 0) AND (DepartmentName = '" & cmbDept.Text & "') AND (GRP_DESC = '" & cmbGrp.Text & "') AND (PROCESS_TYPE = 1 OR PROCESS_TYPE = 2) ORDER BY SECTION_DESC, FullEmpNo", dbConn, 1, 1)
        'Else
        '    rsComSql_2.Open("SELECT TOP (100) PERCENT FullEmpNo, Name, DepartmentName, GRP_DESC, SECTION_DESC, CATEGORY, GRADE, DESIGNATION FROM dbo.VW_EMP_MASTER_SMALL2 WHERE (DEACTIVATE = 0) AND (DepartmentName = '" & cmbDept.Text & "') AND (GRP_DESC = '" & cmbGrp.Text & "') AND (SECTION_DESC = '" & cmbSection.Text & "') AND (PROCESS_TYPE = 1 OR PROCESS_TYPE = 2) ORDER BY SECTION_DESC, FullEmpNo", dbConn, 1, 1)
        'End If
        'If rsComSql_2.RecordCount Then
        '    rsComSql_2.MoveFirst()
        '    While Not rsComSql_2.EOF
        '        intIndex = 1

        '        flxDetails.Rows.Add(rsComSql_2.Fields("FullEmpNo").Value,
        '                            rsComSql_2.Fields("Name").Value,
        '                            rsComSql_2.Fields("DepartmentName").Value,
        '                            rsComSql_2.Fields("SECTION_DESC").Value,
        '                            rsComSql_2.Fields("GRP_DESC").Value,
        '                            rsComSql_2.Fields("CATEGORY").Value,
        '                            rsComSql_2.Fields("GRADE").Value,
        '                            rsComSql_2.Fields("DESIGNATION").Value)

        '        'rsComSql_4 = New ADODB.Recordset
        '        'rsComSql_4.Open("SELECT * FROM tblHR_KPI WHERE EmpNo = '" & rsComSql_2.Fields("FullEmpNo").Value & "' ORDER BY KPI", dbConn, 1, 1)
        '        'If rsComSql_4.RecordCount Then
        '        '    rsComSql_4.MoveFirst()
        '        '    While Not rsComSql_4.EOF
        '        '        flxDetails.Rows.Add(rsComSql_2.Fields("FullEmpNo").Value,
        '        '                        rsComSql_2.Fields("Name").Value,
        '        '                        rsComSql_2.Fields("SECTION_DESC").Value,
        '        '                        rsComSql_2.Fields("CATEGORY").Value,
        '        '                        rsComSql_2.Fields("GRADE").Value,
        '        '                        rsComSql_2.Fields("DESIGNATION").Value,
        '        '                        intIndex,
        '        '                        rsComSql_4.Fields("KPI").Value)

        '        '        intIndex = intIndex + 1
        '        '        rsComSql_4.MoveNext()
        '        '    End While

        '        'End If
        '        'rsComSql_4 = Nothing

        '        rsComSql_2.MoveNext()
        '    End While
        'End If
        'rsComSql_2 = Nothing
    End Sub

    Private Sub Load_OwnKpi()
        Dim intIndex As Integer

        flxOwnKpi.Rows.Clear()
        flxTargets.Rows.Clear()
        intIndex = 1
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblHR_KPI WHERE EmpNo = '" & PBUser_EmpNo & "' ORDER BY KPI", dbConn, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxOwnKpi.Rows.Add(intIndex,
                                   rsComSql.Fields("KPI").Value,
                                   rsComSql.Fields("Weightage").Value,
                                   rsComSql.Fields("Target").Value,
                                   rsComSql.Fields("ID").Value)

                flxTargets.Rows.Add(intIndex,
                                    rsComSql.Fields("Target01").Value,
                                    rsComSql.Fields("Archive01").Value,
                                    rsComSql.Fields("Target02").Value,
                                    rsComSql.Fields("Archive02").Value,
                                    rsComSql.Fields("Target03").Value,
                                    rsComSql.Fields("Archive03").Value,
                                    rsComSql.Fields("Target04").Value,
                                    rsComSql.Fields("Archive04").Value,
                                    rsComSql.Fields("Target05").Value,
                                    rsComSql.Fields("Archive05").Value,
                                    rsComSql.Fields("Target06").Value,
                                    rsComSql.Fields("Archive06").Value,
                                    rsComSql.Fields("Target07").Value,
                                    rsComSql.Fields("Archive07").Value,
                                    rsComSql.Fields("Target08").Value,
                                    rsComSql.Fields("Archive08").Value,
                                    rsComSql.Fields("Target09").Value,
                                    rsComSql.Fields("Archive09").Value,
                                    rsComSql.Fields("Target10").Value,
                                    rsComSql.Fields("Archive10").Value,
                                    rsComSql.Fields("Target11").Value,
                                    rsComSql.Fields("Archive11").Value,
                                    rsComSql.Fields("Target12").Value,
                                    rsComSql.Fields("Archive12").Value,
                                    rsComSql.Fields("ID").Value)

                intIndex = intIndex + 1
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Emp2()
        Dim intIndex As Integer

        flxDetails.Rows.Clear()
        intIndex = 1
        rsComSql_2 = New ADODB.Recordset
        rsComSql_2.Open("SELECT TOP (100) PERCENT FullEmpNo, Name, DepartmentName, GRP_DESC, SECTION_DESC, CATEGORY, GRADE, DESIGNATION " & _
                        "FROM dbo.VW_EMP_MASTER_SMALL2 WHERE (DEACTIVATE = 0) AND (PROCESS_TYPE = 1 OR PROCESS_TYPE = 2) AND (ImSupEmpNo = '" & PBUser_EmpNo & "') ORDER BY FullEmpNo", dbConn, 1, 1)
        If rsComSql_2.RecordCount Then
            rsComSql_2.MoveFirst()
            While Not rsComSql_2.EOF
                intIndex = 1

                flxDetails.Rows.Add(rsComSql_2.Fields("FullEmpNo").Value,
                                    rsComSql_2.Fields("Name").Value,
                                    rsComSql_2.Fields("DepartmentName").Value,
                                    rsComSql_2.Fields("SECTION_DESC").Value,
                                    rsComSql_2.Fields("GRP_DESC").Value,
                                    rsComSql_2.Fields("CATEGORY").Value,
                                    rsComSql_2.Fields("GRADE").Value,
                                    rsComSql_2.Fields("DESIGNATION").Value)

                rsComSql_2.MoveNext()
            End While
        End If
        rsComSql_2 = Nothing
    End Sub

    Private Sub cmbSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSection.SelectedIndexChanged
        Load_Emp()
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean
        Dim dblTotWeight As Double

        blnSave = False

        If flxKpi.Rows.Count < 3 Then
            MsgBox("Minimum 3 KPI's", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If flxKpi.Rows.Count > 5 Then
            MsgBox("Maximum 5 KPI's", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        dblTotWeight = 0
        For intRow = 0 To flxKpi.Rows.Count - 1
            If Len(flxKpi.Item(2, intRow).Value) = 0 Then
                MsgBox("Invalid Weightage", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If Not IsNumeric(flxKpi.Item(2, intRow).Value) Then
                MsgBox("Invalid Weightage", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Len(flxKpi.Item(3, intRow).Value) = 0 Then
                MsgBox("Invalid Target", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If Not IsNumeric(flxKpi.Item(3, intRow).Value) Then
                MsgBox("Invalid Target", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            dblTotWeight = dblTotWeight + CDbl(flxKpi.Item(2, intRow).Value)
        Next
        dblTotWeight = Math.Round(dblTotWeight, 2)

        If dblTotWeight <> 100 Then
            MsgBox("Invalid Weightage. Not 100%", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        dtpToday = GetToday()

        dbConn.Execute("DELETE FROM tblHR_KPI WHERE EmpNo = '" & txtEmpNo.Text & "'")
        For intRow = 0 To flxKpi.Rows.Count - 1
            blnSave = True

            dbConn.Execute("INSERT INTO tblHR_KPI(EmpNo, KPI, DoneBy, DDate, DTime, Weightage, Target) " & _
                           "VALUES('" & txtEmpNo.Text & "','" & flxKpi.Item(1, intRow).Value & "','" & PBUser_EmpNo & "','" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & CDbl(flxKpi.Item(2, intRow).Value) & "','" & CDbl(flxKpi.Item(3, intRow).Value) & "')")
        Next

        If blnSave = True Then
            MsgBox("Details Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
        End If

    End Sub

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Load_Emp()
    End Sub

    Private Sub Load_EmpDetails()
        Dim intIndex As Integer

        txtEmpNo.Text = UCase(txtEmpNo.Text)
        flxKpi.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT FullEmpNo, Name, DepartmentName, GRP_DESC, SECTION_DESC, CATEGORY, GRADE, DESIGNATION, DATE_JOINED FROM dbo.VW_EMP_MASTER_SMALL2 WHERE (DEACTIVATE = 0) AND (PROCESS_TYPE = 1 OR PROCESS_TYPE = 2) AND (FullEmpNo = '" & txtEmpNo.Text & "')", dbConn, 1, 1)
        If rsComSql.RecordCount Then
            txtName.Text = rsComSql.Fields("Name").Value
            txtDept.Text = rsComSql.Fields("DepartmentName").Value
            txtGrp.Text = rsComSql.Fields("GRP_DESC").Value
            txtSect.Text = rsComSql.Fields("SECTION_DESC").Value
            txtDesig.Text = rsComSql.Fields("DESIGNATION").Value
            txtJoinDate.Text = Format(rsComSql.Fields("DATE_JOINED").Value, "yyyy-MM-dd")
            txtCategory.Text = rsComSql.Fields("CATEGORY").Value

            Show_Photo_Path(txtEmpNo.Text)
            txtKpi.Focus()
        Else
            MsgBox("Invalid Emp No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        intIndex = 1
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblHR_KPI WHERE EmpNo = '" & txtEmpNo.Text & "' ORDER BY KPI", dbConn, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxKpi.Rows.Add(intIndex,
                                rsComSql.Fields("KPI").Value,
                                rsComSql.Fields("Weightage").Value,
                                rsComSql.Fields("Target").Value)

                intIndex = intIndex + 1
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Show_Photo_Path(ByVal strEmployeeNo As String)
        Dim filename As String = "\\192.168.2.6\Taaps\Picts" & "\" & strEmployeeNo & ".bmp"
        PBResponse = Dir(filename)
        If Len(PBResponse) > 0 Then
            picImage.Image = Image.FromFile(filename)
        Else
            picImage.Image = Nothing
        End If
    End Sub

    Private Sub txtEmpNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEmpNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If Len(txtEmpNo.Text) = 6 Then
                Load_EmpDetails()
            Else
                MsgBox("Invalid Emp No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        AddRecord()
    End Sub

    Private Sub AddRecord()
        Dim intRow As Integer

        If Len(txtEmpNo.Text) = 6 Then
            txtEmpNo.Text = UCase(txtEmpNo.Text)

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT TOP (100) PERCENT FullEmpNo, Name, SECTION_DESC, CATEGORY, GRADE, DESIGNATION FROM dbo.VW_EMP_MASTER_SMALL2 WHERE (DEACTIVATE = 0) AND (PROCESS_TYPE = 1 OR PROCESS_TYPE = 2) AND (FullEmpNo = '" & txtEmpNo.Text & "')", dbConn, 1, 1)
            If rsComSql.RecordCount Then
            Else
                MsgBox("Invalid Emp No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            If Len(txtKpi.Text) > 0 And Len(txtWeightage.Text) > 0 And Len(txtTarget.Text) > 0 Then
                If flxKpi.Rows.Count > 4 Then
                    MsgBox("Maximum 5 KPI's", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                For intRow = 0 To flxKpi.Rows.Count - 1
                    If txtKpi.Text = flxKpi.Item(1, intRow).Value Then
                        MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        txtKpi.Focus()
                        Exit Sub
                    End If
                Next

                flxKpi.Rows.Add("0",
                                txtKpi.Text,
                                txtWeightage.Text,
                                txtTarget.Text)
                txtKpi.Text = ""
                txtWeightage.Text = ""
                txtTarget.Text = ""
                txtKpi.Focus()
            End If
        End If
    End Sub

    Private Sub flxKpi_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxKpi.CellClick
        txtKpi.Text = flxKpi.Item(1, flxKpi.CurrentRow.Index).Value
        txtWeightage.Text = flxKpi.Item(2, flxKpi.CurrentRow.Index).Value
        txtTarget.Text = flxKpi.Item(3, flxKpi.CurrentRow.Index).Value
    End Sub

    Private Sub flxKpi_DoubleClick(sender As Object, e As EventArgs) Handles flxKpi.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxKpi.Rows.RemoveAt(flxKpi.CurrentRow.Index)
        End If
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        txtEmpNo.Text = flxDetails.Item(0, flxDetails.CurrentRow.Index).Value
        Load_EmpDetails()
    End Sub

    Private Sub txtWeightage_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWeightage.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtWeightage.Text)
    End Sub

    Private Sub txtTarget_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTarget.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtTarget.Text)
    End Sub

    Private Sub Save2()
        Dim intRow As Integer
        Dim intCol As Integer
        Dim blnSave As Boolean

        blnSave = False


        For intRow = 0 To flxTargets.Rows.Count - 1
            For intCol = 1 To 24
                If Len(flxTargets.Item(intCol, intRow).Value) = 0 Then
                    MsgBox("Invalid Entry - Row " & intRow + 1 & "/Col " & intCol + 1, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If Not IsNumeric(flxTargets.Item(intCol, intRow).Value) Then
                    MsgBox("Invalid Entry - Row " & intRow + 1 & "/Col " & intCol + 1, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Next
            

        Next

        For intRow = 0 To flxTargets.Rows.Count - 1
            blnSave = True

            dbConn.Execute("UPDATE tblHR_KPI SET Target01 = '" & CDbl(flxTargets.Item(1, intRow).Value) & "', Archive01 = '" & CDbl(flxTargets.Item(2, intRow).Value) & "', Target02 = '" & CDbl(flxTargets.Item(3, intRow).Value) & "', Archive02 = '" & CDbl(flxTargets.Item(4, intRow).Value) & "'," & _
                            "Target03 = '" & CDbl(flxTargets.Item(5, intRow).Value) & "', Archive03 = '" & CDbl(flxTargets.Item(6, intRow).Value) & "', Target04 = '" & CDbl(flxTargets.Item(7, intRow).Value) & "', Archive04 = '" & CDbl(flxTargets.Item(8, intRow).Value) & "'," & _
                            "Target05 = '" & CDbl(flxTargets.Item(9, intRow).Value) & "', Archive05 = '" & CDbl(flxTargets.Item(10, intRow).Value) & "', Target06 = '" & CDbl(flxTargets.Item(11, intRow).Value) & "', Archive06 = '" & CDbl(flxTargets.Item(12, intRow).Value) & "'," & _
                            "Target07 = '" & CDbl(flxTargets.Item(13, intRow).Value) & "', Archive07 = '" & CDbl(flxTargets.Item(14, intRow).Value) & "',Target08 = '" & CDbl(flxTargets.Item(15, intRow).Value) & "', Archive08 = '" & CDbl(flxTargets.Item(16, intRow).Value) & "'," & _
                            "Target09 = '" & CDbl(flxTargets.Item(17, intRow).Value) & "', Archive09 = '" & CDbl(flxTargets.Item(18, intRow).Value) & "',Target10 = '" & CDbl(flxTargets.Item(19, intRow).Value) & "', Archive10 = '" & CDbl(flxTargets.Item(20, intRow).Value) & "'," & _
                            "Target11 = '" & CDbl(flxTargets.Item(21, intRow).Value) & "', Archive11 = '" & CDbl(flxTargets.Item(22, intRow).Value) & "', Target12 = '" & CDbl(flxTargets.Item(23, intRow).Value) & "', Archive12 = '" & CDbl(flxTargets.Item(24, intRow).Value) & "' WHERE ID = '" & CDbl(flxTargets.Item(25, intRow).Value) & "'")
        Next

        If blnSave = True Then
            MsgBox("Details Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
        End If

    End Sub

    Private Sub cmdSave2_Click(sender As Object, e As EventArgs) Handles cmdSave2.Click
        Save2()
    End Sub
End Class