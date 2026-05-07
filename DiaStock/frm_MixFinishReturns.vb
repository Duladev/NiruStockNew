
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixFinishReturns

    Private Sub ClearFields()
        flxDetails.Rows.Clear()
        txtIssPcs.Text = ""
        txtEmp.Text = ""
        txtCount.Text = ""
        chkSelect.Checked = False
        chkByPass.Checked = False
        chkIssue.Checked = False
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
        Dim intDiffDays As Integer

        ClearFields()

        rsComSql = New ADODB.Recordset
        If chkGroove.Checked = True Then
            If Len(txtOrder.Text) = 0 Then
                rsComSql.Open("SELECT * FROM VW_MixFinishReturnsForNew WHERE Groove = 1 ORDER BY ParNo, PktNo", AdoCN, 1, 1)
            Else
                rsComSql.Open("SELECT * FROM VW_MixFinishReturnsForNew WHERE ParNo = '" & txtOrder.Text & "' AND Groove = 1 ORDER BY ParNo, PktNo", AdoCN, 1, 1)
            End If
        Else
            If Len(txtOrder.Text) = 0 Then
                rsComSql.Open("SELECT * FROM VW_MixFinishReturnsForNew ORDER BY ParNo, PktNo", AdoCN, 1, 1)
            Else
                rsComSql.Open("SELECT * FROM VW_MixFinishReturnsForNew WHERE ParNo = '" & txtOrder.Text & "' ORDER BY ParNo, PktNo", AdoCN, 1, 1)
            End If
        End If
        
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                intDiffDays = DateDiff(DateInterval.Day, CDate(Format(rsComSql.Fields("IssDate").Value, "yyyy/MM/dd")), CDate(Format(Date.Now, "yyyy/MM/dd")))
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM VW_EMP_HOLIDAYS WHERE HDate >= '" & Format(rsComSql.Fields("IssDate").Value, "MM/dd/yyyy") & "' AND HDate <= '" & Format(Date.Now, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
                intDiffDays = intDiffDays - rsComSql_1.RecordCount
                rsComSql_1 = Nothing

                flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("IssPcs").Value,
                                    rsComSql.Fields("IssCts").Value,
                                    rsComSql.Fields("IssPcs").Value - rsComSql.Fields("RetPcs").Value,
                                    Math.Round(rsComSql.Fields("IssCts").Value - rsComSql.Fields("RetCts").Value, 3),
                                    rsComSql.Fields("IssPcs").Value - rsComSql.Fields("RetPcs").Value,
                                    Math.Round(rsComSql.Fields("IssCts").Value - rsComSql.Fields("RetCts").Value, 3),
                                    rsComSql.Fields("PktFlow").Value,
                                    rsComSql.Fields("EmpNo").Value,
                                    False, "0", "0",
                                    IIf(rsComSql.Fields("Groove").Value = 1, "GR", ""),
                                    IIf(rsComSql.Fields("Laser").Value = 1, "LS", ""),
                                    intDiffDays,
                                    rsComSql.Fields("Grp").Value,
                                    Format(rsComSql.Fields("IssDate").Value, "yyyy/MM/dd"))

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

    End Sub

    Private Function CalTotalPcs() As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Rows(intRow).Cells(10).EditedFormattedValue = True Then
                CalTotalPcs = CalTotalPcs + CInt(flxDetails.Item(4, intRow).Value)
            End If
        Next
        Return CalTotalPcs
    End Function

    Private Function CalTotalCount() As Integer
        Dim intRow As Integer

        CalTotalCount = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Rows(intRow).Cells(10).EditedFormattedValue = True Then
                CalTotalCount = CalTotalCount + 1
            End If
        Next
        Return CalTotalCount
    End Function

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        txtOrder.Text = ""
        txtIssPcs.Text = ""
        txtCount.Text = ""
        chkDelay.Checked = False
        Load_Details()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
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

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean

        Dim intRetPcs As Integer
        Dim dblRetCts As Double

        Dim intSecCount As Integer
        Dim dtpIssDate14 As Date
        Dim dblRepPcs As Double
        Dim dblRetPcs As Double
        Dim dblIssPcs As Double
        Dim dblOKPcs As Double
        Dim dblNoPayPcs As Double
        Dim dblDiffHours As Double
        Dim intDiffDays As Integer

        If txtEmp.Text = "" Then MsgBox("Invalid Employee No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If chkByPass.Checked = True Then
            If txtEmp2.Text = "" Then MsgBox("Invalid Employee No. 2", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        End If
        If chkIssue.Checked = True Then
            If txtEmp2.Text = "" Then MsgBox("Invalid Employee No. 2", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        End If
        If cmbEmpNo.Text = "" Then MsgBox("Invalid Checking Employee No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        blnSave = False
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(10, intRow).Value = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT TOP (100) PERCENT dbo.VW_MIXFinishIss.ParNo, dbo.VW_MIXFinishIss.PktNo, dbo.VW_MIXFinishIss.EmpNo," & _
                                "dbo.VW_MIXFinishIss.IssPcsT + dbo.VW_MIXFinishIss.IssPcsB AS IssPcs, dbo.VW_MIXFinishIss.RetPcsT + dbo.VW_MIXFinishIss.RetPcsB AS RetPcs, " & _
                                "dbo.VW_MIXFinishIss.IssCts, dbo.VW_MIXFinishIss.RetCts, dbo.tblMixPacket.PktFlow " & _
                              "FROM dbo.VW_MIXFinishIss INNER JOIN dbo.tblMixPacket ON dbo.VW_MIXFinishIss.ParNo = dbo.tblMixPacket.PktOrdNo AND dbo.VW_MIXFinishIss.PktNo = dbo.tblMixPacket.PktNo " & _
                              "WHERE (dbo.VW_MIXFinishIss.ParNo = '" & flxDetails.Item(0, intRow).Value & "') AND (dbo.VW_MIXFinishIss.PktNo = '" & flxDetails.Item(1, intRow).Value & "') AND " & _
                                "((dbo.VW_MIXFinishIss.IssPcsT + dbo.VW_MIXFinishIss.IssPcsB) - (dbo.VW_MIXFinishIss.RetPcsT + dbo.VW_MIXFinishIss.RetPcsB) = 0) " & _
                              "ORDER BY dbo.VW_MIXFinishIss.ParNo, dbo.VW_MIXFinishIss.PktNo", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    MsgBox("Already Returned - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT dbo.tblMixIssuesRep.ParNo, dbo.tblMixIssuesRep.PktNo, dbo.tblMixIssuesRep.Sec, dbo.tblMixIssuesRep.IssPcs - ISNULL(dbo.tblMixReturnsRep.RetPcs, 0) AS BalPcs, " & _
                                "dbo.tblMixIssuesRep.EmpNo, dbo.tblMixIssuesRep.IssDate, dbo.tblMixIssuesRep.IssTime " & _
                              "FROM dbo.tblMixIssuesRep LEFT OUTER JOIN dbo.tblMixReturnsRep ON dbo.tblMixIssuesRep.ID = dbo.tblMixReturnsRep.IssueID AND dbo.tblMixIssuesRep.ParNo = dbo.tblMixReturnsRep.ParNo AND " & _
                                "dbo.tblMixIssuesRep.PktNo = dbo.tblMixReturnsRep.PktNo AND dbo.tblMixIssuesRep.Sec = dbo.tblMixReturnsRep.Sec " & _
                              "WHERE (dbo.tblMixIssuesRep.ParNo = '" & flxDetails.Item(0, intRow).Value & "') AND (dbo.tblMixIssuesRep.PktNo = '" & flxDetails.Item(1, intRow).Value & "') AND (dbo.tblMixIssuesRep.IssPcs - ISNULL(dbo.tblMixReturnsRep.RetPcs, 0) > 0)", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("BalPcs").Value) Then
                        If rsComSql.Fields("BalPcs").Value > 0 Then
                            MsgBox("Please Complete the Repair Process - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
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

                If Len(flxDetails.Item(11, intRow).Value) = 0 Then
                    MsgBox("Invalid Rep Pcs - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If IsNumeric(flxDetails.Item(11, intRow).Value) = False Then
                    MsgBox("Invalid Rep Pcs - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If CInt(flxDetails.Item(11, intRow).Value) < 0 Then
                    MsgBox("Invalid Rep Pcs - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If CInt(flxDetails.Item(11, intRow).Value) > CInt(flxDetails.Item(6, intRow).Value) Then
                    MsgBox("Invalid Rep Pcs - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If Len(flxDetails.Item(12, intRow).Value) = 0 Then
                    MsgBox("Invalid No Pay Pcs - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If IsNumeric(flxDetails.Item(12, intRow).Value) = False Then
                    MsgBox("Invalid No Pay Pcs - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If CInt(flxDetails.Item(12, intRow).Value) < 0 Then
                    MsgBox("Invalid No Pay Pcs - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If Len(flxDetails.Item(13, intRow).Value) > 0 And chkByPass.Checked = True Then
                    MsgBox("Invalid Packet (Grooving) - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If chkByPass.Checked = True Then
                    rsComSql = New ADODB.Recordset
                    mStrSQL = "SELECT TOP (100) PERCENT dbo.tblMixIssues.IssPcsT, dbo.tblMixIssues.IssPcsB, SUM(ISNULL(dbo.tblMixReturns.RetPcsT, 0)) AS RetPcsT, " & _
                                "SUM(ISNULL(dbo.tblMixReturns.RetPcsB, 0)) AS RetPcsB, SUM(ISNULL(dbo.tblMixReturns.RejPcs, 0)) AS RejPcs, SUM(ISNULL(dbo.tblMixReturns.BroPcs, 0)) AS BroPcs, " & _
                                "SUM(ISNULL(dbo.tblMixReturns.LostPcs, 0)) AS LostPcs, SUM(ISNULL(dbo.tblMixReturns.ExtPcs, 0)) AS ExtPcs " & _
                              "FROM dbo.tblMixIssues LEFT OUTER JOIN dbo.tblMixReturns ON dbo.tblMixIssues.PktNo = dbo.tblMixReturns.PktNo AND dbo.tblMixIssues.ParNo = dbo.tblMixReturns.ParNo AND " & _
                                "dbo.tblMixIssues.Sec = dbo.tblMixReturns.Sec " & _
                              "WHERE (dbo.tblMixIssues.ParNo = '" & flxDetails.Item(0, intRow).Value & "') AND (dbo.tblMixIssues.PktNo = '" & flxDetails.Item(1, intRow).Value & "') AND (dbo.tblMixIssues.Sec = 14) " & _
                              "GROUP BY dbo.tblMixIssues.IssPcsT, dbo.tblMixIssues.IssPcsB"
                    rsComSql.Open(mStrSQL, AdoCN, 1, 1)
                    If rsComSql.Fields("IssPcsT").Value + rsComSql.Fields("IssPcsB").Value <> CInt(flxDetails.Item(6, intRow).Value) + rsComSql.Fields("RetPcsT").Value + rsComSql.Fields("RetPcsB").Value + rsComSql.Fields("RejPcs").Value + rsComSql.Fields("LostPcs").Value + rsComSql.Fields("BroPcs").Value - rsComSql.Fields("ExtPcs").Value Then
                        MsgBox("Packet not Returned full - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql = Nothing

                    If flxDetails.Item(13, intRow).Value = "GR" Then
                        MsgBox("Packet is for Grooving - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If
            End If
        Next

        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(10, intRow).Value = True Then
                intSecCount = 0

                If flxDetails.Item(8, intRow).Value = "Polish" Then
                    intSecCount = 7
                Else
                    intSecCount = 11
                End If

                blnSave = True
                AdoCN.Execute("INSERT INTO tblMixReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,RejStatus,RejReason,DoneBy,ChkEmpNo) " & _
                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(8, intRow).Value & "'," & intSecCount & ",14,'" & txtEmp.Text & "',0,'" & CInt(flxDetails.Item(6, intRow).Value) & "'," & _
                                "'" & CDbl(flxDetails.Item(7, intRow).Value) & "',0,0,0,0,0,'" & CInt(flxDetails.Item(11, intRow).Value) & "','" & CInt(flxDetails.Item(12, intRow).Value) & "','" & Format(Date.Now, "MM/dd/yyyy") & "'," & _
                                "'" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,'-','" & PBUser_EmpNo & "','" & cmbEmpNo.Text & "')")

                dblRepPcs = 0
                dblRetPcs = 0
                dblDiffHours = 0
                dblOKPcs = 0
                dblNoPayPcs = 0
                dblIssPcs = 0

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(RepPcs) AS RepPcs, SUM(RetPcsT + RetPcsB) AS RetPcs FROM tblMixReturns WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = 14", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("RepPcs").Value) Then
                        dblRepPcs = rsComSql.Fields("RepPcs").Value
                        dblRetPcs = rsComSql.Fields("RetPcs").Value
                    End If
                End If
                rsComSql = Nothing

                If chkDelay.Checked = True Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT CAST(IssDate as DATETIME) + CAST(IssTime AS DATETIME) AS DateTime1 FROM tblMixIssues WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = 14", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        dtpIssDate14 = rsComSql.Fields("DateTime1").Value
                    End If
                    rsComSql = Nothing

                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT CAST(SendDate as DATETIME) + CAST(SendTime AS DATETIME) AS DateTime1 FROM tblMixIssuesRep WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = 1 ORDER BY ID", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        rsComSql.MoveFirst()
                        dtpIssDate14 = rsComSql.Fields("DateTime1").Value
                    End If
                    rsComSql = Nothing

                    dblDiffHours = DateDiff(DateInterval.Hour, dtpIssDate14, Date.Now)
                    If dblDiffHours > 54 Then
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM VW_EMP_HOLIDAYS WHERE HDate >= '" & Format(dtpIssDate14, "MM/dd/yyyy") & "' AND HDate <= '" & Format(Date.Now, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
                        dblDiffHours = dblDiffHours - (24 * rsComSql_1.RecordCount)
                        rsComSql_1 = Nothing

                        If dblDiffHours > 54 Then
                            dblOKPcs = CDbl(flxDetails.Item(6, intRow).Value)
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT * FROM tblMixReturns WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec < 14 AND RetPcsT + RetPcsB > 0 ORDER BY ID", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount Then
                                rsComSql_1.MoveFirst()
                                While Not rsComSql_1.EOF
                                    rsComSql_2 = New ADODB.Recordset
                                    rsComSql_2.Open("SELECT IssPcsT + IssPcsB AS IssPcs FROM tblMixIssues WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = " & rsComSql_1.Fields("Sec").Value & "", AdoCN, 1, 1)
                                    If rsComSql_2.RecordCount Then
                                        dblIssPcs = rsComSql_2.Fields("IssPcs").Value
                                    End If
                                    rsComSql_2 = Nothing

                                    dblNoPayPcs = Math.Round(((rsComSql_1.Fields("RetPcsT").Value + rsComSql_1.Fields("RetPcsB").Value) / dblIssPcs) * dblOKPcs, 1)

                                    AdoCN.Execute("UPDATE tblMixReturns SET NopayPcs1 = NopayPcs1 + " & dblNoPayPcs & " WHERE ID = " & rsComSql_1.Fields("ID").Value & "")
                                    rsComSql_1.MoveNext()
                                End While
                            End If
                            rsComSql_1 = Nothing
                        End If
                    End If

                    intDiffDays = DateDiff(DateInterval.Day, CDate(Format(dtpIssDate14, "yyyy/MM/dd")), CDate(Format(Date.Now, "yyyy/MM/dd")))
                    dblDiffHours = DateDiff(DateInterval.Hour, dtpIssDate14, Date.Now)
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM VW_EMP_HOLIDAYS WHERE HDate >= '" & Format(dtpIssDate14, "MM/dd/yyyy") & "' AND HDate <= '" & Format(Date.Now, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
                    intDiffDays = intDiffDays - rsComSql_1.RecordCount
                    dblDiffHours = dblDiffHours - (24 * rsComSql_1.RecordCount)
                    rsComSql_1 = Nothing

                    If dblDiffHours >= 48 Then
                        AdoCN.Execute("UPDATE dbo.tblMixPacket SET NewGrp = 'D' WHERE (PktOrdNo = '" & flxDetails.Item(0, intRow).Value & "') AND (PktNo = '" & flxDetails.Item(1, intRow).Value & "')")
                    End If
                End If

                If chkByPass.Checked = True Then
                    intRetPcs = 0
                    dblRetCts = 0

                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT TOP (100) PERCENT SUM(RetPcsT + RetPcsB) AS RetPcs, ROUND(SUM(RetCts), 3) AS RetCts FROM dbo.tblMixReturns WHERE (ParNo = '" & flxDetails.Item(0, intRow).Value & "') AND (PktNo = '" & flxDetails.Item(1, intRow).Value & "') AND (Sec = 14)", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If Not IsDBNull(rsComSql.Fields("RetPcs").Value) Then
                            intRetPcs = rsComSql.Fields("RetPcs").Value
                            dblRetCts = rsComSql.Fields("RetCts").Value
                        End If
                    End If
                    rsComSql = Nothing

                    If intRetPcs > 0 Then
                        If flxDetails.Item(8, intRow).Value = "Polish" Then
                            intSecCount = 8
                        Else
                            intSecCount = 12
                        End If

                        '15
                        AdoCN.Execute("INSERT INTO tblMixIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                      "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "',15,'" & flxDetails.Item(8, intRow).Value & "'," & intSecCount & ",'" & txtEmp.Text & "',0,'" & intRetPcs & "'," & _
                                        "'" & dblRetCts & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "')")

                        AdoCN.Execute("INSERT INTO tblMixReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,StDate,RejStatus,RejReason,DoneBy) " & _
                                           "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(8, intRow).Value & "'," & intSecCount & ",15,'" & txtEmp.Text & "',0,'" & intRetPcs & "'," & _
                                             "'" & dblRetCts & "',0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "',0,'-','" & PBUser_EmpNo & "')")

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
                If chkIssue.Checked = True Then
                    intRetPcs = 0
                    dblRetCts = 0

                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT TOP (100) PERCENT SUM(RetPcsT + RetPcsB) AS RetPcs, ROUND(SUM(RetCts), 3) AS RetCts FROM dbo.tblMixReturns WHERE (ParNo = '" & flxDetails.Item(0, intRow).Value & "') AND (PktNo = '" & flxDetails.Item(1, intRow).Value & "') AND (Sec = 14)", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If Not IsDBNull(rsComSql.Fields("RetPcs").Value) Then
                            intRetPcs = rsComSql.Fields("RetPcs").Value
                            dblRetCts = rsComSql.Fields("RetCts").Value
                        End If
                    End If
                    rsComSql = Nothing

                    If intRetPcs > 0 Then
                        If flxDetails.Item(8, intRow).Value = "Polish" Then
                            intSecCount = 8
                        Else
                            intSecCount = 12
                        End If

                        '15
                        AdoCN.Execute("INSERT INTO tblMixIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,DoneBy) " & _
                                      "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "',15,'" & flxDetails.Item(8, intRow).Value & "'," & intSecCount & ",'" & txtEmp2.Text & "',0,'" & intRetPcs & "'," & _
                                        "'" & dblRetCts & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "')")
                    End If
                End If
            End If
        Next

        If blnSave = True Then
            MsgBox("Finish Return Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
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
                flxDetails.Item(10, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(10, intRow).Value = False
            Next
        End If
        txtIssPcs.Text = CalTotalPcs()
        txtCount.Text = CalTotalCount()
    End Sub

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 10 Then
            txtIssPcs.Text = CalTotalPcs()
            txtCount.Text = CalTotalCount()
        End If
    End Sub

    Private Sub frm_MixFinishReturns_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_EmpNo()
    End Sub

    Private Sub txtOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrder.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Load_Details()
        End If
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
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

    Private Sub chkByPass_CheckedChanged(sender As Object) Handles chkByPass.CheckedChanged
        If chkByPass.Checked = True Then
            chkIssue.Checked = False
        End If
    End Sub

    Private Sub chkIssue_CheckedChanged(sender As Object) Handles chkIssue.CheckedChanged
        If chkIssue.Checked = True Then
            chkByPass.Checked = False
        End If
    End Sub
End Class