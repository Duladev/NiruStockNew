
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_HRDAssessment
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
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdProcess_Click(sender As Object, e As EventArgs) Handles cmdProcess.Click

    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxSummary)
    End Sub

    Private Sub frm_HRDAssessment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_DeptInc()
        Load_PayMonth()
    End Sub

    Private Sub Load_Emp()
        Dim dblMarks1 As Decimal
        Dim dblMarks2 As Decimal
        Dim dblMarks3 As Decimal
        Dim dblMarks4 As Decimal
        Dim dblMarks5 As Decimal

        Dim dblLeave As Double
        Dim dblShortLeave As Double
        Dim dblAbsent As Double
        Dim dblWDays As Double
        Dim dblNoPay As Double
        Dim dblLate As Double

        Dim dtpLastMonthDate As Date

        Dim intFilled As Integer

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbGrp.Text = "" Then MsgBox("Invalid Group", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        flxDetails.Rows.Clear()

        dtpLastMonthDate = CDate(strMonth & "/01/" & strYear)
        dtpLastMonthDate = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, dtpLastMonthDate))

        rsComSql_2 = New ADODB.Recordset
        If cmbSection.Text = "" Then
            rsComSql_2.Open("SELECT TOP (100) PERCENT FullEmpNo, Name, SECTION_DESC, CATEGORY, GRADE, DESIGNATION, DATE_JOINED, DepartmentName, GRP_DESC FROM dbo.VW_EMP_MASTER_SMALL2 WHERE (DEACTIVATE = 0) AND (DepartmentName = '" & cmbDept.Text & "') AND (GRP_DESC = '" & cmbGrp.Text & "') AND (DATE_JOINED <= '" & Format(dtpLastMonthDate, "MM/dd/yyyy") & "') AND (GRADE NOT LIKE 'NO%') AND (PreFix <> 'T') ORDER BY SECTION_DESC, FullEmpNo", dbConn, 1, 1)
        Else
            rsComSql_2.Open("SELECT TOP (100) PERCENT FullEmpNo, Name, SECTION_DESC, CATEGORY, GRADE, DESIGNATION, DATE_JOINED, DepartmentName, GRP_DESC FROM dbo.VW_EMP_MASTER_SMALL2 WHERE (DEACTIVATE = 0) AND (DepartmentName = '" & cmbDept.Text & "') AND (GRP_DESC = '" & cmbGrp.Text & "') AND (DATE_JOINED <= '" & Format(dtpLastMonthDate, "MM/dd/yyyy") & "') AND (SECTION_DESC = '" & cmbSection.Text & "') AND (GRADE NOT LIKE 'NO%') AND (PreFix <> 'T') ORDER BY SECTION_DESC, FullEmpNo", dbConn, 1, 1)
        End If
        If rsComSql_2.RecordCount Then
            rsComSql_2.MoveFirst()
            While Not rsComSql_2.EOF
                dblMarks1 = 5
                dblMarks2 = 5
                dblMarks3 = 5
                dblMarks4 = 10
                dblMarks5 = 5

                intFilled = 0

                rsComSql_4 = New ADODB.Recordset
                rsComSql_4.Open("SELECT * FROM tblHR_Marks WHERE Year1 = '" & strYear & "' AND Month1 = '" & strMonth & "' AND EmpNo = '" & rsComSql_2.Fields("FullEmpNo").Value & "'", dbConn, 1, 1)
                If rsComSql_4.RecordCount Then
                    dblMarks1 = Format(rsComSql_4.Fields("Marks1").Value, "0.00")
                    dblMarks2 = Format(rsComSql_4.Fields("Marks2").Value, "0.00")
                    dblMarks3 = Format(rsComSql_4.Fields("Marks3").Value, "0.00")
                    dblMarks4 = Format(rsComSql_4.Fields("Marks4").Value, "0.00")
                    dblMarks5 = Format(rsComSql_4.Fields("Marks5").Value, "0.00")

                    intFilled = 1
                End If
                rsComSql_4 = Nothing

                dblLeave = 0
                dblNoPay = 0
                rsComSql_4 = New ADODB.Recordset
                rsComSql_4.Open("SELECT (LeaveDays + NoPayDays) AS LeaveDays, NoPayDays FROM VW_EMP_ATTEND WHERE YEAR1 = '" & strYear & "' AND MONTH1 = '" & strMonth & "' AND FullEmpNo = '" & rsComSql_2.Fields("FullEmpNo").Value & "'", dbConn, 1, 1)
                If rsComSql_4.RecordCount Then
                    dblLeave = rsComSql_4.Fields("LeaveDays").Value
                    dblNoPay = rsComSql_4.Fields("NoPayDays").Value
                End If
                rsComSql_4 = Nothing

                'rsComSql_4 = New ADODB.Recordset
                'rsComSql_4.Open("SELECT LDays FROM VW_EMP_LEAVE_ANNUAL WHERE Year1 = '" & strYear & "' AND Month1 = '" & strMonth & "' AND FullEmpNo = '" & rsComSql_2.Fields("FullEmpNo").Value & "'", dbConn, 1, 1)
                'If rsComSql_4.RecordCount Then
                '    dblLeave = rsComSql_4.Fields("LDays").Value
                'End If
                'rsComSql_4 = Nothing

                dblShortLeave = 0
                rsComSql_4 = New ADODB.Recordset
                rsComSql_4.Open("SELECT SDays1 FROM VW_EMP_LEAVE_SHORT WHERE Year1 = '" & strYear & "' AND Month1 = '" & strMonth & "' AND FullEmpNo = '" & rsComSql_2.Fields("FullEmpNo").Value & "'", dbConn, 1, 1)
                If rsComSql_4.RecordCount Then
                    dblShortLeave = rsComSql_4.Fields("SDays1").Value
                End If
                rsComSql_4 = Nothing

                dblWDays = 0
                rsComSql_4 = New ADODB.Recordset
                rsComSql_4.Open("SELECT WorkedDays FROM VW_EMP_ATTEND WHERE YEAR1 = '" & strYear & "' AND MONTH1 = '" & strMonth & "' AND FullEmpNo = '" & rsComSql_2.Fields("FullEmpNo").Value & "'", dbConn, 1, 1)
                If rsComSql_4.RecordCount Then
                    dblWDays = rsComSql_4.Fields("WorkedDays").Value
                End If
                rsComSql_4 = Nothing

                dblLate = 0
                rsComSql_4 = New ADODB.Recordset
                rsComSql_4.Open("SELECT TTimeIn FROM dbo.VW_EMP_ATTENDANCE_ALL WHERE (YEAR(TDate) = '" & strYear & "') AND (MONTH(TDate) = '" & strMonth & "') AND (FullEmpNo = '" & rsComSql_2.Fields("FullEmpNo").Value & "') AND (TTimeIn > CONVERT(DATETIME, '1900-01-01 07:30:00', 102))", dbConn, 1, 1)
                dblLate = rsComSql_4.RecordCount
                rsComSql_4 = Nothing

                dblAbsent = 0

                flxDetails.Rows.Add(rsComSql_2.Fields("FullEmpNo").Value,
                                    rsComSql_2.Fields("Name").Value,
                                    rsComSql_2.Fields("SECTION_DESC").Value,
                                    rsComSql_2.Fields("CATEGORY").Value,
                                    rsComSql_2.Fields("GRADE").Value,
                                    rsComSql_2.Fields("DESIGNATION").Value,
                                    dblMarks1,
                                    dblMarks2,
                                    dblMarks3,
                                    dblMarks4,
                                    dblMarks5,
                                    dblLeave,
                                    dblShortLeave,
                                    dblWDays,
                                    dblNoPay,
                                    dblLate,
                                    Format(rsComSql_2.Fields("DATE_JOINED").Value, "yyyy/MM/dd"),
                                    intFilled,
                                    strYear,
                                    strMonth,
                                    rsComSql_2.Fields("DepartmentName").Value,
                                    rsComSql_2.Fields("GRP_DESC").Value)

                rsComSql_2.MoveNext()
            End While
        End If
        rsComSql_2 = Nothing
    End Sub

    Private Sub cmbSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSection.SelectedIndexChanged
        Load_Emp()
    End Sub

    Private Sub Load_EmpByEmp()
        Dim dblMarks1 As Decimal
        Dim dblMarks2 As Decimal
        Dim dblMarks3 As Decimal
        Dim dblMarks4 As Decimal
        Dim dblMarks5 As Decimal

        Dim dblLeave As Double
        Dim dblShortLeave As Double
        Dim dblAbsent As Double
        Dim dblWDays As Double
        Dim dblNoPay As Double
        Dim dblLate As Double

        Dim dtpLastMonthDate As Date

        Dim intFilled As Integer
        Dim strMonth2 As String
        Dim intMonth As Integer

        If txtEmpNo.Text = "" Then MsgBox("Invalid Emp No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        flxDetails.Rows.Clear()

        For intMonth = 1 To 12
            strMonth2 = Format(intMonth, "00")
            dtpLastMonthDate = CDate(strMonth2 & "/01/" & strYear)
            dtpLastMonthDate = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, dtpLastMonthDate))

            rsComSql_2 = New ADODB.Recordset
            rsComSql_2.Open("SELECT TOP (100) PERCENT FullEmpNo, Name, SECTION_DESC, CATEGORY, GRADE, DESIGNATION, DATE_JOINED, DepartmentName, GRP_DESC FROM dbo.VW_EMP_MASTER_SMALL2 WHERE (DEACTIVATE = 0) AND (DATE_JOINED <= '" & Format(dtpLastMonthDate, "MM/dd/yyyy") & "') AND (GRADE NOT LIKE 'NO%') AND (PreFix <> 'T') AND (FullEmpNo = '" & txtEmpNo.Text & "') ORDER BY SECTION_DESC, FullEmpNo", dbConn, 1, 1)
            If rsComSql_2.RecordCount Then
                rsComSql_2.MoveFirst()
                While Not rsComSql_2.EOF

                    dblMarks1 = 5
                    dblMarks2 = 5
                    dblMarks3 = 5
                    dblMarks4 = 10
                    dblMarks5 = 5

                    intFilled = 0

                    rsComSql_4 = New ADODB.Recordset
                    rsComSql_4.Open("SELECT * FROM tblHR_Marks WHERE Year1 = '" & strYear & "' AND Month1 = '" & strMonth2 & "' AND EmpNo = '" & rsComSql_2.Fields("FullEmpNo").Value & "'", dbConn, 1, 1)
                    If rsComSql_4.RecordCount Then
                        dblMarks1 = Format(rsComSql_4.Fields("Marks1").Value, "0.00")
                        dblMarks2 = Format(rsComSql_4.Fields("Marks2").Value, "0.00")
                        dblMarks3 = Format(rsComSql_4.Fields("Marks3").Value, "0.00")
                        dblMarks4 = Format(rsComSql_4.Fields("Marks4").Value, "0.00")
                        dblMarks5 = Format(rsComSql_4.Fields("Marks5").Value, "0.00")

                        intFilled = 1
                    End If
                    rsComSql_4 = Nothing

                    dblLeave = 0
                    dblNoPay = 0
                    rsComSql_4 = New ADODB.Recordset
                    rsComSql_4.Open("SELECT (LeaveDays + NoPayDays) AS LeaveDays, NoPayDays FROM VW_EMP_ATTEND WHERE YEAR1 = '" & strYear & "' AND MONTH1 = '" & strMonth2 & "' AND FullEmpNo = '" & rsComSql_2.Fields("FullEmpNo").Value & "'", dbConn, 1, 1)
                    If rsComSql_4.RecordCount Then
                        dblLeave = rsComSql_4.Fields("LeaveDays").Value
                        dblNoPay = rsComSql_4.Fields("NoPayDays").Value
                    End If
                    rsComSql_4 = Nothing

                    dblShortLeave = 0
                    rsComSql_4 = New ADODB.Recordset
                    rsComSql_4.Open("SELECT SDays1 FROM VW_EMP_LEAVE_SHORT WHERE Year1 = '" & strYear & "' AND Month1 = '" & strMonth2 & "' AND FullEmpNo = '" & rsComSql_2.Fields("FullEmpNo").Value & "'", dbConn, 1, 1)
                    If rsComSql_4.RecordCount Then
                        dblShortLeave = rsComSql_4.Fields("SDays1").Value
                    End If
                    rsComSql_4 = Nothing

                    dblWDays = 0
                    rsComSql_4 = New ADODB.Recordset
                    rsComSql_4.Open("SELECT WorkedDays FROM VW_EMP_ATTEND WHERE YEAR1 = '" & strYear & "' AND MONTH1 = '" & strMonth2 & "' AND FullEmpNo = '" & rsComSql_2.Fields("FullEmpNo").Value & "'", dbConn, 1, 1)
                    If rsComSql_4.RecordCount Then
                        dblWDays = rsComSql_4.Fields("WorkedDays").Value
                    End If
                    rsComSql_4 = Nothing

                    dblLate = 0
                    rsComSql_4 = New ADODB.Recordset
                    rsComSql_4.Open("SELECT TTimeIn FROM dbo.VW_EMP_ATTENDANCE_ALL WHERE (YEAR(TDate) = '" & strYear & "') AND (MONTH(TDate) = '" & strMonth2 & "') AND (FullEmpNo = '" & rsComSql_2.Fields("FullEmpNo").Value & "') AND (TTimeIn > CONVERT(DATETIME, '1900-01-01 07:30:00', 102))", dbConn, 1, 1)
                    dblLate = rsComSql_4.RecordCount
                    rsComSql_4 = Nothing

                    dblAbsent = 0

                    flxDetails.Rows.Add(rsComSql_2.Fields("FullEmpNo").Value,
                                        rsComSql_2.Fields("Name").Value,
                                        rsComSql_2.Fields("SECTION_DESC").Value,
                                        rsComSql_2.Fields("CATEGORY").Value,
                                        rsComSql_2.Fields("GRADE").Value,
                                        rsComSql_2.Fields("DESIGNATION").Value,
                                        dblMarks1,
                                        dblMarks2,
                                        dblMarks3,
                                        dblMarks4,
                                        dblMarks5,
                                        dblLeave,
                                        dblShortLeave,
                                        dblWDays,
                                        dblNoPay,
                                        dblLate,
                                        Format(rsComSql_2.Fields("DATE_JOINED").Value, "yyyy/MM/dd"),
                                        intFilled,
                                        strYear,
                                        strMonth2,
                                        rsComSql_2.Fields("DepartmentName").Value,
                                        rsComSql_2.Fields("GRP_DESC").Value)

                    rsComSql_2.MoveNext()
                End While
            End If
            rsComSql_2 = Nothing
        Next

    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean

        Dim strMonth2 As String

        blnSave = False

        For intRow = 0 To flxDetails.Rows.Count - 1
            If Not IsNumeric(flxDetails.Item(6, intRow).Value) = True Then
                MsgBox("Invalid Discipline - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(6, intRow).Value) < 0 Then
                MsgBox("Invalid Discipline - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(6, intRow).Value) > 5 Then
                MsgBox("Invalid Discipline - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Not IsNumeric(flxDetails.Item(7, intRow).Value) = True Then
                MsgBox("Invalid Commitment - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(7, intRow).Value) < 0 Then
                MsgBox("Invalid Commitment - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(7, intRow).Value) > 5 Then
                MsgBox("Invalid Commitment - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Not IsNumeric(flxDetails.Item(8, intRow).Value) = True Then
                MsgBox("Invalid Attitude - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(8, intRow).Value) < 0 Then
                MsgBox("Invalid Attitude - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(8, intRow).Value) > 5 Then
                MsgBox("Invalid Attitude - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Not IsNumeric(flxDetails.Item(9, intRow).Value) = True Then
                MsgBox("Invalid Customer Care - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(9, intRow).Value) < 0 Then
                MsgBox("Invalid Customer Care - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(9, intRow).Value) > 10 Then
                MsgBox("Invalid Customer Care - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Not IsNumeric(flxDetails.Item(10, intRow).Value) = True Then
                MsgBox("Invalid Quality - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(10, intRow).Value) < 0 Then
                MsgBox("Invalid Quality - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(10, intRow).Value) > 5 Then
                MsgBox("Invalid Quality - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        For intRow = 0 To flxDetails.Rows.Count - 1
            strMonth2 = flxDetails.Item(19, intRow).Value

            blnSave = True
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblHR_Marks WHERE Year1 = '" & strYear & "' AND Month1 = '" & strMonth2 & "' AND EmpNo = '" & flxDetails.Item(0, intRow).Value & "'", dbConn, 1, 1)
            If rsComSql.RecordCount = 0 Then
                dbConn.Execute("INSERT INTO tblHR_Marks(Year1, Month1, EmpNo, Marks1, Marks2, Marks3, Marks4, Marks5, UserID) " & _
                               "VALUES('" & strYear & "','" & strMonth2 & "','" & flxDetails.Item(0, intRow).Value & "','" & CDbl(flxDetails.Item(6, intRow).Value) & "'," & _
                                "'" & CDbl(flxDetails.Item(7, intRow).Value) & "','" & CDbl(flxDetails.Item(8, intRow).Value) & "','" & CDbl(flxDetails.Item(9, intRow).Value) & "','" & CDbl(flxDetails.Item(10, intRow).Value) & "','" & PBUser_EmpNo & "')")
            Else
                dbConn.Execute("UPDATE tblHR_Marks SET Marks1 = '" & CDbl(flxDetails.Item(6, intRow).Value) & "', Marks2 = '" & CDbl(flxDetails.Item(7, intRow).Value) & "', Marks3 = '" & CDbl(flxDetails.Item(8, intRow).Value) & "', " & _
                                "Marks4 = '" & CDbl(flxDetails.Item(9, intRow).Value) & "', Marks5 = '" & CDbl(flxDetails.Item(10, intRow).Value) & "' " & _
                               "WHERE Year1 = '" & strYear & "' AND Month1 = '" & strMonth2 & "' AND EmpNo = '" & flxDetails.Item(0, intRow).Value & "'")
            End If
            rsComSql = Nothing
        Next

        If blnSave = True Then
            MsgBox("Details Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
        End If

    End Sub

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Load_Emp()
    End Sub

    Private Sub dtpMonth_ValueChanged(sender As Object, e As EventArgs) Handles dtpMonth.ValueChanged
        strMonth = Format(dtpMonth.Value, "MM")
        strYear = Format(dtpMonth.Value, "yyyy")
    End Sub

    Private Sub cmdReport_Click(sender As Object, e As EventArgs) Handles cmdReport.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptHRAssesment.rpt"
        strReportPath = PBReportPath & "HR\" & mReportName
        objForm.Show()
    End Sub

    Private Sub txtEmpNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEmpNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Load_EmpByEmp()
        End If
    End Sub
End Class