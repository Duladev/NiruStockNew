<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_PCUFinishOrders
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
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
        Me.Confirm = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.RecordNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RejPcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RejCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LostPcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LostCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Broken = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Extra = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Grp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LineNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Category = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Company = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Supplier = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InvNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdExcel = New DiaStock.HazelDev_Button()
        Me.chkGrading = New DiaStock.HazelDev_CheckBox()
        Me.txtOrder = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.optEdit = New System.Windows.Forms.RadioButton()
        Me.optNew = New System.Windows.Forms.RadioButton()
        Me.chkSelect = New DiaStock.HazelDev_CheckBox()
        Me.cmdRefresh = New DiaStock.HazelDev_Button()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.chkRejects = New DiaStock.HazelDev_CheckBox()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.ExpProgress)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxDetails)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(1249, 568)
        Me.HazelDev_ThemeContainer1.TabIndex = 4
        Me.HazelDev_ThemeContainer1.Text = "PRECISION ORDERS VERIFICATION"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'ExpProgress
        '
        Me.ExpProgress.Location = New System.Drawing.Point(6, 537)
        Me.ExpProgress.Name = "ExpProgress"
        Me.ExpProgress.Size = New System.Drawing.Size(1232, 24)
        Me.ExpProgress.TabIndex = 70
        Me.ExpProgress.Visible = False
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.Reference, Me.Side, Me.ParcelNo, Me.Assortment, Me.Price, Me.PacketNo, Me.FinishedPcs, Me.FinishedCts, Me.PacketPcs, Me.PacketCts, Me.IssuePcs, Me.RareCode, Me.Confirm, Me.RecordNo, Me.RejPcs, Me.RejCts, Me.LostPcs, Me.LostCts, Me.Broken, Me.Extra, Me.Grp, Me.LineNo, Me.Category, Me.Company, Me.Supplier, Me.InvNo})
        Me.flxDetails.Location = New System.Drawing.Point(7, 110)
        Me.flxDetails.Name = "flxDetails"
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
        DataGridViewCellStyle1.NullValue = Nothing
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle1
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
        '
        'ParcelNo
        '
        Me.ParcelNo.HeaderText = "Parcel No"
        Me.ParcelNo.Name = "ParcelNo"
        Me.ParcelNo.ReadOnly = True
        '
        'Assortment
        '
        Me.Assortment.HeaderText = "Assortment"
        Me.Assortment.Name = "Assortment"
        Me.Assortment.ReadOnly = True
        '
        'Price
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Price.DefaultCellStyle = DataGridViewCellStyle2
        Me.Price.HeaderText = "Price"
        Me.Price.Name = "Price"
        Me.Price.ReadOnly = True
        '
        'PacketNo
        '
        Me.PacketNo.HeaderText = "Packet No"
        Me.PacketNo.Name = "PacketNo"
        Me.PacketNo.ReadOnly = True
        '
        'FinishedPcs
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.FinishedPcs.DefaultCellStyle = DataGridViewCellStyle3
        Me.FinishedPcs.HeaderText = "Finished Pcs"
        Me.FinishedPcs.Name = "FinishedPcs"
        '
        'FinishedCts
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.FinishedCts.DefaultCellStyle = DataGridViewCellStyle4
        Me.FinishedCts.HeaderText = "Finished Cts"
        Me.FinishedCts.Name = "FinishedCts"
        '
        'PacketPcs
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PacketPcs.DefaultCellStyle = DataGridViewCellStyle5
        Me.PacketPcs.HeaderText = "Packet Pcs"
        Me.PacketPcs.Name = "PacketPcs"
        '
        'PacketCts
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PacketCts.DefaultCellStyle = DataGridViewCellStyle6
        Me.PacketCts.HeaderText = "Packet Cts"
        Me.PacketCts.Name = "PacketCts"
        '
        'IssuePcs
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.IssuePcs.DefaultCellStyle = DataGridViewCellStyle7
        Me.IssuePcs.HeaderText = "Issue Cts"
        Me.IssuePcs.Name = "IssuePcs"
        '
        'RareCode
        '
        Me.RareCode.HeaderText = "Rate Code"
        Me.RareCode.Name = "RareCode"
        Me.RareCode.ReadOnly = True
        '
        'Confirm
        '
        Me.Confirm.HeaderText = "Confirm"
        Me.Confirm.Name = "Confirm"
        '
        'RecordNo
        '
        Me.RecordNo.HeaderText = "Record No"
        Me.RecordNo.Name = "RecordNo"
        Me.RecordNo.ReadOnly = True
        Me.RecordNo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.RecordNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'RejPcs
        '
        Me.RejPcs.HeaderText = "Rej Pcs"
        Me.RejPcs.Name = "RejPcs"
        '
        'RejCts
        '
        Me.RejCts.HeaderText = "Rej Cts"
        Me.RejCts.Name = "RejCts"
        '
        'LostPcs
        '
        Me.LostPcs.HeaderText = "Lost Pcs"
        Me.LostPcs.Name = "LostPcs"
        '
        'LostCts
        '
        Me.LostCts.HeaderText = "Lost Cts"
        Me.LostCts.Name = "LostCts"
        '
        'Broken
        '
        Me.Broken.HeaderText = "Broken"
        Me.Broken.Name = "Broken"
        '
        'Extra
        '
        Me.Extra.HeaderText = "Extra"
        Me.Extra.Name = "Extra"
        '
        'Grp
        '
        Me.Grp.HeaderText = "Grp"
        Me.Grp.Name = "Grp"
        Me.Grp.ReadOnly = True
        '
        'LineNo
        '
        Me.LineNo.HeaderText = "Line No"
        Me.LineNo.Name = "LineNo"
        Me.LineNo.ReadOnly = True
        '
        'Category
        '
        Me.Category.HeaderText = "Category"
        Me.Category.Name = "Category"
        Me.Category.ReadOnly = True
        '
        'Company
        '
        Me.Company.HeaderText = "Company"
        Me.Company.Name = "Company"
        Me.Company.ReadOnly = True
        '
        'Supplier
        '
        Me.Supplier.HeaderText = "Supplier"
        Me.Supplier.Name = "Supplier"
        Me.Supplier.ReadOnly = True
        '
        'InvNo
        '
        Me.InvNo.HeaderText = "Inv No"
        Me.InvNo.Name = "InvNo"
        Me.InvNo.ReadOnly = True
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.chkRejects)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExcel)
        Me.HazelDev_Panel1.Controls.Add(Me.chkGrading)
        Me.HazelDev_Panel1.Controls.Add(Me.txtOrder)
        Me.HazelDev_Panel1.Controls.Add(Me.Label4)
        Me.HazelDev_Panel1.Controls.Add(Me.optEdit)
        Me.HazelDev_Panel1.Controls.Add(Me.optNew)
        Me.HazelDev_Panel1.Controls.Add(Me.chkSelect)
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
        Me.cmdExcel.Location = New System.Drawing.Point(820, 14)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(99, 28)
        Me.cmdExcel.TabIndex = 75
        Me.cmdExcel.Text = "Excel"
        Me.cmdExcel.UseVisualStyleBackColor = False
        '
        'chkGrading
        '
        Me.chkGrading.Checked = False
        Me.chkGrading.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.chkGrading.Location = New System.Drawing.Point(323, 19)
        Me.chkGrading.Name = "chkGrading"
        Me.chkGrading.Size = New System.Drawing.Size(84, 16)
        Me.chkGrading.TabIndex = 74
        Me.chkGrading.Text = "Grading"
        Me.chkGrading.TransparencyKey = System.Drawing.Color.Empty
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
        'optEdit
        '
        Me.optEdit.AutoSize = True
        Me.optEdit.Location = New System.Drawing.Point(736, 14)
        Me.optEdit.Name = "optEdit"
        Me.optEdit.Size = New System.Drawing.Size(46, 18)
        Me.optEdit.TabIndex = 71
        Me.optEdit.Text = "Edit"
        Me.optEdit.UseVisualStyleBackColor = True
        '
        'optNew
        '
        Me.optNew.AutoSize = True
        Me.optNew.Checked = True
        Me.optNew.Location = New System.Drawing.Point(680, 14)
        Me.optNew.Name = "optNew"
        Me.optNew.Size = New System.Drawing.Size(50, 18)
        Me.optNew.TabIndex = 70
        Me.optNew.TabStop = True
        Me.optNew.Text = "New"
        Me.optNew.UseVisualStyleBackColor = True
        '
        'chkSelect
        '
        Me.chkSelect.Checked = False
        Me.chkSelect.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkSelect.Location = New System.Drawing.Point(1136, 19)
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Size = New System.Drawing.Size(84, 16)
        Me.chkSelect.TabIndex = 69
        Me.chkSelect.Text = "Select All"
        Me.chkSelect.TransparencyKey = System.Drawing.Color.Empty
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
        'chkRejects
        '
        Me.chkRejects.Checked = False
        Me.chkRejects.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.chkRejects.Location = New System.Drawing.Point(938, 19)
        Me.chkRejects.Name = "chkRejects"
        Me.chkRejects.Size = New System.Drawing.Size(84, 16)
        Me.chkRejects.TabIndex = 76
        Me.chkRejects.Text = "Rejects"
        Me.chkRejects.TransparencyKey = System.Drawing.Color.Empty
        '
        'frm_PCUFinishOrders
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1249, 568)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_PCUFinishOrders"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Precision Orders Verification"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents optEdit As System.Windows.Forms.RadioButton
    Friend WithEvents optNew As System.Windows.Forms.RadioButton
    Friend WithEvents chkSelect As DiaStock.HazelDev_CheckBox
    Friend WithEvents cmdRefresh As DiaStock.HazelDev_Button
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents txtOrder As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkGrading As DiaStock.HazelDev_CheckBox
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
    Friend WithEvents Confirm As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents RecordNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RejPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RejCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LostPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LostCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Broken As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Extra As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Grp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LineNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Category As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Company As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Supplier As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InvNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExpProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents chkRejects As DiaStock.HazelDev_CheckBox
End Class
