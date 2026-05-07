<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DCLInvoice
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.cmbDescription = New System.Windows.Forms.ComboBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.optCons3 = New DiaStock.HazelDev_RadioButton()
        Me.optCons2 = New DiaStock.HazelDev_RadioButton()
        Me.chkRussiaR = New DiaStock.HazelDev_CheckBox()
        Me.chkRussiaP = New DiaStock.HazelDev_CheckBox()
        Me.cmbCat = New System.Windows.Forms.ComboBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.cmbType = New System.Windows.Forms.ComboBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.cmbInsure = New System.Windows.Forms.ComboBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtTotPolVal = New System.Windows.Forms.TextBox()
        Me.chkMax = New DiaStock.HazelDev_CheckBox()
        Me.optRough = New DiaStock.HazelDev_RadioButton()
        Me.chkTerms = New DiaStock.HazelDev_CheckBox()
        Me.optCom = New DiaStock.HazelDev_RadioButton()
        Me.optConPol = New DiaStock.HazelDev_RadioButton()
        Me.optConRgh = New DiaStock.HazelDev_RadioButton()
        Me.optCons = New DiaStock.HazelDev_RadioButton()
        Me.chkCost = New DiaStock.HazelDev_CheckBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtDelInvNo = New System.Windows.Forms.TextBox()
        Me.txtBuyerCode = New System.Windows.Forms.TextBox()
        Me.txtSupCode = New System.Windows.Forms.TextBox()
        Me.txtLabor = New System.Windows.Forms.TextBox()
        Me.txtCompany = New System.Windows.Forms.TextBox()
        Me.txtNFE = New System.Windows.Forms.TextBox()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.txtSubTotal = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtFrChg = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.flxBOI = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BOIValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SupRefNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtExpNo = New System.Windows.Forms.TextBox()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.txtIns = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtPPNo = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtFwdChg = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtUSD = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbPayBy = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbShipTo = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbBank = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbCarrier = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbBuyer = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbSupplierCode = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtTotalCts = New System.Windows.Forms.TextBox()
        Me.txtTotalPcs = New System.Windows.Forms.TextBox()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DepartmentName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Company = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InvCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Price = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LotID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Article = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Remarks = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ItemName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Urgent = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NewAssort = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HardCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SelectCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdExcel = New DiaStock.HazelDev_Button()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.cmdDelete = New DiaStock.HazelDev_Button()
        Me.cmdReport = New DiaStock.HazelDev_Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.dtpExpInvDate = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbCompany = New System.Windows.Forms.ComboBox()
        Me.cmbCategory = New System.Windows.Forms.ComboBox()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        CType(Me.flxBOI, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbCategory)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbDescription)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label25)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.optCons3)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.optCons2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkRussiaR)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkRussiaP)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbCat)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label24)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbType)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label23)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbInsure)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label22)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotPolVal)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkMax)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.optRough)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkTerms)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.optCom)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.optConPol)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.optConRgh)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.optCons)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.chkCost)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label21)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label20)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label19)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label18)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label17)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label16)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label15)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtDelInvNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtBuyerCode)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtSupCode)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtLabor)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtCompany)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtNFE)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotal)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtSubTotal)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label14)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtFrChg)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label12)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxBOI)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtExpNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtInvoiceNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtIns)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label13)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPPNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label10)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtFwdChg)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label9)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtUSD)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label6)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbPayBy)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label5)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbShipTo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label4)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbBank)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label3)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbCarrier)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbBuyer)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbSupplierCode)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label11)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotalCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotalPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxDetails)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label8)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.dtpExpInvDate)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label7)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbCompany)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(913, 666)
        Me.HazelDev_ThemeContainer1.TabIndex = 1
        Me.HazelDev_ThemeContainer1.Text = "INVOICE"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmbDescription
        '
        Me.cmbDescription.FormattingEnabled = True
        Me.cmbDescription.IntegralHeight = False
        Me.cmbDescription.Items.AddRange(New Object() {"Diamond Sales", "Labor Sales", "Consignment Return", "Contract", "Consignment", "Return"})
        Me.cmbDescription.Location = New System.Drawing.Point(672, 262)
        Me.cmbDescription.Name = "cmbDescription"
        Me.cmbDescription.Size = New System.Drawing.Size(229, 23)
        Me.cmbDescription.TabIndex = 115
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Location = New System.Drawing.Point(564, 262)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(69, 15)
        Me.Label25.TabIndex = 114
        Me.Label25.Text = "Description"
        '
        'optCons3
        '
        Me.optCons3.BackColor = System.Drawing.Color.White
        Me.optCons3.Checked = False
        Me.optCons3.Location = New System.Drawing.Point(563, 632)
        Me.optCons3.Name = "optCons3"
        Me.optCons3.Size = New System.Drawing.Size(100, 19)
        Me.optCons3.TabIndex = 113
        Me.optCons3.Text = "Consignment 3"
        Me.optCons3.TransparencyKey = System.Drawing.Color.Empty
        '
        'optCons2
        '
        Me.optCons2.BackColor = System.Drawing.Color.White
        Me.optCons2.Checked = False
        Me.optCons2.Location = New System.Drawing.Point(780, 632)
        Me.optCons2.Name = "optCons2"
        Me.optCons2.Size = New System.Drawing.Size(100, 19)
        Me.optCons2.TabIndex = 112
        Me.optCons2.Text = "Consignment 2"
        Me.optCons2.TransparencyKey = System.Drawing.Color.Empty
        '
        'chkRussiaR
        '
        Me.chkRussiaR.Checked = False
        Me.chkRussiaR.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkRussiaR.Location = New System.Drawing.Point(668, 578)
        Me.chkRussiaR.Name = "chkRussiaR"
        Me.chkRussiaR.Size = New System.Drawing.Size(100, 25)
        Me.chkRussiaR.TabIndex = 111
        Me.chkRussiaR.Text = "Russian R"
        Me.chkRussiaR.TransparencyKey = System.Drawing.Color.Empty
        '
        'chkRussiaP
        '
        Me.chkRussiaP.Checked = False
        Me.chkRussiaP.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkRussiaP.Location = New System.Drawing.Point(563, 578)
        Me.chkRussiaP.Name = "chkRussiaP"
        Me.chkRussiaP.Size = New System.Drawing.Size(93, 25)
        Me.chkRussiaP.TabIndex = 110
        Me.chkRussiaP.Text = "Russian P"
        Me.chkRussiaP.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmbCat
        '
        Me.cmbCat.FormattingEnabled = True
        Me.cmbCat.IntegralHeight = False
        Me.cmbCat.Items.AddRange(New Object() {"Diamond Sales", "Labor Sales", "Consignment Return", "Contract", "Consignment", "Return"})
        Me.cmbCat.Location = New System.Drawing.Point(355, 262)
        Me.cmbCat.Name = "cmbCat"
        Me.cmbCat.Size = New System.Drawing.Size(188, 23)
        Me.cmbCat.TabIndex = 109
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Location = New System.Drawing.Point(281, 262)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(55, 15)
        Me.Label24.TabIndex = 108
        Me.Label24.Text = "Category"
        '
        'cmbType
        '
        Me.cmbType.FormattingEnabled = True
        Me.cmbType.IntegralHeight = False
        Me.cmbType.Items.AddRange(New Object() {"Rough", "Polished", "Natural Rough"})
        Me.cmbType.Location = New System.Drawing.Point(82, 262)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Size = New System.Drawing.Size(188, 23)
        Me.cmbType.TabIndex = 107
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Location = New System.Drawing.Point(8, 262)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(33, 15)
        Me.Label23.TabIndex = 106
        Me.Label23.Text = "Type"
        '
        'cmbInsure
        '
        Me.cmbInsure.FormattingEnabled = True
        Me.cmbInsure.IntegralHeight = False
        Me.cmbInsure.Location = New System.Drawing.Point(176, 202)
        Me.cmbInsure.Name = "cmbInsure"
        Me.cmbInsure.Size = New System.Drawing.Size(94, 23)
        Me.cmbInsure.TabIndex = 105
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Location = New System.Drawing.Point(560, 541)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(105, 15)
        Me.Label22.TabIndex = 103
        Me.Label22.Text = "Total Polish Value"
        '
        'txtTotPolVal
        '
        Me.txtTotPolVal.Location = New System.Drawing.Point(669, 541)
        Me.txtTotPolVal.Name = "txtTotPolVal"
        Me.txtTotPolVal.ReadOnly = True
        Me.txtTotPolVal.Size = New System.Drawing.Size(102, 21)
        Me.txtTotPolVal.TabIndex = 102
        Me.txtTotPolVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkMax
        '
        Me.chkMax.Checked = False
        Me.chkMax.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkMax.Location = New System.Drawing.Point(467, 233)
        Me.chkMax.Name = "chkMax"
        Me.chkMax.Size = New System.Drawing.Size(90, 25)
        Me.chkMax.TabIndex = 101
        Me.chkMax.Text = "Max Cost"
        Me.chkMax.TransparencyKey = System.Drawing.Color.Empty
        '
        'optRough
        '
        Me.optRough.BackColor = System.Drawing.Color.White
        Me.optRough.Checked = False
        Me.optRough.Location = New System.Drawing.Point(780, 607)
        Me.optRough.Name = "optRough"
        Me.optRough.Size = New System.Drawing.Size(88, 19)
        Me.optRough.TabIndex = 100
        Me.optRough.Text = "Rough Sales"
        Me.optRough.TransparencyKey = System.Drawing.Color.Empty
        '
        'chkTerms
        '
        Me.chkTerms.Checked = False
        Me.chkTerms.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkTerms.Location = New System.Drawing.Point(780, 472)
        Me.chkTerms.Name = "chkTerms"
        Me.chkTerms.Size = New System.Drawing.Size(106, 25)
        Me.chkTerms.TabIndex = 99
        Me.chkTerms.Text = "Terms"
        Me.chkTerms.TransparencyKey = System.Drawing.Color.Empty
        '
        'optCom
        '
        Me.optCom.BackColor = System.Drawing.Color.White
        Me.optCom.Checked = False
        Me.optCom.Location = New System.Drawing.Point(780, 510)
        Me.optCom.Name = "optCom"
        Me.optCom.Size = New System.Drawing.Size(78, 19)
        Me.optCom.TabIndex = 98
        Me.optCom.Text = "Purchased"
        Me.optCom.TransparencyKey = System.Drawing.Color.Empty
        '
        'optConPol
        '
        Me.optConPol.BackColor = System.Drawing.Color.White
        Me.optConPol.Checked = False
        Me.optConPol.Location = New System.Drawing.Point(780, 560)
        Me.optConPol.Name = "optConPol"
        Me.optConPol.Size = New System.Drawing.Size(86, 19)
        Me.optConPol.TabIndex = 97
        Me.optConPol.Text = "Contract Pol"
        Me.optConPol.TransparencyKey = System.Drawing.Color.Empty
        '
        'optConRgh
        '
        Me.optConRgh.BackColor = System.Drawing.Color.White
        Me.optConRgh.Checked = False
        Me.optConRgh.Location = New System.Drawing.Point(780, 535)
        Me.optConRgh.Name = "optConRgh"
        Me.optConRgh.Size = New System.Drawing.Size(90, 19)
        Me.optConRgh.TabIndex = 96
        Me.optConRgh.Text = "Contract Rgh"
        Me.optConRgh.TransparencyKey = System.Drawing.Color.Empty
        '
        'optCons
        '
        Me.optCons.BackColor = System.Drawing.Color.White
        Me.optCons.Checked = False
        Me.optCons.Location = New System.Drawing.Point(780, 582)
        Me.optCons.Name = "optCons"
        Me.optCons.Size = New System.Drawing.Size(90, 19)
        Me.optCons.TabIndex = 95
        Me.optCons.Text = "Consignment"
        Me.optCons.TransparencyKey = System.Drawing.Color.Empty
        '
        'chkCost
        '
        Me.chkCost.Checked = False
        Me.chkCost.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.chkCost.Location = New System.Drawing.Point(355, 233)
        Me.chkCost.Name = "chkCost"
        Me.chkCost.Size = New System.Drawing.Size(106, 25)
        Me.chkCost.TabIndex = 76
        Me.chkCost.Text = "NFE + Labour"
        Me.chkCost.TransparencyKey = System.Drawing.Color.Empty
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Location = New System.Drawing.Point(560, 508)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(34, 15)
        Me.Label21.TabIndex = 93
        Me.Label21.Text = "Total"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Location = New System.Drawing.Point(560, 472)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(59, 15)
        Me.Label20.TabIndex = 92
        Me.Label20.Text = "Sub Total"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Location = New System.Drawing.Point(356, 621)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(64, 15)
        Me.Label19.TabIndex = 91
        Me.Label19.Text = "Invoice No"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Location = New System.Drawing.Point(356, 582)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(46, 15)
        Me.Label18.TabIndex = 90
        Me.Label18.Text = "Labour"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Location = New System.Drawing.Point(356, 544)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(31, 15)
        Me.Label17.TabIndex = 89
        Me.Label17.Text = "NFE"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Location = New System.Drawing.Point(356, 508)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(54, 15)
        Me.Label16.TabIndex = 88
        Me.Label16.Text = "Total Cts"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Location = New System.Drawing.Point(356, 472)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(57, 15)
        Me.Label15.TabIndex = 87
        Me.Label15.Text = "Total Pcs"
        '
        'txtDelInvNo
        '
        Me.txtDelInvNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDelInvNo.Location = New System.Drawing.Point(441, 624)
        Me.txtDelInvNo.Name = "txtDelInvNo"
        Me.txtDelInvNo.Size = New System.Drawing.Size(102, 21)
        Me.txtDelInvNo.TabIndex = 85
        Me.txtDelInvNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtBuyerCode
        '
        Me.txtBuyerCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBuyerCode.Location = New System.Drawing.Point(779, 202)
        Me.txtBuyerCode.Name = "txtBuyerCode"
        Me.txtBuyerCode.ReadOnly = True
        Me.txtBuyerCode.Size = New System.Drawing.Size(122, 21)
        Me.txtBuyerCode.TabIndex = 84
        '
        'txtSupCode
        '
        Me.txtSupCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupCode.Location = New System.Drawing.Point(779, 171)
        Me.txtSupCode.Name = "txtSupCode"
        Me.txtSupCode.ReadOnly = True
        Me.txtSupCode.Size = New System.Drawing.Size(122, 21)
        Me.txtSupCode.TabIndex = 83
        '
        'txtLabor
        '
        Me.txtLabor.Location = New System.Drawing.Point(441, 582)
        Me.txtLabor.Name = "txtLabor"
        Me.txtLabor.ReadOnly = True
        Me.txtLabor.Size = New System.Drawing.Size(102, 21)
        Me.txtLabor.TabIndex = 82
        Me.txtLabor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCompany
        '
        Me.txtCompany.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompany.Location = New System.Drawing.Point(780, 142)
        Me.txtCompany.Name = "txtCompany"
        Me.txtCompany.ReadOnly = True
        Me.txtCompany.Size = New System.Drawing.Size(122, 21)
        Me.txtCompany.TabIndex = 75
        '
        'txtNFE
        '
        Me.txtNFE.Location = New System.Drawing.Point(441, 544)
        Me.txtNFE.Name = "txtNFE"
        Me.txtNFE.ReadOnly = True
        Me.txtNFE.Size = New System.Drawing.Size(102, 21)
        Me.txtNFE.TabIndex = 81
        Me.txtNFE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotal
        '
        Me.txtTotal.Location = New System.Drawing.Point(669, 508)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(102, 21)
        Me.txtTotal.TabIndex = 80
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSubTotal
        '
        Me.txtSubTotal.Location = New System.Drawing.Point(669, 472)
        Me.txtSubTotal.Name = "txtSubTotal"
        Me.txtSubTotal.ReadOnly = True
        Me.txtSubTotal.Size = New System.Drawing.Size(102, 21)
        Me.txtSubTotal.TabIndex = 79
        Me.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Location = New System.Drawing.Point(281, 204)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(61, 15)
        Me.Label14.TabIndex = 78
        Me.Label14.Text = "Export No"
        '
        'txtFrChg
        '
        Me.txtFrChg.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFrChg.Location = New System.Drawing.Point(672, 233)
        Me.txtFrChg.Name = "txtFrChg"
        Me.txtFrChg.Size = New System.Drawing.Size(102, 21)
        Me.txtFrChg.TabIndex = 77
        Me.txtFrChg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Location = New System.Drawing.Point(563, 233)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(45, 15)
        Me.Label12.TabIndex = 76
        Me.Label12.Text = "Freight"
        '
        'flxBOI
        '
        Me.flxBOI.AllowUserToAddRows = False
        Me.flxBOI.AllowUserToDeleteRows = False
        Me.flxBOI.AllowUserToResizeColumns = False
        Me.flxBOI.AllowUserToResizeRows = False
        Me.flxBOI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxBOI.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn13, Me.BOIValue, Me.SupRefNo})
        Me.flxBOI.Location = New System.Drawing.Point(7, 470)
        Me.flxBOI.Name = "flxBOI"
        Me.flxBOI.ReadOnly = True
        Me.flxBOI.RowHeadersVisible = False
        Me.flxBOI.Size = New System.Drawing.Size(327, 184)
        Me.flxBOI.TabIndex = 75
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.HeaderText = "BOI No"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        '
        'BOIValue
        '
        Me.BOIValue.HeaderText = "BOI Value"
        Me.BOIValue.Name = "BOIValue"
        Me.BOIValue.ReadOnly = True
        '
        'SupRefNo
        '
        Me.SupRefNo.HeaderText = "Sup Ref No"
        Me.SupRefNo.Name = "SupRefNo"
        Me.SupRefNo.ReadOnly = True
        '
        'txtExpNo
        '
        Me.txtExpNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpNo.Location = New System.Drawing.Point(355, 202)
        Me.txtExpNo.Name = "txtExpNo"
        Me.txtExpNo.Size = New System.Drawing.Size(122, 21)
        Me.txtExpNo.TabIndex = 72
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceNo.Location = New System.Drawing.Point(780, 110)
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(121, 21)
        Me.txtInvoiceNo.TabIndex = 67
        Me.txtInvoiceNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtIns
        '
        Me.txtIns.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIns.Location = New System.Drawing.Point(672, 202)
        Me.txtIns.Name = "txtIns"
        Me.txtIns.Size = New System.Drawing.Size(102, 21)
        Me.txtIns.TabIndex = 71
        Me.txtIns.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Location = New System.Drawing.Point(563, 204)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(61, 15)
        Me.Label13.TabIndex = 70
        Me.Label13.Text = "Insurance"
        '
        'txtPPNo
        '
        Me.txtPPNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPPNo.Location = New System.Drawing.Point(355, 174)
        Me.txtPPNo.Name = "txtPPNo"
        Me.txtPPNo.Size = New System.Drawing.Size(188, 21)
        Me.txtPPNo.TabIndex = 69
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(281, 174)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(55, 15)
        Me.Label10.TabIndex = 68
        Me.Label10.Text = "Passport"
        '
        'txtFwdChg
        '
        Me.txtFwdChg.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFwdChg.Location = New System.Drawing.Point(672, 171)
        Me.txtFwdChg.Name = "txtFwdChg"
        Me.txtFwdChg.Size = New System.Drawing.Size(102, 21)
        Me.txtFwdChg.TabIndex = 67
        Me.txtFwdChg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(563, 174)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(69, 15)
        Me.Label9.TabIndex = 66
        Me.Label9.Text = "Forwarding"
        '
        'txtUSD
        '
        Me.txtUSD.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUSD.Location = New System.Drawing.Point(672, 142)
        Me.txtUSD.Name = "txtUSD"
        Me.txtUSD.Size = New System.Drawing.Size(102, 21)
        Me.txtUSD.TabIndex = 65
        Me.txtUSD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(563, 142)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 15)
        Me.Label6.TabIndex = 64
        Me.Label6.Text = "USD Rate"
        '
        'cmbPayBy
        '
        Me.cmbPayBy.FormattingEnabled = True
        Me.cmbPayBy.IntegralHeight = False
        Me.cmbPayBy.Location = New System.Drawing.Point(355, 141)
        Me.cmbPayBy.Name = "cmbPayBy"
        Me.cmbPayBy.Size = New System.Drawing.Size(188, 23)
        Me.cmbPayBy.TabIndex = 63
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(281, 141)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 15)
        Me.Label5.TabIndex = 62
        Me.Label5.Text = "Pay By"
        '
        'cmbShipTo
        '
        Me.cmbShipTo.FormattingEnabled = True
        Me.cmbShipTo.IntegralHeight = False
        Me.cmbShipTo.Location = New System.Drawing.Point(355, 112)
        Me.cmbShipTo.Name = "cmbShipTo"
        Me.cmbShipTo.Size = New System.Drawing.Size(188, 23)
        Me.cmbShipTo.TabIndex = 61
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(281, 112)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 15)
        Me.Label4.TabIndex = 60
        Me.Label4.Text = "Ship To"
        '
        'cmbBank
        '
        Me.cmbBank.FormattingEnabled = True
        Me.cmbBank.IntegralHeight = False
        Me.cmbBank.Location = New System.Drawing.Point(82, 233)
        Me.cmbBank.Name = "cmbBank"
        Me.cmbBank.Size = New System.Drawing.Size(188, 23)
        Me.cmbBank.TabIndex = 59
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(8, 233)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 15)
        Me.Label3.TabIndex = 58
        Me.Label3.Text = "Bank"
        '
        'cmbCarrier
        '
        Me.cmbCarrier.FormattingEnabled = True
        Me.cmbCarrier.IntegralHeight = False
        Me.cmbCarrier.Location = New System.Drawing.Point(82, 202)
        Me.cmbCarrier.Name = "cmbCarrier"
        Me.cmbCarrier.Size = New System.Drawing.Size(88, 23)
        Me.cmbCarrier.TabIndex = 57
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(8, 204)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 15)
        Me.Label2.TabIndex = 56
        Me.Label2.Text = "Ship via"
        '
        'cmbBuyer
        '
        Me.cmbBuyer.FormattingEnabled = True
        Me.cmbBuyer.IntegralHeight = False
        Me.cmbBuyer.Location = New System.Drawing.Point(82, 171)
        Me.cmbBuyer.Name = "cmbBuyer"
        Me.cmbBuyer.Size = New System.Drawing.Size(188, 23)
        Me.cmbBuyer.TabIndex = 55
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(8, 171)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 15)
        Me.Label1.TabIndex = 54
        Me.Label1.Text = "Buyer"
        '
        'cmbSupplierCode
        '
        Me.cmbSupplierCode.FormattingEnabled = True
        Me.cmbSupplierCode.IntegralHeight = False
        Me.cmbSupplierCode.Location = New System.Drawing.Point(82, 142)
        Me.cmbSupplierCode.Name = "cmbSupplierCode"
        Me.cmbSupplierCode.Size = New System.Drawing.Size(188, 23)
        Me.cmbSupplierCode.TabIndex = 53
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Location = New System.Drawing.Point(8, 142)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(60, 15)
        Me.Label11.TabIndex = 52
        Me.Label11.Text = "Customer"
        '
        'txtTotalCts
        '
        Me.txtTotalCts.Location = New System.Drawing.Point(441, 508)
        Me.txtTotalCts.Name = "txtTotalCts"
        Me.txtTotalCts.ReadOnly = True
        Me.txtTotalCts.Size = New System.Drawing.Size(102, 21)
        Me.txtTotalCts.TabIndex = 49
        Me.txtTotalCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalPcs
        '
        Me.txtTotalPcs.Location = New System.Drawing.Point(441, 472)
        Me.txtTotalPcs.Name = "txtTotalPcs"
        Me.txtTotalPcs.ReadOnly = True
        Me.txtTotalPcs.Size = New System.Drawing.Size(102, 21)
        Me.txtTotalPcs.TabIndex = 48
        Me.txtTotalPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Code, Me.DepartmentName, Me.Company, Me.InvCts, Me.Price, Me.LotID, Me.Article, Me.Remarks, Me.ItemName, Me.Urgent, Me.NewAssort, Me.HardCost, Me.SelectCost, Me.Column1})
        Me.flxDetails.Location = New System.Drawing.Point(7, 312)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(895, 152)
        Me.flxDetails.TabIndex = 43
        '
        'Code
        '
        Me.Code.HeaderText = "Index"
        Me.Code.Name = "Code"
        Me.Code.ReadOnly = True
        Me.Code.Width = 50
        '
        'DepartmentName
        '
        Me.DepartmentName.HeaderText = "Pack"
        Me.DepartmentName.Name = "DepartmentName"
        '
        'Company
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Company.DefaultCellStyle = DataGridViewCellStyle4
        Me.Company.HeaderText = "Pcs"
        Me.Company.Name = "Company"
        '
        'InvCts
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.InvCts.DefaultCellStyle = DataGridViewCellStyle5
        Me.InvCts.HeaderText = "Cts"
        Me.InvCts.Name = "InvCts"
        '
        'Price
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Price.DefaultCellStyle = DataGridViewCellStyle6
        Me.Price.HeaderText = "Labour"
        Me.Price.Name = "Price"
        '
        'LotID
        '
        Me.LotID.HeaderText = "NFE"
        Me.LotID.Name = "LotID"
        '
        'Article
        '
        Me.Article.HeaderText = "Sub Total"
        Me.Article.Name = "Article"
        Me.Article.ReadOnly = True
        '
        'Remarks
        '
        Me.Remarks.HeaderText = "Export No"
        Me.Remarks.Name = "Remarks"
        Me.Remarks.ReadOnly = True
        '
        'ItemName
        '
        Me.ItemName.HeaderText = "Type"
        Me.ItemName.Name = "ItemName"
        '
        'Urgent
        '
        Me.Urgent.HeaderText = "Sales Rate"
        Me.Urgent.Name = "Urgent"
        Me.Urgent.ReadOnly = True
        Me.Urgent.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Urgent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'NewAssort
        '
        Me.NewAssort.HeaderText = "Polish Value"
        Me.NewAssort.Name = "NewAssort"
        '
        'HardCost
        '
        Me.HardCost.HeaderText = "Rgh Cts"
        Me.HardCost.Name = "HardCost"
        Me.HardCost.ReadOnly = True
        '
        'SelectCost
        '
        Me.SelectCost.HeaderText = "ID"
        Me.SelectCost.Name = "SelectCost"
        '
        'Column1
        '
        Me.Column1.HeaderText = "Hard Value"
        Me.Column1.Name = "Column1"
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExcel)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdDelete)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdReport)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(899, 50)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
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
        Me.cmdExcel.Location = New System.Drawing.Point(784, 13)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(99, 28)
        Me.cmdExcel.TabIndex = 46
        Me.cmdExcel.Text = "Excel"
        Me.cmdExcel.UseVisualStyleBackColor = False
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
        Me.cmdNew.Text = "New"
        Me.cmdNew.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdDelete.FlatAppearance.BorderSize = 0
        Me.cmdDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.ForeColor = System.Drawing.Color.White
        Me.cmdDelete.Location = New System.Drawing.Point(554, 13)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(99, 28)
        Me.cmdDelete.TabIndex = 86
        Me.cmdDelete.Text = "Delete"
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'cmdReport
        '
        Me.cmdReport.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdReport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdReport.FlatAppearance.BorderSize = 0
        Me.cmdReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdReport.ForeColor = System.Drawing.Color.White
        Me.cmdReport.Location = New System.Drawing.Point(669, 13)
        Me.cmdReport.Name = "cmdReport"
        Me.cmdReport.Size = New System.Drawing.Size(99, 28)
        Me.cmdReport.TabIndex = 46
        Me.cmdReport.Text = "Print"
        Me.cmdReport.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(563, 110)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(82, 15)
        Me.Label8.TabIndex = 24
        Me.Label8.Text = "Shipped Date"
        '
        'dtpExpInvDate
        '
        Me.dtpExpInvDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpExpInvDate.Location = New System.Drawing.Point(672, 110)
        Me.dtpExpInvDate.Name = "dtpExpInvDate"
        Me.dtpExpInvDate.Size = New System.Drawing.Size(102, 21)
        Me.dtpExpInvDate.TabIndex = 23
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(8, 110)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(59, 15)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = "Company"
        '
        'cmbCompany
        '
        Me.cmbCompany.FormattingEnabled = True
        Me.cmbCompany.IntegralHeight = False
        Me.cmbCompany.Location = New System.Drawing.Point(82, 110)
        Me.cmbCompany.Name = "cmbCompany"
        Me.cmbCompany.Size = New System.Drawing.Size(188, 23)
        Me.cmbCompany.TabIndex = 6
        '
        'cmbCategory
        '
        Me.cmbCategory.FormattingEnabled = True
        Me.cmbCategory.IntegralHeight = False
        Me.cmbCategory.Items.AddRange(New Object() {"NFE", "Purchased", "Consignment"})
        Me.cmbCategory.Location = New System.Drawing.Point(779, 233)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(123, 23)
        Me.cmbCategory.TabIndex = 116
        '
        'frm_DCLInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(913, 666)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_DCLInvoice"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Invoice"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        CType(Me.flxBOI, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents txtTotalCts As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalPcs As System.Windows.Forms.TextBox
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdReport As DiaStock.HazelDev_Button
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents dtpExpInvDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbCompany As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSupplierCode As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmbBuyer As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbCarrier As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbBank As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbShipTo As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbPayBy As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtFwdChg As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtUSD As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtIns As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtPPNo As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents txtExpNo As System.Windows.Forms.TextBox
    Friend WithEvents txtCompany As System.Windows.Forms.TextBox
    Friend WithEvents chkCost As DiaStock.HazelDev_CheckBox
    Friend WithEvents flxBOI As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BOIValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SupRefNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtFrChg As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtSubTotal As System.Windows.Forms.TextBox
    Friend WithEvents txtLabor As System.Windows.Forms.TextBox
    Friend WithEvents txtNFE As System.Windows.Forms.TextBox
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents txtBuyerCode As System.Windows.Forms.TextBox
    Friend WithEvents txtSupCode As System.Windows.Forms.TextBox
    Friend WithEvents txtDelInvNo As System.Windows.Forms.TextBox
    Friend WithEvents cmdDelete As DiaStock.HazelDev_Button
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents optConPol As DiaStock.HazelDev_RadioButton
    Friend WithEvents optConRgh As DiaStock.HazelDev_RadioButton
    Friend WithEvents optCons As DiaStock.HazelDev_RadioButton
    Friend WithEvents optCom As DiaStock.HazelDev_RadioButton
    Friend WithEvents chkTerms As DiaStock.HazelDev_CheckBox
    Friend WithEvents optRough As DiaStock.HazelDev_RadioButton
    Friend WithEvents chkMax As DiaStock.HazelDev_CheckBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtTotPolVal As System.Windows.Forms.TextBox
    Friend WithEvents cmbInsure As System.Windows.Forms.ComboBox
    Friend WithEvents cmbType As System.Windows.Forms.ComboBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents cmbCat As System.Windows.Forms.ComboBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DepartmentName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Company As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InvCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Price As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LotID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Article As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Remarks As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ItemName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Urgent As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NewAssort As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HardCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SelectCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkRussiaP As DiaStock.HazelDev_CheckBox
    Friend WithEvents chkRussiaR As DiaStock.HazelDev_CheckBox
    Friend WithEvents optCons2 As DiaStock.HazelDev_RadioButton
    Friend WithEvents optCons3 As DiaStock.HazelDev_RadioButton
    Friend WithEvents cmbDescription As System.Windows.Forms.ComboBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
End Class
