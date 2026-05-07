<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_MixShipmentPlan
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
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.HazelDev_ThemeContainer1 = New DiaStock.HazelDev_ThemeContainer()
        Me.txtSubject2 = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtPcs2 = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtExportPcs = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtShipBalPcs = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtBalPcs = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtIssPcs = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtOrdPcs = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtPlanPcs = New System.Windows.Forms.TextBox()
        Me.cmdAdd = New DiaStock.HazelDev_Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtDueDate = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbPriority = New System.Windows.Forms.ComboBox()
        Me.txtSubject = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtClient = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpFinishDate = New System.Windows.Forms.DateTimePicker()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.dtpOrdDate = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtGroup = New System.Windows.Forms.TextBox()
        Me.txtTotExport = New System.Windows.Forms.TextBox()
        Me.txtTotInFinish = New System.Windows.Forms.TextBox()
        Me.txtTotInProd = New System.Windows.Forms.TextBox()
        Me.flxOrder = New System.Windows.Forms.DataGridView()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OrdDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OrderNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Grp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Comment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Prority = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FinishDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Subject = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DueDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.flxDetails = New System.Windows.Forms.DataGridView()
        Me.RetPcsT = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RetCts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EmpNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Exported = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtPlanID = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtPcs = New System.Windows.Forms.TextBox()
        Me.HazelDev_Panel1 = New DiaStock.HazelDev_Panel()
        Me.cmdDelete = New DiaStock.HazelDev_Button()
        Me.cmdLoad = New DiaStock.HazelDev_Button()
        Me.cmdExcel = New DiaStock.HazelDev_Button()
        Me.cmdOpen = New DiaStock.HazelDev_Button()
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.cmdNew = New DiaStock.HazelDev_Button()
        Me.cmdExit = New DiaStock.HazelDev_Button()
        Me.cmdSave = New DiaStock.HazelDev_Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtOrder = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.HazelDev_ThemeContainer1.SuspendLayout()
        CType(Me.flxOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HazelDev_Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'HazelDev_ThemeContainer1
        '
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtSubject2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label17)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPcs2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label15)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtExportPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label14)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtShipBalPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label13)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtBalPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label12)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtIssPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label10)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtOrdPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label9)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label7)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPlanPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmdAdd)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label11)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtDueDate)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label5)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.cmbPriority)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtSubject)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label3)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtClient)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label2)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotal)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.dtpFinishDate)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label20)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.dtpOrdDate)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label8)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtGroup)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotExport)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotInFinish)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtTotInProd)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxOrder)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.flxDetails)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPlanID)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label16)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtPcs)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.HazelDev_Panel1)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label6)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.txtOrder)
        Me.HazelDev_ThemeContainer1.Controls.Add(Me.Label4)
        Me.HazelDev_ThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HazelDev_ThemeContainer1.DrawBottomLine = False
        Me.HazelDev_ThemeContainer1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.HazelDev_ThemeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.HazelDev_ThemeContainer1.Name = "HazelDev_ThemeContainer1"
        Me.HazelDev_ThemeContainer1.Size = New System.Drawing.Size(1083, 582)
        Me.HazelDev_ThemeContainer1.TabIndex = 6
        Me.HazelDev_ThemeContainer1.Text = "MIX SHIPMENT PLAN"
        Me.HazelDev_ThemeContainer1.TransparencyKey = System.Drawing.Color.Empty
        '
        'txtSubject2
        '
        Me.txtSubject2.Location = New System.Drawing.Point(891, 453)
        Me.txtSubject2.MaxLength = 6
        Me.txtSubject2.Name = "txtSubject2"
        Me.txtSubject2.ReadOnly = True
        Me.txtSubject2.Size = New System.Drawing.Size(185, 21)
        Me.txtSubject2.TabIndex = 206
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Location = New System.Drawing.Point(891, 435)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(55, 15)
        Me.Label17.TabIndex = 207
        Me.Label17.Text = "Subject2"
        '
        'txtPcs2
        '
        Me.txtPcs2.Location = New System.Drawing.Point(695, 125)
        Me.txtPcs2.Name = "txtPcs2"
        Me.txtPcs2.Size = New System.Drawing.Size(72, 21)
        Me.txtPcs2.TabIndex = 205
        Me.txtPcs2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Location = New System.Drawing.Point(692, 107)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(37, 15)
        Me.Label15.TabIndex = 204
        Me.Label15.Text = "Pcs 2"
        '
        'txtExportPcs
        '
        Me.txtExportPcs.Location = New System.Drawing.Point(891, 495)
        Me.txtExportPcs.MaxLength = 6
        Me.txtExportPcs.Name = "txtExportPcs"
        Me.txtExportPcs.ReadOnly = True
        Me.txtExportPcs.Size = New System.Drawing.Size(143, 21)
        Me.txtExportPcs.TabIndex = 202
        Me.txtExportPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Location = New System.Drawing.Point(891, 477)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(79, 15)
        Me.Label14.TabIndex = 203
        Me.Label14.Text = "Exported Pcs"
        '
        'txtShipBalPcs
        '
        Me.txtShipBalPcs.Location = New System.Drawing.Point(891, 549)
        Me.txtShipBalPcs.MaxLength = 6
        Me.txtShipBalPcs.Name = "txtShipBalPcs"
        Me.txtShipBalPcs.ReadOnly = True
        Me.txtShipBalPcs.Size = New System.Drawing.Size(143, 21)
        Me.txtShipBalPcs.TabIndex = 200
        Me.txtShipBalPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Location = New System.Drawing.Point(891, 531)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(131, 15)
        Me.Label13.TabIndex = 201
        Me.Label13.Text = "Shipment Balance Pcs"
        '
        'txtBalPcs
        '
        Me.txtBalPcs.Location = New System.Drawing.Point(718, 549)
        Me.txtBalPcs.MaxLength = 6
        Me.txtBalPcs.Name = "txtBalPcs"
        Me.txtBalPcs.ReadOnly = True
        Me.txtBalPcs.Size = New System.Drawing.Size(143, 21)
        Me.txtBalPcs.TabIndex = 198
        Me.txtBalPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Location = New System.Drawing.Point(718, 531)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(75, 15)
        Me.Label12.TabIndex = 199
        Me.Label12.Text = "Balance Pcs"
        '
        'txtIssPcs
        '
        Me.txtIssPcs.Location = New System.Drawing.Point(546, 549)
        Me.txtIssPcs.MaxLength = 6
        Me.txtIssPcs.Name = "txtIssPcs"
        Me.txtIssPcs.ReadOnly = True
        Me.txtIssPcs.Size = New System.Drawing.Size(143, 21)
        Me.txtIssPcs.TabIndex = 196
        Me.txtIssPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(546, 531)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(66, 15)
        Me.Label10.TabIndex = 197
        Me.Label10.Text = "Issued Pcs"
        '
        'txtOrdPcs
        '
        Me.txtOrdPcs.Location = New System.Drawing.Point(718, 495)
        Me.txtOrdPcs.MaxLength = 6
        Me.txtOrdPcs.Name = "txtOrdPcs"
        Me.txtOrdPcs.ReadOnly = True
        Me.txtOrdPcs.Size = New System.Drawing.Size(143, 21)
        Me.txtOrdPcs.TabIndex = 194
        Me.txtOrdPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(718, 477)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(61, 15)
        Me.Label9.TabIndex = 195
        Me.Label9.Text = "Order Pcs"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(762, 388)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(85, 15)
        Me.Label7.TabIndex = 193
        Me.Label7.Text = "Total Plan Pcs"
        '
        'txtPlanPcs
        '
        Me.txtPlanPcs.Location = New System.Drawing.Point(765, 406)
        Me.txtPlanPcs.Name = "txtPlanPcs"
        Me.txtPlanPcs.ReadOnly = True
        Me.txtPlanPcs.Size = New System.Drawing.Size(91, 21)
        Me.txtPlanPcs.TabIndex = 192
        Me.txtPlanPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.cmdAdd.Location = New System.Drawing.Point(965, 118)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(99, 28)
        Me.cmdAdd.TabIndex = 191
        Me.cmdAdd.Text = "Add"
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Location = New System.Drawing.Point(469, 107)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(44, 15)
        Me.Label11.TabIndex = 190
        Me.Label11.Text = "Priority"
        '
        'txtDueDate
        '
        Me.txtDueDate.Location = New System.Drawing.Point(546, 495)
        Me.txtDueDate.MaxLength = 6
        Me.txtDueDate.Name = "txtDueDate"
        Me.txtDueDate.ReadOnly = True
        Me.txtDueDate.Size = New System.Drawing.Size(143, 21)
        Me.txtDueDate.TabIndex = 187
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(546, 477)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 15)
        Me.Label5.TabIndex = 188
        Me.Label5.Text = "Due Date"
        '
        'cmbPriority
        '
        Me.cmbPriority.FormattingEnabled = True
        Me.cmbPriority.IntegralHeight = False
        Me.cmbPriority.Location = New System.Drawing.Point(472, 125)
        Me.cmbPriority.Name = "cmbPriority"
        Me.cmbPriority.Size = New System.Drawing.Size(109, 23)
        Me.cmbPriority.TabIndex = 189
        '
        'txtSubject
        '
        Me.txtSubject.Location = New System.Drawing.Point(546, 453)
        Me.txtSubject.MaxLength = 6
        Me.txtSubject.Name = "txtSubject"
        Me.txtSubject.ReadOnly = True
        Me.txtSubject.Size = New System.Drawing.Size(315, 21)
        Me.txtSubject.TabIndex = 185
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(546, 435)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 15)
        Me.Label3.TabIndex = 186
        Me.Label3.Text = "Subject"
        '
        'txtClient
        '
        Me.txtClient.Location = New System.Drawing.Point(546, 406)
        Me.txtClient.MaxLength = 6
        Me.txtClient.Name = "txtClient"
        Me.txtClient.ReadOnly = True
        Me.txtClient.Size = New System.Drawing.Size(192, 21)
        Me.txtClient.TabIndex = 183
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(546, 388)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 15)
        Me.Label2.TabIndex = 184
        Me.Label2.Text = "Client"
        '
        'txtTotal
        '
        Me.txtTotal.Location = New System.Drawing.Point(411, 549)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(91, 21)
        Me.txtTotal.TabIndex = 182
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(584, 107)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 15)
        Me.Label1.TabIndex = 181
        Me.Label1.Text = "Finish Date"
        '
        'dtpFinishDate
        '
        Me.dtpFinishDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFinishDate.Location = New System.Drawing.Point(587, 125)
        Me.dtpFinishDate.Name = "dtpFinishDate"
        Me.dtpFinishDate.Size = New System.Drawing.Size(102, 21)
        Me.dtpFinishDate.TabIndex = 180
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Location = New System.Drawing.Point(111, 107)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(67, 15)
        Me.Label20.TabIndex = 179
        Me.Label20.Text = "Order Date"
        '
        'dtpOrdDate
        '
        Me.dtpOrdDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpOrdDate.Location = New System.Drawing.Point(112, 125)
        Me.dtpOrdDate.Name = "dtpOrdDate"
        Me.dtpOrdDate.Size = New System.Drawing.Size(102, 21)
        Me.dtpOrdDate.TabIndex = 178
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(397, 107)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(41, 15)
        Me.Label8.TabIndex = 177
        Me.Label8.Text = "Group"
        '
        'txtGroup
        '
        Me.txtGroup.Location = New System.Drawing.Point(400, 125)
        Me.txtGroup.MaxLength = 1
        Me.txtGroup.Name = "txtGroup"
        Me.txtGroup.Size = New System.Drawing.Size(66, 21)
        Me.txtGroup.TabIndex = 176
        '
        'txtTotExport
        '
        Me.txtTotExport.Location = New System.Drawing.Point(314, 549)
        Me.txtTotExport.Name = "txtTotExport"
        Me.txtTotExport.ReadOnly = True
        Me.txtTotExport.Size = New System.Drawing.Size(91, 21)
        Me.txtTotExport.TabIndex = 166
        Me.txtTotExport.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotInFinish
        '
        Me.txtTotInFinish.Location = New System.Drawing.Point(217, 549)
        Me.txtTotInFinish.Name = "txtTotInFinish"
        Me.txtTotInFinish.ReadOnly = True
        Me.txtTotInFinish.Size = New System.Drawing.Size(91, 21)
        Me.txtTotInFinish.TabIndex = 165
        Me.txtTotInFinish.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotInProd
        '
        Me.txtTotInProd.Location = New System.Drawing.Point(120, 549)
        Me.txtTotInProd.Name = "txtTotInProd"
        Me.txtTotInProd.ReadOnly = True
        Me.txtTotInProd.Size = New System.Drawing.Size(91, 21)
        Me.txtTotInProd.TabIndex = 164
        Me.txtTotInProd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'flxOrder
        '
        Me.flxOrder.AllowUserToAddRows = False
        Me.flxOrder.AllowUserToDeleteRows = False
        Me.flxOrder.AllowUserToResizeColumns = False
        Me.flxOrder.AllowUserToResizeRows = False
        Me.flxOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxOrder.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID, Me.OrdDate, Me.OrderNo, Me.DataGridViewTextBoxColumn1, Me.Grp, Me.Status, Me.Comment, Me.Prority, Me.FinishDate, Me.Subject, Me.DueDate, Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column7})
        Me.flxOrder.Location = New System.Drawing.Point(12, 152)
        Me.flxOrder.Name = "flxOrder"
        Me.flxOrder.RowHeadersVisible = False
        Me.flxOrder.Size = New System.Drawing.Size(1064, 230)
        Me.flxOrder.TabIndex = 161
        '
        'ID
        '
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        '
        'OrdDate
        '
        Me.OrdDate.HeaderText = "Date"
        Me.OrdDate.Name = "OrdDate"
        '
        'OrderNo
        '
        Me.OrderNo.HeaderText = "Order No"
        Me.OrderNo.Name = "OrderNo"
        Me.OrderNo.ReadOnly = True
        Me.OrderNo.Width = 80
        '
        'DataGridViewTextBoxColumn1
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewTextBoxColumn1.HeaderText = "Pcs"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 50
        '
        'Grp
        '
        Me.Grp.HeaderText = "Dept"
        Me.Grp.Name = "Grp"
        '
        'Status
        '
        Me.Status.HeaderText = "Status"
        Me.Status.Name = "Status"
        '
        'Comment
        '
        Me.Comment.HeaderText = "Comment"
        Me.Comment.Name = "Comment"
        '
        'Prority
        '
        Me.Prority.HeaderText = "Prority"
        Me.Prority.Name = "Prority"
        '
        'FinishDate
        '
        Me.FinishDate.HeaderText = "Finish Date"
        Me.FinishDate.Name = "FinishDate"
        '
        'Subject
        '
        Me.Subject.HeaderText = "Subject"
        Me.Subject.Name = "Subject"
        '
        'DueDate
        '
        Me.DueDate.HeaderText = "Due Date"
        Me.DueDate.Name = "DueDate"
        '
        'Column1
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle2
        Me.Column1.HeaderText = "Pcs2"
        Me.Column1.Name = "Column1"
        Me.Column1.Width = 50
        '
        'Column2
        '
        Me.Column2.HeaderText = "NOrder No"
        Me.Column2.Name = "Column2"
        '
        'Column3
        '
        Me.Column3.HeaderText = "Item No"
        Me.Column3.Name = "Column3"
        '
        'Column4
        '
        Me.Column4.HeaderText = "Client"
        Me.Column4.Name = "Column4"
        '
        'Column5
        '
        Me.Column5.HeaderText = "Groove"
        Me.Column5.Name = "Column5"
        '
        'Column6
        '
        Me.Column6.HeaderText = "Laser"
        Me.Column6.Name = "Column6"
        '
        'Column7
        '
        Me.Column7.HeaderText = "Subject2"
        Me.Column7.Name = "Column7"
        '
        'flxDetails
        '
        Me.flxDetails.AllowUserToAddRows = False
        Me.flxDetails.AllowUserToDeleteRows = False
        Me.flxDetails.AllowUserToResizeColumns = False
        Me.flxDetails.AllowUserToResizeRows = False
        Me.flxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.flxDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RetPcsT, Me.RetCts, Me.EmpNo, Me.Exported, Me.Total})
        Me.flxDetails.Location = New System.Drawing.Point(12, 388)
        Me.flxDetails.Name = "flxDetails"
        Me.flxDetails.ReadOnly = True
        Me.flxDetails.RowHeadersVisible = False
        Me.flxDetails.Size = New System.Drawing.Size(528, 155)
        Me.flxDetails.TabIndex = 160
        '
        'RetPcsT
        '
        Me.RetPcsT.HeaderText = "Group"
        Me.RetPcsT.Name = "RetPcsT"
        Me.RetPcsT.ReadOnly = True
        '
        'RetCts
        '
        Me.RetCts.HeaderText = "In Prod"
        Me.RetCts.Name = "RetCts"
        Me.RetCts.ReadOnly = True
        '
        'EmpNo
        '
        Me.EmpNo.HeaderText = "In Finish"
        Me.EmpNo.Name = "EmpNo"
        Me.EmpNo.ReadOnly = True
        '
        'Exported
        '
        Me.Exported.HeaderText = "Exported"
        Me.Exported.Name = "Exported"
        Me.Exported.ReadOnly = True
        '
        'Total
        '
        Me.Total.HeaderText = "Total"
        Me.Total.Name = "Total"
        Me.Total.ReadOnly = True
        '
        'txtPlanID
        '
        Me.txtPlanID.Location = New System.Drawing.Point(12, 125)
        Me.txtPlanID.Name = "txtPlanID"
        Me.txtPlanID.ReadOnly = True
        Me.txtPlanID.Size = New System.Drawing.Size(94, 21)
        Me.txtPlanID.TabIndex = 90
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Location = New System.Drawing.Point(9, 107)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(47, 15)
        Me.Label16.TabIndex = 91
        Me.Label16.Text = "Plan ID"
        '
        'txtPcs
        '
        Me.txtPcs.Location = New System.Drawing.Point(322, 125)
        Me.txtPcs.Name = "txtPcs"
        Me.txtPcs.Size = New System.Drawing.Size(72, 21)
        Me.txtPcs.TabIndex = 52
        Me.txtPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'HazelDev_Panel1
        '
        Me.HazelDev_Panel1.BackColor = System.Drawing.Color.White
        Me.HazelDev_Panel1.Controls.Add(Me.cmdDelete)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdLoad)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExcel)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdOpen)
        Me.HazelDev_Panel1.Controls.Add(Me.txtFilePath)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdNew)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdExit)
        Me.HazelDev_Panel1.Controls.Add(Me.cmdSave)
        Me.HazelDev_Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.HazelDev_Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.HazelDev_Panel1.Location = New System.Drawing.Point(3, 54)
        Me.HazelDev_Panel1.Name = "HazelDev_Panel1"
        Me.HazelDev_Panel1.Size = New System.Drawing.Size(1073, 50)
        Me.HazelDev_Panel1.TabIndex = 31
        Me.HazelDev_Panel1.Text = "HazelDev_Panel1"
        Me.HazelDev_Panel1.TransparencyKey = System.Drawing.Color.Empty
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdDelete.FlatAppearance.BorderSize = 0
        Me.cmdDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon
        Me.cmdDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.ForeColor = System.Drawing.Color.White
        Me.cmdDelete.Location = New System.Drawing.Point(319, 13)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.Size = New System.Drawing.Size(99, 28)
        Me.cmdDelete.TabIndex = 180
        Me.cmdDelete.Text = "Delete"
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'cmdLoad
        '
        Me.cmdLoad.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdLoad.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdLoad.FlatAppearance.BorderSize = 0
        Me.cmdLoad.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdLoad.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdLoad.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLoad.ForeColor = System.Drawing.Color.White
        Me.cmdLoad.Location = New System.Drawing.Point(823, 13)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.Size = New System.Drawing.Size(99, 28)
        Me.cmdLoad.TabIndex = 83
        Me.cmdLoad.Text = "Load File"
        Me.cmdLoad.UseVisualStyleBackColor = False
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
        Me.cmdExcel.Location = New System.Drawing.Point(962, 13)
        Me.cmdExcel.Name = "cmdExcel"
        Me.cmdExcel.Size = New System.Drawing.Size(99, 28)
        Me.cmdExcel.TabIndex = 179
        Me.cmdExcel.Text = "Excel"
        Me.cmdExcel.UseVisualStyleBackColor = False
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
        Me.cmdOpen.Location = New System.Drawing.Point(718, 13)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(99, 28)
        Me.cmdOpen.TabIndex = 82
        Me.cmdOpen.Text = "Open File"
        Me.cmdOpen.UseVisualStyleBackColor = False
        '
        'txtFilePath
        '
        Me.txtFilePath.Enabled = False
        Me.txtFilePath.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilePath.Location = New System.Drawing.Point(928, 16)
        Me.txtFilePath.MaxLength = 0
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.Size = New System.Drawing.Size(18, 21)
        Me.txtFilePath.TabIndex = 81
        Me.txtFilePath.Visible = False
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
        Me.cmdNew.TabIndex = 178
        Me.cmdNew.Text = "New"
        Me.cmdNew.UseVisualStyleBackColor = False
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
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(319, 107)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(27, 15)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "Pcs"
        '
        'txtOrder
        '
        Me.txtOrder.Location = New System.Drawing.Point(217, 125)
        Me.txtOrder.MaxLength = 6
        Me.txtOrder.Name = "txtOrder"
        Me.txtOrder.Size = New System.Drawing.Size(99, 21)
        Me.txtOrder.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(217, 107)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 15)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Order No"
        '
        'frm_MixShipmentPlan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1083, 582)
        Me.Controls.Add(Me.HazelDev_ThemeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_MixShipmentPlan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mix Shipment Plan"
        Me.HazelDev_ThemeContainer1.ResumeLayout(False)
        Me.HazelDev_ThemeContainer1.PerformLayout()
        CType(Me.flxOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.flxDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HazelDev_Panel1.ResumeLayout(False)
        Me.HazelDev_Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HazelDev_ThemeContainer1 As DiaStock.HazelDev_ThemeContainer
    Friend WithEvents cmdNew As DiaStock.HazelDev_Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtGroup As System.Windows.Forms.TextBox
    Friend WithEvents txtTotExport As System.Windows.Forms.TextBox
    Friend WithEvents txtTotInFinish As System.Windows.Forms.TextBox
    Friend WithEvents txtTotInProd As System.Windows.Forms.TextBox
    Friend WithEvents flxOrder As System.Windows.Forms.DataGridView
    Friend WithEvents cmdSave As DiaStock.HazelDev_Button
    Friend WithEvents flxDetails As System.Windows.Forms.DataGridView
    Friend WithEvents txtPlanID As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtPcs As System.Windows.Forms.TextBox
    Friend WithEvents HazelDev_Panel1 As DiaStock.HazelDev_Panel
    Friend WithEvents cmdExit As DiaStock.HazelDev_Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtOrder As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents dtpOrdDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpFinishDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents RetPcsT As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RetCts As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EmpNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Exported As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Total As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents txtDueDate As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSubject As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtClient As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmbPriority As System.Windows.Forms.ComboBox
    Friend WithEvents cmdAdd As DiaStock.HazelDev_Button
    Friend WithEvents txtPlanPcs As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtBalPcs As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtIssPcs As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtOrdPcs As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmdExcel As DiaStock.HazelDev_Button
    Friend WithEvents txtShipBalPcs As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtExportPcs As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cmdLoad As DiaStock.HazelDev_Button
    Friend WithEvents cmdOpen As DiaStock.HazelDev_Button
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
    Friend WithEvents txtPcs2 As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrdDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OrderNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Grp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Comment As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Prority As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FinishDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Subject As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DueDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtSubject2 As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents cmdDelete As DiaStock.HazelDev_Button
End Class
