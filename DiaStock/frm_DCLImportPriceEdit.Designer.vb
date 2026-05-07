<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DCLImportPriceEdit
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
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.cmdDelete2 = New DiaStock.HazelDev_Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtParNo2 = New System.Windows.Forms.TextBox()
        Me.cmdNext = New DiaStock.HazelDev_Button()
        Me.txtNextLot = New System.Windows.Forms.TextBox()
        Me.cmdDelete = New DiaStock.HazelDev_Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCts = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPcs = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtParNo = New System.Windows.Forms.TextBox()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.ParNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DepartmentName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Company = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Price = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LotNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtLotNo = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtInvPrice = New System.Windows.Forms.TextBox()
        Me.txtIssCts = New System.Windows.Forms.TextBox()
        Me.txtIssPcs = New System.Windows.Forms.TextBox()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdExcel = New DiaStock.HazelDev_Button()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.txtAvgPrice = New System.Windows.Forms.TextBox()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdDelete2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label3)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtParNo2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdNext)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtNextLot)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdDelete)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label4)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtParNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxDetails)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label13)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtLotNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label12)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtInvPrice)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtIssCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtIssPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtAvgPrice)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(525, 521)
        Me.HazelDev_ThemeContainer1.TabIndex = 8
        Me.HazelDev_ThemeContainer1.Text = "IMPORT LOT/PARCEL"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmdDelete2
        '
        Me.cmdDelete2.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdDelete2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdDelete2.FlatAppearance.BorderSize = 0
        Me.cmdDelete2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdDelete2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdDelete2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDelete2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete2.ForeColor = System.Drawing.Color.White
        Me.cmdDelete2.Location = New System.Drawing.Point(7, 485)
        Me.cmdDelete2.Name = "cmdDelete2"
        Me.cmdDelete2.Size = New System.Drawing.Size(99, 28)
        Me.cmdDelete2.TabIndex = 131
        Me.cmdDelete2.Text = "Import Delete"
        Me.cmdDelete2.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(9, 440)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 15)
        Me.Label3.TabIndex = 130
        Me.Label3.Text = "Parcel No."
        '
        'txtParNo2
        '
        Me.txtParNo2.Location = New System.Drawing.Point(7, 458)
        Me.txtParNo2.MaxLength = 0
        Me.txtParNo2.Name = "txtParNo2"
        Me.txtParNo2.Size = New System.Drawing.Size(163, 21)
        Me.txtParNo2.TabIndex = 129
        '
        'cmdNext
        '
        Me.cmdNext.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdNext.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdNext.FlatAppearance.BorderSize = 0
        Me.cmdNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdNext.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdNext.ForeColor = System.Drawing.Color.White
        Me.cmdNext.Location = New System.Drawing.Point(184, 408)
        Me.cmdNext.Name = "cmdNext"
        Me.cmdNext.Size = New System.Drawing.Size(99, 28)
        Me.cmdNext.TabIndex = 128
        Me.cmdNext.Text = "Next Lot No"
        Me.cmdNext.UseVisualStyleBackColor = False
        '
        'txtNextLot
        '
        Me.txtNextLot.Location = New System.Drawing.Point(353, 411)
        Me.txtNextLot.MaxLength = 0
        Me.txtNextLot.Name = "txtNextLot"
        Me.txtNextLot.ReadOnly = True
        Me.txtNextLot.Size = New System.Drawing.Size(163, 21)
        Me.txtNextLot.TabIndex = 126
        Me.txtNextLot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdDelete.FlatAppearance.BorderSize = 0
        Me.cmdDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.ForeColor = System.Drawing.Color.White
        Me.cmdDelete.Location = New System.Drawing.Point(7, 408)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(99, 28)
        Me.cmdDelete.TabIndex = 125
        Me.cmdDelete.Text = "Pol Box Delete"
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(350, 363)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 15)
        Me.Label4.TabIndex = 124
        Me.Label4.Text = "Box Cts"
        '
        'txtCts
        '
        Me.txtCts.Location = New System.Drawing.Point(353, 381)
        Me.txtCts.MaxLength = 0
        Me.txtCts.Name = "txtCts"
        Me.txtCts.ReadOnly = True
        Me.txtCts.Size = New System.Drawing.Size(163, 21)
        Me.txtCts.TabIndex = 123
        Me.txtCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(181, 363)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 15)
        Me.Label2.TabIndex = 122
        Me.Label2.Text = "Box Pcs"
        '
        'txtPcs
        '
        Me.txtPcs.Location = New System.Drawing.Point(184, 381)
        Me.txtPcs.MaxLength = 0
        Me.txtPcs.Name = "txtPcs"
        Me.txtPcs.ReadOnly = True
        Me.txtPcs.Size = New System.Drawing.Size(163, 21)
        Me.txtPcs.TabIndex = 121
        Me.txtPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(9, 363)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 15)
        Me.Label1.TabIndex = 120
        Me.Label1.Text = "Parcel No."
        '
        'txtParNo
        '
        Me.txtParNo.Location = New System.Drawing.Point(7, 381)
        Me.txtParNo.MaxLength = 0
        Me.txtParNo.Name = "txtParNo"
        Me.txtParNo.Size = New System.Drawing.Size(163, 21)
        Me.txtParNo.TabIndex = 119
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ParNo, Me.DepartmentName, Me.Company, Me.Price, Me.LotNo})
        Me.flxDetails.Location = New System.Drawing.Point(7, 152)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.ReadOnly = True
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(510, 201)
        Me.flxDetails.TabIndex = 118
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
        '
        'Price
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Price.DefaultCellStyle = DataGridViewCellStyle3
        Me.Price.HeaderText = "Price"
        Me.Price.Name = "Price"
        Me.Price.ReadOnly = True
        '
        'LotNo
        '
        Me.LotNo.HeaderText = "Lot No."
        Me.LotNo.Name = "LotNo"
        Me.LotNo.ReadOnly = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Location = New System.Drawing.Point(9, 107)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(46, 15)
        Me.Label13.TabIndex = 117
        Me.Label13.Text = "Lot No."
        '
        'txtLotNo
        '
        Me.txtLotNo.Location = New System.Drawing.Point(7, 125)
        Me.txtLotNo.MaxLength = 0
        Me.txtLotNo.Name = "txtLotNo"
        Me.txtLotNo.Size = New System.Drawing.Size(163, 21)
        Me.txtLotNo.TabIndex = 0
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Location = New System.Drawing.Point(181, 107)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(76, 15)
        Me.Label12.TabIndex = 115
        Me.Label12.Text = "Invoice Price"
        '
        'txtInvPrice
        '
        Me.txtInvPrice.Location = New System.Drawing.Point(184, 125)
        Me.txtInvPrice.MaxLength = 0
        Me.txtInvPrice.Name = "txtInvPrice"
        Me.txtInvPrice.Size = New System.Drawing.Size(163, 21)
        Me.txtInvPrice.TabIndex = 114
        Me.txtInvPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtIssCts
        '
        Me.txtIssCts.Location = New System.Drawing.Point(701, 539)
        Me.txtIssCts.Name = "txtIssCts"
        Me.txtIssCts.ReadOnly = True
        Me.txtIssCts.Size = New System.Drawing.Size(90, 21)
        Me.txtIssCts.TabIndex = 49
        Me.txtIssCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtIssPcs
        '
        Me.txtIssPcs.Location = New System.Drawing.Point(605, 539)
        Me.txtIssPcs.Name = "txtIssPcs"
        Me.txtIssPcs.ReadOnly = True
        Me.txtIssPcs.Size = New System.Drawing.Size(90, 21)
        Me.txtIssPcs.TabIndex = 48
        Me.txtIssPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExcel)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(514, 50)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
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
        Me.cmdExcel.Location = New System.Drawing.Point(411, 13)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(99, 28)
        Me.cmdExcel.TabIndex = 46
        Me.cmdExcel.Text = "Excel"
        Me.cmdExcel.UseVisualStyleBackColor = False
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
        'frm_DCLImportPriceEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(525, 521)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_DCLImportPriceEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import Lot/Parcel"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtLotNo As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtInvPrice As System.Windows.Forms.TextBox
    Friend WithEvents txtIssCts As System.Windows.Forms.TextBox
    Friend WithEvents txtIssPcs As System.Windows.Forms.TextBox
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents txtAvgPrice As System.Windows.Forms.TextBox
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents ParNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DepartmentName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Company As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LotNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtParNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCts As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPcs As System.Windows.Forms.TextBox
    Friend WithEvents cmdDelete As DiaStock.HazelDev_Button
    Friend WithEvents cmdNext As DiaStock.HazelDev_Button
    Friend WithEvents txtNextLot As System.Windows.Forms.TextBox
    Friend WithEvents cmdDelete2 As DiaStock.HazelDev_Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtParNo2 As System.Windows.Forms.TextBox
End Class
