
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDBoilingReturn

    Private Sub ClearFields()
        flxDetails.Rows.Clear()
        txtIssPcs.Text = ""
        txtIssCts.Text = ""
        txtRetPcs.Text = ""
        txtRetCts.Text = ""
        txtEmp.Text = ""
        chkTrf.Checked = False
        txtCount.Text = ""
    End Sub

    Private Sub frm_GRDBoilingReturn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_DepartmentProd(cmbDept)
    End Sub

    Private Sub cmdEmp_Click(sender As Object, e As EventArgs) Handles cmdEmp.Click
        Datavalid = False
        Parcel = False
        Instring = UCase(InputBox("Enter Emp No"))
        ParcelLen = Len(Instring)
        If ParcelLen = 6 Then
            Datavalid = True

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL3 WHERE (Pay = 1) AND (FullEmpNo = '" & Trim(Instring) & "')", AdoCN, 1, 1)
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
        Dim dblIssPcs As Double
        Dim dblIssCts As Double
        Dim strOrderNo As String

        flxDetails.Rows.Clear()

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        dblIssPcs = 0
        dblIssCts = 0
        strOrderNo = ""
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblGrading_BoilingIssues.Department, dbo.tblGrading_BoilingIssues.ParNo, dbo.tblGrading_BoilingIssues.PktNo, " & _
                            "dbo.tblGrading_BoilingIssues.IssPcs, dbo.tblGrading_BoilingIssues.IssCts, dbo.tblGrading_BoilingIssues.Grp, dbo.tblGrading_BoilingIssues.IssDate " & _
                      "FROM dbo.tblGrading_BoilingIssues LEFT OUTER JOIN dbo.tblGrading_BoilingReturns ON dbo.tblGrading_BoilingIssues.Department = dbo.tblGrading_BoilingReturns.Department AND " & _
                            "dbo.tblGrading_BoilingIssues.ParNo = dbo.tblGrading_BoilingReturns.ParNo And dbo.tblGrading_BoilingIssues.PktNo = dbo.tblGrading_BoilingReturns.PktNo " & _
                      "WHERE (dbo.tblGrading_BoilingReturns.PktNo IS NULL) AND (dbo.tblGrading_BoilingIssues.Department = '" & cmbDept.Text & "') AND (YEAR(dbo.tblGrading_BoilingIssues.IssDate) >= '2018') " & _
                      "ORDER BY dbo.tblGrading_BoilingIssues.ParNo, dbo.tblGrading_BoilingIssues.PktNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                strOrderNo = ""

                Select Case rsComSql.Fields("Department").Value
                    Case "GradingPCU_N"
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT OrderNo FROM tblGradingTrf WHERE Department  = '" & rsComSql.Fields("Department").Value & "' AND ParcelNo = '" & rsComSql.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            strOrderNo = rsComSql_1.Fields("OrderNo").Value
                        End If
                        rsComSql_1 = Nothing

                    Case "Baguettes"
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT PktOrdNo FROM tblBAGPacket WHERE ParNo = '" & rsComSql.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            strOrderNo = rsComSql_1.Fields("PktOrdNo").Value
                        End If
                        rsComSql_1 = Nothing

                    Case "Princess"
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT PktOrdNo FROM tblPRPacket WHERE ParNo = '" & rsComSql.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            strOrderNo = rsComSql_1.Fields("PktOrdNo").Value
                        End If
                        rsComSql_1 = Nothing

                    Case "Rounds"
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT PktOrdNo FROM tblRndPacket WHERE ParNo = '" & rsComSql.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            strOrderNo = rsComSql_1.Fields("PktOrdNo").Value
                        End If
                        rsComSql_1 = Nothing

                    Case Else
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT PktOrdNo FROM tblExtPacket WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND ParNo = '" & rsComSql.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            strOrderNo = rsComSql_1.Fields("PktOrdNo").Value
                        End If
                        rsComSql_1 = Nothing
                End Select

                flxDetails.Rows.Add(rsComSql.Fields("Department").Value,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("IssPcs").Value,
                                    rsComSql.Fields("IssCts").Value,
                                    rsComSql.Fields("IssPcs").Value,
                                    rsComSql.Fields("IssCts").Value,
                                    rsComSql.Fields("Grp").Value, False,
                                    strOrderNo,
                                    Format(rsComSql.Fields("IssDate").Value, "yyyy/MM/dd"))

                dblIssPcs = dblIssPcs + rsComSql.Fields("IssPcs").Value
                dblIssCts = dblIssCts + rsComSql.Fields("IssCts").Value
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtIssPcs.Text = dblIssPcs
        txtIssCts.Text = Format(dblIssCts, "#0.000")
        txtRetPcs.Text = CalTotalPcs()
        txtRetCts.Text = Format(CalTotalCts, "#0.000")
        txtCount.Text = flxDetails.Rows.Count

    End Sub

    Private Function CalTotalPcs() As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Rows(intRow).Cells(8).EditedFormattedValue = True Then
                CalTotalPcs = CalTotalPcs + CInt(flxDetails.Item(3, intRow).Value)
            End If
        Next
        Return CalTotalPcs
    End Function

    Private Function CalTotalCts() As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Rows(intRow).Cells(8).EditedFormattedValue = True Then
                CalTotalCts = CalTotalCts + CDbl(flxDetails.Item(4, intRow).Value)
            End If
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
        Return CalTotalCts
    End Function

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmbDept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDept.SelectedIndexChanged
        ClearFields()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean
        Dim intAMS2 As Integer

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtEmp.Text = "" Then MsgBox("Invalid Employee No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        blnSave = False
        intAMS2 = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(8, intRow).Value = "1" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblGrading_BoilingReturns WHERE Department = '" & flxDetails.Item(0, intRow).Value & "' AND ParNo = '" & flxDetails.Item(1, intRow).Value & "' AND PktNo = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    MsgBox("Already Returned - " & flxDetails.Item(1, intRow).Value & "/" & flxDetails.Item(2, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing

                If IsNumeric(flxDetails.Item(6, intRow).Value) = False Then
                    MsgBox("Invalid Cts - " & flxDetails.Item(1, intRow).Value & "/" & flxDetails.Item(2, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If CDbl(flxDetails.Item(6, intRow).Value) > CDbl(flxDetails.Item(4, intRow).Value) + 0.003 Or CDbl(flxDetails.Item(6, intRow).Value) <= 0 Then
                    MsgBox("Invalid Cts - " & flxDetails.Item(1, intRow).Value & "/" & flxDetails.Item(2, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
        Next

        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(8, intRow).Value = "1" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblGrading_BoilingReturns WHERE Department = '" & flxDetails.Item(0, intRow).Value & "' AND ParNo = '" & flxDetails.Item(1, intRow).Value & "' AND PktNo = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    blnSave = True
                    AdoCN.Execute("INSERT INTO tblGrading_BoilingReturns(Department,ParNo,PktNo,EmpNo,RetPcs,RetCts, " & _
                                        "LostPcs,LostCts,RejPcs,RejCts,RetDate,RetTime,UserName,Trf,ExtPcs,Grp) " & _
                                  "VALUES ('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(2, intRow).Value & "','" & Mid(Trim(txtEmp.Text), 1, 6) & "'" & _
                                        "," & CDbl(flxDetails.Item(5, intRow).Value) & "," & CDbl(flxDetails.Item(6, intRow).Value) & ",0,0,0" & _
                                        ",0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_ID & "',0,0,'" & UCase(flxDetails.Item(7, intRow).Value) & "')")

                    If cmbDept.Text = "Rounds" Then

                    Else
                        If chkTrf.Checked = True Then
                            If cmbDept.Text = "Baguettes2" Or cmbDept.Text = "Baguettes3" Or cmbDept.Text = "Princess2" Or cmbDept.Text = "Emerald2" Or cmbDept.Text = "Emerald3" Then
                                intAMS2 = 0
                            Else
                                intAMS2 = 1
                            End If
                            AdoCN.Execute("INSERT INTO tblExpPacket(Department,ParNo,PktNo,PktPcs,PktCts,PktType,PktDate,AMS2,YAH) " & _
                                          "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(2, intRow).Value & "'," & CDbl(flxDetails.Item(5, intRow).Value) & "," & CDbl(flxDetails.Item(6, intRow).Value) & ",'','" & Format(Date.Now, "MM/dd/yyyy") & "'," & intAMS2 & "," & intAMS2 & ")")
                            AdoCN.Execute("UPDATE tblGrading_BoilingReturns SET Trf = 1 WHERE Department = '" & flxDetails.Item(0, intRow).Value & "' AND ParNo = '" & flxDetails.Item(1, intRow).Value & "' AND PktNo = '" & flxDetails.Item(2, intRow).Value & "'")
                        End If
                    End If
                    
                End If
                rsComSql = Nothing
            End If
        Next

        If blnSave = True Then
            MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
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
                flxDetails.Item(8, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(8, intRow).Value = False
            Next
        End If
        txtRetPcs.Text = CalTotalPcs()
        txtRetCts.Text = CalTotalCts()
    End Sub

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 8 Then
            txtRetPcs.Text = CalTotalPcs()
            txtRetCts.Text = CalTotalCts()
        End If
    End Sub

    Private Sub Delete()
        Dim intRow As Integer
        Dim blnSave As Boolean

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        blnSave = False
        PBResponse = MsgBox("Are you sure to Delete this Boiling Issues?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(8, intRow).Value = "1" Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblGrading_BoilingReturns WHERE Department = '" & flxDetails.Item(0, intRow).Value & "' AND ParNo = '" & flxDetails.Item(1, intRow).Value & "' AND PktNo = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        MsgBox("Already Returned - " & flxDetails.Item(1, intRow).Value & "/" & flxDetails.Item(2, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql = Nothing
                End If
            Next

            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(8, intRow).Value = "1" Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblGrading_BoilingReturns WHERE Department = '" & flxDetails.Item(0, intRow).Value & "' AND ParNo = '" & flxDetails.Item(1, intRow).Value & "' AND PktNo = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        blnSave = True
                        AdoCN.Execute("DELETE FROM tblGrading_BoilingIssues WHERE Department = '" & flxDetails.Item(0, intRow).Value & "' AND ParNo = '" & flxDetails.Item(1, intRow).Value & "' AND PktNo = '" & flxDetails.Item(2, intRow).Value & "'")
                        AdoCN.Execute("UPDATE tblGradingTrf SET Status = 0 WHERE Department = '" & flxDetails.Item(0, intRow).Value & "' AND ParcelNo = '" & flxDetails.Item(1, intRow).Value & "' AND PktNo = '" & flxDetails.Item(2, intRow).Value & "'")
                    End If
                    rsComSql = Nothing
                End If
            Next

            If blnSave = True Then
                MsgBox("Deleted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
            End If
        End If
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Delete()
    End Sub

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Dim strParNo As String
        Dim strPktNo As String

        Instring = UCase(InputBox("Enter Par/Pkt Number"))
        ParcelLen = Len(Instring)
        If ParcelLen >= 10 Then
            Select Case cmbDept.Text
                Case "Baguettes"
                    strParNo = Mid(Instring, 1, ParcelLen - 4)
                    strPktNo = strRight(Instring, 4)
                Case "Rounds"
                    If ParcelLen = 11 Then
                        strParNo = Mid(Instring, 1, ParcelLen - 3)
                        strPktNo = strRight(Instring, 3)
                    Else
                        strParNo = Mid(Instring, 1, ParcelLen - 4)
                        strPktNo = strRight(Instring, 4)
                    End If
                Case Else
                    If ParcelLen = 10 Then
                        strParNo = Mid(Instring, 1, ParcelLen - 3)
                        strPktNo = strRight(Instring, 3)
                    Else
                        strParNo = Mid(Instring, 1, ParcelLen - 4)
                        strPktNo = strRight(Instring, 4)
                    End If
            End Select

            Load_DetailsPacket(strParNo, strPktNo)
        Else
            cmdParPkt.Focus()
        End If
    End Sub

    Private Sub Load_DetailsPacket(ByVal strParcelNo As String, ByVal strPacketNo As String)
        Dim dblIssPcs As Double
        Dim dblIssCts As Double
        Dim strOrderNo As String
        Dim intRow As Integer

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        For intRow = 0 To flxDetails.Rows.Count - 1
            If strParcelNo = flxDetails.Item(1, intRow).Value And strPacketNo = flxDetails.Item(2, intRow).Value Then
                MsgBox("Already Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        dblIssPcs = 0
        dblIssCts = 0
        strOrderNo = ""
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblGrading_BoilingIssues.Department, dbo.tblGrading_BoilingIssues.ParNo, dbo.tblGrading_BoilingIssues.PktNo, " & _
                            "dbo.tblGrading_BoilingIssues.IssPcs, dbo.tblGrading_BoilingIssues.IssCts, dbo.tblGrading_BoilingIssues.Grp, dbo.tblGrading_BoilingIssues.IssDate " & _
                      "FROM dbo.tblGrading_BoilingIssues LEFT OUTER JOIN dbo.tblGrading_BoilingReturns ON dbo.tblGrading_BoilingIssues.Department = dbo.tblGrading_BoilingReturns.Department AND " & _
                            "dbo.tblGrading_BoilingIssues.ParNo = dbo.tblGrading_BoilingReturns.ParNo And dbo.tblGrading_BoilingIssues.PktNo = dbo.tblGrading_BoilingReturns.PktNo " & _
                      "WHERE (dbo.tblGrading_BoilingReturns.PktNo IS NULL) AND (dbo.tblGrading_BoilingIssues.Department = '" & cmbDept.Text & "') AND (YEAR(dbo.tblGrading_BoilingIssues.IssDate) >= '2018') AND " & _
                        "(dbo.tblGrading_BoilingIssues.ParNo = '" & strParcelNo & "') AND (dbo.tblGrading_BoilingIssues.PktNo = '" & strPacketNo & "') " & _
                      "ORDER BY dbo.tblGrading_BoilingIssues.ParNo, dbo.tblGrading_BoilingIssues.PktNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                strOrderNo = ""

                Select Case rsComSql.Fields("Department").Value
                    Case "GradingPCU_N"
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT OrderNo FROM tblGradingTrf WHERE Department  = '" & rsComSql.Fields("Department").Value & "' AND ParcelNo = '" & rsComSql.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            strOrderNo = rsComSql_1.Fields("OrderNo").Value
                        End If
                        rsComSql_1 = Nothing

                    Case "Baguettes"
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT PktOrdNo FROM tblBAGPacket WHERE ParNo = '" & rsComSql.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            strOrderNo = rsComSql_1.Fields("PktOrdNo").Value
                        End If
                        rsComSql_1 = Nothing

                    Case "Princess"
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT PktOrdNo FROM tblPRPacket WHERE ParNo = '" & rsComSql.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            strOrderNo = rsComSql_1.Fields("PktOrdNo").Value
                        End If
                        rsComSql_1 = Nothing

                    Case "Rounds"
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT PktOrdNo FROM tblRndPacket WHERE ParNo = '" & rsComSql.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            strOrderNo = rsComSql_1.Fields("PktOrdNo").Value
                        End If
                        rsComSql_1 = Nothing

                    Case Else
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT PktOrdNo FROM tblExtPacket WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND ParNo = '" & rsComSql.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            strOrderNo = rsComSql_1.Fields("PktOrdNo").Value
                        End If
                        rsComSql_1 = Nothing
                End Select

                flxDetails.Rows.Add(rsComSql.Fields("Department").Value,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("IssPcs").Value,
                                    rsComSql.Fields("IssCts").Value,
                                    rsComSql.Fields("IssPcs").Value,
                                    rsComSql.Fields("IssCts").Value,
                                    rsComSql.Fields("Grp").Value, True,
                                    strOrderNo,
                                    Format(rsComSql.Fields("IssDate").Value, "yyyy/MM/dd"))

                dblIssPcs = dblIssPcs + rsComSql.Fields("IssPcs").Value
                dblIssCts = dblIssCts + rsComSql.Fields("IssCts").Value
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtIssPcs.Text = CalTotalPcs()
        txtIssCts.Text = Format(CalTotalCts, "#0.000")
        txtRetPcs.Text = txtIssPcs.Text
        txtRetCts.Text = txtIssCts.Text
        txtCount.Text = flxDetails.Rows.Count

        cmdParPkt.Focus()
    End Sub

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Load_Details()
    End Sub
End Class