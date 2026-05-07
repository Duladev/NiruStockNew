
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_ExpAssortSizing

    Private Sub ClearFields()
        cmbDept.Text = ""
        flxDetails.Rows.Clear()
        txtParNo.Text = ""
        txtPktNo.Text = ""
        txtPcs.Text = "0"
        txtCts.Text = "0"
        txtNewPcs.Text = ""
        txtNewCts.Text = ""
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        cmbAssort.Text = ""
        txtAssortment.Text = ""
        txtEmpNo.Text = ""
        txtPlanValue.Text = "0"
        txtValue.Text = "0"
        chkCont.Checked = False
        cmbDept.Focus()
    End Sub

    Private Sub ClearFields2()
        flxDetails.Rows.Clear()
        txtPktNo.Text = ""
        txtPcs.Text = "0"
        txtCts.Text = "0"
        txtNewPcs.Text = ""
        txtNewCts.Text = ""
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        cmbAssort.Text = ""
        txtAssortment.Text = ""
        txtPlanValue.Text = "0"
        txtValue.Text = "0"
        chkCont.Checked = False
        txtPktNo.Focus()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub Update_ListPrice()
        Dim intRow As Integer

        For intRow = 0 To flxDetails.Rows.Count - 1
            rsComSql_4 = New ADODB.Recordset
            rsComSql_4.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
            If rsComSql_4.RecordCount Then
                If Mid(flxDetails.Item(2, intRow).Value, 1, 1) = "S" Then
                    flxDetails.Item(5, intRow).Value = Math.Round((CDbl(flxDetails.Item(3, intRow).Value) * rsComSql_4.Fields("StonePrice").Value) / CDbl(flxDetails.Item(4, intRow).Value), 2)
                Else
                    flxDetails.Item(5, intRow).Value = Format(rsComSql_4.Fields("MarketPrice").Value, "#0.00")
                End If
            Else
                rsComSql_5 = New ADODB.Recordset
                rsComSql_5.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
                If rsComSql_5.RecordCount Then
                    flxDetails.Item(5, intRow).Value = Format(rsComSql_5.Fields("ListCost").Value, "#0.00")
                End If
                rsComSql_5 = Nothing
            End If
            rsComSql_4 = Nothing
            flxDetails.Item(7, intRow).Value = Math.Round(CDbl(flxDetails.Item(4, intRow).Value) * CDbl(flxDetails.Item(5, intRow).Value), 2)
        Next
        txtValue.Text = CalTotalValue()
    End Sub

    Private Function ParcelFound(ByVal strDept As String, ByVal strParceNo As String) As Boolean
        ParcelFound = True
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblGradingTrf WHERE ParcelNo = '" & strParceNo & "' AND Department = '" & strDept & "' AND Status = 1 AND Opening = 1", AdoCN, 1, 1)
        If rsComSql_1.RecordCount > 0 Then
            ParcelFound = True
        Else
            ParcelFound = False
        End If
        rsComSql_1 = Nothing

    End Function

    Private Function CalTotalPcs() As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + CInt(flxDetails.Item(3, intRow).Value)
        Next
        Return CalTotalPcs
    End Function

    Private Function CalTotalCts() As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            CalTotalCts = CalTotalCts + CDbl(flxDetails.Item(4, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
        Return CalTotalCts
    End Function

    Private Function CalTotalValue() As Double
        Dim intRow As Integer

        CalTotalValue = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            CalTotalValue = CalTotalValue + CDbl(flxDetails.Item(7, intRow).Value)
        Next
        CalTotalValue = Math.Round(CalTotalValue, 2)
        Return CalTotalValue
    End Function

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtParNo.Text = UCase(txtParNo.Text)
            If ParcelFound(cmbDept.Text, txtParNo.Text) = True Then
                txtPktNo.Text = ""
                txtPktNo.Focus()
            Else
                MsgBox("Department and Parcel No. not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtParNo.Text = ""
                txtPktNo.Text = ""
                txtParNo.Focus()
            End If
        End If
    End Sub

    Private Sub txtPktNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtParNo.Text = UCase(txtParNo.Text)
            txtPktNo.Text = UCase(txtPktNo.Text)
            If ParcelFound(cmbDept.Text, txtParNo.Text) = True Then
                Load_ParcelDetails()
            Else
                MsgBox("Department and Parcel No. not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtParNo.Text = ""
                txtPktNo.Text = ""
                txtParNo.Focus()
            End If
        End If
    End Sub

    Private Sub Load_AssortCode()
        cmbAssort.Items.Clear()
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT TOP (100) PERCENT LEFT(Assortment, 3) AS Cat FROM dbo.tblAssortList WHERE (Type = 'A') GROUP BY LEFT(Assortment, 3) ORDER BY Cat", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                cmbAssort.Items.Add(rsComSql_1.Fields("Cat").Value)
                rsComSql_1.MoveNext()
            End While
        End If
        rsComSql_1 = Nothing

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT TOP (100) PERCENT LEFT(Assortment, 6) AS Cat FROM dbo.tblAssortList WHERE (NewType = 'A') GROUP BY LEFT(Assortment, 6) ORDER BY LEFT(Assortment, 6)", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                cmbAssort.Items.Add(rsComSql_1.Fields("Cat").Value)
                rsComSql_1.MoveNext()
            End While
        End If
        rsComSql_1 = Nothing

        cmbAssort.Sorted = True
    End Sub

    Private Sub Load_ParcelDetails()
        Dim rsComSql_1 As New ADODB.Recordset
        Dim blnFound As Boolean

        flxDetails.Rows.Clear()
        blnFound = False

        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblGradingTrf WHERE Department = '" & cmbDept.Text & "' AND ParcelNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Status = 1 AND Opening = 1", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            txtTotPcs.Text = rsComSql_1.Fields("Trf_Pcs").Value
            txtTotCts.Text = rsComSql_1.Fields("Trf_Cts").Value
        Else
            MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql_1 = Nothing

        txtPlanValue.Text = "0"
        rsComSql_1 = New ADODB.Recordset
        Select Case cmbDept.Text
            Case "Princess"
                rsComSql_1.Open("SELECT PlanVal FROM dbo.tblPRPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
            Case "Baguettes"
                rsComSql_1.Open("SELECT PlanVal FROM dbo.tblBAGPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
            Case "Rounds"
                rsComSql_1.Open("SELECT PlanVal FROM dbo.tblRndPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
            Case "Rounds3", "Rounds4", "Rounds6", "Rounds7", "Emerald", "Lamour", "Davinci", "Carrer", "Opening", "Princess2", "Baguettes2", "Baguettes3", "Emerald2", "Emerald3", "Lamour2", "Asscher", "Radiant"
                rsComSql_1.Open("SELECT PlanVal FROM dbo.tblExtPacket WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
            Case Else
                MsgBox("Sorting Sizing is not entitle for this department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
        End Select
        If rsComSql_1.RecordCount Then
            txtPlanValue.Text = rsComSql_1.Fields("PlanVal").Value
        End If
        rsComSql_1 = Nothing

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblExpSizingPlan " & _
                        "WHERE (ParNo = '" & txtParNo.Text & "') AND (PktNo = '" & txtPktNo.Text & "') AND (Department = '" & cmbDept.Text & "') " & _
                        "ORDER BY ReturnType", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            blnFound = True
            While Not rsComSql_1.EOF
                flxDetails.Rows.Add(rsComSql_1.Fields("ParNo").Value,
                                    rsComSql_1.Fields("PktNo").Value,
                                    rsComSql_1.Fields("ReturnType").Value,
                                    rsComSql_1.Fields("Pcs").Value,
                                    rsComSql_1.Fields("Cts").Value,
                                    rsComSql_1.Fields("BasePrice").Value,
                                    rsComSql_1.Fields("ID").Value,
                                    Math.Round(rsComSql_1.Fields("Cts").Value * rsComSql_1.Fields("BasePrice").Value, 2))

                rsComSql_1.MoveNext()
            End While
        End If
        rsComSql_1 = Nothing

        txtPcs.Text = CalTotalPcs()
        txtCts.Text = CalTotalCts()
        txtValue.Text = CalTotalValue()

        cmbAssort.Focus()
    End Sub

    Private Sub cmbAssort_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbAssort.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbAssort.Text = UCase(cmbAssort.Text)
            txtAssortment.Focus()
        End If
    End Sub

    Private Sub txtAssortment_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAssortment.KeyPress
        Dim strAssortment As String

        'e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If cmbAssort.Text = "" Then
                MsgBox("Select the Assortment Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            strAssortment = cmbAssort.Text & Trim(txtAssortment.Text)

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & strAssortment & "' AND Active = 1", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                txtNewPcs.Focus()
            Else
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & strAssortment & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    txtNewPcs.Focus()
                Else
                    MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql_1 = Nothing
            End If
            rsComSql = Nothing
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

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim intRow As Integer
        Dim dblPrice As Double
        Dim strAssortment As String

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtAssortment.Text = "" Then MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtNewPcs.Text = "" Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtNewCts.Text = "" Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtNewPcs.Text) <= 0 Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtNewCts.Text) <= 0 Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If CDbl(txtPcs.Text) + CDbl(txtNewPcs.Text) > CDbl(txtTotPcs.Text) Then MsgBox("Invalid Total Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        'If CDbl(txtCts.Text) + CDbl(txtNewCts.Text) > CDbl(txtTotCts.Text) Then MsgBox("Invalid Total Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        'If CDbl(txtTotCts.Text) + 0.01 < Math.Round(CDbl(txtCts.Text) + CDbl(txtNewCts.Text), 3) Then
        '    MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '    Exit Sub
        'End If
        'If CDbl(txtTotCts.Text) - 0.01 > Math.Round(CDbl(txtCts.Text) + CDbl(txtNewCts.Text), 3) Then
        '    MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '    Exit Sub
        'End If

        strAssortment = UCase(cmbAssort.Text) & UCase(txtAssortment.Text)

        For intRow = 0 To flxDetails.Rows.Count - 1
            If strAssortment = flxDetails.Item(2, intRow).Value Then
                MsgBox("Assortment already entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        dblPrice = 0
        rsComSql_4 = New ADODB.Recordset
        rsComSql_4.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & strAssortment & "' AND Active = 1", AdoCN, 1, 1)
        If rsComSql_4.RecordCount Then
            If Mid(strAssortment, 1, 1) = "S" Then
                dblPrice = Math.Round((CDbl(txtNewPcs.Text) * rsComSql_4.Fields("StonePrice").Value) / CDbl(txtNewCts.Text), 2)
            Else
                dblPrice = Format(rsComSql_4.Fields("MarketPrice").Value, "#0.00")
            End If
        Else
            rsComSql_5 = New ADODB.Recordset
            rsComSql_5.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & strAssortment & "'", AdoCN, 1, 1)
            If rsComSql_5.RecordCount Then
                dblPrice = rsComSql_5.Fields("ListCost").Value
            Else
                MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql_5 = Nothing
        End If
        rsComSql_4 = Nothing

        flxDetails.Rows.Add(txtParNo.Text,
                            txtPktNo.Text,
                            strAssortment,
                            txtNewPcs.Text,
                            txtNewCts.Text,
                            Format(dblPrice, "#0.00"),
                            0,
                            Math.Round(CDbl(txtNewCts.Text) * dblPrice, 2))

        txtPcs.Text = CalTotalPcs()
        txtCts.Text = CalTotalCts()
        txtValue.Text = CalTotalValue()

        cmbAssort.Text = ""
        txtAssortment.Text = ""
        txtNewPcs.Text = ""
        txtNewCts.Text = ""

        cmbAssort.Focus()
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim dblPerc As Double

        txtPcs.Text = CalTotalPcs()
        txtCts.Text = CalTotalCts()
        txtValue.Text = CalTotalValue()

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtParNo.Text = "" Then MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtPktNo.Text = "" Then MsgBox("Invalid Packet No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If flxDetails.Rows.Count < 1 Then MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtPcs.Text) <> CDbl(txtTotPcs.Text) Then MsgBox("Pcs not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        'If CDbl(txtCts.Text) <> CDbl(txtTotCts.Text) Then MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If CDbl(txtTotCts.Text) + 0.01 < Math.Round(CDbl(txtCts.Text), 3) Then
            MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If CDbl(txtTotCts.Text) - 0.01 > Math.Round(CDbl(txtCts.Text), 3) Then
            MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If Len(txtEmpNo.Text) <> 6 Then MsgBox("Invalid Emp No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL3 WHERE FullEmpNo = '" & txtEmpNo.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Employee No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        Else
            txtEmpNo.Text = UCase(txtEmpNo.Text)
        End If
        rsComSql = Nothing

        dblPerc = 0
        If chkCont.Checked = False Then
            If CDbl(txtPlanValue.Text) > CDbl(txtValue.Text) Then
                dblPerc = Math.Round((CDbl(txtValue.Text) / CDbl(txtPlanValue.Text)) * 100, 2)
                If dblPerc < 90 Then
                    MsgBox("Plan Value not Achieved - " & dblPerc & "%", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
        End If

        For intRow = 0 To flxDetails.Rows.Count - 1
            rsComSql_4 = New ADODB.Recordset
            rsComSql_4.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & flxDetails.Item(2, intRow).Value & "' AND Active = 1", AdoCN, 1, 1)
            If rsComSql_4.RecordCount Then
                If Mid(flxDetails.Item(2, intRow).Value, 1, 1) = "S" Then
                    flxDetails.Item(5, intRow).Value = Math.Round((CDbl(flxDetails.Item(3, intRow).Value) * rsComSql_4.Fields("StonePrice").Value) / CDbl(flxDetails.Item(4, intRow).Value), 2)
                Else
                    flxDetails.Item(5, intRow).Value = Format(rsComSql_4.Fields("MarketPrice").Value, "#0.00")
                End If
            Else
                rsComSql_5 = New ADODB.Recordset
                rsComSql_5.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
                If rsComSql_5.RecordCount Then
                    flxDetails.Item(5, intRow).Value = Format(rsComSql_5.Fields("ListCost").Value, "#0.00")
                Else
                    MsgBox("Invalid Assortment - " & flxDetails.Item(2, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql_5 = Nothing
            End If
            rsComSql_4 = Nothing
        Next

        dtpToday = GetToday()

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblExpSizingPlan WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            PBResponse = MsgBox("Are you sure to Update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                AdoCN.Execute("DELETE FROM tblExpSizingPlan WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'")
                For intRow = 0 To flxDetails.Rows.Count - 1
                    AdoCN.Execute("INSERT INTO tblExpSizingPlan(Department,ParNo,PktNo,ReturnType,Pcs,Cts,OK,BasePrice,EmpNo,AddUser,AddDate,AddTime) " & _
                                  "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "','" & UCase(flxDetails.Item(2, intRow).Value) & "'," & _
                                    "" & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",0," & CDbl(flxDetails.Item(5, intRow).Value) & "," & _
                                    "'" & txtEmpNo.Text & "','" & PBUser_EmpNo & "','" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")
                Next

                MsgBox("Updated Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            For intRow = 0 To flxDetails.Rows.Count - 1
                AdoCN.Execute("INSERT INTO tblExpSizingPlan(Department,ParNo,PktNo,ReturnType,Pcs,Cts,OK,BasePrice,EmpNo,AddUser,AddDate,AddTime) " & _
                              "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "','" & UCase(flxDetails.Item(2, intRow).Value) & "'," & _
                                "" & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",0," & CDbl(flxDetails.Item(5, intRow).Value) & "," & _
                                "'" & txtEmpNo.Text & "','" & PBUser_EmpNo & "','" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")
            Next

            MsgBox("Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing

        ClearFields2()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdUpdate_Click(sender As Object, e As EventArgs) Handles cmdUpdate.Click
        PBResponse = MsgBox("Are you sure to Update the List Price?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            Update_ListPrice()
        End If
    End Sub

    Private Sub Delete()
        PBResponse = MsgBox("Are you sure to Delete this packet?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblExpSizingPlan WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                AdoCN.Execute("DELETE FROM tblExpSizingPlan WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'")

                MsgBox("Deleted Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Else
                MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            rsComSql = Nothing

            ClearFields()
        End If
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Delete()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub cmbDept_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbDept.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtParNo.Focus()
        End If
    End Sub

    Private Sub Update_Assortment()
        Dim intRow As Integer

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtParNo.Text = "" Then MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtPktNo.Text = "" Then MsgBox("Invalid Packet No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If flxDetails.Rows.Count < 1 Then MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblExpSizingPlan WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            PBResponse = MsgBox("Are you sure to Update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                For intRow = 0 To flxDetails.Rows.Count - 1
                    rsComSql_4 = New ADODB.Recordset
                    rsComSql_4.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & flxDetails.Item(2, intRow).Value & "' AND Active = 1", AdoCN, 1, 1)
                    If rsComSql_4.RecordCount Then

                    Else
                        rsComSql_5 = New ADODB.Recordset
                        rsComSql_5.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
                        If rsComSql_5.RecordCount Then

                        Else
                            MsgBox("Invalid Assortment - " & flxDetails.Item(2, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                        rsComSql_5 = Nothing
                    End If
                    rsComSql_4 = Nothing
                Next
                Update_ListPrice()

                For intRow = 0 To flxDetails.Rows.Count - 1
                    AdoCN.Execute("UPDATE tblExpSizingPlan SET ReturnType = '" & UCase(flxDetails.Item(2, intRow).Value) & "', BasePrice = " & CDbl(flxDetails.Item(5, intRow).Value) & " WHERE ID = " & CDbl(flxDetails.Item(6, intRow).Value) & "")
                Next

                MsgBox("Updated Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing

        ClearFields()
    End Sub

    Private Sub cmdUpdateAssort_Click(sender As Object, e As EventArgs) Handles cmdUpdateAssort.Click
        Update_Assortment()
    End Sub

    Private Sub frm_ExpAssortSizing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_DepartmentProd(cmbDept)
        Load_AssortCode()

        ClearFields()
    End Sub

    Private Sub flxDetails_DoubleClick(sender As Object, e As EventArgs) Handles flxDetails.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)

            txtPcs.Text = CalTotalPcs()
            txtCts.Text = CalTotalCts()
            txtValue.Text = CalTotalValue()
        End If
    End Sub
End Class