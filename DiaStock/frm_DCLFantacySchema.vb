
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLFantacySchema
    Private Sub GetDetails(ByVal strExportNo As String)
        Dim intRow As Integer
        Dim strLotID As String
        Dim strItemName As String
        Dim strLotName As String
        Dim strDeptCode As String
        Dim strType As String
        Dim dblTotAsking As Double
        Dim strAdditional As String
        Dim strNOrder As String
        Dim strOrderItem As String
        Dim strReference As String
        Dim dblRghCts As Double
        Dim dblExpPcs As Double
        Dim dblExpCts As Double
        Dim strAssortment As String
        Dim dblNFEValue As Double
        Dim dblLabour As Double
        Dim strCommande As String
        Dim strItemNo As String
        Dim strSubject As String
        Dim strClient As String
        Dim strLength As String
        Dim strWidth As String

        For intRow = 0 To flxDetails.Rows.Count - 1
            If strExportNo = flxDetails.Item(23, intRow).Value Then
                MsgBox("Export No. already entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        If optNFE.Checked = True Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM VW_DCLFantacySchema WHERE ExportNo = '" & strExportNo & "' AND Category <> 'NFE'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                MsgBox("Wrong Category - " & rsComSql.Fields("Category").Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing
        Else
            If optPurchased.Checked = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_DCLFantacySchema WHERE ExportNo = '" & strExportNo & "' AND Category <> 'Purchased'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    MsgBox("Wrong Category - " & rsComSql.Fields("Category").Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing
            Else
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM VW_DCLFantacySchema WHERE ExportNo = '" & strExportNo & "' AND Category <> 'Consignment'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    MsgBox("Wrong Category - " & rsComSql.Fields("Category").Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing
            End If
        End If

        rsComSql = New ADODB.Recordset
        If chkNormal.Checked = True Then
            rsComSql.Open("SELECT * FROM VW_DCLFantacySchema WHERE ExportNo = '" & strExportNo & "' ORDER BY LotID", AdoCN, 1, 1)
        ElseIf chkSum.Checked = True Then
            rsComSql.Open("SELECT * FROM VW_DCLFantacySchemaSum WHERE ExportNo = '" & strExportNo & "' ORDER BY LotID", AdoCN, 1, 1)
        ElseIf chkDetails.Checked = True Then
            rsComSql.Open("SELECT * FROM VW_DCLFantacySchemaDetails WHERE ExportNo = '" & strExportNo & "' ORDER BY LotID", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                strLotID = rsComSql.Fields("LotID").Value

                strAssortment = ""
                strItemNo = ""
                strSubject = rsComSql.Fields("Subject").Value
                strLength = ""
                strWidth = ""

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblPOLStockOut WHERE DocID = '" & rsComSql.Fields("Reference2").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strLotID = rsComSql.Fields("Reference2").Value
                End If
                rsComSql_1 = Nothing

                If rsComSql.Fields("Department").Value = "SizeExports" Then
                    strLotName = rsComSql.Fields("AssortmentNo").Value
                Else
                    strLotName = rsComSql.Fields("LotName").Value
                End If

                If rsComSql.Fields("Department").Value <> "Colombo Niru" And (strRight(rsComSql.Fields("Reference2").Value, 1) = "N" Or strRight(rsComSql.Fields("Reference2").Value, 1) = "V") Then
                    strItemName = "ROUGH"
                Else
                    strItemName = rsComSql.Fields("ItemName").Value
                End If

                strDeptCode = ""
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblDCLDeptCode WHERE ProfitCenter = '" & Mid(rsComSql.Fields("LotName").Value, 1, 1) & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strDeptCode = rsComSql_1.Fields("DeptCode").Value
                End If
                rsComSql_1 = Nothing

                If Mid(rsComSql.Fields("LotName").Value, 1, 3) = "SRW" Then
                    strDeptCode = "102"
                End If

                strType = ""
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblGrading_Pack WHERE PackingListNo = '" & rsComSql.Fields("PackingListNo").Value & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strType = rsComSql_1.Fields("Type").Value
                End If
                rsComSql_1 = Nothing

                dblTotAsking = 0
                If strType = "P" Or strType = "S" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT SUM(dbo.tblGrading_PackingList.ActCts * dbo.tblGrading_PackingList.Price) AS AskingValue " & _
                                    "FROM dbo.tblGrading_Pack INNER JOIN dbo.tblGrading_PackingList ON dbo.tblGrading_Pack.Department = dbo.tblGrading_PackingList.Department AND " & _
                                        "dbo.tblGrading_Pack.PackNo = dbo.tblGrading_PackingList.PackNo And dbo.tblGrading_Pack.ParNo = dbo.tblGrading_PackingList.ParNo " & _
                                    "WHERE (dbo.tblGrading_Pack.ParNo LIKE '" & rsComSql.Fields("Reference2").Value & "' + '%') AND (dbo.tblGrading_Pack.PackingListNo = '" & rsComSql.Fields("PackingListNo").Value & "') AND " & _
                                        "(dbo.tblGrading_Pack.Department = '" & rsComSql.Fields("Department").Value & "')", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        If Not IsDBNull(rsComSql_1.Fields("AskingValue").Value) Then
                            dblTotAsking = rsComSql_1.Fields("AskingValue").Value
                        End If
                    End If
                    rsComSql_1 = Nothing

                ElseIf strType = "O" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT dbo.tblNoneOrdersDtls.Assortment, dbo.tblNoneOrders.ItemName, dbo.tblNoneOrdersDtls.Length, dbo.tblNoneOrdersDtls.Width " & _
                                    "FROM dbo.tblNoneOrders INNER JOIN dbo.tblNoneOrdersDtls ON dbo.tblNoneOrders.OrderNo = dbo.tblNoneOrdersDtls.OrderNo " & _
                                    "WHERE (dbo.tblNoneOrders.OrderNo = '" & rsComSql.Fields("Reference1").Value & "') AND (dbo.tblNoneOrdersDtls.RefNo = '" & rsComSql.Fields("OrderRefrence").Value & "')", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        strAssortment = rsComSql_1.Fields("Assortment").Value
                        strItemNo = rsComSql_1.Fields("ItemName").Value
                        strLength = rsComSql_1.Fields("Length").Value
                        strWidth = rsComSql_1.Fields("Width").Value
                    End If
                    rsComSql_1 = Nothing

                    dblTotAsking = Math.Round(rsComSql.Fields("NFEValue").Value + rsComSql.Fields("Labour").Value, 2)

                ElseIf strType = "C" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT SUM(dbo.tblExpReExports.Cts * dbo.tblExpReExports.BasePrice) AS AskingValue " & _
                                    "FROM dbo.tblGrading_Pack INNER JOIN dbo.tblExpReExports ON dbo.tblGrading_Pack.Department = dbo.tblExpReExports.Department AND dbo.tblGrading_Pack.ParNo = dbo.tblExpReExports.ParNo " & _
                                    "WHERE (dbo.tblGrading_Pack.PackingListNo = '" & rsComSql.Fields("PackingListNo").Value & "') AND (dbo.tblGrading_Pack.ParNo = '" & rsComSql.Fields("Reference2").Value & "') AND " & _
                                        "(dbo.tblExpReExports.Assortment = '" & rsComSql.Fields("LotName").Value & "')", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        If Not IsDBNull(rsComSql_1.Fields("AskingValue").Value) Then
                            dblTotAsking = rsComSql_1.Fields("AskingValue").Value
                        End If
                    End If
                    rsComSql_1 = Nothing

                ElseIf strType = "L" Then
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT ROUND(SUM(dbo.tblGrading_PackingListCOL.Cts * dbo.tblGrading_PackingListCOL.Price), 2) As AskingValue " & _
                                    "FROM dbo.tblGrading_PackingListCOL INNER JOIN dbo.tblImport ON dbo.tblGrading_PackingListCOL.ParNo = dbo.tblImport.SupParcelNo " & _
                                    "WHERE (dbo.tblGrading_PackingListCOL.Analyze = 0) AND (dbo.tblImport.LotNo = '" & strLotID & "') AND (dbo.tblImport.SupParcelNo = '" & rsComSql.Fields("Reference1").Value & "')", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        If Not IsDBNull(rsComSql_1.Fields("AskingValue").Value) Then
                            dblTotAsking = rsComSql_1.Fields("AskingValue").Value
                        End If
                    End If
                    rsComSql_1 = Nothing

                ElseIf strType = "J" Then
                    dblTotAsking = Math.Round(rsComSql.Fields("NFEValue").Value, 2)
                End If

                If rsComSql.Fields("Department").Value = "Precision" Then
                    dblTotAsking = Math.Round(rsComSql.Fields("NFEValue").Value + rsComSql.Fields("Labour").Value, 2)
                End If

                If dblTotAsking = 0 Then
                    dblTotAsking = Math.Round(rsComSql.Fields("NFEValue").Value, 2)
                End If

                strAdditional = ""
                If rsComSql.Fields("Labour").Value > 0 Then
                    strAdditional = "LABOR"
                End If

                strNOrder = rsComSql.Fields("NOrderNo").Value
                If Val(rsComSql.Fields("NOrderNo").Value) = 0 Then
                    strNOrder = ""
                End If

                strOrderItem = rsComSql.Fields("OrderItem").Value
                If Val(rsComSql.Fields("OrderItem").Value) = 0 Then
                    strOrderItem = ""
                End If

                If rsComSql.Fields("Department").Value = "ProcessReject" And Mid(strLotName, 1, 2) = "AR" Then
                    strItemName = "ROUGH"
                    strDeptCode = "102"
                End If

                If strItemName = "ROUGH" And rsComSql.Fields("Department").Value <> "ProcessReject" Then
                    strDeptCode = "124"
                End If

                strReference = rsComSql.Fields("Company").Value & "/EXP/" & Format(Date.Now, "yyyy") & "/" & rsComSql.Fields("ExpInvNo").Value & " - " & rsComSql.Fields("PackingListNo").Value & " - " & rsComSql.Fields("PackingType").Value

                dblLabour = rsComSql.Fields("Labour").Value
                dblNFEValue = rsComSql.Fields("NFEValue").Value
                dblRghCts = Math.Round(rsComSql.Fields("RoughCts").Value, 3)
                dblExpPcs = rsComSql.Fields("ExportPcs").Value
                dblExpCts = Math.Round(rsComSql.Fields("ExportCts").Value, 3)
                strCommande = rsComSql.Fields("Commande").Value

                If chkDetails.Checked = True Then
                    dblExpPcs = rsComSql.Fields("ActPcs").Value
                    dblExpCts = Math.Round(rsComSql.Fields("ActCts").Value, 3)
                    dblRghCts = dblExpCts / (rsComSql.Fields("ExportCts").Value / rsComSql.Fields("RoughCts").Value)
                    strAssortment = rsComSql.Fields("Assortment").Value
                    dblNFEValue = dblRghCts * rsComSql.Fields("Price").Value
                    dblLabour = (rsComSql.Fields("Labour").Value / rsComSql.Fields("RoughCts").Value) * dblRghCts
                End If

                strClient = "21768"
                If rsComSql.Fields("CompCode").Value = "DCL" Then
                    strClient = "21768"
                Else
                    If rsComSql.Fields("CompCode").Value = "NLE" Then
                        strClient = "7"
                    End If
                End If

                flxDetails.Rows.Add(strLotID,
                                    strLotID,
                                    strDeptCode,
                                    rsComSql.Fields("Location").Value,
                                    strItemName,
                                    strLotName,
                                    dblExpPcs,
                                    Math.Round(dblRghCts, 3),
                                    Math.Round(dblExpCts, 3),
                                    Math.Round(dblNFEValue, 2),
                                    Math.Round(dblTotAsking, 2),
                                    strAdditional,
                                    Math.Round(dblLabour, 2), "",
                                    strCommande,
                                    strReference, "ACTIVE",
                                    Math.Round(dblNFEValue + dblLabour, 2),
                                    strClient, "402",
                                    strNOrder,
                                    strOrderItem,
                                    rsComSql.Fields("Category").Value,
                                    rsComSql.Fields("ExportNo").Value,
                                    strAssortment,
                                    strItemNo,
                                    strSubject,
                                    rsComSql.Fields("SupInvoiceNo").Value,
                                    strLength,
                                    strWidth,
                                    rsComSql.Fields("Reference1").Value,
                                    rsComSql.Fields("OrderRefrence").Value)


                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub txtExportNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtExportNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            GetDetails(Trim(txtExportNo.Text))
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        flxDetails.Rows.Clear()
        txtExportNo.Text = ""
        txtExportNo.Focus()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub chkNormal_CheckedChanged(sender As Object, e As EventArgs) Handles chkNormal.CheckedChanged
        If chkNormal.Checked = True Then
            chkSum.Checked = False
            chkDetails.Checked = False
        End If
    End Sub

    Private Sub chkSum_CheckedChanged(sender As Object, e As EventArgs) Handles chkSum.CheckedChanged
        If chkSum.Checked = True Then
            chkNormal.Checked = False
            chkDetails.Checked = False
        End If
    End Sub

    Private Sub chkDetails_CheckedChanged(sender As Object, e As EventArgs) Handles chkDetails.CheckedChanged
        If chkDetails.Checked = True Then
            chkNormal.Checked = False
            chkSum.Checked = False
        End If
    End Sub

    Private Sub frm_DCLFantacySchema_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub
End Class