
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_BagTransfer

    Private Sub frm_BagTransfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Packets()
    End Sub

    Private Sub Load_Packets()

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblBAGReturns WHERE Rgh_Trf = 0 AND PCUPcs > 0 ORDER BY ParNo, PktNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("PCUPcs").Value,
                                    rsComSql.Fields("PCUPCts").Value,
                                    rsComSql.Fields("Sec").Value, False)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        dtpToday = GetToday()

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(5, intRow).Value = True Then
                    AdoCN.Execute("UPDATE tblBAGReturns SET Rgh_Trf = 1,Rgh_Trf_User = '" & PBUser_EmpNo & "'," & _
                                    "Rgh_Trf_Date = '" & Format(dtpToday, "MM/dd/yyyy") & "',Rgh_Trf_Time = '" & Format(Date.Now, "HH:mm:ss") & "' " & _
                                  "WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND " & _
                                    "Sec = " & CInt(flxDetails.Item(4, intRow).Value) & " AND PCUPcs > 0")
                End If
            Next

            MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Load_Packets()
        End If
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(5, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(5, intRow).Value = False
            Next
        End If

        txtPcs.Text = CalTotalPcs(flxDetails, 2)
        txtCts.Text = CalTotalCts(flxDetails, 3)
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        Save()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(5).EditedFormattedValue = True Then
                CalTotalPcs = CalTotalPcs + Val(flxSample.Item(intCalRow, intRow).Value)
            End If
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(5).EditedFormattedValue = True Then
                CalTotalCts = CalTotalCts + Val(flxSample.Item(intCalRow, intRow).Value)
            End If
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 5 Then
            txtPcs.Text = CalTotalPcs(flxDetails, 2)
            txtCts.Text = CalTotalCts(flxDetails, 3)
        End If
    End Sub
End Class