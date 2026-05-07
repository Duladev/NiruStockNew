
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLParcel

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ClearFields()
        txtSupParNo.Text = ""
        txtAssortment.Text = ""
        txtPrice.Text = ""
        txtParcelNo.Text = ""
        txtGroup.Text = ""
        txtRevGrp.Text = ""
        txtImportNo.Text = ""
        txtIssPcs.Text = ""
        txtIssCts.Text = ""
        txtSize.Text = ""
        txtRghPcs.Text = ""
        txtRghCts.Text = ""
        cmbRateCode.Text = ""
        cmbFlow.Text = ""
        txtSize.Text = ""
        cmbIncentive.Text = ""
        cmbSegment.Text = ""
        cmbCategory.Text = ""
        cmbInstruct.Text = ""
        txtSpec.Text = ""
        chkIssue.Checked = False
        chkProd.Checked = False
        chkReIssue.Checked = False
        chkComplete.Checked = False
        cmbType.Text = ""
        dtpInvDate.Value = Date.Now
        dtpRecDate.Value = Date.Now
        txtEstSup.Text = ""
        txtEstDcl.Text = ""
        txtEstSar.Text = ""
        txtPlanValue.Text = "0"
        chkHide.Checked = False
        chkBruting.Checked = False
        dtpEstFinDate.Value = Date.Now
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
        cmbDepartment.Text = ""
        txtDclParNo.Text = ""
    End Sub

    Private Sub frm_DCLParcel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Department(cmbDepartment)
        Load_IncentiveCategory(cmbIncentive)
        Load_Segment(cmbSegment)
        Load_Category()
        Load_Types()
        Load_Instructions()
    End Sub

    Private Sub Load_Category()
        cmbCategory.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblCategory ORDER BY Cat", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbCategory.Items.Add(rsComSql.Fields("Cat").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Instructions()
        cmbInstruct.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblInstructions WHERE Dcl_depatment = '" & PBDepartment & "' ORDER BY Instruction", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbInstruct.Items.Add(rsComSql.Fields("Instruction").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Types()
        cmbType.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblRndTypes ORDER BY TP_name", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbType.Items.Add(rsComSql.Fields("TP_name").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub txtDclParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDclParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            GetParcelDetails()
        End If
    End Sub

    Private Sub GetParcelDetails()

        If cmbDepartment.Text = "" Then
            MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        txtDclParNo.Text = UCase(Trim(txtDclParNo.Text))

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblParcel WHERE Depart = '" & cmbDepartment.Text & "' AND GrpParNo = '" & txtDclParNo.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount > 0 Then
            txtSupParNo.Text = rsComSql.Fields("OrigParcelNo").Value
            txtAssortment.Text = rsComSql.Fields("Assortment").Value
            txtPrice.Text = rsComSql.Fields("ItemCost").Value
            txtParcelNo.Text = rsComSql.Fields("ParcelNo").Value
            txtGroup.Text = rsComSql.Fields("Grp").Value
            txtRevGrp.Text = rsComSql.Fields("RevGrp").Value

            txtImportNo.Text = rsComSql.Fields("ImpNo").Value
            dtpInvDate.Value = rsComSql.Fields("ImpDate").Value
            dtpRecDate.Value = IIf(Not IsDBNull(rsComSql.Fields("ImpRecDate").Value), rsComSql.Fields("ImpRecDate").Value, rsComSql.Fields("ImpDate").Value)

            txtIssPcs.Text = rsComSql.Fields("IssuedPcs").Value
            txtIssCts.Text = rsComSql.Fields("IssuedCts").Value

            txtRghPcs.Text = rsComSql.Fields("RghPcs").Value
            txtRghCts.Text = rsComSql.Fields("RghCts").Value

            cmbRateCode.Text = rsComSql.Fields("Charges").Value
            cmbFlow.Text = rsComSql.Fields("Flow").Value
            txtSize.Text = Math.Round(rsComSql.Fields("IssuedCts").Value / rsComSql.Fields("IssuedPcs").Value, 2)

            cmbIncentive.Text = rsComSql.Fields("IncUnit").Value

            cmbInstruct.Text = rsComSql.Fields("Instruction").Value
            cmbSegment.Text = rsComSql.Fields("Segment").Value
            cmbCategory.Text = rsComSql.Fields("Category").Value
            cmbType.Text = rsComSql.Fields("SubCategory").Value

            txtEstSup.Text = rsComSql.Fields("EstSup").Value
            txtEstDcl.Text = rsComSql.Fields("EstDCL").Value
            txtEstSar.Text = rsComSql.Fields("EstSar").Value

            chkIssue.Checked = rsComSql.Fields("IssueFinish").Value
            chkProd.Checked = rsComSql.Fields("ProdFinish").Value
            chkReIssue.Checked = rsComSql.Fields("ReIssue").Value
            chkComplete.Checked = rsComSql.Fields("Complete").Value
            chkHide.Checked = rsComSql.Fields("Hide").Value
            chkBruting.Checked = rsComSql.Fields("Bruting").Value

            txtSpec.Text = rsComSql.Fields("ParSpec").Value
            txtPlanValue.Text = rsComSql.Fields("PlanValue").Value

            If Not IsDBNull(rsComSql.Fields("EstFinDate").Value) Then
                dtpEstFinDate.Value = rsComSql.Fields("EstFinDate").Value
            End If
        Else
            MsgBox("New Parcel", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

            ClearFields()

            txtParcelNo.Text = Mid(txtDclParNo.Text, 1, 6)
            txtGroup.Text = strRight(txtDclParNo.Text, 1)
            txtRevGrp.Text = strRight(txtDclParNo.Text, 1)

            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblDep_Trf WHERE DCLParcelNo = '" & txtParcelNo.Text & "' AND Department = '" & cmbDepartment.Text & "'", AdoCN, 1, 1)
            If rsComSql_1.RecordCount > 0 Then
                txtSupParNo.Text = rsComSql_1.Fields("SupParcelNo").Value
                txtAssortment.Text = rsComSql_1.Fields("AssortmentNo").Value
                txtPrice.Text = rsComSql_1.Fields("ItemCost").Value
                txtImportNo.Text = rsComSql_1.Fields("DCLImportNo").Value
                dtpInvDate.Value = rsComSql_1.Fields("InvoiceDate").Value
                dtpRecDate.Value = rsComSql_1.Fields("RecievedDate").Value
            Else
                MsgBox("Unable to Find parcel information in the Transfers", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
            End If
            rsComSql_1 = Nothing
            
        End If
        rsComSql = Nothing

    End Sub

    Private Sub cmbDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDepartment.SelectedIndexChanged
        Load_CutChg(cmbRateCode, cmbDepartment.Text)
        Load_Flow()
    End Sub

    Private Sub Load_Flow()
        cmbFlow.Items.Clear()
        rsComSql = New ADODB.Recordset
        Select Case cmbDepartment.Text
            Case "Baguettes"
                rsComSql.Open("SELECT * FROM tblBAGFlow ORDER BY Flow", AdoCN, 1, 1)
            Case "Princess"
                rsComSql.Open("SELECT * FROM tblPRFlow ORDER BY Flow", AdoCN, 1, 1)
            Case "Rounds"
                rsComSql.Open("SELECT * FROM tblRndFlow ORDER BY Flow", AdoCN, 1, 1)
            Case "RoundsNLE", "Rounds3", "Rounds4", "Rounds5", "Emerald", "Lamour", "Opening", "Davinci", "Colombo Niru", "Baguettes2", "Baguettes3", "Emerald2", "Emerald3", "Carrer", "Asscher", "Radiant"
                rsComSql.Open("SELECT * FROM tblExtFlow WHERE Department = '" & cmbDepartment.Text & "' ORDER BY Flow", AdoCN, 1, 1)
            Case Else
                rsComSql.Open("SELECT * FROM tblRPrFlow WHERE Department = '" & cmbDepartment.Text & "' ORDER BY Flow", AdoCN, 1, 1)
        End Select
        If rsComSql.RecordCount > 0 Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbFlow.Items.Add(rsComSql.Fields("Flow").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub txtIssPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIssPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtIssPcs.Text <> "" Then
                If txtIssCts.Text <> "" Then
                    If CDbl(txtIssCts.Text) > 0 Then
                        txtSize.Text = Math.Round(CDbl(txtIssPcs.Text) / CDbl(txtIssCts.Text), 2)
                    Else
                        txtSize.Text = "0"
                    End If
                End If
            End If
            txtIssCts.Focus()
        End If
    End Sub

    Private Sub txtIssCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIssCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtIssCts.Text)
        If Asc(e.KeyChar) = 13 Then
            If txtIssCts.Text <> "" Then
                If txtIssPcs.Text <> "" Then
                    If CDbl(txtIssCts.Text) > 0 Then
                        txtSize.Text = Math.Round(CDbl(txtIssPcs.Text) / CDbl(txtIssCts.Text), 2)
                    Else
                        txtSize.Text = "0"
                    End If
                End If
            End If
            cmbRateCode.Focus()
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub Save()
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If ValidateFields() = False Then Exit Sub

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblParcel WHERE Depart = '" & cmbDepartment.Text & "' AND GrpParNo = '" & txtDclParNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblParcel(Depart,ImpNo,ImpDate,ImpRecDate,MainCategory,OrderNo,OrderRef,OrderSide,Assortment,ParcelNo,OrigParcelNo,Grp,GrpParNo,ActPcs,ACtCts," & _
                                "IssuedPcs,IssuedCts,ParSize,Category,SubCategory,IncUnit,Instruction,RejectPcs,RejectCts,RejectRate,ItemCost,Complete,IssueFinish," & _
                                "Charges,vCharges,Flow,ParCut,EstSup,EstDCL,Status,DoneBy,ModifyBy,RghPcs,RghCts,Approval,ReIssue,Segment,ProdFinish,ParSpec,PlanValue,Hide,EstSar,Bruting,RevGrp,EstFinDate) " & _
                              "VALUES('" & cmbDepartment.Text & "','" & txtImportNo.Text & "','" & Format(dtpInvDate.Value, "MM/dd/yyyy") & "','" & Format(dtpRecDate.Value, "MM/dd/yyyy") & "','" & cmbCategory.Text & "',1,1,'N','" & txtAssortment.Text & "'," & _
                                "'" & txtParcelNo.Text & "','" & txtSupParNo.Text & "','" & txtGroup.Text & "','" & txtDclParNo.Text & "'," & CDbl(txtIssPcs.Text) & "," & _
                                "" & CDbl(txtIssCts.Text) & "," & CDbl(txtIssPcs.Text) & "," & CDbl(txtIssCts.Text) & "," & CDbl(txtSize.Text) & ",'" & cmbCategory.Text & "','" & cmbType.Text & "','" & cmbIncentive.Text & "'," & _
                                "'" & cmbInstruct.Text & "',0,0,0," & CDbl(txtPrice.Text) & "," & IIf(chkComplete.Checked = True, 1, 0) & "," & IIf(chkIssue.Checked = True, 1, 0) & "," & _
                                "'" & cmbRateCode.Text & "',0,'" & cmbFlow.Text & "',0," & CDbl(txtEstSup.Text) & "," & CDbl(txtEstDcl.Text) & ",'A','" & PBUser_ID & "','" & PBUser_ID & "'," & _
                                "" & CDbl(txtRghPcs.Text) & "," & CDbl(txtRghCts.Text) & ",0," & IIf(chkReIssue.Checked = True, 1, 0) & ",'" & cmbSegment.Text & "'," & IIf(chkProd.Checked = True, 1, 0) & ",'" & txtSpec.Text & "'," & CDbl(txtPlanValue.Text) & "," & _
                                "" & IIf(chkHide.Checked = True, 1, 0) & "," & CDbl(txtEstSar.Text) & "," & IIf(chkBruting.Checked = True, 1, 0) & ",'" & UCase(txtRevGrp.Text) & "','" & Format(dtpEstFinDate.Value, "MM/dd/yyyy") & "')")

                MsgBox("Parcel Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
                txtDclParNo.Text = ""
            Else
                PBResponse = MsgBox("Already Exists. Do you want to Update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
                If PBResponse = MsgBoxResult.Yes Then

                    AdoCN.Execute("UPDATE tblParcel SET ActPcs = " & CDbl(txtIssPcs.Text) & ",ACtCts = " & CDbl(txtIssCts.Text) & ",IssuedPcs = " & CDbl(txtIssPcs.Text) & ",IssuedCts = " & CDbl(txtIssCts.Text) & "," & _
                                    "Category = '" & cmbCategory.Text & "',SubCategory = '" & cmbType.Text & "',IncUnit = '" & cmbIncentive.Text & "',Instruction = '" & cmbInstruct.Text & "'," & _
                                    "Complete = " & IIf(chkComplete.Checked = True, 1, 0) & ",IssueFinish = " & IIf(chkIssue.Checked = True, 1, 0) & ",Charges = '" & cmbRateCode.Text & "',Flow = '" & cmbFlow.Text & "'," & _
                                    "RghPcs = " & CDbl(txtRghPcs.Text) & ",RghCts = " & CDbl(txtRghCts.Text) & ",ReIssue = " & IIf(chkReIssue.Checked = True, 1, 0) & ",Segment = '" & cmbSegment.Text & "'," & _
                                    "ProdFinish = " & IIf(chkProd.Checked = True, 1, 0) & ",ParSize = " & CDbl(txtSize.Text) & ",ParSpec = '" & txtSpec.Text & "',EstSup = " & CDbl(txtEstSup.Text) & "," & _
                                    "EstDCL = " & CDbl(txtEstDcl.Text) & ", PlanValue = " & CDbl(txtPlanValue.Text) & ",Hide = " & IIf(chkHide.Checked = True, 1, 0) & ",EstSar = " & CDbl(txtEstSar.Text) & "," & _
                                    "Bruting = " & IIf(chkBruting.Checked = True, 1, 0) & ",RevGrp = '" & UCase(txtRevGrp.Text) & "',EstFinDate = '" & Format(dtpEstFinDate.Value, "MM/dd/yyyy") & "' " & _
                                  "WHERE Depart = '" & cmbDepartment.Text & "' AND GrpParNo = '" & txtDclParNo.Text & "'")


                    MsgBox("Parcel Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    ClearFields()
                    txtDclParNo.Text = ""
                End If
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Function ValidateFields() As Boolean
        ValidateFields = True

        If Not Len(Trim(txtImportNo.Text)) > 0 Then
            MsgBox("Please enter New", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(cmbDepartment.Text)) > 0 Then
            MsgBox("Please enter the Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(cmbCategory.Text)) > 0 Then
            MsgBox("Please enter the Category", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(txtDclParNo.Text)) > 0 Then
            MsgBox("Please enter the Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(txtRevGrp.Text)) > 0 Then
            MsgBox("Please enter the Group", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(txtIssPcs.Text)) > 0 Then
            MsgBox("Please enter the Issue Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(txtIssCts.Text)) > 0 Then
            MsgBox("Please enter the Issue Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(cmbRateCode.Text)) > 0 Then
            MsgBox("Please enter the Rate Code", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(cmbFlow.Text)) > 0 Then
            MsgBox("Please enter the Flow", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(cmbInstruct.Text)) > 0 Then
            MsgBox("Please enter the Instruction", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(txtEstSup.Text)) > 0 Then
            MsgBox("Please enter the Supplier Estimate Yield", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(txtEstDcl.Text)) > 0 Then
            MsgBox("Please enter the DCL Estimate Yield", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(txtEstSar.Text)) > 0 Then
            MsgBox("Please enter the Sarine Estimate Yield", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        If Not Len(Trim(txtPlanValue.Text)) > 0 Then
            MsgBox("Please enter the Plan Value", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ValidateFields = False
            Exit Function
        End If

        Return ValidateFields
    End Function

    Private Sub txtIssPcs_TextChanged(sender As Object, e As EventArgs) Handles txtIssPcs.TextChanged
        txtRghPcs.Text = txtIssPcs.Text
    End Sub

    Private Sub txtIssCts_TextChanged(sender As Object, e As EventArgs) Handles txtIssCts.TextChanged
        txtRghCts.Text = txtIssCts.Text
    End Sub

    Private Sub txtEstSup_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEstSup.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtEstSup.Text)
    End Sub

    Private Sub txtEstDcl_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEstDcl.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtEstDcl.Text)
    End Sub

    Private Sub txtPlanValue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPlanValue.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtPlanValue.Text)
    End Sub

    Private Sub txtEstSar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEstSar.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtEstDcl.Text)
    End Sub
End Class