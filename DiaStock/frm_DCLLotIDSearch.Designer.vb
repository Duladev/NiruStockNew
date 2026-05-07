<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DCLLotIDSearch
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
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.flxUpload = New System.Windows.Forms.DataGridView()
        Me.Column29 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_Panel3 = New DiaStock.HazelDev_Panel()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.HazelDev_Panel2 = New DiaStock.HazelDev_Panel()
        Me.txtSupParNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtLotID = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdSearch = New DiaStock.HazelDev_Button()
        Me.cmdLoad = New DiaStock.HazelDev_Button()
        Me.cmdOpen = New DiaStock.HazelDev_Button()
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.HazelDev_Button1 = New DiaStock.HazelDev_Button()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column18 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column19 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column20 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column21 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column22 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column23 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column24 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column25 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column26 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column27 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column28 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column30 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column31 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        CType(Me.flxUpload, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel3.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel2.SuspendLayout()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxUpload)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel3)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(1408, 605)
        Me.HazelDev_ThemeContainer1.TabIndex = 5
        Me.HazelDev_ThemeContainer1.Text = "LOT ID SEARCH"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'flxUpload
        '
        Me.flxUpload.AllowUserToAddRows = False
        Me.flxUpload.AllowUserToDeleteRows = False
        Me.flxUpload.AllowUserToResizeColumns = False
        Me.flxUpload.AllowUserToResizeRows = False
        Me.flxUpload.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxUpload.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column29})
        Me.flxUpload.Location = New System.Drawing.Point(1265, 157)
        Me.flxUpload.Name = "flxUpload"
        Me.flxUpload.ReadOnly = True
        Me.flxUpload.RowHeadersVisible = False
        Me.flxUpload.Size = New System.Drawing.Size(135, 436)
        Me.flxUpload.TabIndex = 45
        '
        'Column29
        '
        Me.Column29.HeaderText = "Lot ID"
        Me.Column29.Name = "Column29"
        Me.Column29.ReadOnly = True
        '
        'HazelDev_Panel3
        '
        Me.HazelDev_Panel3.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel3.Controls.Add(Me.flxDetails)
        Me.HazelDev_Panel3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel3.Location = New System.Drawing.Point(3, 157)
        Me.HazelDev_Panel3.Name = "HazelDev_Panel3"
        Me.HazelDev_Panel3.Size = New System.Drawing.Size(1256, 445)
        Me.HazelDev_Panel3.TabIndex = 33
        Me.HazelDev_Panel3.Text = "HazelDev_Panel3"
        Me.HazelDev_Panel3.TransparencyKey = System.Drawing.Color.Empty
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column12, Me.Column7, Me.Column8, Me.Column9, Me.Column10, Me.Column11, Me.Column13, Me.Column14, Me.Column15, Me.Column17, Me.Column16, Me.Column18, Me.Column19, Me.Column20, Me.Column21, Me.Column22, Me.Column23, Me.Column24, Me.Column25, Me.Column26, Me.Column27, Me.Column28, Me.Column30, Me.Column31})
        Me.flxDetails.Location = New System.Drawing.Point(3, 3)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.ReadOnly = True
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(1250, 433)
        Me.flxDetails.TabIndex = 165
        '
        'HazelDev_Panel2
        '
        Me.HazelDev_Panel2.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel2.Controls.Add(Me.txtSupParNo)
        Me.HazelDev_Panel2.Controls.Add(Me.Label1)
        Me.HazelDev_Panel2.Controls.Add(Me.txtLotID)
        Me.HazelDev_Panel2.Controls.Add(Me.Label4)
        Me.HazelDev_Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel2.Location = New System.Drawing.Point(3, 109)
        Me.HazelDev_Panel2.Name = "HazelDev_Panel2"
        Me.HazelDev_Panel2.Size = New System.Drawing.Size(1397, 42)
        Me.HazelDev_Panel2.TabIndex = 32
        Me.HazelDev_Panel2.Text = "HazelDev_Panel2"
        Me.HazelDev_Panel2.TransparencyKey = System.Drawing.Color.Empty
        '
        'txtSupParNo
        '
        Me.txtSupParNo.Location = New System.Drawing.Point(426, 6)
        Me.txtSupParNo.Name = "txtSupParNo"
        Me.txtSupParNo.ReadOnly = True
        Me.txtSupParNo.Size = New System.Drawing.Size(99, 22)
        Me.txtSupParNo.TabIndex = 91
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(320, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 14)
        Me.Label1.TabIndex = 90
        Me.Label1.Text = "Sup Parcel No"
        '
        'txtLotID
        '
        Me.txtLotID.Location = New System.Drawing.Point(109, 6)
        Me.txtLotID.Name = "txtLotID"
        Me.txtLotID.Size = New System.Drawing.Size(99, 22)
        Me.txtLotID.TabIndex = 89
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(3, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 14)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Lot ID"
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSearch)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdLoad)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdOpen)
        Me.HazelDev_Panel1.Controls.Add(Me.txtFilePath)
        Me.HazelDev_Panel1.Controls.Add(Me.HazelDev_Button1)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(1397, 49)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSearch.FlatAppearance.BorderSize = 0
        Me.cmdSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.Color.White
        Me.cmdSearch.Location = New System.Drawing.Point(1119, 13)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.Size = New System.Drawing.Size(99, 28)
        Me.cmdSearch.TabIndex = 81
        Me.cmdSearch.Text = "Search All"
        Me.cmdSearch.UseVisualStyleBackColor = False
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
        Me.cmdLoad.Location = New System.Drawing.Point(1014, 13)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.Size = New System.Drawing.Size(99, 28)
        Me.cmdLoad.TabIndex = 80
        Me.cmdLoad.Text = "Load File"
        Me.cmdLoad.UseVisualStyleBackColor = False
        '
        'cmdOpen
        '
        Me.cmdOpen.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdOpen.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdOpen.FlatAppearance.BorderSize = 0
        Me.cmdOpen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdOpen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOpen.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOpen.ForeColor = System.Drawing.Color.White
        Me.cmdOpen.Location = New System.Drawing.Point(909, 13)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(99, 28)
        Me.cmdOpen.TabIndex = 79
        Me.cmdOpen.Text = "Open File"
        Me.cmdOpen.UseVisualStyleBackColor = False
        '
        'txtFilePath
        '
        Me.txtFilePath.Enabled = False
        Me.txtFilePath.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilePath.Location = New System.Drawing.Point(1224, 16)
        Me.txtFilePath.MaxLength = 0
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.Size = New System.Drawing.Size(18, 21)
        Me.txtFilePath.TabIndex = 78
        Me.txtFilePath.Visible = False
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
        Me.HazelDev_Button1.Location = New System.Drawing.Point(426, 13)
        Me.HazelDev_Button1.Name = "HazelDev_Button1"
        Me.HazelDev_Button1.Size = New System.Drawing.Size(99, 28)
        Me.HazelDev_Button1.TabIndex = 46
        Me.HazelDev_Button1.Text = "Parcel Process"
        Me.HazelDev_Button1.UseVisualStyleBackColor = False
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
        Me.cmdSave.Text = "Excel"
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
        'Column1
        '
        Me.Column1.HeaderText = "Lot ID"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "Lot Name"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.HeaderText = "Quantity"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.HeaderText = "Weight"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column5
        '
        Me.Column5.HeaderText = "Remarks"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 200
        '
        'Column6
        '
        Me.Column6.HeaderText = "Export No"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'Column12
        '
        Me.Column12.HeaderText = "Export Date"
        Me.Column12.Name = "Column12"
        Me.Column12.ReadOnly = True
        '
        'Column7
        '
        Me.Column7.HeaderText = "Mining Company"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        '
        'Column8
        '
        Me.Column8.HeaderText = "Trace VendorID"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        '
        'Column9
        '
        Me.Column9.HeaderText = "Rough assortment name"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        '
        'Column10
        '
        Me.Column10.HeaderText = "Rough input date"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        '
        'Column11
        '
        Me.Column11.HeaderText = "SupParcelNo"
        Me.Column11.Name = "Column11"
        Me.Column11.ReadOnly = True
        '
        'Column13
        '
        Me.Column13.HeaderText = "Import Lot ID"
        Me.Column13.Name = "Column13"
        Me.Column13.ReadOnly = True
        '
        'Column14
        '
        Me.Column14.HeaderText = "Item Name"
        Me.Column14.Name = "Column14"
        Me.Column14.ReadOnly = True
        '
        'Column15
        '
        Me.Column15.HeaderText = "Equipment"
        Me.Column15.Name = "Column15"
        Me.Column15.ReadOnly = True
        '
        'Column17
        '
        Me.Column17.HeaderText = "Controller"
        Me.Column17.Name = "Column17"
        Me.Column17.ReadOnly = True
        '
        'Column16
        '
        Me.Column16.HeaderText = "Material procurement Flow"
        Me.Column16.Name = "Column16"
        Me.Column16.ReadOnly = True
        '
        'Column18
        '
        Me.Column18.HeaderText = "Material Vendor Name"
        Me.Column18.Name = "Column18"
        Me.Column18.ReadOnly = True
        '
        'Column19
        '
        Me.Column19.HeaderText = "Material Vendor CSR certification"
        Me.Column19.Name = "Column19"
        Me.Column19.ReadOnly = True
        '
        'Column20
        '
        Me.Column20.HeaderText = "Mining Origin"
        Me.Column20.Name = "Column20"
        Me.Column20.ReadOnly = True
        '
        'Column21
        '
        Me.Column21.HeaderText = "Mine"
        Me.Column21.Name = "Column21"
        Me.Column21.ReadOnly = True
        '
        'Column22
        '
        Me.Column22.HeaderText = "Mining Guarantee level"
        Me.Column22.Name = "Column22"
        Me.Column22.ReadOnly = True
        '
        'Column23
        '
        Me.Column23.HeaderText = "Stone Polishing Flow"
        Me.Column23.Name = "Column23"
        Me.Column23.ReadOnly = True
        '
        'Column24
        '
        Me.Column24.HeaderText = "Stone Polishing company"
        Me.Column24.Name = "Column24"
        Me.Column24.ReadOnly = True
        '
        'Column25
        '
        Me.Column25.HeaderText = "Stone Polishing company CSR Cert"
        Me.Column25.Name = "Column25"
        Me.Column25.ReadOnly = True
        '
        'Column26
        '
        Me.Column26.HeaderText = "Rough polished reconciliation Guarantee Level"
        Me.Column26.Name = "Column26"
        Me.Column26.ReadOnly = True
        '
        'Column27
        '
        Me.Column27.HeaderText = "Tech controlled system"
        Me.Column27.Name = "Column27"
        Me.Column27.ReadOnly = True
        '
        'Column28
        '
        Me.Column28.HeaderText = "Tech controlled ID"
        Me.Column28.Name = "Column28"
        Me.Column28.ReadOnly = True
        '
        'Column30
        '
        Me.Column30.HeaderText = "Rough Lot ID"
        Me.Column30.Name = "Column30"
        Me.Column30.ReadOnly = True
        '
        'Column31
        '
        Me.Column31.HeaderText = "Ref"
        Me.Column31.Name = "Column31"
        Me.Column31.ReadOnly = True
        '
        'frm_DCLLotIDSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1408, 605)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_DCLLotIDSearch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Lot ID Search"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        CType(Me.flxUpload, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel3.ResumeLayout(False)
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel2.ResumeLayout(False)
        Me.HazelDev_Panel2.PerformLayout()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents HazelDev_Panel2 As DiaStock.HazelDev_Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents txtLotID As System.Windows.Forms.TextBox
    Friend WithEvents HazelDev_Panel3 As DiaStock.HazelDev_Panel
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents txtSupParNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents HazelDev_Button1 As DiaStock.HazelDev_Button
    Friend WithEvents flxUpload As System.Windows.Forms.DataGridView
    Friend WithEvents Column29 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cmdLoad As DiaStock.HazelDev_Button
    Friend WithEvents cmdOpen As DiaStock.HazelDev_Button
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
    Friend WithEvents cmdSearch As DiaStock.HazelDev_Button
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column20 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column21 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column22 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column23 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column24 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column25 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column26 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column27 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column28 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column30 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column31 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
