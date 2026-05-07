
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RprIncentive
    Dim strFolderPath As String

    Private Sub frm_RprIncentive_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        dtpFromDate.Value = dhFirstDayInMonth(Date.Now)
        dtpToDate.Value = Date.Now

        If strDBName = "DiaStock" Then
            strFolderPath = "Rpr\"
        Else
            strFolderPath = "Rpr\"
        End If
    End Sub

    Private Sub GetIncentive()
        Dim strEmpNo As String
        Dim strIncUnit As String
        Dim dblUnitRate As Double
        Dim dblUnits As Double
        Dim dblRate As Double
        Dim dblIncAmount As Double
        'Dim dblIncAmountNew As Double
        Dim intDays As Integer
        Dim intHolidays As Integer
        Dim strGrade As String
        Dim dblLeave As Double
        Dim dblWorkDays As Double
        Dim dblTarget As Single
        Dim dblMins As Single
        Dim dblHours As Single
        Dim dblAbsent As Single
        Dim intRecordNo As Integer
        Dim dtpTargetDate As Date
        Dim mSMonth As Integer

        ExpProgress.Visible = True
        ExpProgress2.Visible = True

        AdoCN.Execute("DELETE FROM tblRPrIncentive")
        ExpProgress.Value = 0
        ExpProgress2.Value = 0
        intRecordNo = 0

        intDays = DateDiff(DateInterval.Day, dtpFromDate.Value, dtpToDate.Value) + 1

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM VW_EMP_HOLIDAYS WHERE HDate BETWEEN '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "' AND '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
        intHolidays = rsComSql_1.RecordCount
        rsComSql_1 = Nothing

        'Sawing
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT EmpNo " & _
                      "FROM dbo.VW_RprIncentive " & _
                      "WHERE (RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') AND (dbo.VW_RprIncentive.Department LIKE 'RoughSawing%')  " & _
                      "GROUP BY EmpNo " & _
                      "ORDER BY EmpNo", AdoCN, 1, 1)

        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            ExpProgress.Maximum = rsComSql.RecordCount
            While Not rsComSql.EOF
                strEmpNo = rsComSql.Fields("EmpNo").Value
                strGrade = ""
                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT * FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & strEmpNo & "'", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    strGrade = rsComSql_2.Fields("GRADE").Value
                    dtpTargetDate = rsComSql_2.Fields("TargetDate").Value
                End If
                rsComSql_2 = Nothing

                mSMonth = DateDiff("M", Format(dtpTargetDate, "MM/dd/yyyy"), Format(dtpFromDate.Value, "MM/dd/yyyy")) + 1

                If mSMonth >= 7 Then
                    mSMonth = 7
                Else
                    If mSMonth > 1 Then
                        If DateAndTime.Day(dtpTargetDate) > 15 Then
                            mSMonth = mSMonth - 2
                        Else
                            mSMonth = mSMonth - 1
                        End If
                    End If
                End If
                If mSMonth = 0 Then
                    mSMonth = 1
                End If

                dblLeave = 0
                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT SUM(NoOfDays + NoPayDays) AS TotLeave FROM VW_EMP_LEAVE WHERE EmpNo = '" & strEmpNo & "' AND LDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "' AND LDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
                dblLeave = IIf(Not IsDBNull(rsComSql_2.Fields("TotLeave").Value), rsComSql_2.Fields("TotLeave").Value, 0)
                rsComSql_2 = Nothing

                dblWorkDays = intDays - (intHolidays + dblLeave)

                dblAbsent = intDays - (dblWorkDays + intHolidays + dblLeave)

                dblMins = 0
                dblHours = 0
                'rsComSql_3 = New ADODB.Recordset
                'rsComSql_3.Open("SELECT SUM(Minutes) AS Minutes FROM VW_EMP_HOURS WHERE TDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "' AND TDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "' AND FullEmpNo = '" & strEmpNo & "'", AdoCN, 1, 1)
                'If rsComSql_3.RecordCount Then
                '    If Not IsDBNull(rsComSql_3.Fields("Minutes").Value) Then
                '        dblMins = rsComSql_3.Fields("Minutes").Value
                '    End If
                'End If
                'rsComSql_3 = Nothing

                'If dblMins > 0 Then
                '    dblHours = Math.Floor((dblMins - (dblWorkDays * 60)) / 60)
                '    dblHours = dblHours + (dblAbsent * 8)
                'End If

                ExpProgress2.Value = 0
                intRecordNo = 0
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.VW_RprIncentive.Department, SUM(dbo.VW_RprIncentive.NopayPcs) AS NopayPcs, SUM(dbo.VW_RprIncentive.LabPcs) AS LabPcs, dbo.VW_RprIncentive.EmpNo, dbo.tblRndOprCat2.Unit " & _
                                "FROM dbo.VW_RprIncentive INNER JOIN dbo.VW_RprReturns20 ON dbo.VW_RprIncentive.Department = dbo.VW_RprReturns20.Department AND dbo.VW_RprIncentive.ParNo = dbo.VW_RprReturns20.ParNo AND " & _
                                    "dbo.VW_RprIncentive.PktNo = dbo.VW_RprReturns20.PktNo LEFT OUTER JOIN dbo.tblRndOprCat2 ON ROUND(ISNULL(dbo.VW_RprReturns20.ActPcs, dbo.VW_RprIncentive.IssPcs) / dbo.VW_RprIncentive.IssCts, 2) >= dbo.tblRndOprCat2.FromSize AND " & _
                                    "ROUND(ISNULL(dbo.VW_RprReturns20.ActPcs, dbo.VW_RprIncentive.IssPcs) / dbo.VW_RprIncentive.IssCts, 2) < dbo.tblRndOprCat2.ToSize " & _
                                "WHERE (dbo.VW_RprIncentive.RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (dbo.VW_RprIncentive.RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') " & _
                                "GROUP BY dbo.VW_RprIncentive.Department, dbo.tblRndOprCat2.Unit, dbo.VW_RprIncentive.EmpNo, dbo.VW_RprReturns20.ActPcs " & _
                                "HAVING (dbo.VW_RprIncentive.Department LIKE 'RoughSawing%') AND (dbo.VW_RprIncentive.EmpNo = '" & strEmpNo & "') AND (SUM(dbo.VW_RprIncentive.LabPcs) > 0) " & _
                                "ORDER BY dbo.VW_RprIncentive.Department", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    ExpProgress2.Maximum = rsComSql_1.RecordCount
                    rsComSql_1.MoveFirst()
                    While Not rsComSql_1.EOF
                        If rsComSql_1.Fields("LabPcs").Value > 0 Then
                            strIncUnit = rsComSql_1.Fields("Unit").Value

                            dblUnitRate = 0
                            dblIncAmount = 0
                            dblRate = 0
                            dblTarget = 0
                            rsComSql_2 = New ADODB.Recordset
                            rsComSql_2.Open("SELECT * FROM tblRPrTgtUnits WHERE Department = '" & rsComSql_1.Fields("Department").Value & "' AND MMonth = '" & mSMonth & "'", AdoCN, 1, 1)
                            If rsComSql_2.RecordCount Then
                                dblUnitRate = Math.Round(rsComSql_2.Fields("Unit" & strIncUnit).Value, 2)
                                dblUnitRate = Math.Round(dblUnitRate, 2)
                                dblRate = rsComSql_2.Fields("RateP").Value
                                dblTarget = rsComSql_2.Fields("TargetHour").Value
                            End If
                            rsComSql_2 = Nothing

                            dblUnits = (rsComSql_1.Fields("LabPcs").Value - rsComSql_1.Fields("NopayPcs").Value) * dblUnitRate
                            dblIncAmount = dblUnits * dblRate

                            AdoCN.Execute("INSERT INTO tblRPrIncentive(EmpNo,WorkDays,Leave,Grade,Department,ParNo,PktNo,RghPcs,RghCts,IncUnit,TotPcs,TotUnits,Rate,IncAmount,Target,Hours) " & _
                                          "VALUES('" & strEmpNo & "'," & dblWorkDays & "," & dblLeave & ",'" & strGrade & "','" & rsComSql_1.Fields("Department").Value & "','',''," & _
                                            "0,0,'" & strIncUnit & "'," & rsComSql_1.Fields("LabPcs").Value & "," & dblUnits & "," & dblRate & "," & dblIncAmount & ",'" & dblTarget & "','" & dblHours & "')")
                        End If

                        intRecordNo = intRecordNo + 1
                        ExpProgress2.Value = intRecordNo
                        Application.DoEvents()
                        rsComSql_1.MoveNext()
                    End While
                End If
                rsComSql_1 = Nothing

                ExpProgress.Value = ExpProgress.Value + 1
                Application.DoEvents()
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        'Non Sawing
        ExpProgress.Value = 0
        ExpProgress2.Value = 0

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT EmpNo " & _
                      "FROM dbo.VW_RprIncentive " & _
                      "WHERE (RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') AND (NOT (dbo.VW_RprIncentive.Department LIKE 'RoughSawing%'))  " & _
                      "GROUP BY EmpNo " & _
                      "ORDER BY EmpNo", AdoCN, 1, 1)

        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            ExpProgress.Maximum = rsComSql.RecordCount
            While Not rsComSql.EOF
                strEmpNo = rsComSql.Fields("EmpNo").Value
                strGrade = ""
                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT * FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & strEmpNo & "'", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    strGrade = rsComSql_2.Fields("GRADE").Value
                    dtpTargetDate = rsComSql_2.Fields("TargetDate").Value
                End If
                rsComSql_2 = Nothing

                mSMonth = DateDiff("M", Format(dtpTargetDate, "MM/dd/yyyy"), Format(dtpFromDate.Value, "MM/dd/yyyy")) + 1

                If mSMonth >= 7 Then
                    mSMonth = 7
                Else
                    If mSMonth > 1 Then
                        If DateAndTime.Day(dtpTargetDate) > 15 Then
                            mSMonth = mSMonth - 2
                        Else
                            mSMonth = mSMonth - 1
                        End If
                    End If
                End If
                If mSMonth = 0 Then
                    mSMonth = 1
                End If

                dblLeave = 0
                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT SUM(NoOfDays + NoPayDays) AS TotLeave FROM VW_EMP_LEAVE WHERE EmpNo = '" & strEmpNo & "' AND LDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "' AND LDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
                dblLeave = IIf(Not IsDBNull(rsComSql_2.Fields("TotLeave").Value), rsComSql_2.Fields("TotLeave").Value, 0)
                rsComSql_2 = Nothing

                dblWorkDays = intDays - (intHolidays + dblLeave)

                dblAbsent = intDays - (dblWorkDays + intHolidays + dblLeave)

                dblMins = 0
                dblHours = 0
                'rsComSql_3 = New ADODB.Recordset
                'rsComSql_3.Open("SELECT SUM(Minutes - SL) AS Minutes FROM VW_EMP_HOURS WHERE TDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "' AND TDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "' AND FullEmpNo = '" & strEmpNo & "'", AdoCN, 1, 1)
                'If rsComSql_3.RecordCount Then
                '    If Not IsDBNull(rsComSql_3.Fields("Minutes").Value) Then
                '        dblMins = rsComSql_3.Fields("Minutes").Value
                '    End If
                'End If
                'rsComSql_3 = Nothing

                'If dblMins > 0 Then
                '    dblHours = Math.Floor((dblMins - (dblWorkDays * 60)) / 60)
                '    dblHours = dblHours + (dblAbsent * 8)
                'End If

                ExpProgress2.Value = 0
                intRecordNo = 0
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.VW_RprIncentive.Department, SUM(dbo.VW_RprIncentive.NopayPcs) AS NopayPcs, SUM(dbo.VW_RprIncentive.LabPcs) AS LabPcs, dbo.VW_RprIncentive.EmpNo, " & _
                                    "dbo.tblRndOprCat2.Unit " & _
                                "FROM dbo.VW_RprIncentive LEFT OUTER JOIN dbo.tblRndOprCat2 ON ROUND(dbo.VW_RprIncentive.IssPcs / dbo.VW_RprIncentive.IssCts, 2) >= dbo.tblRndOprCat2.FromSize AND " & _
                                    "ROUND(dbo.VW_RprIncentive.IssPcs / dbo.VW_RprIncentive.IssCts, 2) < dbo.tblRndOprCat2.ToSize " & _
                                "WHERE (dbo.VW_RprIncentive.RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (dbo.VW_RprIncentive.RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') AND (dbo.VW_RprIncentive.IssCts > 0) " & _
                                "GROUP BY dbo.VW_RprIncentive.Department, dbo.tblRndOprCat2.Unit, dbo.VW_RprIncentive.EmpNo " & _
                                "HAVING (NOT (dbo.VW_RprIncentive.Department LIKE 'RoughSawing%')) AND (dbo.VW_RprIncentive.EmpNo = '" & strEmpNo & "') AND (SUM(dbo.VW_RprIncentive.LabPcs) > 0) " & _
                                "ORDER BY dbo.VW_RprIncentive.Department", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    ExpProgress2.Maximum = rsComSql_1.RecordCount
                    rsComSql_1.MoveFirst()
                    While Not rsComSql_1.EOF
                        If rsComSql_1.Fields("LabPcs").Value > 0 Then
                            strIncUnit = rsComSql_1.Fields("Unit").Value

                            dblUnitRate = 0
                            dblIncAmount = 0
                            dblRate = 0
                            dblTarget = 0
                            rsComSql_2 = New ADODB.Recordset
                            rsComSql_2.Open("SELECT * FROM tblRPrTgtUnits WHERE Department = '" & rsComSql_1.Fields("Department").Value & "' AND MMonth = '" & mSMonth & "'", AdoCN, 1, 1)
                            If rsComSql_2.RecordCount Then
                                dblUnitRate = Math.Round(rsComSql_2.Fields("Unit" & strIncUnit).Value, 2)
                                dblUnitRate = Math.Round(dblUnitRate, 2)
                                dblRate = rsComSql_2.Fields("RateP").Value
                                dblTarget = rsComSql_2.Fields("TargetHour").Value
                            End If
                            rsComSql_2 = Nothing

                            dblUnits = (rsComSql_1.Fields("LabPcs").Value - rsComSql_1.Fields("NopayPcs").Value) * dblUnitRate
                            dblIncAmount = dblUnits * dblRate

                            AdoCN.Execute("INSERT INTO tblRPrIncentive(EmpNo,WorkDays,Leave,Grade,Department,ParNo,PktNo,RghPcs,RghCts,IncUnit,TotPcs,TotUnits,Rate,IncAmount,Target,Hours) " & _
                                          "VALUES('" & strEmpNo & "'," & dblWorkDays & "," & dblLeave & ",'" & strGrade & "','" & rsComSql_1.Fields("Department").Value & "','',''," & _
                                            "0,0,'" & strIncUnit & "'," & rsComSql_1.Fields("LabPcs").Value & "," & dblUnits & "," & dblRate & "," & dblIncAmount & ",'" & dblTarget & "','" & dblHours & "')")
                        End If

                        intRecordNo = intRecordNo + 1
                        ExpProgress2.Value = intRecordNo
                        Application.DoEvents()
                        rsComSql_1.MoveNext()
                    End While
                End If
                rsComSql_1 = Nothing

                ExpProgress.Value = ExpProgress.Value + 1
                Application.DoEvents()
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        ''Opening
        'ExpProgress.Value = 0
        'ExpProgress2.Value = 0

        'rsComSql = New ADODB.Recordset
        'rsComSql.Open("SELECT TOP (100) PERCENT dbo.VW_ExtIncentiveOPE.EmpNo " & _
        '              "FROM dbo.VW_ExtIncentiveOPE INNER JOIN dbo.VW_EMP_MASTER_SMALL ON dbo.VW_ExtIncentiveOPE.EmpNo = dbo.VW_EMP_MASTER_SMALL.FullEmpNo " & _
        '              "WHERE (dbo.VW_ExtIncentiveOPE.RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (dbo.VW_ExtIncentiveOPE.RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') AND  " & _
        '                    "(dbo.VW_EMP_MASTER_SMALL.CATEGORY LIKE 'DIRECT%') " & _
        '              "GROUP BY dbo.VW_ExtIncentiveOPE.EmpNo " & _
        '              "ORDER BY dbo.VW_ExtIncentiveOPE.EmpNo", AdoCN, 1, 1)
        'If rsComSql.RecordCount Then
        '    ExpProgress.Maximum = rsComSql.RecordCount
        '    rsComSql.MoveFirst()
        '    While Not rsComSql.EOF
        '        strEmpNo = rsComSql.Fields("EmpNo").Value

        '        strGrade = ""
        '        rsComSql_2 = New ADODB.Recordset
        '        rsComSql_2.Open("SELECT * FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & strEmpNo & "'", AdoCN, 1, 1)
        '        If rsComSql_2.RecordCount Then
        '            strGrade = rsComSql_2.Fields("GRADE").Value
        '            dtpTargetDate = rsComSql_2.Fields("TargetDate").Value
        '        End If
        '        rsComSql_2 = Nothing

        '        ExpProgress2.Value = 0
        '        intRecordNo = 0
        '        rsComSql_1 = New ADODB.Recordset
        '        rsComSql_1.Open("SELECT TOP (100) PERCENT IncUnit, SUM(RetPcs) AS RetPcs, Sec " & _
        '                        "FROM dbo.VW_ExtIncentiveOPE " & _
        '                        "WHERE (RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') AND (EmpNo = '" & strEmpNo & "') " & _
        '                        "GROUP BY IncUnit, Sec " & _
        '                        "ORDER BY IncUnit", AdoCN, 1, 1)
        '        If rsComSql_1.RecordCount Then
        '            ExpProgress2.Maximum = rsComSql_1.RecordCount
        '            rsComSql_1.MoveFirst()
        '            While Not rsComSql_1.EOF
        '                dblUnitRate = 0
        '                dblIncAmount = 0
        '                dblRate = 0
        '                dblTarget = 0
        '                strIncUnit = rsComSql_1.Fields("IncUnit").Value
        '                rsComSql_2 = New ADODB.Recordset
        '                rsComSql_2.Open("SELECT * FROM tblExtTgtUnitsOPE WHERE IncUnit = '" & strIncUnit & "'", AdoCN, 1, 1)
        '                If rsComSql_2.RecordCount Then
        '                    dblUnitRate = Math.Round(rsComSql_2.Fields("ConRatio").Value, 2)
        '                    dblRate = rsComSql_2.Fields("Rate").Value
        '                End If
        '                rsComSql_2 = Nothing

        '                dblUnits = (rsComSql_1.Fields("RetPcs").Value) * dblUnitRate
        '                dblIncAmount = dblUnits * dblRate

        '                AdoCN.Execute("INSERT INTO tblRPrIncentive(EmpNo,WorkDays,Leave,Grade,Department,ParNo,PktNo,RghPcs,RghCts,IncUnit,TotPcs,TotUnits,Rate,IncAmount,Target,Hours) " & _
        '                              "VALUES('" & strEmpNo & "'," & dblWorkDays & "," & dblLeave & ",'" & strGrade & "','Opening','',''," & _
        '                                "0,0,'" & strIncUnit & "'," & rsComSql_1.Fields("RetPcs").Value & "," & dblUnits & "," & dblRate & "," & dblIncAmount & ",'" & dblTarget & "','" & dblHours & "')")

        '                intRecordNo = intRecordNo + 1
        '                ExpProgress2.Value = intRecordNo
        '                Application.DoEvents()
        '                rsComSql_1.MoveNext()
        '            End While
        '        End If
        '        rsComSql_1 = Nothing

        '        ExpProgress.Value = ExpProgress.Value + 1
        '        Application.DoEvents()
        '        rsComSql.MoveNext()
        '    End While
        'End If
        'rsComSql = Nothing

        ExpProgress.Value = 0
        ExpProgress2.Value = 0

        ''Calculate Incentive Amount
        'rsComSql = New ADODB.Recordset
        'rsComSql.Open("SELECT TOP (100) PERCENT EmpNo " & _
        '              "FROM dbo.tblRPrIncentive " & _
        '              "GROUP BY EmpNo " & _
        '              "ORDER BY EmpNo", AdoCN, 1, 1)
        'If rsComSql.RecordCount Then
        '    rsComSql.MoveFirst()
        '    ExpProgress.Maximum = rsComSql.RecordCount
        '    While Not rsComSql.EOF
        '        strEmpNo = rsComSql.Fields("EmpNo").Value

        '        dblIncAmountNew = 0
        '        rsComSql_1 = New ADODB.Recordset
        '        rsComSql_1.Open("SELECT SUM(TotPcs) AS TotPcs, SUM(TotUnits) AS TotUnits, SUM(IncAmount) AS IncAmount, Rate, Target, Hours " & _
        '                        "FROM tblRPrIncentive " & _
        '                        "WHERE (EmpNo = '" & strEmpNo & "') " & _
        '                        "GROUP BY Rate, Target, Hours", AdoCN, 1, 1)
        '        If rsComSql_1.RecordCount Then
        '            If rsComSql_1.RecordCount = 1 Then
        '                If rsComSql_1.Fields("Target").Value * rsComSql_1.Fields("Hours").Value < rsComSql_1.Fields("TotUnits").Value Then
        '                    dblIncAmountNew = (rsComSql_1.Fields("TotUnits").Value - (rsComSql_1.Fields("Target").Value * rsComSql_1.Fields("Hours").Value)) * rsComSql_1.Fields("Rate").Value
        '                Else
        '                    dblIncAmountNew = 0
        '                End If
        '            Else
        '                rsComSql_1.MoveFirst()
        '                While Not rsComSql_1.EOF
        '                    rsComSql_2 = New ADODB.Recordset
        '                    rsComSql_2.Open("SELECT SUM(TotUnits) AS TotUnits " & _
        '                                    "FROM tblRPrIncentive " & _
        '                                    "WHERE (EmpNo = '" & strEmpNo & "')", AdoCN, 1, 1)
        '                    If rsComSql_2.RecordCount Then
        '                        If Not IsDBNull(rsComSql_2.Fields("TotUnits").Value) Then
        '                            dblIncAmountNew = dblIncAmountNew + (rsComSql_1.Fields("IncAmount").Value - ((rsComSql_1.Fields("TotUnits").Value / rsComSql_2.Fields("TotUnits").Value) * rsComSql_1.Fields("Rate").Value * (rsComSql_1.Fields("Target").Value * rsComSql_1.Fields("Hours").Value)))
        '                        End If
        '                    End If
        '                    rsComSql_2 = Nothing

        '                    rsComSql_1.MoveNext()
        '                End While
        '            End If
        '        End If
        '        rsComSql_1 = Nothing

        '        AdoCN.Execute("UPDATE tblRPrIncentive SET IncAmountNew = '" & dblIncAmountNew & "' WHERE EmpNo = '" & strEmpNo & "'")

        '        rsComSql.MoveNext()
        '    End While
        'End If
        'rsComSql = Nothing


        ExpProgress.Visible = False
        ExpProgress2.Visible = False

        MsgBox("Incentive Process Completed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If MsgBoxResult.Yes Then
            GetIncentive()
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRprIncentive.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub GetLeaves()
        Dim rstLMW As ADODB.Recordset
        Dim EmpNo, Leaves, DLeaves As String

        AdoCN.Execute("DELETE FROM tblRPrLeaveCount")

        rstLMW = New ADODB.Recordset
        mStrSQL = "SELECT * FROM VW_EMP_LEAVE WHERE LDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "' AND LDate <=  '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "'"
        rstLMW.Open(mStrSQL, AdoCN, 1, 1)
        If rstLMW.RecordCount > 0 Then
            With rstLMW
                Do
                    EmpNo = .Fields("EmpNo").Value
                    Leaves = 0
                    DLeaves = 0
                    Do
                        If (.Fields("LType").Value) = "ANNUAL" Then
                            Leaves = Leaves + .Fields("NoOfDays").Value + .Fields("NoPayDays").Value
                        Else
                            DLeaves = DLeaves + .Fields("NoOfDays").Value + .Fields("NoPayDays").Value
                        End If
                        If .EOF = True Then
                            GoTo txt
                        End If
                        .MoveNext()
                        If .EOF = True Then
                            GoTo txt
                        End If
                    Loop Until EmpNo <> .Fields("EmpNo").Value
txt:
                    AdoCN.Execute("INSERT INTO tblRPrLeaveCount(EmpNo,Leave,DLeave) VALUES('" & EmpNo & "', '" & Leaves & "','" & DLeaves & "')")

                Loop Until .EOF
            End With
        End If
        rstLMW = Nothing
    End Sub
End Class