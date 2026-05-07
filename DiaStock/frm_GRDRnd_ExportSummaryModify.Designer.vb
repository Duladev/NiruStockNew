<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_GRDRnd_ExportSummaryModify
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_GRDRnd_ExportSummaryModify))
        Me.pnlTitle = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlRow1 = New System.Windows.Forms.Panel()
        Me.btnrefresh = New System.Windows.Forms.Button()
        Me.lblLotNo = New System.Windows.Forms.Label()
        Me.btnnew = New System.Windows.Forms.Button()
        Me.txtLotNo = New System.Windows.Forms.TextBox()
        Me.lblTotPcs = New System.Windows.Forms.Label()
        Me.btnexcel = New System.Windows.Forms.Button()
        Me.txtPcs = New System.Windows.Forms.TextBox()
        Me.lblTotCts = New System.Windows.Forms.Label()
        Me.btnsave = New System.Windows.Forms.Button()
        Me.txtCts = New System.Windows.Forms.TextBox()
        Me.lblBuyer = New System.Windows.Forms.Label()
        Me.cmbSupplier = New System.Windows.Forms.ComboBox()
        Me.opt2 = New System.Windows.Forms.RadioButton()
        Me.opt3 = New System.Windows.Forms.RadioButton()
        Me.chkNew = New System.Windows.Forms.CheckBox()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.cmdExcel = New System.Windows.Forms.Button()
        Me.cmdNew = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.btnExportCSV = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.pnlRow2 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.lblAssort = New System.Windows.Forms.Label()
        Me.cmbAssort = New System.Windows.Forms.ComboBox()
        Me.lblSize = New System.Windows.Forms.Label()
        Me.cmbSize = New System.Windows.Forms.ComboBox()
        Me.lblNewPcs = New System.Windows.Forms.Label()
        Me.txtNewPcs = New System.Windows.Forms.TextBox()
        Me.lblNewCts = New System.Windows.Forms.Label()
        Me.txtNewCts = New System.Windows.Forms.TextBox()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.lblPackNo = New System.Windows.Forms.Label()
        Me.txtPack = New System.Windows.Forms.TextBox()
        Me.lblType = New System.Windows.Forms.Label()
        Me.txtType = New System.Windows.Forms.TextBox()
        Me.lblCategory = New System.Windows.Forms.Label()
        Me.txtCategory = New System.Windows.Forms.TextBox()
        Me.lblSupCode = New System.Windows.Forms.Label()
        Me.txtSupCode = New System.Windows.Forms.TextBox()
        Me.cmdAddPack = New System.Windows.Forms.Button()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.pnlTotals = New System.Windows.Forms.Panel()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.cmdSavePrice = New System.Windows.Forms.Button()
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
        Me.pnlTitle.Size = New System.Drawing.Size(1184, 35)
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
        Me.lblTitle.Size = New System.Drawing.Size(1184, 35)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "GRADING PACKAGE MIXING"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlRow1
        '
        Me.pnlRow1.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.pnlRow1.Controls.Add(Me.btnrefresh)
        Me.pnlRow1.Controls.Add(Me.lblLotNo)
        Me.pnlRow1.Controls.Add(Me.btnnew)
        Me.pnlRow1.Controls.Add(Me.txtLotNo)
        Me.pnlRow1.Controls.Add(Me.lblTotPcs)
        Me.pnlRow1.Controls.Add(Me.btnexcel)
        Me.pnlRow1.Controls.Add(Me.txtPcs)
        Me.pnlRow1.Controls.Add(Me.lblTotCts)
        Me.pnlRow1.Controls.Add(Me.btnsave)
        Me.pnlRow1.Controls.Add(Me.txtCts)
        Me.pnlRow1.Controls.Add(Me.lblBuyer)
        Me.pnlRow1.Controls.Add(Me.cmbSupplier)
        Me.pnlRow1.Controls.Add(Me.opt2)
        Me.pnlRow1.Controls.Add(Me.opt3)
        Me.pnlRow1.Controls.Add(Me.chkNew)
        Me.pnlRow1.Location = New System.Drawing.Point(5, 41)
        Me.pnlRow1.Name = "pnlRow1"
        Me.pnlRow1.Size = New System.Drawing.Size(1167, 58)
        Me.pnlRow1.TabIndex = 1
        '
        'btnrefresh
        '
        Me.btnrefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnrefresh.Location = New System.Drawing.Point(119, 11)
        Me.btnrefresh.Name = "btnrefresh"
        Me.btnrefresh.Size = New System.Drawing.Size(81, 31)
        Me.btnrefresh.TabIndex = 14
        Me.btnrefresh.Text = "Refresh"
        Me.btnrefresh.UseVisualStyleBackColor = False
        '
        'lblLotNo
        '
        Me.lblLotNo.BackColor = System.Drawing.Color.Transparent
        Me.lblLotNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblLotNo.Location = New System.Drawing.Point(5, 5)
        Me.lblLotNo.Name = "lblLotNo"
        Me.lblLotNo.Size = New System.Drawing.Size(55, 18)
        Me.lblLotNo.TabIndex = 0
        Me.lblLotNo.Text = "Lot No."
        '
        'btnnew
        '
        Me.btnnew.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnnew.Location = New System.Drawing.Point(304, 11)
        Me.btnnew.Name = "btnnew"
        Me.btnnew.Size = New System.Drawing.Size(81, 31)
        Me.btnnew.TabIndex = 13
        Me.btnnew.Text = "New"
        Me.btnnew.UseVisualStyleBackColor = False
        '
        'txtLotNo
        '
        Me.txtLotNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtLotNo.Location = New System.Drawing.Point(5, 22)
        Me.txtLotNo.Name = "txtLotNo"
        Me.txtLotNo.Size = New System.Drawing.Size(90, 20)
        Me.txtLotNo.TabIndex = 1
        '
        'lblTotPcs
        '
        Me.lblTotPcs.BackColor = System.Drawing.Color.Transparent
        Me.lblTotPcs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTotPcs.Location = New System.Drawing.Point(502, 13)
        Me.lblTotPcs.Name = "lblTotPcs"
        Me.lblTotPcs.Size = New System.Drawing.Size(65, 18)
        Me.lblTotPcs.TabIndex = 2
        Me.lblTotPcs.Text = "Total Pcs"
        '
        'btnexcel
        '
        Me.btnexcel.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnexcel.Location = New System.Drawing.Point(213, 11)
        Me.btnexcel.Name = "btnexcel"
        Me.btnexcel.Size = New System.Drawing.Size(81, 31)
        Me.btnexcel.TabIndex = 12
        Me.btnexcel.Text = "Excel"
        Me.btnexcel.UseVisualStyleBackColor = False
        '
        'txtPcs
        '
        Me.txtPcs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPcs.Location = New System.Drawing.Point(502, 30)
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
        Me.lblTotCts.Location = New System.Drawing.Point(582, 13)
        Me.lblTotCts.Name = "lblTotCts"
        Me.lblTotCts.Size = New System.Drawing.Size(65, 18)
        Me.lblTotCts.TabIndex = 4
        Me.lblTotCts.Text = "Total Cts"
        '
        'btnsave
        '
        Me.btnsave.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.btnsave.Location = New System.Drawing.Point(400, 11)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(81, 31)
        Me.btnsave.TabIndex = 11
        Me.btnsave.Text = "Save"
        Me.btnsave.UseVisualStyleBackColor = False
        '
        'txtCts
        '
        Me.txtCts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCts.Location = New System.Drawing.Point(582, 30)
        Me.txtCts.Name = "txtCts"
        Me.txtCts.ReadOnly = True
        Me.txtCts.Size = New System.Drawing.Size(80, 20)
        Me.txtCts.TabIndex = 5
        Me.txtCts.Text = "0"
        Me.txtCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblBuyer
        '
        Me.lblBuyer.BackColor = System.Drawing.Color.Transparent
        Me.lblBuyer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblBuyer.Location = New System.Drawing.Point(672, 13)
        Me.lblBuyer.Name = "lblBuyer"
        Me.lblBuyer.Size = New System.Drawing.Size(50, 18)
        Me.lblBuyer.TabIndex = 6
        Me.lblBuyer.Text = "Buyer"
        '
        'cmbSupplier
        '
        Me.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSupplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmbSupplier.Location = New System.Drawing.Point(672, 34)
        Me.cmbSupplier.Name = "cmbSupplier"
        Me.cmbSupplier.Size = New System.Drawing.Size(200, 21)
        Me.cmbSupplier.TabIndex = 7
        '
        'opt2
        '
        Me.opt2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.opt2.Location = New System.Drawing.Point(984, 22)
        Me.opt2.Name = "opt2"
        Me.opt2.Size = New System.Drawing.Size(50, 20)
        Me.opt2.TabIndex = 8
        Me.opt2.Text = "2nd"
        '
        'opt3
        '
        Me.opt3.Checked = True
        Me.opt3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.opt3.Location = New System.Drawing.Point(1039, 22)
        Me.opt3.Name = "opt3"
        Me.opt3.Size = New System.Drawing.Size(50, 20)
        Me.opt3.TabIndex = 9
        Me.opt3.TabStop = True
        Me.opt3.Text = "3rd"
        '
        'chkNew
        '
        Me.chkNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.chkNew.ForeColor = System.Drawing.Color.DarkBlue
        Me.chkNew.Location = New System.Drawing.Point(1097, 22)
        Me.chkNew.Name = "chkNew"
        Me.chkNew.Size = New System.Drawing.Size(55, 20)
        Me.chkNew.TabIndex = 10
        Me.chkNew.Text = "New"
        '
        'cmdRefresh
        '
        Me.cmdRefresh.Location = New System.Drawing.Point(0, 0)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(75, 23)
        Me.cmdRefresh.TabIndex = 0
        '
        'cmdExcel
        '
        Me.cmdExcel.Location = New System.Drawing.Point(0, 0)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(75, 23)
        Me.cmdExcel.TabIndex = 0
        '
        'cmdNew
        '
        Me.cmdNew.Location = New System.Drawing.Point(0, 0)
        Me.cmdNew.Name = "cmdNew"
        Me.cmdNew.Size = New System.Drawing.Size(75, 23)
        Me.cmdNew.TabIndex = 0
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(0, 0)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(75, 23)
        Me.cmdSave.TabIndex = 0
        '
        'btnExportCSV
        '
        Me.btnExportCSV.Location = New System.Drawing.Point(0, 0)
        Me.btnExportCSV.Name = "btnExportCSV"
        Me.btnExportCSV.Size = New System.Drawing.Size(75, 23)
        Me.btnExportCSV.TabIndex = 0
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(0, 0)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 0
        '
        'pnlRow2
        '
        Me.pnlRow2.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.pnlRow2.Controls.Add(Me.Button1)
        Me.pnlRow2.Controls.Add(Me.lblAssort)
        Me.pnlRow2.Controls.Add(Me.cmbAssort)
        Me.pnlRow2.Controls.Add(Me.lblSize)
        Me.pnlRow2.Controls.Add(Me.cmbSize)
        Me.pnlRow2.Controls.Add(Me.lblNewPcs)
        Me.pnlRow2.Controls.Add(Me.txtNewPcs)
        Me.pnlRow2.Controls.Add(Me.lblNewCts)
        Me.pnlRow2.Controls.Add(Me.txtNewCts)
        Me.pnlRow2.Controls.Add(Me.cmdAdd)
        Me.pnlRow2.Controls.Add(Me.lblPackNo)
        Me.pnlRow2.Controls.Add(Me.txtPack)
        Me.pnlRow2.Controls.Add(Me.lblType)
        Me.pnlRow2.Controls.Add(Me.txtType)
        Me.pnlRow2.Controls.Add(Me.lblCategory)
        Me.pnlRow2.Controls.Add(Me.txtCategory)
        Me.pnlRow2.Controls.Add(Me.lblSupCode)
        Me.pnlRow2.Controls.Add(Me.txtSupCode)
        Me.pnlRow2.Controls.Add(Me.cmdAddPack)
        Me.pnlRow2.Location = New System.Drawing.Point(5, 105)
        Me.pnlRow2.Name = "pnlRow2"
        Me.pnlRow2.Size = New System.Drawing.Size(1167, 49)
        Me.pnlRow2.TabIndex = 2
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.Button1.Location = New System.Drawing.Point(1060, 11)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(81, 31)
        Me.Button1.TabIndex = 18
        Me.Button1.Text = "Export CSV"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'lblAssort
        '
        Me.lblAssort.BackColor = System.Drawing.Color.Transparent
        Me.lblAssort.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblAssort.Location = New System.Drawing.Point(5, 3)
        Me.lblAssort.Name = "lblAssort"
        Me.lblAssort.Size = New System.Drawing.Size(85, 18)
        Me.lblAssort.TabIndex = 0
        Me.lblAssort.Text = "Assortment"
        '
        'cmbAssort
        '
        Me.cmbAssort.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmbAssort.Location = New System.Drawing.Point(5, 21)
        Me.cmbAssort.Name = "cmbAssort"
        Me.cmbAssort.Size = New System.Drawing.Size(150, 21)
        Me.cmbAssort.TabIndex = 1
        '
        'lblSize
        '
        Me.lblSize.BackColor = System.Drawing.Color.Transparent
        Me.lblSize.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblSize.Location = New System.Drawing.Point(165, 3)
        Me.lblSize.Name = "lblSize"
        Me.lblSize.Size = New System.Drawing.Size(80, 18)
        Me.lblSize.TabIndex = 2
        Me.lblSize.Text = "Size Range"
        '
        'cmbSize
        '
        Me.cmbSize.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmbSize.Location = New System.Drawing.Point(165, 21)
        Me.cmbSize.Name = "cmbSize"
        Me.cmbSize.Size = New System.Drawing.Size(100, 21)
        Me.cmbSize.TabIndex = 3
        '
        'lblNewPcs
        '
        Me.lblNewPcs.BackColor = System.Drawing.Color.Transparent
        Me.lblNewPcs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblNewPcs.Location = New System.Drawing.Point(275, 3)
        Me.lblNewPcs.Name = "lblNewPcs"
        Me.lblNewPcs.Size = New System.Drawing.Size(40, 18)
        Me.lblNewPcs.TabIndex = 4
        Me.lblNewPcs.Text = "Pcs"
        Me.lblNewPcs.Visible = False
        '
        'txtNewPcs
        '
        Me.txtNewPcs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtNewPcs.Location = New System.Drawing.Point(275, 21)
        Me.txtNewPcs.Name = "txtNewPcs"
        Me.txtNewPcs.Size = New System.Drawing.Size(60, 20)
        Me.txtNewPcs.TabIndex = 5
        '
        'lblNewCts
        '
        Me.lblNewCts.BackColor = System.Drawing.Color.Transparent
        Me.lblNewCts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblNewCts.Location = New System.Drawing.Point(345, 3)
        Me.lblNewCts.Name = "lblNewCts"
        Me.lblNewCts.Size = New System.Drawing.Size(40, 18)
        Me.lblNewCts.TabIndex = 6
        Me.lblNewCts.Text = "Cts"
        Me.lblNewCts.Visible = False
        '
        'txtNewCts
        '
        Me.txtNewCts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtNewCts.Location = New System.Drawing.Point(345, 21)
        Me.txtNewCts.Name = "txtNewCts"
        Me.txtNewCts.Size = New System.Drawing.Size(75, 20)
        Me.txtNewCts.TabIndex = 7
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmdAdd.Location = New System.Drawing.Point(430, 11)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(64, 31)
        Me.cmdAdd.TabIndex = 8
        Me.cmdAdd.Text = "Add"
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'lblPackNo
        '
        Me.lblPackNo.BackColor = System.Drawing.Color.Transparent
        Me.lblPackNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblPackNo.Location = New System.Drawing.Point(500, 3)
        Me.lblPackNo.Name = "lblPackNo"
        Me.lblPackNo.Size = New System.Drawing.Size(90, 18)
        Me.lblPackNo.TabIndex = 9
        Me.lblPackNo.Text = "Pack List No"
        '
        'txtPack
        '
        Me.txtPack.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtPack.Location = New System.Drawing.Point(500, 21)
        Me.txtPack.Name = "txtPack"
        Me.txtPack.Size = New System.Drawing.Size(70, 20)
        Me.txtPack.TabIndex = 10
        '
        'lblType
        '
        Me.lblType.BackColor = System.Drawing.Color.Transparent
        Me.lblType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblType.Location = New System.Drawing.Point(595, 3)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(40, 18)
        Me.lblType.TabIndex = 11
        Me.lblType.Text = "Type"
        '
        'txtType
        '
        Me.txtType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtType.Location = New System.Drawing.Point(580, 21)
        Me.txtType.Name = "txtType"
        Me.txtType.Size = New System.Drawing.Size(70, 20)
        Me.txtType.TabIndex = 12
        '
        'lblCategory
        '
        Me.lblCategory.BackColor = System.Drawing.Color.Transparent
        Me.lblCategory.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCategory.Location = New System.Drawing.Point(660, 3)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(65, 18)
        Me.lblCategory.TabIndex = 13
        Me.lblCategory.Text = "Category"
        '
        'txtCategory
        '
        Me.txtCategory.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCategory.Location = New System.Drawing.Point(660, 21)
        Me.txtCategory.Name = "txtCategory"
        Me.txtCategory.Size = New System.Drawing.Size(80, 20)
        Me.txtCategory.TabIndex = 14
        '
        'lblSupCode
        '
        Me.lblSupCode.BackColor = System.Drawing.Color.Transparent
        Me.lblSupCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblSupCode.Location = New System.Drawing.Point(750, 3)
        Me.lblSupCode.Name = "lblSupCode"
        Me.lblSupCode.Size = New System.Drawing.Size(70, 18)
        Me.lblSupCode.TabIndex = 15
        Me.lblSupCode.Text = "Sup Code"
        Me.lblSupCode.Visible = False
        '
        'txtSupCode
        '
        Me.txtSupCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtSupCode.Location = New System.Drawing.Point(750, 21)
        Me.txtSupCode.Name = "txtSupCode"
        Me.txtSupCode.ReadOnly = True
        Me.txtSupCode.Size = New System.Drawing.Size(70, 20)
        Me.txtSupCode.TabIndex = 16
        '
        'cmdAddPack
        '
        Me.cmdAddPack.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdAddPack.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmdAddPack.Location = New System.Drawing.Point(830, 11)
        Me.cmdAddPack.Name = "cmdAddPack"
        Me.cmdAddPack.Size = New System.Drawing.Size(83, 31)
        Me.cmdAddPack.TabIndex = 17
        Me.cmdAddPack.Text = "Add Pack"
        Me.cmdAddPack.UseVisualStyleBackColor = False
        '
        'flxDetails
        '
        Me.flxDetails.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flxDetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.flxDetails.Location = New System.Drawing.Point(5, 160)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.Size = New System.Drawing.Size(1167, 491)
        Me.flxDetails.TabIndex = 3
        '
        'pnlTotals
        '
        Me.pnlTotals.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlTotals.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.pnlTotals.Controls.Add(Me.cmdUpdate)
        Me.pnlTotals.Controls.Add(Me.cmdSavePrice)
        Me.pnlTotals.Controls.Add(Me.lblTotPcsB)
        Me.pnlTotals.Controls.Add(Me.txtTotPcs)
        Me.pnlTotals.Controls.Add(Me.lblTotCtsB)
        Me.pnlTotals.Controls.Add(Me.txtTotCts)
        Me.pnlTotals.Location = New System.Drawing.Point(4, 657)
        Me.pnlTotals.Name = "pnlTotals"
        Me.pnlTotals.Size = New System.Drawing.Size(1168, 43)
        Me.pnlTotals.TabIndex = 4
        '
        'cmdUpdate
        '
        Me.cmdUpdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmdUpdate.Location = New System.Drawing.Point(5, 4)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(90, 36)
        Me.cmdUpdate.TabIndex = 0
        Me.cmdUpdate.Text = "Update Price"
        Me.cmdUpdate.UseVisualStyleBackColor = False
        '
        'cmdSavePrice
        '
        Me.cmdSavePrice.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSavePrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmdSavePrice.Location = New System.Drawing.Point(100, 4)
        Me.cmdSavePrice.Name = "cmdSavePrice"
        Me.cmdSavePrice.Size = New System.Drawing.Size(105, 36)
        Me.cmdSavePrice.TabIndex = 1
        Me.cmdSavePrice.Text = "Save New Price"
        Me.cmdSavePrice.UseVisualStyleBackColor = False
        '
        'lblTotPcsB
        '
        Me.lblTotPcsB.BackColor = System.Drawing.Color.Transparent
        Me.lblTotPcsB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTotPcsB.Location = New System.Drawing.Point(215, 7)
        Me.lblTotPcsB.Name = "lblTotPcsB"
        Me.lblTotPcsB.Size = New System.Drawing.Size(60, 18)
        Me.lblTotPcsB.TabIndex = 2
        Me.lblTotPcsB.Text = "Tot Pcs:"
        '
        'txtTotPcs
        '
        Me.txtTotPcs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtTotPcs.Location = New System.Drawing.Point(280, 5)
        Me.txtTotPcs.Name = "txtTotPcs"
        Me.txtTotPcs.ReadOnly = True
        Me.txtTotPcs.Size = New System.Drawing.Size(70, 20)
        Me.txtTotPcs.TabIndex = 3
        Me.txtTotPcs.Text = "0"
        Me.txtTotPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblTotCtsB
        '
        Me.lblTotCtsB.BackColor = System.Drawing.Color.Transparent
        Me.lblTotCtsB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTotCtsB.Location = New System.Drawing.Point(360, 7)
        Me.lblTotCtsB.Name = "lblTotCtsB"
        Me.lblTotCtsB.Size = New System.Drawing.Size(60, 18)
        Me.lblTotCtsB.TabIndex = 4
        Me.lblTotCtsB.Text = "Tot Cts:"
        '
        'txtTotCts
        '
        Me.txtTotCts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtTotCts.Location = New System.Drawing.Point(425, 5)
        Me.txtTotCts.Name = "txtTotCts"
        Me.txtTotCts.ReadOnly = True
        Me.txtTotCts.Size = New System.Drawing.Size(80, 20)
        Me.txtTotCts.TabIndex = 5
        Me.txtTotCts.Text = "0"
        Me.txtTotCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'frm_GRDRnd_ExportSummaryModify
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1184, 701)
        Me.Controls.Add(Me.pnlTitle)
        Me.Controls.Add(Me.pnlRow1)
        Me.Controls.Add(Me.pnlRow2)
        Me.Controls.Add(Me.flxDetails)
        Me.Controls.Add(Me.pnlTotals)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_GRDRnd_ExportSummaryModify"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GRADING PACKAGE MIXING"
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
    Friend WithEvents lblLotNo As System.Windows.Forms.Label
    Friend WithEvents txtLotNo As System.Windows.Forms.TextBox
    Friend WithEvents lblTotPcs As System.Windows.Forms.Label
    Friend WithEvents txtPcs As System.Windows.Forms.TextBox
    Friend WithEvents lblTotCts As System.Windows.Forms.Label
    Friend WithEvents txtCts As System.Windows.Forms.TextBox
    Friend WithEvents lblBuyer As System.Windows.Forms.Label
    Friend WithEvents cmbSupplier As System.Windows.Forms.ComboBox
    Friend WithEvents opt2 As System.Windows.Forms.RadioButton
    Friend WithEvents opt3 As System.Windows.Forms.RadioButton
    Friend WithEvents chkNew As System.Windows.Forms.CheckBox
    Friend WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents cmdExcel As System.Windows.Forms.Button
    Friend WithEvents cmdNew As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents btnExportCSV As System.Windows.Forms.Button
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
    Friend WithEvents lblPackNo As System.Windows.Forms.Label
    Friend WithEvents txtPack As System.Windows.Forms.TextBox
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents txtType As System.Windows.Forms.TextBox
    Friend WithEvents lblCategory As System.Windows.Forms.Label
    Friend WithEvents txtCategory As System.Windows.Forms.TextBox
    Friend WithEvents lblSupCode As System.Windows.Forms.Label
    Friend WithEvents txtSupCode As System.Windows.Forms.TextBox
    Friend WithEvents cmdAddPack As System.Windows.Forms.Button
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents pnlTotals As System.Windows.Forms.Panel
    Friend WithEvents cmdUpdate As System.Windows.Forms.Button
    Friend WithEvents cmdSavePrice As System.Windows.Forms.Button
    Friend WithEvents lblTotPcsB As System.Windows.Forms.Label
    Friend WithEvents txtTotPcs As System.Windows.Forms.TextBox
    Friend WithEvents lblTotCtsB As System.Windows.Forms.Label
    Friend WithEvents txtTotCts As System.Windows.Forms.TextBox
    Friend WithEvents btnsave As Button
    Friend WithEvents btnexcel As Button
    Friend WithEvents btnnew As Button
    Friend WithEvents btnrefresh As Button
    Friend WithEvents Button1 As Button
End Class