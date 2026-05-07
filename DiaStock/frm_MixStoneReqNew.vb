
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixStoneReqNew

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxDetails.Rows.Clear()
        End If
    End Sub

    Private Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            Load_Details()
            Load_Pcs1()
            Load_Pcs2()
            Load_Pcs3()
        End If
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub Load_Details()
        Dim dblRecord As Double
        Dim dblReqPcs As Double
        Dim dblSelectPcs As Double

        AdoCN.Execute("DELETE FROM tblTempOrderPcsNew2")

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.VW_MixStockPcsReqLenNew.SizeRange, dbo.VW_MixStockPcsReqLenNew.SizeRangeWid, " & _
                        "ISNULL(dbo.VW_MixOrderBalPcsNew.BalPcs, 0) AS BalPcs, dbo.VW_MixStockPcsReqLenNew.StockPcs, " & _
                        "dbo.VW_MixStockPcsReqLenNew.StockValue, dbo.VW_MixStockPcsReqLenNew.Price, ISNULL(dbo.VW_MixOrderBalPcsNew.MaxCost, 0) AS MaxCost " & _
                      "FROM dbo.VW_MixStockPcsReqLenNew LEFT OUTER JOIN dbo.VW_MixOrderBalPcsNew ON dbo.VW_MixStockPcsReqLenNew.SizeRange = dbo.VW_MixOrderBalPcsNew.SizeRange AND  " & _
                        "dbo.VW_MixStockPcsReqLenNew.SizeRangeWid = dbo.VW_MixOrderBalPcsNew.SizeRangeW " & _
                      "ORDER BY dbo.VW_MixStockPcsReqLenNew.SizeRange, dbo.VW_MixStockPcsReqLenNew.SizeRangeWid", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            dblRecord = 0
            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = rsComSql.RecordCount

            While Not rsComSql.EOF
                dblReqPcs = rsComSql.Fields("BalPcs").Value - rsComSql.Fields("StockPcs").Value
                If dblReqPcs < 0 Then
                    dblReqPcs = 0
                End If
                If rsComSql.Fields("StockPcs").Value >= rsComSql.Fields("BalPcs").Value Then
                    dblSelectPcs = rsComSql.Fields("BalPcs").Value
                Else
                    dblSelectPcs = rsComSql.Fields("StockPcs").Value
                End If

                flxDetails.Rows.Add(rsComSql.Fields("SizeRange").Value,
                                    rsComSql.Fields("SizeRangeWid").Value,
                                    rsComSql.Fields("BalPcs").Value,
                                    rsComSql.Fields("StockPcs").Value,
                                    dblSelectPcs,
                                    dblReqPcs,
                                    Format(rsComSql.Fields("Price").Value, "#0"),
                                    Format(rsComSql.Fields("MaxCost").Value, "#0"))

                AdoCN.Execute("INSERT INTO tblTempOrderPcsNew2(Length,Width,Pcs) " & _
                              "VALUES('" & rsComSql.Fields("SizeRange").Value & "','" & rsComSql.Fields("SizeRangeWid").Value & "'," & dblSelectPcs & ")")

                Application.DoEvents()

                rsComSql.MoveNext()
                dblRecord = dblRecord + 1
                ExpProgress.Value = dblRecord
            End While
        End If
        rsComSql = Nothing

        ExpProgress.Visible = False
    End Sub

    Private Sub Load_Pcs1()
        Dim dblRecord As Double
        Dim intRow As Integer
        Dim strNextWidth As String
        Dim dblSelectPcs As Double

        dblRecord = 0
        ExpProgress.Minimum = 0
        ExpProgress.Visible = True
        ExpProgress.Maximum = flxDetails.Rows.Count

        strNextWidth = ""
        For intRow = 0 To flxDetails.Rows.Count - 1
            If CDbl(flxDetails.Item(5, intRow).Value) > 0 Then

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT TOP (1) SizeRange FROM tblAssortSizeRangeWid WHERE (SizeRange > '" & flxDetails.Item(1, intRow).Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strNextWidth = rsComSql.Fields("SizeRange").Value
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT TOP (100) PERCENT dbo.VW_MixStockPcsReqLenNew.SizeRange, dbo.VW_MixStockPcsReqLenNew.SizeRangeWid, " & _
                                "dbo.VW_MixStockPcsReqLenNew.StockPcs - ISNULL(dbo.VW_TempOrderPcsNew2.Pcs, 0) AS StockPcs, dbo.VW_MixStockPcsReqLenNew.Price " & _
                              "FROM dbo.VW_MixStockPcsReqLenNew LEFT OUTER JOIN dbo.VW_TempOrderPcsNew2 ON dbo.VW_MixStockPcsReqLenNew.SizeRange = dbo.VW_TempOrderPcsNew2.Length AND  " & _
                                "dbo.VW_MixStockPcsReqLenNew.SizeRangeWid = dbo.VW_TempOrderPcsNew2.Width " & _
                              "WHERE (dbo.VW_MixStockPcsReqLenNew.SizeRange = '" & flxDetails.Item(0, intRow).Value & "') AND (dbo.VW_MixStockPcsReqLenNew.SizeRangeWid = '" & strNextWidth & "') " & _
                              "ORDER BY dbo.VW_MixStockPcsReqLenNew.SizeRange, dbo.VW_MixStockPcsReqLenNew.SizeRangeWid", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If rsComSql.Fields("StockPcs").Value > 0 Then
                        If rsComSql.Fields("StockPcs").Value >= CDbl(flxDetails.Item(5, intRow).Value) Then
                            dblSelectPcs = CDbl(flxDetails.Item(5, intRow).Value)
                        Else
                            dblSelectPcs = rsComSql.Fields("StockPcs").Value
                        End If
                        AdoCN.Execute("INSERT INTO tblTempOrderPcsNew2(Length,Width,Pcs) " & _
                                      "VALUES('" & rsComSql.Fields("SizeRange").Value & "','" & rsComSql.Fields("SizeRangeWid").Value & "'," & dblSelectPcs & ")")
                        flxDetails.Item(8, intRow).Value = rsComSql.Fields("SizeRange").Value
                        flxDetails.Item(9, intRow).Value = rsComSql.Fields("SizeRangeWid").Value
                        flxDetails.Item(10, intRow).Value = dblSelectPcs
                        flxDetails.Item(11, intRow).Value = Format(rsComSql.Fields("Price").Value, "#0")
                        flxDetails.Item(12, intRow).Value = CDbl(flxDetails.Item(5, intRow).Value) - dblSelectPcs
                    Else
                        flxDetails.Item(10, intRow).Value = "0"
                        flxDetails.Item(12, intRow).Value = flxDetails.Item(5, intRow).Value
                    End If
                Else
                    flxDetails.Item(10, intRow).Value = "0"
                    flxDetails.Item(12, intRow).Value = flxDetails.Item(5, intRow).Value
                End If
                rsComSql = Nothing

            Else
                flxDetails.Item(10, intRow).Value = "0"
                flxDetails.Item(12, intRow).Value = "0"
            End If
            Application.DoEvents()
            dblRecord = dblRecord + 1
            ExpProgress.Value = dblRecord
        Next
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_Pcs2()
        Dim dblRecord As Double
        Dim intRow As Integer
        Dim strNextLength As String
        Dim dblSelectPcs As Double

        dblRecord = 0
        ExpProgress.Minimum = 0
        ExpProgress.Visible = True
        ExpProgress.Maximum = flxDetails.Rows.Count

        strNextLength = ""
        For intRow = 0 To flxDetails.Rows.Count - 1
            If CDbl(flxDetails.Item(12, intRow).Value) > 0 Then

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT TOP (1) SizeRange FROM tblAssortSizeRange WHERE (SizeRange > '" & flxDetails.Item(0, intRow).Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strNextLength = rsComSql.Fields("SizeRange").Value
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT TOP (100) PERCENT dbo.VW_MixStockPcsReqLenNew.SizeRange, dbo.VW_MixStockPcsReqLenNew.SizeRangeWid, " & _
                                "dbo.VW_MixStockPcsReqLenNew.StockPcs - ISNULL(dbo.VW_TempOrderPcsNew2.Pcs, 0) AS StockPcs, dbo.VW_MixStockPcsReqLenNew.Price " & _
                              "FROM dbo.VW_MixStockPcsReqLenNew LEFT OUTER JOIN dbo.VW_TempOrderPcsNew2 ON dbo.VW_MixStockPcsReqLenNew.SizeRange = dbo.VW_TempOrderPcsNew2.Length AND  " & _
                                "dbo.VW_MixStockPcsReqLenNew.SizeRangeWid = dbo.VW_TempOrderPcsNew2.Width " & _
                              "WHERE (dbo.VW_MixStockPcsReqLenNew.SizeRange = '" & strNextLength & "') AND (dbo.VW_MixStockPcsReqLenNew.SizeRangeWid = '" & flxDetails.Item(1, intRow).Value & "') " & _
                              "ORDER BY dbo.VW_MixStockPcsReqLenNew.SizeRange, dbo.VW_MixStockPcsReqLenNew.SizeRangeWid", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If rsComSql.Fields("StockPcs").Value > 0 Then
                        If rsComSql.Fields("StockPcs").Value >= CDbl(flxDetails.Item(12, intRow).Value) Then
                            dblSelectPcs = CDbl(flxDetails.Item(12, intRow).Value)
                        Else
                            dblSelectPcs = rsComSql.Fields("StockPcs").Value
                        End If
                        AdoCN.Execute("INSERT INTO tblTempOrderPcsNew2(Length,Width,Pcs) " & _
                                      "VALUES('" & rsComSql.Fields("SizeRange").Value & "','" & rsComSql.Fields("SizeRangeWid").Value & "'," & dblSelectPcs & ")")
                        flxDetails.Item(13, intRow).Value = rsComSql.Fields("SizeRange").Value
                        flxDetails.Item(14, intRow).Value = rsComSql.Fields("SizeRangeWid").Value
                        flxDetails.Item(15, intRow).Value = dblSelectPcs
                        flxDetails.Item(16, intRow).Value = Format(rsComSql.Fields("Price").Value, "#0")
                        flxDetails.Item(17, intRow).Value = CDbl(flxDetails.Item(12, intRow).Value) - dblSelectPcs
                    Else
                        flxDetails.Item(15, intRow).Value = "0"
                        flxDetails.Item(17, intRow).Value = flxDetails.Item(12, intRow).Value
                    End If
                Else
                    flxDetails.Item(15, intRow).Value = "0"
                    flxDetails.Item(17, intRow).Value = flxDetails.Item(12, intRow).Value
                End If
                rsComSql = Nothing

            Else
                flxDetails.Item(15, intRow).Value = "0"
                flxDetails.Item(17, intRow).Value = "0"
            End If
            Application.DoEvents()
            dblRecord = dblRecord + 1
            ExpProgress.Value = dblRecord
        Next
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_Pcs3()
        Dim dblRecord As Double
        Dim intRow As Integer
        Dim strNextLength As String
        Dim strNextWidth As String
        Dim dblSelectPcs As Double

        dblRecord = 0
        ExpProgress.Minimum = 0
        ExpProgress.Visible = True
        ExpProgress.Maximum = flxDetails.Rows.Count

        strNextLength = ""
        strNextWidth = ""
        For intRow = 0 To flxDetails.Rows.Count - 1
            If CDbl(flxDetails.Item(12, intRow).Value) > 0 Then

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT TOP (1) SizeRange FROM tblAssortSizeRange WHERE (SizeRange > '" & flxDetails.Item(0, intRow).Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strNextLength = rsComSql.Fields("SizeRange").Value
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT TOP (1) SizeRange FROM tblAssortSizeRange WHERE (SizeRange > '" & flxDetails.Item(1, intRow).Value & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strNextWidth = rsComSql.Fields("SizeRange").Value
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT TOP (100) PERCENT dbo.VW_MixStockPcsReqLenNew.SizeRange, dbo.VW_MixStockPcsReqLenNew.SizeRangeWid, " & _
                                "dbo.VW_MixStockPcsReqLenNew.StockPcs - ISNULL(dbo.VW_TempOrderPcsNew2.Pcs, 0) AS StockPcs, dbo.VW_MixStockPcsReqLenNew.Price " & _
                              "FROM dbo.VW_MixStockPcsReqLenNew LEFT OUTER JOIN dbo.VW_TempOrderPcsNew2 ON dbo.VW_MixStockPcsReqLenNew.SizeRange = dbo.VW_TempOrderPcsNew2.Length AND  " & _
                                "dbo.VW_MixStockPcsReqLenNew.SizeRangeWid = dbo.VW_TempOrderPcsNew2.Width " & _
                              "WHERE (dbo.VW_MixStockPcsReqLenNew.SizeRange = '" & strNextLength & "') AND (dbo.VW_MixStockPcsReqLenNew.SizeRangeWid = '" & strNextWidth & "') " & _
                              "ORDER BY dbo.VW_MixStockPcsReqLenNew.SizeRange, dbo.VW_MixStockPcsReqLenNew.SizeRangeWid", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If rsComSql.Fields("StockPcs").Value > 0 Then
                        If rsComSql.Fields("StockPcs").Value >= CDbl(flxDetails.Item(17, intRow).Value) Then
                            dblSelectPcs = CDbl(flxDetails.Item(17, intRow).Value)
                        Else
                            dblSelectPcs = rsComSql.Fields("StockPcs").Value
                        End If
                        AdoCN.Execute("INSERT INTO tblTempOrderPcsNew2(Length,Width,Pcs) " & _
                                      "VALUES('" & rsComSql.Fields("SizeRange").Value & "','" & rsComSql.Fields("SizeRangeWid").Value & "'," & dblSelectPcs & ")")
                        flxDetails.Item(18, intRow).Value = rsComSql.Fields("SizeRange").Value
                        flxDetails.Item(19, intRow).Value = rsComSql.Fields("SizeRangeWid").Value
                        flxDetails.Item(20, intRow).Value = dblSelectPcs
                        flxDetails.Item(21, intRow).Value = Format(rsComSql.Fields("Price").Value, "#0")
                        flxDetails.Item(22, intRow).Value = CDbl(flxDetails.Item(12, intRow).Value) - dblSelectPcs
                    Else
                        flxDetails.Item(20, intRow).Value = "0"
                        flxDetails.Item(22, intRow).Value = flxDetails.Item(17, intRow).Value
                    End If
                Else
                    flxDetails.Item(20, intRow).Value = "0"
                    flxDetails.Item(22, intRow).Value = flxDetails.Item(17, intRow).Value
                End If
                rsComSql = Nothing

            Else
                flxDetails.Item(20, intRow).Value = "0"
                flxDetails.Item(22, intRow).Value = "0"
            End If
            Application.DoEvents()
            dblRecord = dblRecord + 1
            ExpProgress.Value = dblRecord
        Next
        ExpProgress.Visible = False
    End Sub

    Private Sub frm_MixStoneReqNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub
End Class