
Imports System.Data
Imports System.Data.SqlClient

Module mdlVariables
    Public strDBName As String
    Public strServerName As String
    Public strAccessPath As String
    Public strInvoicePath As String

    Public strReportPath As String
    Public strRecordSelectionFormula As String
    Public PBReportPath As String
    Public PBInvoicePath As String
    Public PBImagePath As String
    Public PBKpcPath As String
    Public strFileName As String
    Public PBResponse
    Public AdoCN As New ADODB.Connection()
    Public dbConn As New ADODB.Connection()
    Public dbConnNiru As New ADODB.Connection()
    Public dbHR As New ADODB.Connection()
    Public dbConnOther As New ADODB.Connection()

    Public dbConnDiaStock As New ADODB.Connection()
    Public dbConnDiaSales As New ADODB.Connection()
    Public dbConnDiaShare As New ADODB.Connection()
    Public dbConnEZOnline As New ADODB.Connection()

    Public PBUser_ID As String
    Public PBUser_TablePassword As String
    Public PBDesignation As String
    Public PBDepartment As String
    Public PBUser_Level As Integer
    Public PBUser_EmpNo As String
    Public PBCompName As String
    Public PBDomainUserName As String

    Public Prod_WK_ID As String
    Public WAN_LOCA As String
    'Public Prod_CODE As String
    'Public Prod_SITE_CODE As String
    'Public Prod_SITE_NAME As String

    Public WAN_NAME As String

    '****************************************
    '********* COMMON RECORDSETS ************
    '****************************************
    Public rsComSql As New ADODB.Recordset()
    Public rsComSql_1 As New ADODB.Recordset()
    Public rsComSql_2 As New ADODB.Recordset()
    Public rsComSql_3 As New ADODB.Recordset()
    Public rsComSql_4 As New ADODB.Recordset()
    Public rsComSql_5 As New ADODB.Recordset()
    Public rsComSql_6 As New ADODB.Recordset()

    Public mStrSQL As String

    Public mReportName, mRecordSelectionFormula, mPara As String
    Public objForm As New frm_DCLReportViewer
    Public objForm2 As New frm_DCLReportViewer2

    Public ICNo As String

    Public Parcel As Boolean
    Public Datavalid As Boolean
    'Public ParcelNo As String
    'Public PacketNo As String

    Public Instring As String
    Public Instring1 As String
    Public ParcelLen As Integer

    Public strCurDateFormat As String
    Public dtpToday As Date
    Public intGradingCheckCts As Integer
    Public intCheckAccess As Integer
    Public intSRWLock As Integer

    Public dblExtLabour As Double
    Public dtpPlanStartDate As Date
    Public dtpPlanStartDate2 As Date
    Public dtpMaxDueDate As Date
    Public intCheckIssDate As Integer
    Public intDelayDays As Integer
    Public intCheckPastIssues As Integer
    Public intCheckRepairDate As Integer

    Public intPasswordDays As Integer
End Module
