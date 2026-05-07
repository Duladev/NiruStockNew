<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ExpSizingExport
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.txtTotalCts = New System.Windows.Forms.TextBox()
        Me.txtTotalPcs = New System.Windows.Forms.TextBox()
        Me.cmdTrf = New DiaStock.HazelDev_Button()
        Me.cmdExcel = New DiaStock.HazelDev_Button()
        Me.ExpProgress = New System.Windows.Forms.ProgressBar()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.txtPackNo = New System.Windows.Forms.TextBox()
        Me.chkSelect = New DiaStock.HazelDev_CheckBox()
        Me.cmdRefresh = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.Department = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RecordNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PacketPcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PacketCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SupParNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Category = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Supplier = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Confirm = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Comp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbCompany = New System.Windows.Forms.ComboBox()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotalCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotalPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdTrf)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdExcel)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.ExpProgress)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxDetails)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(1048, 601)
        Me.HazelDev_ThemeContainer1.TabIndex = 6
        Me.HazelDev_ThemeContainer1.Text = "SORTING EXPORT"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'txtTotalCts
        '
        Me.txtTotalCts.Enabled = False
        Me.txtTotalCts.Location = New System.Drawing.Point(403, 567)
        Me.txtTotalCts.Name = "txtTotalCts"
        Me.txtTotalCts.Size = New System.Drawing.Size(102, 21)
        Me.txtTotalCts.TabIndex = 77
        Me.txtTotalCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalPcs
        '
        Me.txtTotalPcs.Enabled = False
        Me.txtTotalPcs.Location = New System.Drawing.Point(300, 567)
        Me.txtTotalPcs.Name = "txtTotalPcs"
        Me.txtTotalPcs.Size = New System.Drawing.Size(97, 21)
        Me.txtTotalPcs.TabIndex = 76
        Me.txtTotalPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmdTrf
        '
        Me.cmdTrf.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdTrf.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdTrf.FlatAppearance.BorderSize = 0
        Me.cmdTrf.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdTrf.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdTrf.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdTrf.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTrf.ForeColor = System.Drawing.Color.White
        Me.cmdTrf.Location = New System.Drawing.Point(940, 567)
        Me.cmdTrf.Name = "cmdTrf"
        Me.cmdTrf.Size = New System.Drawing.Size(99, 28)
        Me.cmdTrf.TabIndex = 75
        Me.cmdTrf.Text = "Verify"
        Me.cmdTrf.UseVisualStyleBackColor = False
        '
        'cmdExcel
        '
        Me.cmdExcel.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdExcel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdExcel.FlatAppearance.BorderSize = 0
        Me.cmdExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdExcel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdExcel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExcel.ForeColor = System.Drawing.Color.White
        Me.cmdExcel.Location = New System.Drawing.Point(7, 567)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(99, 28)
        Me.cmdExcel.TabIndex = 72
        Me.cmdExcel.Text = "Excel"
        Me.cmdExcel.UseVisualStyleBackColor = False
        '
        'ExpProgress
        '
        Me.ExpProgress.Location = New System.Drawing.Point(6, 537)
        Me.ExpProgress.Name = "ExpProgress"
        Me.ExpProgress.Size = New System.Drawing.Size(1033, 24)
        Me.ExpProgress.TabIndex = 70
        Me.ExpProgress.Visible = False
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Department, Me.DataGridViewTextBoxColumn3, Me.RecordNo, Me.PacketPcs, Me.PacketCts, Me.SupParNo, Me.Category, Me.Supplier, Me.Confirm, Me.Comp})
        Me.flxDetails.Location = New System.Drawing.Point(6, 109)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.flxDetails.Size = New System.Drawing.Size(1033, 421)
        Me.flxDetails.TabIndex = 68
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.Label7)
        Me.HazelDev_Panel1.Controls.Add(Me.cmbCompany)
        Me.HazelDev_Panel1.Controls.Add(Me.txtPackNo)
        Me.HazelDev_Panel1.Controls.Add(Me.chkSelect)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdRefresh)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(1036, 49)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'txtPackNo
        '
        Me.txtPackNo.Location = New System.Drawing.Point(109, 13)
        Me.txtPackNo.Name = "txtPackNo"
        Me.txtPackNo.ReadOnly = True
        Me.txtPackNo.Size = New System.Drawing.Size(84, 22)
        Me.txtPackNo.TabIndex = 92
        '
        'chkSelect
        '
        Me.chkSelect.Checked = False
        Me.chkSelect.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkSelect.Location = New System.Drawing.Point(899, 19)
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Size = New System.Drawing.Size(84, 16)
        Me.chkSelect.TabIndex = 69
        Me.chkSelect.Text = "Select All"
        Me.chkSelect.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmdRefresh
        '
        Me.cmdRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdRefresh.FlatAppearance.BorderSize = 0
        Me.cmdRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRefresh.ForeColor = System.Drawing.Color.White
        Me.cmdRefresh.Location = New System.Drawing.Point(199, 13)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(99, 28)
        Me.cmdRefresh.TabIndex = 51
        Me.cmdRefresh.Text = "Refresh"
        Me.cmdRefresh.UseVisualStyleBackColor = False
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdExit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdExit.FlatAppearance.BorderSize = 0
        Me.cmdExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdExit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.ForeColor = System.Drawing.Color.White
        Me.cmdExit.Location = New System.Drawing.Point(4, 13)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(99, 28)
        Me.cmdExit.TabIndex = 32
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'Department
        '
        Me.Department.HeaderText = "Department"
        Me.Department.Name = "Department"
        Me.Department.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        DataGridViewCellStyle4.NullValue = Nothing
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewTextBoxColumn3.HeaderText = "Par No."
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'RecordNo
        '
        Me.RecordNo.HeaderText = "Org Assort"
        Me.RecordNo.Name = "RecordNo"
        Me.RecordNo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'PacketPcs
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PacketPcs.DefaultCellStyle = DataGridViewCellStyle5
        Me.PacketPcs.HeaderText = "Pcs"
        Me.PacketPcs.Name = "PacketPcs"
        Me.PacketPcs.ReadOnly = True
        '
        'PacketCts
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PacketCts.DefaultCellStyle = DataGridViewCellStyle6
        Me.PacketCts.HeaderText = "Cts"
        Me.PacketCts.Name = "PacketCts"
        Me.PacketCts.ReadOnly = True
        '
        'SupParNo
        '
        Me.SupParNo.HeaderText = "SupParNo"
        Me.SupParNo.Name = "SupParNo"
        '
        'Category
        '
        Me.Category.HeaderText = "Category"
        Me.Category.Name = "Category"
        '
        'Supplier
        '
        Me.Supplier.HeaderText = "Supplier"
        Me.Supplier.Name = "Supplier"
        '
        'Confirm
        '
        Me.Confirm.HeaderText = "Confirm"
        Me.Confirm.Name = "Confirm"
        '
        'Comp
        '
        Me.Comp.HeaderText = "Comp"
        Me.Comp.Name = "Comp"
        Me.Comp.ReadOnly = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(308, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(57, 14)
        Me.Label7.TabIndex = 94
        Me.Label7.Text = "Company"
        '
        'cmbCompany
        '
        Me.cmbCompany.FormattingEnabled = True
        Me.cmbCompany.IntegralHeight = False
        Me.cmbCompany.Location = New System.Drawing.Point(382, 13)
        Me.cmbCompany.Name = "cmbCompany"
        Me.cmbCompany.Size = New System.Drawing.Size(188, 22)
        Me.cmbCompany.TabIndex = 93
        '
        'frm_ExpSizingExport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1048, 601)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.Name = "frm_ExpSizingExport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sorting Export"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents cmdTrf As DiaStock.HazelDev_Button
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents ExpProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents txtPackNo As System.Windows.Forms.TextBox
    Friend WithEvents chkSelect As DiaStock.HazelDev_CheckBox
    Friend WithEvents cmdRefresh As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents txtTotalCts As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalPcs As System.Windows.Forms.TextBox
    Friend WithEvents Department As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RecordNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PacketPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PacketCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SupParNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Category As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Supplier As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Confirm As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Comp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbCompany As System.Windows.Forms.ComboBox
End Class
