<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_POLHistory
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
        Me.HazelDev_Button2 = New DiaStock.HazelDev_Button()
        Me.HazelDev_Button1 = New DiaStock.HazelDev_Button()
        Me.flxHistory = New System.Windows.Forms.DataGridView()
        Me.OrderNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PktNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Date2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Time1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtPktCts = New System.Windows.Forms.TextBox()
        Me.txtPktPcs = New System.Windows.Forms.TextBox()
        Me.txtInCts = New System.Windows.Forms.TextBox()
        Me.txtInPcs = New System.Windows.Forms.TextBox()
        Me.flxAssort = New System.Windows.Forms.DataGridView()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtProdCts = New System.Windows.Forms.TextBox()
        Me.txtProdPcs = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtPrice = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdExcel = New DiaStock.HazelDev_Button()
        Me.txtBalCts = New System.Windows.Forms.TextBox()
        Me.txtAssortment = New System.Windows.Forms.TextBox()
        Me.txtBalPcs = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.InPcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Date1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Type = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OrgAssortment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        CType(Me.flxHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxAssort, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Button2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Button1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxHistory)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPktCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPktPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtInCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtInPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxAssort)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(1126, 718)
        Me.HazelDev_ThemeContainer1.TabIndex = 4
        Me.HazelDev_ThemeContainer1.Text = "POLISH BOX ASSORTMENT HISTORY"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'HazelDev_Button2
        '
        Me.HazelDev_Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.HazelDev_Button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.HazelDev_Button2.FlatAppearance.BorderSize = 0
        Me.HazelDev_Button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.HazelDev_Button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.HazelDev_Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.HazelDev_Button2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HazelDev_Button2.ForeColor = System.Drawing.Color.White
        Me.HazelDev_Button2.Location = New System.Drawing.Point(1023, 687)
        Me.HazelDev_Button2.Name = "HazelDev_Button2"
        Me.HazelDev_Button2.Size = New System.Drawing.Size(99, 28)
        Me.HazelDev_Button2.TabIndex = 83
        Me.HazelDev_Button2.Text = "Box Stock"
        Me.HazelDev_Button2.UseVisualStyleBackColor = False
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
        Me.HazelDev_Button1.Location = New System.Drawing.Point(432, 685)
        Me.HazelDev_Button1.Name = "HazelDev_Button1"
        Me.HazelDev_Button1.Size = New System.Drawing.Size(99, 28)
        Me.HazelDev_Button1.TabIndex = 82
        Me.HazelDev_Button1.Text = "Box && Size"
        Me.HazelDev_Button1.UseVisualStyleBackColor = False
        '
        'flxHistory
        '
        Me.flxHistory.AllowUserToAddRows = False
        Me.flxHistory.AllowUserToDeleteRows = False
        Me.flxHistory.AllowUserToResizeColumns = False
        Me.flxHistory.AllowUserToResizeRows = False
        Me.flxHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxHistory.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.OrderNo, Me.PktNo, Me.Pcs, Me.Cts, Me.Date2, Me.Time1, Me.Column1})
        Me.flxHistory.Location = New System.Drawing.Point(552, 109)
        Me.flxHistory.Name = "flxHistory"
        Me.flxHistory.RowHeadersVisible = False
        Me.flxHistory.Size = New System.Drawing.Size(570, 573)
        Me.flxHistory.TabIndex = 81
        '
        'OrderNo
        '
        Me.OrderNo.HeaderText = "Order"
        Me.OrderNo.Name = "OrderNo"
        Me.OrderNo.Width = 80
        '
        'PktNo
        '
        Me.PktNo.HeaderText = "Pkt"
        Me.PktNo.Name = "PktNo"
        Me.PktNo.Width = 60
        '
        'Pcs
        '
        Me.Pcs.HeaderText = "Pcs"
        Me.Pcs.Name = "Pcs"
        Me.Pcs.Width = 80
        '
        'Cts
        '
        Me.Cts.HeaderText = "Cts"
        Me.Cts.Name = "Cts"
        Me.Cts.Width = 80
        '
        'Date2
        '
        Me.Date2.HeaderText = "Date"
        Me.Date2.Name = "Date2"
        Me.Date2.Width = 80
        '
        'Time1
        '
        Me.Time1.HeaderText = "Time"
        Me.Time1.Name = "Time1"
        Me.Time1.Width = 80
        '
        'Column1
        '
        Me.Column1.HeaderText = "Type"
        Me.Column1.Name = "Column1"
        Me.Column1.Width = 80
        '
        'txtPktCts
        '
        Me.txtPktCts.Location = New System.Drawing.Point(630, 688)
        Me.txtPktCts.Name = "txtPktCts"
        Me.txtPktCts.ReadOnly = True
        Me.txtPktCts.Size = New System.Drawing.Size(74, 21)
        Me.txtPktCts.TabIndex = 80
        Me.txtPktCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPktPcs
        '
        Me.txtPktPcs.Location = New System.Drawing.Point(552, 688)
        Me.txtPktPcs.Name = "txtPktPcs"
        Me.txtPktPcs.ReadOnly = True
        Me.txtPktPcs.Size = New System.Drawing.Size(72, 21)
        Me.txtPktPcs.TabIndex = 79
        Me.txtPktPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtInCts
        '
        Me.txtInCts.Location = New System.Drawing.Point(85, 688)
        Me.txtInCts.Name = "txtInCts"
        Me.txtInCts.ReadOnly = True
        Me.txtInCts.Size = New System.Drawing.Size(74, 21)
        Me.txtInCts.TabIndex = 76
        Me.txtInCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtInPcs
        '
        Me.txtInPcs.Location = New System.Drawing.Point(7, 688)
        Me.txtInPcs.Name = "txtInPcs"
        Me.txtInPcs.ReadOnly = True
        Me.txtInPcs.Size = New System.Drawing.Size(72, 21)
        Me.txtInPcs.TabIndex = 75
        Me.txtInPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'flxAssort
        '
        Me.flxAssort.AllowUserToAddRows = False
        Me.flxAssort.AllowUserToDeleteRows = False
        Me.flxAssort.AllowUserToResizeColumns = False
        Me.flxAssort.AllowUserToResizeRows = False
        Me.flxAssort.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxAssort.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.InPcs, Me.InCts, Me.Date1, Me.Type, Me.OrgAssortment, Me.Column2, Me.Column3})
        Me.flxAssort.Location = New System.Drawing.Point(7, 109)
        Me.flxAssort.Name = "flxAssort"
        Me.flxAssort.RowHeadersVisible = False
        Me.flxAssort.Size = New System.Drawing.Size(524, 573)
        Me.flxAssort.TabIndex = 43
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.Label5)
        Me.HazelDev_Panel1.Controls.Add(Me.Label6)
        Me.HazelDev_Panel1.Controls.Add(Me.txtProdCts)
        Me.HazelDev_Panel1.Controls.Add(Me.txtProdPcs)
        Me.HazelDev_Panel1.Controls.Add(Me.Label3)
        Me.HazelDev_Panel1.Controls.Add(Me.txtPrice)
        Me.HazelDev_Panel1.Controls.Add(Me.Label2)
        Me.HazelDev_Panel1.Controls.Add(Me.Label1)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExcel)
        Me.HazelDev_Panel1.Controls.Add(Me.txtBalCts)
        Me.HazelDev_Panel1.Controls.Add(Me.txtAssortment)
        Me.HazelDev_Panel1.Controls.Add(Me.txtBalPcs)
        Me.HazelDev_Panel1.Controls.Add(Me.Label4)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(1119, 49)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(893, 2)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 14)
        Me.Label5.TabIndex = 90
        Me.Label5.Text = "Prod Cts"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(815, 2)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 14)
        Me.Label6.TabIndex = 89
        Me.Label6.Text = "Prod Pcs"
        '
        'txtProdCts
        '
        Me.txtProdCts.Location = New System.Drawing.Point(896, 19)
        Me.txtProdCts.Name = "txtProdCts"
        Me.txtProdCts.ReadOnly = True
        Me.txtProdCts.Size = New System.Drawing.Size(74, 22)
        Me.txtProdCts.TabIndex = 88
        Me.txtProdCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtProdPcs
        '
        Me.txtProdPcs.Location = New System.Drawing.Point(818, 19)
        Me.txtProdPcs.Name = "txtProdPcs"
        Me.txtProdPcs.ReadOnly = True
        Me.txtProdPcs.Size = New System.Drawing.Size(72, 22)
        Me.txtProdPcs.TabIndex = 87
        Me.txtProdPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(704, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 14)
        Me.Label3.TabIndex = 86
        Me.Label3.Text = "Price per Stone"
        '
        'txtPrice
        '
        Me.txtPrice.Location = New System.Drawing.Point(707, 19)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.ReadOnly = True
        Me.txtPrice.Size = New System.Drawing.Size(74, 22)
        Me.txtPrice.TabIndex = 85
        Me.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(624, 2)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 14)
        Me.Label2.TabIndex = 84
        Me.Label2.Text = "Bal Cts"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(546, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 14)
        Me.Label1.TabIndex = 83
        Me.Label1.Text = "Bal Pcs"
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
        Me.cmdExcel.Location = New System.Drawing.Point(1012, 13)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(99, 28)
        Me.cmdExcel.TabIndex = 82
        Me.cmdExcel.Text = "Excel Export"
        Me.cmdExcel.UseVisualStyleBackColor = False
        '
        'txtBalCts
        '
        Me.txtBalCts.Location = New System.Drawing.Point(627, 19)
        Me.txtBalCts.Name = "txtBalCts"
        Me.txtBalCts.ReadOnly = True
        Me.txtBalCts.Size = New System.Drawing.Size(74, 22)
        Me.txtBalCts.TabIndex = 78
        Me.txtBalCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAssortment
        '
        Me.txtAssortment.Location = New System.Drawing.Point(214, 19)
        Me.txtAssortment.Name = "txtAssortment"
        Me.txtAssortment.Size = New System.Drawing.Size(155, 22)
        Me.txtAssortment.TabIndex = 0
        '
        'txtBalPcs
        '
        Me.txtBalPcs.Location = New System.Drawing.Point(549, 19)
        Me.txtBalPcs.Name = "txtBalPcs"
        Me.txtBalPcs.ReadOnly = True
        Me.txtBalPcs.Size = New System.Drawing.Size(72, 22)
        Me.txtBalPcs.TabIndex = 77
        Me.txtBalPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(214, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 14)
        Me.Label4.TabIndex = 76
        Me.Label4.Text = "Assortment"
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
        'InPcs
        '
        Me.InPcs.HeaderText = "In Pcs"
        Me.InPcs.Name = "InPcs"
        Me.InPcs.Width = 80
        '
        'InCts
        '
        Me.InCts.HeaderText = "In Cts"
        Me.InCts.Name = "InCts"
        Me.InCts.Width = 80
        '
        'Date1
        '
        Me.Date1.HeaderText = "Date"
        Me.Date1.Name = "Date1"
        Me.Date1.Width = 80
        '
        'Type
        '
        Me.Type.HeaderText = "Type"
        Me.Type.Name = "Type"
        Me.Type.Width = 50
        '
        'OrgAssortment
        '
        Me.OrgAssortment.HeaderText = "Assort"
        Me.OrgAssortment.Name = "OrgAssortment"
        Me.OrgAssortment.Visible = False
        Me.OrgAssortment.Width = 80
        '
        'Column2
        '
        Me.Column2.HeaderText = "Price"
        Me.Column2.Name = "Column2"
        '
        'Column3
        '
        Me.Column3.HeaderText = "Lot ID"
        Me.Column3.Name = "Column3"
        '
        'frm_POLHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1126, 718)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_POLHistory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Polish Box Assortment History"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        CType(Me.flxHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxAssort, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents flxAssort As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents txtAssortment As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents txtPktCts As System.Windows.Forms.TextBox
    Friend WithEvents txtPktPcs As System.Windows.Forms.TextBox
    Friend WithEvents txtInCts As System.Windows.Forms.TextBox
    Friend WithEvents txtInPcs As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtBalCts As System.Windows.Forms.TextBox
    Friend WithEvents txtBalPcs As System.Windows.Forms.TextBox
    Friend WithEvents flxHistory As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Button1 As DiaStock.HazelDev_Button
    Friend WithEvents HazelDev_Button2 As DiaStock.HazelDev_Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPrice As System.Windows.Forms.TextBox
    Friend WithEvents OrderNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PktNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Date2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Time1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtProdCts As System.Windows.Forms.TextBox
    Friend WithEvents txtProdPcs As System.Windows.Forms.TextBox
    Friend WithEvents InPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Date1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrgAssortment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
