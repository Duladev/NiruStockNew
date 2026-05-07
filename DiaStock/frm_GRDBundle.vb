
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDBundle

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub GetPackNo()
        rsComSql = New ADODB.Recordset
        If optReject.Checked = True Then
            rsComSql.Open("SELECT MAX(BundleNo) AS MaxNo FROM tblGrading_Pack WHERE Type = 'C'", AdoCN, 1, 1)
        Else
            rsComSql.Open("SELECT MAX(BundleNo) AS MaxNo FROM tblGrading_Pack WHERE Type = 'P'", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            If IsDBNull(rsComSql.Fields("MaxNo").Value) Then
                txtPackNo.Text = "1"
            Else
                txtPackNo.Text = rsComSql.Fields("MaxNo").Value + 1
            End If
        Else
            txtPackNo.Text = "1"
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Parcels()
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        If optReject.Checked = True Then
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblGrading_Pack.PackingListNo, SUM(dbo.tblExpReExports.Pcs) AS Pcs, ROUND(SUM(dbo.tblExpReExports.Cts), 3) AS Cts " & _
                          "FROM dbo.tblGrading_Pack INNER JOIN dbo.tblExpReExports ON dbo.tblGrading_Pack.Department = dbo.tblExpReExports.Department AND dbo.tblGrading_Pack.ParNo = dbo.tblExpReExports.ParNo AND " & _
                            "dbo.tblGrading_Pack.PackNo = dbo.tblExpReExports.PackNo " & _
                          "WHERE (dbo.tblGrading_Pack.BundleNo = 0) AND (dbo.tblGrading_Pack.Type = 'C') " & _
                          "GROUP BY dbo.tblGrading_Pack.PackingListNo " & _
                          "ORDER BY dbo.tblGrading_Pack.PackingListNo", AdoCN, 1, 1)
        Else
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.VW_GradingBundle.PackingListNo, SUM(dbo.tblGrading_PackingListM.Pcs) AS Pcs, ROUND(SUM(dbo.tblGrading_PackingListM.Cts), 3) AS Cts " & _
                          "FROM dbo.tblGrading_PackingListM INNER JOIN dbo.VW_GradingBundle ON dbo.tblGrading_PackingListM.PackNo = dbo.VW_GradingBundle.PackingListNo " & _
                          "GROUP BY dbo.VW_GradingBundle.PackingListNo " & _
                          "ORDER BY dbo.VW_GradingBundle.PackingListNo", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("PackingListNo").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    Format(rsComSql.Fields("Cts").Value, "#0.000"), False)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub frm_GRDBundle_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        optReject.Checked = True
        GetPackNo()
        Load_Parcels()
    End Sub

    Private Sub txtBNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean

        blnSave = False
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(3, intRow).Value = True Then
                    blnSave = True

                    If optReject.Checked = True Then
                        AdoCN.Execute("UPDATE tblGrading_Pack SET BundleNo = " & CDbl(txtPackNo.Text) & " " & _
                                      "WHERE PackingListNo = '" & CDbl(flxDetails.Item(0, intRow).Value) & "' AND Type = 'C'")
                    Else
                        AdoCN.Execute("UPDATE tblGrading_Pack SET BundleNo = " & CDbl(txtPackNo.Text) & " " & _
                                      "WHERE PackingListNo = '" & CDbl(flxDetails.Item(0, intRow).Value) & "' AND Type = 'P'")
                    End If
                End If
            Next
            If blnSave = True Then
                MsgBox("Updated Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Else
                MsgBox("No Records to Add", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            Load_Parcels()
            GetPackNo()
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        PBResponse = MsgBox("Are you sure to Clear?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If optReject.Checked = True Then
                AdoCN.Execute("UPDATE tblGrading_Pack SET BundleNo = 0 " & _
                              "WHERE BundleNo = " & CDbl(txtBNo.Text) & " AND Type = 'C'")
            End If
            MsgBox("Cleared Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Load_Parcels()
            GetPackNo()
            txtBNo.Text = ""
        End If
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        GetPackNo()
        Load_Parcels()
    End Sub
End Class