
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDExportSummaryRnd
    Private Sub GetPackNo()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(PackNo) AS MaxNo FROM tblGrading_PackingListRnd", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If IsDBNull(rsComSql.Fields("MaxNo").Value) Then
                txtPackNo.Text = "1"
            Else
                txtPackNo.Text = rsComSql.Fields("MaxNo").Value + 1
            End If
        Else
            txtPackNo.Text = "1"
        End If
        rsComSql = Nothing
    End Sub

    Private Sub frm_GRDExportSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        GetPackNo()

        txtAddPcs.Text = "0"
        txtAddCts.Text = "0"
        txtPcs.Text = "0"
        txtCts.Text = "0"
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_ExportParcels()
        Dim dtLoading As New DataTable("Parcels")

        dtLoading.Columns.Add("Parcel", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("Dept", System.Type.GetType("System.String"))

        cmbParcel.Text = ""
        cmbParcel.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblGrading_RndSizingTypes.Department, dbo.tblParcel.ParcelNo " & _
                      "FROM dbo.tblGrading_RndSizingTypes INNER JOIN dbo.tblParcel ON dbo.tblGrading_RndSizingTypes.Department = dbo.tblParcel.Depart AND dbo.tblGrading_RndSizingTypes.ParNo = dbo.tblParcel.GrpParNo " & _
                      "WHERE (dbo.tblGrading_RndSizingTypes.OK = 0) AND (dbo.tblGrading_RndSizingTypes.Department = 'Rounds') AND (dbo.tblParcel.Complete = 0) " & _
                      "GROUP BY dbo.tblGrading_RndSizingTypes.Department, dbo.tblParcel.ParcelNo " & _
                      "ORDER BY dbo.tblParcel.ParcelNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                Dim dr As DataRow
                dr = dtLoading.NewRow

                dr("Parcel") = rsComSql.Fields("ParcelNo").Value
                dr("Dept") = rsComSql.Fields("Department").Value
                dtLoading.Rows.Add(dr)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        cmbParcel.SelectedIndex = -1
        cmbParcel.Items.Clear()
        cmbParcel.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
        cmbParcel.SourceDataString = New String(1) {"Parcel", "Dept"}
        cmbParcel.SourceDataTable = dtLoading

    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        GetPackNo()
        Load_ExportParcels()
    End Sub

    Private Sub Load_ExportDetails(ByVal strParcel As String, ByVal strDepartment As String)
        Dim intIssPcs As Integer
        Dim dblIssCts As Double
        Dim intRow As Integer

        For intRow = 0 To flxDetails.Rows.Count - 1
            If strParcel = flxDetails.Item(1, intRow).Value And strDepartment = flxDetails.Item(9, intRow).Value Then
                MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        intIssPcs = txtAddPcs.Text
        dblIssCts = txtAddCts.Text

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblParcel.ParcelNo, dbo.tblGrading_RndSizingTypes.ReturnType6, dbo.tblGrading_RndSizingTypes.ReturnType7, SUM(dbo.tblGrading_RndSizingTypes.Pcs) AS Pcs, " & _
                        "ROUND(SUM(dbo.tblGrading_RndSizingTypes.Cts), 3) AS Cts, dbo.tblGrading_RndSizeList.Price, dbo.tblGrading_RndSizeList.MainAssort " & _
                      "FROM dbo.tblGrading_RndSizingTypes INNER JOIN dbo.tblParcel ON dbo.tblGrading_RndSizingTypes.Department = dbo.tblParcel.Depart AND  " & _
                        "dbo.tblGrading_RndSizingTypes.ParNo = dbo.tblParcel.GrpParNo INNER JOIN dbo.tblGrading_RndSizeList ON dbo.tblGrading_RndSizingTypes.ReturnType6 = dbo.tblGrading_RndSizeList.AssortNo " & _
                      "WHERE (dbo.tblGrading_RndSizingTypes.OK = 0) AND (dbo.tblGrading_RndSizingTypes.Department = '" & strDepartment & "') AND (dbo.tblParcel.Complete = 0) AND  " & _
                        "(dbo.tblGrading_RndSizingTypes.OrderNo = '') AND (dbo.tblParcel.ParcelNo = '" & strParcel & "') " & _
                      "GROUP BY dbo.tblGrading_RndSizingTypes.ReturnType6, dbo.tblGrading_RndSizingTypes.ReturnType7, dbo.tblGrading_RndSizeList.Price, dbo.tblGrading_RndSizeList.MainAssort, dbo.tblParcel.ParcelNo " & _
                      "ORDER BY dbo.tblGrading_RndSizingTypes.ReturnType6", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                If rsComSql.Fields("Pcs").Value > 0 Then
                    flxDetails.Rows.Add(rsComSql.Fields("ReturnType6").Value,
                                        rsComSql.Fields("ParcelNo").Value,
                                        rsComSql.Fields("Pcs").Value,
                                        Format(Math.Round(rsComSql.Fields("Cts").Value, 3), "#0.000"),
                                        rsComSql.Fields("Pcs").Value,
                                        Format(Math.Round(rsComSql.Fields("Cts").Value, 3), "#0.000"),
                                        "0",
                                        Format(rsComSql.Fields("Price").Value, "#0.00"),
                                        Format(rsComSql.Fields("Price").Value * (rsComSql.Fields("Cts").Value), "#0.00"),
                                        strDepartment,
                                        "",
                                        False,
                                        "",
                                        rsComSql.Fields("ReturnType7").Value)
                End If

                intIssPcs = intIssPcs + rsComSql.Fields("Pcs").Value
                dblIssCts = dblIssCts + rsComSql.Fields("Cts").Value
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblParcel.ParcelNo, dbo.tblGrading_RndSizingTypes.ReturnType6, dbo.tblGrading_RndSizingTypes.ReturnType7, dbo.tblGrading_RndSizingTypes.Pcs, dbo.tblGrading_RndSizeList.Price, " & _
                        "dbo.tblGrading_RndSizingTypes.Cts, dbo.tblGrading_RndSizeList.MainAssort, dbo.tblGrading_RndSizingTypes.OrderNo " & _
                      "FROM dbo.tblGrading_RndSizingTypes INNER JOIN dbo.tblParcel ON dbo.tblGrading_RndSizingTypes.Department = dbo.tblParcel.Depart AND  " & _
                        "dbo.tblGrading_RndSizingTypes.ParNo = dbo.tblParcel.GrpParNo INNER JOIN dbo.tblGrading_RndSizeList ON dbo.tblGrading_RndSizingTypes.ReturnType6 = dbo.tblGrading_RndSizeList.AssortNo " & _
                      "WHERE (dbo.tblGrading_RndSizingTypes.OK = 0) AND (dbo.tblGrading_RndSizingTypes.Department = '" & strDepartment & "') AND (dbo.tblParcel.Complete = 0) AND  " & _
                        "(dbo.tblGrading_RndSizingTypes.OrderNo <> '') AND (dbo.tblParcel.ParcelNo = '" & strParcel & "') " & _
                      "ORDER BY dbo.tblGrading_RndSizingTypes.ReturnType6", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                If rsComSql.Fields("Pcs").Value > 0 Then
                    flxDetails.Rows.Add(rsComSql.Fields("ReturnType6").Value,
                                        rsComSql.Fields("ParcelNo").Value,
                                        rsComSql.Fields("Pcs").Value,
                                        Format(Math.Round(rsComSql.Fields("Cts").Value, 3), "#0.000"),
                                        rsComSql.Fields("Pcs").Value,
                                        Format(Math.Round(rsComSql.Fields("Cts").Value, 3), "#0.000"),
                                        "0",
                                        Format(rsComSql.Fields("Price").Value, "#0.00"),
                                        Format(rsComSql.Fields("Price").Value * (rsComSql.Fields("Cts").Value), "#0.00"),
                                        strDepartment,
                                        "",
                                        False,
                                        rsComSql.Fields("OrderNo").Value,
                                        rsComSql.Fields("ReturnType7").Value)
                End If

                intIssPcs = intIssPcs + rsComSql.Fields("Pcs").Value
                dblIssCts = dblIssCts + rsComSql.Fields("Cts").Value
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        dblIssCts = Math.Round(dblIssCts, 3)

        txtAddPcs.Text = intIssPcs
        txtPcs.Text = intIssPcs
        txtAddCts.Text = Format(dblIssCts, "#0.000")
        txtCts.Text = Format(dblIssCts, "#0.000")

        flxDetails.Focus()

    End Sub

    Private Sub cmbParcel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbParcel.SelectedIndexChanged
        If Not cmbParcel.SelectedItem Is Nothing Then
            Load_ExportDetails(cmbParcel.SelectedItem.Col1, cmbParcel.SelectedItem.Col2)
        End If
    End Sub

    Private Sub UpdateListPrice()
        Dim intRow As Integer

        For intRow = 0 To flxDetails.Rows.Count - 1
            rsComSql_4 = New ADODB.Recordset
            rsComSql_4.Open("SELECT * FROM tblGrading_RndSizeList WHERE NAME = '" & flxDetails.Item(0, intRow).Value & "'", AdoCN, 1, 1)
            If rsComSql_4.RecordCount Then
                flxDetails.Item(7, intRow).Value = Format(rsComSql_4.Fields("Price").Value, "#0.00")
                flxDetails.Item(8, intRow).Value = Format(Math.Round(rsComSql_4.Fields("Price").Value * CDbl(flxDetails.Item(5, intRow).Value), 2), "#0.00")
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

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(11, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(11, intRow).Value = False
            Next
        End If
    End Sub

    Private Sub ClearFields()
        GetPackNo()
        txtAddPcs.Text = "0"
        txtAddCts.Text = "0"
        txtPcs.Text = "0"
        txtCts.Text = "0"
        flxDetails.Rows.Clear()
        chkSelect.Checked = False
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        If txtPackNo.Text = "" Then MsgBox("Invalid Package No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If flxDetails.Rows.Count < 1 Then MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtAddPcs.Text) <> CDbl(txtPcs.Text) Then MsgBox("Pcs not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblGrading_PackingListRnd WHERE PackNo = " & Val(txtPackNo.Text) & "", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                For intRow = 0 To flxDetails.Rows.Count - 1
                    AdoCN.Execute("UPDATE tblGrading_PackingListRnd SET ActPcs = " & CInt(flxDetails.Item(4, intRow).Value) & ",ActCts = " & CDbl(flxDetails.Item(5, intRow).Value) & " " & _
                                  "WHERE ID = " & CDbl(flxDetails.Item(10, intRow).Value) & "")
                Next

                MsgBox("Updated Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(11, intRow).Value = True Then
                    AdoCN.Execute("INSERT INTO tblGrading_PackingListRnd(Department, PackNo, Assortment, ParNo, Pcs, Cts, ActPcs, ActCts, Price, OrderNo, SizeRange) " & _
                                  "VALUES('" & flxDetails.Item(9, intRow).Value & "'," & CDbl(txtPackNo.Text) & ",'" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & CInt(flxDetails.Item(2, intRow).Value) & "," & _
                                        "" & CDbl(flxDetails.Item(3, intRow).Value) & "," & CInt(flxDetails.Item(4, intRow).Value) & "," & CDbl(flxDetails.Item(5, intRow).Value) & "," & _
                                        "" & CDbl(flxDetails.Item(7, intRow).Value) & ",'" & flxDetails.Item(12, intRow).Value & "','" & flxDetails.Item(13, intRow).Value & "')")

                    AdoCN.Execute("UPDATE tblGrading_RndSizingTypes SET OK = 1 WHERE Department  = '" & flxDetails.Item(9, intRow).Value & "' AND LEFT(ParNo, 6) = '" & flxDetails.Item(1, intRow).Value & "' AND ReturnType6 = '" & flxDetails.Item(0, intRow).Value & "' AND OK = 0")
                End If
            Next

            MsgBox("Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing

        flxDetails.Rows.Clear()
        txtAddPcs.Text = "0"
        txtAddCts.Text = "0"
        txtPcs.Text = "0"
        txtCts.Text = "0"
        GetPackNo()
        cmbParcel.Text = ""
        cmbParcel.Items.Clear()
        Load_ExportParcels()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub Load_PackingList()
        Dim intIssPcs As Integer
        Dim dblIssCts As Double
        Dim intActPcs As Integer
        Dim dblActCts As Double

        flxDetails.Rows.Clear()
        intIssPcs = 0
        dblIssCts = 0
        intActPcs = 0
        dblActCts = 0
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * " & _
                      "FROM dbo.tblGrading_PackingListRnd " & _
                      "WHERE (PackNo = " & Val(txtPackNo.Text) & ") " & _
                      "ORDER BY Assortment", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("Assortment").Value,
                                   rsComSql.Fields("ParNo").Value,
                                   rsComSql.Fields("Pcs").Value,
                                   Format(rsComSql.Fields("Cts").Value, "#0.000"),
                                   rsComSql.Fields("ActPcs").Value,
                                   Format(rsComSql.Fields("ActCts").Value, "#0.000"),
                                   Math.Round(rsComSql.Fields("Cts").Value - rsComSql.Fields("ActCts").Value, 3),
                                   Format(rsComSql.Fields("Price").Value, "#0.00"),
                                   Format(rsComSql.Fields("Price").Value * rsComSql.Fields("ActCts").Value, "#0.00"),
                                   rsComSql.Fields("Department").Value,
                                   rsComSql.Fields("ID").Value, True,
                                   rsComSql.Fields("OrderNo").Value,
                                   rsComSql.Fields("SizeRange").Value)

                intIssPcs = intIssPcs + rsComSql.Fields("Pcs").Value
                intActPcs = intActPcs + rsComSql.Fields("ActPcs").Value
                dblIssCts = dblIssCts + rsComSql.Fields("Cts").Value
                dblActCts = dblActCts + rsComSql.Fields("ActCts").Value
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        dblIssCts = Math.Round(dblIssCts, 3)
        dblActCts = Math.Round(dblActCts, 3)

        txtPcs.Text = intIssPcs
        txtAddPcs.Text = intActPcs
        txtCts.Text = Format(dblIssCts, "#0.000")
        txtAddCts.Text = Format(dblActCts, "#0.000")

        flxDetails.Focus()
    End Sub

    Private Sub txtPackNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPackNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            Load_PackingList()
        End If
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(4, intRow).Value)
        Next
    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            flxSample.Item(6, intRow).Value = Math.Round(Val(flxSample.Item(3, intRow).Value) - Val(flxSample.Item(5, intRow).Value), 3)
            flxSample.Item(8, intRow).Value = Format(Val(flxSample.Item(7, intRow).Value) * Val(flxSample.Item(5, intRow).Value), "#0.00")
            CalTotalCts = CalTotalCts + Val(flxSample.Item(5, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub flxDetails_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellValueChanged
        txtAddPcs.Text = CalTotalPcs(flxDetails)
        txtAddCts.Text = Format(CalTotalCts(flxDetails), "#0.000")
    End Sub
End Class