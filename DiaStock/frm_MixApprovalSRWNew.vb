
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixApprovalSRWNew
    Dim strFolderPath As String

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ClearFields()
        chkSelect.Checked = False
        flxDetails.Rows.Clear()
    End Sub

    Private Sub Load_Details()
        Dim dblStoneCost As Double
        Dim dblProfit As Double

        flxDetails.Rows.Clear()
        dblProfit = 0

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixPacketApproval.ID, dbo.tblMixPacketApproval.PktOrdNo, dbo.tblMixPacketApproval.PktRefNo, dbo.tblMixPacketApproval.Pktside, dbo.tblMixPacketApproval.AssortNo, " & _
                        "dbo.tblMixPacketApproval.PktPcs, dbo.tblMixPacketApproval.PktCts, dbo.tblMixPacketApproval.AvgCost, dbo.tblMixPacketApproval.MaxCost, dbo.tblMixPacketApproval.DDate, dbo.tblMixPacketApproval.Approve, " & _
                        "dbo.tblMixPacketApproval.SystemDateTime, dbo.tblOrders.Subject, dbo.tblOrders.Subject2, dbo.tblOrdersDtls.Length, dbo.tblOrdersDtls.Width, dbo.tblOrders.DueDate " & _
                      "FROM dbo.tblMixPacketApproval INNER JOIN dbo.tblOrders ON dbo.tblMixPacketApproval.PktOrdNo = dbo.tblOrders.OrderNo INNER JOIN " & _
                        "dbo.tblOrdersDtls ON dbo.tblMixPacketApproval.PktOrdNo = dbo.tblOrdersDtls.OrderNo AND dbo.tblMixPacketApproval.PktRefNo = dbo.tblOrdersDtls.RefNo AND  " & _
                        "dbo.tblMixPacketApproval.Pktside = dbo.tblOrdersDtls.Side " & _
                      "WHERE(dbo.tblMixPacketApproval.Approve = 0) " & _
                      "ORDER BY dbo.tblMixPacketApproval.PktOrdNo, dbo.tblMixPacketApproval.PktRefNo, dbo.tblMixPacketApproval.Pktside", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                dblStoneCost = Math.Round(rsComSql.Fields("AvgCost").Value, 2)
                dblProfit = Math.Round(((rsComSql.Fields("MaxCost").Value - dblStoneCost) / rsComSql.Fields("MaxCost").Value) * 100, 2)

                flxDetails.Rows.Add(rsComSql.Fields("PktOrdNo").Value,
                                    rsComSql.Fields("Subject").Value & " " & rsComSql.Fields("Subject2").Value,
                                    rsComSql.Fields("PktRefNo").Value,
                                    rsComSql.Fields("Pktside").Value,
                                    rsComSql.Fields("AssortNo").Value,
                                    rsComSql.Fields("PktPcs").Value,
                                    rsComSql.Fields("PktCts").Value,
                                    Math.Round(rsComSql.Fields("PktCts").Value / rsComSql.Fields("PktPcs").Value, 3),
                                    dblStoneCost,
                                    rsComSql.Fields("MaxCost").Value,
                                    False,
                                    rsComSql.Fields("ID").Value,
                                    rsComSql.Fields("Length").Value,
                                    rsComSql.Fields("Width").Value,
                                    dblProfit,
                                    Format(rsComSql.Fields("DueDate").Value, "yyyy-MM-dd"))
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
        Load_Details()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub Approve(ByVal intResponse As Integer)
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(10, intRow).Value = True Then
                    AdoCN.Execute("UPDATE tblMixPacketApproval SET Approve = '" & intResponse & "' WHERE ID = '" & flxDetails.Item(11, intRow).Value & "'")
                End If
            Next

            MsgBox("Successfully Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Load_Details()
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Approve(1)
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(10, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(10, intRow).Value = False
            Next
        End If
    End Sub

    Private Sub frm_MixApprovalSRWNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        Load_Details()
    End Sub

    Private Sub cmdSaveAll_Click(sender As Object, e As EventArgs) Handles cmdSaveAll.Click
        Approve(2)
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixPacketApproval.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub
End Class