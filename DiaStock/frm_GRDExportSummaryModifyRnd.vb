
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDExportSummaryModifyRnd

    Private Sub frm_GRDExportSummaryModify_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        If optEdit.Checked = True Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_PackingListCOLM WHERE LotNo = '" & txtLotNo.Text & "' ORDER BY Assortment", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    flxDetails.Rows.Add(rsComSql.Fields("Assortment").Value,
                                        rsComSql.Fields("LotNo").Value,
                                        rsComSql.Fields("Pcs").Value,
                                        Format(rsComSql.Fields("Cts").Value, "#0.000"),
                                        Format(rsComSql.Fields("Price").Value, "#0.00"),
                                        Format(rsComSql.Fields("Cts").Value * rsComSql.Fields("Price").Value, "#0.00"),
                                        rsComSql.Fields("OrderNo").Value)

                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing

        ElseIf optNew.Checked = True Then
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.tblGrading_PackingListRnd.Department, dbo.tblGrading_PackingListRnd.Assortment, SUM(dbo.tblGrading_PackingListRnd.ActPcs) AS Pcs, " & _
                                "ROUND(SUM(dbo.tblGrading_PackingListRnd.ActCts), 3) AS Cts, dbo.tblGrading_PackingListRnd.Price, dbo.tblGrading_PackingListRnd.OrderNo, dbo.VW_RndRealParcelLot.LotNo " & _
                            "FROM dbo.tblGrading_PackingListRnd INNER JOIN dbo.VW_RndRealParcelLot ON dbo.tblGrading_PackingListRnd.Department = dbo.VW_RndRealParcelLot.Depart AND " & _
                                "dbo.tblGrading_PackingListRnd.ParNo = dbo.VW_RndRealParcelLot.ParcelNo " & _
                            "GROUP BY dbo.tblGrading_PackingListRnd.Department, dbo.tblGrading_PackingListRnd.Assortment, dbo.tblGrading_PackingListRnd.Price," & _
                                "dbo.tblGrading_PackingListRnd.OrderNo, dbo.VW_RndRealParcelLot.LotNo " & _
                            "HAVING (dbo.tblGrading_PackingListRnd.Department = 'Rounds') AND (dbo.tblGrading_PackingListRnd.OrderNo = '') AND (dbo.VW_RndRealParcelLot.LotNo = '" & txtLotNo.Text & "') " & _
                            "ORDER BY dbo.tblGrading_PackingListRnd.Assortment", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                rsComSql_1.MoveFirst()
                While Not rsComSql_1.EOF
                    flxDetails.Rows.Add(rsComSql_1.Fields("Assortment").Value,
                                        rsComSql_1.Fields("LotNo").Value,
                                        rsComSql_1.Fields("Pcs").Value,
                                        Format(rsComSql_1.Fields("Cts").Value, "#0.000"),
                                        Format(rsComSql_1.Fields("Price").Value, "#0.00"),
                                        Format(rsComSql_1.Fields("Cts").Value * rsComSql_1.Fields("Price").Value, "#0.00"),
                                        rsComSql_1.Fields("OrderNo").Value)

                    rsComSql_1.MoveNext()
                End While
            End If
            rsComSql_1 = Nothing

            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.tblGrading_PackingListRnd.Department, dbo.tblGrading_PackingListRnd.Assortment, dbo.tblGrading_PackingListRnd.ActPcs AS Pcs, " & _
                                "dbo.tblGrading_PackingListRnd.ActCts AS Cts, dbo.tblGrading_PackingListRnd.Price, dbo.tblGrading_PackingListRnd.OrderNo, dbo.VW_RndRealParcelLot.LotNo " & _
                            "FROM dbo.tblGrading_PackingListRnd INNER JOIN dbo.VW_RndRealParcelLot ON dbo.tblGrading_PackingListRnd.Department = dbo.VW_RndRealParcelLot.Depart AND " & _
                                "dbo.tblGrading_PackingListRnd.ParNo = dbo.VW_RndRealParcelLot.ParcelNo " & _
                            "WHERE (dbo.tblGrading_PackingListRnd.Department = 'Rounds') AND (dbo.tblGrading_PackingListRnd.OrderNo <> '') AND (dbo.VW_RndRealParcelLot.LotNo = '" & txtLotNo.Text & "') " & _
                            "ORDER BY dbo.tblGrading_PackingListRnd.Assortment", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                rsComSql_1.MoveFirst()
                While Not rsComSql_1.EOF
                    flxDetails.Rows.Add(rsComSql_1.Fields("Assortment").Value,
                                        rsComSql_1.Fields("LotNo").Value,
                                        rsComSql_1.Fields("Pcs").Value,
                                        Format(rsComSql_1.Fields("Cts").Value, "#0.000"),
                                        Format(rsComSql_1.Fields("Price").Value, "#0.00"),
                                        Format(rsComSql_1.Fields("Cts").Value * rsComSql_1.Fields("Price").Value, "#0.00"),
                                        rsComSql_1.Fields("OrderNo").Value)

                    rsComSql_1.MoveNext()
                End While
            End If
            rsComSql_1 = Nothing
        End If

        txtPcs.Text = CalTotalPcs(flxDetails)
        txtCts.Text = Format(CalTotalCts(flxDetails), "#0.000")

        txtTotPcs.Text = txtPcs.Text
        txtTotCts.Text = txtCts.Text
    End Sub

    Private Sub Load_Assortments()
        Me.Cursor = Cursors.WaitCursor
        cmbAssort.Items.Clear()
        rsComSql_4 = New ADODB.Recordset
        rsComSql_4.Open("SELECT * FROM tblGrading_RndSizeList WHERE (RIGHT(AssortNo, 2) <> '_M') AND (RIGHT(AssortNo, 2) <> '_C') ORDER BY AssortNo", AdoCN, 1, 1)
        If rsComSql_4.RecordCount Then
            rsComSql_4.MoveFirst()
            While Not rsComSql_4.EOF
                cmbAssort.Items.Add(rsComSql_4.Fields("AssortNo").Value)
                rsComSql_4.MoveNext()
            End While
        End If
        rsComSql_4 = Nothing
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        Load_Assortments()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub txtLotNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLotNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            Load_PackingList()
        End If
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

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim intRow As Integer
        Dim dblPrice As Double

        If cmbAssort.Text = "" Then MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtNewPcs.Text = "" Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtNewCts.Text = "" Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtNewPcs.Text) <= 0 Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtNewCts.Text) <= 0 Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If CDbl(txtTotPcs.Text) + CDbl(txtNewPcs.Text) > CDbl(txtPcs.Text) Then MsgBox("Invalid Total Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If Math.Round(CDbl(txtTotCts.Text) + CDbl(txtNewCts.Text), 3) > CDbl(txtCts.Text) Then MsgBox("Invalid Total Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        For intRow = 0 To flxDetails.Rows.Count - 1
            If cmbAssort.Text = flxDetails.Item(0, intRow).Value Then
                MsgBox("Assortment already entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        dblPrice = 0
        rsComSql_4 = New ADODB.Recordset
        rsComSql_4.Open("SELECT PRICE FROM tblGrading_RndSizeList WHERE AssortNo = '" & cmbAssort.Text & "'", AdoCN, 1, 1)
        If rsComSql_4.RecordCount Then
            dblPrice = rsComSql_4.Fields("Price").Value
        End If
        rsComSql_4 = Nothing

        flxDetails.Rows.Add(UCase(cmbAssort.Text),
                            txtLotNo.Text,
                            txtNewPcs.Text,
                            txtNewCts.Text,
                            Format(dblPrice, "#0.00"),
                            Format(dblPrice * CDbl(txtNewCts.Text), "#0.00"),
                            "")

        txtTotPcs.Text = CalTotalPcs(flxDetails)
        txtTotCts.Text = CalTotalCts(flxDetails)

        cmbAssort.Text = ""
        txtNewPcs.Text = ""
        txtNewCts.Text = ""

        cmbAssort.Focus()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
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

    Private Sub UpdateListPrice()
        Dim intRow As Integer

        For intRow = 0 To flxDetails.Rows.Count - 1
            rsComSql_4 = New ADODB.Recordset
            rsComSql_4.Open("SELECT * FROM tblGrading_RndSizeList WHERE AssortNo = '" & flxDetails.Item(0, intRow).Value & "'", AdoCN, 1, 1)
            If rsComSql_4.RecordCount Then
                flxDetails.Item(4, intRow).Value = Format(rsComSql_4.Fields("Price").Value, "#0.00")
                flxDetails.Item(5, intRow).Value = Format(Math.Round(rsComSql_4.Fields("Price").Value * CDbl(flxDetails.Item(3, intRow).Value), 2), "#0.00")
            End If
            rsComSql_4 = Nothing
        Next

    End Sub

    Private Sub cmdProcess_Click(sender As Object, e As EventArgs) Handles cmdProcess.Click
        PBResponse = MsgBox("Are you sure to Update the List Price?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            UpdateListPrice()
        End If
    End Sub

    Private Sub cmbAssort_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbAssort.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbAssort.Text = UCase(cmbAssort.Text)
            txtNewPcs.Focus()
        End If
    End Sub

    Private Sub txtNewPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNewPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtNewCts.Focus()
        End If
    End Sub

    Private Sub txtNewCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNewCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtNewCts.Text)
        If Asc(e.KeyChar) = 13 Then
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub flxDetails_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellEndEdit
        txtTotPcs.Text = CalTotalPcs(flxDetails)
        txtTotCts.Text = CalTotalCts(flxDetails)
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        txtTotPcs.Text = CalTotalPcs(flxDetails)
        txtTotCts.Text = CalTotalCts(flxDetails)

        If txtLotNo.Text = "" Then MsgBox("Invalid Lot No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If flxDetails.Rows.Count < 1 Then MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtTotPcs.Text) <> CDbl(txtPcs.Text) Then MsgBox("Pcs not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtTotCts.Text) <> CDbl(txtCts.Text) Then MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtPack.Text = "" Then MsgBox("Invalid Packing List No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtType.Text = "" Then MsgBox("Invalid Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtCategory.Text = "" Then MsgBox("Invalid Category", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        For intRow = 0 To flxDetails.Rows.Count - 1
            If CInt(flxDetails.Item(2, intRow).Value) > 0 And CDbl(flxDetails.Item(3, intRow).Value) <= 0 Then
                MsgBox("Invalid Cts - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If CInt(flxDetails.Item(2, intRow).Value) <= 0 And CDbl(flxDetails.Item(3, intRow).Value) > 0 Then
                MsgBox("Invalid Pcs - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblGrading_PackingListCOLM WHERE LotNo = '" & txtLotNo.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            PBResponse = MsgBox("Are you sure to Update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                AdoCN.Execute("DELETE FROM tblGrading_PackingListCOLM WHERE LotNo = '" & txtLotNo.Text & "'")
                For intRow = 0 To flxDetails.Rows.Count - 1
                    If CInt(flxDetails.Item(2, intRow).Value) > 0 And CDbl(flxDetails.Item(3, intRow).Value) > 0 Then
                        AdoCN.Execute("INSERT INTO tblGrading_PackingListCOLM(Department,LotNo,Assortment,Pcs,Cts,Price,PackNo,Type,Price2) " & _
                                      "VALUES('Rounds','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(0, intRow).Value & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & _
                                            "" & CDbl(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & "," & CDbl(txtPack.Text) & ",'" & txtCategory.Text & "','" & flxDetails.Item(6, intRow).Value & "'," & CDbl(flxDetails.Item(4, intRow).Value) & ")")

                    End If
                Next

                MsgBox("Updated Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            PBResponse = MsgBox("Are you sure to Save?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                For intRow = 0 To flxDetails.Rows.Count - 1
                    If CInt(flxDetails.Item(2, intRow).Value) > 0 And CDbl(flxDetails.Item(3, intRow).Value) > 0 Then
                        AdoCN.Execute("INSERT INTO tblGrading_PackingListCOLM(Department,LotNo,Assortment,Pcs,Cts,Price,PackNo,Type,Price2) " & _
                                      "VALUES('Rounds','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(0, intRow).Value & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & _
                                            "" & CDbl(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & "," & CDbl(txtPack.Text) & ",'" & txtCategory.Text & "','" & flxDetails.Item(6, intRow).Value & "'," & CDbl(flxDetails.Item(4, intRow).Value) & ")")

                    End If
                Next

                MsgBox("Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        End If
        rsComSql = Nothing

        flxDetails.Rows.Clear()
        txtLotNo.Text = ""
        txtPcs.Text = "0"
        txtCts.Text = "0"
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        txtLotNo.Focus()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        'Save()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub optNew_CheckedChanged(sender As Object, e As EventArgs) Handles optNew.CheckedChanged
        flxDetails.Rows.Clear()
    End Sub

    Private Sub optEdit_CheckedChanged(sender As Object, e As EventArgs) Handles optEdit.CheckedChanged
        flxDetails.Rows.Clear()
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
End Class