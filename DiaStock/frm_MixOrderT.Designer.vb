<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_MixOrderT
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
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.dtpOrdDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpDueDate = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbClient = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbSupp = New System.Windows.Forms.ComboBox()
        Me.pnlDetails2 = New System.Windows.Forms.Panel()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.RefNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Length = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Width1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NoofSet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PcsPerSet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MaxCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MaxType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pnlDetails1 = New System.Windows.Forms.Panel()
        Me.txtSide = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.cmbType = New System.Windows.Forms.ComboBox()
        Me.cmdAdd = New DiaStock.HazelDev_Button()
        Me.txtMaxCost = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.txtTotPcs = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtPcs = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtSets = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtWid = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtLen = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtRef = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdReport = New DiaStock.HazelDev_Button()
        Me.HazelDev_Button1 = New DiaStock.HazelDev_Button()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.txtOrdNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        Me.pnlDetails2.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDetails1.SuspendLayout()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtRemarks)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label37)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label20)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label18)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtDesc)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label17)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.dtpOrdDate)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.dtpDueDate)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label5)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbClient)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label3)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbSupp)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.pnlDetails2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.pnlDetails1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtOrdNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label4)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(901, 556)
        Me.HazelDev_ThemeContainer1.TabIndex = 5
        Me.HazelDev_ThemeContainer1.Text = "MIX TEMP ORDER ENTRY"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(540, 171)
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(276, 21)
        Me.txtRemarks.TabIndex = 132
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.Color.Transparent
        Me.Label37.Location = New System.Drawing.Point(540, 151)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(57, 15)
        Me.Label37.TabIndex = 131
        Me.Label37.Text = "Remarks"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Location = New System.Drawing.Point(119, 151)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(67, 15)
        Me.Label20.TabIndex = 100
        Me.Label20.Text = "Order Date"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Location = New System.Drawing.Point(9, 151)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(59, 15)
        Me.Label18.TabIndex = 99
        Me.Label18.Text = "Due Date"
        '
        'txtDesc
        '
        Me.txtDesc.Location = New System.Drawing.Point(228, 171)
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(306, 21)
        Me.txtDesc.TabIndex = 98
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Location = New System.Drawing.Point(228, 151)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(120, 15)
        Me.Label17.TabIndex = 97
        Me.Label17.Text = "Customer Reference"
        '
        'dtpOrdDate
        '
        Me.dtpOrdDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpOrdDate.Location = New System.Drawing.Point(120, 169)
        Me.dtpOrdDate.Name = "dtpOrdDate"
        Me.dtpOrdDate.Size = New System.Drawing.Size(102, 21)
        Me.dtpOrdDate.TabIndex = 96
        '
        'dtpDueDate
        '
        Me.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDueDate.Location = New System.Drawing.Point(12, 169)
        Me.dtpDueDate.Name = "dtpDueDate"
        Me.dtpDueDate.Size = New System.Drawing.Size(102, 21)
        Me.dtpDueDate.TabIndex = 95
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(296, 107)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(38, 15)
        Me.Label5.TabIndex = 85
        Me.Label5.Text = "Client"
        '
        'cmbClient
        '
        Me.cmbClient.DropDownWidth = 250
        Me.cmbClient.FormattingEnabled = True
        Me.cmbClient.IntegralHeight = False
        Me.cmbClient.Location = New System.Drawing.Point(296, 125)
        Me.cmbClient.Name = "cmbClient"
        Me.cmbClient.Size = New System.Drawing.Size(179, 23)
        Me.cmbClient.TabIndex = 84
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(120, 107)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 15)
        Me.Label3.TabIndex = 83
        Me.Label3.Text = "Supplier"
        '
        'cmbSupp
        '
        Me.cmbSupp.DropDownWidth = 250
        Me.cmbSupp.FormattingEnabled = True
        Me.cmbSupp.IntegralHeight = False
        Me.cmbSupp.Location = New System.Drawing.Point(120, 125)
        Me.cmbSupp.Name = "cmbSupp"
        Me.cmbSupp.Size = New System.Drawing.Size(170, 23)
        Me.cmbSupp.TabIndex = 82
        '
        'pnlDetails2
        '
        Me.pnlDetails2.BackColor = System.Drawing.Color.White
        Me.pnlDetails2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDetails2.Controls.Add(Me.flxDetails)
        Me.pnlDetails2.Location = New System.Drawing.Point(12, 267)
        Me.pnlDetails2.Name = "pnlDetails2"
        Me.pnlDetails2.Size = New System.Drawing.Size(882, 284)
        Me.pnlDetails2.TabIndex = 81
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RefNo, Me.Column1, Me.Length, Me.Width1, Me.NoofSet, Me.PcsPerSet, Me.Column2, Me.MaxCost, Me.MaxType})
        Me.flxDetails.Location = New System.Drawing.Point(6, 8)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(870, 268)
        Me.flxDetails.TabIndex = 44
        '
        'RefNo
        '
        Me.RefNo.HeaderText = "RefNo"
        Me.RefNo.Name = "RefNo"
        Me.RefNo.ReadOnly = True
        '
        'Column1
        '
        Me.Column1.HeaderText = "Side"
        Me.Column1.Name = "Column1"
        '
        'Length
        '
        Me.Length.HeaderText = "Length"
        Me.Length.Name = "Length"
        '
        'Width1
        '
        Me.Width1.HeaderText = "Width"
        Me.Width1.Name = "Width1"
        '
        'NoofSet
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.NoofSet.DefaultCellStyle = DataGridViewCellStyle1
        Me.NoofSet.HeaderText = "No of Sets"
        Me.NoofSet.Name = "NoofSet"
        '
        'PcsPerSet
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PcsPerSet.DefaultCellStyle = DataGridViewCellStyle2
        Me.PcsPerSet.HeaderText = "Pcs Per Set"
        Me.PcsPerSet.Name = "PcsPerSet"
        '
        'Column2
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column2.DefaultCellStyle = DataGridViewCellStyle3
        Me.Column2.HeaderText = "Total Pcs"
        Me.Column2.Name = "Column2"
        '
        'MaxCost
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.MaxCost.DefaultCellStyle = DataGridViewCellStyle4
        Me.MaxCost.HeaderText = "Max Cost"
        Me.MaxCost.Name = "MaxCost"
        '
        'MaxType
        '
        Me.MaxType.HeaderText = "Max Type"
        Me.MaxType.Name = "MaxType"
        '
        'pnlDetails1
        '
        Me.pnlDetails1.BackColor = System.Drawing.Color.White
        Me.pnlDetails1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDetails1.Controls.Add(Me.txtSide)
        Me.pnlDetails1.Controls.Add(Me.Label22)
        Me.pnlDetails1.Controls.Add(Me.Label32)
        Me.pnlDetails1.Controls.Add(Me.cmbType)
        Me.pnlDetails1.Controls.Add(Me.cmdAdd)
        Me.pnlDetails1.Controls.Add(Me.txtMaxCost)
        Me.pnlDetails1.Controls.Add(Me.Label31)
        Me.pnlDetails1.Controls.Add(Me.txtTotPcs)
        Me.pnlDetails1.Controls.Add(Me.Label30)
        Me.pnlDetails1.Controls.Add(Me.txtPcs)
        Me.pnlDetails1.Controls.Add(Me.Label29)
        Me.pnlDetails1.Controls.Add(Me.txtSets)
        Me.pnlDetails1.Controls.Add(Me.Label28)
        Me.pnlDetails1.Controls.Add(Me.txtWid)
        Me.pnlDetails1.Controls.Add(Me.Label2)
        Me.pnlDetails1.Controls.Add(Me.txtLen)
        Me.pnlDetails1.Controls.Add(Me.Label1)
        Me.pnlDetails1.Controls.Add(Me.txtRef)
        Me.pnlDetails1.Controls.Add(Me.Label21)
        Me.pnlDetails1.Location = New System.Drawing.Point(12, 198)
        Me.pnlDetails1.Name = "pnlDetails1"
        Me.pnlDetails1.Size = New System.Drawing.Size(882, 63)
        Me.pnlDetails1.TabIndex = 71
        '
        'txtSide
        '
        Me.txtSide.Location = New System.Drawing.Point(162, 31)
        Me.txtSide.MaxLength = 1
        Me.txtSide.Name = "txtSide"
        Me.txtSide.Size = New System.Drawing.Size(47, 21)
        Me.txtSide.TabIndex = 136
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Location = New System.Drawing.Point(159, 13)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(32, 15)
        Me.Label22.TabIndex = 135
        Me.Label22.Text = "Side"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Location = New System.Drawing.Point(614, 13)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(60, 15)
        Me.Label32.TabIndex = 134
        Me.Label32.Text = "Max Type"
        '
        'cmbType
        '
        Me.cmbType.FormattingEnabled = True
        Me.cmbType.IntegralHeight = False
        Me.cmbType.Items.AddRange(New Object() {"P", "C"})
        Me.cmbType.Location = New System.Drawing.Point(617, 31)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Size = New System.Drawing.Size(52, 23)
        Me.cmbType.TabIndex = 133
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
        Me.cmdAdd.Location = New System.Drawing.Point(777, 26)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(99, 28)
        Me.cmdAdd.TabIndex = 132
        Me.cmdAdd.Text = "Add"
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'txtMaxCost
        '
        Me.txtMaxCost.Location = New System.Drawing.Point(550, 31)
        Me.txtMaxCost.Name = "txtMaxCost"
        Me.txtMaxCost.Size = New System.Drawing.Size(61, 21)
        Me.txtMaxCost.TabIndex = 128
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.Color.Transparent
        Me.Label31.Location = New System.Drawing.Point(547, 13)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(58, 15)
        Me.Label31.TabIndex = 127
        Me.Label31.Text = "Max Cost"
        '
        'txtTotPcs
        '
        Me.txtTotPcs.Location = New System.Drawing.Point(483, 31)
        Me.txtTotPcs.Name = "txtTotPcs"
        Me.txtTotPcs.ReadOnly = True
        Me.txtTotPcs.Size = New System.Drawing.Size(61, 21)
        Me.txtTotPcs.TabIndex = 126
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Location = New System.Drawing.Point(480, 13)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(57, 15)
        Me.Label30.TabIndex = 125
        Me.Label30.Text = "Total Pcs"
        '
        'txtPcs
        '
        Me.txtPcs.Location = New System.Drawing.Point(416, 31)
        Me.txtPcs.Name = "txtPcs"
        Me.txtPcs.Size = New System.Drawing.Size(61, 21)
        Me.txtPcs.TabIndex = 124
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Location = New System.Drawing.Point(413, 13)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(69, 15)
        Me.Label29.TabIndex = 123
        Me.Label29.Text = "Pcs per Set"
        '
        'txtSets
        '
        Me.txtSets.Location = New System.Drawing.Point(349, 31)
        Me.txtSets.Name = "txtSets"
        Me.txtSets.Size = New System.Drawing.Size(61, 21)
        Me.txtSets.TabIndex = 122
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Location = New System.Drawing.Point(346, 13)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(31, 15)
        Me.Label28.TabIndex = 121
        Me.Label28.Text = "Sets"
        '
        'txtWid
        '
        Me.txtWid.Location = New System.Drawing.Point(282, 31)
        Me.txtWid.Name = "txtWid"
        Me.txtWid.Size = New System.Drawing.Size(61, 21)
        Me.txtWid.TabIndex = 98
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(279, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 15)
        Me.Label2.TabIndex = 97
        Me.Label2.Text = "Width"
        '
        'txtLen
        '
        Me.txtLen.Location = New System.Drawing.Point(215, 31)
        Me.txtLen.Name = "txtLen"
        Me.txtLen.Size = New System.Drawing.Size(61, 21)
        Me.txtLen.TabIndex = 96
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(212, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 15)
        Me.Label1.TabIndex = 95
        Me.Label1.Text = "Length"
        '
        'txtRef
        '
        Me.txtRef.Location = New System.Drawing.Point(4, 31)
        Me.txtRef.Name = "txtRef"
        Me.txtRef.Size = New System.Drawing.Size(152, 21)
        Me.txtRef.TabIndex = 92
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Location = New System.Drawing.Point(3, 13)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(64, 15)
        Me.Label21.TabIndex = 91
        Me.Label21.Text = "Reference"
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmdReport)
        Me.HazelDev_Panel1.Controls.Add(Me.HazelDev_Button1)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(891, 50)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmdReport
        '
        Me.cmdReport.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdReport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdReport.FlatAppearance.BorderSize = 0
        Me.cmdReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdReport.ForeColor = System.Drawing.Color.White
        Me.cmdReport.Location = New System.Drawing.Point(787, 13)
        Me.cmdReport.Name = "cmdReport"
        Me.cmdReport.Size = New System.Drawing.Size(99, 28)
        Me.cmdReport.TabIndex = 47
        Me.cmdReport.Text = "Detail Report"
        Me.cmdReport.UseVisualStyleBackColor = False
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
        Me.HazelDev_Button1.Location = New System.Drawing.Point(319, 13)
        Me.HazelDev_Button1.Name = "HazelDev_Button1"
        Me.HazelDev_Button1.Size = New System.Drawing.Size(99, 28)
        Me.HazelDev_Button1.TabIndex = 46
        Me.HazelDev_Button1.Text = "Excel Export"
        Me.HazelDev_Button1.UseVisualStyleBackColor = False
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
        'txtOrdNo
        '
        Me.txtOrdNo.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtOrdNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOrdNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtOrdNo.Location = New System.Drawing.Point(12, 125)
        Me.txtOrdNo.Name = "txtOrdNo"
        Me.txtOrdNo.Size = New System.Drawing.Size(84, 21)
        Me.txtOrdNo.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(9, 107)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 15)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Order No"
        '
        'frm_MixOrderT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(901, 556)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_MixOrderT"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mix Temp Order Entry"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        Me.pnlDetails2.ResumeLayout(False)
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDetails1.ResumeLayout(False)
        Me.pnlDetails1.PerformLayout()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents pnlDetails2 As System.Windows.Forms.Panel
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents pnlDetails1 As System.Windows.Forms.Panel
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents txtOrdNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbSupp As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbClient As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents dtpOrdDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtRef As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtWid As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtLen As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtMaxCost As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtTotPcs As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtPcs As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtSets As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents cmdAdd As DiaStock.HazelDev_Button
    Friend WithEvents HazelDev_Button1 As DiaStock.HazelDev_Button
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents cmbType As System.Windows.Forms.ComboBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents txtSide As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents RefNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Length As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Width1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NoofSet As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PcsPerSet As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MaxCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MaxType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdReport As DiaStock.HazelDev_Button
End Class
