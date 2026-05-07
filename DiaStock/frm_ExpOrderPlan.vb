
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_ExpOrderPlan
    Dim strFolderPath As String

    Private Sub Load_Orders()
        flxOrder.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblMixOrderPlan ORDER BY ParcelNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxOrder.Rows.Add(rsComSql.Fields("ParcelNo").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_OrdersTrf()
        flxOrder2.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblExpTrfPlan ORDER BY ParcelNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxOrder2.Rows.Add(rsComSql.Fields("ParcelNo").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_OrdersSup()
        flxOrder3.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblMixOrderPlanSup ORDER BY ParcelNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxOrder3.Rows.Add(rsComSql.Fields("ParcelNo").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub frm_ExpOrderPlan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        If strDBName = "DiaStock" Then
            strFolderPath = "Export\"
        Else
            strFolderPath = "DiaSalesExport\"
        End If

        Load_Orders()
        Load_OrdersSup()
        Load_OrdersTrf()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Save_Orders()
        If Len(Trim(txtParcel.Text)) = 0 Then Exit Sub
        For intRow = 0 To flxOrder.Rows.Count - 1
            If txtParcel.Text = flxOrder.Item(0, intRow).Value Then
                MsgBox("Already in the list", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        AdoCN.Execute("INSERT INTO tblMixOrderPlan(ParcelNo) VALUES('" & UCase(txtParcel.Text) & "')")
        Load_Orders()
    End Sub

    Private Sub Save_OrdersTrf()
        If Len(Trim(txtParcel.Text)) = 0 Then Exit Sub
        For intRow = 0 To flxOrder2.Rows.Count - 1
            If txtParcel.Text = flxOrder2.Item(0, intRow).Value Then
                MsgBox("Already in the list", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        AdoCN.Execute("INSERT INTO tblExpTrfPlan(ParcelNo) VALUES('" & UCase(txtParcel.Text) & "')")
        Load_OrdersTrf()
    End Sub

    Private Sub Save_OrdersSup()
        If Len(Trim(txtParcel.Text)) = 0 Then Exit Sub
        For intRow = 0 To flxOrder3.Rows.Count - 1
            If txtParcel.Text = flxOrder3.Item(0, intRow).Value Then
                MsgBox("Already in the list", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        AdoCN.Execute("INSERT INTO tblMixOrderPlanSup(ParcelNo) VALUES('" & UCase(txtParcel.Text) & "')")
        Load_OrdersSup()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save_Orders()
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        Save_OrdersTrf()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        Save_OrdersSup()
    End Sub

    Private Sub flxOrder_DoubleClick(sender As Object, e As EventArgs) Handles flxOrder.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            AdoCN.Execute("DELETE FROM tblMixOrderPlan WHERE ParcelNo = '" & flxOrder.Item(0, flxOrder.CurrentRow.Index).Value & "'")
            flxOrder.Rows.RemoveAt(flxOrder.CurrentRow.Index)
        End If
    End Sub

    Private Sub flxOrder2_DoubleClick(sender As Object, e As EventArgs) Handles flxOrder2.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            AdoCN.Execute("DELETE FROM tblExpTrfPlan WHERE ParcelNo = '" & flxOrder2.Item(0, flxOrder2.CurrentRow.Index).Value & "'")
            flxOrder2.Rows.RemoveAt(flxOrder2.CurrentRow.Index)
        End If
    End Sub

    Private Sub flxOrder3_DoubleClick(sender As Object, e As EventArgs) Handles flxOrder3.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            AdoCN.Execute("DELETE FROM tblMixOrderPlanSup WHERE ParcelNo = '" & flxOrder3.Item(0, flxOrder3.CurrentRow.Index).Value & "'")
            flxOrder3.Rows.RemoveAt(flxOrder3.CurrentRow.Index)
        End If
    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpOrderPlanMFG.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button4_Click(sender As Object, e As EventArgs) Handles HazelDev_Button4.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpOrderPlan.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button6_Click(sender As Object, e As EventArgs) Handles HazelDev_Button6.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpTrfPlan.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button5_Click(sender As Object, e As EventArgs) Handles HazelDev_Button5.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpTrfPlanRange.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button7_Click(sender As Object, e As EventArgs) Handles HazelDev_Button7.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExpRghExportDetails.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub
End Class