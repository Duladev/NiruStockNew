
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_ExpRghIssues
    Private Sub Load_ExportParcels()
        Dim dtLoading As New DataTable("Parcels")

        dtLoading.Columns.Add("Parcel", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("Dept", System.Type.GetType("System.String"))

        cmbParcel.Text = ""
        cmbParcel.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT Department, ParNo " & _
                      "FROM dbo.tblExpSizingTypes " & _
                      "WHERE (OK = 0) AND (Sec = 1) " & _
                      "GROUP BY Department, ParNo " & _
                      "ORDER BY ParNo, Department", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                Dim dr As DataRow
                dr = dtLoading.NewRow

                dr("Parcel") = rsComSql.Fields("ParNo").Value
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

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        Load_ExportParcels()
    End Sub

    Private Sub Load_ExportDetails(ByVal strParcel As String, ByVal strDepartment As String)
        Dim intIssPcs As Integer
        Dim dblIssCts As Double
        Dim intRow As Integer

        Dim intRghIssPcs As Integer
        Dim dblRghIssCts As Double

        For intRow = 0 To flxDetails.Rows.Count - 1
            If strParcel = flxDetails.Item(1, intRow).Value Then
                MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        intIssPcs = txtAddPcs.Text
        dblIssCts = txtAddCts.Text

        intRghIssPcs = 0
        dblRghIssCts = 0
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblExpSizingTypes WHERE ParNo = '" & strParcel & "' AND Department = '" & strDepartment & "' AND OK = 0 AND Sec = 1 " & _
                      "ORDER BY Department, ParNo, PktNo, ReturnType", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                intRghIssPcs = 0
                dblRghIssCts = 0
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts " & _
                                "FROM tblExpRghTypes " & _
                                "WHERE (Department = '" & rsComSql.Fields("Department").Value & "') AND (PktNo = '" & rsComSql.Fields("PktNo").Value & "') AND " & _
                                    "(ParNo = '" & rsComSql.Fields("ParNo").Value & "') AND (Assortment = '" & rsComSql.Fields("ReturnType").Value & "') AND (Size = 2)", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                        intRghIssPcs = rsComSql_1.Fields("Pcs").Value
                        dblRghIssCts = rsComSql_1.Fields("Cts").Value
                    End If
                End If
                rsComSql_1 = Nothing

                If rsComSql.Fields("Pcs").Value - intRghIssPcs > 0 Then
                    flxDetails.Rows.Add(rsComSql.Fields("ReturnType").Value,
                                        rsComSql.Fields("ParNo").Value,
                                        rsComSql.Fields("Pcs").Value - intRghIssPcs,
                                        Math.Round(rsComSql.Fields("Cts").Value - dblRghIssCts, 3),
                                        "0",
                                        "0",
                                        rsComSql.Fields("Department").Value,
                                        rsComSql.Fields("PktNo").Value)
                End If

                intIssPcs = intIssPcs + rsComSql.Fields("Pcs").Value - intRghIssPcs
                dblIssCts = dblIssCts + rsComSql.Fields("Cts").Value - dblRghIssCts
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        dblIssCts = Math.Round(dblIssCts, 3)

        txtAddPcs.Text = intIssPcs
        txtPcs.Text = "0"
        txtAddCts.Text = Format(dblIssCts, "#0.000")
        txtCts.Text = "0"

    End Sub

    Private Sub cmbParcel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbParcel.SelectedIndexChanged
        If Not cmbParcel.SelectedItem Is Nothing Then
            Load_ExportDetails(cmbParcel.SelectedItem.Col1, cmbParcel.SelectedItem.Col2)
        End If
    End Sub

    Private Sub ClearFields()
        txtAddPcs.Text = "0"
        txtAddCts.Text = "0"
        txtPcs.Text = "0"
        txtCts.Text = "0"
        flxDetails.Rows.Clear()
    End Sub

    Private Sub frm_ExpRghIssues_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        ClearFields()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Function CalTotalPcs() As Double
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If Len(flxDetails.Item(5, intRow).Value) > 0 Then
                CalTotalPcs = CalTotalPcs + Val(flxDetails.Item(4, intRow).Value)
            End If
        Next

    End Function

    Private Function CalTotalCts() As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If Len(flxDetails.Item(5, intRow).Value) > 0 Then
                CalTotalCts = CalTotalCts + Val(flxDetails.Item(5, intRow).Value)
            End If
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
        Return CalTotalCts
    End Function

    Private Sub Save()
        Dim intRow As Integer
        Dim PBResponse

        If flxDetails.Rows.Count < 1 Then MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        For intRow = 0 To flxDetails.Rows.Count - 1
            If CDbl(flxDetails.Item(4, intRow).Value) > 0 Then
                If CDbl(flxDetails.Item(5, intRow).Value) <= 0 Then
                    MsgBox(flxDetails.Item(0, intRow).Value & " Invalid Issue Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            If CDbl(flxDetails.Item(5, intRow).Value) > 0 Then
                If CDbl(flxDetails.Item(4, intRow).Value) <= 0 Then
                    MsgBox(flxDetails.Item(0, intRow).Value & " Invalid Issue Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            If CDbl(flxDetails.Item(4, intRow).Value) > CDbl(flxDetails.Item(2, intRow).Value) Then
                MsgBox(flxDetails.Item(0, intRow).Value & " Invalid Issue Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(5, intRow).Value) > CDbl(flxDetails.Item(3, intRow).Value) Then
                MsgBox(flxDetails.Item(0, intRow).Value & " Invalid Issue Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(2, intRow).Value) = CDbl(flxDetails.Item(4, intRow).Value) Then
                If CDbl(flxDetails.Item(3, intRow).Value) <> CDbl(flxDetails.Item(5, intRow).Value) Then
                    MsgBox(flxDetails.Item(0, intRow).Value & " Invalid Issue Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            If CDbl(flxDetails.Item(3, intRow).Value) = CDbl(flxDetails.Item(5, intRow).Value) Then
                If CDbl(flxDetails.Item(2, intRow).Value) <> CDbl(flxDetails.Item(4, intRow).Value) Then
                    MsgBox(flxDetails.Item(0, intRow).Value & " Invalid Issue Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
        Next

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If CDbl(flxDetails.Item(4, intRow).Value) > 0 Then
                    AdoCN.Execute("INSERT INTO tblExpRghTypes(Department, ParNo, Assortment, OrderNo, Pcs, Cts, OK, Type, DDate, Assort1, Size, PktNo) " & _
                                  "VALUES('" & flxDetails.Item(6, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & _
                                        "'" & flxDetails.Item(0, intRow).Value & "',''," & CInt(flxDetails.Item(4, intRow).Value) & "," & _
                                        "" & CDbl(flxDetails.Item(5, intRow).Value) & ",0,'E','" & Format(Date.Now, "MM/dd/yyyy") & "','',2," & _
                                        "'" & flxDetails.Item(7, intRow).Value & "')")
                End If
            Next

            MsgBox("Transfered Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If

        flxDetails.Rows.Clear()
        txtAddPcs.Text = "0"
        txtAddCts.Text = "0"
        txtPcs.Text = "0"
        txtCts.Text = "0"
        cmbParcel.Text = ""
        cmbParcel.Items.Clear()
        Load_ExportParcels()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub
End Class