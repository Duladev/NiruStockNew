
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixAssortRejectSend
    Dim strFolderPath As String

    Private Sub frm_MixAssortRejectTransfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        If strDBName = "DiaStock" Then
            strFolderPath = "Mix\"
        Else
            strFolderPath = "DiaSalesMix\"
        End If

        Load_Rejects()
    End Sub

    Private Sub Load_Rejects()
        Dim dblWtAvgOld As Double
        Dim dblPrice As Double

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblMixRejects WHERE Stock = 1 AND OK = 3 ORDER BY ParNo,PktNo,Assortment", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                dblWtAvgOld = 0
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT AvgCost FROM dbo.tblAssortList WHERE Assortment = '" & rsComSql.Fields("OldAssort").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    dblWtAvgOld = rsComSql_1.Fields("AvgCost").Value
                End If
                rsComSql_1 = Nothing

                dblPrice = rsComSql.Fields("Price").Value
                If Mid(rsComSql.Fields("Assortment").Value, 1, 1) = "S" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT AvgStonePrice FROM dbo.tblAssortList WHERE Assortment = '" & rsComSql.Fields("Assortment").Value & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        dblPrice = (rsComSql_1.Fields("AvgStonePrice").Value * rsComSql.Fields("Pcs").Value) / rsComSql.Fields("Cts").Value
                    End If
                    rsComSql_1 = Nothing

                    dblPrice = Math.Round(dblPrice, 2)
                End If

                flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    rsComSql.Fields("Cts").Value,
                                    rsComSql.Fields("Assortment").Value,
                                    rsComSql.Fields("OrgAssort").Value,
                                    dblPrice,
                                    rsComSql.Fields("InID").Value,
                                    rsComSql.Fields("ImportNo").Value,
                                    rsComSql.Fields("OldAssort").Value, False,
                                    rsComSql.Fields("ID").Value,
                                    dblWtAvgOld,
                                    Format(rsComSql.Fields("RejDate").Value, "yyyy/MM/dd"),
                                    rsComSql.Fields("SupParNo").Value,
                                    rsComSql.Fields("Origin").Value,
                                    Format(rsComSql.Fields("ProdRejDate").Value, "yyyy/MM/dd"))

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub AddToStock()
        Dim intRow As Integer
        Dim blnSave As Boolean

        blnSave = False
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            'For intRow = 0 To flxDetails.Rows.Count - 1
            '    If flxDetails.Item(11, intRow).Value = True Then
            '        rsComSql_1 = New ADODB.Recordset
            '        rsComSql_1.Open("SELECT Assortment FROM dbo.tblAssortList WHERE Assortment = '" & flxDetails.Item(5, intRow).Value & "'", AdoCN, 1, 1)
            '        If rsComSql_1.RecordCount = 0 Then
            '            MsgBox("Invalid Assortment - " & flxDetails.Item(5, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '            Exit Sub
            '        End If
            '        rsComSql_1 = Nothing
            '    End If
            'Next

            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(11, intRow).Value = True Then
                    blnSave = True

                    'Update Trf = 0
                    'AdoCN.Execute("UPDATE tblMixRejects SET Assortment = '" & UCase(flxDetails.Item(5, intRow).Value) & "', OK = 0 " & _
                    '              "WHERE ID = " & Val(flxDetails.Item(12, intRow).Value) & "")

                    AdoCN.Execute("UPDATE tblMixRejects SET OK = 0 WHERE ID = " & Val(flxDetails.Item(12, intRow).Value) & "")

                End If
            Next
            If blnSave = True Then
                MsgBox("Added to the Stock Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Else
                MsgBox("No Records to Add", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            Load_Rejects()
        End If
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        AddToStock()
    End Sub

    Private Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        Load_Rejects()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        ExportToExcel(flxDetails)
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

        txtPcs.Text = CalTotalPcs(flxDetails, 2)
        txtCts.Text = CalTotalCts(flxDetails, 3)
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(11).EditedFormattedValue = True Then
                CalTotalPcs = CalTotalPcs + Val(flxSample.Item(intCalRow, intRow).Value)
            End If
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(11).EditedFormattedValue = True Then
                CalTotalCts = CalTotalCts + Val(flxSample.Item(intCalRow, intRow).Value)
            End If
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs)
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixRejectConvertsCat.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 11 Then
            txtPcs.Text = CalTotalPcs(flxDetails, 2)
            txtCts.Text = CalTotalCts(flxDetails, 3)
        End If
    End Sub
End Class