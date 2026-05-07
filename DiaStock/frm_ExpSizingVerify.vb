
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_ExpSizingVerify

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

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

    Private Sub Load_SizingDetails()
        Dim strOrgAssort As String
        Dim strSupParNo As String
        Dim dblInvPrice As Double
        Dim dblBasePrice As Double
        Dim dblSelectPrice As Double
        Dim dblPerStonePrice As Double
        Dim dblMarketPrice As Double

        Dim dblImportNo As Double
        Dim dblRecord As Double

        Dim strCategory As String
        Dim strPriceType As String
        Dim strOrigin As String
        Dim intStatus As Integer

        Dim blnOld As Boolean

        flxDetails.Rows.Clear()
        If chkPurchased.Checked = True Then
            intStatus = 4
        Else
            intStatus = 5
        End If
        rsComSql = New ADODB.Recordset
        If txtParcel.Text = "" Then
            rsComSql.Open("SELECT * FROM tblExpSizingTypes WHERE OK = " & intStatus & " AND Sec = 1 " & _
                          "ORDER BY Department, ParNo, PktNo, ReturnType", AdoCN, 1, 1)
        Else
            rsComSql.Open("SELECT * FROM tblExpSizingTypes WHERE ParNo = '" & txtParcel.Text & "' AND OK = " & intStatus & " AND Sec = 1 " & _
                          "ORDER BY Department, ParNo, PktNo, ReturnType", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            ExpProgress.Value = 0
            ExpProgress.Text = "Please wait ....."
            ExpProgress.Visible = True
            ExpProgress.Maximum = rsComSql.RecordCount
            dblRecord = 0

            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                strOrgAssort = ""
                strSupParNo = ""
                dblInvPrice = 0
                dblBasePrice = 0
                dblSelectPrice = 0
                dblPerStonePrice = 0
                dblMarketPrice = 0
                strPriceType = ""
                strOrigin = "Niru Polish"

                blnOld = False
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT ParNo FROM tblExpSizingPlan WHERE ParNo = '" & rsComSql.Fields("ParNo").Value & "' AND Department = '" & rsComSql.Fields("Department").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    blnOld = True
                Else
                    blnOld = False
                End If
                rsComSql_1 = Nothing

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblParcel WHERE Depart = '" & rsComSql.Fields("Department").Value & "' AND GrpParNo = '" & rsComSql.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strOrgAssort = rsComSql_1.Fields("Assortment").Value
                    dblInvPrice = rsComSql_1.Fields("ItemCost").Value
                    strSupParNo = rsComSql_1.Fields("OrigParcelNo").Value
                End If
                rsComSql_1 = Nothing

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & rsComSql.Fields("ReturnType").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    dblBasePrice = rsComSql_1.Fields("MarketPrice").Value
                    dblMarketPrice = rsComSql_1.Fields("MarketPrice").Value
                    dblPerStonePrice = rsComSql_1.Fields("StonePrice").Value
                Else
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT ListCost FROM tblDCLPermanents WHERE ItemName = '" & rsComSql.Fields("ReturnType").Value & "'", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        dblBasePrice = rsComSql_2.Fields("ListCost").Value
                        dblMarketPrice = rsComSql_2.Fields("ListCost").Value
                    End If
                    rsComSql_2 = Nothing
                End If
                rsComSql_1 = Nothing

                rsComSql_1 = New ADODB.Recordset
                If Len(rsComSql.Fields("ParNo").Value) <> 12 Then
                    rsComSql_1.Open("SELECT * FROM tblImport WHERE DCLParcelNo = '" & Mid(rsComSql.Fields("ParNo").Value, 1, 6) & "'", AdoCN, 1, 1)
                Else
                    rsComSql_1.Open("SELECT * FROM tblImport WHERE DCLParcelNo = '" & Mid(rsComSql.Fields("ParNo").Value, 1, 8) & "'", AdoCN, 1, 1)
                End If
                If rsComSql_1.RecordCount Then
                    If strOrgAssort = "" Then
                        strOrgAssort = rsComSql_1.Fields("AssortmentNo").Value
                    End If
                    If strSupParNo = "" Then
                        strSupParNo = rsComSql_1.Fields("SupParcelNo").Value
                    End If
                    If dblInvPrice = 0 Then
                        dblInvPrice = rsComSql_1.Fields("ItemCost").Value
                    End If
                    dblImportNo = rsComSql_1.Fields("ImportNo").Value
                    dblSelectPrice = rsComSql_1.Fields("SelectCost").Value
                    strPriceType = rsComSql_1.Fields("PriceType").Value
                    strOrigin = rsComSql_1.Fields("Origin").Value
                Else
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT * FROM tblImport WHERE SupParcelNo = '" & rsComSql.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        If strOrgAssort = "" Then
                            strOrgAssort = rsComSql_2.Fields("AssortmentNo").Value
                        End If
                        If strSupParNo = "" Then
                            strSupParNo = rsComSql_2.Fields("SupParcelNo").Value
                        End If
                        If dblInvPrice = 0 Then
                            dblInvPrice = rsComSql_2.Fields("ItemCost").Value
                        End If
                        dblImportNo = rsComSql_2.Fields("ImportNo").Value
                        dblSelectPrice = rsComSql_2.Fields("SelectCost").Value
                        strPriceType = rsComSql_2.Fields("PriceType").Value
                        strOrigin = rsComSql_2.Fields("Origin").Value
                    Else
                        rsComSql_3 = New ADODB.Recordset
                        rsComSql_3.Open("SELECT * FROM tblDep_Trf WHERE DCLParcelNo = '" & rsComSql.Fields("ParNo").Value & "' AND Department = 'Mix'", AdoCN, 1, 1)
                        If rsComSql_3.RecordCount Then
                            If strOrgAssort = "" Then
                                strOrgAssort = rsComSql_3.Fields("AssortmentNo").Value
                            End If
                            If strSupParNo = "" Then
                                strSupParNo = rsComSql_3.Fields("SupParcelNo").Value
                            End If
                            If dblInvPrice = 0 Then
                                dblInvPrice = rsComSql_3.Fields("ItemCost").Value
                            End If
                        End If
                        rsComSql_3 = Nothing
                    End If
                    rsComSql_2 = Nothing
                End If
                rsComSql_1 = Nothing

                'If strRight(rsComSql.Fields("ParNo").Value, 1) = "S" Then
                '    dblBasePrice = rsComSql.Fields("BasePrice").Value
                'ElseIf strRight(rsComSql.Fields("ParNo").Value, 1) = "C" Then
                '    If blnOld = True Then
                '        If Mid(rsComSql.Fields("ReturnType").Value, 1, 1) = "S" Then
                '            dblBasePrice = Math.Round((rsComSql.Fields("Pcs").Value * dblPerStonePrice) / rsComSql.Fields("Cts").Value, 2)
                '        Else
                '            dblBasePrice = rsComSql.Fields("BasePrice").Value
                '        End If
                '    Else
                '        If Mid(rsComSql.Fields("ReturnType").Value, 1, 1) = "S" And Mid(rsComSql.Fields("ReturnType").Value, 1, 3) <> "SRW" Then
                '            dblBasePrice = rsComSql.Fields("BasePrice").Value
                '        Else
                '            If Mid(rsComSql.Fields("ReturnType").Value, 1, 1) = "S" Then
                '                dblBasePrice = Math.Round((rsComSql.Fields("Pcs").Value * dblPerStonePrice) / rsComSql.Fields("Cts").Value, 2)
                '            Else
                '                dblBasePrice = rsComSql.Fields("BasePrice").Value
                '            End If
                '        End If
                '    End If
                'Else
                '    If Mid(rsComSql.Fields("ReturnType").Value, 1, 1) = "S" Then
                '        'dblBasePrice = Math.Round((rsComSql.Fields("Pcs").Value * dblPerStonePrice) / rsComSql.Fields("Cts").Value, 2)
                '        dblBasePrice = rsComSql.Fields("BasePrice").Value
                '    Else
                '        dblBasePrice = rsComSql.Fields("BasePrice").Value
                '    End If

                '    If dblBasePrice = 0 Then
                '        dblBasePrice = dblInvPrice
                '    End If
                'End If

                'If Mid(rsComSql.Fields("ReturnType").Value, 1, 3) = "SRW" And strRight(rsComSql.Fields("ReturnType").Value, 1) = "U" Then
                '    dblBasePrice = rsComSql.Fields("BasePrice").Value
                'End If

                If Mid(rsComSql.Fields("ReturnType").Value, 1, 1) = "S" Or Mid(rsComSql.Fields("ReturnType").Value, 1, 1) = "R" Or Mid(rsComSql.Fields("ReturnType").Value, 7, 1) = "R" Or Mid(rsComSql.Fields("ReturnType").Value, 7, 1) = "S" Then
                    dblBasePrice = rsComSql.Fields("BasePrice").Value
                Else
                    dblBasePrice = dblBasePrice
                End If

                If dblMarketPrice = 0 Then
                    dblMarketPrice = dblBasePrice
                End If

                strCategory = ""
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT Category, PriceType, SelectCost, Origin FROM tblImport WHERE (SupParcelNo = '" & strSupParNo & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strCategory = rsComSql_1.Fields("Category").Value & ""
                    If strPriceType = "" Then
                        strPriceType = rsComSql_1.Fields("PriceType").Value
                    End If
                    If dblSelectPrice = 0 Then
                        dblSelectPrice = rsComSql_1.Fields("SelectCost").Value
                    End If
                    strOrigin = rsComSql_1.Fields("Origin").Value
                End If
                rsComSql_1 = Nothing

                If strPriceType = "" Then
                    strPriceType = "List"
                End If
                If strSupParNo = "" Then
                    strSupParNo = rsComSql.Fields("ParNo").Value
                End If
                If strCategory = "" Then
                    strCategory = "Purchased"
                End If

                flxDetails.Rows.Add(rsComSql.Fields("Department").Value,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    strOrgAssort,
                                    rsComSql.Fields("ReturnType").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    Format(rsComSql.Fields("Cts").Value, "#0.000"),
                                    Format(rsComSql.Fields("Cts").Value + rsComSql.Fields("DiffCts").Value, "#0.000"),
                                    Format(dblBasePrice, "#0.00"),
                                    Format(dblInvPrice, "#0.00"),
                                    Format(dblSelectPrice, "#0.00"),
                                    dblImportNo,
                                    False,
                                    strSupParNo,
                                    strCategory,
                                    strPriceType,
                                    strOrigin,
                                    rsComSql.Fields("ID").Value,
                                    Format(dblBasePrice, "#0.00"),
                                    Format(rsComSql.Fields("EstCts").Value, "#0.000"),
                                    rsComSql.Fields("DiaCost").Value,
                                    strRight(rsComSql.Fields("ParNo").Value, 1))

                rsComSql.MoveNext()
                dblRecord = dblRecord + 1
                ExpProgress.Value = dblRecord
                Application.DoEvents()
            End While
        Else
            MsgBox("No Records to Verify", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing

        ExpProgress.Visible = False

    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        Load_SizingDetails()
        GetPackNo()
        chkSelect.Checked = False
        txtTotalPcs.Text = "0"
        txtTotalCts.Text = "0"
        txtTotalCts2.Text = "0"
        cmbAssortType.Text = ""
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

    Private Function CalTotalPcs() As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Rows(intRow).Cells(12).EditedFormattedValue = True Then
                CalTotalPcs = CalTotalPcs + CInt(flxDetails.Item(5, intRow).Value)
            End If
        Next
        Return CalTotalPcs
    End Function

    Private Function CalTotalCts() As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Rows(intRow).Cells(12).EditedFormattedValue = True Then
                CalTotalCts = CalTotalCts + CDbl(flxDetails.Item(6, intRow).Value)
            End If
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
        Return CalTotalCts
    End Function

    Private Function CalTotalCts2() As Double
        Dim intRow As Integer

        CalTotalCts2 = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Rows(intRow).Cells(12).EditedFormattedValue = True Then
                CalTotalCts2 = CalTotalCts2 + CDbl(flxDetails.Item(7, intRow).Value)
            End If
        Next
        CalTotalCts2 = Math.Round(CalTotalCts2, 3)
        Return CalTotalCts2
    End Function

    Private Sub CalTotals()
        Dim intRow As Integer

        Dim dblTotPcs As Double
        Dim dblTotCts As Double
        Dim dblTotACts As Double

        dblTotPcs = 0
        dblTotCts = 0
        dblTotACts = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Rows(intRow).Cells(12).EditedFormattedValue = True Then
                dblTotPcs = dblTotPcs + CDbl(flxDetails.Item(5, intRow).Value)
                dblTotCts = dblTotCts + CDbl(flxDetails.Item(6, intRow).Value)
                dblTotACts = dblTotACts + CDbl(flxDetails.Item(7, intRow).Value)
            End If
        Next

        txtTotalPcs.Text = dblTotPcs
        txtTotalCts.Text = dblTotCts
        txtTotalCts2.Text = dblTotACts
    End Sub

    Private Sub cmdCalc_Click(sender As Object, e As EventArgs) Handles cmdCalc.Click
        CalTotals()

        'txtTotalPcs.Text = CalTotalPcs()
        'txtTotalCts.Text = CalTotalCts()
        'txtTotalCts2.Text = CalTotalCts2()
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(12, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(12, intRow).Value = False
            Next
        End If
        CalTotals()

        'txtTotalPcs.Text = CalTotalPcs()
        'txtTotalCts.Text = CalTotalCts()
        'txtTotalCts2.Text = CalTotalCts2()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub AddToStock()
        Dim intRow As Integer
        Dim blnSave As Boolean
        Dim strSupParNo As String
        Dim dblListPrice As Double
        Dim dblListPriceNew As Double
        Dim dblAdjCost As Double
        Dim dblWtAvgOld As Double
        Dim dblWtAvgNew As Double
        Dim strType As String
        Dim strNewType As String
        Dim strOrgAssort As String
        Dim dblInPrice As Double
        Dim dblStonePrice As Double
        Dim dblStonePriceAvg As Double
        Dim dblTotPcs As Double
        Dim dblTotCts As Double
        Dim dblStoneAvg As Double
        Dim dblTotalValueAct As Double
        Dim dblTotalValueSys As Double
        Dim dblDiaCost As Double
        Dim dblDiaCostAvg As Double
        Dim strPriceType As String

        blnSave = False
        PBResponse = MsgBox("Are you sure to add to stock?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            

            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(12, intRow).Value = True Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT Assortment FROM dbo.tblAssortList WHERE Assortment = '" & flxDetails.Item(4, intRow).Value & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        MsgBox("Invalid APCU Assortment - " & flxDetails.Item(4, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql = Nothing
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT Assortment FROM dbo.tblAssortBlock WHERE Assortment = '" & flxDetails.Item(4, intRow).Value & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        MsgBox("Assortment is Blocked - " & flxDetails.Item(4, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql = Nothing
                    If Len(flxDetails.Item(7, intRow).Value) = 0 Then
                        MsgBox("Invalid Act Cts " & flxDetails.Item(4, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If Not IsNumeric(flxDetails.Item(7, intRow).Value) = True Then
                        MsgBox("Invalid Act Cts " & flxDetails.Item(4, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If CDbl(flxDetails.Item(7, intRow).Value) <= 0 Then
                        MsgBox("Invalid Act Cts " & flxDetails.Item(4, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If flxDetails.Item(14, intRow).Value <> "Purchased" Then
                        MsgBox("Invalid Import Category " & flxDetails.Item(4, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If
                
            Next

            CalTotals()
            'txtTotalPcs.Text = CalTotalPcs()
            'txtTotalCts.Text = CalTotalCts()
            'txtTotalCts2.Text = CalTotalCts2()

            'If CDbl(txtTotalCts2.Text) < CDbl(txtTotalCts.Text) Then
            '    MsgBox("Total Cts is invalid", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '    Exit Sub
            'End If

            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = flxDetails.Rows.Count

            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(12, intRow).Value = True Then
                    blnSave = True

                    dblListPrice = 0
                    dblListPriceNew = 0
                    dblStonePrice = 0
                    dblStonePriceAvg = 0
                    dblDiaCost = 0
                    dblDiaCostAvg = 0
                    dblInPrice = CDbl(flxDetails.Item(8, intRow).Value)

                    strPriceType = flxDetails.Item(15, intRow).Value

                    Select Case strPriceType
                        Case "List"
                            dblInPrice = CDbl(flxDetails.Item(8, intRow).Value)
                        Case "Import"
                            dblInPrice = CDbl(flxDetails.Item(9, intRow).Value)
                        Case "Select"
                            dblInPrice = CDbl(flxDetails.Item(10, intRow).Value)
                    End Select

                    dblInPrice = CDbl(flxDetails.Item(20, intRow).Value)
                    dblInPrice = Math.Round(dblInPrice, 2)

                    strSupParNo = ""
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT OrigParcelNo FROM dbo.tblParcel WHERE Depart = '" & flxDetails.Item(0, intRow).Value & "' AND GrpParNo = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        strSupParNo = rsComSql.Fields("OrigParcelNo").Value
                    End If
                    rsComSql = Nothing

                    If strSupParNo = "" Then
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT OrigParcelNo FROM dbo.tblParcel WHERE Depart = '" & flxDetails.Item(0, intRow).Value & "' AND OrigParcelNo = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            strSupParNo = rsComSql.Fields("OrigParcelNo").Value
                        End If
                        rsComSql = Nothing
                    End If

                    If strSupParNo = "" Then
                        strSupParNo = flxDetails.Item(1, intRow).Value
                    End If

                    dblAdjCost = CDbl(flxDetails.Item(9, intRow).Value)

                    'Calculate Weighted Average
                    dblWtAvgOld = 0
                    strNewType = ""
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT MarketPrice,AvgCost,StonePrice,AvgStonePrice,NewType,DiaCost FROM dbo.tblAssortList WHERE Assortment = '" & flxDetails.Item(4, intRow).Value & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        dblListPrice = rsComSql.Fields("MarketPrice").Value
                        dblWtAvgOld = rsComSql.Fields("AvgCost").Value
                        dblStonePrice = rsComSql.Fields("StonePrice").Value
                        dblStonePriceAvg = rsComSql.Fields("AvgStonePrice").Value
                        strNewType = rsComSql.Fields("NewType").Value
                        dblDiaCost = rsComSql.Fields("DiaCost").Value
                    End If
                    rsComSql = Nothing

                    dblWtAvgNew = 0
                    dblStoneAvg = 0
                    dblTotPcs = 0
                    dblTotCts = 0

                    dblTotalValueAct = 0
                    dblTotalValueSys = 0

                    'SRW & SSW & SRR
                    If Mid(flxDetails.Item(4, intRow).Value, 1, 1) = "S" Then
                        dblWtAvgNew = 0
                        dblStoneAvg = 0

                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT BoxPcs, ProdPcs, BoxCts, ProdCts, BankCts " & _
                                      "FROM dbo.VW_MixPCUStock2020 " & _
                                      "WHERE Assortment = '" & flxDetails.Item(4, intRow).Value & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            If Not IsDBNull(rsComSql.Fields("BoxCts").Value) Then
                                dblTotPcs = rsComSql.Fields("BoxPcs").Value + rsComSql.Fields("ProdPcs").Value
                                dblTotCts = Math.Round(rsComSql.Fields("BoxCts").Value + IIf(rsComSql.Fields("ProdCts").Value > 0, rsComSql.Fields("ProdCts").Value, 0) + rsComSql.Fields("BankCts").Value, 3)

                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT RetPcs, RetCts FROM dbo.VW_MixPktRejExpNewY WHERE Assortment = '" & flxDetails.Item(4, intRow).Value & "'", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount Then
                                    dblTotPcs = dblTotPcs + rsComSql_1.Fields("RetPcs").Value
                                    dblTotCts = dblTotCts + rsComSql_1.Fields("RetCts").Value
                                End If
                                rsComSql_1 = Nothing

                                dblTotCts = Math.Round(dblTotCts, 3)

                                dblInPrice = CDbl(flxDetails.Item(18, intRow).Value)

                                If dblTotCts < 0 Then
                                    dblTotCts = 0
                                End If

                                'dblWtAvgNew = (((dblTotCts * dblWtAvgOld) + (dblStonePrice * CDbl(flxDetails.Item(5, intRow).Value))) / (CDbl(flxDetails.Item(7, intRow).Value) + dblTotCts))
                                dblWtAvgNew = (((dblTotCts * dblWtAvgOld) + (CDbl(flxDetails.Item(18, intRow).Value) * CDbl(flxDetails.Item(6, intRow).Value))) / (CDbl(flxDetails.Item(7, intRow).Value) + dblTotCts))

                                dblListPriceNew = (((dblTotCts * dblListPrice) + (dblStonePrice * CDbl(flxDetails.Item(5, intRow).Value))) / (CDbl(flxDetails.Item(7, intRow).Value) + dblTotCts))

                                dblStoneAvg = ((dblTotPcs * dblStonePriceAvg) + (dblStonePrice * CDbl(flxDetails.Item(5, intRow).Value))) / (dblTotPcs + CDbl(flxDetails.Item(5, intRow).Value))

                                dblDiaCostAvg = (((dblTotCts * dblDiaCost) + (CDbl(flxDetails.Item(20, intRow).Value) * CDbl(flxDetails.Item(6, intRow).Value))) / (CDbl(flxDetails.Item(7, intRow).Value) + dblTotCts))
                            Else
                                'dblWtAvgNew = (dblStonePrice * CDbl(flxDetails.Item(5, intRow).Value)) / CDbl(flxDetails.Item(7, intRow).Value)
                                dblWtAvgNew = (CDbl(flxDetails.Item(18, intRow).Value) * CDbl(flxDetails.Item(6, intRow).Value)) / CDbl(flxDetails.Item(7, intRow).Value)
                                dblListPriceNew = (dblStonePrice * CDbl(flxDetails.Item(5, intRow).Value)) / CDbl(flxDetails.Item(7, intRow).Value)
                                dblStoneAvg = dblStonePrice
                                dblDiaCostAvg = (CDbl(flxDetails.Item(20, intRow).Value) * CDbl(flxDetails.Item(6, intRow).Value)) / CDbl(flxDetails.Item(7, intRow).Value)
                            End If
                        End If
                        rsComSql = Nothing

                        dblWtAvgNew = Math.Round(dblWtAvgNew, 2)
                        dblListPriceNew = Math.Round(dblListPriceNew, 2)
                        dblStoneAvg = Math.Round(dblStoneAvg, 2)

                        dblTotalValueAct = (dblTotPcs * dblStonePriceAvg) + (dblStonePrice * CDbl(flxDetails.Item(5, intRow).Value))
                        dblTotalValueSys = dblStoneAvg * (dblTotPcs + CDbl(flxDetails.Item(5, intRow).Value))
                    Else
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT BoxCts, ProdCts, BankCts " & _
                                      "FROM dbo.VW_MixPCUStock2020 " & _
                                      "WHERE Assortment = '" & flxDetails.Item(4, intRow).Value & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            If Not IsDBNull(rsComSql.Fields("BoxCts").Value) Then
                                dblTotCts = Math.Round(rsComSql.Fields("BoxCts").Value + IIf(rsComSql.Fields("ProdCts").Value > 0, rsComSql.Fields("ProdCts").Value, 0) + rsComSql.Fields("BankCts").Value, 3)

                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT RetCts FROM dbo.VW_MixPktRejExpNewY WHERE Assortment = '" & flxDetails.Item(4, intRow).Value & "'", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount Then
                                    dblTotCts = dblTotCts + rsComSql_1.Fields("RetCts").Value
                                End If
                                rsComSql_1 = Nothing

                                If dblTotCts < 0 Then
                                    dblTotCts = 0
                                End If

                                If dblTotCts > 0 Then
                                    dblWtAvgNew = (((dblTotCts * dblWtAvgOld) + (dblInPrice * CDbl(flxDetails.Item(6, intRow).Value))) / (CDbl(flxDetails.Item(7, intRow).Value) + dblTotCts))
                                    dblDiaCostAvg = (((dblTotCts * dblDiaCost) + (CDbl(flxDetails.Item(20, intRow).Value) * CDbl(flxDetails.Item(6, intRow).Value))) / (CDbl(flxDetails.Item(7, intRow).Value) + dblTotCts))
                                Else
                                    dblWtAvgNew = (dblInPrice * CDbl(flxDetails.Item(6, intRow).Value)) / CDbl(flxDetails.Item(7, intRow).Value)
                                    dblDiaCostAvg = (CDbl(flxDetails.Item(20, intRow).Value) * CDbl(flxDetails.Item(6, intRow).Value)) / CDbl(flxDetails.Item(7, intRow).Value)
                                End If
                            Else
                                dblWtAvgNew = (dblInPrice * CDbl(flxDetails.Item(6, intRow).Value)) / CDbl(flxDetails.Item(7, intRow).Value)
                                dblDiaCostAvg = (CDbl(flxDetails.Item(20, intRow).Value) * CDbl(flxDetails.Item(6, intRow).Value)) / CDbl(flxDetails.Item(7, intRow).Value)
                            End If
                        Else
                            dblWtAvgNew = (dblInPrice * CDbl(flxDetails.Item(6, intRow).Value)) / CDbl(flxDetails.Item(7, intRow).Value)
                            dblDiaCostAvg = (CDbl(flxDetails.Item(20, intRow).Value) * CDbl(flxDetails.Item(6, intRow).Value)) / CDbl(flxDetails.Item(7, intRow).Value)
                        End If
                        rsComSql = Nothing

                        dblWtAvgNew = Math.Round(dblWtAvgNew, 2)

                        dblTotalValueAct = (dblTotCts * dblWtAvgOld) + (dblInPrice * CDbl(flxDetails.Item(6, intRow).Value))
                        dblTotalValueSys = dblWtAvgNew * (dblTotCts + CDbl(flxDetails.Item(7, intRow).Value))
                    End If

                    dblAdjCost = Math.Round(dblAdjCost, 2)
                    dblWtAvgNew = Math.Round(dblWtAvgNew, 2)
                    dblDiaCostAvg = Math.Round(dblDiaCostAvg, 2)

                    If flxDetails.Item(0, intRow).Value = "Mix" Or flxDetails.Item(0, intRow).Value = "KIT Box" Then
                        strType = "A"
                    Else
                        strType = "T"
                    End If

                    If Mid(flxDetails.Item(4, intRow).Value, 1, 2) = "VM" Then
                        strOrgAssort = "VPCU"
                    Else
                        strOrgAssort = "APCU"
                    End If

                    AdoCN.Execute("INSERT INTO dbo.tblAssortDetails(ImportNo, OrgAssort, Assortment, AssortBox, DDate, InPcs, InCts, AvgCost, BaseCost, CurCost, RejInPcs, RejInCts, RejAvgCost, RejBaseCost, RejCurCost,Type) " & _
                                  "VALUES(" & CDbl(flxDetails.Item(11, intRow).Value) & ",'" & strOrgAssort & "','" & flxDetails.Item(4, intRow).Value & "','" & strSupParNo & "','" & Format(Date.Now, "MM/dd/yyyy") & "'," & _
                                    "" & CInt(flxDetails.Item(5, intRow).Value) & "," & CDbl(flxDetails.Item(7, intRow).Value) & "," & dblWtAvgNew & "," & CDbl(flxDetails.Item(8, intRow).Value) & "," & CDbl(flxDetails.Item(9, intRow).Value) & "," & _
                                    "0,0,0,0,0,'" & strType & "')")

                    AdoCN.Execute("INSERT INTO dbo.tblExpStock(Department,ParNo,PktNo,OrgAssort,Assortment,Pcs,Cts,BasePrice,InvPrice,AdjPrice,ImportNo,InPrice,SysCts) " & _
                                  "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(2, intRow).Value & "'," & _
                                    "'" & flxDetails.Item(3, intRow).Value & "','" & flxDetails.Item(4, intRow).Value & "'," & CInt(flxDetails.Item(5, intRow).Value) & "," & _
                                    "" & CDbl(flxDetails.Item(7, intRow).Value) & "," & CDbl(flxDetails.Item(18, intRow).Value) & "," & CDbl(flxDetails.Item(9, intRow).Value) & "," & _
                                    "" & CDbl(flxDetails.Item(20, intRow).Value) & "," & CDbl(flxDetails.Item(11, intRow).Value) & "," & dblInPrice & "," & CDbl(flxDetails.Item(6, intRow).Value) & ")")

                    AdoCN.Execute("UPDATE dbo.tblExpSizingTypes SET OK = 1, BasePrice = " & CDbl(flxDetails.Item(8, intRow).Value) & " " & _
                                  "WHERE ID = " & CDbl(flxDetails.Item(17, intRow).Value) & "")

                    AdoCN.Execute("UPDATE dbo.tblImport SET TrfPcs = TrfPcs + " & CInt(flxDetails.Item(5, intRow).Value) & ", TrfCts = TrfCts + " & CDbl(flxDetails.Item(7, intRow).Value) & " WHERE AssortmentNo = '" & flxDetails.Item(3, intRow).Value & "' AND SupParcelNo = '" & strSupParNo & "' AND SupplierRefNo NOT LIKE 'LCL%'")

                    'Update Weighted Average
                    AdoCN.Execute("UPDATE dbo.tblAssortList SET AvgCost = " & dblWtAvgNew & ",DiaCost = " & dblDiaCostAvg & " WHERE Assortment = '" & flxDetails.Item(4, intRow).Value & "'")

                    'Update List Price
                    If strNewType = "A" Then
                        If Mid(flxDetails.Item(4, intRow).Value, 1, 2) = "AS" Then
                            AdoCN.Execute("UPDATE dbo.tblAssortList SET MarketPrice = " & dblWtAvgNew & " WHERE Assortment = '" & flxDetails.Item(4, intRow).Value & "'")
                        End If
                    Else
                        'If Mid(flxDetails.Item(4, intRow).Value, 1, 4) = "AROY" Then
                        '    AdoCN.Execute("UPDATE dbo.tblAssortList SET MarketPrice = " & dblWtAvgNew & " WHERE Assortment = '" & flxDetails.Item(4, intRow).Value & "'")
                        'End If
                    End If

                    'Update List Price - SRW,SSW,SRR
                    If Mid(flxDetails.Item(4, intRow).Value, 1, 1) = "S" Then
                        AdoCN.Execute("UPDATE dbo.tblAssortList SET MarketPrice = " & dblListPriceNew & ",AvgStonePrice = " & dblStoneAvg & " WHERE Assortment = '" & flxDetails.Item(4, intRow).Value & "'")
                    End If

                    'Insert Assort Origin
                    AdoCN.Execute("INSERT INTO dbo.tblAssortOrigin(Assortment,Origin,SupParNo,Pcs,EntDate) VALUES('" & flxDetails.Item(4, intRow).Value & "','" & flxDetails.Item(16, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & CInt(flxDetails.Item(5, intRow).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "')")

                    If Mid(flxDetails.Item(4, intRow).Value, 1, 2) = "VM" Then
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT * FROM dbo.tblAssortList WHERE Assortment = '" & flxDetails.Item(4, intRow).Value & "' AND MarketPrice = 0", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            AdoCN.Execute("UPDATE dbo.tblAssortList SET MarketPrice = " & CDbl(flxDetails.Item(9, intRow).Value) & " WHERE Assortment = '" & flxDetails.Item(4, intRow).Value & "'")
                        End If
                        rsComSql = Nothing
                    End If

                    If Math.Round(dblTotalValueAct, 6) <> Math.Round(dblTotalValueSys, 6) Then
                        AdoCN.Execute("INSERT INTO dbo.tblAssortBank(Assortment, DDate, InPcs, InCts, Value, Type) " & _
                                      "VALUES('" & flxDetails.Item(4, intRow).Value & "','" & Format(Date.Now, "MM/dd/yyyy") & "'," & CInt(flxDetails.Item(5, intRow).Value) & "," & CDbl(flxDetails.Item(7, intRow).Value) & "," & Math.Round(dblTotalValueAct - dblTotalValueSys, 6) & ",'A')")
                    End If

                End If
                ExpProgress.Value = intRow + 1
                Application.DoEvents()
            Next
            ExpProgress.Visible = False

            If blnSave = True Then
                MsgBox("Added to the Stock Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Else
                MsgBox("No Records to Add", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If

            'Load_SizingDetails()
            flxDetails.Rows.Clear()
            txtTotalPcs.Text = ""
            txtTotalCts.Text = ""
            txtTotalCts2.Text = ""
            cmbAssortType.Text = ""
        End If
    End Sub

    Private Sub cmdTrf_Click(sender As Object, e As EventArgs) Handles cmdTrf.Click
        AddToStock()
    End Sub

    Private Sub AddToExport()
        Dim intRow As Integer
        Dim blnSave As Boolean

        blnSave = False
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            GetPackNo()
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(12, intRow).Value = True Then
                    blnSave = True

                    'AdoCN.Execute("INSERT INTO tblExpReExports(Department,ParNo,PktNo,OrgAssort,Assortment,Pcs,Cts,BasePrice,InvPrice,AdjPrice,ImportNo,OK,SupParNo,EstCts) " & _
                    '              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(2, intRow).Value & "'," & _
                    '                "'" & flxDetails.Item(3, intRow).Value & "','" & flxDetails.Item(4, intRow).Value & "'," & CInt(flxDetails.Item(5, intRow).Value) & "," & _
                    '                "" & CDbl(flxDetails.Item(6, intRow).Value) & "," & CDbl(flxDetails.Item(8, intRow).Value) & "," & CDbl(flxDetails.Item(9, intRow).Value) & "," & _
                    '                "" & CDbl(flxDetails.Item(10, intRow).Value) & "," & CDbl(flxDetails.Item(11, intRow).Value) & ",2,'" & flxDetails.Item(13, intRow).Value & "'," & _
                    '                "" & CDbl(flxDetails.Item(19, intRow).Value) & ")")

                    AdoCN.Execute("INSERT INTO tblExpReExports(Department,ParNo,PktNo,OrgAssort,Assortment,Pcs,Cts,BasePrice,InvPrice,AdjPrice,ImportNo,OK,SupParNo,EstCts,PackNo) " & _
                                  "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(2, intRow).Value & "'," & _
                                    "'" & flxDetails.Item(3, intRow).Value & "','" & flxDetails.Item(4, intRow).Value & "'," & CInt(flxDetails.Item(5, intRow).Value) & "," & _
                                    "" & CDbl(flxDetails.Item(6, intRow).Value) & "," & CDbl(flxDetails.Item(8, intRow).Value) & "," & CDbl(flxDetails.Item(9, intRow).Value) & "," & _
                                    "" & CDbl(flxDetails.Item(10, intRow).Value) & "," & CDbl(flxDetails.Item(11, intRow).Value) & ",3,'" & flxDetails.Item(13, intRow).Value & "'," & _
                                    "" & CDbl(flxDetails.Item(19, intRow).Value) & "," & CDbl(txtPackNo.Text) & ")")

                    AdoCN.Execute("UPDATE tblExpSizingTypes SET OK = 1, BasePrice = " & CDbl(flxDetails.Item(8, intRow).Value) & " " & _
                                  "WHERE ID = " & CDbl(flxDetails.Item(17, intRow).Value) & "")
                End If
            Next
            If blnSave = True Then
                MsgBox("Updated Successfully - " & txtPackNo.Text, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Else
                MsgBox("No Records to Add", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            Load_SizingDetails()
            GetPackNo()
            txtTotalPcs.Text = ""
            txtTotalCts.Text = ""
            txtTotalCts2.Text = ""
            cmbAssortType.Text = ""
        End If
    End Sub

    Private Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click
        AddToExport()
    End Sub

    Private Sub GetPackNo()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(PackNo) AS MaxNo FROM tblExpReExports", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If IsDBNull(rsComSql.Fields("MaxNo").Value) Then
                txtPackNo.Text = "1"
            Else
                txtPackNo.Text = rsComSql.Fields("MaxNo").Value + 1
            End If
        Else
            txtPackNo.Text = "1"
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Process()
        Dim intRow As Integer
        Dim dblTotBase As Double
        Dim dblTotCts As Double
        Dim dblInvPrice As Double
        Dim dblTotInv As Double
        Dim dblAdjValue As Double
        Dim dblAdjCost As Double
        Dim dblSelectCost As Double

        If txtParcel.Text <> "" Then
            dblTotBase = 0
            dblTotCts = 0
            dblInvPrice = 0
            dblSelectCost = 0
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(0, intRow).Value = "Mix" Then
                    dblTotBase = dblTotBase + (CDbl(flxDetails.Item(6, intRow).Value) * CDbl(flxDetails.Item(8, intRow).Value))
                    dblTotCts = dblTotCts + CDbl(flxDetails.Item(6, intRow).Value)
                    dblInvPrice = CDbl(flxDetails.Item(9, intRow).Value)
                Else
                    Exit Sub
                End If
            Next

            dblTotCts = Math.Round(dblTotCts, 3)
            dblTotInv = dblTotCts * dblInvPrice

            For intRow = 0 To flxDetails.Rows.Count - 1
                dblSelectCost = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SelectCost FROM tblImport WHERE SupParcelNo = '" & flxDetails.Item(13, intRow).Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    dblSelectCost = rsComSql.Fields("SelectCost").Value
                End If
                rsComSql = Nothing

                If dblSelectCost = 0 Then
                    dblAdjValue = (dblTotInv / dblTotBase) * (CDbl(flxDetails.Item(6, intRow).Value) * CDbl(flxDetails.Item(8, intRow).Value))
                    dblAdjCost = dblAdjValue / CDbl(flxDetails.Item(6, intRow).Value)
                    flxDetails.Item(10, intRow).Value = Format(Math.Round(dblAdjCost, 2), "#0.00")
                Else
                    flxDetails.Item(10, intRow).Value = Format(Math.Round(dblSelectCost, 2), "#0.00")
                End If
            Next
        End If
    End Sub

    Private Sub cmdProcess_Click(sender As Object, e As EventArgs) Handles cmdProcess.Click
        Process()
    End Sub

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 12 Then
            CalTotals()
            'txtTotalPcs.Text = CalTotalPcs()
            'txtTotalCts.Text = CalTotalCts()
            'txtTotalCts2.Text = CalTotalCts2()
        End If
    End Sub

    Private Sub Select_Assortments()
        Dim intRow As Integer

        If cmbAssortType.Text <> "" Then
            For intRow = 0 To flxDetails.RowCount - 1
                If cmbAssortType.Text = Mid(flxDetails.Item(4, intRow).Value, 1, 3) Then
                    flxDetails.Item(12, intRow).Value = True
                Else
                    flxDetails.Item(12, intRow).Value = False
                End If
            Next
            CalTotals()
            'txtTotalPcs.Text = CalTotalPcs()
            'txtTotalCts.Text = CalTotalCts()
            'txtTotalCts2.Text = CalTotalCts2()
        Else
            MsgBox("Invalid Category", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            cmbAssortType.Focus()
        End If

    End Sub

    Private Sub frm_ExpSizingVerify_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_AssortTypes()
        Load_AssortCodes()
    End Sub

    Private Sub cmdSelect_Click(sender As Object, e As EventArgs) Handles cmdSelect.Click
        Select_Assortments()
    End Sub

    Private Sub Select_Assortments2()
        Dim intRow As Integer

        If cmbAssortCode.Text <> "" Then
            If Not IsNumeric(cmbAssortCode.Text) = True Then Exit Sub

            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(12, intRow).Value = False
            Next

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblAssortCode WHERE Category = '" & CInt(cmbAssortCode.Text) & "' ORDER BY AssortCode", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    For intRow = 0 To flxDetails.RowCount - 1
                        If rsComSql.Fields("AssortCode").Value = Mid(flxDetails.Item(4, intRow).Value, 1, 3) Then
                            flxDetails.Item(12, intRow).Value = True
                        End If
                    Next

                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing

            CalTotals()
            'txtTotalPcs.Text = CalTotalPcs()
            'txtTotalCts.Text = CalTotalCts()
            'txtTotalCts2.Text = CalTotalCts2()
        Else
            MsgBox("Invalid Category", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            cmbAssortType.Focus()
        End If

    End Sub

    Private Sub cmdSelect2_Click(sender As Object, e As EventArgs) Handles cmdSelect2.Click
        Select_Assortments2()
    End Sub

    Private Sub Select_Group()
        Dim intRow As Integer

        If txtGroup.Text <> "" Then
            txtGroup.Text = UCase(txtGroup.Text)

            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(12, intRow).Value = False
            Next

            For intRow = 0 To flxDetails.RowCount - 1
                If txtGroup.Text = flxDetails.Item(21, intRow).Value Then
                    flxDetails.Item(12, intRow).Value = True
                End If
            Next

            CalTotals()
        Else
            MsgBox("Invalid Group", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            txtGroup.Focus()
        End If

    End Sub

    Private Sub cmdSelect3_Click(sender As Object, e As EventArgs) Handles cmdSelect3.Click
        Select_Group()
    End Sub
End Class