
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frm_LabCertify

    Private Sub frm_LabCertify_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
        Dim objReportDocument As New CrystalDecisions.CrystalReports.Engine.ReportDocument()

        Me.CRViewer1.RefreshReport()
        dtpExpDate.Value = Date.Now
    End Sub

    Private Sub txtPackListNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPackListNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub Create_Certificates(ByVal dblPackNo As Double)
        Dim cryRpt As New ReportDocument
        Dim strPdfPath As String
        Dim intCounter As Integer
        Dim strRootPath As String
        Dim obj_Files As New Scripting.FileSystemObject
        Dim dblExpLotID As Double
        Dim strCommande As String
        Dim strYearMonth As String
        Dim strFolderPath As String
        Dim strFolderPath2 As String
        Dim strFolderPath3 As String

        If txtPackListNo.Text = "" Then MsgBox("Invalid Packing List No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        ExpProgress.Minimum = 0
        ExpProgress.Value = 0
        ExpProgress.Visible = True
        intCounter = 0

        'Generate Lot ID for Lab Certificates
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT PackingListNo, ClientID, Commande, Subject " & _
                      "FROM dbo.tblCosting " & _
                      "WHERE (Department = N'Mix') AND (PackingListNo = '" & dblPackNo & "') AND (ExportLotID = 0) AND (ClientID <> 'NIRU IL') " & _
                      "GROUP BY PackingListNo, Commande, ClientID, Subject " & _
                      "ORDER BY ClientID, Commande, Subject", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT MAX(ExportLotID) AS ExportLotID FROM dbo.tblCosting WHERE (Department = N'Mix') AND (ExportLotID > 0)", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("ExportLotID").Value) Then
                        dblExpLotID = rsComSql_1.Fields("ExportLotID").Value + 1
                    Else
                        dblExpLotID = 51200000
                    End If
                End If
                rsComSql_1 = Nothing

                AdoCN.Execute("UPDATE tblCosting SET ExportLotID = " & dblExpLotID & " " & _
                              "WHERE (Department = N'Mix') AND (PackingListNo = '" & dblPackNo & "') AND " & _
                                "(ClientID = '" & rsComSql.Fields("ClientID").Value & "') AND " & _
                                "(Commande = '" & rsComSql.Fields("Commande").Value & "') AND " & _
                                "(Subject = '" & rsComSql.Fields("Subject").Value & "')")

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        'Generate Lot ID for the Schema
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblOrders.NorderNo, dbo.tblOrders.OrderItem " & _
                      "FROM dbo.tblCosting INNER JOIN dbo.tblOrdersDtls ON dbo.tblCosting.NLineNo = dbo.tblOrdersDtls.NLineNo INNER JOIN " & _
                        "dbo.tblOrders ON dbo.tblOrdersDtls.OrderNo = dbo.tblOrders.OrderNo " & _
                      "WHERE (dbo.tblCosting.Department = N'Mix') AND (dbo.tblCosting.PackingListNo = '" & dblPackNo & "') AND (dbo.tblCosting.ExportLotID2 = 0) AND (dbo.tblCosting.ClientID <> 'NIRU IL')  " & _
                      "GROUP BY dbo.tblCosting.Department, dbo.tblOrders.NorderNo, dbo.tblOrders.OrderItem, dbo.tblCosting.PackingListNo " & _
                      "ORDER BY dbo.tblOrders.NorderNo, dbo.tblOrders.OrderItem", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT MAX(ExportLotID2) AS ExportLotID2 FROM dbo.tblCosting WHERE (Department = N'Mix') AND (ExportLotID2 > 0)", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("ExportLotID2").Value) Then
                        dblExpLotID = rsComSql_1.Fields("ExportLotID2").Value + 1
                    Else
                        dblExpLotID = 1
                    End If
                End If
                rsComSql_1 = Nothing

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.tblCosting.NLineNo " & _
                                "FROM dbo.tblCosting INNER JOIN dbo.tblOrdersDtls ON dbo.tblCosting.NLineNo = dbo.tblOrdersDtls.NLineNo INNER JOIN " & _
                                    "dbo.tblOrders ON dbo.tblOrdersDtls.OrderNo = dbo.tblOrders.OrderNo " & _
                                "WHERE (dbo.tblCosting.PackingListNo = '" & dblPackNo & "') AND (dbo.tblCosting.Department = N'Mix') AND (dbo.tblOrders.NorderNo = '" & rsComSql.Fields("NorderNo").Value & "') AND (dbo.tblOrders.OrderItem = '" & rsComSql.Fields("OrderItem").Value & "') " & _
                                "GROUP BY dbo.tblCosting.NLineNo", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    While Not rsComSql_1.EOF
                        AdoCN.Execute("UPDATE tblCosting SET ExportLotID2 = " & dblExpLotID & " " & _
                                      "WHERE (Department = N'Mix') AND (PackingListNo = '" & dblPackNo & "') AND (NLineNo = '" & rsComSql_1.Fields("NLineNo").Value & "')")

                        rsComSql_1.MoveNext()
                    End While
                End If
                rsComSql_1 = Nothing

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        strRootPath = "E:\LAB\"
        strCommande = ""
        strYearMonth = Format(Date.Now, "yyyyMM")
        strFolderPath = strRootPath & strYearMonth

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT PackingListNo, ClientID, Commande " & _
                      "FROM dbo.tblCosting " & _
                      "WHERE (Department = N'Mix') AND (PackingListNo = '" & dblPackNo & "') AND (ClientID <> 'NIRU IL') " & _
                      "GROUP BY PackingListNo, Commande, ClientID " & _
                      "ORDER BY ClientID, Commande", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            ExpProgress.Maximum = rsComSql.RecordCount
            While Not rsComSql.EOF
                intCounter = intCounter + 1

                If obj_Files.FolderExists(strFolderPath) = False Then
                    obj_Files.CreateFolder(strFolderPath)
                End If

                strFolderPath2 = strFolderPath & "\" & rsComSql.Fields("PackingListNo").Value
                If obj_Files.FolderExists(strFolderPath2) = False Then
                    obj_Files.CreateFolder(strFolderPath2)
                End If

                strFolderPath3 = strFolderPath2 & "\" & strRight(rsComSql.Fields("ClientID").Value, 3)
                If obj_Files.FolderExists(strFolderPath3) = False Then
                    obj_Files.CreateFolder(strFolderPath3)
                End If

                strCommande = Replace(rsComSql.Fields("Commande").Value, "/", " ")
                strCommande = Replace(strCommande, "?", " ")
                strCommande = Replace(strCommande, "*", " ")
                strCommande = Replace(strCommande, ":", " ")
                strPdfPath = strFolderPath3 & "\" & rsComSql.Fields("PackingListNo").Value & "_" & strRight(rsComSql.Fields("ClientID").Value, 3) & "_" & strCommande & ".pdf"

                If Len(Dir(strPdfPath)) = 0 Then
                    CRViewer1.Refresh()
                    mReportName = "crptAMSLabExcelExportPcu2021.rpt"

                    cryRpt = New ReportDocument
                    cryRpt.Load(PBReportPath & "GroupNiru\" & mReportName)
                    cryRpt.RecordSelectionFormula = ""
                    cryRpt.RecordSelectionFormula = "{VW_MixLabExcelCertify.PackingListNo} = " & dblPackNo & " AND {VW_MixLabExcelCertify.Commande} = '" & rsComSql.Fields("Commande").Value & "'"
                    CRViewer1.ReportSource = cryRpt
                    CRViewer1.Refresh()

                    Try
                        Dim CrExportOptions As ExportOptions
                        Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions()
                        Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()



                        'Kill(strPdfPath)
                        CrDiskFileDestinationOptions.DiskFileName = strPdfPath
                        CrExportOptions = cryRpt.ExportOptions
                        With CrExportOptions
                            .ExportDestinationType = ExportDestinationType.DiskFile
                            .ExportFormatType = ExportFormatType.PortableDocFormat
                            .DestinationOptions = CrDiskFileDestinationOptions
                            .FormatOptions = CrFormatTypeOptions
                        End With
                        cryRpt.Export()
                    Catch ex As Exception
                        MsgBox(ex.ToString)
                    End Try
                End If

                rsComSql.MoveNext()
                ExpProgress.Value = intCounter
            End While
        Else
            MsgBox("Invalid Packing List No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing
        ExpProgress.Visible = False
        ExpProgress.Value = 0
        txtPackListNo.Text = ""
        txtPackListNo.Focus()
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        If strDBName = "DiaSales" Then
            Create_Certificates(CDbl(txtPackListNo.Text))
            MsgBox("Lab Certificates Exported - " & txtPackListNo.Text, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        Else
            Create_Certificates2(CDbl(txtPackListNo.Text))
            MsgBox("Lab Certificates Exported - " & txtPackListNo.Text, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub dtpExpDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpExpDate.ValueChanged
        Dim intCount As Integer

        flxDetails.Rows.Clear()
        If strDBName = "DiaSales" Then
            intCount = 1
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT TOP (100) PERCENT PackingListNo, ClientID, SUM(ExportPcs) AS ExportPcs " & _
                          "FROM dbo.tblCosting " & _
                          "WHERE (Department = N'Mix') AND (DateCreated = '" & Format(dtpExpDate.Value, "MM/dd/yyyy") & "') AND (ClientID <> 'NIRU IL') " & _
                          "GROUP BY ClientID, PackingListNo " & _
                          "ORDER BY PackingListNo", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    flxDetails.Rows.Add(rsComSql.Fields("PackingListNo").Value,
                                        rsComSql.Fields("ClientID").Value,
                                        rsComSql.Fields("ExportPcs").Value,
                                        intCount)

                    intCount = intCount + 1
                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing
        Else
            intCount = 1
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblCosting.PackingListNo, dbo.tblCosting.ClientID, SUM(dbo.tblCosting.ExportPcs) AS ExportPcs " & _
                          "FROM dbo.tblCosting INNER JOIN dbo.tblNoneOrders ON dbo.tblCosting.Reference1 = dbo.tblNoneOrders.OrderNo " & _
                          "WHERE (dbo.tblCosting.Department = N'GradingPCU_N') AND (dbo.tblCosting.DateCreated = '" & Format(dtpExpDate.Value, "MM/dd/yyyy") & "') AND (dbo.tblCosting.Status = 'E') AND (dbo.tblNoneOrders.Type = 'ROUNDS') " & _
                          "GROUP BY dbo.tblCosting.ClientID, dbo.tblCosting.PackingListNo " & _
                          "ORDER BY dbo.tblCosting.PackingListNo", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    flxDetails.Rows.Add(rsComSql.Fields("PackingListNo").Value,
                                        rsComSql.Fields("ClientID").Value,
                                        rsComSql.Fields("ExportPcs").Value,
                                        intCount)

                    intCount = intCount + 1
                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing
        End If
        
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        txtPackListNo.Text = flxDetails.Item(0, flxDetails.CurrentRow.Index).Value
    End Sub

    Private Sub cmdTest_Click(sender As Object, e As EventArgs) Handles cmdTest.Click
        Dim dblExpLotID As Double

        'Generate Lot ID for the Schema
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblOrders.NorderNo, dbo.tblOrders.OrderItem " & _
                      "FROM dbo.tblCosting INNER JOIN dbo.tblOrdersDtls ON dbo.tblCosting.NLineNo = dbo.tblOrdersDtls.NLineNo INNER JOIN " & _
                        "dbo.tblOrders ON dbo.tblOrdersDtls.OrderNo = dbo.tblOrders.OrderNo " & _
                      "WHERE (dbo.tblCosting.Department = N'Mix') AND (dbo.tblCosting.PackingListNo = '" & CDbl(txtPackListNo.Text) & "') AND (dbo.tblCosting.ExportLotID2 = 0)  " & _
                      "GROUP BY dbo.tblCosting.Department, dbo.tblOrders.NorderNo, dbo.tblOrders.OrderItem, dbo.tblCosting.PackingListNo " & _
                      "ORDER BY dbo.tblOrders.NorderNo, dbo.tblOrders.OrderItem", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT MAX(ExportLotID2) AS ExportLotID2 FROM dbo.tblCosting WHERE (Department = N'Mix') AND (ExportLotID2 > 0)", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("ExportLotID2").Value) Then
                        dblExpLotID = rsComSql_1.Fields("ExportLotID2").Value + 1
                    Else
                        dblExpLotID = 1
                    End If
                End If
                rsComSql_1 = Nothing

                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.tblCosting.NLineNo " & _
                                "FROM dbo.tblCosting INNER JOIN dbo.tblOrdersDtls ON dbo.tblCosting.NLineNo = dbo.tblOrdersDtls.NLineNo INNER JOIN " & _
                                    "dbo.tblOrders ON dbo.tblOrdersDtls.OrderNo = dbo.tblOrders.OrderNo " & _
                                "WHERE (dbo.tblCosting.PackingListNo = '" & CDbl(txtPackListNo.Text) & "') AND (dbo.tblCosting.Department = N'Mix') AND (dbo.tblOrders.NorderNo = '" & rsComSql.Fields("NorderNo").Value & "') AND (dbo.tblOrders.OrderItem = '" & rsComSql.Fields("OrderItem").Value & "') " & _
                                "GROUP BY dbo.tblCosting.NLineNo", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    While Not rsComSql_1.EOF
                        AdoCN.Execute("UPDATE tblCosting SET ExportLotID2 = " & dblExpLotID & " " & _
                                      "WHERE (Department = N'Mix') AND (PackingListNo = '" & CDbl(txtPackListNo.Text) & "') AND (NLineNo = '" & rsComSql_1.Fields("NLineNo").Value & "')")

                        rsComSql_1.MoveNext()
                    End While
                End If
                rsComSql_1 = Nothing

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Create_Certificates2(ByVal dblPackNo As Double)
        Dim cryRpt As New ReportDocument
        Dim strPdfPath As String
        Dim intCounter As Integer
        Dim strRootPath As String
        Dim obj_Files As New Scripting.FileSystemObject
        Dim strCommande As String
        Dim strYearMonth As String
        Dim strFolderPath As String
        Dim strFolderPath2 As String

        ExpProgress.Minimum = 0
        ExpProgress.Value = 0
        ExpProgress.Visible = True
        intCounter = 0

        strRootPath = "E:\LAB2\"
        strCommande = ""
        strYearMonth = Format(Date.Now, "yyyyMM")
        strFolderPath = strRootPath & strYearMonth

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT PackingListNo " & _
                      "FROM dbo.tblCosting " & _
                      "WHERE (Department = N'GradingPCU_N') AND (PackingListNo = '" & dblPackNo & "') " & _
                      "GROUP BY PackingListNo " & _
                      "ORDER BY PackingListNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            ExpProgress.Maximum = rsComSql.RecordCount
            While Not rsComSql.EOF
                intCounter = intCounter + 1

                CRViewer1.Refresh()
                mReportName = "crptPCULabExcelExportPcu2025.rpt"

                cryRpt = New ReportDocument
                cryRpt.Load(PBReportPath & "Precision\" & mReportName)
                cryRpt.RecordSelectionFormula = ""
                cryRpt.RecordSelectionFormula = "{VW_PCULabExcelCertify.PackingListNo} = " & rsComSql.Fields("PackingListNo").Value & ""
                CRViewer1.ReportSource = cryRpt
                CRViewer1.Refresh()

                Try
                    Dim CrExportOptions As ExportOptions
                    Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions()
                    Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()

                    If obj_Files.FolderExists(strFolderPath) = False Then
                        obj_Files.CreateFolder(strFolderPath)
                    End If

                    strFolderPath2 = strFolderPath & "\" & rsComSql.Fields("PackingListNo").Value
                    If obj_Files.FolderExists(strFolderPath2) = False Then
                        obj_Files.CreateFolder(strFolderPath2)
                    End If

                    strPdfPath = strFolderPath2 & "\" & rsComSql.Fields("PackingListNo").Value & ".pdf"
                    If Len(Dir(strPdfPath)) > 0 Then
                        Kill(strPdfPath)
                    End If
                    CrDiskFileDestinationOptions.DiskFileName = strPdfPath
                    CrExportOptions = cryRpt.ExportOptions
                    With CrExportOptions
                        .ExportDestinationType = ExportDestinationType.DiskFile
                        .ExportFormatType = ExportFormatType.PortableDocFormat
                        .DestinationOptions = CrDiskFileDestinationOptions
                        .FormatOptions = CrFormatTypeOptions
                    End With
                    cryRpt.Export()
                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try

                rsComSql.MoveNext()
                ExpProgress.Value = intCounter
            End While
        Else
            MsgBox("Invalid Packing List No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing
        ExpProgress.Visible = False
        ExpProgress.Value = 0
        txtPackListNo.Text = ""
        txtPackListNo.Focus()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        For intRow = 0 To flxDetails.Rows.Count - 1
            If strDBName = "DiaSales" Then
                'Create_Certificates(CDbl(flxDetails.Item(0, intRow).Value))
            Else
                Create_Certificates2(CDbl(flxDetails.Item(0, intRow).Value))
            End If
        Next
        MsgBox("Lab Certificates Exported - " & strDBName, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
    End Sub
End Class