<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DCLFantacySalesSchema
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
        Me.ExpProgress = New System.Windows.Forms.ProgressBar()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.LotID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Department = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ItemName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ProfitCenter = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WeightOrQuantity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ParcelStone = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LotName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Quantity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Weight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Shape = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Color = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ClarityID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HardTotalCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalAskingPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Sale = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RapList = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LabID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CertNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Remarks = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ClientSOCommande = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Seriename = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Customerdescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Description = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Listprice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Forevermark = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ExportNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.txtExportNo = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmdExcel = New DiaStock.HazelDev_Button()
        Me.optPurchased = New System.Windows.Forms.RadioButton()
        Me.optNFE = New System.Windows.Forms.RadioButton()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.chkCost = New DiaStock.HazelDev_CheckBox()
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
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(1246, 694)
        Me.HazelDev_ThemeContainer1.TabIndex = 4
        Me.HazelDev_ThemeContainer1.Text = "FANTACY SALES SCHEMA"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'ExpProgress
        '
        Me.ExpProgress.Location = New System.Drawing.Point(7, 665)
        Me.ExpProgress.Name = "ExpProgress"
        Me.ExpProgress.Size = New System.Drawing.Size(1232, 24)
        Me.ExpProgress.TabIndex = 71
        Me.ExpProgress.Visible = False
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.LotID, Me.Department, Me.ItemName, Me.ProfitCenter, Me.WeightOrQuantity, Me.ParcelStone, Me.LotName, Me.Quantity, Me.Weight, Me.Shape, Me.Color, Me.ClarityID, Me.HardTotalCost, Me.TotalCost, Me.TotalAskingPrice, Me.Sale, Me.RapList, Me.LabID, Me.CertNo, Me.Remarks, Me.ClientSOCommande, Me.Column4, Me.Seriename, Me.Customerdescription, Me.Description, Me.Listprice, Me.Forevermark, Me.Column1, Me.Column2, Me.Column3, Me.ExportNo})
        Me.flxDetails.Location = New System.Drawing.Point(6, 109)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.flxDetails.Size = New System.Drawing.Size(1232, 550)
        Me.flxDetails.TabIndex = 68
        '
        'LotID
        '
        Me.LotID.HeaderText = "Lot ID"
        Me.LotID.Name = "LotID"
        '
        'Department
        '
        Me.Department.HeaderText = "Department"
        Me.Department.Name = "Department"
        '
        'ItemName
        '
        Me.ItemName.HeaderText = "Item Name"
        Me.ItemName.Name = "ItemName"
        '
        'ProfitCenter
        '
        Me.ProfitCenter.HeaderText = "Profit Center"
        Me.ProfitCenter.Name = "ProfitCenter"
        '
        'WeightOrQuantity
        '
        Me.WeightOrQuantity.HeaderText = "WeightOrQuantity"
        Me.WeightOrQuantity.Name = "WeightOrQuantity"
        '
        'ParcelStone
        '
        Me.ParcelStone.HeaderText = "Parcel\Stone"
        Me.ParcelStone.Name = "ParcelStone"
        '
        'LotName
        '
        Me.LotName.HeaderText = "Lot Name"
        Me.LotName.Name = "LotName"
        '
        'Quantity
        '
        Me.Quantity.HeaderText = "Quantity"
        Me.Quantity.Name = "Quantity"
        '
        'Weight
        '
        Me.Weight.HeaderText = "Weight"
        Me.Weight.Name = "Weight"
        '
        'Shape
        '
        Me.Shape.HeaderText = "Shape"
        Me.Shape.Name = "Shape"
        '
        'Color
        '
        Me.Color.HeaderText = "Color"
        Me.Color.Name = "Color"
        '
        'ClarityID
        '
        Me.ClarityID.HeaderText = "ClarityID"
        Me.ClarityID.Name = "ClarityID"
        '
        'HardTotalCost
        '
        Me.HardTotalCost.HeaderText = "Hard Total Cost"
        Me.HardTotalCost.Name = "HardTotalCost"
        '
        'TotalCost
        '
        Me.TotalCost.HeaderText = "Total Cost"
        Me.TotalCost.Name = "TotalCost"
        '
        'TotalAskingPrice
        '
        Me.TotalAskingPrice.HeaderText = "Total Asking Price"
        Me.TotalAskingPrice.Name = "TotalAskingPrice"
        '
        'Sale
        '
        Me.Sale.HeaderText = "% Sale"
        Me.Sale.Name = "Sale"
        '
        'RapList
        '
        Me.RapList.HeaderText = "Rap List"
        Me.RapList.Name = "RapList"
        '
        'LabID
        '
        Me.LabID.HeaderText = "LabID"
        Me.LabID.Name = "LabID"
        '
        'CertNo
        '
        Me.CertNo.HeaderText = "Cert No."
        Me.CertNo.Name = "CertNo"
        '
        'Remarks
        '
        Me.Remarks.HeaderText = "Remarks"
        Me.Remarks.Name = "Remarks"
        '
        'ClientSOCommande
        '
        Me.ClientSOCommande.HeaderText = "Client SO#\Commande"
        Me.ClientSOCommande.Name = "ClientSOCommande"
        '
        'Column4
        '
        Me.Column4.HeaderText = "SerieID"
        Me.Column4.Name = "Column4"
        '
        'Seriename
        '
        Me.Seriename.HeaderText = "Serie name"
        Me.Seriename.Name = "Seriename"
        '
        'Customerdescription
        '
        Me.Customerdescription.HeaderText = "Customer description"
        Me.Customerdescription.Name = "Customerdescription"
        '
        'Description
        '
        Me.Description.HeaderText = "Description"
        Me.Description.Name = "Description"
        '
        'Listprice
        '
        Me.Listprice.HeaderText = "List price"
        Me.Listprice.Name = "Listprice"
        '
        'Forevermark
        '
        Me.Forevermark.HeaderText = "Forevermark"
        Me.Forevermark.Name = "Forevermark"
        '
        'Column1
        '
        Me.Column1.HeaderText = "T.Value"
        Me.Column1.Name = "Column1"
        '
        'Column2
        '
        Me.Column2.HeaderText = "EZ Box"
        Me.Column2.Name = "Column2"
        '
        'Column3
        '
        Me.Column3.HeaderText = "Total Client"
        Me.Column3.Name = "Column3"
        '
        'ExportNo
        '
        Me.ExportNo.HeaderText = "Export No"
        Me.ExportNo.Name = "ExportNo"
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.chkCost)
        Me.HazelDev_Panel1.Controls.Add(Me.txtExportNo)
        Me.HazelDev_Panel1.Controls.Add(Me.Label12)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExcel)
        Me.HazelDev_Panel1.Controls.Add(Me.optPurchased)
        Me.HazelDev_Panel1.Controls.Add(Me.optNFE)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(1235, 49)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'txtExportNo
        '
        Me.txtExportNo.Location = New System.Drawing.Point(324, 12)
        Me.txtExportNo.Name = "txtExportNo"
        Me.txtExportNo.Size = New System.Drawing.Size(102, 22)
        Me.txtExportNo.TabIndex = 73
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Location = New System.Drawing.Point(252, 15)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(66, 14)
        Me.Label12.TabIndex = 74
        Me.Label12.Text = "Export No."
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
        Me.cmdExcel.Location = New System.Drawing.Point(1122, 13)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(99, 28)
        Me.cmdExcel.TabIndex = 72
        Me.cmdExcel.Text = "Excel"
        Me.cmdExcel.UseVisualStyleBackColor = False
        '
        'optPurchased
        '
        Me.optPurchased.AutoSize = True
        Me.optPurchased.Location = New System.Drawing.Point(165, 13)
        Me.optPurchased.Name = "optPurchased"
        Me.optPurchased.Size = New System.Drawing.Size(81, 18)
        Me.optPurchased.TabIndex = 71
        Me.optPurchased.Text = "Purchased"
        Me.optPurchased.UseVisualStyleBackColor = True
        '
        'optNFE
        '
        Me.optNFE.AutoSize = True
        Me.optNFE.Checked = True
        Me.optNFE.Location = New System.Drawing.Point(109, 13)
        Me.optNFE.Name = "optNFE"
        Me.optNFE.Size = New System.Drawing.Size(46, 18)
        Me.optNFE.TabIndex = 70
        Me.optNFE.TabStop = True
        Me.optNFE.Text = "NFE"
        Me.optNFE.UseVisualStyleBackColor = True
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
        'chkCost
        '
        Me.chkCost.Checked = False
        Me.chkCost.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkCost.Location = New System.Drawing.Point(443, 16)
        Me.chkCost.Name = "chkCost"
        Me.chkCost.Size = New System.Drawing.Size(117, 17)
        Me.chkCost.TabIndex = 75
        Me.chkCost.Text = "Cost + Labour"
        Me.chkCost.TransparencyKey = System.Drawing.Color.Empty
        '
        'frm_DCLFantacySalesSchema
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1246, 694)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_DCLFantacySalesSchema"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Fantacy Sales Schema"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents optPurchased As System.Windows.Forms.RadioButton
    Friend WithEvents optNFE As System.Windows.Forms.RadioButton
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents txtExportNo As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents ExpProgress As System.Windows.Forms.ProgressBar
    Friend WithEvents LotID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Department As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ItemName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ProfitCenter As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WeightOrQuantity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ParcelStone As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LotName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Quantity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Weight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Shape As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Color As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ClarityID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HardTotalCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TotalCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TotalAskingPrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Sale As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RapList As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LabID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CertNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Remarks As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ClientSOCommande As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Seriename As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Customerdescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Listprice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Forevermark As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExportNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkCost As DiaStock.HazelDev_CheckBox
End Class
