
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_LabEntry

    Private Sub frm_LabEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_MasterData(cmbEntryPoint, 1)
        Load_MasterData(cmbMachine, 3)
        Load_MasterData(cmbMachine2, 3)
        Load_MasterData(cmbMachine3, 3)
        Load_MasterData(cmbShape, 4)
        Load_ScreenNo()

        dtpInvDate.Value = Date.Now
        cmbScreen.Text = "1"

        'Load_PackNo()

        If dbConnDiaSales.State = 1 Then
            dbConnDiaSales.Close()
        End If
        dbConnDiaSales.ConnectionString = "Provider=SQLOLEDB;Data Source='" & strServerName & "';Connect Timeout=60;Initial Catalog=DiaSales;Integrated Security=SSPI"
        dbConnDiaSales.Open()
    End Sub

    Private Sub Load_ScreenNo()
        Dim intIndex As Integer

        cmbScreen.Items.Clear()
        For intIndex = 1 To 5
            cmbScreen.Items.Add(intIndex)
        Next
    End Sub

    Private Sub Load_MasterData(ByVal cmbSample As System.Windows.Forms.ComboBox, ByVal intType As Integer)
        Dim rsGrdType As New ADODB.Recordset

        cmbSample.Items.Clear()
        rsGrdType = New ADODB.Recordset
        rsGrdType.Open("SELECT * FROM tblDCLLabCodes WHERE Type = " & intType & " ORDER BY Code", AdoCN, 1, 1)
        If rsGrdType.RecordCount Then
            rsGrdType.MoveFirst()
            While Not rsGrdType.EOF
                cmbSample.Items.Add(rsGrdType.Fields("Description").Value)
                rsGrdType.MoveNext()
            End While
        End If
        rsGrdType = Nothing
    End Sub

    Private Sub Load_PackNo()
        Dim rsGrdType As New ADODB.Recordset

        cmbPackNo.Items.Clear()
        rsGrdType = New ADODB.Recordset
        rsGrdType.Open("SELECT PackingListNo FROM tblGrading_Pack WHERE OK = 0 GROUP BY PackingListNo ORDER BY PackingListNo", AdoCN, 1, 1)
        If rsGrdType.RecordCount Then
            rsGrdType.MoveFirst()
            While Not rsGrdType.EOF
                cmbPackNo.Items.Add(rsGrdType.Fields("PackingListNo").Value)
                rsGrdType.MoveNext()
            End While
        End If
        rsGrdType = Nothing
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub Save()
        If txtEmpNo.Text = "" Then Exit Sub
        If cmbEntryPoint.Text = "" Then Exit Sub
        If cmbMachine.Text = "" Then Exit Sub
        If cmbShape.Text = "" Then Exit Sub
        If cmbSize.Text = "" Then Exit Sub

        If txtParNo.Text = "" Then Exit Sub
        If txtPktNo.Text = "" Then Exit Sub
        If txtLotNo.Text = "" Then Exit Sub
        If txtLotName.Text = "" Then Exit Sub
        If txtPcs.Text = "" Then Exit Sub
        If txtCts.Text = "" Then Exit Sub
        If txtSize.Text = "" Then Exit Sub

        If txtAmsRepNo.Text = "" Then Exit Sub

        If cmbPackNo.Text = "" Then Exit Sub

        If cmbScreen.Text = "" Then Exit Sub

        ConvertToZeros()

        'If Hour(dtpStartTime.Value) < 7 Then
        '    MsgBox("Invalid Start Time", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '    Exit Sub
        'End If

        'If Hour(dtpStartTime.Value) > 21 Then
        '    MsgBox("Invalid End Time", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '    Exit Sub
        'End If

        'If dtpStartTime.Value > dtpEndTime.Value Then
        '    MsgBox("Invalid Time Range 1", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '    Exit Sub
        'End If

        'If dtpStartTime2.Value > dtpEndTime2.Value Then
        '    MsgBox("Invalid Time Range 2", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '    Exit Sub
        'End If

        'If dtpStartTime3.Value > dtpEndTime3.Value Then
        '    MsgBox("Invalid Time Range 3", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '    Exit Sub
        'End If

        dtpToday = GetToday()

        If CDate(Format(dtpRecDate.Value, "MM/dd/yyyy")) > CDate(Format(dtpToday, "MM/dd/yyyy")) Then
            MsgBox("Invalid Received Date", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If CDate(Format(dtpDate1.Value, "MM/dd/yyyy")) > CDate(Format(dtpDate2.Value, "MM/dd/yyyy")) Then
            MsgBox("Invalid Screen Date 1/2", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If CDate(Format(dtpDate2.Value, "MM/dd/yyyy")) > CDate(Format(dtpDate3.Value, "MM/dd/yyyy")) Then
            MsgBox("Invalid Screen Date 2/3", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        'If cmbShape.Text = "Round Brilliant" And cmbEntryPoint.Text = "Exports" Then
        '    If CDbl(txtPcs.Text) <> CDbl(txtLost.Text) + CDbl(txtPass.Text) + CDbl(txtNonD.Text) + CDbl(txtSyn.Text) Then
        '        MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '        Exit Sub
        '    End If
        'Else
        '    If CDbl(txtPcs.Text) <> CDbl(txtLost.Text) + CDbl(txtPass.Text) + CDbl(txtNonD.Text) + CDbl(txtSyn.Text) + CDbl(txtLost2.Text) + CDbl(txtPass2.Text) + _
        '    CDbl(txtNonD2.Text) + CDbl(txtSyn2.Text) + CDbl(txtLost3.Text) + CDbl(txtPass3.Text) + CDbl(txtNonD3.Text) + CDbl(txtSyn3.Text) + CDbl(txtRef3.Text) Then
        '        MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '        Exit Sub
        '    End If
        'End If
        
        If CDbl(txtPcs.Text) <> CDbl(txtLost.Text) + CDbl(txtPass.Text) + CDbl(txtRef1.Text) + CDbl(txtNonD.Text) + CDbl(txtSyn.Text) Then
            MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        

        If CheckEmployee(Trim(txtEmpNo.Text)) = False Then
            MsgBox("Invalid Employee 1", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtEmpNo2.Text <> "" Then
            If CheckEmployee(Trim(txtEmpNo2.Text)) = False Then
                MsgBox("Invalid Employee 2", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        End If

        If txtEmpNo3.Text <> "" Then
            If CheckEmployee(Trim(txtEmpNo3.Text)) = False Then
                MsgBox("Invalid Employee 3", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        End If

        If cmbMachine2.Text <> "" Then
            If CDbl(txtRef1.Text) <> CDbl(txtLost2.Text) + CDbl(txtPass2.Text) + CDbl(txtRef2.Text) + CDbl(txtNonD2.Text) + CDbl(txtSyn2.Text) Then
                MsgBox("Invalid Pcs Screen 2", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        End If

        If cmbMachine3.Text <> "" Then
            If CDbl(txtRef2.Text) <> CDbl(txtLost3.Text) + CDbl(txtPass3.Text) + CDbl(txtRef3.Text) + CDbl(txtNonD3.Text) + CDbl(txtSyn3.Text) Then
                MsgBox("Invalid Pcs Screen 3", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        End If

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblAMSLabExcel WHERE SupParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Screen2 = " & cmbScreen.Text & "", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            AdoCN.Execute("INSERT INTO tblAMSLabExcel(Type, DateReceived, TimeReceived, LotID, SupParNo, PktNo, EntryArea, EntryType, LotName, Cts, ApproxPcs, Size, ScreeningDate, StartTime, " & _
                            "EndTime, Controller, Equipment, AMS2Report, Pass, Refer1, AMS2NonDiamond, Pass2, Refer2, Refer3, NonDiamond, Synthetic, Status, Comments, Remaindates, NxtCalbDate, " & _
                            "SIZERANGE, Lost, PackingListNo, ExpDate, PassCts, Refer1Cts, PassCts2, Refer2Cts, PassCts3, Refer3Cts, Pass3, ScreeningDate2, StartTime2, EndTime2, Controller2, " & _
                            "Equipment2, NonDiamond2, Synthetic2, Lost2, ScreeningDate3, StartTime3, EndTime3, Controller3, Equipment3, NonDiamond3, Synthetic3, Lost3,  NonDiamondCts, SyntheticCts, NonDiamondCts2, " & _
                            "SyntheticCts2, NonDiamondCts3, SyntheticCts3, ClientGoods, Screen2, OrderNo) " & _
                          "VALUES('" & cmbEntryPoint.Text & "','" & Format(dtpRecDate.Value, "MM/dd/yyyy") & "','" & Format(dtpStartTime.Value, "HH:mm:ss") & "','" & UCase(txtLotNo.Text) & "','" & UCase(txtParNo.Text) & "','" & UCase(txtPktNo.Text) & "'," & _
                            "'" & cmbEntryPoint.Text & "','NORMAL','" & UCase(txtLotName.Text) & "','" & CDbl(txtCts.Text) & "','" & CDbl(txtPcs.Text) & "','" & CDbl(txtSize.Text) & "','" & Format(dtpDate1.Value, "MM/dd/yyyy") & "','" & Format(dtpStartTime.Value, "HH:mm:ss") & "'," & _
                            "'" & Format(dtpEndTime.Value, "HH:mm:ss") & "','" & UCase(txtEmpNo.Text) & "','" & cmbMachine.Text & "','" & UCase(txtAmsRepNo.Text) & "','" & CDbl(txtPass.Text) & "','" & CDbl(txtRef1.Text) & "',0,'" & CDbl(txtPass2.Text) & "','" & CDbl(txtRef2.Text) & "','" & CDbl(txtRef3.Text) & "'," & _
                            "'" & CDbl(txtNonD.Text) & "','" & CDbl(txtSyn.Text) & "','" & txtStatus.Text & "','" & txtComment.Text & "',0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & cmbSize.Text & "','" & CDbl(txtLost.Text) & "','" & CDbl(cmbPackNo.Text) & "'," & _
                            "'" & Format(dtpInvDate.Value, "MM/dd/yyyy") & "','" & CDbl(txtPassCts.Text) & "','" & CDbl(txtRef1Cts.Text) & "','" & CDbl(txtPassCts2.Text) & "','" & CDbl(txtRef2Cts.Text) & "','" & CDbl(txtPassCts3.Text) & "','" & CDbl(txtRef3Cts.Text) & "','" & CDbl(txtPass3.Text) & "'," & _
                            "'" & Format(dtpDate2.Value, "MM/dd/yyyy") & "','" & Format(dtpStartTime2.Value, "HH:mm:ss") & "','" & Format(dtpEndTime2.Value, "HH:mm:ss") & "','" & UCase(txtEmpNo2.Text) & "','" & cmbMachine2.Text & "','" & CDbl(txtNonD2.Text) & "','" & CDbl(txtSyn2.Text) & "','" & CDbl(txtLost2.Text) & "'," & _
                            "'" & Format(dtpDate3.Value, "MM/dd/yyyy") & "','" & Format(dtpStartTime3.Value, "HH:mm:ss") & "','" & Format(dtpEndTime3.Value, "HH:mm:ss") & "','" & UCase(txtEmpNo3.Text) & "','" & cmbMachine3.Text & "','" & CDbl(txtNonD3.Text) & "','" & CDbl(txtSyn3.Text) & "','" & CDbl(txtLost3.Text) & "'," & _
                            "'" & CDbl(txtNonDCts.Text) & "','" & CDbl(txtSynCts.Text) & "','" & CDbl(txtNonDCts2.Text) & "','" & CDbl(txtSynCts2.Text) & "','" & CDbl(txtNonDCts3.Text) & "','" & CDbl(txtSynCts3.Text) & "'," & IIf(chkSelect.Checked = True, 1, 0) & "," & cmbScreen.Text & ",'" & txtOrderNo.Text & "')")

            MsgBox("Lab Entry Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
        Else
            MsgBox("Already Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing
    End Sub

    Private Sub ClearFields()
        'cmbEntryPoint.Text = ""

        'cmbShape.Text = ""
        cmbSize.Text = ""
        'cmbPackNo.Text = ""

        txtParNo.Text = ""
        txtPktNo.Text = ""
        txtLotNo.Text = ""
        txtLotName.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtSize.Text = ""
        txtAmsRepNo.Text = ""

        txtStatus.Text = ""
        txtComment.Text = ""

        dtpInvDate.Value = Date.Now
        'dtpRecDate.Value = Date.Now

        'dtpDate1.Value = Date.Now
        cmbMachine.Text = ""
        txtPass.Text = ""
        txtPassCts.Text = ""
        txtRef1.Text = ""
        txtRef1Cts.Text = ""
        txtEmpNo.Text = ""
        txtNonD.Text = ""
        txtSyn.Text = ""
        txtLost.Text = ""

        dtpDate2.Value = Date.Now
        cmbMachine2.Text = ""
        txtPass2.Text = ""
        txtPassCts2.Text = ""
        txtRef2.Text = ""
        txtRef2Cts.Text = ""
        txtEmpNo2.Text = ""
        txtNonD2.Text = ""
        txtSyn2.Text = ""
        txtLost2.Text = ""

        dtpDate3.Value = Date.Now
        cmbMachine3.Text = ""
        txtPass3.Text = ""
        txtPassCts3.Text = ""
        txtRef3.Text = ""
        txtRef3Cts.Text = ""
        txtEmpNo3.Text = ""
        txtNonD3.Text = ""
        txtSyn3.Text = ""
        txtLost3.Text = ""

        txtNonDCts.Text = ""
        txtNonDCts2.Text = ""
        txtNonDCts3.Text = ""
        txtSynCts.Text = ""
        txtSynCts2.Text = ""
        txtSynCts3.Text = ""

        cmbScreen.Text = "1"

        txtPInvNo.Text = ""
        txtCInvNo.Text = ""
        txtConParNo.Text = ""
        txtOrderNo.Text = ""
    End Sub

    Private Sub txtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtCts.Focus()
        End If
    End Sub

    Private Sub txtPass_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPass.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtPassCts.Focus()
        End If
    End Sub

    Private Sub txtRef1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRef1.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtRef1Cts.Focus()
        End If
    End Sub

    Private Sub txtNonD_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNonD.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtNonDCts.Focus()
        End If
    End Sub

    Private Sub txtSyn_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSyn.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtSynCts.Focus()
        End If
    End Sub

    Private Sub txtCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtCts.Text)
        If Asc(e.KeyChar) = 13 Then
            If Len(txtPcs.Text) > 0 And Len(txtCts.Text) > 0 Then
                If CDbl(txtPcs.Text) > 0 And CDbl(txtCts.Text) > 0 Then
                    txtSize.Text = Math.Round(CDbl(txtCts.Text) / CDbl(txtPcs.Text), 3)
                End If
            End If
            txtSize.Focus()
        End If
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        Dim blnFound As Boolean
        Dim intType As Integer

        If Asc(e.KeyChar) = 13 Then
            'Imports DiaStock
            blnFound = False
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT LotNo, AssortmentNo, SupplierRefNo, ConRefNo FROM tblImport WHERE SupParcelNo = '" & txtParNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                blnFound = True
                txtLotNo.Text = rsComSql.Fields("LotNo").Value
                txtLotName.Text = rsComSql.Fields("AssortmentNo").Value
                txtPInvNo.Text = rsComSql.Fields("SupplierRefNo").Value
                txtCInvNo.Text = rsComSql.Fields("ConRefNo").Value
                txtOrderNo.Text = ""
            End If
            rsComSql = Nothing

            If Len(txtCInvNo.Text) > 0 Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SupParcelNo FROM tblImport WHERE SupplierRefNo = '" & txtCInvNo.Text & "' AND AssortmentNo = '" & txtLotName.Text & "' ORDER BY SupParcelNo", dbConnDiaSales, 1, 1)
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        txtConParNo.Text = txtConParNo.Text & rsComSql.Fields("SupParcelNo").Value & ", "

                        rsComSql.MoveNext()
                    End While
                End If
                rsComSql = Nothing
            End If

            'Rounds Parcels
            blnFound = False
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT OrigParcelNo, GrpParNo FROM tblParcel WHERE GrpParNo = '" & txtParNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT LotNo, AssortmentNo FROM tblImport WHERE SupParcelNo = '" & rsComSql.Fields("OrigParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    blnFound = True
                    txtLotNo.Text = rsComSql_1.Fields("LotNo").Value
                    txtLotName.Text = rsComSql_1.Fields("AssortmentNo").Value
                    txtOrderNo.Text = ""
                End If
                rsComSql_1 = Nothing
            End If
            rsComSql = Nothing

            'Polish Box Transfer
            If blnFound = False Then
                If IsNumeric(txtParNo.Text) Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT ID, Assortment, Pcs, Cts FROM tblPOLTransfer WHERE ID = '" & CDbl(txtParNo.Text) & "'", dbConnDiaSales, 1, 1)
                    If rsComSql.RecordCount Then
                        blnFound = True
                        txtLotNo.Text = rsComSql.Fields("ID").Value
                        txtLotName.Text = rsComSql.Fields("Assortment").Value
                        txtPcs.Text = rsComSql.Fields("Pcs").Value
                        txtCts.Text = rsComSql.Fields("Cts").Value
                        txtOrderNo.Text = ""
                    End If
                    rsComSql = Nothing
                End If
            End If

            'Polish Box
            If blnFound = False Then
                If IsNumeric(txtParNo.Text) Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT ID, Assortment, Pcs, Cts FROM tblPOLSales WHERE ID = '" & CDbl(txtParNo.Text) & "'", dbConnDiaSales, 1, 1)
                    If rsComSql.RecordCount Then
                        blnFound = True
                        txtLotNo.Text = rsComSql.Fields("ID").Value
                        txtLotName.Text = rsComSql.Fields("Assortment").Value
                        txtPcs.Text = rsComSql.Fields("Pcs").Value
                        txtCts.Text = rsComSql.Fields("Cts").Value
                        txtOrderNo.Text = ""
                    End If
                    rsComSql = Nothing
                End If
            End If

            'Imports DiaSales
            If blnFound = False Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT LotNo, AssortmentNo FROM tblImport WHERE SupParcelNo = '" & txtParNo.Text & "'", dbConnDiaSales, 1, 1)
                If rsComSql.RecordCount Then
                    blnFound = True
                    txtLotNo.Text = rsComSql.Fields("LotNo").Value
                    txtLotName.Text = rsComSql.Fields("AssortmentNo").Value
                    txtOrderNo.Text = ""
                End If
                rsComSql = Nothing
            End If

            If blnFound = False Then
                If IsNumeric(txtParNo.Text) Then
                    'Forevermark
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT ID FROM tblGrading_Box_Forever WHERE ID = '" & CDbl(txtParNo.Text) & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        blnFound = True
                        intType = 1
                    End If
                    rsComSql = Nothing

                    If blnFound = False Then
                        'Grading PCU_N
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT ID FROM tblGrading_PackingListPCU WHERE ID = '" & CDbl(txtParNo.Text) & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            blnFound = True
                            intType = 2
                        End If
                        rsComSql = Nothing
                    End If

                    If blnFound = False Then
                        'Colombo Niru
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT ID FROM NiruStock.dbo.tblGrading_PackingListM WHERE ID = '" & CDbl(txtParNo.Text) & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            blnFound = True
                            intType = 3
                        End If
                        rsComSql = Nothing
                    End If

                    rsComSql = New ADODB.Recordset
                    Select Case intType
                        Case 1
                            rsComSql.Open("SELECT ID, Assortment FROM tblGrading_Box_Forever WHERE ID = '" & CDbl(txtParNo.Text) & "'", AdoCN, 1, 1)
                            If rsComSql.RecordCount Then
                                txtLotName.Text = rsComSql.Fields("Assortment").Value
                                txtLotNo.Text = rsComSql.Fields("ID").Value
                                txtOrderNo.Text = ""
                            End If
                        Case 2
                            rsComSql.Open("SELECT ID, Assortment, OrderNo, ActPcs, ActCts FROM tblGrading_PackingListPCU WHERE ID = '" & CDbl(txtParNo.Text) & "'", AdoCN, 1, 1)
                            If rsComSql.RecordCount Then
                                txtLotName.Text = rsComSql.Fields("Assortment").Value
                                txtLotNo.Text = rsComSql.Fields("ID").Value
                                txtOrderNo.Text = rsComSql.Fields("OrderNo").Value
                                txtPcs.Text = rsComSql.Fields("ActPcs").Value
                                txtCts.Text = rsComSql.Fields("ActCts").Value
                            End If
                        Case 3
                            rsComSql.Open("SELECT ID, Assortment FROM NiruStock.dbo.tblGrading_PackingListM WHERE ID = '" & CDbl(txtParNo.Text) & "'", AdoCN, 1, 1)
                            If rsComSql.RecordCount Then
                                txtLotName.Text = rsComSql.Fields("Assortment").Value
                                txtLotNo.Text = rsComSql.Fields("ID").Value
                                txtOrderNo.Text = ""
                            End If
                    End Select
                    rsComSql = Nothing
                End If
            End If

            txtPktNo.Focus()
        End If
    End Sub

    Private Sub cmbShape_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbShape.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbSize.Focus()
        End If
    End Sub

    Private Sub cmbShape_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbShape.SelectedIndexChanged
        cmbSize.Items.Clear()

        rsComSql = New ADODB.Recordset
        If cmbShape.Text = "Round Brilliant" Then
            rsComSql.Open("SELECT * FROM tblDCLLabSize WHERE Shape = 'Round Brilliant' ORDER BY SizeRange", AdoCN, 1, 1)
        Else
            rsComSql.Open("SELECT * FROM tblDCLLabSize WHERE Shape = 'Baguette' ORDER BY SizeRange", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbSize.Items.Add(rsComSql.Fields("SizeRange").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtAmsRepNo.Text = txtParNo.Text & "-" & txtPktNo.Text
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub txtEmpNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEmpNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtLost.Focus()
        End If
    End Sub

    Private Sub cmbEntryPoint_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbEntryPoint.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtParNo.Focus()
        End If
    End Sub

    Private Sub cmbMachine_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbMachine.KeyPress
        If Asc(e.KeyChar) = 13 Then
            dtpStartTime.Focus()
        End If
    End Sub

    Private Sub txtPktNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If Len(txtPktNo.Text) > 1 Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblMixPacket WHERE PktOrdNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", dbConnDiaSales, 1, 1)
                If rsComSql.RecordCount Then
                    txtLotName.Text = rsComSql.Fields("AssortNo").Value
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblGrading_RghIssues WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    txtPcs.Text = rsComSql.Fields("IssPcs").Value
                    txtCts.Text = rsComSql.Fields("IssCts").Value
                End If
                rsComSql = Nothing
            End If

            txtPcs.Focus()
        End If
    End Sub

    Private Sub txtSize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSize.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtLotNo.Focus()
        End If
    End Sub

    Private Sub txtLotNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLotNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtLotName.Focus()
        End If
    End Sub

    Private Sub txtLotName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLotName.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbShape.Focus()
        End If
    End Sub

    Private Sub cmbSize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSize.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbPackNo.Focus()
        End If
    End Sub

    Private Sub cmbPackNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbPackNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            dtpDate1.Focus()
        End If
    End Sub

    Private Sub dtpStartTime_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dtpStartTime.KeyPress
        If Asc(e.KeyChar) = 13 Then
            dtpEndTime.Focus()
        End If
    End Sub

    Private Sub dtpEndTime_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dtpEndTime.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtEmpNo.Focus()
        End If
    End Sub

    Private Sub txtStatus_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtStatus.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtComment.Focus()
        End If
    End Sub

    Private Sub txtComment_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtComment.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtLost.Focus()
        End If
    End Sub

    Private Sub txtLost_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLost.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtPass.Focus()
        End If
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptLabSynthetic.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAMSLabExcelExport_Orit.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixLabExcel_Orit.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button4_Click(sender As Object, e As EventArgs) Handles HazelDev_Button4.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAmsLabExcelLotNo.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub txtPassCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPassCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtPassCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtRef1.Focus()
        End If
    End Sub

    Private Sub txtRef1Cts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRef1Cts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRef1Cts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtNonD.Focus()
        End If
    End Sub

    Private Sub HazelDev_Button5_Click(sender As Object, e As EventArgs) Handles HazelDev_Button5.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAMSLabExcelImportPack2021.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button6_Click(sender As Object, e As EventArgs) Handles HazelDev_Button6.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAMSLabExcelExportPack2021.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button7_Click(sender As Object, e As EventArgs) Handles HazelDev_Button7.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAMSLabExcelExportPcu2021.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub dtpDate1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dtpDate1.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbMachine.Focus()
        End If
    End Sub

    Private Sub dtpDate2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dtpDate2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbMachine2.Focus()
        End If
    End Sub

    Private Sub cmbMachine2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbMachine2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            dtpStartTime2.Focus()
        End If
    End Sub

    Private Sub dtpEndTime2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dtpEndTime2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtEmpNo2.Focus()
        End If
    End Sub

    Private Sub txtPass2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPass2.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtPassCts2.Focus()
        End If
    End Sub

    Private Sub dtpEndTime3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dtpEndTime3.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtEmpNo3.Focus()
        End If
    End Sub

    Private Sub txtEmpNo2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEmpNo2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtLost2.Focus()
        End If
    End Sub

    Private Sub txtEmpNo3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEmpNo3.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtLost3.Focus()
        End If
    End Sub

    Private Sub txtLost2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLost2.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtPass2.Focus()
        End If
    End Sub

    Private Sub txtLost3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLost3.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtPass3.Focus()
        End If
    End Sub

    Private Sub txtPass3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPass3.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtPassCts3.Focus()
        End If
    End Sub

    Private Sub txtPassCts2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPassCts2.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtPassCts2.Text)
        If Asc(e.KeyChar) = 13 Then
            txtRef2.Focus()
        End If
    End Sub

    Private Sub txtPassCts3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPassCts3.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtPassCts3.Text)
        If Asc(e.KeyChar) = 13 Then
            txtRef3.Focus()
        End If
    End Sub

    Private Sub txtRef2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRef2.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtRef2Cts.Focus()
        End If
    End Sub

    Private Sub txtRef3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRef3.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtRef3Cts.Focus()
        End If
    End Sub

    Private Sub txtRef2Cts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRef2Cts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRef2Cts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtNonD2.Focus()
        End If
    End Sub

    Private Sub txtRef3Cts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRef3Cts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRef3Cts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtNonD3.Focus()
        End If
    End Sub

    Private Sub txtNonD2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNonD2.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtNonDCts2.Focus()
        End If
    End Sub

    Private Sub txtNonD3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNonD3.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtNonDCts3.Focus()
        End If
    End Sub

    Private Sub txtSyn2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSyn2.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtSynCts2.Focus()
        End If
    End Sub

    Private Sub txtSyn3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSyn3.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtSynCts3.Focus()
        End If
    End Sub

    Private Sub ConvertToZeros()
        If txtLost.Text = "" Then txtLost.Text = "0"
        If txtLost2.Text = "" Then txtLost2.Text = "0"
        If txtLost3.Text = "" Then txtLost3.Text = "0"
        If txtPass.Text = "" Then txtPass.Text = "0"
        If txtPass2.Text = "" Then txtPass2.Text = "0"
        If txtPass3.Text = "" Then txtPass3.Text = "0"
        If txtPassCts.Text = "" Then txtPassCts.Text = "0"
        If txtPassCts2.Text = "" Then txtPassCts2.Text = "0"
        If txtPassCts3.Text = "" Then txtPassCts3.Text = "0"
        If txtRef1.Text = "" Then txtRef1.Text = "0"
        If txtRef2.Text = "" Then txtRef2.Text = "0"
        If txtRef3.Text = "" Then txtRef3.Text = "0"
        If txtRef1Cts.Text = "" Then txtRef1Cts.Text = "0"
        If txtRef2Cts.Text = "" Then txtRef2Cts.Text = "0"
        If txtRef3Cts.Text = "" Then txtRef3Cts.Text = "0"
        If txtNonD.Text = "" Then txtNonD.Text = "0"
        If txtNonD2.Text = "" Then txtNonD2.Text = "0"
        If txtNonD3.Text = "" Then txtNonD3.Text = "0"
        If txtSyn.Text = "" Then txtSyn.Text = "0"
        If txtSyn2.Text = "" Then txtSyn2.Text = "0"
        If txtSyn3.Text = "" Then txtSyn3.Text = "0"
        If txtNonDCts.Text = "" Then txtNonDCts.Text = "0"
        If txtNonDCts2.Text = "" Then txtNonDCts2.Text = "0"
        If txtNonDCts3.Text = "" Then txtNonDCts3.Text = "0"
        If txtSynCts.Text = "" Then txtSynCts.Text = "0"
        If txtSynCts2.Text = "" Then txtSynCts2.Text = "0"
        If txtSynCts3.Text = "" Then txtSynCts3.Text = "0"
    End Sub

    Private Sub dtpStartTime2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dtpStartTime2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            dtpEndTime2.Focus()
        End If
    End Sub

    Private Sub dtpStartTime3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dtpStartTime3.KeyPress
        If Asc(e.KeyChar) = 13 Then
            dtpEndTime3.Focus()
        End If
    End Sub

    Private Sub cmbMachine3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbMachine3.KeyPress
        If Asc(e.KeyChar) = 13 Then
            dtpStartTime3.Focus()
        End If
    End Sub

    Private Sub txtNonDCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNonDCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtNonDCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtSyn.Focus()
        End If
    End Sub

    Private Sub txtSynCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSynCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtSynCts.Text)
        If Asc(e.KeyChar) = 13 Then
            dtpDate2.Focus()
        End If
    End Sub

    Private Sub txtNonDCts2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNonDCts2.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtNonDCts2.Text)
        If Asc(e.KeyChar) = 13 Then
            txtSyn2.Focus()
        End If
    End Sub

    Private Sub txtSynCts2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSynCts2.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtSynCts2.Text)
        If Asc(e.KeyChar) = 13 Then
            dtpDate3.Focus()
        End If
    End Sub

    Private Sub txtNonDCts3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNonDCts3.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtNonDCts3.Text)
        If Asc(e.KeyChar) = 13 Then
            txtSyn3.Focus()
        End If
    End Sub

    Private Sub txtSynCts3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSynCts3.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtSynCts3.Text)
        If Asc(e.KeyChar) = 13 Then
            dtpDate1.Focus()
        End If
    End Sub

    Private Sub HazelDev_Button8_Click(sender As Object, e As EventArgs) Handles HazelDev_Button8.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAmsLabReferSum.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button9_Click(sender As Object, e As EventArgs) Handles HazelDev_Button9.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptLabRefer3.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button10_Click(sender As Object, e As EventArgs) Handles HazelDev_Button10.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptLabSynthetic2.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button11_Click(sender As Object, e As EventArgs) Handles HazelDev_Button11.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixLabExcel_LogBook.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub txtID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtID.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If Len(txtID.Text) > 0 Then
                'Load_LabRecord()
            End If
        End If
    End Sub

    Private Sub Load_LabRecord()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblAMSLabExcel WHERE ID = '" & CDbl(txtID.Text) & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            cmbEntryPoint.Text = rsComSql.Fields("Type").Value
            txtParNo.Text = rsComSql.Fields("SupParNo").Value
            txtPktNo.Text = rsComSql.Fields("PktNo").Value
            txtLotNo.Text = rsComSql.Fields("LotID").Value
            txtLotName.Text = rsComSql.Fields("LotName").Value
            dtpRecDate.Value = rsComSql.Fields("DateReceived").Value
        Else
            MsgBox("Invalid ID", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing
    End Sub

    Private Sub HazelDev_Button12_Click(sender As Object, e As EventArgs) Handles HazelDev_Button12.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptLabRefer3_Date.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button13_Click(sender As Object, e As EventArgs) Handles HazelDev_Button13.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAMSLabExcelExportPack2022.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button14_Click(sender As Object, e As EventArgs) Handles HazelDev_Button14.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptLabNonD.rpt"
        strReportPath = PBReportPath & "Grading\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button15_Click(sender As Object, e As EventArgs) Handles HazelDev_Button15.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAMSLabExcelExport_OK.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button16_Click(sender As Object, e As EventArgs) Handles HazelDev_Button16.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAMSLabExcelExport_REFER.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button17_Click(sender As Object, e As EventArgs) Handles HazelDev_Button17.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAMSLabExcelExport_SYN.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button18_Click(sender As Object, e As EventArgs) Handles HazelDev_Button18.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAMSLabExcelExportPcu2021_1.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button19_Click(sender As Object, e As EventArgs) Handles HazelDev_Button19.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAmsLabExcelLotNoDate.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub cmbScreen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbScreen.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub HazelDev_Button20_Click(sender As Object, e As EventArgs) Handles HazelDev_Button20.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAmsLabExcelPacking.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button21_Click(sender As Object, e As EventArgs) Handles HazelDev_Button21.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAMSLabExcelExport_NOND.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button22_Click(sender As Object, e As EventArgs) Handles HazelDev_Button22.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAmsLabExcelParcel.rpt"
        strReportPath = PBReportPath & "GroupNiru\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button23_Click(sender As Object, e As EventArgs) Handles HazelDev_Button23.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixLabExcel_LogBookCommande.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button24_Click(sender As Object, e As EventArgs) Handles HazelDev_Button24.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPCULabExcelExportPcu2025.rpt"
        strReportPath = PBReportPath & "Precision\" & mReportName
        objForm.Show()
    End Sub
End Class