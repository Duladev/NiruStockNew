<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_MixApprovalSRW
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
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.txtWid = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtLen = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtPlanDate = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtDueDate = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.chkSelect = New DiaStock.HazelDev_CheckBox()
        Me.txtOrderNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtMaxCost = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmdShow = New DiaStock.HazelDev_Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbRef = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbSubject = New System.Windows.Forms.ComboBox()
        Me.txtStonePrice = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdExcel = New DiaStock.HazelDev_Button()
        Me.txtListPrice = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbAssortment = New System.Windows.Forms.ComboBox()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.Assortment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdSaveAll = New DiaStock.HazelDev_Button()
        Me.cmdDelete = New DiaStock.HazelDev_Button()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtWid)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label11)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtLen)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label10)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPlanDate)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label9)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtDueDate)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label8)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkSelect)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtOrderNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label7)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtMaxCost)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label6)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdShow)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label5)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbRef)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label3)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbSubject)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtStonePrice)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdExcel)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtListPrice)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label4)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbAssortment)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxDetails)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(808, 636)
        Me.HazelDev_ThemeContainer1.TabIndex = 4
        Me.HazelDev_ThemeContainer1.Text = "MIX APPROVAL ROUGH"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'txtWid
        '
        Me.txtWid.Location = New System.Drawing.Point(602, 219)
        Me.txtWid.Name = "txtWid"
        Me.txtWid.ReadOnly = True
        Me.txtWid.Size = New System.Drawing.Size(81, 21)
        Me.txtWid.TabIndex = 136
        Me.txtWid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Location = New System.Drawing.Point(599, 201)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(38, 15)
        Me.Label11.TabIndex = 135
        Me.Label11.Text = "Width"
        '
        'txtLen
        '
        Me.txtLen.Location = New System.Drawing.Point(515, 219)
        Me.txtLen.Name = "txtLen"
        Me.txtLen.ReadOnly = True
        Me.txtLen.Size = New System.Drawing.Size(81, 21)
        Me.txtLen.TabIndex = 134
        Me.txtLen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(512, 201)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(45, 15)
        Me.Label10.TabIndex = 133
        Me.Label10.Text = "Length"
        '
        'txtPlanDate
        '
        Me.txtPlanDate.Location = New System.Drawing.Point(625, 130)
        Me.txtPlanDate.Name = "txtPlanDate"
        Me.txtPlanDate.ReadOnly = True
        Me.txtPlanDate.Size = New System.Drawing.Size(104, 21)
        Me.txtPlanDate.TabIndex = 132
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(622, 112)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(87, 15)
        Me.Label9.TabIndex = 131
        Me.Label9.Text = "Last Plan Date"
        '
        'txtDueDate
        '
        Me.txtDueDate.Location = New System.Drawing.Point(515, 130)
        Me.txtDueDate.Name = "txtDueDate"
        Me.txtDueDate.ReadOnly = True
        Me.txtDueDate.Size = New System.Drawing.Size(104, 21)
        Me.txtDueDate.TabIndex = 130
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(512, 112)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(59, 15)
        Me.Label8.TabIndex = 129
        Me.Label8.Text = "Due Date"
        '
        'chkSelect
        '
        Me.chkSelect.Checked = False
        Me.chkSelect.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkSelect.Location = New System.Drawing.Point(722, 112)
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Size = New System.Drawing.Size(81, 16)
        Me.chkSelect.TabIndex = 128
        Me.chkSelect.Text = "Select All"
        Me.chkSelect.TransparencyKey = System.Drawing.Color.Empty
        '
        'txtOrderNo
        '
        Me.txtOrderNo.Location = New System.Drawing.Point(298, 130)
        Me.txtOrderNo.Name = "txtOrderNo"
        Me.txtOrderNo.Size = New System.Drawing.Size(84, 21)
        Me.txtOrderNo.TabIndex = 126
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(295, 112)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(57, 15)
        Me.Label7.TabIndex = 127
        Me.Label7.Text = "Order No"
        '
        'txtMaxCost
        '
        Me.txtMaxCost.Location = New System.Drawing.Point(515, 176)
        Me.txtMaxCost.Name = "txtMaxCost"
        Me.txtMaxCost.ReadOnly = True
        Me.txtMaxCost.Size = New System.Drawing.Size(81, 21)
        Me.txtMaxCost.TabIndex = 125
        Me.txtMaxCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(512, 158)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(58, 15)
        Me.Label6.TabIndex = 124
        Me.Label6.Text = "Max Cost"
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdShow.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdShow.FlatAppearance.BorderSize = 0
        Me.cmdShow.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdShow.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdShow.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.Color.White
        Me.cmdShow.Location = New System.Drawing.Point(388, 123)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.Size = New System.Drawing.Size(99, 28)
        Me.cmdShow.TabIndex = 123
        Me.cmdShow.Text = "Show All"
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(4, 201)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 15)
        Me.Label5.TabIndex = 122
        Me.Label5.Text = "Reference"
        '
        'cmbRef
        '
        Me.cmbRef.FormattingEnabled = True
        Me.cmbRef.IntegralHeight = False
        Me.cmbRef.Location = New System.Drawing.Point(7, 219)
        Me.cmbRef.Name = "cmbRef"
        Me.cmbRef.Size = New System.Drawing.Size(502, 23)
        Me.cmbRef.TabIndex = 121
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(4, 158)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 15)
        Me.Label3.TabIndex = 120
        Me.Label3.Text = "Subject"
        '
        'cmbSubject
        '
        Me.cmbSubject.FormattingEnabled = True
        Me.cmbSubject.IntegralHeight = False
        Me.cmbSubject.Location = New System.Drawing.Point(7, 176)
        Me.cmbSubject.Name = "cmbSubject"
        Me.cmbSubject.Size = New System.Drawing.Size(502, 23)
        Me.cmbSubject.TabIndex = 119
        '
        'txtStonePrice
        '
        Me.txtStonePrice.Location = New System.Drawing.Point(220, 130)
        Me.txtStonePrice.Name = "txtStonePrice"
        Me.txtStonePrice.ReadOnly = True
        Me.txtStonePrice.Size = New System.Drawing.Size(72, 21)
        Me.txtStonePrice.TabIndex = 118
        Me.txtStonePrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(217, 112)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 15)
        Me.Label1.TabIndex = 117
        Me.Label1.Text = "Stone Price"
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
        'txtListPrice
        '
        Me.txtListPrice.Location = New System.Drawing.Point(142, 130)
        Me.txtListPrice.Name = "txtListPrice"
        Me.txtListPrice.ReadOnly = True
        Me.txtListPrice.Size = New System.Drawing.Size(72, 21)
        Me.txtListPrice.TabIndex = 78
        Me.txtListPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(139, 112)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 15)
        Me.Label2.TabIndex = 77
        Me.Label2.Text = "Avg Price"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(4, 112)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 15)
        Me.Label4.TabIndex = 76
        Me.Label4.Text = "Assortment"
        '
        'cmbAssortment
        '
        Me.cmbAssortment.FormattingEnabled = True
        Me.cmbAssortment.IntegralHeight = False
        Me.cmbAssortment.Location = New System.Drawing.Point(7, 130)
        Me.cmbAssortment.Name = "cmbAssortment"
        Me.cmbAssortment.Size = New System.Drawing.Size(129, 23)
        Me.cmbAssortment.TabIndex = 69
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Assortment, Me.Column1, Me.Column2, Me.Column3, Me.Column4})
        Me.flxDetails.Location = New System.Drawing.Point(7, 246)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(796, 351)
        Me.flxDetails.TabIndex = 43
        '
        'Assortment
        '
        Me.Assortment.HeaderText = "Assortment"
        Me.Assortment.Name = "Assortment"
        Me.Assortment.ReadOnly = True
        '
        'Column1
        '
        Me.Column1.HeaderText = "Subject"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 400
        '
        'Column2
        '
        Me.Column2.HeaderText = "Reference"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column3
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column3.DefaultCellStyle = DataGridViewCellStyle1
        Me.Column3.HeaderText = "Max Cost"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.HeaderText = "Select"
        Me.Column4.Name = "Column4"
        Me.Column4.Width = 80
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSaveAll)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdDelete)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(800, 49)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmdSaveAll
        '
        Me.cmdSaveAll.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSaveAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSaveAll.FlatAppearance.BorderSize = 0
        Me.cmdSaveAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdSaveAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdSaveAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSaveAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSaveAll.ForeColor = System.Drawing.Color.White
        Me.cmdSaveAll.Location = New System.Drawing.Point(319, 13)
        Me.cmdSaveAll.Name = "cmdSaveAll"
        Me.cmdSaveAll.Size = New System.Drawing.Size(99, 28)
        Me.cmdSaveAll.TabIndex = 54
        Me.cmdSaveAll.Text = "Save All"
        Me.cmdSaveAll.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdDelete.FlatAppearance.BorderSize = 0
        Me.cmdDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon
        Me.cmdDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.ForeColor = System.Drawing.Color.White
        Me.cmdDelete.Location = New System.Drawing.Point(687, 13)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(99, 28)
        Me.cmdDelete.TabIndex = 53
        Me.cmdDelete.Text = "Delete"
        Me.cmdDelete.UseVisualStyleBackColor = False
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
        'frm_MixApprovalSRW
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(808, 636)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_MixApprovalSRW"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mix Approval Rough"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents txtListPrice As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbAssortment As System.Windows.Forms.ComboBox
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents txtStonePrice As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbSubject As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbRef As System.Windows.Forms.ComboBox
    Friend WithEvents cmdDelete As DiaStock.HazelDev_Button
    Friend WithEvents cmdShow As DiaStock.HazelDev_Button
    Friend WithEvents txtMaxCost As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmdSaveAll As DiaStock.HazelDev_Button
    Friend WithEvents txtOrderNo As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Assortment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents chkSelect As DiaStock.HazelDev_CheckBox
    Friend WithEvents txtDueDate As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtPlanDate As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtWid As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtLen As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
End Class
