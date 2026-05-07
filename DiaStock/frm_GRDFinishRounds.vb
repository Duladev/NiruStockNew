
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDFinishRounds

    Private Sub frm_GRDFinishRounds_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Parcels()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ClearFields()
        flxParcel.Rows.Clear()
        flxAvailable.Rows.Clear()
        flxSelected.Rows.Clear()
        txtPcs.Text = ""
        txtCts.Text = ""
        txtActCts.Text = ""

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
        txtActCts.Text = ""

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.VW_GradingCheckingBalance.Department, dbo.VW_GradingCheckingBalance.ParcelNo, dbo.tblGradingTrf.Grp " & _
                      "FROM dbo.VW_GradingCheckingBalance INNER JOIN dbo.tblGradingTrf ON dbo.VW_GradingCheckingBalance.Department = dbo.tblGradingTrf.Department AND " & _
                        "dbo.VW_GradingCheckingBalance.ParcelNo = dbo.tblGradingTrf.ParcelNo AND dbo.VW_GradingCheckingBalance.PktNo = dbo.tblGradingTrf.PktNo INNER JOIN " & _
                        "dbo.tblParcel ON dbo.VW_GradingCheckingBalance.Department = dbo.tblParcel.Depart AND dbo.VW_GradingCheckingBalance.ParcelNo = dbo.tblParcel.GrpParNo " & _
                      "WHERE (dbo.VW_GradingCheckingBalance.Trf_Pcs = dbo.VW_GradingCheckingBalance.Pcs + dbo.VW_GradingCheckingBalance.LostPcs) AND (dbo.tblGradingTrf.Trf = 0) AND (dbo.tblParcel.Complete = 0) " & _
                      "GROUP BY dbo.VW_GradingCheckingBalance.Department, dbo.VW_GradingCheckingBalance.ParcelNo, dbo.tblGradingTrf.Grp " & _
                      "HAVING (dbo.VW_GradingCheckingBalance.Department = 'Rounds') " & _
                      "ORDER BY dbo.VW_GradingCheckingBalance.ParcelNo", AdoCN, 1, 1)
        If rsComSql.RecordCount > 0 Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxParcel.Rows.Add(rsComSql.Fields("ParcelNo").Value,
                                   rsComSql.Fields("Grp").Value)

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
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.VW_GradingCheckingBalance.Department, dbo.VW_GradingCheckingBalance.ParcelNo, dbo.VW_GradingCheckingBalance.PktNo," & _
                        "dbo.VW_GradingCheckingBalance.Pcs, dbo.VW_GradingCheckingBalance.Cts " & _
                      "FROM dbo.VW_GradingCheckingBalance INNER JOIN dbo.tblGradingTrf ON dbo.VW_GradingCheckingBalance.Department = dbo.tblGradingTrf.Department AND " & _
                        "dbo.VW_GradingCheckingBalance.ParcelNo = dbo.tblGradingTrf.ParcelNo And dbo.VW_GradingCheckingBalance.PktNo = dbo.tblGradingTrf.PktNo " & _
                      "WHERE (dbo.tblGradingTrf.Trf = 0) AND (dbo.VW_GradingCheckingBalance.Trf_Pcs = dbo.VW_GradingCheckingBalance.Pcs + dbo.VW_GradingCheckingBalance.LostPcs) AND " & _
                        "(dbo.VW_GradingCheckingBalance.Department = 'Rounds') AND (dbo.VW_GradingCheckingBalance.ParcelNo = '" & selected_parno & "') " & _
                      "ORDER BY dbo.VW_GradingCheckingBalance.PktNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxAvailable.Rows.Add(rsComSql.Fields("ParcelNo").Value,
                                      rsComSql.Fields("PktNo").Value,
                                      rsComSql.Fields("Pcs").Value,
                                      Math.Round(rsComSql.Fields("Cts").Value, 3),
                                      Math.Round(rsComSql.Fields("Cts").Value, 3))

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
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

    Private Function CalTotalCtsAct(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCtsAct = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCtsAct = CalTotalCtsAct + Val(flxSample.Item(4, intRow).Value)
        Next
        CalTotalCtsAct = Math.Round(CalTotalCtsAct, 3)
    End Function

    Private Sub flxSelected_DoubleClick(sender As Object, e As EventArgs) Handles flxSelected.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxSelected.Rows.RemoveAt(flxSelected.CurrentRow.Index)
            txtPcs.Text = CalTotalPcs(flxSelected)
            txtCts.Text = CalTotalCts(flxSelected)
            txtActCts.Text = CalTotalCtsAct(flxSelected)
        End If
    End Sub

    Private Sub Save()
        Dim trfPCS As Double
        Dim trfCts As Double
        Dim e As Integer

        For e = 0 To flxSelected.Rows.Count - 1
            trfPCS = flxSelected.Item(2, e).Value
            trfCts = flxSelected.Item(3, e).Value

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT ParNo FROM tblGrading_RndPacket WHERE Department = 'Rounds' AND ParNo = '" & flxSelected.Item(0, e).Value & "' AND PktNo = '" & flxSelected.Item(1, e).Value & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblGrading_RndPacket(Department,ParNo,PktNo,PktPcs,PktCts,PktType,PktSize,PktGroup,PktRef,FM) " & _
                              "VALUES('Rounds','" & flxSelected.Item(0, e).Value & "','" & flxSelected.Item(1, e).Value & "','" & trfPCS & "','" & trfCts & "','','','','',0)")

                AdoCN.Execute("UPDATE tblGradingTrf SET Trf = 1 WHERE Department = 'Rounds' AND ParcelNo = '" & flxSelected.Item(0, e).Value & "' AND PktNo = '" & flxSelected.Item(1, e).Value & "'")
            End If
            rsComSql = Nothing
        Next

        trfPCS = 0
        trfCts = 0

        MsgBox("Records Successfully Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub flxAvailable_DoubleClick(sender As Object, e As EventArgs) Handles flxAvailable.DoubleClick
        Dim intRow As Integer

        For intRow = 0 To flxSelected.Rows.Count - 1
            If flxAvailable.Item(0, flxAvailable.CurrentRow.Index).Value = flxSelected.Item(0, intRow).Value And flxAvailable.Item(1, flxAvailable.CurrentRow.Index).Value = flxSelected.Item(1, intRow).Value Then
                MsgBox("Already Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        If Len(flxAvailable.Item(4, flxAvailable.CurrentRow.Index).Value) = 0 Then
            MsgBox("Invalid Act Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If IsNumeric(flxAvailable.Item(4, flxAvailable.CurrentRow.Index).Value) = False Then
            MsgBox("Invalid Act Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If CDbl(flxAvailable.Item(4, flxAvailable.CurrentRow.Index).Value) <= 0 Then
            MsgBox("Invalid Act Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        flxSelected.Rows.Add(flxAvailable.Item(0, flxAvailable.CurrentRow.Index).Value,
                             flxAvailable.Item(1, flxAvailable.CurrentRow.Index).Value,
                             flxAvailable.Item(2, flxAvailable.CurrentRow.Index).Value,
                             flxAvailable.Item(3, flxAvailable.CurrentRow.Index).Value,
                             flxAvailable.Item(4, flxAvailable.CurrentRow.Index).Value)

        flxAvailable.Rows.RemoveAt(flxAvailable.CurrentRow.Index)
        txtPcs.Text = CalTotalPcs(flxSelected)
        txtCts.Text = CalTotalCts(flxSelected)
        txtActCts.Text = CalTotalCtsAct(flxSelected)
    End Sub

    Private Sub flxAvailable_CellClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub
End Class