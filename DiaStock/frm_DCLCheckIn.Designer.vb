<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DCLCheckIn
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.txtCts = New System.Windows.Forms.TextBox()
        Me.txtPcs = New System.Windows.Forms.TextBox()
        Me.flxSelected = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.flxParcel = New System.Windows.Forms.DataGridView()
        Me.cmbDepartment = New System.Windows.Forms.ComboBox()
        Me.flxAvailable = New System.Windows.Forms.DataGridView()
        Me.DepartmentName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PktNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OutTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        CType(Me.flxSelected, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxParcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxAvailable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxSelected)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxParcel)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbDepartment)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxAvailable)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label6)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(628, 598)
        Me.HazelDev_ThemeContainer1.TabIndex = 3
        Me.HazelDev_ThemeContainer1.Text = "CHECKING TRANSFER"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'txtCts
        '
        Me.txtCts.Location = New System.Drawing.Point(508, 570)
        Me.txtCts.Name = "txtCts"
        Me.txtCts.ReadOnly = True
        Me.txtCts.Size = New System.Drawing.Size(74, 21)
        Me.txtCts.TabIndex = 133
        Me.txtCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPcs
        '
        Me.txtPcs.Location = New System.Drawing.Point(430, 570)
        Me.txtPcs.Name = "txtPcs"
        Me.txtPcs.ReadOnly = True
        Me.txtPcs.Size = New System.Drawing.Size(72, 21)
        Me.txtPcs.TabIndex = 132
        Me.txtPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'flxSelected
        '
        Me.flxSelected.AllowUserToAddRows = False
        Me.flxSelected.AllowUserToDeleteRows = False
        Me.flxSelected.AllowUserToResizeColumns = False
        Me.flxSelected.AllowUserToResizeRows = False
        Me.flxSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxSelected.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6})
        Me.flxSelected.Location = New System.Drawing.Point(246, 355)
        Me.flxSelected.Name = "flxSelected"
        Me.flxSelected.ReadOnly = True
        Me.flxSelected.RowHeadersVisible = False
        Me.flxSelected.Size = New System.Drawing.Size(377, 209)
        Me.flxSelected.TabIndex = 131
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Par No."
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "Pkt No"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 80
        '
        'DataGridViewTextBoxColumn5
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.NullValue = Nothing
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewTextBoxColumn5.HeaderText = "Pcs"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 80
        '
        'DataGridViewTextBoxColumn6
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.NullValue = Nothing
        Me.DataGridViewTextBoxColumn6.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewTextBoxColumn6.HeaderText = "Cts"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Width = 80
        '
        'flxParcel
        '
        Me.flxParcel.AllowUserToAddRows = False
        Me.flxParcel.AllowUserToDeleteRows = False
        Me.flxParcel.AllowUserToResizeColumns = False
        Me.flxParcel.AllowUserToResizeRows = False
        Me.flxParcel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxParcel.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3})
        Me.flxParcel.Location = New System.Drawing.Point(7, 140)
        Me.flxParcel.Name = "flxParcel"
        Me.flxParcel.ReadOnly = True
        Me.flxParcel.RowHeadersVisible = False
        Me.flxParcel.Size = New System.Drawing.Size(233, 451)
        Me.flxParcel.TabIndex = 68
        '
        'cmbDepartment
        '
        Me.cmbDepartment.FormattingEnabled = True
        Me.cmbDepartment.IntegralHeight = False
        Me.cmbDepartment.Location = New System.Drawing.Point(81, 109)
        Me.cmbDepartment.Name = "cmbDepartment"
        Me.cmbDepartment.Size = New System.Drawing.Size(159, 23)
        Me.cmbDepartment.TabIndex = 50
        '
        'flxAvailable
        '
        Me.flxAvailable.AllowUserToAddRows = False
        Me.flxAvailable.AllowUserToDeleteRows = False
        Me.flxAvailable.AllowUserToResizeColumns = False
        Me.flxAvailable.AllowUserToResizeRows = False
        Me.flxAvailable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxAvailable.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DepartmentName, Me.PktNo, Me.InTime, Me.OutTime})
        Me.flxAvailable.Location = New System.Drawing.Point(246, 140)
        Me.flxAvailable.Name = "flxAvailable"
        Me.flxAvailable.ReadOnly = True
        Me.flxAvailable.RowHeadersVisible = False
        Me.flxAvailable.Size = New System.Drawing.Size(377, 209)
        Me.flxAvailable.TabIndex = 43
        '
        'DepartmentName
        '
        Me.DepartmentName.HeaderText = "Par No."
        Me.DepartmentName.Name = "DepartmentName"
        Me.DepartmentName.ReadOnly = True
        '
        'PktNo
        '
        Me.PktNo.HeaderText = "Pkt No"
        Me.PktNo.Name = "PktNo"
        Me.PktNo.ReadOnly = True
        Me.PktNo.Width = 80
        '
        'InTime
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.NullValue = Nothing
        Me.InTime.DefaultCellStyle = DataGridViewCellStyle4
        Me.InTime.HeaderText = "Pcs"
        Me.InTime.Name = "InTime"
        Me.InTime.ReadOnly = True
        Me.InTime.Width = 80
        '
        'OutTime
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.NullValue = Nothing
        Me.OutTime.DefaultCellStyle = DataGridViewCellStyle5
        Me.OutTime.HeaderText = "Cts"
        Me.OutTime.Name = "OutTime"
        Me.OutTime.ReadOnly = True
        Me.OutTime.Width = 80
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(620, 49)
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
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(3, 106)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 15)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "Department"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Par No."
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.NullValue = Nothing
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn3.HeaderText = "Grp"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'frm_DCLCheckIn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(628, 598)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_DCLCheckIn"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Checking Transfer"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        CType(Me.flxSelected, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxParcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxAvailable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents txtCts As System.Windows.Forms.TextBox
    Friend WithEvents txtPcs As System.Windows.Forms.TextBox
    Friend WithEvents flxSelected As System.Windows.Forms.DataGridView
    Friend WithEvents flxParcel As System.Windows.Forms.DataGridView
    Friend WithEvents cmbDepartment As System.Windows.Forms.ComboBox
    Friend WithEvents flxAvailable As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DepartmentName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PktNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OutTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
