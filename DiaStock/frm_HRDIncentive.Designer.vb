<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_HRDIncentive
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.HazelDevTabControl1 = New DiaStock.HazelDevTabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.HazelDev_Panel2 = New DiaStock.HazelDev_Panel()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Marks2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.txtTotMarks2 = New System.Windows.Forms.TextBox()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.txtTotMarks = New System.Windows.Forms.TextBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.ExpProgress = New System.Windows.Forms.ProgressBar()
        Me.HazelDev_Panel3 = New DiaStock.HazelDev_Panel()
        Me.flxSummary = New System.Windows.Forms.DataGridView()
        Me.cmdExcel = New DiaStock.HazelDev_Button()
        Me.cmdProcess = New DiaStock.HazelDev_Button()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.dtpMonth = New System.Windows.Forms.DateTimePicker()
        Me.cmbEmpNo = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbGrp = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbDept = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Department = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Group = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Amount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        Me.HazelDevTabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.HazelDev_Panel2.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.HazelDev_Panel3.SuspendLayout()
        CType(Me.flxSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDevTabControl1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(1268, 672)
        Me.HazelDev_ThemeContainer1.TabIndex = 5
        Me.HazelDev_ThemeContainer1.Text = "INCENTIVE"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'HazelDevTabControl1
        '
        Me.HazelDevTabControl1.Controls.Add(Me.TabPage1)
        Me.HazelDevTabControl1.Controls.Add(Me.TabPage2)
        Me.HazelDevTabControl1.ForeColor = System.Drawing.Color.Gray
        Me.HazelDevTabControl1.Location = New System.Drawing.Point(3, 109)
        Me.HazelDevTabControl1.Name = "HazelDevTabControl1"
        Me.HazelDevTabControl1.SelectedIndex = 0
        Me.HazelDevTabControl1.Size = New System.Drawing.Size(1257, 560)
        Me.HazelDevTabControl1.TabIndex = 156
        '
        'TabPage1
        '
        Me.TabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPage1.Controls.Add(Me.HazelDev_Panel2)
        Me.TabPage1.Controls.Add(Me.cmdSave)
        Me.TabPage1.Controls.Add(Me.txtTotMarks2)
        Me.TabPage1.Controls.Add(Me.txtAmount)
        Me.TabPage1.Controls.Add(Me.txtTotMarks)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1249, 531)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Details"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'HazelDev_Panel2
        '
        Me.HazelDev_Panel2.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel2.Controls.Add(Me.flxDetails)
        Me.HazelDev_Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel2.Location = New System.Drawing.Point(6, 6)
        Me.HazelDev_Panel2.Name = "HazelDev_Panel2"
        Me.HazelDev_Panel2.Size = New System.Drawing.Size(1235, 457)
        Me.HazelDev_Panel2.TabIndex = 159
        Me.HazelDev_Panel2.Text = "HazelDev_Panel2"
        Me.HazelDev_Panel2.TransparencyKey = System.Drawing.Color.Empty
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Marks2})
        Me.flxDetails.Location = New System.Drawing.Point(3, 3)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.flxDetails.Size = New System.Drawing.Size(1225, 451)
        Me.flxDetails.TabIndex = 70
        '
        'Column1
        '
        Me.Column1.HeaderText = "Main"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 200
        '
        'Column2
        '
        Me.Column2.HeaderText = "Sub"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 700
        '
        'Column3
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column3.DefaultCellStyle = DataGridViewCellStyle1
        Me.Column3.HeaderText = "Max"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column4
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column4.DefaultCellStyle = DataGridViewCellStyle2
        Me.Column4.HeaderText = "Marks"
        Me.Column4.Name = "Column4"
        '
        'Column5
        '
        Me.Column5.HeaderText = "CrID"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Visible = False
        '
        'Marks2
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Marks2.DefaultCellStyle = DataGridViewCellStyle3
        Me.Marks2.HeaderText = "Extra"
        Me.Marks2.Name = "Marks2"
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSave.FlatAppearance.BorderSize = 0
        Me.cmdSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.Color.White
        Me.cmdSave.Location = New System.Drawing.Point(9, 469)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(99, 28)
        Me.cmdSave.TabIndex = 85
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'txtTotMarks2
        '
        Me.txtTotMarks2.Location = New System.Drawing.Point(1117, 469)
        Me.txtTotMarks2.Name = "txtTotMarks2"
        Me.txtTotMarks2.ReadOnly = True
        Me.txtTotMarks2.Size = New System.Drawing.Size(98, 21)
        Me.txtTotMarks2.TabIndex = 158
        Me.txtTotMarks2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAmount
        '
        Me.txtAmount.Location = New System.Drawing.Point(1117, 500)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.ReadOnly = True
        Me.txtAmount.Size = New System.Drawing.Size(98, 21)
        Me.txtAmount.TabIndex = 157
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotMarks
        '
        Me.txtTotMarks.Location = New System.Drawing.Point(1010, 469)
        Me.txtTotMarks.Name = "txtTotMarks"
        Me.txtTotMarks.ReadOnly = True
        Me.txtTotMarks.Size = New System.Drawing.Size(98, 21)
        Me.txtTotMarks.TabIndex = 156
        Me.txtTotMarks.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TabPage2
        '
        Me.TabPage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TabPage2.Controls.Add(Me.ExpProgress)
        Me.TabPage2.Controls.Add(Me.HazelDev_Panel3)
        Me.TabPage2.Controls.Add(Me.cmdExcel)
        Me.TabPage2.Controls.Add(Me.cmdProcess)
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1249, 531)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Summary"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'ExpProgress
        '
        Me.ExpProgress.Location = New System.Drawing.Point(9, 499)
        Me.ExpProgress.Name = "ExpProgress"
        Me.ExpProgress.Size = New System.Drawing.Size(1229, 24)
        Me.ExpProgress.TabIndex = 89
        Me.ExpProgress.Visible = False
        '
        'HazelDev_Panel3
        '
        Me.HazelDev_Panel3.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel3.Controls.Add(Me.flxSummary)
        Me.HazelDev_Panel3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel3.Location = New System.Drawing.Point(6, 6)
        Me.HazelDev_Panel3.Name = "HazelDev_Panel3"
        Me.HazelDev_Panel3.Size = New System.Drawing.Size(1235, 457)
        Me.HazelDev_Panel3.TabIndex = 88
        Me.HazelDev_Panel3.Text = "HazelDev_Panel3"
        Me.HazelDev_Panel3.TransparencyKey = System.Drawing.Color.Empty
        '
        'flxSummary
        '
        Me.flxSummary.AllowUserToAddRows = False
        Me.flxSummary.AllowUserToDeleteRows = False
        Me.flxSummary.AllowUserToResizeColumns = False
        Me.flxSummary.AllowUserToResizeRows = False
        Me.flxSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxSummary.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.Department, Me.Group, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn6, Me.Amount})
        Me.flxSummary.Location = New System.Drawing.Point(3, 3)
        Me.flxSummary.Name = "flxSummary"
        Me.flxSummary.RowHeadersVisible = False
        Me.flxSummary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.flxSummary.Size = New System.Drawing.Size(1229, 451)
        Me.flxSummary.TabIndex = 71
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
        Me.cmdExcel.Location = New System.Drawing.Point(121, 469)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(99, 28)
        Me.cmdExcel.TabIndex = 87
        Me.cmdExcel.Text = "Excel"
        Me.cmdExcel.UseVisualStyleBackColor = False
        '
        'cmdProcess
        '
        Me.cmdProcess.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdProcess.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdProcess.FlatAppearance.BorderSize = 0
        Me.cmdProcess.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdProcess.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdProcess.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdProcess.ForeColor = System.Drawing.Color.White
        Me.cmdProcess.Location = New System.Drawing.Point(9, 469)
        Me.cmdProcess.Name = "cmdProcess"
        Me.cmdProcess.Size = New System.Drawing.Size(99, 28)
        Me.cmdProcess.TabIndex = 86
        Me.cmdProcess.Text = "Process"
        Me.cmdProcess.UseVisualStyleBackColor = False
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.dtpMonth)
        Me.HazelDev_Panel1.Controls.Add(Me.cmbEmpNo)
        Me.HazelDev_Panel1.Controls.Add(Me.Label2)
        Me.HazelDev_Panel1.Controls.Add(Me.cmbGrp)
        Me.HazelDev_Panel1.Controls.Add(Me.Label1)
        Me.HazelDev_Panel1.Controls.Add(Me.cmbDept)
        Me.HazelDev_Panel1.Controls.Add(Me.Label10)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(1253, 49)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'dtpMonth
        '
        Me.dtpMonth.CustomFormat = "MMM/yyyy"
        Me.dtpMonth.Enabled = False
        Me.dtpMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpMonth.Location = New System.Drawing.Point(950, 13)
        Me.dtpMonth.Name = "dtpMonth"
        Me.dtpMonth.Size = New System.Drawing.Size(87, 22)
        Me.dtpMonth.TabIndex = 84
        '
        'cmbEmpNo
        '
        Me.cmbEmpNo.FormattingEnabled = True
        Me.cmbEmpNo.IntegralHeight = False
        Me.cmbEmpNo.Location = New System.Drawing.Point(770, 13)
        Me.cmbEmpNo.Name = "cmbEmpNo"
        Me.cmbEmpNo.Size = New System.Drawing.Size(152, 22)
        Me.cmbEmpNo.TabIndex = 83
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(691, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 14)
        Me.Label2.TabIndex = 82
        Me.Label2.Text = "Emp No"
        '
        'cmbGrp
        '
        Me.cmbGrp.FormattingEnabled = True
        Me.cmbGrp.IntegralHeight = False
        Me.cmbGrp.Location = New System.Drawing.Point(534, 13)
        Me.cmbGrp.Name = "cmbGrp"
        Me.cmbGrp.Size = New System.Drawing.Size(152, 22)
        Me.cmbGrp.TabIndex = 81
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(455, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 14)
        Me.Label1.TabIndex = 80
        Me.Label1.Text = "Group"
        '
        'cmbDept
        '
        Me.cmbDept.FormattingEnabled = True
        Me.cmbDept.IntegralHeight = False
        Me.cmbDept.Location = New System.Drawing.Point(293, 13)
        Me.cmbDept.Name = "cmbDept"
        Me.cmbDept.Size = New System.Drawing.Size(152, 22)
        Me.cmbDept.TabIndex = 79
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(214, 13)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(73, 14)
        Me.Label10.TabIndex = 78
        Me.Label10.Text = "Department"
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
        'cmdNew
        '
        Me.cmdNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdNew.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdNew.FlatAppearance.BorderSize = 0
        Me.cmdNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdNew.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdNew.ForeColor = System.Drawing.Color.White
        Me.cmdNew.Location = New System.Drawing.Point(109, 13)
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.Size = New System.Drawing.Size(99, 28)
        Me.cmdNew.TabIndex = 31
        Me.cmdNew.Text = "New"
        Me.cmdNew.UseVisualStyleBackColor = False
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Emp No"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 200
        '
        'Department
        '
        Me.Department.HeaderText = "Department"
        Me.Department.Name = "Department"
        Me.Department.Width = 300
        '
        'Group
        '
        Me.Group.HeaderText = "Group"
        Me.Group.Name = "Group"
        Me.Group.Width = 200
        '
        'DataGridViewTextBoxColumn4
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewTextBoxColumn4.HeaderText = "Marks"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        '
        'DataGridViewTextBoxColumn6
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn6.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewTextBoxColumn6.HeaderText = "Extra"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        '
        'Amount
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Amount.DefaultCellStyle = DataGridViewCellStyle6
        Me.Amount.HeaderText = "Amount"
        Me.Amount.Name = "Amount"
        '
        'frm_HRDIncentive
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1268, 672)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_HRDIncentive"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Incentive"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDevTabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.HazelDev_Panel2.ResumeLayout(False)
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.HazelDev_Panel3.ResumeLayout(False)
        CType(Me.flxSummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents dtpMonth As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents txtTotMarks2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtTotMarks As System.Windows.Forms.TextBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents cmbEmpNo As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbGrp As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbDept As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents HazelDevTabControl1 As DiaStock.HazelDevTabControl
    Friend WithEvents cmdProcess As DiaStock.HazelDev_Button
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents HazelDev_Panel2 As DiaStock.HazelDev_Panel
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Marks2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HazelDev_Panel3 As DiaStock.HazelDev_Panel
    Friend WithEvents flxSummary As System.Windows.Forms.DataGridView
    Friend WithEvents ExpProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Department As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Group As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Amount As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
