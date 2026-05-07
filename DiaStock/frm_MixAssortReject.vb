
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixAssortReject
    Dim strFolderPath As String

    Private Sub Load_Rejects()

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        If chkAll.Checked = True Then
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixReturns.ParNo, dbo.tblMixReturns.PktNo, dbo.tblMixReturns.Sec, dbo.tblMixReturns.EmpNo, dbo.tblMixReturns.RejPcs, dbo.tblMixReturns.RejCts, dbo.tblMixReturns.RghCts, " & _
                            "dbo.tblMixPacket.AssortNo, dbo.tblMixReturns.RejReason, dbo.tblMixReturns.RetDate, dbo.tblMixPacket.Grp, dbo.tblMixReturns.ID, dbo.tblMixPacket.PktRefNo, dbo.tblOrders.Niruref " & _
                          "FROM dbo.tblMixReturns INNER JOIN dbo.tblMixPacket ON dbo.tblMixReturns.ParNo = dbo.tblMixPacket.PktOrdNo AND dbo.tblMixReturns.PktNo = dbo.tblMixPacket.PktNo INNER JOIN " & _
                            "dbo.tblOrders ON dbo.tblMixReturns.ParNo = dbo.tblOrders.OrderNo " & _
                          "WHERE (dbo.tblMixReturns.RejStatus = 2) AND (dbo.tblMixReturns.RejPcs > 0) " & _
                          "ORDER BY dbo.tblMixReturns.ParNo, dbo.tblMixReturns.PktNo, dbo.tblMixReturns.Sec, dbo.tblMixReturns.RejReason", AdoCN, 1, 1)
        Else
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixReturns.ParNo, dbo.tblMixReturns.PktNo, dbo.tblMixReturns.Sec, dbo.tblMixReturns.EmpNo, dbo.tblMixReturns.RejPcs, dbo.tblMixReturns.RejCts, dbo.tblMixReturns.RghCts, " & _
                            "dbo.tblMixPacket.AssortNo, dbo.tblMixReturns.RejReason, dbo.tblMixReturns.RetDate, dbo.tblMixPacket.Grp, dbo.tblMixReturns.ID, dbo.tblMixPacket.PktRefNo, dbo.tblOrders.Niruref " & _
                          "FROM dbo.tblMixReturns INNER JOIN dbo.tblMixPacket ON dbo.tblMixReturns.ParNo = dbo.tblMixPacket.PktOrdNo AND dbo.tblMixReturns.PktNo = dbo.tblMixPacket.PktNo INNER JOIN " & _
                            "dbo.tblOrders ON dbo.tblMixReturns.ParNo = dbo.tblOrders.OrderNo " & _
                          "WHERE (dbo.tblMixReturns.RejStatus = 2) AND (dbo.tblMixReturns.RejPcs > 0) AND (dbo.tblMixReturns.RetDate = '" & Format(dtpDate.Value, "MM/dd/yyyy") & "') " & _
                          "ORDER BY dbo.tblMixReturns.ParNo, dbo.tblMixReturns.PktNo, dbo.tblMixReturns.Sec, dbo.tblMixReturns.RejReason", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("RejPcs").Value,
                                    rsComSql.Fields("RejCts").Value,
                                    rsComSql.Fields("AssortNo").Value,
                                    rsComSql.Fields("Sec").Value,
                                    rsComSql.Fields("EmpNo").Value,
                                    rsComSql.Fields("RejReason").Value,
                                    Format(rsComSql.Fields("RetDate").Value, "yyyy/MM/dd"),
                                    rsComSql.Fields("Grp").Value,
                                    rsComSql.Fields("ID").Value,
                                    rsComSql.Fields("PktRefNo").Value,
                                    rsComSql.Fields("Niruref").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

    End Sub

    Private Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        Load_Rejects()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        Dim intRejPcs As Integer
        Dim dblRejCts As Double
        Dim intOutPcs As Integer
        Dim dblOutCts As Double
        Dim strSupParNo As String
        Dim strOrigin As String
        Dim dtpRejDate As Date

        flxReject.Rows.Clear()

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT RejStatus FROM tblMixReturns WHERE ID = " & CDbl(flxDetails.Item(10, flxDetails.CurrentRow.Index).Value) & "", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If rsComSql.Fields("RejStatus").Value <> 2 Then
                MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        End If
        rsComSql = Nothing


        intRejPcs = CInt(flxDetails.Item(2, flxDetails.CurrentRow.Index).Value)
        dblRejCts = Math.Round(CDbl(flxDetails.Item(3, flxDetails.CurrentRow.Index).Value), 3)

        txtTotPcs.Text = flxDetails.Item(2, flxDetails.CurrentRow.Index).Value
        txtTotCts.Text = flxDetails.Item(3, flxDetails.CurrentRow.Index).Value

        dtpRejDate = CDate(flxDetails.Item(8, flxDetails.CurrentRow.Index).Value)

        intOutPcs = 0
        dblOutCts = 0
        strSupParNo = ""
        strOrigin = ""
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts, Assortment, OrgAssort " & _
                      "FROM tblMixPacketDetails " & _
                      "WHERE (ParNo = '" & flxDetails.Item(0, flxDetails.CurrentRow.Index).Value & "') AND (PktNo = '" & flxDetails.Item(1, flxDetails.CurrentRow.Index).Value & "') " & _
                      "GROUP BY Assortment, OrgAssort " & _
                      "HAVING SUM(Pcs) > 0 " & _
                      "ORDER BY Cts ", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF And intRejPcs > 0
                intOutPcs = 0
                dblOutCts = 0
                If rsComSql.Fields("Pcs").Value >= intRejPcs Then
                    intOutPcs = intRejPcs
                    dblOutCts = dblRejCts

                    intRejPcs = 0
                    dblRejCts = 0
                ElseIf rsComSql.Fields("Pcs").Value < intRejPcs Then
                    intOutPcs = rsComSql.Fields("Pcs").Value
                    dblOutCts = rsComSql.Fields("Cts").Value

                    intRejPcs = intRejPcs - rsComSql.Fields("Pcs").Value
                    dblRejCts = dblRejCts - rsComSql.Fields("Cts").Value
                End If

                dblOutCts = Math.Round(dblOutCts, 3)

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblMixRejectOrigin WHERE RetID = " & CDbl(flxDetails.Item(10, flxDetails.CurrentRow.Index).Value) & " ORDER BY Pcs DESC", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    rsComSql_1.MoveFirst()
                    strSupParNo = rsComSql_1.Fields("SupParNo").Value
                    strOrigin = rsComSql_1.Fields("Origin").Value
                Else
                    strSupParNo = "X900003"
                    strOrigin = "De Beers"
                End If
                rsComSql_1 = Nothing

                flxReject.Rows.Add(flxDetails.Item(0, flxDetails.CurrentRow.Index).Value,
                                   flxDetails.Item(1, flxDetails.CurrentRow.Index).Value,
                                   intOutPcs,
                                   dblOutCts,
                                   rsComSql.Fields("Assortment").Value,
                                   rsComSql.Fields("OrgAssort").Value,
                                   flxDetails.Item(5, flxDetails.CurrentRow.Index).Value,
                                   flxDetails.Item(6, flxDetails.CurrentRow.Index).Value,
                                   flxDetails.Item(7, flxDetails.CurrentRow.Index).Value,
                                   flxDetails.Item(10, flxDetails.CurrentRow.Index).Value,
                                   strSupParNo,
                                   strOrigin,
                                   Format(dtpRejDate, "yyyy/MM/dd"))

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub flxReject_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxReject.CellClick
        txtOrder.Text = flxReject.Item(0, flxReject.CurrentRow.Index).Value
        txtPkt.Text = flxReject.Item(1, flxReject.CurrentRow.Index).Value
        txtRejPcs.Text = flxReject.Item(2, flxReject.CurrentRow.Index).Value
        txtPcs.Text = flxReject.Item(2, flxReject.CurrentRow.Index).Value
        txtRejCts.Text = flxReject.Item(3, flxReject.CurrentRow.Index).Value
        txtCts.Text = flxReject.Item(3, flxReject.CurrentRow.Index).Value
        txtAssortment.Text = flxReject.Item(4, flxReject.CurrentRow.Index).Value
        txtOrig.Text = flxReject.Item(5, flxReject.CurrentRow.Index).Value
        txtSec.Text = flxReject.Item(6, flxReject.CurrentRow.Index).Value
        txtEmpNo.Text = flxReject.Item(7, flxReject.CurrentRow.Index).Value
        txtReason.Text = flxReject.Item(8, flxReject.CurrentRow.Index).Value
        txtID.Text = flxReject.Item(9, flxReject.CurrentRow.Index).Value
        txtSupParNo.Text = flxReject.Item(10, flxReject.CurrentRow.Index).Value
        txtOrigin.Text = flxReject.Item(11, flxReject.CurrentRow.Index).Value
        dtpRejDate.Value = flxReject.Item(12, flxReject.CurrentRow.Index).Value

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & txtAssortment.Text & "' AND Active = 1 AND Origin <> ''", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            txtNew.Text = Mid(txtAssortment.Text, 1, 6)
        Else
            txtNew.Text = ""
        End If
        rsComSql = Nothing

        txtPcs.Focus()
    End Sub

    Private Sub txtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtCts.Focus()
        End If
    End Sub

    Private Sub txtCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtNew.Focus()
        End If
    End Sub

    Private Sub txtNew_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNew.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub frm_MixAssortReject_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        dtpDate.Value = DateAdd(DateInterval.Day, -1, Date.Now)
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim dblPrice As Double

        Dim intTotPcs As Integer
        Dim dblTotCts As Double

        If txtOrder.Text <> "" And txtPkt.Text <> "" And txtOrig.Text <> "" And txtAssortment.Text <> "" And txtNew.Text <> "" And txtPcs.Text <> "" And txtCts.Text <> "" Then

            If Len(txtAssortment.Text) < 4 Then
                MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CInt(txtPcs.Text) <= 0 Then
                MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(txtCts.Text) <= 0 Then
                MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Len(txtEstCts.Text) = 0 Then
                txtEstCts.Text = "0"
            End If

            If CDbl(txtCts.Text) < CDbl(txtEstCts.Text) Then
                MsgBox("Invalid Est Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            'If Mid(txtAssortment.Text, 1, 3) = "SRW" Then
            '    If Mid(txtNew.Text, 1, 3) = "SRW" Then
            '        If txtAssortment.Text <> txtNew.Text Then
            '            MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '            Exit Sub
            '        End If
            '    End If
            'End If

            'If Mid(txtAssortment.Text, 1, 3) <> "SRW" Then
            '    If Mid(txtNew.Text, 1, 3) = "SRW" Then
            '        MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '        Exit Sub
            '    End If
            'End If

            intTotPcs = CalTotalPcs(flxSelect) + Val(txtPcs.Text)
            If Val(txtTotPcs.Text) < intTotPcs Then
                MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            dblTotCts = CalTotalCts(flxSelect) + Val(txtCts.Text)
            If Val(txtTotCts.Text) < Format(dblTotCts, "0.000") Then
                MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT Assortment FROM tblAssortList WHERE Assortment = '" & txtAssortment.Text & "' AND Active = 1 AND Origin <> ''", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Mid(txtAssortment.Text, 1, 5) <> Mid(txtNew.Text, 1, 5) Then
                    MsgBox("Invalid Assortment Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT MarketPrice FROM tblAssortList WHERE Assortment = '" & txtNew.Text & "' AND Active = 1", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                dblPrice = rsComSql.Fields("MarketPrice").Value
            Else
                dblPrice = 0
                MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            If Len(txtPlanAssort.Text) > 0 Then
                txtPlanAssort.Text = UCase(txtPlanAssort.Text)

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & txtPlanAssort.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    MsgBox("Invalid Plan Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing

                If CDbl(txtEstCts.Text) <= 0 Then
                    MsgBox("Invalid Estimated Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            'For intRow = 0 To flxSelect.Rows.Count - 1
            '    If flxSelect.Item(4, intRow).Value = txtNew.Text And flxSelect.Item(5, intRow).Value = txtOrig.Text Then
            '        flxSelect.Item(2, intRow).Value = Val(flxSelect.Item(2, intRow).Value) + Val(txtPcs.Text)
            '        flxSelect.Item(3, intRow).Value = Val(flxSelect.Item(3, intRow).Value) + Val(txtCts.Text)

            '        txtPcs.Text = Val(txtRejPcs.Text) - Val(txtPcs.Text)
            '        txtCts.Text = Val(txtRejCts.Text) - Val(txtCts.Text)
            '        Exit Sub
            '    End If
            'Next

            flxSelect.Rows.Add(txtOrder.Text,
                               txtPkt.Text,
                               txtPcs.Text,
                               txtCts.Text,
                               UCase(txtNew.Text),
                               UCase(txtOrig.Text),
                               dblPrice,
                               txtAssortment.Text,
                               Trim(txtReason.Text),
                               Trim(txtEmpNo.Text),
                               txtSupParNo.Text,
                               txtOrigin.Text,
                               dtpRejDate.Value,
                               txtSec.Text,
                               txtEstCts.Text,
                               txtPlanAssort.Text)

            txtPcs.Text = Val(txtRejPcs.Text) - Val(txtPcs.Text)
            txtCts.Text = Val(txtRejCts.Text) - Val(txtCts.Text)
        Else
            MsgBox("Please enter the New Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
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

    Private Sub Save()
        Dim intRow As Integer

        If CalTotalPcs(flxSelect) <> CInt(txtTotPcs.Text) Then
            MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If CalTotalCts(flxSelect) <> Val(txtTotCts.Text) Then
            MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        For intRow = 0 To flxSelect.Rows.Count - 1
            AdoCN.Execute("INSERT INTO tblMixRejects(ParNo,PktNo,Pcs,Cts,Assortment,OrgAssort,Price,InID,ImportNo,RejDate,OldAssort,Stock,Export,OK,SupParNo,Origin,DoneBy,ProdRejDate,Sec,Reason,Reason2,EstCts,EstAssort) " & _
                          "VALUES('" & flxSelect.Item(0, intRow).Value & "','" & flxSelect.Item(1, intRow).Value & "'," & CInt(flxSelect.Item(2, intRow).Value) & "," & CDbl(flxSelect.Item(3, intRow).Value) & "," & _
                            "'" & flxSelect.Item(4, intRow).Value & "','" & flxSelect.Item(5, intRow).Value & "'," & CDbl(flxSelect.Item(6, intRow).Value) & ",0,1,'" & Format(Date.Now, "MM/dd/yyyy") & "'," & _
                            "'" & flxSelect.Item(7, intRow).Value & "',1,0,2,'" & flxSelect.Item(10, intRow).Value & "','" & flxSelect.Item(11, intRow).Value & "','" & PBUser_EmpNo & "'," & _
                            "'" & Format(CDate(dtpRejDate.Value), "MM/dd/yyyy") & "'," & CInt(flxSelect.Item(13, intRow).Value) & ",'" & flxSelect.Item(8, intRow).Value & "','" & cmbReason.Text & "'," & _
                            "" & CDbl(flxSelect.Item(14, intRow).Value) & ",'" & flxSelect.Item(15, intRow).Value & "')")

            If chkReIssue.Checked = True Then
                AdoCN.Execute("UPDATE tblMixReturns SET RejStatus = 3, RejReason = 'Re-Issue Prod' WHERE ID = " & CDbl(txtID.Text) & "")
            Else
                AdoCN.Execute("UPDATE tblMixReturns SET RejStatus = 3 WHERE ID = " & CDbl(txtID.Text) & "")
            End If

        Next

        ClearFields()
        'Load_Rejects()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub ClearFields()
        flxSelect.Rows.Clear()
        flxReject.Rows.Clear()

        txtOrder.Text = ""
        txtPkt.Text = ""
        txtOrig.Text = ""
        txtRejPcs.Text = ""
        txtRejCts.Text = ""
        txtAssortment.Text = ""
        txtNew.Text = ""
        txtPlanAssort.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtEstCts.Text = ""
        txtSec.Text = ""
        txtEmpNo.Text = ""
        txtTotPcs.Text = ""
        txtTotCts.Text = ""
        txtReason.Text = ""
        txtID.Text = ""
        txtSupParNo.Text = ""
        txtOrigin.Text = ""
        cmbReason.Text = ""
        dtpRejDate.Value = Date.Now
        chkReIssue.Checked = False
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixAssortRejectDetails.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixRejectsStatus1.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub flxSelect_DoubleClick(sender As Object, e As EventArgs) Handles flxSelect.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxSelect.Rows.RemoveAt(flxSelect.CurrentRow.Index)

            txtTotPcs.Text = CalTotalPcs(flxSelect)
            txtTotCts.Text = CalTotalCts(flxSelect)
        End If
    End Sub

    Private Sub txtEstCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEstCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtEstCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtPlanAssort.Focus()
        End If
    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixRejectsEst.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub
End Class