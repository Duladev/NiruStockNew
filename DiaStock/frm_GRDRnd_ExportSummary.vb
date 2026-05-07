Imports System.Text
Imports System.IO

Public Class frm_GRDRnd_ExportSummary

    ' ── FORM LOAD ───────────────────────────────────────────────────
    Private Sub frm_Grading_ExportSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.StartPosition = FormStartPosition.CenterScreen
            SetupGrids()
            GetPackNo()
            txtAddPcs.Text = "0"
            txtAddCts.Text = "0"
            txtPcs.Text = "0"
            txtCts.Text = "0"
            Load_ExportParcels()
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

    ' ── GRID SETUP (no DB calls — unchanged) ────────────────────────
    Private Sub SetupGrids()
        flxDetails.Columns.Clear()
        flxDetails.AutoGenerateColumns = False
        flxDetails.AllowUserToAddRows = False
        flxDetails.AllowUserToDeleteRows = False
        flxDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        flxDetails.BackgroundColor = System.Drawing.Color.White
        flxDetails.Font = New System.Drawing.Font("MS Sans Serif", 8.25)
        flxDetails.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(224, 224, 224)

        Dim headers() As String = {
            "Assortment", "Par No", "Pcs", "Cts", "Act Pcs", "Act Cts",
            "Diff", "Price", "Value", "Dept", "ID", "Order No",
            "Pkt No", "Color", "Clarity", "Code", "Size Range"
        }
        Dim names() As String = {
            "Assortment", "ParNo", "Pcs", "Cts", "ActPcs", "ActCts",
            "Diff", "Price", "Value", "Dept", "ID", "OrderNo",
            "PktNo", "Color", "Clarity", "Code", "SizeRange"
        }
        Dim widths() As Integer = {
            110, 80, 60, 70, 60, 70,
            60, 65, 75, 80, 50, 90,
            65, 70, 70, 80, 90
        }
        Dim editable() As Boolean = {
            False, False, False, False, True, False,
            False, False, False, False, False, False,
            False, False, False, False, False
        }

        For idx As Integer = 0 To headers.Length - 1
            Dim col As New DataGridViewTextBoxColumn()
            col.HeaderText = headers(idx)
            col.Name = names(idx)
            col.Width = widths(idx)
            col.ReadOnly = Not editable(idx)
            If idx >= 2 AndAlso idx <= 8 Then
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
            flxDetails.Columns.Add(col)
        Next

        cmbParcel.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub

    ' ── GET NEXT PACK NUMBER ─────────────────────────────────────────
    ' DiaStock pattern: ADODB.Recordset, string-concatenated SQL
    Private Sub GetPackNo()
        Dim rs As New ADODB.Recordset
        Try
            rs.Open("SELECT MAX(PackNo) AS MaxNo FROM tblGrading_RndPackingList",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

            If Not rs.EOF AndAlso Not IsDBNull(rs.Fields("MaxNo").Value) AndAlso
               rs.Fields("MaxNo").Value IsNot Nothing Then
                txtPackNo.Text = (CInt(rs.Fields("MaxNo").Value) + 1).ToString()
            Else
                txtPackNo.Text = "1"
            End If
        Catch ex As Exception
            txtPackNo.Text = "1"
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub

    ' ── LOAD EXPORT PARCELS INTO COMBO ──────────────────────────────
    Private Sub Load_ExportParcels()
        Dim rs As New ADODB.Recordset
        Try
            cmbParcel.Items.Clear()

            Dim sql As String
            If chkNew.Checked Then
                sql = "SELECT TOP (100) PERCENT Department, ParNo " &
                      "FROM tblGrading_RndSizingTypes WHERE OK=0 " &
                      "GROUP BY Department, ParNo ORDER BY ParNo"
            Else
                sql = "SELECT DISTINCT TOP (100) PERCENT Department, LEFT(ParNo,6) AS ParNo " &
                      "FROM tblGrading_RndSizingTypes WHERE OK=0 ORDER BY LEFT(ParNo,6)"
            End If

            rs.Open(sql, AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

            Do While Not rs.EOF
                cmbParcel.Items.Add(New ParcelItem(
                    rs.Fields("ParNo").Value.ToString().Trim(),
                    rs.Fields("Department").Value.ToString().Trim()))
                rs.MoveNext()
            Loop

        Catch ex As Exception
            MsgBox("Error in Load_ExportParcels : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub

    ' ── PARCEL COMBO SELECTION ───────────────────────────────────────
    Private Sub cmbParcel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbParcel.SelectedIndexChanged
        If cmbParcel.SelectedItem Is Nothing Then Return
        Dim item As ParcelItem = CType(cmbParcel.SelectedItem, ParcelItem)
        Load_ExportDetails(item.ParNo, item.Department)
    End Sub

    ' ── LOAD EXPORT DETAILS INTO GRID ───────────────────────────────
    Private Sub Load_ExportDetails(strParcel As String, strDept As String)
        Dim rs As New ADODB.Recordset
        Dim rs2 As New ADODB.Recordset
        Try
            ' Check if parcel already loaded in grid
            For Each row As DataGridViewRow In flxDetails.Rows
                If row.Cells("ParNo").Value?.ToString() = strParcel AndAlso
                   row.Cells("Dept").Value?.ToString() = strDept Then
                    MessageBox.Show("Already Entered", Me.Text,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If
            Next

            Dim intIssPcs As Double = Convert.ToDouble(If(txtAddPcs.Text = "", "0", txtAddPcs.Text))
            Dim dblIssCts As Double = Convert.ToDouble(If(txtAddCts.Text = "", "0", txtAddCts.Text))
            Dim is2nd As Boolean = opt2.Checked
            Dim ctsRound As Integer = If(is2nd, 2, 3)
            Dim parWhere As String = If(chkNew.Checked, "st.ParNo", "LEFT(st.ParNo,6)")

            ' --- Query sizing types (string-concatenated SQL) ---
            Dim sql As String =
                "SELECT TOP (100) PERCENT st.Department, " & parWhere & " AS ParNo, " &
                "st.ReturnType2, st.ReturnType3, SUM(st.Pcs) AS Pcs, " &
                "ROUND(SUM(st.Cts)," & ctsRound & ") AS Cts, sln.Price, sln.MainAssort " &
                "FROM tblGrading_RndSizingTypes st " &
                "INNER JOIN tblGrading_RndSizeListNew sln ON st.ReturnType2 = sln.AssortNo " &
                "INNER JOIN tblGrading_RndSizingPacket sp ON st.Department=sp.Department " &
                "  AND st.ParNo=sp.ParNo AND st.PktNo=sp.PktNo " &
                "WHERE (" & parWhere & " = '" & strParcel & "') AND st.OK=0 " &
                "GROUP BY st.Department, st.ReturnType2, st.ReturnType3, sln.Price, " &
                parWhere & ", sln.MainAssort " &
                "ORDER BY st.ReturnType2"

            rs.Open(sql, AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

            Do While Not rs.EOF
                Dim assort As String = rs.Fields("ReturnType2").Value.ToString().Trim()
                Dim parNo As String = rs.Fields("ParNo").Value.ToString().Trim()
                Dim pcs As Double = Convert.ToDouble(rs.Fields("Pcs").Value)
                Dim cts As Double = Math.Round(Convert.ToDouble(rs.Fields("Cts").Value), ctsRound)
                Dim price As Double = If(IsDBNull(rs.Fields("Price").Value), 0, Convert.ToDouble(rs.Fields("Price").Value))
                Dim mainAss As String = rs.Fields("MainAssort").Value.ToString().Trim()

                ' Subtract already-packed pieces
                Dim pkdPcs As Double = 0
                Dim pkdCts As Double = 0

                Dim sql2 As String =
                    "SELECT SUM(Pcs) AS P, ROUND(SUM(Cts),3) AS C " &
                    "FROM tblGrading_RndBox " &
                    "WHERE FM2=1 AND ParNo='" & strParcel & "' AND Assortment='" & assort & "'"

                rs2.Open(sql2, AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
                If Not rs2.EOF AndAlso Not IsDBNull(rs2.Fields("P").Value) Then
                    pkdPcs = Convert.ToDouble(rs2.Fields("P").Value)
                    pkdCts = Convert.ToDouble(rs2.Fields("C").Value)
                End If
                If rs2.State = ADODB.ObjectStateEnum.adStateOpen Then rs2.Close()

                Dim netPcs As Double = pcs - pkdPcs
                Dim netCts As Double = cts - pkdCts

                If netPcs > 0 Then
                    flxDetails.Rows.Add(
                        assort, parNo,
                        netPcs.ToString(), Format(netCts, If(is2nd, "#0.00", "#0.000")),
                        netPcs.ToString(), Format(netCts, If(is2nd, "#0.00", "#0.000")),
                        "0", Format(price, "#0.00"),
                        Format(price * netCts, "#0.00"),
                        rs.Fields("Department").Value.ToString().Trim(), "",
                        "", "", "", "", mainAss, rs.Fields("ReturnType3").Value.ToString().Trim()
                    )
                    intIssPcs += netPcs
                    dblIssCts += netCts
                End If

                rs.MoveNext()
            Loop
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()

            ' --- Load box items (FM2=1) ---
            Dim sqlBox As String =
                "SELECT b.Assortment, b.Pcs, b.Cts, b.ParNo, sln.Price, " &
                "sln.Color, sln.Clarity, b.PktNo, b.OrderNo " &
                "FROM tblGrading_RndBox b " &
                "INNER JOIN tblGrading_RndSizeListNew sln ON b.Assortment=sln.AssortNo " &
                "WHERE b.FM2=1 AND b.ParNo='" & strParcel & "' ORDER BY b.Assortment"

            rs.Open(sqlBox, AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

            Do While Not rs.EOF
                Dim pcs As Double = Convert.ToDouble(rs.Fields("Pcs").Value)
                Dim cts As Double = Convert.ToDouble(rs.Fields("Cts").Value)
                Dim price As Double = If(IsDBNull(rs.Fields("Price").Value), 0, Convert.ToDouble(rs.Fields("Price").Value))

                flxDetails.Rows.Add(
                    rs.Fields("Assortment").Value.ToString().Trim(),
                    rs.Fields("ParNo").Value.ToString().Trim(),
                    pcs.ToString(), Format(cts, "#0.000"),
                    pcs.ToString(), Format(cts, "#0.000"),
                    "0", Format(price, "#0.00"),
                    Format(price * cts, "#0.00"),
                    strDept, "", rs.Fields("OrderNo").Value.ToString().Trim(),
                    rs.Fields("PktNo").Value.ToString().Trim(),
                    rs.Fields("Color").Value.ToString().Trim(),
                    rs.Fields("Clarity").Value.ToString().Trim(),
                    "ZFOREVERMARK", ""
                )
                intIssPcs += pcs
                dblIssCts += cts
                rs.MoveNext()
            Loop

            dblIssCts = Math.Round(dblIssCts, ctsRound)
            txtAddPcs.Text = intIssPcs.ToString()
            txtPcs.Text = intIssPcs.ToString()
            txtAddCts.Text = Format(dblIssCts, If(is2nd, "#0.00", "#0.000"))
            txtCts.Text = Format(dblIssCts, If(is2nd, "#0.00", "#0.000"))

        Catch ex As Exception
            MsgBox("Error in Load_ExportDetails : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            If rs2.State = ADODB.ObjectStateEnum.adStateOpen Then rs2.Close()
            rs = Nothing
            rs2 = Nothing
        End Try
    End Sub

    ' ── LOAD PACKING LIST BY PACK NUMBER ────────────────────────────
    Private Sub Load_PackingList()
        Dim rs As New ADODB.Recordset
        Try
            flxDetails.Rows.Clear()
            Dim intIssPcs As Double = 0
            Dim dblIssCts As Double = 0
            Dim intActPcs As Double = 0
            Dim dblActCts As Double = 0
            Dim is2nd As Boolean = opt2.Checked
            Dim ctsRound As Integer = If(is2nd, 2, 3)

            Dim sql As String =
                "SELECT * FROM tblGrading_RndPackingList " &
                "WHERE PackNo=" & CInt(txtPackNo.Text) & " ORDER BY Code, Assortment"

            rs.Open(sql, AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

            Dim first As Boolean = True
            Do While Not rs.EOF
                If first Then
                    chkComplete.Checked = (rs.Fields("OK").Value.ToString() <> "0")
                    first = False
                End If
                Dim pcs As Double = Convert.ToDouble(rs.Fields("Pcs").Value)
                Dim cts As Double = Convert.ToDouble(rs.Fields("Cts").Value)
                Dim actPcs As Double = Convert.ToDouble(rs.Fields("ActPcs").Value)
                Dim actCts As Double = Convert.ToDouble(rs.Fields("ActCts").Value)
                Dim price As Double = If(IsDBNull(rs.Fields("Price").Value), 0, Convert.ToDouble(rs.Fields("Price").Value))

                flxDetails.Rows.Add(
                    rs.Fields("Assortment").Value.ToString().Trim(),
                    rs.Fields("ParNo").Value.ToString().Trim(),
                    pcs.ToString(), Format(cts, If(is2nd, "#0.00", "#0.000")),
                    actPcs.ToString(), Format(actCts, If(is2nd, "#0.00", "#0.000")),
                    Format(Math.Round(cts - actCts, ctsRound), "#0.000"),
                    Format(price, "#0.00"),
                    Format(price * actCts, "#0.00"),
                    rs.Fields("Department").Value.ToString().Trim(),
                    rs.Fields("ID").Value.ToString().Trim(),
                    rs.Fields("OrderNo").Value.ToString().Trim(),
                    rs.Fields("PktNo").Value.ToString().Trim(),
                    rs.Fields("Color").Value.ToString().Trim(),
                    rs.Fields("Clarity").Value.ToString().Trim(),
                    rs.Fields("Code").Value.ToString().Trim(),
                    rs.Fields("SizeRange").Value.ToString().Trim()
                )
                intIssPcs += pcs : intActPcs += actPcs
                dblIssCts += cts : dblActCts += actCts
                rs.MoveNext()
            Loop

            txtAddPcs.Text = intIssPcs.ToString()
            txtPcs.Text = intActPcs.ToString()
            txtAddCts.Text = Format(Math.Round(dblIssCts, ctsRound), "#0.000")
            txtCts.Text = Format(Math.Round(dblActCts, ctsRound), "#0.000")

        Catch ex As Exception
            MsgBox("Error in Load_PackingList : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub

    ' ── CALCULATION HELPERS (no DB — unchanged logic) ───────────────
    Private Function CalTotalPcs() As Double
        Dim total As Double = 0
        For Each row As DataGridViewRow In flxDetails.Rows
            total += Convert.ToDouble(If(row.Cells("ActPcs").Value?.ToString() = "", "0", row.Cells("ActPcs").Value?.ToString()))
        Next
        Return total
    End Function

    Private Function CalTotalCts() As Double
        Dim total As Double = 0
        Dim is2nd As Boolean = opt2.Checked
        For Each row As DataGridViewRow In flxDetails.Rows
            Dim actCts As Double = Convert.ToDouble(If(row.Cells("ActCts").Value?.ToString() = "", "0", row.Cells("ActCts").Value?.ToString()))
            Dim cts As Double = Convert.ToDouble(If(row.Cells("Cts").Value?.ToString() = "", "0", row.Cells("Cts").Value?.ToString()))
            Dim price As Double = Convert.ToDouble(If(row.Cells("Price").Value?.ToString() = "", "0", row.Cells("Price").Value?.ToString()))
            row.Cells("Diff").Value = Format(Math.Round(cts - actCts, If(is2nd, 2, 3)), "#0.000")
            row.Cells("Value").Value = Format(price * actCts, "#0.00")
            total += actCts
        Next
        Return Math.Round(total, If(is2nd, 2, 3))
    End Function

    Private Sub flxDetails_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellEndEdit
        If flxDetails.Columns(e.ColumnIndex).Name = "ActCts" Then
            txtPcs.Text = CalTotalPcs().ToString()
            txtCts.Text = Format(CalTotalCts(), "#0.000")
        End If
    End Sub

    ' ── SAVE BUTTON ─────────────────────────────────────────────────
    ' DiaStock pattern: ADODB.Command.Execute for INSERT / UPDATE
    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Dim rs As New ADODB.Recordset
        Dim cmd As New ADODB.Command
        Try
            If txtPackNo.Text.Trim() = "" Then
                MessageBox.Show("Invalid Package No", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If flxDetails.Rows.Count < 1 Then
                MessageBox.Show("No Records", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If Convert.ToDouble(txtAddPcs.Text) <> Convert.ToDouble(txtPcs.Text) Then
                MessageBox.Show("Pcs not matching", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            ' Check if PackNo already exists
            Dim exists As Boolean = False
            rs.Open("SELECT COUNT(*) AS Cnt FROM tblGrading_RndPackingList WHERE PackNo=" & CInt(txtPackNo.Text),
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            If Not rs.EOF Then exists = (CInt(rs.Fields("Cnt").Value) > 0)
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()

            cmd.ActiveConnection = AdoCN

            If exists Then
                ' UPDATE existing rows
                If MessageBox.Show("Are you sure to update?", Me.Text,
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return
                For Each row As DataGridViewRow In flxDetails.Rows
                    cmd.CommandText =
                        "UPDATE tblGrading_RndPackingList SET " &
                        "ActPcs=" & Convert.ToDouble(row.Cells("ActPcs").Value?.ToString()) & ", " &
                        "ActCts=" & Convert.ToDouble(row.Cells("ActCts").Value?.ToString()) & " " &
                        "WHERE ID=" & Convert.ToDouble(row.Cells("ID").Value?.ToString())
                    cmd.Execute()
                Next
                MessageBox.Show("Updated Successfully", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                ' INSERT new rows
                If MessageBox.Show("Are you sure to save?", Me.Text,
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return
                For Each row As DataGridViewRow In flxDetails.Rows
                    ' Safely quote string fields
                    Dim dept As String = row.Cells("Dept").Value?.ToString().Trim().Replace("'", "''")
                    Dim assort As String = row.Cells("Assortment").Value?.ToString().Trim().Replace("'", "''")
                    Dim parNo As String = row.Cells("ParNo").Value?.ToString().Trim().Replace("'", "''")
                    Dim orderNo As String = row.Cells("OrderNo").Value?.ToString().Trim().Replace("'", "''")
                    Dim pktNo As String = row.Cells("PktNo").Value?.ToString().Trim().Replace("'", "''")
                    Dim color As String = row.Cells("Color").Value?.ToString().Trim().Replace("'", "''")
                    Dim clarity As String = row.Cells("Clarity").Value?.ToString().Trim().Replace("'", "''")
                    Dim code As String = row.Cells("Code").Value?.ToString().Trim().Replace("'", "''")
                    Dim sizeR As String = row.Cells("SizeRange").Value?.ToString().Trim().Replace("'", "''")

                    cmd.CommandText =
                        "INSERT INTO tblGrading_RndPackingList" &
                        "(Department,PackNo,Assortment,ParNo,Pcs,Cts,ActPcs,ActCts,Price,OrderNo,PktNo,Color,Clarity,Code,SizeRange) " &
                        "VALUES('" & dept & "'," &
                        CInt(txtPackNo.Text) & ",'" &
                        assort & "','" & parNo & "'," &
                        Convert.ToDouble(row.Cells("Pcs").Value?.ToString()) & "," &
                        Convert.ToDouble(row.Cells("Cts").Value?.ToString()) & "," &
                        Convert.ToDouble(row.Cells("ActPcs").Value?.ToString()) & "," &
                        Convert.ToDouble(row.Cells("ActCts").Value?.ToString()) & "," &
                        Convert.ToDouble(row.Cells("Price").Value?.ToString()) & ",'" &
                        orderNo & "','" & pktNo & "','" &
                        color & "','" & clarity & "','" &
                        code & "','" & sizeR & "')"
                    cmd.Execute()

                    ' Mark SizingTypes as OK=1
                    cmd.CommandText =
                        "UPDATE tblGrading_RndSizingTypes SET OK=1 " &
                        "WHERE Department='" & dept & "' AND LEFT(ParNo,6)='" & parNo & "' AND OK=0"
                    cmd.Execute()
                Next
                MessageBox.Show("Saved Successfully", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            ' Reset form
            flxDetails.Rows.Clear()
            txtAddPcs.Text = "0" : txtAddCts.Text = "0"
            txtPcs.Text = "0" : txtCts.Text = "0"
            GetPackNo()
            cmbParcel.SelectedIndex = -1
            Load_ExportParcels()

        Catch ex As Exception
            MsgBox("Save error : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
            cmd = Nothing
        End Try
    End Sub

    ' ── UPDATE COMPLETE STATUS ───────────────────────────────────────
    Private Sub cmdUpdate_Click(sender As Object, e As EventArgs) Handles cmdUpdate.Click
        Dim cmd As New ADODB.Command
        Try
            If txtPackNo.Text.Trim() = "" Then Return
            Dim okVal As Integer = If(chkComplete.Checked, 1, 0)
            cmd.ActiveConnection = AdoCN
            cmd.CommandText =
                "UPDATE tblGrading_RndPackingList SET Ok=" & okVal &
                " WHERE PackNo=" & CInt(txtPackNo.Text)
            cmd.Execute()
            MessageBox.Show("Package Updated", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox("Error in cmdUpdate_Click : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            cmd = Nothing
        End Try
    End Sub

    ' ── TOOLBAR BUTTONS (no DB — unchanged) ─────────────────────────
    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        GetPackNo()
        txtAddPcs.Text = "0" : txtAddCts.Text = "0"
        txtPcs.Text = "0" : txtCts.Text = "0"
        flxDetails.Rows.Clear()
        chkComplete.Checked = False
    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        GetPackNo()
        Load_ExportParcels()
    End Sub

    ' ── EXCEL / CSV EXPORT (no DB — unchanged) ──────────────────────
    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        Try
            Using dlg As New SaveFileDialog()
                dlg.Filter = "Excel Files (*.xls)|*.xls"
                dlg.FileName = "Package_" & txtPackNo.Text & ".xls"
                If dlg.ShowDialog() = DialogResult.OK Then
                    ExportGridToFile(dlg.FileName, vbTab)
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
                dlg.FileName = "Package_" & txtPackNo.Text & ".csv"
                If dlg.ShowDialog() = DialogResult.OK Then
                    ExportGridToFile(dlg.FileName, ",")
                End If
            End Using
        Catch ex As Exception
            MsgBox("Export error : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    Private Sub ExportGridToFile(filePath As String, delimiter As String)
        Dim sb As New StringBuilder()
        Dim hdrs As New List(Of String)
        For Each col As DataGridViewColumn In flxDetails.Columns
            hdrs.Add(col.HeaderText)
        Next
        sb.AppendLine(String.Join(delimiter, hdrs))
        For Each row As DataGridViewRow In flxDetails.Rows
            Dim cols As New List(Of String)
            For Each cell As DataGridViewCell In row.Cells
                cols.Add(cell.Value?.ToString())
            Next
            sb.AppendLine(String.Join(delimiter, cols))
        Next
        File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8)
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub pnlTotals_Paint(sender As Object, e As PaintEventArgs) Handles pnlTotals.Paint
    End Sub

End Class

'── Helper class for parcel combo (unchanged) ──────────────────────
Public Class ParcelItem
    Public Property ParNo As String
    Public Property Department As String
    Public Sub New(parNo As String, dept As String)
        Me.ParNo = parNo
        Me.Department = dept
    End Sub
    Public Overrides Function ToString() As String
        Return ParNo & "  (" & Department & ")"
    End Function
End Class