
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RprFinishTransfer
    Dim ParcelNo As String
    Dim PacketNo As String

    Private Sub frm_RprEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        ClearText()
        Load_Shape()
        Load_Size()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_Shape()
        Dim rstCut As ADODB.Recordset

        cmbShape.Items.Clear()
        rstCut = New ADODB.Recordset
        rstCut.Open("SELECT * FROM tblRPrShape ORDER BY Shape", AdoCN, 1, 1)
        If rstCut.RecordCount Then
            rstCut.MoveFirst()
            Do While Not rstCut.EOF
                cmbShape.Items.Add(rstCut.Fields("Shape").Value)
                rstCut.MoveNext()
            Loop
        End If
        rstCut = Nothing
    End Sub

    Private Sub Load_Size()
        Dim rstCut As ADODB.Recordset

        cmbSize.Items.Clear()
        rstCut = New ADODB.Recordset
        rstCut.Open("SELECT * FROM tblRgfSize ORDER BY SizeDec", AdoCN, 1, 1)
        If rstCut.RecordCount Then
            rstCut.MoveFirst()
            Do While Not rstCut.EOF
                cmbSize.Items.Add(rstCut.Fields("SizeDec").Value)
                rstCut.MoveNext()
            Loop
        End If
        rstCut = Nothing
    End Sub

    Private Sub Load_Packets()
        txtParNo.Text = UCase(Trim(txtParNo.Text))

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        If cmbShape.Text = "" Or cmbShape.Text = "PCU" Then
            If cmbSize.Text = "" Then
                rsComSql.Open("SELECT * FROM VW_RprReturnDetailsPCUTrf0 WHERE (ParNo = '" & txtParNo.Text & "') ORDER BY PktNo", AdoCN, 1, 1)
            Else
                rsComSql.Open("SELECT * FROM VW_RprReturnDetailsPCUTrf0 WHERE (ParNo = '" & txtParNo.Text & "') AND (Size = '" & cmbSize.Text & "') ORDER BY PktNo", AdoCN, 1, 1)
            End If
        Else
            If cmbSize.Text = "" Then
                rsComSql.Open("SELECT * FROM VW_RprReturnDetailsPCUTrf0Non WHERE (ParNo = '" & txtParNo.Text & "') AND (Shape = '" & cmbShape.Text & "') ORDER BY PktNo", AdoCN, 1, 1)
            Else
                rsComSql.Open("SELECT * FROM VW_RprReturnDetailsPCUTrf0Non WHERE (ParNo = '" & txtParNo.Text & "') AND (Shape = '" & cmbShape.Text & "') AND (Size = '" & cmbSize.Text & "')  ORDER BY PktNo", AdoCN, 1, 1)
            End If

        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    Math.Round(rsComSql.Fields("RghCts").Value, 3),
                                    Math.Round(rsComSql.Fields("FinCts").Value, 3),
                                    rsComSql.Fields("Value").Value,
                                    rsComSql.Fields("Length").Value,
                                    rsComSql.Fields("Width").Value,
                                    False,
                                    rsComSql.Fields("StoneID").Value,
                                    rsComSql.Fields("ID").Value,
                                    rsComSql.Fields("Size").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtTotPcs.Text = CalTotalPcs(flxDetails)
        txtTotCts.Text = CalTotalCts(flxDetails)
        txtTotFinCts.Text = CalTotalCtsFin(flxDetails)
        txtTotValue.Text = CalTotalValue(flxDetails)
        txtCount.Text = flxDetails.RowCount
    End Sub

    Private Sub Load_Stone()
        txtParNo.Text = UCase(Trim(txtParNo.Text))
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM VW_RprReturnDetailsPCUTrf0 WHERE (ParNo = '" & txtParNo.Text & "') AND (ID = " & CDbl(txtPktID.Text) & ") ORDER BY StoneID", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    Math.Round(rsComSql.Fields("RghCts").Value, 3),
                                    Math.Round(rsComSql.Fields("FinCts").Value, 3),
                                    rsComSql.Fields("Value").Value,
                                    rsComSql.Fields("Length").Value,
                                    rsComSql.Fields("Width").Value,
                                    True,
                                    rsComSql.Fields("StoneID").Value,
                                    rsComSql.Fields("ID").Value,
                                    rsComSql.Fields("Size").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtTotPcs.Text = CalTotalPcs(flxDetails)
        txtTotCts.Text = CalTotalCts(flxDetails)
        txtTotFinCts.Text = CalTotalCtsFin(flxDetails)
        txtTotValue.Text = CalTotalValue(flxDetails)
        txtCount.Text = flxDetails.RowCount

        txtPktID.Text = ""
        txtPktID.Focus()
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(8).EditedFormattedValue = True Then
                CalTotalPcs = CalTotalPcs + Val(flxSample.Item(2, intRow).Value)
            End If
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(8).EditedFormattedValue = True Then
                CalTotalCts = CalTotalCts + Val(flxSample.Item(3, intRow).Value)
            End If
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Function CalTotalCtsFin(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCtsFin = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(8).EditedFormattedValue = True Then
                CalTotalCtsFin = CalTotalCtsFin + Val(flxSample.Item(4, intRow).Value)
            End If
        Next
        CalTotalCtsFin = Math.Round(CalTotalCtsFin, 3)
    End Function

    Private Function CalTotalValue(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalValue = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(8).EditedFormattedValue = True Then
                CalTotalValue = CalTotalValue + Val(flxSample.Item(5, intRow).Value)
            End If
        Next
        CalTotalValue = Math.Round(CalTotalValue, 2)
    End Function

    Private Function CalTotalCount(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCount = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(8).EditedFormattedValue = True Then
                CalTotalCount = CalTotalCount + 1
            End If
        Next
    End Function

    Private Sub ClearText()
        flxDetails.Rows.Clear()
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0.000"
        txtTotFinCts.Text = "0.000"
        txtTotValue.Text = "0.00"
        txtCount.Text = "0"
        txtParNo.Text = ""
        txtPktID.Text = ""
        cmbShape.Text = ""
        cmbSize.Text = ""
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearText()
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(8, intRow).Value = True Then
                AdoCN.Execute("UPDATE tblRPrReturnDetails SET Trf = 1 WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND ID = " & CDbl(flxDetails.Item(9, intRow).Value) & "")
            End If
        Next

        MsgBox("Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        ClearText()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtParNo.Text <> "" Then
                If Len(txtParNo.Text) = 7 Then
                    txtPktID.Focus()
                    'Load_Packets()
                End If
            End If
        End If
    End Sub

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 8 Then
            txtTotPcs.Text = CalTotalPcs(flxDetails)
            txtTotCts.Text = CalTotalCts(flxDetails)
            txtTotFinCts.Text = CalTotalCtsFin(flxDetails)
            txtTotValue.Text = CalTotalValue(flxDetails)
            txtCount.Text = CalTotalCount(flxDetails)
        End If
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(8, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(8, intRow).Value = False
            Next
        End If

        txtTotPcs.Text = CalTotalPcs(flxDetails)
        txtTotCts.Text = CalTotalCts(flxDetails)
        txtTotFinCts.Text = CalTotalCtsFin(flxDetails)
        txtTotValue.Text = CalTotalValue(flxDetails)
        txtCount.Text = CalTotalCount(flxDetails)
    End Sub

    Private Sub txtPktID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktID.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtPktID.Text <> "" Then
                Load_Stone()
            End If
        End If
    End Sub

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Load_Packets()
    End Sub
End Class