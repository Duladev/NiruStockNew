
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDSizingModify

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
        txtAssortment.Text = ""
        cmbSize.Text = ""
        txtRejPcs1.Text = "0"
        txtRejCts1.Text = "0"
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Function ParcelFound(ByVal strDept As String, ByVal strParceNo As String) As Boolean
        ParcelFound = True
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblGrading_SizingPacket WHERE ParNo = '" & strParceNo & "' AND Department = '" & strDept & "'", AdoCN, 1, 1)
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
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.tblGrading_SizingPacket.Department, dbo.tblGrading_SizingPacket.ParNo, dbo.tblGrading_SizingPacket.PktNo, dbo.tblGrading_SizingTypes.ReturnType AS Assortment, dbo.tblGrading_SizingTypes.Pcs, " & _
                            "dbo.tblGrading_SizingTypes.Cts, dbo.tblGrading_SizingTypes.ID, dbo.tblGrading_SizingTypes.SizeRange " & _
                        "FROM dbo.tblGrading_SizingPacket INNER JOIN dbo.tblGrading_SizingTypes ON dbo.tblGrading_SizingPacket.Department = dbo.tblGrading_SizingTypes.Department AND dbo.tblGrading_SizingPacket.ParNo = dbo.tblGrading_SizingTypes.ParNo AND " & _
                            "dbo.tblGrading_SizingPacket.PktNo = dbo.tblGrading_SizingTypes.PktNo " & _
                        "WHERE (dbo.tblGrading_SizingTypes.OK = 0) AND (dbo.tblGrading_SizingPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblGrading_SizingPacket.ParNo = '" & txtParNo.Text & "') AND (dbo.tblGrading_SizingPacket.PktNo = '" & txtPktNo.Text & "') " & _
                        "ORDER BY Assortment", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            blnFound = True
            While Not rsComSql_1.EOF
                flxDetails.Rows.Add(rsComSql_1.Fields("ParNo").Value,
                                    rsComSql_1.Fields("PktNo").Value,
                                    rsComSql_1.Fields("Assortment").Value,
                                    rsComSql_1.Fields("Pcs").Value,
                                    rsComSql_1.Fields("Cts").Value,
                                    True,
                                    rsComSql_1.Fields("ID").Value,
                                    rsComSql_1.Fields("SizeRange").Value)

                rsComSql_1.MoveNext()
            End While
        Else
            MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql_1 = Nothing

        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblGrading_SizingReturns WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            txtTotPcs.Text = rsComSql_1.Fields("RetPcs").Value
            txtTotCts.Text = rsComSql_1.Fields("RetCts").Value
        End If
        rsComSql_1 = Nothing

        txtPcs.Text = CalTotalPcs()
        txtCts.Text = CalTotalCts()
    End Sub

    Private Sub txtAssortment_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAssortment.KeyPress
        Dim strAssortment As String

        If Asc(e.KeyChar) = 13 Then

            strAssortment = Trim(txtAssortment.Text)

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_SizingList WHERE NAME = '" & strAssortment & "' AND Active = 1", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                txtNewPcs.Focus()
            Else
                MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
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

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtAssortment.Text = "" Then MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtNewPcs.Text = "" Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtNewCts.Text = "" Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtNewPcs.Text) <= 0 Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtNewCts.Text) <= 0 Then MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If CDbl(txtPcs.Text) + CDbl(txtNewPcs.Text) > CDbl(txtTotPcs.Text) Then MsgBox("Invalid Total Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        If CDbl(txtTotCts.Text) + 0.15 < Math.Round(CDbl(txtCts.Text) + CDbl(txtNewCts.Text), 3) Then
            MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If CDbl(txtTotCts.Text) - 0.15 > Math.Round(CDbl(txtCts.Text) + CDbl(txtNewCts.Text), 3) Then
            MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        For intRow = 0 To flxDetails.Rows.Count - 1
            If txtAssortment.Text = flxDetails.Item(2, intRow).Value Then
                MsgBox("Assortment already entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        dblPrice = 0
        rsComSql_4 = New ADODB.Recordset
        rsComSql_4.Open("SELECT * FROM tblGrading_SizingList WHERE NAME = '" & txtAssortment.Text & "' AND Active = 1", AdoCN, 1, 1)
        If rsComSql_4.RecordCount Then
            
        Else
            MsgBox("Invalid Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql_4 = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblGrading_SizeListRange WHERE Size = '" & Trim(cmbSize.Text) & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Size Range", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        flxDetails.Rows.Add(txtParNo.Text,
                            txtPktNo.Text,
                            UCase(txtAssortment.Text),
                            txtNewPcs.Text,
                            txtNewCts.Text,
                            True,
                            0,
                            cmbSize.Text)

        txtPcs.Text = CalTotalPcs()
        txtCts.Text = CalTotalCts()

        txtAssortment.Text = ""
        cmbSize.Text = ""
        txtNewPcs.Text = ""
        txtNewCts.Text = ""

        txtAssortment.Focus()
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        txtPcs.Text = CalTotalPcs()
        txtCts.Text = CalTotalCts()

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtParNo.Text = "" Then MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtPktNo.Text = "" Then MsgBox("Invalid Packet No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If flxDetails.Rows.Count < 1 Then MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If CDbl(txtPcs.Text) <> CDbl(txtTotPcs.Text) Then MsgBox("Pcs not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

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
            rsComSql_4.Open("SELECT * FROM tblGrading_SizingList WHERE NAME = '" & flxDetails.Item(2, intRow).Value & "' AND Active = 1", AdoCN, 1, 1)
            If rsComSql_4.RecordCount Then
                
            Else
                MsgBox("Invalid Assortment - " & flxDetails.Item(2, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql_4 = Nothing
        Next

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblGrading_SizingTypes WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND OK = 0", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            PBResponse = MsgBox("Are you sure to Update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                AdoCN.Execute("DELETE FROM tblGrading_SizingTypes WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND OK = 0")
                For intRow = 0 To flxDetails.Rows.Count - 1
                    If flxDetails.Item(5, intRow).Value = True Then
                        AdoCN.Execute("INSERT INTO tblExpSizingTypes(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts,OK,BasePrice,EstCts) " & _
                                      "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "',1,'" & UCase(flxDetails.Item(2, intRow).Value) & "'," & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",0," & CDbl(flxDetails.Item(5, intRow).Value) & "," & CDbl(flxDetails.Item(8, intRow).Value) & ")")

                        AdoCN.Execute("INSERT INTO tblGrading_SizingTypes(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts,OK,Color,Clarity,SizeRange) " & _
                                      "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "',1," & _
                                        "'" & UCase(flxDetails.Item(2, intRow).Value) & "'," & CInt(flxDetails.Item(3, intRow).Value) & "," & CDbl(flxDetails.Item(4, intRow).Value) & ",0," & _
                                        "'','','" & flxDetails.Item(7, intRow).Value & "')")
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

    Private Sub Load_SizeRange()

        cmbSize.Items.Clear()
        rsComSql_4 = New ADODB.Recordset
        rsComSql_4.Open("SELECT * FROM tblGrading_SizeListRange ORDER BY Size", AdoCN, 1, 1)
        If rsComSql_4.RecordCount Then
            rsComSql_4.MoveFirst()
            While Not rsComSql_4.EOF
                cmbSize.Items.Add(rsComSql_4.Fields("Size").Value)
                rsComSql_4.MoveNext()
            End While
        End If
        rsComSql_4 = Nothing
    End Sub

    Private Sub frm_GRDSizingModify_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_DepartmentProd(cmbDept)
        Load_SizeRange()

        txtPcs.Text = "0"
        txtCts.Text = "0"
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
    End Sub
End Class