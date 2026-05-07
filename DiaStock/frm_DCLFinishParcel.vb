
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLFinishParcel
    Dim intNoOfRecords As Integer
    Dim intCounter As Long

    Private Sub frm_DCLFinishParcel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_DepartmentProd(cmbDepartment)
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_PolishReturns()
        Dim strSupParNo As String
        Dim strDCLParNo As String
        Dim strOrderNo As String
        Dim dblPktPcs As Double
        Dim dblPktCts As Double
        Dim dblIssPcs As Double
        Dim dblIssCts As Double
        Dim dblInvPrice As Double
        Dim blnFound As Boolean
        Dim blnAMS2 As Boolean

        blnFound = False
        blnAMS2 = False
        flxDetails.Rows.Clear()
        strSupParNo = ""
        strDCLParNo = ""
        strOrderNo = ""
        dblPktPcs = 0
        dblPktCts = 0
        dblIssPcs = 0
        dblIssCts = 0

        'Production Finished
        rsComSql = New ADODB.Recordset
        If Len(txtParcel.Text) = 0 Then
            Select Case cmbDepartment.Text
                Case "Baguettes"
                    mStrSQL = "SELECT * FROM VW_PFGetFinishedBaguettes ORDER BY SuppParNo,ParcelNo"
                Case "Princess"
                    mStrSQL = "SELECT * FROM VW_PFGetFinishedPrincess ORDER BY OrigParcelNo,ParNo"
                Case "Rounds"
                    mStrSQL = "SELECT * FROM VW_PFGetFinishedRounds ORDER BY OrderNo,SupParNo"
                Case "Emerald", "Davinci", "Lamour", "Opening", "Princess2", "Rounds3", "Rounds4", "Colombo Niru", "Baguettes2", "Baguettes3", "Emerald2", "Emerald3", "Carrer", "RoundsNLE", "Asscher", "Radiant"
                    mStrSQL = "SELECT * FROM VW_PFGetFinishedRoundsExt WHERE Department = '" & cmbDepartment.Text & "' ORDER BY OrgParNo,ParNo"
                Case Else
                    MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
            End Select
        Else
            Select Case cmbDepartment.Text
                Case "Baguettes"
                    mStrSQL = "SELECT * FROM VW_PFGetFinishedBaguettes WHERE SuppParNo = '" & txtParcel.Text & "' ORDER BY SuppParNo,ParcelNo"
                Case "Princess"
                    mStrSQL = "SELECT * FROM VW_PFGetFinishedPrincess WHERE OrigParcelNo = '" & txtParcel.Text & "' ORDER BY OrigParcelNo,ParNo"
                Case "Rounds"
                    mStrSQL = "SELECT * FROM VW_PFGetFinishedRounds WHERE OrderNo = '" & txtParcel.Text & "' ORDER BY OrderNo,SupParNo"
                Case "Emerald", "Davinci", "Lamour", "Opening", "Princess2", "Rounds3", "Rounds4", "Colombo Niru", "Baguettes2", "Baguettes3", "Emerald2", "Emerald3", "Carrer", "RoundsNLE", "Asscher", "Radiant"
                    mStrSQL = "SELECT * FROM VW_PFGetFinishedRoundsExt WHERE Department = '" & cmbDepartment.Text & "' AND OrgParNo = '" & txtParcel.Text & "'  ORDER BY OrgParNo,ParNo"
                Case Else
                    MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
            End Select
        End If
        
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            intNoOfRecords = rsComSql.RecordCount

            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            blnFound = True
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                intCounter = intCounter + 1
                'If rsComSql.Fields("ParcelNo").Value = "DB1084W" Then
                '    MsgBox(rsComSql.Fields("ParcelNo").Value)
                'End If
                Select Case cmbDepartment.Text
                    Case "Baguettes"
                        strSupParNo = rsComSql.Fields("SuppParNo").Value
                        strDCLParNo = rsComSql.Fields("ParcelNo").Value
                        strOrderNo = rsComSql.Fields("OrderNo").Value
                        dblPktPcs = rsComSql.Fields("PacketPcs").Value
                        dblPktCts = rsComSql.Fields("RghCts").Value

                        dblIssPcs = dblPktPcs
                        dblIssCts = dblPktCts

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT Sum(PCUPcs) As RejPcs, Sum(PCUPCts) As RejCts " & _
                                        "FROM tblBAGReturns " & _
                                        "WHERE ParNo = '" & strDCLParNo & "'", AdoCN, 1, 1)
                        If IsNumeric(rsComSql_1.Fields("RejCts").Value) Then
                            dblIssCts = dblIssCts - rsComSql_1.Fields("RejCts").Value
                        End If

                        If IsNumeric(rsComSql_1.Fields("RejPcs").Value) Then
                            dblIssPcs = dblIssPcs - rsComSql_1.Fields("RejPcs").Value
                        End If
                        rsComSql_1 = Nothing
                        blnAMS2 = True

                    Case "Princess"
                        strSupParNo = rsComSql.Fields("OrigParcelNo").Value
                        strDCLParNo = rsComSql.Fields("ParNo").Value
                        strOrderNo = rsComSql.Fields("PktOrdNo").Value
                        dblPktPcs = rsComSql.Fields("PktPcs").Value
                        dblPktCts = rsComSql.Fields("RghCts").Value

                        dblIssPcs = dblPktPcs
                        dblIssCts = dblPktCts

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT Sum(PCUPcs) As RejPcs, Sum(PCUCts) As RejCts " & _
                                        "FROM tblPRReturns " & _
                                        "WHERE ParNo = '" & strDCLParNo & "'", AdoCN, 1, 1)
                        If IsNumeric(rsComSql_1.Fields("RejCts").Value) Then
                            dblIssCts = dblIssCts - rsComSql_1.Fields("RejCts").Value
                        End If

                        If IsNumeric(rsComSql_1.Fields("RejPcs").Value) Then
                            dblIssPcs = dblIssPcs - rsComSql_1.Fields("RejPcs").Value
                        End If
                        rsComSql_1 = Nothing
                        blnAMS2 = True

                    Case "Rounds"
                        strSupParNo = rsComSql.Fields("OrderNo").Value
                        strDCLParNo = rsComSql.Fields("SupParNo").Value
                        strOrderNo = rsComSql.Fields("PktOrdNo").Value
                        dblPktPcs = rsComSql.Fields("PacketPcs").Value
                        dblPktCts = rsComSql.Fields("PacketCts").Value

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT SUM(RghCts) AS RghCts " & _
                                        "FROM tblParcel " & _
                                        "WHERE (Grp <> N'N') AND (ParcelNo = '" & strDCLParNo & "') AND (Depart = 'Rounds')", AdoCN, 1, 1)
                        If Not IsDBNull(rsComSql_1.Fields("RghCts").Value) Then
                            dblPktCts = rsComSql_1.Fields("RghCts").Value
                        End If
                        rsComSql_1 = Nothing

                        dblIssPcs = dblPktPcs
                        dblIssCts = dblPktCts

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT Sum(MacPcs) As RejPcs, Sum(MacCts) As RejCts " & _
                                        "FROM tblRndReturns " & _
                                        "WHERE LEFT(ParNo, 6) = '" & strDCLParNo & "'", AdoCN, 1, 1)
                        If IsNumeric(rsComSql_1.Fields("RejCts").Value) Then
                            dblIssCts = dblIssCts - rsComSql_1.Fields("RejCts").Value
                        End If

                        If IsNumeric(rsComSql_1.Fields("RejPcs").Value) Then
                            dblIssPcs = dblIssPcs - rsComSql_1.Fields("RejPcs").Value
                        End If
                        rsComSql_1 = Nothing
                        blnAMS2 = True

                    Case "Emerald", "Davinci", "Lamour", "Opening", "Princess2", "Rounds3", "Rounds4", "Baguettes2", "Baguettes3", "Emerald2", "Emerald3", "Carrer", "RoundsNLE", "Asscher", "Radiant"
                        If cmbDepartment.Text = "Rounds4" Then
                            If rsComSql.Fields("AMS2").Value = 1 And rsComSql.Fields("YAH").Value = 1 Then
                                blnAMS2 = True
                            Else
                                blnAMS2 = False
                            End If
                        Else
                            blnAMS2 = True
                        End If
                        strSupParNo = rsComSql.Fields("OrgParNo").Value
                        strDCLParNo = rsComSql.Fields("ParNo").Value
                        strOrderNo = rsComSql.Fields("PktOrdNo").Value
                        dblPktPcs = rsComSql.Fields("PacketPcs").Value
                        dblPktCts = rsComSql.Fields("RghCts").Value

                        dblIssPcs = dblPktPcs
                        dblIssCts = dblPktCts

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT Sum(MacPcs) As RejPcs, Sum(MacCts) As RejCts " & _
                                        "FROM tblExtReturns " & _
                                        "WHERE ParNo = '" & strDCLParNo & "' AND Department = '" & cmbDepartment.Text & "' AND Status = 0", AdoCN, 1, 1)
                        If IsNumeric(rsComSql_1.Fields("RejCts").Value) Then
                            dblIssCts = dblIssCts - rsComSql_1.Fields("RejCts").Value
                        End If

                        If IsNumeric(rsComSql_1.Fields("RejPcs").Value) Then
                            dblIssPcs = dblIssPcs - rsComSql_1.Fields("RejPcs").Value
                        End If
                        rsComSql_1 = Nothing

                End Select

                If blnAMS2 = True Then
                    flxDetails.Rows.Add(strSupParNo,
                                        strDCLParNo,
                                        rsComSql.Fields("Assortment").Value,
                                        Format(rsComSql.Fields("Price").Value, "#0.00"),
                                        dblPktPcs,
                                        Format(dblPktCts, "#0.000"),
                                        rsComSql.Fields("FinishedPcs").Value,
                                        Format(rsComSql.Fields("FinishedCts").Value, "#0.000"),
                                        dblIssPcs,
                                        Format(dblIssCts, "#0.000"),
                                        rsComSql.Fields("Charges").Value,
                                        False, "",
                                        cmbDepartment.Text,
                                        strOrderNo)
                End If

                rsComSql.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        rsComSql = Nothing

        'No Charge Parcels
        dblInvPrice = 0
        rsComSql = New ADODB.Recordset
        If Len(txtParcel.Text) = 0 Then
            Select Case cmbDepartment.Text
                Case "Baguettes"
                    mStrSQL = "SELECT * FROM VW_BAGNoChargeParcels ORDER BY OrigParcelNo,GrpParNo"
                Case "Princess"
                    mStrSQL = "SELECT * FROM VW_PRNoChargeParcels ORDER BY OrigParcelNo,GrpParNo"
                Case "Rounds"
                    mStrSQL = "SELECT * FROM VW_RndNoChargeParcels WHERE Depart = '" & cmbDepartment.Text & "' ORDER BY OrigParcelNo,GrpParNo"
                Case "Emerald", "Davinci", "Lamour", "Opening", "Princess2", "Rounds3", "Rounds4", "Colombo Niru", "Baguettes2", "Baguettes3", "Emerald2", "Emerald3", "Carrer", "RoundsNLE", "Asscher", "Radiant"
                    mStrSQL = "SELECT * FROM VW_ExtNoChargeParcels WHERE Depart = '" & cmbDepartment.Text & "' ORDER BY OrigParcelNo,GrpParNo"
            End Select
        Else
            Select Case cmbDepartment.Text
                Case "Baguettes"
                    mStrSQL = "SELECT * FROM VW_BAGNoChargeParcels WHERE OrigParcelNo = '" & txtParcel.Text & "' ORDER BY OrigParcelNo,GrpParNo"
                Case "Princess"
                    mStrSQL = "SELECT * FROM VW_PRNoChargeParcels WHERE OrigParcelNo = '" & txtParcel.Text & "' ORDER BY OrigParcelNo,GrpParNo"
                Case "Rounds"
                    mStrSQL = "SELECT * FROM VW_RndNoChargeParcels WHERE Depart = '" & cmbDepartment.Text & "' AND OrigParcelNo = '" & txtParcel.Text & "' ORDER BY OrigParcelNo,GrpParNo"
                Case "Emerald", "Davinci", "Lamour", "Opening", "Princess2", "Rounds3", "Rounds4", "Colombo Niru", "Baguettes2", "Baguettes3", "Emerald2", "Emerald3", "Carrer", "RoundsNLE", "Asscher", "Radiant"
                    mStrSQL = "SELECT * FROM VW_ExtNoChargeParcels WHERE Depart = '" & cmbDepartment.Text & "' AND OrigParcelNo = '" & txtParcel.Text & "' ORDER BY OrigParcelNo,GrpParNo"
            End Select
        End If
        
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            intNoOfRecords = rsComSql.RecordCount

            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = intNoOfRecords
            intCounter = 0

            blnFound = True
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                intCounter = intCounter + 1

                If cmbDepartment.Text = "Colombo Niru" Or cmbDepartment.Text = "Rounds4" Then
                    If rsComSql.Fields("AMS2").Value = 1 And rsComSql.Fields("YAH").Value = 1 Then
                        blnAMS2 = True
                    Else
                        If Len(rsComSql.Fields("ConRefNo").Value) > 0 Then
                            blnAMS2 = True
                        Else
                            blnAMS2 = False
                        End If
                    End If
                Else
                    blnAMS2 = True
                End If

                If blnAMS2 = True Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT ItemCost FROM tblImport WHERE SupParcelNo = '" & rsComSql.Fields("OrigParcelNo").Value & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        dblInvPrice = rsComSql_1.Fields("ItemCost").Value
                    Else
                        dblInvPrice = rsComSql.Fields("ItemCost").Value
                    End If
                    rsComSql_1 = Nothing

                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT ROUND(SUM(PktCts * Price) / SUM(PktCts), 2) AS InvPrice FROM dbo.tblParcelDetails " & _
                                    "WHERE (ParcelNo = '" & rsComSql.Fields("GrpParNo").Value & "')", AdoCN, 1, 1)
                    If Not IsDBNull(rsComSql_1.Fields("InvPrice").Value) Then
                        dblInvPrice = rsComSql_1.Fields("InvPrice").Value
                    End If
                    rsComSql_1 = Nothing
                    dblInvPrice = Math.Round(dblInvPrice, 2)

                    flxDetails.Rows.Add(rsComSql.Fields("OrigParcelNo").Value,
                                        rsComSql.Fields("GrpParNo").Value,
                                        rsComSql.Fields("Assortment").Value,
                                        Format(dblInvPrice, "#0.00"),
                                        rsComSql.Fields("IssuedPcs").Value,
                                        Format(rsComSql.Fields("RghCts").Value, "#0.000"),
                                        rsComSql.Fields("IssuedPcs").Value,
                                        Format(rsComSql.Fields("IssuedCts").Value, "#0.000"),
                                        rsComSql.Fields("IssuedPcs").Value,
                                        Format(rsComSql.Fields("RghCts").Value, "#0.000"),
                                        rsComSql.Fields("Charges").Value,
                                        False, "",
                                        cmbDepartment.Text, "")
                End If

                rsComSql.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        rsComSql = Nothing
        ExpProgress.Visible = False
        If blnFound = False Then
            MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub cmbDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDepartment.SelectedIndexChanged
        flxDetails.Rows.Clear()
    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        If optNew.Checked = True Then
            Load_PolishReturns()
        Else
            Load_SavedData()
        End If
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(11, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(11, intRow).Value = False
            Next
        End If
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        flxDetails.Rows.Clear()
        optNew.Checked = True
    End Sub

    Private Sub Save()
        Dim vRecordNo As Double

        If cmbDepartment.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If optNew.Checked = True Then
            Select Case cmbDepartment.Text
                Case "Baguettes"
                    mStrSQL = "SELECT MAX(RecordNo) AS RecordNo FROM tblBAGFinishParcels"
                Case "Princess"
                    mStrSQL = "SELECT MAX(RecordNo) AS RecordNo FROM tblPRFinishedParcels"
                Case "Rounds"
                    mStrSQL = "SELECT MAX(RecordNo) AS RecordNo FROM tblRndFinishParcels"
                Case "Emerald", "Davinci", "Lamour", "Opening", "Princess2", "Rounds3", "Rounds4", "Colombo Niru", "Baguettes2", "Baguettes3", "Emerald2", "Emerald3", "Carrer", "RoundsNLE", "Asscher", "Radiant"
                    mStrSQL = "SELECT MAX(RecordNo) AS RecordNo FROM tblExtFinishParcels"
                Case Else
                    MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
            End Select

            rsComSql = New ADODB.Recordset
            rsComSql.Open(mStrSQL, AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("RecordNo").Value) Then
                    vRecordNo = rsComSql.Fields("RecordNo").Value + 1
                Else
                    vRecordNo = 1
                End If
            End If
            rsComSql = Nothing

            For intRow = 0 To flxDetails.RowCount - 1
                If flxDetails.Item(11, intRow).Value = True Then
                    Select Case cmbDepartment.Text
                        Case "Baguettes"
                            mStrSQL = "INSERT INTO tblBAGFinishParcels(SuppRef,DCLRef,Assortment,AssPrice,FinishedPcs,FinishedCts,PacketPcs,PacketCts,IssuePcs,IssueCts," & _
                                        "RateCode,Export,Status,AuditNo,RecordNo,DoneBy,ModifyBy,OrderNo) " & _
                                      "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(2, intRow).Value & "'," & _
                                        "" & CDbl(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(6, intRow).Value) & "," & CDbl(flxDetails.Item(7, intRow).Value) & "," & _
                                        "" & CDbl(flxDetails.Item(4, intRow).Value) & "," & CDbl(flxDetails.Item(5, intRow).Value) & "," & CDbl(flxDetails.Item(8, intRow).Value) & "," & _
                                        "" & CDbl(flxDetails.Item(9, intRow).Value) & ",'" & flxDetails.Item(10, intRow).Value & "',1,'A',0," & vRecordNo & ",'" & PBUser_EmpNo & "','" & PBUser_EmpNo & "','" & flxDetails.Item(1, intRow).Value & "')"
                            AdoCN.Execute(mStrSQL)
                            AdoCN.Execute("UPDATE tblBagReturns SET Status = 1 WHERE ParNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = 10 AND Status  = 0")
                            AdoCN.Execute("UPDATE tblParcel SET Verify = 1 WHERE Depart = '" & cmbDepartment.Text & "' AND GrpParNo = '" & flxDetails.Item(1, intRow).Value & "' AND Verify  = 0")

                        Case "Princess"
                            mStrSQL = "INSERT INTO tblPRFinishedParcels(SuppParNo,DCLParNo,Assortment,AsstPrice,FinishedPcs,FinishedCts,PacketPcs,PacketCts,IssuePcs,IssueCts," & _
                                        "RateCode,Export,Status,AuditNo,RecordNo,DoneBy,ModifyBy) " & _
                                      "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(2, intRow).Value & "'," & _
                                        "" & CDbl(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(6, intRow).Value) & "," & CDbl(flxDetails.Item(7, intRow).Value) & "," & _
                                        "" & CDbl(flxDetails.Item(4, intRow).Value) & "," & CDbl(flxDetails.Item(5, intRow).Value) & "," & CDbl(flxDetails.Item(8, intRow).Value) & "," & _
                                        "" & CDbl(flxDetails.Item(9, intRow).Value) & ",'" & flxDetails.Item(10, intRow).Value & "',1,'A',0," & vRecordNo & ",'" & PBUser_EmpNo & "','" & PBUser_EmpNo & "')"
                            AdoCN.Execute(mStrSQL)
                            AdoCN.Execute("UPDATE tblPRReturns SET Status = 1 WHERE ParNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = 9 AND Status = 0")
                            AdoCN.Execute("UPDATE tblParcel SET Verify = 1 WHERE Depart = '" & cmbDepartment.Text & "' AND GrpParNo = '" & flxDetails.Item(1, intRow).Value & "' AND Verify  = 0")

                        Case "Rounds"
                            mStrSQL = "INSERT INTO tblRndFinishParcels(SuppRef,DCLRef,Assortment,AssPrice,FinishedPcs,FinishedCts,PacketPcs,PacketCts,IssuePcs,IssueCts," & _
                                        "RateCode,Export,Status,AuditNo,RecordNo,DoneBy,ModifyBy) " & _
                                      "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(2, intRow).Value & "'," & _
                                        "" & CDbl(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(6, intRow).Value) & "," & CDbl(flxDetails.Item(7, intRow).Value) & "," & _
                                        "" & CDbl(flxDetails.Item(4, intRow).Value) & "," & CDbl(flxDetails.Item(5, intRow).Value) & "," & CDbl(flxDetails.Item(8, intRow).Value) & "," & _
                                        "" & CDbl(flxDetails.Item(9, intRow).Value) & ",'" & flxDetails.Item(10, intRow).Value & "',1,'A',0," & vRecordNo & ",'" & PBUser_EmpNo & "','" & PBUser_EmpNo & "')"
                            AdoCN.Execute(mStrSQL)
                            AdoCN.Execute("UPDATE tblRndReturns SET Status = 1 WHERE LEFT(ParNo, 6) = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = 25 AND Status  = 0")
                            AdoCN.Execute("UPDATE tblParcel SET Verify = 1 WHERE Depart = '" & cmbDepartment.Text & "' AND GrpParNo = '" & flxDetails.Item(1, intRow).Value & "' AND Verify  = 0")

                        Case "Emerald", "Davinci", "Lamour", "Opening", "Princess2", "Rounds3", "Rounds4", "Colombo Niru", "Baguettes2", "Baguettes3", "Emerald2", "Emerald3", "Carrer", "RoundsNLE", "Asscher", "Radiant"
                            mStrSQL = "INSERT INTO tblExtFinishParcels(Department,SuppRef,DCLRef,Assortment,AssPrice,FinishedPcs,FinishedCts,PacketPcs,PacketCts,IssuePcs,IssueCts," & _
                                        "RateCode,Export,Status,AuditNo,RecordNo,DoneBy,ModifyBy,OrderNo) " & _
                                      "VALUES('" & cmbDepartment.Text & "','" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(2, intRow).Value & "'," & _
                                        "" & CDbl(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(6, intRow).Value) & "," & CDbl(flxDetails.Item(7, intRow).Value) & "," & _
                                        "" & CDbl(flxDetails.Item(4, intRow).Value) & "," & CDbl(flxDetails.Item(5, intRow).Value) & "," & CDbl(flxDetails.Item(8, intRow).Value) & "," & _
                                        "" & CDbl(flxDetails.Item(9, intRow).Value) & ",'" & flxDetails.Item(10, intRow).Value & "',1,'A',0," & vRecordNo & ",'" & PBUser_EmpNo & "','" & PBUser_EmpNo & "','" & flxDetails.Item(1, intRow).Value & "')"
                            AdoCN.Execute(mStrSQL)
                            AdoCN.Execute("UPDATE tblExtReturns SET Status = 1 WHERE Department = '" & cmbDepartment.Text & "' AND ParNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = 25 AND Status  = 0")
                            AdoCN.Execute("UPDATE tblParcel SET Verify = 1 WHERE Depart = '" & cmbDepartment.Text & "' AND GrpParNo = '" & flxDetails.Item(1, intRow).Value & "' AND Verify  = 0")

                    End Select
                    vRecordNo = vRecordNo + 1
                End If
            Next
            MsgBox("Finish Parcel Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            flxDetails.Rows.Clear()
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                If flxDetails.Item(11, intRow).Value = True Or flxDetails.Item(11, intRow).Value = 1 Then
                    Select Case cmbDepartment.Text
                        Case "Baguettes"
                            mStrSQL = "UPDATE tblBAGFinishParcels SET PacketPcs = " & CDbl(flxDetails.Item(4, intRow).Value) & ",PacketCts = " & CDbl(flxDetails.Item(5, intRow).Value) & ",FinishedPcs = " & CDbl(flxDetails.Item(6, intRow).Value) & ",FinishedCts = " & CDbl(flxDetails.Item(7, intRow).Value) & "," & _
                                        "IssuePcs = " & CDbl(flxDetails.Item(8, intRow).Value) & ",IssueCts = " & CDbl(flxDetails.Item(9, intRow).Value) & ",ModifyBy = '" & PBUser_EmpNo & "',Export = 1,RateCode = '" & flxDetails.Item(10, intRow).Value & "' " & _
                                      "WHERE RecordNo = " & CDbl(flxDetails.Item(12, intRow).Value) & " AND DCLRef = '" & flxDetails.Item(1, intRow).Value & "' AND Status = 'A'"
                        Case "Princess"
                            mStrSQL = "UPDATE tblPRFinishedParcels SET PacketPcs = " & CDbl(flxDetails.Item(4, intRow).Value) & ",PacketCts = " & CDbl(flxDetails.Item(5, intRow).Value) & ",FinishedPcs = " & CDbl(flxDetails.Item(6, intRow).Value) & ",FinishedCts = " & CDbl(flxDetails.Item(7, intRow).Value) & "," & _
                                        "IssuePcs = " & CDbl(flxDetails.Item(8, intRow).Value) & ",IssueCts = " & CDbl(flxDetails.Item(9, intRow).Value) & ",ModifyBy = '" & PBUser_EmpNo & "',Export = 1,RateCode = '" & flxDetails.Item(10, intRow).Value & "' " & _
                                      "WHERE RecordNo = " & CDbl(flxDetails.Item(12, intRow).Value) & " AND DCLParNo = '" & flxDetails.Item(1, intRow).Value & "' AND Status = 'A'"
                        Case "Rounds"
                            mStrSQL = "UPDATE tblRndFinishParcels SET PacketPcs = " & CDbl(flxDetails.Item(4, intRow).Value) & ",PacketCts = " & CDbl(flxDetails.Item(5, intRow).Value) & ",FinishedPcs = " & CDbl(flxDetails.Item(6, intRow).Value) & ",FinishedCts = " & CDbl(flxDetails.Item(7, intRow).Value) & "," & _
                                        "IssuePcs = " & CDbl(flxDetails.Item(8, intRow).Value) & ",IssueCts = " & CDbl(flxDetails.Item(9, intRow).Value) & ",ModifyBy = '" & PBUser_EmpNo & "',Export = 1,RateCode = '" & flxDetails.Item(10, intRow).Value & "' " & _
                                      "WHERE RecordNo = " & CDbl(flxDetails.Item(12, intRow).Value) & " AND DCLRef = '" & flxDetails.Item(1, intRow).Value & "' AND Status = 'A'"
                        Case "Emerald", "Davinci", "Lamour", "Opening", "Princess2", "Rounds3", "Rounds4", "Colombo Niru", "Baguettes2", "Baguettes3", "Emerald2", "Emerald3", "Carrer", "RoundsNLE", "Asscher", "Radiant"
                            mStrSQL = "UPDATE tblExtFinishParcels SET PacketPcs = " & CDbl(flxDetails.Item(4, intRow).Value) & ",PacketCts = " & CDbl(flxDetails.Item(5, intRow).Value) & ",FinishedPcs = " & CDbl(flxDetails.Item(6, intRow).Value) & ",FinishedCts = " & CDbl(flxDetails.Item(7, intRow).Value) & "," & _
                                        "IssuePcs = " & CDbl(flxDetails.Item(8, intRow).Value) & ",IssueCts = " & CDbl(flxDetails.Item(9, intRow).Value) & ",ModifyBy = '" & PBUser_EmpNo & "',Export = 1,RateCode = '" & flxDetails.Item(10, intRow).Value & "' " & _
                                      "WHERE RecordNo = " & CDbl(flxDetails.Item(12, intRow).Value) & " AND DCLRef = '" & flxDetails.Item(1, intRow).Value & "' AND Status = 'A' AND Department = '" & cmbDepartment.Text & "'"
                    End Select
                    AdoCN.Execute(mStrSQL)
                End If
            Next
            MsgBox("Finish Parcel Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            flxDetails.Rows.Clear()
        End If

    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            Save()
        End If
    End Sub

    Private Sub Load_SavedData()
        If cmbDepartment.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        Select Case cmbDepartment.Text
            Case "Baguettes"
                mStrSQL = "SELECT * FROM tblBAGFinishParcels WHERE Status = 'A' ORDER BY SuppRef, DCLRef"
            Case "Princess"
                mStrSQL = "SELECT SuppParNo AS SuppRef,DCLParNo AS DCLRef,Assortment,AsstPrice AS AssPrice,FinishedPcs,FinishedCts,PacketPcs,PacketCts,IssuePcs,IssueCts,RateCode,Export,RecordNo " & _
                          "FROM tblPRFinishedParcels WHERE Status = 'A' ORDER BY SuppParNo,DCLParNo"
            Case "Rounds"
                mStrSQL = "SELECT * FROM tblRndFinishParcels WHERE Status = 'A' ORDER BY SuppRef,DCLRef"
            Case "Emerald", "Davinci", "Lamour", "Opening", "Princess2", "Rounds3", "Rounds4", "Colombo Niru", "Baguettes2", "Baguettes3", "Emerald2", "Emerald3", "Carrer", "RoundsNLE", "Asscher", "Radiant"
                mStrSQL = "SELECT * FROM tblExtFinishParcels WHERE Department = '" & cmbDepartment.Text & "' AND Status = 'A' ORDER BY SuppRef,DCLRef"
            Case Else
                MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
        End Select
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("SuppRef").Value,
                                    rsComSql.Fields("DCLRef").Value,
                                    rsComSql.Fields("Assortment").Value,
                                    Format(rsComSql.Fields("AssPrice").Value, "#0.00"),
                                    rsComSql.Fields("PacketPcs").Value,
                                    Format(rsComSql.Fields("PacketCts").Value, "#0.000"),
                                    rsComSql.Fields("FinishedPcs").Value,
                                    Format(rsComSql.Fields("FinishedCts").Value, "#0.000"),
                                    rsComSql.Fields("IssuePcs").Value,
                                    Format(rsComSql.Fields("IssueCts").Value, "#0.000"),
                                    rsComSql.Fields("RateCode").Value,
                                    rsComSql.Fields("Export").Value,
                                    rsComSql.Fields("RecordNo").Value,
                                    cmbDepartment.Text)

                rsComSql.MoveNext()
            End While
        Else
            MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing
    End Sub

    Private Sub optEdit_CheckedChanged(sender As Object, e As EventArgs) Handles optEdit.CheckedChanged
        flxDetails.Rows.Clear()
    End Sub

    Private Sub optNew_CheckedChanged(sender As Object, e As EventArgs) Handles optNew.CheckedChanged
        flxDetails.Rows.Clear()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(Me.flxDetails)
    End Sub
End Class