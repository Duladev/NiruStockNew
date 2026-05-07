
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLCostingEdit

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
        rsComSql = New ADODB.Recordset
        mStrSQL = "SELECT * " & _
                  "FROM dbo.tblCosting  " & _
                  "WHERE (ExportNo = " & txtExportNo.Text & ") ORDER BY ID"
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            dtpInvDate.Value = Format(rsComSql.Fields("DateCreated").Value, "MM/dd/yyyy")
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("Department").Value, rsComSql.Fields("Assortment").Value, rsComSql.Fields("Reference1").Value, rsComSql.Fields("Reference2").Value, rsComSql.Fields("RoughPcs").Value,
                                    Math.Round(rsComSql.Fields("RoughCts").Value, 3), rsComSql.Fields("Price").Value, rsComSql.Fields("ExportNo").Value, rsComSql.Fields("ExportPcs").Value,
                                    Math.Round(rsComSql.Fields("ExportCts").Value, 3), rsComSql.Fields("Labour").Value, rsComSql.Fields("GrLabour").Value, rsComSql.Fields("NFEValue").Value,
                                    rsComSql.Fields("Totals").Value, rsComSql.Fields("BOINo").Value, rsComSql.Fields("BaseCost").Value, rsComSql.Fields("Category").Value, rsComSql.Fields("LotID").Value,
                                    rsComSql.Fields("PackingListNo").Value, rsComSql.Fields("PackingType").Value, rsComSql.Fields("RghLabour").Value, rsComSql.Fields("AssLabour").Value, rsComSql.Fields("ID").Value,
                                    rsComSql.Fields("BalancePcs").Value, rsComSql.Fields("BalanceCts").Value, rsComSql.Fields("MasterPcs").Value, rsComSql.Fields("MasterCts").Value, rsComSql.Fields("ClientID").Value,
                                    rsComSql.Fields("Margin").Value, rsComSql.Fields("LabourE").Value, rsComSql.Fields("MaxValue").Value, Format(rsComSql.Fields("DateCreated").Value, "yyyy/MM/dd"), rsComSql.Fields("HardCost").Value,
                                    rsComSql.Fields("InID").Value, rsComSql.Fields("SalesRate").Value, rsComSql.Fields("Status").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        cmbClient.Items.Clear()
        rsComSql = New ADODB.Recordset
        mStrSQL = "SELECT DISTINCT ClientID " & _
                  "FROM dbo.tblCosting  " & _
                  "WHERE (ExportNo = " & txtExportNo.Text & ") ORDER BY ClientID"
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbClient.Items.Add(rsComSql.Fields("ClientID").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtPcs.Text = CalTotalPcs(flxDetails, 4)
        txtCts.Text = CalTotalCts(flxDetails, 5)
        txtTotPcs.Text = CalTotalPcs(flxDetails, 8)
        txtTotCts.Text = CalTotalCts(flxDetails, 9)
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ClearFields()
        flxDetails.Rows.Clear()
        cmbClient.Items.Clear()
        cmbClient.Text = ""
        txtNewExportNo.Text = ""
        txtPackNo.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtTotPcs.Text = ""
        txtTotCts.Text = ""
        txtExportNo.Text = ""
        dtpInvDate.Value = Date.Now
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
        txtExportNo.Focus()
    End Sub

    Private Sub Calculate()
        Dim intRow As Integer

        CheckRowsColumns()

        For intRow = 0 To flxDetails.Rows.Count - 1
            flxDetails.Item(12, intRow).Value = Math.Round(CDbl(flxDetails.Item(6, intRow).Value) * CDbl(flxDetails.Item(5, intRow).Value), 2)
            flxDetails.Item(13, intRow).Value = Math.Round(CDbl(flxDetails.Item(12, intRow).Value) + CDbl(flxDetails.Item(10, intRow).Value) + CDbl(flxDetails.Item(11, intRow).Value) + CDbl(flxDetails.Item(29, intRow).Value), 2)

            If flxDetails.Item(0, intRow).Value = "Mix" Or flxDetails.Item(0, intRow).Value = "Exports" Or flxDetails.Item(0, intRow).Value = "PolishBox" Or flxDetails.Item(0, intRow).Value = "PolishBoxTrf" Then
                flxDetails.Item(32, intRow).Value = flxDetails.Item(6, intRow).Value
            Else
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT ItemCost, ImpPrice FROM tblImport WHERE LotNo = '" & flxDetails.Item(17, intRow).Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    flxDetails.Item(32, intRow).Value = Math.Round((CDbl(flxDetails.Item(6, intRow).Value) / rsComSql.Fields("ItemCost").Value) * rsComSql.Fields("ImpPrice").Value, 3)
                End If
                rsComSql = Nothing
            End If
        Next

        txtPcs.Text = CalTotalPcs(flxDetails, 4)
        txtCts.Text = CalTotalCts(flxDetails, 5)
        txtTotPcs.Text = CalTotalPcs(flxDetails, 8)
        txtTotCts.Text = CalTotalCts(flxDetails, 9)

        MsgBox("Calculated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
    End Sub

    Private Sub cmdCalculate_Click(sender As Object, e As EventArgs) Handles cmdCalculate.Click
        Calculate()
    End Sub

    Private Sub UpdateBase()
        Dim strAssortment As String

        For intRow = 0 To flxDetails.Rows.Count - 1
            strAssortment = flxDetails.Item(1, intRow).Value
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT ListCost FROM tblDCLPermanents WHERE ItemName = '" & strAssortment & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                flxDetails.Item(15, intRow).Value = rsComSql.Fields("ListCost").Value
            End If
            rsComSql = Nothing
        Next

        MsgBox("Base Cost Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
    End Sub

    Private Sub CheckRowsColumns()
        Dim intRow As Integer
        Dim intCol As Integer

        For intRow = 0 To flxDetails.Rows.Count - 1
            For intCol = 0 To flxDetails.Columns.Count - 1
                If intCol <> 27 Then
                    If Len(flxDetails.Item(intCol, intRow).Value) = 0 Then
                        MsgBox("Invalid Cell (Row - " & intRow + 1 & ", Col - " & intCol + 1 & ")", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If
            Next
        Next

        For intRow = 0 To flxDetails.Rows.Count - 1
            For intCol = 0 To flxDetails.Columns.Count - 1
                Select Case intCol
                    Case 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 15, 17, 18, 20, 21, 23, 24, 25, 26, 28, 29, 30, 32, 33, 34
                        If Not IsNumeric(flxDetails.Item(intCol, intRow).Value) = True Then
                            MsgBox("Invalid Cell (Row - " & intRow + 1 & ", Col - " & intCol + 1 & ")", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                    Case Else

                End Select
            Next
        Next
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then

            CheckRowsColumns()

            For intRow = 0 To flxDetails.Rows.Count - 1
                AdoCN.Execute("UPDATE tblCosting " & _
                               "SET Reference1 = '" & flxDetails.Item(2, intRow).Value & "',Reference2 = '" & flxDetails.Item(3, intRow).Value & "',RoughPcs = " & CDbl(flxDetails.Item(4, intRow).Value) & "," & _
                                   "RoughCts = " & CDbl(flxDetails.Item(5, intRow).Value) & ",Price = " & CDbl(flxDetails.Item(6, intRow).Value) & ", " & _
                                   "ExportPcs = " & CDbl(flxDetails.Item(8, intRow).Value) & ",ExportCts = " & CDbl(flxDetails.Item(9, intRow).Value) & ", " & _
                                   "NFEValue = " & CDbl(flxDetails.Item(12, intRow).Value) & ",Labour = " & CDbl(flxDetails.Item(10, intRow).Value) & ", " & _
                                   "Totals = " & CDbl(flxDetails.Item(13, intRow).Value) & ",ExportNo = " & CDbl(flxDetails.Item(7, intRow).Value) & ", " & _
                                   "PackingListNo = " & CDbl(flxDetails.Item(18, intRow).Value) & ",PackingType = '" & flxDetails.Item(19, intRow).Value & "', " & _
                                   "Category = '" & flxDetails.Item(16, intRow).Value & "',ModifyBy = '" & PBUser_ID & "', " & _
                                   "BaseCost = " & CDbl(flxDetails.Item(15, intRow).Value) & ",LotID = " & CDbl(flxDetails.Item(17, intRow).Value) & ", " & _
                                   "RghLabour = " & CDbl(flxDetails.Item(20, intRow).Value) & ",AssLabour = " & CDbl(flxDetails.Item(21, intRow).Value) & ", " & _
                                   "GrLabour = " & CDbl(flxDetails.Item(11, intRow).Value) & ",BOINo = '" & flxDetails.Item(14, intRow).Value & "', " & _
                                   "BalancePcs = " & CDbl(flxDetails.Item(23, intRow).Value) & ",BalanceCts = " & CDbl(flxDetails.Item(24, intRow).Value) & ", " & _
                                   "MasterPcs = " & CDbl(flxDetails.Item(25, intRow).Value) & ",MasterCts = " & CDbl(flxDetails.Item(26, intRow).Value) & ", " & _
                                   "Margin = " & CDbl(flxDetails.Item(28, intRow).Value) & ",LabourE = " & CDbl(flxDetails.Item(29, intRow).Value) & ", " & _
                                   "MaxValue = " & CDbl(flxDetails.Item(30, intRow).Value) & ",DateCreated = '" & Format(dtpInvDate.Value, "MM/dd/yyyy") & "', " & _
                                   "ExportDate = '" & Format(dtpInvDate.Value, "MM/dd/yyyy") & "',HardCost = " & CDbl(flxDetails.Item(32, intRow).Value) & ", " & _
                                   "InID = " & CDbl(flxDetails.Item(33, intRow).Value) & ",Department = '" & flxDetails.Item(0, intRow).Value & "', " & _
                                   "SalesRate = " & CDbl(flxDetails.Item(34, intRow).Value) & ",Assortment = '" & flxDetails.Item(1, intRow).Value & "'," & _
                                   "Status = '" & flxDetails.Item(35, intRow).Value & "' " & _
                              "WHERE ID = " & CDbl(flxDetails.Item(22, intRow).Value) & "")

            Next

            MsgBox("Costing Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
            txtExportNo.Focus()

        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub txtMargin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMargin.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtMargin.Text)
    End Sub

    Private Sub AddMargin()
        Dim intRow As Integer
        Dim dblMargin As Double

        If txtMargin.Text = "" Then MsgBox("Invalid Margin Percentage", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        dblMargin = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(27, intRow).Value = cmbClient.Text Then
                dblMargin = Math.Round((CDbl(flxDetails.Item(13, intRow).Value) * CDbl(txtMargin.Text)) / 100, 2)
                flxDetails.Item(28, intRow).Value = dblMargin
            End If
        Next

        MsgBox("Margin Added", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
    End Sub

    Private Sub cmdMargin_Click(sender As Object, e As EventArgs) Handles cmdMargin.Click
        AddMargin()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub frm_DCLCostingEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
        dtpInvDate.Value = Date.Now
    End Sub

    Private Sub txtNewExportNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNewExportNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub txtPackNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPackNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub UpdateCosting()
        If txtExportNo.Text = "" Then MsgBox("Invalid Export No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtNewExportNo.Text = "" Then MsgBox("Invalid New Export No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtPackNo.Text = "" Then MsgBox("Invalid Packing List No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If txtExportNo.Text = txtNewExportNo.Text Then
                MsgBox("Same Export Number", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            AdoCN.Execute("UPDATE tblCosting SET ExportNo = " & CDbl(txtNewExportNo.Text) & ",PackingListNo = " & CDbl(txtPackNo.Text) & ",ModifyBy = '" & PBUser_ID & "' WHERE ExportNo = " & CDbl(txtExportNo.Text) & "")

            MsgBox("Costing Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

            flxDetails.Rows.Clear()
            cmbClient.Items.Clear()
            cmbClient.Text = ""
            txtNewExportNo.Text = ""
            txtPackNo.Text = ""
            txtExportNo.Text = ""
            txtExportNo.Focus()
        End If
        
    End Sub

    Private Sub cmdUpdate_Click(sender As Object, e As EventArgs) Handles cmdUpdate.Click
        UpdateCosting()
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(intCalRow, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(intCalRow, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Function CalTotalValue(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Double
        Dim intRow As Integer

        CalTotalValue = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalValue = CalTotalValue + Val(flxSample.Item(intCalRow, intRow).Value)
        Next
        CalTotalValue = Math.Round(CalTotalValue, 2)
    End Function

    Private Sub cmdUpdateBase_Click(sender As Object, e As EventArgs) Handles cmdUpdateBase.Click
        UpdateBase()
    End Sub
End Class