
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_MixOrderSolver
    Dim blnStop As Boolean
    Dim FirstInput As Date

    Private Sub Load_OrderDetails()
        Dim intTotPcs As Integer
        Dim intIssPcs As Integer
        Dim intRejPcs As Integer
        Dim intLostPcs As Integer
        Dim intBalPcs As Integer
        Dim dblOrdPcs As Double
        Dim dblOrdRghPcs As Double
        Dim dblRghPcs As Double
        'Dim dblRghPerc As Double
        Dim intApproval As Integer
        Dim dblCurCost As Double
        Dim strShipDate As String
        Dim intCounter As Integer

        For intRow = 0 To flxOrder.Rows.Count - 1
            If Trim(txtOrder.Text) = flxOrder.Item(10, intRow).Value Then
                MsgBox("Order No. already entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        ExpProgress.Minimum = 0
        ExpProgress.Visible = True
        intCounter = 0

        chkNormal.Checked = True
        flxAssort.Rows.Clear()
        'flxOrder.Rows.Clear()
        txtCurCost.Text = ""
        intBalPcs = 0
        intApproval = 0
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM dbo.tblOrders WHERE OrderNo = " & Val(txtOrder.Text) & "", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            txtSubject.Text = rsComSql_1.Fields("Subject").Value & " " & rsComSql_1.Fields("Subject2").Value
            txtClient.Text = rsComSql_1.Fields("Niruref").Value
            txtNiruOrdNo.Text = rsComSql_1.Fields("NorderNo").Value
            txtItemNo.Text = rsComSql_1.Fields("OrderItem").Value
            txtDueDate.Text = Format(rsComSql_1.Fields("DueDate").Value, "yyyy/MM/dd")
            intApproval = rsComSql_1.Fields("Approval").Value
        Else
            MsgBox("Invalid Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql_1 = Nothing

        strShipDate = ""
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MIN(OrderDate) AS OrderDate FROM dbo.tblPlaneOrders WHERE (OrderNo = '" & txtOrder.Text & "')", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("OrderDate").Value) Then
                strShipDate = Format(rsComSql.Fields("OrderDate").Value, "yyyy-MM-dd")
            End If
        End If
        rsComSql = Nothing

        If intApproval = 1 Then
            lblRough.Visible = True
        Else
            lblRough.Visible = False
        End If

        dblOrdPcs = 0
        dblRghPcs = 0
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT SUM(Pcs * Sets) AS TotPcs FROM dbo.tblOrdersDtls WHERE (OrderNo = '" & txtOrder.Text & "')", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("TotPcs").Value) Then
                dblOrdPcs = rsComSql.Fields("TotPcs").Value
            End If
        End If
        rsComSql = Nothing

        dblOrdRghPcs = dblOrdPcs * 0.3

        'rsComSql = New ADODB.Recordset
        'rsComSql.Open("SELECT SUM(PktPcs) AS TotPcs FROM tblMIXPacket WHERE (PktOrdNo = '" & txtOrder.Text & "') AND (LEFT(AssortNo, 3) = 'ARW' OR LEFT(AssortNo, 3) = 'SRW' OR LEFT(AssortNo, 3) = 'SSW')", AdoCN, 1, 1)
        'If rsComSql.RecordCount Then
        '    If Not IsDBNull(rsComSql.Fields("TotPcs").Value) Then
        '        dblRghPcs = rsComSql.Fields("TotPcs").Value
        '    End If
        'End If
        'rsComSql = Nothing

        'rsComSql = New ADODB.Recordset
        'rsComSql.Open("SELECT SUM(dbo.tblMixReturns.RejPcs + dbo.tblMixReturns.LostPcs) AS RejPcs " & _
        '              "FROM dbo.tblMixPacket INNER JOIN dbo.tblMixReturns ON dbo.tblMixPacket.PktOrdNo = dbo.tblMixReturns.ParNo AND dbo.tblMixPacket.PktNo = dbo.tblMixReturns.PktNo " & _
        '              "WHERE (dbo.tblMixPacket.PktOrdNo = '" & txtOrder.Text & "') AND (LEFT(dbo.tblMixPacket.AssortNo, 3) = 'ARW' OR LEFT(dbo.tblMixPacket.AssortNo, 3) = 'SRW' OR LEFT(dbo.tblMixPacket.AssortNo, 3) = 'SSW')", AdoCN, 1, 1)
        'If rsComSql.RecordCount Then
        '    If Not IsDBNull(rsComSql.Fields("RejPcs").Value) Then
        '        dblRghPcs = dblRghPcs - rsComSql.Fields("RejPcs").Value
        '    End If
        'End If
        'rsComSql = Nothing

        'txtRghPcs.Text = Math.Round(dblOrdRghPcs - dblRghPcs, 0)
        'dblRghPerc = (dblRghPcs / dblOrdPcs) * 100
        'dblRghPerc = Math.Round(dblRghPerc, 2)
        'txtRghPerc.Text = dblRghPerc & "%"

        intTotPcs = 0
        rsComSql = New ADODB.Recordset
        'rsComSql.Open("SELECT * FROM tblOrdersDtls WHERE OrderNo = " & Val(txtOrder.Text) & " ORDER BY RefNo", AdoCN, 1, 1)
        rsComSql.Open("SELECT * FROM dbo.VW_MixOrderDetailsSolver WHERE OrderNo = " & Val(txtOrder.Text) & " ORDER BY RefNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            ExpProgress.Maximum = rsComSql.RecordCount
            While Not rsComSql.EOF
                intCounter = intCounter + 1
                'intTotPcs = rsComSql.Fields("Pcs").Value * rsComSql.Fields("Sets").Value
                intTotPcs = rsComSql.Fields("Pcs").Value

                'intIssPcs = 0
                'intRejPcs = 0
                'intLostPcs = 0
                'rsComSql_1 = New ADODB.Recordset
                'rsComSql_1.Open("SELECT * FROM VW_MixSolver WHERE PktOrdNo = '" & txtOrder.Text & "' AND PktRefNo = '" & Replace(rsComSql.Fields("RefNo").Value, "'", "''") & "' AND Pktside = '" & rsComSql.Fields("Side").Value & "'", AdoCN, 1, 1)
                'If rsComSql_1.RecordCount Then
                '    If Not IsDBNull(rsComSql_1.Fields("PktPcs").Value) Then
                '        intIssPcs = intIssPcs + rsComSql_1.Fields("PktPcs").Value
                '        intRejPcs = CInt(rsComSql_1.Fields("RejPcs").Value)
                '        intLostPcs = CInt(rsComSql_1.Fields("LostPcs").Value)
                '    End If
                'End If
                'rsComSql_1 = Nothing

                intIssPcs = rsComSql.Fields("PktPcs").Value
                intRejPcs = CInt(rsComSql.Fields("RejPcs").Value)
                intLostPcs = CInt(rsComSql.Fields("LostPcs").Value)

                intIssPcs = intIssPcs - (intRejPcs + intLostPcs)

                'dblCurCost = 0
                'rsComSql_1 = New ADODB.Recordset
                'rsComSql_1.Open("SELECT SUM(dbo.tblMixPacket.PktCts * dbo.tblAssortList.MarketPrice) AS PktValue " & _
                '                "FROM dbo.tblMixPacket INNER JOIN dbo.tblAssortList ON dbo.tblMixPacket.AssortNo = dbo.tblAssortList.Assortment " & _
                '                "WHERE (dbo.tblMixPacket.PktOrdNo = '" & txtOrder.Text & "') AND (dbo.tblMixPacket.PktRefNo = '" & rsComSql.Fields("RefNo").Value & "') AND (dbo.tblMixPacket.Pktside = '" & rsComSql.Fields("Side").Value & "')", AdoCN, 1, 1)
                'If rsComSql_1.RecordCount Then
                '    If Not IsDBNull(rsComSql_1.Fields("PktValue").Value) Then
                '        dblCurCost = rsComSql_1.Fields("PktValue").Value
                '    End If
                'End If
                'rsComSql_1 = Nothing

                'rsComSql_1 = New ADODB.Recordset
                'rsComSql_1.Open("SELECT SUM(dbo.tblMixReturns.RejCts * dbo.tblAssortList.MarketPrice) AS RejValue " & _
                '                "FROM dbo.tblMixPacket INNER JOIN dbo.tblAssortList ON dbo.tblMixPacket.AssortNo = dbo.tblAssortList.Assortment INNER JOIN " & _
                '                    "dbo.tblMixReturns ON dbo.tblMixPacket.PktOrdNo = dbo.tblMixReturns.ParNo AND dbo.tblMixPacket.PktNo = dbo.tblMixReturns.PktNo " & _
                '                "WHERE (dbo.tblMixPacket.PktOrdNo = '" & txtOrder.Text & "') AND (dbo.tblMixPacket.PktRefNo = '" & rsComSql.Fields("RefNo").Value & "') AND (dbo.tblMixPacket.Pktside = '" & rsComSql.Fields("Side").Value & "')", AdoCN, 1, 1)
                'If rsComSql_1.RecordCount Then
                '    If Not IsDBNull(rsComSql_1.Fields("RejValue").Value) Then
                '        dblCurCost = dblCurCost - rsComSql_1.Fields("RejValue").Value
                '    End If
                'End If
                'rsComSql_1 = Nothing

                dblCurCost = Math.Round(dblCurCost, 2)

                If (intTotPcs - intIssPcs) > 0 Then
                    flxOrder.Rows.Add(rsComSql.Fields("RefNo").Value,
                                      rsComSql.Fields("Side").Value,
                                      rsComSql.Fields("Length").Value,
                                      rsComSql.Fields("Width").Value,
                                      rsComSql.Fields("Bothigh").Value,
                                      intTotPcs - intIssPcs,
                                      Format((rsComSql.Fields("Bothigh").Value / rsComSql.Fields("Width").Value) * 100, "#0.00"),
                                      IIf(rsComSql.Fields("Groove").Value = 1, "GRV", ""),
                                      IIf(rsComSql.Fields("Laser").Value > 0, "LZ", ""),
                                      rsComSql.Fields("MaxCost").Value & "/" & rsComSql.Fields("MaxType").Value,
                                      rsComSql.Fields("OrderNo").Value,
                                      "C" & strRight(txtClient.Text, 3),
                                      txtSubject.Text,
                                      txtDueDate.Text,
                                      strShipDate,
                                      rsComSql.Fields("MaxType").Value)

                    intBalPcs = intBalPcs + (intTotPcs - intIssPcs)
                End If

                rsComSql.MoveNext()
                ExpProgress.Value = intCounter
                'Application.DoEvents()
            End While
        Else
            flxAssort.Rows.Clear()
            txtSubject.Text = ""
            txtClient.Text = ""
            txtBalPcs.Text = ""
            txtTotPcs.Text = ""
            txtSelPcs.Text = ""
            lblRough.Visible = False
        End If
        rsComSql = Nothing
        txtBalPcs.Text = intBalPcs
        ExpProgress.Visible = False

        If chkAll.Checked = True Then
            Load_Details()
        End If
    End Sub

    Private Sub txtOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrder.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            Load_OrderDetails()
        End If
    End Sub

    Private Sub Load_Details()
        Dim intRow As Integer

        txtTotPcs.Text = ""
        txtSelPcs.Text = ""
        txtCurCost.Text = ""
        'ExpProgress.Visible = True
        'ExpProgress.Maximum = flxOrder.Rows.Count
        'ExpProgress.Value = 0

        For intRow = 0 To flxOrder.Rows.Count - 1
            FillAssorts(Val(flxOrder.Item(2, intRow).Value), Val(flxOrder.Item(3, intRow).Value), _
                        Trim(flxOrder.Item(0, intRow).Value), Trim(flxOrder.Item(1, intRow).Value), _
                        Val(flxOrder.Item(4, intRow).Value), Val(flxOrder.Item(5, intRow).Value), _
                        Val(flxOrder.Item(9, intRow).Value), Trim(flxOrder.Item(10, intRow).Value))

            'ExpProgress.Value = ExpProgress.Value + 1
            'Application.DoEvents()
        Next
        txtTotPcs.Text = CalTotalPcs(flxAssort)
        'ExpProgress.Visible = False
    End Sub

    Private Sub Load_Details2()
        Dim intRow As Integer

        txtTotPcs.Text = ""
        txtSelPcs.Text = ""
        txtCurCost.Text = ""
        'ExpProgress.Visible = True
        'ExpProgress.Maximum = flxOrder.Rows.Count
        'ExpProgress.Value = 0
        AdoCN.Execute("DELETE FROM tblTempOrderPcsNew")
        flxAssort.Rows.Clear()
        For intRow = 0 To flxOrder.Rows.Count - 1
            FillAssorts2(CDbl(flxOrder.Item(2, intRow).Value), CDbl(flxOrder.Item(3, intRow).Value), _
                         CDbl(flxOrder.Item(5, intRow).Value), CDbl(flxOrder.Item(9, intRow).Value), _
                         flxOrder.Item(0, intRow).Value)

            'ExpProgress.Value = ExpProgress.Value + 1
            'Application.DoEvents()
        Next
        txtTotPcs.Text = CalTotalPcs(flxAssort)
        'ExpProgress.Visible = False
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub FillAssorts(ByVal dblLen As Double, ByVal dblWid As Double, _
                            ByVal strRef As String, ByVal strSide As String, _
                            ByVal dblHeight As Double, ByVal dblBalPcs As Double, _
                            ByVal dblPricePerStone As Double, ByVal strOrderNo As String)

        Dim rsAssort As ADODB.Recordset
        Dim intRow As Integer
        Dim strAssortment As String
        Dim dblLengthFrom As Double
        Dim dblLengthTo As Double
        Dim dblWidthFrom As Double
        Dim dblWidthTo As Double
        Dim strSelectFrom As String
        Dim strWhere As String
        Dim strOrder As String

        Dim intBalPcs As Integer
        Dim dblBalCts As Double

        Dim intTotPcs As Integer
        Dim dblTotCts As Double
        Dim dblAvgCost As Double

        Dim dblTotCts2 As Double
        Dim dblTotVal As Double
        Dim dblTotVal2 As Double
        Dim dblTotValBag As Double

        Dim intIssPcs As Integer
        Dim dblIssCts As Double
        Dim dblAvgPrice As Double
        Dim dblBagPrice As Double
        Dim strBagAssortment As String

        Dim dblOpenPcs As Double
        Dim dblInPcs As Double
        Dim dblOutPcs As Double
        Dim dblRejPcs As Double
        Dim dtpFromDate As Date
        Dim dblTurnOver As Double

        Dim dblBoxPcs As Double
        Dim dblBoxCts As Double

        Dim dblCurCost As Double
        Dim dblCurPcs As Double

        Dim dblHighValue As Double
        Dim blnStockHave As Boolean
        Dim dblMaxStonePrice As Double

        dtpFromDate = DateAdd(DateInterval.Month, -1 * 6, Date.Now)

        strAssortment = ""
        intTotPcs = 0
        dblTotCts = 0
        dblAvgCost = 0
        dblTotVal = 0
        dblTotCts2 = 0
        dblTotVal2 = 0
        dblHighValue = 0
        dblMaxStonePrice = 0

        dblCurCost = 0
        dblCurPcs = 0
        'rsComSql_1 = New ADODB.Recordset
        'rsComSql_1.Open("SELECT SUM(dbo.tblMixPacket.PktPcs) AS PktPcs,SUM(dbo.tblMixPacket.PktCts * dbo.tblAssortList.AvgCost) AS PktValue " & _
        '                "FROM dbo.tblMixPacket INNER JOIN dbo.tblAssortList ON dbo.tblMixPacket.AssortNo = dbo.tblAssortList.Assortment " & _
        '                "WHERE (dbo.tblMixPacket.PktOrdNo = '" & strOrderNo & "') AND (dbo.tblMixPacket.PktRefNo = '" & Replace(strRef, "'", "''") & "') AND (dbo.tblMixPacket.Pktside = '" & strSide & "') ", AdoCN, 1, 1)
        'If rsComSql_1.RecordCount Then
        '    If Not IsDBNull(rsComSql_1.Fields("PktValue").Value) Then
        '        dblCurCost = rsComSql_1.Fields("PktValue").Value
        '        dblCurPcs = rsComSql_1.Fields("PktPcs").Value
        '    End If
        'End If
        'rsComSql_1 = Nothing

        'rsComSql_1 = New ADODB.Recordset
        'rsComSql_1.Open("SELECT SUM(dbo.tblMixPacket.PktPcs) AS PktPcs,SUM(dbo.tblMixPacket.PktPcs * dbo.tblAssortList.AvgStonePrice) AS PktValue " & _
        '                "FROM dbo.tblMixPacket INNER JOIN dbo.tblAssortList ON dbo.tblMixPacket.AssortNo = dbo.tblAssortList.Assortment " & _
        '                "WHERE (dbo.tblMixPacket.PktOrdNo = '" & strOrderNo & "') AND (dbo.tblMixPacket.PktRefNo = '" & Replace(strRef, "'", "''") & "') AND (dbo.tblMixPacket.Pktside = '" & strSide & "') AND (LEFT(dbo.tblMixPacket.AssortNo, 1) = 'S')", AdoCN, 1, 1)
        'If rsComSql_1.RecordCount Then
        '    If Not IsDBNull(rsComSql_1.Fields("PktValue").Value) Then
        '        dblCurCost = dblCurCost + rsComSql_1.Fields("PktValue").Value
        '        dblCurPcs = dblCurPcs + rsComSql_1.Fields("PktPcs").Value
        '    End If
        'End If
        'rsComSql_1 = Nothing

        'rsComSql_1 = New ADODB.Recordset
        'rsComSql_1.Open("SELECT SUM(dbo.tblMixReturns.RejPcs) AS RejPcs, SUM(dbo.tblMixReturns.RejCts * dbo.tblAssortList.AvgCost) AS RejValue " & _
        '                "FROM dbo.tblMixPacket INNER JOIN dbo.tblAssortList ON dbo.tblMixPacket.AssortNo = dbo.tblAssortList.Assortment INNER JOIN " & _
        '                    "dbo.tblMixReturns ON dbo.tblMixPacket.PktOrdNo = dbo.tblMixReturns.ParNo AND dbo.tblMixPacket.PktNo = dbo.tblMixReturns.PktNo " & _
        '                "WHERE (dbo.tblMixPacket.PktOrdNo = '" & strOrderNo & "') AND (dbo.tblMixPacket.PktRefNo = '" & Replace(strRef, "'", "''") & "') AND (dbo.tblMixPacket.Pktside = '" & strSide & "') ", AdoCN, 1, 1)
        'If rsComSql_1.RecordCount Then
        '    If Not IsDBNull(rsComSql_1.Fields("RejValue").Value) Then
        '        dblCurCost = dblCurCost - rsComSql_1.Fields("RejValue").Value
        '        dblCurPcs = dblCurPcs - rsComSql_1.Fields("RejPcs").Value
        '    End If
        'End If
        'rsComSql_1 = Nothing

        'rsComSql_1 = New ADODB.Recordset
        'rsComSql_1.Open("SELECT SUM(dbo.tblMixReturns.RejPcs) AS RejPcs, SUM(dbo.tblMixReturns.RejPcs * dbo.tblAssortList.AvgStonePrice) AS RejValue " & _
        '                "FROM dbo.tblMixPacket INNER JOIN dbo.tblAssortList ON dbo.tblMixPacket.AssortNo = dbo.tblAssortList.Assortment INNER JOIN " & _
        '                    "dbo.tblMixReturns ON dbo.tblMixPacket.PktOrdNo = dbo.tblMixReturns.ParNo AND dbo.tblMixPacket.PktNo = dbo.tblMixReturns.PktNo " & _
        '                "WHERE (dbo.tblMixPacket.PktOrdNo = '" & strOrderNo & "') AND (dbo.tblMixPacket.PktRefNo = '" & Replace(strRef, "'", "''") & "') AND (dbo.tblMixPacket.Pktside = '" & strSide & "') AND (LEFT(dbo.tblMixPacket.AssortNo, 1) = 'S')", AdoCN, 1, 1)
        'If rsComSql_1.RecordCount Then
        '    If Not IsDBNull(rsComSql_1.Fields("RejValue").Value) Then
        '        dblCurCost = dblCurCost - rsComSql_1.Fields("RejValue").Value
        '        dblCurPcs = dblCurPcs - rsComSql_1.Fields("RejPcs").Value
        '    End If
        'End If
        'rsComSql_1 = Nothing

        'If dblCurPcs > 0 Then
        '    dblCurCost = dblCurCost / dblCurPcs
        '    dblCurCost = dblCurCost + 13

        '    rsComSql_1 = New ADODB.Recordset
        '    rsComSql_1.Open("SELECT GrCount FROM dbo.tblOrdersDtls WHERE OrderNo = '" & strOrderNo & "' AND RefNo = '" & Replace(strRef, "'", "''") & "' AND Side = '" & strSide & "' AND Groove = 1", AdoCN, 1, 1)
        '    If rsComSql_1.RecordCount Then
        '        dblCurCost = dblCurCost + (rsComSql_1.Fields("GrCount").Value * 5)
        '    End If
        '    rsComSql_1 = Nothing

        'End If
        'dblCurCost = Math.Round(dblCurCost, 2)
        'txtCurCost.Text = dblCurCost

        'Application.DoEvents()

        blnStockHave = False
        rsAssort = New ADODB.Recordset
        If cmbCat.Text <> "" Then
            rsAssort.Open("SELECT * FROM dbo.VW_AssortCodesAll WHERE AssortCode = '" & cmbCat.Text & "' ORDER BY Seq", AdoCN, 1, 1)
        Else
            rsAssort.Open("SELECT * FROM dbo.VW_AssortCodesAll ORDER BY Seq", AdoCN, 1, 1)
        End If
        If rsAssort.RecordCount Then
            rsAssort.MoveFirst()
            While Not rsAssort.EOF
                If blnStop = True Then Exit Sub
                'blnStockHave = False
                'rsComSql = New ADODB.Recordset
                'If Len(rsAssort.Fields("AssortCode").Value) = 3 Then
                '    rsComSql.Open("SELECT SUM(Pcs) AS Pcs FROM dbo.VW_MixAssortInOutNew2020 WHERE (LEFT(Assortment, 3) = '" & rsAssort.Fields("AssortCode").Value & "') HAVING (SUM(Pcs) > 0)", AdoCN, 1, 1)
                'Else
                '    rsComSql.Open("SELECT SUM(Pcs) AS Pcs FROM dbo.VW_MixAssortInOutNew2020 WHERE (LEFT(Assortment, 5) = '" & rsAssort.Fields("AssortCode").Value & "') HAVING (SUM(Pcs) > 0)", AdoCN, 1, 1)
                'End If
                'If rsComSql.RecordCount Then
                '    If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                '        If rsComSql.Fields("Pcs").Value > 0 Then
                '            blnStockHave = True
                '        End If
                '    End If
                'End If
                'rsComSql = Nothing

                'If blnStockHave = False Then
                '    GoTo NextAssortCode
                'End If

                If chkAdvance.Checked = True Then
                    dblLengthFrom = dblLen + rsAssort.Fields("LenMin").Value
                    dblWidthFrom = dblWid + rsAssort.Fields("WidMin").Value

                    dblLengthTo = dblLen + rsAssort.Fields("LenMax").Value + 0.1
                    dblWidthTo = dblWid + rsAssort.Fields("WidMax").Value + 0.1

                ElseIf chkNormal.Checked = True Then
                    dblLengthFrom = dblLen + rsAssort.Fields("LenMin").Value
                    dblWidthFrom = dblWid + rsAssort.Fields("WidMin").Value

                    dblLengthTo = dblLen + rsAssort.Fields("LenMax").Value
                    dblWidthTo = dblWid + rsAssort.Fields("WidMax").Value

                ElseIf chkExtra.Checked = True Then
                    dblLengthFrom = dblLen + rsAssort.Fields("LenMin").Value
                    dblWidthFrom = dblWid + rsAssort.Fields("WidMin").Value

                    dblLengthTo = dblLen + rsAssort.Fields("LenMax").Value + 0.2
                    dblWidthTo = dblWid + rsAssort.Fields("WidMax").Value + 0.2

                ElseIf chkExtra2.Checked = True Then
                    dblLengthFrom = dblLen + rsAssort.Fields("LenMin").Value
                    dblWidthFrom = dblWid + rsAssort.Fields("WidMin").Value

                    dblLengthTo = dblLen + rsAssort.Fields("LenMax").Value + 0.3
                    dblWidthTo = dblWid + rsAssort.Fields("WidMax").Value + 0.3
                End If

                If chk10.Checked = True Then
                    dblLengthFrom = dblLengthFrom - 0.1
                    dblWidthFrom = dblWidthFrom - 0.1
                End If
                If chk10P.Checked = True Then
                    dblLengthFrom = dblLengthFrom + 0.1
                    dblWidthFrom = dblWidthFrom + 0.1
                End If
                If chk20P.Checked = True Then
                    dblLengthFrom = dblLengthFrom + 0.2
                    dblWidthFrom = dblWidthFrom + 0.2
                End If
                If chk20M.Checked = True Then
                    dblLengthFrom = dblLengthFrom - 0.2
                    dblWidthFrom = dblWidthFrom - 0.2
                End If

                dblLengthFrom = Math.Round(dblLengthFrom, 2)
                dblWidthFrom = Math.Round(dblWidthFrom, 2)
                dblLengthTo = Math.Round(dblLengthTo, 2)
                dblWidthTo = Math.Round(dblWidthTo, 2)

                rsComSql = New ADODB.Recordset

                'If rsAssort.Fields("AssortCode").Value = "ABFCN" Then
                '    MsgBox(rsAssort.Fields("AssortCode").Value)
                'End If

                'strSelectFrom = "SELECT  TOP (100) PERCENT dbo.tblAssortList.Assortment, dbo.tblAssortList.Color, dbo.tblAssortList.Clarity, dbo.tblAssortList.LengthFrom, dbo.tblAssortList.LengthTo, dbo.tblAssortList.WidthFrom, " & _
                '                    "dbo.tblAssortList.WidthTo, dbo.tblAssortList.Make, dbo.tblAssortList.Flo, dbo.tblAssortList.AvWeight, dbo.tblAssortList.Shape, dbo.tblAssortList.CurrentCost, dbo.tblAssortList.MarketPrice, " & _
                '                    "dbo.tblAssortList.AvgCost, dbo.tblAssortList.StonePrice, dbo.tblAssortList.AvgStonePrice " & _
                '                "FROM dbo.tblAssortList INNER JOIN dbo.VW_MixAssortInOutNew ON dbo.tblAssortList.Assortment = dbo.VW_MixAssortInOutNew.Assortment "
                'If rsAssort.Fields("AssortCode").Value = "ABA" Then
                '    strWhere = " WHERE (dbo.tblAssortList.LengthFrom >= " & dblLengthFrom & " OR dbo.tblAssortList.LengthTo >= " & dblLengthFrom & ") AND (dbo.tblAssortList.WidthFrom >= " & dblWidthFrom & " OR " & _
                '                    "dbo.tblAssortList.WidthTo >= " & dblWidthFrom & ") AND (LEFT(dbo.tblAssortList.Assortment, 3) = '" & rsAssort.Fields("AssortCode").Value & "') AND (dbo.VW_MixAssortInOutNew.InPcs - dbo.VW_MixAssortInOutNew.OutPcs > 0) "
                'Else
                '    If Len(rsAssort.Fields("AssortCode").Value) = 3 Then
                '        strWhere = " WHERE (dbo.tblAssortList.LengthFrom >= " & dblLengthFrom & ") AND (dbo.tblAssortList.LengthFrom <= " & dblLengthTo & ") AND (dbo.tblAssortList.WidthFrom >= " & dblWidthFrom & ") AND " & _
                '                    "(dbo.tblAssortList.WidthFrom <= " & dblWidthTo & ") AND (LEFT(dbo.tblAssortList.Assortment, 3) = '" & rsAssort.Fields("AssortCode").Value & "') AND (dbo.VW_MixAssortInOutNew.InPcs - dbo.VW_MixAssortInOutNew.OutPcs > 0) "
                '    Else
                '        strWhere = " WHERE (dbo.tblAssortList.LengthFrom >= " & dblLengthFrom & ") AND (dbo.tblAssortList.LengthFrom <= " & dblLengthTo & ") AND (dbo.tblAssortList.WidthFrom >= " & dblWidthFrom & ") AND " & _
                '                    "(dbo.tblAssortList.WidthFrom <= " & dblWidthTo & ") AND (LEFT(dbo.tblAssortList.Assortment, 5) = '" & rsAssort.Fields("AssortCode").Value & "') AND (dbo.VW_MixAssortInOutNew.InPcs - dbo.VW_MixAssortInOutNew.OutPcs > 0) "
                '    End If
                'End If

                strSelectFrom = "SELECT  TOP (100) PERCENT dbo.tblAssortList.Assortment, dbo.tblAssortList.Color, dbo.tblAssortList.Clarity, dbo.tblAssortList.LengthFrom, dbo.tblAssortList.LengthTo, dbo.tblAssortList.WidthFrom, " & _
                                    "dbo.tblAssortList.WidthTo, dbo.tblAssortList.Make, dbo.tblAssortList.Flo, dbo.tblAssortList.AvWeight, dbo.tblAssortList.Shape, dbo.tblAssortList.CurrentCost, dbo.tblAssortList.MarketPrice, " & _
                                    "dbo.tblAssortList.AvgCost, dbo.tblAssortList.StonePrice, dbo.tblAssortList.AvgStonePrice, dbo.VW_MixAssortInOutNew2020.Pcs, dbo.VW_MixAssortInOutNew2020.Cts " & _
                                "FROM dbo.tblAssortList INNER JOIN dbo.VW_MixAssortInOutNew2020 ON dbo.tblAssortList.Assortment = dbo.VW_MixAssortInOutNew2020.Assortment "
                If rsAssort.Fields("AssortCode").Value = "ABA" Then
                    strWhere = " WHERE (dbo.tblAssortList.LengthFrom >= " & dblLengthFrom & " OR dbo.tblAssortList.LengthTo >= " & dblLengthFrom & ") AND (dbo.tblAssortList.WidthFrom >= " & dblWidthFrom & " OR " & _
                                    "dbo.tblAssortList.WidthTo >= " & dblWidthFrom & ") AND (LEFT(dbo.tblAssortList.Assortment, 3) = '" & rsAssort.Fields("AssortCode").Value & "') AND (dbo.VW_MixAssortInOutNew2020.Pcs > 0) "
                Else
                    If Len(rsAssort.Fields("AssortCode").Value) = 3 Then
                        strWhere = " WHERE (dbo.tblAssortList.LengthFrom >= " & dblLengthFrom & ") AND (dbo.tblAssortList.LengthFrom <= " & dblLengthTo & ") AND (dbo.tblAssortList.WidthFrom >= " & dblWidthFrom & ") AND " & _
                                    "(dbo.tblAssortList.WidthFrom <= " & dblWidthTo & ") AND (LEFT(dbo.tblAssortList.Assortment, 3) = '" & rsAssort.Fields("AssortCode").Value & "') AND (dbo.VW_MixAssortInOutNew2020.Pcs > 0) "

                    ElseIf Len(rsAssort.Fields("AssortCode").Value) = 4 Then
                        strWhere = " WHERE (dbo.tblAssortList.LengthFrom >= " & dblLengthFrom & ") AND (dbo.tblAssortList.LengthFrom <= " & dblLengthTo & ") AND (dbo.tblAssortList.WidthFrom >= " & dblWidthFrom & ") AND " & _
                                    "(dbo.tblAssortList.WidthFrom <= " & dblWidthTo & ") AND (SUBSTRING(dbo.tblAssortList.Assortment, 7, 4) = '" & rsAssort.Fields("AssortCode").Value & "') AND (dbo.VW_MixAssortInOutNew2020.Pcs > 0) "
                    Else
                        strWhere = " WHERE (dbo.tblAssortList.LengthFrom >= " & dblLengthFrom & ") AND (dbo.tblAssortList.LengthFrom <= " & dblLengthTo & ") AND (dbo.tblAssortList.WidthFrom >= " & dblWidthFrom & ") AND " & _
                                    "(dbo.tblAssortList.WidthFrom <= " & dblWidthTo & ") AND (LEFT(dbo.tblAssortList.Assortment, 5) = '" & rsAssort.Fields("AssortCode").Value & "') AND (dbo.VW_MixAssortInOutNew2020.Pcs > 0) "
                    End If
                End If

                If cmbColor.Text <> "" Then
                    If cmbColor.Text = "DF" Then
                        strWhere = strWhere & " AND (dbo.tblAssortList.Color = 'DE' OR dbo.tblAssortList.Color = 'F')"
                    Else
                        strWhere = strWhere & " AND (dbo.tblAssortList.Color = '" & cmbColor.Text & "')"
                    End If
                End If
                If cmbClarity.Text <> "" Then
                    strWhere = strWhere & " AND (dbo.tblAssortList.Clarity = '" & cmbClarity.Text & "')"
                End If
                If cmbFlo.Text <> "" Then
                    strWhere = strWhere & " AND (dbo.tblAssortList.Flo = '" & cmbFlo.Text & "')"
                End If
                If cmbType.Text <> "" Then
                    If cmbType.Text = "Rough" Then
                        strWhere = strWhere & " AND (dbo.tblAssortList.Assortment LIKE 'S%')"
                    ElseIf cmbType.Text = "Polished" Then
                        strWhere = strWhere & " AND (dbo.tblAssortList.Assortment NOT LIKE 'S%')"
                    Else
                        strWhere = strWhere & " AND (dbo.tblAssortList.Assortment LIKE 'A%')"
                    End If
                End If
                If cmbOrigin.Text <> "" Then
                    strWhere = strWhere & " AND (LEFT(dbo.tblAssortList.Assortment, 3) = '" & cmbOrigin.Text & "')"
                End If
                strOrder = " ORDER BY dbo.tblAssortList.Assortment"

                mStrSQL = strSelectFrom & strWhere & strOrder
                rsComSql.Open(mStrSQL, AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        intBalPcs = 0
                        dblBalCts = 0
                        dblAvgCost = 0
                        dblTotVal = 0
                        dblTotVal2 = 0
                        dblTotCts2 = 0
                        intIssPcs = 0
                        dblIssCts = 0
                        dblTotValBag = 0
                        dblBagPrice = 0
                        dblOpenPcs = 0
                        dblInPcs = 0
                        dblOutPcs = 0
                        dblRejPcs = 0
                        dblTurnOver = 0
                        dblAvgPrice = 0

                        dblBoxPcs = 0
                        dblBoxCts = 0
                        strBagAssortment = ""

                        strAssortment = rsComSql.Fields("Assortment").Value

                        'If strAssortment = "SRW3231C111" Then
                        '    MsgBox(strAssortment, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        'End If

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM dbo.tblAssortBlock WHERE Assortment = '" & strAssortment & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            GoTo NextRecord
                        End If
                        rsComSql_1 = Nothing

                        intBalPcs = 0
                        dblBalCts = 0
                        'rsComSql_1 = New ADODB.Recordset
                        'rsComSql_1.Open("SELECT SUM(InPcs) AS TotPcs,SUM(InCts) AS TotCts FROM dbo.tblAssortDetails WHERE Assortment = '" & strAssortment & "'", AdoCN, 1, 1)
                        'If Not IsDBNull(rsComSql_1.Fields("TotPcs").Value) Then
                        '    intBalPcs = rsComSql_1.Fields("TotPcs").Value
                        '    dblBalCts = rsComSql_1.Fields("TotCts").Value
                        '    dblBalCts = Math.Round(dblBalCts, 3)
                        'End If
                        'rsComSql_1 = Nothing

                        'rsComSql_1 = New ADODB.Recordset
                        'rsComSql_1.Open("SELECT SUM(Pcs) AS TotPcs,SUM(Cts) AS TotCts FROM dbo.tblMixPacketDetails WHERE Assortment = '" & strAssortment & "'", AdoCN, 1, 1)
                        'If Not IsDBNull(rsComSql_1.Fields("TotPcs").Value) Then
                        '    intBalPcs = intBalPcs - rsComSql_1.Fields("TotPcs").Value
                        '    dblBalCts = dblBalCts - rsComSql_1.Fields("TotCts").Value
                        '    dblBalCts = Math.Round(dblBalCts, 3)
                        'End If
                        'rsComSql_1 = Nothing

                        intBalPcs = rsComSql.Fields("Pcs").Value
                        dblBalCts = rsComSql.Fields("Cts").Value
                        dblBalCts = Math.Round(dblBalCts, 3)

                        'Employee Issues - 17/04/2020
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT SUM(BalPcs) AS TotPcs,SUM(BalCts) AS TotCts FROM dbo.VW_MixEmpBal WHERE Assortment = '" & strAssortment & "'", AdoCN, 1, 1)
                        If Not IsDBNull(rsComSql_1.Fields("TotPcs").Value) Then
                            intBalPcs = intBalPcs - rsComSql_1.Fields("TotPcs").Value
                            dblBalCts = dblBalCts - rsComSql_1.Fields("TotCts").Value
                            dblBalCts = Math.Round(dblBalCts, 3)
                        End If
                        rsComSql_1 = Nothing
                        '-----------------------------

                        intIssPcs = 0
                        dblIssCts = 0
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT SUM(Pcs) AS TotPcs,SUM(Cts) AS TotCts FROM dbo.tblMixIntIssues WHERE Assortment = '" & strAssortment & "' AND IssDate = '" & Format(Date.Now, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
                        If Not IsDBNull(rsComSql_1.Fields("TotPcs").Value) Then
                            intIssPcs = rsComSql_1.Fields("TotPcs").Value
                            dblIssCts = rsComSql_1.Fields("TotCts").Value
                            dblIssCts = Math.Round(dblIssCts, 3)
                        End If
                        rsComSql_1 = Nothing

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT SUM(dbo.tblMixPacketDetails.Pcs) AS TotPcs,ROUND(SUM(dbo.tblMixPacketDetails.Cts), 3) AS TotCts " & _
                                        "FROM dbo.tblMixPacket INNER JOIN dbo.tblMixPacketDetails ON dbo.tblMixPacket.PktOrdNo = dbo.tblMixPacketDetails.ParNo AND dbo.tblMixPacket.PktNo = dbo.tblMixPacketDetails.PktNo " & _
                                        "WHERE dbo.tblMixPacketDetails.Assortment = '" & strAssortment & "' AND dbo.tblMixPacket.PktIss = '" & Format(Date.Now, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
                        If Not IsDBNull(rsComSql_1.Fields("TotPcs").Value) Then
                            intIssPcs = intIssPcs - rsComSql_1.Fields("TotPcs").Value
                            dblIssCts = dblIssCts - rsComSql_1.Fields("TotCts").Value
                            dblIssCts = Math.Round(dblIssCts, 3)
                        End If
                        rsComSql_1 = Nothing

                        If intIssPcs < intBalPcs Then
                            dblAvgPrice = rsComSql.Fields("AvgCost").Value

                            'strBagAssortment = ""
                            'rsComSql_1 = New ADODB.Recordset
                            'rsComSql_1.Open("SELECT * " & _
                            '                "FROM dbo.tblAssortMatch " & _
                            '                "WHERE NewAssortment = '" & strAssortment & "'", AdoCN, 1, 1)
                            'If rsComSql_1.RecordCount Then
                            '    strBagAssortment = rsComSql_1.Fields("BagAssortment").Value
                            'End If
                            'rsComSql_1 = Nothing

                            'If strBagAssortment <> "" Then
                            '    rsComSql_2 = New ADODB.Recordset
                            '    rsComSql_2.Open("SELECT PRICE " & _
                            '                    "FROM dbo.tblGrading_SizingList " & _
                            '                    "WHERE (NAME = '" & strBagAssortment & "')", AdoCN, 1, 1)
                            '    If rsComSql_2.RecordCount Then
                            '        If Mid(strAssortment, 1, 2) = "AI" Then
                            '            dblBagPrice = rsComSql_2.Fields("Price").Value * 0.85
                            '        ElseIf Mid(strAssortment, 1, 2) = "AR" Then
                            '            dblBagPrice = rsComSql_2.Fields("Price").Value * 0.75
                            '        ElseIf Mid(strAssortment, 1, 2) = "AN" Then
                            '            dblBagPrice = rsComSql_2.Fields("Price").Value
                            '        End If
                            '    End If
                            '    rsComSql_2 = Nothing
                            '    dblBagPrice = Math.Round(dblBagPrice, 2)
                            'End If

                            '===============
                            dblAvgPrice = Math.Round(dblAvgPrice, 2)

                            dblBoxPcs = rsComSql.Fields("Pcs").Value
                            dblBoxCts = rsComSql.Fields("Cts").Value

                            If Mid(strAssortment, 1, 1) = "S" Then
                                'dblTotVal = dblTotVal + (dblBoxPcs * rsComSql.Fields("StonePrice").Value)
                                dblTotVal = dblTotVal + (dblBoxCts * dblAvgPrice)
                                dblTotVal2 = dblTotVal2 + (dblBoxCts * dblAvgPrice)
                            Else
                                dblTotVal = dblTotVal + (dblBoxCts * rsComSql.Fields("MarketPrice").Value)
                                'dblTotVal = dblTotVal + (dblBoxCts * dblAvgPrice)
                                dblTotVal2 = dblTotVal2 + (dblBoxCts * dblAvgPrice)
                            End If


                            dblTotCts2 = dblTotCts2 + dblBoxCts
                            dblTotValBag = dblTotValBag + (dblBoxCts * dblBagPrice)
                            '===================

                            'rsComSql_1 = New ADODB.Recordset
                            'rsComSql_1.Open("SELECT ROUND(SUM(InCts - OutCts), 3) AS BalCts, SUM(InPcs - OutPcs) AS BalPcs " & _
                            '                "FROM dbo.VW_MixAssortInOutNew " & _
                            '                "WHERE (Assortment = '" & strAssortment & "') AND (ROUND(InCts - OutCts, 2) > 0) ", AdoCN, 1, 1)
                            'If rsComSql_1.RecordCount Then
                            '    If Not IsDBNull(rsComSql_1.Fields("BalCts").Value) Then
                            '        rsComSql_1.MoveFirst()
                            '        While Not rsComSql_1.EOF
                            '            dblBoxPcs = rsComSql_1.Fields("BalPcs").Value
                            '            dblBoxCts = rsComSql_1.Fields("BalCts").Value
                            '            If Mid(strAssortment, 1, 1) = "S" Then
                            '                dblTotVal = dblTotVal + (rsComSql_1.Fields("BalPcs").Value * rsComSql.Fields("StonePrice").Value)
                            '                'dblTotVal2 = dblTotVal2 + (rsComSql_1.Fields("BalPcs").Value * rsComSql.Fields("AvgStonePrice").Value)
                            '                dblTotVal2 = dblTotVal2 + (rsComSql_1.Fields("BalCts").Value * dblAvgPrice)
                            '            Else
                            '                dblTotVal = dblTotVal + (rsComSql_1.Fields("BalCts").Value * rsComSql.Fields("MarketPrice").Value)
                            '                dblTotVal2 = dblTotVal2 + (rsComSql_1.Fields("BalCts").Value * dblAvgPrice)
                            '            End If
                            '            dblTotCts2 = dblTotCts2 + rsComSql_1.Fields("BalCts").Value
                            '            dblTotValBag = dblTotValBag + (rsComSql_1.Fields("BalCts").Value * dblBagPrice)
                            '            rsComSql_1.MoveNext()
                            '        End While
                            '    End If
                            'End If
                            'rsComSql_1 = Nothing

                            If dblTotCts2 <> 0 Then
                                dblAvgCost = dblTotVal2 / dblTotCts2
                                dblAvgCost = Math.Round(dblAvgCost, 1)
                            End If

                            If dblTotVal > dblTotVal2 Then
                                dblHighValue = dblTotVal
                            Else
                                dblHighValue = dblTotVal2
                            End If
                            dblHighValue = Math.Round(dblHighValue, 2)

                            'If strAssortment = "ABDVN-3437-1416" Then
                            '    MsgBox(strAssortment)
                            'End If

                            If chkMaxCost.Checked = True Or chkMax25.Checked = True Or chkMax20.Checked = True Then
                                GoTo CheckMaxCost
                            Else
                                GoTo AddRow
                            End If
CheckMaxCost:
                            dblMaxStonePrice = dblPricePerStone
                            If chkMaxCost.Checked = True Then
                                dblMaxStonePrice = dblPricePerStone
                            ElseIf chkMax25.Checked = True Then
                                dblMaxStonePrice = dblPricePerStone * 0.75
                            ElseIf chkMax20.Checked = True Then
                                dblMaxStonePrice = dblPricePerStone * 0.8
                            Else
                                dblMaxStonePrice = dblPricePerStone
                            End If

                            If Math.Round(dblHighValue / (dblBoxPcs), 1) + 13 <= dblMaxStonePrice Then

AddRow:
                                For intRow = 0 To flxAssort.Rows.Count - 1
                                    If flxAssort.Item(0, intRow).Value = strAssortment Then
                                        GoTo NextRecord
                                    End If
                                Next


                                flxAssort.Rows.Add(strAssortment,
                                                   intBalPcs - intIssPcs,
                                                   Format(dblBalCts - dblIssCts, "#0.000"),
                                                   intBalPcs - intIssPcs,
                                                   False,
                                                   Math.Round(dblTotVal2 / dblBoxPcs, 1) + 13,
                                                   Math.Round(dblTotVal / dblBoxPcs, 1) + 13,
                                                   Format((dblBoxCts) / (dblBoxPcs), "#0.000"),
                                                   rsComSql.Fields("MarketPrice").Value,
                                                   dblAvgCost,
                                                   Format(rsComSql.Fields("LengthFrom").Value, "#0.00") & " - " & Format(rsComSql.Fields("LengthTo").Value, "#0.00"),
                                                   Format(rsComSql.Fields("WidthFrom").Value, "#0.00") & " - " & Format(rsComSql.Fields("WidthTo").Value, "#0.00"),
                                                   strOrderNo,
                                                   txtNiruOrdNo.Text,
                                                   txtItemNo.Text,
                                                   txtDueDate.Text, strRef,
                                                   strSide, dblLen,
                                                   dblWid, dblHeight,
                                                   dblBalPcs, txtSubject.Text,
                                                   strBagAssortment, dblBagPrice,
                                                   Math.Round(dblTotValBag / dblBoxPcs, 1) + 13,
                                                   dblOpenPcs, dblInPcs, dblOutPcs, Math.Round(dblTurnOver, 2) & "%")

                                intTotPcs = intTotPcs + intBalPcs - intIssPcs
                                dblTotCts = dblTotCts + dblBalCts - dblIssCts

                                'Application.DoEvents()
                            End If
                        End If
NextRecord:
                        rsComSql.MoveNext()
                    End While
                End If
                rsComSql = Nothing
NextAssortCode:
                rsAssort.MoveNext()
            End While
        End If
        rsAssort = Nothing

    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(1, intRow).Value)
        Next

    End Function

    Private Function CalSelectPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalSelectPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(4).EditedFormattedValue = True Then
                CalSelectPcs = CalSelectPcs + Val(flxSample.Item(3, intRow).Value)
            End If
        Next

    End Function

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxOrder)
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        ExportToExcel(flxAssort)
    End Sub

    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ClearFields()
    End Sub

    Private Sub ClearFields()
        txtOrder.Text = ""
        txtOrderFrom.Text = ""
        txtOrderTo.Text = ""
        flxOrder.Rows.Clear()
        flxAssort.Rows.Clear()
        txtSubject.Text = ""
        txtClient.Text = ""
        txtBalPcs.Text = ""
        txtTotPcs.Text = ""
        txtSelPcs.Text = ""
        txtNiruOrdNo.Text = ""
        txtItemNo.Text = ""
        txtDueDate.Text = ""
        txtRghPerc.Text = ""
        txtRghPcs.Text = ""
        txtCurCost.Text = ""
        lblRough.Visible = False
        txtOrder.Focus()

        'Load_NewReq()
    End Sub

    Private Sub ClearFields2()
        flxAssort.Rows.Clear()
        txtTotPcs.Text = ""
        txtSelPcs.Text = ""
        txtCurCost.Text = ""
    End Sub

    Private Sub frm_MixOrderSolver_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Cat()
        Load_Color()
        Load_Clarity()
        Load_Flo()
        Load_Type()
        Load_Origin()
        lblRough.Visible = False
    End Sub

    Private Sub Load_Type()
        cmbType.Items.Clear()
        cmbType.Items.Add("Rough")
        cmbType.Items.Add("Polished")
    End Sub

    Private Sub Load_Origin()
        cmbOrigin.Items.Clear()
        cmbOrigin.Items.Add("AAC")
        cmbOrigin.Items.Add("ADT")
        cmbOrigin.Items.Add("AOD")
        cmbOrigin.Items.Add("ART")
    End Sub

    Private Sub Load_Cat()
        cmbCat.Items.Clear()

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM dbo.VW_AssortCodesAll ORDER BY AssortCode", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbCat.Items.Add(rsComSql.Fields("AssortCode").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
        'cmbCat.Items.Add("ANC")
        'cmbCat.Items.Add("ANV")
        'cmbCat.Items.Add("ANW")
        'cmbCat.Items.Add("ABA")
        'cmbCat.Items.Add("ANS")
        'cmbCat.Items.Add("ADV")
        'cmbCat.Items.Add("ALC")
        'cmbCat.Items.Add("AEC")
        'cmbCat.Items.Add("AEW")
        'cmbCat.Items.Add("SRD")
        'cmbCat.Items.Add("SRF")
        'cmbCat.Items.Add("SRG")
        'cmbCat.Items.Add("SRR")
        'cmbCat.Items.Add("SRW")
        'cmbCat.Items.Add("SSW")
    End Sub

    Private Sub Load_Color()
        cmbColor.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblAssortList.Color " & _
                      "FROM dbo.tblAssortList INNER JOIN dbo.VW_MixAssortInOutNew2020 ON dbo.tblAssortList.Assortment = dbo.VW_MixAssortInOutNew2020.Assortment " & _
                      "WHERE (dbo.VW_MixAssortInOutNew2020.Pcs > 0) AND (dbo.tblAssortList.Color <> N'') " & _
                      "GROUP BY dbo.tblAssortList.Color " & _
                      "ORDER BY dbo.tblAssortList.Color", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbColor.Items.Add(rsComSql.Fields("Color").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
        cmbColor.Items.Add("DF")
    End Sub

    Private Sub Load_Clarity()
        cmbClarity.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblAssortList.Clarity " & _
                      "FROM dbo.tblAssortList INNER JOIN dbo.VW_MixAssortInOutNew2020 ON dbo.tblAssortList.Assortment = dbo.VW_MixAssortInOutNew2020.Assortment " & _
                      "WHERE (dbo.VW_MixAssortInOutNew2020.Pcs > 0) AND (dbo.tblAssortList.Clarity <> N'') " & _
                      "GROUP BY dbo.tblAssortList.Clarity " & _
                      "ORDER BY dbo.tblAssortList.Clarity", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbClarity.Items.Add(rsComSql.Fields("Clarity").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Flo()
        cmbFlo.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblAssortList.Flo " & _
                      "FROM dbo.tblAssortList INNER JOIN dbo.VW_MixAssortInOutNew2020 ON dbo.tblAssortList.Assortment = dbo.VW_MixAssortInOutNew2020.Assortment " & _
                      "WHERE (dbo.VW_MixAssortInOutNew2020.Pcs > 0) AND (dbo.tblAssortList.Flo <> N'') " & _
                      "GROUP BY dbo.tblAssortList.Flo " & _
                      "ORDER BY dbo.tblAssortList.Flo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbFlo.Items.Add(rsComSql.Fields("Flo").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

    End Sub

    Private Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        Load_Details()
    End Sub

    Private Sub cmdStop_Click(sender As Object, e As EventArgs) Handles cmdStop.Click
        blnStop = True
    End Sub

    Private Sub flxOrder_Click(sender As Object, e As EventArgs) Handles flxOrder.Click
        'Dim dblLength As Double
        'Dim dblWidth As Double
        'Dim strRef As String
        'Dim strSide As String
        'Dim dblHeight As Double
        'Dim dblBalPcs As Double
        'Dim dblPricePStone As Double
        'Dim strOrder As String

        'If Len(txtSelPcs.Text) > 0 Then
        '    If CInt(txtSelPcs.Text) > 0 Then
        '        Exit Sub
        '    End If
        'End If
        'If flxOrder.Rows.Count > 0 Then
        '    If flxOrder.CurrentRow.Index >= 0 Then
        '        dblLength = Val(flxOrder.Item(2, flxOrder.CurrentRow.Index).Value)
        '        dblWidth = Val(flxOrder.Item(3, flxOrder.CurrentRow.Index).Value)
        '        strRef = Trim(flxOrder.Item(0, flxOrder.CurrentRow.Index).Value)
        '        strSide = Trim(flxOrder.Item(1, flxOrder.CurrentRow.Index).Value)
        '        dblHeight = Val(flxOrder.Item(4, flxOrder.CurrentRow.Index).Value)
        '        dblBalPcs = Val(flxOrder.Item(5, flxOrder.CurrentRow.Index).Value)
        '        dblPricePStone = Val(flxOrder.Item(8, flxOrder.CurrentRow.Index).Value)
        '        strOrder = Trim(flxOrder.Item(9, flxOrder.CurrentRow.Index).Value)

        '        blnStop = False
        '        txtTotPcs.Text = ""
        '        flxAssort.Rows.Clear()

        '        Me.Cursor = Cursors.WaitCursor
        '        FillAssorts(dblLength, dblWidth, strRef, strSide, dblHeight, dblBalPcs, dblPricePStone, strOrder)
        '        Me.Cursor = Cursors.Default

        '        txtTotPcs.Text = CalTotalPcs(flxAssort)
        '    End If
        'End If
    End Sub

    Private Sub flxOrder_DoubleClick(sender As Object, e As EventArgs) Handles flxOrder.DoubleClick
        Dim dblLength As Double
        Dim dblWidth As Double
        Dim strRef As String
        Dim strSide As String
        Dim dblHeight As Double
        Dim dblBalPcs As Double
        Dim dblPricePStone As Double
        Dim strOrder As String

        If Len(txtSelPcs.Text) > 0 Then
            If CInt(txtSelPcs.Text) > 0 Then
                Exit Sub
            End If
        End If
        If flxOrder.Rows.Count > 0 Then
            If flxOrder.CurrentRow.Index >= 0 Then
                dblLength = Val(flxOrder.Item(2, flxOrder.CurrentRow.Index).Value)
                dblWidth = Val(flxOrder.Item(3, flxOrder.CurrentRow.Index).Value)
                strRef = Trim(flxOrder.Item(0, flxOrder.CurrentRow.Index).Value)
                strSide = Trim(flxOrder.Item(1, flxOrder.CurrentRow.Index).Value)
                dblHeight = Val(flxOrder.Item(4, flxOrder.CurrentRow.Index).Value)
                dblBalPcs = Val(flxOrder.Item(5, flxOrder.CurrentRow.Index).Value)
                dblPricePStone = Val(flxOrder.Item(9, flxOrder.CurrentRow.Index).Value)
                strOrder = Trim(flxOrder.Item(10, flxOrder.CurrentRow.Index).Value)

                blnStop = False
                txtTotPcs.Text = ""
                flxAssort.Rows.Clear()

                Me.Cursor = Cursors.WaitCursor
                FillAssorts(dblLength, dblWidth, strRef, strSide, dblHeight, dblBalPcs, dblPricePStone, strOrder)
                Me.Cursor = Cursors.Default

                txtTotPcs.Text = CalTotalPcs(flxAssort)
            End If
        End If
    End Sub

    Private Sub flxAssort_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxAssort.CellContentClick
        If e.ColumnIndex = 4 Then
            txtSelPcs.Text = CalSelectPcs(flxAssort)
        End If
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        If chkSelect.Checked = True Then
            For intRow = 0 To flxAssort.RowCount - 1
                flxAssort.Item(4, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxAssort.RowCount - 1
                flxAssort.Item(4, intRow).Value = False
            Next
        End If
        txtSelPcs.Text = CalSelectPcs(flxAssort)
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            For intRow = 0 To flxAssort.Rows.Count - 1
                If flxAssort.Item(4, intRow).Value = True Then
                    If Not IsNumeric(flxAssort.Item(3, intRow).Value) Then
                        MsgBox("Invalid Select Pcs - " & flxAssort.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If CInt(flxAssort.Item(3, intRow).Value) > CInt(flxAssort.Item(1, intRow).Value) Then
                        MsgBox("Invalid Select Pcs - " & flxAssort.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblMixEmpIssuesReq WHERE Assortment = '" & flxAssort.Item(0, intRow).Value & "' AND Status = 0", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        MsgBox("Already Requested - " & rsComSql.Fields("EmpNo").Value & " " & flxAssort.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        flxAssort.Item(4, intRow).Value = False
                        Exit Sub
                    End If
                    rsComSql = Nothing
                End If
            Next

            For intRow = 0 To flxAssort.Rows.Count - 1
                If flxAssort.Item(4, intRow).Value = True Then
                    If CInt(flxAssort.Item(3, intRow).Value) > 0 Then
                        AdoCN.Execute("INSERT INTO tblMixEmpIssuesReq(Assortment,ReqPcs,EmpNo,ReqDate,ReqTime,OrderNo,RefNo,Side) " & _
                                      "VALUES('" & flxAssort.Item(0, intRow).Value & "'," & CInt(flxAssort.Item(3, intRow).Value) & "," & _
                                        "'" & PBUser_EmpNo & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "'," & _
                                        "" & CInt(flxAssort.Item(12, intRow).Value) & ",'" & Replace(flxAssort.Item(16, intRow).Value, "'", "''") & "','" & flxAssort.Item(17, intRow).Value & "')")
                    End If
                    
                End If
            Next
            MsgBox("Request Saved", MsgBoxStyle.Information + vbOKOnly, Me.Text)
            ClearFields2()
        End If

    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub flxAssort_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles flxAssort.CellValueChanged
        txtSelPcs.Text = CalSelectPcs(flxAssort)
    End Sub

    Private Sub chk10_CheckedChanged(sender As Object) Handles chk10.CheckedChanged
        If chk10.Checked = True Then
            chk10P.Checked = False
            chk20P.Checked = False
            chk20M.Checked = False
        End If
    End Sub

    Private Sub chk10P_CheckedChanged(sender As Object) Handles chk10P.CheckedChanged
        If chk10P.Checked = True Then
            chk10.Checked = False
            chk20P.Checked = False
            chk20M.Checked = False
        End If
    End Sub

    Private Sub chk20P_CheckedChanged(sender As Object) Handles chk20P.CheckedChanged
        If chk20P.Checked = True Then
            chk10.Checked = False
            chk10P.Checked = False
            chk20M.Checked = False
        End If
    End Sub

    Private Sub txtOrderFrom_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrderFrom.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtOrderTo.Focus()
        End If
    End Sub

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Dim intDiff As Integer
        Dim intIndex As Integer

        Dim intTotPcs As Integer
        Dim intIssPcs As Integer
        Dim intRejPcs As Integer
        Dim intLostPcs As Integer
        Dim intBalPcs As Integer
        Dim dblCurCost As Double
        Dim strShipDate As String
        Dim strNewOrderNo As String

        Dim strSubject As String
        Dim strClient As String
        Dim strDueDate As String

        If txtOrderFrom.Text = "" Then Exit Sub
        If txtOrderTo.Text = "" Then Exit Sub

        If CInt(txtOrderFrom.Text) > CInt(txtOrderTo.Text) Then Exit Sub

        intDiff = CInt(txtOrderTo.Text) - CInt(txtOrderFrom.Text)

        For intIndex = 0 To intDiff
            intTotPcs = 0
            strNewOrderNo = CInt(txtOrderFrom.Text) + intIndex
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblOrdersDtls WHERE OrderNo = " & CInt(strNewOrderNo) & " ORDER BY RefNo", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                'ExpProgress.Maximum = rsComSql.RecordCount
                While Not rsComSql.EOF
                    intTotPcs = rsComSql.Fields("Pcs").Value * rsComSql.Fields("Sets").Value

                    intIssPcs = 0
                    intRejPcs = 0
                    intLostPcs = 0
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM VW_MixSolver WHERE PktOrdNo = '" & strNewOrderNo & "' AND PktRefNo = '" & Replace(rsComSql.Fields("RefNo").Value, "'", "''") & "' AND Pktside = '" & rsComSql.Fields("Side").Value & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        If Not IsDBNull(rsComSql_1.Fields("PktPcs").Value) Then
                            intIssPcs = intIssPcs + rsComSql_1.Fields("PktPcs").Value
                            intRejPcs = CInt(rsComSql_1.Fields("RejPcs").Value)
                            intLostPcs = CInt(rsComSql_1.Fields("LostPcs").Value)
                        End If
                    End If
                    rsComSql_1 = Nothing

                    intIssPcs = intIssPcs - (intRejPcs + intLostPcs)

                    dblCurCost = Math.Round(dblCurCost, 2)

                    If (intTotPcs - intIssPcs) > 0 Then
                        strSubject = ""
                        strClient = ""
                        strDueDate = ""
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM tblOrders WHERE OrderNo = " & Val(strNewOrderNo) & "", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            strSubject = rsComSql_1.Fields("Subject").Value & " " & rsComSql_1.Fields("Subject2").Value
                            strClient = rsComSql_1.Fields("Niruref").Value
                            strDueDate = Format(rsComSql_1.Fields("DueDate").Value, "yyyy/MM/dd")
                        End If
                        rsComSql_1 = Nothing

                        strShipDate = ""
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT MIN(OrderDate) AS OrderDate FROM tblPlaneOrders WHERE (OrderNo = " & CInt(strNewOrderNo) & ")", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            If Not IsDBNull(rsComSql_1.Fields("OrderDate").Value) Then
                                strShipDate = Format(rsComSql_1.Fields("OrderDate").Value, "yyyy-MM-dd")
                            End If
                        End If
                        rsComSql_1 = Nothing

                        flxOrder.Rows.Add(rsComSql.Fields("RefNo").Value,
                                          rsComSql.Fields("Side").Value,
                                          rsComSql.Fields("Length").Value,
                                          rsComSql.Fields("Width").Value,
                                          rsComSql.Fields("Bothigh").Value,
                                          intTotPcs - intIssPcs,
                                          Format((rsComSql.Fields("Bothigh").Value / rsComSql.Fields("Width").Value) * 100, "#0.00"),
                                          IIf(rsComSql.Fields("Groove").Value = 1, "GRV", ""),
                                          IIf(rsComSql.Fields("Laser").Value > 0, "LZ", ""),
                                          rsComSql.Fields("MaxCost").Value & "/" & rsComSql.Fields("MaxType").Value,
                                          rsComSql.Fields("OrderNo").Value,
                                          "C" & strRight(strClient, 3),
                                          strSubject,
                                          strDueDate,
                                          strShipDate,
                                          rsComSql.Fields("MaxType").Value)

                        intBalPcs = intBalPcs + (intTotPcs - intIssPcs)
                    End If

                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing
            txtBalPcs.Text = intBalPcs
        Next
    End Sub

    Private Sub chk20M_CheckedChanged(sender As Object) Handles chk20M.CheckedChanged
        If chk20M.Checked = True Then
            chk10P.Checked = False
            chk20P.Checked = False
            chk20P.Checked = False
        End If
    End Sub

    Private Sub cmdOpen_Click(sender As Object, e As EventArgs) Handles cmdOpen.Click
        flxOrder.Rows.Clear()
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
            flxOrder.Rows.Clear()

            For intRow = 2 To 10000
                If xlWorkSheet.Cells(intRow, 1).Value = "" Then Exit For
                flxOrder.Rows.Add(Trim(xlWorkSheet.Cells(intRow, 1).Value), Trim(xlWorkSheet.Cells(intRow, 2).Value),
                                Trim(xlWorkSheet.Cells(intRow, 3).Value), Trim(xlWorkSheet.Cells(intRow, 4).Value),
                                Trim(xlWorkSheet.Cells(intRow, 5).Value), Trim(xlWorkSheet.Cells(intRow, 6).Value),
                                Trim(xlWorkSheet.Cells(intRow, 7).Value), Trim(xlWorkSheet.Cells(intRow, 8).Value),
                                Trim(xlWorkSheet.Cells(intRow, 9).Value), Trim(xlWorkSheet.Cells(intRow, 10).Value),
                                Trim(xlWorkSheet.Cells(intRow, 11).Value), Trim(xlWorkSheet.Cells(intRow, 12).Value),
                                Trim(xlWorkSheet.Cells(intRow, 13).Value), Trim(xlWorkSheet.Cells(intRow, 14).Value),
                                Trim(xlWorkSheet.Cells(intRow, 15).Value), Trim(xlWorkSheet.Cells(intRow, 16).Value))

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

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        Load_Excel()
    End Sub

    Private Sub chkMaxCost_CheckedChanged(sender As Object) Handles chkMaxCost.CheckedChanged
        If chkMaxCost.Checked = True Then
            chkMax25.Checked = False
            chkMax20.Checked = False
        End If
    End Sub

    Private Sub chkMax25_CheckedChanged(sender As Object) Handles chkMax25.CheckedChanged
        If chkMax25.Checked = True Then
            chkMaxCost.Checked = False
            chkMax20.Checked = False
        End If
    End Sub

    Private Sub chkMax20_CheckedChanged(sender As Object) Handles chkMax20.CheckedChanged
        If chkMax20.Checked = True Then
            chkMax25.Checked = False
            chkMaxCost.Checked = False
        End If
    End Sub

    Private Sub FillAssorts2(ByVal dblLen As Double, ByVal dblWid As Double, ByVal dblBalPcs As Double, ByVal dblPricePerStone As Double, ByVal strRef As String)

        Dim rsAssort As ADODB.Recordset
        'Dim intRow As Integer
        Dim strAssortment As String
        Dim dblLengthFrom As Double
        Dim dblLengthTo As Double
        Dim dblWidthFrom As Double
        Dim dblWidthTo As Double
        Dim strSelectFrom As String
        Dim strWhere As String
        Dim strOrder As String

        Dim intBalPcs As Integer
        Dim dblBalCts As Double

        Dim intTotPcs As Integer
        Dim dblTotCts As Double
        Dim dblAvgCost As Double

        Dim dblTotCts2 As Double
        Dim dblTotVal As Double
        Dim dblTotVal2 As Double
        Dim dblTotValBag As Double

        Dim intIssPcs As Integer
        Dim dblIssCts As Double
        Dim dblAvgPrice As Double
        Dim dblBagPrice As Double
        Dim strBagAssortment As String

        Dim dblOpenPcs As Double
        Dim dblInPcs As Double
        Dim dblOutPcs As Double
        Dim dblRejPcs As Double
        Dim dtpFromDate As Date
        Dim dblTurnOver As Double

        Dim dblBoxPcs As Double
        Dim dblBoxCts As Double

        Dim dblCurCost As Double
        Dim dblCurPcs As Double

        Dim dblHighValue As Double
        Dim blnStockHave As Boolean
        Dim dblMaxStonePrice As Double

        Dim dblOrdPcs As Double

        dblOrdPcs = dblBalPcs

        dtpFromDate = DateAdd(DateInterval.Month, -1 * 6, Date.Now)

        strAssortment = ""
        intTotPcs = 0
        dblTotCts = 0
        dblAvgCost = 0
        dblTotVal = 0
        dblTotCts2 = 0
        dblTotVal2 = 0
        dblHighValue = 0
        dblMaxStonePrice = 0

        dblCurCost = 0
        dblCurPcs = 0

        dblLen = Math.Round(dblLen, 2)
        dblWid = Math.Round(dblWid, 2)

        blnStockHave = False
        rsAssort = New ADODB.Recordset
        'If cmbCat.Text <> "" Then
        '    rsAssort.Open("SELECT * FROM dbo.VW_AssortCodesAll WHERE AssortCode = '" & cmbCat.Text & "' ORDER BY Seq", AdoCN, 1, 1)
        'Else
        '    rsAssort.Open("SELECT * FROM dbo.VW_AssortCodesAll ORDER BY Seq", AdoCN, 1, 1)
        'End If
        rsAssort.Open("SELECT * FROM tblAssortCodes2 WHERE AssortCode = 'ABGCN' ORDER BY Seq", AdoCN, 1, 1)
        If rsAssort.RecordCount Then
            rsAssort.MoveFirst()
            While Not rsAssort.EOF
                If blnStop = True Then Exit Sub

                If chkAdvance.Checked = True Then
                    dblLengthFrom = dblLen + rsAssort.Fields("LenMin").Value
                    dblWidthFrom = dblWid + rsAssort.Fields("WidMin").Value

                    dblLengthTo = dblLen + rsAssort.Fields("LenMax").Value + 0.1
                    dblWidthTo = dblWid + rsAssort.Fields("WidMax").Value + 0.1

                ElseIf chkNormal.Checked = True Then
                    dblLengthFrom = dblLen + rsAssort.Fields("LenMin").Value
                    dblWidthFrom = dblWid + rsAssort.Fields("WidMin").Value

                    dblLengthTo = dblLen + rsAssort.Fields("LenMax").Value
                    dblWidthTo = dblWid + rsAssort.Fields("WidMax").Value

                ElseIf chkExtra.Checked = True Then
                    dblLengthFrom = dblLen + rsAssort.Fields("LenMin").Value
                    dblWidthFrom = dblWid + rsAssort.Fields("WidMin").Value

                    dblLengthTo = dblLen + rsAssort.Fields("LenMax").Value + 0.2
                    dblWidthTo = dblWid + rsAssort.Fields("WidMax").Value + 0.2

                ElseIf chkExtra2.Checked = True Then
                    dblLengthFrom = dblLen + rsAssort.Fields("LenMin").Value
                    dblWidthFrom = dblWid + rsAssort.Fields("WidMin").Value

                    dblLengthTo = dblLen + rsAssort.Fields("LenMax").Value + 0.3
                    dblWidthTo = dblWid + rsAssort.Fields("WidMax").Value + 0.3

                Else
                    dblLengthFrom = dblLen + rsAssort.Fields("LenMin").Value
                    dblWidthFrom = dblWid + rsAssort.Fields("WidMin").Value

                    dblLengthTo = dblLen + rsAssort.Fields("LenMax").Value
                    dblWidthTo = dblWid + rsAssort.Fields("WidMax").Value
                End If

                If chk10.Checked = True Then
                    dblLengthFrom = dblLengthFrom - 0.1
                    dblWidthFrom = dblWidthFrom - 0.1
                End If
                If chk10P.Checked = True Then
                    dblLengthFrom = dblLengthFrom + 0.1
                    dblWidthFrom = dblWidthFrom + 0.1
                End If
                If chk20P.Checked = True Then
                    dblLengthFrom = dblLengthFrom + 0.2
                    dblWidthFrom = dblWidthFrom + 0.2
                End If
                If chk20M.Checked = True Then
                    dblLengthFrom = dblLengthFrom - 0.2
                    dblWidthFrom = dblWidthFrom - 0.2
                End If

                dblLengthFrom = Math.Round(dblLengthFrom, 2)
                dblWidthFrom = Math.Round(dblWidthFrom, 2)
                dblLengthTo = Math.Round(dblLengthTo, 2)
                dblWidthTo = Math.Round(dblWidthTo, 2)

                rsComSql = New ADODB.Recordset

                strSelectFrom = "SELECT  TOP (100) PERCENT dbo.tblAssortList.Assortment, dbo.tblAssortList.Color, dbo.tblAssortList.Clarity, dbo.tblAssortList.LengthFrom, dbo.tblAssortList.LengthTo, dbo.tblAssortList.WidthFrom, " & _
                                    "dbo.tblAssortList.WidthTo, dbo.tblAssortList.Make, dbo.tblAssortList.Flo, dbo.tblAssortList.AvWeight, dbo.tblAssortList.Shape, dbo.tblAssortList.CurrentCost, dbo.tblAssortList.MarketPrice, " & _
                                    "dbo.tblAssortList.AvgCost, dbo.tblAssortList.StonePrice, dbo.tblAssortList.AvgStonePrice, dbo.Comp181.Pcs, dbo.Comp181.Cts " & _
                                "FROM dbo.tblAssortList INNER JOIN dbo.Comp181 ON dbo.tblAssortList.Assortment = dbo.Comp181.Assortment "

                'strSelectFrom = "SELECT  TOP (100) PERCENT dbo.tblAssortList.Assortment, dbo.tblAssortList.Color, dbo.tblAssortList.Clarity, dbo.tblAssortList.LengthFrom, dbo.tblAssortList.LengthTo, dbo.tblAssortList.WidthFrom, " & _
                '                    "dbo.tblAssortList.WidthTo, dbo.tblAssortList.Make, dbo.tblAssortList.Flo, dbo.tblAssortList.AvWeight, dbo.tblAssortList.Shape, dbo.tblAssortList.CurrentCost, dbo.tblAssortList.MarketPrice, " & _
                '                    "dbo.tblAssortList.AvgCost, dbo.tblAssortList.StonePrice, dbo.tblAssortList.AvgStonePrice, dbo.VW_MixAssortInOutNew2020.Pcs, dbo.VW_MixAssortInOutNew2020.Cts " & _
                '                "FROM dbo.tblAssortList INNER JOIN dbo.VW_MixAssortInOutNew2020 ON dbo.tblAssortList.Assortment = dbo.VW_MixAssortInOutNew2020.Assortment "

                If rsAssort.Fields("AssortCode").Value = "ABA" Then
                    strWhere = " WHERE (dbo.tblAssortList.LengthFrom >= " & dblLengthFrom & " OR dbo.tblAssortList.LengthTo >= " & dblLengthFrom & ") AND (dbo.tblAssortList.WidthFrom >= " & dblWidthFrom & " OR " & _
                                    "dbo.tblAssortList.WidthTo >= " & dblWidthFrom & ") AND (LEFT(dbo.tblAssortList.Assortment, 3) = '" & rsAssort.Fields("AssortCode").Value & "') AND (dbo.Comp181.Pcs > 0) "
                Else
                    If Len(rsAssort.Fields("AssortCode").Value) = 3 Then
                        strWhere = " WHERE (dbo.tblAssortList.LengthFrom >= " & dblLengthFrom & ") AND (dbo.tblAssortList.LengthFrom <= " & dblLengthTo & ") AND (dbo.tblAssortList.WidthFrom >= " & dblWidthFrom & ") AND " & _
                                    "(dbo.tblAssortList.WidthFrom <= " & dblWidthTo & ") AND (LEFT(dbo.tblAssortList.Assortment, 3) = '" & rsAssort.Fields("AssortCode").Value & "') AND (dbo.Comp181.Pcs > 0) "

                    ElseIf Len(rsAssort.Fields("AssortCode").Value) = 4 Then
                        strWhere = " WHERE (dbo.tblAssortList.LengthFrom >= " & dblLengthFrom & ") AND (dbo.tblAssortList.LengthFrom <= " & dblLengthTo & ") AND (dbo.tblAssortList.WidthFrom >= " & dblWidthFrom & ") AND " & _
                                    "(dbo.tblAssortList.WidthFrom <= " & dblWidthTo & ") AND (SUBSTRING(dbo.tblAssortList.Assortment, 7, 4) = '" & rsAssort.Fields("AssortCode").Value & "') AND (dbo.Comp181.Pcs > 0) "
                    Else
                        strWhere = " WHERE (dbo.tblAssortList.LengthTo >= " & dblLengthFrom & ") AND (dbo.tblAssortList.WidthTo >= " & dblWidthFrom & ") AND (LEFT(dbo.tblAssortList.Assortment, 5) = '" & rsAssort.Fields("AssortCode").Value & "') AND (dbo.Comp181.Pcs > 0) "
                    End If
                End If

                'If rsAssort.Fields("AssortCode").Value = "ABA" Then
                '    strWhere = " WHERE (dbo.tblAssortList.LengthFrom >= " & dblLengthFrom & " OR dbo.tblAssortList.LengthTo >= " & dblLengthFrom & ") AND (dbo.tblAssortList.WidthFrom >= " & dblWidthFrom & " OR " & _
                '                    "dbo.tblAssortList.WidthTo >= " & dblWidthFrom & ") AND (LEFT(dbo.tblAssortList.Assortment, 3) = '" & rsAssort.Fields("AssortCode").Value & "') AND (dbo.VW_MixAssortInOutNew2020.Pcs > 0) "
                'Else
                '    If Len(rsAssort.Fields("AssortCode").Value) = 3 Then
                '        strWhere = " WHERE (dbo.tblAssortList.LengthFrom >= " & dblLengthFrom & ") AND (dbo.tblAssortList.LengthFrom <= " & dblLengthTo & ") AND (dbo.tblAssortList.WidthFrom >= " & dblWidthFrom & ") AND " & _
                '                    "(dbo.tblAssortList.WidthFrom <= " & dblWidthTo & ") AND (LEFT(dbo.tblAssortList.Assortment, 3) = '" & rsAssort.Fields("AssortCode").Value & "') AND (dbo.VW_MixAssortInOutNew2020.Pcs > 0) "

                '    ElseIf Len(rsAssort.Fields("AssortCode").Value) = 4 Then
                '        strWhere = " WHERE (dbo.tblAssortList.LengthFrom >= " & dblLengthFrom & ") AND (dbo.tblAssortList.LengthFrom <= " & dblLengthTo & ") AND (dbo.tblAssortList.WidthFrom >= " & dblWidthFrom & ") AND " & _
                '                    "(dbo.tblAssortList.WidthFrom <= " & dblWidthTo & ") AND (SUBSTRING(dbo.tblAssortList.Assortment, 7, 4) = '" & rsAssort.Fields("AssortCode").Value & "') AND (dbo.VW_MixAssortInOutNew2020.Pcs > 0) "
                '    Else
                '        strWhere = " WHERE (dbo.tblAssortList.LengthFrom >= " & dblLengthFrom & ") AND (dbo.tblAssortList.LengthFrom <= " & dblLengthTo & ") AND (dbo.tblAssortList.WidthFrom >= " & dblWidthFrom & ") AND " & _
                '                    "(dbo.tblAssortList.WidthFrom <= " & dblWidthTo & ") AND (LEFT(dbo.tblAssortList.Assortment, 5) = '" & rsAssort.Fields("AssortCode").Value & "') AND (dbo.VW_MixAssortInOutNew2020.Pcs > 0) "
                '    End If
                'End If

                If cmbColor.Text <> "" Then
                    If cmbColor.Text = "DF" Then
                        strWhere = strWhere & " AND (dbo.tblAssortList.Color = 'DE' OR dbo.tblAssortList.Color = 'F')"
                    Else
                        strWhere = strWhere & " AND (dbo.tblAssortList.Color = '" & cmbColor.Text & "')"
                    End If
                End If
                If cmbClarity.Text <> "" Then
                    strWhere = strWhere & " AND (dbo.tblAssortList.Clarity = '" & cmbClarity.Text & "')"
                End If
                If cmbFlo.Text <> "" Then
                    strWhere = strWhere & " AND (dbo.tblAssortList.Flo = '" & cmbFlo.Text & "')"
                End If
                If cmbType.Text <> "" Then
                    If cmbType.Text = "Rough" Then
                        strWhere = strWhere & " AND (dbo.tblAssortList.Assortment LIKE 'S%')"
                    ElseIf cmbType.Text = "Polished" Then
                        strWhere = strWhere & " AND (dbo.tblAssortList.Assortment NOT LIKE 'S%')"
                    Else
                        strWhere = strWhere & " AND (dbo.tblAssortList.Assortment LIKE 'A%')"
                    End If
                End If
                If cmbOrigin.Text <> "" Then
                    strWhere = strWhere & " AND (LEFT(dbo.tblAssortList.Assortment, 3) = '" & cmbOrigin.Text & "')"
                End If
                strOrder = " ORDER BY dbo.tblAssortList.Assortment"

                mStrSQL = strSelectFrom & strWhere & strOrder
                rsComSql.Open(mStrSQL, AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        intBalPcs = 0
                        dblBalCts = 0
                        dblAvgCost = 0
                        dblTotVal = 0
                        dblTotVal2 = 0
                        dblTotCts2 = 0
                        intIssPcs = 0
                        dblIssCts = 0
                        dblTotValBag = 0
                        dblBagPrice = 0
                        dblOpenPcs = 0
                        dblInPcs = 0
                        dblOutPcs = 0
                        dblRejPcs = 0
                        dblTurnOver = 0
                        dblAvgPrice = 0

                        dblBoxPcs = 0
                        dblBoxCts = 0
                        strBagAssortment = ""

                        strAssortment = rsComSql.Fields("Assortment").Value

                        'If strAssortment = "SRW3231C111" Then
                        '    MsgBox(strAssortment, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        'End If

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM dbo.tblAssortBlock WHERE Assortment = '" & strAssortment & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            GoTo NextRecord
                        End If
                        rsComSql_1 = Nothing

                        intBalPcs = 0
                        dblBalCts = 0

                        intBalPcs = rsComSql.Fields("Pcs").Value
                        dblBalCts = rsComSql.Fields("Cts").Value
                        dblBalCts = Math.Round(dblBalCts, 3)

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT SUM(Pcs) as Pcs FROM dbo.tblTempOrderPcsNew WHERE Assortment = '" & strAssortment & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                                intBalPcs = intBalPcs - rsComSql_1.Fields("Pcs").Value
                            End If
                        End If
                        rsComSql_1 = Nothing

                        If intBalPcs > 0 Then
                            dblAvgPrice = rsComSql.Fields("AvgCost").Value
                            '===============
                            dblAvgPrice = Math.Round(dblAvgPrice, 2)

                            dblBoxPcs = rsComSql.Fields("Pcs").Value
                            dblBoxCts = rsComSql.Fields("Cts").Value

                            If intBalPcs >= dblBalPcs Then
                                intIssPcs = dblBalPcs

                                AdoCN.Execute("INSERT INTO tblTempOrderPcsNew VALUES('" & strAssortment & "','" & intIssPcs & "')")

                                dblBalPcs = dblBalPcs - intIssPcs
                            Else
                                intIssPcs = intBalPcs

                                dblBalPcs = dblBalPcs - intIssPcs

                                AdoCN.Execute("INSERT INTO tblTempOrderPcsNew VALUES('" & strAssortment & "','" & intIssPcs & "')")
                            End If

                            'If strAssortment = "ABDVN-3437-1416" Then
                            '    MsgBox(strAssortment)
                            'End If
                            flxAssort.Rows.Add(strAssortment,
                                            intIssPcs,
                                            Format(dblBalCts - dblIssCts, "#0.000"),
                                            intIssPcs,
                                            False,
                                            Math.Round(dblTotVal2 / dblBoxPcs, 1) + 13,
                                            Math.Round(dblTotVal / dblBoxPcs, 1) + 13,
                                            Format((dblBoxCts) / (dblBoxPcs), "#0.000"),
                                            rsComSql.Fields("MarketPrice").Value,
                                            dblAvgCost,
                                            Format(rsComSql.Fields("LengthFrom").Value, "#0.00") & " - " & Format(rsComSql.Fields("LengthTo").Value, "#0.00"),
                                            Format(rsComSql.Fields("WidthFrom").Value, "#0.00") & " - " & Format(rsComSql.Fields("WidthTo").Value, "#0.00"),
                                            "",
                                            txtNiruOrdNo.Text,
                                            txtItemNo.Text,
                                            txtDueDate.Text, strRef,
                                            "", dblLen,
                                            dblWid, "",
                                            dblOrdPcs, txtSubject.Text,
                                            strBagAssortment, dblBagPrice,
                                            Math.Round(dblTotValBag / dblBoxPcs, 1) + 13,
                                            dblOpenPcs, dblInPcs, dblOutPcs, Math.Round(dblTurnOver, 2) & "%")

                            intTotPcs = intTotPcs + intBalPcs - intIssPcs
                            dblTotCts = dblTotCts + dblBalCts - dblIssCts

                            If dblBalPcs = 0 Then
                                Exit Sub
                            Else

                            End If
                        End If
NextRecord:
                        rsComSql.MoveNext()
                    End While
                End If
                rsComSql = Nothing
NextAssortCode:
                rsAssort.MoveNext()
            End While
        End If
        rsAssort = Nothing

    End Sub

    Private Sub Load_NewReq()
        flxOrder.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM Comp961 ORDER BY IndexNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxOrder.Rows.Add(rsComSql.Fields("Name").Value, "", rsComSql.Fields("Length").Value,
                                  rsComSql.Fields("Width").Value, "0",
                                  rsComSql.Fields("Pcs").Value, "", "", "", 1000)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        Load_Details2()
    End Sub
End Class