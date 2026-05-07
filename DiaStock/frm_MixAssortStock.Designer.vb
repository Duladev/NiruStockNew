<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_MixAssortStock
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
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.chkBulkPolish = New DiaStock.HazelDev_CheckBox()
        Me.cmdLoadPolish = New DiaStock.HazelDev_Button()
        Me.chkSpecial = New DiaStock.HazelDev_CheckBox()
        Me.chkBulk = New DiaStock.HazelDev_CheckBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtBaseVal = New System.Windows.Forms.TextBox()
        Me.cmdCalculate = New DiaStock.HazelDev_Button()
        Me.optPolish = New System.Windows.Forms.RadioButton()
        Me.optRough = New System.Windows.Forms.RadioButton()
        Me.txtOldParNo = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmdExcel = New DiaStock.HazelDev_Button()
        Me.txtAddVal = New System.Windows.Forms.TextBox()
        Me.txtInvValue = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtItemCost = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtAssortment = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtTotCts = New System.Windows.Forms.TextBox()
        Me.txtTotPcs = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbOrgAssort = New System.Windows.Forms.ComboBox()
        Me.txtCts = New System.Windows.Forms.TextBox()
        Me.txtPcs = New System.Windows.Forms.TextBox()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdLoad = New DiaStock.HazelDev_Button()
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.cmdOpen = New DiaStock.HazelDev_Button()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.txtImportNo = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Assortment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OutTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Company = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BaseValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InvPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AdjValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AdjPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkBulkPolish)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdLoadPolish)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkSpecial)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkBulk)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtRemarks)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtBaseVal)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdCalculate)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.optPolish)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.optRough)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtOldParNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label8)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdExcel)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtAddVal)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtInvValue)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label3)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtItemCost)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtAssortment)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label4)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label7)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label6)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbOrgAssort)
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
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(945, 634)
        Me.HazelDev_ThemeContainer1.TabIndex = 3
        Me.HazelDev_ThemeContainer1.Text = "MIX ASSORTMENT STOCK"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'chkBulkPolish
        '
        Me.chkBulkPolish.Checked = False
        Me.chkBulkPolish.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkBulkPolish.Location = New System.Drawing.Point(534, 169)
        Me.chkBulkPolish.Name = "chkBulkPolish"
        Me.chkBulkPolish.Size = New System.Drawing.Size(90, 17)
        Me.chkBulkPolish.TabIndex = 122
        Me.chkBulkPolish.Text = "Bulk Polish"
        Me.chkBulkPolish.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmdLoadPolish
        '
        Me.cmdLoadPolish.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdLoadPolish.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdLoadPolish.FlatAppearance.BorderSize = 0
        Me.cmdLoadPolish.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdLoadPolish.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdLoadPolish.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdLoadPolish.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLoadPolish.ForeColor = System.Drawing.Color.White
        Me.cmdLoadPolish.Location = New System.Drawing.Point(826, 109)
        Me.cmdLoadPolish.Name = "cmdLoadPolish"
        Me.cmdLoadPolish.Size = New System.Drawing.Size(99, 28)
        Me.cmdLoadPolish.TabIndex = 121
        Me.cmdLoadPolish.Text = "Load Polish"
        Me.cmdLoadPolish.UseVisualStyleBackColor = False
        '
        'chkSpecial
        '
        Me.chkSpecial.Checked = False
        Me.chkSpecial.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkSpecial.Location = New System.Drawing.Point(642, 169)
        Me.chkSpecial.Name = "chkSpecial"
        Me.chkSpecial.Size = New System.Drawing.Size(72, 17)
        Me.chkSpecial.TabIndex = 120
        Me.chkSpecial.Text = "Special"
        Me.chkSpecial.TransparencyKey = System.Drawing.Color.Empty
        '
        'chkBulk
        '
        Me.chkBulk.Checked = False
        Me.chkBulk.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkBulk.Location = New System.Drawing.Point(456, 169)
        Me.chkBulk.Name = "chkBulk"
        Me.chkBulk.Size = New System.Drawing.Size(72, 17)
        Me.chkBulk.TabIndex = 119
        Me.chkBulk.Text = "Bulk"
        Me.chkBulk.TransparencyKey = System.Drawing.Color.Empty
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(217, 165)
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ReadOnly = True
        Me.txtRemarks.Size = New System.Drawing.Size(233, 21)
        Me.txtRemarks.TabIndex = 118
        '
        'txtBaseVal
        '
        Me.txtBaseVal.Location = New System.Drawing.Point(522, 603)
        Me.txtBaseVal.Name = "txtBaseVal"
        Me.txtBaseVal.ReadOnly = True
        Me.txtBaseVal.Size = New System.Drawing.Size(102, 21)
        Me.txtBaseVal.TabIndex = 117
        Me.txtBaseVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmdCalculate
        '
        Me.cmdCalculate.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdCalculate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdCalculate.FlatAppearance.BorderSize = 0
        Me.cmdCalculate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdCalculate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCalculate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCalculate.ForeColor = System.Drawing.Color.White
        Me.cmdCalculate.Location = New System.Drawing.Point(417, 600)
        Me.cmdCalculate.Name = "cmdCalculate"
        Me.cmdCalculate.Size = New System.Drawing.Size(99, 28)
        Me.cmdCalculate.TabIndex = 116
        Me.cmdCalculate.Text = "Calculate"
        Me.cmdCalculate.UseVisualStyleBackColor = False
        '
        'optPolish
        '
        Me.optPolish.AutoSize = True
        Me.optPolish.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.optPolish.Location = New System.Drawing.Point(788, 140)
        Me.optPolish.Name = "optPolish"
        Me.optPolish.Size = New System.Drawing.Size(73, 19)
        Me.optPolish.TabIndex = 115
        Me.optPolish.Text = "Polished"
        Me.optPolish.UseVisualStyleBackColor = False
        '
        'optRough
        '
        Me.optRough.AutoSize = True
        Me.optRough.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.optRough.Checked = True
        Me.optRough.Location = New System.Drawing.Point(720, 140)
        Me.optRough.Name = "optRough"
        Me.optRough.Size = New System.Drawing.Size(62, 19)
        Me.optRough.TabIndex = 114
        Me.optRough.TabStop = True
        Me.optRough.Text = "Rough"
        Me.optRough.UseVisualStyleBackColor = False
        '
        'txtOldParNo
        '
        Me.txtOldParNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOldParNo.Location = New System.Drawing.Point(82, 165)
        Me.txtOldParNo.Name = "txtOldParNo"
        Me.txtOldParNo.Size = New System.Drawing.Size(129, 21)
        Me.txtOldParNo.TabIndex = 83
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(12, 165)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(56, 15)
        Me.Label8.TabIndex = 84
        Me.Label8.Text = "Par/Pack"
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
        Me.cmdExcel.Location = New System.Drawing.Point(7, 600)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(99, 28)
        Me.cmdExcel.TabIndex = 82
        Me.cmdExcel.Text = "Excel Export"
        Me.cmdExcel.UseVisualStyleBackColor = False
        '
        'txtAddVal
        '
        Me.txtAddVal.Location = New System.Drawing.Point(703, 603)
        Me.txtAddVal.Name = "txtAddVal"
        Me.txtAddVal.ReadOnly = True
        Me.txtAddVal.Size = New System.Drawing.Size(102, 21)
        Me.txtAddVal.TabIndex = 81
        Me.txtAddVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtInvValue
        '
        Me.txtInvValue.Location = New System.Drawing.Point(614, 138)
        Me.txtInvValue.Name = "txtInvValue"
        Me.txtInvValue.ReadOnly = True
        Me.txtInvValue.Size = New System.Drawing.Size(100, 21)
        Me.txtInvValue.TabIndex = 80
        Me.txtInvValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(611, 120)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 15)
        Me.Label3.TabIndex = 79
        Me.Label3.Text = "Value"
        '
        'txtItemCost
        '
        Me.txtItemCost.Location = New System.Drawing.Point(378, 138)
        Me.txtItemCost.Name = "txtItemCost"
        Me.txtItemCost.ReadOnly = True
        Me.txtItemCost.Size = New System.Drawing.Size(72, 21)
        Me.txtItemCost.TabIndex = 78
        Me.txtItemCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(375, 120)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 15)
        Me.Label2.TabIndex = 77
        Me.Label2.Text = "Price"
        '
        'txtAssortment
        '
        Me.txtAssortment.Location = New System.Drawing.Point(217, 138)
        Me.txtAssortment.Name = "txtAssortment"
        Me.txtAssortment.ReadOnly = True
        Me.txtAssortment.Size = New System.Drawing.Size(155, 21)
        Me.txtAssortment.TabIndex = 75
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(217, 120)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 15)
        Me.Label4.TabIndex = 76
        Me.Label4.Text = "Assortment"
        '
        'txtTotCts
        '
        Me.txtTotCts.Location = New System.Drawing.Point(534, 138)
        Me.txtTotCts.Name = "txtTotCts"
        Me.txtTotCts.ReadOnly = True
        Me.txtTotCts.Size = New System.Drawing.Size(74, 21)
        Me.txtTotCts.TabIndex = 74
        Me.txtTotCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotPcs
        '
        Me.txtTotPcs.Location = New System.Drawing.Point(456, 138)
        Me.txtTotPcs.Name = "txtTotPcs"
        Me.txtTotPcs.ReadOnly = True
        Me.txtTotPcs.Size = New System.Drawing.Size(72, 21)
        Me.txtTotPcs.TabIndex = 73
        Me.txtTotPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(531, 120)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(24, 15)
        Me.Label7.TabIndex = 72
        Me.Label7.Text = "Cts"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(453, 120)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(27, 15)
        Me.Label6.TabIndex = 71
        Me.Label6.Text = "Pcs"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(12, 136)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 15)
        Me.Label1.TabIndex = 70
        Me.Label1.Text = "Parcel No."
        '
        'cmbOrgAssort
        '
        Me.cmbOrgAssort.FormattingEnabled = True
        Me.cmbOrgAssort.IntegralHeight = False
        Me.cmbOrgAssort.Location = New System.Drawing.Point(82, 136)
        Me.cmbOrgAssort.Name = "cmbOrgAssort"
        Me.cmbOrgAssort.Size = New System.Drawing.Size(129, 23)
        Me.cmbOrgAssort.TabIndex = 69
        '
        'txtCts
        '
        Me.txtCts.Location = New System.Drawing.Point(309, 603)
        Me.txtCts.Name = "txtCts"
        Me.txtCts.ReadOnly = True
        Me.txtCts.Size = New System.Drawing.Size(102, 21)
        Me.txtCts.TabIndex = 67
        Me.txtCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPcs
        '
        Me.txtPcs.Location = New System.Drawing.Point(201, 603)
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
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Code, Me.Assortment, Me.InTime, Me.OutTime, Me.Company, Me.BaseValue, Me.InvPrice, Me.AdjValue, Me.AdjPrice})
        Me.flxDetails.Location = New System.Drawing.Point(7, 196)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(932, 401)
        Me.flxDetails.TabIndex = 43
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmdLoad)
        Me.HazelDev_Panel1.Controls.Add(Me.txtFilePath)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdOpen)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(936, 49)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
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
        Me.cmdLoad.Location = New System.Drawing.Point(823, 13)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.Size = New System.Drawing.Size(99, 28)
        Me.cmdLoad.TabIndex = 48
        Me.cmdLoad.Text = "Load File"
        Me.cmdLoad.UseVisualStyleBackColor = False
        '
        'txtFilePath
        '
        Me.txtFilePath.Enabled = False
        Me.txtFilePath.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilePath.Location = New System.Drawing.Point(319, 16)
        Me.txtFilePath.MaxLength = 0
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.ReadOnly = True
        Me.txtFilePath.Size = New System.Drawing.Size(392, 20)
        Me.txtFilePath.TabIndex = 47
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
        Me.cmdOpen.Location = New System.Drawing.Point(720, 13)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(99, 28)
        Me.cmdOpen.TabIndex = 46
        Me.cmdOpen.Text = "Open File"
        Me.cmdOpen.UseVisualStyleBackColor = False
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
        Me.txtImportNo.Location = New System.Drawing.Point(82, 109)
        Me.txtImportNo.Name = "txtImportNo"
        Me.txtImportNo.Size = New System.Drawing.Size(129, 21)
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
        'Code
        '
        Me.Code.HeaderText = "Parcel No."
        Me.Code.Name = "Code"
        Me.Code.ReadOnly = True
        '
        'Assortment
        '
        Me.Assortment.HeaderText = "Assortment"
        Me.Assortment.Name = "Assortment"
        Me.Assortment.ReadOnly = True
        Me.Assortment.Width = 160
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
        Me.Company.HeaderText = "Base Price"
        Me.Company.Name = "Company"
        Me.Company.ReadOnly = True
        '
        'BaseValue
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.BaseValue.DefaultCellStyle = DataGridViewCellStyle4
        Me.BaseValue.HeaderText = "Base Value"
        Me.BaseValue.Name = "BaseValue"
        '
        'InvPrice
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.InvPrice.DefaultCellStyle = DataGridViewCellStyle5
        Me.InvPrice.HeaderText = "Inv Price"
        Me.InvPrice.Name = "InvPrice"
        '
        'AdjValue
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.AdjValue.DefaultCellStyle = DataGridViewCellStyle6
        Me.AdjValue.HeaderText = "Adj Value"
        Me.AdjValue.Name = "AdjValue"
        '
        'AdjPrice
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.AdjPrice.DefaultCellStyle = DataGridViewCellStyle7
        Me.AdjPrice.HeaderText = "Adj Price"
        Me.AdjPrice.Name = "AdjPrice"
        '
        'frm_MixAssortStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(945, 634)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_MixAssortStock"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mix Assortment Stock"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents txtCts As System.Windows.Forms.TextBox
    Friend WithEvents txtPcs As System.Windows.Forms.TextBox
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents txtImportNo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbOrgAssort As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTotCts As System.Windows.Forms.TextBox
    Friend WithEvents txtTotPcs As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtItemCost As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtAssortment As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtInvValue As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
    Friend WithEvents cmdOpen As DiaStock.HazelDev_Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cmdLoad As DiaStock.HazelDev_Button
    Friend WithEvents txtAddVal As System.Windows.Forms.TextBox
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents txtOldParNo As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents optPolish As System.Windows.Forms.RadioButton
    Friend WithEvents optRough As System.Windows.Forms.RadioButton
    Friend WithEvents cmdCalculate As DiaStock.HazelDev_Button
    Friend WithEvents txtBaseVal As System.Windows.Forms.TextBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents chkBulk As DiaStock.HazelDev_CheckBox
    Friend WithEvents chkSpecial As DiaStock.HazelDev_CheckBox
    Friend WithEvents cmdLoadPolish As DiaStock.HazelDev_Button
    Friend WithEvents chkBulkPolish As DiaStock.HazelDev_CheckBox
    Friend WithEvents Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Assortment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OutTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Company As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BaseValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InvPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AdjValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AdjPrice As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
