
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_DCLImport

    Private Sub GetNewImportNo()
        Dim dblImportNo As Double

        dblImportNo = 1
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(ImportNo) AS MaxNo FROM tblImport", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("MaxNo").Value) Then
                dblImportNo = rsComSql.Fields("MaxNo").Value + 1
            End If
        End If
        rsComSql = Nothing
        txtImportNo.Text = dblImportNo

    End Sub

    Private Sub GetSystemRefNo()
        Dim dblSysRefNo As Double

        dblSysRefNo = 1
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(SystemRefNo) AS MaxNo FROM tblImport", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("MaxNo").Value) Then
                dblSysRefNo = rsComSql.Fields("MaxNo").Value + 1
            End If
        End If
        rsComSql = Nothing
        txtSysRefNo.Text = dblSysRefNo

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

    Private Sub Load_Supplier()
        cmbSupplier.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblSuppliers ORDER BY SupplierCode", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbSupplier.Items.Add(Format(rsComSql.Fields("SupplierCode").Value, "00000"))

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub frm_DCLImport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Company()
        Load_Supplier()
        dtpInvDate.Value = Date.Now
        dtpRecDate.Value = Date.Now

        If dbConnDiaStock.State = 1 Then
            dbConnDiaStock.Close()
        End If
        dbConnDiaStock.ConnectionString = "Provider=SQLOLEDB;Data Source='" & strServerName & "';Connect Timeout=60;Initial Catalog=DiaStock;Integrated Security=SSPI"
        dbConnDiaStock.Open()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ClearFields()
        GetNewImportNo()
        GetSystemRefNo()
        txtDclRefNo.Text = ""
        txtBoiNo.Text = ""
        cmbCategory.Text = ""
        cmbCompany.Text = ""
        cmbType.Text = ""
        cmbSupplier.Text = ""
        txtSupplier.Text = ""
        dtpInvDate.Value = Date.Now
        dtpRecDate.Value = Date.Now
        flxDept.Rows.Clear()
        txtFilePath.Text = ""
        txtConRefNo.Text = ""
        cmbSawn.Text = ""
        cmbPriceType.Text = ""
        chkOriginal.Checked = False
        chkInternal.Checked = False
        txtExpNo.Text = ""
        txtSupRefNo.Focus()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
        txtSupRefNo.Text = ""
    End Sub

    Private Function ValidateFields() As Boolean
        ValidateFields = True

        If Not Len(Trim(txtImportNo.Text)) > 0 Then
            MsgBox("Please enter New", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(txtSysRefNo.Text)) > 0 Then
            MsgBox("Please enter the System Ref No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(txtSupRefNo.Text)) > 0 Then
            MsgBox("Please enter the Supplier Ref No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(txtBoiNo.Text)) > 0 Then
            MsgBox("Please enter the B.O.I. No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(cmbType.Text)) > 0 Then
            MsgBox("Please enter the Parcel Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(cmbSupplier.Text)) > 0 Then
            MsgBox("Please enter the Supplier", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(cmbCompany.Text)) > 0 Then
            MsgBox("Please enter the Company", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(cmbCategory.Text)) > 0 Then
            MsgBox("Please enter the Category", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(cmbPriceType.Text)) > 0 Then
            MsgBox("Please enter the Price Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If chkCusDec.Checked = False Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblImportCusDec WHERE InvoiceNo = '" & txtSupRefNo.Text & "' AND Verify = 1", dbConn, 1, 1)
            If rsComSql.RecordCount = 0 Then
                If Len(txtConRefNo.Text) > 0 Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblImportCusDec WHERE InvoiceNo = '" & txtConRefNo.Text & "' AND Verify = 1", dbConn, 1, 1)
                    If rsComSql_1.RecordCount = 0 Then
                        MsgBox("Cus Dec not Entered/Verified", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        ValidateFields = False
                        Exit Function
                    End If
                    rsComSql_1 = Nothing
                Else
                    MsgBox("Cus Dec not Entered/Verified", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    ValidateFields = False
                    Exit Function
                End If
            End If
        End If

        For I = 0 To flxDept.Rows.Count - 1
            If Len(Trim(flxDept.Item(22, I).Value)) = 0 Then
                MsgBox("Invalid Origin", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ValidateFields = False
                Exit Function
            End If
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblDCLOrigin WHERE Origin = '" & Trim(flxDept.Item(22, I).Value) & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Origin", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ValidateFields = False
                Exit Function
            End If
            rsComSql = Nothing
            If Len(Trim(flxDept.Item(23, I).Value)) = 0 Then
                MsgBox("Invalid Import Price", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ValidateFields = False
                Exit Function
            End If
            If Len(Trim(flxDept.Item(31, I).Value)) = 0 Then
                MsgBox("Invalid Diamond Cost", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ValidateFields = False
                Exit Function
            End If
        Next

        Return ValidateFields
    End Function

    Private Sub Save()
        On Error GoTo ErrorHandler
        Dim I As Integer

        Dim strInvoiceNo As String
        Dim strBoiNo As String
        Dim intSupCode As Integer
        Dim dtpInvDate2 As Date

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If ValidateFields() = False Then Exit Sub

            If flxDept.RowCount = 0 Then
                MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblImport WHERE ImportNo = " & CDbl(txtImportNo.Text) & "", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                MsgBox("Import No. already exists. Please Refresh", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            For I = 0 To flxDept.Rows.Count - 1
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblAssortments WHERE AssortmentName = '" & flxDept.Item(1, I).Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT MAX(AssortmentID) AS MaxID FROM tblAssortments", AdoCN, 1, 1)
                    mStrSQL = rsComSql_1.Fields("MaxID").Value
                    AdoCN.Execute("INSERT INTO tblAssortments(AssortmentID,AssortmentName,Colour,Clarity,ClaritySize,Description,SupplierCode,AssortmentTypes,AssortPrice,Status,DoneBy,ModifyBy) " & _
                                  "VALUES(" & rsComSql_1.Fields("MaxID").Value & ",'" & flxDept.Item(1, I).Value & "','','','','',1,'Rough','" & flxDept.Item(7, I).Value & "','A','" & PBUser_ID & "','" & PBUser_ID & "')")

                Else
                    mStrSQL = rsComSql.Fields("AssortmentID").Value
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblItemMasterFile WHERE AssortmentName = '" & flxDept.Item(1, I).Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT MAX(ItemMasterRef) AS NoItms FROM tblItemMasterFile", AdoCN, 1, 1)
                    AdoCN.Execute("INSERT INTO tblItemMasterFile(ItemMasterRef,AssortmentNo,AssortmentName,TotalPcs,TotalCts,ItemCost,TotalValue,SupplierCode,TotalIssuePcs,TotalIssueCts,Status,DoneBy,ModifyBy) " & _
                                  "VALUES(" & rsComSql_2.Fields("NoItms").Value & "," & mStrSQL & ",'" & flxDept.Item(1, I).Value & "','" & flxDept.Item(4, I).Value & "','" & flxDept.Item(5, I).Value & "'," & _
                                        "'" & flxDept.Item(7, I).Value & "',0,1,0,0,'A','" & PBUser_ID & "','" & PBUser_ID & "')")
                    rsComSql_2 = Nothing
                    rsComSql_1 = Nothing
                End If
                rsComSql = Nothing
            Next

            AdoCN.Execute("INSERT INTO tblImportHeader(ImportNo,InvDate,RecDate,SupInvNo,DCLInvNo,BOINo,Supplier,Doneby) " & _
                          "VALUES(" & CDbl(txtImportNo.Text) & ",'" & Format(dtpInvDate.Value, "MM/dd/yyyy") & "','" & Format(dtpRecDate.Value, "MM/dd/yyyy") & "'," & _
                            "'" & txtSupRefNo.Text & "'," & CDbl(txtDclRefNo.Text) & ",'" & txtBoiNo.Text & "'," & CInt(cmbSupplier.Text) & ",'" & PBUser_ID & "')")

            For I = 0 To flxDept.Rows.Count - 1
                mStrSQL = "INSERT INTO tblImport(Department,SystemRefNo,SupplierRefNo,CompanyRefNo,BOINo,InvoiceDate,RecievedDate,SupplierCode,ParcelType,AssortmentNo," & _
                            "SupParcelNo,DCLParcelNo,INVPcs,INVCts,ACTPcs,ACtCts,ItemSize,Charges,ItemCost,RemPcs,RemCts,Status,DoneBy,ModifyBy,ImportNo,TrfPcs,TrfCts," & _
                            "LotNo,Article,Remarks,Category,HardCost,CompCode,ItemName,Urgent,NewAssort,NewLotNo,ImpPrice,ConRefNo,Sawn,Color,Clarity,Length,Width,Height,Origin," & _
                            "PriceType,Pointer,Original,Internal,CusDec,TraceID,BoxNo,ActItemCost,Labour,BoxName,SightNo) " & _
                          "VALUES('Rough Dept'," & CDbl(txtSysRefNo.Text) & ",'" & UCase(txtSupRefNo.Text) & "'," & CDbl(txtDclRefNo.Text) & "," & _
                            "'" & UCase(txtBoiNo.Text) & "','" & Format(dtpInvDate.Value, "MM/dd/yyyy") & "','" & Format(dtpRecDate.Value, "MM/dd/yyyy") & "'," & _
                            "" & CInt(cmbSupplier.Text) & ",'" & cmbType.Text & "','" & flxDept.Item(1, I).Value & "'," & _
                            "'" & flxDept.Item(2, I).Value & "','" & flxDept.Item(3, I).Value & "'," & CDbl(flxDept.Item(4, I).Value) & "," & _
                            "" & CDbl(flxDept.Item(5, I).Value) & "," & CDbl(flxDept.Item(4, I).Value) & "," & CDbl(flxDept.Item(5, I).Value) & "," & _
                            "" & CDbl(flxDept.Item(6, I).Value) & ",0," & CDbl(flxDept.Item(7, I).Value) & "," & _
                            "" & CDbl(flxDept.Item(4, I).Value) & "," & CDbl(flxDept.Item(5, I).Value) & ",'I','" & PBUser_ID & "','" & PBUser_ID & "'," & _
                            "" & CDbl(txtImportNo.Text) & ",0,0," & CDbl(flxDept.Item(8, I).Value) & ",'" & flxDept.Item(9, I).Value & "','" & flxDept.Item(10, I).Value & "'," & _
                            "'" & cmbCategory.Text & "'," & CDbl(flxDept.Item(14, I).Value) & ",'" & cmbCompany.Text & "','" & flxDept.Item(11, I).Value & "'," & _
                            "" & CInt(flxDept.Item(12, I).Value) & ",'" & flxDept.Item(13, I).Value & "'," & CDbl(flxDept.Item(16, I).Value) & "," & CDbl(flxDept.Item(23, I).Value) & "," & _
                            "'" & txtConRefNo.Text & "','" & cmbSawn.Text & "','" & flxDept.Item(17, I).Value & "','" & flxDept.Item(18, I).Value & "','" & flxDept.Item(19, I).Value & "'," & _
                            "'" & flxDept.Item(20, I).Value & "','" & flxDept.Item(21, I).Value & "','" & flxDept.Item(22, I).Value & "','" & cmbPriceType.Text & "','" & flxDept.Item(24, I).Value & "'," & _
                            "" & IIf(chkOriginal.Checked = True, 1, 0) & "," & IIf(chkInternal.Checked = True, 1, 0) & "," & IIf(chkCusDec.Checked = True, 1, 0) & "," & CDbl(flxDept.Item(25, I).Value) & "," & _
                            "'" & flxDept.Item(30, I).Value & "'," & CDbl(flxDept.Item(31, I).Value) & "," & CDbl(flxDept.Item(32, I).Value) & ",'" & flxDept.Item(33, I).Value & "','" & flxDept.Item(34, I).Value & "')"

                AdoCN.Execute(mStrSQL)

                If Len(txtExpNo.Text) > 0 Then
                    strInvoiceNo = ""
                    strBoiNo = ""
                    If Len(flxDept.Item(26, I).Value) > 0 Then
                        strInvoiceNo = flxDept.Item(26, I).Value
                    Else
                        strInvoiceNo = UCase(txtSupRefNo.Text)
                    End If
                    If Len(flxDept.Item(27, I).Value) > 0 Then
                        strBoiNo = flxDept.Item(27, I).Value
                    Else
                        strBoiNo = UCase(txtBoiNo.Text)
                    End If
                    If Len(flxDept.Item(28, I).Value) > 0 Then
                        intSupCode = CInt(flxDept.Item(28, I).Value)
                    Else
                        intSupCode = CInt(cmbSupplier.Text)
                    End If
                    If Len(flxDept.Item(29, I).Value) > 0 Then
                        dtpInvDate2 = flxDept.Item(29, I).Value
                    Else
                        dtpInvDate2 = Format(dtpInvDate.Value, "MM/dd/yyyy")
                    End If
                    AdoCN.Execute("UPDATE tblImport SET SupplierRefNo = '" & strInvoiceNo & "',BOINo = '" & strBoiNo & "',SupplierCode = '" & intSupCode & "', " & _
                                    "InvoiceDate = '" & Format(dtpInvDate2, "MM/dd/yyyy") & "' WHERE SupParcelNo = '" & flxDept.Item(2, I).Value & "'")
                End If

                mStrSQL = "UPDATE tblImportUpload SET OK = 1 WHERE SupplierRefNo = '" & txtSupRefNo.Text & "' AND LotNo = " & CDbl(flxDept.Item(8, I).Value) & ""

                AdoCN.Execute(mStrSQL)
            Next

            MsgBox("Import Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

            ClearFields()
        End If
        
        Exit Sub
ErrorHandler:
        MsgBox(Err.Description, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmbSupplier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSupplier.SelectedIndexChanged
        rsComSql_2 = New ADODB.Recordset
        rsComSql_2.Open("SELECT * FROM tblSuppliers WHERE SupplierCode = " & CInt(cmbSupplier.Text) & "", AdoCN, 1, 1)
        If rsComSql_2.RecordCount Then
            txtSupplier.Text = rsComSql_2.Fields("CompanyName").Value
        Else
            txtSupplier.Text = ""
        End If
        rsComSql_2 = Nothing
    End Sub

    Private Sub cmdReport_Click(sender As Object, e As EventArgs) Handles cmdReport.Click
        If strDBName = "DiaStock" Then
            mReportName = "crptImportInfomation.rpt"
        Else
            mReportName = "crptImportInfomationSales.rpt"
        End If
        objForm = New frm_DCLReportViewer
        strReportPath = PBReportPath & "" & mReportName
        objForm.WindowState = FormWindowState.Maximized
        objForm.Show()
    End Sub

    Private Sub cmdOpen_Click(sender As Object, e As EventArgs) Handles cmdOpen.Click
        flxDept.Rows.Clear()
        OpenFileDialog1.Filter = "All Excel Files|*.xls;*.xlsx"
        OpenFileDialog1.InitialDirectory = "C:\"
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName <> "" Then
            txtFilePath.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Load_Excel()
        'On Error GoTo ErrorHandler

        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim intRow, m_LotNo As Integer
        Dim strAssortment, strLetter, strSupParNo, strDCLParNo As String
        Dim dblSize, dblPrice, dblTotPcs, dblTotCts, dblIndexNo, dblImpPrice, dblHardPrice As Double
        Dim intUrgent As Integer
        Dim dblLotNo As Double
        Dim dblExcelLotNo As Double
        Dim dblMasterLotNo As Double
        Dim dblTraceID As Double
        Dim dblDiaCost As Double
        Dim dblLabour As Double

        If txtFilePath.Text = "" Then
            MsgBox("Please select the Excel File", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If Len(Dir(txtFilePath.Text)) > 0 Then

            If ValidateFields() = False Then Exit Sub

            strLetter = ""
            strAssortment = ""
            If cmbCompany.Text = "DCL" Then
                If cmbSupplier.Text = "00001" Then
                    If cmbType.Text = "Rough" Then
                        strLetter = "D"
                    Else
                        strLetter = "P"
                    End If
                Else
                    If cmbType.Text = "Rough" Then
                        strLetter = "J"
                    Else
                        strLetter = "R"
                    End If
                End If
            ElseIf cmbCompany.Text = "CJM" Then
                If cmbType.Text = "Rough" Then
                    strLetter = "L"
                Else
                    strLetter = "P"
                End If
            Else
                If cmbSupplier.Text = "00001" Then
                    If cmbType.Text = "Rough" Then
                        strLetter = "N"
                    Else
                        strLetter = "Q"
                    End If
                Else
                    If cmbType.Text = "Rough" Then
                        strLetter = "M"
                    Else
                        strLetter = "S"
                    End If
                End If
            End If

            xlApp = New Excel.Application
            xlWorkBook = xlApp.Workbooks.Open(txtFilePath.Text)
            xlWorkSheet = xlWorkBook.Worksheets(1)

            dblIndexNo = 0
            If cmbType.Text = "Rough" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT MAX(CONVERT(numeric, RIGHT(SupParcelNo, 4))) AS MaxNo " & _
                              "FROM dbo.tblImport " & _
                              "WHERE (Department = N'Rough Dept') AND (YEAR(InvoiceDate) >= 2016) AND " & _
                                    "(LEFT(SupParcelNo, 1) = '" & strLetter & "')", AdoCN, 1, 1)
                If IsDBNull(rsComSql.Fields("MaxNo").Value) Then
                    dblIndexNo = 1
                Else
                    dblIndexNo = rsComSql.Fields("MaxNo").Value + 1
                End If
                rsComSql = Nothing
            Else
                AdoCN.Execute("DELETE FROM tblImportCode")
                For intRow = 6 To 1000
                    If Len(xlWorkSheet.Cells(intRow, 1).Value) = 0 Then Exit For
                    If Mid(xlWorkSheet.Cells(intRow, 1).Value, 1, 5) = "Total" Then Exit For
                    If Trim(xlWorkSheet.Cells(intRow, 1).Value) <> "" Then
                        strLetter = Mid(Trim(xlWorkSheet.Cells(intRow, 7).Value), 1, 1)
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM tblImportCode WHERE Letter = '" & strLetter & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            rsComSql = New ADODB.Recordset
                            If strLetter = "C" Then
                                rsComSql.Open("SELECT MAX(CONVERT(numeric, RIGHT(SupParcelNo, 5))) AS MaxNo " & _
                                          "FROM dbo.tblImport " & _
                                          "WHERE (LEFT(SupParcelNo, 1) = '" & strLetter & "')", AdoCN, 1, 1)
                            Else
                                rsComSql.Open("SELECT MAX(CONVERT(numeric, RIGHT(SupParcelNo, 5))) AS MaxNo " & _
                                          "FROM dbo.tblImport " & _
                                          "WHERE (Department = N'Rough Dept') AND (YEAR(InvoiceDate) >= 2016) AND " & _
                                                "(LEFT(SupParcelNo, 1) = '" & strLetter & "')", AdoCN, 1, 1)
                            End If
                            If IsDBNull(rsComSql.Fields("MaxNo").Value) Then
                                dblIndexNo = 1
                            Else
                                dblIndexNo = rsComSql.Fields("MaxNo").Value + 1
                            End If
                            rsComSql = Nothing
                            AdoCN.Execute("INSERT INTO tblImportCode(Letter,MaxNo) VALUES('" & strLetter & "'," & dblIndexNo & ")")
                        End If
                        rsComSql_1 = Nothing
                    End If
                Next
            End If

            flxDept.Rows.Clear()
            m_LotNo = 1
            dblLotNo = 0
            dblExcelLotNo = 0
            dblMasterLotNo = 0
            dblTraceID = 0
            dblDiaCost = 0
            dblLabour = 0
            For intRow = 6 To 1000
                If Mid(xlWorkSheet.Cells(intRow, 1).Value, 1, 5) = "Total" Then Exit For
                If Trim(xlWorkSheet.Cells(intRow, 1).Value) <> "" Then
                    If Len(Trim(xlWorkSheet.Cells(intRow, 1).Value)) > 0 Then
                        If CDbl(Trim(xlWorkSheet.Cells(intRow, 1).Value)) = 0 Then
                            dblExcelLotNo = CDbl(Trim(xlWorkSheet.Cells(intRow, 1).Value))
                            If (dblLotNo = 0 And dblExcelLotNo = 0) Or (dblLotNo < 56545248) Then
                                rsComSql_1 = New ADODB.Recordset
                                If strDBName = "DiaStock" Then
                                    rsComSql_1.Open("SELECT MAX(LotNo) AS MaxLotNo FROM tblImport WHERE (LotNo >= 58070251) AND (LotNo <= 58270251)", AdoCN, 1, 1)
                                Else
                                    rsComSql_1.Open("SELECT MAX(LotNo) AS MaxLotNo FROM tblImport WHERE (LotNo >= 56545248) AND (LotNo <= 56620248)", AdoCN, 1, 1)
                                End If

                                If Not IsDBNull(rsComSql_1.Fields("MaxLotNo").Value) Then
                                    dblLotNo = rsComSql_1.Fields("MaxLotNo").Value + 1
                                End If
                                rsComSql_1 = Nothing
                            Else
                                dblLotNo = dblLotNo + 1
                            End If
                        Else
                            dblLotNo = CDbl(Trim(xlWorkSheet.Cells(intRow, 1).Value))
                        End If
                    End If

                    If Len(Trim(xlWorkSheet.Cells(intRow, 37).Value)) > 0 Then
                        If IsNumeric(Trim(xlWorkSheet.Cells(intRow, 37).Value)) = True Then
                            If CDbl(Trim(xlWorkSheet.Cells(intRow, 37).Value)) > 0 Then
                                dblMasterLotNo = CDbl(Trim(xlWorkSheet.Cells(intRow, 37).Value))
                            Else
                                dblMasterLotNo = dblLotNo
                            End If
                        Else
                            dblMasterLotNo = dblLotNo
                        End If
                    Else
                        dblMasterLotNo = dblLotNo
                    End If

                    dblSize = CDbl(Trim(xlWorkSheet.Cells(intRow, 8).Value)) / CDbl(Trim(xlWorkSheet.Cells(intRow, 9).Value))
                    dblPrice = CDbl(Trim(xlWorkSheet.Cells(intRow, 14).Value)) / CDbl(Trim(xlWorkSheet.Cells(intRow, 9).Value))
                    dblHardPrice = CDbl(Trim(xlWorkSheet.Cells(intRow, 13).Value)) / CDbl(Trim(xlWorkSheet.Cells(intRow, 9).Value))
                    If IsNumeric(Trim(xlWorkSheet.Cells(intRow, 17).Value)) = True Then
                        dblImpPrice = CDbl(Trim(xlWorkSheet.Cells(intRow, 17).Value))
                    Else
                        dblImpPrice = dblPrice
                    End If

                    strAssortment = Trim(xlWorkSheet.Cells(intRow, 7).Value)

                    If cmbType.Text = "Rough" Then
                        strSupParNo = strLetter & strRight(dtpInvDate.Value.Year, 1) & Format(dtpInvDate.Value.Month, "00") & Format(dblIndexNo, "0000")
                        strDCLParNo = Mid(strSupParNo, 1, 1) & "S" & Format(dblIndexNo, "0000")
                        dblIndexNo = dblIndexNo + 1
                    Else
                        strLetter = Mid(strAssortment, 1, 1)
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT * FROM dbo.tblImportCode WHERE (Letter = '" & strLetter & "')", AdoCN, 1, 1)
                        dblIndexNo = rsComSql.Fields("MaxNo").Value
                        rsComSql = Nothing
                        AdoCN.Execute("UPDATE tblImportCode SET MaxNo = MaxNo + 1 WHERE (Letter = '" & strLetter & "')")
                        strSupParNo = strLetter & strRight(dtpInvDate.Value.Year, 1) & Format(dblIndexNo, "00000")
                        strDCLParNo = strSupParNo
                        dblIndexNo = dblIndexNo + 1
                    End If

                    If Len(Trim(xlWorkSheet.Cells(intRow, 38).Value)) > 0 Then
                        If IsNumeric(Trim(xlWorkSheet.Cells(intRow, 38).Value)) = True Then
                            If CDbl(Trim(xlWorkSheet.Cells(intRow, 38).Value)) > 0 Then
                                dblTraceID = CDbl(Trim(xlWorkSheet.Cells(intRow, 38).Value))
                            Else
                                dblTraceID = dblLotNo
                            End If
                        Else
                            dblTraceID = dblLotNo
                        End If
                    Else
                        dblTraceID = dblLotNo
                    End If

                    If Len(Trim(xlWorkSheet.Cells(intRow, 39).Value)) > 0 Then
                        If IsNumeric(Trim(xlWorkSheet.Cells(intRow, 39).Value)) = True Then
                            If CDbl(Trim(xlWorkSheet.Cells(intRow, 39).Value)) <> 0 Then
                                dblDiaCost = CDbl(Trim(xlWorkSheet.Cells(intRow, 39).Value))
                            Else
                                dblDiaCost = dblImpPrice
                            End If
                        Else
                            dblDiaCost = dblImpPrice
                        End If
                    Else
                        dblDiaCost = dblImpPrice
                    End If

                    If Len(Trim(xlWorkSheet.Cells(intRow, 40).Value)) > 0 Then
                        If IsNumeric(Trim(xlWorkSheet.Cells(intRow, 40).Value)) = True Then
                            If CDbl(Trim(xlWorkSheet.Cells(intRow, 40).Value)) > 0 Then
                                dblLabour = CDbl(Trim(xlWorkSheet.Cells(intRow, 40).Value))
                            End If
                        End If
                    End If

                    intUrgent = IIf(xlWorkSheet.Cells(intRow, 26).Value = "1", 1, 0)
                    flxDept.Rows.Add(m_LotNo,
                                     strAssortment,
                                     strSupParNo,
                                     strDCLParNo,
                                     Trim(xlWorkSheet.Cells(intRow, 8).Value),
                                     Math.Round(CDbl(xlWorkSheet.Cells(intRow, 9).Value), 3),
                                     Format(dblSize, "#0.00"),
                                     Format(dblPrice, "#0.00"),
                                     dblLotNo,
                                     Trim(xlWorkSheet.Cells(intRow, 24).Value),
                                     Trim(xlWorkSheet.Cells(intRow, 20).Value),
                                     xlWorkSheet.Cells(intRow, 3).Value,
                                     intUrgent,
                                     "",
                                     Format(dblHardPrice, "#0.00"),
                                     "0",
                                     dblMasterLotNo,
                                     Trim(xlWorkSheet.Cells(intRow, 31).Value),
                                     Trim(xlWorkSheet.Cells(intRow, 32).Value),
                                     Trim(xlWorkSheet.Cells(intRow, 33).Value),
                                     Trim(xlWorkSheet.Cells(intRow, 34).Value),
                                     Trim(xlWorkSheet.Cells(intRow, 35).Value),
                                     Trim(xlWorkSheet.Cells(intRow, 5).Value),
                                     Math.Round(dblImpPrice, 2),
                                     Trim(xlWorkSheet.Cells(intRow, 36).Value),
                                     dblTraceID, "", "", "", "",
                                     Trim(xlWorkSheet.Cells(intRow, 42).Value),
                                     Format(dblDiaCost, "#0.00"),
                                     Format(dblLabour, "#0.00"),
                                     Trim(xlWorkSheet.Cells(intRow, 43).Value),
                                     Trim(xlWorkSheet.Cells(intRow, 44).Value))

                    m_LotNo = m_LotNo + 1
                    dblTotPcs = dblTotPcs + CDbl(Trim(xlWorkSheet.Cells(intRow, 8).Value))
                    dblTotCts = dblTotCts + CDbl(Trim(xlWorkSheet.Cells(intRow, 9).Value))
                Else
                    Exit For
                End If

            Next
            xlWorkSheet = Nothing
            xlWorkBook.Close()
            xlApp.Quit()

            txtTotalPcs.Text = dblTotPcs
            txtTotalCts.Text = dblTotCts

            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)
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

    Private Sub txtSupRefNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSupRefNo.KeyPress
        Dim dblTotPcs As Double
        Dim dblTotCts As Double

        If Asc(e.KeyChar) = 13 Then
            ClearFields()

            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblImport WHERE SupplierRefNo = '" & txtSupRefNo.Text & "'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                txtImportNo.Text = rsComSql_1.Fields("ImportNo").Value
                txtSysRefNo.Text = rsComSql_1.Fields("SystemRefNo").Value
                txtDclRefNo.Text = rsComSql_1.Fields("CompanyRefNo").Value
                txtBoiNo.Text = rsComSql_1.Fields("BOINo").Value
                cmbCategory.Text = rsComSql_1.Fields("Category").Value
                cmbCompany.Text = rsComSql_1.Fields("CompCode").Value
                cmbType.Text = rsComSql_1.Fields("ParcelType").Value
                dtpInvDate.Value = rsComSql_1.Fields("InvoiceDate").Value
                dtpRecDate.Value = rsComSql_1.Fields("RecievedDate").Value
                cmbSupplier.SelectedText = Format(rsComSql_1.Fields("SupplierCode").Value, "00000")
                cmbSupplier.Text = Format(rsComSql_1.Fields("SupplierCode").Value, "00000")
                cmbSawn.Text = rsComSql_1.Fields("Sawn").Value
            End If
            rsComSql_1 = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblImportUpload WHERE SupplierRefNo = '" & txtSupRefNo.Text & "' AND OK = 0 ORDER BY ID", AdoCN, 1, 1)
            If rsComSql.RecordCount > 0 Then
                rsComSql.MoveFirst()

                cmbCompany.Text = rsComSql.Fields("CompCode").Value
                cmbType.Text = rsComSql.Fields("ParcelType").Value
                cmbSupplier.SelectedText = Format(rsComSql.Fields("SupplierCode").Value, "00000")
                cmbSupplier.Text = Format(rsComSql.Fields("SupplierCode").Value, "00000")

                While Not rsComSql.EOF
                    flxDept.Rows.Add(rsComSql.Fields("IndexNo").Value,
                                     rsComSql.Fields("AssortmentNo").Value,
                                     rsComSql.Fields("SupParcelNo").Value,
                                     rsComSql.Fields("DCLParcelNo").Value,
                                     rsComSql.Fields("INVPcs").Value,
                                     rsComSql.Fields("INVCts").Value,
                                     Math.Round(rsComSql.Fields("ItemSize").Value, 2),
                                     rsComSql.Fields("ItemCost").Value,
                                     rsComSql.Fields("LotNo").Value,
                                     rsComSql.Fields("Article").Value,
                                     rsComSql.Fields("Remarks").Value,
                                     rsComSql.Fields("ItemName").Value,
                                     rsComSql.Fields("Urgent").Value,
                                     rsComSql.Fields("NewAssort").Value,
                                     rsComSql.Fields("HardCost").Value,
                                     rsComSql.Fields("SelectCost").Value,
                                     rsComSql.Fields("NewLotNo").Value, "", "", "", "", "",
                                     rsComSql.Fields("Origin").Value,
                                     rsComSql.Fields("ImpPrice").Value, "",
                                     rsComSql.Fields("LotNo").Value)

                    dblTotPcs = dblTotPcs + rsComSql.Fields("INVPcs").Value
                    dblTotCts = dblTotCts + rsComSql.Fields("INVCts").Value
                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing
        End If

        txtTotalPcs.Text = dblTotPcs
        txtTotalCts.Text = Math.Round(dblTotCts, 3)
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRghImportMasterLot.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub Load_ExportNo()
        Dim strLetter As String
        Dim strSupParNo As String
        Dim dblIndexNo As Double
        Dim dblSize As Double
        Dim dblPrice As Double
        Dim dblHardPrice As Double
        Dim strSupplierName As String
        Dim strSupplierCode As String
        Dim dblTotPcs, dblTotCts As Double
        Dim dblCount As Double

        flxDept.Rows.Clear()
        dblTotPcs = 0
        dblTotCts = 0
        dblCount = 1

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM VW_DCLFantacySchemaApcuImport WHERE (ExportNo = '" & txtExpNo.Text & "') ORDER BY LotID", dbConnDiaStock, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()

            cmbCategory.Text = rsComSql.Fields("Category").Value
            txtBoiNo.Text = rsComSql.Fields("BOINo").Value
            txtSupRefNo.Text = rsComSql.Fields("SupInvoiceNo").Value
            cmbCompany.Text = rsComSql.Fields("CompCode").Value
            txtDclRefNo.Text = rsComSql.Fields("CompanyRefNo").Value
            strSupplierName = rsComSql.Fields("CompanyName").Value

            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblSuppliers WHERE CompanyName = '" & strSupplierName & "'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                cmbSupplier.Text = Format(rsComSql_1.Fields("SupplierCode").Value, "00000")
            End If
            rsComSql_1 = Nothing

            cmbType.Text = "Polished"
            cmbSawn.Text = "Sawn"
            cmbPriceType.Text = "List"

            strLetter = "A"
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT MAX(CONVERT(numeric, RIGHT(SupParcelNo, 5))) AS MaxNo " & _
                            "FROM dbo.tblImport " & _
                            "WHERE (Department = N'Rough Dept') AND (YEAR(InvoiceDate) >= 2016) AND " & _
                                    "(LEFT(SupParcelNo, 1) = '" & strLetter & "')", AdoCN, 1, 1)
            If IsDBNull(rsComSql_1.Fields("MaxNo").Value) Then
                dblIndexNo = 1
            Else
                dblIndexNo = rsComSql_1.Fields("MaxNo").Value + 1
            End If
            rsComSql_1 = Nothing

            While Not rsComSql.EOF
                strSupplierCode = ""
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblSuppliers WHERE CompanyName = '" & rsComSql.Fields("CompanyName").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strSupplierCode = Format(rsComSql_1.Fields("SupplierCode").Value, "00000")
                Else
                    MsgBox("Invalid Supplier - " & rsComSql.Fields("CompanyName").Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                End If
                rsComSql_1 = Nothing

                strSupParNo = strLetter & strRight(dtpInvDate.Value.Year, 1) & Format(dblIndexNo, "00000")
                dblSize = rsComSql.Fields("Pcs").Value / rsComSql.Fields("Cts").Value
                dblPrice = rsComSql.Fields("TotalCost").Value / rsComSql.Fields("Cts").Value
                dblHardPrice = rsComSql.Fields("HardCost").Value / rsComSql.Fields("Cts").Value

                flxDept.Rows.Add(dblCount, "A" & rsComSql.Fields("CostingFor").Value,
                                 strSupParNo,
                                 strSupParNo,
                                 rsComSql.Fields("Pcs").Value,
                                 Math.Round(rsComSql.Fields("Cts").Value, 3),
                                 Format(dblSize, "#0.00"),
                                 Format(dblPrice, "#0.00"),
                                 rsComSql.Fields("LotID").Value, "",
                                 rsComSql.Fields("PackingListNo").Value & " - " & rsComSql.Fields("PackingType").Value,
                                 rsComSql.Fields("CostingFor").Value, 0, "",
                                 Format(dblHardPrice, "#0.00"), "0",
                                 rsComSql.Fields("NewLotNo").Value, "", "", "", "", "",
                                 rsComSql.Fields("Origin").Value,
                                 Math.Round(rsComSql.Fields("HardCost").Value, 2), "",
                                 rsComSql.Fields("LotID").Value,
                                 rsComSql.Fields("SupInvoiceNo").Value,
                                 rsComSql.Fields("BOINo").Value,
                                 strSupplierCode,
                                 Format(rsComSql.Fields("InvoiceDate").Value, "yyyy/MM/dd"),
                                 rsComSql.Fields("BoxNo").Value,
                                 Math.Round(rsComSql.Fields("HardCost").Value, 2),
                                 Math.Round(rsComSql.Fields("Labour").Value, 2),
                                 rsComSql.Fields("BoxName").Value,
                                 rsComSql.Fields("SightNo").Value)

                dblTotPcs = dblTotPcs + rsComSql.Fields("Pcs").Value
                dblTotCts = dblTotCts + Math.Round(rsComSql.Fields("Cts").Value, 3)

                rsComSql.MoveNext()
                dblIndexNo = dblIndexNo + 1
                dblCount = dblCount + 1
            End While
        End If
        rsComSql = Nothing

        txtTotalPcs.Text = dblTotPcs
        txtTotalCts.Text = Math.Round(dblTotCts, 3)
    End Sub

    Private Sub Load_ExportNo2()
        Dim strLetter As String
        Dim strSupParNo As String
        Dim dblIndexNo As Double
        Dim dblSize As Double
        Dim dblPrice As Double
        Dim dblHardPrice As Double
        Dim strSupplierName As String
        Dim strSupplierCode As String
        Dim dblTotPcs, dblTotCts As Double
        Dim dblCount As Double

        flxDept.Rows.Clear()
        dblTotPcs = 0
        dblTotCts = 0
        dblCount = 1

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM VW_DCLFantacySchemaApcuImport2 WHERE (PackingListNo = '" & txtExpNo.Text & "') ORDER BY LotID", dbConnDiaStock, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()

            cmbCategory.Text = rsComSql.Fields("Category").Value
            txtBoiNo.Text = rsComSql.Fields("BOINo").Value
            txtSupRefNo.Text = rsComSql.Fields("SupInvoiceNo").Value
            cmbCompany.Text = rsComSql.Fields("CompCode").Value
            txtDclRefNo.Text = rsComSql.Fields("CompanyRefNo").Value
            strSupplierName = rsComSql.Fields("CompanyName").Value
            txtConRefNo.Text = rsComSql.Fields("ConRefNo").Value

            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblSuppliers WHERE CompanyName = '" & strSupplierName & "'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                cmbSupplier.Text = Format(rsComSql_1.Fields("SupplierCode").Value, "00000")
            End If
            rsComSql_1 = Nothing

            cmbType.Text = "Polished"
            cmbSawn.Text = "Sawn"
            cmbPriceType.Text = "List"

            strLetter = "H"
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT MAX(CONVERT(numeric, RIGHT(SupParcelNo, 5))) AS MaxNo " & _
                            "FROM dbo.tblImport " & _
                            "WHERE (Department = N'Rough Dept') AND (YEAR(InvoiceDate) >= 2016) AND " & _
                                    "(LEFT(SupParcelNo, 1) = '" & strLetter & "')", AdoCN, 1, 1)
            If IsDBNull(rsComSql_1.Fields("MaxNo").Value) Then
                dblIndexNo = 1
            Else
                dblIndexNo = rsComSql_1.Fields("MaxNo").Value + 1
            End If
            rsComSql_1 = Nothing

            While Not rsComSql.EOF
                strSupplierCode = ""
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblSuppliers WHERE CompanyName = '" & rsComSql.Fields("CompanyName").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strSupplierCode = Format(rsComSql_1.Fields("SupplierCode").Value, "00000")
                Else
                    MsgBox("Invalid Supplier - " & rsComSql.Fields("CompanyName").Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                End If
                rsComSql_1 = Nothing

                strSupParNo = strLetter & strRight(dtpInvDate.Value.Year, 1) & Format(dblIndexNo, "00000")
                dblSize = rsComSql.Fields("PackPcsA").Value / rsComSql.Fields("PackCtsA").Value
                dblPrice = rsComSql.Fields("ListValueA").Value / rsComSql.Fields("PackCtsA").Value
                dblHardPrice = rsComSql.Fields("ListValueA").Value / rsComSql.Fields("PackCtsA").Value

                flxDept.Rows.Add(dblCount, "H" & rsComSql.Fields("Assortment").Value,
                                 strSupParNo,
                                 strSupParNo,
                                 rsComSql.Fields("PackPcsA").Value,
                                 Math.Round(rsComSql.Fields("PackCtsA").Value, 3),
                                 Format(dblSize, "#0.00"),
                                 Format(dblPrice, "#0.00"),
                                 rsComSql.Fields("LotID").Value,
                                 rsComSql.Fields("PackingListNo").Value,
                                 rsComSql.Fields("Reference1").Value & " - " & rsComSql.Fields("PackingListNo").Value & " - " & rsComSql.Fields("PackingType").Value,
                                 rsComSql.Fields("Assortment").Value, 0, "",
                                 Format(dblHardPrice, "#0.00"), "0",
                                 rsComSql.Fields("NewLotNo").Value, "", "", "", "", "",
                                 rsComSql.Fields("Origin").Value,
                                 Math.Round(dblHardPrice, 2), "",
                                 rsComSql.Fields("LotID").Value,
                                 rsComSql.Fields("SupInvoiceNo").Value,
                                 rsComSql.Fields("BOINo").Value,
                                 strSupplierCode,
                                 Format(rsComSql.Fields("InvoiceDate").Value, "yyyy/MM/dd"),
                                 rsComSql.Fields("BoxNo").Value,
                                 Math.Round(dblHardPrice, 2),
                                 0,
                                 rsComSql.Fields("BoxName").Value,
                                 rsComSql.Fields("SightNo").Value)

                dblTotPcs = dblTotPcs + rsComSql.Fields("PackPcsA").Value
                dblTotCts = dblTotCts + Math.Round(rsComSql.Fields("PackCtsA").Value, 3)

                rsComSql.MoveNext()
                dblIndexNo = dblIndexNo + 1
                dblCount = dblCount + 1
            End While
        End If
        rsComSql = Nothing

        txtTotalPcs.Text = dblTotPcs
        txtTotalCts.Text = Math.Round(dblTotCts, 3)
    End Sub

    Private Sub txtExpNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtExpNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If chkPolish.Checked = True Then
                Load_ExportNo2()
            Else
                Load_ExportNo()
            End If
        End If
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDept)
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        If strDBName = "DiaStock" Then
            mReportName = "crptImportInfomation2.rpt"
        Else
            mReportName = "crptImportInfomationSales.rpt"
        End If
        objForm = New frm_DCLReportViewer
        strReportPath = PBReportPath & "" & mReportName
        objForm.WindowState = FormWindowState.Maximized
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        If strDBName = "DiaStock" Then
            mReportName = "crptDCLImportSticker.rpt"
        Else
            mReportName = "crptDCLImportStickerSales.rpt"
        End If
        objForm = New frm_DCLReportViewer
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.WindowState = FormWindowState.Maximized
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button4_Click(sender As Object, e As EventArgs) Handles HazelDev_Button4.Click
        If strDBName = "DiaStock" Then
            mReportName = "crptImportInfomation3.rpt"
        Else
            mReportName = "crptImportInfomationSales.rpt"
        End If
        objForm = New frm_DCLReportViewer
        strReportPath = PBReportPath & "" & mReportName
        objForm.WindowState = FormWindowState.Maximized
        objForm.Show()
    End Sub

    Private Sub cndRefresh_Click(sender As Object, e As EventArgs) Handles cndRefresh.Click
        GetNewImportNo()
        GetSystemRefNo()
    End Sub
End Class