<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DCLRejectDetails
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
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmbDept = New System.Windows.Forms.ComboBox()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.PictureBox1 = New System.Windows.Forms.Panel()
        Me.cmdAdd = New DiaStock.HazelDev_Button()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtValue = New System.Windows.Forms.TextBox()
        Me.cmbReason = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtIssTime = New System.Windows.Forms.TextBox()
        Me.txtIssDate = New System.Windows.Forms.TextBox()
        Me.txtCts = New System.Windows.Forms.TextBox()
        Me.txtPcs = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtEmp = New System.Windows.Forms.TextBox()
        Me.cmdEmp = New DiaStock.HazelDev_Button()
        Me.txtParPkt = New System.Windows.Forms.TextBox()
        Me.cmdParPkt = New DiaStock.HazelDev_Button()
        Me.cmbSection = New System.Windows.Forms.ComboBox()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdDelete = New DiaStock.HazelDev_Button()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.PictureBox1.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(633, 461)
        Me.HazelDev_ThemeContainer1.TabIndex = 6
        Me.HazelDev_ThemeContainer1.Text = "REJECT DETAILS"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmdDelete)
        Me.HazelDev_Panel1.Controls.Add(Me.Label16)
        Me.HazelDev_Panel1.Controls.Add(Me.cmbDept)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.Label25)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.PictureBox1)
        Me.HazelDev_Panel1.Controls.Add(Me.txtEmp)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdEmp)
        Me.HazelDev_Panel1.Controls.Add(Me.txtParPkt)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdParPkt)
        Me.HazelDev_Panel1.Controls.Add(Me.cmbSection)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(4, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(622, 402)
        Me.HazelDev_Panel1.TabIndex = 0
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label16.Location = New System.Drawing.Point(8, 6)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(81, 14)
        Me.Label16.TabIndex = 84
        Me.Label16.Text = "Department"
        '
        'cmbDept
        '
        Me.cmbDept.FormattingEnabled = True
        Me.cmbDept.IntegralHeight = False
        Me.cmbDept.Items.AddRange(New Object() {"Rough", "Polished"})
        Me.cmbDept.Location = New System.Drawing.Point(12, 23)
        Me.cmbDept.Name = "cmbDept"
        Me.cmbDept.Size = New System.Drawing.Size(152, 22)
        Me.cmbDept.TabIndex = 83
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
        Me.cmdExit.Location = New System.Drawing.Point(8, 364)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(99, 28)
        Me.cmdExit.TabIndex = 82
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Location = New System.Drawing.Point(457, 6)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(48, 14)
        Me.Label25.TabIndex = 78
        Me.Label25.Text = "Section"
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
        Me.cmdNew.Location = New System.Drawing.Point(113, 364)
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
        Me.cmdSave.Location = New System.Drawing.Point(218, 364)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(99, 28)
        Me.cmdSave.TabIndex = 75
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Controls.Add(Me.cmdAdd)
        Me.PictureBox1.Controls.Add(Me.flxDetails)
        Me.PictureBox1.Controls.Add(Me.Label23)
        Me.PictureBox1.Controls.Add(Me.txtValue)
        Me.PictureBox1.Controls.Add(Me.cmbReason)
        Me.PictureBox1.Controls.Add(Me.Label20)
        Me.PictureBox1.Controls.Add(Me.Label21)
        Me.PictureBox1.Controls.Add(Me.txtIssTime)
        Me.PictureBox1.Controls.Add(Me.txtIssDate)
        Me.PictureBox1.Controls.Add(Me.txtCts)
        Me.PictureBox1.Controls.Add(Me.txtPcs)
        Me.PictureBox1.Controls.Add(Me.Label5)
        Me.PictureBox1.Controls.Add(Me.Label6)
        Me.PictureBox1.Controls.Add(Me.Label4)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 86)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(603, 272)
        Me.PictureBox1.TabIndex = 65
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdAdd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdAdd.FlatAppearance.BorderSize = 0
        Me.cmdAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAdd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.ForeColor = System.Drawing.Color.White
        Me.cmdAdd.Location = New System.Drawing.Point(258, 14)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(99, 28)
        Me.cmdAdd.TabIndex = 165
        Me.cmdAdd.Text = "Add"
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column7})
        Me.flxDetails.Location = New System.Drawing.Point(9, 76)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.ReadOnly = True
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(588, 182)
        Me.flxDetails.TabIndex = 164
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(140, 31)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(37, 14)
        Me.Label23.TabIndex = 146
        Me.Label23.Text = "Value"
        '
        'txtValue
        '
        Me.txtValue.Location = New System.Drawing.Point(143, 48)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.Size = New System.Drawing.Size(61, 22)
        Me.txtValue.TabIndex = 144
        Me.txtValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbReason
        '
        Me.cmbReason.FormattingEnabled = True
        Me.cmbReason.IntegralHeight = False
        Me.cmbReason.Location = New System.Drawing.Point(210, 48)
        Me.cmbReason.Name = "cmbReason"
        Me.cmbReason.Size = New System.Drawing.Size(152, 22)
        Me.cmbReason.TabIndex = 143
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(487, 11)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(34, 14)
        Me.Label20.TabIndex = 97
        Me.Label20.Text = "Time"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(374, 11)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(33, 14)
        Me.Label21.TabIndex = 96
        Me.Label21.Text = "Date"
        '
        'txtIssTime
        '
        Me.txtIssTime.Location = New System.Drawing.Point(490, 28)
        Me.txtIssTime.Name = "txtIssTime"
        Me.txtIssTime.ReadOnly = True
        Me.txtIssTime.Size = New System.Drawing.Size(107, 22)
        Me.txtIssTime.TabIndex = 71
        '
        'txtIssDate
        '
        Me.txtIssDate.Location = New System.Drawing.Point(378, 28)
        Me.txtIssDate.Name = "txtIssDate"
        Me.txtIssDate.ReadOnly = True
        Me.txtIssDate.Size = New System.Drawing.Size(108, 22)
        Me.txtIssDate.TabIndex = 70
        '
        'txtCts
        '
        Me.txtCts.Location = New System.Drawing.Point(76, 48)
        Me.txtCts.Name = "txtCts"
        Me.txtCts.Size = New System.Drawing.Size(61, 22)
        Me.txtCts.TabIndex = 69
        Me.txtCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPcs
        '
        Me.txtPcs.Location = New System.Drawing.Point(9, 48)
        Me.txtPcs.Name = "txtPcs"
        Me.txtPcs.Size = New System.Drawing.Size(61, 22)
        Me.txtPcs.TabIndex = 68
        Me.txtPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(78, 32)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(24, 14)
        Me.Label5.TabIndex = 67
        Me.Label5.Text = "Cts"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 32)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(25, 14)
        Me.Label6.TabIndex = 66
        Me.Label6.Text = "Pcs"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(6, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 14)
        Me.Label4.TabIndex = 65
        Me.Label4.Text = "Reject/Broken"
        '
        'txtEmp
        '
        Me.txtEmp.Location = New System.Drawing.Point(330, 52)
        Me.txtEmp.Name = "txtEmp"
        Me.txtEmp.ReadOnly = True
        Me.txtEmp.Size = New System.Drawing.Size(104, 22)
        Me.txtEmp.TabIndex = 36
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
        Me.cmdEmp.Location = New System.Drawing.Point(227, 52)
        Me.cmdEmp.Name = "cmdEmp"
        Me.cmdEmp.Size = New System.Drawing.Size(99, 28)
        Me.cmdEmp.TabIndex = 35
        Me.cmdEmp.Text = "Employee No"
        Me.cmdEmp.UseVisualStyleBackColor = False
        '
        'txtParPkt
        '
        Me.txtParPkt.Location = New System.Drawing.Point(117, 52)
        Me.txtParPkt.Name = "txtParPkt"
        Me.txtParPkt.ReadOnly = True
        Me.txtParPkt.Size = New System.Drawing.Size(104, 22)
        Me.txtParPkt.TabIndex = 34
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
        Me.cmdParPkt.Location = New System.Drawing.Point(12, 52)
        Me.cmdParPkt.Name = "cmdParPkt"
        Me.cmdParPkt.Size = New System.Drawing.Size(99, 28)
        Me.cmdParPkt.TabIndex = 0
        Me.cmdParPkt.Text = "Parcel/Packet"
        Me.cmdParPkt.UseVisualStyleBackColor = False
        '
        'cmbSection
        '
        Me.cmbSection.FormattingEnabled = True
        Me.cmbSection.IntegralHeight = False
        Me.cmbSection.Location = New System.Drawing.Point(459, 24)
        Me.cmbSection.Name = "cmbSection"
        Me.cmbSection.Size = New System.Drawing.Size(152, 22)
        Me.cmbSection.TabIndex = 0
        '
        'Column1
        '
        Me.Column1.HeaderText = "Reason"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "Pcs"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.HeaderText = "Cts"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.HeaderText = "Emp No"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column5
        '
        Me.Column5.HeaderText = "Date"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        '
        'Column6
        '
        Me.Column6.HeaderText = "Value"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'Column7
        '
        Me.Column7.HeaderText = "ID"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
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
        Me.cmdDelete.Location = New System.Drawing.Point(516, 364)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(99, 28)
        Me.cmdDelete.TabIndex = 85
        Me.cmdDelete.Text = "Delete"
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'frm_DCLRejectDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(633, 461)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_DCLRejectDetails"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reject Details"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        Me.PictureBox1.ResumeLayout(False)
        Me.PictureBox1.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cmbDept As System.Windows.Forms.ComboBox
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.Panel
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtValue As System.Windows.Forms.TextBox
    Friend WithEvents cmbReason As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtIssTime As System.Windows.Forms.TextBox
    Friend WithEvents txtIssDate As System.Windows.Forms.TextBox
    Friend WithEvents txtCts As System.Windows.Forms.TextBox
    Friend WithEvents txtPcs As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtEmp As System.Windows.Forms.TextBox
    Friend WithEvents cmdEmp As DiaStock.HazelDev_Button
    Friend WithEvents txtParPkt As System.Windows.Forms.TextBox
    Friend WithEvents cmdParPkt As DiaStock.HazelDev_Button
    Friend WithEvents cmbSection As System.Windows.Forms.ComboBox
    Friend WithEvents cmdAdd As DiaStock.HazelDev_Button
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdDelete As DiaStock.HazelDev_Button
End Class
