
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RprPlanValue

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub ClearFields()
        flxDetails.Rows.Clear()
        txtParNo.Text = ""
        optBagBS.Checked = True
        txtTotPcs.Text = ""
        txtPlanValue.Text = ""
        txtNewValue.Text = ""
    End Sub

    Private Sub frm_RprUpload_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub Load_Details(ByVal strParNo As String)
        'On Error GoTo ErrorHandler

        Dim dblPlanValue As Double

        Dim strLength As String

        Dim strCode As String
        Dim strColor As String
        Dim strClarity As String
        Dim strCut As String
        Dim strShape As String

        Dim dblPcs As Double
        Dim dblCts As Double
        Dim dblOrigValue As Double

        dblPcs = 0
        dblCts = 0
        dblOrigValue = 0
        rsComSql = New ADODB.Recordset
        If optBagBS.Checked = True Then
            rsComSql.Open("SELECT * FROM tblRPrPacketDetails WHERE ParNo = '" & strParNo & "' AND Shape = 'Baguettes' ORDER BY PktNo", AdoCN, 1, 1)
        End If
        If optBagAS.Checked = True Then
            rsComSql.Open("SELECT * FROM tblRPrReturnDetails WHERE ParNo = '" & strParNo & "' AND Shape = 'Baguettes' ORDER BY PktNo", AdoCN, 1, 1)
        End If
        If optPrBS.Checked = True Then
            rsComSql.Open("SELECT * FROM tblRPrPacketDetails WHERE ParNo = '" & strParNo & "' AND Shape = 'Princess' ORDER BY PktNo", AdoCN, 1, 1)
        End If
        If optPrAS.Checked = True Then
            rsComSql.Open("SELECT * FROM tblRPrReturnDetails WHERE ParNo = '" & strParNo & "' AND Shape = 'Princess' ORDER BY PktNo", AdoCN, 1, 1)
        End If
        If optRnd.Checked = True Then
            rsComSql.Open("SELECT * FROM tblRndPacket WHERE ParNo = '" & strParNo & "' ORDER BY PktNo", AdoCN, 1, 1)
        End If
        If optRndBS.Checked = True Then
            rsComSql.Open("SELECT * FROM tblRPrPacketDetails WHERE ParNo = '" & strParNo & "' AND Shape = 'Rounds' ORDER BY PktNo", AdoCN, 1, 1)
        End If
        If optRndAS.Checked = True Then
            rsComSql.Open("SELECT * FROM tblRPrReturnDetails WHERE ParNo = '" & strParNo & "' AND Shape = 'Rounds' ORDER BY PktNo", AdoCN, 1, 1)
        End If
        If optRndLB.Checked = True Then
            rsComSql.Open("SELECT * FROM tblRPrPacket WHERE ParNo = '" & strParNo & "' AND Department = 'RoughBruting' ORDER BY PktNo", AdoCN, 1, 1)
        End If
        If optEmeBS.Checked = True Then
            rsComSql.Open("SELECT * FROM tblRPrPacketDetails WHERE ParNo = '" & strParNo & "' AND Shape = 'Emerald' ORDER BY PktNo", AdoCN, 1, 1)
        End If
        If optEmeAS.Checked = True Then
            rsComSql.Open("SELECT * FROM tblRPrReturnDetails WHERE ParNo = '" & strParNo & "' AND Shape = 'Emerald' ORDER BY PktNo", AdoCN, 1, 1)
        End If
        If optEmeProd.Checked = True Then
            rsComSql.Open("SELECT * FROM tblExtPacket WHERE ParNo = '" & strParNo & "' AND Department = 'Emerald' ORDER BY PktNo", AdoCN, 1, 1)
        End If
        If optBagProd.Checked = True Then
            rsComSql.Open("SELECT * FROM tblBagPacket WHERE ParNo = '" & strParNo & "' ORDER BY PktNo", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                strLength = "0"
                strCut = ""
                strColor = ""
                strClarity = ""
                strShape = ""
                If optBagBS.Checked = True Or optPrBS.Checked = True Or optEmeBS.Checked = True Then
                    strLength = rsComSql.Fields("Size").Value
                    strColor = Trim(rsComSql.Fields("Color").Value)
                    strClarity = Trim(rsComSql.Fields("Clarity").Value)
                    strCut = Trim(rsComSql.Fields("Cut").Value)
                    dblPcs = rsComSql.Fields("Pcs").Value
                    dblCts = rsComSql.Fields("RghCts").Value
                    dblOrigValue = rsComSql.Fields("Value").Value
                    strShape = rsComSql.Fields("Shape").Value
                End If
                If optBagAS.Checked = True Or optPrAS.Checked = True Or optEmeAS.Checked = True Then
                    strLength = rsComSql.Fields("Length").Value
                    strColor = Trim(rsComSql.Fields("Color").Value)
                    strClarity = Trim(rsComSql.Fields("Clarity").Value)
                    strCut = Trim(rsComSql.Fields("Cut").Value)
                    dblPcs = rsComSql.Fields("Pcs").Value
                    dblCts = rsComSql.Fields("RghCts").Value
                    dblOrigValue = rsComSql.Fields("Value").Value
                    strShape = rsComSql.Fields("Shape").Value
                End If
                If optRnd.Checked = True Then
                    strLength = rsComSql.Fields("Sieve").Value
                    strCut = rsComSql.Fields("PktCut").Value
                    strColor = Trim(rsComSql.Fields("PktColor").Value)
                    strClarity = Trim(rsComSql.Fields("Clarity").Value)
                    dblPcs = rsComSql.Fields("PktPcs").Value
                    dblCts = rsComSql.Fields("PktCts").Value
                    dblOrigValue = rsComSql.Fields("PlanVal").Value

                    If Not IsNumeric(strLength) = True Then
                        strLength = "0"
                    End If
                End If
                If optRndBS.Checked = True Then
                    strLength = rsComSql.Fields("Size").Value
                    strCut = rsComSql.Fields("Cut").Value
                    strColor = Trim(rsComSql.Fields("Color").Value)
                    strClarity = Trim(rsComSql.Fields("Clarity").Value)
                    dblPcs = rsComSql.Fields("Pcs").Value
                    dblCts = rsComSql.Fields("RghCts").Value
                    dblOrigValue = rsComSql.Fields("Value").Value

                End If
                If optRndAS.Checked = True Then
                    strLength = rsComSql.Fields("Length").Value
                    strCut = rsComSql.Fields("Cut").Value
                    strColor = Trim(rsComSql.Fields("Color").Value)
                    strClarity = Trim(rsComSql.Fields("Clarity").Value)
                    dblPcs = rsComSql.Fields("Pcs").Value
                    dblCts = rsComSql.Fields("RghCts").Value
                    dblOrigValue = rsComSql.Fields("Value").Value

                End If
                If optRndLB.Checked = True Then
                    strLength = rsComSql.Fields("Sieve").Value
                    strCut = rsComSql.Fields("PktCut").Value
                    strColor = Trim(rsComSql.Fields("PktColor").Value)
                    strClarity = Trim(rsComSql.Fields("PktClarity").Value)
                    dblPcs = rsComSql.Fields("PktPcs").Value
                    dblCts = rsComSql.Fields("PktCts").Value
                    dblOrigValue = rsComSql.Fields("Value").Value

                    If Not IsNumeric(strLength) = True Then
                        strLength = "0"
                    End If
                End If
                If optEmeProd.Checked = True Then
                    strLength = rsComSql.Fields("Sieve").Value
                    strCut = rsComSql.Fields("PktCut").Value
                    strColor = Trim(rsComSql.Fields("PktColor").Value)
                    strClarity = Trim(rsComSql.Fields("Clarity").Value)
                    strCut = Trim(rsComSql.Fields("PktCut").Value)
                    dblPcs = rsComSql.Fields("PktPcs").Value
                    dblCts = rsComSql.Fields("PktCts").Value
                    dblOrigValue = rsComSql.Fields("PlanVal").Value

                    If Not IsNumeric(strLength) = True Then
                        strLength = "0"
                    End If
                End If
                If optBagProd.Checked = True Then
                    strLength = rsComSql.Fields("PktSize").Value
                    strCut = rsComSql.Fields("PktCut").Value
                    strColor = Trim(rsComSql.Fields("PktColor").Value)
                    strClarity = Trim(rsComSql.Fields("Clarity").Value)
                    strCut = Trim(rsComSql.Fields("PktCut").Value)
                    dblPcs = rsComSql.Fields("PktPcs").Value
                    dblCts = rsComSql.Fields("PktCts").Value
                    dblOrigValue = rsComSql.Fields("PlanVal").Value
                    strShape = "Baguettes"

                    If Not IsNumeric(strLength) = True Then
                        strLength = "0"
                    End If
                End If

                If optBagBS.Checked = True Or optBagAS.Checked = True Or optBagProd.Checked = True Then
                    dblPlanValue = 0
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT AVG(dbo.VW_BAGAssort2020.ListCost) AS ListCost " & _
                                    "FROM dbo.VW_BAGAssort2020 INNER JOIN dbo.tblRPrCodes ON dbo.VW_BAGAssort2020.Color = dbo.tblRPrCodes.SysCode INNER JOIN " & _
                                        "dbo.tblRPrCodes AS tblRPrCodes_1 ON dbo.VW_BAGAssort2020.Clarity = tblRPrCodes_1.SysCode " & _
                                    "WHERE (dbo.tblRPrCodes.SysName = '" & strColor & "') AND (tblRPrCodes_1.SysName = '" & strClarity & "') AND (dbo.VW_BAGAssort2020.LengthFrom <= '" & strLength & "') AND (dbo.VW_BAGAssort2020.LengthTo >= '" & strLength & "') AND (dbo.VW_BAGAssort2020.WidthFrom <= '" & rsComSql.Fields("Width").Value & "')  " & _
                                        "AND (dbo.VW_BAGAssort2020.WidthTo >= '" & rsComSql.Fields("Width").Value & "')", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        If Not IsDBNull(rsComSql_1.Fields("ListCost").Value) Then
                            dblPlanValue = Math.Round(rsComSql_1.Fields("ListCost").Value * rsComSql.Fields("FinCts").Value, 0)
                        End If
                    End If
                    rsComSql_1 = Nothing

                    flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                        rsComSql.Fields("PktNo").Value,
                                        Math.Round(dblCts, 3),
                                        rsComSql.Fields("FinCts").Value,
                                        strShape,
                                        strColor,
                                        strClarity,
                                        dblOrigValue,
                                        strLength,
                                        rsComSql.Fields("Width").Value,
                                        dblPcs,
                                        strCut,
                                        dblPlanValue)

                ElseIf optPrBS.Checked = True Or optPrAS.Checked = True Then
                    dblPlanValue = 0
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT AVG(dbo.VW_PRAssort2020.ListCost) AS ListCost " & _
                                    "FROM dbo.VW_PRAssort2020 INNER JOIN dbo.tblRPrCodes ON dbo.VW_PRAssort2020.Color = dbo.tblRPrCodes.SysCode INNER JOIN " & _
                                        "dbo.tblRPrCodes AS tblRPrCodes_1 ON dbo.VW_PRAssort2020.Clarity = tblRPrCodes_1.SysCode " & _
                                    "WHERE (dbo.tblRPrCodes.SysName = '" & strColor & "') AND (tblRPrCodes_1.SysName = '" & strClarity & "') AND (dbo.VW_PRAssort2020.LengthFrom <= '" & strLength & "') AND (dbo.VW_PRAssort2020.LengthTo >= '" & strLength & "')", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        If Not IsDBNull(rsComSql_1.Fields("ListCost").Value) Then
                            dblPlanValue = Math.Round(rsComSql_1.Fields("ListCost").Value * rsComSql.Fields("FinCts").Value, 0)
                        End If
                    End If
                    rsComSql_1 = Nothing

                    flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                        rsComSql.Fields("PktNo").Value,
                                        Math.Round(dblCts, 3),
                                        rsComSql.Fields("FinCts").Value,
                                        rsComSql.Fields("Shape").Value,
                                        strColor,
                                        strClarity,
                                        dblOrigValue,
                                        strLength,
                                        rsComSql.Fields("Width").Value,
                                        dblPcs,
                                        rsComSql.Fields("Cut").Value,
                                        dblPlanValue)

                ElseIf optEmeProd.Checked = True Or optEmeBS.Checked = True Or optEmeAS.Checked = True Then
                    dblPlanValue = 0
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT ListCost " & _
                                    "FROM dbo.tblDCLPermanentsE " & _
                                    "WHERE (Color = '" & strColor & "') AND (Clarity = '" & strClarity & "') AND " & _
                                        "(WtFrom <= " & rsComSql.Fields("FinCts").Value & ") AND (WtTo > " & rsComSql.Fields("FinCts").Value & ")", AdoCN, 1, 1)

                    If rsComSql_1.RecordCount Then
                        If Not IsDBNull(rsComSql_1.Fields("ListCost").Value) Then
                            dblPlanValue = Math.Round(rsComSql_1.Fields("ListCost").Value * rsComSql.Fields("FinCts").Value, 0)
                        End If
                    End If
                    rsComSql_1 = Nothing

                    If strColor = "H" Or strColor = "I" Or strColor = "J" Then
                        If rsComSql.Fields("FinCts").Value >= 0.08 And rsComSql.Fields("FinCts").Value < 0.18 Then
                            dblPlanValue = Math.Round(dblPlanValue * 0.85, 0)
                        ElseIf rsComSql.Fields("FinCts").Value >= 0.18 Then
                            dblPlanValue = Math.Round(dblPlanValue * 0.9, 0)
                        End If
                    End If

                    flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                        rsComSql.Fields("PktNo").Value,
                                        Math.Round(dblCts, 3),
                                        rsComSql.Fields("FinCts").Value,
                                        "Emerald",
                                        strColor,
                                        strClarity,
                                        dblOrigValue,
                                        strLength,
                                        rsComSql.Fields("Width").Value,
                                        dblPcs,
                                        strCut,
                                        dblPlanValue)
                Else
                    'If rsComSql.Fields("PktNo").Value = "0013" Then
                    '    MsgBox(rsComSql.Fields("PktNo").Value)
                    'End If

                    If Mid(strCut, 1, 2) = "EX" And strRight(strCut, 5) = "IDEAL" Then
                        strCut = "Ex Ideal"
                    End If

                    If strCut = "EX" Then
                        strCut = "Excellent"
                    End If

                    If strCut = "VG Point" Or strCut = "VG" Then
                        strCut = "Very Good"
                    End If

                    dblPlanValue = 0
                    strCode = ""
                    If CDbl(strLength) < 4.7 Then
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT Code, Cut, Color, Clarity " & _
                                        "FROM dbo.VW_RndPriceListCode " & _
                                        "WHERE (Color = '" & strColor & "') AND (Clarity = '" & strClarity & "') AND (Cut = '" & strCut & "')", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            strCode = rsComSql_1.Fields("Code").Value
                        End If
                        rsComSql_1 = Nothing

                        If strCode = "" Then
                            'rsComSql_1 = New ADODB.Recordset
                            'rsComSql_1.Open("SELECT Code, Cut, Color, Clarity " & _
                            '                "FROM dbo.VW_RndPriceListCode " & _
                            '                "WHERE (Color = '" & strColor & "') AND (Clarity = '" & strClarity & "') AND (Cut = 'Very Good')", AdoCN, 1, 1)
                            'If rsComSql_1.RecordCount Then
                            '    strCode = rsComSql_1.Fields("Code").Value
                            'End If
                            'rsComSql_1 = Nothing
                        End If

                        If strCode <> "" Then
                            rsComSql_1 = New ADODB.Recordset
                            If optRndBS.Checked = True Then
                                rsComSql_1.Open("SELECT FromLen, ToLen, [EXIDEAL-DEF-NON-IFVVS2], [EXIDEAL-G-NON-IFVVS21], [EX-DEF-NON-IFVVS2], [EX-G-NON-IFVVS2], [VG-IFVVS-DEF], [VG-IFVVS-G], [D-G/VS1], [D-G/VS2], [D-G/SI1], [D-G/I2], [D-H/SI3], [D-H/I1], [H/VVS], [H/VS], " & _
                                                    "[H/SI1], [H/SI2], [I/IF-VS], [I/SI-SI3], [J/IF-VS2], [J/SI1-I1], [KL/IF-SI1], [KL/SI2-I1], [MN/IF-I3], [TLB/IF-SI1], [TLB/SI2-I1], [DI/I2-13], [I/I1] " & _
                                                "FROM dbo.VW_RndPriceList " & _
                                                "WHERE (FromLen <= '" & strLength & "') AND (ToLen > '" & strLength & "')", AdoCN, 1, 1)
                            Else
                                rsComSql_1.Open("SELECT FromLen, ToLen, [EXIDEAL-DEF-NON-IFVVS2], [EXIDEAL-G-NON-IFVVS21], [EX-DEF-NON-IFVVS2], [EX-G-NON-IFVVS2], [VG-IFVVS-DEF], [VG-IFVVS-G], [D-G/VS1], [D-G/VS2], [D-G/SI1], [D-G/I2], [D-H/SI3], [D-H/I1], [H/VVS], [H/VS], " & _
                                                    "[H/SI1], [H/SI2], [I/IF-VS], [I/SI-SI3], [J/IF-VS2], [J/SI1-I1], [KL/IF-SI1], [KL/SI2-I1], [MN/IF-I3], [TLB/IF-SI1], [TLB/SI2-I1], [DI/I2-13], [I/I1] " & _
                                                "FROM dbo.VW_RndPriceList2 " & _
                                                "WHERE (FromLen <= '" & strLength & "') AND (ToLen > '" & strLength & "')", AdoCN, 1, 1)
                            End If
                            If rsComSql_1.RecordCount Then
                                dblPlanValue = IIf(Not IsDBNull(rsComSql_1.Fields(strCode).Value), rsComSql_1.Fields(strCode).Value, 0)
                                dblPlanValue = Math.Round(dblPlanValue * rsComSql.Fields("FinCts").Value, 0)
                            End If
                            rsComSql_1 = Nothing
                        End If

                    Else
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT Code, Cut, Color, Clarity " & _
                                        "FROM dbo.VW_RndPriceListCodeL " & _
                                        "WHERE (Color = '" & strColor & "') AND (Clarity = '" & strClarity & "') AND (Cut = '" & strCut & "')", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            strCode = rsComSql_1.Fields("Code").Value
                        End If
                        rsComSql_1 = Nothing

                        If strCode = "" Then
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT Code, Cut, Color, Clarity " & _
                                            "FROM dbo.VW_RndPriceListCodeL " & _
                                            "WHERE (Color = '" & strColor & "') AND (Clarity = '" & strClarity & "') AND (Cut = 'Very Good')", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount Then
                                strCode = rsComSql_1.Fields("Code").Value
                            End If
                            rsComSql_1 = Nothing
                        End If

                        If strCode <> "" Then
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT FromLen, ToLen, [EXIDEAL-D-NON-IF], [EXIDEAL-D-NON-VVS1], [EXIDEAL-D-NON-VVS2], [EXIDEAL-E-NON-IF], [EXIDEAL-E-NON-VVS1], [EXIDEAL-E-NON-VVS2], [EXIDEAL-F-NON-IF], " & _
                                                "[EXIDEAL-F-NON-VVS1], [EXIDEAL-F-NON-VVS2], [EXIDEAL-G-NON-IF], [EXIDEAL-G-NON-VVS1], [EXIDEAL-G-NON-VVS2], [D/VS1], [E/VS1], [F/VS1], [G/VS1], [D/VS2], [E/VS2], [F/VS2], [G/VS2], [D/SI1], [E/SI1], [F/SI1], [G/SI1], [D/SI2], " & _
                                                "[E/SI2], [F/SI2], [G/SI2], [D-H/SI3], [D-H/I1], [H/IF], [H/VVS1], [H/VVS2], [H/VS1], [H/VS2], [H/SI1], [H/SI2], [I/IF], [I/VVS1], [I/VVS2], [I/VS1], [I/VS2], [I/SI1], [I/SI2], [I/SI3], [J/IF-VS2], [J/SI1-I1], [KL/IF-SI1], [KL/SI2-I1], [MN/IF-I3], [TLB/IF-SI1], " & _
                                                "[TLB/SI2-I1], [DI/I2-13], [I/I1] " & _
                                            "FROM dbo.VW_RndPriceListL " & _
                                            "WHERE (FromLen <= '" & strLength & "') AND (ToLen > '" & strLength & "')", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount Then
                                dblPlanValue = IIf(Not IsDBNull(rsComSql_1.Fields(strCode).Value), rsComSql_1.Fields(strCode).Value, 0)
                                dblPlanValue = Math.Round(dblPlanValue * rsComSql.Fields("FinCts").Value, 0)
                            End If
                            rsComSql_1 = Nothing
                        End If
                    End If

                    flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                        rsComSql.Fields("PktNo").Value,
                                        Math.Round(dblCts, 3),
                                        rsComSql.Fields("FinCts").Value,
                                        "Rounds",
                                        strColor,
                                        strClarity,
                                        dblOrigValue,
                                        strLength,
                                        "0",
                                        dblPcs,
                                        strCut,
                                        dblPlanValue,
                                        strCode)
                End If


                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
        Exit Sub
