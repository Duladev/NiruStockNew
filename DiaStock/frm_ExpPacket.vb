
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_ExpPacket

    Private Sub Load_Machine()
        cmbMachine.Items.Clear()
        cmbMachine.Items.Add("AMS")
        cmbMachine.Items.Add("DFI")
        cmbMachine.Items.Add("ID100")
        cmbMachine.Items.Add("YEHUDA")
        cmbMachine.Items.Add("IMPORT")
        cmbMachine.Items.Add("EXPORT")
    End Sub

    Private Sub Load_PacketDetails()
        If cmbMachine.Text = "" Then MsgBox("Please select the Machine", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        If cmbMachine.Text = "AMS" Then
            rsComSql.Open("SELECT * FROM tblExpPacket WHERE (AMS2 = 0) ORDER BY ParNo, PktNo", AdoCN, 1, 1)
        ElseIf cmbMachine.Text = "ID100" Then
            rsComSql.Open("SELECT * FROM tblExpPacket WHERE (YAH = 0) ORDER BY ParNo, PktNo", AdoCN, 1, 1)
        ElseIf cmbMachine.Text = "YEHUDA" Or cmbMachine.Text = "DFI" Then
            rsComSql.Open("SELECT * FROM tblExpPacket WHERE (YAH = 0) ORDER BY ParNo, PktNo", AdoCN, 1, 1)
        Else
            GoTo ColomboNiru
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("PktPcs").Value,
                                    rsComSql.Fields("PktCts").Value,
                                    False, 0, 0, 0, 0, 0, 0,
                                    rsComSql.Fields("Department").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

ColomboNiru:

        rsComSql = New ADODB.Recordset
        If cmbMachine.Text = "IMPORT" Then
            rsComSql.Open("SELECT * FROM tblParcel WHERE (AMS2 = 0) AND (Depart = 'Colombo Niru' OR Depart = 'Rounds4') ORDER BY OrigParcelNo", AdoCN, 1, 1)
        ElseIf cmbMachine.Text = "EXPORT" Then
            rsComSql.Open("SELECT * FROM tblParcel WHERE (YAH = 0) AND (Depart = 'Colombo Niru' OR Depart = 'Rounds4') ORDER BY OrigParcelNo", AdoCN, 1, 1)
        Else
            GoTo EmpIssues
        End If

        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("OrigParcelNo").Value,
                                    "001",
                                    rsComSql.Fields("IssuedPcs").Value,
                                    rsComSql.Fields("IssuedCts").Value,
                                    False, 0, 0, 0, 0, 0, 0,
                                    rsComSql.Fields("Depart").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

EmpIssues:
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblMixEmpIssues WHERE (Lab = 1) ORDER BY Assortment", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("Assortment").Value,
                                    "001",
                                    rsComSql.Fields("IssPcs").Value,
                                    rsComSql.Fields("IssCts").Value,
                                    False, 0, 0, 0, 0, 0, 0, "APCU")

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(4, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(4, intRow).Value = False
            Next
        End If
    End Sub

    Private Sub VerifyPacket()
        Dim intRow As Integer
        Dim intCol As Integer

        If cmbMachine.Text = "" Then MsgBox("Invalid Machine", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(4, intRow).Value = True Then
                    For intCol = 5 To 10
                        If Len(flxDetails.Item(intCol, intRow).Value) = 0 Then
                            MsgBox("Invalid Pcs in " & intCol & ", " & intRow, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                        If Not IsNumeric(flxDetails.Item(intCol, intRow).Value) = True Then
                            MsgBox("Invalid Pcs in " & intCol & ", " & intRow, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                    Next

                    If CDbl(flxDetails.Item(2, intRow).Value) <> CDbl(flxDetails.Item(5, intRow).Value) + CDbl(flxDetails.Item(6, intRow).Value) + CDbl(flxDetails.Item(7, intRow).Value) + CDbl(flxDetails.Item(8, intRow).Value) + CDbl(flxDetails.Item(9, intRow).Value) + CDbl(flxDetails.Item(10, intRow).Value) Then
                        MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If

                    If flxDetails.Item(11, intRow).Value = "Mix" Or flxDetails.Item(11, intRow).Value = "KIT Box" Or flxDetails.Item(11, intRow).Value = "Baguettes2" Or flxDetails.Item(11, intRow).Value = "Emerald2" Then
                        If cmbMachine.Text = "AMS" Then
                            AdoCN.Execute("UPDATE tblExpPacket SET AMS2 = 1,AMS2Date = '" & Format(Date.Now, "MM/dd/yyyy") & "',AMS2Time = '" & Format(Date.Now, "HH:mm:ss") & "'," & _
                                            "PASS = " & CDbl(flxDetails.Item(5, intRow).Value) & ",REFER = " & CDbl(flxDetails.Item(6, intRow).Value) & ",SYNTHETIC = " & CDbl(flxDetails.Item(7, intRow).Value) & "," & _
                                            "NONDIAMOND = " & CDbl(flxDetails.Item(8, intRow).Value) & ",PURGE = " & CDbl(flxDetails.Item(9, intRow).Value) & " " & _
                                          "WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'")

                        ElseIf cmbMachine.Text = "ID100" Then
                            AdoCN.Execute("UPDATE tblExpPacket SET YAH = 1,AMS2Date = '" & Format(Date.Now, "MM/dd/yyyy") & "',AMS2Time = '" & Format(Date.Now, "HH:mm:ss") & "'," & _
                                            "PASS = " & CDbl(flxDetails.Item(5, intRow).Value) & ",REFER = " & CDbl(flxDetails.Item(6, intRow).Value) & ",SYNTHETIC = " & CDbl(flxDetails.Item(7, intRow).Value) & "," & _
                                            "NONDIAMOND = " & CDbl(flxDetails.Item(8, intRow).Value) & ",PURGE = " & CDbl(flxDetails.Item(9, intRow).Value) & " " & _
                                          "WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'")

                        ElseIf cmbMachine.Text = "YEHUDA" Or cmbMachine.Text = "DFI" Then
                            AdoCN.Execute("UPDATE tblExpPacket SET YAH = 1,AMS2Date = '" & Format(Date.Now, "MM/dd/yyyy") & "',AMS2Time = '" & Format(Date.Now, "HH:mm:ss") & "'," & _
                                            "PASS = " & CDbl(flxDetails.Item(5, intRow).Value) & ",REFER = " & CDbl(flxDetails.Item(6, intRow).Value) & ",SYNTHETIC = " & CDbl(flxDetails.Item(7, intRow).Value) & "," & _
                                            "NONDIAMOND = " & CDbl(flxDetails.Item(8, intRow).Value) & ",PURGE = " & CDbl(flxDetails.Item(9, intRow).Value) & " " & _
                                          "WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'")
                        Else
                            MsgBox("Invalid Machine", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        End If

                    ElseIf flxDetails.Item(11, intRow).Value = "Colombo Niru" Or flxDetails.Item(11, intRow).Value = "Rounds4" Then
                        If cmbMachine.Text = "IMPORT" Then
                            AdoCN.Execute("UPDATE tblParcel SET AMS2 = 1 WHERE OrigParcelNo = '" & flxDetails.Item(0, intRow).Value & "' AND Depart = '" & flxDetails.Item(11, intRow).Value & "'")
                        ElseIf cmbMachine.Text = "EXPORT" Then
                            AdoCN.Execute("UPDATE tblParcel SET YAH = 1 WHERE OrigParcelNo = '" & flxDetails.Item(0, intRow).Value & "' AND Depart = '" & flxDetails.Item(11, intRow).Value & "'")
                        Else
                            MsgBox("Invalid Machine", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        End If
                    Else
                        If cmbMachine.Text = "AMS" Then
                            AdoCN.Execute("UPDATE tblMixEmpIssues SET Lab = 2 WHERE Assortment = '" & flxDetails.Item(0, intRow).Value & "' AND Lab = 1")
                        ElseIf cmbMachine.Text = "ID100" Then
                            AdoCN.Execute("UPDATE tblMixEmpIssues SET Lab = 2 WHERE Assortment = '" & flxDetails.Item(0, intRow).Value & "' AND Lab = 1")
                        ElseIf cmbMachine.Text = "YEHUDA" Or cmbMachine.Text = "DFI" Then
                            AdoCN.Execute("UPDATE tblMixEmpIssues SET Lab = 2 WHERE Assortment = '" & flxDetails.Item(0, intRow).Value & "' AND Lab = 1")
                        Else
                            MsgBox("Invalid Machine", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        End If
                    End If

                    AdoCN.Execute("INSERT INTO tblAMS2Log(MacNo,SupParcelNo,Pcs,Cts,EmpNo,EmpNoEnt,ChkDate,ChkTime,PASS,REFER,SYNTHETIC,NONDIAMOND,PURGE,NOTCHECKED) " & _
                                  "VALUES('" & cmbMachine.Text & "','" & flxDetails.Item(0, intRow).Value & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & _
                                    "'" & PBUser_EmpNo & "','" & PBUser_EmpNo & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "'," & CDbl(flxDetails.Item(5, intRow).Value) & "," & _
                                    "" & CDbl(flxDetails.Item(6, intRow).Value) & "," & CDbl(flxDetails.Item(7, intRow).Value) & "," & CDbl(flxDetails.Item(8, intRow).Value) & "," & CDbl(flxDetails.Item(9, intRow).Value) & "," & CDbl(flxDetails.Item(10, intRow).Value) & ")")

                End If
            Next
            Load_PacketDetails()
        End If
    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        Load_PacketDetails()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        VerifyPacket()
    End Sub

    Private Sub frm_ExpPacket_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Machine()
    End Sub

    Private Sub cmbMachine_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMachine.SelectedIndexChanged
        flxDetails.Rows.Clear()
    End Sub
End Class