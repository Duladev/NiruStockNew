
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_ExpAssortModify

    Private Sub frm_ExpAssortModify_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_DepartmentProd(cmbDept)

        txtPcs.Text = "0"
        txtCts.Text = "0"
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
    End Sub

    Private Sub ClearFields()
        cmbDept.Text = ""
        flxDetails.Rows.Clear()
        txtParNo.Text = ""
        txtPktNo.Text = ""
        txtPcs.Text = "0"
        txtCts.Text = "0"
        txtNewPcs.Text = ""
        txtNewCts.Text = ""
        txtEstCts.Text = ""
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        cmbAssort.Items.Clear()
        cmbAssort.Text = ""
        txtAssortment.Text = ""
        txtClarity.Text = ""
        txtRejPcs.Text = "0"
        txtRejCts.Text = "0"
        txtRejPcs1.Text = "0"
        txtRejCts1.Text = "0"
        txtTrfPcs.Text = "0"
        txtTrfCts.Text = "0"
        txtOrigin.Text = ""
        txtOCode.Text = ""
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub Update_ListPrice()
        Dim intRow As Integer
        Dim dblStonePrice As Double
        Dim dblPrice As Double

        For intRow = 0 To flxDetails.Rows.Count - 1
            dblStonePrice = 0
            dblPrice = 0
            rsComSql_4 = New ADODB.Recordset
            rsComSql_4.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
            If rsComSql_4.RecordCount Then
                If Mid(flxDetails.Item(2, intRow).Value, 1, 1) = "S" Or Mid(flxDetails.Item(2, intRow).Value, 1, 1) = "R" Or Mid(flxDetails.Item(2, intRow).Value, 7, 1) = "R" Or Mid(flxDetails.Item(2, intRow).Value, 7, 1) = "S" Then
                    If cmbDept.Text = "Baguettes" Then
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT dbo.tblExpSizingPacket.Department, dbo.tblExpSizingPacket.ParNo, dbo.tblExpSizingPacket.PktNo, dbo.tblExpSizingPacket.ReturnType, dbo.tblBAGPacket.PlanVal, dbo.tblBAGPacket.PktPcs, dbo.tblBAGPacket.PktCts " & _
                                        "FROM dbo.tblExpSizingPacket INNER JOIN dbo.tblBAGPacket ON dbo.tblExpSizingPacket.ParNo = dbo.tblBAGPacket.ParNo AND dbo.tblExpSizingPacket.ReturnType = dbo.tblBAGPacket.PktNo " & _
                                        "WHERE (dbo.tblExpSizingPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblExpSizingPacket.PktNo = '" & txtPktNo.Text & "')", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            If strRight(txtParNo.Text, 1) = "C" Then
                                dblStonePrice = Math.Round(rsComSql_1.Fields("PlanVal").Value * 1.15 / rsComSql_1.Fields("PktPcs").Value, 2)
                            Else
                                dblStonePrice = Math.Round(rsComSql_1.Fields("PlanVal").Value / rsComSql_1.Fields("PktPcs").Value, 2)
                            End If
                            dblPrice = Math.Round((dblStonePrice * CInt(flxDetails.Item(3, intRow).Value)) / CDbl(flxDetails.Item(4, intRow).Value), 2)
                            flxDetails.Item(5, intRow).Value = Format(dblPrice, "#0.00")
                        End If
                        rsComSql_1 = Nothing
                    End If

                    If cmbDept.Text = "Princess" Then
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT dbo.tblExpSizingPacket.Department, dbo.tblExpSizingPacket.ParNo, dbo.tblExpSizingPacket.PktNo, dbo.tblExpSizingPacket.ReturnType, dbo.tblPRPacket.PlanVal, dbo.tblPRPacket.PktPcs, dbo.tblPRPacket.PktCts " & _
                                        "FROM dbo.tblExpSizingPacket INNER JOIN dbo.tblPRPacket ON dbo.tblExpSizingPacket.ParNo = dbo.tblPRPacket.ParNo AND dbo.tblExpSizingPacket.ReturnType = dbo.tblPRPacket.PktNo " & _
                                        "WHERE (dbo.tblExpSizingPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblExpSizingPacket.PktNo = '" & txtPktNo.Text & "')", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            If strRight(txtParNo.Text, 1) = "C" Then
                                dblStonePrice = Math.Round(rsComSql_1.Fields("PlanVal").Value * 1.15 / rsComSql_1.Fields("PktPcs").Value, 2)
                            Else
                                dblStonePrice = Math.Round(rsComSql_1.Fields("PlanVal").Value / rsComSql_1.Fields("PktPcs").Value, 2)
                            End If
                            dblPrice = Math.Round((dblStonePrice * CInt(flxDetails.Item(3, intRow).Value)) / CDbl(flxDetails.Item(4, intRow).Value), 2)
                            flxDetails.Item(5, intRow).Value = Format(dblPrice, "#0.00")
                        End If
                        rsComSql_1 = Nothing
                    End If

                    If cmbDept.Text = "Carrer" Or cmbDept.Text = "Opening" Or cmbDept.Text = "Radiant" Or cmbDept.Text = "Asscher" Then
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT dbo.tblExpSizingPacket.Department, dbo.tblExpSizingPacket.ParNo, dbo.tblExpSizingPacket.PktNo, dbo.tblExpSizingPacket.ReturnType, dbo.tblExtPacket.PlanVal, dbo.tblExtPacket.PktPcs, dbo.tblExtPacket.PktCts " & _
                                        "FROM dbo.tblExpSizingPacket INNER JOIN dbo.tblExtPacket ON dbo.tblExpSizingPacket.ParNo = dbo.tblExtPacket.ParNo AND dbo.tblExpSizingPacket.ReturnType = dbo.tblExtPacket.PktNo AND dbo.tblExpSizingPacket.Department = dbo.tblExtPacket.Department " & _
                                        "WHERE (dbo.tblExpSizingPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblExpSizingPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblExpSizingPacket.PktNo = '" & txtPktNo.Text & "')", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            If strRight(txtParNo.Text, 1) = "C" Then
                                dblStonePrice = Math.Round(rsComSql_1.Fields("PlanVal").Value * 1.15 / rsComSql_1.Fields("PktPcs").Value, 2)
                            Else
                                dblStonePrice = Math.Round(rsComSql_1.Fields("PlanVal").Value / rsComSql_1.Fields("PktPcs").Value, 2)
                            End If
                            dblPrice = Math.Round((dblStonePrice * CInt(flxDetails.Item(3, intRow).Value)) / CDbl(flxDetails.Item(4, intRow).Value), 2)
                            flxDetails.Item(5, intRow).Value = Format(dblPrice, "#0.00")
                        End If
                        rsComSql_1 = Nothing
                    End If
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
        Next

    End Sub

    Private Sub Update_ListPriceAll()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Department, ParNo, PktNo, PackNo FROM tblExpReExports WHERE(PackNo = 12422) ORDER BY PktNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF



                rsComSql.MoveNext()
            End While
        End If
    End Sub

    Private Function ParcelFound(ByVal strDept As String, ByVal strParceNo As String) As Boolean
        ParcelFound = True
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblExpSizingPacket WHERE ParNo = '" & strParceNo & "' AND Department = '" & strDept & "'", AdoCN, 1, 1)
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
            If flxDetails.Rows(intRow).Cells(6).EditedFormattedValue = True Then
                CalTotalPcs = CalTotalPcs + CInt(flxDetails.Item(3, intRow).Value)
            End If
        Next
        Return CalTotalPcs
    End Function

    Private Function CalTotalCts() As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Rows(intRow).Cells(6).EditedFormattedValue = True Then
                CalTotalCts = CalTotalCts + CDbl(flxDetails.Item(4, intRow).Value)
            End If
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
        Return CalTotalCts
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

    Private Sub Load_ParcelDetails()
        Dim rsComSql_1 As New ADODB.Recordset
        Dim blnFound As Boolean

        flxDetails.Rows.Clear()
        blnFound = False

        txtOrigin.Text = ""
        txtOCode.Text = ""
        If cmbDept.Text = "Mix" Then
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT Origin FROM dbo.tblImport WHERE SupParcelNo = '" & txtParNo.Text & "'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                txtOrigin.Text = rsComSql_1.Fields("Origin").Value
            End If
            rsComSql_1 = Nothing
        Else
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT dbo.tblImportOGL.MiningCompany " & _
                            "FROM dbo.tblImport INNER JOIN dbo.tblParcel ON dbo.tblImport.SupParcelNo = dbo.tblParcel.OrigParcelNo INNER JOIN " & _
                                "dbo.tblImportOGL ON dbo.tblImport.NewLotNo = dbo.tblImportOGL.MasterLotID " & _
                            "WHERE (dbo.tblParcel.GrpParNo = '" & txtParNo.Text & "') AND (dbo.tblParcel.Depart = '" & cmbDept.Text & "')", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                txtOrigin.Text = rsComSql_1.Fields("MiningCompany").Value
            End If
            rsComSql_1 = Nothing
        End If

        If txtOrigin.Text <> "" Then
            If cmbDept.Text = "Mix" Then
                Select Case txtOrigin.Text
                    Case "De Beers"
                        txtOCode.Text = "ADT"
                    Case "Rio Tinto"
                        txtOCode.Text = "ART"
                    Case "Arctic Canadian"
                        txtOCode.Text = "AAC"
                    Case "Debswana"
                        txtOCode.Text = "AOD"
                    Case "Stargems Group"
                        txtOCode.Text = "ASG"
                End Select
            Else
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
        End If

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.tblExpSizingPacket.Department, dbo.tblExpSizingPacket.ParNo, dbo.tblExpSizingPacket.PktNo, dbo.tblExpSizingPacket.ReturnType, " & _
                            "dbo.tblExpSizingTypes.ReturnType AS Assortment, dbo.tblExpSizingTypes.Pcs, dbo.tblExpSizingTypes.Cts, dbo.tblExpSizingTypes.EstCts, dbo.tblExpSizingTypes.BasePrice, dbo.tblExpSizingTypes.ID " & _
                        "FROM dbo.tblExpSizingPacket INNER JOIN dbo.tblExpSizingTypes ON dbo.tblExpSizingPacket.Department = dbo.tblExpSizingTypes.Department AND " & _
                            "dbo.tblExpSizingPacket.ParNo = dbo.tblExpSizingTypes.ParNo AND dbo.tblExpSizingPacket.PktNo = dbo.tblExpSizingTypes.PktNo " & _
                        "WHERE (dbo.tblExpSizingPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblExpSizingPacket.PktNo = '" & txtPktNo.Text & "') AND (dbo.tblExpSizingPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblExpSizingTypes.OK = 0) " & _
                        "ORDER BY Assortment", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            txtClarity.Text = rsComSql_1.Fields("ReturnType").Value
            blnFound = True
            While Not rsComSql_1.EOF
                flxDetails.Rows.Add(rsComSql_1.Fields("ParNo").Value,
                                    rsComSql_1.Fields("PktNo").Value,
                                    rsComSql_1.Fields("Assortment").Value,
                                    rsComSql_1.Fields("Pcs").Value,
                                    rsComSql_1.Fields("Cts").Value,
                                    rsComSql_1.Fields("BasePrice").Value,
                                    True,
                                    rsComSql_1.Fields("ID").Value,
                                    rsComSql_1.Fields("EstCts").Value)

                rsComSql_1.MoveNext()
            End While
        Else
            MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql_1 = Nothing

        cmbAssort.Items.Clear()
        If blnFound = True Then
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblExpSizingAssort WHERE Clarity = '" & txtClarity.Text & "' ORDER BY AssortCode", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                rsComSql_1.MoveFirst()
                While Not rsComSql_1.EOF
                    cmbAssort.Items.Add(rsComSql_1.Fields("AssortCode").Value)
                    rsComSql_1.MoveNext()
                End While
            End If
            rsComSql_1 = Nothing
        End If

        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblExpSizingIssues WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            txtTotPcs.Text = rsComSql_1.Fields("IssPcs").Value
            txtTotCts.Text = rsComSql_1.Fields("IssCts").Value
        End If
        rsComSql_1 = Nothing

        txtRejPcs.Text = "0"
        txtRejCts.Text = "0"
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblExpSizingReturns WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            txtRejPcs.Text = rsComSql_1.Fields("RepPcs").Value
            txtRejCts.Text = rsComSql_1.Fields("RepCts").Value
            txtRejPcs1.Text = rsComSql_1.Fields("RepPcs").Value
            txtRejCts1.Text = rsComSql_1.Fields("RepCts").Value
        End If
        rsComSql_1 = Nothing

        txtTrfPcs.Text = "0"
        txtTrfCts.Text = "0"
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT SUM(Pcs) AS Pcs, SUM(Cts) AS Cts FROM tblExpSizingTypes WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND OK = 1", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                txtTrfPcs.Text = rsComSql_1.Fields("Pcs").Value
                txtTrfCts.Text = Math.Round(rsComSql_1.Fields("Cts").Value, 3)
            End If
        End If
        rsComSql_1 = Nothing

        txtPcs.Text = CalTotalPcs()
        txtCts.Text = CalTotalCts()
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
                If Mid(strAssortment, 1, 1) = "A" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM tblExpSizingAssort WHERE Clarity = '" & txtClarity.Text & "' AND AssortCode = '" & Mid(strAssortment, 1, 3) & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount = 0 Then
                        MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql_1 = Nothing
                End If
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
            txtEstCts.Text = txtNewCts.Text
            txtNewCts.Focus()
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim intRow As Integer
        Dim dblPrice As Double

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbAssort.Text = "" Then MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtAssortment.Text = "" Then MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtNewPcs.Text = "" Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtNewCts.Text = "" Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtEstCts.Text = "" Then MsgBox("Invalid Est Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtNewPcs.Text) <= 0 Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtNewCts.Text) <= 0 Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtEstCts.Text) <= 0 Then MsgBox("Invalid Est Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If CDbl(txtPcs.Text) + CDbl(txtNewPcs.Text) + CDbl(txtRejPcs.Text) > CDbl(txtTotPcs.Text) Then MsgBox("Invalid Total Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If CDbl(txtTotCts.Text) + 0.15 < Math.Round(CDbl(txtCts.Text) + CDbl(txtRejCts.Text) + CDbl(txtNewCts.Text), 3) Then
            MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If CDbl(txtTotCts.Text) - 0.15 > Math.Round(CDbl(txtCts.Text) + CDbl(txtRejCts.Text) + CDbl(txtNewCts.Text), 3) Then
            MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        For intRow = 0 To flxDetails.Rows.Count - 1
            If cmbAssort.Text & txtAssortment.Text = flxDetails.Item(2, intRow).Value Then
                MsgBox("Assortment already entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        dblPrice = 0
        rsComSql_4 = New ADODB.Recordset
        rsComSql_4.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & cmbAssort.Text & txtAssortment.Text & "' AND Active = 1", AdoCN, 1, 1)
        If rsComSql_4.RecordCount Then
            If Mid(cmbAssort.Text & txtAssortment.Text, 1, 1) = "S" Then
                dblPrice = Math.Round((CDbl(txtNewPcs.Text) * rsComSql_4.Fields("StonePrice").Value) / CDbl(txtNewCts.Text), 2)
            Else
                dblPrice = Format(rsComSql_4.Fields("MarketPrice").Value, "#0.00")
            End If

            If rsComSql.Fields("Origin").Value <> "" Then
                If txtOCode.Text <> Mid(cmbAssort.Text & txtAssortment.Text, 1, 3) Then
                    MsgBox("Invalid Assortment Origin", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
        Else
            rsComSql_5 = New ADODB.Recordset
            rsComSql_5.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & cmbAssort.Text & txtAssortment.Text & "'", AdoCN, 1, 1)
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
                            UCase(cmbAssort.Text) & UCase(txtAssortment.Text),
                            txtNewPcs.Text,
                            txtNewCts.Text,
                            Format(dblPrice, "#0.00"),
                            True,
                            0,
                            txtEstCts.Text)

        txtPcs.Text = CalTotalPcs()
        txtCts.Text = CalTotalCts()

        cmbAssort.Text = ""
        txtAssortment.Text = ""
        txtNewPcs.Text = ""
        txtNewCts.Text = ""
        txtEstCts.Text = ""

        cmbAssort.Focus()
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        txtPcs.Text = CalTotalPcs()
        txtCts.Text = CalTotalCts()

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtParNo.Text = "" Then MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtPktNo.Text = "" Then MsgBox("Invalid Packet No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If flxDetails.Rows.Count < 1 Then MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtRejPcs.Text = "" Then MsgBox("Invalid Reject Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtRejCts.Text = "" Then MsgBox("Invalid Reject Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtPcs.Text) + CDbl(txtRejPcs.Text) + CDbl(txtTrfPcs.Text) <> CDbl(txtTotPcs.Text) Then MsgBox("Pcs not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        'If CDbl(txtTotCts.Text) + 0.15 < Math.Round(CDbl(txtCts.Text) + CDbl(txtRejCts.Text), 3) Then
        '    MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '    Exit Sub
        'End If
        'If CDbl(txtTotCts.Text) - 0.15 > Math.Round(CDbl(txtCts.Text) + CDbl(txtRejCts.Text), 3) Then
        '    MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        '    Exit Sub
        'End If

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

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblExpSizingTypes WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND OK = 0", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            PBResponse = MsgBox("Are you sure to Update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                AdoCN.Execute("UPDATE tblExpSizingReturns SET RetPcs = '" & CDbl(txtPcs.Text) + CDbl(txtTrfPcs.Text) & "',RetCts = '" & CDbl(txtCts.Text) + CDbl(txtTrfCts.Text) & "',RepPcs = '" & CDbl(txtRejPcs.Text) & "',RepCts = '" & CDbl(txtRejCts.Text) & "' WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'")
                AdoCN.Execute("DELETE FROM tblExpSizingTypes WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND OK = 0")
                For intRow = 0 To flxDetails.Rows.Count - 1
                    If flxDetails.Item(6, intRow).Value = True Then
                        AdoCN.Execute("INSERT INTO tblExpSizingTypes(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts,OK,BasePrice,EstCts) " & _
                                      "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "',1,'" & UCase(flxDetails.Item(2, intRow).Value) & "'," & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",0," & CDbl(flxDetails.Item(5, intRow).Value) & "," & CDbl(flxDetails.Item(8, intRow).Value) & ")")

                    Else
                        If CDbl(txtRejPcs.Text) > CDbl(txtRejPcs1.Text) Then
                            AdoCN.Execute("INSERT INTO tblExpSizingRejects(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts,OK,BasePrice) " & _
                                          "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "',1,'" & UCase(flxDetails.Item(2, intRow).Value) & "'," & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",0," & CDbl(flxDetails.Item(5, intRow).Value) & ")")
                        End If
                    End If
                Next

                MsgBox("Updated Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing

        ClearFields()
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
            rsComSql.Open("SELECT * FROM tblExpSizingTypes WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND OK = 0", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                AdoCN.Execute("DELETE FROM tblExpSizingReturns WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'")
                AdoCN.Execute("DELETE FROM tblExpSizingTypes WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND OK = 0")
                AdoCN.Execute("DELETE FROM tblExpSizingRejects WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND OK = 0")
                AdoCN.Execute("DELETE FROM tblExpSizingReturnsEmp WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'")

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

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 6 Then
            txtPcs.Text = CalTotalPcs()
            txtCts.Text = CalTotalCts()
        End If
    End Sub

    Private Sub flxDetails_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellValueChanged
        txtPcs.Text = CalTotalPcs()
        txtCts.Text = CalTotalCts()
    End Sub

    Private Sub txtRejPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRejPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtRejCts.Focus()
        End If
    End Sub

    Private Sub txtRejCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRejCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRejCts.Text)
    End Sub

    Private Sub Update_Assortment()
        Dim intRow As Integer

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtParNo.Text = "" Then MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtPktNo.Text = "" Then MsgBox("Invalid Packet No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If flxDetails.Rows.Count < 1 Then MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblExpSizingTypes WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND OK = 0", AdoCN, 1, 1)
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
                    AdoCN.Execute("UPDATE tblExpSizingTypes SET ReturnType = '" & UCase(flxDetails.Item(2, intRow).Value) & "', BasePrice = " & CDbl(flxDetails.Item(5, intRow).Value) & " WHERE ID = " & CDbl(flxDetails.Item(7, intRow).Value) & "")
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

    Private Sub txtEstCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEstCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtEstCts.Text)
        If Asc(e.KeyChar) = 13 Then
            cmdAdd.Focus()
        End If
    End Sub
End Class