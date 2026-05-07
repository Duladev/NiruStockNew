<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_GRDRnd_PacketUpload
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_GRDRnd_PacketUpload))
        Me.pnlTitle = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblParNo = New System.Windows.Forms.Label()
        Me.txtParNo = New System.Windows.Forms.TextBox()
        Me.lblPktNo = New System.Windows.Forms.Label()
        Me.txtPktNo = New System.Windows.Forms.TextBox()
        Me.lblFilePath = New System.Windows.Forms.Label()
        Me.txtBackupLocation = New System.Windows.Forms.TextBox()
        Me.cmdSelect = New System.Windows.Forms.Button()
        Me.cmdLoad = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.pnlTotals = New System.Windows.Forms.Panel()
        Me.lblPktPcs = New System.Windows.Forms.Label()
        Me.txtPktPcs = New System.Windows.Forms.TextBox()
        Me.lblPktCts = New System.Windows.Forms.Label()
        Me.txtPktCts = New System.Windows.Forms.TextBox()
        Me.lblTotPcs = New System.Windows.Forms.Label()
        Me.txtPcs = New System.Windows.Forms.TextBox()
        Me.lblTotCts = New System.Windows.Forms.Label()
        Me.txtCts = New System.Windows.Forms.TextBox()
        Me.chkSize = New System.Windows.Forms.CheckBox()
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
        Me.pnlTitle.Size = New System.Drawing.Size(780, 35)
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
        Me.lblTitle.Size = New System.Drawing.Size(780, 35)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "PACKET UPLOAD"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.pnlTop.Controls.Add(Me.lblParNo)
        Me.pnlTop.Controls.Add(Me.txtParNo)
        Me.pnlTop.Controls.Add(Me.lblPktNo)
        Me.pnlTop.Controls.Add(Me.txtPktNo)
        Me.pnlTop.Controls.Add(Me.lblFilePath)
        Me.pnlTop.Controls.Add(Me.txtBackupLocation)
        Me.pnlTop.Controls.Add(Me.cmdSelect)
        Me.pnlTop.Controls.Add(Me.cmdLoad)
        Me.pnlTop.Controls.Add(Me.cmdSave)
        Me.pnlTop.Controls.Add(Me.btnExit)
        Me.pnlTop.Location = New System.Drawing.Point(0, 35)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(777, 80)
        Me.pnlTop.TabIndex = 1
        '
        'lblParNo
        '
        Me.lblParNo.BackColor = System.Drawing.Color.Transparent
        Me.lblParNo.Font = New System.Drawing.Font("Trebuchet MS", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblParNo.ForeColor = System.Drawing.Color.Black
        Me.lblParNo.Location = New System.Drawing.Point(8, 5)
        Me.lblParNo.Name = "lblParNo"
        Me.lblParNo.Size = New System.Drawing.Size(70, 18)
        Me.lblParNo.TabIndex = 0
        Me.lblParNo.Text = "Parcel No"
        '
        'txtParNo
        '
        Me.txtParNo.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.txtParNo.Location = New System.Drawing.Point(8, 23)
        Me.txtParNo.Name = "txtParNo"
        Me.txtParNo.Size = New System.Drawing.Size(120, 20)
        Me.txtParNo.TabIndex = 1
        '
        'lblPktNo
        '
        Me.lblPktNo.BackColor = System.Drawing.Color.Transparent
        Me.lblPktNo.Font = New System.Drawing.Font("Trebuchet MS", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPktNo.ForeColor = System.Drawing.Color.Black
        Me.lblPktNo.Location = New System.Drawing.Point(140, 5)
        Me.lblPktNo.Name = "lblPktNo"
        Me.lblPktNo.Size = New System.Drawing.Size(70, 18)
        Me.lblPktNo.TabIndex = 2
        Me.lblPktNo.Text = "Packet No"
        '
        'txtPktNo
        '
        Me.txtPktNo.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.txtPktNo.Location = New System.Drawing.Point(140, 23)
        Me.txtPktNo.Name = "txtPktNo"
        Me.txtPktNo.Size = New System.Drawing.Size(60, 20)
        Me.txtPktNo.TabIndex = 3
        '
        'lblFilePath
        '
        Me.lblFilePath.BackColor = System.Drawing.Color.Transparent
        Me.lblFilePath.Font = New System.Drawing.Font("Trebuchet MS", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilePath.ForeColor = System.Drawing.Color.Black
        Me.lblFilePath.Location = New System.Drawing.Point(8, 50)
        Me.lblFilePath.Name = "lblFilePath"
        Me.lblFilePath.Size = New System.Drawing.Size(70, 18)
        Me.lblFilePath.TabIndex = 4
        Me.lblFilePath.Text = "Source File"
        '
        'txtBackupLocation
        '
        Me.txtBackupLocation.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.txtBackupLocation.Location = New System.Drawing.Point(85, 48)
        Me.txtBackupLocation.Name = "txtBackupLocation"
        Me.txtBackupLocation.ReadOnly = True
        Me.txtBackupLocation.Size = New System.Drawing.Size(300, 20)
        Me.txtBackupLocation.TabIndex = 5
        '
        'cmdSelect
        '
        Me.cmdSelect.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSelect.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.cmdSelect.Location = New System.Drawing.Point(390, 47)
        Me.cmdSelect.Name = "cmdSelect"
        Me.cmdSelect.Size = New System.Drawing.Size(30, 24)
        Me.cmdSelect.TabIndex = 6
        Me.cmdSelect.Text = "..."
        Me.cmdSelect.UseVisualStyleBackColor = False
        '
        'cmdLoad
        '
        Me.cmdLoad.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdLoad.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.cmdLoad.Location = New System.Drawing.Point(449, 32)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.Size = New System.Drawing.Size(90, 36)
        Me.cmdLoad.TabIndex = 7
        Me.cmdLoad.Text = "Load File"
        Me.cmdLoad.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSave.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.cmdSave.Location = New System.Drawing.Point(561, 32)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(80, 36)
        Me.cmdSave.TabIndex = 8
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnExit.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.btnExit.Location = New System.Drawing.Point(669, 32)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(80, 36)
        Me.btnExit.TabIndex = 9
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'flxDetails
        '
        Me.flxDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flxDetails.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.flxDetails.Location = New System.Drawing.Point(5, 120)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.Size = New System.Drawing.Size(769, 457)
        Me.flxDetails.TabIndex = 2
        '
        'pnlTotals
        '
        Me.pnlTotals.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlTotals.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.pnlTotals.Controls.Add(Me.lblPktPcs)
        Me.pnlTotals.Controls.Add(Me.txtPktPcs)
        Me.pnlTotals.Controls.Add(Me.lblPktCts)
        Me.pnlTotals.Controls.Add(Me.txtPktCts)
        Me.pnlTotals.Controls.Add(Me.lblTotPcs)
        Me.pnlTotals.Controls.Add(Me.txtPcs)
        Me.pnlTotals.Controls.Add(Me.lblTotCts)
        Me.pnlTotals.Controls.Add(Me.txtCts)
        Me.pnlTotals.Controls.Add(Me.chkSize)
        Me.pnlTotals.Location = New System.Drawing.Point(4, 583)
        Me.pnlTotals.Name = "pnlTotals"
        Me.pnlTotals.Size = New System.Drawing.Size(770, 32)
        Me.pnlTotals.TabIndex = 3
        '
        'lblPktPcs
        '
        Me.lblPktPcs.BackColor = System.Drawing.Color.Transparent
        Me.lblPktPcs.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblPktPcs.Location = New System.Drawing.Point(5, 7)
        Me.lblPktPcs.Name = "lblPktPcs"
        Me.lblPktPcs.Size = New System.Drawing.Size(60, 18)
        Me.lblPktPcs.TabIndex = 0
        Me.lblPktPcs.Text = "Pkt Pcs:"
        '
        'txtPktPcs
        '
        Me.txtPktPcs.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtPktPcs.Location = New System.Drawing.Point(70, 5)
        Me.txtPktPcs.Name = "txtPktPcs"
        Me.txtPktPcs.ReadOnly = True
        Me.txtPktPcs.Size = New System.Drawing.Size(70, 20)
        Me.txtPktPcs.TabIndex = 1
        Me.txtPktPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPktCts
        '
        Me.lblPktCts.BackColor = System.Drawing.Color.Transparent
        Me.lblPktCts.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblPktCts.Location = New System.Drawing.Point(150, 7)
        Me.lblPktCts.Name = "lblPktCts"
        Me.lblPktCts.Size = New System.Drawing.Size(60, 18)
        Me.lblPktCts.TabIndex = 2
        Me.lblPktCts.Text = "Pkt Cts:"
        '
        'txtPktCts
        '
        Me.txtPktCts.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtPktCts.Location = New System.Drawing.Point(215, 5)
        Me.txtPktCts.Name = "txtPktCts"
        Me.txtPktCts.ReadOnly = True
        Me.txtPktCts.Size = New System.Drawing.Size(80, 20)
        Me.txtPktCts.TabIndex = 3
        Me.txtPktCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblTotPcs
        '
        Me.lblTotPcs.BackColor = System.Drawing.Color.Transparent
        Me.lblTotPcs.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTotPcs.Location = New System.Drawing.Point(380, 7)
        Me.lblTotPcs.Name = "lblTotPcs"
        Me.lblTotPcs.Size = New System.Drawing.Size(60, 18)
        Me.lblTotPcs.TabIndex = 4
        Me.lblTotPcs.Text = "Tot Pcs:"
        '
        'txtPcs
        '
        Me.txtPcs.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtPcs.Location = New System.Drawing.Point(445, 5)
        Me.txtPcs.Name = "txtPcs"
        Me.txtPcs.ReadOnly = True
        Me.txtPcs.Size = New System.Drawing.Size(70, 20)
        Me.txtPcs.TabIndex = 5
        Me.txtPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblTotCts
        '
        Me.lblTotCts.BackColor = System.Drawing.Color.Transparent
        Me.lblTotCts.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTotCts.Location = New System.Drawing.Point(525, 7)
        Me.lblTotCts.Name = "lblTotCts"
        Me.lblTotCts.Size = New System.Drawing.Size(60, 18)
        Me.lblTotCts.TabIndex = 6
        Me.lblTotCts.Text = "Tot Cts:"
        '
        'txtCts
        '
        Me.txtCts.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtCts.Location = New System.Drawing.Point(590, 5)
        Me.txtCts.Name = "txtCts"
        Me.txtCts.ReadOnly = True
        Me.txtCts.Size = New System.Drawing.Size(80, 20)
        Me.txtCts.TabIndex = 7
        Me.txtCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkSize
        '
        Me.chkSize.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.chkSize.Location = New System.Drawing.Point(685, 7)
        Me.chkSize.Name = "chkSize"
        Me.chkSize.Size = New System.Drawing.Size(80, 20)
        Me.chkSize.TabIndex = 8
        Me.chkSize.Text = "Size Only"
        '
        'frm_GRDRnd_PacketUpload
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(780, 615)
        Me.Controls.Add(Me.pnlTitle)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.flxDetails)
        Me.Controls.Add(Me.pnlTotals)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_GRDRnd_PacketUpload"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PACKET UPLOAD"
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
    Friend WithEvents lblParNo As System.Windows.Forms.Label
    Friend WithEvents txtParNo As System.Windows.Forms.TextBox
    Friend WithEvents lblPktNo As System.Windows.Forms.Label
    Friend WithEvents txtPktNo As System.Windows.Forms.TextBox
    Friend WithEvents lblFilePath As System.Windows.Forms.Label
    Friend WithEvents txtBackupLocation As System.Windows.Forms.TextBox
    Friend WithEvents cmdSelect As System.Windows.Forms.Button
    Friend WithEvents cmdLoad As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents pnlTotals As System.Windows.Forms.Panel
    Friend WithEvents lblPktPcs As System.Windows.Forms.Label
    Friend WithEvents txtPktPcs As System.Windows.Forms.TextBox
    Friend WithEvents lblPktCts As System.Windows.Forms.Label
    Friend WithEvents txtPktCts As System.Windows.Forms.TextBox
    Friend WithEvents lblTotPcs As System.Windows.Forms.Label
    Friend WithEvents txtPcs As System.Windows.Forms.TextBox
    Friend WithEvents lblTotCts As System.Windows.Forms.Label
    Friend WithEvents txtCts As System.Windows.Forms.TextBox
    Friend WithEvents chkSize As System.Windows.Forms.CheckBox

End Class