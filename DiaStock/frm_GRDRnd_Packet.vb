Public Class frm_GRDRnd_Packet

    ' ── FORM LOAD ───────────────────────────────────────────────────
    Private Sub Frm_GRDRnd_Packet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.StartPosition = FormStartPosition.CenterScreen
            SetupGrid()
            ClearFields()
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

    ' ── SETUP DATAGRIDVIEW COLUMNS (no DB — unchanged) ──────────────
    Private Sub SetupGrid()
        flxPacket.Columns.Clear()
        flxPacket.AutoGenerateColumns = False
        flxPacket.AllowUserToAddRows = False
        flxPacket.AllowUserToDeleteRows = False
        flxPacket.ReadOnly = True
        flxPacket.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        flxPacket.BackgroundColor = System.Drawing.Color.White
        flxPacket.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(224, 224, 224)

        Dim cols() As String = {"Parcel No", "Pkt No", "Pkt Pcs", "Pkt Cts", "Make"}
        Dim names() As String = {"ParNo", "PktNo", "PktPcs", "PktCts", "PktType"}
        Dim widths() As Integer = {120, 80, 80, 80, 160}

        For i As Integer = 0 To cols.Length - 1
            Dim col As New DataGridViewTextBoxColumn()
            col.HeaderText = cols(i)
            col.Name = names(i)
            col.Width = widths(i)
            If i >= 2 AndAlso i <= 3 Then
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
            flxPacket.Columns.Add(col)
        Next
    End Sub

    ' ── PARCEL NO — ENTER KEY ────────────────────────────────────────
    Private Sub TxtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True
            If ParcelFound(txtParNo.Text.Trim()) Then
                txtParNo.Text = txtParNo.Text.Trim().ToUpper()
                GetNewPacket()
                txtPktPcs.Focus()
            Else
                MessageBox.Show("Invalid Parcel", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearFields()
                txtParNo.Focus()
            End If
        End If
    End Sub

    ' ── PACKET NO — ENTER KEY (LOAD EXISTING PACKET) ────────────────
    Private Sub TxtPktNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktNo.KeyPress
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True
            Dim rs As New ADODB.Recordset
            Try
                Dim parQ As String = txtParNo.Text.Trim().Replace("'", "''")
                Dim pktQ As String = txtPktNo.Text.Trim().Replace("'", "''")

                rs.Open("SELECT * FROM tblGrading_RndPacket " &
                        "WHERE ParNo='" & parQ & "' AND PktNo='" & pktQ & "'",
                        AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

                If Not rs.EOF Then
                    txtPktPcs.Text = rs.Fields("PktPcs").Value.ToString()
                    txtPktCts.Text = Format(Convert.ToDouble(rs.Fields("PktCts").Value), "#0.000")
                    Dim pktType As String = rs.Fields("PktType").Value.ToString()
                    For Each rb As RadioButton In grpModel.Controls.OfType(Of RadioButton)()
                        If rb.Text = pktType Then rb.Checked = True
                    Next
                Else
                    MessageBox.Show("Invalid Parcel and Packet", Me.Text,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Catch ex As Exception
                MsgBox("Error in txtPktNo_KeyPress : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Finally
                If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
                rs = Nothing
            End Try
        End If
    End Sub

    ' ── PCS / CTS KEY PRESS ─────────────────────────────────────────
    Private Sub txtPktPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktPcs.KeyPress
        NumericOnly(e, txtPktPcs.Text)
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True
            txtPktCts.Focus()
        End If
    End Sub

    Private Sub txtPktCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktCts.KeyPress
        NumericOnly(e, txtPktCts.Text)
    End Sub

    ' ── TOOLBAR BUTTONS ─────────────────────────────────────────────
    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        ClearFields()
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Save()
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    ' ── SAVE ───────────────────────────────────────────────────────
    Private Sub Save()
        Dim rs As New ADODB.Recordset
        Dim cmd As New ADODB.Command
        Try
            If Not ParcelFound(txtParNo.Text.Trim()) Then Return

            'If cmbSize.Text.Trim() = "" Then
            'MessageBox.Show("Please select the Size", Me.Text,
            'MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            'End If
            If txtPktPcs.Text.Trim() = "" Then
                MessageBox.Show("Please enter the Pcs", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If txtPktCts.Text.Trim() = "" Then
                MessageBox.Show("Please enter the Cts", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            Dim parQ As String = txtParNo.Text.Trim().Replace("'", "''")
            Dim pktQ As String = txtPktNo.Text.Trim().Replace("'", "''")

            ' Get total pcs in parcel from tblGrading_Rndinvoice
            Dim intTotPcs As Double = 0
            rs.Open("SELECT SUM(PolPcs) AS TotPcs FROM tblGrading_Rndinvoice WHERE ParcelNo='" & parQ & "'",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            If Not rs.EOF AndAlso Not IsDBNull(rs.Fields("TotPcs").Value) Then
                intTotPcs = Convert.ToDouble(rs.Fields("TotPcs").Value)
            End If
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()

            ' Get already issued pcs from tblGrading_RndPacket
            Dim intIssPcs As Double = 0
            rs.Open("SELECT SUM(PktPcs) AS TotPcs FROM tblGrading_RndPacket WHERE ParNo='" & parQ & "'",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            If Not rs.EOF AndAlso Not IsDBNull(rs.Fields("TotPcs").Value) Then
                intIssPcs = Convert.ToDouble(rs.Fields("TotPcs").Value)
            End If
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()

            If intTotPcs < intIssPcs + Convert.ToDouble(txtPktPcs.Text) Then
                MessageBox.Show("Not enough Pcs in the Parcel", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            ' Check if packet already exists
            rs.Open("SELECT COUNT(*) AS Cnt FROM tblGrading_RndPacket " &
                    "WHERE ParNo='" & parQ & "' AND PktNo='" & pktQ & "'",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            Dim exists As Boolean = Not rs.EOF AndAlso (CInt(rs.Fields("Cnt").Value) > 0)
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()

            If exists Then
                MessageBox.Show("Already Existing Packet", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            ' Get selected model from radio buttons
            Dim strModel As String = ""
            For Each rb As RadioButton In grpModel.Controls.OfType(Of RadioButton)()
                If rb.Checked Then strModel = rb.Text : Exit For
            Next

            ' Ensure parcel record exists in tblGrading_RndParcel
            rs.Open("SELECT COUNT(*) AS Cnt FROM tblGrading_RndParcel WHERE ParNo='" & parQ & "'",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            Dim parcelExists As Boolean = Not rs.EOF AndAlso (CInt(rs.Fields("Cnt").Value) > 0)
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()

            cmd.ActiveConnection = AdoCN

            If Not parcelExists Then
                cmd.CommandText =
                    "INSERT INTO tblGrading_RndParcel(ParNo,Complete) VALUES('" & parQ & "',0)"
                cmd.Execute()
            End If

            ' Insert the new packet
            Dim modelQ As String = strModel.Replace("'", "''")
            'Dim sizeQ As String = cmbSize.Text.Trim().Replace("'", "''")
            Dim parUpr As String = txtParNo.Text.Trim().ToUpper().Replace("'", "''")

            cmd.CommandText =
                "INSERT INTO tblGrading_RndPacket(Department,ParNo,PktNo,PktPcs,PktCts,PktType,PktSize) " &
                "VALUES('Colombo','" & parUpr & "','" & pktQ & "'," &
                Convert.ToDouble(txtPktPcs.Text) & "," &
                Convert.ToDouble(txtPktCts.Text) & ",'"
            'modelQ & "','" & sizeQ & "')"
            cmd.Execute()

            ' Reset entry fields
            txtPktNo.Text = ""
            txtPktPcs.Text = ""
            txtPktCts.Text = ""
            'cmbSize.Text = ""

            ' Reset radio buttons to default
            For Each rb As RadioButton In grpModel.Controls.OfType(Of RadioButton)()
                rb.Checked = (rb.Text = "Niru Make")
            Next

            GetNewPacket()

        Catch ex As Exception
            MsgBox("Error in Save : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
            cmd = Nothing
        End Try
    End Sub

    ' ── PARCEL FOUND CHECK ──────────────────────────────────────────
    Private Function ParcelFound(parcelNo As String) As Boolean
        Dim rs As New ADODB.Recordset
        Try
            Dim parQ As String = parcelNo.Replace("'", "''")
            rs.Open("SELECT COUNT(*) AS Cnt FROM tblGrading_Rndinvoice WHERE ParcelNo='" & parQ & "'",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            Return Not rs.EOF AndAlso (CInt(rs.Fields("Cnt").Value) > 0)
        Catch
            Return False
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Function

    ' ── GET NEXT PACKET NO + REFRESH GRID ───────────────────────────
    Private Sub GetNewPacket()
        Dim rs As New ADODB.Recordset
        Try
            Dim parQ As String = txtParNo.Text.Trim().Replace("'", "''")

            ' Get next packet number
            rs.Open("SELECT MAX(PktNo) AS MaxPktNo FROM tblGrading_RndPacket WHERE ParNo='" & parQ & "'",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            If Not rs.EOF AndAlso Not IsDBNull(rs.Fields("MaxPktNo").Value) Then
                txtPktNo.Text = Format(CInt(rs.Fields("MaxPktNo").Value) + 1, "000")
            Else
                txtPktNo.Text = "001"
            End If
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()

            ' Load grid
            flxPacket.Rows.Clear()
            rs.Open("SELECT ParNo, PktNo, PktPcs, PktCts, PktType FROM tblGrading_RndPacket " &
                    "WHERE ParNo='" & parQ & "' ORDER BY PktNo",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            Do While Not rs.EOF
                flxPacket.Rows.Add(
                    rs.Fields("ParNo").Value.ToString(),
                    rs.Fields("PktNo").Value.ToString(),
                    rs.Fields("PktPcs").Value.ToString(),
                    Format(Convert.ToDouble(rs.Fields("PktCts").Value), "#0.000"),
                    rs.Fields("PktType").Value.ToString()
                )
                rs.MoveNext()
            Loop

            txtRecordCount.Text = "Records : " & flxPacket.Rows.Count

        Catch ex As Exception
            MsgBox("Error in GetNewPacket : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub

    ' ── CLEAR ALL FIELDS (no DB — unchanged) ────────────────────────
    Private Sub ClearFields()
        txtParNo.Text = ""
        txtPktNo.Text = ""
        txtPktPcs.Text = ""
        txtPktCts.Text = ""
        'cmbSize.Text = ""
        flxPacket.Rows.Clear()
        txtRecordCount.Text = "Record Count"
        For Each rb As RadioButton In grpModel.Controls.OfType(Of RadioButton)()
            rb.Checked = (rb.Text = "Niru Make")
        Next
    End Sub

    ' ── STUB HANDLERS (no logic — retained as-is) ───────────────────
    Private Sub TxtParNo_TextChanged(sender As Object, e As EventArgs) Handles txtParNo.TextChanged
    End Sub

    Private Sub LblTitle_Click(sender As Object, e As EventArgs) Handles lblTitle.Click
    End Sub

    Private Sub CmbSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSize.SelectedIndexChanged
    End Sub

    Private Sub pnlEntry_Paint(sender As Object, e As PaintEventArgs) Handles pnlEntry.Paint

    End Sub
End Class