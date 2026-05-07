
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixIntIssues
    Dim strFolderPath As String

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ClearFields()
        txtAssortment.Text = Mid(txtAssortment.Text, 1, 5)
        txtAssortment.SelectionStart = 5
        txtAssortment.Focus()
        txtEmpNo.Text = "D09472"
        txtTotPcs.Text = ""
        txtTotCts.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtBalPcs.Text = ""
        txtBalCts.Text = ""
        optIssue.Checked = True
        txtIssPcs.Text = ""
        txtIssCts.Text = ""
        txtRetPcs.Text = ""
        txtRetCts.Text = ""
        txtBoxCts.Text = ""
        txtAvgCts.Text = ""
        txtActPcs.Text = ""
        txtActCts.Text = ""
    End Sub

    Private Sub Load_Details()
        Dim intTotPcs As Integer
        Dim dblTotCts As Double
        Dim intIssPcs As Integer
        Dim dblIssCts As Double
        Dim intIssPcsT As Integer
        Dim dblIssCtsT As Double
        Dim intPktPcs As Integer
        Dim dblPktCts As Double
        Dim intIssPcsE As Integer
        Dim dblIssCtsE As Double

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & txtAssortment.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            txtAssortment.Text = UCase(txtAssortment.Text)
            intTotPcs = 0
            dblTotCts = 0

            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT Pcs, Cts FROM VW_MixAssortInOutNew2020 WHERE Assortment = '" & txtAssortment.Text & "'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                    intTotPcs = rsComSql_1.Fields("Pcs").Value
                    dblTotCts = rsComSql_1.Fields("Cts").Value
                    dblTotCts = Math.Round(dblTotCts, 3)
                End If
            End If
            rsComSql_1 = Nothing

            intIssPcsT = 0
            dblIssCtsT = 0
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT SUM(Pcs) AS TotPcs,SUM(Cts) AS TotCts FROM tblMixIntIssues WHERE Assortment = '" & txtAssortment.Text & "' AND IssDate = '" & Format(Date.Now, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
            If Not IsDBNull(rsComSql_1.Fields("TotPcs").Value) Then
                intIssPcsT = rsComSql_1.Fields("TotPcs").Value
                dblIssCtsT = rsComSql_1.Fields("TotCts").Value
                dblIssCtsT = Math.Round(dblIssCtsT, 3)
            End If
            rsComSql_1 = Nothing

            intPktPcs = 0
            dblPktCts = 0
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT SUM(dbo.tblMixPacketDetails.Pcs) AS TotPcs,ROUND(SUM(dbo.tblMixPacketDetails.Cts), 3) AS TotCts " & _
                            "FROM dbo.tblMixPacket INNER JOIN dbo.tblMixPacketDetails ON dbo.tblMixPacket.PktOrdNo = dbo.tblMixPacketDetails.ParNo AND dbo.tblMixPacket.PktNo = dbo.tblMixPacketDetails.PktNo " & _
                            "WHERE dbo.tblMixPacketDetails.Assortment = '" & txtAssortment.Text & "' AND dbo.tblMixPacket.PktIss = '" & Format(Date.Now, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
            If Not IsDBNull(rsComSql_1.Fields("TotPcs").Value) Then
                intPktPcs = rsComSql_1.Fields("TotPcs").Value
                dblPktCts = rsComSql_1.Fields("TotCts").Value
                dblPktCts = Math.Round(dblPktCts, 3)
            End If
            rsComSql_1 = Nothing

            txtTotPcs.Text = intTotPcs
            txtTotCts.Text = Math.Round(dblTotCts, 3)

            txtIssPcs.Text = intIssPcsT - intPktPcs
            txtIssCts.Text = Math.Round(dblIssCtsT - dblPktCts, 3)

            intIssPcs = 0
            dblIssCts = 0
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT SUM(Pcs) AS TotPcs,SUM(Cts) AS TotCts FROM tblMixIntIssues WHERE Assortment = '" & txtAssortment.Text & "' AND IssDate = '" & Format(Date.Now, "MM/dd/yyyy") & "' AND OK = 1", AdoCN, 1, 1)
            If Not IsDBNull(rsComSql_1.Fields("TotPcs").Value) Then
                intIssPcs = rsComSql_1.Fields("TotPcs").Value
                dblIssCts = rsComSql_1.Fields("TotCts").Value
                dblIssCts = Math.Round(dblIssCts, 3)
            End If
            rsComSql_1 = Nothing

            intIssPcsE = 0
            dblIssCtsE = 0
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT SUM(BalPcs) AS TotPcs,SUM(BalCts) AS TotCts FROM VW_MixEmpBal WHERE Assortment = '" & txtAssortment.Text & "'", AdoCN, 1, 1)
            If Not IsDBNull(rsComSql_1.Fields("TotPcs").Value) Then
                intIssPcsE = rsComSql_1.Fields("TotPcs").Value
                dblIssCtsE = rsComSql_1.Fields("TotCts").Value
                dblIssCtsE = Math.Round(dblIssCtsE, 3)
            End If
            rsComSql_1 = Nothing

            txtRetPcs.Text = intIssPcs - intPktPcs
            txtRetCts.Text = Math.Round(dblIssCts - dblPktCts, 3)

            txtBalPcs.Text = CInt(txtTotPcs.Text) - CInt(txtIssPcs.Text) - intIssPcsE
            txtBalCts.Text = Math.Round(CDbl(txtTotCts.Text) - CDbl(txtIssCts.Text) - dblIssCtsE, 3)
            txtPcs.Text = ""
            txtCts.Text = ""

            txtActPcs.Text = intIssPcsE
            txtActCts.Text = dblIssCtsE

            cmbEmpNo2.Focus()
        Else
            MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing
    End Sub

    Private Sub txtAssortment_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAssortment.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Load_Details()
        End If
    End Sub

    Private Sub txtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtPcs.Text <> "" Then
                If optIssue.Checked = True Then
                    txtBalPcs.Text = CInt(txtTotPcs.Text) - CInt(txtPcs.Text) - CInt(txtIssPcs.Text) - CInt(txtActPcs.Text)
                    txtAvgCts.Text = Math.Round(CInt(txtPcs.Text) * (CDbl(txtTotCts.Text) / CDbl(txtTotPcs.Text)), 2)
                Else
                    txtBalPcs.Text = CInt(txtTotPcs.Text) + CInt(txtPcs.Text)
                End If
                txtCts.Focus()
            End If
        End If
    End Sub

    Private Sub txtCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtCts.Text)
        If Asc(e.KeyChar) = 13 Then
            If txtCts.Text <> "" And txtTotCts.Text <> "" Then
                If optIssue.Checked = True Then
                    txtBalCts.Text = Math.Round(CDbl(txtTotCts.Text) - CDbl(txtCts.Text) - CDbl(txtIssCts.Text) - CDbl(txtActCts.Text), 3)
                Else
                    txtBalCts.Text = Math.Round(CDbl(txtTotCts.Text) + CDbl(txtCts.Text), 3)
                End If
            End If
        End If
    End Sub

    Private Sub Save()
        Dim intIssPcs As Integer
        Dim dblIssCts As Double

        If txtAssortment.Text <> "" Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & txtAssortment.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblAssortBlock WHERE Assortment = '" & txtAssortment.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                MsgBox("Assortment is Blocked", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            If txtEmpNo.Text <> "" And cmbEmpNo2.Text <> "" Then
                If UCase(Mid(txtEmpNo.Text, 1, 1)) = "D" Then
                    rsComSql = New ADODB.Recordset
                    mStrSQL = ("SELECT * FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & Mid(txtEmpNo.Text, 1, 6) & "'")
                    rsComSql.Open(mStrSQL, AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        MsgBox("Invalid Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    Else
                        txtEmpNo.Text = UCase(txtEmpNo.Text)
                    End If
                    rsComSql = Nothing
                Else
                    MsgBox("Invalid Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If UCase(Mid(cmbEmpNo2.Text, 1, 1)) = "D" Then
                    rsComSql = New ADODB.Recordset
                    mStrSQL = ("SELECT * FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & Mid(cmbEmpNo2.Text, 1, 6) & "'")
                    rsComSql.Open(mStrSQL, AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        MsgBox("Invalid Assorter", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    Else
                        cmbEmpNo2.Text = UCase(cmbEmpNo2.Text)
                    End If
                    rsComSql = Nothing
                Else
                    MsgBox("Invalid Assorter", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                intIssPcs = 0
                dblIssCts = 0
                If txtPcs.Text <> "" And txtCts.Text <> "" Then
                    If CInt(txtPcs.Text) > 0 And CDbl(txtCts.Text) > 0 Then
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT SUM(Pcs) AS TotPcs,SUM(Cts) AS TotCts FROM tblMixIntIssues WHERE Assortment = '" & txtAssortment.Text & "'", AdoCN, 1, 1)
                        If Not IsDBNull(rsComSql.Fields("TotPcs").Value) Then
                            intIssPcs = rsComSql.Fields("TotPcs").Value
                            dblIssCts = rsComSql.Fields("TotCts").Value
                            dblIssCts = Math.Round(dblIssCts, 3)
                        End If
                        rsComSql = Nothing

                        If optIssue.Checked = True Then
                            If CInt(txtTotPcs.Text) >= CInt(txtPcs.Text) + CInt(txtIssPcs.Text) And CDbl(txtTotCts.Text) >= CDbl(txtCts.Text) + CDbl(txtIssCts.Text) Then
                                AdoCN.Execute("INSERT INTO tblMixIntIssues(Assortment,Pcs,Cts,EmpNo,IssDate,IssTime,EmpNo2,OK) " & _
                                              "VALUES('" & txtAssortment.Text & "'," & CInt(txtPcs.Text) & "," & CDbl(txtCts.Text) & ",'" & txtEmpNo.Text & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & cmbEmpNo2.Text & "',0) ")

                                MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                ClearFields()
                                txtAssortment.Focus()
                            Else
                                MsgBox("Invalid Pcs/Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                Exit Sub
                            End If
                        Else
                            If optReturn.Checked = True Then
                                If CInt(txtRetPcs.Text) >= CInt(txtPcs.Text) And CDbl(txtRetCts.Text) >= CDbl(txtCts.Text) Then
                                    AdoCN.Execute("INSERT INTO tblMixIntIssues(Assortment,Pcs,Cts,EmpNo,IssDate,IssTime,EmpNo2,OK) " & _
                                                  "VALUES('" & txtAssortment.Text & "'," & CInt(txtPcs.Text) & " * (-1)," & CDbl(txtCts.Text) & " * (-1),'" & txtEmpNo.Text & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & cmbEmpNo2.Text & "',2) ")

                                    MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                    ClearFields()
                                    txtAssortment.Focus()
                                Else
                                    MsgBox("Invalid Pcs/Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                    Exit Sub
                                End If
                            End If
                        End If
                    Else
                        MsgBox("Invalid Pcs/Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                Else
                    MsgBox("Invalid Pcs/Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub txtEmpNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEmpNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            rsComSql = New ADODB.Recordset
            mStrSQL = ("SELECT * FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & Mid(txtEmpNo.Text, 1, 6) & "'")
            rsComSql.Open(mStrSQL, AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            Else
                txtEmpNo.Text = UCase(txtEmpNo.Text)
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixIntIssues.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub frm_MixIntIssues_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        If strDBName = "DiaStock" Then
            strFolderPath = "Mix\"
        ElseIf strDBName = "DiaSales" Then
            strFolderPath = "DiaSalesMix\"
        Else
            strFolderPath = "DiaShareMix\"
        End If

        cmbEmpNo2.Items.Clear()
        cmbEmpNo2.Items.Add("D02437")
        cmbEmpNo2.Items.Add("D05502")
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXStkControlIssSummary.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXStkControlSIH.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button4_Click(sender As Object, e As EventArgs) Handles HazelDev_Button4.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXStkControlIssDate.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub cmbEmpNo2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbEmpNo2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            rsComSql = New ADODB.Recordset
            mStrSQL = ("SELECT * FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & Mid(cmbEmpNo2.Text, 1, 6) & "'")
            rsComSql.Open(mStrSQL, AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Assorter", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            Else
                cmbEmpNo2.Text = UCase(cmbEmpNo2.Text)
                txtPcs.Focus()
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub txtBoxCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBoxCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtBoxCts.Text)
        If Asc(e.KeyChar) = 13 Then
            If txtBoxCts.Text <> "" And txtTotCts.Text <> "" Then
                txtCts.Text = Math.Round(CDbl(txtTotCts.Text) - CDbl(txtBoxCts.Text), 3)
                If optIssue.Checked = True Then
                    txtBalCts.Text = Math.Round(CDbl(txtTotCts.Text) - CDbl(txtCts.Text) - CDbl(txtIssCts.Text), 3)
                Else
                    txtBalCts.Text = Math.Round(CDbl(txtTotCts.Text) + CDbl(txtCts.Text), 3)
                End If
            End If
        End If
    End Sub

    Private Sub HazelDev_Button5_Click(sender As Object, e As EventArgs) Handles HazelDev_Button5.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixIntIssueRec.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub
End Class