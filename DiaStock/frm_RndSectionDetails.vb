
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RndSectionDetails
    Dim ParcelNo As String
    Dim PacketNo As String
    Dim intPrepSec As Integer
    Dim intPolSec As Integer

    Private Sub ClearFields()
        txtParPkt.Text = ""
        cmbSize.Text = ""
        cmbSize2.Text = ""
        txtPcs.Text = ""
        txtPcs2.Text = ""
        txtPrepPcs.Text = ""
        txtPolPcs.Text = ""
        txtTotPrepPcs.Text = ""
        txtTotPolPcs.Text = ""
        flxDetails.Rows.Clear()
        picPrep.Visible = False
        picPol.Visible = False
        cmdParPkt.Focus()
    End Sub

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Datavalid = False
        Parcel = True
        Instring = UCase(InputBox("Enter Par/Pkt Number"))
        ParcelLen = Len(Instring)
        If ParcelLen = 11 Then
            Datavalid = True

            ParcelNo = Mid(Instring, 1, ParcelLen - 3)
            PacketNo = strRight(Instring, 3)
        Else
            Datavalid = True

            ParcelNo = Mid(Instring, 1, ParcelLen - 4)
            PacketNo = strRight(Instring, 4)
        End If

        If Datavalid = True Then
            txtParPkt.Text = ParcelNo & "/" & PacketNo
            ShowDetails()
        Else
            txtParPkt.Text = ""
            cmdParPkt.Focus()
        End If
    End Sub

    Private Sub Load_Size()
        cmbSize.Items.Clear()
        cmbSize2.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblRndSizeRangePol ORDER BY SizeRange", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbSize.Items.Add(rsComSql.Fields("SizeRange").Value)
                cmbSize2.Items.Add(rsComSql.Fields("SizeRange").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub frm_RndSectionDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        intPrepSec = 14
        intPolSec = 24

        ClearFields()
        Load_Size()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ShowDetails()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblRndPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND DelDate IS NOT NULL AND AccDate IS NOT NULL", AdoCN, 1, 1)
        If rsComSql.RecordCount > 0 Then
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT SUM(RetPcsT + RetPcsB) AS RetPcs, SUM(RetCts) as RetCts FROM tblRndReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & intPrepSec & "", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                If Not IsDBNull(rsComSql_1.Fields("RetPcs").Value) Then
                    txtPrepPcs.Text = rsComSql_1.Fields("RetPcs").Value
                    picPrep.Visible = True
                Else
                    picPrep.Visible = False
                End If
            Else
                picPrep.Visible = False
            End If
            rsComSql_1 = Nothing

            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT SUM(RetPcsT + RetPcsB) AS RetPcs, SUM(RetCts) as RetCts FROM tblRndReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & intPolSec & "", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                If Not IsDBNull(rsComSql_1.Fields("RetPcs").Value) Then
                    txtPolPcs.Text = rsComSql_1.Fields("RetPcs").Value
                    picPol.Visible = True
                Else
                    picPol.Visible = False
                End If
            Else
                picPol.Visible = False
            End If
            rsComSql_1 = Nothing

            flxDetails.Rows.Clear()
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblRndReturnsDetails WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & intPrepSec & " ORDER BY SizeRange", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                rsComSql_1.MoveFirst()
                While Not rsComSql_1.EOF
                    flxDetails.Rows.Add(rsComSql_1.Fields("SizeRange").Value,
                                        rsComSql_1.Fields("Pcs").Value)

                    rsComSql_1.MoveNext()
                End While
            End If
            rsComSql_1 = Nothing

            txtTotPrepPcs.Text = CalTotalPcs(flxDetails)

            flxPolish.Rows.Clear()
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblRndReturnsDetails WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & intPolSec & " ORDER BY SizeRange", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                rsComSql_1.MoveFirst()
                While Not rsComSql_1.EOF
                    flxPolish.Rows.Add(rsComSql_1.Fields("SizeRange").Value,
                                       rsComSql_1.Fields("Pcs").Value)

                    rsComSql_1.MoveNext()
                End While
            End If
            rsComSql_1 = Nothing

            txtTotPolPcs.Text = CalTotalPcs(flxPolish)
        Else
            MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(1, intRow).Value)
        Next

    End Function

    Private Sub cmbSize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSize.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtPcs.Focus()
        End If
    End Sub

    Private Sub txtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub cmbSize2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSize2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtPcs2.Focus()
        End If
    End Sub

    Private Sub txtPcs2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs2.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            cmdAdd2.Focus()
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim intRow As Integer

        If cmbSize.Text = "" Then MsgBox("Invalid Size Range", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, Me.Text) : Exit Sub
        If txtPcs.Text = "" Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, Me.Text) : Exit Sub
        If CDbl(txtPcs.Text) <= 0 Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, Me.Text) : Exit Sub

        If CDbl(txtPrepPcs.Text) < CDbl(txtTotPrepPcs.Text) + CDbl(txtPcs.Text) Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, Me.Text) : Exit Sub

        For intRow = 0 To flxDetails.Rows.Count - 1
            If cmbSize.Text = flxDetails.Item(0, intRow).Value Then
                MsgBox("Size Range Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        flxDetails.Rows.Add(cmbSize.Text,
                            txtPcs.Text)

        cmbSize.Text = ""
        txtPcs.Text = ""
        cmbSize.Focus()

        txtTotPrepPcs.Text = CalTotalPcs(flxDetails)
    End Sub

    Private Sub cmdAdd2_Click(sender As Object, e As EventArgs) Handles cmdAdd2.Click
        Dim intRow As Integer

        If cmbSize2.Text = "" Then MsgBox("Invalid Size Range", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, Me.Text) : Exit Sub
        If txtPcs2.Text = "" Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, Me.Text) : Exit Sub
        If CDbl(txtPcs2.Text) <= 0 Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, Me.Text) : Exit Sub

        If CDbl(txtPolPcs.Text) < CDbl(txtTotPolPcs.Text) + CDbl(txtPcs2.Text) Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, Me.Text) : Exit Sub

        For intRow = 0 To flxPolish.Rows.Count - 1
            If cmbSize2.Text = flxPolish.Item(0, intRow).Value Then
                MsgBox("Size Range Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        flxPolish.Rows.Add(cmbSize2.Text,
                           txtPcs2.Text)

        cmbSize2.Text = ""
        txtPcs2.Text = ""
        cmbSize2.Focus()

        txtTotPolPcs.Text = CalTotalPcs(flxPolish)
    End Sub

    Private Sub flxDetails_DoubleClick(sender As Object, e As EventArgs) Handles flxDetails.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)
            txtTotPrepPcs.Text = CalTotalPcs(flxDetails)
        End If
    End Sub

    Private Sub flxPolish_DoubleClick(sender As Object, e As EventArgs) Handles flxPolish.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxPolish.Rows.RemoveAt(flxPolish.CurrentRow.Index)
            txtTotPolPcs.Text = CalTotalPcs(flxPolish)
        End If
    End Sub

    Private Sub SavePrep()
        Dim intRow As Integer

        If CDbl(txtPrepPcs.Text) <> CDbl(txtTotPrepPcs.Text) Then Exit Sub

        dtpToday = GetToday()

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblRndReturnsDetails WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & intPrepSec & "", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                For intRow = 0 To flxDetails.Rows.Count - 1
                    AdoCN.Execute("INSERT INTO tblRndReturnsDetails(ParNo, PktNo, Sec, SizeRange, Pcs, RetDate, RetTime) " & _
                                  "VALUES('" & ParcelNo & "','" & PacketNo & "'," & intPrepSec & ",'" & flxDetails.Item(0, intRow).Value & "'," & _
                                    "'" & CDbl(flxDetails.Item(1, intRow).Value) & "','" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")
                Next
                MsgBox("Size Range Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Else
                PBResponse = MsgBox("Already Eneterd. Do you want to Update", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
                If PBResponse = MsgBoxResult.Yes Then
                    AdoCN.Execute("DELETE FROM tblRndReturnsDetails WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & intPrepSec & "")
                    For intRow = 0 To flxDetails.Rows.Count - 1
                        AdoCN.Execute("INSERT INTO tblRndReturnsDetails(ParNo, PktNo, Sec, SizeRange, Pcs, RetDate, RetTime) " & _
                                      "VALUES('" & ParcelNo & "','" & PacketNo & "'," & intPrepSec & ",'" & flxDetails.Item(0, intRow).Value & "'," & _
                                        "'" & CDbl(flxDetails.Item(1, intRow).Value) & "','" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")
                    Next
                    MsgBox("Size Range Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                End If
            End If
            rsComSql = Nothing
        End If
        ClearFields()
    End Sub

    Private Sub SavePol()
        Dim intRow As Integer

        If CDbl(txtPolPcs.Text) <> CDbl(txtTotPolPcs.Text) Then Exit Sub

        dtpToday = GetToday()

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblRndReturnsDetails WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & intPolSec & "", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                For intRow = 0 To flxPolish.Rows.Count - 1
                    AdoCN.Execute("INSERT INTO tblRndReturnsDetails(ParNo, PktNo, Sec, SizeRange, Pcs, RetDate, RetTime) " & _
                                  "VALUES('" & ParcelNo & "','" & PacketNo & "'," & intPolSec & ",'" & flxPolish.Item(0, intRow).Value & "'," & _
                                    "'" & CDbl(flxPolish.Item(1, intRow).Value) & "','" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")
                Next
                MsgBox("Size Range Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Else
                PBResponse = MsgBox("Already Eneterd. Do you want to Update", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
                If PBResponse = MsgBoxResult.Yes Then
                    AdoCN.Execute("DELETE FROM tblRndReturnsDetails WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & intPolSec & "")
                    For intRow = 0 To flxPolish.Rows.Count - 1
                        AdoCN.Execute("INSERT INTO tblRndReturnsDetails(ParNo, PktNo, Sec, SizeRange, Pcs, RetDate, RetTime) " & _
                                      "VALUES('" & ParcelNo & "','" & PacketNo & "'," & intPolSec & ",'" & flxPolish.Item(0, intRow).Value & "'," & _
                                        "'" & CDbl(flxPolish.Item(1, intRow).Value) & "','" & Format(dtpToday, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")
                    Next
                    MsgBox("Size Range Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                End If
            End If
            rsComSql = Nothing
        End If
        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        SavePrep()
    End Sub

    Private Sub cmdSave2_Click(sender As Object, e As EventArgs) Handles cmdSave2.Click
        SavePol()
    End Sub
End Class