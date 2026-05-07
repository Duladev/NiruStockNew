<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_RprPacketAuto
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
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.txtWOEmp = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.cmdAdd = New DiaStock.HazelDev_Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtBalCts = New System.Windows.Forms.TextBox()
        Me.txtBalPcs = New System.Windows.Forms.TextBox()
        Me.pnlDetails1 = New System.Windows.Forms.Panel()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cmbModel = New System.Windows.Forms.ComboBox()
        Me.cmdGetData = New DiaStock.HazelDev_Button()
        Me.txtSysCts = New System.Windows.Forms.TextBox()
        Me.txtSysPcs = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtSelCts = New System.Windows.Forms.TextBox()
        Me.txtSelPcs = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtRghPktNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbFlo = New System.Windows.Forms.ComboBox()
        Me.cmbClarity = New System.Windows.Forms.ComboBox()
        Me.cmbColor = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtTension = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtAssortment = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtMaxPktNo = New System.Windows.Forms.TextBox()
        Me.pnlDetails2 = New System.Windows.Forms.Panel()
        Me.txtTotCts = New System.Windows.Forms.TextBox()
        Me.txtTotPcs = New System.Windows.Forms.TextBox()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.Code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DepartmentName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Company = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Comment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtCts = New System.Windows.Forms.TextBox()
        Me.txtPcs = New System.Windows.Forms.TextBox()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdPktID = New DiaStock.HazelDev_Button()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtParNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        Me.pnlDetails1.SuspendLayout()
        Me.pnlDetails2.SuspendLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtWOEmp)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label16)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label15)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtComment)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdAdd)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label14)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtBalCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtBalPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.pnlDetails1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label8)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTension)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label10)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label27)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtAssortment)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label23)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtMaxPktNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.pnlDetails2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtCts)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label7)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label6)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtParNo)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label4)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(693, 591)
        Me.HazelDev_ThemeContainer1.TabIndex = 3
        Me.HazelDev_ThemeContainer1.Text = "ROUGH PROCESS PACKET (ROUGH PLAN)"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'txtWOEmp
        '
        Me.txtWOEmp.Location = New System.Drawing.Point(346, 123)
        Me.txtWOEmp.Name = "txtWOEmp"
        Me.txtWOEmp.ReadOnly = True
        Me.txtWOEmp.Size = New System.Drawing.Size(74, 21)
        Me.txtWOEmp.TabIndex = 90
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Location = New System.Drawing.Point(346, 105)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(78, 15)
        Me.Label16.TabIndex = 91
        Me.Label16.Text = "WO Emp No."
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Location = New System.Drawing.Point(423, 149)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(61, 15)
        Me.Label15.TabIndex = 88
        Me.Label15.Text = "Comment"
        '
        'txtComment
        '
        Me.txtComment.Location = New System.Drawing.Point(426, 167)
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(113, 21)
        Me.txtComment.TabIndex = 89
        Me.txtComment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.cmdAdd.Location = New System.Drawing.Point(426, 118)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(113, 28)
        Me.cmdAdd.TabIndex = 87
        Me.cmdAdd.Text = "Add"
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Location = New System.Drawing.Point(343, 149)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(51, 15)
        Me.Label14.TabIndex = 83
        Me.Label14.Text = "Tension"
        '
        'txtBalCts
        '
        Me.txtBalCts.Location = New System.Drawing.Point(263, 125)
        Me.txtBalCts.Name = "txtBalCts"
        Me.txtBalCts.ReadOnly = True
        Me.txtBalCts.Size = New System.Drawing.Size(74, 21)
        Me.txtBalCts.TabIndex = 82
        Me.txtBalCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBalPcs
        '
        Me.txtBalPcs.Location = New System.Drawing.Point(185, 125)
        Me.txtBalPcs.Name = "txtBalPcs"
        Me.txtBalPcs.ReadOnly = True
        Me.txtBalPcs.Size = New System.Drawing.Size(72, 21)
        Me.txtBalPcs.TabIndex = 81
        Me.txtBalPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'pnlDetails1
        '
        Me.pnlDetails1.BackColor = System.Drawing.Color.White
        Me.pnlDetails1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDetails1.Controls.Add(Me.Label17)
        Me.pnlDetails1.Controls.Add(Me.cmbModel)
        Me.pnlDetails1.Controls.Add(Me.cmdGetData)
        Me.pnlDetails1.Controls.Add(Me.txtSysCts)
        Me.pnlDetails1.Controls.Add(Me.txtSysPcs)
        Me.pnlDetails1.Controls.Add(Me.Label5)
        Me.pnlDetails1.Controls.Add(Me.Label13)
        Me.pnlDetails1.Controls.Add(Me.txtSelCts)
        Me.pnlDetails1.Controls.Add(Me.txtSelPcs)
        Me.pnlDetails1.Controls.Add(Me.Label2)
        Me.pnlDetails1.Controls.Add(Me.Label3)
        Me.pnlDetails1.Controls.Add(Me.txtRghPktNo)
        Me.pnlDetails1.Controls.Add(Me.Label1)
        Me.pnlDetails1.Controls.Add(Me.Label12)
        Me.pnlDetails1.Controls.Add(Me.Label11)
        Me.pnlDetails1.Controls.Add(Me.Label9)
        Me.pnlDetails1.Controls.Add(Me.cmbFlo)
        Me.pnlDetails1.Controls.Add(Me.cmbClarity)
        Me.pnlDetails1.Controls.Add(Me.cmbColor)
        Me.pnlDetails1.Location = New System.Drawing.Point(545, 113)
        Me.pnlDetails1.Name = "pnlDetails1"
        Me.pnlDetails1.Size = New System.Drawing.Size(139, 466)
        Me.pnlDetails1.TabIndex = 71
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Location = New System.Drawing.Point(9, 153)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(42, 15)
        Me.Label17.TabIndex = 93
        Me.Label17.Text = "Model"
        '
        'cmbModel
        '
        Me.cmbModel.Enabled = False
        Me.cmbModel.FormattingEnabled = True
        Me.cmbModel.IntegralHeight = False
        Me.cmbModel.Location = New System.Drawing.Point(9, 171)
        Me.cmbModel.Name = "cmbModel"
        Me.cmbModel.Size = New System.Drawing.Size(112, 23)
        Me.cmbModel.TabIndex = 92
        '
        'cmdGetData
        '
        Me.cmdGetData.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdGetData.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdGetData.FlatAppearance.BorderSize = 0
        Me.cmdGetData.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdGetData.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdGetData.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdGetData.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGetData.ForeColor = System.Drawing.Color.White
        Me.cmdGetData.Location = New System.Drawing.Point(9, 341)
        Me.cmdGetData.Name = "cmdGetData"
        Me.cmdGetData.Size = New System.Drawing.Size(112, 28)
        Me.cmdGetData.TabIndex = 91
        Me.cmdGetData.Text = "Get Rgh Data"
        Me.cmdGetData.UseVisualStyleBackColor = False
        '
        'txtSysCts
        '
        Me.txtSysCts.Location = New System.Drawing.Point(9, 434)
        Me.txtSysCts.Name = "txtSysCts"
        Me.txtSysCts.ReadOnly = True
        Me.txtSysCts.Size = New System.Drawing.Size(112, 21)
        Me.txtSysCts.TabIndex = 90
        Me.txtSysCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSysPcs
        '
        Me.txtSysPcs.Location = New System.Drawing.Point(9, 390)
        Me.txtSysPcs.Name = "txtSysPcs"
        Me.txtSysPcs.ReadOnly = True
        Me.txtSysPcs.Size = New System.Drawing.Size(112, 21)
        Me.txtSysPcs.TabIndex = 89
        Me.txtSysPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(6, 416)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(46, 15)
        Me.Label5.TabIndex = 88
        Me.Label5.Text = "Sys Cts"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Location = New System.Drawing.Point(6, 372)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(49, 15)
        Me.Label13.TabIndex = 87
        Me.Label13.Text = "Sys Pcs"
        '
        'txtSelCts
        '
        Me.txtSelCts.Location = New System.Drawing.Point(9, 300)
        Me.txtSelCts.Name = "txtSelCts"
        Me.txtSelCts.ReadOnly = True
        Me.txtSelCts.Size = New System.Drawing.Size(112, 21)
        Me.txtSelCts.TabIndex = 86
        Me.txtSelCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSelPcs
        '
        Me.txtSelPcs.Location = New System.Drawing.Point(9, 256)
        Me.txtSelPcs.Name = "txtSelPcs"
        Me.txtSelPcs.ReadOnly = True
        Me.txtSelPcs.Size = New System.Drawing.Size(112, 21)
        Me.txtSelPcs.TabIndex = 85
        Me.txtSelPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(6, 282)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 15)
        Me.Label2.TabIndex = 84
        Me.Label2.Text = "Sel Cts"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(6, 238)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 15)
        Me.Label3.TabIndex = 83
        Me.Label3.Text = "Sel Pcs"
        '
        'txtRghPktNo
        '
        Me.txtRghPktNo.Location = New System.Drawing.Point(9, 215)
        Me.txtRghPktNo.Name = "txtRghPktNo"
        Me.txtRghPktNo.Size = New System.Drawing.Size(84, 21)
        Me.txtRghPktNo.TabIndex = 78
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(9, 197)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 15)
        Me.Label1.TabIndex = 79
        Me.Label1.Text = "WO Pkt"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Location = New System.Drawing.Point(9, 103)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(71, 15)
        Me.Label12.TabIndex = 77
        Me.Label12.Text = "Fluorascent"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Location = New System.Drawing.Point(9, 56)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(40, 15)
        Me.Label11.TabIndex = 76
        Me.Label11.Text = "Clarity"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(9, 9)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(36, 15)
        Me.Label9.TabIndex = 75
        Me.Label9.Text = "Color"
        '
        'cmbFlo
        '
        Me.cmbFlo.Enabled = False
        Me.cmbFlo.FormattingEnabled = True
        Me.cmbFlo.IntegralHeight = False
        Me.cmbFlo.Location = New System.Drawing.Point(9, 121)
        Me.cmbFlo.Name = "cmbFlo"
        Me.cmbFlo.Size = New System.Drawing.Size(112, 23)
        Me.cmbFlo.TabIndex = 73
        '
        'cmbClarity
        '
        Me.cmbClarity.Enabled = False
        Me.cmbClarity.FormattingEnabled = True
        Me.cmbClarity.IntegralHeight = False
        Me.cmbClarity.Location = New System.Drawing.Point(9, 74)
        Me.cmbClarity.Name = "cmbClarity"
        Me.cmbClarity.Size = New System.Drawing.Size(112, 23)
        Me.cmbClarity.TabIndex = 72
        '
        'cmbColor
        '
        Me.cmbColor.Enabled = False
        Me.cmbColor.FormattingEnabled = True
        Me.cmbColor.IntegralHeight = False
        Me.cmbColor.Location = New System.Drawing.Point(9, 27)
        Me.cmbColor.Name = "cmbColor"
        Me.cmbColor.Size = New System.Drawing.Size(112, 23)
        Me.cmbColor.TabIndex = 71
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(260, 107)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(45, 15)
        Me.Label8.TabIndex = 80
        Me.Label8.Text = "Bal Cts"
        '
        'txtTension
        '
        Me.txtTension.Location = New System.Drawing.Point(346, 167)
        Me.txtTension.Name = "txtTension"
        Me.txtTension.Size = New System.Drawing.Size(74, 21)
        Me.txtTension.TabIndex = 86
        Me.txtTension.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(182, 107)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(48, 15)
        Me.Label10.TabIndex = 79
        Me.Label10.Text = "Bal Pcs"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Location = New System.Drawing.Point(12, 149)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(68, 15)
        Me.Label27.TabIndex = 78
        Me.Label27.Text = "Assortment"
        '
        'txtAssortment
        '
        Me.txtAssortment.Location = New System.Drawing.Point(12, 167)
        Me.txtAssortment.Name = "txtAssortment"
        Me.txtAssortment.ReadOnly = True
        Me.txtAssortment.Size = New System.Drawing.Size(167, 21)
        Me.txtAssortment.TabIndex = 77
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Location = New System.Drawing.Point(105, 107)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(74, 15)
        Me.Label23.TabIndex = 76
        Me.Label23.Text = "Next Pkt No."
        '
        'txtMaxPktNo
        '
        Me.txtMaxPktNo.Location = New System.Drawing.Point(105, 125)
        Me.txtMaxPktNo.Name = "txtMaxPktNo"
        Me.txtMaxPktNo.ReadOnly = True
        Me.txtMaxPktNo.Size = New System.Drawing.Size(74, 21)
        Me.txtMaxPktNo.TabIndex = 73
        '
        'pnlDetails2
        '
        Me.pnlDetails2.BackColor = System.Drawing.Color.White
        Me.pnlDetails2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDetails2.Controls.Add(Me.txtTotCts)
        Me.pnlDetails2.Controls.Add(Me.txtTotPcs)
        Me.pnlDetails2.Controls.Add(Me.flxDetails)
        Me.pnlDetails2.Location = New System.Drawing.Point(12, 198)
        Me.pnlDetails2.Name = "pnlDetails2"
        Me.pnlDetails2.Size = New System.Drawing.Size(527, 381)
        Me.pnlDetails2.TabIndex = 68
        '
        'txtTotCts
        '
        Me.txtTotCts.Location = New System.Drawing.Point(188, 346)
        Me.txtTotCts.Name = "txtTotCts"
        Me.txtTotCts.ReadOnly = True
        Me.txtTotCts.Size = New System.Drawing.Size(92, 21)
        Me.txtTotCts.TabIndex = 92
        Me.txtTotCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotPcs
        '
        Me.txtTotPcs.Location = New System.Drawing.Point(93, 346)
        Me.txtTotPcs.Name = "txtTotPcs"
        Me.txtTotPcs.ReadOnly = True
        Me.txtTotPcs.Size = New System.Drawing.Size(91, 21)
        Me.txtTotPcs.TabIndex = 91
        Me.txtTotPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Code, Me.DepartmentName, Me.Company, Me.InTime, Me.Comment})
        Me.flxDetails.Location = New System.Drawing.Point(6, 4)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(513, 336)
        Me.flxDetails.TabIndex = 44
        '
        'Code
        '
        Me.Code.HeaderText = "Pkt No."
        Me.Code.Name = "Code"
        Me.Code.ReadOnly = True
        '
        'DepartmentName
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DepartmentName.DefaultCellStyle = DataGridViewCellStyle1
        Me.DepartmentName.HeaderText = "Pcs"
        Me.DepartmentName.Name = "DepartmentName"
        Me.DepartmentName.ReadOnly = True
        Me.DepartmentName.Width = 80
        '
        'Company
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Company.DefaultCellStyle = DataGridViewCellStyle2
        Me.Company.HeaderText = "Cts"
        Me.Company.Name = "Company"
        '
        'InTime
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.NullValue = Nothing
        Me.InTime.DefaultCellStyle = DataGridViewCellStyle3
        Me.InTime.HeaderText = "Tension"
        Me.InTime.Name = "InTime"
        '
        'Comment
        '
        Me.Comment.HeaderText = "Comment"
        Me.Comment.Name = "Comment"
        '
        'txtCts
        '
        Me.txtCts.Location = New System.Drawing.Point(263, 167)
        Me.txtCts.Name = "txtCts"
        Me.txtCts.Size = New System.Drawing.Size(74, 21)
        Me.txtCts.TabIndex = 53
        Me.txtCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPcs
        '
        Me.txtPcs.Location = New System.Drawing.Point(185, 167)
        Me.txtPcs.Name = "txtPcs"
        Me.txtPcs.Size = New System.Drawing.Size(72, 21)
        Me.txtPcs.TabIndex = 52
        Me.txtPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmdPktID)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(681, 50)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmdPktID
        '
        Me.cmdPktID.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdPktID.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdPktID.FlatAppearance.BorderSize = 0
        Me.cmdPktID.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdPktID.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdPktID.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdPktID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPktID.ForeColor = System.Drawing.Color.White
        Me.cmdPktID.Location = New System.Drawing.Point(565, 13)
        Me.cmdPktID.Name = "cmdPktID"
        Me.cmdPktID.Size = New System.Drawing.Size(99, 28)
        Me.cmdPktID.TabIndex = 46
        Me.cmdPktID.Text = "Set Packet ID"
        Me.cmdPktID.UseVisualStyleBackColor = False
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
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(260, 149)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(24, 15)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = "Cts"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(182, 149)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(27, 15)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "Pcs"
        '
        'txtParNo
        '
        Me.txtParNo.Location = New System.Drawing.Point(12, 125)
        Me.txtParNo.Name = "txtParNo"
        Me.txtParNo.Size = New System.Drawing.Size(84, 21)
        Me.txtParNo.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(12, 107)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 15)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Parcel No"
        '
        'frm_RprPacketAuto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(693, 591)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_RprPacketAuto"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Rpr Packet (Rough Plan)"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        Me.pnlDetails1.ResumeLayout(False)
        Me.pnlDetails1.PerformLayout()
        Me.pnlDetails2.ResumeLayout(False)
        Me.pnlDetails2.PerformLayout()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtAssortment As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtMaxPktNo As System.Windows.Forms.TextBox
    Friend WithEvents pnlDetails1 As System.Windows.Forms.Panel
    Friend WithEvents txtTension As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbFlo As System.Windows.Forms.ComboBox
    Friend WithEvents cmbClarity As System.Windows.Forms.ComboBox
    Friend WithEvents cmbColor As System.Windows.Forms.ComboBox
    Friend WithEvents pnlDetails2 As System.Windows.Forms.Panel
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents txtCts As System.Windows.Forms.TextBox
    Friend WithEvents txtPcs As System.Windows.Forms.TextBox
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtParNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtBalCts As System.Windows.Forms.TextBox
    Friend WithEvents txtBalPcs As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cmdAdd As DiaStock.HazelDev_Button
    Friend WithEvents txtRghPktNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSelCts As System.Windows.Forms.TextBox
    Friend WithEvents txtSelPcs As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtSysCts As System.Windows.Forms.TextBox
    Friend WithEvents txtSysPcs As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmdGetData As DiaStock.HazelDev_Button
    Friend WithEvents txtTotCts As System.Windows.Forms.TextBox
    Friend WithEvents txtTotPcs As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents txtWOEmp As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DepartmentName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Company As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Comment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents cmbModel As System.Windows.Forms.ComboBox
    Friend WithEvents cmdPktID As DiaStock.HazelDev_Button
End Class
