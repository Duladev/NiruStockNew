
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixBoilingIssue
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
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.VW_MIXFinishIssuesBal.ParNo, dbo.VW_MIXFinishIssuesBal.PktNo, dbo.VW_MIXFinishIssuesBal.BalPcs " & _
                      "FROM dbo.VW_MIXFinishIssuesBal LEFT OUTER JOIN dbo.VW_MixRepairPending ON dbo.VW_MIXFinishIssuesBal.ParNo = dbo.VW_MixRepairPending.ParNo AND  " & _
                        "dbo.VW_MIXFinishIssuesBal.PktNo = dbo.VW_MixRepairPending.PktNo " & _
                      "WHERE(dbo.VW_MixRepairPending.PktNo IS NULL) " & _
                      "ORDER BY dbo.VW_MIXFinishIssuesBal.ParNo, dbo.VW_MIXFinishIssuesBal.PktNo", AdoCN, 1, 1)
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
        Dim intIndex As Integer

        If txtEmp.Text = "" Then MsgBox("Invalid Employee No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        blnSave = False
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(3, intRow).Value = True Then
                blnSave = True
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblMixIssuesRep WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = 1", AdoCN, 1, 1)
                intIndex = rsComSql.RecordCount + 1
                rsComSql = Nothing

                AdoCN.Execute("INSERT INTO tblMixIssuesRep(ParNo,PktNo,Sec,IssPcs,EmpNo,IssDate,IssTime,EmpNo2,IndexNo) " & _
                              "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "',1," & CInt(flxDetails.Item(2, intRow).Value) & ",'" & Mid(Trim(txtEmp.Text), 1, 6) & "'," & _
                                    "'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & PBUser_EmpNo & "','" & intIndex & "')")
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

    Private Sub frm_MixBoilingIssue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub GetNextBoilingtNo()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(BatchNo) AS MaxNo FROM tblMixIssuesRep WHERE (Sec = 1)", AdoCN, 1, 1)
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
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixIssuesRep.ID, dbo.tblMixIssuesRep.ParNo, dbo.tblMixIssuesRep.PktNo, dbo.tblMixIssuesRep.Sec, dbo.tblMixIssuesRep.IssPcs," & _
                        "dbo.tblMixIssuesRep.EmpNo, dbo.tblMixIssuesRep.IssDate, dbo.tblMixIssuesRep.IssTime, dbo.tblMixIssuesRep.EmpNo2, dbo.tblMixIssuesRep.OK," & _
                        "dbo.tblMixIssuesRep.BatchNo, dbo.tblMixIssuesRep.SendDate, dbo.tblMixIssuesRep.SendTime " & _
                      "FROM dbo.tblMixIssuesRep LEFT OUTER JOIN dbo.tblMixReturnsRep ON dbo.tblMixIssuesRep.ID = dbo.tblMixReturnsRep.IssueID AND dbo.tblMixIssuesRep.ParNo = dbo.tblMixReturnsRep.ParNo AND " & _
                        "dbo.tblMixIssuesRep.PktNo = dbo.tblMixReturnsRep.PktNo AND dbo.tblMixIssuesRep.Sec = dbo.tblMixReturnsRep.Sec " & _
                      "WHERE (dbo.tblMixIssuesRep.Sec = 1) AND (dbo.tblMixIssuesRep.OK = 0) AND (dbo.tblMixReturnsRep.PktNo IS NULL) " & _
                      "ORDER BY dbo.tblMixIssuesRep.ParNo, dbo.tblMixIssuesRep.PktNo", AdoCN, 1, 1)
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
                        AdoCN.Execute("UPDATE tblMixIssuesRep SET OK = 1, BatchNo = " & CDbl(txtBoilingNo.Text) & ",SendDate = '" & Format(Date.Now, "MM/dd/yyyy") & "',SendTime = '" & Format(Date.Now, "HH:mm") & "' " & _
                                      "WHERE ID = " & CDbl(flxDetails2.Item(6, intRow).Value) & "")
                    End If
                Next

                MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

                objForm = New frm_DCLReportViewer
                mReportName = "crptMixBoilingIssRec.rpt"
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
        mReportName = "crptMixBoilingIssRec.rpt"
        strReportPath = PBReportPath & strFolderPath & mReportName
        objForm.Show()
    End Sub

    Private Sub cmdSave2_Click(sender As Object, e As EventArgs) Handles cmdSave2.Click
        Save2()
    End Sub
End Class