
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_ExpSizingPacket

    Private Sub frm_ExpSizingPacket_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        ClearFields()
        Load_Department(cmbDept)
        Load_GradingTypes()
    End Sub

    Private Sub ClearFields()
        cmbDept.Text = ""
        txtParNo.Text = ""
        flxPacket.Rows.Clear()
        txtPktNo.Text = ""
        txtPktPcs.Text = ""
        txtPktCts.Text = ""
        cmbType.Text = ""
        flxDetails.Rows.Clear()
        txtBalPcs.Text = ""
        txtBalCts.Text = ""
        txtTotBalPcs.Text = ""
        txtTotBalCts.Text = ""
        txtPktNo2.Text = ""
        txtOrigin.Text = ""
        txtOCode.Text = ""
    End Sub

    Private Sub Load_GradingTypes()
        Dim rsGrdType As New ADODB.Recordset

        rsGrdType = New ADODB.Recordset
        rsGrdType.Open("SELECT * FROM tblGrading_Types WHERE Sec = 3 ORDER BY Type", AdoCN, 1, 1)
        If rsGrdType.RecordCount Then
            rsGrdType.MoveFirst()
            While Not rsGrdType.EOF
                cmbType.Items.Add(rsGrdType.Fields("Type").Value)
                rsGrdType.MoveNext()
            End While
        End If
        rsGrdType = Nothing

    End Sub

    Private Sub GetNewPacket(ByVal intSec As Integer)
        Dim intIssPcs As Integer
        Dim dblIssCts As Double
        Dim strPktNo As String

        Dim intPktLen As Integer

        intPktLen = 4
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

        If intSec = 1 Then
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT MAX(RIGHT(PktNo, 3)) AS MaxPktNo FROM tblExpSizingPacket WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND LEFT(PktNo, 1) = 'K'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                If Not IsDBNull(rsComSql_1.Fields("MaxPktNo").Value) Then
                    txtPktNo.Text = "K" & Format(rsComSql_1.Fields("MaxPktNo").Value + 1, "000")
                Else
                    txtPktNo.Text = "K001"
                End If
            End If
            rsComSql_1 = Nothing

        ElseIf intSec = 2 Then
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT MAX(RIGHT(PktNo, 3)) AS MaxPktNo FROM tblExpSizingPacket WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND LEFT(PktNo, 1) = 'J'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                If Not IsDBNull(rsComSql_1.Fields("MaxPktNo").Value) Then
                    txtPktNo.Text = "J" & Format(rsComSql_1.Fields("MaxPktNo").Value + 1, "000")
                Else
                    txtPktNo.Text = "J001"
                End If
            End If
            rsComSql_1 = Nothing

        ElseIf intSec = 4 Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT MAX(LEN(PktNo)) AS PktLen FROM tblExpSizingPacket WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND LEFT(PktNo, 1) = 'M'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("PktLen").Value) Then
                    intPktLen = rsComSql.Fields("PktLen").Value
                End If
            End If
            rsComSql = Nothing

            'rsComSql_1 = New ADODB.Recordset
            'rsComSql_1.Open("SELECT MAX(RIGHT(PktNo, 3)) AS MaxPktNo FROM tblExpSizingPacket WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND LEFT(PktNo, 1) = 'M'", AdoCN, 1, 1)
            'If rsComSql_1.RecordCount Then
            '    If Not IsDBNull(rsComSql_1.Fields("MaxPktNo").Value) Then
            '        txtPktNo.Text = "M" & Format(rsComSql_1.Fields("MaxPktNo").Value + 1, "000")
            '    Else
            '        txtPktNo.Text = "M001"
            '    End If
            'End If
            'rsComSql_1 = Nothing

            If intPktLen = 4 Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT MAX(RIGHT(PktNo,3)) AS MaxPktNo FROM tblExpSizingPacket WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND LEFT(PktNo, 1) = 'M' AND LEN(PktNo) = 4", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("MaxPktNo").Value) Then
                        txtPktNo.Text = "M" & Format(rsComSql.Fields("MaxPktNo").Value + 1, "000")
                    Else
                        txtPktNo.Text = "M001"
                    End If
                End If
                rsComSql = Nothing

            Else
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT MAX(RIGHT(PktNo,4)) AS MaxPktNo FROM tblExpSizingPacket WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND LEFT(PktNo, 1) = 'M' AND LEN(PktNo) = 5", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("MaxPktNo").Value) Then
                        txtPktNo.Text = "M" & Format(rsComSql.Fields("MaxPktNo").Value + 1, "0000")
                    Else
                        txtPktNo.Text = "M0001"
                    End If
                End If
                rsComSql = Nothing

            End If

        Else
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT MAX(RIGHT(PktNo, 3)) AS MaxPktNo FROM tblExpSizingPacket WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND LEFT(PktNo, 1) = 'H'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                If Not IsDBNull(rsComSql_1.Fields("MaxPktNo").Value) Then
                    txtPktNo.Text = "H" & Format(rsComSql_1.Fields("MaxPktNo").Value + 1, "000")
                Else
                    txtPktNo.Text = "H001"
                End If
            End If
            rsComSql_1 = Nothing
        End If

        flxPacket.Rows.Clear()
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        txtTotBalPcs.Text = "0"
        txtTotBalCts.Text = "0"
        strPktNo = ""
        rsComSql_1 = New ADODB.Recordset
        'rsComSql_1.Open("SELECT PktNo, SUM(Pcs) AS TotPcs, SUM(Cts) AS TotCts FROM tblExpReturnDetails WHERE ParNo = '" & txtParNo.Text & "' AND Sec = 3 AND Department = '" & cmbDept.Text & "' GROUP BY PktNo ORDER BY PktNo", AdoCN, 1, 1)

        rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.tblExpReturnDetails.PktNo, ISNULL(dbo.tblGrading_RepairParcelsA.PktNo2, dbo.tblExpReturnDetails.PktNo) AS PktNo2, " & _
                            "SUM(dbo.tblExpReturnDetails.Pcs) AS TotPcs, SUM(dbo.tblExpReturnDetails.Cts) AS TotCts " & _
                        "FROM dbo.tblExpReturnDetails LEFT OUTER JOIN dbo.tblGrading_RepairParcelsA ON dbo.tblExpReturnDetails.Department = dbo.tblGrading_RepairParcelsA.Department AND " & _
                            "dbo.tblExpReturnDetails.ParNo = dbo.tblGrading_RepairParcelsA.ParNo And dbo.tblExpReturnDetails.PktNo = dbo.tblGrading_RepairParcelsA.PktNo " & _
                        "WHERE (dbo.tblExpReturnDetails.ParNo = '" & txtParNo.Text & "') AND (dbo.tblExpReturnDetails.Sec = 3) AND (dbo.tblExpReturnDetails.Department = '" & cmbDept.Text & "') " & _
                        "GROUP BY dbo.tblExpReturnDetails.PktNo, dbo.tblGrading_RepairParcelsA.PktNo2 " & _
                        "ORDER BY dbo.tblExpReturnDetails.PktNo", AdoCN, 1, 1)

        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                strPktNo = rsComSql_1.Fields("PktNo2").Value

                If Mid(rsComSql_1.Fields("PktNo").Value, 1, 1) = "G" Then
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT PktType FROM tblExpPacket WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & rsComSql_1.Fields("PktNo").Value & "'", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        strPktNo = rsComSql_2.Fields("PktType").Value
                    End If
                    rsComSql_2 = Nothing
                End If

                If Mid(rsComSql_1.Fields("PktNo2").Value, 1, 1) = "M" Then
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT OrderNo FROM tblGradingTrf WHERE Department = '" & cmbDept.Text & "' AND ParcelNo = '" & txtParNo.Text & "' AND PktNo = '" & rsComSql_1.Fields("PktNo2").Value & "'", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        strPktNo = rsComSql_2.Fields("OrderNo").Value
                    End If
                    rsComSql_2 = Nothing
                End If

                intIssPcs = 0
                dblIssCts = 0
                rsComSql_2 = New ADODB.Recordset
                'rsComSql_2.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktCts), 3) AS PktCts FROM tblExpSizingPacket WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND ReturnType = '" & rsComSql_1.Fields("PktNo").Value & "'", AdoCN, 1, 1)
                rsComSql_2.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktCts), 3) AS PktCts FROM tblExpSizingPacket WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND PktNo2 = '" & rsComSql_1.Fields("PktNo").Value & "'", AdoCN, 1, 1)
                If Not IsDBNull(rsComSql_2.Fields("PktPcs").Value) Then
                    intIssPcs = rsComSql_2.Fields("PktPcs").Value
                    dblIssCts = Math.Round(rsComSql_2.Fields("PktCts").Value, 3)
                End If
                rsComSql_2 = Nothing

                flxPacket.Rows.Add(rsComSql_1.Fields("PktNo").Value,
                                   rsComSql_1.Fields("TotPcs").Value,
                                   Format(Math.Round(rsComSql_1.Fields("TotCts").Value, 3), "#0.000"),
                                   rsComSql_1.Fields("TotPcs").Value - intIssPcs,
                                   Format(Math.Round(rsComSql_1.Fields("TotCts").Value - dblIssCts, 3), "#0.000"),
                                   strPktNo)

                txtTotPcs.Text = CInt(txtTotPcs.Text) + rsComSql_1.Fields("TotPcs").Value
                txtTotCts.Text = Format(Math.Round(CDbl(txtTotCts.Text) + rsComSql_1.Fields("TotCts").Value, 3), "#0.000")

                txtTotBalPcs.Text = CInt(txtTotBalPcs.Text) + (rsComSql_1.Fields("TotPcs").Value - intIssPcs)
                txtTotBalCts.Text = Format(Math.Round(CDbl(txtTotBalCts.Text) + rsComSql_1.Fields("TotCts").Value - dblIssCts, 3), "#0.000")
                rsComSql_1.MoveNext()
            End While
        End If
        rsComSql_1 = Nothing

        flxDetails.Rows.Clear()
        txtTPktPcs.Text = "0"
        txtTPktCts.Text = "0"
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblExpSizingPacket WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' ORDER BY PktNo", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                flxDetails.Rows.Add(rsComSql_1.Fields("PktNo").Value,
                                    rsComSql_1.Fields("ReturnType").Value,
                                    rsComSql_1.Fields("PktPcs").Value,
                                    Format(Math.Round(rsComSql_1.Fields("PktCts").Value, 3), "#0.000"),
                                    rsComSql_1.Fields("PktNo2").Value)

                txtTPktPcs.Text = CInt(txtTPktPcs.Text) + rsComSql_1.Fields("PktPcs").Value
                txtTPktCts.Text = Format(Math.Round(CDbl(txtTPktCts.Text) + rsComSql_1.Fields("PktCts").Value, 3), "#0.000")
                rsComSql_1.MoveNext()
            End While
        End If
        rsComSql_1 = Nothing

        txtBalPcs.Text = CInt(txtTotPcs.Text) - CInt(txtTPktPcs.Text)
        txtBalCts.Text = Format(CDbl(txtTotCts.Text) - CDbl(txtTPktCts.Text), "#0.000")

    End Sub

    Private Function ParcelFound(ByVal strParceNo As String) As Boolean
        ParcelFound = True
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblExpPacket WHERE ParNo = '" & strParceNo & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount > 0 Then
            ParcelFound = True
        Else
            ParcelFound = False
        End If
        rsComSql_1 = Nothing
        Return ParcelFound
    End Function

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtParNo.Text = UCase(txtParNo.Text)
            If ParcelFound(txtParNo.Text) = True Then
                If cmbDept.Text = "Opening" Then
                    GetNewPacket(4)
                Else
                    GetNewPacket(1)
                End If
            Else
                MsgBox("Invalid Parcel", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
                txtParNo.Focus()
            End If
        End If
    End Sub

    Private Sub flxPacket_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxPacket.CellClick
        If flxPacket.Rows.Count > 0 Then
            cmbType.Text = flxPacket.Item(5, flxPacket.CurrentRow.Index).Value
            txtPktPcs.Text = flxPacket.Item(3, flxPacket.CurrentRow.Index).Value
            txtPktCts.Text = flxPacket.Item(4, flxPacket.CurrentRow.Index).Value
            txtPktNo2.Text = flxPacket.Item(0, flxPacket.CurrentRow.Index).Value
        End If
    End Sub

    Private Sub chkSecond_CheckedChanged(sender As Object) Handles chkSecond.CheckedChanged
        If chkSecond.Checked = True Then
            chkRough.Checked = False
            GetNewPacket(2)
        Else
            GetNewPacket(1)
        End If
    End Sub

    Private Sub chkRough_CheckedChanged(sender As Object) Handles chkRough.CheckedChanged
        If chkRough.Checked = True Then
            chkSecond.Checked = False
            GetNewPacket(3)
        Else
            GetNewPacket(1)
        End If
    End Sub

    Private Sub Save()
        If cmbDept.Text = "" Then
            MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtParNo.Text = "" Then
            MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtPktNo.Text = "" Then
            MsgBox("Invalid Packet No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If cmbType.Text = "" Then
            MsgBox("Invalid Clarity", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtPktPcs.Text = "" Then
            MsgBox("Invalid Packet Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If CInt(txtPktPcs.Text) <= 0 Then
            MsgBox("Invalid Packet Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtPktCts.Text = "" Then
            MsgBox("Invalid Packet Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If CDbl(txtPktCts.Text) <= 0 Then
            MsgBox("Invalid Packet Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If CInt(txtPktPcs.Text) > CInt(txtBalPcs.Text) Then
            MsgBox("Invalid Packet Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If CDbl(txtPktCts.Text) > CDbl(txtBalCts.Text) Then
            MsgBox("Invalid Packet Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblExpSizingPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            AdoCN.Execute("INSERT INTO tblExpSizingPacket(Department, ParNo, PktNo, PktPcs, PktCts, ReturnType, PktNo2) " & _
                          "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "'," & CInt(txtPktPcs.Text) & "," & CDbl(txtPktCts.Text) & ",'" & cmbType.Text & "','" & txtPktNo2.Text & "')")
        Else
            MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing

        txtPktPcs.Text = ""
        txtPktCts.Text = ""
        cmbType.Text = ""
        txtBalPcs.Text = ""
        txtBalCts.Text = ""
        txtTotBalPcs.Text = ""
        txtTotBalCts.Text = ""
        txtPktNo2.Text = ""

        If cmbDept.Text = "Opening" Then
            GetNewPacket(4)
        Else
            If chkSecond.Checked = True Then
                GetNewPacket(2)
            Else
                If chkRough.Checked = True Then
                    GetNewPacket(3)
                Else
                    GetNewPacket(1)
                End If
            End If
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

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub
End Class