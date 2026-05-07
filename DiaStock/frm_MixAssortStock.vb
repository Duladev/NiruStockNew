
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_MixAssortStock

    Private Sub ClearFields()
        txtImportNo.Text = ""
        cmbOrgAssort.Items.Clear()
        cmbOrgAssort.Text = ""
        txtFilePath.Text = ""
        txtTotPcs.Text = ""
        txtTotCts.Text = ""
        txtItemCost.Text = ""
        txtInvValue.Text = ""
        txtAssortment.Text = ""
        flxDetails.Rows.Clear()
        txtPcs.Text = ""
        txtCts.Text = ""
        txtAddVal.Text = ""
        txtOldParNo.Text = ""
        txtBaseVal.Text = ""
        txtRemarks.Text = ""
    End Sub

    Private Sub txtImportNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtImportNo.KeyPress
        IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 And Len(txtImportNo.Text) > 0 Then
            Load_Import(CDbl(txtImportNo.Text))
        End If
    End Sub

    Private Sub Load_Import(ByVal dblImportNo As Double)
        cmbOrgAssort.Items.Clear()
        cmbOrgAssort.Text = ""
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT SupParcelNo FROM tblImport WHERE ImportNo = " & dblImportNo & " GROUP BY SupParcelNo ORDER BY SupParcelNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbOrgAssort.Items.Add(rsComSql.Fields("SupParcelNo").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmbOrgAssort_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbOrgAssort.SelectedIndexChanged
        If cmbOrgAssort.Text <> "" Then
            flxDetails.Rows.Clear()
            txtPcs.Text = ""
            txtCts.Text = ""
            txtAddVal.Text = ""
            txtBaseVal.Text = ""

            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT AssortmentNo, SUM(ACTPcs) AS ACTPcs, SUM(ACtCts) AS ACtCts, MAX(ImpPrice) AS ItemCost, SUM(ACtCts * ImpPrice) AS Value, Remarks " & _
                            "FROM tblImport WHERE ImportNo = " & CDbl(txtImportNo.Text) & " AND SupParcelNo = '" & cmbOrgAssort.Text & "' GROUP BY AssortmentNo, Remarks", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                txtTotPcs.Text = rsComSql_1.Fields("ActPcs").Value
                txtTotCts.Text = Format(rsComSql_1.Fields("ActCts").Value, "#0.000")
                txtItemCost.Text = rsComSql_1.Fields("ItemCost").Value
                txtAssortment.Text = rsComSql_1.Fields("AssortmentNo").Value
                txtInvValue.Text = Format(rsComSql_1.Fields("Value").Value, "#0.00")
                txtRemarks.Text = rsComSql_1.Fields("Remarks").Value
            End If
            rsComSql_1 = Nothing
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

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

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Load_Excel()
    End Sub

    Private Sub Load_Excel()
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim intRow As Integer
        Dim dblPrice As Double
        Dim strAssortment As String

        If txtFilePath.Text = "" Then Exit Sub
        If Len(Dir(txtFilePath.Text)) > 0 Then
            xlApp = New Excel.Application
            xlWorkBook = xlApp.Workbooks.Open(txtFilePath.Text)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            flxDetails.Rows.Clear()

            For intRow = 2 To 10000
                If Len(xlWorkSheet.Cells(intRow, 1).value) <> 0 Then
                    dblPrice = 0

                    strAssortment = Trim(xlWorkSheet.Cells(intRow, 2).Value)

                    If chkSpecial.Checked = True Then
                        dblPrice = Trim(xlWorkSheet.Cells(intRow, 5).Value)
                    Else
                        If Mid(strAssortment, 1, 1) = "A" Or Mid(strAssortment, 1, 1) = "V" Then
                            rsComSql = New ADODB.Recordset
                            rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & strAssortment & "'", AdoCN, 1, 1)
                            If rsComSql.RecordCount Then
                                dblPrice = rsComSql.Fields("MarketPrice").Value
                            End If
                            rsComSql = Nothing

                        End If
                    End If

                    'If Mid(strAssortment, 1, 2) = "VM" Or Mid(strAssortment, 1, 2) = "VP" Then
                    '    dblPrice = CDbl(txtItemCost.Text)
                    'Else
                    '    If Mid(strAssortment, 1, 1) = "A" Or Mid(strAssortment, 1, 1) = "V" Or Mid(strAssortment, 1, 1) = "4" Or Mid(strAssortment, 1, 1) = "3" Then
                    '        rsComSql = New ADODB.Recordset
                    '        rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & strAssortment & "'", AdoCN, 1, 1)
                    '        If rsComSql.RecordCount Then
                    '            If Mid(strAssortment, 1, 2) = "AE" Then
                    '                dblPrice = Trim(xlWorkSheet.Cells(intRow, 5).Value)
                    '            Else
                    '                dblPrice = rsComSql.Fields("MarketPrice").Value
                    '            End If
                    '        Else
                    '            MsgBox("Invalid Assortment - " & strAssortment, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    '            Exit Sub
                    '        End If
                    '        rsComSql = Nothing

                    '    ElseIf Mid(strAssortment, 1, 1) = "S" Then
                    '        rsComSql = New ADODB.Recordset
                    '        rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & strAssortment & "'", AdoCN, 1, 1)
                    '        If rsComSql.RecordCount Then
                    '            dblPrice = Math.Round((rsComSql.Fields("StonePrice").Value * CDbl(xlWorkSheet.Cells(intRow, 3).Value)) / CDbl(Trim(xlWorkSheet.Cells(intRow, 4).Value)), 3)
                    '        Else
                    '            MsgBox("Invalid Assortment - " & strAssortment, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    '            Exit Sub
                    '        End If
                    '        rsComSql = Nothing

                    '    Else
                    '        rsComSql = New ADODB.Recordset
                    '        rsComSql.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & strAssortment & "'", AdoCN, 1, 1)
                    '        If rsComSql.RecordCount Then
                    '            dblPrice = rsComSql.Fields("ListCost").Value
                    '        Else
                    '            MsgBox("Invalid Assortment - " & strAssortment, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    '            Exit Sub
                    '        End If
                    '        rsComSql = Nothing
                    '    End If
                    'End If

                    If dblPrice = 0 Then
                        dblPrice = CDbl(txtItemCost.Text)
                    End If

                    flxDetails.Rows.Add(cmbOrgAssort.Text,
                                        strAssortment,
                                        Trim(xlWorkSheet.Cells(intRow, 3).Value),
                                        Math.Round(CDbl(Trim(xlWorkSheet.Cells(intRow, 4).Value)), 3),
                                        Format(dblPrice, "#0.00"),
                                        Format(CDbl(Trim(xlWorkSheet.Cells(intRow, 4).Value)) * dblPrice, "#0.00"),
                                        Format(CDbl(Trim(txtItemCost.Text)), "#0.00"), "0", "0")

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
            txtBaseVal.Text = CalTotalValue(flxDetails)

            Process()
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

    Private Function CalTotalValue(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalValue = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalValue = CalTotalValue + Val(flxSample.Item(5, intRow).Value)
        Next
        CalTotalValue = Math.Round(CalTotalValue, 2)
    End Function

    Private Sub Process()
        Dim intRow As Integer
        Dim dblTotBase As Double
        Dim dblTotInv As Double
        Dim dblAdjValue As Double
        Dim dblAdjCost As Double

        If flxDetails.Rows.Count >= 1 Then
            If CInt(txtPcs.Text) <> CInt(txtTotPcs.Text) Then
                MsgBox("Pcs not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(txtCts.Text) <> CDbl(txtTotCts.Text) Then
                MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                'Exit Sub
            End If

            dblTotBase = 0
            For intRow = 0 To flxDetails.Rows.Count - 1
                dblTotBase = dblTotBase + (CDbl(flxDetails.Item(3, intRow).Value) * CDbl(flxDetails.Item(4, intRow).Value))
            Next

            dblTotInv = CDbl(txtInvValue.Text)

            For intRow = 0 To flxDetails.Rows.Count - 1
                If CDbl(flxDetails.Item(3, intRow).Value) * CDbl(flxDetails.Item(4, intRow).Value) <> 0 Then
                    dblAdjValue = (dblTotInv / dblTotBase) * (CDbl(flxDetails.Item(3, intRow).Value) * CDbl(flxDetails.Item(4, intRow).Value))
                    flxDetails.Item(7, intRow).Value = Format(Math.Round(dblAdjValue, 2), "#0.00")
                    dblAdjCost = dblAdjValue / CDbl(flxDetails.Item(3, intRow).Value)
                    flxDetails.Item(8, intRow).Value = Format(Math.Round(dblAdjCost, 2), "#0.00")
                End If
            Next
        End If
        CalculateAdjVal()
    End Sub

    Private Sub CalculateAdjVal()
        Dim intRow As Integer

        txtAddVal.Text = "0"
        For intRow = 0 To flxDetails.Rows.Count - 1
            txtAddVal.Text = CDbl(txtAddVal.Text) + CDbl(flxDetails.Item(7, intRow).Value)
        Next
        txtAddVal.Text = Format(CDbl(txtAddVal.Text), "#0.00")
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub SaveBulk()
        Dim intRow As Integer
        Dim strParcelNo As String
        Dim dblBasePrice As Double
        Dim blnSave As Boolean

        blnSave = False

        If CInt(txtPcs.Text) <> CInt(txtTotPcs.Text) Then
            MsgBox("Pcs not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If CDbl(txtCts.Text) <> CDbl(txtTotCts.Text) Then
            MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If Math.Round(Val(txtAddVal.Text) - Val(txtInvValue.Text), 2) > 2 Or Math.Round(Val(txtAddVal.Text) - Val(txtInvValue.Text), 2) < -2 Then
            MsgBox("Values not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT SupParcelNo, LotNo, ACTPcs, ACtCts FROM tblImport WHERE ImportNo = '" & txtImportNo.Text & "' AND SupParcelNo LIKE 'A%' ORDER BY SupParcelNo", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                blnSave = True

                strParcelNo = rsComSql_1.Fields("SupParcelNo").Value
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblExpPacket WHERE Department = 'Mix' AND ParNo = '" & strParcelNo & "' AND AMS2 = 1 AND YAH = 1", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    MsgBox("Invalid Parcel. Please check AMS2/YAHUDA verification - " & strParcelNo, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblExpIssues WHERE Department = 'Mix' AND ParNo = '" & strParcelNo & "' AND PktNo = 'N001' AND Sec = 1", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing

                'Update packing List Number
                AdoCN.Execute("UPDATE tblExpPacket SET PktType = '" & txtOldParNo.Text & "' WHERE Department = 'Mix' AND ParNo = '" & strParcelNo & "'")

                '====================================
                'Fluorescent Checking Issues
                AdoCN.Execute("INSERT INTO tblExpIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                              "VALUES('Mix','" & strParcelNo & "','N001',1,'D08411'," & rsComSql_1.Fields("ACTPcs").Value & "," & rsComSql_1.Fields("ACtCts").Value & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "')")

                'Fluorescent Checking Returns
                AdoCN.Execute("INSERT INTO tblExpReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                              "VALUES('Mix','" & strParcelNo & "','N001',1,'D08411'," & rsComSql_1.Fields("ACTPcs").Value & "," & rsComSql_1.Fields("ACtCts").Value & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0)")

                'Fluorescent Checking Return Details
                AdoCN.Execute("INSERT INTO tblExpReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                              "VALUES('Mix','" & strParcelNo & "','N001',1,'NONE'," & rsComSql_1.Fields("ACTPcs").Value & "," & rsComSql_1.Fields("ACtCts").Value & ")")

                '====================================
                'Color Sorting Issues
                AdoCN.Execute("INSERT INTO tblExpIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                              "VALUES('Mix','" & strParcelNo & "','N001',2,'D08411'," & rsComSql_1.Fields("ACTPcs").Value & "," & rsComSql_1.Fields("ACtCts").Value & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "')")

                'Color Sorting Returns
                AdoCN.Execute("INSERT INTO tblExpReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                              "VALUES('Mix','" & strParcelNo & "','N001',2,'D08411'," & rsComSql_1.Fields("ACTPcs").Value & "," & rsComSql_1.Fields("ACtCts").Value & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0)")

                'Color Sorting Return Details
                AdoCN.Execute("INSERT INTO tblExpReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                              "VALUES('Mix','" & strParcelNo & "','N001',2,'COLOR'," & rsComSql_1.Fields("ACTPcs").Value & "," & rsComSql_1.Fields("ACtCts").Value & ")")

                '====================================
                'Clarity Checking Issues
                AdoCN.Execute("INSERT INTO tblExpIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                              "VALUES('Mix','" & strParcelNo & "','N001',3,'D08411'," & rsComSql_1.Fields("ACTPcs").Value & "," & rsComSql_1.Fields("ACtCts").Value & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "')")

                'Clarity Checking Returns
                AdoCN.Execute("INSERT INTO tblExpReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                              "VALUES('Mix','" & strParcelNo & "','N001',3,'D08411'," & rsComSql_1.Fields("ACTPcs").Value & "," & rsComSql_1.Fields("ACtCts").Value & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0)")

                'Clarity Checking Return Details
                AdoCN.Execute("INSERT INTO tblExpReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                              "VALUES('Mix','" & strParcelNo & "','N001',3,'COLOR'," & rsComSql_1.Fields("ACTPcs").Value & "," & rsComSql_1.Fields("ACtCts").Value & ")")

                '====================================
                'Sizing Packet
                AdoCN.Execute("INSERT INTO tblExpSizingPacket(Department, ParNo, PktNo, PktPcs, PktCts, ReturnType) " & _
                              "VALUES('Mix','" & strParcelNo & "','H001'," & rsComSql_1.Fields("ACTPcs").Value & "," & rsComSql_1.Fields("ACtCts").Value & ",'COLOR')")

                'Sizing Issues
                AdoCN.Execute("INSERT INTO tblExpSizingIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                              "VALUES('Mix','" & strParcelNo & "','H001',1,'D08411'," & rsComSql_1.Fields("ACTPcs").Value & "," & rsComSql_1.Fields("ACtCts").Value & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "')")

                'Sizing Returns
                AdoCN.Execute("INSERT INTO tblExpSizingReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                              "VALUES('Mix','" & strParcelNo & "','H001',1,'D08411', " & rsComSql_1.Fields("ACTPcs").Value & ", " & rsComSql_1.Fields("ACtCts").Value & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0)")

                rsComSql_1.MoveNext()
            End While
        End If
        rsComSql_1 = Nothing

        For intRow = 0 To flxDetails.Rows.Count - 1
            strParcelNo = flxDetails.Item(0, intRow).Value
            dblBasePrice = CDbl(flxDetails.Item(4, intRow).Value)
            If optPolish.Checked = True Then
                If CDbl(txtAddVal.Text) > CDbl(txtBaseVal.Text) Then
                    dblBasePrice = CDbl(flxDetails.Item(8, intRow).Value)
                End If
            End If
            AdoCN.Execute("INSERT INTO tblExpSizingTypes(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts,OK,BasePrice,EstCts,DiaCost) " & _
                          "VALUES('Mix','" & strParcelNo & "','H001',1,'" & UCase(flxDetails.Item(1, intRow).Value) & "'," & CInt(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & ",0," & dblBasePrice & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(8, intRow).Value) & ")")
        Next

        If blnSave = True Then
            MsgBox("Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
        End If
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean
        Dim strParcelNo As String
        Dim dblCurCts As Double
        Dim dblCurValue As Double
        Dim dblAvgPrice As Double
        Dim dblBasePrice As Double

        blnSave = False

        strParcelNo = cmbOrgAssort.Text

        For intRow = 0 To flxDetails.Rows.Count - 1
            If Not IsNumeric(flxDetails.Item(3, intRow).Value) = True Then
                MsgBox("Invalid Cts - " & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        txtCts.Text = CalTotalCts(flxDetails)

        If strDBName <> "DiaShare" Then
            If Math.Round(Val(txtAddVal.Text) - Val(txtInvValue.Text), 2) > 2 Or Math.Round(Val(txtAddVal.Text) - Val(txtInvValue.Text), 2) < -2 Then
                MsgBox("Values not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        End If

        If CInt(txtPcs.Text) <> CInt(txtTotPcs.Text) Then
            MsgBox("Pcs not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If CDbl(txtCts.Text) <> CDbl(txtTotCts.Text) Then
            MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblExpPacket WHERE Department = 'Mix' AND ParNo = '" & strParcelNo & "' AND AMS2 = 1 AND YAH = 1", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Parcel. Please check AMS2/YAHUDA verification", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblExpIssues WHERE Department = 'Mix' AND ParNo = '" & strParcelNo & "' AND PktNo = 'N001' AND Sec = 1", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        'Update packing List Number
        AdoCN.Execute("UPDATE tblExpPacket SET PktType = '" & txtOldParNo.Text & "' WHERE Department = 'Mix' AND ParNo = '" & strParcelNo & "'")

        '====================================
        'Fluorescent Checking Issues
        AdoCN.Execute("INSERT INTO tblExpIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                      "VALUES('Mix','" & strParcelNo & "','N001',1,'D08411'," & CInt(txtTotPcs.Text) & "," & CDbl(txtTotCts.Text) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "')")

        'Fluorescent Checking Returns
        AdoCN.Execute("INSERT INTO tblExpReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                      "VALUES('Mix','" & strParcelNo & "','N001',1,'D08411'," & CInt(txtTotPcs.Text) & "," & CDbl(txtTotCts.Text) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0)")

        'Fluorescent Checking Return Details
        AdoCN.Execute("INSERT INTO tblExpReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                      "VALUES('Mix','" & strParcelNo & "','N001',1,'NONE'," & CInt(txtTotPcs.Text) & "," & CDbl(txtTotCts.Text) & ")")

        '====================================
        'Color Sorting Issues
        AdoCN.Execute("INSERT INTO tblExpIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                      "VALUES('Mix','" & strParcelNo & "','N001',2,'D08411'," & CInt(txtTotPcs.Text) & "," & CDbl(txtTotCts.Text) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "')")

        'Color Sorting Returns
        AdoCN.Execute("INSERT INTO tblExpReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                      "VALUES('Mix','" & strParcelNo & "','N001',2,'D08411'," & CInt(txtTotPcs.Text) & "," & CDbl(txtTotCts.Text) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0)")

        'Color Sorting Return Details
        AdoCN.Execute("INSERT INTO tblExpReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                      "VALUES('Mix','" & strParcelNo & "','N001',2,'COLOR'," & CInt(txtTotPcs.Text) & "," & CDbl(txtTotCts.Text) & ")")

        '====================================
        'Clarity Checking Issues
        AdoCN.Execute("INSERT INTO tblExpIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                      "VALUES('Mix','" & strParcelNo & "','N001',3,'D08411'," & CInt(txtTotPcs.Text) & "," & CDbl(txtTotCts.Text) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "')")

        'Clarity Checking Returns
        AdoCN.Execute("INSERT INTO tblExpReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                      "VALUES('Mix','" & strParcelNo & "','N001',3,'D08411'," & CInt(txtTotPcs.Text) & "," & CDbl(txtTotCts.Text) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0)")

        'Clarity Checking Return Details
        AdoCN.Execute("INSERT INTO tblExpReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                      "VALUES('Mix','" & strParcelNo & "','N001',3,'COLOR'," & CInt(txtTotPcs.Text) & "," & CDbl(txtTotCts.Text) & ")")

        '====================================
        'Sizing Packet
        AdoCN.Execute("INSERT INTO tblExpSizingPacket(Department, ParNo, PktNo, PktPcs, PktCts, ReturnType) " & _
                      "VALUES('Mix','" & strParcelNo & "','H001'," & CInt(txtTotPcs.Text) & "," & CDbl(txtTotCts.Text) & ",'COLOR')")

        'Sizing Issues
        AdoCN.Execute("INSERT INTO tblExpSizingIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                      "VALUES('Mix','" & strParcelNo & "','H001',1,'D08411'," & CInt(txtTotPcs.Text) & "," & CDbl(txtTotCts.Text) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "')")

        'Sizing Returns
        AdoCN.Execute("INSERT INTO tblExpSizingReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                      "VALUES('Mix','" & strParcelNo & "','H001',1,'D08411', " & CInt(txtTotPcs.Text) & ", " & CDbl(txtTotCts.Text) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0)")

        'Sizing Types
        dblCurCts = 0
        dblCurValue = 0
        dblAvgPrice = 0
        dblBasePrice = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            blnSave = True
            If Mid(UCase(flxDetails.Item(1, intRow).Value), 1, 2) = "VM" Or Mid(UCase(flxDetails.Item(1, intRow).Value), 1, 2) = "VP" Then
                'Get the Weighted Average
                dblCurCts = 0
                dblCurValue = 0
                dblAvgPrice = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT dbo.VW_MixAssortInOutNew.Assortment, dbo.VW_MixAssortInOutNew.InCts - dbo.VW_MixAssortInOutNew.OutCts AS BalCts, " & _
                                "(dbo.VW_MixAssortInOutNew.InCts - dbo.VW_MixAssortInOutNew.OutCts) * dbo.tblAssortList.MarketPrice AS Value " & _
                              "FROM dbo.VW_MixAssortInOutNew INNER JOIN dbo.tblAssortList ON dbo.VW_MixAssortInOutNew.Assortment = dbo.tblAssortList.Assortment " & _
                              "WHERE (dbo.VW_MixAssortInOutNew.Assortment = '" & flxDetails.Item(1, intRow).Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("BalCts").Value) Then
                        If rsComSql.Fields("BalCts").Value > 0 Then
                            dblCurCts = rsComSql.Fields("BalCts").Value
                            dblCurValue = rsComSql.Fields("Value").Value
                        End If
                    End If
                End If              
                rsComSql = Nothing

                dblAvgPrice = (dblCurValue + (CDbl(flxDetails.Item(3, intRow).Value) * CDbl(flxDetails.Item(4, intRow).Value))) / (dblCurCts + CDbl(flxDetails.Item(3, intRow).Value))
                dblAvgPrice = Math.Round(dblAvgPrice, 2)

                AdoCN.Execute("UPDATE tblAssortList SET MarketPrice = " & dblAvgPrice & " WHERE Assortment = '" & flxDetails.Item(1, intRow).Value & "'")
            End If
            dblBasePrice = CDbl(flxDetails.Item(4, intRow).Value)
            'If optPolish.Checked = True Then
            '    If CDbl(txtAddVal.Text) > CDbl(txtBaseVal.Text) Then
            '        dblBasePrice = CDbl(flxDetails.Item(8, intRow).Value)
            '    End If
            'End If
            AdoCN.Execute("INSERT INTO tblExpSizingTypes(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts,OK,BasePrice,EstCts,DiaCost) " & _
                          "VALUES('Mix','" & strParcelNo & "','H001',1,'" & UCase(flxDetails.Item(1, intRow).Value) & "'," & CInt(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & ",0," & dblBasePrice & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(8, intRow).Value) & ")")
        Next
        If blnSave = True Then
            MsgBox("Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
        End If

    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If chkBulk.Checked = True Then
            SaveBulk()
        Else
            If chkBulkPolish.Checked = True Then
                SaveBulk2()
            Else
                Save()
            End If
        End If
    End Sub

    Private Sub SaveBulk2()
        Dim intRow As Integer
        Dim strParcelNo As String
        Dim dblBasePrice As Double
        Dim blnSave As Boolean

        blnSave = False

        If CInt(txtPcs.Text) <> CInt(txtTotPcs.Text) Then
            MsgBox("Pcs not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If CDbl(txtCts.Text) <> CDbl(txtTotCts.Text) Then
            MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If Math.Round(Val(txtAddVal.Text) - Val(txtInvValue.Text), 2) > 2 Or Math.Round(Val(txtAddVal.Text) - Val(txtInvValue.Text), 2) < -2 Then
            MsgBox("Values not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.tblImport.SupParcelNo, dbo.tblImport.InvoiceDate, dbo.tblImport.AssortmentNo, dbo.tblImport.LotNo, dbo.tblImport.ImportNo, dbo.tblImport.ItemCost, dbo.tblImport.Article, dbo.tblImport.ACTPcs, dbo.tblImport.ACtCts " & _
                        "FROM dbo.tblImport INNER JOIN dbo.tblExpPacket ON dbo.tblImport.SupParcelNo = dbo.tblExpPacket.ParNo LEFT OUTER JOIN " & _
                            "dbo.tblExpIssues ON dbo.tblImport.SupParcelNo = dbo.tblExpIssues.ParNo " & _
                        "WHERE (dbo.tblImport.SupParcelNo LIKE N'H%') AND (dbo.tblExpIssues.ParNo IS NULL) " & _
                        "ORDER BY dbo.tblImport.SupParcelNo", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                blnSave = True
                strParcelNo = rsComSql_1.Fields("SupParcelNo").Value

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblExpPacket WHERE Department = 'Mix' AND ParNo = '" & strParcelNo & "' AND AMS2 = 1 AND YAH = 1", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    MsgBox("Invalid Parcel. Please check AMS2/YAHUDA verification - " & strParcelNo, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblExpIssues WHERE Department = 'Mix' AND ParNo = '" & strParcelNo & "' AND PktNo = 'N001' AND Sec = 1", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing

                '====================================
                'Fluorescent Checking Issues
                AdoCN.Execute("INSERT INTO tblExpIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                              "VALUES('Mix','" & strParcelNo & "','N001',1,'D08411'," & rsComSql_1.Fields("ACTPcs").Value & "," & rsComSql_1.Fields("ACtCts").Value & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "')")

                'Fluorescent Checking Returns
                AdoCN.Execute("INSERT INTO tblExpReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                              "VALUES('Mix','" & strParcelNo & "','N001',1,'D08411'," & rsComSql_1.Fields("ACTPcs").Value & "," & rsComSql_1.Fields("ACtCts").Value & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0)")

                'Fluorescent Checking Return Details
                AdoCN.Execute("INSERT INTO tblExpReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                              "VALUES('Mix','" & strParcelNo & "','N001',1,'NONE'," & rsComSql_1.Fields("ACTPcs").Value & "," & rsComSql_1.Fields("ACtCts").Value & ")")

                '====================================
                'Color Sorting Issues
                AdoCN.Execute("INSERT INTO tblExpIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                              "VALUES('Mix','" & strParcelNo & "','N001',2,'D08411'," & rsComSql_1.Fields("ACTPcs").Value & "," & rsComSql_1.Fields("ACtCts").Value & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "')")

                'Color Sorting Returns
                AdoCN.Execute("INSERT INTO tblExpReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                              "VALUES('Mix','" & strParcelNo & "','N001',2,'D08411'," & rsComSql_1.Fields("ACTPcs").Value & "," & rsComSql_1.Fields("ACtCts").Value & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0)")

                'Color Sorting Return Details
                AdoCN.Execute("INSERT INTO tblExpReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                              "VALUES('Mix','" & strParcelNo & "','N001',2,'COLOR'," & rsComSql_1.Fields("ACTPcs").Value & "," & rsComSql_1.Fields("ACtCts").Value & ")")

                '====================================
                'Clarity Checking Issues
                AdoCN.Execute("INSERT INTO tblExpIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                              "VALUES('Mix','" & strParcelNo & "','N001',3,'D08411'," & rsComSql_1.Fields("ACTPcs").Value & "," & rsComSql_1.Fields("ACtCts").Value & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "')")

                'Clarity Checking Returns
                AdoCN.Execute("INSERT INTO tblExpReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                              "VALUES('Mix','" & strParcelNo & "','N001',3,'D08411'," & rsComSql_1.Fields("ACTPcs").Value & "," & rsComSql_1.Fields("ACtCts").Value & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0)")

                'Clarity Checking Return Details
                AdoCN.Execute("INSERT INTO tblExpReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                              "VALUES('Mix','" & strParcelNo & "','N001',3,'COLOR'," & rsComSql_1.Fields("ACTPcs").Value & "," & rsComSql_1.Fields("ACtCts").Value & ")")

                '====================================
                'Sizing Packet
                AdoCN.Execute("INSERT INTO tblExpSizingPacket(Department, ParNo, PktNo, PktPcs, PktCts, ReturnType) " & _
                              "VALUES('Mix','" & strParcelNo & "','H001'," & rsComSql_1.Fields("ACTPcs").Value & "," & rsComSql_1.Fields("ACtCts").Value & ",'COLOR')")

                'Sizing Issues
                AdoCN.Execute("INSERT INTO tblExpSizingIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                              "VALUES('Mix','" & strParcelNo & "','H001',1,'D08411'," & rsComSql_1.Fields("ACTPcs").Value & "," & rsComSql_1.Fields("ACtCts").Value & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "')")

                'Sizing Returns
                AdoCN.Execute("INSERT INTO tblExpSizingReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                              "VALUES('Mix','" & strParcelNo & "','H001',1,'D08411', " & rsComSql_1.Fields("ACTPcs").Value & ", " & rsComSql_1.Fields("ACtCts").Value & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0,0)")

                rsComSql_1.MoveNext()
            End While
        End If

        For intRow = 0 To flxDetails.Rows.Count - 1
            strParcelNo = flxDetails.Item(0, intRow).Value
            dblBasePrice = CDbl(flxDetails.Item(4, intRow).Value)
            AdoCN.Execute("INSERT INTO tblExpSizingTypes(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts,OK,BasePrice,EstCts,DiaCost) " & _
                          "VALUES('Mix','" & strParcelNo & "','H001',1,'" & UCase(flxDetails.Item(1, intRow).Value) & "'," & CInt(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & ",0," & dblBasePrice & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(8, intRow).Value) & ")")
        Next

        If blnSave = True Then
            MsgBox("Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
        End If
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub txtOldParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOldParNo.KeyPress
        If Asc(e.KeyChar) = 13 And Len(txtOldParNo.Text) > 0 Then
            If chkBulk.Checked = True Then
                Load_CostingBulkDetails()
            Else
                If optPolish.Checked = True Then
                    Load_ParcelDetails()
                Else
                    Load_CostingDetails()
                End If
            End If
        End If
    End Sub

    Private Sub Load_ParcelDetails()
        Dim strAssortment As String
        Dim dblPrice As Double

        flxDetails.Rows.Clear()

        txtOldParNo.Text = UCase(txtOldParNo.Text)

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT ReturnType, SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts " & _
                      "FROM dbo.tblExpSizingTypes " & _
                      "WHERE (ParNo = '" & txtOldParNo.Text & "') " & _
                      "GROUP BY ReturnType " & _
                      "ORDER BY ReturnType", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                strAssortment = rsComSql.Fields("ReturnType").Value
                dblPrice = 0
                If Mid(strAssortment, 1, 1) = "A" Or Mid(strAssortment, 1, 1) = "D" Or Mid(strAssortment, 1, 1) = "V" Or Mid(strAssortment, 1, 1) = "S" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & strAssortment & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        dblPrice = rsComSql_1.Fields("MarketPrice").Value
                    End If
                    rsComSql_1 = Nothing
                Else
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & strAssortment & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        dblPrice = rsComSql_1.Fields("ListCost").Value
                    End If
                    rsComSql_1 = Nothing
                End If

                If Mid(strAssortment, 1, 4) = "AROY" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT ItemCost FROM tblImport WHERE SupParcelNo = '" & cmbOrgAssort.Text & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        dblPrice = rsComSql_1.Fields("ItemCost").Value
                    End If
                    rsComSql_1 = Nothing
                End If

                flxDetails.Rows.Add(cmbOrgAssort.Text,
                                    strAssortment,
                                    rsComSql.Fields("Pcs").Value,
                                    Math.Round(rsComSql.Fields("Cts").Value, 3),
                                    Format(dblPrice, "#0.00"),
                                    Format(Math.Round(rsComSql.Fields("Cts").Value, 3) * dblPrice, "#0.00"),
                                    Format(dblPrice, "#0.00"), "0", "0")

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtPcs.Text = CalTotalPcs(flxDetails)
        txtCts.Text = CalTotalCts(flxDetails)
        txtBaseVal.Text = CalTotalValue(flxDetails)

        Process()
    End Sub

    Private Sub Load_CostingDetails()

        flxDetails.Rows.Clear()

        txtOldParNo.Text = UCase(txtOldParNo.Text)

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT Assortment, BaseCost, ExportPcs, ExportCts " & _
                      "FROM dbo.tblCosting " & _
                      "WHERE (PackingListNo = '" & txtOldParNo.Text & "') AND (Department = 'SizeExports') " & _
                      "ORDER BY Assortment", dbConn, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(cmbOrgAssort.Text,
                                    rsComSql.Fields("Assortment").Value,
                                    rsComSql.Fields("ExportPcs").Value,
                                    Math.Round(rsComSql.Fields("ExportCts").Value, 3),
                                    Format(rsComSql.Fields("BaseCost").Value, "#0.00"),
                                    Format(Math.Round(rsComSql.Fields("ExportCts").Value, 3) * rsComSql.Fields("BaseCost").Value, "#0.00"),
                                    Format(rsComSql.Fields("BaseCost").Value, "#0.00"), "0", "0")

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtPcs.Text = CalTotalPcs(flxDetails)
        txtCts.Text = CalTotalCts(flxDetails)
        txtBaseVal.Text = CalTotalValue(flxDetails)

        Process()
    End Sub

    Private Sub Load_CostingBulkDetails()

        flxDetails.Rows.Clear()

        txtOldParNo.Text = UCase(txtOldParNo.Text)

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT SupParcelNo, LotNo FROM tblImport WHERE ImportNo = '" & txtImportNo.Text & "' AND SupParcelNo LIKE 'A%' ORDER BY SupParcelNo", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT TOP (100) PERCENT Assortment, BaseCost, ExportPcs, ExportCts " & _
                              "FROM dbo.tblCosting " & _
                              "WHERE (PackingListNo = '" & txtOldParNo.Text & "') AND (Department = 'SizeExports') AND (LotID = '" & rsComSql_1.Fields("LotNo").Value & "') " & _
                              "ORDER BY Assortment", dbConn, 1, 1)
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        flxDetails.Rows.Add(rsComSql_1.Fields("SupParcelNo").Value,
                                            rsComSql.Fields("Assortment").Value,
                                            rsComSql.Fields("ExportPcs").Value,
                                            Math.Round(rsComSql.Fields("ExportCts").Value, 3),
                                            Format(rsComSql.Fields("BaseCost").Value, "#0.00"),
                                            Format(Math.Round(rsComSql.Fields("ExportCts").Value, 3) * rsComSql.Fields("BaseCost").Value, "#0.00"),
                                            Format(rsComSql.Fields("BaseCost").Value, "#0.00"), "0", "0")

                        rsComSql.MoveNext()
                    End While
                End If
                rsComSql = Nothing

                rsComSql_1.MoveNext()
            End While
        End If
        rsComSql_1 = Nothing

        txtPcs.Text = CalTotalPcs(flxDetails)
        txtCts.Text = CalTotalCts(flxDetails)
        txtBaseVal.Text = CalTotalValue(flxDetails)

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT SUM(ACTPcs) AS ACTPcs, SUM(ACtCts) AS ACtCts, SUM(ACtCts * ImpPrice) AS Value FROM tblImport WHERE ImportNo = '" & txtImportNo.Text & "' AND SupParcelNo LIKE 'A%' ", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            txtTotPcs.Text = rsComSql_1.Fields("ACTPcs").Value
            txtTotCts.Text = Math.Round(rsComSql_1.Fields("ACtCts").Value, 3)
            txtInvValue.Text = Math.Round(rsComSql_1.Fields("Value").Value, 2)
        End If

        Process()
    End Sub

    Private Sub Load_CostingBulkDetails2()
        Dim dblPackingNo As Double
        Dim dblPrice As Double

        dblPackingNo = 0
        dblPrice = 0
        flxDetails.Rows.Clear()

        txtOldParNo.Text = UCase(txtOldParNo.Text)

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.tblImport.SupParcelNo, dbo.tblImport.InvoiceDate, dbo.tblImport.AssortmentNo, dbo.tblImport.LotNo, dbo.tblImport.ImportNo, dbo.tblImport.ItemCost, dbo.tblImport.Article " & _
                        "FROM dbo.tblImport INNER JOIN dbo.tblExpPacket ON dbo.tblImport.SupParcelNo = dbo.tblExpPacket.ParNo LEFT OUTER JOIN " & _
                            "dbo.tblExpIssues ON dbo.tblImport.SupParcelNo = dbo.tblExpIssues.ParNo " & _
                        "WHERE (dbo.tblImport.SupParcelNo LIKE N'H%') AND (dbo.tblExpIssues.ParNo IS NULL) " & _
                        "ORDER BY dbo.tblImport.SupParcelNo", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                'dblPackingNo = CDbl(Mid(rsComSql_1.Fields("Remarks").Value, 12, 5))
                dblPackingNo = CDbl(rsComSql_1.Fields("Article").Value)

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT TOP (100) PERCENT Assortment, ListCost, Pcs, Cts " & _
                              "FROM dbo.VW_DCLPackingListAll2A " & _
                              "WHERE (PackNo = '" & dblPackingNo & "') " & _
                              "ORDER BY Assortment", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        dblPrice = rsComSql.Fields("ListCost").Value

                        flxDetails.Rows.Add(rsComSql_1.Fields("SupParcelNo").Value,
                                            rsComSql.Fields("Assortment").Value,
                                            rsComSql.Fields("Pcs").Value,
                                            Math.Round(rsComSql.Fields("Cts").Value, 3),
                                            Format(dblPrice, "#0.00"),
                                            Format(Math.Round(rsComSql.Fields("Cts").Value, 3) * dblPrice, "#0.00"),
                                            Format(dblPrice, "#0.00"), "0", "0")

                        rsComSql.MoveNext()
                    End While
                End If
                rsComSql = Nothing

                rsComSql_1.MoveNext()
            End While
        End If
        rsComSql_1 = Nothing

        txtPcs.Text = CalTotalPcs(flxDetails)
        txtCts.Text = CalTotalCts(flxDetails)
        txtBaseVal.Text = CalTotalValue(flxDetails)

        'rsComSql_1 = New ADODB.Recordset
        'rsComSql_1.Open("SELECT SUM(ACTPcs) AS ACTPcs, SUM(ACtCts) AS ACtCts, SUM(ACtCts * ImpPrice) AS Value FROM tblImport WHERE ImportNo = '" & txtImportNo.Text & "' AND SupParcelNo LIKE 'A%' ", AdoCN, 1, 1)
        'If rsComSql_1.RecordCount Then
        '    txtTotPcs.Text = rsComSql_1.Fields("ACTPcs").Value
        '    txtTotCts.Text = Math.Round(rsComSql_1.Fields("ACtCts").Value, 3)
        '    txtInvValue.Text = Math.Round(rsComSql_1.Fields("Value").Value, 2)
        'End If

        txtTotPcs.Text = txtPcs.Text
        txtTotCts.Text = txtCts.Text
        txtInvValue.Text = txtBaseVal.Text

        Process()
    End Sub

    Private Sub frm_MixAssortStock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub cmdCalculate_Click(sender As Object, e As EventArgs) Handles cmdCalculate.Click
        txtPcs.Text = CalTotalPcs(flxDetails)
        txtCts.Text = CalTotalCts(flxDetails)
        txtBaseVal.Text = CalTotalValue(flxDetails)

        Process()
    End Sub

    Private Sub cmdLoadPolish_Click(sender As Object, e As EventArgs) Handles cmdLoadPolish.Click
        If chkBulkPolish.Checked = True Then
            Load_CostingBulkDetails2()
        End If
    End Sub

    Private Sub chkBulk_CheckedChanged(sender As Object) Handles chkBulk.CheckedChanged
        If chkBulk.Checked = True Then
            chkBulkPolish.Checked = False
        End If
    End Sub

    Private Sub chkBulkPolish_CheckedChanged(sender As Object) Handles chkBulkPolish.CheckedChanged
        If chkBulkPolish.Checked = True Then
            chkBulk.Checked = False
        End If
    End Sub
End Class