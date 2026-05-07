
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLParcelSelection

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        txtParNo.Text = ""
        flxDetails.Rows.Clear()
    End Sub

    Private Sub Load_ParcelDetails(ByVal strParcelNo As String)
        Dim blnFound As Boolean
        Dim blnParcel As Boolean

        Dim dblPcs As Double
        Dim dblCts As Double
        Dim dblValue As Double
        Dim dblPrice As Double
        Dim strDCLParNo As String
        Dim strParType As String
        Dim dblInvPrice As Double
        Dim dblActPrice As Double

        Dim dblRejPcs As Double
        Dim dblRejCts As Double

        blnFound = False
        blnParcel = False
        dblActPrice = 0
        strDCLParNo = ""
        strParType = ""

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT SupParcelNo, AssortmentNo, SUM(ACTPcs) AS ACTPcs, ROUND(SUM(INVCts), 3) AS INVCts, ROUND(SUM(INVCts * ItemCost), 2) AS Value, ParcelType, MAX(DCLParcelNo) AS DCLParcelNo, ItemCost, ActItemCost, LotNo " & _
                      "FROM tblImport WHERE SupParcelNo = '" & strParcelNo & "' AND SupplierRefNo NOT LIKE 'LCL%' AND SupplierCode <> 23 " & _
                      "GROUP BY SupParcelNo, AssortmentNo, ParcelType, ItemCost, ActItemCost, LotNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            blnFound = True
            strDCLParNo = rsComSql.Fields("DCLParcelNo").Value
            strParType = rsComSql.Fields("ParcelType").Value
            dblActPrice = rsComSql.Fields("ActItemCost").Value

            flxDetails.Rows.Add(rsComSql.Fields("SupParcelNo").Value,
                                rsComSql.Fields("AssortmentNo").Value,
                                rsComSql.Fields("ActPcs").Value,
                                rsComSql.Fields("INVCts").Value,
                                rsComSql.Fields("ItemCost").Value,
                                Format(Math.Round(rsComSql.Fields("Value").Value, 2), "###,##0.00"))

            flxDetails.Item(45, flxDetails.Rows.Count - 1).Value = rsComSql.Fields("LotNo").Value

            If dblActPrice = 0 Then dblActPrice = rsComSql.Fields("ItemCost").Value
        End If
        rsComSql = Nothing

        If blnFound = True Then
            dblPcs = 0
            dblCts = 0
            dblValue = 0
            dblPrice = 0

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(dbo.tblExpSizingTypes.Pcs) AS Pcs, ROUND(SUM(dbo.tblExpSizingTypes.Cts), 3) AS Cts, " & _
                            "ROUND(Sum(dbo.tblExpSizingTypes.Cts * dbo.tblExpSizingTypes.BasePrice), 2) As Value " & _
                          "FROM dbo.tblExpSizingTypes INNER JOIN dbo.tblAssortList ON dbo.tblExpSizingTypes.ReturnType = dbo.tblAssortList.Assortment " & _
                          "GROUP BY dbo.tblExpSizingTypes.ParNo, dbo.tblExpSizingTypes.Department " & _
                          "HAVING (dbo.tblExpSizingTypes.ParNo = '" & strParcelNo & "') AND (dbo.tblExpSizingTypes.Department = 'Mix')", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                    dblPcs = rsComSql.Fields("Pcs").Value
                    dblCts = rsComSql.Fields("Cts").Value
                    dblPrice = Math.Round(rsComSql.Fields("Value").Value / rsComSql.Fields("Cts").Value, 2)
                    dblValue = Math.Round(rsComSql.Fields("Value").Value, 2)
                End If
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(dbo.tblExpSizingTypes.Pcs) AS Pcs, ROUND(SUM(dbo.tblExpSizingTypes.Cts), 3) AS Cts, " & _
                                "ROUND(SUM(dbo.tblExpSizingTypes.Cts * dbo.tblExpSizingTypes.BasePrice), 2) As Value " & _
                          "FROM dbo.tblExpSizingTypes INNER JOIN dbo.tblAssortList ON dbo.tblExpSizingTypes.ReturnType = dbo.tblAssortList.Assortment INNER JOIN " & _
                                "dbo.tblParcel ON dbo.tblExpSizingTypes.Department = dbo.tblParcel.Depart AND dbo.tblExpSizingTypes.ParNo = dbo.tblParcel.GrpParNo " & _
                          "WHERE (dbo.tblParcel.OrigParcelNo = '" & strParcelNo & "') AND (dbo.tblExpSizingTypes.Department <> 'Mix')", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                    dblPcs = dblPcs + rsComSql.Fields("Pcs").Value
                    dblCts = dblCts + rsComSql.Fields("Cts").Value
                    dblPrice = Math.Round(rsComSql.Fields("Value").Value / rsComSql.Fields("Cts").Value, 2)
                    dblValue = dblValue + rsComSql.Fields("Value").Value
                Else
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts, ROUND(SUM(Cts * BasePrice), 2) AS Value " & _
                                    "FROM dbo.tblExpStock " & _
                                    "WHERE (ParNo = '" & strParcelNo & "') AND (Department <> 'Mix') " & _
                                    "GROUP BY ParNo", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                            dblPcs = dblPcs + rsComSql_1.Fields("Pcs").Value
                            dblCts = dblCts + rsComSql_1.Fields("Cts").Value
                            dblPrice = rsComSql_1.Fields("Value").Value / rsComSql_1.Fields("Cts").Value
                            dblValue = dblValue + rsComSql_1.Fields("Value").Value
                        End If
                    End If
                    rsComSql_1 = Nothing
                End If
            End If
            rsComSql = Nothing

            flxDetails.Item(6, flxDetails.Rows.Count - 1).Value = dblPcs
            flxDetails.Item(7, flxDetails.Rows.Count - 1).Value = dblCts
            If dblCts > 0 Then
                flxDetails.Item(8, flxDetails.Rows.Count - 1).Value = Format(dblValue / dblCts, "###,##0.00")
            Else
                flxDetails.Item(8, flxDetails.Rows.Count - 1).Value = Format(0, "###,##0.00")
            End If
            flxDetails.Item(9, flxDetails.Rows.Count - 1).Value = Format(dblValue, "###,##0.00")

            flxDetails.Item(10, flxDetails.Rows.Count - 1).Value = CDbl(flxDetails.Item(2, flxDetails.Rows.Count - 1).Value) - CDbl(flxDetails.Item(6, flxDetails.Rows.Count - 1).Value)
            flxDetails.Item(11, flxDetails.Rows.Count - 1).Value = CDbl(flxDetails.Item(3, flxDetails.Rows.Count - 1).Value) - CDbl(flxDetails.Item(7, flxDetails.Rows.Count - 1).Value)
            flxDetails.Item(13, flxDetails.Rows.Count - 1).Value = Format(CDbl(flxDetails.Item(5, flxDetails.Rows.Count - 1).Value) - CDbl(flxDetails.Item(9, flxDetails.Rows.Count - 1).Value), "###,##0.00")
            If CDbl(flxDetails.Item(11, flxDetails.Rows.Count - 1).Value) > 0 Then
                flxDetails.Item(12, flxDetails.Rows.Count - 1).Value = Format(CDbl(flxDetails.Item(13, flxDetails.Rows.Count - 1).Value) / CDbl(flxDetails.Item(11, flxDetails.Rows.Count - 1).Value), "###,##0.00")
            Else
                flxDetails.Item(12, flxDetails.Rows.Count - 1).Value = Format(0, "###,##0.00")
            End If

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(ACTPcs) AS ACTPcs, ROUND(SUM(INVCts), 3) AS INVCts " & _
                          "FROM tblImport " & _
                          "WHERE SupParcelNo = '" & strParcelNo & "' AND SupplierRefNo LIKE 'LCL%'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("ACTPcs").Value) Then
                    flxDetails.Item(14, flxDetails.Rows.Count - 1).Value = rsComSql.Fields("ACTPcs").Value
                    flxDetails.Item(15, flxDetails.Rows.Count - 1).Value = rsComSql.Fields("INVCts").Value
                Else
                    flxDetails.Item(14, flxDetails.Rows.Count - 1).Value = "0"
                    flxDetails.Item(15, flxDetails.Rows.Count - 1).Value = "0"
                End If
            End If
            rsComSql = Nothing

            flxDetails.Item(16, flxDetails.Rows.Count - 1).Value = "0"
            flxDetails.Item(17, flxDetails.Rows.Count - 1).Value = "0"
            flxDetails.Item(18, flxDetails.Rows.Count - 1).Value = "0"
            flxDetails.Item(19, flxDetails.Rows.Count - 1).Value = "0"

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT GrpParNo,IssuedPcs,IssuedCts FROM dbo.tblParcel " & _
                          "WHERE OrigParcelNo = '" & strParcelNo & "' AND (RIGHT(GrpParNo, 1) = 'N' OR RIGHT(GrpParNo, 1) = 'V') ORDER BY GrpParNo", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    If Not IsDBNull(rsComSql.Fields("IssuedPcs").Value) Then
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT ROUND(SUM(PktCts * Price) / SUM(PktCts), 2) AS InvPrice " & _
                                        "FROM dbo.tblParcelDetails " & _
                                        "WHERE (ParcelNo = '" & rsComSql.Fields("GrpParNo").Value & "')", AdoCN, 1, 1)
                        If Not IsDBNull(rsComSql_1.Fields("InvPrice").Value) Then
                            dblInvPrice = rsComSql_1.Fields("InvPrice").Value
                        End If
                        rsComSql_1 = Nothing

                        flxDetails.Item(16, flxDetails.Rows.Count - 1).Value = CDbl(flxDetails.Item(16, flxDetails.Rows.Count - 1).Value) + rsComSql.Fields("IssuedPcs").Value
                        flxDetails.Item(17, flxDetails.Rows.Count - 1).Value = Math.Round(CDbl(flxDetails.Item(17, flxDetails.Rows.Count - 1).Value) + rsComSql.Fields("IssuedCts").Value, 3)
                        flxDetails.Item(19, flxDetails.Rows.Count - 1).Value = Math.Round(CDbl(flxDetails.Item(19, flxDetails.Rows.Count - 1).Value) + dblInvPrice * rsComSql.Fields("IssuedCts").Value, 2)
                        If CDbl(flxDetails.Item(17, flxDetails.Rows.Count - 1).Value) > 0 Then
                            flxDetails.Item(18, flxDetails.Rows.Count - 1).Value = Format(CDbl(flxDetails.Item(19, flxDetails.Rows.Count - 1).Value) / CDbl(flxDetails.Item(17, flxDetails.Rows.Count - 1).Value), "###,##0.00")
                        Else
                            flxDetails.Item(18, flxDetails.Rows.Count - 1).Value = Format(0, "###,##0.00")
                        End If
                    End If

                    rsComSql.MoveNext()
                End While
            Else
                flxDetails.Item(16, flxDetails.Rows.Count - 1).Value = "0"
                flxDetails.Item(17, flxDetails.Rows.Count - 1).Value = "0"
                flxDetails.Item(18, flxDetails.Rows.Count - 1).Value = "0"
                flxDetails.Item(19, flxDetails.Rows.Count - 1).Value = "0"
            End If
            rsComSql = Nothing

            If Val(flxDetails.Item(14, flxDetails.Rows.Count - 1).Value) And strParType = "Rough" > 0 Then
                flxDetails.Item(10, flxDetails.Rows.Count - 1).Value = Val(flxDetails.Item(14, flxDetails.Rows.Count - 1).Value) - Val(flxDetails.Item(6, flxDetails.Rows.Count - 1).Value)
            End If

            If Val(flxDetails.Item(17, flxDetails.Rows.Count - 1).Value) > 0 Then
                flxDetails.Item(11, flxDetails.Rows.Count - 1).Value = Format(Val(flxDetails.Item(11, flxDetails.Rows.Count - 1).Value) - Val(flxDetails.Item(17, flxDetails.Rows.Count - 1).Value), "#0.000")
                flxDetails.Item(13, flxDetails.Rows.Count - 1).Value = Format(CDbl(flxDetails.Item(13, flxDetails.Rows.Count - 1).Value) - CDbl(flxDetails.Item(19, flxDetails.Rows.Count - 1).Value), "###,##0.00")
                If CDbl(flxDetails.Item(11, flxDetails.Rows.Count - 1).Value) <> 0 Then
                    flxDetails.Item(12, flxDetails.Rows.Count - 1).Value = Format(CDbl(flxDetails.Item(13, flxDetails.Rows.Count - 1).Value) / CDbl(flxDetails.Item(11, flxDetails.Rows.Count - 1).Value), "###,##0.00")
                Else
                    flxDetails.Item(12, flxDetails.Rows.Count - 1).Value = "0.00"
                End If
            End If

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(dbo.tblExpSizingTypes.Pcs) AS Pcs, ROUND(SUM(dbo.tblExpSizingTypes.Cts), 3) AS Cts, " & _
                            "ROUND(Sum(dbo.tblExpSizingTypes.Cts * dbo.tblExpSizingTypes.BasePrice), 2) As Value " & _
                          "FROM dbo.tblExpSizingTypes INNER JOIN dbo.tblAssortList ON dbo.tblExpSizingTypes.ReturnType = dbo.tblAssortList.Assortment " & _
                          "GROUP BY dbo.tblExpSizingTypes.ParNo, dbo.tblExpSizingTypes.Department " & _
                          "HAVING (dbo.tblExpSizingTypes.ParNo = '" & strParcelNo & "') AND (dbo.tblExpSizingTypes.Department = 'Mix')", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                    blnParcel = True
                    flxDetails.Item(20, flxDetails.Rows.Count - 1).Value = rsComSql.Fields("Pcs").Value
                    flxDetails.Item(21, flxDetails.Rows.Count - 1).Value = rsComSql.Fields("Cts").Value
                    If rsComSql.Fields("Cts").Value > 0 Then
                        flxDetails.Item(22, flxDetails.Rows.Count - 1).Value = Format(rsComSql.Fields("Value").Value / rsComSql.Fields("Cts").Value, "###,##0.00")
                    Else
                        flxDetails.Item(22, flxDetails.Rows.Count - 1).Value = Format(0, "###,##0.00")
                    End If
                    flxDetails.Item(23, flxDetails.Rows.Count - 1).Value = Format(rsComSql.Fields("Value").Value, "###,##0.00")
                Else
                    flxDetails.Item(20, flxDetails.Rows.Count - 1).Value = "0"
                    flxDetails.Item(21, flxDetails.Rows.Count - 1).Value = "0"
                    flxDetails.Item(22, flxDetails.Rows.Count - 1).Value = "0"
                    flxDetails.Item(23, flxDetails.Rows.Count - 1).Value = "0"
                End If
            Else
                flxDetails.Item(20, flxDetails.Rows.Count - 1).Value = "0"
                flxDetails.Item(21, flxDetails.Rows.Count - 1).Value = "0"
                flxDetails.Item(22, flxDetails.Rows.Count - 1).Value = "0"
                flxDetails.Item(23, flxDetails.Rows.Count - 1).Value = "0"
            End If
            rsComSql = Nothing

            If blnParcel = False Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(dbo.tblExpSizingTypes.Pcs) AS Pcs, ROUND(SUM(dbo.tblExpSizingTypes.Cts), 3) AS Cts, " & _
                                "ROUND(Sum(dbo.tblExpSizingTypes.Cts * dbo.tblExpSizingTypes.BasePrice), 2) As Value " & _
                            "FROM dbo.tblDep_Trf INNER JOIN dbo.tblExpSizingTypes ON dbo.tblDep_Trf.Department = dbo.tblExpSizingTypes.Department AND " & _
                                "dbo.tblDep_Trf.DCLParcelNo = dbo.tblExpSizingTypes.ParNo INNER JOIN dbo.tblAssortList ON dbo.tblExpSizingTypes.ReturnType = dbo.tblAssortList.Assortment " & _
                            "WHERE (dbo.tblDep_Trf.SupParcelNo = '" & strParcelNo & "') AND (dbo.tblDep_Trf.Department = 'Mix')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                        flxDetails.Item(20, flxDetails.Rows.Count - 1).Value = CDbl(flxDetails.Item(20, flxDetails.Rows.Count - 1).Value) + rsComSql.Fields("Pcs").Value
                        flxDetails.Item(21, flxDetails.Rows.Count - 1).Value = CDbl(flxDetails.Item(21, flxDetails.Rows.Count - 1).Value) + rsComSql.Fields("Cts").Value
                        If rsComSql.Fields("Cts").Value > 0 Then
                            flxDetails.Item(22, flxDetails.Rows.Count - 1).Value = Format((rsComSql.Fields("Value").Value + CDbl(flxDetails.Item(23, flxDetails.Rows.Count - 1).Value)) / (CDbl(flxDetails.Item(21, flxDetails.Rows.Count - 1).Value)), "###,##0.00")
                        End If
                        flxDetails.Item(23, flxDetails.Rows.Count - 1).Value = Format(rsComSql.Fields("Value").Value + CDbl(flxDetails.Item(23, flxDetails.Rows.Count - 1).Value), "###,##0.00")

                        flxDetails.Item(6, flxDetails.Rows.Count - 1).Value = CDbl(flxDetails.Item(20, flxDetails.Rows.Count - 1).Value) + CDbl(flxDetails.Item(6, flxDetails.Rows.Count - 1).Value)
                        flxDetails.Item(7, flxDetails.Rows.Count - 1).Value = CDbl(flxDetails.Item(21, flxDetails.Rows.Count - 1).Value) + CDbl(flxDetails.Item(7, flxDetails.Rows.Count - 1).Value)
                        If rsComSql.Fields("Cts").Value > 0 Then
                            flxDetails.Item(8, flxDetails.Rows.Count - 1).Value = Format((CDbl(flxDetails.Item(9, flxDetails.Rows.Count - 1).Value) + CDbl(flxDetails.Item(23, flxDetails.Rows.Count - 1).Value)) / (CDbl(flxDetails.Item(7, flxDetails.Rows.Count - 1).Value)), "###,##0.00")
                        End If
                        flxDetails.Item(9, flxDetails.Rows.Count - 1).Value = Format(CDbl(flxDetails.Item(9, flxDetails.Rows.Count - 1).Value) + CDbl(flxDetails.Item(23, flxDetails.Rows.Count - 1).Value), "###,##0.00")

                        flxDetails.Item(10, flxDetails.Rows.Count - 1).Value = Val(flxDetails.Item(10, flxDetails.Rows.Count - 1).Value) - rsComSql.Fields("Pcs").Value
                        flxDetails.Item(11, flxDetails.Rows.Count - 1).Value = Format(Val(flxDetails.Item(11, flxDetails.Rows.Count - 1).Value) - rsComSql.Fields("Cts").Value, "#0.000")
                        flxDetails.Item(13, flxDetails.Rows.Count - 1).Value = Format(CDbl(flxDetails.Item(13, flxDetails.Rows.Count - 1).Value) - rsComSql.Fields("Value").Value, "###,##0.00")
                        flxDetails.Item(12, flxDetails.Rows.Count - 1).Value = Format(CDbl(flxDetails.Item(13, flxDetails.Rows.Count - 1).Value) / CDbl(flxDetails.Item(11, flxDetails.Rows.Count - 1).Value), "###,##0.00")
                    End If
                End If
                rsComSql = Nothing
            End If

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(dbo.tblExpSizingTypes.Pcs) AS Pcs, ROUND(SUM(dbo.tblExpSizingTypes.Cts), 3) AS Cts, " & _
                                "ROUND(Sum(dbo.tblExpSizingTypes.Cts * dbo.tblExpSizingTypes.BasePrice), 2) As Value " & _
                          "FROM dbo.tblExpSizingTypes INNER JOIN dbo.tblAssortList ON dbo.tblExpSizingTypes.ReturnType = dbo.tblAssortList.Assortment INNER JOIN " & _
                                "dbo.tblParcel ON dbo.tblExpSizingTypes.Department = dbo.tblParcel.Depart AND dbo.tblExpSizingTypes.ParNo = dbo.tblParcel.GrpParNo " & _
                          "WHERE (dbo.tblParcel.OrigParcelNo = '" & strParcelNo & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                    flxDetails.Item(24, flxDetails.Rows.Count - 1).Value = rsComSql.Fields("Pcs").Value
                    flxDetails.Item(25, flxDetails.Rows.Count - 1).Value = rsComSql.Fields("Cts").Value
                    If rsComSql.Fields("Cts").Value > 0 Then
                        flxDetails.Item(26, flxDetails.Rows.Count - 1).Value = Format(rsComSql.Fields("Value").Value / rsComSql.Fields("Cts").Value, "###,##0.00")
                    Else
                        flxDetails.Item(26, flxDetails.Rows.Count - 1).Value = Format(0, "###,##0.00")
                    End If
                    flxDetails.Item(27, flxDetails.Rows.Count - 1).Value = Format(rsComSql.Fields("Value").Value, "###,##0.00")
                Else
                    flxDetails.Item(24, flxDetails.Rows.Count - 1).Value = "0"
                    flxDetails.Item(25, flxDetails.Rows.Count - 1).Value = "0"
                    flxDetails.Item(26, flxDetails.Rows.Count - 1).Value = "0"
                    flxDetails.Item(27, flxDetails.Rows.Count - 1).Value = "0"
                End If
            Else
                flxDetails.Item(24, flxDetails.Rows.Count - 1).Value = "0"
                flxDetails.Item(25, flxDetails.Rows.Count - 1).Value = "0"
                flxDetails.Item(26, flxDetails.Rows.Count - 1).Value = "0"
                flxDetails.Item(27, flxDetails.Rows.Count - 1).Value = "0"
            End If
            rsComSql = Nothing

            flxDetails.Item(28, flxDetails.Rows.Count - 1).Value = strDCLParNo

            'PCU Selection
            dblRejPcs = 0
            dblRejCts = 0
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(dbo.tblPacket.PktPcs) AS Pcs, ROUND(SUM(dbo.tblPacket.PktCts), 3) AS Cts " & _
                          "FROM dbo.VW_RealParcelGrp INNER JOIN dbo.tblPacket ON dbo.VW_RealParcelGrp.ParcelNo = dbo.tblPacket.AParNo " & _
                          "WHERE (dbo.VW_RealParcelGrp.OrigParcelNo = '" & strParcelNo & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then

                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT SUM(dbo.tblReturns.RejPcs) AS RejPcs, ROUND(SUM(dbo.tblReturns.RejCts), 3) AS RejCts " & _
                                    "FROM dbo.VW_RealParcelGrp INNER JOIN dbo.tblPacket ON dbo.VW_RealParcelGrp.ParcelNo = dbo.tblPacket.AParNo INNER JOIN " & _
                                        "dbo.tblReturns ON dbo.tblPacket.PktOrdNo = dbo.tblReturns.ParNo AND dbo.tblPacket.PktNo = dbo.tblReturns.PktNo " & _
                                    "WHERE (dbo.VW_RealParcelGrp.OrigParcelNo = '" & strParcelNo & "')", AdoCN, 1, 1)
                    If Not IsDBNull(rsComSql_1.Fields("RejPcs").Value) Then
                        dblRejPcs = rsComSql_1.Fields("RejPcs").Value
                        dblRejCts = rsComSql_1.Fields("RejCts").Value
                    End If
                    rsComSql_1 = Nothing

                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT SUM(tblGrading_RghIssues.IssPcs) AS RejPcs, ROUND(SUM(tblGrading_RghIssues.IssCts), 3) AS RejCts " & _
                                    "FROM tblGrading_RghIssues INNER JOIN VW_RealParcelGrp ON tblGrading_RghIssues.ParNo = VW_RealParcelGrp.ParcelNo " & _
                                    "WHERE (VW_RealParcelGrp.OrigParcelNo = '" & strParcelNo & "')", AdoCN, 1, 1)
                    If Not IsDBNull(rsComSql_1.Fields("RejPcs").Value) Then
                        dblRejPcs = dblRejPcs + rsComSql_1.Fields("RejPcs").Value
                        dblRejCts = dblRejCts + rsComSql_1.Fields("RejCts").Value
                    End If
                    rsComSql_1 = Nothing

                    flxDetails.Item(29, flxDetails.Rows.Count - 1).Value = rsComSql.Fields("Pcs").Value - dblRejPcs
                    flxDetails.Item(30, flxDetails.Rows.Count - 1).Value = rsComSql.Fields("Cts").Value - dblRejCts
                    If rsComSql.Fields("Cts").Value > 0 Then
                        flxDetails.Item(31, flxDetails.Rows.Count - 1).Value = Format(dblActPrice, "###,##0.#0")
                    Else
                        flxDetails.Item(31, flxDetails.Rows.Count - 1).Value = Format(0, "###,##0.#0")
                    End If
                    flxDetails.Item(32, flxDetails.Rows.Count - 1).Value = Format((rsComSql.Fields("Cts").Value - dblRejCts) * dblActPrice, "###,##0.00")

                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT SUM(dbo.tblPacket.PktPcs) AS PktPcs, ROUND(SUM(dbo.tblPacket.PktCts), 3) AS PktCts, ROUND(SUM(dbo.tblPacket.PktCts * dbo.tblGrading_SizingList.PRICE), 2) AS Value " & _
                                    "FROM dbo.tblParcel INNER JOIN dbo.tblPacket ON dbo.tblParcel.GrpParNo = dbo.tblPacket.AParNo INNER JOIN " & _
                                        "dbo.tblGrading_SizingList ON dbo.tblPacket.AssortNo = dbo.tblGrading_SizingList.NAME " & _
                                    "WHERE (dbo.tblParcel.OrigParcelNo = '" & strParcelNo & "')", AdoCN, 1, 1)
                    If Not IsDBNull(rsComSql_1.Fields("PktPcs").Value) Then
                        flxDetails.Item(29, flxDetails.Rows.Count - 1).Value = CDbl(flxDetails.Item(29, flxDetails.Rows.Count - 1).Value) + rsComSql_1.Fields("PktPcs").Value
                        flxDetails.Item(30, flxDetails.Rows.Count - 1).Value = CDbl(flxDetails.Item(30, flxDetails.Rows.Count - 1).Value) + rsComSql_1.Fields("PktCts").Value
                        flxDetails.Item(32, flxDetails.Rows.Count - 1).Value = Format(CDbl(flxDetails.Item(32, flxDetails.Rows.Count - 1).Value) + rsComSql_1.Fields("Value").Value, "###,##0.00")
                        If rsComSql_1.Fields("PktCts").Value > 0 Then
                            flxDetails.Item(31, flxDetails.Rows.Count - 1).Value = Format(CDbl(flxDetails.Item(32, flxDetails.Rows.Count - 1).Value) / CDbl(flxDetails.Item(30, flxDetails.Rows.Count - 1).Value), "###,##0.#0")
                        Else
                            flxDetails.Item(31, flxDetails.Rows.Count - 1).Value = Format(0, "###,##0.#0")
                        End If

                    End If
                    rsComSql_1 = Nothing

                Else
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT SUM(dbo.tblPacket.PktPcs) AS PktPcs, ROUND(SUM(dbo.tblPacket.PktCts), 3) AS PktCts, ROUND(SUM(dbo.tblPacket.PktCts * dbo.tblGrading_SizingList.PRICE), 2) AS Value " & _
                                    "FROM dbo.tblParcel INNER JOIN dbo.tblPacket ON dbo.tblParcel.GrpParNo = dbo.tblPacket.AParNo INNER JOIN " & _
                                        "dbo.tblGrading_SizingList ON dbo.tblPacket.AssortNo = dbo.tblGrading_SizingList.NAME " & _
                                    "WHERE (dbo.tblParcel.OrigParcelNo = '" & strParcelNo & "')", AdoCN, 1, 1)
                    If Not IsDBNull(rsComSql_1.Fields("PktPcs").Value) Then
                        flxDetails.Item(29, flxDetails.Rows.Count - 1).Value = rsComSql_1.Fields("PktPcs").Value
                        flxDetails.Item(30, flxDetails.Rows.Count - 1).Value = rsComSql_1.Fields("PktCts").Value
                        If rsComSql_1.Fields("PktCts").Value > 0 Then
                            flxDetails.Item(31, flxDetails.Rows.Count - 1).Value = Format(rsComSql_1.Fields("Value").Value / rsComSql_1.Fields("PktCts").Value, "###,##0.#0")
                        Else
                            flxDetails.Item(31, flxDetails.Rows.Count - 1).Value = Format(0, "###,##0.#0")
                        End If
                        flxDetails.Item(32, flxDetails.Rows.Count - 1).Value = Format(rsComSql_1.Fields("Value").Value, "###,##0.00")
                    Else
                        flxDetails.Item(29, flxDetails.Rows.Count - 1).Value = "0"
                        flxDetails.Item(30, flxDetails.Rows.Count - 1).Value = "0"
                        flxDetails.Item(31, flxDetails.Rows.Count - 1).Value = "0"
                        flxDetails.Item(32, flxDetails.Rows.Count - 1).Value = "0"
                    End If
                    rsComSql_1 = Nothing
                End If
            Else
                flxDetails.Item(29, flxDetails.Rows.Count - 1).Value = "0"
                flxDetails.Item(30, flxDetails.Rows.Count - 1).Value = "0"
                flxDetails.Item(31, flxDetails.Rows.Count - 1).Value = "0"
                flxDetails.Item(32, flxDetails.Rows.Count - 1).Value = "0"
            End If
            rsComSql = Nothing

            flxDetails.Item(10, flxDetails.Rows.Count - 1).Value = Val(flxDetails.Item(10, flxDetails.Rows.Count - 1).Value) - Val(flxDetails.Item(29, flxDetails.Rows.Count - 1).Value)
            flxDetails.Item(11, flxDetails.Rows.Count - 1).Value = Val(flxDetails.Item(11, flxDetails.Rows.Count - 1).Value) - Val(flxDetails.Item(30, flxDetails.Rows.Count - 1).Value)
            flxDetails.Item(13, flxDetails.Rows.Count - 1).Value = Format(CDbl(flxDetails.Item(13, flxDetails.Rows.Count - 1).Value) - CDbl(flxDetails.Item(32, flxDetails.Rows.Count - 1).Value), "###,##0.00")
            If CDbl(flxDetails.Item(11, flxDetails.Rows.Count - 1).Value) > 0 Then
                flxDetails.Item(12, flxDetails.Rows.Count - 1).Value = Format(CDbl(flxDetails.Item(13, flxDetails.Rows.Count - 1).Value) / CDbl(flxDetails.Item(11, flxDetails.Rows.Count - 1).Value), "###,##0.00")
            Else
                flxDetails.Item(12, flxDetails.Rows.Count - 1).Value = Format(0, "###,##0.00")
            End If

            'Forever Mark
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT TOP (100) PERCENT SUM(dbo.tblGrading_Box_Forever.Pcs) AS Pcs, ROUND(SUM(dbo.tblGrading_Box_Forever.Cts), 3) AS Cts, " & _
                                "SUM(dbo.tblGrading_Box_Forever.Cts * dbo.tblGrading_Box_Forever.Price) As Value " & _
                          "FROM dbo.tblGrading_Box_Forever INNER JOIN dbo.tblParcel ON dbo.tblGrading_Box_Forever.ParNo + dbo.tblGrading_Box_Forever.Grp = dbo.tblParcel.GrpParNo " & _
                          "WHERE (dbo.tblGrading_Box_Forever.FM = 1) AND (dbo.tblParcel.Depart = 'Rounds') AND (dbo.tblParcel.OrigParcelNo = '" & strParcelNo & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                    flxDetails.Item(33, flxDetails.Rows.Count - 1).Value = rsComSql.Fields("Pcs").Value
                    flxDetails.Item(34, flxDetails.Rows.Count - 1).Value = rsComSql.Fields("Cts").Value
                    If rsComSql.Fields("Cts").Value > 0 Then
                        flxDetails.Item(35, flxDetails.Rows.Count - 1).Value = Format(rsComSql_1.Fields("Value").Value / rsComSql.Fields("Cts").Value, "###,##0.00")
                    Else
                        flxDetails.Item(35, flxDetails.Rows.Count - 1).Value = Format(0, "###,##0.00")
                    End If
                    flxDetails.Item(36, flxDetails.Rows.Count - 1).Value = Format(rsComSql_1.Fields("Value").Value, "###,##0.00")
                Else
                    flxDetails.Item(33, flxDetails.Rows.Count - 1).Value = "0"
                    flxDetails.Item(34, flxDetails.Rows.Count - 1).Value = "0"
                    flxDetails.Item(35, flxDetails.Rows.Count - 1).Value = "0"
                    flxDetails.Item(36, flxDetails.Rows.Count - 1).Value = "0"
                End If
            Else
                flxDetails.Item(33, flxDetails.Rows.Count - 1).Value = "0"
                flxDetails.Item(34, flxDetails.Rows.Count - 1).Value = "0"
                flxDetails.Item(35, flxDetails.Rows.Count - 1).Value = "0"
                flxDetails.Item(36, flxDetails.Rows.Count - 1).Value = "0"
            End If
            rsComSql = Nothing

            flxDetails.Item(10, flxDetails.Rows.Count - 1).Value = Val(flxDetails.Item(10, flxDetails.Rows.Count - 1).Value) - Val(flxDetails.Item(33, flxDetails.Rows.Count - 1).Value)
            flxDetails.Item(11, flxDetails.Rows.Count - 1).Value = Val(flxDetails.Item(11, flxDetails.Rows.Count - 1).Value) - Val(flxDetails.Item(34, flxDetails.Rows.Count - 1).Value)
            flxDetails.Item(13, flxDetails.Rows.Count - 1).Value = Format(CDbl(flxDetails.Item(13, flxDetails.Rows.Count - 1).Value) - CDbl(flxDetails.Item(36, flxDetails.Rows.Count - 1).Value), "###,##0.00")
            If CDbl(flxDetails.Item(11, flxDetails.Rows.Count - 1).Value) > 0 Then
                flxDetails.Item(12, flxDetails.Rows.Count - 1).Value = Format(CDbl(flxDetails.Item(13, flxDetails.Rows.Count - 1).Value) / CDbl(flxDetails.Item(11, flxDetails.Rows.Count - 1).Value), "###,##0.00")
            Else
                flxDetails.Item(12, flxDetails.Rows.Count - 1).Value = Format(0, "###,##0.00")
            End If

            'Contract
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(dbo.tblParcelReturns.PktPcs) AS Pcs, ROUND(SUM(dbo.tblParcelReturns.PktCts), 3) AS Cts, SUM(dbo.tblParcelReturns.PktCts * dbo.tblParcelReturns.Price) AS Value " & _
                          "FROM dbo.tblParcel INNER JOIN dbo.tblParcelReturns ON dbo.tblParcel.GrpParNo = dbo.tblParcelReturns.ParcelNo " & _
                          "WHERE (dbo.tblParcel.OrigParcelNo = '" & strParcelNo & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                    flxDetails.Item(37, flxDetails.Rows.Count - 1).Value = rsComSql.Fields("Pcs").Value
                    flxDetails.Item(38, flxDetails.Rows.Count - 1).Value = rsComSql.Fields("Cts").Value
                    If rsComSql.Fields("Cts").Value > 0 Then
                        flxDetails.Item(39, flxDetails.Rows.Count - 1).Value = Format(rsComSql_1.Fields("Value").Value / rsComSql.Fields("Cts").Value, "###,##0.00")
                    Else
                        flxDetails.Item(39, flxDetails.Rows.Count - 1).Value = Format(0, "###,##0.00")
                    End If
                    flxDetails.Item(40, flxDetails.Rows.Count - 1).Value = Format(rsComSql_1.Fields("Value").Value, "###,##0.00")
                Else
                    flxDetails.Item(37, flxDetails.Rows.Count - 1).Value = "0"
                    flxDetails.Item(38, flxDetails.Rows.Count - 1).Value = "0"
                    flxDetails.Item(39, flxDetails.Rows.Count - 1).Value = "0"
                    flxDetails.Item(40, flxDetails.Rows.Count - 1).Value = "0"
                End If
            Else
                flxDetails.Item(37, flxDetails.Rows.Count - 1).Value = "0"
                flxDetails.Item(38, flxDetails.Rows.Count - 1).Value = "0"
                flxDetails.Item(39, flxDetails.Rows.Count - 1).Value = "0"
                flxDetails.Item(40, flxDetails.Rows.Count - 1).Value = "0"
            End If
            rsComSql = Nothing

            'Sales
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(dbo.tblParcelRghSales.PktPcs) AS Pcs, ROUND(SUM(dbo.tblParcelRghSales.PktCts), 3) AS Cts, SUM(dbo.tblParcelRghSales.PktCts * dbo.tblParcelRghSales.Price) AS Value " & _
                          "FROM dbo.tblParcel INNER JOIN dbo.tblParcelRghSales ON dbo.tblParcel.GrpParNo = dbo.tblParcelRghSales.ParcelNo " & _
                          "WHERE (dbo.tblParcel.OrigParcelNo = '" & strParcelNo & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                    flxDetails.Item(41, flxDetails.Rows.Count - 1).Value = rsComSql.Fields("Pcs").Value
                    flxDetails.Item(42, flxDetails.Rows.Count - 1).Value = rsComSql.Fields("Cts").Value
                    If rsComSql.Fields("Cts").Value > 0 Then
                        flxDetails.Item(43, flxDetails.Rows.Count - 1).Value = Format(rsComSql_1.Fields("Value").Value / rsComSql.Fields("Cts").Value, "###,##0.00")
                    Else
                        flxDetails.Item(43, flxDetails.Rows.Count - 1).Value = Format(0, "###,##0.00")
                    End If
                    flxDetails.Item(44, flxDetails.Rows.Count - 1).Value = Format(rsComSql_1.Fields("Value").Value, "###,##0.00")
                Else
                    flxDetails.Item(41, flxDetails.Rows.Count - 1).Value = "0"
                    flxDetails.Item(42, flxDetails.Rows.Count - 1).Value = "0"
                    flxDetails.Item(43, flxDetails.Rows.Count - 1).Value = "0"
                    flxDetails.Item(44, flxDetails.Rows.Count - 1).Value = "0"
                End If
            Else
                flxDetails.Item(41, flxDetails.Rows.Count - 1).Value = "0"
                flxDetails.Item(42, flxDetails.Rows.Count - 1).Value = "0"
                flxDetails.Item(43, flxDetails.Rows.Count - 1).Value = "0"
                flxDetails.Item(44, flxDetails.Rows.Count - 1).Value = "0"
            End If
            rsComSql = Nothing

            flxDetails.Item(10, flxDetails.Rows.Count - 1).Value = Val(flxDetails.Item(10, flxDetails.Rows.Count - 1).Value) - Val(flxDetails.Item(41, flxDetails.Rows.Count - 1).Value)
            flxDetails.Item(11, flxDetails.Rows.Count - 1).Value = Val(flxDetails.Item(11, flxDetails.Rows.Count - 1).Value) - Val(flxDetails.Item(42, flxDetails.Rows.Count - 1).Value)
            flxDetails.Item(13, flxDetails.Rows.Count - 1).Value = Format(CDbl(flxDetails.Item(13, flxDetails.Rows.Count - 1).Value) - CDbl(flxDetails.Item(44, flxDetails.Rows.Count - 1).Value), "###,##0.00")
            If CDbl(flxDetails.Item(11, flxDetails.Rows.Count - 1).Value) > 0 Then
                flxDetails.Item(12, flxDetails.Rows.Count - 1).Value = Format(CDbl(flxDetails.Item(13, flxDetails.Rows.Count - 1).Value) / CDbl(flxDetails.Item(11, flxDetails.Rows.Count - 1).Value), "###,##0.00")
            Else
                flxDetails.Item(12, flxDetails.Rows.Count - 1).Value = Format(0, "###,##0.00")
            End If
        End If

    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        Load_ParcelDetails(UCase(Trim(txtParNo.Text)))
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Dim intRow As Integer
        Dim dblExpPrice As Double

        For intRow = 0 To flxDetails.Rows.Count - 1
            dblExpPrice = CDbl(flxDetails.Item(12, flxDetails.Rows.Count - 1).Value)

            mStrSQL = "UPDATE tblBAGFinishParcels SET AssPrice = " & dblExpPrice & " WHERE SuppRef = '" & flxDetails.Item(0, flxDetails.Rows.Count - 1).Value & "' AND (RIGHT(DCLRef, 1) <> 'N' AND RIGHT(DCLRef, 1) <> 'V') AND Status = 'A'"
            AdoCN.Execute(mStrSQL)

            mStrSQL = "UPDATE tblPRFinishedParcels SET AsstPrice = " & dblExpPrice & " WHERE SuppParNo = '" & flxDetails.Item(0, flxDetails.Rows.Count - 1).Value & "' AND (RIGHT(DCLParNo, 1) <> 'N' AND RIGHT(DCLParNo, 1) <> 'V') AND Status = 'A'"
            AdoCN.Execute(mStrSQL)

            mStrSQL = "UPDATE tblRndFinishParcels SET AssPrice = " & dblExpPrice & " WHERE SuppRef = '" & flxDetails.Item(0, flxDetails.Rows.Count - 1).Value & "' AND (RIGHT(DCLRef, 1) <> 'N' AND RIGHT(DCLRef, 1) <> 'V') AND Status = 'A'"
            AdoCN.Execute(mStrSQL)

            mStrSQL = "UPDATE tblExtFinishParcels SET AssPrice = " & dblExpPrice & " WHERE SuppRef = '" & flxDetails.Item(0, flxDetails.Rows.Count - 1).Value & "' AND (RIGHT(DCLRef, 1) <> 'N' AND RIGHT(DCLRef, 1) <> 'V') AND Status = 'A'"
            AdoCN.Execute(mStrSQL)

            mStrSQL = "UPDATE tblParcel SET FinalRate = " & dblExpPrice & " WHERE OrigParcelNo = '" & flxDetails.Item(0, flxDetails.Rows.Count - 1).Value & "' AND Grp <> 'N' AND Grp <> 'V'"
            AdoCN.Execute(mStrSQL)
        Next

        MsgBox("Price Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        flxDetails.Rows.Clear()
        txtParNo.Text = ""
    End Sub

    Private Sub frm_DCLParcelSelection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub
End Class