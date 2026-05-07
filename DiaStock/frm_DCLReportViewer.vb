
Public Class frm_DCLReportViewer

    Private Sub frm_DCLReportViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error GoTo ErrorHandler
        Dim objReportDocument As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
        objReportDocument.Load(strReportPath)
        'strRecordSelectionFormula = mRecordSelectionFormula
        'objReportDocument.RecordSelectionFormula = strRecordSelectionFormula
        CRViewer1.ReportSource = objReportDocument

        Exit Sub
ErrorHandler:
        MsgBox(Err.Description, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
    End Sub
End Class