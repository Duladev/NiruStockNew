
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLWeightLoss

    Private Sub txtSupParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSupParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            GetParcelDetails()
        End If
    End Sub

    Private Sub GetParcelDetails()
        Dim strDepartment As String

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        'rsComSql.Open("SELECT * FROM tblParcel WHERE OrigParcelNo = '" & txtSupParNo.Text & "' AND Grp <> 'N' AND ReIssue = 0 AND Depart NOT LIKE 'Rough%' ORDER BY Depart, GrpParNo", AdoCN, 1, 1)
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblDepartment.Department1, dbo.tblParcel.OrigParcelNo, MIN(dbo.tblParcel.ParcelNo) AS ParcelNo, SUM(dbo.tblParcel.IssuedCts) AS IssuedCts, SUM(dbo.tblParcel.RghCts) AS RghCts " & _
                      "FROM dbo.tblParcel INNER JOIN dbo.tblDepartment ON dbo.tblParcel.Depart = dbo.tblDepartment.Department " & _
                      "WHERE (dbo.tblParcel.Grp <> 'N') AND (dbo.tblParcel.ReIssue = 0) AND (dbo.tblDepartment.Local = 1) AND (dbo.tblParcel.Bruting = 0) " & _
                      "GROUP BY dbo.tblDepartment.Department1, dbo.tblParcel.OrigParcelNo " & _
                      "HAVING (dbo.tblParcel.OrigParcelNo = '" & txtSupParNo.Text & "') " & _
                      "ORDER BY dbo.tblDepartment.Department1", AdoCN, 1, 1)
        If rsComSql.RecordCount > 0 Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                strDepartment = rsComSql.Fields("Department1").Value

                flxDetails.Rows.Add(strDepartment,
                                    rsComSql.Fields("ParcelNo").Value,
                                    Format(Math.Round(rsComSql.Fields("IssuedCts").Value, 3), "#0.000"),
                                    Format(Math.Round(rsComSql.Fields("RghCts").Value, 3), "#0.000"),
                                    "0")



                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtIssCts.Text = CalTotalCts(flxDetails)
        txtRghCts.Text = CalTotalRghCts(flxDetails)

        Calculate_Perc()

        txtWtLoss.Focus()
    End Sub

    Private Sub Calculate_Perc()
        Dim intRow As Integer
        Dim dblPerc As Double

        For intRow = 0 To flxDetails.Rows.Count - 1
            dblPerc = (CDbl(flxDetails.Item(2, intRow).Value) / CDbl(txtIssCts.Text)) * 100
            flxDetails.Item(4, intRow).Value = Math.Round(dblPerc, 2)

            Select Case flxDetails.Item(0, intRow).Value
                Case "Baguettes"
                    mStrSQL = "SELECT ParcelNo FROM VW_PFGetFinishedBaguettes WHERE SuppParNo = '" & txtSupParNo.Text & "' ORDER BY ParcelNo"
                Case "Princess"
                    mStrSQL = "SELECT ParNo AS ParcelNo FROM VW_PFGetFinishedPrincess WHERE OrigParcelNo = '" & txtSupParNo.Text & "' ORDER BY ParcelNo"
                Case "Rounds"
                    mStrSQL = "SELECT OrderNo AS ParcelNo FROM VW_PFGetFinishedRoundsB WHERE SupParNo = '" & txtSupParNo.Text & "' AND Bruting = 0 ORDER BY ParcelNo"
                Case "Emerald", "Davinci", "Lamour", "Opening", "Carrer", "Asscher", "Radiant"
                    mStrSQL = "SELECT ParNo AS ParcelNo FROM VW_PFGetFinishedRoundsExt WHERE Department = '" & flxDetails.Item(0, intRow).Value & "' AND OrgParNo = '" & txtSupParNo.Text & "' ORDER BY ParcelNo"
                Case Else
                    MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
            End Select
            rsComSql = New ADODB.Recordset
            rsComSql.Open(mStrSQL, AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                flxDetails.Item(6, intRow).Value = rsComSql.Fields("ParcelNo").Value
            End If
            rsComSql = Nothing
        Next
    End Sub

    Private Sub DistributeWtLoss()
        Dim intRow As Integer
        Dim dblWtLoss As Double

        For intRow = 0 To flxDetails.Rows.Count - 1
            dblWtLoss = (CDbl(flxDetails.Item(4, intRow).Value) * CDbl(txtWtLoss.Text)) / 100
            flxDetails.Item(5, intRow).Value = Math.Round(dblWtLoss, 3)
        Next
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub ClearFields()
        txtSupParNo.Text = ""
        txtIssCts.Text = ""
        txtRghCts.Text = ""
        flxDetails.Rows.Clear()
    End Sub

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(2, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Function CalTotalRghCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalRghCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalRghCts = CalTotalRghCts + Val(flxSample.Item(3, intRow).Value)
        Next
        CalTotalRghCts = Math.Round(CalTotalRghCts, 3)
    End Function

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub txtWtLoss_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWtLoss.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtWtLoss.Text)
        If Asc(e.KeyChar) = 13 And txtWtLoss.Text <> "" Then
            DistributeWtLoss()
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        'Save()
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim dblRghCts As Double

        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(6, intRow).Value <> "" Then
                dblRghCts = Math.Round(CDbl(flxDetails.Item(3, intRow).Value) + CDbl(flxDetails.Item(5, intRow).Value), 3)
                AdoCN.Execute("UPDATE tblParcel SET RghCts = " & dblRghCts & " WHERE GrpParNo = '" & flxDetails.Item(6, intRow).Value & "' AND Depart = '" & flxDetails.Item(0, intRow).Value & "'")
            End If
        Next

        MsgBox("Successfully Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        ClearFields()
    End Sub

    Private Sub frm_DCLWeightLoss_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub
End Class