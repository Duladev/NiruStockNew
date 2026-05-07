
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RprPacketAuto

    Private Sub frm_RprPacketAuto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        cmbColor.Text = ""
        cmbClarity.Text = ""
        cmbFlo.Text = ""
        cmbModel.Text = ""
    End Sub

    Private Sub ClearPacket()
        txtMaxPktNo.Text = ""
        txtAssortment.Text = ""
        txtBalPcs.Text = ""
        txtBalCts.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtTension.Text = ""
        txtComment.Text = ""
        flxDetails.Rows.Clear()
        txtTotPcs.Text = ""
        txtTotCts.Text = ""
        cmbColor.Text = ""
        cmbClarity.Text = ""
        cmbFlo.Text = ""
        cmbModel.Text = ""
        txtRghPktNo.Text = ""
        txtSelPcs.Text = ""
        txtSelCts.Text = ""
        txtSysPcs.Text = ""
        txtSysCts.Text = ""
        txtWOEmp.Text = ""
    End Sub

    Private Sub ClearText()
        txtParNo.Text = ""
        txtMaxPktNo.Text = ""
        txtAssortment.Text = ""
        txtBalPcs.Text = ""
        txtBalCts.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtTension.Text = ""
        txtComment.Text = ""
        flxDetails.Rows.Clear()
        txtTotPcs.Text = ""
        txtTotCts.Text = ""
        cmbColor.Text = ""
        cmbClarity.Text = ""
        cmbFlo.Text = ""
        cmbModel.Text = ""
        txtRghPktNo.Text = ""
        txtSelPcs.Text = ""
        txtSelCts.Text = ""
        txtSysPcs.Text = ""
        txtSysCts.Text = ""
        txtWOEmp.Text = ""
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        Dim intTrfPcs As Integer
        Dim dblTrfCts As Double

        If Asc(e.KeyChar) = 13 And txtParNo.Text <> "" Then
            txtParNo.Text = UCase(txtParNo.Text)
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblParcel WHERE GrpParNo = '" & txtParNo.Text & "' AND Depart = 'RoughPlan'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearText()
                Exit Sub
            Else
                txtAssortment.Text = rsComSql.Fields("Assortment").Value
            End If
            rsComSql = Nothing

            intTrfPcs = 0
            dblTrfCts = 0
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(NewACTPcs) AS NewACTPcs, ROUND(SUM(NewACTCts), 3) AS NewACTCts " & _
                          "FROM dbo.tblDep_Trf " & _
                          "WHERE (Department = 'RoughPlan') AND (DCLParcelNo = '" & Mid(txtParNo.Text, 1, 6) & "')", AdoCN, 1, 1)
            If Not IsDBNull(rsComSql.Fields("NewACTPcs").Value) Then
                intTrfPcs = rsComSql.Fields("NewACTPcs").Value
                dblTrfCts = rsComSql.Fields("NewACTCts").Value
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktCts), 3) AS PktCts FROM tblRPrPacket WHERE ParNo LIKE '" & Mid(txtParNo.Text, 1, 6) & "' + '%' AND Department = 'RoughPlan'", AdoCN, 1, 1)
            If Not IsDBNull(rsComSql.Fields("PktPcs").Value) Then
                intTrfPcs = intTrfPcs - rsComSql.Fields("PktPcs").Value
                dblTrfCts = dblTrfCts - rsComSql.Fields("PktCts").Value
            End If
            rsComSql = Nothing

            txtMaxPktNo.Text = "0001"
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT MAX(PktNo) AS MaxPkt FROM tblRPrPacket WHERE ParNo = '" & txtParNo.Text & "' AND Department = 'RoughPlan'", AdoCN, 1, 1)
            If Not IsDBNull(rsComSql.Fields("MaxPkt").Value) Then
                txtMaxPktNo.Text = Format(rsComSql.Fields("MaxPkt").Value + 1, "0000")
            End If
            rsComSql = Nothing

            txtBalPcs.Text = intTrfPcs
            txtBalCts.Text = Math.Round(dblTrfCts, 3)

            cmbColor.Items.Clear()
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT PktColor FROM dbo.tblRghPacket WHERE (PktType = 6) AND (ParNo = '" & txtParNo.Text & "') GROUP BY PktColor", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    cmbColor.Items.Add(rsComSql.Fields("PktColor").Value)
                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing

            cmbClarity.Items.Clear()
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT PktClarity FROM dbo.tblRghPacket WHERE (PktType = 6) AND (ParNo = '" & txtParNo.Text & "') GROUP BY PktClarity", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    cmbClarity.Items.Add(rsComSql.Fields("PktClarity").Value)
                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing

            cmbFlo.Items.Clear()
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT PktFlo FROM dbo.tblRghPacket WHERE (PktType = 6) AND (ParNo = '" & txtParNo.Text & "') GROUP BY PktFlo", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    cmbFlo.Items.Add(rsComSql.Fields("PktFlo").Value)
                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing

            cmbModel.Items.Clear()
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT PktModel FROM dbo.tblRghPacket WHERE (PktType = 6) AND (ParNo = '" & txtParNo.Text & "') GROUP BY PktModel", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    cmbModel.Items.Add(rsComSql.Fields("PktModel").Value)
                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing

            txtTotPcs.Text = CalTotalPcs(flxDetails)
            txtTotCts.Text = CalTotalCts(flxDetails)

            txtRghPktNo.Focus()
        End If
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(1, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(2, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub SavePacket()
        Dim intRow As Integer
        Dim dblTotPcs As Double
        Dim dblWindowPcs As Double
        Dim dblImpValue As Double
        Dim dblEstValue As Double
        Dim intApproval As Integer
        Dim dblPerc As Double

        If txtParNo.Text = "" Then
            MsgBox("Invalid Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If cmbColor.Text = "" Then
            MsgBox("Invalid Color", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If cmbClarity.Text = "" Then
            MsgBox("Invalid Clarity", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If cmbFlo.Text = "" Then
            MsgBox("Invalid Fluorescent", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If cmbModel.Text = "" Then
            MsgBox("Invalid Model", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        dblTotPcs = 0
        dblImpValue = 0
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT OrigParcelNo,IssuedPcs,IssuedCts,Approval FROM tblParcel WHERE GrpParNo = '" & txtParNo.Text & "' AND Depart = 'Rough Planning'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            dblTotPcs = rsComSql_1.Fields("IssuedPcs").Value
            intApproval = rsComSql_1.Fields("Approval").Value

            rsComSql_2 = New ADODB.Recordset
            rsComSql_2.Open("SELECT ItemCost, HardCost FROM tblImport WHERE SupParcelNo = '" & rsComSql_1.Fields("OrigParcelNo").Value & "'", AdoCN, 1, 1)
            If rsComSql_2.RecordCount Then
                If rsComSql_2.Fields("HardCost").Value > 0 Then
                    dblImpValue = Math.Round(rsComSql_1.Fields("IssuedCts").Value * rsComSql_2.Fields("HardCost").Value, 2)
                Else
                    dblImpValue = Math.Round(rsComSql_1.Fields("IssuedCts").Value * rsComSql_2.Fields("ItemCost").Value, 2)
                End If
            End If
            rsComSql_2 = Nothing
        End If
        rsComSql_1 = Nothing

        dblWindowPcs = 0
        dblEstValue = 0
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktCts * PktPrice), 2) AS PktValue FROM tblRghPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktType = 6", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            If Not IsDBNull(rsComSql_1.Fields("PktPcs").Value) Then
                dblWindowPcs = rsComSql_1.Fields("PktPcs").Value
                dblEstValue = rsComSql_1.Fields("PktValue").Value
            End If
        End If
        rsComSql_1 = Nothing

        If dblTotPcs > dblWindowPcs And intApproval = 0 Then
            MsgBox(dblTotPcs - dblWindowPcs & " pcs pending", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearText()
            Exit Sub
        End If

        If dblImpValue > 0 Then
            dblPerc = 0
            dblPerc = ((dblEstValue - dblImpValue) / dblImpValue) * 100

            If dblPerc < -10 And intApproval = 0 Then
                If dblImpValue > dblEstValue And intApproval = 0 Then
                    MsgBox(dblImpValue - dblEstValue & " value lost. Get the approval to proceed", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                    ClearText()
                    Exit Sub
                End If
            End If
        End If

        dtpToday = GetToday()
        For intRow = 0 To flxDetails.Rows.Count - 1
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblRPrPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & flxDetails.Item(0, intRow).Value & "' AND Department = 'RoughPlan'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount = 0 Then

                AdoCN.Execute("INSERT INTO tblRPrPacket(Department,ParNo,PktNo,PktPcs,PktCts,Assortment,PktColor,PktIss,PktFlow,PktClarity,PktCut,FinCts,EstYld,Flo,Tension,MainPkt,RefPktNo,Comment,WOEmpNo,Model,DoneBy) " & _
                              "VALUES('RoughPlan','" & txtParNo.Text & "','" & flxDetails.Item(0, intRow).Value & "'," & CInt(flxDetails.Item(1, intRow).Value) & "," & CDbl(flxDetails.Item(2, intRow).Value) & "," & _
                                "'" & txtAssortment.Text & "','" & cmbColor.Text & "','" & Format(dtpToday, "MM/dd/yyyy") & "','PlanFlow','" & cmbClarity.Text & "','-',0,0,'" & cmbFlo.Text & "'," & _
                                "" & CDbl(flxDetails.Item(3, intRow).Value) & ",'','" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(4, intRow).Value & "','" & txtWOEmp.Text & "','" & cmbModel.Text & "','" & PBUser_EmpNo & "')")

                AdoCN.Execute("UPDATE tblRPrPacket SET ID = ID2 WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & flxDetails.Item(0, intRow).Value & "' AND Department = 'RoughPlan'")
            End If
            rsComSql_1 = Nothing
            AdoCN.Execute("UPDATE tblRPrPacket SET Trf = 1 WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtRghPktNo.Text & "' AND Department = 'RoughWO'")
        Next
        MsgBox("Packets Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        ClearPacket()
        txtParNo.Focus()
    End Sub

    Private Sub txtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs.KeyPress
        IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 And Len(txtPcs.Text) > 0 Then
            txtCts.Focus()
        End If
    End Sub

    Private Sub txtCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCts.KeyPress
        NumericOnly(Asc(e.KeyChar), txtCts.Text)
        If Asc(e.KeyChar) = 13 And Len(txtCts.Text) > 0 Then
            txtTension.Focus()
        End If
    End Sub

    Private Sub txtTension_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTension.KeyPress
        IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtTension.Text = "" Then
                txtTension.Text = "0"
            End If
            txtComment.Focus()
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        If txtParNo.Text = "" Then MsgBox("Invalid Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtMaxPktNo.Text = "" Then MsgBox("Invalid Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtPcs.Text = "" Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtCts.Text = "" Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtTension.Text = "" Then MsgBox("Invalid Tension", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If txtSelPcs.Text = "" Then MsgBox("Invalid Select Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtSelCts.Text = "" Then MsgBox("Invalid Select Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If CDbl(txtSelPcs.Text) <= 0 Then MsgBox("Invalid Select Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtSelCts.Text) <= 0 Then MsgBox("Invalid Select Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If CDbl(txtBalPcs.Text) < CDbl(txtSelPcs.Text) Then MsgBox("Invalid Select Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If Math.Round(CDbl(txtBalCts.Text), 3) < Math.Round(CDbl(txtSelCts.Text), 3) Then MsgBox("Invalid Select Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If CDbl(txtTotPcs.Text) + CDbl(txtPcs.Text) > CDbl(txtSelPcs.Text) Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtTotCts.Text) + CDbl(txtCts.Text) > CDbl(txtSelCts.Text) Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        flxDetails.Rows.Add(txtMaxPktNo.Text,
                           txtPcs.Text,
                           Format(CDbl(txtCts.Text), "#0.000"),
                           txtTension.Text,
                           txtComment.Text)

        txtMaxPktNo.Text = Format(CDbl(txtMaxPktNo.Text) + 1, "0000")
        txtPcs.Text = "1"
        txtCts.Text = ""
        txtTension.Text = ""
        txtComment.Text = ""

        txtTotPcs.Text = CalTotalPcs(flxDetails)
        txtTotCts.Text = CalTotalCts(flxDetails)

        txtCts.Focus()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearText()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        SavePacket()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub GetRghData()
        txtSysPcs.Text = "0"
        txtSysCts.Text = "0"
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktCts), 3) AS PktCts FROM tblRghPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktType = 6 AND PktColor = '" & cmbColor.Text & "' AND PktClarity = '" & cmbClarity.Text & "' AND PktFlo = '" & cmbFlo.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("PktPcs").Value) Then
                txtSysPcs.Text = rsComSql.Fields("PktPcs").Value
                txtSysCts.Text = rsComSql.Fields("PktCts").Value
            End If
        End If
        rsComSql = Nothing

    End Sub

    Private Sub txtRghPktNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRghPktNo.KeyPress
        IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtRghPktNo.Text = Format(CInt(txtRghPktNo.Text), "0000")
            If txtParNo.Text <> "" Then
                txtSelPcs.Text = "0"
                txtSelCts.Text = "0"

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT dbo.tblRPrPacket.PktColor, dbo.tblRPrPacket.PktClarity , dbo.tblRPrPacket.Flo, dbo.tblRPrPacket.Model " & _
                              "FROM dbo.tblRPrReturns INNER JOIN dbo.tblRPrPacket ON dbo.tblRPrReturns.Department = dbo.tblRPrPacket.Department AND dbo.tblRPrReturns.ParNo = dbo.tblRPrPacket.ParNo AND " & _
                                "dbo.tblRPrReturns.PktNo = dbo.tblRPrPacket.PktNo " & _
                              "WHERE (dbo.tblRPrReturns.Sec = 20) AND (dbo.tblRPrPacket.Department = 'RoughWO') AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrPacket.PktNo = '" & txtRghPktNo.Text & "') ", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    cmbColor.Text = rsComSql_1.Fields("PktColor").Value
                    cmbClarity.Text = rsComSql_1.Fields("PktClarity").Value
                    cmbFlo.Text = rsComSql_1.Fields("Flo").Value
                    cmbModel.Text = rsComSql_1.Fields("Model").Value
                Else
                    cmbColor.Text = ""
                    cmbClarity.Text = ""
                    cmbFlo.Text = ""
                    cmbModel.Text = ""
                End If
                rsComSql_1 = Nothing

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT EmpNo FROM dbo.tblRPrReturns " & _
                                "WHERE (Sec = 2) AND (Department = 'RoughWO') AND (ParNo = '" & txtParNo.Text & "') AND (PktNo = '" & txtRghPktNo.Text & "') ", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    txtWOEmp.Text = UCase(rsComSql_1.Fields("EmpNo").Value)
                Else
                    txtWOEmp.Text = ""
                End If
                rsComSql_1 = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(dbo.tblRPrReturns.RetPcsT + dbo.tblRPrReturns.RetPcsB) AS RetPcs, ROUND(SUM(dbo.tblRPrReturns.RetCts), 3) AS RetCts " & _
                              "FROM dbo.tblRPrReturns INNER JOIN dbo.tblRPrPacket ON dbo.tblRPrReturns.Department = dbo.tblRPrPacket.Department AND dbo.tblRPrReturns.ParNo = dbo.tblRPrPacket.ParNo AND " & _
                                "dbo.tblRPrReturns.PktNo = dbo.tblRPrPacket.PktNo " & _
                              "WHERE (dbo.tblRPrReturns.Sec = 20) AND (dbo.tblRPrPacket.Department = 'RoughWO') AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRPrPacket.PktColor = '" & cmbColor.Text & "') AND " & _
                                "(dbo.tblRPrPacket.PktClarity = '" & cmbClarity.Text & "') AND (dbo.tblRPrPacket.Flo = '" & cmbFlo.Text & "') AND (dbo.tblRPrPacket.Model = '" & cmbModel.Text & "') ", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("RetPcs").Value) Then
                        txtSelPcs.Text = rsComSql.Fields("RetPcs").Value
                        txtSelCts.Text = rsComSql.Fields("RetCts").Value

                        txtPcs.Text = "1"
                        txtCts.Focus()
                    End If
                End If
                rsComSql = Nothing
            End If

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktCts), 3) AS PktCts " & _
                          "FROM dbo.tblRPrPacket " & _
                          "WHERE (Department = 'RoughPlan') AND (ParNo = '" & txtParNo.Text & "') AND (PktColor = '" & cmbColor.Text & "') AND (PktClarity = '" & cmbClarity.Text & "') AND (Flo = '" & cmbFlo.Text & "') AND (Model = '" & cmbModel.Text & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("PktPcs").Value) Then
                    txtSelPcs.Text = CDbl(txtSelPcs.Text) - rsComSql.Fields("PktPcs").Value
                    txtSelCts.Text = Format(Math.Round(CDbl(txtSelCts.Text) - rsComSql.Fields("PktCts").Value, 3), "#0.000")
                End If
            End If
            rsComSql = Nothing

            GetRghData()

        End If
    End Sub

    Private Sub txtComment_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtComment.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub flxDetails_DoubleClick(sender As Object, e As EventArgs) Handles flxDetails.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse  = MsgBoxResult.Yes Then
            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)

            txtTotPcs.Text = CalTotalPcs(flxDetails)
            txtTotCts.Text = CalTotalCts(flxDetails)
        End If
    End Sub

    Private Sub cmdGetData_Click(sender As Object, e As EventArgs) Handles cmdGetData.Click
        GetRghData()
    End Sub

    Private Sub cmdPktID_Click(sender As Object, e As EventArgs) Handles cmdPktID.Click
        AdoCN.Execute("UPDATE tblRPrPacket SET ID = ID2 WHERE (ID IS NULL)")

        MsgBox("Packet ID Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
    End Sub
End Class