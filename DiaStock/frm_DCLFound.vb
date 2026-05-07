
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLFound

    Private Sub frm_DCLFound_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Department(cmbDept)
        cmbDept.Items.Add("Import Excess")
        cmbDept.Items.Add("Manager Found")
        cmbDept.Items.Add("Manager Found Bro/Rej")
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmbDept_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbDept.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtParNo.Focus()
        End If
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtParNo.Text = UCase(txtParNo.Text)
            txtPktNo.Focus()
        End If
    End Sub

    Private Sub txtPktNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtPktNo.Text = UCase(txtPktNo.Text)
            txtSec.Focus()
        End If
    End Sub

    Private Sub txtSec_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSec.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtEmpNo.Focus()
        End If
    End Sub

    Private Sub Load_All()
        Dim dblSize As Double
        Dim dblAmount As Double
        Dim strParcelNo As String

        flxAll.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Depart, OrigParcelNo, Assortment, GrpParNo, PktNo, PktPcs, PktCts, EmpNo, RetDate, " & _
                        "LostPcs, LostCts, RejPcs, RejCts, BroPcs, CompanyName, SecName, ParcelType, Sec, FdDate, FoundPCs, FoundCts " & _
                      "FROM VW_AZAllLostFound " & _
                      "WHERE (FdDate IS NULL) AND (EmpNo = '" & txtEmpNo.Text & "') " & _
                      "ORDER BY Depart, RetDate", dbConn, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                If rsComSql.Fields("PktPcs").Value <> 0 Then
                    dblSize = Math.Round(rsComSql.Fields("PktCts").Value / rsComSql.Fields("PktPcs").Value, 3)
                Else
                    dblSize = 0
                End If

                If rsComSql.Fields("ParcelType").Value = "Rough" Then
                    If (dblSize >= 0.001) And (dblSize < 0.1) Then
                        dblAmount = (dblSize * 525) / 0.01
                    ElseIf (dblSize >= 0.1) And (dblSize < 0.2) Then
                        dblAmount = (dblSize * 630) / 0.01
                    ElseIf (dblSize >= 0.2) And (dblSize < 0.3) Then
                        dblAmount = (dblSize * 735) / 0.01
                    ElseIf (dblSize >= 0.3) And (dblSize < 0.4) Then
                        dblAmount = (dblSize * 840) / 0.01
                    ElseIf (dblSize >= 0.4) And (dblSize < 0.5) Then
                        dblAmount = (dblSize * 945) / 0.01
                    Else
                        dblAmount = (dblSize * 1050) / 0.01
                    End If
                Else
                    If (dblSize >= 0.001) And (dblSize < 0.1) Then
                        dblAmount = (dblSize * 1365) / 0.01
                    ElseIf (dblSize >= 0.1) And (dblSize < 0.2) Then
                        dblAmount = (dblSize * 1575) / 0.01
                    ElseIf (dblSize >= 0.2) And (dblSize < 0.3) Then
                        dblAmount = (dblSize * 1785) / 0.01
                    ElseIf (dblSize >= 0.3) And (dblSize < 0.4) Then
                        dblAmount = (dblSize * 1995) / 0.01
                    ElseIf (dblSize >= 0.4) And (dblSize < 0.5) Then
                        dblAmount = (dblSize * 2205) / 0.01
                    Else
                        dblAmount = (dblSize * 2415) / 0.01
                    End If
                End If

                dblAmount = dblAmount * rsComSql.Fields("LostPcs").Value

                If rsComSql.Fields("Depart").Value = "Lab" Then
                    strParcelNo = rsComSql.Fields("OrigParcelNo").Value
                Else
                    strParcelNo = rsComSql.Fields("GrpParNo").Value
                End If


                flxAll.Rows.Add(strParcelNo,
                                rsComSql.Fields("PktNo").Value,
                                rsComSql.Fields("PktPcs").Value,
                                Math.Round(rsComSql.Fields("PktCts").Value, 3),
                                Format(rsComSql.Fields("RetDate").Value, "yyyy/MM/dd"),
                                rsComSql.Fields("LostPCs").Value,
                                rsComSql.Fields("RejPCs").Value,
                                rsComSql.Fields("BroPCs").Value,
                                rsComSql.Fields("SecName").Value,
                                rsComSql.Fields("CompanyName").Value,
                                rsComSql.Fields("ParcelType").Value,
                                rsComSql.Fields("Assortment").Value,
                                dblAmount,
                                rsComSql.Fields("LostCts").Value,
                                rsComSql.Fields("RejCts").Value,
                                dblAmount,
                                rsComSql.Fields("Depart").Value,
                                rsComSql.Fields("Sec").Value)


                rsComSql.MoveNext()
            End While
        Else
            MsgBox("No New Records Found", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Found()
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT ParNo, PktNo, PktPcs, PktCts, SSec, SecName, EmpNo, LostPCs, LostCts, RejPCs, RejCts, BroPCs, RetDate, " & _
                        "Depart, Supplier, AssortNo, AssortType, LostVal, FdDate, FoundPCs, FoundCts, AddtionalEmp, SpecialPCs, ActVal, HrNotGive, Issued " & _
                      "FROM tblFound " & _
                      "WHERE (ID = '" & CDbl(txtID.Text) & "')", dbConn, 1, 1)
        If rsComSql.RecordCount Then
            chkAccept.Checked = True

            flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                rsComSql.Fields("PktNo").Value,
                                rsComSql.Fields("PktPcs").Value,
                                Math.Round(rsComSql.Fields("PktCts").Value, 3),
                                Format(rsComSql.Fields("RetDate").Value, "yyyy/MM/dd"),
                                rsComSql.Fields("LostPCs").Value,
                                rsComSql.Fields("RejPCs").Value,
                                rsComSql.Fields("BroPCs").Value,
                                rsComSql.Fields("SecName").Value,
                                rsComSql.Fields("Supplier").Value,
                                rsComSql.Fields("AssortType").Value,
                                rsComSql.Fields("AssortNo").Value,
                                rsComSql.Fields("LostVal").Value,
                                rsComSql.Fields("LostCts").Value,
                                rsComSql.Fields("RejCts").Value,
                                rsComSql.Fields("ActVal").Value)

            txtFPcs.Text = rsComSql.Fields("FoundPCs").Value
            txtFCts.Text = rsComSql.Fields("FoundCts").Value
            txtAddEmp.Text = rsComSql.Fields("AddtionalEmp").Value
            txtSPcs.Text = rsComSql.Fields("SpecialPCs").Value

            chkHr.Checked = IIf(rsComSql.Fields("HrNotGive").Value = 1, True, False)
            chkIssued.Checked = IIf(rsComSql.Fields("Issued").Value = 1, True, False)


            MsgBox("Already Accepted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Details()
        Dim blnFound As Boolean
        Dim dblSize As Double
        Dim dblAmount As Double
        Dim strParcelNo As String

        txtParNo.Text = UCase(txtParNo.Text)
        txtPktNo.Text = UCase(txtPktNo.Text)
        txtEmpNo.Text = UCase(txtEmpNo.Text)

        blnFound = True
        flxDetails.Rows.Clear()

        If cmbDept.Text = "Import Excess" Or cmbDept.Text = "Manager Found" Or cmbDept.Text = "Manager Found Bro/Rej" Then
            flxDetails.Rows.Add(txtParNo.Text,
                                txtPktNo.Text,
                                "0",
                                "0",
                                Format(Date.Now, "yyyy/MM/dd"),
                                "0",
                                "0",
                                "0",
                                "0",
                                "DCL",
                                "0",
                                "0",
                                "0",
                                "0",
                                "0",
                                "0",
                                "0")

            blnFound = True
        Else
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT ID, ParNo, PktNo, PktPcs, PktCts, SSec, SecName, EmpNo, LostPCs, LostCts, RejPCs, RejCts, BroPCs, RetDate, " & _
                            "Depart, Supplier, AssortNo, AssortType, LostVal, FdDate, FoundPCs, FoundCts, AddtionalEmp, SpecialPCs, ActVal, Category " & _
                          "FROM tblFound " & _
                          "WHERE (ParNo = '" & txtParNo.Text & "') AND (PktNo = '" & txtPktNo.Text & "') AND (SSec = '" & txtSec.Text & "') AND " & _
                            "(Depart = '" & cmbDept.Text & "') AND (EmpNo = '" & txtEmpNo.Text & "')", dbConn, 1, 1)
            If rsComSql.RecordCount Then
                blnFound = True
                chkAccept.Checked = True

                flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("PktPcs").Value,
                                    rsComSql.Fields("PktCts").Value,
                                    Format(rsComSql.Fields("RetDate").Value, "yyyy/MM/dd"),
                                    rsComSql.Fields("LostPCs").Value,
                                    rsComSql.Fields("RejPCs").Value,
                                    rsComSql.Fields("BroPCs").Value,
                                    rsComSql.Fields("SecName").Value,
                                    rsComSql.Fields("Supplier").Value,
                                    rsComSql.Fields("AssortType").Value,
                                    rsComSql.Fields("AssortNo").Value,
                                    rsComSql.Fields("LostVal").Value,
                                    rsComSql.Fields("LostCts").Value,
                                    rsComSql.Fields("RejCts").Value,
                                    rsComSql.Fields("ActVal").Value,
                                    rsComSql.Fields("Category").Value)

                txtID.Text = rsComSql.Fields("ID").Value
                txtFPcs.Text = rsComSql.Fields("FoundPCs").Value
                txtFCts.Text = rsComSql.Fields("FoundCts").Value
                txtAddEmp.Text = rsComSql.Fields("AddtionalEmp").Value
                txtSPcs.Text = rsComSql.Fields("SpecialPCs").Value

                MsgBox("Already Accepted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Else
                blnFound = False
            End If
            rsComSql = Nothing

            If blnFound = False Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT Depart, OrigParcelNo, Assortment, GrpParNo, PktNo, PktPcs, PktCts, EmpNo, RetDate, LostPcs, LostCts, " & _
                                "RejPcs, RejCts, BroPcs, CompanyName, SecName, ParcelType, Sec, Category " & _
                              "FROM dbo.VW_AZAllLost " & _
                              "WHERE (GrpParNo = '" & txtParNo.Text & "') AND (PktNo = '" & txtPktNo.Text & "') AND (Depart = '" & cmbDept.Text & "') AND " & _
                                "(Sec = '" & txtSec.Text & "') AND (EmpNo = '" & txtEmpNo.Text & "')", dbConn, 1, 1)
                If rsComSql.RecordCount Then
                    blnFound = True

                    txtFPcs.Text = "0"
                    txtFCts.Text = "0"
                    txtAddEmp.Text = ""
                    txtSPcs.Text = "0"

                    If rsComSql.Fields("PktPcs").Value <> 0 Then
                        dblSize = Math.Round(rsComSql.Fields("PktCts").Value / rsComSql.Fields("PktPcs").Value, 3)
                    Else
                        dblSize = 0
                    End If

                    If rsComSql.Fields("ParcelType").Value = "Rough" Then
                        If (dblSize >= 0.001) And (dblSize < 0.1) Then
                            dblAmount = (dblSize * 525) / 0.01
                        ElseIf (dblSize >= 0.1) And (dblSize < 0.2) Then
                            dblAmount = (dblSize * 630) / 0.01
                        ElseIf (dblSize >= 0.2) And (dblSize < 0.3) Then
                            dblAmount = (dblSize * 735) / 0.01
                        ElseIf (dblSize >= 0.3) And (dblSize < 0.4) Then
                            dblAmount = (dblSize * 840) / 0.01
                        ElseIf (dblSize >= 0.4) And (dblSize < 0.5) Then
                            dblAmount = (dblSize * 945) / 0.01
                        Else
                            dblAmount = (dblSize * 1050) / 0.01
                        End If
                    Else
                        If (dblSize >= 0.001) And (dblSize < 0.1) Then
                            dblAmount = (dblSize * 1365) / 0.01
                        ElseIf (dblSize >= 0.1) And (dblSize < 0.2) Then
                            dblAmount = (dblSize * 1575) / 0.01
                        ElseIf (dblSize >= 0.2) And (dblSize < 0.3) Then
                            dblAmount = (dblSize * 1785) / 0.01
                        ElseIf (dblSize >= 0.3) And (dblSize < 0.4) Then
                            dblAmount = (dblSize * 1995) / 0.01
                        ElseIf (dblSize >= 0.4) And (dblSize < 0.5) Then
                            dblAmount = (dblSize * 2205) / 0.01
                        Else
                            dblAmount = (dblSize * 2415) / 0.01
                        End If
                    End If

                    dblAmount = dblAmount * rsComSql.Fields("LostPCs").Value

                    If rsComSql.Fields("Depart").Value = "Lab" Then
                        strParcelNo = rsComSql.Fields("OrigParcelNo").Value
                    Else
                        strParcelNo = rsComSql.Fields("GrpParNo").Value
                    End If

                    flxDetails.Rows.Add(strParcelNo,
                                        rsComSql.Fields("PktNo").Value,
                                        rsComSql.Fields("PktPcs").Value,
                                        Math.Round(rsComSql.Fields("PktCts").Value, 3),
                                        Format(rsComSql.Fields("RetDate").Value, "yyyy/MM/dd"),
                                        rsComSql.Fields("LostPCs").Value,
                                        rsComSql.Fields("RejPCs").Value,
                                        rsComSql.Fields("BroPCs").Value,
                                        rsComSql.Fields("SecName").Value,
                                        rsComSql.Fields("CompanyName").Value,
                                        rsComSql.Fields("ParcelType").Value,
                                        rsComSql.Fields("Assortment").Value,
                                        dblAmount,
                                        rsComSql.Fields("LostCts").Value,
                                        rsComSql.Fields("RejCts").Value,
                                        dblAmount,
                                        rsComSql.Fields("Category").Value)

                    chkAccept.Checked = False
                Else
                    blnFound = False
                End If
                rsComSql = Nothing
            End If
        End If

        If blnFound = False Then
            MsgBox("No Records Found", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Load_Details()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub ClearFields()
        cmbDept.Text = ""
        txtParNo.Text = ""
        txtPktNo.Text = ""
        txtSec.Text = ""
        txtEmpNo.Text = ""
        flxDetails.Rows.Clear()
        txtFPcs.Text = "0"
        txtFCts.Text = "0"
        chkAccept.Checked = False
        txtAddEmp.Text = ""
        txtSPcs.Text = "0"
        flxAll.Rows.Clear()
        txtID.Text = ""
        chkHr.Checked = False
        chkIssued.Checked = False
    End Sub

    Private Sub txtFPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFPcs.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtFPcs.Text)
        If Asc(e.KeyChar) = 13 Then
            txtFCts.Focus()
        End If
    End Sub

    Private Sub Save()
        If chkAccept.Checked = False Then
            If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If txtParNo.Text = "" Then MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If txtPktNo.Text = "" Then MsgBox("Invalid Packet No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If txtSec.Text = "" Then MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If txtEmpNo.Text = "" Then MsgBox("Invalid Emp No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

            dbConn.Execute("INSERT INTO tblFound(ParNo, PktNo, PktPcs, PktCts, SSec, SecName, EmpNo, LostPCs, LostCts, RejPCs, RejCts, BroPCs, RetDate, " & _
                            "Depart, Supplier, AssortNo, AssortType, LostVal, FdDate, FoundPCs, FoundCts, AddtionalEmp, SpecialPCs, ActVal, Category) " & _
                           "VALUES('" & txtParNo.Text & "','" & txtPktNo.Text & "','" & flxDetails.Item(2, 0).Value & "','" & flxDetails.Item(3, 0).Value & "'," & _
                            "'" & txtSec.Text & "','" & flxDetails.Item(8, 0).Value & "','" & txtEmpNo.Text & "','" & flxDetails.Item(5, 0).Value & "'," & _
                            "'" & flxDetails.Item(13, 0).Value & "','" & flxDetails.Item(6, 0).Value & "','" & flxDetails.Item(14, 0).Value & "','" & flxDetails.Item(7, 0).Value & "'," & _
                            "'" & Format(CDate(flxDetails.Item(4, 0).Value), "MM/dd/yyyy") & "','" & cmbDept.Text & "','" & flxDetails.Item(9, 0).Value & "','" & flxDetails.Item(11, 0).Value & "'," & _
                            "'" & flxDetails.Item(10, 0).Value & "','" & flxDetails.Item(12, 0).Value & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & CDbl(txtFPcs.Text) & "','" & CDbl(txtFCts.Text) & "'," & _
                            "'" & txtAddEmp.Text & "','" & CDbl(txtSPcs.Text) & "','" & flxDetails.Item(15, 0).Value & "','" & flxDetails.Item(16, 0).Value & "')")

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT MAX(ID) AS MaxID FROM tblFound", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                MsgBox("Saved Successfully - " & rsComSql.Fields("MaxID").Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            rsComSql = Nothing
        Else
            If txtID.Text = "" Then MsgBox("Invalid ID", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If txtFPcs.Text = "" Then MsgBox("Invalid Found Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If txtFCts.Text = "" Then MsgBox("Invalid Found Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

            dbConn.Execute("UPDATE tblFound SET FdDate = '" & Format(Date.Now, "MM/dd/yyyy") & "', FoundPCs = '" & CDbl(txtFPcs.Text) & "', FoundCts = '" & CDbl(txtFCts.Text) & "',HrNotGive = " & IIf(chkHr.Checked = True, 1, 0) & ",Issued = " & IIf(chkIssued.Checked = True, 1, 0) & " " & _
                           "WHERE (ID = '" & CDbl(txtID.Text) & "')")

            MsgBox("Updated Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub txtEmpNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEmpNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtEmpNo.Text = UCase(txtEmpNo.Text)
            Load_All()
            cmdLoad.Focus()
        End If
    End Sub

    Private Sub txtFCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtFCts.Text)
    End Sub

    Private Sub txtSPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSPcs.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtSPcs.Text)
        If Asc(e.KeyChar) = 13 Then
            If CDbl(txtSPcs.Text) > 0 Then
                If flxDetails.Rows.Count > 0 Then
                    flxDetails.Item(12, 0).Value = CDbl(flxDetails.Item(15, 0).Value) * CDbl(txtSPcs.Text)
                End If
            End If
        End If
    End Sub

    Private Sub flxAll_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxAll.CellClick
        cmbDept.Text = flxAll.Item(16, flxAll.CurrentRow.Index).Value
        txtParNo.Text = flxAll.Item(0, flxAll.CurrentRow.Index).Value
        txtPktNo.Text = flxAll.Item(1, flxAll.CurrentRow.Index).Value
        txtSec.Text = flxAll.Item(17, flxAll.CurrentRow.Index).Value
    End Sub

    Private Sub txtID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtID.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            Load_Found()
        End If
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLLostDetails_AllPar_OrdLostRejBro_All.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptLabSynthetic.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub flxAll_DoubleClick(sender As Object, e As EventArgs) Handles flxAll.DoubleClick
        flxDetails.Rows.Clear()

        flxDetails.Rows.Add(flxAll.Item(0, flxAll.CurrentRow.Index).Value,
                            flxAll.Item(1, flxAll.CurrentRow.Index).Value,
                            flxAll.Item(2, flxAll.CurrentRow.Index).Value,
                            flxAll.Item(3, flxAll.CurrentRow.Index).Value,
                            flxAll.Item(4, flxAll.CurrentRow.Index).Value,
                            flxAll.Item(5, flxAll.CurrentRow.Index).Value,
                            flxAll.Item(6, flxAll.CurrentRow.Index).Value,
                            flxAll.Item(7, flxAll.CurrentRow.Index).Value,
                            flxAll.Item(8, flxAll.CurrentRow.Index).Value,
                            flxAll.Item(9, flxAll.CurrentRow.Index).Value,
                            flxAll.Item(10, flxAll.CurrentRow.Index).Value,
                            flxAll.Item(11, flxAll.CurrentRow.Index).Value,
                            flxAll.Item(12, flxAll.CurrentRow.Index).Value,
                            flxAll.Item(13, flxAll.CurrentRow.Index).Value,
                            flxAll.Item(14, flxAll.CurrentRow.Index).Value,
                            flxAll.Item(15, flxAll.CurrentRow.Index).Value,
                            flxAll.Item(16, flxAll.CurrentRow.Index).Value)
    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptLabSynthetic2.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button12_Click(sender As Object, e As EventArgs) Handles HazelDev_Button12.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptLabRefer3_Date.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button4_Click(sender As Object, e As EventArgs) Handles HazelDev_Button4.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLLostDetails_AllPar_OrdRejBro_All.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub
End Class