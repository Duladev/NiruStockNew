
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_BagPacket

    Dim strDepartment As String
    Dim strParNo As String
    Dim strPktNo As String

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub frm_BagPacket_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        strDepartment = "Baguettes"

        Load_Flow()
        Load_Col()
        Load_Cut()
        Load_Clarity()
        Load_Size()
        Load_IncentiveCat()
    End Sub

    Private Sub Load_IncentiveCat()
        Dim rstIncCat As ADODB.Recordset

        cmbUnit.Items.Clear()
        rstIncCat = New ADODB.Recordset
        rstIncCat.Open("SELECT DISTINCT Unit FROM tblBAGIncentiveCat ORDER BY Unit", AdoCN, 1, 1)
        If rstIncCat.RecordCount Then
            rstIncCat.MoveFirst()
            Do While Not rstIncCat.EOF
                cmbUnit.Items.Add(rstIncCat.Fields("Unit").Value)
                rstIncCat.MoveNext()
            Loop
        End If
        rstIncCat = Nothing
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        Dim rstPacket As ADODB.Recordset
        Dim strGrp As String

        If Asc(e.KeyChar) = 13 Then
            If txtParNo.Text <> "" Then
                If Len(txtParNo.Text) = 7 Then
                    txtParNo.Text = UCase(txtParNo.Text)
                    strGrp = strRight(txtParNo.Text, 1)

                    rstPacket = New ADODB.Recordset
                    rstPacket.Open("SELECT * FROM tblParcel WHERE GrpParNo = '" & txtParNo.Text & "' AND Depart = '" & strDepartment & "'", AdoCN, 1, 1)
                    If rstPacket.RecordCount > 0 Then
                        cmbFlow.Text = rstPacket.Fields("Flow").Value
                        txtAvailPcs.Text = rstPacket.Fields("IssuedPcs").Value
                        txtAssort.Text = rstPacket.Fields("Assortment").Value
                        txtSupParNo.Text = rstPacket.Fields("OrigParcelNo").Value

                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT SUM(PktPcs) AS Pcs, SUM(PktCts) AS Cts FROM tblBAGPacket WHERE ParNo = '" & txtParNo.Text & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                                txtPktPcs.Text = rsComSql.Fields("Pcs").Value
                                txtPktCts.Text = Math.Round(rsComSql.Fields("Cts").Value, 3)
                            Else
                                txtPktPcs.Text = "0"
                                txtPktCts.Text = "0"
                            End If
                        Else
                            txtPktPcs.Text = "0"
                            txtPktCts.Text = "0"
                        End If
                        rsComSql = Nothing

                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT MAX(PktNo) AS MaxPktNo FROM tblBAGPacket WHERE ParNo = '" & txtParNo.Text & "' AND ISNUMERIC(PktNo)  > 0", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            If Not IsDBNull(rsComSql.Fields("MaxPktNo").Value) Then
                                txtPktNo.Text = Format(CDbl(rsComSql.Fields("MaxPktNo").Value) + 1, "0000")
                            Else
                                txtPktNo.Text = "0001"
                            End If
                        Else
                            txtPktNo.Text = "0001"
                        End If
                        rsComSql = Nothing

                        flxDetails.Rows.Clear()
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT * FROM tblBAGPacket WHERE ParNo = '" & txtParNo.Text & "' ORDER BY PktNo", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            rsComSql.MoveFirst()
                            While Not rsComSql.EOF
                                flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                                    rsComSql.Fields("PktNo").Value,
                                                    rsComSql.Fields("PktColor").Value,
                                                    rsComSql.Fields("Clarity").Value,
                                                    rsComSql.Fields("PktPcs").Value,
                                                    Format(rsComSql.Fields("PktCts").Value, "#0.000"),
                                                    rsComSql.Fields("PlanVal").Value,
                                                    rsComSql.Fields("SizeRange").Value,
                                                    rsComSql.Fields("FinCts").Value,
                                                    rsComSql.Fields("PktSize").Value,
                                                    Format(rsComSql.Fields("EstYld").Value, "#0.00"),
                                                    rsComSql.Fields("Width").Value,
                                                    Format(rsComSql.Fields("PktIss").Value, "yyyy/MM/dd"),
                                                    rsComSql.Fields("IncUnit").Value,
                                                    rsComSql.Fields("PktID").Value,
                                                    rsComSql.Fields("PktIDNew").Value,
                                                    rsComSql.Fields("PktOrdNo").Value,
                                                    rsComSql.Fields("PktRefNo").Value,
                                                    rsComSql.Fields("StoneNo").Value,
                                                    rsComSql.Fields("PktOrgCts").Value)

                                rsComSql.MoveNext()
                            End While
                        End If
                        rsComSql = Nothing

                        txtPktNo.Focus()
                    Else
                        MsgBox("Parcel not approved yet or Invalid parcel no!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, Me.Text)
                        ClearFields()
                        txtParNo.Focus()
                    End If
                    rstPacket = Nothing
                End If
            Else
                MsgBox("Pls re-enter Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
                txtParNo.Focus()
            End If
        End If
    End Sub

    Private Sub Load_Flow()
        Dim rstflw As ADODB.Recordset

        cmbFlow.Items.Clear()
        rstflw = New ADODB.Recordset
        rstflw.Open("SELECT * FROM tblBAGFlow ORDER BY Flow", AdoCN, 1, 1)
        If rstflw.RecordCount Then
            rstflw.MoveFirst()
            Do While Not rstflw.EOF
                cmbFlow.Items.Add(rstflw.Fields("Flow").Value)
                rstflw.MoveNext()
            Loop
        End If
        rstflw = Nothing
    End Sub

    Private Sub Load_Col()
        Dim rstcol As ADODB.Recordset

        cmbColor.Items.Clear()
        rstcol = New ADODB.Recordset
        rstcol.Open("SELECT Color FROM tblColor ORDER BY Color", AdoCN, 1, 1)
        If rstcol.RecordCount Then
            rstcol.MoveFirst()
            Do While Not rstcol.EOF
                cmbColor.Items.Add(rstcol.Fields("Color").Value)
                rstcol.MoveNext()
            Loop
        End If
        rstcol = Nothing
    End Sub

    Private Sub Load_Cut()
        Dim rstCut As ADODB.Recordset

        rstCut = New ADODB.Recordset
        rstCut.Open("SELECT * FROM tblCut ORDER BY Cut", AdoCN, 1, 1)
        If rstCut.RecordCount Then
            rstCut.MoveFirst()
            Do While Not rstCut.EOF
                cmbCut.Items.Add(rstCut.Fields("Cut").Value)
                rstCut.MoveNext()
            Loop
        End If
        rstCut = Nothing
    End Sub

    Private Sub Load_Clarity()
        Dim rstClarity As ADODB.Recordset

        rstClarity = New ADODB.Recordset
        rstClarity.Open("SELECT * FROM tblRndClarity ORDER BY Clarity", AdoCN, 1, 1)
        If rstClarity.RecordCount Then
            rstClarity.MoveFirst()
            While Not rstClarity.EOF
                cmbClarity.Items.Add(rstClarity.Fields("Clarity").Value)
                rstClarity.MoveNext()
            End While
        End If
        rstClarity = Nothing
    End Sub

    Private Sub Load_Size()
        Dim rstCut As ADODB.Recordset

        cmbSize.Items.Clear()
        rstCut = New ADODB.Recordset
        rstCut.Open("SELECT DISTINCT SizeRange FROM tblBAGPrice ORDER BY SizeRange", AdoCN, 1, 1)
        If rstCut.RecordCount Then
            rstCut.MoveFirst()
            Do While Not rstCut.EOF
                cmbSize.Items.Add(rstCut.Fields("SizeRange").Value)
                rstCut.MoveNext()
            Loop
        End If
        rstCut = Nothing
    End Sub

    Private Sub txtPktNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtParNo.Text = "" Then Exit Sub
            If Len(txtParNo.Text) <> 7 Then Exit Sub
            If txtPktNo.Text = "" Then Exit Sub
            If Len(txtPktNo.Text) <> 4 Then Exit Sub

            txtPktNo.Text = UCase(txtPktNo.Text)

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblBAGPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                txtPcs.Text = rsComSql.Fields("PktPcs").Value
                txtCts.Text = rsComSql.Fields("PktCts").Value
                txtOrgCts.Text = rsComSql.Fields("PktOrgCts").Value

                cmbColor.Text = Trim(rsComSql.Fields("PktColor").Value)
                cmbClarity.Text = Trim(rsComSql.Fields("Clarity").Value)
                cmbCut.Text = Trim(rsComSql.Fields("PktCut").Value)
                txtPlanVal.Text = Trim(rsComSql.Fields("PlanVal").Value)
                txtFinCts.Text = Trim(rsComSql.Fields("FinCts").Value)
                txtEstYld.Text = Trim(rsComSql.Fields("EstYld").Value)
                txtPktID.Text = Trim(rsComSql.Fields("PktID").Value)
                cmbPktIDNew.Text = Trim(rsComSql.Fields("PktIDNew").Value)
                txtSize.Text = Trim(rsComSql.Fields("PktSize").Value)
                cmbSize.Text = Trim(rsComSql.Fields("SizeRange").Value)
                cmbWidth.Text = Trim(rsComSql.Fields("Width").Value)
                cmbUnit.Text = Trim(rsComSql.Fields("IncUnit").Value)
                txtOrderNo.Text = Trim(rsComSql.Fields("PktOrdNo").Value)
                cmbReference.Text = Trim(rsComSql.Fields("PktRefNo").Value)
                txtStoneNo.Text = Trim(rsComSql.Fields("StoneNo").Value)
            Else
                MsgBox("New Packet No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearPacket()
                txtPcs.Focus()
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub ClearFields()
        txtPktNo.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtOrgCts.Text = ""
        txtAssort.Text = ""
        cmbColor.Text = ""
        cmbFlow.Text = ""
        cmbClarity.Text = ""
        cmbCut.Text = ""
        cmbSize.Text = ""
        cmbWidth.Text = ""
        txtEstYld.Text = "0"
        txtFinCts.Text = "0"
        txtPktID.Text = "0"
        cmbPktIDNew.Items.Clear()
        cmbPktIDNew.Text = "0"
        txtSize.Text = "0"
        txtAvailPcs.Text = "0"
        txtPktPcs.Text = "0"
        txtPktCts.Text = "0"
        txtPlanVal.Text = "0"
        cmbWidth.Text = "0"
        cmbUnit.Text = ""
        txtOrderNo.Text = "0"
        cmbReference.Text = "0"
        txtStoneNo.Text = ""
        flxDetails.Rows.Clear()

        txtStoneID.Text = ""
        flxStone.Rows.Clear()

        txtTotPcs.Text = ""
        txtTotValue.Text = ""
        txtTotRghCts.Text = ""
        txtTotFinCts.Text = ""
    End Sub

    Private Sub ClearPacket()
        txtPcs.Text = ""
        txtCts.Text = ""
        txtOrgCts.Text = ""
        cmbColor.Text = ""
        cmbClarity.Text = ""
        cmbCut.Text = ""
        cmbSize.Text = ""
        cmbWidth.Text = ""
        txtEstYld.Text = "0"
        txtFinCts.Text = "0"
        txtPktID.Text = "0"
        cmbPktIDNew.Items.Clear()
        cmbPktIDNew.Text = "0"
        txtSize.Text = "0"
        txtPlanVal.Text = "0"
        cmbWidth.Text = "0"
        cmbUnit.Text = ""
        txtOrderNo.Text = "0"
        cmbReference.Text = "0"
        txtStoneNo.Text = ""

        txtStoneID.Text = ""
        flxStone.Rows.Clear()

        txtTotPcs.Text = ""
        txtTotValue.Text = ""
        txtTotRghCts.Text = ""
        txtTotFinCts.Text = ""
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub Save()
        Dim rstPacket As ADODB.Recordset
        Dim dblTrfPcs As Double
        Dim dblTrfCts As Double

        Dim dblIssPcs As Double
        Dim dblIssCts As Double

        Dim dblPktPcs As Double
        Dim dblPktCts As Double

        Dim intReIssue As Integer
        Dim dblPlannedPcs As Double

        'Dim strColorSys As String
        'Dim strClaritySys As String

        If txtParNo.Text <> "" And txtPktNo.Text <> "" And txtPcs.Text <> "" And txtCts.Text <> "" And txtOrgCts.Text <> "" And txtPktID.Text <> "" Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT GrpParNo FROM tblParcel WHERE GrpParNo = '" & Trim(txtParNo.Text) & "' AND Depart = 'Baguettes' AND Complete = 0", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Parcel Completed", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                txtParNo.Focus()
                rsComSql = Nothing
                Exit Sub
            End If
            rsComSql = Nothing

            If Val(txtPktID.Text) <> 0 Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT ID FROM tblRprPacket WHERE ID = " & Val(txtPktID.Text) & "", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    MsgBox("Invalid Packet ID", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                    txtPktID.Focus()
                    rsComSql = Nothing
                    Exit Sub
                End If
                rsComSql = Nothing
            End If

            If Val(cmbPktIDNew.Text) <> 0 Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT ID FROM tblRPrReturnDetails WHERE ID = " & Val(cmbPktIDNew.Text) & "", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    MsgBox("Invalid New Packet ID", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                    cmbPktIDNew.Focus()
                    rsComSql = Nothing
                    Exit Sub
                End If
                rsComSql = Nothing
            End If

            If Len(txtPktNo.Text) <> 4 Then
                MsgBox("Invalid Packet No.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If cmbFlow.Text = "" Then
                MsgBox("Invalid Flow", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If txtFinCts.Text = "" Then
                MsgBox("Invalid Finish Cts", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(txtCts.Text) < CDbl(txtFinCts.Text) Then
                MsgBox("Invalid Finish Cts", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If txtPlanVal.Text = "" Then
                MsgBox("Invalid Planning Value", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If cmbUnit.Text = "" Then
                MsgBox("Invalid Incentive Category", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If txtEstYld.Text = "" Then
                MsgBox("Invalid Est Yield", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If txtOrderNo.Text = "" Then
                MsgBox("Invalid Order No", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If cmbReference.Text = "" Then
                MsgBox("Invalid Reference", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(txtEstYld.Text) > 100 Then
                MsgBox("Invalid Est Yield", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(txtPcs.Text) > 1 Then
                If cmbSize.Text = "" Then
                    MsgBox("Invalid Size Range", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            If CDbl(txtPcs.Text) > 1 Then
                If cmbWidth.Text = "" Then
                    MsgBox("Invalid Width Range", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            'If strRight(txtParNo.Text, 1) <> "C" And strRight(txtParNo.Text, 1) <> "G" And strRight(txtParNo.Text, 1) <> "T" And _
            '    strRight(txtParNo.Text, 1) <> "P" And strRight(txtParNo.Text, 1) <> "F" Then

            '    'Moving/Non Moving Check
            '    strColorSys = ""
            '    rsComSql = New ADODB.Recordset
            '    rsComSql.Open("SELECT * FROM tblBAGColorClarity WHERE CodeSys = '" & cmbColor.Text & "' AND Type = 1", AdoCN, 1, 1)
            '    If rsComSql.RecordCount Then
            '        strColorSys = rsComSql.Fields("Code").Value
            '    End If
            '    rsComSql = Nothing

            '    strClaritySys = ""
            '    rsComSql = New ADODB.Recordset
            '    rsComSql.Open("SELECT * FROM tblBAGColorClarity WHERE CodeSys = '" & cmbClarity.Text & "' AND Type = 2", AdoCN, 1, 1)
            '    If rsComSql.RecordCount Then
            '        strClaritySys = rsComSql.Fields("Code").Value
            '    End If
            '    rsComSql = Nothing

            '    rsComSql = New ADODB.Recordset
            '    rsComSql.Open("SELECT * FROM tblBAGSizeRange WHERE Color = '" & strColorSys & "' AND Clarity = '" & strClaritySys & "' AND LFrom <= " & CDbl(txtSize.Text) & " AND LTo > " & CDbl(txtSize.Text) & " AND WFrom <= " & CDbl(cmbWidth.Text) & " AND WTo >= " & CDbl(cmbWidth.Text) & "", AdoCN, 1, 1)
            '    If rsComSql.RecordCount Then
            '        If rsComSql.Fields("Need").Value = 0 Then
            '            MsgBox("This is a Non Moving stone", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '            Exit Sub
            '        End If
            '    Else
            '        MsgBox("This stone is NOT in the Range", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '        Exit Sub
            '    End If
            '    rsComSql = Nothing
            '    '---------------
            'End If

            intReIssue = 0
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT ReIssue " & _
                          "FROM tblParcel WHERE (GrpParNo = '" & txtParNo.Text & "') AND (Depart = '" & strDepartment & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                intReIssue = rsComSql.Fields("ReIssue").Value
            End If
            rsComSql = Nothing

            If intReIssue = 0 Then
                dblTrfPcs = 0
                dblTrfCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(NewACTPcs) AS Pcs, ROUND(SUM(NewACTCts), 3) AS Cts " & _
                              "FROM tblDep_Trf WHERE (DCLParcelNo = '" & Mid(txtParNo.Text, 1, 6) & "') AND (Department = '" & strDepartment & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                        dblTrfPcs = rsComSql.Fields("Pcs").Value
                        dblTrfCts = rsComSql.Fields("Cts").Value
                    End If
                End If
                rsComSql = Nothing

                'rsComSql = New ADODB.Recordset
                'rsComSql.Open("SELECT SUM(Pcs) AS Pcs, ROUND(SUM(Cts), 3) AS Cts " & _
                '              "FROM tblDep_Rec WHERE (DCLParcelNo = '" & Mid(txtParNo.Text, 1, 6) & "') AND (Department = '" & strDepartment & "')", AdoCN, 1, 1)
                'If rsComSql.RecordCount Then
                '    If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                '        dblTrfPcs = dblTrfPcs - rsComSql.Fields("Pcs").Value
                '        dblTrfCts = dblTrfCts - rsComSql.Fields("Cts").Value
                '    End If
                'End If
                'rsComSql = Nothing

                dblPktPcs = 0
                dblPktCts = 0
                rstPacket = New ADODB.Recordset
                rstPacket.Open("SELECT * FROM tblBAGPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                If rstPacket.RecordCount Then
                    dblPktPcs = rstPacket.Fields("PktPcs").Value
                    dblPktCts = rstPacket.Fields("PktCts").Value
                End If
                rstPacket = Nothing

                dblIssPcs = 0
                dblIssCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(dbo.tblBAGPacket.PktPcs) AS PktPcs, ROUND(SUM(dbo.tblBAGPacket.PktCts), 3) AS PktCts " & _
                              "FROM dbo.tblBAGPacket INNER JOIN dbo.tblParcel ON dbo.tblBAGPacket.ParNo = dbo.tblParcel.GrpParNo " & _
                              "WHERE (dbo.tblParcel.ReIssue = 0) AND (dbo.tblParcel.Depart = '" & strDepartment & "') AND " & _
                                    "(LEFT(dbo.tblBAGPacket.ParNo, 6) = '" & Mid(txtParNo.Text, 1, 6) & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("PktPcs").Value) Then
                        dblIssPcs = rsComSql.Fields("PktPcs").Value - dblPktPcs
                        dblIssCts = Math.Round(rsComSql.Fields("PktCts").Value - dblPktCts, 3)
                    End If
                End If
                rsComSql = Nothing

                If dblTrfPcs - dblIssPcs < CDbl(txtPcs.Text) Then
                    MsgBox("Invalid Transfer Pcs : " & dblTrfPcs - dblIssPcs, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If

                If Math.Round(dblTrfCts - dblIssCts, 3) < CDbl(txtCts.Text) Then
                    MsgBox("Invalid Transfer Cts : " & Math.Round(dblTrfCts - dblIssCts, 3), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            dblPlannedPcs = 0
            'rsComSql = New ADODB.Recordset
            'rsComSql.Open("SELECT * FROM tblRgfSize WHERE SizeDec = '" & cmbSize.Text & "'", AdoCN, 1, 1)
            'If rsComSql.RecordCount Then
            '    'rsComSql_1 = New ADODB.Recordset
            '    'rsComSql_1.Open("SELECT SUM(PktPcs) AS Pcs FROM dbo.tblBAGPacket WHERE (SizeRange = '" & cmbSize.Text & "') AND (PktIss >= '03/30/2023')", AdoCN, 1, 1)
            '    'If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
            '    '    dblPlannedPcs = rsComSql_1.Fields("Pcs").Value
            '    'End If
            '    'rsComSql_1 = Nothing

            '    'If dblPlannedPcs + CDbl(txtPcs.Text) > rsComSql.Fields("Pcs").Value Then
            '    '    MsgBox("Plan Pcs Exceeds", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '    '    Exit Sub
            '    'End If

            '    If rsComSql.Fields("Pcs").Value = 0 Then
            '        MsgBox("Order Pcs Zero", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '        Exit Sub
            '    End If
            'End If
            'rsComSql = Nothing

            dtpToday = GetToday()

            rstPacket = New ADODB.Recordset
            rstPacket.Open("SELECT * FROM tblBAGPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
            If rstPacket.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblBAGPacket(ParNo,PktNo,PktPcs,PktCts,PktOrgCts,PktOrdNo,PktRefNo,Pktside,AssortNo,PktFlow," & _
                                    "Grp,AParNo,PktIss,PktSize,PktColor,PktID,Clarity,PktCut,FinCts,PlanVal,EstYld,Length,SizeRange,Width,IncUnit,PktIDNew,StoneNo) " & _
                              "VALUES('" & txtParNo.Text & "','" & txtPktNo.Text & "'," & CDbl(txtPcs.Text) & "," & CDbl(txtCts.Text) & "," & CDbl(txtOrgCts.Text) & ",'" & txtOrderNo.Text & "'," & _
                                    "'" & cmbReference.Text & "','N','" & txtAssort.Text & "','" & cmbFlow.Text & "','" & strRight(txtParNo.Text, 1) & "','" & txtSupParNo.Text & "'," & _
                                    "'" & Format(dtpToday, "MM/dd/yyyy") & "','" & txtSize.Text & "','" & cmbColor.Text & "'," & CDbl(txtPktID.Text) & "," & _
                                    "'" & cmbClarity.Text & "','" & cmbCut.Text & "'," & CDbl(txtFinCts.Text) & "," & CDbl(txtPlanVal.Text) & "," & CDbl(txtEstYld.Text) & ",0," & _
                                    "'" & cmbSize.Text & "','" & cmbWidth.Text & "','" & Trim(cmbUnit.Text) & "'," & CDbl(cmbPktIDNew.Text) & ",'" & txtStoneNo.Text & "')")

                AdoCN.Execute("UPDATE tblRPrReturnDetails SET Trf = 1 WHERE ID = " & CDbl(cmbPktIDNew.Text) & "")

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblParcel WHERE GrpParNo = '" & txtParNo.Text & "' AND Depart = '" & strDepartment & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktCts), 3) AS PktCts FROM tblBAGPacket WHERE (ParNo = '" & txtParNo.Text & "')", AdoCN, 1, 1)
                    If Not IsDBNull(rsComSql_2.Fields("PktPcs").Value) Then
                        If rsComSql_1.Fields("IssuedPcs").Value < rsComSql_2.Fields("PktPcs").Value Then
                            AdoCN.Execute("UPDATE tblParcel SET IssuedPcs = " & rsComSql_2.Fields("PktPcs").Value & ",RghPcs = " & rsComSql_2.Fields("PktPcs").Value & ", IssuedCts = " & rsComSql_2.Fields("PktCts").Value & ", RghCts = " & rsComSql_2.Fields("PktCts").Value & " " & _
                                          "WHERE GrpParNo = '" & txtParNo.Text & "' AND Depart = '" & strDepartment & "'")
                        End If
                    End If
                    rsComSql_2 = Nothing
                End If
                rsComSql_1 = Nothing

            Else
                PBResponse = MsgBox("Are you sure to update this Packet?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, Me.Text)
                If PBResponse = MsgBoxResult.Yes Then
                    AdoCN.Execute("UPDATE tblBAGPacket SET PktColor = '" & cmbColor.Text & "',PktID = " & CDbl(txtPktID.Text) & ",Clarity = '" & cmbClarity.Text & "'," & _
                                        "PktCut = '" & cmbCut.Text & "',FinCts = " & CDbl(txtFinCts.Text) & ",PlanVal = " & CDbl(txtPlanVal.Text) & ", " & _
                                        "EstYld = " & CDbl(txtEstYld.Text) & ",Length = 0,PktSize = '" & txtSize.Text & "',Width = '" & cmbWidth.Text & "'," & _
                                        "SizeRange = '" & cmbSize.Text & "',IncUnit = '" & Trim(cmbUnit.Text) & "',PktOrdNo = '" & txtOrderNo.Text & "'," & _
                                        "PktRefNo = '" & cmbReference.Text & "',StoneNo = '" & txtStoneNo.Text & "' " & _
                                  "WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'")

                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblBAGIssues WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        AdoCN.Execute("UPDATE tblBAGPacket SET PktPcs = " & CDbl(txtPcs.Text) & ",PktCts = " & CDbl(txtCts.Text) & ",PktOrgCts = " & CDbl(txtOrgCts.Text) & " " & _
                                      "WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'")

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM tblParcel WHERE GrpParNo = '" & txtParNo.Text & "' AND Depart = '" & strDepartment & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            rsComSql_2 = New ADODB.Recordset
                            rsComSql_2.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktOrgCts), 3) AS PktCts FROM tblBAGPacket WHERE (ParNo = '" & txtParNo.Text & "')", AdoCN, 1, 1)
                            If Not IsDBNull(rsComSql_2.Fields("PktPcs").Value) Then
                                If rsComSql_1.Fields("IssuedPcs").Value < rsComSql_2.Fields("PktPcs").Value Then
                                    AdoCN.Execute("UPDATE tblParcel SET IssuedPcs = " & rsComSql_2.Fields("PktPcs").Value & ",RghPcs = " & rsComSql_2.Fields("PktPcs").Value & ", IssuedCts = " & rsComSql_2.Fields("PktCts").Value & ", RghCts = " & rsComSql_2.Fields("PktCts").Value & " " & _
                                                  "WHERE GrpParNo = '" & txtParNo.Text & "' AND Depart = '" & strDepartment & "'")
                                End If
                            End If
                            rsComSql_2 = Nothing
                        End If
                        rsComSql_1 = Nothing
                    End If
                    rsComSql = Nothing
                End If
            End If
            rstPacket = Nothing
            ClearFields()
        Else
            MsgBox("Please fill all the entries before Save", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            txtParNo.Focus()
        End If
        txtParNo.Focus()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub txtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtCts.Text <> "" Then
                If CDbl(txtCts.Text) <> 0 Then
                    txtSize.Text = Math.Round(CDbl(txtPcs.Text) / CDbl(txtCts.Text), 2)
                End If
            End If
            txtCts.Focus()
        End If
    End Sub

    Private Sub txtCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtCts.Text)
        If Asc(e.KeyChar) = 13 And txtCts.Text <> "" Then
            If CDbl(txtCts.Text) <> 0 Then
                'txtSize.Text = Math.Round(CDbl(txtPcs.Text) / CDbl(txtCts.Text), 2)
                If txtOrgCts.Text = "" Then
                    txtOrgCts.Text = txtCts.Text
                End If

                Get_IncUnit()
            End If
            txtPktID.Focus()
        End If
    End Sub

    Private Sub txtPktID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktID.KeyPress
        Dim strPlanParNo As String
        Dim strPlanPktNo As String
        Dim strOrgParNo As String

        Dim dtLoading As New DataTable("Parcels")

        dtLoading.Columns.Add("ID", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("RghCts", System.Type.GetType("System.String"))

        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If Val(txtPktID.Text) > 0 Then
                strPlanParNo = ""
                strPlanPktNo = ""

                strParNo = ""
                strPktNo = ""

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT ParNo,PktNo,PktCut,Tension,Flo FROM tblRprPacket WHERE ID = " & Val(txtPktID.Text) & "", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strPlanParNo = Trim(rsComSql.Fields("ParNo").Value)
                    strPlanPktNo = Trim(rsComSql.Fields("PktNo").Value)
                    cmbCut.Text = Trim(rsComSql.Fields("PktCut").Value)

                    strParNo = strPlanParNo
                    strPktNo = strPlanPktNo
                End If
                rsComSql = Nothing

                strOrgParNo = ""
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT OrigParcelNo FROM tblParcel WHERE GrpParNo = '" & strPlanParNo & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strOrgParNo = Trim(rsComSql.Fields("OrigParcelNo").Value)
                End If
                rsComSql = Nothing

                If txtSupParNo.Text <> strOrgParNo Then
                    MsgBox("Invalid Packet ID", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    txtPktID.Text = ""
                    txtPktID.Focus()
                    Exit Sub
                End If

                cmbPktIDNew.Items.Clear()

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblRPrReturnDetails WHERE ParNo = '" & strPlanParNo & "' AND PktNo = '" & strPlanPktNo & "' AND (Shape = 'Baguettes' OR Shape = 'PCU2') ORDER BY ID", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If rsComSql.RecordCount = 1 Then
                        txtPcs.Text = rsComSql.Fields("Pcs").Value
                        txtCts.Text = rsComSql.Fields("RghCts").Value
                        txtOrgCts.Text = rsComSql.Fields("RghCts").Value
                        txtFinCts.Text = rsComSql.Fields("FinCts").Value

                        cmbColor.Text = rsComSql.Fields("Color").Value
                        cmbClarity.Text = rsComSql.Fields("Clarity").Value
                        txtPlanVal.Text = Trim(rsComSql.Fields("Value").Value)
                        txtSize.Text = Trim(rsComSql.Fields("Length").Value)
                        cmbWidth.Text = Trim(rsComSql.Fields("Width").Value)
                        cmbPktIDNew.Text = Trim(rsComSql.Fields("ID").Value)
                        cmbCut.Text = Trim(rsComSql.Fields("Cut").Value)
                        txtStoneNo.Text = Trim(rsComSql.Fields("StoneNO").Value)

                        txtStoneID.Text = cmbPktIDNew.Text
                    Else
                        rsComSql.MoveFirst()
                        While Not rsComSql.EOF
                            Dim dr As DataRow
                            dr = dtLoading.NewRow

                            dr("ID") = rsComSql.Fields("ID").Value
                            dr("RghCts") = rsComSql.Fields("RghCts").Value
                            dtLoading.Rows.Add(dr)

                            rsComSql.MoveNext()
                        End While
                        cmbPktIDNew.Focus()
                    End If
                End If
                rsComSql = Nothing

                cmbPktIDNew.SelectedIndex = -1
                cmbPktIDNew.Items.Clear()
                cmbPktIDNew.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable
                cmbPktIDNew.SourceDataString = New String(1) {"ID", "RghCts"}
                cmbPktIDNew.SourceDataTable = dtLoading

                If txtFinCts.Text <> "" And txtCts.Text <> "" Then
                    Get_IncUnit()
                    If CDbl(txtFinCts.Text) > 0 And CDbl(txtCts.Text) > 0 Then
                        txtEstYld.Text = Format(Math.Round((CDbl(txtFinCts.Text) / CDbl(txtCts.Text)) * 100, 2), "#0.00")
                    End If
                End If
            End If

            cmbColor.Focus()
        End If
    End Sub

    Private Sub txtFinCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFinCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtFinCts.Text)
        If Asc(e.KeyChar) = 13 Then
            If txtFinCts.Text <> "" And txtCts.Text <> "" Then
                If CDbl(txtFinCts.Text) > 0 And CDbl(txtCts.Text) > 0 Then
                    txtEstYld.Text = Format(Math.Round((CDbl(txtFinCts.Text) / CDbl(txtCts.Text)) * 100, 2), "#0.00")

                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblBAGPrice WHERE Color = '" & cmbColor.Text & "' AND Clarity = '" & cmbClarity.Text & "' AND SizeRange = '" & cmbSize.Text & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        txtRate.Text = rsComSql.Fields("Price").Value
                        txtPlanVal.Text = Math.Round(CDbl(txtFinCts.Text) * CDbl(txtRate.Text), 2)
                    Else
                        txtPlanVal.Text = "0"
                    End If
                    rsComSql = Nothing

                    txtPlanVal.Focus()
                Else
                    txtEstYld.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub txtEstYld_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEstYld.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtEstYld.Text)
        If Asc(e.KeyChar) = 13 Then
            If txtCts.Text <> "" And txtEstYld.Text <> "" Then
                If CDbl(txtCts.Text) > 0 And CDbl(txtEstYld.Text) > 0 Then
                    txtFinCts.Text = Math.Round(CDbl(txtCts.Text) * CDbl(txtEstYld.Text) / 100, 3)
                End If
            End If
        End If
    End Sub

    Private Sub txtPlanVal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPlanVal.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtPlanVal.Text)
        If Asc(e.KeyChar) = 13 Then
            txtSize.Focus()
        End If
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        objForm = New frm_DCLReportViewer
        mReportName = "PKTSLEEVE_Full.rpt"
        strReportPath = PBReportPath & "Baguettes\" & mReportName
        objForm.Show()
    End Sub

    Private Sub cmbColor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbColor.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbClarity.Focus()
        End If
    End Sub

    Private Sub cmbClarity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbClarity.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbCut.Focus()
        End If
    End Sub

    Private Sub cmbCut_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbCut.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtFinCts.Focus()
        End If
    End Sub

    Private Sub cmdPrint4_Click(sender As Object, e As EventArgs) Handles cmdPrint4.Click
        objForm = New frm_DCLReportViewer
        mReportName = "BagPK4in1.rpt"
        strReportPath = PBReportPath & "Baguettes\" & mReportName
        objForm.Show()
    End Sub

    Private Sub txtSize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSize.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbSize.Focus()
        End If
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub Delete()
        Dim rstPacket As ADODB.Recordset

        If txtParNo.Text <> "" And txtPktNo.Text <> "" Then
            PBResponse = MsgBox("Are you sure to Delete this Packet?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                rstPacket = New ADODB.Recordset
                rstPacket.Open("SELECT * FROM tblBAGPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                If rstPacket.RecordCount Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblBAGIssues WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        AdoCN.Execute("DELETE FROM tblBAGPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'")
                        AdoCN.Execute("UPDATE tblRPrReturnDetails SET Trf = 0 WHERE ID = " & CDbl(cmbPktIDNew.Text) & "")

                        MsgBox("Packet Deleted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        ClearFields()
                    Else
                        MsgBox("Already Issued", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    End If
                    rsComSql = Nothing
                Else
                    MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                End If
                rstPacket = Nothing
            End If
        Else
            MsgBox("Please fill all the entries before Delete", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Delete()
    End Sub

    Private Sub cmbSize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSize.KeyPress
        If Asc(e.KeyChar) = 13 Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblRgfSize WHERE SizeDec = '" & cmbSize.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If txtFinCts.Text <> "" Then
                    txtRate.Text = rsComSql.Fields("Price2").Value
                    txtPlanVal.Text = CDbl(txtRate.Text) * CDbl(txtPcs.Text)
                Else
                    txtRate.Text = "0"
                End If
            Else
                txtRate.Text = "0"
            End If
            rsComSql = Nothing
            cmbWidth.Focus()
        End If
    End Sub

    Private Sub Get_IncUnit()
        Dim dblSize As Double

        If txtPcs.Text <> "" And txtCts.Text <> "" Then
            If CDbl(txtCts.Text) = 0 Then Exit Sub

            dblSize = Math.Round(CDbl(txtPcs.Text) / CDbl(txtCts.Text), 2)
            dblSize = Math.Round(dblSize, 2)

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblBAGIncentiveCat WHERE FromSize <= " & dblSize & " AND ToSize >= " & dblSize & "", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                cmbUnit.Text = Trim(rsComSql.Fields("Unit").Value)
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub cmbPktIDNew_KeyPress(sender As Object, e As KeyPressEventArgs)
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub cmbPktIDNew_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPktIDNew.SelectedIndexChanged
        If Not cmbPktIDNew.SelectedItem Is Nothing Then
            If cmbPktIDNew.Text <> "" Then
                If IsNumeric(cmbPktIDNew.Text) = True Then
                    txtStoneID.Text = cmbPktIDNew.Text

                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblRPrReturnDetails WHERE ParNo = '" & strParNo & "' AND PktNo = '" & strPktNo & "' AND ID = " & CDbl(cmbPktIDNew.Text) & "", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        cmbColor.Text = rsComSql.Fields("Color").Value
                        cmbClarity.Text = Trim(rsComSql.Fields("Clarity").Value)
                        txtPcs.Text = Trim(rsComSql.Fields("Pcs").Value)
                        txtCts.Text = Trim(rsComSql.Fields("RghCts").Value)
                        txtOrgCts.Text = Trim(rsComSql.Fields("RghCts").Value)
                        txtFinCts.Text = Trim(rsComSql.Fields("FinCts").Value)
                        txtPlanVal.Text = Trim(rsComSql.Fields("Value").Value)
                        txtSize.Text = Trim(rsComSql.Fields("Length").Value)
                        cmbCut.Text = Trim(rsComSql.Fields("Cut").Value)
                        cmbWidth.Text = Trim(rsComSql.Fields("Width").Value)
                        txtStoneNo.Text = Trim(rsComSql.Fields("StoneNo").Value)

                        txtSize.Focus()

                        If txtFinCts.Text <> "" And txtCts.Text <> "" Then
                            If CDbl(txtFinCts.Text) > 0 And CDbl(txtCts.Text) > 0 Then
                                txtEstYld.Text = Format(Math.Round((CDbl(txtFinCts.Text) / CDbl(txtCts.Text)) * 100, 2), "#0.00")
                            End If
                        End If

                        Get_IncUnit()
                    End If
                    rsComSql = Nothing
                End If
            End If
        End If
    End Sub

    Private Sub txtOrderNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrderNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            cmbReference.Items.Clear()
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblNoneOrders.OrderNo, dbo.tblNoneOrdersDtls.RefNo " & _
                          "FROM dbo.tblNoneOrders INNER JOIN dbo.tblNoneOrdersDtls ON dbo.tblNoneOrders.OrderNo = dbo.tblNoneOrdersDtls.OrderNo " & _
                          "WHERE (dbo.tblNoneOrders.OrderNo = '" & txtOrderNo.Text & "') AND  (dbo.tblNoneOrders.Complete = N'N') " & _
                          "GROUP BY dbo.tblNoneOrders.OrderNo, dbo.tblNoneOrdersDtls.RefNo " & _
                          "ORDER BY dbo.tblNoneOrdersDtls.RefNo", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    cmbReference.Items.Add(rsComSql.Fields("RefNo").Value)
                    rsComSql.MoveNext()
                End While
            Else
                MsgBox("Invalid Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim intRow As Integer

        If Len(txtStoneID.Text) = 0 Then Exit Sub

        For intRow = 0 To flxStone.Rows.Count - 1
            If txtStoneID.Text = flxStone.Item(4, intRow).Value Then
                MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblRPrReturnDetails WHERE ID = " & CDbl(txtStoneID.Text) & "", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            flxStone.Rows.Add(rsComSql.Fields("ParNo").Value,
                              rsComSql.Fields("Size").Value,
                              rsComSql.Fields("Pcs").Value,
                              rsComSql.Fields("Value").Value,
                              txtStoneID.Text,
                              rsComSql.Fields("RghCts").Value,
                              rsComSql.Fields("FinCts").Value)

            cmbSize.Text = rsComSql.Fields("Size").Value
        End If
        rsComSql = Nothing

        txtTotPcs.Text = CalTotalPcs(flxStone)
        txtTotValue.Text = CalTotalValue(flxStone)
        txtTotRghCts.Text = CalTotalRghCts(flxStone)
        txtTotFinCts.Text = CalTotalFinCts(flxStone)

        cmbPktIDNew.Text = ""
        txtStoneID.Text = ""
        txtStoneID.Focus()
    End Sub


    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(2, intRow).Value)
        Next

    End Function

    Private Function CalTotalRghCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalRghCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalRghCts = CalTotalRghCts + Val(flxSample.Item(5, intRow).Value)
        Next
        CalTotalRghCts = Math.Round(CalTotalRghCts, 3)
    End Function

    Private Function CalTotalFinCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalFinCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalFinCts = CalTotalFinCts + Val(flxSample.Item(6, intRow).Value)
        Next
        CalTotalFinCts = Math.Round(CalTotalFinCts, 3)
    End Function

    Private Function CalTotalValue(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalValue = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalValue = CalTotalValue + Val(flxSample.Item(3, intRow).Value)
        Next
        CalTotalValue = Math.Round(CalTotalValue, 2)
    End Function

    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        txtStoneID.Text = ""
        flxStone.Rows.Clear()

        txtTotPcs.Text = ""
        txtTotValue.Text = ""
        txtTotRghCts.Text = ""
        txtTotFinCts.Text = ""
    End Sub

    Private Sub cmdGetDetails_Click(sender As Object, e As EventArgs) Handles cmdGetDetails.Click
        txtPcs.Text = txtTotPcs.Text
        txtPlanVal.Text = txtTotValue.Text
        txtCts.Text = txtTotRghCts.Text
        txtOrgCts.Text = txtTotRghCts.Text
        txtFinCts.Text = txtTotFinCts.Text

        If txtFinCts.Text <> "" And txtCts.Text <> "" Then
            If CDbl(txtFinCts.Text) > 0 And CDbl(txtCts.Text) > 0 Then
                txtEstYld.Text = Format(Math.Round((CDbl(txtFinCts.Text) / CDbl(txtCts.Text)) * 100, 2), "#0.00")
            End If
        End If

        txtPktID.Text = "0"
        cmbPktIDNew.Items.Clear()
        cmbPktIDNew.Text = "0"
        txtStoneNo.Text = ""

        Get_IncUnit()
    End Sub

    Private Sub txtStoneID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtStoneID.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub flxStone_DoubleClick(sender As Object, e As EventArgs) Handles flxStone.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxStone.Rows.RemoveAt(flxStone.CurrentRow.Index)

            txtTotPcs.Text = CalTotalPcs(flxStone)
            txtTotValue.Text = CalTotalValue(flxStone)
            txtTotRghCts.Text = CalTotalRghCts(flxStone)
            txtTotFinCts.Text = CalTotalFinCts(flxStone)
        End If
    End Sub

    Private Sub txtOrgCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrgCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtOrgCts.Text)
    End Sub
End Class