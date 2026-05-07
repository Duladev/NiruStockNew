
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDSizingSection
    Dim issued As Boolean
    Dim Checked As Boolean
    Dim ICNo2 As String
    Dim Section As Integer
    Dim strMsg As String

    Private Sub frm_GRDSizingSection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        PictureBox1.Visible = False
        PictureBox2.Visible = False

        Load_Section()
        Load_Department(cmbDept)
        Load_Assortments()
        Load_SizeRange()
        Load_Color()
        Load_Clarity()

        ClearFields()
    End Sub

    Private Sub Load_Section()
        Dim recsection As ADODB.Recordset

        cmbSection.Items.Clear()
        recsection = New ADODB.Recordset
        recsection.Open("SELECT * FROM tblGrading_Sections2 WHERE SecCode = 2 ORDER BY SecCode", AdoCN, 1, 1)
        If recsection.RecordCount Then
            recsection.MoveFirst()
            While Not recsection.EOF
                cmbSection.Items.Add(recsection.Fields("SecName").Value)
                recsection.MoveNext()
            End While
        End If
        recsection = Nothing
        cmbSection.SelectedIndex = 0
    End Sub

    Private Sub Load_Assortments()

        cmbAssortType.Items.Clear()
        rsComSql_4 = New ADODB.Recordset
        rsComSql_4.Open("SELECT * FROM tblGrading_SizingList WHERE OK = 1 AND NOT Name LIKE 'A%' AND NOT Name LIKE 'T%' ORDER BY NAME", AdoCN, 1, 1)
        If rsComSql_4.RecordCount Then
            rsComSql_4.MoveFirst()
            While Not rsComSql_4.EOF
                cmbAssortType.Items.Add(rsComSql_4.Fields("NAME").Value)
                rsComSql_4.MoveNext()
            End While
        End If
        rsComSql_4 = Nothing
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

    Private Sub Load_Color()
        cmbColor.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblGrading_FM WHERE Type = 1 ORDER BY TypeName", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbColor.Items.Add(rsComSql.Fields("TypeName").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Clarity()
        cmbClarity.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblGrading_FM WHERE Type = 2 ORDER BY TypeName", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbClarity.Items.Add(rsComSql.Fields("TypeName").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub CalculateTotals()
        If txtRetTap.Text = "" Then txtRetTap.Text = "0"
        If txtRetCts.Text = "" Then txtRetCts.Text = "0"
        If txtRej.Text = "" Then txtRej.Text = "0"
        If txtRejCts.Text = "" Then txtRejCts.Text = "0"
        If txtLostPcs.Text = "" Then txtLostPcs.Text = "0"
        If txtLostCts.Text = "" Then txtLostCts.Text = "0"
        If txtRepPcs.Text = "" Then txtRepPcs.Text = "0"
        If txtRepCts.Text = "" Then txtRepCts.Text = "0"
        If txtRghPcs.Text = "" Then txtRghPcs.Text = "0"
        If txtRghCts.Text = "" Then txtRghCts.Text = "0"

        lblTotPcs.Text = CInt(txtRetTap.Text) + CInt(txtRej.Text) + CInt(txtLostPcs.Text) + CInt(txtRepPcs.Text) + CInt(txtRghPcs.Text)
        lblTotCts.Text = Format(CDbl(txtRetCts.Text) + CDbl(txtRejCts.Text) + CDbl(txtLostCts.Text) + CDbl(txtRepCts.Text) + CDbl(txtRghCts.Text), "#0.000")
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
        Return ParcelFound
    End Function

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtParNo.Text = UCase(txtParNo.Text)
            If ParcelFound(cmbDept.Text, txtParNo.Text) = True Then
                cmbSection.SelectedIndex = 0
                txtPktNo.Text = ""
                txtPktNo.Focus()
            Else
                MsgBox("Invalid Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtParNo.Text = ""
                txtPktNo.Text = ""
                txtParNo.Focus()
            End If
        End If
    End Sub

    Private Sub Load_Packet()
        If txtParNo.Text <> "" And txtPktNo.Text <> "" Then
            txtParNo.Text = UCase(txtParNo.Text)
            txtPktNo.Text = UCase(txtPktNo.Text)
            If ParcelFound(cmbDept.Text, txtParNo.Text) = True Then
                Load_ParcelDetails()
                cmdEmp.Focus()
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
            Load_Packet()
        End If
    End Sub

    Private Sub Load_ParcelDetails()
        Dim intIssPcsB As Integer
        Dim intIssPcsC As Integer
        Dim intLastSec As Integer

        issued = True
        Checked = False
        intIssPcsB = 0
        intIssPcsC = 0

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
        rsComSql_1.Open("SELECT * FROM tblGrading_SizingPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            txtClarity.Text = rsComSql_1.Fields("ReturnType").Value
        Else
            MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql_1 = Nothing

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblGrading_SizingIssues WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "' ORDER BY ID DESC", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            Checked = True
            intIssPcsC = rsComSql_1.Fields("IssPcs").Value
            cmbSection.SelectedIndex = rsComSql_1.Fields("Sec").Value - 1
            Section = rsComSql_1.Fields("Sec").Value
        Else
            PictureBox2.Visible = True
            txtIssDate.Text = Format(Date.Now, "dd/MM/yyyy")
            txtIssTime.Text = Format(Date.Now, "HH:mm")
            txtIssTap.Enabled = False
            txtIssCts.Enabled = False
            GetNewPacket()
        End If
        rsComSql_1 = Nothing

        If Section = 6 Then
            txtRetTap.ReadOnly = False
            txtRetCts.ReadOnly = False
        Else
            txtRetTap.ReadOnly = True
            txtRetCts.ReadOnly = True
        End If

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblGrading_SizingReturns WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "' ORDER BY ID DESC", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            intLastSec = rsComSql_1.Fields("Sec").Value
            MsgBox("Packet Finished", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
            Exit Sub
        End If
        rsComSql_1 = Nothing

        If issued = True And Checked = True Then
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblGrading_SizingReturns WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "' AND Sec = '" & Section & "'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                If intIssPcsC = rsComSql_1.Fields("RetPcs").Value + rsComSql_1.Fields("RepPcs").Value + rsComSql_1.Fields("LostPcs").Value + rsComSql_1.Fields("RejPcs").Value Then
                    If Section <> 2 Then
                        txtIssTap.Text = rsComSql_1.Fields("RetPcs").Value
                        txtIssCts.Text = rsComSql_1.Fields("RetCts").Value
                        txtIssTap.ReadOnly = False
                        txtIssCts.ReadOnly = False
                        txtIssDate.Text = Format(Date.Now, "dd/MM/yyyy")
                        txtIssTime.Text = Format(Date.Now, "HH:mm")
                        ICNo2 = ""
                        txtEmp.Text = ""
                        PictureBox2.Visible = True
                        PictureBox1.Visible = False
                        cmbSection.SelectedIndex = Section
                        Section = Section + 1
                        cmdEmp.Focus()
                        Checked = False

                    Else
                        MsgBox("Packet Finished", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        ClearFields()
                    End If
                End If
            Else
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblGrading_SizingIssues WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "' AND Sec = '" & Section & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    txtIssTap.Text = rsComSql_1.Fields("IssPcs").Value
                    txtIssCts.Text = rsComSql_1.Fields("IssCts").Value
                    txtIssTap.Enabled = False
                    txtIssCts.Enabled = False
                    txtIssDate.Text = Format(rsComSql_1.Fields("IssDate").Value, "dd/MM/yyyy")
                    txtIssTime.Text = Format(rsComSql_1.Fields("IssTime").Value, "HH:mm")
                    ICNo2 = rsComSql_1.Fields("EmpNo").Value
                    txtEmp.Text = rsComSql_1.Fields("EmpNo").Value

                    PictureBox2.Visible = True
                    PictureBox1.Visible = True
                    cmdEmp.Focus()
                End If
                rsComSql_1 = Nothing
            End If
            rsComSql_1 = Nothing
        End If
    End Sub

    Private Sub ClearFields()

        txtParNo.Text = ""
        txtPktNo.Text = ""
        txtClarity.Text = ""
        txtEmp.Text = ""
        txtRetCts.Text = ""
        txtRetTap.Text = ""
        txtIssCts.Text = ""
        txtIssTap.Text = ""
        txtRej.Text = ""
        txtLostPcs.Text = ""
        txtIssDate.Text = ""
        txtIssTime.Text = ""
        txtRetTime.Text = ""
        txtRetDate.Text = ""
        txtRejCts.Text = ""
        txtLostCts.Text = ""

        cmbColor.Text = ""
        cmbClarity.Text = ""

        txtRepPcs.Text = ""
        txtRepCts.Text = ""

        cmbSection.SelectedIndex = 0

        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"

        PictureBox1.Visible = False
        PictureBox2.Visible = False

        flxType.Rows.Clear()
        cmbAssortType.Text = ""
        cmbSize.Text = ""

        lblTotPcs.Text = "0"
        lblTotCts.Text = "0"

        txtOrigin.Text = ""
        txtOCode.Text = ""
    End Sub

    Private Sub GetNewPacket()

        cmbSection.SelectedIndex = 0
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblGrading_SizingPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            txtIssTap.Text = rsComSql_1.Fields("PktPcs").Value
            txtIssCts.Text = Format(CDbl(rsComSql_1.Fields("PktCts").Value), "#0.000")

            PictureBox2.Visible = True
            PictureBox1.Visible = False

            issued = True
            Checked = False
        Else
            MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
            PictureBox2.Visible = False
            PictureBox1.Visible = False

            issued = False
            Checked = False
            txtParNo.Focus()
        End If
        rsComSql_1 = Nothing

    End Sub

    Private Sub cmdOrig_Click(sender As Object, e As EventArgs) Handles cmdOrig.Click
        Dim strDepartment As String

        strDepartment = cmbDept.Text
        If cmbDept.Text <> "" And txtParNo.Text <> "" And txtPktNo.Text <> "" Then

            If cmbDept.Text = "Mix" Or cmbDept.Text = "GradingPCU" Or cmbDept.Text = "GradingPCU_N" Or _
                cmbDept.Text = "Baguettes" Or cmbDept.Text = "Baguettes2" Or cmbDept.Text = "Princess" Or cmbDept.Text = "Rounds" Or cmbDept.Text = "Emerald" Or cmbDept.Text = "Lamour" Or cmbDept.Text = "Davinci" Then
                If cmbDept.Text = "GradingPCU" Or cmbDept.Text = "GradingPCU_N" Then
                    strDepartment = "Precision"
                End If

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblDep_Trf WHERE DCLParcelNo = '" & txtParNo.Text & "' AND Department = '" & strDepartment & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If cmbAssortType.Text <> "TYCOON" And cmbAssortType.Text <> "CUSHION" Then
                        cmbAssortType.Text = rsComSql.Fields("AssortmentNo").Value
                        cmbSize.Text = "0"
                    End If
                    txtPrice.Text = rsComSql.Fields("ItemCost").Value
                End If
                rsComSql = Nothing

                If cmbAssortType.Text = "" Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblDep_Trf WHERE DCLParcelNo = '" & Mid(txtParNo.Text, 1, 6) & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If cmbAssortType.Text <> "TYCOON" And cmbAssortType.Text <> "CUSHION" Then
                            cmbAssortType.Text = rsComSql.Fields("AssortmentNo").Value
                            cmbSize.Text = "0"
                        End If
                        txtPrice.Text = rsComSql.Fields("ItemCost").Value
                    End If
                    rsComSql = Nothing
                End If

                If cmbAssortType.Text = "" Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblDep_Trf WHERE DCLParcelNo = '" & txtParNo.Text & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If cmbAssortType.Text <> "TYCOON" And cmbAssortType.Text <> "CUSHION" Then
                            cmbAssortType.Text = rsComSql.Fields("AssortmentNo").Value
                            cmbSize.Text = "0"
                        End If
                        txtPrice.Text = rsComSql.Fields("ItemCost").Value
                    End If
                    rsComSql = Nothing
                End If

                If cmbAssortType.Text = "" Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblGradingTrf WHERE ParcelNo = '" & txtParNo.Text & "' AND Department = 'GradingMix'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        cmbAssortType.Text = rsComSql.Fields("Assort1").Value
                        cmbSize.Text = "0"
                        If cmbAssortType.Text <> "" Then
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & cmbAssortType.Text & "'", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount Then
                                txtPrice.Text = rsComSql_1.Fields("MarketPrice").Value
                            End If
                            rsComSql_1 = Nothing
                        End If
                    End If
                    rsComSql = Nothing
                End If

                If UCase(Mid(txtParNo.Text, 1, 3)) = "TRF" Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblPCUStockIn WHERE ParNo = '" & txtParNo.Text & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        cmbAssortType.Text = rsComSql.Fields("Assortment").Value
                        cmbSize.Text = "0"
                        If cmbAssortType.Text <> "" Then
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & cmbAssortType.Text & "'", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount Then
                                txtPrice.Text = rsComSql_1.Fields("MarketPrice").Value
                            End If
                            rsComSql_1 = Nothing
                        End If
                    End If
                    rsComSql = Nothing
                End If

                If cmbDept.Text = "GradingPCU" Or cmbDept.Text = "GradingPCU_N" Then
                    If Mid(txtParNo.Text, 1, 1) = "5" Then
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT * FROM tblPCUStockIn WHERE ParNo = '" & txtParNo.Text & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            cmbAssortType.Text = rsComSql.Fields("OrgAssort").Value
                            cmbSize.Text = "0"
                            If cmbAssortType.Text <> "" Then
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & cmbAssortType.Text & "'", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount Then
                                    txtPrice.Text = rsComSql_1.Fields("ListCost").Value
                                End If
                                rsComSql_1 = Nothing
                            End If
                        End If
                        rsComSql = Nothing
                    End If
                End If
            Else
                If cmbDept.Text = "Direct Import" Then
                    strDepartment = "Grading"
                ElseIf cmbDept.Text = "Rounds Direct" Then
                    strDepartment = "Grading Rounds"
                End If
                rsComSql = New ADODB.Recordset
                If Len(txtParNo.Text) = 6 Then
                    rsComSql.Open("SELECT * FROM tblDep_Trf WHERE DCLParcelNo LIKE '" & Mid(txtParNo.Text, 1, 6) & "' + '%' AND Department = '" & strDepartment & "'", AdoCN, 1, 1)
                Else
                    rsComSql.Open("SELECT * FROM tblDep_Trf WHERE DCLParcelNo LIKE '" & txtParNo.Text & "' AND Department = '" & strDepartment & "'", AdoCN, 1, 1)
                End If
                If rsComSql.RecordCount Then
                    cmbAssortType.Text = rsComSql.Fields("AssortmentNo").Value
                    cmbSize.Text = "0"
                    txtPrice.Text = rsComSql.Fields("ItemCost").Value
                End If
                rsComSql = Nothing
            End If
        End If
    End Sub

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Dim intPos As Integer

        Instring = UCase(InputBox("Enter Par/Pkt Number"))
        If Len(Instring) > 0 Then
            txtParPkt.Text = Trim(Instring)
            intPos = InStr(1, txtParPkt.Text, "/")
            If intPos > 0 Then
                txtParNo.Text = Mid(txtParPkt.Text, 1, intPos - 1)
                txtPktNo.Text = Mid(txtParPkt.Text, intPos + 1, Len(txtParPkt.Text) - intPos)
                txtPktNo.Focus()

                Load_Packet()
            End If
        Else
            txtParPkt.Text = ""
        End If
    End Sub

    Private Sub cmdEmp_Click(sender As Object, e As EventArgs) Handles cmdEmp.Click
        Datavalid = False
        Parcel = False
        Instring = UCase(InputBox("Enter Emp No"))
        ParcelLen = Len(Instring)
        If ParcelLen = 6 Then
            Datavalid = True

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL3 WHERE (Pay = 1) AND (FullEmpNo = '" & Trim(Instring) & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                cmdEmp.Focus()
                Exit Sub
            End If
            rsComSql = Nothing
            ICNo = UCase(Trim(Instring))
            txtEmp.Text = ICNo
        Else
            MsgBox("Invalid IC No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Datavalid = False
            ICNo = ""
            cmdEmp.Focus()
            Exit Sub
        End If

        If issued = True And Checked = True Then
            If Trim(ICNo2) <> Trim(ICNo) Then
                If MsgBox("IC Numbers do not match. Proceed Anyway?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes Then
                    txtEmp.Text = ICNo
                    txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
                    txtRetTime.Text = Format(Date.Now, "HH:mm")
                Else
                    txtEmp.Text = ""
                    Datavalid = False
                    ICNo = ""
                End If
            Else
                txtEmp.Text = ICNo
                txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
                txtRetTime.Text = Format(Date.Now, "HH:mm")
                If Section < 5 Then
                    cmbAssortType.Focus()
                Else
                    txtRetTap.Focus()
                End If
            End If
        ElseIf issued = True And Checked = False Then
            txtEmp.Text = ICNo
            txtRetDate.Text = Format(Date.Now, "dd/MM/yyyy")
            txtRetTime.Text = Format(Date.Now, "HH:mm")
        Else
            txtEmp.Text = ICNo
            txtIssDate.Text = Format(Date.Now, "dd/MM/yyyy")
            txtIssTime.Text = Format(Date.Now, "HH:mm")
            txtIssTap.Focus()
        End If
    End Sub

    Private Sub txtRetTap_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRetTap.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtRetCts.Focus()
        End If
    End Sub

    Private Sub txtRetCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRetCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRetCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtRej.Focus()
            txtRej.Text = "0"
            txtRejCts.Text = "0"
            txtLostPcs.Text = "0"
            txtLostCts.Text = "0"
            txtRepPcs.Text = "0"
            txtRepCts.Text = "0"
            txtRghPcs.Text = "0"
            txtRghCts.Text = "0"
        End If
    End Sub

    Private Sub txtRej_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRej.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtRetCts.Focus()
            CalculateTotals()
        End If
    End Sub

    Private Sub txtRejCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRejCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRetCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtLostPcs.Focus()
            CalculateTotals()
        End If
    End Sub

    Private Sub txtLostPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLostPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtLostCts.Focus()
            CalculateTotals()
        End If
    End Sub

    Private Sub txtLostCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLostCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtLostCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtRepPcs.Focus()
            CalculateTotals()
        End If
    End Sub

    Private Sub txtRepPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRepPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtRepCts.Focus()
            CalculateTotals()
        End If
    End Sub

    Private Sub txtRepCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRepCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRepCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtRghPcs.Focus()
            CalculateTotals()
        End If
    End Sub

    Private Sub txtRghPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRghPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtRghCts.Focus()
            CalculateTotals()
        End If
    End Sub

    Private Sub txtRghCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRghCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRghCts.Text)
        If Asc(e.KeyChar) = 13 Then
            CalculateTotals()
        End If
    End Sub

    Private Sub cmbAssortType_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbAssortType.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbAssortType.Text = UCase(cmbAssortType.Text)
            cmbSize.Text = "0"
            cmbSize.Focus()
        End If
    End Sub

    Private Sub txtTypePcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTypePcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtTypeCts.Focus()
        End If
    End Sub

    Private Sub txtTypeCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTypeCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtTypeCts.Text)
        If Asc(e.KeyChar) = 13 Then
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim intRow As Integer
        Dim intTotPcs As Integer
        Dim dblTotCts As Double
        Dim strAssortment As String

        If txtRetDate.Text = "" Then
            MsgBox("Please Enter the Employee No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If cmbAssortType.Text = "" Then
            MsgBox("Please select the Assortment", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If cmbSize.Text = "" Then
            MsgBox("Please select the Size Range", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        strAssortment = Trim(cmbAssortType.Text)
        If strAssortment = "" Then
            MsgBox("Please check the input entries", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If strRight(strAssortment, 2) = "FM" Then
            If cmbColor.Text = "" Then
                MsgBox("Please select the Color", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If cmbClarity.Text = "" Then
                MsgBox("Please select the Clarity", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        End If

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblGrading_SizingList WHERE NAME = '" & Trim(strAssortment) & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Mid(Trim(strAssortment), 1, 1) <> "U" And Mid(Trim(strAssortment), 1, 1) <> "A" And Mid(Trim(strAssortment), 1, 1) <> "V" And Mid(Trim(strAssortment), 1, 1) <> "T" Then
                If rsComSql.Fields("OK").Value = 0 Then
                    MsgBox("Assortment is Blocked", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
            If rsComSql.Fields("Origin").Value <> "" Then
                If txtOCode.Text <> Mid(strAssortment, 1, 3) Then
                    MsgBox("Invalid Assortment Origin", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
        Else
            PBResponse = MsgBox("Invalid Assortment. Do you want to add?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                If txtPrice.Text <> "" Then
                    AdoCN.Execute("INSERT INTO tblGrading_SizingList(NAME,OLDNAME,PRICE,COLOR,CLARITY,CUT,SHAPE,MODEL,LFROM,LTO,WFROM,WTO,TYPE,OK) " & _
                                  "VALUES('" & strAssortment & "',''," & CDbl(txtPrice.Text) & ",'','','','','','','','','',1,1)")
                Else
                    cmbAssortType.Text = ""
                    txtPrice.Text = ""
                    Exit Sub
                End If
            Else
                cmbAssortType.Text = ""
                txtPrice.Text = ""
                Exit Sub
            End If
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblGrading_SizeListRange WHERE Size = '" & Trim(cmbSize.Text) & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Size Range", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        If txtTypePcs.Text <> "" And txtTypeCts.Text <> "" Then
            If CInt(txtTypePcs.Text) > 0 Then
                'If cmbDept.Text <> "Direct Import" Then
                '    For intRow = 0 To flxType.Rows.Count - 1
                '        If strAssortment = flxType.Item(0, intRow).Value And cmbSize.Text = flxType.Item(5, intRow).Value Then
                '            MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                '            Exit Sub
                '        End If
                '    Next
                'End If

                'If cmbDept.Text = "Lamour" Then
                '    If CInt(txtTypePcs.Text) <> 1 Then
                '        MsgBox("Pcs cannot exceed 1(one)", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                '        Exit Sub
                '    End If
                'End If

                intTotPcs = 0
                dblTotCts = 0
                For intRow = 0 To flxType.Rows.Count - 1
                    intTotPcs = intTotPcs + CInt(flxType.Item(1, intRow).Value)
                    dblTotCts = dblTotCts + CDbl(flxType.Item(2, intRow).Value)
                Next

                If txtRetTap.Text = "" Then txtRetTap.Text = "0"
                If txtRetCts.Text = "" Then txtRetCts.Text = "0"
                If txtRej.Text = "" Then txtRej.Text = "0"
                If txtRejCts.Text = "" Then txtRejCts.Text = "0"
                If txtLostPcs.Text = "" Then txtLostPcs.Text = "0"
                If txtLostCts.Text = "" Then txtLostCts.Text = "0"
                If txtRepPcs.Text = "" Then txtRepPcs.Text = "0"
                If txtRepCts.Text = "" Then txtRepCts.Text = "0"
                If txtRghPcs.Text = "" Then txtRghPcs.Text = "0"
                If txtRghCts.Text = "" Then txtRghCts.Text = "0"

                If intTotPcs + CInt(txtTypePcs.Text) + CInt(txtRej.Text) + CInt(txtLostPcs.Text) + CInt(txtRepPcs.Text) + CInt(txtRghPcs.Text) > CInt(txtIssTap.Text) Then
                    MsgBox("Pcs Invalid", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                flxType.Rows.Add(UCase(strAssortment),
                                 txtTypePcs.Text,
                                 Format(CDbl(txtTypeCts.Text), "#0.000"),
                                 cmbColor.Text,
                                 cmbClarity.Text,
                                 cmbSize.Text, 1)

                txtTotPcs.Text = CInt(txtTotPcs.Text) + CInt(txtTypePcs.Text)
                txtTotCts.Text = Format(CDbl(txtTotCts.Text) + CDbl(txtTypeCts.Text), "#0.000")

                txtRetTap.Text = txtTotPcs.Text
                txtRetCts.Text = txtTotCts.Text

                CalculateTotals()

                txtTypePcs.Text = ""
                txtTypeCts.Text = ""

                cmbColor.Text = ""
                cmbClarity.Text = ""
            Else
                MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            MsgBox("Please check the input entries", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If

        cmbAssortType.Focus()
    End Sub

    Private Sub RecSave()
        Dim dataok As Boolean
        Dim stiss, stret As Integer
        Dim intRow As Integer
        Dim intTotPcs As Integer
        Dim dblTotCts As Double

        dataok = True
        If cmbDept.Text = "" Then
            MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If txtEmp.Text = "" Then
            MsgBox("Invalid Emp. No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If

        If Len(Trim(txtEmp.Text)) <> 6 Then
            MsgBox("Invalid Emp. No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If

        If txtParNo.Text = "" Then
            MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        If txtPktNo.Text = "" Then
            MsgBox("Invalid Packet No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            dataok = False
        End If
        If dataok = False Then Exit Sub

        dataok = True
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblGrading_SizingPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount = 0 Then
            MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
            dataok = False
        End If
        rsComSql_1 = Nothing
        If dataok = False Then Exit Sub

        If issued = True And Checked = False Then
            If txtIssTap.Text = "" Then
                MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub

            If txtIssCts.Text = "" Then
                MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub

            If CInt(txtIssTap.Text) <= 0 Then
                MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub

            If CSng(txtIssCts.Text) <= 0 Then
                MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                dataok = False
            End If
            If dataok = False Then Exit Sub

            If Len(txtIssDate.Text) < 2 Then Exit Sub


        ElseIf issued = True And Checked = True Then
            If txtRetTap.Text = "" Then txtRetTap.Text = "0"
            If txtRetCts.Text = "" Then txtRetCts.Text = "0"
            If txtRej.Text = "" Then txtRej.Text = "0"
            If txtRejCts.Text = "" Then txtRejCts.Text = "0"
            If txtLostPcs.Text = "" Then txtLostPcs.Text = "0"
            If txtLostCts.Text = "" Then txtLostCts.Text = "0"
            If txtRepPcs.Text = "" Then txtRepPcs.Text = "0"
            If txtRepCts.Text = "" Then txtRepCts.Text = "0"
            If txtRghPcs.Text = "" Then txtRghPcs.Text = "0"
            If txtRghCts.Text = "" Then txtRghCts.Text = "0"

            stiss = CInt(txtIssTap.Text)
            stret = CInt(txtRetTap.Text) + CInt(txtRej.Text) + CInt(txtLostPcs.Text) + CInt(txtRepPcs.Text) + CInt(txtRghPcs.Text)
            If stiss <> stret Then
                strMsg = "Stones Issued " & stiss & "   Stones Returned " & stret
                MsgBox(strMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
                dataok = False
                If dataok = False Then Exit Sub
            End If

            If Section < 6 Then
                intTotPcs = 0
                dblTotCts = 0
                For intRow = 0 To flxType.Rows.Count - 1
                    intTotPcs = intTotPcs + CInt(flxType.Item(1, intRow).Value)
                    dblTotCts = dblTotCts + CDbl(flxType.Item(2, intRow).Value)
                Next

                If CInt(txtIssTap.Text) <> intTotPcs + CInt(txtRepPcs.Text) + CInt(txtRej.Text) + CInt(txtLostPcs.Text) + CInt(txtRghPcs.Text) Then
                    MsgBox("Pcs not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
                    dataok = False
                    If dataok = False Then Exit Sub
                End If

                If intGradingCheckCts = 1 Then
                    If txtParNo.Text <> "SB5008B" Or txtParNo.Text <> "DC5011B" Then
                        If Math.Round(CDbl(txtIssCts.Text), 3) - Math.Round(Math.Round(dblTotCts, 3) + Math.Round(Val(txtRepCts.Text), 3) + Math.Round(Val(txtRejCts.Text), 3) + Math.Round(Val(txtLostCts.Text), 3) + Math.Round(Val(txtRghCts.Text), 3), 3) > 0.02 _
                        Or Math.Round(CDbl(txtIssCts.Text), 3) - Math.Round(Math.Round(dblTotCts, 3) + Math.Round(Val(txtRepCts.Text), 3) + Math.Round(Val(txtRejCts.Text), 3) + Math.Round(Val(txtLostCts.Text), 3) + Math.Round(Val(txtRghCts.Text), 3), 3) < -0.02 Then
                            MsgBox("Cts not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
                            dataok = False
                            If dataok = False Then Exit Sub
                        End If
                    End If
                End If

                For intRow = 0 To flxType.Rows.Count - 1
                    If (CInt(flxType.Item(1, intRow).Value) > 0 And CDbl(flxType.Item(2, intRow).Value) <= 0) Or (CInt(flxType.Item(1, intRow).Value) <= 0 And CDbl(flxType.Item(2, intRow).Value) > 0) Then
                        MsgBox("Invalid Data in " & flxType.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
                        dataok = False
                        If dataok = False Then Exit Sub
                    End If
                Next

            End If

        Else
            If Len(txtIssDate.Text) < 2 Then dataok = False
        End If

        If dataok = True Then
            DataSave()
        End If

    End Sub

    Private Sub DataSave()
        Dim strDCLParcelNo As String
        Dim strSupParcelNo As String
        Dim strAssortmentNo As String

        If issued = True And Checked = False Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_SizingIssues WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblGrading_SizingIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                              "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "'," & cmbSection.SelectedIndex + 1 & ",'" & Mid(Trim(txtEmp.Text), 1, 6) & "','" & CInt(txtIssTap.Text) & "'," & _
                                     "'" & CDbl(txtIssCts.Text) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")
            Else
                MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            rsComSql = Nothing

        ElseIf issued = True And Checked = True Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_SizingReturns WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Sec = " & cmbSection.SelectedIndex + 1 & "", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblGrading_SizingReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts," & _
                                    "LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts,RghPcs,RghCts) " & _
                              "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "'," & cmbSection.SelectedIndex + 1 & ",'" & Mid(Trim(txtEmp.Text), 1, 6) & "'" & _
                                    "," & CInt(txtRetTap.Text) & "," & CDbl(txtRetCts.Text) & "," & CInt(txtLostPcs.Text) & "," & CDbl(txtLostCts.Text) & "," & _
                                    "" & CInt(txtRepPcs.Text) & "," & CDbl(txtRepCts.Text) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "'," & CInt(txtRej.Text) & "," & CDbl(txtRejCts.Text) & "," & CInt(txtRghPcs.Text) & "," & CDbl(txtRghCts.Text) & ")")

                If CInt(txtRghPcs.Text) > 0 And (cmbDept.Text = "Rounds Direct" Or cmbDept.Text = "GradingPCU_N") Then
                    strSupParcelNo = ""
                    strDCLParcelNo = ""
                    strAssortmentNo = ""
                    rsComSql_1 = New ADODB.Recordset
                    If cmbDept.Text = "Rounds Direct" Then
                        rsComSql_1.Open("SELECT SupParcelNo, DclParcelNo, AssortmentNo FROM tblDep_Trf WHERE DCLParcelNo = '" & txtParNo.Text & "' AND Department = 'Grading Rounds'", AdoCN, 1, 1)
                    ElseIf cmbDept.Text = "GradingPCU_N" Then
                        rsComSql_1.Open("SELECT SupParcelNo, DclParcelNo, AssortmentNo FROM tblDep_Trf WHERE DCLParcelNo = '" & txtParNo.Text & "' AND Department = 'Precision'", AdoCN, 1, 1)
                    End If
                    If rsComSql_1.RecordCount Then
                        strSupParcelNo = rsComSql_1.Fields("SupParcelNo").Value
                        strDCLParcelNo = rsComSql_1.Fields("DclParcelNo").Value
                        strAssortmentNo = rsComSql_1.Fields("AssortmentNo").Value
                    End If
                    rsComSql_1 = Nothing

                    If cmbDept.Text = "Rounds Direct" Then
                        AdoCN.Execute("INSERT INTO tblDep_Rec(Department,AssortmentNo,SupParcelNo,DCLParcelNo,Pcs,Cts,RghCts) " & _
                                      "VALUES('Grading Rounds','" & strAssortmentNo & "','" & strSupParcelNo & "'," & _
                                        "'" & strDCLParcelNo & "'," & CDbl(txtRghPcs.Text) & "," & CDbl(txtRghCts.Text) & "," & CDbl(txtRghCts.Text) & ")")
                    ElseIf cmbDept.Text = "GradingPCU_N" Then
                        AdoCN.Execute("INSERT INTO tblDep_Rec(Department,AssortmentNo,SupParcelNo,DCLParcelNo,Pcs,Cts,RghCts) " & _
                                      "VALUES('Precision','" & strAssortmentNo & "','" & strSupParcelNo & "'," & _
                                        "'" & strDCLParcelNo & "'," & CDbl(txtRghPcs.Text) & "," & CDbl(txtRghCts.Text) & "," & CDbl(txtRghCts.Text) & ")")
                    End If
                End If

                Save_GradingTypes(txtParNo.Text, txtPktNo.Text, cmbSection.SelectedIndex + 1)

                If CInt(txtRepPcs.Text) > 0 Then
                    Save_RepairDetails(txtParNo.Text, txtPktNo.Text, cmbSection.SelectedIndex + 1, txtClarity.Text)
                End If
            Else
                MsgBox("Already entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            rsComSql = Nothing
        End If
        ClearFields()

    End Sub

    Private Sub Save_GradingTypes(ByVal strParcelNo As String, ByVal strPktNo As String, ByVal intSection As Integer)
        Dim intRow As Integer

        AdoCN.Execute("DELETE FROM tblGrading_SizingTypes WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & strParcelNo & "' AND PktNo = '" & strPktNo & "' AND Sec = " & intSection & "")
        For intRow = 0 To flxType.Rows.Count - 1
            If CDbl(flxType.Item(1, intRow).Value) > 0 Then
                AdoCN.Execute("INSERT INTO tblGrading_SizingTypes(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts,OK,Color,Clarity,SizeRange) " & _
                              "VALUES('" & cmbDept.Text & "','" & strParcelNo & "','" & strPktNo & "'," & intSection & "," & _
                                "'" & flxType.Item(0, intRow).Value & "'," & CInt(flxType.Item(1, intRow).Value) & "," & CDbl(flxType.Item(2, intRow).Value) & ",0," & _
                                "'" & flxType.Item(3, intRow).Value & "','" & flxType.Item(4, intRow).Value & "','" & flxType.Item(5, intRow).Value & "')")
            End If
        Next

    End Sub

    Private Sub Save_RepairDetails(ByVal strParcelNo As String, ByVal strPktNo As String, ByVal intSection As Integer, ByVal strOrgPkt As String)
        Dim strRepNo As String

        If cmbDept.Text = "Baguettes" Or cmbDept.Text = "Princess" Or cmbDept.Text = "Emerald" Or cmbDept.Text = "Carrer" Or cmbDept.Text = "Asscher" Then
            strRepNo = GetNewRepairPkt(strParcelNo)
            AdoCN.Execute("INSERT INTO tblGrading_RepairParcels(Department,ParNo,PktNo,Pcs,Cts,IssDate,IssTime,OK,PktNo2,RepReason,Grp) " & _
                          "VALUES('" & cmbDept.Text & "','" & strParcelNo & "','" & strRepNo & "'," & CInt(txtRepPcs.Text) & "," & CDbl(txtRepCts.Text) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,'" & strOrgPkt & "','','')")

        End If

    End Sub

    Private Function GetNewRepairPkt(ByVal strParcelNo As String) As String

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT MAX(CONVERT(NUMERIC,RIGHT(PktNo,4))) AS MaxRepNo FROM tblGrading_RepairParcels WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & strParcelNo & "' AND LEFT(PktNo, 1) = 'U'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            If Not IsDBNull(rsComSql_1.Fields("MaxRepNo").Value) Then
                GetNewRepairPkt = "U" & Format(rsComSql_1.Fields("MaxRepNo").Value + 1, "0000")
            Else
                GetNewRepairPkt = "U" & "0001"
            End If
        Else
            GetNewRepairPkt = "U" & "0001"
        End If
        rsComSql_1 = Nothing

    End Function

    Private Sub flxType_DoubleClick(sender As Object, e As EventArgs) Handles flxType.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            txtTotPcs.Text = CDbl(txtTotPcs.Text) - CDbl(flxType.Item(1, flxType.CurrentRow.Index).Value)
            txtTotCts.Text = Format(CDbl(txtTotCts.Text) - CDbl(flxType.Item(2, flxType.CurrentRow.Index).Value), "#0.000")
            txtRetTap.Text = txtTotPcs.Text
            txtRetCts.Text = txtTotCts.Text
            flxType.Rows.RemoveAt(flxType.CurrentRow.Index)
            CalculateTotals()
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        RecSave()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmbSize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSize.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbSize.Text = UCase(cmbSize.Text)
            txtTypePcs.Focus()
        End If
    End Sub
End Class