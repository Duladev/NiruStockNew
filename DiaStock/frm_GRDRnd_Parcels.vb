Imports System.Data.SqlClient

Public Class frm_GRDRnd_Parcels

    '──────────────────────────────────────────────────────────────
    '  FORM LOAD
    '──────────────────────────────────────────────────────────────
    Private Sub frm_Grading_Parcels_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.StartPosition = FormStartPosition.CenterScreen
            SetupGrid()
            Load_Parcels()
        Catch ex As Exception
            MsgBox("Error in Form_Load : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    '──────────────────────────────────────────────────────────────
    '  SETUP GRID
    '──────────────────────────────────────────────────────────────
    Private Sub SetupGrid()
        flxDetails.Columns.Clear()
        flxDetails.AutoGenerateColumns = False
        flxDetails.AllowUserToAddRows = False
        flxDetails.AllowUserToDeleteRows = False
        flxDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        flxDetails.BackgroundColor = System.Drawing.Color.White
        flxDetails.Font = New System.Drawing.Font("Tahoma", 8.25)
        flxDetails.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(224, 224, 224)
        flxDetails.RowTemplate.Height = 22

        ' Checkbox column for "Complete"
        Dim chkCol As New DataGridViewCheckBoxColumn()
        chkCol.HeaderText = "Complete"
        chkCol.Name = "Complete"
        chkCol.Width = 70
        flxDetails.Columns.Add(chkCol)

        ' Text columns
        Dim headers() As String = {"Parcel No", "Date", "Ret Pcs", "Ret Cts"}
        Dim names() As String = {"ParNo", "ParDate", "RetPcs", "RetCts"}
        Dim widths() As Integer = {120, 100, 80, 80}

        For idx As Integer = 0 To headers.Length - 1
            Dim col As New DataGridViewTextBoxColumn()
            col.HeaderText = headers(idx)
            col.Name = names(idx)
            col.Width = widths(idx)
            col.ReadOnly = True
            If idx >= 2 Then
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
            flxDetails.Columns.Add(col)
        Next
    End Sub

    '──────────────────────────────────────────────────────────────
    '  LOAD
    '──────────────────────────────────────────────────────────────
    Private Sub Load_Parcels()
        Try
            flxDetails.Rows.Clear()

            Dim sql As String =
                "SELECT TOP (100) PERCENT p.ParNo, p.SystemDateTime, " &
                "SUM(st.Pcs) AS Pcs, ROUND(SUM(st.Cts), 3) AS Cts " &
                "FROM tblGrading_RndParcel p " &
                "INNER JOIN tblGrading_RndSizingTypes st ON p.ParNo = st.ParNo " &
                "WHERE p.Complete = 0 " &
                "GROUP BY p.ParNo, p.SystemDateTime " &
                "ORDER BY p.ParNo"

            Dim rsComSql As New ADODB.Recordset()
            rsComSql.Open(sql, AdoCN,
                          ADODB.CursorTypeEnum.adOpenKeyset,
                          ADODB.LockTypeEnum.adLockOptimistic)

            Do While Not rsComSql.EOF
                Dim rowIdx As Integer = flxDetails.Rows.Add()
                Dim row As DataGridViewRow = flxDetails.Rows(rowIdx)
                row.Cells("Complete").Value = False
                row.Cells("ParNo").Value = rsComSql.Fields("ParNo").Value.ToString().Trim()
                row.Cells("ParDate").Value = Convert.ToDateTime(rsComSql.Fields("SystemDateTime").Value).ToString("dd/MM/yyyy")
                row.Cells("RetPcs").Value = rsComSql.Fields("Pcs").Value.ToString()
                row.Cells("RetCts").Value = Format(Convert.ToDouble(rsComSql.Fields("Cts").Value), "#0.000")
                rsComSql.MoveNext()
            Loop

            rsComSql.Close()
            rsComSql = Nothing

            txtRecordCount.Text = "Records : " & flxDetails.Rows.Count

        Catch ex As Exception
            MsgBox("Error in Load_Parcels : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    '──────────────────────────────────────────────────────────────
    '  SAVE
    '──────────────────────────────────────────────────────────────
    Private Sub Save()
        Try
            Dim blnSave As Boolean = False
            Dim today As String = Date.Now.ToString("MM/dd/yyyy")

            Dim rsComSql As New ADODB.Recordset()

            For Each row As DataGridViewRow In flxDetails.Rows
                Dim isChecked As Boolean = Convert.ToBoolean(row.Cells("Complete").Value)
                If isChecked Then
                    Dim parNo As String = row.Cells("ParNo").Value?.ToString().Trim()

                    ' UPDATE tblGrading_RndParcel
                    rsComSql.Open(
                        "UPDATE tblGrading_RndParcel " &
                        "SET Complete=1, ExportDate='" & today & "' " &
                        "WHERE ParNo='" & parNo & "'",
                        AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                        ADODB.LockTypeEnum.adLockOptimistic)
                    rsComSql.Close()

                    ' UPDATE tblGrading_RndInvoice
                    rsComSql.Open(
                        "UPDATE tblGrading_RndInvoice " &
                        "SET Export=1 " &
                        "WHERE ParcelNo='" & parNo & "'",
                        AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                        ADODB.LockTypeEnum.adLockOptimistic)
                    rsComSql.Close()

                    blnSave = True
                End If
            Next

            rsComSql = Nothing

            If blnSave Then
                MessageBox.Show("Parcels marked as complete successfully.", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                Load_Parcels()
            Else
                MessageBox.Show("No parcels selected. Please check the Complete checkbox for parcels to save.",
                                Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MsgBox("Error in Save : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    '──────────────────────────────────────────────────────────────
    '  TOOLBAR BUTTONS
    '──────────────────────────────────────────────────────────────
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Load_Parcels()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Save()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    '──────────────────────────────────────────────────────────────
    '  SELECT ALL / DESELECT ALL
    '──────────────────────────────────────────────────────────────
    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        For Each row As DataGridViewRow In flxDetails.Rows
            row.Cells("Complete").Value = True
        Next
    End Sub

    Private Sub btnDeselectAll_Click(sender As Object, e As EventArgs) Handles btnDeselectAll.Click
        For Each row As DataGridViewRow In flxDetails.Rows
            row.Cells("Complete").Value = False
        Next
    End Sub
    Private Sub btnRefresh1_Click(sender As Object, e As EventArgs) Handles btnrefresh1.Click
        Load_Parcels()
    End Sub

    Private Sub btnSave1_Click(sender As Object, e As EventArgs) Handles btnsave1.Click
        Save()
    End Sub

    Private Sub btnExit1_Click(sender As Object, e As EventArgs) Handles btnexit1.Click
        Me.Close()
    End Sub

    '──────────────────────────────────────────────────────────────
    '  STUBS
    '──────────────────────────────────────────────────────────────
    Private Sub txtRecordCount_TextChanged(sender As Object, e As EventArgs) Handles txtRecordCount.TextChanged
        ' reserved
    End Sub

    Private Sub lblTitle_Click(sender As Object, e As EventArgs) Handles lblTitle.Click
        ' reserved
    End Sub

End Class