<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_PCUEditReturns
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
        Me.cmdAE = New DiaStock.HazelDev_Button()
        Me.chkSelect = New DiaStock.HazelDev_CheckBox()
        Me.flxParcel = New System.Windows.Forms.DataGridView()
        Me.OrderNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Subject = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.RetPcsT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RetCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EmpNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtEmp = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtPktNo = New System.Windows.Forms.TextBox()
        Me.txtCts = New System.Windows.Forms.TextBox()
        Me.txtPcs = New System.Windows.Forms.TextBox()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtOrder = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        CType(Me.flxParcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdAE)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkSelect)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxParcel)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdSave)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxDetails)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtEmp)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label16)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label23)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPktNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label7)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label6)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtOrder)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label4)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(541, 533)
        Me.HazelDev_ThemeContainer1.TabIndex = 4
        Me.HazelDev_ThemeContainer1.Text = "PRECISION EDIT RETURNS"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmdAE
        '
        Me.cmdAE.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdAE.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdAE.FlatAppearance.BorderSize = 0
        Me.cmdAE.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdAE.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdAE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAE.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAE.ForeColor = System.Drawing.Color.White
        Me.cmdAE.Location = New System.Drawing.Point(433, 263)
        Me.cmdAE.Name = "cmdAE"
        Me.cmdAE.Size = New System.Drawing.Size(99, 28)
        Me.cmdAE.TabIndex = 163
        Me.cmdAE.Text = "A > E"
        Me.cmdAE.UseVisualStyleBackColor = False
        '
        'chkSelect
        '
        Me.chkSelect.Checked = False
        Me.chkSelect.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkSelect.Location = New System.Drawing.Point(15, 275)
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Size = New System.Drawing.Size(84, 16)
        Me.chkSelect.TabIndex = 162
        Me.chkSelect.Text = "Select All"
        Me.chkSelect.TransparencyKey = System.Drawing.Color.Empty
        '
        'flxParcel
        '
        Me.flxParcel.AllowUserToAddRows = False
        Me.flxParcel.AllowUserToDeleteRows = False
        Me.flxParcel.AllowUserToResizeColumns = False
        Me.flxParcel.AllowUserToResizeRows = False
        Me.flxParcel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxParcel.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.OrderNo, Me.Subject, Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3})
        Me.flxParcel.Location = New System.Drawing.Point(12, 297)
        Me.flxParcel.Name = "flxParcel"
        Me.flxParcel.RowHeadersVisible = False
        Me.flxParcel.Size = New System.Drawing.Size(520, 230)
        Me.flxParcel.TabIndex = 161
        '
        'OrderNo
        '
        Me.OrderNo.HeaderText = "Order No"
        Me.OrderNo.Name = "OrderNo"
        Me.OrderNo.ReadOnly = True
        Me.OrderNo.Width = 80
        '
        'Subject
        '
        Me.Subject.HeaderText = "Subject"
        Me.Subject.Name = "Subject"
        Me.Subject.ReadOnly = True
        Me.Subject.Width = 250
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Pcs"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 50
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Cts"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 50
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Select"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.DataGridViewTextBoxColumn3.Width = 60
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
        Me.cmdSave.Location = New System.Drawing.Point(433, 118)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(99, 28)
        Me.cmdSave.TabIndex = 45
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RetPcsT, Me.RetCts, Me.EmpNo})
        Me.flxDetails.Location = New System.Drawing.Point(12, 163)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.ReadOnly = True
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(335, 101)
        Me.flxDetails.TabIndex = 160
        '
        'RetPcsT
        '
        Me.RetPcsT.HeaderText = "Pcs"
        Me.RetPcsT.Name = "RetPcsT"
        Me.RetPcsT.ReadOnly = True
        '
        'RetCts
        '
        Me.RetCts.HeaderText = "Cts"
        Me.RetCts.Name = "RetCts"
        Me.RetCts.ReadOnly = True
        '
        'EmpNo
        '
        Me.EmpNo.HeaderText = "Emp No"
        Me.EmpNo.Name = "EmpNo"
        Me.EmpNo.ReadOnly = True
        '
        'txtEmp
        '
        Me.txtEmp.Location = New System.Drawing.Point(185, 125)
        Me.txtEmp.Name = "txtEmp"
        Me.txtEmp.ReadOnly = True
        Me.txtEmp.Size = New System.Drawing.Size(84, 21)
        Me.txtEmp.TabIndex = 90
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Location = New System.Drawing.Point(182, 107)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(55, 15)
        Me.Label16.TabIndex = 91
        Me.Label16.Text = "Emp No."
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Location = New System.Drawing.Point(105, 107)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(46, 15)
        Me.Label23.TabIndex = 76
        Me.Label23.Text = "Pkt No."
        '
        'txtPktNo
        '
        Me.txtPktNo.Location = New System.Drawing.Point(105, 125)
        Me.txtPktNo.Name = "txtPktNo"
        Me.txtPktNo.Size = New System.Drawing.Size(74, 21)
        Me.txtPktNo.TabIndex = 73
        '
        'txtCts
        '
        Me.txtCts.Location = New System.Drawing.Point(353, 125)
        Me.txtCts.Name = "txtCts"
        Me.txtCts.Size = New System.Drawing.Size(74, 21)
        Me.txtCts.TabIndex = 53
        Me.txtCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPcs
        '
        Me.txtPcs.Location = New System.Drawing.Point(275, 125)
        Me.txtPcs.Name = "txtPcs"
        Me.txtPcs.Size = New System.Drawing.Size(72, 21)
        Me.txtPcs.TabIndex = 52
        Me.txtPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(529, 50)
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
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(350, 107)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(24, 15)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = "Cts"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(272, 107)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(27, 15)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "Pcs"
        '
        'txtOrder
        '
        Me.txtOrder.Location = New System.Drawing.Point(12, 125)
        Me.txtOrder.Name = "txtOrder"
        Me.txtOrder.Size = New System.Drawing.Size(84, 21)
        Me.txtOrder.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(12, 107)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 15)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Order No"
        '
        'frm_PCUEditReturns
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(541, 533)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_PCUEditReturns"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Precision Edit Returns"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        CType(Me.flxParcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents txtEmp As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtPktNo As System.Windows.Forms.TextBox
    Friend WithEvents txtCts As System.Windows.Forms.TextBox
    Friend WithEvents txtPcs As System.Windows.Forms.TextBox
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtOrder As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents RetPcsT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RetCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EmpNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents flxParcel As System.Windows.Forms.DataGridView
    Friend WithEvents chkSelect As DiaStock.HazelDev_CheckBox
    Friend WithEvents cmdAE As DiaStock.HazelDev_Button
    Friend WithEvents OrderNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Subject As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewCheckBoxColumn
End Class
