
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_PCUFinishOrders
    Dim intCounter As Long

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_PolishedReturns()
        'On Error GoTo Err_Trap

        Dim vIssueCts, vPktCts As Double
        Dim rstAssortment As New ADODB.Recordset
        Dim rstImp As New ADODB.Recordset
        Dim vPktPcs As Double
        Dim vAvgPrice As Double
        Dim varParcelNo As String
        Dim mRej, vIssCts As Single
        Dim mRejRgh As Single
        Dim dblRejPcs As Single
        Dim dblRejCts As Single
        Dim strSubject As String
        Dim strCategory As String
        Dim strCompany As String
        Dim strSupCode As String
        Dim strSupplier As String
        Dim strInvNo As String
        Dim strOrgParNo As String
        Dim vCharges As String

        flxDetails.Visible = True
        flxDetails.Rows.Clear()

        If Len(txtOrder.Text) = 0 Then
            mStrSQL = "SELECT TOP (100) PERCENT dbo.tblReturns.ParNo, dbo.tblPacket.PktRefNo, dbo.tblPacket.Pktside, dbo.tblPacket.AParNo, dbo.tblPacket.AssortNo, " & _
                            "ISNULL(dbo.tblItemMasterFile.ItemCost, 0) AS Price, dbo.tblReturns.PktNo, SUM(dbo.tblReturns.RetPcsT + dbo.tblReturns.RetPcsB) AS RetPcs, " & _
                            "SUM(dbo.tblReturns.RetCts) AS RetCts, dbo.tblPacket.PktPcs, dbo.tblPacket.PktCts, dbo.tblReturns.Status, SUM(dbo.tblReturns.RejPcs) AS RejPcs, " & _
                            "SUM(dbo.tblReturns.RejCts) AS RejCts, SUM(dbo.tblReturns.LostPcs) AS LostPcs, SUM(dbo.tblReturns.LostCts) AS LostCts, SUM(dbo.tblReturns.BroPcs) AS BroPcs, " & _
                            "SUM(dbo.tblReturns.ExtPcs) AS ExtPcs, dbo.tblPacket.Grp, dbo.tblNoneOrders.Subject, dbo.tblPacket.CutChg, dbo.tblNoneOrdersDtls.NLineNo " & _
                        "FROM dbo.tblPacket INNER JOIN dbo.tblReturns ON dbo.tblPacket.PktOrdNo = dbo.tblReturns.ParNo AND dbo.tblPacket.PktNo = dbo.tblReturns.PktNo INNER JOIN " & _
                            "dbo.tblNoneOrders ON dbo.tblPacket.PktOrdNo = dbo.tblNoneOrders.OrderNo INNER JOIN " & _
                            "dbo.tblNoneOrdersDtls ON dbo.tblNoneOrders.OrderNo = dbo.tblNoneOrdersDtls.OrderNo AND dbo.tblPacket.PktRefNo = dbo.tblNoneOrdersDtls.RefNo AND " & _
                            "dbo.tblPacket.Pktside = dbo.tblNoneOrdersDtls.Side LEFT OUTER JOIN " & _
                            "dbo.tblItemMasterFile ON dbo.tblPacket.AssortNo = dbo.tblItemMasterFile.AssortmentName " & _
                        "WHERE (dbo.tblReturns.Sec = 18) And (dbo.tblReturns.Status = 0) " & _
                        "GROUP BY dbo.tblReturns.ParNo, dbo.tblReturns.PktNo, dbo.tblPacket.PktRefNo, dbo.tblPacket.AParNo, dbo.tblPacket.PktCts, dbo.tblPacket.PktPcs, dbo.tblPacket.AssortNo, " & _
                            "dbo.tblReturns.Status, dbo.tblItemMasterFile.ItemCost, dbo.tblPacket.Pktside, dbo.tblPacket.Grp, dbo.tblNoneOrders.Subject, dbo.tblPacket.CutChg, dbo.tblNoneOrdersDtls.NLineNo " & _
                        "HAVING (SUM(dbo.tblReturns.RetCts) > 0) And (SUM(dbo.tblReturns.RetPcsT + dbo.tblReturns.RetPcsB) > 0) " & _
                        "ORDER BY dbo.tblReturns.ParNo, dbo.tblPacket.PktRefNo, dbo.tblReturns.PktNo"
        Else
            mStrSQL = "SELECT TOP (100) PERCENT dbo.tblReturns.ParNo, dbo.tblPacket.PktRefNo, dbo.tblPacket.Pktside, dbo.tblPacket.AParNo, dbo.tblPacket.AssortNo, " & _
                            "ISNULL(dbo.tblItemMasterFile.ItemCost, 0) AS Price, dbo.tblReturns.PktNo, SUM(dbo.tblReturns.RetPcsT + dbo.tblReturns.RetPcsB) AS RetPcs, " & _
                            "SUM(dbo.tblReturns.RetCts) AS RetCts, dbo.tblPacket.PktPcs, dbo.tblPacket.PktCts, dbo.tblReturns.Status, SUM(dbo.tblReturns.RejPcs) AS RejPcs, " & _
                            "SUM(dbo.tblReturns.RejCts) AS RejCts, SUM(dbo.tblReturns.LostPcs) AS LostPcs, SUM(dbo.tblReturns.LostCts) AS LostCts, SUM(dbo.tblReturns.BroPcs) AS BroPcs, " & _
                            "SUM(dbo.tblReturns.ExtPcs) AS ExtPcs, dbo.tblPacket.Grp, dbo.tblNoneOrders.Subject, dbo.tblPacket.CutChg, dbo.tblNoneOrdersDtls.NLineNo " & _
                        "FROM dbo.tblPacket INNER JOIN dbo.tblReturns ON dbo.tblPacket.PktOrdNo = dbo.tblReturns.ParNo AND dbo.tblPacket.PktNo = dbo.tblReturns.PktNo INNER JOIN " & _
                            "dbo.tblNoneOrders ON dbo.tblPacket.PktOrdNo = dbo.tblNoneOrders.OrderNo INNER JOIN " & _
                            "dbo.tblNoneOrdersDtls ON dbo.tblNoneOrders.OrderNo = dbo.tblNoneOrdersDtls.OrderNo AND dbo.tblPacket.PktRefNo = dbo.tblNoneOrdersDtls.RefNo AND " & _
                            "dbo.tblPacket.Pktside = dbo.tblNoneOrdersDtls.Side LEFT OUTER JOIN " & _
                            "dbo.tblItemMasterFile ON dbo.tblPacket.AssortNo = dbo.tblItemMasterFile.AssortmentName " & _
                        "WHERE (dbo.tblReturns.Sec = 18) And (dbo.tblReturns.Status = 0) AND (dbo.tblReturns.ParNo = '" & txtOrder.Text & "') " & _
                        "GROUP BY dbo.tblReturns.ParNo, dbo.tblReturns.PktNo, dbo.tblPacket.PktRefNo, dbo.tblPacket.AParNo, dbo.tblPacket.PktCts, dbo.tblPacket.PktPcs, dbo.tblPacket.AssortNo, " & _
                            "dbo.tblReturns.Status, dbo.tblItemMasterFile.ItemCost, dbo.tblPacket.Pktside, dbo.tblPacket.Grp, dbo.tblNoneOrders.Subject, dbo.tblPacket.CutChg, dbo.tblNoneOrdersDtls.NLineNo " & _
                        "HAVING (SUM(dbo.tblReturns.RetCts) > 0) And (SUM(dbo.tblReturns.RetPcsT + dbo.tblReturns.RetPcsB) > 0) " & _
                        "ORDER BY dbo.tblReturns.ParNo, dbo.tblPacket.PktRefNo, dbo.tblReturns.PktNo"
        End If
        rsComSql = New ADODB.Recordset
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)

        ExpProgress.Minimum = 0
        ExpProgress.Visible = True
        ExpProgress.Maximum = rsComSql.RecordCount
        intCounter = 0

        Do While Not rsComSql.EOF
            intCounter = intCounter + 1
            vCharges = rsComSql.Fields("CutChg").Value & ""

            strSubject = Trim(Replace(rsComSql.Fields("Subject").Value, "'", ""))

            'If rsComSql.Fields("ParNo").Value = "70103" Then
            '    MsgBox(rsComSql.Fields("ParNo").Value)
            'End If

            'If rsComSql.Fields("ParNo").Value = "60424" And rsComSql.Fields("PktRefNo").Value = "2" Then
            '    MsgBox(rsComSql.Fields("ParNo").Value)
            'End If

            strCategory = ""
            strCompany = ""
            strSupCode = ""
            strSupplier = ""
            strInvNo = ""
            strOrgParNo = ""

            rstAssortment = New ADODB.Recordset
            mStrSQL = "SELECT tblImport.ParcelType,tblImport.ItemCost, tblItemMasterFile.ItemCost AS ItmM_ItemCost, tblImport.Category, tblImport.CompCode, tblImport.SupplierRefNo, tblImport.SupplierCode " & _
                      "FROM tblImport INNER JOIN dbo.tblItemMasterFile ON dbo.tblImport.AssortmentNo = dbo.tblItemMasterFile.AssortmentName " & _
                      "WHERE (tblImport.AssortmentNo = '" & rsComSql.Fields("AssortNo").Value & "') " & _
                      "ORDER BY tblImport.AssortmentNo"
            rstAssortment.Open(mStrSQL, AdoCN, 1, 1)

            If Not rstAssortment.EOF Then
                If UCase(rstAssortment.Fields("ParcelType").Value) = "ROUGH" Then
                    rstImp = New ADODB.Recordset
                    mStrSQL = "SELECT ItemCost, Category, CompCode, SupplierRefNo, SupplierCode FROM tblImport WHERE (DCLParcelNo = '" & rsComSql("AParNo").Value & "')"
                    rstImp.Open(mStrSQL, AdoCN, 1, 1)
                    If Not rstImp.EOF Then
                        vAvgPrice = rstImp.Fields("ItemCost").Value
                        strCategory = rstImp.Fields("Category").Value
                        strCompany = rstImp.Fields("CompCode").Value
                        strInvNo = rstImp.Fields("SupplierRefNo").Value
                        strSupCode = rstImp.Fields("SupplierCode").Value
                    Else
                        rstImp = New ADODB.Recordset
                        mStrSQL = "SELECT SupParcelNo FROM tblDep_Trf WHERE (DCLParcelNo = '" & rsComSql("AParNo").Value & "')"
                        rstImp.Open(mStrSQL, AdoCN, 1, 1)
                        If Not rstImp.EOF Then
                            strOrgParNo = rstImp.Fields("SupParcelNo").Value

                            rstImp = New ADODB.Recordset
                            mStrSQL = "SELECT ItemCost, Category, CompCode, SupplierRefNo, SupplierCode FROM tblImport WHERE (SupParcelNo = '" & strOrgParNo & "')"
                            rstImp.Open(mStrSQL, AdoCN, 1, 1)
                            If Not rstImp.EOF Then
                                vAvgPrice = rstImp.Fields("ItemCost").Value
                                strCategory = rstImp.Fields("Category").Value
                                strCompany = rstImp.Fields("CompCode").Value
                                strInvNo = rstImp.Fields("SupplierRefNo").Value
                                strSupCode = rstImp.Fields("SupplierCode").Value
                            End If
                        End If
                        rstImp = Nothing
                        vAvgPrice = rstAssortment.Fields("ItmM_ItemCost").Value
                    End If
                    rstImp = Nothing
                Else
                    If UCase(rstAssortment.Fields("ParcelType").Value) = "POLISHED" Then
                        rstImp = New ADODB.Recordset
                        mStrSQL = "SELECT ItemCost, Category, CompCode, SupplierRefNo, SupplierCode FROM tblImport WHERE (DCLParcelNo = '" & rsComSql("AParNo").Value & "')"
                        rstImp.Open(mStrSQL, AdoCN, 1, 1)
                        If Not rstImp.EOF Then
                            vAvgPrice = rstImp.Fields("ItemCost").Value
                            strCategory = rstImp.Fields("Category").Value
                            strCompany = rstImp.Fields("CompCode").Value
                            strInvNo = rstImp.Fields("SupplierRefNo").Value
                            strSupCode = rstImp.Fields("SupplierCode").Value
                        Else
                            rstImp = New ADODB.Recordset
                            mStrSQL = "SELECT SupParcelNo FROM tblDep_Trf WHERE (DCLParcelNo = '" & rsComSql("AParNo").Value & "')"
                            rstImp.Open(mStrSQL, AdoCN, 1, 1)
                            If Not rstImp.EOF Then
                                strOrgParNo = rstImp.Fields("SupParcelNo").Value

                                rstImp = New ADODB.Recordset
                                mStrSQL = "SELECT ItemCost, Category, CompCode, SupplierRefNo, SupplierCode FROM tblImport WHERE (SupParcelNo = '" & strOrgParNo & "')"
                                rstImp.Open(mStrSQL, AdoCN, 1, 1)
                                If Not rstImp.EOF Then
                                    vAvgPrice = rstImp.Fields("ItemCost").Value
                                    strCategory = rstImp.Fields("Category").Value
                                    strCompany = rstImp.Fields("CompCode").Value
                                    strInvNo = rstImp.Fields("SupplierRefNo").Value
                                    strSupCode = rstImp.Fields("SupplierCode").Value
                                End If
                            End If
                            rstImp = Nothing
                        End If
                        rstImp = Nothing
                    Else
                        vAvgPrice = rstAssortment.Fields("ItmM_ItemCost").Value
                        strCategory = rstAssortment.Fields("Category").Value
                        strCompany = rstAssortment.Fields("CompCode").Value
                        strInvNo = rstAssortment.Fields("SupplierRefNo").Value
                        strSupCode = rstAssortment.Fields("SupplierCode").Value
                    End If
                End If
            Else
                rstImp = New ADODB.Recordset
                mStrSQL = "SELECT MarketPrice FROM tblAssortList WHERE (Assortment = '" & rsComSql.Fields("AssortNo").Value & "')"
                rstImp.Open(mStrSQL, AdoCN, 1, 1)
                If Not rstImp.EOF Then
                    vAvgPrice = rstImp.Fields("MarketPrice").Value
                End If
                rstImp = Nothing
            End If
            rstAssortment = Nothing

            If vAvgPrice = 0 Then
                rstImp = New ADODB.Recordset
                mStrSQL = "SELECT PRICE FROM tblGrading_SizingList WHERE (NAME = '" & rsComSql.Fields("AssortNo").Value & "')"
                rstImp.Open(mStrSQL, AdoCN, 1, 1)
                If Not rstImp.EOF Then
                    vAvgPrice = rstImp.Fields("PRICE").Value
                End If
                rstImp = Nothing
            End If

            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblSuppliers WHERE SupplierCode = '" & strSupCode & "'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                strSupplier = rsComSql_1.Fields("CompanyName").Value
            End If
            rsComSql_1 = Nothing

            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblPOLStockOut WHERE DocID = '" & rsComSql.Fields("AParNo").Value & "'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                If strDBName = "DiaSales" Then
                    strCategory = "Purchased"
                Else
                    strCategory = "NFE"
                End If
                strCompany = "DCL"
                strSupplier = "Polish Box"
                strInvNo = ""
                rstImp = New ADODB.Recordset
                mStrSQL = "SELECT AvgCost FROM tblDCLPermanents WHERE (ItemName = '" & rsComSql.Fields("Assortment").Value & "')"
                rstImp.Open(mStrSQL, AdoCN, 1, 1)
                If Not rstImp.EOF Then
                    vAvgPrice = rstImp.Fields("AvgCost").Value
                End If
                rstImp = Nothing
            End If
            rsComSql_1 = Nothing

            dblRejPcs = 0
            dblRejCts = 0
            mRej = 0
            mRejRgh = 0
            vIssCts = 0
            vIssueCts = 0
            rsComSql_1 = New ADODB.Recordset
            mStrSQL = "SELECT Sum(RejPcs) As RejPcs, Sum(RejCts) As RejCts,  Sum(RghCts) As RghCts " & _
                      "FROM tblReturns " & _
                      "WHERE ParNo = '" & rsComSql.Fields("ParNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "' AND RejPcs > 0"
            rsComSql_1.Open(mStrSQL, AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                If Not IsDBNull(rsComSql_1.Fields("RejPCs").Value) Then
                    vPktCts = Format(rsComSql.Fields("PktCts").Value, "#0.000")
                    vIssCts = vPktCts

                    mRej = Format(rsComSql_1.Fields("RejCts").Value, "#0.000")
                    mRejRgh = Format(rsComSql_1.Fields("RghCts").Value, "#0.000")

                    vIssCts = vIssCts - mRej
                    dblRejPcs = rsComSql_1.Fields("RejPCs").Value
                    dblRejCts = mRej

                    vPktPcs = Val(rsComSql.Fields("PktPcs").Value - dblRejPcs)
                    vIssueCts = Format(vIssCts, "#0.000")
                    vIssueCts = (vIssueCts / vPktPcs) * rsComSql.Fields("RetPcs").Value
                    vIssueCts = Math.Round(vIssueCts, 3)
                Else
                    vIssueCts = Format(rsComSql.Fields("PktCts").Value, "#0.000")
                End If
            Else
                vIssueCts = Format(rsComSql.Fields("PktCts").Value, "#0.000")
            End If
            rsComSql_1 = Nothing

            If vAvgPrice = 0 Then
                vAvgPrice = rsComSql.Fields("Price").Value
            End If
            varParcelNo = IIf(IsDBNull((rsComSql.Fields("AParNo").Value)), "-", (rsComSql.Fields("AParNo").Value))

            flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value, strSubject, rsComSql.Fields("PktRefNo").Value, rsComSql.Fields("Pktside").Value,
                              varParcelNo, rsComSql.Fields("AssortNo").Value, vAvgPrice, rsComSql.Fields("PktNo").Value, rsComSql.Fields("RetPcs").Value,
                              Format(rsComSql.Fields("RetCts").Value, "#0.000"), rsComSql.Fields("PktPcs").Value, rsComSql.Fields("PktCts").Value, vIssueCts,
                              vCharges, False, 0, dblRejPcs, Format(dblRejCts, "#0.000"), rsComSql.Fields("LostPcs").Value, Format(rsComSql.Fields("LostCts").Value, "#0.000"),
                              rsComSql.Fields("BroPcs").Value, rsComSql.Fields("ExtPcs").Value, rsComSql.Fields("Grp").Value, rsComSql.Fields("NLineNo").Value, strCategory,
                              strCompany, strSupplier, strInvNo)

            rsComSql.MoveNext()
            ExpProgress.Value = intCounter
        Loop
        rsComSql = Nothing

        ExpProgress.Visible = False
        Exit Sub
Err_Trap:

    End Sub

    Private Sub Load_RejectPackets()
        Dim vRecordNo As Double
        Dim dblPrice As Double
        Dim rstImp As New ADODB.Recordset
        Dim strCategory As String
        Dim strCompany As String
        Dim strSupCode As String
        Dim strSupplier As String
        Dim strInvNo As String
        Dim strOrgParNo As String

        flxDetails.Visible = True
        flxDetails.Rows.Clear()

        rsComSql = New ADODB.Recordset
        mStrSQL = "SELECT MAX(RecordNo) AS RecordNo FROM tblPCUFinishOrders"
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If Not IsDBNull(rsComSql.Fields("RecordNo").Value) Then
            vRecordNo = rsComSql.Fields("RecordNo").Value + 1
        Else
            vRecordNo = 1
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblPacket.PktOrdNo, dbo.tblPacket.PktNo, dbo.tblPacket.AssortNo, dbo.tblPacket.PktPcs, dbo.tblPacket.PktCts, SUM(dbo.tblReturns.RejPcs) AS RejPcs, " & _
                            "ROUND(SUM(dbo.tblReturns.RejCts), 3) AS RejCts, ROUND(dbo.tblPacket.PktCts - SUM(dbo.tblReturns.RejCts), 3) AS DifCts, dbo.tblNoneOrders.Subject, " & _
                            "dbo.tblPacket.PktRefNo , dbo.tblPacket.Pktside, dbo.tblPacket.AParNo, dbo.tblPacket.Grp, dbo.tblNoneOrdersDtls.CutChg, dbo.tblNoneOrdersDtls.NLineNo " & _
                      "FROM dbo.tblPacket INNER JOIN dbo.tblReturns ON dbo.tblPacket.PktOrdNo = dbo.tblReturns.ParNo AND dbo.tblPacket.PktNo = dbo.tblReturns.PktNo INNER JOIN " & _
                            "dbo.tblNoneOrders ON dbo.tblPacket.PktOrdNo = dbo.tblNoneOrders.OrderNo INNER JOIN " & _
                            "dbo.tblNoneOrdersDtls ON dbo.tblPacket.PktOrdNo = dbo.tblNoneOrdersDtls.OrderNo AND dbo.tblPacket.PktRefNo = dbo.tblNoneOrdersDtls.RefNo AND " & _
                            "dbo.tblPacket.Pktside = dbo.tblNoneOrdersDtls.Side " & _
                      "WHERE (dbo.tblReturns.Status = 0) AND (dbo.tblNoneOrders.Complete = N'N') " & _
                      "GROUP BY dbo.tblPacket.PktOrdNo, dbo.tblPacket.PktPcs, dbo.tblPacket.AssortNo, dbo.tblPacket.PktCts, dbo.tblPacket.PktNo, dbo.tblNoneOrders.Subject, " & _
                            "dbo.tblPacket.PktRefNo , dbo.tblPacket.Pktside, dbo.tblPacket.AParNo, dbo.tblPacket.Grp, dbo.tblNoneOrdersDtls.CutChg, dbo.tblNoneOrdersDtls.NLineNo " & _
                      "HAVING (dbo.tblPacket.PktPcs = Sum(dbo.tblReturns.RejPcs)) And (Round(dbo.tblPacket.Pktcts - Sum(dbo.tblReturns.RejCts), 3) > 0) " & _
                      "ORDER BY dbo.tblPacket.PktOrdNo, dbo.tblPacket.PktNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                strCategory = ""
                strCompany = ""
                strSupplier = ""
                strInvNo = ""
                strSupCode = ""

                rstImp = New ADODB.Recordset
                mStrSQL = "SELECT ItemCost, Category, CompCode, SupplierRefNo, SupplierCode FROM tblImport WHERE (DCLParcelNo = '" & rsComSql("AParNo").Value & "')"
                rstImp.Open(mStrSQL, AdoCN, 1, 1)
                If Not rstImp.EOF Then
                    dblPrice = rstImp.Fields("ItemCost").Value
                    strCategory = rstImp.Fields("Category").Value
                    strCompany = rstImp.Fields("CompCode").Value
                    strInvNo = rstImp.Fields("SupplierRefNo").Value
                    strSupCode = rstImp.Fields("SupplierCode").Value
                Else
                    rstImp = New ADODB.Recordset
                    mStrSQL = "SELECT SupParcelNo FROM tblDep_Trf WHERE (DCLParcelNo = '" & rsComSql("AParNo").Value & "')"
                    rstImp.Open(mStrSQL, AdoCN, 1, 1)
                    If Not rstImp.EOF Then
                        strOrgParNo = rstImp.Fields("SupParcelNo").Value

                        rstImp = New ADODB.Recordset
                        mStrSQL = "SELECT ItemCost, Category, CompCode, SupplierRefNo, SupplierCode FROM tblImport WHERE (SupParcelNo = '" & strOrgParNo & "')"
                        rstImp.Open(mStrSQL, AdoCN, 1, 1)
                        If Not rstImp.EOF Then
                            dblPrice = rstImp.Fields("ItemCost").Value
                            strCategory = rstImp.Fields("Category").Value
                            strCompany = rstImp.Fields("CompCode").Value
                            strInvNo = rstImp.Fields("SupplierRefNo").Value
                            strSupCode = rstImp.Fields("SupplierCode").Value
                        End If
                    End If
                    rstImp = Nothing
                End If
                rstImp = Nothing

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblSuppliers WHERE SupplierCode = '" & strSupCode & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strSupplier = rsComSql_1.Fields("CompanyName").Value
                End If
                rsComSql_1 = Nothing

                flxDetails.Rows.Add(rsComSql.Fields("PktOrdNo").Value, rsComSql.Fields("Subject").Value, rsComSql.Fields("PktRefNo").Value, rsComSql.Fields("Pktside").Value,
                                  rsComSql.Fields("AParNo").Value, rsComSql.Fields("AssortNo").Value, dblPrice, rsComSql.Fields("PktNo").Value, "0",
                                  "0.000", rsComSql.Fields("PktPcs").Value, rsComSql.Fields("PktCts").Value, rsComSql.Fields("DifCts").Value,
                                  "0", False, 0, rsComSql.Fields("RejPcs").Value, Format(rsComSql.Fields("RejCts").Value, "#0.000"), "0", "0.000",
                                  "0", "0", rsComSql.Fields("Grp").Value, rsComSql.Fields("NLineNo").Value, strCategory,
                                  strCompany, strSupplier, strInvNo)

                vRecordNo = vRecordNo + 1
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        If optNew.Checked = True Then
            If chkRejects.Checked = True Then
                Load_RejectPackets()
            Else
                Load_PolishedReturns()
            End If
        Else
            Load_SavedData()
        End If
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(14, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(14, intRow).Value = False
            Next
        End If
    End Sub

    Private Sub SaveData()
        'On Error GoTo Err_Trap

        Dim iRow As Integer
        Dim strType As String
        Dim vRecordNo As Double

        If chkGrading.Checked = True Then
            strType = "G"
        Else
            strType = "A"
        End If

        For iRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(14, iRow).Value = True Or flxDetails.Item(14, iRow).Value = 1 Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT Special FROM tblNoneOrders WHERE OrderNo = '" & flxDetails.Item(0, iRow).Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If strType = "A" Then
                        If rsComSql.Fields("Special").Value <> 0 Then
                            MsgBox("Invalid Transfer to PCU - " & flxDetails.Item(0, iRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                    Else
                        If rsComSql.Fields("Special").Value <> 1 Then
                            MsgBox("Invalid Transfer to Grading - " & flxDetails.Item(0, iRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                    End If
                End If
                rsComSql = Nothing
            End If
        Next

        If optNew.Checked = True Then

            rsComSql = New ADODB.Recordset
            mStrSQL = "SELECT MAX(RecordNo) AS RecordNo FROM tblPCUFinishOrders"
            rsComSql.Open(mStrSQL, AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                vRecordNo = IIf(Not IsDBNull(rsComSql.Fields("RecordNo").Value), rsComSql.Fields("RecordNo").Value + 1, 1)
            End If
            rsComSql = Nothing

            For iRow = 0 To flxDetails.RowCount - 1
                If flxDetails.Item(14, iRow).Value = True Or flxDetails.Item(14, iRow).Value = 1 Then
                    mStrSQL = "INSERT INTO tblPCUFinishOrders(OrderNo,Subject,Reference,Side,ParNo,Assortment,AssPrice,PacketNo,FinishedPcs,FinishedCts,PacketPcs,PacketCts," & _
                                "IssueCts,RateCode,Export,Status,AuditNo,RecordNo,ModifyBy,NLineNo,Category,Company,Supplier,InvoiceNo,DoneBy) " & _
                              "VALUES('" & flxDetails.Item(0, iRow).Value & "','" & flxDetails.Item(1, iRow).Value & "','" & flxDetails.Item(2, iRow).Value & "','" & flxDetails.Item(3, iRow).Value & "'," & _
                                "'" & flxDetails.Item(4, iRow).Value & "','" & flxDetails.Item(5, iRow).Value & "','" & CDbl(flxDetails.Item(6, iRow).Value) & "','" & flxDetails.Item(7, iRow).Value & "'," & _
                                "'" & CDbl(flxDetails.Item(8, iRow).Value) & "','" & CDbl(flxDetails.Item(9, iRow).Value) & "','" & CDbl(flxDetails.Item(10, iRow).Value) & "','" & CDbl(flxDetails.Item(11, iRow).Value) & "'," & _
                                "'" & CDbl(flxDetails.Item(12, iRow).Value) & "','" & flxDetails.Item(13, iRow).Value & "',1,'" & strType & "',0,'" & vRecordNo & "','" & PBUser_EmpNo & "'," & _
                                "'" & flxDetails.Item(23, iRow).Value & "','" & flxDetails.Item(24, iRow).Value & "','" & flxDetails.Item(25, iRow).Value & "','" & flxDetails.Item(26, iRow).Value & "'," & _
                                "'" & flxDetails.Item(27, iRow).Value & "','" & PBUser_EmpNo & "')"

                    AdoCN.Execute(mStrSQL)
                    AdoCN.Execute("UPDATE tblReturns SET Status = 1 WHERE ParNo = '" & flxDetails.Item(0, iRow).Value & "' AND PktNo = '" & flxDetails.Item(7, iRow).Value & "' AND Sec = 18 AND Status  = 0")

                    vRecordNo = vRecordNo + 1
                End If
            Next
            MsgBox("Order Verification Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            flxDetails.Rows.Clear()
        Else
            For iRow = 0 To flxDetails.RowCount - 1
                If flxDetails.Item(14, iRow).Value = True Or flxDetails.Item(14, iRow).Value = 1 Then
                    mStrSQL = "UPDATE tblPCUFinishOrders SET PacketPcs = " & CDbl(flxDetails.Item(10, iRow).Value) & ",PacketCts = " & CDbl(flxDetails.Item(11, iRow).Value) & ",FinishedPcs = " & CDbl(flxDetails.Item(8, iRow).Value) & ",FinishedCts = " & CDbl(flxDetails.Item(9, iRow).Value) & "," & _
                                        "IssueCts = " & CDbl(flxDetails.Item(12, iRow).Value) & ",ModifyBy = '" & PBUser_EmpNo & "',Export = 1 " & _
                                      "WHERE RecordNo = " & CDbl(flxDetails.Item(15, iRow).Value) & " AND OrderNo = '" & flxDetails.Item(0, iRow).Value & "' AND Status = 'A'"
                    AdoCN.Execute(mStrSQL)
                End If
            Next

            MsgBox("Order Verification Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            flxDetails.Rows.Clear()
        End If
        Exit Sub
Err_Trap:

    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        flxDetails.Rows.Clear()
        txtOrder.Text = ""
        chkSelect.Checked = False
    End Sub

    Private Sub Load_SavedData()
        Dim strType As String

        flxDetails.Rows.Clear()

        If chkGrading.Checked = True Then
            strType = "G"
        Else
            strType = "A"
        End If

        rsComSql = New ADODB.Recordset
        mStrSQL = "SELECT * FROM tblPCUFinishOrders WHERE Status = '" & strType & "' ORDER BY OrderNo, Reference, PacketNo"
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("OrderNo").Value,
                                    rsComSql.Fields("Subject").Value,
                                    rsComSql.Fields("Reference").Value,
                                    rsComSql.Fields("Side").Value,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("Assortment").Value,
                                    Format(rsComSql.Fields("AssPrice").Value, "#0.00"),
                                    rsComSql.Fields("PacketNo").Value,
                                    rsComSql.Fields("FinishedPcs").Value,
                                    Format(rsComSql.Fields("FinishedCts").Value, "#0.000"),
                                    rsComSql.Fields("PacketPcs").Value,
                                    Format(rsComSql.Fields("PacketCts").Value, "#0.000"),
                                    Format(rsComSql.Fields("IssueCts").Value, "#0.000"),
                                    rsComSql.Fields("RateCode").Value,
                                    rsComSql.Fields("Export").Value,
                                    rsComSql.Fields("RecordNo").Value, 0, 0, 0, 0, 0, 0, "",
                                    rsComSql.Fields("NLineNo").Value,
                                    rsComSql.Fields("Category").Value,
                                    rsComSql.Fields("Company").Value,
                                    rsComSql.Fields("Supplier").Value,
                                    rsComSql.Fields("InvoiceNo").Value)

                rsComSql.MoveNext()
            End While
        Else
            MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        SaveData()
    End Sub

    Private Sub txtOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrder.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub optNew_CheckedChanged(sender As Object, e As EventArgs) Handles optNew.CheckedChanged
        flxDetails.Rows.Clear()
    End Sub

    Private Sub optEdit_CheckedChanged(sender As Object, e As EventArgs) Handles optEdit.CheckedChanged
        flxDetails.Rows.Clear()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(Me.flxDetails)
    End Sub

    Private Sub frm_PCUFinishOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub
End Class