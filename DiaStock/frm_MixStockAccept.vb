
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixStockAccept
    Private Sub Load_Data()
        flxDetails.Rows.Clear()
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        rsComSql = New ADODB.Recordset
        If Len(txtAssortment.Text) = 0 Then
            rsComSql.Open("SELECT * FROM tblMixIntIssues WHERE IssDate = '" & Format(Date.Now, "MM/dd/yyyy") & "' AND (OK = 0) ORDER BY Assortment", AdoCN, 1, 1)
        Else
            rsComSql.Open("SELECT * FROM tblMixIntIssues WHERE IssDate = '" & Format(Date.Now, "MM/dd/yyyy") & "' AND (OK = 0) AND Assortment LIKE '%' + '" & txtAssortment.Text & "' + '%' ORDER BY Assortment", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("Assortment").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    Format(rsComSql.Fields("Cts").Value, "#0.000"),
                                    Format(rsComSql.Fields("IssDate").Value, "yyyy/MM/dd"),
                                    False,
                                    rsComSql.Fields("ID").Value,
                                    rsComSql.Fields("EmpNo2").Value)

                txtTotPcs.Text = CDbl(txtTotPcs.Text) + rsComSql.Fields("Pcs").Value
                txtTotCts.Text = Format(CDbl(txtTotCts.Text) + rsComSql.Fields("Cts").Value, "#0.000")

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Load_Data()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
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

    Private Sub Accept_Packet()
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse  = MsgBoxResult.Yes Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(4, intRow).Value = True Or flxDetails.Item(4, intRow).Value = "1" Then
                    AdoCN.Execute("UPDATE tblMixIntIssues SET OK = 1 WHERE ID = " & flxDetails.Item(5, intRow).Value & "")
                End If
            Next
            Load_Data()
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Accept_Packet()
    End Sub

    Private Sub frm_MixStockAccept_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(Me.flxDetails)
    End Sub

    Private Sub txtAssortment_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAssortment.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Load_Data()
        End If
    End Sub
End Class