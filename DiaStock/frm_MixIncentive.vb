
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixIncentive
    Dim TotTarget, mDays, wDays As Single
    Dim strFolderPath As String

    Private Sub frm_MixIncentive_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        dtpFromDate.Value = dhFirstDayInMonth(Date.Now)
        dtpToDate.Value = Date.Now

        If strDBName = "DiaStock" Then
            strFolderPath = "Mix\"
        Else
            strFolderPath = "DiaSalesMix\"
        End If

        'If PBUser_EmpNo = "D06975" Then
        '    cmdHours.Visible = True
        'Else
        '    cmdHours.Visible = False
        'End If
    End Sub

    Private Sub GetIncentive()
        'On Error GoTo resume next
        Dim rstLev As ADODB.Recordset
        Dim rstRate As ADODB.Recordset
        Dim rstHly As ADODB.Recordset
        Dim rstGet As ADODB.Recordset
        Dim rstmasnew As ADODB.Recordset
        Dim rstTarget As ADODB.Recordset
        'Dim rstTgtQlty As ADODB.Recordset
        Dim mSec, mGrp, mgrd, mEmpNo, mUnit As String
        Dim mProd, mQltyP, mInRate As Single
        Dim xTgt, mSMonth, mSecNo As Integer
        Dim recEMP As Integer
        Dim mOvrTgt, mLv, nUnit, mRghcts, mRetCts, mIssCts As Single
        Dim TotRepair, TotNopay, TotUnits, RUnits, XUnits, TotReturns, TotRejects, TotBroken, TotLost, mQuality, TotNopay1 As Single
        Dim mIncentive, mRate, mActIncen, mSecRate, RIncentive As Double
        Dim mFlow As String
        Dim dblPayUnits As Single

        Dim dblPcs As Single
        Dim dblUnits As Single
        Dim dblTotPcs As Single
        Dim dblSecTarget As Single
        Dim dblTotTarget As Single
        Dim dblProdPerc As Single
        Dim intSec As Integer

        Dim dblMins As Single
        Dim dblHours As Single
        Dim dblAbsent As Single

        ExpProgress.Value = 0
        ExpProgress.Visible = True

        If dtpFromDate.Value <= dtpToDate.Value Then
            If dtpToDate.Value <= Date.Now Then
                'Start calculating Incentive

                AdoCN.Execute("DELETE FROM tblMIXIncentive")
                AdoCN.Execute("DELETE FROM tblMIXIncentive_Details")

                wDays = DateDiff(DateInterval.Day, dtpFromDate.Value, dtpToDate.Value) + 1

                AdoCN.Execute("DELETE FROM tblEmployee")
                AdoCN.Execute("INSERT INTO tblEmployee(Emp_No,Initials,Name,SurName,DOJ,Pay,Staff,TargetDate,SSection,GGroup,Grade,Department,Category) " & _
                              "SELECT FullEmpNo,INITIALS,NAME,SURNAME,DATE_JOINED,Pay,PROCESS_TYPE,TargetDate,SECTION_DESC,GRP_DESC,GRADE,DepartmentName,CATEGORY " & _
                              "FROM VW_EMP_MASTER")

                'Get Holiday information
                rstHly = New ADODB.Recordset
                rstHly.Open("SELECT * FROM VW_EMP_HOLIDAYS WHERE HDate BETWEEN '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "' AND '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
                wDays = wDays - (rstHly.RecordCount)

                GetLeaves()

                'Get production information of employees.

                rstGet = New ADODB.Recordset
                'mStrSQL = "SP_GETPRODMIX "
                mStrSQL = "SP_GETPRODMIX_UNION "
                mStrSQL = mStrSQL & "'" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "','" & Format(dtpToDate.Value, "MM/dd/yyyy") & "'"
                rstGet.Open(mStrSQL, AdoCN, 1, 1)

                ExpProgress.Maximum = rstGet.RecordCount

                If rstGet.RecordCount = 0 Then
                    MsgBox("No Records Found")
                    Exit Sub
                Else
                    rstGet.MoveFirst()
                    Do
                        mLv = 0
                        mEmpNo = Trim(rstGet.Fields("EmpNo").Value)
                        'If mEmpNo = "D09040" Then
                        '    MsgBox(mEmpNo)
                        'End If
                        rstLev = New ADODB.Recordset
                        rstLev.Open("SELECT EmpNo, SUM(Leave) AS Leaves, SUM(DLeave) AS DLeaves FROM tblPCULeaveCount GROUP BY EmpNo HAVING EmpNo = '" & mEmpNo & "'", AdoCN, 1, 1)

                        If rstLev.RecordCount > 0 Then
                            mLv = (rstLev.Fields("Leaves").Value + rstLev.Fields("DLeaves").Value)
                            mDays = wDays - mLv
                        Else
                            mDays = wDays
                        End If
                        dblAbsent = wDays - (mDays + mLv)

                        'Initialze variables

                        TotUnits = 0
                        TotTarget = 0
                        TotReturns = 0
                        TotRejects = 0
                        TotBroken = 0
                        TotLost = 0
                        TotNopay = 0
                        TotNopay1 = 0
                        mQuality = 0
                        TotRepair = 0
                        mRate = 0
                        mIncentive = 0
                        mRghcts = 0
                        mRetCts = 0
                        mIssCts = 0
                        mFlow = ""
                        mProd = 0
                        mQltyP = 0
                        mInRate = 0
                        mSec = 0
                        mGrp = ""
                        mgrd = ""
                        dblPayUnits = 0
                        dblPcs = 0
                        dblUnits = 0
                        dblTotPcs = 0
                        dblSecTarget = 0
                        dblProdPerc = 0
                        dblTotTarget = 0

                        rstmasnew = New ADODB.Recordset
                        rstmasnew.Open("SELECT * FROM tblEmployee WHERE Emp_No = '" & mEmpNo & "'", AdoCN, 1, 1)

                        'Temporary testing
                        Dim xx As Object
                        xx = rstmasnew.RecordCount

                        'Calculate Target Month
                        If xx > 0 Then
                            mSMonth = DateDiff("M", rstmasnew.Fields("TargetDate").Value, Format(dtpFromDate.Value, "MM/dd/yyyy")) + 1
                        End If

                        If xx > 0 Then
                            If mSMonth >= 8 Then
                                mSMonth = 7
                            Else
                                If mSMonth > 1 Then
                                    If DateAndTime.Day(rstmasnew.Fields("TargetDate").Value) > 15 Then
                                        mSMonth = mSMonth - 2
                                    Else
                                        mSMonth = mSMonth - 1
                                    End If
                                End If
                            End If
                        End If

                        If xx > 0 Then
                            mgrd = rstmasnew.Fields("Grade").Value
                            mGrp = rstmasnew.Fields("GGroup").Value

                            'Select actual section of the employees
                            mSec = rstmasnew.Fields("SSection").Value

                            mSecNo = rstGet.Fields("Sec").Value
                            'mFlow = rstGet.Fields("ParFlow").Value
                            mFlow = ""
                        End If

                        'Select the target
                        rstTarget = New ADODB.Recordset
                        If chkNew.Checked = False Then
                            rstTarget.Open("SELECT * FROM tblPCUTgtUnitsNew WHERE SecCode = '" & rstGet.Fields("Sec").Value & "' AND MMonth = '" & mSMonth & "'", AdoCN, 1, 1)
                        Else
                            rstTarget.Open("SELECT * FROM tblPCUTgtUnitsNew WHERE SecCode = '" & rstGet.Fields("Sec").Value & "' AND MMonth = '" & mSMonth & "'", AdoCN, 1, 1)
                        End If

                        'If chkNew.Checked = False Then
                        '    rstTarget.Open("SELECT * FROM tblPCUTgtUnitsNew WHERE MasterSec = '" & mSec & "' AND MMonth = '" & mSMonth & "'", AdoCN, 1, 1)
                        'Else
                        '    rstTarget.Open("SELECT * FROM tblPCUTgtUnitsNew WHERE MasterSec = '" & mSec & "' AND MMonth = '" & mSMonth & "'", AdoCN, 1, 1)
                        'End If
                        xTgt = 0
                        If rstTarget.RecordCount Then
                            'xTgt = (rstTarget.Fields("TargetP").Value * IIf(mDays >= 0, mDays, 0))

                            'Hourly Target
                            dblMins = 0
                            dblHours = 0
                            rsComSql_4 = New ADODB.Recordset
                            'rsComSql_4.Open("SELECT SUM(Minutes) AS Minutes FROM VW_EMP_HOURS WHERE TDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "' AND TDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "' AND FullEmpNo = '" & mEmpNo & "'", AdoCN, 1, 1)
                            rsComSql_4.Open("SELECT SUM(Minutes) AS Minutes FROM VW_EMP_HOURS WHERE YEAR(TDate) = '" & dtpFromDate.Value.Year & "' AND MONTH(TDate) = '" & dtpFromDate.Value.Month & "' AND FullEmpNo = '" & mEmpNo & "'", AdoCN, 1, 1)
                            If rsComSql_4.RecordCount Then
                                If Not IsDBNull(rsComSql_4.Fields("Minutes").Value) Then
                                    dblMins = rsComSql_4.Fields("Minutes").Value
                                End If
                            End If
                            rsComSql_4 = Nothing

                            If dblMins > 0 Then
                                dblHours = Math.Floor((dblMins - (mDays * 60)) / 60)
                                dblHours = dblHours + (dblAbsent * 8)

                                If dblHours < 208 Then
                                    dblHours = 208
                                End If
                            End If

                        End If

                        'Calculating no of Units
                        Do
                            rstTarget = New ADODB.Recordset
                            rstTarget.Open("SELECT * FROM tblPCUTgtUnitsNew WHERE SecCode = '" & rstGet.Fields("Sec").Value & "' AND MMonth = '" & mSMonth & "'", AdoCN, 1, 1)

                            mUnit = "Unit" + rstGet.Fields("ParUnit").Value
                            If rstTarget.RecordCount Then
                                nUnit = rstTarget.Fields(mUnit).Value
                                mRate = rstTarget.Fields("RateP").Value

                                xTgt = rstTarget.Fields("TargetPHour").Value * dblHours

                                dblUnits = (((rstGet.Fields("RetPcsT").Value + rstGet.Fields("RetPcsB").Value) - rstGet.Fields("Nopay").Value - rstGet.Fields("Nopay1").Value) * CSng(nUnit))
                                TotUnits = TotUnits + (((rstGet.Fields("RetPcsT").Value + rstGet.Fields("RetPcsB").Value) - rstGet.Fields("Nopay").Value - rstGet.Fields("Nopay1").Value) * CSng(nUnit))

                                dblPcs = ((rstGet.Fields("RetPcsT").Value + rstGet.Fields("RetPcsB").Value) - rstGet.Fields("Nopay").Value - rstGet.Fields("Nopay1").Value)
                                TotReturns = TotReturns + ((rstGet.Fields("RetPcsT").Value + rstGet.Fields("RetPcsB").Value))

                                'TotRejects = TotRejects + rstGet.Fields("Rej").Value
                                TotRejects = TotRejects + 0

                                'TotBroken = TotBroken + rstGet.Fields("Bro").Value
                                TotBroken = TotBroken + 0

                                'TotLost = TotLost + rstGet.Fields("Lost").Value
                                TotLost = TotLost + 0

                                'TotRepair = TotRepair + rstGet.Fields("Repair").Value
                                TotRepair = TotRepair + 0

                                TotNopay = TotNopay + rstGet.Fields("Nopay").Value
                                TotNopay1 = TotNopay1 + rstGet.Fields("Nopay1").Value

                                'mIssCts = mIssCts + rstGet.Fields("ActIss").Value
                                mIssCts = mIssCts + 0

                                'mRetCts = mRetCts + rstGet.Fields("RetCts").Value
                                mRetCts = mRetCts + 0

                                intSec = rstGet.Fields("Sec").Value
                                Select Case intSec
                                    Case 10
                                        intSec = 3
                                        'Case 11
                                        '    intSec = 5
                                        'Case 12
                                        '    intSec = 6
                                        'Case 13
                                        '    intSec = 7
                                    Case Else

                                End Select

                                AdoCN.Execute("INSERT INTO tblMIXIncentive_Details(EmpNo, Target, Units, ProdPcs, Nopay, Rate, Sec, Sec2, SecNo, Nopay1) " & _
                                              "VALUES('" & mEmpNo & "','" & xTgt & "','" & dblUnits & "','" & dblPcs & "','" & rstGet.Fields("Nopay").Value & "','" & rstTarget.Fields("RateP").Value & "','" & rstGet.Fields("Sec").Value & "','" & intSec & "','" & mSecNo & "','" & rstGet.Fields("Nopay1").Value & "')")
                            End If
                            rstGet.MoveNext()
                            ExpProgress.Value = ExpProgress.Value + 1

                            If rstGet.EOF Then Exit Do
                        Loop Until rstGet.EOF Or Trim(rstGet.Fields("EmpNo").Value) <> mEmpNo

                        'If TotReturns > 0 Then
                        '    mQltyP = ((TotReturns - (TotRepair + TotNopay)) / TotReturns) * 100
                        '    mOvrTgt = Int((TotUnits - (rstTarget.Fields("TargetP") * (IIf(mDays >= 0, mDays, 0)))))
                        'End If

                        rstRate = New ADODB.Recordset
                        'mStrSQL = "SP_GETPRODMIXSECEMP "
                        mStrSQL = "SP_GETPRODMIXSECEMP_UNION "
                        mStrSQL = mStrSQL & "'" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "','" & Format(dtpToDate.Value, "MM/dd/yyyy") & "','" & mEmpNo & "'"
                        rstRate.Open(mStrSQL, AdoCN, 1, 1)

                        recEMP = rstRate.RecordCount

                        rstTarget.Close()
                        RUnits = 0
                        RIncentive = 0
                        XUnits = 0
                        dblPayUnits = 0
                        dblTotPcs = 0

                        If rstRate.RecordCount Then
                            rstRate.MoveFirst()
                            Do
                                rstTarget = New ADODB.Recordset
                                If chkNew.Checked = False Then
                                    rstTarget.Open("SELECT * FROM tblPCUTgtUnitsNew WHERE SecCode = '" & rstRate.Fields("Sec").Value & "' And MMonth = '" & mSMonth & "'", AdoCN, 1, 1)
                                Else
                                    rstTarget.Open("SELECT * FROM tblPCUTgtUnitsNew WHERE SecCode = '" & rstRate.Fields("Sec").Value & "' And MMonth = '" & mSMonth & "'", AdoCN, 1, 1)
                                End If
                                If rstTarget.RecordCount Then
                                    mSecRate = rstTarget.Fields("RateP").Value

                                    mUnit = "Unit" + rstRate.Fields("ParUnit").Value
                                    nUnit = 0
                                    nUnit = rstTarget.Fields(mUnit).Value
                                    RUnits = (((rstRate.Fields("RetPcsT").Value + rstRate.Fields("RetPcsB").Value) - rstRate.Fields("Nopay").Value - rstRate.Fields("Nopay1").Value) * CSng(nUnit))
                                    XUnits = XUnits + RUnits

                                    RIncentive = (RUnits * mSecRate)

                                    mIncentive = mIncentive + RIncentive
                                End If
                                rstRate.MoveNext()
                                If rstRate.EOF Then Exit Do
                            Loop Until Trim(rstRate.Fields("EmpNo").Value) <> mEmpNo Or rstRate.RecordCount <> recEMP
                        End If
                        If xTgt > 0 Then
                            mProd = (XUnits / xTgt) * 100
                        End If

                        mActIncen = mIncentive
                        dblTotTarget = 0
                        '--------------------------------------------------------------------------------------------------
                        'Calculate Incentive amounts
                        rsComSql_4 = New ADODB.Recordset
                        rsComSql_4.Open("SELECT Target, SUM(Units) AS Units, SUM(ProdPcs) AS ProdPcs, SUM(Nopay) AS Nopay, Rate, Sec, SUM(Nopay1) AS Nopay1 " & _
                                        "FROM tblMIXIncentive_Details " & _
                                        "WHERE (EmpNo = '" & mEmpNo & "') " & _
                                        "GROUP BY Target, Rate, Sec ORDER BY Sec", AdoCN, 1, 1)
                        If rsComSql_4.RecordCount = 1 Then
                            dblTotTarget = xTgt
                            If rsComSql_4.Fields("ProdPcs").Value > xTgt Then
                                mActIncen = mIncentive
                            Else
                                mActIncen = (rsComSql_4.Fields("Units").Value - xTgt) * rsComSql_4.Fields("Rate").Value
                                If mActIncen < 0 Then
                                    mActIncen = 0
                                End If
                            End If
                        Else
                            If rsComSql_4.RecordCount > 1 Then
                                dblTotPcs = 0
                                rsComSql_5 = New ADODB.Recordset
                                rsComSql_5.Open("SELECT SUM(ProdPcs) AS ProdPcs " & _
                                                "FROM tblMIXIncentive_Details " & _
                                                "WHERE (EmpNo = '" & mEmpNo & "')", AdoCN, 1, 1)
                                If Not IsDBNull(rsComSql_5.Fields("ProdPcs").Value) Then
                                    dblTotPcs = rsComSql_5.Fields("ProdPcs").Value
                                End If
                                rsComSql_5 = Nothing

                                dblSecTarget = 0
                                mActIncen = 0
                                dblTotTarget = 0
                                rsComSql_5 = New ADODB.Recordset
                                rsComSql_5.Open("SELECT SUM(Units) AS Units, SUM(ProdPcs) AS ProdPcs, SUM(Nopay) AS Nopay, Sec2, SUM(Nopay1) AS Nopay1 " & _
                                                "FROM tblMIXIncentive_Details " & _
                                                "WHERE (EmpNo = '" & mEmpNo & "') " & _
                                                "GROUP BY Sec2 ORDER BY Sec2", AdoCN, 1, 1)
                                If rsComSql_5.RecordCount Then
                                    rsComSql_5.MoveFirst()
                                    While Not rsComSql_5.EOF
                                        dblProdPerc = rsComSql_5.Fields("ProdPcs").Value / dblTotPcs * 100
                                        rsComSql_6 = New ADODB.Recordset
                                        rsComSql_6.Open("SELECT Perc1 FROM tblMIXIncentive_Target WHERE From1 <= '" & dblProdPerc & "' AND To1 > '" & dblProdPerc & "' ", AdoCN, 1, 1)
                                        If rsComSql_6.RecordCount Then
                                            rsComSql_1 = New ADODB.Recordset
                                            rsComSql_1.Open("SELECT Target, Rate, SUM(Units) AS Units, SUM(ProdPcs) AS ProdPcs, SUM(Nopay) AS Nopay, SUM(Nopay1) AS Nopay1 " & _
                                                            "FROM tblMIXIncentive_Details " & _
                                                            "WHERE (EmpNo = '" & mEmpNo & "') AND (Sec2 = '" & rsComSql_5.Fields("Sec2").Value & "') " & _
                                                            "GROUP BY Target, Rate", AdoCN, 1, 1)
                                            If rsComSql_1.RecordCount Then
                                                rsComSql_1.MoveFirst()
                                                dblSecTarget = rsComSql_1.Fields("Target").Value * dblProdPerc / 100 * rsComSql_6.Fields("Perc1").Value / 100
                                                dblTotTarget = dblTotTarget + dblSecTarget
                                                mActIncen = mActIncen + (rsComSql_1.Fields("Rate").Value * (rsComSql_1.Fields("Units").Value - dblSecTarget))
                                            End If
                                            rsComSql_1 = Nothing

                                            
                                        End If
                                        rsComSql_6 = Nothing

                                        rsComSql_5.MoveNext()
                                    End While
                                End If
                                rsComSql_5 = Nothing

                                If dblTotPcs >= dblTotTarget Then
                                    mActIncen = mIncentive
                                End If

                                'If dblTotPcs > dblSecTarget Then
                                '    mActIncen = mIncentive
                                'Else
                                '    mActIncen = 0
                                '    rsComSql_4.MoveFirst()
                                '    While Not rsComSql_4.EOF
                                '        dblSecTarget = 0

                                '        'mActIncen = mActIncen - (rsComSql_4.Fields("ProdPcs").Value / dblTotPcs) * rsComSql_4.Fields("Target").Value * rsComSql_4.Fields("Rate").Value

                                '        dblSecTarget = (rsComSql_4.Fields("ProdPcs").Value / dblTotPcs) * rsComSql_4.Fields("Target").Value
                                '        mActIncen = mActIncen + ((rsComSql_4.Fields("Units").Value - dblSecTarget) * rsComSql_4.Fields("Rate").Value)

                                '        rsComSql_4.MoveNext()
                                '    End While
                                'End If
                            End If

                        End If
                        rsComSql_4 = Nothing

                        'If chkNew.Checked = False Then
                        '    rstTgtQlty = New ADODB.Recordset
                        '    rstTgtQlty.Open("SELECT * FROM tblTgtQlty WHERE TgtFrom <= " & mProd & " AND TgtTo >= " & mProd & " AND QltyFrom <= " & mQltyP & " AND QltyTo >= " & mQltyP & "", AdoCN, 1, 1)
                        '    If rstTgtQlty.RecordCount Then
                        '        mInRate = rstTgtQlty.Fields("Incentive").Value
                        '        mActIncen = ((mInRate / 100) * mIncentive)
                        '    End If
                        'Else
                        '    mActIncen = mIncentive
                        'End If

                        'If mActIncen < 0 Then
                        '    mActIncen = 0
                        'End If
                        '--------------------------------------------------------------------------------------------------
                        AdoCN.Execute("INSERT INTO tblMIXIncentive(EmpNo,Workdays,Target,Units,ProdPcs,IncentivePcs,Reject,Broken,Lost,Repair,Nopay," & _
                                        "Rate,Incentive,ActIncentive,Service,Sec,Grp,Grd,SecNo,Leave,RghCts,IssCts,RetCts,Flow,Prod,QltyP,IncenPer,Nopay1,Hours) " & _
                                      "VALUES('" & mEmpNo & "','" & mDays & "','" & dblTotTarget & "','" & XUnits & "','" & TotReturns & "','" & mOvrTgt & "','" & TotRejects & "','" & TotBroken & "'," & _
                                        "'" & TotLost & "','" & TotRepair & "','" & TotNopay & "','" & mRate & "','" & mIncentive & "','" & mActIncen & "'," & _
                                        "'" & mSMonth & "','" & mSec & "','" & mGrp & "','" & mgrd & "','" & mSecNo & "','" & mLv & "','" & mRghcts & "'," & _
                                        "'" & mIssCts & "','" & mRetCts & "','" & mFlow & "','" & mProd & "','" & mQltyP & "','" & mInRate & "','" & TotNopay1 & "','" & dblHours & "')")

                    Loop Until rstGet.EOF
                End If

                rstGet = Nothing
                rstHly = Nothing
                ExpProgress.Value = 0
                ExpProgress.Visible = False

                MsgBox("Incentive Procees is Complete", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Else
                MsgBox("Invalid end date", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            MsgBox("Dates are not valid", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If

        Exit Sub

Traperror:
        Debug.Print("Exec SP_GETPRODMIX " & dtpFromDate.Value & "," & dtpToDate.Value)
        If Err.Number = 13 Then
            MsgBox("Invalid date period", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        Else
            Debug.Print(Err.Description)
            MsgBox("Error :: Contact ICT Department !! " & Err.Number & "  " & Err.Description, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If

    End Sub

    Private Sub GetLeaves()
        Dim rsttmpLv As ADODB.Recordset
        Dim rstLMW As ADODB.Recordset
        Dim EmpNo, Leaves, DLeaves As String

        AdoCN.Execute("DELETE FROM tblPCULeaveCount")

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
                    'AdoCN.Execute " INSERT INTO tblPCULeaveCount EmpNo, Leave, DLeave) VALUES ('" & EmpNo & "', '" & Leaves & "','" & DLeaves & "');"
                    rsttmpLv = New ADODB.Recordset
                    rsttmpLv.Open("Select * From tblPCULeaveCount", AdoCN, 1, 3)

                    rsttmpLv.AddNew()
                    rsttmpLv.Fields("EmpNo").Value = EmpNo
                    rsttmpLv.Fields("Leave").Value = Leaves
                    rsttmpLv.Fields("DLeave").Value = DLeaves
                    rsttmpLv.Update()

                Loop Until .EOF
            End With
        End If
        rstLMW = Nothing
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If PBUser_EmpNo <> "D06975" Then
            Instring = UCase(InputBox("Enter Access Code", "Authorized Password"))
            If Instring = "CHA19810724" Then
                GetIncentive()
            End If
        Else
            GetIncentive()
        End If
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXIncentiveDeptWise.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXIncentiveAllEmp.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub GetIncentiveEmp(ByVal strEmpNo As String)
        'On Error GoTo resume next
        Dim rstLev As ADODB.Recordset
        Dim rstRate As ADODB.Recordset
        Dim rstHly As ADODB.Recordset
        Dim rstGet As ADODB.Recordset
        Dim rstmasnew As ADODB.Recordset
        Dim rstTarget As ADODB.Recordset
        Dim mSec, mGrp, mgrd, mEmpNo, mUnit As String
        Dim mProd, mQltyP, mInRate As Single
        Dim xTgt, mSMonth, mSecNo As Integer
        Dim recEMP As Integer
        Dim mOvrTgt, mLv, nUnit, mRghcts, mRetCts, mIssCts As Single
        Dim TotRepair, TotNopay, TotUnits, RUnits, XUnits, TotReturns, TotRejects, TotBroken, TotLost, mQuality As Single
        Dim mIncentive, mRate, mActIncen, mSecRate, RIncentive As Double
        Dim mFlow As String

        ExpProgress.Value = 0
        ExpProgress.Visible = True

        If dtpFromDate.Value <= dtpToDate.Value Then
            If dtpToDate.Value <= Date.Now Then
                wDays = DateDiff(DateInterval.Day, dtpFromDate.Value, dtpToDate.Value) + 1

                AdoCN.Execute("DELETE FROM tblEmployee")
                AdoCN.Execute("INSERT INTO tblEmployee(Emp_No,Initials,Name,SurName,DOJ,Pay,Staff,TargetDate,SSection,GGroup,Grade,Department,Category) " & _
                              "SELECT FullEmpNo,INITIALS,NAME,SURNAME,DATE_JOINED,Pay,PROCESS_TYPE,TargetDate,SECTION_DESC,GRP_DESC,GRADE,DepartmentName,CATEGORY " & _
                              "FROM VW_EMP_MASTER")

                'Get Holiday information
                rstHly = New ADODB.Recordset
                rstHly.Open("SELECT * FROM VW_EMP_HOLIDAYS WHERE HDate BETWEEN '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "' AND '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
                wDays = wDays - (rstHly.RecordCount)

                GetLeaves()

                'Start calculating Incentive

                AdoCN.Execute("DELETE FROM tblMIXIncentive_EMP")

                'Get production information of employees.

                rstGet = New ADODB.Recordset
                mStrSQL = "SP_GETPRODMIX_EMP "
                mStrSQL = mStrSQL & "'" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "','" & Format(dtpToDate.Value, "MM/dd/yyyy") & "','" & strEmpNo & "'"
                rstGet.Open(mStrSQL, AdoCN, 1, 1)

                ExpProgress.Maximum = rstGet.RecordCount

                If rstGet.RecordCount = 0 Then
                    MsgBox("No Records Found")
                    Exit Sub
                Else
                    rstGet.MoveFirst()
                    Do
                        mLv = 0
                        mEmpNo = Trim(rstGet.Fields("EmpNo").Value)
                        'If mEmpNo = "D07123" Then
                        '    MsgBoxGT mEmpNo
                        'End If
                        rstLev = New ADODB.Recordset
                        rstLev.Open("SELECT EmpNo, SUM(Leave) AS Leaves, SUM(DLeave) AS DLeaves FROM tblPCULeaveCount GROUP BY EmpNo HAVING EmpNo = '" & mEmpNo & "'", AdoCN, 1, 1)

                        If rstLev.RecordCount > 0 Then
                            mLv = (rstLev.Fields("Leaves").Value + rstLev.Fields("DLeaves").Value)
                            mDays = wDays - (rstLev.Fields("Leaves").Value + rstLev.Fields("DLeaves").Value)
                        Else
                            mDays = wDays
                        End If

                        'Initialze variables

                        TotUnits = 0
                        TotTarget = 0
                        TotReturns = 0
                        TotRejects = 0
                        TotBroken = 0
                        TotLost = 0
                        TotNopay = 0
                        mQuality = 0
                        TotRepair = 0
                        mRate = 0
                        mIncentive = 0
                        mRghcts = 0
                        mRetCts = 0
                        mIssCts = 0
                        mFlow = ""
                        mProd = 0
                        mQltyP = 0
                        mInRate = 0
                        mSec = 0
                        mGrp = ""
                        mgrd = ""

                        rstmasnew = New ADODB.Recordset
                        rstmasnew.Open("SELECT * FROM tblEmployee WHERE Emp_No = '" & mEmpNo & "'", AdoCN, 1, 1)

                        'Temporary testing
                        Dim xx As Object
                        xx = rstmasnew.RecordCount

                        'Calculate Target Month
                        If xx > 0 Then
                            mSMonth = DateDiff("M", rstmasnew.Fields("TargetDate").Value, CDate(Format(dtpFromDate.Value, "yyyy/MM/dd"))) + 1
                        End If

                        If xx > 0 Then
                            If mSMonth >= 8 Then
                                mSMonth = 7
                            Else
                                If DateAndTime.Day(rstmasnew.Fields("TargetDate").Value) > 15 Then
                                    mSMonth = mSMonth - 2
                                Else
                                    mSMonth = mSMonth - 1
                                End If
                            End If
                        End If
                        If xx > 0 Then
                            mgrd = rstmasnew.Fields("Grade").Value
                            mGrp = rstmasnew.Fields("GGroup").Value

                            'Select actual section of the employees
                            mSec = rstmasnew.Fields("SSection").Value

                            mSecNo = rstGet.Fields("Sec").Value
                            mFlow = rstGet.Fields("ParFlow").Value
                        End If

                        'Select the target
                        rstTarget = New ADODB.Recordset
                        If chkNew.Checked = False Then
                            rstTarget.Open("SELECT * FROM tblPCUTgtUnitsNew WHERE MasterSec = '" & mSec & "' AND MMonth = '" & mSMonth & "'", AdoCN, 1, 1)
                        Else
                            rstTarget.Open("SELECT * FROM tblPCUTgtUnitsNew WHERE MasterSec = '" & mSec & "' AND MMonth = '" & mSMonth & "'", AdoCN, 1, 1)
                            'rstTarget.Open("SELECT * FROM tblPCUTgtUnits WHERE MasterSec = '" & mSec & "' AND MMonth = '" & mSMonth & "'", AdoCN, 1, 1)
                        End If
                        xTgt = 0
                        If rstTarget.RecordCount Then
                            xTgt = (rstTarget.Fields("TargetP").Value * IIf(mDays >= 0, mDays, 0))
                        End If

                        'Calculating no of Units
                        Do
                            mUnit = "Unit" + rstGet.Fields("ParUnit").Value
                            If rstTarget.RecordCount Then
                                nUnit = rstTarget.Fields(mUnit).Value
                                mRate = rstTarget.Fields("RateP").Value
                                TotUnits = TotUnits + (((rstGet.Fields("RetPcsT").Value + rstGet.Fields("RetPcsB").Value) - rstGet.Fields("Nopay").Value - rstGet.Fields("Nopay1").Value) * CSng(nUnit))
                                TotReturns = TotReturns + ((rstGet.Fields("RetPcsT").Value + rstGet.Fields("RetPcsB").Value))
                                TotRejects = TotRejects + rstGet.Fields("Rej").Value
                                TotBroken = TotBroken + rstGet.Fields("Bro").Value
                                TotLost = TotLost + rstGet.Fields("Lost").Value
                                TotRepair = TotRepair + rstGet.Fields("Repair").Value
                                TotNopay = TotNopay + rstGet.Fields("Nopay").Value + rstGet.Fields("Nopay1").Value
                                mIssCts = mIssCts + rstGet.Fields("ActIss").Value
                                mRetCts = mRetCts + rstGet.Fields("RetCts").Value
                            End If
                            rstGet.MoveNext()
                            ExpProgress.Value = ExpProgress.Value + 1

                            If rstGet.EOF Then Exit Do
                        Loop Until rstGet.EOF Or Trim(rstGet.Fields("EmpNo").Value) <> mEmpNo

                        'If rstGet.EOF Then Exit Do
                        If TotReturns > 0 Then
                            mQltyP = ((TotReturns - (TotRepair + TotNopay)) / TotReturns) * 100
                            mOvrTgt = Int((TotUnits - (rstTarget.Fields("TargetP").Value * (IIf(mDays >= 0, mDays, 0)))))
                        End If
                        '--------------------------------------------------------------------------------------------------
                        'Select actual section wise rate

                        rstRate = New ADODB.Recordset
                        mStrSQL = "SP_GETPRODMIXSECEMP "
                        mStrSQL = mStrSQL & "'" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "','" & Format(dtpToDate.Value, "MM/dd/yyyy") & "','" & mEmpNo & "'"
                        rstRate.Open(mStrSQL, AdoCN, 1, 1)

                        recEMP = rstRate.RecordCount

                        rstTarget.Close()
                        RUnits = 0
                        RIncentive = 0
                        XUnits = 0

                        If rstRate.RecordCount Then
                            rstRate.MoveFirst()
                            Do
                                rstTarget = New ADODB.Recordset
                                If chkNew.Checked = False Then
                                    rstTarget.Open("SELECT * FROM tblPCUTgtUnitsNew WHERE SecCode = '" & rstRate.Fields("Sec").Value & "' And MMonth = '" & mSMonth & "'", AdoCN, 1, 1)
                                Else
                                    rstTarget.Open("SELECT * FROM tblPCUTgtUnitsNew WHERE SecCode = '" & rstRate.Fields("Sec").Value & "' And MMonth = '" & mSMonth & "'", AdoCN, 1, 1)
                                    'rstTarget.Open("SELECT * FROM tblPCUTgtUnits WHERE SecCode = '" & rstRate.Fields("Sec").Value & "' And MMonth = '" & mSMonth & "'", AdoCN, 1, 1)
                                End If
                                If rstTarget.RecordCount Then
                                    mSecRate = rstTarget.Fields("RateP").Value

                                    mUnit = "Unit" + rstRate.Fields("ParUnit").Value
                                    nUnit = rstTarget.Fields(mUnit).Value
                                    RUnits = (((rstRate.Fields("RetPcsT").Value + rstRate.Fields("RetPcsB").Value) - rstRate.Fields("Nopay").Value - rstRate.Fields("Nopay1").Value) * CSng(nUnit))
                                    XUnits = XUnits + RUnits

                                    RIncentive = (RUnits * mSecRate)
                                    mIncentive = mIncentive + RIncentive

                                    '--------------------------------------------------------------------------------------------------
                                    AdoCN.Execute("INSERT INTO tblMIXIncentive_EMP(EmpNo,Workdays,Target,Units,ProdPcs,IncentivePcs,Reject,Broken,Lost,Repair,Nopay," & _
                                                    "Rate,Incentive,ActIncentive,Service,Sec,Grp,Grd,SecNo,Leave,RghCts,IssCts,RetCts,Flow,Prod,QltyP,IncenPer,IncUnit,FromDate,ToDate) " & _
                                                  "VALUES('" & mEmpNo & "','" & mDays & "','" & xTgt & "','" & RUnits & "','" & rstRate.Fields("RetPcsT").Value + rstRate.Fields("RetPcsB").Value & "','" & mOvrTgt & "','" & rstRate.Fields("Rej").Value & "','" & TotBroken & "'," & _
                                                    "'" & TotLost & "','" & TotRepair & "','" & TotNopay & "','" & mSecRate & "','" & RIncentive & "','" & mActIncen & "'," & _
                                                    "'" & mSMonth & "','" & mSec & "','" & mGrp & "','" & mgrd & "','" & rstRate.Fields("Sec").Value & "','" & mLv & "','" & mRghcts & "'," & _
                                                    "'" & mIssCts & "','" & mRetCts & "','" & mFlow & "','" & mProd & "','" & mQltyP & "','" & nUnit & "','" & rstRate.Fields("ParUnit").Value & "','" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "','" & Format(dtpToDate.Value, "MM/dd/yyyy") & "')")
                                End If
                                rstRate.MoveNext()
                                If rstRate.EOF Then Exit Do
                            Loop Until Trim(rstRate.Fields("EmpNo").Value) <> mEmpNo Or rstRate.RecordCount <> recEMP
                        End If

                        

                    Loop Until rstGet.EOF
                End If

                rstGet = Nothing
                rstHly = Nothing
                ExpProgress.Value = 0
                ExpProgress.Visible = False
            Else
                MsgBox("Invalid end date", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            MsgBox("Dates are not valid", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If

        Exit Sub

Traperror:
        Debug.Print("Exec SP_GETPRODMIX " & dtpFromDate.Value & "," & dtpToDate.Value)
        If Err.Number = 13 Then
            MsgBox("Invalid date period", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        Else
            Debug.Print(Err.Description)
            MsgBox("Error :: Contact ICT Department !! " & Err.Number & "  " & Err.Description, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If

    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        Instring = UCase(InputBox("Enter Employee No"))
        GetIncentiveEmp(Mid(Instring, 1, 6))
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXIncentiveEmpWise.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub GetIncentive_New()
        Dim rstGet As ADODB.Recordset
        Dim strEmpNo As String
        Dim dblLeave As Double
        Dim strDepartment As String
        Dim strGroup As String
        Dim strSection As String
        Dim strCategory As String
        Dim strGrade As String
        Dim dblNormalRate As Double
        Dim dblIncPcs As Double
        Dim dblWorkDays As Double
        Dim dblUnits As Double
        Dim dblTotalUnits As Double
        Dim intTargetMonths As Integer
        Dim strUnit As String
        Dim dblUnitRate As Double
        Dim dblAvgPcs As Double
        Dim dblMultiRate As Double
        Dim dblIncentive As Double
        Dim intMaxSec As Integer
        Dim dblMaxPcs As Double
        Dim strTempEmp As String
        Dim dblTargetPcs As Double

        ExpProgress.Value = 0
        ExpProgress.Visible = True

        If dtpFromDate.Value <= dtpToDate.Value Then
            If dtpToDate.Value <= Date.Now Then
                AdoCN.Execute("DELETE FROM tblMIXIncentive_NEW")

                wDays = DateDiff(DateInterval.Day, dtpFromDate.Value, dtpToDate.Value) + 1

                'Get Holiday information
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_EMP_HOLIDAYS WHERE HDate BETWEEN '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "' AND '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
                wDays = wDays - (rsComSql.RecordCount)
                rsComSql = Nothing

                rstGet = New ADODB.Recordset
                mStrSQL = "SELECT TOP (100) PERCENT EmpNo, Sec, SUM(RetPcsT + RetPcsB) AS RetPCs, SUM(NopayPcs) AS Nopay, SUM(NopayPcs1) AS Nopay1 " & _
                          "FROM dbo.tblMixReturns " & _
                          "WHERE (RetDate BETWEEN '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "' AND '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') AND (RetPcsT + RetPcsB > 0) " & _
                          "GROUP BY Sec, EmpNo " & _
                          "HAVING(Sec < 14) " & _
                          "ORDER BY EmpNo, Sec"
                rstGet.Open(mStrSQL, AdoCN, 1, 1)
                If rstGet.RecordCount Then
                    ExpProgress.Maximum = rstGet.RecordCount
                    rstGet.MoveFirst()
                    While Not rstGet.EOF
                        strEmpNo = Trim(rstGet.Fields("EmpNo").Value)
                        dblIncPcs = rstGet.Fields("RetPCs").Value - (rstGet.Fields("Nopay").Value + rstGet.Fields("Nopay1").Value)

                        strDepartment = ""
                        strGroup = ""
                        strSection = ""
                        strCategory = ""
                        strGrade = ""
                        intTargetMonths = 0
                        dblTargetPcs = 0
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT FullEmpNo, DepartmentName, GRP_DESC, SECTION_DESC, CATEGORY, GRADE, TargetDate FROM dbo.VW_EMP_MASTER_SMALL2 WHERE (FullEmpNo = '" & strEmpNo & "')", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            strDepartment = rsComSql.Fields("DepartmentName").Value
                            strGroup = rsComSql.Fields("GRP_DESC").Value
                            strSection = rsComSql.Fields("SECTION_DESC").Value
                            strCategory = rsComSql.Fields("CATEGORY").Value
                            strGrade = rsComSql.Fields("GRADE").Value
                            intTargetMonths = DateDiff("M", rsComSql.Fields("TargetDate").Value, Format(dtpFromDate.Value, "MM/dd/yyyy")) + 1

                            If intTargetMonths >= 8 Then
                                intTargetMonths = 7
                            Else
                                If DateAndTime.Day(rsComSql.Fields("TargetDate").Value) > 15 Then
                                    intTargetMonths = intTargetMonths - 2
                                Else
                                    intTargetMonths = intTargetMonths - 1
                                End If
                            End If
                        End If
                        rsComSql = Nothing

                        If Mid(UCase(strCategory), 1, 6) = "DIRECT" Then
                            dblLeave = 0
                            rsComSql = New ADODB.Recordset
                            mStrSQL = "SELECT SUM(NoOfDays) AS NoOfDays FROM VW_EMP_LEAVE WHERE LDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "' AND LDate <=  '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "' AND EMPNO = '" & strEmpNo & "' AND (LType = 'ANNUAL' OR LType = 'DUTY')"
                            rsComSql.Open(mStrSQL, AdoCN, 1, 1)
                            If Not IsDBNull(rsComSql.Fields("NoOfDays").Value) Then
                                dblLeave = rsComSql.Fields("NoOfDays").Value
                            End If
                            rsComSql = Nothing

                            dblWorkDays = wDays
                            dblWorkDays = dblWorkDays - dblLeave

                            dblNormalRate = 0
                            rsComSql = New ADODB.Recordset
                            rsComSql.Open("SELECT MAX(RateP) AS RateP FROM dbo.tblPCUTgtUnitsNew WHERE (SecCode = " & rstGet.Fields("Sec").Value & ")", AdoCN, 1, 1)
                            If rsComSql.RecordCount Then
                                dblNormalRate = rsComSql.Fields("RateP").Value
                            End If
                            rsComSql = Nothing

                            dblUnits = 0
                            dblTotalUnits = 0
                            rsComSql = New ADODB.Recordset
                            rsComSql.Open("SELECT TOP (100) PERCENT SUM(dbo.tblMixReturns.RetPcsT + dbo.tblMixReturns.RetPcsB) AS RetPCs, SUM(dbo.tblMixReturns.NopayPcs) AS Nopay, SUM(dbo.tblMixReturns.NopayPcs1) AS Nopay1, dbo.tblOrdersDtls.IncenCat AS ParUnit " & _
                                          "FROM dbo.tblOrdersDtls INNER JOIN dbo.tblMixPacket ON dbo.tblOrdersDtls.OrderNo = dbo.tblMixPacket.PktOrdNo AND dbo.tblOrdersDtls.RefNo = dbo.tblMixPacket.PktRefNo AND " & _
                                            "dbo.tblOrdersDtls.Side = dbo.tblMixPacket.Pktside INNER JOIN dbo.tblMixReturns ON dbo.tblMixPacket.PktOrdNo = dbo.tblMixReturns.ParNo AND dbo.tblMixPacket.PktNo = dbo.tblMixReturns.PktNo " & _
                                          "WHERE (dbo.tblMixReturns.RetDate BETWEEN '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "' AND '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') AND (dbo.tblMixReturns.EmpNo = '" & strEmpNo & "') AND (dbo.tblMixReturns.RetPcsT + dbo.tblMixReturns.RetPcsB > 0) AND (dbo.tblMixReturns.Sec = " & rstGet.Fields("Sec").Value & ") " & _
                                          "GROUP BY dbo.tblOrdersDtls.IncenCat", AdoCN, 1, 1)
                            If rsComSql.RecordCount Then
                                rsComSql.MoveFirst()
                                While Not rsComSql.EOF
                                    dblUnits = 0
                                    rsComSql_1 = New ADODB.Recordset
                                    rsComSql_1.Open("SELECT * FROM tblPCUTgtUnitsNew WHERE SecCode = '" & rstGet.Fields("Sec").Value & "' AND MMonth = " & intTargetMonths & "", AdoCN, 1, 1)
                                    If rsComSql_1.RecordCount Then
                                        strUnit = "Unit" + rsComSql.Fields("ParUnit").Value
                                        dblUnitRate = rsComSql_1.Fields(strUnit).Value

                                        dblTargetPcs = rsComSql_1.Fields("TargetP").Value
                                        dblUnits = ((rsComSql.Fields("RetPCs").Value - (rsComSql.Fields("Nopay").Value + rsComSql.Fields("Nopay1").Value)) * dblUnitRate)
                                        dblTotalUnits = dblTotalUnits + dblUnits
                                    End If
                                    rsComSql_1 = Nothing

                                    rsComSql.MoveNext()
                                End While
                            End If
                            rsComSql = Nothing

                            mStrSQL = "INSERT INTO tblMIXIncentive_NEW(EmpNo, WorkDays, SecNo, Target, TargetPcs, ProdPcs, IncentivePcs, Units, Rate, Incentive, Dept, Grp, Sec, Grd, Cat, Leave, NoPayPcs, NoPayPcs1) " & _
                                      "VALUES('" & strEmpNo & "'," & wDays & "," & rstGet.Fields("Sec").Value & "," & dblWorkDays & "," & dblTargetPcs & "," & rstGet.Fields("RetPCs").Value & "," & dblIncPcs & "," & dblTotalUnits & "," & _
                                        "" & dblNormalRate & ",0,'" & strDepartment & "','" & strGroup & "','" & strSection & "','" & strGrade & "','" & strCategory & "'," & dblLeave & "," & rstGet.Fields("Nopay").Value & "," & rstGet.Fields("Nopay1").Value & ")"

                            AdoCN.Execute(mStrSQL)
                        End If

                        rstGet.MoveNext()
                        ExpProgress.Value = ExpProgress.Value + 1
                    End While
                Else
                    MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rstGet = Nothing

                ExpProgress.Value = 0

                dblAvgPcs = 0
                dblMultiRate = 0
                dblIncentive = 0
                intMaxSec = 0
                dblMaxPcs = 0
                strTempEmp = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblMIXIncentive_NEW ORDER BY EmpNo, SecNo", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    ExpProgress.Maximum = rsComSql.RecordCount
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        If rsComSql.Fields("EmpNo").Value <> strTempEmp Then
                            strTempEmp = rsComSql.Fields("EmpNo").Value
                            dblMaxPcs = rsComSql.Fields("IncentivePcs").Value
                            intMaxSec = rsComSql.Fields("SecNo").Value
                        Else
                            If rsComSql.Fields("IncentivePcs").Value > dblMaxPcs Then
                                dblMaxPcs = rsComSql.Fields("IncentivePcs").Value
                                intMaxSec = rsComSql.Fields("SecNo").Value
                            End If
                        End If
                        
                        dblAvgPcs = rsComSql.Fields("IncentivePcs").Value / rsComSql.Fields("Target").Value

                        dblMultiRate = 0
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT Ratio FROM tblMixAvgTargets WHERE (SecCode = " & rsComSql.Fields("SecNo").Value & ") AND (FromPcs <= " & dblAvgPcs & ") AND (ToPcs > " & dblAvgPcs & ")", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            dblMultiRate = rsComSql_1.Fields("Ratio").Value
                        End If
                        rsComSql_1 = Nothing

                        dblIncentive = dblMultiRate * rsComSql.Fields("Units").Value * rsComSql.Fields("Rate").Value

                        AdoCN.Execute("UPDATE tblMIXIncentive_NEW SET Incentive = " & dblIncentive & ", Ratio = " & dblMultiRate & " WHERE EmpNo = '" & rsComSql.Fields("EmpNo").Value & "' AND SecNo = " & rsComSql.Fields("SecNo").Value & "")
                        AdoCN.Execute("UPDATE tblMIXIncentive_NEW SET MaxSec = " & intMaxSec & " WHERE EmpNo = '" & rsComSql.Fields("EmpNo").Value & "'")

                        rsComSql.MoveNext()
                        ExpProgress.Value = ExpProgress.Value + 1
                    End While
                End If
                rsComSql = Nothing


                ExpProgress.Visible = False

                MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        End If
    End Sub

    Private Sub HazelDev_Button4_Click(sender As Object, e As EventArgs) Handles HazelDev_Button4.Click
        GetIncentive_New()
    End Sub

    Private Sub CalculateEmpHours()
        Dim dblMins As Single
        Dim dblMins1 As Single
        Dim dblHours As Single
        Dim dblHours1 As Single

        ExpProgress.Value = 0
        ExpProgress.Visible = True

        AdoCN.Execute("DELETE FROM tblEmpHours")

        rsComSql = New ADODB.Recordset
        'rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL2 WHERE (PROCESS_TYPE = 3) AND (DEACTIVATE = 0) AND (FullEmpNo = 'D10133') ORDER BY FullEmpNo", AdoCN, 1, 1)
        rsComSql.Open("SELECT FullEmpNo, EMP_NO, WanCode FROM VW_EMP_MASTER_SMALL2 WHERE (PROCESS_TYPE = 3) AND (DEACTIVATE = 0) ORDER BY FullEmpNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            ExpProgress.Maximum = rsComSql.RecordCount
            While Not rsComSql.EOF
                'If rsComSql.Fields("FullEmpNo").Value = "D10133" Then
                '    MsgBox(rsComSql.Fields("FullEmpNo").Value)
                'End If

                'rsComSql_3 = New ADODB.Recordset
                'rsComSql_3.Open("SELECT FullEmpNo FROM VW_EMP_ATTENDANCE WHERE TDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "' AND TDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "' AND FullEmpNo = '" & rsComSql.Fields("FullEmpNo").Value & "'", AdoCN, 1, 1)
                'mDays = rsComSql_3.RecordCount
                'rsComSql_3 = Nothing

                dblMins = 0
                dblMins1 = 0
                dblHours = 0
                dblHours1 = 0
                'rsComSql_3 = New ADODB.Recordset
                'rsComSql_3.Open("SELECT SUM(Minutes) AS Minutes FROM VW_EMP_HOURS_OT WHERE YEAR1 = '" & Format(dtpFromDate.Value, "yyyy") & "' AND MONTH1 = '" & Format(dtpToDate.Value, "MM") & "' AND FullEmpNo = '" & rsComSql.Fields("FullEmpNo").Value & "'", AdoCN, 1, 1)
                'If rsComSql_3.RecordCount Then
                '    If Not IsDBNull(rsComSql_3.Fields("Minutes").Value) Then
                '        dblMins = rsComSql_3.Fields("Minutes").Value
                '    End If
                'End If
                'rsComSql_3 = Nothing

                rsComSql_3 = New ADODB.Recordset
                rsComSql_3.Open("SELECT SUM(ExtraMins) AS Minutes, SUM(ExtraMins1) AS Minutes1 FROM Payroll.dbo.PAY_EMP_OT_SUM WHERE YEAR1 = '" & Format(dtpFromDate.Value, "yyyy") & "' AND MONTH1 = '" & Format(dtpToDate.Value, "MM") & "' AND EMP_NO = '" & rsComSql.Fields("EMP_NO").Value & "' AND WanCode = '" & rsComSql.Fields("WanCode").Value & "'", AdoCN, 1, 1)
                If rsComSql_3.RecordCount Then
                    If Not IsDBNull(rsComSql_3.Fields("Minutes").Value) Then
                        dblMins = rsComSql_3.Fields("Minutes").Value
                        dblMins1 = rsComSql_3.Fields("Minutes1").Value
                    End If
                End If
                rsComSql_3 = Nothing

                rsComSql_3 = New ADODB.Recordset
                rsComSql_3.Open("SELECT SUM(ExtraMins) AS Minutes, SUM(ExtraMins1) AS Minutes1 FROM Payroll.dbo.PAY_EMP_OT_SHIFT WHERE YEAR1 = '" & Format(dtpFromDate.Value, "yyyy") & "' AND MONTH1 = '" & Format(dtpToDate.Value, "MM") & "' AND EMP_NO = '" & rsComSql.Fields("EMP_NO").Value & "' AND WanCode = '" & rsComSql.Fields("WanCode").Value & "'", AdoCN, 1, 1)
                If rsComSql_3.RecordCount Then
                    If Not IsDBNull(rsComSql_3.Fields("Minutes").Value) Then
                        dblMins = rsComSql_3.Fields("Minutes").Value
                        dblMins1 = rsComSql_3.Fields("Minutes1").Value
                    End If
                End If
                rsComSql_3 = Nothing

                dblHours = 0
                If dblMins > 0 Then
                    dblHours = Math.Round(dblMins / 60, 2)
                End If

                dblHours1 = 0
                If dblMins1 > 0 Then
                    dblHours1 = Math.Round(dblMins1 / 60, 2)
                End If

                AdoCN.Execute("INSERT INTO tblEmpHours(EmpNo, Hours, Hours1) VALUES('" & rsComSql.Fields("FullEmpNo").Value & "','" & dblHours & "','" & dblHours1 & "')")

                rsComSql.MoveNext()
                ExpProgress.Value = ExpProgress.Value + 1
            End While
        End If
        rsComSql = Nothing

        ExpProgress.Visible = False

        MsgBox("Calculation Finished", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
    End Sub

    Private Sub cmdHours_Click(sender As Object, e As EventArgs) Handles cmdHours.Click
        If PBUser_EmpNo <> "D06975" Then
            Instring = UCase(InputBox("Enter Access Code", "Authorized Password"))
            If Instring = "CHA19810724" Then
                CalculateEmpHours()
            End If
        Else
            CalculateEmpHours()
        End If
    End Sub

    Private Sub PrintReport10()
        objForm = New frm_DCLReportViewer
        mReportName = "crptEmpIncentive10.rpt"
        strReportPath = PBReportPath & "HR\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button5_Click(sender As Object, e As EventArgs) Handles HazelDev_Button5.Click
        If PBUser_EmpNo <> "D06975" Then
            Instring = UCase(InputBox("Enter Access Code", "Authorized Password"))
            If Instring = "CHA19810724" Then
                PrintReport10()
            End If
        Else
            PrintReport10()
        End If
    End Sub
End Class