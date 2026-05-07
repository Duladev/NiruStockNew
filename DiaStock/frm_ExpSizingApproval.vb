
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_ExpSizingApproval

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_SizingDetails()
        Dim strOrgAssort As String
        Dim strSupParNo As String
        Dim dblInvPrice As Double
        Dim dblSelectPrice As Double

        Dim dblImportNo As Double
        Dim dblRecord As Double

        Dim strCategory As String
        Dim strPriceType As String
        Dim strPriceTypeIn As String

        Dim dblParcelValue As Double

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        If txtParcel.Text = "" Then
            rsComSql.Open("SELECT TOP (100) PERCENT Department, ParNo, SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts, ROUND(SUM(BasePrice * Cts), 2) AS ListValue, ROUND(SUM(DiaCost * Cts), 2) AS DiaValue " & _
                          "FROM dbo.tblExpSizingTypes " & _
                          "WHERE (OK = 2) AND (Sec = 1) " & _
                          "GROUP BY Department, ParNo " & _
                          "ORDER BY Department, ParNo", AdoCN, 1, 1)
        Else
            rsComSql.Open("SELECT TOP (100) PERCENT Department, ParNo, SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts, ROUND(SUM(BasePrice * Cts), 2) AS ListValue, ROUND(SUM(DiaCost * Cts), 2) AS DiaValue " & _
                          "FROM dbo.tblExpSizingTypes " & _
                          "WHERE (OK = 2) AND (Sec = 1) AND (ParNo = '" & txtParcel.Text & "') " & _
                          "GROUP BY Department, ParNo " & _
                          "ORDER BY Department, ParNo", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            ExpProgress.Value = 0
            ExpProgress.Text = "Please wait ....."
            ExpProgress.Visible = True
            ExpProgress.Maximum = rsComSql.RecordCount
            dblRecord = 0

            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                strOrgAssort = ""
                strSupParNo = ""
                dblInvPrice = 0
                dblSelectPrice = 0
                strPriceType = ""
                dblParcelValue = 0
                strPriceTypeIn = ""

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblParcel WHERE Depart = '" & rsComSql.Fields("Department").Value & "' AND GrpParNo = '" & rsComSql.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strOrgAssort = rsComSql_1.Fields("Assortment").Value
                    dblInvPrice = rsComSql_1.Fields("ItemCost").Value
                    strSupParNo = rsComSql_1.Fields("OrigParcelNo").Value
                End If
                rsComSql_1 = Nothing

                rsComSql_1 = New ADODB.Recordset
                If Len(rsComSql.Fields("ParNo").Value) <> 12 Then
                    rsComSql_1.Open("SELECT * FROM tblImport WHERE DCLParcelNo = '" & Mid(rsComSql.Fields("ParNo").Value, 1, 6) & "'", AdoCN, 1, 1)
                Else
                    rsComSql_1.Open("SELECT * FROM tblImport WHERE DCLParcelNo = '" & Mid(rsComSql.Fields("ParNo").Value, 1, 8) & "'", AdoCN, 1, 1)
                End If
                If rsComSql_1.RecordCount Then
                    If strOrgAssort = "" Then
                        strOrgAssort = rsComSql_1.Fields("AssortmentNo").Value
                    End If
                    If strSupParNo = "" Then
                        strSupParNo = rsComSql_1.Fields("SupParcelNo").Value
                    End If
                    If dblInvPrice = 0 Then
                        dblInvPrice = rsComSql_1.Fields("ItemCost").Value
                    End If
                    dblImportNo = rsComSql_1.Fields("ImportNo").Value
                    dblSelectPrice = rsComSql_1.Fields("SelectCost").Value
                    strPriceType = rsComSql_1.Fields("PriceType").Value
                Else
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT * FROM tblImport WHERE SupParcelNo = '" & rsComSql.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        If strOrgAssort = "" Then
                            strOrgAssort = rsComSql_2.Fields("AssortmentNo").Value
                        End If
                        If strSupParNo = "" Then
                            strSupParNo = rsComSql_2.Fields("SupParcelNo").Value
                        End If
                        If dblInvPrice = 0 Then
                            dblInvPrice = rsComSql_2.Fields("ItemCost").Value
                        End If
                        dblImportNo = rsComSql_2.Fields("ImportNo").Value
                        dblSelectPrice = rsComSql_2.Fields("SelectCost").Value
                        strPriceType = rsComSql_2.Fields("PriceType").Value
                    Else
                        rsComSql_3 = New ADODB.Recordset
                        rsComSql_3.Open("SELECT * FROM tblDep_Trf WHERE DCLParcelNo = '" & rsComSql.Fields("ParNo").Value & "' AND Department = 'Mix'", AdoCN, 1, 1)
                        If rsComSql_3.RecordCount Then
                            If strOrgAssort = "" Then
                                strOrgAssort = rsComSql_3.Fields("AssortmentNo").Value
                            End If
                            If strSupParNo = "" Then
                                strSupParNo = rsComSql_3.Fields("SupParcelNo").Value
                            End If
                            If dblInvPrice = 0 Then
                                dblInvPrice = rsComSql_3.Fields("ItemCost").Value
                            End If
                        End If
                        rsComSql_3 = Nothing
                    End If
                    rsComSql_2 = Nothing
                End If
                rsComSql_1 = Nothing

                strCategory = ""
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT Category, PriceType, SelectCost FROM tblImport WHERE (SupParcelNo = '" & strSupParNo & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strCategory = rsComSql_1.Fields("Category").Value & ""
                    If strPriceType = "" Then
                        strPriceType = rsComSql_1.Fields("PriceType").Value
                    End If
                    If dblSelectPrice = 0 Then
                        dblSelectPrice = rsComSql_1.Fields("SelectCost").Value
                    End If
                End If
                rsComSql_1 = Nothing

                If strPriceType = "" Then
                    strPriceType = "List"
                End If

                'rsComSql_1 = New ADODB.Recordset
                'rsComSql_1.Open("SELECT SUM(dbo.tblExpSizingTypes.Pcs * dbo.tblAssortList.StonePrice) AS ParcelValue " & _
                '                "FROM dbo.tblExpSizingTypes INNER JOIN dbo.tblAssortList ON dbo.tblExpSizingTypes.ReturnType = dbo.tblAssortList.Assortment " & _
                '                "WHERE (dbo.tblExpSizingTypes.ReturnType LIKE 'S%') AND (RIGHT(dbo.tblExpSizingTypes.ReturnType, 1) <> 'U') AND (dbo.tblExpSizingTypes.ParNo = '" & strSupParNo & "') AND (dbo.tblExpSizingTypes.OK = 2)", AdoCN, 1, 1)
                'If rsComSql_1.RecordCount Then
                '    If Not IsDBNull(rsComSql_1.Fields("ParcelValue").Value) Then
                '        dblParcelValue = rsComSql_1.Fields("ParcelValue").Value
                '    End If
                'End If
                'rsComSql_1 = Nothing

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT SUM(Cts * BasePrice) AS ParcelValue " & _
                                "FROM dbo.tblExpSizingTypes " & _
                                "WHERE (ReturnType LIKE 'S%') AND (ParNo = '" & strSupParNo & "') AND (OK = 2)", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("ParcelValue").Value) Then
                        dblParcelValue = rsComSql_1.Fields("ParcelValue").Value
                    End If
                End If
                rsComSql_1 = Nothing

                'rsComSql_1 = New ADODB.Recordset
                'rsComSql_1.Open("SELECT SUM(Cts * BasePrice) AS ParcelValue " & _
                '                "FROM dbo.tblExpSizingTypes " & _
                '                "WHERE (ReturnType LIKE 'S%') AND (RIGHT(ReturnType, 1) = 'U') AND (ParNo = '" & strSupParNo & "') AND (OK = 2)", AdoCN, 1, 1)
                'If rsComSql_1.RecordCount Then
                '    If Not IsDBNull(rsComSql_1.Fields("ParcelValue").Value) Then
                '        dblParcelValue = dblParcelValue + rsComSql_1.Fields("ParcelValue").Value
                '    End If
                'End If
                'rsComSql_1 = Nothing

                rsComSql_1 = New ADODB.Recordset
                'rsComSql_1.Open("SELECT SUM(dbo.tblExpSizingTypes.Cts * dbo.tblExpSizingTypes.BasePrice) AS ParcelValue, SUM(dbo.tblExpSizingTypes.Cts * dbo.tblAssortList.MarketPrice) AS ParcelValue2 " & _
                '                "FROM dbo.tblExpSizingTypes INNER JOIN dbo.tblAssortList ON dbo.tblExpSizingTypes.ReturnType = dbo.tblAssortList.Assortment " & _
                '                "WHERE (dbo.tblExpSizingTypes.ReturnType NOT LIKE 'S%') AND (dbo.tblExpSizingTypes.ParNo = '" & strSupParNo & "') AND (dbo.tblExpSizingTypes.OK = 2)", AdoCN, 1, 1)
                rsComSql_1.Open("SELECT SUM(Cts * BasePrice) AS ParcelValue " & _
                                "FROM dbo.tblExpSizingTypes " & _
                                "WHERE (ReturnType NOT LIKE 'S%') AND (ParNo = '" & strSupParNo & "') AND (OK = 2)", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("ParcelValue").Value) Then
                        dblParcelValue = dblParcelValue + rsComSql_1.Fields("ParcelValue").Value
                    End If
                End If
                rsComSql_1 = Nothing

                If dblParcelValue = 0 Then
                    dblParcelValue = dblInvPrice * rsComSql.Fields("Cts").Value
                End If

                'Checking for Stock In Value
                strPriceTypeIn = strPriceType
                If dblSelectPrice > 0 Then
                    If dblParcelValue + 5 > dblSelectPrice * rsComSql.Fields("Cts").Value Then
                        strPriceTypeIn = "List"
                    End If
                End If
                
                If dblParcelValue + 5 < dblInvPrice * rsComSql.Fields("Cts").Value Then
                    strPriceTypeIn = "Import"
                End If

                flxDetails.Rows.Add(rsComSql.Fields("Department").Value,
                                    rsComSql.Fields("ParNo").Value,
                                    strOrgAssort,
                                    rsComSql.Fields("Pcs").Value,
                                    Format(rsComSql.Fields("Cts").Value, "#0.000"),
                                    Format(dblParcelValue, "#0.00"),
                                    Format(dblInvPrice * rsComSql.Fields("Cts").Value, "#0.00"),
                                    Format(dblSelectPrice * rsComSql.Fields("Cts").Value, "#0.00"),
                                    False,
                                    strSupParNo,
                                    strCategory,
                                    strPriceType,
                                    strPriceTypeIn,
                                    Format(rsComSql.Fields("DiaValue").Value, "#0.00"))

                rsComSql.MoveNext()
                dblRecord = dblRecord + 1
                ExpProgress.Value = dblRecord
                Application.DoEvents()
            End While
        Else
            MsgBox("No Records to Approve", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing

        ExpProgress.Visible = False

    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        Load_SizingDetails()
        chkSelect.Checked = False
        txtTotalPcs.Text = "0"
        txtTotalCts.Text = "0"
        txtTotBase.Text = "0"
        txtTotSel.Text = "0"
    End Sub

    Private Function CalTotalPcs() As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Rows(intRow).Cells(8).EditedFormattedValue = True Or flxDetails.Rows(intRow).Cells(8).EditedFormattedValue = 1 Then
                CalTotalPcs = CalTotalPcs + CInt(flxDetails.Item(3, intRow).Value)
            End If
        Next
        Return CalTotalPcs
    End Function

    Private Function CalTotalCts() As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Rows(intRow).Cells(8).EditedFormattedValue = True Or flxDetails.Rows(intRow).Cells(8).EditedFormattedValue = 1 Then
                CalTotalCts = CalTotalCts + CDbl(flxDetails.Item(4, intRow).Value)
            End If
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
        Return CalTotalCts
    End Function

    Private Function CalTotalBaseVal() As Double
        Dim intRow As Integer

        CalTotalBaseVal = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Rows(intRow).Cells(8).EditedFormattedValue = True Or flxDetails.Rows(intRow).Cells(8).EditedFormattedValue = 1 Then
                CalTotalBaseVal = CalTotalBaseVal + CDbl(flxDetails.Item(5, intRow).Value)
            End If
        Next
        CalTotalBaseVal = Math.Round(CalTotalBaseVal, 3)
        Return CalTotalBaseVal
    End Function

    Private Function CalTotalSelectVal() As Double
        Dim intRow As Integer

        CalTotalSelectVal = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Rows(intRow).Cells(8).EditedFormattedValue = True Or flxDetails.Rows(intRow).Cells(8).EditedFormattedValue = 1 Then
                CalTotalSelectVal = CalTotalSelectVal + CDbl(flxDetails.Item(7, intRow).Value)
            End If
        Next
        CalTotalSelectVal = Math.Round(CalTotalSelectVal, 3)
        Return CalTotalSelectVal
    End Function

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
        txtTotBase.Text = CalTotalBaseVal()
        txtTotSel.Text = CalTotalSelectVal()
    End Sub

    Private Sub cmdCalc_Click(sender As Object, e As EventArgs) Handles cmdCalc.Click
        txtTotalPcs.Text = CalTotalPcs()
        txtTotalCts.Text = CalTotalCts()
        txtTotBase.Text = CalTotalBaseVal()
        txtTotSel.Text = CalTotalSelectVal()
    End Sub

    Private Sub cmdTrf_Click(sender As Object, e As EventArgs) Handles cmdTrf.Click
        Approve()
    End Sub

    Private Sub Approve()
        Dim PBResponse
        Dim intRow As Integer
        Dim blnSave As Boolean

        blnSave = False
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(8, intRow).Value = True Then
                    If flxDetails.Item(11, intRow).Value <> flxDetails.Item(12, intRow).Value Then
                        MsgBox("Invalid Price type for - " & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If
            Next

            dtpToday = GetToday()
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(8, intRow).Value = True Then
                    blnSave = True

                    AdoCN.Execute("UPDATE tblExpSizingTypes SET OK = 3,AppUser = '" & PBUser_EmpNo & "',AppDate = '" & Format(dtpToday, "MM/dd/yyyy") & "',AppTime = '" & Format(Date.Now, "HH:mm:ss") & "' " & _
                                  "WHERE Department = '" & flxDetails.Item(0, intRow).Value & "' AND ParNo = '" & flxDetails.Item(1, intRow).Value & "' AND OK = 2")

                End If
            Next
            If blnSave = True Then
                MsgBox("Approved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Else
                MsgBox("No Records to Approve", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            flxDetails.Rows.Clear()
            txtTotalPcs.Text = "0"
            txtTotalCts.Text = "0"
            txtTotBase.Text = "0"
            txtTotSel.Text = "0"
            chkSelect.Checked = False
        End If
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub frm_ExpSizingApproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 8 Then
            txtTotalPcs.Text = CalTotalPcs()
            txtTotalCts.Text = CalTotalCts()
            txtTotBase.Text = CalTotalBaseVal()
            txtTotSel.Text = CalTotalSelectVal()
        End If
    End Sub
End Class