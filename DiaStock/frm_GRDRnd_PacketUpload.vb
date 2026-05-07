Imports System.Data.SqlClient
Imports System.IO

Public Class frm_GRDRnd_PacketUpload

    ' ---------------------------------------------------------------
    '  FORM LOAD
    ' ---------------------------------------------------------------
    Private Sub frm_GRDRnd_PacketUpload_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.StartPosition = FormStartPosition.CenterScreen
            SetupGrid()
        Catch ex As Exception
            MsgBox("Error in Form_Load : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    ' ---------------------------------------------------------------
    '  SETUP GRID
    ' ---------------------------------------------------------------
    Private Sub SetupGrid()
        flxDetails.Columns.Clear()
        flxDetails.AutoGenerateColumns = False
        flxDetails.AllowUserToAddRows = False
        flxDetails.AllowUserToDeleteRows = False
        flxDetails.ReadOnly = True
        flxDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        flxDetails.BackgroundColor = System.Drawing.Color.White
        flxDetails.Font = New System.Drawing.Font("Tahoma", 8.25)
        flxDetails.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(224, 224, 224)

        Dim headers() As String = {"Parcel No", "Assortment", "Code", "Size Range", "Pcs", "Cts"}
        Dim names() As String = {"ParNo", "Assortment", "Code", "SizeRange", "Pcs", "Cts"}
        Dim widths() As Integer = {100, 110, 110, 110, 80, 90}

        For idx As Integer = 0 To headers.Length - 1
            Dim col As New DataGridViewTextBoxColumn()
            col.HeaderText = headers(idx)
            col.Name = names(idx)
            col.Width = widths(idx)
            If idx >= 4 Then
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
            flxDetails.Columns.Add(col)
        Next
    End Sub

    ' ---------------------------------------------------------------
    '  PARCEL NO — ENTER KEY
    ' ---------------------------------------------------------------
    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True
            If ParcelFound(txtParNo.Text.Trim()) Then
                txtParNo.Text = txtParNo.Text.Trim().ToUpper()
                txtPktNo.Focus()
            Else
                MessageBox.Show("Invalid Parcel", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearFields()
                txtParNo.Focus()
            End If
        End If
    End Sub

    ' ---------------------------------------------------------------
    '  PACKET NO — ENTER KEY
    ' ---------------------------------------------------------------
    Private Sub txtPktNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktNo.KeyPress
        If e.KeyChar = ControlChars.Cr Then
            e.Handled = True
            Try
                Dim rsComSql As New ADODB.Recordset()
                rsComSql.Open(
                    "SELECT * FROM tblGrading_RndPacket " &
                    "WHERE ParNo='" & txtParNo.Text.Trim() & "' " &
                    "AND PktNo='" & txtPktNo.Text.Trim() & "'",
                    AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                    ADODB.LockTypeEnum.adLockOptimistic)

                If Not rsComSql.EOF Then
                    txtPktPcs.Text = rsComSql.Fields("PktPcs").Value.ToString()
                    txtPktCts.Text = Format(Convert.ToDouble(rsComSql.Fields("PktCts").Value), "#0.000")
                Else
                    MessageBox.Show("Invalid Parcel and Packet", Me.Text,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtPktNo.Text = ""
                End If

                rsComSql.Close()
                rsComSql = Nothing
            Catch ex As Exception
                MsgBox("Error in txtPktNo_KeyPress : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        End If
    End Sub

    ' ---------------------------------------------------------------
    '  SELECT FILE
    ' ---------------------------------------------------------------
    Private Sub cmdSelect_Click(sender As Object, e As EventArgs) Handles cmdSelect.Click
        flxDetails.Rows.Clear()
        Using dlg As New OpenFileDialog()
            dlg.InitialDirectory = "C:\"
            dlg.Filter = "Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx|All Files|*.*"
            If dlg.ShowDialog() = DialogResult.OK Then
                txtBackupLocation.Text = dlg.FileName
            End If
        End Using
    End Sub

    ' ---------------------------------------------------------------
    '  LOAD FILE
    ' ---------------------------------------------------------------
    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        If txtBackupLocation.Text.Trim() = "" Then
            MessageBox.Show("Please select the Excel file", Me.Text,
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        If Not File.Exists(txtBackupLocation.Text.Trim()) Then
            MessageBox.Show("Invalid File Path", Me.Text,
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Upload_Excel()
    End Sub

    ' ---------------------------------------------------------------
    '  READ EXCEL AND POPULATE GRID
    ' ---------------------------------------------------------------
    Private Sub Upload_Excel()
        Cursor.Current = Cursors.WaitCursor
        Try
            flxDetails.Rows.Clear()
            txtPcs.Text = "0"
            txtCts.Text = "0"

            Dim xlApp As New Microsoft.Office.Interop.Excel.Application()
            xlApp.Visible = False
            Dim wb As Microsoft.Office.Interop.Excel.Workbook =
                xlApp.Workbooks.Open(txtBackupLocation.Text.Trim())
            Dim ws As Microsoft.Office.Interop.Excel.Worksheet =
                CType(wb.Worksheets(1), Microsoft.Office.Interop.Excel.Worksheet)

            Dim totPcs As Double = 0
            Dim totCts As Double = 0

            For intRow As Integer = 2 To 1000
                Dim cellVal As Object = ws.Cells(intRow, 1).Value
                If cellVal Is Nothing OrElse cellVal.ToString().Trim() = "" Then Exit For

                Dim assortNo As String = cellVal.ToString().Trim()

                ' Look up MainAssort — DiaStock ADODB pattern
                Dim mainAssort As String = ""
                Dim rsComSql As New ADODB.Recordset()
                rsComSql.Open(
                    "SELECT MainAssort FROM tblGrading_RndSizeListNew " &
                    "WHERE AssortNo='" & assortNo & "'",
                    AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                    ADODB.LockTypeEnum.adLockOptimistic)

                If Not rsComSql.EOF Then
                    mainAssort = rsComSql.Fields("MainAssort").Value.ToString().Trim()
                End If
                rsComSql.Close()
                rsComSql = Nothing

                If mainAssort = "" Then
                    Application.DoEvents()
                    Continue For
                End If

                Dim sizeRange As String = ws.Cells(intRow, 2).Value?.ToString().Trim()
                Dim pcs As Double = Convert.ToDouble(If(ws.Cells(intRow, 3).Value IsNot Nothing, ws.Cells(intRow, 3).Value, 0))
                Dim cts As Double = Convert.ToDouble(If(ws.Cells(intRow, 4).Value IsNot Nothing, ws.Cells(intRow, 4).Value, 0))

                flxDetails.Rows.Add(
                    txtParNo.Text.Trim(),
                    assortNo,
                    mainAssort,
                    sizeRange,
                    pcs.ToString(),
                    Format(cts, "#0.000")
                )

                totPcs += pcs
                totCts += cts

                Application.DoEvents()
            Next

            txtPcs.Text = totPcs.ToString()
            txtCts.Text = Format(Math.Round(totCts, 3), "#0.000")

            wb.Close(False)
            xlApp.Quit()
            System.Runtime.InteropServices.Marshal.ReleaseComObject(ws)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(wb)
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp)

        Catch ex As Exception
            MessageBox.Show(ex.Message, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor.Current = Cursors.Default
        End Try
    End Sub

    ' ---------------------------------------------------------------
    '  SAVE BUTTON
    ' ---------------------------------------------------------------
    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    ' ---------------------------------------------------------------
    '  SAVE — full logic
    Private Sub Save()
        Try
            If flxDetails.Rows.Count = 0 Then Return

            If txtParNo.Text.Trim() = "" Then MessageBox.Show("Invalid Parcel No", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            If txtPktNo.Text.Trim() = "" Then MessageBox.Show("Invalid Packet No", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            If txtPktPcs.Text.Trim() = "" Then MessageBox.Show("Invalid Packet Pcs", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            If txtPktCts.Text.Trim() = "" Then MessageBox.Show("Invalid Packet Cts", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            If txtPcs.Text.Trim() = "" Then MessageBox.Show("Invalid Total Pcs", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            If txtCts.Text.Trim() = "" Then MessageBox.Show("Invalid Total Cts", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return

            ' Verify pcs / cts match packet
            If Convert.ToDouble(txtPktPcs.Text) <> Convert.ToDouble(txtPcs.Text) Then
                MessageBox.Show("Invalid Pcs — does not match packet", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If Math.Round(Convert.ToDouble(txtPktCts.Text), 3) <> Math.Round(Convert.ToDouble(txtCts.Text), 3) Then
                MessageBox.Show("Invalid Cts — does not match packet", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            Dim confirm = MessageBox.Show("Are you sure?", Me.Text,
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.No Then Return

            ' ---- working variables ----
            Dim par As String = txtParNo.Text.Trim()
            Dim pkt As String = txtPktNo.Text.Trim()
            Dim pcs As Single = CSng(txtPcs.Text)
            Dim cts As Double = Convert.ToDouble(txtCts.Text)
            Dim today As String = Date.Now.ToString("MM/dd/yyyy")
            Dim now As String = DateTime.Now.ToString("HH:mm:ss")

            ' DiaStock session variable — replaces NiruStock PBUser_ID / GlobalVariables
            Dim empNo As String = PBUser_EmpNo   ' from mdlVariables

            Dim rsComSql As New ADODB.Recordset()

            ' ---- Insert checking issues / returns / types (if not Size Only) ----
            If Not chkSize.Checked Then
                For sec As Integer = 1 To 5

                    ' Duplicate check
                    rsComSql.Open(
                        "SELECT COUNT(*) AS CNT FROM tblGrading_RndCheckingIssues " &
                        "WHERE Department='Colombo' AND ParNo='" & par & "' " &
                        "AND PktNo='" & pkt & "' AND Sec=" & sec,
                        AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                        ADODB.LockTypeEnum.adLockOptimistic)

                    If CInt(rsComSql.Fields("CNT").Value) > 0 Then
                        rsComSql.Close()
                        MessageBox.Show("Already Entered", Me.Text,
                                        MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    End If
                    rsComSql.Close()

                    ' tblGrading_RndCheckingIssues
                    rsComSql.Open(
                        "INSERT INTO tblGrading_RndCheckingIssues" &
                        "(Department,ParNo,PktNo,Sec,SecCount,EmpNo,IssPcs,IssCts,IssDate,IssTime) " &
                        "VALUES('Colombo','" & par & "','" & pkt & "'," & sec & "," & sec & ",'" & empNo & "'," &
                        pcs & "," & cts & ",'" & today & "','" & now & "')",
                        AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                        ADODB.LockTypeEnum.adLockOptimistic)
                    rsComSql.Close()

                    ' tblGrading_RndCheckingReturns
                    rsComSql.Open(
                        "INSERT INTO tblGrading_RndCheckingReturns" &
                        "(Department,ParNo,PktNo,Sec,SecCount,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " &
                        "VALUES('Colombo','" & par & "','" & pkt & "'," & sec & "," & sec & ",'" & empNo & "'," &
                        pcs & "," & cts & ",0,0,0,0,'" & today & "','" & now & "',0,0)",
                        AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                        ADODB.LockTypeEnum.adLockOptimistic)
                    rsComSql.Close()

                    ' Return type defaults per section
                    Dim rt1 As String = "DEF"
                    Dim rt2 As String = If(sec >= 2, "A MAKE", "")
                    Dim rt3 As String = If(sec >= 3, "N FLO", "")
                    Dim rt4 As String = If(sec >= 4, "VVS2", "")

                    ' tblGrading_RndCheckingTypes
                    rsComSql.Open(
                        "INSERT INTO tblGrading_RndCheckingTypes" &
                        "(Department,ParNo,PktNo,Sec,ReturnType1,ReturnType2,ReturnType3,ReturnType4,Pcs,Cts) " &
                        "VALUES('Colombo','" & par & "','" & pkt & "'," & sec & "," &
                        "'" & rt1 & "','" & rt2 & "','" & rt3 & "','" & rt4 & "'," & pcs & "," & cts & ")",
                        AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                        ADODB.LockTypeEnum.adLockOptimistic)
                    rsComSql.Close()

                Next
            End If

            ' ---- tblGrading_SizingPacket ----
            rsComSql.Open(
                "INSERT INTO tblGrading_RndSizingPacket" &
                "(Department,ParNo,PktNo,SizeCode,PktPcs,PktCts,ReturnType1,ReturnType2,ReturnType3,ReturnType4,PktType) " &
                "VALUES('Colombo','" & par & "','" & pkt & "','T02-0'," & CDbl(pcs) & "," & cts & ",'','','','','N')",
                AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                ADODB.LockTypeEnum.adLockOptimistic)
            rsComSql.Close()

            ' ---- tblGrading_RndSizingIssues ----
            rsComSql.Open(
                "INSERT INTO tblGrading_RndSizingIssues" &
                "(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " &
                "VALUES('Colombo','" & par & "','" & pkt & "',1,'" & empNo & "'," &
                CDbl(pcs) & "," & cts & ",'" & today & "','" & now & "')",
                AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                ADODB.LockTypeEnum.adLockOptimistic)
            rsComSql.Close()

            ' ---- tblGrading_RndSizingReturns ----
            rsComSql.Open(
                "INSERT INTO tblGrading_RndSizingReturns" &
                "(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " &
                "VALUES('Colombo','" & par & "','" & pkt & "',1,'" & empNo & "'," &
                CDbl(pcs) & "," & cts & ",0,0,0,0,'" & today & "','" & now & "',0,0)",
                AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                ADODB.LockTypeEnum.adLockOptimistic)
            rsComSql.Close()

            ' ---- tblGrading_RndSizingTypes — one row per grid row ----
            For Each row As DataGridViewRow In flxDetails.Rows
                Dim rt1Val As String = row.Cells("Code").Value?.ToString().Trim()
                Dim rt2Val As String = row.Cells("Assortment").Value?.ToString().Trim()
                Dim rt3Val As String = row.Cells("SizeRange").Value?.ToString().Trim()
                Dim rowPcs As Double = Convert.ToDouble(row.Cells("Pcs").Value?.ToString())
                Dim rowCts As Double = Convert.ToDouble(row.Cells("Cts").Value?.ToString())

                rsComSql.Open(
                    "INSERT INTO tblGrading_RndSizingTypes" &
                    "(Department,ParNo,PktNo,Sec,ReturnType1,ReturnType2,ReturnType3,ReturnType4,ReturnType5,Pcs,Cts) " &
                    "VALUES('Colombo','" & par & "','" & pkt & "',1,'" & rt1Val & "','" & rt2Val & "','" & rt3Val & "','',''," &
                    rowPcs & "," & rowCts & ")",
                    AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                    ADODB.LockTypeEnum.adLockOptimistic)
                rsComSql.Close()
            Next

            rsComSql = Nothing

            MessageBox.Show("Parcel Saved", Me.Text,
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            ClearFields()

        Catch ex As Exception
            MsgBox("Error saving : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    ' ---------------------------------------------------------------
    '  PARCEL FOUND CHECK
    ' ---------------------------------------------------------------
    Private Function ParcelFound(parcelNo As String) As Boolean
        Try
            Dim rsComSql As New ADODB.Recordset()
            rsComSql.Open(
                "SELECT COUNT(*) AS CNT FROM tblGrading_RndInvoice WHERE ParcelNo='" & parcelNo & "'",
                AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                ADODB.LockTypeEnum.adLockOptimistic)

            Dim found As Boolean = CInt(rsComSql.Fields("CNT").Value) > 0
            rsComSql.Close()
            rsComSql = Nothing
            Return found
        Catch
            Return False
        End Try
    End Function

    ' ---------------------------------------------------------------
    '  CLEAR ALL FIELDS
    ' ---------------------------------------------------------------
    Private Sub ClearFields()
        txtParNo.Text = ""
        txtPktNo.Text = ""
        txtPktPcs.Text = ""
        txtPktCts.Text = ""
        txtPcs.Text = ""
        txtCts.Text = ""
        txtBackupLocation.Text = ""
        flxDetails.Rows.Clear()
        chkSize.Checked = False
    End Sub

    ' ---------------------------------------------------------------
    '  EXIT
    ' ---------------------------------------------------------------
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub lblTitle_Click(sender As Object, e As EventArgs) Handles lblTitle.Click
        ' reserved
    End Sub

End Class