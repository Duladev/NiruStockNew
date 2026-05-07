Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text

Public Class frm_GRDRnd_Export

    ' ── FORM LOAD 
    Private Sub frm_Grading_Export_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'If GetUserRights(Me.Name) = False Then
        'MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
        'Me.Close()
        'Exit Sub
        'End If

        Me.StartPosition = FormStartPosition.CenterScreen
        SetupGrid()
    End Sub

    ' ── SETUP GRID 
    Private Sub SetupGrid()
        flxDetails.Columns.Clear()
        flxDetails.AutoGenerateColumns = False
        flxDetails.AllowUserToAddRows = False
        flxDetails.AllowUserToDeleteRows = False
        flxDetails.ReadOnly = True
        flxDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        flxDetails.BackgroundColor = System.Drawing.Color.White
        flxDetails.Font = New System.Drawing.Font("Tahoma", 8.25)
        flxDetails.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(224, 224, 224)

        Dim headers() As String = {"Lot No", "Assortment", "Pcs", "Cts", "Price", "Value", "Pack No", "Size Range", "Country", "Make"}
        Dim names() As String = {"LotNo", "Assortment", "Pcs", "Cts", "Price", "Value", "PackNo", "SizeRange", "Country", "Make"}
        Dim widths() As Integer = {100, 120, 60, 75, 70, 80, 70, 100, 70, 80}

        For idx As Integer = 0 To headers.Length - 1
            Dim col As New DataGridViewTextBoxColumn()
            col.HeaderText = headers(idx)
            col.Name = names(idx)
            col.Width = widths(idx)
            If idx >= 2 AndAlso idx <= 5 Then
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
            flxDetails.Columns.Add(col)
        Next
    End Sub

    '  PARCEL/LOT NO — ENTER KEY 
    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            e.Handled = True
            Get_PackingList()
        End If
    End Sub

    '  LOAD PACKING LIST DATA 
    Private Sub Get_PackingList()
        flxDetails.Rows.Clear()
        txtPcs.Text = ""
        txtCts.Text = ""
        txtValue.Text = ""

        Dim dblPcs As Double = 0
        Dim dblCts As Double = 0
        Dim dblValue As Double = 0

        Dim strSql As String

        If optParcel.Checked Then
            strSql = "SELECT TOP (100) PERCENT vp.ParcelID AS LotNo, st.ReturnType2 AS Assortment, " &
                     "st.ReturnType3 AS SizeRange, SUM(st.Pcs) AS Pcs, ROUND(SUM(st.Cts),3) AS Cts, " &
                     "ISNULL(sln.Price, sl.Price) AS Price " &
                     "FROM tblGrading_RndSizingTypes st " &
                     "INNER JOIN VW_GRNRealParcel vp ON st.ParNo = vp.ParcelNo " &
                     "LEFT JOIN tblGrading_RndSizeList sl ON st.ReturnType2 = sl.AssortNo " &
                     "LEFT JOIN tblGrading_RndSizeListNew sln ON st.ReturnType2 = sln.AssortNo " &
                     "WHERE vp.ParcelID = '" & Trim(txtParNo.Text) & "' " &
                     "GROUP BY vp.ParcelID, st.ReturnType2, st.ReturnType3, sln.Price, sl.Price " &
                     "ORDER BY vp.ParcelID, st.ReturnType2"
        Else
            strSql = "SELECT TOP (100) PERCENT LotNo, PackNo, Assortment, SizeRange, " &
                     "SUM(Pcs) AS Pcs, ROUND(SUM(Cts),3) AS Cts, Price, ROUND(SUM(Price*Cts),2) AS Value " &
                     "FROM tblGrading_RndPackingListM " &
                     "WHERE LotNo = '" & Trim(txtParNo.Text) & "' " &
                     "GROUP BY LotNo, PackNo, Assortment, Price, SizeRange " &
                     "ORDER BY Assortment"
        End If

        rsComSql = New ADODB.Recordset
        rsComSql.Open(strSql, AdoCN, 1, 1)

        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                Dim strAssortment As String = Trim(rsComSql.Fields("Assortment").Value)
                Dim strSizeRange As String = Trim(rsComSql.Fields("SizeRange").Value)
                Dim dblRowPcs As Double = CDbl(rsComSql.Fields("Pcs").Value)
                Dim dblRowCts As Double = Math.Round(CDbl(rsComSql.Fields("Cts").Value), 3)
                Dim dblPrice As Double = 0
                If Not IsDBNull(rsComSql.Fields("Price").Value) Then
                    dblPrice = CDbl(rsComSql.Fields("Price").Value)
                End If
                Dim dblRowValue As Double = Math.Round(dblRowCts * dblPrice, 2)

                Dim strPackNo As String = ""
                On Error Resume Next
                If Not IsDBNull(rsComSql.Fields("PackNo").Value) Then
                    strPackNo = Trim(rsComSql.Fields("PackNo").Value)
                End If
                On Error GoTo 0

                Dim strLotNo As String = Trim(rsComSql.Fields("LotNo").Value)

                '  Look up country 
                Dim strCountry As String = "SL"
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT Country FROM VW_GRNDCLPermanentsT WHERE ItemName = '" & strAssortment & "' AND SizeRange = '" & strSizeRange & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strCountry = Trim(rsComSql_1.Fields("Country").Value)
                End If
                rsComSql_1 = Nothing

                ' ── Look up make 
                Dim strMake As String = ""
                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT Make FROM tblGrading_RndSizeListNew WHERE AssortNo = '" & strAssortment & "'", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    strMake = Trim(rsComSql_2.Fields("Make").Value)
                End If
                rsComSql_2 = Nothing

                flxDetails.Rows.Add(
                    strLotNo,
                    strAssortment,
                    dblRowPcs.ToString(),
                    Format(dblRowCts, "#0.000"),
                    Format(dblPrice, "#0.00"),
                    Format(dblRowValue, "#0.00"),
                    strPackNo,
                    strSizeRange,
                    strCountry,
                    strMake)

                dblPcs += dblRowPcs
                dblCts += dblRowCts
                dblValue += dblRowValue

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtPcs.Text = dblPcs.ToString()
        txtCts.Text = Format(Math.Round(dblCts, 3), "#0.000")
        txtValue.Text = Format(Math.Round(dblValue, 2), "#0.00")
    End Sub

    ' ── EXPORT TO EXCEL 
    Private Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click
        Dim dlg As New SaveFileDialog()
        dlg.Filter = "Excel Files (*.xls)|*.xls"
        dlg.FileName = "PackingList_" & Trim(txtParNo.Text) & ".xls"
        If dlg.ShowDialog() = DialogResult.OK Then
            ExportToExcel(dlg.FileName)
        End If
    End Sub

    Private Sub ExportToExcel(ByVal filePath As String)
        Dim sb As New StringBuilder()

        For Each col As DataGridViewColumn In flxDetails.Columns
            sb.Append(col.HeaderText & vbTab)
        Next
        sb.AppendLine()

        For Each row As DataGridViewRow In flxDetails.Rows
            For Each cell As DataGridViewCell In row.Cells
                sb.Append(cell.Value?.ToString() & vbTab)
            Next
            sb.AppendLine()
        Next

        File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8)

        Try
            System.Diagnostics.Process.Start(
                New System.Diagnostics.ProcessStartInfo(filePath) With {
                    .UseShellExecute = True
                })
        Catch
        End Try

        MsgBox("Exported successfully to:" & vbCrLf & filePath,
               MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
    End Sub

    '  EXPORT TO CSV 
    Private Sub btnExportCSV_Click(sender As Object, e As EventArgs) Handles btnExportCSV.Click
        Dim dlg As New SaveFileDialog()
        dlg.Filter = "CSV Files (*.csv)|*.csv"
        dlg.FileName = "PackingList_" & Trim(txtParNo.Text) & ".csv"
        If dlg.ShowDialog() = DialogResult.OK Then
            ExportToCSV(dlg.FileName)
        End If
    End Sub

    Private Sub ExportToCSV(ByVal filePath As String)
        Dim sb As New StringBuilder()

        Dim headerCols As New List(Of String)
        For Each col As DataGridViewColumn In flxDetails.Columns
            headerCols.Add("""" & col.HeaderText & """")
        Next
        sb.AppendLine(String.Join(",", headerCols))

        For Each row As DataGridViewRow In flxDetails.Rows
            Dim rowCols As New List(Of String)
            For Each cell As DataGridViewCell In row.Cells
                rowCols.Add("""" & cell.Value?.ToString().Replace("""", """""") & """")
            Next
            sb.AppendLine(String.Join(",", rowCols))
        Next

        File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8)
        MsgBox("CSV exported successfully to:" & vbCrLf & filePath,
               MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
    End Sub

    '  EXIT 
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub lblTitle_Click(sender As Object, e As EventArgs) Handles lblTitle.Click

    End Sub
End Class