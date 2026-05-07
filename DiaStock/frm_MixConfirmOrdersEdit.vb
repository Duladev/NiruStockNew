
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixConfirmOrdersEdit
    Dim strFolderPath As String

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        flxDetails.Rows.Clear()
        txtPcs.Text = ""
        txtCts.Text = ""
        txtPack.Text = ""
    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        Load_PackigData()
    End Sub

    Private Sub Load_PackigData()
        If Len(txtPack.Text) = 0 Then
            MsgBox("Please Enter the Packing List No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        If cmbLineNo.Text <> "" Then
            mStrSQL = "SELECT * FROM tblMixExportOrders WHERE Status = 'E' AND PackingListNo = '" & txtPack.Text & "' AND NLineNo = '" & cmbLineNo.Text & "' ORDER BY OrderNo"
        Else
            mStrSQL = "SELECT * FROM tblMixExportOrders WHERE Status = 'E' AND PackingListNo = '" & txtPack.Text & "' ORDER BY OrderNo"
        End If
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("OrderNo").Value,
                                    rsComSql.Fields("Subject").Value,
                                    rsComSql.Fields("Reference").Value,
                                    rsComSql.Fields("Side").Value,
                                    rsComSql.Fields("Assortment").Value,
                                    rsComSql.Fields("PacketNo").Value,
                                    rsComSql.Fields("FinishedPcs").Value,
                                    rsComSql.Fields("FinishedCts").Value,
                                    rsComSql.Fields("ID").Value,
                                    rsComSql.Fields("NLineNo").Value,
                                    rsComSql.Fields("AvgCost").Value,
                                    rsComSql.Fields("ClientID").Value)

                rsComSql.MoveNext()
            End While
        Else
            MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing

        txtPcs.Text = CalTotalPcs(flxDetails, 6)
        txtCts.Text = CalTotalCts(flxDetails, 7)
    End Sub

    Private Sub SaveData()
        Dim iRow As Integer
        Dim dblRecord As Double

        ExpProgress.Minimum = 0
        ExpProgress.Visible = True
        If Len(txtPack.Text) = 0 Then
            MsgBox("Please Enter the Packing List No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        For iRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(9, iRow).Value <> "" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT NLineNo FROM tblOrdersDtls WHERE NLineNo = '" & flxDetails.Item(9, iRow).Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    MsgBox("Invalid Line No. - " & flxDetails.Item(9, iRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing
            Else
                MsgBox("Line No. cannot be blank", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        ExpProgress.Minimum = 0
        ExpProgress.Visible = True
        ExpProgress.Maximum = flxDetails.Rows.Count
        dblRecord = 0
        For iRow = 0 To flxDetails.Rows.Count - 1
            AdoCN.Execute("UPDATE tblMixExportOrders SET NLineNo = '" & flxDetails.Item(9, iRow).Value & "' WHERE ID = '" & flxDetails.Item(8, iRow).Value & "'")

            dblRecord = dblRecord + 1
            ExpProgress.Value = dblRecord
            Application.DoEvents()
        Next
        MsgBox("Records Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        flxDetails.Rows.Clear()
        txtPcs.Text = ""
        txtCts.Text = ""
        txtPack.Text = ""
        ExpProgress.Visible = False
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        SaveData()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub frm_MixConfirmOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub Load_PackDetails()
        cmbLineNo.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT NLineNo " & _
                      "FROM dbo.tblMixExportOrders " & _
                      "WHERE (PackingListNo = '" & txtPack.Text & "') AND (Status = 'E') " & _
                      "GROUP BY NLineNo " & _
                      "ORDER BY NLineNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbLineNo.Items.Add(rsComSql.Fields("NLineNo").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(intCalRow, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(intCalRow, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub txtPack_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPack.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtPack.Text <> "" Then
                Load_PackDetails()
            End If
        End If
    End Sub

    Private Sub cmdReport_Click(sender As Object, e As EventArgs) Handles cmdReport.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixExportOrdersSumPacking.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub
End Class