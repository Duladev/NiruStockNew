
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLExportPlan
    Private Sub Load_LotNo()

        cmbLotNo.Items.Clear()
        cmbLotNo.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblImport.LotNo " & _
                      "FROM dbo.tblParcel INNER JOIN dbo.tblImport ON dbo.tblParcel.OrigParcelNo = dbo.tblImport.SupParcelNo " & _
                      "WHERE (dbo.tblParcel.Complete = 0) AND (dbo.tblParcel.Hide = 0) " & _
                      "GROUP BY dbo.tblImport.LotNo " & _
                      "ORDER BY dbo.tblImport.LotNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbLotNo.Items.Add(rsComSql.Fields("LotNo").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        Load_Data()

    End Sub

    Private Sub Load_Data()

        If cmbLotNo.Text = "" Then Exit Sub

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblDCLExportPlan WHERE ExportDate = '" & Format(dtpExportDate.Value, "MM/dd/yyyy") & "' AND LotNo = '" & cmbLotNo.Text & "' AND OK = 0 ORDER BY Department, DCLParNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("DCLParNo").Value,
                                    rsComSql.Fields("Department").Value,
                                    rsComSql.Fields("SupParNo").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    rsComSql.Fields("LotNo").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        'If Weekday(dtpExportDate.Value) <> 6 Then MsgBox("Invalid Date", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If cmbLotNo.Text = "" Then MsgBox("Invalid Lot No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If flxDetails.Rows.Count < 1 Then MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        AdoCN.Execute("DELETE FROM tblDCLExportPlan WHERE ExportDate = '" & Format(dtpExportDate.Value, "MM/dd/yyyy") & "' AND LotNo = '" & CDbl(cmbLotNo.Text) & "' AND OK = 0")
        For intRow = 0 To flxDetails.Rows.Count - 1
            AdoCN.Execute("INSERT INTO tblDCLExportPlan(ExportDate,SupParNo,DCLParNo,Department,Pcs,Cts,RejPcs,RejCts,LotNo,TargetDate) " & _
                          "VALUES('" & Format(dtpExportDate.Value, "MM/dd/yyyy") & "','" & flxDetails.Item(2, intRow).Value & "','" & flxDetails.Item(0, intRow).Value & "'," & _
                            "'" & flxDetails.Item(1, intRow).Value & "'," & CDbl(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ", 0, 0," & _
                            "" & CDbl(flxDetails.Item(5, intRow).Value) & ",'" & Format(dtpTargetDate.Value, "MM/dd/yyyy") & "')")
        Next

        MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        flxParcels.Rows.Clear()
        flxDetails.Rows.Clear()
        cmbLotNo.Text = ""
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub frm_DCLExportPlan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        dtpExportDate.Value = Date.Now
        dtpVerifyDate.Value = Date.Now

        Load_LotNo()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_SavedData()
        flxVerify.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblDCLExportPlan WHERE ExportDate = '" & Format(dtpVerifyDate.Value, "MM/dd/yyyy") & "' ORDER BY Department, DCLParNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxVerify.Rows.Add(rsComSql.Fields("DCLParNo").Value,
                                   rsComSql.Fields("Department").Value,
                                   rsComSql.Fields("SupParNo").Value,
                                   rsComSql.Fields("Pcs").Value,
                                   rsComSql.Fields("Cts").Value,
                                   rsComSql.Fields("RejPcs").Value,
                                   rsComSql.Fields("RejCts").Value,
                                   IIf(rsComSql.Fields("OK").Value = 1, True, False),
                                   rsComSql.Fields("LotNo").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub dtpVerifyDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpVerifyDate.ValueChanged
        Load_SavedData()
    End Sub

    Private Sub Verify()
        'If Weekday(dtpVerifyDate.Value) <> 6 Then MsgBox("Invalid Date", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If flxVerify.Rows.Count < 1 Then MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        For intRow = 0 To flxVerify.Rows.Count - 1
            AdoCN.Execute("UPDATE tblDCLExportPlan SET OK = " & IIf(flxVerify.Item(7, intRow).Value = True, 1, 0) & " " & _
                          "WHERE ExportDate = '" & Format(dtpVerifyDate.Value, "MM/dd/yyyy") & "' AND SupParNo = '" & flxVerify.Item(2, intRow).Value & "' AND " & _
                            "DCLParNo = '" & flxVerify.Item(0, intRow).Value & "' AND Department = '" & flxVerify.Item(1, intRow).Value & "'")
        Next

        MsgBox("Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        flxVerify.Rows.Clear()
    End Sub

    Private Sub cmdVerify_Click(sender As Object, e As EventArgs) Handles cmdVerify.Click
        Instring = UCase(InputBox("Enter Access Code", "Authorized Password"))
        If Instring = "MORATUWA" Then
            Verify()
        End If
    End Sub

    Private Sub cmbLotNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbLotNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Load_ParcelDetails()
        End If
    End Sub

    Private Sub cmbLotNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLotNo.SelectedIndexChanged
        Load_ParcelDetails()
    End Sub

    Private Sub Load_ParcelDetails()
        txtPcs.Text = ""
        txtCts.Text = ""
        flxParcels.Rows.Clear()
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblParcel.Depart, dbo.tblParcel.OrigParcelNo, dbo.tblParcel.GrpParNo, dbo.tblParcel.IssuedPcs, dbo.tblParcel.IssuedCts, dbo.tblParcel.RejectPcs, dbo.tblParcel.RejectCts " & _
                      "FROM dbo.tblParcel INNER JOIN dbo.tblImport ON dbo.tblParcel.OrigParcelNo = dbo.tblImport.SupParcelNo " & _
                      "WHERE (dbo.tblParcel.Complete = 0) AND (dbo.tblParcel.Hide = 0) AND (dbo.tblImport.LotNo = '" & cmbLotNo.Text & "') AND (dbo.tblParcel.Depart <> 'Rough Dept') AND (dbo.tblParcel.Depart <> 'Sawing') AND " & _
                            "(dbo.tblParcel.Depart NOT LIKE 'Rough%') " & _
                      "ORDER BY dbo.tblParcel.Depart, dbo.tblParcel.GrpParNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxParcels.Rows.Add(rsComSql.Fields("GrpParNo").Value,
                                    rsComSql.Fields("Depart").Value,
                                    rsComSql.Fields("IssuedPcs").Value,
                                    rsComSql.Fields("IssuedCts").Value,
                                    rsComSql.Fields("RejectPcs").Value,
                                    rsComSql.Fields("RejectCts").Value,
                                    rsComSql.Fields("OrigParcelNo").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        Load_Data()
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim intRow As Integer

        'If Weekday(dtpExportDate.Value) <> 6 Then MsgBox("Invalid Date", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbLotNo.Text = "" Then MsgBox("Invalid Lot No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtParcel.Text = "" Then MsgBox("Invalid Parcel", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtPcs.Text = "" Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtCts.Text = "" Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblDCLExportPlan WHERE OK = 1 AND DCLParNo = '" & txtParcel.Text & "' AND Department = '" & txtDept.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            MsgBox("Already Verified", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblDCLExportPlan WHERE ExportDate = '" & Format(dtpExportDate.Value, "MM/dd/yyyy") & "' AND OK = 1 AND DCLParNo = '" & txtParcel.Text & "' AND Department = '" & txtDept.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            MsgBox("Already Verified", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        For intRow = 0 To flxDetails.Rows.Count - 1
            If cmbLotNo.Text <> flxDetails.Item(5, intRow).Value Then
                MsgBox("Invalid Lot No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        For intRow = 0 To flxDetails.Rows.Count - 1
            If txtParcel.Text = flxDetails.Item(0, intRow).Value And txtDept.Text = flxDetails.Item(1, intRow).Value Then
                MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        flxDetails.Rows.Add(txtParcel.Text,
                            txtDept.Text,
                            txtSupParNo.Text,
                            txtPcs.Text,
                            txtCts.Text,
                            cmbLotNo.Text)

        txtSupParNo.Text = ""
        txtParcel.Text = ""
        txtDept.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
    End Sub

    Private Sub Select_All()
        Dim intRow As Integer
        Dim intRow2 As Integer

        If cmbLotNo.Text = "" Then MsgBox("Invalid Lot No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        For intRow = 0 To flxParcels.Rows.Count - 1
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblDCLExportPlan WHERE OK = 1 AND DCLParNo = '" & flxParcels.Item(0, intRow).Value & "' AND Department = '" & flxParcels.Item(1, intRow).Value & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                MsgBox("Already Verified - " & flxParcels.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblDCLExportPlan WHERE ExportDate = '" & Format(dtpExportDate.Value, "MM/dd/yyyy") & "' AND OK = 1 AND DCLParNo = '" & flxParcels.Item(0, intRow).Value & "' AND Department = '" & flxParcels.Item(1, intRow).Value & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                MsgBox("Already Verified - " & flxParcels.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            For intRow2 = 0 To flxDetails.Rows.Count - 1
                If cmbLotNo.Text <> flxDetails.Item(5, intRow2).Value Then
                    MsgBox("Invalid Lot No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Next

            For intRow2 = 0 To flxDetails.Rows.Count - 1
                If flxParcels.Item(0, intRow).Value = flxDetails.Item(0, intRow2).Value And flxParcels.Item(1, intRow).Value = flxDetails.Item(1, intRow2).Value Then
                    MsgBox("Already Entered - " & flxParcels.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Next

            flxDetails.Rows.Add(flxParcels.Item(0, intRow).Value,
                                flxParcels.Item(1, intRow).Value,
                                flxParcels.Item(6, intRow).Value,
                                flxParcels.Item(2, intRow).Value,
                                flxParcels.Item(3, intRow).Value,
                                cmbLotNo.Text)
        Next

        txtSupParNo.Text = ""
        txtParcel.Text = ""
        txtDept.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
    End Sub

    Private Sub cmdLoadData_Click(sender As Object, e As EventArgs) Handles cmdLoadData.Click
        Load_Data()
    End Sub

    Private Sub flxParcels_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxParcels.CellClick
        txtParcel.Text = flxParcels.Item(0, flxParcels.CurrentRow.Index).Value
        txtDept.Text = flxParcels.Item(1, flxParcels.CurrentRow.Index).Value
        txtPcs.Text = flxParcels.Item(2, flxParcels.CurrentRow.Index).Value
        txtCts.Text = flxParcels.Item(3, flxParcels.CurrentRow.Index).Value
        txtSupParNo.Text = flxParcels.Item(6, flxParcels.CurrentRow.Index).Value
    End Sub

    Private Sub flxDetails_DoubleClick(sender As Object, e As EventArgs) Handles flxDetails.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)
        End If
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLExportPlan.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLExportPlanDept.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Instring = UCase(InputBox("Enter Access Code", "Authorized Password"))
        If Instring = "MORATUWA" Then
            Delete()
        End If
    End Sub

    Private Sub Delete()
        If cmbLotNo.Text = "" Then MsgBox("Invalid Lot No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        PBResponse = MsgBox("Are you sure to Delete?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            AdoCN.Execute("DELETE FROM tblDCLExportPlan WHERE ExportDate = '" & Format(dtpExportDate.Value, "MM/dd/yyyy") & "' AND LotNo = '" & CDbl(cmbLotNo.Text) & "'")

            MsgBox("Export Plan Deleted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            flxVerify.Rows.Clear()
            Load_ParcelDetails()
        End If
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxVerify.RowCount - 1
                flxVerify.Item(7, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxVerify.RowCount - 1
                flxVerify.Item(7, intRow).Value = False
            Next
        End If
    End Sub

    Private Sub cmdAddAll_Click(sender As Object, e As EventArgs) Handles cmdAddAll.Click
        Select_All()
    End Sub

    Private Sub txtParNo2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Load_Parcel()
        End If
    End Sub

    Private Sub Load_Parcel()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblDCLExportPlan WHERE DCLParNo = '" & txtParNo2.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            txtLotNo2.Text = rsComSql.Fields("LotNo").Value
            txtDept2.Text = rsComSql.Fields("Department").Value
            txtSupPar2.Text = rsComSql.Fields("SupParNo").Value
            dtpExpDate2.Value = rsComSql.Fields("ExportDate").Value
            dtpTargetDate2.Value = rsComSql.Fields("TargetDate").Value
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdUpdate_Click(sender As Object, e As EventArgs) Handles cmdUpdate.Click
        If txtParNo2.Text <> "" And txtSupPar2.Text <> "" Then
            AdoCN.Execute("UPDATE tblDCLExportPlan SET TargetDate = '" & Format(dtpTargetDate2.Value, "MM/dd/yyyy") & "' WHERE SupParNo = '" & txtSupPar2.Text & "' AND Department = '" & txtDept2.Text & "' AND ExportDate = '" & Format(dtpExpDate2.Value, "MM/dd/yyyy") & "'")

            MsgBox("Target Date Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

            txtParNo2.Text = ""
            txtDept2.Text = ""
            txtSupPar2.Text = ""
            txtLotNo2.Text = ""
        End If
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        txtParNo2.Text = ""
        txtDept2.Text = ""
        txtSupPar2.Text = ""
        txtLotNo2.Text = ""
    End Sub
End Class