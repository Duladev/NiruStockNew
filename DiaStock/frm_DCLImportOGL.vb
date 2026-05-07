
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_DCLImportOGL

    Private Sub cmdOpen_Click(sender As Object, e As EventArgs) Handles cmdOpen.Click
        flxDetails.Rows.Clear()
        OpenFileDialog1.Filter = "All Excel Files|*.xls;*.xlsx"
        OpenFileDialog1.InitialDirectory = "C:\"
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName <> "" Then
            txtFilePath.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Load_Excel()
        On Error GoTo ErrorHandler

        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim intRow As Integer
        Dim intOGL As Integer
        Dim strOGL As String

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

                intOGL = 0
                strOGL = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblOGL WHERE RghInv = '" & Trim(xlWorkSheet.Cells(intRow, 7).Value) & "' AND VidPar = '" & Trim(xlWorkSheet.Cells(intRow, 8).Value) & "' AND " & _
                                "Dec = '" & Trim(xlWorkSheet.Cells(intRow, 9).Value) & "' AND OrgMen = '" & Trim(xlWorkSheet.Cells(intRow, 10).Value) & "' AND GemCon = '" & Trim(xlWorkSheet.Cells(intRow, 11).Value) & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    intOGL = rsComSql.Fields("OGL").Value
                    strOGL = rsComSql.Fields("OGLName").Value
                End If
                rsComSql = Nothing

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
                                    strOGL,
                                    Trim(xlWorkSheet.Cells(intRow, 13).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 14).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 15).Value))

            Next
            xlWorkSheet = Nothing
            xlWorkBook.Close()
            xlApp.Quit()

            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)

            MsgBox("OGL List Loaded", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
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

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Load_Excel()
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean

        For intRow = 0 To flxDetails.Rows.Count - 1
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblDCLMiningCountry WHERE MiningCountry = '" & flxDetails.Item(4, intRow).Value & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Mining Country - " & flxDetails.Item(4, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing
        Next

        For intRow = 0 To flxDetails.Rows.Count - 1
            blnSave = True

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblImportOGL WHERE MasterLotID = '" & CDbl(flxDetails.Item(0, intRow).Value) & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblImportOGL(MasterLotID,KPCNo,MaterialProcument,MiningCompany,MiningCountry,Cts,Document1,VideoParcel,Declaration,OriginMentioned,GemControl,OGL,CSR,SupplierCode,MineName) " & _
                              "VALUES('" & CDbl(flxDetails.Item(0, intRow).Value) & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(2, intRow).Value & "','" & flxDetails.Item(3, intRow).Value & "'," & _
                                "'" & flxDetails.Item(4, intRow).Value & "','" & flxDetails.Item(5, intRow).Value & "','" & flxDetails.Item(6, intRow).Value & "','" & flxDetails.Item(7, intRow).Value & "'," & _
                                "'" & flxDetails.Item(8, intRow).Value & "','" & flxDetails.Item(9, intRow).Value & "','" & flxDetails.Item(10, intRow).Value & "','" & flxDetails.Item(11, intRow).Value & "'," & _
                                "'" & flxDetails.Item(12, intRow).Value & "','" & flxDetails.Item(13, intRow).Value & "','" & flxDetails.Item(14, intRow).Value & "')")
            Else
                AdoCN.Execute("UPDATE tblImportOGL SET KPCNo = '" & flxDetails.Item(1, intRow).Value & "',MaterialProcument = '" & flxDetails.Item(2, intRow).Value & "',MiningCompany = '" & flxDetails.Item(3, intRow).Value & "'," & _
                                "MiningCountry = '" & flxDetails.Item(4, intRow).Value & "',Cts = '" & flxDetails.Item(5, intRow).Value & "',Document1 = '" & flxDetails.Item(6, intRow).Value & "',VideoParcel = '" & flxDetails.Item(7, intRow).Value & "'," & _
                                "Declaration = '" & flxDetails.Item(8, intRow).Value & "',OriginMentioned = '" & flxDetails.Item(9, intRow).Value & "',GemControl = '" & flxDetails.Item(10, intRow).Value & "',OGL = '" & flxDetails.Item(11, intRow).Value & "'," & _
                                "CSR = '" & flxDetails.Item(12, intRow).Value & "',SupplierCode = '" & flxDetails.Item(13, intRow).Value & "',MineName = '" & flxDetails.Item(14, intRow).Value & "' " & _
                              "WHERE MasterLotID = '" & CDbl(flxDetails.Item(0, intRow).Value) & "'")
            End If
            rsComSql = Nothing
        Next
        If blnSave = True Then
            MsgBox("OGL Uploaded Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            txtFilePath.Text = ""
            flxDetails.Rows.Clear()
        End If
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

    Private Sub frm_DCLImportOGL_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub ClearFields()
        txtMaterialProcument.Text = ""
        txtMiningCompany.Text = ""
        txtMiningCountry.Text = ""
        txtCts.Text = ""
        txtMineName.Text = ""
    End Sub

    Private Sub txtMasterLot_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMasterLot.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 And Len(txtMasterLot.Text) > 0 Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblImportOGL WHERE MasterLotID = '" & txtMasterLot.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 1 Then
                txtMaterialProcument.Text = rsComSql.Fields("MaterialProcument").Value
                txtMiningCompany.Text = rsComSql.Fields("MiningCompany").Value
                txtMiningCountry.Text = rsComSql.Fields("MiningCountry").Value
                txtCts.Text = rsComSql.Fields("Cts").Value
                txtMineName.Text = rsComSql.Fields("MineName").Value
            Else
                ClearFields()
                txtMasterLot.Text = ""
                txtMasterLot.Focus()
            End If
            rsComSql = Nothing
        Else
            ClearFields()
        End If
    End Sub
End Class