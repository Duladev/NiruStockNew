
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLLineNoSearch

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub txtLotID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLotID.KeyPress
        Dim intRow As Integer

        If Asc(e.KeyChar) = 13 Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If txtLotID.Text = flxDetails.Item(0, intRow).Value Then
                    MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Next

            Get_LotDetails()

            txtLotID.Text = ""
            txtLotID.Focus()
        End If
    End Sub

    Private Sub Get_LotDetails()
        Dim blnFound As Boolean
        Dim strNOrderNo As String
        Dim strOrderItem As String
        Dim strOrderNo As String
       
        Dim dtpExportDate As Date
        Dim strEquipment As String

        blnFound = False
        strNOrderNo = Mid(txtLotID.Text, 5, 9)
        strOrderItem = Mid(txtLotID.Text, 14, 1)
        strOrderNo = ""

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT Reference1, DateCreated FROM dbo.tblCosting WHERE (NOrderNo = '" & strNOrderNo & "') AND (OrderItem = '" & strOrderItem & "') GROUP BY Reference1, DateCreated", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            blnFound = True
            strOrderNo = rsComSql.Fields("Reference1").Value
            dtpExportDate = rsComSql.Fields("DateCreated").Value
        End If
        rsComSql = Nothing

        If blnFound = True Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT TOP (100) PERCENT OrderNo, PacketNo, Assortment, FinishedPcs, FinishedCts, COMMANDE, NLineNo, SubJect " & _
                          "FROM dbo.VW_MixExportOriginOrders WHERE (OrderNo = '" & strOrderNo & "') AND (RetDate = '" & Format(dtpExportDate, "MM/dd/yyyy") & "') GROUP BY PacketNo, OrderNo, Assortment, FinishedPcs, FinishedCts, COMMANDE, NLineNo, SubJect ORDER BY PacketNo", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT * FROM VW_MixReturns16Details WHERE (SupParNo = '" & strOrderNo & "') AND (PktNo = '" & rsComSql.Fields("PacketNo").Value & "')", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        strEquipment = ""
                        rsComSql_2 = New ADODB.Recordset
                        rsComSql_2.Open("SELECT * FROM tblAMSLabExcel WHERE SupParNo = '" & strOrderNo & "' AND PktNo = '" & rsComSql.Fields("PacketNo").Value & "'", AdoCN, 1, 1)
                        If rsComSql_2.RecordCount Then
                            strEquipment = rsComSql_2.Fields("Equipment").Value
                        End If
                        rsComSql_2 = Nothing

                        flxDetails.Rows.Add(txtLotID.Text,
                                            rsComSql.Fields("Assortment").Value,
                                            rsComSql.Fields("FinishedPcs").Value,
                                            Math.Round(rsComSql.Fields("FinishedCts").Value, 3),
                                            rsComSql.Fields("COMMANDE").Value,
                                            rsComSql.Fields("SubJect").Value,
                                            rsComSql.Fields("NLineNo").Value,
                                            Format(dtpExportDate, "yyyy-MM-dd"),
                                            rsComSql.Fields("OrderNo").Value,
                                            rsComSql.Fields("PacketNo").Value,
                                            Format(rsComSql_1.Fields("ScreeningDate").Value, "yyyy-MM-dd"),
                                            rsComSql_1.Fields("Controller").Value,
                                            Format(rsComSql_1.Fields("StartTime").Value, "HH:mm:ss"),
                                            Format(rsComSql_1.Fields("EndTime").Value, "HH:mm:ss"),
                                            "ID100",
                                            rsComSql_1.Fields("Pass").Value,
                                            rsComSql_1.Fields("Refer1").Value,
                                            strEquipment,
                                            rsComSql_1.Fields("Refer1").Value,
                                            rsComSql.Fields("FinishedPcs").Value, "0", "0")
                    End If
                    rsComSql_1 = Nothing

                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        txtLotID.Text = ""
        txtParNo.Text = ""
        txtPktNo.Text = ""
        flxDetails.Rows.Clear()
        flxOrigin.Rows.Clear()
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        txtParNo.Text = flxDetails.Item(8, flxDetails.CurrentRow.Index).Value
        txtPktNo.Text = flxDetails.Item(9, flxDetails.CurrentRow.Index).Value

        Get_PacketDetails()
    End Sub

    Private Sub Get_PacketDetails()
        Dim strOrigin As String
        Dim strOriCode As String
        Dim strAssortment As String

        flxOrigin.Rows.Clear()
        strOrigin = ""
        strOriCode = ""
        strAssortment = ""

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM VW_MixExportOriginOrders WHERE (OrderNo = '" & txtParNo.Text & "') AND (PacketNo = '" & txtPktNo.Text & "')", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                strOrigin = rsComSql.Fields("Origin").Value
                strOriCode = rsComSql.Fields("OriCode").Value
                strAssortment = rsComSql.Fields("AssortmentNo").Value

                If strOrigin = "Impex Polish" Then
                    strOrigin = "Alrosa"
                    strOriCode = "320"
                ElseIf strOrigin = "Market Polished" Then
                    strOrigin = "De Beers"
                    strOriCode = "310"
                ElseIf strOrigin = "Niru Polish" Then
                    strOrigin = "De Beers"
                    strOriCode = "310"
                ElseIf strOrigin = "Sierra Leone" Then
                    strOrigin = "Auction Mix"
                    strOriCode = "330"
                End If

                flxOrigin.Rows.Add(rsComSql.Fields("BalPcs").Value,
                                   strOrigin,
                                   strOriCode,
                                   strAssortment,
                                   Format(rsComSql.Fields("RecievedDate").Value, "yyyy-MM-dd"),
                                   rsComSql.Fields("SupParNo").Value,
                                   rsComSql.Fields("LotNo").Value,
                                   rsComSql.Fields("ItemName").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm2 = New frm_DCLReportViewer2
        mReportName = "crptRghExportDetails.rpt"
        mRecordSelectionFormula = "{VW_RghExportDetails.OrigParcelNo} = '" & txtParNo.Text & "'"
        strReportPath = PBReportPath & "Rough\" & mReportName
        objForm2.Show()

        txtParNo.Text = ""
    End Sub

    Private Sub frm_DCLLineNoSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub
End Class