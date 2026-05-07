<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DCLDeptTransLot
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
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.txtLabour = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtDiaLabCost = New System.Windows.Forms.TextBox()
        Me.txtDiaCost = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtAnaCost = New System.Windows.Forms.TextBox()
        Me.txtListCost = New System.Windows.Forms.TextBox()
        Me.chkSpecial = New DiaStock.HazelDev_CheckBox()
        Me.chkInternal = New DiaStock.HazelDev_CheckBox()
        Me.txtOrigin = New System.Windows.Forms.TextBox()
        Me.chkOriginal = New DiaStock.HazelDev_CheckBox()
        Me.cmdLoad = New DiaStock.HazelDev_Button()
        Me.cmdOpen = New DiaStock.HazelDev_Button()
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.txtLotNo = New System.Windows.Forms.TextBox()
        Me.txtTotCts = New System.Windows.Forms.TextBox()
        Me.txtTotPcs = New System.Windows.Forms.TextBox()
        Me.txtCompCode = New System.Windows.Forms.TextBox()
        Me.cmbOrgAssort = New System.Windows.Forms.ComboBox()
        Me.chkSelect = New DiaStock.HazelDev_CheckBox()
        Me.txtCts = New System.Windows.Forms.TextBox()
        Me.txtPcs = New System.Windows.Forms.TextBox()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Assortment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DepartmentName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OutTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Company = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InvCts = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdExcel = New DiaStock.HazelDev_Button()
        Me.optKit = New System.Windows.Forms.RadioButton()
        Me.optMix = New System.Windows.Forms.RadioButton()
        Me.optPolishOK = New System.Windows.Forms.RadioButton()
        Me.optPolish = New System.Windows.Forms.RadioButton()
        Me.optApcu = New System.Windows.Forms.RadioButton()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.txtImportNo = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtLabour)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label7)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtDiaLabCost)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtDiaCost)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label4)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label6)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label3)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtAnaCost)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtListCost)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkSpecial)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkInternal)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtOrigin)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkOriginal)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdLoad)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdOpen)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtFilePath)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtLotNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtCompCode)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbOrgAssort)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkSelect)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxDetails)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtImportNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label5)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(945, 643)
        Me.HazelDev_ThemeContainer1.TabIndex = 2
        Me.HazelDev_ThemeContainer1.Text = "DEPARTMENT TRANSFER LOT"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'txtLabour
        '
        Me.txtLabour.Location = New System.Drawing.Point(831, 559)
        Me.txtLabour.Name = "txtLabour"
        Me.txtLabour.ReadOnly = True
        Me.txtLabour.Size = New System.Drawing.Size(102, 21)
        Me.txtLabour.TabIndex = 118
        Me.txtLabour.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(748, 559)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(46, 15)
        Me.Label7.TabIndex = 117
        Me.Label7.Text = "Labour"
        '
        'txtDiaLabCost
        '
        Me.txtDiaLabCost.Location = New System.Drawing.Point(831, 613)
        Me.txtDiaLabCost.Name = "txtDiaLabCost"
        Me.txtDiaLabCost.ReadOnly = True
        Me.txtDiaLabCost.Size = New System.Drawing.Size(102, 21)
        Me.txtDiaLabCost.TabIndex = 116
        Me.txtDiaLabCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDiaCost
        '
        Me.txtDiaCost.Location = New System.Drawing.Point(831, 586)
        Me.txtDiaCost.Name = "txtDiaCost"
        Me.txtDiaCost.ReadOnly = True
        Me.txtDiaCost.Size = New System.Drawing.Size(102, 21)
        Me.txtDiaCost.TabIndex = 115
        Me.txtDiaCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(748, 613)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 15)
        Me.Label4.TabIndex = 114
        Me.Label4.Text = "Dia Lab Cost"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(748, 586)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 15)
        Me.Label6.TabIndex = 113
        Me.Label6.Text = "Dia Cost"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(558, 613)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 15)
        Me.Label3.TabIndex = 110
        Me.Label3.Text = "Analyzed"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(558, 586)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 15)
        Me.Label2.TabIndex = 109
        Me.Label2.Text = "List"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(558, 559)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 15)
        Me.Label1.TabIndex = 108
        Me.Label1.Text = "Lot ID"
        '
        'txtAnaCost
        '
        Me.txtAnaCost.Location = New System.Drawing.Point(621, 613)
        Me.txtAnaCost.Name = "txtAnaCost"
        Me.txtAnaCost.ReadOnly = True
        Me.txtAnaCost.Size = New System.Drawing.Size(102, 21)
        Me.txtAnaCost.TabIndex = 107
        Me.txtAnaCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtListCost
        '
        Me.txtListCost.Location = New System.Drawing.Point(621, 586)
        Me.txtListCost.Name = "txtListCost"
        Me.txtListCost.ReadOnly = True
        Me.txtListCost.Size = New System.Drawing.Size(102, 21)
        Me.txtListCost.TabIndex = 106
        Me.txtListCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkSpecial
        '
        Me.chkSpecial.Checked = False
        Me.chkSpecial.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkSpecial.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSpecial.Location = New System.Drawing.Point(217, 583)
        Me.chkSpecial.Name = "chkSpecial"
        Me.chkSpecial.Size = New System.Drawing.Size(112, 21)
        Me.chkSpecial.TabIndex = 105
        Me.chkSpecial.Text = "Special Price"
        Me.chkSpecial.TransparencyKey = System.Drawing.Color.Empty
        '
        'chkInternal
        '
        Me.chkInternal.Checked = False
        Me.chkInternal.Enabled = False
        Me.chkInternal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkInternal.Location = New System.Drawing.Point(551, 113)
        Me.chkInternal.Name = "chkInternal"
        Me.chkInternal.Size = New System.Drawing.Size(73, 17)
        Me.chkInternal.TabIndex = 104
        Me.chkInternal.Text = "Internal"
        Me.chkInternal.TransparencyKey = System.Drawing.Color.Empty
        '
        'txtOrigin
        '
        Me.txtOrigin.Location = New System.Drawing.Point(415, 610)
        Me.txtOrigin.Name = "txtOrigin"
        Me.txtOrigin.ReadOnly = True
        Me.txtOrigin.Size = New System.Drawing.Size(102, 21)
        Me.txtOrigin.TabIndex = 79
        '
        'chkOriginal
        '
        Me.chkOriginal.Checked = False
        Me.chkOriginal.Enabled = False
        Me.chkOriginal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkOriginal.Location = New System.Drawing.Point(480, 113)
        Me.chkOriginal.Name = "chkOriginal"
        Me.chkOriginal.Size = New System.Drawing.Size(65, 16)
        Me.chkOriginal.TabIndex = 78
        Me.chkOriginal.Text = "Original"
        Me.chkOriginal.TransparencyKey = System.Drawing.Color.Empty
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
        Me.cmdLoad.Location = New System.Drawing.Point(112, 603)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.Size = New System.Drawing.Size(99, 28)
        Me.cmdLoad.TabIndex = 77
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
        Me.cmdOpen.Location = New System.Drawing.Point(7, 603)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(99, 28)
        Me.cmdOpen.TabIndex = 76
        Me.cmdOpen.Text = "Open File"
        Me.cmdOpen.UseVisualStyleBackColor = False
        '
        'txtFilePath
        '
        Me.txtFilePath.Enabled = False
        Me.txtFilePath.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilePath.Location = New System.Drawing.Point(217, 609)
        Me.txtFilePath.MaxLength = 0
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.Size = New System.Drawing.Size(192, 21)
        Me.txtFilePath.TabIndex = 75
        Me.txtFilePath.Visible = False
        '
        'txtLotNo
        '
        Me.txtLotNo.Location = New System.Drawing.Point(621, 559)
        Me.txtLotNo.Name = "txtLotNo"
        Me.txtLotNo.ReadOnly = True
        Me.txtLotNo.Size = New System.Drawing.Size(102, 21)
        Me.txtLotNo.TabIndex = 74
        '
        'txtTotCts
        '
        Me.txtTotCts.Location = New System.Drawing.Point(115, 559)
        Me.txtTotCts.Name = "txtTotCts"
        Me.txtTotCts.ReadOnly = True
        Me.txtTotCts.Size = New System.Drawing.Size(102, 21)
        Me.txtTotCts.TabIndex = 73
        Me.txtTotCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotPcs
        '
        Me.txtTotPcs.Location = New System.Drawing.Point(7, 559)
        Me.txtTotPcs.Name = "txtTotPcs"
        Me.txtTotPcs.ReadOnly = True
        Me.txtTotPcs.Size = New System.Drawing.Size(102, 21)
        Me.txtTotPcs.TabIndex = 72
        Me.txtTotPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCompCode
        '
        Me.txtCompCode.Location = New System.Drawing.Point(372, 109)
        Me.txtCompCode.Name = "txtCompCode"
        Me.txtCompCode.ReadOnly = True
        Me.txtCompCode.Size = New System.Drawing.Size(102, 21)
        Me.txtCompCode.TabIndex = 71
        '
        'cmbOrgAssort
        '
        Me.cmbOrgAssort.FormattingEnabled = True
        Me.cmbOrgAssort.IntegralHeight = False
        Me.cmbOrgAssort.Location = New System.Drawing.Point(217, 109)
        Me.cmbOrgAssort.Name = "cmbOrgAssort"
        Me.cmbOrgAssort.Size = New System.Drawing.Size(149, 23)
        Me.cmbOrgAssort.TabIndex = 70
        '
        'chkSelect
        '
        Me.chkSelect.Checked = False
        Me.chkSelect.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkSelect.Location = New System.Drawing.Point(630, 114)
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Size = New System.Drawing.Size(84, 16)
        Me.chkSelect.TabIndex = 68
        Me.chkSelect.Text = "Select All"
        Me.chkSelect.TransparencyKey = System.Drawing.Color.Empty
        '
        'txtCts
        '
        Me.txtCts.Location = New System.Drawing.Point(415, 559)
        Me.txtCts.Name = "txtCts"
        Me.txtCts.ReadOnly = True
        Me.txtCts.Size = New System.Drawing.Size(102, 21)
        Me.txtCts.TabIndex = 67
        Me.txtCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPcs
        '
        Me.txtPcs.Location = New System.Drawing.Point(307, 559)
        Me.txtPcs.Name = "txtPcs"
        Me.txtPcs.ReadOnly = True
        Me.txtPcs.Size = New System.Drawing.Size(102, 21)
        Me.txtPcs.TabIndex = 66
        Me.txtPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Code, Me.Assortment, Me.DepartmentName, Me.InTime, Me.OutTime, Me.Company, Me.InvCts, Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column6, Me.Column5})
        Me.flxDetails.Location = New System.Drawing.Point(7, 146)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(926, 407)
        Me.flxDetails.TabIndex = 43
        '
        'Code
        '
        Me.Code.HeaderText = "Lot No."
        Me.Code.Name = "Code"
        Me.Code.ReadOnly = True
        '
        'Assortment
        '
        Me.Assortment.HeaderText = "Assortment"
        Me.Assortment.Name = "Assortment"
        Me.Assortment.ReadOnly = True
        '
        'DepartmentName
        '
        Me.DepartmentName.HeaderText = "Par No."
        Me.DepartmentName.Name = "DepartmentName"
        Me.DepartmentName.ReadOnly = True
        '
        'InTime
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.NullValue = Nothing
        Me.InTime.DefaultCellStyle = DataGridViewCellStyle1
        Me.InTime.HeaderText = "Pcs"
        Me.InTime.Name = "InTime"
        Me.InTime.ReadOnly = True
        '
        'OutTime
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.NullValue = Nothing
        Me.OutTime.DefaultCellStyle = DataGridViewCellStyle2
        Me.OutTime.HeaderText = "Cts"
        Me.OutTime.Name = "OutTime"
        Me.OutTime.ReadOnly = True
        '
        'Company
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Company.DefaultCellStyle = DataGridViewCellStyle3
        Me.Company.HeaderText = "Price"
        Me.Company.Name = "Company"
        Me.Company.ReadOnly = True
        '
        'InvCts
        '
        Me.InvCts.HeaderText = "Select"
        Me.InvCts.Name = "InvCts"
        Me.InvCts.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.InvCts.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.InvCts.Width = 80
        '
        'Column1
        '
        Me.Column1.HeaderText = "Size Range"
        Me.Column1.Name = "Column1"
        '
        'Column2
        '
        Me.Column2.HeaderText = "List Price"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.HeaderText = "List Value"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.HeaderText = "Dia Coat"
        Me.Column4.Name = "Column4"
        '
        'Column6
        '
        Me.Column6.HeaderText = "Dia Lab Cost"
        Me.Column6.Name = "Column6"
        '
        'Column5
        '
        Me.Column5.HeaderText = "Labour"
        Me.Column5.Name = "Column5"
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExcel)
        Me.HazelDev_Panel1.Controls.Add(Me.optKit)
        Me.HazelDev_Panel1.Controls.Add(Me.optMix)
        Me.HazelDev_Panel1.Controls.Add(Me.optPolishOK)
        Me.HazelDev_Panel1.Controls.Add(Me.optPolish)
        Me.HazelDev_Panel1.Controls.Add(Me.optApcu)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(930, 49)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
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
        Me.cmdExcel.Location = New System.Drawing.Point(817, 13)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(99, 28)
        Me.cmdExcel.TabIndex = 111
        Me.cmdExcel.Text = "Excel"
        Me.cmdExcel.UseVisualStyleBackColor = False
        '
        'optKit
        '
        Me.optKit.AutoSize = True
        Me.optKit.Enabled = False
        Me.optKit.Location = New System.Drawing.Point(666, 18)
        Me.optKit.Name = "optKit"
        Me.optKit.Size = New System.Drawing.Size(68, 18)
        Me.optKit.TabIndex = 50
        Me.optKit.Text = "KIT Box"
        Me.optKit.UseVisualStyleBackColor = True
        '
        'optMix
        '
        Me.optMix.AutoSize = True
        Me.optMix.Location = New System.Drawing.Point(618, 18)
        Me.optMix.Name = "optMix"
        Me.optMix.Size = New System.Drawing.Size(42, 18)
        Me.optMix.TabIndex = 49
        Me.optMix.Text = "Mix"
        Me.optMix.UseVisualStyleBackColor = True
        '
        'optPolishOK
        '
        Me.optPolishOK.AutoSize = True
        Me.optPolishOK.Checked = True
        Me.optPolishOK.Location = New System.Drawing.Point(533, 18)
        Me.optPolishOK.Name = "optPolishOK"
        Me.optPolishOK.Size = New System.Drawing.Size(79, 18)
        Me.optPolishOK.TabIndex = 48
        Me.optPolishOK.TabStop = True
        Me.optPolishOK.Text = "Polish Box"
        Me.optPolishOK.UseVisualStyleBackColor = True
        '
        'optPolish
        '
        Me.optPolish.AutoSize = True
        Me.optPolish.Enabled = False
        Me.optPolish.Location = New System.Drawing.Point(412, 18)
        Me.optPolish.Name = "optPolish"
        Me.optPolish.Size = New System.Drawing.Size(115, 18)
        Me.optPolish.TabIndex = 47
        Me.optPolish.Text = "Temp Polish Box"
        Me.optPolish.UseVisualStyleBackColor = True
        '
        'optApcu
        '
        Me.optApcu.AutoSize = True
        Me.optApcu.Enabled = False
        Me.optApcu.Location = New System.Drawing.Point(327, 18)
        Me.optApcu.Name = "optApcu"
        Me.optApcu.Size = New System.Drawing.Size(79, 18)
        Me.optApcu.TabIndex = 46
        Me.optApcu.Text = "APCU Box"
        Me.optApcu.UseVisualStyleBackColor = True
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
        Me.cmdSave.Location = New System.Drawing.Point(214, 13)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(99, 28)
        Me.cmdSave.TabIndex = 45
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = False
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
        'txtImportNo
        '
        Me.txtImportNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImportNo.Location = New System.Drawing.Point(112, 109)
        Me.txtImportNo.Name = "txtImportNo"
        Me.txtImportNo.Size = New System.Drawing.Size(90, 21)
        Me.txtImportNo.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(12, 109)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 15)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Import No."
        '
        'frm_DCLDeptTransLot
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(945, 643)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_DCLDeptTransLot"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Department Transfer Lot"
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
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents txtImportNo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtCts As System.Windows.Forms.TextBox
    Friend WithEvents txtPcs As System.Windows.Forms.TextBox
    Friend WithEvents optPolishOK As System.Windows.Forms.RadioButton
    Friend WithEvents optPolish As System.Windows.Forms.RadioButton
    Friend WithEvents optApcu As System.Windows.Forms.RadioButton
    Friend WithEvents chkSelect As DiaStock.HazelDev_CheckBox
    Friend WithEvents optMix As System.Windows.Forms.RadioButton
    Friend WithEvents cmbOrgAssort As System.Windows.Forms.ComboBox
    Friend WithEvents txtCompCode As System.Windows.Forms.TextBox
    Friend WithEvents txtLotNo As System.Windows.Forms.TextBox
    Friend WithEvents txtTotCts As System.Windows.Forms.TextBox
    Friend WithEvents txtTotPcs As System.Windows.Forms.TextBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cmdLoad As DiaStock.HazelDev_Button
    Friend WithEvents cmdOpen As DiaStock.HazelDev_Button
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
    Friend WithEvents chkOriginal As DiaStock.HazelDev_CheckBox
    Friend WithEvents txtOrigin As System.Windows.Forms.TextBox
    Friend WithEvents chkInternal As DiaStock.HazelDev_CheckBox
    Friend WithEvents chkSpecial As DiaStock.HazelDev_CheckBox
    Friend WithEvents optKit As System.Windows.Forms.RadioButton
    Friend WithEvents txtAnaCost As System.Windows.Forms.TextBox
    Friend WithEvents txtListCost As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents txtDiaLabCost As System.Windows.Forms.TextBox
    Friend WithEvents txtDiaCost As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtLabour As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Assortment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DepartmentName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OutTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Company As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InvCts As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
