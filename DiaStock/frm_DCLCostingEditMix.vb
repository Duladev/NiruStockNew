
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLCostingEditMix

    Private Sub Load_SavedData()
        flxDetails.Rows.Clear()
        cmbClient.Items.Clear()
        rsComSql = New ADODB.Recordset
        mStrSQL = "SELECT TOP (100) PERCENT dbo.tblOrders.Niruref " & _
                  "FROM dbo.tblMixFinishOrders INNER JOIN dbo.tblOrders ON dbo.tblMixFinishOrders.OrderNo = dbo.tblOrders.OrderNo " & _
                  "WHERE (dbo.tblMixFinishOrders.Status = 'A') " & _
                  "GROUP BY dbo.tblOrders.Niruref " & _
                  "ORDER BY dbo.tblOrders.Niruref"
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbClient.Items.Add(rsComSql.Fields("Niruref").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_CostingData()
        If cmbClient.Text = "" Then MsgBox("Invalid Client No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        rsComSql = New ADODB.Recordset
        If cmbLineNo.Text <> "" And cmbOrderNo.Text <> "" Then
            mStrSQL = "SELECT TOP (100) PERCENT dbo.tblMixFinishOrders.OrderNo, dbo.tblMixFinishOrders.NLineNo, dbo.tblMixFinishOrders.PacketNo, dbo.tblMixFinishOrders.FinishedPcs, dbo.tblMixFinishOrders.ID, " & _
                            "dbo.tblOrders.Subject, dbo.tblOrders.Niruref, dbo.tblOrders.NorderNo, dbo.tblOrders.OrderItem, dbo.tblOrders.COMMANDE, dbo.tblMixFinishOrders.NLineNo2 " & _
                      "FROM dbo.tblMixFinishOrders INNER JOIN dbo.tblOrders ON dbo.tblMixFinishOrders.OrderNo = dbo.tblOrders.OrderNo " & _
                      "WHERE (dbo.tblMixFinishOrders.Status = 'A') AND (dbo.tblOrders.Niruref = '" & cmbClient.Text & "') AND (dbo.tblMixFinishOrders.NLineNo = '" & cmbLineNo.Text & "') AND (dbo.tblMixFinishOrders.OrderNo = '" & cmbOrderNo.Text & "') " & _
                      "ORDER BY dbo.tblMixFinishOrders.OrderNo, dbo.tblMixFinishOrders.PacketNo"
        ElseIf cmbLineNo.Text <> "" And cmbOrderNo.Text = "" Then
            mStrSQL = "SELECT TOP (100) PERCENT dbo.tblMixFinishOrders.OrderNo, dbo.tblMixFinishOrders.NLineNo, dbo.tblMixFinishOrders.PacketNo, dbo.tblMixFinishOrders.FinishedPcs, dbo.tblMixFinishOrders.ID, " & _
                            "dbo.tblOrders.Subject, dbo.tblOrders.Niruref, dbo.tblOrders.NorderNo, dbo.tblOrders.OrderItem, dbo.tblOrders.COMMANDE, dbo.tblMixFinishOrders.NLineNo2 " & _
                      "FROM dbo.tblMixFinishOrders INNER JOIN dbo.tblOrders ON dbo.tblMixFinishOrders.OrderNo = dbo.tblOrders.OrderNo " & _
                      "WHERE (dbo.tblMixFinishOrders.Status = 'A') AND (dbo.tblOrders.Niruref = '" & cmbClient.Text & "') AND (dbo.tblMixFinishOrders.NLineNo = '" & cmbLineNo.Text & "') " & _
                      "ORDER BY dbo.tblMixFinishOrders.OrderNo, dbo.tblMixFinishOrders.PacketNo"
        ElseIf cmbLineNo.Text = "" And cmbOrderNo.Text <> "" Then
            mStrSQL = "SELECT TOP (100) PERCENT dbo.tblMixFinishOrders.OrderNo, dbo.tblMixFinishOrders.NLineNo, dbo.tblMixFinishOrders.PacketNo, dbo.tblMixFinishOrders.FinishedPcs, dbo.tblMixFinishOrders.ID, " & _
                            "dbo.tblOrders.Subject, dbo.tblOrders.Niruref, dbo.tblOrders.NorderNo, dbo.tblOrders.OrderItem, dbo.tblOrders.COMMANDE, dbo.tblMixFinishOrders.NLineNo2 " & _
                      "FROM dbo.tblMixFinishOrders INNER JOIN dbo.tblOrders ON dbo.tblMixFinishOrders.OrderNo = dbo.tblOrders.OrderNo " & _
                      "WHERE (dbo.tblMixFinishOrders.Status = 'A') AND (dbo.tblOrders.Niruref = '" & cmbClient.Text & "') AND (dbo.tblMixFinishOrders.OrderNo = '" & cmbOrderNo.Text & "')" & _
                      "ORDER BY dbo.tblMixFinishOrders.OrderNo, dbo.tblMixFinishOrders.PacketNo"
        Else
            mStrSQL = "SELECT TOP (100) PERCENT dbo.tblMixFinishOrders.OrderNo, dbo.tblMixFinishOrders.NLineNo, dbo.tblMixFinishOrders.PacketNo, dbo.tblMixFinishOrders.FinishedPcs, dbo.tblMixFinishOrders.ID, " & _
                            "dbo.tblOrders.Subject, dbo.tblOrders.Niruref, dbo.tblOrders.NorderNo, dbo.tblOrders.OrderItem, dbo.tblOrders.COMMANDE, dbo.tblMixFinishOrders.NLineNo2 " & _
                      "FROM dbo.tblMixFinishOrders INNER JOIN dbo.tblOrders ON dbo.tblMixFinishOrders.OrderNo = dbo.tblOrders.OrderNo " & _
                      "WHERE (dbo.tblMixFinishOrders.Status = 'A') AND (dbo.tblOrders.Niruref = '" & cmbClient.Text & "') " & _
                      "ORDER BY dbo.tblMixFinishOrders.OrderNo, dbo.tblMixFinishOrders.PacketNo"
        End If
        
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("OrderNo").Value, rsComSql.Fields("PacketNo").Value, rsComSql.Fields("Niruref").Value,
                                    rsComSql.Fields("COMMANDE").Value, rsComSql.Fields("NorderNo").Value, rsComSql.Fields("OrderItem").Value,
                                    rsComSql.Fields("Subject").Value, rsComSql.Fields("NLineNo").Value, rsComSql.Fields("NLineNo2").Value,
                                    rsComSql.Fields("FinishedPcs").Value, rsComSql.Fields("ID").Value, False)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtPcs.Text = CalTotalPcs(flxDetails)
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ClearFields()
        flxDetails.Rows.Clear()
        cmbClient.Items.Clear()
        cmbClient.Text = ""
        cmbLineNo.Items.Clear()
        cmbLineNo.Text = ""
        cmbOrderNo.Items.Clear()
        cmbOrderNo.Text = ""
        txtPcs.Text = ""
        txtLineNo.Text = ""
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
        Load_SavedData()
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        If cmbClient.Text = "" Then MsgBox("Invalid Client No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtLineNo.Text = "" Then MsgBox("Invalid Line No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT dbo.tblOrders.Niruref, dbo.tblOrdersDtls.NLineNo " & _
                          "FROM dbo.tblOrders INNER JOIN dbo.tblOrdersDtls ON dbo.tblOrders.OrderNo = dbo.tblOrdersDtls.OrderNo " & _
                          "WHERE (dbo.tblOrders.Niruref = '" & cmbClient.Text & "') AND (dbo.tblOrdersDtls.NLineNo = '" & txtLineNo.Text & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Line No. for Client", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(11, intRow).Value = True Then
                    AdoCN.Execute("UPDATE tblMixFinishOrders SET NLineNo2 = '" & txtLineNo.Text & "' " & _
                                  "WHERE ID = " & CDbl(flxDetails.Item(10, intRow).Value) & "")
                End If
            Next

            MsgBox("Line No. Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()

        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(11).EditedFormattedValue = True Or flxSample.Rows(intRow).Cells(11).EditedFormattedValue = 1 Then
                CalTotalPcs = CalTotalPcs + Val(flxSample.Item(9, intRow).Value)
            End If
        Next
    End Function

    Private Sub frm_DCLCostingEditMix_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_SavedData()
    End Sub

    Private Sub cmbClient_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbClient.SelectedIndexChanged
        flxDetails.Rows.Clear()
        cmbLineNo.Items.Clear()
        cmbLineNo.Text = ""
        
        rsComSql = New ADODB.Recordset
        mStrSQL = "SELECT TOP (100) PERCENT dbo.tblMixFinishOrders.NLineNo " & _
                  "FROM dbo.tblMixFinishOrders INNER JOIN dbo.tblOrders ON dbo.tblMixFinishOrders.OrderNo = dbo.tblOrders.OrderNo " & _
                  "WHERE (dbo.tblMixFinishOrders.Status = 'A') AND (dbo.tblOrders.Niruref = '" & cmbClient.Text & "') " & _
                  "GROUP BY dbo.tblMixFinishOrders.NLineNo " & _
                  "ORDER BY dbo.tblMixFinishOrders.NLineNo"
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbLineNo.Items.Add(rsComSql.Fields("NLineNo").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        cmbOrderNo.Items.Clear()
        cmbOrderNo.Text = ""
        rsComSql = New ADODB.Recordset
        mStrSQL = "SELECT TOP (100) PERCENT dbo.tblMixFinishOrders.OrderNo " & _
                  "FROM dbo.tblMixFinishOrders INNER JOIN dbo.tblOrders ON dbo.tblMixFinishOrders.OrderNo = dbo.tblOrders.OrderNo " & _
                  "WHERE (dbo.tblMixFinishOrders.Status = 'A') AND (dbo.tblOrders.Niruref = '" & cmbClient.Text & "') " & _
                  "GROUP BY dbo.tblMixFinishOrders.OrderNo " & _
                  "ORDER BY dbo.tblMixFinishOrders.OrderNo"
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbOrderNo.Items.Add(rsComSql.Fields("OrderNo").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        Load_CostingData()
    End Sub

    Private Sub cmbLineNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLineNo.SelectedIndexChanged
        flxDetails.Rows.Clear()
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
        txtPcs.Text = CalTotalPcs(flxDetails)
    End Sub

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 11 Then
            txtPcs.Text = CalTotalPcs(flxDetails)
        End If
    End Sub
End Class