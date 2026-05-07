
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixOrderGrp2
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
        Dim strGroove As String
        Dim strLaser As String
        Dim strPrevDept As String


        cmbDept.Text = ""
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        If chkDate.Checked = True Then
            If chkALL.Checked = True Then
                mStrSQL = "SELECT TOP (100) PERCENT dbo.VW_MIXSummary_Ord.OrderNo, dbo.VW_MIXSummary_Ord.DueDate, dbo.VW_MIXSummary_Ord.Subject, dbo.VW_MIXSummary_Ord.TotPcs," & _
                            "dbo.VW_MIXSummary_Ord.Complete, dbo.VW_MIXSummary_Ord.Dept, dbo.VW_MIXSummary_Ord.Urgent, dbo.VW_MixShipmentPlanDate3.OrderDate," & _
                            "dbo.VW_MIXSummary_Ord.Niruref, dbo.VW_MIXSummary_Ord.Subject2, dbo.VW_MIXSummary_Ord.COMMANDE " & _
                          "FROM dbo.VW_MIXSummary_Ord INNER JOIN dbo.VW_MixShipmentPlanDate3 ON dbo.VW_MIXSummary_Ord.OrderNo = dbo.VW_MixShipmentPlanDate3.OrderNo " & _
                          "WHERE (dbo.VW_MIXSummary_Ord.Complete = 'N') AND (dbo.VW_MixShipmentPlanDate3.OrderDate = '" & Format(dtpOrdDate.Value, "MM/dd/yyyy") & "') " & _
                          "ORDER BY dbo.VW_MIXSummary_Ord.OrderNo"
            Else
                mStrSQL = "SELECT TOP (100) PERCENT dbo.VW_MIXSummary_Ord.OrderNo, dbo.VW_MIXSummary_Ord.DueDate, dbo.VW_MIXSummary_Ord.Subject, dbo.VW_MIXSummary_Ord.TotPcs," & _
                            "dbo.VW_MIXSummary_Ord.Complete, dbo.VW_MIXSummary_Ord.Dept, dbo.VW_MIXSummary_Ord.Urgent, dbo.VW_MixShipmentPlanDate3.OrderDate," & _
                            "dbo.VW_MIXSummary_Ord.Niruref, dbo.VW_MIXSummary_Ord.Subject2, dbo.VW_MIXSummary_Ord.COMMANDE " & _
                          "FROM dbo.VW_MIXSummary_Ord INNER JOIN dbo.VW_MixShipmentPlanDate3 ON dbo.VW_MIXSummary_Ord.OrderNo = dbo.VW_MixShipmentPlanDate3.OrderNo " & _
                          "WHERE (dbo.VW_MIXSummary_Ord.Complete = 'N') AND (dbo.VW_MixShipmentPlanDate3.OrderDate = '" & Format(dtpOrdDate.Value, "MM/dd/yyyy") & "') AND (dbo.VW_MIXSummary_Ord.Dept = '') " & _
                          "ORDER BY dbo.VW_MIXSummary_Ord.OrderNo"
            End If
        Else
            If chkALL.Checked = True Then
                If Len(cmbSubject.Text) = 0 Then
                    mStrSQL = "SELECT OrderNo, DueDate, Subject, TotPcs, Complete, Dept, Urgent, Niruref, Subject2, COMMANDE " & _
                              "FROM dbo.VW_MIXSummary_Ord " & _
                              "WHERE (Complete = 'N') " & _
                              "ORDER BY OrderNo"
                Else
                    mStrSQL = "SELECT OrderNo, DueDate, Subject, TotPcs, Complete, Dept, Urgent, Niruref, Subject2, COMMANDE " & _
                              "FROM dbo.VW_MIXSummary_Ord " & _
                              "WHERE (Complete = 'N') AND (Subject = '" & cmbSubject.Text & "') " & _
                              "ORDER BY OrderNo"
                End If
            Else
                If Len(cmbSubject.Text) = 0 Then
                    mStrSQL = "SELECT OrderNo, DueDate, Subject, TotPcs, Complete, Dept, Urgent, Niruref, Subject2, COMMANDE " & _
                              "FROM dbo.VW_MIXSummary_Ord " & _
                              "WHERE (Complete = 'N') AND (Dept = '') " & _
                              "ORDER BY OrderNo"
                Else
                    mStrSQL = "SELECT OrderNo, DueDate, Subject, TotPcs, Complete, Dept, Urgent, Niruref, Subject2, COMMANDE " & _
                              "FROM dbo.VW_MIXSummary_Ord " & _
                              "WHERE (Complete = 'N') AND (Dept = '') AND (Subject = '" & cmbSubject.Text & "') " & _
                              "ORDER BY OrderNo"
                End If
            End If

        End If
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                strGroove = ""
                strLaser = ""

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

                

                flxDetails.Rows.Add(rsComSql.Fields("OrderNo").Value,
                                    rsComSql.Fields("Subject").Value,
                                    rsComSql.Fields("Subject2").Value,
                                    strLaser,
                                    rsComSql.Fields("Dept").Value,
                                    strPrevDept,
                                    False,
                                    rsComSql.Fields("TotPcs").Value,
                                    IIf(rsComSql.Fields("Urgent").Value = 1, True, False),
                                    Format(rsComSql.Fields("DueDate").Value, "yyyy/MM/dd"),
                                    rsComSql.Fields("Niruref").Value,
                                    rsComSql.Fields("COMMANDE").Value,
                                    strGroove)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        SaveDept()
    End Sub

    Private Sub SaveDept()
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
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
        If PBResponse = MsgBoxResult.Yes Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(8, intRow).Value = True Then
                    AdoCN.Execute("UPDATE tblOrders SET Urgent = '" & IIf(flxDetails.Item(8, intRow).Value = True, 1, 0) & "' WHERE OrderNo = '" & flxDetails.Item(0, intRow).Value & "'")
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

    Private Sub chkALL_CheckedChanged(sender As Object)

    End Sub

    Private Sub frm_MixOrderGrp2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
End Class