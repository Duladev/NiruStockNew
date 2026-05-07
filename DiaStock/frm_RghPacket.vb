
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RghPacket

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_Types()
        cmbType.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblRghSections ORDER BY SecCode", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbType.Items.Add(rsComSql.Fields("SecName").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Color()
        Dim rsGrdType As New ADODB.Recordset

        cmbColor.Items.Clear()
        rsGrdType = New ADODB.Recordset
        rsGrdType.Open("SELECT * FROM tblRghTypes WHERE Sec = 2 ORDER BY Type", AdoCN, 1, 1)
        If rsGrdType.RecordCount Then
            rsGrdType.MoveFirst()
            While Not rsGrdType.EOF
                cmbColor.Items.Add(rsGrdType.Fields("Type").Value)
                rsGrdType.MoveNext()
            End While
        End If
        rsGrdType = Nothing
    End Sub

    Private Sub Load_Flo()
        Dim rsGrdType As New ADODB.Recordset

        cmbFlo.Items.Clear()
        rsGrdType = New ADODB.Recordset
        rsGrdType.Open("SELECT * FROM tblRghTypes WHERE Sec = 3 ORDER BY Type", AdoCN, 1, 1)
        If rsGrdType.RecordCount Then
            rsGrdType.MoveFirst()
            While Not rsGrdType.EOF
                cmbFlo.Items.Add(rsGrdType.Fields("Type").Value)
                rsGrdType.MoveNext()
            End While
        End If
        rsGrdType = Nothing
    End Sub

    Private Sub Load_Clarity()
        Dim rsGrdType As New ADODB.Recordset

        cmbClarity.Items.Clear()
        rsGrdType = New ADODB.Recordset
        rsGrdType.Open("SELECT * FROM tblRghTypes WHERE Sec = 4 ORDER BY Type", AdoCN, 1, 1)
        If rsGrdType.RecordCount Then
            rsGrdType.MoveFirst()
            While Not rsGrdType.EOF
                cmbClarity.Items.Add(rsGrdType.Fields("Type").Value)
                rsGrdType.MoveNext()
            End While
        End If
        rsGrdType = Nothing
    End Sub

    Private Sub Load_Model()
        Dim rsGrdType As New ADODB.Recordset

        cmbModel.Items.Clear()
        rsGrdType = New ADODB.Recordset
        rsGrdType.Open("SELECT * FROM tblRghTypes WHERE Sec = 5 ORDER BY Type", AdoCN, 1, 1)
        If rsGrdType.RecordCount Then
            rsGrdType.MoveFirst()
            While Not rsGrdType.EOF
                cmbModel.Items.Add(rsGrdType.Fields("Type").Value)
                rsGrdType.MoveNext()
            End While
        End If
        rsGrdType = Nothing
    End Sub

    Private Sub frm_RghPacket_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Types()
        Load_Color()
        Load_Flo()
        Load_Clarity()
        Load_Model()

        ClearFields()
    End Sub

    Private Sub ClearFields()
        txtParNo.Text = ""
        txtPktNo.Text = ""
        txtPktCts.Text = ""
        txtPktPcs.Text = ""
        txtPktCts.Text = ""
        txtIssPcs.Text = ""
        txtIssCts.Text = ""
        txtAPcs.Text = ""
        txtACts.Text = ""
        cmbType.SelectedIndex = 0
        flxPacket.Rows.Clear()
        flxDetails.Rows.Clear()
        txtPrice.Text = "0"
        txtAvgPrice.Text = ""
        txtSubPcs.Text = "0"
        txtSubCts.Text = "0"
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtParNo.Text = UCase(txtParNo.Text)
            If ParcelFound(txtParNo.Text) = True Then
                txtParNo.Text = UCase(txtParNo.Text)
                GetNewPacket()
                txtPktPcs.Focus()
            Else
                MsgBox("Invalid Parcel", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
                txtParNo.Focus()
            End If
        End If
    End Sub

    Private Function ParcelFound(ByVal strParceNo As String) As Boolean

        ParcelFound = True
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblParcel WHERE GrpParNo = '" & strParceNo & "' AND Depart = 'Rough Planning'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount > 0 Then
            ParcelFound = True
        Else
            ParcelFound = False
        End If
        rsComSql_1 = Nothing

    End Function

    Private Sub GetNewPacket()
        Dim dblIssPcs As Double
        Dim dblIssCts As Double
        Dim dblTotValue As Double

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT MAX(PktNo) AS MaxPktNo FROM tblRghPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktType = " & cmbType.SelectedIndex + 1 & "", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            If Not IsDBNull(rsComSql_1.Fields("MaxPktNo").Value) Then
                'txtPktNo.Text = Format(rsComSql_1.Fields("MaxPktNo").Value + 1, "000")
                txtPktNo.Text = "001"
            Else
                txtPktNo.Text = "001"
            End If
        End If
        rsComSql_1 = Nothing

        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        flxDetails.Rows.Clear()
        If cmbType.SelectedIndex + 1 = 1 Then
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT SUM(IssuedPcs) AS TotPcs, ROUND(SUM(IssuedCts), 3) AS TotCts FROM tblParcel WHERE GrpParNo = '" & txtParNo.Text & "' AND Depart = 'Rough Planning'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                If Not IsDBNull(rsComSql_1.Fields("TotPcs").Value) Then
                    txtTotPcs.Text = rsComSql_1.Fields("TotPcs").Value
                    txtTotCts.Text = rsComSql_1.Fields("TotCts").Value
                Else
                    txtTotPcs.Text = "0"
                    txtTotCts.Text = "0"
                End If
            End If
            rsComSql_1 = Nothing
        Else
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT SUM(RetPcs) AS TotPcs, ROUND(SUM(RetCts), 3) AS TotCts FROM tblRghReturns WHERE ParNo = '" & txtParNo.Text & "' AND Sec = " & cmbType.SelectedIndex & "", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                If Not IsDBNull(rsComSql_1.Fields("TotPcs").Value) Then
                    txtTotPcs.Text = rsComSql_1.Fields("TotPcs").Value
                    txtTotCts.Text = rsComSql_1.Fields("TotCts").Value
                Else
                    txtTotPcs.Text = "0"
                    txtTotCts.Text = "0"
                End If
            End If
            rsComSql_1 = Nothing
        End If

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.tblRghPacket.ID, dbo.tblRghPacket.ParNo, dbo.tblRghReturnDetails.Boiling, dbo.tblRghReturnDetails.Color, dbo.tblRghReturnDetails.Clarity, " & _
                            "dbo.tblRghReturnDetails.Flo,dbo.tblRghReturnDetails.Model, dbo.tblRghPacket.PktPrice, SUM(dbo.tblRghReturnDetails.Pcs) AS TotPcs, ROUND(SUM(dbo.tblRghReturnDetails.Cts), 3) AS TotCts, " & _
                            "dbo.tblRghSections.SecName,dbo.tblRghReturnDetails.ID AS ID2 " & _
                        "FROM dbo.tblRghReturnDetails INNER JOIN dbo.tblRghPacket ON dbo.tblRghReturnDetails.ParNo = dbo.tblRghPacket.ParNo AND dbo.tblRghReturnDetails.PktNo = dbo.tblRghPacket.PktNo AND " & _
                            "dbo.tblRghReturnDetails.Sec = dbo.tblRghPacket.PktType INNER JOIN dbo.tblRghSections ON dbo.tblRghPacket.PktType = dbo.tblRghSections.SecCode " & _
                        "WHERE (dbo.tblRghReturnDetails.Sec = 6) AND (dbo.tblRghPacket.ParNo = '" & txtParNo.Text & "') " & _
                        "GROUP BY dbo.tblRghPacket.ID, dbo.tblRghPacket.ParNo, dbo.tblRghSections.SecName, dbo.tblRghReturnDetails.Boiling, dbo.tblRghReturnDetails.Color, dbo.tblRghReturnDetails.Clarity, " & _
                            "dbo.tblRghReturnDetails.Flo,dbo.tblRghReturnDetails.Model, dbo.tblRghPacket.PktPrice,dbo.tblRghReturnDetails.ID " & _
                        "ORDER BY dbo.tblRghReturnDetails.Boiling, dbo.tblRghReturnDetails.Color, dbo.tblRghReturnDetails.Clarity, dbo.tblRghReturnDetails.Flo,dbo.tblRghReturnDetails.Model", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                flxDetails.Rows.Add(rsComSql_1.Fields("ParNo").Value,
                                    rsComSql_1.Fields("Boiling").Value,
                                    rsComSql_1.Fields("Color").Value,
                                    rsComSql_1.Fields("Clarity").Value,
                                    rsComSql_1.Fields("Flo").Value,
                                    rsComSql_1.Fields("Model").Value,
                                    rsComSql_1.Fields("TotPcs").Value,
                                    rsComSql_1.Fields("TotCts").Value,
                                    rsComSql_1.Fields("PktPrice").Value,
                                    rsComSql_1.Fields("ID").Value,
                                    rsComSql_1.Fields("ID2").Value)
                rsComSql_1.MoveNext()
            End While
        End If
        rsComSql_1 = Nothing
        txtSubPcs.Text = CalTotalPcs(flxDetails)
        txtSubCts.Text = CalTotalCts(flxDetails)

        txtPktPcs.Text = txtSubPcs.Text
        txtPktCts.Text = txtSubCts.Text

        flxPacket.Rows.Clear()
        dblIssPcs = 0
        dblIssCts = 0
        dblTotValue = 0
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.tblRghPacket.ID, dbo.tblRghPacket.ParNo, dbo.tblRghPacket.PktNo, dbo.tblRghPacket.PktPcs, dbo.tblRghPacket.PktCts, " & _
                            "dbo.tblRghPacket.PktType, dbo.tblRghPacket.PktColor, dbo.tblRghPacket.PktClarity, dbo.tblRghPacket.PktFlo, dbo.tblRghPacket.PktModel, dbo.tblRghPacket.PktIss, " & _
                            "dbo.tblRghSections.SecName, dbo.tblRghPacket.PktPrice " & _
                        "FROM dbo.tblRghPacket INNER JOIN dbo.tblRghSections ON dbo.tblRghPacket.PktType = dbo.tblRghSections.SecCode " & _
                        "WHERE (dbo.tblRghPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblRghPacket.PktType = " & cmbType.SelectedIndex + 1 & ") " & _
                        "ORDER BY dbo.tblRghPacket.PktNo", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                flxPacket.Rows.Add(rsComSql_1.Fields("ParNo").Value,
                                   rsComSql_1.Fields("PktNo").Value,
                                   rsComSql_1.Fields("PktColor").Value,
                                   rsComSql_1.Fields("PktClarity").Value,
                                   rsComSql_1.Fields("PktFlo").Value,
                                   rsComSql_1.Fields("PktModel").Value,
                                   rsComSql_1.Fields("PktPcs").Value,
                                   rsComSql_1.Fields("PktCts").Value,
                                   rsComSql_1.Fields("PktPrice").Value)

                dblIssPcs = dblIssPcs + rsComSql_1.Fields("PktPcs").Value
                dblIssCts = dblIssCts + rsComSql_1.Fields("PktCts").Value
                dblTotValue = dblTotValue + (rsComSql_1.Fields("PktCts").Value * rsComSql_1.Fields("PktPrice").Value)
                rsComSql_1.MoveNext()
            End While
        End If
        rsComSql_1 = Nothing

        txtIssPcs.Text = dblIssPcs
        txtIssCts.Text = Format(dblIssCts, "#0.000")
        If dblIssCts <> 0 Then
            txtAvgPrice.Text = Format(dblTotValue / dblIssCts, "#0.00")
        End If

    End Sub

    Private Sub txtPktPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtPktCts.Focus()
        End If
    End Sub

    Private Sub txtPktCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtPktCts.Text)
    End Sub

    Private Sub Save()
        Dim intTotPcs As Double
        Dim intIssPcs As Double
        Dim intSelPcs As Double
        Dim strSupParNo As String

        If ParcelFound(txtParNo.Text) = True Then
            If cmbType.Text = "" Then
                MsgBox("Please Select the Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If Len(txtPktNo.Text) <> 3 Then
                MsgBox("Invalid Packet No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtPktPcs.Text = "" Then
                MsgBox("Please enter the Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtPktCts.Text = "" Then
                MsgBox("Please enter the Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtPrice.Text = "" Then
                MsgBox("Please enter the Price", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            strSupParNo = ""
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT OrigParcelNo FROM tblParcel WHERE GrpParNo = '" & txtParNo.Text & "' AND Depart = 'Rough Planning'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                strSupParNo = rsComSql.Fields("OrigParcelNo").Value
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT ImpPrice FROM tblImport WHERE SupParcelNo = '" & strSupParNo & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If rsComSql.Fields("ImpPrice").Value <= 0 Then
                    MsgBox("Invalid Import Price", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
            rsComSql = Nothing

            If cmbType.Text = "Finish" Then
                If CDbl(txtPrice.Text) = 0 Then
                    MsgBox("Price cannot be zero", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblRghPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktType = 6 AND PktColor = '" & cmbColor.Text & "' AND PktClarity = '" & cmbClarity.Text & "' AND PktFlo = '" & cmbFlo.Text & "' AND PktModel = '" & cmbModel.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If CDbl(txtPrice.Text) <> rsComSql.Fields("PktPrice").Value Then
                        MsgBox("Price cannot be different", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If
                rsComSql = Nothing
            End If

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(IssuedPcs) AS TotPcs FROM tblParcel WHERE GrpParNo = '" & txtParNo.Text & "' AND Depart = 'Rough Planning'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("TotPcs").Value) Then
                    intTotPcs = rsComSql.Fields("TotPcs").Value
                Else
                    intTotPcs = 0
                End If
            Else
                intTotPcs = 0
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(PktPcs) AS TotPcs FROM tblRghPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktType = " & cmbType.SelectedIndex + 1 & "", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("TotPcs").Value) Then
                    intIssPcs = rsComSql.Fields("TotPcs").Value
                Else
                    intIssPcs = 0
                End If
            Else
                intIssPcs = 0
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(PktPcs) AS TotPcs FROM tblRghPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktType = " & cmbType.SelectedIndex + 1 & " AND PktColor = '" & cmbColor.Text & "' AND PktClarity = '" & cmbClarity.Text & "' AND PktFlo = '" & cmbFlo.Text & "' AND PktModel = '" & cmbModel.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("TotPcs").Value) Then
                    intSelPcs = rsComSql.Fields("TotPcs").Value
                Else
                    intSelPcs = 0
                End If
            Else
                intSelPcs = 0
            End If
            rsComSql = Nothing

            If txtAPcs.Text <> "" Then
                If CDbl(txtPktPcs.Text) > CDbl(txtAPcs.Text) Then
                    MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            If txtACts.Text <> "" Then
                If CDbl(txtPktCts.Text) > CDbl(txtACts.Text) Then
                    MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblRghPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND PktType = " & cmbType.SelectedIndex + 1 & "", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                If intTotPcs < intIssPcs + CDbl(txtPktPcs.Text) Then
                    MsgBox("Not enough Pcs in the Parcel", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If cmbType.SelectedIndex + 1 > 1 Then
                    If CDbl(txtAPcs.Text) < intSelPcs + CDbl(txtPktPcs.Text) Then
                        MsgBox("Not enough Pcs in the Selection", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If

                dtpToday = GetToday()

                AdoCN.Execute("INSERT INTO tblRghPacket(ParNo,PktNo,PktPcs,PktCts,PktType,PktColor,PktClarity,PktFlo,PktModel,PktIss,PktPrice) " & _
                              "VALUES('" & UCase(txtParNo.Text) & "','" & txtPktNo.Text & "'," & CDbl(txtPktPcs.Text) & "," & CDbl(txtPktCts.Text) & "," & _
                                "" & cmbType.SelectedIndex + 1 & ",'" & cmbColor.Text & "','" & cmbClarity.Text & "','" & cmbFlo.Text & "','" & cmbModel.Text & "','" & Format(dtpToday, "MM/dd/yyyy") & "'," & CDbl(txtPrice.Text) & ")")


                txtPktNo.Text = ""
                txtPktCts.Text = ""
                txtPktPcs.Text = ""
                txtPktCts.Text = ""
                txtIssPcs.Text = ""
                txtIssCts.Text = ""
                cmbColor.Text = ""
                cmbClarity.Text = ""
                cmbFlo.Text = ""
                cmbModel.Text = ""
                txtAPcs.Text = ""
                txtACts.Text = ""
                flxPacket.Rows.Clear()
                flxDetails.Rows.Clear()
                txtPrice.Text = "0"

                GetNewPacket()
            Else
                PBResponse = MsgBox("Are you sure to update the price?", vbQuestion + vbYesNo, Me.Text)
                If PBResponse  = MsgBoxResult.Yes Then
                    AdoCN.Execute("UPDATE tblRghPacket SET PktPrice = '" & CDbl(txtPrice.Text) & "' " & _
                                  "WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND PktType = " & cmbType.SelectedIndex + 1 & "")

                    txtPktNo.Text = ""
                    txtPktCts.Text = ""
                    txtPktPcs.Text = ""
                    txtPktCts.Text = ""
                    txtIssPcs.Text = ""
                    txtIssCts.Text = ""
                    cmbColor.Text = ""
                    cmbClarity.Text = ""
                    cmbFlo.Text = ""
                    cmbModel.Text = ""
                    txtAPcs.Text = ""
                    txtACts.Text = ""
                    flxPacket.Rows.Clear()
                    flxDetails.Rows.Clear()
                    txtPrice.Text = "0"

                    GetNewPacket()
                End If
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub flxPacket_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxPacket.CellClick
        If flxPacket.Rows.Count > 0 Then
            txtPktNo.Text = flxPacket.Item(1, flxPacket.CurrentRow.Index).Value
            cmbColor.Text = flxPacket.Item(2, flxPacket.CurrentRow.Index).Value
            cmbClarity.Text = flxPacket.Item(3, flxPacket.CurrentRow.Index).Value
            cmbFlo.Text = flxPacket.Item(4, flxPacket.CurrentRow.Index).Value
            cmbModel.Text = flxPacket.Item(5, flxPacket.CurrentRow.Index).Value
            txtPktPcs.Text = flxPacket.Item(6, flxPacket.CurrentRow.Index).Value
            txtPktCts.Text = flxPacket.Item(7, flxPacket.CurrentRow.Index).Value
            txtPrice.Text = flxPacket.Item(8, flxPacket.CurrentRow.Index).Value
        End If
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        If flxDetails.Rows.Count > 0 Then
            cmbColor.Text = flxDetails.Item(2, flxDetails.CurrentRow.Index).Value
            cmbClarity.Text = flxDetails.Item(3, flxDetails.CurrentRow.Index).Value
            cmbFlo.Text = flxDetails.Item(4, flxDetails.CurrentRow.Index).Value
            cmbModel.Text = flxDetails.Item(5, flxDetails.CurrentRow.Index).Value
            txtPcs.Text = flxDetails.Item(6, flxDetails.CurrentRow.Index).Value
            txtCts.Text = flxDetails.Item(7, flxDetails.CurrentRow.Index).Value
            txtPrice.Text = flxDetails.Item(8, flxDetails.CurrentRow.Index).Value
        End If
    End Sub

    Private Sub txtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrice.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtPrice.Text)
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
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim intRow As Integer

        If cmbColor.Text = "" Then MsgBox("Invalid Color", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbFlo.Text = "" Then MsgBox("Invalid Flo", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbClarity.Text = "" Then MsgBox("Invalid Clarity", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbModel.Text = "" Then MsgBox("Invalid Model", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If txtPktPcs.Text = "" Then MsgBox("Invalid Pkt Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtPktCts.Text = "" Then MsgBox("Invalid Pkt Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If CDbl(txtPktPcs.Text) <= 0 Then MsgBox("Invalid Pkt Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtPktCts.Text) <= 0 Then MsgBox("Invalid Pkt Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If txtPcs.Text = "" Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtCts.Text = "" Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtPrice.Text = "" Then MsgBox("Invalid Price", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If CDbl(txtPcs.Text) <= 0 Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtCts.Text) <= 0 Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtPrice.Text) <= 0 Then MsgBox("Invalid Price", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If CDbl(txtPcs.Text) + CDbl(txtSubPcs.Text) > CDbl(txtPktPcs.Text) Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtCts.Text) + CDbl(txtSubCts.Text) > CDbl(txtPktCts.Text) Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        For intRow = 0 To flxDetails.Rows.Count - 1
            If cmbColor.Text = flxDetails.Item(2, intRow).Value And _
                cmbClarity.Text = flxDetails.Item(3, intRow).Value And _
                cmbFlo.Text = flxDetails.Item(4, intRow).Value And _
                cmbModel.Text = flxDetails.Item(5, intRow).Value Then

                MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        flxDetails.Rows.Add(txtParNo.Text,
                            "Boiling",
                            cmbColor.Text,
                            cmbClarity.Text,
                            cmbFlo.Text,
                            cmbModel.Text,
                            txtPcs.Text,
                            txtCts.Text,
                            txtPrice.Text)

        txtSubPcs.Text = CalTotalPcs(flxDetails)
        txtSubCts.Text = CalTotalCts(flxDetails)

        cmbColor.Text = ""
        cmbFlo.Text = ""
        cmbClarity.Text = ""
        cmbModel.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtPrice.Text = ""
        cmbColor.Focus()
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(6, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(7, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub cmbColor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbColor.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbClarity.Focus()
        End If
    End Sub

    Private Sub cmbClarity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbClarity.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbFlo.Focus()
        End If
    End Sub

    Private Sub cmbFlo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbFlo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbModel.Focus()
        End If
    End Sub

    Private Sub cmbModel_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbModel.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtPrice.Focus()
        End If
    End Sub

    Private Sub SaveDetails()
        Dim intRow As Integer
        Dim strPktNo As String

        If ParcelFound(txtParNo.Text) = True Then
            If cmbType.Text = "" Then
                MsgBox("Please Select the Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If Len(txtPktNo.Text) <> 3 Then
                MsgBox("Invalid Packet No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtPktPcs.Text = "" Then
                MsgBox("Please enter the Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtPktCts.Text = "" Then
                MsgBox("Please enter the Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If CDbl(txtPktPcs.Text) <> CDbl(txtSubPcs.Text) Then
                MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If CDbl(txtPktCts.Text) <> CDbl(txtSubCts.Text) Then
                MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblRghIssues WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = 1", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                'Boiling Issue
                AdoCN.Execute("INSERT INTO tblRghIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                              "VALUES('" & UCase(txtParNo.Text) & "','" & txtPktNo.Text & "',1,'Boiling',1,'D06313'," & CDbl(txtSubPcs.Text) & "," & _
                                "" & CSng(txtSubCts.Text) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "')")

                'Boiling Return
                AdoCN.Execute("INSERT INTO tblRghReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcs,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs) " & _
                              "VALUES('" & UCase(txtParNo.Text) & "','" & txtPktNo.Text & "','Boiling',1,1,'D06313'," & CDbl(txtSubPcs.Text) & "," & _
                                "" & CSng(txtSubCts.Text) & ",0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0)")

                'Boiling Return Detail
                AdoCN.Execute("INSERT INTO tblRghReturnDetails(ParNo,PktNo,Sec,Boiling,Color,Clarity,Flo,Model,Pcs,Cts) " & _
                              "VALUES('" & UCase(txtParNo.Text) & "','" & txtPktNo.Text & "',1,'Boiling','','','',''," & CDbl(txtSubPcs.Text) & "," & CDbl(txtSubCts.Text) & ")")

                strPktNo = "001"
                For intRow = 0 To flxDetails.Rows.Count - 1
                    'Finish Next Packet No.
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT MAX(PktNo) AS MaxPktNo FROM tblRghPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktType = 6", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        If Not IsDBNull(rsComSql_1.Fields("MaxPktNo").Value) Then
                            strPktNo = Format(rsComSql_1.Fields("MaxPktNo").Value + 1, "000")
                        Else
                            strPktNo = "001"
                        End If
                    End If
                    rsComSql_1 = Nothing

                    'Finish Packet
                    AdoCN.Execute("INSERT INTO tblRghPacket(ParNo,PktNo,PktPcs,PktCts,PktType,PktColor,PktClarity,PktFlo,PktModel,PktIss,PktPrice) " & _
                                  "VALUES('" & UCase(txtParNo.Text) & "','" & strPktNo & "'," & CDbl(flxDetails.Item(6, intRow).Value) & "," & CDbl(flxDetails.Item(7, intRow).Value) & "," & _
                                    "6,'" & flxDetails.Item(2, intRow).Value & "','" & flxDetails.Item(3, intRow).Value & "','" & flxDetails.Item(4, intRow).Value & "','" & flxDetails.Item(5, intRow).Value & "'," & _
                                    "'" & Format(Date.Now, "MM/dd/yyyy") & "'," & CDbl(flxDetails.Item(8, intRow).Value) & ")")

                    'Finish Issue
                    AdoCN.Execute("INSERT INTO tblRghIssues(ParNo,PktNo,Sec,Flow,SecCount,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                  "VALUES('" & UCase(txtParNo.Text) & "','" & strPktNo & "',6,'Finish',6,'D06313'," & CDbl(flxDetails.Item(6, intRow).Value) & "," & _
                                    "" & CSng(flxDetails.Item(7, intRow).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "')")

                    'Finish Return
                    AdoCN.Execute("INSERT INTO tblRghReturns(ParNo,PktNo,Flow,SecCount,Sec,EmpNo,RetPcs,RetCts,RejPcs,RejCts,LostPcs,LostCts,BroPcs,RepPcs,NopayPcs,RetDate,RetTime,ExtPcs) " & _
                                  "VALUES('" & UCase(txtParNo.Text) & "','" & strPktNo & "','Finish',6,6,'D06313'," & CDbl(flxDetails.Item(6, intRow).Value) & "," & _
                                    "" & CSng(flxDetails.Item(7, intRow).Value) & ",0,0,0,0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "',0)")

                    'Finish Return Details
                    AdoCN.Execute("INSERT INTO tblRghReturnDetails(ParNo,PktNo,Sec,Boiling,Color,Clarity,Flo,Model,Pcs,Cts) " & _
                                  "VALUES('" & UCase(txtParNo.Text) & "','" & strPktNo & "',6,'Boiling','" & flxDetails.Item(2, intRow).Value & "','" & flxDetails.Item(3, intRow).Value & "'," & _
                                    "'" & flxDetails.Item(4, intRow).Value & "','" & flxDetails.Item(5, intRow).Value & "'," & CDbl(flxDetails.Item(6, intRow).Value) & "," & CDbl(flxDetails.Item(7, intRow).Value) & ")")
                Next
            Else
                PBResponse = MsgBox("Already Issued. Do you want to Update the Parcel?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
                If PBResponse = MsgBoxResult.Yes Then
                    For intRow = 0 To flxDetails.Rows.Count - 1
                        AdoCN.Execute("UPDATE tblRghPacket SET PktColor = '" & flxDetails.Item(2, intRow).Value & "',PktClarity = '" & flxDetails.Item(3, intRow).Value & "',PktFlo = '" & flxDetails.Item(4, intRow).Value & "',PktModel = '" & flxDetails.Item(5, intRow).Value & "',PktPrice = " & CDbl(flxDetails.Item(8, intRow).Value) & " " & _
                                      "WHERE ID = " & flxDetails.Item(9, intRow).Value & "")

                        AdoCN.Execute("UPDATE tblRghReturnDetails SET Color = '" & flxDetails.Item(2, intRow).Value & "',Clarity = '" & flxDetails.Item(3, intRow).Value & "',Flo = '" & flxDetails.Item(4, intRow).Value & "',Model = '" & flxDetails.Item(5, intRow).Value & "' " & _
                                      "WHERE ID = " & flxDetails.Item(10, intRow).Value & "")
                    Next
                End If
            End If
            rsComSql = Nothing

            MsgBox("Details Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
        End If
    End Sub

    Private Sub UpdatePrice()
        Dim intRow As Integer

        If ParcelFound(txtParNo.Text) = True Then
            PBResponse = MsgBox("Do you want to Update the Price?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse  = MsgBoxResult.Yes Then
                For intRow = 0 To flxPacket.Rows.Count - 1
                    If Not IsNumeric(flxPacket.Item(8, intRow).Value) Then
                        MsgBox("Invalid Price", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                Next

                For intRow = 0 To flxPacket.Rows.Count - 1
                    AdoCN.Execute("UPDATE tblRghPacket SET PktPrice = " & CDbl(flxPacket.Item(8, intRow).Value) & " WHERE ParNo = '" & UCase(txtParNo.Text) & "' AND PktNo = '" & flxPacket.Item(1, intRow).Value & "' AND PktType = 6")
                Next

                MsgBox("Price Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
            End If
        End If
    End Sub

    Private Sub cmdSaveDt_Click(sender As Object, e As EventArgs) Handles cmdSaveDt.Click
        SaveDetails()
    End Sub

    Private Sub flxDetails_DoubleClick(sender As Object, e As EventArgs) Handles flxDetails.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse  = MsgBoxResult.Yes Then
            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)

            txtSubPcs.Text = CalTotalPcs(flxDetails)
            txtSubCts.Text = CalTotalCts(flxDetails)
        End If
    End Sub

    Private Sub cmdUpdate_Click(sender As Object, e As EventArgs) Handles cmdUpdate.Click
        UpdatePrice()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub txtPktNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblRghPacket WHERE (ParNo = '" & txtParNo.Text & "') AND (PktType = 6)", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                Exit Sub
            End If
            rsComSql = Nothing

            flxDetails.Rows.Clear()
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblRghSort WHERE (ParNo = '" & Mid(txtParNo.Text, 1, 6) & "') AND (PktNo = '" & txtPktNo.Text & "') ORDER BY ID", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                        "Boiling",
                                        rsComSql.Fields("PktColor").Value,
                                        rsComSql.Fields("PktClarity").Value,
                                        rsComSql.Fields("PktFlo").Value,
                                        rsComSql.Fields("PktModel").Value,
                                        rsComSql.Fields("PktPcs").Value,
                                        rsComSql.Fields("PktCts").Value,
                                        rsComSql.Fields("PktPrice").Value)

                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing

            txtSubPcs.Text = CalTotalPcs(flxDetails)
            txtSubCts.Text = CalTotalCts(flxDetails)
        End If
    End Sub

    Private Sub Delete()
        If ParcelFound(txtParNo.Text) = True Then
            AdoCN.Execute("DELETE FROM tblRghReturnDetails WHERE ParNo = '" & txtParNo.Text & "'")
            AdoCN.Execute("DELETE FROM tblRghReturns WHERE ParNo = '" & txtParNo.Text & "'")
            AdoCN.Execute("DELETE FROM tblRghIssues WHERE ParNo = '" & txtParNo.Text & "'")
            AdoCN.Execute("DELETE FROM tblRghPacket WHERE ParNo = '" & txtParNo.Text & "'")

            Insert_Log("DELETE", "Rgh", txtParNo.Text, txtPktNo.Text, 1)

            MsgBox("Deleted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

            ClearFields()
        End If
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        PBResponse = MsgBox("Are you sure to Delete the full Parcel?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            Delete()
        End If
    End Sub
End Class