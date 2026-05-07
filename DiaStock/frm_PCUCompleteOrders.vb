
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_PCUCompleteOrders

    Private Sub Load_OpenOrders()
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        If optPcu.Checked = True Then
            rsComSql.Open("SELECT * FROM tblNoneOrders WHERE Complete = 'N' ORDER BY OrderNo", AdoCN, 1, 1)
        Else
            If optMix.Checked = True Then
                rsComSql.Open("SELECT * FROM tblOrders WHERE Complete = 'N' ORDER BY OrderNo", AdoCN, 1, 1)
            Else
                rsComSql.Open("SELECT * FROM tblKITOrders WHERE Complete = 'N' ORDER BY OrderNo", AdoCN, 1, 1)
            End If
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("OrderNo").Value,
                                    rsComSql.Fields("Subject").Value,
                                    False)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtCount.Text = Calculate_Orders()
    End Sub

    Private Sub frm_PCUCompleteOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_OpenOrders()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub txtOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOrder.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If txtOrder.Text <> "" Then
            If optPcu.Checked = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblNoneOrders WHERE OrderNo = '" & txtOrder.Text & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If rsComSql.Fields("Complete").Value = "N" Then
                        AdoCN.Execute("UPDATE tblNoneOrders SET Complete = 'Y' WHERE OrderNo = '" & txtOrder.Text & "' AND Complete = 'N'")
                        txtOrder.Text = ""
                        MsgBox("Order Completed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Else
                        PBResponse = MsgBox("Already Completed. Do you want to open?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
                        If PBResponse = MsgBoxResult.Yes Then
                            AdoCN.Execute("UPDATE tblNoneOrders SET Complete = 'N' WHERE OrderNo = '" & txtOrder.Text & "' AND Complete = 'Y'")
                            txtOrder.Text = ""
                            MsgBox("Order Opened", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        End If
                    End If
                End If
                rsComSql = Nothing
            Else
                If optMix.Checked = True Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblOrders WHERE OrderNo = '" & txtOrder.Text & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If rsComSql.Fields("Complete").Value = "N" Then
                            AdoCN.Execute("UPDATE tblOrders SET Complete = 'Y' WHERE OrderNo = '" & txtOrder.Text & "' AND Complete = 'N'")
                            txtOrder.Text = ""
                            MsgBox("Order Completed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Else
                            PBResponse = MsgBox("Already Completed. Do you want to open?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
                            If PBResponse = MsgBoxResult.Yes Then
                                AdoCN.Execute("UPDATE tblOrders SET Complete = 'N' WHERE OrderNo = '" & txtOrder.Text & "' AND Complete = 'Y'")
                                txtOrder.Text = ""
                                MsgBox("Order Opened", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            End If
                        End If
                    End If
                    rsComSql = Nothing
                Else
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblKITOrders WHERE OrderNo = '" & txtOrder.Text & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If rsComSql.Fields("Complete").Value = "N" Then
                            AdoCN.Execute("UPDATE tblKITOrders SET Complete = 'Y' WHERE OrderNo = '" & txtOrder.Text & "' AND Complete = 'N'")
                            txtOrder.Text = ""
                            MsgBox("Order Completed", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Else
                            PBResponse = MsgBox("Already Completed. Do you want to open?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
                            If PBResponse = MsgBoxResult.Yes Then
                                AdoCN.Execute("UPDATE tblKITOrders SET Complete = 'N' WHERE OrderNo = '" & txtOrder.Text & "' AND Complete = 'Y'")
                                txtOrder.Text = ""
                                MsgBox("Order Opened", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            End If
                        End If
                    End If
                    rsComSql = Nothing
                End If
            End If
        Else
            MsgBox("Order No. cannot be blank", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub cmdComplete_Click(sender As Object, e As EventArgs) Handles cmdComplete.Click
        Dim intRow As Integer

        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(2, intRow).Value = True Or flxDetails.Item(2, intRow).Value = 1 Then
                If optPcu.Checked = True Then
                    AdoCN.Execute("UPDATE tblNoneOrders SET Complete = 'Y' WHERE OrderNo = '" & flxDetails.Item(0, intRow).Value & "' AND Complete = 'N'")
                Else
                    If optMix.Checked = True Then
                        AdoCN.Execute("UPDATE tblOrders SET Complete = 'Y' WHERE OrderNo = '" & flxDetails.Item(0, intRow).Value & "' AND Complete = 'N'")
                    Else
                        AdoCN.Execute("UPDATE tblKITOrders SET Complete = 'Y' WHERE OrderNo = '" & flxDetails.Item(0, intRow).Value & "' AND Complete = 'N'")
                    End If
                End If
            End If
        Next

        Load_OpenOrders()
    End Sub

    Private Sub optPcu_CheckedChanged(sender As Object, e As EventArgs) Handles optPcu.CheckedChanged
        Load_OpenOrders()
    End Sub

    Private Sub optMix_CheckedChanged(sender As Object, e As EventArgs) Handles optMix.CheckedChanged
        Load_OpenOrders()
    End Sub

    Private Function Calculate_Orders() As Integer
        Dim intRow As Integer
        Dim intCount As Integer

        intCount = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Rows(intRow).Cells(2).EditedFormattedValue = True Then
                intCount = intCount + 1
            End If
        Next
        Calculate_Orders = intCount
        Return Calculate_Orders
    End Function

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        txtCount.Text = Calculate_Orders()
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
        Dim intRow As Integer

        If txtFilePath.Text = "" Then
            MsgBox("Please select the Excel File", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If Len(Dir(txtFilePath.Text)) > 0 Then

            xlApp = New Excel.Application
            xlWorkBook = xlApp.Workbooks.Open(txtFilePath.Text)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            flxDetails.Rows.Clear()

            For intRow = 2 To 10000
                If Len(xlWorkSheet.Cells(intRow, 1).Value) = 0 Then Exit For
                rsComSql = New ADODB.Recordset
                If optPcu.Checked = True Then
                    rsComSql.Open("SELECT * FROM tblNoneOrders WHERE OrderNo = '" & Trim(xlWorkSheet.Cells(intRow, 1).Value) & "' AND Complete = 'N'", AdoCN, 1, 1)
                Else
                    rsComSql.Open("SELECT * FROM tblOrders WHERE OrderNo = '" & Trim(xlWorkSheet.Cells(intRow, 1).Value) & "' AND Complete = 'N'", AdoCN, 1, 1)
                End If
                If rsComSql.RecordCount Then
                    flxDetails.Rows.Add(rsComSql.Fields("OrderNo").Value,
                                        rsComSql.Fields("Subject").Value,
                                        True)

                End If
                rsComSql = Nothing
            Next
            xlWorkSheet = Nothing
            xlWorkBook.Close()
            xlApp.Quit()

            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)

            Calculate_Orders()

            MsgBox("Order Excel Loaded", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
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

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub optKit_CheckedChanged(sender As Object, e As EventArgs) Handles optKit.CheckedChanged
        Load_OpenOrders()
    End Sub
End Class