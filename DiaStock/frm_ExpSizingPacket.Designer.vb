<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ExpSizingPacket
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
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtOrigin = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtPktNo2 = New System.Windows.Forms.TextBox()
        Me.chkRough = New DiaStock.HazelDev_CheckBox()
        Me.chkSecond = New DiaStock.HazelDev_CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtBalPcs = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtBalCts = New System.Windows.Forms.TextBox()
        Me.txtTPktPcs = New System.Windows.Forms.TextBox()
        Me.txtTPktCts = New System.Windows.Forms.TextBox()
        Me.txtTotBalPcs = New System.Windows.Forms.TextBox()
        Me.txtTotBalCts = New System.Windows.Forms.TextBox()
        Me.txtTotPcs = New System.Windows.Forms.TextBox()
        Me.txtTotCts = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.PktNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.txtPktNo = New System.Windows.Forms.TextBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.txtParNo = New System.Windows.Forms.TextBox()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmbDept = New System.Windows.Forms.ComboBox()
        Me.Type = New System.Windows.Forms.Label()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbType = New System.Windows.Forms.ComboBox()
        Me.flxPacket = New System.Windows.Forms.DataGridView()
        Me.Reason = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BalPcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BalCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtPktPcs = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtPktCts = New System.Windows.Forms.TextBox()
        Me.txtOCode = New System.Windows.Forms.TextBox()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        Me.HazelDev_Panel1.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxPacket, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(951, 645)
        Me.HazelDev_ThemeContainer1.TabIndex = 6
        Me.HazelDev_ThemeContainer1.Text = "SORTING SIZING PACKET"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.txtOCode)
        Me.HazelDev_Panel1.Controls.Add(Me.Label4)
        Me.HazelDev_Panel1.Controls.Add(Me.txtOrigin)
        Me.HazelDev_Panel1.Controls.Add(Me.Label3)
        Me.HazelDev_Panel1.Controls.Add(Me.txtPktNo2)
        Me.HazelDev_Panel1.Controls.Add(Me.chkRough)
        Me.HazelDev_Panel1.Controls.Add(Me.chkSecond)
        Me.HazelDev_Panel1.Controls.Add(Me.Label7)
        Me.HazelDev_Panel1.Controls.Add(Me.txtBalPcs)
        Me.HazelDev_Panel1.Controls.Add(Me.Label8)
        Me.HazelDev_Panel1.Controls.Add(Me.txtBalCts)
        Me.HazelDev_Panel1.Controls.Add(Me.txtTPktPcs)
        Me.HazelDev_Panel1.Controls.Add(Me.txtTPktCts)
        Me.HazelDev_Panel1.Controls.Add(Me.txtTotBalPcs)
        Me.HazelDev_Panel1.Controls.Add(Me.txtTotBalCts)
        Me.HazelDev_Panel1.Controls.Add(Me.txtTotPcs)
        Me.HazelDev_Panel1.Controls.Add(Me.txtTotCts)
        Me.HazelDev_Panel1.Controls.Add(Me.Label2)
        Me.HazelDev_Panel1.Controls.Add(Me.Label1)
        Me.HazelDev_Panel1.Controls.Add(Me.flxDetails)
        Me.HazelDev_Panel1.Controls.Add(Me.Label42)
        Me.HazelDev_Panel1.Controls.Add(Me.txtPktNo)
        Me.HazelDev_Panel1.Controls.Add(Me.Label41)
        Me.HazelDev_Panel1.Controls.Add(Me.txtParNo)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmbDept)
        Me.HazelDev_Panel1.Controls.Add(Me.Type)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.Label10)
        Me.HazelDev_Panel1.Controls.Add(Me.cmbType)
        Me.HazelDev_Panel1.Controls.Add(Me.flxPacket)
        Me.HazelDev_Panel1.Controls.Add(Me.Label6)
        Me.HazelDev_Panel1.Controls.Add(Me.txtPktPcs)
        Me.HazelDev_Panel1.Controls.Add(Me.Label5)
        Me.HazelDev_Panel1.Controls.Add(Me.txtPktCts)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(4, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(944, 589)
        Me.HazelDev_Panel1.TabIndex = 0
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(329, 6)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(38, 14)
        Me.Label4.TabIndex = 171
        Me.Label4.Text = "Origin"
        '
        'txtOrigin
        '
        Me.txtOrigin.Location = New System.Drawing.Point(332, 24)
        Me.txtOrigin.Name = "txtOrigin"
        Me.txtOrigin.ReadOnly = True
        Me.txtOrigin.Size = New System.Drawing.Size(146, 22)
        Me.txtOrigin.TabIndex = 170
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(828, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 14)
        Me.Label3.TabIndex = 169
        Me.Label3.Text = "Pkt No."
        '
        'txtPktNo2
        '
        Me.txtPktNo2.Location = New System.Drawing.Point(830, 24)
        Me.txtPktNo2.Name = "txtPktNo2"
        Me.txtPktNo2.ReadOnly = True
        Me.txtPktNo2.Size = New System.Drawing.Size(53, 22)
        Me.txtPktNo2.TabIndex = 168
        '
        'chkRough
        '
        Me.chkRough.Checked = False
        Me.chkRough.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkRough.Location = New System.Drawing.Point(273, 66)
        Me.chkRough.Name = "chkRough"
        Me.chkRough.Size = New System.Drawing.Size(64, 19)
        Me.chkRough.TabIndex = 135
        Me.chkRough.Text = "Rough"
        Me.chkRough.TransparencyKey = System.Drawing.Color.Empty
        '
        'chkSecond
        '
        Me.chkSecond.Checked = False
        Me.chkSecond.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkSecond.Location = New System.Drawing.Point(171, 66)
        Me.chkSecond.Name = "chkSecond"
        Me.chkSecond.Size = New System.Drawing.Size(79, 19)
        Me.chkSecond.TabIndex = 167
        Me.chkSecond.Text = "2nd Time"
        Me.chkSecond.TransparencyKey = System.Drawing.Color.Empty
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(696, 50)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(44, 14)
        Me.Label7.TabIndex = 163
        Me.Label7.Text = "Bal Pcs"
        '
        'txtBalPcs
        '
        Me.txtBalPcs.Location = New System.Drawing.Point(696, 67)
        Me.txtBalPcs.Name = "txtBalPcs"
        Me.txtBalPcs.ReadOnly = True
        Me.txtBalPcs.Size = New System.Drawing.Size(61, 22)
        Me.txtBalPcs.TabIndex = 165
        Me.txtBalPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(763, 50)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(43, 14)
        Me.Label8.TabIndex = 164
        Me.Label8.Text = "Bal Cts"
        '
        'txtBalCts
        '
        Me.txtBalCts.Location = New System.Drawing.Point(763, 66)
        Me.txtBalCts.Name = "txtBalCts"
        Me.txtBalCts.ReadOnly = True
        Me.txtBalCts.Size = New System.Drawing.Size(61, 22)
        Me.txtBalCts.TabIndex = 166
        Me.txtBalCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTPktPcs
        '
        Me.txtTPktPcs.Location = New System.Drawing.Point(716, 526)
        Me.txtTPktPcs.Name = "txtTPktPcs"
        Me.txtTPktPcs.ReadOnly = True
        Me.txtTPktPcs.Size = New System.Drawing.Size(77, 22)
        Me.txtTPktPcs.TabIndex = 158
        Me.txtTPktPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTPktCts
        '
        Me.txtTPktCts.Location = New System.Drawing.Point(799, 526)
        Me.txtTPktCts.Name = "txtTPktCts"
        Me.txtTPktCts.ReadOnly = True
        Me.txtTPktCts.Size = New System.Drawing.Size(77, 22)
        Me.txtTPktCts.TabIndex = 157
        Me.txtTPktCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotBalPcs
        '
        Me.txtTotBalPcs.Location = New System.Drawing.Point(273, 526)
        Me.txtTotBalPcs.Name = "txtTotBalPcs"
        Me.txtTotBalPcs.ReadOnly = True
        Me.txtTotBalPcs.Size = New System.Drawing.Size(77, 22)
        Me.txtTotBalPcs.TabIndex = 156
        Me.txtTotBalPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotBalCts
        '
        Me.txtTotBalCts.Location = New System.Drawing.Point(356, 526)
        Me.txtTotBalCts.Name = "txtTotBalCts"
        Me.txtTotBalCts.ReadOnly = True
        Me.txtTotBalCts.Size = New System.Drawing.Size(77, 22)
        Me.txtTotBalCts.TabIndex = 155
        Me.txtTotBalCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotPcs
        '
        Me.txtTotPcs.Location = New System.Drawing.Point(107, 526)
        Me.txtTotPcs.Name = "txtTotPcs"
        Me.txtTotPcs.ReadOnly = True
        Me.txtTotPcs.Size = New System.Drawing.Size(77, 22)
        Me.txtTotPcs.TabIndex = 154
        Me.txtTotPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotCts
        '
        Me.txtTotCts.Location = New System.Drawing.Point(190, 526)
        Me.txtTotCts.Name = "txtTotCts"
        Me.txtTotCts.ReadOnly = True
        Me.txtTotCts.Size = New System.Drawing.Size(77, 22)
        Me.txtTotCts.TabIndex = 153
        Me.txtTotCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(536, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 14)
        Me.Label2.TabIndex = 151
        Me.Label2.Text = "Packeted Pcs"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(8, 78)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 14)
        Me.Label1.TabIndex = 150
        Me.Label1.Text = "Finished Pcs"
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PktNo, Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.Column2})
        Me.flxDetails.Location = New System.Drawing.Point(536, 95)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.ReadOnly = True
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(399, 425)
        Me.flxDetails.TabIndex = 149
        '
        'PktNo
        '
        Me.PktNo.HeaderText = "Pkt No"
        Me.PktNo.Name = "PktNo"
        Me.PktNo.ReadOnly = True
        Me.PktNo.Width = 80
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Clarity"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewTextBoxColumn2.HeaderText = "Pkt Pcs"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 80
        '
        'DataGridViewTextBoxColumn3
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewTextBoxColumn3.HeaderText = "Pkt Cts"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 80
        '
        'Column2
        '
        Me.Column2.HeaderText = "Pkt No 2"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 80
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.BackColor = System.Drawing.Color.Transparent
        Me.Label42.Location = New System.Drawing.Point(271, 6)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(48, 14)
        Me.Label42.TabIndex = 86
        Me.Label42.Text = "Pkt No."
        '
        'txtPktNo
        '
        Me.txtPktNo.Location = New System.Drawing.Point(273, 24)
        Me.txtPktNo.Name = "txtPktNo"
        Me.txtPktNo.ReadOnly = True
        Me.txtPktNo.Size = New System.Drawing.Size(53, 22)
        Me.txtPktNo.TabIndex = 85
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.BackColor = System.Drawing.Color.Transparent
        Me.Label41.Location = New System.Drawing.Point(168, 6)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(62, 14)
        Me.Label41.TabIndex = 84
        Me.Label41.Text = "Parcel No."
        '
        'txtParNo
        '
        Me.txtParNo.Location = New System.Drawing.Point(170, 24)
        Me.txtParNo.Name = "txtParNo"
        Me.txtParNo.Size = New System.Drawing.Size(97, 22)
        Me.txtParNo.TabIndex = 83
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
        Me.cmdExit.Location = New System.Drawing.Point(12, 555)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(99, 28)
        Me.cmdExit.TabIndex = 82
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'cmbDept
        '
        Me.cmbDept.FormattingEnabled = True
        Me.cmbDept.IntegralHeight = False
        Me.cmbDept.Location = New System.Drawing.Point(12, 24)
        Me.cmbDept.Name = "cmbDept"
        Me.cmbDept.Size = New System.Drawing.Size(152, 22)
        Me.cmbDept.TabIndex = 77
        '
        'Type
        '
        Me.Type.AutoSize = True
        Me.Type.Location = New System.Drawing.Point(536, 8)
        Me.Type.Name = "Type"
        Me.Type.Size = New System.Drawing.Size(39, 14)
        Me.Type.TabIndex = 148
        Me.Type.Text = "Clarity"
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
        Me.cmdNew.Location = New System.Drawing.Point(117, 555)
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.Size = New System.Drawing.Size(99, 28)
        Me.cmdNew.TabIndex = 76
        Me.cmdNew.Text = "New"
        Me.cmdNew.UseVisualStyleBackColor = False
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
        Me.cmdSave.Location = New System.Drawing.Point(222, 555)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(99, 28)
        Me.cmdSave.TabIndex = 75
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(9, 6)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(73, 14)
        Me.Label10.TabIndex = 30
        Me.Label10.Text = "Department"
        '
        'cmbType
        '
        Me.cmbType.FormattingEnabled = True
        Me.cmbType.IntegralHeight = False
        Me.cmbType.Location = New System.Drawing.Point(536, 24)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Size = New System.Drawing.Size(154, 22)
        Me.cmbType.TabIndex = 144
        '
        'flxPacket
        '
        Me.flxPacket.AllowUserToAddRows = False
        Me.flxPacket.AllowUserToDeleteRows = False
        Me.flxPacket.AllowUserToResizeColumns = False
        Me.flxPacket.AllowUserToResizeRows = False
        Me.flxPacket.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxPacket.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Reason, Me.Pcs, Me.Cts, Me.BalPcs, Me.BalCts, Me.Column1})
        Me.flxPacket.Location = New System.Drawing.Point(12, 95)
        Me.flxPacket.Name = "flxPacket"
        Me.flxPacket.ReadOnly = True
        Me.flxPacket.RowHeadersVisible = False
        Me.flxPacket.Size = New System.Drawing.Size(518, 425)
        Me.flxPacket.TabIndex = 145
        '
        'Reason
        '
        Me.Reason.HeaderText = "Clarity"
        Me.Reason.Name = "Reason"
        Me.Reason.ReadOnly = True
        '
        'Pcs
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Pcs.DefaultCellStyle = DataGridViewCellStyle3
        Me.Pcs.HeaderText = "Tot Pcs"
        Me.Pcs.Name = "Pcs"
        Me.Pcs.ReadOnly = True
        Me.Pcs.Width = 80
        '
        'Cts
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Cts.DefaultCellStyle = DataGridViewCellStyle4
        Me.Cts.HeaderText = "Tot Cts"
        Me.Cts.Name = "Cts"
        Me.Cts.ReadOnly = True
        Me.Cts.Width = 80
        '
        'BalPcs
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.BalPcs.DefaultCellStyle = DataGridViewCellStyle5
        Me.BalPcs.HeaderText = "Bal Pcs"
        Me.BalPcs.Name = "BalPcs"
        Me.BalPcs.ReadOnly = True
        Me.BalPcs.Width = 80
        '
        'BalCts
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.BalCts.DefaultCellStyle = DataGridViewCellStyle6
        Me.BalCts.HeaderText = "Bal Cts"
        Me.BalCts.Name = "BalCts"
        Me.BalCts.ReadOnly = True
        Me.BalCts.Width = 80
        '
        'Column1
        '
        Me.Column1.HeaderText = "Pkt No 2"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 80
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(696, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(25, 14)
        Me.Label6.TabIndex = 66
        Me.Label6.Text = "Pcs"
        '
        'txtPktPcs
        '
        Me.txtPktPcs.Location = New System.Drawing.Point(696, 25)
        Me.txtPktPcs.Name = "txtPktPcs"
        Me.txtPktPcs.Size = New System.Drawing.Size(61, 22)
        Me.txtPktPcs.TabIndex = 68
        Me.txtPktPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(763, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(24, 14)
        Me.Label5.TabIndex = 67
        Me.Label5.Text = "Cts"
        '
        'txtPktCts
        '
        Me.txtPktCts.Location = New System.Drawing.Point(763, 24)
        Me.txtPktCts.Name = "txtPktCts"
        Me.txtPktCts.Size = New System.Drawing.Size(61, 22)
        Me.txtPktCts.TabIndex = 69
        Me.txtPktCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtOCode
        '
        Me.txtOCode.Location = New System.Drawing.Point(414, 67)
        Me.txtOCode.Name = "txtOCode"
        Me.txtOCode.ReadOnly = True
        Me.txtOCode.Size = New System.Drawing.Size(64, 22)
        Me.txtOCode.TabIndex = 172
        '
        'frm_ExpSizingPacket
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(951, 645)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_ExpSizingPacket"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sorting Sizing Packet"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxPacket, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtBalPcs As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtBalCts As System.Windows.Forms.TextBox
    Friend WithEvents txtTPktPcs As System.Windows.Forms.TextBox
    Friend WithEvents txtTPktCts As System.Windows.Forms.TextBox
    Friend WithEvents txtTotBalPcs As System.Windows.Forms.TextBox
    Friend WithEvents txtTotBalCts As System.Windows.Forms.TextBox
    Friend WithEvents txtTotPcs As System.Windows.Forms.TextBox
    Friend WithEvents txtTotCts As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents txtPktNo As System.Windows.Forms.TextBox
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents txtParNo As System.Windows.Forms.TextBox
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmbDept As System.Windows.Forms.ComboBox
    Friend WithEvents Type As System.Windows.Forms.Label
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbType As System.Windows.Forms.ComboBox
    Friend WithEvents flxPacket As System.Windows.Forms.DataGridView
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtPktPcs As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtPktCts As System.Windows.Forms.TextBox
    Friend WithEvents chkRough As DiaStock.HazelDev_CheckBox
    Friend WithEvents chkSecond As DiaStock.HazelDev_CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPktNo2 As System.Windows.Forms.TextBox
    Friend WithEvents PktNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Reason As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BalPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BalCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtOrigin As System.Windows.Forms.TextBox
    Friend WithEvents txtOCode As System.Windows.Forms.TextBox
End Class
