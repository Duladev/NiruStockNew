<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_RghMixTrf
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.chkSizing = New DiaStock.HazelDev_CheckBox()
        Me.txtSupParNo = New System.Windows.Forms.TextBox()
        Me.txtValue = New System.Windows.Forms.TextBox()
        Me.txtPrice = New System.Windows.Forms.TextBox()
        Me.txtTotCts = New System.Windows.Forms.TextBox()
        Me.txtTotPcs = New System.Windows.Forms.TextBox()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Date1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OutTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PassableP1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParcelNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Department = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RefNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Side = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Special = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Assortment = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.chkKOrder = New DiaStock.HazelDev_CheckBox()
        Me.chkOrder = New DiaStock.HazelDev_CheckBox()
        Me.chkExport = New DiaStock.HazelDev_CheckBox()
        Me.optMixPlan = New DiaStock.HazelDev_RadioButton()
        Me.optReject = New DiaStock.HazelDev_RadioButton()
        Me.optPolBox = New DiaStock.HazelDev_RadioButton()
        Me.optSorting = New DiaStock.HazelDev_RadioButton()
        Me.optGrdPcu = New DiaStock.HazelDev_RadioButton()
        Me.optPcu = New DiaStock.HazelDev_RadioButton()
        Me.optGrading = New DiaStock.HazelDev_RadioButton()
        Me.optMix = New DiaStock.HazelDev_RadioButton()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.txtPackNo = New System.Windows.Forms.TextBox()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkSizing)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtSupParNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtValue)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPrice)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxDetails)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPackNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdSave)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(1221, 568)
        Me.HazelDev_ThemeContainer1.TabIndex = 6
        Me.HazelDev_ThemeContainer1.Text = "MIX/GRADING - ROUGH TRANSFER"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'chkSizing
        '
        Me.chkSizing.Checked = False
        Me.chkSizing.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkSizing.Location = New System.Drawing.Point(922, 540)
        Me.chkSizing.Name = "chkSizing"
        Me.chkSizing.Size = New System.Drawing.Size(66, 17)
        Me.chkSizing.TabIndex = 147
        Me.chkSizing.Text = "Sizing"
        Me.chkSizing.TransparencyKey = System.Drawing.Color.Empty
        '
        'txtSupParNo
        '
        Me.txtSupParNo.Location = New System.Drawing.Point(499, 540)
        Me.txtSupParNo.Name = "txtSupParNo"
        Me.txtSupParNo.ReadOnly = True
        Me.txtSupParNo.Size = New System.Drawing.Size(102, 21)
        Me.txtSupParNo.TabIndex = 141
        '
        'txtValue
        '
        Me.txtValue.Location = New System.Drawing.Point(403, 540)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.ReadOnly = True
        Me.txtValue.Size = New System.Drawing.Size(90, 21)
        Me.txtValue.TabIndex = 140
        Me.txtValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPrice
        '
        Me.txtPrice.Location = New System.Drawing.Point(307, 540)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.ReadOnly = True
        Me.txtPrice.Size = New System.Drawing.Size(90, 21)
        Me.txtPrice.TabIndex = 139
        Me.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotCts
        '
        Me.txtTotCts.Location = New System.Drawing.Point(211, 540)
        Me.txtTotCts.Name = "txtTotCts"
        Me.txtTotCts.ReadOnly = True
        Me.txtTotCts.Size = New System.Drawing.Size(90, 21)
        Me.txtTotCts.TabIndex = 138
        Me.txtTotCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotPcs
        '
        Me.txtTotPcs.Location = New System.Drawing.Point(112, 540)
        Me.txtTotPcs.Name = "txtTotPcs"
        Me.txtTotPcs.ReadOnly = True
        Me.txtTotPcs.Size = New System.Drawing.Size(90, 21)
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
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Code, Me.Date1, Me.InTime, Me.OutTime, Me.PassableP1, Me.ParcelNo, Me.Department, Me.RefNo, Me.Side, Me.Special, Me.Assortment})
        Me.flxDetails.Location = New System.Drawing.Point(3, 109)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(1213, 425)
        Me.flxDetails.TabIndex = 43
        '
        'Code
        '
        Me.Code.HeaderText = "Assortment"
        Me.Code.Name = "Code"
        Me.Code.ReadOnly = True
        '
        'Date1
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Date1.DefaultCellStyle = DataGridViewCellStyle5
        Me.Date1.HeaderText = "Pcs"
        Me.Date1.Name = "Date1"
        Me.Date1.ReadOnly = True
        '
        'InTime
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.NullValue = Nothing
        Me.InTime.DefaultCellStyle = DataGridViewCellStyle6
        Me.InTime.HeaderText = "Cts"
        Me.InTime.Name = "InTime"
        Me.InTime.ReadOnly = True
        '
        'OutTime
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.NullValue = Nothing
        Me.OutTime.DefaultCellStyle = DataGridViewCellStyle7
        Me.OutTime.HeaderText = "Price"
        Me.OutTime.Name = "OutTime"
        Me.OutTime.ReadOnly = True
        '
        'PassableP1
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PassableP1.DefaultCellStyle = DataGridViewCellStyle8
        Me.PassableP1.HeaderText = "Value"
        Me.PassableP1.Name = "PassableP1"
        Me.PassableP1.ReadOnly = True
        '
        'ParcelNo
        '
        Me.ParcelNo.HeaderText = "Parcel No"
        Me.ParcelNo.Name = "ParcelNo"
        Me.ParcelNo.ReadOnly = True
        '
        'Department
        '
        Me.Department.HeaderText = "Department"
        Me.Department.Name = "Department"
        Me.Department.ReadOnly = True
        '
        'RefNo
        '
        Me.RefNo.HeaderText = "Org. Assort"
        Me.RefNo.Name = "RefNo"
        Me.RefNo.ReadOnly = True
        '
        'Side
        '
        Me.Side.HeaderText = "Sup Parcel"
        Me.Side.Name = "Side"
        Me.Side.ReadOnly = True
        '
        'Special
        '
        Me.Special.HeaderText = "DCL Parcel"
        Me.Special.Name = "Special"
        Me.Special.ReadOnly = True
        '
        'Assortment
        '
        Me.Assortment.HeaderText = "Select"
        Me.Assortment.Name = "Assortment"
        Me.Assortment.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Assortment.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.chkKOrder)
        Me.HazelDev_Panel1.Controls.Add(Me.chkOrder)
        Me.HazelDev_Panel1.Controls.Add(Me.chkExport)
        Me.HazelDev_Panel1.Controls.Add(Me.optMixPlan)
        Me.HazelDev_Panel1.Controls.Add(Me.optReject)
        Me.HazelDev_Panel1.Controls.Add(Me.optPolBox)
        Me.HazelDev_Panel1.Controls.Add(Me.optSorting)
        Me.HazelDev_Panel1.Controls.Add(Me.optGrdPcu)
        Me.HazelDev_Panel1.Controls.Add(Me.optPcu)
        Me.HazelDev_Panel1.Controls.Add(Me.optGrading)
        Me.HazelDev_Panel1.Controls.Add(Me.optMix)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(1213, 49)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'chkKOrder
        '
        Me.chkKOrder.Checked = False
        Me.chkKOrder.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkKOrder.Location = New System.Drawing.Point(1081, 18)
        Me.chkKOrder.Name = "chkKOrder"
        Me.chkKOrder.Size = New System.Drawing.Size(66, 17)
        Me.chkKOrder.TabIndex = 146
        Me.chkKOrder.Text = "Order K"
        Me.chkKOrder.TransparencyKey = System.Drawing.Color.Empty
        '
        'chkOrder
        '
        Me.chkOrder.Checked = False
        Me.chkOrder.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkOrder.Location = New System.Drawing.Point(1009, 18)
        Me.chkOrder.Name = "chkOrder"
        Me.chkOrder.Size = New System.Drawing.Size(66, 17)
        Me.chkOrder.TabIndex = 145
        Me.chkOrder.Text = "Order"
        Me.chkOrder.TransparencyKey = System.Drawing.Color.Empty
        '
        'chkExport
        '
        Me.chkExport.Checked = False
        Me.chkExport.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkExport.Location = New System.Drawing.Point(937, 18)
        Me.chkExport.Name = "chkExport"
        Me.chkExport.Size = New System.Drawing.Size(66, 17)
        Me.chkExport.TabIndex = 144
        Me.chkExport.Text = "Export"
        Me.chkExport.TransparencyKey = System.Drawing.Color.Empty
        '
        'optMixPlan
        '
        Me.optMixPlan.BackColor = System.Drawing.Color.White
        Me.optMixPlan.Checked = False
        Me.optMixPlan.Location = New System.Drawing.Point(850, 18)
        Me.optMixPlan.Name = "optMixPlan"
        Me.optMixPlan.Size = New System.Drawing.Size(70, 22)
        Me.optMixPlan.TabIndex = 143
        Me.optMixPlan.Text = "MIX Plan"
        Me.optMixPlan.TransparencyKey = System.Drawing.Color.Empty
        '
        'optReject
        '
        Me.optReject.BackColor = System.Drawing.Color.White
        Me.optReject.Checked = False
        Me.optReject.Location = New System.Drawing.Point(747, 18)
        Me.optReject.Name = "optReject"
        Me.optReject.Size = New System.Drawing.Size(92, 22)
        Me.optReject.TabIndex = 142
        Me.optReject.Text = "Reject Export"
        Me.optReject.TransparencyKey = System.Drawing.Color.Empty
        '
        'optPolBox
        '
        Me.optPolBox.BackColor = System.Drawing.Color.White
        Me.optPolBox.Checked = False
        Me.optPolBox.Location = New System.Drawing.Point(664, 18)
        Me.optPolBox.Name = "optPolBox"
        Me.optPolBox.Size = New System.Drawing.Size(77, 22)
        Me.optPolBox.TabIndex = 141
        Me.optPolBox.Text = "Polish Box"
        Me.optPolBox.TransparencyKey = System.Drawing.Color.Empty
        '
        'optSorting
        '
        Me.optSorting.BackColor = System.Drawing.Color.White
        Me.optSorting.Checked = False
        Me.optSorting.Location = New System.Drawing.Point(575, 18)
        Me.optSorting.Name = "optSorting"
        Me.optSorting.Size = New System.Drawing.Size(83, 22)
        Me.optSorting.TabIndex = 140
        Me.optSorting.Text = "MIX Sorting"
        Me.optSorting.TransparencyKey = System.Drawing.Color.Empty
        '
        'optGrdPcu
        '
        Me.optGrdPcu.BackColor = System.Drawing.Color.White
        Me.optGrdPcu.Checked = False
        Me.optGrdPcu.Location = New System.Drawing.Point(433, 18)
        Me.optGrdPcu.Name = "optGrdPcu"
        Me.optGrdPcu.Size = New System.Drawing.Size(131, 22)
        Me.optGrdPcu.TabIndex = 139
        Me.optGrdPcu.Text = "Grading-PCU Sorting"
        Me.optGrdPcu.TransparencyKey = System.Drawing.Color.Empty
        '
        'optPcu
        '
        Me.optPcu.BackColor = System.Drawing.Color.White
        Me.optPcu.Checked = False
        Me.optPcu.Location = New System.Drawing.Point(338, 18)
        Me.optPcu.Name = "optPcu"
        Me.optPcu.Size = New System.Drawing.Size(87, 22)
        Me.optPcu.TabIndex = 138
        Me.optPcu.Text = "PCU Sorting"
        Me.optPcu.TransparencyKey = System.Drawing.Color.Empty
        '
        'optGrading
        '
        Me.optGrading.BackColor = System.Drawing.Color.White
        Me.optGrading.Checked = False
        Me.optGrading.Location = New System.Drawing.Point(265, 18)
        Me.optGrading.Name = "optGrading"
        Me.optGrading.Size = New System.Drawing.Size(64, 22)
        Me.optGrading.TabIndex = 137
        Me.optGrading.Text = "Grading"
        Me.optGrading.TransparencyKey = System.Drawing.Color.Empty
        '
        'optMix
        '
        Me.optMix.BackColor = System.Drawing.Color.White
        Me.optMix.Checked = False
        Me.optMix.Location = New System.Drawing.Point(214, 18)
        Me.optMix.Name = "optMix"
        Me.optMix.Size = New System.Drawing.Size(44, 22)
        Me.optMix.TabIndex = 46
        Me.optMix.Text = "MIX"
        Me.optMix.TransparencyKey = System.Drawing.Color.Empty
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
        'txtPackNo
        '
        Me.txtPackNo.Location = New System.Drawing.Point(801, 540)
        Me.txtPackNo.Name = "txtPackNo"
        Me.txtPackNo.ReadOnly = True
        Me.txtPackNo.Size = New System.Drawing.Size(102, 21)
        Me.txtPackNo.TabIndex = 136
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
        Me.cmdSave.Location = New System.Drawing.Point(1117, 537)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(99, 28)
        Me.cmdSave.TabIndex = 45
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'frm_RghMixTrf
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1221, 568)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_RghMixTrf"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mix/Grading - Rough Transfer"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents txtValue As System.Windows.Forms.TextBox
    Friend WithEvents txtPrice As System.Windows.Forms.TextBox
    Friend WithEvents txtTotCts As System.Windows.Forms.TextBox
    Friend WithEvents txtTotPcs As System.Windows.Forms.TextBox
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents txtPackNo As System.Windows.Forms.TextBox
    Friend WithEvents optMix As DiaStock.HazelDev_RadioButton
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents optPcu As DiaStock.HazelDev_RadioButton
    Friend WithEvents optGrading As DiaStock.HazelDev_RadioButton
    Friend WithEvents optSorting As DiaStock.HazelDev_RadioButton
    Friend WithEvents optGrdPcu As DiaStock.HazelDev_RadioButton
    Friend WithEvents txtSupParNo As System.Windows.Forms.TextBox
    Friend WithEvents optPolBox As DiaStock.HazelDev_RadioButton
    Friend WithEvents optMixPlan As DiaStock.HazelDev_RadioButton
    Friend WithEvents optReject As DiaStock.HazelDev_RadioButton
    Friend WithEvents Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Date1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OutTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PassableP1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParcelNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Department As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RefNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Side As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Special As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Assortment As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents chkOrder As DiaStock.HazelDev_CheckBox
    Friend WithEvents chkExport As DiaStock.HazelDev_CheckBox
    Friend WithEvents chkKOrder As DiaStock.HazelDev_CheckBox
    Friend WithEvents chkSizing As DiaStock.HazelDev_CheckBox
End Class
