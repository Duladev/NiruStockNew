Imports System.Text
Imports System.IO

Public Class frm_GRDRnd_ExportSummaryModify

    Private Sub frm_Grading_ExportSummaryModify_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupGrid()
        Load_Supplier()
        Load_Assortments()
        Load_SizeRange()

    End Sub
    ' ── SETUP GRID (no DB — unchanged) ──────────────────────────────
    Private Sub SetupGrid()
        flxDetails.Columns.Clear()
        flxDetails.AutoGenerateColumns = False
        flxDetails.AllowUserToAddRows = False
        flxDetails.AllowUserToDeleteRows = False
        flxDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        flxDetails.BackgroundColor = System.Drawing.Color.White
        flxDetails.Font = New System.Drawing.Font("MS Sans Serif", 8.25)
        flxDetails.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(224, 224, 224)

        Dim chk As New DataGridViewCheckBoxColumn()
        chk.HeaderText = "Sel" : chk.Name = "Selected" : chk.Width = 40
        flxDetails.Columns.Add(chk)

        Dim headers() As String = {
            "Assortment", "Lot No", "Pcs", "Cts", "Price", "Value",
            "Order No", "Pkt No", "Color", "Clarity", "Code", "Size Range",
            "ID", "Pack No", "Pack Type"
        }
        Dim names() As String = {
            "Assortment", "LotNo", "Pcs", "Cts", "Price", "Value",
            "OrderNo", "PktNo", "Color", "Clarity", "Code", "SizeRange",
            "ID", "PackNo", "PackType"
        }
        Dim widths() As Integer = {
            110, 75, 55, 70, 65, 75,
            80, 60, 65, 65, 90, 90,
            50, 70, 75
        }
        Dim editable() As Boolean = {
            False, False, True, True, True, False,
            False, False, False, False, False, False,
            False, True, True
        }

        For idx As Integer = 0 To headers.Length - 1
            Dim col As New DataGridViewTextBoxColumn()
            col.HeaderText = headers(idx)
            col.Name = names(idx)
            col.Width = widths(idx)
            col.ReadOnly = Not editable(idx)
            If idx >= 2 AndAlso idx <= 5 Then
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
            flxDetails.Columns.Add(col)
        Next
    End Sub

    ' ── NUMERIC-ONLY KEY PRESS HELPER ───────────────────────────────
    Public Sub NumericOnly(ByVal e As KeyPressEventArgs, ByVal CurrentText As String)
        If Not (Char.IsDigit(e.KeyChar) OrElse Asc(e.KeyChar) = 8 OrElse Asc(e.KeyChar) = 46) Then
            e.Handled = True
        Else
            If e.KeyChar = "." AndAlso CurrentText.Contains(".") Then
                e.Handled = True
            End If
        End If
    End Sub

    ' ── LOAD SUPPLIERS ──────────────────────────────────────────────
    Private Sub Load_Supplier()
        Dim rs As New ADODB.Recordset
        Try
            cmbSupplier.Items.Clear()
            rs.Open("SELECT SupplierCode, CompanyName FROM tblGrading_RndSuppliers ORDER BY CompanyName",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            Do While Not rs.EOF
                cmbSupplier.Items.Add(New SupplierItem(
                    rs.Fields("SupplierCode").Value.ToString().Trim(),
                    rs.Fields("CompanyName").Value.ToString().Trim()))
                rs.MoveNext()
            Loop
        Catch ex As Exception
            MsgBox("Error in Load_Supplier : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub

    ' ── SUPPLIER SELECTED ───────────────────────────────────────────
    Private Sub cmbSupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSupplier.SelectedIndexChanged
        If cmbSupplier.SelectedItem IsNot Nothing Then
            txtSupCode.Text = CType(cmbSupplier.SelectedItem, SupplierItem).Code
        End If
    End Sub

    ' ── LOAD ASSORTMENTS ────────────────────────────────────────────
    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        Load_Assortments()
    End Sub

    Private Sub Load_Assortments()
        Dim rs As New ADODB.Recordset
        Try
            Cursor.Current = Cursors.WaitCursor
            cmbAssort.Items.Clear()
            rs.Open("SELECT AssortNo FROM tblGrading_RndSizeListNew ORDER BY AssortNo",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            Do While Not rs.EOF
                cmbAssort.Items.Add(rs.Fields("AssortNo").Value.ToString().Trim())
                rs.MoveNext()
            Loop
        Catch ex As Exception
            MsgBox("Error in Load_Assortments : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            Cursor.Current = Cursors.Default
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub

    ' ── ASSORTMENT ENTER — LOAD SIZE RANGES ─────────────────────────
    Private Sub cmbAssort_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbAssort.KeyPress
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True
            If cmbAssort.Text.Trim() <> "" Then
                Load_SizeRange()
                cmbSize.Focus()
            End If
        End If
    End Sub

    Private Sub Load_SizeRange()
        Dim rs As New ADODB.Recordset
        Try
            cmbSize.Items.Clear()
            Dim intType As Integer = If(chkNew.Checked, 1, 0)
            Dim sql As String =
                "SELECT Size FROM tblGrading_RndSizeListRange " &
                "WHERE AssortNo='" & cmbAssort.Text.Trim().Replace("'", "''") & "' " &
                "AND Type=" & intType & " ORDER BY Size"
            rs.Open(sql, AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            Do While Not rs.EOF
                cmbSize.Items.Add(rs.Fields("Size").Value.ToString().Trim())
                rs.MoveNext()
            Loop
        Catch ex As Exception
            MsgBox("Error in Load_SizeRange : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub

    Private Sub cmbSize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSize.KeyPress
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True
            If cmbSize.Text.Trim() <> "" Then txtNewPcs.Focus()
        End If
    End Sub

    ' ── LOT NO — ENTER KEY ──────────────────────────────────────────
    Private Sub txtLotNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLotNo.KeyPress
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True
            Load_PackingList()
        End If
    End Sub

    ' ── LOAD PACKING LIST BY LOT NO ─────────────────────────────────
    ' DiaStock pattern: ADODB.Recordset for all three SELECT branches
    Private Sub Load_PackingList()
        Dim rs As New ADODB.Recordset
        Try
            flxDetails.Rows.Clear()
            Dim is2nd As Boolean = opt2.Checked
            Dim ctsFormat As String = If(is2nd, "#0.00", "#0.000")
            Dim lotNo As String = txtLotNo.Text.Trim().Replace("'", "''")

            ' --- Try tblGrading_RndPackingListM first ---
            Dim hasM As Boolean = False
            rs.Open("SELECT * FROM tblGrading_RndPackingListM " &
                    "WHERE LotNo='" & lotNo & "' ORDER BY Code, Assortment",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

            Dim first As Boolean = True
            Do While Not rs.EOF
                hasM = True
                If first Then
                    txtPack.Text = rs.Fields("PackNo").Value.ToString().Trim()
                    txtType.Text = rs.Fields("PackType").Value.ToString().Trim()
                    txtCategory.Text = rs.Fields("Category").Value.ToString().Trim()
                    first = False
                End If
                Dim pcs As Double = Convert.ToDouble(rs.Fields("Pcs").Value)
                Dim cts As Double = Convert.ToDouble(rs.Fields("Cts").Value)
                Dim price As Double = If(IsDBNull(rs.Fields("Price").Value), 0, Convert.ToDouble(rs.Fields("Price").Value))

                Dim rowIdx As Integer = flxDetails.Rows.Add()
                Dim row As DataGridViewRow = flxDetails.Rows(rowIdx)
                row.Cells("Selected").Value = True
                row.Cells("Assortment").Value = rs.Fields("Assortment").Value.ToString().Trim()
                row.Cells("LotNo").Value = rs.Fields("LotNo").Value.ToString().Trim()
                row.Cells("Pcs").Value = pcs.ToString()
                row.Cells("Cts").Value = Format(cts, ctsFormat)
                row.Cells("Price").Value = Format(price, "#0.00")
                row.Cells("Value").Value = Format(cts * price, "#0.00")
                row.Cells("OrderNo").Value = rs.Fields("OrderNo").Value.ToString().Trim()
                row.Cells("PktNo").Value = rs.Fields("PktNo").Value.ToString().Trim()
                row.Cells("Color").Value = rs.Fields("Color").Value.ToString().Trim()
                row.Cells("Clarity").Value = rs.Fields("Clarity").Value.ToString().Trim()
                row.Cells("Code").Value = rs.Fields("Code").Value.ToString().Trim()
                row.Cells("SizeRange").Value = rs.Fields("SizeRange").Value.ToString().Trim()
                row.Cells("ID").Value = rs.Fields("ID").Value.ToString().Trim()
                row.Cells("PackNo").Value = rs.Fields("PackNo").Value.ToString().Trim()
                row.Cells("PackType").Value = rs.Fields("PackType").Value.ToString().Trim()
                rs.MoveNext()
            Loop
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()

            If Not hasM Then
                ' --- Fall back: tblGrading_RndPackingList via VW_GradingLot ---
                Dim lotVal As Double = Convert.ToDouble(txtLotNo.Text.Trim())
                Dim ctsRnd As Integer = If(is2nd, 2, 3)

                ' Non-Forevermark rows
                Dim sql1 As String =
                    "SELECT TOP (100) PERCENT gl.LotID, pl.Assortment, pl.SizeRange, " &
                    "SUM(pl.ActPcs) AS Pcs, ROUND(SUM(pl.ActCts)," & ctsRnd & ") AS Cts, " &
                    "ROUND(SUM(pl.ActCts * pl.Price),2) AS Value, pl.Code, pl.Price " &
                    "FROM tblGrading_RndPackingList pl " &
                    "INNER JOIN VW_GradingLot gl ON pl.ParNo=gl.ParNo " &
                    "WHERE pl.Code <> 'ZFOREVERMARK' " &
                    "GROUP BY gl.LotID, pl.Assortment, pl.SizeRange, pl.Code, pl.Price " &
                    "HAVING gl.LotID = '" & lotVal & "' ORDER BY pl.Assortment"

                rs.Open(sql1, AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
                Do While Not rs.EOF
                    Dim rowIdx As Integer = flxDetails.Rows.Add()
                    Dim row As DataGridViewRow = flxDetails.Rows(rowIdx)
                    row.Cells("Selected").Value = True
                    row.Cells("Assortment").Value = rs.Fields("Assortment").Value.ToString().Trim()
                    row.Cells("LotNo").Value = rs.Fields("LotID").Value.ToString().Trim()
                    row.Cells("Pcs").Value = rs.Fields("Pcs").Value.ToString()
                    row.Cells("Cts").Value = Format(Convert.ToDouble(rs.Fields("Cts").Value), ctsFormat)
                    row.Cells("Price").Value = Format(Convert.ToDouble(rs.Fields("Price").Value), "#0.00")
                    row.Cells("Value").Value = Format(Convert.ToDouble(rs.Fields("Value").Value), "#0.00")
                    row.Cells("Code").Value = rs.Fields("Code").Value.ToString().Trim()
                    row.Cells("SizeRange").Value = rs.Fields("SizeRange").Value.ToString().Trim()
                    rs.MoveNext()
                Loop
                If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()

                ' Forevermark rows
                Dim sql2 As String =
                    "SELECT TOP (100) PERCENT gl.LotID, pl.Assortment, pl.SizeRange, " &
                    "pl.ActPcs AS Pcs, pl.ActCts AS Cts, pl.Price, " &
                    "pl.ActCts * pl.Price AS Value, pl.Code, " &
                    "pl.OrderNo, pl.PktNo, pl.Color, pl.Clarity " &
                    "FROM tblGrading_RndPackingList pl " &
                    "INNER JOIN VW_GradingLot gl ON pl.ParNo=gl.ParNo " &
                    "WHERE pl.Code = 'ZFOREVERMARK' AND gl.LotID = '" & lotVal & "' " &
                    "ORDER BY pl.Assortment"

                rs.Open(sql2, AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
                Do While Not rs.EOF
                    Dim rowIdx As Integer = flxDetails.Rows.Add()
                    Dim row As DataGridViewRow = flxDetails.Rows(rowIdx)
                    row.Cells("Selected").Value = True
                    row.Cells("Assortment").Value = rs.Fields("Assortment").Value.ToString().Trim()
                    row.Cells("LotNo").Value = rs.Fields("LotID").Value.ToString().Trim()
                    row.Cells("Pcs").Value = rs.Fields("Pcs").Value.ToString()
                    row.Cells("Cts").Value = Format(Convert.ToDouble(rs.Fields("Cts").Value), ctsFormat)
                    row.Cells("Price").Value = Format(Convert.ToDouble(rs.Fields("Price").Value), "#0.00")
                    row.Cells("Value").Value = Format(Convert.ToDouble(rs.Fields("Value").Value), "#0.00")
                    row.Cells("OrderNo").Value = rs.Fields("OrderNo").Value.ToString().Trim()
                    row.Cells("PktNo").Value = rs.Fields("PktNo").Value.ToString().Trim()
                    row.Cells("Color").Value = rs.Fields("Color").Value.ToString().Trim()
                    row.Cells("Clarity").Value = rs.Fields("Clarity").Value.ToString().Trim()
                    row.Cells("Code").Value = rs.Fields("Code").Value.ToString().Trim()
                    row.Cells("SizeRange").Value = rs.Fields("SizeRange").Value.ToString().Trim()
                    rs.MoveNext()
                Loop
            End If

            RecalcTotals()

        Catch ex As Exception
            MsgBox("Error in Load_PackingList : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub

    ' ── ADD ROW ─────────────────────────────────────────────────────
    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim rs As New ADODB.Recordset
        Try
            If cmbAssort.Text.Trim() = "" Then MessageBox.Show("Invalid Assortment", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            If cmbSize.Text.Trim() = "" Then MessageBox.Show("Invalid Size Range", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            If txtNewPcs.Text.Trim() = "" OrElse Convert.ToDouble(txtNewPcs.Text) <= 0 Then MessageBox.Show("Invalid Pcs", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            If txtNewCts.Text.Trim() = "" OrElse Convert.ToDouble(txtNewCts.Text) <= 0 Then MessageBox.Show("Invalid Cts", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return

            If Convert.ToDouble(txtTotPcs.Text) + Convert.ToDouble(txtNewPcs.Text) > Convert.ToDouble(txtPcs.Text) Then
                MessageBox.Show("Invalid Total Pcs", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If Math.Round(Convert.ToDouble(txtTotCts.Text) + Convert.ToDouble(txtNewCts.Text), 3) > Convert.ToDouble(txtCts.Text) Then
                MessageBox.Show("Invalid Total Cts", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            ' Get price and code from tblGrading_RndSizeListNew
            Dim price As Double = 0
            Dim strCode As String = ""
            Dim assortQ As String = cmbAssort.Text.Trim().Replace("'", "''")

            rs.Open("SELECT PRICE, MainAssort FROM tblGrading_RndSizeListNew WHERE AssortNo='" & assortQ & "'",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            If rs.EOF Then
                MessageBox.Show("Invalid Assortment", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            price = If(IsDBNull(rs.Fields("PRICE").Value), 0, Convert.ToDouble(rs.Fields("PRICE").Value))
            strCode = rs.Fields("MainAssort").Value.ToString().Trim()
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()

            ' Validate size range
            Dim sizeQ As String = cmbSize.Text.Trim().Replace("'", "''")
            rs.Open("SELECT COUNT(*) AS Cnt FROM tblGrading_RndSizeListRange " &
                    "WHERE AssortNo='" & assortQ & "' AND Size='" & sizeQ & "'",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            If Not rs.EOF AndAlso CInt(rs.Fields("Cnt").Value) = 0 Then
                MessageBox.Show("Invalid Size Range", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()

            Dim pcs As Double = Convert.ToDouble(txtNewPcs.Text)
            Dim cts As Double = Convert.ToDouble(txtNewCts.Text)

            Dim rowIdx As Integer = flxDetails.Rows.Add()
            Dim row As DataGridViewRow = flxDetails.Rows(rowIdx)
            row.Cells("Selected").Value = True
            row.Cells("Assortment").Value = cmbAssort.Text.Trim().ToUpper()
            row.Cells("LotNo").Value = txtLotNo.Text.Trim()
            row.Cells("Pcs").Value = pcs.ToString()
            row.Cells("Cts").Value = cts.ToString()
            row.Cells("Price").Value = Format(price, "#0.00")
            row.Cells("Value").Value = Format(price * cts, "#0.00")
            row.Cells("Code").Value = strCode
            row.Cells("SizeRange").Value = cmbSize.Text.Trim()
            row.Cells("ID").Value = "0"
            row.Cells("PackNo").Value = txtPack.Text.Trim()
            row.Cells("PackType").Value = txtType.Text.Trim()

            RecalcTotals()

            cmbAssort.Text = "" : cmbSize.Text = ""
            txtNewPcs.Text = "" : txtNewCts.Text = ""
            cmbAssort.Focus()

        Catch ex As Exception
            MsgBox("Error in cmdAdd_Click : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub

    ' ── APPLY PACK NO TO ALL ROWS ────────────────────────────────────
    Private Sub cmdAddPack_Click(sender As Object, e As EventArgs) Handles cmdAddPack.Click
        If txtPack.Text.Trim() = "" Then
            MessageBox.Show("Invalid Packing List No", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        For Each row As DataGridViewRow In flxDetails.Rows
            row.Cells("PackNo").Value = txtPack.Text.Trim()
        Next
    End Sub

    ' ── RECALCULATE TOTALS (no DB — unchanged logic) ─────────────────
    Private Sub RecalcTotals()
        Dim totPcs As Double = 0
        Dim totCts As Double = 0
        Dim is2nd As Boolean = opt2.Checked
        For Each row As DataGridViewRow In flxDetails.Rows
            If Convert.ToBoolean(row.Cells("Selected").Value) Then
                totPcs += Convert.ToDouble(If(row.Cells("Pcs").Value?.ToString() = "", "0", row.Cells("Pcs").Value?.ToString()))
                totCts += Convert.ToDouble(If(row.Cells("Cts").Value?.ToString() = "", "0", row.Cells("Cts").Value?.ToString()))
            End If
        Next
        txtTotPcs.Text = totPcs.ToString()
        txtTotCts.Text = Format(Math.Round(totCts, If(is2nd, 2, 3)), If(is2nd, "#0.00", "#0.000"))
        txtPcs.Text = totPcs.ToString()
        txtCts.Text = Format(Math.Round(totCts, If(is2nd, 2, 3)), If(is2nd, "#0.00", "#0.000"))
    End Sub

    Private Sub flxDetails_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellValueChanged
        If flxDetails.Columns(e.ColumnIndex).Name = "Selected" OrElse
           flxDetails.Columns(e.ColumnIndex).Name = "Cts" OrElse
           flxDetails.Columns(e.ColumnIndex).Name = "Pcs" Then
            RecalcTotals()
        End If
    End Sub

    Private Sub flxDetails_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles flxDetails.CurrentCellDirtyStateChanged
        If flxDetails.IsCurrentCellDirty Then
            flxDetails.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    ' ── SAVE ────────────────────────────────────────────────────────
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Dim rs As New ADODB.Recordset
        Dim cmd As New ADODB.Command
        Try
            If txtLotNo.Text.Trim() = "" Then MessageBox.Show("Invalid Lot No", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            If flxDetails.Rows.Count < 1 Then MessageBox.Show("No Records", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            If txtPack.Text.Trim() = "" Then MessageBox.Show("Invalid Packing List No", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            If txtType.Text.Trim() = "" Then MessageBox.Show("Invalid Type", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            If txtCategory.Text.Trim() = "" Then MessageBox.Show("Invalid Category", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            If cmbSupplier.Text.Trim() = "" Then MessageBox.Show("Invalid Buyer", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            If txtSupCode.Text.Trim() = "" Then MessageBox.Show("Invalid Buyer", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return

            RecalcTotals()

            If Convert.ToDouble(txtTotPcs.Text) <> Convert.ToDouble(txtPcs.Text) Then
                MessageBox.Show("Pcs not matching", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If Math.Round(Convert.ToDouble(txtTotCts.Text), 3) <> Math.Round(Convert.ToDouble(txtCts.Text), 3) Then
                MessageBox.Show("Cts not matching", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            ' Check if lot already exists in PackingListM
            Dim exists As Boolean = False
            Dim lotQ As String = txtLotNo.Text.Trim().Replace("'", "''")
            rs.Open("SELECT COUNT(*) AS Cnt FROM tblGrading_RndPackingListM WHERE LotNo='" & lotQ & "'",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            If Not rs.EOF Then exists = (CInt(rs.Fields("Cnt").Value) > 0)
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()

            cmd.ActiveConnection = AdoCN

            If exists Then
                ' Validate pack nos for selected rows
                For Each row As DataGridViewRow In flxDetails.Rows
                    If Convert.ToBoolean(row.Cells("Selected").Value) Then
                        If row.Cells("PackNo").Value?.ToString().Trim() = "" Then
                            MessageBox.Show("Invalid Packing List No - " & row.Cells("Assortment").Value?.ToString(),
                                            Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return
                        End If
                    End If
                Next

                If MessageBox.Show("Are you sure to update?", Me.Text,
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return

                For Each row As DataGridViewRow In flxDetails.Rows
                    Dim id As String = row.Cells("ID").Value?.ToString().Trim()

                    If Convert.ToBoolean(row.Cells("Selected").Value) Then
                        ' Check if ID exists in PackingListM
                        Dim idExists As Boolean = False
                        rs.Open("SELECT COUNT(*) AS Cnt FROM tblGrading_RndPackingListM WHERE ID=" & CDbl(id),
                                AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
                        If Not rs.EOF Then idExists = (CInt(rs.Fields("Cnt").Value) > 0)
                        If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()

                        If Not idExists Then
                            InsertPackingListM(row, cmd)
                        Else
                            Dim catQ As String = txtCategory.Text.Trim().Replace("'", "''")
                            Dim ptypeQ As String = row.Cells("PackType").Value?.ToString().Trim().Replace("'", "''")
                            cmd.CommandText =
                                "UPDATE tblGrading_RndPackingListM SET " &
                                "Pcs=" & Convert.ToDouble(row.Cells("Pcs").Value?.ToString()) & ", " &
                                "Cts=" & Convert.ToDouble(row.Cells("Cts").Value?.ToString()) & ", " &
                                "PackNo=" & CDbl(row.Cells("PackNo").Value?.ToString()) & ", " &
                                "Price=" & Convert.ToDouble(row.Cells("Price").Value?.ToString()) & ", " &
                                "PackType='" & ptypeQ & "', " &
                                "Category='" & catQ & "' " &
                                "WHERE ID=" & CDbl(id)
                            cmd.Execute()
                        End If
                    Else
                        ' Delete unselected rows
                        cmd.CommandText = "DELETE FROM tblGrading_RndPackingListM WHERE ID=" & CDbl(id)
                        cmd.Execute()
                    End If
                Next
                MessageBox.Show("Updated Successfully", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                If MessageBox.Show("Are you sure to save?", Me.Text,
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return
                For Each row As DataGridViewRow In flxDetails.Rows
                    If Convert.ToBoolean(row.Cells("Selected").Value) Then
                        InsertPackingListM(row, cmd)
                    End If
                Next
                MessageBox.Show("Saved Successfully", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            ResetForm()

        Catch ex As Exception
            MsgBox("Save error : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
            cmd = Nothing
        End Try
    End Sub

    ' ── INSERT HELPER ────────────────────────────────────────────────
    ' Accepts a shared ADODB.Command to avoid repeated object creation
    Private Sub InsertPackingListM(row As DataGridViewRow, cmd As ADODB.Command)
        Dim assortQ As String = row.Cells("Assortment").Value?.ToString().Trim().Replace("'", "''")
        Dim lotQ As String = txtLotNo.Text.Trim().Replace("'", "''")
        Dim ptypeQ As String = txtType.Text.Trim().Replace("'", "''")
        Dim catQ As String = txtCategory.Text.Trim().Replace("'", "''")
        Dim orderQ As String = row.Cells("OrderNo").Value?.ToString().Trim().Replace("'", "''")
        Dim pktQ As String = row.Cells("PktNo").Value?.ToString().Trim().Replace("'", "''")
        Dim colorQ As String = row.Cells("Color").Value?.ToString().Trim().Replace("'", "''")
        Dim clarityQ As String = row.Cells("Clarity").Value?.ToString().Trim().Replace("'", "''")
        Dim codeQ As String = row.Cells("Code").Value?.ToString().Trim().Replace("'", "''")
        Dim sizeQ As String = row.Cells("SizeRange").Value?.ToString().Trim().Replace("'", "''")

        cmd.CommandText =
            "INSERT INTO tblGrading_RndPackingListM" &
            "(Assortment,LotNo,Pcs,Cts,Price,PackNo,PackType,Category," &
            "OrderNo,PktNo,Color,Clarity,Code,SupCode,SizeRange) VALUES(" &
            "'" & assortQ & "','" & lotQ & "'," &
            Convert.ToDouble(row.Cells("Pcs").Value?.ToString()) & "," &
            Convert.ToDouble(row.Cells("Cts").Value?.ToString()) & "," &
            Convert.ToDouble(row.Cells("Price").Value?.ToString()) & "," &
            CDbl(txtPack.Text) & ",'" & ptypeQ & "','" & catQ & "','" &
            orderQ & "','" & pktQ & "','" & colorQ & "','" & clarityQ & "','" &
            codeQ & "'," & CInt(txtSupCode.Text) & ",'" & sizeQ & "')"
        cmd.Execute()
    End Sub

    ' ── SAVE PRICE ONLY ─────────────────────────────────────────────
    Private Sub cmdSavePrice_Click(sender As Object, e As EventArgs) Handles cmdSavePrice.Click
        Dim cmd As New ADODB.Command
        Try
            If txtLotNo.Text.Trim() = "" Then
                MessageBox.Show("Invalid Lot No", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If flxDetails.Rows.Count < 1 Then
                MessageBox.Show("No Records", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If MessageBox.Show("Are you sure to Save the New Price?", Me.Text,
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return

            cmd.ActiveConnection = AdoCN
            For Each row As DataGridViewRow In flxDetails.Rows
                If Convert.ToBoolean(row.Cells("Selected").Value) Then
                    cmd.CommandText =
                        "UPDATE tblGrading_RndPackingListM SET " &
                        "Price=" & Convert.ToDouble(row.Cells("Price").Value?.ToString()) & " " &
                        "WHERE ID=" & CDbl(row.Cells("ID").Value?.ToString())
                    cmd.Execute()
                End If
            Next
            MessageBox.Show("Price Updated Successfully", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ResetForm()
        Catch ex As Exception
            MsgBox("Error in cmdSavePrice_Click : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            cmd = Nothing
        End Try
    End Sub

    ' ── UPDATE LIST PRICE FROM MASTER ───────────────────────────────
    Private Sub cmdUpdate_Click(sender As Object, e As EventArgs) Handles cmdUpdate.Click
        If MessageBox.Show("Are you sure to Update the List Price?", Me.Text,
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return
        Dim rs As New ADODB.Recordset
        Try
            For Each row As DataGridViewRow In flxDetails.Rows
                Dim assortQ As String = row.Cells("Assortment").Value?.ToString().Trim().Replace("'", "''")
                rs.Open("SELECT PRICE FROM tblGrading_RndSizeListNew WHERE AssortNo='" & assortQ & "'",
                        AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
                If Not rs.EOF AndAlso Not IsDBNull(rs.Fields("PRICE").Value) Then
                    Dim price As Double = Convert.ToDouble(rs.Fields("PRICE").Value)
                    Dim cts As Double = Convert.ToDouble(row.Cells("Cts").Value?.ToString())
                    row.Cells("Price").Value = Format(price, "#0.00")
                    row.Cells("Value").Value = Format(Math.Round(price * cts, 2), "#0.00")
                End If
                If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            Next
        Catch ex As Exception
            MsgBox("Error in cmdUpdate_Click : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub

    ' ── TOOLBAR BUTTONS ─────────────────────────────────────────────
    'Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
    '   ResetForm()
    '  txtLotNo.Focus()
    ' End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        Try
            Using dlg As New SaveFileDialog()
                dlg.Filter = "Excel Files (*.xls)|*.xls"
                dlg.FileName = "PackageMix_" & txtLotNo.Text & ".xls"
                If dlg.ShowDialog() = DialogResult.OK Then
                    ExportGrid(dlg.FileName, vbTab)
                    ShellEx(dlg.FileName)
                End If
            End Using
        Catch ex As Exception
            MsgBox("Export error : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnexcel.Click
        Try
            Using dlg As New SaveFileDialog()
                dlg.Filter = "Excel Files (*.xls)|*.xls"
                dlg.FileName = "PackageMix_" & txtLotNo.Text & ".xls"
                If dlg.ShowDialog() = DialogResult.OK Then
                    ExportGrid(dlg.FileName, vbTab)
                    ShellEx(dlg.FileName)
                End If
            End Using
        Catch ex As Exception
            MsgBox("Export error : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    Private Sub btnExportCSV_Click(sender As Object, e As EventArgs) Handles btnExportCSV.Click
        Try
            Using dlg As New SaveFileDialog()
                dlg.Filter = "CSV Files (*.csv)|*.csv"
                dlg.FileName = "PackageMix_" & txtLotNo.Text & ".csv"
                If dlg.ShowDialog() = DialogResult.OK Then
                    ExportGrid(dlg.FileName, ",")
                End If
            End Using
        Catch ex As Exception
            MsgBox("Export error : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    Private Sub ExportGrid(filePath As String, delim As String)
        Dim sb As New StringBuilder()
        Dim hdrs As New List(Of String)
        For Each col As DataGridViewColumn In flxDetails.Columns
            hdrs.Add(col.HeaderText)
        Next
        sb.AppendLine(String.Join(delim, hdrs))
        For Each row As DataGridViewRow In flxDetails.Rows
            Dim cols As New List(Of String)
            For Each cell As DataGridViewCell In row.Cells
                cols.Add(cell.Value?.ToString())
            Next
            sb.AppendLine(String.Join(delim, cols))
        Next
        File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8)
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    ' ── RESET FORM (no DB — unchanged) ──────────────────────────────
    Private Sub ResetForm()
        flxDetails.Rows.Clear()
        txtLotNo.Text = "" : txtPcs.Text = "0"
        txtCts.Text = "0" : txtTotPcs.Text = "0"
        txtTotCts.Text = "0" : txtPack.Text = ""
        txtType.Text = "" : txtCategory.Text = ""
        cmbSupplier.SelectedIndex = -1
        txtSupCode.Text = ""
    End Sub

    ' ── KEY PRESS HELPERS ────────────────────────────────────────────
    Private Sub txtNewPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNewPcs.KeyPress
        NumericOnly(e, txtNewPcs.Text)
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True
            txtNewCts.Focus()
        End If
    End Sub

    Private Sub txtNewCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNewCts.KeyPress
        NumericOnly(e, txtNewCts.Text)
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub txtPack_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPack.KeyPress
        NumericOnly(e, txtPack.Text)
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True
            cmdAdd.Focus()
        End If
    End Sub

    ' ── STUB HANDLERS (no logic — retained as-is) ───────────────────
    Private Sub lblTitle_Click(sender As Object, e As EventArgs) Handles lblTitle.Click
    End Sub

    Private Sub lblType_Click(sender As Object, e As EventArgs) Handles lblType.Click

    End Sub

    Private Sub cmbSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSize.SelectedIndexChanged

    End Sub

    Private Sub cmbAssort_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAssort.SelectedIndexChanged

    End Sub

    Private Sub txtPcs_TextChanged(sender As Object, e As EventArgs) Handles txtPcs.TextChanged

    End Sub

    Private Sub txtTotPcs_TextChanged(sender As Object, e As EventArgs) Handles txtTotPcs.TextChanged

    End Sub

    Private Sub lblTotPcsB_Click(sender As Object, e As EventArgs) Handles lblTotPcsB.Click

    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        ResetForm()
        txtLotNo.Focus()
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        Load_Assortments()
    End Sub
End Class