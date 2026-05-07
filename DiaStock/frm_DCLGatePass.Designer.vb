<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DCLGatePass
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
        Me.HazelDev_Panel2 = New DiaStock.HazelDev_Panel()
        Me.HazelDev_Button1 = New DiaStock.HazelDev_Button()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdAdd = New DiaStock.HazelDev_Button()
        Me.flxDept = New System.Windows.Forms.DataGridView()
        Me.Index = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DepartmentName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCts = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtPcs = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbDeptTo = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbDeptFrom = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDepartment = New System.Windows.Forms.TextBox()
        Me.txtEmpName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtEmpNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbReason = New System.Windows.Forms.ComboBox()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        Me.HazelDev_Panel2.SuspendLayout()
        Me.HazelDev_Panel1.SuspendLayout()
        CType(Me.flxDept, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(537, 425)
        Me.HazelDev_ThemeContainer1.TabIndex = 2
        Me.HazelDev_ThemeContainer1.Text = "GATE PASS (DIAMOND TRANSFER)"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'HazelDev_Panel2
        '
        Me.HazelDev_Panel2.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel2.Controls.Add(Me.HazelDev_Button1)
        Me.HazelDev_Panel2.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel2.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel2.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel2.Location = New System.Drawing.Point(3, 55)
        Me.HazelDev_Panel2.Name = "HazelDev_Panel2"
        Me.HazelDev_Panel2.Size = New System.Drawing.Size(530, 50)
        Me.HazelDev_Panel2.TabIndex = 32
        Me.HazelDev_Panel2.Text = "HazelDev_Panel2"
        Me.HazelDev_Panel2.TransparencyKey = System.Drawing.Color.Empty
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
        Me.HazelDev_Button1.Location = New System.Drawing.Point(423, 13)
        Me.HazelDev_Button1.Name = "HazelDev_Button1"
        Me.HazelDev_Button1.Size = New System.Drawing.Size(99, 28)
        Me.HazelDev_Button1.TabIndex = 46
        Me.HazelDev_Button1.Text = "Report"
        Me.HazelDev_Button1.UseVisualStyleBackColor = False
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
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmdAdd)
        Me.HazelDev_Panel1.Controls.Add(Me.flxDept)
        Me.HazelDev_Panel1.Controls.Add(Me.Label7)
        Me.HazelDev_Panel1.Controls.Add(Me.txtCts)
        Me.HazelDev_Panel1.Controls.Add(Me.Label8)
        Me.HazelDev_Panel1.Controls.Add(Me.txtPcs)
        Me.HazelDev_Panel1.Controls.Add(Me.Label6)
        Me.HazelDev_Panel1.Controls.Add(Me.cmbDeptTo)
        Me.HazelDev_Panel1.Controls.Add(Me.Label5)
        Me.HazelDev_Panel1.Controls.Add(Me.cmbDeptFrom)
        Me.HazelDev_Panel1.Controls.Add(Me.Label4)
        Me.HazelDev_Panel1.Controls.Add(Me.Label3)
        Me.HazelDev_Panel1.Controls.Add(Me.txtDepartment)
        Me.HazelDev_Panel1.Controls.Add(Me.txtEmpName)
        Me.HazelDev_Panel1.Controls.Add(Me.Label2)
        Me.HazelDev_Panel1.Controls.Add(Me.txtEmpNo)
        Me.HazelDev_Panel1.Controls.Add(Me.Label1)
        Me.HazelDev_Panel1.Controls.Add(Me.cmbReason)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 111)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(530, 310)
        Me.HazelDev_Panel1.TabIndex = 0
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdAdd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdAdd.FlatAppearance.BorderSize = 0
        Me.cmdAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAdd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.ForeColor = System.Drawing.Color.White
        Me.cmdAdd.Location = New System.Drawing.Point(427, 73)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(70, 28)
        Me.cmdAdd.TabIndex = 106
        Me.cmdAdd.Text = "Add"
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'flxDept
        '
        Me.flxDept.AllowUserToAddRows = False
        Me.flxDept.AllowUserToDeleteRows = False
        Me.flxDept.AllowUserToResizeColumns = False
        Me.flxDept.AllowUserToResizeRows = False
        Me.flxDept.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDept.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Index, Me.Code, Me.DepartmentName})
        Me.flxDept.Location = New System.Drawing.Point(9, 146)
        Me.flxDept.Name = "flxDept"
        Me.flxDept.ReadOnly = True
        Me.flxDept.RowHeadersVisible = False
        Me.flxDept.Size = New System.Drawing.Size(481, 151)
        Me.flxDept.TabIndex = 105
        '
        'Index
        '
        Me.Index.HeaderText = "Index"
        Me.Index.Name = "Index"
        Me.Index.ReadOnly = True
        Me.Index.Width = 50
        '
        'Code
        '
        Me.Code.HeaderText = "Emp No."
        Me.Code.Name = "Code"
        Me.Code.ReadOnly = True
        Me.Code.Width = 80
        '
        'DepartmentName
        '
        Me.DepartmentName.HeaderText = "Department Name"
        Me.DepartmentName.Name = "DepartmentName"
        Me.DepartmentName.ReadOnly = True
        Me.DepartmentName.Width = 250
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(395, 17)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(24, 14)
        Me.Label7.TabIndex = 104
        Me.Label7.Text = "Cts"
        '
        'txtCts
        '
        Me.txtCts.Location = New System.Drawing.Point(395, 34)
        Me.txtCts.MaxLength = 0
        Me.txtCts.Name = "txtCts"
        Me.txtCts.Size = New System.Drawing.Size(102, 22)
        Me.txtCts.TabIndex = 103
        Me.txtCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(284, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(25, 14)
        Me.Label8.TabIndex = 102
        Me.Label8.Text = "Pcs"
        '
        'txtPcs
        '
        Me.txtPcs.Location = New System.Drawing.Point(287, 34)
        Me.txtPcs.MaxLength = 0
        Me.txtPcs.Name = "txtPcs"
        Me.txtPcs.Size = New System.Drawing.Size(102, 22)
        Me.txtPcs.TabIndex = 101
        Me.txtPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(218, 60)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(22, 14)
        Me.Label6.TabIndex = 100
        Me.Label6.Text = "To"
        '
        'cmbDeptTo
        '
        Me.cmbDeptTo.FormattingEnabled = True
        Me.cmbDeptTo.Items.AddRange(New Object() {"NIGHT SHIFT OFF", "TO GO HOME", "TO GO HOME - SICK", "TO GO OUT & COME BACK", "TO GO OUT & COME BACK - FUNERAL", "TO GO OUT & COME BACK - WEDDING", "TO GO TO MOSQUE", "COMPANY PURPOSE", "Diamond Transfer to DCL", "Diamond Transfer to NLE "})
        Me.cmbDeptTo.Location = New System.Drawing.Point(218, 77)
        Me.cmbDeptTo.Name = "cmbDeptTo"
        Me.cmbDeptTo.Size = New System.Drawing.Size(203, 22)
        Me.cmbDeptTo.TabIndex = 99
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(9, 60)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(34, 14)
        Me.Label5.TabIndex = 98
        Me.Label5.Text = "From"
        '
        'cmbDeptFrom
        '
        Me.cmbDeptFrom.FormattingEnabled = True
        Me.cmbDeptFrom.Items.AddRange(New Object() {"NIGHT SHIFT OFF", "TO GO HOME", "TO GO HOME - SICK", "TO GO OUT & COME BACK", "TO GO OUT & COME BACK - FUNERAL", "TO GO OUT & COME BACK - WEDDING", "TO GO TO MOSQUE", "COMPANY PURPOSE", "Diamond Transfer to DCL", "Diamond Transfer to NLE "})
        Me.cmbDeptFrom.Location = New System.Drawing.Point(9, 77)
        Me.cmbDeptFrom.Name = "cmbDeptFrom"
        Me.cmbDeptFrom.Size = New System.Drawing.Size(203, 22)
        Me.cmbDeptFrom.TabIndex = 97
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(352, 102)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 14)
        Me.Label4.TabIndex = 96
        Me.Label4.Text = "Department"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(87, 102)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 14)
        Me.Label3.TabIndex = 95
        Me.Label3.Text = "Emp Name"
        '
        'txtDepartment
        '
        Me.txtDepartment.Enabled = False
        Me.txtDepartment.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepartment.Location = New System.Drawing.Point(355, 119)
        Me.txtDepartment.MaxLength = 4
        Me.txtDepartment.Name = "txtDepartment"
        Me.txtDepartment.ReadOnly = True
        Me.txtDepartment.Size = New System.Drawing.Size(135, 21)
        Me.txtDepartment.TabIndex = 94
        '
        'txtEmpName
        '
        Me.txtEmpName.Enabled = False
        Me.txtEmpName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpName.Location = New System.Drawing.Point(87, 119)
        Me.txtEmpName.MaxLength = 4
        Me.txtEmpName.Name = "txtEmpName"
        Me.txtEmpName.ReadOnly = True
        Me.txtEmpName.Size = New System.Drawing.Size(262, 21)
        Me.txtEmpName.TabIndex = 93
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(9, 102)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 14)
        Me.Label2.TabIndex = 92
        Me.Label2.Text = "Emp No"
        '
        'txtEmpNo
        '
        Me.txtEmpNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpNo.Location = New System.Drawing.Point(9, 119)
        Me.txtEmpNo.MaxLength = 6
        Me.txtEmpNo.Name = "txtEmpNo"
        Me.txtEmpNo.Size = New System.Drawing.Size(72, 21)
        Me.txtEmpNo.TabIndex = 91
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(9, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 14)
        Me.Label1.TabIndex = 90
        Me.Label1.Text = "Reason"
        '
        'cmbReason
        '
        Me.cmbReason.FormattingEnabled = True
        Me.cmbReason.Items.AddRange(New Object() {"Diamond Transfer DCL to NLE", "Diamond Transfer NLE to DCL "})
        Me.cmbReason.Location = New System.Drawing.Point(9, 34)
        Me.cmbReason.Name = "cmbReason"
        Me.cmbReason.Size = New System.Drawing.Size(272, 22)
        Me.cmbReason.TabIndex = 62
        '
        'frm_DCLGatePass
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(537, 425)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_DCLGatePass"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Gate Pass"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_Panel2.ResumeLayout(False)
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        CType(Me.flxDept, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents HazelDev_Panel2 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents cmbReason As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDepartment As System.Windows.Forms.TextBox
    Friend WithEvents txtEmpName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtEmpNo As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbDeptTo As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbDeptFrom As System.Windows.Forms.ComboBox
    Friend WithEvents HazelDev_Button1 As DiaStock.HazelDev_Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCts As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtPcs As System.Windows.Forms.TextBox
    Friend WithEvents flxDept As System.Windows.Forms.DataGridView
    Friend WithEvents Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DepartmentName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdAdd As DiaStock.HazelDev_Button
End Class
