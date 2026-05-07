
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RndIncentive
    Dim strFolderPath As String

    Private Sub frm_RndIncentive_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        dtpFromDate.Value = dhFirstDayInMonth(Date.Now)
        dtpToDate.Value = Date.Now
        fraIncentive.Visible = False

        If strDBName = "DiaStock" Then
            strFolderPath = "RoundsFullFlow\"
        Else
            strFolderPath = "RoundsFullFlow\"
        End If
    End Sub

    Private Sub GetIncentive()
        Dim strYear As String : Dim strMonth As String

        Dim strSection As String : Dim strGroup As String
        Dim strGrade As String : Dim strCategory As String

        Dim dtpStartDate As Date : Dim dtpEndDate As Date
        Dim strEmpNo As String
        Dim intSec As Integer : Dim dblRate As Double

        Dim strFlow As String : Dim dblUnitPcs As Double
        Dim dblTotUnitPcs As Double : Dim dblTotProdPcs As Double
        Dim dblTotIncPcs As Double : Dim dblTotBroPcs As Double
        Dim dblTotRejPcs As Double : Dim dblTotRepPcs As Double
        Dim dblTotLostPcs As Double : Dim dblTotNoPayPcs As Double

        Dim dblActRgh As Double : Dim dblIssCts As Double
        Dim dblRetCts As Double : Dim dblTargetP As Double

        Dim intDays As Integer : Dim intHolidays As Integer
        Dim dblLeave As Double : Dim dblWorkDays As Double
        Dim mSMonth As Integer : Dim dtpTargetDate As Date

        Dim dblMonthTarget As Double : Dim dblProdPerc As Double
        Dim dblTARate As Double : Dim dblIncentiveAmt As Double

        Dim blnFound As Boolean : Dim intCounter As Integer
        Dim dblTotIncPcsFull As Double : Dim dblTotIncPcsSingle As Double
        Dim dblRateSingle As Double : Dim strIncUnit As String

        Dim dblPerCts As Double
        Dim dblSmallPcs As Double
        Dim dblBigUnits As Double
        Dim blnOriginalSec As Boolean

        intCounter = 0
        ExpProgress.Value = intCounter
        ExpProgress.Visible = True

        dtpStartDate = CDate(Format(dtpFromDate.Value, "yyyy/MM/dd"))
        dtpEndDate = CDate(Format(dtpToDate.Value, "yyyy/MM/dd"))

        strYear = Format(dtpStartDate, "yyyy")
        strMonth = Format(CDbl(Format(dtpStartDate, "MM")), "00")

        intDays = DateDiff("d", dtpStartDate, dtpEndDate) + 1

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM VW_EMP_HOLIDAYS WHERE HDate BETWEEN '" & Format(dtpStartDate, "MM/dd/yyyy") & "' AND '" & Format(dtpEndDate, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
        intHolidays = rsComSql_1.RecordCount
        rsComSql_1 = Nothing

        AdoCN.Execute("DELETE FROM tblRndIncentive")

        rsComSql_1 = New ADODB.Recordset
        mStrSQL = "SP_GETPRODEMP "
        mStrSQL = mStrSQL & "'" & Format(dtpStartDate, "MM/dd/yyyy") & "','" & Format(dtpEndDate, "MM/dd/yyyy") & "'"
        rsComSql_1.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            ExpProgress.Maximum = rsComSql_1.RecordCount
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                strEmpNo = rsComSql_1.Fields("EmpNo").Value
                intSec = rsComSql_1.Fields("Sec").Value

                dblTotUnitPcs = 0 : dblTotIncPcs = 0
                dblTotProdPcs = 0 : dblTotBroPcs = 0
                dblTotRejPcs = 0 : dblTotRepPcs = 0
                dblTotLostPcs = 0 : dblTotNoPayPcs = 0
                dblTotIncPcsFull = 0 : dblTotIncPcsSingle = 0

                dblActRgh = 0 : dblIssCts = 0 : dblRetCts = 0
                dblSmallPcs = 0
                dblBigUnits = 0
                blnFound = False
                blnOriginalSec = False

                strSection = ""
                strCategory = ""
                strGroup = ""
                strGrade = ""
                strFlow = ""

                'If strEmpNo = "T03331" Then
                '    MsgBox(strEmpNo)
                'End If

                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT * FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & strEmpNo & "'", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    dtpTargetDate = CDate(rsComSql_2.Fields("TargetDate").Value)
                    mSMonth = DateDiff(DateInterval.Month, dtpTargetDate, dtpStartDate) + 1
                    strSection = rsComSql_2.Fields("SECTION_DESC").Value
                    strGroup = rsComSql_2.Fields("GRP_DESC").Value
                    strGrade = rsComSql_2.Fields("GRADE").Value
                    strCategory = rsComSql_2.Fields("Category").Value
                End If
                rsComSql_2 = Nothing

                If Mid(UCase(strCategory), 1, 6) = "DIRECT" Or Mid(UCase(strCategory), 1, 4) = "TEMP" Then
                    blnFound = True
                Else
                    blnFound = False
                End If

                If blnFound = True Then
                    If mSMonth > 9 Then
                        If intSec = 18 Or intSec = 19 Or intSec = 20 Then
                            mSMonth = 10
                        Else
                            mSMonth = 9
                        End If
                    Else
                        If DateAndTime.Day(dtpTargetDate) > 15 Then
                            mSMonth = mSMonth - 2
                        Else
                            mSMonth = mSMonth - 1
                        End If
                    End If

                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT SUM(NoOfDays + NoPayDays) AS TotLeave FROM VW_EMP_LEAVE WHERE EmpNo = '" & strEmpNo & "' AND LDate >= '" & Format(dtpStartDate, "MM/dd/yyyy") & "' AND LDate <= '" & Format(dtpEndDate, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
                    dblLeave = IIf(Not IsDBNull(rsComSql_2.Fields("TotLeave").Value), rsComSql_2.Fields("TotLeave").Value, 0)
                    rsComSql_2 = Nothing

                    dblWorkDays = intDays - (intHolidays + dblLeave)

                    blnFound = False

                    'Calculate Units and Pcs - ROUNDS
                    rsComSql_2 = New ADODB.Recordset
                    mStrSQL = "SP_GETPRODRND "
                    mStrSQL = mStrSQL & "'" & Format(dtpStartDate, "MM/dd/yyyy") & "','" & Format(dtpEndDate, "MM/dd/yyyy") & "','" & strEmpNo & "','" & intSec & "'"
                    rsComSql_2.Open(mStrSQL, AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        rsComSql_2.MoveFirst()
                        While Not rsComSql_2.EOF
                            dblRate = 0
                            dblRateSingle = 0
                            strIncUnit = Trim(rsComSql_2.Fields("IncUnit").Value)
                            dblPerCts = rsComSql_2.Fields("Pktcts").Value / rsComSql_2.Fields("PktPcs").Value
                            dblPerCts = Math.Round(dblPerCts, 3)

                            rsComSql_3 = New ADODB.Recordset
                            rsComSql_3.Open("SELECT * FROM tblRndTgtUnits WHERE SecCode = '" & intSec & "' AND Type = '" & rsComSql_2.Fields("NewCat").Value & "'", AdoCN, 1, 1)
                            If rsComSql_3.RecordCount Then
                                blnFound = True
                                If Trim(UCase(rsComSql_3.Fields("SecName").Value)) = Trim(UCase(strSection)) Then
                                    blnOriginalSec = True
                                End If
                                dblRate = Math.Round(rsComSql_3.Fields("UnitP" & strIncUnit).Value, 2)
                                dblRate = Math.Round(dblRate, 2)
                                If rsComSql_3.Fields("UnitPA").Value <> 0 Then
                                    dblRateSingle = Math.Round(rsComSql_3.Fields("UnitPM").Value / rsComSql_3.Fields("UnitPA").Value, 2)
                                    dblRateSingle = Math.Round(dblRateSingle, 2)
                                End If
                                If dblPerCts > 1 Then
                                    dblRate = dblRate * dblPerCts
                                End If

                                strFlow = rsComSql_2.Fields("PktFlow").Value

                                dblUnitPcs = dblRate * (rsComSql_2.Fields("RetPcsT").Value + rsComSql_2.Fields("RetPcsB").Value - rsComSql_2.Fields("Nopay").Value)

                                dblTotProdPcs = dblTotProdPcs + (rsComSql_2.Fields("RetPcsT").Value + rsComSql_2.Fields("RetPcsB").Value)
                                dblTotIncPcs = dblTotIncPcs + (rsComSql_2.Fields("RetPcsT").Value + rsComSql_2.Fields("RetPcsB").Value - rsComSql_2.Fields("Nopay").Value)
                                dblTotIncPcsFull = dblTotIncPcsFull + rsComSql_2.Fields("RetPcsT").Value
                                dblTotIncPcsSingle = dblTotIncPcsSingle + rsComSql_2.Fields("RetPcsB").Value
                                dblTotUnitPcs = dblTotUnitPcs + dblUnitPcs

                                dblTotBroPcs = dblTotBroPcs + rsComSql_2.Fields("Bro").Value
                                dblTotRejPcs = dblTotRejPcs + rsComSql_2.Fields("Rej").Value
                                dblTotRepPcs = dblTotRepPcs + rsComSql_2.Fields("Repair").Value
                                dblTotLostPcs = dblTotLostPcs + rsComSql_2.Fields("Lost").Value
                                dblTotNoPayPcs = dblTotNoPayPcs + rsComSql_2.Fields("Nopay").Value

                                dblActRgh = dblActRgh + rsComSql_2.Fields("ActRough").Value
                                dblIssCts = dblIssCts + rsComSql_2.Fields("ActIss").Value
                                dblRetCts = dblRetCts + rsComSql_2.Fields("RetCts").Value

                                If strIncUnit = "D" Or strIncUnit = "E" Or strIncUnit = "F" Or strIncUnit = "G" _
                                    Or strIncUnit = "H" Or strIncUnit = "I" Or strIncUnit = "M" Or strIncUnit = "N" _
                                    Or strIncUnit = "O" Or strIncUnit = "P" Or strIncUnit = "Q" Or strIncUnit = "R" _
                                    Or strIncUnit = "AA" Or strIncUnit = "AB" Or strIncUnit = "AC" Or strIncUnit = "AD" _
                                    Or strIncUnit = "AE" Or strIncUnit = "AF" Or strIncUnit = "AG" Or strIncUnit = "AH" Then

                                    dblBigUnits = dblBigUnits + dblUnitPcs
                                Else
                                    dblSmallPcs = dblSmallPcs + (rsComSql_2.Fields("RetPcsT").Value + rsComSql_2.Fields("RetPcsB").Value - rsComSql_2.Fields("Nopay").Value)
                                End If

                            End If
                            rsComSql_3 = Nothing

                            rsComSql_2.MoveNext()
                        End While
                    End If
                    rsComSql_2 = Nothing

                End If

                If blnFound = True Then
                    'Daily Target Pcs
                    dblTargetP = 0
                    rsComSql_3 = New ADODB.Recordset
                    rsComSql_3.Open("SELECT * FROM tblRndTgtUnits WHERE SecCode = '" & intSec & "' AND MMonth = '" & mSMonth & "' AND Type = 'Rough'", AdoCN, 1, 1)
                    If rsComSql_3.RecordCount Then
                        dblTargetP = rsComSql_3.Fields("TargetP").Value
                    End If
                    rsComSql_3 = Nothing

                    'Monthly Target Percentage
                    dblMonthTarget = dblTargetP * dblWorkDays
                    If dblMonthTarget > 0 Then
                        dblProdPerc = Math.Round((dblSmallPcs + dblBigUnits) * 100 / dblMonthTarget, 2)
                    Else
                        dblProdPerc = 100
                    End If

                    'Target Achievement Percentage
                    dblTARate = 0
                    rsComSql_3 = New ADODB.Recordset
                    If mSMonth > 0 Then
                        rsComSql_3.Open("SELECT * FROM tblRndTgtUnits WHERE SecCode = '" & intSec & "' AND MMonth = '" & mSMonth & "' AND ProdFrom <= '" & dblProdPerc & "' AND ProdTo >= '" & dblProdPerc & "' AND Type = 'Rough'", AdoCN, 1, 1)
                    Else
                        rsComSql_3.Open("SELECT * FROM tblRndTgtUnits WHERE SecCode = '" & intSec & "' AND ProdFrom <= '" & dblProdPerc & "' AND ProdTo >= '" & dblProdPerc & "' AND Type = 'Rough'", AdoCN, 1, 1)
                    End If
                    If rsComSql_3.RecordCount Then
                        dblTARate = Math.Round(rsComSql_3.Fields("RateP").Value, 2)
                        dblTARate = Math.Round(dblTARate, 2)
                    End If
                    rsComSql_3 = Nothing

                    dblIncentiveAmt = Math.Round(dblTARate * dblTotUnitPcs, 2)

                    mStrSQL = "INSERT INTO tblRndIncentive(EmpNo,YEAR1,MONTH1,Workdays,Target,Units,ProdPcs,IncentivePcs,FullCutPcs,SingleCutPcs," & _
                                        "Reject,Broken,Lost,Repair,Nopay,Rate,Incentive,ActIncentive,Service,Sec,Grp,Grd,SecNo," & _
                                        "Leave,RghCts,IssCts,RetCts,Flow,Prod,Rate2,SmallPcs,BigUnits,OriginalSec,Department,NewIncentive,Category) " & _
                                  "VALUES('" & strEmpNo & "','" & strYear & "','" & strMonth & "'," & dblWorkDays & "," & dblMonthTarget & "," & _
                                        "" & dblTotUnitPcs & "," & dblTotProdPcs & "," & dblTotIncPcs & "," & dblTotIncPcsFull & "," & dblTotIncPcsSingle & "," & dblTotRejPcs & "," & _
                                        "" & dblTotBroPcs & "," & dblTotLostPcs & "," & dblTotRepPcs & "," & dblTotNoPayPcs & "," & _
                                        "" & dblTargetP & "," & dblIncentiveAmt & "," & dblIncentiveAmt & "," & mSMonth & ",'" & strSection & "'," & _
                                        "'" & strGroup & "','" & strGrade & "'," & intSec & "," & dblLeave & "," & dblActRgh & "," & dblIssCts & "," & _
                                        "" & dblRetCts & ",'" & strFlow & "'," & dblProdPerc & "," & dblTARate & "," & dblSmallPcs & "," & dblBigUnits & ",'" & blnOriginalSec & "','Rounds'," & dblIncentiveAmt & ",'Rough')"

                    AdoCN.Execute(mStrSQL)

                End If

                rsComSql_1.MoveNext()
                intCounter = intCounter + 1
                ExpProgress.Value = intCounter
                Application.DoEvents()
            End While
        End If
        rsComSql_1 = Nothing

        intCounter = 0
        ExpProgress.Value = intCounter

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT EmpNo, ROUND(SUM(SmallPcs + BigUnits), 2) AS TotPcs " & _
                        "FROM tblRndIncentive " & _
                        "WHERE Department = 'Rounds' " & _
                        "GROUP BY EmpNo " & _
                        "HAVING (COUNT(*) > 1) " & _
                        "ORDER BY EmpNo", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            ExpProgress.Maximum = rsComSql_1.RecordCount
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                'If rsComSql_1![EmpNo] = "D05610" Then
                '    MsgBoxGT rsComSql_1![EmpNo]
                'End If

                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT * FROM tblRndIncentive WHERE EmpNo = '" & rsComSql_1.Fields("EmpNo").Value & "' AND OriginalSec = 1 AND Department = 'Rounds'", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    If rsComSql_2.Fields("Target").Value > 0 Then
                        dblProdPerc = (rsComSql_1.Fields("TotPcs").Value * 100) / rsComSql_2.Fields("Target").Value
                        dblProdPerc = Math.Round(dblProdPerc, 2)
                    Else
                        dblProdPerc = 0
                    End If
                Else

                End If
                rsComSql_2 = Nothing

                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT * FROM tblRndIncentive WHERE EmpNo = '" & rsComSql_1.Fields("EmpNo").Value & "' AND Department = 'Rounds' ORDER BY SecNo", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    rsComSql_2.MoveFirst()
                    While Not rsComSql_2.EOF
                        rsComSql_3 = New ADODB.Recordset
                        rsComSql_3.Open("SELECT * FROM tblRndTgtUnits WHERE SecCode = '" & rsComSql_2.Fields("SecNo").Value & "' AND ProdFrom <= '" & dblProdPerc & "' AND ProdTo >= '" & dblProdPerc & "' AND Type = 'Rough'", AdoCN, 1, 1)
                        If rsComSql_3.RecordCount Then
                            AdoCN.Execute("UPDATE tblRndIncentive SET Rate3 = " & rsComSql_3.Fields("RateP").Value & " WHERE EmpNo = '" & rsComSql_1.Fields("EmpNo").Value & "' AND SecNo = '" & rsComSql_2.Fields("SecNo").Value & "' AND Department = 'Rounds'")
                            AdoCN.Execute("UPDATE tblRndIncentive SET NewIncentive = Units * Rate3, ProdNew = " & dblProdPerc & " WHERE EmpNo = '" & rsComSql_1.Fields("EmpNo").Value & "' AND SecNo = '" & rsComSql_2.Fields("SecNo").Value & "' AND Department = 'Rounds'")
                        End If
                        rsComSql_3 = Nothing

                        rsComSql_2.MoveNext()
                    End While
                End If
                rsComSql_2 = Nothing

                intCounter = intCounter + 1
                ExpProgress.Value = intCounter
                rsComSql_1.MoveNext()
            End While
        End If
        rsComSql_1 = Nothing

        intCounter = 0
        ExpProgress.Value = intCounter

        Dim dblTotalPcs As Double
        Dim dblProdTarget As Double
        Dim dblActPerc As Double
        Dim dblProdRate As Double
        Dim dblProdIncentive As Double

        intCounter = 0
        ExpProgress.Value = intCounter

        dblTotalPcs = 0
        dblProdPerc = 0
        dblProdTarget = 0
        dblActPerc = 0
        dblProdRate = 0
        dblProdIncentive = 0
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT TOP (100) PERCENT EmpNo, SUM(SmallPcs + BigUnits) AS ProdPcs " & _
                        "FROM tblRndIncentive " & _
                        "GROUP BY EmpNo " & _
                        "ORDER BY EmpNo", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            ExpProgress.Maximum = rsComSql_1.RecordCount
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                strEmpNo = rsComSql_1.Fields("EmpNo").Value
                dblProdPerc = 0
                dblProdTarget = 0
                dblActPerc = 0
                dblProdRate = 0
                dblProdIncentive = 0
                dblTotalPcs = rsComSql_1.Fields("ProdPcs").Value

                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT TOP (100) PERCENT EmpNo, SmallPcs + BigUnits AS ProdPcs, Target, Service, SecNo, Department, Category " & _
                                "FROM dbo.tblRndIncentive " & _
                                "WHERE (EmpNo = '" & strEmpNo & "') " & _
                                "ORDER BY Department, Category, SecNo", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    rsComSql_2.MoveFirst()
                    While Not rsComSql_2.EOF
                        If dblTotalPcs > 0 Then
                            dblProdPerc = (rsComSql_2.Fields("ProdPcs").Value / dblTotalPcs) * 100
                            dblProdTarget = rsComSql_2.Fields("Target").Value * dblProdPerc / 100
                            dblActPerc = 0
                            If dblProdTarget <> 0 Then
                                dblActPerc = (rsComSql_2.Fields("ProdPcs").Value / dblProdTarget) * 100
                                If dblActPerc < 100 Then
                                    dblActPerc = 100
                                End If
                            End If
                        End If


                        dblProdRate = 0
                        rsComSql_3 = New ADODB.Recordset
                        If rsComSql_2.Fields("department").Value = "Rounds" Then
                            rsComSql_3.Open("SELECT RateP FROM tblRndTgtUnits WHERE SecCode = '" & rsComSql_2.Fields("SecNo").Value & "' AND MMonth = '" & rsComSql_2.Fields("Service").Value & "' AND ProdFrom <= '" & dblActPerc & "' AND ProdTo >= '" & dblActPerc & "' AND Type = 'Rough'", AdoCN, 1, 1)
                        Else
                            rsComSql_3.Open("SELECT RateP FROM tblExtTgtUnits WHERE SecCode = '" & rsComSql_2.Fields("SecNo").Value & "' AND MMonth = '" & rsComSql_2.Fields("Service").Value & "' AND ProdFrom <= '" & dblActPerc & "' AND ProdTo >= '" & dblActPerc & "' AND Type = '" & rsComSql_2.Fields("Category").Value & "'", AdoCN, 1, 1)
                        End If
                        If rsComSql_3.RecordCount Then
                            dblProdRate = rsComSql_3.Fields("RateP").Value
                        End If
                        rsComSql_3 = Nothing

                        dblProdIncentive = rsComSql_2.Fields("ProdPcs").Value * dblProdRate

                        AdoCN.Execute("UPDATE tblRndIncentive SET ProdPerc = '" & dblProdPerc & "',ProdTarget = '" & dblProdTarget & "',ActPerc = '" & dblActPerc & "',ProdRate = '" & dblProdRate & "',ProdIncentive = '" & dblProdIncentive & "' " & _
                                      "WHERE EmpNo = '" & strEmpNo & "' AND Department = '" & rsComSql_2.Fields("department").Value & "' AND Category = '" & rsComSql_2.Fields("Category").Value & "' AND SecNo = '" & rsComSql_2.Fields("SecNo").Value & "'")

                        rsComSql_2.MoveNext()
                    End While
                End If
                rsComSql_2 = Nothing

                intCounter = intCounter + 1
                ExpProgress.Value = intCounter
                rsComSql_1.MoveNext()
            End While
        End If
        rsComSql_1 = Nothing

        MsgBox("Rounds Incentive Process Completed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        ExpProgress.Value = 0
        ExpProgress.Visible = False

    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        GetIncentive()
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRndIncentiveDeptWise.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRndIncentiveAllEmp.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRndIncentiveEmpWise.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub GetNewIncentive()
        'On Error GoTo ErrorHandler
        Dim dtpStartDate As Date
        Dim dtpEndDate As Date
        Dim intIndex As Integer
        Dim intMonthDays As Integer
        Dim strEmpNo As String
        Dim strEmpCategory As String
        Dim strCategory As String
        Dim strSection As String
        Dim strGroup As String
        Dim strGrade As String
        Dim dblLeave As Double
        Dim intDays As Integer
        Dim intHolidays As Integer
        Dim dblWorkDays As Double

        Dim dtpTargetDate As Date
        Dim dtpProdDate As Date
        Dim dtpLastDate As Date
        Dim mSMonthOrg As Integer
        Dim mSMonth As Integer

        Dim blnFound As Boolean
        Dim intSec As Integer
        Dim intSecUnit As Integer

        Dim strIncUnit As String
        Dim dblRate As Double
        Dim dblUnitPcs As Double
        Dim dblPerCts As Double
        Dim dblSmallPcs As Double
        Dim dblBigUnits As Double
        Dim dblTotalUnits As Double
        Dim dblTotalPcs As Double
        Dim dblTargetP As Double
        Dim dblMonthTarget As Double
        Dim dblMainTarget As Double

        Dim intCounter As Integer
        Dim intCounter2 As Integer

        Dim dblSysTarget As Double
        Dim dblProdPerc As Double
        Dim dblProdTarget As Double
        Dim dblActPerc As Double
        Dim dblProdRate As Double
        Dim dblProdIncentive As Double

        Dim dblMins As Double
        Dim dblHours As Double

        AdoCN.Execute("DELETE FROM tblRndIncentive_Date")

        ExpProgress.Visible = True
        ExpProgress2.Visible = True
        intCounter = 0
        intCounter2 = 0
        ExpProgress.Value = intCounter
        ExpProgress2.Value = intCounter2

        dtpStartDate = CDate(Format(dtpFromDate.Value, "MM/dd/yyyy"))
        dtpEndDate = CDate(Format(dtpToDate.Value, "MM/dd/yyyy"))

        intDays = DateDiff("d", dtpStartDate, dtpEndDate) + 1

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM VW_EMP_HOLIDAYS WHERE HDate BETWEEN '" & Format(dtpStartDate, "MM/dd/yyyy") & "' AND '" & Format(dtpEndDate, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
        intHolidays = rsComSql_1.RecordCount
        rsComSql_1 = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT EmpNo " & _
                      "FROM dbo.VW_BothRndExtEmp " & _
                      "WHERE (RetDate >= '" & Format(dtpStartDate, "MM/dd/yyyy") & "') AND (RetDate <= '" & Format(dtpEndDate, "MM/dd/yyyy") & "') AND (LEN(EmpNo) > 0) " & _
                      "GROUP BY EmpNo " & _
                      "ORDER BY EmpNo", AdoCN, 1, 1)

        'rsComSql.Open("SELECT TOP (100) PERCENT EmpNo " & _
        '              "FROM dbo.VW_BothRndExtEmp " & _
        '              "WHERE (RetDate >= '" & Format(dtpStartDate, "MM/dd/yyyy") & "') AND (RetDate <= '" & Format(dtpEndDate, "MM/dd/yyyy") & "') AND (EmpNo = 'T09557') " & _
        '              "GROUP BY EmpNo " & _
        '              "ORDER BY EmpNo", AdoCN, 1, 1)

        'rsComSql.Open("SELECT TOP (100) PERCENT dbo.VW_BothRndExtEmp.EmpNo " & _
        '              "FROM dbo.VW_BothRndExtEmp INNER JOIN dbo.Comp6 ON dbo.VW_BothRndExtEmp.EmpNo = dbo.Comp6.ParNo " & _
        '              "WHERE (dbo.VW_BothRndExtEmp.RetDate >= '" & Format(dtpStartDate, "MM/dd/yyyy") & "') AND (dbo.VW_BothRndExtEmp.RetDate <= '" & Format(dtpEndDate, "MM/dd/yyyy") & "') AND (LEN(dbo.VW_BothRndExtEmp.EmpNo) > 0) " & _
        '              "GROUP BY dbo.VW_BothRndExtEmp.EmpNo " & _
        '              "ORDER BY dbo.VW_BothRndExtEmp.EmpNo", AdoCN, 1, 1)

        If rsComSql.RecordCount Then
            ExpProgress.Maximum = rsComSql.RecordCount
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                strEmpNo = rsComSql.Fields("EmpNo").Value

                'If strEmpNo = "E00602" Then
                '    MsgBox(strEmpNo)
                'End If

                strEmpCategory = ""
                strGrade = ""
                strGroup = ""
                strSection = ""
                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT * FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & strEmpNo & "'", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    dtpTargetDate = CDate(rsComSql_2.Fields("TargetDate").Value)
                    mSMonthOrg = DateDiff(DateInterval.Month, dtpTargetDate, dtpStartDate) + 1
                    strSection = rsComSql_2.Fields("SECTION_DESC").Value
                    strGroup = rsComSql_2.Fields("GRP_DESC").Value
                    strEmpCategory = rsComSql_2.Fields("Category").Value
                    strGrade = rsComSql_2.Fields("GRADE").Value
                End If
                rsComSql_2 = Nothing

                If Mid(UCase(strEmpCategory), 1, 6) = "DIRECT" Or Mid(UCase(strEmpCategory), 1, 4) = "TEMP" Then
                    blnFound = True
                Else
                    blnFound = False
                End If

                If blnFound = True Then
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT SUM(NoOfDays + NoPayDays) AS TotLeave FROM VW_EMP_LEAVE_ROUNDS WHERE EmpNo = '" & strEmpNo & "' AND LDate >= '" & Format(dtpStartDate, "MM/dd/yyyy") & "' AND LDate <= '" & Format(dtpEndDate, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
                    dblLeave = IIf(Not IsDBNull(rsComSql_2.Fields("TotLeave").Value), rsComSql_2.Fields("TotLeave").Value, 0)
                    rsComSql_2 = Nothing

                    dblWorkDays = intDays - (intHolidays + dblLeave)

                    intMonthDays = DateDiff("D", dtpStartDate, dtpEndDate)

                    intCounter2 = 0
                    ExpProgress2.Value = intCounter2
                    ExpProgress2.Maximum = intMonthDays + 1

                    For intIndex = 0 To intMonthDays
                        dtpProdDate = DateAdd(DateInterval.Day, intIndex, dtpStartDate)

                        'If dtpProdDate = CDate("05/08/2025") Then
                        '    MsgBox(dtpProdDate)
                        'End If

                        '*********************************************************
                        'ROUNDS
                        blnFound = False
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT TOP (100) PERCENT Sec " & _
                                        "FROM dbo.tblRndReturns " & _
                                        "WHERE (EmpNo = '" & strEmpNo & "') AND (RetDate = '" & Format(dtpProdDate, "MM/dd/yyyy") & "') AND (Sec <> 15) AND (Sec <> 16) AND (Sec <> 25) " & _
                                        "GROUP BY Sec ORDER BY Sec", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            rsComSql_1.MoveFirst()
                            While Not rsComSql_1.EOF
                                intSec = rsComSql_1.Fields("Sec").Value

                                mSMonth = mSMonthOrg
                                If mSMonth > 9 Then
                                    If intSec = 18 Or intSec = 19 Or intSec = 20 Then
                                        mSMonth = 10
                                    Else
                                        mSMonth = 9
                                    End If
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

                                rsComSql_5 = New ADODB.Recordset
                                rsComSql_5.Open("SELECT NewCat FROM tblRndTypes GROUP BY NewCat ORDER BY NewCat", AdoCN, 1, 1)
                                If rsComSql_5.RecordCount Then
                                    rsComSql_5.MoveFirst()
                                    While Not rsComSql_5.EOF
                                        strCategory = rsComSql_5.Fields("NewCat").Value

                                        dblBigUnits = 0
                                        dblSmallPcs = 0
                                        dblUnitPcs = 0
                                        dblTotalUnits = 0
                                        dblTotalPcs = 0
                                        rsComSql_2 = New ADODB.Recordset
                                        mStrSQL = "SP_GETPRODEMP_DATE "
                                        mStrSQL = mStrSQL & "'" & Format(dtpProdDate, "MM/dd/yyyy") & "','" & strEmpNo & "','" & intSec & "','" & strCategory & "'"
                                        rsComSql_2.Open(mStrSQL, AdoCN, 1, 1)
                                        If rsComSql_2.RecordCount Then
                                            rsComSql_2.MoveFirst()
                                            While Not rsComSql_2.EOF
                                                strIncUnit = Trim(rsComSql_2.Fields("IncUnit").Value)
                                                dblPerCts = rsComSql_2.Fields("Pktcts").Value / rsComSql_2.Fields("PktPcs").Value
                                                dblPerCts = Math.Round(dblPerCts, 3)

                                                rsComSql_3 = New ADODB.Recordset
                                                rsComSql_3.Open("SELECT * FROM tblRndTgtUnits WHERE SecCode = '" & intSec & "' AND Type = '" & strCategory & "'", AdoCN, 1, 1)
                                                If rsComSql_3.RecordCount Then
                                                    blnFound = True
                                                    dblRate = Math.Round(rsComSql_3.Fields("UnitP" & strIncUnit).Value, 2)
                                                    dblRate = Math.Round(dblRate, 2)

                                                    If dblPerCts > 1 Then
                                                        dblRate = dblRate * dblPerCts
                                                    End If

                                                    dblUnitPcs = dblRate * (rsComSql_2.Fields("RetPcsT").Value + rsComSql_2.Fields("RetPcsB").Value - rsComSql_2.Fields("NopayPcs").Value)
                                                    dblTotalUnits = dblTotalUnits + dblUnitPcs
                                                    dblTotalPcs = dblTotalPcs + (rsComSql_2.Fields("RetPcsT").Value + rsComSql_2.Fields("RetPcsB").Value - rsComSql_2.Fields("NopayPcs").Value)

                                                    If strIncUnit = "D" Or strIncUnit = "E" Or strIncUnit = "F" Or strIncUnit = "G" _
                                                        Or strIncUnit = "H" Or strIncUnit = "I" Or strIncUnit = "M" Or strIncUnit = "N" _
                                                        Or strIncUnit = "O" Or strIncUnit = "P" Or strIncUnit = "Q" Or strIncUnit = "R" _
                                                        Or strIncUnit = "AA" Or strIncUnit = "AB" Or strIncUnit = "AC" Or strIncUnit = "AD" _
                                                        Or strIncUnit = "AE" Or strIncUnit = "AF" Or strIncUnit = "AG" Or strIncUnit = "AH" _
                                                        Or strIncUnit = "RF" Or strIncUnit = "RG" Or strIncUnit = "RH" Or strIncUnit = "RI" _
                                                        Or strIncUnit = "RJ" Or strIncUnit = "RK" Or strIncUnit = "RL" Or strIncUnit = "RM" _
                                                        Or strIncUnit = "QA" Or strIncUnit = "QB" Then

                                                        If chkPcs.Checked = True Then
                                                            dblBigUnits = dblBigUnits + (rsComSql_2.Fields("RetPcsT").Value + rsComSql_2.Fields("RetPcsB").Value - rsComSql_2.Fields("NopayPcs").Value)
                                                        Else
                                                            dblBigUnits = dblBigUnits + dblUnitPcs
                                                        End If
                                                    Else
                                                        dblSmallPcs = dblSmallPcs + (rsComSql_2.Fields("RetPcsT").Value + rsComSql_2.Fields("RetPcsB").Value - rsComSql_2.Fields("NopayPcs").Value)
                                                    End If

                                                End If
                                                rsComSql_3 = Nothing

                                                rsComSql_2.MoveNext()
                                            End While
                                        End If
                                        rsComSql_2 = Nothing

                                        If blnFound = True Then
                                            dblTargetP = 0
                                            rsComSql_3 = New ADODB.Recordset
                                            rsComSql_3.Open("SELECT * FROM tblRndTgtUnits WHERE SecCode = '" & intSec & "' AND MMonth = '" & mSMonth & "' AND Type = '" & strCategory & "'", AdoCN, 1, 1)
                                            If rsComSql_3.RecordCount Then
                                                dblTargetP = rsComSql_3.Fields("TargetP").Value
                                                'dblTargetP = rsComSql_3.Fields("TargetPHour").Value
                                            End If
                                            rsComSql_3 = Nothing

                                            dblMainTarget = 0
                                            rsComSql_3 = New ADODB.Recordset
                                            rsComSql_3.Open("SELECT * FROM tblRndTgtUnits WHERE SecName = '" & strSection & "' AND MMonth = '" & mSMonth & "' AND Type = 'Rough'", AdoCN, 1, 1)
                                            If rsComSql_3.RecordCount Then
                                                dblMainTarget = rsComSql_3.Fields("TargetP").Value
                                                'dblMainTarget = rsComSql_3.Fields("TargetPHour").Value
                                            End If
                                            rsComSql_3 = Nothing

                                            dblMonthTarget = dblTargetP
                                            dtpLastDate = dtpProdDate

                                            AdoCN.Execute("INSERT INTO tblRndIncentive_Date(EmpNo,ProdDate,WorkDays,MainTarget,Target,Service,Grp,Sec,SecNo,Leave,SmallPcs,BigUnits,TotalUnits,TotalPcs,OriginalSec,Department,Category,Grade,ComputerName) " & _
                                                          "VALUES('" & strEmpNo & "','" & Format(dtpProdDate, "MM/dd/yyyy") & "','" & dblWorkDays & "','" & dblMainTarget & "','" & dblMonthTarget & "','" & mSMonth & "','" & strGroup & "'," & _
                                                            "'" & strSection & "','" & intSec & "','" & dblLeave & "','" & dblSmallPcs & "','" & dblBigUnits & "','" & dblTotalUnits & "','" & dblTotalPcs & "',1,'Rounds','" & strCategory & "','" & strGrade & "','" & PBCompName & "')")

                                        End If

                                        rsComSql_5.MoveNext()
                                    End While
                                End If
                                rsComSql_5 = Nothing

                                rsComSql_1.MoveNext()
                            End While
                        End If
                        rsComSql_1 = Nothing
                        '*********************************************************


                        ''*********************************************************
                        ''ROUNDS PCU
                        blnFound = False
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT TOP (100) PERCENT Sec " & _
                                        "FROM dbo.tblReturns " & _
                                        "WHERE (EmpNo = '" & strEmpNo & "') AND (RetDate = '" & Format(dtpProdDate, "MM/dd/yyyy") & "') AND (Sec <> 15) " & _
                                        "GROUP BY Sec ORDER BY Sec", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            rsComSql_1.MoveFirst()
                            While Not rsComSql_1.EOF
                                intSec = rsComSql_1.Fields("Sec").Value

                                mSMonth = mSMonthOrg
                                If mSMonth > 9 Then
                                    If intSec = 18 Or intSec = 19 Or intSec = 20 Then
                                        mSMonth = 10
                                    Else
                                        mSMonth = 9
                                    End If
                                Else
                                    If mSMonth > 1 Then
                                        If DateAndTime.Day(dtpTargetDate) > 15 Then
                                            mSMonth = mSMonth - 2
                                        Else
                                            mSMonth = mSMonth - 1
                                        End If
                                    End If
                                End If

                                strCategory = "Rough"

                                dblBigUnits = 0
                                dblSmallPcs = 0
                                dblUnitPcs = 0
                                dblTotalUnits = 0
                                dblTotalPcs = 0
                                intSecUnit = intSec

                                rsComSql_2 = New ADODB.Recordset
                                mStrSQL = "SP_GETPRODEMP_DATE_PCURND "
                                mStrSQL = mStrSQL & "'" & Format(dtpProdDate, "MM/dd/yyyy") & "','" & strEmpNo & "','" & intSec & "'"
                                rsComSql_2.Open(mStrSQL, AdoCN, 1, 1)
                                If rsComSql_2.RecordCount Then
                                    rsComSql_2.MoveFirst()
                                    While Not rsComSql_2.EOF
                                        strIncUnit = Trim(rsComSql_2.Fields("IncUnit").Value)
                                        dblPerCts = rsComSql_2.Fields("Pktcts").Value / rsComSql_2.Fields("PktPcs").Value
                                        dblPerCts = Math.Round(dblPerCts, 3)

                                        If intSec = 16 Then
                                            intSecUnit = 16
                                        End If
                                        If intSec = 17 Then
                                            intSecUnit = 18
                                        End If
                                        If intSec = 18 Then
                                            intSecUnit = 23
                                        End If

                                        rsComSql_3 = New ADODB.Recordset
                                        rsComSql_3.Open("SELECT * FROM tblRndTgtUnits WHERE SecCode = '" & intSecUnit & "' AND Type = '" & strCategory & "'", AdoCN, 1, 1)
                                        If rsComSql_3.RecordCount Then
                                            blnFound = True
                                            dblRate = Math.Round(rsComSql_3.Fields("UnitP" & strIncUnit).Value, 2)
                                            dblRate = Math.Round(dblRate, 2)

                                            If dblPerCts > 1 Then
                                                dblRate = dblRate * dblPerCts
                                            End If

                                            dblUnitPcs = dblRate * (rsComSql_2.Fields("RetPcsT").Value + rsComSql_2.Fields("RetPcsB").Value - rsComSql_2.Fields("NopayPcs").Value)
                                            dblTotalUnits = dblTotalUnits + dblUnitPcs
                                            dblTotalPcs = dblTotalPcs + (rsComSql_2.Fields("RetPcsT").Value + rsComSql_2.Fields("RetPcsB").Value - rsComSql_2.Fields("NopayPcs").Value)

                                            If strIncUnit = "D" Or strIncUnit = "E" Or strIncUnit = "F" Or strIncUnit = "G" _
                                                Or strIncUnit = "H" Or strIncUnit = "I" Or strIncUnit = "M" Or strIncUnit = "N" _
                                                Or strIncUnit = "O" Or strIncUnit = "P" Or strIncUnit = "Q" Or strIncUnit = "R" _
                                                Or strIncUnit = "AA" Or strIncUnit = "AB" Or strIncUnit = "AC" Or strIncUnit = "AD" _
                                                Or strIncUnit = "AE" Or strIncUnit = "AF" Or strIncUnit = "AG" Or strIncUnit = "AH" _
                                                Or strIncUnit = "RF" Or strIncUnit = "RG" Or strIncUnit = "RH" Or strIncUnit = "RI" _
                                                Or strIncUnit = "RJ" Or strIncUnit = "RK" Or strIncUnit = "RL" Or strIncUnit = "RM" _
                                                Or strIncUnit = "QA" Or strIncUnit = "QB" Then

                                                If chkPcs.Checked = True Then
                                                    dblBigUnits = dblBigUnits + (rsComSql_2.Fields("RetPcsT").Value + rsComSql_2.Fields("RetPcsB").Value - rsComSql_2.Fields("NopayPcs").Value)
                                                Else
                                                    dblBigUnits = dblBigUnits + dblUnitPcs
                                                End If
                                            Else
                                                dblSmallPcs = dblSmallPcs + (rsComSql_2.Fields("RetPcsT").Value + rsComSql_2.Fields("RetPcsB").Value - rsComSql_2.Fields("NopayPcs").Value)
                                            End If

                                        End If
                                        rsComSql_3 = Nothing

                                        rsComSql_2.MoveNext()
                                    End While
                                End If
                                rsComSql_2 = Nothing

                                If blnFound = True Then
                                    dblTargetP = 0
                                    rsComSql_3 = New ADODB.Recordset
                                    rsComSql_3.Open("SELECT * FROM tblRndTgtUnits WHERE SecCode = '" & intSecUnit & "' AND MMonth = '" & mSMonth & "' AND Type = '" & strCategory & "'", AdoCN, 1, 1)
                                    If rsComSql_3.RecordCount Then
                                        dblTargetP = rsComSql_3.Fields("TargetP").Value
                                        'dblTargetP = rsComSql_3.Fields("TargetPHour").Value
                                    End If
                                    rsComSql_3 = Nothing

                                    dblMainTarget = 0
                                    rsComSql_3 = New ADODB.Recordset
                                    rsComSql_3.Open("SELECT * FROM tblRndTgtUnits WHERE SecName = '" & strSection & "' AND MMonth = '" & mSMonth & "' AND Type = 'Rough'", AdoCN, 1, 1)
                                    If rsComSql_3.RecordCount Then
                                        dblMainTarget = rsComSql_3.Fields("TargetP").Value
                                        'dblMainTarget = rsComSql_3.Fields("TargetPHour").Value
                                    End If
                                    rsComSql_3 = Nothing

                                    dblMonthTarget = dblTargetP
                                    dtpLastDate = dtpProdDate

                                    AdoCN.Execute("INSERT INTO tblRndIncentive_Date(EmpNo,ProdDate,WorkDays,MainTarget,Target,Service,Grp,Sec,SecNo,Leave,SmallPcs,BigUnits,TotalUnits,TotalPcs,OriginalSec,Department,Category,Grade,ComputerName) " & _
                                                  "VALUES('" & strEmpNo & "','" & Format(dtpProdDate, "MM/dd/yyyy") & "','" & dblWorkDays & "','" & dblMainTarget & "','" & dblMonthTarget & "','" & mSMonth & "','" & strGroup & "'," & _
                                                    "'" & strSection & "','" & intSecUnit & "','" & dblLeave & "','" & dblSmallPcs & "','" & dblBigUnits & "','" & dblTotalUnits & "','" & dblTotalPcs & "',1,'RoundsPCU','" & strCategory & "','" & strGrade & "','" & PBCompName & "')")
                                End If

                                rsComSql_1.MoveNext()
                            End While
                        End If
                        rsComSql_1 = Nothing

                        '*********************************************************

                        ExpProgress2.Value = intCounter2
                        intCounter2 = intCounter2 + 1

                        Application.DoEvents()
                    Next
                End If

                rsComSql.MoveNext()
                intCounter = intCounter + 1
                ExpProgress.Value = intCounter
                Application.DoEvents()
            End While
        End If
        rsComSql = Nothing

        intCounter = 0
        intCounter2 = 0
        ExpProgress.Value = 0
        ExpProgress2.Value = 0

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT EmpNo FROM dbo.tblRndIncentive_Date GROUP BY EmpNo ORDER BY EmpNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            ExpProgress.Maximum = rsComSql.RecordCount
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                strEmpNo = rsComSql.Fields("EmpNo").Value

                intCounter2 = 0
                ExpProgress2.Value = intCounter2
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT TOP (100) PERCENT Grade, ProdDate, ROUND(SUM(SmallPcs + BigUnits), 2) AS ProdPcs, SUM(TotalPcs) AS TotalPcs FROM dbo.tblRndIncentive_Date WHERE (EmpNo = '" & strEmpNo & "') GROUP BY Grade, ProdDate ORDER BY ProdDate", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    ExpProgress2.Maximum = rsComSql_1.RecordCount
                    rsComSql_1.MoveFirst()
                    While Not rsComSql_1.EOF
                        dtpProdDate = rsComSql_1.Fields("ProdDate").Value
                        If chkTA.Checked = True And (rsComSql_1.Fields("GRADE").Value = "D" Or rsComSql_1.Fields("GRADE").Value = "TA" Or rsComSql_1.Fields("GRADE").Value = "TC" Or rsComSql_1.Fields("GRADE").Value = "TRAINEE") Then
                            dblTotalPcs = rsComSql_1.Fields("totalPcs").Value
                        Else
                            dblTotalPcs = rsComSql_1.Fields("ProdPcs").Value
                        End If

                        'If dtpProdDate = "11/21/2016" Then
                        '    MsgBoxGT dtpProdDate
                        'End If

                        dblMins = 0
                        dblHours = 0
                        rsComSql_2 = New ADODB.Recordset
                        rsComSql_2.Open("SELECT TOP (100) PERCENT Service, Target, SecNo, SmallPcs + BigUnits AS ProdPcs, TotalUnits, Department, Category, TotalPcs, Grade " & _
                                        "FROM dbo.tblRndIncentive_Date " & _
                                        "WHERE (EmpNo = '" & strEmpNo & "') AND (ProdDate = '" & Format(dtpProdDate, "MM/dd/yyyy") & "') AND (SmallPcs + BigUnits > 0) " & _
                                        "ORDER BY SecNo", AdoCN, 1, 1)
                        If rsComSql_2.RecordCount Then
                            rsComSql_2.MoveFirst()
                            While Not rsComSql_2.EOF
                                dblSysTarget = rsComSql_2.Fields("Target").Value

                                ''Hourly Target
                                'rsComSql_3 = New ADODB.Recordset
                                'rsComSql_3.Open("SELECT * FROM VW_EMP_HOURS WHERE TDate = '" & dtpProdDate & "' AND FullEmpNo = '" & strEmpNo & "'", AdoCN, 1, 1)
                                'If rsComSql_3.RecordCount Then
                                '    'dblMins = rsComSql_3.Fields("Minutes").Value - rsComSql_3.Fields("SL").Value
                                '    dblMins = rsComSql_3.Fields("Minutes").Value - rsComSql_3.Fields("SL").Value
                                'End If
                                'rsComSql_3 = Nothing

                                'dblHours = Math.Floor((dblMins - 60) / 60)
                                'dblSysTarget = dblSysTarget * dblHours

                                If chkTA.Checked = True And (rsComSql_1.Fields("GRADE").Value = "D" Or rsComSql_1.Fields("GRADE").Value = "TA" Or rsComSql_1.Fields("GRADE").Value = "TC" Or rsComSql_1.Fields("GRADE").Value = "TRAINEE") Then
                                    dblProdPerc = ((rsComSql_2.Fields("totalPcs").Value) / dblTotalPcs) * 100
                                Else
                                    dblProdPerc = ((rsComSql_2.Fields("ProdPcs").Value) / dblTotalPcs) * 100
                                End If

                                If dblProdPerc < 0 Then
                                    dblProdPerc = 0
                                End If

                                dblProdTarget = dblSysTarget * dblProdPerc / 100
                                dblActPerc = 0
                                If dblProdTarget <> 0 Then
                                    If chkTA.Checked = True And (rsComSql_1.Fields("GRADE").Value = "D" Or rsComSql_1.Fields("GRADE").Value = "TA" Or rsComSql_1.Fields("GRADE").Value = "TC" Or rsComSql_1.Fields("GRADE").Value = "TRAINEE") Then
                                        dblActPerc = ((rsComSql_2.Fields("totalPcs").Value) / dblSysTarget) * 100
                                    Else
                                        dblActPerc = ((rsComSql_2.Fields("ProdPcs").Value) / dblSysTarget) * 100
                                    End If
                                    If dblActPerc < 100 Then
                                        dblActPerc = 100
                                    End If
                                End If

                                dblProdRate = 0
                                rsComSql_3 = New ADODB.Recordset
                                If rsComSql_2.Fields("department").Value = "Rounds" Or rsComSql_2.Fields("department").Value = "RoundsPCU" Then
                                    rsComSql_3.Open("SELECT RateP FROM tblRndTgtUnits WHERE SecCode = '" & rsComSql_2.Fields("SecNo").Value & "' AND MMonth = '" & rsComSql_2.Fields("Service").Value & "' AND ProdFrom <= '" & dblActPerc & "' AND ProdTo >= '" & dblActPerc & "' AND Type = '" & rsComSql_2.Fields("Category").Value & "'", AdoCN, 1, 1)
                                Else
                                    rsComSql_3.Open("SELECT RateP FROM tblExtTgtUnits WHERE SecCode = '" & rsComSql_2.Fields("SecNo").Value & "' AND MMonth = '" & rsComSql_2.Fields("Service").Value & "' AND ProdFrom <= '" & dblActPerc & "' AND ProdTo >= '" & dblActPerc & "' AND Type = '" & rsComSql_2.Fields("Category").Value & "'", AdoCN, 1, 1)
                                End If
                                If rsComSql_3.RecordCount Then
                                    dblProdRate = rsComSql_3.Fields("RateP").Value
                                End If
                                rsComSql_3 = Nothing

                                dblProdIncentive = rsComSql_2.Fields("TotalUnits").Value * dblProdRate
                                'dblProdIncentive = (rsComSql_2.Fields("TotalUnits").Value - dblSysTarget) * dblProdRate
                                'If dblProdIncentive < 0 Then
                                '    dblProdIncentive = 0
                                'End If

                                AdoCN.Execute("UPDATE tblRndIncentive_Date SET ProdPerc = '" & dblProdPerc & "',ProdTarget = '" & dblProdTarget & "',ActPerc = '" & dblActPerc & "',ProdRate = '" & dblProdRate & "',ProdIncentive = '" & dblProdIncentive & "', Hours = '" & dblHours & "' " & _
                                              "WHERE EmpNo = '" & strEmpNo & "' AND Department = '" & rsComSql_2.Fields("department").Value & "' AND Category = '" & rsComSql_2.Fields("Category").Value & "' AND SecNo = '" & rsComSql_2.Fields("SecNo").Value & "' AND ProdDate = '" & Format(dtpProdDate, "MM/dd/yyyy") & "'")

                                rsComSql_2.MoveNext()
                            End While
                        End If
                        rsComSql_2 = Nothing

                        rsComSql_1.MoveNext()
                        ExpProgress2.Value = intCounter2
                        intCounter2 = intCounter2 + 1

                        Application.DoEvents()
                    End While
                End If
                rsComSql_1 = Nothing

                rsComSql.MoveNext()
                ExpProgress.Value = intCounter
                intCounter = intCounter + 1

                Application.DoEvents()
            End While
        End If
        rsComSql = Nothing

        MsgBox("New Rounds Incentive Process Finished", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        ExpProgress.Value = 0
        ExpProgress2.Value = 0

        ExpProgress.Visible = False
        ExpProgress2.Visible = False

        Exit Sub
ErrorHandler:
        MsgBox(Err.Description & vbCrLf & strEmpNo, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
    End Sub

    Private Sub chkInc_CheckedChanged(sender As Object) Handles chkInc.CheckedChanged
        Dim Instring As String

        If chkInc.Checked = True Then
            Instring = UCase(InputBox("Enter Access Code", "Authorized Password"))
            If Instring = "CHA19810724" Then
                fraIncentive.Visible = True
            Else
                chkInc.Checked = False
            End If
        End If

        If chkInc.Checked = False Then
            fraIncentive.Visible = False
        End If
    End Sub

    Private Sub HazelDev_Button5_Click(sender As Object, e As EventArgs) Handles HazelDev_Button5.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRndIncentiveDate.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button4_Click(sender As Object, e As EventArgs) Handles HazelDev_Button4.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRndIncentiveDateDetails.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub cmdSave2_Click(sender As Object, e As EventArgs) Handles cmdSave2.Click
        GetNewIncentive()
    End Sub
End Class