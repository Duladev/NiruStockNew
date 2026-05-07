
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixBoiling
    Dim strFolderPath As String

    Private Sub ClearFields()
        GetNextBoilingtNo()
        Load_Packets()
        chkSelect.Checked = False
        txtTotPcs.Text = ""
        txtTotCount.Text = ""
    End Sub

    Private Sub Load_Packets()

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixIssuesRep.ID, dbo.tblMixIssuesRep.ParNo, dbo.tblMixIssuesRep.PktNo, dbo.tblMixIssuesRep.Sec, dbo.tblMixIssuesRep.IssPcs," & _
                        "dbo.tblMixIssuesRep.EmpNo, dbo.tblMixIssuesRep.IssDate, dbo.tblMixIssuesRep.IssTime, dbo.tblMixIssuesRep.EmpNo2, dbo.tblMixIssuesRep.OK," & _
                        "dbo.tblMixIssuesRep.BatchNo, dbo.tblMixIssuesRep.SendDate, dbo.tblMixIssuesRep.SendTime " & _
                      "FROM dbo.tblMixIssuesRep LEFT OUTER JOIN dbo.tblMixReturnsRep ON dbo.tblMixIssuesRep.ID = dbo.tblMixReturnsRep.IssueID AND dbo.tblMixIssuesRep.ParNo = dbo.tblMixReturnsRep.ParNo AND " & _
                        "dbo.tblMixIssuesRep.PktNo = dbo.tblMixReturnsRep.PktNo AND dbo.tblMixIssuesRep.Sec = dbo.tblMixReturnsRep.Sec " & _
                      "WHERE (dbo.tblMixIssuesRep.Sec = 1) AND (dbo.tblMixIssuesRep.OK = 0) AND (dbo.tblMixReturnsRep.PktNo IS NULL) " & _
                      "ORDER BY dbo.tblMixIssuesRep.ParNo, dbo.tblMixIssuesRep.PktNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("IssPcs").Value,
                                    Format(rsComSql.Fields("IssDate").Value, "yyyy/MM/dd"),
                                    Format(rsComSql.Fields("IssTime").Value, "HH:mm"),
                                    False,
                                    rsComSql.Fields("ID").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub frm_MixBoiling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        ClearFields()
    End Sub

    Private Sub GetNextBoilingtNo()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(BatchNo) AS MaxNo FROM tblMixIssuesRep WHERE (Sec = 1)", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("MaxNo").Value) Then
                txtBoilingNo.Text = rsComSql.Fields("MaxNo").Value + 1
            Else
                txtBoilingNo.Text = "1"
            End If
        End If
        rsComSql = Nothing
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(5).EditedFormattedValue = True Then
                CalTotalPcs = CalTotalPcs + Val(flxSample.Item(intCalRow, intRow).Value)
            End If
        Next
        Return CalTotalPcs

    End Function

    Private Function CalTotalCount(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalCount = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(5).EditedFormattedValue = True Then
                CalTotalCount = CalTotalCount + 1
            End If
        Next
        Return CalTotalCount

    End Function

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 5 Then
            txtTotPcs.Text = CalTotalPcs(flxDetails, 2)
            txtTotCount.Text = CalTotalCount(flxDetails)
        End If
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(5, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(5, intRow).Value = False
            Next
        End If
        txtTotPcs.Text = CalTotalPcs(flxDetails, 2)
        txtTotCount.Text = CalTotalCount(flxDetails)
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If txtBoilingNo.Text <> "" Then
                For intRow = 0 To flxDetails.Rows.Count - 1
                    If flxDetails.Item(5, intRow).Value = True Then
                        AdoCN.Execute("UPDATE tblMixIssuesRep SET OK = 1, BatchNo = " & CDbl(txtBoilingNo.Text) & ",SendDate = '" & Format(Date.Now, "MM/dd/yyyy") & "',SendTime = '" & Format(Date.Now, "HH:mm") & "' " & _
                                      "WHERE ID = " & CDbl(flxDetails.Item(6, intRow).Value) & "")
                    End If
                Next

                MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

                objForm = New frm_DCLReportViewer
                mReportName = "crptMixBoilingIssRec.rpt"
                strReportPath = PBReportPath & strFolderPath & mReportName
                objForm.Show()

                ClearFields()
            Else
                MsgBox("Invalid Boiling No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdSticker_Click(sender As Object, e As EventArgs) Handles cmdSticker.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixBoilingIssRec.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        ClearFields()
    End Sub
End Class