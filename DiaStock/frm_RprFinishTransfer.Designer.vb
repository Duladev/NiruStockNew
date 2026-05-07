<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_RprFinishTransfer
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
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.cmbShape = New System.Windows.Forms.ComboBox()
        Me.cmdLoad = New DiaStock.HazelDev_Button()
        Me.txtPktID = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkSelect = New DiaStock.HazelDev_CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtParNo = New System.Windows.Forms.TextBox()
        Me.pnlDetails2 = New System.Windows.Forms.Panel()
        Me.txtTotValue = New System.Windows.Forms.TextBox()
        Me.txtTotFinCts = New System.Windows.Forms.TextBox()
        Me.txtCount = New System.Windows.Forms.TextBox()
        Me.txtTotCts = New System.Windows.Forms.TextBox()
        Me.txtTotPcs = New System.Windows.Forms.TextBox()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.ParNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DepartmentName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Company = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Length = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Width1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtIssCts = New System.Windows.Forms.TextBox()
        Me.txtIssPcs = New System.Windows.Forms.TextBox()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.txtAvgPrice = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbSize = New System.Windows.Forms.ComboBox()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        Me.pnlDetails2.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbSize)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label44)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbShape)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdLoad)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPktID)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkSelect)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label3)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtParNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.pnlDetails2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtIssCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtIssPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtAvgPrice)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(1216, 539)
        Me.HazelDev_ThemeContainer1.TabIndex = 5
        Me.HazelDev_ThemeContainer1.Text = "ROUGH PROCESS FINISH TRANSFER"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.Color.Transparent
        Me.Label44.Location = New System.Drawing.Point(208, 107)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(43, 15)
        Me.Label44.TabIndex = 105
        Me.Label44.Text = "Shape"
        '
        'cmbShape
        '
        Me.cmbShape.FormattingEnabled = True
        Me.cmbShape.IntegralHeight = False
        Me.cmbShape.Location = New System.Drawing.Point(211, 122)
        Me.cmbShape.Name = "cmbShape"
        Me.cmbShape.Size = New System.Drawing.Size(102, 23)
        Me.cmbShape.TabIndex = 104
        '
        'cmdLoad
        '
        Me.cmdLoad.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdLoad.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdLoad.FlatAppearance.BorderSize = 0
        Me.cmdLoad.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdLoad.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdLoad.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLoad.ForeColor = System.Drawing.Color.White
        Me.cmdLoad.Location = New System.Drawing.Point(502, 119)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.Size = New System.Drawing.Size(99, 28)
        Me.cmdLoad.TabIndex = 103
        Me.cmdLoad.Text = "Load Packets"
        Me.cmdLoad.UseVisualStyleBackColor = False
        '
        'txtPktID
        '
        Me.txtPktID.Location = New System.Drawing.Point(112, 124)
        Me.txtPktID.MaxLength = 7
        Me.txtPktID.Name = "txtPktID"
        Me.txtPktID.Size = New System.Drawing.Size(94, 21)
        Me.txtPktID.TabIndex = 102
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(109, 106)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 15)
        Me.Label2.TabIndex = 101
        Me.Label2.Text = "Packet ID"
        '
        'chkSelect
        '
        Me.chkSelect.Checked = False
        Me.chkSelect.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkSelect.Location = New System.Drawing.Point(883, 129)
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Size = New System.Drawing.Size(84, 16)
        Me.chkSelect.TabIndex = 97
        Me.chkSelect.Text = "Select All"
        Me.chkSelect.TransparencyKey = System.Drawing.Color.Empty
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(9, 106)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 15)
        Me.Label3.TabIndex = 96
        Me.Label3.Text = "Par No."
        '
        'txtParNo
        '
        Me.txtParNo.Location = New System.Drawing.Point(12, 124)
        Me.txtParNo.MaxLength = 7
        Me.txtParNo.Name = "txtParNo"
        Me.txtParNo.Size = New System.Drawing.Size(94, 21)
        Me.txtParNo.TabIndex = 95
        '
        'pnlDetails2
        '
        Me.pnlDetails2.BackColor = System.Drawing.Color.White
        Me.pnlDetails2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDetails2.Controls.Add(Me.txtTotValue)
        Me.pnlDetails2.Controls.Add(Me.txtTotFinCts)
        Me.pnlDetails2.Controls.Add(Me.txtCount)
        Me.pnlDetails2.Controls.Add(Me.txtTotCts)
        Me.pnlDetails2.Controls.Add(Me.txtTotPcs)
        Me.pnlDetails2.Controls.Add(Me.flxDetails)
        Me.pnlDetails2.Location = New System.Drawing.Point(12, 154)
        Me.pnlDetails2.Name = "pnlDetails2"
        Me.pnlDetails2.Size = New System.Drawing.Size(1197, 378)
        Me.pnlDetails2.TabIndex = 68
        '
        'txtTotValue
        '
        Me.txtTotValue.Location = New System.Drawing.Point(489, 351)
        Me.txtTotValue.Name = "txtTotValue"
        Me.txtTotValue.ReadOnly = True
        Me.txtTotValue.Size = New System.Drawing.Size(92, 21)
        Me.txtTotValue.TabIndex = 95
        Me.txtTotValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotFinCts
        '
        Me.txtTotFinCts.Location = New System.Drawing.Point(391, 351)
        Me.txtTotFinCts.Name = "txtTotFinCts"
        Me.txtTotFinCts.ReadOnly = True
        Me.txtTotFinCts.Size = New System.Drawing.Size(92, 21)
        Me.txtTotFinCts.TabIndex = 94
        Me.txtTotFinCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCount
        '
        Me.txtCount.Location = New System.Drawing.Point(6, 351)
        Me.txtCount.Name = "txtCount"
        Me.txtCount.ReadOnly = True
        Me.txtCount.Size = New System.Drawing.Size(91, 21)
        Me.txtCount.TabIndex = 93
        Me.txtCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotCts
        '
        Me.txtTotCts.Location = New System.Drawing.Point(293, 351)
        Me.txtTotCts.Name = "txtTotCts"
        Me.txtTotCts.ReadOnly = True
        Me.txtTotCts.Size = New System.Drawing.Size(92, 21)
        Me.txtTotCts.TabIndex = 92
        Me.txtTotCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotPcs
        '
        Me.txtTotPcs.Location = New System.Drawing.Point(198, 351)
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
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ParNo, Me.Code, Me.DepartmentName, Me.Company, Me.Column3, Me.Column4, Me.Length, Me.Width1, Me.Column1, Me.Column2, Me.Column5, Me.Column6})
        Me.flxDetails.Location = New System.Drawing.Point(6, 4)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(1182, 341)
        Me.flxDetails.TabIndex = 44
        '
        'ParNo
        '
        Me.ParNo.HeaderText = "Parcel No"
        Me.ParNo.Name = "ParNo"
        Me.ParNo.ReadOnly = True
        '
        'Code
        '
        Me.Code.HeaderText = "Pkt No."
        Me.Code.Name = "Code"
        Me.Code.ReadOnly = True
        '
        'DepartmentName
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DepartmentName.DefaultCellStyle = DataGridViewCellStyle1
        Me.DepartmentName.HeaderText = "Pcs"
        Me.DepartmentName.Name = "DepartmentName"
        Me.DepartmentName.ReadOnly = True
        Me.DepartmentName.Width = 80
        '
        'Company
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Company.DefaultCellStyle = DataGridViewCellStyle2
        Me.Company.HeaderText = "Rgh Cts"
        Me.Company.Name = "Company"
        Me.Company.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.HeaderText = "Fin Cts"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.HeaderText = "Plan Value"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Length
        '
        Me.Length.HeaderText = "Length"
        Me.Length.Name = "Length"
        Me.Length.ReadOnly = True
        '
        'Width1
        '
        Me.Width1.HeaderText = "Width"
        Me.Width1.Name = "Width1"
        Me.Width1.ReadOnly = True
        '
        'Column1
        '
        Me.Column1.HeaderText = "Select"
        Me.Column1.Name = "Column1"
        Me.Column1.Width = 80
        '
        'Column2
        '
        Me.Column2.HeaderText = "Stone ID"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column5
        '
        Me.Column5.HeaderText = "Packet ID"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        '
        'Column6
        '
        Me.Column6.HeaderText = "Size"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'txtIssCts
        '
        Me.txtIssCts.Location = New System.Drawing.Point(701, 539)
        Me.txtIssCts.Name = "txtIssCts"
        Me.txtIssCts.ReadOnly = True
        Me.txtIssCts.Size = New System.Drawing.Size(90, 21)
        Me.txtIssCts.TabIndex = 49
        Me.txtIssCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtIssPcs
        '
        Me.txtIssPcs.Location = New System.Drawing.Point(605, 539)
        Me.txtIssPcs.Name = "txtIssPcs"
        Me.txtIssPcs.ReadOnly = True
        Me.txtIssPcs.Size = New System.Drawing.Size(90, 21)
        Me.txtIssPcs.TabIndex = 48
        Me.txtIssPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(1206, 50)
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(316, 107)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 15)
        Me.Label1.TabIndex = 107
        Me.Label1.Text = "Size"
        '
        'cmbSize
        '
        Me.cmbSize.FormattingEnabled = True
        Me.cmbSize.IntegralHeight = False
        Me.cmbSize.Location = New System.Drawing.Point(319, 122)
        Me.cmbSize.Name = "cmbSize"
        Me.cmbSize.Size = New System.Drawing.Size(177, 23)
        Me.cmbSize.TabIndex = 106
        '
        'frm_RprFinishTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1216, 539)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_RprFinishTransfer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Rpr Finish Transfer"
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
    Friend WithEvents txtTotCts As System.Windows.Forms.TextBox
    Friend WithEvents txtTotPcs As System.Windows.Forms.TextBox
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents txtIssCts As System.Windows.Forms.TextBox
    Friend WithEvents txtIssPcs As System.Windows.Forms.TextBox
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents txtAvgPrice As System.Windows.Forms.TextBox
    Friend WithEvents txtCount As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtParNo As System.Windows.Forms.TextBox
    Friend WithEvents chkSelect As DiaStock.HazelDev_CheckBox
    Friend WithEvents txtTotFinCts As System.Windows.Forms.TextBox
    Friend WithEvents txtTotValue As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPktID As System.Windows.Forms.TextBox
    Friend WithEvents cmdLoad As DiaStock.HazelDev_Button
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents cmbShape As System.Windows.Forms.ComboBox
    Friend WithEvents ParNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DepartmentName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Company As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Length As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Width1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbSize As System.Windows.Forms.ComboBox
End Class