ErrorHandler:
        MsgBox(Err.Description, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub flxDetails_DoubleClick(sender As Object, e As EventArgs) Handles flxDetails.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)
        End If
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtParNo.Text <> "" Then
                If Len(txtParNo.Text) >= 7 Then
                    txtParNo.Text = UCase(txtParNo.Text)

                    Load_Details(txtParNo.Text)
                End If
            Else
                MsgBox("Pls re-enter Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtParNo.Focus()
            End If
        End If
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        'rsComSql = New ADODB.Recordset
        'rsComSql.Open("SELECT * FROM PrincessStock ORDER BY ParNo, PktNo", AdoCN, 1, 1)
        'If rsComSql.RecordCount Then
        '    rsComSql.MoveFirst()
        '    While Not rsComSql.EOF
        '        rsComSql_1 = New ADODB.Recordset
        '        rsComSql_1.Open("SELECT MAX(ItemName) AS ItemName, AVG(dbo.VW_PRAssort2020.ListCost) AS ListCost " & _
        '                        "FROM dbo.VW_PRAssort2020 INNER JOIN dbo.tblRPrCodes ON dbo.VW_PRAssort2020.Color = dbo.tblRPrCodes.SysCode INNER JOIN " & _
        '                            "dbo.tblRPrCodes AS tblRPrCodes_1 ON dbo.VW_PRAssort2020.Clarity = tblRPrCodes_1.SysCode " & _
        '                        "WHERE (dbo.tblRPrCodes.SysName = '" & rsComSql.Fields("Color").Value & "') AND (tblRPrCodes_1.SysName = '" & rsComSql.Fields("Clarity").Value & "') AND (dbo.VW_PRAssort2020.LengthFrom <= '" & Math.Round(rsComSql.Fields("Length").Value, 2) & "') AND (dbo.VW_PRAssort2020.LengthTo >= '" & Math.Round(rsComSql.Fields("Length").Value, 2) & "')", AdoCN, 1, 1)
        '        If rsComSql_1.RecordCount Then
        '            If Not IsDBNull(rsComSql_1.Fields("ItemName").Value) Then
        '                AdoCN.Execute("UPDATE PrincessStock SET Assortment = '" & rsComSql_1.Fields("ItemName").Value & "' WHERE ID = '" & rsComSql.Fields("ID").Value & "'")
        '            End If
        '        End If
        '        rsComSql_1 = Nothing


        '        rsComSql.MoveNext()
        '    End While
        'End If
        'rsComSql = Nothing

        'rsComSql_6 = New ADODB.Recordset
        'rsComSql_6.Open("SELECT * FROM Comp15 ORDER BY ParNo", AdoCN, 1, 1)
        'If rsComSql_6.RecordCount Then
        '    rsComSql_6.MoveFirst()
        '    While Not rsComSql_6.EOF
        '        Load_Details(rsComSql_6.Fields("ParNo").Value)

        '        rsComSql_6.MoveNext()
        '    End While
        'End If
        'rsComSql_6 = Nothing
    End Sub
End Class