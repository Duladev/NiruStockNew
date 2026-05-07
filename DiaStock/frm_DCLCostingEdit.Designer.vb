<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DCLCostingEdit
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
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.cmdUpdateBase = New DiaStock.HazelDev_Button()
        Me.dtpInvDate = New System.Windows.Forms.DateTimePicker()
        Me.txtTotCts = New System.Windows.Forms.TextBox()
        Me.txtTotPcs = New System.Windows.Forms.TextBox()
        Me.txtCts = New System.Windows.Forms.TextBox()
        Me.txtPcs = New System.Windows.Forms.TextBox()
        Me.cmdUpdate = New DiaStock.HazelDev_Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtPackNo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtNewExportNo = New System.Windows.Forms.TextBox()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdMargin = New DiaStock.HazelDev_Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtMargin = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbClient = New System.Windows.Forms.ComboBox()
        Me.cmdCalculate = New DiaStock.HazelDev_Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmdExcel = New DiaStock.HazelDev_Button()
        Me.txtExportNo = New System.Windows.Forms.TextBox()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.Department = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Assortment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Reference1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Reference2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RoughPcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RoughCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Price = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExportNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExportPcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExportCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Labour = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GrLabour = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NFEValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Totals = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BOINo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BaseCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Category = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LotID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PackNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PackCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RghLabour = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AssortLabour = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BalPcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BalCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MasPcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MasCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Client = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Margin1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExtLabour = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdUpdateBase)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.dtpInvDate)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdUpdate)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label4)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPackNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label3)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtNewExportNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxDetails)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(1242, 677)
        Me.HazelDev_ThemeContainer1.TabIndex = 6
        Me.HazelDev_ThemeContainer1.Text = "COSTING EDITOR"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmdUpdateBase
        '
        Me.cmdUpdateBase.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdUpdateBase.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdUpdateBase.FlatAppearance.BorderSize = 0
        Me.cmdUpdateBase.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdUpdateBase.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdUpdateBase.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdUpdateBase.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUpdateBase.ForeColor = System.Drawing.Color.White
        Me.cmdUpdateBase.Location = New System.Drawing.Point(984, 644)
        Me.cmdUpdateBase.Name = "cmdUpdateBase"
        Me.cmdUpdateBase.Size = New System.Drawing.Size(99, 28)
        Me.cmdUpdateBase.TabIndex = 113
        Me.cmdUpdateBase.Text = "Update Base"
        Me.cmdUpdateBase.UseVisualStyleBackColor = False
        '
        'dtpInvDate
        '
        Me.dtpInvDate.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpInvDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpInvDate.Location = New System.Drawing.Point(876, 649)
        Me.dtpInvDate.Name = "dtpInvDate"
        Me.dtpInvDate.Size = New System.Drawing.Size(102, 21)
        Me.dtpInvDate.TabIndex = 112
        '
        'txtTotCts
        '
        Me.txtTotCts.Location = New System.Drawing.Point(779, 649)
        Me.txtTotCts.Name = "txtTotCts"
        Me.txtTotCts.ReadOnly = True
        Me.txtTotCts.Size = New System.Drawing.Size(91, 21)
        Me.txtTotCts.TabIndex = 111
        Me.txtTotCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotPcs
        '
        Me.txtTotPcs.Location = New System.Drawing.Point(682, 649)
        Me.txtTotPcs.Name = "txtTotPcs"
        Me.txtTotPcs.ReadOnly = True
        Me.txtTotPcs.Size = New System.Drawing.Size(91, 21)
        Me.txtTotPcs.TabIndex = 110
        Me.txtTotPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCts
        '
        Me.txtCts.Location = New System.Drawing.Point(585, 649)
        Me.txtCts.Name = "txtCts"
        Me.txtCts.ReadOnly = True
        Me.txtCts.Size = New System.Drawing.Size(91, 21)
        Me.txtCts.TabIndex = 109
        Me.txtCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPcs
        '
        Me.txtPcs.Location = New System.Drawing.Point(488, 649)
        Me.txtPcs.Name = "txtPcs"
        Me.txtPcs.ReadOnly = True
        Me.txtPcs.Size = New System.Drawing.Size(91, 21)
        Me.txtPcs.TabIndex = 108
        Me.txtPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.cmdUpdate.Location = New System.Drawing.Point(349, 644)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(99, 28)
        Me.cmdUpdate.TabIndex = 103
        Me.cmdUpdate.Text = "Update"
        Me.cmdUpdate.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(161, 652)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(92, 15)
        Me.Label4.TabIndex = 102
        Me.Label4.Text = "Packing List No"
        '
        'txtPackNo
        '
        Me.txtPackNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPackNo.Location = New System.Drawing.Point(259, 649)
        Me.txtPackNo.Name = "txtPackNo"
        Me.txtPackNo.Size = New System.Drawing.Size(84, 21)
        Me.txtPackNo.TabIndex = 101
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(4, 652)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 15)
        Me.Label3.TabIndex = 100
        Me.Label3.Text = "Export No"
        '
        'txtNewExportNo
        '
        Me.txtNewExportNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewExportNo.Location = New System.Drawing.Point(71, 649)
        Me.txtNewExportNo.Name = "txtNewExportNo"
        Me.txtNewExportNo.Size = New System.Drawing.Size(84, 21)
        Me.txtNewExportNo.TabIndex = 99
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Department, Me.Assortment, Me.Reference1, Me.Reference2, Me.RoughPcs, Me.RoughCts, Me.Price, Me.ExportNo, Me.ExportPcs, Me.ExportCts, Me.Labour, Me.GrLabour, Me.NFEValue, Me.Totals, Me.BOINo, Me.BaseCost, Me.Category, Me.LotID, Me.PackNo, Me.PackCode, Me.RghLabour, Me.AssortLabour, Me.ID, Me.BalPcs, Me.BalCts, Me.MasPcs, Me.MasCts, Me.Client, Me.Margin1, Me.ExtLabour, Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6})
        Me.flxDetails.Location = New System.Drawing.Point(6, 109)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.flxDetails.Size = New System.Drawing.Size(1232, 529)
        Me.flxDetails.TabIndex = 68
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmdMargin)
        Me.HazelDev_Panel1.Controls.Add(Me.Label2)
        Me.HazelDev_Panel1.Controls.Add(Me.txtMargin)
        Me.HazelDev_Panel1.Controls.Add(Me.Label1)
        Me.HazelDev_Panel1.Controls.Add(Me.cmbClient)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdCalculate)
        Me.HazelDev_Panel1.Controls.Add(Me.Label6)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExcel)
        Me.HazelDev_Panel1.Controls.Add(Me.txtExportNo)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(1235, 49)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmdMargin
        '
        Me.cmdMargin.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdMargin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdMargin.FlatAppearance.BorderSize = 0
        Me.cmdMargin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdMargin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdMargin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdMargin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdMargin.ForeColor = System.Drawing.Color.White
        Me.cmdMargin.Location = New System.Drawing.Point(923, 13)
        Me.cmdMargin.Name = "cmdMargin"
        Me.cmdMargin.Size = New System.Drawing.Size(99, 28)
        Me.cmdMargin.TabIndex = 104
        Me.cmdMargin.Text = "Add Margin"
        Me.cmdMargin.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(721, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 15)
        Me.Label2.TabIndex = 103
        Me.Label2.Text = "Margin"
        '
        'txtMargin
        '
        Me.txtMargin.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMargin.Location = New System.Drawing.Point(788, 14)
        Me.txtMargin.Name = "txtMargin"
        Me.txtMargin.Size = New System.Drawing.Size(84, 21)
        Me.txtMargin.TabIndex = 102
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(498, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 15)
        Me.Label1.TabIndex = 100
        Me.Label1.Text = "Client"
        '
        'cmbClient
        '
        Me.cmbClient.FormattingEnabled = True
        Me.cmbClient.IntegralHeight = False
        Me.cmbClient.Location = New System.Drawing.Point(552, 13)
        Me.cmbClient.Name = "cmbClient"
        Me.cmbClient.Size = New System.Drawing.Size(159, 22)
        Me.cmbClient.TabIndex = 101
        '
        'cmdCalculate
        '
        Me.cmdCalculate.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdCalculate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdCalculate.FlatAppearance.BorderSize = 0
        Me.cmdCalculate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdCalculate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCalculate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCalculate.ForeColor = System.Drawing.Color.White
        Me.cmdCalculate.Location = New System.Drawing.Point(1028, 13)
        Me.cmdCalculate.Name = "cmdCalculate"
        Me.cmdCalculate.Size = New System.Drawing.Size(99, 28)
        Me.cmdCalculate.TabIndex = 99
        Me.cmdCalculate.Text = "Calculate"
        Me.cmdCalculate.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(319, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 15)
        Me.Label6.TabIndex = 98
        Me.Label6.Text = "Export No"
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
        Me.cmdExcel.Location = New System.Drawing.Point(1133, 13)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(99, 28)
        Me.cmdExcel.TabIndex = 97
        Me.cmdExcel.Text = "Excel"
        Me.cmdExcel.UseVisualStyleBackColor = False
        '
        'txtExportNo
        '
        Me.txtExportNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExportNo.Location = New System.Drawing.Point(386, 13)
        Me.txtExportNo.Name = "txtExportNo"
        Me.txtExportNo.Size = New System.Drawing.Size(84, 21)
        Me.txtExportNo.TabIndex = 0
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
        'Department
        '
        Me.Department.HeaderText = "Department"
        Me.Department.Name = "Department"
        '
        'Assortment
        '
        Me.Assortment.HeaderText = "Assortment"
        Me.Assortment.Name = "Assortment"
        '
        'Reference1
        '
        Me.Reference1.HeaderText = "Reference 1"
        Me.Reference1.Name = "Reference1"
        '
        'Reference2
        '
        Me.Reference2.HeaderText = "Reference 2"
        Me.Reference2.Name = "Reference2"
        '
        'RoughPcs
        '
        DataGridViewCellStyle1.NullValue = Nothing
        Me.RoughPcs.DefaultCellStyle = DataGridViewCellStyle1
        Me.RoughPcs.HeaderText = "Rough Pcs"
        Me.RoughPcs.Name = "RoughPcs"
        '
        'RoughCts
        '
        Me.RoughCts.HeaderText = "Rough Cts"
        Me.RoughCts.Name = "RoughCts"
        '
        'Price
        '
        Me.Price.HeaderText = "Price"
        Me.Price.Name = "Price"
        '
        'ExportNo
        '
        Me.ExportNo.HeaderText = "Export No"
        Me.ExportNo.Name = "ExportNo"
        '
        'ExportPcs
        '
        Me.ExportPcs.HeaderText = "Export Pcs"
        Me.ExportPcs.Name = "ExportPcs"
        '
        'ExportCts
        '
        Me.ExportCts.HeaderText = "Export Cts"
        Me.ExportCts.Name = "ExportCts"
        '
        'Labour
        '
        Me.Labour.HeaderText = "Labour"
        Me.Labour.Name = "Labour"
        '
        'GrLabour
        '
        Me.GrLabour.HeaderText = "Gr Labour"
        Me.GrLabour.Name = "GrLabour"
        '
        'NFEValue
        '
        Me.NFEValue.HeaderText = "NFE Value"
        Me.NFEValue.Name = "NFEValue"
        '
        'Totals
        '
        Me.Totals.HeaderText = "Totals"
        Me.Totals.Name = "Totals"
        '
        'BOINo
        '
        Me.BOINo.HeaderText = "BOINo"
        Me.BOINo.Name = "BOINo"
        Me.BOINo.Width = 200
        '
        'BaseCost
        '
        Me.BaseCost.HeaderText = "Base Cost"
        Me.BaseCost.Name = "BaseCost"
        '
        'Category
        '
        Me.Category.HeaderText = "Category"
        Me.Category.Name = "Category"
        '
        'LotID
        '
        Me.LotID.HeaderText = "Lot ID"
        Me.LotID.Name = "LotID"
        '
        'PackNo
        '
        Me.PackNo.HeaderText = "PackNo"
        Me.PackNo.Name = "PackNo"
        '
        'PackCode
        '
        Me.PackCode.HeaderText = "PackCode"
        Me.PackCode.Name = "PackCode"
        '
        'RghLabour
        '
        Me.RghLabour.HeaderText = "RghLabour"
        Me.RghLabour.Name = "RghLabour"
        '
        'AssortLabour
        '
        Me.AssortLabour.HeaderText = "AssortLabour"
        Me.AssortLabour.Name = "AssortLabour"
        '
        'ID
        '
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        Me.ID.ReadOnly = True
        '
        'BalPcs
        '
        Me.BalPcs.HeaderText = "Bal Pcs"
        Me.BalPcs.Name = "BalPcs"
        '
        'BalCts
        '
        Me.BalCts.HeaderText = "Bal Cts"
        Me.BalCts.Name = "BalCts"
        '
        'MasPcs
        '
        Me.MasPcs.HeaderText = "Mas Pcs"
        Me.MasPcs.Name = "MasPcs"
        '
        'MasCts
        '
        Me.MasCts.HeaderText = "Mas Cts"
        Me.MasCts.Name = "MasCts"
        '
        'Client
        '
        Me.Client.HeaderText = "Client"
        Me.Client.Name = "Client"
        Me.Client.ReadOnly = True
        '
        'Margin1
        '
        Me.Margin1.HeaderText = "Margin"
        Me.Margin1.Name = "Margin1"
        '
        'ExtLabour
        '
        Me.ExtLabour.HeaderText = "Ext Labour"
        Me.ExtLabour.Name = "ExtLabour"
        '
        'Column1
        '
        Me.Column1.HeaderText = "Max Value"
        Me.Column1.Name = "Column1"
        '
        'Column2
        '
        Me.Column2.HeaderText = "Export Date"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.HeaderText = "Hard Cost"
        Me.Column3.Name = "Column3"
        '
        'Column4
        '
        Me.Column4.HeaderText = "InID"
        Me.Column4.Name = "Column4"
        '
        'Column5
        '
        Me.Column5.HeaderText = "Sales Rate"
        Me.Column5.Name = "Column5"
        '
        'Column6
        '
        Me.Column6.HeaderText = "Status"
        Me.Column6.Name = "Column6"
        '
        'frm_DCLCostingEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1242, 677)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_DCLCostingEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Costing Editor"
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
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents txtExportNo As System.Windows.Forms.TextBox
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmdCalculate As DiaStock.HazelDev_Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbClient As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtMargin As System.Windows.Forms.TextBox
    Friend WithEvents cmdMargin As DiaStock.HazelDev_Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPackNo As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtNewExportNo As System.Windows.Forms.TextBox
    Friend WithEvents cmdUpdate As DiaStock.HazelDev_Button
    Friend WithEvents txtTotCts As System.Windows.Forms.TextBox
    Friend WithEvents txtTotPcs As System.Windows.Forms.TextBox
    Friend WithEvents txtCts As System.Windows.Forms.TextBox
    Friend WithEvents txtPcs As System.Windows.Forms.TextBox
    Friend WithEvents dtpInvDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmdUpdateBase As DiaStock.HazelDev_Button
    Friend WithEvents Department As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Assortment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Reference1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Reference2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RoughPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RoughCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExportNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExportPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExportCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Labour As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GrLabour As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NFEValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Totals As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BOINo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BaseCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Category As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LotID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PackNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PackCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RghLabour As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AssortLabour As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BalPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BalCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MasPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MasCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Client As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Margin1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExtLabour As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
