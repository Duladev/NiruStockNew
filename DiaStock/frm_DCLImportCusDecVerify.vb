
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_DCLImportCusDecVerify

    Private Sub Load_CusDec()
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblImportCusDec WHERE Verify = 0 ORDER BY CusDecDate", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(Format(rsComSql.Fields("CusDecDate").Value, "yyyy/MM/dd"),
                                    rsComSql.Fields("BOIEntryNo").Value,
                                    rsComSql.Fields("InvoiceNo").Value,
                                    rsComSql.Fields("Supplier").Value,
                                    rsComSql.Fields("Country").Value,
                                    rsComSql.Fields("Description").Value,
                                    rsComSql.Fields("Agent").Value,
                                    rsComSql.Fields("HSCode").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    rsComSql.Fields("Cts").Value,
                                    False)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean

        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(11, intRow).Value = True Then
                blnSave = True

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT InvoiceNo FROM tblImportCusDec WHERE InvoiceNo = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    AdoCN.Execute("UPDATE tblImportCusDec SET ActCts = " & CDbl(flxDetails.Item(10, intRow).Value) & ",Verify = 1,DoneBy = '" & PBUser_EmpNo & "' WHERE InvoiceNo = '" & flxDetails.Item(2, intRow).Value & "'")
                End If
                rsComSql = Nothing
            End If
        Next
        If blnSave = True Then
            MsgBox("Cus Dec Verified Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            flxDetails.Rows.Clear()
            Load_CusDec()
        End If
    End Sub

    Private Sub Issue()
        If CheckEmployee(Trim(txtIssEmpNo.Text)) = True Then
            dtpToday = GetToday()

            txtIssEmpNo.Text = UCase(Trim(txtIssEmpNo.Text))

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT InvoiceNo FROM tblImportCusDec WHERE InvoiceNo = '" & txtInvoiceNo.Text & "' AND Verify = 1", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                AdoCN.Execute("UPDATE tblImportCusDec SET IssEmpNo = '" & Trim(txtIssEmpNo.Text) & "',IssDate = '" & Format(dtpToday, "MM/dd/yyyy") & "',IssTime = '" & Format(Date.Now, "HH:mm:ss") & "',IssueBy = '" & PBUser_EmpNo & "',Remarks = '" & txtRemarks.Text & "' WHERE InvoiceNo = '" & txtInvoiceNo.Text & "'")

                MsgBox("Cus Dec Issued Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                flxDetails.Rows.Clear()
                Load_CusDec()
            Else

            End If
            rsComSql = Nothing
        Else
            MsgBox("Invalid Emp No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            txtIssEmpNo.Focus()
            Exit Sub
        End If
        
    End Sub

    Private Sub ClearFields()
        txtBoiEntryNo.Text = ""
        txtImpNo.Text = ""
        txtSupplier.Text = ""
        txtCts.Text = ""
        txtActCts.Text = ""
        txtPdfPath.Text = ""
        txtIssEmpNo.Text = ""
        txtIssEmpNo.ReadOnly = True
        txtRemarks.Text = ""
        txtRemarks.ReadOnly = True
        chkVerify.Checked = False
    End Sub

    Private Sub txtInvoiceNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtInvoiceNo.KeyPress
        If Asc(e.KeyChar) = 13 And Len(txtInvoiceNo.Text) > 0 Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblImportCusDec WHERE InvoiceNo = '" & txtInvoiceNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 1 Then
                txtBoiEntryNo.Text = rsComSql.Fields("BOIEntryNo").Value
                txtImpNo.Text = rsComSql.Fields("ImpNo").Value
                txtSupplier.Text = rsComSql.Fields("Supplier").Value
                txtCts.Text = rsComSql.Fields("Cts").Value
                txtActCts.Text = rsComSql.Fields("ActCts").Value
                If rsComSql.Fields("Verify").Value = 1 Then
                    chkVerify.Checked = True
                Else
                    chkVerify.Checked = False
                End If

                txtPdfPath.Text = ""
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblImportScan WHERE InvoiceNo = '" & txtInvoiceNo.Text & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    txtPdfPath.Text = PBInvoicePath & rsComSql_1.Fields("FileName").Value
                End If
                rsComSql_1 = Nothing

                If Not IsDBNull(rsComSql.Fields("IssEmpNo").Value) Then
                    txtIssEmpNo.Text = rsComSql.Fields("IssEmpNo").Value
                Else
                    txtIssEmpNo.Text = ""
                End If

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM VW_DCLImportsAllInvDistinct WHERE SupplierRefNo = '" & txtInvoiceNo.Text & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    txtIssEmpNo.ReadOnly = False
                    txtRemarks.ReadOnly = False
                Else
                    txtIssEmpNo.ReadOnly = True
                    txtRemarks.ReadOnly = True
                End If
                rsComSql_1 = Nothing

            Else
                ClearFields()

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM VW_DCLImportsAllInvDistinct WHERE SupplierRefNo = '" & txtInvoiceNo.Text & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT * FROM tblImportScan WHERE InvoiceNo = '" & txtInvoiceNo.Text & "'", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        txtPdfPath.Text = PBInvoicePath & rsComSql_2.Fields("FileName").Value
                    End If
                    rsComSql_2 = Nothing
                End If
                rsComSql_1 = Nothing

                txtInvoiceNo.Focus()
            End If
            rsComSql = Nothing
        Else
            ClearFields()
        End If
    End Sub

    Private Sub txtPdfPath_DoubleClick(sender As Object, e As EventArgs) Handles txtPdfPath.DoubleClick
        If Len(txtPdfPath.Text) > 0 Then
            Process.Start(txtPdfPath.Text)
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub frm_DCLImportCusDecVerify_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_CusDec()
    End Sub

    Private Sub cmdIssue_Click(sender As Object, e As EventArgs) Handles cmdIssue.Click
        Issue()
    End Sub

    Private Sub cmdReport_Click(sender As Object, e As EventArgs) Handles cmdReport.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLImportCusDec.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub
End Class