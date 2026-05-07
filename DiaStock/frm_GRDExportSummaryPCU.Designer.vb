<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_GRDExportSummaryPCU
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
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_GRDExportSummaryPCU))
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.txtSelCts = New System.Windows.Forms.TextBox()
        Me.txtSelPcs = New System.Windows.Forms.TextBox()
        Me.txtAddCts = New System.Windows.Forms.TextBox()
        Me.txtAddPcs = New System.Windows.Forms.TextBox()
        Me.txtCts = New System.Windows.Forms.TextBox()
        Me.txtPcs = New System.Windows.Forms.TextBox()
        Me.cmdExcel = New DiaStock.HazelDev_Button()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.Assortment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PacketPcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PacketCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ActPcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ActCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DiffCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Price = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Value = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Department = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OrderNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RefNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Side = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PktNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Confirm = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Supplier = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Category = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Invoice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Company = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Country = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Type = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GIANo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmbOrder = New MTGCComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.cmbParcel = New MTGCComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.txtPackNo = New System.Windows.Forms.TextBox()
        Me.cmdRefresh = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.txtOrderNo = New System.Windows.Forms.TextBox()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtSelCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtSelPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtAddCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtAddPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdExcel)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxDetails)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(1244, 623)
        Me.HazelDev_ThemeContainer1.TabIndex = 5
        Me.HazelDev_ThemeContainer1.Text = "GRADING PACKAGE PCU"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'txtSelCts
        '
        Me.txtSelCts.Enabled = False
        Me.txtSelCts.Location = New System.Drawing.Point(307, 594)
        Me.txtSelCts.Name = "txtSelCts"
        Me.txtSelCts.ReadOnly = True
        Me.txtSelCts.Size = New System.Drawing.Size(97, 21)
        Me.txtSelCts.TabIndex = 79
        Me.txtSelCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSelPcs
        '
        Me.txtSelPcs.Enabled = False
        Me.txtSelPcs.Location = New System.Drawing.Point(207, 594)
        Me.txtSelPcs.Name = "txtSelPcs"
        Me.txtSelPcs.ReadOnly = True
        Me.txtSelPcs.Size = New System.Drawing.Size(97, 21)
        Me.txtSelPcs.TabIndex = 78
        Me.txtSelPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAddCts
        '
        Me.txtAddCts.Enabled = False
        Me.txtAddCts.Location = New System.Drawing.Point(307, 567)
        Me.txtAddCts.Name = "txtAddCts"
        Me.txtAddCts.ReadOnly = True
        Me.txtAddCts.Size = New System.Drawing.Size(97, 21)
        Me.txtAddCts.TabIndex = 77
        Me.txtAddCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAddPcs
        '
        Me.txtAddPcs.Enabled = False
        Me.txtAddPcs.Location = New System.Drawing.Point(207, 567)
        Me.txtAddPcs.Name = "txtAddPcs"
        Me.txtAddPcs.ReadOnly = True
        Me.txtAddPcs.Size = New System.Drawing.Size(97, 21)
        Me.txtAddPcs.TabIndex = 76
        Me.txtAddPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCts
        '
        Me.txtCts.Enabled = False
        Me.txtCts.Location = New System.Drawing.Point(510, 567)
        Me.txtCts.Name = "txtCts"
        Me.txtCts.ReadOnly = True
        Me.txtCts.Size = New System.Drawing.Size(97, 21)
        Me.txtCts.TabIndex = 74
        Me.txtCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPcs
        '
        Me.txtPcs.Enabled = False
        Me.txtPcs.Location = New System.Drawing.Point(410, 567)
        Me.txtPcs.Name = "txtPcs"
        Me.txtPcs.ReadOnly = True
        Me.txtPcs.Size = New System.Drawing.Size(97, 21)
        Me.txtPcs.TabIndex = 73
        Me.txtPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.cmdExcel.Location = New System.Drawing.Point(7, 567)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(99, 28)
        Me.cmdExcel.TabIndex = 72
        Me.cmdExcel.Text = "Excel"
        Me.cmdExcel.UseVisualStyleBackColor = False
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Assortment, Me.DataGridViewTextBoxColumn3, Me.PacketPcs, Me.PacketCts, Me.ActPcs, Me.ActCts, Me.DiffCts, Me.Price, Me.Value, Me.Department, Me.ID, Me.OrderNo, Me.RefNo, Me.Side, Me.PktNo, Me.Confirm, Me.Supplier, Me.Category, Me.Invoice, Me.Company, Me.Country, Me.Type, Me.GIANo, Me.Column1})
        Me.flxDetails.Location = New System.Drawing.Point(6, 109)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.flxDetails.Size = New System.Drawing.Size(1232, 452)
        Me.flxDetails.TabIndex = 68
        '
        'Assortment
        '
        Me.Assortment.HeaderText = "Assortment"
        Me.Assortment.Name = "Assortment"
        Me.Assortment.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        DataGridViewCellStyle9.NullValue = Nothing
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewTextBoxColumn3.HeaderText = "Par No."
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'PacketPcs
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PacketPcs.DefaultCellStyle = DataGridViewCellStyle10
        Me.PacketPcs.HeaderText = "Pcs"
        Me.PacketPcs.Name = "PacketPcs"
        '
        'PacketCts
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PacketCts.DefaultCellStyle = DataGridViewCellStyle11
        Me.PacketCts.HeaderText = "Cts"
        Me.PacketCts.Name = "PacketCts"
        '
        'ActPcs
        '
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ActPcs.DefaultCellStyle = DataGridViewCellStyle12
        Me.ActPcs.HeaderText = "Act Pcs"
        Me.ActPcs.Name = "ActPcs"
        '
        'ActCts
        '
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ActCts.DefaultCellStyle = DataGridViewCellStyle13
        Me.ActCts.HeaderText = "Act Cts"
        Me.ActCts.Name = "ActCts"
        '
        'DiffCts
        '
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DiffCts.DefaultCellStyle = DataGridViewCellStyle14
        Me.DiffCts.HeaderText = "Diff Cts"
        Me.DiffCts.Name = "DiffCts"
        Me.DiffCts.ReadOnly = True
        '
        'Price
        '
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Price.DefaultCellStyle = DataGridViewCellStyle15
        Me.Price.HeaderText = "Price"
        Me.Price.Name = "Price"
        Me.Price.ReadOnly = True
        '
        'Value
        '
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Value.DefaultCellStyle = DataGridViewCellStyle16
        Me.Value.HeaderText = "Value"
        Me.Value.Name = "Value"
        Me.Value.ReadOnly = True
        '
        'Department
        '
        Me.Department.HeaderText = "Department"
        Me.Department.Name = "Department"
        Me.Department.ReadOnly = True
        '
        'ID
        '
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        Me.ID.ReadOnly = True
        '
        'OrderNo
        '
        Me.OrderNo.HeaderText = "Order No"
        Me.OrderNo.Name = "OrderNo"
        Me.OrderNo.ReadOnly = True
        '
        'RefNo
        '
        Me.RefNo.HeaderText = "Ref No"
        Me.RefNo.Name = "RefNo"
        Me.RefNo.ReadOnly = True
        '
        'Side
        '
        Me.Side.HeaderText = "Side"
        Me.Side.Name = "Side"
        Me.Side.ReadOnly = True
        '
        'PktNo
        '
        Me.PktNo.HeaderText = "Pkt No"
        Me.PktNo.Name = "PktNo"
        Me.PktNo.ReadOnly = True
        '
        'Confirm
        '
        Me.Confirm.HeaderText = "Select"
        Me.Confirm.Name = "Confirm"
        '
        'Supplier
        '
        Me.Supplier.HeaderText = "Supplier"
        Me.Supplier.Name = "Supplier"
        Me.Supplier.ReadOnly = True
        '
        'Category
        '
        Me.Category.HeaderText = "Category"
        Me.Category.Name = "Category"
        Me.Category.ReadOnly = True
        '
        'Invoice
        '
        Me.Invoice.HeaderText = "Invoice"
        Me.Invoice.Name = "Invoice"
        Me.Invoice.ReadOnly = True
        '
        'Company
        '
        Me.Company.HeaderText = "Company"
        Me.Company.Name = "Company"
        Me.Company.ReadOnly = True
        '
        'Country
        '
        Me.Country.HeaderText = "Country"
        Me.Country.Name = "Country"
        Me.Country.ReadOnly = True
        '
        'Type
        '
        Me.Type.HeaderText = "Type"
        Me.Type.Name = "Type"
        Me.Type.ReadOnly = True
        '
        'GIANo
        '
        Me.GIANo.HeaderText = "GIA No"
        Me.GIANo.Name = "GIANo"
        Me.GIANo.ReadOnly = True
        '
        'Column1
        '
        Me.Column1.HeaderText = "Rate Code"
        Me.Column1.Name = "Column1"
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.txtOrderNo)
        Me.HazelDev_Panel1.Controls.Add(Me.cmbOrder)
        Me.HazelDev_Panel1.Controls.Add(Me.Label2)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Controls.Add(Me.cmbParcel)
        Me.HazelDev_Panel1.Controls.Add(Me.Label1)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.txtPackNo)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdRefresh)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(1235, 49)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmbOrder
        '
        Me.cmbOrder.ArrowBoxColor = System.Drawing.SystemColors.Control
        Me.cmbOrder.ArrowColor = System.Drawing.Color.Black
        Me.cmbOrder.BindedControl = CType(resources.GetObject("cmbOrder.BindedControl"), MTGCComboBox.ControlloAssociato)
        Me.cmbOrder.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.cmbOrder.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbOrder.ColumnNum = 3
        Me.cmbOrder.ColumnWidth = "80; 80; 80"
        Me.cmbOrder.DisabledArrowBoxColor = System.Drawing.SystemColors.Control
        Me.cmbOrder.DisabledArrowColor = System.Drawing.Color.LightGray
        Me.cmbOrder.DisabledBackColor = System.Drawing.SystemColors.Control
        Me.cmbOrder.DisabledBorderColor = System.Drawing.SystemColors.InactiveBorder
        Me.cmbOrder.DisabledForeColor = System.Drawing.SystemColors.GrayText
        Me.cmbOrder.DisplayMember = "Text"
        Me.cmbOrder.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbOrder.DropDownBackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.cmbOrder.DropDownForeColor = System.Drawing.Color.Black
        Me.cmbOrder.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.cmbOrder.DropDownWidth = 245
        Me.cmbOrder.GridLineColor = System.Drawing.Color.LightGray
        Me.cmbOrder.GridLineHorizontal = False
        Me.cmbOrder.GridLineVertical = False
        Me.cmbOrder.IntegralHeight = False
        Me.cmbOrder.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.cmbOrder.Location = New System.Drawing.Point(708, 13)
        Me.cmbOrder.ManagingFastMouseMoving = True
        Me.cmbOrder.ManagingFastMouseMovingInterval = 30
        Me.cmbOrder.Name = "cmbOrder"
        Me.cmbOrder.SelectedItem = Nothing
        Me.cmbOrder.SelectedValue = Nothing
        Me.cmbOrder.Size = New System.Drawing.Size(211, 23)
        Me.cmbOrder.TabIndex = 142
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(628, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 14)
        Me.Label2.TabIndex = 141
        Me.Label2.Text = "Order No."
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
        Me.cmdNew.Location = New System.Drawing.Point(1025, 13)
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.Size = New System.Drawing.Size(99, 28)
        Me.cmdNew.TabIndex = 139
        Me.cmdNew.Text = "New"
        Me.cmdNew.UseVisualStyleBackColor = False
        '
        'cmbParcel
        '
        Me.cmbParcel.ArrowBoxColor = System.Drawing.SystemColors.Control
        Me.cmbParcel.ArrowColor = System.Drawing.Color.Black
        Me.cmbParcel.BindedControl = CType(resources.GetObject("cmbParcel.BindedControl"), MTGCComboBox.ControlloAssociato)
        Me.cmbParcel.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.cmbParcel.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbParcel.ColumnNum = 4
        Me.cmbParcel.ColumnWidth = "80; 80; 80; 80"
        Me.cmbParcel.DisabledArrowBoxColor = System.Drawing.SystemColors.Control
        Me.cmbParcel.DisabledArrowColor = System.Drawing.Color.LightGray
        Me.cmbParcel.DisabledBackColor = System.Drawing.SystemColors.Control
        Me.cmbParcel.DisabledBorderColor = System.Drawing.SystemColors.InactiveBorder
        Me.cmbParcel.DisabledForeColor = System.Drawing.SystemColors.GrayText
        Me.cmbParcel.DisplayMember = "Text"
        Me.cmbParcel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbParcel.DropDownBackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.cmbParcel.DropDownForeColor = System.Drawing.Color.Black
        Me.cmbParcel.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.cmbParcel.DropDownWidth = 325
        Me.cmbParcel.GridLineColor = System.Drawing.Color.LightGray
        Me.cmbParcel.GridLineHorizontal = False
        Me.cmbParcel.GridLineVertical = False
        Me.cmbParcel.IntegralHeight = False
        Me.cmbParcel.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.cmbParcel.Location = New System.Drawing.Point(280, 13)
        Me.cmbParcel.ManagingFastMouseMoving = True
        Me.cmbParcel.ManagingFastMouseMovingInterval = 30
        Me.cmbParcel.Name = "cmbParcel"
        Me.cmbParcel.SelectedItem = Nothing
        Me.cmbParcel.SelectedValue = Nothing
        Me.cmbParcel.Size = New System.Drawing.Size(224, 23)
        Me.cmbParcel.TabIndex = 138
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(109, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 14)
        Me.Label1.TabIndex = 137
        Me.Label1.Text = "Package No."
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
        Me.cmdSave.Location = New System.Drawing.Point(1130, 13)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(99, 28)
        Me.cmdSave.TabIndex = 93
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'txtPackNo
        '
        Me.txtPackNo.Location = New System.Drawing.Point(190, 13)
        Me.txtPackNo.Name = "txtPackNo"
        Me.txtPackNo.Size = New System.Drawing.Size(84, 22)
        Me.txtPackNo.TabIndex = 92
        '
        'cmdRefresh
        '
        Me.cmdRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdRefresh.FlatAppearance.BorderSize = 0
        Me.cmdRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRefresh.ForeColor = System.Drawing.Color.White
        Me.cmdRefresh.Location = New System.Drawing.Point(511, 13)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(99, 28)
        Me.cmdRefresh.TabIndex = 51
        Me.cmdRefresh.Text = "Refresh"
        Me.cmdRefresh.UseVisualStyleBackColor = False
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
        'txtOrderNo
        '
        Me.txtOrderNo.Location = New System.Drawing.Point(925, 13)
        Me.txtOrderNo.Name = "txtOrderNo"
        Me.txtOrderNo.Size = New System.Drawing.Size(84, 22)
        Me.txtOrderNo.TabIndex = 143
        '
        'frm_GRDExportSummaryPCU
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1244, 623)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_GRDExportSummaryPCU"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Grading Package PCU"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents txtCts As System.Windows.Forms.TextBox
    Friend WithEvents txtPcs As System.Windows.Forms.TextBox
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents txtPackNo As System.Windows.Forms.TextBox
    Friend WithEvents cmdRefresh As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbParcel As MTGCComboBox
    Friend WithEvents txtAddCts As System.Windows.Forms.TextBox
    Friend WithEvents txtAddPcs As System.Windows.Forms.TextBox
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents txtSelCts As System.Windows.Forms.TextBox
    Friend WithEvents txtSelPcs As System.Windows.Forms.TextBox
    Friend WithEvents Assortment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PacketPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PacketCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ActPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ActCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DiffCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Value As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Department As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrderNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RefNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Side As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PktNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Confirm As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Supplier As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Category As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Invoice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Company As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Country As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GIANo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbOrder As MTGCComboBox
    Friend WithEvents txtOrderNo As System.Windows.Forms.TextBox
End Class
