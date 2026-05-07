
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RghSortPrice
    Dim dblImpPrice As Double

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtParNo.Text = UCase(txtParNo.Text)
            If ParcelFound(txtParNo.Text) = True Then
                Load_Details()
            Else
                MsgBox("Invalid Parcel", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
                txtParNo.Focus()
            End If
        End If
    End Sub

    Private Function ParcelFound(ByVal strParceNo As String) As Boolean
        dblImpPrice = 0
        ParcelFound = True
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT dbo.tblImport.DCLParcelNo, dbo.tblImport.ImpPrice, dbo.tblImport.NewAssort, dbo.tblImport.INVCts, dbo.tblImport.Article, " & _
                            "dbo.tblImport.AssortmentNo, dbo.tblSuppliers.CompanyName, dbo.tblImport.BoxName " & _
                        "FROM dbo.tblImport INNER JOIN dbo.tblSuppliers ON dbo.tblImport.SupplierCode = dbo.tblSuppliers.SupplierCode " & _
                        "WHERE (dbo.tblImport.DCLParcelNo = '" & strParceNo & "')", AdoCN, 1, 1)
        If rsComSql_1.RecordCount > 0 Then
            dblImpPrice = Math.Round(rsComSql_1.Fields("ImpPrice").Value, 2)
            txtImpPrice.Text = dblImpPrice
            txtRghType.Text = rsComSql_1.Fields("NewAssort").Value
            txtCategory.Text = rsComSql_1.Fields("Article").Value
            txtItemName.Text = rsComSql_1.Fields("AssortmentNo").Value
            txtSupplier.Text = rsComSql_1.Fields("CompanyName").Value
            txtBoxName.Text = rsComSql_1.Fields("BoxName").Value
            txtImpValue.Text = rsComSql_1.Fields("INVCts").Value * rsComSql_1.Fields("ImpPrice").Value
            txtProfit.Text = "0"
            txtProfitPerc.Text = "0"

            ParcelFound = True
        Else
            ParcelFound = False
        End If
        rsComSql_1 = Nothing
    End Function

    Private Function ParcelFound2(ByVal strParceNo As String) As Boolean
        ParcelFound2 = True
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT DCLParcelNo FROM dbo.tblImport WHERE (DCLParcelNo = '" & strParceNo & "')", AdoCN, 1, 1)
        If rsComSql_1.RecordCount > 0 Then
            ParcelFound2 = True
        Else
            ParcelFound2 = False
        End If
        rsComSql_1 = Nothing
    End Function

    Private Sub ClearFields()
        txtParNo.Text = ""
        txtImpPrice.Text = "0"
        txtImpValue.Text = "0"
        txtTotVal.Text = "0"
        flxDetails.Rows.Clear()
        txtSubPcs.Text = "0"
        txtSubCts.Text = "0"
        txtLabCts.Text = "0"
        txtLabRate.Text = "40"
        txtLabValue.Text = "0"
        txtProfit.Text = "0"
        txtRghType.Text = ""
        txtCategory.Text = ""
        txtItemName.Text = ""
        txtSupplier.Text = ""
        cmbResult.Text = ""
        txtPrice.Text = ""
    End Sub

    Private Sub frm_RghSortPrice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        ClearFields()
        Load_Parcels()
    End Sub

    Private Sub Load_Parcels()
        cmbParcel.Text = ""
        cmbParcel.Items.Clear()
        rsComSql_3 = New ADODB.Recordset
        rsComSql_3.Open("SELECT TOP (100) PERCENT ParNo FROM dbo.tblRghSort WHERE (PktPrice = 0) AND (PktIss >= CONVERT(DATETIME, '2024-03-01 00:00:00', 102)) GROUP BY ParNo ORDER BY ParNo", AdoCN, 1, 1)
        If rsComSql_3.RecordCount Then
            rsComSql_3.MoveFirst()
            While Not rsComSql_3.EOF
                cmbParcel.Items.Add(rsComSql_3.Fields("ParNo").Value)
                rsComSql_3.MoveNext()
            End While
        End If
        rsComSql_3 = Nothing
    End Sub

    Private Sub Load_Details()
        Dim strResult As String
        Dim strResult1 As String
        Dim dblPrice As Double
        Dim blnDone As Boolean

        flxDetails.Rows.Clear()
        strResult = ""
        strResult1 = ""
        dblPrice = 0
        blnDone = False
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT ID, ParNo, PktNo, PktPcs, PktCts, PktColor, PktClarity, PktFlo, PktModel, PktIss, PktPrice, LabRate, PolPrice, Yield, Result, Result1, FinCts, Special " & _
                      "FROM dbo.tblRghSort " & _
                      "WHERE (ParNo = '" & txtParNo.Text & "') " & _
                      "ORDER BY PktModel, PktColor, PktClarity, PktFlo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                If rsComSql.Fields("Result").Value <> "" Then
                    strResult = rsComSql.Fields("Result").Value
                Else
                    strResult = "E"
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT FLOUROCENT, CLARUTY, COLOR, MODEL, RESULT " & _
                                    "FROM tblRghLogic " & _
                                    "WHERE (FLOUROCENT = '" & rsComSql.Fields("PktFlo").Value & "') AND (CLARUTY = '" & rsComSql.Fields("PktClarity").Value & "') AND (COLOR = '" & rsComSql.Fields("PktColor").Value & "') AND (MODEL = '" & rsComSql.Fields("PktModel").Value & "')", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        strResult = rsComSql_1.Fields("RESULT").Value
                    End If
                    rsComSql_1 = Nothing
                End If

                If rsComSql.Fields("Result1").Value <> "" Then
                    strResult1 = rsComSql.Fields("Result1").Value
                Else
                    strResult1 = strResult
                End If

                If rsComSql.Fields("PktPrice").Value = 0 Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT Price FROM tblRghLogicPrice WHERE (RghType = '" & txtRghType.Text & "') AND (Result = '" & strResult1 & "')", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        dblPrice = rsComSql_1.Fields("Price").Value
                    End If
                    rsComSql_1 = Nothing
                Else
                    dblPrice = rsComSql.Fields("PktPrice").Value
                    blnDone = True
                End If

                flxDetails.Rows.Add(rsComSql.Fields("PktModel").Value,
                                    rsComSql.Fields("PktColor").Value,
                                    rsComSql.Fields("PktClarity").Value,
                                    rsComSql.Fields("PktFlo").Value,
                                    rsComSql.Fields("PktPcs").Value,
                                    rsComSql.Fields("PktCts").Value,
                                    strResult,
                                    strResult1,
                                    rsComSql.Fields("ID").Value,
                                    dblPrice, "0", "0", "0", "0",
                                    IIf(rsComSql.Fields("Special").Value = 1, True, False))


                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtSubPcs.Text = CalTotalPcs(flxDetails)
        txtSubCts.Text = CalTotalCts(flxDetails)
        txtLabCts.Text = CalTotalCtsLab(flxDetails)
        If blnDone = True Then
            CalculateValue()
        End If


        'txtTotVal.Text = CalTotalValue(flxDetails)
        'txtImpValue.Text = Math.Round(dblImpPrice * CDbl(txtSubCts.Text), 2)
        'CalculateValue()
        'CalculatePerc()
    End Sub

    Private Sub CalculateValue()
        Dim intRow As Integer
        Dim dblPrice As Double
        Dim dblYield As Double

        For intRow = 0 To flxDetails.Rows.Count - 1
            dblYield = 0
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT Yield FROM tblRghLogicYield WHERE (MODEL = '" & flxDetails.Item(0, intRow).Value & "') AND (Category = '" & txtCategory.Text & "')", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                dblYield = rsComSql_1.Fields("Yield").Value
            End If
            rsComSql_1 = Nothing

            flxDetails.Item(13, intRow).Value = Math.Round(dblYield / 100, 2)
            flxDetails.Item(10, intRow).Value = Math.Round(CDbl(flxDetails.Item(5, intRow).Value) * dblYield / 100, 3)

            If IsNumeric(flxDetails.Item(9, intRow).Value) = True Then
                dblPrice = CDbl(flxDetails.Item(9, intRow).Value)
                flxDetails.Item(11, intRow).Value = Format(Math.Round(dblPrice * CDbl(flxDetails.Item(10, intRow).Value), 2), "#0.00")
            Else
                flxDetails.Item(11, intRow).Value = "0.00"
            End If

            flxDetails.Item(12, intRow).Value = Format(Math.Round(CDbl(flxDetails.Item(11, intRow).Value) / CDbl(flxDetails.Item(5, intRow).Value), 2), "#0.00")

            'flxDetails.Item(9, intRow).Value = CDbl(flxDetails.Item(7, intRow).Value) * CDbl(flxDetails.Item(8, intRow).Value) / 100
            'flxDetails.Item(10, intRow).Value = CDbl(flxDetails.Item(6, intRow).Value) * CDbl(flxDetails.Item(9, intRow).Value)
            'flxDetails.Item(11, intRow).Value = Math.Round(CDbl(flxDetails.Item(10, intRow).Value) / CDbl(flxDetails.Item(8, intRow).Value), 2)
        Next

        If txtLabRate.Text = "" Then txtLabRate.Text = "0"
        If txtLabCts.Text = "" Then txtLabCts.Text = "0"
        If txtImpValue.Text = "" Then txtImpValue.Text = "0"

        txtLabValue.Text = Math.Round(CDbl(txtLabCts.Text) * CDbl(txtLabRate.Text), 2)
        txtTotVal.Text = CalTotalValue(flxDetails)
        txtProfit.Text = Math.Round(CDbl(txtTotVal.Text) - (CDbl(txtImpValue.Text) + CDbl(txtLabValue.Text)), 2)
        If CDbl(txtImpValue.Text) + CDbl(txtLabValue.Text) <> 0 Then
            'txtProfitPerc.Text = Math.Round((CDbl(txtProfit.Text) / (CDbl(txtImpValue.Text) + CDbl(txtLabValue.Text))) * 100, 2) & "%"
            txtProfitPerc.Text = Math.Round((CDbl(txtProfit.Text) / (CDbl(txtImpValue.Text))) * 100, 2) & "%"
        Else
            txtProfitPerc.Text = "0.00%"
        End If

    End Sub

    Private Sub CalculatePerc()
        Dim intRow As Integer
        Dim dblPerc As Double

        For intRow = 0 To flxDetails.Rows.Count - 1
            dblPerc = Math.Round(CDbl(flxDetails.Item(2, intRow).Value) / CDbl(txtSubCts.Text) * 100, 2)
            flxDetails.Item(5, intRow).Value = Format(dblPerc, "#0.00") & "%"
        Next
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(4, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(5, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Function CalTotalCtsLab(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCtsLab = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Item(0, intRow).Value <> "Reject" Then
                CalTotalCtsLab = CalTotalCtsLab + Val(flxSample.Item(5, intRow).Value)
            End If
        Next
        CalTotalCtsLab = Math.Round(CalTotalCtsLab, 3)
    End Function

    Private Function CalTotalValue(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalValue = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalValue = CalTotalValue + Val(flxSample.Item(11, intRow).Value)
        Next
        CalTotalValue = Math.Round(CalTotalValue, 2)
    End Function

    Private Sub UpdatePrice()
        Dim intRow As Integer

        For intRow = 0 To flxDetails.Rows.Count - 1
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT Price FROM tblRghLogicPrice WHERE (RghType = '" & txtRghType.Text & "') AND (Result = '" & flxDetails.Item(7, intRow).Value & "')", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                flxDetails.Item(9, intRow).Value = rsComSql_1.Fields("Price").Value
            End If
            rsComSql_1 = Nothing
        Next
    End Sub

    Private Sub RefreshAll()
        Dim intRow As Integer
        Dim strResult As String

        For intRow = 0 To flxDetails.Rows.Count - 1
            strResult = flxDetails.Item(6, intRow).Value
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT FLOUROCENT, CLARUTY, COLOR, MODEL, RESULT " & _
                            "FROM tblRghLogic " & _
                            "WHERE (FLOUROCENT = '" & flxDetails.Item(3, intRow).Value & "') AND (CLARUTY = '" & flxDetails.Item(2, intRow).Value & "') AND (COLOR = '" & flxDetails.Item(1, intRow).Value & "') AND (MODEL = '" & flxDetails.Item(0, intRow).Value & "')", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                strResult = rsComSql_1.Fields("RESULT").Value
            End If
            rsComSql_1 = Nothing

            flxDetails.Item(6, intRow).Value = strResult
            flxDetails.Item(7, intRow).Value = strResult
        Next
    End Sub

    Private Sub cmdCalc_Click(sender As Object, e As EventArgs) Handles cmdCalc.Click
        CalculateValue()
    End Sub

    Private Sub flxDetails_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellValueChanged
        'CalculateValue()
        'UpdatePrice()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub PriceEdit()
        Dim intRow As Integer

        For intRow = 0 To flxDetails.Rows.Count - 1
            If cmbResult.Text = "" Then Exit Sub
            If txtPrice.Text = "" Then Exit Sub
            If CDbl(txtPrice.Text) < 0 Then Exit Sub

            If flxDetails.Item(7, intRow).Value = cmbResult.Text Then
                flxDetails.Item(9, intRow).Value = txtPrice.Text
            End If
        Next
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim strParNo As String

        If ParcelFound2(txtParNo.Text) = True Then

            If txtLabRate.Text = "" Then txtLabRate.Text = "0"

            strParNo = txtParNo.Text & "A"
            'rsComSql = New ADODB.Recordset
            'rsComSql.Open("SELECT * FROM tblRPrPacket WHERE ParNo = '" & strParNo & "'", AdoCN, 1, 1)
            'If rsComSql.RecordCount Then
            '    MsgBox("Parcel Already Issued - " & strParNo, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '    Exit Sub
            'End If
            'rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblRghSort WHERE ParNo = '" & txtParNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                For intRow = 0 To flxDetails.Rows.Count - 1
                    AdoCN.Execute("UPDATE tblRghSort SET Result = '" & flxDetails.Item(6, intRow).Value & "', Result1 = '" & flxDetails.Item(7, intRow).Value & "', " & _
                                    "PktPrice = '" & CDbl(flxDetails.Item(9, intRow).Value) & "', FinCts = '" & CDbl(flxDetails.Item(10, intRow).Value) & "', " & _
                                    "PolPrice = '" & CDbl(flxDetails.Item(12, intRow).Value) & "', Yield = '" & CDbl(flxDetails.Item(13, intRow).Value) & "', " & _
                                    "Special = '" & IIf(flxDetails.Item(14, intRow).Value = True, 1, 0) & "' " & _
                                  "WHERE ID = '" & flxDetails.Item(8, intRow).Value & "'")
                Next
            End If
            rsComSql = Nothing

            MsgBox("Parcel Details Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Load_Parcels()
            'ClearFields()
        End If
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
        Load_Parcels()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub txtLabRate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLabRate.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtLabRate.Text)
        If Asc(e.KeyChar) = 13 Then
            'CalculateValue()
        End If
    End Sub

    Private Sub cmdReport_Click(sender As Object, e As EventArgs) Handles cmdReport.Click
        objForm2 = New frm_DCLReportViewer2
        If strDBName = "DiaStock" Then
            mReportName = "crptRghSortLogicNew.rpt"
        Else
            mReportName = "crptRghSortLogicNewSales.rpt"
        End If
        strReportPath = PBReportPath & "Rgh\" & mReportName
        If txtParNo.Text <> "" Then
            mRecordSelectionFormula = "{VW_RghSortLogicNew.ParNo} = '" & txtParNo.Text & "'"
        Else
            mRecordSelectionFormula = "{VW_RghSortLogicNew.ParNo} = {?ParcelNo}"
        End If
        objForm2.Show()
    End Sub

    Private Sub cmdUpdatePrice_Click(sender As Object, e As EventArgs) Handles cmdUpdatePrice.Click
        UpdatePrice()
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm2 = New frm_DCLReportViewer2
        If strDBName = "DiaStock" Then
            mReportName = "crptRghSortLogicBox.rpt"
        Else
            mReportName = "crptRghSortLogicBoxSales.rpt"
        End If
        strReportPath = PBReportPath & "Rgh\" & mReportName
        If txtBoxName.Text <> "" Then
            mRecordSelectionFormula = "{VW_RghSortLogicNew.BoxName} = '" & txtBoxName.Text & "' AND {VW_RghSortLogicNew.InvoiceDate} >= {?FromDate} AND {VW_RghSortLogicNew.InvoiceDate} <= {?ToDate}"
        Else
            mRecordSelectionFormula = "{VW_RghSortLogicNew.BoxName} = {?BoxName} AND {VW_RghSortLogicNew.InvoiceDate} >= {?FromDate} AND {VW_RghSortLogicNew.InvoiceDate} <= {?ToDate}"
        End If
        objForm2.Show()
    End Sub

    Private Sub cmbParcel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbParcel.SelectedIndexChanged
        txtParNo.Text = cmbParcel.Text
        txtParNo.Focus()
        txtParNo.Text = UCase(txtParNo.Text)
        If ParcelFound(txtParNo.Text) = True Then
            Load_Details()
        Else
            MsgBox("Invalid Parcel", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
            txtParNo.Focus()
        End If
    End Sub

    Private Sub txtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrice.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtPrice.Text)
    End Sub

    Private Sub cmdEdit_Click(sender As Object, e As EventArgs) Handles cmdEdit.Click
        PriceEdit()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        If strDBName = "DiaStock" Then
            mReportName = "crptRghSortLogicSum.rpt"
        Else
            mReportName = "crptRghSortLogicSumSales.rpt"
        End If
        strReportPath = PBReportPath & "Rgh\" & mReportName
        objForm.Show()
    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        RefreshAll()
    End Sub
End Class