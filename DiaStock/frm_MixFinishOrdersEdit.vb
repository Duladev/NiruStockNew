
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixFinishOrdersEdit

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_PolishedReturns()
        Dim dblDiffCts As Double

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        If txtOrder.Text = "" Then
            mStrSQL = "SELECT TOP (100) PERCENT dbo.tblMIXFinishOrders.OrderNo, SUM(dbo.tblMIXFinishOrders.FinishedPcs) AS Pcs, " & _
                        "ROUND(SUM(dbo.tblMIXFinishOrders.SysFinCts), 3) AS Cts, ROUND(SUM(dbo.tblMIXFinishOrders.FinishedCts), 3) AS ActCts, dbo.tblOrders.Subject, dbo.tblMIXFinishOrders.Reference, dbo.tblMIXFinishOrders.Side " & _
                      "FROM dbo.tblMIXFinishOrders INNER JOIN dbo.tblOrders ON dbo.tblMIXFinishOrders.OrderNo = dbo.tblOrders.OrderNo " & _
                      "WHERE (dbo.tblMIXFinishOrders.Status = 'A') " & _
                      "GROUP BY dbo.tblMIXFinishOrders.OrderNo, dbo.tblOrders.Subject, dbo.tblMIXFinishOrders.Reference, dbo.tblMIXFinishOrders.Side " & _
                      "ORDER BY dbo.tblMIXFinishOrders.OrderNo, dbo.tblMIXFinishOrders.Reference, dbo.tblMIXFinishOrders.Side"
        Else
            mStrSQL = "SELECT TOP (100) PERCENT dbo.tblMIXFinishOrders.OrderNo, SUM(dbo.tblMIXFinishOrders.FinishedPcs) AS Pcs, " & _
                        "ROUND(SUM(dbo.tblMIXFinishOrders.SysFinCts), 3) AS Cts, ROUND(SUM(dbo.tblMIXFinishOrders.FinishedCts), 3) AS ActCts, dbo.tblOrders.Subject, dbo.tblMIXFinishOrders.Reference, dbo.tblMIXFinishOrders.Side " & _
                      "FROM dbo.tblMIXFinishOrders INNER JOIN dbo.tblOrders ON dbo.tblMIXFinishOrders.OrderNo = dbo.tblOrders.OrderNo " & _
                      "WHERE (dbo.tblMIXFinishOrders.Status = 'A') AND (dbo.tblMIXFinishOrders.OrderNo = '" & txtOrder.Text & "') " & _
                      "GROUP BY dbo.tblMIXFinishOrders.OrderNo, dbo.tblOrders.Subject, dbo.tblMIXFinishOrders.Reference, dbo.tblMIXFinishOrders.Side " & _
                      "ORDER BY dbo.tblMIXFinishOrders.OrderNo, dbo.tblMIXFinishOrders.Reference, dbo.tblMIXFinishOrders.Side"
        End If
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                dblDiffCts = Math.Round(rsComSql.Fields("ActCts").Value - rsComSql.Fields("Cts").Value, 3)
                flxDetails.Rows.Add(rsComSql.Fields("OrderNo").Value,
                                    rsComSql.Fields("Subject").Value,
                                    rsComSql.Fields("Reference").Value,
                                    rsComSql.Fields("Side").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    Format(rsComSql.Fields("Cts").Value, "#0.000"),
                                    Format(rsComSql.Fields("ActCts").Value, "#0.000"),
                                    IIf(dblDiffCts <> 0, Format(dblDiffCts, "#0.000"), ""))
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtPcs.Text = CalTotalPcs(flxDetails, 4)
        txtCts.Text = CalTotalCts(flxDetails, 6)
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        flxDetails.Rows.Clear()
        flxSelect.Rows.Clear()
        txtOrder.Text = ""
        txtRef.Text = ""
        txtSide.Text = ""
        txtFinPcs.Text = ""
        txtFinCts.Text = ""
        txtActFinCts.Text = ""
        txtDiffCts.Text = ""
        txtPcs.Text = CalTotalPcs(flxDetails, 4)
        txtCts.Text = CalTotalCts(flxDetails, 6)
    End Sub

    Private Sub txtOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrder.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        txtOrder.Text = flxDetails.Item(0, flxDetails.CurrentRow.Index).Value
        txtRef.Text = flxDetails.Item(2, flxDetails.CurrentRow.Index).Value
        txtSide.Text = flxDetails.Item(3, flxDetails.CurrentRow.Index).Value
        txtFinPcs.Text = flxDetails.Item(4, flxDetails.CurrentRow.Index).Value
        txtFinCts.Text = flxDetails.Item(6, flxDetails.CurrentRow.Index).Value
        txtActFinCts.Text = ""
        txtDiffCts.Text = ""

        LoadRefData()

        txtActFinCts.Focus()
    End Sub

    Private Sub LoadRefData()
        flxSelect.Rows.Clear()
        If txtOrder.Text <> "" And txtRef.Text <> "" And txtSide.Text <> "" Then
            rsComSql = New ADODB.Recordset
            mStrSQL = "SELECT OrderNo,Reference,Side,ParNo,Assortment,AssPrice,PacketNo,FinishedPcs," & _
                        "FinishedCts,PacketPcs,PacketCts,IssueCts,RateCode,Export,RecordNo,Subject,NLineNo,Type,GrPcs " & _
                      "FROM tblMixFinishOrders " & _
                      "WHERE Status = 'A' AND OrderNo = '" & txtOrder.Text & "' AND Reference = '" & Replace(txtRef.Text, "'", "''") & "' AND Side = '" & txtSide.Text & "' " & _
                      "ORDER BY FinishedCts DESC"
            rsComSql.Open(mStrSQL, AdoCN, 1, 1)
            While Not rsComSql.EOF
                flxSelect.Rows.Add(rsComSql.Fields("OrderNo").Value,
                                   rsComSql.Fields("Reference").Value,
                                   rsComSql.Fields("Side").Value,
                                   rsComSql.Fields("FinishedPcs").Value,
                                   Format(rsComSql.Fields("FinishedCts").Value, "#0.000"),
                                   rsComSql.Fields("RecordNo").Value)

                rsComSql.MoveNext()
            End While
            rsComSql = Nothing
        End If
    End Sub

    Private Sub txtActFinCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtActFinCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtActFinCts.Text)
        If Asc(e.KeyChar) = 13 Then
            If txtActFinCts.Text <> "" Then
                txtDiffCts.Text = Format(Math.Round(CDbl(txtActFinCts.Text) - CDbl(txtFinCts.Text), 3), "#0.000")
                EditFinishedCts()
            End If
        End If
    End Sub

    Private Sub EditFinishedCts()
        Dim intRow As Integer
        Dim intMaxRow As Integer
        Dim dblCurCts As Double

        If txtOrder.Text = "" Then Exit Sub
        If txtDiffCts.Text = "" Then Exit Sub
        If CDbl(txtDiffCts.Text) = 0 Then Exit Sub

        intMaxRow = 0
        dblCurCts = 0
        For intRow = 0 To flxSelect.Rows.Count - 1
            If dblCurCts < CDbl(flxSelect.Item(4, intRow).Value) Then
                dblCurCts = CDbl(flxSelect.Item(4, intRow).Value)
                intMaxRow = intRow
            End If
        Next

        flxSelect.Item(4, intMaxRow).Value = Format(CSng(flxSelect.Item(4, intMaxRow).Value) + CSng(txtDiffCts.Text), "#0.000")
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        For intRow = 0 To flxSelect.Rows.Count - 1
            AdoCN.Execute("UPDATE tblMixFinishOrders SET FinishedCts = " & CDbl(flxSelect.Item(4, intRow).Value) & " WHERE RecordNo = " & CDbl(flxSelect.Item(5, intRow).Value) & "")
        Next

        flxDetails.Rows.Clear()
        flxSelect.Rows.Clear()
        Load_PolishedReturns()

        txtFinPcs.Text = ""
        txtFinCts.Text = ""
        txtActFinCts.Text = ""
        txtDiffCts.Text = ""

    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(intCalRow, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(intCalRow, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        Load_PolishedReturns()
    End Sub

    Private Sub frm_MixFinishOrdersEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub
End Class