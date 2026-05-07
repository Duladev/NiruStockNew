
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLCostingEditMixNew

    Private Sub Load_CostingData()
        If txtPack.Text = "" Then MsgBox("Invalid Packing List No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        'If cmbLineNo.Text = "" Then MsgBox("Invalid Line No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        rsComSql = New ADODB.Recordset
        If cmbLineNo.Text <> "" Then
            rsComSql.Open("SELECT TOP (100) PERCENT NLineNo, Commande, Subject, SUM(ExportPcs) AS ExportPcs, Reference1, NOrderNo, OrderItem, ClientID " & _
                          "FROM dbo.tblCosting " & _
                          "WHERE (Department = N'Mix') AND (PackingListNo = '" & txtPack.Text & "') AND (NLineNo = '" & cmbLineNo.Text & "') " & _
                          "GROUP BY Subject, Commande, Reference1, NOrderNo, OrderItem, ClientID, NLineNo " & _
                          "ORDER BY Commande, Subject", AdoCN, 1, 1)
        Else
            rsComSql.Open("SELECT TOP (100) PERCENT NLineNo, Commande, Subject, SUM(ExportPcs) AS ExportPcs, Reference1, NOrderNo, OrderItem, ClientID " & _
                          "FROM dbo.tblCosting " & _
                          "WHERE (Department = N'Mix') AND (PackingListNo = '" & txtPack.Text & "') " & _
                          "GROUP BY Subject, Commande, Reference1, NOrderNo, OrderItem, ClientID, NLineNo " & _
                          "ORDER BY Commande, Subject", AdoCN, 1, 1)
        End If
        
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("Reference1").Value, rsComSql.Fields("ClientID").Value,
                                    rsComSql.Fields("Commande").Value, rsComSql.Fields("NOrderNo").Value, rsComSql.Fields("OrderItem").Value,
                                    rsComSql.Fields("Subject").Value, rsComSql.Fields("NLineNo").Value,
                                    rsComSql.Fields("ExportPcs").Value, False)
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
        cmbLineNo.Items.Clear()
        cmbLineNo.Text = ""
        txtPcs.Text = ""
        txtLineNo.Text = ""
        txtPack.Text = ""
        txtSubject.Text = ""
        txtCommande.Text = ""
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        If txtPack.Text = "" Then MsgBox("Invalid Packing List No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtLineNo.Text = "" Then MsgBox("Invalid New Line No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        'If txtSubject.Text = "" Then MsgBox("Invalid Subject", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtCommande.Text = "" Then MsgBox("Invalid Commande", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If cmbLineNo.Text = txtLineNo.Text Then MsgBox("Same Line No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT dbo.tblOrders.Niruref, dbo.tblOrdersDtls.NLineNo " & _
                          "FROM dbo.tblOrders INNER JOIN dbo.tblOrdersDtls ON dbo.tblOrders.OrderNo = dbo.tblOrdersDtls.OrderNo " & _
                          "WHERE (dbo.tblOrdersDtls.NLineNo = '" & txtLineNo.Text & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid New Line No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(8, intRow).Value = True Then
                    AdoCN.Execute("UPDATE tblCosting SET NLineNo = '" & txtLineNo.Text & "', Commande = '" & txtCommande.Text & "' " & _
                                  "WHERE PackingListNo = '" & CDbl(txtPack.Text) & "' AND NLineNo = '" & flxDetails.Item(6, intRow).Value & "'")

                    AdoCN.Execute("UPDATE tblMixExportOrders SET NLineNo = '" & txtLineNo.Text & "' " & _
                                  "WHERE PackingListNo = '" & CDbl(txtPack.Text) & "' AND NLineNo = '" & flxDetails.Item(6, intRow).Value & "'")
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
            If flxSample.Rows(intRow).Cells(8).EditedFormattedValue = True Or flxSample.Rows(intRow).Cells(8).EditedFormattedValue = 1 Then
                CalTotalPcs = CalTotalPcs + Val(flxSample.Item(7, intRow).Value)
            End If
        Next
    End Function

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
                flxDetails.Item(8, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(8, intRow).Value = False
            Next
        End If
        txtPcs.Text = CalTotalPcs(flxDetails)
    End Sub

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 8 Then
            txtPcs.Text = CalTotalPcs(flxDetails)
        End If
    End Sub

    Private Sub frm_DCLCostingEditMixNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub txtPack_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPack.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtPack.Text <> "" Then
                Load_PackDetails()
            End If
        End If
    End Sub

    Private Sub Load_PackDetails()
        cmbLineNo.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT NLineNo " & _
                      "FROM dbo.tblCosting " & _
                      "WHERE (Department = N'Mix') AND (PackingListNo = '" & txtPack.Text & "') " & _
                      "GROUP BY NLineNo " & _
                      "ORDER BY NLineNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbLineNo.Items.Add(rsComSql.Fields("NLineNo").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub txtLineNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLineNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT dbo.tblOrders.Niruref, dbo.tblOrdersDtls.NLineNo, dbo.tblOrders.Subject, dbo.tblOrders.COMMANDE " & _
                          "FROM dbo.tblOrders INNER JOIN dbo.tblOrdersDtls ON dbo.tblOrders.OrderNo = dbo.tblOrdersDtls.OrderNo " & _
                          "WHERE (dbo.tblOrdersDtls.NLineNo = '" & txtLineNo.Text & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                txtSubject.Text = rsComSql.Fields("Subject").Value
                txtCommande.Text = rsComSql.Fields("COMMANDE").Value
            Else
                MsgBox("Invalid Line No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing
        End If
    End Sub
End Class