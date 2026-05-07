
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_DCLImportOriginal

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

    Private Sub Load_Origin()
        cmbOrigin.Items.Clear()
        rsComSql = New ADODB.Recordset
        mStrSQL = "SELECT Origin FROM tblDCLOrigin ORDER BY Origin"
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        Do While Not rsComSql.EOF
            cmbOrigin.Items.Add(rsComSql.Fields("Origin").Value)
            rsComSql.MoveNext()
        Loop
        rsComSql = Nothing
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ClearFields()
        txtSupRefNo.Text = ""
        cmbCategory.Text = ""
        cmbCompany.Text = ""
        cmbType.Text = ""
        cmbSupplier.Text = ""
        txtSupplier.Text = ""
        dtpInvDate.Value = Date.Now
        dtpRecDate.Value = Date.Now
        txtShip.Text = "0"
        txtComm.Text = "0"
        txtCommValue.Text = "0"
        txtBroker.Text = "0"
        txtBrokValue.Text = "0"
        txtInterest.Text = "0"
        txtTotalPcs.Text = "0"
        txtTotalCts.Text = "0"
        txtValue.Text = "0"
        txtFinalValue.Text = "0"
        txtDays.Text = "0"
        flxDept.Rows.Clear()
        txtSupRefNo.Focus()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Function ValidateFields() As Boolean
        ValidateFields = True

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

        If Not Len(Trim(cmbCategory.Text)) > 0 Then
            MsgBox("Please enter the Category", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(txtShip.Text)) > 0 Then
            MsgBox("Please enter the Shipping Cost", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(txtComm.Text)) > 0 Then
            MsgBox("Please enter the Commission Rate", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(txtBroker.Text)) > 0 Then
            MsgBox("Please enter the Brokerage Rate", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        For I = 0 To flxDept.Rows.Count - 1
            If Len(Trim(flxDept.Item(7, I).Value)) = 0 Then
                MsgBox("Invalid Origin", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ValidateFields = False
                Exit Function
            End If
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblDCLOrigin WHERE Origin = '" & Trim(flxDept.Item(7, I).Value) & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Origin", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ValidateFields = False
                Exit Function
            End If
            rsComSql = Nothing
            If Len(Trim(flxDept.Item(4, I).Value)) = 0 Then
                MsgBox("Invalid Import Price", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ValidateFields = False
                Exit Function
            End If
        Next

        Return ValidateFields
    End Function

    Private Sub Save()
        On Error GoTo ErrorHandler
        Dim I As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If ValidateFields() = False Then Exit Sub

            If flxDept.RowCount = 0 Then
                MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            Process()

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SupplierRefNo FROM tblImportOriginal WHERE SupplierRefNo = '" & UCase(txtSupRefNo.Text) & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                For I = 0 To flxDept.Rows.Count - 1
                    mStrSQL = "INSERT INTO tblImportOriginal(IndexNo, SupplierRefNo, InvoiceDate, RecievedDate, SupplierCode, ParcelType, AssortmentNo, INVPcs, INVCts, ItemCost, LotNo, Remarks, CompCode, Origin, " & _
                                "NewPrice, Shipping, Commission, Brokerage, Category, Days, Interest) " & _
                              "VALUES('" & CInt(flxDept.Item(0, I).Value) & "','" & UCase(txtSupRefNo.Text) & "','" & Format(dtpInvDate.Value, "MM/dd/yyyy") & "','" & Format(dtpRecDate.Value, "MM/dd/yyyy") & "'," & _
                                "" & CInt(cmbSupplier.Text) & ",'" & cmbType.Text & "','" & flxDept.Item(1, I).Value & "'," & CDbl(flxDept.Item(2, I).Value) & "," & CDbl(flxDept.Item(3, I).Value) & "," & _
                                "" & CDbl(flxDept.Item(4, I).Value) & "," & CDbl(flxDept.Item(5, I).Value) & ",'" & flxDept.Item(6, I).Value & "','" & cmbCompany.Text & "','" & flxDept.Item(7, I).Value & "'," & CDbl(flxDept.Item(9, I).Value) & "," & _
                                "'" & CDbl(txtShip.Text) & "','" & CDbl(txtComm.Text) & "','" & CDbl(txtBroker.Text) & "','" & cmbCategory.Text & "','" & CInt(txtDays.Text) & "','" & CDbl(txtInterest.Text) & "')"

                    AdoCN.Execute(mStrSQL)

                Next

                MsgBox("Import Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

                ClearFields()
            Else
                PBResponse = MsgBox("Already Entered. Do you want to Update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
                If PBResponse = MsgBoxResult.Yes Then
                    AdoCN.Execute("DELETE FROM tblImportOriginal WHERE SupplierRefNo = '" & UCase(txtSupRefNo.Text) & "'")

                    For I = 0 To flxDept.Rows.Count - 1
                        mStrSQL = "INSERT INTO tblImportOriginal(IndexNo, SupplierRefNo, InvoiceDate, RecievedDate, SupplierCode, ParcelType, AssortmentNo, INVPcs, INVCts, ItemCost, LotNo, Remarks, CompCode, Origin, " & _
                                    "NewPrice, Shipping, Commission, Brokerage, Category, Days, Interest) " & _
                                  "VALUES('" & flxDept.Item(0, I).Value & "','" & UCase(txtSupRefNo.Text) & "','" & Format(dtpInvDate.Value, "MM/dd/yyyy") & "','" & Format(dtpRecDate.Value, "MM/dd/yyyy") & "'," & _
                                    "" & CInt(cmbSupplier.Text) & ",'" & cmbType.Text & "','" & flxDept.Item(1, I).Value & "'," & CDbl(flxDept.Item(2, I).Value) & "," & CDbl(flxDept.Item(3, I).Value) & "," & _
                                    "" & CDbl(flxDept.Item(4, I).Value) & "," & CDbl(flxDept.Item(5, I).Value) & ",'" & flxDept.Item(6, I).Value & "','" & cmbCompany.Text & "','" & flxDept.Item(7, I).Value & "'," & CDbl(flxDept.Item(9, I).Value) & "," & _
                                    "'" & CDbl(txtShip.Text) & "','" & CDbl(txtComm.Text) & "','" & CDbl(txtBroker.Text) & "','" & cmbCategory.Text & "','" & CInt(txtDays.Text) & "','" & CDbl(txtInterest.Text) & "')"

                        AdoCN.Execute(mStrSQL)

                    Next

                    MsgBox("Import Updated Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

                    ClearFields()
                End If
            End If
            rsComSql = Nothing
        End If

        Exit Sub
ErrorHandler:
        MsgBox(Err.Description, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmbSupplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSupplier.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbCategory.Focus()
        End If
    End Sub

    Private Sub cmbSupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSupplier.SelectedIndexChanged
        rsComSql_2 = New ADODB.Recordset
        rsComSql_2.Open("SELECT * FROM tblSuppliers WHERE SupplierCode = " & CInt(cmbSupplier.Text) & "", AdoCN, 1, 1)
        If rsComSql_2.RecordCount Then
            txtSupplier.Text = rsComSql_2.Fields("CompanyName").Value
        Else
            txtSupplier.Text = ""
        End If
        rsComSql_2 = Nothing
    End Sub

    Private Sub cmdReport_Click(sender As Object, e As EventArgs) Handles cmdReport.Click
        mReportName = "crptDCLImportOriginal.rpt"
        objForm = New frm_DCLReportViewer
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.WindowState = FormWindowState.Maximized
        objForm.Show()
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim dblPrice As Double

        If txtLotID.Text = "" Then MsgBox("Invalid Lot ID", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        If txtItemName.Text = "" Then MsgBox("Invalid Item Name", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        If txtPcs.Text = "" Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        If CDbl(txtPcs.Text) <= 0 Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        If txtCts.Text = "" Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        If CDbl(txtCts.Text) <= 0 Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        If txtImpPrice.Text = "" Then MsgBox("Invalid Price", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        If CDbl(txtImpPrice.Text) <= 0 Then MsgBox("Invalid Price", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        If cmbOrigin.Text = "" Then MsgBox("Invalid Origin", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        dblPrice = CDbl(txtImpPrice.Text)

        flxDept.Rows.Add(flxDept.Rows.Count + 1,
                         txtItemName.Text,
                         txtPcs.Text,
                         txtCts.Text,
                         dblPrice,
                         txtLotID.Text,
                         txtRemarks.Text,
                         cmbOrigin.Text,
                         Math.Round(CDbl(txtCts.Text) * CDbl(txtImpPrice.Text), 2),
                         txtImpPrice.Text)


        txtLotID.Text = ""
        txtItemName.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtImpPrice.Text = ""
        cmbOrigin.Text = ""

        txtTotalPcs.Text = CalTotalPcs(flxDept)
        txtTotalCts.Text = CalTotalCts(flxDept)
        txtValue.Text = CalTotalValue(flxDept)
        txtFinalValue.Text = txtValue.Text

        txtLotID.Focus()
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
            CalTotalCts = CalTotalCts + CDbl(flxSample.Item(3, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Function CalTotalValue(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalValue = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalValue = CalTotalValue + Val(flxSample.Item(8, intRow).Value)
        Next
        CalTotalValue = Math.Round(CalTotalValue, 2)
    End Function

    Private Sub txtLotID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLotID.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtItemName.Focus()
        End If
    End Sub

    Private Sub txtItemName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtItemName.KeyPress
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

    Private Sub txtCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtImpPrice.Focus()
        End If
    End Sub

    Private Sub txtImpPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtImpPrice.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtImpPrice.Text)
        If Asc(e.KeyChar) = 13 Then
            cmbOrigin.Focus()
        End If
    End Sub

    Private Sub cmbOrigin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbOrigin.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtRemarks.Focus()
        End If
    End Sub

    Private Sub frm_DCLImportOriginal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Company()
        Load_Supplier()
        Load_Origin()
        dtpInvDate.Value = Date.Now
        dtpRecDate.Value = Date.Now

        ClearFields()
    End Sub

    Private Sub txtSupRefNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSupRefNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtSupRefNo.Text = UCase(txtSupRefNo.Text)
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblImportOriginal WHERE SupplierRefNo = '" & UCase(txtSupRefNo.Text) & "' ORDER BY IndexNo", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                cmbSupplier.Text = Format(rsComSql.Fields("SupplierCode").Value, "00000")
                cmbCategory.Text = rsComSql.Fields("Category").Value
                cmbCompany.Text = rsComSql.Fields("CompCode").Value
                cmbType.Text = rsComSql.Fields("ParcelType").Value

                txtShip.Text = rsComSql.Fields("Shipping").Value
                txtComm.Text = rsComSql.Fields("Commission").Value
                txtBroker.Text = rsComSql.Fields("Brokerage").Value
                txtDays.Text = rsComSql.Fields("Days").Value

                dtpInvDate.Value = rsComSql.Fields("InvoiceDate").Value
                dtpRecDate.Value = rsComSql.Fields("RecievedDate").Value

                While Not rsComSql.EOF
                    flxDept.Rows.Add(rsComSql.Fields("IndexNo").Value,
                                     rsComSql.Fields("AssortmentNo").Value,
                                     rsComSql.Fields("INVPcs").Value,
                                     rsComSql.Fields("INVCts").Value,
                                     rsComSql.Fields("ItemCost").Value,
                                     rsComSql.Fields("LotNo").Value,
                                     rsComSql.Fields("Remarks").Value,
                                     rsComSql.Fields("Origin").Value,
                                     rsComSql.Fields("INVCts").Value * rsComSql.Fields("ItemCost").Value,
                                     rsComSql.Fields("NewPrice").Value)

                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing

            txtTotalPcs.Text = CalTotalPcs(flxDept)
            txtTotalCts.Text = CalTotalCts(flxDept)
            txtValue.Text = CalTotalValue(flxDept)

            Process()

            cmbSupplier.Focus()
        End If
    End Sub

    Private Sub cmbCategory_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbCategory.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbCompany.Focus()
        End If
    End Sub

    Private Sub cmbCompany_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbCompany.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbType.Focus()
        End If
    End Sub

    Private Sub flxDept_DoubleClick(sender As Object, e As EventArgs) Handles flxDept.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxDept.Rows.RemoveAt(flxDept.CurrentRow.Index)

            txtTotalPcs.Text = CalTotalPcs(flxDept)
            txtTotalCts.Text = CalTotalCts(flxDept)
            txtValue.Text = CalTotalValue(flxDept)
        End If
    End Sub

    Private Sub Process()
        Dim I As Integer
        Dim dblTotValue As Double
        Dim dblNewPrice As Double
        Dim dblInterest As Double
        Dim dblMonths As Double

        If txtDays.Text = "" Then MsgBox("Invalid Days", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        If CDbl(txtDays.Text) < 0 Then MsgBox("Invalid Days", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        If txtShip.Text = "" Then MsgBox("Invalid Shipping", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        If txtComm.Text = "" Then MsgBox("Invalid Commission", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        If txtBroker.Text = "" Then MsgBox("Invalid Brokerage", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        If txtValue.Text = "" Then MsgBox("Invalid Value", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        If CDbl(txtValue.Text) <= 0 Then MsgBox("Invalid Value", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        txtCommValue.Text = Math.Round(CDbl(txtValue.Text) * CDbl(txtComm.Text) / 100, 2)
        txtBrokValue.Text = Math.Round(CDbl(txtValue.Text) * CDbl(txtBroker.Text) / 100, 2)
        dblMonths = CDbl(txtDays.Text) / 30
        dblInterest = Math.Round(CDbl(txtValue.Text) * dblMonths / 100, 2)
        txtInterest.Text = dblInterest

        dblTotValue = CDbl(txtValue.Text) + CDbl(txtShip.Text) + CDbl(txtCommValue.Text) + CDbl(txtBrokValue.Text) + dblInterest
        txtFinalValue.Text = dblTotValue
        dblNewPrice = 0
        If dblTotValue > CDbl(txtValue.Text) Then
            For I = 0 To flxDept.Rows.Count - 1
                dblNewPrice = Math.Round(((CDbl(flxDept.Item(8, I).Value) / CDbl(txtValue.Text)) * dblTotValue) / CDbl(flxDept.Item(3, I).Value), 2)
                flxDept.Item(9, I).Value = dblNewPrice
                flxDept.Item(0, I).Value = I + 1
            Next
        End If
    End Sub

    Private Sub cmdProcess_Click(sender As Object, e As EventArgs) Handles cmdProcess.Click
        Process()
    End Sub

    Private Sub txtShip_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtShip.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtShip.Text)
        If Asc(e.KeyChar) = 13 Then
            txtComm.Focus()
        End If
    End Sub

    Private Sub txtComm_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtComm.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtComm.Text)
        If Asc(e.KeyChar) = 13 Then
            txtBroker.Focus()
        End If
    End Sub

    Private Sub txtBroker_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBroker.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtBroker.Text)
    End Sub

    Private Sub txtDays_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDays.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub txtRemarks_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRemarks.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmdAdd.Focus()
        End If
    End Sub
End Class