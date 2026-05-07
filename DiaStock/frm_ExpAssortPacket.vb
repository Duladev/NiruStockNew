
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_ExpAssortPacket
    Dim ParcelNo As String
    Dim PacketNo As String

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        Dim intTotPcs As Integer
        Dim dblTotCts As Double

        flxDetails.Rows.Clear()
        If txtParNo.Text <> "" And cmbDept.Text <> "" Then
            If Asc(e.KeyChar) = 13 Then
                txtParNo.Text = UCase(txtParNo.Text)

                txtPktNo.Text = "0001"
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT MAX(PktNo2) AS MaxPkt FROM tblExpSizingPlan WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND LEN(PktNo2) > 0", AdoCN, 1, 1)
                If Not IsDBNull(rsComSql.Fields("MaxPkt").Value) Then
                    txtPktNo.Text = Format(rsComSql.Fields("MaxPkt").Value + 1, "0000")
                End If
                rsComSql = Nothing

                intTotPcs = 0
                dblTotCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT TOP (100) PERCENT PktNo, SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts " & _
                              "FROM dbo.tblExpSizingPlan " & _
                              "WHERE (OK = 0) AND (Department = '" & cmbDept.Text & "') AND (ParNo = '" & txtParNo.Text & "') " & _
                              "GROUP BY PktNo " & _
                              "ORDER BY PktNo", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        If rsComSql.Fields("Pcs").Value > 0 Then
                            flxDetails.Rows.Add(rsComSql.Fields("PktNo").Value,
                                                rsComSql.Fields("Pcs").Value,
                                                Math.Round(rsComSql.Fields("Cts").Value, 3),
                                                txtParNo.Text)

                            intTotPcs = intTotPcs + rsComSql.Fields("Pcs").Value
                            dblTotCts = dblTotCts + Math.Round(rsComSql.Fields("Cts").Value, 3)
                        End If

                        rsComSql.MoveNext()
                    End While
                End If
                rsComSql = Nothing

                txtTotPcs.Text = intTotPcs
                txtTotCts.Text = dblTotCts
                txtCount.Text = flxDetails.RowCount
            End If
        End If
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(1, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(2, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub cmdSelect_Click(sender As Object, e As EventArgs) Handles cmdSelect.Click
        If txtParNo.Text = "" Then Exit Sub

        flxSelected.Rows.Clear()
        For intIndex = 0 To flxDetails.Rows.Count - 1
            flxSelected.Rows.Add(flxDetails.Item(0, intIndex).Value,
                                 flxDetails.Item(1, intIndex).Value,
                                 flxDetails.Item(2, intIndex).Value,
                                 flxDetails.Item(3, intIndex).Value)

        Next

        flxDetails.Rows.Clear()
        txtTotPcs.Text = CalTotalPcs(flxDetails)
        txtTotCts.Text = CalTotalCts(flxDetails)
        txtSelPcs.Text = CalTotalPcs(flxSelected)
        txtSelCts.Text = CalTotalCts(flxSelected)
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        Dim intRow As Integer

        For intRow = 0 To flxSelected.Rows.Count - 1
            If flxDetails.Item(0, flxDetails.CurrentRow.Index).Value = flxSelected.Item(0, intRow).Value Then
                MsgBox("Already Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        flxSelected.Rows.Add(flxDetails.Item(0, flxDetails.CurrentRow.Index).Value,
                             flxDetails.Item(1, flxDetails.CurrentRow.Index).Value,
                             flxDetails.Item(2, flxDetails.CurrentRow.Index).Value,
                             flxDetails.Item(3, flxDetails.CurrentRow.Index).Value)

        flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)
        txtTotPcs.Text = CalTotalPcs(flxDetails)
        txtTotCts.Text = CalTotalCts(flxDetails)
        txtSelPcs.Text = CalTotalPcs(flxSelected)
        txtSelCts.Text = CalTotalCts(flxSelected)
    End Sub

    Private Sub flxSelected_DoubleClick(sender As Object, e As EventArgs) Handles flxSelected.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxSelected.Rows.RemoveAt(flxSelected.CurrentRow.Index)
            txtSelPcs.Text = CalTotalPcs(flxSelected)
            txtSelCts.Text = CalTotalCts(flxSelected)
        End If
    End Sub

    Private Sub SavePacket()
        Dim intRow As Integer

        For intRow = 0 To flxSelected.Rows.Count - 1
            AdoCN.Execute("UPDATE tblExpSizingPlan SET OK = 1, PktNo2 = '" & txtPktNo.Text & "' WHERE (Department = '" & cmbDept.Text & "') AND (ParNo = '" & txtParNo.Text & "') AND (PktNo = '" & flxSelected.Item(0, intRow).Value & "')")
        Next

        MsgBox("Packets Transfered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        ClearText()
    End Sub

    Private Sub ClearText()
        cmbDept.Text = ""
        txtParNo.Text = ""
        txtPktNo.Text = ""
        flxDetails.Rows.Clear()
        flxSelected.Rows.Clear()
        txtTotPcs.Text = ""
        txtTotCts.Text = ""
        txtSelPcs.Text = ""
        txtSelCts.Text = ""
        txtCount.Text = ""
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearText()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        SavePacket()
    End Sub

    Private Sub frm_ExpAssortPacket_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Department(cmbDept)
    End Sub
End Class