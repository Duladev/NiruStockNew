<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DCLPackingList
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.optKit = New System.Windows.Forms.RadioButton()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbClient = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdAdd = New DiaStock.HazelDev_Button()
        Me.txtPackNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtSupCode = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbCategory = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbSupplierCode = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbType = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbDeliver = New System.Windows.Forms.ComboBox()
        Me.cmbDesc = New System.Windows.Forms.ComboBox()
        Me.optRghSales = New System.Windows.Forms.RadioButton()
        Me.optColombo = New System.Windows.Forms.RadioButton()
        Me.optRounds = New System.Windows.Forms.RadioButton()
        Me.optPolBoxTrf = New System.Windows.Forms.RadioButton()
        Me.optMix = New System.Windows.Forms.RadioButton()
        Me.optPCU = New System.Windows.Forms.RadioButton()
        Me.optMixExport = New System.Windows.Forms.RadioButton()
        Me.optContract = New System.Windows.Forms.RadioButton()
        Me.optSorting = New System.Windows.Forms.RadioButton()
        Me.optSales = New System.Windows.Forms.RadioButton()
        Me.optRghRej = New System.Windows.Forms.RadioButton()
        Me.optOrder = New System.Windows.Forms.RadioButton()
        Me.optPrecision = New System.Windows.Forms.RadioButton()
        Me.optGMix = New System.Windows.Forms.RadioButton()
        Me.optPolRej = New System.Windows.Forms.RadioButton()
        Me.optParcel = New System.Windows.Forms.RadioButton()
        Me.pnlDetails2 = New System.Windows.Forms.Panel()
        Me.cmdExcel = New DiaStock.HazelDev_Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtNewPackListNo = New System.Windows.Forms.TextBox()
        Me.cmdUpdate = New DiaStock.HazelDev_Button()
        Me.txtTotCts = New System.Windows.Forms.TextBox()
        Me.txtTotPcs = New System.Windows.Forms.TextBox()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DepartmentName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Company = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PackNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Type = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Company1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Category = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Supplier = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPackListNo = New System.Windows.Forms.TextBox()
        Me.cndRefresh = New DiaStock.HazelDev_Button()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlDetails2.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Panel1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.pnlDetails2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(636, 635)
        Me.HazelDev_ThemeContainer1.TabIndex = 8
        Me.HazelDev_ThemeContainer1.Text = "PACKING LIST"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.optKit)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.cmbClient)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.cmdAdd)
        Me.Panel1.Controls.Add(Me.txtPackNo)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.txtSupCode)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.cmbCategory)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.cmbSupplierCode)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.cmbType)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.cmbDeliver)
        Me.Panel1.Controls.Add(Me.cmbDesc)
        Me.Panel1.Controls.Add(Me.optRghSales)
        Me.Panel1.Controls.Add(Me.optColombo)
        Me.Panel1.Controls.Add(Me.optRounds)
        Me.Panel1.Controls.Add(Me.optPolBoxTrf)
        Me.Panel1.Controls.Add(Me.optMix)
        Me.Panel1.Controls.Add(Me.optPCU)
        Me.Panel1.Controls.Add(Me.optMixExport)
        Me.Panel1.Controls.Add(Me.optContract)
        Me.Panel1.Controls.Add(Me.optSorting)
        Me.Panel1.Controls.Add(Me.optSales)
        Me.Panel1.Controls.Add(Me.optRghRej)
        Me.Panel1.Controls.Add(Me.optOrder)
        Me.Panel1.Controls.Add(Me.optPrecision)
        Me.Panel1.Controls.Add(Me.optGMix)
        Me.Panel1.Controls.Add(Me.optPolRej)
        Me.Panel1.Controls.Add(Me.optParcel)
        Me.Panel1.Location = New System.Drawing.Point(3, 110)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(625, 197)
        Me.Panel1.TabIndex = 143
        '
        'optKit
        '
        Me.optKit.AutoSize = True
        Me.optKit.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.optKit.Location = New System.Drawing.Point(531, 3)
        Me.optKit.Name = "optKit"
        Me.optKit.Size = New System.Drawing.Size(39, 19)
        Me.optKit.TabIndex = 151
        Me.optKit.Text = "Kit"
        Me.optKit.UseVisualStyleBackColor = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label9.Location = New System.Drawing.Point(349, 146)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(42, 14)
        Me.Label9.TabIndex = 150
        Me.Label9.Text = "Client"
        '
        'cmbClient
        '
        Me.cmbClient.FormattingEnabled = True
        Me.cmbClient.IntegralHeight = False
        Me.cmbClient.Location = New System.Drawing.Point(349, 163)
        Me.cmbClient.Name = "cmbClient"
        Me.cmbClient.Size = New System.Drawing.Size(176, 23)
        Me.cmbClient.TabIndex = 149
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 100)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 14)
        Me.Label2.TabIndex = 102
        Me.Label2.Text = "Description"
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
        Me.cmdAdd.Location = New System.Drawing.Point(96, 159)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(99, 28)
        Me.cmdAdd.TabIndex = 148
        Me.cmdAdd.Text = "Add"
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'txtPackNo
        '
        Me.txtPackNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtPackNo.Location = New System.Drawing.Point(6, 163)
        Me.txtPackNo.Name = "txtPackNo"
        Me.txtPackNo.Size = New System.Drawing.Size(84, 22)
        Me.txtPackNo.TabIndex = 146
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 146)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(81, 14)
        Me.Label7.TabIndex = 147
        Me.Label7.Text = "Package No."
        '
        'txtSupCode
        '
        Me.txtSupCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtSupCode.Location = New System.Drawing.Point(531, 163)
        Me.txtSupCode.Name = "txtSupCode"
        Me.txtSupCode.ReadOnly = True
        Me.txtSupCode.Size = New System.Drawing.Size(84, 22)
        Me.txtSupCode.TabIndex = 145
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label6.Location = New System.Drawing.Point(199, 146)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(63, 14)
        Me.Label6.TabIndex = 144
        Me.Label6.Text = "Category"
        '
        'cmbCategory
        '
        Me.cmbCategory.FormattingEnabled = True
        Me.cmbCategory.IntegralHeight = False
        Me.cmbCategory.Location = New System.Drawing.Point(199, 163)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(144, 23)
        Me.cmbCategory.TabIndex = 143
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(406, 100)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 14)
        Me.Label5.TabIndex = 108
        Me.Label5.Text = "Customer"
        '
        'cmbSupplierCode
        '
        Me.cmbSupplierCode.FormattingEnabled = True
        Me.cmbSupplierCode.IntegralHeight = False
        Me.cmbSupplierCode.Location = New System.Drawing.Point(406, 118)
        Me.cmbSupplierCode.Name = "cmbSupplierCode"
        Me.cmbSupplierCode.Size = New System.Drawing.Size(209, 23)
        Me.cmbSupplierCode.TabIndex = 107
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(330, 100)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(36, 14)
        Me.Label4.TabIndex = 106
        Me.Label4.Text = "Type"
        '
        'cmbType
        '
        Me.cmbType.FormattingEnabled = True
        Me.cmbType.IntegralHeight = False
        Me.cmbType.Location = New System.Drawing.Point(330, 118)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Size = New System.Drawing.Size(72, 23)
        Me.cmbType.TabIndex = 105
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(172, 100)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 14)
        Me.Label3.TabIndex = 104
        Me.Label3.Text = "Delivered To"
        '
        'cmbDeliver
        '
        Me.cmbDeliver.FormattingEnabled = True
        Me.cmbDeliver.IntegralHeight = False
        Me.cmbDeliver.Location = New System.Drawing.Point(172, 118)
        Me.cmbDeliver.Name = "cmbDeliver"
        Me.cmbDeliver.Size = New System.Drawing.Size(152, 23)
        Me.cmbDeliver.TabIndex = 103
        '
        'cmbDesc
        '
        Me.cmbDesc.FormattingEnabled = True
        Me.cmbDesc.IntegralHeight = False
        Me.cmbDesc.Location = New System.Drawing.Point(6, 118)
        Me.cmbDesc.Name = "cmbDesc"
        Me.cmbDesc.Size = New System.Drawing.Size(163, 23)
        Me.cmbDesc.TabIndex = 101
        '
        'optRghSales
        '
        Me.optRghSales.AutoSize = True
        Me.optRghSales.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.optRghSales.Location = New System.Drawing.Point(390, 78)
        Me.optRghSales.Name = "optRghSales"
        Me.optRghSales.Size = New System.Drawing.Size(96, 19)
        Me.optRghSales.TabIndex = 100
        Me.optRghSales.Text = "Rough Sales"
        Me.optRghSales.UseVisualStyleBackColor = False
        '
        'optColombo
        '
        Me.optColombo.AutoSize = True
        Me.optColombo.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.optColombo.Location = New System.Drawing.Point(390, 53)
        Me.optColombo.Name = "optColombo"
        Me.optColombo.Size = New System.Drawing.Size(122, 19)
        Me.optColombo.TabIndex = 99
        Me.optColombo.Text = "Colombo Grading"
        Me.optColombo.UseVisualStyleBackColor = False
        '
        'optRounds
        '
        Me.optRounds.AutoSize = True
        Me.optRounds.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.optRounds.Location = New System.Drawing.Point(390, 28)
        Me.optRounds.Name = "optRounds"
        Me.optRounds.Size = New System.Drawing.Size(68, 19)
        Me.optRounds.TabIndex = 98
        Me.optRounds.Text = "Rounds"
        Me.optRounds.UseVisualStyleBackColor = False
        '
        'optPolBoxTrf
        '
        Me.optPolBoxTrf.AutoSize = True
        Me.optPolBoxTrf.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.optPolBoxTrf.Location = New System.Drawing.Point(390, 3)
        Me.optPolBoxTrf.Name = "optPolBoxTrf"
        Me.optPolBoxTrf.Size = New System.Drawing.Size(100, 19)
        Me.optPolBoxTrf.TabIndex = 97
        Me.optPolBoxTrf.Text = "Polish Box Trf"
        Me.optPolBoxTrf.UseVisualStyleBackColor = False
        '
        'optMix
        '
        Me.optMix.AutoSize = True
        Me.optMix.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.optMix.Location = New System.Drawing.Point(254, 78)
        Me.optMix.Name = "optMix"
        Me.optMix.Size = New System.Drawing.Size(45, 19)
        Me.optMix.TabIndex = 96
        Me.optMix.Text = "Mix"
        Me.optMix.UseVisualStyleBackColor = False
        '
        'optPCU
        '
        Me.optPCU.AutoSize = True
        Me.optPCU.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.optPCU.Location = New System.Drawing.Point(254, 53)
        Me.optPCU.Name = "optPCU"
        Me.optPCU.Size = New System.Drawing.Size(112, 19)
        Me.optPCU.TabIndex = 95
        Me.optPCU.Text = "PCU Send Back"
        Me.optPCU.UseVisualStyleBackColor = False
        '
        'optMixExport
        '
        Me.optMixExport.AutoSize = True
        Me.optMixExport.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.optMixExport.Location = New System.Drawing.Point(254, 28)
        Me.optMixExport.Name = "optMixExport"
        Me.optMixExport.Size = New System.Drawing.Size(89, 19)
        Me.optMixExport.TabIndex = 94
        Me.optMixExport.Text = "Mix Exports"
        Me.optMixExport.UseVisualStyleBackColor = False
        '
        'optContract
        '
        Me.optContract.AutoSize = True
        Me.optContract.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.optContract.Location = New System.Drawing.Point(254, 3)
        Me.optContract.Name = "optContract"
        Me.optContract.Size = New System.Drawing.Size(70, 19)
        Me.optContract.TabIndex = 93
        Me.optContract.Text = "Contract"
        Me.optContract.UseVisualStyleBackColor = False
        '
        'optSorting
        '
        Me.optSorting.AutoSize = True
        Me.optSorting.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.optSorting.Location = New System.Drawing.Point(130, 78)
        Me.optSorting.Name = "optSorting"
        Me.optSorting.Size = New System.Drawing.Size(99, 19)
        Me.optSorting.TabIndex = 92
        Me.optSorting.Text = "Direct Sorting"
        Me.optSorting.UseVisualStyleBackColor = False
        '
        'optSales
        '
        Me.optSales.AutoSize = True
        Me.optSales.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.optSales.Location = New System.Drawing.Point(130, 53)
        Me.optSales.Name = "optSales"
        Me.optSales.Size = New System.Drawing.Size(56, 19)
        Me.optSales.TabIndex = 91
        Me.optSales.Text = "Sales"
        Me.optSales.UseVisualStyleBackColor = False
        '
        'optRghRej
        '
        Me.optRghRej.AutoSize = True
        Me.optRghRej.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.optRghRej.Location = New System.Drawing.Point(130, 28)
        Me.optRghRej.Name = "optRghRej"
        Me.optRghRej.Size = New System.Drawing.Size(106, 19)
        Me.optRghRej.TabIndex = 90
        Me.optRghRej.Text = "Rough Rejects"
        Me.optRghRej.UseVisualStyleBackColor = False
        '
        'optOrder
        '
        Me.optOrder.AutoSize = True
        Me.optOrder.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.optOrder.Location = New System.Drawing.Point(130, 3)
        Me.optOrder.Name = "optOrder"
        Me.optOrder.Size = New System.Drawing.Size(106, 19)
        Me.optOrder.TabIndex = 89
        Me.optOrder.Text = "Internal Orders"
        Me.optOrder.UseVisualStyleBackColor = False
        '
        'optPrecision
        '
        Me.optPrecision.AutoSize = True
        Me.optPrecision.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.optPrecision.Location = New System.Drawing.Point(10, 78)
        Me.optPrecision.Name = "optPrecision"
        Me.optPrecision.Size = New System.Drawing.Size(76, 19)
        Me.optPrecision.TabIndex = 88
        Me.optPrecision.Text = "Precision"
        Me.optPrecision.UseVisualStyleBackColor = False
        '
        'optGMix
        '
        Me.optGMix.AutoSize = True
        Me.optGMix.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.optGMix.Location = New System.Drawing.Point(10, 53)
        Me.optGMix.Name = "optGMix"
        Me.optGMix.Size = New System.Drawing.Size(92, 19)
        Me.optGMix.TabIndex = 87
        Me.optGMix.Text = "Grading Mix"
        Me.optGMix.UseVisualStyleBackColor = False
        '
        'optPolRej
        '
        Me.optPolRej.AutoSize = True
        Me.optPolRej.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.optPolRej.Location = New System.Drawing.Point(10, 28)
        Me.optPolRej.Name = "optPolRej"
        Me.optPolRej.Size = New System.Drawing.Size(103, 19)
        Me.optPolRej.TabIndex = 86
        Me.optPolRej.Text = "Polish Rejects"
        Me.optPolRej.UseVisualStyleBackColor = False
        '
        'optParcel
        '
        Me.optParcel.AutoSize = True
        Me.optParcel.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.optParcel.Checked = True
        Me.optParcel.Location = New System.Drawing.Point(10, 3)
        Me.optParcel.Name = "optParcel"
        Me.optParcel.Size = New System.Drawing.Size(100, 19)
        Me.optParcel.TabIndex = 85
        Me.optParcel.TabStop = True
        Me.optParcel.Text = "Rough Parcel"
        Me.optParcel.UseVisualStyleBackColor = False
        '
        'pnlDetails2
        '
        Me.pnlDetails2.BackColor = System.Drawing.Color.White
        Me.pnlDetails2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDetails2.Controls.Add(Me.cmdExcel)
        Me.pnlDetails2.Controls.Add(Me.Label8)
        Me.pnlDetails2.Controls.Add(Me.txtNewPackListNo)
        Me.pnlDetails2.Controls.Add(Me.cmdUpdate)
        Me.pnlDetails2.Controls.Add(Me.txtTotCts)
        Me.pnlDetails2.Controls.Add(Me.txtTotPcs)
        Me.pnlDetails2.Controls.Add(Me.flxDetails)
        Me.pnlDetails2.Location = New System.Drawing.Point(3, 313)
        Me.pnlDetails2.Name = "pnlDetails2"
        Me.pnlDetails2.Size = New System.Drawing.Size(625, 316)
        Me.pnlDetails2.TabIndex = 68
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
        Me.cmdExcel.Location = New System.Drawing.Point(413, 283)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(99, 28)
        Me.cmdExcel.TabIndex = 141
        Me.cmdExcel.Text = "Excel"
        Me.cmdExcel.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label8.Location = New System.Drawing.Point(6, 284)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(104, 14)
        Me.Label8.TabIndex = 140
        Me.Label8.Text = "Packing List No."
        '
        'txtNewPackListNo
        '
        Me.txtNewPackListNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtNewPackListNo.Location = New System.Drawing.Point(116, 283)
        Me.txtNewPackListNo.Name = "txtNewPackListNo"
        Me.txtNewPackListNo.Size = New System.Drawing.Size(68, 22)
        Me.txtNewPackListNo.TabIndex = 139
        '
        'cmdUpdate
        '
        Me.cmdUpdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdUpdate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdUpdate.FlatAppearance.BorderSize = 0
        Me.cmdUpdate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdUpdate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdUpdate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUpdate.ForeColor = System.Drawing.Color.White
        Me.cmdUpdate.Location = New System.Drawing.Point(516, 283)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(99, 28)
        Me.cmdUpdate.TabIndex = 80
        Me.cmdUpdate.Text = "Update"
        Me.cmdUpdate.UseVisualStyleBackColor = False
        '
        'txtTotCts
        '
        Me.txtTotCts.Enabled = False
        Me.txtTotCts.Location = New System.Drawing.Point(293, 284)
        Me.txtTotCts.Name = "txtTotCts"
        Me.txtTotCts.ReadOnly = True
        Me.txtTotCts.Size = New System.Drawing.Size(97, 21)
        Me.txtTotCts.TabIndex = 79
        Me.txtTotCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotPcs
        '
        Me.txtTotPcs.Enabled = False
        Me.txtTotPcs.Location = New System.Drawing.Point(190, 284)
        Me.txtTotPcs.Name = "txtTotPcs"
        Me.txtTotPcs.ReadOnly = True
        Me.txtTotPcs.Size = New System.Drawing.Size(97, 21)
        Me.txtTotPcs.TabIndex = 78
        Me.txtTotPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Code, Me.ParNo, Me.DepartmentName, Me.Company, Me.PackNo, Me.Type, Me.Company1, Me.Category, Me.Supplier})
        Me.flxDetails.Location = New System.Drawing.Point(6, 4)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(609, 274)
        Me.flxDetails.TabIndex = 44
        '
        'Code
        '
        Me.Code.HeaderText = "Department"
        Me.Code.Name = "Code"
        Me.Code.ReadOnly = True
        '
        'ParNo
        '
        Me.ParNo.HeaderText = "Parcel No"
        Me.ParNo.Name = "ParNo"
        Me.ParNo.ReadOnly = True
        '
        'DepartmentName
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DepartmentName.DefaultCellStyle = DataGridViewCellStyle1
        Me.DepartmentName.HeaderText = "Pcs"
        Me.DepartmentName.Name = "DepartmentName"
        Me.DepartmentName.ReadOnly = True
        Me.DepartmentName.Width = 80
        '
        'Company
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Company.DefaultCellStyle = DataGridViewCellStyle2
        Me.Company.HeaderText = "Cts"
        Me.Company.Name = "Company"
        Me.Company.ReadOnly = True
        Me.Company.Width = 80
        '
        'PackNo
        '
        Me.PackNo.HeaderText = "Pack No"
        Me.PackNo.Name = "PackNo"
        Me.PackNo.Width = 80
        '
        'Type
        '
        Me.Type.HeaderText = "Type"
        Me.Type.Name = "Type"
        Me.Type.Width = 80
        '
        'Company1
        '
        Me.Company1.HeaderText = "Company"
        Me.Company1.Name = "Company1"
        Me.Company1.Width = 80
        '
        'Category
        '
        Me.Category.HeaderText = "Category"
        Me.Category.Name = "Category"
        Me.Category.Width = 80
        '
        'Supplier
        '
        Me.Supplier.HeaderText = "Supplier"
        Me.Supplier.Name = "Supplier"
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.Label1)
        Me.HazelDev_Panel1.Controls.Add(Me.txtPackListNo)
        Me.HazelDev_Panel1.Controls.Add(Me.cndRefresh)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(625, 50)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(109, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 14)
        Me.Label1.TabIndex = 139
        Me.Label1.Text = "Packing List No."
        '
        'txtPackListNo
        '
        Me.txtPackListNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtPackListNo.Location = New System.Drawing.Point(217, 16)
        Me.txtPackListNo.Name = "txtPackListNo"
        Me.txtPackListNo.Size = New System.Drawing.Size(84, 22)
        Me.txtPackListNo.TabIndex = 138
        '
        'cndRefresh
        '
        Me.cndRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cndRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cndRefresh.FlatAppearance.BorderSize = 0
        Me.cndRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cndRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cndRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cndRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cndRefresh.ForeColor = System.Drawing.Color.White
        Me.cndRefresh.Location = New System.Drawing.Point(307, 13)
        Me.cndRefresh.Name = "cndRefresh"
        Me.cndRefresh.Size = New System.Drawing.Size(99, 28)
        Me.cndRefresh.TabIndex = 140
        Me.cndRefresh.Text = "Refresh"
        Me.cndRefresh.UseVisualStyleBackColor = False
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
        Me.cmdSave.Location = New System.Drawing.Point(517, 13)
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
        Me.cmdNew.Location = New System.Drawing.Point(412, 13)
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.Size = New System.Drawing.Size(99, 28)
        Me.cmdNew.TabIndex = 31
        Me.cmdNew.Text = "New"
        Me.cmdNew.UseVisualStyleBackColor = False
        '
        'frm_DCLPackingList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(636, 635)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_DCLPackingList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Packing List"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
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
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPackListNo As System.Windows.Forms.TextBox
    Friend WithEvents cndRefresh As DiaStock.HazelDev_Button
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents txtTotCts As System.Windows.Forms.TextBox
    Friend WithEvents txtTotPcs As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cmdAdd As DiaStock.HazelDev_Button
    Friend WithEvents txtPackNo As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtSupCode As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbSupplierCode As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbType As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbDeliver As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbDesc As System.Windows.Forms.ComboBox
    Friend WithEvents optRghSales As System.Windows.Forms.RadioButton
    Friend WithEvents optColombo As System.Windows.Forms.RadioButton
    Friend WithEvents optRounds As System.Windows.Forms.RadioButton
    Friend WithEvents optPolBoxTrf As System.Windows.Forms.RadioButton
    Friend WithEvents optMix As System.Windows.Forms.RadioButton
    Friend WithEvents optPCU As System.Windows.Forms.RadioButton
    Friend WithEvents optMixExport As System.Windows.Forms.RadioButton
    Friend WithEvents optContract As System.Windows.Forms.RadioButton
    Friend WithEvents optSorting As System.Windows.Forms.RadioButton
    Friend WithEvents optSales As System.Windows.Forms.RadioButton
    Friend WithEvents optRghRej As System.Windows.Forms.RadioButton
    Friend WithEvents optOrder As System.Windows.Forms.RadioButton
    Friend WithEvents optPrecision As System.Windows.Forms.RadioButton
    Friend WithEvents optGMix As System.Windows.Forms.RadioButton
    Friend WithEvents optPolRej As System.Windows.Forms.RadioButton
    Friend WithEvents optParcel As System.Windows.Forms.RadioButton
    Friend WithEvents cmdUpdate As DiaStock.HazelDev_Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtNewPackListNo As System.Windows.Forms.TextBox
    Friend WithEvents Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DepartmentName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Company As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PackNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Company1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Category As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Supplier As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbClient As System.Windows.Forms.ComboBox
    Friend WithEvents optKit As System.Windows.Forms.RadioButton
End Class
