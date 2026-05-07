<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ExpRejects
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
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.cmbPack = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbRate = New System.Windows.Forms.ComboBox()
        Me.HazelDev_Button1 = New DiaStock.HazelDev_Button()
        Me.cmdAdd = New DiaStock.HazelDev_Button()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Type = New System.Windows.Forms.Label()
        Me.txtNewPcs = New System.Windows.Forms.TextBox()
        Me.txtNewCts = New System.Windows.Forms.TextBox()
        Me.cmbAssort = New System.Windows.Forms.ComboBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.txtParNo = New System.Windows.Forms.TextBox()
        Me.cmbDept = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmdExcel = New DiaStock.HazelDev_Button()
        Me.txtPcs = New System.Windows.Forms.TextBox()
        Me.txtCts = New System.Windows.Forms.TextBox()
        Me.txtTotPcs = New System.Windows.Forms.TextBox()
        Me.txtTotCts = New System.Windows.Forms.TextBox()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Assortment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdUpdateAssort = New DiaStock.HazelDev_Button()
        Me.cmdDelete = New DiaStock.HazelDev_Button()
        Me.cmdUpdate = New DiaStock.HazelDev_Button()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbPack)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbRate)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Button1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdAdd)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label36)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label35)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Type)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtNewPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtNewCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbAssort)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label41)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtParNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbDept)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label10)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdExcel)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxDetails)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(674, 695)
        Me.HazelDev_ThemeContainer1.TabIndex = 7
        Me.HazelDev_ThemeContainer1.Text = "SORTING REJECTS"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmbPack
        '
        Me.cmbPack.FormattingEnabled = True
        Me.cmbPack.IntegralHeight = False
        Me.cmbPack.Location = New System.Drawing.Point(361, 662)
        Me.cmbPack.Name = "cmbPack"
        Me.cmbPack.Size = New System.Drawing.Size(99, 23)
        Me.cmbPack.TabIndex = 178
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(358, 644)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 15)
        Me.Label2.TabIndex = 177
        Me.Label2.Text = "Pack No"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(191, 643)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 15)
        Me.Label1.TabIndex = 176
        Me.Label1.Text = "Rate Code"
        '
        'cmbRate
        '
        Me.cmbRate.FormattingEnabled = True
        Me.cmbRate.IntegralHeight = False
        Me.cmbRate.Location = New System.Drawing.Point(194, 660)
        Me.cmbRate.Name = "cmbRate"
        Me.cmbRate.Size = New System.Drawing.Size(160, 23)
        Me.cmbRate.TabIndex = 175
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
        Me.HazelDev_Button1.Location = New System.Drawing.Point(7, 657)
        Me.HazelDev_Button1.Name = "HazelDev_Button1"
        Me.HazelDev_Button1.Size = New System.Drawing.Size(99, 28)
        Me.HazelDev_Button1.TabIndex = 174
        Me.HazelDev_Button1.Text = "Assortments"
        Me.HazelDev_Button1.UseVisualStyleBackColor = False
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
        Me.cmdAdd.Location = New System.Drawing.Point(593, 118)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(72, 28)
        Me.cmdAdd.TabIndex = 53
        Me.cmdAdd.Text = "Add"
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.Color.Transparent
        Me.Label36.Location = New System.Drawing.Point(523, 106)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(24, 15)
        Me.Label36.TabIndex = 173
        Me.Label36.Text = "Cts"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.Color.Transparent
        Me.Label35.Location = New System.Drawing.Point(456, 106)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(27, 15)
        Me.Label35.TabIndex = 172
        Me.Label35.Text = "Pcs"
        '
        'Type
        '
        Me.Type.AutoSize = True
        Me.Type.BackColor = System.Drawing.Color.Transparent
        Me.Type.Location = New System.Drawing.Point(214, 106)
        Me.Type.Name = "Type"
        Me.Type.Size = New System.Drawing.Size(33, 15)
        Me.Type.TabIndex = 171
        Me.Type.Text = "Type"
        '
        'txtNewPcs
        '
        Me.txtNewPcs.Location = New System.Drawing.Point(459, 123)
        Me.txtNewPcs.Name = "txtNewPcs"
        Me.txtNewPcs.Size = New System.Drawing.Size(61, 21)
        Me.txtNewPcs.TabIndex = 170
        Me.txtNewPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtNewCts
        '
        Me.txtNewCts.Location = New System.Drawing.Point(526, 123)
        Me.txtNewCts.Name = "txtNewCts"
        Me.txtNewCts.Size = New System.Drawing.Size(61, 21)
        Me.txtNewCts.TabIndex = 169
        Me.txtNewCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbAssort
        '
        Me.cmbAssort.FormattingEnabled = True
        Me.cmbAssort.IntegralHeight = False
        Me.cmbAssort.Location = New System.Drawing.Point(217, 123)
        Me.cmbAssort.Name = "cmbAssort"
        Me.cmbAssort.Size = New System.Drawing.Size(236, 23)
        Me.cmbAssort.TabIndex = 168
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.BackColor = System.Drawing.Color.Transparent
        Me.Label41.Location = New System.Drawing.Point(112, 106)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(64, 15)
        Me.Label41.TabIndex = 165
        Me.Label41.Text = "Parcel No."
        '
        'txtParNo
        '
        Me.txtParNo.Location = New System.Drawing.Point(114, 124)
        Me.txtParNo.Name = "txtParNo"
        Me.txtParNo.Size = New System.Drawing.Size(97, 21)
        Me.txtParNo.TabIndex = 164
        '
        'cmbDept
        '
        Me.cmbDept.FormattingEnabled = True
        Me.cmbDept.IntegralHeight = False
        Me.cmbDept.Location = New System.Drawing.Point(7, 124)
        Me.cmbDept.Name = "cmbDept"
        Me.cmbDept.Size = New System.Drawing.Size(99, 23)
        Me.cmbDept.TabIndex = 163
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(4, 106)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(72, 15)
        Me.Label10.TabIndex = 162
        Me.Label10.Text = "Department"
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
        Me.cmdExcel.Location = New System.Drawing.Point(7, 623)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(99, 28)
        Me.cmdExcel.TabIndex = 161
        Me.cmdExcel.Text = "Excel"
        Me.cmdExcel.UseVisualStyleBackColor = False
        '
        'txtPcs
        '
        Me.txtPcs.Location = New System.Drawing.Point(361, 622)
        Me.txtPcs.Name = "txtPcs"
        Me.txtPcs.ReadOnly = True
        Me.txtPcs.Size = New System.Drawing.Size(77, 21)
        Me.txtPcs.TabIndex = 160
        Me.txtPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCts
        '
        Me.txtCts.Location = New System.Drawing.Point(444, 622)
        Me.txtCts.Name = "txtCts"
        Me.txtCts.ReadOnly = True
        Me.txtCts.Size = New System.Drawing.Size(77, 21)
        Me.txtCts.TabIndex = 159
        Me.txtCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotPcs
        '
        Me.txtTotPcs.Location = New System.Drawing.Point(194, 622)
        Me.txtTotPcs.Name = "txtTotPcs"
        Me.txtTotPcs.ReadOnly = True
        Me.txtTotPcs.Size = New System.Drawing.Size(77, 21)
        Me.txtTotPcs.TabIndex = 158
        Me.txtTotPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotCts
        '
        Me.txtTotCts.Location = New System.Drawing.Point(277, 622)
        Me.txtTotCts.Name = "txtTotCts"
        Me.txtTotCts.ReadOnly = True
        Me.txtTotCts.Size = New System.Drawing.Size(77, 21)
        Me.txtTotCts.TabIndex = 157
        Me.txtTotCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Code, Me.Assortment, Me.Pcs, Me.Cts, Me.Column1, Me.Column2})
        Me.flxDetails.Location = New System.Drawing.Point(3, 153)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.ReadOnly = True
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(662, 463)
        Me.flxDetails.TabIndex = 43
        '
        'Code
        '
        Me.Code.HeaderText = "Parcel No."
        Me.Code.Name = "Code"
        Me.Code.ReadOnly = True
        '
        'Assortment
        '
        Me.Assortment.HeaderText = "Assortment"
        Me.Assortment.Name = "Assortment"
        Me.Assortment.ReadOnly = True
        Me.Assortment.Width = 200
        '
        'Pcs
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Pcs.DefaultCellStyle = DataGridViewCellStyle1
        Me.Pcs.HeaderText = "Pcs"
        Me.Pcs.Name = "Pcs"
        Me.Pcs.ReadOnly = True
        Me.Pcs.Width = 80
        '
        'Cts
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Cts.DefaultCellStyle = DataGridViewCellStyle2
        Me.Cts.HeaderText = "Cts"
        Me.Cts.Name = "Cts"
        Me.Cts.ReadOnly = True
        Me.Cts.Width = 80
        '
        'Column1
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle3
        Me.Column1.HeaderText = "Price"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "ID"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmdUpdateAssort)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdDelete)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdUpdate)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(662, 49)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmdUpdateAssort
        '
        Me.cmdUpdateAssort.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdUpdateAssort.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdUpdateAssort.FlatAppearance.BorderSize = 0
        Me.cmdUpdateAssort.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdUpdateAssort.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdUpdateAssort.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdUpdateAssort.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUpdateAssort.ForeColor = System.Drawing.Color.White
        Me.cmdUpdateAssort.Location = New System.Drawing.Point(319, 13)
        Me.cmdUpdateAssort.Name = "cmdUpdateAssort"
        Me.cmdUpdateAssort.Size = New System.Drawing.Size(99, 28)
        Me.cmdUpdateAssort.TabIndex = 164
        Me.cmdUpdateAssort.Text = "Update Assort"
        Me.cmdUpdateAssort.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.Color.Red
        Me.cmdDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdDelete.FlatAppearance.BorderSize = 0
        Me.cmdDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.ForeColor = System.Drawing.Color.White
        Me.cmdDelete.Location = New System.Drawing.Point(446, 13)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(99, 28)
        Me.cmdDelete.TabIndex = 163
        Me.cmdDelete.Text = "Delete"
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'cmdUpdate
        '
        Me.cmdUpdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdUpdate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdUpdate.FlatAppearance.BorderSize = 0
        Me.cmdUpdate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdUpdate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdUpdate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUpdate.ForeColor = System.Drawing.Color.White
        Me.cmdUpdate.Location = New System.Drawing.Point(551, 13)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(99, 28)
        Me.cmdUpdate.TabIndex = 162
        Me.cmdUpdate.Text = "Update Price"
        Me.cmdUpdate.UseVisualStyleBackColor = False
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
        'frm_ExpRejects
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(674, 695)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_ExpRejects"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sorting Rejects"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents txtPcs As System.Windows.Forms.TextBox
    Friend WithEvents txtCts As System.Windows.Forms.TextBox
    Friend WithEvents txtTotPcs As System.Windows.Forms.TextBox
    Friend WithEvents txtTotCts As System.Windows.Forms.TextBox
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdAdd As DiaStock.HazelDev_Button
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents txtParNo As System.Windows.Forms.TextBox
    Friend WithEvents cmbDept As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Type As System.Windows.Forms.Label
    Friend WithEvents txtNewPcs As System.Windows.Forms.TextBox
    Friend WithEvents txtNewCts As System.Windows.Forms.TextBox
    Friend WithEvents cmbAssort As System.Windows.Forms.ComboBox
    Friend WithEvents cmdUpdate As DiaStock.HazelDev_Button
    Friend WithEvents cmdDelete As DiaStock.HazelDev_Button
    Friend WithEvents cmdUpdateAssort As DiaStock.HazelDev_Button
    Friend WithEvents Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Assortment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HazelDev_Button1 As DiaStock.HazelDev_Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbRate As System.Windows.Forms.ComboBox
    Friend WithEvents cmbPack As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
