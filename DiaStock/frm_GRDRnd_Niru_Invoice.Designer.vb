<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_GRDRnd_Niru_Invoice
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_GRDRnd_Niru_Invoice))
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.grpInvoices = New System.Windows.Forms.GroupBox()
        Me.flxInvoice = New System.Windows.Forms.DataGridView()
        Me.lblInvoiceNo = New System.Windows.Forms.Label()
        Me.cmbInvoice = New System.Windows.Forms.ComboBox()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdNew = New System.Windows.Forms.Button()
        Me.lblCompany = New System.Windows.Forms.Label()
        Me.cmbHotel = New System.Windows.Forms.ComboBox()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        Me.grpInvoices.SuspendLayout()
        CType(Me.flxInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.grpInvoices)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.pnlTop)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(949, 527)
        Me.HazelDev_ThemeContainer1.TabIndex = 0
        Me.HazelDev_ThemeContainer1.Text = "Invoices"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'grpInvoices
        '
        Me.grpInvoices.BackColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.grpInvoices.Controls.Add(Me.flxInvoice)
        Me.grpInvoices.Controls.Add(Me.lblInvoiceNo)
        Me.grpInvoices.Controls.Add(Me.cmbInvoice)
        Me.grpInvoices.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.grpInvoices.Location = New System.Drawing.Point(0, 107)
        Me.grpInvoices.Name = "grpInvoices"
        Me.grpInvoices.Size = New System.Drawing.Size(947, 417)
        Me.grpInvoices.TabIndex = 3
        Me.grpInvoices.TabStop = False
        Me.grpInvoices.Text = "Invoices"
        '
        'flxInvoice
        '
        Me.flxInvoice.AllowUserToAddRows = False
        Me.flxInvoice.AllowUserToDeleteRows = False
        Me.flxInvoice.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flxInvoice.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(230, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.flxInvoice.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.flxInvoice.ColumnHeadersHeight = 24
        Me.flxInvoice.EnableHeadersVisualStyles = False
        Me.flxInvoice.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.flxInvoice.Location = New System.Drawing.Point(8, 68)
        Me.flxInvoice.Name = "flxInvoice"
        Me.flxInvoice.RowTemplate.Height = 20
        Me.flxInvoice.Size = New System.Drawing.Size(931, 339)
        Me.flxInvoice.TabIndex = 2
        '
        'lblInvoiceNo
        '
        Me.lblInvoiceNo.BackColor = System.Drawing.Color.Transparent
        Me.lblInvoiceNo.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblInvoiceNo.Location = New System.Drawing.Point(8, 18)
        Me.lblInvoiceNo.Name = "lblInvoiceNo"
        Me.lblInvoiceNo.Size = New System.Drawing.Size(80, 18)
        Me.lblInvoiceNo.TabIndex = 0
        Me.lblInvoiceNo.Text = "Invoice No."
        '
        'cmbInvoice
        '
        Me.cmbInvoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbInvoice.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.cmbInvoice.Location = New System.Drawing.Point(8, 38)
        Me.cmbInvoice.Name = "cmbInvoice"
        Me.cmbInvoice.Size = New System.Drawing.Size(160, 24)
        Me.cmbInvoice.TabIndex = 1
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlTop.Controls.Add(Me.cmdExit)
        Me.pnlTop.Controls.Add(Me.cmdSave)
        Me.pnlTop.Controls.Add(Me.cmdNew)
        Me.pnlTop.Controls.Add(Me.lblCompany)
        Me.pnlTop.Controls.Add(Me.cmbHotel)
        Me.pnlTop.Location = New System.Drawing.Point(0, 51)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(947, 50)
        Me.pnlTop.TabIndex = 2
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdExit.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.cmdExit.ForeColor = System.Drawing.Color.White
        Me.cmdExit.Location = New System.Drawing.Point(8, 10)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(85, 37)
        Me.cmdExit.TabIndex = 0
        Me.cmdExit.Text = "&Exit"
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSave.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.cmdSave.ForeColor = System.Drawing.Color.White
        Me.cmdSave.Location = New System.Drawing.Point(100, 10)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(86, 37)
        Me.cmdSave.TabIndex = 1
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'cmdNew
        '
        Me.cmdNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdNew.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.cmdNew.ForeColor = System.Drawing.Color.White
        Me.cmdNew.Location = New System.Drawing.Point(192, 10)
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.Size = New System.Drawing.Size(87, 37)
        Me.cmdNew.TabIndex = 2
        Me.cmdNew.Text = "&New"
        Me.cmdNew.UseVisualStyleBackColor = False
        '
        'lblCompany
        '
        Me.lblCompany.BackColor = System.Drawing.Color.Transparent
        Me.lblCompany.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.lblCompany.Location = New System.Drawing.Point(770, 5)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(70, 18)
        Me.lblCompany.TabIndex = 3
        Me.lblCompany.Text = "Company"
        '
        'cmbHotel
        '
        Me.cmbHotel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbHotel.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.cmbHotel.Location = New System.Drawing.Point(770, 23)
        Me.cmbHotel.Name = "cmbHotel"
        Me.cmbHotel.Size = New System.Drawing.Size(170, 24)
        Me.cmbHotel.TabIndex = 4
        '
        'frm_GRDRnd_Niru_Invoice
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(949, 527)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_GRDRnd_Niru_Invoice"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "I N V O I C E S"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.grpInvoices.ResumeLayout(False)
        CType(Me.flxInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents HazelDev_ThemeContainer1 As HazelDev_ThemeContainer
    Friend WithEvents grpInvoices As GroupBox
    Friend WithEvents lblInvoiceNo As Label
    Friend WithEvents cmbInvoice As ComboBox
    Friend WithEvents flxInvoice As DataGridView
    Friend WithEvents pnlTop As Panel
    Friend WithEvents cmdExit As Button
    Friend WithEvents cmdSave As Button
    Friend WithEvents cmdNew As Button
    Friend WithEvents lblCompany As Label
    Friend WithEvents cmbHotel As ComboBox
End Class