<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DCLLotApproval
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
        Me.pnlDetails2 = New System.Windows.Forms.Panel()
        Me.cmdSaveRnd = New DiaStock.HazelDev_Button()
        Me.cmdSaveFan = New DiaStock.HazelDev_Button()
        Me.cmdSaveRgh = New DiaStock.HazelDev_Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtLotNo = New System.Windows.Forms.TextBox()
        Me.flxRough = New System.Windows.Forms.DataGridView()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.LotNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Check = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.flxFancy = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.flxRounds = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewCheckBoxColumn2 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        Me.pnlDetails2.SuspendLayout()
        CType(Me.flxRough, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        CType(Me.flxFancy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxRounds, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.pnlDetails2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(531, 536)
        Me.HazelDev_ThemeContainer1.TabIndex = 7
        Me.HazelDev_ThemeContainer1.Text = "LOT APPROVAL REQUEST"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'pnlDetails2
        '
        Me.pnlDetails2.BackColor = System.Drawing.Color.White
        Me.pnlDetails2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDetails2.Controls.Add(Me.flxRounds)
        Me.pnlDetails2.Controls.Add(Me.flxFancy)
        Me.pnlDetails2.Controls.Add(Me.cmdSaveRnd)
        Me.pnlDetails2.Controls.Add(Me.cmdSaveFan)
        Me.pnlDetails2.Controls.Add(Me.cmdSaveRgh)
        Me.pnlDetails2.Controls.Add(Me.Label1)
        Me.pnlDetails2.Controls.Add(Me.txtLotNo)
        Me.pnlDetails2.Controls.Add(Me.flxRough)
        Me.pnlDetails2.Location = New System.Drawing.Point(3, 110)
        Me.pnlDetails2.Name = "pnlDetails2"
        Me.pnlDetails2.Size = New System.Drawing.Size(522, 422)
        Me.pnlDetails2.TabIndex = 68
        '
        'cmdSaveRnd
        '
        Me.cmdSaveRnd.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSaveRnd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSaveRnd.FlatAppearance.BorderSize = 0
        Me.cmdSaveRnd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdSaveRnd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdSaveRnd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSaveRnd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSaveRnd.ForeColor = System.Drawing.Color.White
        Me.cmdSaveRnd.Location = New System.Drawing.Point(344, 389)
        Me.cmdSaveRnd.Name = "cmdSaveRnd"
        Me.cmdSaveRnd.Size = New System.Drawing.Size(99, 28)
        Me.cmdSaveRnd.TabIndex = 80
        Me.cmdSaveRnd.Text = "Save Rounds"
        Me.cmdSaveRnd.UseVisualStyleBackColor = False
        '
        'cmdSaveFan
        '
        Me.cmdSaveFan.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSaveFan.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSaveFan.FlatAppearance.BorderSize = 0
        Me.cmdSaveFan.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdSaveFan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdSaveFan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSaveFan.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSaveFan.ForeColor = System.Drawing.Color.White
        Me.cmdSaveFan.Location = New System.Drawing.Point(175, 389)
        Me.cmdSaveFan.Name = "cmdSaveFan"
        Me.cmdSaveFan.Size = New System.Drawing.Size(99, 28)
        Me.cmdSaveFan.TabIndex = 79
        Me.cmdSaveFan.Text = "Save Fancy"
        Me.cmdSaveFan.UseVisualStyleBackColor = False
        '
        'cmdSaveRgh
        '
        Me.cmdSaveRgh.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSaveRgh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSaveRgh.FlatAppearance.BorderSize = 0
        Me.cmdSaveRgh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdSaveRgh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdSaveRgh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSaveRgh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSaveRgh.ForeColor = System.Drawing.Color.White
        Me.cmdSaveRgh.Location = New System.Drawing.Point(6, 389)
        Me.cmdSaveRgh.Name = "cmdSaveRgh"
        Me.cmdSaveRgh.Size = New System.Drawing.Size(99, 28)
        Me.cmdSaveRgh.TabIndex = 45
        Me.cmdSaveRgh.Text = "Save Rough"
        Me.cmdSaveRgh.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(9, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 15)
        Me.Label1.TabIndex = 78
        Me.Label1.Text = "Lot No."
        '
        'txtLotNo
        '
        Me.txtLotNo.Location = New System.Drawing.Point(61, 9)
        Me.txtLotNo.MaxLength = 8
        Me.txtLotNo.Name = "txtLotNo"
        Me.txtLotNo.Size = New System.Drawing.Size(108, 21)
        Me.txtLotNo.TabIndex = 0
        '
        'flxRough
        '
        Me.flxRough.AllowUserToAddRows = False
        Me.flxRough.AllowUserToDeleteRows = False
        Me.flxRough.AllowUserToResizeColumns = False
        Me.flxRough.AllowUserToResizeRows = False
        Me.flxRough.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxRough.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.LotNo, Me.Check})
        Me.flxRough.Location = New System.Drawing.Point(6, 42)
        Me.flxRough.Name = "flxRough"
        Me.flxRough.ReadOnly = True
        Me.flxRough.RowHeadersVisible = False
        Me.flxRough.Size = New System.Drawing.Size(163, 341)
        Me.flxRough.TabIndex = 44
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(522, 50)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
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
        'LotNo
        '
        Me.LotNo.HeaderText = "Lot No"
        Me.LotNo.Name = "LotNo"
        Me.LotNo.ReadOnly = True
        Me.LotNo.Width = 80
        '
        'Check
        '
        Me.Check.HeaderText = "Check"
        Me.Check.Name = "Check"
        Me.Check.ReadOnly = True
        Me.Check.Width = 50
        '
        'flxFancy
        '
        Me.flxFancy.AllowUserToAddRows = False
        Me.flxFancy.AllowUserToDeleteRows = False
        Me.flxFancy.AllowUserToResizeColumns = False
        Me.flxFancy.AllowUserToResizeRows = False
        Me.flxFancy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxFancy.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewCheckBoxColumn1})
        Me.flxFancy.Location = New System.Drawing.Point(175, 42)
        Me.flxFancy.Name = "flxFancy"
        Me.flxFancy.ReadOnly = True
        Me.flxFancy.RowHeadersVisible = False
        Me.flxFancy.Size = New System.Drawing.Size(163, 341)
        Me.flxFancy.TabIndex = 81
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Lot No"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 80
        '
        'DataGridViewCheckBoxColumn1
        '
        Me.DataGridViewCheckBoxColumn1.HeaderText = "Check"
        Me.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
        Me.DataGridViewCheckBoxColumn1.ReadOnly = True
        Me.DataGridViewCheckBoxColumn1.Width = 50
        '
        'flxRounds
        '
        Me.flxRounds.AllowUserToAddRows = False
        Me.flxRounds.AllowUserToDeleteRows = False
        Me.flxRounds.AllowUserToResizeColumns = False
        Me.flxRounds.AllowUserToResizeRows = False
        Me.flxRounds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxRounds.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn2, Me.DataGridViewCheckBoxColumn2})
        Me.flxRounds.Location = New System.Drawing.Point(344, 42)
        Me.flxRounds.Name = "flxRounds"
        Me.flxRounds.ReadOnly = True
        Me.flxRounds.RowHeadersVisible = False
        Me.flxRounds.Size = New System.Drawing.Size(163, 341)
        Me.flxRounds.TabIndex = 82
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Lot No"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 80
        '
        'DataGridViewCheckBoxColumn2
        '
        Me.DataGridViewCheckBoxColumn2.HeaderText = "Check"
        Me.DataGridViewCheckBoxColumn2.Name = "DataGridViewCheckBoxColumn2"
        Me.DataGridViewCheckBoxColumn2.ReadOnly = True
        Me.DataGridViewCheckBoxColumn2.Width = 50
        '
        'frm_DCLLotApproval
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(531, 536)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_DCLLotApproval"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Lot Approval Request"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.pnlDetails2.ResumeLayout(False)
        Me.pnlDetails2.PerformLayout()
        CType(Me.flxRough, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        CType(Me.flxFancy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxRounds, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents pnlDetails2 As System.Windows.Forms.Panel
    Friend WithEvents flxRough As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdSaveRgh As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtLotNo As System.Windows.Forms.TextBox
    Friend WithEvents cmdSaveRnd As DiaStock.HazelDev_Button
    Friend WithEvents cmdSaveFan As DiaStock.HazelDev_Button
    Friend WithEvents flxRounds As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn2 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents flxFancy As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents LotNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Check As System.Windows.Forms.DataGridViewCheckBoxColumn
End Class
