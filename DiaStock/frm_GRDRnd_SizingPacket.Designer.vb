<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_GRDRnd_SizingPacket
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_GRDRnd_SizingPacket))
        Me.pnlTitle = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlToolbar = New System.Windows.Forms.Panel()
        Me.btnexit1 = New System.Windows.Forms.Button()
        Me.btnexport = New System.Windows.Forms.Button()
        Me.btnsave1 = New System.Windows.Forms.Button()
        Me.btnnew1 = New System.Windows.Forms.Button()
        Me.txtRecordCount = New System.Windows.Forms.TextBox()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnExcel = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.pnlEntry = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtParNo = New System.Windows.Forms.TextBox()
        Me.txtPktNo = New System.Windows.Forms.TextBox()
        Me.cmbSizeType = New System.Windows.Forms.ComboBox()
        Me.cmbType1 = New System.Windows.Forms.ComboBox()
        Me.cmbType2 = New System.Windows.Forms.ComboBox()
        Me.cmbType3 = New System.Windows.Forms.ComboBox()
        Me.cmbType4 = New System.Windows.Forms.ComboBox()
        Me.txtPktPcs = New System.Windows.Forms.TextBox()
        Me.txtPktCts = New System.Windows.Forms.TextBox()
        Me.lblBalPcs = New System.Windows.Forms.Label()
        Me.txtBalPcs = New System.Windows.Forms.TextBox()
        Me.lblBalCts = New System.Windows.Forms.Label()
        Me.txtBalCts = New System.Windows.Forms.TextBox()
        Me.lblPktType = New System.Windows.Forms.Label()
        Me.cmbPktType = New System.Windows.Forms.ComboBox()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.lblActPcs = New System.Windows.Forms.Label()
        Me.txtActPcs = New System.Windows.Forms.TextBox()
        Me.lblActCts = New System.Windows.Forms.Label()
        Me.txtActCts = New System.Windows.Forms.TextBox()
        Me.lblParNo = New System.Windows.Forms.Label()
        Me.lblPktNo = New System.Windows.Forms.Label()
        Me.lblSizeCode = New System.Windows.Forms.Label()
        Me.lblColor = New System.Windows.Forms.Label()
        Me.lblMake = New System.Windows.Forms.Label()
        Me.lblFluor = New System.Windows.Forms.Label()
        Me.lblClarity = New System.Windows.Forms.Label()
        Me.lblPcs = New System.Windows.Forms.Label()
        Me.lblCts = New System.Windows.Forms.Label()
        Me.lblFinPcs = New System.Windows.Forms.Label()
        Me.lblTotPcs = New System.Windows.Forms.Label()
        Me.txtTotPcs = New System.Windows.Forms.TextBox()
        Me.lblTotCts = New System.Windows.Forms.Label()
        Me.txtTotCts = New System.Windows.Forms.TextBox()
        Me.lblTPktPcs = New System.Windows.Forms.Label()
        Me.txtTPktPcs = New System.Windows.Forms.TextBox()
        Me.lblTPktCts = New System.Windows.Forms.Label()
        Me.txtTPktCts = New System.Windows.Forms.TextBox()
        Me.flxType = New System.Windows.Forms.DataGridView()
        Me.flxSelect = New System.Windows.Forms.DataGridView()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.lblGridType = New System.Windows.Forms.Label()
        Me.lblGridSelect = New System.Windows.Forms.Label()
        Me.lblGridDetails = New System.Windows.Forms.Label()
        Me.pnlTitle.SuspendLayout()
        Me.pnlToolbar.SuspendLayout()
        Me.pnlEntry.SuspendLayout()
        CType(Me.flxType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlTitle
        '
        Me.pnlTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.pnlTitle.Controls.Add(Me.lblTitle)
        Me.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTitle.Location = New System.Drawing.Point(0, 0)
        Me.pnlTitle.Name = "pnlTitle"
        Me.pnlTitle.Size = New System.Drawing.Size(1101, 35)
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
        Me.lblTitle.Size = New System.Drawing.Size(1101, 35)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Sizing Packet Entry"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlToolbar
        '
        Me.pnlToolbar.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.pnlToolbar.Controls.Add(Me.btnexit1)
        Me.pnlToolbar.Controls.Add(Me.btnexport)
        Me.pnlToolbar.Controls.Add(Me.btnsave1)
        Me.pnlToolbar.Controls.Add(Me.btnnew1)
        Me.pnlToolbar.Controls.Add(Me.txtRecordCount)
        Me.pnlToolbar.Location = New System.Drawing.Point(0, 35)
        Me.pnlToolbar.Name = "pnlToolbar"
        Me.pnlToolbar.Size = New System.Drawing.Size(1090, 53)
        Me.pnlToolbar.TabIndex = 1
        '
        'btnexit1
        '
        Me.btnexit1.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnexit1.Location = New System.Drawing.Point(395, 6)
        Me.btnexit1.Name = "btnexit1"
        Me.btnexit1.Size = New System.Drawing.Size(99, 34)
        Me.btnexit1.TabIndex = 30
        Me.btnexit1.Text = "Exit"
        Me.btnexit1.UseVisualStyleBackColor = False
        '
        'btnexport
        '
        Me.btnexport.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnexport.Location = New System.Drawing.Point(276, 6)
        Me.btnexport.Name = "btnexport"
        Me.btnexport.Size = New System.Drawing.Size(99, 34)
        Me.btnexport.TabIndex = 29
        Me.btnexport.Text = "Export"
        Me.btnexport.UseVisualStyleBackColor = False
        '
        'btnsave1
        '
        Me.btnsave1.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnsave1.Location = New System.Drawing.Point(156, 6)
        Me.btnsave1.Name = "btnsave1"
        Me.btnsave1.Size = New System.Drawing.Size(99, 34)
        Me.btnsave1.TabIndex = 28
        Me.btnsave1.Text = "Save"
        Me.btnsave1.UseVisualStyleBackColor = False
        '
        'btnnew1
        '
        Me.btnnew1.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnnew1.Location = New System.Drawing.Point(40, 6)
        Me.btnnew1.Name = "btnnew1"
        Me.btnnew1.Size = New System.Drawing.Size(99, 34)
        Me.btnnew1.TabIndex = 27
        Me.btnnew1.Text = "New"
        Me.btnnew1.UseVisualStyleBackColor = False
        '
        'txtRecordCount
        '
        Me.txtRecordCount.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.txtRecordCount.Location = New System.Drawing.Point(834, 12)
        Me.txtRecordCount.Name = "txtRecordCount"
        Me.txtRecordCount.ReadOnly = True
        Me.txtRecordCount.Size = New System.Drawing.Size(150, 20)
        Me.txtRecordCount.TabIndex = 0
        Me.txtRecordCount.Text = "Record Count"
        Me.txtRecordCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(0, 0)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 23)
        Me.btnNew.TabIndex = 0
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(0, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 0
        '
        'btnExcel
        '
        Me.btnExcel.Location = New System.Drawing.Point(0, 0)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(75, 23)
        Me.btnExcel.TabIndex = 0
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(0, 0)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 0
        '
        'pnlEntry
        '
        Me.pnlEntry.BackColor = System.Drawing.Color.White
        Me.pnlEntry.Controls.Add(Me.Label9)
        Me.pnlEntry.Controls.Add(Me.Label8)
        Me.pnlEntry.Controls.Add(Me.Label7)
        Me.pnlEntry.Controls.Add(Me.Label6)
        Me.pnlEntry.Controls.Add(Me.Label5)
        Me.pnlEntry.Controls.Add(Me.Label4)
        Me.pnlEntry.Controls.Add(Me.Label3)
        Me.pnlEntry.Controls.Add(Me.Label2)
        Me.pnlEntry.Controls.Add(Me.Label1)
        Me.pnlEntry.Controls.Add(Me.txtParNo)
        Me.pnlEntry.Controls.Add(Me.txtPktNo)
        Me.pnlEntry.Controls.Add(Me.cmbSizeType)
        Me.pnlEntry.Controls.Add(Me.cmbType1)
        Me.pnlEntry.Controls.Add(Me.cmbType2)
        Me.pnlEntry.Controls.Add(Me.cmbType3)
        Me.pnlEntry.Controls.Add(Me.cmbType4)
        Me.pnlEntry.Controls.Add(Me.txtPktPcs)
        Me.pnlEntry.Controls.Add(Me.txtPktCts)
        Me.pnlEntry.Controls.Add(Me.lblBalPcs)
        Me.pnlEntry.Controls.Add(Me.txtBalPcs)
        Me.pnlEntry.Controls.Add(Me.lblBalCts)
        Me.pnlEntry.Controls.Add(Me.txtBalCts)
        Me.pnlEntry.Controls.Add(Me.lblPktType)
        Me.pnlEntry.Controls.Add(Me.cmbPktType)
        Me.pnlEntry.Controls.Add(Me.cmdAdd)
        Me.pnlEntry.Location = New System.Drawing.Point(0, 75)
        Me.pnlEntry.Name = "pnlEntry"
        Me.pnlEntry.Size = New System.Drawing.Size(1090, 126)
        Me.pnlEntry.TabIndex = 2
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(964, 38)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 18)
        Me.Label9.TabIndex = 28
        Me.Label9.Text = "Cts"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(884, 38)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(70, 18)
        Me.Label8.TabIndex = 27
        Me.Label8.Text = "PCs"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(700, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(35, 13)
        Me.Label7.TabIndex = 26
        Me.Label7.Text = "Clarity"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(560, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 13)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "Fluorescent"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(440, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(34, 13)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "Make"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(322, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(31, 13)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "Color"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(200, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "Size Code"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(135, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Pkt No"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Parcel No"
        '
        'txtParNo
        '
        Me.txtParNo.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.txtParNo.Location = New System.Drawing.Point(5, 32)
        Me.txtParNo.Name = "txtParNo"
        Me.txtParNo.Size = New System.Drawing.Size(120, 20)
        Me.txtParNo.TabIndex = 0
        '
        'txtPktNo
        '
        Me.txtPktNo.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.txtPktNo.Location = New System.Drawing.Point(135, 32)
        Me.txtPktNo.Name = "txtPktNo"
        Me.txtPktNo.ReadOnly = True
        Me.txtPktNo.Size = New System.Drawing.Size(55, 20)
        Me.txtPktNo.TabIndex = 1
        '
        'cmbSizeType
        '
        Me.cmbSizeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSizeType.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.cmbSizeType.Location = New System.Drawing.Point(203, 32)
        Me.cmbSizeType.Name = "cmbSizeType"
        Me.cmbSizeType.Size = New System.Drawing.Size(110, 24)
        Me.cmbSizeType.TabIndex = 2
        '
        'cmbType1
        '
        Me.cmbType1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbType1.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.cmbType1.Location = New System.Drawing.Point(319, 32)
        Me.cmbType1.Name = "cmbType1"
        Me.cmbType1.Size = New System.Drawing.Size(110, 24)
        Me.cmbType1.TabIndex = 3
        '
        'cmbType2
        '
        Me.cmbType2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbType2.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.cmbType2.Location = New System.Drawing.Point(435, 32)
        Me.cmbType2.Name = "cmbType2"
        Me.cmbType2.Size = New System.Drawing.Size(110, 24)
        Me.cmbType2.TabIndex = 4
        '
        'cmbType3
        '
        Me.cmbType3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbType3.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.cmbType3.Location = New System.Drawing.Point(563, 32)
        Me.cmbType3.Name = "cmbType3"
        Me.cmbType3.Size = New System.Drawing.Size(130, 24)
        Me.cmbType3.TabIndex = 5
        '
        'cmbType4
        '
        Me.cmbType4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbType4.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.cmbType4.Location = New System.Drawing.Point(700, 32)
        Me.cmbType4.Name = "cmbType4"
        Me.cmbType4.Size = New System.Drawing.Size(125, 24)
        Me.cmbType4.TabIndex = 6
        '
        'txtPktPcs
        '
        Me.txtPktPcs.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.txtPktPcs.Location = New System.Drawing.Point(884, 59)
        Me.txtPktPcs.Name = "txtPktPcs"
        Me.txtPktPcs.Size = New System.Drawing.Size(60, 20)
        Me.txtPktPcs.TabIndex = 7
        '
        'txtPktCts
        '
        Me.txtPktCts.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.txtPktCts.Location = New System.Drawing.Point(964, 59)
        Me.txtPktCts.Name = "txtPktCts"
        Me.txtPktCts.Size = New System.Drawing.Size(70, 20)
        Me.txtPktCts.TabIndex = 8
        '
        'lblBalPcs
        '
        Me.lblBalPcs.BackColor = System.Drawing.Color.Transparent
        Me.lblBalPcs.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblBalPcs.ForeColor = System.Drawing.Color.Black
        Me.lblBalPcs.Location = New System.Drawing.Point(884, 83)
        Me.lblBalPcs.Name = "lblBalPcs"
        Me.lblBalPcs.Size = New System.Drawing.Size(70, 18)
        Me.lblBalPcs.TabIndex = 9
        Me.lblBalPcs.Text = "Bal. PCs"
        '
        'txtBalPcs
        '
        Me.txtBalPcs.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.txtBalPcs.Location = New System.Drawing.Point(884, 102)
        Me.txtBalPcs.Name = "txtBalPcs"
        Me.txtBalPcs.ReadOnly = True
        Me.txtBalPcs.Size = New System.Drawing.Size(60, 20)
        Me.txtBalPcs.TabIndex = 10
        '
        'lblBalCts
        '
        Me.lblBalCts.BackColor = System.Drawing.Color.Transparent
        Me.lblBalCts.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblBalCts.ForeColor = System.Drawing.Color.Black
        Me.lblBalCts.Location = New System.Drawing.Point(964, 83)
        Me.lblBalCts.Name = "lblBalCts"
        Me.lblBalCts.Size = New System.Drawing.Size(70, 18)
        Me.lblBalCts.TabIndex = 11
        Me.lblBalCts.Text = "Bal. Cts"
        '
        'txtBalCts
        '
        Me.txtBalCts.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.txtBalCts.Location = New System.Drawing.Point(964, 102)
        Me.txtBalCts.Name = "txtBalCts"
        Me.txtBalCts.ReadOnly = True
        Me.txtBalCts.Size = New System.Drawing.Size(70, 20)
        Me.txtBalCts.TabIndex = 12
        '
        'lblPktType
        '
        Me.lblPktType.BackColor = System.Drawing.Color.Transparent
        Me.lblPktType.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblPktType.ForeColor = System.Drawing.Color.Black
        Me.lblPktType.Location = New System.Drawing.Point(580, 62)
        Me.lblPktType.Name = "lblPktType"
        Me.lblPktType.Size = New System.Drawing.Size(70, 18)
        Me.lblPktType.TabIndex = 17
        Me.lblPktType.Text = "Pkt Type"
        '
        'cmbPktType
        '
        Me.cmbPktType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPktType.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.cmbPktType.Location = New System.Drawing.Point(583, 86)
        Me.cmbPktType.Name = "cmbPktType"
        Me.cmbPktType.Size = New System.Drawing.Size(60, 24)
        Me.cmbPktType.TabIndex = 18
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdAdd.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.cmdAdd.Location = New System.Drawing.Point(700, 68)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(100, 33)
        Me.cmdAdd.TabIndex = 19
        Me.cmdAdd.Text = "<< Add >>"
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'lblActPcs
        '
        Me.lblActPcs.BackColor = System.Drawing.Color.Transparent
        Me.lblActPcs.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblActPcs.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblActPcs.Location = New System.Drawing.Point(750, 360)
        Me.lblActPcs.Name = "lblActPcs"
        Me.lblActPcs.Size = New System.Drawing.Size(70, 18)
        Me.lblActPcs.TabIndex = 13
        Me.lblActPcs.Text = "Act. PCs"
        '
        'txtActPcs
        '
        Me.txtActPcs.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.txtActPcs.Location = New System.Drawing.Point(826, 358)
        Me.txtActPcs.Name = "txtActPcs"
        Me.txtActPcs.ReadOnly = True
        Me.txtActPcs.Size = New System.Drawing.Size(60, 20)
        Me.txtActPcs.TabIndex = 14
        Me.txtActPcs.Text = "0"
        '
        'lblActCts
        '
        Me.lblActCts.BackColor = System.Drawing.Color.Transparent
        Me.lblActCts.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblActCts.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblActCts.Location = New System.Drawing.Point(914, 358)
        Me.lblActCts.Name = "lblActCts"
        Me.lblActCts.Size = New System.Drawing.Size(70, 18)
        Me.lblActCts.TabIndex = 15
        Me.lblActCts.Text = "Act. Cts"
        '
        'txtActCts
        '
        Me.txtActCts.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.txtActCts.Location = New System.Drawing.Point(990, 358)
        Me.txtActCts.Name = "txtActCts"
        Me.txtActCts.ReadOnly = True
        Me.txtActCts.Size = New System.Drawing.Size(70, 20)
        Me.txtActCts.TabIndex = 16
        Me.txtActCts.Text = "0"
        '
        'lblParNo
        '
        Me.lblParNo.Location = New System.Drawing.Point(0, 0)
        Me.lblParNo.Name = "lblParNo"
        Me.lblParNo.Size = New System.Drawing.Size(100, 23)
        Me.lblParNo.TabIndex = 0
        '
        'lblPktNo
        '
        Me.lblPktNo.Location = New System.Drawing.Point(0, 0)
        Me.lblPktNo.Name = "lblPktNo"
        Me.lblPktNo.Size = New System.Drawing.Size(100, 23)
        Me.lblPktNo.TabIndex = 0
        '
        'lblSizeCode
        '
        Me.lblSizeCode.Location = New System.Drawing.Point(0, 0)
        Me.lblSizeCode.Name = "lblSizeCode"
        Me.lblSizeCode.Size = New System.Drawing.Size(100, 23)
        Me.lblSizeCode.TabIndex = 0
        '
        'lblColor
        '
        Me.lblColor.Location = New System.Drawing.Point(0, 0)
        Me.lblColor.Name = "lblColor"
        Me.lblColor.Size = New System.Drawing.Size(100, 23)
        Me.lblColor.TabIndex = 0
        '
        'lblMake
        '
        Me.lblMake.Location = New System.Drawing.Point(0, 0)
        Me.lblMake.Name = "lblMake"
        Me.lblMake.Size = New System.Drawing.Size(100, 23)
        Me.lblMake.TabIndex = 0
        '
        'lblFluor
        '
        Me.lblFluor.Location = New System.Drawing.Point(0, 0)
        Me.lblFluor.Name = "lblFluor"
        Me.lblFluor.Size = New System.Drawing.Size(100, 23)
        Me.lblFluor.TabIndex = 0
        '
        'lblClarity
        '
        Me.lblClarity.Location = New System.Drawing.Point(0, 0)
        Me.lblClarity.Name = "lblClarity"
        Me.lblClarity.Size = New System.Drawing.Size(100, 23)
        Me.lblClarity.TabIndex = 0
        '
        'lblPcs
        '
        Me.lblPcs.Location = New System.Drawing.Point(0, 0)
        Me.lblPcs.Name = "lblPcs"
        Me.lblPcs.Size = New System.Drawing.Size(100, 23)
        Me.lblPcs.TabIndex = 0
        '
        'lblCts
        '
        Me.lblCts.Location = New System.Drawing.Point(0, 0)
        Me.lblCts.Name = "lblCts"
        Me.lblCts.Size = New System.Drawing.Size(100, 23)
        Me.lblCts.TabIndex = 0
        '
        'lblFinPcs
        '
        Me.lblFinPcs.Location = New System.Drawing.Point(0, 0)
        Me.lblFinPcs.Name = "lblFinPcs"
        Me.lblFinPcs.Size = New System.Drawing.Size(100, 23)
        Me.lblFinPcs.TabIndex = 0
        '
        'lblTotPcs
        '
        Me.lblTotPcs.BackColor = System.Drawing.Color.Transparent
        Me.lblTotPcs.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTotPcs.Location = New System.Drawing.Point(9, 620)
        Me.lblTotPcs.Name = "lblTotPcs"
        Me.lblTotPcs.Size = New System.Drawing.Size(65, 18)
        Me.lblTotPcs.TabIndex = 9
        Me.lblTotPcs.Text = "Tot Pcs"
        '
        'txtTotPcs
        '
        Me.txtTotPcs.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.txtTotPcs.Location = New System.Drawing.Point(79, 618)
        Me.txtTotPcs.Name = "txtTotPcs"
        Me.txtTotPcs.ReadOnly = True
        Me.txtTotPcs.Size = New System.Drawing.Size(60, 20)
        Me.txtTotPcs.TabIndex = 10
        Me.txtTotPcs.Text = "0"
        '
        'lblTotCts
        '
        Me.lblTotCts.BackColor = System.Drawing.Color.Transparent
        Me.lblTotCts.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTotCts.Location = New System.Drawing.Point(149, 620)
        Me.lblTotCts.Name = "lblTotCts"
        Me.lblTotCts.Size = New System.Drawing.Size(65, 18)
        Me.lblTotCts.TabIndex = 11
        Me.lblTotCts.Text = "Tot Cts"
        '
        'txtTotCts
        '
        Me.txtTotCts.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.txtTotCts.Location = New System.Drawing.Point(219, 618)
        Me.txtTotCts.Name = "txtTotCts"
        Me.txtTotCts.ReadOnly = True
        Me.txtTotCts.Size = New System.Drawing.Size(70, 20)
        Me.txtTotCts.TabIndex = 12
        Me.txtTotCts.Text = "0"
        '
        'lblTPktPcs
        '
        Me.lblTPktPcs.BackColor = System.Drawing.Color.Transparent
        Me.lblTPktPcs.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTPktPcs.Location = New System.Drawing.Point(760, 620)
        Me.lblTPktPcs.Name = "lblTPktPcs"
        Me.lblTPktPcs.Size = New System.Drawing.Size(65, 18)
        Me.lblTPktPcs.TabIndex = 13
        Me.lblTPktPcs.Text = "Pkt Pcs"
        '
        'txtTPktPcs
        '
        Me.txtTPktPcs.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.txtTPktPcs.Location = New System.Drawing.Point(830, 618)
        Me.txtTPktPcs.Name = "txtTPktPcs"
        Me.txtTPktPcs.ReadOnly = True
        Me.txtTPktPcs.Size = New System.Drawing.Size(60, 20)
        Me.txtTPktPcs.TabIndex = 14
        Me.txtTPktPcs.Text = "0"
        '
        'lblTPktCts
        '
        Me.lblTPktCts.BackColor = System.Drawing.Color.Transparent
        Me.lblTPktCts.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTPktCts.Location = New System.Drawing.Point(900, 620)
        Me.lblTPktCts.Name = "lblTPktCts"
        Me.lblTPktCts.Size = New System.Drawing.Size(65, 18)
        Me.lblTPktCts.TabIndex = 15
        Me.lblTPktCts.Text = "Pkt Cts"
        '
        'txtTPktCts
        '
        Me.txtTPktCts.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.txtTPktCts.Location = New System.Drawing.Point(970, 618)
        Me.txtTPktCts.Name = "txtTPktCts"
        Me.txtTPktCts.ReadOnly = True
        Me.txtTPktCts.Size = New System.Drawing.Size(70, 20)
        Me.txtTPktCts.TabIndex = 16
        Me.txtTPktCts.Text = "0"
        '
        'flxType
        '
        Me.flxType.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.flxType.Location = New System.Drawing.Point(5, 225)
        Me.flxType.Name = "flxType"
        Me.flxType.Size = New System.Drawing.Size(560, 383)
        Me.flxType.TabIndex = 6
        '
        'flxSelect
        '
        Me.flxSelect.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.flxSelect.Location = New System.Drawing.Point(575, 225)
        Me.flxSelect.Name = "flxSelect"
        Me.flxSelect.Size = New System.Drawing.Size(510, 126)
        Me.flxSelect.TabIndex = 7
        '
        'flxDetails
        '
        Me.flxDetails.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.flxDetails.Location = New System.Drawing.Point(575, 409)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.Size = New System.Drawing.Size(510, 199)
        Me.flxDetails.TabIndex = 8
        '
        'lblGridType
        '
        Me.lblGridType.BackColor = System.Drawing.Color.Transparent
        Me.lblGridType.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblGridType.ForeColor = System.Drawing.Color.DarkRed
        Me.lblGridType.Location = New System.Drawing.Point(5, 204)
        Me.lblGridType.Name = "lblGridType"
        Me.lblGridType.Size = New System.Drawing.Size(120, 18)
        Me.lblGridType.TabIndex = 3
        Me.lblGridType.Text = "Finished Pcs"
        '
        'lblGridSelect
        '
        Me.lblGridSelect.BackColor = System.Drawing.Color.Transparent
        Me.lblGridSelect.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblGridSelect.ForeColor = System.Drawing.Color.DarkRed
        Me.lblGridSelect.Location = New System.Drawing.Point(580, 204)
        Me.lblGridSelect.Name = "lblGridSelect"
        Me.lblGridSelect.Size = New System.Drawing.Size(120, 18)
        Me.lblGridSelect.TabIndex = 4
        Me.lblGridSelect.Text = "Selected Pcs"
        '
        'lblGridDetails
        '
        Me.lblGridDetails.BackColor = System.Drawing.Color.Transparent
        Me.lblGridDetails.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblGridDetails.ForeColor = System.Drawing.Color.DarkRed
        Me.lblGridDetails.Location = New System.Drawing.Point(580, 391)
        Me.lblGridDetails.Name = "lblGridDetails"
        Me.lblGridDetails.Size = New System.Drawing.Size(120, 18)
        Me.lblGridDetails.TabIndex = 5
        Me.lblGridDetails.Text = "Packeted Pcs"
        '
        'frm_GRDRnd_SizingPacket
        '
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1101, 648)
        Me.Controls.Add(Me.pnlTitle)
        Me.Controls.Add(Me.pnlToolbar)
        Me.Controls.Add(Me.pnlEntry)
        Me.Controls.Add(Me.lblGridType)
        Me.Controls.Add(Me.lblGridSelect)
        Me.Controls.Add(Me.lblGridDetails)
        Me.Controls.Add(Me.flxType)
        Me.Controls.Add(Me.flxSelect)
        Me.Controls.Add(Me.flxDetails)
        Me.Controls.Add(Me.lblTotPcs)
        Me.Controls.Add(Me.txtTotPcs)
        Me.Controls.Add(Me.lblTotCts)
        Me.Controls.Add(Me.txtTotCts)
        Me.Controls.Add(Me.lblTPktPcs)
        Me.Controls.Add(Me.txtTPktPcs)
        Me.Controls.Add(Me.lblTPktCts)
        Me.Controls.Add(Me.txtTPktCts)
        Me.Controls.Add(Me.txtActCts)
        Me.Controls.Add(Me.lblActCts)
        Me.Controls.Add(Me.txtActPcs)
        Me.Controls.Add(Me.lblActPcs)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_GRDRnd_SizingPacket"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sizing Packet Entry"
        Me.pnlTitle.ResumeLayout(False)
        Me.pnlToolbar.ResumeLayout(False)
        Me.pnlToolbar.PerformLayout()
        Me.pnlEntry.ResumeLayout(False)
        Me.pnlEntry.PerformLayout()
        CType(Me.flxType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    '── Control Declarations ───────────────────────────────────
    Friend WithEvents pnlTitle As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents pnlToolbar As System.Windows.Forms.Panel
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnExcel As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents txtRecordCount As System.Windows.Forms.TextBox
    Friend WithEvents pnlEntry As System.Windows.Forms.Panel

    Friend WithEvents lblParNo As System.Windows.Forms.Label
    Friend WithEvents txtParNo As System.Windows.Forms.TextBox
    Friend WithEvents lblPktNo As System.Windows.Forms.Label
    Friend WithEvents txtPktNo As System.Windows.Forms.TextBox
    Friend WithEvents lblSizeCode As System.Windows.Forms.Label
    Friend WithEvents cmbSizeType As System.Windows.Forms.ComboBox
    Friend WithEvents lblColor As System.Windows.Forms.Label
    Friend WithEvents cmbType1 As System.Windows.Forms.ComboBox
    Friend WithEvents lblMake As System.Windows.Forms.Label
    Friend WithEvents cmbType2 As System.Windows.Forms.ComboBox
    Friend WithEvents lblFluor As System.Windows.Forms.Label
    Friend WithEvents cmbType3 As System.Windows.Forms.ComboBox
    Friend WithEvents lblClarity As System.Windows.Forms.Label
    Friend WithEvents cmbType4 As System.Windows.Forms.ComboBox
    Friend WithEvents lblPcs As System.Windows.Forms.Label
    Friend WithEvents txtPktPcs As System.Windows.Forms.TextBox
    Friend WithEvents lblCts As System.Windows.Forms.Label
    Friend WithEvents txtPktCts As System.Windows.Forms.TextBox

    Friend WithEvents lblBalPcs As System.Windows.Forms.Label
    Friend WithEvents txtBalPcs As System.Windows.Forms.TextBox
    Friend WithEvents lblBalCts As System.Windows.Forms.Label
    Friend WithEvents txtBalCts As System.Windows.Forms.TextBox
    Friend WithEvents lblActPcs As System.Windows.Forms.Label
    Friend WithEvents txtActPcs As System.Windows.Forms.TextBox
    Friend WithEvents lblActCts As System.Windows.Forms.Label
    Friend WithEvents txtActCts As System.Windows.Forms.TextBox
    Friend WithEvents lblPktType As System.Windows.Forms.Label
    Friend WithEvents cmbPktType As System.Windows.Forms.ComboBox
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents lblFinPcs As System.Windows.Forms.Label

    Friend WithEvents lblTotPcs As System.Windows.Forms.Label
    Friend WithEvents txtTotPcs As System.Windows.Forms.TextBox
    Friend WithEvents lblTotCts As System.Windows.Forms.Label
    Friend WithEvents txtTotCts As System.Windows.Forms.TextBox
    Friend WithEvents lblTPktPcs As System.Windows.Forms.Label
    Friend WithEvents txtTPktPcs As System.Windows.Forms.TextBox
    Friend WithEvents lblTPktCts As System.Windows.Forms.Label
    Friend WithEvents txtTPktCts As System.Windows.Forms.TextBox

    Friend WithEvents lblGridType As System.Windows.Forms.Label
    Friend WithEvents lblGridSelect As System.Windows.Forms.Label
    Friend WithEvents lblGridDetails As System.Windows.Forms.Label

    Friend WithEvents flxType As System.Windows.Forms.DataGridView
    Friend WithEvents flxSelect As System.Windows.Forms.DataGridView
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents btnnew1 As Button
    Friend WithEvents btnsave1 As Button
    Friend WithEvents btnexport As Button
    Friend WithEvents btnexit1 As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label

End Class