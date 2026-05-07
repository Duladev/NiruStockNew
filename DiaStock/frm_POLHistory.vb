
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_POLHistory
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
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblPOLStockIn.SupParNo, dbo.tblPOLStockIn.Assortment, dbo.tblPOLStockIn.Pcs, dbo.tblPOLStockIn.Cts, dbo.tblPOLStockIn.SysDateTime, dbo.tblPOLStockIn.CompCode, dbo.tblPOLStockIn.SizeRange, " & _
                        "dbo.tblImport.NewLotNo, dbo.tblPOLStockIn.Price  " & _
                        "FROM dbo.tblPOLStockIn INNER JOIN dbo.tblImport ON dbo.tblPOLStockIn.SupParNo = dbo.tblImport.SupParcelNo " & _
                        "WHERE        (dbo.tblPOLStockIn.Assortment = '" & txtAssortment.Text & "') AND (dbo.tblPOLStockIn.SysDateTime >= '01/01/2025') " & _
                        "ORDER BY dbo.tblPOLStockIn.SysDateTime DESC", AdoCN, 1, 1)

        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxAssort.Rows.Add(rsComSql.Fields("Pcs").Value,
                                   Format(Math.Round(rsComSql.Fields("Cts").Value, 3), "#0.000"),
                                   Format(CDate(rsComSql.Fields("SysDateTime").Value), "yyyy/MM/dd"),
                                   strType,
                                   rsComSql.Fields("Assortment").Value,
                                   rsComSql.Fields("Price").Value,
                                   rsComSql.Fields("NewLotNo").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

    End Sub

    Private Sub LoadPktIssues(ByVal strAssort As String)

        flxHistory.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * " & _
                      "FROM tblPOLStockOut WHERE Assortment = '" & strAssort & "' AND SysDateTime >= '01/01/2026' " & _
                      "ORDER BY SysDateTime DESC", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxHistory.Rows.Add(rsComSql.Fields("DocID").Value,
                                    rsComSql.Fields("DocID").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    Format(Math.Round(rsComSql.Fields("Cts").Value, 3), "#0.000"),
                                    Format(rsComSql.Fields("SysDateTime").Value, "yyyy/MM/dd"),
                                    Format(rsComSql.Fields("SysDateTime").Value, "HH:mm tt"),
                                    rsComSql.Fields("Type").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing


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
        txtInCts.Text = Format(CDbl(txtInCts.Text), "#0.000")

        txtPktPcs.Text = "0"
        For intRow = 0 To flxHistory.Rows.Count - 1
            txtPktPcs.Text = CDbl(txtPktPcs.Text) + CDbl(flxHistory.Item(2, intRow).Value)
        Next

        txtPktCts.Text = "0"
        For intRow = 0 To flxHistory.Rows.Count - 1
            txtPktCts.Text = CDbl(txtPktCts.Text) + CDbl(flxHistory.Item(3, intRow).Value)
        Next
        txtPktCts.Text = Format(CDbl(txtPktCts.Text), "#0.000")

        txtBalPcs.Text = CDbl(txtInPcs.Text) - CDbl(txtPktPcs.Text)
        txtBalCts.Text = Format(CDbl(txtInCts.Text) - CDbl(txtPktCts.Text), "#0.000")

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT AvgCost FROM tblDCLPermanents WHERE ItemName = '" & txtAssortment.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            txtPrice.Text = rsComSql.Fields("AvgCost").Value
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