
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDExportSummaryPCU
    Private Sub GetPackNo()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(PackNo) AS MaxNo FROM tblGrading_PackingListPCU", AdoCN, 1, 1)
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

    Private Sub frm_GRDExportSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        GetPackNo()

        txtAddPcs.Text = "0"
        txtAddCts.Text = "0"
        txtPcs.Text = "0"
        txtCts.Text = "0"
        txtSelPcs.Text = "0"
        txtSelCts.Text = "0"
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_ExportParcels()
        Dim dtLoading As New DataTable("Parcels")

        dtLoading.Columns.Add("Parcel", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("Order", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("Ref", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("Side", System.Type.GetType("System.String"))

        cmbParcel.Text = ""
        cmbParcel.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT OrderNo, RefNo, Side, ParNo " & _
                      "FROM dbo.VW_GradingPCUSizeFinishBal " & _
                      "GROUP BY OrderNo, RefNo, Side, ParNo " & _
                      "ORDER BY OrderNo, RefNo, Side, ParNo", AdoCN, 1, 1)

        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                Dim dr As DataRow
                dr = dtLoading.NewRow

                dr("Parcel") = rsComSql.Fields("ParNo").Value
                dr("Order") = rsComSql.Fields("OrderNo").Value
                dr("Ref") = rsComSql.Fields("RefNo").Value
                dr("Side") = rsComSql.Fields("Side").Value
                dtLoading.Rows.Add(dr)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        cmbParcel.SelectedIndex = -1
        cmbParcel.Items.Clear()
        cmbParcel.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        cmbParcel.SourceDataString = New String(3) {"Parcel", "Order", "Ref", "Side"}
        cmbParcel.SourceDataTable = dtLoading

    End Sub

    Private Sub Load_ExportOrders()
        Dim dtLoading As New DataTable("Orders")

        dtLoading.Columns.Add("Order", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("Ref", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("Side", System.Type.GetType("System.String"))

        cmbOrder.Text = ""
        cmbOrder.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT OrderNo, RefNo, Side " & _
                      "FROM dbo.VW_GradingPCUSizeFinishBal " & _
                      "GROUP BY OrderNo, RefNo, Side " & _
                      "ORDER BY OrderNo, RefNo, Side", AdoCN, 1, 1)

        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                Dim dr As DataRow
                dr = dtLoading.NewRow

                dr("Order") = rsComSql.Fields("OrderNo").Value
                dr("Ref") = rsComSql.Fields("RefNo").Value
                dr("Side") = rsComSql.Fields("Side").Value
                dtLoading.Rows.Add(dr)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        cmbOrder.SelectedIndex = -1
        cmbOrder.Items.Clear()
        cmbOrder.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        cmbOrder.SourceDataString = New String(2) {"Order", "Ref", "Side"}
        cmbOrder.SourceDataTable = dtLoading

    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        GetPackNo()
        Load_ExportParcels()
        Load_ExportOrders()
    End Sub

    Private Sub Load_ExportDetails(ByVal strParcel As String, ByVal strDepartment As String, ByVal strOrderNo As String, ByVal strRef As String, ByVal strSide As String)
        Dim intIssPcs As Integer
        Dim dblIssCts As Double
        Dim intRow As Integer

        Dim intRghIssPcs As Integer
        Dim dblRghIssCts As Double

        Dim strSupplier As String
        Dim strSupplierMain As String
        Dim strCategory As String
        Dim strCompany As String
        Dim strCategoryMain As String
        Dim strCompanyMain As String
        Dim strInvoice As String
        Dim strInvoiceMain As String
        Dim strCountry As String
        Dim strCountryMain As String
        Dim strParcelType As String
        Dim strParcelTypeMain As String

        For intRow = 0 To flxDetails.Rows.Count - 1
            If strParcel = flxDetails.Item(1, intRow).Value And strOrderNo = flxDetails.Item(11, intRow).Value And _
                strRef = flxDetails.Item(12, intRow).Value And strSide = flxDetails.Item(13, intRow).Value Then
                MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        strSupplierMain = ""
        strCategoryMain = ""
        strInvoiceMain = ""
        strCompanyMain = ""
        strCountryMain = ""
        strParcelTypeMain = ""
        If flxDetails.Rows.Count > 0 Then
            If flxDetails.Item(12, 0).Value <> "" Then
                If strRef <> flxDetails.Item(12, 0).Value Then
                    MsgBox("Invalid Ref", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            If flxDetails.Item(13, 0).Value <> "" Then
                If strSide <> flxDetails.Item(13, 0).Value Then
                    MsgBox("Invalid Side", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            strSupplierMain = flxDetails.Item(16, 0).Value
            strCategoryMain = flxDetails.Item(17, 0).Value
            strInvoiceMain = flxDetails.Item(18, 0).Value
            strCompanyMain = flxDetails.Item(19, 0).Value
            strCountryMain = flxDetails.Item(20, 0).Value
            strParcelTypeMain = flxDetails.Item(21, 0).Value
        End If

        intIssPcs = txtAddPcs.Text
        dblIssCts = txtAddCts.Text

        intRghIssPcs = 0
        dblRghIssCts = 0
        rsComSql = New ADODB.Recordset
        If strDepartment = "GradingPCU_N" Then
            'rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblGrading_SizingTypes.Department, dbo.tblGrading_SizingTypes.ReturnType, dbo.tblGrading_SizingTypes.ParNo,dbo.tblGrading_SizingTypes.PktNo, " & _
            '                    "SUM(dbo.tblGrading_SizingTypes.Pcs) AS Pcs, ROUND(SUM(dbo.tblGrading_SizingTypes.Cts), 3) AS Cts, dbo.tblGrading_SizingList.PRICE, " & _
            '                    "dbo.tblGrading_SizingPacket.OrderNo AS OrderNo, dbo.tblGrading_SizingPacket.RefNo, dbo.tblGrading_SizingPacket.Side, dbo.tblGrading_SizingPacket.GiaNo, dbo.tblGrading_SizingPacket.RateCode " & _
            '              "FROM dbo.tblGrading_SizingTypes INNER JOIN dbo.tblGrading_SizingList ON dbo.tblGrading_SizingTypes.ReturnType = dbo.tblGrading_SizingList.NAME INNER JOIN " & _
            '                    "dbo.tblGrading_SizingPacket ON dbo.tblGrading_SizingTypes.Department = dbo.tblGrading_SizingPacket.Department AND " & _
            '                    "dbo.tblGrading_SizingTypes.ParNo = dbo.tblGrading_SizingPacket.ParNo And dbo.tblGrading_SizingTypes.PktNo = dbo.tblGrading_SizingPacket.PktNo " & _
            '              "WHERE (dbo.tblGrading_SizingTypes.ParNo = '" & strParcel & "') AND (dbo.tblGrading_SizingTypes.OK = 0) AND (dbo.tblGrading_SizingPacket.OrderNo = '" & strOrderNo & "') AND " & _
            '                    "(dbo.tblGrading_SizingPacket.RefNo = '" & strRef & "') AND (dbo.tblGrading_SizingPacket.Side = '" & strSide & "') " & _
            '              "GROUP BY dbo.tblGrading_SizingTypes.Department, dbo.tblGrading_SizingTypes.ReturnType, dbo.tblGrading_SizingList.PRICE, dbo.tblGrading_SizingTypes.ParNo,dbo.tblGrading_SizingTypes.PktNo, " & _
            '                    "dbo.tblGrading_SizingPacket.OrderNo, dbo.tblGrading_SizingPacket.RefNo, dbo.tblGrading_SizingPacket.Side, dbo.tblGrading_SizingPacket.GiaNo, dbo.tblGrading_SizingPacket.RateCode " & _
            '              "ORDER BY dbo.tblGrading_SizingTypes.ReturnType, dbo.tblGrading_SizingTypes.PktNo", AdoCN, 1, 1)

            rsComSql.Open("SELECT TOP (100) PERCENT dbo.VW_GradingPCUSizeFinishBal.Department, dbo.VW_GradingPCUSizeFinishBal.OrderNo, dbo.VW_GradingPCUSizeFinishBal.RefNo, " & _
                                "dbo.VW_GradingPCUSizeFinishBal.Side, dbo.VW_GradingPCUSizeFinishBal.ParNo, dbo.VW_GradingPCUSizeFinishBal.PktNo, " & _
                                "dbo.VW_GradingPCUSizeFinishBal.ReturnType, dbo.VW_GradingPCUSizeFinishBal.BalPcs AS Pcs, dbo.VW_GradingPCUSizeFinishBal.BalCts AS Cts, " & _
                                "dbo.tblGrading_SizingList.PRICE, dbo.VW_GradingPCUSizeFinishBal.GiaNo, dbo.VW_GradingPCUSizeFinishBal.RateCode " & _
                          "FROM dbo.VW_GradingPCUSizeFinishBal INNER JOIN dbo.tblGrading_SizingList ON dbo.VW_GradingPCUSizeFinishBal.ReturnType = dbo.tblGrading_SizingList.NAME " & _
                          "WHERE (dbo.VW_GradingPCUSizeFinishBal.OrderNo = " & strOrderNo & ") AND (dbo.VW_GradingPCUSizeFinishBal.RefNo = '" & strRef & "') AND " & _
                                "(dbo.VW_GradingPCUSizeFinishBal.ParNo = '" & strParcel & "') AND (dbo.VW_GradingPCUSizeFinishBal.Side = '" & strSide & "') " & _
                          "ORDER BY dbo.VW_GradingPCUSizeFinishBal.ReturnType, dbo.VW_GradingPCUSizeFinishBal.PktNo", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                intRghIssPcs = 0
                dblRghIssCts = 0
                'rsComSql_1 = New ADODB.Recordset
                'rsComSql_1.Open("SELECT SUM(IssPcs) AS IssPcs, ROUND(SUM(IssCts), 3) AS IssCts " & _
                '                "FROM tblGrading_RghIssues " & _
                '                "WHERE (Export = 0) AND (Department = '" & rsComSql.Fields("Department").Value & "') AND " & _
                '                    "(ParNo = '" & rsComSql.Fields("ParNo").Value & "') AND (PktNo = '" & rsComSql.Fields("PktNo").Value & "') AND (Assortment = '" & rsComSql.Fields("ReturnType").Value & "')", AdoCN, 1, 1)
                'If rsComSql_1.RecordCount Then
                '    If Not IsDBNull(rsComSql_1.Fields("IssPcs").Value) Then
                '        intRghIssPcs = rsComSql_1.Fields("IssPcs").Value
                '        dblRghIssCts = rsComSql_1.Fields("IssCts").Value
                '    End If
                'End If
                'rsComSql_1 = Nothing

                strSupplier = "Niru Diamonds Israel (1987) Ltd"
                strInvoice = ""
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT dbo.tblDep_Trf.Department, dbo.tblDep_Trf.DCLParcelNo, dbo.tblSuppliers.CompanyName, dbo.tblDep_Trf.SupplierRefNo " & _
                                "FROM dbo.tblDep_Trf INNER JOIN dbo.tblSuppliers ON dbo.tblDep_Trf.SupplierCode = dbo.tblSuppliers.SupplierCode " & _
                                "WHERE (dbo.tblDep_Trf.Department = 'Precision') AND (dbo.tblDep_Trf.DCLParcelNo = '" & strParcel & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strSupplier = rsComSql_1.Fields("CompanyName").Value
                    strInvoice = rsComSql_1.Fields("SupplierRefNo").Value
                End If
                rsComSql_1 = Nothing

                If strInvoice = "" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT dbo.tblDep_Trf.Department, dbo.tblDep_Trf.DCLParcelNo, dbo.tblSuppliers.CompanyName, dbo.tblDep_Trf.SupplierRefNo " & _
                                    "FROM dbo.tblDep_Trf INNER JOIN dbo.tblSuppliers ON dbo.tblDep_Trf.SupplierCode = dbo.tblSuppliers.SupplierCode " & _
                                    "WHERE (dbo.tblDep_Trf.DCLParcelNo = '" & Mid(strParcel, 1, 6) & "')", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        strSupplier = rsComSql_1.Fields("CompanyName").Value
                        strInvoice = rsComSql_1.Fields("SupplierRefNo").Value
                    End If
                    rsComSql_1 = Nothing
                End If

                If strInvoice = "" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT dbo.tblDep_Trf.Department, dbo.tblDep_Trf.DCLParcelNo, dbo.tblSuppliers.CompanyName, dbo.tblDep_Trf.SupplierRefNo " & _
                                    "FROM dbo.tblDep_Trf INNER JOIN dbo.tblSuppliers ON dbo.tblDep_Trf.SupplierCode = dbo.tblSuppliers.SupplierCode " & _
                                    "WHERE (dbo.tblDep_Trf.DCLParcelNo = '" & strParcel & "')", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        strSupplier = rsComSql_1.Fields("CompanyName").Value
                        strInvoice = rsComSql_1.Fields("SupplierRefNo").Value
                    End If
                    rsComSql_1 = Nothing
                End If

                strCategory = ""
                strCompany = "DCL"
                strParcelType = ""
                strCountry = ""

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT Category, CompCode, ParcelType " & _
                                "FROM dbo.tblImport " & _
                                "WHERE (DCLParcelNo = '" & strParcel & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strCategory = rsComSql_1.Fields("Category").Value
                    strCompany = rsComSql_1.Fields("CompCode").Value
                    strParcelType = rsComSql_1.Fields("ParcelType").Value
                End If
                rsComSql_1 = Nothing

                If strParcelType = "" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT Category, CompCode, ParcelType " & _
                                    "FROM dbo.tblImport " & _
                                    "WHERE (SupplierRefNo = '" & strInvoice & "') AND (SupplierRefNo <> '0') ORDER BY InvoiceDate", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        strCategory = rsComSql_1.Fields("Category").Value
                        strCompany = rsComSql_1.Fields("CompCode").Value
                        strParcelType = rsComSql_1.Fields("ParcelType").Value
                    End If
                    rsComSql_1 = Nothing
                End If

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * " & _
                                "FROM dbo.tblPOLStockOut " & _
                                "WHERE (DocID = '" & strParcel & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If strDBName = "DiaStock" Then
                        strCategory = "NFE"
                    Else
                        strCategory = "Purchased"
                    End If
                    strParcelType = "Polished"
                End If
                rsComSql_1 = Nothing

                If strCategoryMain <> "" Then
                    If strCategoryMain <> strCategory Then
                        MsgBox("Invalid Category", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If

                'If strCompanyMain <> "" Then
                '    If strCompanyMain <> strCompany Then
                '        MsgBox("Invalid Company", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                '        Exit Sub
                '    End If
                'End If

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT Country " & _
                                "FROM dbo.tblNoneOrders " & _
                                "WHERE (OrderNo = '" & strOrderNo & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strCountry = rsComSql_1.Fields("Country").Value
                End If
                rsComSql_1 = Nothing

                If strCountryMain <> "" Then
                    If strCountryMain <> strCountry Then
                        MsgBox("Invalid Country", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If

                'If strParcelTypeMain <> "" Then
                '    If strParcelTypeMain <> strParcelType Then
                '        MsgBox("Invalid Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                '        Exit Sub
                '    End If
                'End If

                If strCategory = "NFE" Then
                    If strSupplierMain <> "" Then
                        If strSupplierMain <> strSupplier Then
                            MsgBox("Invalid Supplier", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                    End If
                End If

                If rsComSql.Fields("Pcs").Value - intRghIssPcs > 0 Then
                    flxDetails.Rows.Add(rsComSql.Fields("ReturnType").Value,
                                        rsComSql.Fields("ParNo").Value,
                                        rsComSql.Fields("Pcs").Value - intRghIssPcs,
                                        Format(Math.Round(rsComSql.Fields("Cts").Value - dblRghIssCts, 3), "#0.000"),
                                        rsComSql.Fields("Pcs").Value - intRghIssPcs,
                                        Format(Math.Round(rsComSql.Fields("Cts").Value - dblRghIssCts, 3), "#0.000"),
                                        "0",
                                        Format(rsComSql.Fields("Price").Value, "#0.00"),
                                        Format(rsComSql.Fields("Price").Value * (rsComSql.Fields("Cts").Value - dblRghIssCts), "#0.00"),
                                        rsComSql.Fields("Department").Value,
                                        "",
                                        rsComSql.Fields("OrderNo").Value,
                                        rsComSql.Fields("RefNo").Value,
                                        rsComSql.Fields("Side").Value,
                                        rsComSql.Fields("PktNo").Value,
                                        False,
                                        strSupplier,
                                        strCategory,
                                        strInvoice,
                                        strCompany,
                                        strCountry,
                                        strParcelType,
                                        rsComSql.Fields("GiaNo").Value,
                                        rsComSql.Fields("RateCode").Value)
                End If

                intIssPcs = intIssPcs + rsComSql.Fields("Pcs").Value - intRghIssPcs
                dblIssCts = dblIssCts + rsComSql.Fields("Cts").Value - dblRghIssCts
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        dblIssCts = Math.Round(dblIssCts, 3)

        txtAddPcs.Text = intIssPcs
        txtPcs.Text = intIssPcs
        txtAddCts.Text = Format(dblIssCts, "#0.000")
        txtCts.Text = Format(dblIssCts, "#0.000")

        'flxDetails.Focus()

    End Sub

    Private Sub cmbParcel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbParcel.SelectedIndexChanged
        If Not cmbParcel.SelectedItem Is Nothing Then
            Load_ExportDetails(cmbParcel.SelectedItem.Col1, "GradingPCU_N", cmbParcel.SelectedItem.Col2, cmbParcel.SelectedItem.Col3, cmbParcel.SelectedItem.Col4)
        End If
    End Sub

    Private Sub ClearFields()
        GetPackNo()
        txtAddPcs.Text = "0"
        txtAddCts.Text = "0"
        txtPcs.Text = "0"
        txtCts.Text = "0"
        txtSelPcs.Text = "0"
        txtSelCts.Text = "0"
        flxDetails.Rows.Clear()
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        If txtPackNo.Text = "" Then MsgBox("Invalid Package No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If flxDetails.Rows.Count < 1 Then MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblGrading_PackingListPCU WHERE PackNo = " & Val(txtPackNo.Text) & "", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                For intRow = 0 To flxDetails.Rows.Count - 1

                Next

                For intRow = 0 To flxDetails.Rows.Count - 1
                    AdoCN.Execute("UPDATE tblGrading_PackingList SET Pcs = " & CDbl(flxDetails.Item(2, intRow).Value) & ", Cts = " & CDbl(flxDetails.Item(3, intRow).Value) & ", " & _
                                    "ActPcs = " & CDbl(flxDetails.Item(4, intRow).Value) & ", ActCts = " & CDbl(flxDetails.Item(5, intRow).Value) & " " & _
                                  "WHERE ID = " & CDbl(flxDetails.Item(10, intRow).Value) & "")
                Next

                MsgBox("Updated Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(15, intRow).Value = True Then
                    AdoCN.Execute("INSERT INTO tblGrading_PackingListPCU(Department,PackNo,Assortment,ParNo,Pcs,Cts,ActPcs,ActCts,Price,OrderNo,RefNo,Side,PktNo,Supplier,Category,InvoiceNo,Country,Company,Type,GiaNo,RateCode) " & _
                                  "VALUES('" & flxDetails.Item(9, intRow).Value & "'," & CDbl(txtPackNo.Text) & ",'" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & CInt(flxDetails.Item(2, intRow).Value) & "," & _
                                        "" & CDbl(flxDetails.Item(3, intRow).Value) & "," & CInt(flxDetails.Item(4, intRow).Value) & "," & CDbl(flxDetails.Item(5, intRow).Value) & "," & _
                                        "" & CDbl(flxDetails.Item(7, intRow).Value) & ",'" & flxDetails.Item(11, intRow).Value & "','" & flxDetails.Item(12, intRow).Value & "','" & flxDetails.Item(13, intRow).Value & "'," & _
                                        "'" & flxDetails.Item(14, intRow).Value & "','" & flxDetails.Item(16, intRow).Value & "','" & flxDetails.Item(17, intRow).Value & "','" & flxDetails.Item(18, intRow).Value & "','" & flxDetails.Item(20, intRow).Value & "'," & _
                                        "'" & flxDetails.Item(19, intRow).Value & "','" & flxDetails.Item(21, intRow).Value & "','" & flxDetails.Item(22, intRow).Value & "','" & flxDetails.Item(23, intRow).Value & "')")

                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.tblGrading_SizingTypes.ID " & _
                                    "FROM dbo.tblGrading_SizingTypes INNER JOIN dbo.tblGrading_SizingPacket ON dbo.tblGrading_SizingTypes.Department = dbo.tblGrading_SizingPacket.Department AND " & _
                                        "dbo.tblGrading_SizingTypes.ParNo = dbo.tblGrading_SizingPacket.ParNo And dbo.tblGrading_SizingTypes.PktNo = dbo.tblGrading_SizingPacket.PktNo " & _
                                    "WHERE (dbo.tblGrading_SizingTypes.OK = 0) AND (dbo.tblGrading_SizingTypes.Department = '" & flxDetails.Item(9, intRow).Value & "') AND (dbo.tblGrading_SizingTypes.ParNo = '" & flxDetails.Item(1, intRow).Value & "') " & _
                                        "AND (dbo.tblGrading_SizingPacket.OrderNo = '" & flxDetails.Item(11, intRow).Value & "') AND (dbo.tblGrading_SizingTypes.ReturnType = '" & flxDetails.Item(0, intRow).Value & "') " & _
                                        "AND (dbo.tblGrading_SizingTypes.PktNo = '" & flxDetails.Item(14, intRow).Value & "') " & _
                                    "ORDER BY dbo.tblGrading_SizingTypes.ID", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        rsComSql_1.MoveFirst()
                        While Not rsComSql_1.EOF
                            AdoCN.Execute("UPDATE tblGrading_SizingTypes SET OK = 1 WHERE ID  = '" & rsComSql_1.Fields("ID").Value & "' AND OK = 0")

                            rsComSql_1.MoveNext()
                        End While
                    End If
                    rsComSql_1 = Nothing

                    AdoCN.Execute("UPDATE tblGrading_RghIssues SET Export = 1 WHERE Department  = '" & flxDetails.Item(9, intRow).Value & "' AND ParNo = '" & flxDetails.Item(1, intRow).Value & "' AND Export = 0")
                End If
            Next

            MsgBox("Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing

        flxDetails.Rows.Clear()
        txtAddPcs.Text = "0"
        txtAddCts.Text = "0"
        txtPcs.Text = "0"
        txtCts.Text = "0"
        GetPackNo()
        cmbParcel.Text = ""
        cmbParcel.Items.Clear()
        Load_ExportParcels()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub Load_PackingList()
        Dim intIssPcs As Integer
        Dim dblIssCts As Double
        Dim intActPcs As Integer
        Dim dblActCts As Double

        flxDetails.Rows.Clear()
        intIssPcs = 0
        dblIssCts = 0
        intActPcs = 0
        dblActCts = 0
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * " & _
                      "FROM dbo.tblGrading_PackingListPCU " & _
                      "WHERE (PackNo = " & Val(txtPackNo.Text) & ") " & _
                      "ORDER BY Assortment", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("Assortment").Value,
                                   rsComSql.Fields("ParNo").Value,
                                   rsComSql.Fields("Pcs").Value,
                                   Format(rsComSql.Fields("Cts").Value, "#0.000"),
                                   rsComSql.Fields("ActPcs").Value,
                                   Format(rsComSql.Fields("ActCts").Value, "#0.000"),
                                   Math.Round(rsComSql.Fields("Cts").Value - rsComSql.Fields("ActCts").Value, 3),
                                   Format(rsComSql.Fields("Price").Value, "#0.00"),
                                   Format(rsComSql.Fields("Price").Value * rsComSql.Fields("ActCts").Value, "#0.00"),
                                   rsComSql.Fields("Department").Value,
                                   rsComSql.Fields("ID").Value,
                                   rsComSql.Fields("OrderNo").Value,
                                   rsComSql.Fields("RefNo").Value,
                                   rsComSql.Fields("Side").Value,
                                   rsComSql.Fields("PktNo").Value, True,
                                   rsComSql.Fields("Supplier").Value,
                                   rsComSql.Fields("Category").Value,
                                   rsComSql.Fields("InvoiceNo").Value,
                                   rsComSql.Fields("Company").Value,
                                   rsComSql.Fields("Country").Value,
                                   rsComSql.Fields("Type").Value,
                                   rsComSql.Fields("GiaNo").Value,
                                   rsComSql.Fields("RateCode").Value)

                intIssPcs = intIssPcs + rsComSql.Fields("Pcs").Value
                intActPcs = intActPcs + rsComSql.Fields("ActPcs").Value
                dblIssCts = dblIssCts + rsComSql.Fields("Cts").Value
                dblActCts = dblActCts + rsComSql.Fields("ActCts").Value
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        dblIssCts = Math.Round(dblIssCts, 3)
        dblActCts = Math.Round(dblActCts, 3)

        txtAddPcs.Text = intIssPcs
        txtPcs.Text = intActPcs
        txtAddCts.Text = Format(dblIssCts, "#0.000")
        txtCts.Text = Format(dblActCts, "#0.000")

        flxDetails.Focus()
    End Sub

    Private Sub txtPackNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPackNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            Load_PackingList()
        End If
    End Sub

    Private Function CalSelectPcs() As Double
        Dim intRow As Integer

        CalSelectPcs = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(15, intRow).Value = True Or flxDetails.Item(15, intRow).Value = 1 Then
                CalSelectPcs = CalSelectPcs + Val(flxDetails.Item(2, intRow).Value)
            End If
        Next

    End Function

    Private Function CalSelectCts() As Double
        Dim intRow As Integer

        CalSelectCts = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(15, intRow).Value = True Or flxDetails.Item(15, intRow).Value = 1 Then
                CalSelectCts = CalSelectCts + Val(flxDetails.Item(3, intRow).Value)
            End If
        Next
        CalSelectCts = Math.Round(CalSelectCts, 3)

    End Function

    Private Sub flxDetails_Click(sender As Object, e As EventArgs) Handles flxDetails.Click
        If flxDetails.CurrentCell.ColumnIndex = 15 Then
            txtSelPcs.Text = CalSelectPcs()
            txtSelCts.Text = CalSelectCts()
        End If
    End Sub

    Private Sub Load_ExportDetailsOrder(ByVal strDepartment As String, ByVal strOrderNo As String, ByVal strRef As String, ByVal strSide As String)
        Dim intIssPcs As Integer
        Dim dblIssCts As Double
        'Dim intRow As Integer

        Dim intRghIssPcs As Integer
        Dim dblRghIssCts As Double

        Dim strSupplier As String
        Dim strSupplierMain As String
        Dim strCategory As String
        Dim strCompany As String
        Dim strCategoryMain As String
        Dim strCompanyMain As String
        Dim strInvoice As String
        Dim strInvoiceMain As String
        Dim strCountry As String
        Dim strCountryMain As String
        Dim strParcelType As String
        Dim strParcelTypeMain As String

        Dim strParcel As String

        'For intRow = 0 To flxDetails.Rows.Count - 1
        '    If strParcel = flxDetails.Item(1, intRow).Value And strOrderNo = flxDetails.Item(11, intRow).Value And _
        '        strRef = flxDetails.Item(12, intRow).Value And strSide = flxDetails.Item(13, intRow).Value Then
        '        MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '        Exit Sub
        '    End If
        'Next

        strSupplierMain = ""
        strCategoryMain = ""
        strInvoiceMain = ""
        strCompanyMain = ""
        strCountryMain = ""
        strParcelTypeMain = ""

        strParcel = ""

        If flxDetails.Rows.Count > 0 Then
            If flxDetails.Item(12, 0).Value <> "" Then
                'If strRef <> flxDetails.Item(12, 0).Value Then
                '    MsgBox("Invalid Ref", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                '    Exit Sub
                'End If
            End If

            If flxDetails.Item(13, 0).Value <> "" Then
                'If strSide <> flxDetails.Item(13, 0).Value Then
                '    MsgBox("Invalid Side", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                '    Exit Sub
                'End If
            End If

            strSupplierMain = flxDetails.Item(16, 0).Value
            strCategoryMain = flxDetails.Item(17, 0).Value
            strInvoiceMain = flxDetails.Item(18, 0).Value
            strCompanyMain = flxDetails.Item(19, 0).Value
            strCountryMain = flxDetails.Item(20, 0).Value
            strParcelTypeMain = flxDetails.Item(21, 0).Value
        End If

        intIssPcs = 0
        dblIssCts = 0
        flxDetails.Rows.Clear()

        intRghIssPcs = 0
        dblRghIssCts = 0
        rsComSql = New ADODB.Recordset
        If strDepartment = "GradingPCU_N" Then
            'rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblGrading_SizingTypes.Department, dbo.tblGrading_SizingTypes.ReturnType, dbo.tblGrading_SizingTypes.ParNo,dbo.tblGrading_SizingTypes.PktNo, " & _
            '                    "SUM(dbo.tblGrading_SizingTypes.Pcs) AS Pcs, ROUND(SUM(dbo.tblGrading_SizingTypes.Cts), 3) AS Cts, dbo.tblGrading_SizingList.PRICE, " & _
            '                    "dbo.tblGrading_SizingPacket.OrderNo AS OrderNo, dbo.tblGrading_SizingPacket.RefNo, dbo.tblGrading_SizingPacket.Side, dbo.tblGrading_SizingPacket.GiaNo, dbo.tblGrading_SizingPacket.RateCode " & _
            '              "FROM dbo.tblGrading_SizingTypes INNER JOIN dbo.tblGrading_SizingList ON dbo.tblGrading_SizingTypes.ReturnType = dbo.tblGrading_SizingList.NAME INNER JOIN " & _
            '                    "dbo.tblGrading_SizingPacket ON dbo.tblGrading_SizingTypes.Department = dbo.tblGrading_SizingPacket.Department AND " & _
            '                    "dbo.tblGrading_SizingTypes.ParNo = dbo.tblGrading_SizingPacket.ParNo And dbo.tblGrading_SizingTypes.PktNo = dbo.tblGrading_SizingPacket.PktNo " & _
            '              "WHERE (dbo.tblGrading_SizingTypes.ParNo = '" & strParcel & "') AND (dbo.tblGrading_SizingTypes.OK = 0) AND (dbo.tblGrading_SizingPacket.OrderNo = '" & strOrderNo & "') AND " & _
            '                    "(dbo.tblGrading_SizingPacket.RefNo = '" & strRef & "') AND (dbo.tblGrading_SizingPacket.Side = '" & strSide & "') " & _
            '              "GROUP BY dbo.tblGrading_SizingTypes.Department, dbo.tblGrading_SizingTypes.ReturnType, dbo.tblGrading_SizingList.PRICE, dbo.tblGrading_SizingTypes.ParNo,dbo.tblGrading_SizingTypes.PktNo, " & _
            '                    "dbo.tblGrading_SizingPacket.OrderNo, dbo.tblGrading_SizingPacket.RefNo, dbo.tblGrading_SizingPacket.Side, dbo.tblGrading_SizingPacket.GiaNo, dbo.tblGrading_SizingPacket.RateCode " & _
            '              "ORDER BY dbo.tblGrading_SizingTypes.ReturnType, dbo.tblGrading_SizingTypes.PktNo", AdoCN, 1, 1)

            rsComSql.Open("SELECT TOP (100) PERCENT dbo.VW_GradingPCUSizeFinishBal.Department, dbo.VW_GradingPCUSizeFinishBal.OrderNo, dbo.VW_GradingPCUSizeFinishBal.RefNo, " & _
                                "dbo.VW_GradingPCUSizeFinishBal.Side, dbo.VW_GradingPCUSizeFinishBal.ParNo, dbo.VW_GradingPCUSizeFinishBal.PktNo, " & _
                                "dbo.VW_GradingPCUSizeFinishBal.ReturnType, dbo.VW_GradingPCUSizeFinishBal.BalPcs AS Pcs, dbo.VW_GradingPCUSizeFinishBal.BalCts AS Cts, " & _
                                "dbo.tblGrading_SizingList.PRICE, dbo.VW_GradingPCUSizeFinishBal.GiaNo, dbo.VW_GradingPCUSizeFinishBal.RateCode " & _
                          "FROM dbo.VW_GradingPCUSizeFinishBal INNER JOIN dbo.tblGrading_SizingList ON dbo.VW_GradingPCUSizeFinishBal.ReturnType = dbo.tblGrading_SizingList.NAME " & _
                          "WHERE (dbo.VW_GradingPCUSizeFinishBal.OrderNo = " & strOrderNo & ") AND (dbo.VW_GradingPCUSizeFinishBal.RefNo = '" & strRef & "') AND (dbo.VW_GradingPCUSizeFinishBal.Side = '" & strSide & "') " & _
                          "ORDER BY dbo.VW_GradingPCUSizeFinishBal.ReturnType, dbo.VW_GradingPCUSizeFinishBal.PktNo", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                intRghIssPcs = 0
                dblRghIssCts = 0
                'rsComSql_1 = New ADODB.Recordset
                'rsComSql_1.Open("SELECT SUM(IssPcs) AS IssPcs, ROUND(SUM(IssCts), 3) AS IssCts " & _
                '                "FROM tblGrading_RghIssues " & _
                '                "WHERE (Export = 0) AND (Department = '" & rsComSql.Fields("Department").Value & "') AND " & _
                '                    "(ParNo = '" & rsComSql.Fields("ParNo").Value & "') AND (PktNo = '" & rsComSql.Fields("PktNo").Value & "') AND (Assortment = '" & rsComSql.Fields("ReturnType").Value & "')", AdoCN, 1, 1)
                'If rsComSql_1.RecordCount Then
                '    If Not IsDBNull(rsComSql_1.Fields("IssPcs").Value) Then
                '        intRghIssPcs = rsComSql_1.Fields("IssPcs").Value
                '        dblRghIssCts = rsComSql_1.Fields("IssCts").Value
                '    End If
                'End If
                'rsComSql_1 = Nothing
                strInvoice = ""

                strSupplier = "Niru Diamonds Israel (1987) Ltd"
                strParcel = rsComSql.Fields("ParNo").Value

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT dbo.tblDep_Trf.Department, dbo.tblDep_Trf.DCLParcelNo, dbo.tblSuppliers.CompanyName, dbo.tblDep_Trf.SupplierRefNo " & _
                                "FROM dbo.tblDep_Trf INNER JOIN dbo.tblSuppliers ON dbo.tblDep_Trf.SupplierCode = dbo.tblSuppliers.SupplierCode " & _
                                "WHERE (dbo.tblDep_Trf.Department = 'Precision') AND (dbo.tblDep_Trf.DCLParcelNo = '" & strParcel & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strSupplier = rsComSql_1.Fields("CompanyName").Value
                    strInvoice = rsComSql_1.Fields("SupplierRefNo").Value
                End If
                rsComSql_1 = Nothing

                If strInvoice = "" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT dbo.tblDep_Trf.Department, dbo.tblDep_Trf.DCLParcelNo, dbo.tblSuppliers.CompanyName, dbo.tblDep_Trf.SupplierRefNo " & _
                                    "FROM dbo.tblDep_Trf INNER JOIN dbo.tblSuppliers ON dbo.tblDep_Trf.SupplierCode = dbo.tblSuppliers.SupplierCode " & _
                                    "WHERE (dbo.tblDep_Trf.DCLParcelNo = '" & Mid(strParcel, 1, 6) & "')", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        strSupplier = rsComSql_1.Fields("CompanyName").Value
                        strInvoice = rsComSql_1.Fields("SupplierRefNo").Value
                    End If
                    rsComSql_1 = Nothing
                End If

                If strInvoice = "" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT dbo.tblDep_Trf.Department, dbo.tblDep_Trf.DCLParcelNo, dbo.tblSuppliers.CompanyName, dbo.tblDep_Trf.SupplierRefNo " & _
                                    "FROM dbo.tblDep_Trf INNER JOIN dbo.tblSuppliers ON dbo.tblDep_Trf.SupplierCode = dbo.tblSuppliers.SupplierCode " & _
                                    "WHERE (dbo.tblDep_Trf.DCLParcelNo = '" & strParcel & "')", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        strSupplier = rsComSql_1.Fields("CompanyName").Value
                        strInvoice = rsComSql_1.Fields("SupplierRefNo").Value
                    End If
                    rsComSql_1 = Nothing
                End If

                strCategory = ""
                strCompany = "DCL"
                strParcelType = ""
                strCountry = ""

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT Category, CompCode, ParcelType " & _
                                "FROM dbo.tblImport " & _
                                "WHERE (DCLParcelNo = '" & strParcel & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strCategory = rsComSql_1.Fields("Category").Value
                    strCompany = rsComSql_1.Fields("CompCode").Value
                    strParcelType = rsComSql_1.Fields("ParcelType").Value
                End If
                rsComSql_1 = Nothing

                If strParcelType = "" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT Category, CompCode, ParcelType " & _
                                    "FROM dbo.tblImport " & _
                                    "WHERE (SupplierRefNo = '" & strInvoice & "') AND (SupplierRefNo <> '0') ORDER BY InvoiceDate", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        strCategory = rsComSql_1.Fields("Category").Value
                        strCompany = rsComSql_1.Fields("CompCode").Value
                        strParcelType = rsComSql_1.Fields("ParcelType").Value
                    End If
                    rsComSql_1 = Nothing
                End If

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * " & _
                                "FROM dbo.tblPOLStockOut " & _
                                "WHERE (DocID = '" & strParcel & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If strDBName = "DiaStock" Then
                        strCategory = "NFE"
                    Else
                        strCategory = "Purchased"
                    End If
                    strParcelType = "Polished"
                End If
                rsComSql_1 = Nothing

                If strCategoryMain <> "" Then
                    If strCategoryMain <> strCategory Then
                        MsgBox("Invalid Category", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If

                'If strCompanyMain <> "" Then
                '    If strCompanyMain <> strCompany Then
                '        MsgBox("Invalid Company", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                '        Exit Sub
                '    End If
                'End If

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT Country " & _
                                "FROM dbo.tblNoneOrders " & _
                                "WHERE (OrderNo = '" & strOrderNo & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strCountry = rsComSql_1.Fields("Country").Value
                End If
                rsComSql_1 = Nothing

                If strCountryMain <> "" Then
                    If strCountryMain <> strCountry Then
                        MsgBox("Invalid Country", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If

                'If strParcelTypeMain <> "" Then
                '    If strParcelTypeMain <> strParcelType Then
                '        MsgBox("Invalid Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                '        Exit Sub
                '    End If
                'End If

                If strCategory = "NFE" Then
                    If strSupplierMain <> "" Then
                        If strSupplierMain <> strSupplier Then
                            MsgBox("Invalid Supplier", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                    End If
                End If

                If rsComSql.Fields("Pcs").Value - intRghIssPcs > 0 Then
                    flxDetails.Rows.Add(rsComSql.Fields("ReturnType").Value,
                                        rsComSql.Fields("ParNo").Value,
                                        rsComSql.Fields("Pcs").Value - intRghIssPcs,
                                        Format(Math.Round(rsComSql.Fields("Cts").Value - dblRghIssCts, 3), "#0.000"),
                                        rsComSql.Fields("Pcs").Value - intRghIssPcs,
                                        Format(Math.Round(rsComSql.Fields("Cts").Value - dblRghIssCts, 3), "#0.000"),
                                        "0",
                                        Format(rsComSql.Fields("Price").Value, "#0.00"),
                                        Format(rsComSql.Fields("Price").Value * (rsComSql.Fields("Cts").Value - dblRghIssCts), "#0.00"),
                                        rsComSql.Fields("Department").Value,
                                        "",
                                        rsComSql.Fields("OrderNo").Value,
                                        rsComSql.Fields("RefNo").Value,
                                        rsComSql.Fields("Side").Value,
                                        rsComSql.Fields("PktNo").Value,
                                        False,
                                        strSupplier,
                                        strCategory,
                                        strInvoice,
                                        strCompany,
                                        strCountry,
                                        strParcelType,
                                        rsComSql.Fields("GiaNo").Value,
                                        rsComSql.Fields("RateCode").Value)
                End If

                intIssPcs = intIssPcs + rsComSql.Fields("Pcs").Value - intRghIssPcs
                dblIssCts = dblIssCts + rsComSql.Fields("Cts").Value - dblRghIssCts
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        dblIssCts = Math.Round(dblIssCts, 3)

        txtAddPcs.Text = intIssPcs
        txtPcs.Text = intIssPcs
        txtAddCts.Text = Format(dblIssCts, "#0.000")
        txtCts.Text = Format(dblIssCts, "#0.000")

        'flxDetails.Focus()
        cmbOrder.Text = ""

    End Sub

    Private Sub cmbOrder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbOrder.SelectedIndexChanged
        If Not cmbOrder.SelectedItem Is Nothing Then
            Load_ExportDetailsOrder("GradingPCU_N", cmbOrder.SelectedItem.Col1, cmbOrder.SelectedItem.Col2, cmbOrder.SelectedItem.Col3)
        End If
    End Sub

    Private Sub Load_ExportDetailsOrderAll(ByVal strDepartment As String, ByVal strOrderNo As String)
        Dim intIssPcs As Integer
        Dim dblIssCts As Double
        'Dim intRow As Integer

        Dim intRghIssPcs As Integer
        Dim dblRghIssCts As Double

        Dim strSupplier As String
        Dim strSupplierMain As String
        Dim strCategory As String
        Dim strCompany As String
        Dim strCategoryMain As String
        Dim strCompanyMain As String
        Dim strInvoice As String
        Dim strInvoiceMain As String
        Dim strCountry As String
        Dim strCountryMain As String
        Dim strParcelType As String
        Dim strParcelTypeMain As String

        Dim strParcel As String

        'For intRow = 0 To flxDetails.Rows.Count - 1
        '    If strParcel = flxDetails.Item(1, intRow).Value And strOrderNo = flxDetails.Item(11, intRow).Value And _
        '        strRef = flxDetails.Item(12, intRow).Value And strSide = flxDetails.Item(13, intRow).Value Then
        '        MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '        Exit Sub
        '    End If
        'Next

        strSupplierMain = ""
        strCategoryMain = ""
        strInvoiceMain = ""
        strCompanyMain = ""
        strCountryMain = ""
        strParcelTypeMain = ""

        strParcel = ""

        If flxDetails.Rows.Count > 0 Then
            If flxDetails.Item(12, 0).Value <> "" Then
                'If strRef <> flxDetails.Item(12, 0).Value Then
                '    MsgBox("Invalid Ref", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                '    Exit Sub
                'End If
            End If

            If flxDetails.Item(13, 0).Value <> "" Then
                'If strSide <> flxDetails.Item(13, 0).Value Then
                '    MsgBox("Invalid Side", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                '    Exit Sub
                'End If
            End If

            strSupplierMain = flxDetails.Item(16, 0).Value
            strCategoryMain = flxDetails.Item(17, 0).Value
            strInvoiceMain = flxDetails.Item(18, 0).Value
            strCompanyMain = flxDetails.Item(19, 0).Value
            strCountryMain = flxDetails.Item(20, 0).Value
            strParcelTypeMain = flxDetails.Item(21, 0).Value
        End If

        intIssPcs = 0
        dblIssCts = 0
        flxDetails.Rows.Clear()

        intRghIssPcs = 0
        dblRghIssCts = 0
        rsComSql = New ADODB.Recordset
        If strDepartment = "GradingPCU_N" Then
            'rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblGrading_SizingTypes.Department, dbo.tblGrading_SizingTypes.ReturnType, dbo.tblGrading_SizingTypes.ParNo,dbo.tblGrading_SizingTypes.PktNo, " & _
            '                    "SUM(dbo.tblGrading_SizingTypes.Pcs) AS Pcs, ROUND(SUM(dbo.tblGrading_SizingTypes.Cts), 3) AS Cts, dbo.tblGrading_SizingList.PRICE, " & _
            '                    "dbo.tblGrading_SizingPacket.OrderNo AS OrderNo, dbo.tblGrading_SizingPacket.RefNo, dbo.tblGrading_SizingPacket.Side, dbo.tblGrading_SizingPacket.GiaNo, dbo.tblGrading_SizingPacket.RateCode " & _
            '              "FROM dbo.tblGrading_SizingTypes INNER JOIN dbo.tblGrading_SizingList ON dbo.tblGrading_SizingTypes.ReturnType = dbo.tblGrading_SizingList.NAME INNER JOIN " & _
            '                    "dbo.tblGrading_SizingPacket ON dbo.tblGrading_SizingTypes.Department = dbo.tblGrading_SizingPacket.Department AND " & _
            '                    "dbo.tblGrading_SizingTypes.ParNo = dbo.tblGrading_SizingPacket.ParNo And dbo.tblGrading_SizingTypes.PktNo = dbo.tblGrading_SizingPacket.PktNo " & _
            '              "WHERE (dbo.tblGrading_SizingTypes.ParNo = '" & strParcel & "') AND (dbo.tblGrading_SizingTypes.OK = 0) AND (dbo.tblGrading_SizingPacket.OrderNo = '" & strOrderNo & "') AND " & _
            '                    "(dbo.tblGrading_SizingPacket.RefNo = '" & strRef & "') AND (dbo.tblGrading_SizingPacket.Side = '" & strSide & "') " & _
            '              "GROUP BY dbo.tblGrading_SizingTypes.Department, dbo.tblGrading_SizingTypes.ReturnType, dbo.tblGrading_SizingList.PRICE, dbo.tblGrading_SizingTypes.ParNo,dbo.tblGrading_SizingTypes.PktNo, " & _
            '                    "dbo.tblGrading_SizingPacket.OrderNo, dbo.tblGrading_SizingPacket.RefNo, dbo.tblGrading_SizingPacket.Side, dbo.tblGrading_SizingPacket.GiaNo, dbo.tblGrading_SizingPacket.RateCode " & _
            '              "ORDER BY dbo.tblGrading_SizingTypes.ReturnType, dbo.tblGrading_SizingTypes.PktNo", AdoCN, 1, 1)

            rsComSql.Open("SELECT TOP (100) PERCENT dbo.VW_GradingPCUSizeFinishBal.Department, dbo.VW_GradingPCUSizeFinishBal.OrderNo, dbo.VW_GradingPCUSizeFinishBal.RefNo, " & _
                                "dbo.VW_GradingPCUSizeFinishBal.Side, dbo.VW_GradingPCUSizeFinishBal.ParNo, dbo.VW_GradingPCUSizeFinishBal.PktNo, " & _
                                "dbo.VW_GradingPCUSizeFinishBal.ReturnType, dbo.VW_GradingPCUSizeFinishBal.BalPcs AS Pcs, dbo.VW_GradingPCUSizeFinishBal.BalCts AS Cts, " & _
                                "dbo.tblGrading_SizingList.PRICE, dbo.VW_GradingPCUSizeFinishBal.GiaNo, dbo.VW_GradingPCUSizeFinishBal.RateCode " & _
                          "FROM dbo.VW_GradingPCUSizeFinishBal INNER JOIN dbo.tblGrading_SizingList ON dbo.VW_GradingPCUSizeFinishBal.ReturnType = dbo.tblGrading_SizingList.NAME " & _
                          "WHERE (dbo.VW_GradingPCUSizeFinishBal.OrderNo = " & strOrderNo & ") " & _
                          "ORDER BY dbo.VW_GradingPCUSizeFinishBal.ReturnType, dbo.VW_GradingPCUSizeFinishBal.PktNo", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                intRghIssPcs = 0
                dblRghIssCts = 0
                'rsComSql_1 = New ADODB.Recordset
                'rsComSql_1.Open("SELECT SUM(IssPcs) AS IssPcs, ROUND(SUM(IssCts), 3) AS IssCts " & _
                '                "FROM tblGrading_RghIssues " & _
                '                "WHERE (Export = 0) AND (Department = '" & rsComSql.Fields("Department").Value & "') AND " & _
                '                    "(ParNo = '" & rsComSql.Fields("ParNo").Value & "') AND (PktNo = '" & rsComSql.Fields("PktNo").Value & "') AND (Assortment = '" & rsComSql.Fields("ReturnType").Value & "')", AdoCN, 1, 1)
                'If rsComSql_1.RecordCount Then
                '    If Not IsDBNull(rsComSql_1.Fields("IssPcs").Value) Then
                '        intRghIssPcs = rsComSql_1.Fields("IssPcs").Value
                '        dblRghIssCts = rsComSql_1.Fields("IssCts").Value
                '    End If
                'End If
                'rsComSql_1 = Nothing
                strInvoice = ""

                strSupplier = "Niru Diamonds Israel (1987) Ltd"
                strParcel = rsComSql.Fields("ParNo").Value

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT dbo.tblDep_Trf.Department, dbo.tblDep_Trf.DCLParcelNo, dbo.tblSuppliers.CompanyName, dbo.tblDep_Trf.SupplierRefNo " & _
                                "FROM dbo.tblDep_Trf INNER JOIN dbo.tblSuppliers ON dbo.tblDep_Trf.SupplierCode = dbo.tblSuppliers.SupplierCode " & _
                                "WHERE (dbo.tblDep_Trf.Department = 'Precision') AND (dbo.tblDep_Trf.DCLParcelNo = '" & strParcel & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strSupplier = rsComSql_1.Fields("CompanyName").Value
                    strInvoice = rsComSql_1.Fields("SupplierRefNo").Value
                End If
                rsComSql_1 = Nothing

                If strInvoice = "" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT dbo.tblDep_Trf.Department, dbo.tblDep_Trf.DCLParcelNo, dbo.tblSuppliers.CompanyName, dbo.tblDep_Trf.SupplierRefNo " & _
                                    "FROM dbo.tblDep_Trf INNER JOIN dbo.tblSuppliers ON dbo.tblDep_Trf.SupplierCode = dbo.tblSuppliers.SupplierCode " & _
                                    "WHERE (dbo.tblDep_Trf.DCLParcelNo = '" & Mid(strParcel, 1, 6) & "')", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        strSupplier = rsComSql_1.Fields("CompanyName").Value
                        strInvoice = rsComSql_1.Fields("SupplierRefNo").Value
                    End If
                    rsComSql_1 = Nothing
                End If

                If strInvoice = "" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT dbo.tblDep_Trf.Department, dbo.tblDep_Trf.DCLParcelNo, dbo.tblSuppliers.CompanyName, dbo.tblDep_Trf.SupplierRefNo " & _
                                    "FROM dbo.tblDep_Trf INNER JOIN dbo.tblSuppliers ON dbo.tblDep_Trf.SupplierCode = dbo.tblSuppliers.SupplierCode " & _
                                    "WHERE (dbo.tblDep_Trf.DCLParcelNo = '" & strParcel & "')", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        strSupplier = rsComSql_1.Fields("CompanyName").Value
                        strInvoice = rsComSql_1.Fields("SupplierRefNo").Value
                    End If
                    rsComSql_1 = Nothing
                End If

                strCategory = ""
                strCompany = "DCL"
                strParcelType = ""
                strCountry = ""

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT Category, CompCode, ParcelType " & _
                                "FROM dbo.tblImport " & _
                                "WHERE (DCLParcelNo = '" & strParcel & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strCategory = rsComSql_1.Fields("Category").Value
                    strCompany = rsComSql_1.Fields("CompCode").Value
                    strParcelType = rsComSql_1.Fields("ParcelType").Value
                End If
                rsComSql_1 = Nothing

                If strParcelType = "" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT Category, CompCode, ParcelType " & _
                                    "FROM dbo.tblImport " & _
                                    "WHERE (SupplierRefNo = '" & strInvoice & "') AND (SupplierRefNo <> '0') ORDER BY InvoiceDate", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        strCategory = rsComSql_1.Fields("Category").Value
                        strCompany = rsComSql_1.Fields("CompCode").Value
                        strParcelType = rsComSql_1.Fields("ParcelType").Value
                    End If
                    rsComSql_1 = Nothing
                End If

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * " & _
                                "FROM dbo.tblPOLStockOut " & _
                                "WHERE (DocID = '" & strParcel & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If strDBName = "DiaStock" Then
                        strCategory = "NFE"
                    Else
                        strCategory = "Purchased"
                    End If
                    strParcelType = "Polished"
                End If
                rsComSql_1 = Nothing

                If strCategoryMain <> "" Then
                    If strCategoryMain <> strCategory Then
                        MsgBox("Invalid Category", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If

                'If strCompanyMain <> "" Then
                '    If strCompanyMain <> strCompany Then
                '        MsgBox("Invalid Company", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                '        Exit Sub
                '    End If
                'End If

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT Country " & _
                                "FROM dbo.tblNoneOrders " & _
                                "WHERE (OrderNo = '" & strOrderNo & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strCountry = rsComSql_1.Fields("Country").Value
                End If
                rsComSql_1 = Nothing

                If strCountryMain <> "" Then
                    If strCountryMain <> strCountry Then
                        MsgBox("Invalid Country", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If

                'If strParcelTypeMain <> "" Then
                '    If strParcelTypeMain <> strParcelType Then
                '        MsgBox("Invalid Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                '        Exit Sub
                '    End If
                'End If

                If strCategory = "NFE" Then
                    If strSupplierMain <> "" Then
                        If strSupplierMain <> strSupplier Then
                            MsgBox("Invalid Supplier", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                    End If
                End If

                If rsComSql.Fields("Pcs").Value - intRghIssPcs > 0 Then
                    flxDetails.Rows.Add(rsComSql.Fields("ReturnType").Value,
                                        rsComSql.Fields("ParNo").Value,
                                        rsComSql.Fields("Pcs").Value - intRghIssPcs,
                                        Format(Math.Round(rsComSql.Fields("Cts").Value - dblRghIssCts, 3), "#0.000"),
                                        rsComSql.Fields("Pcs").Value - intRghIssPcs,
                                        Format(Math.Round(rsComSql.Fields("Cts").Value - dblRghIssCts, 3), "#0.000"),
                                        "0",
                                        Format(rsComSql.Fields("Price").Value, "#0.00"),
                                        Format(rsComSql.Fields("Price").Value * (rsComSql.Fields("Cts").Value - dblRghIssCts), "#0.00"),
                                        rsComSql.Fields("Department").Value,
                                        "",
                                        rsComSql.Fields("OrderNo").Value,
                                        rsComSql.Fields("RefNo").Value,
                                        rsComSql.Fields("Side").Value,
                                        rsComSql.Fields("PktNo").Value,
                                        False,
                                        strSupplier,
                                        strCategory,
                                        strInvoice,
                                        strCompany,
                                        strCountry,
                                        strParcelType,
                                        rsComSql.Fields("GiaNo").Value,
                                        rsComSql.Fields("RateCode").Value)
                End If

                intIssPcs = intIssPcs + rsComSql.Fields("Pcs").Value - intRghIssPcs
                dblIssCts = dblIssCts + rsComSql.Fields("Cts").Value - dblRghIssCts
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        dblIssCts = Math.Round(dblIssCts, 3)

        txtAddPcs.Text = intIssPcs
        txtPcs.Text = intIssPcs
        txtAddCts.Text = Format(dblIssCts, "#0.000")
        txtCts.Text = Format(dblIssCts, "#0.000")

        'flxDetails.Focus()
        txtOrderNo.Text = ""

    End Sub

    Private Sub txtOrderNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrderNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            Load_ExportDetailsOrderAll("GradingPCU_N", txtOrderNo.Text)
        End If
    End Sub
End Class