
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixEmpIssues
    Dim strFolderPath As String
    Dim FirstInput As Date

    Private Sub Load_AssortCodes()
        cmbAssortCode.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Category FROM tblAssortCode GROUP BY Category ORDER BY Category", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbAssortCode.Items.Add(rsComSql.Fields("Category").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Emp()
        cmbEmp.Items.Clear()
        If optIssue.Checked = True Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT EmpNo FROM tblMixEmpIssuesReq WHERE (Status = 0) GROUP BY EmpNo ORDER BY EmpNo", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    cmbEmp.Items.Add(rsComSql.Fields("EmpNo").Value)
                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing
        Else
            If optReturn.Checked = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT TOP (100) PERCENT EmpNo2 FROM dbo.VW_MixEmpBalEmp GROUP BY EmpNo2 ORDER BY EmpNo2", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        cmbEmp.Items.Add(rsComSql.Fields("EmpNo2").Value)
                        rsComSql.MoveNext()
                    End While
                End If
                rsComSql = Nothing
            End If
        End If
    End Sub

    Private Sub Load_Details()
        Dim intTotPcs As Integer
        Dim dblTotCts As Double
        Dim intIssPcsT As Integer
        Dim dblIssCtsT As Double

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & txtAssortment.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            txtAssortment.Text = UCase(txtAssortment.Text)
            intTotPcs = 0
            dblTotCts = 0
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT SUM(InCts - OutCts) AS Cts, SUM(InPcs - OutPcs) AS PCs " & _
                            "FROM VW_MixAssortInOutNew " & _
                            "WHERE Assortment = '" & txtAssortment.Text & "'", AdoCN, 1, 1)
            If Not IsDBNull(rsComSql_1.Fields("PCs").Value) Then
                intTotPcs = rsComSql_1.Fields("PCs").Value
                dblTotCts = rsComSql_1.Fields("Cts").Value
                dblTotCts = Math.Round(dblTotCts, 3)
            End If
            rsComSql_1 = Nothing

            intIssPcsT = 0
            dblIssCtsT = 0
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT SUM(BalPcs) AS TotPcs,SUM(BalCts) AS TotCts FROM VW_MixEmpBal WHERE Assortment = '" & txtAssortment.Text & "'", AdoCN, 1, 1)
            If Not IsDBNull(rsComSql_1.Fields("TotPcs").Value) Then
                intIssPcsT = rsComSql_1.Fields("TotPcs").Value
                dblIssCtsT = rsComSql_1.Fields("TotCts").Value
                dblIssCtsT = Math.Round(dblIssCtsT, 3)
            End If
            rsComSql_1 = Nothing

            txtTotPcs.Text = intTotPcs
            txtTotCts.Text = Math.Round(dblTotCts, 3)

            txtBalPcs.Text = CInt(txtTotPcs.Text) - intIssPcsT
            txtBalCts.Text = Math.Round(CDbl(txtTotCts.Text) - dblIssCtsT, 3)

            txtPcs.Text = ""
            txtCts.Text = ""

            cmdEmp.Focus()
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

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub txtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtPcs.Text <> "" Then
                txtCts.Focus()
            End If
        End If
    End Sub

    Private Sub txtCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtCts.Text)
        If Asc(e.KeyChar) = 13 Then
            If txtCts.Text <> "" And txtTotCts.Text <> "" Then

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

            If txtEmp.Text <> "" Then
                rsComSql = New ADODB.Recordset
                mStrSQL = ("SELECT * FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & Mid(txtEmp.Text, 1, 6) & "'")
                rsComSql.Open(mStrSQL, AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    MsgBox("Invalid Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    cmdEmp.Focus()
                    Exit Sub
                Else
                    txtEmp.Text = UCase(txtEmp.Text)
                End If
                rsComSql = Nothing

                intIssPcs = 0
                dblIssCts = 0
                If txtPcs.Text <> "" And txtCts.Text <> "" Then
                    If CInt(txtPcs.Text) > 0 And CDbl(txtCts.Text) > 0 Then
                        If optIssue.Checked = True Then
                            If CInt(txtTotPcs.Text) >= CInt(txtPcs.Text) + CInt(txtIssPcs.Text) And CDbl(txtTotCts.Text) >= CDbl(txtCts.Text) + CDbl(txtIssCts.Text) Then
                                AdoCN.Execute("INSERT INTO tblMixEmpIssues(Assortment,IssPcs,IssCts,EmpNo,IssDate,IssTime,EmpNo2,Lab) " & _
                                              "VALUES('" & txtAssortment.Text & "'," & CInt(txtPcs.Text) & "," & CDbl(txtCts.Text) & ",'" & PBUser_EmpNo & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & txtEmp.Text & "'," & IIf(chkLab.Checked = True, 1, 0) & ") ")

                                MsgBox("Issue Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                ClearFields()
                                txtAssortment.Focus()
                            Else
                                MsgBox("Invalid Pcs/Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                Exit Sub
                            End If
                        Else
                            If optReturn.Checked = True Then
                                If CInt(txtIssPcs.Text) >= CInt(txtPcs.Text) And CDbl(txtIssCts.Text) >= CDbl(txtCts.Text) Then
                                    AdoCN.Execute("INSERT INTO tblMixEmpReturns(Assortment,RetPcs,RetCts,EmpNo,RetDate,RetTime,EmpNo2) " & _
                                                  "VALUES('" & txtAssortment.Text & "'," & CInt(txtPcs.Text) & "," & CDbl(txtCts.Text) & ",'" & PBUser_EmpNo & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & txtEmp.Text & "') ")

                                    MsgBox("Return Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
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

    Private Sub SaveAll()
        Dim intRow As Integer
        Dim strAssortment As String
        Dim i, j As Integer
        Dim intTotPcs As Integer
        Dim dblTotCts As Double
        Dim intIssPcsT As Integer
        Dim dblIssCtsT As Double
        Dim intPktPcs As Integer
        Dim dblPktCts As Double

        If cmbEmp.Text <> "" Then
            If optIssue.Checked = True Then
                If txtEmp.Text <> "" Then
                    rsComSql = New ADODB.Recordset
                    mStrSQL = ("SELECT * FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & Mid(txtEmp.Text, 1, 6) & "'")
                    rsComSql.Open(mStrSQL, AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        MsgBox("Invalid Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        cmdEmp.Focus()
                        Exit Sub
                    Else
                        txtEmp.Text = UCase(txtEmp.Text)
                    End If
                    rsComSql = Nothing
                Else
                    MsgBox("Invalid Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    cmdEmp.Focus()
                    Exit Sub
                End If
            End If

            If optIssue.Checked = True Then
                For i = 0 To flxDetails.Rows.Count - 1
                    If flxDetails.Item(5, i).Value = True Then
                        strAssortment = flxDetails.Item(0, i).Value
                        If Not IsNumeric(flxDetails.Item(1, i).Value) = True Then
                            MsgBox("Invalid Req Pcs - " & strAssortment, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                        If CDbl(flxDetails.Item(1, i).Value) > CDbl(flxDetails.Item(3, i).Value) Then
                            MsgBox("Invalid Req Pcs - " & strAssortment, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                        If Not IsNumeric(flxDetails.Item(2, i).Value) = True Then
                            MsgBox("Invalid Req Cts - " & strAssortment, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                        If CDbl(flxDetails.Item(2, i).Value) > CDbl(flxDetails.Item(4, i).Value) Then
                            MsgBox("Invalid Req Cts - " & strAssortment, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                        For j = i + 1 To flxDetails.Rows.Count - 1
                            If flxDetails.Item(5, j).Value = True Then
                                If flxDetails.Item(0, j).Value = strAssortment Then
                                    MsgBox("Duplicate Assortment - " & strAssortment, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                    Exit Sub
                                End If
                            End If
                        Next
                    End If
                Next
            Else
                If optReturn.Checked = True Then
                    For i = 0 To flxDetails.Rows.Count - 1
                        If flxDetails.Item(5, i).Value = True And chkIssue.Checked = True Then
                            strAssortment = flxDetails.Item(0, i).Value
                            If Not IsNumeric(flxDetails.Item(3, i).Value) = True Then
                                MsgBox("Invalid Select Pcs - " & strAssortment, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                Exit Sub
                            End If
                            If Not IsNumeric(flxDetails.Item(4, i).Value) = True Then
                                MsgBox("Invalid Select Cts - " & strAssortment, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                Exit Sub
                            End If
                            If CDbl(flxDetails.Item(3, i).Value) <= 0 Then
                                MsgBox("Invalid Select Pcs - " & strAssortment, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                Exit Sub
                            End If
                            If CDbl(flxDetails.Item(4, i).Value) <= 0 Then
                                MsgBox("Invalid Select Cts - " & strAssortment, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                Exit Sub
                            End If

                            intTotPcs = 0
                            dblTotCts = 0
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT SUM(InPcs - OutPcs) AS TotPcs,SUM(InCts - OutCts) AS TotCts FROM VW_MixAssortInOutNew WHERE Assortment = '" & strAssortment & "'", AdoCN, 1, 1)
                            If Not IsDBNull(rsComSql_1.Fields("TotPcs").Value) Then
                                intTotPcs = rsComSql_1.Fields("TotPcs").Value
                                dblTotCts = rsComSql_1.Fields("TotCts").Value
                                dblTotCts = Math.Round(dblTotCts, 3)
                            End If
                            rsComSql_1 = Nothing

                            intIssPcsT = 0
                            dblIssCtsT = 0
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT SUM(Pcs) AS TotPcs,SUM(Cts) AS TotCts FROM tblMixIntIssues WHERE Assortment = '" & strAssortment & "' AND IssDate = '" & Format(Date.Now, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
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
                                            "WHERE dbo.tblMixPacketDetails.Assortment = '" & strAssortment & "' AND dbo.tblMixPacket.PktIss = '" & Format(Date.Now, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
                            If Not IsDBNull(rsComSql_1.Fields("TotPcs").Value) Then
                                intPktPcs = rsComSql_1.Fields("TotPcs").Value
                                dblPktCts = rsComSql_1.Fields("TotCts").Value
                                dblPktCts = Math.Round(dblPktCts, 3)
                            End If
                            rsComSql_1 = Nothing

                            If intTotPcs < intIssPcsT - intPktPcs + CInt(flxDetails.Item(3, i).Value) Then
                                MsgBox("Invalid Select Pcs - " & strAssortment, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                Exit Sub
                            End If
                            If Math.Round(dblTotCts, 3) < Math.Round(dblIssCtsT - dblPktCts + CDbl(flxDetails.Item(4, i).Value), 3) Then
                                MsgBox("Invalid Select Cts - " & strAssortment, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                Exit Sub
                            End If
                        End If
                    Next
                End If
            End If

            If optIssue.Checked = True Then
                For intRow = 0 To flxDetails.Rows.Count - 1
                    If flxDetails.Item(5, intRow).Value = True Then
                        AdoCN.Execute("INSERT INTO tblMixEmpIssues(Assortment,IssPcs,IssCts,EmpNo,IssDate,IssTime,EmpNo2) " & _
                                      "VALUES('" & flxDetails.Item(0, intRow).Value & "'," & CInt(flxDetails.Item(1, intRow).Value) & "," & CDbl(flxDetails.Item(2, intRow).Value) & "," & _
                                        "'" & PBUser_EmpNo & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & txtEmp.Text & "') ")
                    End If
                    AdoCN.Execute("UPDATE tblMixEmpIssuesReq SET Status = 1 WHERE ID = " & CDbl(flxDetails.Item(6, intRow).Value) & "")
                Next

                MsgBox("Issue Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Else
                If optReturn.Checked = True Then
                    For intRow = 0 To flxDetails.Rows.Count - 1
                        If flxDetails.Item(5, intRow).Value = True Then
                            strAssortment = flxDetails.Item(0, intRow).Value
                            AdoCN.Execute("INSERT INTO tblMixEmpReturns(Assortment,RetPcs,RetCts,EmpNo,RetDate,RetTime,EmpNo2) " & _
                                          "VALUES('" & strAssortment & "'," & CInt(flxDetails.Item(1, intRow).Value) & "," & CDbl(flxDetails.Item(2, intRow).Value) & "," & _
                                            "'" & PBUser_EmpNo & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & cmbEmp.Text & "') ")

                            If chkIssue.Checked = True Then
                                If CDbl(flxDetails.Item(3, intRow).Value) > 0 And CDbl(flxDetails.Item(4, intRow).Value) > 0 Then
                                    AdoCN.Execute("INSERT INTO tblMixIntIssues(Assortment,Pcs,Cts,EmpNo,IssDate,IssTime,EmpNo2,OK) " & _
                                                  "VALUES('" & strAssortment & "'," & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",'D05230','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & cmbEmp.Text & "',0) ")
                                End If
                            End If
                        End If
                    Next

                    MsgBox("Return Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                End If
            End If

            ClearFields()
        End If
    End Sub

    Private Sub ClearFields()
        txtAssortment.Text = Mid(txtAssortment.Text, 1, 3)
        txtAssortment.SelectionStart = 3
        txtAssortment.Focus()
        txtEmp.Text = ""
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
        txtSelPcs.Text = ""
        txtSelCts.Text = ""
        chkSelect.Checked = False
        flxDetails.Rows.Clear()
        cmbEmp.Text = ""
        cmbAssortCode.Text = ""
        chkIssue.Checked = False
        chkLab.Checked = False
        Load_Emp()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdEmp_Click(sender As Object, e As EventArgs) Handles cmdEmp.Click
        'Dim intIssPcsT As Integer
        'Dim dblIssCtsT As Double

        'Instring = UCase(InputBox("Enter Emp No", "Employee No."))
        'If CheckEmployee(Trim(Instring)) = True Then
        '    Datavalid = True
        '    txtEmp.Text = UCase(Trim(Instring))
        'Else
        '    MsgBox("Invalid IC No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '    Datavalid = False
        '    txtIssPcs.Text = "0"
        '    txtIssCts.Text = "0"
        '    txtEmp.Text = ""
        '    cmdEmp.Focus()
        '    Exit Sub
        'End If

        'intIssPcsT = 0
        'dblIssCtsT = 0
        'rsComSql_1 = New ADODB.Recordset
        'rsComSql_1.Open("SELECT SUM(BalPcs) AS TotPcs,SUM(BalCts) AS TotCts FROM VW_MixEmpBalEmp WHERE Assortment = '" & txtAssortment.Text & "' AND EmpNo2 = '" & txtEmp.Text & "'", AdoCN, 1, 1)
        'If Not IsDBNull(rsComSql_1.Fields("TotPcs").Value) Then
        '    intIssPcsT = rsComSql_1.Fields("TotPcs").Value
        '    dblIssCtsT = rsComSql_1.Fields("TotCts").Value
        '    dblIssCtsT = Math.Round(dblIssCtsT, 3)
        'End If
        'rsComSql_1 = Nothing

        'txtIssPcs.Text = intIssPcsT
        'txtIssCts.Text = dblIssCtsT

        'txtPcs.Focus()

        pnlEmp.Visible = True
        txtEmp2.Text = ""
        txtEmp2.Focus()
    End Sub

    Private Sub optIssue_CheckedChanged(sender As Object, e As EventArgs) Handles optIssue.CheckedChanged
        txtPcs.Focus()
        cmbEmp.Text = ""
        Load_Emp()
        flxDetails.Rows.Clear()
        chkSelect.Checked = False
        txtSelPcs.Text = ""
        txtSelCts.Text = ""

        If optIssue.Checked = True Then
            flxDetails.Columns(3).HeaderText = "Pcs"
            flxDetails.Columns(4).HeaderText = "Cts"

            flxDetails.Columns(2).ReadOnly = False
            flxDetails.Columns(3).ReadOnly = True
            flxDetails.Columns(4).ReadOnly = True
        End If
        
    End Sub

    Private Sub optReturn_CheckedChanged(sender As Object, e As EventArgs) Handles optReturn.CheckedChanged
        txtPcs.Focus()
        cmbEmp.Text = ""
        Load_Emp()
        flxDetails.Rows.Clear()
        chkSelect.Checked = False
        txtSelPcs.Text = ""
        txtSelCts.Text = ""

        If optReturn.Checked = True Then
            flxDetails.Columns(3).HeaderText = "Sel Pcs"
            flxDetails.Columns(4).HeaderText = "Sel Cts"

            flxDetails.Columns(2).ReadOnly = True
            flxDetails.Columns(3).ReadOnly = False
            flxDetails.Columns(4).ReadOnly = False
        End If
        
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixEmpBal.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub frm_MixEmpIssues_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        If strDBName = "DiaStock" Then
            strFolderPath = "Mix\"
        Else
            strFolderPath = "DiaSalesMix\"
        End If
        Load_Emp()
        Load_AssortCodes()
    End Sub

    Private Sub txtEmp_GotFocus(sender As Object, e As EventArgs) Handles txtEmp.GotFocus
        FirstInput = Now()
        txtEmp.Text = ""
    End Sub

    Private Sub txtEmp_KeyUp(sender As Object, e As KeyEventArgs) Handles txtEmp.KeyUp
        Dim TimeDiff As Integer

        If Asc(e.KeyCode) <> 9 And Asc(e.KeyCode) <> 13 Then
            If FirstInput = Nothing Then
                FirstInput = Now()
            Else
                TimeDiff = DateDiff(DateInterval.Second, FirstInput, Now())
            End If

            If TimeDiff > 0.5 Then
                MsgBox("Please scan in using the attached scanner", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                txtEmp.Text = ""
                FirstInput = Nothing
            End If

        End If
    End Sub

    Private Sub txtEmp_LostFocus(sender As Object, e As EventArgs) Handles txtEmp.LostFocus
        FirstInput = Nothing
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles cmdEmpCancel.Click
        txtEmp2.Text = ""
        pnlEmp.Visible = False
    End Sub

    Private Sub ShowDetails()
        Dim intIssPcsT As Integer
        Dim dblIssCtsT As Double

        intIssPcsT = 0
        dblIssCtsT = 0
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT SUM(BalPcs) AS TotPcs,SUM(BalCts) AS TotCts FROM VW_MixEmpBalEmp WHERE Assortment = '" & txtAssortment.Text & "' AND EmpNo2 = '" & txtEmp.Text & "'", AdoCN, 1, 1)
        If Not IsDBNull(rsComSql_1.Fields("TotPcs").Value) Then
            intIssPcsT = rsComSql_1.Fields("TotPcs").Value
            dblIssCtsT = rsComSql_1.Fields("TotCts").Value
            dblIssCtsT = Math.Round(dblIssCtsT, 3)
        End If
        rsComSql_1 = Nothing

        txtIssPcs.Text = intIssPcsT
        txtIssCts.Text = dblIssCtsT

        txtPcs.Focus()
    End Sub

    Private Sub txtEmp2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEmp2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If CheckEmployee(Trim(txtEmp2.Text)) = True Then
                Datavalid = True
                txtEmp.Text = UCase(Trim(txtEmp2.Text))
            Else
                MsgBox("Invalid IC No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Datavalid = False
                txtEmp.Text = ""
                txtEmp2.Focus()
                Exit Sub
            End If
            txtEmp.Text = txtEmp2.Text
            pnlEmp.Visible = False

            ShowDetails()
        End If
    End Sub

    Private Sub txtEmp2_KeyUp(sender As Object, e As KeyEventArgs) Handles txtEmp2.KeyUp
        Dim TimeDiff As Integer
        Dim TimeDiff1 As TimeSpan
        
        If Asc(e.KeyCode) <> 9 And Asc(e.KeyCode) <> 13 Then
            If FirstInput = Nothing Then
                FirstInput = Now()
            Else
                'TimeDiff = DateDiff(DateInterval.Second, FirstInput, Now())
                TimeDiff1 = Now() - FirstInput
                TimeDiff = TimeDiff1.Milliseconds
            End If

            If TimeDiff > 600 Then
                MsgBox("Please use the Barcode scanner", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                txtEmp.Text = ""
                FirstInput = Nothing
                pnlEmp.Visible = False
                cmdEmp.Focus()
            End If

        End If
    End Sub

    Private Sub cmbEmp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEmp.SelectedIndexChanged
        If optIssue.Checked = True Then
            Load_Request()
        Else
            If optReturn.Checked = True Then
                Load_Issues()
            End If
        End If
    End Sub

    Private Sub Load_Request()
        Dim intTotPcs As Integer
        Dim dblTotCts As Double
        Dim intIssPcsT As Integer
        Dim dblIssCtsT As Double

        If Len(cmbAssortCode.Text) = 0 Then Exit Sub
        If Len(cmbEmp.Text) = 0 Then Exit Sub

        flxDetails.Rows.Clear()

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixEmpIssuesReq.ID, dbo.tblMixEmpIssuesReq.Assortment, dbo.tblMixEmpIssuesReq.ReqPcs, dbo.tblMixEmpIssuesReq.EmpNo," & _
                        "dbo.tblMixEmpIssuesReq.ReqDate, dbo.tblMixEmpIssuesReq.ReqTime, dbo.tblMixEmpIssuesReq.Status, dbo.tblAssortCode.Category " & _
                      "FROM dbo.tblMixEmpIssuesReq INNER JOIN dbo.tblAssortCode ON LEFT(dbo.tblMixEmpIssuesReq.Assortment, 3) = dbo.tblAssortCode.AssortCode " & _
                      "WHERE (dbo.tblMixEmpIssuesReq.Status = 0) AND (dbo.tblMixEmpIssuesReq.EmpNo = '" & cmbEmp.Text & "') AND (dbo.tblAssortCode.Category = '" & cmbAssortCode.Text & "') " & _
                      "ORDER BY dbo.tblMixEmpIssuesReq.Assortment", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                intTotPcs = 0
                dblTotCts = 0
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT SUM(InCts - OutCts) AS Cts, SUM(InPcs - OutPcs) AS PCs " & _
                                "FROM VW_MixAssortInOutNew " & _
                                "WHERE Assortment = '" & rsComSql.Fields("Assortment").Value & "'", AdoCN, 1, 1)
                If Not IsDBNull(rsComSql_1.Fields("PCs").Value) Then
                    intTotPcs = rsComSql_1.Fields("PCs").Value
                    dblTotCts = rsComSql_1.Fields("Cts").Value
                    dblTotCts = Math.Round(dblTotCts, 3)
                End If
                rsComSql_1 = Nothing

                intIssPcsT = 0
                dblIssCtsT = 0
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT SUM(BalPcs) AS TotPcs,SUM(BalCts) AS TotCts FROM VW_MixEmpBal WHERE Assortment = '" & rsComSql.Fields("Assortment").Value & "'", AdoCN, 1, 1)
                If Not IsDBNull(rsComSql_1.Fields("TotPcs").Value) Then
                    intIssPcsT = rsComSql_1.Fields("TotPcs").Value
                    dblIssCtsT = rsComSql_1.Fields("TotCts").Value
                    dblIssCtsT = Math.Round(dblIssCtsT, 3)
                End If
                rsComSql_1 = Nothing

                flxDetails.Rows.Add(rsComSql.Fields("Assortment").Value,
                                    rsComSql.Fields("ReqPcs").Value,
                                    dblTotCts - dblIssCtsT,
                                    intTotPcs - intIssPcsT,
                                    dblTotCts - dblIssCtsT,
                                    False,
                                    rsComSql.Fields("ID").Value,
                                    Format(rsComSql.Fields("ReqDate").Value, "yyyy/MM/dd"))
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

    End Sub

    Private Sub Load_Issues()
        If Len(cmbAssortCode.Text) = 0 Then Exit Sub
        If Len(cmbEmp.Text) = 0 Then Exit Sub

        flxDetails.Rows.Clear()

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.VW_MixEmpBalEmp.Assortment, dbo.VW_MixEmpBalEmp.BalPcs, ROUND(dbo.VW_MixEmpBalEmp.BalCts, 3) AS BalCts " & _
                      "FROM dbo.VW_MixEmpBalEmp INNER JOIN dbo.tblAssortCode ON LEFT(dbo.VW_MixEmpBalEmp.Assortment, 3) = dbo.tblAssortCode.AssortCode " & _
                      "WHERE (dbo.VW_MixEmpBalEmp.EmpNo2 = '" & cmbEmp.Text & "') AND (dbo.tblAssortCode.Category = '" & cmbAssortCode.Text & "') " & _
                      "ORDER BY dbo.VW_MixEmpBalEmp.Assortment", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("Assortment").Value,
                                    rsComSql.Fields("BalPcs").Value,
                                    rsComSql.Fields("BalCts").Value,
                                    "0",
                                    "0",
                                    False,
                                    "0")

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(5).EditedFormattedValue = True Then
                CalTotalPcs = CalTotalPcs + Val(flxSample.Item(intCalRow, intRow).Value)
            End If
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(5).EditedFormattedValue = True Then
                CalTotalCts = CalTotalCts + Val(flxSample.Item(intCalRow, intRow).Value)
            End If
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(5, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(5, intRow).Value = False
            Next
        End If
        txtSelPcs.Text = CalTotalPcs(flxDetails, 1)
        txtSelCts.Text = CalTotalCts(flxDetails, 2)
    End Sub

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 5 Then
            txtSelPcs.Text = CalTotalPcs(flxDetails, 1)
            txtSelCts.Text = CalTotalCts(flxDetails, 2)
        End If
    End Sub

    Private Sub cmdSave2_Click(sender As Object, e As EventArgs) Handles cmdSave2.Click
        SaveAll()
    End Sub

    Private Sub cmbAssortCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAssortCode.SelectedIndexChanged
        If optIssue.Checked = True Then
            Load_Request()
        Else
            If optReturn.Checked = True Then
                Load_Issues()
            End If
        End If
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub cmdSticker_Click(sender As Object, e As EventArgs) Handles cmdSticker.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixEmpIssueReq.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button2_Click_1(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixEmpStockRec.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub
End Class