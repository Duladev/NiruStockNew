<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_GRDRnd_Packet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_GRDRnd_Packet))
        Me.pnlTitle = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlToolbar = New System.Windows.Forms.Panel()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.txtRecordCount = New System.Windows.Forms.TextBox()
        Me.pnlEntry = New System.Windows.Forms.Panel()
        Me.lblParNo = New System.Windows.Forms.Label()
        Me.txtParNo = New System.Windows.Forms.TextBox()
        Me.lblPktNo = New System.Windows.Forms.Label()
        Me.txtPktNo = New System.Windows.Forms.TextBox()
        Me.lblPcs = New System.Windows.Forms.Label()
        Me.txtPktPcs = New System.Windows.Forms.TextBox()
        Me.lblCts = New System.Windows.Forms.Label()
        Me.txtPktCts = New System.Windows.Forms.TextBox()
        Me.lblSize = New System.Windows.Forms.Label()
        Me.cmbSize = New System.Windows.Forms.ComboBox()
        Me.grpModel = New System.Windows.Forms.GroupBox()
        Me.optNiruMake = New System.Windows.Forms.RadioButton()
        Me.optCommercial = New System.Windows.Forms.RadioButton()
        Me.optBlack = New System.Windows.Forms.RadioButton()
        Me.optSingleCut = New System.Windows.Forms.RadioButton()
        Me.flxPacket = New System.Windows.Forms.DataGridView()
        Me.pnlTitle.SuspendLayout()
        Me.pnlToolbar.SuspendLayout()
        Me.pnlEntry.SuspendLayout()
        Me.grpModel.SuspendLayout()
        CType(Me.flxPacket, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlTitle
        '
        Me.pnlTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.pnlTitle.Controls.Add(Me.lblTitle)
        Me.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTitle.Location = New System.Drawing.Point(0, 0)
        Me.pnlTitle.Name = "pnlTitle"
        Me.pnlTitle.Size = New System.Drawing.Size(607, 35)
        Me.pnlTitle.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(607, 35)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Grading Packet Entry"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlToolbar
        '
        Me.pnlToolbar.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.pnlToolbar.Controls.Add(Me.btnNew)
        Me.pnlToolbar.Controls.Add(Me.btnSave)
        Me.pnlToolbar.Controls.Add(Me.btnExit)
        Me.pnlToolbar.Controls.Add(Me.txtRecordCount)
        Me.pnlToolbar.Location = New System.Drawing.Point(0, 35)
        Me.pnlToolbar.Name = "pnlToolbar"
        Me.pnlToolbar.Size = New System.Drawing.Size(607, 46)
        Me.pnlToolbar.TabIndex = 1
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btnNew.Location = New System.Drawing.Point(5, 6)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(70, 31)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = "New"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btnSave.Location = New System.Drawing.Point(80, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(70, 31)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.btnExit.Location = New System.Drawing.Point(155, 6)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(70, 31)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'txtRecordCount
        '
        Me.txtRecordCount.BackColor = System.Drawing.Color.White
        Me.txtRecordCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.txtRecordCount.Location = New System.Drawing.Point(400, 8)
        Me.txtRecordCount.Name = "txtRecordCount"
        Me.txtRecordCount.ReadOnly = True
        Me.txtRecordCount.Size = New System.Drawing.Size(150, 21)
        Me.txtRecordCount.TabIndex = 3
        Me.txtRecordCount.Text = "Record Count"
        Me.txtRecordCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'pnlEntry
        '
        Me.pnlEntry.BackColor = System.Drawing.Color.White
        Me.pnlEntry.Controls.Add(Me.lblParNo)
        Me.pnlEntry.Controls.Add(Me.txtParNo)
        Me.pnlEntry.Controls.Add(Me.lblPktNo)
        Me.pnlEntry.Controls.Add(Me.txtPktNo)
        Me.pnlEntry.Controls.Add(Me.lblPcs)
        Me.pnlEntry.Controls.Add(Me.txtPktPcs)
        Me.pnlEntry.Controls.Add(Me.lblCts)
        Me.pnlEntry.Controls.Add(Me.txtPktCts)
        Me.pnlEntry.Controls.Add(Me.lblSize)
        Me.pnlEntry.Controls.Add(Me.cmbSize)
        Me.pnlEntry.Controls.Add(Me.grpModel)
        Me.pnlEntry.Location = New System.Drawing.Point(0, 87)
        Me.pnlEntry.Name = "pnlEntry"
        Me.pnlEntry.Size = New System.Drawing.Size(607, 175)
        Me.pnlEntry.TabIndex = 2
        '
        'lblParNo
        '
        Me.lblParNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.lblParNo.ForeColor = System.Drawing.Color.DarkRed
        Me.lblParNo.Location = New System.Drawing.Point(10, 15)
        Me.lblParNo.Name = "lblParNo"
        Me.lblParNo.Size = New System.Drawing.Size(80, 20)
        Me.lblParNo.TabIndex = 0
        Me.lblParNo.Text = "Parcel No"
        '
        'txtParNo
        '
        Me.txtParNo.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.txtParNo.Location = New System.Drawing.Point(100, 12)
        Me.txtParNo.Name = "txtParNo"
        Me.txtParNo.Size = New System.Drawing.Size(120, 20)
        Me.txtParNo.TabIndex = 1
        '
        'lblPktNo
        '
        Me.lblPktNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.lblPktNo.ForeColor = System.Drawing.Color.DarkRed
        Me.lblPktNo.Location = New System.Drawing.Point(240, 15)
        Me.lblPktNo.Name = "lblPktNo"
        Me.lblPktNo.Size = New System.Drawing.Size(75, 20)
        Me.lblPktNo.TabIndex = 2
        Me.lblPktNo.Text = "Packet No"
        '
        'txtPktNo
        '
        Me.txtPktNo.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.txtPktNo.Location = New System.Drawing.Point(321, 13)
        Me.txtPktNo.Name = "txtPktNo"
        Me.txtPktNo.Size = New System.Drawing.Size(60, 20)
        Me.txtPktNo.TabIndex = 3
        '
        'lblPcs
        '
        Me.lblPcs.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.lblPcs.Location = New System.Drawing.Point(10, 55)
        Me.lblPcs.Name = "lblPcs"
        Me.lblPcs.Size = New System.Drawing.Size(40, 20)
        Me.lblPcs.TabIndex = 4
        Me.lblPcs.Text = "PCs"
        '
        'txtPktPcs
        '
        Me.txtPktPcs.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.txtPktPcs.Location = New System.Drawing.Point(55, 52)
        Me.txtPktPcs.Name = "txtPktPcs"
        Me.txtPktPcs.Size = New System.Drawing.Size(60, 20)
        Me.txtPktPcs.TabIndex = 5
        '
        'lblCts
        '
        Me.lblCts.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.lblCts.Location = New System.Drawing.Point(130, 55)
        Me.lblCts.Name = "lblCts"
        Me.lblCts.Size = New System.Drawing.Size(40, 20)
        Me.lblCts.TabIndex = 6
        Me.lblCts.Text = "Cts"
        '
        'txtPktCts
        '
        Me.txtPktCts.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.txtPktCts.Location = New System.Drawing.Point(175, 52)
        Me.txtPktCts.Name = "txtPktCts"
        Me.txtPktCts.Size = New System.Drawing.Size(80, 20)
        Me.txtPktCts.TabIndex = 7
        '
        'lblSize
        '
        Me.lblSize.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.lblSize.Location = New System.Drawing.Point(270, 55)
        Me.lblSize.Name = "lblSize"
        Me.lblSize.Size = New System.Drawing.Size(40, 20)
        Me.lblSize.TabIndex = 8
        Me.lblSize.Text = "Size"
        Me.lblSize.Visible = False
        '
        'cmbSize
        '
        Me.cmbSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSize.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.cmbSize.Location = New System.Drawing.Point(321, 52)
        Me.cmbSize.Name = "cmbSize"
        Me.cmbSize.Size = New System.Drawing.Size(200, 24)
        Me.cmbSize.TabIndex = 9
        Me.cmbSize.Visible = False
        '
        'grpModel
        '
        Me.grpModel.Controls.Add(Me.optNiruMake)
        Me.grpModel.Controls.Add(Me.optCommercial)
        Me.grpModel.Controls.Add(Me.optBlack)
        Me.grpModel.Controls.Add(Me.optSingleCut)
        Me.grpModel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.grpModel.Location = New System.Drawing.Point(10, 90)
        Me.grpModel.Name = "grpModel"
        Me.grpModel.Size = New System.Drawing.Size(590, 50)
        Me.grpModel.TabIndex = 10
        Me.grpModel.TabStop = False
        Me.grpModel.Text = "Make"
        '
        'optNiruMake
        '
        Me.optNiruMake.Checked = True
        Me.optNiruMake.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.optNiruMake.Location = New System.Drawing.Point(10, 18)
        Me.optNiruMake.Name = "optNiruMake"
        Me.optNiruMake.Size = New System.Drawing.Size(100, 22)
        Me.optNiruMake.TabIndex = 0
        Me.optNiruMake.TabStop = True
        Me.optNiruMake.Text = "Niru Make"
        '
        'optCommercial
        '
        Me.optCommercial.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.optCommercial.Location = New System.Drawing.Point(120, 18)
        Me.optCommercial.Name = "optCommercial"
        Me.optCommercial.Size = New System.Drawing.Size(140, 22)
        Me.optCommercial.TabIndex = 1
        Me.optCommercial.Text = "Commercial Make"
        '
        'optBlack
        '
        Me.optBlack.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.optBlack.Location = New System.Drawing.Point(270, 18)
        Me.optBlack.Name = "optBlack"
        Me.optBlack.Size = New System.Drawing.Size(80, 22)
        Me.optBlack.TabIndex = 2
        Me.optBlack.Text = "Black"
        '
        'optSingleCut
        '
        Me.optSingleCut.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.optSingleCut.Location = New System.Drawing.Point(360, 18)
        Me.optSingleCut.Name = "optSingleCut"
        Me.optSingleCut.Size = New System.Drawing.Size(100, 22)
        Me.optSingleCut.TabIndex = 3
        Me.optSingleCut.Text = "Single Cut"
        '
        'flxPacket
        '
        Me.flxPacket.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.flxPacket.Location = New System.Drawing.Point(10, 268)
        Me.flxPacket.Name = "flxPacket"
        Me.flxPacket.Size = New System.Drawing.Size(590, 210)
        Me.flxPacket.TabIndex = 3
        '
        'frm_GRDRnd_Packet
        '
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(607, 521)
        Me.Controls.Add(Me.pnlTitle)
        Me.Controls.Add(Me.pnlToolbar)
        Me.Controls.Add(Me.pnlEntry)
        Me.Controls.Add(Me.flxPacket)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_GRDRnd_Packet"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Grading Packet Entry"
        Me.pnlTitle.ResumeLayout(False)
        Me.pnlToolbar.ResumeLayout(False)
        Me.pnlToolbar.PerformLayout()
        Me.pnlEntry.ResumeLayout(False)
        Me.pnlEntry.PerformLayout()
        Me.grpModel.ResumeLayout(False)
        CType(Me.flxPacket, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    '── Control Declarations ───────────────────────────────────
    Friend WithEvents pnlTitle As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents pnlToolbar As System.Windows.Forms.Panel
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents txtRecordCount As System.Windows.Forms.TextBox
    Friend WithEvents pnlEntry As System.Windows.Forms.Panel
    Friend WithEvents lblParNo As System.Windows.Forms.Label
    Friend WithEvents txtParNo As System.Windows.Forms.TextBox
    Friend WithEvents lblPktNo As System.Windows.Forms.Label
    Friend WithEvents txtPktNo As System.Windows.Forms.TextBox
    Friend WithEvents lblPcs As System.Windows.Forms.Label
    Friend WithEvents txtPktPcs As System.Windows.Forms.TextBox
    Friend WithEvents lblCts As System.Windows.Forms.Label
    Friend WithEvents txtPktCts As System.Windows.Forms.TextBox
    Friend WithEvents lblSize As System.Windows.Forms.Label
    Friend WithEvents cmbSize As System.Windows.Forms.ComboBox
    Friend WithEvents grpModel As System.Windows.Forms.GroupBox
    Friend WithEvents optNiruMake As System.Windows.Forms.RadioButton
    Friend WithEvents optCommercial As System.Windows.Forms.RadioButton
    Friend WithEvents optBlack As System.Windows.Forms.RadioButton
    Friend WithEvents optSingleCut As System.Windows.Forms.RadioButton
    Friend WithEvents flxPacket As System.Windows.Forms.DataGridView

End Class
