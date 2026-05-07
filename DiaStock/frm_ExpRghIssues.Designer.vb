<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ExpRghIssues
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_ExpRghIssues))
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.optRough = New System.Windows.Forms.RadioButton()
        Me.optPcu = New System.Windows.Forms.RadioButton()
        Me.cmdExcel = New DiaStock.HazelDev_Button()
        Me.txtPcs = New System.Windows.Forms.TextBox()
        Me.txtCts = New System.Windows.Forms.TextBox()
        Me.txtAddPcs = New System.Windows.Forms.TextBox()
        Me.txtAddCts = New System.Windows.Forms.TextBox()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.Assortment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IssPcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IssCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Department = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PktNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmbParcel = New MTGCComboBox()
        Me.cmdRefresh = New DiaStock.HazelDev_Button()
        Me.Label6 = New System.Windows.Forms.Label()
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
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.optRough)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.optPcu)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdExcel)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtAddPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtAddCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxDetails)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(717, 659)
        Me.HazelDev_ThemeContainer1.TabIndex = 6
        Me.HazelDev_ThemeContainer1.Text = "SORTING ROUGH TRANSFER"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'optRough
        '
        Me.optRough.AutoSize = True
        Me.optRough.BackColor = System.Drawing.Color.White
        Me.optRough.Location = New System.Drawing.Point(602, 624)
        Me.optRough.Name = "optRough"
        Me.optRough.Size = New System.Drawing.Size(62, 19)
        Me.optRough.TabIndex = 163
        Me.optRough.Text = "Rough"
        Me.optRough.UseVisualStyleBackColor = False
        '
        'optPcu
        '
        Me.optPcu.AutoSize = True
        Me.optPcu.BackColor = System.Drawing.Color.White
        Me.optPcu.Checked = True
        Me.optPcu.Location = New System.Drawing.Point(546, 624)
        Me.optPcu.Name = "optPcu"
        Me.optPcu.Size = New System.Drawing.Size(50, 19)
        Me.optPcu.TabIndex = 162
        Me.optPcu.TabStop = True
        Me.optPcu.Text = "PCU"
        Me.optPcu.UseVisualStyleBackColor = False
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
        Me.cmdExcel.Location = New System.Drawing.Point(7, 623)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(99, 28)
        Me.cmdExcel.TabIndex = 161
        Me.cmdExcel.Text = "Excel"
        Me.cmdExcel.UseVisualStyleBackColor = False
        '
        'txtPcs
        '
        Me.txtPcs.Location = New System.Drawing.Point(366, 621)
        Me.txtPcs.Name = "txtPcs"
        Me.txtPcs.ReadOnly = True
        Me.txtPcs.Size = New System.Drawing.Size(77, 21)
        Me.txtPcs.TabIndex = 160
        Me.txtPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCts
        '
        Me.txtCts.Location = New System.Drawing.Point(449, 621)
        Me.txtCts.Name = "txtCts"
        Me.txtCts.ReadOnly = True
        Me.txtCts.Size = New System.Drawing.Size(77, 21)
        Me.txtCts.TabIndex = 159
        Me.txtCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAddPcs
        '
        Me.txtAddPcs.Location = New System.Drawing.Point(200, 621)
        Me.txtAddPcs.Name = "txtAddPcs"
        Me.txtAddPcs.ReadOnly = True
        Me.txtAddPcs.Size = New System.Drawing.Size(77, 21)
        Me.txtAddPcs.TabIndex = 158
        Me.txtAddPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAddCts
        '
        Me.txtAddCts.Location = New System.Drawing.Point(283, 621)
        Me.txtAddCts.Name = "txtAddCts"
        Me.txtAddCts.ReadOnly = True
        Me.txtAddCts.Size = New System.Drawing.Size(77, 21)
        Me.txtAddCts.TabIndex = 157
        Me.txtAddCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Assortment, Me.Code, Me.Pcs, Me.Cts, Me.IssPcs, Me.IssCts, Me.Department, Me.PktNo})
        Me.flxDetails.Location = New System.Drawing.Point(3, 109)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(707, 507)
        Me.flxDetails.TabIndex = 43
        '
        'Assortment
        '
        Me.Assortment.HeaderText = "Assortment"
        Me.Assortment.Name = "Assortment"
        Me.Assortment.ReadOnly = True
        '
        'Code
        '
        Me.Code.HeaderText = "Parcel No."
        Me.Code.Name = "Code"
        Me.Code.ReadOnly = True
        '
        'Pcs
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Pcs.DefaultCellStyle = DataGridViewCellStyle1
        Me.Pcs.HeaderText = "Pcs"
        Me.Pcs.Name = "Pcs"
        Me.Pcs.ReadOnly = True
        Me.Pcs.Width = 80
        '
        'Cts
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Cts.DefaultCellStyle = DataGridViewCellStyle2
        Me.Cts.HeaderText = "Cts"
        Me.Cts.Name = "Cts"
        Me.Cts.ReadOnly = True
        Me.Cts.Width = 80
        '
        'IssPcs
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.IssPcs.DefaultCellStyle = DataGridViewCellStyle3
        Me.IssPcs.HeaderText = "Iss. Pcs"
        Me.IssPcs.Name = "IssPcs"
        Me.IssPcs.Width = 80
        '
        'IssCts
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.IssCts.DefaultCellStyle = DataGridViewCellStyle4
        Me.IssCts.HeaderText = "Iss. Cts"
        Me.IssCts.Name = "IssCts"
        Me.IssCts.Width = 80
        '
        'Department
        '
        Me.Department.HeaderText = "Department"
        Me.Department.Name = "Department"
        Me.Department.ReadOnly = True
        Me.Department.Width = 85
        '
        'PktNo
        '
        Me.PktNo.HeaderText = "Pkt No"
        Me.PktNo.Name = "PktNo"
        Me.PktNo.ReadOnly = True
        Me.PktNo.Width = 70
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmbParcel)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdRefresh)
        Me.HazelDev_Panel1.Controls.Add(Me.Label6)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(707, 49)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmbParcel
        '
        Me.cmbParcel.ArrowBoxColor = System.Drawing.SystemColors.Control
        Me.cmbParcel.ArrowColor = System.Drawing.Color.Black
        Me.cmbParcel.BindedControl = CType(resources.GetObject("cmbParcel.BindedControl"), MTGCComboBox.ControlloAssociato)
        Me.cmbParcel.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.cmbParcel.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbParcel.ColumnNum = 2
        Me.cmbParcel.ColumnWidth = "80; 116"
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
        Me.cmbParcel.DropDownWidth = 196
        Me.cmbParcel.GridLineColor = System.Drawing.Color.LightGray
        Me.cmbParcel.GridLineHorizontal = False
        Me.cmbParcel.GridLineVertical = False
        Me.cmbParcel.IntegralHeight = False
        Me.cmbParcel.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.cmbParcel.Location = New System.Drawing.Point(388, 13)
        Me.cmbParcel.ManagingFastMouseMoving = True
        Me.cmbParcel.ManagingFastMouseMovingInterval = 30
        Me.cmbParcel.Name = "cmbParcel"
        Me.cmbParcel.SelectedItem = Nothing
        Me.cmbParcel.SelectedValue = Nothing
        Me.cmbParcel.Size = New System.Drawing.Size(196, 23)
        Me.cmbParcel.TabIndex = 54
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
        Me.cmdRefresh.Location = New System.Drawing.Point(599, 13)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(99, 28)
        Me.cmdRefresh.TabIndex = 53
        Me.cmdRefresh.Text = "Refresh"
        Me.cmdRefresh.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(324, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(58, 14)
        Me.Label6.TabIndex = 51
        Me.Label6.Text = "Parcel No"
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
        'frm_ExpRghIssues
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(717, 659)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_ExpRghIssues"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sorting Rough Transfer"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents optRough As System.Windows.Forms.RadioButton
    Friend WithEvents optPcu As System.Windows.Forms.RadioButton
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents txtPcs As System.Windows.Forms.TextBox
    Friend WithEvents txtCts As System.Windows.Forms.TextBox
    Friend WithEvents txtAddPcs As System.Windows.Forms.TextBox
    Friend WithEvents txtAddCts As System.Windows.Forms.TextBox
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents Assortment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IssPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IssCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Department As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PktNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmbParcel As MTGCComboBox
    Friend WithEvents cmdRefresh As DiaStock.HazelDev_Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
End Class
