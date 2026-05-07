
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixPacketVerify
    Private Sub Load_PacketDetails()
        Dim strPlanDate As String

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        If txtOrder.Text = "" Then
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixPacket.PktOrdNo, dbo.tblMixPacket.PktNo, dbo.tblMixPacket.PktPcs, dbo.tblMixPacket.PktCts, dbo.tblMixPacket.PktRefNo, dbo.tblMixPacket.PktIss, " & _
                            "dbo.tblMixPacket.Pktside, dbo.tblMixPacket.PktFlow, dbo.tblOrders.Subject, dbo.tblOrders.Subject2, dbo.tblMixPacket.Ok, dbo.tblMixPacket.AssortNo, dbo.tblOrders.Niruref, dbo.tblOrders.Dept " & _
                          "FROM dbo.tblMixPacket INNER JOIN dbo.tblOrders ON dbo.tblMixPacket.PktOrdNo = dbo.tblOrders.OrderNo " & _
                          "WHERE(dbo.tblMixPacket.Ok = 0) " & _
                          "ORDER BY dbo.tblMixPacket.PktOrdNo, dbo.tblMixPacket.PktNo", AdoCN, 1, 1)
        Else
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixPacket.PktOrdNo, dbo.tblMixPacket.PktNo, dbo.tblMixPacket.PktPcs, dbo.tblMixPacket.PktCts, dbo.tblMixPacket.PktRefNo, dbo.tblMixPacket.PktIss, " & _
                            "dbo.tblMixPacket.Pktside, dbo.tblMixPacket.PktFlow, dbo.tblOrders.Subject, dbo.tblOrders.Subject2, dbo.tblMixPacket.Ok, dbo.tblMixPacket.AssortNo, dbo.tblOrders.Niruref, dbo.tblOrders.Dept " & _
                          "FROM dbo.tblMixPacket INNER JOIN dbo.tblOrders ON dbo.tblMixPacket.PktOrdNo = dbo.tblOrders.OrderNo " & _
                          "WHERE(dbo.tblMixPacket.Ok = 0) AND (dbo.tblMixPacket.PktOrdNo = '" & txtOrder.Text & "') " & _
                          "ORDER BY dbo.tblMixPacket.PktOrdNo, dbo.tblMixPacket.PktNo", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                strPlanDate = ""
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT OrderDateMin FROM VW_MixShipmentPlanDate WHERE OrderNo = '" & rsComSql.Fields("PktOrdNo").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("OrderDateMin").Value) Then
                        strPlanDate = Format(rsComSql_1.Fields("OrderDateMin").Value, "yyyy/MM/dd")
                    End If
                End If
                rsComSql_1 = Nothing

                flxDetails.Rows.Add(rsComSql.Fields("PktOrdNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("Niruref").Value,
                                    rsComSql.Fields("AssortNo").Value,
                                    rsComSql.Fields("PktPcs").Value,
                                    rsComSql.Fields("PktCts").Value,
                                    rsComSql.Fields("PktFlow").Value,
                                    rsComSql.Fields("Subject").Value & " " & rsComSql.Fields("Subject2").Value,
                                    rsComSql.Fields("PktRefNo").Value,
                                    rsComSql.Fields("Pktside").Value, False,
                                    rsComSql.Fields("Dept").Value, False,
                                    Format(rsComSql.Fields("PktIss").Value, "yyyy/MM/dd"),
                                    strPlanDate)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_PacketDetailsNext()
        Dim intRow As Integer
        Dim blnFound As Boolean
        Dim strSelect As String
        Dim strWhere As String
        Dim strOrder As String

        blnFound = False
        rsComSql = New ADODB.Recordset
        If txtOrder.Text = "" Then
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixPacket.PktOrdNo, dbo.tblMixPacket.PktNo, dbo.tblMixPacket.PktPcs, dbo.tblMixPacket.PktCts, dbo.tblMixPacket.PktRefNo, dbo.tblMixPacket.PktIss, " & _
                            "dbo.tblMixPacket.Pktside, dbo.tblMixPacket.PktFlow, dbo.tblOrders.Subject, dbo.tblOrders.Subject2, dbo.tblMixPacket.Ok, dbo.tblMixPacket.AssortNo, dbo.tblOrders.Niruref, dbo.tblOrders.Dept " & _
                          "FROM dbo.tblMixPacket INNER JOIN dbo.tblOrders ON dbo.tblMixPacket.PktOrdNo = dbo.tblOrders.OrderNo " & _
                          "WHERE (dbo.tblMixPacket.Ok = 0) " & _
                          "ORDER BY dbo.tblMixPacket.PktOrdNo, dbo.tblMixPacket.PktNo", AdoCN, 1, 1)
        Else
            strSelect = "SELECT TOP (100) PERCENT dbo.tblMixPacket.PktOrdNo, dbo.tblMixPacket.PktNo, dbo.tblMixPacket.PktPcs, dbo.tblMixPacket.PktCts, dbo.tblMixPacket.PktRefNo, dbo.tblMixPacket.PktIss, " & _
                            "dbo.tblMixPacket.Pktside, dbo.tblMixPacket.PktFlow, dbo.tblOrders.Subject, dbo.tblOrders.Subject2, dbo.tblMixPacket.Ok, dbo.tblMixPacket.AssortNo, dbo.tblOrders.Niruref, dbo.tblOrders.Dept " & _
                        "FROM dbo.tblMixPacket INNER JOIN dbo.tblOrders ON dbo.tblMixPacket.PktOrdNo = dbo.tblOrders.OrderNo "
            strWhere = "WHERE (dbo.tblMixPacket.Ok = 0) AND (dbo.tblMixPacket.PktOrdNo = '" & txtOrder.Text & "') "

            If Len(txtFrom.Text) = 4 And Len(txtTo.Text) = 4 Then
                If CInt(txtFrom.Text) <= CInt(txtTo.Text) Then
                    strWhere = strWhere & " AND (dbo.tblMixPacket.PktNo >= '" & txtFrom.Text & "') AND (dbo.tblMixPacket.PktNo <= '" & txtTo.Text & "') "
                End If
            Else

            End If

            strOrder = "ORDER BY dbo.tblMixPacket.PktOrdNo, dbo.tblMixPacket.PktNo"
            mStrSQL = strSelect & strWhere & strOrder
            rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                blnFound = False
                For intRow = 0 To flxDetails.Rows.Count - 1
                    If flxDetails.Item(0, intRow).Value = rsComSql.Fields("PktOrdNo").Value And flxDetails.Item(1, intRow).Value = rsComSql.Fields("PktNo").Value Then
                        blnFound = True
                        Exit For
                    End If
                Next
                If blnFound = False Then
                    flxDetails.Rows.Add(rsComSql.Fields("PktOrdNo").Value,
                                        rsComSql.Fields("PktNo").Value,
                                        rsComSql.Fields("Niruref").Value,
                                        rsComSql.Fields("AssortNo").Value,
                                        rsComSql.Fields("PktPcs").Value,
                                        rsComSql.Fields("PktCts").Value,
                                        rsComSql.Fields("PktFlow").Value,
                                        rsComSql.Fields("Subject").Value & " " & rsComSql.Fields("Subject2").Value,
                                        rsComSql.Fields("PktRefNo").Value,
                                        rsComSql.Fields("Pktside").Value, False,
                                        rsComSql.Fields("Dept").Value, False,
                                        Format(rsComSql.Fields("PktIss").Value, "yyyy/MM/dd"))

                End If

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub frm_MixPacketVerify_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_PacketDetails()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub VerifyPacket()
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(10, intRow).Value = True Then
                    If flxDetails.Item(12, intRow).Value = False Then
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT * FROM tblNiruRef WHERE NiruCust = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            If rsComSql.Fields("ColorCheck").Value = 1 Then
                                MsgBox("Color need to check - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                Exit Sub
                            End If
                        Else
                            MsgBox("Invalid Client - " & flxDetails.Item(2, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                        rsComSql = Nothing
                    End If
                End If
            Next

            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(10, intRow).Value = True Then
                    AdoCN.Execute("UPDATE tblMixPacket SET Ok = 1,Color = '" & IIf(flxDetails.Item(12, intRow).Value = True, 1, 0) & "',DelDate = '" & Format(Date.Now, "MM/dd/yyyy") & "',DelEmp = '" & PBUser_EmpNo & "',DelBy = '" & PBUser_ID & "' WHERE PktOrdNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'")
                End If
            Next
            Load_PacketDetails()
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        VerifyPacket()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub flxDetails_KeyPress(sender As Object, e As KeyPressEventArgs) Handles flxDetails.KeyPress
        If Asc(e.KeyChar) = Keys.Space Then
            If flxDetails.Item(10, flxDetails.CurrentRow.Index).Value = True Then
                flxDetails.Item(10, flxDetails.CurrentRow.Index).Value = False
            Else
                flxDetails.Item(10, flxDetails.CurrentRow.Index).Value = True
            End If
        End If
    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        Load_PacketDetailsNext()
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(10, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(10, intRow).Value = False
            Next
        End If
    End Sub

    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        ClearFields()
    End Sub

    Private Sub ClearFields()
        flxDetails.Rows.Clear()
        txtOrder.Text = ""
        txtFrom.Text = ""
        txtTo.Text = ""
        chkSelect.Checked = False
    End Sub

    Private Sub txtOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrder.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtFrom.Focus()
        End If
    End Sub

    Private Sub txtFrom_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFrom.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtTo.Focus()
        End If
    End Sub

    Private Sub txtTo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            cmdRefresh.Focus()
        End If
    End Sub
End Class