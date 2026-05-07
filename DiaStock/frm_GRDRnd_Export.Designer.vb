<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_GRDRnd_Export
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_GRDRnd_Export))
        Me.pnlTitle = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblParNo = New System.Windows.Forms.Label()
        Me.txtParNo = New System.Windows.Forms.TextBox()
        Me.optParcel = New System.Windows.Forms.RadioButton()
        Me.optLot = New System.Windows.Forms.RadioButton()
        Me.cmdExport = New System.Windows.Forms.Button()
        Me.btnExportCSV = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.pnlTotals = New System.Windows.Forms.Panel()
        Me.lblPcs = New System.Windows.Forms.Label()
        Me.txtPcs = New System.Windows.Forms.TextBox()
        Me.lblCts = New System.Windows.Forms.Label()
        Me.txtCts = New System.Windows.Forms.TextBox()
        Me.lblValue = New System.Windows.Forms.Label()
        Me.txtValue = New System.Windows.Forms.TextBox()
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
        Me.pnlTitle.Size = New System.Drawing.Size(999, 35)
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
        Me.lblTitle.Size = New System.Drawing.Size(999, 35)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "PACKING LIST EXPORT"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.pnlTop.Controls.Add(Me.lblParNo)
        Me.pnlTop.Controls.Add(Me.txtParNo)
        Me.pnlTop.Controls.Add(Me.optParcel)
        Me.pnlTop.Controls.Add(Me.optLot)
        Me.pnlTop.Controls.Add(Me.cmdExport)
        Me.pnlTop.Controls.Add(Me.btnExportCSV)
        Me.pnlTop.Controls.Add(Me.btnExit)
        Me.pnlTop.Location = New System.Drawing.Point(8, 41)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(956, 47)
        Me.pnlTop.TabIndex = 1
        '
        'lblParNo
        '
        Me.lblParNo.BackColor = System.Drawing.Color.Transparent
        Me.lblParNo.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblParNo.ForeColor = System.Drawing.Color.DarkRed
        Me.lblParNo.Location = New System.Drawing.Point(8, 5)
        Me.lblParNo.Name = "lblParNo"
        Me.lblParNo.Size = New System.Drawing.Size(140, 18)
        Me.lblParNo.TabIndex = 0
        Me.lblParNo.Text = "Parcel No / Lot No"
        '
        'txtParNo
        '
        Me.txtParNo.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.txtParNo.Location = New System.Drawing.Point(8, 22)
        Me.txtParNo.Name = "txtParNo"
        Me.txtParNo.Size = New System.Drawing.Size(190, 20)
        Me.txtParNo.TabIndex = 1
        '
        'optParcel
        '
        Me.optParcel.Checked = True
        Me.optParcel.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.optParcel.Location = New System.Drawing.Point(210, 22)
        Me.optParcel.Name = "optParcel"
        Me.optParcel.Size = New System.Drawing.Size(65, 20)
        Me.optParcel.TabIndex = 2
        Me.optParcel.TabStop = True
        Me.optParcel.Text = "Parcel"
        '
        'optLot
        '
        Me.optLot.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.optLot.Location = New System.Drawing.Point(280, 22)
        Me.optLot.Name = "optLot"
        Me.optLot.Size = New System.Drawing.Size(50, 20)
        Me.optLot.TabIndex = 3
        Me.optLot.Text = "Lot"
        '
        'cmdExport
        '
        Me.cmdExport.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdExport.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.cmdExport.Location = New System.Drawing.Point(539, 9)
        Me.cmdExport.Name = "cmdExport"
        Me.cmdExport.Size = New System.Drawing.Size(104, 32)
        Me.cmdExport.TabIndex = 4
        Me.cmdExport.Text = "Export Excel"
        Me.cmdExport.UseVisualStyleBackColor = False
        '
        'btnExportCSV
        '
        Me.btnExportCSV.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnExportCSV.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.btnExportCSV.Location = New System.Drawing.Point(673, 9)
        Me.btnExportCSV.Name = "btnExportCSV"
        Me.btnExportCSV.Size = New System.Drawing.Size(95, 32)
        Me.btnExportCSV.TabIndex = 5
        Me.btnExportCSV.Text = "Export CSV"
        Me.btnExportCSV.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnExit.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.btnExit.Location = New System.Drawing.Point(849, 9)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(74, 32)
        Me.btnExit.TabIndex = 6
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'flxDetails
        '
        Me.flxDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flxDetails.Font = New System.Drawing.Font("Trebuchet MS", 8.25!)
        Me.flxDetails.Location = New System.Drawing.Point(8, 94)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.Size = New System.Drawing.Size(956, 388)
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
        Me.pnlTotals.Controls.Add(Me.lblValue)
        Me.pnlTotals.Controls.Add(Me.txtValue)
        Me.pnlTotals.Location = New System.Drawing.Point(4, 488)
        Me.pnlTotals.Name = "pnlTotals"
        Me.pnlTotals.Size = New System.Drawing.Size(960, 32)
        Me.pnlTotals.TabIndex = 3
        '
        'lblPcs
        '
        Me.lblPcs.BackColor = System.Drawing.Color.Transparent
        Me.lblPcs.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblPcs.Location = New System.Drawing.Point(200, 7)
        Me.lblPcs.Name = "lblPcs"
        Me.lblPcs.Size = New System.Drawing.Size(70, 18)
        Me.lblPcs.TabIndex = 0
        Me.lblPcs.Text = "Total Pcs:"
        '
        'txtPcs
        '
        Me.txtPcs.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtPcs.Location = New System.Drawing.Point(275, 5)
        Me.txtPcs.Name = "txtPcs"
        Me.txtPcs.ReadOnly = True
        Me.txtPcs.Size = New System.Drawing.Size(60, 20)
        Me.txtPcs.TabIndex = 1
        Me.txtPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblCts
        '
        Me.lblCts.BackColor = System.Drawing.Color.Transparent
        Me.lblCts.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCts.Location = New System.Drawing.Point(345, 7)
        Me.lblCts.Name = "lblCts"
        Me.lblCts.Size = New System.Drawing.Size(70, 18)
        Me.lblCts.TabIndex = 2
        Me.lblCts.Text = "Total Cts:"
        '
        'txtCts
        '
        Me.txtCts.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtCts.Location = New System.Drawing.Point(420, 5)
        Me.txtCts.Name = "txtCts"
        Me.txtCts.ReadOnly = True
        Me.txtCts.Size = New System.Drawing.Size(75, 20)
        Me.txtCts.TabIndex = 3
        Me.txtCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblValue
        '
        Me.lblValue.BackColor = System.Drawing.Color.Transparent
        Me.lblValue.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblValue.Location = New System.Drawing.Point(505, 7)
        Me.lblValue.Name = "lblValue"
        Me.lblValue.Size = New System.Drawing.Size(80, 18)
        Me.lblValue.TabIndex = 4
        Me.lblValue.Text = "Total Value:"
        '
        'txtValue
        '
        Me.txtValue.Font = New System.Drawing.Font("Trebuchet MS", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtValue.Location = New System.Drawing.Point(590, 5)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.ReadOnly = True
        Me.txtValue.Size = New System.Drawing.Size(90, 20)
        Me.txtValue.TabIndex = 5
        Me.txtValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'frm_GRDRnd_Export
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(999, 532)
        Me.Controls.Add(Me.pnlTitle)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.flxDetails)
        Me.Controls.Add(Me.pnlTotals)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_GRDRnd_Export"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PACKING LIST EXPORT"
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
    Friend WithEvents optParcel As System.Windows.Forms.RadioButton
    Friend WithEvents optLot As System.Windows.Forms.RadioButton
    Friend WithEvents cmdExport As System.Windows.Forms.Button
    Friend WithEvents btnExportCSV As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents pnlTotals As System.Windows.Forms.Panel
    Friend WithEvents lblPcs As System.Windows.Forms.Label
    Friend WithEvents txtPcs As System.Windows.Forms.TextBox
    Friend WithEvents lblCts As System.Windows.Forms.Label
    Friend WithEvents txtCts As System.Windows.Forms.TextBox
    Friend WithEvents lblValue As System.Windows.Forms.Label
    Friend WithEvents txtValue As System.Windows.Forms.TextBox

End Class