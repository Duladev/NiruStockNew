<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_RprUpload
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.pnlDetails2 = New System.Windows.Forms.Panel()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DepartmentName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Company = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OutTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InvCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParSize = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Width1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pcs1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cut1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdLoad = New DiaStock.HazelDev_Button()
        Me.cmdOpen = New DiaStock.HazelDev_Button()
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.cmdExcel = New DiaStock.HazelDev_Button()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        Me.pnlDetails2.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.pnlDetails2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(1279, 684)
        Me.HazelDev_ThemeContainer1.TabIndex = 6
        Me.HazelDev_ThemeContainer1.Text = "ROUGH PROCESS UPLOAD"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'pnlDetails2
        '
        Me.pnlDetails2.BackColor = System.Drawing.Color.White
        Me.pnlDetails2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDetails2.Controls.Add(Me.flxDetails)
        Me.pnlDetails2.Location = New System.Drawing.Point(3, 110)
        Me.pnlDetails2.Name = "pnlDetails2"
        Me.pnlDetails2.Size = New System.Drawing.Size(1268, 568)
        Me.pnlDetails2.TabIndex = 68
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Code, Me.DepartmentName, Me.Company, Me.InTime, Me.OutTime, Me.InvCts, Me.ParSize, Me.Width1, Me.Pcs1, Me.Cut1, Me.Column3})
        Me.flxDetails.Location = New System.Drawing.Point(8, 12)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(1252, 546)
        Me.flxDetails.TabIndex = 45
        '
        'Column1
        '
        Me.Column1.HeaderText = "Par No"
        Me.Column1.Name = "Column1"
        '
        'Column2
        '
        Me.Column2.HeaderText = "Pkt No"
        Me.Column2.Name = "Column2"
        '
        'Code
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Code.DefaultCellStyle = DataGridViewCellStyle1
        Me.Code.HeaderText = "Rgh Cts"
        Me.Code.Name = "Code"
        '
        'DepartmentName
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DepartmentName.DefaultCellStyle = DataGridViewCellStyle2
        Me.DepartmentName.HeaderText = "Fin Cts"
        Me.DepartmentName.Name = "DepartmentName"
        Me.DepartmentName.Width = 80
        '
        'Company
        '
        Me.Company.HeaderText = "Shape"
        Me.Company.Name = "Company"
        '
        'InTime
        '
        DataGridViewCellStyle3.NullValue = Nothing
        Me.InTime.DefaultCellStyle = DataGridViewCellStyle3
        Me.InTime.HeaderText = "Color"
        Me.InTime.Name = "InTime"
        '
        'OutTime
        '
        DataGridViewCellStyle4.NullValue = Nothing
        Me.OutTime.DefaultCellStyle = DataGridViewCellStyle4
        Me.OutTime.HeaderText = "Clarity"
        Me.OutTime.Name = "OutTime"
        '
        'InvCts
        '
        Me.InvCts.HeaderText = "Plan Value"
        Me.InvCts.Name = "InvCts"
        '
        'ParSize
        '
        Me.ParSize.HeaderText = "Size"
        Me.ParSize.Name = "ParSize"
        '
        'Width1
        '
        Me.Width1.HeaderText = "Width"
        Me.Width1.Name = "Width1"
        '
        'Pcs1
        '
        Me.Pcs1.HeaderText = "Pcs"
        Me.Pcs1.Name = "Pcs1"
        Me.Pcs1.Width = 80
        '
        'Cut1
        '
        Me.Cut1.HeaderText = "Cut"
        Me.Cut1.Name = "Cut1"
        '
        'Column3
        '
        Me.Column3.HeaderText = "Saved"
        Me.Column3.Name = "Column3"
        Me.Column3.Width = 70
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmdLoad)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdOpen)
        Me.HazelDev_Panel1.Controls.Add(Me.txtFilePath)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExcel)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(1268, 50)
        Me.HazelDev_Panel1.TabIndex = 31
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
        Me.cmdLoad.TabIndex = 51
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
        Me.cmdOpen.TabIndex = 50
        Me.cmdOpen.Text = "Open File"
        Me.cmdOpen.UseVisualStyleBackColor = False
        '
        'txtFilePath
        '
        Me.txtFilePath.Enabled = False
        Me.txtFilePath.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilePath.Location = New System.Drawing.Point(813, 14)
        Me.txtFilePath.MaxLength = 0
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.Size = New System.Drawing.Size(18, 21)
        Me.txtFilePath.TabIndex = 49
        Me.txtFilePath.Visible = False
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
        Me.cmdExcel.Location = New System.Drawing.Point(1088, 13)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(99, 28)
        Me.cmdExcel.TabIndex = 46
        Me.cmdExcel.Text = "Excel"
        Me.cmdExcel.UseVisualStyleBackColor = False
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
        'frm_RprUpload
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1279, 684)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_RprUpload"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Rpr Upload"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.pnlDetails2.ResumeLayout(False)
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents pnlDetails2 As System.Windows.Forms.Panel
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cmdLoad As DiaStock.HazelDev_Button
    Friend WithEvents cmdOpen As DiaStock.HazelDev_Button
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DepartmentName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Company As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OutTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InvCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParSize As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Width1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pcs1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cut1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
