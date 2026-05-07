Imports System.Text
Imports System.IO

Public Class frm_GRDRnd_ExportSummaryBundle

    ' ── FORM LOAD ───────────────────────────────────────────────────
    Private Sub frm_Grading_ExportSummaryBundle_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.StartPosition = FormStartPosition.CenterScreen
            SetupGrid()
            Load_SizeRange()
            txtPcs.Text = "0"
            txtCts.Text = "0"
            txtTotPcs.Text = "0"
            txtTotCts.Text = "0"
        Catch ex As Exception
            MsgBox("Error in Form_Load : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
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

        Dim headers() As String = {"Assortment", "Bundle No", "Pcs", "Cts", "Price", "Value", "Size Range"}
        Dim names() As String = {"Assortment", "BundleNo", "Pcs", "Cts", "Price", "Value", "SizeRange"}
        Dim widths() As Integer = {120, 75, 60, 75, 65, 75, 100}
        Dim editable() As Boolean = {False, False, True, True, False, False, False}

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

    ' ── LOAD SIZE RANGES ────────────────────────────────────────────
    ' DiaStock pattern: ADODB.Recordset
    Private Sub Load_SizeRange()
        Dim rs As New ADODB.Recordset
        Try
            cmbSize.Items.Clear()
            rs.Open("SELECT Code FROM tblGrading_RndSizingRange ORDER BY Code",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            Do While Not rs.EOF
                cmbSize.Items.Add(rs.Fields("Code").Value.ToString().Trim())
                rs.MoveNext()
            Loop
        Catch ex As Exception
            MsgBox("Error in Load_SizeRange : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub

    ' ── LOAD ASSORTMENTS (Refresh button) ───────────────────────────
    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
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
            MsgBox("Error in cmdRefresh_Click : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            Cursor.Current = Cursors.Default
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub

    ' ── BUNDLE NO — ENTER KEY ────────────────────────────────────────
    Private Sub txtBundleNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBundleNo.KeyPress
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True
            Load_PackingList()
        End If
    End Sub

    ' ── LOAD PACKING LIST FOR BUNDLE ────────────────────────────────
    ' DiaStock pattern: two ADODB.Recordsets — try PackingListB first,
    ' fall back to PackingListM + Bundle join
    Private Sub Load_PackingList()
        Dim rs As New ADODB.Recordset
        Try
            flxDetails.Rows.Clear()
            Dim bundleNo As String = txtBundleNo.Text.Trim()

            ' --- Try PackingListB first ---
            Dim hasB As Boolean = False
            rs.Open("SELECT * FROM tblGrading_RndPackingListB " &
                    "WHERE BundleNo='" & bundleNo & "' ORDER BY Assortment, Pcs, Cts",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

            Do While Not rs.EOF
                hasB = True
                Dim rowIdx As Integer = flxDetails.Rows.Add()
                Dim row As DataGridViewRow = flxDetails.Rows(rowIdx)
                row.Cells("Selected").Value = True
                row.Cells("Assortment").Value = rs.Fields("Assortment").Value.ToString().Trim()
                row.Cells("BundleNo").Value = rs.Fields("BundleNo").Value.ToString().Trim()
                row.Cells("Pcs").Value = rs.Fields("Pcs").Value.ToString()
                row.Cells("Cts").Value = Format(Convert.ToDouble(rs.Fields("Cts").Value), "#0.000")
                row.Cells("Price").Value = Format(Convert.ToDouble(rs.Fields("Price").Value), "#0.00")
                row.Cells("Value").Value = Format(Convert.ToDouble(rs.Fields("Cts").Value) * Convert.ToDouble(rs.Fields("Price").Value), "#0.00")
                row.Cells("SizeRange").Value = rs.Fields("SizeRange").Value.ToString().Trim()
                rs.MoveNext()
            Loop
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()

            If Not hasB Then
                ' --- Fall back: PackingListM + Bundle join ---
                Dim sql As String =
                    "SELECT TOP (100) PERCENT m.Assortment, m.SizeRange, " &
                    "SUM(m.Pcs) AS Pcs, ROUND(SUM(m.Cts),3) AS Cts, " &
                    "sln.Price, ROUND(SUM(sln.Price * m.Cts),2) AS Value " &
                    "FROM tblGrading_RndBundle b " &
                    "INNER JOIN tblGrading_RndPackingListM m ON b.PackNo = m.PackNo " &
                    "INNER JOIN tblGrading_RndSizeListNew sln ON m.Assortment = sln.AssortNo " &
                    "WHERE b.BundleNo = '" & bundleNo & "' " &
                    "GROUP BY m.Assortment, m.SizeRange, sln.Price " &
                    "ORDER BY m.Assortment"

                rs.Open(sql, AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

                Do While Not rs.EOF
                    Dim rowIdx As Integer = flxDetails.Rows.Add()
                    Dim row As DataGridViewRow = flxDetails.Rows(rowIdx)
                    row.Cells("Selected").Value = True
                    row.Cells("Assortment").Value = rs.Fields("Assortment").Value.ToString().Trim()
                    row.Cells("BundleNo").Value = bundleNo
                    row.Cells("Pcs").Value = rs.Fields("Pcs").Value.ToString()
                    row.Cells("Cts").Value = Format(Convert.ToDouble(rs.Fields("Cts").Value), "#0.000")
                    row.Cells("Price").Value = Format(Convert.ToDouble(rs.Fields("Price").Value), "#0.00")
                    row.Cells("Value").Value = Format(Convert.ToDouble(rs.Fields("Value").Value), "#0.00")
                    row.Cells("SizeRange").Value = rs.Fields("SizeRange").Value.ToString().Trim()
                    rs.MoveNext()
                Loop
            End If

            RecalcTotals(setOriginal:=True)

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

            ' Get price from tblGrading_RndSizeListNew
            Dim price As Double = 0
            rs.Open("SELECT PRICE FROM tblGrading_RndSizeListNew WHERE AssortNo='" & cmbAssort.Text.Trim() & "'",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            If rs.EOF Then
                MessageBox.Show("Invalid Assortment", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            price = If(IsDBNull(rs.Fields("PRICE").Value), 0, Convert.ToDouble(rs.Fields("PRICE").Value))
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()

            ' Validate size range
            rs.Open("SELECT COUNT(*) AS Cnt FROM tblGrading_RndSizingRange WHERE Code='" & cmbSize.Text.Trim() & "'",
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
            row.Cells("BundleNo").Value = txtBundleNo.Text.Trim()
            row.Cells("Pcs").Value = pcs.ToString()
            row.Cells("Cts").Value = cts.ToString()
            row.Cells("Price").Value = Format(price, "#0.00")
            row.Cells("Value").Value = Format(price * cts, "#0.00")
            row.Cells("SizeRange").Value = cmbSize.Text.Trim()

            RecalcTotals()

            cmbAssort.Text = ""
            cmbSize.Text = ""
            txtNewPcs.Text = ""
            txtNewCts.Text = ""
            cmbAssort.Focus()

        Catch ex As Exception
            MsgBox("Error in cmdAdd_Click : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub

    ' ── RECALCULATE TOTALS (no DB — unchanged logic) ─────────────────
    Private Sub RecalcTotals(Optional setOriginal As Boolean = False)
        Dim totPcs As Double = 0
        Dim totCts As Double = 0
        For Each row As DataGridViewRow In flxDetails.Rows
            If Convert.ToBoolean(row.Cells("Selected").Value) Then
                totPcs += Convert.ToDouble(If(row.Cells("Pcs").Value?.ToString() = "", "0", row.Cells("Pcs").Value?.ToString()))
                totCts += Convert.ToDouble(If(row.Cells("Cts").Value?.ToString() = "", "0", row.Cells("Cts").Value?.ToString()))
            End If
        Next
        txtTotPcs.Text = totPcs.ToString()
        txtTotCts.Text = Format(Math.Round(totCts, 3), "#0.000")
        If setOriginal Then
            txtPcs.Text = totPcs.ToString()
            txtCts.Text = Format(Math.Round(totCts, 3), "#0.000")
        End If
    End Sub

    Private Sub flxDetails_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellValueChanged
        RecalcTotals()
    End Sub

    Private Sub flxDetails_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles flxDetails.CurrentCellDirtyStateChanged
        If flxDetails.IsCurrentCellDirty Then
            flxDetails.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    ' ── SAVE ────────────────────────────────────────────────────────
    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Dim rs As New ADODB.Recordset
        Try
            If txtBundleNo.Text.Trim() = "" Then
                MessageBox.Show("Invalid Bundle No", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If flxDetails.Rows.Count < 1 Then
                MessageBox.Show("No Records", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            RecalcTotals()

            If Convert.ToDouble(txtTotPcs.Text) <> Convert.ToDouble(txtPcs.Text) Then
                MessageBox.Show("Pcs not matching", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If Math.Round(Convert.ToDouble(txtTotCts.Text), 3) <> Math.Round(Convert.ToDouble(txtCts.Text), 3) Then
                MessageBox.Show("Cts not matching", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            ' Check if BundleNo already exists
            Dim exists As Boolean = False
            rs.Open("SELECT COUNT(*) AS Cnt FROM tblGrading_RndPackingListB WHERE BundleNo=" & CDbl(txtBundleNo.Text),
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            If Not rs.EOF Then exists = (CInt(rs.Fields("Cnt").Value) > 0)
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()

            If exists Then
                If MessageBox.Show("Are you sure to update?", Me.Text,
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return
                ' Delete existing rows then re-insert
                Dim cmdDel As New ADODB.Command
                cmdDel.ActiveConnection = AdoCN
                cmdDel.CommandText = "DELETE FROM tblGrading_RndPackingListB WHERE BundleNo=" & CDbl(txtBundleNo.Text)
                cmdDel.Execute()
                cmdDel = Nothing
                InsertRows()
                MessageBox.Show("Updated Successfully", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                If MessageBox.Show("Are you sure to save?", Me.Text,
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return
                InsertRows()
                MessageBox.Show("Saved Successfully", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            ResetForm()

        Catch ex As Exception
            MsgBox("Save error : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub

    ' ── INSERT ROWS HELPER ───────────────────────────────────────────
    ' DiaStock pattern: ADODB.Command.Execute, string-concatenated SQL
    Private Sub InsertRows()
        Dim cmd As New ADODB.Command
        Try
            cmd.ActiveConnection = AdoCN
            For Each row As DataGridViewRow In flxDetails.Rows
                If Convert.ToBoolean(row.Cells("Selected").Value) Then
                    Dim assort As String = row.Cells("Assortment").Value?.ToString().Trim().Replace("'", "''")
                    Dim sizeR As String = row.Cells("SizeRange").Value?.ToString().Trim().Replace("'", "''")
                    Dim pcs As Double = Convert.ToDouble(row.Cells("Pcs").Value?.ToString())
                    Dim cts As Double = Convert.ToDouble(row.Cells("Cts").Value?.ToString())
                    Dim price As Double = Convert.ToDouble(row.Cells("Price").Value?.ToString())
                    Dim bundleNo As Double = CDbl(txtBundleNo.Text)

                    cmd.CommandText =
                        "INSERT INTO tblGrading_RndPackingListB(Assortment,BundleNo,Pcs,Cts,Price,SizeRange) " &
                        "VALUES('" & assort & "'," & bundleNo & "," &
                        pcs & "," & cts & "," & price & ",'" & sizeR & "')"
                    cmd.Execute()
                End If
            Next
        Finally
            cmd = Nothing
        End Try
    End Sub

    ' ── TOOLBAR BUTTONS ─────────────────────────────────────────────
    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ResetForm()
        txtBundleNo.Focus()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        Try
            Using dlg As New SaveFileDialog()
                dlg.Filter = "Excel Files (*.xls)|*.xls"
                dlg.FileName = "BundlePackage_" & txtBundleNo.Text & ".xls"
                If dlg.ShowDialog() = DialogResult.OK Then
                    Dim sb As New StringBuilder()
                    For Each col As DataGridViewColumn In flxDetails.Columns
                        sb.Append(col.HeaderText & vbTab)
                    Next
                    sb.AppendLine()
                    For Each row As DataGridViewRow In flxDetails.Rows
                        For Each cell As DataGridViewCell In row.Cells
                            sb.Append(cell.Value?.ToString() & vbTab)
                        Next
                        sb.AppendLine()
                    Next
                    File.WriteAllText(dlg.FileName, sb.ToString(), System.Text.Encoding.UTF8)
                    ShellEx(dlg.FileName)
                End If
            End Using
        Catch ex As Exception
            MsgBox("Export error : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    ' ── KEY PRESS HELPERS ────────────────────────────────────────────
    Private Sub txtNewPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNewPcs.KeyPress
        NumericOnly(e, txtNewPcs.Text)
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub txtNewCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNewCts.KeyPress
        NumericOnly(e, txtNewCts.Text)
        If e.KeyChar = ControlChars.Cr Then e.Handled = True : cmdAdd.Focus()
    End Sub

    ' ── RESET FORM (no DB — unchanged) ──────────────────────────────
    Private Sub ResetForm()
        flxDetails.Rows.Clear()
        txtBundleNo.Text = "" : txtPcs.Text = "0"
        txtCts.Text = "0" : txtTotPcs.Text = "0"
        txtTotCts.Text = "0" : cmbSize.Text = ""
    End Sub

    ' ── STUB HANDLERS (no logic — retained as-is) ───────────────────
    Private Sub lblSize_Click(sender As Object, e As EventArgs) Handles lblSize.Click
    End Sub

    Private Sub pnlRow2_Paint(sender As Object, e As PaintEventArgs) Handles pnlRow2.Paint
    End Sub

    Private Sub lblTitle_Click(sender As Object, e As EventArgs) Handles lblTitle.Click
    End Sub

End Class