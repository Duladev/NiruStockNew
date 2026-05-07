
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_HRDIncentive
    Dim strYear As String
    Dim strMonth As String

    Private Sub Load_DeptInc()
        cmbDept.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT DepartmentName FROM dbo.VW_EMP_MASTER_SMALL2 WHERE (DEACTIVATE = 0) GROUP BY DepartmentName ORDER BY DepartmentName", AdoCN, 1, 1)
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
        rsComSql.Open("SELECT PAY_DATE FROM PAYROLL.dbo.PAY_SYS_PAR WHERE COMP_CODE = 'DC'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 1 Then
            'dtpMonth.Value = Format(rsComSql.Fields("PAY_DATE").Value, "yyyy/MM")
            dtpMonth.Value = Format(CDate("12/01/2021"), "yyyy/MM")
            strMonth = Format(dtpMonth.Value, "MM")
            strYear = Format(dtpMonth.Value, "yyyy")
        End If
        rsComSql = Nothing
    End Sub

    Private Sub frm_HRDIncentive_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_DeptInc()
        Load_PayMonth()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmbDept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDept.SelectedIndexChanged
        cmbGrp.Items.Clear()
        cmbEmpNo.Items.Clear()

        cmbGrp.Text = ""
        cmbEmpNo.Text = ""
        flxDetails.Rows.Clear()

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT TOP (100) PERCENT GRP_DESC FROM dbo.VW_EMP_MASTER_SMALL2 WHERE (DEACTIVATE = 0) AND (DepartmentName = '" & cmbDept.Text & "') GROUP BY GRP_DESC ORDER BY GRP_DESC", AdoCN, 1, 1)
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
        cmbEmpNo.Items.Clear()

        cmbEmpNo.Text = ""
        flxDetails.Rows.Clear()

        rsComSql_2 = New ADODB.Recordset
        rsComSql_2.Open("SELECT TOP (100) PERCENT FullEmpNo FROM dbo.VW_EMP_MASTER_SMALL2 WHERE (DEACTIVATE = 0) AND (DepartmentName = '" & cmbDept.Text & "') AND (GRP_DESC = '" & cmbGrp.Text & "') AND (DEACTIVATE = 0) AND (NOT (CATEGORY LIKE 'DIRECT%')) AND (PROCESS_TYPE = 3 OR PROCESS_TYPE = 2) GROUP BY FullEmpNo ORDER BY FullEmpNo", AdoCN, 1, 1)
        If rsComSql_2.RecordCount Then
            rsComSql_2.MoveFirst()
            While Not rsComSql_2.EOF
                cmbEmpNo.Items.Add(rsComSql_2.Fields("FullEmpNo").Value)

                rsComSql_2.MoveNext()
            End While
        End If
        rsComSql_2 = Nothing

        cmbEmpNo.Focus()
    End Sub

    Private Sub cmbEmpNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEmpNo.SelectedIndexChanged
        Dim strSection As String
        Dim dblMarks As Double
        Dim dblMarks2 As Double
        Dim dblTotMarks As Double
        Dim dblTotMarks2 As Double
        Dim dblLeave As Double
        Dim dblNoPay As Double
        Dim dblLate As Double
        Dim dblShortLeave As Double
        Dim dblNights As Double

        flxDetails.Rows.Clear()
        dblTotMarks = 0
        dblTotMarks2 = 0
        dblLeave = 0
        dblNoPay = 0
        dblLate = 0
        dblShortLeave = 0
        dblNights = 0

        strSection = ""
        rsComSql_3 = New ADODB.Recordset
        rsComSql_3.Open("SELECT SECTION_DESC FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & cmbEmpNo.Text & "'", AdoCN, 1, 1)
        If rsComSql_3.RecordCount Then
            strSection = rsComSql_3.Fields("SECTION_DESC").Value
        End If
        rsComSql_3 = Nothing

        rsComSql_3 = New ADODB.Recordset
        rsComSql_3.Open("SELECT TOP (100) PERCENT dbo.tblHR_Criteria.Main, dbo.tblHR_Criteria.Sub, dbo.tblHR_Criteria.Max, dbo.VW_EMP_DEP_GRP.DepartmentName, " & _
                            "dbo.VW_EMP_DEP_GRP.GRP_DESC, dbo.VW_EMP_DEP_GRP.WanCode, dbo.tblHR_CriteriaDep.CrID " & _
                        "FROM dbo.tblHR_Criteria INNER JOIN dbo.tblHR_CriteriaDep ON dbo.tblHR_Criteria.ID = dbo.tblHR_CriteriaDep.CrID INNER JOIN " & _
                            "dbo.VW_EMP_DEP_GRP ON dbo.tblHR_CriteriaDep.GrpID = dbo.VW_EMP_DEP_GRP.GrpID " & _
                        "WHERE (dbo.VW_EMP_DEP_GRP.DepartmentName = '" & cmbDept.Text & "') AND (dbo.VW_EMP_DEP_GRP.GRP_DESC = '" & cmbGrp.Text & "') " & _
                        "ORDER BY dbo.VW_EMP_DEP_GRP.DepartmentName, dbo.VW_EMP_DEP_GRP.GRP_DESC, dbo.tblHR_Criteria.Main, dbo.tblHR_Criteria.Sub", AdoCN, 1, 1)
        If rsComSql_3.RecordCount Then
            rsComSql_3.MoveFirst()
            While Not rsComSql_3.EOF
                dblMarks = 0
                dblMarks2 = 0
                rsComSql_4 = New ADODB.Recordset
                rsComSql_4.Open("SELECT * FROM tblHR_Details WHERE Year1 = '" & strYear & "' AND Month1 = '" & strMonth & "' AND EmpNo = '" & cmbEmpNo.Text & "' AND CrID = " & rsComSql_3.Fields("CrID").Value & "", AdoCN, 1, 1)
                If rsComSql_4.RecordCount Then
                    dblMarks = rsComSql_4.Fields("Marks").Value
                    dblMarks2 = rsComSql_4.Fields("Marks2").Value
                End If
                rsComSql_4 = Nothing

                If Trim(rsComSql_3.Fields("Sub").Value) = "Attendance" Then
                    dblMarks = 25
                    rsComSql_4 = New ADODB.Recordset
                    rsComSql_4.Open("SELECT * FROM VW_EMP_ATTEND WHERE YEAR1 = '" & strYear & "' AND MONTH1 = '" & strMonth & "' AND FullEmpNo = '" & cmbEmpNo.Text & "'", AdoCN, 1, 1)
                    If rsComSql_4.RecordCount Then
                        dblLeave = rsComSql_4.Fields("LeaveDays").Value
                        dblNoPay = rsComSql_4.Fields("NoPayDays").Value
                    End If
                    rsComSql_4 = Nothing

                    If dblLeave > 1 Then
                        dblMarks = dblMarks - ((dblLeave - 1) * 3)
                    End If
                    dblMarks = dblMarks - (dblNoPay * 5)

                    If cmbDept.Text = "GRADING" Then
                        rsComSql_4 = New ADODB.Recordset
                        rsComSql_4.Open("SELECT FullEmpNo,CATEGORY FROM VW_EMP_ATTENDANCE WHERE YEAR(TDate)  = '" & strYear & "' AND MONTH(TDate) = '" & strMonth & "' AND FullEmpNo = '" & cmbEmpNo.Text & "' AND TTimeIn > '01/01/1900 07:35:59'", AdoCN, 1, 1)
                        dblLate = rsComSql_4.RecordCount
                        If dblLate > 0 Then
                            rsComSql_5 = New ADODB.Recordset
                            rsComSql_5.Open("SELECT * FROM VW_EMP_LEAVE_SL WHERE FullEmpNo = '" & cmbEmpNo.Text & "' AND YEAR(LFrom) = '" & strYear & "' AND MONTH(LFrom) = '" & strMonth & "' AND Purpose = 'LATE'", AdoCN, 1, 1)
                            dblShortLeave = rsComSql_5.RecordCount
                            rsComSql_5 = Nothing

                            dblLate = dblLate - dblShortLeave
                            If rsComSql_4.Fields("CATEGORY").Value = "ADMINISTRATORS" Then
                                dblMarks = dblMarks - (dblLate)
                            ElseIf rsComSql_4.Fields("CATEGORY").Value = "CHECKERS" Then
                                dblMarks = dblMarks - (dblLate * 3)
                            ElseIf rsComSql_4.Fields("CATEGORY").Value = "LEADERS" Then
                                If strSection = "Admin" Then
                                    dblMarks = dblMarks - (dblLate)
                                ElseIf strSection = "Checking" Then
                                    dblMarks = dblMarks - (dblLate * 3)
                                End If
                            End If
                        End If
                        rsComSql_4 = Nothing
                    End If
                End If

                If Trim(rsComSql_3.Fields("Sub").Value) = "Extra work performed" Then
                    dblMarks = 0
                    rsComSql_4 = New ADODB.Recordset
                    rsComSql_4.Open("SELECT FullEmpNo,CATEGORY FROM VW_EMP_ATTENDANCE WHERE YEAR(TDate)  = '" & strYear & "' AND MONTH(TDate) = '" & strMonth & "' AND FullEmpNo = '" & cmbEmpNo.Text & "' AND TTimeIn > '01/01/1900 19:40:00'", AdoCN, 1, 1)
                    dblNights = rsComSql_4.RecordCount

                    If rsComSql_4.Fields("CATEGORY").Value = "ADMINISTRATORS" Then
                        dblMarks = dblNights * 3
                    ElseIf rsComSql_4.Fields("CATEGORY").Value = "CHECKERS" Then
                        dblMarks = dblNights * 3
                    ElseIf rsComSql_4.Fields("CATEGORY").Value = "LEADERS" Then
                        If strSection = "Admin" Then
                            dblMarks = dblNights * 3
                        ElseIf strSection = "Checking" Then
                            dblMarks = dblNights * 3
                        End If
                    End If

                    rsComSql_4 = Nothing
                End If

                flxDetails.Rows.Add(rsComSql_3.Fields("Main").Value,
                                    rsComSql_3.Fields("Sub").Value,
                                    rsComSql_3.Fields("Max").Value,
                                    dblMarks,
                                    rsComSql_3.Fields("CrID").Value,
                                    dblMarks2)

                dblTotMarks = dblTotMarks + dblMarks
                dblTotMarks2 = dblTotMarks2 + dblMarks2
                rsComSql_3.MoveNext()
            End While
        End If
        rsComSql_3 = Nothing
        txtTotMarks.Text = dblTotMarks
        txtTotMarks2.Text = dblTotMarks2

        txtAmount.Text = GetIncentive(cmbEmpNo.Text, dblTotMarks, dblTotMarks2, strYear, strMonth)

        flxDetails.Focus()
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbGrp.Text = "" Then MsgBox("Invalid Group", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbEmpNo.Text = "" Then MsgBox("Invalid Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        For intRow = 0 To flxDetails.Rows.Count - 1
            If Not IsNumeric(flxDetails.Item(3, intRow).Value) = True Then
                MsgBox("Invalid Marks - " & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(3, intRow).Value) < 0 Then
                MsgBox("Invalid Marks - " & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(3, intRow).Value) > CDbl(flxDetails.Item(2, intRow).Value) Then
                MsgBox("Invalid Marks - " & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Not IsNumeric(flxDetails.Item(5, intRow).Value) = True Then
                MsgBox("Invalid Marks - " & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(5, intRow).Value) < 0 Then
                MsgBox("Invalid Marks - " & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(5, intRow).Value) > CDbl(flxDetails.Item(2, intRow).Value) Then
                MsgBox("Invalid Marks - " & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(3, intRow).Value) > 0 And CDbl(flxDetails.Item(5, intRow).Value) > 0 Then
                MsgBox("Invalid Marks - " & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblHR_Details WHERE Year1 = '" & strYear & "' AND Month1 = '" & strMonth & "' AND EmpNo = '" & cmbEmpNo.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                AdoCN.Execute("INSERT INTO tblHR_Details(Year1,Month1,EmpNo,CrID,Marks,UserID,Marks2) " & _
                              "VALUES('" & strYear & "','" & strMonth & "','" & cmbEmpNo.Text & "','" & CDbl(flxDetails.Item(4, intRow).Value) & "','" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & PBUser_EmpNo & "','" & CDbl(flxDetails.Item(5, intRow).Value) & "')")
            Next

            MsgBox("Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

            cmbEmpNo.Text = ""
            flxDetails.Rows.Clear()
        Else
            PBResponse = MsgBox("Already Entered. Do you want Update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                AdoCN.Execute("DELETE FROM tblHR_Details WHERE Year1 = '" & strYear & "' AND Month1 = '" & strMonth & "' AND EmpNo = '" & cmbEmpNo.Text & "'")

                For intRow = 0 To flxDetails.Rows.Count - 1
                    AdoCN.Execute("INSERT INTO tblHR_Details(Year1,Month1,EmpNo,CrID,Marks,UserID,Marks2) " & _
                                  "VALUES('" & strYear & "','" & strMonth & "','" & cmbEmpNo.Text & "','" & CDbl(flxDetails.Item(4, intRow).Value) & "','" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & PBUser_EmpNo & "','" & CDbl(flxDetails.Item(5, intRow).Value) & "')")
                Next

                MsgBox("Updated Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

                cmbEmpNo.Text = ""
                flxDetails.Rows.Clear()
            End If
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        flxDetails.Rows.Clear()
        txtTotMarks.Text = ""
        txtTotMarks2.Text = ""
        txtAmount.Text = ""
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub Process()
        Dim dblEmpInc As Double
        Dim intCounter As Integer

        flxSummary.Rows.Clear()
        dblEmpInc = 0
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblHR_Details.Year1, dbo.tblHR_Details.Month1, dbo.VW_EMP_MASTER_SMALL.DepartmentName, dbo.VW_EMP_MASTER_SMALL.GRP_DESC, dbo.tblHR_Details.EmpNo, " & _
                            "SUM(dbo.tblHR_Details.Marks) AS Marks, SUM(dbo.tblHR_Details.Marks2) AS Marks2 " & _
                      "FROM dbo.tblHR_Details INNER JOIN dbo.VW_EMP_MASTER_SMALL ON dbo.tblHR_Details.EmpNo = dbo.VW_EMP_MASTER_SMALL.FullEmpNo " & _
                      "GROUP BY dbo.tblHR_Details.Year1, dbo.tblHR_Details.Month1, dbo.tblHR_Details.EmpNo, dbo.VW_EMP_MASTER_SMALL.DepartmentName, dbo.VW_EMP_MASTER_SMALL.GRP_DESC " & _
                      "HAVING (dbo.tblHR_Details.Year1 = '" & strYear & "') AND (dbo.tblHR_Details.Month1 = '" & strMonth & "') " & _
                      "ORDER BY dbo.VW_EMP_MASTER_SMALL.DepartmentName, dbo.VW_EMP_MASTER_SMALL.GRP_DESC, dbo.tblHR_Details.EmpNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = rsComSql.RecordCount
            intCounter = 0
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                intCounter = intCounter + 1
                'If rsComSql.Fields("EmpNo").Value = "D08728" Then
                '    MsgBox(rsComSql.Fields("EmpNo").Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                'End If
                dblEmpInc = GetIncentive(rsComSql.Fields("EmpNo").Value, rsComSql.Fields("Marks").Value, rsComSql.Fields("Marks2").Value, strYear, strMonth)
                flxSummary.Rows.Add(rsComSql.Fields("EmpNo").Value,
                                    rsComSql.Fields("DepartmentName").Value,
                                    rsComSql.Fields("GRP_DESC").Value,
                                    rsComSql.Fields("Marks").Value,
                                    rsComSql.Fields("Marks2").Value,
                                    Format(dblEmpInc, "#0.00"))

                rsComSql.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        rsComSql = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub cmdProcess_Click(sender As Object, e As EventArgs) Handles cmdProcess.Click
        Process()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxSummary)
    End Sub
End Class