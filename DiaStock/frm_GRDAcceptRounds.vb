
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDAcceptRounds

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ClearFields()
        flxParcel.Rows.Clear()
        flxAvailable.Rows.Clear()
        flxSelected.Rows.Clear()
        txtPcs.Text = ""
        txtCts.Text = ""

        Load_Parcels()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub Load_Parcels()
        flxParcel.Rows.Clear()
        flxAvailable.Rows.Clear()
        flxSelected.Rows.Clear()

        txtPcs.Text = ""
        txtCts.Text = ""

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT Department, ParNo " & _
                      "FROM dbo.tblGrading_RndPacket " & _
                      "WHERE(Status = 0) " & _
                      "GROUP BY Department, ParNo " & _
                      "HAVING (Department = 'Rounds') " & _
                      "ORDER BY ParNo", AdoCN, 1, 1)
        If rsComSql.RecordCount > 0 Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxParcel.Rows.Add(rsComSql.Fields("ParNo").Value,
                                   "A")

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub flxParcel_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxParcel.CellClick
        Dim selected_parno As String

        selected_parno = flxParcel.Item(0, flxParcel.CurrentRow.Index).Value
        flxAvailable.Rows.Clear()

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT Department, ParNo, PktNo, PktPcs, PktCts " & _
                      "FROM dbo.tblGrading_RndPacket " & _
                      "WHERE (Status = 0) AND (Department = 'Rounds') AND (ParNo = '" & selected_parno & "') " & _
                      "ORDER BY PktNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxAvailable.Rows.Add(rsComSql.Fields("ParNo").Value,
                                      rsComSql.Fields("PktNo").Value,
                                      rsComSql.Fields("PktPcs").Value,
                                      Math.Round(rsComSql.Fields("PktCts").Value, 3))

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub flxAvailable_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxAvailable.CellClick
        Dim intRow As Integer

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

    Private Sub Save()
        Dim e As Integer

        For e = 0 To flxSelected.Rows.Count - 1
            AdoCN.Execute("UPDATE tblGrading_RndPacket SET Status = 1 WHERE Department = 'Rounds' AND ParNo = '" & flxSelected.Item(0, e).Value & "' AND PktNo = '" & flxSelected.Item(1, e).Value & "'")
        Next

        MsgBox("Records Successfully Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub frm_GRDAcceptRounds_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Parcels()
    End Sub
End Class