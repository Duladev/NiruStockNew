
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_PCUPacket

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ClearFields()
        txtPktNo.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        cmbRef.Text = ""
        cmbRef.Items.Clear()
        cmbFlow.Text = ""
        cmbSide.Text = ""
        cmbSide.Items.Clear()
        cmbAssort.Text = ""
        cmbParcel.Text = ""
        txtBalPcs.Text = "0"
        txtBalCts.Text = "0"
        txtFinCts.Text = "0"
        txtPktID.Text = "0"
        txtGroup.Text = ""
        txtCost.Text = "0"
        txtValue.Text = "0"
        txtGiaNo.Text = "0"
        txtIncUnit.Text = ""
        txtPlanVal.Text = ""
        cmbIncUnit.Text = ""
        cmbChg.Text = ""
        txtMaxCost.Text = "0"
        chkReIssue.Checked = False
        flxDetails.Rows.Clear()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
        txtOrderNo.Focus()
    End Sub

    Private Sub txtOrderNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrderNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtOrderNo.Text <> "" Then
                If Len(txtOrderNo.Text) = 5 Then
                    ClearFields()
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT OrderNo FROM tblNoneOrders WHERE OrderNo = '" & CInt(txtOrderNo.Text) & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then

                        cmbRef.Items.Clear()
                        cmbSide.Items.Clear()
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT RefNo FROM tblNoneOrdersDtls WHERE OrderNo = '" & CInt(txtOrderNo.Text) & "' GROUP BY RefNo ORDER BY RefNo", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            rsComSql_1.MoveFirst()
                            While Not rsComSql_1.EOF
                                cmbRef.Items.Add(rsComSql_1.Fields("RefNo").Value)

                                rsComSql_1.MoveNext()
                            End While
                            cmbRef.SelectedIndex = 0
                        End If
                        rsComSql_1 = Nothing

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT MAX(PktNo) AS MaxPktNo FROM tblPacket WHERE PktOrdNo = '" & CInt(txtOrderNo.Text) & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            If Not IsDBNull(rsComSql_1.Fields("MaxPktNo").Value) Then
                                txtPktNo.Text = Format(CDbl(rsComSql_1.Fields("MaxPktNo").Value) + 1, "000")
                            Else
                                txtPktNo.Text = "001"
                            End If
                        Else
                            txtPktNo.Text = "001"
                        End If
                        rsComSql_1 = Nothing

                        flxDetails.Rows.Clear()
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM tblPacket WHERE PktOrdNo = '" & CInt(txtOrderNo.Text) & "' ORDER BY PktNo", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            rsComSql_1.MoveFirst()
                            While Not rsComSql_1.EOF
                                flxDetails.Rows.Add(rsComSql_1.Fields("PktOrdNo").Value,
                                                    rsComSql_1.Fields("PktNo").Value,
                                                    rsComSql_1.Fields("PktRefNo").Value,
                                                    rsComSql_1.Fields("Pktside").Value,
                                                    UCase(rsComSql_1.Fields("Grp").Value),
                                                    rsComSql_1.Fields("AssortNo").Value,
                                                    rsComSql_1.Fields("AParNo").Value,
                                                    rsComSql_1.Fields("PktPcs").Value,
                                                    Format(rsComSql_1.Fields("PktCts").Value, "#0.000"),
                                                    Format(rsComSql_1.Fields("PktIss").Value, "yyyy/MM/dd"),
                                                    rsComSql_1.Fields("IncUnit").Value,
                                                    rsComSql_1.Fields("PlanVal").Value,
                                                    rsComSql_1.Fields("IncUnit2").Value,
                                                    rsComSql_1.Fields("CutChg").Value,
                                                    rsComSql_1.Fields("PktID").Value)

                                rsComSql_1.MoveNext()
                            End While
                        End If
                        rsComSql_1 = Nothing

                        cmbRef.Focus()
                    Else
                        MsgBox("Invalid Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        ClearFields()
                        txtOrderNo.Focus()
                    End If
                    rsComSql = Nothing
                Else
                    MsgBox("Invalid Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    ClearFields()
                    txtOrderNo.Focus()
                End If
            Else
                MsgBox("Invalid Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
                txtOrderNo.Focus()
            End If
        End If
    End Sub

    Private Sub cmbRef_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbRef.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbSide.Focus()
        End If
    End Sub

    Private Sub cmbRef_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRef.SelectedIndexChanged
        cmbSide.Items.Clear()
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT Side FROM tblNoneOrdersDtls WHERE OrderNo = '" & CInt(txtOrderNo.Text) & "' AND RefNo = '" & cmbRef.Text & "' GROUP BY Side ORDER BY Side", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                cmbSide.Items.Add(rsComSql_1.Fields("Side").Value)

                rsComSql_1.MoveNext()
            End While
            cmbSide.SelectedIndex = 0
        End If
        rsComSql_1 = Nothing

        rsComSql_1 = New ADODB.Recordset
        mStrSQL = "SELECT * FROM VW_NoneOrderRefNos WHERE OrderNo = '" & txtOrderNo.Text & "' AND RefNo = '" & cmbRef.Text & "'"
        rsComSql_1.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            cmbFlow.Items.Add(rsComSql_1.Fields("Flow").Value)
            cmbFlow.Text = rsComSql_1.Fields("Flow").Value
        End If
        rsComSql_1 = Nothing

        cmbSide.Focus()
    End Sub

    Private Sub Load_Assort()
        Dim rstAssort As ADODB.Recordset

        cmbAssort.Items.Clear()
        rstAssort = New ADODB.Recordset
        rstAssort.Open("SELECT DISTINCT Assortment FROM VW_PCUStockBal WHERE BalPcs > 0 ORDER BY Assortment", AdoCN, 1, 1)
        If rstAssort.RecordCount Then
            rstAssort.MoveFirst()
            While Not rstAssort.EOF
                cmbAssort.Items.Add(rstAssort.Fields("Assortment").Value)
                rstAssort.MoveNext()
            End While
        End If
        rstAssort = Nothing
    End Sub

    Private Sub Load_OrderBox()
        Dim rstAssort As ADODB.Recordset

        cmbAssort.Items.Clear()
        rstAssort = New ADODB.Recordset
        rstAssort.Open("SELECT Assortment FROM VW_POLStockBal WHERE Pcs > 0 AND CompCode = 'ORD' ORDER BY Assortment", dbConnDiaSales, 1, 1)
        If rstAssort.RecordCount Then
            rstAssort.MoveFirst()
            While Not rstAssort.EOF
                cmbAssort.Items.Add(rstAssort.Fields("Assortment").Value)
                rstAssort.MoveNext()
            End While
        End If
        rstAssort = Nothing
    End Sub

    Private Sub Load_InCat()
        cmbIncUnit.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblUnits ORDER BY Unit", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            Do While Not rsComSql.EOF
                cmbIncUnit.Items.Add(rsComSql.Fields("Unit").Value)
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

    Private Sub frm_PCUPacket_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        If dbConnDiaSales.State = 1 Then
            dbConnDiaSales.Close()
        End If
        dbConnDiaSales.ConnectionString = "Provider=SQLOLEDB;Data Source='" & strServerName & "';Connect Timeout=60;Initial Catalog=DiaSales;Integrated Security=SSPI"
        dbConnDiaSales.Open()

        Load_Assort()
        Load_InCat()
        Load_RateCode()
    End Sub

    Private Sub cmbAssort_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbAssort.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbParcel.Focus()
        End If
    End Sub

    Private Sub cmbAssort_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAssort.SelectedIndexChanged

        cmbParcel.Items.Clear()
        If cmbAssort.Text <> "" Then
            If chkOrdBox.Checked = True Then
                txtBalPcs.Text = "0"
                txtBalCts.Text = "0"
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT dbo.VW_POLStockBal.Assortment, dbo.VW_POLStockBal.Pcs, dbo.VW_POLStockBal.Cts, dbo.VW_POLStockBal.CompCode, dbo.VW_POLStockBal.SizeRange, dbo.tblDCLPermanents.AvgCost3 " & _
                              "FROM dbo.VW_POLStockBal INNER JOIN dbo.tblDCLPermanents ON dbo.VW_POLStockBal.Assortment = dbo.tblDCLPermanents.ItemName " & _
                              "WHERE (dbo.VW_POLStockBal.CompCode = 'ORD') AND (dbo.VW_POLStockBal.Assortment = '" & cmbAssort.Text & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                        txtBalPcs.Text = rsComSql.Fields("Pcs").Value
                        txtBalCts.Text = rsComSql.Fields("Cts").Value
                        txtCost.Text = rsComSql.Fields("AvgCost3").Value
                    End If
                End If
                rsComSql = Nothing

                cmbParcel.Text = "OrderBox"
            Else
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT ParNo FROM dbo.VW_PCUStockBalPar " & _
                              "WHERE (Assortment = '" & cmbAssort.Text & "') AND (BalPcs > 0) " & _
                              "GROUP BY ParNo", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        cmbParcel.Items.Add(rsComSql.Fields("ParNo").Value)

                        rsComSql.MoveNext()
                    End While
                End If
                rsComSql = Nothing

                txtBalPcs.Text = "0"
                txtBalCts.Text = "0"
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(BalPcs) AS BalPcs, ROUND(SUM(BalCts), 3) AS BalCts, MAX(Price) AS Price " & _
                              "FROM VW_PCUStockBal WHERE Assortment = '" & cmbAssort.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("BalPcs").Value) Then
                        txtBalPcs.Text = rsComSql.Fields("BalPcs").Value
                        txtBalCts.Text = rsComSql.Fields("BalCts").Value
                        txtCost.Text = rsComSql.Fields("Price").Value
                    End If
                End If
                rsComSql = Nothing

                cmbParcel.Focus()
            End If

        Else

        End If
    End Sub

    Private Sub cmbSide_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSide.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbAssort.Focus()
        End If
    End Sub

    Private Sub cmbParcel_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbParcel.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtPcs.Focus()
        End If
    End Sub

    Private Sub txtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtCts.Focus()
        End If
    End Sub

    Private Sub Get_IncUnit2()
        Dim dblSize As Double

        If txtPcs.Text <> "" And txtCts.Text <> "" And cmbFlow.Text <> "" Then
            If CDbl(txtCts.Text) = 0 Then Exit Sub

            dblSize = Math.Round(CDbl(txtPcs.Text) / CDbl(txtCts.Text), 2)
            dblSize = Math.Round(dblSize, 2)
            txtIncUnit.Text = ""

            If cmbFlow.Text = "RndSize" Then
                dblSize = Math.Round(dblSize * 0.42, 2)
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblRndIncentiveCat WHERE FromSize <= " & dblSize & " AND ToSize >= " & dblSize & " AND Category = 'MB'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    txtIncUnit.Text = rsComSql_1.Fields("Unit").Value
                End If
                rsComSql_1 = Nothing

            ElseIf cmbFlow.Text = "Emerald" Or cmbFlow.Text = "Radiant" Or cmbFlow.Text = "Cushion" Then
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblExtIncentiveCat WHERE Department = '" & cmbFlow.Text & "' AND FromSize <= " & dblSize & " AND ToSize >= " & dblSize & "", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    txtIncUnit.Text = Trim(rsComSql_1.Fields("Unit").Value)
                End If
                rsComSql_1 = Nothing

            ElseIf cmbFlow.Text = "Baguettes" Or cmbFlow.Text = "BagRough" Then
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblBAGIncentiveCat WHERE FromSize <= " & dblSize & " AND ToSize >= " & dblSize & "", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    txtIncUnit.Text = Trim(rsComSql_1.Fields("Unit").Value)
                End If
                rsComSql_1 = Nothing

            ElseIf cmbFlow.Text = "Princess" Then
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblPrIncentiveCat WHERE FromSize <= " & dblSize & " AND ToSize >= " & dblSize & "", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    txtIncUnit.Text = Trim(rsComSql_1.Fields("Unit").Value)
                End If
                rsComSql_1 = Nothing

            ElseIf cmbFlow.Text = "Precision" Then
                txtIncUnit.Text = Trim(cmbIncUnit.Text)

            Else

            End If
        End If
    End Sub

    Private Sub txtCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtCts.Text)
        If Asc(e.KeyChar) = 13 Then
            Get_IncUnit2()
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM VW_PCUStockBal WHERE Assortment = '" & cmbAssort.Text & "' AND BalPcs > 0", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If CDbl(txtCts.Text) > Math.Round(rsComSql.Fields("BalCts").Value, 3) Then
                    MsgBox("Cannot Issue this Assortment Cts exceeding", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    txtCts.Focus()
                    Exit Sub
                End If
            Else
                MsgBox("Cannot Issue this Assortment. No Cts In hand", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtCts.Focus()
                Exit Sub
            End If
            rsComSql = Nothing

            txtValue.Text = (CSng(txtCts.Text) * CSng(txtCost.Text)) / CInt(txtPcs.Text)
            txtPlanVal.Text = Math.Round(CSng(txtCts.Text) * CSng(txtCost.Text), 2)
            txtFinCts.Focus()
        End If
    End Sub

    Private Sub txtPktNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtOrderNo.Text <> "" And txtPktNo.Text <> "" Then
                If Len(txtOrderNo.Text) = 5 And Len(txtPktNo.Text) = 3 Then
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT * FROM tblPacket WHERE PktOrdNo = '" & txtOrderNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount > 0 Then
                        txtPcs.Text = rsComSql_2.Fields("PktPcs").Value
                        txtCts.Text = rsComSql_2.Fields("PktCts").Value
                        cmbRef.Text = rsComSql_2.Fields("PktRefNo").Value
                        cmbSide.Text = rsComSql_2.Fields("Pktside").Value
                        cmbFlow.Text = rsComSql_2.Fields("PktFlow").Value
                        cmbAssort.Text = rsComSql_2.Fields("AssortNo").Value
                        cmbParcel.Text = rsComSql_2.Fields("AParNo").Value
                        If IsDBNull((rsComSql_2.Fields("Grp").Value)) = False Then
                            txtGroup.Text = UCase(rsComSql_2.Fields("Grp").Value)
                        Else
                            txtGroup.Text = ""
                        End If

                        If rsComSql_2.Fields("ReIssue").Value = 1 Then
                            chkReIssue.Checked = True
                        Else
                            chkReIssue.Checked = False
                        End If

                        txtFinCts.Text = rsComSql_2.Fields("PlanCts").Value

                        txtPktID.Text = rsComSql_2.Fields("PktID").Value
                        txtGiaNo.Text = rsComSql_2.Fields("GiaNo").Value
                        txtIncUnit.Text = rsComSql_2.Fields("IncUnit").Value
                        txtPlanVal.Text = rsComSql_2.Fields("PlanVal").Value
                        cmbIncUnit.Text = rsComSql_2.Fields("IncUnit2").Value
                        cmbChg.Text = rsComSql_2.Fields("CutChg").Value
                    End If
                    rsComSql_2 = Nothing
                End If
            End If
        End If
    End Sub

    Private Sub Delete()
        If txtOrderNo.Text <> "" And txtPktNo.Text <> "" And txtPcs.Text <> "" And txtCts.Text <> "" Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblIssues WHERE ParNo = '" & txtOrderNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                AdoCN.Execute("DELETE FROM tblPacket WHERE PktOrdNo = '" & txtOrderNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'")

                ClearFields()
            Else
                MsgBox("Already Issued", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub Save()
        Dim rstPacket As ADODB.Recordset
        Dim intTotPcs As Integer
        Dim intIssPcs As Integer
        Dim intRejPcs As Integer
        Dim intExtPcs As Integer
        Dim intLostPcs As Integer

        If txtOrderNo.Text <> "" And txtPktNo.Text <> "" And txtPcs.Text <> "" And txtCts.Text <> "" Then

            If CDbl(txtPcs.Text) <= 0 Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If CDbl(txtCts.Text) <= 0 Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

            If Len(txtPktNo.Text) <> 3 Then MsgBox("Invalid Packet No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

            intTotPcs = 0
            intIssPcs = 0
            intRejPcs = 0
            intLostPcs = 0
            intExtPcs = 0

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT OrderNo FROM tblNoneOrders WHERE OrderNo = '" & CInt(txtOrderNo.Text) & "' AND Complete = 'N'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Order Number", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblNoneOrdersDtls WHERE OrderNo = '" & txtOrderNo.Text & "' AND RefNo = '" & cmbRef.Text & "' AND Side = '" & cmbSide.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                intTotPcs = rsComSql.Fields("Pcs").Value * rsComSql.Fields("Sets").Value
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(PktPcs) AS TotPcs FROM tblPacket WHERE PktOrdNo = '" & txtOrderNo.Text & "' AND PktRefNo = '" & cmbRef.Text & "' AND Pktside = '" & cmbSide.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("TotPcs").Value) Then
                    intIssPcs = rsComSql.Fields("TotPcs").Value
                End If
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM VW_PCURej WHERE OrderNo = '" & txtOrderNo.Text & "' AND RefNo = '" & cmbRef.Text & "' AND Side = '" & cmbSide.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("RejPcs").Value) Then
                    intRejPcs = CInt(rsComSql.Fields("RejPcs").Value)
                End If
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM VW_PCUExtra WHERE OrderNo = '" & txtOrderNo.Text & "' AND RefNo = '" & cmbRef.Text & "' AND Side = '" & cmbSide.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("ExtPcs").Value) Then
                    intExtPcs = CInt(rsComSql.Fields("ExtPcs").Value)
                End If
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(dbo.tblGrading_SizingReturns.RghPcs) AS RghPcs " & _
                          "FROM dbo.tblGrading_SizingReturns INNER JOIN dbo.tblGrading_SizingPacket ON dbo.tblGrading_SizingReturns.Department = dbo.tblGrading_SizingPacket.Department AND " & _
                            "dbo.tblGrading_SizingReturns.ParNo = dbo.tblGrading_SizingPacket.ParNo And dbo.tblGrading_SizingReturns.PktNo = dbo.tblGrading_SizingPacket.PktNo " & _
                          "WHERE (dbo.tblGrading_SizingReturns.Department LIKE 'GradingPCU%') AND (dbo.tblGrading_SizingPacket.OrderNo = '" & txtOrderNo.Text & "') AND (dbo.tblGrading_SizingPacket.RefNo = '" & cmbRef.Text & "') " & _
                            "AND (dbo.tblGrading_SizingPacket.Side = '" & cmbSide.Text & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("RghPcs").Value) Then
                    intRejPcs = intRejPcs + CInt(rsComSql.Fields("RghPcs").Value)
                End If
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(IssPcs) AS IssPcs FROM VW_GradingPCU_N_RghIssues " & _
                          "WHERE (OrderNo = '" & txtOrderNo.Text & "') AND (RefNo = '" & cmbRef.Text & "') " & _
                            "AND (Side = '" & cmbSide.Text & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("IssPcs").Value) Then
                    intRejPcs = intRejPcs + CInt(rsComSql.Fields("IssPcs").Value)
                End If
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM VW_PCULost WHERE OrderNo = '" & txtOrderNo.Text & "' AND RefNo = '" & cmbRef.Text & "' AND Side = '" & cmbSide.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("LostPcs").Value) Then
                    intLostPcs = CInt(rsComSql.Fields("LostPcs").Value)
                End If
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(dbo.tblFinalRepReturns.LostPcs) AS LostPcs " & _
                          "FROM dbo.tblGradingTrf INNER JOIN dbo.tblGrading_RepairParcels ON dbo.tblGradingTrf.Department = dbo.tblGrading_RepairParcels.Department AND dbo.tblGradingTrf.ParcelNo = dbo.tblGrading_RepairParcels.ParNo AND " & _
                                "dbo.tblGradingTrf.PktNo = dbo.tblGrading_RepairParcels.PktNo2 INNER JOIN dbo.tblFinalRepReturns ON dbo.tblGrading_RepairParcels.Department = dbo.tblFinalRepReturns.Department AND dbo.tblGrading_RepairParcels.ParNo = dbo.tblFinalRepReturns.ParNo AND " & _
                                "dbo.tblGrading_RepairParcels.PktNo = dbo.tblFinalRepReturns.PktNo " & _
                          "WHERE (dbo.tblGradingTrf.OrderNo = '" & txtOrderNo.Text & "') AND (dbo.tblGradingTrf.RefNo = '" & cmbRef.Text & "') AND (dbo.tblGradingTrf.Side = '" & cmbSide.Text & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("LostPcs").Value) Then
                    intLostPcs = intLostPcs + CInt(rsComSql.Fields("LostPcs").Value)
                End If
            End If
            rsComSql = Nothing

            intIssPcs = intIssPcs - (intRejPcs + intLostPcs) + intExtPcs

            If cmbFlow.Text = "" Then
                MsgBox("Invalid Flow", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If txtFinCts.Text = "" Then
                MsgBox("Invalid Plan Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If txtPktID.Text = "" Then
                MsgBox("Invalid Packet ID", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If txtGiaNo.Text = "" Then
                MsgBox("Invalid GIA Certification No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If txtPlanVal.Text = "" Then
                MsgBox("Invalid Plan Value", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If cmbIncUnit.Text = "" Then
                MsgBox("Invalid Incentive Unit", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If cmbChg.Text = "" Then
                MsgBox("Invalid Cutting Charge", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Val(txtPktID.Text) <> 0 Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT ID FROM tblRprPacket WHERE ID = " & Val(txtPktID.Text) & "", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    MsgBox("Invalid Packet ID", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                    txtPktID.Focus()
                    Exit Sub
                End If
                rsComSql = Nothing
            End If

            rstPacket = New ADODB.Recordset
            rstPacket.Open("SELECT * FROM tblPacket WHERE PktOrdNo = '" & txtOrderNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
            If rstPacket.RecordCount = 0 Then
                If intTotPcs < intIssPcs + CInt(txtPcs.Text) Then
                    MsgBox("Pcs exceeds the limit. Pls Check the Order PCs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_PCUStockBalPar WHERE Assortment = '" & cmbAssort.Text & "' AND ParNo = '" & cmbParcel.Text & "' AND BalPcs > 0", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If CDbl(txtPcs.Text) > rsComSql.Fields("BalPcs").Value Then
                        MsgBox("Cannot Issue this Assortment Pcs exceeding", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        txtPcs.Focus()
                        Exit Sub
                    End If
                    If CDbl(txtCts.Text) > Math.Round(rsComSql.Fields("BalCts").Value, 3) Then
                        MsgBox("Cannot Issue this Assortment Cts exceeding", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        txtPcs.Focus()
                        Exit Sub
                    End If
                Else
                    MsgBox("Cannot Issue this Assortment. No Pcs In hand", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    txtPcs.Focus()
                    Exit Sub
                End If
                rsComSql = Nothing

                dtpToday = GetToday()

                AdoCN.Execute("INSERT INTO tblPacket(ParNo,PktNo,PktPcs,PktCts,PktOrdNo,PktRefNo,Pktside,AssortNo,PktFlow,Grp," & _
                                "AParNo,PktIss,Remarks,PlanCts,PktID,GiaNo,FM,IncUnit,PlanVal,IncUnit2,CutChg,ReIssue) " & _
                              "VALUES('','" & txtPktNo.Text & "','" & CDbl(txtPcs.Text) & "','" & CDbl(txtCts.Text) & "','" & txtOrderNo.Text & "'," & _
                                "'" & cmbRef.Text & "','" & cmbSide.Text & "','" & cmbAssort.Text & "','" & cmbFlow.Text & "','" & UCase(txtGroup.Text) & "'," & _
                                "'" & cmbParcel.Text & "','" & Format(dtpToday, "MM/dd/yyyy") & "','','" & CDbl(txtFinCts.Text) & "','" & CDbl(txtPktID.Text) & "'," & _
                                "'" & txtGiaNo.Text & "',0,'" & txtIncUnit.Text & "','" & CDbl(txtPlanVal.Text) & "'," & _
                                "'" & cmbIncUnit.Text & "','" & cmbChg.Text & "'," & IIf(chkReIssue.Checked = True, 1, 0) & ")")

                If chkOrdBox.Checked = True Then
                    AdoCN.Execute("INSERT INTO tblPOLStockOut(Assortment,Assortment2,Pcs,Cts,Price,DocID,OrderNo,CompCode,SizeRange,Type) " & _
                                  "VALUES('" & UCase(cmbAssort.Text) & "','" & UCase(cmbAssort.Text) & "'," & CInt(txtPcs.Text) & "," & CDbl(txtCts.Text) & "," & _
                                    "" & CDbl(txtCost.Text) & ",'" & txtOrderNo.Text & "','" & txtOrderNo.Text & "','ORD','0','P')")
                End If
            Else
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_PCUStockBalPar WHERE Assortment = '" & cmbAssort.Text & "' AND ParNo = '" & cmbParcel.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                Else
                    MsgBox("Invalid Assortment or Parcel", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    cmbParcel.Focus()
                    Exit Sub
                End If
                rsComSql = Nothing

                AdoCN.Execute("UPDATE tblPacket SET PktFlow = '" & cmbFlow.Text & "',Grp = '" & UCase(txtGroup.Text) & "'," & _
                                "PlanCts = '" & CDbl(txtFinCts.Text) & "',PktID = '" & CDbl(txtPktID.Text) & "',FM = 0,PlanVal = '" & CDbl(txtPlanVal.Text) & "'," & _
                                "IncUnit2 = '" & cmbIncUnit.Text & "',CutChg = '" & cmbChg.Text & "',ReIssue = " & IIf(chkReIssue.Checked = True, 1, 0) & "," & _
                                "AssortNo = '" & cmbAssort.Text & "',AParNo = '" & cmbParcel.Text & "' " & _
                              "WHERE PktOrdNo = '" & txtOrderNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'")

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblIssues WHERE ParNo = '" & txtOrderNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    AdoCN.Execute("UPDATE tblPacket SET PktPcs = '" & CDbl(txtPcs.Text) & "',PktCts = '" & CDbl(txtCts.Text) & "',IncUnit = '" & txtIncUnit.Text & "' " & _
                                  "WHERE PktOrdNo = '" & txtOrderNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'")
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblPCUFinishOrders WHERE OrderNo = '" & txtOrderNo.Text & "' AND PacketNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    AdoCN.Execute("UPDATE tblPCUFinishOrders SET RateCode = '" & cmbChg.Text & "' " & _
                                  "WHERE OrderNo = '" & txtOrderNo.Text & "' AND PacketNo = '" & txtPktNo.Text & "'")
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblGradingTrf WHERE ParcelNo = '" & cmbParcel.Text & "' AND OrderNo = '" & txtOrderNo.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    AdoCN.Execute("UPDATE tblGradingTrf SET RateCode = '" & cmbChg.Text & "' " & _
                                  "WHERE ParcelNo = '" & cmbParcel.Text & "' AND OrderNo = '" & txtOrderNo.Text & "'")
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblGrading_SizingPacket WHERE ParNo = '" & cmbParcel.Text & "' AND OrderNo = '" & txtOrderNo.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    AdoCN.Execute("UPDATE tblGrading_SizingPacket SET RateCode = '" & cmbChg.Text & "' " & _
                                  "WHERE ParNo = '" & cmbParcel.Text & "' AND OrderNo = '" & txtOrderNo.Text & "'")
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblGrading_PackingListPCU WHERE ParNo = '" & cmbParcel.Text & "' AND OrderNo = '" & txtOrderNo.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    AdoCN.Execute("UPDATE tblGrading_PackingListPCU SET RateCode = '" & cmbChg.Text & "' " & _
                                  "WHERE ParNo = '" & cmbParcel.Text & "' AND OrderNo = '" & txtOrderNo.Text & "'")
                End If
                rsComSql = Nothing

            End If
            rstPacket = Nothing
            ClearFields()
            txtOrderNo.Focus()
        Else
            MsgBox("Please fill all the entries before save.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            txtOrderNo.Focus()
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        objForm = New frm_DCLReportViewer
        mReportName = "PKTSLEEVE_Full2.rpt"
        strReportPath = PBReportPath & "Precision\" & mReportName
        objForm.Show()
    End Sub

    Private Sub txtPktID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktID.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtPlanVal.Focus()
        End If
    End Sub

    Private Sub txtFinCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFinCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtPktID.Focus()
        End If
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        PBResponse = MsgBox("Are yo usure to Delete?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            Delete()
        End If
    End Sub

    Private Sub txtPlanVal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPlanVal.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtPlanVal.Text)
    End Sub

    Private Sub cmbSide_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSide.SelectedIndexChanged
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT IncenCat, CutChg, Flow, OutPrice FROM tblNoneOrdersDtls WHERE OrderNo = '" & CInt(txtOrderNo.Text) & "' AND RefNo = '" & cmbRef.Text & "' AND Side = '" & cmbSide.Text & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            cmbIncUnit.Text = rsComSql_1.Fields("IncenCat").Value
            cmbChg.Text = rsComSql_1.Fields("CutChg").Value
            cmbFlow.Text = rsComSql_1.Fields("Flow").Value
            txtMaxCost.Text = rsComSql_1.Fields("OutPrice").Value
        End If
        rsComSql_1 = Nothing
    End Sub

    Private Sub chkOrdBox_CheckedChanged(sender As Object) Handles chkOrdBox.CheckedChanged
        If chkOrdBox.Checked = True Then
            Load_OrderBox()
        Else
            Load_Assort()
        End If
    End Sub

    Private Sub cmbParcel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbParcel.SelectedIndexChanged
        If cmbParcel.Text <> "" Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT ItemCost " & _
                          "FROM dbo.tblDep_Trf " & _
                          "WHERE (DCLParcelNo = '" & cmbParcel.Text & "') AND (Department = 'Precision')", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                txtCost.Text = rsComSql.Fields("ItemCost").Value
            End If
            rsComSql = Nothing
        End If
        
    End Sub
End Class