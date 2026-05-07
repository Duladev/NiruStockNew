
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_PrIncentive
    Dim strFolderPath As String

    Private Sub frm_PrIncentive_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        dtpFromDate.Value = dhFirstDayInMonth(Date.Now)
        dtpToDate.Value = Date.Now

        If strDBName = "DiaStock" Then
            strFolderPath = "Princess\"
        Else
            strFolderPath = "Princess\"
        End If
    End Sub

    Private Sub GetLeaves()
        Dim rstLMW As ADODB.Recordset
        Dim EmpNo, Leaves, DLeaves As String

        AdoCN.Execute("DELETE FROM tblPRLeaveCount")

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
                    AdoCN.Execute("INSERT INTO tblPRLeaveCount(EmpNo,Leave,DLeave) VALUES('" & EmpNo & "', '" & Leaves & "','" & DLeaves & "')")

                Loop Until .EOF
            End With
        End If
        rstLMW = Nothing
    End Sub

    Private Sub GetIncentive()
        Dim rstGet As ADODB.Recordset
        Dim rstQltytbl As ADODB.Recordset
        Dim rstLev As ADODB.Recordset
        Dim rstHly As ADODB.Recordset
        Dim rstQlty As ADODB.Recordset
        Dim rstmasnew As ADODB.Recordset
        Dim rstTarget As ADODB.Recordset
        Dim rstWrk As ADODB.Recordset
        Dim mEmp, mini, mnam, msur, mSec, mGrp, mgrd, mdep, mCat, mdoj, mtrn, mTgt, mEmpNo As String
        Dim QEmp, QGrp As String
        Dim QQlty, Qmore85, QIn7585, QIn6575, QLess65, QMr85P, QIn7585P, QIn6575P, QLes65P As Single
        Dim QSec, QBadPcs, QRetPcs, xTgt As Integer
        Dim QPQlty, mProd, mQltyP, PFrom, PTo, QFrom, QTo, mInRate As Single
        Dim QFlow As String
        Dim QAll As Integer
        Dim mpay, mstf As Boolean
        Dim x As Date
        Dim y As Date
        Dim rstIncUnit As ADODB.Recordset
        Dim strIncUnit, mUnit As String
        Dim TotTarget, recno, HRecNo, mSMonth, mSecNo, i, wDays As Integer
        Dim TotRepair, TotNopay, TotUnits, TotReturns, TotRejects, TotBroken, TotLost, mQuality, mDays As Single
        Dim mIncentive, mRate As Double
        Dim Leaves, DLeaves, Attn, mOvrTgt, mOvrUnt, mLv, mQlty, mQltyTgt, nUnit, mRghcts, mRetCts, mIssCts As Single

        ExpProgress.Value = 0
        ExpProgress.Visible = True

        'On Error GoTo Traperror
        On Error Resume Next
        If dtpFromDate.Value <= dtpToDate.Value Then
            If dtpToDate.Value <= Date.Now Then
                wDays = DateDiff(DateInterval.Day, dtpFromDate.Value, dtpToDate.Value) + 1
                'Get Master Data from Personal MDB
                AdoCN.Execute("DELETE FROM tblPRIncentive")
                AdoCN.Execute("DELETE FROM tblPREmployee")
                AdoCN.Execute("INSERT INTO tblPREmployee(Emp_No,Initials,Name,SurName,DOJ,Pay,Staff,TargetDate,SSection,GGroup,Grade,Department,Category) " & _
                                "SELECT FullEmpNo,INITIALS,NAME,SURNAME,DATE_JOINED,Pay,PROCESS_TYPE,TargetDate,SECTION_DESC,GRP_DESC,GRADE,DepartmentName,CATEGORY " & _
                                "FROM VW_EMP_MASTER")


                '-------------------------------------------------------------------------------------------------------------------------------------------
                'Get production information of employees.
                rstGet = New ADODB.Recordset
                mStrSQL = "SP_PRGETPROD "
                mStrSQL = mStrSQL & "'" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "','" & Format(dtpToDate.Value, "MM/dd/yyyy") & "',''"
                rstGet.Open(mStrSQL, AdoCN, 1, 1)
                recno = rstGet.RecordCount
                ExpProgress.Maximum = recno

                '-------------------------------------------------------------------------------------------------------------------------------------------
                'Get Holiday information

                rstHly = New ADODB.Recordset
                rstHly.Open("SELECT * FROM VW_EMP_HOLIDAYS WHERE HDate BETWEEN '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "' AND '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
                recno = rstGet.RecordCount
                HRecNo = rstHly.RecordCount
                wDays = wDays - (rstHly.RecordCount)

                'On Error Resume Next
                GetLeaves()
                '--------------------------------------------------------------------------------------------------
                'Get production Quality information of employees.

                rstQlty = New ADODB.Recordset
                mStrSQL = "SP_PRQlty "
                mStrSQL = mStrSQL & "'" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "','" & Format(dtpToDate.Value, "MM/dd/yyyy") & "',''"
                rstQlty.Open(mStrSQL, AdoCN, 1, 1)

                If rstQlty.BOF = False Then
                    rstQlty.MoveFirst()
                End If

                AdoCN.Execute("DELETE FROM tblPRQuality")  'REM BUT NEED TO CREATE TBL IN NEW DB

                '--------------------------------------------------------------------------------------------------
                rstQlty.MoveLast()
                rstQlty.MoveFirst()

                Do

                    QEmp = ""
                    QFlow = ""
                    QSec = 0
                    QQlty = 0
                    QBadPcs = 0
                    QRetPcs = 0

                    Do
                        QEmp = rstQlty.Fields("EmpNo").Value
                        QSec = rstQlty.Fields("Sec").Value
                        QFlow = rstQlty.Fields("Flow").Value
                        QQlty = Format(rstQlty.Fields("QltyPcs").Value, "#00")
                        QRetPcs = Format(rstQlty.Fields("RetPcs").Value, "#00")
                        QBadPcs = Format(rstQlty.Fields("BadPcs").Value, "#00")

                        QPQlty = ((QQlty / QRetPcs) * 100)

                        rstQlty.MoveNext()
                    Loop Until rstQlty.Fields("EmpNo").Value <> QEmp

                    rstQltytbl = New ADODB.Recordset
                    rstQltytbl.Open("Select * From tblPRQuality", AdoCN, 1, 3)

                    rstQltytbl.AddNew()
                    rstQltytbl.Fields("EmpNo").Value = QEmp
                    rstQltytbl.Fields("Flow").Value = QFlow
                    rstQltytbl.Fields("SecNo").Value = QSec
                    rstQltytbl.Fields("RetPcs").Value = QRetPcs
                    rstQltytbl.Fields("Badpcs").Value = QBadPcs
                    rstQltytbl.Fields("Qltypcs").Value = QQlty
                    rstQltytbl.Fields("PercentQlty").Value = QPQlty

                    rstQltytbl.Update()


                    rstQlty.MoveNext()
                Loop Until rstQlty.EOF = True
                rstQltytbl.Close()
                '-------------------------------------------------------------------------------------------------------------------------------------------
                'Start calculating Incentive

                rstGet.MoveFirst()
                If rstGet.RecordCount = 0 Then
                    MsgBox("No Records", MsgBoxStyle.Information + vbOKOnly, Me.Text)
                    Exit Sub
                Else
                    Do
                        mLv = 0
                        mDays = 0
                        mEmpNo = Trim(rstGet.Fields("EmpNo").Value)
                        'If mEmpNo = "D06713" Then
                        '    MsgBox(mEmpNo)
                        'End If
                        rstLev = New ADODB.Recordset
                        rstLev.Open("SELECT EmpNo, SUM(Leave) AS Leaves, SUM(DLeave) AS DLeaves From tblBAGLeaveCount GROUP BY EmpNo HAVING EmpNo = '" & mEmpNo & "'", AdoCN, 1, 1)
                        If rstLev.RecordCount Then
                            mLv = (rstLev.Fields("Leaves").Value + rstLev.Fields("DLeaves").Value)
                            If rstLev.RecordCount > 0 Then
                                mDays = wDays - (rstLev.Fields("Leaves").Value + rstLev.Fields("DLeaves").Value)
                            Else
                                mDays = wDays
                            End If
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
                        mSec = 0
                        mGrp = ""
                        mgrd = ""

                        rstmasnew = New ADODB.Recordset
                        rstmasnew.Open("SELECT * FROM tblPREmployee WHERE Emp_No = '" & mEmpNo & "'", AdoCN, 1, 1)

                        'Temporary testing
                        Dim xx As Object
                        xx = rstmasnew.RecordCount
                        If xx > 0 Then
                            'Calculate service
                            mSMonth = DateDiff("M", rstmasnew.Fields("TargetDate").Value, Format(dtpFromDate.Value, "MM/dd/yyyy")) + 1

                            If mSMonth > 11 Then
                                mSMonth = 11
                            Else
                                If DateAndTime.Day(rstmasnew.Fields("TargetDate").Value) > 15 Then
                                    mSMonth = mSMonth - 1
                                Else
                                    mSMonth = mSMonth
                                End If
                            End If
                            'Correct groups
                            mGrp = rstmasnew.Fields("GGroup").Value
                            mgrd = rstmasnew.Fields("Grade").Value

                            'Select actual section of the employees
                            mSec = rstmasnew.Fields("SSection").Value
                        End If

                        mSecNo = rstGet.Fields("Sec").Value

                        'Select the target
                        rstTarget = New ADODB.Recordset
                        If mSMonth = "0" Then mSMonth = "1"
                        mStrSQL = "SELECT * FROM tblPRUnits WHERE SecName = '" & mSecNo & "' And MMonth = '" & mSMonth & "'"
                        rstTarget.Open(mStrSQL, AdoCN, 1, 1)

                        Do
                            strIncUnit = ""
                            rstIncUnit = New ADODB.Recordset
                            rstIncUnit.Open("SELECT IncUnit FROM tblParcel WHERE Depart = 'Princess' AND GrpParNo = '" & rstGet.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                            If rstIncUnit.RecordCount Then
                                strIncUnit = rstIncUnit.Fields("IncUnit").Value
                            End If
                            rstIncUnit = Nothing

                            'mUnit = "Unit" + rstGet.Fields("IncUnit")
                            mUnit = "Unit" & strIncUnit
                            nUnit = rstTarget.Fields(mUnit).Value
                            mRate = rstTarget.Fields("Rate").Value
                            TotUnits = TotUnits + (((rstGet.Fields("RetPcsT").Value + rstGet.Fields("RetPcsB").Value) - rstGet.Fields("Nopay").Value) * CSng(nUnit))
                            TotReturns = TotReturns + ((rstGet.Fields("RetPcsT").Value + rstGet.Fields("RetPcsB").Value) - rstGet.Fields("Nopay").Value)
                            TotRejects = TotRejects + rstGet.Fields("Rej").Value
                            TotBroken = TotBroken + rstGet.Fields("Bro").Value
                            TotLost = TotLost + rstGet.Fields("Lost").Value
                            TotRepair = TotRepair + rstGet.Fields("Repair").Value
                            TotNopay = TotNopay + rstGet.Fields("Nopay").Value
                            mRghcts = mRghcts + rstGet.Fields("ActRough").Value
                            mIssCts = mIssCts + rstGet.Fields("ActIss").Value
                            mRetCts = mRetCts + rstGet.Fields("RetCts").Value

                            rstGet.MoveNext()
                            ExpProgress.Value = ExpProgress.Value + 1
                        Loop Until Trim(rstGet.Fields("EmpNo").Value) <> mEmpNo Or rstGet.RecordCount <> recno

                        mOvrTgt = Int((TotUnits - (rstTarget.Fields("Target").Value * (IIf(mDays >= 0, mDays, 0)))))

                        'Quality marks system
                        '--------------------------------------------------------------------------------------------------
                        If mSMonth >= 2 Then
                            rstQltytbl = New ADODB.Recordset
                            rstQltytbl.Open("SELECT * FROM tblBAGQuality WHERE EmpNo = '" & mEmpNo & "'", AdoCN, 1, 1)
                            xx = 0
                            xx = rstQltytbl.RecordCount
                            If rstQltytbl.RecordCount > 0 Then
                                i = 0
                                Do
                                    If i = 1 Then
                                        mQlty = rstQltytbl.Fields("more85p").Value
                                        mQltyTgt = (mQlty / 100) * mOvrTgt
                                        mIncentive = mIncentive + (mQltyTgt * IIf(mOvrTgt >= 0, rstTarget.Fields("Rate").Value, 0))
                                        mQlty = rstTarget.Fields("Target").Value
                                    End If
                                    If i = 2 Then
                                        mQlty = 0
                                        mQlty = rstQltytbl.Fields("In7585p").Value
                                        mQltyTgt = (mQlty / 100) * mOvrTgt
                                        mIncentive = mIncentive + ((mQltyTgt * IIf(mOvrTgt >= 0, rstTarget.Fields("Rate").Value, 0)) / 100) * 75
                                    End If
                                    If i = 3 Then
                                        mQlty = 0
                                        mQlty = rstQltytbl.Fields("In6575p").Value
                                        mQltyTgt = (mQlty / 100) * mOvrTgt
                                        mIncentive = mIncentive + ((mQltyTgt * IIf(mOvrTgt >= 0, rstTarget.Fields("Rate").Value, 0)) / 100) * 50
                                    End If
                                    If i = 4 Then
                                        mQlty = 0
                                        mQlty = rstQltytbl.Fields("Less65p").Value
                                        mQltyTgt = (mQlty / 100) * mOvrTgt
                                        mIncentive = mIncentive + ((mQltyTgt * IIf(mOvrTgt >= 0, rstTarget.Fields("Rate").Value, 0)) / 100) * 0
                                    End If
                                    i = i + 1
                                Loop Until i = 5
                            Else
                                mIncentive = mOvrTgt * mRate
                            End If
                        End If

                        '--------------------------------------------------------------------------------------------------
                        rstWrk = New ADODB.Recordset
                        rstWrk.Open("SELECT * FROM tblBAGIncentive", AdoCN, 1, 3)

                        rstWrk.AddNew()
                        rstWrk.Fields("EmpNo").Value = mEmpNo
                        rstWrk.Fields("Workdays").Value = mDays
                        rstWrk.Fields("Target").Value = (rstTarget.Fields("Target").Value * IIf(mDays >= 0, mDays, 0))
                        rstWrk.Fields("Units").Value = Format(TotUnits, "#,###,0.00")
                        rstWrk.Fields("Returns").Value = TotReturns
                        rstWrk.Fields("IncentivePcs").Value = mOvrTgt
                        rstWrk.Fields("Reject").Value = TotRejects
                        rstWrk.Fields("Broken").Value = TotBroken
                        rstWrk.Fields("Lost").Value = TotLost
                        rstWrk.Fields("Repiar").Value = TotRepair
                        rstWrk.Fields("Nopay").Value = TotNopay
                        rstWrk.Fields("Rate").Value = mRate
                        rstWrk.Fields("Incentive").Value = mIncentive
                        rstWrk.Fields("Service").Value = mSMonth
                        rstWrk.Fields("Sec").Value = mSec
                        rstWrk.Fields("Grp").Value = mGrp
                        rstWrk.Fields("Grd").Value = mgrd
                        rstWrk.Fields("SecNo").Value = mSecNo
                        rstWrk.Fields("Leave").Value = mLv
                        rstWrk.Fields("RghCts").Value = mRghcts
                        rstWrk.Fields("IssCts").Value = mIssCts
                        rstWrk.Fields("RetCts").Value = mRetCts
                        rstWrk.Update()

                    Loop Until rstGet.RecordCount <> recno And Trim(rstGet.Fields("EmpNo").Value) <> ""
                End If

                rstGet.Close()
                rstHly.Close()
                rstQlty.Close()
                ExpProgress.Value = 0
                ExpProgress.Visible = False

                MsgBox("Baguettes Incentive process done", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Else
                MsgBox("Invalid end date", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            MsgBox("Dates are not valid", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If
        Exit Sub

Traperror:
        If Err.Number = 13 Then
            MsgBox("Invalid date period", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        Else
            MsgBox("Error :: Contact System Administrator !! " & Err.Number & "  " & Err.Description, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        GetIncentive()
    End Sub
End Class