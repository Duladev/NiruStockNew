
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLLab
    Private Sub Load_Machine()
        'cmbMachine.Items.Clear()
        'cmbMachine.Items.Add("AMS")
        'cmbMachine.Items.Add("YAHUDA")

        Dim I As Integer
        Dim dgvcc As DataGridViewComboBoxCell

        For I = 0 To flxDetails.Rows.Count - 1
            dgvcc = flxDetails.Rows(I).Cells(12)
            dgvcc.Items.Clear()

            dgvcc.Items.Add("AMS")
            dgvcc.Items.Add("DFI")
            dgvcc.Items.Add("ID100")
            dgvcc.Items.Add("YEHUDA")
        Next
        
    End Sub

    Private Sub frm_DCLLab_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub txtPack_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPack.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            If Len(txtPack.Text) > 0 Then
                Load_PackingList()
            End If
        End If
    End Sub

    Private Sub Load_PackingList()
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT dbo.tblPOLSales.Assortment, SUM(dbo.tblPOLSales.Pcs) AS Pcs, ROUND(SUM(dbo.tblPOLSales.Cts), 3) AS Cts " & _
                      "FROM dbo.tblGrading_Pack INNER JOIN dbo.tblPOLSales ON dbo.tblGrading_Pack.PackNo = dbo.tblPOLSales.SalesNo " & _
                      "WHERE(dbo.tblGrading_Pack.PackingListNo = " & CDbl(txtPack.Text) & ") AND (dbo.tblPOLSales.OK = 0) " & _
                      "GROUP BY dbo.tblPOLSales.Assortment " & _
                      "ORDER BY Pcs DESC", AdoCN, 1, 1)

        'rsComSql.Open("SELECT dbo.tblPOLSales.Assortment, SUM(dbo.tblPOLSales.Pcs) AS Pcs, ROUND(SUM(dbo.tblPOLSales.Cts), 3) AS Cts " & _
        '              "FROM dbo.tblGrading_Pack INNER JOIN dbo.tblPOLSales ON dbo.tblGrading_Pack.PackNo = dbo.tblPOLSales.SalesNo " & _
        '              "WHERE(dbo.tblGrading_Pack.PackingListNo = " & CDbl(txtPack.Text) & ") " & _
        '              "GROUP BY dbo.tblPOLSales.Assortment " & _
        '              "ORDER BY Pcs DESC", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(txtPack.Text,
                                    rsComSql.Fields("Assortment").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("Cts").Value,
                                    False,
                                    rsComSql.Fields("Pcs").Value, 0, 0, 0, 0, 0, "PolishBox")

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtTotalPcs.Text = CalTotalPcs(flxDetails)

        Select_Assortments()

        txtTotSelPcs.Text = CalTotalSelPcs(flxDetails)

        Load_Machine()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(2, intRow).Value)
        Next
    End Function

    Private Function CalTotalSelPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalSelPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(4).EditedFormattedValue = True Or flxSample.Rows(intRow).Cells(4).EditedFormattedValue = 1 Then
                CalTotalSelPcs = CalTotalSelPcs + Val(flxSample.Item(2, intRow).Value)
            End If
        Next
    End Function

    Private Sub Select_Assortments()
        Dim dblSelectPcs As Double
        Dim dblCheckPcs As Double

        dblCheckPcs = CDbl(txtTotalPcs.Text) * 0.25
        dblCheckPcs = Math.Round(dblCheckPcs)

        dblSelectPcs = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If dblSelectPcs < dblCheckPcs Then
                dblSelectPcs = dblSelectPcs + CDbl(flxDetails.Item(2, intRow).Value)
                flxDetails.Item(4, intRow).Value = True
            Else
                Exit For
            End If
        Next
    End Sub

    Private Sub VerifyPacket()
        Dim intRow As Integer
        Dim intCol As Integer

        If Len(txtTotalPcs.Text) = 0 Then Exit Sub
        If Len(txtTotSelPcs.Text) = 0 Then Exit Sub
        If CDbl(txtTotSelPcs.Text) < CDbl(txtTotalPcs.Text) * 0.25 Then MsgBox("Pcs not Enough", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(4, intRow).Value = True Then
                    For intCol = 5 To 10
                        If Len(flxDetails.Item(intCol, intRow).Value) = 0 Then
                            MsgBox("Invalid Pcs in " & intCol & ", " & intRow, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                        If Not IsNumeric(flxDetails.Item(intCol, intRow).Value) = True Then
                            MsgBox("Invalid Pcs in " & intCol & ", " & intRow, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                            Exit Sub
                        End If
                    Next

                    If Len(flxDetails.Item(12, intRow).Value) = 0 Then
                        MsgBox("Invalid Machine No - " & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If

                    If CDbl(flxDetails.Item(2, intRow).Value) <> CDbl(flxDetails.Item(5, intRow).Value) + CDbl(flxDetails.Item(6, intRow).Value) + CDbl(flxDetails.Item(7, intRow).Value) + CDbl(flxDetails.Item(8, intRow).Value) + CDbl(flxDetails.Item(9, intRow).Value) + CDbl(flxDetails.Item(10, intRow).Value) Then
                        MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If
            Next

            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(4, intRow).Value = True Then
                    AdoCN.Execute("INSERT INTO tblAMS2Log(MacNo,SupParcelNo,Pcs,Cts,EmpNo,EmpNoEnt,ChkDate,ChkTime,PASS,REFER,SYNTHETIC,NONDIAMOND,PURGE,NOTCHECKED,Assortment) " & _
                                  "VALUES('" & flxDetails.Item(12, intRow).Value & "','" & flxDetails.Item(0, intRow).Value & "'," & CDbl(flxDetails.Item(2, intRow).Value) & "," & CDbl(flxDetails.Item(3, intRow).Value) & "," & _
                                    "'" & PBUser_EmpNo & "','" & PBUser_EmpNo & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "'," & CDbl(flxDetails.Item(5, intRow).Value) & "," & _
                                    "" & CDbl(flxDetails.Item(6, intRow).Value) & "," & CDbl(flxDetails.Item(7, intRow).Value) & "," & CDbl(flxDetails.Item(8, intRow).Value) & "," & CDbl(flxDetails.Item(9, intRow).Value) & "," & CDbl(flxDetails.Item(10, intRow).Value) & ",'" & flxDetails.Item(1, intRow).Value & "')")

                End If
            Next
            ClearFields()
        End If
    End Sub

    Private Sub ClearFields()
        txtPack.Text = ""
        flxDetails.Rows.Clear()
        txtTotalPcs.Text = ""
        txtTotSelPcs.Text = ""
    End Sub

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 4 Then
            txtTotSelPcs.Text = CalTotalSelPcs(flxDetails)
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        VerifyPacket()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptAMSLog.rpt"
        strReportPath = PBReportPath & "DiaSalesGrading\" & mReportName
        objForm.Show()
    End Sub
End Class