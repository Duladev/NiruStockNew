
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixSectionBoil
    Dim ParcelNo As String
    Dim PacketNo As String
    Dim issued As Boolean
    Dim Section As Integer
    Dim ICNo2 As String

    Private Sub ClearFields()
        txtParPkt.Text = ""
        txtEmp.Text = ""
        txtRetPcs.Text = ""
        txtIssPcs.Text = ""
        txtIssDate.Text = ""
        txtIssTime.Text = ""
        txtIssueID.Text = ""
        txtGroup.Text = ""
        txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
        txtRetTime.Text = Format(Date.Now, "HH:mm:ss")
        PictureBox1.Visible = False
        PictureBox2.Visible = False
        cmdParPkt.Focus()
    End Sub

    Private Sub ClearFields2()
        txtParPkt.Text = ""
        txtRetPcs.Text = ""
        txtIssPcs.Text = ""
        txtIssueID.Text = ""
        txtGroup.Text = ""
        txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
        txtRetTime.Text = Format(Date.Now, "HH:mm:ss")
        PictureBox1.Visible = False
        PictureBox2.Visible = False
        cmdParPkt.Focus()
    End Sub

    Private Sub Load_Section()
        cmbSection.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblMixSectionsBoil ORDER BY SecCode", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbSection.Items.Add(rsComSql.Fields("SecName").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Datavalid = False
        Parcel = True
        Instring = UCase(InputBox("Enter Par/Pkt Number"))
        ParcelLen = Len(Instring)
        If ParcelLen = 9 Then
            Datavalid = True

            ParcelNo = Mid(Instring, 1, ParcelLen - 3)
            PacketNo = strRight(Instring, 3)

        ElseIf ParcelLen = 10 Then
            Datavalid = True

            ParcelNo = Mid(Instring, 1, ParcelLen - 4)
            PacketNo = strRight(Instring, 4)
        End If

        If Datavalid = True Then
            txtParPkt.Text = ParcelNo & "/" & PacketNo
            cmdEmp.Enabled = True
            cmdEmp.Focus()
            'txtEmp.Text = ""
            ShowDetails()
        Else
            txtParPkt.Text = ""
            txtEmp.Text = ""
            cmdEmp.Enabled = False
        End If
    End Sub

    Private Sub cmdEmp_Click(sender As Object, e As EventArgs) Handles cmdEmp.Click
        Datavalid = False
        Parcel = False
        Instring = UCase(InputBox("Enter Emp No"))
        ParcelLen = Len(Instring)

        If CheckEmployee(Trim(Instring)) = True Then
            Datavalid = True
            ICNo = UCase(Trim(Instring))
        Else
            MsgBox("Invalid IC No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Datavalid = False
            ICNo = ""
            cmdEmp.Focus()
            Exit Sub
        End If
        txtEmp.Text = ICNo

        If issued = True Then
            If Trim(ICNo2) <> Trim(ICNo) Then
                If MsgBox("IC Numbers do not match. Proceed Anyway?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes Then
                    txtEmp.Text = ICNo
                    txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
                    txtRetTime.Text = Format(Date.Now, "HH:mm")
                    txtRetPcs.Focus()
                Else
                    txtEmp.Text = ""
                    Datavalid = False
                    ICNo = ""
                End If
            Else
                txtEmp.Text = ICNo
                txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
                txtRetTime.Text = Format(Date.Now, "HH:mm")
                txtRetPcs.Focus()
            End If
        Else
            txtEmp.Text = ICNo
            txtIssDate.Text = Format(Date.Now, "dd/MM/yyyy")
            txtIssTime.Text = Format(Date.Now, "HH:mm")
            txtIssPcs.Focus()
        End If
    End Sub

    Private Sub ShowDetails()
        Dim blnFound As Boolean
        Dim intPktPcs As Integer

        blnFound = False
        intPktPcs = 0
        cmdEmp.Focus()

        txtGroup.Text = ""
        rsComSql = New ADODB.Recordset
        mStrSQL = "SELECT * FROM dbo.tblMixPacket WHERE (PktOrdNo = '" & ParcelNo & "') AND (PktNo = '" & PacketNo & "')  AND (Ok = 1)"
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Packet is not verified", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
            Exit Sub
        Else
            txtGroup.Text = rsComSql.Fields("Grp").Value
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        mStrSQL = "SELECT ParNo,PktNo,(IssPcsT + IssPcsB) - (RetPcsT + RetPcsB + RejPcs + LostPcs) AS BalPcs " & _
                  "FROM dbo.VW_MIXFinishIss15 " & _
                  "WHERE ((IssPcsT + IssPcsB) - (RetPcsT + RetPcsB) > 0) AND (ParNo = '" & ParcelNo & "') AND (PktNo = '" & PacketNo & "')"
        rsComSql.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("BalPcs").Value) Then
                If rsComSql.Fields("BalPcs").Value > 0 Then
                    intPktPcs = rsComSql.Fields("BalPcs").Value
                    blnFound = True
                End If
            End If
        End If
        rsComSql = Nothing

        If blnFound = True Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT dbo.tblMixIssuesBoil.ParNo, dbo.tblMixIssuesBoil.PktNo, dbo.tblMixIssuesBoil.Sec, dbo.tblMixIssuesBoil.IssPcs - ISNULL(dbo.tblMixReturnsBoil.RetPcs, 0) AS BalPcs, " & _
                            "dbo.tblMixIssuesBoil.EmpNo, dbo.tblMixIssuesBoil.IssDate, dbo.tblMixIssuesBoil.IssTime, dbo.tblMixIssuesBoil.ID " & _
                          "FROM dbo.tblMixIssuesBoil LEFT OUTER JOIN dbo.tblMixReturnsBoil ON dbo.tblMixIssuesBoil.ID = dbo.tblMixReturnsBoil.IssueID AND dbo.tblMixIssuesBoil.ParNo = dbo.tblMixReturnsBoil.ParNo AND " & _
                            "dbo.tblMixIssuesBoil.PktNo = dbo.tblMixReturnsBoil.PktNo AND dbo.tblMixIssuesBoil.Sec = dbo.tblMixReturnsBoil.Sec " & _
                          "WHERE (dbo.tblMixIssuesBoil.ParNo = '" & ParcelNo & "') AND (dbo.tblMixIssuesBoil.PktNo = '" & PacketNo & "') AND (dbo.tblMixIssuesBoil.IssPcs - ISNULL(dbo.tblMixReturnsBoil.RetPcs, 0) > 0)", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                issued = True
                txtIssPcs.Text = rsComSql.Fields("BalPcs").Value
                txtIssDate.Text = Format(rsComSql.Fields("IssDate").Value, "dd/MM/yyyy")
                txtIssTime.Text = Format(rsComSql.Fields("IssTime").Value, "HH:mm")
                ICNo2 = rsComSql.Fields("EmpNo").Value
                txtIssueID.Text = rsComSql.Fields("ID").Value
                txtEmp.Text = ICNo2

                txtRetPcs.Text = rsComSql.Fields("BalPcs").Value
                PictureBox1.Visible = True
                PictureBox2.Visible = True
                Section = rsComSql.Fields("Sec").Value
                cmbSection.SelectedIndex = Section - 1
                cmdEmp.Focus()
            Else
                txtIssPcs.Text = intPktPcs

                issued = False
                PictureBox2.Visible = True
                Section = 0
                cmbSection.SelectedIndex = Section
                cmdEmp.Focus()
            End If
            rsComSql = Nothing
        Else
            MsgBox("Invalid Packet", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
            Exit Sub
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub RecSave()
        Dim dataok As Boolean
        Dim stiss, stret As Double

        dataok = True
        If issued Then
            If Len(txtRetDate.Text) < 2 Then Exit Sub
            
            stiss = CDbl(txtIssPcs.Text)
            stret = CDbl(txtRetPcs.Text)

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT PktOrdNo,PktNo,PktPcs FROM tblMixPacket WHERE PktOrdNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Ok = 1", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If rsComSql.Fields("PktPcs").Value < stiss Then
                    dataok = False
                    MsgBox("Stones issued " & stiss & " Stones Packeted " & rsComSql.Fields("PktPcs").Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    If dataok = False Then Exit Sub
                End If
            End If
            rsComSql = Nothing


            If stiss < stret Then
                dataok = False
                MsgBox("Stones issued " & stiss & "   Stones returned " & stret, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                If dataok = False Then Exit Sub
            End If

            If stret <= 0 Then
                MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Else
            If Len(txtIssDate.Text) < 2 Then dataok = False
            If Not CInt(txtIssPcs.Text) > 0 Then dataok = False

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT PktOrdNo,PktNo,PktPcs FROM tblMixPacket WHERE PktOrdNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Ok = 1", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If rsComSql.Fields("PktPcs").Value < CDbl(txtIssPcs.Text) Then
                    dataok = False
                    MsgBox("Stones issued " & CDbl(txtIssPcs.Text) & " Stones Packeted " & rsComSql.Fields("PktPcs").Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    If dataok = False Then Exit Sub
                End If
            End If
            rsComSql = Nothing
        End If
        If dataok = True Then
            DataSave()
        End If
        cmdParPkt.Focus()

    End Sub

    Private Sub DataSave()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT PktOrdNo,PktNo FROM tblMixPacket WHERE PktOrdNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Ok = 1", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Please check the packet details", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        If Len(Trim(txtEmp.Text)) <> 6 Then
            MsgBox("Invalid Emp No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM VW_EMP_MASTER_SMALL2 WHERE FullEmpNo = '" & Trim(txtEmp.Text) & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Emp No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        If cmbSection.SelectedIndex < 0 Then
            MsgBox("Invalid Section", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If issued = False Then
            mStrSQL = "INSERT INTO tblMixIssuesBoil(ParNo,PktNo,Sec,IssPcs,EmpNo,IssDate,IssTime,EmpNo2) " & _
                      "VALUES('" & ParcelNo & "','" & PacketNo & "'," & cmbSection.SelectedIndex + 1 & "," & CInt(txtIssPcs.Text) & ",'" & Mid(Trim(txtEmp.Text), 1, 6) & "'," & _
                        "'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & PBUser_EmpNo & "')"

            AdoCN.Execute(mStrSQL)
        Else
            mStrSQL = "INSERT INTO tblMixReturnsBoil(IssueID,ParNo,PktNo,Sec,RetPcs,EmpNo,RetDate,RetTime,EmpNo2) " & _
                      "VALUES('" & CDbl(txtIssueID.Text) & "','" & ParcelNo & "','" & PacketNo & "'," & Section & "," & CInt(txtRetPcs.Text) & ",'" & Mid(Trim(txtEmp.Text), 1, 6) & "'," & _
                        "'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & PBUser_EmpNo & "')"

            AdoCN.Execute(mStrSQL)
        End If
        ClearFields2()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        RecSave()
    End Sub

    Private Sub frm_MixSectionBoil_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Section()
        ClearFields()
    End Sub

    Private Sub txtIssPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIssPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub
End Class