
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_MixSolver

    Private Sub ClearFields()
        cmbMake.Text = ""
        cmbColor.Text = ""
        txtLength.Text = ""
        txtWidth.Text = ""
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        txtSelPcs.Text = ""
        cmbAssortType.Text = ""
        chkAdvance.Checked = False
        chkEmpIssues.Checked = True
        flxDetails.Rows.Clear()
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

    Private Sub Load_Color()
        cmbColor.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT DISTINCT Color FROM tblAssortList ORDER BY Color", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbColor.Items.Add(rsComSql.Fields("Color").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

    End Sub

    Private Sub Load_Make()
        cmbMake.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT DISTINCT Make FROM tblAssortList ORDER BY Make", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbMake.Items.Add(rsComSql.Fields("Make").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ClearFields()
    End Sub

    Private Sub frm_MixSolver_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        ClearFields()
        Load_Color()
        Load_Make()
        Load_AssortTypes()
        Load_Type()
    End Sub

    Private Sub Load_Type()
        cmbType.Items.Clear()
        cmbType.Items.Add("Rough")
        cmbType.Items.Add("Polished")
    End Sub

    Private Sub txtLength_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLength.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtLength.Text)
        If Asc(e.KeyChar) = 13 Then
            txtWidth.Focus()
        End If
    End Sub

    Private Sub txtWidth_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWidth.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtWidth.Text)
        If Asc(e.KeyChar) = 13 Then
            cmdFind.Focus()
        End If
    End Sub

    Private Sub Find_All()
        Dim rsAssort As ADODB.Recordset

        Dim dblLengthFrom As Double
        Dim dblLengthTo As Double
        Dim dblWidthFrom As Double
        Dim dblWidthTo As Double
        Dim strSelectFrom As String
        Dim strWhere As String
        Dim strOrder As String

        Dim intPktPcs As Integer
        Dim dblPktCts As Double
        Dim intBalPcs As Integer
        Dim dblBalCts As Double

        Dim intTotPcs As Integer
        Dim dblTotCts As Double
        Dim dblBaseCost As Double

        Dim dblTotVal As Double
        Dim dblTotVal2 As Double

        Dim intIssPcs As Integer
        Dim dblIssCts As Double
        Dim dblAvgPrice As Double

        Dim strAssortment As String
        Dim strBagAssortment As String
        Dim dblBagPrice As Double
        Dim dblTotValBag As Double
        Dim intCounter As Long

        intTotPcs = CDbl(txtTotPcs.Text)
        dblTotCts = CDbl(txtTotCts.Text)
        dblTotVal = 0
        dblTotVal2 = 0
        dblBaseCost = 0

        If cmbAssortType.Text = "" Then MsgBox("Invalid Category", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtLength.Text = "" Then MsgBox("Invalid Length", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtWidth.Text = "" Then MsgBox("Invalid Width", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        rsAssort = New ADODB.Recordset
        rsAssort.Open("SELECT * FROM tblAssortCodes2 WHERE AssortCode = '" & cmbAssortType.Text & "' ORDER BY Seq", AdoCN, 1, 1)
        If rsAssort.RecordCount Then

            ExpProgress.Minimum = 0
            ExpProgress.Visible = True

            intCounter = 0

            rsAssort.MoveFirst()
            While Not rsAssort.EOF
                If chkAdvance.Checked = True Then
                    dblLengthFrom = Math.Round(CDbl(txtLength.Text) + rsAssort.Fields("LenMin").Value, 2)
                    dblWidthFrom = Math.Round(CDbl(txtWidth.Text) + rsAssort.Fields("WidMin").Value, 2)

                    dblLengthTo = Math.Round(CDbl(txtLength.Text) + rsAssort.Fields("LenMax").Value + 0.5, 2)
                    dblWidthTo = Math.Round(CDbl(txtWidth.Text) + rsAssort.Fields("WidMax").Value + 0.5, 2)
                Else
                    dblLengthFrom = Math.Round(CDbl(txtLength.Text) + rsAssort.Fields("LenMin").Value, 2)
                    dblWidthFrom = Math.Round(CDbl(txtWidth.Text) + rsAssort.Fields("WidMin").Value, 2)

                    dblLengthTo = Math.Round(CDbl(txtLength.Text) + rsAssort.Fields("LenMax").Value, 2)
                    dblWidthTo = Math.Round(CDbl(txtWidth.Text) + rsAssort.Fields("WidMax").Value, 2)
                End If
                dblLengthFrom = Math.Round(dblLengthFrom, 2)
                dblWidthFrom = Math.Round(dblWidthFrom, 2)
                dblLengthTo = Math.Round(dblLengthTo, 2)
                dblWidthTo = Math.Round(dblWidthTo, 2)

                rsComSql = New ADODB.Recordset
                strSelectFrom = "SELECT TOP (100) PERCENT dbo.tblAssortList.Assortment, dbo.tblAssortList.Color, dbo.tblAssortList.LengthFrom, dbo.tblAssortList.LengthTo, dbo.tblAssortList.WidthFrom, " & _
                                    "dbo.tblAssortList.WidthTo, dbo.tblAssortList.Make, dbo.tblAssortList.AvWeight, dbo.tblAssortList.Shape, dbo.tblAssortList.AvgCost,dbo.tblAssortList.MarketPrice,dbo.tblAssortList.StonePrice " & _
                                "FROM dbo.tblAssortList INNER JOIN dbo.VW_MixAssortInOutNew ON dbo.tblAssortList.Assortment = dbo.VW_MixAssortInOutNew.Assortment "
                If CDbl(txtLength.Text) = 0 And CDbl(txtWidth.Text) = 0 Then
                    If Len(rsAssort.Fields("AssortCode").Value) = 3 Then
                        strWhere = " WHERE (LEFT(dbo.tblAssortList.Assortment, 3) = '" & rsAssort.Fields("AssortCode").Value & "') AND (dbo.VW_MixAssortInOutNew.InPcs - dbo.VW_MixAssortInOutNew.OutPcs > 0) "
                    Else
                        strWhere = " WHERE (LEFT(dbo.tblAssortList.Assortment, 5) = '" & rsAssort.Fields("AssortCode").Value & "') AND (dbo.VW_MixAssortInOutNew.InPcs - dbo.VW_MixAssortInOutNew.OutPcs > 0) "
                    End If
                Else
                    If Len(rsAssort.Fields("AssortCode").Value) = 3 Then
                        strWhere = " WHERE (dbo.tblAssortList.LengthFrom >= " & dblLengthFrom & ") AND (dbo.tblAssortList.LengthFrom <= " & dblLengthTo & ") AND (dbo.tblAssortList.WidthFrom >= " & dblWidthFrom & ") AND " & _
                                    "(dbo.tblAssortList.WidthFrom <= " & dblWidthTo & ") AND (LEFT(dbo.tblAssortList.Assortment, 3) = '" & rsAssort.Fields("AssortCode").Value & "') AND (dbo.VW_MixAssortInOutNew.InPcs - dbo.VW_MixAssortInOutNew.OutPcs > 0) "
                    Else
                        strWhere = " WHERE (dbo.tblAssortList.LengthFrom >= " & dblLengthFrom & ") AND (dbo.tblAssortList.LengthFrom <= " & dblLengthTo & ") AND (dbo.tblAssortList.WidthFrom >= " & dblWidthFrom & ") AND " & _
                                    "(dbo.tblAssortList.WidthFrom <= " & dblWidthTo & ") AND (LEFT(dbo.tblAssortList.Assortment, 5) = '" & rsAssort.Fields("AssortCode").Value & "') AND (dbo.VW_MixAssortInOutNew.InPcs - dbo.VW_MixAssortInOutNew.OutPcs > 0) "
                    End If    
                End If
                strOrder = " ORDER BY dbo.tblAssortList.Assortment"

                mStrSQL = strSelectFrom & strWhere & strOrder
                rsComSql.Open(mStrSQL, AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    ExpProgress.Maximum = rsComSql.RecordCount
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        intCounter = intCounter + 1

                        intPktPcs = 0
                        dblPktCts = 0
                        intBalPcs = 0
                        dblBalCts = 0
                        dblTotVal = 0
                        intIssPcs = 0
                        dblIssCts = 0
                        dblTotVal2 = 0
                        dblBaseCost = 0
                        strBagAssortment = ""
                        dblBagPrice = 0
                        dblTotValBag = 0
                        strAssortment = rsComSql.Fields("Assortment").Value

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM tblAssortBlock WHERE Assortment = '" & strAssortment & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            GoTo NextRecord
                        End If
                        rsComSql_1 = Nothing

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT SUM(InPcs - OutPcs) as BalPcs, ROUND(SUM(InCts - OutCts), 2) AS BalCts " & _
                                        "FROM dbo.VW_MixAssortInOutNew " & _
                                        "WHERE (Assortment = '" & strAssortment & "') AND (ROUND(InCts - OutCts, 2) > 0) ", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            If Not IsDBNull(rsComSql_1.Fields("BalCts").Value) Then
                                intBalPcs = rsComSql_1.Fields("BalPcs").Value
                                dblBalCts = rsComSql_1.Fields("BalCts").Value
                            End If
                        End If
                        rsComSql_1 = Nothing

                        'Employee Issues - 24/04/2020
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT SUM(BalPcs) AS TotPcs,SUM(BalCts) AS TotCts FROM VW_MixEmpBal WHERE Assortment = '" & strAssortment & "'", AdoCN, 1, 1)
                        If Not IsDBNull(rsComSql_1.Fields("TotPcs").Value) Then
                            intBalPcs = intBalPcs - rsComSql_1.Fields("TotPcs").Value
                            dblBalCts = dblBalCts - rsComSql_1.Fields("TotCts").Value
                            dblBalCts = Math.Round(dblBalCts, 3)
                        End If
                        rsComSql_1 = Nothing
                        '-----------------------------

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT SUM(Pcs) AS Pcs,SUM(Cts) AS Cts FROM tblMixIntIssues WHERE Assortment = '" & strAssortment & "' AND IssDate = '" & Format(Date.Now, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
                        If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                            intIssPcs = rsComSql_1.Fields("Pcs").Value
                            dblIssCts = rsComSql_1.Fields("Cts").Value
                        End If
                        rsComSql_1 = Nothing

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT SUM(PktPcs) AS Pcs,SUM(PktCts) AS Cts FROM tblMixPacket WHERE AssortNo = '" & strAssortment & "' AND PktIss = '" & Format(Date.Now, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
                        If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                            intIssPcs = intIssPcs - rsComSql_1.Fields("Pcs").Value
                            dblIssCts = dblIssCts - rsComSql_1.Fields("Cts").Value
                        End If
                        rsComSql_1 = Nothing

                        intBalPcs = intBalPcs - intIssPcs
                        dblBalCts = dblBalCts - dblIssCts

                        If intBalPcs > 0 Then
                            dblAvgPrice = rsComSql.Fields("AvgCost").Value

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

                            dblAvgPrice = Math.Round(dblAvgPrice, 2)

                            dblBaseCost = rsComSql.Fields("MarketPrice").Value
                            If Mid(strAssortment, 1, 1) = "S" Then
                                dblTotVal = intBalPcs * rsComSql.Fields("StonePrice").Value
                                dblTotVal2 = dblBalCts * dblAvgPrice
                                dblBaseCost = Math.Round(dblTotVal / dblBalCts, 2)
                            Else
                                dblTotVal = dblBalCts * dblBaseCost
                                dblTotVal2 = dblBalCts * dblAvgPrice
                            End If
                            dblTotValBag = dblBalCts * dblBagPrice

                            flxDetails.Rows.Add(strAssortment,
                                                intBalPcs,
                                                Format(dblBalCts, "#0.000"),
                                                intBalPcs,
                                                False,
                                                rsComSql.Fields("Color").Value,
                                                Format(dblBalCts / intBalPcs, "#0.000"),
                                                rsComSql.Fields("Make").Value,
                                                dblAvgPrice,
                                                Math.Round(dblTotVal2 / (intBalPcs), 1) + 13,
                                                Format(rsComSql.Fields("LengthFrom").Value, "#0.00") & " - " & Format(rsComSql.Fields("LengthTo").Value, "#0.00"),
                                                Format(rsComSql.Fields("WidthFrom").Value, "#0.00") & " - " & Format(rsComSql.Fields("WidthTo").Value, "#0.00"),
                                                dblBaseCost,
                                                Math.Round(dblTotVal / (intBalPcs), 1) + 13,
                                                strBagAssortment,
                                                dblBagPrice,
                                                Math.Round(dblTotValBag / (intBalPcs), 1) + 13)

                            intTotPcs = intTotPcs + intBalPcs
                            dblTotCts = dblTotCts + dblBalCts
                        End If
NextRecord:
                        rsComSql.MoveNext()
                        ExpProgress.Value = intCounter
                    End While
                End If
                rsComSql = Nothing

                rsAssort.MoveNext()
            End While
        End If
        rsAssort = Nothing

        txtTotPcs.Text = intTotPcs
        txtTotCts.Text = Format(Math.Round(dblTotCts, 3), "#0.00")

        ExpProgress.Visible = False
        intCounter = 0
    End Sub

    Private Sub Find_Size()
        Dim rsAssort As ADODB.Recordset

        Dim dblLengthFrom As Double
        Dim dblLengthTo As Double
        Dim dblWidthFrom As Double
        Dim dblWidthTo As Double
        Dim strSelectFrom As String
        Dim strWhere As String
        Dim strOrder As String

        Dim intPktPcs As Integer
        Dim dblPktCts As Double
        Dim intBalPcs As Integer
        Dim dblBalCts As Double

        Dim intTotPcs As Integer
        Dim dblTotCts As Double
        Dim dblBaseCost As Double

        Dim dblTotVal As Double
        Dim dblTotVal2 As Double

        Dim intIssPcs As Integer
        Dim dblIssCts As Double
        Dim dblAvgPrice As Double

        Dim strAssortment As String
        Dim strBagAssortment As String
        Dim dblBagPrice As Double
        Dim dblTotValBag As Double
        Dim intCounter As Long

        Dim dblSizePcs As Double
        'Dim blnStockHave As Boolean

        intTotPcs = CDbl(txtTotPcs.Text)
        dblTotCts = CDbl(txtTotCts.Text)
        dblTotVal = 0
        dblTotVal2 = 0
        dblBaseCost = 0

        If txtLength.Text = "" Then MsgBox("Invalid Length", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtWidth.Text = "" Then MsgBox("Invalid Width", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        rsAssort = New ADODB.Recordset
        'rsAssort.Open("SELECT * FROM tblAssortCodes2 WHERE AssortCode = 'ABGCN' ORDER BY Seq", AdoCN, 1, 1)
        rsAssort.Open("SELECT * FROM dbo.VW_AssortCodesAll ORDER BY Seq", AdoCN, 1, 1)
        If rsAssort.RecordCount Then

            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = rsAssort.RecordCount
            intCounter = 0

            rsAssort.MoveFirst()
            While Not rsAssort.EOF
                intCounter = intCounter + 1

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
                    dblLengthFrom = Math.Round(CDbl(txtLength.Text) + rsAssort.Fields("LenMin").Value, 2)
                    dblWidthFrom = Math.Round(CDbl(txtWidth.Text) + rsAssort.Fields("WidMin").Value, 2)

                    dblLengthTo = Math.Round(CDbl(txtLength.Text) + rsAssort.Fields("LenMax").Value + 0.5, 2)
                    dblWidthTo = Math.Round(CDbl(txtWidth.Text) + rsAssort.Fields("WidMax").Value + 0.5, 2)

                ElseIf chkFull.Checked = True Then
                    dblLengthFrom = Math.Round(CDbl(txtLength.Text) + rsAssort.Fields("LenMin").Value, 2)
                    dblWidthFrom = Math.Round(CDbl(txtWidth.Text) + rsAssort.Fields("WidMin").Value, 2)

                    dblLengthTo = Math.Round(CDbl(txtLength.Text) + rsAssort.Fields("LenMax").Value + 10, 2)
                    dblWidthTo = Math.Round(CDbl(txtWidth.Text) + rsAssort.Fields("WidMax").Value + 10, 2)

                Else
                    dblLengthFrom = Math.Round(CDbl(txtLength.Text) + rsAssort.Fields("LenMin").Value, 2)
                    dblWidthFrom = Math.Round(CDbl(txtWidth.Text) + rsAssort.Fields("WidMin").Value, 2)

                    dblLengthTo = Math.Round(CDbl(txtLength.Text) + rsAssort.Fields("LenMax").Value, 2)
                    dblWidthTo = Math.Round(CDbl(txtWidth.Text) + rsAssort.Fields("WidMax").Value, 2)
                End If
                dblLengthFrom = Math.Round(dblLengthFrom, 2)
                dblWidthFrom = Math.Round(dblWidthFrom, 2)
                dblLengthTo = Math.Round(dblLengthTo, 2)
                dblWidthTo = Math.Round(dblWidthTo, 2)

                rsComSql = New ADODB.Recordset
                strSelectFrom = "SELECT  TOP (100) PERCENT dbo.tblAssortList.Assortment, dbo.tblAssortList.Color, dbo.tblAssortList.LengthFrom, dbo.tblAssortList.LengthTo, dbo.tblAssortList.WidthFrom, " & _
                                    "dbo.tblAssortList.WidthTo, dbo.tblAssortList.Make, dbo.tblAssortList.AvWeight, dbo.tblAssortList.Shape, dbo.tblAssortList.AvgCost,dbo.tblAssortList.MarketPrice,dbo.tblAssortList.StonePrice " & _
                                "FROM dbo.tblAssortList INNER JOIN dbo.VW_MixAssortInOutNew ON dbo.tblAssortList.Assortment = dbo.VW_MixAssortInOutNew.Assortment "
                If Len(rsAssort.Fields("AssortCode").Value) = 3 Then
                    strWhere = " WHERE (dbo.tblAssortList.LengthFrom >= " & dblLengthFrom & ") AND (dbo.tblAssortList.LengthFrom <= " & dblLengthTo & ") AND (dbo.tblAssortList.WidthFrom >= " & dblWidthFrom & ") AND " & _
                                    "(dbo.tblAssortList.WidthFrom <= " & dblWidthTo & ") AND (LEFT(dbo.tblAssortList.Assortment, 3) = '" & rsAssort.Fields("AssortCode").Value & "') AND (dbo.VW_MixAssortInOutNew.InPcs - dbo.VW_MixAssortInOutNew.OutPcs > 0) "

                ElseIf Len(rsAssort.Fields("AssortCode").Value) = 4 Then
                    strWhere = " WHERE (dbo.tblAssortList.LengthFrom >= " & dblLengthFrom & ") AND (dbo.tblAssortList.LengthFrom <= " & dblLengthTo & ") AND (dbo.tblAssortList.WidthFrom >= " & dblWidthFrom & ") AND " & _
                                "(dbo.tblAssortList.WidthFrom <= " & dblWidthTo & ") AND (SUBSTRING(dbo.tblAssortList.Assortment, 7, 4) = '" & rsAssort.Fields("AssortCode").Value & "') AND (dbo.VW_MixAssortInOutNew.InPcs - dbo.VW_MixAssortInOutNew.OutPcs > 0) "
                Else
                    'strWhere = " WHERE (dbo.tblAssortList.LengthTo >= " & dblLengthFrom & ") AND (dbo.tblAssortList.LengthTo <= " & dblLengthTo & ") AND (dbo.tblAssortList.WidthTo >= " & dblWidthFrom & ") AND " & _
                    '                "(dbo.tblAssortList.WidthTo <= " & dblWidthTo & ") AND (LEFT(dbo.tblAssortList.Assortment, 5) = '" & rsAssort.Fields("AssortCode").Value & "') AND (dbo.VW_MixAssortInOutNew.InPcs - dbo.VW_MixAssortInOutNew.OutPcs > 0) "

                    strWhere = " WHERE (dbo.tblAssortList.LengthFrom >= " & dblLengthFrom & ") AND (dbo.tblAssortList.LengthFrom <= " & dblLengthTo & ") AND (dbo.tblAssortList.WidthFrom >= " & dblWidthFrom & ") AND " & _
                                    "(dbo.tblAssortList.WidthFrom <= " & dblWidthTo & ") AND (LEFT(dbo.tblAssortList.Assortment, 5) = '" & rsAssort.Fields("AssortCode").Value & "') AND (dbo.VW_MixAssortInOutNew.InPcs - dbo.VW_MixAssortInOutNew.OutPcs > 0) "
                End If
                
                If cmbColor.Text <> "" Then
                    strWhere = strWhere & " AND (dbo.tblAssortList.Color = '" & cmbColor.Text & "')"
                End If
                If cmbMake.Text <> "" Then
                    strWhere = strWhere & " AND (dbo.tblAssortList.Make = '" & cmbMake.Text & "')"
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
                strOrder = " ORDER BY dbo.tblAssortList.Assortment"

                mStrSQL = strSelectFrom & strWhere & strOrder
                rsComSql.Open(mStrSQL, AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        intPktPcs = 0
                        dblPktCts = 0
                        intBalPcs = 0
                        dblBalCts = 0
                        dblTotVal = 0
                        intIssPcs = 0
                        dblIssCts = 0
                        dblTotVal2 = 0
                        dblBaseCost = 0
                        strBagAssortment = ""
                        dblBagPrice = 0
                        dblTotValBag = 0
                        strAssortment = rsComSql.Fields("Assortment").Value

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM tblAssortBlock WHERE Assortment = '" & strAssortment & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            GoTo NextRecord
                        End If
                        rsComSql_1 = Nothing

                        'rsComSql_1 = New ADODB.Recordset
                        'rsComSql_1.Open("SELECT Pcs as BalPcs, Cts AS BalCts " & _
                        '                "FROM dbo.Comp181 " & _
                        '                "WHERE (Assortment = '" & strAssortment & "') AND (Pcs > 0) ", AdoCN, 1, 1)
                        'If rsComSql_1.RecordCount Then
                        '    If Not IsDBNull(rsComSql_1.Fields("BalCts").Value) Then
                        '        intBalPcs = rsComSql_1.Fields("BalPcs").Value
                        '        dblBalCts = rsComSql_1.Fields("BalCts").Value
                        '    End If
                        'End If
                        'rsComSql_1 = Nothing

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT SUM(InPcs - OutPcs) as BalPcs, ROUND(SUM(InCts - OutCts), 2) AS BalCts " & _
                                        "FROM dbo.VW_MixAssortInOutNew " & _
                                        "WHERE (Assortment = '" & strAssortment & "') AND (ROUND(InCts - OutCts, 2) > 0) ", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            If Not IsDBNull(rsComSql_1.Fields("BalCts").Value) Then
                                intBalPcs = rsComSql_1.Fields("BalPcs").Value
                                dblBalCts = rsComSql_1.Fields("BalCts").Value
                            End If
                        End If
                        rsComSql_1 = Nothing

                        'Employee Issues - 24/04/2020
                        If chkEmpIssues.Checked = True Then
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT SUM(BalPcs) AS TotPcs,SUM(BalCts) AS TotCts FROM VW_MixEmpBal WHERE Assortment = '" & strAssortment & "'", AdoCN, 1, 1)
                            If Not IsDBNull(rsComSql_1.Fields("TotPcs").Value) Then
                                intBalPcs = intBalPcs - rsComSql_1.Fields("TotPcs").Value
                                dblBalCts = dblBalCts - rsComSql_1.Fields("TotCts").Value
                                dblBalCts = Math.Round(dblBalCts, 3)
                            End If
                            rsComSql_1 = Nothing
                        End If
                        '-----------------------------

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT SUM(Pcs) AS Pcs,SUM(Cts) AS Cts FROM tblMixIntIssues WHERE Assortment = '" & strAssortment & "' AND IssDate = '" & Format(Date.Now, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
                        If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                            intIssPcs = rsComSql_1.Fields("Pcs").Value
                            dblIssCts = rsComSql_1.Fields("Cts").Value
                        End If
                        rsComSql_1 = Nothing

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT SUM(PktPcs) AS Pcs,SUM(PktCts) AS Cts FROM tblMixPacket WHERE AssortNo = '" & strAssortment & "' AND PktIss = '" & Format(Date.Now, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
                        If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                            intIssPcs = intIssPcs - rsComSql_1.Fields("Pcs").Value
                            dblIssCts = dblIssCts - rsComSql_1.Fields("Cts").Value
                        End If
                        rsComSql_1 = Nothing

                        intBalPcs = intBalPcs - intIssPcs
                        dblBalCts = dblBalCts - dblIssCts

                        If intBalPcs > 0 Then
                            dblAvgPrice = rsComSql.Fields("AvgCost").Value

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

                            dblAvgPrice = Math.Round(dblAvgPrice, 2)

                            dblBaseCost = rsComSql.Fields("MarketPrice").Value
                            If Mid(strAssortment, 1, 1) = "S" Then
                                'dblTotVal = intBalPcs * rsComSql.Fields("StonePrice").Value
                                dblTotVal = dblBalCts * dblAvgPrice
                                dblTotVal2 = dblBalCts * dblAvgPrice
                                dblBaseCost = Math.Round(dblTotVal / dblBalCts, 2)
                            Else
                                dblTotVal = dblBalCts * dblAvgPrice
                                dblTotVal2 = dblBalCts * dblAvgPrice
                            End If
                            
                            dblTotValBag = dblBalCts * dblBagPrice

                            dblSizePcs = 0
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT SUM(Pcs) AS Pcs FROM tblExpSizingTypes WHERE ReturnType = '" & strAssortment & "' AND OK <> 1", dbConn, 1, 1)
                            If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                                dblSizePcs = rsComSql_1.Fields("Pcs").Value
                            End If
                            rsComSql_1 = Nothing

                            flxDetails.Rows.Add(strAssortment,
                                                intBalPcs,
                                                Format(dblBalCts, "#0.000"),
                                                intBalPcs,
                                                False,
                                                rsComSql.Fields("Color").Value,
                                                Format(dblBalCts / intBalPcs, "#0.000"),
                                                rsComSql.Fields("Make").Value,
                                                dblAvgPrice,
                                                Math.Round(dblTotVal2 / (intBalPcs), 1) + 13,
                                                Format(rsComSql.Fields("LengthFrom").Value, "#0.00") & " - " & Format(rsComSql.Fields("LengthTo").Value, "#0.00"),
                                                Format(rsComSql.Fields("WidthFrom").Value, "#0.00") & " - " & Format(rsComSql.Fields("WidthTo").Value, "#0.00"),
                                                dblBaseCost,
                                                Math.Round(dblTotVal / (intBalPcs), 1) + 13,
                                                strBagAssortment,
                                                dblBagPrice,
                                                Math.Round(dblTotValBag / (intBalPcs), 1) + 13,
                                                dblSizePcs)

                            intTotPcs = intTotPcs + intBalPcs
                            dblTotCts = dblTotCts + dblBalCts
                        End If
NextRecord:
                        rsComSql.MoveNext()
                    End While
                End If
                rsComSql = Nothing
NextAssortCode:
                rsAssort.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        rsAssort = Nothing

        txtTotPcs.Text = intTotPcs
        txtTotCts.Text = Format(Math.Round(dblTotCts, 3), "#0.00")

        ExpProgress.Visible = False
        intCounter = 0
    End Sub

    Private Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        Find_Size()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(Me.flxDetails)
    End Sub

    Private Function CalSelectPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalSelectPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(4).EditedFormattedValue = True Then
                CalSelectPcs = CalSelectPcs + Val(flxSample.Item(3, intRow).Value)
            End If
        Next

    End Function

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 4 Then
            txtSelPcs.Text = CalSelectPcs(flxDetails)
        End If
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(4, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(4, intRow).Value = False
            Next
        End If
        txtSelPcs.Text = CalSelectPcs(flxDetails)
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(4, intRow).Value = True Then
                    If Not IsNumeric(flxDetails.Item(3, intRow).Value) Then
                        MsgBox("Invalid Select Pcs - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If CInt(flxDetails.Item(3, intRow).Value) > CInt(flxDetails.Item(1, intRow).Value) Then
                        MsgBox("Invalid Select Pcs - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblMixEmpIssuesReq WHERE Assortment = '" & flxDetails.Item(0, intRow).Value & "' AND Status = 0", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        MsgBox("Already Requested - " & rsComSql.Fields("EmpNo").Value & " " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        flxDetails.Item(4, intRow).Value = False
                        Exit Sub
                    End If
                    rsComSql = Nothing
                End If
            Next

            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(4, intRow).Value = True Then
                    If CInt(flxDetails.Item(3, intRow).Value) > 0 Then
                        AdoCN.Execute("INSERT INTO tblMixEmpIssuesReq(Assortment,ReqPcs,EmpNo,ReqDate,ReqTime) " & _
                                      "VALUES('" & flxDetails.Item(0, intRow).Value & "'," & CInt(flxDetails.Item(3, intRow).Value) & ",'" & PBUser_EmpNo & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "') ")
                    End If

                End If
            Next
            MsgBox("Request Saved", MsgBoxStyle.Information + vbOKOnly, Me.Text)
            ClearFields()
        End If

    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdFindAll_Click(sender As Object, e As EventArgs) Handles cmdFindAll.Click
        Find_All()
    End Sub

    Private Sub chkAdvance_CheckedChanged(sender As Object) Handles chkAdvance.CheckedChanged
        If chkAdvance.Checked = True Then
            chkFull.Checked = False
        End If
    End Sub

    Private Sub chkFull_CheckedChanged(sender As Object) Handles chkFull.CheckedChanged
        If chkFull.Checked = True Then
            chkAdvance.Checked = False
        End If
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

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Load_Excel()
    End Sub

    Private Sub Load_Excel()
        On Error GoTo ErrorHandler

        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim intRow As Integer
        Dim intRow2 As Integer
        Dim dblPrice As Double
        Dim strAssortment As String
        Dim intBalPcs, dblBalCts As Double
        Dim intIssPcs, dblIssCts As Double

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

                strAssortment = Trim(xlWorkSheet.Cells(intRow, 1).Value)

                For intRow2 = 2 To flxDetails.Rows.Count - 1
                    If strAssortment = flxDetails.Item(0, intRow2).Value Then
                        GoTo NextLine
                    End If
                Next

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT SUM(InPcs - OutPcs) as BalPcs, ROUND(SUM(InCts - OutCts), 2) AS BalCts " & _
                                "FROM dbo.VW_MixAssortInOutNew " & _
                                "WHERE (Assortment = '" & strAssortment & "') AND (ROUND(InCts - OutCts, 2) > 0) ", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("BalCts").Value) Then
                        intBalPcs = rsComSql_1.Fields("BalPcs").Value
                        dblBalCts = rsComSql_1.Fields("BalCts").Value
                    End If
                End If
                rsComSql_1 = Nothing

                'Employee Issues - 24/04/2020
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT SUM(BalPcs) AS TotPcs,SUM(BalCts) AS TotCts FROM VW_MixEmpBal WHERE Assortment = '" & strAssortment & "'", AdoCN, 1, 1)
                If Not IsDBNull(rsComSql_1.Fields("TotPcs").Value) Then
                    intBalPcs = intBalPcs - rsComSql_1.Fields("TotPcs").Value
                    dblBalCts = dblBalCts - rsComSql_1.Fields("TotCts").Value
                    dblBalCts = Math.Round(dblBalCts, 3)
                End If
                rsComSql_1 = Nothing
                '-----------------------------

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT SUM(Pcs) AS Pcs,SUM(Cts) AS Cts FROM tblMixIntIssues WHERE Assortment = '" & strAssortment & "' AND IssDate = '" & Format(Date.Now, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
                If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                    intIssPcs = rsComSql_1.Fields("Pcs").Value
                    dblIssCts = rsComSql_1.Fields("Cts").Value
                End If
                rsComSql_1 = Nothing

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT SUM(PktPcs) AS Pcs,SUM(PktCts) AS Cts FROM tblMixPacket WHERE AssortNo = '" & strAssortment & "' AND PktIss = '" & Format(Date.Now, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
                If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                    intIssPcs = intIssPcs - rsComSql_1.Fields("Pcs").Value
                    dblIssCts = dblIssCts - rsComSql_1.Fields("Cts").Value
                End If
                rsComSql_1 = Nothing

                intBalPcs = intBalPcs - intIssPcs
                dblBalCts = dblBalCts - dblIssCts

                flxDetails.Rows.Add(strAssortment,
                                    intBalPcs,
                                    dblBalCts,
                                    Trim(xlWorkSheet.Cells(intRow, 4).Value),
                                    True)

NextLine:

            Next
            xlWorkSheet = Nothing
            xlWorkBook.Close()
            xlApp.Quit()

            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)

            MsgBox("Assortment Request List Loaded", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
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
End Class