
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_POLModifyAssort

    Private Sub frm_POLModifyAssort_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        cmbCompCode.Items.Add("DCL")
        cmbCompCode.Items.Add("NLE")
    End Sub

    Private Sub ClearFields()
        txtAssortment.Text = ""
        txtOAssort.Text = ""
        cmbAssort.Text = ""
        cmbAssort.Items.Clear()
        txtBalPcs.Text = "0"
        txtBalCts.Text = "0"
        txtPrice.Text = "0"
        txtPcs.Text = ""
        txtCts.Text = ""
        cmbCompCode.Text = ""
    End Sub

    Private Sub txtAssortment_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAssortment.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If cmbCompCode.Text = "" Then
                MsgBox("Invalid Company", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            txtAssortment.Text = UCase(txtAssortment.Text)
            txtBalPcs.Text = "0"
            txtBalCts.Text = "0"
            txtPrice.Text = "0"
            cmbAssort.Items.Clear()
            txtOAssort.Text = ""

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM VW_POLStockBal2New WHERE Assortment2 = '" & txtAssortment.Text & "' AND CompCode = '" & cmbCompCode.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                    txtOAssort.Text = rsComSql.Fields("Assortment").Value

                    txtBalPcs.Text = rsComSql.Fields("Pcs").Value
                    txtBalCts.Text = Math.Round(rsComSql.Fields("Cts").Value, 3)

                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & txtOAssort.Text & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        If cmbCompCode.Text = "DCL" Then
                            txtPrice.Text = rsComSql_1.Fields("AvgCost").Value
                        Else
                            txtPrice.Text = rsComSql_1.Fields("AvgCost2").Value
                        End If
                    End If
                    rsComSql_1 = Nothing

                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblPOLAssortMatch WHERE OrigAssort = '" & txtOAssort.Text & "' ORDER BY NewAssort", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        rsComSql_1.MoveFirst()
                        While Not rsComSql_1.EOF
                            cmbAssort.Items.Add(rsComSql_1.Fields("NewAssort").Value)
                            rsComSql_1.MoveNext()
                        End While
                    End If
                    rsComSql_1 = Nothing

                    txtPcs.Focus()
                End If
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub txtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtCts.Focus()
        End If
    End Sub

    Private Sub txtCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtCts.Text)
        If Asc(e.KeyChar) = 13 Then
            cmdSave.Focus()
        End If
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub Save()
        If cmbCompCode.Text = "" Then
            MsgBox("Invalid Company", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtAssortment.Text = "" Then MsgBox("Invalid New Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbAssort.Text = "" Then MsgBox("Invalid Old Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtPcs.Text = "" Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtCts.Text = "" Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If CInt(txtPcs.Text) <= 0 Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtCts.Text) <= 0 Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If CInt(txtPcs.Text) > CInt(txtBalPcs.Text) Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtCts.Text) > CDbl(txtBalCts.Text) Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblPOLAssortMatch WHERE OrigAssort = '" & txtOAssort.Text & "' AND NewAssort = '" & cmbAssort.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        AdoCN.Execute("INSERT INTO tblPOLModify2(NewAssort, OldAssort, Pcs, Cts, CompCode) " & _
                      "VALUES('" & UCase(cmbAssort.Text) & "','" & UCase(txtAssortment.Text) & "'," & _
                        "" & CInt(txtPcs.Text) & "," & CDbl(txtCts.Text) & ",'" & cmbCompCode.Text & "')")

        AdoCN.Execute("INSERT INTO tblPOLStockIn(SupParNo,Assortment,Assortment2,Pcs,Cts,Price,CompCode) " & _
                        "VALUES('9','" & txtOAssort.Text & "','" & UCase(cmbAssort.Text) & "'," & _
                            "" & CInt(txtPcs.Text) & "," & CDbl(txtCts.Text) & "," & CDbl(txtPrice.Text) & ",'" & cmbCompCode.Text & "')")

        AdoCN.Execute("INSERT INTO tblPOLStockOut(Assortment,Assortment2,Pcs,Cts,Price,DocID,CompCode) " & _
                      "VALUES('" & txtOAssort.Text & "','" & txtAssortment.Text & "'," & CInt(txtPcs.Text) & "," & CDbl(txtCts.Text) & "," & CDbl(txtPrice.Text) & ",'9','" & cmbCompCode.Text & "')")

        MsgBox("Transfered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        ClearFields()
    End Sub
End Class