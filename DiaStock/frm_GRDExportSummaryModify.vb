
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDExportSummaryModify

    Private Sub frm_GRDExportSummaryModify_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        txtPcs.Text = "0"
        txtCts.Text = "0"
        txtLotNo.Focus()
    End Sub

    Private Sub Load_PackingList()
        Dim intLamourNo As Integer

        flxDetails.Rows.Clear()
        flxPack.Rows.Clear()
        intLamourNo = LastLamourNo() + 1

        txtOrigin.Text = ""
        txtOCode.Text = ""
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT dbo.tblImportOGL.MiningCompany " & _
                        "FROM dbo.tblImport INNER JOIN dbo.tblImportOGL ON dbo.tblImport.NewLotNo = dbo.tblImportOGL.MasterLotID " & _
                        "WHERE (dbo.tblImport.LotNo = '" & txtLotNo.Text & "')", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            txtOrigin.Text = rsComSql_1.Fields("MiningCompany").Value
        End If
        rsComSql_1 = Nothing

        If txtOrigin.Text <> "" Then
            Select Case txtOrigin.Text
                Case "DTC"
                    txtOCode.Text = "ADT"
                Case "Rio Tinto"
                    txtOCode.Text = "ART"
                Case "Arctic Canadian Diamond Company Ltd."
                    txtOCode.Text = "AAC"
                Case "Debswana"
                    txtOCode.Text = "AOD"
                Case "Stargems Group"
                    txtOCode.Text = "ASG"
            End Select
        End If

        If optEdit.Checked = True Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblGrading_PackingListExport.LotNo, dbo.tblGrading_PackingListExport.Assortment, " & _
                                "dbo.tblGrading_PackingListExport.Pcs - ISNULL(dbo.VW_GradingPackingListM.Pcs, 0) AS Pcs, " & _
                                "dbo.tblGrading_PackingListExport.Cts - ISNULL(dbo.VW_GradingPackingListM.Cts, 0) AS Cts, dbo.tblGrading_PackingListExport.Price, " & _
                                "dbo.tblGrading_PackingListExport.Stone, dbo.tblGrading_PackingListExport.SizeRange " & _
                          "FROM dbo.tblGrading_PackingListExport LEFT OUTER JOIN dbo.VW_GradingPackingListM ON dbo.tblGrading_PackingListExport.Stone = dbo.VW_GradingPackingListM.Stone AND " & _
                                "dbo.tblGrading_PackingListExport.Assortment = dbo.VW_GradingPackingListM.Assortment AND " & _
                                "dbo.tblGrading_PackingListExport.LotNo = dbo.VW_GradingPackingListM.LotNo AND dbo.tblGrading_PackingListExport.SizeRange = dbo.VW_GradingPackingListM.SizeRange " & _
                          "WHERE (dbo.tblGrading_PackingListExport.LotNo = '" & txtLotNo.Text & "') AND (dbo.tblGrading_PackingListExport.Pcs - ISNULL(dbo.VW_GradingPackingListM.Pcs, 0) > 0) " & _
                          "ORDER BY dbo.tblGrading_PackingListExport.Assortment, dbo.tblGrading_PackingListExport.SizeRange", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    flxDetails.Rows.Add(rsComSql.Fields("Assortment").Value,
                                        rsComSql.Fields("LotNo").Value,
                                        rsComSql.Fields("Pcs").Value,
                                        Format(rsComSql.Fields("Cts").Value, "#0.000"),
                                        Format(rsComSql.Fields("Price").Value, "#0.00"),
                                        Format(rsComSql.Fields("Cts").Value * rsComSql.Fields("Price").Value, "#0.00"),
                                        True,
                                        rsComSql.Fields("Stone").Value,
                                        rsComSql.Fields("SizeRange").Value)

                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing

        ElseIf optNew.Checked = True Then
            'Not Lamour/Emerald
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.tblImport.LotNo, dbo.tblGrading_PackingList.Assortment, SUM(dbo.tblGrading_PackingList.ActPcs) AS Pcs, " & _
                                "ROUND(SUM(dbo.tblGrading_PackingList.ActCts), 3) AS Cts, dbo.tblGrading_SizingList.PRICE, " & _
                                "ROUND(SUM(dbo.tblGrading_PackingList.ActCts * dbo.tblGrading_SizingList.Price), 2) As Value, dbo.tblGrading_PackingList.SizeRange " & _
                            "FROM dbo.tblGrading_PackingList INNER JOIN dbo.tblParcel ON dbo.tblGrading_PackingList.Department = dbo.tblParcel.Depart AND dbo.tblGrading_PackingList.ParNo = dbo.tblParcel.GrpParNo INNER JOIN " & _
                                "dbo.tblImport ON dbo.tblParcel.OrigParcelNo = dbo.tblImport.SupParcelNo INNER JOIN " & _
                                "dbo.tblGrading_SizingList ON dbo.tblGrading_PackingList.Assortment = dbo.tblGrading_SizingList.NAME " & _
                            "WHERE (dbo.tblGrading_PackingList.Upgrade = 0) AND (LEFT(dbo.tblGrading_PackingList.Assortment, 2) <> 'VC') AND (LEFT(dbo.tblGrading_PackingList.Assortment, 2) <> 'AE') " & _
                            "GROUP BY dbo.tblImport.LotNo, dbo.tblGrading_PackingList.Assortment, dbo.tblGrading_SizingList.PRICE, dbo.tblGrading_PackingList.SizeRange " & _
                            "HAVING (dbo.tblImport.LotNo = '" & txtLotNo.Text & "') " & _
                            "ORDER BY dbo.tblGrading_PackingList.Assortment, dbo.tblGrading_PackingList.SizeRange", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                rsComSql_1.MoveFirst()
                While Not rsComSql_1.EOF
                    flxDetails.Rows.Add(rsComSql_1.Fields("Assortment").Value,
                                        rsComSql_1.Fields("LotNo").Value,
                                        rsComSql_1.Fields("Pcs").Value,
                                        Format(rsComSql_1.Fields("Cts").Value, "#0.000"),
                                        Format(rsComSql_1.Fields("Price").Value, "#0.00"),
                                        Format(rsComSql_1.Fields("Value").Value, "#0.00"),
                                        True,
                                        "0",
                                        rsComSql_1.Fields("SizeRange").Value)

                    rsComSql_1.MoveNext()
                End While
            Else
                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT TOP (100) PERCENT dbo.tblGrading_PackingList.Assortment, SUM(dbo.tblGrading_PackingList.ActPcs) AS Pcs, ROUND(SUM(dbo.tblGrading_PackingList.ActCts), 3) AS Cts, " & _
                                    "dbo.tblGrading_SizingList.PRICE, ROUND(SUM(dbo.tblGrading_PackingList.ActCts * dbo.tblGrading_SizingList.PRICE), 2) AS Value,dbo.VW_RealDepTrfGrd.LotNo, dbo.tblGrading_PackingList.SizeRange " & _
                                "FROM dbo.tblGrading_PackingList INNER JOIN dbo.tblGrading_SizingList ON dbo.tblGrading_PackingList.Assortment = dbo.tblGrading_SizingList.NAME INNER JOIN " & _
                                    "dbo.VW_RealDepTrfGrd ON dbo.tblGrading_PackingList.ParNo = dbo.VW_RealDepTrfGrd.DCLParcelNo " & _
                                "WHERE (dbo.tblGrading_PackingList.Department = 'Direct Import') " & _
                                "GROUP BY dbo.tblGrading_PackingList.Assortment, dbo.tblGrading_SizingList.PRICE, dbo.VW_RealDepTrfGrd.LotNo, dbo.tblGrading_PackingList.SizeRange " & _
                                "HAVING (dbo.VW_RealDepTrfGrd.LotNo = '" & txtLotNo.Text & "') " & _
                                "ORDER BY dbo.tblGrading_PackingList.Assortment, dbo.tblGrading_PackingList.SizeRange", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    rsComSql_2.MoveFirst()
                    While Not rsComSql_2.EOF
                        flxDetails.Rows.Add(rsComSql_2.Fields("Assortment").Value,
                                            rsComSql_2.Fields("LotNo").Value,
                                            rsComSql_2.Fields("Pcs").Value,
                                            Format(rsComSql_2.Fields("Cts").Value, "#0.000"),
                                            Format(rsComSql_2.Fields("Price").Value, "#0.00"),
                                            Format(rsComSql_2.Fields("Value").Value, "#0.00"),
                                            True,
                                            "0",
                                            rsComSql_2.Fields("SizeRange").Value)

                        rsComSql_2.MoveNext()
                    End While
                End If
                rsComSql_2 = Nothing
            End If
            rsComSql_1 = Nothing

            'Lamour
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.tblImport.LotNo, dbo.tblGrading_PackingList.Assortment, dbo.tblGrading_PackingList.ActPcs AS Pcs, " & _
                                "ROUND(dbo.tblGrading_PackingList.ActCts, 3) AS Cts, dbo.tblGrading_SizingList.PRICE, " & _
                                "ROUND(dbo.tblGrading_PackingList.ActCts * dbo.tblGrading_SizingList.Price, 2) As Value, dbo.tblGrading_PackingList.SizeRange " & _
                            "FROM dbo.tblGrading_PackingList INNER JOIN dbo.tblParcel ON dbo.tblGrading_PackingList.Department = dbo.tblParcel.Depart AND dbo.tblGrading_PackingList.ParNo = dbo.tblParcel.GrpParNo INNER JOIN " & _
                                "dbo.tblImport ON dbo.tblParcel.OrigParcelNo = dbo.tblImport.SupParcelNo INNER JOIN " & _
                                "dbo.tblGrading_SizingList ON dbo.tblGrading_PackingList.Assortment = dbo.tblGrading_SizingList.NAME " & _
                            "WHERE (dbo.tblGrading_PackingList.Upgrade = 0) AND (LEFT(dbo.tblGrading_PackingList.Assortment, 2) = 'VC') AND " & _
                                "(dbo.tblImport.LotNo = '" & txtLotNo.Text & "') " & _
                            "ORDER BY dbo.tblGrading_PackingList.Assortment, dbo.tblGrading_PackingList.SizeRange", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                rsComSql_1.MoveFirst()
                While Not rsComSql_1.EOF
                    flxDetails.Rows.Add(rsComSql_1.Fields("Assortment").Value,
                                        rsComSql_1.Fields("LotNo").Value,
                                        rsComSql_1.Fields("Pcs").Value,
                                        Format(rsComSql_1.Fields("Cts").Value, "#0.000"),
                                        Format(rsComSql_1.Fields("Price").Value, "#0.00"),
                                        Format(rsComSql_1.Fields("Value").Value, "#0.00"),
                                        True,
                                        intLamourNo,
                                        rsComSql_1.Fields("SizeRange").Value)

                    intLamourNo = intLamourNo + 1
                    rsComSql_1.MoveNext()
                End While
            Else
                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT TOP (100) PERCENT dbo.tblGrading_PackingList.Assortment, dbo.tblGrading_PackingList.ActPcs AS Pcs, ROUND(dbo.tblGrading_PackingList.ActCts, 3) AS Cts, " & _
                                    "dbo.tblGrading_SizingList.PRICE, ROUND(dbo.tblGrading_PackingList.ActCts * dbo.tblGrading_SizingList.PRICE, 2) AS Value,dbo.VW_RealDepTrfGrd.LotNo, dbo.tblGrading_PackingList.SizeRange " & _
                                "FROM dbo.tblGrading_PackingList INNER JOIN dbo.tblGrading_SizingList ON dbo.tblGrading_PackingList.Assortment = dbo.tblGrading_SizingList.NAME INNER JOIN " & _
                                    "dbo.VW_RealDepTrfGrd ON dbo.tblGrading_PackingList.ParNo = dbo.VW_RealDepTrfGrd.DCLParcelNo " & _
                                "WHERE (dbo.tblGrading_PackingList.Department = 'Direct Import') AND (LEFT(dbo.tblGrading_PackingList.Assortment, 2) = 'VC') AND " & _
                                    "(dbo.VW_RealDepTrfGrd.LotNo = '" & txtLotNo.Text & "') " & _
                                "ORDER BY dbo.tblGrading_PackingList.Assortment, dbo.tblGrading_PackingList.SizeRange", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    rsComSql_2.MoveFirst()
                    While Not rsComSql_2.EOF
                        flxDetails.Rows.Add(rsComSql_2.Fields("Assortment").Value,
                                            rsComSql_2.Fields("LotNo").Value,
                                            rsComSql_2.Fields("Pcs").Value,
                                            Format(rsComSql_2.Fields("Cts").Value, "#0.000"),
                                            Format(rsComSql_2.Fields("Price").Value, "#0.00"),
                                            Format(rsComSql_2.Fields("Value").Value, "#0.00"),
                                            True,
                                            intLamourNo,
                                            rsComSql_2.Fields("SizeRange").Value)

                        intLamourNo = intLamourNo + 1
                        rsComSql_2.MoveNext()
                    End While
                End If
                rsComSql_2 = Nothing
            End If
            rsComSql_1 = Nothing
        End If

        'Emerald
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.tblImport.LotNo, dbo.tblGrading_PackingList.Assortment, SUM(dbo.tblGrading_PackingList.ActPcs) AS Pcs, " & _
                            "ROUND(SUM(dbo.tblGrading_PackingList.ActCts), 3) AS Cts, dbo.tblGrading_PackingList.PRICE, " & _
                            "ROUND(SUM(dbo.tblGrading_PackingList.ActCts * dbo.tblGrading_SizingList.Price), 2) As Value, dbo.tblGrading_PackingList.SizeRange " & _
                        "FROM dbo.tblGrading_PackingList INNER JOIN dbo.tblParcel ON dbo.tblGrading_PackingList.Department = dbo.tblParcel.Depart AND dbo.tblGrading_PackingList.ParNo = dbo.tblParcel.GrpParNo INNER JOIN " & _
                            "dbo.tblImport ON dbo.tblParcel.OrigParcelNo = dbo.tblImport.SupParcelNo INNER JOIN " & _
                            "dbo.tblGrading_SizingList ON dbo.tblGrading_PackingList.Assortment = dbo.tblGrading_SizingList.NAME " & _
                        "WHERE (dbo.tblGrading_PackingList.Upgrade = 0) AND (LEFT(dbo.tblGrading_PackingList.Assortment, 2) = 'AE') " & _
                        "GROUP BY dbo.tblImport.LotNo, dbo.tblGrading_PackingList.Assortment, dbo.tblGrading_PackingList.PRICE, dbo.tblGrading_PackingList.SizeRange " & _
                        "HAVING (dbo.tblImport.LotNo = '" & txtLotNo.Text & "') " & _
                        "ORDER BY dbo.tblGrading_PackingList.Assortment, dbo.tblGrading_PackingList.SizeRange", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                flxDetails.Rows.Add(rsComSql_1.Fields("Assortment").Value,
                                    rsComSql_1.Fields("LotNo").Value,
                                    rsComSql_1.Fields("Pcs").Value,
                                    Format(rsComSql_1.Fields("Cts").Value, "#0.000"),
                                    Format(rsComSql_1.Fields("Price").Value, "#0.00"),
                                    Format(rsComSql_1.Fields("Value").Value, "#0.00"),
                                    True,
                                    "0",
                                    rsComSql_1.Fields("SizeRange").Value)

                rsComSql_1.MoveNext()
            End While
        End If
        rsComSql_1 = Nothing

        txtLastLamourNo.Text = intLamourNo

        'Show Package Numbers
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblGrading_PackingList.PackNo " & _
                      "FROM dbo.tblGrading_PackingList INNER JOIN dbo.tblParcel ON dbo.tblGrading_PackingList.Department = dbo.tblParcel.Depart AND dbo.tblGrading_PackingList.ParNo = dbo.tblParcel.GrpParNo INNER JOIN " & _
                            "dbo.tblImport ON dbo.tblParcel.OrigParcelNo = dbo.tblImport.SupParcelNo " & _
                      "WHERE (dbo.tblImport.LotNo = '" & txtLotNo.Text & "') " & _
                      "GROUP BY dbo.tblGrading_PackingList.PackNo " & _
                      "ORDER BY dbo.tblGrading_PackingList.PackNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxPack.Rows.Add(rsComSql.Fields("PackNo").Value)

                rsComSql.MoveNext()
            End While
        Else
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.tblGrading_PackingList.PackNo " & _
                            "FROM dbo.tblGrading_PackingList INNER JOIN dbo.VW_RealDepTrfGrd ON dbo.tblGrading_PackingList.ParNo = dbo.VW_RealDepTrfGrd.DCLParcelNo " & _
                            "WHERE (dbo.tblGrading_PackingList.Department = 'Direct Import') AND (dbo.VW_RealDepTrfGrd.LotNo = 50197622) " & _
                            "GROUP BY dbo.tblGrading_PackingList.PackNo " & _
                            "ORDER BY dbo.tblGrading_PackingList.PackNo", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                rsComSql_1.MoveFirst()
                While Not rsComSql_1.EOF
                    flxPack.Rows.Add(rsComSql_1.Fields("PackNo").Value)

                    rsComSql_1.MoveNext()
                End While
            End If
            rsComSql_1 = Nothing
        End If
        rsComSql = Nothing

        txtPcs.Text = CalTotalPcs(flxDetails)
        txtCts.Text = Format(CalTotalCts(flxDetails), "#0.000")

        txtTotPcs.Text = txtPcs.Text
        txtTotCts.Text = txtCts.Text
    End Sub

    Private Function LastLamourNo() As Integer
        LastLamourNo = 0
        rsComSql_4 = New ADODB.Recordset
        rsComSql_4.Open("SELECT MAX(Stone) AS MaxNo FROM tblGrading_PackingListExport WHERE LotNo = '" & Trim(txtLotNo.Text) & "'", AdoCN, 1, 1)
        If rsComSql_4.RecordCount Then
            If Not IsDBNull(rsComSql_4.Fields("MaxNo").Value) Then
                LastLamourNo = rsComSql_4.Fields("MaxNo").Value
            End If
        End If
        rsComSql_4 = Nothing
        Return LastLamourNo
    End Function

    Private Sub Load_Assortments()
        Me.Cursor = Cursors.WaitCursor
        cmbAssort.Items.Clear()
        rsComSql_4 = New ADODB.Recordset
        rsComSql_4.Open("SELECT * FROM tblGrading_SizingList WHERE OK = 1 AND NOT Name LIKE 'A%' AND NOT Name LIKE 'T%' ORDER BY NAME", AdoCN, 1, 1)
        If rsComSql_4.RecordCount Then
            rsComSql_4.MoveFirst()
            While Not rsComSql_4.EOF
                cmbAssort.Items.Add(rsComSql_4.Fields("Name").Value)
                rsComSql_4.MoveNext()
            End While
        End If
        rsComSql_4 = Nothing
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Load_SizeRange()

        cmbSize.Items.Clear()
        rsComSql_4 = New ADODB.Recordset
        rsComSql_4.Open("SELECT * FROM tblGrading_SizeListRange ORDER BY Size", AdoCN, 1, 1)
        If rsComSql_4.RecordCount Then
            rsComSql_4.MoveFirst()
            While Not rsComSql_4.EOF
                cmbSize.Items.Add(rsComSql_4.Fields("Size").Value)
                rsComSql_4.MoveNext()
            End While
        End If
        rsComSql_4 = Nothing
    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        Load_Assortments()
        Load_SizeRange()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub txtLotNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLotNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            Load_PackingList()
        End If
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(2, intRow).Value)
        Next
    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(3, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim intRow As Integer
        Dim dblPrice As Double
        Dim intStoneNo As Integer

        If cmbAssort.Text = "" Then MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbSize.Text = "" Then MsgBox("Invalid Size Range", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtNewPcs.Text = "" Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtNewCts.Text = "" Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtNewPcs.Text) <= 0 Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtNewCts.Text) <= 0 Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If CDbl(txtTotPcs.Text) + CDbl(txtNewPcs.Text) > CDbl(txtPcs.Text) Then MsgBox("Invalid Total Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If Math.Round(CDbl(txtTotCts.Text) + CDbl(txtNewCts.Text), 3) > CDbl(txtCts.Text) Then MsgBox("Invalid Total Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        For intRow = 0 To flxDetails.Rows.Count - 1
            If cmbAssort.Text = flxDetails.Item(0, intRow).Value And cmbSize.Text = flxDetails.Item(8, intRow).Value Then
                MsgBox("Assortment already entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        dblPrice = 0
        rsComSql_4 = New ADODB.Recordset
        rsComSql_4.Open("SELECT PRICE, OK, Origin FROM tblGrading_SizingList WHERE NAME = '" & cmbAssort.Text & "'", AdoCN, 1, 1)
        If rsComSql_4.RecordCount Then
            If rsComSql_4.Fields("Origin").Value <> "" Then
                If txtOCode.Text <> Mid(cmbAssort.Text, 1, 3) Then
                    MsgBox("Invalid Assortment Origin", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
            If Mid(Trim(cmbAssort.Text), 1, 1) <> "U" And Mid(Trim(cmbAssort.Text), 1, 1) <> "A" And Mid(Trim(cmbAssort.Text), 1, 1) <> "V" Then
                If rsComSql_4.Fields("OK").Value = 0 Then
                    MsgBox("Assortment is Blocked", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                Else
                    dblPrice = rsComSql_4.Fields("PRICE").Value
                End If
            Else
                dblPrice = rsComSql_4.Fields("PRICE").Value
            End If
        Else
            MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql_4 = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblGrading_SizeListRange WHERE Size = '" & Trim(cmbSize.Text) & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Size Range", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        If Mid(cmbAssort.Text, 1, 2) = "VC" Then
            intStoneNo = CInt(txtLastLamourNo.Text)
            txtLastLamourNo.Text = CInt(txtLastLamourNo.Text) + 1
        Else
            intStoneNo = 0
        End If

        flxDetails.Rows.Add(UCase(cmbAssort.Text),
                            txtLotNo.Text,
                            txtNewPcs.Text,
                            txtNewCts.Text,
                            Format(dblPrice, "#0.00"),
                            Format(dblPrice * CDbl(txtNewCts.Text), "#0.00"),
                            True,
                            intStoneNo,
                            cmbSize.Text)

        txtTotPcs.Text = CalTotalPcs(flxDetails)
        txtTotCts.Text = CalTotalCts(flxDetails)

        cmbAssort.Text = ""
        cmbSize.Text = ""
        txtNewPcs.Text = ""
        txtNewCts.Text = ""

        cmbAssort.Focus()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        flxDetails.Rows.Clear()
        flxPack.Rows.Clear()
        txtLotNo.Text = ""
        txtPcs.Text = "0"
        txtCts.Text = "0"
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        txtLotNo.Focus()
    End Sub

    Private Sub UpdateListPrice()
        Dim intRow As Integer

        For intRow = 0 To flxDetails.Rows.Count - 1
            rsComSql_4 = New ADODB.Recordset
            rsComSql_4.Open("SELECT * FROM tblGrading_SizingList WHERE NAME = '" & flxDetails.Item(0, intRow).Value & "'", AdoCN, 1, 1)
            If rsComSql_4.RecordCount Then
                flxDetails.Item(4, intRow).Value = Format(rsComSql_4.Fields("Price").Value, "#0.00")
                flxDetails.Item(5, intRow).Value = Format(Math.Round(rsComSql_4.Fields("Price").Value * CDbl(flxDetails.Item(3, intRow).Value), 2), "#0.00")
            End If
            rsComSql_4 = Nothing
        Next

    End Sub

    Private Sub cmdProcess_Click(sender As Object, e As EventArgs) Handles cmdProcess.Click
        PBResponse = MsgBox("Are you sure to Update the List Price?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            UpdateListPrice()
        End If
    End Sub

    Private Sub cmbAssort_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbAssort.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbAssort.Text = UCase(cmbAssort.Text)
            cmbSize.Focus()
        End If
    End Sub

    Private Sub txtNewPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNewPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtNewCts.Focus()
        End If
    End Sub

    Private Sub txtNewCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNewCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtNewCts.Text)
        If Asc(e.KeyChar) = 13 Then
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub flxDetails_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellEndEdit
        txtTotPcs.Text = CalTotalPcs(flxDetails)
        txtTotCts.Text = CalTotalCts(flxDetails)
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim dblExpPcs As Double
        Dim dblExpCts As Double

        txtTotPcs.Text = CalTotalPcs(flxDetails)
        txtTotCts.Text = CalTotalCts(flxDetails)

        If txtLotNo.Text = "" Then MsgBox("Invalid Lot No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If flxDetails.Rows.Count < 1 Then MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtTotPcs.Text) <> CDbl(txtPcs.Text) Then MsgBox("Pcs not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtTotCts.Text) <> CDbl(txtCts.Text) Then MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        For intRow = 0 To flxDetails.Rows.Count - 1
            If CInt(flxDetails.Item(2, intRow).Value) > 0 And CDbl(flxDetails.Item(3, intRow).Value) <= 0 Then
                MsgBox("Invalid Cts - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If CInt(flxDetails.Item(2, intRow).Value) <= 0 And CDbl(flxDetails.Item(3, intRow).Value) > 0 Then
                MsgBox("Invalid Pcs - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblGrading_PackingListExport WHERE LotNo = '" & txtLotNo.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            PBResponse = MsgBox("Are you sure to Update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                If optEdit.Checked = True Then
                    'AdoCN.Execute("DELETE FROM tblGrading_PackingListExport WHERE LotNo = '" & txtLotNo.Text & "'")
                End If
                For intRow = 0 To flxDetails.Rows.Count - 1
                    If CInt(flxDetails.Item(2, intRow).Value) > 0 And CDbl(flxDetails.Item(3, intRow).Value) > 0 Then
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM tblGrading_PackingListExport WHERE LotNo = '" & txtLotNo.Text & "' AND Assortment = '" & flxDetails.Item(0, intRow).Value & "' AND SizeRange = '" & flxDetails.Item(8, intRow).Value & "' AND Stone = " & CInt(flxDetails.Item(7, intRow).Value) & "", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            AdoCN.Execute("INSERT INTO tblGrading_PackingListExport(Assortment,LotNo,Pcs,Cts,Price,Stone,Price2,SizeRange) " & _
                                          "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & CInt(flxDetails.Item(2, intRow).Value) & "," & _
                                                "" & CDbl(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & "," & CInt(flxDetails.Item(7, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",'" & flxDetails.Item(8, intRow).Value & "')")
                        Else
                            dblExpPcs = 0
                            dblExpCts = 0
                            rsComSql_2 = New ADODB.Recordset
                            rsComSql_2.Open("SELECT SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts FROM tblGrading_PackingListM WHERE LotNo = '" & txtLotNo.Text & "' AND Assortment = '" & flxDetails.Item(0, intRow).Value & "' AND SizeRange = '" & flxDetails.Item(8, intRow).Value & "' AND Stone = " & CInt(flxDetails.Item(7, intRow).Value) & "", AdoCN, 1, 1)
                            If rsComSql_2.RecordCount Then
                                If Not IsDBNull(rsComSql_2.Fields("Pcs").Value) Then
                                    dblExpPcs = rsComSql_2.Fields("Pcs").Value
                                    dblExpCts = rsComSql_2.Fields("Cts").Value
                                    dblExpCts = Math.Round(dblExpCts, 3)
                                End If
                            End If
                            rsComSql_2 = Nothing

                            AdoCN.Execute("UPDATE tblGrading_PackingListExport SET Pcs = " & dblExpPcs & " + " & CInt(flxDetails.Item(2, intRow).Value) & ", Cts = " & dblExpCts & " + " & Math.Round(CDbl(flxDetails.Item(3, intRow).Value), 3) & ",Price = " & CDbl(flxDetails.Item(4, intRow).Value) & ",Price2 = " & CDbl(flxDetails.Item(4, intRow).Value) & " " & _
                                          "WHERE LotNo = '" & txtLotNo.Text & "' AND Assortment = '" & flxDetails.Item(0, intRow).Value & "' AND SizeRange = '" & flxDetails.Item(8, intRow).Value & "' AND Stone = " & CInt(flxDetails.Item(7, intRow).Value) & "")
                        End If
                        rsComSql_1 = Nothing
                    Else
                        If optNew.Checked = True Then
                            AdoCN.Execute("DELETE FROM tblGrading_PackingListExport " & _
                                          "WHERE LotNo = '" & txtLotNo.Text & "' AND Assortment = '" & flxDetails.Item(0, intRow).Value & "' AND SizeRange = '" & flxDetails.Item(8, intRow).Value & "' AND Stone = " & CInt(flxDetails.Item(7, intRow).Value) & "")
                        Else
                            dblExpPcs = 0
                            dblExpCts = 0
                            rsComSql_2 = New ADODB.Recordset
                            rsComSql_2.Open("SELECT * FROM tblGrading_PackingListM WHERE LotNo = '" & txtLotNo.Text & "' AND Assortment = '" & flxDetails.Item(0, intRow).Value & "' AND SizeRange = '" & flxDetails.Item(8, intRow).Value & "' AND Stone = " & CInt(flxDetails.Item(7, intRow).Value) & "", AdoCN, 1, 1)
                            If rsComSql_2.RecordCount Then
                                dblExpPcs = rsComSql_2.Fields("Pcs").Value
                                dblExpCts = rsComSql_2.Fields("Cts").Value
                                dblExpCts = Math.Round(dblExpCts, 3)
                            End If
                            rsComSql_2 = Nothing

                            AdoCN.Execute("UPDATE tblGrading_PackingListExport SET Pcs = " & dblExpPcs & ",Cts = " & dblExpCts & ",Price = " & CDbl(flxDetails.Item(4, intRow).Value) & ",Price2 = " & CDbl(flxDetails.Item(4, intRow).Value) & " " & _
                                          "WHERE LotNo = '" & txtLotNo.Text & "' AND Assortment = '" & flxDetails.Item(0, intRow).Value & "' AND SizeRange = '" & flxDetails.Item(8, intRow).Value & "' AND Stone = " & CInt(flxDetails.Item(7, intRow).Value) & "")
                        End If
                    End If
                Next
                If optNew.Checked = True Then
                    For intRow = 0 To flxPack.Rows.Count - 1
                        AdoCN.Execute("UPDATE tblGrading_PackingList SET Upgrade = 1 " & _
                                      "WHERE PackNo = " & CDbl(flxPack.Item(0, intRow).Value) & "")
                    Next
                End If

                MsgBox("Updated Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            PBResponse = MsgBox("Are you sure to Save?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                For intRow = 0 To flxDetails.Rows.Count - 1
                    If CInt(flxDetails.Item(2, intRow).Value) > 0 And CDbl(flxDetails.Item(3, intRow).Value) > 0 Then
                        AdoCN.Execute("INSERT INTO tblGrading_PackingListExport(Assortment,LotNo,Pcs,Cts,Price,Stone,Price2,SizeRange) " & _
                                      "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & CInt(flxDetails.Item(2, intRow).Value) & "," & _
                                            "" & CDbl(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & "," & CInt(flxDetails.Item(7, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",'" & flxDetails.Item(8, intRow).Value & "')")

                    End If
                Next
                If optNew.Checked = True Then
                    For intRow = 0 To flxPack.Rows.Count - 1
                        AdoCN.Execute("UPDATE tblGrading_PackingList SET Upgrade = 1 " & _
                                      "WHERE PackNo = " & CDbl(flxPack.Item(0, intRow).Value) & "")
                    Next
                End If

                MsgBox("Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        End If
        rsComSql = Nothing

        flxDetails.Rows.Clear()
        flxPack.Rows.Clear()
        txtLotNo.Text = ""
        txtPcs.Text = "0"
        txtCts.Text = "0"
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        txtLotNo.Focus()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub optNew_CheckedChanged(sender As Object, e As EventArgs) Handles optNew.CheckedChanged
        flxDetails.Rows.Clear()
        flxPack.Rows.Clear()
    End Sub

    Private Sub optEdit_CheckedChanged(sender As Object, e As EventArgs) Handles optEdit.CheckedChanged
        flxDetails.Rows.Clear()
        flxPack.Rows.Clear()
    End Sub

    Private Sub cmbSize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSize.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtNewPcs.Focus()
        End If
    End Sub
End Class