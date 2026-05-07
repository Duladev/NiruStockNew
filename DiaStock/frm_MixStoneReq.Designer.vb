<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_MixStoneReq
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
        Me.ExpProgress = New System.Windows.Forms.ProgressBar()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.Length = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Width1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OrderPcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IssuePcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RejPcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BalPcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BoxPcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SelectPcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RequirePcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BagAssort = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.txtMax = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtMin = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdExcel = New DiaStock.HazelDev_Button()
        Me.cmdFind = New DiaStock.HazelDev_Button()
        Me.cmdClear = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
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
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(1045, 665)
        Me.HazelDev_ThemeContainer1.TabIndex = 7
        Me.HazelDev_ThemeContainer1.Text = "MIX STONE REQUIREMENT"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'ExpProgress
        '
        Me.ExpProgress.Location = New System.Drawing.Point(6, 634)
        Me.ExpProgress.Name = "ExpProgress"
        Me.ExpProgress.Size = New System.Drawing.Size(1033, 24)
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
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Length, Me.Width1, Me.OrderPcs, Me.IssuePcs, Me.RejPcs, Me.BalPcs, Me.BoxPcs, Me.SelectPcs, Me.RequirePcs, Me.BagAssort})
        Me.flxDetails.Location = New System.Drawing.Point(6, 109)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.flxDetails.Size = New System.Drawing.Size(1033, 519)
        Me.flxDetails.TabIndex = 68
        '
        'Length
        '
        Me.Length.HeaderText = "Length"
        Me.Length.Name = "Length"
        '
        'Width1
        '
        Me.Width1.HeaderText = "Width"
        Me.Width1.Name = "Width1"
        '
        'OrderPcs
        '
        Me.OrderPcs.HeaderText = "Order Pcs"
        Me.OrderPcs.Name = "OrderPcs"
        '
        'IssuePcs
        '
        Me.IssuePcs.HeaderText = "Issue Pcs"
        Me.IssuePcs.Name = "IssuePcs"
        '
        'RejPcs
        '
        Me.RejPcs.HeaderText = "Rej Pcs"
        Me.RejPcs.Name = "RejPcs"
        '
        'BalPcs
        '
        Me.BalPcs.HeaderText = "Bal Pcs"
        Me.BalPcs.Name = "BalPcs"
        '
        'BoxPcs
        '
        Me.BoxPcs.HeaderText = "Box Pcs"
        Me.BoxPcs.Name = "BoxPcs"
        '
        'SelectPcs
        '
        Me.SelectPcs.HeaderText = "Select Pcs"
        Me.SelectPcs.Name = "SelectPcs"
        '
        'RequirePcs
        '
        Me.RequirePcs.HeaderText = "Require Pcs"
        Me.RequirePcs.Name = "RequirePcs"
        '
        'BagAssort
        '
        Me.BagAssort.HeaderText = "Bag Assort"
        Me.BagAssort.Name = "BagAssort"
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.txtMax)
        Me.HazelDev_Panel1.Controls.Add(Me.Label2)
        Me.HazelDev_Panel1.Controls.Add(Me.txtMin)
        Me.HazelDev_Panel1.Controls.Add(Me.Label1)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExcel)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdFind)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdClear)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(1036, 49)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'txtMax
        '
        Me.txtMax.Location = New System.Drawing.Point(401, 24)
        Me.txtMax.Name = "txtMax"
        Me.txtMax.Size = New System.Drawing.Size(61, 22)
        Me.txtMax.TabIndex = 169
        Me.txtMax.Text = "0.20"
        Me.txtMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(398, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 14)
        Me.Label2.TabIndex = 168
        Me.Label2.Text = "Max"
        '
        'txtMin
        '
        Me.txtMin.Location = New System.Drawing.Point(334, 24)
        Me.txtMin.Name = "txtMin"
        Me.txtMin.Size = New System.Drawing.Size(61, 22)
        Me.txtMin.TabIndex = 167
        Me.txtMin.Text = "0.00"
        Me.txtMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(331, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(25, 14)
        Me.Label1.TabIndex = 166
        Me.Label1.Text = "Min"
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
        Me.cmdExcel.Location = New System.Drawing.Point(934, 13)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(99, 28)
        Me.cmdExcel.TabIndex = 165
        Me.cmdExcel.Text = "Excel"
        Me.cmdExcel.UseVisualStyleBackColor = False
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
        Me.cmdFind.Location = New System.Drawing.Point(214, 13)
        Me.cmdFind.Name = "cmdFind"
        Me.cmdFind.Size = New System.Drawing.Size(99, 28)
        Me.cmdFind.TabIndex = 147
        Me.cmdFind.Text = "Find"
        Me.cmdFind.UseVisualStyleBackColor = False
        '
        'cmdClear
        '
        Me.cmdClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdClear.FlatAppearance.BorderSize = 0
        Me.cmdClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClear.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClear.ForeColor = System.Drawing.Color.White
        Me.cmdClear.Location = New System.Drawing.Point(109, 13)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(99, 28)
        Me.cmdClear.TabIndex = 33
        Me.cmdClear.Text = "Clear"
        Me.cmdClear.UseVisualStyleBackColor = False
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
        'frm_MixStoneReq
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1045, 665)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_MixStoneReq"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mix Stone Requirement"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents cmdFind As DiaStock.HazelDev_Button
    Friend WithEvents cmdClear As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents ExpProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents txtMax As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtMin As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Length As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Width1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrderPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IssuePcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RejPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BalPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BoxPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SelectPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RequirePcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BagAssort As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
