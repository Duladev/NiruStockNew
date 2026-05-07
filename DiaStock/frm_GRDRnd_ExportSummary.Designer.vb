<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_GRDRnd_ExportSummary
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_GRDRnd_ExportSummary))
        Me.pnlTitle = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblPackNo = New System.Windows.Forms.Label()
        Me.txtPackNo = New System.Windows.Forms.TextBox()
        Me.lblParcelNo = New System.Windows.Forms.Label()
        Me.cmbParcel = New System.Windows.Forms.ComboBox()
        Me.opt2 = New System.Windows.Forms.RadioButton()
        Me.opt3 = New System.Windows.Forms.RadioButton()
        Me.chkNew = New System.Windows.Forms.CheckBox()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.cmdExcel = New System.Windows.Forms.Button()
        Me.cmdNew = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.btnExportCSV = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.pnlTotals = New System.Windows.Forms.Panel()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.chkComplete = New System.Windows.Forms.CheckBox()
        Me.lblAddPcs = New System.Windows.Forms.Label()
        Me.txtAddPcs = New System.Windows.Forms.TextBox()
        Me.lblAddCts = New System.Windows.Forms.Label()
        Me.txtAddCts = New System.Windows.Forms.TextBox()
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
        Me.pnlTitle.Size = New System.Drawing.Size(1282, 35)
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
        Me.lblTitle.Size = New System.Drawing.Size(1282, 35)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "GRADING PACKAGE"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.pnlTop.Controls.Add(Me.lblPackNo)
        Me.pnlTop.Controls.Add(Me.txtPackNo)
        Me.pnlTop.Controls.Add(Me.lblParcelNo)
        Me.pnlTop.Controls.Add(Me.cmbParcel)
        Me.pnlTop.Controls.Add(Me.opt2)
        Me.pnlTop.Controls.Add(Me.opt3)
        Me.pnlTop.Controls.Add(Me.chkNew)
        Me.pnlTop.Controls.Add(Me.cmdRefresh)
        Me.pnlTop.Controls.Add(Me.cmdExcel)
        Me.pnlTop.Controls.Add(Me.cmdNew)
        Me.pnlTop.Controls.Add(Me.cmdSave)
        Me.pnlTop.Controls.Add(Me.btnExportCSV)
        Me.pnlTop.Controls.Add(Me.btnExit)
        Me.pnlTop.Location = New System.Drawing.Point(0, 35)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(1282, 53)
        Me.pnlTop.TabIndex = 1
        '
        'lblPackNo
        '
        Me.lblPackNo.BackColor = System.Drawing.Color.Transparent
        Me.lblPackNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblPackNo.Location = New System.Drawing.Point(8, 5)
        Me.lblPackNo.Name = "lblPackNo"
        Me.lblPackNo.Size = New System.Drawing.Size(85, 18)
        Me.lblPackNo.TabIndex = 0
        Me.lblPackNo.Text = "Package No."
        '
        'txtPackNo
        '
        Me.txtPackNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPackNo.Location = New System.Drawing.Point(8, 27)
        Me.txtPackNo.Name = "txtPackNo"
        Me.txtPackNo.Size = New System.Drawing.Size(70, 20)
        Me.txtPackNo.TabIndex = 1
        Me.txtPackNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblParcelNo
        '
        Me.lblParcelNo.BackColor = System.Drawing.Color.Transparent
        Me.lblParcelNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblParcelNo.Location = New System.Drawing.Point(90, 5)
        Me.lblParcelNo.Name = "lblParcelNo"
        Me.lblParcelNo.Size = New System.Drawing.Size(75, 18)
        Me.lblParcelNo.TabIndex = 2
        Me.lblParcelNo.Text = "Parcel No."
        '
        'cmbParcel
        '
        Me.cmbParcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmbParcel.Location = New System.Drawing.Point(90, 26)
        Me.cmbParcel.Name = "cmbParcel"
        Me.cmbParcel.Size = New System.Drawing.Size(200, 21)
        Me.cmbParcel.TabIndex = 3
        '
        'opt2
        '
        Me.opt2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.opt2.Location = New System.Drawing.Point(302, 24)
        Me.opt2.Name = "opt2"
        Me.opt2.Size = New System.Drawing.Size(50, 20)
        Me.opt2.TabIndex = 4
        Me.opt2.Text = "2nd"
        '
        'opt3
        '
        Me.opt3.Checked = True
        Me.opt3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.opt3.Location = New System.Drawing.Point(355, 24)
        Me.opt3.Name = "opt3"
        Me.opt3.Size = New System.Drawing.Size(50, 20)
        Me.opt3.TabIndex = 5
        Me.opt3.TabStop = True
        Me.opt3.Text = "3rd"
        '
        'chkNew
        '
        Me.chkNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.chkNew.ForeColor = System.Drawing.Color.DarkBlue
        Me.chkNew.Location = New System.Drawing.Point(415, 22)
        Me.chkNew.Name = "chkNew"
        Me.chkNew.Size = New System.Drawing.Size(55, 22)
        Me.chkNew.TabIndex = 6
        Me.chkNew.Text = "New"
        '
        'cmdRefresh
        '
        Me.cmdRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdRefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmdRefresh.Location = New System.Drawing.Point(623, 7)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(83, 32)
        Me.cmdRefresh.TabIndex = 7
        Me.cmdRefresh.Text = "Refresh"
        Me.cmdRefresh.UseVisualStyleBackColor = False
        '
        'cmdExcel
        '
        Me.cmdExcel.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdExcel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmdExcel.Location = New System.Drawing.Point(725, 7)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(80, 32)
        Me.cmdExcel.TabIndex = 8
        Me.cmdExcel.Text = "Excel"
        Me.cmdExcel.UseVisualStyleBackColor = False
        '
        'cmdNew
        '
        Me.cmdNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmdNew.Location = New System.Drawing.Point(813, 7)
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.Size = New System.Drawing.Size(80, 32)
        Me.cmdNew.TabIndex = 9
        Me.cmdNew.Text = "New"
        Me.cmdNew.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmdSave.Location = New System.Drawing.Point(901, 7)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(80, 32)
        Me.cmdSave.TabIndex = 10
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'btnExportCSV
        '
        Me.btnExportCSV.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnExportCSV.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnExportCSV.Location = New System.Drawing.Point(989, 7)
        Me.btnExportCSV.Name = "btnExportCSV"
        Me.btnExportCSV.Size = New System.Drawing.Size(80, 32)
        Me.btnExportCSV.TabIndex = 11
        Me.btnExportCSV.Text = "CSV"
        Me.btnExportCSV.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.btnExit.Location = New System.Drawing.Point(1077, 7)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(80, 32)
        Me.btnExit.TabIndex = 12
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'flxDetails
        '
        Me.flxDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flxDetails.ColumnHeadersHeight = 29
        Me.flxDetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.flxDetails.Location = New System.Drawing.Point(5, 94)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersWidth = 51
        Me.flxDetails.Size = New System.Drawing.Size(1277, 407)
        Me.flxDetails.TabIndex = 2
        '
        'pnlTotals
        '
        Me.pnlTotals.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlTotals.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.pnlTotals.Controls.Add(Me.cmdUpdate)
        Me.pnlTotals.Controls.Add(Me.chkComplete)
        Me.pnlTotals.Controls.Add(Me.lblAddPcs)
        Me.pnlTotals.Controls.Add(Me.txtAddPcs)
        Me.pnlTotals.Controls.Add(Me.lblAddCts)
        Me.pnlTotals.Controls.Add(Me.txtAddCts)
        Me.pnlTotals.Controls.Add(Me.lblPcs)
        Me.pnlTotals.Controls.Add(Me.txtPcs)
        Me.pnlTotals.Controls.Add(Me.lblCts)
        Me.pnlTotals.Controls.Add(Me.txtCts)
        Me.pnlTotals.Location = New System.Drawing.Point(5, 507)
        Me.pnlTotals.Name = "pnlTotals"
        Me.pnlTotals.Size = New System.Drawing.Size(1277, 45)
        Me.pnlTotals.TabIndex = 3
        '
        'cmdUpdate
        '
        Me.cmdUpdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmdUpdate.Location = New System.Drawing.Point(5, 7)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(74, 28)
        Me.cmdUpdate.TabIndex = 0
        Me.cmdUpdate.Text = "Update"
        Me.cmdUpdate.UseVisualStyleBackColor = False
        '
        'chkComplete
        '
        Me.chkComplete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.chkComplete.Location = New System.Drawing.Point(85, 7)
        Me.chkComplete.Name = "chkComplete"
        Me.chkComplete.Size = New System.Drawing.Size(80, 20)
        Me.chkComplete.TabIndex = 1
        Me.chkComplete.Text = "Complete"
        '
        'lblAddPcs
        '
        Me.lblAddPcs.BackColor = System.Drawing.Color.Transparent
        Me.lblAddPcs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblAddPcs.Location = New System.Drawing.Point(180, 7)
        Me.lblAddPcs.Name = "lblAddPcs"
        Me.lblAddPcs.Size = New System.Drawing.Size(65, 18)
        Me.lblAddPcs.TabIndex = 2
        Me.lblAddPcs.Text = "Orig Pcs:"
        '
        'txtAddPcs
        '
        Me.txtAddPcs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtAddPcs.Location = New System.Drawing.Point(250, 5)
        Me.txtAddPcs.Name = "txtAddPcs"
        Me.txtAddPcs.ReadOnly = True
        Me.txtAddPcs.Size = New System.Drawing.Size(60, 20)
        Me.txtAddPcs.TabIndex = 3
        Me.txtAddPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblAddCts
        '
        Me.lblAddCts.BackColor = System.Drawing.Color.Transparent
        Me.lblAddCts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblAddCts.Location = New System.Drawing.Point(320, 7)
        Me.lblAddCts.Name = "lblAddCts"
        Me.lblAddCts.Size = New System.Drawing.Size(65, 18)
        Me.lblAddCts.TabIndex = 4
        Me.lblAddCts.Text = "Orig Cts:"
        '
        'txtAddCts
        '
        Me.txtAddCts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtAddCts.Location = New System.Drawing.Point(390, 5)
        Me.txtAddCts.Name = "txtAddCts"
        Me.txtAddCts.ReadOnly = True
        Me.txtAddCts.Size = New System.Drawing.Size(75, 20)
        Me.txtAddCts.TabIndex = 5
        Me.txtAddCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPcs
        '
        Me.lblPcs.BackColor = System.Drawing.Color.Transparent
        Me.lblPcs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblPcs.Location = New System.Drawing.Point(480, 7)
        Me.lblPcs.Name = "lblPcs"
        Me.lblPcs.Size = New System.Drawing.Size(60, 18)
        Me.lblPcs.TabIndex = 6
        Me.lblPcs.Text = "Act Pcs:"
        '
        'txtPcs
        '
        Me.txtPcs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtPcs.Location = New System.Drawing.Point(545, 5)
        Me.txtPcs.Name = "txtPcs"
        Me.txtPcs.ReadOnly = True
        Me.txtPcs.Size = New System.Drawing.Size(60, 20)
        Me.txtPcs.TabIndex = 7
        Me.txtPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblCts
        '
        Me.lblCts.BackColor = System.Drawing.Color.Transparent
        Me.lblCts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCts.Location = New System.Drawing.Point(615, 7)
        Me.lblCts.Name = "lblCts"
        Me.lblCts.Size = New System.Drawing.Size(60, 18)
        Me.lblCts.TabIndex = 8
        Me.lblCts.Text = "Act Cts:"
        '
        'txtCts
        '
        Me.txtCts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtCts.Location = New System.Drawing.Point(680, 5)
        Me.txtCts.Name = "txtCts"
        Me.txtCts.ReadOnly = True
        Me.txtCts.Size = New System.Drawing.Size(80, 20)
        Me.txtCts.TabIndex = 9
        Me.txtCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'frm_GRDRnd_ExportSummary
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1282, 556)
        Me.Controls.Add(Me.pnlTitle)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.flxDetails)
        Me.Controls.Add(Me.pnlTotals)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_GRDRnd_ExportSummary"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GRADING PACKAGE"
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
    Friend WithEvents lblPackNo As System.Windows.Forms.Label
    Friend WithEvents txtPackNo As System.Windows.Forms.TextBox
    Friend WithEvents lblParcelNo As System.Windows.Forms.Label
    Friend WithEvents cmbParcel As System.Windows.Forms.ComboBox
    Friend WithEvents opt2 As System.Windows.Forms.RadioButton
    Friend WithEvents opt3 As System.Windows.Forms.RadioButton
    Friend WithEvents chkNew As System.Windows.Forms.CheckBox
    Friend WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents cmdExcel As System.Windows.Forms.Button
    Friend WithEvents cmdNew As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents btnExportCSV As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents pnlTotals As System.Windows.Forms.Panel
    Friend WithEvents cmdUpdate As System.Windows.Forms.Button
    Friend WithEvents chkComplete As System.Windows.Forms.CheckBox
    Friend WithEvents lblAddPcs As System.Windows.Forms.Label
    Friend WithEvents txtAddPcs As System.Windows.Forms.TextBox
    Friend WithEvents lblAddCts As System.Windows.Forms.Label
    Friend WithEvents txtAddCts As System.Windows.Forms.TextBox
    Friend WithEvents lblPcs As System.Windows.Forms.Label
    Friend WithEvents txtPcs As System.Windows.Forms.TextBox
    Friend WithEvents lblCts As System.Windows.Forms.Label
    Friend WithEvents txtCts As System.Windows.Forms.TextBox

End Class