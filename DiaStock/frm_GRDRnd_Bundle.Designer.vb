<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_GRDRnd_Bundle
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_GRDRnd_Bundle))
        Me.pnlTitle = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblBundleNo = New System.Windows.Forms.Label()
        Me.txtBundleNo = New System.Windows.Forms.TextBox()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.pnlEntry = New System.Windows.Forms.Panel()
        Me.lblPackNo = New System.Windows.Forms.Label()
        Me.txtPackNo = New System.Windows.Forms.TextBox()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.lblBNo = New System.Windows.Forms.Label()
        Me.txtBNo = New System.Windows.Forms.TextBox()
        Me.cmdClear = New System.Windows.Forms.Button()
        Me.pnlTitle.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.pnlEntry.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTitle
        '
        Me.pnlTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.pnlTitle.Controls.Add(Me.lblTitle)
        Me.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTitle.Location = New System.Drawing.Point(0, 0)
        Me.pnlTitle.Name = "pnlTitle"
        Me.pnlTitle.Size = New System.Drawing.Size(509, 35)
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
        Me.lblTitle.Size = New System.Drawing.Size(509, 35)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "BUNDLE MODULE"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.pnlTop.Controls.Add(Me.lblBundleNo)
        Me.pnlTop.Controls.Add(Me.txtBundleNo)
        Me.pnlTop.Controls.Add(Me.cmdRefresh)
        Me.pnlTop.Controls.Add(Me.cmdSave)
        Me.pnlTop.Controls.Add(Me.btnExit)
        Me.pnlTop.Location = New System.Drawing.Point(0, 35)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(506, 52)
        Me.pnlTop.TabIndex = 1
        '
        'lblBundleNo
        '
        Me.lblBundleNo.BackColor = System.Drawing.Color.Transparent
        Me.lblBundleNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblBundleNo.Location = New System.Drawing.Point(10, 16)
        Me.lblBundleNo.Name = "lblBundleNo"
        Me.lblBundleNo.Size = New System.Drawing.Size(80, 18)
        Me.lblBundleNo.TabIndex = 0
        Me.lblBundleNo.Text = "Bundle No."
        '
        'txtBundleNo
        '
        Me.txtBundleNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtBundleNo.Location = New System.Drawing.Point(96, 16)
        Me.txtBundleNo.Name = "txtBundleNo"
        Me.txtBundleNo.ReadOnly = True
        Me.txtBundleNo.Size = New System.Drawing.Size(70, 20)
        Me.txtBundleNo.TabIndex = 1
        Me.txtBundleNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmdRefresh
        '
        Me.cmdRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdRefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmdRefresh.Location = New System.Drawing.Point(205, 10)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(85, 29)
        Me.cmdRefresh.TabIndex = 2
        Me.cmdRefresh.Text = "Refresh"
        Me.cmdRefresh.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmdSave.Location = New System.Drawing.Point(311, 10)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(82, 29)
        Me.cmdSave.TabIndex = 3
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnExit.Location = New System.Drawing.Point(410, 11)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(84, 28)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'pnlEntry
        '
        Me.pnlEntry.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.pnlEntry.Controls.Add(Me.lblPackNo)
        Me.pnlEntry.Controls.Add(Me.txtPackNo)
        Me.pnlEntry.Controls.Add(Me.cmdAdd)
        Me.pnlEntry.Location = New System.Drawing.Point(0, 93)
        Me.pnlEntry.Name = "pnlEntry"
        Me.pnlEntry.Size = New System.Drawing.Size(506, 35)
        Me.pnlEntry.TabIndex = 2
        '
        'lblPackNo
        '
        Me.lblPackNo.BackColor = System.Drawing.Color.Transparent
        Me.lblPackNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblPackNo.Location = New System.Drawing.Point(8, 7)
        Me.lblPackNo.Name = "lblPackNo"
        Me.lblPackNo.Size = New System.Drawing.Size(120, 18)
        Me.lblPackNo.TabIndex = 0
        Me.lblPackNo.Text = "Packing List No."
        '
        'txtPackNo
        '
        Me.txtPackNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPackNo.Location = New System.Drawing.Point(112, 4)
        Me.txtPackNo.Name = "txtPackNo"
        Me.txtPackNo.Size = New System.Drawing.Size(70, 20)
        Me.txtPackNo.TabIndex = 1
        Me.txtPackNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmdAdd.Location = New System.Drawing.Point(240, 4)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(70, 28)
        Me.cmdAdd.TabIndex = 2
        Me.cmdAdd.Text = "Add"
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'flxDetails
        '
        Me.flxDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flxDetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.flxDetails.Location = New System.Drawing.Point(5, 134)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.Size = New System.Drawing.Size(499, 359)
        Me.flxDetails.TabIndex = 3
        '
        'pnlBottom
        '
        Me.pnlBottom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.pnlBottom.Controls.Add(Me.lblBNo)
        Me.pnlBottom.Controls.Add(Me.txtBNo)
        Me.pnlBottom.Controls.Add(Me.cmdClear)
        Me.pnlBottom.Location = New System.Drawing.Point(5, 499)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(499, 48)
        Me.pnlBottom.TabIndex = 4
        '
        'lblBNo
        '
        Me.lblBNo.BackColor = System.Drawing.Color.Transparent
        Me.lblBNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblBNo.Location = New System.Drawing.Point(9, 17)
        Me.lblBNo.Name = "lblBNo"
        Me.lblBNo.Size = New System.Drawing.Size(140, 18)
        Me.lblBNo.TabIndex = 0
        Me.lblBNo.Text = "Bundle No. to Clear:"
        '
        'txtBNo
        '
        Me.txtBNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtBNo.Location = New System.Drawing.Point(156, 15)
        Me.txtBNo.Name = "txtBNo"
        Me.txtBNo.Size = New System.Drawing.Size(60, 20)
        Me.txtBNo.TabIndex = 1
        Me.txtBNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmdClear
        '
        Me.cmdClear.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmdClear.Location = New System.Drawing.Point(222, 8)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(100, 30)
        Me.cmdClear.TabIndex = 2
        Me.cmdClear.Text = "Clear Bundle"
        Me.cmdClear.UseVisualStyleBackColor = False
        '
        'frm_GRDRnd_Bundle
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(509, 549)
        Me.Controls.Add(Me.pnlTitle)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlEntry)
        Me.Controls.Add(Me.flxDetails)
        Me.Controls.Add(Me.pnlBottom)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_GRDRnd_Bundle"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BUNDLE MODULE"
        Me.pnlTitle.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.pnlEntry.ResumeLayout(False)
        Me.pnlEntry.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlTitle As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents lblBundleNo As System.Windows.Forms.Label
    Friend WithEvents txtBundleNo As System.Windows.Forms.TextBox
    Friend WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents pnlEntry As System.Windows.Forms.Panel
    Friend WithEvents lblPackNo As System.Windows.Forms.Label
    Friend WithEvents txtPackNo As System.Windows.Forms.TextBox
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents lblBNo As System.Windows.Forms.Label
    Friend WithEvents txtBNo As System.Windows.Forms.TextBox
    Friend WithEvents cmdClear As System.Windows.Forms.Button

End Class