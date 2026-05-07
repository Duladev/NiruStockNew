Public Class frm_GRDRnd_Niru_Invoice

    ' ── FORM LOAD ───────────────────────────────────────────────────
    Private Sub frmNiru_Invoice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.StartPosition = FormStartPosition.CenterScreen
            SetupGrid()
            Load_Hotel_Name()
            Load_Invoices()
            ClearAll()
        Catch ex As Exception
            MsgBox("Error in Form_Load : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    ' ── SETUP GRID ──────────────────────────────────────────────────
    Private Sub SetupGrid()
        flxInvoice.Columns.Clear()
        flxInvoice.AutoGenerateColumns = False
        flxInvoice.AllowUserToAddRows = False
        flxInvoice.AllowUserToDeleteRows = False
        flxInvoice.ReadOnly = False
        flxInvoice.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        flxInvoice.BackgroundColor = System.Drawing.Color.White
        flxInvoice.Font = New System.Drawing.Font("Trebuchet MS", 8.25)

        ' ✅ MUST be before BackColor assignment
        flxInvoice.EnableHeadersVisualStyles = False
        flxInvoice.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.SteelBlue
        flxInvoice.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White
        flxInvoice.ColumnHeadersDefaultCellStyle.Font =
            New System.Drawing.Font("Trebuchet MS", 8.25, FontStyle.Bold)
        flxInvoice.ColumnHeadersHeight = 24
        flxInvoice.RowTemplate.Height = 20

        ' Name, Header, Width, ReadOnly, Align
        Dim cols(,) As Object = {
            {"Col0", "Index", 45, True, "R"},
            {"Col1", "Rgh Price", 85, False, "R"},
            {"Col2", "Rgh Pcs", 65, False, "R"},
            {"Col3", "Rgh Cts", 70, False, "R"},
            {"Col4", "Labour", 65, False, "R"},
            {"Col5", "Pol Price", 85, True, "R"},
            {"Col6", "Pol Pcs", 65, False, "R"},
            {"Col7", "Pol Cts", 70, False, "R"},
            {"Col8", "Parcel No", 100, True, "L"},
            {"Col9", "Assortment No", 110, True, "L"},
            {"Col10", "Size", 55, True, "L"},
            {"Col11", "Checked", 60, True, "C"},
            {"Col12", "Export", 55, True, "C"},
            {"Col13", "Act Pcs", 65, False, "R"},
            {"Col14", "Act Cts", 70, False, "R"},
            {"Col15", "Description", 100, True, "L"},
            {"Col16", "Parcel ID", 80, True, "L"},
            {"Col17", "OSPONo", 75, True, "L"},
            {"Col18", "Sup Par No", 80, True, "L"},
            {"Col19", "Repair", 55, False, "C"},
            {"Col20", "Lot ID", 70, False, "R"},
            {"Col21", "ID", 50, True, "R"},
            {"Col22", "Remarks", 110, False, "L"}
        }

        For i As Integer = 0 To cols.GetUpperBound(0)
            Dim col As New DataGridViewTextBoxColumn()
            col.Name = CStr(cols(i, 0))
            col.HeaderText = CStr(cols(i, 1))
            col.Width = CInt(cols(i, 2))
            col.ReadOnly = CBool(cols(i, 3))
            col.SortMode = DataGridViewColumnSortMode.NotSortable

            Select Case CStr(cols(i, 4))
                Case "R" : col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                Case "C" : col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                Case Else : col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End Select

            ' Tint editable columns
            If Not CBool(cols(i, 3)) Then
                col.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(224, 255, 255)
            End If

            flxInvoice.Columns.Add(col)
        Next
    End Sub

    ' ── LOAD COMPANIES ──────────────────────────────────────────────
    Private Sub Load_Hotel_Name()
        Dim rs As New ADODB.Recordset
        Try
            cmbHotel.Items.Clear()
            rs.Open("SELECT WAN_CODE, WAN_NAME FROM tblGrading_RndWAN_LOCA ORDER BY WAN_NAME",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

            Do While Not rs.EOF
                Dim code As String = rs.Fields("WAN_CODE").Value.ToString().Trim()
                Dim name As String = rs.Fields("WAN_NAME").Value.ToString().Trim()
                If Prod_WK_ID = "XX" Then
                    cmbHotel.Items.Add(New CompanyItem(code, name))
                ElseIf Prod_WK_ID = code Then
                    Dim item As New CompanyItem(code, name)
                    cmbHotel.Items.Add(item)
                    cmbHotel.SelectedItem = item
                End If
                rs.MoveNext()
            Loop

            If cmbHotel.Items.Count = 0 Then
                If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
                rs.Open("SELECT WAN_CODE, WAN_NAME FROM tblGrading_RndWAN_LOCA ORDER BY WAN_NAME",
                        AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
                Do While Not rs.EOF
                    cmbHotel.Items.Add(New CompanyItem(
                        rs.Fields("WAN_CODE").Value.ToString().Trim(),
                        rs.Fields("WAN_NAME").Value.ToString().Trim()))
                    rs.MoveNext()
                Loop
            End If

        Catch ex As Exception
            MsgBox("Error in Load_Hotel_Name : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub

    ' ✅ Added Handles clause
    Private Sub cmbHotel_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles cmbHotel.SelectedIndexChanged
        If cmbHotel.SelectedItem IsNot Nothing Then
            PBDepartment = CType(cmbHotel.SelectedItem, CompanyItem).Code
        End If
    End Sub

    ' ── LOAD INVOICE NUMBERS ─────────────────────────────────────────
    Private Sub Load_Invoices()
        Dim rs As New ADODB.Recordset
        Try
            ' ✅ Detach event while filling to prevent premature load trigger
            RemoveHandler cmbInvoice.SelectedIndexChanged,
                          AddressOf cmbInvoice_SelectedIndexChanged

            cmbInvoice.Items.Clear()
            rs.Open("SELECT InvoiceNo FROM tblGrading_RndInvoice " &
                    "WHERE InvoiceNo IS NOT NULL " &
                    "GROUP BY InvoiceNo ORDER BY InvoiceNo",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            Do While Not rs.EOF
                cmbInvoice.Items.Add(rs.Fields("InvoiceNo").Value.ToString().Trim())
                rs.MoveNext()
            Loop

        Catch ex As Exception
            MsgBox("Error in Load_Invoices : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
            ' ✅ Re-attach event after filling
            AddHandler cmbInvoice.SelectedIndexChanged,
                       AddressOf cmbInvoice_SelectedIndexChanged
        End Try
    End Sub

    '  INVOICE SELECTED 
    Private Sub cmbInvoice_SelectedIndexChanged(sender As Object, e As EventArgs)
        If cmbInvoice.SelectedItem IsNot Nothing Then
            Load_Invoice_Details(cmbInvoice.SelectedItem.ToString())
        End If
    End Sub

    '  LOAD INVOICE DETAILS 
    Private Sub Load_Invoice_Details(invoiceNo As String)
        Dim rs As New ADODB.Recordset
        Try
            flxInvoice.Rows.Clear()

            Dim invQ As String = invoiceNo.Replace("'", "''")
            rs.Open("SELECT * FROM tblGrading_RndInvoice " &
                    "WHERE InvoiceNo='" & invQ & "' ORDER BY ParcelNo, AssortNo",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

            If rs.EOF Then
                MessageBox.Show("No records found for Invoice: " & invoiceNo,
                                Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim intCount As Integer = 1
            Do While Not rs.EOF
                Try
                    Dim rowData(22) As Object
                    rowData(0) = intCount.ToString()
                    rowData(1) = Format(ToDouble(rs.Fields("RghPrice").Value), "###,##0.00")
                    rowData(2) = SafeStr(rs.Fields("RghPcs").Value)
                    rowData(3) = Format(ToDouble(rs.Fields("RhgCts").Value), "#0.000")
                    rowData(4) = SafeStr(rs.Fields("Labour").Value)
                    rowData(5) = Format(ToDouble(rs.Fields("PolPrice").Value), "###,##0.00")
                    rowData(6) = SafeStr(rs.Fields("PolPcs").Value)
                    rowData(7) = Format(ToDouble(rs.Fields("PolCts").Value), "#0.000")
                    rowData(8) = SafeStr(rs.Fields("ParcelNo").Value)
                    rowData(9) = SafeStr(rs.Fields("AssortNo").Value)
                    rowData(10) = SafeStr(rs.Fields("Size1").Value)
                    rowData(11) = SafeStr(rs.Fields("Checked").Value)
                    rowData(12) = SafeStr(rs.Fields("Export").Value)
                    rowData(13) = SafeStr(rs.Fields("ActPcs").Value)
                    rowData(14) = Format(ToDouble(rs.Fields("ActCts").Value), "#0.000")
                    rowData(15) = SafeStr(rs.Fields("Description").Value)
                    rowData(16) = SafeStr(rs.Fields("ParcelID").Value)
                    rowData(17) = SafeStr(rs.Fields("OSPONo").Value)
                    rowData(18) = SafeStr(rs.Fields("SupParNo").Value)
                    rowData(19) = If(SafeStr(rs.Fields("Repair").Value) = "True", "1", "0")
                    rowData(20) = Format(ToDouble(rs.Fields("LotID").Value), "0")
                    rowData(21) = SafeStr(rs.Fields("ID").Value)
                    rowData(22) = SafeStr(rs.Fields("Remarks").Value)

                    flxInvoice.Rows.Add(rowData)
                    intCount += 1

                Catch exRow As Exception
                    ' Log bad row but continue loading rest
                    Debug.Print("Row " & intCount & " skipped: " & exRow.Message)
                End Try

                rs.MoveNext()
            Loop

        Catch ex As Exception
            MsgBox("Error in Load_Invoice_Details : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub

    ' SAVE 
    Private Sub cmdSave_Click_1(sender As Object, e As EventArgs) Handles cmdSave.Click
        Dim rsCheck As New ADODB.Recordset
        Dim rsLog As New ADODB.Recordset
        Dim cmd As New ADODB.Command
        Try
            If cmbInvoice.SelectedItem Is Nothing Then
                MessageBox.Show("Please select an Invoice", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            Dim invoiceNo As String = cmbInvoice.SelectedItem.ToString()
            Dim invQ As String = invoiceNo.Replace("'", "''")

            rsCheck.Open("SELECT COUNT(*) AS Cnt FROM tblGrading_RndInvoice " &
                         "WHERE InvoiceNo='" & invQ & "'",
                         AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            Dim invExists As Boolean =
                Not rsCheck.EOF AndAlso (CInt(rsCheck.Fields("Cnt").Value) > 0)
            If rsCheck.State = ADODB.ObjectStateEnum.adStateOpen Then rsCheck.Close()

            If Not invExists Then
                MessageBox.Show("Invalid Invoice No.", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            If MessageBox.Show("Invoice already exists. Do you want to update?",
                               Me.Text, MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question) = DialogResult.No Then Return

            cmd.ActiveConnection = AdoCN

            For Each row As DataGridViewRow In flxInvoice.Rows
                Dim id As String = row.Cells("Col21").Value?.ToString().Trim()
                If String.IsNullOrEmpty(id) Then Continue For

                rsLog.Open("SELECT * FROM tblGrading_RndInvoice WHERE ID=" & CDbl(id),
                           AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
                If Not rsLog.EOF Then
                    If ToDouble(row.Cells("Col1").Value) <> ToDouble(rsLog.Fields("RghPrice").Value) Then Log_book("Invoice", "Update", invoiceNo, "Amended Rgh Price")
                    If ToDouble(row.Cells("Col2").Value) <> ToDouble(rsLog.Fields("RghPcs").Value) Then Log_book("Invoice", "Update", invoiceNo, "Amended Rgh Pcs")
                    If ToDouble(row.Cells("Col3").Value) <> ToDouble(rsLog.Fields("RhgCts").Value) Then Log_book("Invoice", "Update", invoiceNo, "Amended Rgh Cts")
                    If ToDouble(row.Cells("Col6").Value) <> ToDouble(rsLog.Fields("PolPcs").Value) Then Log_book("Invoice", "Update", invoiceNo, "Amended Pol Pcs")
                    If ToDouble(row.Cells("Col7").Value) <> ToDouble(rsLog.Fields("PolCts").Value) Then Log_book("Invoice", "Update", invoiceNo, "Amended Pol Cts")
                End If
                If rsLog.State = ADODB.ObjectStateEnum.adStateOpen Then rsLog.Close()

                Dim remarksQ As String =
                    row.Cells("Col22").Value?.ToString().Trim().Replace("'", "''")

                cmd.CommandText =
                    "UPDATE tblGrading_RndInvoice SET " &
                    "RghPrice=" & ToDouble(row.Cells("Col1").Value) & ", " &
                    "RghPcs=" & ToDouble(row.Cells("Col2").Value) & ", " &
                    "RhgCts=" & ToDouble(row.Cells("Col3").Value) & ", " &
                    "PolPcs=" & ToDouble(row.Cells("Col6").Value) & ", " &
                    "PolCts=" & ToDouble(row.Cells("Col7").Value) & ", " &
                    "Remarks='" & remarksQ & "', " &
                    "LotID=" & ToDouble(row.Cells("Col20").Value) & " " &
                    "WHERE ID=" & CDbl(id)
                cmd.Execute()
            Next

            MessageBox.Show("Updated Successfully", Me.Text,
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearAll()

        Catch ex As Exception
            MsgBox("Save error : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rsCheck.State = ADODB.ObjectStateEnum.adStateOpen Then rsCheck.Close()
            If rsLog.State = ADODB.ObjectStateEnum.adStateOpen Then rsLog.Close()
            rsCheck = Nothing : rsLog = Nothing : cmd = Nothing
        End Try
    End Sub

    '  CLEAR 
    Private Sub ClearAll()
        RemoveHandler cmbInvoice.SelectedIndexChanged,
                      AddressOf cmbInvoice_SelectedIndexChanged
        cmbInvoice.SelectedIndex = -1
        AddHandler cmbInvoice.SelectedIndexChanged,
                   AddressOf cmbInvoice_SelectedIndexChanged

        flxInvoice.Rows.Clear()
    End Sub

    '  HELPERS 
    Private Function SafeStr(val As Object) As String
        If val Is Nothing OrElse IsDBNull(val) Then Return ""
        Return val.ToString().Trim()
    End Function

    Private Function ToDouble(val As Object) As Double
        If val Is Nothing OrElse IsDBNull(val) Then Return 0
        Dim s As String = val.ToString().Replace(",", "").Trim()
        If IsNumeric(s) Then Return Convert.ToDouble(s)
        Return 0
    End Function

    '  TOOLBAR 
    Private Sub cmdNew_Click_1(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearAll()
    End Sub

    Private Sub cmdExit_Click_1(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub HazelDev_ThemeContainer1_Click(sender As Object, e As EventArgs) _
        Handles HazelDev_ThemeContainer1.Click
    End Sub

    Private Sub grpInvoices_Enter(sender As Object, e As EventArgs) Handles grpInvoices.Enter
    End Sub

End Class