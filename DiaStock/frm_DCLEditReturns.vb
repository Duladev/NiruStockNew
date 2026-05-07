
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLEditReturns
    Dim ParcelNo As String
    Dim PacketNo As String

    Private Sub frm_DCLEditReturns_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_DepartmentEdit(cmbDepartment)

        If PBUser_Level <= 3 Then
            cmdDelete.Enabled = True
        Else
            cmdDelete.Enabled = False
        End If
    End Sub

    Private Sub cmbDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDepartment.SelectedIndexChanged
        txtParNo.Focus()
        flxDetails.Rows.Clear()
        txtParNo.Text = ""
        txtPktNo.Text = ""
        txtSec.Text = ""
        chkTrf.Checked = True
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtParNo.Text <> "" Then
                txtParNo.Text = UCase(Trim(txtParNo.Text))
                flxDetails.Rows.Clear()
                txtPktNo.Text = ""
                txtSec.Text = ""
                chkTrf.Checked = True
                txtPktNo.Focus()
            End If
        End If
    End Sub

    Private Sub Show_PacketDetails()
        If txtPktNo.Text <> "" Then
            txtPktNo.Text = UCase(Trim(txtPktNo.Text))
            flxDetails.Rows.Clear()
            txtSec.Text = ""
            rsComSql = New ADODB.Recordset
            Select Case cmbDepartment.Text
                Case "Baguettes"
                    mStrSQL = "SELECT ID,ParNo,PktNo,Sec,EmpNo,RetPcsT + RetPcsB AS RetPcs,RetCts,RetDate,RetTime,RejPcs,RejCts,LostPcs,DoneBy FROM tblBAGReturns WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' ORDER BY Sec"
                Case "Princess"
                    mStrSQL = "SELECT ID,ParNo,PktNo,Sec,EmpNo,RetPcsC + RetPcsP AS RetPcs,RetCtsC + RetCtsP AS RetCts,RetDate,RetTime,RejPcs,RejCts,LostPcs,DoneBy FROM tblPRReturns WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' ORDER BY Sec"
                Case "Rounds"
                    mStrSQL = "SELECT ID,ParNo,PktNo,Sec,EmpNo,RetPcsT + RetPcsB AS RetPcs,RetCts,RetDate,RetTime,RejPcs,RejCts,LostPcs,DoneBy FROM tblRndReturns WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' ORDER BY Sec"
                Case "Emerald", "Davinci", "Lamour", "Opening", "Princess2", "Rounds3", "Rounds4", "Baguettes2", "Baguettes3", "Emerald2", "Emerald3", "Carrer", "RoundsNLE", "Asscher", "Radiant"
                    mStrSQL = "SELECT ID,ParNo,PktNo,Sec,EmpNo,RetPcsT + RetPcsB AS RetPcs,RetCts,RetDate,RetTime,RejPcs,RejCts,LostPcs,DoneBy FROM tblExtReturns WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDepartment.Text & "' ORDER BY Sec"
                Case "Mix"
                    mStrSQL = "SELECT ID,ParNo,PktNo,Sec,EmpNo,RetPcsT + RetPcsB AS RetPcs,RetCts,RetDate,RetTime,RejPcs,RejCts,LostPcs,DoneBy FROM tblMixReturns WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' ORDER BY Sec"
                Case "Precision"
                    mStrSQL = "SELECT ID,ParNo,PktNo,Sec,EmpNo,RetPcsT + RetPcsB AS RetPcs,RetCts,RetDate,RetTime,RejPcs,RejCts,LostPcs,ModifiedBy AS DoneBy FROM tblReturns WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' ORDER BY Sec"
                Case Else
                    If Mid(cmbDepartment.Text, 1, 5) = "Rough" Then
                        mStrSQL = "SELECT 1 AS ID,ParNo,PktNo,Sec,EmpNo,RetPcsT + RetPcsB AS RetPcs,RetCts,RetDate,RetTime,RejPcs,RejCts,LostPcs,UserName AS DoneBy FROM tblRprReturns WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDepartment.Text & "' ORDER BY Sec"
                    Else
                        MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
            End Select
            rsComSql.Open(mStrSQL, AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    flxDetails.Rows.Add(rsComSql.Fields("ID").Value,
                                        rsComSql.Fields("ParNo").Value,
                                        rsComSql.Fields("PktNo").Value,
                                        rsComSql.Fields("Sec").Value,
                                        rsComSql.Fields("RetPcs").Value,
                                        rsComSql.Fields("RetCts").Value,
                                        rsComSql.Fields("EmpNo").Value, "",
                                        Format(rsComSql.Fields("RetDate").Value, "yyyy/MM/dd"),
                                        Format(rsComSql.Fields("RetTime").Value, "hh:mm:ss tt"),
                                        rsComSql.Fields("RejPcs").Value,
                                        rsComSql.Fields("RejCts").Value,
                                        rsComSql.Fields("LostPcs").Value,
                                        rsComSql.Fields("DoneBy").Value)

                    rsComSql.MoveNext()
                End While
            Else
                MsgBox("No Returns", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtPktNo.Focus()
                Exit Sub
            End If
            rsComSql = Nothing
            Check_Transfer()
        End If
    End Sub

    Private Sub txtPktNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Show_PacketDetails()
        End If
    End Sub

    Private Sub Check_Transfer()
        chkTrf.Checked = True
        rsComSql = New ADODB.Recordset
        Select Case cmbDepartment.Text
            Case "Baguettes", "Princess", "Rounds", "Emerald", "Davinci", "Lamour", "Opening", "Princess2", "Rounds3", "Rounds4", "Baguettes2", "Baguettes3", "Emerald2", "Emerald3", "Carrer", "RoundsNLE", "Asscher", "Radiant"
                mStrSQL = "SELECT Status FROM tblGradingTrf WHERE ParcelNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDepartment.Text & "'"
            Case "Precision"
                mStrSQL = "SELECT Status FROM tblPCUFinishOrders WHERE OrderNo = '" & txtParNo.Text & "' AND PacketNo = '" & txtPktNo.Text & "'"
            Case "Mix"
                mStrSQL = "SELECT SUM(RejPcs + LostPcs + BroPcs) AS RejPcs FROM tblMixReturns WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'"
            Case Else
                If Mid(cmbDepartment.Text, 1, 5) = "Rough" Then
                    mStrSQL = "SELECT Trf AS Status FROM tblRPrPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDepartment.Text & "'"
                Else
                    Exit Sub
                End If
        End Select
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If cmbDepartment.Text = "Mix" Then
                If Not IsDBNull(rsComSql.Fields("RejPcs").Value) Then
                    If rsComSql.Fields("RejPcs").Value > 0 Then
                        chkTrf.Checked = True
                    Else
                        chkTrf.Checked = False
                    End If
                Else
                    chkTrf.Checked = True
                End If
                rsComSql_1 = New ADODB.Recordset
                mStrSQL = "SELECT PacketNo FROM tblMixFinishOrders WHERE OrderNo = '" & txtParNo.Text & "' AND PacketNo = '" & txtPktNo.Text & "'"
                rsComSql_1.Open(mStrSQL, AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    chkTrf.Checked = True
                End If
                rsComSql_1 = Nothing
            ElseIf cmbDepartment.Text = "Precision" Then
                If rsComSql.Fields("Status").Value = "F" Or rsComSql.Fields("Status").Value = "E" Then
                    chkTrf.Checked = True
                Else
                    chkTrf.Checked = False
                End If
            Else
                If rsComSql.Fields("Status").Value = 1 Then
                    chkTrf.Checked = True
                Else
                    chkTrf.Checked = False
                End If
            End If
        Else
            chkTrf.Checked = False
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Delete()
        Dim strParcelNo As String
        Dim strPktNo As String

        If cmbDepartment.Text = "" Then Exit Sub
        If txtParNo.Text = "" Then Exit Sub
        If txtPktNo.Text = "" Then Exit Sub
        If txtSec.Text = "" Then Exit Sub

        strParcelNo = ""
        strPktNo = ""
        If chkTrf.Checked = False Then
            PBResponse = MsgBox("Are you sure to Delete?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                If flxDetails.Rows.Count > 0 Then
                    strParcelNo = flxDetails.Item(1, 0).Value
                    strPktNo = flxDetails.Item(2, 0).Value
                    Select Case cmbDepartment.Text
                        Case "Baguettes"
                            AdoCN.Execute("DELETE FROM tblBAGIssues WHERE ParNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Sec >= '" & CInt(txtSec.Text) & "'")
                            AdoCN.Execute("DELETE FROM tblBAGReturns WHERE ParNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Sec >= '" & CInt(txtSec.Text) & "'")
                            AdoCN.Execute("DELETE FROM tblGradingTrf WHERE ParcelNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Department = '" & cmbDepartment.Text & "'")
                        Case "Princess"
                            AdoCN.Execute("DELETE FROM tblPRIssues WHERE ParNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Sec >= '" & CInt(txtSec.Text) & "'")
                            AdoCN.Execute("DELETE FROM tblPRReturns WHERE ParNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Sec >= '" & CInt(txtSec.Text) & "'")
                            AdoCN.Execute("DELETE FROM tblGradingTrf WHERE ParcelNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Department = '" & cmbDepartment.Text & "'")
                        Case "Rounds"
                            AdoCN.Execute("DELETE FROM tblRndIssues WHERE ParNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Sec >= '" & CInt(txtSec.Text) & "'")
                            AdoCN.Execute("DELETE FROM tblRndReturns WHERE ParNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Sec >= '" & CInt(txtSec.Text) & "'")
                            AdoCN.Execute("DELETE FROM tblGradingTrf WHERE ParcelNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Department = '" & cmbDepartment.Text & "'")
                        Case "Emerald", "Davinci", "Lamour", "Opening", "Princess2", "Rounds3", "Rounds4", "Baguettes2", "Baguettes3", "Emerald2", "Emerald3", "Carrer", "RoundsNLE", "Asscher", "Radiant"
                            AdoCN.Execute("DELETE FROM tblExtIssues WHERE ParNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Department = '" & cmbDepartment.Text & "' AND Sec >= '" & CInt(txtSec.Text) & "'")
                            AdoCN.Execute("DELETE FROM tblExtReturns WHERE ParNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Department = '" & cmbDepartment.Text & "' AND Sec >= '" & CInt(txtSec.Text) & "'")
                            AdoCN.Execute("DELETE FROM tblGradingTrf WHERE ParcelNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Department = '" & cmbDepartment.Text & "'")
                        Case "Precision"
                            AdoCN.Execute("DELETE FROM tblIssues WHERE ParNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Sec >= '" & CInt(txtSec.Text) & "'")
                            AdoCN.Execute("DELETE FROM tblReturns WHERE ParNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Sec >= '" & CInt(txtSec.Text) & "'")
                            AdoCN.Execute("DELETE FROM tblPCUFinishOrders WHERE OrderNo = '" & strParcelNo & "' AND PacketNo = '" & strPktNo & "'")
                        Case "Mix"
                            AdoCN.Execute("DELETE FROM tblMixIssues WHERE ParNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Sec >= '" & CInt(txtSec.Text) & "'")
                            AdoCN.Execute("DELETE FROM tblMixReturns WHERE ParNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Sec >= '" & CInt(txtSec.Text) & "'")
                        Case Else
                            If Mid(cmbDepartment.Text, 1, 5) = "Rough" Then
                                AdoCN.Execute("DELETE FROM tblRPrIssues WHERE ParNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Department = '" & cmbDepartment.Text & "' AND Sec >= '" & CInt(txtSec.Text) & "'")
                                AdoCN.Execute("DELETE FROM tblRprReturns WHERE ParNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Department = '" & cmbDepartment.Text & "' AND Sec >= '" & CInt(txtSec.Text) & "'")
                            Else
                                MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                Exit Sub
                            End If
                    End Select
                    Insert_Log("TRANS MODIFIED", cmbDepartment.Text, strParcelNo, strPktNo, CInt(txtSec.Text))
                    MsgBox("Issues/Returns Deleted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

                    txtPktNo.Text = ""
                    txtSec.Text = ""
                    chkTrf.Checked = True
                    txtPktNo.Focus()
                    flxDetails.Rows.Clear()
                End If
            End If
        Else
            MsgBox("Already Transfered to Grading", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim strEmpNo As String

        If cmbDepartment.Text = "" Then Exit Sub
        If txtParNo.Text = "" Then Exit Sub
        If txtPktNo.Text = "" Then Exit Sub

        PBResponse = MsgBox("Are you sure to Edit?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                Select Case cmbDepartment.Text
                    Case "Baguettes"
                    Case "Princess"
                    Case "Rounds"
                    Case "Emerald", "Davinci", "Lamour", "Opening", "Princess2", "Rounds3", "Rounds4", "Baguettes2", "Baguettes3", "Emerald2", "Emerald3", "Carrer", "RoundsNLE", "Asscher", "Radiant"
                    Case "Mix"
                    Case "Precision"
                    Case Else
                        If Mid(cmbDepartment.Text, 1, 5) = "Rough" Then
                        Else
                            MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                End Select

                strEmpNo = UCase(Trim(flxDetails.Item(7, intRow).Value))
                If Len(strEmpNo) > 0 Then
                    rsComSql = New ADODB.Recordset
                    mStrSQL = ("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL3 WHERE FullEmpNo = '" & strEmpNo & "'")
                    rsComSql.Open(mStrSQL, AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        MsgBox("Invalid Employee No - " & strEmpNo, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql = Nothing
                End If
            Next

            For intRow = 0 To flxDetails.Rows.Count - 1
                strEmpNo = UCase(Trim(flxDetails.Item(7, intRow).Value))
                If Len(strEmpNo) > 0 Then
                    Select Case cmbDepartment.Text
                        Case "Baguettes"
                            mStrSQL = "UPDATE tblBAGReturns SET EmpNo = '" & strEmpNo & "',ModifiedBy = '" & PBUser_EmpNo & "' WHERE ID = '" & flxDetails.Item(0, intRow).Value & "'"
                        Case "Princess"
                            mStrSQL = "UPDATE tblPRReturns SET EmpNo = '" & strEmpNo & "',ModifiedBy = '" & PBUser_EmpNo & "' WHERE ID = '" & flxDetails.Item(0, intRow).Value & "'"
                        Case "Rounds"
                            mStrSQL = "UPDATE tblRndReturns SET EmpNo = '" & strEmpNo & "',ModifiedBy = '" & PBUser_EmpNo & "' WHERE ID = '" & flxDetails.Item(0, intRow).Value & "'"
                        Case "Emerald", "Davinci", "Lamour", "Opening", "Princess2", "Rounds3", "Rounds4", "Baguettes2", "Baguettes3", "Emerald2", "Emerald3", "Carrer", "RoundsNLE", "Asscher", "Radiant"
                            mStrSQL = "UPDATE tblExtReturns SET EmpNo = '" & strEmpNo & "',ModifiedBy = '" & PBUser_EmpNo & "' WHERE ID = '" & flxDetails.Item(0, intRow).Value & "'"
                        Case "Mix"
                            mStrSQL = "UPDATE tblMixReturns SET EmpNo = '" & strEmpNo & "',ModifiedBy = '" & PBUser_EmpNo & "' WHERE ID = '" & flxDetails.Item(0, intRow).Value & "'"
                        Case "Precision"
                            mStrSQL = "UPDATE tblReturns SET EmpNo = '" & strEmpNo & "',ModifiedBy = '" & PBUser_EmpNo & "' WHERE ID = '" & flxDetails.Item(0, intRow).Value & "'"
                        Case Else
                            If Mid(cmbDepartment.Text, 1, 5) = "Rough" Then
                                mStrSQL = "UPDATE tblRprReturns SET EmpNo = '" & strEmpNo & "',ModifiedBy = '" & PBUser_EmpNo & "' WHERE ParNo = '" & flxDetails.Item(1, intRow).Value & "' AND PktNo = '" & flxDetails.Item(2, intRow).Value & "' AND Sec = '" & flxDetails.Item(3, intRow).Value & "' AND Department = '" & cmbDepartment.Text & "'"
                            Else
                                MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                Exit Sub
                            End If
                    End Select

                    AdoCN.Execute(mStrSQL)
                    Insert_Log("EMP CHANGE", cmbDepartment.Text, flxDetails.Item(1, intRow).Value, flxDetails.Item(2, intRow).Value, CInt(flxDetails.Item(3, intRow).Value))
                End If
            Next

            MsgBox("Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

            txtPktNo.Text = ""
            txtSec.Text = ""
            chkTrf.Checked = True
            txtPktNo.Focus()
            flxDetails.Rows.Clear()
        End If

    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Delete()
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        If flxDetails.Rows.Count > 0 Then
            txtSec.Text = flxDetails.Item(3, flxDetails.CurrentRow.Index).Value

            If cmbDepartment.Text = "Mix" Then
                If IsNumeric(txtSec.Text) = True Then
                    rsComSql = New ADODB.Recordset
                    mStrSQL = "SELECT SUM(RejPcs + LostPcs + BroPcs) AS RejPcs FROM tblMixReturns WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec >= " & CInt(txtSec.Text) & ""
                    rsComSql.Open(mStrSQL, AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If Not IsDBNull(rsComSql.Fields("RejPcs").Value) Then
                            If rsComSql.Fields("RejPcs").Value > 0 Then
                                chkTrf.Checked = True
                            Else
                                chkTrf.Checked = False
                            End If
                        Else
                            chkTrf.Checked = True
                        End If
                    End If
                    rsComSql = Nothing
                End If
                rsComSql_1 = New ADODB.Recordset
                mStrSQL = "SELECT PacketNo FROM tblMixFinishOrders WHERE OrderNo = '" & txtParNo.Text & "' AND PacketNo = '" & txtPktNo.Text & "'"
                rsComSql_1.Open(mStrSQL, AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    chkTrf.Checked = True
                End If
                rsComSql_1 = Nothing
            End If
        End If
    End Sub

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Instring = UCase(InputBox("Enter Par/Pkt Number"))
        ParcelLen = Len(Instring)
        If ParcelLen > 9 Then
            Datavalid = True

            ParcelNo = Mid(Instring, 1, ParcelLen - 4)
            PacketNo = strRight(Instring, 4)
        End If

        If Datavalid = True Then
            txtParNo.Text = ParcelNo
            txtPktNo.Text = PacketNo

            Show_PacketDetails()
        Else
        End If
    End Sub
End Class