
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLLotApprovalAcc

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_Data()
        flxRough.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblDCLLotApproval WHERE Type = 0 AND Approval = 0 ORDER BY LotNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxRough.Rows.Add(rsComSql.Fields("LotNo").Value,
                                  rsComSql.Fields("Approval").Value,
                                  rsComSql.Fields("ReCheck").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        flxFancy.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblDCLLotApproval WHERE Type = 1 AND Approval = 0 ORDER BY LotNo", AdoCN, 1, 1)
        'rsComSql.Open("SELECT * FROM tblDCLLotApproval WHERE Type = 1 ORDER BY LotNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxFancy.Rows.Add(rsComSql.Fields("LotNo").Value,
                                  rsComSql.Fields("Approval").Value,
                                  rsComSql.Fields("ReCheck").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        flxRounds.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblDCLLotApproval WHERE Type = 2 AND Approval = 0 ORDER BY LotNo", AdoCN, 1, 1)
        'rsComSql.Open("SELECT * FROM tblDCLLotApproval WHERE Type = 2 ORDER BY LotNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxRounds.Rows.Add(rsComSql.Fields("LotNo").Value,
                                   rsComSql.Fields("Approval").Value,
                                   rsComSql.Fields("ReCheck").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        flxProfit.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT LotNo, Proceed FROM dbo.tblDCLLotApproval WHERE (Type = 2 OR Type = 1) AND Approval = 0 AND Proceed = 0 GROUP BY LotNo, Proceed ORDER BY LotNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxProfit.Rows.Add(rsComSql.Fields("LotNo").Value,
                                   rsComSql.Fields("Proceed").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub frm_DCLLotApprovalAcc_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        'Load_Data()
        Load_Origin()
        dtpToDate.Value = Date.Now
        dtpFromDate.Value = DateAdd(DateInterval.Month, -6, dtpToDate.Value)
    End Sub

    Private Sub Load_Origin()
        cmbOrigin.Items.Clear()
        rsComSql = New ADODB.Recordset
        mStrSQL = "SELECT Origin FROM tblDCLOrigin ORDER BY Origin"
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        Do While Not rsComSql.EOF
            cmbOrigin.Items.Add(rsComSql.Fields("Origin").Value)
            rsComSql.MoveNext()
        Loop
        rsComSql = Nothing
    End Sub

    Private Sub Save_Rough()
        Dim intRow As Integer

        For intRow = 0 To flxRough.Rows.Count - 1
            AdoCN.Execute("UPDATE tblDCLLotApproval SET Approval = " & IIf(flxRough.Item(1, intRow).Value = True, 1, 0) & ", ReCheck = " & IIf(flxRough.Item(2, intRow).Value = True, 1, 0) & " WHERE LotNo = '" & flxRough.Item(0, intRow).Value & "' AND Type = 0")
        Next

        Load_Data()
    End Sub

    Private Sub Save_Fancy()
        Dim intRow As Integer

        For intRow = 0 To flxFancy.Rows.Count - 1
            AdoCN.Execute("UPDATE tblDCLLotApproval SET Approval = " & IIf(flxFancy.Item(1, intRow).Value = True, 1, 0) & ", ReCheck = " & IIf(flxFancy.Item(2, intRow).Value = True, 1, 0) & " WHERE LotNo = '" & flxFancy.Item(0, intRow).Value & "' AND Type = 1")
        Next

        Load_Data()
    End Sub

    Private Sub Save_Rounds()
        Dim intRow As Integer

        For intRow = 0 To flxRounds.Rows.Count - 1
            AdoCN.Execute("UPDATE tblDCLLotApproval SET Approval = " & IIf(flxRounds.Item(1, intRow).Value = True, 1, 0) & ", ReCheck = " & IIf(flxRounds.Item(2, intRow).Value = True, 1, 0) & " WHERE LotNo = '" & flxRounds.Item(0, intRow).Value & "' AND Type = 2")
        Next

        Load_Data()
    End Sub

    Private Sub Save_Profit()
        Dim intRow As Integer

        For intRow = 0 To flxProfit.Rows.Count - 1
            AdoCN.Execute("UPDATE tblDCLLotApproval SET Proceed = " & IIf(flxProfit.Item(1, intRow).Value = True, 1, 0) & " WHERE LotNo = '" & flxRounds.Item(0, intRow).Value & "'")
        Next

        Load_Data()
    End Sub

    Private Sub cmdSaveRgh_Click(sender As Object, e As EventArgs) Handles cmdSaveRgh.Click
        Save_Rough()
    End Sub

    Private Sub cmdSaveFan_Click(sender As Object, e As EventArgs) Handles cmdSaveFan.Click
        Save_Fancy()
    End Sub

    Private Sub cmdSaveRnd_Click(sender As Object, e As EventArgs) Handles cmdSaveRnd.Click
        Save_Rounds()
    End Sub

    Private Sub flxRough_DoubleClick(sender As Object, e As EventArgs) Handles flxRough.DoubleClick
        Dim dblLotNo As Double

        If flxRough.Rows.Count > 0 Then
            dblLotNo = CDbl(flxRough.Item(0, flxRough.CurrentRow.Index).Value)
            objForm2 = New frm_DCLReportViewer2
            mReportName = "crptRprPlanDetailsSum.rpt"
            mRecordSelectionFormula = "{VW_RealImportsLot.LotNo} = " & dblLotNo & ""
            strReportPath = PBReportPath & "Rpr\" & mReportName
            objForm2.Show()
        End If
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        'Load_Data()
        cmbOrigin.Text = ""
        flxItem.Rows.Clear()
        flxLot.Rows.Clear()
    End Sub

    Private Sub flxFancy_DoubleClick(sender As Object, e As EventArgs) Handles flxFancy.DoubleClick
        Dim dblLotNo As Double

        If flxFancy.Rows.Count > 0 Then
            dblLotNo = CDbl(flxFancy.Item(0, flxFancy.CurrentRow.Index).Value)
            objForm2 = New frm_DCLReportViewer2
            mReportName = "crptFanProdSum.rpt"
            mRecordSelectionFormula = "{VW_FanAAProdSum.LotNo} = " & dblLotNo & ""
            strReportPath = PBReportPath & "Grading\" & mReportName
            objForm2.Show()
        End If
    End Sub

    Private Sub flxRounds_DoubleClick(sender As Object, e As EventArgs) Handles flxRounds.DoubleClick
        Dim dblLotNo As Double

        If flxRounds.Rows.Count > 0 Then
            dblLotNo = CDbl(flxRounds.Item(0, flxRounds.CurrentRow.Index).Value)
            objForm2 = New frm_DCLReportViewer2
            mReportName = "crptRndProdSum.rpt"
            mRecordSelectionFormula = "{VW_RndABProdSum.LotNo} = " & dblLotNo & ""
            strReportPath = PBReportPath & "RoundsFullFlow\" & mReportName
            objForm2.Show()
        End If
    End Sub

    Private Sub flxProfit_DoubleClick(sender As Object, e As EventArgs) Handles flxProfit.DoubleClick
        Dim dblLotNo As Double

        If flxProfit.Rows.Count > 0 Then
            dblLotNo = CDbl(flxProfit.Item(0, flxProfit.CurrentRow.Index).Value)
            objForm2 = New frm_DCLReportViewer2
            If optPolish.Checked = True Then
                mReportName = "crptDCLPolishProfitDCL2.rpt"
                mRecordSelectionFormula = "{VW_DCLParcelProfitLoss.LotNo} = " & dblLotNo & ""
                strReportPath = PBReportPath & "Grading\" & mReportName
            Else
                mReportName = "crptRprPlanDetailsSum.rpt"
                mRecordSelectionFormula = "{VW_RealImportsLot.LotNo} = " & dblLotNo & ""
                strReportPath = PBReportPath & "Rpr\" & mReportName
            End If
            objForm2.Show()
        End If
    End Sub

    Private Sub cmbOrigin_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbOrigin.SelectedIndexChanged
        flxItem.Rows.Clear()

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT ItemName FROM dbo.tblImport " & _
                      "WHERE (Original = 1) AND (ParcelType = N'Rough') AND (InvoiceDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (InvoiceDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') AND (Origin = '" & cmbOrigin.Text & "') " & _
                      "GROUP BY ItemName ORDER BY ItemName", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxItem.Rows.Add(rsComSql.Fields("ItemName").Value, False)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

    End Sub

    Private Sub Load_Lots()
        Dim intRow As Integer
        Dim dblInvCts As Double
        Dim dblExpCts As Double
        Dim dblBalCts As Double

        flxLot.Rows.Clear()
        For intRow = 0 To flxItem.Rows.Count - 1
            If flxItem.Item(1, intRow).Value = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT TOP (100) PERCENT LotNo FROM dbo.tblImport " & _
                              "WHERE (Original = 1) AND (ParcelType = N'Rough') AND (InvoiceDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (InvoiceDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') AND (Origin = '" & cmbOrigin.Text & "') AND (ItemName = '" & flxItem.Item(0, intRow).Value & "') " & _
                              "GROUP BY LotNo ORDER BY LotNo", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        dblInvCts = 0
                        dblExpCts = 0
                        dblBalCts = 0

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT ROUND(SUM(INVCts), 3) AS INVCts FROM dbo.tblImport WHERE (Original = 1) AND (LotNo = '" & rsComSql.Fields("LotNo").Value & "')", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            If Not IsDBNull(rsComSql_1.Fields("INVCts").Value) Then
                                dblInvCts = rsComSql_1.Fields("INVCts").Value
                            End If
                        End If
                        rsComSql_1 = Nothing

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT ROUND(SUM(RoughCts), 3) AS RoughCts FROM dbo.tblCosting WHERE (Department <> N'Contract') AND (LotID = '" & rsComSql.Fields("LotNo").Value & "')", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            If Not IsDBNull(rsComSql_1.Fields("RoughCts").Value) Then
                                dblExpCts = rsComSql_1.Fields("RoughCts").Value
                            End If
                        End If
                        rsComSql_1 = Nothing

                        dblBalCts = Math.Round(dblInvCts - dblExpCts, 3)

                        flxLot.Rows.Add(rsComSql.Fields("LotNo").Value, dblInvCts, dblExpCts, dblBalCts)

                        rsComSql.MoveNext()
                    End While
                End If
                rsComSql = Nothing
            End If
        Next

    End Sub

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Load_Lots()
    End Sub

    Private Sub Insert_Lots()
        Dim intRow As Integer

        AdoCN.Execute("DELETE FROM tblDCLLotNoPrice")

        For intRow = 0 To flxLot.Rows.Count - 1
            If CDbl(flxLot.Item(3, intRow).Value) = 0 Then
                AdoCN.Execute("INSERT INTO tblDCLLotNoPrice(LotNo) VALUES('" & flxLot.Item(0, intRow).Value & "')")
            End If
        Next

        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLAMLotPrice.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub cmdReport_Click(sender As Object, e As EventArgs) Handles cmdReport.Click
        Insert_Lots()
    End Sub
End Class