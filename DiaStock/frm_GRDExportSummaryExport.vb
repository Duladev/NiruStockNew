
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDExportSummaryExport

    Private Sub frm_GRDExportSummaryExport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        txtPcs.Text = "0"
        txtCts.Text = "0"
        txtLotNo.Focus()
    End Sub

    Private Sub Load_PackingList()

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        If optEdit.Checked = True Then
            If txtPack.Text <> "" Then
                rsComSql.Open("SELECT * FROM tblGrading_PackingListM WHERE LotNo = '" & txtLotNo.Text & "' AND PackNo = " & CDbl(txtPack.Text) & " ORDER BY Assortment", AdoCN, 1, 1)
            Else
                MsgBox("Invalid Pack No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Else
            'rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblGrading_PackingListExport.LotNo, dbo.tblGrading_PackingListExport.Assortment, " & _
            '                    "dbo.tblGrading_PackingListExport.Pcs - ISNULL(dbo.VW_GradingPackingListM.Pcs, 0) AS Pcs, " & _
            '                    "dbo.tblGrading_PackingListExport.Cts - ISNULL(dbo.VW_GradingPackingListM.Cts, 0) AS Cts, dbo.tblGrading_PackingListExport.Price, " & _
            '                    "dbo.tblGrading_PackingListExport.Stone, dbo.tblGrading_PackingListExport.SizeRange " & _
            '             "FROM dbo.tblGrading_PackingListExport LEFT OUTER JOIN dbo.VW_GradingPackingListM ON dbo.tblGrading_PackingListExport.Stone = dbo.VW_GradingPackingListM.Stone AND " & _
            '                    "dbo.tblGrading_PackingListExport.Assortment = dbo.VW_GradingPackingListM.Assortment AND " & _
            '                    "dbo.tblGrading_PackingListExport.LotNo = dbo.VW_GradingPackingListM.LotNo AND dbo.tblGrading_PackingListExport.SizeRange = dbo.VW_GradingPackingListM.SizeRange " & _
            '             "WHERE (dbo.tblGrading_PackingListExport.LotNo = '" & txtLotNo.Text & "') AND (dbo.tblGrading_PackingListExport.Pcs - ISNULL(dbo.VW_GradingPackingListM.Pcs, 0) > 0) " & _
            '             "ORDER BY dbo.tblGrading_PackingListExport.Assortment, dbo.tblGrading_PackingListExport.SizeRange", AdoCN, 1, 1)

            rsComSql.Open("SELECT TOP (100) PERCENT dbo.VW_GradingPackingListExport2.LotNo, dbo.VW_GradingPackingListExport2.Assortment, " & _
                                "dbo.VW_GradingPackingListExport2.Pcs - ISNULL(dbo.VW_GradingPackingListM.Pcs, 0) AS Pcs, " & _
                                "dbo.VW_GradingPackingListExport2.Cts - ISNULL(dbo.VW_GradingPackingListM.Cts, 0) AS Cts, dbo.VW_GradingPackingListExport2.Price, " & _
                                "dbo.VW_GradingPackingListExport2.Stone, dbo.VW_GradingPackingListExport2.SizeRange " & _
                         "FROM dbo.VW_GradingPackingListExport2 LEFT OUTER JOIN dbo.VW_GradingPackingListM ON dbo.VW_GradingPackingListExport2.Stone = dbo.VW_GradingPackingListM.Stone AND " & _
                                "dbo.VW_GradingPackingListExport2.Assortment = dbo.VW_GradingPackingListM.Assortment AND " & _
                                "dbo.VW_GradingPackingListExport2.LotNo = dbo.VW_GradingPackingListM.LotNo AND dbo.VW_GradingPackingListExport2.SizeRange = dbo.VW_GradingPackingListM.SizeRange " & _
                         "WHERE (dbo.VW_GradingPackingListExport2.LotNo = '" & txtLotNo.Text & "') AND (dbo.VW_GradingPackingListExport2.Pcs - ISNULL(dbo.VW_GradingPackingListM.Pcs, 0) > 0) " & _
                         "ORDER BY dbo.VW_GradingPackingListExport2.Assortment, dbo.VW_GradingPackingListExport2.SizeRange", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            If optEdit.Checked = True Then
                txtPack.Text = rsComSql.Fields("PackNo").Value
                txtType.Text = rsComSql.Fields("PackType").Value
                txtCategory.Text = rsComSql.Fields("Category").Value
            End If
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("Assortment").Value,
                                    rsComSql.Fields("LotNo").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    Format(rsComSql.Fields("Cts").Value, "#0.000"),
                                    Format(rsComSql.Fields("Price").Value, "#0.00"),
                                    Format(rsComSql.Fields("Cts").Value * rsComSql.Fields("Price").Value, "#0.00"),
                                    True,
                                    rsComSql.Fields("Stone").Value,
                                    rsComSql.Fields("SizeRange").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtPcs.Text = CalTotalPcs(flxDetails)
        txtCts.Text = Format(CalTotalCts(flxDetails), "#0.000")

        txtTotPcs.Text = txtPcs.Text
        txtTotCts.Text = txtCts.Text
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

    Private Sub ClearFields()
        flxDetails.Rows.Clear()
        txtLotNo.Text = ""
        txtPcs.Text = "0"
        txtCts.Text = "0"
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        txtPack.Text = ""
        txtType.Text = ""
        txtCategory.Text = ""
        txtLotNo.Focus()
    End Sub

    Private Sub txtLotNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLotNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            Load_PackingList()
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub txtPack_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPack.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_Pack WHERE PackingListNo = '" & CDbl(txtPack.Text) & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                txtType.Text = rsComSql.Fields("PackType").Value
                txtCategory.Text = rsComSql.Fields("Category").Value
            Else
                txtType.Text = ""
                txtCategory.Text = ""
                txtPack.Text = ""
                txtPack.Focus()
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        txtTotPcs.Text = CalTotalPcs(flxDetails)
        txtTotCts.Text = CalTotalCts(flxDetails)

        If txtLotNo.Text = "" Then MsgBox("Invalid Lot No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If flxDetails.Rows.Count < 1 Then MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtTotPcs.Text) <= 0 Then MsgBox("Pcs not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtTotPcs.Text) > CDbl(txtPcs.Text) Then MsgBox("Pcs not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtTotCts.Text) <= 0 Then MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtTotCts.Text) > CDbl(txtCts.Text) Then MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtPack.Text = "" Then MsgBox("Invalid Packing List No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtType.Text = "" Then MsgBox("Invalid Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtCategory.Text = "" Then MsgBox("Invalid Category", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(6, intRow).Value = True Or flxDetails.Item(6, intRow).Value = 1 Then
                If CDbl(flxDetails.Item(2, intRow).Value) <= 0 Then
                    MsgBox("Invalid Pcs - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If CDbl(flxDetails.Item(3, intRow).Value) <= 0 Then
                    MsgBox("Invalid Cts - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
        Next

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblGrading_PackingListM WHERE LotNo = '" & txtLotNo.Text & "' AND PackNo = " & CDbl(txtPack.Text) & "", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            PBResponse = MsgBox("Are you sure to Update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                AdoCN.Execute("DELETE FROM tblGrading_PackingListM WHERE LotNo = '" & txtLotNo.Text & "' AND PackNo = " & CDbl(txtPack.Text) & "")
                For intRow = 0 To flxDetails.Rows.Count - 1
                    If flxDetails.Item(6, intRow).Value = True Or flxDetails.Item(6, intRow).Value = 1 Then
                        AdoCN.Execute("INSERT INTO tblGrading_PackingListM(Assortment, LotNo, Pcs, Cts, Price, PackNo, PackType, Category, Stone, SIzeRange) " & _
                                      "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & CInt(flxDetails.Item(2, intRow).Value) & "," & _
                                            "" & CDbl(flxDetails.Item(3, intRow).Value) & "," & CInt(flxDetails.Item(4, intRow).Value) & "," & CDbl(txtPack.Text) & ",'" & txtType.Text & "'," & _
                                            "'" & txtCategory.Text & "'," & CInt(flxDetails.Item(7, intRow).Value) & ",'" & flxDetails.Item(8, intRow).Value & "')")
                    End If
                Next

                MsgBox("Updated Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            PBResponse = MsgBox("Are you sure to Save?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                For intRow = 0 To flxDetails.Rows.Count - 1
                    If flxDetails.Item(6, intRow).Value = True Or flxDetails.Item(6, intRow).Value = 1 Then
                        AdoCN.Execute("INSERT INTO tblGrading_PackingListM(Assortment, LotNo, Pcs, Cts, Price, PackNo, PackType, Category, Stone, SizeRange) " & _
                                      "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & CInt(flxDetails.Item(2, intRow).Value) & "," & _
                                            "" & CDbl(flxDetails.Item(3, intRow).Value) & "," & CInt(flxDetails.Item(4, intRow).Value) & "," & CDbl(txtPack.Text) & ",'" & txtType.Text & "'," & _
                                            "'" & txtCategory.Text & "'," & CInt(flxDetails.Item(7, intRow).Value) & ",'" & flxDetails.Item(8, intRow).Value & "')")
                    End If
                Next
                MsgBox("Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        End If
        rsComSql = Nothing

        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub optNew_CheckedChanged(sender As Object, e As EventArgs) Handles optNew.CheckedChanged
        flxDetails.Rows.Clear()
    End Sub

    Private Sub optEdit_CheckedChanged(sender As Object, e As EventArgs) Handles optEdit.CheckedChanged
        flxDetails.Rows.Clear()
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        If txtPack.Text = "" Then MsgBox("Invalid Packing List No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtLotNo.Text = "" Then MsgBox("Invalid Lot No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        PBResponse = MsgBox("Are you sure to Delete?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblCosting WHERE PackingListNo = " & CDbl(txtPack.Text) & " AND Status = 'E'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                AdoCN.Execute("DELETE FROM tblGrading_PackingListM WHERE PackNo = " & CDbl(txtPack.Text) & " AND LotNo = '" & CDbl(txtLotNo.Text) & "'")

                MsgBox("Packing Details Deleted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Else
                MsgBox("Costing and Invoicing done. Can't delete", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            ClearFields()
        End If
    End Sub

    Private Sub flxDetails_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellEndEdit
        txtTotPcs.Text = CalTotalPcs(flxDetails)
        txtTotCts.Text = CalTotalCts(flxDetails)
    End Sub

    Private Sub cmdProcess_Click(sender As Object, e As EventArgs) Handles cmdProcess.Click
        PBResponse = MsgBox("Are you sure to Update the List Price?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            UpdateListPrice()
        End If
    End Sub

    Private Sub UpdateListPrice()
        Dim intRow As Integer

        For intRow = 0 To flxDetails.Rows.Count - 1
            rsComSql_4 = New ADODB.Recordset
            rsComSql_4.Open("SELECT * FROM tblGrading_SizingList WHERE NAME = '" & flxDetails.Item(0, intRow).Value & "'", AdoCN, 1, 1)
            If rsComSql_4.RecordCount Then
                flxDetails.Item(4, intRow).Value = Format(rsComSql_4.Fields("Price").Value, "#0.00")
                flxDetails.Item(5, intRow).Value = Format(Math.Round(rsComSql_4.Fields("Price").Value * CDbl(flxDetails.Item(3, intRow).Value), 2), "#0.00")
            End If
            rsComSql_4 = Nothing
        Next

    End Sub

    Private Sub txtLotNo_TextChanged(sender As Object, e As EventArgs) Handles txtLotNo.TextChanged

    End Sub
End Class