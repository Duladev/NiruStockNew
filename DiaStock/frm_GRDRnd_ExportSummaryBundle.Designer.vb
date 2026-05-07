<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_GRDRnd_ExportSummaryBundle
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_GRDRnd_ExportSummaryBundle))
        Me.pnlTitle = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlRow1 = New System.Windows.Forms.Panel()
        Me.lblBundleNo = New System.Windows.Forms.Label()
        Me.txtBundleNo = New System.Windows.Forms.TextBox()
        Me.lblTotPcs = New System.Windows.Forms.Label()
        Me.txtPcs = New System.Windows.Forms.TextBox()
        Me.lblTotCts = New System.Windows.Forms.Label()
        Me.txtCts = New System.Windows.Forms.TextBox()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.cmdExcel = New System.Windows.Forms.Button()
        Me.cmdNew = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.pnlRow2 = New System.Windows.Forms.Panel()
        Me.lblAssort = New System.Windows.Forms.Label()
        Me.cmbAssort = New System.Windows.Forms.ComboBox()
        Me.lblSize = New System.Windows.Forms.Label()
        Me.cmbSize = New System.Windows.Forms.ComboBox()
        Me.lblNewPcs = New System.Windows.Forms.Label()
        Me.txtNewPcs = New System.Windows.Forms.TextBox()
        Me.lblNewCts = New System.Windows.Forms.Label()
        Me.txtNewCts = New System.Windows.Forms.TextBox()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.pnlTotals = New System.Windows.Forms.Panel()
        Me.lblTotPcsB = New System.Windows.Forms.Label()
        Me.txtTotPcs = New System.Windows.Forms.TextBox()
        Me.lblTotCtsB = New System.Windows.Forms.Label()
        Me.txtTotCts = New System.Windows.Forms.TextBox()
        Me.pnlTitle.SuspendLayout()
        Me.pnlRow1.SuspendLayout()
        Me.pnlRow2.SuspendLayout()
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
        Me.pnlTitle.Size = New System.Drawing.Size(902, 35)
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
        Me.lblTitle.Size = New System.Drawing.Size(902, 35)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "BUNDLE PACKAGES"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlRow1
        '
        Me.pnlRow1.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.pnlRow1.Controls.Add(Me.lblBundleNo)
        Me.pnlRow1.Controls.Add(Me.txtBundleNo)
        Me.pnlRow1.Controls.Add(Me.lblTotPcs)
        Me.pnlRow1.Controls.Add(Me.txtPcs)
        Me.pnlRow1.Controls.Add(Me.lblTotCts)
        Me.pnlRow1.Controls.Add(Me.txtCts)
        Me.pnlRow1.Controls.Add(Me.cmdRefresh)
        Me.pnlRow1.Controls.Add(Me.cmdExcel)
        Me.pnlRow1.Controls.Add(Me.cmdNew)
        Me.pnlRow1.Controls.Add(Me.cmdSave)
        Me.pnlRow1.Controls.Add(Me.btnExit)
        Me.pnlRow1.Location = New System.Drawing.Point(0, 35)
        Me.pnlRow1.Name = "pnlRow1"
        Me.pnlRow1.Size = New System.Drawing.Size(902, 55)
        Me.pnlRow1.TabIndex = 1
        '
        'lblBundleNo
        '
        Me.lblBundleNo.BackColor = System.Drawing.Color.Transparent
        Me.lblBundleNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblBundleNo.Location = New System.Drawing.Point(5, 5)
        Me.lblBundleNo.Name = "lblBundleNo"
        Me.lblBundleNo.Size = New System.Drawing.Size(80, 18)
        Me.lblBundleNo.TabIndex = 0
        Me.lblBundleNo.Text = "Bundle No."
        '
        'txtBundleNo
        '
        Me.txtBundleNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtBundleNo.Location = New System.Drawing.Point(5, 22)
        Me.txtBundleNo.Name = "txtBundleNo"
        Me.txtBundleNo.Size = New System.Drawing.Size(80, 20)
        Me.txtBundleNo.TabIndex = 1
        '
        'lblTotPcs
        '
        Me.lblTotPcs.BackColor = System.Drawing.Color.Transparent
        Me.lblTotPcs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTotPcs.Location = New System.Drawing.Point(95, 5)
        Me.lblTotPcs.Name = "lblTotPcs"
        Me.lblTotPcs.Size = New System.Drawing.Size(65, 18)
        Me.lblTotPcs.TabIndex = 2
        Me.lblTotPcs.Text = "Total Pcs"
        '
        'txtPcs
        '
        Me.txtPcs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPcs.Location = New System.Drawing.Point(95, 22)
        Me.txtPcs.Name = "txtPcs"
        Me.txtPcs.ReadOnly = True
        Me.txtPcs.Size = New System.Drawing.Size(70, 20)
        Me.txtPcs.TabIndex = 3
        Me.txtPcs.Text = "0"
        Me.txtPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblTotCts
        '
        Me.lblTotCts.BackColor = System.Drawing.Color.Transparent
        Me.lblTotCts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTotCts.Location = New System.Drawing.Point(175, 5)
        Me.lblTotCts.Name = "lblTotCts"
        Me.lblTotCts.Size = New System.Drawing.Size(65, 18)
        Me.lblTotCts.TabIndex = 4
        Me.lblTotCts.Text = "Total Cts"
        '
        'txtCts
        '
        Me.txtCts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCts.Location = New System.Drawing.Point(175, 22)
        Me.txtCts.Name = "txtCts"
        Me.txtCts.ReadOnly = True
        Me.txtCts.Size = New System.Drawing.Size(80, 20)
        Me.txtCts.TabIndex = 5
        Me.txtCts.Text = "0"
        Me.txtCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmdRefresh
        '
        Me.cmdRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdRefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmdRefresh.Location = New System.Drawing.Point(463, 12)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(80, 30)
        Me.cmdRefresh.TabIndex = 6
        Me.cmdRefresh.Text = "Refresh"
        Me.cmdRefresh.UseVisualStyleBackColor = False
        '
        'cmdExcel
        '
        Me.cmdExcel.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdExcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmdExcel.Location = New System.Drawing.Point(549, 12)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(80, 30)
        Me.cmdExcel.TabIndex = 7
        Me.cmdExcel.Text = "Excel"
        Me.cmdExcel.UseVisualStyleBackColor = False
        '
        'cmdNew
        '
        Me.cmdNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmdNew.Location = New System.Drawing.Point(635, 12)
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.Size = New System.Drawing.Size(80, 30)
        Me.cmdNew.TabIndex = 8
        Me.cmdNew.Text = "New"
        Me.cmdNew.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmdSave.Location = New System.Drawing.Point(721, 12)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(80, 30)
        Me.cmdSave.TabIndex = 9
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnExit.Location = New System.Drawing.Point(819, 12)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(80, 30)
        Me.btnExit.TabIndex = 10
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'pnlRow2
        '
        Me.pnlRow2.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.pnlRow2.Controls.Add(Me.lblAssort)
        Me.pnlRow2.Controls.Add(Me.cmbAssort)
        Me.pnlRow2.Controls.Add(Me.lblSize)
        Me.pnlRow2.Controls.Add(Me.cmbSize)
        Me.pnlRow2.Controls.Add(Me.lblNewPcs)
        Me.pnlRow2.Controls.Add(Me.txtNewPcs)
        Me.pnlRow2.Controls.Add(Me.lblNewCts)
        Me.pnlRow2.Controls.Add(Me.txtNewCts)
        Me.pnlRow2.Controls.Add(Me.cmdAdd)
        Me.pnlRow2.Location = New System.Drawing.Point(0, 96)
        Me.pnlRow2.Name = "pnlRow2"
        Me.pnlRow2.Size = New System.Drawing.Size(902, 57)
        Me.pnlRow2.TabIndex = 2
        '
        'lblAssort
        '
        Me.lblAssort.BackColor = System.Drawing.Color.Transparent
        Me.lblAssort.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblAssort.Location = New System.Drawing.Point(5, 5)
        Me.lblAssort.Name = "lblAssort"
        Me.lblAssort.Size = New System.Drawing.Size(85, 18)
        Me.lblAssort.TabIndex = 0
        Me.lblAssort.Text = "Assortment"
        '
        'cmbAssort
        '
        Me.cmbAssort.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmbAssort.Location = New System.Drawing.Point(5, 24)
        Me.cmbAssort.Name = "cmbAssort"
        Me.cmbAssort.Size = New System.Drawing.Size(150, 21)
        Me.cmbAssort.TabIndex = 1
        '
        'lblSize
        '
        Me.lblSize.BackColor = System.Drawing.Color.Transparent
        Me.lblSize.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblSize.Location = New System.Drawing.Point(165, 5)
        Me.lblSize.Name = "lblSize"
        Me.lblSize.Size = New System.Drawing.Size(80, 18)
        Me.lblSize.TabIndex = 2
        Me.lblSize.Text = "Size Range"
        '
        'cmbSize
        '
        Me.cmbSize.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmbSize.Location = New System.Drawing.Point(165, 24)
        Me.cmbSize.Name = "cmbSize"
        Me.cmbSize.Size = New System.Drawing.Size(100, 21)
        Me.cmbSize.TabIndex = 3
        '
        'lblNewPcs
        '
        Me.lblNewPcs.BackColor = System.Drawing.Color.Transparent
        Me.lblNewPcs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblNewPcs.Location = New System.Drawing.Point(275, 5)
        Me.lblNewPcs.Name = "lblNewPcs"
        Me.lblNewPcs.Size = New System.Drawing.Size(40, 18)
        Me.lblNewPcs.TabIndex = 4
        Me.lblNewPcs.Text = "Pcs"
        '
        'txtNewPcs
        '
        Me.txtNewPcs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtNewPcs.Location = New System.Drawing.Point(275, 24)
        Me.txtNewPcs.Name = "txtNewPcs"
        Me.txtNewPcs.Size = New System.Drawing.Size(60, 20)
        Me.txtNewPcs.TabIndex = 5
        '
        'lblNewCts
        '
        Me.lblNewCts.BackColor = System.Drawing.Color.Transparent
        Me.lblNewCts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblNewCts.Location = New System.Drawing.Point(345, 5)
        Me.lblNewCts.Name = "lblNewCts"
        Me.lblNewCts.Size = New System.Drawing.Size(40, 18)
        Me.lblNewCts.TabIndex = 6
        Me.lblNewCts.Text = "Cts"
        '
        'txtNewCts
        '
        Me.txtNewCts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtNewCts.Location = New System.Drawing.Point(345, 24)
        Me.txtNewCts.Name = "txtNewCts"
        Me.txtNewCts.Size = New System.Drawing.Size(75, 20)
        Me.txtNewCts.TabIndex = 7
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmdAdd.Location = New System.Drawing.Point(430, 21)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(60, 24)
        Me.cmdAdd.TabIndex = 8
        Me.cmdAdd.Text = "Add"
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'flxDetails
        '
        Me.flxDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flxDetails.ColumnHeadersHeight = 29
        Me.flxDetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.flxDetails.Location = New System.Drawing.Point(5, 161)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersWidth = 51
        Me.flxDetails.Size = New System.Drawing.Size(897, 461)
        Me.flxDetails.TabIndex = 3
        '
        'pnlTotals
        '
        Me.pnlTotals.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlTotals.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.pnlTotals.Controls.Add(Me.lblTotPcsB)
        Me.pnlTotals.Controls.Add(Me.txtTotPcs)
        Me.pnlTotals.Controls.Add(Me.lblTotCtsB)
        Me.pnlTotals.Controls.Add(Me.txtTotCts)
        Me.pnlTotals.Location = New System.Drawing.Point(0, 650)
        Me.pnlTotals.Name = "pnlTotals"
        Me.pnlTotals.Size = New System.Drawing.Size(914, 30)
        Me.pnlTotals.TabIndex = 4
        '
        'lblTotPcsB
        '
        Me.lblTotPcsB.BackColor = System.Drawing.Color.Transparent
        Me.lblTotPcsB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTotPcsB.Location = New System.Drawing.Point(220, 6)
        Me.lblTotPcsB.Name = "lblTotPcsB"
        Me.lblTotPcsB.Size = New System.Drawing.Size(60, 18)
        Me.lblTotPcsB.TabIndex = 0
        Me.lblTotPcsB.Text = "Act Pcs:"
        '
        'txtTotPcs
        '
        Me.txtTotPcs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtTotPcs.Location = New System.Drawing.Point(285, 4)
        Me.txtTotPcs.Name = "txtTotPcs"
        Me.txtTotPcs.ReadOnly = True
        Me.txtTotPcs.Size = New System.Drawing.Size(70, 20)
        Me.txtTotPcs.TabIndex = 1
        Me.txtTotPcs.Text = "0"
        Me.txtTotPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblTotCtsB
        '
        Me.lblTotCtsB.BackColor = System.Drawing.Color.Transparent
        Me.lblTotCtsB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTotCtsB.Location = New System.Drawing.Point(365, 6)
        Me.lblTotCtsB.Name = "lblTotCtsB"
        Me.lblTotCtsB.Size = New System.Drawing.Size(60, 18)
        Me.lblTotCtsB.TabIndex = 2
        Me.lblTotCtsB.Text = "Act Cts:"
        '
        'txtTotCts
        '
        Me.txtTotCts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtTotCts.Location = New System.Drawing.Point(430, 4)
        Me.txtTotCts.Name = "txtTotCts"
        Me.txtTotCts.ReadOnly = True
        Me.txtTotCts.Size = New System.Drawing.Size(80, 20)
        Me.txtTotCts.TabIndex = 3
        Me.txtTotCts.Text = "0"
        Me.txtTotCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'frm_GRDRnd_ExportSummaryBundle
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(902, 650)
        Me.Controls.Add(Me.pnlTitle)
        Me.Controls.Add(Me.pnlRow1)
        Me.Controls.Add(Me.pnlRow2)
        Me.Controls.Add(Me.flxDetails)
        Me.Controls.Add(Me.pnlTotals)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_GRDRnd_ExportSummaryBundle"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BUNDLE PACKAGES"
        Me.pnlTitle.ResumeLayout(False)
        Me.pnlRow1.ResumeLayout(False)
        Me.pnlRow1.PerformLayout()
        Me.pnlRow2.ResumeLayout(False)
        Me.pnlRow2.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTotals.ResumeLayout(False)
        Me.pnlTotals.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlTitle As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents pnlRow1 As System.Windows.Forms.Panel
    Friend WithEvents lblBundleNo As System.Windows.Forms.Label
    Friend WithEvents txtBundleNo As System.Windows.Forms.TextBox
    Friend WithEvents lblTotPcs As System.Windows.Forms.Label
    Friend WithEvents txtPcs As System.Windows.Forms.TextBox
    Friend WithEvents lblTotCts As System.Windows.Forms.Label
    Friend WithEvents txtCts As System.Windows.Forms.TextBox
    Friend WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents cmdExcel As System.Windows.Forms.Button
    Friend WithEvents cmdNew As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents pnlRow2 As System.Windows.Forms.Panel
    Friend WithEvents lblAssort As System.Windows.Forms.Label
    Friend WithEvents cmbAssort As System.Windows.Forms.ComboBox
    Friend WithEvents lblSize As System.Windows.Forms.Label
    Friend WithEvents cmbSize As System.Windows.Forms.ComboBox
    Friend WithEvents lblNewPcs As System.Windows.Forms.Label
    Friend WithEvents txtNewPcs As System.Windows.Forms.TextBox
    Friend WithEvents lblNewCts As System.Windows.Forms.Label
    Friend WithEvents txtNewCts As System.Windows.Forms.TextBox
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents pnlTotals As System.Windows.Forms.Panel
    Friend WithEvents lblTotPcsB As System.Windows.Forms.Label
    Friend WithEvents txtTotPcs As System.Windows.Forms.TextBox
    Friend WithEvents lblTotCtsB As System.Windows.Forms.Label
    Friend WithEvents txtTotCts As System.Windows.Forms.TextBox

End Class