
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLFantacySalesSchema
    Private Sub GetDetails(ByVal strExportNo As String)
        Dim intRow As Integer
        Dim strLotID As String
        Dim strItemName As String
        Dim strLotName As String
        Dim strDeptCode As String
        Dim intProfitCentre As Integer
        Dim strType As String
        Dim dblTotAsking As Double
        Dim strReference As String
        Dim dblRghCts As Double
        Dim dblExpPcs As Double
        Dim dblExpCts As Double
        Dim strAssortment As String
        Dim dblNFEValue As Double
        Dim dblLabour As Double
        Dim strCommande As String
        Dim strItemNo As String
        Dim strShape As String
        Dim strColor As String
        Dim strClarity As String
        Dim dblListPrice As Double
        Dim intCounter As Integer
        Dim dblTotalCost As Double

        For intRow = 0 To flxDetails.Rows.Count - 1
            If strExportNo = flxDetails.Item(25, intRow).Value Then
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
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM VW_DCLFantacySchema WHERE ExportNo = '" & strExportNo & "' AND Category <> 'Purchased'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                MsgBox("Wrong Category - " & rsComSql.Fields("Category").Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing
        End If

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM VW_DCLFantacySchema WHERE ExportNo = '" & strExportNo & "' ORDER BY LotID", AdoCN, 1, 1)
        If rsComSql.RecordCount Then

            ExpProgress.Minimum = 0
            ExpProgress.Visible = True
            ExpProgress.Maximum = rsComSql.RecordCount
            intCounter = 0

            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                intCounter = intCounter + 1
                strLotID = rsComSql.Fields("LotID").Value

                strAssortment = ""
                strItemNo = ""
                intProfitCentre = 0
                strShape = ""
                strColor = ""
                strClarity = ""
                dblListPrice = 0
                dblTotalCost = 0

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
                'If strType = "P" Or strType = "S" Then
                '    rsComSql_1 = New ADODB.Recordset
                '    rsComSql_1.Open("SELECT SUM(dbo.tblGrading_PackingList.ActCts * dbo.tblGrading_PackingList.Price) AS AskingValue " & _
                '                    "FROM dbo.tblGrading_Pack INNER JOIN dbo.tblGrading_PackingList ON dbo.tblGrading_Pack.Department = dbo.tblGrading_PackingList.Department AND " & _
                '                        "dbo.tblGrading_Pack.PackNo = dbo.tblGrading_PackingList.PackNo And dbo.tblGrading_Pack.ParNo = dbo.tblGrading_PackingList.ParNo " & _
                '                    "WHERE (dbo.tblGrading_Pack.ParNo LIKE '" & rsComSql.Fields("Reference2").Value & "' + '%') AND (dbo.tblGrading_Pack.PackingListNo = '" & rsComSql.Fields("PackingListNo").Value & "') AND " & _
                '                        "(dbo.tblGrading_Pack.Department = '" & rsComSql.Fields("Department").Value & "')", AdoCN, 1, 1)
                '    If rsComSql_1.RecordCount Then
                '        If Not IsDBNull(rsComSql_1.Fields("AskingValue").Value) Then
                '            dblTotAsking = rsComSql_1.Fields("AskingValue").Value
                '        End If
                '    End If
                '    rsComSql_1 = Nothing

                'ElseIf strType = "O" Then
                '    rsComSql_1 = New ADODB.Recordset
                '    rsComSql_1.Open("SELECT * FROM tblNoneOrders WHERE NorderNo = '" & rsComSql.Fields("NOrderNo").Value & "' AND OrderItem = '" & rsComSql.Fields("OrderItem").Value & "'", AdoCN, 1, 1)
                '    If rsComSql_1.RecordCount Then
                '        strAssortment = rsComSql_1.Fields("Assortment").Value
                '        strItemNo = rsComSql_1.Fields("ItemName").Value
                '    End If
                '    rsComSql_1 = Nothing

                '    dblTotAsking = Math.Round(rsComSql.Fields("NFEValue").Value + rsComSql.Fields("Labour").Value, 2)

                'ElseIf strType = "C" Then
                '    rsComSql_1 = New ADODB.Recordset
                '    rsComSql_1.Open("SELECT SUM(dbo.tblExpReExports.Cts * dbo.tblExpReExports.BasePrice) AS AskingValue " & _
                '                    "FROM dbo.tblGrading_Pack INNER JOIN dbo.tblExpReExports ON dbo.tblGrading_Pack.Department = dbo.tblExpReExports.Department AND dbo.tblGrading_Pack.ParNo = dbo.tblExpReExports.ParNo " & _
                '                    "WHERE (dbo.tblGrading_Pack.PackingListNo = '" & rsComSql.Fields("PackingListNo").Value & "') AND (dbo.tblGrading_Pack.ParNo = '" & rsComSql.Fields("Reference2").Value & "') AND " & _
                '                        "(dbo.tblExpReExports.Assortment = '" & rsComSql.Fields("LotName").Value & "')", AdoCN, 1, 1)
                '    If rsComSql_1.RecordCount Then
                '        If Not IsDBNull(rsComSql_1.Fields("AskingValue").Value) Then
                '            dblTotAsking = rsComSql_1.Fields("AskingValue").Value
                '        End If
                '    End If
                '    rsComSql_1 = Nothing

                'ElseIf strType = "L" Then
                '    rsComSql_1 = New ADODB.Recordset
                '    rsComSql_1.Open("SELECT ROUND(SUM(dbo.tblGrading_PackingListCOL.Cts * dbo.tblGrading_PackingListCOL.Price), 2) As AskingValue " & _
                '                    "FROM dbo.tblGrading_PackingListCOL INNER JOIN dbo.tblImport ON dbo.tblGrading_PackingListCOL.ParNo = dbo.tblImport.SupParcelNo " & _
                '                    "WHERE (dbo.tblGrading_PackingListCOL.Analyze = 0) AND (dbo.tblImport.LotNo = '" & strLotID & "') AND (dbo.tblImport.SupParcelNo = '" & rsComSql.Fields("Reference1").Value & "')", AdoCN, 1, 1)
                '    If rsComSql_1.RecordCount Then
                '        If Not IsDBNull(rsComSql_1.Fields("AskingValue").Value) Then
                '            dblTotAsking = rsComSql_1.Fields("AskingValue").Value
                '        End If
                '    End If
                '    rsComSql_1 = Nothing

                'ElseIf strType = "J" Then
                '    dblTotAsking = Math.Round(rsComSql.Fields("NFEValue").Value, 2)
                'End If

                'If rsComSql.Fields("Department").Value = "Precision" Then
                '    dblTotAsking = Math.Round(rsComSql.Fields("NFEValue").Value + rsComSql.Fields("Labour").Value, 2)
                'End If

                'If dblTotAsking = 0 Then
                '    dblTotAsking = Math.Round(rsComSql.Fields("NFEValue").Value, 2)
                'End If

                'strAdditional = ""
                'If rsComSql.Fields("Labour").Value > 0 Then
                '    strAdditional = "LABOR"
                'End If

                'strNOrder = rsComSql.Fields("NOrderNo").Value
                'If Val(rsComSql.Fields("NOrderNo").Value) = 0 Then
                '    strNOrder = ""
                'End If

                'strOrderItem = rsComSql.Fields("OrderItem").Value
                'If Val(rsComSql.Fields("OrderItem").Value) = 0 Then
                '    strOrderItem = ""
                'End If

                If rsComSql.Fields("Department").Value = "ProcessReject" And Mid(strLotName, 1, 2) = "AR" Then
                    strItemName = "ROUGH"
                    strDeptCode = "102"
                End If

                If strItemName = "ROUGH" And rsComSql.Fields("Department").Value <> "ProcessReject" Then
                    strDeptCode = "124"
                End If

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & strLotName & "'", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    strShape = rsComSql_1.Fields("Shape").Value
                    strColor = rsComSql_1.Fields("Color").Value
                    strClarity = rsComSql_1.Fields("Clarity").Value
                    dblListPrice = rsComSql_1.Fields("ListCost").Value
                End If
                rsComSql_1 = Nothing

                strReference = rsComSql.Fields("Company").Value & "/EXP/" & Format(Date.Now, "yyyy") & "/" & rsComSql.Fields("ExpInvNo").Value & " - " & rsComSql.Fields("PackingListNo").Value & " - " & rsComSql.Fields("PackingType").Value

                intProfitCentre = CInt(strRight(strDeptCode, 2))
                dblLabour = rsComSql.Fields("Labour").Value
                dblNFEValue = rsComSql.Fields("NFEValue").Value
                dblRghCts = Math.Round(rsComSql.Fields("RoughCts").Value, 3)
                dblExpPcs = rsComSql.Fields("ExportPcs").Value
                dblExpCts = Math.Round(rsComSql.Fields("ExportCts").Value, 3)
                strCommande = rsComSql.Fields("Commande").Value

                If chkCost.Checked = True Then
                    dblTotalCost = dblNFEValue + dblLabour
                Else
                    dblTotalCost = dblListPrice * dblExpCts
                End If

                flxDetails.Rows.Add(strLotID,
                                    strDeptCode,
                                    strLotName,
                                    intProfitCentre,
                                    "W", "P",
                                    strLotName,
                                    dblExpPcs,
                                    Math.Round(dblExpCts, 3),
                                    strShape,
                                    strColor,
                                    strClarity,
                                    Math.Round(dblTotalCost, 2),
                                    Math.Round(dblTotalCost, 2),
                                    Math.Round(dblTotalCost, 2),
                                    "0.00", "0.00", "", "",
                                    strReference, "", "0", "", "", "",
                                    Math.Round(dblListPrice, 2), "", "", "", "",
                                    strExportNo)


                rsComSql.MoveNext()
                ExpProgress.Value = intCounter
            End While
        End If
        rsComSql = Nothing
        ExpProgress.Visible = False
        ExpProgress.Value = 0
    End Sub

    Private Sub txtExportNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtExportNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            GetDetails(Trim(txtExportNo.Text))
        End If
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub frm_DCLFantacySalesSchema_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub
End Class