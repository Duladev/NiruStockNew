
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLPackingList

    Private Sub ClearFields()
        txtPackListNo.Text = ""
        cmbDesc.Text = ""
        cmbDeliver.Text = ""
        cmbType.Text = ""
        txtPackNo.Text = ""
        flxDetails.Rows.Clear()
        txtTotPcs.Text = ""
        txtTotCts.Text = ""
        cmbSupplierCode.Text = ""
        txtSupCode.Text = ""
        cmbCategory.Text = ""
        txtNewPackListNo.Text = ""
        cmbClient.Text = ""
        GetNextPackListNo()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub GetNextPackListNo()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(PackingListNo) AS MaxNo FROM tblGrading_Pack", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("MaxNo").Value) Then
                txtPackListNo.Text = rsComSql.Fields("MaxNo").Value + 1
            Else
                txtPackListNo.Text = "1"
            End If
        End If
        rsComSql = Nothing
    End Sub

    Private Sub frm_DCLPackingList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        GetNextPackListNo()
        Load_Delivery()
        Load_Description()
        Load_Sup()
        Load_Type()
        Load_Category()
        Load_Client()
    End Sub

    Private Sub txtPackListNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPackListNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            flxDetails.Rows.Clear()
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT dbo.tblGrading_Pack.PackingListNo, dbo.tblGrading_Pack.Delivery, dbo.tblGrading_Pack.PackType, dbo.tblGrading_Pack.CustCode, dbo.tblGrading_Pack.Description, dbo.tblSuppliers.CompanyName, dbo.tblGrading_Pack.Category, dbo.tblGrading_Pack.Client " & _
                          "FROM dbo.tblGrading_Pack INNER JOIN dbo.tblSuppliers ON dbo.tblGrading_Pack.CustCode = dbo.tblSuppliers.SupplierCode " & _
                          "GROUP BY dbo.tblGrading_Pack.PackingListNo, dbo.tblGrading_Pack.Delivery, dbo.tblGrading_Pack.PackType, dbo.tblGrading_Pack.CustCode, dbo.tblGrading_Pack.Description, dbo.tblSuppliers.CompanyName, dbo.tblGrading_Pack.Category, dbo.tblGrading_Pack.Client " & _
                          "HAVING (dbo.tblGrading_Pack.PackingListNo = '" & CDbl(txtPackListNo.Text) & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                cmbDesc.Text = rsComSql.Fields("Description").Value
                cmbDeliver.Text = rsComSql.Fields("Delivery").Value
                cmbType.Text = rsComSql.Fields("PackType").Value & ""
                txtSupCode.Text = rsComSql.Fields("CustCode").Value
                cmbSupplierCode.Text = rsComSql.Fields("CompanyName").Value
                cmbCategory.Text = rsComSql.Fields("Category").Value
                cmbClient.Text = rsComSql.Fields("Client").Value
                txtNewPackListNo.Text = rsComSql.Fields("PackingListNo").Value

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT ID, Department, ParNo, PackNo, Type, CompCode, Category, Supplier " & _
                                "FROM tblGrading_Pack " & _
                                "WHERE(PackingListNo = '" & CDbl(txtPackListNo.Text) & "') " & _
                                "ORDER BY ID DESC", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    rsComSql_1.MoveFirst()
                    While Not rsComSql_1.EOF
                        flxDetails.Rows.Add(rsComSql_1.Fields("Department").Value,
                                            rsComSql_1.Fields("ParNo").Value,
                                            "0",
                                            "0",
                                            rsComSql_1.Fields("PackNo").Value,
                                            rsComSql_1.Fields("Type").Value,
                                            rsComSql_1.Fields("CompCode").Value,
                                            rsComSql_1.Fields("Category").Value,
                                            rsComSql_1.Fields("Supplier").Value)

                        rsComSql_1.MoveNext()
                    End While
                End If
                rsComSql_1 = Nothing
            Else
                MsgBox("New Packing List No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub cndRefresh_Click(sender As Object, e As EventArgs) Handles cndRefresh.Click
        GetNextPackListNo()
    End Sub

    Private Sub Load_Category()
        cmbCategory.Items.Clear()
        cmbCategory.Items.Add("NFE")
        cmbCategory.Items.Add("Purchased")
        cmbCategory.Items.Add("Consignment")
    End Sub

    Private Sub Load_Sup()
        cmbSupplierCode.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblSuppliers ORDER BY CompanyName", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbSupplierCode.Items.Add(rsComSql.Fields("CompanyName").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Client()
        cmbClient.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblNiruRef ORDER BY NiruCust", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbClient.Items.Add(rsComSql.Fields("NiruCust").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Description()
        cmbDesc.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblDescription ORDER BY Description", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbDesc.Items.Add(rsComSql.Fields("Description").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Delivery()
        cmbDeliver.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblDelivery ORDER BY Delivery", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbDeliver.Items.Add(rsComSql.Fields("Delivery").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Type()
        cmbType.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblDCLPackingListType ORDER BY PackType", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbType.Items.Add(rsComSql.Fields("PackType").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub AddPackage()
        Dim intRow As Integer
        Dim strMainType As String
        Dim strMainComp As String
        Dim strMainCat As String
        Dim strMainSup As String
        Dim strType As String
        Dim strCompCode As String
        Dim strCat As String
        Dim strSupplier As String

        strType = ""
        If optParcel.Checked = True Then
            strType = "P"
        End If
        If optOrder.Checked = True Then
            strType = "O"
        End If
        If optSorting.Checked = True Then
            strType = "S"
        End If
        If optPCU.Checked = True Then
            strType = "C"
        End If
        If optPolRej.Checked = True Then
            strType = "J"
        End If
        If optRghRej.Checked = True Then
            strType = "R"
        End If
        If optContract.Checked = True Then
            strType = "I"
        End If
        If optMix.Checked = True Then
            strType = "M"
        End If
        If optGMix.Checked = True Then
            strType = "G"
        End If
        If optSales.Checked = True Then
            strType = "L"
        End If
        If optMixExport.Checked = True Then
            strType = "E"
        End If
        If optPolBoxTrf.Checked = True Then
            strType = "B"
        End If
        If optRounds.Checked = True Then
            strType = "X"
        End If
        If optColombo.Checked = True Then
            strType = "Y"
        End If
        If optRghSales.Checked = True Then
            strType = "H"
        End If
        If optPrecision.Checked = True Then
            strType = "K"
        End If
        If optKit.Checked = True Then
            strType = "T"
        End If

        strMainType = ""
        If flxDetails.Rows.Count > 0 Then
            strMainType = flxDetails.Item(5, 0).Value
        End If

        strMainComp = ""
        If flxDetails.Rows.Count > 0 Then
            strMainComp = flxDetails.Item(6, 0).Value
        End If

        strMainCat = ""
        If flxDetails.Rows.Count > 0 Then
            strMainCat = flxDetails.Item(7, 0).Value
        End If

        strMainSup = ""
        If flxDetails.Rows.Count > 0 Then
            strMainSup = flxDetails.Item(8, 0).Value
        End If

        If strMainType <> "" Then
            If strMainType <> strType Then
                MsgBox("Invalid Package", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        End If

        If strType <> "P" And strType <> "Y" Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_Pack WHERE PackNo = " & CDbl(txtPackNo.Text) & " AND Type = '" & strType & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                MsgBox("Already in the Packing List - " & rsComSql.Fields("PackingListNo").Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing
        End If

        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(4, intRow).Value = txtPackNo.Text And flxDetails.Item(5, intRow).Value = strType Then
                MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        rsComSql = New ADODB.Recordset
        If optParcel.Checked = True Then
            rsComSql.Open("SELECT dbo.tblGrading_PackingList.Department, dbo.tblGrading_PackingList.ParNo, SUM(dbo.tblGrading_PackingList.ActPcs) AS Pcs, " & _
                                "ROUND(SUM(dbo.tblGrading_PackingList.ActCts), 3) AS Cts, dbo.tblParcel.OrigParcelNo, dbo.VW_DCLImportsLotSupComp.CompCode, " & _
                                "dbo.VW_DCLImportsLotSupComp.Category, dbo.VW_DCLImportsLotSupComp.CompanyName " & _
                          "FROM dbo.tblGrading_PackingList INNER JOIN dbo.tblParcel ON dbo.tblGrading_PackingList.Department = dbo.tblParcel.Depart AND dbo.tblGrading_PackingList.ParNo = dbo.tblParcel.GrpParNo INNER JOIN " & _
                                "dbo.VW_DCLImportsLotSupComp ON dbo.tblParcel.OrigParcelNo = dbo.VW_DCLImportsLotSupComp.SupParcelNo " & _
                          "WHERE (dbo.tblGrading_PackingList.PackNo = " & CDbl(txtPackNo.Text) & ") " & _
                          "GROUP BY dbo.tblGrading_PackingList.Department, dbo.tblGrading_PackingList.ParNo, dbo.tblParcel.OrigParcelNo, dbo.VW_DCLImportsLotSupComp.CompanyName, " & _
                                "dbo.VW_DCLImportsLotSupComp.CompCode, dbo.VW_DCLImportsLotSupComp.Category", AdoCN, 1, 1)
        End If
        If optOrder.Checked = True Then
            'rsComSql.Open("SELECT Department, ParNo, SUM(ActPcs) AS Pcs, ROUND(SUM(ActCts), 3) AS Cts, OrderNo, RefNo, Side, Company AS CompCode, Category, Supplier AS CompanyName " & _
            '              "FROM dbo.tblGrading_PackingListPCU " & _
            '              "WHERE (PackNo = " & CDbl(txtPackNo.Text) & ") " & _
            '              "GROUP BY Department, ParNo, OrderNo, RefNo, Side, Company, Category, Supplier", AdoCN, 1, 1)

            rsComSql.Open("SELECT Department, ParNo, SUM(ActPcs) AS Pcs, ROUND(SUM(ActCts), 3) AS Cts, OrderNo, Company AS CompCode, Category, Supplier AS CompanyName " & _
                          "FROM dbo.tblGrading_PackingListPCU " & _
                          "WHERE (PackNo = " & CDbl(txtPackNo.Text) & ") " & _
                          "GROUP BY Department, ParNo, OrderNo, Company, Category, Supplier", AdoCN, 1, 1)

            'rsComSql.Open("SELECT Department, ParNo, SUM(ActPcs) AS Pcs, ROUND(SUM(ActCts), 3) AS Cts, Company AS CompCode, Category, Supplier AS CompanyName " & _
            '              "FROM dbo.tblGrading_PackingListPCU " & _
            '              "WHERE (PackNo = " & CDbl(txtPackNo.Text) & ") " & _
            '              "GROUP BY Department, ParNo, Company, Category, Supplier", AdoCN, 1, 1)
        End If
        If optSorting.Checked = True Then
            rsComSql.Open("SELECT dbo.tblGrading_PackingList.Department, dbo.tblGrading_PackingList.ParNo, SUM(dbo.tblGrading_PackingList.ActPcs) AS Pcs, " & _
                                "ROUND(SUM(dbo.tblGrading_PackingList.ActCts), 3) AS Cts, dbo.VW_RealDepTrfPack.SupParcelNo, dbo.VW_RealDepTrfPack.CompCode, " & _
                                "dbo.VW_RealDepTrfPack.Category, dbo.VW_RealDepTrfPack.CompanyName " & _
                          "FROM dbo.tblGrading_PackingList INNER JOIN dbo.VW_RealDepTrfPack ON dbo.tblGrading_PackingList.ParNo = dbo.VW_RealDepTrfPack.DCLParcelNo " & _
                          "WHERE (dbo.tblGrading_PackingList.PackNo = " & CDbl(txtPackNo.Text) & ") " & _
                          "GROUP BY dbo.tblGrading_PackingList.Department, dbo.tblGrading_PackingList.ParNo, dbo.VW_RealDepTrfPack.SupParcelNo, dbo.VW_RealDepTrfPack.CompCode, " & _
                                "dbo.VW_RealDepTrfPack.Category, dbo.VW_RealDepTrfPack.CompanyName", AdoCN, 1, 1)
        End If
        If optPCU.Checked = True Then
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblExpReExports.Department, dbo.tblExpReExports.ParNo, SUM(dbo.tblExpReExports.Pcs) AS Pcs, ROUND(SUM(dbo.tblExpReExports.Cts), 3) AS Cts, " & _
                                "ISNULL(dbo.VW_DCLImportsLotSupComp.CompanyName, dbo.VW_RealDepTrfPack.CompanyName) AS CompanyName, " & _
                                "ISNULL(dbo.VW_DCLImportsLotSupComp.CompCode, dbo.VW_RealDepTrfPack.CompCode) AS CompCode, ISNULL(dbo.VW_DCLImportsLotSupComp.Category, " & _
                                "dbo.VW_RealDepTrfPack.Category) AS Category " & _
                          "FROM dbo.VW_RealDepTrfPack RIGHT OUTER JOIN dbo.tblExpReExports ON dbo.VW_RealDepTrfPack.DCLParcelNo = dbo.tblExpReExports.ParNo LEFT OUTER JOIN " & _
                                "dbo.VW_DCLImportsLotSupComp RIGHT OUTER JOIN dbo.tblParcel ON dbo.VW_DCLImportsLotSupComp.SupParcelNo = dbo.tblParcel.OrigParcelNo ON dbo.tblExpReExports.Department = dbo.tblParcel.Depart AND " & _
                                "dbo.tblExpReExports.ParNo = dbo.tblParcel.GrpParNo " & _
                          "WHERE (dbo.tblExpReExports.PackNo = " & CDbl(txtPackNo.Text) & ") " & _
                          "GROUP BY dbo.tblExpReExports.Department, dbo.tblExpReExports.ParNo, dbo.VW_DCLImportsLotSupComp.CompanyName, dbo.VW_DCLImportsLotSupComp.CompCode, " & _
                                "dbo.VW_DCLImportsLotSupComp.Category, dbo.VW_RealDepTrfPack.CompCode, dbo.VW_RealDepTrfPack.Category, dbo.VW_RealDepTrfPack.CompanyName", AdoCN, 1, 1)
        End If
        If optPolRej.Checked = True Then
            rsComSql.Open("SELECT dbo.tblExpRejExports.Department, dbo.tblExpRejExports.ParNo, SUM(dbo.tblExpRejExports.Pcs) AS Pcs, ROUND(SUM(dbo.tblExpRejExports.Cts), 3) AS Cts, " & _
                                "dbo.VW_RealDepTrfPack.CompCode, dbo.VW_RealDepTrfPack.Category, dbo.VW_RealDepTrfPack.CompanyName " & _
                          "FROM dbo.tblExpRejExports INNER JOIN dbo.VW_RealDepTrfPack ON dbo.tblExpRejExports.ParNo = dbo.VW_RealDepTrfPack.DCLParcelNo " & _
                          "WHERE (dbo.tblExpRejExports.PackNo = " & CDbl(txtPackNo.Text) & ") " & _
                          "GROUP BY dbo.tblExpRejExports.Department, dbo.tblExpRejExports.ParNo, dbo.VW_RealDepTrfPack.CompCode, dbo.VW_RealDepTrfPack.Category, " & _
                                "dbo.VW_RealDepTrfPack.CompanyName", AdoCN, 1, 1)
        End If
        If optRghRej.Checked = True Then
            rsComSql.Open("SELECT dbo.tblParcel.Depart AS Department, dbo.tblParcelDetails.ParcelNo AS ParNo, SUM(dbo.tblParcelDetails.PktPcs) AS Pcs, ROUND(SUM(dbo.tblParcelDetails.PktCts), 3) AS Cts, " & _
                                "dbo.VW_DCLImportsLotSupComp.CompCode, dbo.VW_DCLImportsLotSupComp.Category, dbo.VW_DCLImportsLotSupComp.CompanyName " & _
                          "FROM dbo.VW_DCLImportsLotSupComp INNER JOIN dbo.tblParcel ON dbo.VW_DCLImportsLotSupComp.SupParcelNo = dbo.tblParcel.OrigParcelNo INNER JOIN " & _
                                "dbo.tblParcelDetails ON dbo.tblParcel.GrpParNo = dbo.tblParcelDetails.ParcelNo " & _
                          "WHERE (dbo.tblParcelDetails.PackNo = " & CDbl(txtPackNo.Text) & ") " & _
                          "GROUP BY dbo.tblParcel.Depart, dbo.tblParcelDetails.ParcelNo, dbo.VW_DCLImportsLotSupComp.CompCode, dbo.VW_DCLImportsLotSupComp.Category, " & _
                                "dbo.VW_DCLImportsLotSupComp.CompanyName", AdoCN, 1, 1)
        End If
        If optContract.Checked = True Then
            rsComSql.Open("SELECT dbo.tblParcel.Depart AS Department, dbo.tblParcelReturns.ParcelNo AS ParNo, SUM(dbo.tblParcelReturns.PktPcs) AS Pcs, ROUND(SUM(dbo.tblParcelReturns.PktCts), 3) AS Cts, " & _
                                "dbo.VW_DCLImportsLotSupComp.CompCode, dbo.VW_DCLImportsLotSupComp.Category, dbo.VW_DCLImportsLotSupComp.CompanyName " & _
                          "FROM dbo.VW_DCLImportsLotSupComp INNER JOIN dbo.tblParcel ON dbo.VW_DCLImportsLotSupComp.SupParcelNo = dbo.tblParcel.OrigParcelNo INNER JOIN " & _
                                "dbo.tblParcelReturns ON dbo.tblParcel.GrpParNo = dbo.tblParcelReturns.ParcelNo " & _
                          "WHERE (dbo.tblParcelReturns.PackNo = " & CDbl(txtPackNo.Text) & ") " & _
                          "GROUP BY dbo.tblParcel.Depart, dbo.tblParcelReturns.ParcelNo, dbo.VW_DCLImportsLotSupComp.CompCode, dbo.VW_DCLImportsLotSupComp.Category, " & _
                                "dbo.VW_DCLImportsLotSupComp.CompanyName", AdoCN, 1, 1)
        End If
        If optGMix.Checked = True Then
            rsComSql.Open("SELECT Department, ParNo, SUM(ActPcs) AS Pcs, ROUND(SUM(ActCts), 3) AS Cts, 'DCL' AS CompCode, 'NFE' AS Category, 'Niru Diamonds Israel (1987) Ltd' AS CompanyName " & _
                          "FROM dbo.tblGrading_PackingList " & _
                          "WHERE (PackNo = " & CDbl(txtPackNo.Text) & ") " & _
                          "GROUP BY ParNo, Department", AdoCN, 1, 1)
        End If
        If optSales.Checked = True Then
            rsComSql.Open("SELECT 'PolishBox' AS Department,'0' AS ParNo, SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts, CompCode, 'Purchased' AS Category, 'Niru Diamonds Israel (1987) Ltd' AS CompanyName " & _
                          "FROM dbo.tblPOLSales " & _
                          "WHERE (SalesNo = " & CDbl(txtPackNo.Text) & ") " & _
                          "GROUP BY CompCode", AdoCN, 1, 1)
        End If
        If optMix.Checked = True Then
            rsComSql.Open("SELECT 'Mix' AS Department,'0' AS ParNo, SUM(PackPcs) AS Pcs, ROUND(SUM(PackCts), 3) AS Cts, 'DCL' AS CompCode, 'Purchased' AS Category, 'Niru Diamonds Israel (1987) Ltd' AS CompanyName " & _
                          "FROM dbo.tblMixPackingList " & _
                          "WHERE (PackNo = " & CDbl(txtPackNo.Text) & ")", AdoCN, 1, 1)
        End If
        If optMixExport.Checked = True Then
            rsComSql.Open("SELECT 'MixExport' AS Department,'0' AS ParNo, SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts, 'DCL' AS CompCode, 'Purchased' AS Category, 'Niru Diamonds Israel (1987) Ltd' AS CompanyName " & _
                          "FROM dbo.tblAssortExportDetails " & _
                          "WHERE (ExpNo = " & CDbl(txtPackNo.Text) & ") AND (Export = 1 OR Export = 5)", AdoCN, 1, 1)
        End If
        If optPolBoxTrf.Checked = True Then
            rsComSql.Open("SELECT 'PolishBoxTrf' AS Department,'0' AS ParNo, SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts, CompCode, 'Purchased' AS Category, 'Niru Diamonds Israel (1987) Ltd' AS CompanyName " & _
                          "FROM dbo.tblPOLTransfer " & _
                          "WHERE (TransferNo = " & CDbl(txtPackNo.Text) & ") " & _
                          "GROUP BY CompCode", AdoCN, 1, 1)
        End If
        If optRounds.Checked = True Then
            rsComSql.Open("SELECT Department, ParNo, SUM(ActPcs) AS Pcs, ROUND(SUM(ActCts), 3) AS Cts, 'DCL' AS CompCode, 'NFE' AS Category, 'Niru Diamonds Israel (1987) Ltd' AS CompanyName " & _
                          "FROM dbo.tblGrading_PackingList " & _
                          "WHERE (PackNo = " & CDbl(txtPackNo.Text) & ") " & _
                          "GROUP BY ParNo, Department", AdoCN, 1, 1)
        End If
        If optRghSales.Checked = True Then
            rsComSql.Open("SELECT dbo.tblParcel.Depart AS Department, dbo.tblParcelRghSales.ParcelNo AS ParNo, SUM(dbo.tblParcelRghSales.PktPcs) AS Pcs, ROUND(SUM(dbo.tblParcelRghSales.PktCts), 3) AS Cts, " & _
                                "dbo.VW_DCLImportsLotSupComp.CompCode, dbo.VW_DCLImportsLotSupComp.Category, dbo.VW_DCLImportsLotSupComp.CompanyName " & _
                          "FROM dbo.VW_DCLImportsLotSupComp INNER JOIN dbo.tblParcel ON dbo.VW_DCLImportsLotSupComp.SupParcelNo = dbo.tblParcel.OrigParcelNo INNER JOIN " & _
                                "dbo.tblParcelRghSales ON dbo.tblParcel.GrpParNo = dbo.tblParcelRghSales.ParcelNo " & _
                          "WHERE (dbo.tblParcelRghSales.PackNo = " & CDbl(txtPackNo.Text) & ") " & _
                          "GROUP BY dbo.tblParcel.Depart, dbo.tblParcelRghSales.ParcelNo, dbo.VW_DCLImportsLotSupComp.CompCode, dbo.VW_DCLImportsLotSupComp.Category, " & _
                                "dbo.VW_DCLImportsLotSupComp.CompanyName", AdoCN, 1, 1)
        End If
        If optPrecision.Checked = True Then
            rsComSql.Open("SELECT 'Precision' AS Department,'0' AS ParNo, SUM(PackPcs) AS Pcs, ROUND(SUM(PackCts), 3) AS Cts, 'DCL' AS CompCode, 'Purchased' AS Category, 'Niru Diamonds Israel (1987) Ltd' AS CompanyName " & _
                          "FROM dbo.tblPCUPackingList " & _
                          "WHERE (PackNo = " & CDbl(txtPackNo.Text) & ")", AdoCN, 1, 1)
        End If
        If optKit.Checked = True Then
            rsComSql.Open("SELECT 'KIT Box' AS Department,dbo.tblKITOrders.NorderNo AS ParNo, SUM(dbo.tblKITOrdersDtls.Sets * dbo.tblKITOrdersDtls.PCs) AS Pcs, 0 AS Cts, 'DCL' AS CompCode, 'Purchased' AS Category, 'Niru Diamonds Israel (1987) Ltd' AS CompanyName " & _
                          "FROM dbo.tblKITOrders INNER JOIN dbo.tblKITOrdersDtls ON dbo.tblKITOrders.OrderNo = dbo.tblKITOrdersDtls.OrderNo " & _
                          "WHERE (dbo.tblKITOrders.NorderNo = " & CDbl(txtPackNo.Text) & ") " & _
                          "GROUP BY dbo.tblKITOrders.NorderNo", AdoCN, 1, 1)
        End If
        If optColombo.Checked = True Then
            If cmbCategory.Text = "" Then
                MsgBox("Invalid Category", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            GoTo ColomboGrading
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            strCompCode = rsComSql.Fields("CompCode").Value
            strCat = rsComSql.Fields("Category").Value
            strSupplier = rsComSql.Fields("CompanyName").Value

            If optPCU.Checked = True Then
                If strMainComp <> "" Then
                    If strMainComp <> strCompCode Then
                        MsgBox("Invalid Company", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If
            End If
            
            If strMainCat <> "" Then
                If strMainCat <> strCat Then
                    MsgBox("Invalid Category", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            If strMainSup <> "" Then
                If strMainSup <> strSupplier Then
                    PBResponse = MsgBox("Different Supplier. Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
                    If PBResponse = MsgBoxResult.Yes Then

                    Else
                        Exit Sub
                    End If
                End If
            End If

            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("Department").Value,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    Format(rsComSql.Fields("Cts").Value, "#0.000"),
                                    txtPackNo.Text,
                                    strType,
                                    rsComSql.Fields("CompCode").Value,
                                    rsComSql.Fields("Category").Value,
                                    rsComSql.Fields("CompanyName").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        GoTo LastCal

ColomboGrading:
        flxDetails.Rows.Add("Colombo Niru",
                            txtPackNo.Text,
                            "0",
                            Format("0", "#0.000"),
                            "0",
                            strType,
                            "DCL",
                            cmbCategory.Text,
                            "Niru Diamonds Israel (1987) Ltd")

LastCal:
        txtPackNo.Text = ""
        txtTotPcs.Text = CalTotalPcs(flxDetails)
        txtTotCts.Text = Format(CalTotalCts(flxDetails), "#0.000")

        txtPackNo.Focus()
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(2, intRow).Value)
        Next
    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(3, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub Save()
        Dim intRow As Integer
        Dim strType As String

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then

            If txtPackListNo.Text = "" Then MsgBox("Invalid Packing List No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If cmbDesc.Text = "" Then MsgBox("Invalid Description", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If cmbDeliver.Text = "" Then MsgBox("Invalid Delivery Info", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If cmbType.Text = "" Then MsgBox("Invalid Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If cmbSupplierCode.Text = "" Then MsgBox("Invalid Customer", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_Pack WHERE PackingListNo = " & CDbl(txtPackListNo.Text) & "", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                MsgBox("Packing List No. already exists. Please Refresh", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            strType = ""
            If optParcel.Checked = True Then
                strType = "P"
            End If
            If optOrder.Checked = True Then
                strType = "O"
            End If
            If optSorting.Checked = True Then
                strType = "S"
            End If
            If optPCU.Checked = True Then
                strType = "C"
            End If
            If optPolRej.Checked = True Then
                strType = "J"
            End If
            If optRghRej.Checked = True Then
                strType = "R"
            End If
            If optContract.Checked = True Then
                strType = "I"
            End If
            If optMix.Checked = True Then
                strType = "M"
            End If
            If optGMix.Checked = True Then
                strType = "G"
            End If
            If optSales.Checked = True Then
                strType = "L"
            End If
            If optMixExport.Checked = True Then
                strType = "E"
            End If
            If optPolBoxTrf.Checked = True Then
                strType = "B"
            End If
            If optRounds.Checked = True Then
                strType = "X"
            End If
            If optColombo.Checked = True Then
                strType = "Y"
            End If
            If optRghSales.Checked = True Then
                strType = "H"
            End If
            If optPrecision.Checked = True Then
                strType = "K"
            End If
            If optKit.Checked = True Then
                strType = "T"
            End If

            For intRow = 0 To flxDetails.Rows.Count - 1
                AdoCN.Execute("INSERT INTO tblGrading_Pack(Department,PackingListNo,PackNo,ParNo,Type,Description,Delivery,CompCode,Category,Supplier,PackType,CustCode,Client) " & _
                              "VALUES('" & flxDetails.Item(0, intRow).Value & "'," & CDbl(txtPackListNo.Text) & "," & CDbl(flxDetails.Item(4, intRow).Value) & "," & _
                                "'" & flxDetails.Item(1, intRow).Value & "','" & strType & "','" & cmbDesc.Text & "','" & cmbDeliver.Text & "','" & flxDetails.Item(6, intRow).Value & "'," & _
                                "'" & flxDetails.Item(7, intRow).Value & "','" & flxDetails.Item(8, intRow).Value & "','" & cmbType.Text & "','" & CInt(txtSupCode.Text) & "','" & cmbClient.Text & "')")
            Next

            MsgBox("Packing List Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

            ClearFields()

        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        AddPackage()
    End Sub

    Private Sub cmbSupplierCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSupplierCode.SelectedIndexChanged
        Dim rsSup As New ADODB.Recordset

        rsSup = New ADODB.Recordset
        rsSup.Open("SELECT SupplierCode FROM tblSuppliers " & _
                   "WHERE CompanyName = '" & cmbSupplierCode.Text & "'", AdoCN, 1, 1)
        If Not rsSup.EOF Then
            txtSupCode.Text = rsSup.Fields("SupplierCode").Value
        End If
        rsSup = Nothing
    End Sub

    Private Sub txtPackNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPackNo.KeyPress
        If optColombo.Checked = False Then
            e.Handled = IntegerOnly(Asc(e.KeyChar))
        End If
        If Asc(e.KeyChar) = 13 Then
            If txtPackNo.Text <> "" Then
                cmdAdd.Focus()
            End If
        End If
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub UpdatePack()
        PBResponse = MsgBox("Are you sure to Update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then

            If txtPackListNo.Text = "" Then MsgBox("Invalid Packing List No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If txtNewPackListNo.Text = "" Then MsgBox("Invalid New Packing List No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If cmbDesc.Text = "" Then MsgBox("Invalid Description", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If cmbDeliver.Text = "" Then MsgBox("Invalid Delivery Info", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If cmbType.Text = "" Then MsgBox("Invalid Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If cmbSupplierCode.Text = "" Then MsgBox("Invalid Customer", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If cmbCategory.Text = "" Then MsgBox("Invalid Category", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_Pack WHERE PackingListNo = " & CDbl(txtPackListNo.Text) & "", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                AdoCN.Execute("UPDATE tblGrading_Pack SET Delivery = '" & cmbDeliver.Text & "',PackType = '" & cmbType.Text & "',CustCode = '" & txtSupCode.Text & "'," & _
                                "Category = '" & cmbCategory.Text & "',Description = '" & cmbDesc.Text & "',PackingListNo = " & CDbl(txtNewPackListNo.Text) & ", Client = '" & cmbClient.Text & "' " & _
                              "WHERE PackingListNo = " & CDbl(txtPackListNo.Text) & "")

                MsgBox("Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
            Else
                MsgBox("New Packing List No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub cmdUpdate_Click(sender As Object, e As EventArgs) Handles cmdUpdate.Click
        UpdatePack()
    End Sub

    Private Sub txtNewPackListNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNewPackListNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub txtNewPackListNo_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub
End Class