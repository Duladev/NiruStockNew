
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixAssortExport
    Dim strFolderPath As String

    Private Sub ClearFields()
        txtExpNo.Text = GetNewExpNo()
        txtAssortment.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtActCts.Text = ""
        flxAssort.Rows.Clear()
        txtTotPcs.Text = ""
        txtTotCts.Text = ""
        txtTotAct.Text = ""
        txtDifCts.Text = ""
        txtBalPcs.Text = ""
        txtBalCts.Text = ""
        dtpExpDate.Value = Date.Now
    End Sub

    Private Function GetNewExpNo()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(ExpNo) AS MaxNo FROM tblAssortExports", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("MaxNo").Value) Then
                GetNewExpNo = rsComSql.Fields("MaxNo").Value + 1
            Else
                GetNewExpNo = 1
            End If
        Else
            GetNewExpNo = 1
        End If
        rsComSql = Nothing

    End Function

    Private Sub frm_MixAssortExport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        If strDBName = "DiaStock" Then
            strFolderPath = "Export\"
        Else
            strFolderPath = "DiaSalesExport\"
        End If

        txtExpNo.Text = GetNewExpNo()
        dtpExpDate.Value = Date.Now
    End Sub

    Private Sub txtAssortment_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAssortment.KeyPress
        If Asc(e.KeyChar) = 13 Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & txtAssortment.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtAssortment.Text = ""
            Else
                txtAssortment.Text = UCase(txtAssortment.Text)

                txtBalPcs.Text = "0"
                txtBalCts.Text = "0"
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT SUM(InPcs - OutPcs) AS BalPcs, ROUND(SUM(InCts - OutCts), 3) AS BalCts " & _
                                "FROM VW_MixAssortInOutNew " & _
                                "WHERE Assortment = '" & txtAssortment.Text & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("BalPcs").Value) Then
                        txtBalPcs.Text = rsComSql_1.Fields("BalPcs").Value
                        txtBalCts.Text = Math.Round(rsComSql_1.Fields("BalCts").Value, 3)
                    End If
                End If
                rsComSql_1 = Nothing

                txtPcs.Focus()
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub txtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If CInt(txtPcs.Text) = CInt(txtBalPcs.Text) Then
                txtCts.Text = txtBalCts.Text
                txtActCts.Focus()
            Else
                txtCts.Focus()
            End If
        End If
    End Sub

    Private Sub txtCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtActCts.Focus()
        End If
    End Sub

    Private Sub txtActCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtActCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtActCts.Text)
        If Asc(e.KeyChar) = 13 Then
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim intAssortPcs As Integer
        Dim dblAssortCts As Double
        Dim dblDiffCts As Double

        If txtExpNo.Text = "" Then MsgBox("Invalid Export No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtAssortment.Text = "" Then MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtPcs.Text = "" Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If Val(txtPcs.Text) <= 0 Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtCts.Text = "" Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If Val(txtCts.Text) <= 0 Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtActCts.Text = "" Then MsgBox("Invalid Act Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If Val(txtActCts.Text) <= 0 Then MsgBox("Invalid Act Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & txtAssortment.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT SUM(InPcs) AS TotPcs, SUM(InCts) AS TotCts FROM tblAssortDetails WHERE Assortment = '" & txtAssortment.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("TotPcs").Value) Then
                intAssortPcs = rsComSql.Fields("TotPcs").Value
                dblAssortCts = Math.Round(rsComSql.Fields("TotCts").Value, 3)
                dblAssortCts = Math.Round(dblAssortCts, 3)
            End If
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT SUM(Pcs) AS TotPcs, SUM(Cts) AS TotCts FROM tblMixPacketDetails WHERE Assortment = '" & txtAssortment.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("TotPcs").Value) Then
                intAssortPcs = intAssortPcs - rsComSql.Fields("TotPcs").Value
                dblAssortCts = dblAssortCts - Math.Round(rsComSql.Fields("TotCts").Value, 3)
                dblAssortCts = Math.Round(dblAssortCts, 3)
            End If
        End If
        rsComSql = Nothing

        If intAssortPcs < CInt(txtPcs.Text) Then
            MsgBox("Pcs Exceeds the Assortment Stock Balance", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If dblAssortCts < CDbl(txtCts.Text) Then
            MsgBox("Cts Exceeds the Assortment Stock Balance", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        dblDiffCts = CDbl(txtCts.Text) - CDbl(txtActCts.Text)
        dblDiffCts = Math.Round(dblDiffCts, 3)

        flxAssort.Rows.Add(txtAssortment.Text,
                           txtPcs.Text,
                           txtCts.Text,
                           txtActCts.Text,
                           dblDiffCts)

        txtTotPcs.Text = CalTotalPcs(flxAssort, 1)
        txtTotCts.Text = CalTotalCts(flxAssort, 2)
        txtTotAct.Text = CalTotalCts(flxAssort, 3)
        txtDifCts.Text = CalTotalCts(flxAssort, 4)
        txtAssortment.Text = Mid(txtAssortment.Text, 1, 3)
        txtAssortment.SelectionStart = 4

        txtBalPcs.Text = ""
        txtBalCts.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtActCts.Text = ""

        txtAssortment.Focus()
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(intCalRow, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(intCalRow, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub flxAssort_DoubleClick(sender As Object, e As EventArgs) Handles flxAssort.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxAssort.Rows.RemoveAt(flxAssort.CurrentRow.Index)
            txtTotPcs.Text = CalTotalPcs(flxAssort, 1)
            txtTotCts.Text = CalTotalCts(flxAssort, 2)
            txtTotAct.Text = CalTotalCts(flxAssort, 3)
            txtDifCts.Text = CalTotalCts(flxAssort, 4)
        End If
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim strType As String
        Dim intExport As Integer
        Dim intBalPcs As Integer
        Dim intOutPcs As Integer
        Dim blnFound As Boolean

        If txtExpNo.Text = "" Then MsgBox("Invalid Export No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then

            strType = ""
            If optExport.Checked = True Then
                intExport = 1
                strType = "E"
            ElseIf optRough.Checked = True Then
                intExport = 2
                strType = "T"
            ElseIf optGrading.Checked = True Then
                intExport = 3
                strType = "G"
            ElseIf optSorting.Checked = True Then
                intExport = 4
                strType = "S"
            ElseIf optSales.Checked = True Then
                intExport = 5
                strType = "L"
            ElseIf optPlan.Checked = True Then
                intExport = 6
                strType = "P"
            End If

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblAssortExports WHERE ExpNo = " & CDbl(txtExpNo.Text) & "", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                For intRow = 0 To flxAssort.Rows.Count - 1
                    If Val(flxAssort.Item(1, intRow).Value) > 0 Then

                        AdoCN.Execute("INSERT INTO tblAssortExports(ExpNo,Assortment,Pcs,Cts,ActCts,Export,EntDate) " & _
                                      "VALUES(" & CDbl(txtExpNo.Text) & ",'" & flxAssort.Item(0, intRow).Value & "'," & CInt(flxAssort.Item(1, intRow).Value) & "," & CDbl(flxAssort.Item(2, intRow).Value) & "," & CDbl(flxAssort.Item(3, intRow).Value) & "," & intExport & ",'" & Format(dtpExpDate.Value, "MM/dd/yyyy") & "')")

                        AdoCN.Execute("INSERT INTO tblMixPacketDetails(ParNo,PktNo,Pcs,Cts,Assortment,OrgAssort,EntDate,Type) " & _
                                      "VALUES('" & txtExpNo.Text & "','0'," & Val(flxAssort.Item(1, intRow).Value) & "," & Val(flxAssort.Item(2, intRow).Value) & ",'" & flxAssort.Item(0, intRow).Value & "','APCU','" & Format(Date.Now, "MM/dd/yyyy") & "','" & strType & "')")

                        AdoCN.Execute("INSERT INTO tblAssortExportDetails(ExpNo,Pcs,Cts,Assortment,InID,OrgAssort,ActCts,Export) " & _
                                      "VALUES(" & CDbl(txtExpNo.Text) & "," & Val(flxAssort.Item(1, intRow).Value) & "," & Val(flxAssort.Item(2, intRow).Value) & "," & _
                                        "'" & flxAssort.Item(0, intRow).Value & "',0,'APCU'," & Val(flxAssort.Item(3, intRow).Value) & "," & intExport & ")")

                        'Origin Entry
                        intOutPcs = 0
                        intBalPcs = CInt(flxAssort.Item(1, intRow).Value)
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM VW_MixOriginBal WHERE Assortment = '" & flxAssort.Item(0, intRow).Value & "' AND BalPcs > 0 ORDER BY SysDateTime", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            rsComSql_1.MoveFirst()
                            While Not rsComSql_1.EOF And intBalPcs > 0
                                If intBalPcs > 0 Then
                                    blnFound = False
                                    If intBalPcs <= rsComSql_1.Fields("BalPcs").Value Then
                                        intOutPcs = intBalPcs

                                        intBalPcs = 0
                                        blnFound = True
                                    Else
                                        intOutPcs = rsComSql_1.Fields("BalPcs").Value
                                        intBalPcs = intBalPcs - intOutPcs
                                        blnFound = True
                                    End If
                                    If blnFound = True Then
                                        AdoCN.Execute("INSERT INTO tblMixPacketOrigin(ParNo,PktNo,Assortment,SupParNo,Origin,Pcs,EntDate,BoxInDate) " & _
                                                      "VALUES('" & Trim(txtExpNo.Text) & "','000','" & flxAssort.Item(0, intRow).Value & "','" & rsComSql_1.Fields("SupParNo").Value & "','" & rsComSql_1.Fields("Origin").Value & "'," & intOutPcs & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(rsComSql_1.Fields("SysDateTime").Value, "MM/dd/yyyy") & "')")

                                    End If
                                End If
                                rsComSql_1.MoveNext()
                            End While
                        Else
                            AdoCN.Execute("INSERT INTO tblMixPacketOrigin(ParNo,PktNo,Assortment,SupParNo,Origin,Pcs,EntDate,BoxInDate) " & _
                                  "VALUES('" & Trim(txtExpNo.Text) & "','000','" & flxAssort.Item(0, intRow).Value & "','X900003','De Beers'," & intOutPcs & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "MM/dd/yyyy") & "')")
                        End If
                        rsComSql_1 = Nothing
                    End If
                Next
                MsgBox("Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

            End If
            rsComSql = Nothing

            ClearFields()
        End If

    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingPackingListMix.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpAssortPlanningTrf.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpAssortReExportsPacking.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button5_Click(sender As Object, e As EventArgs) Handles HazelDev_Button5.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpAssortSortingTrf.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button4_Click(sender As Object, e As EventArgs) Handles HazelDev_Button4.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpAssortRoughTrf.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button6_Click(sender As Object, e As EventArgs) Handles HazelDev_Button6.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpAssortGradingTrf.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button8_Click(sender As Object, e As EventArgs) Handles HazelDev_Button8.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpAssortRoughTrf.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button7_Click(sender As Object, e As EventArgs) Handles HazelDev_Button7.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpAssortReExportsSum.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxAssort)
    End Sub
End Class