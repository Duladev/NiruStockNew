
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixPacketFlow
    Private Sub Load_Flow()

        cmbFlow.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblMixFlow ORDER BY Flow", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            Do While Not rsComSql.EOF
                cmbFlow.Items.Add(rsComSql.Fields("Flow").Value)
                rsComSql.MoveNext()
            Loop
        End If
        rsComSql = Nothing
    End Sub

    Private Sub frm_MixPacketGrp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Flow()
    End Sub

    Private Sub Load_Packets()

        'flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblMixPacket WHERE PktOrdNo = '" & txtOrderNo.Text & "' AND PktRefNo = '" & Replace(cmbRef.Text, "'", "''") & "' AND Ok = 1 AND Accept = 1 ORDER BY PktNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT PktNo FROM tblMixIssues WHERE ParNo = '" & rsComSql.Fields("PktOrdNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "' AND Sec > 7", AdoCN, 1, 1)
                If rsComSql_1.RecordCount = 0 Then

                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT IncenCat FROM dbo.tblOrdersDtls WHERE (OrderNo = '" & txtOrderNo.Text & "') AND (RefNo = '" & Replace(rsComSql.Fields("PktRefNo").Value, "'", "''") & "') AND (Side = '" & rsComSql.Fields("Pktside").Value & "')", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        flxDetails.Rows.Add(rsComSql.Fields("PktOrdNo").Value,
                                        rsComSql.Fields("PktNo").Value,
                                        rsComSql.Fields("PktPcs").Value,
                                        rsComSql.Fields("PktCts").Value,
                                        rsComSql.Fields("Grp").Value,
                                        rsComSql.Fields("PktFlow").Value, False,
                                        rsComSql_2.Fields("IncenCat").Value,
                                        rsComSql.Fields("PktRefNo").Value)
                    End If
                    rsComSql_2 = Nothing

                End If
                rsComSql_1 = Nothing            

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_PacketsAll()

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblMixPacket WHERE PktOrdNo = '" & txtOrderNo.Text & "' AND Ok = 1 AND Accept = 1 ORDER BY PktNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT PktNo FROM tblMixIssues WHERE ParNo = '" & rsComSql.Fields("PktOrdNo").Value & "' AND PktNo = '" & rsComSql.Fields("PktNo").Value & "' AND Sec > 7", AdoCN, 1, 1)
                If rsComSql_1.RecordCount = 0 Then
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT IncenCat FROM dbo.tblOrdersDtls WHERE (OrderNo = '" & txtOrderNo.Text & "') AND (RefNo = '" & Replace(rsComSql.Fields("PktRefNo").Value, "'", "''") & "') AND (Side = '" & rsComSql.Fields("Pktside").Value & "')", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        flxDetails.Rows.Add(rsComSql.Fields("PktOrdNo").Value,
                                        rsComSql.Fields("PktNo").Value,
                                        rsComSql.Fields("PktPcs").Value,
                                        rsComSql.Fields("PktCts").Value,
                                        rsComSql.Fields("Grp").Value,
                                        rsComSql.Fields("PktFlow").Value, False,
                                        rsComSql_2.Fields("IncenCat").Value,
                                        rsComSql.Fields("PktRefNo").Value)
                    End If
                    rsComSql_2 = Nothing

                End If
                rsComSql_1 = Nothing

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Ref()
        cmbRef.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT RefNo FROM dbo.tblOrdersDtls WHERE (OrderNo = '" & txtOrderNo.Text & "') GROUP BY RefNo ORDER BY RefNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbRef.Items.Add(rsComSql.Fields("RefNo").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub txtOrderNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrderNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtOrderNo.Text <> "" Then
                If Len(txtOrderNo.Text) = 6 Then
                    txtSubject.Text = ""
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT Subject, Subject2 FROM dbo.tblOrders WHERE (OrderNo = '" & txtOrderNo.Text & "')", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        txtSubject.Text = rsComSql.Fields("Subject").Value & " " & rsComSql.Fields("Subject2").Value
                    End If
                    Load_Ref()
                    cmbRef.Focus()
                    'Load_PacketsAll()
                Else
                    MsgBox("Invalid Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    txtSubject.Text = ""
                    cmbRef.Items.Clear()
                    txtOrderNo.Focus()
                End If
            Else
                MsgBox("Invalid Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtSubject.Text = ""
                cmbRef.Items.Clear()
                txtOrderNo.Focus()
            End If
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub SaveFlow()
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If txtOrderNo.Text <> "" And cmbFlow.Text <> "" Then
                For intRow = 0 To flxDetails.Rows.Count - 1
                    If flxDetails.Item(6, intRow).Value = True Then
                        AdoCN.Execute("UPDATE tblMixPacket SET PktFlow = '" & cmbFlow.Text & "' " & _
                                      "WHERE PktOrdNo = '" & flxDetails.Item(0, intRow).Value & "' AND " & _
                                            "PktNo = '" & flxDetails.Item(1, intRow).Value & "'")
                    End If
                Next

                MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                'txtOrderNo.Text = ""
                'txtSubject.Text = ""
                cmbRef.Text = ""
                cmbFlow.Text = ""
                flxDetails.Rows.Clear()
            Else
                MsgBox("Invalid Order No./Flow", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        SaveFlow()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub cmbRef_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRef.SelectedIndexChanged
        Load_Packets()
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

    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        flxDetails.Rows.Clear()
    End Sub

    Private Sub cmdShow_Click(sender As Object, e As EventArgs) Handles cmdShow.Click
        Load_PacketsAll()
    End Sub
End Class