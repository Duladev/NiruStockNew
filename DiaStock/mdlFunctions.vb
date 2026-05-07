
Imports Excel = Microsoft.Office.Interop.Excel

Module mdlFunctions

    Public Sub OpenDB()
        strServerName = "DCL-ICT-007\DEVELOPER"
        strAccessPath = ""
        strInvoicePath = ""

        If AdoCN.State = 1 Then
            AdoCN.Close()
        End If
        AdoCN.ConnectionString = "Provider=SQLOLEDB;Data Source='" & strServerName & "';Connect Timeout=600;Initial Catalog=" & strDBName & ";Integrated Security=SSPI"
        AdoCN.Open()

        If dbConn.State = 1 Then
            dbConn.Close()
        End If
        dbConn.ConnectionString = "Provider=SQLOLEDB;Data Source='" & strServerName & "';Connect Timeout=600;Initial Catalog=DiaStock;Integrated Security=SSPI"
        dbConn.Open()

        PBReportPath = "D:\Production Reports\"
        PBInvoicePath = "\\" & strInvoicePath & "\Import Info\Invoices\"
        PBImagePath = "\\" & strInvoicePath & "\Import Info\Images\"
        PBKpcPath = "\\" & strInvoicePath & "\Import Info\KPC\"
    End Sub

    Public Sub Load_Parameters()
        Dim dtpCurrentDate As Date
        Dim dtpPreviousDate As Date

        intGradingCheckCts = 0
        intSRWLock = 0
        intCheckIssDate = 0
        intCheckPastIssues = 0
        intCheckRepairDate = 0

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT GradingCheckCts,CheckAccess,SRWLock,PlanStartDate,PlanStartDate1,MaxDueDate,CheckIssDate,DelayDays,CheckPastIssues,CheckRepairDate FROM tblParameters", dbConn, 1, 1)
        If rsComSql.RecordCount Then
            intGradingCheckCts = rsComSql.Fields("GradingCheckCts").Value
            intSRWLock = rsComSql.Fields("SRWLock").Value
            dtpPlanStartDate = rsComSql.Fields("PlanStartDate").Value
            dtpPlanStartDate2 = rsComSql.Fields("PlanStartDate1").Value
            dtpMaxDueDate = rsComSql.Fields("MaxDueDate").Value
            intCheckIssDate = rsComSql.Fields("CheckIssDate").Value
            intDelayDays = rsComSql.Fields("DelayDays").Value
            intCheckPastIssues = rsComSql.Fields("CheckPastIssues").Value
            intCheckRepairDate = rsComSql.Fields("CheckRepairDate").Value
        End If
        rsComSql = Nothing

        dtpCurrentDate = GetToday()

        dtpPreviousDate = DateAdd(DateInterval.Day, -1, dtpCurrentDate)

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM VW_EMP_HOLIDAYS WHERE HDate = '" & Format(dtpPreviousDate, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            intDelayDays = 2
        Else
            Exit Sub
        End If
        rsComSql_1 = Nothing

        dtpPreviousDate = DateAdd(DateInterval.Day, -2, dtpCurrentDate)

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM VW_EMP_HOLIDAYS WHERE HDate = '" & Format(dtpPreviousDate, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            intDelayDays = 3
        Else
            Exit Sub
        End If
        rsComSql_1 = Nothing

        dtpPreviousDate = DateAdd(DateInterval.Day, -3, dtpCurrentDate)

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM VW_EMP_HOLIDAYS WHERE HDate = '" & Format(dtpPreviousDate, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            intDelayDays = 4
        Else
            Exit Sub
        End If
        rsComSql_1 = Nothing
    End Sub

    Public Function GetComputerName() As String
        GetComputerName = System.Environment.MachineName
        Return GetComputerName
    End Function

    Public Function GetDomainUserName() As String
        GetDomainUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString()
        Return GetDomainUserName
    End Function

    Public Function GetUserRights(ByVal strFormName As String) As Boolean
        Dim rsGetUserRights As ADODB.Recordset

        If intCheckAccess = 1 Then
            GetUserRights = False
            rsGetUserRights = New ADODB.Recordset
            rsGetUserRights.Open("SELECT * FROM tblSYS_UserRights WHERE EmpNo = '" & PBUser_EmpNo & "' AND FormName = '" & strFormName & "' AND Allow = 1", dbConn, 1, 1)
            If rsGetUserRights.RecordCount > 0 Then
                GetUserRights = True
            Else
                GetUserRights = False
            End If
            rsGetUserRights = Nothing
        Else
            GetUserRights = True
        End If
        
        Return GetUserRights
    End Function

    Public Function NumericOnly(ByRef KeyAscii As Integer, ByVal currentText As String) As Boolean
        If (KeyAscii < 45) Or (KeyAscii > 57) Then
            NumericOnly = True
        End If
        If (KeyAscii = 8) Then
            NumericOnly = False
        End If
        If InStr(1, currentText, ".", vbTextCompare) > 0 And KeyAscii = Asc(".") Then NumericOnly = True
        Return NumericOnly
    End Function

    Public Function IntegerOnly(ByRef KeyAscii As Integer) As Boolean
        If (KeyAscii < 45) Or (KeyAscii > 57) Then
            IntegerOnly = True
        End If
        If (KeyAscii = 8) Then
            IntegerOnly = False
        End If
        If (KeyAscii = 46) Then
            IntegerOnly = True
        End If
        Return IntegerOnly
    End Function

    Public Function CharacterOnly(ByRef KeyAscii As Integer, ByVal currentText As String) As Boolean
        If (KeyAscii > 47) And (KeyAscii < 58) Then
            CharacterOnly = True
        Else
            CharacterOnly = False
        End If
    End Function

    Public Function strRight(ByVal s As String, ByVal i As Integer) As String
        If Not ((i >= s.Length) Or (s.Length < 1)) Then
            s = s.Substring(s.Length - i, i)
        End If
        Return (s)
    End Function

    Public Function FixedLengthString(ByVal value As String, ByVal totalLength As Integer, ByVal padding As Char) As String
        Dim length = value.Length
        If (length > totalLength) Then Return value.Substring(0, totalLength)
        Return value.PadRight(totalLength, padding)
    End Function

    'Public Sub ClearTextBox(frmSample As System.Windows.Forms.Form)
    '    For Each Control In frmSample.Controls
    '        If (Control.GetType() Is GetType(TextBox)) Then
    '            Dim txt As TextBox = CType(Control, TextBox)
    '            txt.Text = ""
    '        End If
    '        If (Control.GetType() Is GetType(ComboBox)) Then
    '            Dim cmb As TextBox = CType(Control, TextBox)
    '            cmb.Text = ""
    '        End If
    '    Next
    'End Sub

    Public Sub Dep_Grading_Trf(department As String, max_BatchNo As Double, ParcelNo As String, PktNo As String, trfPCS As Double, trfCts As Double, Par_RghPcs As Double, Par_RghCts As Double)
        Dim grading_trf_sql As String
        Dim rs_grading_trf As New ADODB.Recordset
        Dim TrfDate As String
        Dim TrfTime As Date

        TrfDate = Format(Date.Now, "MM/dd/yyyy")
        TrfTime = FormatDateTime(Now(), vbShortTime)

        grading_trf_sql = "INSERT INTO tblGradingTrf(Trfdate,TrfTime,Department,BatchNo,ParcelNo,PktNo,Trf_Pcs,Trf_Cts,Rgh_Pcs,Rgh_Cts,DoneBy,DoneFrom,Status)"
        grading_trf_sql = grading_trf_sql + " VALUES('" & TrfDate & "','" & TrfTime & "','" & department & "'," & max_BatchNo & ",'" & ParcelNo & "','" & PktNo & "'," & trfPCS & "," & trfCts & "," & Par_RghPcs & "," & Par_RghCts & ", suser_sname(), host_name(),0)"
        AdoCN.Execute(grading_trf_sql)

        grading_trf_sql = ""
        rs_grading_trf = Nothing

    End Sub

    Public Sub Load_IncentiveCategory(cbo As System.Windows.Forms.ComboBox)
        Dim i As Integer

        cbo.Items.Clear()
        cbo.Text = ""
        For i = 65 To 90
            cbo.Items.Add(Chr(i))
        Next
    End Sub

    Public Sub Load_Department(cbo As System.Windows.Forms.ComboBox)
        cbo.Items.Clear()
        cbo.Text = ""
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Department FROM tblDepartment WHERE (Transfer = 1) ORDER BY Department", AdoCN, 1, 1)
        If rsComSql.RecordCount > 0 Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cbo.Items.Add(rsComSql.Fields("Department").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Public Sub Load_DepartmentProd(cbo As System.Windows.Forms.ComboBox)
        cbo.Items.Clear()
        cbo.Text = ""
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Department FROM tblDepartment WHERE (Transfer = 1) AND (Prod = 1) ORDER BY Department", AdoCN, 1, 1)
        If rsComSql.RecordCount > 0 Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cbo.Items.Add(rsComSql.Fields("Department").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Public Sub Load_DepartmentExp(cbo As System.Windows.Forms.ComboBox)
        cbo.Items.Clear()
        cbo.Text = ""
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Department FROM tblDepartment WHERE (Prod = 1) ORDER BY Department", AdoCN, 1, 1)
        If rsComSql.RecordCount > 0 Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cbo.Items.Add(rsComSql.Fields("Department").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Public Sub Load_DepartmentEdit(cbo As System.Windows.Forms.ComboBox)
        cbo.Items.Clear()
        cbo.Text = ""
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Department FROM tblDepartment WHERE (Edit = 1) ORDER BY Department", AdoCN, 1, 1)
        If rsComSql.RecordCount > 0 Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cbo.Items.Add(rsComSql.Fields("Department").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Public Sub Load_CutChg(cbo As System.Windows.Forms.ComboBox, strDepartment As String)
        cbo.Items.Clear()
        cbo.Text = ""
        If strDepartment = "Baguettes2" Or strDepartment = "Baguettes3" Or strDepartment = "Emerald2" Or strDepartment = "Emerald3" Then
            strDepartment = "Princess2"
        End If
        If strDepartment = "Rounds4" Then
            strDepartment = "Rounds"
        End If
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT RateCode FROM tblCuttingCharges WHERE (Department = '" & strDepartment & "') ORDER BY RateCode", AdoCN, 1, 1)
        If rsComSql.RecordCount > 0 Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cbo.Items.Add(rsComSql.Fields("RateCode").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Public Sub Load_Segment(cbo As System.Windows.Forms.ComboBox)
        cbo.Items.Clear()
        cbo.Text = ""
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Segment FROM tblRndSegment ORDER BY Segment", AdoCN, 1, 1)
        If Not rsComSql.EOF Then
            While Not rsComSql.EOF
                cbo.Items.Add(Trim(rsComSql.Fields("Segment").Value))
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Public Function GetToday() As Date
        Dim rsGetToDay As ADODB.Recordset

        rsGetToDay = New ADODB.Recordset
        If strCurDateFormat = "MM/dd/yyyy" Or strCurDateFormat = "M/d/yyyy" Then
            rsGetToDay.Open("SELECT CONVERT(VARCHAR(11),GETDATE(),101) AS Today", AdoCN, 1, 1)
        Else
            rsGetToDay.Open("SELECT CONVERT(VARCHAR(11),GETDATE(),103) AS Today", AdoCN, 1, 1)
        End If
        GetToday = CDate(rsGetToDay.Fields("Today").Value)
        rsGetToDay = Nothing
        Return GetToday
    End Function

    Public Sub Dep_Grading_Trf_Selection(select_string As String, dep_sec As Integer, group_ParNo As String, pktRgh_infor As String, lstoutput As DataGridView, strDept As String)
        'Declaring Variables
        '=======================
        Dim export_part_sql As String
        Dim sel_parcel_rghinfor_sql As String
        Dim rs_exportpart As New ADODB.Recordset
        Dim rs_sel_parcel_rghinfor As New ADODB.Recordset
        Dim rgh_pcs As Integer
        Dim rgh_cts As Double
        Dim strGroup As String
        Dim rsRounds As New ADODB.Recordset

        'The Global Query
        '==================================
        export_part_sql = select_string

        rs_exportpart.Open(export_part_sql, AdoCN, 1, 1)

        If Not rs_exportpart.EOF = True Then

            Do Until rs_exportpart.EOF = True 'Lopping through the selected recordset

                'Looping to Find the Rough infor basing on the ParNo and PktNo from related Packet Tables

                sel_parcel_rghinfor_sql = pktRgh_infor
                If strDept = "Rounds3" Or strDept = "Rounds4" Or strDept = "Rounds6" Or strDept = "Rounds7" Or strDept = "RoundsNLE" Or strDept = "Princess2" Or strDept = "Mix" Or _
                    strDept = "Emerald" Or strDept = "Opening" Or strDept = "Lamour" Or strDept = "Davinci" Or strDept = "Baguettes2" Or strDept = "Baguettes3" Or strDept = "Emerald2" Or _
                    strDept = "Emerald3" Or strDept = "Carrer" Or strDept = "Asscher" Or strDept = "Radiant" Then
                    sel_parcel_rghinfor_sql = sel_parcel_rghinfor_sql + " AND ParNo = '" & rs_exportpart.Fields(0).Value & "' AND pktno = '" & rs_exportpart.Fields(1).Value & "'"
                Else
                    sel_parcel_rghinfor_sql = sel_parcel_rghinfor_sql + " WHERE ParNo = '" & rs_exportpart.Fields(0).Value & "' AND pktno = '" & rs_exportpart.Fields(1).Value & "'"
                End If
                rs_sel_parcel_rghinfor.Open(sel_parcel_rghinfor_sql, AdoCN, 1, 1)

                If rs_sel_parcel_rghinfor.EOF = True Then
                    MsgBox("Unable to find the Rough Information ", vbCritical, "Rough Information not found")
                Else
                    rgh_pcs = rs_sel_parcel_rghinfor.Fields(0).Value
                    rgh_cts = rs_sel_parcel_rghinfor.Fields(1).Value
                End If

                strGroup = ""
                If strDept = "Rounds" Then
                    rsRounds = New ADODB.Recordset
                    rsRounds.Open("SELECT Grp FROM tblRndPacket WHERE ParNo = '" & rs_exportpart.Fields(0).Value & "' AND PktNo = '" & rs_exportpart.Fields(1).Value & "'", AdoCN, 1, 1)
                    If rsRounds.RecordCount Then
                        strGroup = rsRounds.Fields("Grp").Value
                    End If
                    rsRounds = Nothing
                ElseIf strDept = "Opening" Or strDept = "Radiant" Or strDept = "Carrer" Or strDept = "Asscher" Or strDept = "Emerald" Then
                    rsRounds = New ADODB.Recordset
                    rsRounds.Open("SELECT Grp FROM tblExtPacket WHERE ParNo = '" & rs_exportpart.Fields(0).Value & "' AND PktNo = '" & rs_exportpart.Fields(1).Value & "' AND Department = '" & strDept & "'", AdoCN, 1, 1)
                    If rsRounds.RecordCount Then
                        strGroup = rsRounds.Fields("Grp").Value
                    End If
                    rsRounds = Nothing
                End If

                lstoutput.Rows.Add(rs_exportpart.Fields(0).Value,
                                   rs_exportpart.Fields(1).Value,
                                   rs_exportpart.Fields(2).Value,
                                   Format(CDbl(rs_exportpart.Fields(3).Value), "#0.000"),
                                   rgh_pcs,
                                   Format(rgh_cts, "#0.000"),
                                   strGroup)

                sel_parcel_rghinfor_sql = Nothing 'Closing the Packet Tables
                rs_sel_parcel_rghinfor.Close()

                rs_exportpart.MoveNext()
            Loop

        Else
            MsgBox("Unable to Find Data", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, strDBName)
        End If

        export_part_sql = ""
        rs_exportpart.Close()

    End Sub

    Public Sub Dep_Grading_Trf(department As String, max_BatchNo As Double, ParcelNo As String, PktNo As String, trfPCS As Double, trfCts As Double, Par_RghPcs As Double, Par_RghCts As Double, strGroup As String)
        Dim grading_trf_sql As String
        Dim rs_grading_trf As New ADODB.Recordset
        Dim rs_Check As New ADODB.Recordset

        grading_trf_sql = "INSERT INTO tblGradingTrf(Trfdate,TrfTime,Department,BatchNo,ParcelNo,PktNo,Trf_Pcs,Trf_Cts,Rgh_Pcs,Rgh_Cts,DoneBy,DoneFrom,Status,Grp) " & _
                          "VALUES('" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & department & "'," & max_BatchNo & ",'" & ParcelNo & "','" & PktNo & "'," & trfPCS & "," & trfCts & "," & Par_RghPcs & "," & Par_RghCts & ", suser_sname(), host_name(),0,'" & strGroup & "')"

        rs_Check = New ADODB.Recordset
        rs_Check.Open("SELECT Department FROM tblGradingTrf WHERE Department = '" & department & "' AND ParcelNo = '" & ParcelNo & "' AND PktNo = '" & PktNo & "' AND Trf_Pcs = " & trfPCS & " AND Trf_Cts = " & trfCts & "", AdoCN, 1, 1)
        If rs_Check.RecordCount = 0 Then
            AdoCN.Execute(grading_trf_sql)
        End If
        rs_Check = Nothing

        grading_trf_sql = ""
        rs_grading_trf = Nothing

    End Sub

    Public Sub ReturnTablesUpdation(dep As String, Trf_ParcelNo As String, Trf_PktNo As String)
        Dim sql_ReturnTablesUpdation As String
        Dim pro_dep As String

        pro_dep = dep

        Select Case pro_dep
            Case "Baguettes"
                sql_ReturnTablesUpdation = "UPDATE dbo.tblBAGReturns SET Gra_Trf=1 WHERE parno='" & Trf_ParcelNo & "' and pktno='" & Trf_PktNo & "' and sec=10 and Gra_Trf=0"
                AdoCN.Execute(sql_ReturnTablesUpdation)
            Case "Princess"
                sql_ReturnTablesUpdation = "UPDATE dbo.tblPRReturns SET Gra_Trf=1 where parno='" & Trf_ParcelNo & "' and pktno='" & Trf_PktNo & "' and sec=9 and Gra_Trf=0"
                AdoCN.Execute(sql_ReturnTablesUpdation)
            Case "Rounds"
                sql_ReturnTablesUpdation = "UPDATE dbo.tblRNDReturns SET Gra_Trf=1 where parno='" & Trf_ParcelNo & "' and pktno='" & Trf_PktNo & "' and sec=25 and Gra_Trf=0"
                AdoCN.Execute(sql_ReturnTablesUpdation)
            Case "Baguettes2", "Baguettes3", "Emerald", "Emerald2", "Emerald3", "Princess2", "Rounds3", "Rounds4", "Rounds6", "RoundsNLE", "Opening", "Lamour", "Davinci", "Carrer", "Asscher", "Radiant"
                sql_ReturnTablesUpdation = "UPDATE dbo.tblExtReturns SET Gra_Trf=1 where parno='" & Trf_ParcelNo & "' and pktno='" & Trf_PktNo & "' and sec=25 and Gra_Trf=0 and Department = '" & pro_dep & "'"
                AdoCN.Execute(sql_ReturnTablesUpdation)
            Case "Mix"
                sql_ReturnTablesUpdation = "UPDATE dbo.tblExpGrading SET OK = 1 WHERE ParNo = '" & Trf_ParcelNo & "' AND PktNo = '" & Trf_PktNo & "' AND OK = 0"
                AdoCN.Execute(sql_ReturnTablesUpdation)
            Case "Niru"
                sql_ReturnTablesUpdation = "UPDATE dbo.tblNiruReturns SET Gra_Trf=1  where parno='" & Trf_ParcelNo & "' and pktno='" & Trf_PktNo & "' and sec=25 and Gra_Trf=0"
                AdoCN.Execute(sql_ReturnTablesUpdation)
        End Select

        Trf_ParcelNo = ""
        Trf_PktNo = ""
        sql_ReturnTablesUpdation = ""

    End Sub

    Public Function max_BatchNo() As Double
        Dim rs_BatchNo As New ADODB.Recordset
        Dim maxNo As Double

        rs_BatchNo.Open("SELECT MAX(BatchNo) FROM dbo.tblGradingTrf", AdoCN, 1, 1)
        maxNo = rs_BatchNo.Fields(0).Value
        rs_BatchNo = Nothing

        max_BatchNo = maxNo + 1

        Return max_BatchNo

    End Function

    Public Sub ExportToExcel(DataGridView1 As DataGridView)
        Dim ExcelApp As Object, ExcelBook As Object
        Dim ExcelSheet As Object
        Dim i As Integer
        Dim j As Integer

        'create object of excel
        ExcelApp = CreateObject("Excel.Application")
        ExcelBook = ExcelApp.WorkBooks.Add
        ExcelSheet = ExcelBook.WorkSheets(1)

        With ExcelSheet
            For Each column As DataGridViewColumn In DataGridView1.Columns
                .cells(1, column.Index + 1) = column.HeaderText
            Next
            For i = 1 To DataGridView1.RowCount
                .cells(i + 1, 1) = DataGridView1.Rows(i - 1).Cells(0).Value
                For j = 1 To DataGridView1.Columns.Count - 1
                    .cells(i + 1, j + 1) = DataGridView1.Rows(i - 1).Cells(j).Value
                Next
            Next
        End With

        ExcelApp.Visible = True

        ExcelSheet = Nothing
        ExcelBook = Nothing
        ExcelApp = Nothing
    End Sub

    Public Sub GradingAcceptations(Accep_Parno As String, Accept_pktno As String, dblTrID As Double, intOpen As Integer)
        Dim sql_GradingAccept As String

        'AdoCN.BeginTrans()
        sql_GradingAccept = "UPDATE dbo.tblGradingTrf SET ActDate = '" & Format(Date.Now, "MM/dd/yyyy") & "',ActTime = '" & Format(Date.Now, "HH:mm:ss") & "',ActBy = '" & PBDomainUserName & "',ActFrom = '" & PBCompName & "',Status = 1,TR_ID = " & dblTrID & ",Opening = " & intOpen & " " & _
                            "WHERE ParcelNo = '" & Accep_Parno & "' AND PktNo = '" & Accept_pktno & "'"
        AdoCN.Execute(sql_GradingAccept)
        'AdoCN.CommitTrans()

        sql_GradingAccept = ""

    End Sub

    Public Function PFGetValueCharges(ByVal cRateCode As String) As Double
        Dim rstResultset As New ADODB.Recordset

        rstResultset = New ADODB.Recordset
        rstResultset.Open("SELECT InvRate FROM tblCuttingCharges WHERE RateCode = '" & cRateCode & "'", AdoCN, 1, 1)
        If Not rstResultset.EOF Then
            PFGetValueCharges = rstResultset.Fields("InvRate").Value
        End If
        rstResultset = Nothing
        Return PFGetValueCharges

    End Function

    Public Function PFGetLabourCharges(ByVal cRateCode As String, ByVal dblPcs As Double, ByVal dblCts As Double) As Double
        Dim rstResultset As New ADODB.Recordset

        dblExtLabour = 0
        rstResultset = New ADODB.Recordset
        rstResultset.Open("SELECT InvRate, Type FROM tblCuttingCharges WHERE RateCode = '" & cRateCode & "'", AdoCN, 1, 1)
        If Not rstResultset.EOF Then
            If rstResultset.Fields("Type").Value = "P" Then
                PFGetLabourCharges = rstResultset.Fields("InvRate").Value * dblPcs
            Else
                PFGetLabourCharges = rstResultset.Fields("InvRate").Value * dblCts
                If rstResultset.Fields("InvRate").Value = 35 Then
                    dblExtLabour = 2.5 * dblCts
                Else
                    dblExtLabour = 0
                End If
            End If
        End If
        rstResultset = Nothing
        PFGetLabourCharges = Math.Round(PFGetLabourCharges, 2)
        dblExtLabour = Math.Round(dblExtLabour, 2)
        Return PFGetLabourCharges

    End Function

    Public Function dhFirstDayInMonth(dtmDate As Date) As Date
        dhFirstDayInMonth = DateSerial(Year(dtmDate), Month(dtmDate), 1)
    End Function

    Public Function GetIncentive(ByVal strEmpNo As String, ByVal intMarks As Double, ByVal intMarks2 As Double, ByVal strYear As String, ByVal strMonth As String) As Double
        Dim rsIncentive As New ADODB.Recordset
        Dim strDepartment As String
        Dim strGroup As String
        Dim strCategory As String
        Dim strGrade As String
        Dim dblAvgIncentive As Double
        Dim dblAvgMarks As Double
        Dim dblNewRate As Double
        Dim intProcessType As Integer
        Dim strPr_Year As String
        Dim strPr_Month As String
        Dim dtpFromDate As Date
        Dim dtpToDate As Date
        Dim dblWDays As Double
        Dim dblIncentive As Double
        Dim dblCompWDays As Double
        Dim dblHolidays As Double

        dblIncentive = 0
        strDepartment = ""
        strGroup = ""
        strCategory = ""
        strGrade = ""
        intProcessType = 0
        dblWDays = 0
        dblCompWDays = 0

        rsIncentive = New ADODB.Recordset
        rsIncentive.Open("SELECT * FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & strEmpNo & "'", AdoCN, 1, 1)
        If rsIncentive.RecordCount Then
            strDepartment = rsIncentive.Fields("DepartmentName").Value
            strGroup = rsIncentive.Fields("GRP_DESC").Value
            strCategory = rsIncentive.Fields("CATEGORY").Value
            strGrade = rsIncentive.Fields("GRADE").Value
            intProcessType = rsIncentive.Fields("PROCESS_TYPE").Value
        End If
        rsIncentive = Nothing

        strPr_Year = strYear
        strPr_Month = CInt(strMonth) - 1
        strPr_Month = CInt(strPr_Month).ToString("00")
        If CInt(strPr_Month) = 0 Then
            strPr_Month = "12"
            strPr_Year = CInt(strYear) - 1
            strPr_Year = CInt(strPr_Year).ToString("0000")
        End If

        If intProcessType = 1 Or intProcessType = 2 Then
            dtpFromDate = CDate(strPr_Month & "/26/" & strPr_Year)
            dtpToDate = CDate(strMonth & "/25/" & strYear)
        ElseIf intProcessType = 3 Then
            dtpFromDate = CDate(strMonth & "/01/" & strYear)
            dtpToDate = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, dtpFromDate))
        End If

        dblCompWDays = DateDiff(DateInterval.Day, dtpFromDate, dtpToDate) + 1

        rsIncentive = New ADODB.Recordset
        rsIncentive.Open("SELECT HDate FROM VW_EMP_HOLIDAYS WHERE (HDate >= '" & Format(dtpFromDate, "MM/dd/yyyy") & "') AND (HDate <= '" & Format(dtpToDate, "MM/dd/yyyy") & "')", AdoCN, 1, 1)
        dblHolidays = rsIncentive.RecordCount
        rsIncentive = Nothing

        dblCompWDays = dblCompWDays - dblHolidays

        Select Case Mid(strGrade, 1, 1)
            Case "A", "B", "C", "D"
                strGrade = Mid(strGrade, 1, 1)
            Case Else
        End Select

        rsIncentive = New ADODB.Recordset
        rsIncentive.Open("SELECT * FROM tblHR_Incentive WHERE Department = '" & strDepartment & "' AND Grade = '" & strGrade & "' AND Year1 = '" & strYear & "' AND Month1 = '" & strMonth & "'", AdoCN, 1, 1)
        If rsIncentive.RecordCount Then
            dblAvgIncentive = rsIncentive.Fields("Amount").Value
        Else
            rsIncentive = New ADODB.Recordset
            rsIncentive.Open("SELECT * FROM tblHR_Incentive WHERE Department = '" & strDepartment & "' AND Category = '" & strCategory & "' AND Year1 = '" & strYear & "' AND Month1 = '" & strMonth & "'", AdoCN, 1, 1)
            If rsIncentive.RecordCount Then
                dblAvgIncentive = rsIncentive.Fields("Amount").Value
            End If
        End If
        rsIncentive = Nothing

        dblAvgMarks = 0
        dblNewRate = 0
        rsIncentive = New ADODB.Recordset
        Select Case strDepartment
            Case "GRADING"
                rsIncentive.Open("SELECT * FROM tblHR_Rates WHERE Department = '" & strDepartment & "' AND GroupName = '" & strGroup & "' AND Category = '" & strCategory & "' AND FromMark <= " & intMarks & " AND ToMark > " & intMarks & "", AdoCN, 1, 1)
            Case "ROUNDS"
                rsIncentive.Open("SELECT * FROM tblHR_Rates WHERE Department = '" & strDepartment & "' AND GroupName = '" & strGroup & "' AND FromMark <= " & intMarks & " AND ToMark > " & intMarks & "", AdoCN, 1, 1)
            Case Else
                rsIncentive.Open("SELECT * FROM tblHR_Rates WHERE Department = '" & strDepartment & "' AND FromMark <= " & intMarks & " AND ToMark > " & intMarks & "", AdoCN, 1, 1)
        End Select
        If rsIncentive.RecordCount Then
            dblAvgMarks = dblAvgIncentive / rsIncentive.Fields("MaxMark").Value
            dblNewRate = (dblAvgMarks / rsIncentive.Fields("MaxOld").Value) * rsIncentive.Fields("RateOld").Value
        End If
        rsIncentive = Nothing

        rsIncentive = New ADODB.Recordset
        rsIncentive.Open("SELECT * FROM VW_EMP_ATTEND WHERE FullEmpNo = '" & strEmpNo & "' AND YEAR1 = '" & strYear & "' AND MONTH1 = '" & strMonth & "'", AdoCN, 1, 1)
        If rsIncentive.RecordCount Then
            dblWDays = rsIncentive.Fields("WorkedDays").Value
        End If
        rsIncentive = Nothing

        rsIncentive = New ADODB.Recordset
        rsIncentive.Open("SELECT * FROM dbo.VW_EMP_LEAVE_ROUNDS WHERE EMPNO = '" & strEmpNo & "' AND LDate >= '" & Format(dtpFromDate, "MM/dd/yyyy") & "' AND LDate <= '" & Format(dtpToDate, "MM/dd/yyyy") & "' AND LType = 'DUTY' AND NoOfDays >= 1", AdoCN, 1, 1)
        If rsIncentive.RecordCount Then
            dblWDays = dblWDays + rsIncentive.RecordCount
        End If
        rsIncentive = Nothing

        If strDepartment = "GRADING" Then
            dblIncentive = dblNewRate * intMarks
            dblIncentive = dblIncentive + (dblNewRate * intMarks2)
            dblIncentive = Math.Round(dblIncentive, 2)
        Else
            dblIncentive = ((dblNewRate * intMarks) * dblWDays) / dblCompWDays
            dblIncentive = dblIncentive + (dblNewRate * intMarks2)
            dblIncentive = Math.Round(dblIncentive, 2)
            If dblIncentive > dblAvgIncentive Then
                dblIncentive = dblAvgIncentive
            End If
        End If

        GetIncentive = Math.Round(dblIncentive, 2)

        Return GetIncentive
    End Function

    Public Sub Insert_Log(ByVal strAction As String, ByVal strDepartment As String, ByVal strParcel As String, ByVal strPkt As String, ByVal intSec As Integer)
        AdoCN.Execute("INSERT INTO tblDCLLog(Action,Department,ParNo,PktNo,Sec,EmpNo) " & _
                      "VALUES('" & strAction & "','" & strDepartment & "','" & strParcel & "','" & strPkt & "'," & intSec & ",'" & PBUser_EmpNo & "')")
    End Sub

    Public Function CheckEmployee(ByVal strEmpNo As String) As Boolean
        Dim rsCheckEmp As New ADODB.Recordset

        CheckEmployee = False
        If Len(Trim(strEmpNo)) = 6 Then
            rsCheckEmp = New ADODB.Recordset
            mStrSQL = ("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL3 WHERE FullEmpNo = '" & Trim(strEmpNo) & "' AND Pay = 1")
            rsCheckEmp.Open(mStrSQL, AdoCN, 1, 1)
            If rsCheckEmp.RecordCount Then
                CheckEmployee = True
            End If
            rsCheckEmp = Nothing
        End If

        Return CheckEmployee
    End Function

    Public Function ListAsArray(ByVal cString As String, ByVal cDelimiter As String) As Object
        Dim nCount As Integer
        Dim nPos As Integer
        Dim aArray() As String

        nCount = 0
        nPos = InStr(1, cString, cDelimiter)
        Do While nPos <> 0
            nCount = nCount + 1
            ReDim Preserve aArray(0 To nCount - 1)
            aArray(nCount - 1) = Mid$(cString, 1, nPos - 1)
            cString = Mid$(cString, nPos + 1)
            nPos = InStr(1, cString, cDelimiter)
        Loop
        nCount = nCount + 1

        ReDim Preserve aArray(0 To nCount - 1)
        aArray(nCount - 1) = cString

        ListAsArray = aArray

    End Function

End Module
