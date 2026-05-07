
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixStoneReq

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxDetails.Rows.Clear()
        End If
    End Sub

    Private Sub Load_Details()
        Dim dblRecord As Double
        Dim dblOrdPcs As Double
        Dim dblIssPcs As Double
        Dim dblRejPcs As Double
        Dim dblBalPcs As Double

        Dim dblLengthFrom As Double
        Dim dblLengthTo As Double
        Dim dblWidthFrom As Double
        Dim dblWidthTo As Double

        Dim dblAvailPcs As Double
        Dim dblSelectPcs As Double
        Dim dblReqPcs As Double
        Dim dblOrgBalPcs As Double

        Dim strBagAssort As String

        If Len(txtMin.Text) = 0 Then
            txtMin.Text = "0.00"
        End If
        If Len(txtMax.Text) = 0 Then
            txtMax.Text = "0.20"
        End If

        If CDbl(txtMin.Text) >= CDbl(txtMax.Text) Then
            Exit Sub
        End If

        AdoCN.Execute("DELETE FROM tblTempOrderPcsNew")

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblOrdersDtls.Length, dbo.tblOrdersDtls.Width, SUM(dbo.tblOrdersDtls.PCs * dbo.tblOrdersDtls.Sets) AS Pcs " & _
                      "FROM dbo.tblOrders INNER JOIN dbo.tblOrdersDtls ON dbo.tblOrders.OrderNo = dbo.tblOrdersDtls.OrderNo " & _
                      "WHERE (dbo.tblOrders.Complete = 'N') AND (dbo.tblOrders.Niruref <> N'NIRU IL') " & _
                      "GROUP BY dbo.tblOrdersDtls.Length, dbo.tblOrdersDtls.Width " & _
                      "ORDER BY dbo.tblOrdersDtls.Length, dbo.tblOrdersDtls.Width", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()

            dblRecord = 0
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = rsComSql.RecordCount

            While Not rsComSql.EOF
                dblOrdPcs = 0
                dblIssPcs = 0
                dblRejPcs = 0
                dblBalPcs = 0

                dblLengthFrom = Math.Round(CDbl(rsComSql.Fields("Length").Value) + CDbl(txtMin.Text), 2)
                dblWidthFrom = Math.Round(CDbl(rsComSql.Fields("Width").Value) + CDbl(txtMin.Text), 2)

                dblLengthTo = Math.Round(CDbl(rsComSql.Fields("Length").Value) + CDbl(txtMax.Text), 2)
                dblWidthTo = Math.Round(CDbl(rsComSql.Fields("Width").Value) + CDbl(txtMax.Text), 2)

                dblOrdPcs = rsComSql.Fields("Pcs").Value

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT TOP (100) PERCENT SUM(ISNULL(dbo.tblMixPacket.PktPcs, 0)) AS PktPcs " & _
                                "FROM dbo.tblOrders INNER JOIN dbo.tblOrdersDtls ON dbo.tblOrders.OrderNo = dbo.tblOrdersDtls.OrderNo INNER JOIN " & _
                                    "dbo.tblMixPacket ON dbo.tblOrdersDtls.OrderNo = dbo.tblMixPacket.PktOrdNo AND dbo.tblOrdersDtls.RefNo = dbo.tblMixPacket.PktRefNo AND  " & _
                                    "dbo.tblOrdersDtls.Side = dbo.tblMixPacket.Pktside " & _
                                "WHERE (dbo.tblOrders.Complete = 'N') AND (dbo.tblOrdersDtls.Length = '" & rsComSql.Fields("Length").Value & "') AND (dbo.tblOrdersDtls.Width = '" & rsComSql.Fields("Width").Value & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("PktPcs").Value) Then
                        dblIssPcs = rsComSql_1.Fields("PktPcs").Value
                    End If
                End If
                rsComSql_1 = Nothing

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT TOP (100) PERCENT SUM(dbo.tblMixReturns.RejPcs + dbo.tblMixReturns.LostPcs) AS EffPcs " & _
                                "FROM dbo.tblOrders INNER JOIN dbo.tblOrdersDtls ON dbo.tblOrders.OrderNo = dbo.tblOrdersDtls.OrderNo INNER JOIN " & _
                                    "dbo.tblMixPacket ON dbo.tblOrdersDtls.OrderNo = dbo.tblMixPacket.PktOrdNo AND dbo.tblOrdersDtls.RefNo = dbo.tblMixPacket.PktRefNo AND  " & _
                                    "dbo.tblOrdersDtls.Side = dbo.tblMixPacket.Pktside INNER JOIN dbo.tblMixReturns ON dbo.tblMixPacket.PktNo = dbo.tblMixReturns.PktNo AND dbo.tblMixPacket.PktOrdNo = dbo.tblMixReturns.ParNo " & _
                                "WHERE (dbo.tblOrders.Complete = 'N') AND (dbo.tblOrdersDtls.Length = '" & rsComSql.Fields("Length").Value & "') AND (dbo.tblOrdersDtls.Width = '" & rsComSql.Fields("Width").Value & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("EffPcs").Value) Then
                        dblRejPcs = rsComSql_1.Fields("EffPcs").Value
                    End If
                End If
                rsComSql_1 = Nothing

                dblBalPcs = dblOrdPcs - (dblIssPcs - dblRejPcs)
                dblOrgBalPcs = dblBalPcs

                If dblBalPcs > 0 Then
                    dblAvailPcs = 0
                    dblSelectPcs = 0
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT TOP (100) PERCENT dbo.VW_MixAssortInNew.Assortment, dbo.VW_MixAssortInNew.InPcs - ISNULL(dbo.VW_MixAssortOut.Pcs, 0) " & _
                                            "- ISNULL(dbo.VW_TempOrderPcsNew.IssPcs, 0) AS Pcs, ISNULL(dbo.VW_ExpSizingFinishStockAPCUStock.Pcs, 0) AS SizePcs " & _
                                        "FROM dbo.VW_MixAssortInNew INNER JOIN dbo.tblAssortList ON dbo.VW_MixAssortInNew.Assortment = dbo.tblAssortList.Assortment LEFT OUTER JOIN " & _
                                            "dbo.VW_ExpSizingFinishStockAPCUStock ON dbo.tblAssortList.Assortment = dbo.VW_ExpSizingFinishStockAPCUStock.ReturnType LEFT OUTER JOIN " & _
                                            "dbo.VW_TempOrderPcsNew ON dbo.VW_MixAssortInNew.Assortment = dbo.VW_TempOrderPcsNew.Assortment LEFT OUTER JOIN " & _
                                            "dbo.VW_MixAssortOut ON dbo.VW_MixAssortInNew.Assortment = dbo.VW_MixAssortOut.Assortment " & _
                                        "WHERE (dbo.VW_MixAssortInNew.InPcs + ISNULL(dbo.VW_ExpSizingFinishStockAPCUStock.Pcs, 0) - ISNULL(dbo.VW_MixAssortOut.Pcs, 0) " & _
                                            "- ISNULL(dbo.VW_TempOrderPcsNew.IssPcs, 0) > 0) AND (dbo.tblAssortList.LengthFrom >= " & dblLengthFrom & ") AND " & _
                                            "(dbo.tblAssortList.LengthFrom <= " & dblLengthTo & ") AND (dbo.tblAssortList.WidthFrom >= " & dblWidthFrom & ") AND (dbo.tblAssortList.WidthFrom <= " & dblWidthTo & ") " & _
                                        "ORDER BY dbo.VW_MixAssortInNew.Assortment", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        rsComSql_2.MoveFirst()
                        While Not rsComSql_2.EOF
                            If rsComSql_2.Fields("Pcs").Value + rsComSql_2.Fields("SizePcs").Value > 0 Then
                                If dblBalPcs > rsComSql_2.Fields("Pcs").Value + rsComSql_2.Fields("SizePcs").Value Then
                                    dblBalPcs = dblBalPcs - (rsComSql_2.Fields("Pcs").Value + rsComSql_2.Fields("SizePcs").Value)
                                    dblAvailPcs = dblAvailPcs + (rsComSql_2.Fields("Pcs").Value)
                                    dblSelectPcs = dblSelectPcs + rsComSql_2.Fields("SizePcs").Value
                                    AdoCN.Execute("INSERT INTO tblTempOrderPcsNew(Assortment,Pcs) " & _
                                                  "VALUES('" & rsComSql_2.Fields("Assortment").Value & "'," & rsComSql_2.Fields("Pcs").Value + rsComSql_2.Fields("SizePcs").Value & ")")
                                Else
                                    If dblBalPcs <= rsComSql_2.Fields("Pcs").Value Then
                                        dblAvailPcs = dblAvailPcs + dblBalPcs
                                        AdoCN.Execute("INSERT INTO tblTempOrderPcsNew(Assortment,Pcs) " & _
                                                      "VALUES('" & rsComSql_2.Fields("Assortment").Value & "'," & dblBalPcs & ")")
                                        dblBalPcs = 0
                                    Else
                                        dblSelectPcs = dblSelectPcs + dblBalPcs
                                        AdoCN.Execute("INSERT INTO tblTempOrderPcsNew(Assortment,Pcs) " & _
                                                      "VALUES('" & rsComSql_2.Fields("Assortment").Value & "'," & dblBalPcs & ")")
                                        dblBalPcs = 0
                                    End If

                                    GoTo InsertGrid
                                End If
                            End If

                            rsComSql_2.MoveNext()
                        End While
                    End If
                    rsComSql_2 = Nothing
