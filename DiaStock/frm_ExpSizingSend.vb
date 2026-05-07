
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_ExpSizingSend

    Private Sub Load_SizingDetails()
        Dim strOrgAssort As String
        Dim strSupParNo As String
        Dim dblInvPrice As Double
        Dim dblBasePrice As Double
        Dim dblPerStonePrice As Double

        Dim intIssPcs As Integer
        Dim dblIssCts As Double

        Dim dblImportNo As Double
        Dim dblRecord As Double

        Dim strCategory As String

        Dim blnOld As Boolean

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        If txtParcel.Text = "" Then
            rsComSql.Open("SELECT * FROM tblExpSizingTypes WHERE OK = 3 AND Sec = 1 " & _
                          "ORDER BY Department, ParNo, PktNo, ReturnType", AdoCN, 1, 1)
        Else
            rsComSql.Open("SELECT * FROM tblExpSizingTypes WHERE ParNo = '" & txtParcel.Text & "' AND OK = 3 AND Sec = 1 " & _
                          "ORDER BY Department, ParNo, PktNo, ReturnType", AdoCN, 1, 1)
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
                dblBasePrice = 0
                dblPerStonePrice = 0

                blnOld = False
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT ParNo FROM tblExpSizingPlan WHERE ParNo = '" & rsComSql.Fields("ParNo").Value & "' AND Department = '" & rsComSql.Fields("Department").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    blnOld = True
                Else
                    blnOld = False
                End If
                rsComSql_1 = Nothing

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblParcel WHERE Depart = '" & rsComSql.Fields("Department").Value & "' AND GrpParNo = '" & rsComSql.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strOrgAssort = rsComSql_1.Fields("Assortment").Value
                    dblInvPrice = rsComSql_1.Fields("ItemCost").Value
                    strSupParNo = rsComSql_1.Fields("OrigParcelNo").Value
                End If
                rsComSql_1 = Nothing

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & rsComSql.Fields("ReturnType").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    dblBasePrice = rsComSql_1.Fields("MarketPrice").Value
                    dblPerStonePrice = rsComSql_1.Fields("StonePrice").Value
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
                End If
                rsComSql_1 = Nothing

                If dblImportNo = 0 Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblImport WHERE SupParcelNo = '" & strSupParNo & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        dblImportNo = rsComSql_1.Fields("ImportNo").Value
                    End If
                    rsComSql_1 = Nothing
                End If

                'If strRight(rsComSql.Fields("ParNo").Value, 1) = "S" Then
                '    dblBasePrice = rsComSql.Fields("BasePrice").Value
                'ElseIf strRight(rsComSql.Fields("ParNo").Value, 1) = "C" Then
                '    If blnOld = True Then
                '        dblBasePrice = Math.Round((rsComSql.Fields("Pcs").Value * dblPerStonePrice) / rsComSql.Fields("Cts").Value, 2)
                '    Else
                '        If Mid(rsComSql.Fields("ReturnType").Value, 1, 1) = "S" And Mid(rsComSql.Fields("ReturnType").Value, 1, 3) <> "SRW" Then
                '            dblBasePrice = rsComSql.Fields("BasePrice").Value
                '        Else
                '            If Mid(rsComSql.Fields("ReturnType").Value, 1, 1) = "S" Then
                '                dblBasePrice = Math.Round((rsComSql.Fields("Pcs").Value * dblPerStonePrice) / rsComSql.Fields("Cts").Value, 2)
                '            Else
                '                dblBasePrice = rsComSql.Fields("BasePrice").Value
                '            End If
                '        End If
                '    End If
                'Else
                '    If Mid(rsComSql.Fields("ReturnType").Value, 1, 1) = "S" Then
                '        'dblBasePrice = Math.Round((rsComSql.Fields("Pcs").Value * dblPerStonePrice) / rsComSql.Fields("Cts").Value, 2)
                '        dblBasePrice = rsComSql.Fields("BasePrice").Value
                '    Else
                '        dblBasePrice = rsComSql.Fields("BasePrice").Value
                '    End If

                '    If dblBasePrice = 0 Then
                '        dblBasePrice = dblInvPrice
                '    End If
                'End If

                'If Mid(rsComSql.Fields("ReturnType").Value, 1, 3) = "SRW" And strRight(rsComSql.Fields("ReturnType").Value, 1) = "U" Then
                '    dblBasePrice = rsComSql.Fields("BasePrice").Value
                'End If

                If Mid(rsComSql.Fields("ReturnType").Value, 1, 1) = "S" Or Mid(rsComSql.Fields("ReturnType").Value, 7, 1) = "R" Or Mid(rsComSql.Fields("ReturnType").Value, 7, 1) = "S" Then
                    dblBasePrice = rsComSql.Fields("BasePrice").Value
                Else
                    dblBasePrice = dblBasePrice
                End If

                intIssPcs = 0
                dblIssCts = 0
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts " & _
                                "FROM tblExpRghTypes " & _
                                "WHERE (Department = '" & rsComSql.Fields("Department").Value & "') AND (PktNo = '" & rsComSql.Fields("PktNo").Value & "') AND " & _
                                    "(ParNo = '" & rsComSql.Fields("ParNo").Value & "') AND (Assortment = '" & rsComSql.Fields("ReturnType").Value & "') AND (Size = 2)", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                        intIssPcs = rsComSql_1.Fields("Pcs").Value
                        dblIssCts = rsComSql_1.Fields("Cts").Value
                    End If
                End If
                rsComSql_1 = Nothing

                flxDetails.Rows.Add(rsComSql.Fields("Department").Value,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    strOrgAssort,
                                    rsComSql.Fields("ReturnType").Value,
                                    rsComSql.Fields("Pcs").Value - intIssPcs,
                                    Format(Math.Round(rsComSql.Fields("Cts").Value - dblIssCts, 3), "#0.000"),
                                    Format(Math.Round(rsComSql.Fields("Cts").Value - dblIssCts, 2), "#0.000"),
                                    Format(dblBasePrice, "#0.00"),
                                    Format(dblInvPrice, "#0.00"),
                                    Format(dblInvPrice, "#0.00"),
                                    dblImportNo,
                                    False,
                                    strCategory,
                                    rsComSql.Fields("ID").Value)

                rsComSql.MoveNext()
                dblRecord = dblRecord + 1
                ExpProgress.Value = dblRecord
                Application.DoEvents()
            End While
        Else
            MsgBox("No Records to Send", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing

        ExpProgress.Visible = False

    End Sub

    Private Sub frm_ExpSizingTransfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        ExpProgress.Visible = False
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        Load_SizingDetails()
        chkSelect.Checked = False
        txtTotalPcs.Text = "0"
        txtTotalCts.Text = "0"
        txtTotalCts2.Text = "0"
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                If optRough.Checked = True Then
                    If Mid(flxDetails.Item(4, intRow).Value, 1, 2) = "AR" Or Mid(flxDetails.Item(4, intRow).Value, 1, 2) = "SR" Or Mid(flxDetails.Item(4, intRow).Value, 1, 2) = "SS" Or Mid(flxDetails.Item(4, intRow).Value, 7, 1) = "S" Or Mid(flxDetails.Item(4, intRow).Value, 7, 1) = "R" Then
                        flxDetails.Item(12, intRow).Value = True
                    End If
                End If
                If optPolish.Checked = True Then
                    If Mid(flxDetails.Item(4, intRow).Value, 1, 2) <> "AR" And Mid(flxDetails.Item(4, intRow).Value, 1, 2) <> "SR" And Mid(flxDetails.Item(4, intRow).Value, 1, 2) <> "SS" And Mid(flxDetails.Item(4, intRow).Value, 7, 1) <> "S" And Mid(flxDetails.Item(4, intRow).Value, 7, 1) <> "R" Then
                        flxDetails.Item(12, intRow).Value = True
                    End If
                End If
                If optAll.Checked = True Then
                    flxDetails.Item(12, intRow).Value = True
                End If
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(12, intRow).Value = False
            Next
        End If
        txtTotalPcs.Text = CalTotalPcs()
        txtTotalCts.Text = CalTotalCts()
        txtTotalCts2.Text = CalTotalCts2()
    End Sub

    Private Function CalTotalPcs() As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Rows(intRow).Cells(12).EditedFormattedValue = True Then
                CalTotalPcs = CalTotalPcs + CInt(flxDetails.Item(5, intRow).Value)
            End If
        Next
        Return CalTotalPcs
    End Function

    Private Function CalTotalCts() As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Rows(intRow).Cells(12).EditedFormattedValue = True Then
                CalTotalCts = CalTotalCts + CDbl(flxDetails.Item(6, intRow).Value)
            End If
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
        Return CalTotalCts
    End Function

    Private Function CalTotalCts2() As Double
        Dim intRow As Integer

        CalTotalCts2 = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Rows(intRow).Cells(12).EditedFormattedValue = True Then
                CalTotalCts2 = CalTotalCts2 + CDbl(flxDetails.Item(7, intRow).Value)
            End If
        Next
        CalTotalCts2 = Math.Round(CalTotalCts2, 3)
        Return CalTotalCts2
    End Function

    Private Sub cmdCalc_Click(sender As Object, e As EventArgs) Handles cmdCalc.Click
        txtTotalPcs.Text = CalTotalPcs()
        txtTotalCts.Text = CalTotalCts()
        txtTotalCts2.Text = CalTotalCts2()
    End Sub

    Private Sub AddToStock()
        Dim PBResponse
        Dim intRow As Integer
        Dim blnSave As Boolean
        Dim dblDiffCts As Double
        Dim dblDiffCtsTemp As Double
        Dim dblPerStoneCts As Double

        blnSave = False
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(12, intRow).Value = True Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT Assortment FROM tblAssortList WHERE Assortment = '" & flxDetails.Item(4, intRow).Value & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        'MsgBoxGT("Invalid Assortment - " & flxDetails.TextMatrix(intRow, 4), vbInformation + vbOKOnly, Me.Caption)
                        'Exit Sub
                    Else
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & flxDetails.Item(4, intRow).Value & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then

                        Else
                            MsgBox("Invalid Assortment - " & flxDetails.Item(4, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                        rsComSql_1 = Nothing
                    End If
                    rsComSql = Nothing

                    If Len(flxDetails.Item(7, intRow).Value) = 0 Then
                        MsgBox("Invalid Act Cts - " & flxDetails.Item(4, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If IsNumeric(flxDetails.Item(7, intRow).Value) = False Then
                        MsgBox("Invalid Act Cts - " & flxDetails.Item(4, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If CDbl(flxDetails.Item(7, intRow).Value) <= 0 Then
                        MsgBox("Invalid Act Cts - " & flxDetails.Item(4, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If

                    dblDiffCtsTemp = Math.Round(CDbl(flxDetails.Item(7, intRow).Value) - CDbl(flxDetails.Item(6, intRow).Value), 3)
                    If dblDiffCtsTemp <> 0 Then
                        dblPerStoneCts = Math.Round(CDbl(flxDetails.Item(6, intRow).Value) / CDbl(flxDetails.Item(5, intRow).Value), 3)

                        If dblDiffCtsTemp > 0 Then
                            If dblDiffCtsTemp > dblPerStoneCts * 2 Then
                                MsgBox("Invalid Act Cts - " & flxDetails.Item(4, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                Exit Sub
                            End If
                        Else
                            If dblDiffCtsTemp < dblPerStoneCts * -0.5 Then
                                MsgBox("Invalid Act Cts - " & flxDetails.Item(4, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            Next

            dtpToday = GetToday()
            dblDiffCts = 0
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(12, intRow).Value = True Then
                    blnSave = True
                    dblDiffCts = Math.Round(CDbl(flxDetails.Item(7, intRow).Value) - CDbl(flxDetails.Item(6, intRow).Value), 3)

                    AdoCN.Execute("UPDATE tblExpSizingTypes SET OK = 4, BasePrice = '" & CDbl(flxDetails.Item(8, intRow).Value) & "', " & _
                                    "DiffCts = " & dblDiffCts & ", SendDate = '" & Format(dtpToday, "MM/dd/yyyy") & "', SendUser = '" & PBUser_EmpNo & "'," & _
                                    "SendTime = '" & Format(Date.Now, "HH:mm:ss") & "' " & _
                                  "WHERE ID = " & CDbl(flxDetails.Item(14, intRow).Value) & "")

                End If
            Next
            If blnSave = True Then
                MsgBox("Added to the APCU Transfer Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Else
                MsgBox("No Records to Add", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            flxDetails.Rows.Clear()
            txtTotalPcs.Text = "0"
            txtTotalCts.Text = "0"
            txtTotalCts2.Text = "0"
            chkSelect.Checked = False
        End If
    End Sub

    Private Sub cmdTrf_Click(sender As Object, e As EventArgs) Handles cmdTrf.Click
        AddToStock()
    End Sub

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 12 Then
            txtTotalPcs.Text = CalTotalPcs()
            txtTotalCts.Text = CalTotalCts()
            txtTotalCts2.Text = CalTotalCts2()
        End If
    End Sub

    Private Sub flxDetails_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellValueChanged
        txtTotalCts2.Text = CalTotalCts2()
    End Sub
End Class