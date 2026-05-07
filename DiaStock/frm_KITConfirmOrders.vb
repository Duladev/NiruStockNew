
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_KITConfirmOrders

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        flxDetails.Rows.Clear()
        txtOrder.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtPack.Text = ""
    End Sub

    Private Sub Load_MixFinishOrders()
        Dim rstMax As ADODB.Recordset
        Dim vRecordNo As Double
        Dim dblMaxValue As Double
        Dim strMaxType As String
        Dim dblMaxCost As Double
        Dim dblRecord As Double

        flxDetails.Rows.Clear()
        ExpProgress.Minimum = 0
        ExpProgress.Visible = True

        If txtOrder.Text <> "" And cmbClient.Text <> "" Then
            MsgBox("Invalid Selection", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        dblMaxValue = 0
        strMaxType = ""
        dblMaxCost = 0
        rsComSql = New ADODB.Recordset
        If txtOrder.Text = "" And cmbClient.Text = "" Then
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixFinishOrders.OrderNo, dbo.tblMixFinishOrders.Reference, dbo.tblMixFinishOrders.Side, dbo.tblMixFinishOrders.ParNo, " & _
                            "dbo.tblMixFinishOrders.Assortment, dbo.tblMixFinishOrders.AssPrice, dbo.tblMixFinishOrders.PacketNo, dbo.tblMixFinishOrders.FinishedPcs, " & _
                            "dbo.tblMixFinishOrders.FinishedCts, dbo.tblMixFinishOrders.PacketPcs, dbo.tblMixFinishOrders.PacketCts, dbo.tblMixFinishOrders.IssueCts, " & _
                            "dbo.tblMixFinishOrders.RateCode, dbo.tblMixFinishOrders.Export, dbo.tblMixFinishOrders.SumExportPcs, dbo.tblMixFinishOrders.Status, " & _
                            "dbo.tblMixFinishOrders.AuditNo, dbo.tblMixFinishOrders.RecordNo, dbo.tblMixFinishOrders.DoneBy, dbo.tblMixFinishOrders.ModifyBy, " & _
                            "dbo.tblMixFinishOrders.SystemDateTime, dbo.tblMixFinishOrders.RejPcs, dbo.tblMixFinishOrders.RejCts, dbo.tblMixFinishOrders.LostPcs, " & _
                            "dbo.tblMixFinishOrders.LostCts, dbo.tblMixFinishOrders.Bro, dbo.tblMixFinishOrders.Subject, dbo.tblMixFinishOrders.NLineNo, dbo.tblMixFinishOrders.Type, " & _
                            "dbo.tblMixFinishOrders.GrdPktNo, dbo.tblMixFinishOrders.GrPcs, dbo.tblMixFinishOrders.SysFinCts, dbo.tblMixFinishOrders.NLineNo2, " & _
                            "dbo.tblMixFinishOrders.IssuePcs, dbo.tblAssortList.CurrentCost, dbo.tblAssortList.AvgCost, dbo.tblKITOrders.Niruref " & _
                          "FROM dbo.tblMixFinishOrders INNER JOIN dbo.tblAssortList ON dbo.tblMixFinishOrders.Assortment = dbo.tblAssortList.Assortment INNER JOIN dbo.tblKITOrders ON dbo.tblMixFinishOrders.OrderNo = dbo.tblKITOrders.OrderNo " & _
                          "WHERE (dbo.tblMixFinishOrders.Status LIKE 'A') AND (dbo.tblMixFinishOrders.Export = 1) " & _
                          "ORDER BY dbo.tblMixFinishOrders.OrderNo, dbo.tblMixFinishOrders.PacketNo", AdoCN, 1, 1)

        ElseIf cmbClient.Text <> "" And txtOrder.Text = "" Then
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixFinishOrders.OrderNo, dbo.tblMixFinishOrders.Reference, dbo.tblMixFinishOrders.Side, dbo.tblMixFinishOrders.ParNo, " & _
                            "dbo.tblMixFinishOrders.Assortment, dbo.tblMixFinishOrders.AssPrice, dbo.tblMixFinishOrders.PacketNo, dbo.tblMixFinishOrders.FinishedPcs, " & _
                            "dbo.tblMixFinishOrders.FinishedCts, dbo.tblMixFinishOrders.PacketPcs, dbo.tblMixFinishOrders.PacketCts, dbo.tblMixFinishOrders.IssueCts, " & _
                            "dbo.tblMixFinishOrders.RateCode, dbo.tblMixFinishOrders.Export, dbo.tblMixFinishOrders.SumExportPcs, dbo.tblMixFinishOrders.Status, " & _
                            "dbo.tblMixFinishOrders.AuditNo, dbo.tblMixFinishOrders.RecordNo, dbo.tblMixFinishOrders.DoneBy, dbo.tblMixFinishOrders.ModifyBy, " & _
                            "dbo.tblMixFinishOrders.SystemDateTime, dbo.tblMixFinishOrders.RejPcs, dbo.tblMixFinishOrders.RejCts, dbo.tblMixFinishOrders.LostPcs, " & _
                            "dbo.tblMixFinishOrders.LostCts, dbo.tblMixFinishOrders.Bro, dbo.tblMixFinishOrders.Subject, dbo.tblMixFinishOrders.NLineNo, dbo.tblMixFinishOrders.Type, " & _
                            "dbo.tblMixFinishOrders.GrdPktNo, dbo.tblMixFinishOrders.GrPcs, dbo.tblMixFinishOrders.SysFinCts, dbo.tblMixFinishOrders.NLineNo2, " & _
                            "dbo.tblMixFinishOrders.IssuePcs, dbo.tblAssortList.CurrentCost, dbo.tblAssortList.AvgCost, dbo.tblKITOrders.Niruref " & _
                          "FROM dbo.tblMixFinishOrders INNER JOIN dbo.tblAssortList ON dbo.tblMixFinishOrders.Assortment = dbo.tblAssortList.Assortment INNER JOIN dbo.tblKITOrders ON dbo.tblMixFinishOrders.OrderNo = dbo.tblKITOrders.OrderNo " & _
                          "WHERE (dbo.tblMixFinishOrders.Status LIKE 'A') AND (dbo.tblMixFinishOrders.Export = 1) AND (dbo.tblKITOrders.Niruref = '" & cmbClient.Text & "') " & _
                          "ORDER BY dbo.tblMixFinishOrders.OrderNo, dbo.tblMixFinishOrders.PacketNo", AdoCN, 1, 1)

        Else
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixFinishOrders.OrderNo, dbo.tblMixFinishOrders.Reference, dbo.tblMixFinishOrders.Side, dbo.tblMixFinishOrders.ParNo, " & _
                            "dbo.tblMixFinishOrders.Assortment, dbo.tblMixFinishOrders.AssPrice, dbo.tblMixFinishOrders.PacketNo, dbo.tblMixFinishOrders.FinishedPcs, " & _
                            "dbo.tblMixFinishOrders.FinishedCts, dbo.tblMixFinishOrders.PacketPcs, dbo.tblMixFinishOrders.PacketCts, dbo.tblMixFinishOrders.IssueCts, " & _
                            "dbo.tblMixFinishOrders.RateCode, dbo.tblMixFinishOrders.Export, dbo.tblMixFinishOrders.SumExportPcs, dbo.tblMixFinishOrders.Status, " & _
                            "dbo.tblMixFinishOrders.AuditNo, dbo.tblMixFinishOrders.RecordNo, dbo.tblMixFinishOrders.DoneBy, dbo.tblMixFinishOrders.ModifyBy, " & _
                            "dbo.tblMixFinishOrders.SystemDateTime, dbo.tblMixFinishOrders.RejPcs, dbo.tblMixFinishOrders.RejCts, dbo.tblMixFinishOrders.LostPcs, " & _
                            "dbo.tblMixFinishOrders.LostCts, dbo.tblMixFinishOrders.Bro, dbo.tblMixFinishOrders.Subject, dbo.tblMixFinishOrders.NLineNo, dbo.tblMixFinishOrders.Type, " & _
                            "dbo.tblMixFinishOrders.GrdPktNo, dbo.tblMixFinishOrders.GrPcs, dbo.tblMixFinishOrders.SysFinCts, dbo.tblMixFinishOrders.NLineNo2, " & _
                            "dbo.tblMixFinishOrders.IssuePcs, dbo.tblAssortList.CurrentCost, dbo.tblAssortList.AvgCost, dbo.tblKITOrders.Niruref " & _
                          "FROM dbo.tblMixFinishOrders INNER JOIN dbo.tblAssortList ON dbo.tblMixFinishOrders.Assortment = dbo.tblAssortList.Assortment INNER JOIN dbo.tblKITOrders ON dbo.tblMixFinishOrders.OrderNo = dbo.tblKITOrders.OrderNo " & _
                          "WHERE (dbo.tblMixFinishOrders.Status LIKE 'A') AND (dbo.tblMixFinishOrders.Export = 1) AND (dbo.tblMixFinishOrders.OrderNo = '" & Trim(txtOrder.Text) & "') " & _
                          "ORDER BY dbo.tblMixFinishOrders.OrderNo, dbo.tblMixFinishOrders.PacketNo", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()

            dblRecord = 0
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = rsComSql.RecordCount

            rstMax = New ADODB.Recordset
            mStrSQL = "SELECT MAX(RecordNo) AS RecordNo FROM tblMixExportOrders"
            rstMax.Open(mStrSQL, AdoCN, 1, 1)
            If Not IsDBNull(rstMax.Fields("RecordNo").Value) Then
                vRecordNo = rstMax.Fields("RecordNo").Value + 1
            Else
                vRecordNo = 1
            End If
            rstMax = Nothing

            While Not rsComSql.EOF

                'If rsComSql.Fields("OrderNo").Value = "143362" Then
                '    MsgBox(rsComSql.Fields("OrderNo").Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                'End If

                'If rsComSql.Fields("OrderNo").Value = "143362" And rsComSql.Fields("PacketNo").Value = "002" Then
                '    MsgBox(rsComSql.Fields("OrderNo").Value & "/" & rsComSql.Fields("PacketNo").Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                'End If

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT MaxType, OutPrice " & _
                                "FROM dbo.tblKITOrdersDtls " & _
                                "WHERE (OrderNo = '" & rsComSql.Fields("OrderNo").Value & "') AND (RefNo = '" & rsComSql.Fields("Reference").Value & "') AND (Side = '" & rsComSql.Fields("Side").Value & "')", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strMaxType = rsComSql_1.Fields("MaxType").Value
                    dblMaxCost = rsComSql_1.Fields("OutPrice").Value
                End If
                rsComSql_1 = Nothing

                If strMaxType = "P" Then
                    dblMaxValue = dblMaxCost * rsComSql.Fields("FinishedPcs").Value
                Else
                    dblMaxValue = dblMaxCost * rsComSql.Fields("FinishedCts").Value
                End If
                dblMaxValue = Math.Round(dblMaxValue, 2)

                If rsComSql.Fields("FinishedPcs").Value > 0 Then
                    flxDetails.Rows.Add(rsComSql.Fields("OrderNo").Value,
                                        rsComSql.Fields("Subject").Value,
                                        rsComSql.Fields("Reference").Value,
                                        rsComSql.Fields("Side").Value,
                                        rsComSql.Fields("ParNo").Value,
                                        rsComSql.Fields("Assortment").Value,
                                        rsComSql.Fields("CurrentCost").Value,
                                        rsComSql.Fields("PacketNo").Value,
                                        rsComSql.Fields("FinishedPcs").Value,
                                        Math.Round(rsComSql.Fields("FinishedCts").Value, 3),
                                        rsComSql.Fields("PacketPcs").Value,
                                        Math.Round(rsComSql.Fields("PacketCts").Value, 3),
                                        Math.Round(rsComSql.Fields("IssueCts").Value, 3),
                                        rsComSql.Fields("RateCode").Value,
                                        vRecordNo,
                                        "APCU",
                                        rsComSql.Fields("NLineNo2").Value,
                                        rsComSql.Fields("GrPcs").Value,
                                        rsComSql.Fields("AvgCost").Value,
                                        rsComSql.Fields("IssuePcs").Value,
                                        rsComSql.Fields("Niruref").Value,
                                        dblMaxValue)

                    vRecordNo = vRecordNo + 1
                End If

                If rsComSql.Fields("FinishedPcs").Value = 0 And rsComSql.Fields("IssueCts").Value > 0 Then
                    flxDetails.Rows.Add(rsComSql.Fields("OrderNo").Value,
                                        rsComSql.Fields("Subject").Value,
                                        rsComSql.Fields("Reference").Value,
                                        rsComSql.Fields("Side").Value,
                                        rsComSql.Fields("ParNo").Value,
                                        rsComSql.Fields("Assortment").Value,
                                        rsComSql.Fields("CurrentCost").Value,
                                        rsComSql.Fields("PacketNo").Value,
                                        "0",
                                        "0",
                                        rsComSql.Fields("PacketPcs").Value,
                                        rsComSql.Fields("PacketCts").Value,
                                        rsComSql.Fields("IssueCts").Value,
                                        rsComSql.Fields("RateCode").Value,
                                        vRecordNo,
                                        "APCU",
                                        rsComSql.Fields("NLineNo2").Value,
                                        "0",
                                        rsComSql.Fields("AvgCost").Value,
                                        rsComSql.Fields("IssuePcs").Value,
                                        rsComSql.Fields("Niruref").Value,
                                        0)

                    vRecordNo = vRecordNo + 1

                End If

                rsComSql.MoveNext()
                dblRecord = dblRecord + 1
                ExpProgress.Value = dblRecord
            End While
        Else
            MsgBox("No Records to Confirm", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing

        ExpProgress.Visible = False
        txtPcs.Text = CalTotalPcs(flxDetails, 8)
        txtCts.Text = CalTotalCts(flxDetails, 9)
    End Sub

    Private Sub txtOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrder.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        If optNew.Checked = True Then
            Load_MixFinishOrders()
        Else
            Load_SavedData()
        End If
    End Sub

    Private Sub Load_SavedData()
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        mStrSQL = "SELECT * FROM tblMixExportOrders WHERE Status LIKE 'A' AND OrderNo <> 'Returns' ORDER BY OrderNo"
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("OrderNo").Value,
                                    rsComSql.Fields("Subject").Value,
                                    rsComSql.Fields("Reference").Value,
                                    rsComSql.Fields("Side").Value,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("Assortment").Value,
                                    rsComSql.Fields("AssPrice").Value,
                                    rsComSql.Fields("PacketNo").Value,
                                    rsComSql.Fields("FinishedPcs").Value,
                                    rsComSql.Fields("FinishedCts").Value,
                                    rsComSql.Fields("PacketPcs").Value,
                                    rsComSql.Fields("PacketCts").Value,
                                    rsComSql.Fields("IssueCts").Value,
                                    rsComSql.Fields("RateCode").Value,
                                    rsComSql.Fields("RecordNo").Value,
                                    rsComSql.Fields("OrigAssort").Value,
                                    rsComSql.Fields("NLineNo").Value,
                                    rsComSql.Fields("GrPcs").Value,
                                    rsComSql.Fields("AvgCost").Value,
                                    rsComSql.Fields("IssuePcs").Value)

                rsComSql.MoveNext()
            End While
        Else
            MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing

        txtPcs.Text = CalTotalPcs(flxDetails, 8)
        txtCts.Text = CalTotalCts(flxDetails, 9)
    End Sub

    Private Sub SaveData()
        Dim iRow As Integer
        Dim dblRecord As Double

        ExpProgress.Minimum = 0
        ExpProgress.Visible = True
        If optNew.Checked = True Then
            If Len(txtPack.Text) = 0 Then
                MsgBox("Please Enter the Packing List No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            For iRow = 0 To flxDetails.Rows.Count - 1
                If CDbl(flxDetails.Item(12, iRow).Value) < 0 Then
                    MsgBox("Invalid Issue Cts - " & flxDetails.Item(0, iRow).Value & "/" & flxDetails.Item(7, iRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Next

            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = flxDetails.Rows.Count
            dblRecord = 0
            For iRow = 0 To flxDetails.Rows.Count - 1
                AdoCN.Execute("INSERT INTO tblMixExportOrders(OrderNo,Subject,Reference,Side,ParNo,Assortment,AssPrice,PacketNo,FinishedPcs,FinishedCts,PacketPcs," & _
                                    "PacketCts,IssueCts,RateCode,OrigAssort,Export,Status,AuditNo,RecordNo,InID,NLineNo,ModifyBy,DoneBy,GrPcs,RetDate,AvgCost,IssuePcs,PackingListNo) " & _
                              "VALUES('" & flxDetails.Item(0, iRow).Value & "','" & flxDetails.Item(1, iRow).Value & "','" & flxDetails.Item(2, iRow).Value & "'," & _
                                    "'" & flxDetails.Item(3, iRow).Value & "','" & flxDetails.Item(4, iRow).Value & "','" & flxDetails.Item(5, iRow).Value & "'," & _
                                    "" & CDbl(flxDetails.Item(6, iRow).Value) & ",'" & flxDetails.Item(7, iRow).Value & "'," & CInt(flxDetails.Item(8, iRow).Value) & "," & _
                                    "" & CDbl(flxDetails.Item(9, iRow).Value) & "," & CInt(flxDetails.Item(10, iRow).Value) & "," & CDbl(flxDetails.Item(11, iRow).Value) & "," & _
                                    "" & CDbl(flxDetails.Item(12, iRow).Value) & ",'" & flxDetails.Item(13, iRow).Value & "','" & flxDetails.Item(15, iRow).Value & "'," & _
                                    "1,'A',0," & CDbl(flxDetails.Item(14, iRow).Value) & ",0,'" & flxDetails.Item(16, iRow).Value & "'," & _
                                    "'" & PBUser_ID & "','" & PBUser_ID & "'," & CDbl(flxDetails.Item(17, iRow).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "'," & _
                                    "" & CDbl(flxDetails.Item(18, iRow).Value) & "," & CDbl(flxDetails.Item(19, iRow).Value) & "," & CDbl(txtPack.Text) & ")")

                AdoCN.Execute("UPDATE tblMixPacketDetails " & _
                              "SET Ok = 1 " & _
                              "WHERE ParNo = '" & flxDetails.Item(0, iRow).Value & "' AND " & _
                                    "PktNo = '" & flxDetails.Item(7, iRow).Value & "'")

                AdoCN.Execute("UPDATE tblMixReturns " & _
                              "SET Status = 1 " & _
                              "WHERE ParNo = '" & flxDetails.Item(0, iRow).Value & "' AND " & _
                                    "PktNo = '" & flxDetails.Item(7, iRow).Value & "' AND " & _
                                    "Sec = 18 AND Status = 2")

                dblRecord = dblRecord + 1
                ExpProgress.Value = dblRecord
                Application.DoEvents()
            Next
            MsgBox("Records Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            flxDetails.Rows.Clear()
            txtOrder.Text = ""
            txtPcs.Text = ""
            txtCts.Text = ""
            txtPack.Text = ""
        End If
        ExpProgress.Visible = False
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        SaveData()
    End Sub

    Private Sub optNew_CheckedChanged(sender As Object, e As EventArgs) Handles optNew.CheckedChanged
        flxDetails.Rows.Clear()
        txtPcs.Text = ""
        txtCts.Text = ""
        txtPack.Text = ""
    End Sub

    Private Sub optEdit_CheckedChanged(sender As Object, e As EventArgs) Handles optEdit.CheckedChanged
        flxDetails.Rows.Clear()
        txtPcs.Text = ""
        txtCts.Text = ""
        txtPack.Text = ""
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub Load_Client()
        cmbClient.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT NiruCust FROM tblNiruRef ORDER BY NiruCust", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            Do While Not rsComSql.EOF
                cmbClient.Items.Add(rsComSql.Fields("NiruCust").Value)
                rsComSql.MoveNext()
            Loop
        End If
        rsComSql = Nothing
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(intCalRow, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(intCalRow, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub txtPack_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPack.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub frm_KITConfirmOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Client()
    End Sub
End Class