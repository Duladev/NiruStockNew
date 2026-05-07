
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_MixOrderT
    Dim strFolderPath As String

    Private Sub ClearText()
        'txtOrdNo.Text = ""
        cmbSupp.Text = "Niru Diamonds Israel (1987) Ltd"
        cmbClient.Text = ""
        txtDesc.Text = ""
        txtRemarks.Text = ""
        dtpDueDate.Value = Date.Now
        dtpOrdDate.Value = Date.Now

        txtRef.Text = ""
        txtSide.Text = ""
        txtLen.Text = ""
        txtWid.Text = ""
        txtSets.Text = ""
        txtPcs.Text = ""
        txtTotPcs.Text = ""
        txtMaxCost.Text = ""
        cmbType.Text = ""

        flxDetails.Rows.Clear()
    End Sub

    Private Sub GetNewOrderNo()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(OrderNo) AS OrdNo FROM tblOrdersT", AdoCN, 1, 1)
        If IsDBNull(rsComSql.Fields("OrdNo").Value) Then
            txtOrdNo.Text = "100001"
        Else
            txtOrdNo.Text = CDbl(rsComSql.Fields("OrdNo").Value) + 1
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearText()
        GetNewOrderNo()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_Supplier()
        cmbSupp.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT CompanyName FROM tblSuppliers ORDER BY CompanyName", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            Do While Not rsComSql.EOF
                cmbSupp.Items.Add(rsComSql.Fields("CompanyName").Value)
                rsComSql.MoveNext()
            Loop
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Client()
        cmbClient.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT NiruCust FROM tblNiruRef ORDER BY NiruCust", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            Do While Not rsComSql.EOF
                cmbClient.Items.Add(rsComSql.Fields("NiruCust").Value)
                rsComSql.MoveNext()
            Loop
        End If
        rsComSql = Nothing
    End Sub

    Private Sub txtOrdNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrdNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            Load_OrderDetails()
        End If
    End Sub

    Private Sub txtNiruOrdNo_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Asc(e.KeyChar) = 13 Then
            cmbSupp.Focus()
        End If
    End Sub

    Private Sub cmbSupp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSupp.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbClient.Focus()
        End If
    End Sub

    Private Sub txtDesc_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDesc.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtRef.Focus()
        End If
    End Sub

    Private Sub txtRef_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRef.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtSide.Focus()
        End If
    End Sub

    Private Sub txtLen_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLen.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtWid.Focus()
        End If
    End Sub

    Private Sub txtWid_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWid.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtSets.Focus()
        End If
    End Sub

    Private Sub txtSets_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSets.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtSets.Text <> "" And txtPcs.Text <> "" Then
                txtTotPcs.Text = CDbl(txtSets.Text) * CDbl(txtPcs.Text)
            End If
            txtPcs.Focus()
        End If
    End Sub

    Private Sub txtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If txtSets.Text <> "" And txtPcs.Text <> "" Then
                txtTotPcs.Text = CDbl(txtSets.Text) * CDbl(txtPcs.Text)
            End If
            txtMaxCost.Focus()
        End If
    End Sub

    Private Sub txtMaxCost_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMaxCost.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtMaxCost.Text)
        If Asc(e.KeyChar) = 13 Then
            cmbType.Focus()
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        AddReference()
    End Sub

    Private Sub AddReference()
        PBResponse = MsgBox("Are you sure you want to update this Transaction?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If txtRef.Text = "" Then
                MsgBox("Invalid Reference", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtSide.Text = "" Then
                MsgBox("Invalid Side", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtLen.Text = "" Then
                MsgBox("Invalid Length", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If Not IsNumeric(txtLen.Text) Then
                MsgBox("Invalid Length", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtWid.Text = "" Then
                MsgBox("Invalid Width", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If Not IsNumeric(txtWid.Text) Then
                MsgBox("Invalid Width", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If txtMaxCost.Text = "" Then
                MsgBox("Invalid Maximum Cost", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If cmbType.Text = "" Then
                MsgBox("Invalid Maximum Cost Type", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            flxDetails.Rows.Add(txtRef.Text, txtSide.Text, Format(CSng(txtLen.Text), "#0.00"),
                                Format(CSng(txtWid.Text), "#0.00"), txtSets.Text, txtPcs.Text, txtTotPcs.Text, txtMaxCost.Text, cmbType.Text)

            txtRef.Focus()
        End If
    End Sub

    Private Sub SaveOrder()
        Dim intRow As Integer
        Dim dblCutRate As Double
        Dim blnAccess As Boolean

        dblCutRate = 0
        blnAccess = False

        If Len(txtOrdNo.Text) <> 6 Then
            MsgBox("Invalid Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblOrdersT WHERE OrderNo = " & CInt(txtOrdNo.Text) & "", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                mStrSQL = "INSERT INTO tblOrdersT(OrderNo,Customer,DueDate,Subject,Niruref,OrdDate,Complete,Dept,Subject2) " & _
                          "VALUES(" & CInt(txtOrdNo.Text) & ",'" & cmbSupp.Text & "','" & Format(dtpDueDate.Value, "MM/dd/yyyy") & "','" & Trim(txtDesc.Text) & "','" & cmbClient.Text & "'," & _
                            "'" & Format(dtpOrdDate.Value, "MM/dd/yyyy") & "','N','','" & Trim(txtRemarks.Text) & "')"

                AdoCN.Execute(mStrSQL)

                For intRow = 0 To flxDetails.Rows.Count - 1
                    mStrSQL = "INSERT INTO tblOrdersDtlsT(OrderNo,RefNo,Side,Length,Width,Sets,PCs,MaxCost,MaxType) " & _
                              "VALUES(" & CInt(txtOrdNo.Text) & ",'" & Trim(flxDetails.Item(0, intRow).Value) & "','" & Trim(flxDetails.Item(1, intRow).Value) & "','" & Trim(flxDetails.Item(2, intRow).Value) & "','" & Trim(flxDetails.Item(3, intRow).Value) & "'," & _
                                "" & CInt(flxDetails.Item(4, intRow).Value) & "," & CInt(flxDetails.Item(5, intRow).Value) & "," & CDbl(flxDetails.Item(7, intRow).Value) & ",'" & Trim(flxDetails.Item(8, intRow).Value) & "')"

                    AdoCN.Execute(mStrSQL)
                Next

                MsgBox("Order Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        Else
            PBResponse = MsgBox("Already Exists. Are you sure to Update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                blnAccess = True
                If blnAccess = True Then
                    mStrSQL = "UPDATE tblOrdersT SET Customer = '" & cmbSupp.Text & "',DueDate = '" & Format(dtpDueDate.Value, "MM/dd/yyyy") & "',Subject = '" & Trim(txtDesc.Text) & "',Niruref = '" & cmbClient.Text & "',OrdDate = '" & Format(dtpOrdDate.Value, "MM/dd/yyyy") & "'," & _
                                "Subject2 = '" & Trim(txtRemarks.Text) & "' " & _
                              "WHERE OrderNo = " & CInt(txtOrdNo.Text) & ""

                    AdoCN.Execute(mStrSQL)

                    mStrSQL = "DELETE FROM tblOrdersDtlsT WHERE OrderNo = " & CInt(txtOrdNo.Text) & ""

                    AdoCN.Execute(mStrSQL)

                    For intRow = 0 To flxDetails.Rows.Count - 1
                        mStrSQL = "INSERT INTO tblOrdersDtlsT(OrderNo,RefNo,Side,Length,Width,Sets,PCs,MaxCost,MaxType) " & _
                              "VALUES(" & CInt(txtOrdNo.Text) & ",'" & Trim(flxDetails.Item(0, intRow).Value) & "','" & Trim(flxDetails.Item(1, intRow).Value) & "','" & Trim(flxDetails.Item(2, intRow).Value) & "','" & Trim(flxDetails.Item(3, intRow).Value) & "'," & _
                                "" & CInt(flxDetails.Item(4, intRow).Value) & "," & CInt(flxDetails.Item(5, intRow).Value) & "," & CDbl(flxDetails.Item(7, intRow).Value) & ",'" & Trim(flxDetails.Item(8, intRow).Value) & "')"

                        AdoCN.Execute(mStrSQL)
                    Next

                    MsgBox("Order Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                End If

            End If

        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_OrderDetails()
        Dim blnFound As Boolean

        ClearText()
        blnFound = False
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblOrdersT WHERE OrderNo = " & txtOrdNo.Text & "", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            blnFound = True
            cmbSupp.Text = rsComSql.Fields("Customer").Value
            txtDesc.Text = rsComSql.Fields("Subject").Value
            cmbClient.Text = rsComSql.Fields("Niruref").Value
            dtpDueDate.Value = rsComSql.Fields("DueDate").Value
            dtpOrdDate.Value = rsComSql.Fields("OrdDate").Value
            txtRemarks.Text = rsComSql.Fields("Subject2").Value

            flxDetails.Rows.Clear()
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblOrdersDtlsT WHERE OrderNo = " & txtOrdNo.Text & " ORDER BY RefNo,Side", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                rsComSql_1.MoveFirst()
                While Not rsComSql_1.EOF
                    flxDetails.Rows.Add(rsComSql_1.Fields("RefNo").Value,
                                        rsComSql_1.Fields("Side").Value,
                                        rsComSql_1.Fields("Length").Value,
                                        rsComSql_1.Fields("Width").Value,
                                        rsComSql_1.Fields("Sets").Value,
                                        rsComSql_1.Fields("Pcs").Value,
                                        rsComSql_1.Fields("Sets").Value * rsComSql_1.Fields("Pcs").Value,
                                        rsComSql_1.Fields("MaxCost").Value,
                                        rsComSql_1.Fields("MaxType").Value)

                    rsComSql_1.MoveNext()
                End While
            End If
            rsComSql_1 = Nothing
        Else
            rsComSql_1 = New ADODB.Recordset
            rsComSql_1.Open("SELECT * FROM tblOrders WHERE OrderNo = " & txtOrdNo.Text & "", AdoCN, 1, 1)
            If rsComSql_1.RecordCount Then
                blnFound = True
                cmbSupp.Text = rsComSql_1.Fields("Customer").Value
                txtDesc.Text = rsComSql_1.Fields("Subject").Value
                cmbClient.Text = rsComSql_1.Fields("Niruref").Value
                dtpDueDate.Value = rsComSql_1.Fields("DueDate").Value
                dtpOrdDate.Value = rsComSql_1.Fields("OrdDate").Value
                txtRemarks.Text = rsComSql_1.Fields("Subject2").Value

                flxDetails.Rows.Clear()
                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT * FROM tblOrdersDtls WHERE OrderNo = " & txtOrdNo.Text & " ORDER BY RefNo,Side", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    rsComSql_2.MoveFirst()
                    While Not rsComSql_2.EOF
                        flxDetails.Rows.Add(rsComSql_2.Fields("RefNo").Value,
                                            rsComSql_2.Fields("Side").Value,
                                            rsComSql_2.Fields("Length").Value,
                                            rsComSql_2.Fields("Width").Value,
                                            rsComSql_2.Fields("Sets").Value,
                                            rsComSql_2.Fields("Pcs").Value,
                                            rsComSql_2.Fields("Sets").Value * rsComSql_2.Fields("Pcs").Value,
                                            rsComSql_2.Fields("MaxCost").Value,
                                            rsComSql_2.Fields("MaxType").Value)

                        rsComSql_2.MoveNext()
                    End While
                End If
                rsComSql_2 = Nothing
            End If
            rsComSql_1 = Nothing
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        SaveOrder()
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        txtRef.Text = flxDetails.Item(0, flxDetails.CurrentRow.Index).Value
        txtSide.Text = flxDetails.Item(1, flxDetails.CurrentRow.Index).Value
        txtLen.Text = flxDetails.Item(2, flxDetails.CurrentRow.Index).Value
        txtWid.Text = flxDetails.Item(3, flxDetails.CurrentRow.Index).Value
        txtSets.Text = flxDetails.Item(4, flxDetails.CurrentRow.Index).Value
        txtPcs.Text = flxDetails.Item(5, flxDetails.CurrentRow.Index).Value
        txtTotPcs.Text = CDbl(txtSets.Text) * CDbl(txtPcs.Text)
        txtMaxCost.Text = flxDetails.Item(7, flxDetails.CurrentRow.Index).Value
        cmbType.Text = flxDetails.Item(8, flxDetails.CurrentRow.Index).Value
    End Sub

    Private Sub cmbType_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbType.KeyPress
        If Asc(e.KeyChar) = 13 Then

        End If
    End Sub

    Private Sub flxDetails_DoubleClick(sender As Object, e As EventArgs) Handles flxDetails.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)
        End If
    End Sub

    Private Sub txtSide_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSide.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtLen.Focus()
        End If
    End Sub

    Private Sub cmdReport_Click(sender As Object, e As EventArgs) Handles cmdReport.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixOrdersT.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub frm_MixOrderT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        If strDBName = "DiaStock" Then
            strFolderPath = "Mix\"
        Else
            strFolderPath = "DiaSalesMix\"
        End If

        Load_Supplier()
        Load_Client()

        ClearText()
        GetNewOrderNo()
    End Sub
End Class