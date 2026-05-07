
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RprPacketAdmin

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
        Dim intPrevSec As Integer

        Dim intType As Integer

        If cmbDept.Text <> "" Then
            strDepartment = cmbDept.Text
        Else
            MsgBox("Please select the Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        intType = 0

        Select Case True
            Case optNIss.Checked
                intType = 1
            Case optNRet.Checked
                intType = 2
        End Select

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
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT ParNo, PktFlow FROM tblRprPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & strDepartment & "' AND DelDate IS NOT NULL AND Trf = 0", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                blnFound = True
            Else
                blnFound = False
            End If
            rsComSql = Nothing
            If blnFound = False Then cmdParPkt.Focus() : Exit Sub

            If cmbSection.SelectedIndex + 1 = 20 Then
                If Mid(cmbDept.Text, 1, 9) = "RoughPlan" Then
                    intPrevSec = 7
                ElseIf Mid(cmbDept.Text, 1, 11) = "RoughSawing" Then
                    intPrevSec = 7
                End If
            Else
                intPrevSec = cmbSection.SelectedIndex
            End If

            If intType = 1 Then
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
                    Select Case intType
                        Case 1
                            rsComSql = New ADODB.Recordset
                            rsComSql.Open("SELECT * FROM tblRPrPacketAdmin WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & strDepartment & "' AND Done = 0", AdoCN, 1, 1)
                            If rsComSql.RecordCount = 0 Then
                                blnFound = True
                            Else
                                blnFound = False
                            End If
                            rsComSql = Nothing
                            If blnFound = False Then cmdParPkt.Focus() : Exit Sub
                        Case 2
                            rsComSql = New ADODB.Recordset
                            rsComSql.Open("SELECT * FROM tblRPrPacketAdmin WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & strDepartment & "' AND Done = 0", AdoCN, 1, 1)
                            If rsComSql.RecordCount Then
                                If Not IsDBNull(rsComSql.Fields("IssDate").Value) And IsDBNull(rsComSql.Fields("RetDate").Value) Then
                                    blnFound = True
                                Else
                                    blnFound = False
                                End If
                            End If
                            rsComSql = Nothing
                            If blnFound = False Then cmdParPkt.Focus() : Exit Sub
                    End Select
                End If

                If blnFound = True Then
                    flxDetails.Rows.Add(ParcelNo,
                                        PacketNo,
                                        dblRetPcs,
                                        Math.Round(dblRetCts, 3),
                                        strIssEmp)

                    txtTotPcs.Text = CDbl(txtTotPcs.Text) + dblRetPcs
                    txtTotCts.Text = Math.Round(CDbl(txtTotCts.Text) + dblRetCts, 3)
                    txtCount.Text = CDbl(txtCount.Text) + 1

                    'txtTotPcs.Text = CalTotalPcs(flxDetails)
                    'txtTotCts.Text = CalTotalCts(flxDetails)
                    'txtCount.Text = flxDetails.RowCount

                    cmdParPkt.Focus()
                End If
            Else
                strIssEmp = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblRPrPacketAdmin WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & strDepartment & "' AND Done = 0", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("IssDate").Value) And IsDBNull(rsComSql.Fields("RetDate").Value) Then
                        blnFound = True
                        dblRetPcs = rsComSql.Fields("Pcs").Value
                        dblRetCts = rsComSql.Fields("Cts").Value
                        strIssEmp = rsComSql.Fields("IssEmp").Value
                    Else
                        blnFound = False
                    End If
                Else
                    blnFound = False
                End If
                rsComSql = Nothing
                If blnFound = False Then cmdParPkt.Focus() : Exit Sub

                If blnFound = True Then
                    flxDetails.Rows.Add(ParcelNo,
                                        PacketNo,
                                        dblRetPcs,
                                        Math.Round(dblRetCts, 3),
                                        strIssEmp)

                    txtTotPcs.Text = CDbl(txtTotPcs.Text) + dblRetPcs
                    txtTotCts.Text = Math.Round(CDbl(txtTotCts.Text) + dblRetCts, 3)
                    txtCount.Text = CDbl(txtCount.Text) + 1

                    'txtTotPcs.Text = CalTotalPcs(flxDetails)
                    'txtTotCts.Text = CalTotalCts(flxDetails)
                    'txtCount.Text = flxDetails.RowCount

                    cmdParPkt.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim intType As Integer

        Select Case True
            Case optNIss.Checked
                intType = 1
            Case optNRet.Checked
                intType = 2
        End Select

        If cmbDept.Text <> "" Then
            strDepartment = cmbDept.Text
        Else
            MsgBox("Please select the Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        For intRow = 0 To flxDetails.Rows.Count - 1
            Select Case intType
                Case 1
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblRPrPacketAdmin WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Department = '" & strDepartment & "' AND Done = 0", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        AdoCN.Execute("INSERT INTO tblRPrPacketAdmin(Department, ParNo, PktNo, Sec, Pcs, Cts, IssEmp, IssDate, IssTime) " & _
                                      "VALUES('" & strDepartment & "','" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & cmbSection.SelectedIndex + 1 & "','" & CDbl(flxDetails.Item(2, intRow).Value) & "','" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & PBUser_EmpNo & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")
                    End If
                    rsComSql = Nothing
                Case 2
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblRPrPacketAdmin WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Department = '" & strDepartment & "' AND Done = 0", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        AdoCN.Execute("UPDATE tblRPrPacketAdmin SET RetEmp = '" & PBUser_EmpNo & "',RetDate = '" & Format(Date.Now, "MM/dd/yyyy") & "', RetTime = '" & Format(Date.Now, "HH:mm:ss") & "', Done = 1 WHERE ID = '" & rsComSql.Fields("ID").Value & "'")
                    End If
                    rsComSql = Nothing
            End Select
        Next

        MsgBox("Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        ClearText()
        'cmbDept.Text = ""
    End Sub

    Private Sub flxDetails_DoubleClick(sender As Object, e As EventArgs) Handles flxDetails.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
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

    Private Sub optNIss_CheckedChanged(sender As Object, e As EventArgs) Handles optNIss.CheckedChanged
        flxDetails.Rows.Clear()
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0.000"
        txtCount.Text = "0"
    End Sub

    Private Sub optNRet_CheckedChanged(sender As Object, e As EventArgs) Handles optNRet.CheckedChanged
        flxDetails.Rows.Clear()
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0.000"
        txtCount.Text = "0"
    End Sub
End Class