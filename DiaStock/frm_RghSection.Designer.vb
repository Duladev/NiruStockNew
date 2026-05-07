<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_RghSection
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
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.PictureBox1 = New System.Windows.Forms.Panel()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.flxType = New System.Windows.Forms.DataGridView()
        Me.Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParSize = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LotID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtNoPay = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtRep = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtExt = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtBro = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtLostCts = New System.Windows.Forms.TextBox()
        Me.txtLost = New System.Windows.Forms.TextBox()
        Me.txtRejCts = New System.Windows.Forms.TextBox()
        Me.txtRej = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmdAdd = New DiaStock.HazelDev_Button()
        Me.txtTypeCts = New System.Windows.Forms.TextBox()
        Me.txtTypePcs = New System.Windows.Forms.TextBox()
        Me.txtRetTime = New System.Windows.Forms.TextBox()
        Me.txtRetDate = New System.Windows.Forms.TextBox()
        Me.txtRetCts = New System.Windows.Forms.TextBox()
        Me.txtRetPcs = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbType = New System.Windows.Forms.ComboBox()
        Me.PictureBox2 = New System.Windows.Forms.Panel()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtModel = New System.Windows.Forms.TextBox()
        Me.txtFlo = New System.Windows.Forms.TextBox()
        Me.txtClarity = New System.Windows.Forms.TextBox()
        Me.txtColor = New System.Windows.Forms.TextBox()
        Me.txtBoiling = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtIssPcs = New System.Windows.Forms.TextBox()
        Me.txtIssTime = New System.Windows.Forms.TextBox()
        Me.txtIssDate = New System.Windows.Forms.TextBox()
        Me.txtIssCts = New System.Windows.Forms.TextBox()
        Me.txtEmp = New System.Windows.Forms.TextBox()
        Me.cmdEmp = New DiaStock.HazelDev_Button()
        Me.txtParPkt = New System.Windows.Forms.TextBox()
        Me.cmdParPkt = New DiaStock.HazelDev_Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbSection = New System.Windows.Forms.ComboBox()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.PictureBox1.SuspendLayout()
        CType(Me.flxType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PictureBox2.SuspendLayout()
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
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(622, 568)
        Me.HazelDev_ThemeContainer1.TabIndex = 1
        Me.HazelDev_ThemeContainer1.Text = "ROUGH ISSUE/RETURN"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.PictureBox1)
        Me.HazelDev_Panel1.Controls.Add(Me.PictureBox2)
        Me.HazelDev_Panel1.Controls.Add(Me.txtEmp)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdEmp)
        Me.HazelDev_Panel1.Controls.Add(Me.txtParPkt)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdParPkt)
        Me.HazelDev_Panel1.Controls.Add(Me.Label10)
        Me.HazelDev_Panel1.Controls.Add(Me.cmbSection)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(4, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(615, 511)
        Me.HazelDev_Panel1.TabIndex = 0
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.cmdExit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdExit.FlatAppearance.BorderSize = 0
        Me.cmdExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdExit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.ForeColor = System.Drawing.Color.White
        Me.cmdExit.Location = New System.Drawing.Point(8, 479)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(99, 28)
        Me.cmdExit.TabIndex = 77
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'cmdNew
        '
        Me.cmdNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.cmdNew.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdNew.FlatAppearance.BorderSize = 0
        Me.cmdNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdNew.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdNew.ForeColor = System.Drawing.Color.White
        Me.cmdNew.Location = New System.Drawing.Point(113, 479)
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.Size = New System.Drawing.Size(99, 28)
        Me.cmdNew.TabIndex = 76
        Me.cmdNew.Text = "New"
        Me.cmdNew.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.cmdSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSave.FlatAppearance.BorderSize = 0
        Me.cmdSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.Color.White
        Me.cmdSave.Location = New System.Drawing.Point(218, 479)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(99, 28)
        Me.cmdSave.TabIndex = 75
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Controls.Add(Me.Label24)
        Me.PictureBox1.Controls.Add(Me.Label23)
        Me.PictureBox1.Controls.Add(Me.Label22)
        Me.PictureBox1.Controls.Add(Me.Label20)
        Me.PictureBox1.Controls.Add(Me.Label21)
        Me.PictureBox1.Controls.Add(Me.flxType)
        Me.PictureBox1.Controls.Add(Me.Label17)
        Me.PictureBox1.Controls.Add(Me.Label16)
        Me.PictureBox1.Controls.Add(Me.Label15)
        Me.PictureBox1.Controls.Add(Me.txtNoPay)
        Me.PictureBox1.Controls.Add(Me.Label14)
        Me.PictureBox1.Controls.Add(Me.txtRep)
        Me.PictureBox1.Controls.Add(Me.Label13)
        Me.PictureBox1.Controls.Add(Me.txtExt)
        Me.PictureBox1.Controls.Add(Me.Label12)
        Me.PictureBox1.Controls.Add(Me.txtBro)
        Me.PictureBox1.Controls.Add(Me.Label11)
        Me.PictureBox1.Controls.Add(Me.Label9)
        Me.PictureBox1.Controls.Add(Me.txtLostCts)
        Me.PictureBox1.Controls.Add(Me.txtLost)
        Me.PictureBox1.Controls.Add(Me.txtRejCts)
        Me.PictureBox1.Controls.Add(Me.txtRej)
        Me.PictureBox1.Controls.Add(Me.Label7)
        Me.PictureBox1.Controls.Add(Me.Label8)
        Me.PictureBox1.Controls.Add(Me.cmdAdd)
        Me.PictureBox1.Controls.Add(Me.txtTypeCts)
        Me.PictureBox1.Controls.Add(Me.txtTypePcs)
        Me.PictureBox1.Controls.Add(Me.txtRetTime)
        Me.PictureBox1.Controls.Add(Me.txtRetDate)
        Me.PictureBox1.Controls.Add(Me.txtRetCts)
        Me.PictureBox1.Controls.Add(Me.txtRetPcs)
        Me.PictureBox1.Controls.Add(Me.Label5)
        Me.PictureBox1.Controls.Add(Me.Label6)
        Me.PictureBox1.Controls.Add(Me.Label4)
        Me.PictureBox1.Controls.Add(Me.cmbType)
        Me.PictureBox1.Location = New System.Drawing.Point(8, 208)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(603, 265)
        Me.PictureBox1.TabIndex = 65
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(428, 59)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(24, 14)
        Me.Label24.TabIndex = 100
        Me.Label24.Text = "Cts"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(364, 59)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(25, 14)
        Me.Label23.TabIndex = 99
        Me.Label23.Text = "Pcs"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(203, 59)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(35, 14)
        Me.Label22.TabIndex = 98
        Me.Label22.Text = "Type"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(462, 10)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(34, 14)
        Me.Label20.TabIndex = 97
        Me.Label20.Text = "Time"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(349, 10)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(33, 14)
        Me.Label21.TabIndex = 96
        Me.Label21.Text = "Date"
        '
        'flxType
        '
        Me.flxType.AllowUserToAddRows = False
        Me.flxType.AllowUserToDeleteRows = False
        Me.flxType.AllowUserToResizeColumns = False
        Me.flxType.AllowUserToResizeRows = False
        Me.flxType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxType.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Code, Me.ParSize, Me.LotID})
        Me.flxType.Location = New System.Drawing.Point(206, 107)
        Me.flxType.Name = "flxType"
        Me.flxType.ReadOnly = True
        Me.flxType.RowHeadersVisible = False
        Me.flxType.Size = New System.Drawing.Size(391, 148)
        Me.flxType.TabIndex = 95
        '
        'Code
        '
        Me.Code.HeaderText = "Type"
        Me.Code.Name = "Code"
        Me.Code.ReadOnly = True
        '
        'ParSize
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ParSize.DefaultCellStyle = DataGridViewCellStyle1
        Me.ParSize.HeaderText = "Pcs"
        Me.ParSize.Name = "ParSize"
        Me.ParSize.ReadOnly = True
        '
        'LotID
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.LotID.DefaultCellStyle = DataGridViewCellStyle2
        Me.LotID.HeaderText = "Cts"
        Me.LotID.Name = "LotID"
        Me.LotID.ReadOnly = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(6, 233)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(25, 14)
        Me.Label17.TabIndex = 94
        Me.Label17.Text = "Pcs"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(6, 192)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(25, 14)
        Me.Label16.TabIndex = 93
        Me.Label16.Text = "Pcs"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(103, 217)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(45, 14)
        Me.Label15.TabIndex = 92
        Me.Label15.Text = "No Pay"
        '
        'txtNoPay
        '
        Me.txtNoPay.Location = New System.Drawing.Point(104, 233)
        Me.txtNoPay.Name = "txtNoPay"
        Me.txtNoPay.Size = New System.Drawing.Size(61, 22)
        Me.txtNoPay.TabIndex = 91
        Me.txtNoPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(36, 217)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(40, 14)
        Me.Label14.TabIndex = 90
        Me.Label14.Text = "Repair"
        '
        'txtRep
        '
        Me.txtRep.Location = New System.Drawing.Point(37, 233)
        Me.txtRep.Name = "txtRep"
        Me.txtRep.Size = New System.Drawing.Size(61, 22)
        Me.txtRep.TabIndex = 89
        Me.txtRep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(103, 176)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(35, 14)
        Me.Label13.TabIndex = 88
        Me.Label13.Text = "Extra"
        '
        'txtExt
        '
        Me.txtExt.Location = New System.Drawing.Point(104, 192)
        Me.txtExt.Name = "txtExt"
        Me.txtExt.Size = New System.Drawing.Size(61, 22)
        Me.txtExt.TabIndex = 87
        Me.txtExt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(36, 176)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(45, 14)
        Me.Label12.TabIndex = 86
        Me.Label12.Text = "Broken"
        '
        'txtBro
        '
        Me.txtBro.Location = New System.Drawing.Point(37, 192)
        Me.txtBro.Name = "txtBro"
        Me.txtBro.Size = New System.Drawing.Size(61, 22)
        Me.txtBro.TabIndex = 85
        Me.txtBro.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(101, 107)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(30, 14)
        Me.Label11.TabIndex = 84
        Me.Label11.Text = "Lost"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(36, 107)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(42, 14)
        Me.Label9.TabIndex = 83
        Me.Label9.Text = "Reject"
        '
        'txtLostCts
        '
        Me.txtLostCts.Location = New System.Drawing.Point(104, 151)
        Me.txtLostCts.Name = "txtLostCts"
        Me.txtLostCts.Size = New System.Drawing.Size(61, 22)
        Me.txtLostCts.TabIndex = 82
        Me.txtLostCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtLost
        '
        Me.txtLost.Location = New System.Drawing.Point(104, 123)
        Me.txtLost.Name = "txtLost"
        Me.txtLost.Size = New System.Drawing.Size(61, 22)
        Me.txtLost.TabIndex = 81
        Me.txtLost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRejCts
        '
        Me.txtRejCts.Location = New System.Drawing.Point(37, 151)
        Me.txtRejCts.Name = "txtRejCts"
        Me.txtRejCts.Size = New System.Drawing.Size(61, 22)
        Me.txtRejCts.TabIndex = 78
        Me.txtRejCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRej
        '
        Me.txtRej.Location = New System.Drawing.Point(37, 123)
        Me.txtRej.Name = "txtRej"
        Me.txtRej.Size = New System.Drawing.Size(61, 22)
        Me.txtRej.TabIndex = 77
        Me.txtRej.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 151)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(24, 14)
        Me.Label7.TabIndex = 76
        Me.Label7.Text = "Cts"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 123)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(25, 14)
        Me.Label8.TabIndex = 75
        Me.Label8.Text = "Pcs"
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.cmdAdd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdAdd.FlatAppearance.BorderSize = 0
        Me.cmdAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAdd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.ForeColor = System.Drawing.Color.White
        Me.cmdAdd.Location = New System.Drawing.Point(498, 73)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(99, 28)
        Me.cmdAdd.TabIndex = 74
        Me.cmdAdd.Text = "Add"
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'txtTypeCts
        '
        Me.txtTypeCts.Location = New System.Drawing.Point(431, 76)
        Me.txtTypeCts.Name = "txtTypeCts"
        Me.txtTypeCts.Size = New System.Drawing.Size(61, 22)
        Me.txtTypeCts.TabIndex = 73
        Me.txtTypeCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTypePcs
        '
        Me.txtTypePcs.Location = New System.Drawing.Point(364, 76)
        Me.txtTypePcs.Name = "txtTypePcs"
        Me.txtTypePcs.Size = New System.Drawing.Size(61, 22)
        Me.txtTypePcs.TabIndex = 72
        Me.txtTypePcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRetTime
        '
        Me.txtRetTime.Location = New System.Drawing.Point(465, 27)
        Me.txtRetTime.Name = "txtRetTime"
        Me.txtRetTime.ReadOnly = True
        Me.txtRetTime.Size = New System.Drawing.Size(107, 22)
        Me.txtRetTime.TabIndex = 71
        '
        'txtRetDate
        '
        Me.txtRetDate.Location = New System.Drawing.Point(353, 27)
        Me.txtRetDate.Name = "txtRetDate"
        Me.txtRetDate.ReadOnly = True
        Me.txtRetDate.Size = New System.Drawing.Size(108, 22)
        Me.txtRetDate.TabIndex = 70
        '
        'txtRetCts
        '
        Me.txtRetCts.Location = New System.Drawing.Point(37, 76)
        Me.txtRetCts.Name = "txtRetCts"
        Me.txtRetCts.Size = New System.Drawing.Size(61, 22)
        Me.txtRetCts.TabIndex = 69
        Me.txtRetCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRetPcs
        '
        Me.txtRetPcs.Location = New System.Drawing.Point(37, 48)
        Me.txtRetPcs.Name = "txtRetPcs"
        Me.txtRetPcs.Size = New System.Drawing.Size(61, 22)
        Me.txtRetPcs.TabIndex = 68
        Me.txtRetPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 79)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(24, 14)
        Me.Label5.TabIndex = 67
        Me.Label5.Text = "Cts"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 51)
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
        Me.Label4.Size = New System.Drawing.Size(63, 14)
        Me.Label4.TabIndex = 65
        Me.Label4.Text = "RETURNS"
        '
        'cmbType
        '
        Me.cmbType.FormattingEnabled = True
        Me.cmbType.IntegralHeight = False
        Me.cmbType.Location = New System.Drawing.Point(206, 76)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Size = New System.Drawing.Size(152, 22)
        Me.cmbType.TabIndex = 64
        '
        'PictureBox2
        '
        Me.PictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox2.Controls.Add(Me.Label19)
        Me.PictureBox2.Controls.Add(Me.Label18)
        Me.PictureBox2.Controls.Add(Me.txtModel)
        Me.PictureBox2.Controls.Add(Me.txtFlo)
        Me.PictureBox2.Controls.Add(Me.txtClarity)
        Me.PictureBox2.Controls.Add(Me.txtColor)
        Me.PictureBox2.Controls.Add(Me.txtBoiling)
        Me.PictureBox2.Controls.Add(Me.Label3)
        Me.PictureBox2.Controls.Add(Me.Label2)
        Me.PictureBox2.Controls.Add(Me.Label1)
        Me.PictureBox2.Controls.Add(Me.txtIssPcs)
        Me.PictureBox2.Controls.Add(Me.txtIssTime)
        Me.PictureBox2.Controls.Add(Me.txtIssDate)
        Me.PictureBox2.Controls.Add(Me.txtIssCts)
        Me.PictureBox2.Location = New System.Drawing.Point(8, 61)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(603, 132)
        Me.PictureBox2.TabIndex = 64
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(462, 51)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(34, 14)
        Me.Label19.TabIndex = 85
        Me.Label19.Text = "Time"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(349, 51)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(33, 14)
        Me.Label18.TabIndex = 84
        Me.Label18.Text = "Date"
        '
        'txtModel
        '
        Me.txtModel.Location = New System.Drawing.Point(465, 97)
        Me.txtModel.Name = "txtModel"
        Me.txtModel.ReadOnly = True
        Me.txtModel.Size = New System.Drawing.Size(109, 22)
        Me.txtModel.TabIndex = 68
        '
        'txtFlo
        '
        Me.txtFlo.Location = New System.Drawing.Point(352, 97)
        Me.txtFlo.Name = "txtFlo"
        Me.txtFlo.ReadOnly = True
        Me.txtFlo.Size = New System.Drawing.Size(109, 22)
        Me.txtFlo.TabIndex = 67
        '
        'txtClarity
        '
        Me.txtClarity.Location = New System.Drawing.Point(237, 97)
        Me.txtClarity.Name = "txtClarity"
        Me.txtClarity.ReadOnly = True
        Me.txtClarity.Size = New System.Drawing.Size(109, 22)
        Me.txtClarity.TabIndex = 66
        '
        'txtColor
        '
        Me.txtColor.Location = New System.Drawing.Point(122, 97)
        Me.txtColor.Name = "txtColor"
        Me.txtColor.ReadOnly = True
        Me.txtColor.Size = New System.Drawing.Size(109, 22)
        Me.txtColor.TabIndex = 65
        '
        'txtBoiling
        '
        Me.txtBoiling.Location = New System.Drawing.Point(7, 97)
        Me.txtBoiling.Name = "txtBoiling"
        Me.txtBoiling.ReadOnly = True
        Me.txtBoiling.Size = New System.Drawing.Size(109, 22)
        Me.txtBoiling.TabIndex = 64
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(24, 14)
        Me.Label3.TabIndex = 63
        Me.Label3.Text = "Cts"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(25, 14)
        Me.Label2.TabIndex = 62
        Me.Label2.Text = "Pcs"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(6, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 14)
        Me.Label1.TabIndex = 61
        Me.Label1.Text = "ISSUES"
        '
        'txtIssPcs
        '
        Me.txtIssPcs.Location = New System.Drawing.Point(37, 38)
        Me.txtIssPcs.Name = "txtIssPcs"
        Me.txtIssPcs.ReadOnly = True
        Me.txtIssPcs.Size = New System.Drawing.Size(61, 22)
        Me.txtIssPcs.TabIndex = 60
        Me.txtIssPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtIssTime
        '
        Me.txtIssTime.Location = New System.Drawing.Point(465, 68)
        Me.txtIssTime.Name = "txtIssTime"
        Me.txtIssTime.ReadOnly = True
        Me.txtIssTime.Size = New System.Drawing.Size(109, 22)
        Me.txtIssTime.TabIndex = 45
        '
        'txtIssDate
        '
        Me.txtIssDate.Location = New System.Drawing.Point(353, 68)
        Me.txtIssDate.Name = "txtIssDate"
        Me.txtIssDate.ReadOnly = True
        Me.txtIssDate.Size = New System.Drawing.Size(108, 22)
        Me.txtIssDate.TabIndex = 44
        '
        'txtIssCts
        '
        Me.txtIssCts.Location = New System.Drawing.Point(37, 66)
        Me.txtIssCts.Name = "txtIssCts"
        Me.txtIssCts.ReadOnly = True
        Me.txtIssCts.Size = New System.Drawing.Size(61, 22)
        Me.txtIssCts.TabIndex = 43
        Me.txtIssCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtEmp
        '
        Me.txtEmp.Location = New System.Drawing.Point(507, 22)
        Me.txtEmp.Name = "txtEmp"
        Me.txtEmp.ReadOnly = True
        Me.txtEmp.Size = New System.Drawing.Size(104, 22)
        Me.txtEmp.TabIndex = 36
        '
        'cmdEmp
        '
        Me.cmdEmp.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.cmdEmp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdEmp.FlatAppearance.BorderSize = 0
        Me.cmdEmp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdEmp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdEmp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdEmp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEmp.ForeColor = System.Drawing.Color.White
        Me.cmdEmp.Location = New System.Drawing.Point(402, 18)
        Me.cmdEmp.Name = "cmdEmp"
        Me.cmdEmp.Size = New System.Drawing.Size(99, 28)
        Me.cmdEmp.TabIndex = 35
        Me.cmdEmp.Text = "Employee No"
        Me.cmdEmp.UseVisualStyleBackColor = False
        '
        'txtParPkt
        '
        Me.txtParPkt.Location = New System.Drawing.Point(294, 22)
        Me.txtParPkt.Name = "txtParPkt"
        Me.txtParPkt.ReadOnly = True
        Me.txtParPkt.Size = New System.Drawing.Size(104, 22)
        Me.txtParPkt.TabIndex = 34
        '
        'cmdParPkt
        '
        Me.cmdParPkt.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.cmdParPkt.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdParPkt.FlatAppearance.BorderSize = 0
        Me.cmdParPkt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdParPkt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdParPkt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdParPkt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdParPkt.ForeColor = System.Drawing.Color.White
        Me.cmdParPkt.Location = New System.Drawing.Point(189, 18)
        Me.cmdParPkt.Name = "cmdParPkt"
        Me.cmdParPkt.Size = New System.Drawing.Size(99, 28)
        Me.cmdParPkt.TabIndex = 1
        Me.cmdParPkt.Text = "Parcel/Packet"
        Me.cmdParPkt.UseVisualStyleBackColor = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(9, 6)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(48, 14)
        Me.Label10.TabIndex = 30
        Me.Label10.Text = "Section"
        '
        'cmbSection
        '
        Me.cmbSection.FormattingEnabled = True
        Me.cmbSection.IntegralHeight = False
        Me.cmbSection.Location = New System.Drawing.Point(9, 24)
        Me.cmbSection.Name = "cmbSection"
        Me.cmbSection.Size = New System.Drawing.Size(152, 22)
        Me.cmbSection.TabIndex = 0
        '
        'frm_RghSection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(622, 568)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_RghSection"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Rough Issue/Return"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        Me.PictureBox1.ResumeLayout(False)
        Me.PictureBox1.PerformLayout()
        CType(Me.flxType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PictureBox2.ResumeLayout(False)
        Me.PictureBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbSection As System.Windows.Forms.ComboBox
    Friend WithEvents cmdParPkt As DiaStock.HazelDev_Button
    Friend WithEvents txtParPkt As System.Windows.Forms.TextBox
    Friend WithEvents txtEmp As System.Windows.Forms.TextBox
    Friend WithEvents cmdEmp As DiaStock.HazelDev_Button
    Friend WithEvents txtIssCts As System.Windows.Forms.TextBox
    Friend WithEvents txtIssTime As System.Windows.Forms.TextBox
    Friend WithEvents txtIssDate As System.Windows.Forms.TextBox
    Friend WithEvents txtIssPcs As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.Panel
    Friend WithEvents cmdAdd As DiaStock.HazelDev_Button
    Friend WithEvents txtTypeCts As System.Windows.Forms.TextBox
    Friend WithEvents txtTypePcs As System.Windows.Forms.TextBox
    Friend WithEvents txtRetTime As System.Windows.Forms.TextBox
    Friend WithEvents txtRetDate As System.Windows.Forms.TextBox
    Friend WithEvents txtRetCts As System.Windows.Forms.TextBox
    Friend WithEvents txtRetPcs As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbType As System.Windows.Forms.ComboBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.Panel
    Friend WithEvents txtModel As System.Windows.Forms.TextBox
    Friend WithEvents txtFlo As System.Windows.Forms.TextBox
    Friend WithEvents txtClarity As System.Windows.Forms.TextBox
    Friend WithEvents txtColor As System.Windows.Forms.TextBox
    Friend WithEvents txtBoiling As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtLostCts As System.Windows.Forms.TextBox
    Friend WithEvents txtLost As System.Windows.Forms.TextBox
    Friend WithEvents txtRejCts As System.Windows.Forms.TextBox
    Friend WithEvents txtRej As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtNoPay As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtRep As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtExt As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtBro As System.Windows.Forms.TextBox
    Friend WithEvents flxType As System.Windows.Forms.DataGridView
    Friend WithEvents Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParSize As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LotID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
End Class
