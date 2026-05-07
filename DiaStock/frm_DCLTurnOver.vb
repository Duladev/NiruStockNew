
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLTurnOver

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_AssortData()
        Dim dtpFromDate As Date
        Dim dblOpenPcs As Double
        Dim dblInPcs As Double
        Dim dblExpPcs As Double
        Dim dblDExpPcs As Double
        Dim dblCurPcs As Double
        Dim dblCurCts As Double
        Dim dblTurnOver As Double
        Dim dblBagPrice As Double
        Dim strAssortment As String
        Dim strNewAssort As String
        Dim strBagAssort As String
        Dim dblRecord As Double
        Dim strSizeRange As String

        dtpFromDate = CDate("01/01/2024")

        AdoCN.Execute("DELETE FROM tblDCL_TurnOver2")
        flxDetails.Rows.Clear()

        rsComSql = New ADODB.Recordset
        'rsComSql.Open("SELECT TOP (100) PERCENT Assortment FROM dbo.tblAssortDetails WHERE ((Len(Assortment) = 7) OR (Len(Assortment) = 6)) AND Assortment LIKE 'ARW%'  GROUP BY Assortment ORDER BY Assortment", AdoCN, 1, 1)
        'rsComSql.Open("SELECT TOP (100) PERCENT Assortment FROM dbo.tblAssortDetails WHERE ((Len(Assortment) = 7) OR (Len(Assortment) = 6)) GROUP BY Assortment ORDER BY Assortment", AdoCN, 1, 1)
        rsComSql.Open("SELECT TOP (100) PERCENT Assortment FROM dbo.VW_AssortOpen GROUP BY Assortment ORDER BY Assortment", AdoCN, 1, 1)
        'rsComSql.Open("SELECT TOP (100) PERCENT Assortment FROM dbo.VW_AssortOpen WHERE Assortment LIKE 'ANW%' GROUP BY Assortment ORDER BY Assortment", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            txtTotCount.Text = rsComSql.RecordCount
            rsComSql.MoveFirst()

            ExpProgress.Value = 0
            ExpProgress.Minimum = 0
            ExpProgress.Text = "Please wait ....."
            ExpProgress.Visible = True
            ExpProgress.Maximum = rsComSql.RecordCount
            dblRecord = 0

            While Not rsComSql.EOF
                dblOpenPcs = 0
                dblInPcs = 0
                dblExpPcs = 0
                dblDExpPcs = 0
                dblCurPcs = 0
                dblCurCts = 0
                dblTurnOver = 0
                strBagAssort = ""
                strNewAssort = ""
                dblBagPrice = 0
                strSizeRange = ""

                strAssortment = rsComSql.Fields("Assortment").Value

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & strAssortment & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    'Open
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT SUM(Pcs) AS Pcs " & _
                                    "FROM dbo.tblAssortOpen " & _
                                    "WHERE (Assortment = '" & strAssortment & "')", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        If Not IsDBNull(rsComSql_2.Fields("Pcs").Value) Then
                            dblOpenPcs = rsComSql_2.Fields("Pcs").Value
                        End If
                    End If
                    rsComSql_2 = Nothing

                    'In
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT SUM(Pcs) AS Pcs " & _
                                    "FROM dbo.tblExpStock " & _
                                    "WHERE (Assortment = '" & strAssortment & "') AND (CONVERT(datetime, CONVERT(varchar(10),SystemDateTime, 101)) >= '" & Format(dtpFromDate, "MM/dd/yyyy") & "')", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        If Not IsDBNull(rsComSql_2.Fields("Pcs").Value) Then
                            dblInPcs = rsComSql_2.Fields("Pcs").Value
                        End If
                    End If
                    rsComSql_2 = Nothing

                    'Out
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT SUM(ExportPcs) AS Pcs " & _
                                    "FROM dbo.tblCosting " & _
                                    "WHERE (Assortment = '" & strAssortment & "') AND (Department = 'Mix') AND (DateCreated >= '" & Format(dtpFromDate, "MM/dd/yyyy") & "')", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        If Not IsDBNull(rsComSql_2.Fields("Pcs").Value) Then
                            dblExpPcs = rsComSql_2.Fields("Pcs").Value
                        End If
                    End If
                    rsComSql_2 = Nothing

                    'Direct Out
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT SUM(ExportPcs) AS Pcs " & _
                                    "FROM dbo.tblCosting " & _
                                    "WHERE (Assortment = '" & strAssortment & "') AND (Department = 'Exports') AND (DateCreated >= '" & Format(dtpFromDate, "MM/dd/yyyy") & "')", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        If Not IsDBNull(rsComSql_2.Fields("Pcs").Value) Then
                            dblDExpPcs = rsComSql_2.Fields("Pcs").Value
                        End If
                    End If
                    rsComSql_2 = Nothing

                    'Current
                    'rsComSql_2 = New ADODB.Recordset
                    'rsComSql_2.Open("SELECT InPcs,OutPcs,InCts,OutCts " & _
                    '                "FROM dbo.VW_MixAssortInOutNew " & _
                    '                "WHERE (Assortment = '" & strAssortment & "')", AdoCN, 1, 1)
                    'If rsComSql_2.RecordCount Then
                    '    If Not IsDBNull(rsComSql_2.Fields("InPcs").Value) Then
                    '        dblCurPcs = rsComSql_2.Fields("InPcs").Value - rsComSql_2.Fields("OutPcs").Value
                    '        dblCurCts = Math.Round(rsComSql_2.Fields("InCts").Value - rsComSql_2.Fields("OutCts").Value, 3)
                    '    End If
                    'End If
                    'rsComSql_2 = Nothing

                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT BoxPcs, ProdPcs, BoxCts, ProdCts, BankCts " & _
                                  "FROM VW_MixPCUStock2020 " & _
                                  "WHERE Assortment = '" & strAssortment & "'", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        dblCurPcs = rsComSql_2.Fields("BoxPcs").Value + rsComSql_2.Fields("ProdPcs").Value
                        dblCurCts = Math.Round(rsComSql_2.Fields("BoxCts").Value + IIf(rsComSql_2.Fields("ProdCts").Value > 0, rsComSql_2.Fields("ProdCts").Value, 0) + rsComSql_2.Fields("BankCts").Value, 3)

                        rsComSql_3 = New ADODB.Recordset
                        rsComSql_3.Open("SELECT RetPcs, RetCts FROM VW_MixPktRejExpNewY WHERE Assortment = '" & strAssortment & "'", AdoCN, 1, 1)
                        If rsComSql_3.RecordCount Then
                            dblCurPcs = dblCurPcs + rsComSql_3.Fields("RetPcs").Value
                            dblCurCts = dblCurCts + rsComSql_3.Fields("RetCts").Value
                        End If
                        rsComSql_3 = Nothing

                        dblCurCts = Math.Round(dblCurCts, 3)
                    End If

                    dblTurnOver = 0

                    strNewAssort = "ANW" & strRight(strAssortment, 4)

                    'rsComSql_2 = New ADODB.Recordset
                    'rsComSql_2.Open("SELECT BagAssortment " & _
                    '                "FROM dbo.tblAssortMatch2 " & _
                    '                "WHERE (NewAssortment = '" & strNewAssort & "')", AdoCN, 1, 1)
                    'If rsComSql_2.RecordCount Then
                    '    strBagAssort = rsComSql_2.Fields("BagAssortment").Value
                    'End If
                    'rsComSql_2 = Nothing

                    'If strBagAssort <> "" Then
                    '    rsComSql_2 = New ADODB.Recordset
                    '    rsComSql_2.Open("SELECT PRICE " & _
                    '                    "FROM dbo.tblGrading_SizingList " & _
                    '                    "WHERE (NAME = '" & strBagAssort & "')", AdoCN, 1, 1)
                    '    If rsComSql_2.RecordCount Then
                    '        dblBagPrice = rsComSql_2.Fields("Price").Value
                    '    End If
                    '    rsComSql_2 = Nothing
                    '    dblBagPrice = Math.Round(dblBagPrice, 2)
                    'End If

                    If rsComSql_1.Fields("LengthFrom").Value < 1.9 Then
                        strSizeRange = "A) 1.90-"
                    ElseIf rsComSql_1.Fields("LengthFrom").Value >= 1.9 And rsComSql_1.Fields("LengthFrom").Value < 2.4 Then
                        strSizeRange = "B) 1.90-2.39"
                    ElseIf rsComSql_1.Fields("LengthFrom").Value >= 2.4 And rsComSql_1.Fields("LengthFrom").Value < 2.9 Then
                        strSizeRange = "C) 2.40-2.89"
                    ElseIf rsComSql_1.Fields("LengthFrom").Value >= 2.9 And rsComSql_1.Fields("LengthFrom").Value < 3.4 Then
                        strSizeRange = "D) 2.90-3.39"
                    ElseIf rsComSql_1.Fields("LengthFrom").Value >= 3.4 And rsComSql_1.Fields("LengthFrom").Value < 3.9 Then
                        strSizeRange = "E) 3.40-3.89"
                    ElseIf rsComSql_1.Fields("LengthFrom").Value >= 3.9 And rsComSql_1.Fields("LengthFrom").Value < 4.4 Then
                        strSizeRange = "F) 3.90-4.39"
                    ElseIf rsComSql_1.Fields("LengthFrom").Value >= 4.4 And rsComSql_1.Fields("LengthFrom").Value < 4.9 Then
                        strSizeRange = "G) 4.40-4.89"
                    ElseIf rsComSql_1.Fields("LengthFrom").Value >= 4.9 And rsComSql_1.Fields("LengthFrom").Value < 5.4 Then
                        strSizeRange = "H) 4.90-5.39"
                    ElseIf rsComSql_1.Fields("LengthFrom").Value >= 5.4 And rsComSql_1.Fields("LengthFrom").Value < 5.9 Then
                        strSizeRange = "I) 5.40-5.89"
                    ElseIf rsComSql_1.Fields("LengthFrom").Value >= 5.9 And rsComSql_1.Fields("LengthFrom").Value < 6.4 Then
                        strSizeRange = "J) 5.90-6.39"
                    ElseIf rsComSql_1.Fields("LengthFrom").Value >= 6.4 And rsComSql_1.Fields("LengthFrom").Value < 6.9 Then
                        strSizeRange = "K) 6.40-6.89"
                    Else
                        strSizeRange = "L) 6.90+"
                    End If

                    flxDetails.Rows.Add(strAssortment, rsComSql_1.Fields("MarketPrice").Value, dblOpenPcs, dblInPcs,
                                        dblExpPcs, dblDExpPcs, dblCurPcs, dblCurCts, dblTurnOver, strBagAssort, dblBagPrice,
                                        rsComSql_1.Fields("AvgCost").Value, strSizeRange)

                    AdoCN.Execute("INSERT INTO tblDCL_TurnOver2(Assortment,ListPrice,OpenPcs,InPcs,ExpPcs,DExpPcs,CurPcs,CurCts,TurnOver,BagAssortment,BagPrice,AvgCost,StartDate,EndDate,SizeRange) " & _
                                  "VALUES('" & strAssortment & "'," & rsComSql_1.Fields("MarketPrice").Value & "," & dblOpenPcs & "," & dblInPcs & "," & dblExpPcs & "," & dblDExpPcs & "," & _
                                  "" & dblCurPcs & "," & dblCurCts & "," & dblTurnOver & ",'" & strBagAssort & "'," & dblBagPrice & "," & rsComSql_1.Fields("AvgCost").Value & "," & _
                                  "'" & Format(dtpFromDate, "MM/dd/yyyy") & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & strSizeRange & "')")
                End If
                rsComSql_1 = Nothing

                dblRecord = dblRecord + 1
                ExpProgress.Value = dblRecord
                txtCount.Text = dblRecord
                rsComSql.MoveNext()
                Application.DoEvents()
            End While
        End If
        rsComSql = Nothing

        ExpProgress.Visible = False
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        Load_AssortData()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub frm_DCLTurnOver_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        txtCount.Text = "0"
        txtTotCount.Text = "0"
    End Sub

    Private Sub Load_AssortDataCts()
        Dim dtpFromDate As Date
        Dim dblOpenPcs As Double
        Dim dblInPcs As Double
        Dim dblExpPcs As Double
        Dim dblDExpPcs As Double
        Dim dblCurPcs As Double
        Dim dblCurCts As Double
        Dim dblTurnOver As Double
        Dim dblBagPrice As Double
        Dim strAssortment As String
        Dim strNewAssort As String
        Dim strBagAssort As String
        Dim dblRecord As Double

        dtpFromDate = CDate("01/01/2019")

        AdoCN.Execute("DELETE FROM tblDCL_TurnOver3")
        flxDetails.Rows.Clear()

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT Assortment FROM dbo.tblAssortDetails WHERE (Len(Assortment) = 7) OR (Len(Assortment) = 6) GROUP BY Assortment ORDER BY Assortment", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()

            ExpProgress.Value = 0
            ExpProgress.Minimum = 0
            ExpProgress.Text = "Please wait ....."
            ExpProgress.Visible = True
            ExpProgress.Maximum = rsComSql.RecordCount
            dblRecord = 0

            While Not rsComSql.EOF
                dblOpenPcs = 0
                dblInPcs = 0
                dblExpPcs = 0
                dblDExpPcs = 0
                dblCurPcs = 0
                dblCurCts = 0
                dblTurnOver = 0
                strBagAssort = ""
                strNewAssort = ""
                dblBagPrice = 0

                strAssortment = rsComSql.Fields("Assortment").Value

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & strAssortment & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    'Open
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT SUM(Cts) AS Pcs " & _
                                    "FROM dbo.tblAssortOpen " & _
                                    "WHERE (Assortment = '" & strAssortment & "')", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        If Not IsDBNull(rsComSql_2.Fields("Pcs").Value) Then
                            dblOpenPcs = rsComSql_2.Fields("Pcs").Value
                        End If
                    End If
                    rsComSql_2 = Nothing

                    'In
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT SUM(Cts) AS Pcs " & _
                                    "FROM dbo.tblExpStock " & _
                                    "WHERE (Assortment = '" & strAssortment & "') AND (CONVERT(datetime, CONVERT(varchar(10),SystemDateTime, 101)) >= '" & Format(dtpFromDate, "MM/dd/yyyy") & "')", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        If Not IsDBNull(rsComSql_2.Fields("Pcs").Value) Then
                            dblInPcs = rsComSql_2.Fields("Pcs").Value
                        End If
                    End If
                    rsComSql_2 = Nothing

                    'Out
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT SUM(RoughCts) AS Pcs " & _
                                    "FROM dbo.tblCosting " & _
                                    "WHERE (Assortment = '" & strAssortment & "') AND (Department = 'Mix') AND (DateCreated >= '" & Format(dtpFromDate, "MM/dd/yyyy") & "')", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        If Not IsDBNull(rsComSql_2.Fields("Pcs").Value) Then
                            dblExpPcs = rsComSql_2.Fields("Pcs").Value
                        End If
                    End If
                    rsComSql_2 = Nothing

                    'Direct Out
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT SUM(ExportCts) AS Pcs " & _
                                    "FROM dbo.tblCosting " & _
                                    "WHERE (Assortment = '" & strAssortment & "') AND (Department = 'Exports') AND (DateCreated >= '" & Format(dtpFromDate, "MM/dd/yyyy") & "')", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        If Not IsDBNull(rsComSql_2.Fields("Pcs").Value) Then
                            dblDExpPcs = rsComSql_2.Fields("Pcs").Value
                        End If
                    End If
                    rsComSql_2 = Nothing

                    'Current
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT InPcs,OutPcs,InCts,OutCts " & _
                                    "FROM dbo.VW_MixAssortInOutNew " & _
                                    "WHERE (Assortment = '" & strAssortment & "')", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        If Not IsDBNull(rsComSql_2.Fields("InPcs").Value) Then
                            dblCurPcs = rsComSql_2.Fields("InPcs").Value - rsComSql_2.Fields("OutPcs").Value
                            dblCurCts = Math.Round(rsComSql_2.Fields("InCts").Value - rsComSql_2.Fields("OutCts").Value, 3)
                        End If
                    End If
                    rsComSql_2 = Nothing

                    dblTurnOver = 0

                    strNewAssort = "ANW" & strRight(strAssortment, 4)

                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT BagAssortment " & _
                                    "FROM dbo.tblAssortMatch2 " & _
                                    "WHERE (NewAssortment = '" & strNewAssort & "')", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        strBagAssort = rsComSql_2.Fields("BagAssortment").Value
                    End If
                    rsComSql_2 = Nothing

                    If strBagAssort <> "" Then
                        rsComSql_2 = New ADODB.Recordset
                        rsComSql_2.Open("SELECT PRICE " & _
                                        "FROM dbo.tblGrading_SizingList " & _
                                        "WHERE (NAME = '" & strBagAssort & "')", AdoCN, 1, 1)
                        If rsComSql_2.RecordCount Then
                            dblBagPrice = rsComSql_2.Fields("Price").Value
                        End If
                        rsComSql_2 = Nothing
                        dblBagPrice = Math.Round(dblBagPrice, 2)
                    End If

                    flxDetails.Rows.Add(strAssortment, rsComSql_1.Fields("MarketPrice").Value, dblOpenPcs, dblInPcs,
                                        dblExpPcs, dblDExpPcs, dblCurPcs, dblCurCts, dblTurnOver, strBagAssort, dblBagPrice, rsComSql_1.Fields("AvgCost").Value)

                    AdoCN.Execute("INSERT INTO tblDCL_TurnOver3(Assortment,ListPrice,OpenPcs,InPcs,ExpPcs,DExpPcs,CurPcs,CurCts,TurnOver,BagAssortment,BagPrice,AvgCost,StartDate,EndDate) " & _
                                  "VALUES('" & strAssortment & "'," & rsComSql_1.Fields("MarketPrice").Value & "," & dblOpenPcs & "," & dblInPcs & "," & dblExpPcs & "," & dblDExpPcs & "," & _
                                  "" & dblCurPcs & "," & dblCurCts & "," & dblTurnOver & ",'" & strBagAssort & "'," & dblBagPrice & "," & rsComSql_1.Fields("AvgCost").Value & ",'" & Format(dtpFromDate, "MM/dd/yyyy") & "','" & Format(Date.Now, "MM/dd/yyyy") & "')")
                End If
                rsComSql_1 = Nothing

                dblRecord = dblRecord + 1
                ExpProgress.Value = dblRecord
                rsComSql.MoveNext()
                Application.DoEvents()
            End While
        End If
        rsComSql = Nothing

        ExpProgress.Visible = False
    End Sub

    Private Sub cmdNew2_Click(sender As Object, e As EventArgs) Handles cmdNew2.Click
        Load_AssortDataCts()
    End Sub

    Private Sub UpdateSize()
        Dim strSizeRange As String

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblAssortList.LengthFrom, dbo.tblDCL_TurnOver2.Assortment " & _
                      "FROM dbo.tblDCL_TurnOver2 INNER JOIN dbo.tblAssortList ON dbo.tblDCL_TurnOver2.Assortment = dbo.tblAssortList.Assortment " & _
                      "GROUP BY dbo.tblAssortList.LengthFrom, dbo.tblDCL_TurnOver2.Assortment " & _
                      "ORDER BY dbo.tblAssortList.LengthFrom, dbo.tblDCL_TurnOver2.Assortment", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                If rsComSql.Fields("LengthFrom").Value < 1.9 Then
                    strSizeRange = "A) 1.90-"
                ElseIf rsComSql.Fields("LengthFrom").Value >= 1.9 And rsComSql.Fields("LengthFrom").Value < 2.4 Then
                    strSizeRange = "B) 1.90-2.39"
                ElseIf rsComSql.Fields("LengthFrom").Value >= 2.4 And rsComSql.Fields("LengthFrom").Value < 2.9 Then
                    strSizeRange = "C) 2.40-2.89"
                ElseIf rsComSql.Fields("LengthFrom").Value >= 2.9 And rsComSql.Fields("LengthFrom").Value < 3.4 Then
                    strSizeRange = "D) 2.90-3.39"
                ElseIf rsComSql.Fields("LengthFrom").Value >= 3.4 And rsComSql.Fields("LengthFrom").Value < 3.9 Then
                    strSizeRange = "E) 3.40-3.89"
                ElseIf rsComSql.Fields("LengthFrom").Value >= 3.9 And rsComSql.Fields("LengthFrom").Value < 4.4 Then
                    strSizeRange = "F) 3.90-4.39"
                ElseIf rsComSql.Fields("LengthFrom").Value >= 4.4 And rsComSql.Fields("LengthFrom").Value < 4.9 Then
                    strSizeRange = "G) 4.40-4.89"
                ElseIf rsComSql.Fields("LengthFrom").Value >= 4.9 And rsComSql.Fields("LengthFrom").Value < 5.4 Then
                    strSizeRange = "H) 4.90-5.39"
                ElseIf rsComSql.Fields("LengthFrom").Value >= 5.4 And rsComSql.Fields("LengthFrom").Value < 5.9 Then
                    strSizeRange = "I) 5.40-5.89"
                ElseIf rsComSql.Fields("LengthFrom").Value >= 5.9 And rsComSql.Fields("LengthFrom").Value < 6.4 Then
                    strSizeRange = "J) 5.90-6.39"
                ElseIf rsComSql.Fields("LengthFrom").Value >= 6.4 And rsComSql.Fields("LengthFrom").Value < 6.9 Then
                    strSizeRange = "K) 6.40-6.89"
                Else
                    strSizeRange = "L) 6.90+"
                End If

                AdoCN.Execute("UPDATE tblDCL_TurnOver2 SET SizeRange = '" & strSizeRange & "' WHERE Assortment = '" & rsComSql.Fields("Assortment").Value & "'")
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdUpdate_Click(sender As Object, e As EventArgs) Handles cmdUpdate.Click
        'UpdateSize()
        UpdateSizeWidth()
    End Sub

    Private Sub UpdateSizeWidth()
        Dim strSizeRange As String

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblAssortList.WidthFrom, dbo.tblDCL_TurnOver2.Assortment " & _
                      "FROM dbo.tblDCL_TurnOver2 INNER JOIN dbo.tblAssortList ON dbo.tblDCL_TurnOver2.Assortment = dbo.tblAssortList.Assortment " & _
                      "GROUP BY dbo.tblAssortList.WidthFrom, dbo.tblDCL_TurnOver2.Assortment " & _
                      "ORDER BY dbo.tblAssortList.WidthFrom, dbo.tblDCL_TurnOver2.Assortment", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                If rsComSql.Fields("WidthFrom").Value < 1.9 Then
                    strSizeRange = "A) 1.90-"
                ElseIf rsComSql.Fields("WidthFrom").Value >= 1.9 And rsComSql.Fields("WidthFrom").Value < 2.1 Then
                    strSizeRange = "B) 1.90-2.09"
                ElseIf rsComSql.Fields("WidthFrom").Value >= 2.1 And rsComSql.Fields("WidthFrom").Value < 2.3 Then
                    strSizeRange = "C) 2.10-2.29"
                ElseIf rsComSql.Fields("WidthFrom").Value >= 2.3 And rsComSql.Fields("WidthFrom").Value < 2.5 Then
                    strSizeRange = "D) 2.30-2.49"
                ElseIf rsComSql.Fields("WidthFrom").Value >= 2.5 And rsComSql.Fields("WidthFrom").Value < 2.7 Then
                    strSizeRange = "E) 2.50-2.69"
                ElseIf rsComSql.Fields("WidthFrom").Value >= 2.7 And rsComSql.Fields("WidthFrom").Value < 2.9 Then
                    strSizeRange = "F) 2.70-2.89"
                ElseIf rsComSql.Fields("WidthFrom").Value >= 2.9 And rsComSql.Fields("WidthFrom").Value < 3.1 Then
                    strSizeRange = "G) 2.90-3.09"
                ElseIf rsComSql.Fields("WidthFrom").Value >= 3.1 And rsComSql.Fields("WidthFrom").Value < 3.3 Then
                    strSizeRange = "H) 3.10-3.29"
                ElseIf rsComSql.Fields("WidthFrom").Value >= 3.3 And rsComSql.Fields("WidthFrom").Value < 3.5 Then
                    strSizeRange = "I) 3.30-3.49"
                ElseIf rsComSql.Fields("WidthFrom").Value >= 3.5 And rsComSql.Fields("WidthFrom").Value < 3.7 Then
                    strSizeRange = "J) 3.50-3.69"
                ElseIf rsComSql.Fields("WidthFrom").Value >= 3.7 And rsComSql.Fields("WidthFrom").Value < 3.9 Then
                    strSizeRange = "K) 3.70-3.89"
                ElseIf rsComSql.Fields("WidthFrom").Value >= 3.9 And rsComSql.Fields("WidthFrom").Value < 4.1 Then
                    strSizeRange = "L) 3.90-4.09"
                ElseIf rsComSql.Fields("WidthFrom").Value >= 4.1 And rsComSql.Fields("WidthFrom").Value < 4.3 Then
                    strSizeRange = "M) 4.10-4.29"
                ElseIf rsComSql.Fields("WidthFrom").Value >= 4.3 And rsComSql.Fields("WidthFrom").Value < 4.5 Then
                    strSizeRange = "N) 4.30-4.49"
                ElseIf rsComSql.Fields("WidthFrom").Value >= 4.5 And rsComSql.Fields("WidthFrom").Value < 4.7 Then
                    strSizeRange = "O) 4.50-4.69"
                ElseIf rsComSql.Fields("WidthFrom").Value >= 4.7 And rsComSql.Fields("WidthFrom").Value < 4.9 Then
                    strSizeRange = "P) 4.70-4.89"
                ElseIf rsComSql.Fields("WidthFrom").Value >= 4.9 And rsComSql.Fields("WidthFrom").Value < 5.1 Then
                    strSizeRange = "Q) 4.90-5.09"
                ElseIf rsComSql.Fields("WidthFrom").Value >= 5.1 And rsComSql.Fields("WidthFrom").Value < 5.3 Then
                    strSizeRange = "R) 5.10-5.29"
                ElseIf rsComSql.Fields("WidthFrom").Value >= 5.3 And rsComSql.Fields("WidthFrom").Value < 5.5 Then
                    strSizeRange = "S) 5.30-5.49"
                ElseIf rsComSql.Fields("WidthFrom").Value >= 5.5 And rsComSql.Fields("WidthFrom").Value < 5.7 Then
                    strSizeRange = "T) 5.50-5.69"
                ElseIf rsComSql.Fields("WidthFrom").Value >= 5.7 And rsComSql.Fields("WidthFrom").Value < 5.9 Then
                    strSizeRange = "U) 5.70-5.89"
                ElseIf rsComSql.Fields("WidthFrom").Value >= 5.9 And rsComSql.Fields("WidthFrom").Value < 6.1 Then
                    strSizeRange = "V) 5.90-6.09"
                ElseIf rsComSql.Fields("WidthFrom").Value >= 6.1 And rsComSql.Fields("WidthFrom").Value < 6.3 Then
                    strSizeRange = "W) 6.10-6.29"
                ElseIf rsComSql.Fields("WidthFrom").Value >= 6.3 And rsComSql.Fields("WidthFrom").Value < 6.5 Then
                    strSizeRange = "X) 6.30-6.49"
                ElseIf rsComSql.Fields("WidthFrom").Value >= 6.5 And rsComSql.Fields("WidthFrom").Value < 6.7 Then
                    strSizeRange = "Y) 6.50-6.69"
                Else
                    strSizeRange = "V) 6.70+"
                End If

                AdoCN.Execute("UPDATE tblDCL_TurnOver2 SET SizeRange1 = '" & strSizeRange & "' WHERE Assortment = '" & rsComSql.Fields("Assortment").Value & "'")
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXTurnOver2019.rpt"
        strReportPath = PBReportPath & "DiaSalesMix\" & mReportName
        objForm.Show()
    End Sub
End Class