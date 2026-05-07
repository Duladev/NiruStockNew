
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_MixOrder

    Private Sub frm_MixOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        If PBUser_Level = 1 Then
            cmdUpdate.Visible = True
            chkApprove.Visible = True
        Else
            cmdUpdate.Visible = False
            chkApprove.Visible = False
        End If

        Load_Supplier()
        Load_Client()
        Load_Color()
        Load_Clarity()
        Load_Toler()
        Load_Descrip()
        Load_Flow()
        Load_Culet()
        Load_InCat()
        Load_RateCode()

        ClearText()
        GetNewOrderNo()
    End Sub

    Private Sub ClearText()
        'txtOrdNo.Text = ""
        txtNiruOrdNo.Text = ""
        cmbSupp.Text = "Niru Diamonds Israel (1987) Ltd"
        cmbClient.Text = ""
        txtOrderItem.Text = ""
        txtCommande.Text = ""
        cmbTol.Text = ""
        txtDesc.Text = ""
        cmbColor.Text = ""
        cmbClarity.Text = ""
        txtRemarks.Text = ""
        txtKitNo.Text = ""
        dtpDueDate.Value = Date.Now
        dtpOrdDate.Value = Date.Now

        txtRef.Text = ""
        txtSide.Text = ""
        txtLen.Text = ""
        txtWid.Text = ""
        txtBot.Text = ""
        txtRon.Text = ""
        txtAngles.Text = ""
        txtMaxAngle.Text = ""
        txtLineNo.Text = ""
        txtTLen.Text = ""
        txtTWit.Text = ""
        txtTHgt.Text = ""
        txtRangeID.Text = ""
        txtRangeID1.Text = ""
        cmbDec.Text = ""
        cmbCul.Text = ""
        cmbIntCat.Text = ""
        cmbFlow.Text = ""
        cmbChg.Text = ""
        txtSets.Text = ""
        txtPcs.Text = ""
        txtTotPcs.Text = ""
        txtMaxCost.Text = ""
        txtAskPrice.Text = ""
        cmbType.Text = ""
        chkGroove.Checked = False
        cmbGroove.Text = ""
        chkLaser.Checked = False

        txtLmHt1.Text = "0"
        txtLmHt2.Text = "0"
        txtGdHt1.Text = "0"
        txtGdHt2.Text = "0"

        chkApprove.Checked = False
        chkInternal.Checked = False

        flxDetails.Rows.Clear()
    End Sub

    Private Sub GetNewOrderNo()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(OrderNo) AS OrdNo FROM tblOrders", AdoCN, 1, 1)
        If IsDBNull(rsComSql.Fields("OrdNo").Value) Then
            txtOrdNo.Text = strRight(Format(Date.Now, "yyyy"), 2) & "0001"
        Else
            'If Mid(rsComSql.Fields("OrdNo").Value, 1, 2) <> strRight(Format(Date.Now, "yyyy"), 2) Then
            '    txtOrdNo.Text = strRight(Format(Date.Now, "yyyy"), 2) & "0001"
            'Else
            '    txtOrdNo.Text = CDbl(rsComSql.Fields("OrdNo").Value) + 1
            'End If
            txtOrdNo.Text = CDbl(rsComSql.Fields("OrdNo").Value) + 1
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearText()
        GetNewOrderNo()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_Supplier()
        cmbSupp.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT CompanyName FROM tblSuppliers ORDER BY CompanyName", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            Do While Not rsComSql.EOF
                cmbSupp.Items.Add(rsComSql.Fields("CompanyName").Value)
                rsComSql.MoveNext()
            Loop
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Client()
        cmbClient.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT NiruCust FROM tblNiruRef ORDER BY NiruCust", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            Do While Not rsComSql.EOF
                cmbClient.Items.Add(rsComSql.Fields("NiruCust").Value)
                rsComSql.MoveNext()
            Loop
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Color()
        cmbColor.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Description FROM tblOrderTypes WHERE Type = 'Color' ORDER BY Description", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            Do While Not rsComSql.EOF
                cmbColor.Items.Add(rsComSql.Fields("Description").Value)
                rsComSql.MoveNext()
            Loop
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Clarity()
        cmbClarity.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Description FROM tblOrderTypes WHERE Type = 'Clarity' ORDER BY Description", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            Do While Not rsComSql.EOF
                cmbClarity.Items.Add(rsComSql.Fields("Description").Value)
                rsComSql.MoveNext()
            Loop
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmbClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbClient.SelectedIndexChanged
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT Color, Clarity FROM tblNiruRef WHERE NiruCust = '" & cmbClient.Text & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            cmbColor.Text = rsComSql_1.Fields("Color").Value
            cmbClarity.Text = rsComSql_1.Fields("Clarity").Value

            txtOrderItem.Focus()
        End If
        rsComSql_1 = Nothing
    End Sub

    Private Sub Load_Toler()
        cmbTol.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TolerSize FROM tblToleran ORDER BY TolerSize", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            Do While Not rsComSql.EOF
                cmbTol.Items.Add(rsComSql.Fields("TolerSize").Value)
                rsComSql.MoveNext()
            Loop
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Descrip()
        cmbDec.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblDecription ORDER BY DDescript", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            Do While Not rsComSql.EOF
                cmbDec.Items.Add(rsComSql.Fields("DDescript").Value)
                rsComSql.MoveNext()
            Loop
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Flow()
        cmbFlow.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblMixFlow ORDER BY Flow", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            Do While Not rsComSql.EOF
                cmbFlow.Items.Add(rsComSql.Fields("Flow").Value)
                rsComSql.MoveNext()
            Loop
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Culet()
        cmbCul.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Culdir FROM tblCuletDirec ORDER BY Culdir", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            Do While Not rsComSql.EOF
                cmbCul.Items.Add(rsComSql.Fields("Culdir").Value)
                rsComSql.MoveNext()
            Loop
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_InCat()
        cmbIntCat.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblUnits ORDER BY Unit", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            Do While Not rsComSql.EOF
                cmbIntCat.Items.Add(rsComSql.Fields("Unit").Value)
                rsComSql.MoveNext()
            Loop
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_RateCode()
        cmbChg.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT RateCode FROM tblCuttingCharges WHERE Department = 'PRECISION' ORDER BY RateCode", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            Do While Not rsComSql.EOF
                cmbChg.Items.Add(rsComSql.Fields("RateCode").Value)
                rsComSql.MoveNext()
            Loop
        End If
        rsComSql = Nothing
    End Sub

    Private Sub txtOrdNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrdNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            Load_OrderDetails()
        End If
    End Sub

    Private Sub txtNiruOrdNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNiruOrdNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbSupp.Focus()
        End If
    End Sub

    Private Sub cmbSupp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSupp.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbClient.Focus()
        End If
    End Sub

    Private Sub txtOrderItem_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrderItem.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtCommande.Focus()
        End If
    End Sub

    Private Sub txtCommande_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCommande.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbTol.Focus()
        End If
    End Sub

    Private Sub cmbTol_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbTol.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtDesc.Focus()
        End If
    End Sub

    Private Sub txtDesc_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDesc.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtRef.Focus()
        End If
    End Sub

    Private Sub txtRef_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRef.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtSide.Focus()
        End If
    End Sub

    Private Sub txtSide_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSide.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtSide.Text = UCase(txtSide.Text)
            txtLen.Focus()
        End If
    End Sub

    Private Sub txtLen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLen.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtWid.Focus()
        End If
    End Sub

    Private Sub txtWid_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWid.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If IsNumeric(txtWid.Text) Then
                If CDbl(txtWid.Text) < 3 Then
                    txtLmHt1.Text = Math.Round(CDbl(txtWid.Text) * 0.09, 2)
                    txtLmHt2.Text = Math.Round(CDbl(txtWid.Text) * 0.1, 2)
                Else
                    txtLmHt1.Text = Math.Round(CDbl(txtWid.Text) * 0.085, 2)
                    txtLmHt2.Text = Math.Round(CDbl(txtWid.Text) * 0.095, 2)
                End If
                txtGdHt1.Text = Math.Round(CDbl(txtWid.Text) * 0.04, 2)
                txtGdHt2.Text = Math.Round(CDbl(txtWid.Text) * 0.05, 2)
            End If
            txtBot.Focus()
        End If
    End Sub

    Private Sub txtBot_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBot.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtRon.Focus()
        End If
    End Sub

    Private Sub txtRon_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRon.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbDec.Focus()
        End If
    End Sub

    Private Sub cmbDec_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbDec.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtAngles.Focus()
        End If
    End Sub

    Private Sub txtAngles_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAngles.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtMaxAngle.Focus()
        End If
    End Sub

    Private Sub txtMaxAngle_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMaxAngle.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtMaxAngle.Text)
        If Asc(e.KeyChar) = 13 Then
            txtLineNo.Focus()
        End If
    End Sub

    Private Sub txtLineNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLineNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbCul.Focus()
        End If
    End Sub

    Private Sub cmbCul_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbCul.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbIntCat.Focus()
        End If
    End Sub

    Private Sub cmbIntCat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbIntCat.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbFlow.Focus()
        End If
    End Sub

    Private Sub cmbFlow_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbFlow.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbChg.Focus()
        End If
    End Sub

    Private Sub cmbChg_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbChg.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtSets.Focus()
        End If
    End Sub

    Private Sub txtSets_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSets.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtSets.Text <> "" And txtPcs.Text <> "" Then
                txtTotPcs.Text = CDbl(txtSets.Text) * CDbl(txtPcs.Text)
            End If
            txtPcs.Focus()
        End If
    End Sub

    Private Sub txtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtSets.Text <> "" And txtPcs.Text <> "" Then
                txtTotPcs.Text = CDbl(txtSets.Text) * CDbl(txtPcs.Text)
            End If
            txtMaxCost.Focus()
        End If
    End Sub

    Private Sub txtMaxCost_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMaxCost.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtMaxCost.Text)
        If Asc(e.KeyChar) = 13 Then
            cmbType.Focus()
        End If
    End Sub

    Private Sub cmbGroove_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbGroove.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtAskPrice.Focus()
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        AddReference()
    End Sub

    Private Sub AddReference()
        Dim dblLen As Double
        Dim dblWid As Double
        Dim intRow As Integer

        'Dim dblMaxCost As Double

        PBResponse = MsgBox("Are you sure you want to update this Transaction?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If txtRef.Text = "" Then
                MsgBox("Invalid Reference", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtSide.Text = "" Then
                MsgBox("Invalid Side", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtLen.Text = "" Then
                MsgBox("Invalid Length", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If Not IsNumeric(txtLen.Text) Then
                MsgBox("Invalid Length", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtWid.Text = "" Then
                MsgBox("Invalid Width", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If Not IsNumeric(txtWid.Text) Then
                MsgBox("Invalid Width", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If Len(cmbIntCat.Text) <> 2 Then
                MsgBox("Invalid Incentive Category", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtLineNo.Text = "" Then
                MsgBox("Invalid Line No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtAngles.Text = "" Then
                MsgBox("Invalid Angles", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtMaxAngle.Text = "" Then
                MsgBox("Invalid Maximum Angle", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtMaxCost.Text = "" Then
                MsgBox("Invalid Maximum Cost", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If cmbType.Text = "" Then
                MsgBox("Invalid Maximum Cost Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If chkGroove.Checked = True Then
                If CInt(cmbGroove.Text) <= 0 Then
                    MsgBox("Groove Count", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Else
                cmbGroove.Text = "0"
            End If
            If txtLmHt1.Text = "" Then
                MsgBox("Invalid Lemala Height", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtLmHt2.Text = "" Then
                MsgBox("Invalid Lemala Height", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtGdHt1.Text = "" Then
                MsgBox("Invalid Girdle Height", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtGdHt2.Text = "" Then
                MsgBox("Invalid Girdle Height", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If txtAskPrice.Text = "" Then
                MsgBox("Invalid Asking Price", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            txtRangeID.Text = "0"
            txtRangeID1.Text = "0"
            If txtLen.Text <> "" And txtWid.Text <> "" Then
                If IsNumeric(txtLen.Text) = True And IsNumeric(txtWid.Text) = True Then
                    If CDbl(txtLen.Text) < CDbl(txtWid.Text) Then
                        dblLen = CDbl(txtWid.Text) + 0.2
                        dblWid = CDbl(txtLen.Text) + 0.2
                    Else
                        dblLen = CDbl(txtLen.Text) + 0.2
                        dblWid = CDbl(txtWid.Text) + 0.2
                    End If
                    If dblLen < 1 Then dblLen = 1
                    If dblWid < 1 Then dblWid = 1

                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblAssortRange WHERE (LengthFrom <= " & dblLen & ") AND (LengthTo >= " & dblLen & ") AND (WidthFrom <= " & dblWid & ") AND (WidthTo >= " & dblWid & ")", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        txtRangeID.Text = rsComSql.Fields("ID").Value
                    End If
                    rsComSql = Nothing
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblAssortRange1 WHERE (LengthFrom <= " & dblLen & ") AND (LengthTo >= " & dblLen & ") AND (WidthFrom <= " & dblWid & ") AND (WidthTo >= " & dblWid & ")", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        txtRangeID1.Text = rsComSql.Fields("ID").Value
                    End If
                    rsComSql = Nothing
                End If
            End If

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblOrdersDtls WHERE NLineNo = '" & Trim(txtLineNo.Text) & "' AND OrderNo <> " & CInt(txtOrdNo.Text) & "", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                MsgBox("Line No. already exists " & rsComSql.Fields("OrderNo").Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            For intRow = 0 To flxDetails.Rows.Count - 1
                If Trim(txtLineNo.Text) = Trim(flxDetails.Item(0, intRow).Value) Then
                    MsgBox("Line No. duplicated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If Trim(txtRef.Text) = Trim(flxDetails.Item(1, intRow).Value) And Trim(txtSide.Text) = Trim(flxDetails.Item(2, intRow).Value) Then
                    MsgBox("Ref/Side duplicated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Next

            'dblMaxCost = Math.Round(CDbl(txtAskPrice.Text) * 0.96, 0)

            flxDetails.Rows.Add(txtLineNo.Text, txtRef.Text, UCase(txtSide.Text), Format(CSng(txtLen.Text), "#0.00"),
                                Format(CSng(txtWid.Text), "#0.00"), txtBot.Text, txtRon.Text, cmbDec.Text, cmbCul.Text,
                                cmbIntCat.Text, cmbFlow.Text, cmbChg.Text, txtSets.Text, txtPcs.Text, "0", txtTLen.Text,
                                txtTWit.Text, txtTHgt.Text, txtAngles.Text, txtMaxAngle.Text, IIf(chkGroove.Checked = True, 1, 0),
                                txtRangeID.Text, txtRangeID1.Text, cmbGroove.Text, txtMaxCost.Text, cmbType.Text,
                                txtLmHt1.Text, txtLmHt2.Text, txtGdHt1.Text, txtGdHt2.Text, IIf(chkLaser.Checked = True, 1, 0),
                                txtMaxCost.Text, txtAskPrice.Text)

            txtRef.Focus()
        End If
    End Sub

    Private Sub SaveOrder()
        Dim intRow As Integer
        Dim intGrid As Integer
        Dim dblCutRate As Double
        Dim blnAccess As Boolean

        dblCutRate = 0
        blnAccess = False

        If Len(txtOrdNo.Text) <> 6 Then
            MsgBox("Invalid Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtNiruOrdNo.Text = "" Then
            MsgBox("Please enter the Niru Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtOrderItem.Text = "" Then
            MsgBox("Please enter the Order Item", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtCommande.Text = "" Then
            MsgBox("Please enter the Commande", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblNiruRef WHERE NiruCust = '" & cmbClient.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Client", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        For intRow = 0 To flxDetails.Rows.Count - 1
            If Trim(flxDetails.Item(0, intRow).Value) = "" Then
                MsgBox("Line No. cannot be blank", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblOrdersDtls WHERE NLineNo = '" & Trim(flxDetails.Item(0, intRow).Value) & "' AND OrderNo <> " & CInt(txtOrdNo.Text) & "", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                MsgBox("Order No. " & rsComSql.Fields("OrderNo").Value & vbCrLf & "Line No. " & Trim(flxDetails.Item(0, intRow).Value) & " already exists", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblNoneOrdersDtls WHERE NLineNo = '" & Trim(flxDetails.Item(0, intRow).Value) & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                MsgBox("Order No. " & rsComSql.Fields("OrderNo").Value & vbCrLf & "Line No. " & Trim(flxDetails.Item(0, intRow).Value) & " already exists", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblCuttingCharges WHERE RateCode = '" & Trim(flxDetails.Item(11, intRow).Value) & "' AND Department = 'PRECISION'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Labour Code " & Trim(flxDetails.Item(11, intRow).Value), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            If Trim(flxDetails.Item(3, intRow).Value) = "" Then
                MsgBox("Length cannot be blank", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Trim(flxDetails.Item(4, intRow).Value) = "" Then
                MsgBox("Width cannot be blank", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Trim(flxDetails.Item(5, intRow).Value) = "" Then
                MsgBox("B. Height cannot be blank", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Trim(flxDetails.Item(6, intRow).Value) = "" Then
                MsgBox("R. Height cannot be blank", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Trim(flxDetails.Item(7, intRow).Value) = "" Then
                MsgBox("Description cannot be blank", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Trim(flxDetails.Item(10, intRow).Value) = "" Then
                MsgBox("Flow cannot be blank", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Trim(flxDetails.Item(24, intRow).Value) = "" Then
                MsgBox("Maximum Cost cannot be blank", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Trim(flxDetails.Item(25, intRow).Value) = "" Then
                MsgBox("Maximum Cost Type cannot be blank", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Trim(flxDetails.Item(40, intRow).Value) = "" Then
                MsgBox("Asking Price cannot be blank", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If UCase(Trim(flxDetails.Item(25, intRow).Value)) = "P" Or UCase(Trim(flxDetails.Item(25, intRow).Value)) = "C" Then
            Else
                MsgBox("Invalid Maximum Cost Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Trim(flxDetails.Item(26, intRow).Value) = "" Then
                MsgBox("Invalid Lemala Height", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Trim(flxDetails.Item(27, intRow).Value) = "" Then
                MsgBox("Invalid Lemala Height", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Trim(flxDetails.Item(28, intRow).Value) = "" Then
                MsgBox("Invalid Girdle Height", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Trim(flxDetails.Item(29, intRow).Value) = "" Then
                MsgBox("Invalid Girdle Height", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblMixFlow WHERE Flow = '" & Trim(flxDetails.Item(10, intRow).Value) & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Flow " & Trim(flxDetails.Item(10, intRow).Value), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblUnits WHERE Unit = '" & Trim(flxDetails.Item(9, intRow).Value) & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Incentive Unit " & Trim(flxDetails.Item(9, intRow).Value), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            Else
                If Trim(flxDetails.Item(10, intRow).Value) <> rsComSql.Fields("Flow").Value Then
                    MsgBox("Invalid Flow " & Trim(flxDetails.Item(9, intRow).Value & "/" & Trim(flxDetails.Item(10, intRow).Value)), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
            rsComSql = Nothing

            For intGrid = intRow + 1 To flxDetails.Rows.Count - 1
                If Trim(flxDetails.Item(0, intRow).Value) = Trim(flxDetails.Item(0, intGrid).Value) Then
                    MsgBox("Line No. duplicated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Next
        Next

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblOrders WHERE OrderNo = " & CInt(txtOrdNo.Text) & "", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                mStrSQL = "INSERT INTO tblOrders(OrderNo,Customer,DueDate,Subject,Niruref,Toler,OrdDate,Complete,Dept,NorderNo,OrderItem,COMMANDE,Color,Clarity,RevDueDate,Subject2,Internal,KITNo) " & _
                          "VALUES(" & CInt(txtOrdNo.Text) & ",'" & cmbSupp.Text & "','" & Format(dtpDueDate.Value, "MM/dd/yyyy") & "','" & Trim(txtDesc.Text) & "','" & cmbClient.Text & "'," & _
                            "'" & cmbTol.Text & "','" & Format(dtpOrdDate.Value, "MM/dd/yyyy") & "','N','','" & txtNiruOrdNo.Text & "'," & CDbl(txtOrderItem.Text) & "," & _
                            "'" & Replace(txtCommande.Text, "'", "''") & "','" & cmbColor.Text & "','" & cmbClarity.Text & "','" & Format(dtpDueDate.Value, "MM/dd/yyyy") & "','" & Trim(txtRemarks.Text) & "','" & IIf(chkInternal.Checked = True, 1, 0) & "','" & txtKitNo.Text & "')"

                AdoCN.Execute(mStrSQL)

                For intRow = 0 To flxDetails.Rows.Count - 1
                    dblCutRate = 0
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT InvRate FROM tblCuttingCharges WHERE RateCode = '" & Trim(flxDetails.Item(11, intRow).Value) & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        dblCutRate = rsComSql.Fields("InvRate").Value
                    End If
                    rsComSql = Nothing

                    mStrSQL = "INSERT INTO tblOrdersDtls(OrderNo,NLineNo,RefNo,Side,Length,Width,Bothigh,Ronhigh,CulDirect,IncenCat,Flow,Descrip,Sets,PCs,Cts,RefComp,CutChg,vCutChg,LenTol,WidTol,HiTol,Angles,MaxAngle,Groove,RangeID,RangeID1,GrCount,MaxCost,MaxType,LMHT1,LMHT2,GDHT1,GDHT2,Laser,OldMaxCost,AskingPrice) " & _
                              "VALUES(" & CInt(txtOrdNo.Text) & ",'" & Trim(flxDetails.Item(0, intRow).Value) & "','" & Replace(Trim(flxDetails.Item(1, intRow).Value), "'", "''") & "','" & Trim(flxDetails.Item(2, intRow).Value) & "'," & _
                                "'" & Trim(flxDetails.Item(3, intRow).Value) & "','" & Trim(flxDetails.Item(4, intRow).Value) & "','" & Trim(flxDetails.Item(5, intRow).Value) & "','" & Trim(flxDetails.Item(6, intRow).Value) & "'," & _
                                "'" & Trim(flxDetails.Item(8, intRow).Value) & "','" & Trim(flxDetails.Item(9, intRow).Value) & "','" & Trim(flxDetails.Item(10, intRow).Value) & "','" & Trim(flxDetails.Item(7, intRow).Value) & "'," & CInt(flxDetails.Item(12, intRow).Value) & "," & _
                                "" & CInt(flxDetails.Item(13, intRow).Value) & ",0,'N','" & Trim(flxDetails.Item(11, intRow).Value) & "'," & dblCutRate & ",'" & Trim(flxDetails.Item(15, intRow).Value) & "','" & Trim(flxDetails.Item(16, intRow).Value) & "','" & Trim(flxDetails.Item(17, intRow).Value) & "'," & _
                                "" & CInt(flxDetails.Item(18, intRow).Value) & "," & CDbl(flxDetails.Item(19, intRow).Value) & "," & CInt(flxDetails.Item(20, intRow).Value) & "," & CDbl(flxDetails.Item(21, intRow).Value) & "," & CDbl(flxDetails.Item(22, intRow).Value) & "," & CInt(flxDetails.Item(23, intRow).Value) & "," & _
                                "" & CDbl(flxDetails.Item(24, intRow).Value) & ",'" & Trim(flxDetails.Item(25, intRow).Value) & "'," & CDbl(flxDetails.Item(26, intRow).Value) & "," & CDbl(flxDetails.Item(27, intRow).Value) & "," & CDbl(flxDetails.Item(28, intRow).Value) & "," & CDbl(flxDetails.Item(29, intRow).Value) & "," & _
                                "" & CInt(flxDetails.Item(38, intRow).Value) & "," & CDbl(flxDetails.Item(39, intRow).Value) & "," & CDbl(flxDetails.Item(40, intRow).Value) & ")"

                    AdoCN.Execute(mStrSQL)
                Next

                MsgBox("Order Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            PBResponse = MsgBox("Already Exists. Are you sure to Update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                'If PBUser_EmpNo <> "D06975" Then
                '    Instring = UCase(InputBox("Enter Access Code", "Authorized Password"))
                '    If Instring = "KB123" Then
                '        blnAccess = True
                '    Else
                '        blnAccess = False
                '    End If
                'Else
                '    blnAccess = True
                'End If
                blnAccess = True
                If blnAccess = True Then
                    mStrSQL = "UPDATE tblOrders SET Customer = '" & cmbSupp.Text & "',DueDate = '" & Format(dtpDueDate.Value, "MM/dd/yyyy") & "',Subject = '" & Trim(txtDesc.Text) & "',Niruref = '" & cmbClient.Text & "',Toler = '" & cmbTol.Text & "',OrdDate = '" & Format(dtpOrdDate.Value, "MM/dd/yyyy") & "'," & _
                                "NorderNo = '" & txtNiruOrdNo.Text & "',OrderItem = " & CDbl(txtOrderItem.Text) & ",COMMANDE = '" & Replace(txtCommande.Text, "'", "''") & "',Color = '" & cmbColor.Text & "',Clarity = '" & cmbClarity.Text & "',Subject2 = '" & Trim(txtRemarks.Text) & "',Internal = '" & IIf(chkInternal.Checked = True, 1, 0) & "'," & _
                                "KITNo = '" & txtKitNo.Text & "' " & _
                              "WHERE OrderNo = " & CInt(txtOrdNo.Text) & ""

                    AdoCN.Execute(mStrSQL)

                    mStrSQL = "DELETE FROM tblOrdersDtls WHERE OrderNo = " & CInt(txtOrdNo.Text) & ""

                    AdoCN.Execute(mStrSQL)

                    For intRow = 0 To flxDetails.Rows.Count - 1
                        dblCutRate = 0
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT InvRate FROM tblCuttingCharges WHERE RateCode = '" & Trim(flxDetails.Item(11, intRow).Value) & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            dblCutRate = rsComSql.Fields("InvRate").Value
                        End If
                        rsComSql = Nothing

                        mStrSQL = "INSERT INTO tblOrdersDtls(OrderNo,NLineNo,RefNo,Side,Length,Width,Bothigh,Ronhigh,CulDirect,IncenCat,Flow,Descrip,Sets,PCs,Cts,RefComp,CutChg,vCutChg,LenTol,WidTol,HiTol,Angles,MaxAngle,Groove,RangeID,RangeID1,GrCount,MaxCost,MaxType,LMHT1,LMHT2,GDHT1,GDHT2,Laser,OldMaxCost,AskingPrice) " & _
                                  "VALUES(" & CInt(txtOrdNo.Text) & ",'" & Trim(flxDetails.Item(0, intRow).Value) & "','" & Replace(Trim(flxDetails.Item(1, intRow).Value), "'", "''") & "','" & Trim(flxDetails.Item(2, intRow).Value) & "'," & _
                                    "'" & Trim(flxDetails.Item(3, intRow).Value) & "','" & Trim(flxDetails.Item(4, intRow).Value) & "','" & Trim(flxDetails.Item(5, intRow).Value) & "','" & Trim(flxDetails.Item(6, intRow).Value) & "'," & _
                                    "'" & Trim(flxDetails.Item(8, intRow).Value) & "','" & Trim(flxDetails.Item(9, intRow).Value) & "','" & Trim(flxDetails.Item(10, intRow).Value) & "','" & Trim(flxDetails.Item(7, intRow).Value) & "'," & CInt(flxDetails.Item(12, intRow).Value) & "," & _
                                    "" & CInt(flxDetails.Item(13, intRow).Value) & ",0,'N','" & Trim(flxDetails.Item(11, intRow).Value) & "'," & dblCutRate & ",'" & Trim(flxDetails.Item(15, intRow).Value) & "','" & Trim(flxDetails.Item(16, intRow).Value) & "','" & Trim(flxDetails.Item(17, intRow).Value) & "'," & _
                                    "" & CInt(flxDetails.Item(18, intRow).Value) & "," & CDbl(flxDetails.Item(19, intRow).Value) & "," & CInt(flxDetails.Item(20, intRow).Value) & "," & CDbl(flxDetails.Item(21, intRow).Value) & "," & CDbl(flxDetails.Item(22, intRow).Value) & "," & CInt(flxDetails.Item(23, intRow).Value) & "," & _
                                    "" & CDbl(flxDetails.Item(24, intRow).Value) & ",'" & Trim(flxDetails.Item(25, intRow).Value) & "'," & CDbl(flxDetails.Item(26, intRow).Value) & "," & CDbl(flxDetails.Item(27, intRow).Value) & "," & CDbl(flxDetails.Item(28, intRow).Value) & "," & CDbl(flxDetails.Item(29, intRow).Value) & "," & _
                                    "" & CInt(flxDetails.Item(38, intRow).Value) & "," & CDbl(flxDetails.Item(39, intRow).Value) & "," & CDbl(flxDetails.Item(40, intRow).Value) & ")"

                        AdoCN.Execute(mStrSQL)
                    Next

                    MsgBox("Order Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                End If
                
            End If

        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_OrderDetails()
        Dim blnFound As Boolean

        ClearText()
        blnFound = False
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblOrders WHERE OrderNo = " & txtOrdNo.Text & "", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            blnFound = True
            cmbSupp.Text = rsComSql.Fields("Customer").Value
            txtDesc.Text = rsComSql.Fields("Subject").Value
            cmbClient.Text = rsComSql.Fields("Niruref").Value
            cmbTol.Text = rsComSql.Fields("Toler").Value
            txtNiruOrdNo.Text = rsComSql.Fields("NorderNo").Value
            txtOrderItem.Text = rsComSql.Fields("OrderItem").Value
            txtCommande.Text = rsComSql.Fields("COMMANDE").Value
            cmbColor.Text = rsComSql.Fields("Color").Value
            cmbClarity.Text = rsComSql.Fields("Clarity").Value
            dtpDueDate.Value = rsComSql.Fields("DueDate").Value
            dtpOrdDate.Value = rsComSql.Fields("OrdDate").Value
            chkApprove.Checked = IIf(rsComSql.Fields("Approval").Value = 1, True, False)
            chkInternal.Checked = IIf(rsComSql.Fields("Internal").Value = 1, True, False)
            txtRemarks.Text = rsComSql.Fields("Subject2").Value
            txtKitNo.Text = rsComSql.Fields("KITNo").Value

            flxDetails.Rows.Clear()
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblOrdersDtls WHERE OrderNo = " & txtOrdNo.Text & " ORDER BY NLineNo", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                rsComSql_1.MoveFirst()
                While Not rsComSql_1.EOF
                    flxDetails.Rows.Add(rsComSql_1.Fields("NLineNo").Value,
                                        rsComSql_1.Fields("RefNo").Value, rsComSql_1.Fields("Side").Value,
                                        rsComSql_1.Fields("Length").Value, rsComSql_1.Fields("Width").Value,
                                        rsComSql_1.Fields("Bothigh").Value, rsComSql_1.Fields("Ronhigh").Value,
                                        rsComSql_1.Fields("Descrip").Value, rsComSql_1.Fields("CulDirect").Value,
                                        rsComSql_1.Fields("IncenCat").Value, rsComSql_1.Fields("Flow").Value,
                                        rsComSql_1.Fields("CutChg").Value,
                                        rsComSql_1.Fields("Sets").Value, rsComSql_1.Fields("Pcs").Value,
                                        rsComSql_1.Fields("Cts").Value, rsComSql_1.Fields("LenTol").Value,
                                        rsComSql_1.Fields("WidTol").Value, rsComSql_1.Fields("HiTol").Value,
                                        rsComSql_1.Fields("Angles").Value, rsComSql_1.Fields("MaxAngle").Value,
                                        rsComSql_1.Fields("Groove").Value, rsComSql_1.Fields("RangeID").Value,
                                        rsComSql_1.Fields("RangeID1").Value, rsComSql_1.Fields("GrCount").Value,
                                        rsComSql_1.Fields("MaxCost").Value, rsComSql_1.Fields("MaxType").Value,
                                        rsComSql_1.Fields("LMHT1").Value, rsComSql_1.Fields("LMHT2").Value,
                                        rsComSql_1.Fields("GDHT1").Value, rsComSql_1.Fields("GDHT2").Value,
                                        txtNiruOrdNo.Text, txtOrderItem.Text, cmbClient.Text, txtCommande.Text,
                                        txtDesc.Text, txtRemarks.Text, Format(rsComSql.Fields("OrdDate").Value, "MM/dd/yyyy"),
                                        Format(rsComSql.Fields("DueDate").Value, "MM/dd/yyyy"), rsComSql_1.Fields("Laser").Value,
                                        rsComSql_1.Fields("OldMaxCost").Value, rsComSql_1.Fields("AskingPrice").Value, txtKitNo.Text)

                    rsComSql_1.MoveNext()
                End While
            End If
            rsComSql_1 = Nothing
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        SaveOrder()
    End Sub

    Private Sub UpdateOrder()
        If Len(txtOrdNo.Text) <> 6 Then
            MsgBox("Invalid Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblOrders WHERE OrderNo = " & CInt(txtOrdNo.Text) & "", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            AdoCN.Execute("UPDATE tblOrders SET Approval = " & IIf(chkApprove.Checked = True, 1, 0) & " WHERE OrderNo = " & CInt(txtOrdNo.Text) & "")

            MsgBox("Approved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        Else
            MsgBox("Invalid Order", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub cmdUpdate_Click(sender As Object, e As EventArgs) Handles cmdUpdate.Click
        UpdateOrder()
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        txtLineNo.Text = flxDetails.Item(0, flxDetails.CurrentRow.Index).Value
        txtRef.Text = flxDetails.Item(1, flxDetails.CurrentRow.Index).Value
        txtSide.Text = flxDetails.Item(2, flxDetails.CurrentRow.Index).Value
        txtLen.Text = flxDetails.Item(3, flxDetails.CurrentRow.Index).Value
        txtWid.Text = flxDetails.Item(4, flxDetails.CurrentRow.Index).Value
        txtBot.Text = flxDetails.Item(5, flxDetails.CurrentRow.Index).Value
        txtRon.Text = flxDetails.Item(6, flxDetails.CurrentRow.Index).Value
        cmbDec.Text = flxDetails.Item(7, flxDetails.CurrentRow.Index).Value
        cmbCul.Text = flxDetails.Item(8, flxDetails.CurrentRow.Index).Value
        cmbIntCat.Text = flxDetails.Item(9, flxDetails.CurrentRow.Index).Value
        cmbFlow.Text = flxDetails.Item(10, flxDetails.CurrentRow.Index).Value
        cmbChg.Text = flxDetails.Item(11, flxDetails.CurrentRow.Index).Value
        txtSets.Text = flxDetails.Item(12, flxDetails.CurrentRow.Index).Value
        txtPcs.Text = flxDetails.Item(13, flxDetails.CurrentRow.Index).Value
        txtTotPcs.Text = CDbl(txtSets.Text) * CDbl(txtPcs.Text)
        txtTLen.Text = flxDetails.Item(15, flxDetails.CurrentRow.Index).Value
        txtTWit.Text = flxDetails.Item(16, flxDetails.CurrentRow.Index).Value
        txtTHgt.Text = flxDetails.Item(17, flxDetails.CurrentRow.Index).Value
        txtAngles.Text = flxDetails.Item(18, flxDetails.CurrentRow.Index).Value
        txtMaxAngle.Text = flxDetails.Item(19, flxDetails.CurrentRow.Index).Value
        txtRangeID.Text = flxDetails.Item(21, flxDetails.CurrentRow.Index).Value
        txtRangeID1.Text = flxDetails.Item(22, flxDetails.CurrentRow.Index).Value
        cmbGroove.Text = flxDetails.Item(23, flxDetails.CurrentRow.Index).Value
        txtMaxCost.Text = flxDetails.Item(24, flxDetails.CurrentRow.Index).Value
        cmbType.Text = flxDetails.Item(25, flxDetails.CurrentRow.Index).Value
        txtLmHt1.Text = flxDetails.Item(26, flxDetails.CurrentRow.Index).Value
        txtLmHt2.Text = flxDetails.Item(27, flxDetails.CurrentRow.Index).Value
        txtGdHt1.Text = flxDetails.Item(28, flxDetails.CurrentRow.Index).Value
        txtGdHt2.Text = flxDetails.Item(29, flxDetails.CurrentRow.Index).Value
        chkGroove.Checked = IIf(flxDetails.Item(20, flxDetails.CurrentRow.Index).Value = 1, True, False)
        chkLaser.Checked = IIf(flxDetails.Item(38, flxDetails.CurrentRow.Index).Value = 1, True, False)
        txtAskPrice.Text = flxDetails.Item(40, flxDetails.CurrentRow.Index).Value
    End Sub

    Private Sub cmbType_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbType.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbGroove.Focus()
        End If
    End Sub

    Private Sub flxDetails_DoubleClick(sender As Object, e As EventArgs) Handles flxDetails.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse  = MsgBoxResult.Yes Then
            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)
        End If
    End Sub

    Private Sub txtLmHt1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLmHt1.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtLmHt1.Text)
        If Asc(e.KeyChar) = 13 Then
            txtLmHt2.Focus()
        End If
    End Sub

    Private Sub txtLmHt2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLmHt2.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtLmHt2.Text)
        If Asc(e.KeyChar) = 13 Then
            txtGdHt1.Focus()
        End If
    End Sub

    Private Sub txtGdHt1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGdHt1.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtGdHt1.Text)
        If Asc(e.KeyChar) = 13 Then
            txtGdHt2.Focus()
        End If
    End Sub

    Private Sub txtGdHt2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGdHt2.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtGdHt2.Text)
    End Sub

    Private Sub cmdOpen_Click(sender As Object, e As EventArgs) Handles cmdOpen.Click
        flxDetails.Rows.Clear()
        OpenFileDialog1.Filter = "All Excel Files|*.xls;*.xlsx"
        OpenFileDialog1.InitialDirectory = "C:\"
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName <> "" Then
            txtFilePath.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Load_Excel()
        On Error GoTo ErrorHandler

        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim intRow, m_LotNo As Integer

        If txtFilePath.Text = "" Then
            MsgBox("Please select the Excel File", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If Len(Dir(txtFilePath.Text)) > 0 Then

            xlApp = New Excel.Application
            xlWorkBook = xlApp.Workbooks.Open(txtFilePath.Text)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            flxDetails.Rows.Clear()

            For intRow = 2 To 10000
                If xlWorkSheet.Cells(intRow, 1).Value = "" Then Exit For
                If intRow = 2 Then
                    txtNiruOrdNo.Text = Trim(xlWorkSheet.Cells(intRow, 31).Value)
                    txtOrderItem.Text = Trim(xlWorkSheet.Cells(intRow, 32).Value)
                    cmbClient.Text = Trim(xlWorkSheet.Cells(intRow, 33).Value)
                    txtCommande.Text = Trim(xlWorkSheet.Cells(intRow, 34).Value)
                    txtDesc.Text = Trim(xlWorkSheet.Cells(intRow, 35).Value)
                    txtRemarks.Text = Trim(xlWorkSheet.Cells(intRow, 36).Value)
                    dtpOrdDate.Value = CDate(Trim(xlWorkSheet.Cells(intRow, 37).Value))
                    dtpDueDate.Value = CDate(Trim(xlWorkSheet.Cells(intRow, 38).Value))
                    txtKitNo.Text = Trim(xlWorkSheet.Cells(intRow, 41).Value)
                    
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblNiruRef WHERE NiruCust = '" & cmbClient.Text & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        cmbColor.Text = rsComSql.Fields("Color").Value
                        cmbClarity.Text = rsComSql.Fields("Clarity").Value
                    End If
                    rsComSql = Nothing
                End If
                flxDetails.Rows.Add(Trim(xlWorkSheet.Cells(intRow, 1).Value), Trim(xlWorkSheet.Cells(intRow, 2).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 3).Value), Trim(xlWorkSheet.Cells(intRow, 4).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 5).Value), Trim(xlWorkSheet.Cells(intRow, 6).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 7).Value), Trim(xlWorkSheet.Cells(intRow, 8).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 9).Value), Trim(xlWorkSheet.Cells(intRow, 10).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 11).Value), Trim(xlWorkSheet.Cells(intRow, 12).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 13).Value), Trim(xlWorkSheet.Cells(intRow, 14).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 15).Value), Trim(xlWorkSheet.Cells(intRow, 16).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 17).Value), Trim(xlWorkSheet.Cells(intRow, 18).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 19).Value), Trim(xlWorkSheet.Cells(intRow, 20).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 21).Value), Trim(xlWorkSheet.Cells(intRow, 22).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 23).Value), Trim(xlWorkSheet.Cells(intRow, 24).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 25).Value), Trim(xlWorkSheet.Cells(intRow, 26).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 27).Value), Trim(xlWorkSheet.Cells(intRow, 28).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 29).Value), Trim(xlWorkSheet.Cells(intRow, 30).Value),
                                    txtNiruOrdNo.Text, txtOrderItem.Text, cmbClient.Text, txtCommande.Text, txtDesc.Text,
                                    txtRemarks.Text, dtpOrdDate.Value, dtpDueDate.Value,
                                    Trim(xlWorkSheet.Cells(intRow, 39).Value), Trim(xlWorkSheet.Cells(intRow, 25).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 40).Value), txtKitNo.Text)

            Next
            xlWorkSheet = Nothing
            xlWorkBook.Close()
            xlApp.Quit()

            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)

            MsgBox("Order Detail Loaded", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        Exit Sub
ErrorHandler:
        MsgBox(Err.Description, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Load_Excel()
    End Sub

    Private Sub cmbClient_TextChanged(sender As Object, e As EventArgs) Handles cmbClient.TextChanged
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT Color, Clarity FROM tblNiruRef WHERE NiruCust = '" & cmbClient.Text & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            cmbColor.Text = rsComSql_1.Fields("Color").Value
            cmbClarity.Text = rsComSql_1.Fields("Clarity").Value
        End If
        rsComSql_1 = Nothing
    End Sub

    Private Sub cmdFlow_Click(sender As Object, e As EventArgs) Handles cmdFlow.Click
        frm_MixPacketFlow.Show()
    End Sub

    Private Sub txtAskPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAskPrice.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtAskPrice.Text)
        If Asc(e.KeyChar) = 13 Then
            cmdAdd.Focus()
        End If
    End Sub
End Class