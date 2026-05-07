<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_GRDRnd_Upload
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_GRDRnd_Upload))
        Me.pnlTitle = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.txtBackupLocation = New System.Windows.Forms.TextBox()
        Me.cmdSelect = New System.Windows.Forms.Button()
        Me.cmdLoad = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.pnlTotals = New System.Windows.Forms.Panel()
        Me.lblPcs = New System.Windows.Forms.Label()
        Me.txtPcs = New System.Windows.Forms.TextBox()
        Me.lblCts = New System.Windows.Forms.Label()
        Me.txtCts = New System.Windows.Forms.TextBox()
        Me.pnlTitle.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTotals.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTitle
        '
        Me.pnlTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.pnlTitle.Controls.Add(Me.lblTitle)
        Me.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTitle.Location = New System.Drawing.Point(0, 0)
        Me.pnlTitle.Name = "pnlTitle"
        Me.pnlTitle.Size = New System.Drawing.Size(764, 35)
        Me.pnlTitle.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTitle.Font = New System.Drawing.Font("Trebuchet MS", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(764, 35)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "U P L O A D"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.pnlTop.Controls.Add(Me.txtBackupLocation)
        Me.pnlTop.Controls.Add(Me.cmdSelect)
        Me.pnlTop.Controls.Add(Me.cmdLoad)
        Me.pnlTop.Controls.Add(Me.cmdSave)
        Me.pnlTop.Controls.Add(Me.btnExit)
        Me.pnlTop.Location = New System.Drawing.Point(4, 38)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(757, 40)
        Me.pnlTop.TabIndex = 1
        '
        'txtBackupLocation
        '
        Me.txtBackupLocation.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.txtBackupLocation.Location = New System.Drawing.Point(5, 9)
        Me.txtBackupLocation.Name = "txtBackupLocation"
        Me.txtBackupLocation.ReadOnly = True
        Me.txtBackupLocation.Size = New System.Drawing.Size(310, 20)
        Me.txtBackupLocation.TabIndex = 0
        '
        'cmdSelect
        '
        Me.cmdSelect.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSelect.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.cmdSelect.Location = New System.Drawing.Point(320, 8)
        Me.cmdSelect.Name = "cmdSelect"
        Me.cmdSelect.Size = New System.Drawing.Size(28, 24)
        Me.cmdSelect.TabIndex = 1
        Me.cmdSelect.Text = "..."
        Me.cmdSelect.UseVisualStyleBackColor = False
        '
        'cmdLoad
        '
        Me.cmdLoad.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdLoad.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.cmdLoad.Location = New System.Drawing.Point(364, 5)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.Size = New System.Drawing.Size(89, 30)
        Me.cmdLoad.TabIndex = 2
        Me.cmdLoad.Text = "&Load File"
        Me.cmdLoad.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSave.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.cmdSave.Location = New System.Drawing.Point(470, 5)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(85, 30)
        Me.cmdSave.TabIndex = 3
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnExit.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.btnExit.Location = New System.Drawing.Point(579, 5)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(85, 30)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "&Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'flxDetails
        '
        Me.flxDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flxDetails.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.flxDetails.Location = New System.Drawing.Point(5, 81)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.Size = New System.Drawing.Size(747, 479)
        Me.flxDetails.TabIndex = 2
        '
        'pnlTotals
        '
        Me.pnlTotals.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlTotals.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.pnlTotals.Controls.Add(Me.lblPcs)
        Me.pnlTotals.Controls.Add(Me.txtPcs)
        Me.pnlTotals.Controls.Add(Me.lblCts)
        Me.pnlTotals.Controls.Add(Me.txtCts)
        Me.pnlTotals.Location = New System.Drawing.Point(4, 566)
        Me.pnlTotals.Name = "pnlTotals"
        Me.pnlTotals.Size = New System.Drawing.Size(748, 50)
        Me.pnlTotals.TabIndex = 3
        '
        'lblPcs
        '
        Me.lblPcs.BackColor = System.Drawing.Color.Transparent
        Me.lblPcs.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblPcs.Location = New System.Drawing.Point(410, 6)
        Me.lblPcs.Name = "lblPcs"
        Me.lblPcs.Size = New System.Drawing.Size(70, 18)
        Me.lblPcs.TabIndex = 0
        Me.lblPcs.Text = "Total Pcs:"
        '
        'txtPcs
        '
        Me.txtPcs.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtPcs.Location = New System.Drawing.Point(485, 4)
        Me.txtPcs.Name = "txtPcs"
        Me.txtPcs.ReadOnly = True
        Me.txtPcs.Size = New System.Drawing.Size(70, 20)
        Me.txtPcs.TabIndex = 1
        Me.txtPcs.Text = "0"
        Me.txtPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblCts
        '
        Me.lblCts.BackColor = System.Drawing.Color.Transparent
        Me.lblCts.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCts.Location = New System.Drawing.Point(565, 6)
        Me.lblCts.Name = "lblCts"
        Me.lblCts.Size = New System.Drawing.Size(70, 18)
        Me.lblCts.TabIndex = 2
        Me.lblCts.Text = "Total Cts:"
        '
        'txtCts
        '
        Me.txtCts.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtCts.Location = New System.Drawing.Point(640, 4)
        Me.txtCts.Name = "txtCts"
        Me.txtCts.ReadOnly = True
        Me.txtCts.Size = New System.Drawing.Size(90, 20)
        Me.txtCts.TabIndex = 3
        Me.txtCts.Text = "0"
        Me.txtCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'frm_GRDRnd_Upload
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(764, 628)
        Me.Controls.Add(Me.pnlTitle)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.flxDetails)
        Me.Controls.Add(Me.pnlTotals)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_GRDRnd_Upload"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "U P L O A D"
        Me.pnlTitle.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTotals.ResumeLayout(False)
        Me.pnlTotals.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlTitle As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents txtBackupLocation As System.Windows.Forms.TextBox
    Friend WithEvents cmdSelect As System.Windows.Forms.Button
    Friend WithEvents cmdLoad As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents pnlTotals As System.Windows.Forms.Panel
    Friend WithEvents lblPcs As System.Windows.Forms.Label
    Friend WithEvents txtPcs As System.Windows.Forms.TextBox
    Friend WithEvents lblCts As System.Windows.Forms.Label
    Friend WithEvents txtCts As System.Windows.Forms.TextBox

End Class