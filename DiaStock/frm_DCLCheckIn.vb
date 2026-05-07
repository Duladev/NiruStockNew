
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLCheckIn

    Private Sub frm_DCLCheckIn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbDepartment.Items.Add("Rounds")
        cmbDepartment.Items.Add("RoundsNLE")
    End Sub

    Private Sub cmbDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDepartment.SelectedIndexChanged
        If cmbDepartment.Text = "" Then Exit Sub

        flxParcel.Rows.Clear()
        flxAvailable.Rows.Clear()
        flxSelected.Rows.Clear()

        rsComSql = New ADODB.Recordset
        If cmbDepartment.Text = "Rounds" Then
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRndPacket.ParNo " & _
                          "FROM dbo.tblRndPacket INNER JOIN dbo.tblParcel ON dbo.tblRndPacket.ParNo = dbo.tblParcel.GrpParNo " & _
                          "WHERE (dbo.tblParcel.Depart = 'Rounds') AND (dbo.tblParcel.Complete = 0) AND (dbo.tblRndPacket.CheckIn = 0) AND (dbo.tblRndPacket.CheckOut = 0) AND " & _
                                "(dbo.tblRndPacket.BrutIn = 0) AND (dbo.tblRndPacket.BrutOut = 0) AND (dbo.tblRndPacket.ProdIss = 0) AND (NOT (dbo.tblRndPacket.DelDate IS NULL)) " & _
                          "GROUP BY dbo.tblRndPacket.ParNo " & _
                          "ORDER BY dbo.tblRndPacket.ParNo", AdoCN, 1, 1)
        Else
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblExtPacket.ParNo " & _
                          "FROM dbo.tblExtPacket INNER JOIN dbo.tblParcel ON dbo.tblExtPacket.ParNo = dbo.tblParcel.GrpParNo AND dbo.tblExtPacket.Department = dbo.tblParcel.Depart " & _
                          "WHERE (dbo.tblParcel.Depart = 'RoundsNLE') AND (dbo.tblParcel.Complete = 0) AND (dbo.tblExtPacket.CheckIn = 0) AND (dbo.tblExtPacket.CheckOut = 0) AND " & _
                                "(dbo.tblExtPacket.BrutIn = 0) AND (dbo.tblExtPacket.BrutOut = 0) AND (dbo.tblExtPacket.ProdIss = 0) AND (NOT (dbo.tblExtPacket.DelDate IS NULL)) " & _
                          "GROUP BY dbo.tblExtPacket.ParNo " & _
                          "ORDER BY dbo.tblExtPacket.ParNo", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxParcel.Rows.Add(rsComSql.Fields("ParNo").Value,
                                   strRight(rsComSql.Fields("ParNo").Value, 1))
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub flxParcel_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxParcel.CellClick
        flxAvailable.Rows.Clear()
        rsComSql = New ADODB.Recordset
        If cmbDepartment.Text = "Rounds" Then
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRndPacket.ParNo, dbo.tblRndPacket.PktNo, dbo.tblRndPacket.PktPcs, dbo.tblRndPacket.PktCts " & _
                          "FROM dbo.tblRndPacket INNER JOIN dbo.tblParcel ON dbo.tblRndPacket.ParNo = dbo.tblParcel.GrpParNo " & _
                          "WHERE (NOT (dbo.tblRndPacket.DelDate IS NULL)) AND (dbo.tblRndPacket.CheckIn = 0) AND (dbo.tblRndPacket.CheckOut = 0) AND (dbo.tblRndPacket.BrutIn = 0) AND " & _
                                "(dbo.tblRndPacket.BrutOut = 0) AND (dbo.tblRndPacket.ProdIss = 0) AND (dbo.tblParcel.Depart = '" & cmbDepartment.Text & "') AND (dbo.tblParcel.Complete = 0) AND " & _
                                "(dbo.tblRndPacket.ParNo = '" & flxParcel.Item(0, flxParcel.CurrentRow.Index).Value & "') " & _
                          "ORDER BY dbo.tblRndPacket.PktNo", AdoCN, 1, 1)
        Else
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblExtPacket.ParNo, dbo.tblExtPacket.PktNo, dbo.tblExtPacket.PktPcs, dbo.tblExtPacket.PktCts " & _
                          "FROM dbo.tblExtPacket INNER JOIN dbo.tblParcel ON dbo.tblExtPacket.ParNo = dbo.tblParcel.GrpParNo AND dbo.tblExtPacket.Department = dbo.tblParcel.Depart " & _
                          "WHERE (dbo.tblParcel.Depart = 'RoundsNLE') AND (dbo.tblParcel.Complete = 0) AND (NOT (dbo.tblExtPacket.DelDate IS NULL)) AND (dbo.tblExtPacket.CheckIn = 0) AND " & _
                                "(dbo.tblExtPacket.CheckOut = 0) AND (dbo.tblExtPacket.BrutIn = 0) AND (dbo.tblExtPacket.BrutOut = 0) AND (dbo.tblExtPacket.ProdIss = 0) AND " & _
                                "(dbo.tblExtPacket.ParNo = '" & flxParcel.Item(0, flxParcel.CurrentRow.Index).Value & "') " & _
                          "ORDER BY dbo.tblExtPacket.PktNo", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxAvailable.Rows.Add(rsComSql.Fields("ParNo").Value,
                                      rsComSql.Fields("PktNo").Value,
                                      rsComSql.Fields("PktPcs").Value,
                                      rsComSql.Fields("PktCts").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub flxAvailable_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxAvailable.CellClick
        Dim intRow As Integer

        If cmbDepartment.Text <> "" Then
            For intRow = 0 To flxSelected.Rows.Count - 1
                If flxAvailable.Item(0, flxAvailable.CurrentRow.Index).Value = flxSelected.Item(0, intRow).Value And flxAvailable.Item(1, flxAvailable.CurrentRow.Index).Value = flxSelected.Item(1, intRow).Value Then
                    MsgBox("Already Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Next

            flxSelected.Rows.Add(flxAvailable.Item(0, flxAvailable.CurrentRow.Index).Value,
                                 flxAvailable.Item(1, flxAvailable.CurrentRow.Index).Value,
                                 flxAvailable.Item(2, flxAvailable.CurrentRow.Index).Value,
                                 flxAvailable.Item(3, flxAvailable.CurrentRow.Index).Value)

            flxAvailable.Rows.RemoveAt(flxAvailable.CurrentRow.Index)
            txtPcs.Text = CalTotalPcs(flxSelected)
            txtCts.Text = CalTotalCts(flxSelected)
        End If
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(2, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(3, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub flxSelected_DoubleClick(sender As Object, e As EventArgs) Handles flxSelected.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxSelected.Rows.RemoveAt(flxSelected.CurrentRow.Index)
            txtPcs.Text = CalTotalPcs(flxSelected)
            txtCts.Text = CalTotalCts(flxSelected)
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        If cmbDepartment.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If cmbDepartment.Text = "Rounds" Then
            For intRow = 0 To flxSelected.Rows.Count - 1
                AdoCN.Execute("UPDATE tblRndPacket SET CheckIn = 1 WHERE ParNo = '" & flxSelected.Item(0, intRow).Value & "' AND PktNo = '" & flxSelected.Item(1, intRow).Value & "'")
            Next
        Else
            For intRow = 0 To flxSelected.Rows.Count - 1
                AdoCN.Execute("UPDATE tblExtPacket SET CheckIn = 1 WHERE ParNo = '" & flxSelected.Item(0, intRow).Value & "' AND PktNo = '" & flxSelected.Item(1, intRow).Value & "' AND Department = 'RoundsNLE'")
            Next
        End If

        ClearFields
    End Sub

    Private Sub ClearFields()
        cmbDepartment.Text = ""
        flxParcel.Rows.Clear()
        flxAvailable.Rows.Clear()
        flxSelected.Rows.Clear()

        txtPcs.Text = ""
        txtCts.Text = ""
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub
End Class