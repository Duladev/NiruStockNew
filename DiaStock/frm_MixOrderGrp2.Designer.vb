<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_MixOrderGrp2
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.chkALL = New DiaStock.HazelDev_CheckBox()
        Me.pnlDetails2 = New System.Windows.Forms.Panel()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Subject = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Dept = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Select1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ParSize = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Urgent = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DueDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkFull = New DiaStock.HazelDev_CheckBox()
        Me.chkDate = New DiaStock.HazelDev_CheckBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.dtpOrdDate = New System.Windows.Forms.DateTimePicker()
        Me.cmdExcel = New DiaStock.HazelDev_Button()
        Me.cmdRefresh = New DiaStock.HazelDev_Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbSubject = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmbDept = New System.Windows.Forms.ComboBox()
        Me.cmdSave2 = New DiaStock.HazelDev_Button()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.chkSelect = New DiaStock.HazelDev_CheckBox()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        Me.pnlDetails2.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkSelect)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkALL)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.pnlDetails2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(1267, 594)
        Me.HazelDev_ThemeContainer1.TabIndex = 8
        Me.HazelDev_ThemeContainer1.Text = "MIX ORDER GROUP NEW"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'chkALL
        '
        Me.chkALL.Checked = False
        Me.chkALL.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkALL.Location = New System.Drawing.Point(3, 568)
        Me.chkALL.Name = "chkALL"
        Me.chkALL.Size = New System.Drawing.Size(65, 22)
        Me.chkALL.TabIndex = 184
        Me.chkALL.Text = "ALL"
        Me.chkALL.TransparencyKey = System.Drawing.Color.Empty
        '
        'pnlDetails2
        '
        Me.pnlDetails2.BackColor = System.Drawing.Color.White
        Me.pnlDetails2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDetails2.Controls.Add(Me.flxDetails)
        Me.pnlDetails2.Location = New System.Drawing.Point(3, 110)
        Me.pnlDetails2.Name = "pnlDetails2"
        Me.pnlDetails2.Size = New System.Drawing.Size(1261, 452)
        Me.pnlDetails2.TabIndex = 81
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Code, Me.Subject, Me.Column7, Me.Column10, Me.Dept, Me.Column13, Me.Select1, Me.ParSize, Me.Urgent, Me.DueDate, Me.Column5, Me.Column8, Me.Column9})
        Me.flxDetails.Location = New System.Drawing.Point(3, 3)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.flxDetails.Size = New System.Drawing.Size(1253, 441)
        Me.flxDetails.TabIndex = 44
        '
        'Code
        '
        Me.Code.HeaderText = "Ord No"
        Me.Code.Name = "Code"
        Me.Code.ReadOnly = True
        '
        'Subject
        '
        Me.Subject.HeaderText = "Subject"
        Me.Subject.Name = "Subject"
        Me.Subject.ReadOnly = True
        Me.Subject.Width = 150
        '
        'Column7
        '
        Me.Column7.HeaderText = "Subject 2"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Width = 150
        '
        'Column10
        '
        Me.Column10.HeaderText = "Laser"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        '
        'Dept
        '
        Me.Dept.HeaderText = "Dept"
        Me.Dept.Name = "Dept"
        Me.Dept.ReadOnly = True
        '
        'Column13
        '
        Me.Column13.HeaderText = "Prev. Dept."
        Me.Column13.Name = "Column13"
        '
        'Select1
        '
        Me.Select1.HeaderText = "Select"
        Me.Select1.Name = "Select1"
        '
        'ParSize
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ParSize.DefaultCellStyle = DataGridViewCellStyle2
        Me.ParSize.HeaderText = "Ord Pcs"
        Me.ParSize.Name = "ParSize"
        Me.ParSize.ReadOnly = True
        '
        'Urgent
        '
        Me.Urgent.HeaderText = "Urgent"
        Me.Urgent.Name = "Urgent"
        '
        'DueDate
        '
        Me.DueDate.HeaderText = "Due Date"
        Me.DueDate.Name = "DueDate"
        Me.DueDate.ReadOnly = True
        '
        'Column5
        '
        Me.Column5.HeaderText = "Client No"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        '
        'Column8
        '
        Me.Column8.HeaderText = "Commande"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        '
        'Column9
        '
        Me.Column9.HeaderText = "Groove"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.Label2)
        Me.HazelDev_Panel1.Controls.Add(Me.chkFull)
        Me.HazelDev_Panel1.Controls.Add(Me.chkDate)
        Me.HazelDev_Panel1.Controls.Add(Me.Label20)
        Me.HazelDev_Panel1.Controls.Add(Me.dtpOrdDate)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExcel)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdRefresh)
        Me.HazelDev_Panel1.Controls.Add(Me.Label1)
        Me.HazelDev_Panel1.Controls.Add(Me.cmbSubject)
        Me.HazelDev_Panel1.Controls.Add(Me.Label11)
        Me.HazelDev_Panel1.Controls.Add(Me.cmbDept)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave2)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(1261, 50)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(1118, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(24, 14)
        Me.Label2.TabIndex = 184
        Me.Label2.Text = "Full"
        '
        'chkFull
        '
        Me.chkFull.Checked = False
        Me.chkFull.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkFull.Location = New System.Drawing.Point(1121, 21)
        Me.chkFull.Name = "chkFull"
        Me.chkFull.Size = New System.Drawing.Size(26, 22)
        Me.chkFull.TabIndex = 183
        Me.chkFull.TransparencyKey = System.Drawing.Color.Empty
        '
        'chkDate
        '
        Me.chkDate.Checked = False
        Me.chkDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkDate.Location = New System.Drawing.Point(1091, 21)
        Me.chkDate.Name = "chkDate"
        Me.chkDate.Size = New System.Drawing.Size(26, 22)
        Me.chkDate.TabIndex = 182
        Me.chkDate.TransparencyKey = System.Drawing.Color.Empty
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Location = New System.Drawing.Point(982, 3)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(59, 14)
        Me.Label20.TabIndex = 181
        Me.Label20.Text = "Plan Date"
        '
        'dtpOrdDate
        '
        Me.dtpOrdDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpOrdDate.Location = New System.Drawing.Point(983, 21)
        Me.dtpOrdDate.Name = "dtpOrdDate"
        Me.dtpOrdDate.Size = New System.Drawing.Size(102, 22)
        Me.dtpOrdDate.TabIndex = 180
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
        Me.cmdExcel.Location = New System.Drawing.Point(878, 12)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(99, 28)
        Me.cmdExcel.TabIndex = 82
        Me.cmdExcel.Text = "Excel"
        Me.cmdExcel.UseVisualStyleBackColor = False
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
        Me.cmdRefresh.Location = New System.Drawing.Point(109, 13)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(99, 28)
        Me.cmdRefresh.TabIndex = 81
        Me.cmdRefresh.Text = "Refresh"
        Me.cmdRefresh.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(542, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 14)
        Me.Label1.TabIndex = 80
        Me.Label1.Text = "Subject"
        '
        'cmbSubject
        '
        Me.cmbSubject.FormattingEnabled = True
        Me.cmbSubject.IntegralHeight = False
        Me.cmbSubject.Location = New System.Drawing.Point(597, 17)
        Me.cmbSubject.Name = "cmbSubject"
        Me.cmbSubject.Size = New System.Drawing.Size(275, 22)
        Me.cmbSubject.TabIndex = 79
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Location = New System.Drawing.Point(225, 19)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(73, 14)
        Me.Label11.TabIndex = 78
        Me.Label11.Text = "Department"
        '
        'cmbDept
        '
        Me.cmbDept.FormattingEnabled = True
        Me.cmbDept.IntegralHeight = False
        Me.cmbDept.Location = New System.Drawing.Point(304, 19)
        Me.cmbDept.Name = "cmbDept"
        Me.cmbDept.Size = New System.Drawing.Size(109, 22)
        Me.cmbDept.TabIndex = 77
        '
        'cmdSave2
        '
        Me.cmdSave2.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSave2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSave2.FlatAppearance.BorderSize = 0
        Me.cmdSave2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdSave2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdSave2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSave2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave2.ForeColor = System.Drawing.Color.White
        Me.cmdSave2.Location = New System.Drawing.Point(1153, 12)
        Me.cmdSave2.Name = "cmdSave2"
        Me.cmdSave2.Size = New System.Drawing.Size(99, 28)
        Me.cmdSave2.TabIndex = 46
        Me.cmdSave2.Text = "Save Urgent"
        Me.cmdSave2.UseVisualStyleBackColor = False
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
        Me.cmdSave.Location = New System.Drawing.Point(437, 15)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(99, 28)
        Me.cmdSave.TabIndex = 45
        Me.cmdSave.Text = "Save Dept"
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
        'chkSelect
        '
        Me.chkSelect.Checked = False
        Me.chkSelect.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkSelect.Location = New System.Drawing.Point(74, 568)
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Size = New System.Drawing.Size(84, 16)
        Me.chkSelect.TabIndex = 185
        Me.chkSelect.Text = "Select All"
        Me.chkSelect.TransparencyKey = System.Drawing.Color.Empty
        '
        'frm_MixOrderGrp2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1267, 594)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_MixOrderGrp2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mix Order Group New"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
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
    Friend WithEvents cmdSave2 As DiaStock.HazelDev_Button
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmbDept As System.Windows.Forms.ComboBox
    Friend WithEvents cmdRefresh As DiaStock.HazelDev_Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbSubject As System.Windows.Forms.ComboBox
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents dtpOrdDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkDate As DiaStock.HazelDev_CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkFull As DiaStock.HazelDev_CheckBox
    Friend WithEvents chkALL As DiaStock.HazelDev_CheckBox
    Friend WithEvents Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Subject As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Dept As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Select1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ParSize As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Urgent As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DueDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkSelect As DiaStock.HazelDev_CheckBox
End Class
