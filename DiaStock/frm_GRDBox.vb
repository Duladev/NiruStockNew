
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_GRDBox
    Private Sub ClearFields()
        txtParNo.Text = ""
        txtTotPcs.Text = ""
        txtTotCts.Text = ""
        txtBalPcs.Text = ""
        txtBalCts.Text = ""
        flxDetails.Rows.Clear()
        txtBoxNo.Text = ""
        txtPktPcs.Text = ""
        txtPktCts.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtActCts.Text = ""
        flxBox.Rows.Clear()
        optNiru.Checked = True
        txtGroup.Text = ""
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

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim intType As Integer

        If txtParNo.Text = "" Then MsgBox("Invalid Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        Select Case True
            Case optNiru.Checked
                intType = 0
            Case optRepair.Checked
                intType = 2
        End Select

        For intRow = 0 To flxBox.Rows.Count - 1
            AdoCN.Execute("INSERT INTO tblGrading_Box(Department,ParNo,BoxNo,Pcs,Cts,BoxDate,BoxTime,Type,TypeName,PktNo,ParGroup,Clarity,FM,ActCts) " & _
                          "VALUES('Rounds','" & txtParNo.Text & "'," & CDbl(flxBox.Item(1, intRow).Value) & "," & _
                            "" & CDbl(flxBox.Item(2, intRow).Value) & "," & CDbl(flxBox.Item(3, intRow).Value) & "," & _
                            "'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "'," & intType & ",'" & flxBox.Item(4, intRow).Value & "'," & _
                            "'" & flxBox.Item(5, intRow).Value & "','" & flxBox.Item(6, intRow).Value & "','" & flxBox.Item(7, intRow).Value & "',0," & CDbl(flxBox.Item(8, intRow).Value) & ")")
        Next

        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub Load_GradingBox()
        Dim intType As Integer
        Dim dblPlanCts As Double
        Dim strPlanClarity As String
        Dim strPlanColor As String
        Dim dblRghPcs As Double
        Dim dblRghCts As Double

        If txtGroup.text = "" Then Exit Sub

        txtParNo.Text = UCase(txtParNo.Text)
        txtGroup.Text = UCase(txtGroup.Text)

        txtBoxNo.Text = GetBoxNo
        rsComSql = New ADODB.Recordset
        Select Case True
            Case optNiru.Checked
                intType = 0
                rsComSql.Open("SELECT SUM(dbo.tblGrading_CheckingReturns.ExPcs + dbo.tblGrading_CheckingReturns.VgPcs + dbo.tblGrading_CheckingReturns.PsPcs + dbo.tblGrading_CheckingReturns.SzPcs + dbo.tblGrading_CheckingReturns.OkPcs + dbo.tblGrading_CheckingReturns.BlPcs + dbo.tblGrading_CheckingReturns.ScPcs) AS Pcs, " & _
                                "ROUND(SUM(dbo.tblGrading_CheckingReturns.ExCts + dbo.tblGrading_CheckingReturns.VgCts + dbo.tblGrading_CheckingReturns.PsCts + dbo.tblGrading_CheckingReturns.SzCts + dbo.tblGrading_CheckingReturns.OkCts + dbo.tblGrading_CheckingReturns.BlCts + dbo.tblGrading_CheckingReturns.ScCts), 3) AS Cts " & _
                              "FROM dbo.tblGrading_CheckingReturns INNER JOIN dbo.tblGradingTrf ON dbo.tblGrading_CheckingReturns.Department = dbo.tblGradingTrf.Department AND dbo.tblGrading_CheckingReturns.ParNo = dbo.tblGradingTrf.ParcelNo AND " & _
                                "dbo.tblGrading_CheckingReturns.PktNo = dbo.tblGradingTrf.PktNo " & _
                              "WHERE (dbo.tblGrading_CheckingReturns.Sec = 3) AND (LEFT(dbo.tblGrading_CheckingReturns.ParNo, 6) = '" & txtParNo.Text & "') AND (LEFT(dbo.tblGrading_CheckingReturns.PktNo, 1) <> 'P') AND " & _
                                "(dbo.tblGrading_CheckingReturns.Department = 'Rounds') AND (dbo.tblGradingTrf.Grp = '" & txtGroup.Text & "')", AdoCN, 1, 1)

            Case optRepair.Checked
                intType = 2

                rsComSql.Open("SELECT SUM(dbo.tblGrading_CheckingReturns.ExPcs + dbo.tblGrading_CheckingReturns.VgPcs + dbo.tblGrading_CheckingReturns.PsPcs + dbo.tblGrading_CheckingReturns.SzPcs + dbo.tblGrading_CheckingReturns.OkPcs + dbo.tblGrading_CheckingReturns.BlPcs + dbo.tblGrading_CheckingReturns.ScPcs) AS Pcs, " & _
                                "ROUND(SUM(dbo.tblGrading_CheckingReturns.ExCts + dbo.tblGrading_CheckingReturns.VgCts + dbo.tblGrading_CheckingReturns.PsCts + dbo.tblGrading_CheckingReturns.SzCts + dbo.tblGrading_CheckingReturns.OkCts + dbo.tblGrading_CheckingReturns.BlCts + dbo.tblGrading_CheckingReturns.ScCts), 3) AS Cts " & _
                              "FROM dbo.tblGrading_CheckingReturns INNER JOIN dbo.tblGradingTrf ON dbo.tblGrading_CheckingReturns.Department = dbo.tblGradingTrf.Department AND dbo.tblGrading_CheckingReturns.ParNo = dbo.tblGradingTrf.ParcelNo AND " & _
                                "dbo.tblGrading_CheckingReturns.PktNo = dbo.tblGradingTrf.PktNo " & _
                              "WHERE (dbo.tblGrading_CheckingReturns.Sec = 3) AND (LEFT(dbo.tblGrading_CheckingReturns.ParNo, 6) = '" & txtParNo.Text & "') AND (LEFT(dbo.tblGrading_CheckingReturns.PktNo, 1) = 'P') AND " & _
                                "(dbo.tblGrading_CheckingReturns.Department = 'Rounds') AND (dbo.tblGradingTrf.Grp = '" & txtGroup.Text & "')", AdoCN, 1, 1)

        End Select
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                txtTotPcs.Text = rsComSql.Fields("Pcs").Value
                txtTotCts.Text = rsComSql.Fields("Cts").Value
            Else
                txtTotPcs.Text = "0"
                txtTotCts.Text = "0"
            End If
        Else
            txtTotPcs.Text = "0"
            txtTotCts.Text = "0"
        End If
        rsComSql = Nothing

        dblRghPcs = 0
        dblRghCts = 0
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT SUM(IssPcs) AS IssPcs,ROUND(SUM(IssCts), 3) AS IssCts  FROM tblGrading_RghIssues WHERE Department = 'Rounds' AND LEFT(ParNo, 6) = '" & txtParNo.Text & "' AND Type = 'R' AND Type2 = " & intType & "", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("IssPcs").Value) Then
                dblRghPcs = rsComSql.Fields("IssPcs").Value
                dblRghCts = rsComSql.Fields("IssCts").Value
            End If
        End If
        rsComSql = Nothing

        txtRghPcs.Text = dblRghPcs
        txtRghCts.Text = Format(dblRghCts, "#0.000")

        flxDetails.Rows.Clear()
        flxBox.Rows.Clear()
        txtPktPcs.Text = "0"
        txtPktCts.Text = "0"
        dblPlanCts = 0
        strPlanClarity = ""
        strPlanColor = ""
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblGrading_Box WHERE Department = 'Rounds' AND ParNo = '" & txtParNo.Text & "' AND Type = " & intType & " AND ParGroup = '" & txtGroup.Text & "' ORDER BY BoxNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                dblPlanCts = 0
                strPlanClarity = ""
                strPlanColor = ""

                flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("BoxNo").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    Format(rsComSql.Fields("Cts").Value, "#0.000"),
                                    Format(rsComSql.Fields("BoxDate").Value, "yyyy/MM/dd"),
                                    Format(rsComSql.Fields("BoxTime").Value, "hh:mm"),
                                    rsComSql.Fields("TypeName").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("ParGroup").Value,
                                    rsComSql.Fields("Clarity").Value,
                                    Format(dblPlanCts, "#0.000"),
                                    strPlanClarity,
                                    strPlanColor,
                                    Format(rsComSql.Fields("ActCts").Value, "#0.000"))

                txtPktPcs.Text = CDbl(txtPktPcs.Text) + rsComSql.Fields("Pcs").Value
                txtPktCts.Text = CDbl(txtPktCts.Text) + rsComSql.Fields("Cts").Value
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
        txtPktCts.Text = Format(Math.Round(CDbl(txtPktCts.Text), 3), "#0.000")

        txtBalPcs.Text = CDbl(txtTotPcs.Text) - CDbl(txtPktPcs.Text)
        txtBalCts.Text = CDbl(txtTotCts.Text) - CDbl(txtPktCts.Text)
        txtBalCts.Text = Math.Round(CDbl(txtBalCts.Text), 3)

        txtPcs.Focus()
    End Sub

    Private Function GetBoxNo() As Integer
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(BoxNo) AS MaxBoxNo FROM tblGrading_Box WHERE Department = 'Rounds' AND ParNo = '" & txtParNo.Text & "' AND ParGroup = '" & txtGroup.Text & "'", AdoCN, 1, 1)
        If Not IsDBNull(rsComSql.Fields("MaxBoxNo").Value) Then
            GetBoxNo = rsComSql.Fields("MaxBoxNo").Value + 1
        Else
            GetBoxNo = 1
        End If
        rsComSql = Nothing
    End Function

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtParNo.Text = UCase(txtParNo.Text)
            Load_GradingBox()
        End If

    End Sub

    Private Sub frm_GRDBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim strType As String

        If txtParNo.Text = "" Then MsgBox("Invalid Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtGroup.Text = "" Then MsgBox("Invalid Group", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtBoxNo.Text = "" Then MsgBox("Invalid Box No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtPcs.Text = "" Or txtCts.Text = "" Or txtActCts.Text = "" Then MsgBox("Invalid Entries", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtPcs.Text) <= 0 Or CDbl(txtCts.Text) <= 0 Or CDbl(txtActCts.Text) <= 0 Then MsgBox("Invalid Entries", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtPcs.Text) > CDbl(txtBalPcs.Text) Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtCts.Text) > CDbl(txtBalCts.Text) Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        strType = ""
        If CDbl(txtPcs.Text) > 100 Then
            MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        Else
            If CDbl(txtPcs.Text) = CDbl(txtBalPcs.Text) Then
                If CDbl(txtCts.Text) <> CDbl(txtBalCts.Text) Then
                    MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            Select Case True
                Case optNiru.Checked
                    strType = "N & C"
                Case optRepair.Checked
                    strType = "Repair OK"
            End Select

            flxBox.Rows.Add(txtParNo.Text,
                            txtBoxNo.Text,
                            txtPcs.Text,
                            Format(CDbl(txtCts.Text), "#0.000"),
                            strType,
                            "",
                            txtGroup.Text,
                            "",
                            txtActCts.Text)

            txtBoxNo.Text = CDbl(txtBoxNo.Text) + 1
            txtBalPcs.Text = CDbl(txtBalPcs.Text) - CDbl(txtPcs.Text)
            txtBalCts.Text = Format(Math.Round(CDbl(txtBalCts.Text) - CDbl(txtCts.Text), 3), "#0.000")
            txtPcs.Text = ""
            txtCts.Text = ""
            txtActCts.Text = ""

            txtPcs.Focus()
        End If
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub RemoveLast()
        flxBox.Rows.RemoveAt(flxBox.Rows.Count - 1)
        txtBalPcs.Text = CDbl(txtTotPcs.Text) - CDbl(txtPktPcs.Text) - CalTotalPcs(flxBox)
        txtBalCts.Text = Format(CDbl(txtTotCts.Text) - CDbl(txtPktCts.Text) - CalTotalCts(flxBox), "#0.000")
        txtBoxNo.Text = CDbl(txtBoxNo.Text) - 1
    End Sub

    Private Sub cmdRemove_Click(sender As Object, e As EventArgs) Handles cmdRemove.Click
        RemoveLast()
    End Sub

    Private Sub txtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtCts.Focus()
        End If
    End Sub

    Private Sub txtCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtActCts.Text = txtCts.Text
        End If
    End Sub

    Private Sub txtGroup_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGroup.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtGroup.Text = UCase(txtGroup.Text)
            txtParNo.Focus()
        End If
    End Sub

    Private Sub txtActCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtActCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtActCts.Text)
        If Asc(e.KeyChar) = 13 Then
            cmdAdd.Focus()
        End If
    End Sub
End Class