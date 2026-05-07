
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_ExpRejectBox
    Dim strFolderPath As String

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_RoughSelectParcelsSize()
        Dim strAssortment As String
        Dim strBagAssort As String
        Dim strCategory As String

        Dim blnRecordFound As Boolean

        blnRecordFound = False

        strAssortment = ""
        strBagAssort = ""
        strCategory = ""
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.VW_ExpSizingRghSelect.Department, dbo.VW_ExpSizingRghSelect.ParNo, dbo.VW_ExpSizingRghSelect.Pcs, " & _
                            "ISNULL(dbo.VW_ExpSizingRghTaken.Pcs, 0) AS TakenPcs, dbo.VW_ExpSizingRghSelect.Cts, ISNULL(dbo.VW_ExpSizingRghTaken.Cts, 0) AS TakenCts " & _
                      "FROM dbo.VW_ExpSizingRghSelect LEFT OUTER JOIN dbo.VW_ExpSizingRghTaken ON dbo.VW_ExpSizingRghSelect.Department = dbo.VW_ExpSizingRghTaken.Department AND " & _
                            "dbo.VW_ExpSizingRghSelect.ParNo = dbo.VW_ExpSizingRghTaken.ParNo " & _
                      "WHERE (dbo.VW_ExpSizingRghSelect.Pcs - ISNULL(dbo.VW_ExpSizingRghTaken.Pcs, 0) > 0) " & _
                      "ORDER BY dbo.VW_ExpSizingRghSelect.Department, dbo.VW_ExpSizingRghSelect.ParNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                blnRecordFound = True
                strAssortment = ""
                strBagAssort = ""

                rsComSql_1 = New ADODB.Recordset
                If rsComSql.Fields("Department").Value = "Mix" Then
                    If Len(rsComSql.Fields("ParNo").Value) > 8 Then
                        rsComSql_1.Open("SELECT * FROM tblImport WHERE SupParcelNo = '" & Mid(rsComSql.Fields("ParNo").Value, 1, 8) & "' AND SupplierRefNo NOT LIKE 'LCL%'", AdoCN, 1, 1)
                    Else
                        rsComSql_1.Open("SELECT * FROM tblImport WHERE SupParcelNo = '" & rsComSql.Fields("ParNo").Value & "' AND SupplierRefNo NOT LIKE 'LCL%'", AdoCN, 1, 1)
                    End If
                Else
                    rsComSql_1.Open("SELECT dbo.tblImport.Department, dbo.tblImport.SystemRefNo, dbo.tblImport.SupplierRefNo, dbo.tblImport.CompanyRefNo, dbo.tblImport.BOINo, dbo.tblImport.InvoiceDate, " & _
                                "dbo.tblImport.RecievedDate, dbo.tblImport.SupplierCode, dbo.tblImport.ParcelType, dbo.tblImport.AssortmentNo, dbo.tblImport.SupParcelNo, " & _
                                "dbo.tblImport.DclParcelNo , dbo.tblImport.Charges, dbo.tblImport.ItemCost, dbo.tblImport.ImportNo, dbo.tblParcel.GrpParNo, dbo.tblImport.Category " & _
                        "FROM dbo.tblParcel INNER JOIN dbo.tblImport ON dbo.tblParcel.OrigParcelNo = dbo.tblImport.SupParcelNo " & _
                        "WHERE (dbo.tblParcel.Depart = '" & rsComSql.Fields("Department").Value & "') AND (NOT (dbo.tblImport.SupplierRefNo LIKE N'LCL%')) AND (dbo.tblParcel.GrpParNo = '" & rsComSql.Fields("ParNo").Value & "')", AdoCN, 1, 1)
                End If
                If rsComSql_1.RecordCount Then
                    strAssortment = rsComSql_1.Fields("AssortmentNo").Value
                    strCategory = rsComSql_1.Fields("Category").Value
                End If
                rsComSql_1 = Nothing

                If strAssortment = "" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblGradingTrf WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND ParcelNo = '" & rsComSql.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        strAssortment = rsComSql_1.Fields("Assort1").Value
                    End If
                    rsComSql_1 = Nothing
                End If

                If strAssortment = "" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblExpPacket WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND ParNo = '" & rsComSql.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        strAssortment = rsComSql_1.Fields("AParNo").Value
                        strBagAssort = rsComSql_1.Fields("AParNo").Value
                    End If
                    rsComSql_1 = Nothing
                End If

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblExpPacket WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND ParNo = '" & rsComSql.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strBagAssort = rsComSql_1.Fields("AParNo").Value
                End If
                rsComSql_1 = Nothing

                flxDetails.Rows.Add(rsComSql.Fields("Department").Value,
                                    strAssortment,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    Format(rsComSql.Fields("Cts").Value, "#0.#00"),
                                    rsComSql.Fields("Pcs").Value - rsComSql.Fields("TakenPcs").Value,
                                    Format(rsComSql.Fields("Cts").Value - rsComSql.Fields("TakenCts").Value, "#0.#00"),
                                    strBagAssort,
                                    "", strCategory)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        If blnRecordFound = False Then
            MsgBox("No Records Found", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If

    End Sub

    Private Sub Load_RoughSelectParcelsL()
        Dim strAssortment As String
        Dim strBagAssort As String
        Dim strCategory As String

        Dim blnRecordFound As Boolean

        blnRecordFound = False

        strAssortment = ""
        strBagAssort = ""
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.VW_ExpRghSelectL.Department, dbo.VW_ExpRghSelectL.ParNo, dbo.VW_ExpRghSelectL.Assort1, dbo.VW_ExpRghSelectL.Pcs, " & _
                            "ISNULL(dbo.VW_ExpRghTakenL.Pcs, 0) AS TakenPcs, dbo.VW_ExpRghSelectL.Cts, ISNULL(dbo.VW_ExpRghTakenL.Cts, 0) AS TakenCts, " & _
                            "dbo.VW_ExpRghSelectL.PktNo " & _
                      "FROM dbo.VW_ExpRghSelectL LEFT OUTER JOIN dbo.VW_ExpRghTakenL ON dbo.VW_ExpRghSelectL.PktNo = dbo.VW_ExpRghTakenL.PktNo AND " & _
                            "dbo.VW_ExpRghSelectL.Department = dbo.VW_ExpRghTakenL.Department AND dbo.VW_ExpRghSelectL.ParNo = dbo.VW_ExpRghTakenL.ParNo AND " & _
                            "dbo.VW_ExpRghSelectL.Assort1 = dbo.VW_ExpRghTakenL.Assort1 " & _
                      "WHERE (dbo.VW_ExpRghSelectL.Pcs - ISNULL(dbo.VW_ExpRghTakenL.Pcs, 0) > 0) " & _
                      "ORDER BY dbo.VW_ExpRghSelectL.Department, dbo.VW_ExpRghSelectL.ParNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                blnRecordFound = True
                strAssortment = ""
                strBagAssort = ""
                strCategory = ""

                rsComSql_1 = New ADODB.Recordset
                If rsComSql.Fields("Department").Value = "Mix" Then
                    If Len(rsComSql.Fields("ParNo").Value) > 8 Then
                        rsComSql_1.Open("SELECT * FROM tblImport WHERE SupParcelNo = '" & Mid(rsComSql.Fields("ParNo").Value, 1, 8) & "' AND SupplierRefNo NOT LIKE 'LCL%'", AdoCN, 1, 1)
                    Else
                        rsComSql_1.Open("SELECT * FROM tblImport WHERE SupParcelNo = '" & rsComSql.Fields("ParNo").Value & "' AND SupplierRefNo NOT LIKE 'LCL%'", AdoCN, 1, 1)
                    End If
                Else
                    rsComSql_1.Open("SELECT dbo.tblImport.Department, dbo.tblImport.SystemRefNo, dbo.tblImport.SupplierRefNo, dbo.tblImport.CompanyRefNo, dbo.tblImport.BOINo, dbo.tblImport.InvoiceDate, " & _
                                            "dbo.tblImport.RecievedDate, dbo.tblImport.SupplierCode, dbo.tblImport.ParcelType, dbo.tblImport.AssortmentNo, dbo.tblImport.SupParcelNo, " & _
                                            "dbo.tblImport.DclParcelNo , dbo.tblImport.Charges, dbo.tblImport.ItemCost, dbo.tblImport.ImportNo, dbo.tblParcel.GrpParNo, dbo.tblImport.Category " & _
                                    "FROM dbo.tblParcel INNER JOIN dbo.tblImport ON dbo.tblParcel.OrigParcelNo = dbo.tblImport.SupParcelNo " & _
                                    "WHERE (dbo.tblParcel.Depart = '" & rsComSql.Fields("Department").Value & "') AND (NOT (dbo.tblImport.SupplierRefNo LIKE N'LCL%')) AND (dbo.tblParcel.GrpParNo = '" & rsComSql.Fields("ParNo").Value & "')", AdoCN, 1, 1)
                End If
                If rsComSql_1.RecordCount Then
                    strAssortment = rsComSql_1.Fields("AssortmentNo").Value
                    strCategory = rsComSql_1.Fields("Category").Value
                End If
                rsComSql_1 = Nothing

                If strAssortment = "" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblGradingTrf WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND ParcelNo = '" & rsComSql.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        strAssortment = rsComSql_1.Fields("Assort1").Value
                    End If
                    rsComSql_1 = Nothing
                End If

                If strAssortment = "" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblExpPacket WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND ParNo = '" & rsComSql.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        strAssortment = rsComSql_1.Fields("AParNo").Value
                    End If
                    rsComSql_1 = Nothing
                End If

                flxDetails.Rows.Add(rsComSql.Fields("Department").Value,
                                    strAssortment,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    Format(rsComSql.Fields("Cts").Value, "#0.#00"),
                                    rsComSql.Fields("Pcs").Value - rsComSql.Fields("TakenPcs").Value,
                                    Format(rsComSql.Fields("Cts").Value - rsComSql.Fields("TakenCts").Value, "#0.#00"),
                                    rsComSql.Fields("Assort1").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    strCategory)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        If blnRecordFound = False Then
            MsgBox("No Records Found", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If

    End Sub

    Private Sub Load_RoughSelectParcels()
        Dim strAssortment As String
        Dim strBagAssort As String
        Dim strCategory As String

        Dim blnRecordFound As Boolean

        blnRecordFound = False

        strAssortment = ""
        strBagAssort = ""
        strCategory = ""
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.VW_ExpRghSelect.Department, dbo.VW_ExpRghSelect.ParNo, dbo.VW_ExpRghSelect.Assort1, dbo.VW_ExpRghSelect.Pcs, ISNULL(dbo.VW_ExpRghTaken.Pcs, 0) " & _
                            "AS TakenPcs, dbo.VW_ExpRghSelect.Cts, ISNULL(dbo.VW_ExpRghTaken.Cts, 0) AS TakenCts " & _
                      "FROM dbo.VW_ExpRghSelect LEFT OUTER JOIN dbo.VW_ExpRghTaken ON dbo.VW_ExpRghSelect.Department = dbo.VW_ExpRghTaken.Department AND " & _
                            "dbo.VW_ExpRghSelect.ParNo = dbo.VW_ExpRghTaken.ParNo AND dbo.VW_ExpRghSelect.Assort1 = dbo.VW_ExpRghTaken.Assort1 " & _
                      "WHERE (dbo.VW_ExpRghSelect.Pcs - IsNull(dbo.VW_ExpRghTaken.Pcs, 0) > 0) " & _
                      "ORDER BY dbo.VW_ExpRghSelect.Department, dbo.VW_ExpRghSelect.ParNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                blnRecordFound = True
                strAssortment = ""
                strBagAssort = ""

                rsComSql_1 = New ADODB.Recordset
                If rsComSql.Fields("Department").Value = "Mix" Then
                    If Len(rsComSql.Fields("ParNo").Value) > 8 Then
                        rsComSql_1.Open("SELECT * FROM tblImport WHERE SupParcelNo = '" & Mid(rsComSql.Fields("ParNo").Value, 1, 8) & "' AND SupplierRefNo NOT LIKE 'LCL%'", AdoCN, 1, 1)
                    Else
                        rsComSql_1.Open("SELECT * FROM tblImport WHERE SupParcelNo = '" & rsComSql.Fields("ParNo").Value & "' AND SupplierRefNo NOT LIKE 'LCL%'", AdoCN, 1, 1)
                    End If
                Else
                    rsComSql_1.Open("SELECT dbo.tblImport.Department, dbo.tblImport.SystemRefNo, dbo.tblImport.SupplierRefNo, dbo.tblImport.CompanyRefNo, dbo.tblImport.BOINo, dbo.tblImport.InvoiceDate, " & _
                                            "dbo.tblImport.RecievedDate, dbo.tblImport.SupplierCode, dbo.tblImport.ParcelType, dbo.tblImport.AssortmentNo, dbo.tblImport.SupParcelNo, " & _
                                            "dbo.tblImport.DclParcelNo , dbo.tblImport.Charges, dbo.tblImport.ItemCost, dbo.tblImport.ImportNo, dbo.tblParcel.GrpParNo, dbo.tblImport.Category " & _
                                    "FROM dbo.tblParcel INNER JOIN dbo.tblImport ON dbo.tblParcel.OrigParcelNo = dbo.tblImport.SupParcelNo " & _
                                    "WHERE (dbo.tblParcel.Depart = '" & rsComSql.Fields("Department").Value & "') AND (NOT (dbo.tblImport.SupplierRefNo LIKE N'LCL%')) AND (dbo.tblParcel.GrpParNo = '" & rsComSql.Fields("ParNo").Value & "')", AdoCN, 1, 1)
                End If
                If rsComSql_1.RecordCount Then
                    strAssortment = rsComSql_1.Fields("AssortmentNo").Value
                    strCategory = rsComSql_1.Fields("Category").Value
                End If
                rsComSql_1 = Nothing

                If strAssortment = "" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblGradingTrf WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND ParcelNo = '" & rsComSql.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        strAssortment = rsComSql_1.Fields("Assort1").Value
                    End If
                    rsComSql_1 = Nothing
                End If

                If strAssortment = "" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblExpPacket WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND ParNo = '" & rsComSql.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        strAssortment = rsComSql_1.Fields("AParNo").Value
                    End If
                    rsComSql_1 = Nothing
                End If

                flxDetails.Rows.Add(rsComSql.Fields("Department").Value,
                                    strAssortment,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    Format(rsComSql.Fields("Cts").Value, "#0.#00"),
                                    rsComSql.Fields("Pcs").Value - rsComSql.Fields("TakenPcs").Value,
                                    Format(rsComSql.Fields("Cts").Value - rsComSql.Fields("TakenCts").Value, "#0.#00"),
                                    rsComSql.Fields("Assort1").Value, "",
                                    strCategory)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        If blnRecordFound = False Then
            MsgBox("No Records Found", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If

    End Sub

    Private Sub txtOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrder.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblNoneOrders WHERE OrderNo = '" & txtOrder.Text & "' AND Complete = 'N'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtOrder.Text = ""
            Else
                txtAddPcs.Focus()
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub txtAddPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAddPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtAddCts.Focus()
        End If
    End Sub

    Private Sub txtAddCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAddCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtAddCts.Text)
        If Asc(e.KeyChar) = 13 Then
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim intRow As Integer

        If txtDepartment.Text <> "" And txtParNo.Text <> "" And txtAddPcs.Text <> "" And txtAddCts.Text <> "" Then

            For intRow = 0 To flxSelect.Rows.Count - 1
                If txtDepartment.Text = flxSelect.Item(0, intRow).Value And _
                   txtParNo.Text = flxSelect.Item(1, intRow).Value And _
                   txtOrder.Text = flxSelect.Item(2, intRow).Value Then
                    MsgBox("Already Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Next

            If CDbl(txtPcs.Text) < CDbl(txtAddPcs.Text) + CDbl(txtSelPcs.Text) Then
                MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtAddPcs.Focus()
                Exit Sub
            End If
            If CDbl(txtCts.Text) < Math.Round(CDbl(txtAddCts.Text) + CDbl(txtSelCts.Text), 3) Then
                MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtAddCts.Focus()
                Exit Sub
            End If
            If CDbl(txtPcs.Text) - CDbl(txtSelPcs.Text) = CDbl(txtAddPcs.Text) Then
                If CDbl(txtCts.Text) <> Math.Round(CDbl(txtAddCts.Text) + CDbl(txtSelCts.Text), 3) Then
                    MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    txtAddCts.Focus()
                    Exit Sub
                End If
            End If
            If CDbl(txtCts.Text) - CDbl(txtSelCts.Text) = CDbl(txtAddCts.Text) Then
                If CDbl(txtPcs.Text) <> CDbl(txtAddPcs.Text) + CDbl(txtSelPcs.Text) Then
                    MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    txtAddPcs.Focus()
                    Exit Sub
                End If
            End If

            flxSelect.Rows.Add(txtDepartment.Text,
                               txtAssortment.Text,
                               txtParNo.Text,
                               txtOrder.Text,
                               txtAddPcs.Text,
                               Format(CDbl(txtAddCts.Text), "0.#00"),
                               txtBagAssort.Text,
                               txtPktNo.Text)

            txtOrder.Text = ""
            txtAddPcs.Text = ""
            txtAddCts.Text = ""

            txtSelPcs.Text = CalTotalPcs
            txtSelCts.Text = CalTotalCts

            txtOrder.Focus()
        End If
    End Sub

    Private Function CalTotalPcs() As Double
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSelect.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSelect.Item(4, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts() As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSelect.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSelect.Item(5, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)

    End Function

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        If flxDetails.Rows.Count > 0 Then
            txtDepartment.Text = flxDetails.Item(0, flxDetails.CurrentRow.Index).Value
            txtAssortment.Text = flxDetails.Item(1, flxDetails.CurrentRow.Index).Value
            txtParNo.Text = flxDetails.Item(2, flxDetails.CurrentRow.Index).Value
            txtPcs.Text = flxDetails.Item(5, flxDetails.CurrentRow.Index).Value
            txtCts.Text = flxDetails.Item(6, flxDetails.CurrentRow.Index).Value
            txtBagAssort.Text = flxDetails.Item(7, flxDetails.CurrentRow.Index).Value
            txtPktNo.Text = flxDetails.Item(8, flxDetails.CurrentRow.Index).Value
            flxSelect.Rows.Clear()
            txtOrder.Focus()
        End If
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub ClearFields()
        txtDepartment.Text = ""
        txtParNo.Text = ""
        txtAssortment.Text = ""
        txtAddPcs.Text = ""
        txtAddCts.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtSelPcs.Text = "0"
        txtSelCts.Text = "0"
        optRough.Checked = False
        optGrading.Checked = False
        flxDetails.Rows.Clear()
        flxSelect.Rows.Clear()
        txtPktNo.Text = ""
    End Sub

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        If chkSize.Checked = True Then
            Load_RoughSelectParcelsSize()
        Else
            If optOpen.Checked = True Then
                Load_RoughSelectParcelsL()
            Else
                Load_RoughSelectParcels()
            End If
        End If
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub Save()
        Dim strType As String
        Dim intCat As Integer
        Dim intRow As Integer
        Dim strPktNo As String

        Dim dblRghPcs As Double
        Dim dblRghCts As Double
        Dim dblRetPcs As Double
        Dim dblRetCts As Double

        If flxSelect.Rows.Count < 1 Then MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If optRough.Checked = False And optGrading.Checked = False And optApcu.Checked = False And optExport.Checked = False And optOpen.Checked = False Then MsgBox("Select the Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            strType = ""
            If optRough.Checked = True Then
                strType = "R"
            End If
            If optGrading.Checked = True Then
                strType = "G"
            End If
            If optApcu.Checked = True Then
                strType = "X"
            End If
            If optExport.Checked = True Then
                strType = "E"
            End If
            If optOpen.Checked = True Then
                strType = "L"
            End If
            If chkSize.Checked = True Then
                intCat = 1
            Else
                intCat = 0
            End If

            If strType = "R" Then
                For intRow = 0 To flxSelect.Rows.Count - 1
                    If flxSelect.Item(0, intRow).Value = "Mix" Then
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT * FROM tblImport WHERE DCLParcelNo = '" & flxSelect.Item(2, intRow).Value & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            If rsComSql.Fields("CompCode").Value <> "DCL" Then
                                MsgBox("Invalid Company - " & rsComSql.Fields("CompCode").Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                Exit Sub
                            End If
                        End If
                        rsComSql = Nothing
                    Else

                    End If
                Next
            End If

            For intRow = 0 To flxSelect.Rows.Count - 1
                AdoCN.Execute("INSERT INTO tblExpRghTypes(Department, ParNo, Assortment, OrderNo, Pcs, Cts, OK, Type, DDate, Assort1, Size, PktNo) " & _
                              "VALUES('" & flxSelect.Item(0, intRow).Value & "','" & flxSelect.Item(2, intRow).Value & "'," & _
                                    "'" & flxSelect.Item(1, intRow).Value & "','" & flxSelect.Item(3, intRow).Value & "'," & CInt(flxSelect.Item(4, intRow).Value) & "," & _
                                    "" & CDbl(flxSelect.Item(5, intRow).Value) & ",0,'" & strType & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & flxSelect.Item(6, intRow).Value & "'," & intCat & ",'" & flxSelect.Item(7, intRow).Value & "')")

                If optApcu.Checked = True Then
                    strPktNo = "X001"
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT MAX(RIGHT(PktNo, 3)) AS MaxPktNo FROM tblExpPacket WHERE Department = '" & flxSelect.Item(0, intRow).Value & "' AND ParNo = '" & flxSelect.Item(2, intRow).Value & "' AND LEFT(PktNo, 1) = 'X'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If Not IsDBNull(rsComSql.Fields("MaxPktNo").Value) Then
                            strPktNo = "X" & Format(CInt(rsComSql.Fields("MaxPktNo").Value) + 1, "000")
                        End If
                    End If
                    rsComSql = Nothing

                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblExpPacket WHERE Department = '" & flxSelect.Item(0, intRow).Value & "' AND ParNo = '" & flxSelect.Item(2, intRow).Value & "' AND PktNo = '" & strPktNo & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        AdoCN.Execute("INSERT INTO tblExpPacket(Department,ParNo,PktNo,PktPcs,PktCts,PktType,PktDate,AParNo) " & _
                                      "VALUES('" & flxSelect.Item(0, intRow).Value & "','" & flxSelect.Item(2, intRow).Value & "','" & strPktNo & "'," & CInt(flxSelect.Item(4, intRow).Value) & "," & CDbl(flxSelect.Item(5, intRow).Value) & ",'','" & Format(Date.Now, "MM/dd/yyyy") & "','" & flxSelect.Item(1, intRow).Value & "')")
                    End If
                    rsComSql = Nothing

                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblExpIssues WHERE Department = '" & flxSelect.Item(0, intRow).Value & "' AND ParNo = '" & flxSelect.Item(2, intRow).Value & "' AND PktNo = '" & strPktNo & "' AND Sec = 1", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        AdoCN.Execute("INSERT INTO tblExpIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                      "VALUES('" & flxSelect.Item(0, intRow).Value & "','" & flxSelect.Item(2, intRow).Value & "','" & strPktNo & "',1,'D08411'," & CInt(flxSelect.Item(4, intRow).Value) & "," & _
                                             "" & CDbl(flxSelect.Item(5, intRow).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")
                    End If
                    rsComSql = Nothing
                End If

                If optOpen.Checked = True Then
                    dblRghPcs = 0
                    dblRghCts = 0
                    dblRetPcs = 0
                    dblRetCts = 0

                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblBAGPacket WHERE ParNo = '" & flxSelect.Item(2, intRow).Value & "' AND PktNo = '" & strRight(flxSelect.Item(7, intRow).Value, 4) & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        dblRghPcs = rsComSql.Fields("PktPcs").Value
                        dblRghCts = rsComSql.Fields("PktCts").Value
                        dblRghCts = Math.Round(dblRghCts, 3)
                    End If
                    rsComSql = Nothing

                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblBAGReturns WHERE ParNo = '" & flxSelect.Item(2, intRow).Value & "' AND PktNo = '" & strRight(flxSelect.Item(7, intRow).Value, 4) & "' AND Sec = 3", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        dblRetPcs = rsComSql.Fields("RetPcsT").Value + rsComSql.Fields("RetPcsB").Value
                        dblRetCts = rsComSql.Fields("RetCts").Value
                        dblRetCts = Math.Round(dblRetCts, 3)
                    End If
                    rsComSql = Nothing

                    If strRight(flxSelect.Item(2, intRow).Value, 1) = "S" Then
                        AdoCN.Execute("UPDATE tblBAGReturns SET PCUPcs = " & dblRetPcs - CInt(flxSelect.Item(4, intRow).Value) & ",PCUPCts = " & Math.Round(dblRetCts - CDbl(flxSelect.Item(5, intRow).Value), 3) & "," & _
                                        "PCUCts = " & Math.Round((dblRghCts / dblRghPcs) * (dblRetPcs - CInt(flxSelect.Item(4, intRow).Value)), 3) & ",IncPcs = " & CInt(flxSelect.Item(4, intRow).Value) & ",IncCts = " & Math.Round(CDbl(flxSelect.Item(5, intRow).Value), 3) & "  " & _
                                      "WHERE ParNo = '" & flxSelect.Item(2, intRow).Value & "' AND PktNo = '" & strRight(flxSelect.Item(7, intRow).Value, 4) & "' AND Sec = 3")
                    Else
                        AdoCN.Execute("UPDATE tblBAGReturns SET PCUPcs = " & dblRetPcs - CInt(flxSelect.Item(4, intRow).Value) & ",PCUPCts = " & Math.Round(dblRetCts - CDbl(flxSelect.Item(5, intRow).Value), 3) & "," & _
                                        "PCUCts = " & Math.Round((dblRghCts / dblRghPcs) * (dblRetPcs - CInt(flxSelect.Item(4, intRow).Value)), 3) & " " & _
                                      "WHERE ParNo = '" & flxSelect.Item(2, intRow).Value & "' AND PktNo = '" & strRight(flxSelect.Item(7, intRow).Value, 4) & "' AND Sec = 3")
                    End If
                End If
            Next

            MsgBox("Transfered Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If

        txtDepartment.Text = ""
        txtParNo.Text = ""
        txtAssortment.Text = ""
        flxSelect.Rows.Clear()
        flxDetails.Rows.Clear()
        txtAddPcs.Text = ""
        txtAddCts.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtSelPcs.Text = "0"
        txtSelCts.Text = "0"
        optRough.Checked = False
        optGrading.Checked = False
        txtPktNo.Text = ""

        If chkSize.Checked = True Then
            Load_RoughSelectParcelsSize()
        Else
            If optOpen.Checked = True Then
                Load_RoughSelectParcelsL()
            Else
                Load_RoughSelectParcels()
            End If
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub frm_ExpRejectBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        If strDBName = "DiaStock" Then
            strFolderPath = "Export\"
        Else
            strFolderPath = "DiaSalesExport\"
        End If

        txtAddPcs.Text = ""
        txtAddCts.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtSelPcs.Text = "0"
        txtSelCts.Text = "0"
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpRghIssues.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpGradingIssues.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpGradingIssuesL.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub chkSize_CheckedChanged(sender As Object) Handles chkSize.CheckedChanged
        ClearFields()
    End Sub
End Class