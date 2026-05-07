
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLParcelSummary

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        txtParNo.Text = ""
        flxDetails.Rows.Clear()
    End Sub

    Private Sub Load_ParcelDetails(ByVal strNiruParcelNo As String)
        Dim blnFound As Boolean
        Dim dblIssuePcs As Double
        Dim dblIssueCts As Double
        Dim dblPcs As Double
        Dim dblCts As Double
        Dim dblGrdPcs As Double
        Dim dblGrdCts As Double
        Dim dblGrdPrice As Double
        Dim dblGrdValue As Double
        Dim dblRejPcs As Double
        Dim dblLostPcs As Double
        Dim dblExtraPcs As Double
        Dim dblPCUPcs As Double
        Dim dblPCUCts As Double
        Dim dblPCUCtsR As Double
        Dim dblTrfPcs As Double
        Dim dblTrfCts As Double
        Dim dblTrfPrice As Double
        Dim dblTrfValue As Double
        Dim strParcelNo As String
        Dim strGrpParNo As String
        Dim strAssortment As String
        Dim strDepartment As String
        Dim dblActPcs As Double
        Dim dblActCts As Double
        Dim dblImportValue As Double
        Dim dblImportPrice As Double
        Dim strRateCode As String
        Dim dblSwWtLoss As Double
        Dim intRow As Integer
        Dim dblOrdPcs As Double
        Dim dblOrdCts As Double

        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(0, intRow).Value = strNiruParcelNo Then
                MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT DISTINCT OrigParcelNo FROM tblParcel WHERE (Complete = 0) AND (OrigParcelNo = '" & strNiruParcelNo & "') ORDER BY OrigParcelNo", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                strParcelNo = rsComSql_1.Fields("OrigParcelNo").Value
                strAssortment = ""
                dblActPcs = 0
                dblActCts = 0
                blnFound = False

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblImport WHERE SupParcelNo = '" & strParcelNo & "' AND SupplierRefNo NOT LIKE 'LCL%'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    blnFound = True
                    strAssortment = rsComSql.Fields("AssortmentNo").Value
                    dblActPcs = rsComSql.Fields("ActPcs").Value
                    dblActCts = rsComSql.Fields("INVCts").Value
                    dblActCts = Math.Round(dblActCts, 3)
                    dblImportPrice = rsComSql.Fields("ItemCost").Value
                    dblImportValue = dblActCts * rsComSql.Fields("ItemCost").Value
                End If
                rsComSql = Nothing

                If blnFound = True Then
                    dblPcs = 0
                    dblCts = 0
                    dblGrdPcs = 0
                    dblGrdCts = 0
                    dblGrdPrice = 0
                    dblGrdValue = 0
                    dblRejPcs = 0
                    dblLostPcs = 0
                    dblExtraPcs = 0
                    dblPCUPcs = 0
                    dblPCUCts = 0
                    dblPCUCtsR = 0
                    dblIssuePcs = 0
                    dblIssueCts = 0
                    dblTrfPcs = 0
                    dblTrfCts = 0
                    dblSwWtLoss = 0
                    dblTrfValue = 0
                    dblTrfPrice = 0
                    dblOrdPcs = 0
                    dblOrdCts = 0
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT * FROM tblParcel " & _
                                    "WHERE (OrigParcelNo = '" & strParcelNo & "') AND " & _
                                        "(Depart = 'Baguettes' OR Depart = 'Baguettes2' OR Depart = 'Baguettes3' OR Depart = 'Princess' OR Depart = 'Princess2' OR " & _
                                         "Depart = 'Rounds' OR Depart = 'Rounds3' OR Depart = 'Rounds4' OR " & _
                                         "Depart = 'Rounds5' OR Depart = 'RoundsNLE' OR Depart = 'Emerald' OR Depart = 'Emerald2' OR Depart = 'Emerald3' OR Depart = 'Opening' " & _
                                         "OR Depart = 'Lamour' OR Depart = 'Davinci' OR Depart = 'Carrer' OR Depart = 'Asscher' OR Depart = 'Radiant') AND (Grp <> 'N') ORDER BY Depart, GrpParNo", AdoCN, 1, 1)
                    If rsComSql_2.RecordCount Then
                        rsComSql_2.MoveFirst()
                        While Not rsComSql_2.EOF
                            dblPcs = 0
                            dblCts = 0
                            dblGrdPcs = 0
                            dblGrdCts = 0
                            dblGrdPrice = 0
                            dblGrdValue = 0
                            dblRejPcs = 0
                            dblLostPcs = 0
                            dblExtraPcs = 0
                            dblPCUPcs = 0
                            dblPCUCts = 0
                            dblPCUCtsR = 0
                            dblTrfPcs = 0
                            dblTrfCts = 0
                            dblTrfPrice = 0
                            dblTrfValue = 0
                            strGrpParNo = rsComSql_2.Fields("GrpParNo").Value
                            strDepartment = rsComSql_2.Fields("Depart").Value
                            dblIssuePcs = rsComSql_2.Fields("IssuedPcs").Value
                            dblIssueCts = rsComSql_2.Fields("IssuedCts").Value
                            strRateCode = rsComSql_2.Fields("Charges").Value
                            dblSwWtLoss = rsComSql_2.Fields("RghCts").Value - rsComSql_2.Fields("IssuedCts").Value
                            dblOrdPcs = 0
                            dblOrdCts = 0
                            Select Case strDepartment
                                Case "Baguettes"
                                    rsComSql = New ADODB.Recordset
                                    rsComSql.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktCts), 3) AS PktCts " & _
                                                  "FROM tblBAGPacket " & _
                                                  "WHERE (ParNo = '" & strGrpParNo & "')", AdoCN, 1, 1)
                                    If rsComSql.RecordCount Then
                                        If Not IsDBNull(rsComSql.Fields("PktPcs").Value) Then
                                            dblPcs = rsComSql.Fields("PktPcs").Value
                                            dblCts = rsComSql.Fields("PktCts").Value
                                        End If
                                    End If
                                    rsComSql = Nothing

                                    rsComSql = New ADODB.Recordset
                                    rsComSql.Open("SELECT SUM(RejPcs) AS RejPcs, SUM(LostPcs) AS LostPcs, SUM(ExtPcs) AS ExtPcs, SUM(PCUPcs) AS PCUPcs, ROUND(SUM(PCUCts), 3) AS PCUCts, ROUND(SUM(PCUPCts), 3) AS PCUPCts " & _
                                                  "FROM tblBAGReturns " & _
                                                  "WHERE (ParNo = '" & strGrpParNo & "')", AdoCN, 1, 1)
                                    If rsComSql.RecordCount Then
                                        If Not IsDBNull(rsComSql.Fields("RejPcs").Value) Then
                                            dblRejPcs = rsComSql.Fields("RejPcs").Value
                                            dblLostPcs = rsComSql.Fields("LostPcs").Value
                                            dblExtraPcs = rsComSql.Fields("ExtPcs").Value
                                        End If
                                        If Not IsDBNull(rsComSql.Fields("PcuPcs").Value) Then
                                            dblPCUPcs = rsComSql.Fields("PcuPcs").Value
                                            dblPCUCts = rsComSql.Fields("PCUCts").Value
                                            dblPCUCtsR = rsComSql.Fields("PcuPCts").Value
                                        End If
                                    End If
                                    rsComSql = Nothing

                                Case "Princess"
                                    rsComSql = New ADODB.Recordset
                                    rsComSql.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktCts), 3) AS PktCts " & _
                                                  "FROM tblPRPacket " & _
                                                  "WHERE (ParNo = '" & strGrpParNo & "')", AdoCN, 1, 1)
                                    If rsComSql.RecordCount Then
                                        If Not IsDBNull(rsComSql.Fields("PktPcs").Value) Then
                                            dblPcs = rsComSql.Fields("PktPcs").Value
                                            dblCts = rsComSql.Fields("PktCts").Value
                                        End If
                                    End If
                                    rsComSql = Nothing

                                    rsComSql = New ADODB.Recordset
                                    rsComSql.Open("SELECT SUM(RejPcs) AS RejPcs, SUM(LostPcs) AS LostPcs, SUM(ExtPcs) AS ExtPcs, SUM(PCUPcs) AS PCUPcs, ROUND(SUM(PCUCts), 3) AS PCUCts, ROUND(SUM(PCUPCts), 3) AS PCUPCts " & _
                                                  "FROM tblPRReturns " & _
                                                  "WHERE (ParNo = '" & strGrpParNo & "')", AdoCN, 1, 1)
                                    If rsComSql.RecordCount Then
                                        If Not IsDBNull(rsComSql.Fields("RejPcs").Value) Then
                                            dblRejPcs = rsComSql.Fields("RejPcs").Value
                                            dblLostPcs = rsComSql.Fields("LostPcs").Value
                                            dblExtraPcs = rsComSql.Fields("ExtPcs").Value
                                        End If
                                        If Not IsDBNull(rsComSql.Fields("PcuPcs").Value) Then
                                            dblPCUPcs = rsComSql.Fields("PcuPcs").Value
                                            dblPCUCts = rsComSql.Fields("PCUCts").Value
                                            dblPCUCtsR = rsComSql.Fields("PcuPCts").Value
                                        End If
                                    End If
                                    rsComSql = Nothing

                                Case "Rounds"
                                    rsComSql = New ADODB.Recordset
                                    rsComSql.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktCts), 3) AS PktCts " & _
                                                  "FROM tblRndPacket " & _
                                                  "WHERE (ParNo = '" & strGrpParNo & "')", AdoCN, 1, 1)
                                    If rsComSql.RecordCount Then
                                        If Not IsDBNull(rsComSql.Fields("PktPcs").Value) Then
                                            dblPcs = rsComSql.Fields("PktPcs").Value
                                            dblCts = rsComSql.Fields("PktCts").Value
                                        End If
                                    End If
                                    rsComSql = Nothing

                                    rsComSql = New ADODB.Recordset
                                    rsComSql.Open("SELECT SUM(RejPcs) AS RejPcs, SUM(LostPcs) AS LostPcs, SUM(ExtPcs) AS ExtPcs " & _
                                                  "FROM tblRndReturns " & _
                                                  "WHERE (ParNo = '" & strGrpParNo & "')", AdoCN, 1, 1)
                                    If rsComSql.RecordCount Then
                                        If Not IsDBNull(rsComSql.Fields("RejPcs").Value) Then
                                            dblRejPcs = rsComSql.Fields("RejPcs").Value
                                            dblLostPcs = rsComSql.Fields("LostPcs").Value
                                            dblExtraPcs = rsComSql.Fields("ExtPcs").Value
                                        End If
                                    End If
                                    rsComSql = Nothing

                                Case "Rounds3", "Rounds4", "Rounds5", "Rounds7", "RoundsNLE", "Princess2", "Emerald", "Opening", "Lamour", "Davinci", "Carrer", "Asscher", "Radiant"
                                    rsComSql = New ADODB.Recordset
                                    rsComSql.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktCts), 3) AS PktCts " & _
                                                  "FROM tblExtPacket " & _
                                                  "WHERE (ParNo = '" & strGrpParNo & "') AND (Department = '" & strDepartment & "')", AdoCN, 1, 1)
                                    If rsComSql.RecordCount Then
                                        If Not IsDBNull(rsComSql.Fields("PktPcs").Value) Then
                                            dblPcs = rsComSql.Fields("PktPcs").Value
                                            dblCts = rsComSql.Fields("PktCts").Value
                                        End If
                                    End If
                                    rsComSql = Nothing

                                    rsComSql = New ADODB.Recordset
                                    rsComSql.Open("SELECT SUM(RejPcs) AS RejPcs, SUM(LostPcs) AS LostPcs, SUM(ExtPcs) AS ExtPcs " & _
                                                  "FROM tblExtReturns " & _
                                                  "WHERE (ParNo = '" & strGrpParNo & "') AND (Department = '" & strDepartment & "')", AdoCN, 1, 1)
                                    If rsComSql.RecordCount Then
                                        If Not IsDBNull(rsComSql.Fields("RejPcs").Value) Then
                                            dblRejPcs = rsComSql.Fields("RejPcs").Value
                                            dblLostPcs = rsComSql.Fields("LostPcs").Value
                                            dblExtraPcs = rsComSql.Fields("ExtPcs").Value
                                        End If
                                    End If
                                    rsComSql = Nothing

                                Case "Niru"
                                    rsComSql = New ADODB.Recordset
                                    rsComSql.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktCts), 3) AS PktCts " & _
                                                  "FROM tblNiruPacket " & _
                                                  "WHERE (ParNo = '" & strGrpParNo & "')", AdoCN, 1, 1)
                                    If rsComSql.RecordCount Then
                                        If Not IsDBNull(rsComSql.Fields("PktPcs").Value) Then
                                            dblPcs = rsComSql.Fields("PktPcs").Value
                                            dblCts = rsComSql.Fields("PktCts").Value
                                        End If
                                    End If
                                    rsComSql = Nothing

                                    rsComSql = New ADODB.Recordset
                                    rsComSql.Open("SELECT SUM(RejPcs) AS RejPcs, SUM(LostPcs) AS LostPcs, SUM(ExtPcs) AS ExtPcs " & _
                                                  "FROM tblNiruReturns " & _
                                                  "WHERE (ParNo = '" & strGrpParNo & "')", AdoCN, 1, 1)
                                    If rsComSql.RecordCount Then
                                        If Not IsDBNull(rsComSql.Fields("RejPcs").Value) Then
                                            dblRejPcs = rsComSql.Fields("RejPcs").Value
                                            dblLostPcs = rsComSql.Fields("LostPcs").Value
                                            dblExtraPcs = rsComSql.Fields("ExtPcs").Value
                                        End If
                                    End If
                                    rsComSql = Nothing

                            End Select

                            'Grading Pcs
                            rsComSql = New ADODB.Recordset
                            rsComSql.Open("SELECT SUM(Trf_Pcs) AS Trf_Pcs, SUM((Rgh_Cts / Rgh_Pcs) * Trf_Pcs) AS Trf_Cts " & _
                                          "FROM tblGradingTrf " & _
                                          "WHERE (Department = '" & strDepartment & "') AND (ParcelNo = '" & strGrpParNo & "') AND (Status = 1) AND (Len(PktNo) = 3)", AdoCN, 1, 1)
                            If rsComSql.RecordCount Then
                                If Not IsDBNull(rsComSql.Fields("Trf_Pcs").Value) Then
                                    dblGrdPcs = rsComSql.Fields("Trf_Pcs").Value
                                    dblGrdCts = rsComSql.Fields("Trf_Cts").Value
                                End If
                            End If
                            rsComSql = Nothing

                            'PCU Pcs
                            rsComSql = New ADODB.Recordset
                            rsComSql.Open("SELECT SUM(dbo.tblExpSizingTypes.Pcs) AS RetPcs, ROUND(SUM(dbo.tblExpSizingTypes.Cts), 3) AS RetCts, ROUND(SUM(dbo.tblExpSizingTypes.BasePrice * dbo.tblExpSizingTypes.Cts), 2) AS Value " & _
                                          "FROM dbo.tblExpSizingTypes INNER JOIN dbo.tblAssortList ON dbo.tblExpSizingTypes.ReturnType = dbo.tblAssortList.Assortment " & _
                                          "WHERE (dbo.tblExpSizingTypes.Department = '" & strDepartment & "') AND (dbo.tblExpSizingTypes.ParNo = '" & strGrpParNo & "')", AdoCN, 1, 1)
                            If rsComSql.RecordCount Then
                                If Not IsDBNull(rsComSql.Fields("RetPcs").Value) Then
                                    dblTrfPcs = rsComSql.Fields("RetPcs").Value
                                    dblTrfCts = rsComSql.Fields("RetCts").Value
                                    dblTrfValue = rsComSql.Fields("Value").Value
                                    If dblTrfValue <> 0 Then
                                        dblTrfPrice = dblTrfValue / dblTrfCts
                                    End If
                                End If
                            End If
                            rsComSql = Nothing

                            'Order PCU Pcs
                            rsComSql = New ADODB.Recordset
                            rsComSql.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktCts), 3) AS PktCts " & _
                                          "FROM  dbo.tblPacket " & _
                                          "WHERE (AParNo = '" & rsComSql_2.Fields("ParcelNo").Value & "')", AdoCN, 1, 1)
                            If rsComSql.RecordCount Then
                                If Not IsDBNull(rsComSql.Fields("PktPcs").Value) Then
                                    dblOrdPcs = rsComSql.Fields("PktPcs").Value
                                    dblOrdCts = rsComSql.Fields("PktCts").Value
                                End If
                            End If
                            rsComSql = Nothing

                            'dblGrdValue = dblImportValue - dblTrfValue
                            'If dblGrdValue <> 0 Then
                            '    dblGrdPrice = dblGrdValue / (dblIssueCts - dblTrfCts - dblPCUCts)
                            'End If

                            flxDetails.Rows.Add(strParcelNo, strAssortment,
                                               dblActPcs, dblActCts, dblImportPrice,
                                               strDepartment, strGrpParNo,
                                               dblIssuePcs, Math.Round(dblIssueCts, 3),
                                               Math.Round(dblSwWtLoss, 3),
                                               dblPcs, dblCts,
                                               dblPcs - (dblRejPcs + dblLostPcs - dblExtraPcs + dblPCUPcs + dblGrdPcs),
                                               dblRejPcs, dblLostPcs,
                                               dblExtraPcs, dblPCUPcs, dblPCUCtsR,
                                               dblGrdPcs - dblTrfPcs,
                                               Math.Round(dblIssueCts - dblTrfCts - dblPCUCts, 3),
                                               Math.Round(dblGrdPrice, 2), dblGrdValue,
                                               dblTrfPcs, dblTrfCts,
                                               Math.Round(dblTrfPrice, 2), dblTrfValue,
                                               strRateCode, dblOrdPcs, dblOrdCts,
                                               rsComSql_2.Fields("Complete").Value)

                            rsComSql_2.MoveNext()
                        End While
                    End If
                    rsComSql_2 = Nothing
                End If

                rsComSql_1.MoveNext()
            End While
        End If
        rsComSql_1 = Nothing
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        txtParNo.Text = UCase(txtParNo.Text)
        Load_ParcelDetails(Trim(txtParNo.Text))
        txtParNo.Text = ""
        txtParNo.Focus()
    End Sub

    Private Sub frm_DCLParcelSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub
End Class