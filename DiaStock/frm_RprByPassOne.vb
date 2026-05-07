
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RprByPassOne

    Dim strDepartment As String
    Dim ParcelNo As String
    Dim PacketNo As String

    Private Sub Load_RprDepartments()
        cmbDept.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT Department FROM dbo.tblRPrFlow GROUP BY Department ORDER BY Department", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbDept.Items.Add(rsComSql.Fields("Department").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub ClearText()
        flxDetails.Rows.Clear()
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0.000"
        txtEmpNo.Text = ""
        txtCount.Text = "0"
        'cmbSection.Text = ""
        'cmbSection.Items.Clear()
    End Sub

    Private Sub frm_RprByPassOne_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        ClearText()
        Load_RprDepartments()
    End Sub

    Private Sub cmbDept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDept.SelectedIndexChanged
        Load_Section()
    End Sub

    Private Sub Load_Section()
        Dim rsSection As ADODB.Recordset

        cmbSection.Items.Clear()
        rsSection = New ADODB.Recordset
        rsSection.Open("SELECT * FROM tblRprSections WHERE Department = '" & cmbDept.Text & "' ORDER BY secCode", AdoCN, 1, 1)
        If rsSection.RecordCount Then
            rsSection.MoveFirst()
            Do
                cmbSection.Items.Add(rsSection.Fields("SecName").Value)
                rsSection.MoveNext()
            Loop Until rsSection.EOF
        End If
        rsSection = Nothing
        cmbSection.SelectedIndex = 0
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearText()
        cmbDept.Text = ""
    End Sub

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Dim intRow As Integer
        Dim blnFound As Boolean
        Dim dblRetPcs As Double
        Dim dblRetCts As Double
        Dim strIssEmp As String
        Dim strFlow As String
        Dim intPrevSec As Integer

        If cmbDept.Text <> "" Then
            strDepartment = cmbDept.Text
        Else
            MsgBox("Please select the Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If Mid(strDepartment, 1, 11) = "RoughSawing" Then
            If cmbSection.Text = "FinishSawing" Then
                MsgBox("You cannot Bypass this section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        End If

        Datavalid = False
        Parcel = True
        Instring = UCase(InputBox("Enter Par/Pkt Number"))
        ParcelLen = Len(Instring)
        If ParcelLen > 10 Then
            Datavalid = True

            ParcelNo = Mid(Instring, 1, ParcelLen - 4)
            PacketNo = strRight(Instring, 4)
        End If

        If Datavalid = True Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(0, intRow).Value = ParcelNo And flxDetails.Item(1, intRow).Value = PacketNo Then
                    MsgBox("Already Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    cmdParPkt.Focus()
                    Exit Sub
                End If
            Next

            blnFound = False
            strFlow = ""
            rsComSql = New ADODB.Recordset
            If strDepartment = "RoughBruting" Then
                rsComSql.Open("SELECT ParNo, PktFlow FROM tblRprPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & strDepartment & "' AND DelDate IS NOT NULL AND AccDate IS NOT NULL", AdoCN, 1, 1)
            Else
                rsComSql.Open("SELECT ParNo, PktFlow FROM tblRprPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & strDepartment & "' AND DelDate IS NOT NULL ", AdoCN, 1, 1)
            End If
            If rsComSql.RecordCount Then
                blnFound = True
                strFlow = rsComSql.Fields("PktFlow").Value
            Else
                blnFound = False
            End If
            rsComSql = Nothing
            If blnFound = False Then cmdParPkt.Focus() : Exit Sub

            If cmbSection.SelectedIndex + 1 = 20 Then
                If Mid(cmbDept.Text, 1, 9) = "RoughPlan" Then
                    intPrevSec = 7
                ElseIf Mid(cmbDept.Text, 1, 11) = "RoughSawing" Then
                    intPrevSec = 12
                ElseIf Mid(cmbDept.Text, 1, 12) = "RoughBruting" Then
                    intPrevSec = 4
                End If
            Else
                intPrevSec = cmbSection.SelectedIndex
            End If

            If blnFound = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT ParNo FROM tblRPrIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & strDepartment & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                rsComSql = Nothing
                If blnFound = False Then cmdParPkt.Focus() : Exit Sub
            End If

            If blnFound = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT ParNo FROM tblRPrIssues WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & strDepartment & "' AND Sec = " & intPrevSec & "", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    blnFound = True
                Else
                    blnFound = False
                End If
                rsComSql = Nothing
                If blnFound = False Then cmdParPkt.Focus() : Exit Sub
            End If

            dblRetPcs = 0
            dblRetCts = 0
            strIssEmp = ""
            If blnFound = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT ParNo, RetPcsT + RetPcsB AS RetPcs, RetCts FROM tblRPrReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & strDepartment & "' AND Sec = " & intPrevSec & "", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    dblRetPcs = rsComSql.Fields("RetPcs").Value
                    dblRetCts = rsComSql.Fields("RetCts").Value
                    blnFound = True
                Else
                    blnFound = False
                End If
                rsComSql = Nothing
                If blnFound = False Then cmdParPkt.Focus() : Exit Sub
            End If

            If blnFound = True Then
                flxDetails.Rows.Add(ParcelNo,
                                    PacketNo,
                                    dblRetPcs,
                                    Math.Round(dblRetCts, 3),
                                    strIssEmp,
                                    strFlow)

                txtTotPcs.Text = CDbl(txtTotPcs.Text) + dblRetPcs
                txtTotCts.Text = Math.Round(CDbl(txtTotCts.Text) + dblRetCts, 3)
                txtCount.Text = CDbl(txtCount.Text) + 1

                'txtTotPcs.Text = CalTotalPcs(flxDetails)
                'txtTotCts.Text = CalTotalCts(flxDetails)
                'txtCount.Text = flxDetails.RowCount

                cmdParPkt.Focus()
            End If

        End If
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        If Len(txtEmpNo.Text) <> 6 Then MsgBox("Invalid Emp No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & txtEmpNo.Text & "' AND Pay = 1", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Employee No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        Else
            txtEmpNo.Text = UCase(txtEmpNo.Text)
        End If
        rsComSql = Nothing

        'rsComSql_2 = New ADODB.Recordset
        'rsComSql_2.Open("SELECT EmpNo FROM tblRprReturns WHERE EmpNo = '" & txtEmpNo.Text & "' AND LostPcs > 0 AND Active = 1", AdoCN, 1, 1)
        'If rsComSql_2.RecordCount Then
        '    MsgBox("Have a Lost to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '    Exit Sub
        'End If
        'rsComSql_2 = Nothing

        rsComSql_2 = New ADODB.Recordset
        rsComSql_2.Open("SELECT EmpNo FROM VW_ALLLostEmpNo WHERE EmpNo = '" & txtEmpNo.Text & "'", AdoCN, 1, 1)
        If rsComSql_2.RecordCount Then
            MsgBox("Have a Lost to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql_2 = Nothing

        If intCheckIssDate = 1 Then
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT FullEmpNo, CATEGORY, Pay FROM VW_EMP_MASTER_SMALL2 WHERE (CATEGORY LIKE 'DIRECT%' OR CATEGORY LIKE 'TEMP%') AND (Pay = 1) AND (FullEmpNo = '" & txtEmpNo.Text & "')", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT TOP (100) PERCENT DATEDIFF(d, dbo.tblRprIssues.IssDate, GETDATE()) AS Days " & _
                                "FROM dbo.tblRprIssues INNER JOIN dbo.tblParcel ON dbo.tblRprIssues.ParNo = dbo.tblParcel.GrpParNo AND dbo.tblRprIssues.Department = dbo.tblParcel.Depart LEFT OUTER JOIN " & _
                                    "dbo.tblRprReturns ON dbo.tblRprIssues.Department = dbo.tblRprReturns.Department AND dbo.tblRprIssues.ParNo = dbo.tblRprReturns.ParNo AND dbo.tblRprIssues.PktNo = dbo.tblRprReturns.PktNo AND " & _
                                    "dbo.tblRprIssues.Sec = dbo.tblRprReturns.Sec " & _
                                "WHERE (dbo.tblParcel.Complete = 0) AND (dbo.tblRprReturns.PktNo IS NULL) AND (DATEDIFF(d, dbo.tblRprIssues.IssDate, GETDATE()) > '" & intDelayDays & "') AND (dbo.tblParcel.Depart = '" & cmbDept.Text & "') AND (dbo.tblRprIssues.EmpNo = '" & txtEmpNo.Text & "')", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    MsgBox("Have a Delayed Packet to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql_2 = Nothing
            End If
            rsComSql_1 = Nothing
        End If

        If cmbDept.Text <> "" Then
            strDepartment = cmbDept.Text
        Else
            MsgBox("Please select the Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        For intRow = 0 To flxDetails.Rows.Count - 1
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT ParNo FROM tblRPrIssues WHERE Department = '" & strDepartment & "' AND ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = '" & cmbSection.SelectedIndex + 1 & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                'Issues
                AdoCN.Execute("INSERT INTO tblRPrIssues(Department,ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime) " & _
                              "VALUES('" & strDepartment & "','" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & cmbSection.SelectedIndex + 1 & "','" & flxDetails.Item(5, intRow).Value & "','" & cmbSection.SelectedIndex + 1 & "','" & txtEmpNo.Text & "','" & CDbl(flxDetails.Item(2, intRow).Value) & "',0,'" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")

                'Returns
                AdoCN.Execute("INSERT INTO tblRPrReturns(Department,ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcsT,RetPcsB,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs,Status,RghCts,LRghCts,UserName,CompName) " & _
                              "VALUES('" & strDepartment & "','" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(5, intRow).Value & "','" & cmbSection.SelectedIndex + 1 & "','" & cmbSection.SelectedIndex + 1 & "','" & txtEmpNo.Text & "','" & CDbl(flxDetails.Item(2, intRow).Value) & "',0,'" & CDbl(flxDetails.Item(3, intRow).Value) & "',0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,'" & PBUser_EmpNo & "','" & PBCompName & "')")

            End If
            rsComSql = Nothing
        Next

        MsgBox("ByPassed Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        ClearText()
        'cmbDept.Text = ""
    End Sub

    Private Sub flxDetails_DoubleClick(sender As Object, e As EventArgs) Handles flxDetails.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse  = MsgBoxResult.Yes Then
            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)
            txtTotPcs.Text = CalTotalPcs(flxDetails)
            txtTotCts.Text = CalTotalCts(flxDetails)
            txtCount.Text = flxDetails.RowCount
        End If
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(2, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(3, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmbSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSection.SelectedIndexChanged
        flxDetails.Rows.Clear()
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0.000"
        txtCount.Text = "0"
    End Sub
End Class