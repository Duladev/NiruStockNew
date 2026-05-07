<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_GRDRnd_Parcels
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_GRDRnd_Parcels))
        Me.pnlTitle = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlToolbar = New System.Windows.Forms.Panel()
        Me.btnexit1 = New System.Windows.Forms.Button()
        Me.btnsave1 = New System.Windows.Forms.Button()
        Me.btnrefresh1 = New System.Windows.Forms.Button()
        Me.txtRecordCount = New System.Windows.Forms.TextBox()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.btnDeselectAll = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.pnlTitle.SuspendLayout()
        Me.pnlToolbar.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlTitle
        '
        Me.pnlTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.pnlTitle.Controls.Add(Me.lblTitle)
        Me.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTitle.Location = New System.Drawing.Point(0, 0)
        Me.pnlTitle.Name = "pnlTitle"
        Me.pnlTitle.Size = New System.Drawing.Size(557, 35)
        Me.pnlTitle.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(557, 35)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Grading Parcels"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlToolbar
        '
        Me.pnlToolbar.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.pnlToolbar.Controls.Add(Me.btnexit1)
        Me.pnlToolbar.Controls.Add(Me.btnsave1)
        Me.pnlToolbar.Controls.Add(Me.btnrefresh1)
        Me.pnlToolbar.Controls.Add(Me.txtRecordCount)
        Me.pnlToolbar.Location = New System.Drawing.Point(0, 35)
        Me.pnlToolbar.Name = "pnlToolbar"
        Me.pnlToolbar.Size = New System.Drawing.Size(554, 40)
        Me.pnlToolbar.TabIndex = 1
        '
        'btnexit1
        '
        Me.btnexit1.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnexit1.Location = New System.Drawing.Point(250, 8)
        Me.btnexit1.Name = "btnexit1"
        Me.btnexit1.Size = New System.Drawing.Size(85, 29)
        Me.btnexit1.TabIndex = 3
        Me.btnexit1.Text = "Exit"
        Me.btnexit1.UseVisualStyleBackColor = False
        '
        'btnsave1
        '
        Me.btnsave1.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnsave1.Location = New System.Drawing.Point(129, 8)
        Me.btnsave1.Name = "btnsave1"
        Me.btnsave1.Size = New System.Drawing.Size(85, 29)
        Me.btnsave1.TabIndex = 2
        Me.btnsave1.Text = "Save"
        Me.btnsave1.UseVisualStyleBackColor = False
        '
        'btnrefresh1
        '
        Me.btnrefresh1.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnrefresh1.Location = New System.Drawing.Point(12, 8)
        Me.btnrefresh1.Name = "btnrefresh1"
        Me.btnrefresh1.Size = New System.Drawing.Size(85, 29)
        Me.btnrefresh1.TabIndex = 1
        Me.btnrefresh1.Text = "Refresh"
        Me.btnrefresh1.UseVisualStyleBackColor = False
        '
        'txtRecordCount
        '
        Me.txtRecordCount.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.txtRecordCount.Location = New System.Drawing.Point(460, 8)
        Me.txtRecordCount.Name = "txtRecordCount"
        Me.txtRecordCount.ReadOnly = True
        Me.txtRecordCount.Size = New System.Drawing.Size(85, 20)
        Me.txtRecordCount.TabIndex = 0
        Me.txtRecordCount.Text = "Record Count"
        Me.txtRecordCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(0, 0)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnRefresh.TabIndex = 0
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(0, 0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 0
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Location = New System.Drawing.Point(0, 0)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(75, 23)
        Me.btnSelectAll.TabIndex = 0
        '
        'btnDeselectAll
        '
        Me.btnDeselectAll.Location = New System.Drawing.Point(0, 0)
        Me.btnDeselectAll.Name = "btnDeselectAll"
        Me.btnDeselectAll.Size = New System.Drawing.Size(75, 23)
        Me.btnDeselectAll.TabIndex = 0
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(0, 0)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 0
        '
        'flxDetails
        '
        Me.flxDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flxDetails.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.flxDetails.Location = New System.Drawing.Point(5, 81)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.Size = New System.Drawing.Size(549, 496)
        Me.flxDetails.TabIndex = 2
        '
        'frm_GRDRnd_Parcels
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(557, 581)
        Me.Controls.Add(Me.pnlTitle)
        Me.Controls.Add(Me.pnlToolbar)
        Me.Controls.Add(Me.flxDetails)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_GRDRnd_Parcels"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Grading Parcels"
        Me.pnlTitle.ResumeLayout(False)
        Me.pnlToolbar.ResumeLayout(False)
        Me.pnlToolbar.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    '── Control Declarations ───────────────────────────────────
    Friend WithEvents pnlTitle As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents pnlToolbar As System.Windows.Forms.Panel
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnSelectAll As System.Windows.Forms.Button
    Friend WithEvents btnDeselectAll As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents txtRecordCount As System.Windows.Forms.TextBox
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents btnrefresh1 As Button
    Friend WithEvents btnexit1 As Button
    Friend WithEvents btnsave1 As Button

End Class