Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDRnd_Bundle

    ' ── FORM LOAD 
    Private Sub frm_Grading_Bundle_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If GetUserRights(Me.Name) = False Then
        'MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
        'Me.Close()
        'Exit Sub
        'End If

        Me.StartPosition = FormStartPosition.CenterScreen
        SetupGrid()
        GetBundleNo()
    End Sub

    '  SETUP GRID 
    Private Sub SetupGrid()
        flxDetails.Columns.Clear()
        flxDetails.AutoGenerateColumns = False
        flxDetails.AllowUserToAddRows = False
        flxDetails.AllowUserToDeleteRows = False
        flxDetails.ReadOnly = True
        flxDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        flxDetails.BackgroundColor = System.Drawing.Color.White
        flxDetails.Font = New System.Drawing.Font("MS Sans Serif", 8.25)
        flxDetails.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(224, 224, 224)

        Dim col As New DataGridViewTextBoxColumn()
        col.HeaderText = "Packing List No."
        col.Name = "PackNo"
        col.Width = 300
        flxDetails.Columns.Add(col)
    End Sub

    '  GET NEXT BUNDLE NUMBER 
    Private Sub GetBundleNo()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(BundleNo) AS MaxNo FROM tblGrading_RndBundle", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If IsDBNull(rsComSql.Fields("MaxNo").Value) Then
                txtBundleNo.Text = "1"
            Else
                txtBundleNo.Text = CStr(CInt(rsComSql.Fields("MaxNo").Value) + 1)
            End If
        Else
            txtBundleNo.Text = "1"
        End If
        rsComSql = Nothing
    End Sub

    '  PACKING LIST NO KEY PRESS 
    Private Sub txtPackNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPackNo.KeyPress
        If NumericOnly(Asc(e.KeyChar), txtPackNo.Text) Then e.Handled = True
        If Asc(e.KeyChar) = 13 Then
            e.Handled = True
            cmdAdd.Focus()
        End If
    End Sub

    '  BUNDLE NO KEY PRESS 
    Private Sub txtBNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBNo.KeyPress
        If NumericOnly(Asc(e.KeyChar), txtBNo.Text) Then e.Handled = True
        If Asc(e.KeyChar) = 13 Then
            e.Handled = True
            cmdAdd.Focus()
        End If
    End Sub

    '  ADD BUTTON 
    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        If Trim(txtPackNo.Text) = "" Then Exit Sub

        ' Check if already bundled in DB
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT BundleNo FROM tblGrading_RndBundle WHERE PackNo = " & CDbl(txtPackNo.Text), AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            Dim existingBundle As String = CStr(rsComSql.Fields("BundleNo").Value)
            rsComSql = Nothing
            MsgBox("Already Bundled - " & existingBundle, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        ' Check if already in current list
        For Each row As DataGridViewRow In flxDetails.Rows
            If row.Cells("PackNo").Value?.ToString() = Trim(txtPackNo.Text) Then
                MsgBox("Already Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        flxDetails.Rows.Add(Trim(txtPackNo.Text))
        txtPackNo.Text = ""
        txtPackNo.Focus()
    End Sub

    '  ROW CLICK — REMOVE ITEM 
    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        If e.RowIndex < 0 Then Exit Sub

        PBResponse = MsgBox("Are you sure to remove this entry?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxDetails.Rows.RemoveAt(e.RowIndex)
        End If
    End Sub

    '  SAVE BUTTON 
    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If flxDetails.Rows.Count = 0 Then
            MsgBox("No packing list items added.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.No Then Exit Sub

        For Each row As DataGridViewRow In flxDetails.Rows
            mStrSQL = "INSERT INTO tblGrading_RndBundle(BundleNo, PackNo) VALUES(" &
                      CDbl(txtBundleNo.Text) & ", " & CDbl(row.Cells("PackNo").Value?.ToString()) & ")"
            AdoCN.Execute(mStrSQL)
        Next

        MsgBox("Bundle saved successfully.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        ResetForm()
    End Sub

    '  REFRESH BUTTON 
    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        ResetForm()
    End Sub

    '  CLEAR BUTTON — DELETE BUNDLE FROM DB 
    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If Trim(txtBNo.Text) <> "" Then
                AdoCN.Execute("DELETE FROM tblGrading_RndBundle WHERE BundleNo = " & CDbl(txtBNo.Text))
            End If
            ResetForm()
        End If
    End Sub

    ' ─ EXIT 
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    ' ── RESET FORM 
    Private Sub ResetForm()
        GetBundleNo()
        txtPackNo.Text = ""
        txtBNo.Text = ""
        flxDetails.Rows.Clear()
    End Sub

End Class