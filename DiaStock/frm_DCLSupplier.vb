
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLSupplier

    Private Sub frm_DCLSupplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Supplier()
        ClearFields()
    End Sub

    Private Sub Load_Supplier()
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblSuppliers ORDER BY CompanyName", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("SupplierCode").Value,
                                    rsComSql.Fields("CompanyName").Value,
                                    rsComSql.Fields("SupDorNo").Value,
                                    rsComSql.Fields("SupCity").Value,
                                    rsComSql.Fields("Country").Value,
                                    rsComSql.Fields("ContactName").Value,
                                    rsComSql.Fields("GSTNo").Value,
                                    rsComSql.Fields("TelNo").Value,
                                    rsComSql.Fields("Cert1").Value,
                                    rsComSql.Fields("Cert2").Value,
                                    rsComSql.Fields("Cert3").Value,
                                    rsComSql.Fields("Cert4").Value,
                                    rsComSql.Fields("Cert5").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        txtCode.Text = flxDetails.Item(0, flxDetails.CurrentRow.Index).Value
        txtSupplier.Text = flxDetails.Item(1, flxDetails.CurrentRow.Index).Value
        txtAddress1.Text = flxDetails.Item(2, flxDetails.CurrentRow.Index).Value
        txtAddress2.Text = flxDetails.Item(3, flxDetails.CurrentRow.Index).Value
        txtCountry.Text = flxDetails.Item(4, flxDetails.CurrentRow.Index).Value
        txtContact.Text = flxDetails.Item(5, flxDetails.CurrentRow.Index).Value
        txtGst.Text = flxDetails.Item(6, flxDetails.CurrentRow.Index).Value
        txtTele.Text = flxDetails.Item(7, flxDetails.CurrentRow.Index).Value
        txtCert1.Text = flxDetails.Item(8, flxDetails.CurrentRow.Index).Value
        txtCert2.Text = flxDetails.Item(9, flxDetails.CurrentRow.Index).Value
        txtCert3.Text = flxDetails.Item(10, flxDetails.CurrentRow.Index).Value
        txtCert4.Text = flxDetails.Item(11, flxDetails.CurrentRow.Index).Value
        txtCert5.Text = flxDetails.Item(12, flxDetails.CurrentRow.Index).Value
    End Sub

    Private Sub ClearFields()
        txtCode.Text = "0"
        txtSupplier.Text = ""
        txtAddress1.Text = ""
        txtAddress2.Text = ""
        txtCountry.Text = ""
        txtContact.Text = ""
        txtGst.Text = ""
        txtTele.Text = ""
        txtCert1.Text = ""
        txtCert2.Text = ""
        txtCert3.Text = ""
        txtCert4.Text = ""
        txtCert5.Text = ""
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub Save()
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If ValidateFields() = False Then Exit Sub

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblSuppliers WHERE SupplierCode = '" & CDbl(txtCode.Text) & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblSuppliers(CompanyName,SupDorNo,SupCity,Country,ContactName,GSTNo,TelNo,FaxNo,EmailNo,SupWeb,Remarks,Status,CompRefNo,ShipDet,Cert1,Cert2,Cert3,Cert4,Cert5) " & _
                              "VALUES('" & txtSupplier.Text & "','" & txtAddress1.Text & "','" & txtAddress2.Text & "'," & _
                                "'" & txtCountry.Text & "','" & txtContact.Text & "','" & txtGst.Text & "','" & txtTele.Text & "','','','','','','','','" & txtCert1.Text & "','" & txtCert2.Text & "','" & txtCert3.Text & "','" & txtCert4.Text & "','" & txtCert5.Text & "')")

                MsgBox("Supplier Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Else
                PBResponse = MsgBox("Already Exists. Do you want to Update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
                If PBResponse = MsgBoxResult.Yes Then
                    AdoCN.Execute("UPDATE tblSuppliers SET CompanyName = '" & txtSupplier.Text & "',SupDorNo = '" & txtAddress1.Text & "'," & _
                                    "SupCity = '" & txtAddress2.Text & "',Country = '" & txtCountry.Text & "',ContactName = '" & txtContact.Text & "'," & _
                                    "GSTNo = '" & txtGst.Text & "',TelNo = '" & txtTele.Text & "',Cert1 = '" & txtCert1.Text & "',Cert2 = '" & txtCert2.Text & "'," & _
                                    "Cert3 = '" & txtCert3.Text & "',Cert4 = '" & txtCert4.Text & "',Cert5 = '" & txtCert5.Text & "' " & _
                                  "WHERE SupplierCode = '" & CDbl(txtCode.Text) & "'")

                    MsgBox("Supplier Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                End If
            End If
            rsComSql = Nothing
            ClearFields()
            Load_Supplier()
        End If

    End Sub

    Private Function ValidateFields() As Boolean
        ValidateFields = True

        If Not Len(Trim(txtSupplier.Text)) > 0 Then
            MsgBox("Invalid Supplier", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(txtAddress1.Text)) > 0 Then
            MsgBox("Invalid Address 1", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(txtAddress2.Text)) > 0 Then
            MsgBox("Invalid Address 2", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(txtCountry.Text)) > 0 Then
            MsgBox("Invalid Country", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

    End Function

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub
End Class