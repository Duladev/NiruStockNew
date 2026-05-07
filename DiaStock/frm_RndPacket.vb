
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RndPacket

    Dim strDepartment As String
    Dim strParNo As String
    Dim strPktNo As String

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub frm_RndPacket_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        strDepartment = "Rounds"

        Load_Flow()
        Load_Category()
        Load_Col()
        Load_Cut()
        Load_Clarity()
        Load_Model()
        Load_Girdling()
        Load_Crown()
        Load_FinishCut()
        Load_IncentiveCat()
        Load_Mistake()
        Load_Size()

        ClearFields()
    End Sub

    Private Sub Load_Flow()
        Dim rstflw As ADODB.Recordset

        cmbFlow.Items.Clear()
        rstflw = New ADODB.Recordset
        rstflw.Open("SELECT * FROM tblRndFlow ORDER BY Flow", AdoCN, 1, 1)
        If rstflw.RecordCount Then
            rstflw.MoveFirst()
            Do While Not rstflw.EOF
                cmbFlow.Items.Add(rstflw.Fields("Flow").Value)
                rstflw.MoveNext()
            Loop
        End If
        rstflw = Nothing
    End Sub

    Private Sub Load_Category()
        Dim rstCategory As ADODB.Recordset

        cmbCategory.Items.Clear()
        rstCategory = New ADODB.Recordset
        rstCategory.Open("SELECT DISTINCT Category FROM tblRndIncentiveCat ORDER BY Category", AdoCN, 1, 1)
        If rstCategory.RecordCount Then
            rstCategory.MoveFirst()
            Do While Not rstCategory.EOF
                cmbCategory.Items.Add(rstCategory.Fields("Category").Value)
                rstCategory.MoveNext()
            Loop
        End If
        rstCategory = Nothing
    End Sub

    Private Sub Load_Col()
        Dim rstCol As ADODB.Recordset

        cmbColor.Items.Clear()
        rstCol = New ADODB.Recordset
        rstCol.Open("SELECT Color FROM tblColor ORDER BY Color", AdoCN, 1, 1)
        If rstCol.RecordCount Then
            rstCol.MoveFirst()
            Do While Not rstCol.EOF
                cmbColor.Items.Add(rstCol.Fields("Color").Value)
                rstCol.MoveNext()
            Loop
        End If
        rstCol = Nothing
    End Sub

    Private Sub Load_Cut()
        Dim rstCut As ADODB.Recordset

        cmbCut.Items.Clear()
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

        cmbClarity.Items.Clear()
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

    Private Sub Load_Model()
        Dim rstModel As ADODB.Recordset

        cmbModel.Items.Clear()
        rstModel = New ADODB.Recordset
        rstModel.Open("SELECT * FROM tblRndMakble ORDER BY Name", AdoCN, 1, 1)
        If rstModel.RecordCount Then
            rstModel.MoveFirst()
            While Not rstModel.EOF
                cmbModel.Items.Add(rstModel.Fields("Name").Value)
                rstModel.MoveNext()
            End While
        End If
        rstModel = Nothing
    End Sub

    Private Sub Load_Girdling()
        cmbGirdling.Items.Clear()
        cmbGirdling.Items.Add("Thin")
        cmbGirdling.Items.Add("Thick")
        cmbGirdling.Items.Add("Crack")
        cmbGirdling.Items.Add("Gir Bro1")
        cmbGirdling.Items.Add("Gir Bro2")
        cmbGirdling.Items.Add("Gir Bro3")
    End Sub

    Private Sub Load_Crown()
        cmbCrown.Items.Clear()
        cmbCrown.Items.Add("Very Very Thin")
        cmbCrown.Items.Add("Thin")
        cmbCrown.Items.Add("Thick")
    End Sub

    Private Sub Load_Size()
        Dim rstSize As ADODB.Recordset

        cmbSize.Items.Clear()
        rstSize = New ADODB.Recordset
        rstSize.Open("SELECT * FROM tblRndSizeRange ORDER BY SizeRange", AdoCN, 1, 1)
        If rstSize.RecordCount Then
            rstSize.MoveFirst()
            Do While Not rstSize.EOF
                cmbSize.Items.Add(rstSize.Fields("SizeRange").Value)
                rstSize.MoveNext()
            Loop
        End If
        rstSize = Nothing

        'cmbSize.Items.Add("0.180 - 0.229")
        'cmbSize.Items.Add("0.230 - 0.290")
    End Sub

    Private Sub Load_FinishCut()
        Dim rstCut As ADODB.Recordset

        cmbFinCut.Items.Clear()
        rstCut = New ADODB.Recordset
        rstCut.Open("SELECT * FROM tblRndFinCut ORDER BY FinCut", AdoCN, 1, 1)
        If rstCut.RecordCount Then
            rstCut.MoveFirst()
            Do While Not rstCut.EOF
                cmbFinCut.Items.Add(rstCut.Fields("FinCut").Value)
                rstCut.MoveNext()
            Loop
        End If
        rstCut = Nothing
    End Sub

    Private Sub Load_IncentiveCat()
        Dim rstIncCat As ADODB.Recordset

        cmbUnit.Items.Clear()
        rstIncCat = New ADODB.Recordset
        rstIncCat.Open("SELECT DISTINCT Unit FROM tblRndIncentiveCat ORDER BY Unit", AdoCN, 1, 1)
        If rstIncCat.RecordCount Then
            rstIncCat.MoveFirst()
            Do While Not rstIncCat.EOF
                cmbUnit.Items.Add(rstIncCat.Fields("Unit").Value)
                rstIncCat.MoveNext()
            Loop
        End If
        rstIncCat = Nothing
    End Sub

    Private Sub Load_Mistake()
        Dim rstMistake As ADODB.Recordset

        cmbMistake.Items.Clear()
        rstMistake = New ADODB.Recordset
        rstMistake.Open("SELECT * FROM tblMistake ORDER BY Mistake", AdoCN, 1, 1)
        If rstMistake.RecordCount Then
            rstMistake.MoveFirst()
            Do While Not rstMistake.EOF
                cmbMistake.Items.Add(rstMistake.Fields("Mistake").Value)
                rstMistake.MoveNext()
            Loop
        End If
        rstMistake = Nothing
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        Dim rstPacket As ADODB.Recordset
        Dim strGrp As String

        If Asc(e.KeyChar) = 13 Then
            If txtParNo.Text <> "" Then
                If Len(txtParNo.Text) = 8 Then
                    txtParNo.Text = UCase(txtParNo.Text)
                    strGrp = strRight(txtParNo.Text, 1)
                    txtGroup.Text = strGrp

                    rstPacket = New ADODB.Recordset
                    rstPacket.Open("SELECT * FROM tblParcel WHERE GrpParNo = '" & txtParNo.Text & "' AND Depart = '" & strDepartment & "'", AdoCN, 1, 1)
                    If rstPacket.RecordCount > 0 Then
                        cmbFlow.Text = rstPacket.Fields("Flow").Value
                        txtAvailPcs.Text = rstPacket.Fields("IssuedPcs").Value
                        txtAssort.Text = rstPacket.Fields("Assortment").Value
                        txtSupParNo.Text = rstPacket.Fields("OrigParcelNo").Value
                        cmbCategory.Text = rstPacket.Fields("Category").Value

                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT SUM(PktPcs) AS Pcs FROM tblRndPacket WHERE ParNo = '" & txtParNo.Text & "'", AdoCN, 1, 1)
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
                        rsComSql.Open("SELECT MAX(CONVERT(numeric, PktNo)) AS MaxPktNo FROM tblRndPacket WHERE ParNo = '" & txtParNo.Text & "'", AdoCN, 1, 1)
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
        txtOrgCts.Text = ""
        txtAssort.Text = ""
        cmbColor.Text = ""
        cmbFlow.Text = ""
        cmbClarity.Text = ""
        cmbCut.Text = ""
        cmbModel.Text = "-"
        cmbGirdling.Text = ""
        cmbCrown.Text = ""
        txtEstYld.Text = "0"
        txtFinCts.Text = "0"
        txtPktID.Text = "0"
        txtSize.Text = "0"
        txtAvailPcs.Text = "0"
        txtPktPcs.Text = "0"
        txtPlanVal.Text = "0"
        cmbCategory.Text = ""
        txtGroup.Text = ""
        txtRevPoint.Text = "0"
        txtActDiameter.Text = "0"
        txtTension.Text = "0"
        cmbFinCut.Text = ""
        cmbMistake.Text = ""
        cmbSize.Text = ""
        txtPlanValAdj.Text = "0"
        txtFlo.Text = ""
        flxDetails.Rows.Clear()
        cmbPktNo.Items.Clear()
        cmbPktNo.Text = ""
        cmbPktIDNew.Text = "0"
        txtOrderNo.Text = "0"
        cmbReference.Text = "0"
        txtStoneNo.Text = ""
        cmbPktIDNew.Items.Clear()
    End Sub

    Private Sub ClearPacket()
        txtPcs.Text = ""
        txtCts.Text = ""
        txtOrgCts.Text = ""
        cmbColor.Text = ""
        cmbClarity.Text = ""
        cmbCut.Text = ""
        cmbModel.Text = "-"
        cmbGirdling.Text = ""
        cmbCrown.Text = ""
        txtEstYld.Text = "0"
        txtFinCts.Text = "0"
        txtPktID.Text = "0"
        txtSize.Text = "0"
        txtPlanVal.Text = "0"
        txtRevPoint.Text = "0"
        txtActDiameter.Text = "0"
        txtTension.Text = "0"
        cmbFinCut.Text = ""
        cmbMistake.Text = ""
        cmbSize.Text = ""
        txtPlanValAdj.Text = "0"
        txtFlo.Text = ""
        cmbPktNo.Items.Clear()
        cmbPktNo.Text = ""
        cmbPktIDNew.Text = "0"
        txtOrderNo.Text = "0"
        cmbReference.Text = "0"
        txtStoneNo.Text = ""
        cmbPktIDNew.Items.Clear()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub txtPktNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtParNo.Text = "" Then Exit Sub
            If Len(txtParNo.Text) <> 8 Then Exit Sub
            If txtPktNo.Text = "" Then Exit Sub
            If Len(txtPktNo.Text) < 3 Then Exit Sub

            txtPktNo.Text = UCase(txtPktNo.Text)

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblRndPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                txtPcs.Text = rsComSql.Fields("PktPcs").Value
                txtCts.Text = rsComSql.Fields("PktCts").Value
                txtOrgCts.Text = rsComSql.Fields("PktOrgCts").Value
                txtGroup.Text = Trim(rsComSql.Fields("Grp").Value)

                cmbColor.Text = Trim(rsComSql.Fields("PktColor").Value)
                cmbClarity.Text = Trim(rsComSql.Fields("Clarity").Value)
                cmbCut.Text = Trim(rsComSql.Fields("PktCut").Value)
                txtPlanVal.Text = Trim(rsComSql.Fields("PlanVal").Value)
                txtFinCts.Text = Trim(rsComSql.Fields("FinCts").Value)
                txtEstYld.Text = Trim(rsComSql.Fields("EstYld").Value)
                txtPktID.Text = Trim(rsComSql.Fields("PktID").Value)
                cmbPktIDNew.Text = Trim(rsComSql.Fields("PktIDNew").Value)
                txtSize.Text = Trim(rsComSql.Fields("Sieve").Value)
                cmbModel.Text = Trim(rsComSql.Fields("Model").Value)
                cmbGirdling.Text = Trim(rsComSql.Fields("Girdling").Value)
                cmbCrown.Text = Trim(rsComSql.Fields("Crown").Value)
                cmbUnit.Text = Trim(rsComSql.Fields("IncUnit").Value)
                cmbFinCut.Text = Trim(rsComSql.Fields("FinCut").Value)
                txtTension.Text = Trim(rsComSql.Fields("Tension").Value)
                txtRevPoint.Text = Trim(rsComSql.Fields("RevPoint").Value)
                txtDiameter.Text = Trim(rsComSql.Fields("Diameter").Value)
                txtActDiameter.Text = Trim(rsComSql.Fields("ActDiameter").Value)
                cmbMistake.Text = Trim(rsComSql.Fields("Mistake").Value)
                txtPlanValAdj.Text = Trim(rsComSql.Fields("PlanValAdj").Value)
                cmbSize.Text = Trim(rsComSql.Fields("PktSize").Value)
                txtFlo.Text = Trim(rsComSql.Fields("Flo").Value)
                txtOrderNo.Text = Trim(rsComSql.Fields("PktOrdNo").Value)
                cmbReference.Text = Trim(rsComSql.Fields("PktRefNo").Value)
                txtStoneNo.Text = Trim(rsComSql.Fields("StoneNo").Value)
            Else
                MsgBox("New Packet No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearPacket()
                txtPktID.Focus()
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub Get_IncUnit()
        Dim dblSize As Double

        If txtPcs.Text <> "" And txtCts.Text <> "" Then
            If CDbl(txtCts.Text) = 0 Then Exit Sub

            dblSize = Math.Round(CDbl(txtPcs.Text) / CDbl(txtCts.Text), 3)
            dblSize = Math.Round(dblSize, 3)

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblRndIncentiveCat WHERE FromSize <= " & dblSize & " AND ToSize >= " & dblSize & " AND Category = '" & cmbCategory.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                cmbUnit.Text = Trim(rsComSql.Fields("Unit").Value)
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub txtCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtCts.Text)
        If Asc(e.KeyChar) = 13 And txtCts.Text <> "" Then
            If CDbl(txtCts.Text) <> 0 Then
                If txtOrgCts.Text = "" Then
                    txtOrgCts.Text = txtCts.Text
                End If

                Get_IncUnit()
            End If
            cmbColor.Focus()
        End If
    End Sub

    Private Sub txtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtCts.Text <> "" Then
                If CDbl(txtCts.Text) <> 0 Then
                    'txtSize.Text = Math.Round(CDbl(txtPcs.Text) / CDbl(txtCts.Text), 2)
                End If
            End If
            txtCts.Focus()
        End If
    End Sub

    Private Sub cmbColor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbColor.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbClarity.Focus()
        End If
    End Sub

    Private Sub cmbClarity_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbClarity.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtSize.Focus()
        End If
    End Sub

    Private Sub txtSize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSize.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtSize.Text)
        If Asc(e.KeyChar) = 13 Then
            cmbCut.Focus()
        End If
    End Sub

    Private Sub cmbCut_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbCut.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtEstYld.Focus()
        End If
    End Sub

    Private Sub txtPlanVal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPlanVal.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtPlanVal.Text)
        If Asc(e.KeyChar) = 13 Then
            cmbSize.Focus()
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

    Private Sub txtEstYld_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEstYld.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtEstYld.Text)
        If Asc(e.KeyChar) = 13 Then
            If txtPcs.Text <> "" And txtEstYld.Text <> "" Then
                If CDbl(txtPcs.Text) > 0 Then
                    txtFinCts.Text = Math.Round(CDbl(txtCts.Text) * CDbl(txtEstYld.Text) / 100, 3)
                    If Val(txtPcs.Text) <> 1 Then
                        txtPlanVal.Text = Math.Round(CDbl(txtFinCts.Text) * 600, 2)
                    End If
                End If
            End If
            txtFinCts.Focus()
        End If
    End Sub

    Private Sub cmbModel_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbModel.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbGirdling.Focus()
        End If
    End Sub

    Private Sub cmbGirdling_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbGirdling.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbCrown.Focus()
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

        If txtParNo.Text <> "" And txtPktNo.Text <> "" And txtPcs.Text <> "" And txtCts.Text <> "" And txtOrgCts.Text <> "" And txtPktID.Text <> "" Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT GrpParNo FROM tblParcel WHERE GrpParNo = '" & Trim(txtParNo.Text) & "' AND Depart = '" & strDepartment & "' AND Complete = 0", AdoCN, 1, 1)
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

            If Len(txtPktNo.Text) < 3 Or Len(txtPktNo.Text) > 4 Then
                MsgBox("Invalid Packet No.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If cmbFlow.Text = "" Then
                MsgBox("Invalid Flow", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If cmbColor.Text = "" Then
                MsgBox("Invalid Color", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If cmbClarity.Text = "" Then
                MsgBox("Invalid Clarity", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If cmbModel.Text = "" Then
                MsgBox("Invalid Model", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If txtFinCts.Text = "" Then
                MsgBox("Invalid Finish Cts", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(txtCts.Text) > CDbl(txtOrgCts.Text) Then
                MsgBox("Invalid Original Cts", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
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

            If txtPlanValAdj.Text = "" Then
                MsgBox("Invalid Adjusted Planning Value", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If txtRevPoint.Text = "" Then
                MsgBox("Invalid Revised Pointers", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
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

            If txtSize.Text = "" Then
                MsgBox("Invalid Diameter", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            'If CDbl(txtSize.Text) > 9 Then
            '    MsgBox("Invalid Diameter", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
            '    Exit Sub
            'End If

            If txtActDiameter.Text = "" Then
                MsgBox("Invalid Act. Diameter", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If txtGroup.Text = "" Then
                MsgBox("Invalid Group", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
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

            intReIssue = 0
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT ReIssue " & _
                          "FROM tblParcel WHERE (GrpParNo = '" & txtParNo.Text & "') AND (Depart = '" & strDepartment & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                intReIssue = rsComSql.Fields("ReIssue").Value
            End If
            rsComSql = Nothing

            'If strRight(txtParNo.Text, 1) = "G" Or strRight(txtParNo.Text, 1) = "Q" Then
            '    If cmbColor.Text <> "D" And cmbColor.Text <> "E" And cmbColor.Text <> "F" And cmbColor.Text <> "G" And cmbColor.Text <> "H" Then
            '        MsgBox("Invalid Color", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '        Exit Sub
            '    End If
            '    If Mid(cmbClarity.Text, 1, 1) <> "V" And cmbClarity.Text <> "IF" Then
            '        MsgBox("Invalid Clarity", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '        Exit Sub
            '    End If
            '    If CDbl(txtFinCts.Text) < 0.18 Or CDbl(txtFinCts.Text) >= 0.3 Then
            '        MsgBox("Invalid Fin Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '        Exit Sub
            '    End If
            '    If txtFlo.Text <> "NONE" Then
            '        MsgBox("Invalid Fluorescent", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            '        Exit Sub
            '    End If
            'End If

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
                rstPacket.Open("SELECT * FROM tblRndPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                If rstPacket.RecordCount Then
                    dblPktPcs = rstPacket.Fields("PktPcs").Value
                    dblPktCts = rstPacket.Fields("PktCts").Value
                End If
                rstPacket = Nothing

                dblIssPcs = 0
                dblIssCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(dbo.tblRndPacket.PktPcs) AS PktPcs, ROUND(SUM(dbo.tblRndPacket.PktCts), 3) AS PktCts " & _
                              "FROM dbo.tblRndPacket INNER JOIN dbo.tblParcel ON dbo.tblRndPacket.ParNo = dbo.tblParcel.GrpParNo " & _
                              "WHERE (dbo.tblParcel.ReIssue = 0) AND (dbo.tblParcel.Depart = '" & strDepartment & "') AND " & _
                                    "(LEFT(dbo.tblRndPacket.ParNo, 6) = '" & Mid(txtParNo.Text, 1, 6) & "')", AdoCN, 1, 1)
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
            rstPacket.Open("SELECT * FROM tblRndPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
            If rstPacket.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblRndPacket(ParNo,PktNo,PktPcs,PktCts,PktOrgCts,PktOrdNo,PktRefNo,Pktside,AssortNo,PktFlow," & _
                                    "Grp,OrgParNo,PktIss,Sieve,PktColor,PktID,Clarity,PktCut,FinCts,PlanVal,EstYld,PktCategory,IncUnit,Model," & _
                                    "Girdling,Crown,Diameter,RevPoint,FinCut,ActDiameter,Tension,Mistake,PlanValAdj,Opt,doneBy,doneFrom,PktSize,Flo,PktIDNew,StoneNo) " & _
                              "VALUES('" & txtParNo.Text & "','" & txtPktNo.Text & "'," & CDbl(txtPcs.Text) & "," & CDbl(txtCts.Text) & "," & CDbl(txtOrgCts.Text) & ",'" & txtOrderNo.Text & "'," & _
                                    "'" & cmbReference.Text & "','N','" & txtAssort.Text & "','" & cmbFlow.Text & "','" & Trim(UCase(txtGroup.Text)) & "','" & txtSupParNo.Text & "'," & _
                                    "'" & Format(dtpToday, "MM/dd/yyyy") & "','" & txtSize.Text & "','" & cmbColor.Text & "'," & CDbl(txtPktID.Text) & "," & _
                                    "'" & cmbClarity.Text & "','" & cmbCut.Text & "'," & CDbl(txtFinCts.Text) & "," & CDbl(txtPlanVal.Text) & "," & CDbl(txtEstYld.Text) & "," & _
                                    "'" & cmbCategory.Text & "','" & cmbUnit.Text & "','" & cmbModel.Text & "','" & cmbGirdling.Text & "','" & cmbCrown.Text & "','" & txtDiameter.Text & "'," & _
                                    "" & CDbl(txtRevPoint.Text) & ",'" & cmbFinCut.Text & "'," & CDbl(txtActDiameter.Text) & "," & CDbl(txtTension.Text) & ",'" & cmbMistake.Text & "'," & _
                                    "'" & CDbl(txtPlanValAdj.Text) & "','','" & PBUser_EmpNo & "','" & PBCompName & "','" & cmbSize.Text & "','" & txtFlo.Text & "'," & CDbl(cmbPktIDNew.Text) & ",'" & txtStoneNo.Text & "')")

                AdoCN.Execute("UPDATE tblRPrReturnDetails SET Trf = 1 WHERE ID = " & CDbl(cmbPktIDNew.Text) & "")

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblParcel WHERE GrpParNo = '" & txtParNo.Text & "' AND Depart = '" & strDepartment & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktOrgCts), 3) AS PktCts FROM tblRndPacket WHERE (ParNo = '" & txtParNo.Text & "')", AdoCN, 1, 1)
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
                PBResponse = MsgBox("Are you sure to update this Packet?", MsgBoxStyle.Information + vbYesNo, Me.Text)
                If PBResponse = MsgBoxResult.Yes Then
                    AdoCN.Execute("UPDATE tblRndPacket SET PktColor = '" & cmbColor.Text & "',PktID = " & CDbl(txtPktID.Text) & ",Clarity = '" & cmbClarity.Text & "'," & _
                                        "PktCut = '" & cmbCut.Text & "',FinCts = " & CDbl(txtFinCts.Text) & ",PlanVal = " & CDbl(txtPlanVal.Text) & ",PktFlow = '" & cmbFlow.Text & "', " & _
                                        "EstYld = " & CDbl(txtEstYld.Text) & ",IncUnit = '" & cmbUnit.Text & "',Grp = '" & Trim(UCase(txtGroup.Text)) & "',Model = '" & cmbModel.Text & "'," & _
                                        "Girdling = '" & cmbGirdling.Text & "',Crown = '" & cmbCrown.Text & "',Diameter = '" & txtDiameter.Text & "',RevPoint = " & CDbl(txtRevPoint.Text) & "," & _
                                        "FinCut = '" & cmbFinCut.Text & "',ActDiameter = " & CDbl(txtActDiameter.Text) & ",Mistake = '" & cmbMistake.Text & "',Sieve = '" & txtSize.Text & "'," & _
                                        "PlanValAdj = '" & CDbl(txtPlanValAdj.Text) & "',Tension = " & CDbl(txtTension.Text) & ",PktSize = '" & cmbSize.Text & "',PktOrdNo = '" & txtOrderNo.Text & "'," & _
                                        "PktRefNo = '" & cmbReference.Text & "',StoneNo = '" & txtStoneNo.Text & "' " & _
                                  "WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'")

                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblRndIssues WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        AdoCN.Execute("UPDATE tblRndPacket SET PktPcs = " & CDbl(txtPcs.Text) & ",PktCts = " & CDbl(txtCts.Text) & ",PktOrgCts = " & CDbl(txtOrgCts.Text) & " " & _
                                      "WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'")

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM tblParcel WHERE GrpParNo = '" & txtParNo.Text & "' AND Depart = '" & strDepartment & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            rsComSql_2 = New ADODB.Recordset
                            rsComSql_2.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktOrgCts), 3) AS PktCts FROM tblRndPacket WHERE (ParNo = '" & txtParNo.Text & "')", AdoCN, 1, 1)
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

    Private Sub Delete()
        Dim rstPacket As ADODB.Recordset

        If txtParNo.Text <> "" And txtPktNo.Text <> "" Then
            PBResponse = MsgBox("Are you sure to Delete this Packet?", MsgBoxStyle.Information + vbYesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                rstPacket = New ADODB.Recordset
                rstPacket.Open("SELECT * FROM tblRndPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                If rstPacket.RecordCount Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblRndIssues WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        AdoCN.Execute("DELETE FROM tblRndPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "'")

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

    Private Sub txtPktID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktID.KeyPress
        Dim strPlanParNo As String
        Dim strPlanPktNo As String
        Dim strBruParNo As String
        Dim strBruPktNo As String
        Dim strOrgParNo As String

        Dim dtLoading As New DataTable("Parcels")

        dtLoading.Columns.Add("ID", System.Type.GetType("System.String"))
        dtLoading.Columns.Add("RghCts", System.Type.GetType("System.String"))

        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 And Len(txtPktID.Text) > 0 Then
            If Val(txtPktID.Text) > 0 Then
                strPlanParNo = ""
                strPlanPktNo = ""
                strBruParNo = ""
                strBruPktNo = ""

                strParNo = ""
                strPktNo = ""

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT ParNo,PktNo,PktCut,Tension,Flo FROM tblRprPacket WHERE ID = " & Val(txtPktID.Text) & "", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strPlanParNo = Trim(rsComSql.Fields("ParNo").Value)
                    strPlanPktNo = Trim(rsComSql.Fields("PktNo").Value)
                    cmbCut.Text = Trim(rsComSql.Fields("PktCut").Value)
                    txtFlo.Text = Trim(rsComSql.Fields("Flo").Value)
                    txtTension.Text = Trim(rsComSql.Fields("Tension").Value)

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

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblRPrReturnDetails WHERE ParNo = '" & strPlanParNo & "' AND PktNo = '" & strPlanPktNo & "' AND Shape = 'Rounds' ORDER BY ID", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If rsComSql.RecordCount = 1 Then
                        txtPcs.Text = rsComSql.Fields("Pcs").Value
                        txtCts.Text = rsComSql.Fields("RghCts").Value
                        txtOrgCts.Text = rsComSql.Fields("RghCts").Value
                        txtFinCts.Text = rsComSql.Fields("FinCts").Value

                        cmbColor.Text = rsComSql.Fields("Color").Value
                        cmbClarity.Text = rsComSql.Fields("Clarity").Value
                        cmbCut.Text = rsComSql.Fields("Cut").Value
                        txtPlanVal.Text = Trim(rsComSql.Fields("Value").Value)
                        txtSize.Text = Trim(rsComSql.Fields("Length").Value)
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

                cmbPktNo.Items.Clear()
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT dbo.tblRPrPacket.PktColor, dbo.tblRPrPacket.PktClarity, dbo.tblRPrPacket.PktCut, dbo.tblRPrReturns.RetPcsT + dbo.tblRPrReturns.RetPcsB AS RetPcs, " & _
                                "dbo.tblRPrReturns.RetCts, dbo.tblRPrPacket.FinCts, dbo.tblRPrPacket.ParNo, dbo.tblRPrPacket.PktNo, dbo.tblRPrPacket.PktCts " & _
                              "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrPacket.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrReturns.ParNo AND  " & _
                                "dbo.tblRPrPacket.PktNo = dbo.tblRPrReturns.PktNo " & _
                              "WHERE (dbo.tblRPrPacket.PktID = " & Val(txtPktID.Text) & ") AND (dbo.tblRPrPacket.Department = 'RoughBruting') AND (dbo.tblRPrReturns.Sec = 20) AND (dbo.tblRPrPacket.ParNo = '" & txtParNo.Text & "') " & _
                              "ORDER BY dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    rsComSql.MoveFirst()
                    If rsComSql.RecordCount = 1 Then
                        strBruParNo = Trim(rsComSql.Fields("ParNo").Value)
                        strBruPktNo = Trim(rsComSql.Fields("PktNo").Value)
                        txtOrgCts.Text = rsComSql.Fields("PktCts").Value
                        txtFinCts.Text = rsComSql.Fields("FinCts").Value

                        cmbColor.Text = rsComSql.Fields("PktColor").Value
                        cmbClarity.Text = rsComSql.Fields("PktClarity").Value
                        cmbCut.Text = rsComSql.Fields("PktCut").Value
                        txtPcs.Text = rsComSql.Fields("RetPcs").Value
                        txtCts.Text = rsComSql.Fields("RetCts").Value
                    Else
                        While Not rsComSql.EOF
                            cmbPktNo.Items.Add(Trim(rsComSql.Fields("PktNo").Value))

                            rsComSql.MoveNext()
                        End While
                    End If
                End If
                rsComSql = Nothing

                If txtFinCts.Text <> "" And txtCts.Text <> "" Then
                    If CDbl(txtFinCts.Text) > 0 And CDbl(txtCts.Text) > 0 Then
                        txtEstYld.Text = Format(Math.Round((CDbl(txtFinCts.Text) / CDbl(txtCts.Text)) * 100, 2), "#0.00")
                    End If
                End If

                If Len(txtCts.Text) > 0 Then
                    If CDbl(txtCts.Text) <> 0 Then
                        Get_IncUnit()
                        txtSize.Focus()
                    Else
                        txtCts.Focus()
                    End If
                End If
            Else
                txtPcs.Focus()
            End If
        End If
    End Sub

    Private Sub txtRevPoint_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRevPoint.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRevPoint.Text)
    End Sub

    Private Sub txtDiameter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDiameter.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtDiameter.Text)
    End Sub

    Private Sub txtActDiameter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtActDiameter.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtActDiameter.Text)
        If Asc(e.KeyChar) = 13 Then
            txtPlanValAdj.Focus()
        End If
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        objForm = New frm_DCLReportViewer
        mReportName = "RndPKTSLEEVE_SmlStone.rpt"
        strReportPath = PBReportPath & "RoundsFullFlow\" & mReportName
        objForm.Show()
    End Sub

    Private Sub cmdPrint4_Click(sender As Object, e As EventArgs) Handles cmdPrint4.Click
        objForm = New frm_DCLReportViewer
        mReportName = "RndPKTSLEEVE_BigStone.rpt"
        strReportPath = PBReportPath & "RoundsFullFlow\" & mReportName
        objForm.Show()
    End Sub

    Private Sub cmdPrintReCut_Click(sender As Object, e As EventArgs) Handles cmdPrintReCut.Click
        objForm = New frm_DCLReportViewer
        mReportName = "RndPKTSLEEVE_RoughNLE.rpt"
        strReportPath = PBReportPath & "RoundsFullFlow\" & mReportName
        objForm.Show()
    End Sub

    Private Sub txtPlanValAdj_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPlanValAdj.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtPlanValAdj.Text)
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        'mReportName = "RndPKTSLEEVE_Stkr.rpt"
        'mReportName = "RndPKTSLEEVE_Lable.rpt"
        mReportName = "RndPKTSLEEVE_A4.rpt"
        strReportPath = PBReportPath & "RoundsFullFlow\" & mReportName
        objForm.Show()
    End Sub

    Private Sub txtOrgCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrgCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtOrgCts.Text)
    End Sub

    Private Sub cmbPktNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPktNo.SelectedIndexChanged
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT dbo.tblRPrPacket.PktColor, dbo.tblRPrPacket.PktClarity, dbo.tblRPrPacket.PktCut, dbo.tblRPrReturns.RetPcsT + dbo.tblRPrReturns.RetPcsB AS RetPcs, " & _
                        "dbo.tblRPrReturns.RetCts, dbo.tblRPrPacket.FinCts, dbo.tblRPrPacket.ParNo, dbo.tblRPrPacket.PktNo, dbo.tblRPrPacket.PktCts " & _
                      "FROM dbo.tblRPrPacket INNER JOIN dbo.tblRPrReturns ON dbo.tblRPrPacket.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrPacket.ParNo = dbo.tblRPrReturns.ParNo AND  " & _
                        "dbo.tblRPrPacket.PktNo = dbo.tblRPrReturns.PktNo " & _
                      "WHERE (dbo.tblRPrPacket.PktID = " & Val(txtPktID.Text) & ") AND (dbo.tblRPrPacket.Department = 'RoughBruting') AND (dbo.tblRPrReturns.Sec = 20) AND (dbo.tblRPrPacket.PktNo = '" & cmbPktNo.Text & "') " & _
                      "ORDER BY dbo.tblRPrPacket.PktNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            txtOrgCts.Text = rsComSql.Fields("PktCts").Value
            txtFinCts.Text = rsComSql.Fields("FinCts").Value

            cmbColor.Text = rsComSql.Fields("PktColor").Value
            cmbClarity.Text = rsComSql.Fields("PktClarity").Value
            cmbCut.Text = rsComSql.Fields("PktCut").Value
            txtPcs.Text = rsComSql.Fields("RetPcs").Value
            txtCts.Text = rsComSql.Fields("RetCts").Value
        End If
        rsComSql = Nothing

        Get_IncUnit()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Delete()
    End Sub

    Private Sub cmbPktIDNew_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPktIDNew.SelectedIndexChanged
        If Not cmbPktIDNew.SelectedItem Is Nothing Then
            If cmbPktIDNew.Text <> "" Then
                If IsNumeric(cmbPktIDNew.Text) = True Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblRPrReturnDetails WHERE ParNo = '" & strParNo & "' AND PktNo = '" & strPktNo & "' AND Shape = 'Rounds' AND ID = " & CDbl(cmbPktIDNew.Text) & "", AdoCN, 1, 1)
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
                        txtStoneNo.Text = Trim(rsComSql.Fields("StoneNo").Value)

                        'If Trim(rsComSql.Fields("Cut").Value) = "VG" Then
                        '    cmbCut.Text = "Very Good"
                        'Else
                        '    cmbCut.Text = Trim(rsComSql.Fields("Cut").Value)
                        'End If
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

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblRndPacket WHERE ParNo = '" & txtParNo.Text & "' ORDER BY CONVERT(NUMERIC, PktNo)", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("PktColor").Value,
                                    rsComSql.Fields("Clarity").Value,
                                    rsComSql.Fields("PktCut").Value,
                                    rsComSql.Fields("PktPcs").Value,
                                    Format(rsComSql.Fields("PktCts").Value, "#0.000"),
                                    rsComSql.Fields("PlanVal").Value,
                                    rsComSql.Fields("Grp").Value,
                                    Format(rsComSql.Fields("PktIss").Value, "yyyy-MM-dd"),
                                    rsComSql.Fields("IncUnit").Value,
                                    rsComSql.Fields("FinCts").Value,
                                    rsComSql.Fields("PktSize").Value,
                                    rsComSql.Fields("Sieve").Value,
                                    Format(rsComSql.Fields("PktIss").Value, "yyyy/MM/dd"),
                                    Format(rsComSql.Fields("PktOrgCts").Value, "#0.000"),
                                    rsComSql.Fields("PktID").Value,
                                    rsComSql.Fields("Flo").Value,
                                    rsComSql.Fields("PktIDNew").Value,
                                    rsComSql.Fields("PktOrdNo").Value,
                                    rsComSql.Fields("PktRefNo").Value,
                                    rsComSql.Fields("StoneNo").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmbSize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSize.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtOrderNo.Focus()
        End If
    End Sub
End Class