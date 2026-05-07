
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDCheckingIssue
    Private Sub ClearFields()
        flxDetails.Rows.Clear()
        txtIssPcs.Text = ""
        txtIssCts.Text = ""
        txtRetPcs.Text = ""
        txtRetCts.Text = ""
        txtEmp.Text = ""
        txtCount.Text = ""
    End Sub

    Private Sub frm_GRDCheckingIssue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_DepartmentProd(cmbDept)
        optCheck.Checked = True
    End Sub

    Private Sub cmbDept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDept.SelectedIndexChanged
        ClearFields()
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

        If cmbDept.Text = "Rounds" Then
            If optColor.Checked = True Then
                MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        End If

        dblIssPcs = 0
        dblIssCts = 0
        rsComSql = New ADODB.Recordset
        If optCheck.Checked = True Then
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblGrading_BoilingReturns.Department, dbo.tblGrading_BoilingReturns.ParNo, dbo.tblGrading_BoilingReturns.PktNo, " & _
                                "dbo.tblGrading_BoilingReturns.RetPcs, dbo.tblGrading_BoilingReturns.RetCts, dbo.tblGrading_BoilingReturns.Grp " & _
                          "FROM dbo.tblGrading_BoilingReturns LEFT OUTER JOIN dbo.tblGrading_CheckingIssues ON dbo.tblGrading_BoilingReturns.Department = dbo.tblGrading_CheckingIssues.Department AND " & _
                                "dbo.tblGrading_BoilingReturns.ParNo = dbo.tblGrading_CheckingIssues.ParNo AND " & _
                                "dbo.tblGrading_BoilingReturns.PktNo = dbo.tblGrading_CheckingIssues.PktNo " & _
                          "WHERE (dbo.tblGrading_BoilingReturns.Department = '" & cmbDept.Text & "') AND (dbo.tblGrading_CheckingIssues.PktNo Is Null) And (Year(dbo.tblGrading_BoilingReturns.RetDate) >= 2018) And (dbo.tblGrading_BoilingReturns.Trf = 0) " & _
                          "ORDER BY dbo.tblGrading_BoilingReturns.ParNo, dbo.tblGrading_BoilingReturns.PktNo", AdoCN, 1, 1)
        Else
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblGrading_CheckingReturns.Department, dbo.tblGrading_CheckingReturns.ParNo, dbo.tblGrading_CheckingReturns.PktNo, " & _
                                "dbo.tblGrading_CheckingReturns.ExPcs + dbo.tblGrading_CheckingReturns.VgPcs + dbo.tblGrading_CheckingReturns.BlPcs + dbo.tblGrading_CheckingReturns.ScPcs + " & _
                                "dbo.tblGrading_CheckingReturns.PsPcs + dbo.tblGrading_CheckingReturns.SzPcs + dbo.tblGrading_CheckingReturns.OkPcs + dbo.tblGrading_CheckingReturns.VRepPcs AS RetPcs, " & _
                                "dbo.tblGrading_CheckingReturns.ExCts + dbo.tblGrading_CheckingReturns.VgCts + dbo.tblGrading_CheckingReturns.BlCts + dbo.tblGrading_CheckingReturns.ScCts + dbo.tblGrading_CheckingReturns.PsCts " & _
                                " + dbo.tblGrading_CheckingReturns.SzCts + dbo.tblGrading_CheckingReturns.OkCts + dbo.tblGrading_CheckingReturns.VRepCts AS RetCts, dbo.tblGrading_CheckingReturns.Grp " & _
                         "FROM dbo.tblGrading_CheckingReturns LEFT OUTER JOIN dbo.tblGrading_Issues ON dbo.tblGrading_CheckingReturns.Department = dbo.tblGrading_Issues.Department AND " & _
                                "dbo.tblGrading_CheckingReturns.ParNo = dbo.tblGrading_Issues.ParNo And dbo.tblGrading_CheckingReturns.PktNo = dbo.tblGrading_Issues.PktNo " & _
                         "WHERE (dbo.tblGrading_Issues.PktNo IS NULL) AND (dbo.tblGrading_CheckingReturns.Department = '" & cmbDept.Text & "') AND (dbo.tblGrading_CheckingReturns.Sec = 2) AND " & _
                                "(dbo.tblGrading_CheckingReturns.ExPcs + dbo.tblGrading_CheckingReturns.VgPcs + dbo.tblGrading_CheckingReturns.BlPcs + dbo.tblGrading_CheckingReturns.ScPcs + " & _
                                "dbo.tblGrading_CheckingReturns.PsPcs + dbo.tblGrading_CheckingReturns.SzPcs + dbo.tblGrading_CheckingReturns.OkPcs + dbo.tblGrading_CheckingReturns.VRepPcs > 0) AND (YEAR(dbo.tblGrading_CheckingReturns.RetDate) >= 2018)" & _
                         "ORDER BY dbo.tblGrading_CheckingReturns.ParNo, dbo.tblGrading_CheckingReturns.PktNo", AdoCN, 1, 1)
        End If
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
                                    rsComSql.Fields("RetPcs").Value,
                                    rsComSql.Fields("RetCts").Value,
                                    rsComSql.Fields("RetPcs").Value,
                                    rsComSql.Fields("RetCts").Value,
                                    rsComSql.Fields("Grp").Value, False,
                                    strOrderNo)

                dblIssPcs = dblIssPcs + rsComSql.Fields("RetPcs").Value
                dblIssCts = dblIssCts + rsComSql.Fields("RetCts").Value
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        If optCheck.Checked = True Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblGrading_BoilingReturns.Department, dbo.tblGrading_BoilingReturns.ParNo, dbo.tblGrading_BoilingReturns.PktNo, dbo.tblGrading_BoilingReturns.RetPcs, " & _
                            "dbo.tblGrading_BoilingReturns.RetCts, dbo.tblGrading_BoilingReturns.Grp, dbo.tblExpReturns.Sec, dbo.tblExpReturns.RejPcs, dbo.tblExpReturns.RejCts " & _
                          "FROM dbo.tblGrading_BoilingReturns INNER JOIN dbo.tblExpReturns ON dbo.tblGrading_BoilingReturns.Department = dbo.tblExpReturns.Department AND dbo.tblGrading_BoilingReturns.ParNo = dbo.tblExpReturns.ParNo AND " & _
                            "dbo.tblGrading_BoilingReturns.PktNo = dbo.tblExpReturns.PktNo LEFT OUTER JOIN " & _
                            "dbo.tblGrading_CheckingIssues ON dbo.tblGrading_BoilingReturns.Department = dbo.tblGrading_CheckingIssues.Department AND  " & _
                            "dbo.tblGrading_BoilingReturns.ParNo = dbo.tblGrading_CheckingIssues.ParNo And dbo.tblGrading_BoilingReturns.PktNo = dbo.tblGrading_CheckingIssues.PktNo " & _
                          "WHERE (dbo.tblGrading_BoilingReturns.Department = '" & cmbDept.Text & "') AND (dbo.tblGrading_CheckingIssues.PktNo IS NULL) AND (YEAR(dbo.tblGrading_BoilingReturns.RetDate) >= 2018) AND " & _
                            "(dbo.tblGrading_BoilingReturns.Trf = 1) AND (dbo.tblExpReturns.Sec = 3) AND (dbo.tblExpReturns.RejPcs > 0) " & _
                          "ORDER BY dbo.tblGrading_BoilingReturns.ParNo, dbo.tblGrading_BoilingReturns.PktNo", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    flxDetails.Rows.Add(rsComSql.Fields("Department").Value,
                                        rsComSql.Fields("ParNo").Value,
                                        rsComSql.Fields("PktNo").Value,
                                        rsComSql.Fields("RejPcs").Value,
                                        rsComSql.Fields("RejCts").Value,
                                        rsComSql.Fields("RejPcs").Value,
                                        rsComSql.Fields("RejCts").Value,
                                        rsComSql.Fields("Grp").Value, False)

                    dblIssPcs = dblIssPcs + rsComSql.Fields("RejPcs").Value
                    dblIssCts = dblIssCts + rsComSql.Fields("RejCts").Value

                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing
        End If


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

    Private Sub optColor_Click(sender As Object, e As EventArgs) Handles optColor.Click
        ClearFields()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
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

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean
        Dim intSec As Integer

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtEmp.Text = "" Then MsgBox("Invalid Employee No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        blnSave = False

        If cmbDept.Text = "Rounds" Then
            intSec = 3
            If optColor.Checked = True Then
                MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Else
            intSec = 2
        End If

        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(8, intRow).Value = "1" Then
                If optCheck.Checked = True Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblGrading_CheckingIssues WHERE Department = '" & flxDetails.Item(0, intRow).Value & "' AND ParNo = '" & flxDetails.Item(1, intRow).Value & "' AND PktNo = '" & flxDetails.Item(2, intRow).Value & "' AND Sec = " & intSec & "", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        MsgBox("Already Issued - " & flxDetails.Item(1, intRow).Value & "/" & flxDetails.Item(2, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql = Nothing
                Else
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblGrading_Issues WHERE Department = '" & flxDetails.Item(0, intRow).Value & "' AND ParNo = '" & flxDetails.Item(1, intRow).Value & "' AND PktNo = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        MsgBox("Already Issued - " & flxDetails.Item(1, intRow).Value & "/" & flxDetails.Item(2, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql = Nothing
                End If

                If CDbl(flxDetails.Item(6, intRow).Value) > CDbl(flxDetails.Item(4, intRow).Value) Or CDbl(flxDetails.Item(6, intRow).Value) <= 0 Then
                    MsgBox("Invalid Cts - " & flxDetails.Item(1, intRow).Value & "/" & flxDetails.Item(2, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
        Next

        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(8, intRow).Value = "1" Then
                If optCheck.Checked = True Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblGrading_CheckingIssues WHERE Department = '" & flxDetails.Item(0, intRow).Value & "' AND ParNo = '" & flxDetails.Item(1, intRow).Value & "' AND PktNo = '" & flxDetails.Item(2, intRow).Value & "' AND Sec = " & intSec & "", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        blnSave = True
                        AdoCN.Execute("INSERT INTO tblGrading_CheckingIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime,UserName,Grp) " & _
                                      "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(2, intRow).Value & "'," & intSec & ",'" & Mid(Trim(txtEmp.Text), 1, 6) & "','" & CDbl(flxDetails.Item(5, intRow).Value) & "'," & _
                                             "'" & CDbl(flxDetails.Item(6, intRow).Value) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_ID & "','" & UCase(flxDetails.Item(7, intRow).Value) & "')")
                    End If
                    rsComSql = Nothing
                Else
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblGrading_Issues WHERE Department = '" & flxDetails.Item(0, intRow).Value & "' AND ParNo = '" & flxDetails.Item(1, intRow).Value & "' AND PktNo = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        blnSave = True
                        AdoCN.Execute("INSERT INTO tblGrading_Issues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                      "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(2, intRow).Value & "',1,'" & Mid(Trim(txtEmp.Text), 1, 6) & "','" & CDbl(flxDetails.Item(5, intRow).Value) & "'," & _
                                             "'" & CDbl(flxDetails.Item(6, intRow).Value) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")
                    End If
                    rsComSql = Nothing
                End If
            End If
        Next

        If blnSave = True Then
            MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
        End If

    End Sub

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 8 Then
            txtRetPcs.Text = CalTotalPcs()
            txtRetCts.Text = CalTotalCts()
        End If
    End Sub

    Private Sub optCheck_Click(sender As Object, e As EventArgs) Handles optCheck.Click
        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            Save()
        End If
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
                    strParNo = Mid(Instring, 1, ParcelLen - 3)
                    strPktNo = strRight(Instring, 3)
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

        If cmbDept.Text = "Rounds" Then
            If optColor.Checked = True Then
                MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        End If

        dblIssPcs = 0
        dblIssCts = 0
        rsComSql = New ADODB.Recordset
        If optCheck.Checked = True Then
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblGrading_BoilingReturns.Department, dbo.tblGrading_BoilingReturns.ParNo, dbo.tblGrading_BoilingReturns.PktNo, " & _
                                "dbo.tblGrading_BoilingReturns.RetPcs, dbo.tblGrading_BoilingReturns.RetCts, dbo.tblGrading_BoilingReturns.Grp " & _
                          "FROM dbo.tblGrading_BoilingReturns LEFT OUTER JOIN dbo.tblGrading_CheckingIssues ON dbo.tblGrading_BoilingReturns.Department = dbo.tblGrading_CheckingIssues.Department AND " & _
                                "dbo.tblGrading_BoilingReturns.ParNo = dbo.tblGrading_CheckingIssues.ParNo AND " & _
                                "dbo.tblGrading_BoilingReturns.PktNo = dbo.tblGrading_CheckingIssues.PktNo " & _
                          "WHERE (dbo.tblGrading_BoilingReturns.Department = '" & cmbDept.Text & "') AND (dbo.tblGrading_CheckingIssues.PktNo Is Null) And (Year(dbo.tblGrading_BoilingReturns.RetDate) >= 2018) AND (dbo.tblGrading_BoilingReturns.Trf = 0) AND (dbo.tblGrading_BoilingReturns.ParNo = '" & strParcelNo & "') AND (dbo.tblGrading_BoilingReturns.PktNo = '" & strPacketNo & "') " & _
                          "ORDER BY dbo.tblGrading_BoilingReturns.ParNo, dbo.tblGrading_BoilingReturns.PktNo", AdoCN, 1, 1)
        Else
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblGrading_CheckingReturns.Department, dbo.tblGrading_CheckingReturns.ParNo, dbo.tblGrading_CheckingReturns.PktNo, " & _
                                "dbo.tblGrading_CheckingReturns.ExPcs + dbo.tblGrading_CheckingReturns.VgPcs + dbo.tblGrading_CheckingReturns.BlPcs + dbo.tblGrading_CheckingReturns.ScPcs + " & _
                                "dbo.tblGrading_CheckingReturns.PsPcs + dbo.tblGrading_CheckingReturns.SzPcs + dbo.tblGrading_CheckingReturns.OkPcs + dbo.tblGrading_CheckingReturns.VRepPcs AS RetPcs, " & _
                                "dbo.tblGrading_CheckingReturns.ExCts + dbo.tblGrading_CheckingReturns.VgCts + dbo.tblGrading_CheckingReturns.BlCts + dbo.tblGrading_CheckingReturns.ScCts + dbo.tblGrading_CheckingReturns.PsCts " & _
                                " + dbo.tblGrading_CheckingReturns.SzCts + dbo.tblGrading_CheckingReturns.OkCts + dbo.tblGrading_CheckingReturns.VRepCts AS RetCts, dbo.tblGrading_CheckingReturns.Grp " & _
                         "FROM dbo.tblGrading_CheckingReturns LEFT OUTER JOIN dbo.tblGrading_Issues ON dbo.tblGrading_CheckingReturns.Department = dbo.tblGrading_Issues.Department AND " & _
                                "dbo.tblGrading_CheckingReturns.ParNo = dbo.tblGrading_Issues.ParNo And dbo.tblGrading_CheckingReturns.PktNo = dbo.tblGrading_Issues.PktNo " & _
                         "WHERE (dbo.tblGrading_Issues.PktNo IS NULL) AND (dbo.tblGrading_CheckingReturns.Department = '" & cmbDept.Text & "') AND (dbo.tblGrading_CheckingReturns.Sec = 2) AND (dbo.tblGrading_CheckingReturns.ParNo = '" & strParcelNo & "') AND (dbo.tblGrading_CheckingReturns.PktNo = '" & strPacketNo & "') AND " & _
                                "(dbo.tblGrading_CheckingReturns.ExPcs + dbo.tblGrading_CheckingReturns.VgPcs + dbo.tblGrading_CheckingReturns.BlPcs + dbo.tblGrading_CheckingReturns.ScPcs + " & _
                                "dbo.tblGrading_CheckingReturns.PsPcs + dbo.tblGrading_CheckingReturns.SzPcs + dbo.tblGrading_CheckingReturns.OkPcs + dbo.tblGrading_CheckingReturns.VRepPcs > 0) AND (YEAR(dbo.tblGrading_CheckingReturns.RetDate) >= 2018)" & _
                         "ORDER BY dbo.tblGrading_CheckingReturns.ParNo, dbo.tblGrading_CheckingReturns.PktNo", AdoCN, 1, 1)
        End If
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
                                    rsComSql.Fields("RetPcs").Value,
                                    rsComSql.Fields("RetCts").Value,
                                    rsComSql.Fields("RetPcs").Value,
                                    rsComSql.Fields("RetCts").Value,
                                    rsComSql.Fields("Grp").Value, True,
                                    strOrderNo)

                dblIssPcs = dblIssPcs + rsComSql.Fields("RetPcs").Value
                dblIssCts = dblIssCts + rsComSql.Fields("RetCts").Value
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        If optCheck.Checked = True Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblGrading_BoilingReturns.Department, dbo.tblGrading_BoilingReturns.ParNo, dbo.tblGrading_BoilingReturns.PktNo, dbo.tblGrading_BoilingReturns.RetPcs, " & _
                            "dbo.tblGrading_BoilingReturns.RetCts, dbo.tblGrading_BoilingReturns.Grp, dbo.tblExpReturns.Sec, dbo.tblExpReturns.RejPcs, dbo.tblExpReturns.RejCts " & _
                          "FROM dbo.tblGrading_BoilingReturns INNER JOIN dbo.tblExpReturns ON dbo.tblGrading_BoilingReturns.Department = dbo.tblExpReturns.Department AND dbo.tblGrading_BoilingReturns.ParNo = dbo.tblExpReturns.ParNo AND " & _
                            "dbo.tblGrading_BoilingReturns.PktNo = dbo.tblExpReturns.PktNo LEFT OUTER JOIN " & _
                            "dbo.tblGrading_CheckingIssues ON dbo.tblGrading_BoilingReturns.Department = dbo.tblGrading_CheckingIssues.Department AND  " & _
                            "dbo.tblGrading_BoilingReturns.ParNo = dbo.tblGrading_CheckingIssues.ParNo And dbo.tblGrading_BoilingReturns.PktNo = dbo.tblGrading_CheckingIssues.PktNo " & _
                          "WHERE (dbo.tblGrading_BoilingReturns.Department = '" & cmbDept.Text & "') AND (dbo.tblGrading_CheckingIssues.PktNo IS NULL) AND (YEAR(dbo.tblGrading_BoilingReturns.RetDate) >= 2018) AND " & _
                            "(dbo.tblGrading_BoilingReturns.Trf = 1) AND (dbo.tblExpReturns.Sec = 3) AND (dbo.tblExpReturns.RejPcs > 0) AND (dbo.tblGrading_BoilingReturns.ParNo = '" & strParcelNo & "') AND (dbo.tblGrading_BoilingReturns.PktNo = '" & strPacketNo & "') " & _
                          "ORDER BY dbo.tblGrading_BoilingReturns.ParNo, dbo.tblGrading_BoilingReturns.PktNo", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    flxDetails.Rows.Add(rsComSql.Fields("Department").Value,
                                        rsComSql.Fields("ParNo").Value,
                                        rsComSql.Fields("PktNo").Value,
                                        rsComSql.Fields("RejPcs").Value,
                                        rsComSql.Fields("RejCts").Value,
                                        rsComSql.Fields("RejPcs").Value,
                                        rsComSql.Fields("RejCts").Value,
                                        rsComSql.Fields("Grp").Value, True)

                    dblIssPcs = dblIssPcs + rsComSql.Fields("RejPcs").Value
                    dblIssCts = dblIssCts + rsComSql.Fields("RejCts").Value

                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing
        End If


        txtIssPcs.Text = CalTotalPcs()
        txtIssCts.Text = Format(CalTotalCts, "#0.000")
        txtRetPcs.Text = txtIssPcs.Text
        txtRetCts.Text = txtIssCts.Text
        txtCount.Text = flxDetails.Rows.Count

    End Sub

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Load_Details()
    End Sub
End Class