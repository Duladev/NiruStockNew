
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_DCLImportCusDec

    Private Sub Load_Excel()
        On Error GoTo ErrorHandler

        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim intRow As Integer

        If txtFilePath.Text = "" Then
            MsgBox("Please select the Excel File", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If Len(Dir(txtFilePath.Text)) > 0 Then

            xlApp = New Excel.Application
            xlWorkBook = xlApp.Workbooks.Open(txtFilePath.Text)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            flxDetails.Rows.Clear()

            For intRow = 2 To 10000
                If Len(xlWorkSheet.Cells(intRow, 1).Value) = 0 Then Exit For

                flxDetails.Rows.Add(Trim(xlWorkSheet.Cells(intRow, 1).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 2).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 3).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 4).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 5).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 6).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 7).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 8).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 9).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 10).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 11).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 12).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 13).Value))

            Next
            xlWorkSheet = Nothing
            xlWorkBook.Close()
            xlApp.Quit()

            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)

            MsgBox("Cus Dec List Loaded", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        Exit Sub
ErrorHandler:
        MsgBox(Err.Description, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean

        For intRow = 0 To flxDetails.Rows.Count - 1
            blnSave = True

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblImportCusDec WHERE InvoiceNo = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                MsgBox("Already Entered - " & flxDetails.Item(2, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing
        Next

        For intRow = 0 To flxDetails.Rows.Count - 1
            blnSave = True

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblImportCusDec WHERE InvoiceNo = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblImportCusDec(CusDecDate, BOIEntryNo, InvoiceNo, Supplier, Country, Description, Agent, HSCode, Pcs, Cts, Value, ImpNo, Freight) " & _
                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(2, intRow).Value & "','" & flxDetails.Item(3, intRow).Value & "'," & _
                                "'" & flxDetails.Item(4, intRow).Value & "','" & flxDetails.Item(5, intRow).Value & "','" & flxDetails.Item(6, intRow).Value & "','" & flxDetails.Item(7, intRow).Value & "'," & _
                                "'" & flxDetails.Item(8, intRow).Value & "','" & flxDetails.Item(9, intRow).Value & "','" & flxDetails.Item(10, intRow).Value & "','" & flxDetails.Item(11, intRow).Value & "'," & _
                                "'" & flxDetails.Item(12, intRow).Value & "')")
            Else
                'AdoCN.Execute("UPDATE tblImportCusDec SET CusDecDate = '" & flxDetails.Item(0, intRow).Value & "',BOIEntryNo = '" & flxDetails.Item(1, intRow).Value & "'," & _
                '                "Supplier = '" & flxDetails.Item(3, intRow).Value & "',Country = '" & flxDetails.Item(4, intRow).Value & "',Description = '" & flxDetails.Item(5, intRow).Value & "',Agent = '" & flxDetails.Item(6, intRow).Value & "'," & _
                '                "HSCode = '" & flxDetails.Item(7, intRow).Value & "',Pcs = '" & flxDetails.Item(8, intRow).Value & "',Cts = '" & flxDetails.Item(9, intRow).Value & "',Value = '" & flxDetails.Item(10, intRow).Value & "'," & _
                '                "ImpNo = '" & flxDetails.Item(11, intRow).Value & "',Freight = '" & flxDetails.Item(12, intRow).Value & "' " & _
                '              "WHERE InvoiceNo = '" & flxDetails.Item(2, intRow).Value & "'")
            End If
            rsComSql = Nothing
        Next
        If blnSave = True Then
            MsgBox("Cus Dec Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            txtFilePath.Text = ""
            flxDetails.Rows.Clear()
        End If
    End Sub

    Private Sub UpdateCusDec()
        Dim intRow As Integer
        Dim blnSave As Boolean

        For intRow = 0 To flxDetails.Rows.Count - 1
            blnSave = True

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblImportCusDec WHERE InvoiceNo = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("New Record - " & flxDetails.Item(2, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing
        Next

        For intRow = 0 To flxDetails.Rows.Count - 1
            blnSave = True

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblImportCusDec WHERE InvoiceNo = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                'AdoCN.Execute("INSERT INTO tblImportCusDec(CusDecDate, BOIEntryNo, InvoiceNo, Supplier, Country, Description, Agent, HSCode, Pcs, Cts, Value, ImpNo, Freight) " & _
                '              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(2, intRow).Value & "','" & flxDetails.Item(3, intRow).Value & "'," & _
                '                "'" & flxDetails.Item(4, intRow).Value & "','" & flxDetails.Item(5, intRow).Value & "','" & flxDetails.Item(6, intRow).Value & "','" & flxDetails.Item(7, intRow).Value & "'," & _
                '                "'" & flxDetails.Item(8, intRow).Value & "','" & flxDetails.Item(9, intRow).Value & "','" & flxDetails.Item(10, intRow).Value & "','" & flxDetails.Item(11, intRow).Value & "'," & _
                '                "'" & flxDetails.Item(12, intRow).Value & "')")
            Else
                AdoCN.Execute("UPDATE tblImportCusDec SET CusDecDate = '" & flxDetails.Item(0, intRow).Value & "',BOIEntryNo = '" & flxDetails.Item(1, intRow).Value & "'," & _
                                "Supplier = '" & flxDetails.Item(3, intRow).Value & "',Country = '" & flxDetails.Item(4, intRow).Value & "',Description = '" & flxDetails.Item(5, intRow).Value & "',Agent = '" & flxDetails.Item(6, intRow).Value & "'," & _
                                "HSCode = '" & flxDetails.Item(7, intRow).Value & "',Pcs = '" & flxDetails.Item(8, intRow).Value & "',Cts = '" & flxDetails.Item(9, intRow).Value & "',Value = '" & flxDetails.Item(10, intRow).Value & "'," & _
                                "ImpNo = '" & flxDetails.Item(11, intRow).Value & "',Freight = '" & flxDetails.Item(12, intRow).Value & "' " & _
                              "WHERE InvoiceNo = '" & flxDetails.Item(2, intRow).Value & "'")
            End If
            rsComSql = Nothing
        Next
        If blnSave = True Then
            MsgBox("Cus Dec Updated Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            txtFilePath.Text = ""
            flxDetails.Rows.Clear()
        End If
    End Sub

    Private Sub ClearFields()
        txtBoiEntryNo.Text = ""
        txtImpNo.Text = ""
        txtSupplier.Text = ""
        txtCts.Text = ""
        txtPdfPath.Text = ""
        chkVerify.Checked = False
    End Sub

    Private Sub frm_DCLImportCusDec_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
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

    Private Sub cmdOpen_Click(sender As Object, e As EventArgs) Handles cmdOpen.Click
        flxDetails.Rows.Clear()
        OpenFileDialog1.Filter = "All Excel Files|*.xls;*.xlsx"
        OpenFileDialog1.InitialDirectory = "C:\"
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName <> "" Then
            txtFilePath.Text = OpenFileDialog1.FileName
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

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Load_Excel()
    End Sub

    Private Sub Delete()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblImportCusDec WHERE InvoiceNo = '" & txtInvoiceNo.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 1 Then
            If rsComSql.Fields("Verify").Value = 1 Then
                MsgBox("Already Verified", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Else
                AdoCN.Execute("DELETE FROM tblImportCusDec WHERE InvoiceNo = '" & txtInvoiceNo.Text & "' AND Verify = 0")
                MsgBox("Cus Dec Deleted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
            End If
        Else
            MsgBox("Invalid Invoice No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Delete()
    End Sub

    Private Sub cmdUpdate_Click(sender As Object, e As EventArgs) Handles cmdUpdate.Click
        UpdateCusDec()
    End Sub
End Class