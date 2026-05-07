
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_MixPackingUpload
    Private Sub Get_PackNo()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(PackNo) AS MaxNo FROM tblMixPackingList", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If IsDBNull(rsComSql.Fields("MaxNo").Value) Then
                txtPackNo.Text = "1"
            Else
                txtPackNo.Text = rsComSql.Fields("MaxNo").Value + 1
            End If
        Else
            txtPackNo.Text = "1"
        End If
        rsComSql = Nothing
    End Sub

    Private Sub frm_MixPackingUpload_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Get_PackNo()
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
        Dim strAssortment As String
        Dim dblSize, dblPrice, dblTotPcs, dblTotCts As Double

        If txtFilePath.Text = "" Then
            MsgBox("Please select the Excel File", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If Len(Dir(txtFilePath.Text)) > 0 Then

            xlApp = New Excel.Application
            xlWorkBook = xlApp.Workbooks.Open(txtFilePath.Text)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            flxDetails.Rows.Clear()
            m_LotNo = 1
            For intRow = 2 To 1000
                If Trim(xlWorkSheet.Cells(intRow, 1).Value) = "" Then Exit For
                If Trim(xlWorkSheet.Cells(intRow, 1).Value) <> "" Then
                    flxDetails.Rows.Add(Trim(xlWorkSheet.Cells(intRow, 1).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 2).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 3).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 4).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 5).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 6).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 7).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 8).Value),
                                        Trim(xlWorkSheet.Cells(intRow, 9).Value))

                    m_LotNo = m_LotNo + 1
                    dblTotPcs = dblTotPcs + CDbl(Trim(xlWorkSheet.Cells(intRow, 8).Value))
                    dblTotCts = dblTotCts + CDbl(Trim(xlWorkSheet.Cells(intRow, 9).Value))
                End If

            Next
            xlWorkSheet = Nothing
            xlWorkBook.Close()
            xlApp.Quit()

            txtTotalPcs.Text = dblTotPcs
            txtTotalCts.Text = dblTotCts

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

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean

        AdoCN.Execute("DELETE FROM tblMixPackingList WHERE PackNo = '" & CDbl(txtPackNo.Text) & "'")
        For intRow = 0 To flxDetails.Rows.Count - 1
            blnSave = True
            AdoCN.Execute("INSERT INTO tblMixPackingList(PackNo,InvDate,PktSerialNo,NiruOrdNo,Item,Command,Client,CusRef,StnRef,PackPcs,PackCts) " & _
                          "VALUES('" & CDbl(txtPackNo.Text) & "','" & Format(Date.Now, "MM/dd/yyyy") & "'," & CInt(flxDetails.Item(0, intRow).Value) & ",'" & flxDetails.Item(1, intRow).Value & "'," & _
                            "" & CInt(flxDetails.Item(2, intRow).Value) & ",'" & flxDetails.Item(3, intRow).Value & "','" & Replace(flxDetails.Item(4, intRow).Value, "'", "''") & "','" & Replace(flxDetails.Item(5, intRow).Value, "'", "''") & "'," & _
                            "'" & Replace(flxDetails.Item(6, intRow).Value, "'", "''") & "'," & CDbl(flxDetails.Item(7, intRow).Value) & "," & CDbl(flxDetails.Item(8, intRow).Value) & ")")
        Next
        If blnSave = True Then
            MsgBox("Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            txtFilePath.Text = ""
            txtPackNo.Text = ""
            txtTotalPcs.Text = ""
            txtTotalCts.Text = ""
            flxDetails.Rows.Clear()

            Get_PackNo()
        End If
    End Sub

    Private Sub cmdPrint_Click(sender As Object, e As EventArgs) Handles cmdPrint.Click
        Save()
    End Sub
End Class