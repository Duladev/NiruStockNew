
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_DCLDeptTransLot

    Private Sub ClearFields()
        txtImportNo.Text = ""
        flxDetails.Rows.Clear()
        chkSelect.Checked = False
        txtPcs.Text = ""
        txtCts.Text = ""
        cmbOrgAssort.Text = ""
        cmbOrgAssort.Items.Clear()
        txtLotNo.Text = ""
        txtTotPcs.Text = ""
        txtTotCts.Text = ""
        txtCompCode.Text = ""
        txtOrigin.Text = ""
        txtListCost.Text = ""
        txtAnaCost.Text = ""
        txtDiaCost.Text = ""
        txtDiaLabCost.Text = ""
        txtLabour.Text = ""
        chkOriginal.Checked = False
        chkInternal.Checked = False
        txtImportNo.Focus()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_ImportDetails()
        flxDetails.Rows.Clear()

        If optPolish.Checked = True Or optPolishOK.Checked = True Then
            Load_Import(CDbl(txtImportNo.Text))
        Else
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblImport WHERE ImportNo = " & CDbl(txtImportNo.Text) & " ORDER BY SupParcelNo", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                txtCompCode.Text = rsComSql.Fields("CompCode").Value
                If rsComSql.Fields("Original").Value = 1 Then
                    If Len(rsComSql.Fields("ConRefNo").Value) > 0 Then
                        chkOriginal.Checked = False
                    Else
                        chkOriginal.Checked = True
                    End If
                Else
                    chkOriginal.Checked = False
                End If
                If rsComSql.Fields("ParcelType").Value = "Polished" Then
                    While Not rsComSql.EOF
                        flxDetails.Rows.Add(rsComSql.Fields("LotNo").Value,
                                            rsComSql.Fields("AssortmentNo").Value,
                                            rsComSql.Fields("SupParcelNo").Value,
                                            rsComSql.Fields("INVPcs").Value,
                                            rsComSql.Fields("INVCts").Value,
                                            rsComSql.Fields("ItemCost").Value)
                        rsComSql.MoveNext()
                    End While
                ElseIf rsComSql.Fields("ParcelType").Value = "Rough" Then
                    While Not rsComSql.EOF
                        flxDetails.Rows.Add(rsComSql.Fields("LotNo").Value,
                                            rsComSql.Fields("AssortmentNo").Value,
                                            rsComSql.Fields("SupParcelNo").Value,
                                            rsComSql.Fields("INVPcs").Value,
                                            rsComSql.Fields("INVCts").Value,
                                            rsComSql.Fields("ItemCost").Value)

                        rsComSql.MoveNext()
                    End While
                Else
                    MsgBox("The Import is NFE", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
            rsComSql = Nothing
        End If

        txtPcs.Text = CalTotalPcs(flxDetails)
        txtCts.Text = CalTotalCts(flxDetails)

    End Sub

    Private Sub Load_Import(ByVal dblImportNo As Double)
        cmbOrgAssort.Items.Clear()
        cmbOrgAssort.Text = ""
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT SupParcelNo, CompCode FROM tblImport WHERE ImportNo = " & dblImportNo & " GROUP BY SupParcelNo, CompCode ORDER BY SupParcelNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            txtCompCode.Text = rsComSql.Fields("CompCode").Value
            While Not rsComSql.EOF
                cmbOrgAssort.Items.Add(rsComSql.Fields("SupParcelNo").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(3, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(4, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub txtImportNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtImportNo.KeyPress
        IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 And Len(txtImportNo.Text) > 0 Then
            'If optPolish.Checked = True Or optPolishOK.Checked = True Then
            '    Load_Import(CDbl(txtImportNo.Text))
            'Else
            '    Load_ImportDetails()
            'End If
            Load_ImportDetails()
        End If
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim DataEnter As String
        Dim enterd_Date As String
        Dim strDepartment As String
        Dim dblAvgCost As Double
        Dim dblCurCost As Double
        Dim dblStockCts As Double
        Dim dblStockValue As Double
        Dim dblCurValue As Double
        Dim strAssortment As String
        Dim intAMS As Integer
        Dim intYahuda As Integer

        Dim dblDiaCost As Double
        Dim dblDiaLabCost As Double
        Dim dblLabourCost As Double

        Dim dblDiaValue As Double
        Dim dblDiaLabValue As Double
        Dim dblLabourValue As Double

        DataEnter = "suser_sname()"
        enterd_Date = "GETDATE()"

        If optApcu.Checked = True Or optMix.Checked = True Then
            strDepartment = "Mix"
        Else
            If optPolishOK.Checked = True Then
                strDepartment = "PolishBox"
            Else
                If optPolish.Checked = True Then
                    strDepartment = "TempPolishBox"
                Else
                    If optKit.Checked = True Then
                        strDepartment = "KIT Box"
                    Else
                        strDepartment = ""
                        MsgBox("Invalid Selection", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If
            End If
        End If

        If txtImportNo.Text = "" Then Exit Sub

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If optPolish.Checked = True Or optPolishOK.Checked = True Then
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblDep_Trf WHERE Department = '" & strDepartment & "' AND SupParcelNo = '" & cmbOrgAssort.Text & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    MsgBox("Already Transferred", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql_1 = Nothing

                If CDbl(txtTotPcs.Text) <> CDbl(txtPcs.Text) Then
                    MsgBox("Invalid Pcs in the List", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            For intRow = 0 To flxDetails.RowCount - 1
                If flxDetails.Item(6, intRow).Value = True Then
                    rsComSql = New ADODB.Recordset
                    If strDepartment = "Mix" Or strDepartment = "KIT Box" Then
                        rsComSql.Open("SELECT Assortment FROM tblAssortList WHERE Assortment = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                    Else
                        rsComSql.Open("SELECT ItemName FROM tblDCLPermanents WHERE ItemName = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                    End If
                    If rsComSql.RecordCount = 0 Then
                        'MsgBoxGT("(Invalid Assortment - " & flxDetails.TextMatrix(intRow, 1), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Caption)
                        'Exit Sub
                    End If
                    rsComSql = Nothing

                    If Len(flxDetails.Item(3, intRow).Value) = 0 Then
                        MsgBox("Invalid Pcs - " & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If IsNumeric(flxDetails.Item(3, intRow).Value) = False Then
                        MsgBox("Invalid Pcs - " & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If CDbl(flxDetails.Item(3, intRow).Value) <= 0 Then
                        MsgBox("Invalid Pcs - " & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If

                    If Len(flxDetails.Item(4, intRow).Value) = 0 Then
                        MsgBox("Invalid Cts - " & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If IsNumeric(flxDetails.Item(4, intRow).Value) = False Then
                        MsgBox("Invalid Cts - " & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    If CDbl(flxDetails.Item(4, intRow).Value) <= 0 Then
                        MsgBox("Invalid Cts - " & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If
                
            Next

            For intRow = 0 To flxDetails.RowCount - 1
                If flxDetails.Item(6, intRow).Value = True Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblImport WHERE SupParcelNo = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        rsComSql.MoveFirst()
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM tblDep_Trf WHERE Department = '" & strDepartment & "' AND SupParcelNo = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            AdoCN.Execute("INSERT INTO tblDep_Trf(Department,DCLImportNo,SupplierRefNo,CompanyRefNo,BOINo,InvoiceDate,RecievedDate,SupplierCode,ParcelType,AssortmentNo,SupParcelNo,DCLParcelNo,INVPcs,INVCts,ACTPcs,ACtCts,NewACTPcs,NewACTCts,ItemSize,Charges,ItemCost,RemPcs,RemCts,Status,DoneBy,ModifyBy,SysDateTime,AParNo) " & _
                                          "VALUES('" & strDepartment & "','" & rsComSql.Fields("SystemRefNo").Value & "','" & rsComSql.Fields("SupplierRefNo").Value & "','" & rsComSql.Fields("CompanyRefNo").Value & "','" & rsComSql.Fields("BOINo").Value & "','" & Format(rsComSql.Fields("InvoiceDate").Value, "MM/dd/yyyy") & "','" & Format(rsComSql.Fields("RecievedDate").Value, "MM/dd/yyyy") & "','" & rsComSql.Fields("SupplierCode").Value & "','" & rsComSql.Fields("ParcelType").Value & "'," & _
                                                "'" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(2, intRow).Value & "','" & flxDetails.Item(2, intRow).Value & "'," & flxDetails.Item(3, intRow).Value & "," & flxDetails.Item(4, intRow).Value & "," & flxDetails.Item(3, intRow).Value & "," & flxDetails.Item(4, intRow).Value & "," & flxDetails.Item(3, intRow).Value & "," & _
                                                "" & flxDetails.Item(4, intRow).Value & "," & rsComSql.Fields("ItemSize").Value & "," & IIf(IsDBNull(rsComSql.Fields("Charges").Value), 0, rsComSql.Fields("Charges").Value) & "," & rsComSql.Fields("ItemCost").Value & "," & flxDetails.Item(3, intRow).Value & "," & flxDetails.Item(4, intRow).Value & ",'I','" & PBUser_ID & "'," & DataEnter & "," & enterd_Date & ",'" & flxDetails.Item(2, intRow).Value & "')")
                        Else
                            'MsgBox("Already Transferred", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            'Exit Sub
                        End If
                        rsComSql_1 = Nothing

                        If strDepartment = "Mix" Or strDepartment = "KIT Box" Then
                            '====================================
                            If chkOriginal.Checked = False Then
                                intAMS = 1
                                intYahuda = 1
                            Else
                                If Len(rsComSql.Fields("ConRefNo").Value) = 0 Then
                                    intAMS = 0
                                    intYahuda = 0
                                Else
                                    intAMS = 1
                                    intYahuda = 1
                                End If
                            End If

                            'Exp Packet
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT * FROM tblExpPacket WHERE Department = '" & strDepartment & "' AND ParNo = '" & flxDetails.Item(2, intRow).Value & "' AND PktNo = 'N001'", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount = 0 Then
                                AdoCN.Execute("INSERT INTO tblExpPacket(Department,ParNo,PktNo,PktPcs,PktCts,PktType,PktDate,AParNo,AMS2,YAH) " & _
                                              "VALUES('" & strDepartment & "','" & flxDetails.Item(2, intRow).Value & "','N001'," & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",'','" & Format(Date.Now, "MM/dd/yyyy") & "','" & flxDetails.Item(1, intRow).Value & "'," & intAMS & "," & intYahuda & ")")
                            Else
                                MsgBox("Already Transferred", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                Exit Sub
                            End If
                            rsComSql_1 = Nothing

                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT * FROM tblGrading_Parcel WHERE ParNo = '" & flxDetails.Item(2, intRow).Value & "' AND Dept = '" & strDepartment & "'", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount = 0 Then
                                AdoCN.Execute("INSERT INTO tblGrading_Parcel(ParNo,Complete,Dept,Assort) VALUES('" & flxDetails.Item(2, intRow).Value & "',0,'" & strDepartment & "','" & flxDetails.Item(1, intRow).Value & "')")
                            End If
                            rsComSql_1 = Nothing

                            If optApcu.Checked = True Then
                                '====================================
                                'Fluorescent Checking Issues
                                AdoCN.Execute("INSERT INTO tblExpIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                              "VALUES('" & strDepartment & "','" & flxDetails.Item(2, intRow).Value & "','N001',1,'" & PBUser_EmpNo & "'," & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")

                                'Fluorescent Checking Returns
                                AdoCN.Execute("INSERT INTO tblExpReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                                              "VALUES('" & strDepartment & "','" & flxDetails.Item(2, intRow).Value & "','N001',1,'" & PBUser_EmpNo & "'," & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0)")

                                'Fluorescent Checking Return Details
                                AdoCN.Execute("INSERT INTO tblExpReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                                              "VALUES('" & strDepartment & "','" & flxDetails.Item(2, intRow).Value & "','N001',1,'NONE'," & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ")")

                                '====================================
                                'Color Sorting Issues
                                AdoCN.Execute("INSERT INTO tblExpIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                              "VALUES('" & strDepartment & "','" & flxDetails.Item(2, intRow).Value & "','N001',2,'" & PBUser_EmpNo & "'," & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")

                                'Color Sorting Returns
                                AdoCN.Execute("INSERT INTO tblExpReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                                              "VALUES('" & strDepartment & "','" & flxDetails.Item(2, intRow).Value & "','N001',2,'" & PBUser_EmpNo & "'," & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0)")

                                'Color Sorting Return Details
                                AdoCN.Execute("INSERT INTO tblExpReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                                              "VALUES('" & strDepartment & "','" & flxDetails.Item(2, intRow).Value & "','N001',2,'COLOR'," & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ")")

                                '====================================
                                'Clarity Checking Issues
                                AdoCN.Execute("INSERT INTO tblExpIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                              "VALUES('" & strDepartment & "','" & flxDetails.Item(2, intRow).Value & "','N001',3,'" & PBUser_EmpNo & "'," & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")

                                'Clarity Checking Returns
                                AdoCN.Execute("INSERT INTO tblExpReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                                              "VALUES('" & strDepartment & "','" & flxDetails.Item(2, intRow).Value & "','N001',3,'" & PBUser_EmpNo & "'," & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0)")

                                'Clarity Checking Return Details
                                AdoCN.Execute("INSERT INTO tblExpReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                                              "VALUES('" & strDepartment & "','" & flxDetails.Item(2, intRow).Value & "','N001',3,'COLOR'," & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ")")

                                '====================================
                                'Sizing Packet
                                AdoCN.Execute("INSERT INTO tblExpSizingPacket(Department, ParNo, PktNo, PktPcs, PktCts, ReturnType) " & _
                                              "VALUES('" & strDepartment & "','" & flxDetails.Item(2, intRow).Value & "','H001'," & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",'COLOR')")

                                'Sizing Issues
                                AdoCN.Execute("INSERT INTO tblExpSizingIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                              "VALUES('" & strDepartment & "','" & flxDetails.Item(2, intRow).Value & "','H001',1,'" & PBUser_EmpNo & "'," & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")

                                If optApcu.Checked = True Then
                                    'Sizing Returns
                                    AdoCN.Execute("INSERT INTO tblExpSizingReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                                                  "VALUES('" & strDepartment & "','" & flxDetails.Item(2, intRow).Value & "','H001',1,'" & PBUser_EmpNo & "', " & CInt(flxDetails.Item(3, intRow).Value) & ", " & CDbl(flxDetails.Item(4, intRow).Value) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0)")

                                    'Sizing Types
                                    AdoCN.Execute("INSERT INTO tblExpSizingTypes(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts,OK,EstCts) " & _
                                                  "VALUES('" & strDepartment & "','" & flxDetails.Item(2, intRow).Value & "','H001',1,'" & flxDetails.Item(1, intRow).Value & "'," & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",0," & CDbl(flxDetails.Item(4, intRow).Value) & ")")
                                End If
                            End If

                        ElseIf strDepartment = "TempPolishBox" Then
                            dblAvgCost = 0
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount Then
                                strAssortment = flxDetails.Item(1, intRow).Value
                                If txtCompCode.Text = "DCL" Then
                                    dblAvgCost = rsComSql_1.Fields("AvgCost").Value
                                Else
                                    dblAvgCost = rsComSql_1.Fields("AvgCost2").Value
                                End If
                            Else
                                strAssortment = flxDetails.Item(1, intRow).Value & "_" & flxDetails.Item(0, intRow).Value
                                AdoCN.Execute("INSERT INTO tblDCLPermanents(LotTypeID,ItemTypeID,ItemID,ItemName,LotID,LotName,Shape,Make,Color,Clarity,LengthFrom,LengthTo,WidthFrom,WidthTo,WtFrom,WtTo,ProfitCenter,ListCost,DocTypeDesc,DocDate,AvgCost,AvgCost2) " & _
                                              "VALUES('Item','Polished diamonds',99999,'" & strAssortment & "',99999,'" & strAssortment & "','','','','',0,0,0,0,0,0,'M'," & CDbl(flxDetails.Item(5, intRow).Value) & ",'OPEN STOCK','" & Format(Date.Now, "MM/dd/yyyy") & "'," & CDbl(flxDetails.Item(5, intRow).Value) & "," & CDbl(flxDetails.Item(5, intRow).Value) & ")")
                            End If
                            rsComSql_1 = Nothing

                            If dblAvgCost = 0 Then
                                dblAvgCost = CDbl(flxDetails.Item(5, intRow).Value)
                            End If

                            dblStockCts = 0
                            dblStockValue = 0
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT * FROM VW_POLStockBal WHERE Assortment = '" & strAssortment & "' AND CompCode = '" & txtCompCode.Text & "'", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount Then
                                If Not IsDBNull(rsComSql_1.Fields("Cts").Value) Then
                                    dblStockCts = rsComSql_1.Fields("Cts").Value
                                    dblStockValue = rsComSql_1.Fields("Cts").Value * dblAvgCost
                                End If
                            End If
                            rsComSql_1 = Nothing

                            rsComSql_1 = New ADODB.Recordset
                            If txtCompCode.Text = "DCL" Then
                                rsComSql_1.Open("SELECT SUM(BalCts) AS Cts, SUM(BalCts * AvgCost) AS Value " & _
                                                "FROM VW_POMStockBal " & _
                                                "WHERE (Assortment = '" & strAssortment & "') AND (CompCode = '" & txtCompCode.Text & "')", AdoCN, 1, 1)
                            Else
                                rsComSql_1.Open("SELECT SUM(BalCts) AS Cts, SUM(BalCts * AvgCost2) AS Value " & _
                                                "FROM VW_POMStockBal " & _
                                                "WHERE (Assortment = '" & strAssortment & "') AND (CompCode = '" & txtCompCode.Text & "')", AdoCN, 1, 1)
                            End If
                            If rsComSql_1.RecordCount Then
                                If Not IsDBNull(rsComSql_1.Fields("Cts").Value) Then
                                    dblStockCts = dblStockCts + Math.Round(rsComSql_1.Fields("Cts").Value, 3)
                                    dblStockValue = dblStockValue + rsComSql_1.Fields("Value").Value
                                End If
                            End If
                            rsComSql_1 = Nothing

                            dblAvgCost = (dblStockValue + (CDbl(flxDetails.Item(4, intRow).Value) * CDbl(flxDetails.Item(5, intRow).Value))) / (dblStockCts + CDbl(flxDetails.Item(4, intRow).Value))
                            dblAvgCost = Math.Round(dblAvgCost, 2)

                            AdoCN.Execute("INSERT INTO tblPOMStockIn(ImportNo,SupParNo,Assortment,Assortment2,Pcs,Cts,Price,CompCode) " & _
                                          "VALUES(" & CDbl(txtImportNo.Text) & ",'" & flxDetails.Item(2, intRow).Value & "','" & strAssortment & "','" & strAssortment & "'," & _
                                              "" & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & "," & _
                                              "" & CDbl(flxDetails.Item(5, intRow).Value) & ",'" & txtCompCode.Text & "')")

                            AdoCN.Execute("UPDATE tblImport SET PolPcs = PolPcs + " & CInt(flxDetails.Item(3, intRow).Value) & ", PolCts = PolCts + " & CDbl(flxDetails.Item(4, intRow).Value) & " " & _
                                          "WHERE SupParcelNo = '" & flxDetails.Item(2, intRow).Value & "'")

                            If txtCompCode.Text = "DCL" Then
                                AdoCN.Execute("UPDATE tblDCLPermanents SET AvgCost = " & dblAvgCost & " WHERE ItemName = '" & strAssortment & "'")
                            Else
                                AdoCN.Execute("UPDATE tblDCLPermanents SET AvgCost2 = " & dblAvgCost & " WHERE ItemName = '" & strAssortment & "'")
                            End If

                        ElseIf strDepartment = "PolishBox" Then
                            dblAvgCost = 0
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount Then
                                strAssortment = flxDetails.Item(1, intRow).Value
                                If txtCompCode.Text = "DCL" Then
                                    dblAvgCost = rsComSql_1.Fields("AvgCost").Value
                                    dblCurCost = rsComSql_1.Fields("CurCost").Value

                                    dblDiaCost = rsComSql_1.Fields("DiaCost").Value
                                    dblDiaLabCost = rsComSql_1.Fields("DiaCostLab").Value
                                    dblLabourCost = rsComSql_1.Fields("LabourCost").Value

                                ElseIf txtCompCode.Text = "NLE" Then
                                    dblAvgCost = rsComSql_1.Fields("AvgCost2").Value
                                    dblCurCost = rsComSql_1.Fields("CurCost2").Value

                                    dblDiaCost = rsComSql_1.Fields("DiaCost").Value
                                    dblDiaLabCost = rsComSql_1.Fields("DiaCostLab").Value
                                    dblLabourCost = rsComSql_1.Fields("LabourCost").Value

                                Else
                                    dblAvgCost = rsComSql_1.Fields("AvgCost3").Value
                                    dblCurCost = rsComSql_1.Fields("CurCost3").Value

                                    dblDiaCost = rsComSql_1.Fields("DiaCost").Value
                                    dblDiaLabCost = rsComSql_1.Fields("DiaCostLab").Value
                                    dblLabourCost = rsComSql_1.Fields("LabourCost").Value
                                End If
                            Else
                                strAssortment = flxDetails.Item(1, intRow).Value & "_" & flxDetails.Item(0, intRow).Value
                                AdoCN.Execute("INSERT INTO tblDCLPermanents(LotTypeID,ItemTypeID,ItemID,ItemName,LotID,LotName,Shape,Make,Color,Clarity,LengthFrom,LengthTo,WidthFrom,WidthTo,WtFrom,WtTo,ProfitCenter,ListCost,DocTypeDesc,DocDate,AvgCost,AvgCost2) " & _
                                              "VALUES('Item','Polished diamonds',99999,'" & strAssortment & "',99999,'" & strAssortment & "','','','','',0,0,0,0,0,0,'M'," & CDbl(flxDetails.Item(5, intRow).Value) & ",'OPEN STOCK','" & Format(Date.Now, "MM/dd/yyyy") & "'," & CDbl(flxDetails.Item(5, intRow).Value) & "," & CDbl(flxDetails.Item(5, intRow).Value) & ")")
                            End If
                            rsComSql_1 = Nothing

                            If dblAvgCost = 0 Then
                                dblAvgCost = CDbl(flxDetails.Item(5, intRow).Value)
                            End If

                            dblStockCts = 0
                            dblStockValue = 0
                            dblCurValue = 0
                            dblDiaValue = 0
                            dblDiaLabValue = 0
                            dblLabourValue = 0
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT ROUND(SUM(Cts), 3) AS Cts FROM VW_POLStockBal WHERE Assortment = '" & strAssortment & "' AND CompCode = '" & txtCompCode.Text & "'", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount Then
                                If Not IsDBNull(rsComSql_1.Fields("Cts").Value) Then
                                    If dblStockCts < 0 Then
                                        dblStockCts = 0
                                    End If
                                    dblStockCts = rsComSql_1.Fields("Cts").Value
                                    dblStockValue = dblStockCts * dblAvgCost
                                    dblCurValue = dblStockCts * dblCurCost
                                    dblDiaValue = dblStockCts * dblDiaCost
                                    dblDiaLabValue = dblStockCts * dblDiaLabCost
                                    dblLabourValue = dblStockCts * dblLabourCost
                                End If
                            End If
                            rsComSql_1 = Nothing

                            'rsComSql_1 = New ADODB.Recordset
                            'If txtCompCode.Text = "DCL" Then
                            '    rsComSql_1.Open("SELECT SUM(BalCts) AS Cts, SUM(BalCts * AvgCost) AS Value " & _
                            '                    "FROM VW_POMStockBal " & _
                            '                    "WHERE (Assortment = '" & strAssortment & "') AND (CompCode = '" & txtCompCode.Text & "')", AdoCN, 1, 1)
                            'Else
                            '    rsComSql_1.Open("SELECT SUM(BalCts) AS Cts, SUM(BalCts * AvgCost2) AS Value " & _
                            '                    "FROM VW_POMStockBal " & _
                            '                    "WHERE (Assortment = '" & strAssortment & "') AND (CompCode = '" & txtCompCode.Text & "')", AdoCN, 1, 1)
                            'End If
                            'If rsComSql_1.RecordCount Then
                            '    If Not IsDBNull(rsComSql_1.Fields("Cts").Value) Then
                            '        dblStockCts = dblStockCts + rsComSql_1.Fields("Cts").Value
                            '        dblStockValue = dblStockValue + rsComSql_1.Fields("Value").Value
                            '    End If
                            'End If
                            'rsComSql_1 = Nothing

                            dblAvgCost = (dblStockValue + (CDbl(flxDetails.Item(4, intRow).Value) * CDbl(flxDetails.Item(8, intRow).Value))) / (dblStockCts + CDbl(flxDetails.Item(4, intRow).Value))
                            dblAvgCost = Math.Round(dblAvgCost, 2)

                            dblCurCost = (dblCurValue + (CDbl(flxDetails.Item(4, intRow).Value) * CDbl(flxDetails.Item(11, intRow).Value))) / (dblStockCts + CDbl(flxDetails.Item(4, intRow).Value))
                            dblCurCost = Math.Round(dblCurCost, 2)

                            dblDiaCost = (dblDiaValue + (CDbl(flxDetails.Item(4, intRow).Value) * CDbl(flxDetails.Item(10, intRow).Value))) / (dblStockCts + CDbl(flxDetails.Item(4, intRow).Value))
                            dblDiaCost = Math.Round(dblDiaCost, 2)

                            dblDiaLabCost = (dblDiaLabValue + (CDbl(flxDetails.Item(4, intRow).Value) * CDbl(flxDetails.Item(11, intRow).Value))) / (dblStockCts + CDbl(flxDetails.Item(4, intRow).Value))
                            dblDiaLabCost = Math.Round(dblDiaLabCost, 2)

                            dblLabourCost = (dblLabourValue + (CDbl(flxDetails.Item(4, intRow).Value) * CDbl(flxDetails.Item(12, intRow).Value))) / (dblStockCts + CDbl(flxDetails.Item(4, intRow).Value))
                            dblLabourCost = Math.Round(dblLabourCost, 2)

                            AdoCN.Execute("INSERT INTO tblPOLStockIn(SupParNo,Assortment,Assortment2,Pcs,Cts,Price,CompCode,SizeRange,DiaCost,LabourCost) " & _
                                          "VALUES('" & flxDetails.Item(2, intRow).Value & "','" & strAssortment & "','" & strAssortment & "'," & _
                                              "" & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & "," & _
                                              "" & CDbl(flxDetails.Item(5, intRow).Value) & ",'" & txtCompCode.Text & "','" & flxDetails.Item(7, intRow).Value & "'," & CDbl(flxDetails.Item(10, intRow).Value) & "," & CDbl(flxDetails.Item(12, intRow).Value) & ")")

                            'If chkInternal.Checked = False Then
                            'Insert Stock In Origin
                            AdoCN.Execute("INSERT INTO tblPOLStockInOrigin(Assortment,Origin,SupParNo,Pcs,EntDate,CompCode) " & _
                                          "VALUES('" & flxDetails.Item(1, intRow).Value & "','" & txtOrigin.Text & "','" & cmbOrgAssort.Text & "'," & CInt(flxDetails.Item(3, intRow).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & txtCompCode.Text & "')")
                            'End If

                            AdoCN.Execute("UPDATE tblImport SET PolPcs = PolPcs + " & CInt(flxDetails.Item(3, intRow).Value) & ", PolCts = PolCts + " & CDbl(flxDetails.Item(4, intRow).Value) & " " & _
                                          "WHERE SupParcelNo = '" & flxDetails.Item(2, intRow).Value & "'")

                            If txtCompCode.Text = "DCL" Then
                                AdoCN.Execute("UPDATE tblDCLPermanents SET AvgCost = " & dblAvgCost & ", CurCost = " & dblCurCost & ",DiaCost = " & dblDiaCost & ",DiaCostLab = " & dblDiaLabCost & ",LabourCost = " & dblLabourCost & " WHERE ItemName = '" & strAssortment & "'")

                            ElseIf txtCompCode.Text = "NLE" Then
                                AdoCN.Execute("UPDATE tblDCLPermanents SET AvgCost2 = " & dblAvgCost & ", CurCost2 = " & dblCurCost & ",DiaCost = " & dblDiaCost & ",DiaCostLab = " & dblDiaLabCost & ",LabourCost = " & dblLabourCost & " WHERE ItemName = '" & strAssortment & "'")

                            Else
                                AdoCN.Execute("UPDATE tblDCLPermanents SET AvgCost3 = " & dblAvgCost & ", CurCost3 = " & dblCurCost & ",DiaCost = " & dblDiaCost & ",DiaCostLab = " & dblDiaLabCost & ",LabourCost = " & dblLabourCost & " WHERE ItemName = '" & strAssortment & "'")
                            End If
                        End If
                    End If
                    rsComSql = Nothing
                End If
            Next

            MsgBox("Successfully Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
        End If
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(6, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(6, intRow).Value = False
            Next
        End If
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmbOrgAssort_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbOrgAssort.SelectedIndexChanged
        If cmbOrgAssort.Text <> "" Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT LotNo,ACTPcs,ACtCts,Original,Internal,Origin,ItemCost,HardCost,CompCode,ActItemCost,ImpPrice,Labour " & _
                          "FROM tblImport WHERE ImportNo = " & CDbl(txtImportNo.Text) & " AND SupParcelNo = '" & cmbOrgAssort.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                txtLotNo.Text = rsComSql.Fields("LotNo").Value
                txtTotPcs.Text = rsComSql.Fields("ActPcs").Value
                txtTotCts.Text = Format(rsComSql.Fields("ActCts").Value, "#0.000")
                txtOrigin.Text = rsComSql.Fields("Origin").Value
                txtListCost.Text = rsComSql.Fields("ItemCost").Value
                txtAnaCost.Text = rsComSql.Fields("HardCost").Value
                txtDiaCost.Text = rsComSql.Fields("ActItemCost").Value
                txtDiaLabCost.Text = rsComSql.Fields("ImpPrice").Value
                txtLabour.Text = rsComSql.Fields("Labour").Value
                txtCompCode.Text = rsComSql.Fields("CompCode").Value
                If rsComSql.Fields("Original").Value = 1 Then
                    chkOriginal.Checked = True
                Else
                    chkOriginal.Checked = False
                End If
                If rsComSql.Fields("Internal").Value = 1 Then
                    chkInternal.Checked = True
                Else
                    chkInternal.Checked = False
                End If
            End If
            rsComSql = Nothing
        End If
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
        Dim intRow, m_LotNo As Integer
        Dim strAssortment As String
        Dim dblTotPcs, dblTotCts As Double
        Dim dblPrice As Double
        Dim strSizeRange As String

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
                If xlWorkSheet.Cells(intRow, 1).Value = "" Then Exit For

                strAssortment = xlWorkSheet.Cells(intRow, 1).Value
                strSizeRange = Trim(xlWorkSheet.Cells(intRow, 5).Value)

                dblPrice = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT ItemName,ListCost FROM tblDCLPermanents WHERE ItemName = '" & strAssortment & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    MsgBox("Invalid Assortment - " & strAssortment, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                Else
                    If chkSpecial.Checked = False Then
                        dblPrice = rsComSql.Fields("ListCost").Value
                        If dblPrice = 0 Then
                            dblPrice = xlWorkSheet.Cells(intRow, 4).Value
                        End If
                    Else
                        dblPrice = xlWorkSheet.Cells(intRow, 4).Value
                    End If
                End If
                rsComSql = Nothing

                If Len(strSizeRange) = 0 Then
                    strSizeRange = "0"
                End If

                flxDetails.Rows.Add(txtLotNo.Text,
                                    strAssortment,
                                    cmbOrgAssort.Text,
                                    Trim(xlWorkSheet.Cells(intRow, 2).Value),
                                    Math.Round(CDbl(xlWorkSheet.Cells(intRow, 3).Value), 3),
                                    dblPrice,
                                    False,
                                    strSizeRange,
                                    dblPrice,
                                    dblPrice * Math.Round(CDbl(xlWorkSheet.Cells(intRow, 3).Value), 3))

                dblTotPcs = dblTotPcs + CDbl(Trim(xlWorkSheet.Cells(intRow, 2).Value))
                dblTotCts = dblTotCts + CDbl(Trim(xlWorkSheet.Cells(intRow, 3).Value))

            Next
            xlWorkSheet = Nothing
            xlWorkBook.Close()
            xlApp.Quit()

            txtPcs.Text = dblTotPcs
            txtCts.Text = Math.Round(dblTotCts, 3)

            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)

            If chkSpecial.Checked = False Then
                Process()
            End If

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

    Private Sub frm_DCLDeptTransLot_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub Process()
        Dim intRow As Integer
        Dim dblTotBase As Double
        Dim dblTotInv As Double
        Dim dblAdjValue As Double
        'Dim dblAdjCost As Double

        Dim dblDiaValue As Double
        Dim dblDiaCost As Double

        Dim dblDiaLabValue As Double
        Dim dblDiaLabCost As Double

        Dim dblLabourValue As Double
        Dim dblLabourCost As Double

        If flxDetails.Rows.Count >= 1 Then
            If CInt(txtPcs.Text) <> CInt(txtTotPcs.Text) Then
                MsgBox("Pcs not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(txtCts.Text) <> CDbl(txtTotCts.Text) Then
                MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            dblTotBase = 0
            For intRow = 0 To flxDetails.Rows.Count - 1
                dblTotBase = dblTotBase + (CDbl(flxDetails.Item(9, intRow).Value))
            Next

            dblTotInv = CDbl(txtAnaCost.Text) * CDbl(txtTotCts.Text)
            dblDiaValue = CDbl(txtDiaCost.Text) * CDbl(txtTotCts.Text)
            dblDiaLabValue = CDbl(txtDiaLabCost.Text) * CDbl(txtTotCts.Text)
            dblLabourValue = CDbl(txtLabour.Text)

            'If dblTotInv > dblTotBase Then
            '    For intRow = 0 To flxDetails.Rows.Count - 1
            '        If dblTotBase > 0 Then
            '            dblAdjValue = (dblTotInv / dblTotBase) * (CDbl(flxDetails.Item(4, intRow).Value) * CDbl(flxDetails.Item(5, intRow).Value))
            '            dblAdjCost = dblAdjValue / CDbl(flxDetails.Item(4, intRow).Value)
            '            flxDetails.Item(5, intRow).Value = Format(Math.Round(dblAdjCost, 2), "#0.00")
            '        End If

            '        If dblDiaValue > 0 Then
            '            dblAdjValue = (dblDiaValue / dblTotBase) * (CDbl(flxDetails.Item(4, intRow).Value) * CDbl(flxDetails.Item(8, intRow).Value))
            '            dblDiaCost = dblAdjValue / CDbl(flxDetails.Item(4, intRow).Value)
            '            flxDetails.Item(10, intRow).Value = Format(Math.Round(dblDiaCost, 2), "#0.00")
            '        End If

            '        If dblDiaLabValue > 0 Then
            '            dblAdjValue = (dblDiaLabValue / dblTotBase) * (CDbl(flxDetails.Item(4, intRow).Value) * CDbl(flxDetails.Item(8, intRow).Value))
            '            dblDiaLabCost = dblAdjValue / CDbl(flxDetails.Item(4, intRow).Value)
            '            flxDetails.Item(11, intRow).Value = Format(Math.Round(dblDiaLabCost, 2), "#0.00")
            '        End If
            '    Next
            'Else
            '    For intRow = 0 To flxDetails.Rows.Count - 1
            '        If dblDiaValue > 0 Then
            '            dblAdjValue = (dblDiaValue / dblTotBase) * (CDbl(flxDetails.Item(4, intRow).Value) * CDbl(flxDetails.Item(8, intRow).Value))
            '            dblDiaCost = dblAdjValue / CDbl(flxDetails.Item(4, intRow).Value)
            '            flxDetails.Item(10, intRow).Value = Format(Math.Round(dblDiaCost, 2), "#0.00")
            '        End If

            '        If dblDiaLabValue > 0 Then
            '            dblAdjValue = (dblDiaLabValue / dblTotBase) * (CDbl(flxDetails.Item(4, intRow).Value) * CDbl(flxDetails.Item(8, intRow).Value))
            '            dblDiaLabCost = dblAdjValue / CDbl(flxDetails.Item(4, intRow).Value)
            '            flxDetails.Item(11, intRow).Value = Format(Math.Round(dblDiaLabCost, 2), "#0.00")
            '        End If
            '    Next
            'End If

            For intRow = 0 To flxDetails.Rows.Count - 1
                dblDiaCost = 0
                If dblDiaValue <> 0 And dblTotBase <> 0 Then
                    dblAdjValue = (dblDiaValue / dblTotBase) * (CDbl(flxDetails.Item(4, intRow).Value) * CDbl(flxDetails.Item(8, intRow).Value))
                    dblDiaCost = dblAdjValue / CDbl(flxDetails.Item(4, intRow).Value)
                    flxDetails.Item(10, intRow).Value = Format(Math.Round(dblDiaCost, 2), "#0.00")
                Else
                    flxDetails.Item(10, intRow).Value = Format(Math.Round(dblDiaCost, 2), "#0.00")
                End If

                dblDiaLabCost = 0
                If dblDiaLabValue <> 0 And dblTotBase <> 0 Then
                    dblAdjValue = (dblDiaLabValue / dblTotBase) * (CDbl(flxDetails.Item(4, intRow).Value) * CDbl(flxDetails.Item(8, intRow).Value))
                    dblDiaLabCost = dblAdjValue / CDbl(flxDetails.Item(4, intRow).Value)
                    flxDetails.Item(11, intRow).Value = Format(Math.Round(dblDiaLabCost, 2), "#0.00")
                Else
                    flxDetails.Item(11, intRow).Value = Format(Math.Round(dblDiaLabCost, 2), "#0.00")
                End If

                dblLabourCost = 0
                If dblLabourValue <> 0 And dblTotBase <> 0 Then
                    dblAdjValue = (dblLabourValue / dblTotBase) * (CDbl(flxDetails.Item(4, intRow).Value) * CDbl(flxDetails.Item(8, intRow).Value))
                    dblLabourCost = dblAdjValue / CDbl(flxDetails.Item(4, intRow).Value)
                    flxDetails.Item(12, intRow).Value = Format(Math.Round(dblLabourCost, 2), "#0.00")
                Else
                    flxDetails.Item(12, intRow).Value = Format(Math.Round(dblLabourCost, 2), "#0.00")
                End If
            Next
        End If
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub
End Class