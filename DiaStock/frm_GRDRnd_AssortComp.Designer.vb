<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_GRDRnd_AssortComp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_GRDRnd_AssortComp))
        Me.pnlTitle = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.grpMain = New System.Windows.Forms.GroupBox()
        Me.lblCts = New System.Windows.Forms.Label()
        Me.txtCts = New System.Windows.Forms.TextBox()
        Me.lblAssort1 = New System.Windows.Forms.Label()
        Me.cmbAssort1 = New System.Windows.Forms.ComboBox()
        Me.lblAssort2 = New System.Windows.Forms.Label()
        Me.cmbAssort2 = New System.Windows.Forms.ComboBox()
        Me.lblValue1 = New System.Windows.Forms.Label()
        Me.txtValue1 = New System.Windows.Forms.TextBox()
        Me.lblValue2 = New System.Windows.Forms.Label()
        Me.txtValue2 = New System.Windows.Forms.TextBox()
        Me.cmdCalc = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.pnlTitle.SuspendLayout()
        Me.grpMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTitle
        '
        Me.pnlTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.pnlTitle.Controls.Add(Me.lblTitle)
        Me.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTitle.Location = New System.Drawing.Point(0, 0)
        Me.pnlTitle.Name = "pnlTitle"
        Me.pnlTitle.Size = New System.Drawing.Size(464, 35)
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
        Me.lblTitle.Size = New System.Drawing.Size(464, 35)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "ASSORTMENT CALCULATOR"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grpMain
        '
        Me.grpMain.BackColor = System.Drawing.Color.Transparent
        Me.grpMain.Controls.Add(Me.lblCts)
        Me.grpMain.Controls.Add(Me.txtCts)
        Me.grpMain.Controls.Add(Me.lblAssort1)
        Me.grpMain.Controls.Add(Me.cmbAssort1)
        Me.grpMain.Controls.Add(Me.lblAssort2)
        Me.grpMain.Controls.Add(Me.cmbAssort2)
        Me.grpMain.Controls.Add(Me.lblValue1)
        Me.grpMain.Controls.Add(Me.txtValue1)
        Me.grpMain.Controls.Add(Me.lblValue2)
        Me.grpMain.Controls.Add(Me.txtValue2)
        Me.grpMain.Controls.Add(Me.cmdCalc)
        Me.grpMain.Controls.Add(Me.cmdExit)
        Me.grpMain.Location = New System.Drawing.Point(5, 40)
        Me.grpMain.Name = "grpMain"
        Me.grpMain.Size = New System.Drawing.Size(447, 199)
        Me.grpMain.TabIndex = 1
        Me.grpMain.TabStop = False
        '
        'lblCts
        '
        Me.lblCts.BackColor = System.Drawing.Color.Transparent
        Me.lblCts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCts.Location = New System.Drawing.Point(10, 18)
        Me.lblCts.Name = "lblCts"
        Me.lblCts.Size = New System.Drawing.Size(40, 18)
        Me.lblCts.TabIndex = 0
        Me.lblCts.Text = "Cts"
        '
        'txtCts
        '
        Me.txtCts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtCts.Location = New System.Drawing.Point(10, 36)
        Me.txtCts.Name = "txtCts"
        Me.txtCts.Size = New System.Drawing.Size(80, 20)
        Me.txtCts.TabIndex = 1
        Me.txtCts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblAssort1
        '
        Me.lblAssort1.BackColor = System.Drawing.Color.Transparent
        Me.lblAssort1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblAssort1.Location = New System.Drawing.Point(105, 18)
        Me.lblAssort1.Name = "lblAssort1"
        Me.lblAssort1.Size = New System.Drawing.Size(100, 18)
        Me.lblAssort1.TabIndex = 2
        Me.lblAssort1.Text = "Assortment 1"
        '
        'cmbAssort1
        '
        Me.cmbAssort1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmbAssort1.Location = New System.Drawing.Point(105, 36)
        Me.cmbAssort1.Name = "cmbAssort1"
        Me.cmbAssort1.Size = New System.Drawing.Size(160, 21)
        Me.cmbAssort1.TabIndex = 3
        '
        'lblAssort2
        '
        Me.lblAssort2.BackColor = System.Drawing.Color.Transparent
        Me.lblAssort2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblAssort2.Location = New System.Drawing.Point(280, 18)
        Me.lblAssort2.Name = "lblAssort2"
        Me.lblAssort2.Size = New System.Drawing.Size(100, 18)
        Me.lblAssort2.TabIndex = 4
        Me.lblAssort2.Text = "Assortment 2"
        '
        'cmbAssort2
        '
        Me.cmbAssort2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmbAssort2.Location = New System.Drawing.Point(280, 36)
        Me.cmbAssort2.Name = "cmbAssort2"
        Me.cmbAssort2.Size = New System.Drawing.Size(160, 21)
        Me.cmbAssort2.TabIndex = 5
        '
        'lblValue1
        '
        Me.lblValue1.BackColor = System.Drawing.Color.Transparent
        Me.lblValue1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblValue1.Location = New System.Drawing.Point(105, 75)
        Me.lblValue1.Name = "lblValue1"
        Me.lblValue1.Size = New System.Drawing.Size(70, 18)
        Me.lblValue1.TabIndex = 6
        Me.lblValue1.Text = "Value 1"
        '
        'txtValue1
        '
        Me.txtValue1.BackColor = System.Drawing.Color.LightYellow
        Me.txtValue1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtValue1.Location = New System.Drawing.Point(105, 93)
        Me.txtValue1.Name = "txtValue1"
        Me.txtValue1.ReadOnly = True
        Me.txtValue1.Size = New System.Drawing.Size(160, 20)
        Me.txtValue1.TabIndex = 7
        Me.txtValue1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblValue2
        '
        Me.lblValue2.BackColor = System.Drawing.Color.Transparent
        Me.lblValue2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblValue2.Location = New System.Drawing.Point(280, 75)
        Me.lblValue2.Name = "lblValue2"
        Me.lblValue2.Size = New System.Drawing.Size(70, 18)
        Me.lblValue2.TabIndex = 8
        Me.lblValue2.Text = "Value 2"
        '
        'txtValue2
        '
        Me.txtValue2.BackColor = System.Drawing.Color.LightYellow
        Me.txtValue2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.txtValue2.Location = New System.Drawing.Point(280, 93)
        Me.txtValue2.Name = "txtValue2"
        Me.txtValue2.ReadOnly = True
        Me.txtValue2.Size = New System.Drawing.Size(160, 20)
        Me.txtValue2.TabIndex = 9
        Me.txtValue2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmdCalc
        '
        Me.cmdCalc.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdCalc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmdCalc.Location = New System.Drawing.Point(105, 140)
        Me.cmdCalc.Name = "cmdCalc"
        Me.cmdCalc.Size = New System.Drawing.Size(160, 30)
        Me.cmdCalc.TabIndex = 10
        Me.cmdCalc.Text = "Calculate"
        Me.cmdCalc.UseVisualStyleBackColor = False
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.cmdExit.Location = New System.Drawing.Point(280, 140)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(160, 30)
        Me.cmdExit.TabIndex = 11
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'frm_GRDRnd_AssortComp
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(216, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(464, 251)
        Me.Controls.Add(Me.pnlTitle)
        Me.Controls.Add(Me.grpMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_GRDRnd_AssortComp"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ASSORTMENT CALCULATOR"
        Me.pnlTitle.ResumeLayout(False)
        Me.grpMain.ResumeLayout(False)
        Me.grpMain.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlTitle As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents grpMain As System.Windows.Forms.GroupBox
    Friend WithEvents lblCts As System.Windows.Forms.Label
    Friend WithEvents txtCts As System.Windows.Forms.TextBox
    Friend WithEvents lblAssort1 As System.Windows.Forms.Label
    Friend WithEvents cmbAssort1 As System.Windows.Forms.ComboBox
    Friend WithEvents lblAssort2 As System.Windows.Forms.Label
    Friend WithEvents cmbAssort2 As System.Windows.Forms.ComboBox
    Friend WithEvents lblValue1 As System.Windows.Forms.Label
    Friend WithEvents txtValue1 As System.Windows.Forms.TextBox
    Friend WithEvents lblValue2 As System.Windows.Forms.Label
    Friend WithEvents txtValue2 As System.Windows.Forms.TextBox
    Friend WithEvents cmdCalc As System.Windows.Forms.Button
    Friend WithEvents cmdExit As System.Windows.Forms.Button

End Class