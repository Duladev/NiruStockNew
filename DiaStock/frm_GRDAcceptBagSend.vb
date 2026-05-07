
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDAcceptBagSend

    Private Sub Load_Parcels()

        flxParcel.Rows.Clear()
        flxAvailable.Rows.Clear()
        flxSelected.Rows.Clear()

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT ParNo, MAX(IssDate) AS IssDate " & _
                      "FROM dbo.tblBAGIssues " & _
                      "WHERE (Sec = 4) AND (GrdTrf = 2) " & _
                      "GROUP BY ParNo " & _
                      "ORDER BY ParNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxParcel.Rows.Add(rsComSql.Fields("ParNo").Value,
                                   Format(rsComSql.Fields("IssDate").Value, "yyyy/MM/dd"))
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub flxParcel_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxParcel.CellClick
        Dim selected_parno As String

        If flxParcel.Rows.Count > 0 Then
            selected_parno = flxParcel.Item(0, flxParcel.CurrentRow.Index).Value
            flxAvailable.Rows.Clear()
            flxSelected.Rows.Clear()

            txtSearch.Text = selected_parno
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT ParNo, PktNo, IssPcsT + IssPcsB AS IssPcs, IssCts " & _
                          "FROM dbo.tblBAGIssues " & _
                          "WHERE ParNo = '" & selected_parno & "' AND GrdTrf = 2 AND Sec = 4 " & _
                          "ORDER BY ParNo,PktNo", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    flxAvailable.Rows.Add(rsComSql.Fields("ParNo").Value,
                                          rsComSql.Fields("PktNo").Value,
                                          rsComSql.Fields("IssPcs").Value,
                                          rsComSql.Fields("IssCts").Value,
                                          rsComSql.Fields("IssPcs").Value,
                                          rsComSql.Fields("IssCts").Value)
                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub flxAvailable_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxAvailable.CellClick
        Dim intRow As Integer

        For intRow = 0 To flxSelected.Rows.Count - 1
            If flxAvailable.Item(0, flxAvailable.CurrentRow.Index).Value = flxSelected.Item(0, intRow).Value And flxAvailable.Item(1, flxAvailable.CurrentRow.Index).Value = flxSelected.Item(1, intRow).Value Then
                MsgBox("Already Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        flxSelected.Rows.Add(flxAvailable.Item(0, flxAvailable.CurrentRow.Index).Value,
                             flxAvailable.Item(1, flxAvailable.CurrentRow.Index).Value,
                             flxAvailable.Item(2, flxAvailable.CurrentRow.Index).Value,
                             flxAvailable.Item(3, flxAvailable.CurrentRow.Index).Value,
                             flxAvailable.Item(4, flxAvailable.CurrentRow.Index).Value,
                             flxAvailable.Item(5, flxAvailable.CurrentRow.Index).Value)

        flxAvailable.Rows.RemoveAt(flxAvailable.CurrentRow.Index)
        txtPcs.Text = CalTotalPcs(flxSelected)
        txtCts.Text = CalTotalCts(flxSelected)
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(2, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(3, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub flxSelected_DoubleClick(sender As Object, e As EventArgs) Handles flxSelected.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxSelected.Rows.RemoveAt(flxSelected.CurrentRow.Index)
            txtPcs.Text = CalTotalPcs(flxSelected)
            txtCts.Text = CalTotalCts(flxSelected)
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ClearFields()
        txtSearch.Text = ""
        flxParcel.Rows.Clear()
        flxAvailable.Rows.Clear()
        flxSelected.Rows.Clear()
        txtPcs.Text = ""
        txtCts.Text = ""
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
        Load_Parcels()
    End Sub

    Private Sub SaveAccept()
        Dim u As Integer

        For u = 0 To flxSelected.Rows.Count - 1
            AdoCN.Execute("UPDATE tblBAGIssues SET GrdTrf = 3,SendBy = '" & PBUser_EmpNo & "' WHERE ParNo = '" & flxSelected.Item(0, u).Value & "' AND PktNo = '" & flxSelected.Item(1, u).Value & "' AND GrdTrf = 2 AND Sec = 4")
        Next

        MsgBox("Packets Sent Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        ClearFields()
        Load_Parcels()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        SaveAccept()
    End Sub

    Private Sub frm_GRDAcceptBagSend_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Parcels()
    End Sub
End Class