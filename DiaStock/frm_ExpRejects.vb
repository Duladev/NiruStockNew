
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_ExpRejects

    Private Sub ClearFields()
        cmbDept.Text = ""
        flxDetails.Rows.Clear()
        txtParNo.Text = ""
        txtPcs.Text = "0"
        txtCts.Text = "0"
        txtNewPcs.Text = ""
        txtNewCts.Text = ""
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        cmbAssort.Text = ""
        cmbPack.Items.Clear()
        cmbPack.Text = ""
        cmbRate.Text = ""
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
            rsComSql_4.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
            If rsComSql_4.RecordCount Then
                If Mid(flxDetails.Item(2, intRow).Value, 1, 1) = "S" Then
                    flxDetails.Item(4, intRow).Value = Math.Round((CDbl(flxDetails.Item(2, intRow).Value) * rsComSql_4.Fields("StonePrice").Value) / CDbl(flxDetails.Item(3, intRow).Value), 2)
                Else
                    flxDetails.Item(4, intRow).Value = Format(rsComSql_4.Fields("MarketPrice").Value, "#0.00")
                End If
            Else
                rsComSql_5 = New ADODB.Recordset
                rsComSql_5.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                If rsComSql_5.RecordCount Then
                    flxDetails.Item(4, intRow).Value = Format(rsComSql_5.Fields("ListCost").Value, "#0.00")
                End If
                rsComSql_5 = Nothing
            End If
            rsComSql_4 = Nothing
        Next

    End Sub

    Private Function ParcelFound(ByVal strDept As String, ByVal strParceNo As String) As Boolean
        ParcelFound = True
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblExpRejExports WHERE ParNo = '" & strParceNo & "' AND Department = '" & strDept & "'", AdoCN, 1, 1)
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
            CalTotalPcs = CalTotalPcs + CInt(flxDetails.Item(2, intRow).Value)
        Next
        Return CalTotalPcs
    End Function

    Private Function CalTotalCts() As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            CalTotalCts = CalTotalCts + CDbl(flxDetails.Item(3, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
        Return CalTotalCts
    End Function

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtParNo.Text = UCase(txtParNo.Text)
            If ParcelFound(cmbDept.Text, txtParNo.Text) = True Then
                Load_PackNo()
            Else
                MsgBox("Department and Parcel No. not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtParNo.Text = ""
                txtParNo.Focus()
            End If
        End If
    End Sub

    Private Sub Load_PackNo()
        cmbPack.Items.Clear()
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT PackNo FROM tblExpRejExports WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' GROUP BY PackNo ORDER BY PackNo", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                cmbPack.Items.Add(rsComSql_1.Fields("PackNo").Value)
                rsComSql_1.MoveNext()
            End While
        End If
        rsComSql_1 = Nothing
    End Sub

    Private Sub Load_ParcelDetails()
        Dim rsComSql_1 As New ADODB.Recordset
        Dim blnFound As Boolean

        flxDetails.Rows.Clear()
        blnFound = False
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM dbo.tblExpRejExportsDetails  " & _
                        "WHERE (ParNo = '" & txtParNo.Text & "') AND (Department = '" & cmbDept.Text & "') AND (PackNo = '" & cmbPack.Text & "') " & _
                        "ORDER BY ReturnType", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            blnFound = True
            While Not rsComSql_1.EOF
                flxDetails.Rows.Add(rsComSql_1.Fields("ParNo").Value,
                                    rsComSql_1.Fields("ReturnType").Value,
                                    rsComSql_1.Fields("Pcs").Value,
                                    rsComSql_1.Fields("Cts").Value,
                                    rsComSql_1.Fields("BasePrice").Value,
                                    rsComSql_1.Fields("ID").Value)

                rsComSql_1.MoveNext()
            End While
        Else
            MsgBox("New Parcel", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql_1 = Nothing

        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        cmbRate.Text = ""
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblExpRejExports WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PackNo = '" & cmbPack.Text & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            txtTotPcs.Text = rsComSql_1.Fields("Pcs").Value
            txtTotCts.Text = rsComSql_1.Fields("Cts").Value
            cmbRate.Text = rsComSql_1.Fields("RateCode").Value
        End If
        rsComSql_1 = Nothing

        txtPcs.Text = CalTotalPcs()
        txtCts.Text = CalTotalCts()
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

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim intRow As Integer
        Dim dblPrice As Double

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbAssort.Text = "" Then MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtNewPcs.Text = "" Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtNewCts.Text = "" Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtNewPcs.Text) <= 0 Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtNewCts.Text) <= 0 Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If CDbl(txtPcs.Text) + CDbl(txtNewPcs.Text) > CDbl(txtTotPcs.Text) Then MsgBox("Invalid Total Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If CDbl(txtTotCts.Text) < Math.Round(CDbl(txtCts.Text) + CDbl(txtNewCts.Text), 3) Then
            MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        For intRow = 0 To flxDetails.Rows.Count - 1
            If cmbAssort.Text = flxDetails.Item(1, intRow).Value Then
                MsgBox("Assortment already entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        dblPrice = 0
        rsComSql_4 = New ADODB.Recordset
        rsComSql_4.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & cmbAssort.Text & "' AND Active = 1", AdoCN, 1, 1)
        If rsComSql_4.RecordCount Then
            If Mid(cmbAssort.Text, 1, 1) = "S" Then
                dblPrice = Math.Round((CDbl(txtNewPcs.Text) * rsComSql_4.Fields("StonePrice").Value) / CDbl(txtNewCts.Text), 2)
            Else
                dblPrice = Format(rsComSql_4.Fields("MarketPrice").Value, "#0.00")
            End If
        Else
            rsComSql_5 = New ADODB.Recordset
            rsComSql_5.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & cmbAssort.Text & "'", AdoCN, 1, 1)
            If rsComSql_5.RecordCount Then
                dblPrice = rsComSql_5.Fields("ListCost").Value
            Else
                rsComSql_6 = New ADODB.Recordset
                rsComSql_6.Open("SELECT * FROM tblExpRejTypes WHERE RejType = '" & cmbAssort.Text & "'", AdoCN, 1, 1)
                If rsComSql_6.RecordCount Then
                    dblPrice = 0
                Else
                    MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql_6 = Nothing
            End If
            rsComSql_5 = Nothing
        End If
        rsComSql_4 = Nothing

        flxDetails.Rows.Add(txtParNo.Text,
                            UCase(cmbAssort.Text),
                            txtNewPcs.Text,
                            txtNewCts.Text,
                            Format(dblPrice, "#0.00"),
                            0)

        txtPcs.Text = CalTotalPcs()
        txtCts.Text = CalTotalCts()

        cmbAssort.Text = ""
        txtNewPcs.Text = ""
        txtNewCts.Text = ""

        cmbAssort.Focus()
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        txtPcs.Text = CalTotalPcs()
        txtCts.Text = CalTotalCts()

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbRate.Text = "" Then MsgBox("Invalid Rate Code", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtParNo.Text = "" Then MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbPack.Text = "" Then MsgBox("Invalid Package No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If flxDetails.Rows.Count < 1 Then MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtPcs.Text) <> CDbl(txtTotPcs.Text) Then MsgBox("Pcs not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtCts.Text) <> CDbl(txtTotCts.Text) Then MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        For intRow = 0 To flxDetails.Rows.Count - 1
            rsComSql_4 = New ADODB.Recordset
            rsComSql_4.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & flxDetails.Item(1, intRow).Value & "' AND Active = 1", AdoCN, 1, 1)
            If rsComSql_4.RecordCount Then

            Else
                rsComSql_5 = New ADODB.Recordset
                rsComSql_5.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                If rsComSql_5.RecordCount Then

                Else
                    'MsgBox("Invalid Assortment - " & flxDetails.Item(2, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    'Exit Sub
                End If
                rsComSql_5 = Nothing
            End If
            rsComSql_4 = Nothing
        Next

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblExpRejExportsDetails WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PackNo = '" & cmbPack.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            PBResponse = MsgBox("Are you sure to Update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                AdoCN.Execute("DELETE FROM tblExpRejExportsDetails WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PackNo = '" & cmbPack.Text & "'")
                For intRow = 0 To flxDetails.Rows.Count - 1
                    AdoCN.Execute("INSERT INTO tblExpRejExportsDetails(Department,ParNo,ReturnType,Pcs,Cts,BasePrice,PackNo) " & _
                                  "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & UCase(flxDetails.Item(1, intRow).Value) & "'," & CInt(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",'" & cmbPack.Text & "')")
                Next
                AdoCN.Execute("UPDATE tblExpRejExports SET RateCode = '" & cmbRate.Text & "' WHERE ParNo = '" & txtParNo.Text & "' AND PackNo = '" & cmbPack.Text & "'")

                MsgBox("Updated Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            For intRow = 0 To flxDetails.Rows.Count - 1
                AdoCN.Execute("INSERT INTO tblExpRejExportsDetails(Department,ParNo,ReturnType,Pcs,Cts,BasePrice,PackNo) " & _
                              "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & UCase(flxDetails.Item(1, intRow).Value) & "'," & CInt(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",'" & cmbPack.Text & "')")
            Next
            AdoCN.Execute("UPDATE tblExpRejExports SET RateCode = '" & cmbRate.Text & "' WHERE ParNo = '" & txtParNo.Text & "' AND PackNo = '" & cmbPack.Text & "'")

            MsgBox("Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing

        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdUpdate_Click(sender As Object, e As EventArgs) Handles cmdUpdate.Click
        PBResponse = MsgBox("Are you sure to Update the List Price?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            Update_ListPrice()
        End If
    End Sub

    Private Sub Delete()
        PBResponse = MsgBox("Are you sure to Delete this packet?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblExpRejExportsDetails WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PackNo = '" & cmbPack.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                AdoCN.Execute("DELETE FROM tblExpRejExportsDetails WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PackNo = '" & cmbPack.Text & "'")

                MsgBox("Deleted Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Else
                MsgBox("Invalid Parcel", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            rsComSql = Nothing

            ClearFields()
        End If
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Delete()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub cmbDept_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbDept.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtParNo.Focus()
        End If
    End Sub

    Private Sub Update_Assortment()
        Dim intRow As Integer

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtParNo.Text = "" Then MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbPack.Text = "" Then MsgBox("Invalid Package No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If flxDetails.Rows.Count < 1 Then MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblExpRejExportsDetails WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PackNo = '" & cmbPack.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            PBResponse = MsgBox("Are you sure to Update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                For intRow = 0 To flxDetails.Rows.Count - 1
                    rsComSql_4 = New ADODB.Recordset
                    rsComSql_4.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & flxDetails.Item(1, intRow).Value & "' AND Active = 1", AdoCN, 1, 1)
                    If rsComSql_4.RecordCount Then

                    Else
                        rsComSql_5 = New ADODB.Recordset
                        rsComSql_5.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                        If rsComSql_5.RecordCount Then

                        Else
                            'MsgBox("Invalid Assortment - " & flxDetails.Item(2, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            'Exit Sub
                        End If
                        rsComSql_5 = Nothing
                    End If
                    rsComSql_4 = Nothing
                Next
                Update_ListPrice()

                For intRow = 0 To flxDetails.Rows.Count - 1
                    AdoCN.Execute("UPDATE tblExpRejExportsDetails SET ReturnType = '" & UCase(flxDetails.Item(1, intRow).Value) & "', BasePrice = " & CDbl(flxDetails.Item(4, intRow).Value) & " WHERE ID = " & CDbl(flxDetails.Item(5, intRow).Value) & "")
                Next

                MsgBox("Updated Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            MsgBox("Invalid Parcel", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing

        ClearFields()
    End Sub

    Private Sub cmdUpdateAssort_Click(sender As Object, e As EventArgs) Handles cmdUpdateAssort.Click
        Update_Assortment()
    End Sub

    Private Sub frm_ExpRejects_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_DepartmentProd(cmbDept)
        Load_Types()
        Load_RateCode()

        txtPcs.Text = "0"
        txtCts.Text = "0"
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
    End Sub

    Private Sub flxDetails_DoubleClick(sender As Object, e As EventArgs) Handles flxDetails.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)
            txtPcs.Text = CalTotalPcs()
            txtCts.Text = CalTotalCts()
        End If
    End Sub

    Private Sub Load_Types()
        cmbAssort.Items.Clear()
        rsComSql_4 = New ADODB.Recordset
        rsComSql_4.Open("SELECT * FROM tblExpRejTypes ORDER BY RejType", AdoCN, 1, 1)
        If rsComSql_4.RecordCount Then
            rsComSql_4.MoveFirst()
            While Not rsComSql_4.EOF
                cmbAssort.Items.Add(rsComSql_4.Fields("RejType").Value)
                rsComSql_4.MoveNext()
            End While
        End If
        rsComSql_4 = Nothing
    End Sub

    Private Sub Load_RateCode()
        cmbRate.Items.Clear()
        rsComSql_4 = New ADODB.Recordset
        rsComSql_4.Open("SELECT * FROM tblCuttingCharges WHERE Department = 'ProcessReject' ORDER BY RateCode", AdoCN, 1, 1)
        If rsComSql_4.RecordCount Then
            rsComSql_4.MoveFirst()
            While Not rsComSql_4.EOF
                cmbRate.Items.Add(rsComSql_4.Fields("RateCode").Value)
                rsComSql_4.MoveNext()
            End While
        End If
        rsComSql_4 = Nothing
    End Sub

    Private Sub Load_Assortments()
        cmbAssort.Items.Clear()
        rsComSql_4 = New ADODB.Recordset
        rsComSql_4.Open("SELECT * FROM tblDCLPermanents WHERE ItemName LIKE 'B%' OR ItemName LIKE 'C%' OR ItemName LIKE 'V%' ORDER BY ItemName", AdoCN, 1, 1)
        If rsComSql_4.RecordCount Then
            rsComSql_4.MoveFirst()
            While Not rsComSql_4.EOF
                cmbAssort.Items.Add(rsComSql_4.Fields("ItemName").Value)
                rsComSql_4.MoveNext()
            End While
        End If
        rsComSql_4 = Nothing
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        Load_Assortments()
    End Sub

    Private Sub cmbPack_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPack.SelectedIndexChanged
        Load_ParcelDetails()
    End Sub
End Class