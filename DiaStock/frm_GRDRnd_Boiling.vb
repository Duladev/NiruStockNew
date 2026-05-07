Public Class frm_GRDRnd_Boiling

    '  FORM LEVEL VARIABLES 
    Private issued As Boolean = False
    Private Checked As Boolean = False
    Private frmnew As Boolean = True
    Private Section As Integer = 0
    Private intSecCount As Integer = 1
    Private ICNo2 As String = ""

    Private Const GRID_COLOR As Integer = 0
    Private Const GRID_MAKE As Integer = 1
    Private Const GRID_FLO As Integer = 2
    Private Const GRID_CLARITY As Integer = 3
    Private Const GRID_PCS As Integer = 4
    Private Const GRID_CTS As Integer = 5

    Private Const RET_COLOR As Integer = 0
    Private Const RET_MAKE As Integer = 1
    Private Const RET_FLO As Integer = 2
    Private Const RET_CLARITY As Integer = 3
    Private Const RET_PCS As Integer = 4
    Private Const RET_CTS As Integer = 5

    Private Const EMP_NO As Integer = 0
    Private Const EMP_PCS As Integer = 1

    '  FORM LOAD 
    Private Sub frm_Grading_Boiling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.StartPosition = FormStartPosition.CenterScreen
            SetupGrids()
            Load_Section()
            LoadDepartments()
            Load_GradingTypes(1)
            Load_GradingTypes(2)
            Load_GradingTypes(3)
            Load_GradingTypes(4)
            frmInitialze()
        Catch ex As Exception
            MsgBox("Error in Form_Load : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    '  SETUP GRIDS 
    Private Sub SetupGrids()
        SetupGrid(flxType,
            {"Color", "Make", "Fluorescent", "Clarity", "Pcs", "Cts"},
            {"Col0", "Col1", "Col2", "Col3", "Col4", "Col5"},
            {120, 100, 100, 120, 60, 80})

        SetupGrid(flxReturns,
            {"Color", "Make", "Fluorescent", "Clarity", "Pcs", "Cts"},
            {"R0", "R1", "R2", "R3", "R4", "R5"},
            {120, 100, 100, 120, 60, 80})

        SetupGrid(flxEmp,
            {"Emp No", "Pcs"},
            {"EmpNo", "EmpPcs"},
            {100, 80})
    End Sub

    Private Sub SetupGrid(grid As DataGridView,
                          headers() As String,
                          names() As String,
                          widths() As Integer)
        grid.Columns.Clear()
        grid.AutoGenerateColumns = False
        grid.AllowUserToAddRows = False
        grid.AllowUserToDeleteRows = False
        grid.ReadOnly = True
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        grid.BackgroundColor = System.Drawing.Color.White
        grid.RowTemplate.Height = 20

        With grid.ColumnHeadersDefaultCellStyle
            .BackColor = System.Drawing.Color.FromArgb(50, 100, 160)
            .ForeColor = System.Drawing.Color.White
            .Font = New System.Drawing.Font("Tahoma", 8.0F, System.Drawing.FontStyle.Bold)
        End With
        grid.EnableHeadersVisualStyles = False

        grid.AlternatingRowsDefaultCellStyle.BackColor =
            System.Drawing.Color.FromArgb(235, 245, 255)

        grid.Font = New System.Drawing.Font("Trebuchet MS", 8.25F)

        For i As Integer = 0 To headers.Length - 1
            Dim col As New DataGridViewTextBoxColumn()
            col.HeaderText = headers(i)
            col.Name = names(i)
            col.Width = widths(i)
            grid.Columns.Add(col)
        Next
    End Sub

    '  LOAD SECTIONS 
    Private Sub Load_Section()
        Dim rs As New ADODB.Recordset
        Try
            cmbSection.Items.Clear()
            rs.Open("SELECT SecName FROM tblGrading_RndSections WHERE Seq < 6 ORDER BY Seq",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            Do While Not rs.EOF
                cmbSection.Items.Add(rs.Fields("SecName").Value.ToString().Trim())
                rs.MoveNext()
            Loop
            If cmbSection.Items.Count > 0 Then cmbSection.SelectedIndex = 0
        Catch ex As Exception
            MsgBox("Error in Load_Section : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub

    '  LOAD DEPARTMENTS 
    Private Sub LoadDepartments()
        Dim rs As New ADODB.Recordset
        Try
            cmbDept.Items.Clear()
            rs.Open("SELECT DISTINCT Department FROM tblGrading_RndCheckingIssues ORDER BY Department",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            Do While Not rs.EOF
                cmbDept.Items.Add(rs.Fields("Department").Value.ToString().Trim())
                rs.MoveNext()
            Loop
            If cmbDept.Items.Count = 0 Then cmbDept.Items.Add("Colombo")
            cmbDept.SelectedIndex = 0
        Catch ex As Exception
            MsgBox("Error in LoadDepartments : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub

    '  LOAD GRADING TYPES 
    Private Sub Load_GradingTypes(intSec As Integer)
        Dim rs As New ADODB.Recordset
        Try
            rs.Open("SELECT [Type] FROM tblGrading_RndTypes WHERE Sec=" & intSec & " ORDER BY [Type]",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            Do While Not rs.EOF
                Dim t As String = rs.Fields("Type").Value.ToString().Trim()
                Select Case intSec
                    Case 1 : cmbColor.Items.Add(t)
                    Case 2 : cmbMake.Items.Add(t)
                    Case 3 : cmbFlo.Items.Add(t)
                    Case 4 : cmbClarity.Items.Add(t)
                End Select
                rs.MoveNext()
            Loop
        Catch ex As Exception
            MsgBox("Error in Load_GradingTypes : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub

    '  CLEAR / INITIALISE FORM 
    Private Sub frmInitialze()
        txtParNo.Text = "" : txtPktNo.Text = "" : txtemp.Text = ""
        txtRetCts.Text = "" : txtRetTap.Text = ""
        txtIssCts.Text = "" : txtIssTap.Text = ""
        txtRej.Text = "" : txtLostPcs.Text = ""
        txtIssDate.Text = "" : txtIssTime.Text = ""
        txtRetTime.Text = "" : txtRetDate.Text = ""
        txtRejCts.Text = "" : txtLostCts.Text = ""
        txtRepPcs.Text = "" : txtRepCts.Text = ""
        txtTotPcs.Text = "0" : txtTotCts.Text = "0"
        txtTypePcs.Text = "" : txtTypeCts.Text = ""
        txtTotPcs2.Text = "0"
        txtEmpNo.Text = "" : txtEmpPcs.Text = ""

        cmbColor.SelectedIndex = -1
        cmbMake.SelectedIndex = -1
        cmbFlo.SelectedIndex = -1
        cmbClarity.SelectedIndex = -1
        If cmbSection.Items.Count > 0 Then cmbSection.SelectedIndex = 0

        flxType.Rows.Clear()
        flxReturns.Rows.Clear()
        flxEmp.Rows.Clear()

        pnlIssues.Visible = False
        pnlReturns.Visible = False

        issued = False : Checked = False
        frmnew = True : Section = 0
        intSecCount = 1 : ICNo2 = ""
    End Sub

    '  SECTION COMBO CHANGE 
    Private Sub cmbSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSection.SelectedIndexChanged
        cmbColor.SelectedIndex = -1
        cmbMake.SelectedIndex = -1
        cmbFlo.SelectedIndex = -1
        cmbClarity.SelectedIndex = -1
        flxType.Rows.Clear()
        txtTotPcs.Text = "0" : txtTotCts.Text = "0"
        txtTypePcs.Text = "" : txtTypeCts.Text = ""
        txtTotPcs2.Text = "0"
    End Sub

    '  PARCEL NO KEY PRESS 
    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True
            txtParNo.Text = txtParNo.Text.Trim().ToUpper()
            If ParcelFound(txtParNo.Text) Then
                txtPktNo.Text = "" : txtPktNo.Focus()
            Else
                MessageBox.Show("Invalid Parcel No.", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtParNo.Text = "" : txtPktNo.Text = "" : txtParNo.Focus()
            End If
        End If
    End Sub

    ' ── PACKET NO KEY PRESS 
    Private Sub txtPktNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktNo.KeyPress
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True
            If txtParNo.Text <> "" AndAlso txtPktNo.Text <> "" Then
                txtParNo.Text = txtParNo.Text.Trim().ToUpper()
                If ParcelFound(txtParNo.Text) Then
                    Load_ParcelDetails()
                    txtemp.Focus()
                Else
                    MessageBox.Show("Invalid Parcel No.", Me.Text,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtParNo.Text = "" : txtPktNo.Text = "" : txtParNo.Focus()
                End If
            End If
        End If
    End Sub

    '  PARCEL EXISTS CHECK 
    Private Function ParcelFound(parcelNo As String) As Boolean
        Dim rs As New ADODB.Recordset
        Try
            Dim parQ As String = parcelNo.Replace("'", "''")
            rs.Open("SELECT COUNT(*) AS CNT FROM tblGrading_RndInvoice WHERE ParcelNo='" & parQ & "'",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            Return Not rs.EOF AndAlso (CInt(rs.Fields("CNT").Value) > 0)
        Catch
            Return False
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Function

    '  LOAD PARCEL DETAILS 
    Private Sub Load_ParcelDetails()
        Dim rs As New ADODB.Recordset
        Try
            issued = True
            Checked = False
            Section = 0

            Dim par As String = txtParNo.Text.Trim().Replace("'", "''")
            Dim pkt As String = txtPktNo.Text.Trim().Replace("'", "''")
            Dim dept As String = cmbDept.Text.Trim().Replace("'", "''")

            Dim intIssPcsC As Double = 0
            rs.Open("SELECT TOP 1 * FROM tblGrading_RndCheckingIssues " &
                    "WHERE ParNo='" & par & "' AND PktNo='" & pkt & "' " &
                    "AND Department='" & dept & "' ORDER BY ID DESC",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

            If Not rs.EOF Then
                Checked = True
                intIssPcsC = CDbl(If(rs.Fields("IssPcs").Value Is DBNull.Value, 0, rs.Fields("IssPcs").Value))
                Section = CInt(rs.Fields("Sec").Value)
                intSecCount = CInt(rs.Fields("SecCount").Value)
                If cmbSection.Items.Count > Section - 1 Then
                    cmbSection.SelectedIndex = Section - 1
                End If
            End If
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()

            If Not Checked Then
                GetNewPacket()
                intSecCount = 1
                Return
            End If

            rs.Open("SELECT * FROM tblGrading_RndCheckingReturns " &
                    "WHERE ParNo='" & par & "' AND PktNo='" & pkt & "' " &
                    "AND Department='" & dept & "' AND Sec=" & Section,
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

            If Not rs.EOF Then
                Dim retTotal As Double =
                    CDbl(If(rs.Fields("RetPcs").Value Is DBNull.Value, 0, rs.Fields("RetPcs").Value)) +
                    CDbl(If(rs.Fields("RepPcs").Value Is DBNull.Value, 0, rs.Fields("RepPcs").Value)) +
                    CDbl(If(rs.Fields("LostPcs").Value Is DBNull.Value, 0, rs.Fields("LostPcs").Value)) +
                    CDbl(If(rs.Fields("RejPcs").Value Is DBNull.Value, 0, rs.Fields("RejPcs").Value))
                If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()

                If intIssPcsC = retTotal Then
                    Dim nextSec As Integer = Section + 1
                    If nextSec <= cmbSection.Items.Count Then
                        cmbSection.SelectedIndex = nextSec - 1
                        Section = nextSec
                        intSecCount += 1
                        ICNo2 = ""
                        txtemp.Text = ""
                        txtIssDate.Text = Date.Now.ToString("dd/MM/yyyy")
                        txtIssTime.Text = Date.Now.ToString("HH:mm")
                        txtIssTap.Text = ""
                        txtIssCts.Text = ""
                        pnlIssues.Visible = True
                        pnlReturns.Visible = False
                        flxType.Rows.Clear()
                        flxReturns.Rows.Clear()
                        flxEmp.Rows.Clear()
                        txtTotPcs.Text = "0" : txtTotCts.Text = "0"
                        txtTotPcs2.Text = "0"
                        Checked = False
                        GetNewPacket()
                    Else
                        MessageBox.Show("Packet Finished", Me.Text,
                                        MessageBoxButtons.OK, MessageBoxIcon.Information)
                        frmInitialze()
                    End If
                Else
                    MessageBox.Show("Stones Mismatch in previous return. Please check.",
                                    Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                Return
            End If
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()

            rs.Open("SELECT * FROM tblGrading_RndCheckingIssues " &
                    "WHERE ParNo='" & par & "' AND PktNo='" & pkt & "' " &
                    "AND Department='" & dept & "' AND Sec=" & Section,
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

            If Not rs.EOF Then
                txtIssTap.Text = rs.Fields("IssPcs").Value.ToString()
                txtIssCts.Text = Format(CDbl(rs.Fields("IssCts").Value), "#0.000")
                txtIssDate.Text = Format(CDate(rs.Fields("IssDate").Value), "dd/MM/yyyy")
                txtIssTime.Text = Format(CDate(rs.Fields("IssTime").Value), "HH:mm")
                ICNo2 = rs.Fields("EmpNo").Value.ToString().Trim()
                txtemp.Text = ICNo2
                intSecCount = CInt(rs.Fields("SecCount").Value)
            End If
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()

            Load_ReturnsGrid()
            pnlIssues.Visible = True
            pnlReturns.Visible = True

        Catch ex As Exception
            MsgBox("Error in Load_ParcelDetails : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub

    '  GET NEW PACKET 
    Private Sub GetNewPacket()
        Dim rs As New ADODB.Recordset
        Try
            Dim par As String = txtParNo.Text.Trim().Replace("'", "''")
            Dim pkt As String = txtPktNo.Text.Trim().Replace("'", "''")
            rs.Open("SELECT * FROM tblGrading_RndPacket " &
                    "WHERE ParNo='" & par & "' AND PktNo='" & pkt & "'",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            If Not rs.EOF Then
                txtIssTap.Text = rs.Fields("PktPcs").Value.ToString()
                txtIssCts.Text = Format(CDbl(rs.Fields("PktCts").Value), "#0.000")
                txtIssDate.Text = Date.Now.ToString("dd/MM/yyyy")
                txtIssTime.Text = Date.Now.ToString("HH:mm")
                pnlIssues.Visible = True
                pnlReturns.Visible = False
                issued = True : Checked = False
            Else
                MessageBox.Show("Invalid Packet", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                frmInitialze()
            End If
        Catch ex As Exception
            MsgBox("Error in GetNewPacket : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub

    '  LOAD RETURNS GRID 
    Private Sub Load_ReturnsGrid()
        Dim rs As New ADODB.Recordset
        Try
            flxReturns.Rows.Clear()
            Dim par As String = txtParNo.Text.Trim().Replace("'", "''")
            Dim pkt As String = txtPktNo.Text.Trim().Replace("'", "''")
            Dim dept As String = cmbDept.Text.Trim().Replace("'", "''")
            Dim refSec As Integer = If(Section > 1, Section - 1, Section)

            rs.Open("SELECT * FROM tblGrading_RndCheckingTypes " &
                    "WHERE ParNo='" & par & "' AND PktNo='" & pkt & "' " &
                    "AND Department='" & dept & "' AND Sec=" & refSec & " ORDER BY ID",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            Do While Not rs.EOF
                flxReturns.Rows.Add(
                    rs.Fields("ReturnType1").Value.ToString().Trim(),
                    rs.Fields("ReturnType2").Value.ToString().Trim(),
                    rs.Fields("ReturnType3").Value.ToString().Trim(),
                    rs.Fields("ReturnType4").Value.ToString().Trim(),
                    rs.Fields("Pcs").Value.ToString(),
                    Format(CDbl(rs.Fields("Cts").Value), "#0.000"))
                rs.MoveNext()
            Loop
        Catch ex As Exception
            MsgBox("Error in Load_ReturnsGrid : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub

    '  EMP NO — ENTER KEY 
    Private Sub txtemp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtemp.KeyPress
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True
            If txtemp.Text.Trim().Length = 6 Then
                ' IC number mismatch check
                If issued AndAlso Checked AndAlso ICNo2 <> "" AndAlso
                   ICNo2.Trim() <> txtemp.Text.Trim() Then
                    Dim resp As DialogResult =
                        MessageBox.Show("IC Numbers do not match. Proceed Anyway?",
                                        Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If resp = DialogResult.No Then
                        txtemp.Text = "" : txtemp.Focus() : Return
                    End If
                End If

                If issued AndAlso Checked Then
                    txtRetDate.Text = Date.Now.ToString("dd/MM/yyyy")
                    txtRetTime.Text = Date.Now.ToString("HH:mm")
                Else
                    txtIssDate.Text = Date.Now.ToString("dd/MM/yyyy")
                    txtIssTime.Text = Date.Now.ToString("HH:mm")
                    txtIssTap.Focus()
                End If
            Else
                MessageBox.Show("Invalid Emp. No.", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtemp.Text = "" : txtemp.Focus()
            End If
        End If
    End Sub

    '  ADD TYPE ROW 
    Private Sub cmdTypeAdd_Click(sender As Object, e As EventArgs) Handles cmdTypeAdd.Click
        Try
            If txtTypePcs.Text.Trim() = "" OrElse txtTypeCts.Text.Trim() = "" Then
                MessageBox.Show("Please check the input entries", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If Convert.ToSingle(txtTypePcs.Text) <= 0 OrElse Convert.ToDouble(txtTypeCts.Text) <= 0 Then
                MessageBox.Show("Invalid Pcs / Cts", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            Dim totPcs As Single = 0
            Dim totCts As Double = 0
            For Each row As DataGridViewRow In flxType.Rows
                If row.IsNewRow Then Continue For
                totPcs += CSngSafe(row.Cells("Col4").Value?.ToString())
                totCts += CDblSafe(row.Cells("Col5").Value?.ToString())
            Next

            If totPcs + Convert.ToSingle(txtTypePcs.Text) > Convert.ToSingle(txtIssTap.Text) Then
                MessageBox.Show("Pcs Invalid", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If Math.Round(totCts + Convert.ToDouble(txtTypeCts.Text), 3) >
               Math.Round(Convert.ToDouble(txtIssCts.Text), 3) Then
                MessageBox.Show("Cts Invalid", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            flxType.Rows.Add(
                cmbColor.Text.Trim(), cmbMake.Text.Trim(),
                cmbFlo.Text.Trim(), cmbClarity.Text.Trim(),
                txtTypePcs.Text.Trim(),
                Format(Convert.ToDouble(txtTypeCts.Text), "#0.000"))

            txtTotPcs.Text = (Convert.ToSingle(txtTotPcs.Text) + Convert.ToSingle(txtTypePcs.Text)).ToString()
            txtTotCts.Text = Format(Convert.ToDouble(txtTotCts.Text) + Convert.ToDouble(txtTypeCts.Text), "#0.000")
            txtRetTap.Text = txtTotPcs.Text
            txtRetCts.Text = txtTotCts.Text

            cmbColor.SelectedIndex = -1 : cmbMake.SelectedIndex = -1
            cmbFlo.SelectedIndex = -1 : cmbClarity.SelectedIndex = -1
            txtTypePcs.Text = "" : txtTypeCts.Text = ""
        Catch ex As Exception
            MsgBox("Error in cmdTypeAdd_Click : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    '  ADD EMPLOYEE ROW 

    Private Sub cmdEmpAdd_Click(sender As Object, e As EventArgs) Handles cmdEmpAdd.Click
        Try
            If txtEmpNo.Text.Trim() = "" OrElse txtEmpPcs.Text.Trim() = "" Then
                MessageBox.Show("Invalid Emp. No./Pcs", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If txtEmpNo.Text.Trim().Length <> 6 Then
                MessageBox.Show("Invalid Emp No", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If CDblSafe(txtEmpPcs.Text) <= 0 Then
                MessageBox.Show("Invalid Pcs", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            For Each row As DataGridViewRow In flxEmp.Rows
                If row.IsNewRow Then Continue For
                If row.Cells("EmpNo").Value?.ToString() = txtEmpNo.Text.Trim() Then
                    MessageBox.Show("Already Entered", Me.Text,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
                End If
            Next

            Dim totPcs As Double = 0
            For Each row As DataGridViewRow In flxEmp.Rows
                If row.IsNewRow Then Continue For
                totPcs += CDblSafe(row.Cells("EmpPcs").Value?.ToString())
            Next

            If CDblSafe(txtTotPcs.Text) >= totPcs + CDblSafe(txtEmpPcs.Text) Then
                flxEmp.Rows.Add(txtEmpNo.Text.Trim(), txtEmpPcs.Text.Trim())
                txtTotPcs2.Text = (totPcs + CDblSafe(txtEmpPcs.Text)).ToString()
                txtEmpNo.Text = "" : txtEmpPcs.Text = "" : txtEmpNo.Focus()
            Else
                MessageBox.Show("Invalid Pcs", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MsgBox("Error in cmdEmpAdd_Click : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    '  SELECT ALL RETURNS 
    Private Sub cmdSelectAll_Click(sender As Object, e As EventArgs) Handles cmdSelectAll.Click
        Try
            If flxType.Rows.Count > 0 Then
                MessageBox.Show("Already Selected", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If flxReturns.Rows.Count = 0 Then
                MessageBox.Show("No Records to select", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If MessageBox.Show("Are you sure?", Me.Text,
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return

            For Each row As DataGridViewRow In flxReturns.Rows
                If row.IsNewRow Then Continue For
                flxType.Rows.Add(
                    If(cmbColor.Text.Trim() <> "", cmbColor.Text.Trim(), row.Cells("R0").Value?.ToString()),
                    If(cmbMake.Text.Trim() <> "", cmbMake.Text.Trim(), row.Cells("R1").Value?.ToString()),
                    If(cmbFlo.Text.Trim() <> "", cmbFlo.Text.Trim(), row.Cells("R2").Value?.ToString()),
                    If(cmbClarity.Text.Trim() <> "", cmbClarity.Text.Trim(), row.Cells("R3").Value?.ToString()),
                    row.Cells("R4").Value?.ToString(),
                    row.Cells("R5").Value?.ToString())
                txtTotPcs.Text = (Convert.ToSingle(txtTotPcs.Text) +
                                  CSngSafe(row.Cells("R4").Value?.ToString())).ToString()
                txtTotCts.Text = Format(Convert.ToDouble(txtTotCts.Text) +
                                        CDblSafe(row.Cells("R5").Value?.ToString()), "#0.000")
            Next
            txtRetTap.Text = txtTotPcs.Text : txtRetCts.Text = txtTotCts.Text
            cmbColor.SelectedIndex = -1 : cmbMake.SelectedIndex = -1
            cmbFlo.SelectedIndex = -1 : cmbClarity.SelectedIndex = -1
            txtTypePcs.Text = "" : txtTypeCts.Text = ""
        Catch ex As Exception
            MsgBox("Error in cmdSelectAll_Click : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    '  DOUBLE CLICK DELETE — flxType 
    Private Sub flxType_DoubleClick(sender As Object, e As EventArgs) Handles flxType.DoubleClick
        If flxType.Rows.Count > 0 AndAlso flxType.CurrentRow IsNot Nothing Then
            If MessageBox.Show("Are you sure you want to Delete?", Me.Text,
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Dim row As DataGridViewRow = flxType.CurrentRow
                txtTotPcs.Text = (Convert.ToSingle(txtTotPcs.Text) -
                                  CSngSafe(row.Cells("Col4").Value?.ToString())).ToString()
                txtTotCts.Text = Format(Convert.ToDouble(txtTotCts.Text) -
                                        CDblSafe(row.Cells("Col5").Value?.ToString()), "#0.000")
                txtRetTap.Text = txtTotPcs.Text : txtRetCts.Text = txtTotCts.Text
                flxType.Rows.Remove(row)
                If flxType.Rows.Count = 0 Then
                    txtTotPcs.Text = "0" : txtTotCts.Text = "0"
                    txtRetTap.Text = "0" : txtRetCts.Text = "0"
                End If
            End If
        End If
    End Sub

    '  DOUBLE CLICK DELETE — flxEmp 
    Private Sub flxEmp_DoubleClick(sender As Object, e As EventArgs) Handles flxEmp.DoubleClick
        If flxEmp.Rows.Count > 0 AndAlso flxEmp.CurrentRow IsNot Nothing Then
            If MessageBox.Show("Are you sure you want to Delete?", Me.Text,
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                flxEmp.Rows.Remove(flxEmp.CurrentRow)
                Dim tot As Double = 0
                For Each row As DataGridViewRow In flxEmp.Rows
                    If row.IsNewRow Then Continue For
                    tot += CDblSafe(row.Cells("EmpPcs").Value?.ToString())
                Next
                txtTotPcs2.Text = tot.ToString()
            End If
        End If
    End Sub

    '  RETURNS GRID CLICK 
    Private Sub flxReturns_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxReturns.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = flxReturns.Rows(e.RowIndex)
            Dim c0 As String = row.Cells("R0").Value?.ToString()
            Dim c1 As String = row.Cells("R1").Value?.ToString()
            If Not String.IsNullOrEmpty(c0) Then
                Dim idx As Integer = cmbColor.FindStringExact(c0)
                If idx >= 0 Then cmbColor.SelectedIndex = idx
            End If
            If Not String.IsNullOrEmpty(c1) Then
                Dim idx2 As Integer = cmbMake.FindStringExact(c1)
                If idx2 >= 0 Then cmbMake.SelectedIndex = idx2
            End If
        End If
    End Sub

    '  TOOLBAR BUTTONS 
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        If txtParNo.Text.Trim() <> "" Then
            txtParNo.Text = txtParNo.Text.Trim().ToUpper()
            GetNewPacket() : txtemp.Focus()
        Else
            MessageBox.Show("Please enter the Parcel No.", Me.Text,
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtParNo.Focus()
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Recsave()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        frmInitialze()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    '  VALIDATE AND SAVE 
    Private Sub Recsave()
        Dim rs As New ADODB.Recordset
        Try
            Dim par As String = txtParNo.Text.Trim().Replace("'", "''")
            Dim pkt As String = txtPktNo.Text.Trim().Replace("'", "''")

            If par = "" Then
                MessageBox.Show("Invalid Parcel No", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If pkt = "" Then
                MessageBox.Show("Invalid Packet No", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            rs.Open("SELECT COUNT(*) AS CNT FROM tblGrading_RndPacket " &
                    "WHERE ParNo='" & par & "' AND PktNo='" & pkt & "'",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            Dim pktExists As Boolean = Not rs.EOF AndAlso (CInt(rs.Fields("CNT").Value) > 0)
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()

            If Not pktExists Then
                MessageBox.Show("Invalid Packet", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                frmInitialze() : Return
            End If

            If cmbDept.Text.Trim() = "" Then
                MessageBox.Show("Invalid Department", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If txtemp.Text.Trim() = "" OrElse txtemp.Text.Trim().Length <> 6 Then
                MessageBox.Show("Invalid Emp. No.", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            If issued AndAlso Not Checked Then
                ' Issue validation
                If txtIssTap.Text.Trim() = "" OrElse CSngSafe(txtIssTap.Text) <= 0 Then
                    MessageBox.Show("Invalid Pcs", Me.Text,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
                End If
                If txtIssCts.Text.Trim() = "" OrElse CDblSafe(txtIssCts.Text) <= 0 Then
                    MessageBox.Show("Invalid Cts", Me.Text,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
                End If

            ElseIf issued AndAlso Checked Then
                ' Return validation
                If txtRej.Text.Trim() = "" Then txtRej.Text = "0"
                If txtRejCts.Text.Trim() = "" Then txtRejCts.Text = "0"
                If txtLostPcs.Text.Trim() = "" Then txtLostPcs.Text = "0"
                If txtLostCts.Text.Trim() = "" Then txtLostCts.Text = "0"
                If txtRepPcs.Text.Trim() = "" Then txtRepPcs.Text = "0"
                If txtRepCts.Text.Trim() = "" Then txtRepCts.Text = "0"

                Dim stiss As Single = CSngSafe(txtIssTap.Text)
                Dim stret As Single = CSngSafe(txtRetTap.Text) +
                                      CSngSafe(txtRej.Text) +
                                      CSngSafe(txtLostPcs.Text) +
                                      CSngSafe(txtRepPcs.Text)
                If stiss <> stret Then
                    MessageBox.Show("Stones Issued " & stiss & "   Stones Returned " & stret,
                                    Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
                End If

                ' FIX 8: Carats balance check (was missing in original boiling form)
                Dim ciss As Single = CSngSafe(txtIssCts.Text) + 0.1F
                Dim cret As Single = CSng(Math.Round(
                    CDblSafe(txtRetCts.Text) + CDblSafe(txtRejCts.Text) +
                    CDblSafe(txtLostCts.Text) + CDblSafe(txtRepCts.Text), 3))
                If ciss < cret Then
                    MessageBox.Show("Carats Issued " & CSngSafe(txtIssCts.Text).ToString("##.###") &
                                    "   Carats Returned " & cret.ToString("##.###"),
                                    Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
                End If

                ' FIX 9: Type grid pcs/cts balance check (was missing)
                Dim intTotPcs As Double = 0
                Dim dblTotCts As Double = 0
                For Each row As DataGridViewRow In flxType.Rows
                    If row.IsNewRow Then Continue For
                    intTotPcs += CDblSafe(row.Cells("Col4").Value?.ToString())
                    dblTotCts += CDblSafe(row.Cells("Col5").Value?.ToString())
                Next

                If CDblSafe(txtIssTap.Text) <>
                   intTotPcs + CDblSafe(txtRepPcs.Text) + CDblSafe(txtRej.Text) + CDblSafe(txtLostPcs.Text) Then
                    MessageBox.Show("Pcs not matching", Me.Text,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
                End If

                ' Auto-add single employee if grid is empty
                If flxEmp.Rows.Count = 0 Then
                    flxEmp.Rows.Add(txtemp.Text.Trim(), txtRetTap.Text.Trim())
                End If

                Dim empTot As Single = 0
                For Each row As DataGridViewRow In flxEmp.Rows
                    If row.IsNewRow Then Continue For
                    empTot += CSngSafe(row.Cells("EmpPcs").Value?.ToString())
                Next
                If CSngSafe(txtIssTap.Text) <> empTot Then
                    MessageBox.Show("Emp Pcs not matching", Me.Text,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
                End If
            End If

            DataSave()

        Catch ex As Exception
            MsgBox("Error in Recsave : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub

    '  SAVE TO DATABASE 
    Private Sub DataSave()
        Dim rs As New ADODB.Recordset
        Dim cmd As New ADODB.Command
        Try
            Dim par As String = txtParNo.Text.Trim().Replace("'", "''")
            Dim pkt As String = txtPktNo.Text.Trim().Replace("'", "''")
            Dim dept As String = cmbDept.Text.Trim().Replace("'", "''")
            Dim sec As Integer = cmbSection.SelectedIndex + 1
            Dim emp As String = txtemp.Text.Trim().Substring(0, 6).Replace("'", "''")
            Dim today As String = Date.Now.ToString("MM/dd/yyyy")

            cmd.ActiveConnection = AdoCN

            If issued AndAlso Not Checked Then
                ' ── Duplicate check
                rs.Open("SELECT COUNT(*) AS CNT FROM tblGrading_RndCheckingIssues " &
                        "WHERE Department='" & dept & "' AND ParNo='" & par & "' " &
                        "AND PktNo='" & pkt & "' AND Sec=" & sec,
                        AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
                Dim dupIss As Boolean = Not rs.EOF AndAlso (CInt(rs.Fields("CNT").Value) > 0)
                If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
                If dupIss Then
                    MessageBox.Show("Already Entered", Me.Text,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
                End If

                ' ── INSERT Issue
                cmd.CommandText =
                    "INSERT INTO tblGrading_RndCheckingIssues " &
                    "(Department,ParNo,PktNo,Sec,SecCount,EmpNo,IssPcs,IssCts,IssDate,IssTime) " &
                    "VALUES('" & dept & "','" & par & "','" & pkt & "'," &
                    sec & "," & intSecCount & ",'" & emp & "'," &
                    CSngSafe(txtIssTap.Text) & "," &
                    CDblSafe(txtIssCts.Text) & ",'" &
                    today & "','" & txtIssTime.Text.Trim() & "')"
                cmd.Execute()

            ElseIf issued AndAlso Checked Then
                ' ── Duplicate check
                rs.Open("SELECT COUNT(*) AS CNT FROM tblGrading_RndCheckingReturns " &
                        "WHERE Department='" & dept & "' AND ParNo='" & par & "' " &
                        "AND PktNo='" & pkt & "' AND Sec=" & sec,
                        AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
                Dim dupRet As Boolean = Not rs.EOF AndAlso (CInt(rs.Fields("CNT").Value) > 0)
                If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
                If dupRet Then
                    MessageBox.Show("Already entered", Me.Text,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
                End If

                ' ── INSERT Return
                cmd.CommandText =
                    "INSERT INTO tblGrading_RndCheckingReturns " &
                    "(Department,ParNo,PktNo,Sec,SecCount,EmpNo,RetPcs,RetCts," &
                    "LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " &
                    "VALUES('" & dept & "','" & par & "','" & pkt & "'," &
                    sec & "," & intSecCount & ",'" & emp & "'," &
                    CSngSafe(txtRetTap.Text) & "," & CDblSafe(txtRetCts.Text) & "," &
                    CSngSafe(txtLostPcs.Text) & "," & CDblSafe(txtLostCts.Text) & "," &
                    CSngSafe(txtRepPcs.Text) & "," & CDblSafe(txtRepCts.Text) & ",'" &
                    today & "','" & txtRetTime.Text.Trim() & "'," &
                    CSngSafe(txtRej.Text) & "," & CDblSafe(txtRejCts.Text) & ")"
                cmd.Execute()

                Save_GradingTypes(par, pkt, sec, cmd)
            End If

            MessageBox.Show("Saved Successfully", Me.Text,
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            frmInitialze()

        Catch ex As Exception
            MsgBox("Error in DataSave : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
            cmd = Nothing
        End Try
    End Sub

    '  SAVE GRADING TYPES AND EMPLOYEE DETAILS 
    Private Sub Save_GradingTypes(parNo As String, pktNo As String,
                                   sec As Integer, cmd As ADODB.Command)
        Dim dept As String = cmbDept.Text.Trim().Replace("'", "''")
        Dim today As String = Date.Now.ToString("MM/dd/yyyy")

        ' DELETE existing checking types for this packet/section
        cmd.CommandText =
            "DELETE FROM tblGrading_RndCheckingTypes " &
            "WHERE Department='" & dept & "' AND ParNo='" & parNo & "' " &
            "AND PktNo='" & pktNo & "' AND Sec=" & sec
        cmd.Execute()

        ' INSERT each type row
        For Each row As DataGridViewRow In flxType.Rows
            If row.IsNewRow Then Continue For
            Dim c0 As String = row.Cells("Col0").Value?.ToString().Replace("'", "''")
            Dim c1 As String = row.Cells("Col1").Value?.ToString().Replace("'", "''")
            Dim c2 As String = row.Cells("Col2").Value?.ToString().Replace("'", "''")
            Dim c3 As String = row.Cells("Col3").Value?.ToString().Replace("'", "''")
            cmd.CommandText =
                "INSERT INTO tblGrading_RndCheckingTypes " &
                "(Department,ParNo,PktNo,Sec,ReturnType1,ReturnType2,ReturnType3,ReturnType4,Pcs,Cts) " &
                "VALUES('" & dept & "','" & parNo & "','" & pktNo & "'," & sec & ",'" &
                c0 & "','" & c1 & "','" & c2 & "','" & c3 & "'," &
                CSngSafe(row.Cells("Col4").Value?.ToString()) & "," &
                CDblSafe(row.Cells("Col5").Value?.ToString()) & ")"
            cmd.Execute()
        Next

        ' DELETE existing checking details for this packet/section
        cmd.CommandText =
            "DELETE FROM tblGrading_RndCheckingDetails " &
            "WHERE Department='" & dept & "' AND ParNo='" & parNo & "' " &
            "AND PktNo='" & pktNo & "' AND Sec=" & sec
        cmd.Execute()

        ' INSERT each employee row
        For Each row As DataGridViewRow In flxEmp.Rows
            If row.IsNewRow Then Continue For
            Dim empQ As String = row.Cells("EmpNo").Value?.ToString().ToUpper().Replace("'", "''")
            cmd.CommandText =
                "INSERT INTO tblGrading_RndCheckingDetails " &
                "(Department,ParNo,PktNo,Sec,EmpNo,Pcs,RetDate) " &
                "VALUES('" & dept & "','" & parNo & "','" & pktNo & "'," & sec & ",'" &
                empQ & "'," &
                CDblSafe(row.Cells("EmpPcs").Value?.ToString()) & ",'" & today & "')"
            cmd.Execute()
        Next
    End Sub

    '  HELPER UTILITIES 
    Private Function CDblSafe(value As String) As Double
        If String.IsNullOrWhiteSpace(value) Then Return 0
        Dim result As Double
        If Double.TryParse(value.Replace(",", ""), result) Then Return result
        Return 0
    End Function

    Private Function CSngSafe(value As String) As Single
        If String.IsNullOrWhiteSpace(value) Then Return 0
        Dim result As Single
        If Single.TryParse(value.Replace(",", ""), result) Then Return result
        Return 0
    End Function

    ' ── KEY PRESS HELPERS 
    Private Sub NumericOnly(ByVal e As KeyPressEventArgs, ByVal CurrentText As String)
        If Not (Char.IsDigit(e.KeyChar) OrElse
                Asc(e.KeyChar) = 8 OrElse
                e.KeyChar = "."c OrElse
                e.KeyChar = ControlChars.Cr) Then
            e.Handled = True
        Else
            If e.KeyChar = "."c AndAlso CurrentText.Contains(".") Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub IntegerOnly(ByVal e As KeyPressEventArgs)
        If Not (Char.IsDigit(e.KeyChar) OrElse
                Asc(e.KeyChar) = 8 OrElse
                e.KeyChar = ControlChars.Cr) Then
            e.Handled = True
        End If
    End Sub

    ' ── KEY PRESS / LEAVE HANDLERS 
    Private Sub txtIssTap_KeyPress(s As Object, e As KeyPressEventArgs) Handles txtIssTap.KeyPress
        NumericOnly(e, txtIssTap.Text)
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True : txtIssCts.Focus()
        End If
    End Sub
    Private Sub txtIssTap_Leave(s As Object, e As EventArgs) Handles txtIssTap.Leave
        If Not IsNumeric(txtIssTap.Text) Then txtIssTap.Text = "0"
    End Sub

    Private Sub txtIssCts_KeyPress(s As Object, e As KeyPressEventArgs) Handles txtIssCts.KeyPress
        NumericOnly(e, txtIssCts.Text)
    End Sub
    Private Sub txtIssCts_Leave(s As Object, e As EventArgs) Handles txtIssCts.Leave
        If Not IsNumeric(txtIssCts.Text) Then txtIssCts.Text = "0"
    End Sub

    Private Sub txtRej_KeyPress(s As Object, e As KeyPressEventArgs) Handles txtRej.KeyPress
        IntegerOnly(e)
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True : txtRejCts.Focus()
        End If
    End Sub
    Private Sub txtRej_Leave(s As Object, e As EventArgs) Handles txtRej.Leave
        If Not IsNumeric(txtRej.Text) Then txtRej.Text = "0"
    End Sub

    Private Sub txtRejCts_KeyPress(s As Object, e As KeyPressEventArgs) Handles txtRejCts.KeyPress
        NumericOnly(e, txtRejCts.Text)
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True : txtLostPcs.Focus()
        End If
    End Sub
    Private Sub txtRejCts_Leave(s As Object, e As EventArgs) Handles txtRejCts.Leave
        If Not IsNumeric(txtRejCts.Text) Then txtRejCts.Text = "0"
    End Sub

    Private Sub txtLostPcs_KeyPress(s As Object, e As KeyPressEventArgs) Handles txtLostPcs.KeyPress
        IntegerOnly(e)
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True : txtLostCts.Focus()
        End If
    End Sub
    Private Sub txtLostPcs_Leave(s As Object, e As EventArgs) Handles txtLostPcs.Leave
        If Not IsNumeric(txtLostPcs.Text) Then txtLostPcs.Text = "0"
    End Sub

    Private Sub txtLostCts_KeyPress(s As Object, e As KeyPressEventArgs) Handles txtLostCts.KeyPress
        NumericOnly(e, txtLostCts.Text)
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True : txtRepPcs.Focus()
        End If
    End Sub
    Private Sub txtLostCts_Leave(s As Object, e As EventArgs) Handles txtLostCts.Leave
        If txtLostCts.Text = "" Then txtLostCts.Text = "0"
    End Sub

    Private Sub txtRepPcs_KeyPress(s As Object, e As KeyPressEventArgs) Handles txtRepPcs.KeyPress
        IntegerOnly(e)
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True : txtRepCts.Focus()
        End If
    End Sub
    Private Sub txtRepPcs_Leave(s As Object, e As EventArgs) Handles txtRepPcs.Leave
        If txtRepPcs.Text = "" Then txtRepPcs.Text = "0"
    End Sub

    Private Sub txtRepCts_KeyPress(s As Object, e As KeyPressEventArgs) Handles txtRepCts.KeyPress
        NumericOnly(e, txtRepCts.Text)
    End Sub
    Private Sub txtRepCts_Leave(s As Object, e As EventArgs) Handles txtRepCts.Leave
        If txtRepCts.Text = "" Then txtRepCts.Text = "0"
    End Sub

    Private Sub txtTypePcs_KeyPress(s As Object, e As KeyPressEventArgs) Handles txtTypePcs.KeyPress
        IntegerOnly(e)
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True : txtTypeCts.Focus()
        End If
    End Sub

    Private Sub txtTypeCts_KeyPress(s As Object, e As KeyPressEventArgs) Handles txtTypeCts.KeyPress
        NumericOnly(e, txtTypeCts.Text)
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True : cmdTypeAdd.Focus()
        End If
    End Sub

    Private Sub txtEmpNo_KeyPress(s As Object, e As KeyPressEventArgs) Handles txtEmpNo.KeyPress
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True
            If txtEmpNo.Text.Trim().Length = 6 Then
                txtEmpPcs.Focus()
            Else
                MessageBox.Show("Invalid Employee", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtEmpNo.Text = "" : txtEmpNo.Focus()
            End If
        End If
    End Sub

    Private Sub txtEmpPcs_KeyPress(s As Object, e As KeyPressEventArgs) Handles txtEmpPcs.KeyPress
        IntegerOnly(e)
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True
            If txtEmpPcs.Text <> "" AndAlso CDblSafe(txtEmpPcs.Text) > 0 Then
                cmdEmpAdd.Focus()
            Else
                MessageBox.Show("Invalid Pcs", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtEmpPcs.Text = "" : txtEmpPcs.Focus()
            End If
        End If
    End Sub

    Private Sub flxEmp_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxEmp.CellContentClick
        ' Reserved
    End Sub

End Class