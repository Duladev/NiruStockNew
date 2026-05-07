
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_MixAssortModify
    Dim strFolderPath As String

    Private Sub txtAssortment_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAssortment.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtAssortment.Text = UCase(txtAssortment.Text)
            ClearFields()
            Load_AssortDetails(txtAssortment.Text)
        End If
    End Sub

    Private Sub Load_AssortDetails(ByVal strAssort As String)
        Dim intIssPcsT As Integer
        Dim dblIssCtsT As Double

        flxAssort.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * " & _
                      "FROM VW_MixAssortInOutNew " & _
                      "WHERE Assortment = '" & strAssort & "' AND InPcs - OutPcs > 0  " & _
                      "ORDER BY Assortment", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                intIssPcsT = 0
                dblIssCtsT = 0
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT SUM(BalPcs) AS TotPcs,SUM(BalCts) AS TotCts FROM VW_MixEmpBal WHERE Assortment = '" & strAssort & "'", AdoCN, 1, 1)
                If Not IsDBNull(rsComSql_1.Fields("TotPcs").Value) Then
                    intIssPcsT = rsComSql_1.Fields("TotPcs").Value
                    dblIssCtsT = rsComSql_1.Fields("TotCts").Value
                    dblIssCtsT = Math.Round(dblIssCtsT, 3)
                End If
                rsComSql_1 = Nothing

                flxAssort.Rows.Add(rsComSql.Fields("InPcs").Value - rsComSql.Fields("OutPcs").Value,
                                   Format(Math.Round(rsComSql.Fields("InCts").Value - rsComSql.Fields("OutCts").Value, 3), "#0.000"),
                                   intIssPcsT,
                                   Format(Math.Round(dblIssCtsT, 3), "#0.000"),
                                   rsComSql.Fields("InPcs").Value - rsComSql.Fields("OutPcs").Value - intIssPcsT,
                                   Format(Math.Round(rsComSql.Fields("InCts").Value - rsComSql.Fields("OutCts").Value - dblIssCtsT, 3), "#0.000"))



                txtBalPcs.Text = CInt(txtBalPcs.Text) + (rsComSql.Fields("InPcs").Value - rsComSql.Fields("OutPcs").Value - intIssPcsT)
                txtBalCts.Text = Format(Math.Round(CDbl(txtBalCts.Text) + Math.Round(rsComSql.Fields("InCts").Value - rsComSql.Fields("OutCts").Value - dblIssCtsT, 3), 3), "#0.000")
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtAvgCost.Text = "0"
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT AvgCost FROM dbo.tblAssortList WHERE Assortment = '" & strAssort & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            txtAvgCost.Text = rsComSql.Fields("AvgCost").Value
        End If
        rsComSql = Nothing

    End Sub

    Private Sub ClearFields()
        txtNewAssortment.Text = ""
        txtPcs.Text = "0"
        txtCts.Text = "0"
        txtBasePrice.Text = ""
        flxAssort.Rows.Clear()
        flxDetails.Rows.Clear()
        flxOrigin.Rows.Clear()
        cmdAdd.Enabled = True
        txtAvgCost.Text = ""
        txtAvgCost2.Text = ""
        txtBalPcs.Text = "0"
        txtBalCts.Text = "0"
        txtInPcs.Text = ""
        txtInCts.Text = ""
        txtOutPcs.Text = ""
        txtOutCts.Text = ""
        flxExtra.Rows.Clear()
    End Sub

    Private Sub frm_MixAssortModify_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        ClearFields()
    End Sub

    Private Sub flxAssort_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxAssort.CellClick
        txtInPcs.Text = flxAssort.Item(0, flxAssort.CurrentRow.Index).Value
        txtInCts.Text = flxAssort.Item(1, flxAssort.CurrentRow.Index).Value
        txtOutPcs.Text = flxAssort.Item(2, flxAssort.CurrentRow.Index).Value
        txtOutCts.Text = flxAssort.Item(3, flxAssort.CurrentRow.Index).Value
        txtPcs.Text = flxAssort.Item(4, flxAssort.CurrentRow.Index).Value
        txtCts.Text = flxAssort.Item(5, flxAssort.CurrentRow.Index).Value
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub txtNewAssortment_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNewAssortment.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtNewAssortment.Text = UCase(txtNewAssortment.Text)

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & txtNewAssortment.Text & "' AND Type <> 'B' AND Active = 1", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                txtBasePrice.Text = rsComSql.Fields("MarketPrice").Value
                txtAvgCost2.Text = rsComSql.Fields("AvgCost").Value
            Else
                MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtNewAssortment.Text = ""
                Exit Sub
            End If
            rsComSql = Nothing
            txtPcs.Focus()
        End If
    End Sub

    Private Sub txtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtCts.Focus()
        End If
    End Sub

    Private Sub txtCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtCts.Text)
        If Asc(e.KeyChar) = 13 Then
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub InsertAssortment()
        Dim intRow As Integer
        Dim intBalPcs As Integer
        Dim intOutPcs As Integer
        Dim blnFound As Boolean

        If txtNewAssortment.Text <> "" And txtPcs.Text <> "" And txtCts.Text <> "" And txtBasePrice.Text <> "" Then
            If Val(txtPcs.Text) > 0 And Val(txtCts.Text) > 0 Then
                If CInt(txtPcs.Text) > CInt(txtBalPcs.Text) Then MsgBox("Invalid Balance Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
                If CInt(txtCts.Text) > CInt(txtBalCts.Text) Then MsgBox("Invalid Balance Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

                txtNewAssortment.Text = UCase(txtNewAssortment.Text)

                If chkMix.Checked = True Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & txtNewAssortment.Text & "' AND Type = 'B' AND Active = 1", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql = Nothing
                Else
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & txtNewAssortment.Text & "' AND Type <> 'B' AND Active = 1", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql = Nothing
                End If

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & txtAssortment.Text & "' AND Active = 1", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    MsgBox("Invalid Old Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblAssortBlock WHERE Assortment = '" & txtNewAssortment.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    MsgBox("Assortment is Blocked - " & txtNewAssortment.Text, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblAssortBlock WHERE Assortment = '" & txtAssortment.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    MsgBox("Assortment is Blocked - " & txtAssortment.Text, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing

                If Mid(txtAssortment.Text, 1, 3) = "SRW" Then
                    If intSRWLock = 1 Then
                        If Mid(txtNewAssortment.Text, 1, 3) <> "SRW" Then
                            MsgBox("Invalid New Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                    Else

                    End If
                End If

                For intRow = 0 To flxDetails.Rows.Count - 1
                    If flxDetails.Item(2, intRow).Value = txtNewAssortment.Text Then
                        MsgBox("Already Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                Next

                'Origin Entry
                intOutPcs = 0
                intBalPcs = CInt(txtPcs.Text)
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM VW_MixOriginBal WHERE Assortment = '" & txtAssortment.Text & "' AND BalPcs > 0 ORDER BY SysDateTime", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    rsComSql_1.MoveFirst()
                    While Not rsComSql_1.EOF And intBalPcs > 0
                        If intBalPcs > 0 Then
                            blnFound = False
                            If intBalPcs <= rsComSql_1.Fields("BalPcs").Value Then
                                intOutPcs = intBalPcs

                                intBalPcs = 0
                                blnFound = True
                            Else
                                intOutPcs = rsComSql_1.Fields("BalPcs").Value
                                intBalPcs = intBalPcs - intOutPcs
                                blnFound = True
                            End If
                            If blnFound = True Then
                                flxOrigin.Rows.Add(intOutPcs,
                                                   txtNewAssortment.Text,
                                                   rsComSql_1.Fields("Origin").Value,
                                                   rsComSql_1.Fields("SupParNo").Value,
                                                   "I")

                                flxOrigin.Rows.Add(intOutPcs,
                                                   txtAssortment.Text,
                                                   rsComSql_1.Fields("Origin").Value,
                                                   rsComSql_1.Fields("SupParNo").Value,
                                                   "O")
                            End If
                        End If
                        rsComSql_1.MoveNext()
                    End While
                Else
                    flxOrigin.Rows.Add(intOutPcs,
                                       txtNewAssortment.Text,
                                       "De Beers",
                                       "X900003",
                                       "I")

                    flxOrigin.Rows.Add(intOutPcs,
                                       txtAssortment.Text,
                                       "De Beers",
                                       "X900003",
                                       "O")
                End If
                rsComSql_1 = Nothing

                If Val(txtBalPcs.Text) >= Val(txtPcs.Text) Then
                    flxDetails.Rows.Add(txtPcs.Text,
                                        txtCts.Text,
                                        txtNewAssortment.Text,
                                        "I",
                                        txtAvgCost2.Text)


                    flxDetails.Rows.Add(txtPcs.Text,
                                        txtCts.Text,
                                        txtAssortment.Text,
                                        "O",
                                        "0")

                End If
            End If
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        InsertAssortment()
    End Sub

    Private Sub chkMix_CheckedChanged(sender As Object) Handles chkMix.CheckedChanged
        If chkMix.Checked = True Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblAssortMatch WHERE NewAssortment = '" & txtAssortment.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                txtNewAssortment.Text = rsComSql.Fields("BagAssortment").Value

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & txtNewAssortment.Text & "' AND Type = 'B' AND Active = 1", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    txtBasePrice.Text = rsComSql_1.Fields("MarketPrice").Value
                Else
                    MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql_1 = Nothing
                txtPcs.Focus()
            Else
                MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            rsComSql = Nothing
        Else
            txtNewAssortment.Text = ""
            txtBasePrice.Text = ""
        End If
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean
        Dim strType As String
        Dim strOrgAssort As String
        Dim dblWtAvgNew As Double
        Dim dblWtAvgOld As Double
        Dim dblListPrice As Double
        Dim dblListPriceNew As Double
        Dim dblStonePrice As Double
        Dim dblTotPcs As Double
        Dim dblTotCts As Double
        Dim dblStoneAvg As Double
        Dim dblListPriceOld As Double
        Dim dblStonePriceOld As Double
        Dim dblInValue As Double
        Dim strNewType As String

        Dim dblDiaCost As Double
        Dim dblDiaCostOld As Double
        Dim dblDiaValue As Double
        Dim dblDiaCostAvg As Double

        If txtAssortment.Text <> "" And txtNewAssortment.Text <> "" Then
            blnSave = False

            For intRow = 0 To flxDetails.Rows.Count - 1
                If Mid(flxDetails.Item(2, intRow).Value, 1, 2) = "VM" Then
                    strOrgAssort = "VPCU"
                Else
                    strOrgAssort = "APCU"
                End If

                If flxDetails.Item(3, intRow).Value = "I" Then
                    dblListPrice = 0
                    dblListPriceNew = 0
                    dblStonePrice = 0
                    dblWtAvgOld = 0
                    dblTotPcs = 0
                    dblTotCts = 0
                    dblStoneAvg = 0
                    dblInValue = 0
                    strNewType = ""

                    dblDiaCost = 0
                    dblDiaCostOld = 0
                    dblDiaValue = 0
                    dblDiaCostAvg = 0

                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT MarketPrice,AvgCost,AvgStonePrice,NewType,DiaCost FROM dbo.tblAssortList WHERE Assortment = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        dblListPrice = rsComSql.Fields("MarketPrice").Value
                        dblWtAvgOld = rsComSql.Fields("AvgCost").Value
                        dblStonePrice = rsComSql.Fields("AvgStonePrice").Value
                        strNewType = rsComSql.Fields("NewType").Value
                        dblDiaCost = rsComSql.Fields("DiaCost").Value
                    End If
                    rsComSql = Nothing

                    dblListPriceOld = 0
                    dblStonePriceOld = 0
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT AvgCost, AvgStonePrice,DiaCost FROM dbo.tblAssortList WHERE Assortment = '" & Trim(txtAssortment.Text) & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        dblListPriceOld = rsComSql.Fields("AvgCost").Value
                        dblStonePriceOld = rsComSql.Fields("AvgStonePrice").Value
                        dblDiaCostOld = rsComSql.Fields("DiaCost").Value
                    End If
                    rsComSql = Nothing

                    'Calculate Weighted Average
                    If Mid(flxDetails.Item(2, intRow).Value, 1, 1) = "X" Then
                        dblWtAvgNew = 0
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT BoxPcs, ProdPcs, BoxCts, ProdCts, BankCts " & _
                                      "FROM VW_MixPCUStock2020 " & _
                                      "WHERE Assortment = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            If Not IsDBNull(rsComSql.Fields("BoxCts").Value) Then
                                dblTotPcs = rsComSql.Fields("BoxPcs").Value + rsComSql.Fields("ProdPcs").Value
                                dblTotCts = Math.Round(rsComSql.Fields("BoxCts").Value + IIf(rsComSql.Fields("ProdCts").Value > 0, rsComSql.Fields("ProdCts").Value, 0) + rsComSql.Fields("BankCts").Value, 3)

                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT RetPcs, RetCts FROM VW_MixPktRejExpNewY WHERE Assortment = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount Then
                                    dblTotPcs = dblTotPcs + rsComSql_1.Fields("RetPcs").Value
                                    dblTotCts = dblTotCts + rsComSql_1.Fields("RetCts").Value
                                End If
                                rsComSql_1 = Nothing

                                If Mid(txtAssortment.Text, 1, 1) = "X" Then
                                    dblWtAvgNew = (((dblTotCts * dblWtAvgOld) + (dblStonePrice * CDbl(flxDetails.Item(0, intRow).Value))) / (CDbl(flxDetails.Item(1, intRow).Value) + dblTotCts))
                                    dblStoneAvg = ((dblTotPcs * dblStonePrice) + (dblStonePriceOld * CDbl(flxDetails.Item(0, intRow).Value))) / (dblTotPcs + CDbl(flxDetails.Item(0, intRow).Value))
                                Else
                                    dblWtAvgNew = (((dblTotCts * dblWtAvgOld) + (dblStonePrice * CDbl(flxDetails.Item(0, intRow).Value))) / (CDbl(flxDetails.Item(1, intRow).Value) + dblTotCts))
                                    dblStoneAvg = ((dblTotPcs * dblStonePrice) + (dblListPriceOld * CDbl(flxDetails.Item(1, intRow).Value))) / (dblTotPcs + CDbl(flxDetails.Item(0, intRow).Value))
                                End If
                            Else
                                dblWtAvgNew = (dblStonePrice * CDbl(flxDetails.Item(0, intRow).Value)) / CDbl(flxDetails.Item(1, intRow).Value)
                                dblListPriceNew = (dblStonePrice * CDbl(flxDetails.Item(0, intRow).Value)) / CDbl(flxDetails.Item(1, intRow).Value)
                                dblStoneAvg = dblStonePriceOld
                            End If
                        End If
                        rsComSql = Nothing

                        dblWtAvgNew = Math.Round(dblWtAvgNew, 2)
                        dblListPriceNew = Math.Round(dblListPriceNew, 2)
                        dblStoneAvg = Math.Round(dblStoneAvg, 2)
                    Else
                        dblWtAvgNew = 0
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT BoxCts, ProdCts, BankCts " & _
                                      "FROM VW_MixPCUStock2020 " & _
                                      "WHERE Assortment = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            If Not IsDBNull(rsComSql.Fields("BoxCts").Value) Then
                                dblTotCts = Math.Round(rsComSql.Fields("BoxCts").Value + IIf(rsComSql.Fields("ProdCts").Value > 0, rsComSql.Fields("ProdCts").Value, 0) + rsComSql.Fields("BankCts").Value, 3)

                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT RetCts FROM VW_MixPktRejExpNewY WHERE Assortment = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount Then
                                    dblTotCts = dblTotCts + rsComSql_1.Fields("RetCts").Value
                                End If
                                rsComSql_1 = Nothing

                                If dblTotCts > 0 Then
                                    If Mid(txtAssortment.Text, 1, 1) = "X" Then
                                        dblInValue = (CDbl(flxDetails.Item(0, intRow).Value) * dblStonePriceOld)
                                        dblWtAvgNew = ((dblTotCts * dblWtAvgOld) + dblInValue) / (CDbl(flxDetails.Item(1, intRow).Value) + dblTotCts)
                                    Else
                                        dblInValue = CDbl(flxDetails.Item(1, intRow).Value) * dblListPriceOld
                                        dblWtAvgNew = ((dblTotCts * dblWtAvgOld) + dblInValue) / (CDbl(flxDetails.Item(1, intRow).Value) + dblTotCts)

                                        dblDiaValue = CDbl(flxDetails.Item(1, intRow).Value) * dblDiaCostOld
                                        dblDiaCostAvg = ((dblTotCts * dblDiaCost) + dblDiaValue) / (CDbl(flxDetails.Item(1, intRow).Value) + dblTotCts)
                                    End If
                                Else
                                    If Mid(txtAssortment.Text, 1, 1) = "X" Then
                                        dblInValue = CDbl(flxDetails.Item(0, intRow).Value) * dblStonePriceOld
                                        dblWtAvgNew = dblInValue / CDbl(flxDetails.Item(1, intRow).Value)
                                    Else
                                        dblWtAvgNew = dblListPriceOld
                                        dblDiaCostAvg = dblDiaCostOld
                                    End If
                                End If
                            Else
                                If Mid(txtAssortment.Text, 1, 1) = "X" Then
                                    dblInValue = CDbl(flxDetails.Item(0, intRow).Value) * dblStonePriceOld
                                    dblWtAvgNew = dblInValue / CDbl(flxDetails.Item(1, intRow).Value)
                                Else
                                    dblWtAvgNew = dblListPriceOld
                                    dblDiaCostAvg = dblDiaCostOld
                                End If
                            End If
                        Else
                            If Mid(txtAssortment.Text, 1, 1) = "X" Then
                                dblInValue = CDbl(flxDetails.Item(0, intRow).Value) * dblStonePriceOld
                                dblWtAvgNew = dblInValue / CDbl(flxDetails.Item(1, intRow).Value)
                            Else
                                dblWtAvgNew = dblListPriceOld
                                dblDiaCostAvg = dblDiaCostOld
                            End If
                        End If
                        rsComSql = Nothing

                        dblWtAvgNew = Math.Round(dblWtAvgNew, 2)
                        dblDiaCostAvg = Math.Round(dblDiaCostAvg, 2)
                    End If

                    'Update Weighted Average
                    AdoCN.Execute("UPDATE tblAssortList SET AvgCost = " & dblWtAvgNew & ",DiaCost = " & dblDiaCostAvg & " WHERE Assortment = '" & flxDetails.Item(2, intRow).Value & "'")

                    'Update List Price
                    If strNewType = "A" Then
                        If Mid(flxDetails.Item(2, intRow).Value, 1, 2) = "AS" Then
                            AdoCN.Execute("UPDATE tblAssortList SET MarketPrice = " & dblWtAvgNew & " WHERE Assortment = '" & flxDetails.Item(2, intRow).Value & "'")
                        End If
                    End If

                    'Update List Price - SRW,SSW,ARE
                    If Mid(flxDetails.Item(2, intRow).Value, 1, 1) = "X" Or Mid(flxDetails.Item(2, intRow).Value, 1, 3) = "ARE" Then
                        AdoCN.Execute("UPDATE tblAssortList SET MarketPrice = " & dblListPriceNew & ",AvgCost = " & dblWtAvgNew & ",AvgStonePrice = " & dblStoneAvg & " WHERE Assortment = '" & flxDetails.Item(2, intRow).Value & "'")
                    End If

                    AdoCN.Execute("INSERT INTO tblAssortDetails(ImportNo,OrgAssort,Assortment,AssortBox,DDate,InPcs,InCts,AvgCost,BaseCost,CurCost,RejInPcs,RejInCts,RejAvgCost,RejBaseCost,RejCurCost,Type) " & _
                                  "VALUES(0,'" & strOrgAssort & "','" & flxDetails.Item(2, intRow).Value & "','','" & Format(Date.Now, "MM/dd/yyyy") & "'," & CDbl(flxDetails.Item(0, intRow).Value) & "," & CDbl(flxDetails.Item(1, intRow).Value) & "," & CDbl(txtAvgCost.Text) & "," & CDbl(txtBasePrice.Text) & "," & CDbl(txtAvgCost.Text) & ",0,0,0,0,0,'C')")

                ElseIf flxDetails.Item(3, intRow).Value = "O" Then
                    AdoCN.Execute("INSERT INTO tblMixPacketDetails(ParNo,PktNo,Pcs,Cts,Assortment,OrgAssort,EntDate,Type) " & _
                                  "VALUES('Modify',''," & CDbl(flxDetails.Item(0, intRow).Value) & "," & CDbl(flxDetails.Item(1, intRow).Value) & ",'" & flxDetails.Item(2, intRow).Value & "','" & strOrgAssort & "','" & Format(Date.Now, "MM/dd/yyyy") & "','C')")
                End If
                blnSave = True

            Next
            For intRow = 0 To flxOrigin.Rows.Count - 1
                If flxOrigin.Item(4, intRow).Value = "I" Then
                    'Insert Assort Origin
                    AdoCN.Execute("INSERT INTO tblAssortOrigin(Assortment,Origin,SupParNo,Pcs,EntDate) " & _
                                  "VALUES('" & flxOrigin.Item(1, intRow).Value & "','" & flxOrigin.Item(2, intRow).Value & "','" & flxOrigin.Item(3, intRow).Value & "'," & CInt(flxOrigin.Item(0, intRow).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "')")
                ElseIf flxOrigin.Item(4, intRow).Value = "O" Then
                    'Insert Packet Origin
                    AdoCN.Execute("INSERT INTO tblMixPacketOrigin(ParNo,PktNo,Assortment,SupParNo,Origin,Pcs,EntDate,BoxInDate) " & _
                                  "VALUES('Modify','000','" & flxOrigin.Item(1, intRow).Value & "','" & flxOrigin.Item(3, intRow).Value & "','" & flxOrigin.Item(2, intRow).Value & "'," & CInt(flxOrigin.Item(0, intRow).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "MM/dd/yyyy") & "')")
                End If
            Next
            If blnSave = True Then
                If chkMix.Checked = True Then
                    strType = "B"
                Else
                    strType = "A"
                End If
                If Mid(txtAssortment.Text, 1, 2) = "VM" Then
                    strOrgAssort = "VPCU"
                Else
                    strOrgAssort = "APCU"
                End If
                For intRow = 0 To flxDetails.Rows.Count - 1
                    If flxDetails.Item(3, intRow).Value = "I" Then
                        AdoCN.Execute("INSERT INTO tblAssortConvert(OldAssortment,NewAssortment,OrgAssortment,Pcs,Cts,InID,Type,ConvertDate,OldPrice,NewPrice,DoneBy) " & _
                                      "VALUES('" & txtAssortment.Text & "','" & flxDetails.Item(2, intRow).Value & "','" & strOrgAssort & "'," & CInt(flxDetails.Item(0, intRow).Value) & "," & CDbl(flxDetails.Item(1, intRow).Value) & ",0,'" & strType & "','" & Format(Date.Now, "MM/dd/yyyy") & "'," & CDbl(txtAvgCost.Text) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",'" & PBUser_EmpNo & "')")
                    End If
                Next

                MsgBox(txtAssortment.Text & " converted to " & txtNewAssortment.Text, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            ClearFields()
            txtAssortment.Text = ""
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If MsgBoxResult.Yes Then
            Save()
        End If
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXAssortConvertSummary.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXAssortConvert.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixAssortConvertDetails.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub InsertAssortment2(ByVal strAssortment As String, ByVal dblPcs As Double, ByVal dblCts As Double)
        Dim intRow As Integer
        Dim intBalPcs As Integer
        Dim intOutPcs As Integer
        Dim blnFound As Boolean
        Dim dblAvgCost As Double

        If Val(dblPcs) > 0 And Val(dblPcs) > 0 Then
            If chkMix.Checked = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & strAssortment & "' AND Type = 'B'", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing
            Else
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & strAssortment & "' AND Type <> 'B'", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                Else
                    dblAvgCost = rsComSql.Fields("AvgCost").Value
                End If
                rsComSql = Nothing
            End If

            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(3, intRow).Value = strAssortment Then
                    MsgBox("Already Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Next

            'Origin Entry
            intOutPcs = 0
            intBalPcs = dblPcs
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM VW_MixOriginBal WHERE Assortment = '" & txtAssortment.Text & "' AND BalPcs > 0 ORDER BY SysDateTime", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                rsComSql_1.MoveFirst()
                While Not rsComSql_1.EOF And intBalPcs > 0
                    If intBalPcs > 0 Then
                        blnFound = False
                        If intBalPcs <= rsComSql_1.Fields("BalPcs").Value Then
                            intOutPcs = intBalPcs

                            intBalPcs = 0
                            blnFound = True
                        Else
                            intOutPcs = rsComSql_1.Fields("BalPcs").Value
                            intBalPcs = intBalPcs - intOutPcs
                            blnFound = True
                        End If
                        If blnFound = True Then
                            flxOrigin.Rows.Add(intOutPcs,
                                               strAssortment,
                                               rsComSql_1.Fields("Origin").Value,
                                               rsComSql_1.Fields("SupParNo").Value,
                                               "I")

                            flxOrigin.Rows.Add(intOutPcs,
                                               txtAssortment.Text,
                                               rsComSql_1.Fields("Origin").Value,
                                               rsComSql_1.Fields("SupParNo").Value,
                                               "O")
                        End If
                    End If
                    rsComSql_1.MoveNext()
                End While
            Else
                flxOrigin.Rows.Add(intOutPcs,
                                   txtNewAssortment.Text,
                                   "De Beers",
                                   "X900003",
                                   "I")

                flxOrigin.Rows.Add(intOutPcs,
                                   txtAssortment.Text,
                                   "De Beers",
                                   "X900003",
                                   "O")
            End If
            rsComSql_1 = Nothing

            If Val(txtInPcs.Text) + Val(txtOutPcs.Text) >= Val(dblPcs) Then
                flxDetails.Rows.Add(dblPcs,
                                    dblCts,
                                    strAssortment,
                                    "I",
                                    dblAvgCost)


                flxDetails.Rows.Add(dblPcs,
                                    dblCts,
                                    txtAssortment.Text,
                                    "O",
                                    "0")

            End If
        End If
    End Sub

    Private Sub InsertAssortment3(ByVal strAssortment As String, ByVal dblPcs As Double, ByVal dblCts As Double)
        Dim intRow As Integer
        Dim intBalPcs As Integer
        Dim intOutPcs As Integer
        Dim blnFound As Boolean
        Dim dblAvgCost As Double

        If Val(dblPcs) > 0 And Val(dblCts) > 0 Then
            If chkMix.Checked = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & strAssortment & "' AND Type = 'B'", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing
            Else
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & strAssortment & "' AND Type <> 'B'", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                Else
                    dblAvgCost = rsComSql.Fields("AvgCost").Value
                End If
                rsComSql = Nothing
            End If

            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(3, intRow).Value = strAssortment Then
                    MsgBox("Already Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Next

            'Origin Entry
            intOutPcs = 0
            intBalPcs = dblPcs
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM VW_MixOriginBal WHERE Assortment = '" & strAssortment & "' AND BalPcs > 0 ORDER BY SysDateTime", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                rsComSql_1.MoveFirst()
                While Not rsComSql_1.EOF And intBalPcs > 0
                    If intBalPcs > 0 Then
                        blnFound = False
                        If intBalPcs <= rsComSql_1.Fields("BalPcs").Value Then
                            intOutPcs = intBalPcs

                            intBalPcs = 0
                            blnFound = True
                        Else
                            intOutPcs = rsComSql_1.Fields("BalPcs").Value
                            intBalPcs = intBalPcs - intOutPcs
                            blnFound = True
                        End If
                        If blnFound = True Then
                            flxOrigin.Rows.Add(intOutPcs,
                                               txtNewAssortment.Text,
                                               rsComSql_1.Fields("Origin").Value,
                                               rsComSql_1.Fields("SupParNo").Value,
                                               "I")

                            flxOrigin.Rows.Add(intOutPcs,
                                               strAssortment,
                                               rsComSql_1.Fields("Origin").Value,
                                               rsComSql_1.Fields("SupParNo").Value,
                                               "O")
                        End If
                    End If
                    rsComSql_1.MoveNext()
                End While
            Else
                flxOrigin.Rows.Add(intOutPcs,
                                   txtNewAssortment.Text,
                                   "De Beers",
                                   "X900003",
                                   "I")

                flxOrigin.Rows.Add(intOutPcs,
                                   strAssortment,
                                   "De Beers",
                                   "X900003",
                                   "O")
            End If
            rsComSql_1 = Nothing

            flxDetails.Rows.Add(dblPcs,
                                dblCts,
                                txtNewAssortment.Text,
                                "I",
                                dblAvgCost)


            flxDetails.Rows.Add(dblPcs,
                                dblCts,
                                strAssortment,
                                "O",
                                "0")
        End If
    End Sub

    Private Sub cmdOpen_Click(sender As Object, e As EventArgs) Handles cmdOpen.Click
        flxExtra.Rows.Clear()

        OpenFileDialog1.Filter = "All Excel Files|*.xls;*.xlsx"
        OpenFileDialog1.InitialDirectory = "C:\"
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName <> "" Then
            txtFilePath.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Load_Excel()
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim intRow As Integer
        Dim strAssortment As String

        If txtFilePath.Text = "" Then Exit Sub
        If Len(Dir(txtFilePath.Text)) > 0 Then
            xlApp = New Excel.Application
            xlWorkBook = xlApp.Workbooks.Open(txtFilePath.Text)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            flxExtra.Rows.Clear()
            For intRow = 2 To 10000
                If Len(xlWorkSheet.Cells(intRow, 1).value) <> 0 Then
                    strAssortment = Trim(xlWorkSheet.Cells(intRow, 1).Value)

                    flxExtra.Rows.Add(strAssortment,
                                     Trim(xlWorkSheet.Cells(intRow, 2).Value),
                                     Math.Round(CDbl(Trim(xlWorkSheet.Cells(intRow, 3).Value)), 3))

                Else
                    Exit For
                End If
            Next
            xlWorkSheet = Nothing
            xlWorkBook.Close()
            xlApp.Quit()

            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)

        End If
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

    Private Sub HazelDev_Button4_Click(sender As Object, e As EventArgs) Handles HazelDev_Button4.Click
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & txtNewAssortment.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid New Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            txtNewAssortment.Text = ""
            Exit Sub
        End If
        rsComSql = Nothing

        flxDetails.Rows.Clear()
        flxOrigin.Rows.Clear()
        For intRow = 0 To flxExtra.Rows.Count - 1
            'InsertAssortment2(flxExtra.Item(0, intRow).Value, CDbl(flxExtra.Item(1, intRow).Value), CDbl(flxExtra.Item(2, intRow).Value))
            InsertAssortment3(flxExtra.Item(0, intRow).Value, CDbl(flxExtra.Item(1, intRow).Value), CDbl(flxExtra.Item(2, intRow).Value))
        Next
    End Sub

    Private Sub Save2()
        Dim intRow As Integer
        Dim dblAdjCost As Double
        Dim dblBaseCost As Double
        Dim blnSave As Boolean
        Dim strType As String
        Dim strOrgAssort As String
        Dim dblWtAvgNew As Double

        blnSave = False
        strType = ""

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & txtNewAssortment.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid New Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            txtNewAssortment.Text = ""
            Exit Sub
        End If
        rsComSql = Nothing

        For intRow = 0 To flxDetails.Rows.Count - 1
            If Mid(flxDetails.Item(2, intRow).Value, 1, 2) = "VM" Then
                strOrgAssort = "VPCU"
            Else
                strOrgAssort = "APCU"
            End If

            dblBaseCost = 0
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                dblBaseCost = rsComSql.Fields("MarketPrice").Value
            End If
            rsComSql = Nothing

            If flxDetails.Item(3, intRow).Value = "I" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & txtNewAssortment.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    dblAdjCost = rsComSql.Fields("AvgCost").Value
                End If
                rsComSql = Nothing

                'Calculate Weighted Average
                dblWtAvgNew = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT ROUND(SUM(InCts - OutCts), 3) AS TotalCts FROM VW_MixAssortInOutNew WHERE Assortment = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("TotalCts").Value) Then
                        dblWtAvgNew = (((rsComSql.Fields("TotalCts").Value * dblAdjCost) + (CDbl(flxDetails.Item(1, intRow).Value) * CDbl(flxDetails.Item(4, intRow).Value))) / (CDbl(flxDetails.Item(1, intRow).Value) + rsComSql.Fields("TotalCts").Value))
                    Else
                        dblWtAvgNew = CDbl(flxDetails.Item(4, intRow).Value)
                    End If
                End If
                rsComSql = Nothing

                dblWtAvgNew = Math.Round(dblWtAvgNew, 2)

                'Update Weighted Average
                AdoCN.Execute("UPDATE tblAssortList SET AvgCost = " & dblWtAvgNew & " WHERE Assortment = '" & flxDetails.Item(2, intRow).Value & "'")

                AdoCN.Execute("INSERT INTO tblAssortDetails(ImportNo,OrgAssort,Assortment,AssortBox,DDate,InPcs,InCts,AvgCost,BaseCost,CurCost,RejInPcs,RejInCts,RejAvgCost,RejBaseCost,RejCurCost,Type) " & _
                              "VALUES(0,'" & strOrgAssort & "','" & flxDetails.Item(2, intRow).Value & "','','" & Format(Date.Now, "MM/dd/yyyy") & "'," & CDbl(flxDetails.Item(0, intRow).Value) & "," & CDbl(flxDetails.Item(1, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & "," & dblBaseCost & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",0,0,0,0,0,'C')")

            ElseIf flxDetails.Item(3, intRow).Value = "O" Then
                AdoCN.Execute("INSERT INTO tblAssortConvert(OldAssortment,NewAssortment,OrgAssortment,Pcs,Cts,InID,Type,ConvertDate,OldPrice,NewPrice) " & _
                          "VALUES('" & flxDetails.Item(2, intRow).Value & "','" & txtNewAssortment.Text & "','" & strOrgAssort & "'," & CInt(flxDetails.Item(0, intRow).Value) & "," & CDbl(flxDetails.Item(1, intRow).Value) & ",0,'" & strType & "','" & Format(Date.Now, "MM/dd/yyyy") & "'," & dblBaseCost & "," & CDbl(flxDetails.Item(4, intRow).Value) & ")")

                AdoCN.Execute("INSERT INTO tblMixPacketDetails(ParNo,PktNo,Pcs,Cts,Assortment,OrgAssort,EntDate,Type) " & _
                              "VALUES('Modify',''," & CDbl(flxDetails.Item(0, intRow).Value) & "," & CDbl(flxDetails.Item(1, intRow).Value) & ",'" & flxDetails.Item(2, intRow).Value & "','" & strOrgAssort & "','" & Format(Date.Now, "MM/dd/yyyy") & "','C')")
            End If
            blnSave = True
            If chkMix.Checked = True Then
                strType = "B"
            Else
                strType = "A"
            End If
            If Mid(txtAssortment.Text, 1, 2) = "VM" Then
                strOrgAssort = "VPCU"
            Else
                strOrgAssort = "APCU"
            End If
            
        Next
        For intRow = 0 To flxOrigin.Rows.Count - 1
            If flxOrigin.Item(4, intRow).Value = "I" Then
                'Insert Assort Origin
                AdoCN.Execute("INSERT INTO tblAssortOrigin(Assortment,Origin,SupParNo,Pcs,EntDate) " & _
                              "VALUES('" & flxOrigin.Item(1, intRow).Value & "','" & flxOrigin.Item(2, intRow).Value & "','" & flxOrigin.Item(3, intRow).Value & "'," & CInt(flxOrigin.Item(0, intRow).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "')")
            ElseIf flxOrigin.Item(4, intRow).Value = "O" Then
                'Insert Packet Origin
                AdoCN.Execute("INSERT INTO tblMixPacketOrigin(ParNo,PktNo,Assortment,SupParNo,Origin,Pcs,EntDate,BoxInDate) " & _
                              "VALUES('Modify','000','" & flxOrigin.Item(1, intRow).Value & "','" & flxOrigin.Item(3, intRow).Value & "','" & flxOrigin.Item(2, intRow).Value & "'," & CInt(flxOrigin.Item(0, intRow).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "MM/dd/yyyy") & "')")
            End If
        Next
        If blnSave = True Then
            MsgBox(txtAssortment.Text & " converted to " & txtNewAssortment.Text, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        ClearFields()
        txtAssortment.Text = ""
        txtInPcs.Text = ""
        txtInCts.Text = ""
        txtOutPcs.Text = ""
        txtOutCts.Text = ""
        txtBalPcs.Text = "0"
        txtBalCts.Text = "0"
        flxAssort.Rows.Clear()
    End Sub

    Private Sub HazelDev_Button5_Click(sender As Object, e As EventArgs) Handles HazelDev_Button5.Click
        Save2()
    End Sub
End Class