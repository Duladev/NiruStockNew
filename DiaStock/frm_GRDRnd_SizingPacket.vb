Imports System.Data.SqlClient
Imports System.Data

Public Class frm_GRDRnd_SizingPacket

    '──────────────────────────────────────────────────────────────
    '  NUMERIC-ONLY HELPER (unchanged)
    '──────────────────────────────────────────────────────────────
    Public Sub NumericOnly(ByVal e As KeyPressEventArgs, ByVal CurrentText As String)
        If Not (Char.IsDigit(e.KeyChar) OrElse Asc(e.KeyChar) = 8 OrElse Asc(e.KeyChar) = 46) Then
            e.Handled = True
        Else
            If e.KeyChar = "." AndAlso CurrentText.Contains(".") Then
                e.Handled = True
            End If
        End If
    End Sub

    '──────────────────────────────────────────────────────────────
    '  FORM LOAD
    '──────────────────────────────────────────────────────────────
    Private Sub frm_Grading_SizingPacket_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.StartPosition = FormStartPosition.CenterScreen
            SetupGrids()
            ClearFields()
            Load_Size_Types()
            Load_Pkt_Types()
            Load_GradingTypes(1)
            Load_GradingTypes(2)
            Load_GradingTypes(3)
            Load_GradingTypes(4)
        Catch ex As Exception
            MsgBox("Error in Form_Load : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    '──────────────────────────────────────────────────────────────
    '  SETUP GRIDS (no DB calls — unchanged)
    '──────────────────────────────────────────────────────────────
    Private Sub SetupGrids()
        SetupGrid(flxType, {"Code", "Color", "Make", "Fluorescent", "Clarity", "Pcs", "Cts", "Bal Pcs", "Bal Cts"},
                           {"Code", "Color", "Make", "Fluor", "Clarity", "Pcs", "Cts", "BalPcs", "BalCts"},
                           {100, 70, 70, 90, 80, 70, 80, 80, 80})

        SetupGrid(flxSelect, {"Pkt No", "Code", "Color", "Make", "Clarity", "Flo", "Pkt Pcs", "Pkt Cts", "Type"},
                             {"PktNo", "Code", "Color", "Make", "Clarity", "Flo", "PktPcs", "PktCts", "Type"},
                             {60, 80, 60, 60, 70, 50, 70, 75, 50})

        SetupGrid(flxDetails, {"Pkt No", "Code", "Color", "Make", "Clarity", "Flo", "Pkt Pcs", "Pkt Cts", "Type"},
                              {"PktNo", "Code", "Color", "Make", "Clarity", "Flo", "PktPcs", "PktCts", "Type"},
                              {60, 80, 60, 60, 70, 50, 70, 75, 50})
    End Sub

    Private Sub SetupGrid(grid As DataGridView, headers() As String, names() As String, widths() As Integer)
        grid.Columns.Clear()
        grid.AutoGenerateColumns = False
        grid.AllowUserToAddRows = False
        grid.AllowUserToDeleteRows = False
        grid.ReadOnly = True
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        grid.BackgroundColor = System.Drawing.Color.White
        grid.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(224, 224, 224)

        For idx As Integer = 0 To headers.Length - 1
            Dim col As New DataGridViewTextBoxColumn()
            col.HeaderText = headers(idx)
            col.Name = names(idx)
            col.Width = widths(idx)
            If idx >= 5 Then
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
            grid.Columns.Add(col)
        Next
    End Sub

    '──────────────────────────────────────────────────────────────
    '  LOAD DROPDOWNS
    '──────────────────────────────────────────────────────────────
    Private Sub Load_Size_Types()
        Try
            cmbSizeType.Items.Clear()
            Dim rsComSql As New ADODB.Recordset()
            rsComSql.Open(
                "SELECT Code FROM tblGrading_RndSizingCodes GROUP BY Code ORDER BY Code",
                AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                ADODB.LockTypeEnum.adLockOptimistic)

            Do While Not rsComSql.EOF
                cmbSizeType.Items.Add(rsComSql.Fields("Code").Value.ToString().Trim())
                rsComSql.MoveNext()
            Loop

            rsComSql.Close()
            rsComSql = Nothing
        Catch ex As Exception
            MsgBox("Error in Load_Size_Types : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    Private Sub Load_Pkt_Types()
        cmbPktType.Items.Clear()
        cmbPktType.Items.AddRange({"N", "B", "C", "M"})
    End Sub

    Private Sub Load_GradingTypes(intSec As Integer)
        Try
            Dim cbo As ComboBox = Nothing
            Select Case intSec
                Case 1 : cbo = cmbType1
                Case 2 : cbo = cmbType2
                Case 3 : cbo = cmbType3
                Case 4 : cbo = cmbType4
            End Select
            If cbo Is Nothing Then Return

            cbo.Items.Clear()
            Dim rsComSql As New ADODB.Recordset()
            rsComSql.Open(
                "SELECT Type FROM tblGrading_RndTypes WHERE Sec=" & intSec & " ORDER BY Type",
                AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                ADODB.LockTypeEnum.adLockOptimistic)

            Do While Not rsComSql.EOF
                cbo.Items.Add(rsComSql.Fields("Type").Value.ToString().Trim())
                rsComSql.MoveNext()
            Loop

            rsComSql.Close()
            rsComSql = Nothing
        Catch ex As Exception
            MsgBox("Error in Load_GradingTypes : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    '──────────────────────────────────────────────────────────────
    '  PARCEL NO — ENTER KEY
    '──────────────────────────────────────────────────────────────
    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True
            If ParcelFound(txtParNo.Text.Trim()) Then
                txtParNo.Text = txtParNo.Text.Trim().ToUpper()
                GetNewPacket()
            Else
                MessageBox.Show("Invalid Parcel", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearFields()
                txtParNo.Focus()
            End If
        End If
    End Sub

    '──────────────────────────────────────────────────────────────
    '  SIZE CODE SELECTED — AUTO FILL COLOR/MAKE/CLARITY
    '──────────────────────────────────────────────────────────────
    Private Sub cmbSizeType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSizeType.SelectedIndexChanged
        If cmbSizeType.Text.Trim() = "" Then Return
        Try
            Dim rsComSql As New ADODB.Recordset()
            rsComSql.Open(
                "SELECT * FROM tblGrading_RndSizingCodes WHERE Code='" & cmbSizeType.Text.Trim() & "'",
                AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                ADODB.LockTypeEnum.adLockOptimistic)

            If Not rsComSql.EOF Then
                cmbType1.Text = rsComSql.Fields("Color").Value.ToString().Trim()
                cmbType2.Text = rsComSql.Fields("Make").Value.ToString().Trim()
                cmbType4.Text = rsComSql.Fields("Clarity").Value.ToString().Trim()
            End If

            rsComSql.Close()
            rsComSql = Nothing
            txtPktPcs.Focus()
        Catch ex As Exception
            MsgBox("Error in cmbSizeType_SelectedIndexChanged : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    '──────────────────────────────────────────────────────────────
    '  PCS / CTS KEY PRESS (unchanged)
    '──────────────────────────────────────────────────────────────
    Private Sub txtPktPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktPcs.KeyPress
        NumericOnly(e, txtPktPcs.Text)
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub txtPktCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktCts.KeyPress
        NumericOnly(e, txtPktCts.Text)
    End Sub

    '──────────────────────────────────────────────────────────────
    '  flxType ROW CLICK — FILL ENTRY FIELDS (no DB, unchanged)
    '──────────────────────────────────────────────────────────────
    Private Sub flxType_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxType.CellClick
        If e.RowIndex < 0 Then Return
        Try
            Dim row As DataGridViewRow = flxType.Rows(e.RowIndex)
            cmbSizeType.Text = row.Cells("Code").Value?.ToString()
            cmbType1.Text = row.Cells("Color").Value?.ToString()
            cmbType2.Text = row.Cells("Make").Value?.ToString()
            cmbType3.Text = row.Cells("Fluor").Value?.ToString()
            cmbType4.Text = row.Cells("Clarity").Value?.ToString()
            txtBalPcs.Text = row.Cells("BalPcs").Value?.ToString()
            txtBalCts.Text = row.Cells("BalCts").Value?.ToString()
            txtPktPcs.Text = row.Cells("BalPcs").Value?.ToString()
            txtPktCts.Text = row.Cells("BalCts").Value?.ToString()
        Catch ex As Exception
            MsgBox("Error in flxType_CellClick : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    '──────────────────────────────────────────────────────────────
    '  flxSelect DOUBLE CLICK — DELETE ROW (no DB, unchanged)
    '──────────────────────────────────────────────────────────────
    Private Sub flxSelect_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxSelect.CellDoubleClick
        If e.RowIndex < 0 Then Return
        If flxSelect.Rows.Count = 0 Then Return

        Dim confirm = MessageBox.Show("Are you sure you want to delete?", Me.Text,
                                      MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirm = DialogResult.Yes Then
            Dim row As DataGridViewRow = flxSelect.Rows(e.RowIndex)
            Dim pcs As Double = Convert.ToDouble(If(row.Cells("PktPcs").Value?.ToString() = "", "0", row.Cells("PktPcs").Value?.ToString()))
            Dim cts As Double = Convert.ToDouble(If(row.Cells("PktCts").Value?.ToString() = "", "0", row.Cells("PktCts").Value?.ToString()))

            txtActPcs.Text = Format(Convert.ToDouble(txtActPcs.Text) - pcs, "#0")
            txtActCts.Text = Format(Convert.ToDouble(txtActCts.Text) - cts, "#0.000")
            txtBalPcs.Text = Format(Convert.ToDouble(txtBalPcs.Text) + pcs, "#0")
            txtBalCts.Text = Format(Convert.ToDouble(txtBalCts.Text) + cts, "#0.000")

            flxSelect.Rows.RemoveAt(e.RowIndex)
        End If
    End Sub

    '──────────────────────────────────────────────────────────────
    '  ADD BUTTON — ADD ROW TO flxSelect (no DB, unchanged)
    '──────────────────────────────────────────────────────────────
    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        If Not ValidateEntry() Then Return

        Dim pcs As Double = Convert.ToDouble(txtPktPcs.Text)
        Dim cts As Double = Convert.ToDouble(txtPktCts.Text)

        flxSelect.Rows.Add(
            txtPktNo.Text,
            cmbSizeType.Text,
            cmbType1.Text,
            cmbType2.Text,
            cmbType4.Text,
            cmbType3.Text,
            pcs.ToString(),
            Format(cts, "#0.000"),
            cmbPktType.Text
        )

        txtActPcs.Text = Format(Convert.ToDouble(txtActPcs.Text) + pcs, "#0")
        txtActCts.Text = Format(Convert.ToDouble(txtActCts.Text) + cts, "#0.000")
        txtBalPcs.Text = Format(Convert.ToDouble(txtBalPcs.Text) - pcs, "#0")
        txtBalCts.Text = Format(Convert.ToDouble(txtBalCts.Text) - cts, "#0.000")

        txtPktPcs.Text = ""
        txtPktCts.Text = ""
    End Sub

    '──────────────────────────────────────────────────────────────
    '  VALIDATE ENTRY (no DB, unchanged)
    '──────────────────────────────────────────────────────────────
    Private Function ValidateEntry() As Boolean
        If txtParNo.Text.Trim() = "" Then
            MessageBox.Show("Invalid Parcel No", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return False
        End If
        If txtPktNo.Text.Trim() = "" Then
            MessageBox.Show("Invalid Packet No", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return False
        End If
        If cmbSizeType.Text.Trim() = "" Then
            MessageBox.Show("Invalid Code", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return False
        End If
        If cmbPktType.Text.Trim() = "" Then
            MessageBox.Show("Invalid Packet Type", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return False
        End If
        If txtPktPcs.Text.Trim() = "" OrElse Convert.ToDouble(txtPktPcs.Text) <= 0 Then
            MessageBox.Show("Invalid Packet Pcs", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return False
        End If
        If txtPktCts.Text.Trim() = "" OrElse Convert.ToDouble(txtPktCts.Text) <= 0 Then
            MessageBox.Show("Invalid Packet Cts", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return False
        End If
        If Convert.ToDouble(txtPktPcs.Text) > Convert.ToDouble(txtBalPcs.Text) Then
            MessageBox.Show("Invalid Packet Pcs — exceeds balance", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return False
        End If
        If Convert.ToDouble(txtPktCts.Text) > Convert.ToDouble(txtBalCts.Text) Then
            MessageBox.Show("Invalid Packet Cts — exceeds balance", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return False
        End If
        Return True
    End Function

    '──────────────────────────────────────────────────────────────
    '  TOOLBAR BUTTONS
    '──────────────────────────────────────────────────────────────
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        ClearFields()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Save()
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        ExportGridToExcel(flxType)
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    '──────────────────────────────────────────────────────────────
    '  SAVE
    '──────────────────────────────────────────────────────────────
    Private Sub Save()
        Try
            If txtParNo.Text.Trim() = "" Then
                MessageBox.Show("Invalid Parcel No", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If txtPktNo.Text.Trim() = "" Then
                MessageBox.Show("Invalid Packet No", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If cmbSizeType.Text.Trim() = "" Then
                MessageBox.Show("Invalid Code", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If cmbPktType.Text.Trim() = "" Then
                MessageBox.Show("Invalid Packet Type", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If txtActPcs.Text.Trim() = "" OrElse Convert.ToDouble(txtActPcs.Text) <= 0 Then
                MessageBox.Show("Invalid Packet Pcs", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If txtActCts.Text.Trim() = "" OrElse Convert.ToDouble(txtActCts.Text) <= 0 Then
                MessageBox.Show("Invalid Packet Cts", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            Dim par As String = txtParNo.Text.Trim()
            Dim pkt As String = txtPktNo.Text.Trim()
            Dim rsComSql As New ADODB.Recordset()

            ' Duplicate check
            rsComSql.Open(
                "SELECT COUNT(*) AS CNT FROM tblGrading_RndSizingPacket " &
                "WHERE ParNo='" & par & "' AND PktNo='" & pkt & "'",
                AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                ADODB.LockTypeEnum.adLockOptimistic)

            If CInt(rsComSql.Fields("CNT").Value) > 0 Then
                rsComSql.Close()
                MessageBox.Show("Already Entered", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            rsComSql.Close()

            ' INSERT
            rsComSql.Open(
                "INSERT INTO tblGrading_RndSizingPacket" &
                "(Department,ParNo,PktNo,SizeCode,PktPcs,PktCts," &
                "ReturnType1,ReturnType2,ReturnType3,ReturnType4,PktType) " &
                "VALUES('Colombo','" & par & "','" & pkt & "'," &
                "'" & cmbSizeType.Text.Trim() & "'," &
                Convert.ToDouble(txtActPcs.Text) & "," &
                Convert.ToDouble(txtActCts.Text) & "," &
                "'" & cmbType1.Text.Trim() & "','" & cmbType2.Text.Trim() & "'," &
                "'" & cmbType3.Text.Trim() & "','" & cmbType4.Text.Trim() & "'," &
                "'" & cmbPktType.Text.Trim() & "')",
                AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                ADODB.LockTypeEnum.adLockOptimistic)
            rsComSql.Close()
            rsComSql = Nothing

            ' Reset entry fields
            txtPktPcs.Text = ""
            txtPktCts.Text = ""
            cmbType1.Text = ""
            cmbType2.Text = ""
            cmbType3.Text = ""
            cmbType4.Text = ""
            cmbSizeType.Text = ""
            cmbPktType.Text = ""
            txtBalPcs.Text = ""
            txtBalCts.Text = ""
            txtActPcs.Text = "0"
            txtActCts.Text = "0"
            flxSelect.Rows.Clear()

            GetNewPacket()

        Catch ex As Exception
            MsgBox("Error in Save : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    '──────────────────────────────────────────────────────────────
    '  PARCEL FOUND CHECK
    '──────────────────────────────────────────────────────────────
    Private Function ParcelFound(parcelNo As String) As Boolean
        Try
            Dim rsComSql As New ADODB.Recordset()
            rsComSql.Open(
                "SELECT COUNT(*) AS CNT FROM tblGrading_RndInvoice WHERE ParcelNo='" & parcelNo & "'",
                AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                ADODB.LockTypeEnum.adLockOptimistic)

            Dim found As Boolean = CInt(rsComSql.Fields("CNT").Value) > 0
            rsComSql.Close()
            rsComSql = Nothing
            Return found
        Catch
            Return False
        End Try
    End Function

    '──────────────────────────────────────────────────────────────
    '  GET NEXT PACKET + LOAD ALL GRIDS
    '──────────────────────────────────────────────────────────────
    Private Sub GetNewPacket()
        Try
            Dim par As String = txtParNo.Text.Trim()
            Dim rsComSql As New ADODB.Recordset()

            ' Next packet number
            rsComSql.Open(
                "SELECT MAX(PktNo) AS MaxPkt FROM tblGrading_RndSizingPacket WHERE ParNo='" & par & "'",
                AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                ADODB.LockTypeEnum.adLockOptimistic)

            If Not rsComSql.EOF AndAlso Not IsDBNull(rsComSql.Fields("MaxPkt").Value) _
               AndAlso rsComSql.Fields("MaxPkt").Value IsNot Nothing Then
                txtPktNo.Text = Format(CInt(rsComSql.Fields("MaxPkt").Value) + 1, "000")
            Else
                txtPktNo.Text = "001"
            End If
            rsComSql.Close()

            ' ── Load flxType — sizing summary ──
            flxType.Rows.Clear()
            txtTotPcs.Text = "0"
            txtTotCts.Text = "0"

            Dim sqlType As String =
                "SELECT TOP (100) PERCENT sc.Code, " &
                "SUM(ct.Pcs) AS TotPcs, ROUND(SUM(ct.Cts),3) AS TotCts " &
                "FROM tblGrading_RndCheckingTypes ct " &
                "INNER JOIN tblGrading_RndCheckingReturns cr " &
                    "ON ct.Department=cr.Department AND ct.ParNo=cr.ParNo " &
                    "AND ct.PktNo=cr.PktNo AND ct.Sec=cr.Sec " &
                "INNER JOIN tblGrading_RndSizingCodesNew sc " &
                    "ON ct.ReturnType1=sc.Color AND ct.ReturnType2=sc.Make " &
                    "AND ct.ReturnType4=sc.Clarity " &
                "WHERE ct.ParNo='" & par & "' AND cr.SecCount=4 " &
                "GROUP BY sc.Code ORDER BY sc.Code"

            rsComSql.Open(sqlType, AdoCN,
                          ADODB.CursorTypeEnum.adOpenKeyset,
                          ADODB.LockTypeEnum.adLockOptimistic)

            Do While Not rsComSql.EOF
                Dim code As String = rsComSql.Fields("Code").Value.ToString()
                Dim totPcs As Double = Convert.ToDouble(rsComSql.Fields("TotPcs").Value)
                Dim totCts As Double = Math.Round(Convert.ToDouble(rsComSql.Fields("TotCts").Value), 3)

                ' Get already packeted pcs/cts for this code
                Dim rsPkt As New ADODB.Recordset()
                rsPkt.Open(
                    "SELECT SUM(PktPcs) AS P, ROUND(SUM(PktCts),3) AS C " &
                    "FROM tblGrading_RndSizingPacket " &
                    "WHERE ParNo='" & par & "' AND SizeCode='" & code & "'",
                    AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                    ADODB.LockTypeEnum.adLockOptimistic)

                Dim pktPcs As Double = 0
                Dim pktCts As Double = 0
                If Not rsPkt.EOF AndAlso Not IsDBNull(rsPkt.Fields("P").Value) Then
                    pktPcs = Convert.ToDouble(rsPkt.Fields("P").Value)
                    pktCts = Math.Round(Convert.ToDouble(rsPkt.Fields("C").Value), 3)
                End If
                rsPkt.Close()
                rsPkt = Nothing

                flxType.Rows.Add(code, "", "", "", "",
                    totPcs.ToString(),
                    Format(totCts, "#0.000"),
                    (totPcs - pktPcs).ToString(),
                    Format(Math.Round(totCts - pktCts, 3), "#0.000"))

                txtTotPcs.Text = Format(Convert.ToDouble(txtTotPcs.Text) + totPcs, "#0")
                txtTotCts.Text = Format(Math.Round(Convert.ToDouble(txtTotCts.Text) + totCts, 3), "#0.000")

                rsComSql.MoveNext()
            Loop
            rsComSql.Close()

            ' ── Load flxDetails — all saved packets ──
            flxDetails.Rows.Clear()
            txtTPktPcs.Text = "0"
            txtTPktCts.Text = "0"

            rsComSql.Open(
                "SELECT * FROM tblGrading_RndSizingPacket WHERE ParNo='" & par & "' ORDER BY PktNo",
                AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                ADODB.LockTypeEnum.adLockOptimistic)

            Do While Not rsComSql.EOF
                Dim pcs As Double = Convert.ToDouble(rsComSql.Fields("PktPcs").Value)
                Dim cts As Double = Math.Round(Convert.ToDouble(rsComSql.Fields("PktCts").Value), 3)

                flxDetails.Rows.Add(
                    rsComSql.Fields("PktNo").Value.ToString(),
                    rsComSql.Fields("SizeCode").Value.ToString(),
                    rsComSql.Fields("ReturnType1").Value.ToString(),
                    rsComSql.Fields("ReturnType2").Value.ToString(),
                    rsComSql.Fields("ReturnType4").Value.ToString(),
                    rsComSql.Fields("ReturnType3").Value.ToString(),
                    pcs.ToString(),
                    Format(cts, "#0.000"),
                    rsComSql.Fields("PktType").Value.ToString()
                )

                txtTPktPcs.Text = Format(Convert.ToDouble(txtTPktPcs.Text) + pcs, "#0")
                txtTPktCts.Text = Format(Math.Round(Convert.ToDouble(txtTPktCts.Text) + cts, 3), "#0.000")
                rsComSql.MoveNext()
            Loop

            rsComSql.Close()
            rsComSql = Nothing

        Catch ex As Exception
            MsgBox("Error in GetNewPacket : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    '──────────────────────────────────────────────────────────────
    '  CLEAR ALL (no DB, unchanged)
    '──────────────────────────────────────────────────────────────
    Private Sub ClearFields()
        txtParNo.Text = ""
        txtPktNo.Text = ""
        txtPktPcs.Text = ""
        txtPktCts.Text = ""
        cmbType1.Text = ""
        cmbType2.Text = ""
        cmbType3.Text = ""
        cmbType4.Text = ""
        cmbSizeType.Text = ""
        cmbPktType.Text = ""
        txtBalPcs.Text = ""
        txtBalCts.Text = ""
        txtActPcs.Text = "0"
        txtActCts.Text = "0"
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        txtTPktPcs.Text = "0"
        txtTPktCts.Text = "0"
        flxType.Rows.Clear()
        flxSelect.Rows.Clear()
        flxDetails.Rows.Clear()
    End Sub

    '──────────────────────────────────────────────────────────────
    '  EXPORT GRID TO EXCEL (no DB, unchanged)
    '──────────────────────────────────────────────────────────────
    Private Sub ExportGridToExcel(grid As DataGridView)
        Try
            Dim sb As New System.Text.StringBuilder()
            For Each col As DataGridViewColumn In grid.Columns
                sb.Append(col.HeaderText & vbTab)
            Next
            sb.AppendLine()
            For Each row As DataGridViewRow In grid.Rows
                For Each cell As DataGridViewCell In row.Cells
                    sb.Append(cell.Value?.ToString() & vbTab)
                Next
                sb.AppendLine()
            Next
            Dim tempFile As String = System.IO.Path.Combine(
                System.IO.Path.GetTempPath(), "SizingExport.xls")
            System.IO.File.WriteAllText(tempFile, sb.ToString())
            ShellEx(tempFile)
        Catch ex As Exception
            MessageBox.Show("Export error: " & ex.Message)
        End Try
    End Sub

    '──────────────────────────────────────────────────────────────
    '  SECONDARY TOOLBAR BUTTONS
    '──────────────────────────────────────────────────────────────
    Private Sub btnnew1_Click(sender As Object, e As EventArgs) Handles btnnew1.Click
        ClearFields()
    End Sub

    Private Sub btnsave1_Click(sender As Object, e As EventArgs) Handles btnsave1.Click
        Save()
    End Sub

    Private Sub btnexport_Click(sender As Object, e As EventArgs) Handles btnexport.Click
        ExportGridToExcel(flxType)
    End Sub

    Private Sub btnexit1_Click(sender As Object, e As EventArgs) Handles btnexit1.Click
        Me.Close()
    End Sub

    '──────────────────────────────────────────────────────────────
    '  STUBS
    '──────────────────────────────────────────────────────────────
    Private Sub txtRecordCount_TextChanged(sender As Object, e As EventArgs) Handles txtRecordCount.TextChanged
    End Sub

    Private Sub pnlEntry_Paint(sender As Object, e As PaintEventArgs) Handles pnlEntry.Paint
    End Sub

    Private Sub txtParNo_TextChanged(sender As Object, e As EventArgs) Handles txtParNo.TextChanged
    End Sub

    Private Sub pnlToolbar_Paint(sender As Object, e As PaintEventArgs) Handles pnlToolbar.Paint
    End Sub

    Private Sub flxType_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxType.CellContentClick
    End Sub

    Private Sub txtBalPcs_TextChanged(sender As Object, e As EventArgs) Handles txtBalPcs.TextChanged
    End Sub

    Private Sub cmbPktType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPktType.SelectedIndexChanged

    End Sub
End Class