<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DCLWeightLoss
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
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtWtLoss = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtSupParNo = New System.Windows.Forms.TextBox()
        Me.pnlDetails2 = New System.Windows.Forms.Panel()
        Me.txtRghCts = New System.Windows.Forms.TextBox()
        Me.txtIssCts = New System.Windows.Forms.TextBox()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.Department = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IssCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RghCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Perc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WtLoss = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GrpParNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        Me.pnlDetails2.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtWtLoss)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label5)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtSupParNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.pnlDetails2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(861, 526)
        Me.HazelDev_ThemeContainer1.TabIndex = 7
        Me.HazelDev_ThemeContainer1.Text = "WEIGHT LOSS"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(416, 110)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 15)
        Me.Label1.TabIndex = 72
        Me.Label1.Text = "Weight Loss"
        '
        'txtWtLoss
        '
        Me.txtWtLoss.Location = New System.Drawing.Point(521, 107)
        Me.txtWtLoss.Name = "txtWtLoss"
        Me.txtWtLoss.Size = New System.Drawing.Size(118, 21)
        Me.txtWtLoss.TabIndex = 71
        Me.txtWtLoss.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(7, 113)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 15)
        Me.Label5.TabIndex = 70
        Me.Label5.Text = "Sup Par No"
        '
        'txtSupParNo
        '
        Me.txtSupParNo.Location = New System.Drawing.Point(112, 110)
        Me.txtSupParNo.Name = "txtSupParNo"
        Me.txtSupParNo.Size = New System.Drawing.Size(118, 21)
        Me.txtSupParNo.TabIndex = 0
        '
        'pnlDetails2
        '
        Me.pnlDetails2.BackColor = System.Drawing.Color.White
        Me.pnlDetails2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDetails2.Controls.Add(Me.txtRghCts)
        Me.pnlDetails2.Controls.Add(Me.txtIssCts)
        Me.pnlDetails2.Controls.Add(Me.flxDetails)
        Me.pnlDetails2.Location = New System.Drawing.Point(3, 137)
        Me.pnlDetails2.Name = "pnlDetails2"
        Me.pnlDetails2.Size = New System.Drawing.Size(852, 386)
        Me.pnlDetails2.TabIndex = 68
        '
        'txtRghCts
        '
        Me.txtRghCts.Location = New System.Drawing.Point(406, 350)
        Me.txtRghCts.Name = "txtRghCts"
        Me.txtRghCts.ReadOnly = True
        Me.txtRghCts.Size = New System.Drawing.Size(92, 21)
        Me.txtRghCts.TabIndex = 96
        Me.txtRghCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtIssCts
        '
        Me.txtIssCts.Location = New System.Drawing.Point(308, 350)
        Me.txtIssCts.Name = "txtIssCts"
        Me.txtIssCts.ReadOnly = True
        Me.txtIssCts.Size = New System.Drawing.Size(92, 21)
        Me.txtIssCts.TabIndex = 95
        Me.txtIssCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Department, Me.ParNo, Me.IssCts, Me.RghCts, Me.Perc, Me.WtLoss, Me.GrpParNo})
        Me.flxDetails.Location = New System.Drawing.Point(3, 3)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.ReadOnly = True
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(839, 341)
        Me.flxDetails.TabIndex = 44
        '
        'Department
        '
        Me.Department.HeaderText = "Department"
        Me.Department.Name = "Department"
        Me.Department.ReadOnly = True
        Me.Department.Width = 150
        '
        'ParNo
        '
        Me.ParNo.HeaderText = "Parcel No"
        Me.ParNo.Name = "ParNo"
        Me.ParNo.ReadOnly = True
        Me.ParNo.Width = 150
        '
        'IssCts
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.IssCts.DefaultCellStyle = DataGridViewCellStyle1
        Me.IssCts.HeaderText = "Iss Cts"
        Me.IssCts.Name = "IssCts"
        Me.IssCts.ReadOnly = True
        '
        'RghCts
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.RghCts.DefaultCellStyle = DataGridViewCellStyle2
        Me.RghCts.HeaderText = "Rgh Cts"
        Me.RghCts.Name = "RghCts"
        Me.RghCts.ReadOnly = True
        '
        'Perc
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Perc.DefaultCellStyle = DataGridViewCellStyle3
        Me.Perc.HeaderText = "Perc %"
        Me.Perc.Name = "Perc"
        Me.Perc.ReadOnly = True
        '
        'WtLoss
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.WtLoss.DefaultCellStyle = DataGridViewCellStyle4
        Me.WtLoss.HeaderText = "Wt Loss"
        Me.WtLoss.Name = "WtLoss"
        Me.WtLoss.ReadOnly = True
        '
        'GrpParNo
        '
        Me.GrpParNo.HeaderText = "Grp Par No"
        Me.GrpParNo.Name = "GrpParNo"
        Me.GrpParNo.ReadOnly = True
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
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(852, 50)
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
        'frm_DCLWeightLoss
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(861, 526)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_DCLWeightLoss"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Weight Loss"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        Me.pnlDetails2.ResumeLayout(False)
        Me.pnlDetails2.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents pnlDetails2 As System.Windows.Forms.Panel
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSupParNo As System.Windows.Forms.TextBox
    Friend WithEvents txtRghCts As System.Windows.Forms.TextBox
    Friend WithEvents txtIssCts As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtWtLoss As System.Windows.Forms.TextBox
    Friend WithEvents Department As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IssCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RghCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Perc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WtLoss As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GrpParNo As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
