
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_MixOrderO
    Dim strFolderPath As String

    Private Sub ClearText()
        'txtOrdNo.Text = ""
        cmbSupp.Text = "Niru Diamonds Israel (1987) Ltd"
        cmbClient.Text = ""
        txtDesc.Text = ""
        cmbOrderType.Text = ""
        txtRemarks.Text = ""
        txtColor.Text = ""
        txtClarity.Text = ""
        txtFlo.Text = ""
        dtpDueDate.Value = Date.Now
        dtpOrdDate.Value = Date.Now
        dtpEstDueDate.Value = Date.Now

        txtRef.Text = ""
        txtSide.Text = ""
        txtLen.Text = ""
        txtWid.Text = ""
        txtSets.Text = ""
        txtPcs.Text = ""
        txtTotPcs.Text = ""
        txtMaxCost.Text = ""
        cmbType.Text = ""
        txtNiruOrdNo.Text = ""
        txtOrderItem.Text = ""
        txtCommande.Text = ""
        chkGroove.Checked = False
        cmbGroove.Text = ""
        txtGrRate.Text = ""
        txtLaser.Text = ""
        txtLaserRate.Text = ""
        txtSalesPrice.Text = ""
        txtBrokerPrice.Text = ""

        chkConfirm.Checked = False

        flxDetails.Rows.Clear()
    End Sub

    Private Sub GetNewOrderNo()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(OrderNo) AS OrdNo FROM tblOrdersO", AdoCN, 1, 1)
        If IsDBNull(rsComSql.Fields("OrdNo").Value) Then
            txtOrdNo.Text = "200001"
        Else
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

    Private Sub txtOrdNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrdNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            Load_OrderDetails()
        End If
    End Sub

    Private Sub txtNiruOrdNo_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Asc(e.KeyChar) = 13 Then
            cmbSupp.Focus()
        End If
    End Sub

    Private Sub cmbSupp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSupp.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbClient.Focus()
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

    Private Sub txtLen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLen.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtWid.Focus()
        End If
    End Sub

    Private Sub txtWid_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWid.KeyPress
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
            txtSalesPrice.Focus()
        End If
    End Sub

    Private Sub txtMaxCost_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMaxCost.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtMaxCost.Text)
        If Asc(e.KeyChar) = 13 Then
            cmbType.Focus()
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        AddReference()
    End Sub

    Private Sub AddReference()
        Dim dblMaxCost As Double
        Dim dblAskingPrice As Double

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
            If txtGrRate.Text = "" Then
                MsgBox("Invalid Groove Rate", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtLaser.Text = "" Then
                MsgBox("Invalid Laser Count", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtLaserRate.Text = "" Then
                MsgBox("Invalid Laser Rate", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtSalesPrice.Text = "" Then
                MsgBox("Invalid Sales Price", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtBrokerPrice.Text = "" Then
                MsgBox("Invalid Broker Price", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            dblAskingPrice = CDbl(txtSalesPrice.Text) + (CDbl(cmbGroove.Text) * CDbl(txtGrRate.Text)) + (CDbl(txtLaser.Text) * CDbl(txtLaserRate.Text))
            dblMaxCost = Math.Round(dblAskingPrice * 0.96, 0)

            flxDetails.Rows.Add(txtRef.Text, txtSide.Text, Format(CSng(txtLen.Text), "#0.00"), Format(CSng(txtWid.Text), "#0.00"),
                                txtSets.Text, txtPcs.Text, txtTotPcs.Text, dblMaxCost, cmbType.Text, "",
                                IIf(chkGroove.Checked = True, 1, 0), cmbGroove.Text, txtLaser.Text, txtSalesPrice.Text, txtBrokerPrice.Text,
                                "", txtGrRate.Text, txtLaserRate.Text, dblAskingPrice)

            txtRef.Text = ""
            txtSide.Text = ""
            txtLen.Text = ""
            txtWid.Text = ""
            txtSets.Text = ""
            txtPcs.Text = ""
            txtTotPcs.Text = ""
            txtMaxCost.Text = ""
            cmbType.Text = ""
            chkGroove.Checked = False
            cmbGroove.Text = ""
            txtGrRate.Text = ""
            txtLaser.Text = ""
            txtLaserRate.Text = ""
            txtSalesPrice.Text = ""
            txtBrokerPrice.Text = ""
            dblMaxCost = 0
            dblAskingPrice = 0
            txtRef.Focus()
        End If
    End Sub

    Private Sub SaveOrder()
        Dim intRow As Integer
        Dim dblCutRate As Double
        Dim blnAccess As Boolean

        dblCutRate = 0
        blnAccess = False

        If Len(txtOrdNo.Text) <> 6 Then
            MsgBox("Invalid Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If Len(txtNiruOrdNo.Text) = 0 Then
            MsgBox("Invalid Niru Order Number. Please Process", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If Len(txtOrderItem.Text) = 0 Then
            MsgBox("Invalid Order Item", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If Len(cmbOrderType.Text) = 0 Then
            MsgBox("Invalid Order Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        For intRow = 0 To flxDetails.Rows.Count - 1
            If Trim(flxDetails.Item(9, intRow).Value) = "" Then
                MsgBox("Line No. cannot be blank. Please Process", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT OrderNo, NLineNo FROM tblOrdersDtlsO WHERE NLineNo = '" & Trim(flxDetails.Item(9, intRow).Value) & "' AND OrderNo <> '" & txtOrdNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                MsgBox("Line No. already eneted - " & rsComSql.Fields("OrderNo").Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing
        Next

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblOrdersO WHERE OrderNo = " & CInt(txtOrdNo.Text) & "", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                mStrSQL = "INSERT INTO tblOrdersO(OrderNo,Customer,DueDate,Subject,Niruref,OrdDate,Complete,Dept,Subject2,NorderNo,OrderItem,COMMANDE,EnterDate,Color,Clarity,Flo,EstDueDate,OrderType) " & _
                          "VALUES(" & CInt(txtOrdNo.Text) & ",'" & cmbSupp.Text & "','" & Format(dtpDueDate.Value, "MM/dd/yyyy") & "','" & Trim(txtDesc.Text) & "','" & cmbClient.Text & "'," & _
                            "'" & Format(dtpOrdDate.Value, "MM/dd/yyyy") & "','N','','" & Trim(txtRemarks.Text) & "','" & Trim(txtNiruOrdNo.Text) & "','" & Trim(txtOrderItem.Text) & "','" & Trim(txtCommande.Text) & "'," & _
                            "'" & Format(Date.Now, "MM/dd/yyyy") & "','" & txtColor.Text & "','" & txtClarity.Text & "','" & txtFlo.Text & "','" & Format(dtpEstDueDate.Value, "MM/dd/yyyy") & "','" & cmbOrderType.Text & "')"

                AdoCN.Execute(mStrSQL)

                For intRow = 0 To flxDetails.Rows.Count - 1
                    mStrSQL = "INSERT INTO tblOrdersDtlsO(OrderNo,RefNo,Side,Length,Width,Sets,PCs,MaxCost,MaxType,NLineNo,Groove,GrCount,Laser,SalesPrice,BrokerPrice,NLineNo2,GrRate,LaserRate,AskingPrice) " & _
                              "VALUES(" & CInt(txtOrdNo.Text) & ",'" & Replace(Trim(flxDetails.Item(0, intRow).Value), "'", "''") & "','" & Trim(flxDetails.Item(1, intRow).Value) & "','" & Trim(flxDetails.Item(2, intRow).Value) & "','" & Trim(flxDetails.Item(3, intRow).Value) & "'," & _
                                "" & CInt(flxDetails.Item(4, intRow).Value) & "," & CInt(flxDetails.Item(5, intRow).Value) & "," & CDbl(flxDetails.Item(7, intRow).Value) & ",'" & Trim(flxDetails.Item(8, intRow).Value) & "','" & Trim(flxDetails.Item(9, intRow).Value) & "'," & _
                                "'" & Trim(flxDetails.Item(10, intRow).Value) & "','" & Trim(flxDetails.Item(11, intRow).Value) & "','" & Trim(flxDetails.Item(12, intRow).Value) & "'," & CDbl(flxDetails.Item(13, intRow).Value) & "," & CDbl(flxDetails.Item(14, intRow).Value) & "," & _
                                "'" & flxDetails.Item(15, intRow).Value & "'," & CDbl(flxDetails.Item(16, intRow).Value) & "," & CDbl(flxDetails.Item(17, intRow).Value) & "," & CDbl(flxDetails.Item(18, intRow).Value) & ")"

                    AdoCN.Execute(mStrSQL)
                Next

                MsgBox("Order Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearText()
                GetNewOrderNo()
            End If
        Else
            PBResponse = MsgBox("Already Exists. Are you sure to Update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                blnAccess = True
                If blnAccess = True Then
                    mStrSQL = "UPDATE tblOrdersO SET Customer = '" & cmbSupp.Text & "',DueDate = '" & Format(dtpDueDate.Value, "MM/dd/yyyy") & "',Subject = '" & Trim(txtDesc.Text) & "',Niruref = '" & cmbClient.Text & "',OrdDate = '" & Format(dtpOrdDate.Value, "MM/dd/yyyy") & "',EstDueDate = '" & Format(dtpEstDueDate.Value, "MM/dd/yyyy") & "', " & _
                                "Subject2 = '" & Trim(txtRemarks.Text) & "',COMMANDE = '" & txtCommande.Text & "',NorderNo = '" & txtNiruOrdNo.Text & "',OrderItem = '" & txtOrderItem.Text & "',Color = '" & txtColor.Text & "',Clarity = '" & txtClarity.Text & "',Flo = '" & txtFlo.Text & "',OrderType = '" & cmbOrderType.Text & "' " & _
                              "WHERE OrderNo = " & CInt(txtOrdNo.Text) & ""

                    AdoCN.Execute(mStrSQL)

                    mStrSQL = "DELETE FROM tblOrdersDtlsO WHERE OrderNo = " & CInt(txtOrdNo.Text) & ""

                    AdoCN.Execute(mStrSQL)


                    For intRow = 0 To flxDetails.Rows.Count - 1
                        mStrSQL = "INSERT INTO tblOrdersDtlsO(OrderNo,RefNo,Side,Length,Width,Sets,PCs,MaxCost,MaxType,NLineNo,Groove,GrCount,Laser,SalesPrice,BrokerPrice,NLineNo2,GrRate,LaserRate,AskingPrice) " & _
                                  "VALUES(" & CInt(txtOrdNo.Text) & ",'" & Replace(Trim(flxDetails.Item(0, intRow).Value), "'", "''") & "','" & Trim(flxDetails.Item(1, intRow).Value) & "','" & Trim(flxDetails.Item(2, intRow).Value) & "','" & Trim(flxDetails.Item(3, intRow).Value) & "'," & _
                                    "" & CInt(flxDetails.Item(4, intRow).Value) & "," & CInt(flxDetails.Item(5, intRow).Value) & "," & CDbl(flxDetails.Item(7, intRow).Value) & ",'" & Trim(flxDetails.Item(8, intRow).Value) & "','" & Trim(flxDetails.Item(9, intRow).Value) & "'," & _
                                    "'" & Trim(flxDetails.Item(10, intRow).Value) & "','" & Trim(flxDetails.Item(11, intRow).Value) & "','" & Trim(flxDetails.Item(12, intRow).Value) & "'," & CDbl(flxDetails.Item(13, intRow).Value) & "," & CDbl(flxDetails.Item(14, intRow).Value) & "," & _
                                    "'" & flxDetails.Item(15, intRow).Value & "'," & CDbl(flxDetails.Item(16, intRow).Value) & "," & CDbl(flxDetails.Item(17, intRow).Value) & "," & CDbl(flxDetails.Item(18, intRow).Value) & ")"

                        AdoCN.Execute(mStrSQL)
                    Next

                    'For intRow = 0 To flxDetails.Rows.Count - 1
                    '    rsComSql_1 = New ADODB.Recordset
                    '    rsComSql_1.Open("SELECT NLineNo FROM tblOrdersDtlsO WHERE NLineNo = '" & Trim(flxDetails.Item(9, intRow).Value) & "'", AdoCN, 1, 1)
                    '    If rsComSql_1.RecordCount = 0 Then
                    '        mStrSQL = "INSERT INTO tblOrdersDtlsO(OrderNo,RefNo,Side,Length,Width,Sets,PCs,MaxCost,MaxType,NLineNo,Groove,GrCount,Laser,SalesPrice,BrokerPrice,NLineNo2,GrRate,LaserRate) " & _
                    '                  "VALUES(" & CInt(txtOrdNo.Text) & ",'" & Replace(Trim(flxDetails.Item(0, intRow).Value), "'", "''") & "','" & Trim(flxDetails.Item(1, intRow).Value) & "','" & Trim(flxDetails.Item(2, intRow).Value) & "','" & Trim(flxDetails.Item(3, intRow).Value) & "'," & _
                    '                    "" & CInt(flxDetails.Item(4, intRow).Value) & "," & CInt(flxDetails.Item(5, intRow).Value) & "," & CDbl(flxDetails.Item(7, intRow).Value) & ",'" & Trim(flxDetails.Item(8, intRow).Value) & "','" & Trim(flxDetails.Item(9, intRow).Value) & "'," & _
                    '                    "'" & Trim(flxDetails.Item(10, intRow).Value) & "','" & Trim(flxDetails.Item(11, intRow).Value) & "','" & Trim(flxDetails.Item(12, intRow).Value) & "'," & CDbl(flxDetails.Item(13, intRow).Value) & "," & CDbl(flxDetails.Item(14, intRow).Value) & "," & _
                    '                    "'" & flxDetails.Item(15, intRow).Value & "'," & CDbl(flxDetails.Item(16, intRow).Value) & "," & CDbl(flxDetails.Item(17, intRow).Value) & ")"

                    '        AdoCN.Execute(mStrSQL)
                    '    Else
                    '        mStrSQL = "UPDATE tblOrdersDtlsO SET RefNo = '" & Replace(Trim(flxDetails.Item(0, intRow).Value), "'", "''") & "',Side = '" & Trim(flxDetails.Item(1, intRow).Value) & "',Length = '" & Trim(flxDetails.Item(2, intRow).Value) & "',Width = '" & Trim(flxDetails.Item(3, intRow).Value) & "'," & _
                    '                    "Sets = " & CInt(flxDetails.Item(4, intRow).Value) & ",PCs = " & CInt(flxDetails.Item(5, intRow).Value) & ",MaxCost = " & CDbl(flxDetails.Item(7, intRow).Value) & ",MaxType = '" & Trim(flxDetails.Item(8, intRow).Value) & "',Groove = '" & Trim(flxDetails.Item(10, intRow).Value) & "'," & _
                    '                    "GrCount = '" & Trim(flxDetails.Item(11, intRow).Value) & "',Laser = '" & Trim(flxDetails.Item(12, intRow).Value) & "',SalesPrice = " & CDbl(flxDetails.Item(13, intRow).Value) & ",BrokerPrice = " & CDbl(flxDetails.Item(14, intRow).Value) & "," & _
                    '                    "GrRate = " & CDbl(flxDetails.Item(16, intRow).Value) & ",LaserRate = " & CDbl(flxDetails.Item(17, intRow).Value) & " " & _
                    '                  "WHERE NLineNo = '" & Trim(flxDetails.Item(9, intRow).Value) & "'"

                    '        AdoCN.Execute(mStrSQL)
                    '    End If
                    '    rsComSql_1 = Nothing

                    'Next

                    MsgBox("Order Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    ClearText()
                    GetNewOrderNo()
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
        rsComSql.Open("SELECT * FROM tblOrdersO WHERE OrderNo = " & CInt(txtOrdNo.Text) & "", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            blnFound = True
            cmbSupp.Text = rsComSql.Fields("Customer").Value
            txtDesc.Text = rsComSql.Fields("Subject").Value
            cmbClient.Text = rsComSql.Fields("Niruref").Value
            dtpDueDate.Value = rsComSql.Fields("DueDate").Value
            dtpOrdDate.Value = rsComSql.Fields("OrdDate").Value
            dtpEstDueDate.Value = rsComSql.Fields("EstDueDate").Value
            txtRemarks.Text = rsComSql.Fields("Subject2").Value
            txtNiruOrdNo.Text = rsComSql.Fields("NorderNo").Value
            txtOrderItem.Text = rsComSql.Fields("OrderItem").Value
            txtCommande.Text = rsComSql.Fields("COMMANDE").Value
            cmbOrderType.Text = rsComSql.Fields("OrderType").Value
            txtColor.Text = rsComSql.Fields("Color").Value
            txtClarity.Text = rsComSql.Fields("Clarity").Value
            txtFlo.Text = rsComSql.Fields("Flo").Value
            If rsComSql.Fields("Confirmed").Value = 1 Then
                chkConfirm.Checked = True
            Else
                chkConfirm.Checked = False
            End If

            flxDetails.Rows.Clear()
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblOrdersDtlsO WHERE OrderNo = " & CInt(txtOrdNo.Text) & " ORDER BY ID", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                rsComSql_1.MoveFirst()
                While Not rsComSql_1.EOF
                    flxDetails.Rows.Add(rsComSql_1.Fields("RefNo").Value,
                                        rsComSql_1.Fields("Side").Value,
                                        rsComSql_1.Fields("Length").Value,
                                        rsComSql_1.Fields("Width").Value,
                                        rsComSql_1.Fields("Sets").Value,
                                        rsComSql_1.Fields("Pcs").Value,
                                        rsComSql_1.Fields("Sets").Value * rsComSql_1.Fields("Pcs").Value,
                                        rsComSql_1.Fields("MaxCost").Value,
                                        rsComSql_1.Fields("MaxType").Value,
                                        rsComSql_1.Fields("NLineNo").Value,
                                        rsComSql_1.Fields("Groove").Value,
                                        rsComSql_1.Fields("GrCount").Value,
                                        rsComSql_1.Fields("Laser").Value,
                                        rsComSql_1.Fields("SalesPrice").Value,
                                        rsComSql_1.Fields("BrokerPrice").Value,
                                        rsComSql_1.Fields("NLineNo2").Value,
                                        rsComSql_1.Fields("GrRate").Value,
                                        rsComSql_1.Fields("LaserRate").Value,
                                        rsComSql_1.Fields("AskingPrice").Value)

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

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        'txtRef.Text = flxDetails.Item(0, flxDetails.CurrentRow.Index).Value
        'txtSide.Text = flxDetails.Item(1, flxDetails.CurrentRow.Index).Value
        'txtLen.Text = flxDetails.Item(2, flxDetails.CurrentRow.Index).Value
        'txtWid.Text = flxDetails.Item(3, flxDetails.CurrentRow.Index).Value
        'txtSets.Text = flxDetails.Item(4, flxDetails.CurrentRow.Index).Value
        'txtPcs.Text = flxDetails.Item(5, flxDetails.CurrentRow.Index).Value
        'txtTotPcs.Text = CDbl(txtSets.Text) * CDbl(txtPcs.Text)
        'txtMaxCost.Text = flxDetails.Item(7, flxDetails.CurrentRow.Index).Value
        'cmbType.Text = flxDetails.Item(8, flxDetails.CurrentRow.Index).Value
    End Sub

    Private Sub cmbType_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbType.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbGroove.Focus()
        End If
    End Sub

    Private Sub flxDetails_DoubleClick(sender As Object, e As EventArgs) Handles flxDetails.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)
        End If
    End Sub

    Private Sub txtSide_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSide.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtLen.Focus()
        End If
    End Sub

    Private Sub cmdReport_Click(sender As Object, e As EventArgs) Handles cmdReport.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixOrderOffer.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub frm_MixOrderT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        If strDBName = "DiaStock" Then
            strFolderPath = "Mix\"
        Else
            strFolderPath = "DiaSalesMix\"
        End If

        Load_Supplier()
        Load_Client()

        ClearText()
        GetNewOrderNo()
    End Sub

    Private Sub Process()
        Dim intRow As Integer
        Dim strNLineNo As String

        If txtCommande.Text = "" Then
            MsgBox("Please enter the Commande", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT NorderNo FROM tblOrdersO WHERE COMMANDE = '" & txtCommande.Text & "' AND Niruref = '" & cmbClient.Text & "' AND DueDate = '" & Format(dtpDueDate.Value, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            txtNiruOrdNo.Text = rsComSql.Fields("NorderNo").Value
        End If
        rsComSql = Nothing

        If txtNiruOrdNo.Text = "" Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT MAX(NorderNo) AS NorderNo FROM tblOrdersO", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                txtNiruOrdNo.Text = CDbl(rsComSql.Fields("NorderNo").Value) + 1
            End If
            rsComSql = Nothing
        End If

        If txtOrderItem.Text <> "" Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                strNLineNo = txtNiruOrdNo.Text & "-" & txtOrderItem.Text & "-" & intRow + 1
                flxDetails.Item(9, intRow).Value = strNLineNo
            Next
        End If
    End Sub

    Private Sub ProcessNew()
        Dim intRow As Integer
        Dim strNLineNo As String

        If txtCommande.Text = "" Then
            MsgBox("Please enter the Commande", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        'rsComSql = New ADODB.Recordset
        'rsComSql.Open("SELECT NorderNo FROM tblOrdersO WHERE COMMANDE = '" & txtCommande.Text & "' AND Niruref = '" & cmbClient.Text & "' AND DueDate = '" & Format(dtpDueDate.Value, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
        'If rsComSql.RecordCount Then
        '    txtNiruOrdNo.Text = rsComSql.Fields("NorderNo").Value
        'End If
        'rsComSql = Nothing

        If txtNiruOrdNo.Text = "" Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT MAX(NorderNo) AS NorderNo FROM tblOrdersO", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                txtNiruOrdNo.Text = CDbl(rsComSql.Fields("NorderNo").Value) + 1
            End If
            rsComSql = Nothing
        End If

        If txtOrderItem.Text <> "" Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                strNLineNo = txtNiruOrdNo.Text & "-" & txtOrderItem.Text & "-" & intRow + 1
                flxDetails.Item(9, intRow).Value = strNLineNo
            Next
        End If
    End Sub

    Private Sub cmdProcess_Click(sender As Object, e As EventArgs) Handles cmdProcess.Click
        Process()
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
        'Dim dblMaxCost As Double
        'Dim dblSalesPrice As Double
        'Dim dblBrokerPrice As Double
        'Dim intSets As Integer

        If txtFilePath.Text = "" Then
            MsgBox("Please select the Excel File", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        'If txtSets.Text = "" Then
        '    MsgBox("Please enter the number of sets", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '    Exit Sub
        'End If

        'If CInt(txtSets.Text) <= 0 Then
        '    MsgBox("Invalid number of sets", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '    Exit Sub
        'End If

        If Len(Dir(txtFilePath.Text)) > 0 Then

            xlApp = New Excel.Application
            xlWorkBook = xlApp.Workbooks.Open(txtFilePath.Text)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            flxDetails.Rows.Clear()

            'intSets = CInt(txtSets.Text)

            For intRow = 2 To 10000
                If xlWorkSheet.Cells(intRow, 1).Value = "" Then Exit For
                If intRow = 2 Then
                    txtDesc.Text = Trim(xlWorkSheet.Cells(intRow, 1).Value)
                    cmbClient.Text = Trim(xlWorkSheet.Cells(intRow, 2).Value)
                    txtOrderItem.Text = Trim(xlWorkSheet.Cells(intRow, 3).Value)
                    txtCommande.Text = Trim(xlWorkSheet.Cells(intRow, 4).Value)
                End If
                'dblSalesPrice = Trim(xlWorkSheet.Cells(intRow, 10).Value)
                'dblMaxCost = Math.Round(dblSalesPrice * 0.95, 0)
                'dblBrokerPrice = 0

                flxDetails.Rows.Add(Trim(xlWorkSheet.Cells(intRow, 5).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 6).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 7).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 8).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 9).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 10).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 9).Value) * Trim(xlWorkSheet.Cells(intRow, 10).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 11).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 12).Value),
                                    "",
                                    IIf(Trim(xlWorkSheet.Cells(intRow, 13).Value) > 0, 1, 0),
                                    Trim(xlWorkSheet.Cells(intRow, 14).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 15).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 16).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 17).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 18).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 19).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 20).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 21).Value))

            Next
            xlWorkSheet = Nothing
            xlWorkBook.Close()
            xlApp.Quit()

            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)

            MsgBox("Offer Detail Loaded", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        Exit Sub
ErrorHandler:
        MsgBox(Err.Description, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        releaseObject(xlApp)
        releaseObject(xlWorkBook)
        releaseObject(xlWorkSheet)
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

    Private Sub txtSalesPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSalesPrice.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtSalesPrice.Text)
        If Asc(e.KeyChar) = 13 Then
            If txtSalesPrice.Text <> "" Then
                If CDbl(txtSalesPrice.Text) > 0 Then
                    txtBrokerPrice.Text = Math.Round(CDbl(txtSalesPrice.Text) * 0.97, 0)
                    txtMaxCost.Text = Math.Round(CDbl(txtSalesPrice.Text) * 0.92, 0)
                End If
            End If
            txtBrokerPrice.Focus()
        End If
    End Sub

    Private Sub txtBrokerPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBrokerPrice.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtBrokerPrice.Text)
        If Asc(e.KeyChar) = 13 Then
            If txtBrokerPrice.Text <> "" Then
                If CDbl(txtBrokerPrice.Text) = 0 Then
                    txtMaxCost.Text = Math.Round(CDbl(txtSalesPrice.Text) * 0.96, 0)
                End If
            End If
            txtMaxCost.Focus()
        End If
    End Sub

    Private Sub Delete()
        If txtOrdNo.Text <> "" Then
            PBResponse = MsgBox("Are you sure to Delete this Order?", MsgBoxStyle.Information + vbYesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblOrdersO WHERE OrderNo = '" & txtOrdNo.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    AdoCN.Execute("DELETE FROM tblOrdersO WHERE OrderNo = '" & txtOrdNo.Text & "'")
                    AdoCN.Execute("DELETE FROM tblOrdersDtlsO WHERE OrderNo = '" & txtOrdNo.Text & "'")

                    MsgBox("Order Deleted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    ClearText()
                    GetNewOrderNo()
                Else
                    MsgBox("Invalid Order", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                End If
                rsComSql = Nothing
            End If
        Else
            MsgBox("Please fill all the entries before Delete", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Delete()
    End Sub

    Private Sub cmbGroove_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbGroove.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtGrRate.Focus()
        End If
    End Sub

    Private Sub txtGrRate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGrRate.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtLaser.Focus()
        End If
    End Sub

    Private Sub txtLaser_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLaser.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtLaserRate.Focus()
        End If
    End Sub

    Private Sub txtLaserRate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLaserRate.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub UpdateOrder()
        If txtOrdNo.Text <> "" Then
            PBResponse = MsgBox("Are you sure to Update this Order?", MsgBoxStyle.Information + vbYesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblOrdersO WHERE OrderNo = '" & txtOrdNo.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    AdoCN.Execute("UPDATE tblOrdersO SET Confirmed = '" & IIf(chkConfirm.Checked = True, 1, 0) & "' WHERE OrderNo = '" & txtOrdNo.Text & "'")

                    MsgBox("Order Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    ClearText()
                    GetNewOrderNo()
                Else
                    MsgBox("Invalid Order", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                End If
                rsComSql = Nothing
            End If
        Else
            MsgBox("Please fill all the entries before Delete", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub cmdUpdate_Click(sender As Object, e As EventArgs) Handles cmdUpdate.Click
        UpdateOrder()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixOrderOffer2.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub dtpDueDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpDueDate.ValueChanged
        dtpEstDueDate.Value = DateAdd(DateInterval.Day, -7, dtpDueDate.Value)
    End Sub

    Private Sub cmbClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbClient.SelectedIndexChanged
        txtClientName.Text = ""
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblNiruRef WHERE NiruCust = '" & cmbClient.Text & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            txtClientName.Text = rsComSql_1.Fields("ClientName").Value
        End If
        rsComSql_1 = Nothing
    End Sub

    Private Sub cmdProcessNew_Click(sender As Object, e As EventArgs) Handles cmdProcessNew.Click
        ProcessNew()
    End Sub
End Class