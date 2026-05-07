
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixAssortRejectVerify

    Private Sub Load_AssortTypes()
        cmbAssortType.Items.Clear()
        rsComSql_4 = New ADODB.Recordset
        rsComSql_4.Open("SELECT TOP (100) PERCENT LEFT(Assortment, 3) AS AssortCode FROM dbo.tblAssortList GROUP BY LEFT(Assortment, 3) ORDER BY AssortCode", AdoCN, 1, 1)
        If rsComSql_4.RecordCount Then
            rsComSql_4.MoveFirst()
            While Not rsComSql_4.EOF
                cmbAssortType.Items.Add(rsComSql_4.Fields("AssortCode").Value)
                rsComSql_4.MoveNext()
            End While
        End If
        rsComSql_4 = Nothing
    End Sub

    Private Sub Load_AssortCodes()
        cmbAssortCode.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Category FROM tblAssortCode GROUP BY Category ORDER BY Category", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbAssortCode.Items.Add(rsComSql.Fields("Category").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Rejects()
        Dim dblWtAvgOld As Double
        Dim dblPrice As Double

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        If chkAll.Checked = True Then
            rsComSql.Open("SELECT * FROM tblMixRejects WHERE Stock = 1 AND OK = 0 ORDER BY ParNo,PktNo,Assortment", AdoCN, 1, 1)
        Else
            rsComSql.Open("SELECT * FROM tblMixRejects WHERE Stock = 1 AND OK = 0 AND RejDate = '" & Format(dtpDate.Value, "MM/dd/yyyy") & "' ORDER BY ParNo,PktNo,Assortment", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                dblWtAvgOld = 0
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT AvgCost FROM dbo.tblAssortList WHERE Assortment = '" & rsComSql.Fields("OldAssort").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    dblWtAvgOld = rsComSql_1.Fields("AvgCost").Value
                End If
                rsComSql_1 = Nothing

                dblPrice = rsComSql.Fields("Price").Value
                If Mid(rsComSql.Fields("Assortment").Value, 1, 1) = "S" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT AvgStonePrice FROM dbo.tblAssortList WHERE Assortment = '" & rsComSql.Fields("Assortment").Value & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        dblPrice = (rsComSql_1.Fields("AvgStonePrice").Value * rsComSql.Fields("Pcs").Value) / rsComSql.Fields("Cts").Value
                    End If
                    rsComSql_1 = Nothing

                    dblPrice = Math.Round(dblPrice, 2)
                End If

                flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    rsComSql.Fields("Cts").Value,
                                    rsComSql.Fields("Assortment").Value,
                                    rsComSql.Fields("OrgAssort").Value,
                                    dblPrice,
                                    rsComSql.Fields("InID").Value,
                                    rsComSql.Fields("ImportNo").Value,
                                    rsComSql.Fields("OldAssort").Value, False,
                                    rsComSql.Fields("ID").Value,
                                    dblWtAvgOld,
                                    Format(rsComSql.Fields("RejDate").Value, "yyyy/MM/dd"),
                                    rsComSql.Fields("SupParNo").Value,
                                    rsComSql.Fields("Origin").Value,
                                    Format(rsComSql.Fields("ProdRejDate").Value, "yyyy/MM/dd"))

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

    End Sub

    Private Sub frm_MixAssortRejectVerify_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        dtpDate.Value = DateAdd(DateInterval.Day, -1, Date.Now)
        Load_AssortTypes()
        Load_AssortCodes()
        Load_Rejects()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub AddToStock()
        Dim intRow As Integer
        Dim blnSave As Boolean
        Dim dblWtAvgOld As Double
        Dim dblWtAvgNew As Double
        Dim strError As String
        Dim dblListPrice As Double
        Dim dblListPriceNew As Double
        Dim dblStonePrice As Double
        Dim dblStonePriceOld As Double
        Dim dblListPriceOld As Double
        Dim dblAvgPriceOld As Double
        Dim dblTotPcs As Double
        Dim dblTotCts As Double
        Dim dblInValue As Double
        Dim dblStoneAvg As Double
        Dim strNewType As String

        Dim dblDiaCost As Double
        Dim dblDiaCostOld As Double
        Dim dblDiaValue As Double
        Dim dblDiaCostAvg As Double

        Dim dblTotalValueAct As Double
        Dim dblTotalValueSys As Double

        blnSave = False
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(11, intRow).Value = True Then
                    strError = flxDetails.Item(5, intRow).Value & " (" & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value & ")"
                    If Len(flxDetails.Item(4, intRow).Value) = 0 Then
                        MsgBox("Invalid Act Cts - " & strError, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If IsNumeric(flxDetails.Item(4, intRow).Value) = False Then
                        MsgBox("Invalid Act Cts - " & strError, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If CDbl(flxDetails.Item(4, intRow).Value) < CDbl(flxDetails.Item(3, intRow).Value) Then
                        MsgBox("Invalid Act Cts - " & strError, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblAssortBlock WHERE Assortment = '" & flxDetails.Item(5, intRow).Value & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        MsgBox("Assortment is Blocked - " & strError, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql = Nothing

                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblAssortDetails WHERE AssortBox = '" & flxDetails.Item(12, intRow).Value & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        MsgBox("Assortment is already added - " & strError, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql = Nothing
                End If
            Next
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(11, intRow).Value = True Then
                    blnSave = True

                    dblListPrice = 0
                    dblListPriceNew = 0
                    dblStonePrice = 0
                    dblStonePriceOld = 0
                    dblAvgPriceOld = 0
                    dblInValue = 0
                    dblStoneAvg = 0

                    dblDiaCost = 0
                    dblDiaCostOld = 0
                    dblDiaValue = 0
                    dblDiaCostAvg = 0

                    'Calculate Weighted Average
                    dblWtAvgOld = 0
                    strNewType = ""
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT MarketPrice, AvgCost, AvgStonePrice, NewType, DiaCost FROM dbo.tblAssortList WHERE Assortment = '" & flxDetails.Item(5, intRow).Value & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        dblListPrice = rsComSql.Fields("MarketPrice").Value
                        dblWtAvgOld = rsComSql.Fields("AvgCost").Value
                        dblStonePrice = rsComSql.Fields("AvgStonePrice").Value
                        strNewType = rsComSql.Fields("NewType").Value
                        dblDiaCost = rsComSql.Fields("DiaCost").Value
                    End If
                    rsComSql = Nothing

                    dblListPriceOld = 0
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT MarketPrice, AvgCost, AvgStonePrice, DiaCost FROM dbo.tblAssortList WHERE Assortment = '" & flxDetails.Item(10, intRow).Value & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        dblListPriceOld = rsComSql.Fields("MarketPrice").Value
                        dblStonePriceOld = rsComSql.Fields("AvgStonePrice").Value
                        dblAvgPriceOld = rsComSql.Fields("AvgCost").Value
                        dblDiaCostOld = rsComSql.Fields("DiaCost").Value
                    End If
                    rsComSql = Nothing

                    dblWtAvgNew = 0
                    dblTotPcs = 0
                    dblTotCts = 0

                    dblTotalValueAct = 0
                    dblTotalValueSys = 0
                    
                    'SRW & SSW
                    If Mid(flxDetails.Item(5, intRow).Value, 1, 1) = "X" Then
                        dblWtAvgNew = 0

                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT BoxPcs, ProdPcs, BoxCts, ProdCts, BankCts " & _
                                      "FROM VW_MixPCUStock2020 " & _
                                      "WHERE Assortment = '" & flxDetails.Item(5, intRow).Value & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            If Not IsDBNull(rsComSql.Fields("BoxCts").Value) Then
                                dblTotPcs = rsComSql.Fields("BoxPcs").Value + rsComSql.Fields("ProdPcs").Value
                                dblTotCts = Math.Round(rsComSql.Fields("BoxCts").Value + IIf(rsComSql.Fields("ProdCts").Value > 0, rsComSql.Fields("ProdCts").Value, 0) + rsComSql.Fields("BankCts").Value, 3)

                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT RetPcs, RetCts FROM VW_MixPktRejExpNewY WHERE Assortment = '" & flxDetails.Item(5, intRow).Value & "'", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount Then
                                    dblTotPcs = dblTotPcs + rsComSql_1.Fields("RetPcs").Value
                                    dblTotCts = dblTotCts + rsComSql_1.Fields("RetCts").Value
                                End If
                                rsComSql_1 = Nothing

                                If Mid(flxDetails.Item(10, intRow).Value, 1, 1) = "X" Then
                                    dblWtAvgNew = (((dblTotCts * dblWtAvgOld) + (dblStonePrice * CDbl(flxDetails.Item(2, intRow).Value))) / (CDbl(flxDetails.Item(4, intRow).Value) + dblTotCts))
                                    dblStoneAvg = ((dblTotPcs * dblStonePrice) + (dblStonePriceOld * CDbl(flxDetails.Item(2, intRow).Value))) / (dblTotPcs + CDbl(flxDetails.Item(2, intRow).Value))
                                Else
                                    dblWtAvgNew = (((dblTotCts * dblWtAvgOld) + (dblStonePrice * CDbl(flxDetails.Item(2, intRow).Value))) / (CDbl(flxDetails.Item(4, intRow).Value) + dblTotCts))
                                    dblStoneAvg = ((dblTotPcs * dblStonePrice) + (CDbl(flxDetails.Item(13, intRow).Value) * CDbl(flxDetails.Item(4, intRow).Value))) / (dblTotPcs + CDbl(flxDetails.Item(2, intRow).Value))
                                End If

                                dblListPriceNew = (((dblTotCts * dblListPrice) + (dblStonePrice * CDbl(flxDetails.Item(2, intRow).Value))) / (CDbl(flxDetails.Item(4, intRow).Value) + dblTotCts))
                            Else
                                dblWtAvgNew = (dblStonePrice * CDbl(flxDetails.Item(2, intRow).Value)) / CDbl(flxDetails.Item(4, intRow).Value)
                                dblListPriceNew = (dblStonePrice * CDbl(flxDetails.Item(2, intRow).Value)) / CDbl(flxDetails.Item(4, intRow).Value)
                                dblStoneAvg = dblStonePriceOld
                            End If
                        End If
                        rsComSql = Nothing

                        dblWtAvgNew = Math.Round(dblWtAvgNew, 2)
                        dblListPriceNew = Math.Round(dblListPriceNew, 2)
                        dblStoneAvg = Math.Round(dblStoneAvg, 2)

                        If Mid(flxDetails.Item(10, intRow).Value, 1, 1) = "X" Then
                            dblTotalValueAct = (dblTotPcs * dblStonePrice) + (dblStonePriceOld * CDbl(flxDetails.Item(2, intRow).Value))
                            dblTotalValueSys = dblStoneAvg * (dblTotPcs + CDbl(flxDetails.Item(2, intRow).Value))
                        Else
                            dblTotalValueAct = (dblTotPcs * dblStonePrice) + (CDbl(flxDetails.Item(13, intRow).Value) * CDbl(flxDetails.Item(4, intRow).Value))
                            dblTotalValueSys = dblStoneAvg * (dblTotPcs + CDbl(flxDetails.Item(2, intRow).Value))
                        End If
                    Else
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT BoxCts, ProdCts, BankCts " & _
                                      "FROM VW_MixPCUStock2020 " & _
                                      "WHERE Assortment = '" & flxDetails.Item(5, intRow).Value & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            If Not IsDBNull(rsComSql.Fields("BoxCts").Value) Then
                                dblTotCts = Math.Round(rsComSql.Fields("BoxCts").Value + IIf(rsComSql.Fields("ProdCts").Value > 0, rsComSql.Fields("ProdCts").Value, 0) + rsComSql.Fields("BankCts").Value, 3)

                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT RetCts FROM VW_MixPktRejExpNewY WHERE Assortment = '" & flxDetails.Item(5, intRow).Value & "'", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount Then
                                    dblTotCts = dblTotCts + rsComSql_1.Fields("RetCts").Value
                                End If
                                rsComSql_1 = Nothing

                                If dblTotCts > 0 Then
                                    If Mid(flxDetails.Item(10, intRow).Value, 1, 1) = "X" Then
                                        dblInValue = (CDbl(flxDetails.Item(2, intRow).Value) * dblStonePriceOld)
                                        dblWtAvgNew = ((dblTotCts * dblWtAvgOld) + dblInValue) / (CDbl(flxDetails.Item(4, intRow).Value) + dblTotCts)
                                    Else
                                        dblInValue = CDbl(flxDetails.Item(4, intRow).Value) * CDbl(flxDetails.Item(13, intRow).Value)
                                        dblWtAvgNew = ((dblTotCts * dblWtAvgOld) + dblInValue) / (CDbl(flxDetails.Item(4, intRow).Value) + dblTotCts)

                                        dblDiaValue = CDbl(flxDetails.Item(4, intRow).Value) * dblDiaCostOld
                                        dblDiaCostAvg = ((dblTotCts * dblDiaCost) + dblDiaValue) / (CDbl(flxDetails.Item(4, intRow).Value) + dblTotCts)
                                    End If
                                Else
                                    If Mid(flxDetails.Item(10, intRow).Value, 1, 1) = "X" Then
                                        dblInValue = CDbl(flxDetails.Item(2, intRow).Value) * dblStonePriceOld
                                        dblWtAvgNew = dblInValue / CDbl(flxDetails.Item(4, intRow).Value)
                                    Else
                                        dblWtAvgNew = CDbl(flxDetails.Item(13, intRow).Value)
                                        dblDiaCostAvg = dblDiaCost
                                    End If
                                End If
                            Else
                                If Mid(flxDetails.Item(10, intRow).Value, 1, 1) = "X" Then
                                    dblInValue = CDbl(flxDetails.Item(2, intRow).Value) * dblStonePriceOld
                                    dblWtAvgNew = dblInValue / CDbl(flxDetails.Item(4, intRow).Value)
                                Else
                                    dblWtAvgNew = CDbl(flxDetails.Item(13, intRow).Value)
                                    dblDiaCostAvg = dblDiaCost
                                End If
                            End If
                        Else
                            If Mid(flxDetails.Item(10, intRow).Value, 1, 1) = "X" Then
                                dblInValue = CDbl(flxDetails.Item(2, intRow).Value) * dblStonePriceOld
                                dblWtAvgNew = dblInValue / CDbl(flxDetails.Item(4, intRow).Value)
                            Else
                                dblWtAvgNew = CDbl(flxDetails.Item(13, intRow).Value)
                                dblDiaCostAvg = dblDiaCost
                            End If
                        End If
                        rsComSql = Nothing

                        dblWtAvgNew = Math.Round(dblWtAvgNew, 2)
                        dblDiaCostAvg = Math.Round(dblDiaCostAvg, 2)

                        dblTotalValueAct = (dblTotCts * dblWtAvgOld) + dblInValue
                        dblTotalValueSys = dblWtAvgNew * (CDbl(flxDetails.Item(4, intRow).Value) + dblTotCts)
                    End If

                    'Insert Assortment In
                    AdoCN.Execute("INSERT INTO tblAssortDetails(ImportNo,OrgAssort,Assortment,AssortBox,DDate,InPcs,InCts,AvgCost,BaseCost,CurCost,RejInPcs,RejInCts,RejAvgCost,RejBaseCost,RejCurCost,Type) " & _
                                  "VALUES(" & CDbl(flxDetails.Item(9, intRow).Value) & ",'" & flxDetails.Item(6, intRow).Value & "','" & flxDetails.Item(5, intRow).Value & "','" & flxDetails.Item(12, intRow).Value & "','" & Format(Date.Now, "MM/dd/yyyy") & "'," & _
                                    "" & CInt(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & "," & CDbl(flxDetails.Item(7, intRow).Value) & "," & CDbl(flxDetails.Item(7, intRow).Value) & "," & CDbl(flxDetails.Item(7, intRow).Value) & ",0,0,0,0,0,'R')")

                    'Insert Assort Origin
                    AdoCN.Execute("INSERT INTO tblAssortOrigin(Assortment,Origin,SupParNo,Pcs,EntDate) VALUES('" & flxDetails.Item(5, intRow).Value & "','" & flxDetails.Item(16, intRow).Value & "','" & flxDetails.Item(15, intRow).Value & "'," & CInt(flxDetails.Item(2, intRow).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "')")

                    'Update Trf = 1
                    AdoCN.Execute("UPDATE tblMixRejects SET OK = 1,VerifyDate = '" & Format(Date.Now, "MM/dd/yyyy") & "',NewAvgPrice = " & dblWtAvgOld & ",OldAvgPrice = " & dblAvgPriceOld & " WHERE ID = " & Val(flxDetails.Item(12, intRow).Value) & "")

                    'Update Weighted Average
                    AdoCN.Execute("UPDATE tblAssortList SET AvgCost = " & dblWtAvgNew & ",DiaCost = " & dblDiaCostAvg & " WHERE Assortment = '" & flxDetails.Item(5, intRow).Value & "'")

                    'Update List Price - SRW,SSW
                    If Mid(flxDetails.Item(5, intRow).Value, 1, 1) = "X" Then
                        AdoCN.Execute("UPDATE tblAssortList SET MarketPrice = " & dblListPriceNew & ",AvgStonePrice = " & dblStoneAvg & " WHERE Assortment = '" & flxDetails.Item(5, intRow).Value & "'")
                    End If

                    'Update List Price - AREJ
                    If Mid(flxDetails.Item(5, intRow).Value, 1, 4) = "AREJ" Then
                        AdoCN.Execute("UPDATE tblAssortList SET MarketPrice = " & dblWtAvgNew & ", CurrentCost = " & dblWtAvgNew & " WHERE Assortment = '" & flxDetails.Item(5, intRow).Value & "'")
                    End If

                    If strNewType = "A" Then
                        If Mid(flxDetails.Item(5, intRow).Value, 1, 2) = "AS" Then
                            AdoCN.Execute("UPDATE tblAssortList SET MarketPrice = " & dblWtAvgNew & ", CurrentCost = " & dblWtAvgNew & " WHERE Assortment = '" & flxDetails.Item(5, intRow).Value & "'")
                        End If
                    End If
                    
                    If Math.Round(dblTotalValueAct, 6) <> Math.Round(dblTotalValueSys, 6) Then
                        AdoCN.Execute("INSERT INTO tblAssortBank(Assortment, DDate, InPcs, InCts, Value, Type) " & _
                                      "VALUES('" & flxDetails.Item(5, intRow).Value & "','" & Format(Date.Now, "MM/dd/yyyy") & "'," & CInt(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & "," & Math.Round(dblTotalValueAct - dblTotalValueSys, 6) & ",'R')")
                    End If
                End If
            Next
            If blnSave = True Then
                MsgBox("Added to the Stock Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Else
                MsgBox("No Records to Add", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            'Load_Rejects()
            flxDetails.Rows.Clear()
            txtPcs.Text = ""
            txtCts.Text = ""
        End If
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        AddToStock()
    End Sub

    Private Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        Load_Rejects()
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(11, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(11, intRow).Value = False
            Next
        End If

        txtPcs.Text = CalTotalPcs(flxDetails, 2)
        txtCts.Text = CalTotalCts(flxDetails, 3)
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(11).EditedFormattedValue = True Then
                CalTotalPcs = CalTotalPcs + Val(flxSample.Item(intCalRow, intRow).Value)
            End If
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(11).EditedFormattedValue = True Then
                CalTotalCts = CalTotalCts + Val(flxSample.Item(intCalRow, intRow).Value)
            End If
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 11 Then
            txtPcs.Text = CalTotalPcs(flxDetails, 2)
            txtCts.Text = CalTotalCts(flxDetails, 3)
        End If
    End Sub

    Private Sub Select_Assortments()
        Dim intRow As Integer

        If cmbAssortType.Text <> "" Then
            For intRow = 0 To flxDetails.RowCount - 1
                If cmbAssortType.Text = Mid(flxDetails.Item(5, intRow).Value, 1, 3) Then
                    flxDetails.Item(11, intRow).Value = True
                Else
                    flxDetails.Item(11, intRow).Value = False
                End If
            Next
            txtPcs.Text = CalTotalPcs(flxDetails, 2)
            txtCts.Text = CalTotalCts(flxDetails, 3)
        Else
            MsgBox("Invalid Category", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            cmbAssortType.Focus()
        End If

    End Sub

    Private Sub cmdSelect_Click(sender As Object, e As EventArgs) Handles cmdSelect.Click
        Select_Assortments()
    End Sub

    Private Sub cmdSelect2_Click(sender As Object, e As EventArgs) Handles cmdSelect2.Click
        Select_Assortments2()
    End Sub

    Private Sub Select_Assortments2()
        Dim intRow As Integer

        If cmbAssortCode.Text <> "" Then
            If Not IsNumeric(cmbAssortCode.Text) = True Then Exit Sub

            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(11, intRow).Value = False
            Next

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblAssortCode WHERE Category = '" & CInt(cmbAssortCode.Text) & "' ORDER BY AssortCode", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    For intRow = 0 To flxDetails.RowCount - 1
                        If rsComSql.Fields("AssortCode").Value = Mid(flxDetails.Item(5, intRow).Value, 1, 3) Then
                            flxDetails.Item(11, intRow).Value = True
                        End If
                    Next

                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing

            txtPcs.Text = CalTotalPcs(flxDetails, 2)
            txtCts.Text = CalTotalCts(flxDetails, 3)
        Else
            MsgBox("Invalid Category", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            cmbAssortCode.Focus()
        End If

    End Sub
End Class