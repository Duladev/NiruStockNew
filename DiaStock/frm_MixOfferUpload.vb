
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_MixOfferUpload
    Dim strFolderPath As String

    Private Sub ClearText()
        flxDetails.Rows.Clear()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearText()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub SaveOrder()
        Dim intRow As Integer
        Dim blnAccess As Boolean

        blnAccess = False

        ExpProgress.Minimum = 0
        ExpProgress.Visible = True
        ExpProgress.Maximum = flxDetails.Rows.Count

        For intRow = 0 To flxDetails.Rows.Count - 1
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblOffers WHERE ClientReference = '" & flxDetails.Item(0, intRow).Value & "' AND CustomerStoneReference = '" & flxDetails.Item(5, intRow).Value & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblOffers(ClientReference, DrawingReference, Client, Date, PlanDescription, CustomerStoneReference, Length, Width, QTY, PERSTONE, TotalStones, PriceForLaser, QtyForLaser, " & _
                                "PriceForGrooving, QtyForGrooving, TotalServices, TotalAmount, TotalAmountPerStone, FinalWeight) " & _
                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(2, intRow).Value & "','" & flxDetails.Item(3, intRow).Value & "'," & _
                                "'" & flxDetails.Item(4, intRow).Value & "','" & Replace(flxDetails.Item(5, intRow).Value, "'", "''") & "','" & flxDetails.Item(6, intRow).Value & "','" & flxDetails.Item(7, intRow).Value & "'," & _
                                "'" & flxDetails.Item(8, intRow).Value & "','" & flxDetails.Item(9, intRow).Value & "','" & flxDetails.Item(10, intRow).Value & "','" & flxDetails.Item(11, intRow).Value & "'," & _
                                "'" & flxDetails.Item(12, intRow).Value & "','" & flxDetails.Item(13, intRow).Value & "','" & flxDetails.Item(14, intRow).Value & "','" & flxDetails.Item(15, intRow).Value & "'," & _
                                "'" & flxDetails.Item(16, intRow).Value & "','" & flxDetails.Item(17, intRow).Value & "','" & flxDetails.Item(18, intRow).Value & "')")
            End If
            rsComSql = Nothing

            ExpProgress.Value = intRow + 1
        Next
        ExpProgress.Visible = False

        MsgBox("Offers Uploaded Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        ClearText()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        PBResponse = MsgBox("Are you sure to Upload?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            SaveOrder()
        End If
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub frm_MixOfferUpload_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        ClearText()
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
            flxDetails.Rows.Clear()

            For intRow = 2 To 10000
                If xlWorkSheet.Cells(intRow, 1).Value = "" Then Exit For

                flxDetails.Rows.Add(Trim(xlWorkSheet.Cells(intRow, 1).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 2).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 3).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 4).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 5).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 6).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 7).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 8).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 9).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 10).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 11).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 12).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 13).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 14).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 15).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 16).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 17).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 18).Value),
                                    Trim(xlWorkSheet.Cells(intRow, 19).Value))

            Next
            xlWorkSheet = Nothing
            xlWorkBook.Close()
            xlApp.Quit()

            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)

            MsgBox("Offer Detail Loaded", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        Exit Sub
ErrorHandler:
        MsgBox(Err.Description, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
        releaseObject(xlApp)
        releaseObject(xlWorkBook)
        releaseObject(xlWorkSheet)
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

    Private Sub cmdReport_Click(sender As Object, e As EventArgs) Handles cmdReport.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixOffer.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub
End Class