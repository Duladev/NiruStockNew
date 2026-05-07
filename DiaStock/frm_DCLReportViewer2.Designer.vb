<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_DCLReportViewer2
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
        Me.CRViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.SuspendLayout()
        '
        'CRViewer1
        '
        Me.CRViewer1.ActiveViewIndex = -1
        Me.CRViewer1.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.CRViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CRViewer1.Cursor = System.Windows.Forms.Cursors.Default
        Me.CRViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CRViewer1.Location = New System.Drawing.Point(0, 0)
        Me.CRViewer1.Name = "CRViewer1"
        Me.CRViewer1.ShowLogo = False
        Me.CRViewer1.Size = New System.Drawing.Size(731, 512)
        Me.CRViewer1.TabIndex = 1
        Me.CRViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        '
        'frm_DCLReportViewer2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(731, 512)
        Me.Controls.Add(Me.CRViewer1)
        Me.Name = "frm_DCLReportViewer2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "REPORT VIEWER"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CRViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
