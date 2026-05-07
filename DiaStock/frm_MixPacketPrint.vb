
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixPacketPrint
    Dim strFolderPath As String

    Private Sub Load_PacketDetails()
        Dim strBig As String
        Dim strLaser As String
        Dim strPlanDate As String

        flxDetails.Rows.Clear()
        strBig = ""
        strLaser = ""
        rsComSql = New ADODB.Recordset
        If txtOrderNo.Text <> "" Then
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixPacket.PktOrdNo, dbo.tblMixPacket.PktNo, dbo.tblMixPacket.PktPcs, dbo.tblMixPacket.PktCts, dbo.tblMixPacket.PktRefNo, dbo.tblMixPacket.Pktside, dbo.tblMixPacket.PktFlow," & _
                        "dbo.tblOrders.Subject, dbo.tblOrders.Subject2, dbo.tblMixPacket.Ok, dbo.tblMixPacket.AssortNo, dbo.tblOrders.Niruref, dbo.tblMixPacket.RejectRep, dbo.tblOrdersDtls.Laser " & _
                      "FROM dbo.tblMixPacket INNER JOIN dbo.tblOrders ON dbo.tblMixPacket.PktOrdNo = dbo.tblOrders.OrderNo INNER JOIN " & _
                         "dbo.tblOrdersDtls ON dbo.tblMixPacket.PktOrdNo = dbo.tblOrdersDtls.OrderNo AND dbo.tblMixPacket.PktRefNo = dbo.tblOrdersDtls.RefNo AND dbo.tblMixPacket.Pktside = dbo.tblOrdersDtls.Side " & _
                      "WHERE (dbo.tblMixPacket.PktPrint = 0) AND (dbo.tblMixPacket.PktOrdNo = '" & txtOrderNo.Text & "') " & _
                      "ORDER BY dbo.tblMixPacket.PktOrdNo, dbo.tblMixPacket.PktNo", AdoCN, 1, 1)
        Else
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixPacket.PktOrdNo, dbo.tblMixPacket.PktNo, dbo.tblMixPacket.PktPcs, dbo.tblMixPacket.PktCts, dbo.tblMixPacket.PktRefNo, dbo.tblMixPacket.Pktside, dbo.tblMixPacket.PktFlow," & _
                        "dbo.tblOrders.Subject, dbo.tblOrders.Subject2, dbo.tblMixPacket.Ok, dbo.tblMixPacket.AssortNo, dbo.tblOrders.Niruref, dbo.tblMixPacket.RejectRep, dbo.tblOrdersDtls.Laser " & _
                      "FROM dbo.tblMixPacket INNER JOIN dbo.tblOrders ON dbo.tblMixPacket.PktOrdNo = dbo.tblOrders.OrderNo INNER JOIN " & _
                         "dbo.tblOrdersDtls ON dbo.tblMixPacket.PktOrdNo = dbo.tblOrdersDtls.OrderNo AND dbo.tblMixPacket.PktRefNo = dbo.tblOrdersDtls.RefNo AND dbo.tblMixPacket.Pktside = dbo.tblOrdersDtls.Side " & _
                      "WHERE (dbo.tblMixPacket.PktPrint = 0) " & _
                      "ORDER BY dbo.tblMixPacket.PktOrdNo, dbo.tblMixPacket.PktNo", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                strBig = ""
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT LengthFrom FROM tblAssortList WHERE Assortment = '" & rsComSql.Fields("AssortNo").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If rsComSql_1.Fields("LengthFrom").Value >= 5 Then
                        strBig = "Big"
                    End If
                End If
                rsComSql_1 = Nothing

                If rsComSql.Fields("Laser").Value > 0 Then
                    strLaser = "Laser"
                Else
                    strLaser = ""
                End If
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
                                    IIf(rsComSql.Fields("RejectRep").Value = 1, "Y", ""),
                                    strBig,
                                    strLaser,
                                    strPlanDate)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_PacketDetailsNext()
        Dim intRow As Integer
        Dim blnFound As Boolean
        Dim strBig As String
        Dim strLaser As String
        Dim strPlanDate As String

        strBig = ""
        blnFound = False
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixPacket.PktOrdNo, dbo.tblMixPacket.PktNo, dbo.tblMixPacket.PktPcs, dbo.tblMixPacket.PktCts, dbo.tblMixPacket.PktRefNo, dbo.tblMixPacket.Pktside, dbo.tblMixPacket.PktFlow," & _
                        "dbo.tblOrders.Subject, dbo.tblOrders.Subject2, dbo.tblMixPacket.Ok, dbo.tblMixPacket.AssortNo, dbo.tblOrders.Niruref, dbo.tblMixPacket.RejectRep, dbo.tblOrdersDtls.Laser " & _
                      "FROM dbo.tblMixPacket INNER JOIN dbo.tblOrders ON dbo.tblMixPacket.PktOrdNo = dbo.tblOrders.OrderNo INNER JOIN " & _
                         "dbo.tblOrdersDtls ON dbo.tblMixPacket.PktOrdNo = dbo.tblOrdersDtls.OrderNo AND dbo.tblMixPacket.PktRefNo = dbo.tblOrdersDtls.RefNo AND dbo.tblMixPacket.Pktside = dbo.tblOrdersDtls.Side " & _
                      "WHERE (dbo.tblMixPacket.PktPrint = 0) " & _
                      "ORDER BY dbo.tblMixPacket.PktOrdNo, dbo.tblMixPacket.PktNo", AdoCN, 1, 1)
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
                    strBig = ""
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT LengthFrom FROM tblAssortList WHERE Assortment = '" & rsComSql.Fields("AssortNo").Value & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        If rsComSql_1.Fields("LengthFrom").Value >= 5 Then
                            strBig = "Big"
                        End If
                    End If
                    rsComSql_1 = Nothing

                    If rsComSql.Fields("Laser").Value > 0 Then
                        strLaser = "Laser"
                    Else
                        strLaser = ""
                    End If
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
                                        IIf(rsComSql.Fields("RejectRep").Value = 1, "Y", ""),
                                        strBig,
                                        strLaser,
                                        strPlanDate)

                End If

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub VerifyPacket()
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            AdoCN.Execute("DELETE FROM tblMixPacketPrint")
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(10, intRow).Value = True Then
                    AdoCN.Execute("INSERT INTO tblMixPacketPrint(ParNo,PktNo) VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "')")
                    AdoCN.Execute("UPDATE tblMixPacket SET PktPrint = 1 WHERE PktOrdNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'")
                End If
            Next

            objForm = New frm_DCLReportViewer
            mReportName = "PKTSLEEVE_Full_Auto.rpt"
            strReportPath = PBReportPath & strFolderPath & mReportName
            objForm.Show()

            Load_PacketDetails()
        End If
    End Sub

    Private Sub VerifyPacketImage()
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            AdoCN.Execute("DELETE FROM tblMixPacketPrint")
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(10, intRow).Value = True Then
                    AdoCN.Execute("INSERT INTO tblMixPacketPrint(ParNo,PktNo) VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "')")
                    AdoCN.Execute("UPDATE tblMixPacket SET PktPrint = 1 WHERE PktOrdNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'")
                End If
            Next

            objForm = New frm_DCLReportViewer
            mReportName = "PKTSLEEVE_Full_Image_Auto.rpt"
            strReportPath = PBReportPath & strFolderPath & mReportName
            objForm.Show()

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

    Private Sub frm_MixPacketPrint_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
        If strDBName = "DiaStock" Then
            strFolderPath = "Mix\"
        Else
            strFolderPath = "DiaSalesMix\"
        End If

        Load_PacketDetails()
    End Sub

    Private Sub cmdLastPrint_Click(sender As Object, e As EventArgs) Handles cmdLastPrint.Click
        objForm = New frm_DCLReportViewer
        mReportName = "PKTSLEEVE_Full_Auto.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub txtOrderNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrderNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            Load_PacketDetails()
        End If
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        VerifyPacketImage()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "PKTSLEEVE_Full_Image_Auto.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub
End Class