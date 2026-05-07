
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO.Path
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_DCLImportScan

    Private Sub cmdOpen_Click(sender As Object, e As EventArgs) Handles cmdOpen.Click
        flxDetails.Rows.Clear()
        OpenFileDialog1.Filter = "All PDF Files|*.pdf;"
        OpenFileDialog1.InitialDirectory = "C:\"
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName <> "" Then
            txtFileName.Text = GetFileName(OpenFileDialog1.FileName)
            txtPdfPath.Text = PBInvoicePath & txtFileName.Text
        End If
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
        If txtInvoiceNo.Text = "" Then MsgBox("Invalid Invoice Number", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtFileName.Text = "" Then MsgBox("Invalid File Name", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtPdfPath.Text = "" Then MsgBox("Invalid PDF Path", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblImportScan WHERE InvoiceNo = '" & txtInvoiceNo.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            AdoCN.Execute("INSERT INTO tblImportScan(InvoiceNo, FileName, InvScan, KPC, Affirmative, Zimbabwe, Russia, SupDec) " & _
                          "VALUES('" & txtInvoiceNo.Text & "','" & txtFileName.Text & "'," & IIf(chkInvoice.Checked = True, 1, 0) & "," & IIf(chkKim.Checked = True, 1, 0) & "," & IIf(chkAffirmative.Checked = True, 1, 0) & "," & _
                            "" & IIf(chkZimbabwe.Checked = True, 1, 0) & "," & IIf(chkRussia.Checked = True, 1, 0) & "," & IIf(chkSupp.Checked = True, 1, 0) & ")")
        Else
            AdoCN.Execute("UPDATE tblImportScan SET FileName = '" & txtFileName.Text & "',InvScan = " & IIf(chkInvoice.Checked = True, 1, 0) & ", KPC = " & IIf(chkKim.Checked = True, 1, 0) & ",Affirmative  = " & IIf(chkAffirmative.Checked = True, 1, 0) & "," & _
                            "Zimbabwe = " & IIf(chkZimbabwe.Checked = True, 1, 0) & ", Russia = " & IIf(chkRussia.Checked = True, 1, 0) & ", SupDec = " & IIf(chkSupp.Checked = True, 1, 0) & " " & _
                          "WHERE InvoiceNo = '" & txtInvoiceNo.Text & "'")
        End If
        rsComSql = Nothing

        MsgBox("Invoice Uploaded Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        ClearFields
        flxDetails.Rows.Clear()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub ClearFields()
        txtFileName.Text = ""
        txtPdfPath.Text = ""
        txtImgPath.Text = ""
        chkInvoice.Checked = False
        chkRussia.Checked = False
        chkKim.Checked = False
        chkSupp.Checked = False
        chkZimbabwe.Checked = False
        chkAffirmative.Checked = False
    End Sub

    Private Sub txtPdfPath_DoubleClick(sender As Object, e As EventArgs) Handles txtPdfPath.DoubleClick
        If Len(txtPdfPath.Text) > 0 Then
            If Len(Dir(txtPdfPath.Text)) > 0 Then
                Process.Start(txtPdfPath.Text)
            End If
        End If
    End Sub

    Private Sub frm_DCLImportScan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub txtInvoiceNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtInvoiceNo.KeyPress
        If Asc(e.KeyChar) = 13 And Len(txtInvoiceNo.Text) > 0 Then
            flxDetails.Rows.Clear()
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM VW_DCLImportsAllInvDistinct WHERE SupplierRefNo = '" & txtInvoiceNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 1 Then

                txtPdfPath.Text = ""
                txtImgPath.Text = ""
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblImportScan WHERE InvoiceNo = '" & txtInvoiceNo.Text & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    txtPdfPath.Text = PBInvoicePath & rsComSql_1.Fields("FileName").Value
                    txtImgPath.Text = PBImagePath & rsComSql_1.Fields("FileName").Value
                    chkInvoice.Checked = rsComSql_1.Fields("InvScan").Value
                    chkRussia.Checked = rsComSql_1.Fields("Russia").Value
                    chkKim.Checked = rsComSql_1.Fields("KPC").Value
                    chkSupp.Checked = rsComSql_1.Fields("SupDec").Value
                    chkAffirmative.Checked = rsComSql_1.Fields("Affirmative").Value
                    chkZimbabwe.Checked = rsComSql_1.Fields("Zimbabwe").Value
                    txtFileName.Text = rsComSql_1.Fields("FileName").Value
                End If
                rsComSql_1 = Nothing

                flxDetails.Rows.Add(txtInvoiceNo.Text,
                                    txtPdfPath.Text,
                                    txtImgPath.Text)
            Else
                ClearFields()

                txtInvoiceNo.Text = ""
                txtInvoiceNo.Focus()
            End If
            rsComSql = Nothing
        Else
            ClearFields()
        End If
    End Sub

    Private Sub txtImgPath_DoubleClick(sender As Object, e As EventArgs) Handles txtImgPath.DoubleClick
        If Len(txtImgPath.Text) > 0 Then
            If Len(Dir(txtImgPath.Text)) > 0 Then
                Process.Start(txtImgPath.Text)
            End If
        End If
    End Sub

    Private Sub Show_All()
        flxDetails.Rows.Clear()
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblImportScan ORDER BY InvoiceNo", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                flxDetails.Rows.Add(rsComSql_1.Fields("InvoiceNo").Value,
                                    rsComSql_1.Fields("FileName").Value,
                                    PBInvoicePath & rsComSql_1.Fields("FileName").Value)

                rsComSql_1.MoveNext()
            End While
        End If
    End Sub
End Class