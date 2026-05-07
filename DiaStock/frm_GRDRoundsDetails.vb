
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDRoundsDetails
    Dim strFolderPath As String

    Private Sub frm_GRDRoundsDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        If dbConnNiru.State = 1 Then
            dbConnNiru.Close()
        End If
        dbConnNiru.ConnectionString = "Provider=SQLOLEDB;Data Source='" & strServerName & "';Connect Timeout=60;Initial Catalog=NiruStock;Integrated Security=SSPI"
        dbConnNiru.Open()

        If strDBName = "DiaStock" Then
            strFolderPath = "Grading\"
        Else
            strFolderPath = "DiaSalesGrading\"
        End If

        Load_GradingTypes(cmbColor, 1)
        Load_GradingTypes(cmbMake, 2)
        Load_GradingTypes(cmbFlo, 3)
        Load_GradingTypes(cmbClarity, 4)
        Load_Orders()

        ClearFields()
    End Sub

    Private Sub Load_GradingTypes(ByVal cmbSample As System.Windows.Forms.ComboBox, ByVal intSec As Integer)
        Dim rsGrdType As New ADODB.Recordset

        cmbSample.Items.Clear()
        rsGrdType = New ADODB.Recordset
        rsGrdType.Open("SELECT * FROM tblGrading_RndTypes WHERE Sec = " & intSec & " ORDER BY Type", AdoCN, 1, 1)
        If rsGrdType.RecordCount Then
            rsGrdType.MoveFirst()
            While Not rsGrdType.EOF
                cmbSample.Items.Add(rsGrdType.Fields("Type").Value)
                rsGrdType.MoveNext()
            End While
        End If
        rsGrdType = Nothing

    End Sub

    Private Sub Load_SizeRange()
        Dim rsGrdType As New ADODB.Recordset

        cmbSize.Items.Clear()
        rsGrdType = New ADODB.Recordset
        'rsGrdType.Open("SELECT * FROM tblGrading_RndSizingRange ORDER BY Code", AdoCN, 1, 1)
        rsGrdType.Open("SELECT * FROM tblGrading_RndSizeListRange WHERE AssortNo = '" & cmbAssort.Text & "' ORDER BY Size", AdoCN, 1, 1)
        If rsGrdType.RecordCount Then
            rsGrdType.MoveFirst()
            While Not rsGrdType.EOF
                cmbSize.Items.Add(rsGrdType.Fields("Size").Value)
                rsGrdType.MoveNext()
            End While
        End If
        rsGrdType = Nothing

    End Sub

    Private Sub Load_Orders()
        Dim rsGrdType As New ADODB.Recordset

        cmbOrder.Items.Clear()
        rsGrdType = New ADODB.Recordset
        rsGrdType.Open("SELECT OrderNo FROM tblNoneOrders WHERE (Type = 'ROUNDS') AND (Complete = N'N') ORDER BY OrderNo", AdoCN, 1, 1)
        If rsGrdType.RecordCount Then
            rsGrdType.MoveFirst()
            While Not rsGrdType.EOF
                cmbOrder.Items.Add(rsGrdType.Fields("OrderNo").Value)
                rsGrdType.MoveNext()
            End While
        End If
        rsGrdType = Nothing

    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Get_AssortCode()
        txtCode.Text = ""
        cmbAssort.Items.Clear()
        If cmbColor.Text <> "" And cmbMake.Text <> "" And cmbClarity.Text <> "" Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_RndSizingCodes WHERE Color = '" & cmbColor.Text & "' AND Make = '" & cmbMake.Text & "' AND Clarity = '" & cmbClarity.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                txtCode.Text = rsComSql.Fields("Code").Value
            End If
            rsComSql = Nothing

            If txtCode.Text <> "" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblGrading_RndSizeList WHERE MainAssort = '" & txtCode.Text & "' AND (RIGHT(AssortNo, 2) <> '_M') AND (RIGHT(AssortNo, 2) <> '_C') ORDER BY AssortNo", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    While Not rsComSql.EOF
                        cmbAssort.Items.Add(rsComSql.Fields("AssortNo").Value)
                        rsComSql.MoveNext()
                    End While
                End If
                rsComSql = Nothing
            End If
        End If
    End Sub

    Private Sub cmbColor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbColor.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Get_AssortCode()
            cmbMake.Focus()
        End If
    End Sub

    Private Sub cmbColor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbColor.SelectedIndexChanged
        Get_AssortCode()
    End Sub

    Private Sub cmbMake_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbMake.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Get_AssortCode()
            cmbFlo.Focus()
        End If
    End Sub

    Private Sub cmbMake_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMake.SelectedIndexChanged
        Get_AssortCode()
    End Sub

    Private Sub cmbClarity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbClarity.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Get_AssortCode()
            cmbAssort.Focus()
        End If
    End Sub

    Private Sub cmbClarity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbClarity.SelectedIndexChanged
        Get_AssortCode()
    End Sub

    Private Sub ClearFields()
        txtParNo.Text = ""
        txtPktNo.Text = ""
        txtTrfPcs.Text = ""
        txtTrfCts.Text = ""
        txtTotPcs.Text = ""
        txtTotCts.Text = ""
        cmbColor.Text = ""
        cmbMake.Text = ""
        cmbFlo.Text = ""
        cmbClarity.Text = ""
        txtCode.Text = ""
        cmbAssort.Text = ""
        cmbAssort.Items.Clear()
        txtPktPcs.Text = ""
        txtPktCts.Text = ""
        flxDetails.Rows.Clear()
        cmbOrder.Text = ""
        cmbSize.Text = ""
        txtRemarks.Text = ""
        txtParNo.Focus()
    End Sub

    Private Sub ClearPacket()
        txtPktNo.Text = ""
        txtTrfPcs.Text = ""
        txtTrfCts.Text = ""
        txtTotPcs.Text = ""
        txtTotCts.Text = ""
        cmbColor.Text = ""
        cmbMake.Text = ""
        cmbFlo.Text = ""
        cmbClarity.Text = ""
        txtCode.Text = ""
        cmbAssort.Text = ""
        cmbAssort.Items.Clear()
        txtPktPcs.Text = ""
        txtPktCts.Text = ""
        flxDetails.Rows.Clear()
        cmbOrder.Text = ""
        txtRemarks.Text = ""
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtParNo.Text = UCase(txtParNo.Text)
            If ParcelFound("Rounds", txtParNo.Text) = True Then
                txtPktNo.Text = ""
                txtTrfPcs.Text = ""
                txtTrfCts.Text = ""
                txtPktNo.Focus()
            Else
                MsgBox("Invalid Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtParNo.Text = ""
                txtPktNo.Text = ""
                txtParNo.Focus()
            End If
        End If
    End Sub

    Private Function ParcelFound(ByVal strDept As String, ByVal strParceNo As String) As Boolean
        ParcelFound = True
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblGrading_CheckingReturns WHERE ParNo = '" & strParceNo & "' AND Department = '" & strDept & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount > 0 Then
            ParcelFound = True
        Else
            ParcelFound = False
        End If
        rsComSql_1 = Nothing
        Return ParcelFound
    End Function

    Private Sub txtPktNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtParNo.Text = UCase(txtParNo.Text)
            txtPktNo.Text = UCase(txtPktNo.Text)

            Load_Packet()
        End If
    End Sub

    Private Sub Load_Packet()
        Dim intTotPcs As Integer
        Dim dblTotCts As Double

        txtTrfPcs.Text = ""
        txtTrfCts.Text = ""
        'rsComSql = New ADODB.Recordset
        'rsComSql.Open("SELECT SUM(Trf_Pcs) AS Trf_Pcs, ROUND(SUM(Trf_Cts), 3) AS Trf_Cts " & _
        '              "FROM dbo.tblGradingTrf " & _
        '              "WHERE (Department = 'Rounds') AND (ParcelNo = '" & txtParNo.Text & "') AND (PktNo = '" & txtPktNo.Text & "')", AdoCN, 1, 1)
        'If rsComSql.RecordCount Then
        '    txtTrfPcs.Text = rsComSql.Fields("Trf_Pcs").Value
        '    txtTrfCts.Text = rsComSql.Fields("Trf_Cts").Value
        'End If
        'rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT PktPcs AS Pcs, PktCts AS Cts " & _
                      "FROM tblGrading_RndPacket " & _
                      "WHERE (Department = 'Rounds') AND (ParNo = '" & txtParNo.Text & "') AND (PktNo = '" & txtPktNo.Text & "') AND (Status = 1)", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            txtTrfPcs.Text = rsComSql.Fields("Pcs").Value
            txtTrfCts.Text = rsComSql.Fields("Cts").Value
        Else
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT SUM(Pcs) AS Pcs, SUM(Cts) AS Cts " & _
                          "FROM dbo.VW_GradingCheckingBalance " & _
                          "WHERE (Department = 'Rounds') AND (ParcelNo = '" & txtParNo.Text & "') AND (PktNo = '" & txtPktNo.Text & "')", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                txtTrfPcs.Text = rsComSql_1.Fields("Pcs").Value
                txtTrfCts.Text = rsComSql_1.Fields("Cts").Value
            End If
            rsComSql_1 = Nothing
        End If
        rsComSql = Nothing

        intTotPcs = 0
        dblTotCts = 0
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM dbo.tblGrading_RndSizingTypes " & _
                      "WHERE (Department = 'Rounds') AND (ParNo = '" & txtParNo.Text & "') AND (PktNo = '" & txtPktNo.Text & "')", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            txtRemarks.Text = rsComSql.Fields("Remarks").Value
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("ReturnType1").Value, rsComSql.Fields("ReturnType2").Value,
                                    rsComSql.Fields("ReturnType3").Value, rsComSql.Fields("ReturnType4").Value,
                                    rsComSql.Fields("ReturnType5").Value, rsComSql.Fields("ReturnType6").Value,
                                    rsComSql.Fields("Pcs").Value, rsComSql.Fields("Cts").Value,
                                    rsComSql.Fields("Price").Value, rsComSql.Fields("OrderNo").Value,
                                    rsComSql.Fields("DocID").Value, rsComSql.Fields("ReturnType7").Value)

                intTotPcs = intTotPcs + rsComSql.Fields("Pcs").Value
                dblTotCts = dblTotCts + rsComSql.Fields("Cts").Value

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtTotPcs.Text = intTotPcs
        txtTotCts.Text = Math.Round(dblTotCts, 3)
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub txtPktPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtPktCts.Focus()
        End If
    End Sub

    Private Sub txtPktCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtPktCts.Text)
        If Asc(e.KeyChar) = 13 Then
            cmbOrder.Focus()
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim intRow As Integer
        Dim intTotPcs As Integer
        Dim dblTotCts As Double
        Dim dblPrice As Double

        dblPrice = 0

        If cmbColor.Text = "" Then
            MsgBox("Please check the Color", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If cmbMake.Text = "" Then
            MsgBox("Please check the Make", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If cmbFlo.Text = "" Then
            MsgBox("Please check the Flo", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If cmbClarity.Text = "" Then
            MsgBox("Please check the Clarity", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If cmbAssort.Text = "" Then
            MsgBox("Please check the Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If cmbSize.Text = "" Then
            MsgBox("Please check the Size Range", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtPktPcs.Text <> "" And txtPktCts.Text <> "" Then
            If CInt(txtPktPcs.Text) > 0 Then

                intTotPcs = 0
                dblTotCts = 0
                For intRow = 0 To flxDetails.Rows.Count - 1
                    intTotPcs = intTotPcs + CInt(flxDetails.Item(6, intRow).Value)
                    dblTotCts = dblTotCts + CDbl(flxDetails.Item(7, intRow).Value)
                Next
                txtTotPcs.Text = intTotPcs
                txtTotCts.Text = dblTotCts

                If intTotPcs + CInt(txtPktPcs.Text) > CInt(txtTrfPcs.Text) Then
                    MsgBox("Pcs Invalid", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If Math.Round(dblTotCts + CDbl(txtPktCts.Text), 3) > Math.Round(CDbl(txtTrfCts.Text), 3) + 0.1 Then
                    MsgBox("Cts Invalid", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                cmbAssort.Text = UCase(cmbAssort.Text)
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblGrading_RndSizeList WHERE AssortNo = '" & cmbAssort.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    dblPrice = rsComSql.Fields("Price").Value

                    txtCode.Text = rsComSql.Fields("MainAssort").Value
                Else
                    MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    cmbAssort.Text = ""
                    cmbAssort.Focus()
                    Exit Sub
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblGrading_RndSizeListRange WHERE Size = '" & cmbSize.Text & "' AND AssortNo = '" & cmbAssort.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    MsgBox("Invalid Size Range", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    cmbSize.Text = ""
                    cmbSize.Focus()
                    Exit Sub
                End If
                rsComSql = Nothing

                flxDetails.Rows.Add(cmbColor.Text, cmbMake.Text,
                                    cmbFlo.Text, cmbClarity.Text,
                                    txtCode.Text, cmbAssort.Text,
                                    txtPktPcs.Text, Format(CDbl(txtPktCts.Text), "#0.000"),
                                    dblPrice, cmbOrder.Text, "",
                                    cmbSize.Text)

                txtTotPcs.Text = CInt(txtTotPcs.Text) + CInt(txtPktPcs.Text)
                txtTotCts.Text = Format(CDbl(txtTotCts.Text) + CDbl(txtPktCts.Text), "#0.000")

                txtPktPcs.Text = ""
                txtPktCts.Text = ""
                cmbOrder.Text = ""
                cmbAssort.Text = ""
                cmbSize.Text = ""
                cmbColor.Focus()
            Else
                MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            MsgBox("Please check the Pcs/Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If

        cmbColor.Focus()
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        If flxDetails.Rows.Count > 0 Then
            cmbColor.Text = flxDetails.Item(0, flxDetails.CurrentRow.Index).Value
            cmbMake.Text = flxDetails.Item(1, flxDetails.CurrentRow.Index).Value
            cmbFlo.Text = flxDetails.Item(2, flxDetails.CurrentRow.Index).Value
            cmbClarity.Text = flxDetails.Item(3, flxDetails.CurrentRow.Index).Value
            txtCode.Text = flxDetails.Item(4, flxDetails.CurrentRow.Index).Value
            cmbAssort.Text = flxDetails.Item(5, flxDetails.CurrentRow.Index).Value
            txtPktPcs.Text = flxDetails.Item(6, flxDetails.CurrentRow.Index).Value
            txtPktCts.Text = flxDetails.Item(7, flxDetails.CurrentRow.Index).Value
            cmbOrder.Text = flxDetails.Item(9, flxDetails.CurrentRow.Index).Value
            cmbSize.Text = flxDetails.Item(11, flxDetails.CurrentRow.Index).Value
        End If
    End Sub

    Private Sub flxDetails_DoubleClick(sender As Object, e As EventArgs) Handles flxDetails.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            txtTotPcs.Text = CDbl(txtTotPcs.Text) - CDbl(flxDetails.Item(6, flxDetails.CurrentRow.Index).Value)
            txtTotCts.Text = Format(CDbl(txtTotCts.Text) - CDbl(flxDetails.Item(7, flxDetails.CurrentRow.Index).Value), "#0.000")
            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)
        End If
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        If txtParNo.Text = "" Then MsgBox("Invalid Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtPktNo.Text = "" Then MsgBox("Invalid Packet No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtTrfPcs.Text) <> CDbl(txtTotPcs.Text) Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtTrfCts.Text) + 0.1 < CDbl(txtTotCts.Text) Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM dbo.tblGrading_RndSizingTypes WHERE (Department = 'Rounds') AND (ParNo = '" & txtParNo.Text & "') AND (PktNo = '" & txtPktNo.Text & "')", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then

            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblGrading_Parcel WHERE (ParNo = '" & txtParNo.Text & "')", dbConnNiru, 1, 1)
            If rsComSql_1.RecordCount = 0 Then
                dbConnNiru.Execute("INSERT INTO tblGrading_Parcel(ParNo,Complete) VALUES('" & txtParNo.Text & "',0)")
            End If
            rsComSql_1 = Nothing

            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblGrading_SizingPacket WHERE (ParNo = '" & txtParNo.Text & "') AND (PktNo = '" & txtPktNo.Text & "')", dbConnNiru, 1, 1)
            If rsComSql_1.RecordCount = 0 Then
                dbConnNiru.Execute("INSERT INTO tblGrading_SizingPacket(Department,ParNo,PktNo,SizeCode,PktPcs,PktCts,ReturnType1,ReturnType2,ReturnType3,ReturnType4,PktType) " & _
                                   "VALUES('Colombo','" & txtParNo.Text & "','" & txtPktNo.Text & "',''," & CDbl(txtTotPcs.Text) & "," & CDbl(txtTotCts.Text) & ",'','','','','N')")
            End If
            rsComSql_1 = Nothing

            dbConnNiru.Execute("DELETE FROM tblGrading_SizingTypes WHERE (ParNo = '" & txtParNo.Text & "') AND (PktNo = '" & txtPktNo.Text & "')")
            dbConnNiru.Execute("DELETE FROM tblGrading_Box WHERE (ParNo = '" & Mid(txtParNo.Text, 1, 6) & "') AND (Grp = '" & strRight(txtParNo.Text, 2) & "') AND (PktNo = '" & txtPktNo.Text & "')")

            For intRow = 0 To flxDetails.Rows.Count - 1
                AdoCN.Execute("INSERT INTO tblGrading_RndSizingTypes(Department,ParNo,PktNo,Sec,ReturnType1,ReturnType2,ReturnType3,ReturnType4,ReturnType5,ReturnType6,ReturnType7,Pcs,Cts,Price,OrderNo,Remarks,DocID) " & _
                              "VALUES('Rounds','" & txtParNo.Text & "','" & txtPktNo.Text & "',1,'" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & _
                                "'" & flxDetails.Item(2, intRow).Value & "','" & flxDetails.Item(3, intRow).Value & "','" & flxDetails.Item(4, intRow).Value & "','" & flxDetails.Item(5, intRow).Value & "','" & flxDetails.Item(11, intRow).Value & "'," & _
                                "'" & CDbl(flxDetails.Item(6, intRow).Value) & "','" & CDbl(flxDetails.Item(7, intRow).Value) & "','" & CDbl(flxDetails.Item(8, intRow).Value) & "','" & flxDetails.Item(9, intRow).Value & "','" & Trim(txtRemarks.Text) & "','" & flxDetails.Item(10, intRow).Value & "')")

                dbConnNiru.Execute("INSERT INTO tblGrading_SizingTypes(Department,ParNo,PktNo,Sec,ReturnType1,ReturnType2,ReturnType3,ReturnType4,ReturnType5,Pcs,Cts) " & _
                                   "VALUES('Colombo','" & txtParNo.Text & "','" & txtPktNo.Text & "',1,'" & flxDetails.Item(4, intRow).Value & "','" & flxDetails.Item(5, intRow).Value & "','" & flxDetails.Item(11, intRow).Value & "','',''," & CDbl(flxDetails.Item(6, intRow).Value) & "," & CDbl(flxDetails.Item(7, intRow).Value) & ")")

                If Len(flxDetails.Item(9, intRow).Value) > 0 Then
                    dbConnNiru.Execute("INSERT INTO tblGrading_Box(ParNo, Grp, PktNo, BoxNo, Pcs, Cts, FM, FM2, Assortment, OrderNo, DocID) " & _
                                       "VALUES('" & Mid(txtParNo.Text, 1, 6) & "','" & strRight(txtParNo.Text, 2) & "','" & txtPktNo.Text & "',1," & CDbl(flxDetails.Item(6, intRow).Value) & "," & _
                                            "" & CDbl(flxDetails.Item(7, intRow).Value) & ",0,1,'" & flxDetails.Item(5, intRow).Value & "','" & flxDetails.Item(9, intRow).Value & "','" & flxDetails.Item(10, intRow).Value & "')")
                End If
            Next
        Else
            PBResponse = MsgBox("Already entered. Do you want to Update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblGrading_Parcel WHERE (ParNo = '" & txtParNo.Text & "')", dbConnNiru, 1, 1)
                If rsComSql_1.RecordCount = 0 Then
                    dbConnNiru.Execute("INSERT INTO tblGrading_Parcel(ParNo,Complete) VALUES('" & txtParNo.Text & "',0)")
                End If
                rsComSql_1 = Nothing

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblGrading_SizingPacket WHERE (ParNo = '" & txtParNo.Text & "') AND (PktNo = '" & txtPktNo.Text & "')", dbConnNiru, 1, 1)
                If rsComSql_1.RecordCount = 0 Then
                    dbConnNiru.Execute("INSERT INTO tblGrading_SizingPacket(Department,ParNo,PktNo,SizeCode,PktPcs,PktCts,ReturnType1,ReturnType2,ReturnType3,ReturnType4,PktType) " & _
                                       "VALUES('Colombo','" & txtParNo.Text & "','" & txtPktNo.Text & "',''," & CDbl(txtTotPcs.Text) & "," & CDbl(txtTotCts.Text) & ",'','','','','N')")
                End If
                rsComSql_1 = Nothing

                AdoCN.Execute("DELETE FROM tblGrading_RndSizingTypes WHERE (Department = 'Rounds') AND (ParNo = '" & txtParNo.Text & "') AND (PktNo = '" & txtPktNo.Text & "')")
                dbConnNiru.Execute("DELETE FROM tblGrading_SizingTypes WHERE (ParNo = '" & txtParNo.Text & "') AND (PktNo = '" & txtPktNo.Text & "')")
                dbConnNiru.Execute("DELETE FROM tblGrading_Box WHERE (ParNo = '" & Mid(txtParNo.Text, 1, 6) & "') AND (Grp = '" & strRight(txtParNo.Text, 2) & "') AND (PktNo = '" & txtPktNo.Text & "')")

                For intRow = 0 To flxDetails.Rows.Count - 1
                    AdoCN.Execute("INSERT INTO tblGrading_RndSizingTypes(Department,ParNo,PktNo,Sec,ReturnType1,ReturnType2,ReturnType3,ReturnType4,ReturnType5,ReturnType6,ReturnType7,Pcs,Cts,Price,OrderNo,Remarks,DocID) " & _
                                  "VALUES('Rounds','" & txtParNo.Text & "','" & txtPktNo.Text & "',1,'" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & _
                                    "'" & flxDetails.Item(2, intRow).Value & "','" & flxDetails.Item(3, intRow).Value & "','" & flxDetails.Item(4, intRow).Value & "','" & flxDetails.Item(5, intRow).Value & "','" & flxDetails.Item(11, intRow).Value & "'," & _
                                    "'" & CDbl(flxDetails.Item(6, intRow).Value) & "','" & CDbl(flxDetails.Item(7, intRow).Value) & "','" & CDbl(flxDetails.Item(8, intRow).Value) & "','" & flxDetails.Item(9, intRow).Value & "','" & Trim(txtRemarks.Text) & "','" & flxDetails.Item(10, intRow).Value & "')")

                    dbConnNiru.Execute("INSERT INTO tblGrading_SizingTypes(Department,ParNo,PktNo,Sec,ReturnType1,ReturnType2,ReturnType3,ReturnType4,ReturnType5,Pcs,Cts) " & _
                                       "VALUES('Colombo','" & txtParNo.Text & "','" & txtPktNo.Text & "',1,'" & flxDetails.Item(4, intRow).Value & "','" & flxDetails.Item(5, intRow).Value & "','" & flxDetails.Item(11, intRow).Value & "','',''," & CDbl(flxDetails.Item(6, intRow).Value) & "," & CDbl(flxDetails.Item(7, intRow).Value) & ")")

                    If Len(flxDetails.Item(9, intRow).Value) > 0 Then
                        dbConnNiru.Execute("INSERT INTO tblGrading_Box(ParNo, Grp, PktNo, BoxNo, Pcs, Cts, FM, FM2, Assortment, OrderNo, DocID) " & _
                                           "VALUES('" & Mid(txtParNo.Text, 1, 6) & "','" & strRight(txtParNo.Text, 2) & "','" & txtPktNo.Text & "',1," & CDbl(flxDetails.Item(6, intRow).Value) & "," & _
                                                "" & CDbl(flxDetails.Item(7, intRow).Value) & ",0,1,'" & flxDetails.Item(5, intRow).Value & "','" & flxDetails.Item(9, intRow).Value & "','" & flxDetails.Item(10, intRow).Value & "')")
                    End If
                Next
            End If
        End If
        rsComSql = Nothing

        ClearPacket()
        txtPktNo.Focus()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        PBResponse = MsgBox("Are you sure to Save?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            Save()
        End If
    End Sub

    Private Sub cmbFlo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbFlo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbClarity.Focus()
        End If
    End Sub

    Private Sub cmbAssort_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbAssort.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtPktPcs.Focus()
        End If
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptGradingBoxNew.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub cmbOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbOrder.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            cmbSize.Text = "0"
            cmbSize.Focus()
        End If
    End Sub

    Private Sub cmbSize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSize.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub cmbAssort_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAssort.SelectedIndexChanged
        Load_SizeRange()
    End Sub
End Class