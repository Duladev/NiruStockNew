<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_RprPlanValue
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
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.pnlDetails2 = New System.Windows.Forms.Panel()
        Me.txtPlanValue = New System.Windows.Forms.TextBox()
        Me.txtNewValue = New System.Windows.Forms.TextBox()
        Me.txtTotPcs = New System.Windows.Forms.TextBox()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DepartmentName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Company = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OutTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InvCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParSize = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Width1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pcs1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cut1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.optEmeAS = New System.Windows.Forms.RadioButton()
        Me.optEmeBS = New System.Windows.Forms.RadioButton()
        Me.optEmeProd = New System.Windows.Forms.RadioButton()
        Me.optRndLB = New System.Windows.Forms.RadioButton()
        Me.HazelDev_Button1 = New DiaStock.HazelDev_Button()
        Me.optPrAS = New System.Windows.Forms.RadioButton()
        Me.optPrBS = New System.Windows.Forms.RadioButton()
        Me.optRndAS = New System.Windows.Forms.RadioButton()
        Me.optRndBS = New System.Windows.Forms.RadioButton()
        Me.optRnd = New System.Windows.Forms.RadioButton()
        Me.optBagAS = New System.Windows.Forms.RadioButton()
        Me.optBagBS = New System.Windows.Forms.RadioButton()
        Me.txtParNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmdExcel = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.optBagProd = New System.Windows.Forms.RadioButton()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        Me.pnlDetails2.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.pnlDetails2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(1279, 708)
        Me.HazelDev_ThemeContainer1.TabIndex = 6
        Me.HazelDev_ThemeContainer1.Text = "ROUGH PROCESS PLAN VALUE"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'pnlDetails2
        '
        Me.pnlDetails2.BackColor = System.Drawing.Color.White
        Me.pnlDetails2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDetails2.Controls.Add(Me.txtPlanValue)
        Me.pnlDetails2.Controls.Add(Me.txtNewValue)
        Me.pnlDetails2.Controls.Add(Me.txtTotPcs)
        Me.pnlDetails2.Controls.Add(Me.flxDetails)
        Me.pnlDetails2.Location = New System.Drawing.Point(3, 110)
        Me.pnlDetails2.Name = "pnlDetails2"
        Me.pnlDetails2.Size = New System.Drawing.Size(1268, 595)
        Me.pnlDetails2.TabIndex = 68
        '
        'txtPlanValue
        '
        Me.txtPlanValue.Location = New System.Drawing.Point(701, 564)
        Me.txtPlanValue.Name = "txtPlanValue"
        Me.txtPlanValue.ReadOnly = True
        Me.txtPlanValue.Size = New System.Drawing.Size(91, 21)
        Me.txtPlanValue.TabIndex = 94
        Me.txtPlanValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtNewValue
        '
        Me.txtNewValue.Location = New System.Drawing.Point(1169, 564)
        Me.txtNewValue.Name = "txtNewValue"
        Me.txtNewValue.ReadOnly = True
        Me.txtNewValue.Size = New System.Drawing.Size(91, 21)
        Me.txtNewValue.TabIndex = 93
        Me.txtNewValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotPcs
        '
        Me.txtTotPcs.Location = New System.Drawing.Point(985, 564)
        Me.txtTotPcs.Name = "txtTotPcs"
        Me.txtTotPcs.ReadOnly = True
        Me.txtTotPcs.Size = New System.Drawing.Size(91, 21)
        Me.txtTotPcs.TabIndex = 92
        Me.txtTotPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Code, Me.DepartmentName, Me.Company, Me.InTime, Me.OutTime, Me.InvCts, Me.ParSize, Me.Width1, Me.Pcs1, Me.Cut1, Me.Column3, Me.Column4})
        Me.flxDetails.Location = New System.Drawing.Point(8, 12)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(1252, 546)
        Me.flxDetails.TabIndex = 45
        '
        'Column1
        '
        Me.Column1.HeaderText = "Par No"
        Me.Column1.Name = "Column1"
        '
        'Column2
        '
        Me.Column2.HeaderText = "Pkt No"
        Me.Column2.Name = "Column2"
        '
        'Code
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Code.DefaultCellStyle = DataGridViewCellStyle1
        Me.Code.HeaderText = "Rgh Cts"
        Me.Code.Name = "Code"
        '
        'DepartmentName
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DepartmentName.DefaultCellStyle = DataGridViewCellStyle2
        Me.DepartmentName.HeaderText = "Fin Cts"
        Me.DepartmentName.Name = "DepartmentName"
        Me.DepartmentName.Width = 80
        '
        'Company
        '
        Me.Company.HeaderText = "Shape"
        Me.Company.Name = "Company"
        '
        'InTime
        '
        DataGridViewCellStyle3.NullValue = Nothing
        Me.InTime.DefaultCellStyle = DataGridViewCellStyle3
        Me.InTime.HeaderText = "Color"
        Me.InTime.Name = "InTime"
        '
        'OutTime
        '
        DataGridViewCellStyle4.NullValue = Nothing
        Me.OutTime.DefaultCellStyle = DataGridViewCellStyle4
        Me.OutTime.HeaderText = "Clarity"
        Me.OutTime.Name = "OutTime"
        '
        'InvCts
        '
        Me.InvCts.HeaderText = "Plan Value"
        Me.InvCts.Name = "InvCts"
        '
        'ParSize
        '
        Me.ParSize.HeaderText = "Size"
        Me.ParSize.Name = "ParSize"
        '
        'Width1
        '
        Me.Width1.HeaderText = "Width"
        Me.Width1.Name = "Width1"
        '
        'Pcs1
        '
        Me.Pcs1.HeaderText = "Pcs"
        Me.Pcs1.Name = "Pcs1"
        Me.Pcs1.Width = 80
        '
        'Cut1
        '
        Me.Cut1.HeaderText = "Cut"
        Me.Cut1.Name = "Cut1"
        '
        'Column3
        '
        Me.Column3.HeaderText = "New Value"
        Me.Column3.Name = "Column3"
        '
        'Column4
        '
        Me.Column4.HeaderText = "Code"
        Me.Column4.Name = "Column4"
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.optBagProd)
        Me.HazelDev_Panel1.Controls.Add(Me.optEmeAS)
        Me.HazelDev_Panel1.Controls.Add(Me.optEmeBS)
        Me.HazelDev_Panel1.Controls.Add(Me.optEmeProd)
        Me.HazelDev_Panel1.Controls.Add(Me.optRndLB)
        Me.HazelDev_Panel1.Controls.Add(Me.HazelDev_Button1)
        Me.HazelDev_Panel1.Controls.Add(Me.optPrAS)
        Me.HazelDev_Panel1.Controls.Add(Me.optPrBS)
        Me.HazelDev_Panel1.Controls.Add(Me.optRndAS)
        Me.HazelDev_Panel1.Controls.Add(Me.optRndBS)
        Me.HazelDev_Panel1.Controls.Add(Me.optRnd)
        Me.HazelDev_Panel1.Controls.Add(Me.optBagAS)
        Me.HazelDev_Panel1.Controls.Add(Me.optBagBS)
        Me.HazelDev_Panel1.Controls.Add(Me.txtParNo)
        Me.HazelDev_Panel1.Controls.Add(Me.Label4)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExcel)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(1268, 50)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'optEmeAS
        '
        Me.optEmeAS.AutoSize = True
        Me.optEmeAS.BackColor = System.Drawing.SystemColors.Window
        Me.optEmeAS.Location = New System.Drawing.Point(857, 27)
        Me.optEmeAS.Name = "optEmeAS"
        Me.optEmeAS.Size = New System.Drawing.Size(87, 18)
        Me.optEmeAS.TabIndex = 111
        Me.optEmeAS.Text = "Emerald AS"
        Me.optEmeAS.UseVisualStyleBackColor = False
        '
        'optEmeBS
        '
        Me.optEmeBS.AutoSize = True
        Me.optEmeBS.BackColor = System.Drawing.SystemColors.Window
        Me.optEmeBS.Location = New System.Drawing.Point(857, 3)
        Me.optEmeBS.Name = "optEmeBS"
        Me.optEmeBS.Size = New System.Drawing.Size(86, 18)
        Me.optEmeBS.TabIndex = 110
        Me.optEmeBS.Text = "Emerald BS"
        Me.optEmeBS.UseVisualStyleBackColor = False
        '
        'optEmeProd
        '
        Me.optEmeProd.AutoSize = True
        Me.optEmeProd.BackColor = System.Drawing.SystemColors.Window
        Me.optEmeProd.Location = New System.Drawing.Point(949, 3)
        Me.optEmeProd.Name = "optEmeProd"
        Me.optEmeProd.Size = New System.Drawing.Size(97, 18)
        Me.optEmeProd.TabIndex = 109
        Me.optEmeProd.Text = "Emerald Prod"
        Me.optEmeProd.UseVisualStyleBackColor = False
        '
        'optRndLB
        '
        Me.optRndLB.AutoSize = True
        Me.optRndLB.BackColor = System.Drawing.SystemColors.Window
        Me.optRndLB.Location = New System.Drawing.Point(759, 27)
        Me.optRndLB.Name = "optRndLB"
        Me.optRndLB.Size = New System.Drawing.Size(82, 18)
        Me.optRndLB.TabIndex = 108
        Me.optRndLB.Text = "Rounds LB"
        Me.optRndLB.UseVisualStyleBackColor = False
        '
        'HazelDev_Button1
        '
        Me.HazelDev_Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.HazelDev_Button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.HazelDev_Button1.FlatAppearance.BorderSize = 0
        Me.HazelDev_Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.HazelDev_Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.HazelDev_Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.HazelDev_Button1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HazelDev_Button1.ForeColor = System.Drawing.Color.White
        Me.HazelDev_Button1.Location = New System.Drawing.Point(1110, 13)
        Me.HazelDev_Button1.Name = "HazelDev_Button1"
        Me.HazelDev_Button1.Size = New System.Drawing.Size(46, 28)
        Me.HazelDev_Button1.TabIndex = 107
        Me.HazelDev_Button1.Text = "Run"
        Me.HazelDev_Button1.UseVisualStyleBackColor = False
        Me.HazelDev_Button1.Visible = False
        '
        'optPrAS
        '
        Me.optPrAS.AutoSize = True
        Me.optPrAS.BackColor = System.Drawing.SystemColors.Window
        Me.optPrAS.Location = New System.Drawing.Point(541, 29)
        Me.optPrAS.Name = "optPrAS"
        Me.optPrAS.Size = New System.Drawing.Size(87, 18)
        Me.optPrAS.TabIndex = 106
        Me.optPrAS.Text = "Princess AS"
        Me.optPrAS.UseVisualStyleBackColor = False
        '
        'optPrBS
        '
        Me.optPrBS.AutoSize = True
        Me.optPrBS.BackColor = System.Drawing.SystemColors.Window
        Me.optPrBS.Location = New System.Drawing.Point(541, 3)
        Me.optPrBS.Name = "optPrBS"
        Me.optPrBS.Size = New System.Drawing.Size(86, 18)
        Me.optPrBS.TabIndex = 105
        Me.optPrBS.Text = "Princess BS"
        Me.optPrBS.UseVisualStyleBackColor = False
        '
        'optRndAS
        '
        Me.optRndAS.AutoSize = True
        Me.optRndAS.BackColor = System.Drawing.SystemColors.Window
        Me.optRndAS.Location = New System.Drawing.Point(651, 27)
        Me.optRndAS.Name = "optRndAS"
        Me.optRndAS.Size = New System.Drawing.Size(84, 18)
        Me.optRndAS.TabIndex = 104
        Me.optRndAS.Text = "Rounds AS"
        Me.optRndAS.UseVisualStyleBackColor = False
        '
        'optRndBS
        '
        Me.optRndBS.AutoSize = True
        Me.optRndBS.BackColor = System.Drawing.SystemColors.Window
        Me.optRndBS.Location = New System.Drawing.Point(651, 3)
        Me.optRndBS.Name = "optRndBS"
        Me.optRndBS.Size = New System.Drawing.Size(83, 18)
        Me.optRndBS.TabIndex = 103
        Me.optRndBS.Text = "Rounds BS"
        Me.optRndBS.UseVisualStyleBackColor = False
        '
        'optRnd
        '
        Me.optRnd.AutoSize = True
        Me.optRnd.BackColor = System.Drawing.SystemColors.Window
        Me.optRnd.Location = New System.Drawing.Point(759, 3)
        Me.optRnd.Name = "optRnd"
        Me.optRnd.Size = New System.Drawing.Size(94, 18)
        Me.optRnd.TabIndex = 102
        Me.optRnd.Text = "Rounds Prod"
        Me.optRnd.UseVisualStyleBackColor = False
        '
        'optBagAS
        '
        Me.optBagAS.AutoSize = True
        Me.optBagAS.BackColor = System.Drawing.SystemColors.Window
        Me.optBagAS.Location = New System.Drawing.Point(407, 27)
        Me.optBagAS.Name = "optBagAS"
        Me.optBagAS.Size = New System.Drawing.Size(100, 18)
        Me.optBagAS.TabIndex = 101
        Me.optBagAS.Text = "Baguettes AS"
        Me.optBagAS.UseVisualStyleBackColor = False
        '
        'optBagBS
        '
        Me.optBagBS.AutoSize = True
        Me.optBagBS.BackColor = System.Drawing.SystemColors.Window
        Me.optBagBS.Checked = True
        Me.optBagBS.Location = New System.Drawing.Point(407, 3)
        Me.optBagBS.Name = "optBagBS"
        Me.optBagBS.Size = New System.Drawing.Size(99, 18)
        Me.optBagBS.TabIndex = 100
        Me.optBagBS.Text = "Baguettes BS"
        Me.optBagBS.UseVisualStyleBackColor = False
        '
        'txtParNo
        '
        Me.txtParNo.Location = New System.Drawing.Point(300, 17)
        Me.txtParNo.Name = "txtParNo"
        Me.txtParNo.Size = New System.Drawing.Size(84, 22)
        Me.txtParNo.TabIndex = 47
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(226, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 14)
        Me.Label4.TabIndex = 48
        Me.Label4.Text = "Parcel No"
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
        Me.cmdExcel.Location = New System.Drawing.Point(1162, 13)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(99, 28)
        Me.cmdExcel.TabIndex = 46
        Me.cmdExcel.Text = "Excel"
        Me.cmdExcel.UseVisualStyleBackColor = False
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
        'optBagProd
        '
        Me.optBagProd.AutoSize = True
        Me.optBagProd.BackColor = System.Drawing.SystemColors.Window
        Me.optBagProd.Location = New System.Drawing.Point(950, 27)
        Me.optBagProd.Name = "optBagProd"
        Me.optBagProd.Size = New System.Drawing.Size(110, 18)
        Me.optBagProd.TabIndex = 112
        Me.optBagProd.Text = "Baguettes Prod"
        Me.optBagProd.UseVisualStyleBackColor = False
        '
        'frm_RprPlanValue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1279, 708)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_RprPlanValue"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Rpr Plan Value"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.pnlDetails2.ResumeLayout(False)
        Me.pnlDetails2.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents pnlDetails2 As System.Windows.Forms.Panel
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents txtParNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents optRnd As System.Windows.Forms.RadioButton
    Friend WithEvents optBagAS As System.Windows.Forms.RadioButton
    Friend WithEvents optBagBS As System.Windows.Forms.RadioButton
    Friend WithEvents optRndBS As System.Windows.Forms.RadioButton
    Friend WithEvents optRndAS As System.Windows.Forms.RadioButton
    Friend WithEvents optPrBS As System.Windows.Forms.RadioButton
    Friend WithEvents optPrAS As System.Windows.Forms.RadioButton
    Friend WithEvents HazelDev_Button1 As DiaStock.HazelDev_Button
    Friend WithEvents txtPlanValue As System.Windows.Forms.TextBox
    Friend WithEvents txtNewValue As System.Windows.Forms.TextBox
    Friend WithEvents txtTotPcs As System.Windows.Forms.TextBox
    Friend WithEvents optRndLB As System.Windows.Forms.RadioButton
    Friend WithEvents optEmeProd As System.Windows.Forms.RadioButton
    Friend WithEvents optEmeBS As System.Windows.Forms.RadioButton
    Friend WithEvents optEmeAS As System.Windows.Forms.RadioButton
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DepartmentName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Company As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OutTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InvCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParSize As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Width1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pcs1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cut1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents optBagProd As System.Windows.Forms.RadioButton
End Class
