
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RndParcelAnalysis
    Private Sub LoadDetails()
        Dim dblPlanValue As Double
        Dim dblEstPcs As Double
        Dim dblEstRghCts As Double
        Dim dblEstFinCts As Double
        Dim dblActPcs As Double
        Dim dblActRghCts As Double
        Dim dblActFinCts As Double
        Dim dblEstYield As Double
        Dim dblActYield As Double

        Dim dblRecord As Double

        txtParNo.Text = UCase(txtParNo.Text)
        flxDetails.Rows.Clear()
        ExpProgress.Minimum = 0
        ExpProgress.Visible = True

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblBKKEstSeg ORDER BY Type", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()

            dblRecord = 0
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = rsComSql.RecordCount
            While Not rsComSql.EOF
                dblEstPcs = 0
                dblEstRghCts = 0
                dblEstFinCts = 0
                dblEstYield = 0
                dblPlanValue = 0
                rsComSql_1 = New ADODB.Recordset
                If chkAll.Checked = False Then
                    rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.tblRndPacket.ParNo, SUM(dbo.tblRndPacket.PktPcs) AS Pcs, SUM(dbo.tblRndPacket.PktCts) AS Cts, SUM(dbo.tblRndPacket.FinCts) AS FinCts, SUM(dbo.tblRndPacket.PlanVal) AS Value " & _
                                    "FROM dbo.tblRndPacket INNER JOIN dbo.tblRndReturns ON dbo.tblRndPacket.ParNo = dbo.tblRndReturns.ParNo AND dbo.tblRndPacket.PktNo = dbo.tblRndReturns.PktNo " & _
                                    "WHERE (dbo.tblRndReturns.Sec = 25) And (dbo.tblRndPacket.FinCts * 100 >= " & rsComSql.Fields("FromWt").Value & ") And (dbo.tblRndPacket.FinCts * 100 <= " & rsComSql.Fields("ToWt").Value & ") " & _
                                    "GROUP BY dbo.tblRndPacket.ParNo " & _
                                    "HAVING (dbo.tblRndPacket.ParNo = '" & Trim(txtParNo.Text) & "')", AdoCN, 1, 1)
                Else
                    rsComSql_1.Open("SELECT TOP (100) PERCENT left(dbo.tblRndPacket.ParNo,6) AS ParNo, SUM(dbo.tblRndPacket.PktPcs) AS Pcs, SUM(dbo.tblRndPacket.PktCts) AS Cts, SUM(dbo.tblRndPacket.FinCts) AS FinCts, SUM(dbo.tblRndPacket.PlanVal) AS Value " & _
                                    "FROM dbo.tblRndPacket INNER JOIN dbo.tblRndReturns ON dbo.tblRndPacket.ParNo = dbo.tblRndReturns.ParNo AND dbo.tblRndPacket.PktNo = dbo.tblRndReturns.PktNo " & _
                                    "WHERE (dbo.tblRndReturns.Sec = 25) And (dbo.tblRndPacket.FinCts * 100 >= " & rsComSql.Fields("FromWt").Value & ") And (dbo.tblRndPacket.FinCts * 100 <= " & rsComSql.Fields("ToWt").Value & ") " & _
                                    "GROUP BY LEFT(dbo.tblRndPacket.ParNo,6) " & _
                                    "HAVING (LEFT(dbo.tblRndPacket.ParNo,6) = '" & Mid(Trim(txtParNo.Text), 1, 6) & "')", AdoCN, 1, 1)
                End If
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                        dblPlanValue = rsComSql_1.Fields("Value").Value
                        dblEstPcs = rsComSql_1.Fields("Pcs").Value
                        dblEstRghCts = rsComSql_1.Fields("Cts").Value
                        dblEstFinCts = rsComSql_1.Fields("FinCts").Value
                        dblEstYield = (dblEstFinCts / dblEstRghCts) * 100
                    End If
                End If
                rsComSql_1 = Nothing

                dblActPcs = 0
                dblActRghCts = 0
                dblActFinCts = 0
                dblActYield = 0
                rsComSql_1 = New ADODB.Recordset
                If chkAll.Checked = False Then
                    rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.tblRndPacket.ParNo, SUM(dbo.tblRndPacket.PktPcs) AS Pcs, SUM(dbo.tblRndPacket.PktCts) AS Cts, SUM(dbo.tblRndReturns.RetCts) AS FinCts " & _
                                    "FROM dbo.tblRndPacket INNER JOIN dbo.tblRndReturns ON dbo.tblRndPacket.ParNo = dbo.tblRndReturns.ParNo AND dbo.tblRndPacket.PktNo = dbo.tblRndReturns.PktNo " & _
                                    "WHERE (dbo.tblRndReturns.Sec = 25) And (dbo.tblRndReturns.RetCts * 100 >= " & rsComSql.Fields("FromWt").Value & ") And (dbo.tblRndReturns.RetCts * 100 <= " & rsComSql.Fields("ToWt").Value & ") " & _
                                    "GROUP BY dbo.tblRndPacket.ParNo " & _
                                    "HAVING (dbo.tblRndPacket.ParNo = '" & Trim(txtParNo.Text) & "')", AdoCN, 1, 1)
                Else
                    rsComSql_1.Open("SELECT TOP (100) PERCENT LEFT(dbo.tblRndPacket.ParNo,6) AS ParNo, SUM(dbo.tblRndPacket.PktPcs) AS Pcs, SUM(dbo.tblRndPacket.PktCts) AS Cts, SUM(dbo.tblRndReturns.RetCts) AS FinCts " & _
                                    "FROM dbo.tblRndPacket INNER JOIN dbo.tblRndReturns ON dbo.tblRndPacket.ParNo = dbo.tblRndReturns.ParNo AND dbo.tblRndPacket.PktNo = dbo.tblRndReturns.PktNo " & _
                                    "WHERE (dbo.tblRndReturns.Sec = 25) And (dbo.tblRndReturns.RetCts * 100 >= " & rsComSql.Fields("FromWt").Value & ") And (dbo.tblRndReturns.RetCts * 100 <= " & rsComSql.Fields("ToWt").Value & ") " & _
                                    "GROUP BY LEFT(dbo.tblRndPacket.ParNo,6) " & _
                                    "HAVING (LEFT(dbo.tblRndPacket.ParNo,6) = '" & Mid(Trim(txtParNo.Text), 1, 6) & "')", AdoCN, 1, 1)
                End If
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                        dblActPcs = rsComSql_1.Fields("Pcs").Value
                        dblActRghCts = rsComSql_1.Fields("Cts").Value
                        dblActFinCts = rsComSql_1.Fields("FinCts").Value
                        dblActYield = (dblActFinCts / dblActRghCts) * 100
                    End If
                End If
                rsComSql_1 = Nothing


                flxDetails.Rows.Add(rsComSql.Fields("FromWt").Value & " - " & rsComSql.Fields("ToWt").Value,
                                    dblEstPcs,
                                    Math.Round(dblEstRghCts, 3),
                                    Math.Round(dblEstFinCts, 3),
                                    Math.Round(dblEstYield, 2),
                                    dblActPcs,
                                    Math.Round(dblActRghCts, 3),
                                    Math.Round(dblActFinCts, 3),
                                    Math.Round(dblActYield, 2),
                                    Math.Round(dblPlanValue, 2),
                                    rsComSql.Fields("Type").Value)
                rsComSql.MoveNext()

                dblRecord = dblRecord + 1
                ExpProgress.Value = dblRecord
            End While
        End If
        rsComSql = Nothing
        ExpProgress.Visible = False
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If chkAll.Checked = False And Len(txtParNo.Text) = 8 Then
                LoadDetails()
            ElseIf chkAll.Checked = True And Len(txtParNo.Text) = 6 Then
                LoadDetails()
            End If
        End If
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        txtParNo.Text = ""
        flxDetails.Rows.Clear()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Dim intRow As Integer

        AdoCN.Execute("DELETE FROM tblRndParcelAnalysis")
        For intRow = 0 To flxDetails.Rows.Count - 1
            AdoCN.Execute("INSERT INTO tblRndParcelAnalysis(ParNo, Range,EstPcs,EstRghCts,EstFinCts,EstYield,ActPcs,ActRghCts,ActFinCts,ActYield,PlanValue,Type) " & _
                          "VALUES('" & UCase(txtParNo.Text) & "','" & flxDetails.Item(0, intRow).Value & "'," & CDbl(flxDetails.Item(1, intRow).Value) & "," & CDbl(flxDetails.Item(2, intRow).Value) & "," & _
                            "" & CDbl(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & "," & CDbl(flxDetails.Item(5, intRow).Value) & "," & _
                            "" & CDbl(flxDetails.Item(6, intRow).Value) & "," & CDbl(flxDetails.Item(7, intRow).Value) & "," & CDbl(flxDetails.Item(8, intRow).Value) & "," & _
                            "" & CDbl(flxDetails.Item(9, intRow).Value) & ",'" & flxDetails.Item(10, intRow).Value & "')")

        Next
        objForm = New frm_DCLReportViewer
        mReportName = "crptRndParcelAnalysis.rpt"
        strReportPath = PBReportPath & "RoundsFullFlow\" & mReportName
        objForm.Show()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub frm_RndParcelAnalysis_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub
End Class