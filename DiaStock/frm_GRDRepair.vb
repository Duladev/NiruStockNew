
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDRepair

    Private Sub frm_GRDRepair_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_DepartmentProd(cmbDepartment)
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ClearFields()
        'cmbDepartment.Text = ""
        txtParNo.Text = ""
        txtTotPcs.Text = ""
        txtTotCts.Text = ""
        txtPktNo.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtIssPcs.Text = ""
        txtIssCts.Text = ""
        flxDetails.Rows.Clear()
        flxSelect.Rows.Clear()
        txtNewPkt.Text = ""
        txtPktPcs.Text = ""
        txtPktCts.Text = ""
        txtBalPcs.Text = ""
        txtBalCts.Text = ""
        cmbOrderNo.Text = ""
        cmbOrderNo.Items.Clear()
        cmbRef.Text = ""
        cmbRef.Items.Clear()
        cmbSide.Text = ""
        cmbSide.Items.Clear()
        txtActCts.Text = ""
        txtGroup.Text = ""
        txtRate.Text = ""
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(1, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(2, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        Dim intRow As Integer

        If cmbDepartment.Text <> "" Then
            For intRow = 0 To flxSelect.Rows.Count - 1
                If flxDetails.Item(0, flxDetails.CurrentRow.Index).Value = flxSelect.Item(0, intRow).Value Then
                    MsgBox("Already Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If flxDetails.Item(7, flxDetails.CurrentRow.Index).Value <> flxSelect.Item(5, intRow).Value Then
                    MsgBox("Invalid Original Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Next
            txtOrgPkt.Text = flxDetails.Item(7, flxDetails.CurrentRow.Index).Value
            If txtGroup.Text = "" Then
                txtGroup.Text = flxDetails.Item(6, flxDetails.CurrentRow.Index).Value
            Else
                If txtGroup.Text <> flxDetails.Item(6, flxDetails.CurrentRow.Index).Value Then
                    MsgBox("Invalid Group", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            If cmbDepartment.Text = "GradingPCU_N" Then
                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT OrderNo, RefNo, Side, RateCode FROM dbo.tblGradingTrf " & _
                                "WHERE (Department = '" & cmbDepartment.Text & "') AND (ParcelNo = '" & txtParNo.Text & "') AND (PktNo = '" & flxDetails.Item(7, flxDetails.CurrentRow.Index).Value & "')", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    cmbOrderNo.Text = rsComSql_2.Fields("OrderNo").Value
                    cmbRef.Text = rsComSql_2.Fields("RefNo").Value
                    cmbSide.Text = rsComSql_2.Fields("Side").Value
                    txtRate.Text = rsComSql_2.Fields("RateCode").Value
                End If
                rsComSql_2 = Nothing
            End If

            flxSelect.Rows.Add(flxDetails.Item(0, flxDetails.CurrentRow.Index).Value,
                               IIf(flxDetails.Item(4, flxDetails.CurrentRow.Index).Value <> 0, flxDetails.Item(4, flxDetails.CurrentRow.Index).Value, flxDetails.Item(1, flxDetails.CurrentRow.Index).Value),
                               IIf(flxDetails.Item(5, flxDetails.CurrentRow.Index).Value <> 0, flxDetails.Item(5, flxDetails.CurrentRow.Index).Value, flxDetails.Item(2, flxDetails.CurrentRow.Index).Value),
                               flxDetails.Item(3, flxDetails.CurrentRow.Index).Value,
                               flxDetails.Item(6, flxDetails.CurrentRow.Index).Value,
                               flxDetails.Item(7, flxDetails.CurrentRow.Index).Value)

            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)

            txtPktPcs.Text = CalTotalPcs(flxSelect)
            txtPktCts.Text = CalTotalCts(flxSelect)
            txtActCts.Text = txtPktCts.Text
        End If
    End Sub

    Private Sub flxSelect_DoubleClick(sender As Object, e As EventArgs) Handles flxSelect.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxSelect.Rows.RemoveAt(flxSelect.CurrentRow.Index)
            txtPktPcs.Text = CalTotalPcs(flxSelect)
            txtPktCts.Text = CalTotalCts(flxSelect)
        End If
    End Sub

    Private Sub cmdSelect_Click(sender As Object, e As EventArgs) Handles cmdSelect.Click
        Dim intRow As Integer

        For intRow = 0 To flxDetails.Rows.Count - 1
            flxSelect.Rows.Add(flxDetails.Item(0, intRow).Value,
                               flxDetails.Item(1, intRow).Value,
                               flxDetails.Item(2, intRow).Value,
                               flxDetails.Item(3, intRow).Value,
                               flxDetails.Item(6, intRow).Value)
        Next
        flxDetails.Rows.Clear()
        txtPktPcs.Text = CalTotalPcs(flxSelect)
        txtPktCts.Text = CalTotalCts(flxSelect)
        txtActCts.Text = txtPktCts.Text
    End Sub

    Private Sub cmbOrderNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbOrderNo.SelectedIndexChanged
        cmbRef.Text = ""
        cmbRef.Items.Clear()
        cmbSide.Text = ""
        cmbSide.Items.Clear()
        If cmbDepartment.Text = "GradingPCU_N" Or cmbDepartment.Text = "GradingPCU" Then
            If cmbOrderNo.Text <> "" Then
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT OrderNo, RefNo FROM dbo.tblGradingTrf " & _
                                "WHERE (Department = '" & cmbDepartment.Text & "') AND (ParcelNo = '" & txtParNo.Text & "') AND (OrderNo = '" & cmbOrderNo.Text & "') " & _
                                "GROUP BY OrderNo, RefNo", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    rsComSql_1.MoveFirst()
                    While Not rsComSql_1.EOF
                        cmbRef.Items.Add(rsComSql_1.Fields("RefNo").Value)

                        rsComSql_1.MoveNext()
                    End While
                End If
                rsComSql_1 = Nothing
            End If
        End If
    End Sub

    Private Sub cmbRef_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRef.SelectedIndexChanged
        cmbSide.Text = ""
        cmbSide.Items.Clear()
        If cmbDepartment.Text = "GradingPCU_N" Or cmbDepartment.Text = "GradingPCU" Then
            If cmbOrderNo.Text <> "" Then
                If cmbRef.Text <> "" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT OrderNo, RefNo, Side FROM dbo.tblGradingTrf " & _
                                    "WHERE (Department = '" & cmbDepartment.Text & "') AND (ParcelNo = '" & txtParNo.Text & "') AND (OrderNo = '" & cmbOrderNo.Text & "') AND (RefNo = '" & cmbRef.Text & "') " & _
                                    "GROUP BY OrderNo, RefNo, Side", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        rsComSql_1.MoveFirst()
                        While Not rsComSql_1.EOF
                            cmbSide.Items.Add(rsComSql_1.Fields("Side").Value)

                            rsComSql_1.MoveNext()
                        End While
                    End If
                    rsComSql_1 = Nothing
                End If
            End If
        End If
    End Sub

    Private Sub Save()
        Dim dataok As Boolean

        dataok = True
        If cmbDepartment.Text = "" Then
            MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If txtParNo.Text = "" Then
            MsgBox("Invalid Parcel", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If txtPktNo.Text = "" Then
            MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If txtPcs.Text = "" Then
            MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If txtCts.Text = "" Then
            MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If CInt(txtTotPcs.Text) < CInt(txtIssPcs.Text) + CInt(txtPcs.Text) Then
            MsgBox("Pcs Exceeds", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If CDbl(txtTotCts.Text) < CDbl(txtIssCts.Text) + CDbl(txtCts.Text) Then
            MsgBox("Pcs Exceeds", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If dataok = True Then
            AdoCN.Execute("INSERT INTO tblGrading_RepairParcels(Department,ParNo,PktNo,Pcs,Cts,IssDate,IssTime,OK) " & _
                          "VALUES('" & cmbDepartment.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "'," & CInt(txtPcs.Text) & "," & Val(txtCts.Text) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0)")
        End If
        ClearFields()

    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        Dim intPktLen As Integer

        If optNormal.Checked = True Then
            intPktLen = 4
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT MAX(LEN(PktNo)) AS PktLen FROM tblGrading_RepairParcelsA WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDepartment.Text & "' AND LEFT(PktNo, 1) = 'P'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("PktLen").Value) Then
                    intPktLen = rsComSql.Fields("PktLen").Value
                End If
            End If
            rsComSql = Nothing

            If intPktLen = 4 Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT MAX(RIGHT(PktNo,3)) AS MaxPktNo FROM tblGrading_RepairParcelsA WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDepartment.Text & "' AND LEFT(PktNo, 1) = 'P' AND LEN(PktNo) = 4", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("MaxPktNo").Value) Then
                        txtNewPkt.Text = "P" & Format(rsComSql.Fields("MaxPktNo").Value + 1, "000")
                    Else
                        txtNewPkt.Text = "P001"
                    End If
                End If
                rsComSql = Nothing

            Else
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT MAX(RIGHT(PktNo,4)) AS MaxPktNo FROM tblGrading_RepairParcelsA WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDepartment.Text & "' AND LEFT(PktNo, 1) = 'P' AND LEN(PktNo) = 5", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("MaxPktNo").Value) Then
                        txtNewPkt.Text = "P" & Format(rsComSql.Fields("MaxPktNo").Value + 1, "0000")
                    Else
                        txtNewPkt.Text = "P0001"
                    End If
                End If
                rsComSql = Nothing

            End If

        ElseIf optVs.Checked = True Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT MAX(RIGHT(PktNo,3)) AS MaxPktNo FROM tblGrading_RepairParcelsA WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDepartment.Text & "' AND LEFT(PktNo, 1) = 'V'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("MaxPktNo").Value) Then
                    txtNewPkt.Text = "V" & Format(rsComSql.Fields("MaxPktNo").Value + 1, "000")
                Else
                    txtNewPkt.Text = "V001"
                End If
            End If
            rsComSql = Nothing

        ElseIf optSize.Checked = True Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT MAX(RIGHT(PktNo,3)) AS MaxPktNo FROM tblGrading_RepairParcelsA WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDepartment.Text & "' AND LEFT(PktNo, 1) = 'Z'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("MaxPktNo").Value) Then
                    txtNewPkt.Text = "Z" & Format(rsComSql.Fields("MaxPktNo").Value + 1, "000")
                Else
                    txtNewPkt.Text = "Z001"
                End If
            End If
            rsComSql = Nothing

        End If
    End Sub

    Private Sub SavePacket()
        Dim dataok As Boolean
        Dim intRow As Integer
        Dim dblDiffCts As Double
        Dim dblMaxID As Double

        dataok = True
        If cmbDepartment.Text = "" Then
            MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If txtParNo.Text = "" Then
            MsgBox("Invalid Parcel", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If txtNewPkt.Text = "" Then
            MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If txtOrgPkt.Text = "" Then
            MsgBox("Invalid Org Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If txtPktPcs.Text = "" Then
            MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If txtPktCts.Text = "" Then
            MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If cmbDepartment.Text = "Rounds" Then
            If txtGroup.Text = "" Then
                MsgBox("Invalid Group", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub
        End If

        If cmbDepartment.Text = "GradingPCU_N" Or cmbDepartment.Text = "GradingPCU" Then
            If cmbOrderNo.Text = "" Then
                MsgBox("Invalid Order No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub

            If cmbRef.Text = "" Then
                MsgBox("Invalid Ref", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub

            If cmbSide.Text = "" Then
                MsgBox("Invalid Side", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub
        End If

        If dataok = True Then
            dblDiffCts = 0

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_RepairParcelsA WHERE Department = '" & cmbDepartment.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo ='" & txtNewPkt.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then

                AdoCN.Execute("INSERT INTO tblGrading_RepairParcelsA(Department,ParNo,PktNo,Pcs,Cts,IssDate,IssTime,OK,Grp,PktNo2) " & _
                              "VALUES('" & cmbDepartment.Text & "','" & txtParNo.Text & "','" & txtNewPkt.Text & "'," & CDbl(txtPktPcs.Text) & "," & CDbl(txtPktCts.Text) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,'" & UCase(txtGroup.Text) & "','" & txtOrgPkt.Text & "')")

                For intRow = 0 To flxSelect.Rows.Count - 1
                    AdoCN.Execute("UPDATE tblGrading_RepairParcels SET OK = 1 WHERE Department = '" & cmbDepartment.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & flxSelect.Item(0, intRow).Value & "'")
                Next

                Call Dep_Grading_Trf(cmbDepartment.Text, 9996, txtParNo.Text, txtNewPkt.Text, txtPktPcs.Text, txtPktCts.Text, txtPktPcs.Text, txtPktCts.Text, UCase(txtGroup.Text))

                If cmbDepartment.Text = "GradingPCU_N" Or cmbDepartment.Text = "GradingPCU" Then
                    AdoCN.Execute("UPDATE tblGradingTrf SET OrderNo = '" & cmbOrderNo.Text & "',RefNo = '" & cmbRef.Text & "',Side = '" & cmbSide.Text & "', RateCode = '" & txtRate.Text & "' " & _
                                  "WHERE Department = '" & cmbDepartment.Text & "' AND ParcelNo = '" & txtParNo.Text & "' AND PktNo = '" & txtNewPkt.Text & "'")

                ElseIf cmbDepartment.Text = "Rounds" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblGradingTrf_Diff WHERE Depart = '" & cmbDepartment.Text & "' AND ParcelNo = '" & txtParNo.Text & "' AND TrDate = '" & Format(Date.Now, "MM/dd/yyyy") & "' AND Pcs = " & CDbl(txtPktPcs.Text) & "", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount = 0 Then
                        AdoCN.Execute("INSERT INTO tblGradingTrf_Diff(Depart,ParcelNo,Pcs,Cts,ActPcs,ActCts,TrDate) " & _
                                      "VALUES('" & cmbDepartment.Text & "','" & txtParNo.Text & "','" & CDbl(txtPktPcs.Text) & "','" & CDbl(txtPktCts.Text) & "','" & CDbl(txtPktPcs.Text) & "','" & CDbl(txtPktCts.Text) & "','" & Format(Date.Now, "MM/dd/yyyy") & "')")
                    End If
                    rsComSql_1 = Nothing

                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT MAX(ID) AS MaxID FROM tblGradingTrf_Diff", AdoCN, 1, 1)
                    dblMaxID = rsComSql_1.Fields("MaxID").Value
                    rsComSql_1 = Nothing

                    GradingAcceptations(txtParNo.Text, txtNewPkt.Text, dblMaxID, 0)

                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblGrading_BoilingIssues WHERE Department = '" & cmbDepartment.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtNewPkt.Text & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount = 0 Then
                        'Boiling Issues
                        AdoCN.Execute("INSERT INTO tblGrading_BoilingIssues(Department,ParNo,PktNo,EmpNo,IssPcs,IssCts,IssDate,IssTime,UserName,Remarks,Grp) " & _
                                      "VALUES('" & cmbDepartment.Text & "','" & txtParNo.Text & "','" & txtNewPkt.Text & "','D08877'," & CInt(txtPktPcs.Text) & "," & _
                                             "" & CDbl(txtPktCts.Text) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "','','" & Trim(txtGroup.Text) & "')")
                    End If
                    rsComSql_1 = Nothing
                End If
            Else
                MsgBox("Packet Already Created", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            rsComSql = Nothing
        End If
        ClearFields()

    End Sub

    Private Sub cmdSavePkt_Click(sender As Object, e As EventArgs) Handles cmdSavePkt.Click
        SavePacket()
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        Dim blnBigStone As Boolean
        Dim intPktLen As Integer
        Dim strOrderNo As String

        If Asc(e.KeyChar) = 13 Then
            flxDetails.Rows.Clear()
            txtParNo.Text = UCase(txtParNo.Text)
            blnBigStone = False

            If (cmbDepartment.Text = "Rounds" Or cmbDepartment.Text = "RoundsNLE") And (strRight(txtParNo.Text, 1) = "Z" Or strRight(txtParNo.Text, 1) = "P" Or strRight(txtParNo.Text, 1) = "T") Then
                blnBigStone = True
                txtPktNo.ReadOnly = False
                txtNewPkt.ReadOnly = False
            Else
                txtPktNo.ReadOnly = True
                txtNewPkt.ReadOnly = True
            End If

            cmbOrderNo.Text = ""
            cmbOrderNo.Items.Clear()
            cmbRef.Text = ""
            cmbRef.Items.Clear()
            cmbSide.Text = ""
            cmbSide.Items.Clear()

            If cmbDepartment.Text = "GradingPCU_N" Or cmbDepartment.Text = "GradingPCU" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT OrderNo FROM dbo.tblGradingTrf " & _
                              "WHERE (Department = '" & cmbDepartment.Text & "') AND (ParcelNo = '" & txtParNo.Text & "') AND (OrderNo <> '') " & _
                              "GROUP BY OrderNo " & _
                              "ORDER BY OrderNo", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        cmbOrderNo.Items.Add(rsComSql.Fields("OrderNo").Value)

                        rsComSql.MoveNext()
                    End While
                End If
                rsComSql = Nothing

            End If

            If optNormal.Checked = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT MAX(RIGHT(PktNo,4)) AS MaxPktNo FROM tblGrading_RepairParcels WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDepartment.Text & "' AND LEFT(PktNo, 1) = 'R'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("MaxPktNo").Value) Then
                        txtPktNo.Text = "R" & Format(rsComSql.Fields("MaxPktNo").Value + 1, "0000")
                    Else
                        txtPktNo.Text = "R0001"
                    End If
                End If
                rsComSql = Nothing

                intPktLen = 4
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT MAX(LEN(PktNo)) AS PktLen FROM tblGrading_RepairParcelsA WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDepartment.Text & "' AND LEFT(PktNo, 1) = 'P'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("PktLen").Value) Then
                        intPktLen = rsComSql.Fields("PktLen").Value
                    End If
                End If
                rsComSql = Nothing

                If intPktLen = 4 Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT MAX(RIGHT(PktNo,3)) AS MaxPktNo FROM tblGrading_RepairParcelsA WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDepartment.Text & "' AND LEFT(PktNo, 1) = 'P' AND LEN(PktNo) = 4", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If Not IsDBNull(rsComSql.Fields("MaxPktNo").Value) Then
                            txtNewPkt.Text = "P" & Format(rsComSql.Fields("MaxPktNo").Value + 1, "000")
                        Else
                            txtNewPkt.Text = "P001"
                        End If
                    End If
                    rsComSql = Nothing

                ElseIf intPktLen = 5 Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT MAX(RIGHT(PktNo,4)) AS MaxPktNo FROM tblGrading_RepairParcelsA WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDepartment.Text & "' AND LEFT(PktNo, 1) = 'P' AND LEN(PktNo) = 5", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If Not IsDBNull(rsComSql.Fields("MaxPktNo").Value) Then
                            txtNewPkt.Text = "P" & Format(rsComSql.Fields("MaxPktNo").Value + 1, "0000")
                        Else
                            txtNewPkt.Text = "P0001"
                        End If
                    End If
                    rsComSql = Nothing
                Else
                    MsgBox("Please contact the IT Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT TOP (100) PERCENT SUM(RepPcs) AS RepPcs, ROUND(SUM(RepCts), 3) AS RepCts, Sec " & _
                              "FROM dbo.tblGrading_CheckingReturns " & _
                              "WHERE (ParNo = '" & txtParNo.Text & "') AND (Department = '" & cmbDepartment.Text & "') " & _
                              "GROUP BY Sec " & _
                              "ORDER BY Sec DESC", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    Select Case cmbDepartment.Text
                        Case "Rounds", "Niru", "RoundsNLE"
                            If rsComSql.Fields("Sec").Value = 3 Then
                                txtTotPcs.Text = rsComSql.Fields("RepPcs").Value
                                txtTotCts.Text = rsComSql.Fields("RepCts").Value
                            Else
                                MsgBox("No Repair Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                ClearFields()
                                Exit Sub
                            End If
                        Case Else
                            If rsComSql.Fields("Sec").Value = 2 Then
                                txtTotPcs.Text = rsComSql.Fields("RepPcs").Value
                                txtTotCts.Text = rsComSql.Fields("RepCts").Value
                            Else
                                MsgBox("No Repair Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                ClearFields()
                                Exit Sub
                            End If
                    End Select
                End If
                rsComSql = Nothing

                strOrderNo = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblGrading_RepairParcels.PktNo, dbo.tblGrading_RepairParcels.Pcs, dbo.tblGrading_RepairParcels.Cts, " & _
                                "dbo.tblGrading_RepairParcels.IssDate, ISNULL(dbo.tblFinalRepReturns.RetPcs, 0) AS RetPcs, ISNULL(dbo.tblFinalRepReturns.RetCts, 0) AS RetCts, dbo.tblGrading_RepairParcels.Grp, dbo.tblGrading_RepairParcels.PktNo2 " & _
                              "FROM dbo.tblGrading_RepairParcels LEFT OUTER JOIN dbo.tblFinalRepReturns ON dbo.tblGrading_RepairParcels.Department = dbo.tblFinalRepReturns.Department AND " & _
                                "dbo.tblGrading_RepairParcels.ParNo = dbo.tblFinalRepReturns.ParNo And dbo.tblGrading_RepairParcels.PktNo = dbo.tblFinalRepReturns.PktNo " & _
                              "WHERE (dbo.tblGrading_RepairParcels.Department = '" & cmbDepartment.Text & "') AND (dbo.tblGrading_RepairParcels.OK = 0) AND (LEFT(dbo.tblGrading_RepairParcels.PktNo, 1) = 'R') AND " & _
                                "(dbo.tblGrading_RepairParcels.ParNo = '" & txtParNo.Text & "') " & _
                              "ORDER BY dbo.tblGrading_RepairParcels.PktNo", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        strOrderNo = ""
                        If cmbDepartment.Text = "GradingPCU_N" Then
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT OrderNo FROM dbo.tblGradingTrf " & _
                                            "WHERE (Department = '" & cmbDepartment.Text & "') AND (ParcelNo = '" & txtParNo.Text & "') AND (PktNo = '" & rsComSql.Fields("PktNo2").Value & "') " & _
                                            "GROUP BY OrderNo", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount Then
                                strOrderNo = rsComSql_1.Fields("OrderNo").Value
                            End If
                            rsComSql_1 = Nothing
                        End If
                        flxDetails.Rows.Add(rsComSql.Fields("PktNo").Value,
                                            rsComSql.Fields("Pcs").Value,
                                            rsComSql.Fields("Cts").Value,
                                            Format(rsComSql.Fields("IssDate").Value, "dd/MM/yyyy"),
                                            rsComSql.Fields("RetPcs").Value,
                                            rsComSql.Fields("RetCts").Value,
                                            rsComSql.Fields("Grp").Value,
                                            rsComSql.Fields("PktNo2").Value,
                                            strOrderNo)
                        rsComSql.MoveNext()
                    End While
                End If
                rsComSql = Nothing

                txtIssPcs.Text = CalTotalPcs(flxDetails)
                txtIssCts.Text = CalTotalCts(flxDetails)

                txtBalPcs.Text = txtTotPcs.Text
                txtBalCts.Text = txtTotCts.Text
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(Pcs) as Pcs, SUM(Cts) AS Cts FROM tblGrading_RepairParcelsA WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDepartment.Text & "' AND LEFT(PktNo, 1) = 'P'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                        txtBalPcs.Text = CDbl(txtTotPcs.Text) - rsComSql.Fields("Pcs").Value
                        txtBalCts.Text = CDbl(txtTotCts.Text) - rsComSql.Fields("Cts").Value
                        txtBalCts.Text = Math.Round(CDbl(txtBalCts.Text), 3)
                    End If
                End If
                rsComSql = Nothing

            ElseIf optVs.Checked = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT MAX(RIGHT(PktNo,4)) AS MaxPktNo FROM tblGrading_RepairParcels WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDepartment.Text & "' AND LEFT(PktNo, 1) = 'T'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("MaxPktNo").Value) Then
                        txtPktNo.Text = "T" & Format(rsComSql.Fields("MaxPktNo").Value + 1, "0000")
                    Else
                        txtPktNo.Text = "T0001"
                    End If
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT MAX(RIGHT(PktNo,3)) AS MaxPktNo FROM tblGrading_RepairParcelsA WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDepartment.Text & "' AND LEFT(PktNo, 1) = 'V'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("MaxPktNo").Value) Then
                        txtNewPkt.Text = "V" & Format(rsComSql.Fields("MaxPktNo").Value + 1, "000")
                    Else
                        txtNewPkt.Text = "V001"
                    End If
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT TOP (100) PERCENT SUM(VRepPcs) AS RepPcs, ROUND(SUM(VRepCts), 3) AS RepCts, Sec " & _
                              "FROM dbo.tblGrading_CheckingReturns " & _
                              "WHERE (ParNo = '" & txtParNo.Text & "') AND (Department = '" & cmbDepartment.Text & "') " & _
                              "GROUP BY Sec " & _
                              "ORDER BY Sec DESC", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    Select Case cmbDepartment.Text
                        Case "Rounds", "Niru", "RoundsNLE"
                            If rsComSql.Fields("Sec").Value = 3 Then
                                txtTotPcs.Text = rsComSql.Fields("RepPcs").Value
                                txtTotCts.Text = rsComSql.Fields("RepCts").Value
                            Else
                                MsgBox("No Repair Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                ClearFields()
                                Exit Sub
                            End If
                        Case Else
                            If rsComSql.Fields("Sec").Value = 2 Then
                                txtTotPcs.Text = rsComSql.Fields("RepPcs").Value
                                txtTotCts.Text = rsComSql.Fields("RepCts").Value
                            Else
                                MsgBox("No Repair Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                ClearFields()
                                Exit Sub
                            End If
                    End Select
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblGrading_RepairParcels WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDepartment.Text & "' AND OK = 0 AND LEFT(PktNo, 1) = 'T' ORDER BY PktNo", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        flxDetails.Rows.Add(rsComSql.Fields("PktNo").Value,
                                            rsComSql.Fields("Pcs").Value,
                                            rsComSql.Fields("Cts").Value,
                                            Format(rsComSql.Fields("IssDate").Value, "dd/MM/yyyy"))
                        rsComSql.MoveNext()
                    End While
                End If
                rsComSql = Nothing

                txtIssPcs.Text = CalTotalPcs(flxDetails)
                txtIssCts.Text = CalTotalCts(flxDetails)

                txtBalPcs.Text = txtTotPcs.Text
                txtBalCts.Text = txtTotCts.Text
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(Pcs) as Pcs, SUM(Cts) AS Cts FROM tblGrading_RepairParcelsA WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDepartment.Text & "' AND LEFT(PktNo, 1) = 'V'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                        txtBalPcs.Text = CDbl(txtTotPcs.Text) - rsComSql.Fields("Pcs").Value
                        txtBalCts.Text = CDbl(txtTotCts.Text) - rsComSql.Fields("Cts").Value
                        txtBalCts.Text = Math.Round(CDbl(txtBalCts.Text), 3)
                    End If
                End If
                rsComSql = Nothing

            ElseIf optSize.Checked = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT MAX(RIGHT(PktNo,3)) AS MaxPktNo FROM tblGrading_RepairParcels WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDepartment.Text & "' AND LEFT(PktNo, 1) = 'U'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("MaxPktNo").Value) Then
                        txtPktNo.Text = "U" & Format(rsComSql.Fields("MaxPktNo").Value + 1, "0000")
                    Else
                        txtPktNo.Text = "U0001"
                    End If
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT MAX(RIGHT(PktNo,3)) AS MaxPktNo FROM tblGrading_RepairParcelsA WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDepartment.Text & "' AND LEFT(PktNo, 1) = 'Z'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("MaxPktNo").Value) Then
                        txtNewPkt.Text = "Z" & Format(rsComSql.Fields("MaxPktNo").Value + 1, "000")
                    Else
                        txtNewPkt.Text = "Z001"
                    End If
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT TOP (100) PERCENT SUM(RepPcs) AS RepPcs, ROUND(SUM(RepCts), 3) AS RepCts, Sec " & _
                              "FROM dbo.tblGrading_SizingReturns " & _
                              "WHERE (ParNo = '" & txtParNo.Text & "') AND (Department = '" & cmbDepartment.Text & "') " & _
                              "GROUP BY Sec " & _
                              "ORDER BY Sec DESC", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    txtTotPcs.Text = rsComSql.Fields("RepPcs").Value
                    txtTotCts.Text = rsComSql.Fields("RepCts").Value
                Else
                    MsgBox("No Repair Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    ClearFields()
                    Exit Sub
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblGrading_RepairParcels.PktNo, dbo.tblGrading_RepairParcels.Pcs, dbo.tblGrading_RepairParcels.Cts, " & _
                                "dbo.tblGrading_RepairParcels.IssDate, ISNULL(dbo.tblFinalRepReturns.RetPcs, 0) AS RetPcs, ISNULL(dbo.tblFinalRepReturns.RetCts, 0) AS RetCts, dbo.tblGrading_RepairParcels.Grp, dbo.tblGrading_RepairParcels.PktNo2 " & _
                              "FROM dbo.tblGrading_RepairParcels LEFT OUTER JOIN dbo.tblFinalRepReturns ON dbo.tblGrading_RepairParcels.Department = dbo.tblFinalRepReturns.Department AND " & _
                                "dbo.tblGrading_RepairParcels.ParNo = dbo.tblFinalRepReturns.ParNo And dbo.tblGrading_RepairParcels.PktNo = dbo.tblFinalRepReturns.PktNo " & _
                              "WHERE (dbo.tblGrading_RepairParcels.Department = '" & cmbDepartment.Text & "') AND (dbo.tblGrading_RepairParcels.OK = 0) AND (LEFT(dbo.tblGrading_RepairParcels.PktNo, 1) = 'U') AND " & _
                                "(dbo.tblGrading_RepairParcels.ParNo = '" & txtParNo.Text & "') " & _
                              "ORDER BY dbo.tblGrading_RepairParcels.PktNo", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        flxDetails.Rows.Add(rsComSql.Fields("PktNo").Value,
                                            rsComSql.Fields("Pcs").Value,
                                            rsComSql.Fields("Cts").Value,
                                            Format(rsComSql.Fields("IssDate").Value, "dd/MM/yyyy"),
                                            rsComSql.Fields("RetPcs").Value,
                                            rsComSql.Fields("RetCts").Value,
                                            rsComSql.Fields("Grp").Value,
                                            rsComSql.Fields("PktNo2").Value,
                                            "")
                        rsComSql.MoveNext()
                    End While
                End If
                rsComSql = Nothing

                txtIssPcs.Text = CalTotalPcs(flxDetails)
                txtIssCts.Text = CalTotalCts(flxDetails)

                txtBalPcs.Text = txtTotPcs.Text
                txtBalCts.Text = txtTotCts.Text
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(Pcs) as Pcs, SUM(Cts) AS Cts FROM tblGrading_RepairParcelsA WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDepartment.Text & "' AND LEFT(PktNo, 1) = 'Z'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                        txtBalPcs.Text = CDbl(txtTotPcs.Text) - rsComSql.Fields("Pcs").Value
                        txtBalCts.Text = CDbl(txtTotCts.Text) - rsComSql.Fields("Cts").Value
                        txtBalCts.Text = Math.Round(CDbl(txtBalCts.Text), 3)
                    End If
                End If
                rsComSql = Nothing

            End If

            txtPcs.Focus()
        End If
    End Sub
End Class