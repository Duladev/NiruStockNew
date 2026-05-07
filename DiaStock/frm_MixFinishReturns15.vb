
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixFinishReturns15

    Private Sub ClearFields()
        flxDetails.Rows.Clear()
        txtIssPcs.Text = ""
        txtEmp.Text = ""
        txtCount.Text = ""
        chkSelect.Checked = False
        cmbEmpNo.Text = ""
    End Sub

    Private Sub cmdEmp_Click(sender As Object, e As EventArgs) Handles cmdEmp.Click
        Datavalid = False
        Parcel = False
        Instring = UCase(InputBox("Enter Emp No"))
        ParcelLen = Len(Instring)
        If ParcelLen = 6 Then
            Datavalid = True

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL2 WHERE (Pay = 1) AND (FullEmpNo = '" & Trim(Instring) & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                cmdEmp.Focus()
                Exit Sub
            End If
            rsComSql = Nothing
            ICNo = UCase(Trim(Instring))
            txtEmp.Text = ICNo
        Else
            MsgBox("Invalid IC No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Datavalid = False
            ICNo = ""
            cmdEmp.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub Load_Details()
        ClearFields()

        rsComSql = New ADODB.Recordset
        If Len(txtOrder.Text) = 0 Then
            rsComSql.Open("SELECT * FROM dbo.VW_MixPending15 ORDER BY ParNo, PktNo", AdoCN, 1, 1)
        Else
            rsComSql.Open("SELECT * FROM dbo.VW_MixPending15 WHERE ParNo = '" & txtOrder.Text & "' ORDER BY ParNo, PktNo", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("IssPcs").Value,
                                    rsComSql.Fields("IssCts").Value,
                                    rsComSql.Fields("IssPcs").Value - rsComSql.Fields("RetPcs").Value - rsComSql.Fields("RejPcs").Value,
                                    Math.Round(rsComSql.Fields("IssCts").Value - rsComSql.Fields("RetCts").Value - rsComSql.Fields("RejCts").Value, 3),
                                    rsComSql.Fields("IssPcs").Value - rsComSql.Fields("RetPcs").Value - rsComSql.Fields("RejPcs").Value,
                                    Math.Round(rsComSql.Fields("IssCts").Value - rsComSql.Fields("RetCts").Value - rsComSql.Fields("RejCts").Value, 3),
                                    rsComSql.Fields("PktFlow").Value,
                                    False, "0", "0", "0",
                                    rsComSql.Fields("IssPcs").Value - rsComSql.Fields("RetPcs").Value - rsComSql.Fields("RejPcs").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

    End Sub

    Private Function CalTotalPcs() As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Rows(intRow).Cells(9).EditedFormattedValue = True Then
                CalTotalPcs = CalTotalPcs + CInt(flxDetails.Item(4, intRow).Value)
            End If
        Next
        Return CalTotalPcs
    End Function

    Private Function CalTotalCount() As Integer
        Dim intRow As Integer

        CalTotalCount = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Rows(intRow).Cells(9).EditedFormattedValue = True Then
                CalTotalCount = CalTotalCount + 1
            End If
        Next
        Return CalTotalCount
    End Function

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        txtOrder.Text = ""
        txtIssPcs.Text = ""
        txtCount.Text = ""
        Load_Details()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean

        Dim intRetPcs As Integer
        Dim dblRetCts As Double

        Dim intSecCount As Integer
        Dim intGroove As Integer

        If txtEmp.Text = "" Then MsgBox("Invalid Employee No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If chkByPass.Checked = True Then
            If txtEmp2.Text = "" Then MsgBox("Invalid Employee No. 2", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        End If
        If cmbEmpNo.Text = "" Then MsgBox("Invalid Checking Employee No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        blnSave = False
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(9, intRow).Value = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * " & _
                              "FROM dbo.VW_MixPending15 " & _
                              "WHERE (ParNo = '" & flxDetails.Item(0, intRow).Value & "') AND (PktNo = '" & flxDetails.Item(1, intRow).Value & "') AND " & _
                                "(IssPcs - (RetPcs + RejPcs + LostPcs) = 0) ", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    MsgBox("Already Returned - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT dbo.tblMixIssuesBoil.ParNo, dbo.tblMixIssuesBoil.PktNo, dbo.tblMixIssuesBoil.Sec, dbo.tblMixIssuesBoil.IssPcs - ISNULL(dbo.tblMixReturnsBoil.RetPcs, 0) AS BalPcs, " & _
                                "dbo.tblMixIssuesBoil.EmpNo, dbo.tblMixIssuesBoil.IssDate, dbo.tblMixIssuesBoil.IssTime " & _
                              "FROM dbo.tblMixIssuesBoil LEFT OUTER JOIN dbo.tblMixReturnsBoil ON dbo.tblMixIssuesBoil.ID = dbo.tblMixReturnsBoil.IssueID AND dbo.tblMixIssuesBoil.ParNo = dbo.tblMixReturnsBoil.ParNo AND " & _
                                "dbo.tblMixIssuesBoil.PktNo = dbo.tblMixReturnsBoil.PktNo AND dbo.tblMixIssuesBoil.Sec = dbo.tblMixReturnsBoil.Sec " & _
                              "WHERE (dbo.tblMixIssuesBoil.ParNo = '" & flxDetails.Item(0, intRow).Value & "') AND (dbo.tblMixIssuesBoil.PktNo = '" & flxDetails.Item(1, intRow).Value & "') AND (dbo.tblMixIssuesBoil.IssPcs - ISNULL(dbo.tblMixReturnsBoil.RetPcs, 0) > 0)", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("BalPcs").Value) Then
                        If rsComSql.Fields("BalPcs").Value > 0 Then
                            MsgBox("Please Complete the Boiling Process - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                    End If
                End If
                rsComSql = Nothing

                If Len(flxDetails.Item(6, intRow).Value) = 0 Then
                    MsgBox("Invalid Pcs - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If IsNumeric(flxDetails.Item(6, intRow).Value) = False Then
                    MsgBox("Invalid Pcs - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If CInt(flxDetails.Item(6, intRow).Value) <= 0 Then
                    MsgBox("Invalid Pcs - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If CInt(flxDetails.Item(6, intRow).Value) > CInt(flxDetails.Item(4, intRow).Value) Then
                    MsgBox("Invalid Pcs - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If Len(flxDetails.Item(7, intRow).Value) = 0 Then
                    MsgBox("Invalid Cts - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If IsNumeric(flxDetails.Item(7, intRow).Value) = False Then
                    MsgBox("Invalid Cts - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If CDbl(flxDetails.Item(7, intRow).Value) <= 0 Then
                    MsgBox("Invalid Cts - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If CDbl(flxDetails.Item(7, intRow).Value) > CDbl(flxDetails.Item(5, intRow).Value) Then
                    MsgBox("Invalid Cts - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If Len(flxDetails.Item(10, intRow).Value) = 0 Then
                    MsgBox("Invalid Rep Pcs - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If IsNumeric(flxDetails.Item(10, intRow).Value) = False Then
                    MsgBox("Invalid Rep Pcs - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If CInt(flxDetails.Item(10, intRow).Value) < 0 Then
                    MsgBox("Invalid Rep Pcs - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If CInt(flxDetails.Item(11, intRow).Value) > CInt(flxDetails.Item(6, intRow).Value) Then
                    MsgBox("Invalid Rep Pcs - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If Len(flxDetails.Item(11, intRow).Value) = 0 Then
                    MsgBox("Invalid No Pay Pcs - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If IsNumeric(flxDetails.Item(11, intRow).Value) = False Then
                    MsgBox("Invalid No Pay Pcs - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If CInt(flxDetails.Item(11, intRow).Value) < 0 Then
                    MsgBox("Invalid No Pay Pcs - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If Len(flxDetails.Item(12, intRow).Value) = 0 Then
                    MsgBox("Invalid Groove Height - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If IsNumeric(flxDetails.Item(12, intRow).Value) = False Then
                    MsgBox("Invalid Groove Height - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If Len(flxDetails.Item(13, intRow).Value) = 0 Then
                    MsgBox("Invalid Groove Pcs - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If IsNumeric(flxDetails.Item(13, intRow).Value) = False Then
                    MsgBox("Invalid Groove Pcs - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If CInt(flxDetails.Item(13, intRow).Value) < 0 Then
                    MsgBox("Invalid Groove Pcs - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If CInt(flxDetails.Item(13, intRow).Value) > CInt(flxDetails.Item(4, intRow).Value) Then
                    MsgBox("Invalid Groove Pcs - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
        Next

        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(9, intRow).Value = True Then
                intSecCount = 0

                If flxDetails.Item(8, intRow).Value = "Polish" Then
                    intSecCount = 8
                Else
                    intSecCount = 12
                End If

                intGroove = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT dbo.tblOrdersDtls.Groove " & _
                              "FROM dbo.tblMixPacket INNER JOIN dbo.tblOrdersDtls ON dbo.tblMixPacket.PktOrdNo = dbo.tblOrdersDtls.OrderNo AND dbo.tblMixPacket.PktRefNo = dbo.tblOrdersDtls.RefNo AND " & _
                                "dbo.tblMixPacket.Pktside = dbo.tblOrdersDtls.Side " & _
                              "WHERE (dbo.tblMixPacket.PktOrdNo = '" & flxDetails.Item(0, intRow).Value & "') AND (dbo.tblMixPacket.PktNo = '" & flxDetails.Item(1, intRow).Value & "') AND (dbo.tblOrdersDtls.Groove = 1) ", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intGroove = 1
                End If
                rsComSql = Nothing

                blnSave = True
                AdoCN.Execute("INSERT INTO tblMixReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,RejStatus,RejReason,DoneBy,GrPcs,Groove,ChkEmpNo) " & _
                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(8, intRow).Value & "'," & intSecCount & ",15,'" & txtEmp.Text & "',0,'" & CInt(flxDetails.Item(6, intRow).Value) & "'," & _
                                "'" & CDbl(flxDetails.Item(7, intRow).Value) & "',0,0,0,0,0,'" & CInt(flxDetails.Item(10, intRow).Value) & "','" & CInt(flxDetails.Item(11, intRow).Value) & "','" & Format(Date.Now, "MM/dd/yyyy") & "'," & _
                                "'" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,'-','" & PBUser_EmpNo & "','" & CInt(flxDetails.Item(13, intRow).Value) & "'," & intGroove & ",'" & cmbEmpNo.Text & "')")

                mStrSQL = "UPDATE tblMixPacket SET GrHeight = " & CDbl(flxDetails.Item(12, intRow).Value) & " WHERE (PktOrdNo = '" & flxDetails.Item(0, intRow).Value & "') AND (PktNo = '" & flxDetails.Item(1, intRow).Value & "')"
                AdoCN.Execute(mStrSQL)

                If chkByPass.Checked = True Then
                    intRetPcs = 0
                    dblRetCts = 0

                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT TOP (100) PERCENT SUM(RetPcsT + RetPcsB) AS RetPcs, ROUND(SUM(RetCts), 3) AS RetCts FROM dbo.tblMixReturns WHERE (ParNo = '" & flxDetails.Item(0, intRow).Value & "') AND (PktNo = '" & flxDetails.Item(1, intRow).Value & "') AND (Sec = 15)", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If Not IsDBNull(rsComSql.Fields("RetPcs").Value) Then
                            intRetPcs = rsComSql.Fields("RetPcs").Value
                            dblRetCts = rsComSql.Fields("RetCts").Value
                        End If
                    End If
                    rsComSql = Nothing

                    If intRetPcs > 0 Then
                        If flxDetails.Item(8, intRow).Value = "Polish" Then
                            intSecCount = 9
                        Else
                            intSecCount = 13
                        End If
                        '16
                        AdoCN.Execute("INSERT INTO tblMixIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                      "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "',16,'" & flxDetails.Item(8, intRow).Value & "'," & intSecCount & ",'" & txtEmp2.Text & "',0,'" & intRetPcs & "'," & _
                                        "'" & dblRetCts & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "')")

                        'AdoCN.Execute("INSERT INTO tblMixReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,RejStatus,RejReason,DoneBy) " & _
                        '              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(8, intRow).Value & "'," & intSecCount & ",16,'" & txtEmp.Text & "',0,'" & intRetPcs & "'," & _
                        '                "'" & dblRetCts & "',0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,'-','" & PBUser_EmpNo & "')")

                        'If flxDetails.Item(8, intRow).Value = "Polish" Then
                        '    intSecCount = 10
                        'Else
                        '    intSecCount = 14
                        'End If
                        ''18
                        'AdoCN.Execute("INSERT INTO tblMixIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                        '              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "',18,'" & flxDetails.Item(8, intRow).Value & "'," & intSecCount & ",'" & txtEmp.Text & "',0,'" & intRetPcs & "'," & _
                        '                "'" & dblRetCts & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "')")

                        'AdoCN.Execute("INSERT INTO tblMixReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,RejStatus,RejReason,DoneBy) " & _
                        '              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(8, intRow).Value & "'," & intSecCount & ",18,'" & txtEmp.Text & "',0,'" & intRetPcs & "'," & _
                        '                "'" & dblRetCts & "',0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,'-','" & PBUser_EmpNo & "')")
                    End If
                End If
            End If
        Next

        If blnSave = True Then
            MsgBox("15 Return Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
        End If

    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            Save()
        End If
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(9, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(9, intRow).Value = False
            Next
        End If
        txtIssPcs.Text = CalTotalPcs()
        txtCount.Text = CalTotalCount()
    End Sub

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 9 Then
            txtIssPcs.Text = CalTotalPcs()
            txtCount.Text = CalTotalCount()
        End If
    End Sub

    Private Sub txtOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrder.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Load_Details()
        End If
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub frm_MixFinishReturns15_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_EmpNo()
    End Sub

    Private Sub Load_EmpNo()

        cmbEmpNo.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblMixEmpNo ORDER BY EmpNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbEmpNo.Items.Add(rsComSql.Fields("EmpNo").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdEmp2_Click(sender As Object, e As EventArgs) Handles cmdEmp2.Click
        Datavalid = False
        Parcel = False
        Instring = UCase(InputBox("Enter Emp No"))
        ParcelLen = Len(Instring)
        If ParcelLen = 6 Then
            Datavalid = True

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL2 WHERE (Pay = 1) AND (FullEmpNo = '" & Trim(Instring) & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                cmdEmp2.Focus()
                Exit Sub
            End If
            rsComSql = Nothing
            ICNo = UCase(Trim(Instring))
            txtEmp2.Text = ICNo
        Else
            MsgBox("Invalid IC No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Datavalid = False
            ICNo = ""
            cmdEmp2.Focus()
            Exit Sub
        End If
    End Sub
End Class