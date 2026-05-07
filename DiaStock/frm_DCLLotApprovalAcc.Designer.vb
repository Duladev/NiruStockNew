<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DCLLotApprovalAcc
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
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.HazelDev_Panel2 = New DiaStock.HazelDev_Panel()
        Me.flxLot = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmbOrigin = New System.Windows.Forms.ComboBox()
        Me.flxItem = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewCheckBoxColumn6 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.pnlDetails2 = New System.Windows.Forms.Panel()
        Me.flxProfit = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewCheckBoxColumn5 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.flxRounds = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewCheckBoxColumn3 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridViewCheckBoxColumn4 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.flxFancy = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridViewCheckBoxColumn2 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.optPolish = New System.Windows.Forms.RadioButton()
        Me.optRough = New System.Windows.Forms.RadioButton()
        Me.cmdSaveRnd = New DiaStock.HazelDev_Button()
        Me.cmdSaveFan = New DiaStock.HazelDev_Button()
        Me.cmdSaveRgh = New DiaStock.HazelDev_Button()
        Me.flxRough = New System.Windows.Forms.DataGridView()
        Me.LotNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.App = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Check = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdReport = New DiaStock.HazelDev_Button()
        Me.cmdLoad = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        Me.HazelDev_Panel2.SuspendLayout()
        CType(Me.flxLot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDetails2.SuspendLayout()
        CType(Me.flxProfit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxRounds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxFancy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxRough, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.pnlDetails2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(746, 574)
        Me.HazelDev_ThemeContainer1.TabIndex = 8
        Me.HazelDev_ThemeContainer1.Text = "LOT SELECTION"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'HazelDev_Panel2
        '
        Me.HazelDev_Panel2.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel2.Controls.Add(Me.flxLot)
        Me.HazelDev_Panel2.Controls.Add(Me.cmbOrigin)
        Me.HazelDev_Panel2.Controls.Add(Me.flxItem)
        Me.HazelDev_Panel2.Controls.Add(Me.Label20)
        Me.HazelDev_Panel2.Controls.Add(Me.dtpToDate)
        Me.HazelDev_Panel2.Controls.Add(Me.Label2)
        Me.HazelDev_Panel2.Controls.Add(Me.Label23)
        Me.HazelDev_Panel2.Controls.Add(Me.dtpFromDate)
        Me.HazelDev_Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel2.Location = New System.Drawing.Point(3, 110)
        Me.HazelDev_Panel2.Name = "HazelDev_Panel2"
        Me.HazelDev_Panel2.Size = New System.Drawing.Size(739, 461)
        Me.HazelDev_Panel2.TabIndex = 149
        Me.HazelDev_Panel2.Text = "HazelDev_Panel2"
        Me.HazelDev_Panel2.TransparencyKey = System.Drawing.Color.Empty
        '
        'flxLot
        '
        Me.flxLot.AllowUserToAddRows = False
        Me.flxLot.AllowUserToDeleteRows = False
        Me.flxLot.AllowUserToResizeColumns = False
        Me.flxLot.AllowUserToResizeRows = False
        Me.flxLot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxLot.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn5, Me.Column1, Me.Column2, Me.Column3})
        Me.flxLot.Location = New System.Drawing.Point(295, 52)
        Me.flxLot.Name = "flxLot"
        Me.flxLot.RowHeadersVisible = False
        Me.flxLot.Size = New System.Drawing.Size(430, 401)
        Me.flxLot.TabIndex = 149
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Lot No"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        '
        'Column1
        '
        Me.Column1.HeaderText = "Inv Cts"
        Me.Column1.Name = "Column1"
        '
        'Column2
        '
        Me.Column2.HeaderText = "Exp Cts"
        Me.Column2.Name = "Column2"
        '
        'Column3
        '
        Me.Column3.HeaderText = "Bal Cts"
        Me.Column3.Name = "Column3"
        '
        'cmbOrigin
        '
        Me.cmbOrigin.FormattingEnabled = True
        Me.cmbOrigin.IntegralHeight = False
        Me.cmbOrigin.Items.AddRange(New Object() {"Sawn", "Sawable"})
        Me.cmbOrigin.Location = New System.Drawing.Point(9, 24)
        Me.cmbOrigin.Name = "cmbOrigin"
        Me.cmbOrigin.Size = New System.Drawing.Size(172, 22)
        Me.cmbOrigin.TabIndex = 136
        '
        'flxItem
        '
        Me.flxItem.AllowUserToAddRows = False
        Me.flxItem.AllowUserToDeleteRows = False
        Me.flxItem.AllowUserToResizeColumns = False
        Me.flxItem.AllowUserToResizeRows = False
        Me.flxItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxItem.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn4, Me.DataGridViewCheckBoxColumn6})
        Me.flxItem.Location = New System.Drawing.Point(9, 52)
        Me.flxItem.Name = "flxItem"
        Me.flxItem.RowHeadersVisible = False
        Me.flxItem.Size = New System.Drawing.Size(280, 401)
        Me.flxItem.TabIndex = 148
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "Item Name"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 200
        '
        'DataGridViewCheckBoxColumn6
        '
        Me.DataGridViewCheckBoxColumn6.HeaderText = "Sel"
        Me.DataGridViewCheckBoxColumn6.Name = "DataGridViewCheckBoxColumn6"
        Me.DataGridViewCheckBoxColumn6.Width = 50
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Location = New System.Drawing.Point(6, 4)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(38, 14)
        Me.Label20.TabIndex = 137
        Me.Label20.Text = "Origin"
        '
        'dtpToDate
        '
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpToDate.Location = New System.Drawing.Point(295, 24)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(102, 22)
        Me.dtpToDate.TabIndex = 147
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(292, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 14)
        Me.Label2.TabIndex = 146
        Me.Label2.Text = "To Date"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Location = New System.Drawing.Point(184, 4)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(64, 14)
        Me.Label23.TabIndex = 144
        Me.Label23.Text = "From Date"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFromDate.Location = New System.Drawing.Point(187, 24)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(102, 22)
        Me.dtpFromDate.TabIndex = 145
        '
        'pnlDetails2
        '
        Me.pnlDetails2.BackColor = System.Drawing.Color.White
        Me.pnlDetails2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDetails2.Controls.Add(Me.flxProfit)
        Me.pnlDetails2.Controls.Add(Me.flxRounds)
        Me.pnlDetails2.Controls.Add(Me.flxFancy)
        Me.pnlDetails2.Controls.Add(Me.optPolish)
        Me.pnlDetails2.Controls.Add(Me.optRough)
        Me.pnlDetails2.Controls.Add(Me.cmdSaveRnd)
        Me.pnlDetails2.Controls.Add(Me.cmdSaveFan)
        Me.pnlDetails2.Controls.Add(Me.cmdSaveRgh)
        Me.pnlDetails2.Controls.Add(Me.flxRough)
        Me.pnlDetails2.Location = New System.Drawing.Point(831, 184)
        Me.pnlDetails2.Name = "pnlDetails2"
        Me.pnlDetails2.Size = New System.Drawing.Size(825, 422)
        Me.pnlDetails2.TabIndex = 68
        Me.pnlDetails2.Visible = False
        '
        'flxProfit
        '
        Me.flxProfit.AllowUserToAddRows = False
        Me.flxProfit.AllowUserToDeleteRows = False
        Me.flxProfit.AllowUserToResizeColumns = False
        Me.flxProfit.AllowUserToResizeRows = False
        Me.flxProfit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxProfit.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn3, Me.DataGridViewCheckBoxColumn5})
        Me.flxProfit.Location = New System.Drawing.Point(653, 3)
        Me.flxProfit.Name = "flxProfit"
        Me.flxProfit.RowHeadersVisible = False
        Me.flxProfit.Size = New System.Drawing.Size(160, 380)
        Me.flxProfit.TabIndex = 90
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Lot No"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 80
        '
        'DataGridViewCheckBoxColumn5
        '
        Me.DataGridViewCheckBoxColumn5.HeaderText = "App"
        Me.DataGridViewCheckBoxColumn5.Name = "DataGridViewCheckBoxColumn5"
        Me.DataGridViewCheckBoxColumn5.Width = 50
        '
        'flxRounds
        '
        Me.flxRounds.AllowUserToAddRows = False
        Me.flxRounds.AllowUserToDeleteRows = False
        Me.flxRounds.AllowUserToResizeColumns = False
        Me.flxRounds.AllowUserToResizeRows = False
        Me.flxRounds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxRounds.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn2, Me.DataGridViewCheckBoxColumn3, Me.DataGridViewCheckBoxColumn4})
        Me.flxRounds.Location = New System.Drawing.Point(438, 3)
        Me.flxRounds.Name = "flxRounds"
        Me.flxRounds.RowHeadersVisible = False
        Me.flxRounds.Size = New System.Drawing.Size(209, 380)
        Me.flxRounds.TabIndex = 89
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Lot No"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 80
        '
        'DataGridViewCheckBoxColumn3
        '
        Me.DataGridViewCheckBoxColumn3.HeaderText = "App"
        Me.DataGridViewCheckBoxColumn3.Name = "DataGridViewCheckBoxColumn3"
        Me.DataGridViewCheckBoxColumn3.Width = 50
        '
        'DataGridViewCheckBoxColumn4
        '
        Me.DataGridViewCheckBoxColumn4.HeaderText = "Check"
        Me.DataGridViewCheckBoxColumn4.Name = "DataGridViewCheckBoxColumn4"
        Me.DataGridViewCheckBoxColumn4.Width = 50
        '
        'flxFancy
        '
        Me.flxFancy.AllowUserToAddRows = False
        Me.flxFancy.AllowUserToDeleteRows = False
        Me.flxFancy.AllowUserToResizeColumns = False
        Me.flxFancy.AllowUserToResizeRows = False
        Me.flxFancy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxFancy.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewCheckBoxColumn1, Me.DataGridViewCheckBoxColumn2})
        Me.flxFancy.Location = New System.Drawing.Point(223, 3)
        Me.flxFancy.Name = "flxFancy"
        Me.flxFancy.RowHeadersVisible = False
        Me.flxFancy.Size = New System.Drawing.Size(209, 380)
        Me.flxFancy.TabIndex = 88
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Lot No"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 80
        '
        'DataGridViewCheckBoxColumn1
        '
        Me.DataGridViewCheckBoxColumn1.HeaderText = "App"
        Me.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
        Me.DataGridViewCheckBoxColumn1.Width = 50
        '
        'DataGridViewCheckBoxColumn2
        '
        Me.DataGridViewCheckBoxColumn2.HeaderText = "Check"
        Me.DataGridViewCheckBoxColumn2.Name = "DataGridViewCheckBoxColumn2"
        Me.DataGridViewCheckBoxColumn2.Width = 50
        '
        'optPolish
        '
        Me.optPolish.AutoSize = True
        Me.optPolish.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.optPolish.Location = New System.Drawing.Point(722, 395)
        Me.optPolish.Name = "optPolish"
        Me.optPolish.Size = New System.Drawing.Size(59, 19)
        Me.optPolish.TabIndex = 87
        Me.optPolish.Text = "Polish"
        Me.optPolish.UseVisualStyleBackColor = False
        '
        'optRough
        '
        Me.optRough.AutoSize = True
        Me.optRough.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.optRough.Checked = True
        Me.optRough.Location = New System.Drawing.Point(654, 395)
        Me.optRough.Name = "optRough"
        Me.optRough.Size = New System.Drawing.Size(62, 19)
        Me.optRough.TabIndex = 86
        Me.optRough.TabStop = True
        Me.optRough.Text = "Rough"
        Me.optRough.UseVisualStyleBackColor = False
        '
        'cmdSaveRnd
        '
        Me.cmdSaveRnd.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSaveRnd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSaveRnd.FlatAppearance.BorderSize = 0
        Me.cmdSaveRnd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdSaveRnd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdSaveRnd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSaveRnd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSaveRnd.ForeColor = System.Drawing.Color.White
        Me.cmdSaveRnd.Location = New System.Drawing.Point(438, 389)
        Me.cmdSaveRnd.Name = "cmdSaveRnd"
        Me.cmdSaveRnd.Size = New System.Drawing.Size(99, 28)
        Me.cmdSaveRnd.TabIndex = 80
        Me.cmdSaveRnd.Text = "Save Rounds"
        Me.cmdSaveRnd.UseVisualStyleBackColor = False
        '
        'cmdSaveFan
        '
        Me.cmdSaveFan.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSaveFan.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSaveFan.FlatAppearance.BorderSize = 0
        Me.cmdSaveFan.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdSaveFan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdSaveFan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSaveFan.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSaveFan.ForeColor = System.Drawing.Color.White
        Me.cmdSaveFan.Location = New System.Drawing.Point(223, 389)
        Me.cmdSaveFan.Name = "cmdSaveFan"
        Me.cmdSaveFan.Size = New System.Drawing.Size(99, 28)
        Me.cmdSaveFan.TabIndex = 79
        Me.cmdSaveFan.Text = "Save Fancy"
        Me.cmdSaveFan.UseVisualStyleBackColor = False
        '
        'cmdSaveRgh
        '
        Me.cmdSaveRgh.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSaveRgh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSaveRgh.FlatAppearance.BorderSize = 0
        Me.cmdSaveRgh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdSaveRgh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdSaveRgh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSaveRgh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSaveRgh.ForeColor = System.Drawing.Color.White
        Me.cmdSaveRgh.Location = New System.Drawing.Point(6, 389)
        Me.cmdSaveRgh.Name = "cmdSaveRgh"
        Me.cmdSaveRgh.Size = New System.Drawing.Size(99, 28)
        Me.cmdSaveRgh.TabIndex = 45
        Me.cmdSaveRgh.Text = "Save Rough"
        Me.cmdSaveRgh.UseVisualStyleBackColor = False
        '
        'flxRough
        '
        Me.flxRough.AllowUserToAddRows = False
        Me.flxRough.AllowUserToDeleteRows = False
        Me.flxRough.AllowUserToResizeColumns = False
        Me.flxRough.AllowUserToResizeRows = False
        Me.flxRough.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxRough.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.LotNo, Me.App, Me.Check})
        Me.flxRough.Location = New System.Drawing.Point(6, 3)
        Me.flxRough.Name = "flxRough"
        Me.flxRough.RowHeadersVisible = False
        Me.flxRough.Size = New System.Drawing.Size(209, 380)
        Me.flxRough.TabIndex = 44
        '
        'LotNo
        '
        Me.LotNo.HeaderText = "Lot No"
        Me.LotNo.Name = "LotNo"
        Me.LotNo.ReadOnly = True
        Me.LotNo.Width = 80
        '
        'App
        '
        Me.App.HeaderText = "App"
        Me.App.Name = "App"
        Me.App.Width = 50
        '
        'Check
        '
        Me.Check.HeaderText = "Check"
        Me.Check.Name = "Check"
        Me.Check.Width = 50
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmdReport)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdLoad)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(739, 50)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmdReport
        '
        Me.cmdReport.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdReport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdReport.FlatAppearance.BorderSize = 0
        Me.cmdReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdReport.ForeColor = System.Drawing.Color.White
        Me.cmdReport.Location = New System.Drawing.Point(626, 13)
        Me.cmdReport.Name = "cmdReport"
        Me.cmdReport.Size = New System.Drawing.Size(99, 28)
        Me.cmdReport.TabIndex = 34
        Me.cmdReport.Text = "Report"
        Me.cmdReport.UseVisualStyleBackColor = False
        '
        'cmdLoad
        '
        Me.cmdLoad.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdLoad.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdLoad.FlatAppearance.BorderSize = 0
        Me.cmdLoad.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdLoad.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdLoad.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLoad.ForeColor = System.Drawing.Color.White
        Me.cmdLoad.Location = New System.Drawing.Point(295, 13)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.Size = New System.Drawing.Size(99, 28)
        Me.cmdLoad.TabIndex = 33
        Me.cmdLoad.Text = "Load"
        Me.cmdLoad.UseVisualStyleBackColor = False
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
        'frm_DCLLotApprovalAcc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(746, 574)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_DCLLotApprovalAcc"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Lot Approval Accept"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_Panel2.ResumeLayout(False)
        Me.HazelDev_Panel2.PerformLayout()
        CType(Me.flxLot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDetails2.ResumeLayout(False)
        Me.pnlDetails2.PerformLayout()
        CType(Me.flxProfit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxRounds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxFancy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxRough, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents pnlDetails2 As System.Windows.Forms.Panel
    Friend WithEvents cmdSaveRnd As DiaStock.HazelDev_Button
    Friend WithEvents cmdSaveFan As DiaStock.HazelDev_Button
    Friend WithEvents cmdSaveRgh As DiaStock.HazelDev_Button
    Friend WithEvents flxRough As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents optPolish As System.Windows.Forms.RadioButton
    Friend WithEvents optRough As System.Windows.Forms.RadioButton
    Friend WithEvents flxProfit As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn5 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents flxRounds As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn3 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn4 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents flxFancy As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn2 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents LotNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents App As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Check As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cmbOrigin As System.Windows.Forms.ComboBox
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents flxItem As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel2 As DiaStock.HazelDev_Panel
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn6 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents flxLot As System.Windows.Forms.DataGridView
    Friend WithEvents cmdLoad As DiaStock.HazelDev_Button
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdReport As DiaStock.HazelDev_Button
End Class
