
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixBoilingManual
    Dim strFolderPath As String

    Private Sub ClearFields()
        flxDetails.Rows.Clear()
        txtIssPcs.Text = ""
        txtEmp.Text = ""
        chkSelect.Checked = False
    End Sub

    Private Sub cmdEmp_Click(sender As Object, e As EventArgs) Handles cmdEmp.Click
        Datavalid = False
        Parcel = False
        Instring = UCase(InputBox("Enter Emp No"))
        ParcelLen = Len(Instring)
        If ParcelLen = 6 Then
            Datavalid = True

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL3 WHERE (Pay = 1) AND (FullEmpNo = '" & Trim(Instring) & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                cmdEmp.Focus()
                Exit Sub
            End If
            rsComSql = Nothing
            ICNo = UCase(Trim(Instring))
            txtEmp.Text = ICNo
        Else
            MsgBox("Invalid IC No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Datavalid = False
            ICNo = ""
            cmdEmp.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub Load_Details()
        ClearFields()

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.VW_MIXFinishIssuesBal15.ParNo, dbo.VW_MIXFinishIssuesBal15.PktNo, dbo.VW_MIXFinishIssuesBal15.BalPcs " & _
                      "FROM dbo.VW_MIXFinishIssuesBal15 LEFT OUTER JOIN dbo.VW_MixRepairPending15 ON dbo.VW_MIXFinishIssuesBal15.ParNo = dbo.VW_MixRepairPending15.ParNo AND  " & _
                        "dbo.VW_MIXFinishIssuesBal15.PktNo = dbo.VW_MixRepairPending15.PktNo " & _
                      "WHERE(dbo.VW_MixRepairPending15.PktNo IS NULL) " & _
                      "ORDER BY dbo.VW_MIXFinishIssuesBal15.ParNo, dbo.VW_MIXFinishIssuesBal15.PktNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("BalPcs").Value,
                                    False)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

    End Sub

    Private Function CalTotalPcs() As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Rows(intRow).Cells(3).EditedFormattedValue = True Then
                CalTotalPcs = CalTotalPcs + CInt(flxDetails.Item(2, intRow).Value)
            End If
        Next
        Return CalTotalPcs
    End Function

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        Load_Details()
    End Sub

    Private Sub cmbDept_SelectedIndexChanged(sender As Object, e As EventArgs)
        ClearFields()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean

        If txtEmp.Text = "" Then MsgBox("Invalid Employee No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(3, intRow).Value = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM dbo.VW_MIXFinishIssuesBal15 WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    If rsComSql.Fields("BalPcs").Value < CInt(flxDetails.Item(2, intRow).Value) Then
                        MsgBox("Invalid Pcs - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                    rsComSql = Nothing
                End If
            End If
        Next

        blnSave = False
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(3, intRow).Value = True Then
                blnSave = True
                AdoCN.Execute("INSERT INTO tblMixIssuesBoil(ParNo,PktNo,Sec,IssPcs,EmpNo,IssDate,IssTime,EmpNo2) " & _
                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "',1," & CInt(flxDetails.Item(2, intRow).Value) & ",'" & Mid(Trim(txtEmp.Text), 1, 6) & "'," & _
                                    "'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & PBUser_EmpNo & "')")
            End If
        Next

        If blnSave = True Then
            MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
        End If

    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            Save()
        End If
    End Sub

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(3, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(3, intRow).Value = False
            Next
        End If
        txtIssPcs.Text = CalTotalPcs()
    End Sub

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 3 Then
            txtIssPcs.Text = CalTotalPcs()
        End If
    End Sub

    Private Sub GetNextBoilingtNo()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(BatchNo) AS MaxNo FROM tblMixIssuesBoil WHERE (Sec = 1)", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            If Not IsDBNull(rsComSql.Fields("MaxNo").Value) Then
                txtBoilingNo.Text = rsComSql.Fields("MaxNo").Value + 1
            Else
                txtBoilingNo.Text = "1"
            End If
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_Packets()

        flxDetails2.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixIssuesBoil.ID, dbo.tblMixIssuesBoil.ParNo, dbo.tblMixIssuesBoil.PktNo, dbo.tblMixIssuesBoil.Sec, dbo.tblMixIssuesBoil.IssPcs," & _
                        "dbo.tblMixIssuesBoil.EmpNo, dbo.tblMixIssuesBoil.IssDate, dbo.tblMixIssuesBoil.IssTime, dbo.tblMixIssuesBoil.EmpNo2, dbo.tblMixIssuesBoil.OK," & _
                        "dbo.tblMixIssuesBoil.BatchNo, dbo.tblMixIssuesBoil.SendDate, dbo.tblMixIssuesBoil.SendTime " & _
                      "FROM dbo.tblMixIssuesBoil LEFT OUTER JOIN dbo.tblMixReturnsBoil ON dbo.tblMixIssuesBoil.ID = dbo.tblMixReturnsBoil.IssueID AND dbo.tblMixIssuesBoil.ParNo = dbo.tblMixReturnsBoil.ParNo AND " & _
                        "dbo.tblMixIssuesBoil.PktNo = dbo.tblMixReturnsBoil.PktNo AND dbo.tblMixIssuesBoil.Sec = dbo.tblMixReturnsBoil.Sec " & _
                      "WHERE (dbo.tblMixIssuesBoil.Sec = 1) AND (dbo.tblMixIssuesBoil.OK = 0) AND (dbo.tblMixReturnsBoil.PktNo IS NULL) " & _
                      "ORDER BY dbo.tblMixIssuesBoil.ParNo, dbo.tblMixIssuesBoil.PktNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails2.Rows.Add(rsComSql.Fields("ParNo").Value,
                                     rsComSql.Fields("PktNo").Value,
                                     rsComSql.Fields("IssPcs").Value,
                                     Format(rsComSql.Fields("IssDate").Value, "yyyy/MM/dd"),
                                     Format(rsComSql.Fields("IssTime").Value, "HH:mm"),
                                     False,
                                     rsComSql.Fields("ID").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub ClearFields2()
        GetNextBoilingtNo()
        Load_Packets()
        chkSelect2.Checked = False
        txtTotPcs.Text = ""
        txtTotCount.Text = ""
    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        ClearFields2()
    End Sub

    Private Sub chkSelect2_CheckedChanged(sender As Object) Handles chkSelect2.CheckedChanged
        If chkSelect2.Checked = True Then
            For intRow = 0 To flxDetails2.RowCount - 1
                flxDetails2.Item(5, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails2.RowCount - 1
                flxDetails2.Item(5, intRow).Value = False
            Next
        End If
        txtTotPcs.Text = CalTotalPcs2(flxDetails2, 2)
        txtTotCount.Text = CalTotalCount(flxDetails2)
    End Sub

    Private Function CalTotalPcs2(ByVal flxSample As System.Windows.Forms.DataGridView, ByVal intCalRow As Integer) As Integer
        Dim intRow As Integer

        CalTotalPcs2 = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(5).EditedFormattedValue = True Then
                CalTotalPcs2 = CalTotalPcs2 + Val(flxSample.Item(intCalRow, intRow).Value)
            End If
        Next
        Return CalTotalPcs2

    End Function

    Private Function CalTotalCount(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalCount = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            If flxSample.Rows(intRow).Cells(5).EditedFormattedValue = True Then
                CalTotalCount = CalTotalCount + 1
            End If
        Next
        Return CalTotalCount

    End Function

    Private Sub Save2()
        Dim intRow As Integer

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            If txtBoilingNo.Text <> "" Then
                For intRow = 0 To flxDetails2.Rows.Count - 1
                    If flxDetails2.Item(5, intRow).Value = True Then
                        AdoCN.Execute("UPDATE tblMixIssuesBoil SET OK = 1, BatchNo = " & CDbl(txtBoilingNo.Text) & ",SendDate = '" & Format(Date.Now, "MM/dd/yyyy") & "',SendTime = '" & Format(Date.Now, "HH:mm") & "' " & _
                                      "WHERE ID = " & CDbl(flxDetails2.Item(6, intRow).Value) & "")
                    End If
                Next

                MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

                objForm = New frm_DCLReportViewer
                mReportName = "crptMixBoilingIssRec15.rpt"
                strReportPath = PBReportPath & strFolderPath & mReportName
                objForm.Show()

                ClearFields2()
            Else
                MsgBox("Invalid Boiling No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        End If
    End Sub

    Private Sub cmdSticker_Click(sender As Object, e As EventArgs) Handles cmdSticker.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptMixBoilingIssRec15.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub cmdSave2_Click(sender As Object, e As EventArgs) Handles cmdSave2.Click
        Save2()
    End Sub

    Private Sub frm_MixBoilingManual_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        ClearFields()
        ClearFields2()
    End Sub
End Class