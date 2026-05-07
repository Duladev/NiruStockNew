
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLCostingDelete

    Private Sub frm_DCLCostingDelete_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub txtExportNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtExportNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtExportNo.Text <> "" Then
                Load_SavedData()
            End If
        End If
    End Sub

    Private Sub Load_SavedData()
        flxDetails.Rows.Clear()
        flxExport.Rows.Clear()
        rsComSql = New ADODB.Recordset
        mStrSQL = "SELECT * " & _
                  "FROM dbo.tblCosting  " & _
                  "WHERE (ExportNo = " & txtExportNo.Text & ") ORDER BY ID"
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("Department").Value, rsComSql.Fields("Assortment").Value, rsComSql.Fields("Reference1").Value, rsComSql.Fields("Reference2").Value, rsComSql.Fields("RoughPcs").Value,
                                    rsComSql.Fields("RoughCts").Value, rsComSql.Fields("Price").Value, rsComSql.Fields("ExportNo").Value, rsComSql.Fields("ExportPcs").Value,
                                    rsComSql.Fields("ExportCts").Value, rsComSql.Fields("Labour").Value, rsComSql.Fields("GrLabour").Value, rsComSql.Fields("NFEValue").Value,
                                    rsComSql.Fields("Totals").Value, rsComSql.Fields("LotID").Value, rsComSql.Fields("PackingListNo").Value, rsComSql.Fields("PackingType").Value,
                                    rsComSql.Fields("ID").Value, Format(rsComSql.Fields("DateCreated").Value, "yyyy/MM/dd"), rsComSql.Fields("HardCost").Value)

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT RecordID, Department, Assortment, Price, Reference1, Reference2, ExpPcs, ExpCts, RoughPcs, RoughCts, Status, SystemDateTime " & _
                                "FROM tblExportVarification " & _
                                "WHERE (Department = '" & rsComSql.Fields("Department").Value & "') AND (Assortment = '" & rsComSql.Fields("Assortment").Value & "') AND (Reference2 = '" & rsComSql.Fields("Reference2").Value & "') AND (Reference1 = '" & rsComSql.Fields("Reference1").Value & "') AND (Status = 'E') " & _
                                "ORDER BY RecordID", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    rsComSql_1.MoveFirst()
                    While Not rsComSql_1.EOF
                        flxExport.Rows.Add(rsComSql_1.Fields("Department").Value, rsComSql_1.Fields("Assortment").Value, rsComSql_1.Fields("Reference1").Value,
                                           rsComSql_1.Fields("Reference2").Value, rsComSql_1.Fields("RoughPcs").Value, rsComSql_1.Fields("RoughCts").Value,
                                           rsComSql_1.Fields("Price").Value, rsComSql_1.Fields("ExpPcs").Value, rsComSql_1.Fields("ExpCts").Value,
                                           rsComSql_1.Fields("RecordID").Value, Format(rsComSql_1.Fields("SystemDateTime").Value, "yyyy/MM/dd"))

                        rsComSql_1.MoveNext()
                    End While
                End If
                rsComSql_1 = Nothing

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ClearFields()
        flxDetails.Rows.Clear()
        flxExport.Rows.Clear()
        txtExportNo.Text = ""
        txtExportNo.Focus()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub Delete()
        Dim intRow As Integer

        If txtExportNo.Text <> "" Then
            PBResponse = MsgBox("Are you sure to Delete this Costing?", MsgBoxStyle.Information + vbYesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                For intRow = 0 To flxDetails.Rows.Count - 1
                    AdoCN.Execute("DELETE FROM tblCosting WHERE ID = " & CDbl(flxDetails.Item(17, intRow).Value) & "")
                Next

                For intRow = 0 To flxExport.Rows.Count - 1
                    AdoCN.Execute("UPDATE tblExportVarification SET Status = 'A' WHERE RecordID = " & CDbl(flxExport.Item(9, intRow).Value) & "")
                Next

                MsgBox("Costing Deleted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
                txtExportNo.Focus()
            End If
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Delete()
    End Sub
End Class