
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_KITOrder

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearText()
        GetNewOrderNo()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ClearText()
        'txtOrdNo.Text = ""
        txtNiruOrdNo.Text = ""
        cmbSupp.Text = ""
        cmbClient.Text = ""
        txtOrderItem.Text = ""
        txtCommande.Text = ""
        txtAssort.Text = ""
        txtItemName.Text = ""
        cmbTol.Text = ""
        txtDesc.Text = ""
        cmbColor.Text = ""
        cmbClarity.Text = ""
        cmbCountry.Text = ""
        cmbLocation.Text = ""
        cmbDept.Text = ""
        txtSalesRate.Text = ""
        dtpDueDate.Value = Format(Date.Now, "yyyy/MM/dd")
        dtpOrdDate.Value = Format(Date.Now, "yyyy/MM/dd")

        cmbRef.Text = ""
        cmbSide.Text = ""
        txtLen.Text = ""
        txtWid.Text = ""
        txtBot.Text = ""
        txtRon.Text = ""
        txtLineNo.Text = ""
        txtTLen.Text = ""
        txtTWit.Text = ""
        txtTHgt.Text = ""
        cmbDec.Text = ""
        cmbCul.Text = ""
        cmbIntCat.Text = ""
        cmbFlow.Text = ""
        cmbChg.Text = ""
        txtSets.Text = ""
        txtPcs.Text = ""
        txtTotPcs.Text = ""
        txtOrderCts.Text = ""
        txtShLen.Text = ""
        txtAssort2.Text = ""
        chkGroove.Checked = True
        cmbGroove.Text = ""
        txtOutPrice.Text = ""
        cmbType.Text = ""
        txtRemarks.Text = ""

        flxDetails.Rows.Clear()
    End Sub

    Private Sub GetNewOrderNo()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(OrderNo) AS OrdNo FROM tblKITOrders", AdoCN, 1, 1)
        If IsDBNull(rsComSql.Fields("OrdNo").Value) Then
            txtOrdNo.Text = "100001"
        Else
            txtOrdNo.Text = CInt(rsComSql.Fields("OrdNo").Value) + 1
        End If
        rsComSql = Nothing
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

    Private Sub Load_Country()
        cmbCountry.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Country FROM tblCountry ORDER BY Country", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            Do While Not rsComSql.EOF
                cmbCountry.Items.Add(rsComSql.Fields("Country").Value)
                rsComSql.MoveNext()
            Loop
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Ref()
        cmbRef.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT KitRef FROM tblKITRef GROUP BY KitRef ORDER BY KitRef", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            Do While Not rsComSql.EOF
                cmbRef.Items.Add(rsComSql.Fields("KitRef").Value)
                rsComSql.MoveNext()
            Loop
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Location()
        cmbLocation.Items.Clear()
        cmbLocation.Items.Add("BAGUETTES")
        cmbLocation.Items.Add("EMERALD")
        cmbLocation.Items.Add("NLE")
        cmbLocation.Items.Add("PCU")
        cmbLocation.Items.Add("PRINCESS")
        cmbLocation.Items.Add("ROUNDS")
    End Sub

    Private Sub Load_Dept()
        cmbDept.Items.Clear()
        cmbDept.Items.Add("GRADING")
        cmbDept.Items.Add("PCU")
    End Sub

    Private Sub Load_OrderDetails()
        Dim blnFound As Boolean

        ClearText()
        blnFound = False
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblKITOrders WHERE OrderNo = " & txtOrdNo.Text & "", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            blnFound = True
            cmbSupp.Text = rsComSql.Fields("Customer").Value
            txtDesc.Text = rsComSql.Fields("Subject").Value
            cmbClient.Text = rsComSql.Fields("Niruref").Value
            cmbTol.Text = rsComSql.Fields("Toler").Value
            txtNiruOrdNo.Text = CDbl(rsComSql.Fields("NorderNo").Value) & ""
            txtOrderItem.Text = rsComSql.Fields("OrderItem").Value
            txtCommande.Text = rsComSql.Fields("COMMANDE").Value
            cmbColor.Text = rsComSql.Fields("Color").Value
            cmbClarity.Text = rsComSql.Fields("Clarity").Value
            cmbCountry.Text = rsComSql.Fields("Country").Value
            cmbDept.Text = rsComSql.Fields("Dept").Value
            cmbLocation.Text = rsComSql.Fields("Type").Value
            txtAssort.Text = rsComSql.Fields("Assortment").Value
            txtItemName.Text = rsComSql.Fields("ItemName").Value
            txtSalesRate.Text = rsComSql.Fields("SalesRate").Value
            chkSpecial.Checked = IIf(rsComSql.Fields("Special").Value = 1, True, False)
            dtpDueDate.Value = Format(rsComSql.Fields("DueDate").Value, "yyyy/MM/dd")
            dtpOrdDate.Value = Format(rsComSql.Fields("OrdDate").Value, "yyyy/MM/dd")

            flxDetails.Rows.Clear()
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblKITOrdersDtls WHERE OrderNo = " & txtOrdNo.Text & " ORDER BY NLineNo,RefNo", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                rsComSql_1.MoveFirst()
                While Not rsComSql_1.EOF
                    flxDetails.Rows.Add(rsComSql_1.Fields("NLineNo").Value,
                                        rsComSql_1.Fields("RefNo").Value,
                                        rsComSql_1.Fields("Side").Value,
                                        rsComSql_1.Fields("Length").Value,
                                        rsComSql_1.Fields("Width").Value,
                                        rsComSql_1.Fields("Bothigh").Value,
                                        rsComSql_1.Fields("Ronhigh").Value,
                                        rsComSql_1.Fields("Descrip").Value,
                                        rsComSql_1.Fields("CulDirect").Value,
                                        rsComSql_1.Fields("IncenCat").Value,
                                        rsComSql_1.Fields("Flow").Value,
                                        rsComSql_1.Fields("CutChg").Value,
                                        rsComSql_1.Fields("Sets").Value,
                                        rsComSql_1.Fields("Pcs").Value,
                                        rsComSql_1.Fields("Cts").Value,
                                        rsComSql_1.Fields("LenTol").Value,
                                        rsComSql_1.Fields("WidTol").Value,
                                        rsComSql_1.Fields("HiTol").Value,
                                        rsComSql_1.Fields("ShortLen").Value,
                                        rsComSql_1.Fields("OrderCts").Value,
                                        rsComSql_1.Fields("Groove").Value,
                                        rsComSql_1.Fields("GrCount").Value,
                                        rsComSql_1.Fields("Assortment").Value,
                                        rsComSql_1.Fields("OutPrice").Value,
                                        rsComSql_1.Fields("MaxType").Value,
                                        rsComSql_1.Fields("Remarks").Value)

                    rsComSql_1.MoveNext()
                End While
            End If
            rsComSql_1 = Nothing
        End If
        rsComSql = Nothing
    End Sub

    Private Sub txtOrdNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrdNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            Load_OrderDetails()
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        AddReference()
    End Sub

    Private Sub AddReference()
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure to Add this Ref?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If cmbRef.Text = "" Then
                MsgBox("Invalid Reference", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            'If cmbSide.Text = "" Then
            '    MsgBox("Invalid Side", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '    Exit Sub
            'End If
            If txtLen.Text = "" Then
                MsgBox("Invalid Length", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            'If Not IsNumeric(txtLen.Text) Then
            '    MsgBox("Invalid Length", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '    Exit Sub
            'End If
            If txtWid.Text = "" Then
                MsgBox("Invalid Width", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            'If Not IsNumeric(txtWid.Text) Then
            '    MsgBox("Invalid Width", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '    Exit Sub
            'End If
            If Len(cmbIntCat.Text) <> 2 Then
                MsgBox("Invalid Incentive Category", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtLineNo.Text = "" Then
                MsgBox("Invalid Line No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtOrderCts.Text = "" Then
                MsgBox("Invalid Order Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
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
            'If txtAssort2.Text = "" Then
            '    MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '    Exit Sub
            'End If
            'If txtOutPrice.Text = "" Then
            '    MsgBox("Invalid Out Price", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '    Exit Sub
            'End If
            If cmbType.Text = "" Then
                MsgBox("Invalid Price Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If CDbl(txtOutPrice.Text) < 0 Then
                MsgBox("Invalid Out Price", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtRemarks.Text = "" Then
                MsgBox("Invalid Remarks", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblKITRef WHERE KitRef = '" & cmbRef.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Reference", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            'rsComSql = New ADODB.Recordset
            'rsComSql.Open("SELECT * FROM tblKITOrdersDtls WHERE NLineNo = '" & Trim(txtLineNo.Text) & "' AND OrderNo <> " & CInt(txtOrdNo.Text) & "", AdoCN, 1, 1)
            'If rsComSql.RecordCount Then
            '    MsgBox("Line No. already exists " & rsComSql.Fields("OrderNo").Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '    Exit Sub
            'End If
            'rsComSql = Nothing

            For intRow = 0 To flxDetails.Rows.Count - 1
                If Trim(cmbRef.Text) = Trim(flxDetails.Item(1, intRow).Value) And Trim(txtLineNo.Text) = Trim(flxDetails.Item(0, intRow).Value) Then
                    MsgBox("Ref/Line No duplicated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Next

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblKITRef WHERE KitRef = '" & cmbRef.Text & "' ORDER BY DiamondRef", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    flxDetails.Rows.Add(txtLineNo.Text, cmbRef.Text, UCase(rsComSql.Fields("DiamondRef").Value), txtLen.Text,
                                        txtWid.Text, txtBot.Text, txtRon.Text, cmbDec.Text, cmbCul.Text,
                                        cmbIntCat.Text, cmbFlow.Text, cmbChg.Text, txtSets.Text, rsComSql.Fields("KitPcs").Value, txtOrderCts.Text, txtTLen.Text,
                                        txtTWit.Text, txtTHgt.Text, txtShLen.Text, txtOrderCts.Text, IIf(chkGroove.Checked = True, 1, 0),
                                        cmbGroove.Text, rsComSql.Fields("LotName").Value, rsComSql.Fields("Price").Value, cmbType.Text, txtRemarks.Text)

                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing

            cmbRef.Focus()
        End If
    End Sub

    Private Sub SaveOrder()
        Dim intRow As Integer
        Dim dblCutRate As Double

        dblCutRate = 0

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

        If cmbLocation.Text = "" Then
            MsgBox("Please enter the Location", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If cmbDept.Text = "" Then
            MsgBox("Please enter the Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtSalesRate.Text = "" Then
            MsgBox("Please enter the Sales Rate", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        For intRow = 0 To flxDetails.Rows.Count - 1
            If Trim(flxDetails.Item(0, intRow).Value) = "" Then
                MsgBox("Line No. cannot be blank", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            'rsComSql = New ADODB.Recordset
            'rsComSql.Open("SELECT * FROM tblKITOrdersDtls WHERE NLineNo = '" & Trim(flxDetails.Item(0, intRow).Value) & "' AND OrderNo <> " & CInt(txtOrdNo.Text) & "", AdoCN, 1, 1)
            'If rsComSql.RecordCount Then
            '    MsgBox("Order No. " & rsComSql.Fields("OrderNo").Value & vbCrLf & "Line No. " & Trim(flxDetails.Item(0, intRow).Value) & " already exists", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '    Exit Sub
            'End If
            'rsComSql = Nothing

            'rsComSql = New ADODB.Recordset
            'rsComSql.Open("SELECT * FROM tblKITOrdersDtls WHERE NLineNo = '" & Trim(flxDetails.Item(0, intRow).Value) & "' AND OrderNo <> " & CInt(txtOrdNo.Text) & "", AdoCN, 1, 1)
            'If rsComSql.RecordCount Then
            '    MsgBox("Order No. " & rsComSql.Fields("OrderNo").Value & vbCrLf & "Line No. " & Trim(flxDetails.Item(0, intRow).Value) & " already exists", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '    Exit Sub
            'End If
            'rsComSql = Nothing

            If Not IsNumeric(flxDetails.Item(12, intRow).Value) = True Then
                MsgBox("Invalid Sets " & "Ref No. " & Trim(flxDetails.Item(1, intRow).Value) & " Side " & Trim(flxDetails.Item(2, intRow).Value), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Not IsNumeric(flxDetails.Item(13, intRow).Value) = True Then
                MsgBox("Invalid Pcs " & "Ref No. " & Trim(flxDetails.Item(1, intRow).Value) & " Side " & Trim(flxDetails.Item(2, intRow).Value), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Not IsNumeric(flxDetails.Item(19, intRow).Value) = True Then
                MsgBox("Invalid Order Cts " & "Ref No. " & Trim(flxDetails.Item(1, intRow).Value) & " Side " & Trim(flxDetails.Item(2, intRow).Value), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            'For intGrid = intRow + 1 To flxDetails.Rows.Count - 1
            '    If Trim(flxDetails.Item(0, intRow).Value) = Trim(flxDetails.Item(0, intGrid).Value) Then
            '        MsgBox("Line No. duplicated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '        Exit Sub
            '    End If
            'Next
        Next

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblKITOrders WHERE OrderNo = " & CInt(txtOrdNo.Text) & "", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                mStrSQL = "INSERT INTO tblKITOrders(OrderNo,Customer,DueDate,Subject,Niruref,DCLref,Toler,OrdDate,Complete,Dept,NorderNo,OrderItem,COMMANDE,Special,Color,Clarity,Country,Type,Assortment,ItemName,SalesRate) " & _
                          "VALUES(" & CInt(txtOrdNo.Text) & ",'" & cmbSupp.Text & "','" & Format(dtpDueDate.Value, "MM/dd/yyyy") & "','" & Trim(txtDesc.Text) & "','" & cmbClient.Text & "','0'," & _
                            "'" & cmbTol.Text & "','" & Format(dtpOrdDate.Value, "MM/dd/yyyy") & "','N','" & cmbDept.Text & "','" & txtNiruOrdNo.Text & "'," & CDbl(txtOrderItem.Text) & "," & _
                            "'" & txtCommande.Text & "'," & IIf(chkSpecial.Checked = True, 1, 0) & ",'" & cmbColor.Text & "','" & cmbClarity.Text & "','" & cmbCountry.Text & "','" & cmbLocation.Text & "'," & _
                            "'" & UCase(Trim(txtAssort.Text)) & "','" & UCase(Trim(txtItemName.Text)) & "','" & CDbl(txtSalesRate.Text) & "')"

                AdoCN.Execute(mStrSQL)

                For intRow = 0 To flxDetails.Rows.Count - 1
                    dblCutRate = 0
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT InvRate FROM tblCuttingCharges WHERE RateCode = '" & Trim(flxDetails.Item(11, intRow).Value) & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        dblCutRate = rsComSql.Fields("InvRate").Value
                    End If
                    rsComSql = Nothing

                    mStrSQL = "INSERT INTO tblKITOrdersDtls(OrderNo,NLineNo,RefNo,Side,Length,Width,Bothigh,Ronhigh,CulDirect,IncenCat,Flow,Descrip,Sets,PCs,Cts,RefComp,CutChg,vCutChg,LenTol,WidTol,HiTol,ShortLen,OrderCts,Groove,GrCount,Assortment,OutPrice,MaxType,Remarks) " & _
                              "VALUES(" & CInt(txtOrdNo.Text) & ",'" & Trim(flxDetails.Item(0, intRow).Value) & "','" & Trim(flxDetails.Item(1, intRow).Value) & "','" & Trim(flxDetails.Item(2, intRow).Value) & "'," & _
                                "'" & Trim(flxDetails.Item(3, intRow).Value) & "','" & Trim(flxDetails.Item(4, intRow).Value) & "','" & Trim(flxDetails.Item(5, intRow).Value) & "','" & Trim(flxDetails.Item(6, intRow).Value) & "'," & _
                                "'" & Trim(flxDetails.Item(8, intRow).Value) & "','" & Trim(flxDetails.Item(9, intRow).Value) & "','" & Trim(flxDetails.Item(10, intRow).Value) & "','" & Trim(flxDetails.Item(7, intRow).Value) & "'," & CInt(flxDetails.Item(12, intRow).Value) & "," & _
                                "" & CInt(flxDetails.Item(13, intRow).Value) & "," & CDbl(flxDetails.Item(14, intRow).Value) & ",'N','" & Trim(flxDetails.Item(11, intRow).Value) & "'," & dblCutRate & ",'" & Trim(flxDetails.Item(15, intRow).Value) & "','" & Trim(flxDetails.Item(16, intRow).Value) & "','" & Trim(flxDetails.Item(17, intRow).Value) & "'," & _
                                "'" & Trim(flxDetails.Item(18, intRow).Value) & "'," & CDbl(flxDetails.Item(19, intRow).Value) & "," & CInt(flxDetails.Item(20, intRow).Value) & "," & CInt(flxDetails.Item(21, intRow).Value) & ",'" & flxDetails.Item(22, intRow).Value & "'," & CDbl(flxDetails.Item(23, intRow).Value) & "," & _
                                "'" & flxDetails.Item(24, intRow).Value & "','" & flxDetails.Item(25, intRow).Value & "')"

                    AdoCN.Execute(mStrSQL)
                Next

                MsgBox("Order Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            PBResponse = MsgBox("Already Exists. Are you sure to Update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                mStrSQL = "UPDATE tblKITOrders SET Customer = '" & cmbSupp.Text & "',DueDate = '" & Format(dtpDueDate.Value, "MM/dd/yyyy") & "',Subject = '" & Trim(txtDesc.Text) & "',Niruref = '" & cmbClient.Text & "',Toler = '" & cmbTol.Text & "',OrdDate = '" & Format(dtpOrdDate.Value, "MM/dd/yyyy") & "'," & _
                            "Dept = '" & cmbDept.Text & "',NorderNo = '" & txtNiruOrdNo.Text & "',OrderItem = " & CDbl(txtOrderItem.Text) & ",COMMANDE = '" & txtCommande.Text & "',Special = " & IIf(chkSpecial.Checked = True, 1, 0) & ",Color = '" & cmbColor.Text & "',Clarity = '" & cmbClarity.Text & "',Country = '" & cmbCountry.Text & "'," & _
                            "Type = '" & cmbLocation.Text & "',Assortment = '" & UCase(Trim(txtAssort.Text)) & "',ItemName = '" & UCase(Trim(txtItemName.Text)) & "', SalesRate = '" & CDbl(txtSalesRate.Text) & "' " & _
                          "WHERE OrderNo = " & CInt(txtOrdNo.Text) & ""

                AdoCN.Execute(mStrSQL)

                mStrSQL = "DELETE FROM tblKITOrdersDtls WHERE OrderNo = " & CInt(txtOrdNo.Text) & ""

                AdoCN.Execute(mStrSQL)

                For intRow = 0 To flxDetails.Rows.Count - 1
                    dblCutRate = 0
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT InvRate FROM tblCuttingCharges WHERE RateCode = '" & Trim(flxDetails.Item(11, intRow).Value) & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        dblCutRate = rsComSql.Fields("InvRate").Value
                    End If
                    rsComSql = Nothing

                    mStrSQL = "INSERT INTO tblKITOrdersDtls(OrderNo,NLineNo,RefNo,Side,Length,Width,Bothigh,Ronhigh,CulDirect,IncenCat,Flow,Descrip,Sets,PCs,Cts,RefComp,CutChg,vCutChg,LenTol,WidTol,HiTol,ShortLen,OrderCts,Groove,GrCount,Assortment,OutPrice,MaxType,Remarks) " & _
                              "VALUES(" & CInt(txtOrdNo.Text) & ",'" & Trim(flxDetails.Item(0, intRow).Value) & "','" & Trim(flxDetails.Item(1, intRow).Value) & "','" & Trim(flxDetails.Item(2, intRow).Value) & "'," & _
                                "'" & Trim(flxDetails.Item(3, intRow).Value) & "','" & Trim(flxDetails.Item(4, intRow).Value) & "','" & Trim(flxDetails.Item(5, intRow).Value) & "','" & Trim(flxDetails.Item(6, intRow).Value) & "'," & _
                                "'" & Trim(flxDetails.Item(8, intRow).Value) & "','" & Trim(flxDetails.Item(9, intRow).Value) & "','" & Trim(flxDetails.Item(10, intRow).Value) & "','" & Trim(flxDetails.Item(7, intRow).Value) & "'," & CInt(flxDetails.Item(12, intRow).Value) & "," & _
                                "" & CInt(flxDetails.Item(13, intRow).Value) & "," & CDbl(flxDetails.Item(14, intRow).Value) & ",'N','" & Trim(flxDetails.Item(11, intRow).Value) & "'," & dblCutRate & ",'" & Trim(flxDetails.Item(15, intRow).Value) & "','" & Trim(flxDetails.Item(16, intRow).Value) & "','" & Trim(flxDetails.Item(17, intRow).Value) & "'," & _
                                "'" & flxDetails.Item(18, intRow).Value & "','" & CDbl(flxDetails.Item(19, intRow).Value) & "'," & CInt(flxDetails.Item(20, intRow).Value) & "," & CInt(flxDetails.Item(21, intRow).Value) & ",'" & flxDetails.Item(22, intRow).Value & "'," & CDbl(flxDetails.Item(23, intRow).Value) & "," & _
                                "'" & flxDetails.Item(24, intRow).Value & "','" & flxDetails.Item(25, intRow).Value & "')"

                    AdoCN.Execute(mStrSQL)
                Next

                MsgBox("Order Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        SaveOrder()
    End Sub

    Private Sub txtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtSets.Text <> "" And txtPcs.Text <> "" Then
                txtTotPcs.Text = CDbl(txtSets.Text) * CDbl(txtPcs.Text)
            End If
            txtOrderCts.Focus()
        End If
    End Sub

    Private Sub txtLen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLen.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtWid.Focus()
        End If
    End Sub

    Private Sub txtWid_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWid.KeyPress
        If Asc(e.KeyChar) = 13 Then
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
            txtShLen.Focus()
        End If
    End Sub

    Private Sub txtShLen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtShLen.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbDec.Focus()
        End If
    End Sub

    Private Sub cmbDec_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbDec.KeyPress
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

    Private Sub txtOrderCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrderCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtOrderCts.Text)
        If Asc(e.KeyChar) = 13 Then
            cmbGroove.Focus()
        End If
    End Sub

    Private Sub cmbGroove_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbGroove.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If cmbGroove.Text = "" Then
                cmbGroove.Text = "0"
            End If
            cmdAdd.Focus()
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
            txtDesc.Focus()
        End If
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        txtLineNo.Text = flxDetails.Item(0, flxDetails.CurrentRow.Index).Value
        cmbRef.Text = flxDetails.Item(1, flxDetails.CurrentRow.Index).Value
        cmbSide.Text = flxDetails.Item(2, flxDetails.CurrentRow.Index).Value
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
        txtOrderCts.Text = flxDetails.Item(14, flxDetails.CurrentRow.Index).Value
        txtTLen.Text = flxDetails.Item(15, flxDetails.CurrentRow.Index).Value
        txtTWit.Text = flxDetails.Item(16, flxDetails.CurrentRow.Index).Value
        txtTHgt.Text = flxDetails.Item(17, flxDetails.CurrentRow.Index).Value
        txtShLen.Text = flxDetails.Item(18, flxDetails.CurrentRow.Index).Value
        txtOrderCts.Text = flxDetails.Item(19, flxDetails.CurrentRow.Index).Value
        chkGroove.Checked = IIf(flxDetails.Item(20, flxDetails.CurrentRow.Index).Value = "1", True, False)
        cmbGroove.Text = flxDetails.Item(21, flxDetails.CurrentRow.Index).Value
        txtAssort2.Text = flxDetails.Item(22, flxDetails.CurrentRow.Index).Value
        txtOutPrice.Text = flxDetails.Item(23, flxDetails.CurrentRow.Index).Value
        cmbType.Text = flxDetails.Item(24, flxDetails.CurrentRow.Index).Value
        txtRemarks.Text = flxDetails.Item(25, flxDetails.CurrentRow.Index).Value
    End Sub

    Private Sub txtSalesRate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSalesRate.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtSalesRate.Text)
    End Sub

    Private Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub txtOutPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOutPrice.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtOutPrice.Text)
        If Asc(e.KeyChar) = 13 Then
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub txtAssort2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAssort2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbType.Focus()
        End If
    End Sub

    Private Sub cmbType_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbType.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtOutPrice.Focus()
        End If
    End Sub

    Private Sub frm_KITOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
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
        Load_Country()
        Load_Location()
        Load_Dept()
        Load_Ref()

        ClearText()
        GetNewOrderNo()
    End Sub

    Private Sub cmbRef_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRef.SelectedIndexChanged
        cmbSide.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT SUM(KitPcs) AS KitPcs FROM tblKITRef WHERE KitRef = '" & cmbRef.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            txtPcs.Text = rsComSql.Fields("KitPcs").Value
        End If
        rsComSql = Nothing

        cmbSide.Focus()
    End Sub

    Private Sub cmbSide_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSide.SelectedIndexChanged
        'rsComSql = New ADODB.Recordset
        'rsComSql.Open("SELECT * FROM tblKITRef WHERE KitRef = '" & cmbRef.Text & "' AND DiamondRef = '" & cmbSide.Text & "'", AdoCN, 1, 1)
        'If rsComSql.RecordCount Then
        '    txtPcs.Text = rsComSql.Fields("KitPcs").Value
        '    txtAssort2.Text = rsComSql.Fields("LotName").Value
        '    txtOutPrice.Text = rsComSql.Fields("Price").Value
        'End If
        'rsComSql = Nothing
    End Sub
End Class