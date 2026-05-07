
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_RprUpload

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub ClearFields()
        flxDetails.Rows.Clear()
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        For intRow = 0 To flxDetails.Rows.Count - 1
            If Len(flxDetails.Item(12, intRow).Value) = 0 Then
                If Not IsNumeric(flxDetails.Item(2, intRow).Value) Then
                    MsgBox("Invalid Rgh Cts - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If Not IsNumeric(flxDetails.Item(3, intRow).Value) Then
                    MsgBox("Invalid Fin Cts - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If Not IsNumeric(flxDetails.Item(7, intRow).Value) Then
                    MsgBox("Invalid Plan Value - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If Not IsNumeric(flxDetails.Item(9, intRow).Value) Then
                    MsgBox("Invalid Width - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                If Not IsNumeric(flxDetails.Item(10, intRow).Value) Then
                    MsgBox("Invalid Pcs - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            End If
        Next

        For intRow = 0 To flxDetails.Rows.Count - 1
            If Len(flxDetails.Item(12, intRow).Value) = 0 Then
                AdoCN.Execute("INSERT INTO tblRPrPacketDetails(Department,ParNo,PktNo,RghCts,FinCts,Shape,Color,Clarity,Value,Size,Width,Pcs,Cut,EntDate,UserName,CompName) " & _
                              "VALUES('RoughPlan','" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & _
                                "" & CDbl(flxDetails.Item(3, intRow).Value) & ",'" & flxDetails.Item(4, intRow).Value & "','" & flxDetails.Item(5, intRow).Value & "'," & _
                                "'" & flxDetails.Item(6, intRow).Value & "'," & CDbl(flxDetails.Item(7, intRow).Value) & ",'" & flxDetails.Item(8, intRow).Value & "'," & _
                                "" & CDbl(flxDetails.Item(9, intRow).Value) & "," & CDbl(flxDetails.Item(10, intRow).Value) & ",'" & flxDetails.Item(11, intRow).Value & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & PBUser_EmpNo & "','" & PBCompName & "')")
            End If
        Next
        MsgBox("Plan Details Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            Save()
        End If
    End Sub

    Private Sub frm_RprUpload_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub cmdOpen_Click(sender As Object, e As EventArgs) Handles cmdOpen.Click
        flxDetails.Rows.Clear()
        OpenFileDialog1.Filter = "All Excel Files|*.xls;*.xlsx;*.csv"
        'OpenFileDialog1.InitialDirectory = "C:\"
        OpenFileDialog1.InitialDirectory = "\\192.168.2.5\Rough-Excel\Active"
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
        
        Dim strParNo As String
        Dim strPktNo As String
        Dim dblPlanValue As Double
        Dim strStatus As String

        Dim strLength As String
        Dim strWidth As String
        Dim strSize As String

        flxDetails.Rows.Clear()
        If txtFilePath.Text = "" Then
            MsgBox("Please select the Excel File", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If Len(Dir(txtFilePath.Text)) > 0 Then
            xlApp = New Excel.Application
            xlWorkBook = xlApp.Workbooks.Open(txtFilePath.Text)
            xlWorkSheet = xlWorkBook.Worksheets(1)

            strParNo = ""
            strPktNo = ""
            dblPlanValue = 0
            strStatus = ""
            For intRow = 2 To 5000
                If Len(xlWorkSheet.Cells(intRow, 2).Value) = 0 Then
                    If Len(xlWorkSheet.Cells(intRow, 1).Value) > 0 Then
                        intRow = intRow + 1
                        If Len(xlWorkSheet.Cells(intRow, 2).Value) = 0 Then
                            Exit For
                        End If
                    Else
                        Exit For
                    End If
                End If
                strParNo = Mid(xlWorkSheet.Cells(intRow, 2).Value, 1, 7)
                strPktNo = strRight(xlWorkSheet.Cells(intRow, 2).Value, 4)

                If Len(xlWorkSheet.Cells(intRow, 13).Value) = 0 Then
                    dblPlanValue = 0
                Else
                    If Not IsNumeric(xlWorkSheet.Cells(intRow, 13).Value) Then
                        dblPlanValue = 0
                    Else
                        dblPlanValue = CDbl(xlWorkSheet.Cells(intRow, 13).Value)
                    End If
                End If

                strLength = ""
                strWidth = ""
                strSize = ""

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT ParNo FROM tblRPrPacket WHERE ParNo = '" & strParNo & "' AND PktNo = '" & strPktNo & "' AND Department = 'RoughPlan'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    strStatus = ""
                    rsComSql_1 = New ADODB.Recordset
                    rsComSql_1.Open("SELECT ParNo FROM tblRPrPacketDetails WHERE ParNo = '" & strParNo & "' AND PktNo = '" & strPktNo & "' AND Department = 'RoughPlan'", AdoCN, 1, 1)
                    If rsComSql_1.RecordCount Then
                        strStatus = "YES"
                    Else
                        strStatus = ""
                    End If
                    rsComSql_1 = Nothing

                    strLength = Trim(xlWorkSheet.Cells(intRow, 5).Value)
                    strWidth = Trim(xlWorkSheet.Cells(intRow, 6).Value)

                    If UCase(Trim(xlWorkSheet.Cells(intRow, 3).Value)) = "PCU" Then
                        If IsNumeric(strLength) Then
                            strLength = Format(CDbl(strLength), "#0.00")
                            strLength = Mid(strLength, 1, Len(strLength) - 1) & "0"
                        End If
                        If IsNumeric(strWidth) Then
                            strWidth = Format(CDbl(strWidth), "#0.00")
                            strWidth = Mid(strWidth, 1, Len(strWidth) - 1) & "0"
                        End If
                        strSize = strLength & "*" & strWidth

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM tblRgfSize WHERE SizeDec = '" & strSize & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            dblPlanValue = rsComSql_1.Fields("Price2").Value
                        Else
                            dblPlanValue = 0
                        End If
                        rsComSql_1 = Nothing

                        flxDetails.Rows.Add(strParNo, strPktNo, Trim(xlWorkSheet.Cells(intRow, 4).Value), Trim(xlWorkSheet.Cells(intRow, 9).Value),
                                            Trim(xlWorkSheet.Cells(intRow, 3).Value), Trim(xlWorkSheet.Cells(intRow, 11).Value), Trim(xlWorkSheet.Cells(intRow, 12).Value),
                                            dblPlanValue, strSize, "0", "1",
                                            Trim(xlWorkSheet.Cells(intRow, 8).Value), strStatus)

                    ElseIf Trim(xlWorkSheet.Cells(intRow, 3).Value) = "Baguettes" Then
                        dblPlanValue = 0
                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT AVG(dbo.VW_BAGAssort2020.ListCost) AS ListCost " & _
                                        "FROM dbo.VW_BAGAssort2020 INNER JOIN dbo.tblRPrCodes ON dbo.VW_BAGAssort2020.Color = dbo.tblRPrCodes.SysCode INNER JOIN " & _
                                            "dbo.tblRPrCodes AS tblRPrCodes_1 ON dbo.VW_BAGAssort2020.Clarity = tblRPrCodes_1.SysCode " & _
                                        "WHERE (dbo.tblRPrCodes.SysName = '" & Trim(xlWorkSheet.Cells(intRow, 11).Value) & "') AND (tblRPrCodes_1.SysName = '" & Trim(xlWorkSheet.Cells(intRow, 12).Value) & "') AND (dbo.VW_BAGAssort2020.LengthFrom <= '" & strLength & "') AND (dbo.VW_BAGAssort2020.LengthTo >= '" & strLength & "') AND (dbo.VW_BAGAssort2020.WidthFrom <= '" & strWidth & "')  " & _
                                            "AND (dbo.VW_BAGAssort2020.WidthTo >= '" & strWidth & "')", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount Then
                            If Not IsDBNull(rsComSql_1.Fields("ListCost").Value) Then
                                dblPlanValue = Math.Round(rsComSql_1.Fields("ListCost").Value * CDbl(Trim(xlWorkSheet.Cells(intRow, 9).Value)), 0)
                            Else
                                dblPlanValue = 0
                            End If
                        End If
                        rsComSql_1 = Nothing

                        flxDetails.Rows.Add(strParNo, strPktNo, Trim(xlWorkSheet.Cells(intRow, 4).Value), Trim(xlWorkSheet.Cells(intRow, 9).Value),
                                            Trim(xlWorkSheet.Cells(intRow, 3).Value), Trim(xlWorkSheet.Cells(intRow, 11).Value), Trim(xlWorkSheet.Cells(intRow, 12).Value),
                                            dblPlanValue, Trim(xlWorkSheet.Cells(intRow, 5).Value), Trim(xlWorkSheet.Cells(intRow, 6).Value), "1",
                                            Trim(xlWorkSheet.Cells(intRow, 8).Value), strStatus)
                    Else
                        flxDetails.Rows.Add(strParNo, strPktNo, Trim(xlWorkSheet.Cells(intRow, 4).Value), Trim(xlWorkSheet.Cells(intRow, 9).Value),
                                            Trim(xlWorkSheet.Cells(intRow, 3).Value), Trim(xlWorkSheet.Cells(intRow, 11).Value), Trim(xlWorkSheet.Cells(intRow, 12).Value),
                                            dblPlanValue, Trim(xlWorkSheet.Cells(intRow, 5).Value), Trim(xlWorkSheet.Cells(intRow, 6).Value), "1",
                                            Trim(xlWorkSheet.Cells(intRow, 8).Value), strStatus)
                    End If
                    
                End If
                rsComSql = Nothing
            Next

            xlWorkSheet = Nothing
            xlWorkBook.Close()
            xlApp.Quit()

            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)
        End If
        Exit Sub
ErrorHandler:
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

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub flxDetails_DoubleClick(sender As Object, e As EventArgs) Handles flxDetails.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)
        End If
    End Sub
End Class