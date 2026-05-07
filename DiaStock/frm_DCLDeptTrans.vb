
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLDeptTrans
    Dim blnPCU As Boolean
    Dim intOriginal As Integer

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub txtSupParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSupParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            GetParcelDetails()
        End If
    End Sub

    Private Sub GetParcelDetails()
        Dim dblRecPcs As Double
        Dim dblRecCts As Double

        ClearFields()
        blnPCU = False
        intOriginal = 0
        txtSupParNo.Text = UCase(txtSupParNo.Text)
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Department, AssortmentNo, CompanyRefNo, ParcelType, SupParcelNo, DCLParcelNo, ItemCost, InvoiceDate, LotNo, Original, ConRefNo, SUM(ACTPcs) AS ActPcs, ROUND(SUM(ACtCts), 3) AS ActCts " & _
                      "FROM tblImport " & _
                      "WHERE (SupParcelNo = '" & txtSupParNo.Text & "') AND (SupplierCode <> 22) AND (SupplierCode <> 30) " & _
                      "GROUP BY Department, AssortmentNo, CompanyRefNo, ParcelType, SupParcelNo, DCLParcelNo, ItemCost, InvoiceDate, LotNo, Original, ConRefNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            txtAssortment.Text = rsComSql.Fields("AssortmentNo").Value
            txtDclParNo.Text = rsComSql.Fields("DCLParcelNo").Value
            txtImportNo.Text = rsComSql.Fields("CompanyRefNo").Value
            txtType.Text = rsComSql.Fields("ParcelType").Value
            txtImpPcs.Text = rsComSql.Fields("ACTPcs").Value
            txtImpCts.Text = rsComSql.Fields("ActCts").Value
            txtPrice.Text = rsComSql.Fields("ItemCost").Value
            txtLotNo.Text = rsComSql.Fields("LotNo").Value

            If rsComSql.Fields("Original").Value = 1 Then
                If Len(rsComSql.Fields("ConRefNo").Value) > 0 Then
                    intOriginal = 0
                Else
                    intOriginal = 1
                End If
            Else
                intOriginal = 0
            End If
        Else
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT OrgAssort, Assortment, ParNo, SUM(InPcs) AS InPcs, ROUND(SUM(InCts), 3) AS InCts, Price " & _
                          "FROM tblPCUStockIn WHERE ParNo = '" & txtSupParNo.Text & "' GROUP BY OrgAssort, Assortment, ParNo, Price", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                If Not IsDBNull(rsComSql_1.Fields("InPcs").Value) Then
                    blnPCU = True
                    txtAssortment.Text = rsComSql_1.Fields("Assortment").Value
                    txtType.Text = "Polished"
                    txtImpPcs.Text = rsComSql_1.Fields("InPcs").Value
                    txtImpCts.Text = rsComSql_1.Fields("InCts").Value
                    txtPrice.Text = rsComSql_1.Fields("Price").Value
                Else
                    MsgBox("There is no such a Parcel No. in the Database", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                End If
            Else
                MsgBox("There is no such a Parcel No. in the Database", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
            End If
            rsComSql_1 = Nothing
        End If
        rsComSql = Nothing

        If blnPCU = False Then
            dblRecPcs = 0
            dblRecCts = 0
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT SUM(NewACTCts) AS Cts,SUM(NewACTPcs) AS Pcs FROM dbo.tblDep_Trf " & _
                            "WHERE SupParcelNo = '" & txtSupParNo.Text & "'", AdoCN, 1, 1)
            If IsDBNull(rsComSql_1.Fields("Cts").Value) Then
                txtBalPcs.Text = txtImpPcs.Text
                txtBalCts.Text = txtImpCts.Text
            Else
                txtBalPcs.Text = 0
                txtBalPcs.Text = 0

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(Pcs) AS Pcs, SUM(Cts) AS Cts FROM tblDep_Rec " & _
                              "WHERE SupParcelNo = '" & txtSupParNo.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                        dblRecPcs = rsComSql.Fields("Pcs").Value
                        dblRecCts = rsComSql.Fields("Cts").Value
                        dblRecCts = Math.Round(dblRecCts, 3)
                    End If
                End If
                rsComSql = Nothing

                txtBalPcs.Text = CSng(txtImpPcs.Text) - CSng(rsComSql_1.Fields("Pcs").Value) + dblRecPcs
                txtBalCts.Text = Math.Round(CSng(txtImpCts.Text) - CSng(rsComSql_1.Fields("Cts").Value) + dblRecCts, 3)
            End If
            rsComSql_1 = Nothing
        Else
            txtBalPcs.Text = CSng(txtImpPcs.Text)
            txtBalCts.Text = Math.Round(CSng(txtImpCts.Text), 3)

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktCts), 3) AS PktCts " & _
                          "FROM tblPacket " & _
                          "WHERE (AParNo = '" & txtSupParNo.Text & "')", AdoCN, 1, 1)
            If Not IsDBNull(rsComSql.Fields("PktPcs").Value) Then
                txtBalPcs.Text = CSng(txtBalPcs.Text) - rsComSql.Fields("PktPcs").Value
                txtBalCts.Text = Math.Round(CSng(txtBalCts.Text) - rsComSql.Fields("PktCts").Value, 3)
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(NewACTCts) AS Cts,SUM(NewACTPcs) AS Pcs FROM dbo.tblDep_Trf " & _
                          "WHERE SupParcelNo = '" & txtSupParNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                    txtBalPcs.Text = CSng(txtBalPcs.Text) - rsComSql.Fields("Pcs").Value
                    txtBalCts.Text = Math.Round(CSng(txtBalCts.Text) - rsComSql.Fields("Cts").Value, 3)
                End If
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(Pcs) AS Pcs, SUM(Cts) AS Cts FROM tblDep_Rec " & _
                          "WHERE SupParcelNo = '" & txtSupParNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                    dblRecPcs = rsComSql.Fields("Pcs").Value
                    dblRecCts = rsComSql.Fields("Cts").Value
                    dblRecCts = Math.Round(dblRecCts, 3)
                End If
            End If
            rsComSql = Nothing

            txtBalPcs.Text = CSng(txtBalPcs.Text) + dblRecPcs
            txtBalCts.Text = Math.Round(CSng(txtBalCts.Text) + dblRecCts, 3)

        End If

        rsComSql_1 = New ADODB.Recordset
        mStrSQL = "SELECT Department,DCLParcelNo,NewACTPcs,NewACTCts,AParNo FROM dbo.tblDep_Trf " & _
                  "WHERE SupParcelNo = '" & txtSupParNo.Text & "' " & _
                  "ORDER BY Department, DCLParcelNo"
        rsComSql_1.Open(mStrSQL, AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                dblRecPcs = 0
                dblRecCts = 0
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT SUM(Pcs) AS Pcs, SUM(Cts) AS Cts FROM tblDep_Rec " & _
                              "WHERE Department = '" & rsComSql_1.Fields("Department").Value & "' AND " & _
                                "SupParcelNo = '" & txtSupParNo.Text & "' AND DCLParcelNo = '" & rsComSql_1.Fields("DCLParcelNo").Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                        dblRecPcs = rsComSql.Fields("Pcs").Value
                        dblRecCts = rsComSql.Fields("Cts").Value
                        dblRecCts = Math.Round(dblRecCts, 3)
                    End If
                End If
                rsComSql = Nothing

                flxDept.Rows.Add(rsComSql_1.Fields("Department").Value,
                                 rsComSql_1.Fields("DCLParcelNo").Value,
                                 rsComSql_1.Fields("NewACTPcs").Value,
                                 rsComSql_1.Fields("NewACTCts").Value,
                                 dblRecPcs,
                                 dblRecCts)

                rsComSql_1.MoveNext()
            End While
        End If
        rsComSql_1 = Nothing
    End Sub

    Private Sub ClearFields()
        txtAssortment.Text = ""
        txtDclParNo.Text = ""
        txtImportNo.Text = ""
        txtType.Text = ""
        txtImpPcs.Text = ""
        txtImpCts.Text = ""
        txtPrice.Text = ""
        txtLotNo.Text = ""
        txtBalPcs.Text = ""
        txtBalCts.Text = ""
        cmbDepartment.Text = ""
        flxDept.Rows.Clear()
        txtNewDclNo.Text = ""
        txtNewPcs.Text = ""
        txtNewCts.Text = ""
        txtRecDept.Text = ""
        txtRecParNo.Text = ""
        txtRecPcs.Text = ""
        txtRecCts.Text = ""
        txtRghCts.Text = ""
        flxTransfers.Rows.Clear()
    End Sub

    Private Sub frm_DCLDeptTrans_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Department(cmbDepartment)
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        AddData()
    End Sub

    Private Sub AddData()
        Dim intRow As Integer

        If cmbDepartment.Text = "" Then
            MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If txtNewDclNo.Text = "" Then
            MsgBox("Invalid Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If txtNewPcs.Text = "" Then
            MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        If txtNewCts.Text = "" Then
            MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If CSng(txtNewPcs.Text) > CSng(txtBalPcs.Text) Then
            MsgBox("Invalid Balance Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            txtNewPcs.Text = ""
            txtNewPcs.Focus()
            Exit Sub
        End If

        If CSng(txtNewCts.Text) > CSng(txtBalCts.Text) Then
            MsgBox("Invalid Balance Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            txtNewCts.Text = ""
            txtNewCts.Focus()
            Exit Sub
        End If

        For intRow = 0 To flxTransfers.Rows.Count - 1
            If flxTransfers.Item(1, intRow).Value = Trim(txtNewDclNo.Text) Then
                MsgBox("DCL Parcel No. already selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        If cmbDepartment.Text = "Precision" Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblImport WHERE SupParcelNo = '" & txtSupParNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If UCase(PBUser_ID) <> "MANJULA" Then
                    If rsComSql.Fields("CompCode").Value <> "DCL" Then
                        MsgBox("Invalid Company - " & rsComSql.Fields("CompCode").Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    'If rsComSql.Fields("Category").Value <> "NFE" Then
                    '    MsgBox("Invalid Category - " & rsComSql.Fields("Category").Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    '    Exit Sub
                    'End If
                End If
            Else
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT OrgAssort, Assortment, ParNo, SUM(InPcs) AS InPcs, ROUND(SUM(InCts), 3) AS InCts, Price " & _
                              "FROM tblPCUStockIn WHERE ParNo = '" & txtSupParNo.Text & "' GROUP BY OrgAssort, Assortment, ParNo, Price", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("InPcs").Value) Then

                    Else
                        MsgBox("There is no such a Parcel No. in the Database", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                    End If
                Else
                    MsgBox("There is no such a Parcel No. in the Database", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
                End If
                rsComSql_1 = Nothing
            End If
            rsComSql = Nothing
        End If

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblParcel WHERE Depart = '" & cmbDepartment.Text & "' AND ParcelNo = '" & txtNewDclNo.Text & "' AND Complete = 1 AND Grp <> 'N'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            MsgBox("Parcel Completed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        If Mid(cmbDepartment.Text, 1, 5) = "Rough" Then
            If Len(txtNewDclNo.Text) <> 6 Then
                MsgBox("Invalid DCL Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        End If

        If Mid(txtSupParNo.Text, 1, 1) <> Mid(txtNewDclNo.Text, 1, 1) Then
            MsgBox("Invalid DCL Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If strRight(txtSupParNo.Text, 4) <> strRight(txtNewDclNo.Text, 4) Then
            MsgBox("Invalid DCL Parcel No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblDep_Trf WHERE SupParcelNo <> '" & txtSupParNo.Text & "' AND DCLParcelNo = '" & txtNewDclNo.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            flxTransfers.Rows.Add(cmbDepartment.Text,
                                  UCase(Trim(txtNewDclNo.Text)),
                                  txtNewPcs.Text,
                                  txtNewCts.Text)

            cmbDepartment.Text = ""
            txtNewDclNo.Text = ""
            txtNewPcs.Text = ""
            txtNewCts.Text = ""
        Else
            MsgBox("DCL Parcel No. already used for " & rsComSql.Fields("SupParcelNo").Value, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdRemove_Click(sender As Object, e As EventArgs) Handles cmdRemove.Click
        If flxTransfers.Rows.Count > 0 Then
            PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                flxTransfers.Rows.RemoveAt(flxTransfers.Rows.Count - 1)
            End If
        End If
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub txtNewDclNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNewDclNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtDclParNo.Text <> "" Then
                txtNewPcs.Focus()
            End If
        End If
    End Sub

    Private Sub txtNewPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNewPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtNewPcs.Text <> "" Then
                txtNewCts.Focus()
            End If
        End If
    End Sub

    Private Sub txtNewCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNewCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtNewCts.Text)
        If Asc(e.KeyChar) = 13 Then
            If txtNewCts.Text <> "" Then
                cmdAdd.Focus()
            End If
        End If
    End Sub

    Private Sub flxDept_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDept.CellClick
        txtRecDept.Text = flxDept.Item(0, flxDept.CurrentRow.Index).Value
        txtRecParNo.Text = flxDept.Item(1, flxDept.CurrentRow.Index).Value
        txtRecPcs.Text = flxDept.Item(2, flxDept.CurrentRow.Index).Value
        txtRecCts.Text = Math.Round(CDbl(flxDept.Item(3, flxDept.CurrentRow.Index).Value), 3)
        txtRghCts.Text = Math.Round(CDbl(flxDept.Item(3, flxDept.CurrentRow.Index).Value), 3)

        If txtRecDept.Text = "RoughWO" Or txtRecDept.Text = "RoughWO2" Or txtRecDept.Text = "RoughPlan" Or txtRecDept.Text = "RoughPlan2" Or _
            txtRecDept.Text = "RoughPlanAS" Or txtRecDept.Text = "RoughPlanAS2" Or txtRecDept.Text = "RoughPlanAS3" Or txtRecDept.Text = "RoughPlanAS4" Or txtRecDept.Text = "RoughPlanAS5" Or txtRecDept.Text = "RoughPlanAS6" Or _
            txtRecDept.Text = "RoughTS" Or txtRecDept.Text = "RoughTS2" Or txtRecDept.Text = "RoughTS3" Or txtRecDept.Text = "RoughTS4" Or txtRecDept.Text = "RoughTS5" Or txtRecDept.Text = "RoughTS6" Or _
            txtRecDept.Text = "RoughOpr" Or txtRecDept.Text = "RoughOpr2" Or txtRecDept.Text = "RoughOpr3" Or txtRecDept.Text = "RoughOpr4" Or txtRecDept.Text = "RoughOpr5" Or txtRecDept.Text = "RoughOpr6" Or _
            txtRecDept.Text = "RoughBruting" Or txtRecDept.Text = "RoughSMarking" Or _
            txtRecDept.Text = "RoughASSorting" Then

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(RetPcsT + RetPcsB + ExtPcs) AS RetPcs, SUM(RetCts) AS RetCts " & _
                          "FROM dbo.tblRPrReturns " & _
                          "WHERE (Sec = 20) AND (Department = '" & txtRecDept.Text & "') AND (LEFT(ParNo, 6) = '" & txtRecParNo.Text & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("RetPcs").Value) Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT SUM(Pcs) AS Pcs, SUM(Cts) AS Cts FROM tblDep_Rec " & _
                                    "WHERE Department = '" & txtRecDept.Text & "' AND SupParcelNo = '" & txtSupParNo.Text & "' AND DCLParcelNo = '" & txtRecParNo.Text & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                            txtRecPcs.Text = rsComSql.Fields("RetPcs").Value - rsComSql_1.Fields("Pcs").Value
                            txtRecCts.Text = Math.Round(rsComSql.Fields("RetCts").Value - rsComSql_1.Fields("Cts").Value, 3)
                            txtRghCts.Text = Math.Round(rsComSql.Fields("RetCts").Value - rsComSql_1.Fields("Cts").Value, 3)
                        Else
                            txtRecPcs.Text = rsComSql.Fields("RetPcs").Value
                            txtRecCts.Text = Format(rsComSql.Fields("RetCts").Value, "#0.000")
                            txtRghCts.Text = Format(rsComSql.Fields("RetCts").Value, "#0.000")
                        End If
                    End If
                    rsComSql_1 = Nothing
                Else
                    txtRecPcs.Text = "0"
                    txtRecCts.Text = "0"
                    txtRghCts.Text = "0"
                End If
            End If
            rsComSql = Nothing

        ElseIf txtRecDept.Text = "RoughSawing" Or txtRecDept.Text = "RoughBoil" Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(ActPcs) AS RetPcs, SUM(RetCts) AS RetCts " & _
                          "FROM dbo.tblRPrReturns " & _
                          "WHERE (Sec = 20) AND (Department = '" & txtRecDept.Text & "') AND (LEFT(ParNo, 6) = '" & txtRecParNo.Text & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("RetPcs").Value) Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT SUM(Pcs) AS Pcs, SUM(Cts) AS Cts FROM tblDep_Rec " & _
                                    "WHERE Department = '" & txtRecDept.Text & "' AND SupParcelNo = '" & txtSupParNo.Text & "' AND DCLParcelNo = '" & txtRecParNo.Text & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                            txtRecPcs.Text = rsComSql.Fields("RetPcs").Value - rsComSql_1.Fields("Pcs").Value
                            txtRecCts.Text = Math.Round(rsComSql.Fields("RetCts").Value - rsComSql_1.Fields("Cts").Value, 3)
                            txtRghCts.Text = Math.Round(rsComSql.Fields("RetCts").Value - rsComSql_1.Fields("Cts").Value, 3)
                        Else
                            txtRecPcs.Text = rsComSql.Fields("RetPcs").Value
                            txtRecCts.Text = Format(rsComSql.Fields("RetCts").Value, "#0.000")
                            txtRghCts.Text = Format(rsComSql.Fields("RetCts").Value, "#0.000")
                        End If
                    End If
                    rsComSql_1 = Nothing
                Else
                    txtRecPcs.Text = "0"
                    txtRecCts.Text = "0"
                    txtRghCts.Text = "0"
                End If
            End If
            rsComSql = Nothing

        ElseIf txtRecDept.Text = "RoughSawing2" Or txtRecDept.Text = "RoughSawing3" Or txtRecDept.Text = "RoughSawing4" Or txtRecDept.Text = "RoughSawing5" Or txtRecDept.Text = "RoughSawing6" Or txtRecDept.Text = "RoughSawingS" Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(RetPcsT + RetPcsB + ActPcs) AS RetPcs, SUM(RetCts) AS RetCts " & _
                          "FROM dbo.tblRPrReturns " & _
                          "WHERE (Sec = 20) AND (Department = '" & txtRecDept.Text & "') AND (LEFT(ParNo, 6) = '" & txtRecParNo.Text & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("RetPcs").Value) Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT SUM(Pcs) AS Pcs, SUM(Cts) AS Cts FROM tblDep_Rec " & _
                                    "WHERE Department = '" & txtRecDept.Text & "' AND SupParcelNo = '" & txtSupParNo.Text & "' AND DCLParcelNo = '" & txtRecParNo.Text & "'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                            txtRecPcs.Text = rsComSql.Fields("RetPcs").Value - rsComSql_1.Fields("Pcs").Value
                            txtRecCts.Text = Math.Round(rsComSql.Fields("RetCts").Value - rsComSql_1.Fields("Cts").Value, 3)
                            txtRghCts.Text = Math.Round(rsComSql.Fields("RetCts").Value - rsComSql_1.Fields("Cts").Value, 3)
                        Else
                            txtRecPcs.Text = rsComSql.Fields("RetPcs").Value
                            txtRecCts.Text = Format(rsComSql.Fields("RetCts").Value, "#0.000")
                            txtRghCts.Text = Format(rsComSql.Fields("RetCts").Value, "#0.000")
                        End If
                    End If
                    rsComSql_1 = Nothing
                Else
                    txtRecPcs.Text = "0"
                    txtRecCts.Text = "0"
                    txtRghCts.Text = "0"
                End If
            End If
            rsComSql = Nothing

        ElseIf txtRecDept.Text = "Rough Planning" Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(RetPcs) AS RetPcs, SUM(RetCts) AS RetCts " & _
                          "FROM dbo.tblRghReturns " & _
                          "WHERE (Sec = 6) AND (LEFT(ParNo, 6) = '" & txtRecParNo.Text & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("RetPcs").Value) Then
                    txtRecPcs.Text = rsComSql.Fields("RetPcs").Value
                    txtRecCts.Text = Math.Round(rsComSql.Fields("RetCts").Value, 3)
                    txtRghCts.Text = Math.Round(rsComSql.Fields("RetCts").Value, 3)
                Else
                    txtRecPcs.Text = "0"
                    txtRecCts.Text = "0"
                    txtRghCts.Text = "0"
                End If
            End If
            rsComSql = Nothing
        End If

        txtRecPcs.Focus()
    End Sub

    Private Sub cmdReceive_Click(sender As Object, e As EventArgs) Handles cmdReceive.Click
        ReceiveData()
    End Sub

    Private Sub ReceiveData()
        Dim dblIssPcs As Double
        Dim dblIssCts As Double
        Dim dblRecPcs As Double
        Dim dblRecCts As Double

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblParcel WHERE Depart = '" & txtRecDept.Text & "' AND ParcelNo = '" & txtRecParNo.Text & "' AND Complete = 1", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                MsgBox("Parcel Completed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            dblIssPcs = 0
            dblIssCts = 0
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(NewACTPcs) AS Pcs, SUM(NewACTCts) AS Cts FROM tblDep_Trf WHERE Department = '" & txtRecDept.Text & "' AND SupParcelNo = '" & txtSupParNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                    dblIssPcs = rsComSql.Fields("Pcs").Value
                    dblIssCts = rsComSql.Fields("Cts").Value
                    dblIssCts = Math.Round(dblIssCts, 3)
                End If
            End If
            rsComSql = Nothing

            dblRecPcs = 0
            dblRecCts = 0
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT SUM(Pcs) AS Pcs, SUM(Cts) AS Cts FROM tblDep_Rec WHERE Department = '" & txtRecDept.Text & "' AND SupParcelNo = '" & txtSupParNo.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("Pcs").Value) Then
                    dblRecPcs = rsComSql.Fields("Pcs").Value
                    dblRecCts = rsComSql.Fields("Cts").Value
                    dblRecCts = Math.Round(dblRecCts, 3)
                End If
            End If
            rsComSql = Nothing

            If txtRecDept.Text <> "Sawing" And txtRecDept.Text <> "Rough Dept" And txtRecDept.Text <> "Brookay" And txtRecDept.Text <> "Rounds5" And Mid(txtRecDept.Text, 1, 11) <> "RoughSawing" Then
                If dblIssPcs < dblRecPcs + CDbl(txtRecPcs.Text) Then
                    MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If dblIssCts < dblRecCts + CDbl(txtRecCts.Text) Then
                    MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Else
                If dblIssCts < dblRecCts + CDbl(txtRecCts.Text) Then
                    MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If

            AdoCN.Execute("INSERT INTO tblDep_Rec(Department,AssortmentNo,SupParcelNo,DCLParcelNo,Pcs,Cts,RghCts) " & _
                          "VALUES('" & txtRecDept.Text & "','" & txtAssortment.Text & "','" & txtSupParNo.Text & "'," & _
                            "'" & txtRecParNo.Text & "'," & CDbl(txtRecPcs.Text) & "," & CDbl(txtRecCts.Text) & "," & CDbl(txtRghCts.Text) & ")")

            MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

            ClearFields()
        End If

    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim Rs1 As ADODB.Recordset
        Dim rs2 As ADODB.Recordset
        Dim DataEnter As String

        Dim strPktNo As String
        Dim strOrgAssort As String
        Dim strGrpParNo As String
        Dim strGrp As String
        Dim strFlow As String
        Dim intReIssue As Integer

        Dim intAMS As Integer
        Dim intYahuda As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then

            DataEnter = "suser_sname()"

            intAMS = 0
            intYahuda = 0

            Rs1 = New ADODB.Recordset
            Rs1.Open("SELECT Department,SystemRefNo,SupplierRefNo,CompanyRefNo,BOINo,InvoiceDate,RecievedDate," & _
                            "SupplierCode,ParcelType,AssortmentNo,SupParcelNo,DCLParcelNo,INVPcs,INVCts,ACTPcs,ACtCts," & _
                            "ItemSize,Charges,ItemCost,RemPcs,RemCts,Status,DoneBy,ConRefNo " & _
                     "FROM dbo.tblImport " & _
                     "WHERE (SupParcelNo = '" & txtSupParNo.Text & "') AND (Department = 'Rough Dept') AND (DCLParcelNo = '" & txtDclParNo.Text & "')", AdoCN, 1, 1)
            If Rs1.RecordCount Then
                For intRow = 0 To flxTransfers.Rows.Count - 1
                    rs2 = New ADODB.Recordset
                    rs2.Open("SELECT * FROM tblDep_Trf WHERE Department = '" & flxTransfers.Item(0, intRow).Value & "' AND SupParcelNo = '" & txtSupParNo.Text & "' AND DCLParcelNo = '" & flxTransfers.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                    If rs2.RecordCount = 0 Then
                        AdoCN.Execute("INSERT INTO tblDep_Trf(Department,DCLImportNo,SupplierRefNo,CompanyRefNo,BOINo,InvoiceDate,RecievedDate,SupplierCode,ParcelType,AssortmentNo,SupParcelNo,DCLParcelNo,INVPcs,INVCts,ACTPcs,ACtCts,NewACTPcs,NewACTCts,ItemSize,Charges,ItemCost,RemPcs,RemCts,Status,DoneBy,ModifyBy,AParNo) " & _
                                      "VALUES('" & flxTransfers.Item(0, intRow).Value & "','" & Rs1.Fields("SystemRefNo").Value & "','" & Rs1.Fields("SupplierRefNo").Value & "','" & Rs1.Fields("CompanyRefNo").Value & "','" & Rs1.Fields("BOINo").Value & "','" & Format(Rs1.Fields("InvoiceDate").Value, "MM/dd/yyyy") & "','" & Format(Rs1.Fields("RecievedDate").Value, "MM/dd/yyyy") & "','" & Rs1.Fields("SupplierCode").Value & "','" & Rs1.Fields("ParcelType").Value & "'," & _
                                        "'" & Rs1.Fields("AssortmentNo").Value & "','" & txtSupParNo.Text & "','" & flxTransfers.Item(1, intRow).Value & "'," & flxTransfers.Item(2, intRow).Value & "," & flxTransfers.Item(3, intRow).Value & "," & flxTransfers.Item(2, intRow).Value & "," & flxTransfers.Item(3, intRow).Value & "," & flxTransfers.Item(2, intRow).Value & "," & _
                                        "" & flxTransfers.Item(3, intRow).Value & "," & Rs1.Fields("ItemSize").Value & "," & IIf(IsDBNull(Rs1.Fields("Charges").Value), 0, Rs1.Fields("Charges").Value) & "," & Rs1.Fields("ItemCost").Value & "," & Rs1.Fields("RemPcs").Value & "," & Rs1.Fields("RemCts").Value & ",'I','" & PBUser_ID & "'," & DataEnter & ",'')")

                        '***************************
                        If flxTransfers.Item(0, intRow).Value = "RoughWO" Or flxTransfers.Item(0, intRow).Value = "RoughWO2" Or flxTransfers.Item(0, intRow).Value = "RoughPlan" Or flxTransfers.Item(0, intRow).Value = "RoughPlan2" Or _
                           flxTransfers.Item(0, intRow).Value = "RoughPlanAS" Or flxTransfers.Item(0, intRow).Value = "RoughPlanAS2" Or flxTransfers.Item(0, intRow).Value = "RoughPlanAS3" Or flxTransfers.Item(0, intRow).Value = "RoughPlanAS4" Or _
                           flxTransfers.Item(0, intRow).Value = "RoughPlanAS5" Or flxTransfers.Item(0, intRow).Value = "RoughPlanAS6" Or flxTransfers.Item(0, intRow).Value = "RoughTS" Or flxTransfers.Item(0, intRow).Value = "RoughTS2" Or flxTransfers.Item(0, intRow).Value = "RoughTS3" Or flxTransfers.Item(0, intRow).Value = "RoughTS4" Or flxTransfers.Item(0, intRow).Value = "RoughTS5" Or flxTransfers.Item(0, intRow).Value = "RoughTS5" Or flxTransfers.Item(0, intRow).Value = "RoughOpr" Or _
                           flxTransfers.Item(0, intRow).Value = "RoughOpr2" Or flxTransfers.Item(0, intRow).Value = "RoughOpr3" Or flxTransfers.Item(0, intRow).Value = "RoughOpr4" Or flxTransfers.Item(0, intRow).Value = "RoughOpr5" Or flxTransfers.Item(0, intRow).Value = "RoughOpr6" Or flxTransfers.Item(0, intRow).Value = "Sawing" Or _
                           flxTransfers.Item(0, intRow).Value = "RoughSMarking" Or flxTransfers.Item(0, intRow).Value = "RoughSawing" Or flxTransfers.Item(0, intRow).Value = "RoughSawing2" Or flxTransfers.Item(0, intRow).Value = "RoughSawing3" Or flxTransfers.Item(0, intRow).Value = "RoughSawing4" Or flxTransfers.Item(0, intRow).Value = "RoughSawing5" Or flxTransfers.Item(0, intRow).Value = "RoughSawing6" Or flxTransfers.Item(0, intRow).Value = "RoughSawingS" Or _
                           flxTransfers.Item(0, intRow).Value = "RoughASSorting" Or flxTransfers.Item(0, intRow).Value = "RoughBoil" Then

                            strGrp = "A"
                            strGrpParNo = flxTransfers.Item(1, intRow).Value & strGrp
                            intReIssue = 0

                            strFlow = ""
                            rsComSql_2 = New ADODB.Recordset
                            rsComSql_2.Open("SELECT * FROM tblRPrFlow WHERE Department = '" & flxTransfers.Item(0, intRow).Value & "'", AdoCN, 1, 1)
                            If rsComSql_2.RecordCount Then
                                strFlow = rsComSql_2.Fields("Flow").Value
                            End If
                            rsComSql_2 = Nothing

                            rsComSql_2 = New ADODB.Recordset
                            rsComSql_2.Open("SELECT * FROM tblParcel WHERE GrpParNo  = '" & strGrpParNo & "' AND Depart = '" & flxTransfers.Item(0, intRow).Value & "'", AdoCN, 1, 3)
                            If rsComSql_2.RecordCount = 0 Then
                                rsComSql_2.AddNew()
                            End If

                            rsComSql_2.Fields("Depart").Value = flxTransfers.Item(0, intRow).Value
                            rsComSql_2.Fields("ImpNo").Value = txtImportNo.Text
                            rsComSql_2.Fields("ImpDate").Value = Rs1.Fields("InvoiceDate").Value
                            rsComSql_2.Fields("MainCategory").Value = "DCL"
                            rsComSql_2.Fields("OrderRef").Value = 1
                            rsComSql_2.Fields("Assortment").Value = txtAssortment.Text
                            rsComSql_2.Fields("ParcelNo").Value = flxTransfers.Item(1, intRow).Value
                            rsComSql_2.Fields("OrigParcelNo").Value = txtSupParNo.Text
                            rsComSql_2.Fields("Grp").Value = strGrp
                            rsComSql_2.Fields("RevGrp").Value = strGrp
                            rsComSql_2.Fields("GrpParNo").Value = strGrpParNo
                            rsComSql_2.Fields("ActPcs").Value = flxTransfers.Item(2, intRow).Value
                            rsComSql_2.Fields("ACtCts").Value = flxTransfers.Item(3, intRow).Value
                            rsComSql_2.Fields("IssuedPcs").Value = flxTransfers.Item(2, intRow).Value
                            rsComSql_2.Fields("IssuedCts").Value = flxTransfers.Item(3, intRow).Value
                            rsComSql_2.Fields("Category").Value = "MB"
                            rsComSql_2.Fields("SubCategory").Value = "Rounds Sub"
                            rsComSql_2.Fields("IncUnit").Value = "A"
                            rsComSql_2.Fields("Instruction").Value = ""
                            rsComSql_2.Fields("RejectPcs").Value = 0
                            rsComSql_2.Fields("RejectCts").Value = 0
                            rsComSql_2.Fields("RejectRate").Value = 0
                            rsComSql_2.Fields("ItemCost").Value = txtPrice.Text
                            rsComSql_2.Fields("Complete").Value = 0
                            rsComSql_2.Fields("IssueFinish").Value = 0
                            rsComSql_2.Fields("Charges").Value = 0
                            rsComSql_2.Fields("vCharges").Value = 0
                            rsComSql_2.Fields("Flow").Value = strFlow
                            rsComSql_2.Fields("ParCut").Value = 0
                            rsComSql_2.Fields("EstSup").Value = 0
                            rsComSql_2.Fields("EstDCL").Value = 0
                            rsComSql_2.Fields("Status").Value = "I"
                            rsComSql_2.Fields("DoneBy").Value = PBUser_ID
                            rsComSql_2.Fields("ModifyBy").Value = PBUser_ID
                            rsComSql_2.Fields("RghPcs").Value = flxTransfers.Item(2, intRow).Value
                            rsComSql_2.Fields("RghCts").Value = flxTransfers.Item(3, intRow).Value
                            rsComSql_2.Fields("Approval").Value = 0
                            rsComSql_2.Fields("ReIssue").Value = intReIssue
                            rsComSql_2.Fields("Segment").Value = ""
                            rsComSql_2.Update()

                            rsComSql_2 = Nothing
                        End If
                        '***************************

                        If flxTransfers.Item(0, intRow).Value = "Grading" Or flxTransfers.Item(0, intRow).Value = "Grading Rounds" Then
                            Call Dep_Grading_Trf("Direct Import", 9999, flxTransfers.Item(1, intRow).Value, "001", flxTransfers.Item(2, intRow).Value, flxTransfers.Item(3, intRow).Value, flxTransfers.Item(2, intRow).Value, flxTransfers.Item(3, intRow).Value)

                        ElseIf flxTransfers.Item(0, intRow).Value = "Grading Checking" Then
                            strPktNo = "001"
                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT MAX(PktNo) AS MaxPktNo FROM tblGradingTrf WHERE ParcelNo = '" & flxTransfers.Item(1, intRow).Value & "' AND Department = 'Grading Checking'", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount Then
                                If Not IsDBNull(rsComSql_1.Fields("MaxPktNo").Value) Then
                                    strPktNo = Format(rsComSql_1.Fields("MaxPktNo").Value + 1, "000")
                                Else
                                    strPktNo = "001"
                                End If
                            End If
                            rsComSql_1 = Nothing
                            Call Dep_Grading_Trf("Grading Checking", 9998, flxTransfers.Item(1, intRow).Value, strPktNo, flxTransfers.Item(2, intRow).Value, flxTransfers.Item(3, intRow).Value, flxTransfers.Item(2, intRow).Value, flxTransfers.Item(3, intRow).Value)

                        ElseIf flxTransfers.Item(0, intRow).Value = "Precision" Then
                            strOrgAssort = txtAssortment.Text

                            AdoCN.Execute("INSERT INTO tblPCUStockIn(OrgAssort,Assortment,ParNo,InPcs,InCts,Price,Status) " & _
                                          "VALUES('" & strOrgAssort & "','" & strOrgAssort & "'," & _
                                                "'" & flxTransfers.Item(1, intRow).Value & "'," & CInt(flxTransfers.Item(2, intRow).Value) & "," & _
                                                "" & CDbl(flxTransfers.Item(3, intRow).Value) & "," & CDbl(txtPrice.Text) & ",1)")

                        ElseIf flxTransfers.Item(0, intRow).Value = "Mix" Then
                            strPktNo = "N001"

                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT MAX(RIGHT(PktNo, 3)) AS MaxPkt FROM tblExpPacket WHERE Department = 'Mix' AND ParNo = '" & flxTransfers.Item(1, intRow).Value & "' AND LEFT(PktNo, 1) = 'N'", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount Then
                                If Not IsDBNull(rsComSql_1.Fields("MaxPkt").Value) Then
                                    strPktNo = "N" & Format(rsComSql_1.Fields("MaxPkt").Value + 1, "000")
                                End If
                            End If
                            rsComSql_1 = Nothing

                            If strDBName = "DiaShare" Then
                                intAMS = 1
                                intYahuda = 1
                            End If

                            If intOriginal = 0 Then
                                intAMS = 1
                                intYahuda = 1
                            Else
                                If Len(Rs1.Fields("ConRefNo").Value) = 0 Then
                                    intAMS = 0
                                    intYahuda = 0
                                Else
                                    intAMS = 1
                                    intYahuda = 1
                                End If
                            End If

                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT * FROM tblExpPacket WHERE Department = 'Mix' AND ParNo = '" & flxTransfers.Item(1, intRow).Value & "' AND PktNo = '" & strPktNo & "'", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount = 0 Then
                                AdoCN.Execute("INSERT INTO tblExpPacket(Department,ParNo,PktNo,PktPcs,PktCts,PktType,PktDate,AParNo,AMS2,YAH) " & _
                                              "VALUES('Mix','" & flxTransfers.Item(1, intRow).Value & "','" & strPktNo & "'," & CInt(flxTransfers.Item(2, intRow).Value) & "," & CDbl(flxTransfers.Item(3, intRow).Value) & ",'','" & Format(Date.Now, "MM/dd/yyyy") & "','" & txtAssortment.Text & "'," & intAMS & "," & intYahuda & ")")
                            End If
                            rsComSql_1 = Nothing

                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT * FROM tblGrading_Parcel WHERE ParNo = '" & flxTransfers.Item(1, intRow).Value & "' AND Dept = 'Mix'", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount = 0 Then
                                AdoCN.Execute("INSERT INTO tblGrading_Parcel(ParNo,Complete,Dept,Assort) VALUES('" & flxTransfers.Item(1, intRow).Value & "',0,'Mix','" & txtAssortment.Text & "')")
                            End If
                            rsComSql_1 = Nothing
                        End If
                    Else
                        AdoCN.Execute("UPDATE dbo.tblDep_Trf SET NewACTPcs = NewACTPcs + " & flxTransfers.Item(2, intRow).Value & ",NewACTCts = NewACTCts + " & flxTransfers.Item(3, intRow).Value & ",ModifyBy = " & DataEnter & ",Status = 'I'" & " " & _
                                      "WHERE (SupParcelNo = '" & txtSupParNo.Text & "') AND (Department = '" & flxTransfers.Item(0, intRow).Value & "') AND (DCLParcelNo = '" & flxTransfers.Item(1, intRow).Value & "')")

                        If flxTransfers.Item(0, intRow).Value = "RoughWO" Or flxTransfers.Item(0, intRow).Value = "RoughWO2" Or flxTransfers.Item(0, intRow).Value = "RoughPlan" Or flxTransfers.Item(0, intRow).Value = "RoughPlan2" Or _
                           flxTransfers.Item(0, intRow).Value = "RoughPlanAS" Or flxTransfers.Item(0, intRow).Value = "RoughPlanAS2" Or flxTransfers.Item(0, intRow).Value = "RoughPlanAS3" Or flxTransfers.Item(0, intRow).Value = "RoughPlanAS4" Or _
                           flxTransfers.Item(0, intRow).Value = "RoughPlanAS5" Or flxTransfers.Item(0, intRow).Value = "RoughPlanAS6" Or flxTransfers.Item(0, intRow).Value = "RoughTS" Or flxTransfers.Item(0, intRow).Value = "RoughTS2" Or flxTransfers.Item(0, intRow).Value = "RoughTS3" Or flxTransfers.Item(0, intRow).Value = "RoughTS4" Or flxTransfers.Item(0, intRow).Value = "RoughTS5" Or flxTransfers.Item(0, intRow).Value = "RoughTS6" Or flxTransfers.Item(0, intRow).Value = "RoughOpr" Or _
                           flxTransfers.Item(0, intRow).Value = "RoughOpr2" Or flxTransfers.Item(0, intRow).Value = "RoughOpr3" Or flxTransfers.Item(0, intRow).Value = "RoughOpr4" Or flxTransfers.Item(0, intRow).Value = "RoughOpr5" Or flxTransfers.Item(0, intRow).Value = "RoughOpr6" Or flxTransfers.Item(0, intRow).Value = "Sawing" Or _
                           flxTransfers.Item(0, intRow).Value = "RoughSMarking" Or flxTransfers.Item(0, intRow).Value = "RoughSawing" Or flxTransfers.Item(0, intRow).Value = "RoughSawing2" Or flxTransfers.Item(0, intRow).Value = "RoughSawing3" Or flxTransfers.Item(0, intRow).Value = "RoughSawing4" Or flxTransfers.Item(0, intRow).Value = "RoughSawing5" Or flxTransfers.Item(0, intRow).Value = "RoughSawing6" Or flxTransfers.Item(0, intRow).Value = "RoughSawingS" Or _
                           flxTransfers.Item(0, intRow).Value = "RoughASSorting" Or flxTransfers.Item(0, intRow).Value = "RoughBoil" Then

                            strGrp = "A"
                            strGrpParNo = flxTransfers.Item(1, intRow).Value & strGrp
                            intReIssue = 0

                            strFlow = ""
                            rsComSql_2 = New ADODB.Recordset
                            rsComSql_2.Open("SELECT * FROM tblRPrFlow WHERE Department = '" & flxTransfers.Item(0, intRow).Value & "'", AdoCN, 1, 1)
                            If rsComSql_2.RecordCount Then
                                strFlow = rsComSql_2.Fields("Flow").Value
                            End If
                            rsComSql_2 = Nothing

                            rsComSql_2 = New ADODB.Recordset
                            rsComSql_2.Open("SELECT * FROM tblParcel WHERE GrpParNo  = '" & strGrpParNo & "' AND Depart = '" & flxTransfers.Item(0, intRow).Value & "'", AdoCN, 1, 3)
                            If rsComSql_2.RecordCount Then
                                rsComSql_2.Fields("ActPcs").Value = rsComSql_2.Fields("ActPcs").Value + CDbl(flxTransfers.Item(2, intRow).Value)
                                rsComSql_2.Fields("ACtCts").Value = rsComSql_2.Fields("ACtCts").Value + CDbl(flxTransfers.Item(3, intRow).Value)
                                rsComSql_2.Fields("IssuedPcs").Value = rsComSql_2.Fields("IssuedPcs").Value + CDbl(flxTransfers.Item(2, intRow).Value)
                                rsComSql_2.Fields("IssuedCts").Value = rsComSql_2.Fields("IssuedCts").Value + CDbl(flxTransfers.Item(3, intRow).Value)
                                rsComSql_2.Update()
                            Else
                                rsComSql_2.AddNew()
                                rsComSql_2.Fields("Depart").Value = flxTransfers.Item(0, intRow).Value
                                rsComSql_2.Fields("ImpNo").Value = txtImportNo.Text
                                rsComSql_2.Fields("ImpDate").Value = Rs1.Fields("InvoiceDate").Value
                                rsComSql_2.Fields("MainCategory").Value = "DCL"
                                rsComSql_2.Fields("OrderRef").Value = 1
                                rsComSql_2.Fields("Assortment").Value = txtAssortment.Text
                                rsComSql_2.Fields("ParcelNo").Value = flxTransfers.Item(1, intRow).Value
                                rsComSql_2.Fields("OrigParcelNo").Value = txtSupParNo.Text
                                rsComSql_2.Fields("Grp").Value = strGrp
                                rsComSql_2.Fields("RevGrp").Value = strGrp
                                rsComSql_2.Fields("GrpParNo").Value = strGrpParNo
                                rsComSql_2.Fields("ActPcs").Value = flxTransfers.Item(2, intRow).Value
                                rsComSql_2.Fields("ACtCts").Value = flxTransfers.Item(3, intRow).Value
                                rsComSql_2.Fields("IssuedPcs").Value = flxTransfers.Item(2, intRow).Value
                                rsComSql_2.Fields("IssuedCts").Value = flxTransfers.Item(3, intRow).Value
                                rsComSql_2.Fields("Category").Value = "MB"
                                rsComSql_2.Fields("SubCategory").Value = "Rounds Sub"
                                rsComSql_2.Fields("IncUnit").Value = "A"
                                rsComSql_2.Fields("Instruction").Value = ""
                                rsComSql_2.Fields("RejectPcs").Value = 0
                                rsComSql_2.Fields("RejectCts").Value = 0
                                rsComSql_2.Fields("RejectRate").Value = 0
                                rsComSql_2.Fields("ItemCost").Value = txtPrice.Text
                                rsComSql_2.Fields("Complete").Value = 0
                                rsComSql_2.Fields("IssueFinish").Value = 0
                                rsComSql_2.Fields("Charges").Value = 0
                                rsComSql_2.Fields("vCharges").Value = 0
                                rsComSql_2.Fields("Flow").Value = strFlow
                                rsComSql_2.Fields("ParCut").Value = 0
                                rsComSql_2.Fields("EstSup").Value = 0
                                rsComSql_2.Fields("EstDCL").Value = 0
                                rsComSql_2.Fields("Status").Value = "I"
                                rsComSql_2.Fields("DoneBy").Value = PBUser_ID
                                rsComSql_2.Fields("ModifyBy").Value = PBUser_ID
                                rsComSql_2.Fields("RghPcs").Value = flxTransfers.Item(2, intRow).Value
                                rsComSql_2.Fields("RghCts").Value = flxTransfers.Item(3, intRow).Value
                                rsComSql_2.Fields("Approval").Value = 0
                                rsComSql_2.Fields("ReIssue").Value = intReIssue
                                rsComSql_2.Fields("Segment").Value = ""
                                rsComSql_2.Update()
                            End If
                            rsComSql_2 = Nothing
                        End If

                        If flxTransfers.Item(2, intRow).Value > 0 Then
                            If flxTransfers.Item(0, intRow).Value = "Grading" Then
                                Call Dep_Grading_Trf("Direct Import", 9999, flxTransfers.Item(1, intRow).Value, "001", flxTransfers.Item(2, intRow).Value, flxTransfers.Item(3, intRow).Value, flxTransfers.Item(2, intRow).Value, flxTransfers.Item(3, intRow).Value)

                            ElseIf flxTransfers.Item(0, intRow).Value = "Grading Checking" Then
                                strPktNo = "001"
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT MAX(PktNo) AS MaxPktNo FROM tblGradingTrf WHERE ParcelNo = '" & flxTransfers.Item(1, intRow).Value & "' AND Department = 'Grading Checking'", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount Then
                                    If Not IsDBNull(rsComSql_1.Fields("MaxPktNo").Value) Then
                                        strPktNo = Format(rsComSql_1.Fields("MaxPktNo").Value + 1, "000")
                                    Else
                                        strPktNo = "001"
                                    End If
                                End If
                                rsComSql_1 = Nothing
                                Call Dep_Grading_Trf("Grading Checking", 9999, flxTransfers.Item(1, intRow).Value, strPktNo, flxTransfers.Item(2, intRow).Value, flxTransfers.Item(3, intRow).Value, flxTransfers.Item(2, intRow).Value, flxTransfers.Item(3, intRow).Value)

                            ElseIf flxTransfers.Item(0, intRow).Value = "Precision" Then
                                strOrgAssort = txtAssortment.Text

                                AdoCN.Execute("INSERT INTO tblPCUStockIn(OrgAssort,Assortment,ParNo,InPcs,InCts,Price,Status) " & _
                                              "VALUES('" & strOrgAssort & "','" & strOrgAssort & "'," & _
                                                    "'" & flxTransfers.Item(1, intRow).Value & "'," & CInt(flxTransfers.Item(2, intRow).Value) & "," & _
                                                    "" & CDbl(flxTransfers.Item(3, intRow).Value) & "," & CDbl(txtPrice.Text) & ",1)")

                            ElseIf flxTransfers.Item(0, intRow).Value = "Mix" Or flxTransfers.Item(0, intRow).Value = "KIT Box" Then
                                strPktNo = "N001"
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT MAX(RIGHT(PktNo, 3)) AS MaxPktNo FROM tblExpPacket WHERE ParNo = '" & flxTransfers.Item(1, intRow).Value & "' AND Department = 'Mix' AND LEFT(PktNo, 1) = 'N'", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount Then
                                    If Not IsDBNull(rsComSql_1.Fields("MaxPktNo").Value) Then
                                        strPktNo = "N" & Format(rsComSql_1.Fields("MaxPktNo").Value + 1, "000")
                                    Else
                                        strPktNo = "N001"
                                    End If
                                End If
                                rsComSql_1 = Nothing

                                If strDBName = "DiaShare" Then
                                    intAMS = 1
                                    intYahuda = 1
                                End If

                                If intOriginal = 0 Then
                                    intAMS = 1
                                    intYahuda = 1
                                Else
                                    If Len(Rs1.Fields("ConRefNo").Value) = 0 Then
                                        intAMS = 0
                                        intYahuda = 0
                                    Else
                                        intAMS = 1
                                        intYahuda = 1
                                    End If
                                End If

                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT * FROM tblExpPacket WHERE Department = '" & flxTransfers.Item(0, intRow).Value & "' AND ParNo = '" & flxTransfers.Item(1, intRow).Value & "' AND PktNo = '" & strPktNo & "'", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount = 0 Then
                                    AdoCN.Execute("INSERT INTO tblExpPacket(Department,ParNo,PktNo,PktPcs,PktCts,PktType,PktDate,AParNo,AMS2,YAH) " & _
                                                  "VALUES('" & flxTransfers.Item(0, intRow).Value & "','" & flxTransfers.Item(1, intRow).Value & "','" & strPktNo & "'," & CInt(flxTransfers.Item(2, intRow).Value) & "," & CDbl(flxTransfers.Item(3, intRow).Value) & ",'','" & Format(Date.Now, "MM/dd/yyyy") & "','" & txtAssortment.Text & "'," & intAMS & "," & intYahuda & ")")
                                End If
                                rsComSql_1 = Nothing

                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT * FROM tblGrading_Parcel WHERE ParNo = '" & flxTransfers.Item(1, intRow).Value & "' AND Dept = '" & flxTransfers.Item(0, intRow).Value & "'", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount = 0 Then
                                    AdoCN.Execute("INSERT INTO tblGrading_Parcel(ParNo,Complete,Dept,Assort) VALUES('" & flxTransfers.Item(1, intRow).Value & "',0,'" & flxTransfers.Item(0, intRow).Value & "','" & txtAssortment.Text & "')")
                                End If
                                rsComSql_1 = Nothing
                            End If
                        End If
                    End If
                    rs2 = Nothing
                Next

                MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

                ClearFields()
            Else
                If blnPCU = True Then
                    For intRow = 0 To flxTransfers.Rows.Count - 1
                        rs2 = New ADODB.Recordset
                        rs2.Open("SELECT * FROM tblDep_Trf WHERE Department = '" & flxTransfers.Item(0, intRow).Value & "' AND SupParcelNo = '" & txtSupParNo.Text & "' AND DCLParcelNo = '" & flxTransfers.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                        If rs2.RecordCount = 0 Then
                            AdoCN.Execute("INSERT INTO tblDep_Trf(Department,DCLImportNo,SupplierRefNo,CompanyRefNo,BOINo,InvoiceDate,RecievedDate,SupplierCode,ParcelType,AssortmentNo,SupParcelNo,DCLParcelNo,INVPcs,INVCts,ACTPcs,ACtCts,NewACTPcs,NewACTCts,ItemSize,Charges,ItemCost,RemPcs,RemCts,Status,DoneBy,ModifyBy,AParNo) " & _
                                          "VALUES('" & flxTransfers.Item(0, intRow).Value & "','0','0','0','','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "MM/dd/yyyy") & "','1','Polished'," & _
                                            "'" & txtAssortment.Text & "','" & txtSupParNo.Text & "','" & flxTransfers.Item(1, intRow).Value & "'," & flxTransfers.Item(2, intRow).Value & "," & flxTransfers.Item(3, intRow).Value & "," & flxTransfers.Item(2, intRow).Value & "," & flxTransfers.Item(3, intRow).Value & "," & flxTransfers.Item(2, intRow).Value & "," & _
                                            "" & flxTransfers.Item(3, intRow).Value & ",0,0,0,0,0,'I','" & PBUser_ID & "'," & DataEnter & ",'')")

                            If flxTransfers.Item(0, intRow).Value = "Grading" Then
                                Call Dep_Grading_Trf("Direct Import", 9999, flxTransfers.Item(1, intRow).Value, "001", flxTransfers.Item(2, intRow).Value, flxTransfers.Item(3, intRow).Value, flxTransfers.Item(2, intRow).Value, flxTransfers.Item(3, intRow).Value)

                            ElseIf flxTransfers.Item(0, intRow).Value = "Grading Rounds" Then
                                Call Dep_Grading_Trf("Rounds Direct", 9999, flxTransfers.Item(1, intRow).Value, "001", flxTransfers.Item(2, intRow).Value, flxTransfers.Item(3, intRow).Value, flxTransfers.Item(2, intRow).Value, flxTransfers.Item(3, intRow).Value)

                            ElseIf flxTransfers.Item(0, intRow).Value = "Grading Checking" Then
                                strPktNo = "001"
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT MAX(PktNo) AS MaxPktNo FROM tblGradingTrf WHERE ParcelNo = '" & flxTransfers.Item(1, intRow).Value & "' AND Department = 'Grading Checking'", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount Then
                                    If Not IsDBNull(rsComSql_1.Fields("MaxPktNo").Value) Then
                                        strPktNo = Format(rsComSql_1.Fields("MaxPktNo").Value + 1, "000")
                                    Else
                                        strPktNo = "001"
                                    End If
                                End If
                                rsComSql_1 = Nothing
                                Call Dep_Grading_Trf("Grading Checking", 9998, flxTransfers.Item(1, intRow).Value, strPktNo, flxTransfers.Item(2, intRow).Value, flxTransfers.Item(3, intRow).Value, flxTransfers.Item(2, intRow).Value, flxTransfers.Item(3, intRow).Value)

                            ElseIf flxTransfers.Item(0, intRow).Value = "Precision" Then
                                strOrgAssort = txtAssortment.Text

                                AdoCN.Execute("INSERT INTO tblPCUStockIn(OrgAssort,Assortment,ParNo,InPcs,InCts,Price,Status) " & _
                                              "VALUES('" & strOrgAssort & "','" & strOrgAssort & "'," & _
                                                    "'" & flxTransfers.Item(1, intRow).Value & "'," & CInt(flxTransfers.Item(2, intRow).Value) & "," & _
                                                    "" & CDbl(flxTransfers.Item(3, intRow).Value) & "," & CDbl(txtPrice.Text) & ",1)")

                            ElseIf flxTransfers.Item(0, intRow).Value = "Mix" Then
                                If strDBName = "DiaShare" Then
                                    intAMS = 1
                                    intYahuda = 1
                                End If

                                If intOriginal = 0 Then
                                    intAMS = 1
                                    intYahuda = 1
                                Else
                                    intAMS = 0
                                    intYahuda = 0
                                End If

                                strPktNo = "N001"
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT * FROM tblExpPacket WHERE Department = 'Mix' AND ParNo = '" & flxTransfers.Item(1, intRow).Value & "' AND PktNo = '" & strPktNo & "'", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount = 0 Then
                                    AdoCN.Execute("INSERT INTO tblExpPacket(Department,ParNo,PktNo,PktPcs,PktCts,PktType,PktDate,AParNo,AMS2,YAH) " & _
                                                  "VALUES('Mix','" & flxTransfers.Item(1, intRow).Value & "','" & strPktNo & "'," & CInt(flxTransfers.Item(2, intRow).Value) & "," & CDbl(flxTransfers.Item(3, intRow).Value) & ",'','" & Format(Date.Now, "MM/dd/yyyy") & "',''," & intAMS & "," & intYahuda & ")")
                                End If
                                rsComSql_1 = Nothing

                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT * FROM tblGrading_Parcel WHERE ParNo = '" & flxTransfers.Item(1, intRow).Value & "' AND Dept = 'Mix'", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount = 0 Then
                                    AdoCN.Execute("INSERT INTO tblGrading_Parcel(ParNo,Complete,Dept,Assort) VALUES('" & flxTransfers.Item(1, intRow).Value & "',0,'Mix','" & txtAssortment.Text & "')")
                                End If
                                rsComSql_1 = Nothing
                            End If

                        Else
                            AdoCN.Execute("UPDATE dbo.tblDep_Trf SET NewACTPcs = NewACTPcs + " & flxTransfers.Item(2, intRow).Value & ",NewACTCts = NewACTCts + " & flxTransfers.Item(3, intRow).Value & ",ModifyBy = " & DataEnter & ",Status = 'I'" & " " & _
                                          "WHERE (SupParcelNo = '" & txtSupParNo.Text & "') AND (Department = '" & flxTransfers.Item(0, intRow).Value & "') AND (DCLParcelNo = '" & flxTransfers.Item(1, intRow).Value & "')")

                            If flxTransfers.Item(2, intRow).Value > 0 Then
                                If flxTransfers.Item(0, intRow).Value = "Grading" Then
                                    Call Dep_Grading_Trf("Direct Import", 9999, flxTransfers.Item(1, intRow).Value, "001", flxTransfers.Item(2, intRow).Value, flxTransfers.Item(3, intRow).Value, flxTransfers.Item(2, intRow).Value, flxTransfers.Item(3, intRow).Value)

                                ElseIf flxTransfers.Item(0, intRow).Value = "Grading Checking" Then
                                    strPktNo = "001"
                                    rsComSql_1 = New ADODB.Recordset
                                    rsComSql_1.Open("SELECT MAX(PktNo) AS MaxPktNo FROM tblGradingTrf WHERE ParcelNo = '" & flxTransfers.Item(1, intRow).Value & "' AND Department = 'Grading Checking'", AdoCN, 1, 1)
                                    If rsComSql_1.RecordCount Then
                                        If Not IsDBNull(rsComSql_1.Fields("MaxPktNo").Value) Then
                                            strPktNo = Format(rsComSql_1.Fields("MaxPktNo").Value + 1, "000")
                                        Else
                                            strPktNo = "001"
                                        End If
                                    End If
                                    rsComSql_1 = Nothing
                                    Call Dep_Grading_Trf("Grading Checking", 9999, flxTransfers.Item(1, intRow).Value, strPktNo, flxTransfers.Item(2, intRow).Value, flxTransfers.Item(3, intRow).Value, flxTransfers.Item(2, intRow).Value, flxTransfers.Item(3, intRow).Value)

                                ElseIf flxTransfers.Item(0, intRow).Value = "Precision" Then
                                    strOrgAssort = txtAssortment.Text

                                    AdoCN.Execute("INSERT INTO tblPCUStockIn(OrgAssort,Assortment,ParNo,InPcs,InCts,Price,Status) " & _
                                                  "VALUES('" & strOrgAssort & "','" & strOrgAssort & "'," & _
                                                        "'" & flxTransfers.Item(1, intRow).Value & "'," & CInt(flxTransfers.Item(2, intRow).Value) & "," & _
                                                        "" & CDbl(flxTransfers.Item(3, intRow).Value) & "," & CDbl(txtPrice.Text) & ",1)")

                                ElseIf flxTransfers.Item(0, intRow).Value = "Mix" Then
                                    strPktNo = "N001"
                                    rsComSql_1 = New ADODB.Recordset
                                    rsComSql_1.Open("SELECT MAX(RIGHT(PktNo, 3)) AS MaxPktNo FROM tblExpPacket WHERE ParNo = '" & flxTransfers.Item(1, intRow).Value & "' AND Department = 'Mix' AND LEFT(PktNo, 1) = 'N'", AdoCN, 1, 1)
                                    If rsComSql_1.RecordCount Then
                                        If Not IsDBNull(rsComSql_1.Fields("MaxPktNo").Value) Then
                                            strPktNo = "N" & Format(rsComSql_1.Fields("MaxPktNo").Value + 1, "000")
                                        Else
                                            strPktNo = "N001"
                                        End If
                                    End If
                                    rsComSql_1 = Nothing

                                    rsComSql_1 = New ADODB.Recordset
                                    rsComSql_1.Open("SELECT * FROM tblExpPacket WHERE Department = 'Mix' AND ParNo = '" & flxTransfers.Item(1, intRow).Value & "' AND PktNo = '" & strPktNo & "'", AdoCN, 1, 1)
                                    If rsComSql_1.RecordCount = 0 Then
                                        AdoCN.Execute("INSERT INTO tblExpPacket(Department,ParNo,PktNo,PktPcs,PktCts,PktType,PktDate,AParNo,AMS2,YAH) " & _
                                                      "VALUES('Mix','" & flxTransfers.Item(1, intRow).Value & "','" & strPktNo & "'," & CInt(flxTransfers.Item(2, intRow).Value) & "," & CDbl(flxTransfers.Item(3, intRow).Value) & ",'','" & Format(Date.Now, "MM/dd/yyyy") & "','" & txtAssortment.Text & "',1,1)")
                                    End If
                                    rsComSql_1 = Nothing

                                    rsComSql_1 = New ADODB.Recordset
                                    rsComSql_1.Open("SELECT * FROM tblGrading_Parcel WHERE ParNo = '" & flxTransfers.Item(1, intRow).Value & "' AND Dept = 'Mix'", AdoCN, 1, 1)
                                    If rsComSql_1.RecordCount = 0 Then
                                        AdoCN.Execute("INSERT INTO tblGrading_Parcel(ParNo,Complete,Dept,Assort) VALUES('" & flxTransfers.Item(1, intRow).Value & "',0,'Mix','" & txtAssortment.Text & "')")
                                    End If
                                    rsComSql_1 = Nothing
                                End If
                            End If
                        End If
                        rs2 = Nothing
                    Next
                End If

                MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

                ClearFields()
            End If
            Rs1 = Nothing
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmbDepartment_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbDepartment.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtNewDclNo.Focus()
        End If
    End Sub

    Private Sub txtRecPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRecPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtRecCts.Focus()
        End If
    End Sub

    Private Sub txtRecCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRecCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRecCts.Text)
        If Asc(e.KeyChar) = 13 Then
            txtRghCts.Focus()
        End If
    End Sub

    Private Sub txtRghCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRghCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtRghCts.Text)
    End Sub
End Class