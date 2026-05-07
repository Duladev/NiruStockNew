
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLEmpProduction

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        txtEmpNo.Text = ""
        flxDetails.Rows.Clear()
        dtpFromDate.Value = dhFirstDayInMonth(Date.Now)
        dtpToDate.Value = Date.Now
        txtEmpNo.Focus()
    End Sub

    Private Sub frm_DCLEmpProduction_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        'If dbConnDiaShare.State = 1 Then
        '    dbConnDiaShare.Close()
        'End If
        'dbConnDiaShare.ConnectionString = "Provider=SQLOLEDB;Data Source='" & strServerName & "';Connect Timeout=60;Initial Catalog=DiaShare;Integrated Security=SSPI"
        'dbConnDiaShare.Open()

        dtpFromDate.Value = dhFirstDayInMonth(Date.Now)
        dtpToDate.Value = Date.Now
    End Sub

    Private Sub Load_Production()
        Dim strDept As String
        Dim dblRate As Double
        Dim dblUnits As Double
        Dim strUnit As String

        txtEmpNo.Text = UCase(txtEmpNo.Text)
        strUnit = ""

        strDept = "Baguettes"
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblBAGReturns.ParNo, dbo.tblBAGReturns.PktNo, dbo.tblBAGReturns.Sec, SUM(dbo.tblBAGReturns.RetPcsT + dbo.tblBAGReturns.RetPcsB) AS Pcs, " & _
                        "ROUND(SUM(dbo.tblBAGReturns.RetCts), 3) AS Cts, dbo.tblBAGPacket.IncUnit, SUM(dbo.tblBAGReturns.NopayPcs) AS NoPayPcs, dbo.tblBAGPacket.PktCts, dbo.tblBAGPacket.PktPcs, dbo.tblBAGPacket.FinCts " & _
                      "FROM dbo.tblBAGReturns INNER JOIN dbo.tblParcel ON dbo.tblBAGReturns.ParNo = dbo.tblParcel.GrpParNo INNER JOIN " & _
                        "dbo.tblBAGPacket ON dbo.tblBAGReturns.ParNo = dbo.tblBAGPacket.ParNo AND dbo.tblBAGReturns.PktNo = dbo.tblBAGPacket.PktNo " & _
                      "WHERE (dbo.tblParcel.Depart = 'Baguettes') AND (dbo.tblBAGReturns.EmpNo = '" & txtEmpNo.Text & "') AND (dbo.tblBAGReturns.RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (dbo.tblBAGReturns.RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') " & _
                      "GROUP BY dbo.tblBAGReturns.ParNo, dbo.tblBAGReturns.PktNo, dbo.tblBAGReturns.Sec, dbo.tblBAGPacket.IncUnit, dbo.tblBAGPacket.PktCts, dbo.tblBAGPacket.PktPcs, dbo.tblBAGPacket.FinCts " & _
                      "ORDER BY dbo.tblBAGReturns.ParNo, dbo.tblBAGReturns.PktNo, dbo.tblBAGReturns.Sec", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                dblRate = 0
                dblUnits = 0
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblBAGTgt WHERE SecName = '" & rsComSql.Fields("Sec").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strUnit = Trim(rsComSql.Fields("IncUnit").Value)
                    dblRate = rsComSql_1.Fields("Unit" & strUnit).Value
                    dblRate = Math.Round(dblRate, 2)
                End If
                rsComSql_1 = Nothing

                flxDetails.Rows.Add(txtEmpNo.Text,
                                    strDept,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("Sec").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    rsComSql.Fields("IncUnit").Value,
                                    rsComSql.Fields("NopayPcs").Value,
                                    rsComSql.Fields("Pktcts").Value,
                                    rsComSql.Fields("PktPcs").Value, "",
                                    rsComSql.Fields("FinCts").Value,
                                    Math.Round(dblRate * (rsComSql.Fields("Pcs").Value - rsComSql.Fields("NopayPcs").Value), 2))

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        strDept = "PCU Sorting"
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Sec, SUM(Pcs) AS Pcs, 0 AS Cts " & _
                      "FROM tblExpReturnsEmp " & _
                      "WHERE (EmpNo = '" & txtEmpNo.Text & "') AND (RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') " & _
                      "GROUP BY Sec " & _
                      "ORDER BY Sec", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(txtEmpNo.Text,
                                    strDept,
                                    "",
                                    "",
                                    rsComSql.Fields("Sec").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    "", "0", "0", "0", "", "0", "0")

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        strDept = "PCU Sorting Sizing"
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Sec, SUM(RetPcs) AS Pcs, ROUND(SUM(RetCts), 3) AS Cts " & _
                      "FROM tblExpSizingReturns " & _
                      "WHERE (EmpNo = '" & txtEmpNo.Text & "') AND (RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') " & _
                      "GROUP BY Sec " & _
                      "ORDER BY Sec", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(txtEmpNo.Text,
                                    strDept,
                                    "",
                                    "",
                                    rsComSql.Fields("Sec").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    "", "0", "0", "0", "", "0", "0")

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        strDept = "Grading Boiling"
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT 1 AS Sec, SUM(RetPcs) AS Pcs, ROUND(SUM(RetCts), 3) AS Cts " & _
                      "FROM tblGrading_BoilingReturns " & _
                      "WHERE (EmpNo = '" & txtEmpNo.Text & "') AND (RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "')", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(txtEmpNo.Text,
                                    strDept,
                                    "",
                                    "",
                                    rsComSql.Fields("Sec").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    "", "0", "0", "0", "", "0", "0")

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        strDept = "Grading Checking"
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Sec, SUM(ExPcs + VgPcs + BlPcs + ScPcs + PsPcs + SzPcs + OkPcs) AS Pcs, ROUND(SUM(ExCts + VgCts + BlCts + ScCts + PsCts + SzCts + OkCts), 3) AS Cts " & _
                      "FROM tblGrading_CheckingReturns " & _
                      "WHERE (EmpNo = '" & txtEmpNo.Text & "') AND (RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') " & _
                      "GROUP BY Sec " & _
                      "ORDER BY Sec", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(txtEmpNo.Text,
                                    strDept,
                                    "",
                                    "",
                                    rsComSql.Fields("Sec").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    "", "0", "0", "0", "", "0", "0")

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        strDept = "Grading Color"
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Sec, SUM(RetPcs) AS Pcs, ROUND(SUM(RetCts), 3) AS Cts " & _
                      "FROM tblGrading_Returns " & _
                      "WHERE (EmpNo = '" & txtEmpNo.Text & "') AND (RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') " & _
                      "GROUP BY Sec " & _
                      "ORDER BY Sec", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(txtEmpNo.Text,
                                    strDept,
                                    "",
                                    "",
                                    rsComSql.Fields("Sec").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    "", "0", "0", "0", "", "0", "0")

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        strDept = "Grading Sizing"
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Sec, SUM(RetPcs) AS Pcs, ROUND(SUM(RetCts), 3) AS Cts " & _
                      "FROM tblGrading_SizingReturns " & _
                      "WHERE (EmpNo = '" & txtEmpNo.Text & "') AND (RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') " & _
                      "GROUP BY Sec " & _
                      "ORDER BY Sec", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(txtEmpNo.Text,
                                    strDept,
                                    "",
                                    "",
                                    rsComSql.Fields("Sec").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    "", "0", "0", "0", "", "0", "0")

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        'strDept = "Sawing"
        'rsComSql = New ADODB.Recordset
        'rsComSql.Open("SELECT ParNo, Sec, SUM(RetPcsT) AS Pcs, ROUND(SUM(RetCts), 3) AS Cts " & _
        '              "FROM tblLZReturns " & _
        '              "WHERE (EmpNo = '" & txtEmpNo.Text & "') AND (RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') " & _
        '              "GROUP BY ParNo, Sec " & _
        '              "ORDER BY ParNo, Sec", AdoCN, 1, 1)
        'If rsComSql.RecordCount Then
        '    rsComSql.MoveFirst()
        '    While Not rsComSql.EOF
        '        flxDetails.Rows.Add(txtEmpNo.Text,
        '                            strDept,
        '                            rsComSql.Fields("PktNo").Value,
        '                            "",
        '                            rsComSql.Fields("Sec").Value,
        '                            rsComSql.Fields("Pcs").Value,
        '                            rsComSql.Fields("Cts").Value,
        '                            "", "0", "0", "0", "", "0", "0")

        '        rsComSql.MoveNext()
        '    End While
        'End If
        'rsComSql = Nothing

        strDept = "Mix"
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixReturns.ParNo, dbo.tblMixReturns.PktNo, dbo.tblMixReturns.Sec, SUM(dbo.tblMixReturns.RetPcsT + dbo.tblMixReturns.RetPcsB) AS Pcs, " & _
                        "ROUND(SUM(dbo.tblMixReturns.RetCts), 3) AS Cts, dbo.tblOrdersDtls.IncenCat, SUM(dbo.tblMixReturns.NopayPcs + dbo.tblMixReturns.NopayPcs1) AS NoPayPcs, dbo.tblMixPacket.PktCts, dbo.tblMixPacket.PktPcs " & _
                      "FROM dbo.tblMixReturns INNER JOIN dbo.tblMixPacket ON dbo.tblMixReturns.ParNo = dbo.tblMixPacket.PktOrdNo AND dbo.tblMixReturns.PktNo = dbo.tblMixPacket.PktNo INNER JOIN " & _
                        "dbo.tblOrdersDtls ON dbo.tblMixPacket.PktOrdNo = dbo.tblOrdersDtls.OrderNo AND dbo.tblMixPacket.PktRefNo = dbo.tblOrdersDtls.RefNo AND " & _
                        "dbo.tblMixPacket.Pktside = dbo.tblOrdersDtls.Side " & _
                      "WHERE (dbo.tblMixReturns.EmpNo = '" & txtEmpNo.Text & "') AND (dbo.tblMixReturns.RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (dbo.tblMixReturns.RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') " & _
                      "GROUP BY dbo.tblMixReturns.ParNo, dbo.tblMixReturns.PktNo, dbo.tblMixReturns.Sec, dbo.tblOrdersDtls.IncenCat, dbo.tblMixPacket.PktCts, dbo.tblMixPacket.PktPcs " & _
                      "ORDER BY dbo.tblMixReturns.ParNo, dbo.tblMixReturns.PktNo, dbo.tblMixReturns.Sec", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                dblRate = 0
                dblUnits = 0
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblPCUTgtUnitsNew WHERE SecCode = '" & rsComSql.Fields("Sec").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strUnit = Trim(rsComSql.Fields("IncenCat").Value)
                    dblRate = rsComSql_1.Fields("Unit" & strUnit).Value
                    dblRate = Math.Round(dblRate, 2)
                End If
                rsComSql_1 = Nothing

                flxDetails.Rows.Add(txtEmpNo.Text,
                                    strDept,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("Sec").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    rsComSql.Fields("IncenCat").Value,
                                    rsComSql.Fields("NopayPcs").Value,
                                    rsComSql.Fields("Pktcts").Value,
                                    rsComSql.Fields("PktPcs").Value, "",
                                    "0",
                                    Math.Round(dblRate * (rsComSql.Fields("Pcs").Value - rsComSql.Fields("NopayPcs").Value), 2))

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        'strDept = "MixShare"
        'rsComSql = New ADODB.Recordset
        'rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixReturns.ParNo, dbo.tblMixReturns.PktNo, dbo.tblMixReturns.Sec, SUM(dbo.tblMixReturns.RetPcsT + dbo.tblMixReturns.RetPcsB) AS Pcs, " & _
        '                "ROUND(SUM(dbo.tblMixReturns.RetCts), 3) AS Cts, dbo.tblOrdersDtls.IncenCat, SUM(dbo.tblMixReturns.NopayPcs + dbo.tblMixReturns.NopayPcs1) AS NoPayPcs, dbo.tblMixPacket.PktCts, dbo.tblMixPacket.PktPcs " & _
        '              "FROM dbo.tblMixReturns INNER JOIN dbo.tblMixPacket ON dbo.tblMixReturns.ParNo = dbo.tblMixPacket.PktOrdNo AND dbo.tblMixReturns.PktNo = dbo.tblMixPacket.PktNo INNER JOIN " & _
        '                "dbo.tblOrdersDtls ON dbo.tblMixPacket.PktOrdNo = dbo.tblOrdersDtls.OrderNo AND dbo.tblMixPacket.PktRefNo = dbo.tblOrdersDtls.RefNo AND " & _
        '                "dbo.tblMixPacket.Pktside = dbo.tblOrdersDtls.Side " & _
        '              "WHERE (dbo.tblMixReturns.EmpNo = '" & txtEmpNo.Text & "') AND (dbo.tblMixReturns.RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (dbo.tblMixReturns.RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') " & _
        '              "GROUP BY dbo.tblMixReturns.ParNo, dbo.tblMixReturns.PktNo, dbo.tblMixReturns.Sec, dbo.tblOrdersDtls.IncenCat, dbo.tblMixPacket.PktCts, dbo.tblMixPacket.PktPcs " & _
        '              "ORDER BY dbo.tblMixReturns.ParNo, dbo.tblMixReturns.PktNo, dbo.tblMixReturns.Sec", dbConnDiaShare, 1, 1)
        'If rsComSql.RecordCount Then
        '    rsComSql.MoveFirst()
        '    While Not rsComSql.EOF
        '        dblRate = 0
        '        dblUnits = 0
        '        rsComSql_1 = New ADODB.Recordset
        '        rsComSql_1.Open("SELECT * FROM tblPCUTgtUnitsNew WHERE SecCode = '" & rsComSql.Fields("Sec").Value & "'", AdoCN, 1, 1)
        '        If rsComSql_1.RecordCount Then
        '            strUnit = Trim(rsComSql.Fields("IncenCat").Value)
        '            dblRate = rsComSql_1.Fields("Unit" & strUnit).Value
        '            dblRate = Math.Round(dblRate, 2)
        '        End If
        '        rsComSql_1 = Nothing

        '        flxDetails.Rows.Add(txtEmpNo.Text,
        '                            strDept,
        '                            rsComSql.Fields("ParNo").Value,
        '                            rsComSql.Fields("PktNo").Value,
        '                            rsComSql.Fields("Sec").Value,
        '                            rsComSql.Fields("Pcs").Value,
        '                            rsComSql.Fields("Cts").Value,
        '                            rsComSql.Fields("IncenCat").Value,
        '                            rsComSql.Fields("NopayPcs").Value,
        '                            rsComSql.Fields("Pktcts").Value,
        '                            rsComSql.Fields("PktPcs").Value, "",
        '                            "0",
        '                            Math.Round(dblRate * (rsComSql.Fields("Pcs").Value - rsComSql.Fields("NopayPcs").Value), 2))

        '        rsComSql.MoveNext()
        '    End While
        'End If
        'rsComSql = Nothing

        'strDept = "Rounds2"
        'rsComSql = New ADODB.Recordset
        'rsComSql.Open("SELECT ParNo, Sec, SUM(RetPcsT + RetPcsB) AS Pcs, ROUND(SUM(RetCts), 3) AS Cts " & _
        '              "FROM tblNiruReturns " & _
        '              "WHERE (EmpNo = '" & txtEmpNo.Text & "') AND (RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') " & _
        '              "GROUP BY ParNo, Sec " & _
        '              "ORDER BY ParNo, Sec", AdoCN, 1, 1)
        'If rsComSql.RecordCount Then
        '    rsComSql.MoveFirst()
        '    While Not rsComSql.EOF
        '        flxDetails.Rows.Add(txtEmpNo.Text,
        '                            strDept,
        '                            rsComSql.Fields("PktNo").Value,
        '                            "",
        '                            rsComSql.Fields("Sec").Value,
        '                            rsComSql.Fields("Pcs").Value,
        '                            rsComSql.Fields("Cts").Value,
        '                            "", "0", "0", "0", "", "0", "0")

        '        rsComSql.MoveNext()
        '    End While
        'End If
        'rsComSql = Nothing

        strDept = "Princess"
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblPRReturns.ParNo, dbo.tblPRReturns.PktNo, dbo.tblPRReturns.Sec, SUM(dbo.tblPRReturns.RetPcsC + dbo.tblPRReturns.RetPcsP) AS Pcs, " & _
                          "ROUND(SUM(dbo.tblPRReturns.RetCtsC + dbo.tblPRReturns.RetCtsP), 3) AS Cts, dbo.tblPRPacket.IncUnit, SUM(dbo.tblPRReturns.NopayPcs) AS NoPayPcs, dbo.tblPRPacket.PktCts, dbo.tblPRPacket.PktPcs, dbo.tblPRPacket.FinCts " & _
                      "FROM dbo.tblPRReturns INNER JOIN dbo.tblParcel ON dbo.tblPRReturns.ParNo = dbo.tblParcel.GrpParNo INNER JOIN " & _
                          "dbo.tblPRPacket ON dbo.tblPRReturns.ParNo = dbo.tblPRPacket.ParNo AND dbo.tblPRReturns.PktNo = dbo.tblPRPacket.PktNo " & _
                      "WHERE (dbo.tblParcel.Depart = 'Princess') AND (dbo.tblPRReturns.EmpNo = '" & txtEmpNo.Text & "') AND (dbo.tblPRReturns.RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (dbo.tblPRReturns.RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') " & _
                      "GROUP BY dbo.tblPRReturns.ParNo, dbo.tblPRReturns.Sec, dbo.tblPRReturns.PktNo, dbo.tblPRPacket.IncUnit, dbo.tblPRPacket.PktCts, dbo.tblPRPacket.PktPcs, dbo.tblPRPacket.FinCts " & _
                      "ORDER BY dbo.tblPRReturns.ParNo, dbo.tblPRReturns.PktNo, dbo.tblPRReturns.Sec", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                dblRate = 0
                dblUnits = 0
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblPRUnits WHERE SecCode = '" & rsComSql.Fields("Sec").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strUnit = Trim(rsComSql.Fields("IncUnit").Value)
                    dblRate = rsComSql_1.Fields("Unit" & strUnit).Value
                    dblRate = Math.Round(dblRate, 2)
                End If
                rsComSql_1 = Nothing

                flxDetails.Rows.Add(txtEmpNo.Text,
                                    strDept,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("Sec").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    rsComSql.Fields("IncUnit").Value,
                                    rsComSql.Fields("NopayPcs").Value,
                                    rsComSql.Fields("Pktcts").Value,
                                    rsComSql.Fields("PktPcs").Value, "",
                                    rsComSql.Fields("FinCts").Value,
                                    Math.Round(dblRate * (rsComSql.Fields("Pcs").Value - rsComSql.Fields("NopayPcs").Value), 2))

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        strDept = "PCU"
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblReturns.ParNo, dbo.tblReturns.PktNo, dbo.tblReturns.Sec, SUM(dbo.tblReturns.RetPcsT + dbo.tblReturns.RetPcsB) AS Pcs, " & _
                          "ROUND(SUM(dbo.tblReturns.RetCts), 3) AS Cts, dbo.tblPacket.IncUnit2, SUM(dbo.tblReturns.NopayPcs) AS NoPayPcs, dbo.tblPacket.PktCts, dbo.tblPacket.PktPcs, dbo.tblPacket.AParNo " & _
                      "FROM dbo.tblReturns INNER JOIN dbo.tblPacket ON dbo.tblReturns.ParNo = dbo.tblPacket.PktOrdNo AND dbo.tblReturns.PktNo = dbo.tblPacket.PktNo INNER JOIN " & _
                          "dbo.tblNoneOrdersDtls ON dbo.tblPacket.PktOrdNo = dbo.tblNoneOrdersDtls.OrderNo AND dbo.tblPacket.PktRefNo = dbo.tblNoneOrdersDtls.RefNo AND " & _
                          "dbo.tblPacket.Pktside = dbo.tblNoneOrdersDtls.Side " & _
                      "WHERE (dbo.tblReturns.EmpNo = '" & txtEmpNo.Text & "') AND (dbo.tblReturns.RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (dbo.tblReturns.RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') " & _
                      "GROUP BY dbo.tblReturns.ParNo, dbo.tblReturns.PktNo, dbo.tblReturns.Sec, dbo.tblPacket.IncUnit2, dbo.tblPacket.PktCts, dbo.tblPacket.PktPcs, dbo.tblPacket.AParNo " & _
                      "ORDER BY dbo.tblReturns.ParNo, dbo.tblReturns.PktNo, dbo.tblReturns.Sec", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                dblRate = 0
                dblUnits = 0
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblPCUTgtUnits WHERE SecCode = '" & rsComSql.Fields("Sec").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strUnit = Trim(rsComSql.Fields("IncUnit2").Value)
                    dblRate = rsComSql_1.Fields("Unit" & strUnit).Value
                    dblRate = Math.Round(dblRate, 2)
                End If
                rsComSql_1 = Nothing

                flxDetails.Rows.Add(txtEmpNo.Text,
                                    strDept,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("Sec").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    rsComSql.Fields("IncUnit2").Value,
                                    rsComSql.Fields("NopayPcs").Value,
                                    rsComSql.Fields("Pktcts").Value,
                                    rsComSql.Fields("PktPcs").Value,
                                    rsComSql.Fields("AParNo").Value,
                                    "0",
                                    Math.Round(dblRate * (rsComSql.Fields("Pcs").Value - rsComSql.Fields("NopayPcs").Value), 2))

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        strDept = "Rough"
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRgfReturns.ParNo, dbo.tblRgfReturns.PktNo, dbo.tblRgfReturns.Sec, SUM(dbo.tblRgfReturns.RetPcsC + dbo.tblRgfReturns.RetPcsP) AS Pcs, " & _
                          "ROUND(SUM(dbo.tblRgfReturns.RetCtsC + dbo.tblRgfReturns.RetCtsP), 3) AS Cts, dbo.tblParcel.IncUnit " & _
                      "FROM dbo.tblRgfReturns INNER JOIN dbo.tblRgfPacket ON dbo.tblRgfReturns.ParNo = dbo.tblRgfPacket.ParNo AND dbo.tblRgfReturns.PktNo = dbo.tblRgfPacket.PktNo INNER JOIN " & _
                          "dbo.tblParcel ON dbo.tblRgfPacket.ParNo = dbo.tblParcel.GrpParNo " & _
                      "WHERE (dbo.tblRgfReturns.EmpNo = '" & txtEmpNo.Text & "') AND (dbo.tblRgfReturns.RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (dbo.tblRgfReturns.RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') " & _
                      "GROUP BY dbo.tblRgfReturns.ParNo, dbo.tblRgfReturns.PktNo, dbo.tblRgfReturns.Sec, dbo.tblParcel.Depart, dbo.tblParcel.IncUnit " & _
                      "HAVING (dbo.tblParcel.Depart = 'Rough Dept') " & _
                      "ORDER BY dbo.tblRgfReturns.ParNo, dbo.tblRgfReturns.PktNo, dbo.tblRgfReturns.Sec", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(txtEmpNo.Text,
                                    strDept,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("Sec").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    rsComSql.Fields("IncUnit").Value, "0", "0", "0", "", "0", "0")

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        strDept = "Rounds"
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRndReturns.ParNo, dbo.tblRndReturns.PktNo, dbo.tblRndReturns.Sec, SUM(dbo.tblRndReturns.RetPcsT + dbo.tblRndReturns.RetPcsB) AS Pcs, " & _
                        "ROUND(SUM(dbo.tblRndReturns.RetCts), 3) AS Cts, dbo.tblRndPacket.IncUnit, SUM(dbo.tblRndReturns.NopayPcs) AS NoPayPcs, dbo.tblRndPacket.PktCts, dbo.tblRndPacket.PktPcs, dbo.tblRndPacket.FinCts, dbo.VW_RndParcelInc.NewCat " & _
                      "FROM dbo.tblRndReturns INNER JOIN dbo.tblRndPacket ON dbo.tblRndReturns.ParNo = dbo.tblRndPacket.ParNo AND dbo.tblRndReturns.PktNo = dbo.tblRndPacket.PktNo INNER JOIN dbo.VW_RndParcelInc ON dbo.tblRndPacket.ParNo = dbo.VW_RndParcelInc.GrpParNo " & _
                      "WHERE (dbo.tblRndReturns.EmpNo = '" & txtEmpNo.Text & "') AND (dbo.tblRndReturns.RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (dbo.tblRndReturns.RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') " & _
                      "GROUP BY dbo.tblRndReturns.ParNo, dbo.tblRndReturns.PktNo, dbo.tblRndReturns.Sec, dbo.tblRndPacket.IncUnit, dbo.tblRndPacket.PktCts, dbo.tblRndPacket.PktPcs, dbo.tblRndPacket.FinCts, dbo.VW_RndParcelInc.NewCat " & _
                      "ORDER BY dbo.tblRndReturns.ParNo, dbo.tblRndReturns.PktNo, dbo.tblRndReturns.Sec", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                dblRate = 0
                dblUnits = 0
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblRndTgtUnits WHERE SecCode = '" & rsComSql.Fields("Sec").Value & "' AND Type = '" & rsComSql.Fields("NewCat").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strUnit = Trim(rsComSql.Fields("IncUnit").Value)
                    dblRate = rsComSql_1.Fields("UnitP" & strUnit).Value
                    dblRate = Math.Round(dblRate, 2)
                End If
                rsComSql_1 = Nothing

                flxDetails.Rows.Add(txtEmpNo.Text,
                                    strDept,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("Sec").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    rsComSql.Fields("IncUnit").Value,
                                    rsComSql.Fields("NopayPcs").Value,
                                    rsComSql.Fields("Pktcts").Value,
                                    rsComSql.Fields("PktPcs").Value, "",
                                    rsComSql.Fields("FinCts").Value,
                                    Math.Round(dblRate * (rsComSql.Fields("Pcs").Value - rsComSql.Fields("NopayPcs").Value), 2))
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        strDept = "RoundsNLE"
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblExtReturns.ParNo, dbo.tblExtReturns.PktNo, dbo.tblExtReturns.Sec, SUM(dbo.tblExtReturns.RetPcsT + dbo.tblExtReturns.RetPcsB) AS Pcs, " & _
                        "ROUND(SUM(dbo.tblExtReturns.RetCts), 3) AS Cts, dbo.tblExtPacket.IncUnit, SUM(dbo.tblExtReturns.NopayPcs) AS NoPayPcs, dbo.tblExtPacket.PktCts, dbo.tblExtPacket.PktPcs, dbo.tblExtPacket.FinCts " & _
                      "FROM dbo.tblExtReturns INNER JOIN dbo.tblExtPacket ON dbo.tblExtReturns.ParNo = dbo.tblExtPacket.ParNo AND dbo.tblExtReturns.PktNo = dbo.tblExtPacket.PktNo " & _
                      "WHERE (dbo.tblExtReturns.Department = '" & strDept & "') AND (dbo.tblExtReturns.EmpNo = '" & txtEmpNo.Text & "') AND (dbo.tblExtReturns.RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (dbo.tblExtReturns.RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') " & _
                      "GROUP BY dbo.tblExtReturns.ParNo, dbo.tblExtReturns.PktNo, dbo.tblExtReturns.Sec, dbo.tblExtPacket.IncUnit, dbo.tblExtPacket.PktCts, dbo.tblExtPacket.PktPcs, dbo.tblExtPacket.FinCts " & _
                      "ORDER BY dbo.tblExtReturns.ParNo, dbo.tblExtReturns.PktNo, dbo.tblExtReturns.Sec", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF

                flxDetails.Rows.Add(txtEmpNo.Text,
                                    strDept,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("Sec").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    rsComSql.Fields("IncUnit").Value,
                                    rsComSql.Fields("NopayPcs").Value,
                                    rsComSql.Fields("Pktcts").Value,
                                    rsComSql.Fields("PktPcs").Value, "",
                                    rsComSql.Fields("FinCts").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        strDept = "Emerald"
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblExtReturns.ParNo, dbo.tblExtReturns.PktNo, dbo.tblExtReturns.Sec, SUM(dbo.tblExtReturns.RetPcsT + dbo.tblExtReturns.RetPcsB) AS Pcs, " & _
                        "ROUND(SUM(dbo.tblExtReturns.RetCts), 3) AS Cts, dbo.tblExtPacket.IncUnit, SUM(dbo.tblExtReturns.NopayPcs) AS NoPayPcs, dbo.tblExtPacket.PktCts, dbo.tblExtPacket.PktPcs, dbo.tblExtPacket.FinCts " & _
                      "FROM dbo.tblExtReturns INNER JOIN dbo.tblExtPacket ON dbo.tblExtReturns.ParNo = dbo.tblExtPacket.ParNo AND dbo.tblExtReturns.PktNo = dbo.tblExtPacket.PktNo " & _
                      "WHERE (dbo.tblExtReturns.Department = '" & strDept & "') AND (dbo.tblExtReturns.EmpNo = '" & txtEmpNo.Text & "') AND (dbo.tblExtReturns.RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (dbo.tblExtReturns.RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') " & _
                      "GROUP BY dbo.tblExtReturns.ParNo, dbo.tblExtReturns.PktNo, dbo.tblExtReturns.Sec, dbo.tblExtPacket.IncUnit, dbo.tblExtPacket.PktCts, dbo.tblExtPacket.PktPcs, dbo.tblExtPacket.FinCts " & _
                      "ORDER BY dbo.tblExtReturns.ParNo, dbo.tblExtReturns.PktNo, dbo.tblExtReturns.Sec", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                dblRate = 0
                dblUnits = 0
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblExtTgtRate WHERE Sec = '" & rsComSql.Fields("Sec").Value & "' AND Department = 'Emerald' AND Unit = '" & Trim(rsComSql.Fields("IncUnit").Value) & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    dblRate = Math.Round(rsComSql_1.Fields("Rate").Value, 2)
                End If
                rsComSql_1 = Nothing

                flxDetails.Rows.Add(txtEmpNo.Text,
                                    strDept,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("Sec").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    rsComSql.Fields("IncUnit").Value,
                                    rsComSql.Fields("NopayPcs").Value,
                                    rsComSql.Fields("Pktcts").Value,
                                    rsComSql.Fields("PktPcs").Value, "",
                                    rsComSql.Fields("FinCts").Value,
                                    Math.Round(dblRate * (rsComSql.Fields("Pcs").Value - rsComSql.Fields("NopayPcs").Value), 2))

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        strDept = "Opening"
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblExtReturns.ParNo, dbo.tblExtReturns.PktNo, dbo.tblExtReturns.Sec, SUM(dbo.tblExtReturns.RetPcsT + dbo.tblExtReturns.RetPcsB) AS Pcs, " & _
                        "ROUND(SUM(dbo.tblExtReturns.RetCts), 3) AS Cts, dbo.tblExtPacket.IncUnit, SUM(dbo.tblExtReturns.NopayPcs) AS NoPayPcs, dbo.tblExtPacket.PktCts, dbo.tblExtPacket.PktPcs, dbo.tblExtPacket.FinCts " & _
                      "FROM dbo.tblExtReturns INNER JOIN dbo.tblExtPacket ON dbo.tblExtReturns.ParNo = dbo.tblExtPacket.ParNo AND dbo.tblExtReturns.PktNo = dbo.tblExtPacket.PktNo " & _
                      "WHERE (dbo.tblExtReturns.Department = '" & strDept & "') AND (dbo.tblExtReturns.EmpNo = '" & txtEmpNo.Text & "') AND (dbo.tblExtReturns.RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (dbo.tblExtReturns.RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') " & _
                      "GROUP BY dbo.tblExtReturns.ParNo, dbo.tblExtReturns.PktNo, dbo.tblExtReturns.Sec, dbo.tblExtPacket.IncUnit, dbo.tblExtPacket.PktCts, dbo.tblExtPacket.PktPcs, dbo.tblExtPacket.FinCts " & _
                      "ORDER BY dbo.tblExtReturns.ParNo, dbo.tblExtReturns.PktNo, dbo.tblExtReturns.Sec", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                dblRate = 0
                dblUnits = 0
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblExtTgtRate WHERE Sec = '" & rsComSql.Fields("Sec").Value & "' AND Department = 'Opening' AND Unit = '" & Trim(rsComSql.Fields("IncUnit").Value) & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    dblRate = Math.Round(rsComSql_1.Fields("Rate").Value, 2)
                End If
                rsComSql_1 = Nothing

                flxDetails.Rows.Add(txtEmpNo.Text,
                                    strDept,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("Sec").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    rsComSql.Fields("IncUnit").Value,
                                    rsComSql.Fields("NopayPcs").Value,
                                    rsComSql.Fields("Pktcts").Value,
                                    rsComSql.Fields("PktPcs").Value, "",
                                    rsComSql.Fields("FinCts").Value,
                                    Math.Round(dblRate * (rsComSql.Fields("Pcs").Value - rsComSql.Fields("NopayPcs").Value), 2))

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        strDept = "Lamour"
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblExtReturns.ParNo, dbo.tblExtReturns.PktNo, dbo.tblExtReturns.Sec, SUM(dbo.tblExtReturns.RetPcsT + dbo.tblExtReturns.RetPcsB) AS Pcs, " & _
                        "ROUND(SUM(dbo.tblExtReturns.RetCts), 3) AS Cts, dbo.tblExtPacket.IncUnit, SUM(dbo.tblExtReturns.NopayPcs) AS NoPayPcs, dbo.tblExtPacket.PktCts, dbo.tblExtPacket.PktPcs, dbo.tblExtPacket.FinCts " & _
                      "FROM dbo.tblExtReturns INNER JOIN dbo.tblExtPacket ON dbo.tblExtReturns.ParNo = dbo.tblExtPacket.ParNo AND dbo.tblExtReturns.PktNo = dbo.tblExtPacket.PktNo " & _
                      "WHERE (dbo.tblExtReturns.Department = '" & strDept & "') AND (dbo.tblExtReturns.EmpNo = '" & txtEmpNo.Text & "') AND (dbo.tblExtReturns.RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (dbo.tblExtReturns.RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') " & _
                      "GROUP BY dbo.tblExtReturns.ParNo, dbo.tblExtReturns.PktNo, dbo.tblExtReturns.Sec, dbo.tblExtPacket.IncUnit, dbo.tblExtPacket.PktCts, dbo.tblExtPacket.PktPcs, dbo.tblExtPacket.FinCts " & _
                      "ORDER BY dbo.tblExtReturns.ParNo, dbo.tblExtReturns.PktNo, dbo.tblExtReturns.Sec", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                dblRate = 0
                dblUnits = 0
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblExtTgtRate WHERE Sec = '" & rsComSql.Fields("Sec").Value & "' AND Department = 'Lamour' AND Unit = '" & Trim(rsComSql.Fields("IncUnit").Value) & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    dblRate = Math.Round(rsComSql_1.Fields("Rate").Value, 2)
                End If
                rsComSql_1 = Nothing

                flxDetails.Rows.Add(txtEmpNo.Text,
                                    strDept,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("Sec").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    rsComSql.Fields("IncUnit").Value,
                                    rsComSql.Fields("NopayPcs").Value,
                                    rsComSql.Fields("Pktcts").Value,
                                    rsComSql.Fields("PktPcs").Value, "",
                                    rsComSql.Fields("FinCts").Value,
                                    Math.Round(dblRate * (rsComSql.Fields("Pcs").Value - rsComSql.Fields("NopayPcs").Value), 2))

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        strDept = "Davinci"
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblExtReturns.ParNo, dbo.tblExtReturns.PktNo, dbo.tblExtReturns.Sec, SUM(dbo.tblExtReturns.RetPcsT + dbo.tblExtReturns.RetPcsB) AS Pcs, " & _
                        "ROUND(SUM(dbo.tblExtReturns.RetCts), 3) AS Cts, dbo.tblExtPacket.IncUnit, SUM(dbo.tblExtReturns.NopayPcs) AS NoPayPcs, dbo.tblExtPacket.PktCts, dbo.tblExtPacket.PktPcs, dbo.tblExtPacket.FinCts " & _
                      "FROM dbo.tblExtReturns INNER JOIN dbo.tblExtPacket ON dbo.tblExtReturns.ParNo = dbo.tblExtPacket.ParNo AND dbo.tblExtReturns.PktNo = dbo.tblExtPacket.PktNo " & _
                      "WHERE (dbo.tblExtReturns.Department = '" & strDept & "') AND (dbo.tblExtReturns.EmpNo = '" & txtEmpNo.Text & "') AND (dbo.tblExtReturns.RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (dbo.tblExtReturns.RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') " & _
                      "GROUP BY dbo.tblExtReturns.ParNo, dbo.tblExtReturns.PktNo, dbo.tblExtReturns.Sec, dbo.tblExtPacket.IncUnit, dbo.tblExtPacket.PktCts, dbo.tblExtPacket.PktPcs, dbo.tblExtPacket.FinCts " & _
                      "ORDER BY dbo.tblExtReturns.ParNo, dbo.tblExtReturns.PktNo, dbo.tblExtReturns.Sec", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                dblRate = 0
                dblUnits = 0
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblExtTgtRate WHERE Sec = '" & rsComSql.Fields("Sec").Value & "' AND Department = 'Davinci' AND Unit = '" & Trim(rsComSql.Fields("IncUnit").Value) & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    dblRate = Math.Round(rsComSql_1.Fields("Rate").Value, 2)
                End If
                rsComSql_1 = Nothing

                flxDetails.Rows.Add(txtEmpNo.Text,
                                    strDept,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("Sec").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    rsComSql.Fields("IncUnit").Value,
                                    rsComSql.Fields("NopayPcs").Value,
                                    rsComSql.Fields("Pktcts").Value,
                                    rsComSql.Fields("PktPcs").Value, "",
                                    rsComSql.Fields("FinCts").Value,
                                    Math.Round(dblRate * (rsComSql.Fields("Pcs").Value - rsComSql.Fields("NopayPcs").Value), 2))

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        strDept = "Carrer"
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblExtReturns.ParNo, dbo.tblExtReturns.PktNo, dbo.tblExtReturns.Sec, SUM(dbo.tblExtReturns.RetPcsT + dbo.tblExtReturns.RetPcsB) AS Pcs, " & _
                        "ROUND(SUM(dbo.tblExtReturns.RetCts), 3) AS Cts, dbo.tblExtPacket.IncUnit, SUM(dbo.tblExtReturns.NopayPcs) AS NoPayPcs, dbo.tblExtPacket.PktCts, dbo.tblExtPacket.PktPcs, dbo.tblExtPacket.FinCts " & _
                      "FROM dbo.tblExtReturns INNER JOIN dbo.tblExtPacket ON dbo.tblExtReturns.ParNo = dbo.tblExtPacket.ParNo AND dbo.tblExtReturns.PktNo = dbo.tblExtPacket.PktNo " & _
                      "WHERE (dbo.tblExtReturns.Department = '" & strDept & "') AND (dbo.tblExtReturns.EmpNo = '" & txtEmpNo.Text & "') AND (dbo.tblExtReturns.RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (dbo.tblExtReturns.RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') " & _
                      "GROUP BY dbo.tblExtReturns.ParNo, dbo.tblExtReturns.PktNo, dbo.tblExtReturns.Sec, dbo.tblExtPacket.IncUnit, dbo.tblExtPacket.PktCts, dbo.tblExtPacket.PktPcs, dbo.tblExtPacket.FinCts " & _
                      "ORDER BY dbo.tblExtReturns.ParNo, dbo.tblExtReturns.PktNo, dbo.tblExtReturns.Sec", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                dblRate = 0
                dblUnits = 0
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblExtTgtRate WHERE Sec = '" & rsComSql.Fields("Sec").Value & "' AND Department = 'Carrer' AND Unit = '" & Trim(rsComSql.Fields("IncUnit").Value) & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    dblRate = Math.Round(rsComSql_1.Fields("Rate").Value, 2)
                End If
                rsComSql_1 = Nothing

                flxDetails.Rows.Add(txtEmpNo.Text,
                                    strDept,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("Sec").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    rsComSql.Fields("IncUnit").Value,
                                    rsComSql.Fields("NopayPcs").Value,
                                    rsComSql.Fields("Pktcts").Value,
                                    rsComSql.Fields("PktPcs").Value, "",
                                    rsComSql.Fields("FinCts").Value,
                                    Math.Round(dblRate * (rsComSql.Fields("Pcs").Value - rsComSql.Fields("NopayPcs").Value), 2))

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        strDept = "Asscher"
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblExtReturns.ParNo, dbo.tblExtReturns.PktNo, dbo.tblExtReturns.Sec, SUM(dbo.tblExtReturns.RetPcsT + dbo.tblExtReturns.RetPcsB) AS Pcs, " & _
                        "ROUND(SUM(dbo.tblExtReturns.RetCts), 3) AS Cts, dbo.tblExtPacket.IncUnit, SUM(dbo.tblExtReturns.NopayPcs) AS NoPayPcs, dbo.tblExtPacket.PktCts, dbo.tblExtPacket.PktPcs, dbo.tblExtPacket.FinCts " & _
                      "FROM dbo.tblExtReturns INNER JOIN dbo.tblExtPacket ON dbo.tblExtReturns.ParNo = dbo.tblExtPacket.ParNo AND dbo.tblExtReturns.PktNo = dbo.tblExtPacket.PktNo " & _
                      "WHERE (dbo.tblExtReturns.Department = '" & strDept & "') AND (dbo.tblExtReturns.EmpNo = '" & txtEmpNo.Text & "') AND (dbo.tblExtReturns.RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (dbo.tblExtReturns.RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') " & _
                      "GROUP BY dbo.tblExtReturns.ParNo, dbo.tblExtReturns.PktNo, dbo.tblExtReturns.Sec, dbo.tblExtPacket.IncUnit, dbo.tblExtPacket.PktCts, dbo.tblExtPacket.PktPcs, dbo.tblExtPacket.FinCts " & _
                      "ORDER BY dbo.tblExtReturns.ParNo, dbo.tblExtReturns.PktNo, dbo.tblExtReturns.Sec", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                dblRate = 0
                dblUnits = 0
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblExtTgtRate WHERE Sec = '" & rsComSql.Fields("Sec").Value & "' AND Department = 'Asscher' AND Unit = '" & Trim(rsComSql.Fields("IncUnit").Value) & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    dblRate = Math.Round(rsComSql_1.Fields("Rate").Value, 2)
                End If
                rsComSql_1 = Nothing

                flxDetails.Rows.Add(txtEmpNo.Text,
                                    strDept,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("Sec").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    rsComSql.Fields("IncUnit").Value,
                                    rsComSql.Fields("NopayPcs").Value,
                                    rsComSql.Fields("Pktcts").Value,
                                    rsComSql.Fields("PktPcs").Value, "",
                                    rsComSql.Fields("FinCts").Value,
                                    Math.Round(dblRate * (rsComSql.Fields("Pcs").Value - rsComSql.Fields("NopayPcs").Value), 2))

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        strDept = "Radiant"
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblExtReturns.ParNo, dbo.tblExtReturns.PktNo, dbo.tblExtReturns.Sec, SUM(dbo.tblExtReturns.RetPcsT + dbo.tblExtReturns.RetPcsB) AS Pcs, " & _
                        "ROUND(SUM(dbo.tblExtReturns.RetCts), 3) AS Cts, dbo.tblExtPacket.IncUnit, SUM(dbo.tblExtReturns.NopayPcs) AS NoPayPcs, dbo.tblExtPacket.PktCts, dbo.tblExtPacket.PktPcs, dbo.tblExtPacket.FinCts " & _
                      "FROM dbo.tblExtReturns INNER JOIN dbo.tblExtPacket ON dbo.tblExtReturns.ParNo = dbo.tblExtPacket.ParNo AND dbo.tblExtReturns.PktNo = dbo.tblExtPacket.PktNo " & _
                      "WHERE (dbo.tblExtReturns.Department = '" & strDept & "') AND (dbo.tblExtReturns.EmpNo = '" & txtEmpNo.Text & "') AND (dbo.tblExtReturns.RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (dbo.tblExtReturns.RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') " & _
                      "GROUP BY dbo.tblExtReturns.ParNo, dbo.tblExtReturns.PktNo, dbo.tblExtReturns.Sec, dbo.tblExtPacket.IncUnit, dbo.tblExtPacket.PktCts, dbo.tblExtPacket.PktPcs, dbo.tblExtPacket.FinCts " & _
                      "ORDER BY dbo.tblExtReturns.ParNo, dbo.tblExtReturns.PktNo, dbo.tblExtReturns.Sec", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                dblRate = 0
                dblUnits = 0
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblExtTgtRate WHERE Sec = '" & rsComSql.Fields("Sec").Value & "' AND Department = 'Radiant' AND Unit = '" & Trim(rsComSql.Fields("IncUnit").Value) & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    dblRate = Math.Round(rsComSql_1.Fields("Rate").Value, 2)
                End If
                rsComSql_1 = Nothing

                flxDetails.Rows.Add(txtEmpNo.Text,
                                    strDept,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("Sec").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    rsComSql.Fields("IncUnit").Value,
                                    rsComSql.Fields("NopayPcs").Value,
                                    rsComSql.Fields("Pktcts").Value,
                                    rsComSql.Fields("PktPcs").Value, "",
                                    rsComSql.Fields("FinCts").Value,
                                    Math.Round(dblRate * (rsComSql.Fields("Pcs").Value - rsComSql.Fields("NopayPcs").Value), 2))

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        strDept = "RPR"
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblRPrReturns.ParNo, dbo.tblRPrReturns.PktNo, dbo.tblRPrReturns.Sec, SUM(dbo.tblRPrReturns.LabPcs) AS Pcs, " & _
                        "ROUND(SUM(dbo.tblRPrReturns.RetCts), 3) AS Cts, SUM(dbo.tblRPrReturns.NopayPcs) AS NoPayPcs, dbo.tblRPrReturns.Department, dbo.tblRPrPacket.PktCts, dbo.tblRPrPacket.PktPcs " & _
                      "FROM dbo.tblRPrReturns INNER JOIN dbo.tblRPrPacket ON dbo.tblRPrReturns.ParNo = dbo.tblRPrPacket.ParNo AND dbo.tblRPrReturns.PktNo = dbo.tblRPrPacket.PktNo AND " & _
                        "dbo.tblRPrReturns.Department = dbo.tblRPrPacket.Department " & _
                      "WHERE (dbo.tblRPrReturns.RetDate >= '" & Format(dtpFromDate.Value, "MM/dd/yyyy") & "') AND (dbo.tblRPrReturns.RetDate <= '" & Format(dtpToDate.Value, "MM/dd/yyyy") & "') AND (dbo.tblRPrReturns.EmpNo = '" & txtEmpNo.Text & "') " & _
                      "GROUP BY dbo.tblRPrReturns.ParNo, dbo.tblRPrReturns.PktNo, dbo.tblRPrReturns.Sec, dbo.tblRPrReturns.Department, dbo.tblRPrPacket.PktCts, dbo.tblRPrPacket.PktPcs " & _
                      "ORDER BY dbo.tblRPrReturns.ParNo, dbo.tblRPrReturns.PktNo, dbo.tblRPrReturns.Sec", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(txtEmpNo.Text,
                                    rsComSql.Fields("department").Value,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("Sec").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    "",
                                    rsComSql.Fields("NopayPcs").Value,
                                    rsComSql.Fields("Pktcts").Value,
                                    rsComSql.Fields("PktPcs").Value, "",
                                    "0")
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub txtEmpNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEmpNo.KeyPress
        Dim intRow As Integer

        If Asc(e.KeyChar) = 13 And Len(txtEmpNo.Text) = 6 Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(0, intRow).Value = txtEmpNo.Text Then
                    MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Next
            Load_Production()
            txtEmpNo.Text = ""
            txtEmpNo.Focus()
        End If
    End Sub
End Class