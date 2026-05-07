<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_SYSUserRights
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_SYSUserRights))
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.HazelDev_Panel2 = New DiaStock.HazelDev_Panel()
        Me.chkAll = New DiaStock.HazelDev_CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.flxDept = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbUser = New MTGCComboBox()
        Me.txtEmpNo = New System.Windows.Forms.TextBox()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.Code = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DepartmentName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FormName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        Me.HazelDev_Panel2.SuspendLayout()
        CType(Me.flxDept, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(502, 483)
        Me.HazelDev_ThemeContainer1.TabIndex = 7
        Me.HazelDev_ThemeContainer1.Text = "USER RIGHTS"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'HazelDev_Panel2
        '
        Me.HazelDev_Panel2.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel2.Controls.Add(Me.chkAll)
        Me.HazelDev_Panel2.Controls.Add(Me.Label1)
        Me.HazelDev_Panel2.Controls.Add(Me.flxDept)
        Me.HazelDev_Panel2.Controls.Add(Me.Label2)
        Me.HazelDev_Panel2.Controls.Add(Me.cmbUser)
        Me.HazelDev_Panel2.Controls.Add(Me.txtEmpNo)
        Me.HazelDev_Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel2.Location = New System.Drawing.Point(3, 110)
        Me.HazelDev_Panel2.Name = "HazelDev_Panel2"
        Me.HazelDev_Panel2.Size = New System.Drawing.Size(491, 363)
        Me.HazelDev_Panel2.TabIndex = 97
        Me.HazelDev_Panel2.Text = "HazelDev_Panel2"
        Me.HazelDev_Panel2.TransparencyKey = System.Drawing.Color.Empty
        '
        'chkAll
        '
        Me.chkAll.Checked = False
        Me.chkAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkAll.Location = New System.Drawing.Point(399, 26)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(70, 21)
        Me.chkAll.TabIndex = 97
        Me.chkAll.Text = "ALL"
        Me.chkAll.TransparencyKey = System.Drawing.Color.Empty
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(12, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 14)
        Me.Label1.TabIndex = 91
        Me.Label1.Text = "Users"
        '
        'flxDept
        '
        Me.flxDept.AllowUserToAddRows = False
        Me.flxDept.AllowUserToDeleteRows = False
        Me.flxDept.AllowUserToResizeColumns = False
        Me.flxDept.AllowUserToResizeRows = False
        Me.flxDept.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDept.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Code, Me.Column1, Me.DepartmentName, Me.FormName})
        Me.flxDept.Location = New System.Drawing.Point(15, 55)
        Me.flxDept.Name = "flxDept"
        Me.flxDept.RowHeadersVisible = False
        Me.flxDept.Size = New System.Drawing.Size(463, 298)
        Me.flxDept.TabIndex = 94
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(315, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 14)
        Me.Label2.TabIndex = 93
        Me.Label2.Text = "Emp No"
        '
        'cmbUser
        '
        Me.cmbUser.ArrowBoxColor = System.Drawing.SystemColors.Control
        Me.cmbUser.ArrowColor = System.Drawing.Color.Black
        Me.cmbUser.BindedControl = CType(resources.GetObject("cmbUser.BindedControl"), MTGCComboBox.ControlloAssociato)
        Me.cmbUser.BorderStyle = MTGCComboBox.TipiBordi.Fixed3D
        Me.cmbUser.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.cmbUser.ColumnNum = 2
        Me.cmbUser.ColumnWidth = "200; 32"
        Me.cmbUser.DisabledArrowBoxColor = System.Drawing.SystemColors.Control
        Me.cmbUser.DisabledArrowColor = System.Drawing.Color.LightGray
        Me.cmbUser.DisabledBackColor = System.Drawing.SystemColors.Control
        Me.cmbUser.DisabledBorderColor = System.Drawing.SystemColors.InactiveBorder
        Me.cmbUser.DisabledForeColor = System.Drawing.SystemColors.GrayText
        Me.cmbUser.DisplayMember = "Text"
        Me.cmbUser.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbUser.DropDownBackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.cmbUser.DropDownForeColor = System.Drawing.Color.Black
        Me.cmbUser.DropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
        Me.cmbUser.DropDownWidth = 272
        Me.cmbUser.GridLineColor = System.Drawing.Color.LightGray
        Me.cmbUser.GridLineHorizontal = False
        Me.cmbUser.GridLineVertical = False
        Me.cmbUser.IntegralHeight = False
        Me.cmbUser.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.cmbUser.Location = New System.Drawing.Point(15, 26)
        Me.cmbUser.ManagingFastMouseMoving = True
        Me.cmbUser.ManagingFastMouseMovingInterval = 30
        Me.cmbUser.Name = "cmbUser"
        Me.cmbUser.SelectedItem = Nothing
        Me.cmbUser.SelectedValue = Nothing
        Me.cmbUser.Size = New System.Drawing.Size(272, 23)
        Me.cmbUser.TabIndex = 96
        '
        'txtEmpNo
        '
        Me.txtEmpNo.Enabled = False
        Me.txtEmpNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpNo.Location = New System.Drawing.Point(318, 26)
        Me.txtEmpNo.MaxLength = 4
        Me.txtEmpNo.Name = "txtEmpNo"
        Me.txtEmpNo.Size = New System.Drawing.Size(57, 21)
        Me.txtEmpNo.TabIndex = 92
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(491, 50)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
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
        Me.cmdSave.Location = New System.Drawing.Point(109, 13)
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
        'Code
        '
        Me.Code.HeaderText = "Select"
        Me.Code.Name = "Code"
        Me.Code.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Code.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Code.Width = 50
        '
        'Column1
        '
        Me.Column1.HeaderText = "Group"
        Me.Column1.Name = "Column1"
        '
        'DepartmentName
        '
        Me.DepartmentName.HeaderText = "Screen Name"
        Me.DepartmentName.Name = "DepartmentName"
        Me.DepartmentName.ReadOnly = True
        Me.DepartmentName.Width = 250
        '
        'FormName
        '
        Me.FormName.HeaderText = "Form Name"
        Me.FormName.Name = "FormName"
        Me.FormName.ReadOnly = True
        Me.FormName.Visible = False
        '
        'frm_SYSUserRights
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(502, 483)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_SYSUserRights"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "User Rights"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_Panel2.ResumeLayout(False)
        Me.HazelDev_Panel2.PerformLayout()
        CType(Me.flxDept, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents flxDept As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtEmpNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbUser As MTGCComboBox
    Friend WithEvents HazelDev_Panel2 As DiaStock.HazelDev_Panel
    Friend WithEvents chkAll As DiaStock.HazelDev_CheckBox
    Friend WithEvents Code As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DepartmentName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FormName As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
