
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_ExpAssortPriceUpdate

    Private Sub ClearFields()
        cmbDept.Text = ""
        flxDetails.Rows.Clear()
        txtParNo.Text = ""
        txtPcs.Text = "0"
        txtCts.Text = "0"
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub Update_ListPrice()
        Dim intRow As Integer

        For intRow = 0 To flxDetails.Rows.Count - 1
            rsComSql_4 = New ADODB.Recordset
            rsComSql_4.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
            If rsComSql_4.RecordCount Then
                If Mid(flxDetails.Item(2, intRow).Value, 1, 1) = "S" Then
                    flxDetails.Item(5, intRow).Value = Math.Round((CDbl(flxDetails.Item(3, intRow).Value) * rsComSql_4.Fields("StonePrice").Value) / CDbl(flxDetails.Item(4, intRow).Value), 2)
                Else
                    flxDetails.Item(5, intRow).Value = Format(rsComSql_4.Fields("MarketPrice").Value, "#0.00")
                End If
            Else
                rsComSql_5 = New ADODB.Recordset
                rsComSql_5.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
                If rsComSql_5.RecordCount Then
                    flxDetails.Item(5, intRow).Value = Format(rsComSql_5.Fields("ListCost").Value, "#0.00")
                End If
                rsComSql_5 = Nothing
            End If
            rsComSql_4 = Nothing
        Next

    End Sub

    Private Function ParcelFound(ByVal strDept As String, ByVal strParceNo As String) As Boolean
        ParcelFound = True
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblExpSizingPacket WHERE ParNo = '" & strParceNo & "' AND Department = '" & strDept & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount > 0 Then
            ParcelFound = True
        Else
            ParcelFound = False
        End If
        rsComSql_1 = Nothing

    End Function

    Private Function CalTotalPcs() As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Rows(intRow).Cells(6).EditedFormattedValue = True Then
                CalTotalPcs = CalTotalPcs + CInt(flxDetails.Item(3, intRow).Value)
            End If
        Next
        Return CalTotalPcs
    End Function

    Private Function CalTotalCts() As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Rows(intRow).Cells(6).EditedFormattedValue = True Then
                CalTotalCts = CalTotalCts + CDbl(flxDetails.Item(4, intRow).Value)
            End If
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
        Return CalTotalCts
    End Function

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtParNo.Text = UCase(txtParNo.Text)
            If ParcelFound(cmbDept.Text, txtParNo.Text) = True Then
                Load_ParcelDetails()
            Else
                MsgBox("Department and Parcel No. not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtParNo.Text = ""
                txtParNo.Focus()
            End If
        End If
    End Sub

    Private Sub Load_ParcelDetails()
        Dim rsComSql_1 As New ADODB.Recordset
        Dim blnFound As Boolean

        flxDetails.Rows.Clear()
        blnFound = False
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.tblExpSizingPacket.Department, dbo.tblExpSizingPacket.ParNo, dbo.tblExpSizingPacket.PktNo, dbo.tblExpSizingPacket.ReturnType, " & _
                            "dbo.tblExpSizingTypes.ReturnType AS Assortment, dbo.tblExpSizingTypes.Pcs, dbo.tblExpSizingTypes.Cts, dbo.tblExpSizingTypes.EstCts, dbo.tblExpSizingTypes.BasePrice, dbo.tblExpSizingTypes.ID " & _
                        "FROM dbo.tblExpSizingPacket INNER JOIN dbo.tblExpSizingTypes ON dbo.tblExpSizingPacket.Department = dbo.tblExpSizingTypes.Department AND " & _
                            "dbo.tblExpSizingPacket.ParNo = dbo.tblExpSizingTypes.ParNo AND dbo.tblExpSizingPacket.PktNo = dbo.tblExpSizingTypes.PktNo " & _
                        "WHERE (dbo.tblExpSizingPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblExpSizingPacket.Department = '" & cmbDept.Text & "') " & _
                        "ORDER BY dbo.tblExpSizingPacket.PktNo, Assortment", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            blnFound = True
            While Not rsComSql_1.EOF
                flxDetails.Rows.Add(rsComSql_1.Fields("ParNo").Value,
                                    rsComSql_1.Fields("PktNo").Value,
                                    rsComSql_1.Fields("Assortment").Value,
                                    rsComSql_1.Fields("Pcs").Value,
                                    rsComSql_1.Fields("Cts").Value,
                                    rsComSql_1.Fields("BasePrice").Value,
                                    True,
                                    rsComSql_1.Fields("ID").Value)

                rsComSql_1.MoveNext()
            End While
        Else
            MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql_1 = Nothing

        txtPcs.Text = CalTotalPcs()
        txtCts.Text = CalTotalCts()
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        txtPcs.Text = CalTotalPcs()
        txtCts.Text = CalTotalCts()

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtParNo.Text = "" Then MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If flxDetails.Rows.Count < 1 Then MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(6, intRow).Value = True Then
                AdoCN.Execute("UPDATE tblExpSizingTypes SET BasePrice = '" & CDbl(flxDetails.Item(5, intRow).Value) & "' WHERE ID = '" & CDbl(flxDetails.Item(7, intRow).Value) & "'")
            End If
        Next

        MsgBox("Updated Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        PBResponse = MsgBox("Are you sure to Save the New List Price?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            Save()
        End If
    End Sub

    Private Sub cmdUpdate_Click(sender As Object, e As EventArgs) Handles cmdUpdate.Click
        PBResponse = MsgBox("Are you sure to Update the List Price?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            Update_ListPrice()
        End If
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub cmbDept_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbDept.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtParNo.Focus()
        End If
    End Sub

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 6 Then
            txtPcs.Text = CalTotalPcs()
            txtCts.Text = CalTotalCts()
        End If
    End Sub

    Private Sub flxDetails_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellValueChanged
        txtPcs.Text = CalTotalPcs()
        txtCts.Text = CalTotalCts()
    End Sub

    Private Sub frm_ExpAssortPriceUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_DepartmentProd(cmbDept)

        txtPcs.Text = "0"
        txtCts.Text = "0"
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
    End Sub
End Class