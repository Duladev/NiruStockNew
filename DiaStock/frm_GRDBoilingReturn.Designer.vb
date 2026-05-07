<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_GRDBoilingReturn
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
        Me.chkTrf = New DiaStock.HazelDev_CheckBox()
        Me.chkSelect = New DiaStock.HazelDev_CheckBox()
        Me.cmdEmp = New DiaStock.HazelDev_Button()
        Me.txtIssCts = New System.Windows.Forms.TextBox()
        Me.txtIssPcs = New System.Windows.Forms.TextBox()
        Me.txtEmp = New System.Windows.Forms.TextBox()
        Me.txtRetCts = New System.Windows.Forms.TextBox()
        Me.txtRetPcs = New System.Windows.Forms.TextBox()
        Me.cmbDept = New System.Windows.Forms.ComboBox()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DepartmentName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PktNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OutTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Company = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InvCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Grp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Assortment = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdLoad = New DiaStock.HazelDev_Button()
        Me.cmdParPkt = New DiaStock.HazelDev_Button()
        Me.cmdDelete = New DiaStock.HazelDev_Button()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCount = New System.Windows.Forms.TextBox()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtCount)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkTrf)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkSelect)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdEmp)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtIssCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtIssPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtEmp)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtRetCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtRetPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbDept)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxDetails)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label6)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(1003, 599)
        Me.HazelDev_ThemeContainer1.TabIndex = 4
        Me.HazelDev_ThemeContainer1.Text = "GRADING BOILING RETURN"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'chkTrf
        '
        Me.chkTrf.Checked = False
        Me.chkTrf.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkTrf.Location = New System.Drawing.Point(498, 109)
        Me.chkTrf.Name = "chkTrf"
        Me.chkTrf.Size = New System.Drawing.Size(84, 16)
        Me.chkTrf.TabIndex = 146
        Me.chkTrf.Text = "Transfer"
        Me.chkTrf.TransparencyKey = System.Drawing.Color.Empty
        '
        'chkSelect
        '
        Me.chkSelect.Checked = False
        Me.chkSelect.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkSelect.Location = New System.Drawing.Point(738, 109)
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Size = New System.Drawing.Size(84, 16)
        Me.chkSelect.TabIndex = 145
        Me.chkSelect.Text = "Select All"
        Me.chkSelect.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmdEmp
        '
        Me.cmdEmp.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdEmp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdEmp.FlatAppearance.BorderSize = 0
        Me.cmdEmp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdEmp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdEmp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdEmp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEmp.ForeColor = System.Drawing.Color.White
        Me.cmdEmp.Location = New System.Drawing.Point(262, 109)
        Me.cmdEmp.Name = "cmdEmp"
        Me.cmdEmp.Size = New System.Drawing.Size(99, 28)
        Me.cmdEmp.TabIndex = 144
        Me.cmdEmp.Text = "Employee No"
        Me.cmdEmp.UseVisualStyleBackColor = False
        '
        'txtIssCts
        '
        Me.txtIssCts.Location = New System.Drawing.Point(380, 570)
        Me.txtIssCts.Name = "txtIssCts"
        Me.txtIssCts.ReadOnly = True
        Me.txtIssCts.Size = New System.Drawing.Size(74, 21)
        Me.txtIssCts.TabIndex = 143
        Me.txtIssCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtIssPcs
        '
        Me.txtIssPcs.Location = New System.Drawing.Point(302, 570)
        Me.txtIssPcs.Name = "txtIssPcs"
        Me.txtIssPcs.ReadOnly = True
        Me.txtIssPcs.Size = New System.Drawing.Size(72, 21)
        Me.txtIssPcs.TabIndex = 142
        Me.txtIssPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtEmp
        '
        Me.txtEmp.Location = New System.Drawing.Point(367, 109)
        Me.txtEmp.Name = "txtEmp"
        Me.txtEmp.ReadOnly = True
        Me.txtEmp.Size = New System.Drawing.Size(109, 21)
        Me.txtEmp.TabIndex = 135
        '
        'txtRetCts
        '
        Me.txtRetCts.Location = New System.Drawing.Point(538, 570)
        Me.txtRetCts.Name = "txtRetCts"
        Me.txtRetCts.ReadOnly = True
        Me.txtRetCts.Size = New System.Drawing.Size(74, 21)
        Me.txtRetCts.TabIndex = 133
        Me.txtRetCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRetPcs
        '
        Me.txtRetPcs.Location = New System.Drawing.Point(460, 570)
        Me.txtRetPcs.Name = "txtRetPcs"
        Me.txtRetPcs.ReadOnly = True
        Me.txtRetPcs.Size = New System.Drawing.Size(72, 21)
        Me.txtRetPcs.TabIndex = 132
        Me.txtRetPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbDept
        '
        Me.cmbDept.FormattingEnabled = True
        Me.cmbDept.IntegralHeight = False
        Me.cmbDept.Location = New System.Drawing.Point(81, 109)
        Me.cmbDept.Name = "cmbDept"
        Me.cmbDept.Size = New System.Drawing.Size(159, 23)
        Me.cmbDept.TabIndex = 50
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.DepartmentName, Me.PktNo, Me.InTime, Me.OutTime, Me.Company, Me.InvCts, Me.Grp, Me.Assortment, Me.Column2, Me.Column3})
        Me.flxDetails.Location = New System.Drawing.Point(7, 150)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(991, 414)
        Me.flxDetails.TabIndex = 43
        '
        'Column1
        '
        Me.Column1.HeaderText = "Department"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'DepartmentName
        '
        Me.DepartmentName.HeaderText = "Par No."
        Me.DepartmentName.Name = "DepartmentName"
        Me.DepartmentName.ReadOnly = True
        '
        'PktNo
        '
        Me.PktNo.HeaderText = "Pkt No"
        Me.PktNo.Name = "PktNo"
        Me.PktNo.ReadOnly = True
        Me.PktNo.Width = 80
        '
        'InTime
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.NullValue = Nothing
        Me.InTime.DefaultCellStyle = DataGridViewCellStyle1
        Me.InTime.HeaderText = "Pcs"
        Me.InTime.Name = "InTime"
        Me.InTime.ReadOnly = True
        Me.InTime.Width = 80
        '
        'OutTime
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.NullValue = Nothing
        Me.OutTime.DefaultCellStyle = DataGridViewCellStyle2
        Me.OutTime.HeaderText = "Cts"
        Me.OutTime.Name = "OutTime"
        Me.OutTime.ReadOnly = True
        Me.OutTime.Width = 80
        '
        'Company
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Company.DefaultCellStyle = DataGridViewCellStyle3
        Me.Company.HeaderText = "Ret Pcs"
        Me.Company.Name = "Company"
        Me.Company.ReadOnly = True
        Me.Company.Width = 80
        '
        'InvCts
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.InvCts.DefaultCellStyle = DataGridViewCellStyle4
        Me.InvCts.HeaderText = "Ret Cts"
        Me.InvCts.Name = "InvCts"
        Me.InvCts.Width = 80
        '
        'Grp
        '
        Me.Grp.HeaderText = "Grp"
        Me.Grp.Name = "Grp"
        Me.Grp.ReadOnly = True
        Me.Grp.Width = 50
        '
        'Assortment
        '
        Me.Assortment.HeaderText = "OK"
        Me.Assortment.Name = "Assortment"
        Me.Assortment.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Assortment.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Column2
        '
        Me.Column2.HeaderText = "Order No"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.HeaderText = "Issue Date"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmdLoad)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdParPkt)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdDelete)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(995, 49)
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
        Me.cmdLoad.Location = New System.Drawing.Point(214, 13)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.Size = New System.Drawing.Size(99, 28)
        Me.cmdLoad.TabIndex = 90
        Me.cmdLoad.Text = "Load Packets"
        Me.cmdLoad.UseVisualStyleBackColor = False
        '
        'cmdParPkt
        '
        Me.cmdParPkt.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdParPkt.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdParPkt.FlatAppearance.BorderSize = 0
        Me.cmdParPkt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdParPkt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdParPkt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdParPkt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdParPkt.ForeColor = System.Drawing.Color.White
        Me.cmdParPkt.Location = New System.Drawing.Point(424, 13)
        Me.cmdParPkt.Name = "cmdParPkt"
        Me.cmdParPkt.Size = New System.Drawing.Size(99, 28)
        Me.cmdParPkt.TabIndex = 89
        Me.cmdParPkt.Text = "Parcel/Packet"
        Me.cmdParPkt.UseVisualStyleBackColor = False
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
        Me.cmdDelete.Location = New System.Drawing.Point(889, 13)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(99, 28)
        Me.cmdDelete.TabIndex = 88
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
        Me.cmdSave.Location = New System.Drawing.Point(319, 13)
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
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(3, 112)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 15)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "Department"
        '
        'txtCount
        '
        Me.txtCount.Location = New System.Drawing.Point(7, 570)
        Me.txtCount.Name = "txtCount"
        Me.txtCount.ReadOnly = True
        Me.txtCount.Size = New System.Drawing.Size(72, 21)
        Me.txtCount.TabIndex = 147
        Me.txtCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'frm_GRDBoilingReturn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1003, 599)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_GRDBoilingReturn"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Grading Boiling Return"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents txtIssCts As System.Windows.Forms.TextBox
    Friend WithEvents txtIssPcs As System.Windows.Forms.TextBox
    Friend WithEvents txtEmp As System.Windows.Forms.TextBox
    Friend WithEvents txtRetCts As System.Windows.Forms.TextBox
    Friend WithEvents txtRetPcs As System.Windows.Forms.TextBox
    Friend WithEvents cmbDept As System.Windows.Forms.ComboBox
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmdEmp As DiaStock.HazelDev_Button
    Friend WithEvents chkSelect As DiaStock.HazelDev_CheckBox
    Friend WithEvents chkTrf As DiaStock.HazelDev_CheckBox
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DepartmentName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PktNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OutTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Company As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InvCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Grp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Assortment As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdDelete As DiaStock.HazelDev_Button
    Friend WithEvents cmdParPkt As DiaStock.HazelDev_Button
    Friend WithEvents cmdLoad As DiaStock.HazelDev_Button
    Friend WithEvents txtCount As System.Windows.Forms.TextBox
End Class
