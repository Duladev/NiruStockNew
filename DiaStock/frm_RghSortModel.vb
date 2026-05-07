
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RghSortModel
    Dim dblImpPrice As Double

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtParNo.Text = UCase(txtParNo.Text)
            Load_Details()
        End If
    End Sub

    Private Sub ClearFields()
        txtParNo.Text = ""
        txtImpValue.Text = "0"
        txtTotVal.Text = "0"
        flxDetails.Rows.Clear()
        txtSubPcs.Text = "0"
        txtSubCts.Text = "0"
        txtLabCts.Text = "0"
        txtLabRate.Text = "0"
        txtLabValue.Text = "0"
        txtProfit.Text = "0"
    End Sub

    Private Sub Load_Details()
        Dim intGroup As Integer
        Dim strFullName As String

        flxDetails.Rows.Clear()
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblRghSortModel WHERE ParNo = '" & txtParNo.Text & "' ORDER BY Model, Color, Clarity, PktModel, PktColor, PktClarity", AdoCN, 1, 1)
        If rsComSql_1.RecordCount = 0 Then
            intGroup = 0
            strFullName = ""
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRghPacket.ParNo, tblRghTypes_2.Type3 AS Model, dbo.tblRghTypes.Type3 AS Color, tblRghTypes_1.Type3 AS Clarity, SUM(dbo.tblRghPacket.PktPcs) AS Pcs, " & _
                            "ROUND(SUM(dbo.tblRghPacket.PktCts), 3) AS Cts, ROUND(SUM(dbo.tblRghPacket.PktCts * dbo.tblRghPacket.PktPrice), 2) AS Value, dbo.tblRghPacket.PktModel, dbo.tblRghPacket.PktColor," & _
                            "dbo.tblRghPacket.PktClarity " & _
                          "FROM dbo.tblRghPacket INNER JOIN dbo.tblRghTypes ON dbo.tblRghPacket.PktColor = dbo.tblRghTypes.Type INNER JOIN " & _
                            "dbo.tblRghTypes AS tblRghTypes_2 ON dbo.tblRghPacket.PktModel = tblRghTypes_2.Type INNER JOIN " & _
                            "dbo.tblRghTypes AS tblRghTypes_1 ON dbo.tblRghPacket.PktClarity = tblRghTypes_1.Type " & _
                          "WHERE (dbo.tblRghPacket.PktType = 6) AND (dbo.tblRghTypes.Sec = 2) AND (tblRghTypes_1.Sec = 4) AND (tblRghTypes_2.Sec = 5) " & _
                          "GROUP BY dbo.tblRghPacket.ParNo, dbo.tblRghTypes.Type3, tblRghTypes_1.Type3, tblRghTypes_2.Type3, dbo.tblRghPacket.PktModel, dbo.tblRghPacket.PktColor, dbo.tblRghPacket.PktClarity " & _
                          "HAVING (dbo.tblRghPacket.ParNo = '" & txtParNo.Text & "') " & _
                          "ORDER BY Model, Color, Clarity, dbo.tblRghPacket.PktModel, dbo.tblRghPacket.PktColor, dbo.tblRghPacket.PktClarity", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    If intGroup = 0 Then
                        intGroup = 1
                        strFullName = rsComSql.Fields("Model").Value & rsComSql.Fields("Color").Value & rsComSql.Fields("Clarity").Value
                    Else
                        If strFullName <> rsComSql.Fields("Model").Value & rsComSql.Fields("Color").Value & rsComSql.Fields("Clarity").Value Then
                            intGroup = intGroup + 1
                            strFullName = rsComSql.Fields("Model").Value & rsComSql.Fields("Color").Value & rsComSql.Fields("Clarity").Value
                        End If
                    End If
                    flxDetails.Rows.Add(rsComSql.Fields("Model").Value,
                                        rsComSql.Fields("Color").Value,
                                        rsComSql.Fields("Clarity").Value,
                                        rsComSql.Fields("Pcs").Value,
                                        rsComSql.Fields("Cts").Value,
                                        rsComSql.Fields("Value").Value,
                                        Math.Round(rsComSql.Fields("Value").Value / rsComSql.Fields("Cts").Value, 2),
                                        intGroup,
                                        Trim(rsComSql.Fields("PktModel").Value),
                                        Trim(rsComSql.Fields("PktColor").Value),
                                        Trim(rsComSql.Fields("PktClarity").Value))

                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing
        Else
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                flxDetails.Rows.Add(rsComSql_1.Fields("Model").Value,
                                    rsComSql_1.Fields("Color").Value,
                                    rsComSql_1.Fields("Clarity").Value,
                                    rsComSql_1.Fields("PktPcs").Value,
                                    rsComSql_1.Fields("PktCts").Value,
                                    Math.Round(rsComSql_1.Fields("PktCts").Value * rsComSql_1.Fields("PktPrice").Value, 2),
                                    rsComSql_1.Fields("PktPrice").Value,
                                    rsComSql_1.Fields("Grp").Value,
                                    rsComSql_1.Fields("PktModel").Value,
                                    rsComSql_1.Fields("PktColor").Value,
                                    rsComSql_1.Fields("PktClarity").Value)

                rsComSql_1.MoveNext()
            End While
        End If
        rsComSql_1 = Nothing

        txtSubPcs.Text = CalTotalPcs(flxDetails)
        txtSubCts.Text = CalTotalCts(flxDetails)
        txtTotVal.Text = CalTotalValue(flxDetails)
        txtImpValue.Text = Math.Round(dblImpPrice * CDbl(txtSubCts.Text), 2)
    End Sub

    Private Sub CalculateValue()
        Dim intRow As Integer
        Dim dblPrice As Double

        For intRow = 0 To flxDetails.Rows.Count - 1
            If IsNumeric(flxDetails.Item(3, intRow).Value) = True Then
                dblPrice = CDbl(flxDetails.Item(3, intRow).Value)
                flxDetails.Item(4, intRow).Value = Format(Math.Round(dblPrice * CDbl(flxDetails.Item(2, intRow).Value), 2), "#0.00")
            Else
                flxDetails.Item(4, intRow).Value = "0.00"
            End If
            flxDetails.Item(9, intRow).Value = CDbl(flxDetails.Item(7, intRow).Value) * CDbl(flxDetails.Item(8, intRow).Value) / 100
            flxDetails.Item(10, intRow).Value = CDbl(flxDetails.Item(6, intRow).Value) * CDbl(flxDetails.Item(9, intRow).Value)
            flxDetails.Item(11, intRow).Value = Math.Round(CDbl(flxDetails.Item(10, intRow).Value) / CDbl(flxDetails.Item(8, intRow).Value), 2)
        Next

        If txtLabRate.Text = "" Then txtLabRate.Text = "0"
        If txtLabCts.Text = "" Then txtLabCts.Text = "0"
        If txtImpValue.Text = "" Then txtImpValue.Text = "0"

        txtLabValue.Text = Math.Round(CDbl(txtLabCts.Text) * CDbl(txtLabRate.Text), 2)
        txtTotVal.Text = CalTotalValue(flxDetails)
        txtProfit.Text = Math.Round(CDbl(txtTotVal.Text) - (CDbl(txtImpValue.Text) + CDbl(txtLabValue.Text)), 2)
        If CDbl(txtImpValue.Text) + CDbl(txtLabValue.Text) <> 0 Then
            txtProfitPerc.Text = Math.Round((CDbl(txtProfit.Text) / (CDbl(txtImpValue.Text) + CDbl(txtLabValue.Text))) * 100, 2) & "%"
        Else
            txtProfitPerc.Text = "0.00%"
        End If

    End Sub

    Private Sub CalculatePerc()
        Dim intRow As Integer
        Dim dblPerc As Double

        For intRow = 0 To flxDetails.Rows.Count - 1
            dblPerc = Math.Round(CDbl(flxDetails.Item(2, intRow).Value) / CDbl(txtSubCts.Text) * 100, 2)
            flxDetails.Item(5, intRow).Value = Format(dblPerc, "#0.00") & "%"
        Next
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(3, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(4, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Function CalTotalCtsLab(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCtsLab = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Item(0, intRow).Value <> "Reject" Then
                CalTotalCtsLab = CalTotalCtsLab + Val(flxSample.Item(2, intRow).Value)
            End If
        Next
        CalTotalCtsLab = Math.Round(CalTotalCtsLab, 3)
    End Function

    Private Function CalTotalValue(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalValue = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalValue = CalTotalValue + Val(flxSample.Item(5, intRow).Value)
        Next
        CalTotalValue = Math.Round(CalTotalValue, 2)
    End Function

    Private Sub cmdCalc_Click(sender As Object, e As EventArgs) Handles cmdCalc.Click
        CalculateValue()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        For intRow = 0 To flxDetails.Rows.Count - 1
            If IsNumeric(flxDetails.Item(7, intRow).Value) = False Then
                MsgBox("Invalid Group No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblRghSortModel WHERE ParNo = '" & txtParNo.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                AdoCN.Execute("INSERT INTO tblRghSortModel(ParNo,Model,Color,Clarity,PktModel,PktColor,PktClarity,PktPcs,PktCts,PktPrice,Grp,PktIss) " & _
                              "VALUES('" & txtParNo.Text & "','" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(2, intRow).Value & "'," & _
                                "'" & flxDetails.Item(8, intRow).Value & "','" & flxDetails.Item(9, intRow).Value & "','" & flxDetails.Item(10, intRow).Value & "'," & _
                                "'" & CDbl(flxDetails.Item(3, intRow).Value) & "','" & CDbl(flxDetails.Item(4, intRow).Value) & "','" & CDbl(flxDetails.Item(6, intRow).Value) & "'," & _
                                "'" & CInt(flxDetails.Item(7, intRow).Value) & "','" & Format(Date.Now, "MM/dd/yyyy") & "')")
            Next
        Else
            PBResponse = MsgBox("Do you want to Update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                For intRow = 0 To flxDetails.Rows.Count - 1
                    AdoCN.Execute("UPDATE tblRghSortModel SET Grp = " & CInt(flxDetails.Item(7, intRow).Value) & " WHERE ParNo = '" & txtParNo.Text & "' AND PktModel = '" & flxDetails.Item(8, intRow).Value & "' AND " & _
                                    "PktColor = '" & flxDetails.Item(9, intRow).Value & "' AND PktClarity = '" & flxDetails.Item(10, intRow).Value & "'")
                Next
            End If
        End If
        rsComSql = Nothing

        MsgBox("Details Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        ClearFields()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub txtLabRate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLabRate.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtLabRate.Text)
        If Asc(e.KeyChar) = 13 Then
            CalculateValue()
        End If
    End Sub

    Private Sub cmdReport_Click(sender As Object, e As EventArgs) Handles cmdReport.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRghParcelAnalysis2021New.rpt"
        strReportPath = PBReportPath & "Rgh\" & mReportName
        objForm.Show()
    End Sub

    Private Sub frm_RghSortModel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        ClearFields()
    End Sub
End Class