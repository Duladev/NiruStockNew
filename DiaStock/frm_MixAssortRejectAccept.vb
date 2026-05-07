
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixAssortRejectAccept
    Dim strFolderPath As String

    Private Sub Load_Rejects()

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        If chkAll.Checked = True Then
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixReturns.ParNo, dbo.tblMixReturns.PktNo, dbo.tblMixReturns.Sec, dbo.tblMixReturns.EmpNo, dbo.tblMixReturns.RejPcs, dbo.tblMixPacket.Sarine, " & _
                                "dbo.tblMixReturns.RejCts, dbo.tblMixReturns.RghCts, dbo.tblMixPacket.AssortNo, dbo.tblMixReturns.RejReason, dbo.tblMixReturns.RetDate, dbo.tblMixReturns.RetTime, dbo.tblMixPacket.Grp, dbo.tblMixReturns.ID, dbo.tblMixPacket.PktRefNo " & _
                          "FROM dbo.tblMixReturns INNER JOIN dbo.tblMixPacket ON dbo.tblMixReturns.ParNo = dbo.tblMixPacket.PktOrdNo AND dbo.tblMixReturns.PktNo = dbo.tblMixPacket.PktNo " & _
                          "WHERE (dbo.tblMixReturns.RejStatus = 1) AND (dbo.tblMixReturns.RejPcs > 0) AND (dbo.tblMixReturns.RejReason <> 'DFI Refer Reject') " & _
                          "ORDER BY dbo.tblMixReturns.ParNo, dbo.tblMixReturns.PktNo, dbo.tblMixReturns.Sec, dbo.tblMixReturns.RejReason", AdoCN, 1, 1)
        Else
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixReturns.ParNo, dbo.tblMixReturns.PktNo, dbo.tblMixReturns.Sec, dbo.tblMixReturns.EmpNo, dbo.tblMixReturns.RejPcs, dbo.tblMixPacket.Sarine, " & _
                                "dbo.tblMixReturns.RejCts, dbo.tblMixReturns.RghCts, dbo.tblMixPacket.AssortNo, dbo.tblMixReturns.RejReason, dbo.tblMixReturns.RetDate, dbo.tblMixReturns.RetTime, dbo.tblMixPacket.Grp, dbo.tblMixReturns.ID, dbo.tblMixPacket.PktRefNo " & _
                          "FROM dbo.tblMixReturns INNER JOIN dbo.tblMixPacket ON dbo.tblMixReturns.ParNo = dbo.tblMixPacket.PktOrdNo AND dbo.tblMixReturns.PktNo = dbo.tblMixPacket.PktNo " & _
                          "WHERE (dbo.tblMixReturns.RejStatus = 1) AND (dbo.tblMixReturns.RejPcs > 0) AND (dbo.tblMixReturns.RejReason <> 'DFI Refer Reject') AND (dbo.tblMixReturns.RetDate = '" & Format(dtpDate.Value, "MM/dd/yyyy") & "') " & _
                          "ORDER BY dbo.tblMixReturns.ParNo, dbo.tblMixReturns.PktNo, dbo.tblMixReturns.Sec, dbo.tblMixReturns.RejReason", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("RejPcs").Value,
                                    rsComSql.Fields("RejCts").Value,
                                    rsComSql.Fields("AssortNo").Value,
                                    rsComSql.Fields("Sec").Value,
                                    rsComSql.Fields("EmpNo").Value,
                                    rsComSql.Fields("RejReason").Value,
                                    Format(rsComSql.Fields("RetDate").Value, "yyyy/MM/dd"),
                                    rsComSql.Fields("Grp").Value,
                                    rsComSql.Fields("ID").Value,
                                    False,
                                    Format(rsComSql.Fields("RetTime").Value, "HH:mm"),
                                    rsComSql.Fields("PktRefNo").Value,
                                    rsComSql.Fields("Sarine").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

    End Sub

    Private Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        Load_Rejects()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(11).EditedFormattedValue = True Or flxSample.Rows(intRow).Cells(11).EditedFormattedValue = 1 Then
                CalTotalPcs = CalTotalPcs + Val(flxSample.Item(2, intRow).Value)
            End If
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(11).EditedFormattedValue = True Or flxSample.Rows(intRow).Cells(11).EditedFormattedValue = 1 Then
                CalTotalCts = CalTotalCts + Val(flxSample.Item(3, intRow).Value)
            End If
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)

    End Function

    Private Sub Save()
        Dim intRow As Integer

        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(11, intRow).Value = True Then
                AdoCN.Execute("UPDATE tblMixReturns SET RejStatus = 2 WHERE ID = " & CDbl(flxDetails.Item(10, intRow).Value) & "")
            End If
        Next

        ClearFields()
        Load_Rejects()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub ClearFields()
        txtTotPcs.Text = ""
        txtTotCts.Text = ""
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixAssortRejectDetails.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixRejectsStatus1.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub frm_MixAssortRejectAccept_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        If strDBName = "DiaStock" Then
            strFolderPath = "Mix\"
        Else
            strFolderPath = "DiaSalesMix\"
        End If

        dtpDate.Value = DateAdd(DateInterval.Day, -1, Date.Now)
    End Sub

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 11 Then
            txtTotPcs.Text = CalTotalPcs(flxDetails)
            txtTotCts.Text = CalTotalCts(flxDetails)
        End If
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(11, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(11, intRow).Value = False
            Next
        End If
        txtTotPcs.Text = CalTotalPcs(flxDetails)
        txtTotCts.Text = CalTotalCts(flxDetails)
    End Sub
End Class