<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_GRDMixTransfer
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
        Me.txtSelCts = New System.Windows.Forms.TextBox()
        Me.txtSelPcs = New System.Windows.Forms.TextBox()
        Me.txtRghCts = New System.Windows.Forms.TextBox()
        Me.txtValue = New System.Windows.Forms.TextBox()
        Me.txtPrice = New System.Windows.Forms.TextBox()
        Me.txtTotCts = New System.Windows.Forms.TextBox()
        Me.txtTotPcs = New System.Windows.Forms.TextBox()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.chkSelect = New DiaStock.HazelDev_CheckBox()
        Me.txtParNo = New System.Windows.Forms.TextBox()
        Me.optPcuSort = New DiaStock.HazelDev_RadioButton()
        Me.optPcu = New DiaStock.HazelDev_RadioButton()
        Me.optFinish = New DiaStock.HazelDev_RadioButton()
        Me.optMix = New DiaStock.HazelDev_RadioButton()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Date1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OutTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PassableP1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PCUPending = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParcelNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RefNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Side = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Special = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Department = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BagAssort = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PktNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Assortment = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.GiaNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtSelCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtSelPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtRghCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtValue)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPrice)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxDetails)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(1053, 566)
        Me.HazelDev_ThemeContainer1.TabIndex = 5
        Me.HazelDev_ThemeContainer1.Text = "MIX TRANSFER"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'txtSelCts
        '
        Me.txtSelCts.Location = New System.Drawing.Point(534, 540)
        Me.txtSelCts.Name = "txtSelCts"
        Me.txtSelCts.ReadOnly = True
        Me.txtSelCts.Size = New System.Drawing.Size(65, 21)
        Me.txtSelCts.TabIndex = 143
        Me.txtSelCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSelPcs
        '
        Me.txtSelPcs.Location = New System.Drawing.Point(463, 540)
        Me.txtSelPcs.Name = "txtSelPcs"
        Me.txtSelPcs.ReadOnly = True
        Me.txtSelPcs.Size = New System.Drawing.Size(65, 21)
        Me.txtSelPcs.TabIndex = 142
        Me.txtSelPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRghCts
        '
        Me.txtRghCts.Location = New System.Drawing.Point(392, 540)
        Me.txtRghCts.Name = "txtRghCts"
        Me.txtRghCts.ReadOnly = True
        Me.txtRghCts.Size = New System.Drawing.Size(65, 21)
        Me.txtRghCts.TabIndex = 141
        Me.txtRghCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtValue
        '
        Me.txtValue.Location = New System.Drawing.Point(323, 540)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.ReadOnly = True
        Me.txtValue.Size = New System.Drawing.Size(65, 21)
        Me.txtValue.TabIndex = 140
        Me.txtValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPrice
        '
        Me.txtPrice.Location = New System.Drawing.Point(252, 540)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.ReadOnly = True
        Me.txtPrice.Size = New System.Drawing.Size(65, 21)
        Me.txtPrice.TabIndex = 139
        Me.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotCts
        '
        Me.txtTotCts.Location = New System.Drawing.Point(181, 540)
        Me.txtTotCts.Name = "txtTotCts"
        Me.txtTotCts.ReadOnly = True
        Me.txtTotCts.Size = New System.Drawing.Size(65, 21)
        Me.txtTotCts.TabIndex = 138
        Me.txtTotCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotPcs
        '
        Me.txtTotPcs.Location = New System.Drawing.Point(110, 540)
        Me.txtTotPcs.Name = "txtTotPcs"
        Me.txtTotPcs.ReadOnly = True
        Me.txtTotPcs.Size = New System.Drawing.Size(65, 21)
        Me.txtTotPcs.TabIndex = 137
        Me.txtTotPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Code, Me.Date1, Me.InTime, Me.OutTime, Me.PassableP1, Me.PCUPending, Me.ParcelNo, Me.RefNo, Me.Side, Me.Special, Me.Department, Me.BagAssort, Me.PktNo, Me.Assortment, Me.GiaNo, Me.Column1, Me.Column2, Me.Column3})
        Me.flxDetails.Location = New System.Drawing.Point(3, 109)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(1045, 425)
        Me.flxDetails.TabIndex = 43
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.chkSelect)
        Me.HazelDev_Panel1.Controls.Add(Me.txtParNo)
        Me.HazelDev_Panel1.Controls.Add(Me.optPcuSort)
        Me.HazelDev_Panel1.Controls.Add(Me.optPcu)
        Me.HazelDev_Panel1.Controls.Add(Me.optFinish)
        Me.HazelDev_Panel1.Controls.Add(Me.optMix)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(1045, 49)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'chkSelect
        '
        Me.chkSelect.Checked = False
        Me.chkSelect.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkSelect.Location = New System.Drawing.Point(917, 19)
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Size = New System.Drawing.Size(84, 16)
        Me.chkSelect.TabIndex = 137
        Me.chkSelect.Text = "Select All"
        Me.chkSelect.TransparencyKey = System.Drawing.Color.Empty
        '
        'txtParNo
        '
        Me.txtParNo.Location = New System.Drawing.Point(721, 13)
        Me.txtParNo.Name = "txtParNo"
        Me.txtParNo.ReadOnly = True
        Me.txtParNo.Size = New System.Drawing.Size(109, 22)
        Me.txtParNo.TabIndex = 136
        '
        'optPcuSort
        '
        Me.optPcuSort.BackColor = System.Drawing.Color.White
        Me.optPcuSort.Checked = False
        Me.optPcuSort.Location = New System.Drawing.Point(497, 18)
        Me.optPcuSort.Name = "optPcuSort"
        Me.optPcuSort.Size = New System.Drawing.Size(87, 22)
        Me.optPcuSort.TabIndex = 49
        Me.optPcuSort.Text = "PCU Sorting"
        Me.optPcuSort.TransparencyKey = System.Drawing.Color.Empty
        '
        'optPcu
        '
        Me.optPcu.BackColor = System.Drawing.Color.White
        Me.optPcu.Checked = False
        Me.optPcu.Location = New System.Drawing.Point(443, 18)
        Me.optPcu.Name = "optPcu"
        Me.optPcu.Size = New System.Drawing.Size(48, 22)
        Me.optPcu.TabIndex = 48
        Me.optPcu.Text = "PCU"
        Me.optPcu.TransparencyKey = System.Drawing.Color.Empty
        '
        'optFinish
        '
        Me.optFinish.BackColor = System.Drawing.Color.White
        Me.optFinish.Checked = False
        Me.optFinish.Location = New System.Drawing.Point(389, 18)
        Me.optFinish.Name = "optFinish"
        Me.optFinish.Size = New System.Drawing.Size(44, 22)
        Me.optFinish.TabIndex = 47
        Me.optFinish.Text = "MIX"
        Me.optFinish.TransparencyKey = System.Drawing.Color.Empty
        '
        'optMix
        '
        Me.optMix.BackColor = System.Drawing.Color.White
        Me.optMix.Checked = False
        Me.optMix.Location = New System.Drawing.Point(336, 18)
        Me.optMix.Name = "optMix"
        Me.optMix.Size = New System.Drawing.Size(48, 22)
        Me.optMix.TabIndex = 46
        Me.optMix.Text = "BOX"
        Me.optMix.TransparencyKey = System.Drawing.Color.Empty
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
        Me.cmdSave.Location = New System.Drawing.Point(601, 13)
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
        Me.cmdNew.Text = "Load"
        Me.cmdNew.UseVisualStyleBackColor = False
        '
        'Code
        '
        Me.Code.HeaderText = "Assort/Order No"
        Me.Code.Name = "Code"
        Me.Code.ReadOnly = True
        Me.Code.Width = 120
        '
        'Date1
        '
        Me.Date1.HeaderText = "Pcs"
        Me.Date1.Name = "Date1"
        Me.Date1.ReadOnly = True
        Me.Date1.Width = 60
        '
        'InTime
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.NullValue = Nothing
        Me.InTime.DefaultCellStyle = DataGridViewCellStyle1
        Me.InTime.HeaderText = "Cts"
        Me.InTime.Name = "InTime"
        Me.InTime.ReadOnly = True
        Me.InTime.Width = 60
        '
        'OutTime
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.NullValue = Nothing
        Me.OutTime.DefaultCellStyle = DataGridViewCellStyle2
        Me.OutTime.HeaderText = "Price"
        Me.OutTime.Name = "OutTime"
        Me.OutTime.ReadOnly = True
        Me.OutTime.Width = 60
        '
        'PassableP1
        '
        Me.PassableP1.HeaderText = "Value"
        Me.PassableP1.Name = "PassableP1"
        Me.PassableP1.ReadOnly = True
        Me.PassableP1.Width = 80
        '
        'PCUPending
        '
        Me.PCUPending.HeaderText = "RghCts"
        Me.PCUPending.Name = "PCUPending"
        Me.PCUPending.ReadOnly = True
        Me.PCUPending.Width = 60
        '
        'ParcelNo
        '
        Me.ParcelNo.HeaderText = "Parcel No"
        Me.ParcelNo.Name = "ParcelNo"
        Me.ParcelNo.ReadOnly = True
        '
        'RefNo
        '
        Me.RefNo.HeaderText = "Ref"
        Me.RefNo.Name = "RefNo"
        Me.RefNo.ReadOnly = True
        Me.RefNo.Width = 60
        '
        'Side
        '
        Me.Side.HeaderText = "Side"
        Me.Side.Name = "Side"
        Me.Side.ReadOnly = True
        Me.Side.Width = 50
        '
        'Special
        '
        Me.Special.HeaderText = "Special"
        Me.Special.Name = "Special"
        Me.Special.ReadOnly = True
        Me.Special.Width = 50
        '
        'Department
        '
        Me.Department.HeaderText = "Department"
        Me.Department.Name = "Department"
        Me.Department.ReadOnly = True
        '
        'BagAssort
        '
        Me.BagAssort.HeaderText = "Bag Assort"
        Me.BagAssort.Name = "BagAssort"
        Me.BagAssort.ReadOnly = True
        '
        'PktNo
        '
        Me.PktNo.HeaderText = "Pkt"
        Me.PktNo.Name = "PktNo"
        Me.PktNo.ReadOnly = True
        Me.PktNo.Width = 60
        '
        'Assortment
        '
        Me.Assortment.HeaderText = "Select"
        Me.Assortment.Name = "Assortment"
        Me.Assortment.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Assortment.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'GiaNo
        '
        Me.GiaNo.HeaderText = "GiaNo"
        Me.GiaNo.Name = "GiaNo"
        Me.GiaNo.ReadOnly = True
        '
        'Column1
        '
        Me.Column1.HeaderText = "Rate Code"
        Me.Column1.Name = "Column1"
        '
        'Column2
        '
        Me.Column2.HeaderText = "Grp"
        Me.Column2.Name = "Column2"
        '
        'Column3
        '
        Me.Column3.HeaderText = "Plan Value"
        Me.Column3.Name = "Column3"
        '
        'frm_GRDMixTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1053, 566)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_GRDMixTransfer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mix Transfer"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents optPcuSort As DiaStock.HazelDev_RadioButton
    Friend WithEvents optPcu As DiaStock.HazelDev_RadioButton
    Friend WithEvents optFinish As DiaStock.HazelDev_RadioButton
    Friend WithEvents optMix As DiaStock.HazelDev_RadioButton
    Friend WithEvents txtParNo As System.Windows.Forms.TextBox
    Friend WithEvents txtRghCts As System.Windows.Forms.TextBox
    Friend WithEvents txtValue As System.Windows.Forms.TextBox
    Friend WithEvents txtPrice As System.Windows.Forms.TextBox
    Friend WithEvents txtTotCts As System.Windows.Forms.TextBox
    Friend WithEvents txtTotPcs As System.Windows.Forms.TextBox
    Friend WithEvents txtSelCts As System.Windows.Forms.TextBox
    Friend WithEvents txtSelPcs As System.Windows.Forms.TextBox
    Friend WithEvents chkSelect As DiaStock.HazelDev_CheckBox
    Friend WithEvents Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Date1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OutTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PassableP1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PCUPending As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParcelNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RefNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Side As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Special As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Department As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BagAssort As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PktNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Assortment As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents GiaNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
