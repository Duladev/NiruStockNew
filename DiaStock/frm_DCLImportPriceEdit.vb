
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLImportPriceEdit

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ClearFields()
        txtLotNo.Text = ""
        txtInvPrice.Text = ""
        flxDetails.Rows.Clear()
        txtParNo.Text = ""
        txtParNo2.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtNextLot.Text = ""
    End Sub

    Private Sub txtInvPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtInvPrice.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtInvPrice.Text)
    End Sub

    Private Sub txtLotNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLotNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 And Len(txtLotNo.Text) > 0 Then
            flxDetails.Rows.Clear()
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblImport WHERE LotNo = '" & CDbl(txtLotNo.Text) & "' ORDER BY SupParcelNo", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    flxDetails.Rows.Add(rsComSql.Fields("SupParcelNo").Value,
                                        rsComSql.Fields("INVPcs").Value,
                                        rsComSql.Fields("INVCts").Value,
                                        rsComSql.Fields("ImpPrice").Value,
                                        rsComSql.Fields("LotNo").Value)

                    rsComSql.MoveNext()
                End While
                txtInvPrice.Focus()
            Else
                MsgBox("Invalid Lot No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
                txtLotNo.Focus()
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub Save()
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If txtLotNo.Text <> "" And txtInvPrice.Text <> "" Then
                If CDbl(txtInvPrice.Text) <= 0 Then
                    MsgBox("Invalid Price", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                AdoCN.Execute("UPDATE tblImport SET ImpPrice = " & CDbl(txtInvPrice.Text) & " " & _
                              "WHERE LotNo = '" & CDbl(txtLotNo.Text) & "'")

                MsgBox("Invoice Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
            End If
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub frm_DCLImportPriceEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 And Len(txtParNo.Text) > 0 Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblImport WHERE SupParcelNo = '" & txtParNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                txtParNo.Text = rsComSql.Fields("SupParcelNo").Value
                txtPcs.Text = "0"
                txtCts.Text = "0"

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT SUM(Pcs) AS Pcs, SUM(Cts) AS Cts FROM tblPOLStockIn WHERE (SupParNo = '" & txtParNo.Text & "')", AdoCN, 1, 1)
                If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                    txtPcs.Text = rsComSql_1.Fields("Pcs").Value
                    txtCts.Text = Math.Round(rsComSql_1.Fields("Cts").Value, 3)
                End If
                rsComSql_1 = Nothing
            Else
                MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
                txtParNo.Focus()
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Delete()
    End Sub

    Private Sub Delete()
        PBResponse = MsgBox("Are you sure to Delete?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If txtParNo.Text <> "" Then
                AdoCN.Execute("DELETE FROM tblDep_Trf WHERE SupParcelNo = '" & txtParNo.Text & "' AND Department = 'PolishBox'")
                AdoCN.Execute("DELETE FROM tblPOLStockIn WHERE SupParNo = '" & txtParNo.Text & "'")
                AdoCN.Execute("DELETE FROM tblPOLStockInOrigin WHERE SupParNo = '" & txtParNo.Text & "'")

                MsgBox("Parcel Deleted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
            End If
        End If
    End Sub

    Private Sub cmdNext_Click(sender As Object, e As EventArgs) Handles cmdNext.Click
        txtNextLot.Text = ""
        rsComSql_1 = New ADODB.Recordset
        If strDBName = "DiaStock" Then
            rsComSql_1.Open("SELECT MAX(LotNo) AS MaxLotNo FROM tblImport WHERE (LotNo >= 58070251) AND (LotNo <= 58270251)", AdoCN, 1, 1)
        Else
            rsComSql_1.Open("SELECT MAX(LotNo) AS MaxLotNo FROM tblImport WHERE (LotNo >= 56545248) AND (LotNo <= 56620248)", AdoCN, 1, 1)
        End If
        If Not IsDBNull(rsComSql_1.Fields("MaxLotNo").Value) Then
            txtNextLot.Text = rsComSql_1.Fields("MaxLotNo").Value + 1
        End If
        rsComSql_1 = Nothing
    End Sub

    Private Sub cmdDelete2_Click(sender As Object, e As EventArgs) Handles cmdDelete2.Click
        DeleteImport()
    End Sub

    Private Sub DeleteImport()
        PBResponse = MsgBox("Are you sure to Delete?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If txtParNo2.Text <> "" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SupParcelNo FROM tblDep_Trf WHERE SupParcelNo = '" & txtParNo2.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    AdoCN.Execute("DELETE FROM tblImport WHERE SupParcelNo = '" & txtParNo2.Text & "'")

                    MsgBox("Import Deleted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Else
                    MsgBox("Department Transfer already done", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                End If
                rsComSql = Nothing

                ClearFields()
            End If
        End If
    End Sub
End Class