<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_MixAssortRejectVerify
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
        Me.cmdSelect2 = New DiaStock.HazelDev_Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbAssortCode = New System.Windows.Forms.ComboBox()
        Me.txtCts = New System.Windows.Forms.TextBox()
        Me.txtPcs = New System.Windows.Forms.TextBox()
        Me.pnlDetails2 = New System.Windows.Forms.Panel()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdSelect = New DiaStock.HazelDev_Button()
        Me.Type = New System.Windows.Forms.Label()
        Me.cmbAssortType = New System.Windows.Forms.ComboBox()
        Me.chkSelect = New DiaStock.HazelDev_CheckBox()
        Me.cmdFind = New DiaStock.HazelDev_Button()
        Me.chkAll = New DiaStock.HazelDev_CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.cmdExcel = New DiaStock.HazelDev_Button()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PktNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Assortment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OrigAssort = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Price = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ImportNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OldAssort = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Select1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AvgCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RejDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        Me.pnlDetails2.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdSelect2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbAssortCode)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.pnlDetails2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(1205, 593)
        Me.HazelDev_ThemeContainer1.TabIndex = 9
        Me.HazelDev_ThemeContainer1.Text = "MIX REJECT VERFICATION"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmdSelect2
        '
        Me.cmdSelect2.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSelect2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSelect2.FlatAppearance.BorderSize = 0
        Me.cmdSelect2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdSelect2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdSelect2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSelect2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSelect2.ForeColor = System.Drawing.Color.White
        Me.cmdSelect2.Location = New System.Drawing.Point(852, 563)
        Me.cmdSelect2.Name = "cmdSelect2"
        Me.cmdSelect2.Size = New System.Drawing.Size(99, 28)
        Me.cmdSelect2.TabIndex = 167
        Me.cmdSelect2.Text = "Select"
        Me.cmdSelect2.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(729, 566)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 15)
        Me.Label1.TabIndex = 155
        Me.Label1.Text = "Code"
        '
        'cmbAssortCode
        '
        Me.cmbAssortCode.FormattingEnabled = True
        Me.cmbAssortCode.IntegralHeight = False
        Me.cmbAssortCode.Location = New System.Drawing.Point(770, 566)
        Me.cmbAssortCode.MaxLength = 3
        Me.cmbAssortCode.Name = "cmbAssortCode"
        Me.cmbAssortCode.Size = New System.Drawing.Size(76, 23)
        Me.cmbAssortCode.TabIndex = 154
        '
        'txtCts
        '
        Me.txtCts.Location = New System.Drawing.Point(313, 568)
        Me.txtCts.Name = "txtCts"
        Me.txtCts.ReadOnly = True
        Me.txtCts.Size = New System.Drawing.Size(102, 21)
        Me.txtCts.TabIndex = 83
        Me.txtCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPcs
        '
        Me.txtPcs.Location = New System.Drawing.Point(205, 568)
        Me.txtPcs.Name = "txtPcs"
        Me.txtPcs.ReadOnly = True
        Me.txtPcs.Size = New System.Drawing.Size(102, 21)
        Me.txtPcs.TabIndex = 82
        Me.txtPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'pnlDetails2
        '
        Me.pnlDetails2.BackColor = System.Drawing.Color.White
        Me.pnlDetails2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDetails2.Controls.Add(Me.flxDetails)
        Me.pnlDetails2.Location = New System.Drawing.Point(3, 110)
        Me.pnlDetails2.Name = "pnlDetails2"
        Me.pnlDetails2.Size = New System.Drawing.Size(1196, 452)
        Me.pnlDetails2.TabIndex = 81
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Code, Me.PktNo, Me.Pcs, Me.Cts, Me.Column3, Me.Assortment, Me.OrigAssort, Me.Price, Me.InID, Me.ImportNo, Me.OldAssort, Me.Select1, Me.ID, Me.AvgCost, Me.RejDate, Me.Column1, Me.Column2, Me.Column4})
        Me.flxDetails.Location = New System.Drawing.Point(3, 3)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.flxDetails.Size = New System.Drawing.Size(1188, 441)
        Me.flxDetails.TabIndex = 44
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSelect)
        Me.HazelDev_Panel1.Controls.Add(Me.Type)
        Me.HazelDev_Panel1.Controls.Add(Me.cmbAssortType)
        Me.HazelDev_Panel1.Controls.Add(Me.chkSelect)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdFind)
        Me.HazelDev_Panel1.Controls.Add(Me.chkAll)
        Me.HazelDev_Panel1.Controls.Add(Me.Label8)
        Me.HazelDev_Panel1.Controls.Add(Me.dtpDate)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExcel)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(1196, 50)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
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
        Me.cmdSelect.Location = New System.Drawing.Point(849, 12)
        Me.cmdSelect.Name = "cmdSelect"
        Me.cmdSelect.Size = New System.Drawing.Size(99, 28)
        Me.cmdSelect.TabIndex = 166
        Me.cmdSelect.Text = "Select"
        Me.cmdSelect.UseVisualStyleBackColor = False
        '
        'Type
        '
        Me.Type.AutoSize = True
        Me.Type.Location = New System.Drawing.Point(705, 14)
        Me.Type.Name = "Type"
        Me.Type.Size = New System.Drawing.Size(56, 14)
        Me.Type.TabIndex = 165
        Me.Type.Text = "Category"
        '
        'cmbAssortType
        '
        Me.cmbAssortType.FormattingEnabled = True
        Me.cmbAssortType.IntegralHeight = False
        Me.cmbAssortType.Location = New System.Drawing.Point(767, 14)
        Me.cmbAssortType.MaxLength = 3
        Me.cmbAssortType.Name = "cmbAssortType"
        Me.cmbAssortType.Size = New System.Drawing.Size(76, 22)
        Me.cmbAssortType.TabIndex = 164
        '
        'chkSelect
        '
        Me.chkSelect.Checked = False
        Me.chkSelect.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkSelect.Location = New System.Drawing.Point(977, 20)
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Size = New System.Drawing.Size(84, 16)
        Me.chkSelect.TabIndex = 163
        Me.chkSelect.Text = "Select All"
        Me.chkSelect.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmdFind
        '
        Me.cmdFind.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdFind.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdFind.FlatAppearance.BorderSize = 0
        Me.cmdFind.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdFind.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdFind.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFind.ForeColor = System.Drawing.Color.White
        Me.cmdFind.Location = New System.Drawing.Point(451, 12)
        Me.cmdFind.Name = "cmdFind"
        Me.cmdFind.Size = New System.Drawing.Size(99, 28)
        Me.cmdFind.TabIndex = 101
        Me.cmdFind.Text = "Find"
        Me.cmdFind.UseVisualStyleBackColor = False
        '
        'chkAll
        '
        Me.chkAll.Checked = False
        Me.chkAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkAll.Location = New System.Drawing.Point(323, 19)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(105, 17)
        Me.chkAll.TabIndex = 100
        Me.chkAll.Text = "ALL"
        Me.chkAll.TransparencyKey = System.Drawing.Color.Empty
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(109, 19)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 14)
        Me.Label8.TabIndex = 99
        Me.Label8.Text = "Reject Date"
        '
        'dtpDate
        '
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate.Location = New System.Drawing.Point(187, 14)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(102, 22)
        Me.dtpDate.TabIndex = 98
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
        Me.cmdExcel.Location = New System.Drawing.Point(1093, 12)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(99, 28)
        Me.cmdExcel.TabIndex = 46
        Me.cmdExcel.Text = "Add to Stock"
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
        Me.cmdSave.Location = New System.Drawing.Point(600, 12)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(99, 28)
        Me.cmdSave.TabIndex = 45
        Me.cmdSave.Text = "Excel"
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
        'Code
        '
        Me.Code.HeaderText = "Ord No"
        Me.Code.Name = "Code"
        Me.Code.ReadOnly = True
        '
        'PktNo
        '
        Me.PktNo.HeaderText = "Pkt No"
        Me.PktNo.Name = "PktNo"
        Me.PktNo.ReadOnly = True
        '
        'Pcs
        '
        Me.Pcs.HeaderText = "Pcs"
        Me.Pcs.Name = "Pcs"
        Me.Pcs.ReadOnly = True
        '
        'Cts
        '
        Me.Cts.HeaderText = "Cts"
        Me.Cts.Name = "Cts"
        Me.Cts.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.HeaderText = "Act Cts"
        Me.Column3.Name = "Column3"
        '
        'Assortment
        '
        Me.Assortment.HeaderText = "Assortment"
        Me.Assortment.Name = "Assortment"
        Me.Assortment.ReadOnly = True
        Me.Assortment.Width = 160
        '
        'OrigAssort
        '
        Me.OrigAssort.HeaderText = "Orig Assort"
        Me.OrigAssort.Name = "OrigAssort"
        Me.OrigAssort.ReadOnly = True
        '
        'Price
        '
        Me.Price.HeaderText = "Price"
        Me.Price.Name = "Price"
        Me.Price.ReadOnly = True
        '
        'InID
        '
        Me.InID.HeaderText = "In"
        Me.InID.Name = "InID"
        Me.InID.ReadOnly = True
        Me.InID.Width = 30
        '
        'ImportNo
        '
        Me.ImportNo.HeaderText = "Im"
        Me.ImportNo.Name = "ImportNo"
        Me.ImportNo.ReadOnly = True
        Me.ImportNo.Width = 30
        '
        'OldAssort
        '
        Me.OldAssort.HeaderText = "Old Assort"
        Me.OldAssort.Name = "OldAssort"
        Me.OldAssort.ReadOnly = True
        '
        'Select1
        '
        Me.Select1.HeaderText = "Select"
        Me.Select1.Name = "Select1"
        '
        'ID
        '
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        Me.ID.ReadOnly = True
        Me.ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'AvgCost
        '
        Me.AvgCost.HeaderText = "Avg Cost"
        Me.AvgCost.Name = "AvgCost"
        Me.AvgCost.ReadOnly = True
        Me.AvgCost.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'RejDate
        '
        Me.RejDate.HeaderText = "Convert Date"
        Me.RejDate.Name = "RejDate"
        Me.RejDate.ReadOnly = True
        '
        'Column1
        '
        Me.Column1.HeaderText = "Sup Par No"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "Origin"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.HeaderText = "Reject Date"
        Me.Column4.Name = "Column4"
        '
        'frm_MixAssortRejectVerify
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1205, 593)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_MixAssortRejectVerify"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mix Reject Verification"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        Me.pnlDetails2.ResumeLayout(False)
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents pnlDetails2 As System.Windows.Forms.Panel
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents chkAll As DiaStock.HazelDev_CheckBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmdFind As DiaStock.HazelDev_Button
    Friend WithEvents chkSelect As DiaStock.HazelDev_CheckBox
    Friend WithEvents txtCts As System.Windows.Forms.TextBox
    Friend WithEvents txtPcs As System.Windows.Forms.TextBox
    Friend WithEvents Type As System.Windows.Forms.Label
    Friend WithEvents cmbAssortType As System.Windows.Forms.ComboBox
    Friend WithEvents cmdSelect As DiaStock.HazelDev_Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbAssortCode As System.Windows.Forms.ComboBox
    Friend WithEvents cmdSelect2 As DiaStock.HazelDev_Button
    Friend WithEvents Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PktNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Assortment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrigAssort As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ImportNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OldAssort As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Select1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AvgCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RejDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
