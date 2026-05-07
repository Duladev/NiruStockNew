<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DCLTurnOver
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.ExpProgress = New System.Windows.Forms.ProgressBar()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.Assortment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Price = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PacketPcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StCt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Charges = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AddChg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IssuePcs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IssueCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RareCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.txtCount = New System.Windows.Forms.TextBox()
        Me.txtTotCount = New System.Windows.Forms.TextBox()
        Me.HazelDev_Button1 = New DiaStock.HazelDev_Button()
        Me.cmdUpdate = New DiaStock.HazelDev_Button()
        Me.cmdNew2 = New DiaStock.HazelDev_Button()
        Me.cmdExcel = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.ExpProgress)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxDetails)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(1246, 638)
        Me.HazelDev_ThemeContainer1.TabIndex = 5
        Me.HazelDev_ThemeContainer1.Text = "TURN OVER"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'ExpProgress
        '
        Me.ExpProgress.Location = New System.Drawing.Point(6, 611)
        Me.ExpProgress.Name = "ExpProgress"
        Me.ExpProgress.Size = New System.Drawing.Size(1232, 24)
        Me.ExpProgress.TabIndex = 70
        Me.ExpProgress.Visible = False
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Assortment, Me.Price, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.PacketPcs, Me.Column1, Me.StCt, Me.Charges, Me.AddChg, Me.IssuePcs, Me.IssueCts, Me.RareCode, Me.Column2})
        Me.flxDetails.Location = New System.Drawing.Point(6, 109)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.ReadOnly = True
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.flxDetails.Size = New System.Drawing.Size(1232, 496)
        Me.flxDetails.TabIndex = 68
        '
        'Assortment
        '
        Me.Assortment.HeaderText = "Assortment"
        Me.Assortment.Name = "Assortment"
        Me.Assortment.ReadOnly = True
        '
        'Price
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Format = "N2"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.Price.DefaultCellStyle = DataGridViewCellStyle1
        Me.Price.HeaderText = "List Price"
        Me.Price.Name = "Price"
        Me.Price.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewTextBoxColumn2.HeaderText = "Open Pcs"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.NullValue = Nothing
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridViewTextBoxColumn3.HeaderText = "In Pcs"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'PacketPcs
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PacketPcs.DefaultCellStyle = DataGridViewCellStyle4
        Me.PacketPcs.HeaderText = "Exp Pcs"
        Me.PacketPcs.Name = "PacketPcs"
        Me.PacketPcs.ReadOnly = True
        '
        'Column1
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle5
        Me.Column1.HeaderText = "D Exp Pcs"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'StCt
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.StCt.DefaultCellStyle = DataGridViewCellStyle6
        Me.StCt.HeaderText = "Cur Pcs"
        Me.StCt.Name = "StCt"
        Me.StCt.ReadOnly = True
        '
        'Charges
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Charges.DefaultCellStyle = DataGridViewCellStyle7
        Me.Charges.HeaderText = "Cur Cts"
        Me.Charges.Name = "Charges"
        Me.Charges.ReadOnly = True
        '
        'AddChg
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.AddChg.DefaultCellStyle = DataGridViewCellStyle8
        Me.AddChg.HeaderText = "Turn Over"
        Me.AddChg.Name = "AddChg"
        Me.AddChg.ReadOnly = True
        '
        'IssuePcs
        '
        Me.IssuePcs.HeaderText = "BAG Assrt"
        Me.IssuePcs.Name = "IssuePcs"
        Me.IssuePcs.ReadOnly = True
        '
        'IssueCts
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.IssueCts.DefaultCellStyle = DataGridViewCellStyle9
        Me.IssueCts.HeaderText = "BAG Price"
        Me.IssueCts.Name = "IssueCts"
        Me.IssueCts.ReadOnly = True
        '
        'RareCode
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.RareCode.DefaultCellStyle = DataGridViewCellStyle10
        Me.RareCode.HeaderText = "Avg Price"
        Me.RareCode.Name = "RareCode"
        Me.RareCode.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "Size Range"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.txtCount)
        Me.HazelDev_Panel1.Controls.Add(Me.txtTotCount)
        Me.HazelDev_Panel1.Controls.Add(Me.HazelDev_Button1)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdUpdate)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew2)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExcel)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(1235, 49)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'txtCount
        '
        Me.txtCount.Location = New System.Drawing.Point(847, 17)
        Me.txtCount.Name = "txtCount"
        Me.txtCount.ReadOnly = True
        Me.txtCount.Size = New System.Drawing.Size(84, 22)
        Me.txtCount.TabIndex = 98
        Me.txtCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotCount
        '
        Me.txtTotCount.Location = New System.Drawing.Point(937, 17)
        Me.txtTotCount.Name = "txtTotCount"
        Me.txtTotCount.ReadOnly = True
        Me.txtTotCount.Size = New System.Drawing.Size(84, 22)
        Me.txtTotCount.TabIndex = 97
        Me.txtTotCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'HazelDev_Button1
        '
        Me.HazelDev_Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.HazelDev_Button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.HazelDev_Button1.FlatAppearance.BorderSize = 0
        Me.HazelDev_Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.HazelDev_Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.HazelDev_Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.HazelDev_Button1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HazelDev_Button1.ForeColor = System.Drawing.Color.White
        Me.HazelDev_Button1.Location = New System.Drawing.Point(319, 13)
        Me.HazelDev_Button1.Name = "HazelDev_Button1"
        Me.HazelDev_Button1.Size = New System.Drawing.Size(99, 28)
        Me.HazelDev_Button1.TabIndex = 36
        Me.HazelDev_Button1.Text = "Report"
        Me.HazelDev_Button1.UseVisualStyleBackColor = False
        '
        'cmdUpdate
        '
        Me.cmdUpdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdUpdate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdUpdate.FlatAppearance.BorderSize = 0
        Me.cmdUpdate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdUpdate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdUpdate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUpdate.ForeColor = System.Drawing.Color.White
        Me.cmdUpdate.Location = New System.Drawing.Point(1027, 13)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(99, 28)
        Me.cmdUpdate.TabIndex = 35
        Me.cmdUpdate.Text = "Update Size"
        Me.cmdUpdate.UseVisualStyleBackColor = False
        '
        'cmdNew2
        '
        Me.cmdNew2.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdNew2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdNew2.FlatAppearance.BorderSize = 0
        Me.cmdNew2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdNew2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdNew2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdNew2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdNew2.ForeColor = System.Drawing.Color.White
        Me.cmdNew2.Location = New System.Drawing.Point(214, 13)
        Me.cmdNew2.Name = "cmdNew2"
        Me.cmdNew2.Size = New System.Drawing.Size(99, 28)
        Me.cmdNew2.TabIndex = 34
        Me.cmdNew2.Text = "New Cts"
        Me.cmdNew2.UseVisualStyleBackColor = False
        Me.cmdNew2.Visible = False
        '
        'cmdExcel
        '
        Me.cmdExcel.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdExcel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdExcel.FlatAppearance.BorderSize = 0
        Me.cmdExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdExcel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdExcel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExcel.ForeColor = System.Drawing.Color.White
        Me.cmdExcel.Location = New System.Drawing.Point(1132, 13)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(99, 28)
        Me.cmdExcel.TabIndex = 33
        Me.cmdExcel.Text = "Excel"
        Me.cmdExcel.UseVisualStyleBackColor = False
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
        Me.cmdNew.Text = "New"
        Me.cmdNew.UseVisualStyleBackColor = False
        '
        'frm_DCLTurnOver
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1246, 638)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_DCLTurnOver"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Turn Over"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents ExpProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew2 As DiaStock.HazelDev_Button
    Friend WithEvents Assortment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PacketPcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StCt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Charges As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AddChg As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IssuePcs As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IssueCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RareCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmdUpdate As DiaStock.HazelDev_Button
    Friend WithEvents HazelDev_Button1 As DiaStock.HazelDev_Button
    Friend WithEvents txtCount As System.Windows.Forms.TextBox
    Friend WithEvents txtTotCount As System.Windows.Forms.TextBox
End Class
