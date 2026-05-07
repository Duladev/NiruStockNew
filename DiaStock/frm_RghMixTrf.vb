
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RghMixTrf

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub GetSupParNo()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(RIGHT(ParNo, 4)) AS ParNo  FROM tblPCUStockIn WHERE LEFT(ParNo, 2) = 'TR' AND LEFT(ParNo, 3) <> 'TRF'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("ParNo").Value) Then
                txtSupParNo.Text = "TR" & Format(rsComSql.Fields("ParNo").Value + 1, "0000")
            Else
                txtSupParNo.Text = "TR0001"
            End If
        Else
            txtSupParNo.Text = "TR0001"
        End If
        rsComSql = Nothing
    End Sub

    Private Sub GetExpMixParNo()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(RIGHT(ParNo, 5)) AS ParNo  FROM tblExpPacket WHERE LEFT(ParNo, 2) = 'BX'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("ParNo").Value) Then
                txtSupParNo.Text = "BX" & Format(rsComSql.Fields("ParNo").Value + 1, "00000")
            Else
                txtSupParNo.Text = "BX00001"
            End If
        Else
            txtSupParNo.Text = "BX00001"
        End If
        rsComSql = Nothing
    End Sub

    Private Sub GetNextPackListNo()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(PackNo) AS MaxNo FROM tblExpRejExports", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("MaxNo").Value) Then
                txtPackNo.Text = rsComSql.Fields("MaxNo").Value + 1
            Else
                txtPackNo.Text = "1"
            End If
        End If
        rsComSql = Nothing
    End Sub

    Private Sub frm_RghMixTrf_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        GetSupParNo()
        LoadRghMixTrf()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        If optMix.Checked = True Then
            GetSupParNo()
            LoadRghMixTrf()
        End If
        If optGrading.Checked = True Then
            LoadRghGradingTrf()
        End If
        If optPcu.Checked = True Then
            LoadRghPCUTrf()
        End If
        If optGrdPcu.Checked = True Then
            LoadGradingPCUTrf()
        End If
        If optSorting.Checked = True Then
            GetExpMixParNo()
            LoadBoxExpTrf()
        End If
        If optPolBox.Checked = True Then
            LoadPolishBoxTrf()
        End If
        If optReject.Checked = True Then
            LoadRejExport()
        End If
        If optMixPlan.Checked = True Then
            GetSupParNo()
            LoadRghMixPlanTrf()
        End If
        GetNextPackListNo()
    End Sub

    Private Sub LoadRghMixTrf()
        Dim intIndex As Integer

        txtTotCts.Text = "0"
        txtValue.Text = "0"
        intIndex = 0
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblAssortExportDetails.Assortment, SUM(dbo.tblAssortExportDetails.Pcs) AS Pcs, ROUND(SUM(dbo.tblAssortExportDetails.Cts), 3) AS Cts, " & _
                            "dbo.tblAssortList.MarketPrice " & _
                      "FROM dbo.tblAssortExportDetails INNER JOIN dbo.tblAssortExports ON dbo.tblAssortExportDetails.ExpNo = dbo.tblAssortExports.ExpNo INNER JOIN " & _
                            "dbo.tblAssortList ON dbo.tblAssortExportDetails.Assortment = dbo.tblAssortList.Assortment " & _
                      "WHERE (dbo.tblAssortExportDetails.Export = 2) And (dbo.tblAssortExportDetails.Status = 0) " & _
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
                                    "TRF" & Format(Val(strRight(txtSupParNo.Text, 5)) + intIndex, "00000"), "", "", "", "", False)

                txtTotPcs = CInt(txtTotPcs.Text) + rsComSql.Fields("Pcs").Value
                txtTotCts = CDbl(txtTotCts.Text) + rsComSql.Fields("Cts").Value
                txtValue = CDbl(txtValue.Text) + (rsComSql.Fields("Cts").Value * rsComSql.Fields("MarketPrice").Value)
                intIndex = intIndex + 1
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtTotCts.Text = Format(CDbl(txtTotCts.Text), "#0.000")
        txtValue.Text = Format(CDbl(txtValue.Text), "#0.00")
        If CDbl(txtTotCts.Text) > 0 Then
            txtPrice.Text = Format(CDbl(txtValue.Text) / CDbl(txtTotCts.Text), "#0.00")
        Else
            txtPrice.Text = "0"
        End If
    End Sub

    Private Sub LoadRghGradingTrf()
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        txtValue.Text = "0"
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT Department, ParNo, Assortment, SUM(IssPcs) AS IssPcs, ROUND(SUM(IssCts), 3) AS IssCts " & _
                      "FROM dbo.tblGrading_RghIssues " & _
                      "WHERE (OK = 0) AND (Type = 'R') AND (Department <> 'Direct Import') " & _
                      "GROUP BY Department, ParNo, Assortment " & _
                      "ORDER BY ParNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                rsComSql_1 = New ADODB.Recordset
                If rsComSql.Fields("Department").Value <> "GradingPCU" And rsComSql.Fields("Department").Value <> "GradingPCU_N" Then
                    rsComSql_1.Open("SELECT * FROM tblDep_Trf WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND DCLParcelNo = '" & Mid(rsComSql.Fields("ParNo").Value, 1, 6) & "'", AdoCN, 1, 1)
                Else
                    If Mid(rsComSql.Fields("ParNo").Value, 1, 1) = "5" Then
                        rsComSql_1.Open("SELECT * FROM tblDep_Trf WHERE DCLParcelNo = '" & rsComSql.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                    Else
                        rsComSql_1.Open("SELECT * FROM tblDep_Trf WHERE Department = 'Precision' AND DCLParcelNo = '" & Mid(rsComSql.Fields("ParNo").Value, 1, 6) & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT * FROM tblDep_Trf WHERE Department = 'Precision' AND DCLParcelNo = '" & rsComSql.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                        End If
                    End If
                End If
                If rsComSql_1.RecordCount Then
                    flxDetails.Rows.Add(rsComSql.Fields("Assortment").Value,
                                        rsComSql.Fields("IssPcs").Value,
                                        rsComSql.Fields("IssCts").Value,
                                        "0",
                                        "0",
                                        rsComSql.Fields("ParNo").Value,
                                        rsComSql.Fields("Department").Value,
                                        rsComSql_1.Fields("AssortmentNo").Value,
                                        rsComSql_1.Fields("SupParcelNo").Value,
                                        rsComSql_1.Fields("DclParcelNo").Value, False)

                    txtTotPcs.Text = CInt(txtTotPcs.Text) + rsComSql.Fields("IssPcs").Value
                    txtTotCts.Text = CDbl(txtTotCts.Text) + rsComSql.Fields("IssCts").Value
                End If
                rsComSql_1 = Nothing

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT Department, ParNo, Assortment, SUM(IssPcs) AS IssPcs, ROUND(SUM(IssCts), 3) AS IssCts " & _
                      "FROM dbo.tblGrading_RghIssues " & _
                      "WHERE (OK = 0) AND (Type = 'R') AND (Department = 'Direct Import') " & _
                      "GROUP BY Department, ParNo, Assortment " & _
                      "ORDER BY ParNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblDep_Trf WHERE Department = 'Grading' AND DCLParcelNo = '" & rsComSql.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    flxDetails.Rows.Add(rsComSql.Fields("Assortment").Value,
                                        rsComSql.Fields("IssPcs").Value,
                                        rsComSql.Fields("IssCts").Value,
                                        "0",
                                        "0",
                                        rsComSql.Fields("ParNo").Value,
                                        rsComSql.Fields("Department").Value,
                                        rsComSql_1.Fields("AssortmentNo").Value,
                                        rsComSql_1.Fields("SupParcelNo").Value,
                                        rsComSql_1.Fields("DclParcelNo").Value, False)

                    txtTotPcs.Text = CInt(txtTotPcs.Text) + rsComSql.Fields("IssPcs").Value
                    txtTotCts.Text = CDbl(txtTotCts.Text) + rsComSql.Fields("IssCts").Value
                End If
                rsComSql_1 = Nothing

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtTotCts.Text = Format(CDbl(txtTotCts.Text), "#0.000")
        txtValue.Text = "0"
        txtPrice.Text = "0"
    End Sub

    Private Sub LoadRghPCUTrf()
        Dim strAssortment As String

        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        txtValue.Text = "0"
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT Department, Assortment, Assort1, ParNo, SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts " & _
                      "FROM dbo.tblExpRghTypes " & _
                      "WHERE (OK = 0) AND (Type = 'R') " & _
                      "GROUP BY Department, Assortment, Assort1, ParNo " & _
                      "ORDER BY ParNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                If rsComSql.Fields("Assort1").Value <> "" Then
                    strAssortment = rsComSql.Fields("Assort1").Value
                Else
                    strAssortment = rsComSql.Fields("Assortment").Value
                End If
                rsComSql_1 = New ADODB.Recordset
                If rsComSql.Fields("Department").Value = "Mix" Then
                    rsComSql_1.Open("SELECT * FROM tblDep_Trf WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND DCLParcelNo = '" & rsComSql.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                ElseIf rsComSql.Fields("Department").Value = "Direct Import" Then
                    rsComSql_1.Open("SELECT * FROM tblDep_Trf WHERE Department = 'Grading' AND DCLParcelNo = '" & rsComSql.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                Else
                    rsComSql_1.Open("SELECT * FROM tblDep_Trf WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND DCLParcelNo = '" & Mid(rsComSql.Fields("ParNo").Value, 1, 6) & "'", AdoCN, 1, 1)
                End If
                If rsComSql_1.RecordCount Then
                    If strAssortment = "" Then
                        strAssortment = rsComSql_1.Fields("AssortmentNo").Value
                    End If
                    flxDetails.Rows.Add(strAssortment,
                                        rsComSql.Fields("Pcs").Value,
                                        rsComSql.Fields("Cts").Value,
                                        "0",
                                        "0",
                                        rsComSql.Fields("ParNo").Value,
                                        rsComSql.Fields("Department").Value,
                                        rsComSql_1.Fields("AssortmentNo").Value,
                                        rsComSql_1.Fields("SupParcelNo").Value,
                                        rsComSql_1.Fields("DclParcelNo").Value, False)
                Else
                    flxDetails.Rows.Add(strAssortment,
                                        rsComSql.Fields("Pcs").Value,
                                        rsComSql.Fields("Cts").Value,
                                        "0",
                                        "0",
                                        rsComSql.Fields("ParNo").Value,
                                        rsComSql.Fields("Department").Value,
                                        "APCU",
                                        rsComSql.Fields("ParNo").Value,
                                        rsComSql.Fields("ParNo").Value, False)
                End If
                rsComSql_1 = Nothing

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

    Private Sub LoadGradingPCUTrf()
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        txtValue.Text = "0"
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT Department, ParNo, Assortment, SUM(IssPcs) AS IssPcs, ROUND(SUM(IssCts), 3) AS IssCts " & _
                      "FROM dbo.tblGrading_RghIssues " & _
                      "WHERE (OK = 0) AND (Type = 'P') " & _
                      "GROUP BY Department, ParNo, Assortment " & _
                      "ORDER BY ParNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                rsComSql_1 = New ADODB.Recordset
                If rsComSql.Fields("Department").Value = "GradingPCU" Then
                    rsComSql_1.Open("SELECT * FROM tblDep_Trf WHERE DCLParcelNo = '" & Mid(rsComSql.Fields("ParNo").Value, 1, 6) & "'", AdoCN, 1, 1)
                ElseIf rsComSql.Fields("Department").Value = "Mix" Then
                    rsComSql_1.Open("SELECT * FROM tblDep_Trf WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND DCLParcelNo = '" & rsComSql.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                ElseIf rsComSql.Fields("Department").Value = "Direct Import" Then
                    rsComSql_1.Open("SELECT * FROM tblDep_Trf WHERE Department = 'Grading' AND DCLParcelNo = '" & rsComSql.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                Else
                    rsComSql_1.Open("SELECT * FROM tblDep_Trf WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND DCLParcelNo = '" & Mid(rsComSql.Fields("ParNo").Value, 1, 6) & "'", AdoCN, 1, 1)
                End If
                If rsComSql_1.RecordCount Then
                    flxDetails.Rows.Add(rsComSql.Fields("Assortment").Value,
                                        rsComSql.Fields("IssPcs").Value,
                                        rsComSql.Fields("IssCts").Value,
                                        "0",
                                        "0",
                                        rsComSql.Fields("ParNo").Value,
                                        rsComSql.Fields("Department").Value,
                                        rsComSql_1.Fields("AssortmentNo").Value,
                                        rsComSql_1.Fields("SupParcelNo").Value,
                                        rsComSql_1.Fields("DclParcelNo").Value, False)

                    txtTotPcs.Text = CInt(txtTotPcs.Text) + rsComSql.Fields("IssPcs").Value
                    txtTotCts.Text = CDbl(txtTotCts.Text) + rsComSql.Fields("IssCts").Value

                Else
                    If rsComSql.Fields("Department").Value = "GradingMix" Then
                        flxDetails.Rows.Add(rsComSql.Fields("Assortment").Value,
                                            rsComSql.Fields("IssPcs").Value,
                                            rsComSql.Fields("IssCts").Value,
                                            "0",
                                            "0",
                                            rsComSql.Fields("ParNo").Value,
                                            rsComSql.Fields("Department").Value,
                                            "APCU",
                                            rsComSql_1.Fields("ParNo").Value,
                                            rsComSql_1.Fields("ParNo").Value, False)

                        txtTotPcs.Text = CInt(txtTotPcs.Text) + rsComSql.Fields("IssPcs").Value
                        txtTotCts.Text = CDbl(txtTotCts.Text) + rsComSql.Fields("IssCts").Value
                    End If
                End If
                rsComSql_1 = Nothing

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtTotCts.Text = Format(CDbl(txtTotCts.Text), "#0.000")
        txtValue.Text = "0"
        txtPrice.Text = "0"
    End Sub

    Private Sub LoadBoxExpTrf()
        Dim intIndex As Integer

        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        txtValue.Text = "0"
        intIndex = 0
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblAssortExportDetails.Assortment, SUM(dbo.tblAssortExportDetails.Pcs) AS Pcs, ROUND(SUM(dbo.tblAssortExportDetails.Cts), 3) AS Cts, " & _
                            "dbo.tblAssortList.MarketPrice " & _
                      "FROM dbo.tblAssortExportDetails INNER JOIN dbo.tblAssortExports ON dbo.tblAssortExportDetails.ExpNo = dbo.tblAssortExports.ExpNo AND " & _
                            "dbo.tblAssortExportDetails.Assortment = dbo.tblAssortExports.Assortment INNER JOIN dbo.tblAssortList ON dbo.tblAssortExportDetails.Assortment = dbo.tblAssortList.Assortment " & _
                      "WHERE (dbo.tblAssortExportDetails.Export = 4) And (dbo.tblAssortExportDetails.Status = 0) " & _
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
                                    "BX" & Format(Val(strRight(txtSupParNo.Text, 5)) + intIndex, "00000"),
                                    "PcuMix", "", "", "", False)

                txtTotPcs.Text = CInt(txtTotPcs.Text) + rsComSql.Fields("Pcs").Value
                txtTotCts.Text = CDbl(txtTotCts.Text) + rsComSql.Fields("Cts").Value
                txtValue.Text = CDbl(txtValue.Text) + (rsComSql.Fields("Cts").Value * rsComSql.Fields("MarketPrice").Value)
                intIndex = intIndex + 1
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtTotCts.Text = Format(CDbl(txtTotCts.Text), "#0.000")
        txtValue.Text = Format(CDbl(txtValue.Text), "#0.00")
        If CDbl(txtTotCts.Text) > 0 Then
            txtPrice.Text = Format(CDbl(txtValue.Text) / CDbl(txtTotCts.Text), "#0.00")
        Else
            txtPrice.Text = "0"
        End If
    End Sub

    Private Sub LoadPolishBoxTrf()
        Dim intIndex As Integer

        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        txtValue.Text = "0"
        intIndex = 0
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Assortment,ParNo,Price, SUM(InPcs) AS Pcs, ROUND(SUM(InCts), 3) AS Cts " & _
                      "FROM tblPCUStockIn " & _
                      "WHERE (Status = 0) " & _
                      "GROUP BY Assortment,ParNo,Price " & _
                      "ORDER BY Assortment", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("Assortment").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    rsComSql.Fields("Price").Value,
                                    Format(rsComSql.Fields("Cts").Value * rsComSql.Fields("Price").Value, "#0.00"),
                                    rsComSql.Fields("ParNo").Value,
                                    "PolishBox", "", "", "", False)

                txtTotPcs.Text = CInt(txtTotPcs.Text) + rsComSql.Fields("Pcs").Value
                txtTotCts.Text = CDbl(txtTotCts.Text) + rsComSql.Fields("Cts").Value
                txtValue.Text = CDbl(txtValue.Text) + (rsComSql.Fields("Cts").Value * rsComSql.Fields("Price").Value)
                intIndex = intIndex + 1
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtTotCts.Text = Format(CDbl(txtTotCts.Text), "#0.000")
        txtValue.Text = Format(CDbl(txtValue.Text), "#0.00")
        If CDbl(txtTotCts.Text) > 0 Then
            txtPrice.Text = Format(CDbl(txtValue.Text) / CDbl(txtTotCts.Text), "#0.00")
        Else
            txtPrice.Text = "0"
        End If
    End Sub

    Private Sub LoadRejExport()
        Dim strAssortment As String

        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        txtValue.Text = "0"
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT Department, Assortment, Assort1, ParNo, SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts " & _
                      "FROM dbo.tblExpRghTypes " & _
                      "WHERE (OK = 0) AND (Type = 'E') " & _
                      "GROUP BY Department, Assortment, Assort1, ParNo " & _
                      "ORDER BY ParNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                If rsComSql.Fields("Assort1").Value <> "" Then
                    strAssortment = rsComSql.Fields("Assort1").Value
                Else
                    strAssortment = rsComSql.Fields("Assortment").Value
                End If
                rsComSql_1 = New ADODB.Recordset
                If rsComSql.Fields("Department").Value = "Mix" Then
                    rsComSql_1.Open("SELECT * FROM tblDep_Trf WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND DCLParcelNo = '" & rsComSql.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                ElseIf rsComSql.Fields("Department").Value = "Direct Import" Then
                    rsComSql_1.Open("SELECT * FROM tblDep_Trf WHERE Department = 'Grading' AND DCLParcelNo = '" & rsComSql.Fields("ParNo").Value & "'", AdoCN, 1, 1)
                Else
                    rsComSql_1.Open("SELECT * FROM tblDep_Trf WHERE Department = '" & rsComSql.Fields("Department").Value & "' AND DCLParcelNo = '" & Mid(rsComSql.Fields("ParNo").Value, 1, 6) & "'", AdoCN, 1, 1)
                End If
                If rsComSql_1.RecordCount Then
                    flxDetails.Rows.Add(strAssortment,
                                        rsComSql.Fields("Pcs").Value,
                                        rsComSql.Fields("Cts").Value,
                                        "0",
                                        "0",
                                        rsComSql.Fields("ParNo").Value,
                                        rsComSql.Fields("Department").Value,
                                        rsComSql_1.Fields("AssortmentNo").Value,
                                        rsComSql_1.Fields("SupParcelNo").Value,
                                        rsComSql_1.Fields("DclParcelNo").Value, False)
                Else
                    flxDetails.Rows.Add(strAssortment,
                                        rsComSql.Fields("Pcs").Value,
                                        rsComSql.Fields("Cts").Value,
                                        "0",
                                        "0",
                                        rsComSql.Fields("ParNo").Value,
                                        rsComSql.Fields("Department").Value,
                                        "APCU",
                                        rsComSql.Fields("ParNo").Value,
                                        rsComSql.Fields("ParNo").Value, False)
                End If
                rsComSql_1 = Nothing

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

    Private Sub LoadRghMixPlanTrf()
        Dim intIndex As Integer

        txtTotCts.Text = "0"
        txtValue.Text = "0"
        intIndex = 0
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblAssortExportDetails.Assortment, SUM(dbo.tblAssortExportDetails.Pcs) AS Pcs, ROUND(SUM(dbo.tblAssortExportDetails.Cts), 3) AS Cts, " & _
                            "dbo.tblAssortList.MarketPrice " & _
                      "FROM dbo.tblAssortExportDetails INNER JOIN dbo.tblAssortExports ON dbo.tblAssortExportDetails.ExpNo = dbo.tblAssortExports.ExpNo INNER JOIN " & _
                            "dbo.tblAssortList ON dbo.tblAssortExportDetails.Assortment = dbo.tblAssortList.Assortment " & _
                      "WHERE (dbo.tblAssortExportDetails.Export = 6) And (dbo.tblAssortExportDetails.Status = 0) " & _
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
                                    "TRF" & Format(Val(strRight(txtSupParNo.Text, 5)) + intIndex, "00000"), "", "", "", "", False)

                txtTotPcs.Text = CInt(txtTotPcs.Text) + rsComSql.Fields("Pcs").Value
                txtTotCts.Text = CDbl(txtTotCts.Text) + rsComSql.Fields("Cts").Value
                txtValue.Text = CDbl(txtValue.Text) + (rsComSql.Fields("Cts").Value * rsComSql.Fields("MarketPrice").Value)
                intIndex = intIndex + 1
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtTotCts.Text = Format(CDbl(txtTotCts.Text), "#0.000")
        txtValue.Text = Format(CDbl(txtValue.Text), "#0.00")
        If CDbl(txtTotCts.Text) > 0 Then
            txtPrice.Text = Format(CDbl(txtValue.Text) / CDbl(txtTotCts.Text), "#0.00")
        Else
            txtPrice.Text = "0"
        End If
    End Sub

    Private Sub Save2()
        Dim strSupParNo As String
        Dim intIndex As Integer
        Dim strOrgAssort As String
        Dim blnSave As Boolean
        Dim strPktNo As String
        Dim intSec As Integer

        If Len(txtTotPcs.Text) = 0 Then Exit Sub
        If CInt(txtTotPcs.Text) <= 0 Then Exit Sub

        blnSave = False
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse  = MsgBoxResult.Yes Then
            For intIndex = 0 To flxDetails.Rows.Count - 1
                'MIX
                If optMix.Checked = True Then
                    strSupParNo = flxDetails.Item(5, intIndex).Value
                    If Mid(flxDetails.Item(0, intIndex).Value, 1, 2) = "VM" Or Mid(flxDetails.Item(0, intIndex).Value, 1, 2) = "VP" Then
                        strOrgAssort = "VPCU"
                    Else
                        strOrgAssort = "APCU"
                    End If

                    AdoCN.Execute("INSERT INTO tblPCUStockIn(OrgAssort,Assortment,ParNo,InPcs,InCts,Price) " & _
                                  "VALUES('" & strOrgAssort & "','" & flxDetails.Item(0, intIndex).Value & "'," & _
                                        "'" & flxDetails.Item(5, intIndex).Value & "'," & Val(flxDetails.Item(1, intIndex).Value) & "," & _
                                        "" & CDbl(flxDetails.Item(2, intIndex).Value) & "," & CDbl(flxDetails.Item(3, intIndex).Value) & ")")

                    AdoCN.Execute("UPDATE tblAssortExportDetails SET Status = 1,ParNo = '" & flxDetails.Item(5, intIndex).Value & "' WHERE Assortment = '" & flxDetails.Item(0, intIndex).Value & "' AND Status = 0 AND Export = 2")

                    blnSave = True
                End If
                'Grading
                If optGrading.Checked = True Then
                    If flxDetails.Item(10, intIndex).Value = True Then
                        If flxDetails.Item(6, intIndex).Value <> "Direct Import" Then
                            If chkOrder.Checked = True Then
                                AdoCN.Execute("INSERT INTO tblPCUStockIn(OrgAssort,Assortment,ParNo,InPcs,InCts,Price,Status) " & _
                                              "VALUES('" & flxDetails.Item(7, intIndex).Value & "','" & flxDetails.Item(0, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "'," & _
                                                    "" & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & "," & CDbl(flxDetails.Item(3, intIndex).Value) & ",1)")
                            Else
                                AdoCN.Execute("INSERT INTO tblDep_Rec(Department,AssortmentNo,SupParcelNo,DCLParcelNo,Pcs,Cts,RghCts) " & _
                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(7, intIndex).Value & "','" & flxDetails.Item(8, intIndex).Value & "'," & _
                                                "'" & flxDetails.Item(9, intIndex).Value & "'," & CDbl(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ")")
                            End If
                        Else
                            AdoCN.Execute("INSERT INTO tblDep_Rec(Department,AssortmentNo,SupParcelNo,DCLParcelNo,Pcs,Cts,RghCts) " & _
                                          "VALUES('Grading','" & flxDetails.Item(7, intIndex).Value & "','" & flxDetails.Item(8, intIndex).Value & "'," & _
                                            "'" & flxDetails.Item(9, intIndex).Value & "'," & CDbl(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ")")
                        End If

                        AdoCN.Execute("UPDATE tblGrading_RghIssues SET OK = 1 WHERE ParNo = '" & flxDetails.Item(5, intIndex).Value & "' AND OK = 0 AND Type = 'R'")

                        blnSave = True
                    End If
                End If
                'PCU Sorting
                If optPcu.Checked = True Then
                    If flxDetails.Item(10, intIndex).Value = True Then
                        If chkExport.Checked = False Then
                            If flxDetails.Item(6, intIndex).Value <> "GradingMix" And flxDetails.Item(6, intIndex).Value <> "PcuMix" Then
                                If chkOrder.Checked = True Or chkKOrder.Checked = True Then
                                    If chkOrder.Checked = True Then
                                        AdoCN.Execute("INSERT INTO tblPCUStockIn(OrgAssort,Assortment,ParNo,InPcs,InCts,Price,Status) " & _
                                                      "VALUES('" & flxDetails.Item(7, intIndex).Value & "','" & flxDetails.Item(0, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "'," & _
                                                        "" & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",0,1)")
                                    Else
                                        AdoCN.Execute("INSERT INTO tblPCUStockIn(OrgAssort,Assortment,ParNo,InPcs,InCts,Price,Status) " & _
                                                      "VALUES('" & flxDetails.Item(7, intIndex).Value & "','" & flxDetails.Item(0, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "'," & _
                                                        "" & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",0,1)")
                                    End If
                                Else
                                    AdoCN.Execute("INSERT INTO tblDep_Rec(Department,AssortmentNo,SupParcelNo,DCLParcelNo,Pcs,Cts,RghCts) " & _
                                                  "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(7, intIndex).Value & "','" & flxDetails.Item(8, intIndex).Value & "'," & _
                                                    "'" & flxDetails.Item(9, intIndex).Value & "'," & CDbl(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ")")
                                End If
                            Else
                                AdoCN.Execute("INSERT INTO tblPCUStockIn(OrgAssort,Assortment,ParNo,InPcs,InCts,Price,Status) " & _
                                              "VALUES('" & flxDetails.Item(7, intIndex).Value & "','" & flxDetails.Item(0, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "'," & _
                                                "" & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",0,1)")
                            End If
                        Else
                            AdoCN.Execute("INSERT INTO tblExpRejExports(Department,ParNo,OrgAssort,Pcs,Cts,PackNo) " & _
                                          "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(9, intIndex).Value & "','" & flxDetails.Item(7, intIndex).Value & "'," & CDbl(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & "," & CDbl(txtPackNo.Text) & ")")
                        End If
                        AdoCN.Execute("UPDATE tblExpRghTypes SET OK = 1 WHERE ParNo = '" & flxDetails.Item(5, intIndex).Value & "' AND OK = 0 AND Type = 'R'")

                        blnSave = True
                    End If
                End If
                'Grading-PCU Sorting
                If optGrdPcu.Checked = True Then
                    strPktNo = "G001"
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT MAX(RIGHT(PktNo, 3)) AS MaxPktNo FROM tblExpPacket WHERE Department = '" & flxDetails.Item(6, intIndex).Value & "' AND ParNo = '" & flxDetails.Item(5, intIndex).Value & "' AND LEFT(PktNo, 1) = 'G'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If Not IsDBNull(rsComSql.Fields("MaxPktNo").Value) Then
                            strPktNo = "G" & Format(CInt(rsComSql.Fields("MaxPktNo").Value) + 1, "000")
                        End If
                    End If
                    rsComSql = Nothing

                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblExpPacket WHERE Department = '" & flxDetails.Item(6, intIndex).Value & "' AND ParNo = '" & flxDetails.Item(5, intIndex).Value & "' AND PktNo = '" & strPktNo & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        AdoCN.Execute("INSERT INTO tblExpPacket(Department,ParNo,PktNo,PktPcs,PktCts,PktType,PktDate,AParNo) " & _
                                      "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",'','" & Format(Date.Now, "MM/dd/yyyy") & "','" & flxDetails.Item(0, intIndex).Value & "')")
                    End If
                    rsComSql = Nothing

                    For intSec = 1 To 3
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT * FROM tblExpIssues WHERE Department = '" & flxDetails.Item(6, intIndex).Value & "' AND ParNo = '" & flxDetails.Item(5, intIndex).Value & "' AND PktNo = '" & strPktNo & "' AND Sec = " & intSec & "", AdoCN, 1, 1)
                        If rsComSql.RecordCount = 0 Then
                            AdoCN.Execute("INSERT INTO tblExpIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                          "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "'," & intSec & ",'D08411'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & _
                                                 "" & CDbl(flxDetails.Item(2, intIndex).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")
                        End If
                        rsComSql = Nothing

                        If intSec < 3 Then
                            rsComSql = New ADODB.Recordset
                            rsComSql.Open("SELECT * FROM tblExpReturns WHERE Department = '" & flxDetails.Item(6, intIndex).Value & "' AND ParNo = '" & flxDetails.Item(5, intIndex).Value & "' AND PktNo = '" & strPktNo & "' AND Sec = " & intSec & "", AdoCN, 1, 1)
                            If rsComSql.RecordCount = 0 Then
                                AdoCN.Execute("INSERT INTO tblExpReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts,ColPcs,ColCts,FloPcs,FloCts,IncPcs,IncCts) " & _
                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "'," & intSec & ",'D08411'," & _
                                                    "" & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,0,0,0,0)")
                            End If
                            rsComSql = Nothing

                            AdoCN.Execute("DELETE FROM tblExpReturnDetails WHERE Department = '" & flxDetails.Item(6, intIndex).Value & "' AND ParNo = '" & flxDetails.Item(5, intIndex).Value & "' AND PktNo = '" & strPktNo & "' AND Sec = " & intSec & "")
                            AdoCN.Execute("INSERT INTO tblExpReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                                          "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "'," & intSec & ",'MIX'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ")")
                        End If
                    Next

                    AdoCN.Execute("UPDATE tblGrading_RghIssues SET OK = 1 WHERE ParNo = '" & flxDetails.Item(5, intIndex).Value & "' AND Assortment = '" & flxDetails.Item(0, intIndex).Value & "' AND OK = 0 AND Type = 'P'")

                    blnSave = True
                End If
                'MIX Sorting
                If optSorting.Checked = True Then
                    If flxDetails.Item(10, intIndex).Value = True Then
                        strPktNo = "B001"
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT MAX(RIGHT(PktNo, 3)) AS MaxPktNo FROM tblExpPacket WHERE Department = '" & flxDetails.Item(6, intIndex).Value & "' AND ParNo = '" & flxDetails.Item(5, intIndex).Value & "' AND LEFT(PktNo, 1) = 'B'", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            If Not IsDBNull(rsComSql.Fields("MaxPktNo").Value) Then
                                strPktNo = "B" & Format(CInt(rsComSql.Fields("MaxPktNo").Value) + 1, "000")
                            End If
                        End If
                        rsComSql = Nothing

                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT * FROM tblExpPacket WHERE Department = '" & flxDetails.Item(6, intIndex).Value & "' AND ParNo = '" & flxDetails.Item(5, intIndex).Value & "' AND PktNo = '" & strPktNo & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount = 0 Then
                            AdoCN.Execute("INSERT INTO tblExpPacket(Department,ParNo,PktNo,PktPcs,PktCts,PktType,PktDate,AParNo) " & _
                                          "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",'','" & Format(Date.Now, "MM/dd/yyyy") & "','" & flxDetails.Item(0, intIndex).Value & "')")

                            If chkSizing.Checked = True Then
                                '====================================
                                'Fluorescent Checking Issues
                                AdoCN.Execute("INSERT INTO tblExpIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "',1,'D08411'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")

                                'Fluorescent Checking Returns
                                AdoCN.Execute("INSERT INTO tblExpReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "',1,'D08411'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0)")

                                'Fluorescent Checking Return Details
                                AdoCN.Execute("INSERT INTO tblExpReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "',1,'NONE'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ")")

                                '====================================
                                'Color Sorting Issues
                                AdoCN.Execute("INSERT INTO tblExpIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "',2,'D08411'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")

                                'Color Sorting Returns
                                AdoCN.Execute("INSERT INTO tblExpReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "',2,'D08411'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0)")

                                'Color Sorting Return Details
                                AdoCN.Execute("INSERT INTO tblExpReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "',2,'COLOR'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ")")

                                '====================================
                                'Clarity Checking Issues
                                AdoCN.Execute("INSERT INTO tblExpIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "',3,'D08411'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")

                                'Clarity Checking Returns
                                AdoCN.Execute("INSERT INTO tblExpReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "',3,'D08411'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0)")

                                'Clarity Checking Return Details
                                AdoCN.Execute("INSERT INTO tblExpReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "',3,'COLOR'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ")")

                                '====================================
                                'Sizing Packet
                                AdoCN.Execute("INSERT INTO tblExpSizingPacket(Department, ParNo, PktNo, PktPcs, PktCts, ReturnType) " & _
                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",'COLOR')")

                                'Sizing Issues
                                AdoCN.Execute("INSERT INTO tblExpSizingIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "',1,'D08411'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")

                                'Sizing Returns
                                AdoCN.Execute("INSERT INTO tblExpSizingReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "',1,'D08411', " & CInt(flxDetails.Item(1, intIndex).Value) & ", " & CDbl(flxDetails.Item(2, intIndex).Value) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0)")

                                'Sizing Types
                                AdoCN.Execute("INSERT INTO tblExpSizingTypes(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts,OK,BasePrice,EstCts) " & _
                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "',1,'" & UCase(flxDetails.Item(0, intIndex).Value) & "'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",0," & CDbl(flxDetails.Item(3, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ")")
                            End If

                        End If
                        rsComSql = Nothing

                        AdoCN.Execute("UPDATE tblAssortExportDetails SET Status = 1 WHERE Assortment = '" & flxDetails.Item(0, intIndex).Value & "' AND Status = 0 AND Export = 4")
                        blnSave = True
                    End If
                End If
                'Polish Box
                If optPolBox.Checked = True Then
                    AdoCN.Execute("UPDATE tblPCUStockIn SET Status = 1 WHERE Assortment = '" & flxDetails.Item(0, intIndex).Value & "' AND ParNo = '" & flxDetails.Item(5, intIndex).Value & "'")
                End If
                'Reject Export
                If optReject.Checked = True Then
                    If flxDetails.Item(10, intIndex).Value = True Then
                        blnSave = True
                        AdoCN.Execute("INSERT INTO tblExpRejExports(Department,ParNo,OrgAssort,Pcs,Cts,PackNo) " & _
                                      "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(9, intIndex).Value & "','" & flxDetails.Item(7, intIndex).Value & "'," & CDbl(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & "," & CDbl(txtPackNo.Text) & ")")

                        AdoCN.Execute("UPDATE tblExpRghTypes SET OK = 1 WHERE ParNo = '" & flxDetails.Item(5, intIndex).Value & "' AND OK = 0 AND Type = 'E'")
                    End If
                End If
                'MIX Plan
                If optMixPlan.Checked = True Then
                    strSupParNo = flxDetails.Item(5, intIndex).Value
                    If Mid(flxDetails.Item(0, intIndex).Value, 1, 2) = "VM" Or Mid(flxDetails.Item(0, intIndex).Value, 1, 2) = "VP" Then
                        strOrgAssort = "VPCU"
                    Else
                        strOrgAssort = "APCU"
                    End If

                    AdoCN.Execute("INSERT INTO tblPCUStockIn(OrgAssort,Assortment,ParNo,InPcs,InCts,Price) " & _
                                  "VALUES('" & strOrgAssort & "','" & flxDetails.Item(0, intIndex).Value & "'," & _
                                        "'" & flxDetails.Item(5, intIndex).Value & "'," & Val(flxDetails.Item(1, intIndex).Value) & "," & _
                                        "" & CDbl(flxDetails.Item(2, intIndex).Value) & "," & CDbl(flxDetails.Item(3, intIndex).Value) & ")")

                    AdoCN.Execute("UPDATE tblAssortExportDetails SET Status = 1,ParNo = '" & flxDetails.Item(5, intIndex).Value & "' WHERE Assortment = '" & flxDetails.Item(0, intIndex).Value & "' AND Status = 0 AND Export = 6")

                    blnSave = True
                End If
            Next

            If blnSave = True Then
                MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If

            flxDetails.Rows.Clear()
            txtTotPcs.Text = "0"
            txtTotCts.Text = "0"
            txtPrice.Text = "0"
            txtValue.Text = "0"
            txtSupParNo.Text = ""
            chkExport.Checked = False
            chkOrder.Checked = False
        End If
    End Sub

    Private Sub Save()
        Dim strSupParNo As String
        Dim intIndex As Integer
        Dim strOrgAssort As String
        Dim blnSave As Boolean
        Dim strPktNo As String
        Dim intSec As Integer

        If Len(txtTotPcs.Text) = 0 Then Exit Sub
        If CInt(txtTotPcs.Text) <= 0 Then Exit Sub

        blnSave = False
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse  = MsgBoxResult.Yes Then
            For intIndex = 0 To flxDetails.Rows.Count - 1
                If optMix.Checked = True Then
                    strSupParNo = flxDetails.Item(5, intIndex).Value
                    If Mid(flxDetails.Item(0, intIndex).Value, 1, 2) = "VM" Or Mid(flxDetails.Item(0, intIndex).Value, 1, 2) = "VP" Then
                        strOrgAssort = "VPCU"
                    Else
                        strOrgAssort = "APCU"
                    End If

                    AdoCN.Execute("INSERT INTO tblPCUStockIn(OrgAssort,Assortment,ParNo,InPcs,InCts,Price) " & _
                                  "VALUES('" & strOrgAssort & "','" & flxDetails.Item(0, intIndex).Value & "'," & _
                                        "'" & flxDetails.Item(5, intIndex).Value & "'," & Val(flxDetails.Item(1, intIndex).Value) & "," & _
                                        "" & CDbl(flxDetails.Item(2, intIndex).Value) & "," & CDbl(flxDetails.Item(3, intIndex).Value) & ")")

                    AdoCN.Execute("UPDATE tblAssortExportDetails SET Status = 1,ParNo = '" & flxDetails.Item(5, intIndex).Value & "' WHERE Assortment = '" & flxDetails.Item(0, intIndex).Value & "' AND Status = 0 AND Export = 2")

                    blnSave = True
                Else
                    If optGrdPcu.Checked = True Then
                        strPktNo = "G001"
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT MAX(RIGHT(PktNo, 3)) AS MaxPktNo FROM tblExpPacket WHERE Department = '" & flxDetails.Item(6, intIndex).Value & "' AND ParNo = '" & flxDetails.Item(5, intIndex).Value & "' AND LEFT(PktNo, 1) = 'G'", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            If Not IsDBNull(rsComSql.Fields("MaxPktNo").Value) Then
                                strPktNo = "G" & Format(CInt(rsComSql.Fields("MaxPktNo").Value) + 1, "000")
                            End If
                        End If
                        rsComSql = Nothing

                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT * FROM tblExpPacket WHERE Department = '" & flxDetails.Item(6, intIndex).Value & "' AND ParNo = '" & flxDetails.Item(5, intIndex).Value & "' AND PktNo = '" & strPktNo & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount = 0 Then
                            AdoCN.Execute("INSERT INTO tblExpPacket(Department,ParNo,PktNo,PktPcs,PktCts,PktType,PktDate,AParNo) " & _
                                          "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",'','" & Format(Date.Now, "MM/dd/yyyy") & "','" & flxDetails.Item(0, intIndex).Value & "')")
                        End If
                        rsComSql = Nothing

                        For intSec = 1 To 3
                            rsComSql = New ADODB.Recordset
                            rsComSql.Open("SELECT * FROM tblExpIssues WHERE Department = '" & flxDetails.Item(6, intIndex).Value & "' AND ParNo = '" & flxDetails.Item(5, intIndex).Value & "' AND PktNo = '" & strPktNo & "' AND Sec = " & intSec & "", AdoCN, 1, 1)
                            If rsComSql.RecordCount = 0 Then
                                AdoCN.Execute("INSERT INTO tblExpIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "'," & intSec & ",'D08411'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & _
                                                     "" & CDbl(flxDetails.Item(2, intIndex).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")
                            End If
                            rsComSql = Nothing

                            If intSec < 3 Then
                                rsComSql = New ADODB.Recordset
                                rsComSql.Open("SELECT * FROM tblExpReturns WHERE Department = '" & flxDetails.Item(6, intIndex).Value & "' AND ParNo = '" & flxDetails.Item(5, intIndex).Value & "' AND PktNo = '" & strPktNo & "' AND Sec = " & intSec & "", AdoCN, 1, 1)
                                If rsComSql.RecordCount = 0 Then
                                    AdoCN.Execute("INSERT INTO tblExpReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts,ColPcs,ColCts,FloPcs,FloCts,IncPcs,IncCts) " & _
                                                  "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "'," & intSec & ",'D08411'," & _
                                                        "" & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,0,0,0,0)")
                                End If
                                rsComSql = Nothing

                                AdoCN.Execute("DELETE FROM tblExpReturnDetails WHERE Department = '" & flxDetails.Item(6, intIndex).Value & "' AND ParNo = '" & flxDetails.Item(5, intIndex).Value & "' AND PktNo = '" & strPktNo & "' AND Sec = " & intSec & "")
                                AdoCN.Execute("INSERT INTO tblExpReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "'," & intSec & ",'MIX'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ")")
                            End If
                        Next

                        AdoCN.Execute("UPDATE tblGrading_RghIssues SET OK = 1 WHERE ParNo = '" & flxDetails.Item(5, intIndex).Value & "' AND Assortment = '" & flxDetails.Item(0, intIndex).Value & "' AND OK = 0 AND Type = 'P'")

                        blnSave = True

                    Else
                        If optPcu.Checked = True Then
                            If flxDetails.Item(10, intIndex).Value = True Then
                                If chkExport.Checked = False Then
                                    If flxDetails.Item(6, intIndex).Value <> "GradingMix" And flxDetails.Item(6, intIndex).Value <> "PcuMix" Then
                                        If chkOrder.Checked = True Or chkKOrder.Checked = True Then
                                            If chkOrder.Checked = True Then
                                                AdoCN.Execute("INSERT INTO tblPCUStockIn(OrgAssort,Assortment,ParNo,InPcs,InCts,Price,Status) " & _
                                                              "VALUES('" & flxDetails.Item(7, intIndex).Value & "','" & flxDetails.Item(0, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "'," & _
                                                                "" & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",0,1)")
                                            Else
                                                AdoCN.Execute("INSERT INTO tblPCUStockIn(OrgAssort,Assortment,ParNo,InPcs,InCts,Price,Status) " & _
                                                              "VALUES('" & flxDetails.Item(7, intIndex).Value & "','" & flxDetails.Item(0, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "'," & _
                                                                "" & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",0,1)")
                                            End If
                                        Else
                                            AdoCN.Execute("INSERT INTO tblDep_Rec(Department,AssortmentNo,SupParcelNo,DCLParcelNo,Pcs,Cts,RghCts) " & _
                                                          "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(7, intIndex).Value & "','" & flxDetails.Item(8, intIndex).Value & "'," & _
                                                            "'" & flxDetails.Item(9, intIndex).Value & "'," & CDbl(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ")")
                                        End If
                                    Else
                                        AdoCN.Execute("INSERT INTO tblPCUStockIn(OrgAssort,Assortment,ParNo,InPcs,InCts,Price,Status) " & _
                                                      "VALUES('" & flxDetails.Item(7, intIndex).Value & "','" & flxDetails.Item(0, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "'," & _
                                                        "" & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",0,1)")
                                    End If
                                Else
                                    AdoCN.Execute("INSERT INTO tblExpRejExports(Department,ParNo,OrgAssort,Pcs,Cts,PackNo) " & _
                                                  "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(9, intIndex).Value & "','" & flxDetails.Item(7, intIndex).Value & "'," & CDbl(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & "," & CDbl(txtPackNo.Text) & ")")
                                End If
                                AdoCN.Execute("UPDATE tblExpRghTypes SET OK = 1 WHERE ParNo = '" & flxDetails.Item(5, intIndex).Value & "' AND OK = 0 AND Type = 'R'")

                                blnSave = True
                            End If
                        Else
                            If optGrading.Checked = True Then
                                If flxDetails.Item(6, intIndex).Value <> "Direct Import" Then
                                    If chkOrder.Checked = True Then
                                        AdoCN.Execute("INSERT INTO tblPCUStockIn(OrgAssort,Assortment,ParNo,InPcs,InCts,Price,Status) " & _
                                                      "VALUES('" & flxDetails.Item(7, intIndex).Value & "','" & flxDetails.Item(0, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "'," & _
                                                            "" & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & "," & CDbl(flxDetails.Item(3, intIndex).Value) & ",1)")
                                    Else
                                        AdoCN.Execute("INSERT INTO tblDep_Rec(Department,AssortmentNo,SupParcelNo,DCLParcelNo,Pcs,Cts,RghCts) " & _
                                                      "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(7, intIndex).Value & "','" & flxDetails.Item(8, intIndex).Value & "'," & _
                                                        "'" & flxDetails.Item(9, intIndex).Value & "'," & CDbl(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ")")
                                    End If
                                Else
                                    AdoCN.Execute("INSERT INTO tblDep_Rec(Department,AssortmentNo,SupParcelNo,DCLParcelNo,Pcs,Cts,RghCts) " & _
                                                  "VALUES('Grading','" & flxDetails.Item(7, intIndex).Value & "','" & flxDetails.Item(8, intIndex).Value & "'," & _
                                                    "'" & flxDetails.Item(9, intIndex).Value & "'," & CDbl(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ")")
                                End If

                                AdoCN.Execute("UPDATE tblGrading_RghIssues SET OK = 1 WHERE ParNo = '" & flxDetails.Item(5, intIndex).Value & "' AND OK = 0 AND Type = 'R'")

                                blnSave = True

                            Else
                                If optSorting.Checked = True Then
                                    If flxDetails.Item(10, intIndex).Value = True Then
                                        strPktNo = "B001"
                                        rsComSql = New ADODB.Recordset
                                        rsComSql.Open("SELECT MAX(RIGHT(PktNo, 3)) AS MaxPktNo FROM tblExpPacket WHERE Department = '" & flxDetails.Item(6, intIndex).Value & "' AND ParNo = '" & flxDetails.Item(5, intIndex).Value & "' AND LEFT(PktNo, 1) = 'B'", AdoCN, 1, 1)
                                        If rsComSql.RecordCount Then
                                            If Not IsDBNull(rsComSql.Fields("MaxPktNo").Value) Then
                                                strPktNo = "B" & Format(CInt(rsComSql.Fields("MaxPktNo").Value) + 1, "000")
                                            End If
                                        End If
                                        rsComSql = Nothing

                                        rsComSql = New ADODB.Recordset
                                        rsComSql.Open("SELECT * FROM tblExpPacket WHERE Department = '" & flxDetails.Item(6, intIndex).Value & "' AND ParNo = '" & flxDetails.Item(5, intIndex).Value & "' AND PktNo = '" & strPktNo & "'", AdoCN, 1, 1)
                                        If rsComSql.RecordCount = 0 Then
                                            AdoCN.Execute("INSERT INTO tblExpPacket(Department,ParNo,PktNo,PktPcs,PktCts,PktType,PktDate,AParNo) " & _
                                                          "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",'','" & Format(Date.Now, "MM/dd/yyyy") & "','" & flxDetails.Item(0, intIndex).Value & "')")

                                            If chkSizing.Checked = True Then
                                                '====================================
                                                'Fluorescent Checking Issues
                                                AdoCN.Execute("INSERT INTO tblExpIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "',1,'D08411'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")

                                                'Fluorescent Checking Returns
                                                AdoCN.Execute("INSERT INTO tblExpReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "',1,'D08411'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0)")

                                                'Fluorescent Checking Return Details
                                                AdoCN.Execute("INSERT INTO tblExpReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "',1,'NONE'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ")")

                                                '====================================
                                                'Color Sorting Issues
                                                AdoCN.Execute("INSERT INTO tblExpIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "',2,'D08411'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")

                                                'Color Sorting Returns
                                                AdoCN.Execute("INSERT INTO tblExpReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "',2,'D08411'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0)")

                                                'Color Sorting Return Details
                                                AdoCN.Execute("INSERT INTO tblExpReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "',2,'COLOR'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ")")

                                                '====================================
                                                'Clarity Checking Issues
                                                AdoCN.Execute("INSERT INTO tblExpIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "',3,'D08411'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")

                                                'Clarity Checking Returns
                                                AdoCN.Execute("INSERT INTO tblExpReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "',3,'D08411'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0)")

                                                'Clarity Checking Return Details
                                                AdoCN.Execute("INSERT INTO tblExpReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "',3,'COLOR'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ")")

                                                '====================================
                                                'Sizing Packet
                                                AdoCN.Execute("INSERT INTO tblExpSizingPacket(Department, ParNo, PktNo, PktPcs, PktCts, ReturnType) " & _
                                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",'COLOR')")

                                                'Sizing Issues
                                                AdoCN.Execute("INSERT INTO tblExpSizingIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "',1,'D08411'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")

                                                'Sizing Returns
                                                AdoCN.Execute("INSERT INTO tblExpSizingReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "',1,'D08411', " & CInt(flxDetails.Item(1, intIndex).Value) & ", " & CDbl(flxDetails.Item(2, intIndex).Value) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0)")

                                                'Sizing Types
                                                AdoCN.Execute("INSERT INTO tblExpSizingTypes(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts,OK,BasePrice,EstCts) " & _
                                                              "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(5, intIndex).Value & "','" & strPktNo & "',1,'" & UCase(flxDetails.Item(0, intIndex).Value) & "'," & CInt(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ",0," & CDbl(flxDetails.Item(3, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & ")")
                                            End If

                                        End If
                                        rsComSql = Nothing

                                        AdoCN.Execute("UPDATE tblAssortExportDetails SET Status = 1 WHERE Assortment = '" & flxDetails.Item(0, intIndex).Value & "' AND Status = 0 AND Export = 4")
                                        blnSave = True
                                    End If
                                Else
                                    If optPolBox.Checked = True Then
                                        AdoCN.Execute("UPDATE tblPCUStockIn SET Status = 1 WHERE Assortment = '" & flxDetails.Item(0, intIndex).Value & "' AND ParNo = '" & flxDetails.Item(5, intIndex).Value & "'")
                                    Else
                                        If optReject.Checked = True Then
                                            blnSave = True
                                            AdoCN.Execute("INSERT INTO tblExpRejExports(Department,ParNo,OrgAssort,Pcs,Cts,PackNo) " & _
                                                          "VALUES('" & flxDetails.Item(6, intIndex).Value & "','" & flxDetails.Item(9, intIndex).Value & "','" & flxDetails.Item(7, intIndex).Value & "'," & CDbl(flxDetails.Item(1, intIndex).Value) & "," & CDbl(flxDetails.Item(2, intIndex).Value) & "," & CDbl(txtPackNo.Text) & ")")

                                            AdoCN.Execute("UPDATE tblExpRghTypes SET OK = 1 WHERE ParNo = '" & flxDetails.Item(5, intIndex).Value & "' AND OK = 0 AND Type = 'E'")
                                        Else
                                            If optMixPlan.Checked = True Then
                                                strSupParNo = flxDetails.Item(5, intIndex).Value
                                                If Mid(flxDetails.Item(0, intIndex).Value, 1, 2) = "VM" Or Mid(flxDetails.Item(0, intIndex).Value, 1, 2) = "VP" Then
                                                    strOrgAssort = "VPCU"
                                                Else
                                                    strOrgAssort = "APCU"
                                                End If

                                                AdoCN.Execute("INSERT INTO tblPCUStockIn(OrgAssort,Assortment,ParNo,InPcs,InCts,Price) " & _
                                                              "VALUES('" & strOrgAssort & "','" & flxDetails.Item(0, intIndex).Value & "'," & _
                                                                    "'" & flxDetails.Item(5, intIndex).Value & "'," & Val(flxDetails.Item(1, intIndex).Value) & "," & _
                                                                    "" & CDbl(flxDetails.Item(2, intIndex).Value) & "," & CDbl(flxDetails.Item(3, intIndex).Value) & ")")

                                                AdoCN.Execute("UPDATE tblAssortExportDetails SET Status = 1,ParNo = '" & flxDetails.Item(5, intIndex).Value & "' WHERE Assortment = '" & flxDetails.Item(0, intIndex).Value & "' AND Status = 0 AND Export = 6")

                                                blnSave = True
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            Next

            If blnSave = True Then
                MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If

            flxDetails.Rows.Clear()
            txtTotPcs.Text = "0"
            txtTotCts.Text = "0"
            txtPrice.Text = "0"
            txtValue.Text = "0"
            txtSupParNo.Text = ""
            chkExport.Checked = False
            chkOrder.Checked = False
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save2()
    End Sub

    Private Sub chkOrder_CheckedChanged(sender As Object) Handles chkOrder.CheckedChanged
        If chkOrder.Checked = True Then
            chkKOrder.Checked = False
        Else
            chkKOrder.Checked = True
        End If
    End Sub

    Private Sub chkKOrder_CheckedChanged(sender As Object) Handles chkKOrder.CheckedChanged
        If chkKOrder.Checked = True Then
            chkOrder.Checked = False
        Else
            chkOrder.Checked = True
        End If
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class