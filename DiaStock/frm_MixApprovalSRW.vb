
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixApprovalSRW

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ClearFields()
        cmbAssortment.Text = ""
        cmbSubject.Text = ""
        cmbRef.Text = ""
        txtMaxCost.Text = ""
        txtListPrice.Text = ""
        txtStonePrice.Text = ""
        txtOrderNo.Text = ""
        chkSelect.Checked = False
        txtDueDate.Text = ""
        txtPlanDate.Text = ""
        txtLen.Text = ""
        txtWid.Text = ""
        chkSelect.Checked = False
        flxDetails.Rows.Clear()
    End Sub

    Private Sub Load_Assortments()
        cmbAssortment.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT Assortment FROM dbo.tblAssortList WHERE (Assortment LIKE N'S%') AND (Active = 1) ORDER BY Assortment", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbAssortment.Items.Add(rsComSql.Fields("Assortment").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Subject()
        cmbSubject.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT Subject + ' ' + Subject2 AS SubjectFull FROM dbo.tblOrders WHERE (Complete = N'N') GROUP BY Subject + ' ' + Subject2 ORDER BY SubjectFull", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbSubject.Items.Add(rsComSql.Fields("SubjectFull").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub frm_MixApprovalSRW_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Assortments()
        Load_Subject()
        ClearFields()
    End Sub

    Private Sub cmbAssortment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAssortment.SelectedIndexChanged
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM dbo.tblAssortList WHERE (Assortment = '" & cmbAssortment.Text & "')", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            txtListPrice.Text = rsComSql.Fields("AvgCost").Value
            txtStonePrice.Text = rsComSql.Fields("StonePrice").Value
        End If
        rsComSql = Nothing

        Load_Details()
    End Sub

    Private Sub Load_Details()
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixOrderAssort.Assortment, dbo.tblMixOrderAssort.Subject, dbo.tblMixOrderAssort.Ref, ISNULL(dbo.VW_MixSubjectMaxCost.MaxCost, 0) AS MaxCost " & _
                      "FROM dbo.tblMixOrderAssort LEFT OUTER JOIN dbo.VW_MixSubjectMaxCost ON dbo.tblMixOrderAssort.Subject = dbo.VW_MixSubjectMaxCost.Subject AND dbo.tblMixOrderAssort.Ref = dbo.VW_MixSubjectMaxCost.RefNo " & _
                      "WHERE (dbo.tblMixOrderAssort.Assortment = '" & cmbAssortment.Text & "') " & _
                      "ORDER BY dbo.tblMixOrderAssort.Assortment, dbo.tblMixOrderAssort.Subject, dbo.tblMixOrderAssort.Ref", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("Assortment").Value,
                                    rsComSql.Fields("Subject").Value,
                                    rsComSql.Fields("Ref").Value,
                                    rsComSql.Fields("MaxCost").Value,
                                    False)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_DetailsAll()
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixOrderAssort.Assortment, dbo.tblMixOrderAssort.Subject, dbo.tblMixOrderAssort.Ref, ISNULL(dbo.VW_MixSubjectMaxCost.MaxCost, 0) AS MaxCost " & _
                      "FROM dbo.tblMixOrderAssort LEFT OUTER JOIN dbo.VW_MixSubjectMaxCost ON dbo.tblMixOrderAssort.Subject = dbo.VW_MixSubjectMaxCost.Subject AND dbo.tblMixOrderAssort.Ref = dbo.VW_MixSubjectMaxCost.RefNo " & _
                      "ORDER BY dbo.tblMixOrderAssort.Assortment, dbo.tblMixOrderAssort.Subject, dbo.tblMixOrderAssort.Ref", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("Assortment").Value,
                                    rsComSql.Fields("Subject").Value,
                                    rsComSql.Fields("Ref").Value,
                                    rsComSql.Fields("MaxCost").Value,
                                    False)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmbSubject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSubject.SelectedIndexChanged
        cmbRef.Items.Clear()
        cmbRef.Text = ""
        txtMaxCost.Text = ""
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblOrdersDtls.RefNo " & _
                      "FROM dbo.tblOrders INNER JOIN dbo.tblOrdersDtls ON dbo.tblOrders.OrderNo = dbo.tblOrdersDtls.OrderNo " & _
                      "WHERE (dbo.tblOrders.Complete = N'N') AND (dbo.tblOrders.Subject + ' ' + dbo.tblOrders.Subject2 = '" & cmbSubject.Text & "') " & _
                      "GROUP BY dbo.tblOrdersDtls.RefNo " & _
                      "ORDER BY dbo.tblOrdersDtls.RefNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbRef.Items.Add(rsComSql.Fields("RefNo").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub SaveAll()
        If cmbAssortment.Text = "" Then MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbSubject.Text = "" Then MsgBox("Invalid Subject", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbRef.Text <> "" Then MsgBox("Invalid Ref", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.tblOrdersDtls.RefNo " & _
                          "FROM dbo.tblOrders INNER JOIN dbo.tblOrdersDtls ON dbo.tblOrders.OrderNo = dbo.tblOrdersDtls.OrderNo " & _
                          "WHERE (dbo.tblOrders.Complete = N'N') AND (dbo.tblOrders.Subject + ' ' + dbo.tblOrders.Subject2 = '" & cmbSubject.Text & "') " & _
                          "GROUP BY dbo.tblOrdersDtls.RefNo " & _
                          "ORDER BY dbo.tblOrdersDtls.RefNo", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                rsComSql_1.MoveFirst()
                While Not rsComSql_1.EOF
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM dbo.tblMixOrderAssort WHERE (Assortment = '" & cmbAssortment.Text & "') AND (Subject = '" & cmbSubject.Text & "') AND (Ref = '" & Replace(rsComSql_1.Fields("RefNo").Value, "'", "''") & "')", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        AdoCN.Execute("INSERT INTO tblMixOrderAssort(Assortment,Subject,Ref) VALUES('" & cmbAssortment.Text & "','" & cmbSubject.Text & "','" & Replace(rsComSql_1.Fields("RefNo").Value, "'", "''") & "')")
                    End If
                    rsComSql = Nothing

                    rsComSql_1.MoveNext()
                End While
            End If
            rsComSql_1 = Nothing

            MsgBox("Successfully Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Load_Details()
        End If
    End Sub

    Private Sub Save()
        If cmbAssortment.Text = "" Then MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbSubject.Text = "" Then MsgBox("Invalid Subject", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbRef.Text = "" Then MsgBox("Invalid Ref", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM dbo.tblMixOrderAssort WHERE (Assortment = '" & cmbAssortment.Text & "') AND (Subject = '" & cmbSubject.Text & "') AND (Ref = '" & Replace(cmbRef.Text, "'", "''") & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblMixOrderAssort(Assortment,Subject,Ref) VALUES('" & cmbAssortment.Text & "','" & cmbSubject.Text & "','" & Replace(cmbRef.Text, "'", "''") & "')")

                MsgBox("Successfully Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Load_Details()
            Else
                MsgBox("Already Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub Delete()
        Dim intRow As Integer
        'If cmbAssortment.Text = "" Then MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        'If cmbSubject.Text = "" Then MsgBox("Invalid Subject", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        'If cmbRef.Text = "" Then MsgBox("Invalid Ref", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        PBResponse = MsgBox("Are you sure to Delete?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(4, intRow).Value = True Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM dbo.tblMixOrderAssort WHERE (Assortment = '" & flxDetails.Item(0, intRow).Value & "') AND (Subject = '" & flxDetails.Item(1, intRow).Value & "') AND (Ref = '" & Replace(flxDetails.Item(2, intRow).Value, "'", "''") & "')", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        AdoCN.Execute("DELETE FROM tblMixOrderAssort WHERE (Assortment = '" & flxDetails.Item(0, intRow).Value & "') AND (Subject = '" & flxDetails.Item(1, intRow).Value & "') AND (Ref = '" & Replace(flxDetails.Item(2, intRow).Value, "'", "''") & "')")
                    End If
                    rsComSql = Nothing
                End If
            Next

            MsgBox("Successfully Deleted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            cmbSubject.Text = ""
            cmbRef.Text = ""
            cmbRef.Items.Clear()
            chkSelect.Checked = False
            Load_Details()
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Delete()
    End Sub

    Private Sub cmdShow_Click(sender As Object, e As EventArgs) Handles cmdShow.Click
        Load_DetailsAll()
    End Sub

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        cmbAssortment.Text = flxDetails.Item(0, flxDetails.CurrentRow.Index).Value
        cmbSubject.Text = flxDetails.Item(1, flxDetails.CurrentRow.Index).Value
        cmbRef.Text = flxDetails.Item(2, flxDetails.CurrentRow.Index).Value
        txtMaxCost.Text = flxDetails.Item(3, flxDetails.CurrentRow.Index).Value
    End Sub

    Private Sub cmbRef_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRef.SelectedIndexChanged
        txtMaxCost.Text = "0"
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM VW_MixSubjectMaxCost WHERE (Subject = '" & cmbSubject.Text & "') AND (RefNo = '" & Replace(cmbRef.Text, "'", "''") & "')", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            txtMaxCost.Text = rsComSql.Fields("MaxCost").Value
        End If
        rsComSql = Nothing

        txtLen.Text = ""
        txtWid.Text = ""
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblOrdersDtls WHERE (OrderNo = '" & txtOrderNo.Text & "') AND (RefNo = '" & Replace(cmbRef.Text, "'", "''") & "')", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            txtLen.Text = rsComSql.Fields("Length").Value
            txtWid.Text = rsComSql.Fields("Width").Value
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdSaveAll_Click(sender As Object, e As EventArgs) Handles cmdSaveAll.Click
        SaveAll()
    End Sub

    Private Sub txtOrderNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrderNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtOrderNo.Text <> "" Then
                If Len(txtOrderNo.Text) = 6 Then
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT OrderNo,Subject,Subject2,Dept,Niruref,DueDate FROM tblOrders WHERE OrderNo = '" & CInt(txtOrderNo.Text) & "' AND Complete = 'N'", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        cmbSubject.Text = rsComSql_2.Fields("Subject").Value & " " & rsComSql_2.Fields("Subject2").Value
                        txtDueDate.Text = Format(rsComSql_2.Fields("DueDate").Value, "yyyy/MM/dd")

                        txtPlanDate.Text = ""
                        rsComSql_3 = New ADODB.Recordset
                        rsComSql_3.Open("SELECT MAX(OrderDate) AS OrderDate FROM tblPlaneOrders WHERE (OrderNo = '" & CInt(txtOrderNo.Text) & "')", AdoCN, 1, 1)
                        If Not IsDBNull(rsComSql_3.Fields("OrderDate").Value) Then
                            txtPlanDate.Text = Format(rsComSql_3.Fields("OrderDate").Value, "yyyy/MM/dd")
                        End If
                        rsComSql_3 = Nothing
                    End If
                    rsComSql_2 = Nothing
                Else
                    MsgBox("Invalid Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    txtOrderNo.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(4, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(4, intRow).Value = False
            Next
        End If
    End Sub
End Class