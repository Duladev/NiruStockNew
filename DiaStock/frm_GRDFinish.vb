
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDFinish

    Private Sub frm_GRDFinish_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub cmbDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDepartment.SelectedIndexChanged
        If cmbDepartment.Text = "" Then Exit Sub

        flxParcel.Rows.Clear()
        flxAvailable.Rows.Clear()
        flxSelected.Rows.Clear()

        'chkRepair.Checked = False
        chkFinish.Checked = False

        txtPcs.Text = ""
        txtCts.Text = ""

        rsComSql = New ADODB.Recordset

        Select Case cmbDepartment.Text
            Case "Princess"
                If chkRepair.Checked = False Then
                    rsComSql.Open("SELECT ParNo,RTRIM(SUBSTRING(ParNo, 7, 1)) AS [Group] FROM dbo.tblPRReturns WHERE Sec = 9 AND Gra_Trf = 0 GROUP BY ParNo,RTRIM(SUBSTRING(ParNo, 7, 1)) ORDER BY RTRIM(SUBSTRING(ParNo, 7, 1))", AdoCN, 1, 1)
                Else
                    rsComSql.Open("SELECT ParNo,RTRIM(SUBSTRING(ParNo, 7, 1))'Group' FROM dbo.tblFinalRepReturns WHERE Trf = 0 AND Department = '" & cmbDepartment.Text & "' group by parno,RTRIM(SUBSTRING(ParNo, 7, 1)) order by RTRIM(SUBSTRING(ParNo, 7, 1))", AdoCN, 1, 1)
                End If
            Case "Baguettes"
                If chkRepair.Checked = False Then
                    rsComSql.Open("SELECT ParNo,RTRIM(SUBSTRING(ParNo, 7, 1))'Group' FROM dbo.tblBAGReturns  WHERE sec=10 and Gra_Trf=0  group by parno,RTRIM(SUBSTRING(ParNo, 7, 1)) order by RTRIM(SUBSTRING(ParNo, 7, 1))", AdoCN, 1, 1)
                Else
                    rsComSql.Open("SELECT ParNo,RTRIM(SUBSTRING(ParNo, 7, 1))'Group' FROM dbo.tblFinalRepReturns WHERE Trf = 0 AND Department = '" & cmbDepartment.Text & "' group by parno,RTRIM(SUBSTRING(ParNo, 7, 1)) order by RTRIM(SUBSTRING(ParNo, 7, 1))", AdoCN, 1, 1)
                End If
            Case "Rounds"
                If chkRepair.Checked = False Then
                    rsComSql.Open("SELECT ParNo,RTRIM(SUBSTRING(ParNo, 8, 1))'Group' FROM dbo.tblRNDReturns where sec=25 and Gra_Trf=0  group by parno,RTRIM(SUBSTRING(ParNo, 8, 1)) order by RTRIM(SUBSTRING(ParNo, 8, 1))", AdoCN, 1, 1)
                Else
                    rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblFinalRepReturns.ParNo, dbo.tblGrading_RepairParcels.Grp AS [Group] " & _
                                    "FROM dbo.tblFinalRepReturns INNER JOIN dbo.tblGrading_RepairParcels ON dbo.tblFinalRepReturns.Department = dbo.tblGrading_RepairParcels.Department AND " & _
                                        "dbo.tblFinalRepReturns.ParNo = dbo.tblGrading_RepairParcels.ParNo And dbo.tblFinalRepReturns.PktNo = dbo.tblGrading_RepairParcels.PktNo " & _
                                    "WHERE (dbo.tblFinalRepReturns.Trf = 0) AND (dbo.tblFinalRepReturns.Department = 'Rounds') " & _
                                    "GROUP BY dbo.tblFinalRepReturns.ParNo, dbo.tblGrading_RepairParcels.Grp " & _
                                    "ORDER BY dbo.tblGrading_RepairParcels.Grp, dbo.tblFinalRepReturns.ParNo", AdoCN, 1, 1)

                End If
            Case "Niru"
                rsComSql.Open("SELECT ParNo,RTRIM(SUBSTRING(ParNo, 8, 1))'Group' FROM dbo.tblNiruReturns WHERE sec=25 and Gra_Trf=0  group by parno,RTRIM(SUBSTRING(ParNo, 7, 1)) order by RTRIM(SUBSTRING(ParNo, 7, 1))", AdoCN, 1, 1)
            Case "Rounds3", "Rounds4", "Rounds6", "Rounds7", "Emerald", "Lamour", "Davinci", "Carrer", "Opening", "Princess2", "Baguettes2", "Baguettes3", "Emerald2", "Emerald3", "Lamour2", "Asscher", "Radiant"
                If chkRepair.Checked = False Then
                    rsComSql.Open("SELECT ParNo,RTRIM(SUBSTRING(ParNo, 7, 1))'Group' FROM dbo.tblExtReturns WHERE sec=25 and Gra_Trf=0 and Department = '" & cmbDepartment.Text & "' group by parno,RTRIM(SUBSTRING(ParNo, 7, 1)) order by RTRIM(SUBSTRING(ParNo, 7, 1))", AdoCN, 1, 1)
                Else
                    rsComSql.Open("SELECT ParNo,RTRIM(SUBSTRING(ParNo, 7, 1))'Group' FROM dbo.tblFinalRepReturns WHERE Trf = 0 AND Department = '" & cmbDepartment.Text & "' group by parno,RTRIM(SUBSTRING(ParNo, 7, 1)) order by RTRIM(SUBSTRING(ParNo, 7, 1))", AdoCN, 1, 1)
                End If
            Case "Mix"
                rsComSql.Open("SELECT DISTINCT ParNo, '' AS [Group] FROM tblExpGrading WHERE (OK = 0) ORDER BY ParNo", AdoCN, 1, 1)
            Case "GradingPCU_N"
                If chkRepair.Checked = True Then
                    rsComSql.Open("SELECT ParNo,RTRIM(SUBSTRING(ParNo, 7, 1))'Group' FROM dbo.tblFinalRepReturns WHERE Trf = 0 AND Department = '" & cmbDepartment.Text & "' group by parno,RTRIM(SUBSTRING(ParNo, 7, 1)) order by RTRIM(SUBSTRING(ParNo, 7, 1))", AdoCN, 1, 1)
                Else
                    Exit Sub
                End If
            Case Else
                MsgBox("Grading transfer is not entitle for this department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
        End Select
        If rsComSql.RecordCount > 0 Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxParcel.Rows.Add(rsComSql.Fields("ParNo").Value,
                                   rsComSql.Fields("Group").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub ClearFields()
        cmbDepartment.Text = ""
        txtSearch.Text = ""
        flxParcel.Rows.Clear()
        flxAvailable.Rows.Clear()
        flxSelected.Rows.Clear()
        txtPcs.Text = ""
        txtCts.Text = ""
        chkRepair.Checked = False
        chkFinish.Checked = False
    End Sub

    Private Sub Load_Packets(ByVal strParcelNo As String, ByVal strGroup As String)

        rsComSql = New ADODB.Recordset
        If cmbDepartment.Text = "Rounds" Then
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblFinalRepReturns.PktNo, SUM(dbo.tblFinalRepReturns.RetPcs) AS RetPcs, ROUND(SUM(dbo.tblFinalRepReturns.RetCts), 3) AS RetCts " & _
                          "FROM dbo.tblFinalRepReturns INNER JOIN dbo.tblGrading_RepairParcels ON dbo.tblFinalRepReturns.Department = dbo.tblGrading_RepairParcels.Department AND " & _
                            "dbo.tblFinalRepReturns.ParNo = dbo.tblGrading_RepairParcels.ParNo And dbo.tblFinalRepReturns.PktNo = dbo.tblGrading_RepairParcels.PktNo " & _
                          "WHERE (dbo.tblFinalRepReturns.Department = '" & cmbDepartment.Text & "') AND (dbo.tblFinalRepReturns.Trf = 0) AND (dbo.tblFinalRepReturns.ParNo = '" & strParcelNo & "') AND " & _
                            "(dbo.tblGrading_RepairParcels.Grp = '" & strGroup & "') " & _
                          "GROUP BY dbo.tblFinalRepReturns.PktNo " & _
                          "ORDER BY dbo.tblFinalRepReturns.PktNo", AdoCN, 1, 1)
        Else
            rsComSql.Open("SELECT PktNo, SUM(RetPcs) AS RetPcs, ROUND(SUM(RetCts), 3) AS RetCts " & _
                          "FROM tblFinalRepReturns " & _
                          "WHERE (Department = '" & cmbDepartment.Text & "') AND (Trf = 0) AND (ParNo = '" & strParcelNo & "') " & _
                          "GROUP BY PktNo ORDER BY PktNo", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxAvailable.Rows.Add(strParcelNo,
                                      rsComSql.Fields("PktNo").Value,
                                      rsComSql.Fields("RetPcs").Value,
                                      rsComSql.Fields("RetCts").Value,
                                      rsComSql.Fields("RetPcs").Value,
                                      rsComSql.Fields("RetCts").Value,
                                      strGroup)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub flxParcel_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxParcel.CellClick
        Dim selected_parno As String
        Dim selected_Grp As String

        selected_parno = flxParcel.Item(0, flxParcel.CurrentRow.Index).Value
        selected_Grp = flxParcel.Item(1, flxParcel.CurrentRow.Index).Value
        flxAvailable.Rows.Clear()

        Select Case cmbDepartment.Text
            Case "Baguettes"
                If chkRepair.Checked = False Then
                    Dep_Grading_Trf_Selection("SELECT parno,pktno, sum(RetPcsT+RetPcsB),rtrim(sum(RetCts)) from dbo.tblBAGReturns WHERE sec = 10 AND ParNo='" & selected_parno & "'and Gra_Trf=0 and RetPcsT+RetPcsB > 0 group by parno,pktno", 10, selected_parno, "select pktPcs,rtrim(pktCts) from dbo.tblBAGPacket", flxAvailable, "Baguettes")
                Else
                    Load_Packets(selected_parno, "")
                End If
            Case "Princess"
                If chkRepair.Checked = False Then
                    Dep_Grading_Trf_Selection("SELECT parno,pktno,sum(RetPcsC+ RetPcsP)as 'Pcs',rtrim(sum(RetCtsC+RetCtsP))as 'Cts' from dbo.tblPRReturns WHERE sec = 9 AND ParNo='" & selected_parno & "'and Gra_Trf=0 and RetPcsC+ RetPcsP > 0 group by parno,pktno", 9, selected_parno, "select pktPcs,rtrim(pktCts) from dbo.tblPRPacket", flxAvailable, "Princess")
                Else
                    Load_Packets(selected_parno, "")
                End If
            Case "Rounds"
                If chkRepair.Checked = False Then
                    Dep_Grading_Trf_Selection("SELECT parno,pktno,sum(RetPcsT+RetPcsB),rtrim(sum(RetCts)) from dbo.tblRNDReturns WHERE sec = 25 AND ParNo='" & selected_parno & "'and Gra_Trf=0 and RetPcsT+RetPcsB > 0 group by parno,pktno", 25, selected_parno, "select pktPcs,rtrim(pktCts) from dbo.tblRNDPacket", flxAvailable, "Rounds")
                Else
                    Load_Packets(selected_parno, selected_Grp)
                End If
            Case "Niru"
                Dep_Grading_Trf_Selection("SELECT parno,pktno, sum(RetPcsT+RetPcsB),rtrim(sum(RetCts)) from dbo.tblNiruReturns WHERE sec = 25 AND ParNo='" & selected_parno & "'and Gra_Trf=0 and RetPcsT+RetPcsB > 0 group by parno,pktno", 25, selected_parno, "select pktPcs,rtrim(pktCts) from dbo.tblNiruPacket", flxAvailable, "Niru")
            Case "Emerald", "Davinci", "Lamour", "Opening", "Princess2", "Rounds3", "Rounds4", "Colombo Niru", "Baguettes2", "Baguettes3", "Emerald2", "Emerald3", "Carrer", "RoundsNLE", "Asscher", "Radiant"
                If chkRepair.Checked = False Then
                    Dep_Grading_Trf_Selection("select parno,pktno, sum(RetPcsT+RetPcsB),rtrim(sum(RetCts)) from dbo.tblExtReturns where department = '" & cmbDepartment.Text & "' and sec = 25 AND ParNo='" & selected_parno & "'and Gra_Trf=0 and RetPcsT+RetPcsB > 0 group by parno,pktno", 25, selected_parno, "select pktPcs,rtrim(pktCts) from dbo.tblExtPacket where department = '" & cmbDepartment.Text & "'", flxAvailable, cmbDepartment.Text)
                Else
                    Load_Packets(selected_parno, "")
                End If
            Case "Mix"
                Dep_Grading_Trf_Selection("SELECT ParNo, PktNo, SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts FROM tblExpGrading WHERE (OK = 0) AND (ParNo = '" & selected_parno & "') GROUP BY ParNo, PktNo", 25, selected_parno, "SELECT SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts FROM tblExpGrading WHERE (OK = 0)", flxAvailable, "Mix")
            Case "GradingPCU_N"
                If chkRepair.Checked = True Then
                    Load_Packets(selected_parno, "")
                End If
        End Select


    End Sub

    Private Sub flxAvailable_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxAvailable.CellClick
        Dim intRow As Integer

        If cmbDepartment.Text <> "" Then
            For intRow = 0 To flxSelected.Rows.Count - 1
                If flxAvailable.Item(0, flxAvailable.CurrentRow.Index).Value = flxSelected.Item(0, intRow).Value And flxAvailable.Item(1, flxAvailable.CurrentRow.Index).Value = flxSelected.Item(1, intRow).Value Then
                    MsgBox("Already Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Next

            flxSelected.Rows.Add(flxAvailable.Item(0, flxAvailable.CurrentRow.Index).Value,
                                 flxAvailable.Item(1, flxAvailable.CurrentRow.Index).Value,
                                 flxAvailable.Item(2, flxAvailable.CurrentRow.Index).Value,
                                 flxAvailable.Item(3, flxAvailable.CurrentRow.Index).Value,
                                 flxAvailable.Item(4, flxAvailable.CurrentRow.Index).Value,
                                 flxAvailable.Item(5, flxAvailable.CurrentRow.Index).Value,
                                 flxAvailable.Item(6, flxAvailable.CurrentRow.Index).Value)

            flxAvailable.Rows.RemoveAt(flxAvailable.CurrentRow.Index)
            txtPcs.Text = CalTotalPcs(flxSelected)
            txtCts.Text = CalTotalCts(flxSelected)
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

    Private Sub Save()
        Dim trfPCS As Double
        Dim trfCts As Double
        Dim trfRghPcs As Double
        Dim trfRghCts As Double
        Dim trfGroup As String
        Dim BatchNo As Double
        Dim e As Integer

        BatchNo = max_BatchNo()

        For e = 0 To flxSelected.Rows.Count - 1
            trfPCS = flxSelected.Item(2, e).Value
            trfCts = flxSelected.Item(3, e).Value
            trfRghPcs = flxSelected.Item(4, e).Value
            trfRghCts = flxSelected.Item(5, e).Value
            trfGroup = flxSelected.Item(6, e).Value

            If chkRepair.Checked = False Then
                Dep_Grading_Trf(cmbDepartment.Text, BatchNo, flxSelected.Item(0, e).Value, flxSelected.Item(1, e).Value, trfPCS, trfCts, trfRghPcs, trfRghCts, trfGroup)
                ReturnTablesUpdation(cmbDepartment.Text, flxSelected.Item(0, e).Value, flxSelected.Item(1, e).Value)
                If chkFinish.Checked = True Then
                    AdoCN.Execute("UPDATE tblParcel SET ProdFinish = 1 WHERE Depart = '" & cmbDepartment.Text & "' AND GrpParNo = '" & flxSelected.Item(0, e).Value & "'")
                End If

                If chkBoiling.Checked = True Then
                    GradingAcceptations(flxSelected.Item(0, e).Value, flxSelected.Item(1, e).Value, 0, 0)

                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblGrading_BoilingIssues WHERE Department = '" & cmbDepartment.Text & "' AND ParNo = '" & flxSelected.Item(0, e).Value & "' AND PktNo = '" & flxSelected.Item(1, e).Value & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        'Boiling Issues
                        AdoCN.Execute("INSERT INTO tblGrading_BoilingIssues(Department,ParNo,PktNo,EmpNo,IssPcs,IssCts,IssDate,IssTime,UserName,Remarks,Grp) " & _
                                      "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, e).Value & "','" & flxSelected.Item(1, e).Value & "','" & PBUser_EmpNo & "'," & CInt(flxSelected.Item(2, e).Value) & "," & _
                                             "" & CDbl(flxSelected.Item(3, e).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "','','" & flxSelected.Item(6, e).Value & "')")

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM tblGrading_Parcel WHERE ParNo = '" & Mid(flxSelected.Item(0, e).Value, 1, 6) & "' AND Dept = 'Grading'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            AdoCN.Execute("INSERT INTO tblGrading_Parcel(ParNo,Complete,Dept) VALUES('" & Mid(flxSelected.Item(0, e).Value, 1, 6) & "',0,'Grading')")
                        End If
                        rsComSql_1 = Nothing

                    End If
                    rsComSql = Nothing
                End If
            Else
                AdoCN.Execute("UPDATE tblFinalRepReturns SET Trf = 1, TrfDate = '" & Format(Date.Now, "MM/dd/yyyy") & "' WHERE (Department = '" & cmbDepartment.Text & "') AND (Trf = 0) AND (ParNo = '" & flxSelected.Item(0, e).Value & "') AND (PktNo = '" & flxSelected.Item(1, e).Value & "')")
            End If
        Next

        trfPCS = 0
        trfCts = 0
        trfRghPcs = 0
        trfRghCts = 0
        BatchNo = 0

        MsgBox("Records Successfully Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub txtSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSearch.KeyPress
        If Asc(e.KeyChar) = 13 Then
            flxParcel.Rows.Clear()
            flxAvailable.Rows.Clear()

            rsComSql = New ADODB.Recordset
            Select Case cmbDepartment.Text
                Case "Princess"
                    rsComSql.Open("SELECT ParNo,RTRIM(SUBSTRING(ParNo, 7, 1)) AS [Group] FROM dbo.tblPRReturns WHERE sec=9 and Gra_Trf=0 and ParNo = '" & Trim(txtSearch.Text) & "' group by parno,RTRIM(SUBSTRING(ParNo, 7, 1)) order by RTRIM(SUBSTRING(ParNo, 7, 1))", AdoCN, 1, 1)
                Case "Baguettes"
                    rsComSql.Open("SELECT ParNo AS [Parcel No],RTRIM(SUBSTRING(ParNo, 7, 1))'Group' FROM dbo.tblBAGReturns WHERE sec=10 and Gra_Trf=0 and ParNo = '" & Trim(txtSearch.Text) & "' group by parno,RTRIM(SUBSTRING(ParNo, 7, 1)) order by RTRIM(SUBSTRING(ParNo, 7, 1))", AdoCN, 1, 1)
                Case "Rounds"
                    rsComSql.Open("SELECT ParNo,RTRIM(SUBSTRING(ParNo, 8, 1))'Group' FROM dbo.tblRNDReturns WHERE sec=25 and Gra_Trf=0 and ParNo = '" & Trim(txtSearch.Text) & "' group by parno,RTRIM(SUBSTRING(ParNo, 7, 1)) order by RTRIM(SUBSTRING(ParNo, 7, 1))", AdoCN, 1, 1)
                Case "Niru"
                    rsComSql.Open("SELECT ParNo,RTRIM(SUBSTRING(ParNo, 8, 1))'Group' FROM dbo.tblNiruReturns WHERE sec=25 and Gra_Trf=0 and ParNo = '" & Trim(txtSearch.Text) & "' group by parno,RTRIM(SUBSTRING(ParNo, 7, 1)) order by RTRIM(SUBSTRING(ParNo, 7, 1))", AdoCN, 1, 1)
                Case "Rounds3"
                    rsComSql.Open("SELECT ParNo,RTRIM(SUBSTRING(ParNo, 8, 1))'Group' FROM dbo.tblExtReturns WHERE sec=25 and Gra_Trf=0 and ParNo = '" & Trim(txtSearch.Text) & "' and Department = 'Rounds3' group by parno,RTRIM(SUBSTRING(ParNo, 7, 1)) order by RTRIM(SUBSTRING(ParNo, 7, 1))", AdoCN, 1, 1)
                Case "Rounds4"
                    rsComSql.Open("SELECT ParNo,RTRIM(SUBSTRING(ParNo, 8, 1))'Group' FROM dbo.tblExtReturns WHERE sec=25 and Gra_Trf=0 and ParNo = '" & Trim(txtSearch.Text) & "' and Department = 'Rounds4' group by parno,RTRIM(SUBSTRING(ParNo, 7, 1)) order by RTRIM(SUBSTRING(ParNo, 7, 1))", AdoCN, 1, 1)
                Case Else
                    MsgBox("Grading transfer is not entitle for this department")
            End Select
            If rsComSql.RecordCount > 0 Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    flxParcel.Rows.Add(rsComSql.Fields("ParNo").Value,
                                       rsComSql.Fields("Group").Value)

                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub flxSelected_DoubleClick(sender As Object, e As EventArgs) Handles flxSelected.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxSelected.Rows.RemoveAt(flxSelected.CurrentRow.Index)
            txtPcs.Text = CalTotalPcs(flxSelected)
            txtCts.Text = CalTotalCts(flxSelected)
        End If
    End Sub
End Class