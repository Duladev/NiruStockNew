<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ExpSizingTransfer
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
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.txtDiaValue = New System.Windows.Forms.TextBox()
        Me.txtListValue = New System.Windows.Forms.TextBox()
        Me.txtInvValue = New System.Windows.Forms.TextBox()
        Me.cmdTrf = New DiaStock.HazelDev_Button()
        Me.txtTotalCts = New System.Windows.Forms.TextBox()
        Me.txtTotalPcs = New System.Windows.Forms.TextBox()
        Me.cmdExcel = New DiaStock.HazelDev_Button()
        Me.ExpProgress = New System.Windows.Forms.ProgressBar()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.optRoughS = New System.Windows.Forms.RadioButton()
        Me.txtDepartment = New System.Windows.Forms.TextBox()
        Me.optPolish = New System.Windows.Forms.RadioButton()
        Me.optAll = New System.Windows.Forms.RadioButton()
        Me.optRough = New System.Windows.Forms.RadioButton()
        Me.cmdCalc = New DiaStock.HazelDev_Button()
        Me.txtParcel = New System.Windows.Forms.TextBox()
        Me.chkSelect = New DiaStock.HazelDev_CheckBox()
        Me.cmdRefresh = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.Department = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RareCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RecordNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Assortment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PacketPcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PacketCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Price = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FinishedPcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FinishedCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IssuePcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Confirm = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Category = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtDiaValue)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtListValue)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtInvValue)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdTrf)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotalCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotalPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdExcel)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.ExpProgress)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxDetails)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(1244, 599)
        Me.HazelDev_ThemeContainer1.TabIndex = 4
        Me.HazelDev_ThemeContainer1.Text = "SORTING TRANSFER"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'txtDiaValue
        '
        Me.txtDiaValue.Location = New System.Drawing.Point(924, 565)
        Me.txtDiaValue.Name = "txtDiaValue"
        Me.txtDiaValue.ReadOnly = True
        Me.txtDiaValue.Size = New System.Drawing.Size(100, 21)
        Me.txtDiaValue.TabIndex = 84
        Me.txtDiaValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtListValue
        '
        Me.txtListValue.Location = New System.Drawing.Point(712, 565)
        Me.txtListValue.Name = "txtListValue"
        Me.txtListValue.ReadOnly = True
        Me.txtListValue.Size = New System.Drawing.Size(100, 21)
        Me.txtListValue.TabIndex = 83
        Me.txtListValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtInvValue
        '
        Me.txtInvValue.Location = New System.Drawing.Point(818, 565)
        Me.txtInvValue.Name = "txtInvValue"
        Me.txtInvValue.ReadOnly = True
        Me.txtInvValue.Size = New System.Drawing.Size(100, 21)
        Me.txtInvValue.TabIndex = 82
        Me.txtInvValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.cmdTrf.Location = New System.Drawing.Point(1139, 567)
        Me.cmdTrf.Name = "cmdTrf"
        Me.cmdTrf.Size = New System.Drawing.Size(99, 28)
        Me.cmdTrf.TabIndex = 75
        Me.cmdTrf.Text = "Transfer"
        Me.cmdTrf.UseVisualStyleBackColor = False
        '
        'txtTotalCts
        '
        Me.txtTotalCts.Location = New System.Drawing.Point(604, 565)
        Me.txtTotalCts.Name = "txtTotalCts"
        Me.txtTotalCts.ReadOnly = True
        Me.txtTotalCts.Size = New System.Drawing.Size(102, 21)
        Me.txtTotalCts.TabIndex = 74
        Me.txtTotalCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalPcs
        '
        Me.txtTotalPcs.Location = New System.Drawing.Point(501, 565)
        Me.txtTotalPcs.Name = "txtTotalPcs"
        Me.txtTotalPcs.ReadOnly = True
        Me.txtTotalPcs.Size = New System.Drawing.Size(97, 21)
        Me.txtTotalPcs.TabIndex = 73
        Me.txtTotalPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.ExpProgress.Size = New System.Drawing.Size(1232, 24)
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
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Department, Me.DataGridViewTextBoxColumn3, Me.RareCode, Me.RecordNo, Me.Assortment, Me.PacketPcs, Me.PacketCts, Me.Price, Me.FinishedPcs, Me.FinishedCts, Me.IssuePcs, Me.Confirm, Me.Category, Me.ID, Me.Column1, Me.Column2})
        Me.flxDetails.Location = New System.Drawing.Point(6, 109)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.flxDetails.Size = New System.Drawing.Size(1232, 421)
        Me.flxDetails.TabIndex = 68
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.optRoughS)
        Me.HazelDev_Panel1.Controls.Add(Me.txtDepartment)
        Me.HazelDev_Panel1.Controls.Add(Me.optPolish)
        Me.HazelDev_Panel1.Controls.Add(Me.optAll)
        Me.HazelDev_Panel1.Controls.Add(Me.optRough)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdCalc)
        Me.HazelDev_Panel1.Controls.Add(Me.txtParcel)
        Me.HazelDev_Panel1.Controls.Add(Me.chkSelect)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdRefresh)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(1235, 49)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'optRoughS
        '
        Me.optRoughS.AutoSize = True
        Me.optRoughS.BackColor = System.Drawing.Color.White
        Me.optRoughS.Location = New System.Drawing.Point(642, 15)
        Me.optRoughS.Name = "optRoughS"
        Me.optRoughS.Size = New System.Drawing.Size(71, 18)
        Me.optRoughS.TabIndex = 168
        Me.optRoughS.Text = "Rough S"
        Me.optRoughS.UseVisualStyleBackColor = False
        '
        'txtDepartment
        '
        Me.txtDepartment.Location = New System.Drawing.Point(304, 13)
        Me.txtDepartment.Name = "txtDepartment"
        Me.txtDepartment.ReadOnly = True
        Me.txtDepartment.Size = New System.Drawing.Size(201, 22)
        Me.txtDepartment.TabIndex = 167
        '
        'optPolish
        '
        Me.optPolish.AutoSize = True
        Me.optPolish.BackColor = System.Drawing.Color.White
        Me.optPolish.Location = New System.Drawing.Point(719, 15)
        Me.optPolish.Name = "optPolish"
        Me.optPolish.Size = New System.Drawing.Size(69, 18)
        Me.optPolish.TabIndex = 166
        Me.optPolish.Text = "Polished"
        Me.optPolish.UseVisualStyleBackColor = False
        '
        'optAll
        '
        Me.optAll.AutoSize = True
        Me.optAll.BackColor = System.Drawing.Color.White
        Me.optAll.Checked = True
        Me.optAll.Location = New System.Drawing.Point(511, 15)
        Me.optAll.Name = "optAll"
        Me.optAll.Size = New System.Drawing.Size(37, 18)
        Me.optAll.TabIndex = 165
        Me.optAll.TabStop = True
        Me.optAll.Text = "All"
        Me.optAll.UseVisualStyleBackColor = False
        '
        'optRough
        '
        Me.optRough.AutoSize = True
        Me.optRough.BackColor = System.Drawing.Color.White
        Me.optRough.Location = New System.Drawing.Point(564, 15)
        Me.optRough.Name = "optRough"
        Me.optRough.Size = New System.Drawing.Size(72, 18)
        Me.optRough.TabIndex = 164
        Me.optRough.Text = "Rough A"
        Me.optRough.UseVisualStyleBackColor = False
        '
        'cmdCalc
        '
        Me.cmdCalc.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdCalc.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdCalc.FlatAppearance.BorderSize = 0
        Me.cmdCalc.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdCalc.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdCalc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCalc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCalc.ForeColor = System.Drawing.Color.White
        Me.cmdCalc.Location = New System.Drawing.Point(1017, 13)
        Me.cmdCalc.Name = "cmdCalc"
        Me.cmdCalc.Size = New System.Drawing.Size(99, 28)
        Me.cmdCalc.TabIndex = 93
        Me.cmdCalc.Text = "Calculate"
        Me.cmdCalc.UseVisualStyleBackColor = False
        '
        'txtParcel
        '
        Me.txtParcel.Location = New System.Drawing.Point(109, 13)
        Me.txtParcel.Name = "txtParcel"
        Me.txtParcel.Size = New System.Drawing.Size(84, 22)
        Me.txtParcel.TabIndex = 92
        '
        'chkSelect
        '
        Me.chkSelect.Checked = False
        Me.chkSelect.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkSelect.Location = New System.Drawing.Point(1136, 19)
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
        DataGridViewCellStyle1.NullValue = Nothing
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewTextBoxColumn3.HeaderText = "Par No."
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'RareCode
        '
        Me.RareCode.HeaderText = "Pkt No"
        Me.RareCode.Name = "RareCode"
        Me.RareCode.ReadOnly = True
        '
        'RecordNo
        '
        Me.RecordNo.HeaderText = "Org"
        Me.RecordNo.Name = "RecordNo"
        Me.RecordNo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.RecordNo.Width = 60
        '
        'Assortment
        '
        Me.Assortment.HeaderText = "Assortment"
        Me.Assortment.Name = "Assortment"
        Me.Assortment.ReadOnly = True
        Me.Assortment.Width = 160
        '
        'PacketPcs
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PacketPcs.DefaultCellStyle = DataGridViewCellStyle2
        Me.PacketPcs.HeaderText = "Pcs"
        Me.PacketPcs.Name = "PacketPcs"
        Me.PacketPcs.ReadOnly = True
        '
        'PacketCts
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PacketCts.DefaultCellStyle = DataGridViewCellStyle3
        Me.PacketCts.HeaderText = "Cts"
        Me.PacketCts.Name = "PacketCts"
        Me.PacketCts.ReadOnly = True
        '
        'Price
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Price.DefaultCellStyle = DataGridViewCellStyle4
        Me.Price.HeaderText = "Base Price"
        Me.Price.Name = "Price"
        Me.Price.ReadOnly = True
        '
        'FinishedPcs
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.FinishedPcs.DefaultCellStyle = DataGridViewCellStyle5
        Me.FinishedPcs.HeaderText = "Inv Price"
        Me.FinishedPcs.Name = "FinishedPcs"
        Me.FinishedPcs.ReadOnly = True
        '
        'FinishedCts
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.FinishedCts.DefaultCellStyle = DataGridViewCellStyle6
        Me.FinishedCts.HeaderText = "Adj Price"
        Me.FinishedCts.Name = "FinishedCts"
        Me.FinishedCts.ReadOnly = True
        '
        'IssuePcs
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.IssuePcs.DefaultCellStyle = DataGridViewCellStyle7
        Me.IssuePcs.HeaderText = "Import No"
        Me.IssuePcs.Name = "IssuePcs"
        Me.IssuePcs.ReadOnly = True
        '
        'Confirm
        '
        Me.Confirm.HeaderText = "Confirm"
        Me.Confirm.Name = "Confirm"
        '
        'Category
        '
        Me.Category.HeaderText = "Category"
        Me.Category.Name = "Category"
        Me.Category.ReadOnly = True
        '
        'ID
        '
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        Me.ID.ReadOnly = True
        '
        'Column1
        '
        Me.Column1.HeaderText = "Est Cts"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "Dia Cost"
        Me.Column2.Name = "Column2"
        '
        'frm_ExpSizingTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1244, 599)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_ExpSizingTransfer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sorting Transfer"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents ExpProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents cmdRefresh As DiaStock.HazelDev_Button
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents chkSelect As DiaStock.HazelDev_CheckBox
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents txtParcel As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalCts As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalPcs As System.Windows.Forms.TextBox
    Friend WithEvents cmdCalc As DiaStock.HazelDev_Button
    Friend WithEvents cmdTrf As DiaStock.HazelDev_Button
    Friend WithEvents optPolish As System.Windows.Forms.RadioButton
    Friend WithEvents optAll As System.Windows.Forms.RadioButton
    Friend WithEvents optRough As System.Windows.Forms.RadioButton
    Friend WithEvents txtInvValue As System.Windows.Forms.TextBox
    Friend WithEvents txtListValue As System.Windows.Forms.TextBox
    Friend WithEvents txtDepartment As System.Windows.Forms.TextBox
    Friend WithEvents optRoughS As System.Windows.Forms.RadioButton
    Friend WithEvents txtDiaValue As System.Windows.Forms.TextBox
    Friend WithEvents Department As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RareCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RecordNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Assortment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PacketPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PacketCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FinishedPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FinishedCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IssuePcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Confirm As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Category As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
