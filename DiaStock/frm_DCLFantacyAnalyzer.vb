
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLFantacyAnalyzer
    Private Sub GetDetails(ByVal strLotNo As String)
        Dim strDeptCode As String
        Dim strPS As String
        Dim strTF As String
        Dim strFantacyName As String
        Dim strSizeRange As String

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT Assortment, NLineNo, Type, Pcs, Cts, Value, SizeRange " & _
                      "FROM dbo.VW_AE_ALL_Summary " & _
                      "WHERE (LotNo = '" & strLotNo & "') " & _
                      "ORDER BY Assortment, SizeRange", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                strDeptCode = ""
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblDCLDeptCode WHERE ProfitCenter = '" & Mid(rsComSql.Fields("Assortment").Value, 1, 1) & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strDeptCode = rsComSql_1.Fields("DeptCode").Value
                End If
                rsComSql_1 = Nothing

                If rsComSql.Fields("Type").Value = "FOREVERMARK" Then
                    strPS = "S"
                    strTF = "TRUE"
                Else
                    strPS = "P"
                    strTF = "TRUE"
                End If

                strFantacyName = rsComSql.Fields("Assortment").Value

                If Mid(strFantacyName, 1, 2) = "VC" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblGrading_SizingList WHERE NAME = '" & strFantacyName & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        strFantacyName = rsComSql_1.Fields("OLDNAME").Value
                    End If
                    rsComSql_1 = Nothing
                End If

                strSizeRange = rsComSql.Fields("SizeRange").Value
                If Len(strSizeRange) = 0 Then
                    strSizeRange = "0"
                End If

                flxDetails.Rows.Add(strDeptCode,
                                   strFantacyName,
                                   rsComSql.Fields("Assortment").Value,
                                   strPS,
                                   rsComSql.Fields("Pcs").Value,
                                   Math.Round(rsComSql.Fields("Cts").Value, 2),
                                   Math.Round(rsComSql.Fields("Value").Value, 2),
                                   "", strSizeRange,
                                   strTF,
                                   CInt(strRight(strDeptCode, 2)), "", "",
                                   rsComSql.Fields("NLineNo").Value,
                                   rsComSql.Fields("Type").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub txtLotNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLotNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            GetDetails(Trim(txtLotNo.Text))
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        txtLotNo.Text = ""
        flxDetails.Rows.Clear()
        txtLotNo.Focus()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub Analyzer_Finish()
        PBResponse = MsgBox("Are you Sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            PBResponse = MsgBox("Are you crazy enough to Finish the Analyzer?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                AdoCN.Execute("UPDATE tblGrading_PackingListM SET Analyze = 1 WHERE Analyze = 0")
                AdoCN.Execute("UPDATE tblGrading_PackingListPCU SET Analyze = 1 WHERE Analyze = 0")
                AdoCN.Execute("UPDATE tblGrading_PackingListCOLM SET Analyze = 1 WHERE Analyze = 0")
                AdoCN.Execute("UPDATE tblExpReExports SET Analyze = 1 WHERE Analyze = 0")

                MsgBox("Analyzer Finished", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtLotNo.Text = ""
                flxDetails.Rows.Clear()
            End If
        End If
    End Sub

    Private Sub cmdFinish_Click(sender As Object, e As EventArgs) Handles cmdFinish.Click
        Analyzer_Finish()
    End Sub

    Private Sub Load_MixPcs()
        txtExpPcs.Text = "0"
        txtRghCts.Text = "0.000"
        txtAddCts.Text = "0.000"
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT SUM(ExpPcs) AS ExpPcs, SUM(RoughCts) AS RoughCts FROM dbo.tblExportVarification WHERE (Department = 'Mix') AND (Status = 'A') AND (Assortment NOT LIKE 'ABK%') AND (Assortment NOT LIKE 'ANL%') AND (Assortment NOT LIKE 'PIM%')", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("ExpPcs").Value) Then
                txtExpPcs.Text = rsComSql.Fields("ExpPcs").Value
                txtRghCts.Text = Format(rsComSql.Fields("RoughCts").Value, "#0.000")
            End If
        End If
        rsComSql = Nothing
    End Sub

    Private Sub txtAddCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAddCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtAddCts.Text)
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If txtAddCts.Text <> "" Then
                If CDbl(txtAddCts.Text) > 0 And CDbl(txtAddCts.Text) <= 0.03 Then
                    AdoCN.Execute("UPDATE tblExportVarification SET RoughCts = RoughCts + ExpPcs * " & CDbl(txtAddCts.Text) & " WHERE (Department = 'Mix') AND (Status = 'A') AND (Assortment NOT LIKE 'ABK%') AND (Assortment NOT LIKE 'ANL%') AND (Assortment NOT LIKE 'PIM%')")

                    Load_MixPcs()

                    MsgBox("Cts Added", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

                    Me.Close()
                Else
                    MsgBox("Invalid Add Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                End If
            End If
        End If
    End Sub

    Private Sub cmdMix_Click(sender As Object, e As EventArgs) Handles cmdMix.Click
        Load_MixPcs()
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptFantacyAnalyzer.rpt"
        strReportPath = PBReportPath & "NiruStock\" & mReportName
        objForm.Show()
    End Sub

    Private Sub frm_DCLFantacyAnalyzer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub
End Class