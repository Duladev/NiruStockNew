
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLImportEdit

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_Company()
        cmbCompany.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblCompany ORDER BY CompCode", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbCompany.Items.Add(rsComSql.Fields("CompCode").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Supplier()
        cmbSupplier.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblSuppliers ORDER BY CompanyName", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbSupplier.Items.Add(rsComSql.Fields("CompanyName").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Origin()
        cmbOrigin.Items.Clear()
        rsComSql = New ADODB.Recordset
        mStrSQL = "SELECT Origin FROM tblDCLOrigin ORDER BY Origin"
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        Do While Not rsComSql.EOF
            cmbOrigin.Items.Add(rsComSql.Fields("Origin").Value)
            rsComSql.MoveNext()
        Loop
        rsComSql = Nothing
    End Sub

    Private Sub ClearFields()
        txtSupParNo.Text = ""
        txtAssortment.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtItemName.Text = ""
        txtInst.Text = ""
        txtSelectCost.Text = ""

        txtNewAssort.Text = ""
        txtImpPrice.Text = ""
        txtActImpPrice.Text = ""
        txtLotNo.Text = ""
        txtMasterLot.Text = ""
        txtHardCost.Text = ""
        txtInvPrice.Text = ""
        txtSupRefNo.Text = ""
        txtConRefNo.Text = ""
        txtBoi.Text = ""

        cmbCategory.Text = ""
        cmbSawn.Text = ""
        cmbOrigin.Text = ""
        cmbPriceType.Text = ""
        cmbSupplier.Text = ""
        txtSupplier.Text = ""
        dtpInvDate.Value = Date.Now
        dtpRecDate.Value = Date.Now
        txtPointer.Text = ""
        txtImportNo.Text = ""
        chkOriginal.Checked = True
        chkInternal.Checked = False
        cmbCompany.Text = ""
        txtSysDate.Text = ""
        txtRemarks.Text = ""

        txtColor.Text = ""
        txtClarity.Text = ""
        txtLength.Text = ""
        txtWidth.Text = ""
        txtHeight.Text = ""
        txtBoxNo.Text = ""
        txtLabour.Text = ""

        txtBoxName.Text = ""
        cmbLocation.Text = ""
        cmbArticle.Text = ""
        txtSightNo.Text = ""

        picBox.Image = Nothing
    End Sub

    Private Sub txtSupParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSupParNo.KeyPress
        If Asc(e.KeyChar) = 13 And Len(txtSupParNo.Text) > 0 Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblImport WHERE SupParcelNo = '" & txtSupParNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 1 Then
                txtAssortment.Text = rsComSql.Fields("AssortmentNo").Value
                txtItemName.Text = rsComSql.Fields("ItemName").Value
                txtPcs.Text = rsComSql.Fields("INVPcs").Value
                txtCts.Text = rsComSql.Fields("INVCts").Value
                txtInst.Text = rsComSql.Fields("LocalInst").Value
                txtSelectCost.Text = rsComSql.Fields("SelectCost").Value & ""

                txtNewAssort.Text = rsComSql.Fields("NewAssort").Value
                txtImpPrice.Text = rsComSql.Fields("ItemCost").Value
                txtActImpPrice.Text = rsComSql.Fields("ActItemCost").Value
                txtLotNo.Text = rsComSql.Fields("LotNo").Value
                txtMasterLot.Text = rsComSql.Fields("NewLotNo").Value
                txtHardCost.Text = rsComSql.Fields("HardCost").Value
                txtInvPrice.Text = rsComSql.Fields("ImpPrice").Value
                txtSupRefNo.Text = rsComSql.Fields("SupplierRefNo").Value
                txtConRefNo.Text = rsComSql.Fields("ConRefNo").Value
                txtBoi.Text = rsComSql.Fields("BOINo").Value
                cmbCategory.Text = rsComSql.Fields("Category").Value
                cmbSawn.Text = rsComSql.Fields("Sawn").Value

                cmbOrigin.Text = rsComSql.Fields("Origin").Value
                cmbPriceType.Text = rsComSql.Fields("PriceType").Value
                dtpInvDate.Value = rsComSql.Fields("InvoiceDate").Value
                dtpRecDate.Value = rsComSql.Fields("RecievedDate").Value

                txtPointer.Text = rsComSql.Fields("Pointer").Value & ""
                txtImportNo.Text = rsComSql.Fields("ImportNo").Value

                chkOriginal.Checked = IIf(rsComSql.Fields("Original").Value = 1, True, False)
                chkInternal.Checked = IIf(rsComSql.Fields("Internal").Value = 1, True, False)

                txtSupplier.Text = rsComSql.Fields("SupplierCode").Value
                cmbCompany.Text = rsComSql.Fields("CompCode").Value
                txtSysDate.Text = Format(rsComSql.Fields("SysDateTime").Value, "yyyy/MM/dd")
                txtRemarks.Text = rsComSql.Fields("Remarks").Value

                txtColor.Text = rsComSql.Fields("Color").Value
                txtClarity.Text = rsComSql.Fields("Clarity").Value
                txtLength.Text = rsComSql.Fields("Length").Value
                txtWidth.Text = rsComSql.Fields("Width").Value
                txtHeight.Text = rsComSql.Fields("Height").Value

                txtTraceID.Text = rsComSql.Fields("TraceID").Value
                txtBoxNo.Text = rsComSql.Fields("BoxNo").Value
                txtLabour.Text = rsComSql.Fields("Labour").Value

                txtBoxName.Text = rsComSql.Fields("BoxName").Value
                cmbLocation.Text = rsComSql.Fields("Location").Value
                cmbArticle.Text = rsComSql.Fields("Article").Value
                txtSightNo.Text = rsComSql.Fields("SightNo").Value

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblSuppliers WHERE SupplierCode = " & rsComSql.Fields("SupplierCode").Value & "", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    cmbSupplier.Text = rsComSql_1.Fields("CompanyName").Value
                End If
                rsComSql_1 = Nothing

                Show_Photo(txtLotNo.Text)

                txtPcs.Focus()
            Else
                MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
                txtSupParNo.Focus()
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub frm_DCLImportEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        If PBUser_Level = 1 Then
            txtInvPrice.Visible = True
        Else
            txtInvPrice.Visible = False
        End If
        Load_Origin()
        Load_Supplier()
        Load_Company()
    End Sub

    Private Sub txtCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtCts.Text)
    End Sub

    Private Sub txtImpPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtImpPrice.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtImpPrice.Text)
    End Sub

    Private Sub txtHardCost_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHardCost.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtHardCost.Text)
    End Sub

    Private Sub txtSelectCost_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSelectCost.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtSelectCost.Text)
    End Sub

    Private Sub txtActImpPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtActImpPrice.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtActImpPrice.Text)
    End Sub

    Private Sub txtInvPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtInvPrice.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtInvPrice.Text)
    End Sub

    Private Sub txtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub Save()
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then

            If txtSupParNo.Text <> "" Then
                If txtAssortment.Text = "" Then
                    MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If txtPcs.Text = "" Then
                    MsgBox("Invalid Import Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If txtCts.Text = "" Then
                    MsgBox("Invalid Import Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If txtImpPrice.Text = "" Then
                    MsgBox("Invalid Import Price", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If txtActImpPrice.Text = "" Then
                    MsgBox("Invalid Diamond Cost", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If txtHardCost.Text = "" Then
                    MsgBox("Invalid Hard Cost", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If txtInvPrice.Text = "" Then
                    MsgBox("Invalid Invoice Price", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If txtLotNo.Text = "" Then
                    MsgBox("Invalid Lot No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If txtMasterLot.Text = "" Then
                    MsgBox("Invalid Master Lot No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If txtSupRefNo.Text = "" Then
                    MsgBox("Invalid Supplier Ref No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If txtBoi.Text = "" Then
                    MsgBox("Invalid BOI No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If cmbCategory.Text = "" Then
                    MsgBox("Invalid Category", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If cmbOrigin.Text = "" Then
                    MsgBox("Invalid Origin", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblDCLOrigin WHERE Origin = '" & Trim(cmbOrigin.Text) & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    MsgBox("Invalid Origin", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing

                If txtNewAssort.Text = "" Then
                    MsgBox("Invalid New Assort", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If txtSelectCost.Text = "" Then
                    MsgBox("Invalid Select Cost", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If cmbPriceType.Text = "" Then
                    MsgBox("Invalid Price Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If cmbSupplier.Text = "" Then
                    MsgBox("Invalid Supplier", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If txtSupplier.Text = "" Then
                    MsgBox("Invalid Supplier", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If cmbCompany.Text = "" Then
                    MsgBox("Invalid Company", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If txtTraceID.Text = "" Then
                    MsgBox("Invalid Trace ID", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If txtLabour.Text = "" Then
                    MsgBox("Invalid Labour", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblImport WHERE SupParcelNo = '" & txtSupParNo.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount = 1 Then
                    If rsComSql.Fields("INVPcs").Value <> CDbl(txtPcs.Text) Then
                        Insert_Log("IMPORT PCS CHANGED " & rsComSql.Fields("INVPcs").Value & " TO " & CDbl(txtPcs.Text), "IMPORT", txtSupParNo.Text, "", 0)
                    End If
                    If Math.Round(rsComSql.Fields("INVCts").Value, 3) <> Math.Round(CDbl(txtCts.Text), 3) Then
                        Insert_Log("IMPORT CTS CHANGED " & rsComSql.Fields("INVCts").Value & " TO " & CDbl(txtCts.Text), "IMPORT", txtSupParNo.Text, "", 0)
                    End If

                    AdoCN.Execute("UPDATE tblImport SET AssortmentNo = '" & Trim(txtAssortment.Text) & "',INVPcs = " & CDbl(txtPcs.Text) & ",ACTPcs = " & CDbl(txtPcs.Text) & "," & _
                                    "INVCts = " & CDbl(txtCts.Text) & ",ACTCts = " & CDbl(txtCts.Text) & ",RemPcs = " & CDbl(txtPcs.Text) & ",LocalInst = '" & txtInst.Text & "'," & _
                                    "SelectCost = " & CDbl(txtSelectCost.Text) & ", ItemName = '" & txtItemName.Text & "',NewAssort = '" & txtNewAssort.Text & "',ItemCost = " & CDbl(txtImpPrice.Text) & "," & _
                                    "ActItemCost = " & CDbl(txtActImpPrice.Text) & ",LotNo = " & CDbl(txtLotNo.Text) & ",NewLotNo = " & CDbl(txtMasterLot.Text) & ", " & _
                                    "HardCost = " & CDbl(txtHardCost.Text) & ",ImpPrice = " & CDbl(txtInvPrice.Text) & ",SupplierRefNo = '" & txtSupRefNo.Text & "', " & _
                                    "ConRefNo = '" & txtConRefNo.Text & "',BOINo = '" & Trim(txtBoi.Text) & "',Category = '" & cmbCategory.Text & "',Sawn = '" & cmbSawn.Text & "', " & _
                                    "Origin = '" & cmbOrigin.Text & "',PriceType = '" & cmbPriceType.Text & "',SupplierCode = '" & CInt(txtSupplier.Text) & "',InvoiceDate = '" & Format(dtpInvDate.Value, "MM/dd/yyyy") & "'," & _
                                    "Pointer = '" & txtPointer.Text & "',Original = " & IIf(chkOriginal.Checked = True, 1, 0) & ",Internal = " & IIf(chkInternal.Checked = True, 1, 0) & ",CompCode = '" & cmbCompany.Text & "'," & _
                                    "ModifyBy = '" & PBUser_ID & "',Remarks = '" & txtRemarks.Text & "',Color = '" & txtColor.Text & "',Clarity = '" & txtClarity.Text & "',Length = '" & txtLength.Text & "'," & _
                                    "Width = '" & txtWidth.Text & "',Height = '" & txtHeight.Text & "',TraceID = " & CDbl(txtTraceID.Text) & ",RecievedDate = '" & Format(dtpRecDate.Value, "MM/dd/yyyy") & "',BoxNo = '" & txtBoxNo.Text & "'," & _
                                    "Labour = " & CDbl(txtLabour.Text) & ",BoxName = '" & txtBoxName.Text & "',Location = '" & cmbLocation.Text & "',Article = '" & cmbArticle.Text & "',SightNo = '" & txtSightNo.Text & "' " & _
                                  "WHERE SupParcelNo = '" & txtSupParNo.Text & "'")

                    MsgBox("Import Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    txtSupParNo.Focus()
                Else
                    MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    ClearFields()
                    txtSupParNo.Focus()
                End If
                rsComSql = Nothing
            Else
                MsgBox("Invalid Sup. Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub txtLotNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLotNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub txtMasterLot_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMasterLot.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub Show_Photo(ByVal strLotNo As String)
        Dim filename As String = "\\" & strServerName & "\Import Lot Images\" & strLotNo & ".JPG"
        PBResponse = Dir(filename)
        If Len(PBResponse) > 0 Then
            picBox.Image = Image.FromFile(filename)
        Else
            picBox.Image = Nothing
        End If
    End Sub

    Private Sub cmbSupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSupplier.SelectedIndexChanged
        rsComSql_2 = New ADODB.Recordset
        rsComSql_2.Open("SELECT * FROM tblSuppliers WHERE CompanyName = '" & cmbSupplier.Text & "'", AdoCN, 1, 1)
        If rsComSql_2.RecordCount Then
            txtSupplier.Text = rsComSql_2.Fields("SupplierCode").Value
        Else
            txtSupplier.Text = ""
        End If
        rsComSql_2 = Nothing
    End Sub

    Private Sub txtTraceID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTraceID.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub txtLabour_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLabour.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtLabour.Text)
    End Sub
End Class