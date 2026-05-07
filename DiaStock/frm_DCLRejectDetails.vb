
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLRejectDetails
    Dim ParcelNo As String
    Dim PacketNo As String

    Private Sub frm_DCLRejectDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F5
                Save()
        End Select
    End Sub

    Private Sub frm_DCLRejectDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Department(cmbDept)
        Load_RejReasons()
    End Sub

    Private Sub Load_RejReasons()
        cmbReason.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblMixRejReasons ORDER BY RejReason", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbReason.Items.Add(rsComSql.Fields("RejReason").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Section()
        cmbSection.Items.Clear()
        rsComSql = New ADODB.Recordset
        Select Case cmbDept.Text
            Case "Baguettes"
                rsComSql.Open("SELECT * FROM tblBAGSections ORDER BY SecCode", AdoCN, 1, 1)
            Case "Princess"
                rsComSql.Open("SELECT * FROM tblPRSections ORDER BY SecCode", AdoCN, 1, 1)
            Case "Rounds"
                rsComSql.Open("SELECT * FROM tblRndSections ORDER BY SecCode", AdoCN, 1, 1)
            Case "Lamour", "Carrer", "Davinci", "Emerald"
                rsComSql.Open("SELECT * FROM tblExtSections WHERE Department = '" & cmbDept.Text & "' ORDER BY SecCode", AdoCN, 1, 1)
            Case Else
                rsComSql.Open("SELECT * FROM tblRprSections WHERE Department = '" & cmbDept.Text & "' ORDER BY SecCode", AdoCN, 1, 1)
        End Select
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbSection.Items.Add(rsComSql.Fields("SecName").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmbDept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDept.SelectedIndexChanged
        Load_Section()
    End Sub

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Datavalid = False
        Parcel = True
        Instring = UCase(InputBox("Enter Par/Pkt Number"))
        ParcelLen = Len(Instring)
        If ParcelLen > 9 Then
            Datavalid = True

            If Mid(cmbDept.Text, 1, 5) = "Rough" Then
                ParcelNo = Mid(Instring, 1, ParcelLen - 4)
                PacketNo = strRight(Instring, 4)
            Else
                ParcelNo = Mid(Instring, 1, ParcelLen - 3)
                PacketNo = strRight(Instring, 3)
            End If
            
        End If

        If Datavalid = True Then
            txtParPkt.Text = ParcelNo & "/" & PacketNo
            cmdEmp.Enabled = True
            cmdEmp.Focus()
            txtEmp.Text = ""
            ShowDetails()
        Else
            txtParPkt.Text = ""
            txtEmp.Text = ""
            cmdEmp.Enabled = False
        End If
    End Sub

    Private Sub ShowDetails()
        cmdAdd.Enabled = False
        rsComSql = New ADODB.Recordset
        Select Case cmbDept.Text
            Case "Baguettes"
                rsComSql.Open("SELECT * FROM tblBAGPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "'", AdoCN, 1, 1)
            Case "Princess"
                rsComSql.Open("SELECT * FROM tblPRPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "'", AdoCN, 1, 1)
            Case "Rounds"
                rsComSql.Open("SELECT * FROM tblRndPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "'", AdoCN, 1, 1)
            Case "Lamour", "Carrer", "Davinci", "Emerald"
                rsComSql.Open("SELECT * FROM tblExtPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
            Case Else
                rsComSql.Open("SELECT * FROM tblRprPacket WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "'", AdoCN, 1, 1)
        End Select
        If rsComSql.RecordCount > 0 Then

        Else
            Exit Sub
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        Select Case cmbDept.Text
            Case "Baguettes"
                rsComSql.Open("SELECT ParNo FROM tblBAGReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
            Case "Princess"
                rsComSql.Open("SELECT ParNo FROM tblPRReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
            Case "Rounds"
                rsComSql.Open("SELECT ParNo FROM tblRndReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
            Case "Lamour", "Carrer", "Davinci", "Emerald"
                rsComSql.Open("SELECT ParNo FROM tblExtReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & " AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
            Case Else
                rsComSql.Open("SELECT * FROM tblRprReturns WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
        End Select
        If rsComSql.RecordCount Then
            flxDetails.Rows.Clear()
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblDCLReturnDetails WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                rsComSql_1.MoveFirst()
                While Not rsComSql_1.EOF
                    flxDetails.Rows.Add(rsComSql_1.Fields("RejReason").Value,
                                        rsComSql_1.Fields("RejPcs").Value,
                                        rsComSql_1.Fields("RejCts").Value,
                                        rsComSql_1.Fields("EmpNo").Value,
                                        Format(rsComSql_1.Fields("RejDate").Value, "yyyy/MM/dd"),
                                        rsComSql_1.Fields("RejValue").Value,
                                        rsComSql_1.Fields("ID").Value)

                    rsComSql_1.MoveNext()
                End While
            Else
                cmdAdd.Enabled = True
            End If
            rsComSql_1 = Nothing

            PictureBox1.Visible = True
        Else
            MsgBox("Invalid Parcel/Packet/Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing

    End Sub

    Private Sub cmdEmp_Click(sender As Object, e As EventArgs) Handles cmdEmp.Click
        Datavalid = False
        Parcel = False
        Instring = UCase(InputBox("Enter Emp No"))
        ParcelLen = Len(Instring)
        If ParcelLen = 6 Then
            Datavalid = True

            rsComSql = New ADODB.Recordset
            mStrSQL = ("SELECT * FROM VW_EMP_MASTER_SMALL3 WHERE FullEmpNo = '" & Trim(Instring) & "'")
            rsComSql.Open(mStrSQL, AdoCN, 1, 1)
            ICNo = ""
            If Not rsComSql.EOF Then
                Datavalid = True
                ICNo = UCase(Trim(Instring))
            Else
                MsgBox("Invalid IC No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Datavalid = False
                ICNo = ""
                cmdEmp.Focus()
                Exit Sub
            End If
            rsComSql = Nothing
            txtEmp.Text = ICNo
        Else
            MsgBox("Invalid IC No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Datavalid = False
            ICNo = ""
            cmdEmp.Focus()
            Exit Sub
        End If

        txtEmp.Text = ICNo
        txtIssDate.Text = Format(Date.Now, "dd/MM/yyyy")
        txtIssTime.Text = Format(Date.Now, "HH:mm")
        txtPcs.Focus()
    End Sub

    Private Sub ClearText()
        cmbDept.Text = ""
        txtParPkt.Text = ""
        txtEmp.Text = ""
        cmbSection.Text = ""
        cmbSection.Items.Clear()
        txtPcs.Text = ""
        txtCts.Text = ""
        txtValue.Text = ""
        txtIssDate.Text = ""
        txtIssTime.Text = ""
        cmbReason.Text = ""
        flxDetails.Rows.Clear()
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
            txtValue.Focus()
        End If
    End Sub

    Private Sub txtValue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtValue.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtValue.Text)
        If Asc(e.KeyChar) = 13 Then
            cmbReason.Focus()
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        If txtPcs.Text = "" Then Exit Sub
        If txtCts.Text = "" Then Exit Sub
        If txtValue.Text = "" Then Exit Sub
        If cmbReason.Text = "" Then Exit Sub

        If CInt(txtPcs.Text) <= 0 Then Exit Sub
        If CDbl(txtCts.Text) <= 0 Then Exit Sub
        If CDbl(txtValue.Text) <= 0 Then Exit Sub

        flxDetails.Rows.Add(cmbReason.Text,
                            txtPcs.Text,
                            txtCts.Text,
                            txtEmp.Text,
                            txtIssDate.Text,
                            txtValue.Text)

        txtPcs.Text = ""
        txtCts.Text = ""
        txtValue.Text = ""
        cmbReason.Text = ""
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearText()
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        rsComSql = New ADODB.Recordset
        Select Case cmbDept.Text
            Case "Baguettes"
                rsComSql.Open("SELECT * FROM tblBAGPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "'", AdoCN, 1, 1)
            Case "Princess"
                rsComSql.Open("SELECT * FROM tblPRPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "'", AdoCN, 1, 1)
            Case "Rounds"
                rsComSql.Open("SELECT * FROM tblRndPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "'", AdoCN, 1, 1)
            Case "Lamour", "Carrer", "Davinci", "Emerald"
                rsComSql.Open("SELECT * FROM tblExtPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
            Case Else
                rsComSql.Open("SELECT * FROM tblRprPacket WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "'", AdoCN, 1, 1)
        End Select
        If rsComSql.RecordCount > 0 Then
        Else
            MsgBox("Invalid Parcel/Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        rsComSql = New ADODB.Recordset
        Select Case cmbDept.Text
            Case "Baguettes"
                rsComSql.Open("SELECT ParNo FROM tblBAGReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
            Case "Princess"
                rsComSql.Open("SELECT ParNo FROM tblPRReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
            Case "Rounds"
                rsComSql.Open("SELECT ParNo FROM tblRndReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
            Case "Lamour", "Carrer", "Davinci", "Emerald"
                rsComSql.Open("SELECT ParNo FROM tblExtReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & " AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
            Case Else
                rsComSql.Open("SELECT * FROM tblRprReturns WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
        End Select
        If rsComSql.RecordCount Then
        Else
            MsgBox("Invalid Parcel/Packet/Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT ParNo FROM tblDCLReturnDetails WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        Else
            For intRow = 0 To flxDetails.Rows.Count - 1
                AdoCN.Execute("INSERT INTO tblDCLReturnDetails(Department,ParNo,PktNo,Sec,EmpNo,RejPcs,RejCts,RejDate,RghCts,RejReason,RejValue) " & _
                              "VALUES('" & cmbDept.Text & "','" & ParcelNo & "','" & PacketNo & "'," & cmbSection.SelectedIndex + 1 & "," & _
                                "'" & flxDetails.Item(3, intRow).Value & "'," & CInt(flxDetails.Item(1, intRow).Value) & "," & _
                                "" & CDbl(flxDetails.Item(2, intRow).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "'," & _
                                "" & CDbl(flxDetails.Item(2, intRow).Value) & ",'" & flxDetails.Item(0, intRow).Value & "'," & CDbl(flxDetails.Item(5, intRow).Value) & ")")
            Next
        End If
        rsComSql = Nothing

        ClearText()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub Delete()
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT ParNo FROM tblDCLReturnDetails WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("No Records", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            Else
                For intRow = 0 To flxDetails.Rows.Count - 1
                    If Len(flxDetails.Item(6, intRow).Value) > 0 Then
                        AdoCN.Execute("DELETE FROM tblDCLReturnDetails WHERE ID = " & CDbl(flxDetails.Item(6, intRow).Value) & "")
                    End If
                Next
            End If
            rsComSql = Nothing

            ClearText()
        End If
        
    End Sub

    Private Sub cmbReason_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbReason.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Delete()
    End Sub
End Class