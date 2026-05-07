<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_KITConfirmOrders
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
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPack = New System.Windows.Forms.TextBox()
        Me.txtCts = New System.Windows.Forms.TextBox()
        Me.txtPcs = New System.Windows.Forms.TextBox()
        Me.ExpProgress = New System.Windows.Forms.ProgressBar()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Reference = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Side = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParcelNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Assortment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Price = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PacketNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FinishedPcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FinishedCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PacketPcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PacketCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IssuePcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RareCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RecordNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OrgAssort = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LineNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GrPcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbClient = New System.Windows.Forms.ComboBox()
        Me.cmdExcel = New DiaStock.HazelDev_Button()
        Me.optEdit = New System.Windows.Forms.RadioButton()
        Me.optNew = New System.Windows.Forms.RadioButton()
        Me.txtOrder = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmdRefresh = New DiaStock.HazelDev_Button()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPack)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.ExpProgress)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxDetails)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(1250, 592)
        Me.HazelDev_ThemeContainer1.TabIndex = 6
        Me.HazelDev_ThemeContainer1.Text = "KIT CONFIRM ORDERS"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(4, 566)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 15)
        Me.Label2.TabIndex = 100
        Me.Label2.Text = "Packing List No"
        '
        'txtPack
        '
        Me.txtPack.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPack.Location = New System.Drawing.Point(102, 566)
        Me.txtPack.Name = "txtPack"
        Me.txtPack.Size = New System.Drawing.Size(84, 21)
        Me.txtPack.TabIndex = 99
        '
        'txtCts
        '
        Me.txtCts.Location = New System.Drawing.Point(683, 566)
        Me.txtCts.Name = "txtCts"
        Me.txtCts.ReadOnly = True
        Me.txtCts.Size = New System.Drawing.Size(102, 21)
        Me.txtCts.TabIndex = 76
        Me.txtCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPcs
        '
        Me.txtPcs.Location = New System.Drawing.Point(575, 566)
        Me.txtPcs.Name = "txtPcs"
        Me.txtPcs.ReadOnly = True
        Me.txtPcs.Size = New System.Drawing.Size(102, 21)
        Me.txtPcs.TabIndex = 75
        Me.txtPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ExpProgress
        '
        Me.ExpProgress.Location = New System.Drawing.Point(3, 536)
        Me.ExpProgress.Name = "ExpProgress"
        Me.ExpProgress.Size = New System.Drawing.Size(1232, 24)
        Me.ExpProgress.TabIndex = 69
        Me.ExpProgress.Visible = False
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.Reference, Me.Side, Me.ParcelNo, Me.Assortment, Me.Price, Me.PacketNo, Me.FinishedPcs, Me.FinishedCts, Me.PacketPcs, Me.PacketCts, Me.IssuePcs, Me.RareCode, Me.RecordNo, Me.OrgAssort, Me.LineNo, Me.GrPcs, Me.Column1, Me.Column2, Me.Column3, Me.Column4})
        Me.flxDetails.Location = New System.Drawing.Point(6, 109)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.ReadOnly = True
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.flxDetails.Size = New System.Drawing.Size(1232, 421)
        Me.flxDetails.TabIndex = 68
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Order No."
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        DataGridViewCellStyle8.NullValue = Nothing
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridViewTextBoxColumn3.HeaderText = "Subject"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'Reference
        '
        Me.Reference.HeaderText = "Reference"
        Me.Reference.Name = "Reference"
        Me.Reference.ReadOnly = True
        '
        'Side
        '
        Me.Side.HeaderText = "Side"
        Me.Side.Name = "Side"
        Me.Side.ReadOnly = True
        Me.Side.Width = 40
        '
        'ParcelNo
        '
        Me.ParcelNo.HeaderText = "Parcel"
        Me.ParcelNo.Name = "ParcelNo"
        Me.ParcelNo.ReadOnly = True
        Me.ParcelNo.Width = 50
        '
        'Assortment
        '
        Me.Assortment.HeaderText = "Assortment"
        Me.Assortment.Name = "Assortment"
        Me.Assortment.ReadOnly = True
        '
        'Price
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Price.DefaultCellStyle = DataGridViewCellStyle9
        Me.Price.HeaderText = "Price"
        Me.Price.Name = "Price"
        Me.Price.ReadOnly = True
        Me.Price.Width = 50
        '
        'PacketNo
        '
        Me.PacketNo.HeaderText = "Pkt"
        Me.PacketNo.Name = "PacketNo"
        Me.PacketNo.ReadOnly = True
        Me.PacketNo.Width = 60
        '
        'FinishedPcs
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.FinishedPcs.DefaultCellStyle = DataGridViewCellStyle10
        Me.FinishedPcs.HeaderText = "Fin Pcs"
        Me.FinishedPcs.Name = "FinishedPcs"
        Me.FinishedPcs.ReadOnly = True
        Me.FinishedPcs.Width = 70
        '
        'FinishedCts
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.FinishedCts.DefaultCellStyle = DataGridViewCellStyle11
        Me.FinishedCts.HeaderText = "Fin Cts"
        Me.FinishedCts.Name = "FinishedCts"
        Me.FinishedCts.ReadOnly = True
        Me.FinishedCts.Width = 70
        '
        'PacketPcs
        '
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PacketPcs.DefaultCellStyle = DataGridViewCellStyle12
        Me.PacketPcs.HeaderText = "Pkt Pcs"
        Me.PacketPcs.Name = "PacketPcs"
        Me.PacketPcs.ReadOnly = True
        Me.PacketPcs.Width = 70
        '
        'PacketCts
        '
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PacketCts.DefaultCellStyle = DataGridViewCellStyle13
        Me.PacketCts.HeaderText = "Pkt Cts"
        Me.PacketCts.Name = "PacketCts"
        Me.PacketCts.ReadOnly = True
        Me.PacketCts.Width = 70
        '
        'IssuePcs
        '
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.IssuePcs.DefaultCellStyle = DataGridViewCellStyle14
        Me.IssuePcs.HeaderText = "Iss Cts"
        Me.IssuePcs.Name = "IssuePcs"
        Me.IssuePcs.ReadOnly = True
        Me.IssuePcs.Width = 70
        '
        'RareCode
        '
        Me.RareCode.HeaderText = "Rate"
        Me.RareCode.Name = "RareCode"
        Me.RareCode.ReadOnly = True
        Me.RareCode.Width = 70
        '
        'RecordNo
        '
        Me.RecordNo.HeaderText = "Record No"
        Me.RecordNo.Name = "RecordNo"
        Me.RecordNo.ReadOnly = True
        Me.RecordNo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.RecordNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'OrgAssort
        '
        Me.OrgAssort.HeaderText = "Org Assort"
        Me.OrgAssort.Name = "OrgAssort"
        Me.OrgAssort.ReadOnly = True
        '
        'LineNo
        '
        Me.LineNo.HeaderText = "Line No"
        Me.LineNo.Name = "LineNo"
        Me.LineNo.ReadOnly = True
        '
        'GrPcs
        '
        Me.GrPcs.HeaderText = "Gr Pcs"
        Me.GrPcs.Name = "GrPcs"
        Me.GrPcs.ReadOnly = True
        '
        'Column1
        '
        Me.Column1.HeaderText = "Avg Price"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "Iss Pcs"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.HeaderText = "Client No"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.HeaderText = "Max Value"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.Label5)
        Me.HazelDev_Panel1.Controls.Add(Me.cmbClient)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExcel)
        Me.HazelDev_Panel1.Controls.Add(Me.optEdit)
        Me.HazelDev_Panel1.Controls.Add(Me.optNew)
        Me.HazelDev_Panel1.Controls.Add(Me.txtOrder)
        Me.HazelDev_Panel1.Controls.Add(Me.Label4)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdRefresh)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(1235, 49)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(808, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 14)
        Me.Label5.TabIndex = 87
        Me.Label5.Text = "Client"
        '
        'cmbClient
        '
        Me.cmbClient.DropDownWidth = 250
        Me.cmbClient.FormattingEnabled = True
        Me.cmbClient.IntegralHeight = False
        Me.cmbClient.Location = New System.Drawing.Point(851, 17)
        Me.cmbClient.Name = "cmbClient"
        Me.cmbClient.Size = New System.Drawing.Size(179, 22)
        Me.cmbClient.TabIndex = 86
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
        Me.cmdExcel.Location = New System.Drawing.Point(1121, 13)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(99, 28)
        Me.cmdExcel.TabIndex = 77
        Me.cmdExcel.Text = "Excel"
        Me.cmdExcel.UseVisualStyleBackColor = False
        '
        'optEdit
        '
        Me.optEdit.AutoSize = True
        Me.optEdit.Location = New System.Drawing.Point(739, 16)
        Me.optEdit.Name = "optEdit"
        Me.optEdit.Size = New System.Drawing.Size(46, 18)
        Me.optEdit.TabIndex = 75
        Me.optEdit.Text = "Edit"
        Me.optEdit.UseVisualStyleBackColor = True
        '
        'optNew
        '
        Me.optNew.AutoSize = True
        Me.optNew.Checked = True
        Me.optNew.Location = New System.Drawing.Point(683, 16)
        Me.optNew.Name = "optNew"
        Me.optNew.Size = New System.Drawing.Size(50, 18)
        Me.optNew.TabIndex = 74
        Me.optNew.TabStop = True
        Me.optNew.Text = "New"
        Me.optNew.UseVisualStyleBackColor = True
        '
        'txtOrder
        '
        Me.txtOrder.BackColor = System.Drawing.SystemColors.Window
        Me.txtOrder.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtOrder.Location = New System.Drawing.Point(476, 17)
        Me.txtOrder.Name = "txtOrder"
        Me.txtOrder.Size = New System.Drawing.Size(84, 21)
        Me.txtOrder.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(413, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 14)
        Me.Label4.TabIndex = 73
        Me.Label4.Text = "Order No"
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
        Me.cmdRefresh.Location = New System.Drawing.Point(575, 13)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(99, 28)
        Me.cmdRefresh.TabIndex = 51
        Me.cmdRefresh.Text = "Refresh"
        Me.cmdRefresh.UseVisualStyleBackColor = False
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
        'frm_KITConfirmOrders
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1250, 592)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_KITConfirmOrders"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Kit Confirm Orders"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents ExpProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents txtOrder As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmdRefresh As DiaStock.HazelDev_Button
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents optEdit As System.Windows.Forms.RadioButton
    Friend WithEvents optNew As System.Windows.Forms.RadioButton
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents txtCts As System.Windows.Forms.TextBox
    Friend WithEvents txtPcs As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbClient As System.Windows.Forms.ComboBox
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Reference As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Side As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParcelNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Assortment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PacketNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FinishedPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FinishedCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PacketPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PacketCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IssuePcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RareCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RecordNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrgAssort As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LineNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GrPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPack As System.Windows.Forms.TextBox
End Class
