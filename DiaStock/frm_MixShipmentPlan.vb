
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_MixShipmentPlan

    Private Sub ClearFields()
        GetNewPlanID()
        txtOrder.Text = ""
        txtPcs.Text = ""
        txtPcs2.Text = ""
        txtGroup.Text = ""
        cmbPriority.Text = ""
        flxOrder.Rows.Clear()
        flxDetails.Rows.Clear()
        dtpFinishDate.Value = Date.Now
        txtClient.Text = ""
        txtSubject.Text = ""
        txtSubject2.Text = ""
        txtDueDate.Text = ""
        txtTotal.Text = ""
        txtTotExport.Text = ""
        txtTotInFinish.Text = ""
        txtTotInProd.Text = ""
        txtPlanPcs.Text = ""
        txtOrdPcs.Text = ""
        txtIssPcs.Text = ""
        txtBalPcs.Text = ""
        txtShipBalPcs.Text = ""
        txtExportPcs.Text = ""
    End Sub

    Private Sub GetNewPlanID()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(OrderID) AS MaxID FROM tblPlaneOrders", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("MaxID").Value) Then
                txtPlanID.Text = rsComSql.Fields("MaxID").Value + 1
            Else
                txtPlanID.Text = "1"
            End If
        Else
            txtPlanID.Text = "1"
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Priority()
        cmbPriority.Items.Clear()
        cmbPriority.Items.Add("Priority 1")
        cmbPriority.Items.Add("Priority 2")
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
        dtpOrdDate.Value = Format(Date.Now, "MM/dd/yyyy")
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(3, intRow).Value)
        Next

    End Function

    Private Sub Load_OrderPlan()
        Dim strGroove As String
        Dim strLaser As String

        ClearFields()

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblPlaneOrders WHERE OrderDate = '" & Format(dtpOrdDate.Value, "MM/dd/yyyy") & "' ORDER BY OrderNo, Grp", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            txtPlanID.Text = rsComSql.Fields("OrderID").Value
            While Not rsComSql.EOF
                strGroove = ""
                strLaser = ""

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblOrders WHERE OrderNo = " & rsComSql.Fields("OrderNo").Value & "", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then

                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT MAX(Groove) AS Groove, MAX(Laser) AS Laser FROM dbo.tblOrdersDtls WHERE (OrderNo = '" & rsComSql.Fields("OrderNo").Value & "')", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        If rsComSql_2.Fields("Groove").Value > 0 Then
                            strGroove = "GROOVE"
                        End If
                        If rsComSql_2.Fields("Laser").Value > 0 Then
                            strLaser = "LASER"
                        End If
                    End If
                    rsComSql_2 = Nothing

                    flxOrder.Rows.Add(rsComSql.Fields("OrderID").Value,
                                  Format(rsComSql.Fields("OrderDate").Value, "yyyy/MM/dd"),
                                  rsComSql.Fields("OrderNo").Value,
                                  rsComSql.Fields("Pieces").Value,
                                  UCase(rsComSql_1.Fields("Dept").Value),
                                  rsComSql.Fields("Status").Value,
                                  rsComSql.Fields("Remarks").Value,
                                  rsComSql.Fields("Priority").Value,
                                  Format(rsComSql.Fields("FinishDate").Value, "yyyy/MM/dd"),
                                  rsComSql_1.Fields("Subject").Value,
                                  Format(rsComSql_1.Fields("DueDate").Value, "yyyy/MM/dd"),
                                  rsComSql.Fields("Pieces1").Value,
                                  rsComSql_1.Fields("NorderNo").Value,
                                  rsComSql_1.Fields("OrderItem").Value,
                                  rsComSql_1.Fields("NiruRef").Value,
                                  strGroove,
                                  strLaser,
                                  rsComSql_1.Fields("Subject2").Value)
                End If
                rsComSql_1 = Nothing

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtPlanPcs.Text = CalTotalPcs(flxOrder)
    End Sub

    Private Sub dtpOrdDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpOrdDate.ValueChanged
        Load_OrderPlan()
    End Sub

    Private Sub txtOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrder.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            Load_OrderDetails()
        End If
    End Sub

    Private Sub Load_OrderDetails()
        Dim dblTotal As Double
        Dim dblTotInProd As Double
        Dim dblTotInFinish As Double
        Dim dblTotExport As Double
        Dim intIssPcs As Integer
        Dim dblTotExportE As Double

        flxDetails.Rows.Clear()
        txtTotal.Text = ""
        txtTotInProd.Text = ""
        txtTotInFinish.Text = ""
        txtTotExport.Text = ""

        txtOrdPcs.Text = "0"
        txtIssPcs.Text = "0"
        txtBalPcs.Text = "0"
        txtShipBalPcs.Text = "0"
        txtExportPcs.Text = "0"

        dblTotal = 0
        dblTotInProd = 0
        dblTotInFinish = 0
        dblTotExport = 0
        dblTotExportE = 0

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblOrders WHERE OrderNo = " & txtOrder.Text & "", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If rsComSql.Fields("Complete").Value = "Y" Then
                MsgBox("Completed Order", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtOrder.Text = ""
                txtOrder.Focus()
                Exit Sub
            End If
            txtClient.Text = rsComSql.Fields("Niruref").Value
            txtDueDate.Text = Format(rsComSql.Fields("DueDate").Value, "yyyy/MM/dd")
            txtSubject.Text = rsComSql.Fields("Subject").Value
            txtSubject2.Text = rsComSql.Fields("Subject2").Value

            txtPcs.Focus()
        Else
            txtClient.Text = ""
            txtDueDate.Text = ""
            txtSubject.Text = ""

            MsgBox("Invalid Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM VW_MixOrderPcs WHERE OrderNo = " & txtOrder.Text & "", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            txtOrdPcs.Text = rsComSql.Fields("OrdPcs").Value
        End If
        rsComSql = Nothing

        intIssPcs = 0
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT SUM(PktPcs) AS TotPcs FROM tblMIXPacket WHERE PktOrdNo = '" & txtOrder.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("TotPcs").Value) Then
                intIssPcs = intIssPcs + rsComSql.Fields("TotPcs").Value
            End If
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT SUM(RejPcs + LostPcs) AS RejPcs FROM tblMixReturns WHERE ParNo = '" & txtOrder.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("RejPcs").Value) Then
                intIssPcs = intIssPcs - CInt(rsComSql.Fields("RejPcs").Value)
            End If
        End If
        rsComSql = Nothing

        txtIssPcs.Text = intIssPcs
        txtBalPcs.Text = CDbl(txtOrdPcs.Text) - CDbl(txtIssPcs.Text)

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM VW_MixOrderPlanSummary WHERE PktOrdNo = '" & txtOrder.Text & "' ORDER BY Grp", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(UCase(rsComSql.Fields("Grp").Value),
                                    rsComSql.Fields("SumOfPktPcs").Value - (rsComSql.Fields("TotEffects").Value + rsComSql.Fields("RetPcsT18").Value),
                                    rsComSql.Fields("RetB").Value,
                                    rsComSql.Fields("FinishedPcs").Value,
                                    rsComSql.Fields("SumOfPktPcs").Value - (rsComSql.Fields("TotEffects").Value + rsComSql.Fields("RetPcsT18").Value) + rsComSql.Fields("RetB").Value)

                dblTotal = dblTotal + rsComSql.Fields("SumOfPktPcs").Value - (rsComSql.Fields("TotEffects").Value + rsComSql.Fields("RetPcsT18").Value) + rsComSql.Fields("RetB").Value
                dblTotInProd = dblTotInProd + (rsComSql.Fields("SumOfPktPcs").Value - (rsComSql.Fields("TotEffects").Value + rsComSql.Fields("RetPcsT18").Value))
                dblTotInFinish = dblTotInFinish + (rsComSql.Fields("RetB").Value)
                dblTotExport = dblTotExport + (rsComSql.Fields("FinishedPcs").Value)
                dblTotExportE = dblTotExportE + (rsComSql.Fields("ExportPcs").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtTotal.Text = dblTotal
        txtTotInProd.Text = dblTotInProd
        txtTotInFinish.Text = dblTotInFinish
        txtTotExport.Text = dblTotExport
        txtExportPcs.Text = dblTotExportE
        txtShipBalPcs.Text = CDbl(txtOrdPcs.Text) - CDbl(txtExportPcs.Text)
    End Sub

    Private Sub frm_MixShipmentPlan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Priority()
        ClearFields()
        dtpOrdDate.Value = Date.Now
    End Sub

    Private Sub txtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtGroup.Focus()
        End If
    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim intRow As Integer

        If txtOrder.Text = "" Then
            MsgBox("Invalid Order No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtPcs.Text = "" Then
            MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If CDbl(txtPcs.Text) < 0 Then
            MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If CDbl(txtPcs.Text) > CDbl(txtShipBalPcs.Text) Then
            MsgBox("Shipment Plan Pcs Exceeds Balance Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtPcs2.Text = "" Then
            MsgBox("Invalid Pcs 2", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If cmbPriority.Text = "" Then
            MsgBox("Select Priority", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        For intRow = 0 To flxOrder.Rows.Count - 1
            If txtOrder.Text = flxOrder.Item(2, intRow).Value And txtGroup.Text = flxOrder.Item(4, intRow).Value Then
                MsgBox("Already in the list", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        flxOrder.Rows.Add(txtPlanID.Text,
                          Format(dtpFinishDate.Value, "yyyy/MM/dd"),
                          txtOrder.Text,
                          txtPcs.Text,
                          UCase(txtGroup.Text),
                          "I",
                          "",
                          cmbPriority.Text,
                          Format(dtpFinishDate.Value, "yyyy/MM/dd"),
                          txtSubject.Text,
                          txtDueDate.Text,
                          txtPcs2.Text)

        txtPlanPcs.Text = CalTotalPcs(flxOrder)
        txtOrder.Focus()
    End Sub

    Private Sub DataSave()
        Dim intRow As Integer

        For intRow = 0 To flxOrder.Rows.Count - 1
            If Not IsDate(flxOrder.Item(8, intRow).Value) Then
                MsgBox("Invalid Finish Date - " & flxOrder.Item(2, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblPlaneOrders WHERE OrderDate = '" & Format(dtpOrdDate.Value, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            For intRow = 0 To flxOrder.Rows.Count - 1
                AdoCN.Execute("INSERT INTO tblPlaneOrders(OrderID,OrderDate,OrderNo,Pieces,Remarks,Status,ModifyBy,SysDateTime,Grp,DoneBy,Priority,FinishDate,Pieces1) " & _
                              "VALUES(" & CDbl(flxOrder.Item(0, intRow).Value) & ",'" & Format(dtpOrdDate.Value, "MM/dd/yyyy") & "','" & flxOrder.Item(2, intRow).Value & "'," & _
                                "" & CDbl(flxOrder.Item(3, intRow).Value) & ",'" & flxOrder.Item(6, intRow).Value & "','I','" & PBUser_EmpNo & "'," & _
                                "'" & Format(Date.Now, "MM/dd/yyyy") & "','','" & PBUser_EmpNo & "','" & flxOrder.Item(7, intRow).Value & "'," & _
                                "'" & Format(CDate(flxOrder.Item(8, intRow).Value), "MM/dd/yyyy") & "'," & CDbl(flxOrder.Item(11, intRow).Value) & ")")
            Next
        Else
            If flxOrder.Rows.Count < 1 Then
                MsgBox("No Records to Save", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            PBResponse = MsgBox("Already Exists. Are you want to update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then

                AdoCN.Execute("DELETE FROM tblPlaneOrders WHERE OrderDate = '" & Format(dtpOrdDate.Value, "MM/dd/yyyy") & "'")
                For intRow = 0 To flxOrder.Rows.Count - 1
                    AdoCN.Execute("INSERT INTO tblPlaneOrders(OrderID,OrderDate,OrderNo,Pieces,Remarks,Status,ModifyBy,SysDateTime,Grp,DoneBy,Priority,FinishDate,Pieces1) " & _
                                  "VALUES(" & CDbl(flxOrder.Item(0, intRow).Value) & ",'" & Format(dtpOrdDate.Value, "MM/dd/yyyy") & "','" & flxOrder.Item(2, intRow).Value & "'," & _
                                    "" & CDbl(flxOrder.Item(3, intRow).Value) & ",'" & flxOrder.Item(6, intRow).Value & "','I','" & PBUser_EmpNo & "'," & _
                                    "'" & Format(Date.Now, "MM/dd/yyyy") & "','','" & PBUser_EmpNo & "','" & flxOrder.Item(7, intRow).Value & "'," & _
                                    "'" & Format(CDate(flxOrder.Item(8, intRow).Value), "MM/dd/yyyy") & "'," & CDbl(flxOrder.Item(11, intRow).Value) & ")")

                Next
            End If
        End If
        MsgBox("Shipment Plan Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        ClearFields()
    End Sub

    Private Sub Delete()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblPlaneOrders WHERE OrderDate = '" & Format(dtpOrdDate.Value, "MM/dd/yyyy") & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            PBResponse = MsgBox("Already Exists. Are you want to Delete?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                AdoCN.Execute("DELETE FROM tblPlaneOrders WHERE OrderDate = '" & Format(dtpOrdDate.Value, "MM/dd/yyyy") & "'")
            End If
        End If
        MsgBox("Shipment Plan Deleted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        ClearFields()
    End Sub

    Private Sub flxOrder_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxOrder.CellClick
        If flxOrder.Rows.Count > 0 Then
            txtOrder.Text = flxOrder.Item(2, flxOrder.CurrentRow.Index).Value
            txtPcs.Text = flxOrder.Item(3, flxOrder.CurrentRow.Index).Value
            txtGroup.Text = flxOrder.Item(4, flxOrder.CurrentRow.Index).Value
            cmbPriority.Text = flxOrder.Item(7, flxOrder.CurrentRow.Index).Value
            dtpFinishDate.Value = flxOrder.Item(8, flxOrder.CurrentRow.Index).Value
            txtPcs2.Text = flxOrder.Item(11, flxOrder.CurrentRow.Index).Value

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblOrders WHERE OrderNo = " & txtOrder.Text & "", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                txtClient.Text = rsComSql.Fields("Niruref").Value
                txtDueDate.Text = Format(rsComSql.Fields("DueDate").Value, "dd/MM/yyyy")
                txtSubject.Text = rsComSql.Fields("Subject").Value

                txtPcs.Focus()
            Else
                txtClient.Text = ""
                txtDueDate.Text = ""
                txtSubject.Text = ""
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub flxOrder_DoubleClick(sender As Object, e As EventArgs) Handles flxOrder.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxOrder.Rows.RemoveAt(flxOrder.CurrentRow.Index)
            txtPlanPcs.Text = CalTotalPcs(flxOrder)
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        DataSave()
    End Sub

    Private Sub txtGroup_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtGroup.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbPriority.Focus()
        End If
    End Sub

    Private Sub cmbPriority_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbPriority.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmdAdd.Focus()
        End If
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxOrder)
    End Sub

    Private Sub cmdOpen_Click(sender As Object, e As EventArgs) Handles cmdOpen.Click
        flxDetails.Rows.Clear()
        OpenFileDialog1.Filter = "All Excel Files|*.xls;*.xlsx"
        OpenFileDialog1.InitialDirectory = "C:\"
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName <> "" Then
            txtFilePath.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Load_Excel()
        On Error GoTo ErrorHandler

        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim intRow, m_LotNo As Integer

        If txtFilePath.Text = "" Then
            MsgBox("Please select the Excel File", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If Len(Dir(txtFilePath.Text)) > 0 Then

            xlApp = New Excel.Application
            xlWorkBook = xlApp.Workbooks.Open(txtFilePath.Text)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            flxOrder.Rows.Clear()

            For intRow = 2 To 10000
                If Len(xlWorkSheet.Cells(intRow, 1).Value) = 0 Then Exit For
                flxOrder.Rows.Add(Trim(txtPlanID.Text), Format(dtpOrdDate.Value, "yyyy/MM/dd"),
                                  Trim(xlWorkSheet.Cells(intRow, 2).Value), Trim(xlWorkSheet.Cells(intRow, 3).Value), "",
                                  Trim(xlWorkSheet.Cells(intRow, 5).Value), Trim(xlWorkSheet.Cells(intRow, 4).Value),
                                  Trim(xlWorkSheet.Cells(intRow, 10).Value), Format(dtpFinishDate.Value, "yyyy/MM/dd"),
                                  "", Format(dtpFinishDate.Value, "yyyy/MM/dd"), Trim(xlWorkSheet.Cells(intRow, 12).Value))

            Next
            xlWorkSheet = Nothing
            xlWorkBook.Close()
            xlApp.Quit()

            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)

            MsgBox("Order Detail Loaded", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        Exit Sub
ErrorHandler:
        releaseObject(xlApp)
        releaseObject(xlWorkBook)
        releaseObject(xlWorkSheet)
        MsgBox(Err.Description, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
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

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Load_Excel()
    End Sub

    Private Sub txtPcs2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs2.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Delete()
    End Sub
End Class