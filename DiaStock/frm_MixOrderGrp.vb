
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixOrderGrp
    Private Sub Load_Dept()
        cmbDept.Items.Clear()
        rsComSql = New ADODB.Recordset
        mStrSQL = "SELECT Dept FROM tblOrders GROUP BY Dept HAVING (Dept <> N'') ORDER BY Dept"
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbDept.Items.Add(rsComSql.Fields("Dept").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub frm_MixOrderGrp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Dept()
        Load_Subject()
        'Load_Orders()
        dtpOrdDate.Value = Date.Now
    End Sub

    Private Sub Load_Subject()
        cmbSubject.Items.Clear()
        rsComSql = New ADODB.Recordset
        mStrSQL = "SELECT TOP (100) PERCENT Subject " & _
                  "FROM dbo.VW_MIXSummary_Ord " & _
                  "WHERE (Complete = 'N') " & _
                  "GROUP BY Subject " & _
                  "ORDER BY Subject"
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbSubject.Items.Add(rsComSql.Fields("Subject").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Orders()
        Dim dblExpPcs As Double
        Dim strGroove As String
        Dim strLaser As String
        Dim strPrevDept As String
        Dim dblForModel As Double

        cmbDept.Text = ""
        flxDetails.Rows.Clear()
        dblExpPcs = 0
        dblForModel = 0
        rsComSql = New ADODB.Recordset
        If chkDate.Checked = True Then
            If chkALL.Checked = True Then
                mStrSQL = "SELECT TOP (100) PERCENT dbo.VW_MIXSummary_StockOrderSum.OrderNo, dbo.VW_MIXSummary_StockOrderSum.DueDate, dbo.VW_MIXSummary_StockOrderSum.Subject, dbo.VW_MIXSummary_StockOrderSum.KITNo, " & _
                            "dbo.VW_MIXSummary_StockOrderSum.TotPcs, dbo.VW_MIXSummary_StockOrderSum.Complete, dbo.VW_MIXSummary_StockOrderSum.Rejp, dbo.VW_MIXSummary_StockOrderSum.PktPcs, " & _
                            "dbo.VW_MIXSummary_StockOrderSum.OrderItem, dbo.VW_MIXSummary_StockOrderSum.NorderNo, dbo.VW_MIXSummary_StockOrderSum.Dept, dbo.VW_MIXSummary_StockOrderSum.Urgent, " & _
                            "dbo.VW_MIXSummary_StockOrderSum.ExPcs, dbo.VW_MIXSummary_StockOrderSum.Pcu1Pcs, dbo.VW_MIXSummary_StockOrderSum.Pcu2Pcs, dbo.VW_MIXSummary_StockOrderSum.FancyPcs, " & _
                            "dbo.VW_MixShipmentPlanDate3.OrderDate, dbo.VW_MIXSummary_StockOrderSum.Niruref, dbo.VW_MIXSummary_StockOrderSum.Subject2, dbo.VW_MIXSummary_StockOrderSum.COMMANDE, " & _
                            "dbo.VW_MIXSummary_StockOrderSum.ExpPcs, dbo.VW_MIXSummary_StockOrderSum.FinishedPcs, dbo.VW_MIXSummary_StockOrderSum.FinishPcs " & _
                          "FROM dbo.VW_MIXSummary_StockOrderSum INNER JOIN dbo.VW_MixShipmentPlanDate3 ON dbo.VW_MIXSummary_StockOrderSum.OrderNo = dbo.VW_MixShipmentPlanDate3.OrderNo " & _
                          "WHERE (dbo.VW_MixShipmentPlanDate3.OrderDate = '" & Format(dtpOrdDate.Value, "MM/dd/yyyy") & "') " & _
                          "ORDER BY dbo.VW_MIXSummary_StockOrderSum.OrderNo"
            Else
                mStrSQL = "SELECT TOP (100) PERCENT dbo.VW_MIXSummary_StockOrderSum.OrderNo, dbo.VW_MIXSummary_StockOrderSum.DueDate, dbo.VW_MIXSummary_StockOrderSum.Subject, dbo.VW_MIXSummary_StockOrderSum.KITNo,  " & _
                            "dbo.VW_MIXSummary_StockOrderSum.TotPcs, dbo.VW_MIXSummary_StockOrderSum.Complete, dbo.VW_MIXSummary_StockOrderSum.Rejp, dbo.VW_MIXSummary_StockOrderSum.PktPcs, " & _
                            "dbo.VW_MIXSummary_StockOrderSum.OrderItem, dbo.VW_MIXSummary_StockOrderSum.NorderNo, dbo.VW_MIXSummary_StockOrderSum.Dept, dbo.VW_MIXSummary_StockOrderSum.Urgent, " & _
                            "dbo.VW_MIXSummary_StockOrderSum.ExPcs, dbo.VW_MIXSummary_StockOrderSum.Pcu1Pcs, dbo.VW_MIXSummary_StockOrderSum.Pcu2Pcs, dbo.VW_MIXSummary_StockOrderSum.FancyPcs, " & _
                            "dbo.VW_MixShipmentPlanDate3.OrderDate, dbo.VW_MIXSummary_StockOrderSum.Niruref, dbo.VW_MIXSummary_StockOrderSum.Subject2, dbo.VW_MIXSummary_StockOrderSum.COMMANDE, " & _
                            "dbo.VW_MIXSummary_StockOrderSum.ExpPcs, dbo.VW_MIXSummary_StockOrderSum.FinishedPcs, dbo.VW_MIXSummary_StockOrderSum.FinishPcs " & _
                          "FROM dbo.VW_MIXSummary_StockOrderSum INNER JOIN dbo.VW_MixShipmentPlanDate3 ON dbo.VW_MIXSummary_StockOrderSum.OrderNo = dbo.VW_MixShipmentPlanDate3.OrderNo " & _
                          "WHERE (dbo.VW_MixShipmentPlanDate3.OrderDate = '" & Format(dtpOrdDate.Value, "MM/dd/yyyy") & "') AND (dbo.VW_MIXSummary_StockOrderSum.Dept = '') " & _
                          "ORDER BY dbo.VW_MIXSummary_StockOrderSum.OrderNo"
            End If
        Else
            If chkALL.Checked = True Then
                If Len(cmbSubject.Text) = 0 Then
                    mStrSQL = "SELECT OrderNo, DueDate, Subject, TotPcs, Complete, Rejp, PktPcs, OrderItem, NorderNo, Dept, Urgent, ExPcs, Pcu1Pcs, Pcu2Pcs, FancyPcs, Niruref, Subject2, COMMANDE, FPcs, PrPcs, RndPcs, KITNo, ExpPcs, FinishedPcs, FinishPcs " & _
                              "FROM dbo.VW_MIXSummary_StockOrderSum " & _
                              "ORDER BY OrderNo"
                Else
                    mStrSQL = "SELECT OrderNo, DueDate, Subject, TotPcs, Complete, Rejp, PktPcs, OrderItem, NorderNo, Dept, Urgent, ExPcs, Pcu1Pcs, Pcu2Pcs, FancyPcs, Niruref, Subject2, COMMANDE, FPcs, PrPcs, RndPcs, KITNo, ExpPcs, FinishedPcs, FinishPcs " & _
                              "FROM dbo.VW_MIXSummary_StockOrderSum " & _
                              "WHERE (Subject = '" & cmbSubject.Text & "') " & _
                              "ORDER BY OrderNo"
                End If
            Else
                If Len(cmbSubject.Text) = 0 Then
                    mStrSQL = "SELECT OrderNo, DueDate, Subject, TotPcs, Complete, Rejp, PktPcs, OrderItem, NorderNo, Dept, Urgent, ExPcs, Pcu1Pcs, Pcu2Pcs, FancyPcs, Niruref, Subject2, COMMANDE, FPcs, PrPcs, RndPcs, KITNo, ExpPcs, FinishedPcs, FinishPcs " & _
                              "FROM dbo.VW_MIXSummary_StockOrderSum " & _
                              "WHERE Dept = '' " & _
                              "ORDER BY OrderNo"
                Else
                    mStrSQL = "SELECT OrderNo, DueDate, Subject, TotPcs, Complete, Rejp, PktPcs, OrderItem, NorderNo, Dept, Urgent, ExPcs, Pcu1Pcs, Pcu2Pcs, FancyPcs, Niruref, Subject2, COMMANDE, FPcs, PrPcs, RndPcs, KITNo, ExpPcs, FinishedPcs, FinishPcs " & _
                              "FROM dbo.VW_MIXSummary_StockOrderSum " & _
                              "WHERE Dept = '' AND (Subject = '" & cmbSubject.Text & "') " & _
                              "ORDER BY OrderNo"
                End If
            End If
            
        End If
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                dblExpPcs = 0

                strGroove = ""
                strLaser = ""

                If chkFull.Checked = True Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT SUM(ExportPcs) AS ExportPcs FROM dbo.tblCosting WHERE (Department = 'Mix') AND (Reference1 = '" & rsComSql.Fields("OrderNo").Value & "')", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        If Not IsDBNull(rsComSql_1.Fields("ExportPcs").Value) Then
                            dblExpPcs = rsComSql_1.Fields("ExportPcs").Value
                        End If
                    End If
                    rsComSql_1 = Nothing
                End If

                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT MAX(Groove) AS Groove, MAX(Laser) AS Laser FROM dbo.tblOrdersDtls WHERE (OrderNo = '" & rsComSql.Fields("OrderNo").Value & "')", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    If rsComSql_2.Fields("Groove").Value > 0 Then
                        strGroove = "GROOVE"
                    End If
                    If rsComSql_2.Fields("Laser").Value > 0 Then
                        strLaser = "LASER"
                    End If
                End If
                rsComSql_2 = Nothing

                strPrevDept = ""
                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT Dept FROM dbo.tblOrders WHERE (Subject = '" & rsComSql.Fields("Subject").Value & "') AND (Dept <> '') ORDER BY OrderNo DESC", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    rsComSql_2.MoveFirst()
                    strPrevDept = rsComSql_2.Fields("Dept").Value
                End If
                rsComSql_2 = Nothing

                dblForModel = 0
                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT TOP (100) PERCENT SUM(dbo.tblMixPacket.PktPcs) AS PktPcs " & _
                                "FROM dbo.tblMixPacket LEFT OUTER JOIN dbo.tblMixIssues ON dbo.tblMixPacket.PktOrdNo = dbo.tblMixIssues.ParNo AND dbo.tblMixPacket.PktNo = dbo.tblMixIssues.PktNo " & _
                                "WHERE (dbo.tblMixIssues.PktNo IS NULL) AND (dbo.tblMixPacket.PktOrdNo = '" & rsComSql.Fields("OrderNo").Value & "')", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    If Not IsDBNull(rsComSql_2.Fields("PktPcs").Value) Then
                        dblForModel = rsComSql_2.Fields("PktPcs").Value
                    End If
                End If
                rsComSql_2 = Nothing

                flxDetails.Rows.Add(rsComSql.Fields("OrderNo").Value,
                                    rsComSql.Fields("NorderNo").Value,
                                    rsComSql.Fields("OrderItem").Value,
                                    rsComSql.Fields("Subject").Value,
                                    rsComSql.Fields("Subject2").Value,
                                    rsComSql.Fields("Dept").Value,
                                    False,
                                    rsComSql.Fields("TotPcs").Value,
                                    rsComSql.Fields("PktPcs").Value - rsComSql.Fields("RejP").Value,
                                    rsComSql.Fields("TotPcs").Value - (rsComSql.Fields("PktPcs").Value - rsComSql.Fields("RejP").Value),
                                    rsComSql.Fields("PktPcs").Value - (rsComSql.Fields("RejP").Value + rsComSql.Fields("ExPcs").Value),
                                    IIf(rsComSql.Fields("Urgent").Value = 1, True, False),
                                    Format(rsComSql.Fields("DueDate").Value, "yyyy/MM/dd"),
                                    rsComSql.Fields("Pcu1Pcs").Value,
                                    rsComSql.Fields("Pcu2Pcs").Value,
                                    rsComSql.Fields("FancyPcs").Value,
                                    rsComSql.Fields("RndPcs").Value,
                                    rsComSql.Fields("PrPcs").Value,
                                    rsComSql.Fields("ExpPcs").Value,
                                    rsComSql.Fields("Niruref").Value,
                                    dblExpPcs,
                                    rsComSql.Fields("COMMANDE").Value,
                                    strGroove,
                                    strLaser,
                                    strPrevDept,
                                    rsComSql.Fields("KITNo").Value,
                                    rsComSql.Fields("FinishedPcs").Value,
                                    rsComSql.Fields("FinishPcs").Value,
                                    dblForModel)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtTotOrd.Text = CalTotalPcs(flxDetails, 7)
        txtTotIss.Text = CalTotalPcs(flxDetails, 8)
        txtTotBal.Text = CalTotalPcs(flxDetails, 9)
        txtTotIn.Text = CalTotalPcs(flxDetails, 10)
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        SaveDept()
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(intCalRow, intRow).Value)
        Next

    End Function

    Private Sub SaveDept()
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse  = MsgBoxResult.Yes Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(6, intRow).Value = True Then
                    AdoCN.Execute("UPDATE tblOrders SET Dept = '" & cmbDept.Text & "' WHERE OrderNo = '" & flxDetails.Item(0, intRow).Value & "'")
                End If
            Next
            Load_Orders()
        End If
    End Sub

    Private Sub SaveUrgent()
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse  = MsgBoxResult.Yes Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(11, intRow).Value = True Then
                    AdoCN.Execute("UPDATE tblOrders SET Urgent = '" & IIf(flxDetails.Item(11, intRow).Value = True, 1, 0) & "' WHERE OrderNo = '" & flxDetails.Item(0, intRow).Value & "'")
                End If
            Next
            Load_Orders()
        End If
    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        Load_Orders()
    End Sub

    Private Sub cmdSave2_Click(sender As Object, e As EventArgs) Handles cmdSave2.Click
        SaveUrgent()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub
End Class