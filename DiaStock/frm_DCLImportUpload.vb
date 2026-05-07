
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_DCLImportUpload
    Dim dblLotID As Double
    Dim dblImpPrice As Double
    Dim strArticle As String
    Dim strRemarks As String
    Dim strItemName As String
    Dim intUrgent As Integer

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub ClearFields()
        txtSupRefNo.Text = ""
        cmbCompany.Text = ""
        cmbType.Text = ""
        cmbSupplier.Text = ""
        txtSupplier.Text = ""
        flxDept.Rows.Clear()
        txtTotalPcs.Text = ""
        txtTotalCts.Text = ""
        txtFilePath.Text = ""
        txtIndex.Text = ""
        txtAssortment.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtPrice.Text = ""
        txtSelPcs.Text = ""
        txtSelCts.Text = ""
        txtBalPcs.Text = ""
        txtBalCts.Text = ""
        txtInvPcs.Text = ""
        txtInvCts.Text = ""
        cmbAssort.Text = ""
        txtLotID.Text = ""
        txtOrigin.Text = ""
        txtImpPrice.Text = ""
        flxSelect.Rows.Clear()

        txtSupRefNo.Focus()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_Company()
        cmbCompany.Items.Clear()
        rsComSql = New ADODB.Recordset
        mStrSQL = "SELECT CompCode FROM tblCompany ORDER BY CompCode"
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        Do While Not rsComSql.EOF
            cmbCompany.Items.Add(rsComSql.Fields("CompCode").Value)
            rsComSql.MoveNext()
        Loop
        rsComSql = Nothing
    End Sub

    Private Sub Load_Supplier()
        cmbSupplier.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblSuppliers ORDER BY SupplierCode", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbSupplier.Items.Add(Format(rsComSql.Fields("SupplierCode").Value, "00000"))

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_NewAssort()
        cmbAssort.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblImportNewAssort ORDER BY NewAssort", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbAssort.Items.Add(rsComSql.Fields("NewAssort").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdOpen_Click(sender As Object, e As EventArgs) Handles cmdOpen.Click
        flxDept.Rows.Clear()
        OpenFileDialog1.Filter = "All Excel Files|*.xls;*.xlsx"
        OpenFileDialog1.InitialDirectory = "C:\"
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName <> "" Then
            txtFilePath.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Load_Excel()
        On Error GoTo ErrorHandler

        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim intRow, m_LotNo As Integer
        Dim strAssortment As String
        Dim dblSize, dblPrice, dblTotPcs, dblTotCts, dblImpPrice As Double
        Dim dblLotNo As Double

        If txtFilePath.Text = "" Then
            MsgBox("Please select the Excel File", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If Len(Dir(txtFilePath.Text)) > 0 Then

            If ValidateFields() = False Then Exit Sub

            xlApp = New Excel.Application
            xlWorkBook = xlApp.Workbooks.Open(txtFilePath.Text)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            flxDept.Rows.Clear()
            m_LotNo = 1
            dblLotNo = 0
            For intRow = 6 To 1000
                If Mid(xlWorkSheet.Cells(intRow, 1).Value, 1, 5) = "Total" Then Exit For
                If Trim(xlWorkSheet.Cells(intRow, 1).Value) <> "" Then
                    If Len(Trim(xlWorkSheet.Cells(intRow, 1).Value)) > 0 Then
                        If CDbl(Trim(xlWorkSheet.Cells(intRow, 1).Value)) = 0 Then
                            If dblLotNo = 0 Then
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT MAX(LotNo) AS MaxLotNo FROM tblImport WHERE (LotNo >= 80000000) AND (LotNo <= 89999999)", AdoCN, 1, 1)
                                If Not IsDBNull(rsComSql_1.Fields("MaxLotNo").Value) Then
                                    dblLotNo = rsComSql_1.Fields("MaxLotNo").Value + 1
                                End If
                                rsComSql_1 = Nothing
                            Else
                                dblLotNo = dblLotNo + 1
                            End If
                        Else
                            dblLotNo = CDbl(Trim(xlWorkSheet.Cells(intRow, 1).Value))
                        End If
                    End If

                    dblSize = CDbl(Trim(xlWorkSheet.Cells(intRow, 8).Value)) / CDbl(Trim(xlWorkSheet.Cells(intRow, 9).Value))
                    dblPrice = CDbl(Trim(xlWorkSheet.Cells(intRow, 14).Value)) / CDbl(Trim(xlWorkSheet.Cells(intRow, 9).Value))
                    If IsNumeric(Trim(xlWorkSheet.Cells(intRow, 17).Value)) = True Then
                        dblImpPrice = CDbl(Trim(xlWorkSheet.Cells(intRow, 17).Value))
                    Else
                        dblImpPrice = dblPrice
                    End If

                    strAssortment = Trim(xlWorkSheet.Cells(intRow, 7).Value)
                    intUrgent = IIf(xlWorkSheet.Cells(intRow, 25).Value = "1", 1, 0)

                    flxDept.Rows.Add(m_LotNo,
                                    strAssortment,
                                    "",
                                    "",
                                    Trim(xlWorkSheet.Cells(intRow, 8).Value),
                                    Math.Round(CDbl(xlWorkSheet.Cells(intRow, 9).Value), 3),
                                    Format(dblSize, "#0.00"),
                                    Format(dblPrice, "#0.00"),
                                    dblLotNo,
                                    Trim(xlWorkSheet.Cells(intRow, 23).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 20).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 3).Value),
                                    intUrgent, "",
                                    Trim(xlWorkSheet.Cells(intRow, 5).Value),
                                    dblImpPrice)

                    m_LotNo = m_LotNo + 1
                    dblTotPcs = dblTotPcs + CDbl(Trim(xlWorkSheet.Cells(intRow, 8).Value))
                    dblTotCts = dblTotCts + CDbl(Trim(xlWorkSheet.Cells(intRow, 9).Value))
                End If

            Next
            xlWorkSheet = Nothing
            xlWorkBook.Close()
            xlApp.Quit()

            txtTotalPcs.Text = dblTotPcs
            txtTotalCts.Text = dblTotCts

            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)
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

    Private Function ValidateFields() As Boolean
        ValidateFields = True

        txtSupRefNo.Text = UCase(txtSupRefNo.Text)
        If Not Len(Trim(txtSupRefNo.Text)) > 0 Then
            MsgBox("Please enter the Supplier Ref No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(cmbType.Text)) > 0 Then
            MsgBox("Please enter the Parcel Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(cmbSupplier.Text)) > 0 Then
            MsgBox("Please enter the Supplier", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(cmbCompany.Text)) > 0 Then
            MsgBox("Please enter the Company", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        Return ValidateFields
    End Function

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Load_Excel()
    End Sub

    Private Sub frm_DCLImportUpload_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Company()
        Load_Supplier()
        Load_NewAssort()
    End Sub

    Private Sub cmbSupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSupplier.SelectedIndexChanged
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblSuppliers WHERE SupplierCode = " & CInt(cmbSupplier.Text) & "", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            txtSupplier.Text = rsComSql_1.Fields("CompanyName").Value
        Else
            txtSupplier.Text = ""
        End If
        rsComSql_1 = Nothing
    End Sub

    Private Sub flxDept_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDept.CellClick
        If (txtPcs.Text = txtBalPcs.Text And txtCts.Text = txtBalCts.Text) Or (txtBalPcs.Text = "" And txtBalCts.Text = "") Then
            txtIndex.Text = flxDept.Item(0, flxDept.CurrentRow.Index).Value
            txtAssortment.Text = flxDept.Item(1, flxDept.CurrentRow.Index).Value
            txtPcs.Text = flxDept.Item(4, flxDept.CurrentRow.Index).Value
            txtCts.Text = flxDept.Item(5, flxDept.CurrentRow.Index).Value
            txtBalPcs.Text = flxDept.Item(4, flxDept.CurrentRow.Index).Value
            txtBalCts.Text = flxDept.Item(5, flxDept.CurrentRow.Index).Value
            dblImpPrice = flxDept.Item(7, flxDept.CurrentRow.Index).Value
            txtPrice.Text = dblImpPrice
            dblLotID = flxDept.Item(8, flxDept.CurrentRow.Index).Value
            strArticle = flxDept.Item(9, flxDept.CurrentRow.Index).Value
            strRemarks = flxDept.Item(10, flxDept.CurrentRow.Index).Value
            strItemName = flxDept.Item(11, flxDept.CurrentRow.Index).Value
            intUrgent = CInt(flxDept.Item(12, flxDept.CurrentRow.Index).Value)
            txtLotID.Text = flxDept.Item(8, flxDept.CurrentRow.Index).Value
            txtOrigin.Text = flxDept.Item(14, flxDept.CurrentRow.Index).Value
            txtImpPrice.Text = flxDept.Item(15, flxDept.CurrentRow.Index).Value

            txtPrice.Focus()
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        AddData()
    End Sub

    Private Sub AddData()

        If txtIndex.Text <> "" And txtAssortment.Text <> "" And txtLotID.Text <> "" Then
            If txtSelPcs.Text <> "" And txtSelCts.Text <> "" Then
                If CDbl(txtSelCts.Text) > 0 Then
                    If CDbl(txtSelPcs.Text) <= CDbl(txtBalPcs.Text) Then
                        If CDbl(txtSelCts.Text) <= CDbl(txtBalCts.Text) Then
                            If txtPrice.Text = "" Then
                                MsgBox("Invalid Price", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                Exit Sub
                            End If
                            If CDbl(txtPrice.Text) <= 0 Then
                                MsgBox("Invalid Price", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                Exit Sub
                            End If

                            If cmbAssort.Text = "" Then
                                MsgBox("Invalid New Assort", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                Exit Sub
                            End If

                            flxSelect.Rows.Add(txtIndex.Text,
                                               txtAssortment.Text,
                                               "", "",
                                               txtSelPcs.Text,
                                               txtSelCts.Text,
                                               CDbl(txtSelPcs.Text) / CDbl(txtSelCts.Text),
                                               dblImpPrice,
                                               txtLotID.Text,
                                               strArticle,
                                               strRemarks,
                                               strItemName,
                                               intUrgent,
                                               cmbAssort.Text,
                                               txtPrice.Text,
                                               dblLotID,
                                               txtOrigin.Text,
                                               txtImpPrice.Text)

                            txtBalPcs.Text = CDbl(txtBalPcs.Text) - CDbl(txtSelPcs.Text)
                            txtBalCts.Text = CDbl(txtBalCts.Text) - CDbl(txtSelCts.Text)
                            txtBalCts.Text = Math.Round(CDbl(txtBalCts.Text), 3)

                            If CDbl(txtBalPcs.Text) = 0 And CDbl(txtBalCts.Text) = 0 Then
                                txtIndex.Text = ""
                                txtAssortment.Text = ""
                                txtPcs.Text = ""
                                txtCts.Text = ""
                                txtBalPcs.Text = ""
                                txtBalCts.Text = ""
                                txtPrice.Text = ""
                                dblLotID = 0
                                strArticle = ""
                                strRemarks = ""
                                strItemName = ""
                                cmbAssort.Text = ""
                                txtLotID.Text = ""
                                txtOrigin.Text = ""
                                txtImpPrice.Text = ""
                            End If

                            txtInvPcs.Text = CalTotalPcs(flxSelect)
                            txtInvCts.Text = CalTotalCts(flxSelect)

                            txtSelPcs.Text = ""
                            txtSelCts.Text = ""
                            cmbAssort.Text = ""
                            txtLotID.Text = ""
                            txtOrigin.Text = ""
                            txtImpPrice.Text = ""
                            txtSelPcs.Focus()
                        Else
                            MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            txtSelCts.Focus()
                        End If
                    Else
                        MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        txtSelPcs.Focus()
                    End If
                Else
                    MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    txtSelCts.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub txtSelPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSelPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtSelPcs.Text <> "" Then
                txtSelCts.Focus()
            End If
        End If
    End Sub

    Private Sub txtSelCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSelCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtSelCts.Text)
        If Asc(e.KeyChar) = 13 Then
            If txtSelCts.Text <> "" Then
                txtLotID.Focus()
            End If
        End If
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(4, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(5, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub Save()

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If ValidateFields() = False Then Exit Sub

            'rsComSql = New ADODB.Recordset
            'rsComSql.Open("SELECT * FROM tblImportUpload WHERE SupplierRefNo = '" & txtSupRefNo.Text & "'", AdoCN, 1, 1)
            'If rsComSql.RecordCount > 0 Then
            '    MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '    Exit Sub
            'End If
            'rsComSql = Nothing

            'If CDbl(txtTotalPcs.Text) <> CDbl(txtInvPcs.Text) Then
            '    MsgBox("Invalid Total Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '    Exit Sub
            'End If

            'If CDbl(txtTotalCts.Text) <> CDbl(txtInvCts.Text) Then
            '    MsgBox("Invalid Total Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '    Exit Sub
            'End If

            If flxSelect.RowCount = 0 Then
                MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            For I = 0 To flxSelect.Rows.Count - 1
                If flxSelect.Item(2, I).Value = "" Then
                    MsgBox("Invalid Sup Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If flxSelect.Item(3, I).Value = "" Then
                    MsgBox("Invalid DCL Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Next

            For I = 0 To flxSelect.Rows.Count - 1
                mStrSQL = "INSERT INTO tblImportUpload(IndexNo,SupplierRefNo,SupplierCode,ParcelType,AssortmentNo,SupParcelNo,DCLParcelNo,INVPcs,INVCts,ItemSize,ItemCost,LotNo,Article,Remarks,CompCode,ItemName,Urgent,NewAssort,HardCost,NewLotNo,Origin,ImpPrice) " & _
                          "VALUES(" & CDbl(flxSelect.Item(0, I).Value) & ",'" & UCase(txtSupRefNo.Text) & "'," & CInt(cmbSupplier.Text) & ",'" & cmbType.Text & "','" & flxSelect.Item(1, I).Value & "','" & flxSelect.Item(2, I).Value & "','" & flxSelect.Item(3, I).Value & "'," & CDbl(flxSelect.Item(4, I).Value) & "," & _
                            "" & CDbl(flxSelect.Item(5, I).Value) & "," & CDbl(flxSelect.Item(6, I).Value) & "," & CDbl(flxSelect.Item(7, I).Value) & "," & CDbl(flxSelect.Item(8, I).Value) & ",'" & flxSelect.Item(9, I).Value & "','" & flxSelect.Item(10, I).Value & "'," & _
                            "'" & cmbCompany.Text & "','" & flxSelect.Item(11, I).Value & "'," & CInt(flxSelect.Item(12, I).Value) & ",'" & flxSelect.Item(13, I).Value & "'," & CDbl(flxSelect.Item(14, I).Value) & "," & CDbl(flxSelect.Item(15, I).Value) & ",'" & flxSelect.Item(16, I).Value & "'," & CDbl(flxSelect.Item(17, I).Value) & ")"

                AdoCN.Execute(mStrSQL)

                mStrSQL = "UPDATE tblImportMain SET OK = 1 WHERE SupplierRefNo = '" & txtSupRefNo.Text & "' AND LotNo = " & CDbl(flxSelect.Item(8, I).Value) & ""

                AdoCN.Execute(mStrSQL)
            Next

            MsgBox("Import Upload Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If

    End Sub

    Private Sub SaveMain()
        Dim blnNew As Boolean

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If ValidateFields() = False Then Exit Sub

            blnNew = True
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblImportMain WHERE SupplierRefNo = '" & txtSupRefNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount > 0 Then
                blnNew = False
                PBResponse = MsgBox("Do you want to Update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
                If PBResponse = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If
            rsComSql = Nothing

            If flxDept.RowCount = 0 Then
                MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            For I = 0 To flxDept.Rows.Count - 1
                If blnNew = True Then
                    mStrSQL = "INSERT INTO tblImportMain(IndexNo,InvoiceDate,SupplierRefNo,SupplierCode,ParcelType,AssortmentNo,INVPcs,INVCts,ItemSize,ItemCost,LotNo,Article,Remarks,CompCode,ItemName,Urgent,Origin,ImpPrice) " & _
                              "VALUES(" & CDbl(flxDept.Item(0, I).Value) & ",'" & Format(dtpInvDate.Value, "MM/dd/yyyy") & "','" & UCase(txtSupRefNo.Text) & "'," & CInt(cmbSupplier.Text) & ",'" & cmbType.Text & "','" & flxDept.Item(1, I).Value & "'," & CDbl(flxDept.Item(4, I).Value) & "," & _
                                "" & CDbl(flxDept.Item(5, I).Value) & "," & CDbl(flxDept.Item(6, I).Value) & "," & CDbl(flxDept.Item(7, I).Value) & "," & CDbl(flxDept.Item(8, I).Value) & ",'" & flxDept.Item(9, I).Value & "','" & flxDept.Item(10, I).Value & "'," & _
                                "'" & cmbCompany.Text & "','" & flxDept.Item(11, I).Value & "'," & CInt(flxDept.Item(12, I).Value) & ",'" & flxDept.Item(14, I).Value & "'," & CDbl(flxDept.Item(15, I).Value) & ")"
                Else
                    mStrSQL = "UPDATE tblImportMain SET INVPcs = " & CDbl(flxDept.Item(4, I).Value) & " WHERE ID = " & CDbl(flxDept.Item(13, I).Value) & ""
                End If

                AdoCN.Execute(mStrSQL)
            Next

            If blnNew = True Then
                MsgBox("Import Main Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Else
                MsgBox("Import Main Updated Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If

            ClearFields()
        End If

    End Sub

    Private Sub Process()
        Dim strLetter, strAssortment, strSupParNo, strDCLParNo As String
        Dim dblIndexNo As Double
        Dim dblImpValue As Double
        Dim dblTotalValue As Double
        'Dim dblPerc As Double
        'Dim dblNewPrice As Double

        If ValidateFields() = False Then Exit Sub

        'If CDbl(txtTotalPcs.Text) <> CDbl(txtInvPcs.Text) Then
        '    MsgBox("Invalid Total Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '    Exit Sub
        'End If

        'If CDbl(txtTotalCts.Text) <> CDbl(txtInvCts.Text) Then
        '    MsgBox("Invalid Total Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '    Exit Sub
        'End If

        If flxSelect.RowCount = 0 Then
            MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        strLetter = ""
        strAssortment = ""
        If cmbCompany.Text = "DCL" Then
            If cmbSupplier.Text = "00001" Then
                If cmbType.Text = "Rough" Then
                    strLetter = "D"
                Else
                    strLetter = "P"
                End If
            Else
                If cmbType.Text = "Rough" Then
                    strLetter = "J"
                Else
                    strLetter = "R"
                End If
            End If
        ElseIf cmbCompany.Text = "CJM" Then
            If cmbType.Text = "Rough" Then
                strLetter = "L"
            Else
                strLetter = "P"
            End If
        Else
            If cmbSupplier.Text = "00001" Then
                If cmbType.Text = "Rough" Then
                    strLetter = "N"
                Else
                    strLetter = "Q"
                End If
            Else
                If cmbType.Text = "Rough" Then
                    strLetter = "M"
                Else
                    strLetter = "S"
                End If
            End If
        End If

        dblIndexNo = 0
        dblImpValue = 0
        dblTotalValue = 0
        If cmbType.Text = "Rough" Then
            rsComSql = New ADODB.Recordset
            'rsComSql.Open("SELECT MAX(CONVERT(numeric, RIGHT(SupParcelNo, 4))) AS MaxNo " & _
            '              "FROM dbo.tblImport " & _
            '              "WHERE (Department = N'Rough Dept') AND (YEAR(InvoiceDate) = '" & dtpInvDate.Value.Year & "') AND " & _
            '                    "(LEFT(SupParcelNo, 1) = '" & strLetter & "')", AdoCN, 1, 1)
            rsComSql.Open("SELECT MAX(CONVERT(numeric, RIGHT(SupParcelNo, 4))) AS MaxNo " & _
                          "FROM dbo.tblImport " & _
                          "WHERE (Department = N'Rough Dept') AND (YEAR(InvoiceDate) >= 2016) AND " & _
                                "(LEFT(SupParcelNo, 1) = '" & strLetter & "')", AdoCN, 1, 1)
            If IsDBNull(rsComSql.Fields("MaxNo").Value) Then
                dblIndexNo = 1
            Else
                dblIndexNo = rsComSql.Fields("MaxNo").Value + 1
            End If
            rsComSql = Nothing
        Else
            AdoCN.Execute("DELETE FROM tblImportCode")
            For intRow = 0 To flxSelect.Rows.Count - 1
                If Trim(flxSelect.Item(0, intRow).Value) <> "" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblImportCode WHERE Letter = '" & Mid(Trim(flxSelect.Item(1, intRow).Value), 1, 1) & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount = 0 Then
                        strLetter = Mid(Trim(flxSelect.Item(1, intRow).Value), 1, 1)
                        rsComSql = New ADODB.Recordset
                        'rsComSql.Open("SELECT MAX(CONVERT(numeric, RIGHT(SupParcelNo, 5))) AS MaxNo " & _
                        '              "FROM dbo.tblImport " & _
                        '              "WHERE (Department = N'Rough Dept') AND (YEAR(InvoiceDate) = '" & dtpInvDate.Value.Year & "') AND " & _
                        '                    "(LEFT(SupParcelNo, 1) = '" & strLetter & "')", AdoCN, 1, 1)
                        rsComSql.Open("SELECT MAX(CONVERT(numeric, RIGHT(SupParcelNo, 5))) AS MaxNo " & _
                                      "FROM dbo.tblImport " & _
                                      "WHERE (Department = N'Rough Dept') AND (YEAR(InvoiceDate) >= 2016) AND " & _
                                            "(LEFT(SupParcelNo, 1) = '" & strLetter & "')", AdoCN, 1, 1)
                        If IsDBNull(rsComSql.Fields("MaxNo").Value) Then
                            dblIndexNo = 1
                        Else
                            dblIndexNo = rsComSql.Fields("MaxNo").Value + 1
                        End If
                        rsComSql = Nothing
                        AdoCN.Execute("INSERT INTO tblImportCode(Letter,MaxNo) VALUES('" & Mid(Trim(flxSelect.Item(1, intRow).Value), 1, 1) & "'," & dblIndexNo & ")")
                    End If
                    rsComSql_1 = Nothing
                End If
            Next
        End If

        For intRow = 0 To flxSelect.Rows.Count - 1
            dblImpValue = dblImpValue + (CDbl(Trim(flxSelect.Item(5, intRow).Value)) * CDbl(Trim(flxSelect.Item(7, intRow).Value)))
            dblTotalValue = dblTotalValue + (CDbl(Trim(flxSelect.Item(5, intRow).Value)) * CDbl(Trim(flxSelect.Item(14, intRow).Value)))
        Next

        For intRow = 0 To flxSelect.Rows.Count - 1
            strAssortment = Trim(flxSelect.Item(1, intRow).Value)
            If cmbType.Text = "Rough" Then
                strSupParNo = strLetter & strRight(dtpInvDate.Value.Year, 1) & Format(dtpInvDate.Value.Month, "00") & Format(dblIndexNo, "0000")
                strDCLParNo = Mid(strSupParNo, 1, 1) & "S" & Format(dblIndexNo, "0000")
                dblIndexNo = dblIndexNo + 1
            Else
                strLetter = Mid(strAssortment, 1, 1)
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM dbo.tblImportCode WHERE (Letter = '" & strLetter & "')", AdoCN, 1, 1)
                dblIndexNo = rsComSql.Fields("MaxNo").Value
                rsComSql = Nothing
                AdoCN.Execute("UPDATE tblImportCode SET MaxNo = MaxNo + 1 WHERE (Letter = '" & strLetter & "')")
                strSupParNo = strLetter & strRight(dtpInvDate.Value.Year, 1) & Format(dblIndexNo, "00000")
                strDCLParNo = strSupParNo
                dblIndexNo = dblIndexNo + 1
            End If

            flxSelect.Item(2, intRow).Value = strSupParNo
            flxSelect.Item(3, intRow).Value = strDCLParNo

            'dblPerc = (CDbl(Trim(flxSelect.Item(5, intRow).Value)) * CDbl(Trim(flxSelect.Item(14, intRow).Value))) / dblTotalValue
            'dblNewPrice = (dblImpValue * dblPerc) / (CDbl(Trim(flxSelect.Item(5, intRow).Value)))
            'dblNewPrice = Math.Round(dblNewPrice, 2)

            'flxSelect.Item(14, intRow).Value = dblNewPrice
        Next
    End Sub

    Private Sub cmdProcess_Click(sender As Object, e As EventArgs) Handles cmdProcess.Click
        Process()
    End Sub

    Private Sub txtPcs_MouseClick(sender As Object, e As MouseEventArgs) Handles txtPcs.MouseClick
        txtSelPcs.Text = txtPcs.Text
    End Sub

    Private Sub txtCts_MouseClick(sender As Object, e As MouseEventArgs) Handles txtCts.MouseClick
        txtSelCts.Text = txtCts.Text
    End Sub

    Private Sub txtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrice.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtPrice.Text)
        If Asc(e.KeyChar) = 13 Then
            If txtPrice.Text <> "" Then
                txtSelPcs.Focus()
            End If
        End If
    End Sub

    Private Sub txtSupRefNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSupRefNo.KeyPress
        Dim dblTotPcs As Double
        Dim dblTotCts As Double
        Dim strSupRefNo As String

        If Asc(e.KeyChar) = 13 Then
            strSupRefNo = txtSupRefNo.Text
            ClearFields()

            txtSupRefNo.Text = strSupRefNo

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblImportMain WHERE SupplierRefNo = '" & txtSupRefNo.Text & "' AND OK = 0 ORDER BY ID", AdoCN, 1, 1)
            If rsComSql.RecordCount > 0 Then
                rsComSql.MoveFirst()

                dtpInvDate.Value = rsComSql.Fields("InvoiceDate").Value
                cmbCompany.Text = rsComSql.Fields("CompCode").Value
                cmbType.Text = rsComSql.Fields("ParcelType").Value
                cmbSupplier.SelectedText = Format(rsComSql.Fields("SupplierCode").Value, "00000")
                cmbSupplier.Text = Format(rsComSql.Fields("SupplierCode").Value, "00000")

                While Not rsComSql.EOF
                    flxDept.Rows.Add(rsComSql.Fields("IndexNo").Value,
                                     rsComSql.Fields("AssortmentNo").Value,
                                     "",
                                     "",
                                     rsComSql.Fields("INVPcs").Value,
                                     rsComSql.Fields("INVCts").Value,
                                     Math.Round(rsComSql.Fields("ItemSize").Value, 2),
                                     rsComSql.Fields("ItemCost").Value,
                                     rsComSql.Fields("LotNo").Value,
                                     rsComSql.Fields("Article").Value,
                                     rsComSql.Fields("Remarks").Value,
                                     rsComSql.Fields("ItemName").Value,
                                     rsComSql.Fields("Urgent").Value,
                                     rsComSql.Fields("ID").Value,
                                     rsComSql.Fields("Origin").Value,
                                     rsComSql.Fields("ImpPrice").Value)

                    dblTotPcs = dblTotPcs + rsComSql.Fields("INVPcs").Value
                    dblTotCts = dblTotCts + rsComSql.Fields("INVCts").Value
                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing
        End If

        txtTotalPcs.Text = dblTotPcs
        txtTotalCts.Text = Math.Round(dblTotCts, 3)
    End Sub

    Private Sub cmdSaveMain_Click(sender As Object, e As EventArgs) Handles cmdSaveMain.Click
        SaveMain()
    End Sub

    Private Sub txtLotID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLotID.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtLotID.Text <> "" Then
                cmdAdd.Focus()
            End If
        End If
    End Sub

End Class