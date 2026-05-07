
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLRghRejects

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ClearText()
        txtParNo.Text = ""
        txtIssPcs.Text = ""
        txtIssCts.Text = ""
        cmbMainCat.Text = ""
        cmbCategory.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        flxDetails.Rows.Clear()
        txtTotPcs.Text = ""
        txtTotCts.Text = ""
        cmbColor.Text = ""
        cmbClarity.Text = ""
        txtPackNo.Text = ""
    End Sub

    Private Sub Load_MainCat()
        cmbMainCat.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT DISTINCT MainCat FROM tblRghRepairCat ORDER BY MainCat", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbMainCat.Items.Add(rsComSql.Fields("MainCat").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
        cmbCategory.Focus()
    End Sub

    Private Sub frm_DCLRghRejects_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_MainCat()
        Load_Color()
        Load_Clarity()
    End Sub

    Private Sub cmbMainCat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMainCat.SelectedIndexChanged
        cmbCategory.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblRghRepairCat WHERE MainCat = '" & cmbMainCat.Text & "' ORDER BY Category", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbCategory.Items.Add(rsComSql.Fields("Category").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Color()
        cmbColor.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblDCLColorClarity WHERE Code = 1 ORDER BY Description", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbColor.Items.Add(rsComSql.Fields("Description").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Clarity()
        cmbClarity.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblDCLColorClarity WHERE Code = 2 ORDER BY Description", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbClarity.Items.Add(rsComSql.Fields("Description").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtParNo.Text <> "" Then
                flxDetails.Rows.Clear()
                txtParNo.Text = UCase(Trim(txtParNo.Text))
                rsComSql = New ADODB.Recordset
                If optRejects.Checked = True Then
                    rsComSql.Open("SELECT * FROM tblParcel WHERE Complete = 0 AND GrpParNo = '" & txtParNo.Text & "' AND (RIGHT(GrpParNo, 2) = 'UN' OR RIGHT(GrpParNo, 2) = 'SN' OR RIGHT(GrpParNo, 2) = 'UV' OR RIGHT(GrpParNo, 2) = 'SV' OR RIGHT(GrpParNo, 2) = 'PN' OR RIGHT(GrpParNo, 2) = 'PV' OR RIGHT(GrpParNo, 2) = 'RN' OR RIGHT(GrpParNo, 2) = 'AN' OR RIGHT(GrpParNo, 2) = 'BN' OR RIGHT(GrpParNo, 2) = 'KN') AND Depart <> 'Rough Dept'", AdoCN, 1, 1)
                Else
                    If optSales.Checked = True Then
                        rsComSql.Open("SELECT * FROM tblParcel WHERE Complete = 0 AND GrpParNo = '" & txtParNo.Text & "' AND Depart = 'RoughSales'", AdoCN, 1, 1)
                    Else
                        rsComSql.Open("SELECT * FROM tblParcel WHERE Complete = 0 AND GrpParNo = '" & txtParNo.Text & "' AND (Depart = 'Princess2' OR Depart = 'Baguettes2' OR Depart = 'Baguettes3' OR Depart = 'Emerald2' OR Depart = 'Emerald3' OR Depart = 'Rounds4' OR Depart = 'Lamour2' OR Depart = 'Galaxy')", AdoCN, 1, 1)
                    End If
                End If
                If rsComSql.RecordCount Then
                    txtIssPcs.Text = rsComSql.Fields("IssuedPcs").Value
                    txtIssCts.Text = rsComSql.Fields("IssuedCts").Value

                    rsComSql_1 = New ADODB.Recordset
                    If optRejects.Checked = True Then
                        rsComSql_1.Open("SELECT * FROM tblParcelDetails WHERE ParcelNo = '" & txtParNo.Text & "' ORDER BY MainCategory, Category", AdoCN, 1, 1)
                    Else
                        If optSales.Checked = True Then
                            rsComSql_1.Open("SELECT * FROM tblParcelRghSales WHERE ParcelNo = '" & txtParNo.Text & "' ORDER BY MainCategory, Category", AdoCN, 1, 1)
                        Else
                            rsComSql_1.Open("SELECT * FROM tblParcelReturns WHERE ParcelNo = '" & txtParNo.Text & "' ORDER BY MainCategory, Category", AdoCN, 1, 1)
                        End If
                    End If
                    If rsComSql_1.RecordCount Then
                        rsComSql_1.MoveFirst()
                        While Not rsComSql_1.EOF
                            flxDetails.Rows.Add(rsComSql_1.Fields("MainCategory").Value,
                                                rsComSql_1.Fields("Category").Value,
                                                rsComSql_1.Fields("PktPcs").Value,
                                                rsComSql_1.Fields("Pktcts").Value,
                                                Format(rsComSql_1.Fields("PktDate").Value, "yyyy-MM-dd"),
                                                rsComSql_1.Fields("Price").Value,
                                                rsComSql_1.Fields("Color").Value,
                                                rsComSql_1.Fields("Clarity").Value,
                                                rsComSql_1.Fields("PackNo").Value)

                            rsComSql_1.MoveNext()
                        End While
                    End If
                    rsComSql_1 = Nothing

                    txtTotPcs.Text = CalTotalPcs(flxDetails)
                    txtTotCts.Text = CalTotalCts(flxDetails)
                Else
                    MsgBox("Invalid Rough Reject Parcel", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    txtParNo.Text = ""
                    txtParNo.Focus()
                End If
                rsComSql = Nothing
            Else
                MsgBox("Pls re-enter Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtParNo.Focus()
            End If
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

    Private Sub cmbCategory_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbCategory.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtPcs.Focus()
        End If
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
            txtPrice.Focus()
        End If
    End Sub

    Private Sub txtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrice.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtPrice.Text)
        If Asc(e.KeyChar) = 13 Then
            cmbColor.Text = "-"
            cmbClarity.Text = "0"
            txtPackNo.Text = "0"
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub cmbColor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbColor.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbClarity.Focus()
        End If
    End Sub

    Private Sub cmbClarity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbClarity.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtPackNo.Focus()
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        If txtParNo.Text = "" Then Exit Sub
        If cmbMainCat.Text = "" Then Exit Sub
        If cmbCategory.Text = "" Then Exit Sub
        If txtPcs.Text = "" Then Exit Sub
        If txtCts.Text = "" Then Exit Sub
        If txtPrice.Text = "" Then Exit Sub
        If cmbColor.Text = "" Then Exit Sub
        If cmbClarity.Text = "" Then Exit Sub
        If txtPackNo.Text = "" Then Exit Sub

        If Val(txtIssPcs.Text) < Val(txtTotPcs.Text) + Val(txtPcs.Text) Then Exit Sub
        If Val(txtIssCts.Text) < Math.Round(Val(txtTotCts.Text) + Val(txtCts.Text), 3) Then Exit Sub

        flxDetails.Rows.Add(cmbMainCat.Text,
                           cmbCategory.Text,
                           txtPcs.Text,
                           txtCts.Text,
                           Format(Date.Now, "yyyy-MM-dd"),
                           txtPrice.Text,
                           cmbColor.Text,
                           cmbClarity.Text,
                           txtPackNo.Text)

        txtTotPcs.Text = CalTotalPcs(flxDetails)
        txtTotCts.Text = CalTotalCts(flxDetails)

        cmbCategory.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtPrice.Text = ""
        cmbColor.Text = ""
        cmbClarity.Text = ""
        txtPackNo.Text = ""
        cmbCategory.Focus()
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim dblInvPrice As Double
        Dim dblInvValue As Double

        rsComSql = New ADODB.Recordset
        If optRejects.Checked = True Then
            rsComSql.Open("SELECT * FROM tblParcel WHERE Complete = 0 AND GrpParNo = '" & txtParNo.Text & "' AND (RIGHT(GrpParNo, 1) = 'N' OR RIGHT(GrpParNo, 1) = 'V')", AdoCN, 1, 1)
        Else
            If optSales.Checked = True Then
                rsComSql.Open("SELECT * FROM tblParcel WHERE Complete = 0 AND GrpParNo = '" & txtParNo.Text & "'", AdoCN, 1, 1)
            Else
                rsComSql.Open("SELECT * FROM tblParcel WHERE Complete = 0 AND GrpParNo = '" & txtParNo.Text & "' AND (Depart = 'Princess2' OR Depart = 'Baguettes2' OR Depart = 'Baguettes3' OR Depart = 'Emerald2' OR Depart = 'Emerald3' OR Depart = 'Rounds4' OR Depart = 'Galaxy')", AdoCN, 1, 1)
            End If
        End If
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Reject/Return/Sales Parcel", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        If optRejects.Checked = True Then
            AdoCN.Execute("DELETE FROM tblParcelDetails WHERE ParcelNo = '" & txtParNo.Text & "'")
        Else
            If optSales.Checked = True Then
                AdoCN.Execute("DELETE FROM tblParcelRghSales WHERE ParcelNo = '" & txtParNo.Text & "'")
            Else
                AdoCN.Execute("DELETE FROM tblParcelReturns WHERE ParcelNo = '" & txtParNo.Text & "'")
            End If
        End If

        For intRow = 0 To flxDetails.Rows.Count - 1
            If optRejects.Checked = True Then
                AdoCN.Execute("INSERT INTO tblParcelDetails(ParcelNo,PktPcs,PktCts,Category,PktDate,MainCategory,Price,Color,Clarity,PackNo) " & _
                              "VALUES('" & txtParNo.Text & "'," & Val(flxDetails.Item(2, intRow).Value) & "," & _
                                "" & Val(flxDetails.Item(3, intRow).Value) & ",'" & flxDetails.Item(1, intRow).Value & "'," & _
                                "'" & Format(Date.Now, "MM/dd/yyyy") & "','" & flxDetails.Item(0, intRow).Value & "'," & Val(flxDetails.Item(5, intRow).Value) & "," & _
                                "'" & flxDetails.Item(6, intRow).Value & "','" & flxDetails.Item(7, intRow).Value & "'," & Val(flxDetails.Item(8, intRow).Value) & ")")
            Else
                If optSales.Checked = True Then

                    AdoCN.Execute("INSERT INTO tblParcelRghSales(ParcelNo,PktPcs,PktCts,Category,PktDate,MainCategory,Price,Color,Clarity,PackNo) " & _
                                  "VALUES('" & txtParNo.Text & "'," & Val(flxDetails.Item(2, intRow).Value) & "," & _
                                      "" & Val(flxDetails.Item(3, intRow).Value) & ",'" & flxDetails.Item(1, intRow).Value & "'," & _
                                      "'" & Format(Date.Now, "MM/dd/yyyy") & "','" & flxDetails.Item(0, intRow).Value & "'," & Val(flxDetails.Item(5, intRow).Value) & "," & _
                                      "'" & flxDetails.Item(6, intRow).Value & "','" & flxDetails.Item(7, intRow).Value & "'," & Val(flxDetails.Item(8, intRow).Value) & ")")

                Else

                    AdoCN.Execute("INSERT INTO tblParcelReturns(ParcelNo,PktPcs,PktCts,Category,PktDate,MainCategory,Price,Color,Clarity,PackNo) " & _
                                  "VALUES('" & txtParNo.Text & "'," & Val(flxDetails.Item(2, intRow).Value) & "," & _
                                    "" & Val(flxDetails.Item(3, intRow).Value) & ",'" & flxDetails.Item(1, intRow).Value & "'," & _
                                    "'" & Format(Date.Now, "MM/dd/yyyy") & "','" & flxDetails.Item(0, intRow).Value & "'," & Val(flxDetails.Item(5, intRow).Value) & "," & _
                                    "'" & flxDetails.Item(6, intRow).Value & "','" & flxDetails.Item(7, intRow).Value & "'," & Val(flxDetails.Item(8, intRow).Value) & ")")
                End If
            End If
        Next

        If optRejects.Checked = True Then
            dblInvPrice = 0
            dblInvValue = 0
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT ROUND(SUM(PktCts * Price) / SUM(PktCts), 2) AS InvPrice, ROUND(SUM(PktCts * Price), 2) AS InvValue FROM dbo.tblParcelDetails " & _
                          "WHERE (ParcelNo = '" & txtParNo.Text & "')", AdoCN, 1, 1)
            If Not IsDBNull(rsComSql.Fields("InvPrice").Value) Then
                dblInvPrice = rsComSql.Fields("InvPrice").Value
                dblInvValue = rsComSql.Fields("InvValue").Value
            End If
            rsComSql = Nothing

            AdoCN.Execute("UPDATE tblParcel SET FinalRate = " & dblInvPrice & ", PlanValue = " & dblInvValue & " WHERE GrpParNo = '" & txtParNo.Text & "'")
        Else
            If optSales.Checked = True Then
                dblInvPrice = 0
                dblInvValue = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT ROUND(SUM(PktCts * Price) / SUM(PktCts), 2) AS InvPrice, ROUND(SUM(PktCts * Price), 2) AS InvValue FROM dbo.tblParcelRghSales " & _
                              "WHERE (ParcelNo = '" & txtParNo.Text & "')", AdoCN, 1, 1)
                If Not IsDBNull(rsComSql.Fields("InvPrice").Value) Then
                    dblInvPrice = rsComSql.Fields("InvPrice").Value
                    dblInvValue = rsComSql.Fields("InvValue").Value
                End If
                rsComSql = Nothing

                AdoCN.Execute("UPDATE tblParcel SET FinalRate = " & dblInvPrice & ", PlanValue = " & dblInvValue & " WHERE GrpParNo = '" & txtParNo.Text & "'")
            Else
                dblInvPrice = 0
                dblInvValue = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT ROUND(SUM(PktCts * Price) / SUM(PktCts), 2) AS InvPrice, ROUND(SUM(PktCts * Price), 2) AS InvValue FROM dbo.tblParcelReturns " & _
                              "WHERE (ParcelNo = '" & txtParNo.Text & "')", AdoCN, 1, 1)
                If Not IsDBNull(rsComSql.Fields("InvPrice").Value) Then
                    dblInvPrice = rsComSql.Fields("InvPrice").Value
                    dblInvValue = rsComSql.Fields("InvValue").Value
                End If
                rsComSql = Nothing

                AdoCN.Execute("UPDATE tblParcel SET FinalRate = " & dblInvPrice & ", PlanValue = " & dblInvValue & " WHERE GrpParNo = '" & txtParNo.Text & "'")
            End If

        End If

        ClearText()
    End Sub

    Private Sub Delete()
        PBResponse = MsgBox("Are you sure to Delete this Parcel?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If optRejects.Checked = True Then
                AdoCN.Execute("DELETE FROM tblParcelDetails WHERE ParcelNo = '" & txtParNo.Text & "'")
            Else
                If optSales.Checked = True Then
                    AdoCN.Execute("DELETE FROM tblParcelRghSales WHERE ParcelNo = '" & txtParNo.Text & "'")
                Else
                    AdoCN.Execute("DELETE FROM tblParcelReturns WHERE ParcelNo = '" & txtParNo.Text & "'")
                End If
            End If
        End If
    End Sub

    Private Sub txtPackNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPackNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearText()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRghSalesValue.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRghRejectsValue.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button3_Click(sender As Object, e As EventArgs) Handles HazelDev_Button3.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRghReturnsValue.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub flxDetails_DoubleClick(sender As Object, e As EventArgs) Handles flxDetails.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)
            txtTotPcs.Text = CalTotalPcs(flxDetails)
            txtTotCts.Text = CalTotalCts(flxDetails)
        End If
    End Sub

    Private Sub HazelDev_Button5_Click(sender As Object, e As EventArgs) Handles HazelDev_Button5.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRghRejects.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button4_Click(sender As Object, e As EventArgs) Handles HazelDev_Button4.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRghSales.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button6_Click(sender As Object, e As EventArgs) Handles HazelDev_Button6.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptDCLParcelRejects.rpt"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm.Show()
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Delete()
    End Sub
End Class