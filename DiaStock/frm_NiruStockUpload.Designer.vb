<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_NiruStockUpload
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
        Me.flxDept = New System.Windows.Forms.DataGridView()
        Me.Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DepartmentName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DCLCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DCLMCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DCLTCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HKCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HKMCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HKTCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ILCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ILMCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ILTCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.INDCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.INDMCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.INDTCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NYCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NYMCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NYTCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdLoad = New DiaStock.HazelDev_Button()
        Me.cmdOpen = New DiaStock.HazelDev_Button()
        Me.cmdReport = New DiaStock.HazelDev_Button()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        CType(Me.flxDept, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxDept)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(821, 515)
        Me.HazelDev_ThemeContainer1.TabIndex = 2
        Me.HazelDev_ThemeContainer1.Text = "NIRU STOCK UPLOAD"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'flxDept
        '
        Me.flxDept.AllowUserToAddRows = False
        Me.flxDept.AllowUserToDeleteRows = False
        Me.flxDept.AllowUserToResizeColumns = False
        Me.flxDept.AllowUserToResizeRows = False
        Me.flxDept.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDept.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Code, Me.DepartmentName, Me.DCLCts, Me.DCLMCts, Me.DCLTCts, Me.HKCts, Me.HKMCts, Me.HKTCts, Me.ILCts, Me.ILMCts, Me.ILTCts, Me.INDCts, Me.INDMCts, Me.INDTCts, Me.NYCts, Me.NYMCts, Me.NYTCts})
        Me.flxDept.Location = New System.Drawing.Point(5, 110)
        Me.flxDept.Name = "flxDept"
        Me.flxDept.ReadOnly = True
        Me.flxDept.RowHeadersVisible = False
        Me.flxDept.Size = New System.Drawing.Size(804, 393)
        Me.flxDept.TabIndex = 44
        '
        'Code
        '
        Me.Code.HeaderText = "Index"
        Me.Code.Name = "Code"
        Me.Code.ReadOnly = True
        Me.Code.Width = 50
        '
        'DepartmentName
        '
        Me.DepartmentName.HeaderText = "Assortment"
        Me.DepartmentName.Name = "DepartmentName"
        Me.DepartmentName.ReadOnly = True
        Me.DepartmentName.Width = 150
        '
        'DCLCts
        '
        Me.DCLCts.HeaderText = "DCLCts"
        Me.DCLCts.Name = "DCLCts"
        Me.DCLCts.ReadOnly = True
        '
        'DCLMCts
        '
        Me.DCLMCts.HeaderText = "DCLMCts"
        Me.DCLMCts.Name = "DCLMCts"
        Me.DCLMCts.ReadOnly = True
        '
        'DCLTCts
        '
        Me.DCLTCts.HeaderText = "DCLTCts"
        Me.DCLTCts.Name = "DCLTCts"
        Me.DCLTCts.ReadOnly = True
        '
        'HKCts
        '
        Me.HKCts.HeaderText = "HKCts"
        Me.HKCts.Name = "HKCts"
        Me.HKCts.ReadOnly = True
        '
        'HKMCts
        '
        Me.HKMCts.HeaderText = "HKMCts"
        Me.HKMCts.Name = "HKMCts"
        Me.HKMCts.ReadOnly = True
        '
        'HKTCts
        '
        Me.HKTCts.HeaderText = "HKTCts"
        Me.HKTCts.Name = "HKTCts"
        Me.HKTCts.ReadOnly = True
        '
        'ILCts
        '
        Me.ILCts.HeaderText = "ILCts"
        Me.ILCts.Name = "ILCts"
        Me.ILCts.ReadOnly = True
        '
        'ILMCts
        '
        Me.ILMCts.HeaderText = "ILMCts"
        Me.ILMCts.Name = "ILMCts"
        Me.ILMCts.ReadOnly = True
        '
        'ILTCts
        '
        Me.ILTCts.HeaderText = "ILTCts"
        Me.ILTCts.Name = "ILTCts"
        Me.ILTCts.ReadOnly = True
        '
        'INDCts
        '
        Me.INDCts.HeaderText = "INDCts"
        Me.INDCts.Name = "INDCts"
        Me.INDCts.ReadOnly = True
        '
        'INDMCts
        '
        Me.INDMCts.HeaderText = "INDMCts"
        Me.INDMCts.Name = "INDMCts"
        Me.INDMCts.ReadOnly = True
        '
        'INDTCts
        '
        Me.INDTCts.HeaderText = "INDTCts"
        Me.INDTCts.Name = "INDTCts"
        Me.INDTCts.ReadOnly = True
        '
        'NYCts
        '
        Me.NYCts.HeaderText = "NYCts"
        Me.NYCts.Name = "NYCts"
        Me.NYCts.ReadOnly = True
        '
        'NYMCts
        '
        Me.NYMCts.HeaderText = "NYMCts"
        Me.NYMCts.Name = "NYMCts"
        Me.NYMCts.ReadOnly = True
        '
        'NYTCts
        '
        Me.NYTCts.HeaderText = "NYTCts"
        Me.NYTCts.Name = "NYTCts"
        Me.NYTCts.ReadOnly = True
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmdLoad)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdOpen)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdReport)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.txtFilePath)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(806, 50)
        Me.HazelDev_Panel1.TabIndex = 32
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
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
        Me.cmdLoad.Location = New System.Drawing.Point(319, 13)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.Size = New System.Drawing.Size(99, 28)
        Me.cmdLoad.TabIndex = 48
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
        Me.cmdOpen.Location = New System.Drawing.Point(214, 13)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(99, 28)
        Me.cmdOpen.TabIndex = 47
        Me.cmdOpen.Text = "Open File"
        Me.cmdOpen.UseVisualStyleBackColor = False
        '
        'cmdReport
        '
        Me.cmdReport.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdReport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdReport.FlatAppearance.BorderSize = 0
        Me.cmdReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdReport.ForeColor = System.Drawing.Color.White
        Me.cmdReport.Location = New System.Drawing.Point(529, 13)
        Me.cmdReport.Name = "cmdReport"
        Me.cmdReport.Size = New System.Drawing.Size(99, 28)
        Me.cmdReport.TabIndex = 46
        Me.cmdReport.Text = "Report"
        Me.cmdReport.UseVisualStyleBackColor = False
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
        Me.cmdSave.Location = New System.Drawing.Point(424, 13)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(99, 28)
        Me.cmdSave.TabIndex = 45
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'txtFilePath
        '
        Me.txtFilePath.Enabled = False
        Me.txtFilePath.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilePath.Location = New System.Drawing.Point(696, 13)
        Me.txtFilePath.MaxLength = 0
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.Size = New System.Drawing.Size(18, 21)
        Me.txtFilePath.TabIndex = 43
        Me.txtFilePath.Visible = False
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
        'frm_NiruStockUpload
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(821, 515)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_NiruStockUpload"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "NIRU Stock Upload"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        CType(Me.flxDept, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdLoad As DiaStock.HazelDev_Button
    Friend WithEvents cmdOpen As DiaStock.HazelDev_Button
    Friend WithEvents cmdReport As DiaStock.HazelDev_Button
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents flxDept As System.Windows.Forms.DataGridView
    Friend WithEvents Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DepartmentName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DCLCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DCLMCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DCLTCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HKCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HKMCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HKTCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ILCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ILMCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ILTCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents INDCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents INDMCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents INDTCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NYCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NYMCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NYTCts As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
