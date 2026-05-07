
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLDashboard
    Dim rsGetEmp As New ADODB.Recordset
    Dim theDate As Date
    Dim dtpFirstDate As Date
    Dim dblDays As Double
    Dim intHolidays As Integer
    Dim dblMins As Double
    Dim intHours As Integer

    Private Function GetEmpCount(ByVal strDepartment As String) As Integer
        GetEmpCount = 0
        rsGetEmp = New ADODB.Recordset
        rsGetEmp.Open("SELECT EMP_NO FROM vw_pay_emp_master WHERE DepartmentName = '" & strDepartment & "' AND Pay = 1", dbHR, 1, 1)
        GetEmpCount = rsGetEmp.RecordCount
        rsGetEmp = Nothing
    End Function

    Private Function GetAttendCount(ByVal strDepartment As String, ByVal dtpToday As Date) As Integer
        GetAttendCount = 0
        rsGetEmp = New ADODB.Recordset
        rsGetEmp.Open("SELECT EMP_NO FROM vw_pay_attend WHERE DepartmentName = '" & strDepartment & "' AND TDate = '" & Format(dtpToday, "MM/dd/yyyy") & "'", dbHR, 1, 1)
        GetAttendCount = rsGetEmp.RecordCount
        rsGetEmp = Nothing
    End Function

    Private Function GetDutyLeaveCount(ByVal strDepartment As String, ByVal dtpToday As Date) As Integer
        GetDutyLeaveCount = 0
        rsGetEmp = New ADODB.Recordset
        rsGetEmp.Open("SELECT EMP_NO FROM vw_pay_emp_Leave WHERE DepartmentName = '" & strDepartment & "' AND LFrom <= '" & Format(dtpToday, "MM/dd/yyyy") & "' AND LTo >= '" & Format(dtpToday, "MM/dd/yyyy") & "' AND LType = 'DUTY'", dbHR, 1, 1)
        GetDutyLeaveCount = rsGetEmp.RecordCount
        rsGetEmp = Nothing
    End Function

    Private Sub frm_DCLDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        OpenHR()
        RefreshDates()
    End Sub

    Private Sub RefreshDates()
        theDate = Format(Date.Now, "MM/dd/yyyy")
        dtpFirstDate = dhFirstDayInMonth(theDate)
        dblDays = DateDiff(DateInterval.Day, dtpFirstDate, theDate)

        intHolidays = 0

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM VW_EMP_HOLIDAYS WHERE (HDate >= '" & Format(dtpFirstDate, "MM/dd/yyyy") & "') AND (HDate <= '" & Format(theDate, "MM/dd/yyyy") & "')", AdoCN, 1, 1)
        intHolidays = rsComSql.RecordCount
        rsComSql = Nothing

        dblDays = dblDays - intHolidays

        dblMins = DateDiff(DateInterval.Minute, CDate("07:30 AM"), CDate(Format(Date.Now, "HH:mm")))
        intHours = Math.Round(dblMins \ 60, 0)
        If intHours > 9 Then
            intHours = 9
        End If
    End Sub

    Private Sub OpenHR()
        If dbHR.State = 1 Then
            dbHR.Close()
        End If
        dbHR.ConnectionString = "Provider=SQLOLEDB;Data Source='" & strServerName & "';Connect Timeout=60;Initial Catalog=PAYROLL;Integrated Security=SSPI"
        dbHR.Open()
    End Sub

    Private Sub GetProduction()
        Dim dblDailyTarget As Double
        Dim dblTotalPcs As Double
        Dim dblPerc As Double
        Dim dblTotalTarget As Double

        flxDetails.Rows.Clear()

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblMixRef ORDER BY Department, Grp", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                dblDailyTarget = 0
                dblTotalPcs = 0
                dblTotalTarget = 0
                dblPerc = 0

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT SUM(Target) AS Target " & _
                                "FROM dbo.tblDCLDailyTargets " & _
                                "WHERE Department = '" & rsComSql.Fields("TargetDept").Value & "' AND (Grp = '" & rsComSql.Fields("Grp").Value & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("Target").Value) Then
                        dblDailyTarget = rsComSql_1.Fields("Target").Value
                    End If
                End If
                rsComSql_1 = Nothing

                dblTotalTarget = (dblDailyTarget * dblDays) + ((dblDailyTarget / 9) * intHours)

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT SUM(dbo.tblMIXReturns.RetPcsT + dbo.tblMIXReturns.RetPcsB) AS TotalPcs " & _
                                "FROM dbo.tblMIXReturns INNER JOIN dbo.tblMIXPacket ON dbo.tblMIXReturns.ParNo = dbo.tblMIXPacket.PktOrdNo AND dbo.tblMIXReturns.PktNo = dbo.tblMIXPacket.PktNo " & _
                                "WHERE (dbo.tblMIXReturns.Sec = 14) AND (dbo.tblMIXReturns.RetDate >= '" & dtpFirstDate & "') AND (dbo.tblMIXReturns.RetDate <= '" & theDate & "') AND (dbo.tblMIXPacket.Grp = '" & rsComSql.Fields("Grp").Value & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("TotalPcs").Value) Then
                        dblTotalPcs = rsComSql_1.Fields("TotalPcs").Value
                    End If
                End If
                rsComSql_1 = Nothing

                If dblTotalTarget > 0 Then
                    dblPerc = (dblTotalPcs / dblTotalTarget) * 100
                End If

                flxDetails.Rows.Add(rsComSql.Fields("Department").Value,
                                    rsComSql.Fields("Grp").Value,
                                    dblDailyTarget,
                                    Math.Round(dblTotalTarget, 0),
                                    dblTotalPcs,
                                    Math.Round(dblPerc, 0) & "%",
                                    Math.Round(dblTotalPcs / dblDays, 0))

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        RefreshDates()
        GetProduction()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'RefreshDates()
        'GetProduction()
    End Sub
End Class