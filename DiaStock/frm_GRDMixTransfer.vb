
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDMixTransfer

    Private Sub frm_GRDMixTransfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        optMix.Checked = True
    End Sub

    Private Sub GetParNo()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(RIGHT(ParcelNo, 5)) AS ParcelNo FROM tblGradingTrf WHERE LEFT(ParcelNo, 1) = 'M' AND LEFT(ParcelNo, 2) <> 'MX' AND Department = 'GradingMix'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("ParcelNo").Value) Then
                txtParNo.Text = "M" & Format(rsComSql.Fields("ParcelNo").Value + 1, "00000")
            Else
                txtParNo.Text = "M00001"
            End If
        Else
            txtParNo.Text = "M00001"
        End If
        rsComSql = Nothing

    End Sub

    Private Sub Load_RghMixTrf()
        Dim dblIndexNo As Double

        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        txtRghCts.Text = "0"
        txtValue.Text = "0"
        txtSelPcs.Text = "0"
        txtSelCts.Text = "0"
        dblIndexNo = CDbl(strRight(txtParNo.Text, 5))

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblAssortExportDetails.Assortment, SUM(dbo.tblAssortExportDetails.Pcs) AS Pcs, ROUND(SUM(dbo.tblAssortExportDetails.Cts), 3) AS Cts, " & _
                            "dbo.tblAssortList.MarketPrice " & _
                      "FROM dbo.tblAssortExportDetails INNER JOIN dbo.tblAssortExports ON dbo.tblAssortExportDetails.ExpNo = dbo.tblAssortExports.ExpNo INNER JOIN " & _
                            "dbo.tblAssortList ON dbo.tblAssortExportDetails.Assortment = dbo.tblAssortList.Assortment " & _
                      "WHERE (dbo.tblAssortExportDetails.Export = 3) And (dbo.tblAssortExportDetails.Status = 0) " & _
                      "GROUP BY dbo.tblAssortExportDetails.Assortment, dbo.tblAssortList.MarketPrice " & _
                      "ORDER BY dbo.tblAssortExportDetails.Assortment", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("Assortment").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    rsComSql.Fields("MarketPrice").Value,
                                    Format(rsComSql.Fields("Cts").Value * rsComSql.Fields("MarketPrice").Value, "#0.00"),
                                    rsComSql.Fields("Cts").Value,
                                    "M" & Format(dblIndexNo, "00000"), "", "", "", "", "", "", False, "", "", "", "0")

                txtTotPcs.Text = CInt(txtTotPcs.Text) + rsComSql.Fields("Pcs").Value
                txtTotCts.Text = CDbl(txtTotCts.Text) + rsComSql.Fields("Cts").Value
                txtRghCts.Text = CDbl(txtRghCts.Text) + rsComSql.Fields("Cts").Value
                txtValue.Text = CDbl(txtValue.Text) + (rsComSql.Fields("Cts").Value * rsComSql.Fields("MarketPrice").Value)
                dblIndexNo = dblIndexNo + 1
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtTotCts.Text = Format(CDbl(txtTotCts.Text), "#0.000")
        txtRghCts.Text = Format(CDbl(txtRghCts.Text), "#0.000")
        txtValue.Text = Format(CDbl(txtValue.Text), "#0.00")
        If CDbl(txtRghCts.Text) > 0 Then
            txtPrice.Text = Format(CDbl(txtValue.Text) / CDbl(txtRghCts.Text), "#0.00")
        Else
            txtPrice.Text = "0"
        End If

    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        Select Case True
            Case optMix.Checked
                GetParNo()
                Load_RghMixTrf()
            Case optFinish.Checked
                Load_FinishOrders()
            Case optPcu.Checked
                Load_PCU()
            Case optPcuSort.Checked
                Load_PCUGradingTrf()
            Case Else

        End Select
    End Sub

    Private Sub Load_FinishOrders()
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        txtRghCts.Text = "0"
        txtValue.Text = "0"
        txtSelPcs.Text = "0"
        txtSelCts.Text = "0"

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixFinishOrders.OrderNo, SUM(dbo.tblMixFinishOrders.FinishedPcs) AS Pcs, ROUND(SUM(dbo.tblMixFinishOrders.FinishedCts), 2) AS Cts, " & _
                           "ROUND(SUM(dbo.tblMixFinishOrders.IssueCts), 3) AS IssCts, ROUND(SUM(dbo.tblMixFinishOrders.IssueCts * dbo.tblAssortList.MarketPrice), 2) AS Value " & _
                      "FROM dbo.tblMixFinishOrders INNER JOIN dbo.tblAssortList ON dbo.tblMixFinishOrders.Assortment = dbo.tblAssortList.Assortment " & _
                      "WHERE (dbo.tblMixFinishOrders.Status = 'G') " & _
                      "GROUP BY dbo.tblMixFinishOrders.OrderNo " & _
                      "ORDER BY dbo.tblMixFinishOrders.OrderNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("OrderNo").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    Format(rsComSql.Fields("Value").Value / rsComSql.Fields("IssCts").Value, "#0.00"),
                                    Format(rsComSql.Fields("Value").Value, "#0.00"),
                                    rsComSql.Fields("IssCts").Value, "", "", "", "", "", "", "", False, "", "", "", "0")

                txtTotPcs.Text = CInt(txtTotPcs.Text) + rsComSql.Fields("Pcs").Value
                txtTotCts.Text = CDbl(txtTotCts.Text) + rsComSql.Fields("Cts").Value
                txtRghCts.Text = CDbl(txtRghCts.Text) + rsComSql.Fields("IssCts").Value
                txtValue.Text = CDbl(txtValue.Text) + (rsComSql.Fields("Value").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtTotCts.Text = Format(CDbl(txtTotCts.Text), "#0.000")
        txtRghCts.Text = Format(CDbl(txtRghCts.Text), "#0.000")
        txtValue.Text = Format(CDbl(txtValue.Text), "#0.00")
        If CDbl(txtRghCts.Text) > 0 Then
            txtPrice.Text = Format(CDbl(txtValue.Text) / CDbl(txtRghCts.Text), "#0.00")
        Else
            txtPrice.Text = "0"
        End If

    End Sub

    Private Sub Load_PCU()
        Dim dblPrice As Double
        Dim strGiaNo As String
        Dim dblPlanValue As Double

        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        txtRghCts.Text = "0"
        txtValue.Text = "0"
        dblPrice = 0
        txtSelPcs.Text = "0"
        txtSelCts.Text = "0"

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblPCUFinishOrders.OrderNo, dbo.tblPCUFinishOrders.ParNo, dbo.tblPCUFinishOrders.PacketNo, dbo.tblPCUFinishOrders.Assortment, " & _
                        "SUM(dbo.tblPCUFinishOrders.FinishedPcs) AS Pcs, ROUND(SUM(dbo.tblPCUFinishOrders.FinishedCts), 3) AS Cts, ROUND(SUM(dbo.tblPCUFinishOrders.IssueCts), 3) AS IssCts, " & _
                        "ROUND(SUM(dbo.tblPCUFinishOrders.IssueCts * dbo.tblPCUFinishOrders.AssPrice), 2) AS Value, dbo.tblPCUFinishOrders.Reference, dbo.tblPCUFinishOrders.Side, dbo.tblNoneOrders.Special, " & _
                        "dbo.tblPCUFinishOrders.RateCode, dbo.tblPacket.Grp " & _
                      "FROM dbo.tblPCUFinishOrders INNER JOIN dbo.tblNoneOrders ON dbo.tblPCUFinishOrders.OrderNo = dbo.tblNoneOrders.OrderNo INNER JOIN " & _
                        "dbo.tblPacket ON dbo.tblPCUFinishOrders.OrderNo = dbo.tblPacket.PktOrdNo AND dbo.tblPCUFinishOrders.PacketNo = dbo.tblPacket.PktNo " & _
                      "WHERE (dbo.tblPCUFinishOrders.Status = 'G') " & _
                      "GROUP BY dbo.tblPCUFinishOrders.OrderNo, dbo.tblPCUFinishOrders.ParNo, dbo.tblPCUFinishOrders.PacketNo, dbo.tblPCUFinishOrders.Assortment, dbo.tblPCUFinishOrders.Reference, " & _
                        "dbo.tblPCUFinishOrders.Side, dbo.tblNoneOrders.Special, dbo.tblPCUFinishOrders.RateCode, dbo.tblPacket.Grp " & _
                      "ORDER BY dbo.tblPCUFinishOrders.OrderNo, dbo.tblPCUFinishOrders.Reference, dbo.tblPCUFinishOrders.PacketNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                dblPrice = 0
                If rsComSql.Fields("Value").Value <> 0 Then
                    dblPrice = rsComSql.Fields("Value").Value / rsComSql.Fields("IssCts").Value
                End If
                strGiaNo = ""
                dblPlanValue = 0
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT GiaNo, PlanVal FROM tblPacket WHERE PktOrdNo = '" & rsComSql.Fields("OrderNo").Value & "' AND PktNo = '" & rsComSql.Fields("PacketNo").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strGiaNo = rsComSql_1.Fields("GiaNo").Value
                    dblPlanValue = rsComSql_1.Fields("PlanVal").Value
                End If
                rsComSql_1 = Nothing

                flxDetails.Rows.Add(rsComSql.Fields("OrderNo").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    Format(dblPrice, "#0.00"),
                                    Format(rsComSql.Fields("Value").Value, "#0.00"),
                                    rsComSql.Fields("IssCts").Value,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("Reference").Value,
                                    rsComSql.Fields("Side").Value,
                                    rsComSql.Fields("Special").Value,
                                    "",
                                    rsComSql.Fields("Assortment").Value,
                                    rsComSql.Fields("PacketNo").Value,
                                    False, strGiaNo,
                                    rsComSql.Fields("RateCode").Value,
                                    rsComSql.Fields("Grp").Value,
                                    dblPlanValue)

                txtTotPcs.Text = CInt(txtTotPcs.Text) + rsComSql.Fields("Pcs").Value
                txtTotCts.Text = CDbl(txtTotCts.Text) + rsComSql.Fields("Cts").Value
                txtRghCts.Text = CDbl(txtRghCts.Text) + rsComSql.Fields("IssCts").Value
                txtValue.Text = CDbl(txtValue.Text) + rsComSql.Fields("Value").Value
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtTotCts.Text = Format(CDbl(txtTotCts.Text), "#0.000")
        txtRghCts.Text = Format(CDbl(txtRghCts.Text), "#0.000")
        txtValue.Text = Format(CDbl(txtValue.Text), "#0.00")
        If CDbl(txtRghCts.Text) > 0 Then
            txtPrice.Text = Format(CDbl(txtValue.Text) / CDbl(txtRghCts.Text), "#0.00")
        Else
            txtPrice.Text = "0"
        End If

    End Sub

    Private Sub Load_PCUGradingTrf()
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        txtValue.Text = "0"
        txtRghCts.Text = "0"
        txtSelPcs.Text = "0"
        txtSelCts.Text = "0"

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT Department, Assortment, ParNo, Assort1, SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts " & _
                      "FROM dbo.tblExpRghTypes " & _
                      "WHERE (OK = 0) AND (Type = 'G') " & _
                      "GROUP BY Department, Assortment, ParNo, Assort1 " & _
                      "ORDER BY ParNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("Assortment").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    "0", "0",
                                    rsComSql.Fields("Cts").Value,
                                    rsComSql.Fields("ParNo").Value,
                                    "", "", "",
                                    rsComSql.Fields("Department").Value,
                                    rsComSql.Fields("Assort1").Value, "", False, "", "", "")

                txtTotPcs.Text = CInt(txtTotPcs.Text) + rsComSql.Fields("Pcs").Value
                txtTotCts.Text = CDbl(txtTotCts.Text) + rsComSql.Fields("Cts").Value

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtTotCts.Text = Format(CDbl(txtTotCts.Text), "#0.000")
        txtValue.Text = "0"
        txtPrice.Text = "0"
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Item(13, intRow).Value = True Or flxSample.Item(13, intRow).Value = "1" Then
                CalTotalPcs = CalTotalPcs + Val(flxSample.Item(1, intRow).Value)
            End If
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Item(13, intRow).Value = True Or flxSample.Item(13, intRow).Value = "1" Then
                CalTotalCts = CalTotalCts + Val(flxSample.Item(2, intRow).Value)
            End If
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(13, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(13, intRow).Value = False
            Next
        End If
        txtSelPcs.Text = CalTotalPcs(flxDetails)
        txtSelCts.Text = Format(CalTotalCts(flxDetails), "#0.000")
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Save_Mix()
        Dim strParNo As String
        Dim intRow As Integer

        strParNo = txtParNo.Text

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then

            If optMix.Checked = True Then
                For intRow = 0 To flxDetails.Rows.Count - 1
                    Call Dep_Grading_Trf("GradingMix", 9977, flxDetails.Item(6, intRow).Value, "001", flxDetails.Item(1, intRow).Value, flxDetails.Item(2, intRow).Value, flxDetails.Item(1, intRow).Value, flxDetails.Item(5, intRow).Value, "")

                    AdoCN.Execute("UPDATE tblGradingTrf SET OrderNo = '',RefNo = '',Side = '', Assort1 = '" & flxDetails.Item(0, intRow).Value & "' " & _
                                  "WHERE Department = 'GradingMix' AND ParcelNo = '" & flxDetails.Item(6, intRow).Value & "' AND PktNo = '001'")

                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblGrading_Parcel WHERE ParNo = '" & flxDetails.Item(6, intRow).Value & "' AND Dept = 'Grading'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount = 0 Then
                        AdoCN.Execute("INSERT INTO tblGrading_Parcel(ParNo,Complete,Dept,Assort) VALUES('" & flxDetails.Item(6, intRow).Value & "',0,'Grading','" & flxDetails.Item(0, intRow).Value & "')")
                    End If
                    rsComSql_1 = Nothing

                    AdoCN.Execute("UPDATE tblAssortExportDetails SET Status = 1, ParNo = '" & flxDetails.Item(6, intRow).Value & "' WHERE Assortment = '" & flxDetails.Item(0, intRow).Value & "' AND Status = 0 AND Export = 3")
                Next
            End If

            MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

            flxDetails.Rows.Clear()
            txtTotPcs.Text = "0"
            txtTotCts.Text = "0"
            txtPrice.Text = "0"
            txtValue.Text = "0"
            txtParNo.Text = ""
            txtSelPcs.Text = "0"
            txtSelCts.Text = "0"
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Select Case True
            Case optMix.Checked
                Save_Mix()
            Case optFinish.Checked
                Save_Finish()
            Case optPcu.Checked
                Save_PCU()
            Case optPcuSort.Checked
                Save_PCUSorting()
            Case Else

        End Select
    End Sub

    Private Sub Save_Finish()
        Dim strParNo As String
        Dim strPktNo As String
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If optFinish.Checked = True Then
                For intRow = 0 To flxDetails.Rows.Count - 1
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblMixFinishOrders WHERE OrderNo = '" & flxDetails.Item(0, intRow).Value & "' AND Status = 'G' AND LEFT(Assortment, 2) = 'VM'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM tblMixFinishOrders WHERE OrderNo = '" & flxDetails.Item(0, intRow).Value & "' AND Status = 'G' AND LEFT(Assortment, 2) <> 'VM'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            MsgBox("Wrong Assortment Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        Else
                            strParNo = flxDetails.Item(0, intRow).Value & "V"
                        End If
                        rsComSql_1 = Nothing
                    Else
                        strParNo = flxDetails.Item(0, intRow).Value & "A"
                    End If
                    rsComSql = Nothing

                    strPktNo = ""
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT MAX(PktNo) AS MaxPktNo FROM tblGradingTrf WHERE ParcelNo = '" & strParNo & "' AND Department = 'GradingMix' AND LEN(PktNo) = 3", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If Not IsDBNull(rsComSql.Fields("MaxPktNo").Value) Then
                            strPktNo = Format(rsComSql.Fields("MaxPktNo").Value + 1, "000")
                        Else
                            strPktNo = "001"
                        End If
                    End If
                    rsComSql = Nothing

                    Call Dep_Grading_Trf("GradingMix", 9966, strParNo, strPktNo, flxDetails.Item(1, intRow).Value, flxDetails.Item(2, intRow).Value, flxDetails.Item(1, intRow).Value, flxDetails.Item(5, intRow).Value, flxDetails.Item(6, intRow).Value)
                    AdoCN.Execute("UPDATE tblMixFinishOrders SET Status = 'F', GrdPktNo = '" & strPktNo & "' WHERE OrderNo = '" & flxDetails.Item(0, intRow).Value & "' AND Status = 'G'")
                Next
            End If

            MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

            flxDetails.Rows.Clear()
            txtTotPcs.Text = "0"
            txtTotCts.Text = "0"
            txtPrice.Text = "0"
            txtValue.Text = "0"
            txtParNo.Text = ""
            txtSelPcs.Text = "0"
            txtSelCts.Text = "0"
        End If
    End Sub

    Private Sub Save_PCU()
        Dim strParNo As String
        Dim strPktNo As String
        Dim intRow As Integer
        Dim strDept As String

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If optPcu.Checked = True Then
                For intRow = 0 To flxDetails.Rows.Count - 1
                    If flxDetails.Item(13, intRow).Value = True Or flxDetails.Item(13, intRow).Value = "1" Then
                        strParNo = flxDetails.Item(6, intRow).Value

                        If flxDetails.Item(9, intRow).Value = "0" Then
                            strDept = "GradingPCU"
                        Else
                            strDept = "GradingPCU_N"
                        End If

                        strPktNo = ""
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT MAX(PktNo) AS MaxPktNo FROM tblGradingTrf WHERE ParcelNo = '" & strParNo & "' AND Department = '" & strDept & "' AND LEN(PktNo) = 3", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            If Not IsDBNull(rsComSql.Fields("MaxPktNo").Value) Then
                                strPktNo = Format(rsComSql.Fields("MaxPktNo").Value + 1, "000")
                            Else
                                strPktNo = "001"
                            End If
                        Else
                            strPktNo = "001"
                        End If
                        rsComSql = Nothing

                        Call Dep_Grading_Trf(strDept, 9944, strParNo, strPktNo, flxDetails.Item(1, intRow).Value, flxDetails.Item(2, intRow).Value, flxDetails.Item(1, intRow).Value, flxDetails.Item(5, intRow).Value, "")

                        AdoCN.Execute("UPDATE tblGradingTrf SET OrderNo = '" & flxDetails.Item(0, intRow).Value & "',RefNo = '" & flxDetails.Item(7, intRow).Value & "',Side = '" & flxDetails.Item(8, intRow).Value & "'," & _
                                            "GiaNo = '" & flxDetails.Item(14, intRow).Value & "',Assort1 = '" & flxDetails.Item(11, intRow).Value & "',RateCode = '" & flxDetails.Item(15, intRow).Value & "'," & _
                                            "Grp = '" & flxDetails.Item(16, intRow).Value & "',PlanValue = '" & CDbl(flxDetails.Item(17, intRow).Value) & "' " & _
                                      "WHERE Department = '" & strDept & "' AND ParcelNo = '" & strParNo & "' AND PktNo = '" & strPktNo & "'")

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM tblGrading_Parcel WHERE ParNo = '" & strParNo & "' AND Dept = 'Grading'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            AdoCN.Execute("INSERT INTO tblGrading_Parcel(ParNo,Complete,Dept,Assort) VALUES('" & strParNo & "',0,'Grading','" & flxDetails.Item(0, intRow).Value & "')")
                        Else
                            AdoCN.Execute("UPDATE tblGrading_Parcel SET Assort = '" & flxDetails.Item(0, intRow).Value & "' WHERE ParNo = '" & strParNo & "' AND Dept = 'Grading'")
                        End If
                        rsComSql_1 = Nothing

                        AdoCN.Execute("UPDATE tblPCUFinishOrders SET Status = 'F' WHERE OrderNo = '" & flxDetails.Item(0, intRow).Value & "' AND ParNo = '" & flxDetails.Item(6, intRow).Value & "' AND PacketNo = '" & flxDetails.Item(12, intRow).Value & "' AND Status = 'G'")
                    End If
                Next
            End If

            MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

            flxDetails.Rows.Clear()
            txtTotPcs.Text = "0"
            txtTotCts.Text = "0"
            txtPrice.Text = "0"
            txtValue.Text = "0"
            txtParNo.Text = ""
            txtSelPcs.Text = "0"
            txtSelCts.Text = "0"
        End If
    End Sub

    Private Sub Save_PCUSorting()
        Dim strParNo As String
        Dim strPktNo As String
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If optPcuSort.Checked = True Then
                For intRow = 0 To flxDetails.Rows.Count - 1
                    strParNo = flxDetails.Item(6, intRow).Value

                    strPktNo = ""
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT MAX(RIGHT(PktNo, 3)) AS MaxPktNo FROM tblGradingTrf WHERE ParcelNo = '" & strParNo & "' AND Department = '" & flxDetails.Item(10, intRow).Value & "' AND LEFT(PktNo, 1) = 'G'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If Not IsDBNull(rsComSql.Fields("MaxPktNo").Value) Then
                            strPktNo = "G" & Format(rsComSql.Fields("MaxPktNo").Value + 1, "000")
                        Else
                            strPktNo = "G001"
                        End If
                    Else
                        strPktNo = "G001"
                    End If
                    rsComSql = Nothing

                    Call Dep_Grading_Trf(flxDetails.Item(10, intRow).Value, 9944, strParNo, strPktNo, flxDetails.Item(1, intRow).Value, flxDetails.Item(2, intRow).Value, flxDetails.Item(1, intRow).Value, flxDetails.Item(5, intRow).Value, "")

                    AdoCN.Execute("UPDATE tblGradingTrf SET OrderNo = '',RefNo = '',Side = '', Assort1 = '" & flxDetails.Item(11, intRow).Value & "' " & _
                                  "WHERE Department = '" & flxDetails.Item(10, intRow).Value & "' AND ParcelNo = '" & strParNo & "' AND PktNo = '" & strPktNo & "'")


                    If flxDetails.Item(10, intRow).Value = "Mix" Then
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM tblGrading_Parcel WHERE ParNo = '" & strParNo & "' AND Dept = 'Grading'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            AdoCN.Execute("INSERT INTO tblGrading_Parcel(ParNo,Complete,Dept,Assort) VALUES('" & strParNo & "',0,'Grading','" & flxDetails.Item(0, intRow).Value & "')")
                        Else
                            AdoCN.Execute("UPDATE tblGrading_Parcel SET Assort = '" & flxDetails.Item(11, intRow).Value & "' WHERE ParNo = '" & strParNo & "' AND Dept = 'Grading'")
                        End If
                        rsComSql_1 = Nothing
                    End If

                    AdoCN.Execute("UPDATE tblExpRghTypes SET OK = 1 WHERE Department = '" & flxDetails.Item(10, intRow).Value & "' AND ParNo = '" & strParNo & "' AND OK = 0 AND Type = 'G'")

                Next
            End If

            MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

            flxDetails.Rows.Clear()
            txtTotPcs.Text = "0"
            txtTotCts.Text = "0"
            txtPrice.Text = "0"
            txtValue.Text = "0"
            txtParNo.Text = ""
            txtSelPcs.Text = "0"
            txtSelCts.Text = "0"
        End If
    End Sub
End Class