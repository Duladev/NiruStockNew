
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_PrPacket
    Dim strDepartment As String
    Dim strParNo As String
    Dim strPktNo As String

    Private Sub frm_PrPacket_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        strDepartment = "Princess"

        Load_Clarity()
        Load_Col()
        Load_Cut()
        Load_Flow()
        Load_IncentiveCat()
        Load_Size()
    End Sub

    Private Sub Load_Flow()
        Dim rstflw As ADODB.Recordset

        cmbFlow.Items.Clear()
        rstflw = New ADODB.Recordset
        rstflw.Open("SELECT * FROM tblPRFlow ORDER BY Flow", AdoCN, 1, 1)
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
        rstCut.Open("SELECT * FROM tblRgfSize ORDER BY SizeDec", AdoCN, 1, 1)
        If rstCut.RecordCount Then
            rstCut.MoveFirst()
            Do While Not rstCut.EOF
                cmbSize.Items.Add(rstCut.Fields("SizeDec").Value)
                rstCut.MoveNext()
            Loop
        End If
        rstCut = Nothing
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
                        rsComSql.Open("SELECT SUM(PktPcs) AS Pcs FROM tblPRPacket WHERE ParNo = '" & txtParNo.Text & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                                txtPktPcs.Text = rsComSql.Fields("Pcs").Value
                            Else
                                txtPktPcs.Text = "0"
                            End If
                        Else
                            txtPktPcs.Text = "0"
                        End If
                        rsComSql = Nothing

                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT MAX(PktNo) AS MaxPktNo FROM tblPRPacket WHERE ParNo = '" & txtParNo.Text & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount Then
                            If Not IsDBNull(rsComSql.Fields("MaxPktNo").Value) Then
                                txtPktNo.Text = Format(CDbl(rsComSql.Fields("MaxPktNo").Value) + 1, "000")
                            Else
                                txtPktNo.Text = "001"
                            End If
                        Else
                            txtPktNo.Text = "001"
                        End If
                        rsComSql = Nothing

                        flxDetails.Rows.Clear()
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT * FROM tblPRPacket WHERE ParNo = '" & txtParNo.Text & "' ORDER BY PktNo", AdoCN, 1, 1)
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
                                                    Format(rsComSql.Fields("FinCts").Value, "#0.000"),
                                                    rsComSql.Fields("EstYield").Value,
                                                    Format(rsComSql.Fields("PktIss").Value, "yyyy/MM/dd"),
                                                    rsComSql.Fields("Length").Value,
                                                    rsComSql.Fields("FinDia").Value,
                                                    rsComSql.Fields("PktID").Value,
                                                    rsComSql.Fields("IncUnit").Value,
                                                    rsComSql.Fields("PktIDNew").Value,
                                                    rsComSql.Fields("PktOrdNo").Value,
                                                    rsComSql.Fields("PktRefNo").Value,
                                                    rsComSql.Fields("SizeRange").Value,
                                                    rsComSql.Fields("StoneNo").Value)

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

    Private Sub ClearFields()
        txtPktNo.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtAssort.Text = ""
        cmbColor.Text = ""
        cmbFlow.Text = ""
        cmbClarity.Text = ""
        cmbCut.Text = ""
        cmbSize.Text = ""
        txtEstYld.Text = "0"
        txtFinCts.Text = "0"
        txtPktID.Text = "0"
        cmbPktIDNew.Items.Clear()
        cmbPktIDNew.Text = "0"
        txtAvailPcs.Text = "0"
        txtPktPcs.Text = "0"
        txtPlanVal.Text = "0"
        txtLen.Text = "0"
        txtFinDia.Text = "0"
        cmbUnit.Text = ""
        txtOrderNo.Text = "0"
        cmbReference.Text = "0"
        txtStoneNo.Text = ""
        flxDetails.Rows.Clear()
    End Sub

    Private Sub ClearPacket()
        txtPcs.Text = ""
        txtCts.Text = ""
        cmbColor.Text = ""
        cmbClarity.Text = ""
        cmbCut.Text = ""
        txtEstYld.Text = "0"
        txtFinCts.Text = "0"
        txtPktID.Text = "0"
        cmbPktIDNew.Items.Clear()
        cmbPktIDNew.Text = "0"
        txtPlanVal.Text = "0"
        txtFinDia.Text = "0"
        txtStoneNo.Text = ""
    End Sub

    Private Sub txtPktNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtParNo.Text = "" Then Exit Sub
            If Len(txtParNo.Text) <> 7 Then Exit Sub
            If txtPktNo.Text = "" Then Exit Sub
            If Len(txtPktNo.Text) <> 3 Then Exit Sub

            txtPktNo.Text = UCase(txtPktNo.Text)

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblPRPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                txtPcs.Text = rsComSql.Fields("PktPcs").Value
                txtCts.Text = rsComSql.Fields("PktCts").Value

                cmbColor.Text = Trim(rsComSql.Fields("PktColor").Value)
                cmbClarity.Text = Trim(rsComSql.Fields("Clarity").Value)
                cmbCut.Text = Trim(rsComSql.Fields("PktCut").Value)
                txtPlanVal.Text = Trim(rsComSql.Fields("PlanVal").Value)
                txtFinCts.Text = Trim(rsComSql.Fields("FinCts").Value)
                txtEstYld.Text = Trim(rsComSql.Fields("EstYield").Value)
                txtPktID.Text = Trim(rsComSql.Fields("PktID").Value)
                cmbPktIDNew.Text = Trim(rsComSql.Fields("PktIDNew").Value)
                txtLen.Text = Trim(rsComSql.Fields("Length").Value)
                txtTension.Text = Trim(rsComSql.Fields("Tension").Value)
                txtFinDia.Text = Trim(rsComSql.Fields("FinDia").Value)
                cmbUnit.Text = Trim(rsComSql.Fields("IncUnit").Value)
                txtOrderNo.Text = Trim(rsComSql.Fields("PktOrdNo").Value)
                cmbReference.Text = Trim(rsComSql.Fields("PktRefNo").Value)
                cmbSize.Text = Trim(rsComSql.Fields("SizeRange").Value)
                txtStoneNo.Text = Trim(rsComSql.Fields("StoneNo").Value)
            Else
                MsgBox("New Packet No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearPacket()
                txtPcs.Focus()
            End If
            rsComSql = Nothing
        End If
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

        If txtParNo.Text <> "" And txtPktNo.Text <> "" And txtPcs.Text <> "" And txtCts.Text <> "" And txtPktID.Text <> "" Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT GrpParNo FROM tblParcel WHERE GrpParNo = '" & Trim(txtParNo.Text) & "' AND Depart = 'Princess' AND Complete = 0", AdoCN, 1, 1)
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

            If Len(txtPktNo.Text) <> 3 Then
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

            If txtEstYld.Text = "" Then
                MsgBox("Invalid Est Yield", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If


            If CDbl(txtEstYld.Text) > 100 Then
                MsgBox("Invalid Est Yield", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If txtLen.Text = "" Then
                MsgBox("Invalid Length", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If txtTension.Text = "" Then
                MsgBox("Invalid Tension", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If txtFinDia.Text = "" Then
                MsgBox("Invalid FInish Diameter", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If cmbUnit.Text = "" Then
                MsgBox("Invalid Incentive Category", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

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
                rstPacket.Open("SELECT * FROM tblPRPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                If rstPacket.RecordCount Then
                    dblPktPcs = rstPacket.Fields("PktPcs").Value
                    dblPktCts = rstPacket.Fields("PktCts").Value
                End If
                rstPacket = Nothing

                dblIssPcs = 0
                dblIssCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(dbo.tblPRPacket.PktPcs) AS PktPcs, ROUND(SUM(dbo.tblPRPacket.PktCts), 3) AS PktCts " & _
                              "FROM dbo.tblPRPacket INNER JOIN dbo.tblParcel ON dbo.tblPRPacket.ParNo = dbo.tblParcel.GrpParNo " & _
                              "WHERE (dbo.tblParcel.ReIssue = 0) AND (dbo.tblParcel.Depart = '" & strDepartment & "') AND " & _
                                    "(LEFT(dbo.tblPRPacket.ParNo, 6) = '" & Mid(txtParNo.Text, 1, 6) & "')", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("PktPcs").Value) Then
                        dblIssPcs = rsComSql.Fields("PktPcs").Value - dblPktPcs
                        dblIssCts = rsComSql.Fields("PktCts").Value - dblPktCts
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

            dtpToday = GetToday()

            rstPacket = New ADODB.Recordset
            rstPacket.Open("SELECT * FROM tblPRPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
            If rstPacket.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblPRPacket(ParNo,PktNo,PktPcs,PktCts,PktColor,Assortment,PktFlow,PktIss,PktID,Clarity,PktCut,FinCts,PlanVal,EstYield,Length,Tension,FinDia,IncUnit,PktIDNew,PktOrdNo,PktRefNo,SizeRange,StoneNo) " & _
                              "VALUES('" & txtParNo.Text & "','" & txtPktNo.Text & "'," & CDbl(txtPcs.Text) & "," & CDbl(txtCts.Text) & ",'" & cmbColor.Text & "'," & _
                                    "'" & txtAssort.Text & "','" & cmbFlow.Text & "','" & Format(dtpToday, "MM/dd/yyyy") & "'," & CDbl(txtPktID.Text) & "," & _
                                    "'" & cmbClarity.Text & "','" & cmbCut.Text & "'," & CDbl(txtFinCts.Text) & "," & CDbl(txtPlanVal.Text) & "," & CDbl(txtEstYld.Text) & "," & _
                                    "" & CDbl(txtLen.Text) & ",'" & CDbl(txtTension.Text) & "','" & Trim(txtFinDia.Text) & "','" & cmbUnit.Text & "'," & CDbl(cmbPktIDNew.Text) & ",'" & txtOrderNo.Text & "','" & cmbReference.Text & "','" & cmbSize.Text & "','" & txtStoneNo.Text & "')")

                AdoCN.Execute("UPDATE tblRPrReturnDetails SET Trf = 1 WHERE ID = " & CDbl(cmbPktIDNew.Text) & "")

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblParcel WHERE GrpParNo = '" & txtParNo.Text & "' AND Depart = '" & strDepartment & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktCts), 3) AS PktCts FROM tblPRPacket WHERE (ParNo = '" & txtParNo.Text & "')", AdoCN, 1, 1)
                    If Not IsDBNull(rsComSql_2.Fields("PktPcs").Value) Then
                        If rsComSql_1.Fields("IssuedPcs").Value < rsComSql_2.Fields("PktPcs").Value Then
                            AdoCN.Execute("UPDATE tblParcel SET IssuedPcs = " & rsComSql_2.Fields("PktPcs").Value & ",RghPcs = " & rsComSql_2.Fields("PktPcs").Value & ",IssuedCts = " & rsComSql_2.Fields("PktCts").Value & "," & _
                                          "RghCts = " & rsComSql_2.Fields("PktCts").Value & " " & _
                                          "WHERE GrpParNo = '" & txtParNo.Text & "' AND Depart = '" & strDepartment & "'")
                        End If
                    End If
                    rsComSql_2 = Nothing
                End If
                rsComSql_1 = Nothing

            Else
                PBResponse = MsgBox("Are you sure to update this Packet?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, Me.Text)
                If PBResponse = MsgBoxResult.Yes Then
                    AdoCN.Execute("UPDATE tblPRPacket SET PktColor = '" & cmbColor.Text & "',PktID = " & CDbl(txtPktID.Text) & ",Clarity = '" & cmbClarity.Text & "'," & _
                                        "PktCut = '" & cmbCut.Text & "',FinCts = " & CDbl(txtFinCts.Text) & ",PlanVal = " & CDbl(txtPlanVal.Text) & ",PktFlow = '" & cmbFlow.Text & "', " & _
                                        "EstYield = " & CDbl(txtEstYld.Text) & ",Length = " & CDbl(txtLen.Text) & ",Tension = '" & CDbl(txtTension.Text) & "',FinDia = '" & Trim(txtFinDia.Text) & "'," & _
                                        "IncUnit = '" & Trim(cmbUnit.Text) & "',PktOrdNo = '" & txtOrderNo.Text & "',PktRefNo = '" & cmbReference.Text & "',SizeRange = '" & cmbSize.Text & "',StoneNo = '" & txtStoneNo.Text & "' " & _
                                  "WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'")

                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblPRIssues WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        AdoCN.Execute("UPDATE tblPRPacket SET PktPcs = " & CDbl(txtPcs.Text) & ",PktCts = " & CDbl(txtCts.Text) & " " & _
                                      "WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'")

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM tblParcel WHERE GrpParNo = '" & txtParNo.Text & "' AND Depart = '" & strDepartment & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            rsComSql_2 = New ADODB.Recordset
                            rsComSql_2.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktCts), 3) AS PktCts FROM tblPRPacket WHERE (ParNo = '" & txtParNo.Text & "')", AdoCN, 1, 1)
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

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        objForm = New frm_DCLReportViewer
        mReportName = "PKTSLEEVESQL_FULL.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub cmdPrint4_Click(sender As Object, e As EventArgs) Handles cmdPrint4.Click
        objForm = New frm_DCLReportViewer
        mReportName = "PRPK4in1.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
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

    Private Sub txtFinCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFinCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtFinCts.Text)
        If Asc(e.KeyChar) = 13 Then
            If txtFinCts.Text <> "" And txtCts.Text <> "" Then
                If CDbl(txtFinCts.Text) > 0 And CDbl(txtCts.Text) > 0 Then
                    txtEstYld.Text = Format(Math.Round((CDbl(txtFinCts.Text) / CDbl(txtCts.Text)) * 100, 2), "#0.00")
                    txtPlanVal.Focus()
                End If
            End If
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
            Get_IncUnit()
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

                    strParNo = strPlanParNo
                    strPktNo = strPlanPktNo
                    cmbCut.Text = Trim(rsComSql.Fields("PktCut").Value)
                    txtTension.Text = Trim(rsComSql.Fields("Tension").Value)
                Else
                    txtTension.Text = "0"
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
                'rsComSql = New ADODB.Recordset
                'rsComSql.Open("SELECT * FROM tblRPrReturnDetails WHERE ParNo = '" & strPlanParNo & "' AND PktNo = '" & strPlanPktNo & "' AND (Shape = 'Princess' OR Shape = 'PCU2') ORDER BY ID", AdoCN, 1, 1)
                'If rsComSql.RecordCount Then
                '    rsComSql.MoveFirst()
                '    While Not rsComSql.EOF
                '        cmbPktIDNew.Items.Add(rsComSql.Fields("ID").Value)

                '        rsComSql.MoveNext()
                '    End While
                'End If

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblRPrReturnDetails WHERE ParNo = '" & strPlanParNo & "' AND PktNo = '" & strPlanPktNo & "' AND Shape = 'Princess' ORDER BY ID", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If rsComSql.RecordCount = 1 Then
                        txtPcs.Text = rsComSql.Fields("Pcs").Value
                        txtCts.Text = rsComSql.Fields("RghCts").Value
                        txtFinCts.Text = rsComSql.Fields("FinCts").Value

                        cmbColor.Text = rsComSql.Fields("Color").Value
                        cmbClarity.Text = rsComSql.Fields("Clarity").Value
                        txtPlanVal.Text = Trim(rsComSql.Fields("Value").Value)
                        txtLen.Text = Trim(rsComSql.Fields("Length").Value)
                        cmbPktIDNew.Text = Trim(rsComSql.Fields("ID").Value)
                        txtStoneNo.Text = Trim(rsComSql.Fields("StoneNo").Value)
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

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub Delete()
        Dim rstPacket As ADODB.Recordset

        If txtParNo.Text <> "" And txtPktNo.Text <> "" Then
            PBResponse = MsgBox("Are you sure to Delete this Packet?", MsgBoxStyle.Information + vbYesNo, Me.Text)
            If PBResponse  = MsgBoxResult.Yes Then
                rstPacket = New ADODB.Recordset
                rstPacket.Open("SELECT * FROM tblPRPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                If rstPacket.RecordCount Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblPRIssues WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        AdoCN.Execute("DELETE FROM tblPRPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'")
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

    Private Sub txtLen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLen.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtLen.Text)
    End Sub

    Private Sub Get_IncUnit()
        Dim dblSize As Double

        If txtPcs.Text <> "" And txtCts.Text <> "" Then
            If CDbl(txtCts.Text) = 0 Then Exit Sub

            dblSize = Math.Round(CDbl(txtPcs.Text) / CDbl(txtCts.Text), 2)
            dblSize = Math.Round(dblSize, 2)

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblPrIncentiveCat WHERE FromSize <= " & dblSize & " AND ToSize >= " & dblSize & "", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                cmbUnit.Text = Trim(rsComSql.Fields("Unit").Value)
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub Load_IncentiveCat()
        Dim rstIncCat As ADODB.Recordset

        cmbUnit.Items.Clear()
        rstIncCat = New ADODB.Recordset
        rstIncCat.Open("SELECT DISTINCT Unit FROM tblPrIncentiveCat ORDER BY Unit", AdoCN, 1, 1)
        If rstIncCat.RecordCount Then
            rstIncCat.MoveFirst()
            Do While Not rstIncCat.EOF
                cmbUnit.Items.Add(rstIncCat.Fields("Unit").Value)
                rstIncCat.MoveNext()
            Loop
        End If
        rstIncCat = Nothing
    End Sub

    Private Sub cmbPktIDNew_KeyPress(sender As Object, e As KeyPressEventArgs)
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub cmbPktIDNew_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPktIDNew.SelectedIndexChanged
        If Not cmbPktIDNew.SelectedItem Is Nothing Then
            If cmbPktIDNew.Text <> "" Then
                If IsNumeric(cmbPktIDNew.Text) = True Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblRPrReturnDetails WHERE ParNo = '" & strParNo & "' AND PktNo = '" & strPktNo & "' AND ID = " & CDbl(cmbPktIDNew.Text) & "", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        cmbColor.Text = rsComSql.Fields("Color").Value
                        cmbClarity.Text = Trim(rsComSql.Fields("Clarity").Value)
                        txtPcs.Text = Trim(rsComSql.Fields("Pcs").Value)
                        txtCts.Text = Trim(rsComSql.Fields("RghCts").Value)
                        txtFinCts.Text = Trim(rsComSql.Fields("FinCts").Value)
                        txtPlanVal.Text = Trim(rsComSql.Fields("Value").Value)
                        txtLen.Text = Trim(rsComSql.Fields("Length").Value)
                        cmbCut.Text = Trim(rsComSql.Fields("Cut").Value)
                        txtStoneNo.Text = Trim(rsComSql.Fields("StoneNo").Value)

                        txtLen.Focus()

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

    Private Sub cmbSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSize.SelectedIndexChanged
        If cmbSize.Text <> "" Then
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
        End If
    End Sub
End Class