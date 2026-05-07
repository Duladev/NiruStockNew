<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_GRDRnd_PAY_Excel_Upload
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.cmdLoad = New System.Windows.Forms.Button()
        Me.cmdNew = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.grpMain = New System.Windows.Forms.GroupBox()
        Me.lblCompany = New System.Windows.Forms.Label()
        Me.cmbHotel = New System.Windows.Forms.ComboBox()
        Me.lblSupplier = New System.Windows.Forms.Label()
        Me.cmbSupplier = New System.Windows.Forms.ComboBox()
        Me.lblSourceLoc = New System.Windows.Forms.Label()
        Me.txtBackupLocation = New System.Windows.Forms.TextBox()
        Me.cmdSelect = New System.Windows.Forms.Button()
        Me.lblInvDate = New System.Windows.Forms.Label()
        Me.dtpInvDate = New System.Windows.Forms.DateTimePicker()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.txtCount = New System.Windows.Forms.TextBox()
        Me.lblInvoice = New System.Windows.Forms.Label()
        Me.txtInvoice = New System.Windows.Forms.TextBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.flxOT = New System.Windows.Forms.DataGridView()
        Me.txtError = New System.Windows.Forms.TextBox()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        Me.pnlButtons.SuspendLayout()
        Me.grpMain.SuspendLayout()
        CType(Me.flxOT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.pnlButtons)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.grpMain)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(705, 625)
        Me.HazelDev_ThemeContainer1.TabIndex = 0
        Me.HazelDev_ThemeContainer1.Text = "Import Upload"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'pnlButtons
        '
        Me.pnlButtons.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlButtons.Controls.Add(Me.cmdLoad)
        Me.pnlButtons.Controls.Add(Me.cmdNew)
        Me.pnlButtons.Controls.Add(Me.cmdSave)
        Me.pnlButtons.Controls.Add(Me.cmdClose)
        Me.pnlButtons.Location = New System.Drawing.Point(0, 50)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(714, 38)
        Me.pnlButtons.TabIndex = 2
        '
        'cmdLoad
        '
        Me.cmdLoad.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdLoad.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.cmdLoad.ForeColor = System.Drawing.Color.White
        Me.cmdLoad.Location = New System.Drawing.Point(5, 5)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.Size = New System.Drawing.Size(80, 26)
        Me.cmdLoad.TabIndex = 0
        Me.cmdLoad.Text = "Load File"
        Me.cmdLoad.UseVisualStyleBackColor = False
        '
        'cmdNew
        '
        Me.cmdNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdNew.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.cmdNew.ForeColor = System.Drawing.Color.White
        Me.cmdNew.Location = New System.Drawing.Point(430, 5)
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.Size = New System.Drawing.Size(80, 26)
        Me.cmdNew.TabIndex = 1
        Me.cmdNew.Text = "New"
        Me.cmdNew.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSave.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.cmdSave.ForeColor = System.Drawing.Color.White
        Me.cmdSave.Location = New System.Drawing.Point(515, 5)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(80, 26)
        Me.cmdSave.TabIndex = 2
        Me.cmdSave.Text = "Upload"
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdClose.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.cmdClose.ForeColor = System.Drawing.Color.White
        Me.cmdClose.Location = New System.Drawing.Point(600, 5)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(80, 26)
        Me.cmdClose.TabIndex = 3
        Me.cmdClose.Text = "Exit"
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'grpMain
        '
        Me.grpMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.grpMain.Controls.Add(Me.lblCompany)
        Me.grpMain.Controls.Add(Me.cmbHotel)
        Me.grpMain.Controls.Add(Me.lblSupplier)
        Me.grpMain.Controls.Add(Me.cmbSupplier)
        Me.grpMain.Controls.Add(Me.lblSourceLoc)
        Me.grpMain.Controls.Add(Me.txtBackupLocation)
        Me.grpMain.Controls.Add(Me.cmdSelect)
        Me.grpMain.Controls.Add(Me.lblInvDate)
        Me.grpMain.Controls.Add(Me.dtpInvDate)
        Me.grpMain.Controls.Add(Me.lblCount)
        Me.grpMain.Controls.Add(Me.txtCount)
        Me.grpMain.Controls.Add(Me.lblInvoice)
        Me.grpMain.Controls.Add(Me.txtInvoice)
        Me.grpMain.Controls.Add(Me.ProgressBar1)
        Me.grpMain.Controls.Add(Me.flxOT)
        Me.grpMain.Controls.Add(Me.txtError)
        Me.grpMain.Location = New System.Drawing.Point(0, 92)
        Me.grpMain.Name = "grpMain"
        Me.grpMain.Size = New System.Drawing.Size(705, 535)
        Me.grpMain.TabIndex = 3
        Me.grpMain.TabStop = False
        '
        'lblCompany
        '
        Me.lblCompany.BackColor = System.Drawing.Color.Transparent
        Me.lblCompany.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblCompany.Location = New System.Drawing.Point(8, 15)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(70, 18)
        Me.lblCompany.TabIndex = 0
        Me.lblCompany.Text = "Company"
        '
        'cmbHotel
        '
        Me.cmbHotel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbHotel.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.cmbHotel.Location = New System.Drawing.Point(8, 33)
        Me.cmbHotel.Name = "cmbHotel"
        Me.cmbHotel.Size = New System.Drawing.Size(280, 24)
        Me.cmbHotel.TabIndex = 1
        '
        'lblSupplier
        '
        Me.lblSupplier.BackColor = System.Drawing.Color.Transparent
        Me.lblSupplier.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblSupplier.Location = New System.Drawing.Point(360, 15)
        Me.lblSupplier.Name = "lblSupplier"
        Me.lblSupplier.Size = New System.Drawing.Size(60, 18)
        Me.lblSupplier.TabIndex = 2
        Me.lblSupplier.Text = "Supplier"
        '
        'cmbSupplier
        '
        Me.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSupplier.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.cmbSupplier.Location = New System.Drawing.Point(360, 33)
        Me.cmbSupplier.Name = "cmbSupplier"
        Me.cmbSupplier.Size = New System.Drawing.Size(325, 24)
        Me.cmbSupplier.TabIndex = 3
        '
        'lblSourceLoc
        '
        Me.lblSourceLoc.BackColor = System.Drawing.Color.Transparent
        Me.lblSourceLoc.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblSourceLoc.Location = New System.Drawing.Point(8, 63)
        Me.lblSourceLoc.Name = "lblSourceLoc"
        Me.lblSourceLoc.Size = New System.Drawing.Size(110, 18)
        Me.lblSourceLoc.TabIndex = 4
        Me.lblSourceLoc.Text = "Source Location"
        '
        'txtBackupLocation
        '
        Me.txtBackupLocation.BackColor = System.Drawing.Color.White
        Me.txtBackupLocation.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.txtBackupLocation.Location = New System.Drawing.Point(8, 81)
        Me.txtBackupLocation.Multiline = True
        Me.txtBackupLocation.Name = "txtBackupLocation"
        Me.txtBackupLocation.ReadOnly = True
        Me.txtBackupLocation.Size = New System.Drawing.Size(260, 42)
        Me.txtBackupLocation.TabIndex = 5
        '
        'cmdSelect
        '
        Me.cmdSelect.BackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.cmdSelect.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.cmdSelect.Location = New System.Drawing.Point(273, 81)
        Me.cmdSelect.Name = "cmdSelect"
        Me.cmdSelect.Size = New System.Drawing.Size(28, 22)
        Me.cmdSelect.TabIndex = 6
        Me.cmdSelect.Text = "..."
        Me.cmdSelect.UseVisualStyleBackColor = False
        '
        'lblInvDate
        '
        Me.lblInvDate.BackColor = System.Drawing.Color.Transparent
        Me.lblInvDate.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblInvDate.Location = New System.Drawing.Point(360, 63)
        Me.lblInvDate.Name = "lblInvDate"
        Me.lblInvDate.Size = New System.Drawing.Size(90, 18)
        Me.lblInvDate.TabIndex = 7
        Me.lblInvDate.Text = "Invoice Date"
        '
        'dtpInvDate
        '
        Me.dtpInvDate.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.dtpInvDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpInvDate.Location = New System.Drawing.Point(360, 81)
        Me.dtpInvDate.Name = "dtpInvDate"
        Me.dtpInvDate.Size = New System.Drawing.Size(140, 20)
        Me.dtpInvDate.TabIndex = 8
        '
        'lblCount
        '
        Me.lblCount.BackColor = System.Drawing.Color.Transparent
        Me.lblCount.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblCount.Location = New System.Drawing.Point(8, 132)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.Size = New System.Drawing.Size(50, 18)
        Me.lblCount.TabIndex = 9
        Me.lblCount.Text = "Count"
        '
        'txtCount
        '
        Me.txtCount.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtCount.Location = New System.Drawing.Point(8, 150)
        Me.txtCount.Name = "txtCount"
        Me.txtCount.ReadOnly = True
        Me.txtCount.Size = New System.Drawing.Size(65, 20)
        Me.txtCount.TabIndex = 10
        Me.txtCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblInvoice
        '
        Me.lblInvoice.BackColor = System.Drawing.Color.Transparent
        Me.lblInvoice.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblInvoice.Location = New System.Drawing.Point(85, 132)
        Me.lblInvoice.Name = "lblInvoice"
        Me.lblInvoice.Size = New System.Drawing.Size(80, 18)
        Me.lblInvoice.TabIndex = 11
        Me.lblInvoice.Text = "Invoice No."
        '
        'txtInvoice
        '
        Me.txtInvoice.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtInvoice.Location = New System.Drawing.Point(85, 150)
        Me.txtInvoice.Name = "txtInvoice"
        Me.txtInvoice.ReadOnly = True
        Me.txtInvoice.Size = New System.Drawing.Size(150, 20)
        Me.txtInvoice.TabIndex = 12
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(8, 180)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(682, 14)
        Me.ProgressBar1.TabIndex = 13
        '
        'flxOT
        '
        Me.flxOT.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flxOT.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.flxOT.Location = New System.Drawing.Point(8, 200)
        Me.flxOT.Name = "flxOT"
        Me.flxOT.Size = New System.Drawing.Size(687, 290)
        Me.flxOT.TabIndex = 14
        '
        'txtError
        '
        Me.txtError.BackColor = System.Drawing.Color.LightYellow
        Me.txtError.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.txtError.ForeColor = System.Drawing.Color.Red
        Me.txtError.Location = New System.Drawing.Point(8, 498)
        Me.txtError.Multiline = True
        Me.txtError.Name = "txtError"
        Me.txtError.ReadOnly = True
        Me.txtError.Size = New System.Drawing.Size(450, 28)
        Me.txtError.TabIndex = 15
        '
        'frm_GRDRnd_PAY_Excel_Upload
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(705, 625)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_GRDRnd_PAY_Excel_Upload"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "I M P O R T   U P L O A D"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.pnlButtons.ResumeLayout(False)
        Me.grpMain.ResumeLayout(False)
        Me.grpMain.PerformLayout()
        CType(Me.flxOT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents HazelDev_ThemeContainer1 As HazelDev_ThemeContainer
    Friend WithEvents pnlButtons As Panel
    Friend WithEvents cmdLoad As Button
    Friend WithEvents cmdNew As Button
    Friend WithEvents cmdSave As Button
    Friend WithEvents cmdClose As Button
    Friend WithEvents grpMain As GroupBox
    Friend WithEvents lblCompany As Label
    Friend WithEvents cmbHotel As ComboBox
    Friend WithEvents lblSupplier As Label
    Friend WithEvents cmbSupplier As ComboBox
    Friend WithEvents lblSourceLoc As Label
    Friend WithEvents txtBackupLocation As TextBox
    Friend WithEvents cmdSelect As Button
    Friend WithEvents lblInvDate As Label
    Friend WithEvents dtpInvDate As DateTimePicker
    Friend WithEvents lblCount As Label
    Friend WithEvents txtCount As TextBox
    Friend WithEvents lblInvoice As Label
    Friend WithEvents txtInvoice As TextBox
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents flxOT As DataGridView
    Friend WithEvents txtError As TextBox
End Class