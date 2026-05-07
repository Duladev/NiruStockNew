<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_RprPacketTransfer
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
        Me.Label32 = New System.Windows.Forms.Label()
        Me.cmbModel = New System.Windows.Forms.ComboBox()
        Me.cmdParPkt = New DiaStock.HazelDev_Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtSelCts = New System.Windows.Forms.TextBox()
        Me.txtSelPcs = New System.Windows.Forms.TextBox()
        Me.flxSelected = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RefNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbNewDept = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbDept = New System.Windows.Forms.ComboBox()
        Me.cmdSelect = New DiaStock.HazelDev_Button()
        Me.txtBalCts = New System.Windows.Forms.TextBox()
        Me.txtBalPcs = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtPktNo = New System.Windows.Forms.TextBox()
        Me.pnlDetails2 = New System.Windows.Forms.Panel()
        Me.txtTotCts = New System.Windows.Forms.TextBox()
        Me.txtTotPcs = New System.Windows.Forms.TextBox()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DepartmentName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Company = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParcelNo1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.txtAvgPrice = New System.Windows.Forms.TextBox()
        Me.txtParNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCount = New System.Windows.Forms.TextBox()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.flxSelected, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDetails2.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label32)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbModel)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdParPkt)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Panel1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbNewDept)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbDept)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdSelect)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtBalCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtBalPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label8)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label10)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label23)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPktNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.pnlDetails2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtAvgPrice)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtParNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label4)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(780, 670)
        Me.HazelDev_ThemeContainer1.TabIndex = 4
        Me.HazelDev_ThemeContainer1.Text = "ROUGH PROCESS PACKET TRANSFER"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Location = New System.Drawing.Point(167, 107)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(42, 15)
        Me.Label32.TabIndex = 95
        Me.Label32.Text = "Model"
        '
        'cmbModel
        '
        Me.cmbModel.FormattingEnabled = True
        Me.cmbModel.IntegralHeight = False
        Me.cmbModel.Location = New System.Drawing.Point(170, 125)
        Me.cmbModel.Name = "cmbModel"
        Me.cmbModel.Size = New System.Drawing.Size(87, 23)
        Me.cmbModel.TabIndex = 94
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
        Me.cmdParPkt.Location = New System.Drawing.Point(170, 164)
        Me.cmdParPkt.Name = "cmdParPkt"
        Me.cmdParPkt.Size = New System.Drawing.Size(99, 28)
        Me.cmdParPkt.TabIndex = 93
        Me.cmdParPkt.Text = "Parcel/Packet"
        Me.cmdParPkt.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtSelCts)
        Me.Panel1.Controls.Add(Me.txtSelPcs)
        Me.Panel1.Controls.Add(Me.flxSelected)
        Me.Panel1.Location = New System.Drawing.Point(365, 198)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(409, 468)
        Me.Panel1.TabIndex = 92
        '
        'txtSelCts
        '
        Me.txtSelCts.Location = New System.Drawing.Point(200, 438)
        Me.txtSelCts.Name = "txtSelCts"
        Me.txtSelCts.ReadOnly = True
        Me.txtSelCts.Size = New System.Drawing.Size(92, 21)
        Me.txtSelCts.TabIndex = 92
        Me.txtSelCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSelPcs
        '
        Me.txtSelPcs.Location = New System.Drawing.Point(105, 438)
        Me.txtSelPcs.Name = "txtSelPcs"
        Me.txtSelPcs.ReadOnly = True
        Me.txtSelPcs.Size = New System.Drawing.Size(91, 21)
        Me.txtSelPcs.TabIndex = 91
        Me.txtSelPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'flxSelected
        '
        Me.flxSelected.AllowUserToAddRows = False
        Me.flxSelected.AllowUserToDeleteRows = False
        Me.flxSelected.AllowUserToResizeColumns = False
        Me.flxSelected.AllowUserToResizeRows = False
        Me.flxSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxSelected.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.RefNo})
        Me.flxSelected.Location = New System.Drawing.Point(6, 4)
        Me.flxSelected.Name = "flxSelected"
        Me.flxSelected.ReadOnly = True
        Me.flxSelected.RowHeadersVisible = False
        Me.flxSelected.Size = New System.Drawing.Size(398, 428)
        Me.flxSelected.TabIndex = 44
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Pkt No."
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewTextBoxColumn2.HeaderText = "Pcs"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 80
        '
        'DataGridViewTextBoxColumn3
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewTextBoxColumn3.HeaderText = "Cts"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'RefNo
        '
        Me.RefNo.HeaderText = "Ref No."
        Me.RefNo.Name = "RefNo"
        Me.RefNo.ReadOnly = True
        Me.RefNo.Width = 80
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(365, 107)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 15)
        Me.Label2.TabIndex = 91
        Me.Label2.Text = "Department"
        '
        'cmbNewDept
        '
        Me.cmbNewDept.FormattingEnabled = True
        Me.cmbNewDept.IntegralHeight = False
        Me.cmbNewDept.Location = New System.Drawing.Point(365, 125)
        Me.cmbNewDept.Name = "cmbNewDept"
        Me.cmbNewDept.Size = New System.Drawing.Size(152, 23)
        Me.cmbNewDept.TabIndex = 90
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(12, 107)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 15)
        Me.Label1.TabIndex = 89
        Me.Label1.Text = "Department"
        '
        'cmbDept
        '
        Me.cmbDept.FormattingEnabled = True
        Me.cmbDept.IntegralHeight = False
        Me.cmbDept.Location = New System.Drawing.Point(12, 125)
        Me.cmbDept.Name = "cmbDept"
        Me.cmbDept.Size = New System.Drawing.Size(152, 23)
        Me.cmbDept.TabIndex = 88
        '
        'cmdSelect
        '
        Me.cmdSelect.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSelect.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSelect.FlatAppearance.BorderSize = 0
        Me.cmdSelect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdSelect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSelect.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSelect.ForeColor = System.Drawing.Color.White
        Me.cmdSelect.Location = New System.Drawing.Point(365, 164)
        Me.cmdSelect.Name = "cmdSelect"
        Me.cmdSelect.Size = New System.Drawing.Size(76, 28)
        Me.cmdSelect.TabIndex = 87
        Me.cmdSelect.Text = "Select All"
        Me.cmdSelect.UseVisualStyleBackColor = False
        '
        'txtBalCts
        '
        Me.txtBalCts.Location = New System.Drawing.Point(549, 171)
        Me.txtBalCts.Name = "txtBalCts"
        Me.txtBalCts.ReadOnly = True
        Me.txtBalCts.Size = New System.Drawing.Size(74, 21)
        Me.txtBalCts.TabIndex = 82
        Me.txtBalCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBalPcs
        '
        Me.txtBalPcs.Location = New System.Drawing.Point(471, 171)
        Me.txtBalPcs.Name = "txtBalPcs"
        Me.txtBalPcs.ReadOnly = True
        Me.txtBalPcs.Size = New System.Drawing.Size(72, 21)
        Me.txtBalPcs.TabIndex = 81
        Me.txtBalPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(546, 153)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(45, 15)
        Me.Label8.TabIndex = 80
        Me.Label8.Text = "Bal Cts"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(468, 153)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(48, 15)
        Me.Label10.TabIndex = 79
        Me.Label10.Text = "Bal Pcs"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Location = New System.Drawing.Point(12, 153)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(46, 15)
        Me.Label23.TabIndex = 76
        Me.Label23.Text = "Pkt No."
        '
        'txtPktNo
        '
        Me.txtPktNo.Location = New System.Drawing.Point(12, 171)
        Me.txtPktNo.Name = "txtPktNo"
        Me.txtPktNo.Size = New System.Drawing.Size(74, 21)
        Me.txtPktNo.TabIndex = 73
        '
        'pnlDetails2
        '
        Me.pnlDetails2.BackColor = System.Drawing.Color.White
        Me.pnlDetails2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDetails2.Controls.Add(Me.txtCount)
        Me.pnlDetails2.Controls.Add(Me.txtTotCts)
        Me.pnlDetails2.Controls.Add(Me.txtTotPcs)
        Me.pnlDetails2.Controls.Add(Me.flxDetails)
        Me.pnlDetails2.Location = New System.Drawing.Point(12, 198)
        Me.pnlDetails2.Name = "pnlDetails2"
        Me.pnlDetails2.Size = New System.Drawing.Size(335, 468)
        Me.pnlDetails2.TabIndex = 68
        '
        'txtTotCts
        '
        Me.txtTotCts.Location = New System.Drawing.Point(204, 438)
        Me.txtTotCts.Name = "txtTotCts"
        Me.txtTotCts.ReadOnly = True
        Me.txtTotCts.Size = New System.Drawing.Size(92, 21)
        Me.txtTotCts.TabIndex = 92
        Me.txtTotCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotPcs
        '
        Me.txtTotPcs.Location = New System.Drawing.Point(109, 438)
        Me.txtTotPcs.Name = "txtTotPcs"
        Me.txtTotPcs.ReadOnly = True
        Me.txtTotPcs.Size = New System.Drawing.Size(91, 21)
        Me.txtTotPcs.TabIndex = 91
        Me.txtTotPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Code, Me.DepartmentName, Me.Company, Me.ParcelNo1})
        Me.flxDetails.Location = New System.Drawing.Point(6, 4)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.ReadOnly = True
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(318, 428)
        Me.flxDetails.TabIndex = 44
        '
        'Code
        '
        Me.Code.HeaderText = "Pkt No."
        Me.Code.Name = "Code"
        Me.Code.ReadOnly = True
        '
        'DepartmentName
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DepartmentName.DefaultCellStyle = DataGridViewCellStyle3
        Me.DepartmentName.HeaderText = "Pcs"
        Me.DepartmentName.Name = "DepartmentName"
        Me.DepartmentName.ReadOnly = True
        Me.DepartmentName.Width = 80
        '
        'Company
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Company.DefaultCellStyle = DataGridViewCellStyle4
        Me.Company.HeaderText = "Cts"
        Me.Company.Name = "Company"
        Me.Company.ReadOnly = True
        '
        'ParcelNo1
        '
        Me.ParcelNo1.HeaderText = "Parcel No"
        Me.ParcelNo1.MinimumWidth = 50
        Me.ParcelNo1.Name = "ParcelNo1"
        Me.ParcelNo1.ReadOnly = True
        Me.ParcelNo1.Visible = False
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(771, 50)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
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
        'txtAvgPrice
        '
        Me.txtAvgPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAvgPrice.Location = New System.Drawing.Point(797, 539)
        Me.txtAvgPrice.Name = "txtAvgPrice"
        Me.txtAvgPrice.ReadOnly = True
        Me.txtAvgPrice.Size = New System.Drawing.Size(94, 21)
        Me.txtAvgPrice.TabIndex = 9
        Me.txtAvgPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtParNo
        '
        Me.txtParNo.Location = New System.Drawing.Point(263, 125)
        Me.txtParNo.Name = "txtParNo"
        Me.txtParNo.Size = New System.Drawing.Size(84, 21)
        Me.txtParNo.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(263, 107)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 15)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Parcel No"
        '
        'txtCount
        '
        Me.txtCount.Location = New System.Drawing.Point(6, 438)
        Me.txtCount.Name = "txtCount"
        Me.txtCount.ReadOnly = True
        Me.txtCount.Size = New System.Drawing.Size(91, 21)
        Me.txtCount.TabIndex = 93
        '
        'frm_RprPacketTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(780, 670)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_RprPacketTransfer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Rpr Packet Transfer"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.flxSelected, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDetails2.ResumeLayout(False)
        Me.pnlDetails2.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents cmdSelect As DiaStock.HazelDev_Button
    Friend WithEvents txtBalCts As System.Windows.Forms.TextBox
    Friend WithEvents txtBalPcs As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtPktNo As System.Windows.Forms.TextBox
    Friend WithEvents pnlDetails2 As System.Windows.Forms.Panel
    Friend WithEvents txtTotCts As System.Windows.Forms.TextBox
    Friend WithEvents txtTotPcs As System.Windows.Forms.TextBox
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents txtAvgPrice As System.Windows.Forms.TextBox
    Friend WithEvents txtParNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbNewDept As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbDept As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtSelCts As System.Windows.Forms.TextBox
    Friend WithEvents txtSelPcs As System.Windows.Forms.TextBox
    Friend WithEvents flxSelected As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RefNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdParPkt As DiaStock.HazelDev_Button
    Friend WithEvents Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DepartmentName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Company As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParcelNo1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents cmbModel As System.Windows.Forms.ComboBox
    Friend WithEvents txtCount As System.Windows.Forms.TextBox
End Class
