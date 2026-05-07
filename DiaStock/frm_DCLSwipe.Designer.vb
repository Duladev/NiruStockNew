<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DCLSwipe
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
        Me.cmdCancel = New DiaStock.HazelDev_Button()
        Me.cmdSwipe = New DiaStock.HazelDev_Button()
        Me.lblLabel3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdCancel.FlatAppearance.BorderSize = 0
        Me.cmdCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.Color.White
        Me.cmdCancel.Location = New System.Drawing.Point(146, 35)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(99, 28)
        Me.cmdCancel.TabIndex = 47
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdSwipe
        '
        Me.cmdSwipe.BackColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSwipe.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(66, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.cmdSwipe.FlatAppearance.BorderSize = 0
        Me.cmdSwipe.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(179, Byte), Integer))
        Me.cmdSwipe.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(188, Byte), Integer))
        Me.cmdSwipe.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSwipe.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSwipe.ForeColor = System.Drawing.Color.White
        Me.cmdSwipe.Location = New System.Drawing.Point(24, 35)
        Me.cmdSwipe.Name = "cmdSwipe"
        Me.cmdSwipe.Size = New System.Drawing.Size(99, 28)
        Me.cmdSwipe.TabIndex = 46
        Me.cmdSwipe.Text = "Swipe"
        Me.cmdSwipe.UseVisualStyleBackColor = False
        '
        'lblLabel3
        '
        Me.lblLabel3.AutoSize = True
        Me.lblLabel3.Location = New System.Drawing.Point(24, 75)
        Me.lblLabel3.Name = "lblLabel3"
        Me.lblLabel3.Size = New System.Drawing.Size(0, 13)
        Me.lblLabel3.TabIndex = 48
        '
        'frm_DCLSwipe
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(267, 107)
        Me.Controls.Add(Me.lblLabel3)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdSwipe)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.Name = "frm_DCLSwipe"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Swipe"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdCancel As DiaStock.HazelDev_Button
    Friend WithEvents cmdSwipe As DiaStock.HazelDev_Button
    Friend WithEvents lblLabel3 As System.Windows.Forms.Label
End Class
