
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixAssortHistory
    Dim strFolderPath As String

    Private Sub txtAssortment_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAssortment.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtInPcs.Text = ""
            txtInCts.Text = ""
            txtPktPcs.Text = ""
            txtPktCts.Text = ""
            txtBalPcs.Text = ""
            txtBalCts.Text = ""
            txtPrice.Text = ""
            txtProdPcs.Text = ""
            txtProdCts.Text = ""

            txtAssortment.Text = UCase(txtAssortment.Text)
            LoadAssortDetails(txtAssortment.Text)
            LoadPktIssues(txtAssortment.Text)
            CalculateBalance()
            LoadProdStock(txtAssortment.Text)

            txtAssortment.Focus()
        End If
    End Sub

    Private Sub LoadProdStock(ByVal strAssort As String)
        Dim dblTotPcs As Double
        Dim dblTotCts As Double

        dblTotPcs = 0
        dblTotCts = 0
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT BoxCts, ProdPcs, ProdCts, BankCts " & _
                      "FROM VW_MixPCUStock2020 " & _
                      "WHERE Assortment = '" & strAssort & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("BoxCts").Value) Then
                dblTotPcs = IIf(rsComSql.Fields("ProdPcs").Value > 0, rsComSql.Fields("ProdPcs").Value, 0)
                dblTotCts = Math.Round(IIf(rsComSql.Fields("ProdCts").Value > 0, rsComSql.Fields("ProdCts").Value, 0) + rsComSql.Fields("BankCts").Value, 3)

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT RetPcs, RetCts FROM VW_MixPktRejExpNewY WHERE Assortment = '" & strAssort & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    dblTotPcs = dblTotPcs + rsComSql_1.Fields("RetPcs").Value
                    dblTotCts = dblTotCts + rsComSql_1.Fields("RetCts").Value
                End If
                rsComSql_1 = Nothing

            End If
        End If
        rsComSql = Nothing

        txtProdPcs.Text = dblTotPcs
        txtProdCts.Text = dblTotCts
    End Sub

    Private Sub LoadAssortDetails(ByVal strAssort As String)
        Dim strType As String

        flxAssort.Rows.Clear()
        strType = ""
        rsComSql = New ADODB.Recordset
        'rsComSql.Open("SELECT OrgAssort, CONVERT(DATETIME,CONVERT(VARCHAR(10), SystemDateTime, 101)) AS SysDate,Type,SUM(InPcs) AS InPcs,SUM(InCts) AS InCts, BaseCost " & _
        '              "FROM tblAssortDetails WHERE Assortment = '" & strAssort & "' " & _
        '              "GROUP BY OrgAssort, CONVERT(VARCHAR(10), SystemDateTime, 101), Type, BaseCost " & _
        '              "ORDER BY SysDate DESC", AdoCN, 1, 1)

        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblAssortDetails.OrgAssort, CONVERT(DATETIME, CONVERT(VARCHAR(10), dbo.tblAssortDetails.SystemDateTime, 101)) AS SysDate, dbo.tblAssortDetails.Type, " & _
                        "SUM(dbo.tblAssortDetails.InPcs) AS InPcs, SUM(dbo.tblAssortDetails.InCts) AS InCts, dbo.tblAssortDetails.BaseCost, dbo.tblAssortDetails.AssortBox, dbo.tblImport.LotNo, ISNULL(dbo.tblImport.TraceID, '') AS TraceID " & _
                      "FROM dbo.tblAssortDetails LEFT OUTER JOIN dbo.tblImport ON dbo.tblAssortDetails.AssortBox = dbo.tblImport.SupParcelNo " & _
                      "WHERE (dbo.tblAssortDetails.Assortment = '" & strAssort & "') " & _
                      "GROUP BY dbo.tblAssortDetails.OrgAssort, CONVERT(VARCHAR(10), dbo.tblAssortDetails.SystemDateTime, 101), dbo.tblAssortDetails.Type, dbo.tblAssortDetails.BaseCost, dbo.tblAssortDetails.AssortBox, dbo.tblImport.LotNo, " & _
                         "dbo.tblImport.TraceID " & _
                      "ORDER BY SysDate DESC", AdoCN, 1, 1)

        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                Select Case rsComSql.Fields("Type").Value
                    Case "A"
                        strType = "Import"
                    Case "R"
                        strType = "Reject"
                    Case "T"
                        strType = "Transfer"
                    Case "M"
                        strType = "Re-Transfer"
                    Case "C"
                        strType = "Modify"
                    Case "K"
                        strType = "Auto Packet"
                End Select

                flxAssort.Rows.Add(rsComSql.Fields("InPcs").Value,
                                   Format(Math.Round(rsComSql.Fields("InCts").Value, 2), "#0.00"),
                                   Format(CDate(rsComSql.Fields("SysDate").Value), "yyyy/MM/dd"),
                                   strType,
                                   rsComSql.Fields("OrgAssort").Value,
                                   rsComSql.Fields("BaseCost").Value,
                                   rsComSql.Fields("TraceID").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

    End Sub

    Private Sub LoadPktIssues(ByVal strAssort As String)

        flxHistory.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT ParNo,PktNo,Pcs,Cts,EntDate,Assortment,SysDateTime,Type " & _
                      "FROM tblMixPacketDetails WHERE Assortment = '" & strAssort & "' " & _
                      "ORDER BY SysDateTime DESC", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxHistory.Rows.Add(rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    Format(Math.Round(rsComSql.Fields("Cts").Value, 2), "#0.00"),
                                    Format(rsComSql.Fields("EntDate").Value, "yyyy/MM/dd"),
                                    Format(rsComSql.Fields("SysDateTime").Value, "HH:mm tt"),
                                    rsComSql.Fields("Type").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        'rsComSql = New ADODB.Recordset
        'rsComSql.Open("SELECT ParNo,PktNo,Pcs,Cts,EntDate,Assortment,SysDateTime " & _
        '              "FROM tblMixPacketDetails WHERE Assortment = '" & strAssort & "' AND Type = 'P' " & _
        '              "ORDER BY SysDateTime DESC", AdoCN, 1, 1)
        'If rsComSql.RecordCount Then
        '    rsComSql.MoveFirst()
        '    While Not rsComSql.EOF
        '        flxHistory.Rows.Add(rsComSql.Fields("ParNo").Value,
        '                            rsComSql.Fields("PktNo").Value,
        '                            rsComSql.Fields("Pcs").Value,
        '                            Format(Math.Round(rsComSql.Fields("Cts").Value, 2), "#0.00"),
        '                            Format(rsComSql.Fields("EntDate").Value, "yyyy/MM/dd"),
        '                            Format(rsComSql.Fields("SysDateTime").Value, "HH:mm tt"))
        '        rsComSql.MoveNext()
        '    End While
        'End If
        'rsComSql = Nothing

        'rsComSql = New ADODB.Recordset
        'rsComSql.Open("SELECT ExpNo,Pcs,Cts,EntDate,Assortment " & _
        '              "FROM tblAssortExports WHERE Assortment = '" & strAssort & "' " & _
        '              "ORDER BY EntDate DESC", AdoCN, 1, 1)
        'If rsComSql.RecordCount Then
        '    rsComSql.MoveFirst()
        '    While Not rsComSql.EOF
        '        flxHistory.Rows.Add(rsComSql.Fields("ExpNo").Value,
        '                            "000",
        '                            rsComSql.Fields("Pcs").Value,
        '                            Format(Math.Round(rsComSql.Fields("Cts").Value, 2), "#0.00"),
        '                            Format(rsComSql.Fields("EntDate").Value, "yyyy/MM/dd"),
        '                            Format(rsComSql.Fields("EntDate").Value, "HH:mm tt"))
        '        rsComSql.MoveNext()
        '    End While
        'End If
        'rsComSql = Nothing

        'rsComSql = New ADODB.Recordset
        'rsComSql.Open("SELECT ParNo,Pcs,Cts,EntDate,Assortment,SysDateTime " & _
        '              "FROM tblMixPacketDetails WHERE Assortment = '" & strAssort & "' AND Type = 'C' " & _
        '              "ORDER BY SysDateTime DESC", AdoCN, 1, 1)
        'If rsComSql.RecordCount Then
        '    rsComSql.MoveFirst()
        '    While Not rsComSql.EOF
        '        flxHistory.Rows.Add(rsComSql.Fields("ParNo").Value,
        '                            "000",
        '                            rsComSql.Fields("Pcs").Value,
        '                            Format(Math.Round(rsComSql.Fields("Cts").Value, 2), "#0.00"),
        '                            Format(rsComSql.Fields("EntDate").Value, "yyyy/MM/dd"),
        '                            Format(rsComSql.Fields("SysDateTime").Value, "HH:mm tt"))
        '        rsComSql.MoveNext()
        '    End While
        'End If
        'rsComSql = Nothing

        'rsComSql = New ADODB.Recordset
        'rsComSql.Open("SELECT ParNo,Pcs,Cts,EntDate,Assortment,SysDateTime " & _
        '              "FROM tblMixPacketDetails WHERE Assortment = '" & strAssort & "' AND Type = 'X' " & _
        '              "ORDER BY SysDateTime DESC", AdoCN, 1, 1)
        'If rsComSql.RecordCount Then
        '    rsComSql.MoveFirst()
        '    While Not rsComSql.EOF
        '        flxHistory.Rows.Add(rsComSql.Fields("ParNo").Value,
        '                            "000",
        '                            rsComSql.Fields("Pcs").Value,
        '                            Format(Math.Round(rsComSql.Fields("Cts").Value, 2), "#0.00"),
        '                            Format(rsComSql.Fields("EntDate").Value, "yyyy/MM/dd"),
        '                            Format(rsComSql.Fields("SysDateTime").Value, "HH:mm tt"))
        '        rsComSql.MoveNext()
        '    End While
        'End If
        'rsComSql = Nothing

    End Sub

    Private Sub CalculateBalance()
        Dim intRow As Integer

        txtInPcs.Text = "0"
        For intRow = 0 To flxAssort.Rows.Count - 1
            txtInPcs.Text = CDbl(txtInPcs.Text) + CDbl(flxAssort.Item(0, intRow).Value)
        Next

        txtInCts.Text = "0"
        For intRow = 0 To flxAssort.Rows.Count - 1
            txtInCts.Text = CDbl(txtInCts.Text) + CDbl(flxAssort.Item(1, intRow).Value)
        Next
        txtInCts.Text = Format(CDbl(txtInCts.Text), "#0.00")

        txtPktPcs.Text = "0"
        For intRow = 0 To flxHistory.Rows.Count - 1
            txtPktPcs.Text = CDbl(txtPktPcs.Text) + CDbl(flxHistory.Item(2, intRow).Value)
        Next

        txtPktCts.Text = "0"
        For intRow = 0 To flxHistory.Rows.Count - 1
            txtPktCts.Text = CDbl(txtPktCts.Text) + CDbl(flxHistory.Item(3, intRow).Value)
        Next
        txtPktCts.Text = Format(CDbl(txtPktCts.Text), "#0.00")

        txtBalPcs.Text = CDbl(txtInPcs.Text) - CDbl(txtPktPcs.Text)
        txtBalCts.Text = Format(CDbl(txtInCts.Text) - CDbl(txtPktCts.Text), "#0.00")

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT AvgCost, AvgStonePrice FROM tblAssortList WHERE Assortment = '" & txtAssortment.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Mid(txtAssortment.Text, 1, 1) = "S" Then
                txtPrice.Text = rsComSql.Fields("AvgStonePrice").Value
            Else
                txtPrice.Text = Math.Round((CDbl(txtBalCts.Text) * rsComSql.Fields("AvgCost").Value) / CDbl(txtBalPcs.Text), 2)
            End If
        End If
        rsComSql = Nothing

    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxHistory)
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub ClearFields()
        txtAssortment.Text = ""
        txtInPcs.Text = ""
        txtInCts.Text = ""
        txtPktPcs.Text = ""
        txtPktCts.Text = ""
        txtBalPcs.Text = ""
        txtBalCts.Text = ""
        txtPrice.Text = ""

        flxAssort.Rows.Clear()
        flxHistory.Rows.Clear()
        txtAssortment.Focus()
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixBoxSizeStock.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub frm_MixAssortHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        If strDBName = "DiaStock" Then
            strFolderPath = "Mix\"
        Else
            strFolderPath = "DiaSalesMix\"
        End If
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMIXAssortInPktPcs.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub
End Class