InsertGrid:
                    dblReqPcs = 0
                    If dblOrgBalPcs > dblAvailPcs + dblSelectPcs Then
                        dblReqPcs = dblOrgBalPcs - (dblAvailPcs + dblSelectPcs)
                    End If

                    strBagAssort = ""
                    If dblReqPcs > 0 Then
                        'rsComSql_2 = New ADODB.Recordset
                        'rsComSql_2.Open("SELECT ItemName FROM dbo.tblDCLPermanents " & _
                        '                "WHERE (LengthFrom >= " & dblLengthFrom & ") AND (LengthFrom <= " & dblLengthTo & ") AND (WidthFrom >= " & dblWidthFrom & ") AND (WidthFrom <= " & dblWidthTo & ") OR  " & _
                        '                    "(LengthFrom >= " & dblLengthFrom & ") AND (LengthFrom <= " & dblLengthTo & ") AND (WidthTo >= " & dblWidthFrom & ") AND (WidthTo <= " & dblWidthTo & ") OR " & _
                        '                    "(LengthTo >= " & dblLengthFrom & ") AND (LengthTo <= " & dblLengthTo & ") AND (WidthFrom >= " & dblWidthFrom & ") AND (WidthFrom <= " & dblWidthTo & ") OR " & _
                        '                    "(LengthTo >= " & dblLengthFrom & ") AND (LengthTo <= " & dblLengthTo & ") AND (WidthTo >= " & dblWidthFrom & ") AND (WidthTo <= " & dblWidthTo & ") " & _
                        '                "GROUP BY ItemName " & _
                        '                "HAVING (ItemName LIKE N'BA%' OR ItemName LIKE N'C%') ORDER BY ItemName", AdoCN, 1, 1)
                        'If rsComSql_2.RecordCount Then
                        '    rsComSql_2.MoveFirst()
                        '    strBagAssort = rsComSql_2.Fields("ItemName").Value
                        'End If
                        'rsComSql_2 = Nothing

                        flxDetails.Rows.Add(rsComSql.Fields("Length").Value,
                                            rsComSql.Fields("Width").Value,
                                            dblOrdPcs,
                                            dblIssPcs,
                                            dblRejPcs,
                                            dblOrgBalPcs,
                                            dblAvailPcs,
                                            dblSelectPcs,
                                            dblReqPcs,
                                            strBagAssort)
                        Application.DoEvents()
                    End If
                End If

                rsComSql.MoveNext()
                dblRecord = dblRecord + 1
                ExpProgress.Value = dblRecord
            End While
        End If
        rsComSql = Nothing

        ExpProgress.Visible = False
    End Sub

    Private Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            Load_Details()
        End If
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub txtMin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMin.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtMin.Text)
    End Sub

    Private Sub txtMax_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMax.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtMax.Text)
    End Sub

    Private Sub frm_MixStoneReq_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub
End Class