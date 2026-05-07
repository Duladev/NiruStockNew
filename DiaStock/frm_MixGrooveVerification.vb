
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixGrooveVerification
    Private Sub Load_Orders()
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        If cmbClient.Text <> "" Then
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixFinishOrders.OrderNo, dbo.tblOrders.Subject + ' ' + dbo.tblOrders.Subject2 AS Subject, dbo.tblMixFinishOrders.NLineNo, dbo.tblMixFinishOrders.Reference, " & _
                          "dbo.tblMixFinishOrders.Side, SUM(dbo.tblMixFinishOrders.FinishedPcs) AS FinishedPcs, dbo.tblOrdersDtls.Groove, dbo.tblOrdersDtls.GrCount, " & _
                          "dbo.tblOrdersDtls.GrDone, dbo.tblOrdersDtls.Laser, dbo.tblOrders.Niruref " & _
                      "FROM dbo.tblMixFinishOrders INNER JOIN dbo.tblOrdersDtls ON dbo.tblMixFinishOrders.OrderNo = dbo.tblOrdersDtls.OrderNo AND dbo.tblMixFinishOrders.Reference = dbo.tblOrdersDtls.RefNo AND " & _
                          "dbo.tblMixFinishOrders.Side = dbo.tblOrdersDtls.Side INNER JOIN dbo.tblOrders ON dbo.tblMixFinishOrders.OrderNo = dbo.tblOrders.OrderNo " & _
                      "WHERE (dbo.tblMixFinishOrders.Status = 'A') AND (dbo.tblOrders.Niruref = '" & cmbClient.Text & "') " & _
                      "GROUP BY dbo.tblMixFinishOrders.OrderNo, dbo.tblMixFinishOrders.NLineNo, dbo.tblMixFinishOrders.Reference, dbo.tblMixFinishOrders.Side, dbo.tblOrdersDtls.Groove, " & _
                          "dbo.tblOrdersDtls.GrCount, dbo.tblOrdersDtls.GrDone, dbo.tblOrders.Subject, dbo.tblOrders.Subject2, dbo.tblOrdersDtls.Laser, dbo.tblOrders.Niruref " & _
                      "HAVING (dbo.tblOrdersDtls.Groove = 1) " & _
                      "ORDER BY dbo.tblMixFinishOrders.OrderNo, dbo.tblMixFinishOrders.NLineNo, dbo.tblMixFinishOrders.Reference, dbo.tblMixFinishOrders.Side", AdoCN, 1, 1)
        Else
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixFinishOrders.OrderNo, dbo.tblOrders.Subject + ' ' + dbo.tblOrders.Subject2 AS Subject, dbo.tblMixFinishOrders.NLineNo, dbo.tblMixFinishOrders.Reference, " & _
                          "dbo.tblMixFinishOrders.Side, SUM(dbo.tblMixFinishOrders.FinishedPcs) AS FinishedPcs, dbo.tblOrdersDtls.Groove, dbo.tblOrdersDtls.GrCount, " & _
                          "dbo.tblOrdersDtls.GrDone, dbo.tblOrdersDtls.Laser, dbo.tblOrders.Niruref " & _
                      "FROM dbo.tblMixFinishOrders INNER JOIN dbo.tblOrdersDtls ON dbo.tblMixFinishOrders.OrderNo = dbo.tblOrdersDtls.OrderNo AND dbo.tblMixFinishOrders.Reference = dbo.tblOrdersDtls.RefNo AND " & _
                          "dbo.tblMixFinishOrders.Side = dbo.tblOrdersDtls.Side INNER JOIN dbo.tblOrders ON dbo.tblMixFinishOrders.OrderNo = dbo.tblOrders.OrderNo " & _
                      "WHERE (dbo.tblMixFinishOrders.Status = 'A') " & _
                      "GROUP BY dbo.tblMixFinishOrders.OrderNo, dbo.tblMixFinishOrders.NLineNo, dbo.tblMixFinishOrders.Reference, dbo.tblMixFinishOrders.Side, dbo.tblOrdersDtls.Groove, " & _
                          "dbo.tblOrdersDtls.GrCount, dbo.tblOrdersDtls.GrDone, dbo.tblOrders.Subject, dbo.tblOrders.Subject2, dbo.tblOrdersDtls.Laser, dbo.tblOrders.Niruref " & _
                      "HAVING (dbo.tblOrdersDtls.Groove = 1) " & _
                      "ORDER BY dbo.tblMixFinishOrders.OrderNo, dbo.tblMixFinishOrders.NLineNo, dbo.tblMixFinishOrders.Reference, dbo.tblMixFinishOrders.Side", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("OrderNo").Value,
                                    rsComSql.Fields("Subject").Value,
                                    rsComSql.Fields("NLineNo").Value,
                                    rsComSql.Fields("Reference").Value,
                                    rsComSql.Fields("Side").Value,
                                    rsComSql.Fields("FinishedPcs").Value,
                                    rsComSql.Fields("Groove").Value,
                                    rsComSql.Fields("GrCount").Value,
                                    rsComSql.Fields("GrCount").Value * 5 * rsComSql.Fields("FinishedPcs").Value,
                                    IIf(rsComSql.Fields("GrDone").Value = 1, True, False),
                                    rsComSql.Fields("Laser").Value,
                                    rsComSql.Fields("Niruref").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub frm_MixGrooveVerification_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Orders()
        Load_Client()
        Calculate()
    End Sub

    Private Sub Load_Client()
        cmbClient.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT NiruCust FROM tblNiruRef ORDER BY NiruCust", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            Do While Not rsComSql.EOF
                cmbClient.Items.Add(rsComSql.Fields("NiruCust").Value)
                rsComSql.MoveNext()
            Loop
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Calculate()
        Dim intRow As Integer
        Dim vPcs As Double
        Dim vCts As Double
        Dim vACts As Double

        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Rows(intRow).Cells(9).EditedFormattedValue = True Then
                vPcs = vPcs + CDbl(flxDetails.Item(5, intRow).Value)
                vCts = vCts + CDbl(flxDetails.Item(6, intRow).Value)
                vACts = vACts + CDbl(flxDetails.Item(8, intRow).Value)
            End If
        Next
        txtTotPcs.Text = vPcs
        txtTotGr.Text = vCts
        txtTotLab.Text = Format(vACts, "#0.00")
    End Sub

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 9 Then
            Calculate()
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        Load_Orders()
        Calculate()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        For intRow = 0 To flxDetails.Rows.Count - 1
            AdoCN.Execute("UPDATE tblOrdersDtls SET GrDone = " & IIf(flxDetails.Item(9, intRow).Value = True, 1, 0) & " " & _
                          "WHERE OrderNo = " & Trim(flxDetails.Item(0, intRow).Value) & " AND RefNo = '" & Replace(Trim(flxDetails.Item(3, intRow).Value), "'", "''") & "' AND Side = '" & Trim(flxDetails.Item(4, intRow).Value) & "'")

        Next

        MsgBox("Order Updated Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        flxDetails.Rows.Clear()
        Calculate()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(9, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(9, intRow).Value = False
            Next
        End If
        Calculate()
    End Sub
End Class