<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_MixSolver
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
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.cmdLoad = New DiaStock.HazelDev_Button()
        Me.cmdOpen = New DiaStock.HazelDev_Button()
        Me.chkFull = New DiaStock.HazelDev_CheckBox()
        Me.chkEmpIssues = New DiaStock.HazelDev_CheckBox()
        Me.txtSelPcs = New System.Windows.Forms.TextBox()
        Me.ExpProgress = New System.Windows.Forms.ProgressBar()
        Me.txtTotCts = New System.Windows.Forms.TextBox()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.Assortment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Color = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AvgWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Make = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AdjCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RateStone = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Length = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Width1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BaseCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BaseCostStone = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BagAssort = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BagCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BagCostStone = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdExcel = New DiaStock.HazelDev_Button()
        Me.txtTotPcs = New System.Windows.Forms.TextBox()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbType = New System.Windows.Forms.ComboBox()
        Me.cmdFindAll = New DiaStock.HazelDev_Button()
        Me.Type = New System.Windows.Forms.Label()
        Me.cmbAssortType = New System.Windows.Forms.ComboBox()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.chkSelect = New DiaStock.HazelDev_CheckBox()
        Me.chkAdvance = New DiaStock.HazelDev_CheckBox()
        Me.cmdFind = New DiaStock.HazelDev_Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtWidth = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtLength = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbMake = New System.Windows.Forms.ComboBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.cmbColor = New System.Windows.Forms.ComboBox()
        Me.cmdClear = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtFilePath)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdLoad)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdOpen)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkFull)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkEmpIssues)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtSelPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.ExpProgress)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxDetails)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdExcel)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(1245, 597)
        Me.HazelDev_ThemeContainer1.TabIndex = 6
        Me.HazelDev_ThemeContainer1.Text = "MIX ASSORTMENT SOLVER"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'txtFilePath
        '
        Me.txtFilePath.Enabled = False
        Me.txtFilePath.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilePath.Location = New System.Drawing.Point(840, 539)
        Me.txtFilePath.MaxLength = 0
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.Size = New System.Drawing.Size(279, 21)
        Me.txtFilePath.TabIndex = 177
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
        Me.cmdLoad.Location = New System.Drawing.Point(732, 536)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.Size = New System.Drawing.Size(99, 28)
        Me.cmdLoad.TabIndex = 176
        Me.cmdLoad.Text = "Load File"
        Me.cmdLoad.UseVisualStyleBackColor = False
        '
        'cmdOpen
        '
        Me.cmdOpen.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdOpen.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdOpen.FlatAppearance.BorderSize = 0
        Me.cmdOpen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdOpen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOpen.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOpen.ForeColor = System.Drawing.Color.White
        Me.cmdOpen.Location = New System.Drawing.Point(627, 536)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(99, 28)
        Me.cmdOpen.TabIndex = 175
        Me.cmdOpen.Text = "Open File"
        Me.cmdOpen.UseVisualStyleBackColor = False
        '
        'chkFull
        '
        Me.chkFull.Checked = False
        Me.chkFull.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkFull.Location = New System.Drawing.Point(544, 536)
        Me.chkFull.Name = "chkFull"
        Me.chkFull.Size = New System.Drawing.Size(77, 22)
        Me.chkFull.TabIndex = 174
        Me.chkFull.Text = "Full"
        Me.chkFull.TransparencyKey = System.Drawing.Color.Empty
        '
        'chkEmpIssues
        '
        Me.chkEmpIssues.Checked = False
        Me.chkEmpIssues.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkEmpIssues.Location = New System.Drawing.Point(6, 539)
        Me.chkEmpIssues.Name = "chkEmpIssues"
        Me.chkEmpIssues.Size = New System.Drawing.Size(84, 16)
        Me.chkEmpIssues.TabIndex = 173
        Me.chkEmpIssues.Text = "Emp Issues"
        Me.chkEmpIssues.TransparencyKey = System.Drawing.Color.Empty
        '
        'txtSelPcs
        '
        Me.txtSelPcs.Location = New System.Drawing.Point(318, 534)
        Me.txtSelPcs.Name = "txtSelPcs"
        Me.txtSelPcs.ReadOnly = True
        Me.txtSelPcs.Size = New System.Drawing.Size(99, 21)
        Me.txtSelPcs.TabIndex = 171
        Me.txtSelPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ExpProgress
        '
        Me.ExpProgress.Location = New System.Drawing.Point(3, 568)
        Me.ExpProgress.Name = "ExpProgress"
        Me.ExpProgress.Size = New System.Drawing.Size(1232, 24)
        Me.ExpProgress.TabIndex = 70
        Me.ExpProgress.Visible = False
        '
        'txtTotCts
        '
        Me.txtTotCts.Location = New System.Drawing.Point(213, 534)
        Me.txtTotCts.Name = "txtTotCts"
        Me.txtTotCts.ReadOnly = True
        Me.txtTotCts.Size = New System.Drawing.Size(99, 21)
        Me.txtTotCts.TabIndex = 164
        Me.txtTotCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Assortment, Me.Pcs, Me.Cts, Me.Column1, Me.Column2, Me.Color, Me.AvgWeight, Me.Make, Me.AdjCost, Me.RateStone, Me.Length, Me.Width1, Me.BaseCost, Me.BaseCostStone, Me.BagAssort, Me.BagCost, Me.BagCostStone, Me.Column3})
        Me.flxDetails.Location = New System.Drawing.Point(6, 109)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.flxDetails.Size = New System.Drawing.Size(1232, 421)
        Me.flxDetails.TabIndex = 68
        '
        'Assortment
        '
        Me.Assortment.HeaderText = "Assortment"
        Me.Assortment.Name = "Assortment"
        Me.Assortment.ReadOnly = True
        Me.Assortment.Width = 160
        '
        'Pcs
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Pcs.DefaultCellStyle = DataGridViewCellStyle1
        Me.Pcs.HeaderText = "Pcs"
        Me.Pcs.Name = "Pcs"
        Me.Pcs.ReadOnly = True
        '
        'Cts
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Cts.DefaultCellStyle = DataGridViewCellStyle2
        Me.Cts.HeaderText = "Cts"
        Me.Cts.Name = "Cts"
        Me.Cts.ReadOnly = True
        '
        'Column1
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle3
        Me.Column1.HeaderText = "Sel Pcs"
        Me.Column1.Name = "Column1"
        '
        'Column2
        '
        Me.Column2.HeaderText = "Select"
        Me.Column2.Name = "Column2"
        '
        'Color
        '
        Me.Color.HeaderText = "Color"
        Me.Color.Name = "Color"
        Me.Color.ReadOnly = True
        '
        'AvgWeight
        '
        Me.AvgWeight.HeaderText = "Avg Weight"
        Me.AvgWeight.Name = "AvgWeight"
        Me.AvgWeight.ReadOnly = True
        '
        'Make
        '
        Me.Make.HeaderText = "Make"
        Me.Make.Name = "Make"
        Me.Make.ReadOnly = True
        '
        'AdjCost
        '
        Me.AdjCost.HeaderText = "Adj Cost"
        Me.AdjCost.Name = "AdjCost"
        Me.AdjCost.ReadOnly = True
        '
        'RateStone
        '
        Me.RateStone.HeaderText = "Rate/Stone"
        Me.RateStone.Name = "RateStone"
        Me.RateStone.ReadOnly = True
        '
        'Length
        '
        Me.Length.HeaderText = "Length"
        Me.Length.Name = "Length"
        Me.Length.ReadOnly = True
        '
        'Width1
        '
        Me.Width1.HeaderText = "Width"
        Me.Width1.Name = "Width1"
        Me.Width1.ReadOnly = True
        '
        'BaseCost
        '
        Me.BaseCost.HeaderText = "Base Cost"
        Me.BaseCost.Name = "BaseCost"
        Me.BaseCost.ReadOnly = True
        '
        'BaseCostStone
        '
        Me.BaseCostStone.HeaderText = "Base Cost/Stone"
        Me.BaseCostStone.Name = "BaseCostStone"
        Me.BaseCostStone.ReadOnly = True
        '
        'BagAssort
        '
        Me.BagAssort.HeaderText = "Bag Assort"
        Me.BagAssort.Name = "BagAssort"
        Me.BagAssort.ReadOnly = True
        '
        'BagCost
        '
        Me.BagCost.HeaderText = "Bag Cost"
        Me.BagCost.Name = "BagCost"
        Me.BagCost.ReadOnly = True
        '
        'BagCostStone
        '
        Me.BagCostStone.HeaderText = "Bag Cost/Stone"
        Me.BagCostStone.Name = "BagCostStone"
        Me.BagCostStone.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.HeaderText = "Sizing Pcs"
        Me.Column3.Name = "Column3"
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
        Me.cmdExcel.Location = New System.Drawing.Point(1138, 534)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(99, 28)
        Me.cmdExcel.TabIndex = 165
        Me.cmdExcel.Text = "Excel"
        Me.cmdExcel.UseVisualStyleBackColor = False
        '
        'txtTotPcs
        '
        Me.txtTotPcs.Location = New System.Drawing.Point(108, 534)
        Me.txtTotPcs.Name = "txtTotPcs"
        Me.txtTotPcs.ReadOnly = True
        Me.txtTotPcs.Size = New System.Drawing.Size(99, 21)
        Me.txtTotPcs.TabIndex = 163
        Me.txtTotPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.Label3)
        Me.HazelDev_Panel1.Controls.Add(Me.cmbType)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdFindAll)
        Me.HazelDev_Panel1.Controls.Add(Me.Type)
        Me.HazelDev_Panel1.Controls.Add(Me.cmbAssortType)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.chkSelect)
        Me.HazelDev_Panel1.Controls.Add(Me.chkAdvance)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdFind)
        Me.HazelDev_Panel1.Controls.Add(Me.Label2)
        Me.HazelDev_Panel1.Controls.Add(Me.txtWidth)
        Me.HazelDev_Panel1.Controls.Add(Me.Label10)
        Me.HazelDev_Panel1.Controls.Add(Me.txtLength)
        Me.HazelDev_Panel1.Controls.Add(Me.Label1)
        Me.HazelDev_Panel1.Controls.Add(Me.cmbMake)
        Me.HazelDev_Panel1.Controls.Add(Me.Label25)
        Me.HazelDev_Panel1.Controls.Add(Me.cmbColor)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdClear)
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
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(916, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 14)
        Me.Label3.TabIndex = 180
        Me.Label3.Text = "Type"
        '
        'cmbType
        '
        Me.cmbType.FormattingEnabled = True
        Me.cmbType.IntegralHeight = False
        Me.cmbType.Location = New System.Drawing.Point(919, 19)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Size = New System.Drawing.Size(83, 22)
        Me.cmbType.TabIndex = 179
        '
        'cmdFindAll
        '
        Me.cmdFindAll.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdFindAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdFindAll.FlatAppearance.BorderSize = 0
        Me.cmdFindAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdFindAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdFindAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdFindAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFindAll.ForeColor = System.Drawing.Color.White
        Me.cmdFindAll.Location = New System.Drawing.Point(1017, 13)
        Me.cmdFindAll.Name = "cmdFindAll"
        Me.cmdFindAll.Size = New System.Drawing.Size(99, 28)
        Me.cmdFindAll.TabIndex = 176
        Me.cmdFindAll.Text = "Find All"
        Me.cmdFindAll.UseVisualStyleBackColor = False
        '
        'Type
        '
        Me.Type.AutoSize = True
        Me.Type.Location = New System.Drawing.Point(834, 2)
        Me.Type.Name = "Type"
        Me.Type.Size = New System.Drawing.Size(56, 14)
        Me.Type.TabIndex = 175
        Me.Type.Text = "Category"
        '
        'cmbAssortType
        '
        Me.cmbAssortType.FormattingEnabled = True
        Me.cmbAssortType.IntegralHeight = False
        Me.cmbAssortType.Location = New System.Drawing.Point(837, 19)
        Me.cmbAssortType.MaxLength = 3
        Me.cmbAssortType.Name = "cmbAssortType"
        Me.cmbAssortType.Size = New System.Drawing.Size(76, 22)
        Me.cmbAssortType.TabIndex = 174
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
        Me.cmdSave.Location = New System.Drawing.Point(729, 13)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(99, 28)
        Me.cmdSave.TabIndex = 173
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'chkSelect
        '
        Me.chkSelect.Checked = False
        Me.chkSelect.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkSelect.Location = New System.Drawing.Point(1135, 17)
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Size = New System.Drawing.Size(84, 16)
        Me.chkSelect.TabIndex = 172
        Me.chkSelect.Text = "Select All"
        Me.chkSelect.TransparencyKey = System.Drawing.Color.Empty
        '
        'chkAdvance
        '
        Me.chkAdvance.Checked = False
        Me.chkAdvance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkAdvance.Location = New System.Drawing.Point(541, 17)
        Me.chkAdvance.Name = "chkAdvance"
        Me.chkAdvance.Size = New System.Drawing.Size(77, 22)
        Me.chkAdvance.TabIndex = 148
        Me.chkAdvance.Text = "Advance"
        Me.chkAdvance.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmdFind
        '
        Me.cmdFind.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdFind.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdFind.FlatAppearance.BorderSize = 0
        Me.cmdFind.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdFind.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdFind.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFind.ForeColor = System.Drawing.Color.White
        Me.cmdFind.Location = New System.Drawing.Point(624, 13)
        Me.cmdFind.Name = "cmdFind"
        Me.cmdFind.Size = New System.Drawing.Size(99, 28)
        Me.cmdFind.TabIndex = 147
        Me.cmdFind.Text = "Find"
        Me.cmdFind.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(468, 2)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 14)
        Me.Label2.TabIndex = 146
        Me.Label2.Text = "Width"
        '
        'txtWidth
        '
        Me.txtWidth.Location = New System.Drawing.Point(471, 19)
        Me.txtWidth.Name = "txtWidth"
        Me.txtWidth.Size = New System.Drawing.Size(61, 22)
        Me.txtWidth.TabIndex = 145
        Me.txtWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(401, 2)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(46, 14)
        Me.Label10.TabIndex = 144
        Me.Label10.Text = "Length"
        '
        'txtLength
        '
        Me.txtLength.Location = New System.Drawing.Point(404, 19)
        Me.txtLength.Name = "txtLength"
        Me.txtLength.Size = New System.Drawing.Size(61, 22)
        Me.txtLength.TabIndex = 143
        Me.txtLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(315, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 14)
        Me.Label1.TabIndex = 82
        Me.Label1.Text = "Make"
        '
        'cmbMake
        '
        Me.cmbMake.FormattingEnabled = True
        Me.cmbMake.IntegralHeight = False
        Me.cmbMake.Location = New System.Drawing.Point(315, 19)
        Me.cmbMake.Name = "cmbMake"
        Me.cmbMake.Size = New System.Drawing.Size(83, 22)
        Me.cmbMake.TabIndex = 81
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Location = New System.Drawing.Point(220, 2)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(34, 14)
        Me.Label25.TabIndex = 80
        Me.Label25.Text = "Color"
        '
        'cmbColor
        '
        Me.cmbColor.FormattingEnabled = True
        Me.cmbColor.IntegralHeight = False
        Me.cmbColor.Location = New System.Drawing.Point(223, 19)
        Me.cmbColor.Name = "cmbColor"
        Me.cmbColor.Size = New System.Drawing.Size(86, 22)
        Me.cmbColor.TabIndex = 79
        '
        'cmdClear
        '
        Me.cmdClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdClear.FlatAppearance.BorderSize = 0
        Me.cmdClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClear.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClear.ForeColor = System.Drawing.Color.White
        Me.cmdClear.Location = New System.Drawing.Point(109, 13)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(99, 28)
        Me.cmdClear.TabIndex = 33
        Me.cmdClear.Text = "Clear"
        Me.cmdClear.UseVisualStyleBackColor = False
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
        'frm_MixSolver
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1245, 597)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_MixSolver"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mix Assortment Solver"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdClear As DiaStock.HazelDev_Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbMake As System.Windows.Forms.ComboBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents cmbColor As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtWidth As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtLength As System.Windows.Forms.TextBox
    Friend WithEvents cmdFind As DiaStock.HazelDev_Button
    Friend WithEvents chkAdvance As DiaStock.HazelDev_CheckBox
    Friend WithEvents txtTotCts As System.Windows.Forms.TextBox
    Friend WithEvents txtTotPcs As System.Windows.Forms.TextBox
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents ExpProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents chkSelect As DiaStock.HazelDev_CheckBox
    Friend WithEvents txtSelPcs As System.Windows.Forms.TextBox
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents Type As System.Windows.Forms.Label
    Friend WithEvents cmbAssortType As System.Windows.Forms.ComboBox
    Friend WithEvents cmdFindAll As DiaStock.HazelDev_Button
    Friend WithEvents chkEmpIssues As DiaStock.HazelDev_CheckBox
    Friend WithEvents chkFull As DiaStock.HazelDev_CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbType As System.Windows.Forms.ComboBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cmdLoad As DiaStock.HazelDev_Button
    Friend WithEvents cmdOpen As DiaStock.HazelDev_Button
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
    Friend WithEvents Assortment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Color As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AvgWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Make As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AdjCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RateStone As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Length As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Width1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BaseCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BaseCostStone As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BagAssort As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BagCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BagCostStone As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
