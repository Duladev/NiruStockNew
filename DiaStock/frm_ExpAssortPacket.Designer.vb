<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ExpAssortPacket
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtSelCts = New System.Windows.Forms.TextBox()
        Me.txtSelPcs = New System.Windows.Forms.TextBox()
        Me.flxSelected = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbDept = New System.Windows.Forms.ComboBox()
        Me.cmdSelect = New DiaStock.HazelDev_Button()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtPktNo = New System.Windows.Forms.TextBox()
        Me.pnlDetails2 = New System.Windows.Forms.Panel()
        Me.txtCount = New System.Windows.Forms.TextBox()
        Me.txtTotCts = New System.Windows.Forms.TextBox()
        Me.txtTotPcs = New System.Windows.Forms.TextBox()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DepartmentName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Company = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParcelNo1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.txtAvgPrice = New System.Windows.Forms.TextBox()
        Me.txtParNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RefNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.flxSelected, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDetails2.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Panel1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbDept)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdSelect)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label23)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPktNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.pnlDetails2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtAvgPrice)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtParNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label4)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(780, 628)
        Me.HazelDev_ThemeContainer1.TabIndex = 4
        Me.HazelDev_ThemeContainer1.Text = "SORTING PACKET"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtSelCts)
        Me.Panel1.Controls.Add(Me.txtSelPcs)
        Me.Panel1.Controls.Add(Me.flxSelected)
        Me.Panel1.Location = New System.Drawing.Point(365, 156)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(409, 468)
        Me.Panel1.TabIndex = 92
        '
        'txtSelCts
        '
        Me.txtSelCts.Location = New System.Drawing.Point(200, 438)
        Me.txtSelCts.Name = "txtSelCts"
        Me.txtSelCts.ReadOnly = True
        Me.txtSelCts.Size = New System.Drawing.Size(92, 21)
        Me.txtSelCts.TabIndex = 92
        Me.txtSelCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSelPcs
        '
        Me.txtSelPcs.Location = New System.Drawing.Point(105, 438)
        Me.txtSelPcs.Name = "txtSelPcs"
        Me.txtSelPcs.ReadOnly = True
        Me.txtSelPcs.Size = New System.Drawing.Size(91, 21)
        Me.txtSelPcs.TabIndex = 91
        Me.txtSelPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'flxSelected
        '
        Me.flxSelected.AllowUserToAddRows = False
        Me.flxSelected.AllowUserToDeleteRows = False
        Me.flxSelected.AllowUserToResizeColumns = False
        Me.flxSelected.AllowUserToResizeRows = False
        Me.flxSelected.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxSelected.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.RefNo})
        Me.flxSelected.Location = New System.Drawing.Point(6, 4)
        Me.flxSelected.Name = "flxSelected"
        Me.flxSelected.ReadOnly = True
        Me.flxSelected.RowHeadersVisible = False
        Me.flxSelected.Size = New System.Drawing.Size(398, 428)
        Me.flxSelected.TabIndex = 44
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(12, 107)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 15)
        Me.Label1.TabIndex = 89
        Me.Label1.Text = "Department"
        '
        'cmbDept
        '
        Me.cmbDept.FormattingEnabled = True
        Me.cmbDept.IntegralHeight = False
        Me.cmbDept.Location = New System.Drawing.Point(12, 125)
        Me.cmbDept.Name = "cmbDept"
        Me.cmbDept.Size = New System.Drawing.Size(152, 23)
        Me.cmbDept.TabIndex = 88
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
        Me.cmdSelect.Location = New System.Drawing.Point(271, 122)
        Me.cmdSelect.Name = "cmdSelect"
        Me.cmdSelect.Size = New System.Drawing.Size(76, 28)
        Me.cmdSelect.TabIndex = 87
        Me.cmdSelect.Text = "Select All"
        Me.cmdSelect.UseVisualStyleBackColor = False
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Location = New System.Drawing.Point(365, 107)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(46, 15)
        Me.Label23.TabIndex = 76
        Me.Label23.Text = "Pkt No."
        '
        'txtPktNo
        '
        Me.txtPktNo.Location = New System.Drawing.Point(365, 125)
        Me.txtPktNo.Name = "txtPktNo"
        Me.txtPktNo.ReadOnly = True
        Me.txtPktNo.Size = New System.Drawing.Size(74, 21)
        Me.txtPktNo.TabIndex = 73
        '
        'pnlDetails2
        '
        Me.pnlDetails2.BackColor = System.Drawing.Color.White
        Me.pnlDetails2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDetails2.Controls.Add(Me.txtCount)
        Me.pnlDetails2.Controls.Add(Me.txtTotCts)
        Me.pnlDetails2.Controls.Add(Me.txtTotPcs)
        Me.pnlDetails2.Controls.Add(Me.flxDetails)
        Me.pnlDetails2.Location = New System.Drawing.Point(12, 156)
        Me.pnlDetails2.Name = "pnlDetails2"
        Me.pnlDetails2.Size = New System.Drawing.Size(335, 468)
        Me.pnlDetails2.TabIndex = 68
        '
        'txtCount
        '
        Me.txtCount.Location = New System.Drawing.Point(6, 438)
        Me.txtCount.Name = "txtCount"
        Me.txtCount.ReadOnly = True
        Me.txtCount.Size = New System.Drawing.Size(91, 21)
        Me.txtCount.TabIndex = 93
        '
        'txtTotCts
        '
        Me.txtTotCts.Location = New System.Drawing.Point(204, 438)
        Me.txtTotCts.Name = "txtTotCts"
        Me.txtTotCts.ReadOnly = True
        Me.txtTotCts.Size = New System.Drawing.Size(92, 21)
        Me.txtTotCts.TabIndex = 92
        Me.txtTotCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotPcs
        '
        Me.txtTotPcs.Location = New System.Drawing.Point(109, 438)
        Me.txtTotPcs.Name = "txtTotPcs"
        Me.txtTotPcs.ReadOnly = True
        Me.txtTotPcs.Size = New System.Drawing.Size(91, 21)
        Me.txtTotPcs.TabIndex = 91
        Me.txtTotPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Code, Me.DepartmentName, Me.Company, Me.ParcelNo1})
        Me.flxDetails.Location = New System.Drawing.Point(6, 4)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.ReadOnly = True
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(318, 428)
        Me.flxDetails.TabIndex = 44
        '
        'Code
        '
        Me.Code.HeaderText = "Pkt No."
        Me.Code.Name = "Code"
        Me.Code.ReadOnly = True
        '
        'DepartmentName
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DepartmentName.DefaultCellStyle = DataGridViewCellStyle3
        Me.DepartmentName.HeaderText = "Pcs"
        Me.DepartmentName.Name = "DepartmentName"
        Me.DepartmentName.ReadOnly = True
        Me.DepartmentName.Width = 80
        '
        'Company
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Company.DefaultCellStyle = DataGridViewCellStyle4
        Me.Company.HeaderText = "Cts"
        Me.Company.Name = "Company"
        Me.Company.ReadOnly = True
        '
        'ParcelNo1
        '
        Me.ParcelNo1.HeaderText = "Parcel No"
        Me.ParcelNo1.MinimumWidth = 50
        Me.ParcelNo1.Name = "ParcelNo1"
        Me.ParcelNo1.ReadOnly = True
        Me.ParcelNo1.Visible = False
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
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(771, 50)
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
        'txtAvgPrice
        '
        Me.txtAvgPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAvgPrice.Location = New System.Drawing.Point(797, 539)
        Me.txtAvgPrice.Name = "txtAvgPrice"
        Me.txtAvgPrice.ReadOnly = True
        Me.txtAvgPrice.Size = New System.Drawing.Size(94, 21)
        Me.txtAvgPrice.TabIndex = 9
        Me.txtAvgPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtParNo
        '
        Me.txtParNo.Location = New System.Drawing.Point(170, 125)
        Me.txtParNo.Name = "txtParNo"
        Me.txtParNo.Size = New System.Drawing.Size(84, 21)
        Me.txtParNo.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(170, 107)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 15)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Parcel No"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Pkt No."
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewTextBoxColumn2.HeaderText = "Pcs"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 80
        '
        'DataGridViewTextBoxColumn3
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewTextBoxColumn3.HeaderText = "Cts"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'RefNo
        '
        Me.RefNo.HeaderText = "Par No."
        Me.RefNo.Name = "RefNo"
        Me.RefNo.ReadOnly = True
        Me.RefNo.Width = 80
        '
        'frm_ExpAssortPacket
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(780, 628)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_ExpAssortPacket"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sorting Packet"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.flxSelected, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDetails2.ResumeLayout(False)
        Me.pnlDetails2.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents cmdSelect As DiaStock.HazelDev_Button
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtPktNo As System.Windows.Forms.TextBox
    Friend WithEvents pnlDetails2 As System.Windows.Forms.Panel
    Friend WithEvents txtTotCts As System.Windows.Forms.TextBox
    Friend WithEvents txtTotPcs As System.Windows.Forms.TextBox
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents txtAvgPrice As System.Windows.Forms.TextBox
    Friend WithEvents txtParNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbDept As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtSelCts As System.Windows.Forms.TextBox
    Friend WithEvents txtSelPcs As System.Windows.Forms.TextBox
    Friend WithEvents flxSelected As System.Windows.Forms.DataGridView
    Friend WithEvents Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DepartmentName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Company As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParcelNo1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtCount As System.Windows.Forms.TextBox
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RefNo As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
