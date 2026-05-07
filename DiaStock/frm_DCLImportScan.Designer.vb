<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DCLImportScan
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
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtImgPath = New System.Windows.Forms.TextBox()
        Me.chkAffirmative = New DiaStock.HazelDev_CheckBox()
        Me.chkZimbabwe = New DiaStock.HazelDev_CheckBox()
        Me.chkSupp = New DiaStock.HazelDev_CheckBox()
        Me.chkKim = New DiaStock.HazelDev_CheckBox()
        Me.chkRussia = New DiaStock.HazelDev_CheckBox()
        Me.chkInvoice = New DiaStock.HazelDev_CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtPdfPath = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.cmdOpen = New DiaStock.HazelDev_Button()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtImgPath)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkAffirmative)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkZimbabwe)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkSupp)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkKim)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkRussia)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkInvoice)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtFileName)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label5)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPdfPath)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label14)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtInvoiceNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdOpen)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxDetails)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(763, 574)
        Me.HazelDev_ThemeContainer1.TabIndex = 3
        Me.HazelDev_ThemeContainer1.Text = "IMPORT DOCUMENT VERIFICATION"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(595, 106)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 15)
        Me.Label2.TabIndex = 141
        Me.Label2.Text = "Img Path"
        '
        'txtImgPath
        '
        Me.txtImgPath.Location = New System.Drawing.Point(598, 124)
        Me.txtImgPath.MaxLength = 0
        Me.txtImgPath.Name = "txtImgPath"
        Me.txtImgPath.ReadOnly = True
        Me.txtImgPath.Size = New System.Drawing.Size(153, 21)
        Me.txtImgPath.TabIndex = 140
        '
        'chkAffirmative
        '
        Me.chkAffirmative.Checked = False
        Me.chkAffirmative.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkAffirmative.Location = New System.Drawing.Point(7, 601)
        Me.chkAffirmative.Name = "chkAffirmative"
        Me.chkAffirmative.Size = New System.Drawing.Size(82, 25)
        Me.chkAffirmative.TabIndex = 139
        Me.chkAffirmative.Text = "Affirmative"
        Me.chkAffirmative.TransparencyKey = System.Drawing.Color.Empty
        Me.chkAffirmative.Visible = False
        '
        'chkZimbabwe
        '
        Me.chkZimbabwe.Checked = False
        Me.chkZimbabwe.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkZimbabwe.Location = New System.Drawing.Point(215, 601)
        Me.chkZimbabwe.Name = "chkZimbabwe"
        Me.chkZimbabwe.Size = New System.Drawing.Size(127, 25)
        Me.chkZimbabwe.TabIndex = 138
        Me.chkZimbabwe.Text = "Non Zimbabwian"
        Me.chkZimbabwe.TransparencyKey = System.Drawing.Color.Empty
        Me.chkZimbabwe.Visible = False
        '
        'chkSupp
        '
        Me.chkSupp.Checked = False
        Me.chkSupp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkSupp.Location = New System.Drawing.Point(360, 601)
        Me.chkSupp.Name = "chkSupp"
        Me.chkSupp.Size = New System.Drawing.Size(151, 25)
        Me.chkSupp.TabIndex = 137
        Me.chkSupp.Text = "Supplier Declaration "
        Me.chkSupp.TransparencyKey = System.Drawing.Color.Empty
        Me.chkSupp.Visible = False
        '
        'chkKim
        '
        Me.chkKim.Checked = False
        Me.chkKim.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkKim.Location = New System.Drawing.Point(360, 570)
        Me.chkKim.Name = "chkKim"
        Me.chkKim.Size = New System.Drawing.Size(94, 25)
        Me.chkKim.TabIndex = 136
        Me.chkKim.Text = "KPC"
        Me.chkKim.TransparencyKey = System.Drawing.Color.Empty
        Me.chkKim.Visible = False
        '
        'chkRussia
        '
        Me.chkRussia.Checked = False
        Me.chkRussia.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkRussia.Location = New System.Drawing.Point(215, 570)
        Me.chkRussia.Name = "chkRussia"
        Me.chkRussia.Size = New System.Drawing.Size(107, 25)
        Me.chkRussia.TabIndex = 135
        Me.chkRussia.Text = "Non Russian"
        Me.chkRussia.TransparencyKey = System.Drawing.Color.Empty
        Me.chkRussia.Visible = False
        '
        'chkInvoice
        '
        Me.chkInvoice.Checked = False
        Me.chkInvoice.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkInvoice.Location = New System.Drawing.Point(7, 570)
        Me.chkInvoice.Name = "chkInvoice"
        Me.chkInvoice.Size = New System.Drawing.Size(169, 25)
        Me.chkInvoice.TabIndex = 134
        Me.chkInvoice.Text = "Mining Company Invoice"
        Me.chkInvoice.TransparencyKey = System.Drawing.Color.Empty
        Me.chkInvoice.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(278, 106)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 15)
        Me.Label1.TabIndex = 133
        Me.Label1.Text = "File Name"
        '
        'txtFileName
        '
        Me.txtFileName.Location = New System.Drawing.Point(281, 124)
        Me.txtFileName.MaxLength = 0
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.ReadOnly = True
        Me.txtFileName.Size = New System.Drawing.Size(129, 21)
        Me.txtFileName.TabIndex = 132
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(413, 106)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 15)
        Me.Label5.TabIndex = 131
        Me.Label5.Text = "Inv Path"
        '
        'txtPdfPath
        '
        Me.txtPdfPath.Location = New System.Drawing.Point(416, 124)
        Me.txtPdfPath.MaxLength = 0
        Me.txtPdfPath.Name = "txtPdfPath"
        Me.txtPdfPath.ReadOnly = True
        Me.txtPdfPath.Size = New System.Drawing.Size(176, 21)
        Me.txtPdfPath.TabIndex = 130
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Location = New System.Drawing.Point(109, 106)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(67, 15)
        Me.Label14.TabIndex = 121
        Me.Label14.Text = "Invoice No."
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.Location = New System.Drawing.Point(112, 124)
        Me.txtInvoiceNo.MaxLength = 0
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(163, 21)
        Me.txtInvoiceNo.TabIndex = 120
        '
        'cmdOpen
        '
        Me.cmdOpen.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdOpen.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdOpen.FlatAppearance.BorderSize = 0
        Me.cmdOpen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdOpen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOpen.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOpen.ForeColor = System.Drawing.Color.White
        Me.cmdOpen.Location = New System.Drawing.Point(7, 112)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(99, 28)
        Me.cmdOpen.TabIndex = 76
        Me.cmdOpen.Text = "Open File"
        Me.cmdOpen.UseVisualStyleBackColor = False
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column3, Me.Column4, Me.Column5})
        Me.flxDetails.Location = New System.Drawing.Point(7, 151)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(744, 413)
        Me.flxDetails.TabIndex = 43
        '
        'Column3
        '
        Me.Column3.HeaderText = "InvoiceNo"
        Me.Column3.Name = "Column3"
        Me.Column3.Width = 150
        '
        'Column4
        '
        Me.Column4.HeaderText = "File Name"
        Me.Column4.Name = "Column4"
        Me.Column4.Width = 280
        '
        'Column5
        '
        Me.Column5.HeaderText = "File Path"
        Me.Column5.Name = "Column5"
        Me.Column5.Width = 280
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(748, 49)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSave.FlatAppearance.BorderSize = 0
        Me.cmdSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.Color.White
        Me.cmdSave.Location = New System.Drawing.Point(214, 13)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(99, 28)
        Me.cmdSave.TabIndex = 45
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdExit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdExit.FlatAppearance.BorderSize = 0
        Me.cmdExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdExit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.ForeColor = System.Drawing.Color.White
        Me.cmdExit.Location = New System.Drawing.Point(4, 13)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(99, 28)
        Me.cmdExit.TabIndex = 32
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'cmdNew
        '
        Me.cmdNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdNew.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdNew.FlatAppearance.BorderSize = 0
        Me.cmdNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdNew.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdNew.ForeColor = System.Drawing.Color.White
        Me.cmdNew.Location = New System.Drawing.Point(109, 13)
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.Size = New System.Drawing.Size(99, 28)
        Me.cmdNew.TabIndex = 31
        Me.cmdNew.Text = "Excel"
        Me.cmdNew.UseVisualStyleBackColor = False
        '
        'frm_DCLImportScan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(763, 574)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_DCLImportScan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import Document Verification"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents cmdOpen As DiaStock.HazelDev_Button
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents txtPdfPath As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents chkSupp As DiaStock.HazelDev_CheckBox
    Friend WithEvents chkKim As DiaStock.HazelDev_CheckBox
    Friend WithEvents chkRussia As DiaStock.HazelDev_CheckBox
    Friend WithEvents chkInvoice As DiaStock.HazelDev_CheckBox
    Friend WithEvents chkAffirmative As DiaStock.HazelDev_CheckBox
    Friend WithEvents chkZimbabwe As DiaStock.HazelDev_CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtImgPath As System.Windows.Forms.TextBox
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
