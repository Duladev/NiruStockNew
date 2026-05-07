<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DCLFantacyAnalyzer
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
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.cmdAdd = New DiaStock.HazelDev_Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtRghCts = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtAddCts = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtExpPcs = New System.Windows.Forms.TextBox()
        Me.cmdMix = New DiaStock.HazelDev_Button()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.DeprtmentNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ItemName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LotName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Qty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Weight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalSale = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Remark = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ForceNew = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProfitCentre = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.C = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.A = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LineNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Type = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.HazelDev_Button1 = New DiaStock.HazelDev_Button()
        Me.cmdFinish = New DiaStock.HazelDev_Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtLotNo = New System.Windows.Forms.TextBox()
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
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdAdd)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label3)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtRghCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtAddCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtExpPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdMix)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxDetails)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(1247, 605)
        Me.HazelDev_ThemeContainer1.TabIndex = 4
        Me.HazelDev_ThemeContainer1.Text = "FANTACY ANALYZER"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdAdd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdAdd.FlatAppearance.BorderSize = 0
        Me.cmdAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAdd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.ForeColor = System.Drawing.Color.White
        Me.cmdAdd.Location = New System.Drawing.Point(748, 571)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(99, 28)
        Me.cmdAdd.TabIndex = 82
        Me.cmdAdd.Text = "Add Cts"
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(311, 574)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 15)
        Me.Label3.TabIndex = 81
        Me.Label3.Text = "Rgh Cts"
        '
        'txtRghCts
        '
        Me.txtRghCts.Location = New System.Drawing.Point(383, 574)
        Me.txtRghCts.Name = "txtRghCts"
        Me.txtRghCts.ReadOnly = True
        Me.txtRghCts.Size = New System.Drawing.Size(102, 21)
        Me.txtRghCts.TabIndex = 80
        Me.txtRghCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(495, 574)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 15)
        Me.Label2.TabIndex = 79
        Me.Label2.Text = "Add Cts"
        '
        'txtAddCts
        '
        Me.txtAddCts.Location = New System.Drawing.Point(567, 574)
        Me.txtAddCts.Name = "txtAddCts"
        Me.txtAddCts.Size = New System.Drawing.Size(102, 21)
        Me.txtAddCts.TabIndex = 78
        Me.txtAddCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(128, 574)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 15)
        Me.Label1.TabIndex = 77
        Me.Label1.Text = "Exp Pcs"
        '
        'txtExpPcs
        '
        Me.txtExpPcs.Location = New System.Drawing.Point(200, 574)
        Me.txtExpPcs.Name = "txtExpPcs"
        Me.txtExpPcs.ReadOnly = True
        Me.txtExpPcs.Size = New System.Drawing.Size(102, 21)
        Me.txtExpPcs.TabIndex = 76
        Me.txtExpPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmdMix
        '
        Me.cmdMix.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdMix.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdMix.FlatAppearance.BorderSize = 0
        Me.cmdMix.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdMix.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdMix.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdMix.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdMix.ForeColor = System.Drawing.Color.White
        Me.cmdMix.Location = New System.Drawing.Point(7, 571)
        Me.cmdMix.Name = "cmdMix"
        Me.cmdMix.Size = New System.Drawing.Size(99, 28)
        Me.cmdMix.TabIndex = 69
        Me.cmdMix.Text = "Mix"
        Me.cmdMix.UseVisualStyleBackColor = False
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DeprtmentNo, Me.ItemName, Me.LotName, Me.PS, Me.Qty, Me.Weight, Me.TotalCost, Me.TotalSale, Me.Remark, Me.ForceNew, Me.ProfitCentre, Me.C, Me.A, Me.LineNo, Me.Type})
        Me.flxDetails.Location = New System.Drawing.Point(6, 109)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.ReadOnly = True
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.flxDetails.Size = New System.Drawing.Size(1232, 456)
        Me.flxDetails.TabIndex = 68
        '
        'DeprtmentNo
        '
        Me.DeprtmentNo.HeaderText = "Deprt. No."
        Me.DeprtmentNo.Name = "DeprtmentNo"
        Me.DeprtmentNo.ReadOnly = True
        '
        'ItemName
        '
        Me.ItemName.HeaderText = "Item Name"
        Me.ItemName.Name = "ItemName"
        Me.ItemName.ReadOnly = True
        '
        'LotName
        '
        Me.LotName.HeaderText = "Lot Name"
        Me.LotName.Name = "LotName"
        Me.LotName.ReadOnly = True
        '
        'PS
        '
        Me.PS.HeaderText = "P/S"
        Me.PS.Name = "PS"
        Me.PS.ReadOnly = True
        '
        'Qty
        '
        Me.Qty.HeaderText = "Qty"
        Me.Qty.Name = "Qty"
        Me.Qty.ReadOnly = True
        '
        'Weight
        '
        Me.Weight.HeaderText = "Weight"
        Me.Weight.Name = "Weight"
        Me.Weight.ReadOnly = True
        '
        'TotalCost
        '
        Me.TotalCost.HeaderText = "Total Cost"
        Me.TotalCost.Name = "TotalCost"
        Me.TotalCost.ReadOnly = True
        '
        'TotalSale
        '
        Me.TotalSale.HeaderText = "Total Sale"
        Me.TotalSale.Name = "TotalSale"
        Me.TotalSale.ReadOnly = True
        '
        'Remark
        '
        Me.Remark.HeaderText = "Remark"
        Me.Remark.Name = "Remark"
        Me.Remark.ReadOnly = True
        '
        'ForceNew
        '
        Me.ForceNew.HeaderText = "Force New"
        Me.ForceNew.Name = "ForceNew"
        Me.ForceNew.ReadOnly = True
        '
        'ProfitCentre
        '
        Me.ProfitCentre.HeaderText = "Profit Centre"
        Me.ProfitCentre.Name = "ProfitCentre"
        Me.ProfitCentre.ReadOnly = True
        '
        'C
        '
        Me.C.HeaderText = "C"
        Me.C.Name = "C"
        Me.C.ReadOnly = True
        '
        'A
        '
        Me.A.HeaderText = "A"
        Me.A.Name = "A"
        Me.A.ReadOnly = True
        '
        'LineNo
        '
        Me.LineNo.HeaderText = "Line No."
        Me.LineNo.Name = "LineNo"
        Me.LineNo.ReadOnly = True
        '
        'Type
        '
        Me.Type.HeaderText = "Type"
        Me.Type.Name = "Type"
        Me.Type.ReadOnly = True
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.HazelDev_Button1)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdFinish)
        Me.HazelDev_Panel1.Controls.Add(Me.Label12)
        Me.HazelDev_Panel1.Controls.Add(Me.txtLotNo)
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
        Me.HazelDev_Button1.Location = New System.Drawing.Point(1028, 13)
        Me.HazelDev_Button1.Name = "HazelDev_Button1"
        Me.HazelDev_Button1.Size = New System.Drawing.Size(99, 28)
        Me.HazelDev_Button1.TabIndex = 77
        Me.HazelDev_Button1.Text = "Assorting"
        Me.HazelDev_Button1.UseVisualStyleBackColor = False
        '
        'cmdFinish
        '
        Me.cmdFinish.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdFinish.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdFinish.FlatAppearance.BorderSize = 0
        Me.cmdFinish.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdFinish.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdFinish.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdFinish.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFinish.ForeColor = System.Drawing.Color.White
        Me.cmdFinish.Location = New System.Drawing.Point(745, 13)
        Me.cmdFinish.Name = "cmdFinish"
        Me.cmdFinish.Size = New System.Drawing.Size(99, 28)
        Me.cmdFinish.TabIndex = 76
        Me.cmdFinish.Text = "Analyzer FInish"
        Me.cmdFinish.UseVisualStyleBackColor = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Location = New System.Drawing.Point(219, 13)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(48, 14)
        Me.Label12.TabIndex = 75
        Me.Label12.Text = "Lot No."
        '
        'txtLotNo
        '
        Me.txtLotNo.Location = New System.Drawing.Point(291, 13)
        Me.txtLotNo.Name = "txtLotNo"
        Me.txtLotNo.Size = New System.Drawing.Size(102, 22)
        Me.txtLotNo.TabIndex = 0
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
        Me.cmdExcel.Location = New System.Drawing.Point(1133, 13)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(99, 28)
        Me.cmdExcel.TabIndex = 72
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
        'frm_DCLFantacyAnalyzer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1247, 605)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_DCLFantacyAnalyzer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Fantacy Analyzer"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents txtLotNo As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cmdFinish As DiaStock.HazelDev_Button
    Friend WithEvents cmdMix As DiaStock.HazelDev_Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtExpPcs As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtRghCts As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtAddCts As System.Windows.Forms.TextBox
    Friend WithEvents cmdAdd As DiaStock.HazelDev_Button
    Friend WithEvents HazelDev_Button1 As DiaStock.HazelDev_Button
    Friend WithEvents DeprtmentNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ItemName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LotName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Weight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TotalCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TotalSale As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Remark As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ForceNew As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ProfitCentre As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents A As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LineNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Type As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
