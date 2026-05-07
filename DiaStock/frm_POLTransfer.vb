
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_POLTransfer
    Dim strFolderPath As String

    Private Sub ClearFields()
        txtPcs.Text = ""
        txtCts.Text = ""
        txtFilePath.Text = ""
        flxDetails.Rows.Clear()
        txtExpNo.Text = GetNewExpNo()
        txtTrfNo.Text = GetNewTrfNo
        cmbCompCode.Text = ""
    End Sub

    Private Sub Load_Company()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblCompany ORDER BY CompCode", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbCompCode.Items.Add(rsComSql.Fields("CompCode").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub frm_POLTransfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Company()

        If strDBName = "DiaStock" Then
            strFolderPath = "PolishBox\"
        Else
            strFolderPath = "DiaSalesPolishBox\"
        End If

        ClearFields()
    End Sub

    Private Function GetNewExpNo()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(SalesNo) AS MaxNo FROM tblPOLSales", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("MaxNo").Value) Then
                GetNewExpNo = rsComSql.Fields("MaxNo").Value + 1
            Else
                GetNewExpNo = 1
            End If
        Else
            GetNewExpNo = 1
        End If
        rsComSql = Nothing
        Return GetNewExpNo
    End Function

    Private Function GetNewTrfNo()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(TransferNo) AS MaxNo FROM tblPOLTransfer", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("MaxNo").Value) Then
                GetNewTrfNo = rsComSql.Fields("MaxNo").Value + 1
            Else
                GetNewTrfNo = 1
            End If
        Else
            GetNewTrfNo = 1
        End If
        rsComSql = Nothing
        Return GetNewTrfNo
    End Function

    Private Sub cmdOpen_Click(sender As Object, e As EventArgs) Handles cmdOpen.Click
        flxDetails.Rows.Clear()
        txtPcs.Text = ""
        txtCts.Text = ""

        OpenFileDialog1.Filter = "All Excel Files|*.xls;*.xlsx"
        OpenFileDialog1.InitialDirectory = "C:\"
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName <> "" Then
            txtFilePath.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Load_Excel()
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim intRow As Integer
        Dim strSizeRange As String
        Dim dblAvgCost As Double

        If txtFilePath.Text = "" Then Exit Sub
        If Len(Dir(txtFilePath.Text)) > 0 Then
            xlApp = New Excel.Application
            xlWorkBook = xlApp.Workbooks.Open(txtFilePath.Text)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            flxDetails.Rows.Clear()
            For intRow = 2 To 10000
                If Len(xlWorkSheet.Cells(intRow, 1).value) <> 0 Then
                    strSizeRange = Trim(xlWorkSheet.Cells(intRow, 7).Value)
                    If Len(strSizeRange) = 0 Then
                        strSizeRange = "0"
                    End If

                    dblAvgCost = 0
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & Trim(xlWorkSheet.Cells(intRow, 5).Value) & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        If cmbCompCode.Text = "DCL" Then
                            dblAvgCost = rsComSql_1.Fields("CurCost").Value
                        Else
                            dblAvgCost = rsComSql_1.Fields("CurCost2").Value
                        End If
                    End If
                    rsComSql_1 = Nothing

                    flxDetails.Rows.Add(Trim(xlWorkSheet.Cells(intRow, 1).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 5).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 3).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 4).Value),
                                        dblAvgCost,
                                        Math.Round(dblAvgCost * CDbl(Trim(xlWorkSheet.Cells(intRow, 4).Value)), 2),
                                        Trim(xlWorkSheet.Cells(intRow, 6).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 2).Value),
                                        "",
                                        strSizeRange)

                Else
                    Exit For
                End If
            Next
            xlWorkSheet = Nothing
            xlWorkBook.Close()
            xlApp.Quit()

            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)

            txtPcs.Text = CalTotalPcs(flxDetails)
            txtCts.Text = CalTotalCts(flxDetails)

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

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(2, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(3, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean
        Dim intOutPcs As Double
        Dim intBalPcs As Double
        Dim blnFound As Boolean
        Dim strType As String
        Dim strRefNo As String

        Dim dblSalesID As Double

        If cmbCompCode.Text = "" Then
            MsgBox("Invalid Company", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If optProd.Checked = True Then
            PBResponse = MsgBox("Are you sure to Production?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            strType = "P"
        ElseIf optSales.Checked = True Then
            PBResponse = MsgBox("Are you sure to Sales?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            strType = "S"
        Else
            PBResponse = MsgBox("Are you sure to Transfer?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            strType = "T"
        End If

        If PBResponse  = MsgBoxResult.Yes Then

            If optSales.Checked = True Then
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblPOLSales WHERE SalesNo = " & Val(txtExpNo.Text) & "", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    MsgBox("Sales No. already taken", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    txtExpNo.Text = GetNewExpNo()
                    Exit Sub
                End If
                rsComSql_1 = Nothing
            End If

            If optTransfer.Checked = True Then
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblPOLTransfer WHERE TransferNo = " & Val(txtTrfNo.Text) & "", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    MsgBox("Transfer No. already taken", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    txtTrfNo.Text = GetNewTrfNo()
                    Exit Sub
                End If
                rsComSql_1 = Nothing
            End If

            For intRow = 0 To flxDetails.Rows.Count - 1
                rsComSql = New ADODB.Recordset
                mStrSQL = "SELECT * FROM VW_POLStockBal2New WHERE Assortment2 = '" & Trim(flxDetails.Item(1, intRow).Value) & "' AND CompCode = '" & cmbCompCode.Text & "' AND SizeRange = '" & Trim(flxDetails.Item(9, intRow).Value) & "'"
                rsComSql.Open(mStrSQL, AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If rsComSql.Fields("Pcs").Value < CDbl(flxDetails.Item(2, intRow).Value) Then
                        MsgBox("Invalid Pcs - " & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If Math.Round(rsComSql.Fields("Cts").Value, 3) < CDbl(flxDetails.Item(3, intRow).Value) Then
                        MsgBox("Invalid Cts - " & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                Else
                    MsgBox("Invalid Assortment = " & flxDetails.Item(1, intRow).Value & "/" & Trim(flxDetails.Item(9, intRow).Value), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing

                'If optProd.Checked = True Then
                '    rsComSql = New ADODB.Recordset
                '    rsComSql.Open("SELECT * FROM tblPCUStockIn WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "'", AdoCN, 1, 1)
                '    If rsComSql.RecordCount Then
                '        MsgBox("Doc ID Already used - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                '        Exit Sub
                '    End If
                '    rsComSql = Nothing

                'ElseIf optSales.Checked = True Then
                '    rsComSql = New ADODB.Recordset
                '    rsComSql.Open("SELECT * FROM tblPOLSales WHERE DocID = '" & flxDetails.Item(0, intRow).Value & "'", AdoCN, 1, 1)
                '    If rsComSql.RecordCount Then
                '        MsgBox("Doc ID Already used - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                '        Exit Sub
                '    End If
                '    rsComSql = Nothing

                'Else
                '    rsComSql = New ADODB.Recordset
                '    rsComSql.Open("SELECT * FROM tblPOLTransfer WHERE DocID = '" & flxDetails.Item(0, intRow).Value & "'", AdoCN, 1, 1)
                '    If rsComSql.RecordCount Then
                '        MsgBox("Doc ID Already used - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                '        Exit Sub
                '    End If
                '    rsComSql = Nothing
                'End If
            Next

            strRefNo = txtExpNo.Text
            If optSales.Checked = True Then
                strRefNo = txtExpNo.Text
            Else
                If optTransfer.Checked = True Then
                    strRefNo = txtTrfNo.Text
                End If
            End If

            For intRow = 0 To flxDetails.Rows.Count - 1
                blnSave = True
                If optProd.Checked = True Then
                    AdoCN.Execute("INSERT INTO tblPCUStockIn(OrgAssort,Assortment,ParNo,InPcs,InCts,Price,Status,OrderNo) " & _
                                  "VALUES('" & UCase(flxDetails.Item(7, intRow).Value) & "','" & UCase(flxDetails.Item(1, intRow).Value) & "','" & flxDetails.Item(0, intRow).Value & "'," & _
                                    "" & CInt(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",0,'" & flxDetails.Item(6, intRow).Value & "')")

                ElseIf optSales.Checked = True Then
                    AdoCN.Execute("INSERT INTO tblPOLSales(SalesNo,Assortment,Assortment2,Pcs,Cts,Price,DocID,RefNo,CompCode,SizeRange) " & _
                                  "VALUES(" & Val(txtExpNo.Text) & ",'" & UCase(flxDetails.Item(7, intRow).Value) & "','" & UCase(flxDetails.Item(1, intRow).Value) & "'," & CInt(flxDetails.Item(2, intRow).Value) & "," & _
                                      "" & CDbl(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",'" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(8, intRow).Value & "','" & cmbCompCode.Text & "','" & Trim(flxDetails.Item(9, intRow).Value) & "')")

                Else
                    AdoCN.Execute("INSERT INTO tblPOLTransfer(TransferNo,Assortment,Assortment2,Pcs,Cts,Price,DocID,CompCode,SizeRange) " & _
                                  "VALUES(" & Val(txtTrfNo.Text) & ",'" & UCase(flxDetails.Item(7, intRow).Value) & "','" & UCase(flxDetails.Item(1, intRow).Value) & "'," & CInt(flxDetails.Item(2, intRow).Value) & "," & _
                                      "" & CDbl(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",'" & flxDetails.Item(0, intRow).Value & "','" & cmbCompCode.Text & "','" & Trim(flxDetails.Item(9, intRow).Value) & "')")

                End If

                AdoCN.Execute("INSERT INTO tblPOLStockOut(Assortment,Assortment2,Pcs,Cts,Price,DocID,OrderNo,CompCode,SizeRange,Type) " & _
                              "VALUES('" & flxDetails.Item(7, intRow).Value & "','" & UCase(flxDetails.Item(1, intRow).Value) & "'," & CInt(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & _
                                "" & CDbl(flxDetails.Item(4, intRow).Value) & ",'" & strRefNo & "','" & flxDetails.Item(6, intRow).Value & "','" & cmbCompCode.Text & "','" & Trim(flxDetails.Item(9, intRow).Value) & "','" & strType & "')")

                dblSalesID = 0
                If optSales.Checked = True Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT MAX(ID) AS MaxID FROM tblPOLSales WHERE SalesNo = " & Val(txtExpNo.Text) & "", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If Not IsDBNull(rsComSql.Fields("MaxID").Value) Then
                            dblSalesID = rsComSql.Fields("MaxID").Value
                        End If
                    End If
                    rsComSql = Nothing

                ElseIf optTransfer.Checked = True Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT MAX(ID) AS MaxID FROM tblPOLTransfer WHERE TransferNo = " & Val(txtTrfNo.Text) & "", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If Not IsDBNull(rsComSql.Fields("MaxID").Value) Then
                            dblSalesID = rsComSql.Fields("MaxID").Value
                        End If
                    End If
                    rsComSql = Nothing
                End If

                'Origin Entry
                If strType = "T" Or (strType = "S" And Mid(UCase(flxDetails.Item(7, intRow).Value), 1, 1) = "T") Then
                    intOutPcs = 0
                    intBalPcs = CInt(flxDetails.Item(2, intRow).Value)
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM VW_POLStockBalOrigin WHERE Assortment = '" & flxDetails.Item(7, intRow).Value & "' AND CompCode = '" & cmbCompCode.Text & "' ORDER BY SysDateTime", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        rsComSql.MoveFirst()
                        While Not rsComSql.EOF And intBalPcs > 0
                            If intBalPcs > 0 Then
                                blnFound = False
                                If intBalPcs <= rsComSql.Fields("BalPcs").Value Then
                                    intOutPcs = intBalPcs

                                    intBalPcs = 0
                                    blnFound = True
                                Else
                                    intOutPcs = rsComSql.Fields("BalPcs").Value
                                    intBalPcs = intBalPcs - intOutPcs
                                    blnFound = True
                                End If
                                If blnFound = True Then
                                    AdoCN.Execute("INSERT INTO tblPOLStockOutOrigin(RefNo,Assortment,SupParNo,Origin,Pcs,EntDate,BoxInDate,Type,CompCode,SalesID) " & _
                                                  "VALUES('" & strRefNo & "','" & flxDetails.Item(7, intRow).Value & "','" & rsComSql.Fields("SupParNo").Value & "','" & rsComSql.Fields("Origin").Value & "'," & intOutPcs & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(rsComSql.Fields("SysDateTime").Value, "MM/dd/yyyy") & "','" & strType & "','" & cmbCompCode.Text & "'," & dblSalesID & ")")
                                End If
                            End If
                            rsComSql.MoveNext()
                        End While
                    Else
                        AdoCN.Execute("INSERT INTO tblPOLStockOutOrigin(RefNo,Assortment,SupParNo,Origin,Pcs,EntDate,BoxInDate,Type,CompCode,SalesID) " & _
                                      "VALUES('" & strRefNo & "','" & flxDetails.Item(7, intRow).Value & "','X900003','De Beers'," & intBalPcs & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & strType & "','" & cmbCompCode.Text & "'," & dblSalesID & ")")
                    End If
                    rsComSql = Nothing
                End If
            Next

            If blnSave = True Then
                MsgBox("Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
            End If
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Load_Excel()
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLStockValue.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLStockTransferMFG.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLStockTransfer.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub HazelDev_Button4_Click(sender As Object, e As EventArgs) Handles HazelDev_Button4.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLStockValueAll.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button5_Click(sender As Object, e As EventArgs) Handles HazelDev_Button5.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLTransfer.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button6_Click(sender As Object, e As EventArgs) Handles HazelDev_Button6.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLStockValue2.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button7_Click(sender As Object, e As EventArgs) Handles HazelDev_Button7.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPOLStockValueRnd.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub
End Class