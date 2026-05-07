<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_LabCertify
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.CRViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdTest = New DiaStock.HazelDev_Button()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.dtpExpDate = New System.Windows.Forms.DateTimePicker()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.ExpProgress = New System.Windows.Forms.ProgressBar()
        Me.txtPackListNo = New System.Windows.Forms.TextBox()
        Me.HazelDev_Button1 = New DiaStock.HazelDev_Button()
        Me.HazelDev_Button2 = New DiaStock.HazelDev_Button()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        Me.HazelDev_Panel1.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CRViewer1
        '
        Me.CRViewer1.ActiveViewIndex = -1
        Me.CRViewer1.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.CRViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CRViewer1.Cursor = System.Windows.Forms.Cursors.Default
        Me.CRViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CRViewer1.Location = New System.Drawing.Point(0, 0)
        Me.CRViewer1.Name = "CRViewer1"
        Me.CRViewer1.ShowLogo = False
        Me.CRViewer1.Size = New System.Drawing.Size(715, 507)
        Me.CRViewer1.TabIndex = 0
        Me.CRViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(704, 442)
        Me.HazelDev_ThemeContainer1.TabIndex = 3
        Me.HazelDev_ThemeContainer1.Text = "LAB CERTIFICATION"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.HazelDev_Button2)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdTest)
        Me.HazelDev_Panel1.Controls.Add(Me.flxDetails)
        Me.HazelDev_Panel1.Controls.Add(Me.Label30)
        Me.HazelDev_Panel1.Controls.Add(Me.dtpExpDate)
        Me.HazelDev_Panel1.Controls.Add(Me.Label25)
        Me.HazelDev_Panel1.Controls.Add(Me.ExpProgress)
        Me.HazelDev_Panel1.Controls.Add(Me.txtPackListNo)
        Me.HazelDev_Panel1.Controls.Add(Me.HazelDev_Button1)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(693, 383)
        Me.HazelDev_Panel1.TabIndex = 0
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmdTest
        '
        Me.cmdTest.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdTest.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdTest.FlatAppearance.BorderSize = 0
        Me.cmdTest.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdTest.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdTest.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTest.ForeColor = System.Drawing.Color.White
        Me.cmdTest.Location = New System.Drawing.Point(549, 47)
        Me.cmdTest.Name = "cmdTest"
        Me.cmdTest.Size = New System.Drawing.Size(130, 32)
        Me.cmdTest.TabIndex = 146
        Me.cmdTest.Text = "Test"
        Me.cmdTest.UseVisualStyleBackColor = False
        Me.cmdTest.Visible = False
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column3, Me.Column2, Me.Column4})
        Me.flxDetails.Location = New System.Drawing.Point(12, 194)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.ReadOnly = True
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(667, 171)
        Me.flxDetails.TabIndex = 145
        '
        'Column1
        '
        Me.Column1.HeaderText = "Packing List No"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 150
        '
        'Column3
        '
        Me.Column3.HeaderText = "Client No"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "Pcs"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.HeaderText = "No"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(9, 148)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(73, 14)
        Me.Label30.TabIndex = 144
        Me.Label30.Text = "Export Date"
        '
        'dtpExpDate
        '
        Me.dtpExpDate.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpExpDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpExpDate.Location = New System.Drawing.Point(12, 166)
        Me.dtpExpDate.Name = "dtpExpDate"
        Me.dtpExpDate.Size = New System.Drawing.Size(102, 22)
        Me.dtpExpDate.TabIndex = 143
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(9, 2)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(93, 14)
        Me.Label25.TabIndex = 142
        Me.Label25.Text = "Packing List No."
        '
        'ExpProgress
        '
        Me.ExpProgress.Location = New System.Drawing.Point(9, 106)
        Me.ExpProgress.Name = "ExpProgress"
        Me.ExpProgress.Size = New System.Drawing.Size(670, 24)
        Me.ExpProgress.TabIndex = 141
        Me.ExpProgress.Visible = False
        '
        'txtPackListNo
        '
        Me.txtPackListNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.txtPackListNo.Location = New System.Drawing.Point(9, 19)
        Me.txtPackListNo.Name = "txtPackListNo"
        Me.txtPackListNo.Size = New System.Drawing.Size(130, 22)
        Me.txtPackListNo.TabIndex = 0
        '
        'HazelDev_Button1
        '
        Me.HazelDev_Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.HazelDev_Button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.HazelDev_Button1.FlatAppearance.BorderSize = 0
        Me.HazelDev_Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.HazelDev_Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.HazelDev_Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.HazelDev_Button1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HazelDev_Button1.ForeColor = System.Drawing.Color.White
        Me.HazelDev_Button1.Location = New System.Drawing.Point(9, 47)
        Me.HazelDev_Button1.Name = "HazelDev_Button1"
        Me.HazelDev_Button1.Size = New System.Drawing.Size(130, 32)
        Me.HazelDev_Button1.TabIndex = 0
        Me.HazelDev_Button1.Text = "Lab Certificates"
        Me.HazelDev_Button1.UseVisualStyleBackColor = False
        '
        'HazelDev_Button2
        '
        Me.HazelDev_Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.HazelDev_Button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.HazelDev_Button2.FlatAppearance.BorderSize = 0
        Me.HazelDev_Button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.HazelDev_Button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.HazelDev_Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.HazelDev_Button2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HazelDev_Button2.ForeColor = System.Drawing.Color.White
        Me.HazelDev_Button2.Location = New System.Drawing.Point(145, 47)
        Me.HazelDev_Button2.Name = "HazelDev_Button2"
        Me.HazelDev_Button2.Size = New System.Drawing.Size(130, 32)
        Me.HazelDev_Button2.TabIndex = 147
        Me.HazelDev_Button2.Text = "Export All"
        Me.HazelDev_Button2.UseVisualStyleBackColor = False
        '
        'frm_LabCertify
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(704, 442)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_LabCertify"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Lab Certification"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents HazelDev_Button1 As DiaStock.HazelDev_Button
    Private WithEvents CRViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents txtPackListNo As System.Windows.Forms.TextBox
    Friend WithEvents ExpProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents dtpExpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdTest As DiaStock.HazelDev_Button
    Friend WithEvents HazelDev_Button2 As DiaStock.HazelDev_Button
End Class
