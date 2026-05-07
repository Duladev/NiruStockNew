
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_DCLLotIDSearch

    Private Sub frm_DCLLotIDSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub txtLotID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLotID.KeyPress
        Dim intRow As Integer

        'e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If IsNumeric(txtLotID.Text) Then
                For intRow = 0 To flxDetails.Rows.Count - 1
                    If txtLotID.Text = flxDetails.Item(0, intRow).Value Then
                        MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                Next

                Get_LotDetails(txtLotID.Text)
            Else
                MsgBox("Invalid Lot No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If

            txtLotID.Text = ""
            txtLotID.Focus()
        End If
    End Sub

    Private Sub Get_LotDetails(ByVal strLotID As String)
        Dim blnFound As Boolean
        Dim intType As Integer
        Dim strRemark As String
        Dim strProcFlow As String
        Dim strEquipment As String
        Dim strController As String
        Dim strMineCompany As String
        Dim strGuaranteeLevel As String

        blnFound = False
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblGrading_Box_Forever WHERE ID = '" & CDbl(strLotID) & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            blnFound = True
            intType = 1
        End If
        rsComSql = Nothing

        If blnFound = False Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_PackingListPCU WHERE ID = '" & CDbl(strLotID) & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                blnFound = True
                intType = 2
            End If
            rsComSql = Nothing
        End If

        If blnFound = False Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM NiruStock.dbo.tblGrading_PackingListM WHERE ID = '" & CDbl(strLotID) & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                blnFound = True
                intType = 3
            End If
            rsComSql = Nothing
        End If

        strEquipment = ""
        strController = ""
        If blnFound = True Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM dbo.tblAMSLabExcel WHERE SupParNo = '" & txtLotID.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                strEquipment = rsComSql.Fields("Equipment").Value
                strController = rsComSql.Fields("Controller").Value
            End If
            rsComSql = Nothing
        End If

        rsComSql = New ADODB.Recordset
        Select Case intType
            Case 1
                rsComSql.Open("SELECT * FROM VW_DCLFantacySchemaFM WHERE ID = '" & CDbl(strLotID) & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strProcFlow = ""
                    If Not IsDBNull(rsComSql.Fields("MaterialProcument").Value) Then
                        Select Case rsComSql.Fields("MaterialProcument").Value
                            Case "310"
                                strProcFlow = "Rough Tender House"
                            Case "320"
                                strProcFlow = "Rough - Mining company"
                            Case "330"
                                strProcFlow = "Rough - Secondary market"
                        End Select
                    End If

                    strMineCompany = ""
                    strGuaranteeLevel = ""
                    If Not IsDBNull(rsComSql.Fields("MiningCompany2").Value) Then
                        Select Case rsComSql.Fields("MiningCompany2").Value
                            Case "11101"
                                strMineCompany = "Alrosa"
                                strGuaranteeLevel = "Doc & system Flow level"
                            Case "12255"
                                strMineCompany = "DTC"
                                strGuaranteeLevel = "Doc & system Flow level"
                            Case "Mining Companies Mix"
                                strMineCompany = "Mining Companies Mix"
                                strGuaranteeLevel = "Declarative level"
                        End Select
                    End If

                    strRemark = rsComSql.Fields("Company").Value & "/EXP/" & Year(rsComSql.Fields("InvDate").Value) & "/" & rsComSql.Fields("ExpInvNo").Value & "-" & rsComSql.Fields("PackNo").Value & " - " & rsComSql.Fields("PackingType").Value
                    flxDetails.Rows.Add(rsComSql.Fields("ID").Value,
                                        rsComSql.Fields("Assortment").Value,
                                        rsComSql.Fields("Pcs").Value,
                                        Math.Round(rsComSql.Fields("Cts").Value, 3),
                                        strRemark,
                                        rsComSql.Fields("ExportNo").Value,
                                        Format(rsComSql.Fields("InvDate").Value, "yyyy-MM-dd"),
                                        strMineCompany,
                                        rsComSql.Fields("MaterialProcument").Value,
                                        rsComSql.Fields("AssortmentNo").Value,
                                        Format(rsComSql.Fields("InvoiceDate").Value, "yyyy-MM-dd"),
                                        rsComSql.Fields("SupParcelNo").Value,
                                        rsComSql.Fields("LotNo2").Value,
                                        rsComSql.Fields("ItemName").Value,
                                        strEquipment, strController,
                                        strProcFlow, rsComSql.Fields("CompanyName").Value,
                                        rsComSql.Fields("CSR").Value, rsComSql.Fields("MiningCountry").Value, "",
                                        "Declarative level ", "Production from rough : Internal production", "DIAMOND CUTTERS LTD",
                                        "RJC Certified", strGuaranteeLevel, "", "",
                                        rsComSql.Fields("NewLotNo").Value)
                Else
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM VW_DCLFantacySchemaFM2 WHERE ID = '" & CDbl(strLotID) & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        strProcFlow = ""
                        If Not IsDBNull(rsComSql.Fields("MaterialProcument").Value) Then
                            Select Case rsComSql.Fields("MaterialProcument").Value
                                Case "310"
                                    strProcFlow = "Rough Tender House"
                                Case "320"
                                    strProcFlow = "Rough - Mining company"
                                Case "330"
                                    strProcFlow = "Rough - Secondary market"
                            End Select
                        End If

                        strMineCompany = ""
                        strGuaranteeLevel = ""
                        If Not IsDBNull(rsComSql.Fields("MiningCompany").Value) Then
                            Select Case rsComSql.Fields("MiningCompany").Value
                                Case "11101"
                                    strMineCompany = "Alrosa"
                                    strGuaranteeLevel = "Doc & system Flow level"
                                Case "12255"
                                    strMineCompany = "DTC"
                                    strGuaranteeLevel = "Doc & system Flow level"
                                Case "Mining Companies Mix"
                                    strMineCompany = "Mining Companies Mix"
                                    strGuaranteeLevel = "Declarative level"
                            End Select
                        End If

                        strRemark = rsComSql.Fields("Company").Value & "/EXP/" & Year(rsComSql.Fields("InvDate").Value) & "/" & rsComSql.Fields("ExpInvNo").Value & "-" & rsComSql.Fields("PackNo").Value & " - " & rsComSql.Fields("PackingType").Value
                        flxDetails.Rows.Add(rsComSql.Fields("ID").Value,
                                            rsComSql.Fields("Assortment").Value,
                                            rsComSql.Fields("Pcs").Value,
                                            Math.Round(rsComSql.Fields("Cts").Value, 3),
                                            strRemark,
                                            rsComSql.Fields("ExportNo").Value,
                                            Format(rsComSql.Fields("InvDate").Value, "yyyy-MM-dd"),
                                            strMineCompany,
                                            rsComSql.Fields("MaterialProcument").Value,
                                            rsComSql.Fields("AssortmentNo").Value,
                                            Format(rsComSql.Fields("InvoiceDate").Value, "yyyy-MM-dd"),
                                            rsComSql.Fields("SupParcelNo").Value,
                                            rsComSql.Fields("LotNo2").Value,
                                            rsComSql.Fields("ItemName").Value,
                                            strEquipment, strController,
                                            strProcFlow, rsComSql.Fields("CompanyName").Value,
                                            rsComSql.Fields("CSR").Value, rsComSql.Fields("MiningCountry").Value, "",
                                            "Declarative level ", "Production from rough : Internal production", "DIAMOND CUTTERS LTD",
                                            "RJC Certified", strGuaranteeLevel, "", "",
                                            rsComSql.Fields("NewLotNo").Value)
                    End If
                End If
            Case 2
                rsComSql.Open("SELECT * FROM VW_DCLFantacySchemaPcuClientF WHERE ID = '" & CDbl(strLotID) & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strProcFlow = ""
                    If Not IsDBNull(rsComSql.Fields("MaterialProcument").Value) Then
                        Select Case rsComSql.Fields("MaterialProcument").Value
                            Case "310"
                                strProcFlow = "Rough Tender House"
                            Case "320"
                                strProcFlow = "Rough - Mining company"
                            Case "330"
                                strProcFlow = "Rough - Secondary market"
                        End Select
                    End If

                    strMineCompany = ""
                    strGuaranteeLevel = ""
                    If Not IsDBNull(rsComSql.Fields("MiningCompany").Value) Then
                        Select Case rsComSql.Fields("MiningCompany").Value
                            Case "11101"
                                strMineCompany = "Alrosa"
                                strGuaranteeLevel = "Doc & system Flow level"
                            Case "12255"
                                strMineCompany = "DTC"
                                strGuaranteeLevel = "Doc & system Flow level"
                            Case "Mining Companies Mix"
                                strMineCompany = "Mining Companies Mix"
                                strGuaranteeLevel = "Declarative level"
                        End Select
                    End If


                    strRemark = rsComSql.Fields("Company").Value & "/EXP/" & Year(rsComSql.Fields("InvDate").Value) & "/" & rsComSql.Fields("ExpInvNo").Value & "-" & rsComSql.Fields("PackingListNo").Value & " - " & rsComSql.Fields("PackingType").Value
                    flxDetails.Rows.Add(rsComSql.Fields("ID").Value,
                                        rsComSql.Fields("Assortment").Value,
                                        rsComSql.Fields("ActPcs").Value,
                                        Math.Round(rsComSql.Fields("ActCts").Value, 3),
                                        strRemark,
                                        rsComSql.Fields("ExportNo").Value,
                                        Format(rsComSql.Fields("InvDate").Value, "yyyy-MM-dd"),
                                        strMineCompany,
                                        rsComSql.Fields("MaterialProcument").Value,
                                        rsComSql.Fields("AssortmentNo").Value,
                                        Format(rsComSql.Fields("InvoiceDate").Value, "yyyy-MM-dd"),
                                        rsComSql.Fields("SupParcelNo").Value,
                                        rsComSql.Fields("LotNo2").Value,
                                        rsComSql.Fields("ItemName").Value,
                                        strEquipment, strController,
                                        strProcFlow, rsComSql.Fields("CompanyName").Value,
                                        rsComSql.Fields("CSR").Value, rsComSql.Fields("MiningCountry").Value, "",
                                        "Declarative level ", "Production from rough : Internal production", "DIAMOND CUTTERS LTD",
                                        "RJC Certified", strGuaranteeLevel, "", "",
                                        rsComSql.Fields("NewLotNo").Value)
                End If
            Case 3
                rsComSql.Open("SELECT * FROM VW_DCLFantacySchemaColDetails WHERE ID = '" & CDbl(strLotID) & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strProcFlow = ""
                    If Not IsDBNull(rsComSql.Fields("MaterialProcument").Value) Then
                        Select Case rsComSql.Fields("MaterialProcument").Value
                            Case "310"
                                strProcFlow = "Rough Tender House"
                            Case "320"
                                strProcFlow = "Rough - Mining company"
                            Case "330"
                                strProcFlow = "Rough - Secondary market"
                        End Select
                    End If

                    strMineCompany = ""
                    strGuaranteeLevel = ""
                    If Not IsDBNull(rsComSql.Fields("MiningCompany2").Value) Then
                        Select Case rsComSql.Fields("MiningCompany2").Value
                            Case "11101"
                                strMineCompany = "Alrosa"
                                strGuaranteeLevel = "Doc & system Flow level"
                            Case "12255"
                                strMineCompany = "DTC"
                                strGuaranteeLevel = "Doc & system Flow level"
                            Case "Mining Companies Mix"
                                strMineCompany = "Mining Companies Mix"
                                strGuaranteeLevel = "Declarative level"
                        End Select
                    End If

                    strRemark = rsComSql.Fields("CompCode").Value & "/EXP/" & Year(rsComSql.Fields("InvDate").Value) & "/" & rsComSql.Fields("ExpInvNo").Value & "-" & rsComSql.Fields("PackingListNo").Value & " - " & rsComSql.Fields("PackingType").Value
                    flxDetails.Rows.Add(rsComSql.Fields("ID").Value,
                                        rsComSql.Fields("Assortment").Value,
                                        rsComSql.Fields("Pcs").Value,
                                        Math.Round(rsComSql.Fields("Cts").Value, 3),
                                        strRemark,
                                        rsComSql.Fields("ExportNo").Value,
                                        Format(rsComSql.Fields("InvDate").Value, "yyyy-MM-dd"),
                                        strMineCompany,
                                        rsComSql.Fields("MaterialProcument").Value,
                                        rsComSql.Fields("AssortmentNo").Value,
                                        Format(rsComSql.Fields("InvoiceDate").Value, "yyyy-MM-dd"),
                                        rsComSql.Fields("SupParcelNo").Value,
                                        rsComSql.Fields("LotNo").Value,
                                        rsComSql.Fields("ItemName").Value,
                                        strEquipment, strController,
                                        strProcFlow, rsComSql.Fields("CompanyName").Value,
                                        rsComSql.Fields("CSR").Value, rsComSql.Fields("MiningCountry").Value, "",
                                        "Declarative level ", "Production from rough : Internal production", "DIAMOND CUTTERS LTD",
                                        "RJC Certified", strGuaranteeLevel, "", "",
                                        rsComSql.Fields("NewLotNo").Value)
                End If
        End Select
        rsComSql = Nothing

        If blnFound = False Then
            flxDetails.Rows.Add(strLotID)
        End If

        'rsComSql = New ADODB.Recordset
        'rsComSql.Open("SELECT * FROM tblDCLGiaSchema WHERE Col03 = '" & strLotID & "'", AdoCN, 1, 1)
        'If rsComSql.RecordCount Then
        '    flxDetails.Item(7, flxDetails.Rows.Count - 1).Value = rsComSql.Fields("Col37").Value
        '    flxDetails.Item(16, flxDetails.Rows.Count - 1).Value = rsComSql.Fields("Col34").Value
        '    flxDetails.Item(17, flxDetails.Rows.Count - 1).Value = rsComSql.Fields("Col35").Value
        '    flxDetails.Item(18, flxDetails.Rows.Count - 1).Value = rsComSql.Fields("Col36").Value
        '    flxDetails.Item(19, flxDetails.Rows.Count - 1).Value = rsComSql.Fields("Col38").Value
        '    flxDetails.Item(21, flxDetails.Rows.Count - 1).Value = rsComSql.Fields("Col40").Value
        '    flxDetails.Item(22, flxDetails.Rows.Count - 1).Value = rsComSql.Fields("Col41").Value
        '    flxDetails.Item(24, flxDetails.Rows.Count - 1).Value = rsComSql.Fields("Col43").Value
        '    flxDetails.Item(25, flxDetails.Rows.Count - 1).Value = rsComSql.Fields("Col45").Value

        '    If flxDetails.Item(28, flxDetails.Rows.Count - 1).Value <> rsComSql.Fields("Col04").Value Then
        '        flxDetails.Item(29, flxDetails.Rows.Count - 1).Value = "Y"
        '    End If

        '    flxDetails.Item(28, flxDetails.Rows.Count - 1).Value = rsComSql.Fields("Col04").Value
        'End If
        'rsComSql = Nothing
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        txtLotID.Text = ""
        txtSupParNo.Text = ""
        flxDetails.Rows.Clear()
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        txtSupParNo.Text = flxDetails.Item(10, flxDetails.CurrentRow.Index).Value
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm2 = New frm_DCLReportViewer2
        mReportName = "crptRghExportDetails.rpt"
        mRecordSelectionFormula = "{VW_RghExportDetails.OrigParcelNo} = '" & txtSupParNo.Text & "'"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm2.Show()

        txtSupParNo.Text = ""
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

                flxUpload.Rows.Add(Trim(xlWorkSheet.Cells(intRow, 1).Value))

            Next
            xlWorkSheet = Nothing
            xlWorkBook.Close()
            xlApp.Quit()

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

    Private Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        Dim intRow As Integer

        For intRow = 0 To flxUpload.Rows.Count - 1
            Get_LotDetails(flxUpload.Item(0, intRow).Value)
        Next
    End Sub
End Class