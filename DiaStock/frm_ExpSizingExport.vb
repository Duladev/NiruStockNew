
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_ExpSizingExport

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_Exports()
        Dim dblRecord As Double

        Dim strSupplier As String
        Dim strType As String
        Dim strCompCode As String

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT Department, ParNo, OrgAssort, SupParNo, SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts " & _
                      "FROM dbo.tblExpReExports " & _
                      "WHERE (OK = 2) " & _
                      "GROUP BY Department, ParNo, OrgAssort, SupParNo " & _
                      "ORDER BY Department, ParNo, OrgAssort", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            ExpProgress.Value = 0
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = rsComSql.RecordCount
            dblRecord = 0

            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                strSupplier = ""
                strType = ""
                strCompCode = ""
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT dbo.tblImport.SupParcelNo, dbo.tblSuppliers.CompanyName, dbo.tblImport.Category, dbo.tblImport.CompCode " & _
                                "FROM dbo.tblImport INNER JOIN dbo.tblSuppliers ON dbo.tblImport.SupplierCode = dbo.tblSuppliers.SupplierCode " & _
                                "WHERE (dbo.tblImport.SupParcelNo = '" & rsComSql.Fields("SupParNo").Value & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strSupplier = rsComSql_1.Fields("CompanyName").Value
                    strType = rsComSql_1.Fields("Category").Value
                    strCompCode = rsComSql_1.Fields("CompCode").Value
                End If
                rsComSql_1 = Nothing

                flxDetails.Rows.Add(rsComSql.Fields("Department").Value,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("OrgAssort").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    Format(rsComSql.Fields("Cts").Value, "#0.000"),
                                    rsComSql.Fields("SupParNo").Value,
                                    strSupplier,
                                    strType,
                                    False,
                                    strCompCode)

                rsComSql.MoveNext()
                dblRecord = dblRecord + 1
                ExpProgress.Value = dblRecord
                Application.DoEvents()
            End While
        Else
            MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing

        ExpProgress.Visible = False

    End Sub

    Private Sub GetPackNo()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(PackNo) AS MaxNo FROM tblExpReExports", AdoCN, 1, 1)
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

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        Load_Exports()
        GetPackNo()
        txtTotalPcs.Text = ""
        txtTotalCts.Text = ""
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(8, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(8, intRow).Value = False
            Next
        End If
        txtTotalPcs.Text = CalTotalPcs()
        txtTotalCts.Text = CalTotalCts()
    End Sub

    Private Sub Verify()
        Dim intRow As Integer
        Dim blnSave As Boolean

        blnSave = False
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(8, intRow).Value = True Then
                    blnSave = True

                    AdoCN.Execute("UPDATE tblExpReExports SET OK = 3, PackNo = " & CDbl(txtPackNo.Text) & " " & _
                                  "WHERE Department = '" & flxDetails.Item(0, intRow).Value & "' AND ParNo = '" & flxDetails.Item(1, intRow).Value & "' AND (OK = 2)")
                End If
            Next
            If blnSave = True Then
                MsgBox("Updated Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Else
                MsgBox("No Records to Add", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            Load_Exports()
            GetPackNo()
        End If
    End Sub

    Private Sub cmdTrf_Click(sender As Object, e As EventArgs) Handles cmdTrf.Click
        Verify()
    End Sub

    Private Function CalTotalPcs() As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(8, intRow).Value = True Then
                CalTotalPcs = CalTotalPcs + CInt(flxDetails.Item(3, intRow).Value)
            End If
        Next
        Return CalTotalPcs
    End Function

    Private Function CalTotalCts() As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(8, intRow).Value = True Then
                CalTotalCts = CalTotalCts + CDbl(flxDetails.Item(4, intRow).Value)
            End If
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
        Return CalTotalCts
    End Function

    Private Sub frm_ExpSizingExport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Company()
    End Sub

    Private Sub Load_Company()
        cmbCompany.Items.Clear()
        rsComSql = New ADODB.Recordset
        mStrSQL = "SELECT CompCode FROM tblCompany ORDER BY CompCode"
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        Do While Not rsComSql.EOF
            cmbCompany.Items.Add(rsComSql.Fields("CompCode").Value)
            rsComSql.MoveNext()
        Loop
        rsComSql = Nothing
    End Sub
End Class