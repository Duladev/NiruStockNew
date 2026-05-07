
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RprPacket
    Dim strParNo As String
    Dim strPktNo As String

    Private Sub Load_RprDepartments()
        cmbDept.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT Department FROM dbo.tblRPrFlow GROUP BY Department ORDER BY Department", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbDept.Items.Add(rsComSql.Fields("Department").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub frm_RprPacket_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F5
                Save()
        End Select
    End Sub

    Private Sub frm_RprPacket_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_RprDepartments()
        Load_Col()
        Load_Clarity()
        Load_Cut()
        Load_Flo()
        Load_Flw()
        Load_Shape()
        Load_Shape2()
        Load_Model()
        Load_Size()
        Load_Plan()
        Load_Cut2()
        Load_Size2()

        ClearFields()

        pnlDetails1.Visible = False
        pnlDetails2.Visible = False
        pnlDetails3.Visible = False
        pnlDetails4.Visible = False

        dtpToday = GetToday()

        If PBUser_EmpNo = "D02429" Or PBUser_EmpNo = "D06975" Then
            cmdSave2.Visible = True
        Else
            cmdSave2.Visible = False
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_Col()
        Dim rstcol As ADODB.Recordset

        cmbColor.Items.Clear()
        cmbColor2.Items.Clear()
        cmbColor3.Items.Clear()
        rstcol = New ADODB.Recordset
        rstcol.Open("SELECT Type FROM tblRghTypes WHERE (Sec = 2) ORDER BY Type", AdoCN, 1, 1)
        If rstcol.RecordCount Then
            rstcol.MoveFirst()
            Do While Not rstcol.EOF
                cmbColor.Items.Add(rstcol.Fields("Type").Value)
                rstcol.MoveNext()
            Loop
        End If
        rstcol = Nothing

        rstcol = New ADODB.Recordset
        rstcol.Open("SELECT ColorCode FROM tblRPrColor ORDER BY ColorCode", AdoCN, 1, 1)
        If rstcol.RecordCount Then
            rstcol.MoveFirst()
            Do While Not rstcol.EOF
                cmbColor2.Items.Add(rstcol.Fields("ColorCode").Value)
                cmbColor3.Items.Add(rstcol.Fields("ColorCode").Value)
                rstcol.MoveNext()
            Loop
        End If
        rstcol = Nothing
    End Sub

    Private Sub Load_Plan()
        cmbPlan.Items.Clear()
        cmbPlan.Items.Add("Plan1")
        cmbPlan.Items.Add("Plan2")
        cmbPlan.Items.Add("Plan3")
    End Sub

    Private Sub Load_Cut2()
        cmbCut2.Items.Clear()
        cmbCut3.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Cut FROM tblRPrCut ORDER BY Cut", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbCut2.Items.Add(rsComSql.Fields("Cut").Value)
                cmbCut3.Items.Add(rsComSql.Fields("Cut").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing


        'cmbCut2.Items.Add("VG")
        'cmbCut2.Items.Add("EX")
        'cmbCut2.Items.Add("EX–IDEAL")
        'cmbCut2.Items.Add("GOOD")

        'cmbCut3.Items.Clear()
        'cmbCut3.Items.Add("VG")
        'cmbCut3.Items.Add("EX")
        'cmbCut3.Items.Add("EX–IDEAL")
        'cmbCut3.Items.Add("GOOD")
    End Sub

    Private Sub Load_Clarity()
        Dim rstClarity As ADODB.Recordset

        cmbClarity.Items.Clear()
        cmbClarity2.Items.Clear()
        cmbClarity3.Items.Clear()
        rstClarity = New ADODB.Recordset
        rstClarity.Open("SELECT Type FROM tblRghTypes WHERE (Sec = 4) ORDER BY Type", AdoCN, 1, 1)
        If rstClarity.RecordCount Then
            rstClarity.MoveFirst()
            Do While Not rstClarity.EOF
                cmbClarity.Items.Add(rstClarity.Fields("Type").Value)
                rstClarity.MoveNext()
            Loop
        End If
        rstClarity = Nothing

        rstClarity = New ADODB.Recordset
        rstClarity.Open("SELECT ClarityCode FROM tblRPrClarity ORDER BY ClarityCode", AdoCN, 1, 1)
        If rstClarity.RecordCount Then
            rstClarity.MoveFirst()
            Do While Not rstClarity.EOF
                cmbClarity2.Items.Add(rstClarity.Fields("ClarityCode").Value)
                cmbClarity3.Items.Add(rstClarity.Fields("ClarityCode").Value)
                rstClarity.MoveNext()
            Loop
        End If
        rstClarity = Nothing
    End Sub

    Private Sub Load_Cut()
        Dim rstCut As ADODB.Recordset

        cmbCut.Items.Clear()
        rstCut = New ADODB.Recordset
        rstCut.Open("SELECT * FROM tblCut ORDER BY Cut", AdoCN, 1, 1)
        If rstCut.RecordCount Then
            rstCut.MoveFirst()
            Do While Not rstCut.EOF
                cmbCut.Items.Add(rstCut.Fields("Cut").Value)
                rstCut.MoveNext()
            Loop
        End If
        rstCut = Nothing
    End Sub

    Private Sub Load_Shape()
        Dim rstCut As ADODB.Recordset

        cmbShape.Items.Clear()
        rstCut = New ADODB.Recordset
        rstCut.Open("SELECT * FROM tblShape ORDER BY Shape", AdoCN, 1, 1)
        If rstCut.RecordCount Then
            rstCut.MoveFirst()
            Do While Not rstCut.EOF
                cmbShape.Items.Add(rstCut.Fields("Shape").Value)
                rstCut.MoveNext()
            Loop
        End If
        rstCut = Nothing
    End Sub

    Private Sub Load_Shape2()
        Dim rstCut As ADODB.Recordset

        cmbShape2.Items.Clear()
        rstCut = New ADODB.Recordset
        rstCut.Open("SELECT * FROM tblRPrShape ORDER BY Shape", AdoCN, 1, 1)
        If rstCut.RecordCount Then
            rstCut.MoveFirst()
            Do While Not rstCut.EOF
                cmbShape2.Items.Add(rstCut.Fields("Shape").Value)
                rstCut.MoveNext()
            Loop
        End If
        rstCut = Nothing
    End Sub

    Private Sub Load_Size()
        Dim rstCut As ADODB.Recordset

        cmbSize.Items.Clear()
        cmbSize3.Items.Clear()
        rstCut = New ADODB.Recordset
        rstCut.Open("SELECT * FROM tblRgfSize ORDER BY SizeDec", AdoCN, 1, 1)
        If rstCut.RecordCount Then
            rstCut.MoveFirst()
            Do While Not rstCut.EOF
                cmbSize.Items.Add(rstCut.Fields("SizeDec").Value)
                cmbSize3.Items.Add(rstCut.Fields("SizeDec").Value)
                rstCut.MoveNext()
            Loop
        End If
        rstCut = Nothing
    End Sub

    Private Sub Load_Size2()
        Dim rstSize As ADODB.Recordset

        cmbSize2.Items.Clear()
        cmbSizeRange.Items.Clear()
        rstSize = New ADODB.Recordset
        rstSize.Open("SELECT * FROM tblRndSizeRange ORDER BY SizeRange", AdoCN, 1, 1)
        If rstSize.RecordCount Then
            rstSize.MoveFirst()
            Do While Not rstSize.EOF
                'cmbSize2.Items.Add(rstSize.Fields("SizeRange").Value)
                cmbSizeRange.Items.Add(rstSize.Fields("SizeRange").Value)
                rstSize.MoveNext()
            Loop
        End If
        rstSize = Nothing
    End Sub

    Private Sub Load_Flw()
        Dim rstflw As ADODB.Recordset

        cmbFlow.Items.Clear()
        rstflw = New ADODB.Recordset
        rstflw.Open("SELECT * FROM tblRprFlow ORDER BY Flow", AdoCN, 1, 1)
        If rstflw.RecordCount Then
            rstflw.MoveFirst()
            Do While Not rstflw.EOF
                cmbFlow.Items.Add(rstflw.Fields("Flow").Value)
                rstflw.MoveNext()
            Loop
        End If
        rstflw = Nothing

    End Sub

    Private Sub Load_Flo()
        Dim rstcol As ADODB.Recordset

        cmbFlo.Items.Clear()
        rstcol = New ADODB.Recordset
        rstcol.Open("SELECT Type FROM tblRghTypes WHERE (Sec = 3) ORDER BY Type", AdoCN, 1, 1)
        If rstcol.RecordCount Then
            rstcol.MoveFirst()
            Do While Not rstcol.EOF
                cmbFlo.Items.Add(rstcol.Fields("Type").Value)
                rstcol.MoveNext()
            Loop
        End If
        rstcol = Nothing
    End Sub

    Private Sub Load_Model()
        Dim rsGrdType As New ADODB.Recordset

        cmbModel.Items.Clear()
        rsGrdType = New ADODB.Recordset
        rsGrdType.Open("SELECT * FROM tblRghTypes WHERE Sec = 5 ORDER BY Type", AdoCN, 1, 1)
        If rsGrdType.RecordCount Then
            rsGrdType.MoveFirst()
            While Not rsGrdType.EOF
                cmbModel.Items.Add(rsGrdType.Fields("Type").Value)
                rsGrdType.MoveNext()
            End While
        End If
        rsGrdType = Nothing
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim dblPrice As Double
        Dim dblPlannedPcs As Double
        Dim dblPlanValue As Double

        Dim dblTotRghCts As Double
        Dim intRow As Integer

        If txtParNo.Text <> "" And txtPktNo.Text <> "" And cmbDept.Text = "RoughPlan" Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblRPrPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' And Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                rsComSql = Nothing
                Exit Sub
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblRPrReturns WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec >= 2  And Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Return", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                rsComSql = Nothing
                Exit Sub
            End If
            rsComSql = Nothing
        Else
            MsgBox("Invalid Parcel No/Packet No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If cmbPlan.Text = "" Then
            MsgBox("Invalid Plan", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If cmbPlan.Text <> "Plan1" And cmbPlan.Text <> "Plan2" And cmbPlan.Text <> "Plan3" Then
            MsgBox("Invalid Plan", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If txtRghPcs.Text = "" Then
            MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If CDbl(txtRghPcs.Text) <= 0 Then
            MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If txtCts.Text = "" Then
            MsgBox("Invalid Packet Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If txtRghCts.Text = "" Then
            MsgBox("Invalid Est Rgh Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If CDbl(txtRghCts.Text) <= 0 Then
            MsgBox("Invalid Est Rgh Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        dblTotRghCts = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If cmbPlan.Text = flxDetails.Item(7, intRow).Value Then
                dblTotRghCts = dblTotRghCts + CDbl(flxDetails.Item(0, intRow).Value)
            End If
        Next
        dblTotRghCts = Math.Round(dblTotRghCts, 3)

        If dblTotRghCts + CDbl(txtRghCts.Text) > CDbl(txtCts.Text) Then
            MsgBox("Invalid Est Rgh Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtFinCts2.Text = "" Then
            MsgBox("Invalid Est Finish Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If CDbl(txtFinCts2.Text) <= 0 Then
            MsgBox("Invalid Est Finish Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If cmbShape.Text = "" Then
            MsgBox("Invalid Shape", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If cmbColor.Text = "" Then
            MsgBox("Invalid Color", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If cmbColor2.Text = "" Then
            MsgBox("Invalid Color", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If CDbl(txtRghCts.Text) < CDbl(txtFinCts2.Text) Then
            MsgBox("Invalid Est Finish Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblRPrColor WHERE ColorCode = '" & cmbColor2.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Color", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            rsComSql = Nothing
            Exit Sub
        End If
        rsComSql = Nothing
        If cmbClarity.Text = "" Then
            MsgBox("Invalid Clarity", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If cmbClarity2.Text = "" Then
            MsgBox("Invalid Clarity", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblRPrClarity WHERE ClarityCode = '" & cmbClarity2.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Clarity", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            rsComSql = Nothing
            Exit Sub
        End If
        rsComSql = Nothing

        If cmbSize.Text = "" Then
            MsgBox("Invalid Size", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If txtPrice.Text = "" Then
            MsgBox("Invalid Plan Value", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If CDbl(txtPrice.Text) <= 0 Then
            MsgBox("Invalid Plan Value", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If CDbl(txtRghPcs.Text) = 1 Then
            If CDbl(txtPrice.Text) > 2500 Then
                MsgBox("Invalid Plan Value", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        End If
        If CDbl(txtCts.Text) < CDbl(txtRghCts.Text) Then
            MsgBox("Invalid Est Rough Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If CDbl(txtCts.Text) < CDbl(txtFinCts2.Text) Then
            MsgBox("Invalid Est Finish Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblShape WHERE Shape = '" & cmbShape.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Shape", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            rsComSql = Nothing
            Exit Sub
        End If
        rsComSql = Nothing

        If cmbCut2.Text = "" Then
            MsgBox("Invalid Cut", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        'If cmbCut2.Text <> "VG" And cmbCut2.Text <> "XXX EX" And cmbCut2.Text <> "EX" And cmbCut2.Text <> "EX–IDEAL" And cmbCut2.Text <> "GOOD" Then
        '    MsgBox("Invalid Cut", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '    Exit Sub
        'End If

        dblPlanValue = 0
        dblPlannedPcs = 0
        If cmbShape.Text = "PCU" Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblRgfSize WHERE SizeDec = '" & cmbSize.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Size", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                rsComSql = Nothing
                Exit Sub
            Else
                If rsComSql.Fields("Dept").Value = "BAG" Or rsComSql.Fields("Dept").Value = "PR" Then
                    If cmbClarity2.Text <> "IF" Then
                        MsgBox("Invalid Clarity", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT SUM(Pcs) AS Pcs FROM dbo.tblRPrPacketDetails WHERE (Shape = 'PCU') AND (Size = '" & cmbSize.Text & "') AND (EntDate >= '" & Format(dtpPlanStartDate, "MM/dd/yyyy") & "')", AdoCN, 1, 1)
                If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                    dblPlannedPcs = rsComSql_1.Fields("Pcs").Value
                End If
                rsComSql_1 = Nothing

                If dblPlannedPcs + CDbl(txtRghPcs.Text) > rsComSql.Fields("Pcs").Value Then
                    MsgBox("Plan Pcs Exceeds", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
            rsComSql = Nothing
        End If

        dblPlannedPcs = 0
        If cmbShape.Text = "Orders" Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblRgfSize WHERE SizeDec = '" & cmbSize.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Size", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                rsComSql = Nothing
                Exit Sub
            Else
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT SUM(Pcs) AS Pcs FROM dbo.tblRPrPacketDetails WHERE (Shape = 'Orders') AND (Size = '" & cmbSize.Text & "') AND (EntDate >= '" & Format(dtpPlanStartDate2, "MM/dd/yyyy") & "')", AdoCN, 1, 1)
                If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                    dblPlannedPcs = rsComSql_1.Fields("Pcs").Value
                End If
                rsComSql_1 = Nothing

                If dblPlannedPcs + CDbl(txtRghPcs.Text) > rsComSql.Fields("Pcs").Value Then
                    MsgBox("Plan Pcs Exceeds", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
            rsComSql = Nothing
        End If

        If UCase(PBUser_ID) <> "MANJULA" Then
            'If cmbShape.Text = "Emerald" Or strRight(cmbShape.Text, 5) = "Lumer" Then
            If strRight(cmbShape.Text, 5) = "Lumer" Then
                If CDbl(txtFinCts2.Text) < 0.18 Then
                    MsgBox("Invalid Est Finish Cts for " & cmbShape.Text, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
        End If

        If cmbShape.Text = "Baguettes" Or Mid(cmbShape.Text, 1, 5) = "Lumer" Then
            If Not IsNumeric(cmbSize.Text) Then
                MsgBox("Invalid Size", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If CDbl(cmbSize.Text) <= 0 Then
                MsgBox("Invalid Size", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtWidth.Text = "" Then
                MsgBox("Invalid Width", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If CDbl(txtWidth.Text) <= 0 Then
                MsgBox("Invalid Width", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If cmbShape.Text = "Baguettes" Then
                dblPlanValue = 0
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT AVG(dbo.VW_BAGAssort2020.ListCost) AS ListCost " & _
                                "FROM dbo.VW_BAGAssort2020 INNER JOIN dbo.tblRPrCodes ON dbo.VW_BAGAssort2020.Color = dbo.tblRPrCodes.SysCode INNER JOIN " & _
                                    "dbo.tblRPrCodes AS tblRPrCodes_1 ON dbo.VW_BAGAssort2020.Clarity = tblRPrCodes_1.SysCode " & _
                                "WHERE (dbo.tblRPrCodes.SysName = '" & Trim(cmbColor2.Text) & "') AND (tblRPrCodes_1.SysName = '" & Trim(cmbClarity2.Text) & "') AND (dbo.VW_BAGAssort2020.LengthFrom <= '" & cmbSize.Text & "') AND (dbo.VW_BAGAssort2020.LengthTo >= '" & cmbSize.Text & "') AND (dbo.VW_BAGAssort2020.WidthFrom <= '" & txtWidth.Text & "')  " & _
                                    "AND (dbo.VW_BAGAssort2020.WidthTo >= '" & txtWidth.Text & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("ListCost").Value) Then
                        dblPlanValue = Math.Round(rsComSql_1.Fields("ListCost").Value * CDbl(txtFinCts2.Text), 0)
                        txtPrice.Text = dblPlanValue
                    Else
                        dblPlanValue = 0
                        txtPrice.Text = dblPlanValue
                    End If
                End If
                rsComSql_1 = Nothing
            End If
        Else
            If txtWidth.Text = "" Then
                txtWidth.Text = "0"
            End If
        End If

        dblPrice = CDbl(txtPrice.Text)

        If cmbShape.Text = "Rounds" Then
            If Not IsNumeric(cmbSize.Text) Then
                MsgBox("Invalid Size", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If CDbl(cmbSize.Text) <= 0 Then
                MsgBox("Invalid Size", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            'rsComSql = New ADODB.Recordset
            'rsComSql.Open("SELECT dbo.tblRndSizeRangeRgh.LFrom, dbo.tblRndSizeRangeRgh.LTo, dbo.tblRndSizeRangeRgh.Need " & _
            '              "FROM dbo.tblRndColorClarityRgh AS tblRndColorClarityRgh_1 INNER JOIN " & _
            '                "dbo.tblRndSizeRangeRgh ON tblRndColorClarityRgh_1.Code = dbo.tblRndSizeRangeRgh.Clarity INNER JOIN " & _
            '                "dbo.tblRndColorClarityRgh AS tblRndColorClarityRgh_2 ON dbo.tblRndSizeRangeRgh.Cut = tblRndColorClarityRgh_2.Code INNER JOIN " & _
            '                "dbo.tblRndColorClarityRgh ON dbo.tblRndSizeRangeRgh.Color = dbo.tblRndColorClarityRgh.Code " & _
            '              "WHERE (dbo.tblRndColorClarityRgh.CodeSys = '" & cmbColor2.Text & "') AND (tblRndColorClarityRgh_1.CodeSys = '" & cmbClarity2.Text & "') AND (tblRndColorClarityRgh_2.CodeSys = '" & cmbCut2.Text & "') AND (dbo.tblRndSizeRangeRgh.LFrom <= " & CDbl(cmbSize.Text) & ") AND  " & _
            '                "(dbo.tblRndSizeRangeRgh.LTo >= " & CDbl(cmbSize.Text) & ")", AdoCN, 1, 1)
            'If rsComSql.RecordCount Then
            '    If rsComSql.Fields("Need").Value = 0 Then
            '        dblPrice = dblPrice / 0.97
            '    ElseIf rsComSql.Fields("Need").Value = 1 Then
            '        dblPrice = dblPrice / 1.1
            '    Else
            '        dblPrice = dblPrice / 1.03
            '    End If
            'End If
            'rsComSql = Nothing

            'If dblPrice > 0 Then
            '    dblPrice = Math.Round(dblPrice / 1.05, 2)
            'End If
        End If

        dblPrice = Math.Round(dblPrice, 2)

        flxDetails.Rows.Add(txtRghCts.Text,
                            txtFinCts2.Text,
                            cmbShape.Text,
                            cmbColor2.Text,
                            cmbClarity2.Text,
                            dblPrice,
                            cmbSize.Text,
                            cmbPlan.Text,
                            txtWidth.Text,
                            txtRghPcs.Text,
                            cmbCut2.Text)

        txtRghCts.Text = ""
        txtFinCts2.Text = ""
        cmbShape.Text = ""
        cmbColor2.Text = ""
        cmbClarity2.Text = ""
        txtPrice.Text = ""
        cmbSize.Text = ""
        txtWidth.Text = ""
        txtRghPcs.Text = ""
        cmbCut2.Text = "VG"
        txtRghCts.Focus()
    End Sub

    Private Sub cmbSize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSize.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If cmbShape.Text = "Rounds" Or cmbShape.Text = "Princess" Or cmbShape.Text = "Asscher" Or cmbShape.Text = "Carrer" Then
                txtWidth.Text = cmbSize.Text
            End If
            txtWidth.Focus()
        End If
    End Sub

    Private Sub cmbSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSize.SelectedIndexChanged
        If cmbSize.Text <> "" Then
            If cmbShape.Text = "PCU" Or cmbShape.Text = "Other" Or cmbShape.Text = "Orders" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblRgfSize WHERE SizeDec = '" & cmbSize.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If txtFinCts2.Text <> "" Then
                        If cmbShape.Text = "Orders" Then
                            txtPrice.Text = Math.Round(rsComSql.Fields("Price2").Value * CDbl(txtFinCts2.Text), 0)
                        Else
                            txtPrice.Text = rsComSql.Fields("Price2").Value
                        End If
                    Else
                        txtPrice.Text = "0"
                    End If
                Else
                    txtPrice.Text = "0"
                End If
                rsComSql = Nothing
            End If
        End If
    End Sub

    Private Sub txtCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtCts.Text)
        If Asc(e.KeyChar) = 13 Then
            cmbColor.Focus()
        End If
    End Sub

    Private Sub txtRghCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRghCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRghCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtFinCts2.Focus()
        End If
    End Sub

    Private Sub txtFinCts2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFinCts2.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtFinCts2.Text)
        If Asc(e.KeyChar) = 13 Then
            cmbShape.Focus()
        End If
    End Sub

    Private Sub txtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrice.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtPrice.Text)
        If Asc(e.KeyChar) = 13 Then
            cmbCut2.Focus()
        End If
    End Sub

    Private Sub txtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtCts.Focus()
        End If
    End Sub

    Private Sub cmbDept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDept.SelectedIndexChanged
        If cmbDept.Text <> "" Then
            pnlDetails1.Visible = True
            If cmbDept.Text = "RoughBruting" Then
                pnlDetails2.Visible = False
                pnlDetails3.Visible = True
                pnlDetails4.Visible = False
            Else
                pnlDetails2.Visible = True
                pnlDetails3.Visible = True
                pnlDetails4.Visible = True
            End If
        End If
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtParNo.Text <> "" Then
                If Len(txtParNo.Text) = 7 Or Len(txtParNo.Text) = 8 Then
                    txtParNo.Text = UCase(txtParNo.Text)

                    Load_ParcelDetails()
                End If
            Else
                MsgBox("Pls re-enter Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtParNo.Focus()
            End If
        End If
    End Sub

    Private Sub Load_ParcelDetails()
        Dim rstPacket As ADODB.Recordset

        If cmbDept.Text <> "" And txtParNo.Text <> "" Then
            chkTrf.Checked = False
            rstPacket = New ADODB.Recordset
            rstPacket.Open("SELECT Flow, IssuedPcs, IssuedCts, Assortment, OrigParcelNo FROM tblParcel WHERE GrpParNo = '" & txtParNo.Text & "' AND Depart = '" & cmbDept.Text & "'", AdoCN, 1, 1)
            If rstPacket.RecordCount > 0 Then
                cmbFlow.Text = rstPacket.Fields("Flow").Value
                txtAvailPcs.Text = rstPacket.Fields("IssuedPcs").Value
                txtAvailCts.Text = rstPacket.Fields("IssuedCts").Value
                txtAssort.Text = rstPacket.Fields("Assortment").Value
                txtSupParNo.Text = rstPacket.Fields("OrigParcelNo").Value

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT LotNo FROM tblImport WHERE SupParcelNo = '" & txtSupParNo.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    txtLotNo.Text = rsComSql.Fields("LotNo").Value
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(PktPcs) AS Pcs FROM tblRPrPacket WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                        txtPktPcs.Text = rsComSql.Fields("Pcs").Value
                    Else
                        txtPktPcs.Text = "0"
                    End If
                Else
                    txtPktPcs.Text = "0"
                End If
                rsComSql = Nothing

                'rsComSql = New ADODB.Recordset
                'rsComSql.Open("SELECT MAX(PktNo) AS MaxPkt FROM tblRPrPacket WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                'If rsComSql.RecordCount Then
                '    If Not IsDBNull(rsComSql.Fields("MaxPkt").Value) Then
                '        txtMaxPkt.Text = rsComSql.Fields("MaxPkt").Value
                '    Else
                '        txtMaxPkt.Text = "0000"
                '    End If
                'Else
                '    txtMaxPkt.Text = "0000"
                'End If
                'rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT MAX(PktNo) AS MaxPkt FROM tblRPrPacket WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("MaxPkt").Value) Then
                        txtPktNo.Text = Format(CDbl(rsComSql.Fields("MaxPkt").Value) + 1, "0000")
                    Else
                        txtPktNo.Text = "0001"
                    End If
                Else
                    txtPktNo.Text = "0001"
                End If
                rsComSql = Nothing

                txtValuePkts.Text = "0"
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT COUNT(DISTINCT PktNo) AS PktCount FROM tblRPrPacketDetails WHERE ParNo = '" & txtParNo.Text & "' AND Department = 'RoughPlan'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("PktCount").Value) Then
                        txtValuePkts.Text = rsComSql.Fields("PktCount").Value
                    End If
                End If
                rsComSql = Nothing

                txtChkPkts.Text = "0"
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT COUNT(DISTINCT PktNo) AS PktCount FROM tblRPrPacketDetails WHERE ParNo = '" & txtParNo.Text & "' AND Department = 'RoughPlan' AND Checked = 1", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("PktCount").Value) Then
                        txtChkPkts.Text = rsComSql.Fields("PktCount").Value
                    End If
                End If
                rsComSql = Nothing

                txtValuePcs.Text = "0"
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(Pcs) AS TotPcs FROM tblRPrPacketDetails WHERE ParNo = '" & txtParNo.Text & "' AND Department = 'RoughPlan'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("TotPcs").Value) Then
                        txtValuePcs.Text = rsComSql.Fields("TotPcs").Value
                    End If
                End If
                rsComSql = Nothing

                txtBrPcs.Text = "0"
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(PktPcs) AS Pcs FROM tblRPrPacket WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND (Model = 'RoundsBrown' OR Model = 'Reject')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                        txtBrPcs.Text = rsComSql.Fields("Pcs").Value
                    End If
                End If
                rsComSql = Nothing

                txtPrPcs.Text = "0"
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(PktPcs) AS PktPcs FROM tblRghPacket WHERE (PktType = 6) AND (ParNo = '" & txtParNo.Text & "') AND (PktModel = N'Princess')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("PktPcs").Value) Then
                        txtPrPcs.Text = rsComSql.Fields("PktPcs").Value
                    End If
                End If
                rsComSql = Nothing

                flxDetails.Rows.Clear()

                flxPacket.Rows.Clear()
                'If cmbDept.Text = "RoughBruting" Then
                '    rsComSql = New ADODB.Recordset
                '    rsComSql.Open("SELECT * FROM tblRPrPacket WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' ORDER BY PktNo", AdoCN, 1, 1)
                '    If rsComSql.RecordCount Then
                '        rsComSql.MoveFirst()
                '        While Not rsComSql.EOF
                '            flxPacket.Rows.Add(rsComSql.Fields("PktNo").Value,
                '                               rsComSql.Fields("PktColor").Value,
                '                               rsComSql.Fields("PktClarity").Value,
                '                               rsComSql.Fields("PktCut").Value,
                '                               rsComSql.Fields("PktPcs").Value,
                '                               rsComSql.Fields("PktCts").Value,
                '                               rsComSql.Fields("Value").Value,
                '                               Format(rsComSql.Fields("PktIss").Value, "yyyy-MM-dd"),
                '                               rsComSql.Fields("FinCts").Value,
                '                               rsComSql.Fields("Sieve").Value,
                '                               rsComSql.Fields("PktSize").Value,
                '                               rsComSql.Fields("PktID").Value,
                '                               rsComSql.Fields("Flo").Value,
                '                               rsComSql.Fields("ParNo").Value,
                '                               rsComSql.Fields("Model").Value,
                '                               rsComSql.Fields("PktIDNew").Value)


                '            rsComSql.MoveNext()
                '        End While
                '    End If
                '    rsComSql = Nothing
                'End If

                txtPktNo.Focus()
            Else
                MsgBox("Parcel not approved yet or Invalid parcel no!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtParNo.Focus()
            End If
            rstPacket = Nothing
        End If
    End Sub

    Private Sub txtPktNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktNo.KeyPress
        Dim rstPacket As ADODB.Recordset

        IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtParNo.Text <> "" Then
                flxDetails.Rows.Clear()
                flxResult.Rows.Clear()

                rstPacket = New ADODB.Recordset
                rstPacket.Open("SELECT * FROM tblRPrPacket WHERE ParNo = '" & txtParNo.Text & "' And PktNo = '" & txtPktNo.Text & "' And Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                If rstPacket.RecordCount > 0 Then
                    txtPcs.Text = rstPacket.Fields("PktPcs").Value
                    txtCts.Text = rstPacket.Fields("PktCts").Value
                    txtAssort.Text = rstPacket.Fields("Assortment").Value
                    cmbColor.Text = rstPacket.Fields("PktColor").Value
                    cmbClarity.Text = Trim(rstPacket.Fields("PktClarity").Value)
                    cmbCut.Text = Trim(rstPacket.Fields("PktCut").Value)
                    txtFinCts.Text = Trim(rstPacket.Fields("FinCts").Value)
                    txtEstYld.Text = rstPacket.Fields("EstYld").Value
                    txtPktID.Text = rstPacket.Fields("ID").Value
                    cmbFlow.Text = rstPacket.Fields("PktFlow").Value
                    cmbFlo.Text = rstPacket.Fields("Flo").Value
                    txtTension.Text = rstPacket.Fields("Tension").Value
                    txtMainPkt.Text = rstPacket.Fields("MainPkt").Value
                    txtComment.Text = rstPacket.Fields("Comment").Value
                    cmbModel.Text = rstPacket.Fields("Model").Value
                    txtPktID2.Text = rstPacket.Fields("PktID").Value
                    cmbPktIDNew.Text = rstPacket.Fields("PktIDNew").Value
                    txtValue.Text = rstPacket.Fields("Value").Value
                    cmbSize2.Text = rstPacket.Fields("Sieve").Value
                    cmbSizeRange.Text = rstPacket.Fields("PktSize").Value
                    txtStoneNo2.Text = rstPacket.Fields("StoneNo").Value
                    chkTrf.Checked = IIf(rstPacket.Fields("Trf").Value = 1, True, False)

                    If cmbDept.Text = "RoughPlan" Then
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT * FROM tblRPrPacketDetails WHERE ParNo = '" & txtParNo.Text & "' And PktNo = '" & txtPktNo.Text & "' And Department = '" & cmbDept.Text & "' ORDER BY ID", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            rsComSql.MoveFirst()
                            While Not rsComSql.EOF
                                flxDetails.Rows.Add(rsComSql.Fields("RghCts").Value,
                                                    rsComSql.Fields("FinCts").Value,
                                                    rsComSql.Fields("Shape").Value,
                                                    rsComSql.Fields("Color").Value,
                                                    rsComSql.Fields("Clarity").Value,
                                                    rsComSql.Fields("Value").Value,
                                                    rsComSql.Fields("Size").Value,
                                                    "Plan1",
                                                    rsComSql.Fields("Width").Value,
                                                    rsComSql.Fields("Pcs").Value,
                                                    rsComSql.Fields("Cut").Value)

                                rsComSql.MoveNext()
                            End While
                        End If
                        rsComSql = Nothing

                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT * FROM tblRPrPacketDetails2 WHERE ParNo = '" & txtParNo.Text & "' And PktNo = '" & txtPktNo.Text & "' And Department = '" & cmbDept.Text & "' ORDER BY ID", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            rsComSql.MoveFirst()
                            While Not rsComSql.EOF
                                flxDetails.Rows.Add(rsComSql.Fields("RghCts").Value,
                                                    rsComSql.Fields("FinCts").Value,
                                                    rsComSql.Fields("Shape").Value,
                                                    rsComSql.Fields("Color").Value,
                                                    rsComSql.Fields("Clarity").Value,
                                                    rsComSql.Fields("Value").Value,
                                                    rsComSql.Fields("Size").Value,
                                                    "Plan2",
                                                    rsComSql.Fields("Width").Value,
                                                    rsComSql.Fields("Pcs").Value,
                                                    rsComSql.Fields("Cut").Value)

                                rsComSql.MoveNext()
                            End While
                        End If
                        rsComSql = Nothing

                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT * FROM tblRPrPacketDetails3 WHERE ParNo = '" & txtParNo.Text & "' And PktNo = '" & txtPktNo.Text & "' And Department = '" & cmbDept.Text & "' ORDER BY ID", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            rsComSql.MoveFirst()
                            While Not rsComSql.EOF
                                flxDetails.Rows.Add(rsComSql.Fields("RghCts").Value,
                                                    rsComSql.Fields("FinCts").Value,
                                                    rsComSql.Fields("Shape").Value,
                                                    rsComSql.Fields("Color").Value,
                                                    rsComSql.Fields("Clarity").Value,
                                                    rsComSql.Fields("Value").Value,
                                                    rsComSql.Fields("Size").Value,
                                                    "Plan3",
                                                    rsComSql.Fields("Width").Value,
                                                    rsComSql.Fields("Pcs").Value,
                                                    rsComSql.Fields("Cut").Value)

                                rsComSql.MoveNext()
                            End While
                        End If
                        rsComSql = Nothing
                    End If

                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblRPrReturnDetails WHERE ParNo = '" & txtParNo.Text & "' And PktNo = '" & txtPktNo.Text & "' ORDER BY ID", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        rsComSql.MoveFirst()
                        txtDepartment.Text = rsComSql.Fields("Department").Value
                        While Not rsComSql.EOF
                            flxResult.Rows.Add(rsComSql.Fields("Department").Value,
                                               rsComSql.Fields("Shape").Value,
                                               rsComSql.Fields("Pcs").Value,
                                               rsComSql.Fields("RghCts").Value,
                                               rsComSql.Fields("FinCts").Value,
                                               rsComSql.Fields("Color").Value,
                                               rsComSql.Fields("Clarity").Value,
                                               rsComSql.Fields("Value").Value,
                                               rsComSql.Fields("Cut").Value,
                                               rsComSql.Fields("Length").Value,
                                               rsComSql.Fields("Width").Value,
                                               rsComSql.Fields("Size").Value,
                                               rsComSql.Fields("StoneNo").Value,
                                               rsComSql.Fields("ID").Value)

                            rsComSql.MoveNext()
                        End While
                    End If
                    rsComSql = Nothing
                Else
                    flxDetails.Rows.Clear()
                    flxResult.Rows.Clear()
                End If
                If cmbDept.Text = "RoughBruting" Then
                    txtPktID2.Focus()
                Else
                    txtPcs.Focus()
                End If
            Else
                MsgBox("Pls re-enter Packet No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtPcs.Text = ""
                txtPcs2.Text = ""
                txtRghPcs.Text = ""
                txtCts.Text = ""
                txtAssort.Text = ""
                cmbColor.Text = "-"
                cmbFlow.Text = ""
                cmbClarity.Text = "-"
                cmbCut.Text = "-"
                cmbModel.Text = ""
                txtEstYld.Text = "0"
                txtFinCts.Text = "0"
                txtPktID.Text = "0"
                cmbFlo.Text = ""
                txtTension.Text = "0"
                txtMainPkt.Text = ""
                txtAvailPcs.Text = "0"
                txtAvailCts.Text = "0"
                txtPktPcs.Text = "0"
                txtPrice.Text = "0"
                txtPlanValue.Text = "0"
                cmbColor2.Text = "-"
                cmbColor3.Text = "-"
                cmbClarity2.Text = "-"
                cmbClarity3.Text = "-"
                cmbShape.Text = "-"
                cmbShape2.Text = "-"
                txtRghCts.Text = "0"
                txtRghCts2.Text = "0"
                txtFinCts2.Text = "0"
                txtFinCts3.Text = "0"
                txtComment.Text = ""
                flxDetails.Rows.Clear()
                flxResult.Rows.Clear()
                txtPktNo2.Text = ""
                txtPktID2.Text = "0"
                cmbPktIDNew.Text = "0"
                cmbPktIDNew.Items.Clear()
                txtStoneNo2.Text = ""
                txtDepartment.Text = ""
                chkTrf.Checked = False
                txtPktNo.Focus()
            End If
        End If
    End Sub

    Private Sub txtPktNo_TextChanged(sender As Object, e As EventArgs) Handles txtPktNo.TextChanged
        txtPktNo2.Text = txtPktNo.Text
    End Sub

    Private Sub Insert_PacketDetails()
        Dim intRow As Integer
        Dim dblOrderPcs As Double
        Dim dblPlanPcs As Double
        Dim strError As String
        Dim dblTotRghCtsP1 As Double
        Dim dblTotRghCtsP2 As Double
        Dim dblTotRghCtsP3 As Double

        dblTotRghCtsP1 = 0
        dblTotRghCtsP2 = 0
        dblTotRghCtsP3 = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If Len(flxDetails.Item(0, intRow).Value) = 0 Then
                MsgBox("Invalid Rgh Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If IsNumeric(flxDetails.Item(0, intRow).Value) = False Then
                MsgBox("Invalid Rgh Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(0, intRow).Value) <= 0 Then
                MsgBox("Invalid Rgh Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Len(flxDetails.Item(1, intRow).Value) = 0 Then
                MsgBox("Invalid Fin Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If IsNumeric(flxDetails.Item(1, intRow).Value) = False Then
                MsgBox("Invalid Fin Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(1, intRow).Value) <= 0 Then
                MsgBox("Invalid Fin Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Len(flxDetails.Item(5, intRow).Value) = 0 Then
                MsgBox("Invalid Plan Value", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If IsNumeric(flxDetails.Item(5, intRow).Value) = False Then
                MsgBox("Invalid Plan Value", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(5, intRow).Value) <= 0 Then
                MsgBox("Invalid Plan Value", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Len(flxDetails.Item(9, intRow).Value) = 0 Then
                MsgBox("Invalid Rgh Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If IsNumeric(flxDetails.Item(9, intRow).Value) = False Then
                MsgBox("Invalid Rgh Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(9, intRow).Value) <= 0 Then
                MsgBox("Invalid Rgh Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If flxDetails.Item(7, intRow).Value = "Plan1" Then
                dblTotRghCtsP1 = dblTotRghCtsP1 + CDbl(flxDetails.Item(0, intRow).Value)
            End If
            If flxDetails.Item(7, intRow).Value = "Plan2" Then
                dblTotRghCtsP2 = dblTotRghCtsP2 + CDbl(flxDetails.Item(0, intRow).Value)
            End If
            If flxDetails.Item(7, intRow).Value = "Plan3" Then
                dblTotRghCtsP3 = dblTotRghCtsP3 + CDbl(flxDetails.Item(0, intRow).Value)
            End If
        Next
        dblTotRghCtsP1 = Math.Round(dblTotRghCtsP1, 3)
        dblTotRghCtsP2 = Math.Round(dblTotRghCtsP2, 3)
        dblTotRghCtsP3 = Math.Round(dblTotRghCtsP3, 3)

        If dblTotRghCtsP1 > CDbl(txtCts.Text) Then
            MsgBox("Invalid Est Rgh Cts Plan 1", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If dblTotRghCtsP2 > CDbl(txtCts.Text) Then
            MsgBox("Invalid Est Rgh Cts Plan 2", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If dblTotRghCtsP3 > CDbl(txtCts.Text) Then
            MsgBox("Invalid Est Rgh Cts Plan 3", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        'Plan Block Checker
        For intRow = 0 To flxDetails.Rows.Count - 1
            dblOrderPcs = 0
            dblPlanPcs = 0
            strError = ""
            If flxDetails.Item(2, intRow).Value = "Asscher" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT dbo.tblRPrPlanBlock.Shape, dbo.tblRPrPlanBlock.LFrom, dbo.tblRPrPlanBlock.LTo, dbo.tblRPrPlanBlock.WFrom, dbo.tblRPrPlanBlock.WTo, " & _
                                    "dbo.tblRPrClarity2.ClarityCode, dbo.tblRPrColor2.ColorCode, dbo.tblRPrPlanBlock.Pcs " & _
                              "FROM dbo.tblRPrClarity2 INNER JOIN dbo.tblRPrPlanBlock ON dbo.tblRPrClarity2.ClarityCode = dbo.tblRPrPlanBlock.Clarity INNER JOIN " & _
                                    "dbo.tblRPrColor2 ON dbo.tblRPrPlanBlock.Color = dbo.tblRPrColor2.ColorCode " & _
                              "WHERE (dbo.tblRPrColor2.Color = '" & flxDetails.Item(3, intRow).Value & "') AND (dbo.tblRPrClarity2.Clarity = '" & flxDetails.Item(4, intRow).Value & "') AND (dbo.tblRPrPlanBlock.LFrom <= '" & flxDetails.Item(6, intRow).Value & "') AND (dbo.tblRPrPlanBlock.LTo >= '" & flxDetails.Item(6, intRow).Value & "') AND " & _
                                    "(dbo.tblRPrPlanBlock.WFrom <= '" & flxDetails.Item(8, intRow).Value & "') AND (dbo.tblRPrPlanBlock.WTo >= '" & flxDetails.Item(8, intRow).Value & "') AND (dbo.tblRPrPlanBlock.Shape = '" & flxDetails.Item(2, intRow).Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    dblOrderPcs = rsComSql.Fields("Pcs").Value
                    Select Case flxDetails.Item(7, intRow).Value
                        Case "Plan1"
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT SUM(dbo.tblRPrPacketDetails.Pcs) AS Pcs " & _
                                            "FROM dbo.tblRPrPacketDetails INNER JOIN dbo.tblRPrColor2 ON dbo.tblRPrPacketDetails.Color = dbo.tblRPrColor2.Color INNER JOIN " & _
                                                "dbo.tblRPrClarity2 ON dbo.tblRPrPacketDetails.Clarity = dbo.tblRPrClarity2.Clarity " & _
                                            "WHERE (dbo.tblRPrPacketDetails.Size >= '" & rsComSql.Fields("LFrom").Value & "') AND (dbo.tblRPrPacketDetails.Width >= '" & rsComSql.Fields("LTo").Value & "') AND (dbo.tblRPrPacketDetails.Size <= '" & rsComSql.Fields("WFrom").Value & "') AND  " & _
                                                "(dbo.tblRPrPacketDetails.Width <= '" & rsComSql.Fields("WTo").Value & "') AND (dbo.tblRPrColor2.ColorCode = '" & rsComSql.Fields("ColorCode").Value & "') AND (dbo.tblRPrClarity2.ClarityCode = '" & rsComSql.Fields("ClarityCode").Value & "') AND  " & _
                                                "(dbo.tblRPrPacketDetails.Shape = '" & rsComSql.Fields("Shape").Value & "')", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount Then
                                If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                                    dblPlanPcs = rsComSql_1.Fields("Pcs").Value
                                End If
                            End If
                            rsComSql_1 = Nothing

                        Case "Plan2"
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT SUM(dbo.tblRPrPacketDetails2.Pcs) AS Pcs " & _
                                            "FROM dbo.tblRPrPacketDetails2 INNER JOIN dbo.tblRPrColor2 ON dbo.tblRPrPacketDetails2.Color = dbo.tblRPrColor2.Color INNER JOIN " & _
                                                "dbo.tblRPrClarity2 ON dbo.tblRPrPacketDetails2.Clarity = dbo.tblRPrClarity2.Clarity " & _
                                            "WHERE (dbo.tblRPrPacketDetails2.Size >= '" & rsComSql.Fields("LFrom").Value & "') AND (dbo.tblRPrPacketDetails2.Width >= '" & rsComSql.Fields("LTo").Value & "') AND (dbo.tblRPrPacketDetails2.Size <= '" & rsComSql.Fields("WFrom").Value & "') AND  " & _
                                                "(dbo.tblRPrPacketDetails2.Width <= '" & rsComSql.Fields("WTo").Value & "') AND (dbo.tblRPrColor2.ColorCode = '" & rsComSql.Fields("ColorCode").Value & "') AND (dbo.tblRPrClarity2.ClarityCode = '" & rsComSql.Fields("ClarityCode").Value & "') AND  " & _
                                                "(dbo.tblRPrPacketDetails2.Shape = '" & rsComSql.Fields("Shape").Value & "')", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount Then
                                If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                                    dblPlanPcs = rsComSql_1.Fields("Pcs").Value
                                End If
                            End If

                        Case "Plan3"
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT SUM(dbo.tblRPrPacketDetails3.Pcs) AS Pcs " & _
                                            "FROM dbo.tblRPrPacketDetails3 INNER JOIN dbo.tblRPrColor2 ON dbo.tblRPrPacketDetails3.Color = dbo.tblRPrColor2.Color INNER JOIN " & _
                                                "dbo.tblRPrClarity2 ON dbo.tblRPrPacketDetails3.Clarity = dbo.tblRPrClarity2.Clarity " & _
                                            "WHERE (dbo.tblRPrPacketDetails3.Size >= '" & rsComSql.Fields("LFrom").Value & "') AND (dbo.tblRPrPacketDetails3.Width >= '" & rsComSql.Fields("LTo").Value & "') AND (dbo.tblRPrPacketDetails3.Size <= '" & rsComSql.Fields("WFrom").Value & "') AND  " & _
                                                "(dbo.tblRPrPacketDetails3.Width <= '" & rsComSql.Fields("WTo").Value & "') AND (dbo.tblRPrColor2.ColorCode = '" & rsComSql.Fields("ColorCode").Value & "') AND (dbo.tblRPrClarity2.ClarityCode = '" & rsComSql.Fields("ClarityCode").Value & "') AND  " & _
                                                "(dbo.tblRPrPacketDetails3.Shape = '" & rsComSql.Fields("Shape").Value & "')", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount Then
                                If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                                    dblPlanPcs = rsComSql_1.Fields("Pcs").Value
                                End If
                            End If
                            rsComSql_1 = Nothing
                    End Select

                    If dblPlanPcs + CDbl(flxDetails.Item(9, intRow).Value) > dblOrderPcs Then
                        strError = rsComSql.Fields("Shape").Value & "/" & flxDetails.Item(3, intRow).Value & "/" & flxDetails.Item(4, intRow).Value & "/" & flxDetails.Item(6, intRow).Value & "/" & flxDetails.Item(8, intRow).Value
                        MsgBox("Plan Pcs Exceeds the Order Pcs - " & strError, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If
                rsComSql = Nothing
            End If
        Next

        AdoCN.Execute("DELETE FROM tblRPrPacketDetails WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' And Department = '" & cmbDept.Text & "'")
        AdoCN.Execute("DELETE FROM tblRPrPacketDetails2 WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' And Department = '" & cmbDept.Text & "'")
        AdoCN.Execute("DELETE FROM tblRPrPacketDetails3 WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' And Department = '" & cmbDept.Text & "'")

        For intRow = 0 To flxDetails.Rows.Count - 1
            Select Case flxDetails.Item(7, intRow).Value
                Case "Plan1"
                    AdoCN.Execute("INSERT INTO tblRPrPacketDetails(Department,ParNo,PktNo,RghCts,FinCts,Shape,Color,Clarity,Value,Size,Width,Pcs,Cut,EntDate,Checked,UserName,CompName) " & _
                                  "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "'," & CDbl(flxDetails.Item(0, intRow).Value) & "," & _
                                    "" & CDbl(flxDetails.Item(1, intRow).Value) & ",'" & flxDetails.Item(2, intRow).Value & "'," & _
                                    "'" & flxDetails.Item(3, intRow).Value & "','" & flxDetails.Item(4, intRow).Value & "'," & _
                                    "" & CDbl(flxDetails.Item(5, intRow).Value) & ",'" & flxDetails.Item(6, intRow).Value & "'," & CDbl(flxDetails.Item(8, intRow).Value) & "," & _
                                    "" & CDbl(flxDetails.Item(9, intRow).Value) & ",'" & flxDetails.Item(10, intRow).Value & "','" & Format(Date.Now, "MM/dd/yyyy") & "',1,'" & PBUser_EmpNo & "','" & PBCompName & "')")
                Case "Plan2"
                    AdoCN.Execute("INSERT INTO tblRPrPacketDetails2(Department,ParNo,PktNo,RghCts,FinCts,Shape,Color,Clarity,Value,Size,Width,Pcs,Cut,EntDate,UserName,CompName) " & _
                                  "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "'," & CDbl(flxDetails.Item(0, intRow).Value) & "," & _
                                    "" & CDbl(flxDetails.Item(1, intRow).Value) & ",'" & flxDetails.Item(2, intRow).Value & "'," & _
                                    "'" & flxDetails.Item(3, intRow).Value & "','" & flxDetails.Item(4, intRow).Value & "'," & _
                                    "" & CDbl(flxDetails.Item(5, intRow).Value) & ",'" & flxDetails.Item(6, intRow).Value & "'," & CDbl(flxDetails.Item(8, intRow).Value) & "," & _
                                    "" & CDbl(flxDetails.Item(9, intRow).Value) & ",'" & flxDetails.Item(10, intRow).Value & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & PBUser_EmpNo & "','" & PBCompName & "')")
                Case "Plan3"
                    AdoCN.Execute("INSERT INTO tblRPrPacketDetails3(Department,ParNo,PktNo,RghCts,FinCts,Shape,Color,Clarity,Value,Size,Width,Pcs,Cut,EntDate,UserName,CompName) " & _
                                  "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "'," & CDbl(flxDetails.Item(0, intRow).Value) & "," & _
                                    "" & CDbl(flxDetails.Item(1, intRow).Value) & ",'" & flxDetails.Item(2, intRow).Value & "'," & _
                                    "'" & flxDetails.Item(3, intRow).Value & "','" & flxDetails.Item(4, intRow).Value & "'," & _
                                    "" & CDbl(flxDetails.Item(5, intRow).Value) & ",'" & flxDetails.Item(6, intRow).Value & "'," & CDbl(flxDetails.Item(8, intRow).Value) & "," & _
                                    "" & CDbl(flxDetails.Item(9, intRow).Value) & ",'" & flxDetails.Item(10, intRow).Value & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & PBUser_EmpNo & "','" & PBCompName & "')")
            End Select

        Next
    End Sub

    Private Sub Insert_FinishDetails()
        AdoCN.Execute("DELETE FROM tblRPrReturnDetails WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'")

        For intRow = 0 To flxResult.Rows.Count - 1
            mStrSQL = "INSERT INTO tblRPrReturnDetails(Department,ParNo,PktNo,Shape,Pcs,RghCts,FinCts,Value,Color,Clarity,Cut,Length,Width,Size,Edited,StoneNo) " & _
                      "VALUES('" & flxResult.Item(0, intRow).Value & "','" & txtParNo.Text & "','" & txtPktNo.Text & "','" & flxResult.Item(1, intRow).Value & "'," & CDbl(flxResult.Item(2, intRow).Value) & "," & _
                        "" & CDbl(flxResult.Item(3, intRow).Value) & "," & CDbl(flxResult.Item(4, intRow).Value) & "," & CDbl(flxResult.Item(7, intRow).Value) & ",'" & flxResult.Item(5, intRow).Value & "'," & _
                        "'" & flxResult.Item(6, intRow).Value & "','" & flxResult.Item(8, intRow).Value & "'," & CDbl(flxResult.Item(9, intRow).Value) & "," & CDbl(flxResult.Item(10, intRow).Value) & "," & _
                        "'" & flxResult.Item(11, intRow).Value & "',1,'" & UCase(flxResult.Item(12, intRow).Value) & "')"
            AdoCN.Execute(mStrSQL)
        Next
    End Sub

    Private Sub Save()
        Dim rstPacket As ADODB.Recordset
        Dim dblTotPcs As Double
        Dim dblWindowPcs As Double
        Dim dblImpValue As Double
        Dim dblEstValue As Double
        Dim intApproval As Integer
        Dim dblPerc As Double

        If cmbDept.Text <> "" And txtParNo.Text <> "" And txtPktNo.Text <> "" And txtPcs.Text <> "" And txtCts.Text <> "" And cmbColor.Text <> "" And cmbCut.Text <> "" And cmbClarity.Text <> "" And txtEstYld.Text <> "" And txtFinCts.Text <> "" And cmbModel.Text <> "" Then

            dblTotPcs = 0
            dblImpValue = 0
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT OrigParcelNo,IssuedPcs,IssuedCts,Approval FROM tblParcel WHERE GrpParNo = '" & txtParNo.Text & "' AND Depart = 'Rough Planning'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                dblTotPcs = rsComSql_1.Fields("IssuedPcs").Value
                intApproval = rsComSql_1.Fields("Approval").Value

                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT ItemCost, HardCost FROM tblImport WHERE SupParcelNo = '" & rsComSql_1.Fields("OrigParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    If rsComSql_2.Fields("HardCost").Value > 0 Then
                        dblImpValue = Math.Round(rsComSql_1.Fields("IssuedCts").Value * rsComSql_2.Fields("HardCost").Value, 2)
                    Else
                        dblImpValue = Math.Round(rsComSql_1.Fields("IssuedCts").Value * rsComSql_2.Fields("ItemCost").Value, 2)
                    End If
                End If
                rsComSql_2 = Nothing
            End If
            rsComSql_1 = Nothing

            dblWindowPcs = 0
            dblEstValue = 0
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktCts * PktPrice), 2) AS PktValue FROM tblRghPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktType = 6", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                If Not IsDBNull(rsComSql_1.Fields("PktPcs").Value) Then
                    dblWindowPcs = rsComSql_1.Fields("PktPcs").Value
                    dblEstValue = rsComSql_1.Fields("PktValue").Value
                End If
            End If
            rsComSql_1 = Nothing

            If dblTotPcs > dblWindowPcs And intApproval = 0 Then
                MsgBox(dblTotPcs - dblWindowPcs & " pcs pending", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
                Exit Sub
            End If

            If dblImpValue > 0 Then
                dblPerc = 0
                dblPerc = ((dblEstValue - dblImpValue) / dblImpValue) * 100

                If dblPerc < -10 And intApproval = 0 Then
                    If dblImpValue > dblEstValue And intApproval = 0 Then
                        MsgBox(dblImpValue - dblEstValue & " value lost. Get the approval to proceed", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                        ClearFields()
                        Exit Sub
                    End If
                End If
            End If

            If CDbl(txtEstYld.Text) < 0 Then MsgBox("Invalid Estimated Yield", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

            If cmbDept.Text = "RoughBruting" And cmbFlo.Text = "SB" And CDbl(txtTension.Text) >= 70 And CDbl(txtFinCts.Text) >= 0.3 Then
                MsgBox("Invalid Tension", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Len(txtPktNo.Text) <> 4 Then
                MsgBox("Invalid Pkt No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If txtPktNo2.Text = "" Then
                MsgBox("Invalid Ref Pkt No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If cmbFlow.Text = "" Then
                MsgBox("Invalid Flow", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If txtPktID2.Text = "" Then
                MsgBox("Invalid Packet ID", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If txtValue.Text = "" Then
                MsgBox("Invalid Value", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(txtCts.Text) < CDbl(txtFinCts.Text) Then
                MsgBox("Invalid Finish Cts", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(txtEstYld.Text) > 100 Then
                MsgBox("Invalid Est Yield", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If txtWidth2.Text = "" Then
                txtWidth2.Text = "0"
            End If

            If cmbDept.Text = "RoughBruting" Then
                If CDbl(txtPktID2.Text) <= 0 And CDbl(txtPcs.Text) = 1 Then
                    MsgBox("Invalid Packet ID", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If CDbl(txtValue.Text) <= 0 And CDbl(txtPcs.Text) = 1 Then
                    MsgBox("Invalid Value", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If CDbl(txtPcs.Text) = 1 Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT ID FROM tblRPrPacket WHERE ID = '" & CDbl(txtPktID2.Text) & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        MsgBox("Invalid Packet ID", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql = Nothing

                    If Val(cmbPktIDNew.Text) <> 0 Then
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT ID FROM tblRPrReturnDetails WHERE ID = " & Val(cmbPktIDNew.Text) & "", AdoCN, 1, 1)
                        If rsComSql.RecordCount = 0 Then
                            MsgBox("Invalid New Packet ID", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                            cmbPktIDNew.Focus()
                            rsComSql = Nothing
                            Exit Sub
                        End If
                        rsComSql = Nothing
                    End If
                End If
            End If

            rstPacket = New ADODB.Recordset
            rstPacket.Open("SELECT ParNo FROM tblRPrPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
            If rstPacket.RecordCount = 0 Then
                If cmbDept.Text <> "RoughBruting" Then
                    If CDbl(txtPcs.Text) > CDbl(txtAvailPcs.Text) - CDbl(txtPktPcs.Text) Then
                        MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If

                AdoCN.Execute("INSERT INTO tblRPrPacket(Department,ParNo,PktNo,PktPcs,PktCts,Assortment,PktColor,PktIss,PktFlow,PktClarity,PktCut,FinCts,EstYld,Flo,Tension,MainPkt,RefPktNo,Comment,Model,PktID,Value,Sieve,PktSize,PktIDNew,StoneNo,DoneBy,Width) " & _
                              "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "'," & CInt(txtPcs.Text) & "," & CDbl(txtCts.Text) & ",'" & txtAssort.Text & "','" & cmbColor.Text & "'," & _
                                "'" & Format(dtpToday, "MM/dd/yyyy") & "','" & cmbFlow.Text & "','" & cmbClarity.Text & "','" & cmbCut.Text & "'," & CDbl(txtFinCts.Text) & "," & CDbl(txtEstYld.Text) & "," & _
                                "'" & cmbFlo.Text & "'," & CDbl(txtTension.Text) & ",'" & txtMainPkt.Text & "','" & txtPktNo2.Text & "','" & txtComment.Text & "','" & cmbModel.Text & "'," & CDbl(txtPktID2.Text) & "," & _
                                "" & CDbl(txtValue.Text) & ",'" & cmbSize2.Text & "','" & cmbSizeRange.Text & "'," & CDbl(cmbPktIDNew.Text) & ",'" & txtStoneNo2.Text & "','" & PBUser_EmpNo & "','" & txtWidth2.Text & "')")

                AdoCN.Execute("UPDATE tblRPrPacket SET ID = ID2 WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' And Department = '" & cmbDept.Text & "'")

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT IssuedPcs FROM tblParcel WHERE GrpParNo = '" & txtParNo.Text & "' AND Depart = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktCts), 3) AS PktCts FROM tblRPrPacket WHERE (ParNo = '" & txtParNo.Text & "') AND (Department = '" & cmbDept.Text & "')", AdoCN, 1, 1)
                    If Not IsDBNull(rsComSql_2.Fields("PktPcs").Value) Then
                        If rsComSql_1.Fields("IssuedPcs").Value < rsComSql_2.Fields("PktPcs").Value Then
                            AdoCN.Execute("UPDATE tblParcel SET IssuedPcs = " & rsComSql_2.Fields("PktPcs").Value & ",RghPcs = " & rsComSql_2.Fields("PktPcs").Value & ", IssuedCts = " & rsComSql_2.Fields("PktCts").Value & ", RghCts = " & rsComSql_2.Fields("PktCts").Value & " " & _
                                          "WHERE GrpParNo = '" & txtParNo.Text & "' AND Depart = '" & cmbDept.Text & "'")
                        End If
                    End If
                    rsComSql_2 = Nothing
                End If
                rsComSql_1 = Nothing

                ClearFields()
            Else
                AdoCN.Execute("UPDATE tblRPrPacket SET Assortment = '" & txtAssort.Text & "',PktColor = '" & cmbColor.Text & "',PktFlow = '" & cmbFlow.Text & "',PktClarity = '" & cmbClarity.Text & "'," & _
                                "PktCut = '" & cmbCut.Text & "',FinCts = " & CDbl(txtFinCts.Text) & ",EstYld = " & CDbl(txtEstYld.Text) & ",Flo = '" & cmbFlo.Text & "',Tension = " & CDbl(txtTension.Text) & "," & _
                                "MainPkt = '" & txtMainPkt.Text & "',RefPktNo = '" & txtPktNo2.Text & "',Comment = '" & txtComment.Text & "',Model = '" & cmbModel.Text & "',PktID = " & CDbl(txtPktID2.Text) & "," & _
                                "Value = " & CDbl(txtValue.Text) & ",Sieve = '" & cmbSize2.Text & "',PktSize = '" & cmbSizeRange.Text & "',StoneNo = '" & txtStoneNo2.Text & "',Width = '" & txtWidth2.Text & "' " & _
                              "WHERE ParNo = '" & txtParNo.Text & "' And PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "'")

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT ParNo FROM tblRPrIssues WHERE ParNo = '" & txtParNo.Text & "' And PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 3)
                If rsComSql.RecordCount = 0 Then
                    AdoCN.Execute("UPDATE tblRPrPacket SET PktPcs = " & CInt(txtPcs.Text) & ",PktCts = " & CDbl(txtCts.Text) & " " & _
                                  "WHERE ParNo = '" & txtParNo.Text & "' And PktNo = '" & txtPktNo.Text & "' And Department = '" & cmbDept.Text & "'")
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT ParNo FROM tblRPrReturns WHERE ParNo = '" & txtParNo.Text & "' And PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "' AND Sec = 20", AdoCN, 1, 3)
                If rsComSql.RecordCount Then
                    AdoCN.Execute("UPDATE tblRPrPacket SET Trf = '" & IIf(chkTrf.Checked = True, 1, 0) & "' " & _
                                  "WHERE ParNo = '" & txtParNo.Text & "' And PktNo = '" & txtPktNo.Text & "' And Department = '" & cmbDept.Text & "'")
                End If
                rsComSql = Nothing

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT IssuedPcs FROM tblParcel WHERE GrpParNo = '" & txtParNo.Text & "' AND Depart = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktCts), 3) AS PktCts FROM tblRPrPacket WHERE (ParNo = '" & txtParNo.Text & "') AND (Department = '" & cmbDept.Text & "')", AdoCN, 1, 1)
                    If Not IsDBNull(rsComSql_2.Fields("PktPcs").Value) Then
                        If rsComSql_1.Fields("IssuedPcs").Value < rsComSql_2.Fields("PktPcs").Value Then
                            AdoCN.Execute("UPDATE tblParcel SET IssuedPcs = " & rsComSql_2.Fields("PktPcs").Value & ",RghPcs = " & rsComSql_2.Fields("PktPcs").Value & ", IssuedCts = " & rsComSql_2.Fields("PktCts").Value & ", RghCts = " & rsComSql_2.Fields("PktCts").Value & " " & _
                                          "WHERE GrpParNo = '" & txtParNo.Text & "' AND Depart = '" & cmbDept.Text & "'")
                        End If
                    End If
                    rsComSql_2 = Nothing
                End If
                rsComSql_1 = Nothing

                If cmbDept.Text = "RoughPlan" Then
                    Insert_PacketDetails()
                    If chkFinish.Checked = True Then
                        Insert_FinishDetails()
                    End If
                End If

                ClearFields()
            End If
            rstPacket = Nothing
            txtParNo.Focus()
        Else
            MsgBox("Please fill all the entries before Saving", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            txtParNo.Focus()
        End If

    End Sub

    Private Sub Delete()
        Dim rstPacket As ADODB.Recordset

        If cmbDept.Text <> "" And txtParNo.Text <> "" And txtPktNo.Text <> "" Then
            PBResponse = MsgBox("Are you sure to Delete this Packet?", MsgBoxStyle.Information + vbYesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                rstPacket = New ADODB.Recordset
                rstPacket.Open("SELECT * FROM tblRPrPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                If rstPacket.RecordCount Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblRPrIssues WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        AdoCN.Execute("DELETE FROM tblRPrPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "'")

                        Insert_Log("RPR PACKET DELETE", cmbDept.Text, txtParNo.Text, txtPktNo.Text, 0)

                        MsgBox("Packet Deleted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        ClearFields()
                    Else
                        MsgBox("Already Issued", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    End If
                    rsComSql = Nothing
                Else
                    MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                End If
                rstPacket = Nothing
            End If
        Else
            MsgBox("Please fill all the entries before Delete", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub ClearFields()
        txtPktNo.Text = ""
        txtLotNo.Text = ""
        txtPcs.Text = ""
        txtPcs2.Text = ""
        txtRghPcs.Text = ""
        txtCts.Text = ""
        txtAssort.Text = ""
        txtSupParNo.Text = ""
        cmbColor.Text = "-"
        cmbFlow.Text = ""
        cmbClarity.Text = "-"
        cmbCut.Text = "-"
        cmbModel.Text = ""
        txtEstYld.Text = "0"
        txtFinCts.Text = "0"
        txtPktID.Text = "0"
        cmbFlo.Text = ""
        txtTension.Text = "0"
        txtMainPkt.Text = ""
        txtAvailPcs.Text = "0"
        txtAvailCts.Text = "0"
        txtPktPcs.Text = "0"
        txtBrPcs.Text = "0"
        txtPrPcs.Text = "0"
        txtPrice.Text = "0"
        txtPlanValue.Text = "0"
        cmbColor2.Text = ""
        cmbColor3.Text = ""
        cmbClarity2.Text = ""
        cmbClarity3.Text = ""
        cmbShape.Text = ""
        cmbShape2.Text = ""
        txtRghCts.Text = "0"
        txtRghCts2.Text = "0"
        txtFinCts2.Text = "0"
        txtFinCts3.Text = "0"
        flxDetails.Rows.Clear()
        flxPacket.Rows.Clear()
        flxResult.Rows.Clear()
        txtPktNo2.Text = ""
        txtMaxPkt.Text = ""
        txtRghPktNo.Text = ""
        txtComment.Text = ""
        txtWidth.Text = ""
        cmbPlan.Text = "Plan1"
        cmbCut2.Text = "Very Good"
        cmbCut3.Text = "Very Good"
        txtLen.Text = ""
        txtWid.Text = ""
        txtParNo.Text = UCase(txtParNo.Text)
        txtValuePkts.Text = ""
        txtChkPkts.Text = ""
        txtValuePcs.Text = ""
        prgBarCopy.Visible = False
        prgBarCopy.Value = 0
        txtPktID2.Text = "0"
        txtValue.Text = "0"
        cmbSize2.Text = ""
        txtWidth2.Text = ""
        cmbSizeRange.Text = ""
        txtDepartment.Text = ""
        cmbPktIDNew.Text = "0"
        cmbPktIDNew.Items.Clear()
        chkFinish.Checked = False
        chkTrf.Checked = False
        txtStoneNo.Text = ""
        txtStoneNo2.Text = ""

        Load_ParcelDetails()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmbColor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbColor.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbClarity.Focus()
        End If
    End Sub

    Private Sub cmbClarity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbClarity.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbFlo.Focus()
        End If
    End Sub

    Private Sub cmbFlo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbFlo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbModel.Focus()
        End If
    End Sub

    Private Sub cmbCut_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbCut.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If cmbDept.Text = "RoughBruting" Then
                txtFinCts.Focus()
            Else
                cmbSize2.Focus()
            End If
        End If
    End Sub

    Private Sub txtWidth_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWidth.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtWidth.Text)
        If Asc(e.KeyChar) = 13 Then
            cmbClarity2.Focus()
        End If
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        objForm = New frm_DCLReportViewer
        mReportName = "RgfPKTSLEEVE_FULL.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub cmbShape_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbShape.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbColor2.Focus()
            If cmbShape.Text = "PCU" Or cmbShape.Text = "PCU2" Then
                cmbClarity2.Text = "IF"
            End If
        End If
    End Sub

    Private Sub cmbColor2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbColor2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbSize.Focus()
        End If
    End Sub

    Private Sub cmbClarity2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbClarity2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtPrice.Focus()
        End If
    End Sub

    Private Sub cmbPlan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbPlan.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtWidth.Focus()
        End If
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        txtRghCts.Text = flxDetails.Item(0, flxDetails.CurrentRow.Index).Value
        txtFinCts2.Text = flxDetails.Item(1, flxDetails.CurrentRow.Index).Value
        cmbShape.Text = flxDetails.Item(2, flxDetails.CurrentRow.Index).Value
        cmbColor2.Text = flxDetails.Item(3, flxDetails.CurrentRow.Index).Value
        cmbSize.Text = flxDetails.Item(6, flxDetails.CurrentRow.Index).Value
        txtWidth.Text = flxDetails.Item(8, flxDetails.CurrentRow.Index).Value
        cmbClarity2.Text = flxDetails.Item(4, flxDetails.CurrentRow.Index).Value
        txtPrice.Text = flxDetails.Item(5, flxDetails.CurrentRow.Index).Value
        cmbPlan.Text = flxDetails.Item(7, flxDetails.CurrentRow.Index).Value
        cmbCut2.Text = flxDetails.Item(10, flxDetails.CurrentRow.Index).Value
        txtRghPcs.Text = flxDetails.Item(9, flxDetails.CurrentRow.Index).Value
    End Sub

    Private Sub flxDetails_DoubleClick(sender As Object, e As EventArgs) Handles flxDetails.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)
        End If
    End Sub

    Private Sub CopyPlan1Plan2()

        If txtParNo.Text <> "" Then
            prgBarCopy.Visible = True
            prgBarCopy.Value = 0
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT PktNo FROM tblRPrPacketDetails WHERE ParNo = '" & txtParNo.Text & "' AND Department = 'RoughPlan' GROUP BY PktNo ORDER BY PktNo", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                prgBarCopy.Maximum = rsComSql.RecordCount
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT PktNo FROM tblRPrPacketDetails2 WHERE ParNo = '" & txtParNo.Text & "' AND Department = 'RoughPlan' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount = 0 Then

                        rsComSql_2 = New ADODB.Recordset
                        rsComSql_2.Open("SELECT * FROM tblRPrPacketDetails WHERE ParNo = '" & txtParNo.Text & "' AND Department = 'RoughPlan' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "' ORDER BY ID", AdoCN, 1, 1)
                        If rsComSql_2.RecordCount Then
                            rsComSql_2.MoveFirst()
                            While Not rsComSql_2.EOF
                                AdoCN.Execute("INSERT INTO tblRPrPacketDetails2(Department,ParNo,PktNo,RghCts,FinCts,Shape,Color,Clarity,Value,Size,Width,Cut) " & _
                                              "VALUES('RoughPlan','" & txtParNo.Text & "','" & rsComSql_2.Fields("PktNo").Value & "'," & rsComSql_2.Fields("RghCts").Value & "," & _
                                                    "" & rsComSql_2.Fields("FinCts").Value & ",'" & rsComSql_2.Fields("Shape").Value & "'," & _
                                                    "'" & rsComSql_2.Fields("Color").Value & "','" & rsComSql_2.Fields("Clarity").Value & "'," & _
                                                    "" & rsComSql_2.Fields("Value").Value & ",'" & rsComSql_2.Fields("Size").Value & "'," & rsComSql_2.Fields("Width").Value & ",'" & rsComSql_2.Fields("Cut").Value & "')")

                                rsComSql_2.MoveNext()
                            End While
                        End If
                        rsComSql_2 = Nothing

                    End If
                    rsComSql_1 = Nothing

                    prgBarCopy.Value = prgBarCopy.Value + 1
                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing
            prgBarCopy.Value = 0
            prgBarCopy.Visible = False
            MsgBox("Successfully Copied from PLAN 1 to PLAN 2", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        Else
            MsgBox("Invalid Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub CopyPlan1Plan3()

        If txtParNo.Text <> "" Then
            prgBarCopy.Visible = True
            prgBarCopy.Value = 0
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT PktNo FROM tblRPrPacketDetails WHERE ParNo = '" & txtParNo.Text & "' AND Department = 'RoughPlan' GROUP BY PktNo ORDER BY PktNo", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                prgBarCopy.Maximum = rsComSql.RecordCount
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT PktNo FROM tblRPrPacketDetails3 WHERE ParNo = '" & txtParNo.Text & "' AND Department = 'RoughPlan' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount = 0 Then

                        rsComSql_2 = New ADODB.Recordset
                        rsComSql_2.Open("SELECT * FROM tblRPrPacketDetails WHERE ParNo = '" & txtParNo.Text & "' AND Department = 'RoughPlan' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "' ORDER BY ID", AdoCN, 1, 1)
                        If rsComSql_2.RecordCount Then
                            rsComSql_2.MoveFirst()
                            While Not rsComSql_2.EOF
                                AdoCN.Execute("INSERT INTO tblRPrPacketDetails3(Department,ParNo,PktNo,RghCts,FinCts,Shape,Color,Clarity,Value,Size,Width,Cut) " & _
                                              "VALUES('RoughPlan','" & txtParNo.Text & "','" & rsComSql_2.Fields("PktNo").Value & "'," & rsComSql_2.Fields("RghCts").Value & "," & _
                                                    "" & rsComSql_2.Fields("FinCts").Value & ",'" & rsComSql_2.Fields("Shape").Value & "'," & _
                                                    "'" & rsComSql_2.Fields("Color").Value & "','" & rsComSql_2.Fields("Clarity").Value & "'," & _
                                                    "" & rsComSql_2.Fields("Value").Value & ",'" & rsComSql_2.Fields("Size").Value & "'," & rsComSql_2.Fields("Width").Value & ",'" & rsComSql_2.Fields("Cut").Value & "')")

                                rsComSql_2.MoveNext()
                            End While
                        End If
                        rsComSql_2 = Nothing

                    End If
                    rsComSql_1 = Nothing

                    prgBarCopy.Value = prgBarCopy.Value + 1
                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing
            prgBarCopy.Value = 0
            prgBarCopy.Visible = False
            MsgBox("Successfully Copied from PLAN 1 to PLAN 3", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        Else
            MsgBox("Invalid Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub cmdCopy_Click(sender As Object, e As EventArgs) Handles cmdCopy.Click
        If UCase(PBUser_ID) = "MANJULA" Or UCase(PBUser_ID) = "CHAMEERA" Then
            PBResponse = MsgBox("Are you sure to copy from PLAN 1 to PLAN 2?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                CopyPlan1Plan2()
            End If
        Else
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub cmbModel_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbModel.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbCut.Focus()
        End If
    End Sub

    Private Sub txtPktID2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktID2.KeyPress
        Dim strOrgParNo As String
        Dim dtLoading As New DataTable("Parcels")
        Dim strShape As String

        dtLoading.Columns.Add("ID", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("RghCts", System.Type.GetType("System.String"))

        strShape = "Rounds"

        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If Val(txtPktID2.Text) > 0 Then
                If Mid(txtParNo.Text, 2, 1) = "R" Then
                    strShape = "Rounds"
                End If
                If Mid(txtParNo.Text, 2, 1) = "B" Then
                    strShape = "Baguettes"
                End If
                If Mid(txtParNo.Text, 2, 1) = "E" Then
                    strShape = "Emerald"
                End If

                strParNo = ""
                strPktNo = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT ParNo,PktNo,PktCut,Tension,Flo,Model FROM tblRprPacket WHERE ID = " & Val(txtPktID2.Text) & "", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strParNo = rsComSql.Fields("ParNo").Value
                    strPktNo = rsComSql.Fields("PktNo").Value
                    cmbCut.Text = Trim(rsComSql.Fields("PktCut").Value)
                    txtTension.Text = Trim(rsComSql.Fields("Tension").Value)

                    cmbFlo.Text = rsComSql.Fields("Flo").Value
                    cmbModel.Text = rsComSql.Fields("Model").Value
                End If
                rsComSql = Nothing

                strOrgParNo = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT OrigParcelNo FROM tblParcel WHERE GrpParNo = '" & strParNo & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strOrgParNo = Trim(rsComSql.Fields("OrigParcelNo").Value)
                End If
                rsComSql = Nothing

                If txtSupParNo.Text <> strOrgParNo Then
                    MsgBox("Invalid Packet ID", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    txtPktID.Text = ""
                    txtPktID.Focus()
                    Exit Sub
                End If

                cmbPktIDNew.Items.Clear()
                rsComSql = New ADODB.Recordset
                Select Case strShape
                    Case "Rounds"
                        rsComSql.Open("SELECT * FROM tblRPrReturnDetails WHERE ParNo = '" & strParNo & "' AND PktNo = '" & strPktNo & "' AND Shape = 'Rounds' ORDER BY ID", AdoCN, 1, 1)
                    Case "Baguettes"
                        rsComSql.Open("SELECT * FROM tblRPrReturnDetails WHERE ParNo = '" & strParNo & "' AND PktNo = '" & strPktNo & "' AND (Shape = 'Baguettes' OR Shape = 'PCU2') ORDER BY ID", AdoCN, 1, 1)
                    Case "Emerald"
                        rsComSql.Open("SELECT * FROM tblRPrReturnDetails WHERE ParNo = '" & strParNo & "' AND PktNo = '" & strPktNo & "' AND (Shape = 'Emerald') ORDER BY ID", AdoCN, 1, 1)
                End Select
                If rsComSql.RecordCount Then
                    If rsComSql.RecordCount = 1 Then
                        cmbColor.Text = rsComSql.Fields("Color").Value
                        cmbClarity.Text = Trim(rsComSql.Fields("Clarity").Value)
                        txtPcs.Text = Trim(rsComSql.Fields("Pcs").Value)
                        txtCts.Text = Trim(rsComSql.Fields("RghCts").Value)
                        txtFinCts.Text = Trim(rsComSql.Fields("FinCts").Value)
                        txtValue.Text = Trim(rsComSql.Fields("Value").Value)
                        cmbSize2.Text = Trim(rsComSql.Fields("Length").Value)
                        txtWidth2.Text = Trim(rsComSql.Fields("Width").Value)
                        cmbCut.Text = Trim(rsComSql.Fields("Cut").Value)
                        cmbPktIDNew.Text = Trim(rsComSql.Fields("ID").Value)
                        txtStoneNo2.Text = Trim(rsComSql.Fields("StoneNo").Value)
                        'If Trim(rsComSql.Fields("Cut").Value) = "VG" Then
                        '    cmbCut.Text = "Very Good"
                        'ElseIf Trim(rsComSql.Fields("Cut").Value) = "EX" Then
                        '    cmbCut.Text = "Excellent"
                        'Else
                        '    cmbCut.Text = Trim(rsComSql.Fields("Cut").Value)
                        'End If
                        cmbSize2.Focus()

                        If txtFinCts.Text <> "" And txtCts.Text <> "" Then
                            If CDbl(txtFinCts.Text) > 0 And CDbl(txtCts.Text) > 0 Then
                                txtEstYld.Text = Format(Math.Round((CDbl(txtFinCts.Text) / CDbl(txtCts.Text)) * 100, 2), "#0.00")
                            End If
                        End If
                    Else
                        rsComSql.MoveFirst()
                        While Not rsComSql.EOF
                            Dim dr As DataRow
                            dr = dtLoading.NewRow

                            dr("ID") = rsComSql.Fields("ID").Value
                            dr("RghCts") = rsComSql.Fields("RghCts").Value
                            dtLoading.Rows.Add(dr)

                            rsComSql.MoveNext()
                        End While
                        cmbPktIDNew.Focus()
                    End If
                End If
                rsComSql = Nothing

                cmbPktIDNew.SelectedIndex = -1
                cmbPktIDNew.Items.Clear()
                cmbPktIDNew.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
                cmbPktIDNew.SourceDataString = New String(1) {"ID", "RghCts"}
                cmbPktIDNew.SourceDataTable = dtLoading

                txtPcs.Focus()
            Else
                txtPcs.Focus()
            End If
        End If
    End Sub

    Private Sub cmdPrint2_Click(sender As Object, e As EventArgs) Handles cmdPrint2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "RgfPKTSLEEVE_Bruting.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub txtValue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtValue.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtValue.Text)
        If Asc(e.KeyChar) = 13 Then
            If cmbDept.Text = "RoughBruting" Then
                cmbSize2.Focus()
            Else
                cmbClarity2.Focus()
            End If
        End If
    End Sub

    Private Sub cmbShape_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbShape.SelectedIndexChanged
        If cmbShape.Text = "Princess" Or cmbShape.Text = "Emerald" Then
            cmbSize.Text = "0"
            txtWidth.Text = "0"
        ElseIf cmbShape.Text = "Rounds" Then
            cmbSize.Text = ""
            txtWidth.Text = ""
        Else
            If cmbShape.Text = "PCU" Then
                txtWidth.Text = "0"
            End If
        End If
    End Sub

    Private Sub txtFinCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFinCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtFinCts.Text)
        If Asc(e.KeyChar) = 13 Then
            If txtFinCts.Text <> "" And txtCts.Text <> "" Then
                If CDbl(txtFinCts.Text) > 0 And CDbl(txtCts.Text) > 0 Then
                    txtEstYld.Text = Format(Math.Round((CDbl(txtFinCts.Text) / CDbl(txtCts.Text)) * 100, 2), "#0.00")
                    txtEstYld.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub txtEstYld_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEstYld.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtEstYld.Text)
        If Asc(e.KeyChar) = 13 Then
            If txtPcs.Text <> "" And txtEstYld.Text <> "" Then
                If CDbl(txtPcs.Text) > 0 Then
                    txtFinCts.Text = Math.Round(CDbl(txtCts.Text) * CDbl(txtEstYld.Text) / 100, 3)
                    If Val(txtPcs.Text) <> 1 Then
                        txtValue.Text = Math.Round(CDbl(txtFinCts.Text) * 600, 2)
                    End If
                End If
            End If
            If cmbDept.Text = "RoughBruting" Then
                txtTension.Focus()
            Else
                cmbModel.Focus()
            End If
        End If
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxPacket)
    End Sub

    Private Sub txtTension_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTension.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtValue.Focus()
        End If
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "RprPKTSLEEVE_FULL4in1Bruting.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub flxResult_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxResult.CellClick
        txtPcs2.Text = flxResult.Item(2, flxResult.CurrentRow.Index).Value
        txtRghCts2.Text = flxResult.Item(3, flxResult.CurrentRow.Index).Value
        txtFinCts3.Text = flxResult.Item(4, flxResult.CurrentRow.Index).Value
        cmbShape2.Text = flxResult.Item(1, flxResult.CurrentRow.Index).Value
        cmbColor3.Text = flxResult.Item(5, flxResult.CurrentRow.Index).Value
        cmbClarity3.Text = flxResult.Item(6, flxResult.CurrentRow.Index).Value
        txtPlanValue.Text = flxResult.Item(7, flxResult.CurrentRow.Index).Value
        txtDepartment.Text = flxResult.Item(0, flxResult.CurrentRow.Index).Value
        cmbCut3.Text = flxResult.Item(8, flxResult.CurrentRow.Index).Value
        txtLen.Text = flxResult.Item(9, flxResult.CurrentRow.Index).Value
        txtWid.Text = flxResult.Item(10, flxResult.CurrentRow.Index).Value
        txtStoneNo.Text = flxResult.Item(12, flxResult.CurrentRow.Index).Value
    End Sub

    Private Sub flxResult_DoubleClick(sender As Object, e As EventArgs) Handles flxResult.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxResult.Rows.RemoveAt(flxResult.CurrentRow.Index)
        End If
    End Sub

    Private Sub txtRghCts2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRghCts2.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRghCts2.Text)
        If Asc(e.KeyChar) = 13 Then
            txtFinCts3.Focus()
        End If
    End Sub

    Private Sub txtFinCts3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFinCts3.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtFinCts3.Text)
        If Asc(e.KeyChar) = 13 Then
            cmbShape2.Focus()
        End If
    End Sub

    Private Sub cmbShape2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbShape2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbColor3.Focus()
            If cmbShape2.Text = "PCU" Or cmbShape2.Text = "PCU2" Then
                cmbClarity3.Text = "IF"
            End If
        End If
    End Sub

    Private Sub cmbColor3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbColor3.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbClarity3.Focus()
        End If
    End Sub

    Private Sub cmbClarity3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbClarity3.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtPlanValue.Focus()
        End If
    End Sub

    Private Sub txtPlanValue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPlanValue.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtPlanValue.Text)
        If Asc(e.KeyChar) = 13 Then
            cmbCut3.Focus()
        End If
    End Sub

    Private Sub cmdAdd2_Click(sender As Object, e As EventArgs) Handles cmdAdd2.Click
        Dim dblPlanValue As Double

        Dim strLength As String

        Dim strCode As String
        Dim strColor As String
        Dim strClarity As String
        Dim strCut As String

        Dim dblPcs As Double
        Dim dblCts As Double
        Dim dblOrigValue As Double

        If txtParNo.Text <> "" And txtPktNo.Text <> "" And Mid(cmbDept.Text, 1, 9) = "RoughPlan" Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblRPrPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' And Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                rsComSql = Nothing
                Exit Sub
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblRPrReturns WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = 20  And Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Return", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                rsComSql = Nothing
                Exit Sub
            End If
            rsComSql = Nothing
        Else
            MsgBox("Invalid Parcel No/Packet No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtCts.Text = "" Then
            MsgBox("Invalid Packet Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If txtPcs2.Text = "" Then
            MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If CDbl(txtPcs2.Text) <= 0 Then
            MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If txtRghCts2.Text = "" Then
            MsgBox("Invalid Est Rgh Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If CDbl(txtRghCts2.Text) <= 0 Then
            MsgBox("Invalid Est Rgh Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If txtFinCts3.Text = "" Then
            MsgBox("Invalid Est Finish Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If CDbl(txtFinCts3.Text) <= 0 Then
            MsgBox("Invalid Est Finish Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If cmbShape2.Text = "" Then
            MsgBox("Invalid Shape", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If cmbColor3.Text = "" Then
            MsgBox("Invalid Color", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If CDbl(txtRghCts2.Text) < CDbl(txtFinCts3.Text) Then
            MsgBox("Invalid Est Finish Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblRPrColor WHERE ColorCode = '" & cmbColor3.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Color", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            rsComSql = Nothing
            Exit Sub
        End If
        rsComSql = Nothing
        If cmbClarity3.Text = "" Then
            MsgBox("Invalid Clarity", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblRPrClarity WHERE ClarityCode = '" & cmbClarity3.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Clarity", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            rsComSql = Nothing
            Exit Sub
        End If
        rsComSql = Nothing

        If txtPlanValue.Text = "" Then
            MsgBox("Invalid Plan Value", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If CDbl(txtPlanValue.Text) <= 0 Then
            MsgBox("Invalid Plan Value", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If CDbl(txtCts.Text) < CDbl(txtRghCts2.Text) Then
            MsgBox("Invalid Est Rough Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If CDbl(txtCts.Text) < CDbl(txtFinCts3.Text) Then
            MsgBox("Invalid Est Finish Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If cmbCut3.Text = "" Then
            MsgBox("Invalid Cut", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblRPrCut WHERE Cut = '" & cmbCut3.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Cut", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        'If cmbCut3.Text <> "VG" And cmbCut3.Text <> "XXX EX" And cmbCut3.Text <> "EX" And cmbCut3.Text <> "EX–IDEAL" And cmbCut3.Text <> "GOOD" Then
        '    MsgBox("Invalid Cut", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '    Exit Sub
        'End If
        If txtLen.Text = "" Then
            MsgBox("Invalid Length", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If txtWid.Text = "" Then
            MsgBox("Invalid Width", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If cmbSize3.Text = "" Then
            MsgBox("Invalid Size", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If txtStoneNo.Text = "" Then
            MsgBox("Invalid Stone No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        'If UCase(cmbCut3.Text) = "XXX EX" Then
        '    If CDbl(txtFinCts3.Text) < 0.18 Then
        '        MsgBox("Invalid Cut and Finish Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '        Exit Sub
        '    End If
        'End If

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblRPrShape WHERE Shape = '" & cmbShape2.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Shape", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            rsComSql = Nothing
            Exit Sub
        End If
        rsComSql = Nothing

        dblPlanValue = 0
        If cmbShape2.Text = "Baguettes" Then
            dblPlanValue = 0
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT AVG(dbo.VW_BAGAssort2020.ListCost) AS ListCost " & _
                            "FROM dbo.VW_BAGAssort2020 INNER JOIN dbo.tblRPrCodes ON dbo.VW_BAGAssort2020.Color = dbo.tblRPrCodes.SysCode INNER JOIN " & _
                                "dbo.tblRPrCodes AS tblRPrCodes_1 ON dbo.VW_BAGAssort2020.Clarity = tblRPrCodes_1.SysCode " & _
                            "WHERE (dbo.tblRPrCodes.SysName = '" & Trim(cmbColor3.Text) & "') AND (tblRPrCodes_1.SysName = '" & Trim(cmbClarity3.Text) & "') AND (dbo.VW_BAGAssort2020.LengthFrom <= '" & txtLen.Text & "') AND (dbo.VW_BAGAssort2020.LengthTo >= '" & txtLen.Text & "') AND (dbo.VW_BAGAssort2020.WidthFrom <= '" & txtWid.Text & "')  " & _
                                "AND (dbo.VW_BAGAssort2020.WidthTo >= '" & txtWid.Text & "')", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                If Not IsDBNull(rsComSql_1.Fields("ListCost").Value) Then
                    dblPlanValue = Math.Round(rsComSql_1.Fields("ListCost").Value * CDbl(txtFinCts3.Text), 0)
                    txtPlanValue.Text = dblPlanValue
                Else
                    dblPlanValue = 0
                    txtPlanValue.Text = dblPlanValue
                End If
            End If
            rsComSql_1 = Nothing

        ElseIf cmbShape2.Text = "Princess" Then
            dblPlanValue = 0
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT AVG(dbo.VW_PRAssort2020.ListCost) AS ListCost " & _
                            "FROM dbo.VW_PRAssort2020 INNER JOIN dbo.tblRPrCodes ON dbo.VW_PRAssort2020.Color = dbo.tblRPrCodes.SysCode INNER JOIN " & _
                                "dbo.tblRPrCodes AS tblRPrCodes_1 ON dbo.VW_PRAssort2020.Clarity = tblRPrCodes_1.SysCode " & _
                            "WHERE (dbo.tblRPrCodes.SysName = '" & Trim(cmbColor3.Text) & "') AND (tblRPrCodes_1.SysName = '" & Trim(cmbClarity3.Text) & "') AND (dbo.VW_PRAssort2020.LengthFrom <= '" & txtLen.Text & "') AND (dbo.VW_PRAssort2020.LengthTo >= '" & txtLen.Text & "')", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                If Not IsDBNull(rsComSql_1.Fields("ListCost").Value) Then
                    dblPlanValue = Math.Round(rsComSql_1.Fields("ListCost").Value * CDbl(txtFinCts3.Text), 0)
                    txtPlanValue.Text = dblPlanValue
                End If
            End If
            rsComSql_1 = Nothing

        ElseIf cmbShape2.Text = "Rounds" Then
            strLength = txtLen.Text
            strCut = cmbCut3.Text
            strColor = cmbColor3.Text
            strClarity = cmbClarity3.Text
            dblPcs = CDbl(txtPcs2.Text)
            dblCts = CDbl(txtRghCts2.Text)
            dblOrigValue = CDbl(txtPlanValue.Text)

            'If strCut = "Very Good" Then
            '    strCut = "VG"
            'End If

            'If UCase(strRight(strCut, 5)) = "IDEAL" Then
            '    strCut = "EX-IDEAL"
            'End If

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
                    '                "WHERE (Color = '" & strColor & "') AND (Clarity = '" & strClarity & "') AND (Cut = 'VG')", AdoCN, 1, 1)
                    'If rsComSql_1.RecordCount Then
                    '    strCode = rsComSql_1.Fields("Code").Value
                    'End If
                    'rsComSql_1 = Nothing
                End If

                If strCode <> "" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT FromLen, ToLen, [EXIDEAL-DEF-NON-IFVVS2], [EXIDEAL-G-NON-IFVVS21], [EX-DEF-NON-IFVVS2], [EX-G-NON-IFVVS2], [VG-IFVVS-DEF], [VG-IFVVS-G], [D-G/VS1], [D-G/VS2], [D-G/SI1], [D-G/I2], [D-H/SI3], [D-H/I1], [H/VVS], [H/VS], " & _
                                        "[H/SI1], [H/SI2], [I/IF-VS], [I/SI-SI3], [J/IF-VS2], [J/SI1-I1], [KL/IF-SI1], [KL/SI2-I1], [MN/IF-I3], [TLB/IF-SI1], [TLB/SI2-I1], [DI/I2-13], [I/I1] " & _
                                    "FROM dbo.VW_RndPriceList2 " & _
                                    "WHERE (FromLen <= '" & strLength & "') AND (ToLen > '" & strLength & "')", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        dblPlanValue = IIf(Not IsDBNull(rsComSql_1.Fields(strCode).Value), rsComSql_1.Fields(strCode).Value, 0)
                        dblPlanValue = Math.Round(dblPlanValue * CDbl(txtFinCts3.Text), 0)
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
                    'rsComSql_1 = New ADODB.Recordset
                    'rsComSql_1.Open("SELECT Code, Cut, Color, Clarity " & _
                    '                "FROM dbo.VW_RndPriceListCodeL " & _
                    '                "WHERE (Color = '" & strColor & "') AND (Clarity = '" & strClarity & "') AND (Cut = 'VG')", AdoCN, 1, 1)
                    'If rsComSql_1.RecordCount Then
                    '    strCode = rsComSql_1.Fields("Code").Value
                    'End If
                    'rsComSql_1 = Nothing
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
                        dblPlanValue = Math.Round(dblPlanValue * CDbl(txtFinCts3.Text), 0)
                    End If
                    rsComSql_1 = Nothing
                End If
            End If
            If dblPlanValue <> 0 Then
                txtPlanValue.Text = dblPlanValue
            End If

        ElseIf cmbShape2.Text = "PCU" Then
            dblPlanValue = 0

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT Pcs, Value FROM tblRPrPacketDetails WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Shape = '" & cmbShape2.Text & "' AND Size = '" & cmbSize3.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                dblPlanValue = Math.Round((rsComSql.Fields("Value").Value / rsComSql.Fields("Pcs").Value) * CDbl(txtPcs2.Text), 0)
            Else
                dblPlanValue = Math.Round(CDbl(txtPlanValue.Text), 0)
            End If
            rsComSql = Nothing
            txtPlanValue.Text = dblPlanValue
        End If

        flxResult.Rows.Add(txtDepartment.Text,
                           cmbShape2.Text,
                           txtPcs2.Text,
                           txtRghCts2.Text,
                           txtFinCts3.Text,
                           cmbColor3.Text,
                           cmbClarity3.Text,
                           txtPlanValue.Text,
                           cmbCut3.Text,
                           txtLen.Text,
                           txtWid.Text,
                           cmbSize3.Text,
                           UCase(txtStoneNo.Text))

        txtPcs2.Text = ""
        txtRghCts2.Text = ""
        txtFinCts3.Text = ""
        cmbShape2.Text = ""
        cmbColor3.Text = ""
        cmbClarity3.Text = ""
        txtPlanValue.Text = ""
        cmbCut3.Text = "VG"
        txtLen.Text = ""
        txtWid.Text = ""
        cmbSize3.Text = ""
        txtStoneNo.Text = ""
        txtPcs2.Focus()
    End Sub

    Private Sub txtPcs2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs2.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtRghCts2.Focus()
        End If
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Delete()
    End Sub

    Private Sub txtRghPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRghPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            cmbCut2.Focus()
        End If
    End Sub

    Private Sub cmbCut2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbCut2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtRghPcs.Text = "1"
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub cmbCut3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbCut3.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtLen.Focus()
        End If
    End Sub

    Private Sub txtLen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLen.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtLen.Text)
        If Asc(e.KeyChar) = 13 Then
            If cmbShape2.Text = "Rounds" Or cmbShape2.Text = "Princess" Or cmbShape2.Text = "Asscher" Or cmbShape2.Text = "Carrer" Then
                txtWid.Text = txtLen.Text
            End If
            txtWid.Focus()
        End If
    End Sub

    Private Sub txtWid_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWid.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtWid.Text)
        If Asc(e.KeyChar) = 13 Then
            cmdAdd2.Focus()
        End If
    End Sub

    Private Sub cmdCopy2_Click(sender As Object, e As EventArgs) Handles cmdCopy2.Click
        If UCase(PBUser_ID) = "MANJULA" Or UCase(PBUser_ID) = "CHAMEERA" Then
            PBResponse = MsgBox("Are you sure to copy from PLAN 1 to PLAN 3?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse  = MsgBoxResult.Yes Then
                CopyPlan1Plan3()
            End If
        Else
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub cmbSize2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSize2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbSizeRange.Focus()
        End If
    End Sub

    Private Sub cmbPktIDNew_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPktIDNew.SelectedIndexChanged
        Dim strShape As String

        strShape = "Rounds"
        If Not cmbPktIDNew.SelectedItem Is Nothing Then
            If cmbPktIDNew.Text <> "" Then
                If IsNumeric(cmbPktIDNew.Text) = True Then
                    If Mid(txtParNo.Text, 2, 1) = "R" Then
                        strShape = "Rounds"
                    End If
                    If Mid(txtParNo.Text, 2, 1) = "B" Then
                        strShape = "Baguettes"
                    End If
                    If Mid(txtParNo.Text, 2, 1) = "E" Then
                        strShape = "Emerald"
                    End If

                    rsComSql = New ADODB.Recordset
                    Select Case strShape
                        Case "Rounds"
                            rsComSql.Open("SELECT * FROM tblRPrReturnDetails WHERE ParNo = '" & strParNo & "' AND PktNo = '" & strPktNo & "' AND Shape = 'Rounds' AND ID = " & CDbl(cmbPktIDNew.SelectedItem.Col1) & "", AdoCN, 1, 1)
                        Case "Baguettes"
                            rsComSql.Open("SELECT * FROM tblRPrReturnDetails WHERE ParNo = '" & strParNo & "' AND PktNo = '" & strPktNo & "' AND (Shape = 'Baguettes' OR Shape = 'PCU2') AND ID = " & CDbl(cmbPktIDNew.SelectedItem.Col1) & "", AdoCN, 1, 1)
                        Case "Emerald"
                            rsComSql.Open("SELECT * FROM tblRPrReturnDetails WHERE ParNo = '" & strParNo & "' AND PktNo = '" & strPktNo & "' AND (Shape = 'Emerald') AND ID = " & CDbl(cmbPktIDNew.SelectedItem.Col1) & "", AdoCN, 1, 1)
                    End Select

                    If rsComSql.RecordCount Then
                        cmbColor.Text = rsComSql.Fields("Color").Value
                        cmbClarity.Text = Trim(rsComSql.Fields("Clarity").Value)
                        txtPcs.Text = Trim(rsComSql.Fields("Pcs").Value)
                        txtCts.Text = Trim(rsComSql.Fields("RghCts").Value)
                        txtFinCts.Text = Trim(rsComSql.Fields("FinCts").Value)
                        txtValue.Text = Trim(rsComSql.Fields("Value").Value)
                        cmbSize2.Text = Trim(rsComSql.Fields("Length").Value)
                        txtWidth2.Text = Trim(rsComSql.Fields("Width").Value)
                        cmbCut.Text = Trim(rsComSql.Fields("Cut").Value)
                        txtStoneNo2.Text = Trim(rsComSql.Fields("StoneNo").Value)
                        'If Trim(rsComSql.Fields("Cut").Value) = "VG" Then
                        '    cmbCut.Text = "Very Good"
                        'Else
                        '    cmbCut.Text = Trim(rsComSql.Fields("Cut").Value)
                        'End If
                        cmbSize2.Focus()

                        If txtFinCts.Text <> "" And txtCts.Text <> "" Then
                            If CDbl(txtFinCts.Text) > 0 And CDbl(txtCts.Text) > 0 Then
                                txtEstYld.Text = Format(Math.Round((CDbl(txtFinCts.Text) / CDbl(txtCts.Text)) * 100, 2), "#0.00")
                            End If
                        End If

                    End If
                    rsComSql = Nothing
                End If
            End If
        End If
    End Sub

    Private Sub cmbSize3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSize3.SelectedIndexChanged
        If cmbSize3.Text <> "" Then
            If cmbShape2.Text = "PCU" Or cmbShape2.Text = "Other" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblRgfSize WHERE SizeDec = '" & cmbSize3.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If txtFinCts3.Text <> "" Then
                        If cmbShape2.Text = "Orders" Then
                            txtPlanValue.Text = Math.Round(rsComSql.Fields("Price2").Value * CDbl(txtFinCts3.Text), 0)
                        Else
                            txtPlanValue.Text = rsComSql.Fields("Price2").Value
                        End If

                        txtLen.Text = "0"
                        txtWid.Text = "0"
                    Else
                        txtPlanValue.Text = "0"
                    End If
                Else
                    txtPlanValue.Text = "0"
                End If
                rsComSql = Nothing
            Else
                txtPlanValue.Focus()
            End If
        End If
    End Sub

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Dim strChecked As String

        flxPacket.Rows.Clear()
        strChecked = ""
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblRPrPacket WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' ORDER BY PktNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                strChecked = ""
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT Checked FROM tblRPrPacketDetails WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "' AND Checked = 1", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strChecked = "YES"
                End If
                rsComSql_1 = Nothing

                flxPacket.Rows.Add(rsComSql.Fields("PktNo").Value,
                                   rsComSql.Fields("PktColor").Value,
                                   rsComSql.Fields("PktClarity").Value,
                                   rsComSql.Fields("PktCut").Value,
                                   rsComSql.Fields("PktPcs").Value,
                                   rsComSql.Fields("PktCts").Value,
                                   rsComSql.Fields("Value").Value,
                                   Format(rsComSql.Fields("PktIss").Value, "yyyy-MM-dd"),
                                   rsComSql.Fields("FinCts").Value,
                                   rsComSql.Fields("Sieve").Value,
                                   rsComSql.Fields("PktSize").Value,
                                   rsComSql.Fields("PktID").Value,
                                   rsComSql.Fields("Flo").Value,
                                   rsComSql.Fields("ParNo").Value,
                                   rsComSql.Fields("Model").Value,
                                   rsComSql.Fields("PktIDNew").Value,
                                   strChecked,
                                   rsComSql.Fields("StoneNo").Value)


                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub SaveAfterSawing()
        If cmbDept.Text <> "" And txtParNo.Text <> "" Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT ParNo FROM tblRPrPacket WHERE ParNo = '" & txtParNo.Text & "' AND Department = 'RoughSawing'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                AdoCN.Execute("UPDATE tblRPrPacket SET Trf = 1 WHERE ParNo = '" & txtParNo.Text & "' AND Department = 'RoughSawing'")

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT ParNo FROM tblRPrReturnDetails WHERE ParNo = '" & txtParNo.Text & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount = 0 Then
                    mStrSQL = "INSERT INTO tblRPrReturnDetails(Department,ParNo,PktNo,Shape,Pcs,RghCts,FinCts,Value,Color,Clarity,Cut,Length,Width,Size,StoneNo) " & _
                              "SELECT 'RoughPlanAS' AS Department, ParNo, PktNo, Shape, Pcs, RghCts, FinCts, Value, Color, Clarity, Cut, Size, Width, '0' AS Size1, 0 AS StoneNo " & _
                              "FROM dbo.tblRPrPacketDetails WHERE (ParNo = '" & txtParNo.Text & "')"
                    AdoCN.Execute(mStrSQL)

                    MsgBox("Rough Plan After Sawing Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                End If
                rsComSql_1 = Nothing
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub cmdSave2_Click(sender As Object, e As EventArgs) Handles cmdSave2.Click
        SaveAfterSawing()
    End Sub

    Private Sub cmdSaveDetails_Click(sender As Object, e As EventArgs) Handles cmdSaveDetails.Click
        If cmbDept.Text = "RoughPlan" Then
            If txtParNo.Text <> "" And txtPktNo.Text <> "" And txtPcs.Text <> "" Then
                Insert_PacketDetails()
            End If
            ClearFields()
        End If
    End Sub

    Private Sub txtWidth2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWidth2.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtWidth2.Text)
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "RgfPKTSLEEVE_BrutingBAG.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        objForm = New frm_DCLReportViewer
        mReportName = "RgfPKTSLEEVE_BrutingEME.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub
End Class