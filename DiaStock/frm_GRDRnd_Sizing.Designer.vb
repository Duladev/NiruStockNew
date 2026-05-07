<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_GRDRnd_Sizing
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_GRDRnd_Sizing))
        Me.pnlTitle = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.lblDept = New System.Windows.Forms.Label()
        Me.cmbDept = New System.Windows.Forms.ComboBox()
        Me.lblSection = New System.Windows.Forms.Label()
        Me.cmbSection = New System.Windows.Forms.ComboBox()
        Me.lblParcelNo = New System.Windows.Forms.Label()
        Me.txtParNo = New System.Windows.Forms.TextBox()
        Me.lblPacketNo = New System.Windows.Forms.Label()
        Me.txtPktNo = New System.Windows.Forms.TextBox()
        Me.lblCode = New System.Windows.Forms.Label()
        Me.txtType1 = New System.Windows.Forms.TextBox()
        Me.lblPacketType = New System.Windows.Forms.Label()
        Me.txtPktType = New System.Windows.Forms.TextBox()
        Me.btnEmpNo = New System.Windows.Forms.Button()
        Me.txtemp = New System.Windows.Forms.TextBox()
        Me.chkNew = New System.Windows.Forms.CheckBox()
        Me.grpIssues = New System.Windows.Forms.GroupBox()
        Me.lblIssPcs = New System.Windows.Forms.Label()
        Me.txtIssTap = New System.Windows.Forms.TextBox()
        Me.lblIssCts = New System.Windows.Forms.Label()
        Me.txtIssCts = New System.Windows.Forms.TextBox()
        Me.lblIssDate = New System.Windows.Forms.Label()
        Me.txtIssDate = New System.Windows.Forms.TextBox()
        Me.lblIssTime = New System.Windows.Forms.Label()
        Me.txtIssTime = New System.Windows.Forms.TextBox()
        Me.grpReturns = New System.Windows.Forms.GroupBox()
        Me.lblRetPcs = New System.Windows.Forms.Label()
        Me.txtRetTap = New System.Windows.Forms.TextBox()
        Me.lblRetCts = New System.Windows.Forms.Label()
        Me.txtRetCts = New System.Windows.Forms.TextBox()
        Me.lblRejects = New System.Windows.Forms.Label()
        Me.lblRejPcs = New System.Windows.Forms.Label()
        Me.txtRej = New System.Windows.Forms.TextBox()
        Me.lblRejCts = New System.Windows.Forms.Label()
        Me.txtRejCts = New System.Windows.Forms.TextBox()
        Me.lblLost = New System.Windows.Forms.Label()
        Me.lblLostPcs = New System.Windows.Forms.Label()
        Me.txtLostPcs = New System.Windows.Forms.TextBox()
        Me.lblLostCts = New System.Windows.Forms.Label()
        Me.txtLostCts = New System.Windows.Forms.TextBox()
        Me.lblRepair = New System.Windows.Forms.Label()
        Me.lblRepPcs = New System.Windows.Forms.Label()
        Me.txtRepPcs = New System.Windows.Forms.TextBox()
        Me.lblRepCts = New System.Windows.Forms.Label()
        Me.txtRepCts = New System.Windows.Forms.TextBox()
        Me.lblRetDate = New System.Windows.Forms.Label()
        Me.txtRetDate = New System.Windows.Forms.TextBox()
        Me.lblRetTime = New System.Windows.Forms.Label()
        Me.txtRetTime = New System.Windows.Forms.TextBox()
        Me.grpSizingTypes = New System.Windows.Forms.GroupBox()
        Me.lblAssortment = New System.Windows.Forms.Label()
        Me.txtAssortment = New System.Windows.Forms.TextBox()
        Me.lblSizeRange = New System.Windows.Forms.Label()
        Me.cmbSize = New System.Windows.Forms.ComboBox()
        Me.lblTypePcs = New System.Windows.Forms.Label()
        Me.txtTypePcs = New System.Windows.Forms.TextBox()
        Me.lblTotPcs = New System.Windows.Forms.Label()
        Me.txtTotCts = New System.Windows.Forms.TextBox()
        Me.lblTotCts = New System.Windows.Forms.Label()
        Me.lblTypeCts = New System.Windows.Forms.Label()
        Me.txtTotPcs = New System.Windows.Forms.TextBox()
        Me.txtTypeCts = New System.Windows.Forms.TextBox()
        Me.btnAddType = New System.Windows.Forms.Button()
        Me.dgvGrid = New System.Windows.Forms.DataGridView()
        Me.grpEmp = New System.Windows.Forms.GroupBox()
        Me.lblEmpNo = New System.Windows.Forms.Label()
        Me.txtEmpNo = New System.Windows.Forms.TextBox()
        Me.lblEmpPcs = New System.Windows.Forms.Label()
        Me.txtEmpPcs = New System.Windows.Forms.TextBox()
        Me.btnEmpAdd = New System.Windows.Forms.Button()
        Me.dgvEmp = New System.Windows.Forms.DataGridView()
        Me.pnlControl = New System.Windows.Forms.Panel()
        Me.btnFirst = New System.Windows.Forms.Button()
        Me.btnPrevious = New System.Windows.Forms.Button()
        Me.txtRecordCount = New System.Windows.Forms.TextBox()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnLast = New System.Windows.Forms.Button()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.pnlTitle.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.grpIssues.SuspendLayout()
        Me.grpReturns.SuspendLayout()
        Me.grpSizingTypes.SuspendLayout()
        CType(Me.dgvGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpEmp.SuspendLayout()
        CType(Me.dgvEmp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlTitle
        '
        Me.pnlTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.pnlTitle.Controls.Add(Me.lblTitle)
        Me.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTitle.Location = New System.Drawing.Point(0, 110)
        Me.pnlTitle.Name = "pnlTitle"
        Me.pnlTitle.Size = New System.Drawing.Size(1048, 36)
        Me.pnlTitle.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTitle.Font = New System.Drawing.Font("Trebuchet MS", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(1048, 36)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Sizing Issues && Returns"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.Button1)
        Me.pnlHeader.Controls.Add(Me.lblDept)
        Me.pnlHeader.Controls.Add(Me.cmbDept)
        Me.pnlHeader.Controls.Add(Me.lblSection)
        Me.pnlHeader.Controls.Add(Me.cmbSection)
        Me.pnlHeader.Controls.Add(Me.lblParcelNo)
        Me.pnlHeader.Controls.Add(Me.txtParNo)
        Me.pnlHeader.Controls.Add(Me.lblPacketNo)
        Me.pnlHeader.Controls.Add(Me.txtPktNo)
        Me.pnlHeader.Controls.Add(Me.lblCode)
        Me.pnlHeader.Controls.Add(Me.txtType1)
        Me.pnlHeader.Controls.Add(Me.lblPacketType)
        Me.pnlHeader.Controls.Add(Me.txtPktType)
        Me.pnlHeader.Controls.Add(Me.btnEmpNo)
        Me.pnlHeader.Controls.Add(Me.txtemp)
        Me.pnlHeader.Controls.Add(Me.chkNew)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Padding = New System.Windows.Forms.Padding(8)
        Me.pnlHeader.Size = New System.Drawing.Size(1048, 110)
        Me.pnlHeader.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.Button1.Location = New System.Drawing.Point(879, 24)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(84, 32)
        Me.Button1.TabIndex = 9
        Me.Button1.Text = "Exit"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'lblDept
        '
        Me.lblDept.AutoSize = True
        Me.lblDept.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblDept.Location = New System.Drawing.Point(10, 12)
        Me.lblDept.Name = "lblDept"
        Me.lblDept.Size = New System.Drawing.Size(87, 16)
        Me.lblDept.TabIndex = 0
        Me.lblDept.Text = "Department"
        '
        'cmbDept
        '
        Me.cmbDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDept.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmbDept.Location = New System.Drawing.Point(10, 32)
        Me.cmbDept.Name = "cmbDept"
        Me.cmbDept.Size = New System.Drawing.Size(155, 21)
        Me.cmbDept.TabIndex = 0
        '
        'lblSection
        '
        Me.lblSection.AutoSize = True
        Me.lblSection.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblSection.Location = New System.Drawing.Point(10, 60)
        Me.lblSection.Name = "lblSection"
        Me.lblSection.Size = New System.Drawing.Size(59, 16)
        Me.lblSection.TabIndex = 1
        Me.lblSection.Text = "Section"
        '
        'cmbSection
        '
        Me.cmbSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSection.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmbSection.Location = New System.Drawing.Point(10, 78)
        Me.cmbSection.Name = "cmbSection"
        Me.cmbSection.Size = New System.Drawing.Size(155, 21)
        Me.cmbSection.TabIndex = 1
        '
        'lblParcelNo
        '
        Me.lblParcelNo.AutoSize = True
        Me.lblParcelNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblParcelNo.ForeColor = System.Drawing.Color.Black
        Me.lblParcelNo.Location = New System.Drawing.Point(178, 12)
        Me.lblParcelNo.Name = "lblParcelNo"
        Me.lblParcelNo.Size = New System.Drawing.Size(76, 16)
        Me.lblParcelNo.TabIndex = 2
        Me.lblParcelNo.Text = "Parcel No"
        '
        'txtParNo
        '
        Me.txtParNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtParNo.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtParNo.Location = New System.Drawing.Point(178, 32)
        Me.txtParNo.Name = "txtParNo"
        Me.txtParNo.Size = New System.Drawing.Size(110, 21)
        Me.txtParNo.TabIndex = 2
        '
        'lblPacketNo
        '
        Me.lblPacketNo.AutoSize = True
        Me.lblPacketNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblPacketNo.ForeColor = System.Drawing.Color.Black
        Me.lblPacketNo.Location = New System.Drawing.Point(300, 12)
        Me.lblPacketNo.Name = "lblPacketNo"
        Me.lblPacketNo.Size = New System.Drawing.Size(79, 16)
        Me.lblPacketNo.TabIndex = 3
        Me.lblPacketNo.Text = "Packet No"
        '
        'txtPktNo
        '
        Me.txtPktNo.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtPktNo.Location = New System.Drawing.Point(300, 32)
        Me.txtPktNo.Name = "txtPktNo"
        Me.txtPktNo.Size = New System.Drawing.Size(70, 21)
        Me.txtPktNo.TabIndex = 3
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblCode.ForeColor = System.Drawing.Color.Black
        Me.lblCode.Location = New System.Drawing.Point(385, 12)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(44, 16)
        Me.lblCode.TabIndex = 4
        Me.lblCode.Text = "Code"
        '
        'txtType1
        '
        Me.txtType1.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtType1.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtType1.Location = New System.Drawing.Point(385, 32)
        Me.txtType1.Name = "txtType1"
        Me.txtType1.ReadOnly = True
        Me.txtType1.Size = New System.Drawing.Size(100, 21)
        Me.txtType1.TabIndex = 4
        Me.txtType1.TabStop = False
        '
        'lblPacketType
        '
        Me.lblPacketType.AutoSize = True
        Me.lblPacketType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblPacketType.ForeColor = System.Drawing.Color.Black
        Me.lblPacketType.Location = New System.Drawing.Point(498, 12)
        Me.lblPacketType.Name = "lblPacketType"
        Me.lblPacketType.Size = New System.Drawing.Size(95, 16)
        Me.lblPacketType.TabIndex = 5
        Me.lblPacketType.Text = "Packet Type"
        '
        'txtPktType
        '
        Me.txtPktType.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtPktType.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtPktType.Location = New System.Drawing.Point(498, 32)
        Me.txtPktType.Name = "txtPktType"
        Me.txtPktType.ReadOnly = True
        Me.txtPktType.Size = New System.Drawing.Size(110, 21)
        Me.txtPktType.TabIndex = 5
        Me.txtPktType.TabStop = False
        '
        'btnEmpNo
        '
        Me.btnEmpNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.btnEmpNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEmpNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnEmpNo.Location = New System.Drawing.Point(625, 12)
        Me.btnEmpNo.Name = "btnEmpNo"
        Me.btnEmpNo.Size = New System.Drawing.Size(100, 22)
        Me.btnEmpNo.TabIndex = 6
        Me.btnEmpNo.Text = "Employee No"
        Me.btnEmpNo.UseVisualStyleBackColor = False
        '
        'txtemp
        '
        Me.txtemp.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtemp.Location = New System.Drawing.Point(625, 38)
        Me.txtemp.MaxLength = 6
        Me.txtemp.Name = "txtemp"
        Me.txtemp.Size = New System.Drawing.Size(100, 21)
        Me.txtemp.TabIndex = 7
        '
        'chkNew
        '
        Me.chkNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.chkNew.ForeColor = System.Drawing.Color.DarkBlue
        Me.chkNew.Location = New System.Drawing.Point(745, 32)
        Me.chkNew.Name = "chkNew"
        Me.chkNew.Size = New System.Drawing.Size(60, 24)
        Me.chkNew.TabIndex = 8
        Me.chkNew.Text = "New"
        '
        'grpIssues
        '
        Me.grpIssues.Controls.Add(Me.lblIssPcs)
        Me.grpIssues.Controls.Add(Me.txtIssTap)
        Me.grpIssues.Controls.Add(Me.lblIssCts)
        Me.grpIssues.Controls.Add(Me.txtIssCts)
        Me.grpIssues.Controls.Add(Me.lblIssDate)
        Me.grpIssues.Controls.Add(Me.txtIssDate)
        Me.grpIssues.Controls.Add(Me.lblIssTime)
        Me.grpIssues.Controls.Add(Me.txtIssTime)
        Me.grpIssues.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.grpIssues.ForeColor = System.Drawing.Color.DarkRed
        Me.grpIssues.Location = New System.Drawing.Point(8, 152)
        Me.grpIssues.Name = "grpIssues"
        Me.grpIssues.Size = New System.Drawing.Size(1028, 100)
        Me.grpIssues.TabIndex = 10
        Me.grpIssues.TabStop = False
        Me.grpIssues.Text = "Issues"
        Me.grpIssues.Visible = False
        '
        'lblIssPcs
        '
        Me.lblIssPcs.AutoSize = True
        Me.lblIssPcs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblIssPcs.ForeColor = System.Drawing.Color.Black
        Me.lblIssPcs.Location = New System.Drawing.Point(8, 24)
        Me.lblIssPcs.Name = "lblIssPcs"
        Me.lblIssPcs.Size = New System.Drawing.Size(26, 13)
        Me.lblIssPcs.TabIndex = 0
        Me.lblIssPcs.Text = "PCs"
        '
        'txtIssTap
        '
        Me.txtIssTap.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtIssTap.Location = New System.Drawing.Point(40, 20)
        Me.txtIssTap.Name = "txtIssTap"
        Me.txtIssTap.Size = New System.Drawing.Size(60, 21)
        Me.txtIssTap.TabIndex = 0
        '
        'lblIssCts
        '
        Me.lblIssCts.AutoSize = True
        Me.lblIssCts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblIssCts.ForeColor = System.Drawing.Color.Black
        Me.lblIssCts.Location = New System.Drawing.Point(8, 52)
        Me.lblIssCts.Name = "lblIssCts"
        Me.lblIssCts.Size = New System.Drawing.Size(22, 13)
        Me.lblIssCts.TabIndex = 1
        Me.lblIssCts.Text = "Cts"
        '
        'txtIssCts
        '
        Me.txtIssCts.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtIssCts.Location = New System.Drawing.Point(40, 48)
        Me.txtIssCts.Name = "txtIssCts"
        Me.txtIssCts.Size = New System.Drawing.Size(60, 21)
        Me.txtIssCts.TabIndex = 1
        '
        'lblIssDate
        '
        Me.lblIssDate.AutoSize = True
        Me.lblIssDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblIssDate.ForeColor = System.Drawing.Color.Black
        Me.lblIssDate.Location = New System.Drawing.Point(694, 14)
        Me.lblIssDate.Name = "lblIssDate"
        Me.lblIssDate.Size = New System.Drawing.Size(30, 13)
        Me.lblIssDate.TabIndex = 2
        Me.lblIssDate.Text = "Date"
        '
        'txtIssDate
        '
        Me.txtIssDate.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtIssDate.Location = New System.Drawing.Point(694, 30)
        Me.txtIssDate.Name = "txtIssDate"
        Me.txtIssDate.Size = New System.Drawing.Size(90, 21)
        Me.txtIssDate.TabIndex = 2
        '
        'lblIssTime
        '
        Me.lblIssTime.AutoSize = True
        Me.lblIssTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblIssTime.ForeColor = System.Drawing.Color.Black
        Me.lblIssTime.Location = New System.Drawing.Point(790, 14)
        Me.lblIssTime.Name = "lblIssTime"
        Me.lblIssTime.Size = New System.Drawing.Size(30, 13)
        Me.lblIssTime.TabIndex = 3
        Me.lblIssTime.Text = "Time"
        '
        'txtIssTime
        '
        Me.txtIssTime.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtIssTime.Location = New System.Drawing.Point(790, 30)
        Me.txtIssTime.Name = "txtIssTime"
        Me.txtIssTime.Size = New System.Drawing.Size(90, 21)
        Me.txtIssTime.TabIndex = 3
        '
        'grpReturns
        '
        Me.grpReturns.Controls.Add(Me.lblRetPcs)
        Me.grpReturns.Controls.Add(Me.txtRetTap)
        Me.grpReturns.Controls.Add(Me.lblRetCts)
        Me.grpReturns.Controls.Add(Me.txtRetCts)
        Me.grpReturns.Controls.Add(Me.lblRejects)
        Me.grpReturns.Controls.Add(Me.lblRejPcs)
        Me.grpReturns.Controls.Add(Me.txtRej)
        Me.grpReturns.Controls.Add(Me.lblRejCts)
        Me.grpReturns.Controls.Add(Me.txtRejCts)
        Me.grpReturns.Controls.Add(Me.lblLost)
        Me.grpReturns.Controls.Add(Me.lblLostPcs)
        Me.grpReturns.Controls.Add(Me.txtLostPcs)
        Me.grpReturns.Controls.Add(Me.lblLostCts)
        Me.grpReturns.Controls.Add(Me.txtLostCts)
        Me.grpReturns.Controls.Add(Me.lblRepair)
        Me.grpReturns.Controls.Add(Me.lblRepPcs)
        Me.grpReturns.Controls.Add(Me.txtRepPcs)
        Me.grpReturns.Controls.Add(Me.lblRepCts)
        Me.grpReturns.Controls.Add(Me.txtRepCts)
        Me.grpReturns.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.grpReturns.ForeColor = System.Drawing.Color.DarkRed
        Me.grpReturns.Location = New System.Drawing.Point(8, 260)
        Me.grpReturns.Name = "grpReturns"
        Me.grpReturns.Size = New System.Drawing.Size(230, 229)
        Me.grpReturns.TabIndex = 11
        Me.grpReturns.TabStop = False
        Me.grpReturns.Text = "Returns"
        Me.grpReturns.Visible = False
        '
        'lblRetPcs
        '
        Me.lblRetPcs.AutoSize = True
        Me.lblRetPcs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblRetPcs.ForeColor = System.Drawing.Color.Black
        Me.lblRetPcs.Location = New System.Drawing.Point(8, 22)
        Me.lblRetPcs.Name = "lblRetPcs"
        Me.lblRetPcs.Size = New System.Drawing.Size(26, 13)
        Me.lblRetPcs.TabIndex = 0
        Me.lblRetPcs.Text = "PCs"
        '
        'txtRetTap
        '
        Me.txtRetTap.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtRetTap.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtRetTap.Location = New System.Drawing.Point(40, 18)
        Me.txtRetTap.Name = "txtRetTap"
        Me.txtRetTap.ReadOnly = True
        Me.txtRetTap.Size = New System.Drawing.Size(60, 21)
        Me.txtRetTap.TabIndex = 0
        '
        'lblRetCts
        '
        Me.lblRetCts.AutoSize = True
        Me.lblRetCts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblRetCts.ForeColor = System.Drawing.Color.Black
        Me.lblRetCts.Location = New System.Drawing.Point(8, 50)
        Me.lblRetCts.Name = "lblRetCts"
        Me.lblRetCts.Size = New System.Drawing.Size(22, 13)
        Me.lblRetCts.TabIndex = 1
        Me.lblRetCts.Text = "Cts"
        '
        'txtRetCts
        '
        Me.txtRetCts.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtRetCts.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtRetCts.Location = New System.Drawing.Point(40, 46)
        Me.txtRetCts.Name = "txtRetCts"
        Me.txtRetCts.ReadOnly = True
        Me.txtRetCts.Size = New System.Drawing.Size(60, 21)
        Me.txtRetCts.TabIndex = 1
        '
        'lblRejects
        '
        Me.lblRejects.AutoSize = True
        Me.lblRejects.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblRejects.ForeColor = System.Drawing.Color.Black
        Me.lblRejects.Location = New System.Drawing.Point(9, 81)
        Me.lblRejects.Name = "lblRejects"
        Me.lblRejects.Size = New System.Drawing.Size(50, 13)
        Me.lblRejects.TabIndex = 4
        Me.lblRejects.Text = "Rejects"
        '
        'lblRejPcs
        '
        Me.lblRejPcs.AutoSize = True
        Me.lblRejPcs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblRejPcs.ForeColor = System.Drawing.Color.Black
        Me.lblRejPcs.Location = New System.Drawing.Point(9, 101)
        Me.lblRejPcs.Name = "lblRejPcs"
        Me.lblRejPcs.Size = New System.Drawing.Size(26, 13)
        Me.lblRejPcs.TabIndex = 5
        Me.lblRejPcs.Text = "PCs"
        '
        'txtRej
        '
        Me.txtRej.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtRej.Location = New System.Drawing.Point(41, 97)
        Me.txtRej.Name = "txtRej"
        Me.txtRej.Size = New System.Drawing.Size(60, 21)
        Me.txtRej.TabIndex = 4
        '
        'lblRejCts
        '
        Me.lblRejCts.AutoSize = True
        Me.lblRejCts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblRejCts.ForeColor = System.Drawing.Color.Black
        Me.lblRejCts.Location = New System.Drawing.Point(9, 127)
        Me.lblRejCts.Name = "lblRejCts"
        Me.lblRejCts.Size = New System.Drawing.Size(22, 13)
        Me.lblRejCts.TabIndex = 6
        Me.lblRejCts.Text = "Cts"
        '
        'txtRejCts
        '
        Me.txtRejCts.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtRejCts.Location = New System.Drawing.Point(41, 123)
        Me.txtRejCts.Name = "txtRejCts"
        Me.txtRejCts.Size = New System.Drawing.Size(60, 21)
        Me.txtRejCts.TabIndex = 5
        '
        'lblLost
        '
        Me.lblLost.AutoSize = True
        Me.lblLost.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblLost.ForeColor = System.Drawing.Color.Black
        Me.lblLost.Location = New System.Drawing.Point(116, 81)
        Me.lblLost.Name = "lblLost"
        Me.lblLost.Size = New System.Drawing.Size(31, 13)
        Me.lblLost.TabIndex = 7
        Me.lblLost.Text = "Lost"
        '
        'lblLostPcs
        '
        Me.lblLostPcs.AutoSize = True
        Me.lblLostPcs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblLostPcs.ForeColor = System.Drawing.Color.Black
        Me.lblLostPcs.Location = New System.Drawing.Point(116, 101)
        Me.lblLostPcs.Name = "lblLostPcs"
        Me.lblLostPcs.Size = New System.Drawing.Size(26, 13)
        Me.lblLostPcs.TabIndex = 8
        Me.lblLostPcs.Text = "PCs"
        '
        'txtLostPcs
        '
        Me.txtLostPcs.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtLostPcs.Location = New System.Drawing.Point(146, 97)
        Me.txtLostPcs.Name = "txtLostPcs"
        Me.txtLostPcs.Size = New System.Drawing.Size(60, 21)
        Me.txtLostPcs.TabIndex = 6
        '
        'lblLostCts
        '
        Me.lblLostCts.AutoSize = True
        Me.lblLostCts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblLostCts.ForeColor = System.Drawing.Color.Black
        Me.lblLostCts.Location = New System.Drawing.Point(116, 127)
        Me.lblLostCts.Name = "lblLostCts"
        Me.lblLostCts.Size = New System.Drawing.Size(22, 13)
        Me.lblLostCts.TabIndex = 9
        Me.lblLostCts.Text = "Cts"
        '
        'txtLostCts
        '
        Me.txtLostCts.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtLostCts.Location = New System.Drawing.Point(146, 123)
        Me.txtLostCts.Name = "txtLostCts"
        Me.txtLostCts.Size = New System.Drawing.Size(60, 21)
        Me.txtLostCts.TabIndex = 7
        '
        'lblRepair
        '
        Me.lblRepair.AutoSize = True
        Me.lblRepair.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblRepair.ForeColor = System.Drawing.Color.Black
        Me.lblRepair.Location = New System.Drawing.Point(9, 155)
        Me.lblRepair.Name = "lblRepair"
        Me.lblRepair.Size = New System.Drawing.Size(44, 13)
        Me.lblRepair.TabIndex = 10
        Me.lblRepair.Text = "Repair"
        '
        'lblRepPcs
        '
        Me.lblRepPcs.AutoSize = True
        Me.lblRepPcs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblRepPcs.ForeColor = System.Drawing.Color.Black
        Me.lblRepPcs.Location = New System.Drawing.Point(9, 175)
        Me.lblRepPcs.Name = "lblRepPcs"
        Me.lblRepPcs.Size = New System.Drawing.Size(26, 13)
        Me.lblRepPcs.TabIndex = 11
        Me.lblRepPcs.Text = "PCs"
        '
        'txtRepPcs
        '
        Me.txtRepPcs.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtRepPcs.Location = New System.Drawing.Point(41, 171)
        Me.txtRepPcs.Name = "txtRepPcs"
        Me.txtRepPcs.Size = New System.Drawing.Size(60, 21)
        Me.txtRepPcs.TabIndex = 8
        '
        'lblRepCts
        '
        Me.lblRepCts.AutoSize = True
        Me.lblRepCts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblRepCts.ForeColor = System.Drawing.Color.Black
        Me.lblRepCts.Location = New System.Drawing.Point(9, 201)
        Me.lblRepCts.Name = "lblRepCts"
        Me.lblRepCts.Size = New System.Drawing.Size(22, 13)
        Me.lblRepCts.TabIndex = 12
        Me.lblRepCts.Text = "Cts"
        '
        'txtRepCts
        '
        Me.txtRepCts.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtRepCts.Location = New System.Drawing.Point(41, 197)
        Me.txtRepCts.Name = "txtRepCts"
        Me.txtRepCts.Size = New System.Drawing.Size(60, 21)
        Me.txtRepCts.TabIndex = 9
        '
        'lblRetDate
        '
        Me.lblRetDate.AutoSize = True
        Me.lblRetDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblRetDate.ForeColor = System.Drawing.Color.Black
        Me.lblRetDate.Location = New System.Drawing.Point(589, 72)
        Me.lblRetDate.Name = "lblRetDate"
        Me.lblRetDate.Size = New System.Drawing.Size(30, 13)
        Me.lblRetDate.TabIndex = 2
        Me.lblRetDate.Text = "Date"
        '
        'txtRetDate
        '
        Me.txtRetDate.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtRetDate.Location = New System.Drawing.Point(589, 88)
        Me.txtRetDate.Name = "txtRetDate"
        Me.txtRetDate.Size = New System.Drawing.Size(95, 21)
        Me.txtRetDate.TabIndex = 2
        '
        'lblRetTime
        '
        Me.lblRetTime.AutoSize = True
        Me.lblRetTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblRetTime.ForeColor = System.Drawing.Color.Black
        Me.lblRetTime.Location = New System.Drawing.Point(589, 118)
        Me.lblRetTime.Name = "lblRetTime"
        Me.lblRetTime.Size = New System.Drawing.Size(30, 13)
        Me.lblRetTime.TabIndex = 3
        Me.lblRetTime.Text = "Time"
        '
        'txtRetTime
        '
        Me.txtRetTime.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtRetTime.Location = New System.Drawing.Point(589, 134)
        Me.txtRetTime.Name = "txtRetTime"
        Me.txtRetTime.Size = New System.Drawing.Size(95, 21)
        Me.txtRetTime.TabIndex = 3
        '
        'grpSizingTypes
        '
        Me.grpSizingTypes.Controls.Add(Me.lblAssortment)
        Me.grpSizingTypes.Controls.Add(Me.txtAssortment)
        Me.grpSizingTypes.Controls.Add(Me.lblSizeRange)
        Me.grpSizingTypes.Controls.Add(Me.cmbSize)
        Me.grpSizingTypes.Controls.Add(Me.lblRetDate)
        Me.grpSizingTypes.Controls.Add(Me.lblTypePcs)
        Me.grpSizingTypes.Controls.Add(Me.txtTypePcs)
        Me.grpSizingTypes.Controls.Add(Me.lblTotPcs)
        Me.grpSizingTypes.Controls.Add(Me.txtRetDate)
        Me.grpSizingTypes.Controls.Add(Me.txtTotCts)
        Me.grpSizingTypes.Controls.Add(Me.lblTotCts)
        Me.grpSizingTypes.Controls.Add(Me.lblTypeCts)
        Me.grpSizingTypes.Controls.Add(Me.txtTotPcs)
        Me.grpSizingTypes.Controls.Add(Me.lblRetTime)
        Me.grpSizingTypes.Controls.Add(Me.txtTypeCts)
        Me.grpSizingTypes.Controls.Add(Me.btnAddType)
        Me.grpSizingTypes.Controls.Add(Me.txtRetTime)
        Me.grpSizingTypes.Controls.Add(Me.dgvGrid)
        Me.grpSizingTypes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.grpSizingTypes.Location = New System.Drawing.Point(244, 278)
        Me.grpSizingTypes.Name = "grpSizingTypes"
        Me.grpSizingTypes.Size = New System.Drawing.Size(792, 350)
        Me.grpSizingTypes.TabIndex = 12
        Me.grpSizingTypes.TabStop = False
        Me.grpSizingTypes.Text = "Sizing / Grading Types"
        Me.grpSizingTypes.Visible = False
        '
        'lblAssortment
        '
        Me.lblAssortment.AutoSize = True
        Me.lblAssortment.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblAssortment.Location = New System.Drawing.Point(8, 22)
        Me.lblAssortment.Name = "lblAssortment"
        Me.lblAssortment.Size = New System.Drawing.Size(59, 13)
        Me.lblAssortment.TabIndex = 0
        Me.lblAssortment.Text = "Assortment"
        '
        'txtAssortment
        '
        Me.txtAssortment.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtAssortment.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtAssortment.Location = New System.Drawing.Point(8, 38)
        Me.txtAssortment.Name = "txtAssortment"
        Me.txtAssortment.Size = New System.Drawing.Size(155, 21)
        Me.txtAssortment.TabIndex = 0
        '
        'lblSizeRange
        '
        Me.lblSizeRange.AutoSize = True
        Me.lblSizeRange.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblSizeRange.Location = New System.Drawing.Point(172, 22)
        Me.lblSizeRange.Name = "lblSizeRange"
        Me.lblSizeRange.Size = New System.Drawing.Size(62, 13)
        Me.lblSizeRange.TabIndex = 1
        Me.lblSizeRange.Text = "Size Range"
        '
        'cmbSize
        '
        Me.cmbSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSize.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmbSize.Location = New System.Drawing.Point(172, 38)
        Me.cmbSize.Name = "cmbSize"
        Me.cmbSize.Size = New System.Drawing.Size(100, 21)
        Me.cmbSize.TabIndex = 1
        '
        'lblTypePcs
        '
        Me.lblTypePcs.AutoSize = True
        Me.lblTypePcs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblTypePcs.Location = New System.Drawing.Point(282, 22)
        Me.lblTypePcs.Name = "lblTypePcs"
        Me.lblTypePcs.Size = New System.Drawing.Size(25, 13)
        Me.lblTypePcs.TabIndex = 2
        Me.lblTypePcs.Text = "Pcs"
        '
        'txtTypePcs
        '
        Me.txtTypePcs.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtTypePcs.Location = New System.Drawing.Point(282, 38)
        Me.txtTypePcs.Name = "txtTypePcs"
        Me.txtTypePcs.Size = New System.Drawing.Size(60, 21)
        Me.txtTypePcs.TabIndex = 2
        '
        'lblTotPcs
        '
        Me.lblTotPcs.AutoSize = True
        Me.lblTotPcs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblTotPcs.Location = New System.Drawing.Point(526, 314)
        Me.lblTotPcs.Name = "lblTotPcs"
        Me.lblTotPcs.Size = New System.Drawing.Size(55, 13)
        Me.lblTotPcs.TabIndex = 6
        Me.lblTotPcs.Text = "Total Pcs:"
        '
        'txtTotCts
        '
        Me.txtTotCts.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtTotCts.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtTotCts.Location = New System.Drawing.Point(716, 310)
        Me.txtTotCts.Name = "txtTotCts"
        Me.txtTotCts.ReadOnly = True
        Me.txtTotCts.Size = New System.Drawing.Size(70, 21)
        Me.txtTotCts.TabIndex = 7
        Me.txtTotCts.Text = "0"
        Me.txtTotCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblTotCts
        '
        Me.lblTotCts.AutoSize = True
        Me.lblTotCts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblTotCts.Location = New System.Drawing.Point(658, 314)
        Me.lblTotCts.Name = "lblTotCts"
        Me.lblTotCts.Size = New System.Drawing.Size(52, 13)
        Me.lblTotCts.TabIndex = 7
        Me.lblTotCts.Text = "Total Cts:"
        '
        'lblTypeCts
        '
        Me.lblTypeCts.AutoSize = True
        Me.lblTypeCts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblTypeCts.Location = New System.Drawing.Point(352, 22)
        Me.lblTypeCts.Name = "lblTypeCts"
        Me.lblTypeCts.Size = New System.Drawing.Size(22, 13)
        Me.lblTypeCts.TabIndex = 3
        Me.lblTypeCts.Text = "Cts"
        '
        'txtTotPcs
        '
        Me.txtTotPcs.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtTotPcs.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtTotPcs.Location = New System.Drawing.Point(583, 310)
        Me.txtTotPcs.Name = "txtTotPcs"
        Me.txtTotPcs.ReadOnly = True
        Me.txtTotPcs.Size = New System.Drawing.Size(60, 21)
        Me.txtTotPcs.TabIndex = 6
        Me.txtTotPcs.Text = "0"
        Me.txtTotPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTypeCts
        '
        Me.txtTypeCts.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtTypeCts.Location = New System.Drawing.Point(352, 38)
        Me.txtTypeCts.Name = "txtTypeCts"
        Me.txtTypeCts.Size = New System.Drawing.Size(65, 21)
        Me.txtTypeCts.TabIndex = 3
        '
        'btnAddType
        '
        Me.btnAddType.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnAddType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnAddType.Location = New System.Drawing.Point(665, 35)
        Me.btnAddType.Name = "btnAddType"
        Me.btnAddType.Size = New System.Drawing.Size(80, 24)
        Me.btnAddType.TabIndex = 4
        Me.btnAddType.Text = "Add"
        Me.btnAddType.UseVisualStyleBackColor = False
        '
        'dgvGrid
        '
        Me.dgvGrid.AllowUserToAddRows = False
        Me.dgvGrid.AllowUserToDeleteRows = False
        Me.dgvGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvGrid.BackgroundColor = System.Drawing.Color.White
        Me.dgvGrid.ColumnHeadersHeight = 26
        Me.dgvGrid.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.dgvGrid.Location = New System.Drawing.Point(8, 70)
        Me.dgvGrid.Name = "dgvGrid"
        Me.dgvGrid.RowHeadersVisible = False
        Me.dgvGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvGrid.Size = New System.Drawing.Size(516, 274)
        Me.dgvGrid.TabIndex = 5
        '
        'grpEmp
        '
        Me.grpEmp.Controls.Add(Me.lblEmpNo)
        Me.grpEmp.Controls.Add(Me.txtEmpNo)
        Me.grpEmp.Controls.Add(Me.lblEmpPcs)
        Me.grpEmp.Controls.Add(Me.txtEmpPcs)
        Me.grpEmp.Controls.Add(Me.btnEmpAdd)
        Me.grpEmp.Controls.Add(Me.dgvEmp)
        Me.grpEmp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.grpEmp.Location = New System.Drawing.Point(8, 495)
        Me.grpEmp.Name = "grpEmp"
        Me.grpEmp.Size = New System.Drawing.Size(206, 178)
        Me.grpEmp.TabIndex = 13
        Me.grpEmp.TabStop = False
        Me.grpEmp.Text = "Employees"
        Me.grpEmp.Visible = False
        '
        'lblEmpNo
        '
        Me.lblEmpNo.AutoSize = True
        Me.lblEmpNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblEmpNo.Location = New System.Drawing.Point(8, 22)
        Me.lblEmpNo.Name = "lblEmpNo"
        Me.lblEmpNo.Size = New System.Drawing.Size(45, 13)
        Me.lblEmpNo.TabIndex = 0
        Me.lblEmpNo.Text = "Emp No"
        '
        'txtEmpNo
        '
        Me.txtEmpNo.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtEmpNo.Location = New System.Drawing.Point(8, 38)
        Me.txtEmpNo.MaxLength = 6
        Me.txtEmpNo.Name = "txtEmpNo"
        Me.txtEmpNo.Size = New System.Drawing.Size(65, 21)
        Me.txtEmpNo.TabIndex = 0
        '
        'lblEmpPcs
        '
        Me.lblEmpPcs.AutoSize = True
        Me.lblEmpPcs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblEmpPcs.Location = New System.Drawing.Point(80, 22)
        Me.lblEmpPcs.Name = "lblEmpPcs"
        Me.lblEmpPcs.Size = New System.Drawing.Size(26, 13)
        Me.lblEmpPcs.TabIndex = 1
        Me.lblEmpPcs.Text = "PCs"
        '
        'txtEmpPcs
        '
        Me.txtEmpPcs.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.txtEmpPcs.Location = New System.Drawing.Point(80, 38)
        Me.txtEmpPcs.Name = "txtEmpPcs"
        Me.txtEmpPcs.Size = New System.Drawing.Size(50, 21)
        Me.txtEmpPcs.TabIndex = 1
        '
        'btnEmpAdd
        '
        Me.btnEmpAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnEmpAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEmpAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnEmpAdd.Location = New System.Drawing.Point(136, 35)
        Me.btnEmpAdd.Name = "btnEmpAdd"
        Me.btnEmpAdd.Size = New System.Drawing.Size(56, 24)
        Me.btnEmpAdd.TabIndex = 2
        Me.btnEmpAdd.Text = "Add"
        Me.btnEmpAdd.UseVisualStyleBackColor = False
        '
        'dgvEmp
        '
        Me.dgvEmp.AllowUserToAddRows = False
        Me.dgvEmp.BackgroundColor = System.Drawing.Color.White
        Me.dgvEmp.ColumnHeadersHeight = 26
        Me.dgvEmp.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.dgvEmp.Location = New System.Drawing.Point(8, 68)
        Me.dgvEmp.Name = "dgvEmp"
        Me.dgvEmp.RowHeadersVisible = False
        Me.dgvEmp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvEmp.Size = New System.Drawing.Size(173, 101)
        Me.dgvEmp.TabIndex = 3
        '
        'pnlControl
        '
        Me.pnlControl.Location = New System.Drawing.Point(0, 0)
        Me.pnlControl.Name = "pnlControl"
        Me.pnlControl.Size = New System.Drawing.Size(200, 100)
        Me.pnlControl.TabIndex = 14
        '
        'btnFirst
        '
        Me.btnFirst.Location = New System.Drawing.Point(0, 0)
        Me.btnFirst.Name = "btnFirst"
        Me.btnFirst.Size = New System.Drawing.Size(75, 23)
        Me.btnFirst.TabIndex = 0
        '
        'btnPrevious
        '
        Me.btnPrevious.Location = New System.Drawing.Point(0, 0)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(75, 23)
        Me.btnPrevious.TabIndex = 0
        '
        'txtRecordCount
        '
        Me.txtRecordCount.Location = New System.Drawing.Point(0, 0)
        Me.txtRecordCount.Name = "txtRecordCount"
        Me.txtRecordCount.Size = New System.Drawing.Size(100, 20)
        Me.txtRecordCount.TabIndex = 0
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(0, 0)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(75, 23)
        Me.btnNext.TabIndex = 0
        '
        'btnLast
        '
        Me.btnLast.Location = New System.Drawing.Point(0, 0)
        Me.btnLast.Name = "btnLast"
        Me.btnLast.Size = New System.Drawing.Size(75, 23)
        Me.btnLast.TabIndex = 0
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(0, 0)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 23)
        Me.btnNew.TabIndex = 0
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(0, 0)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(75, 23)
        Me.btnEdit.TabIndex = 0
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(0, 0)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 0
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(0, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 0
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(0, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(0, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 0
        '
        'frm_GRDRnd_Sizing
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1048, 675)
        Me.Controls.Add(Me.pnlTitle)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.grpIssues)
        Me.Controls.Add(Me.grpReturns)
        Me.Controls.Add(Me.grpSizingTypes)
        Me.Controls.Add(Me.grpEmp)
        Me.Controls.Add(Me.pnlControl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_GRDRnd_Sizing"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sizing Issues & Returns"
        Me.pnlTitle.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.grpIssues.ResumeLayout(False)
        Me.grpIssues.PerformLayout()
        Me.grpReturns.ResumeLayout(False)
        Me.grpReturns.PerformLayout()
        Me.grpSizingTypes.ResumeLayout(False)
        Me.grpSizingTypes.PerformLayout()
        CType(Me.dgvGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpEmp.ResumeLayout(False)
        Me.grpEmp.PerformLayout()
        CType(Me.dgvEmp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    ' ---- Control Declarations ----
    Friend WithEvents pnlTitle As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblDept As System.Windows.Forms.Label
    Friend WithEvents cmbDept As System.Windows.Forms.ComboBox
    Friend WithEvents lblSection As System.Windows.Forms.Label
    Friend WithEvents cmbSection As System.Windows.Forms.ComboBox
    Friend WithEvents lblParcelNo As System.Windows.Forms.Label
    Friend WithEvents txtParNo As System.Windows.Forms.TextBox
    Friend WithEvents lblPacketNo As System.Windows.Forms.Label
    Friend WithEvents txtPktNo As System.Windows.Forms.TextBox
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Friend WithEvents txtType1 As System.Windows.Forms.TextBox
    Friend WithEvents lblPacketType As System.Windows.Forms.Label
    Friend WithEvents txtPktType As System.Windows.Forms.TextBox
    Friend WithEvents btnEmpNo As System.Windows.Forms.Button
    Friend WithEvents txtemp As System.Windows.Forms.TextBox
    Friend WithEvents chkNew As System.Windows.Forms.CheckBox
    Friend WithEvents grpIssues As System.Windows.Forms.GroupBox
    Friend WithEvents lblIssPcs As System.Windows.Forms.Label
    Friend WithEvents txtIssTap As System.Windows.Forms.TextBox
    Friend WithEvents lblIssCts As System.Windows.Forms.Label
    Friend WithEvents txtIssCts As System.Windows.Forms.TextBox
    Friend WithEvents lblIssDate As System.Windows.Forms.Label
    Friend WithEvents txtIssDate As System.Windows.Forms.TextBox
    Friend WithEvents lblIssTime As System.Windows.Forms.Label
    Friend WithEvents txtIssTime As System.Windows.Forms.TextBox
    Friend WithEvents grpReturns As System.Windows.Forms.GroupBox
    Friend WithEvents lblRetPcs As System.Windows.Forms.Label
    Friend WithEvents txtRetTap As System.Windows.Forms.TextBox
    Friend WithEvents lblRetCts As System.Windows.Forms.Label
    Friend WithEvents txtRetCts As System.Windows.Forms.TextBox
    Friend WithEvents lblRetDate As System.Windows.Forms.Label
    Friend WithEvents txtRetDate As System.Windows.Forms.TextBox
    Friend WithEvents lblRetTime As System.Windows.Forms.Label
    Friend WithEvents txtRetTime As System.Windows.Forms.TextBox
    Friend WithEvents lblRejects As System.Windows.Forms.Label
    Friend WithEvents lblRejPcs As System.Windows.Forms.Label
    Friend WithEvents txtRej As System.Windows.Forms.TextBox
    Friend WithEvents lblRejCts As System.Windows.Forms.Label
    Friend WithEvents txtRejCts As System.Windows.Forms.TextBox
    Friend WithEvents lblLost As System.Windows.Forms.Label
    Friend WithEvents lblLostPcs As System.Windows.Forms.Label
    Friend WithEvents txtLostPcs As System.Windows.Forms.TextBox
    Friend WithEvents lblLostCts As System.Windows.Forms.Label
    Friend WithEvents txtLostCts As System.Windows.Forms.TextBox
    Friend WithEvents lblRepair As System.Windows.Forms.Label
    Friend WithEvents lblRepPcs As System.Windows.Forms.Label
    Friend WithEvents txtRepPcs As System.Windows.Forms.TextBox
    Friend WithEvents lblRepCts As System.Windows.Forms.Label
    Friend WithEvents txtRepCts As System.Windows.Forms.TextBox
    Friend WithEvents grpSizingTypes As System.Windows.Forms.GroupBox
    Friend WithEvents lblAssortment As System.Windows.Forms.Label
    Friend WithEvents txtAssortment As System.Windows.Forms.TextBox
    Friend WithEvents lblSizeRange As System.Windows.Forms.Label
    Friend WithEvents cmbSize As System.Windows.Forms.ComboBox
    Friend WithEvents lblTypePcs As System.Windows.Forms.Label
    Friend WithEvents txtTypePcs As System.Windows.Forms.TextBox
    Friend WithEvents lblTypeCts As System.Windows.Forms.Label
    Friend WithEvents txtTypeCts As System.Windows.Forms.TextBox
    Friend WithEvents btnAddType As System.Windows.Forms.Button
    Friend WithEvents dgvGrid As System.Windows.Forms.DataGridView
    Friend WithEvents lblTotPcs As System.Windows.Forms.Label
    Friend WithEvents txtTotPcs As System.Windows.Forms.TextBox
    Friend WithEvents lblTotCts As System.Windows.Forms.Label
    Friend WithEvents txtTotCts As System.Windows.Forms.TextBox
    Friend WithEvents grpEmp As System.Windows.Forms.GroupBox
    Friend WithEvents lblEmpNo As System.Windows.Forms.Label
    Friend WithEvents txtEmpNo As System.Windows.Forms.TextBox
    Friend WithEvents lblEmpPcs As System.Windows.Forms.Label
    Friend WithEvents txtEmpPcs As System.Windows.Forms.TextBox
    Friend WithEvents btnEmpAdd As System.Windows.Forms.Button
    Friend WithEvents dgvEmp As System.Windows.Forms.DataGridView
    Friend WithEvents pnlControl As System.Windows.Forms.Panel
    Friend WithEvents btnFirst As System.Windows.Forms.Button
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents txtRecordCount As System.Windows.Forms.TextBox
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnLast As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Button1 As Button
End Class
