
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_MixBoilingReturnManual

    Private Sub ClearFields()
        flxDetails.Rows.Clear()
        txtIssPcs.Text = ""
        txtEmp.Text = ""
        chkTrf.Checked = False
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
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblMixIssuesBoil.ID, dbo.tblMixIssuesBoil.ParNo, dbo.tblMixIssuesBoil.PktNo, dbo.tblMixIssuesBoil.Sec, dbo.tblMixIssuesBoil.IssPcs, " & _
                        "dbo.tblMixIssuesBoil.EmpNo, dbo.tblMixIssuesBoil.BatchNo " & _
                      "FROM dbo.tblMixIssuesBoil LEFT OUTER JOIN dbo.tblMixReturnsBoil ON dbo.tblMixIssuesBoil.ID = dbo.tblMixReturnsBoil.IssueID AND dbo.tblMixIssuesBoil.ParNo = dbo.tblMixReturnsBoil.ParNo AND " & _
                        "dbo.tblMixIssuesBoil.PktNo = dbo.tblMixReturnsBoil.PktNo And dbo.tblMixIssuesBoil.Sec = dbo.tblMixReturnsBoil.Sec " & _
                      "WHERE (dbo.tblMixReturnsBoil.PktNo IS NULL) AND (dbo.tblMixIssuesBoil.Sec = 1) AND (dbo.tblMixIssuesBoil.OK = 1) " & _
                      "ORDER BY dbo.tblMixIssuesBoil.BatchNo, dbo.tblMixIssuesBoil.ParNo, dbo.tblMixIssuesBoil.PktNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("BatchNo").Value,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("IssPcs").Value,
                                    False,
                                    rsComSql.Fields("ID").Value,
                                    rsComSql.Fields("EmpNo").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

    End Sub

    Private Function CalTotalPcs() As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Rows(intRow).Cells(4).EditedFormattedValue = True Then
                CalTotalPcs = CalTotalPcs + CInt(flxDetails.Item(3, intRow).Value)
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

        If chkTrf.Checked = True Then
            If txtEmp.Text = "" Then MsgBox("Invalid Employee No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        End If

        blnSave = False
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(4, intRow).Value = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblMixReturnsBoil WHERE ParNo = '" & flxDetails.Item(1, intRow).Value & "' AND PktNo = '" & flxDetails.Item(2, intRow).Value & "' AND IssueID = '" & CDbl(flxDetails.Item(5, intRow).Value) & "' AND Sec = 1", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    MsgBox("Already Returned - " & flxDetails.Item(1, intRow).Value & "/" & flxDetails.Item(2, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing
            End If
        Next

        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(4, intRow).Value = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblMixReturnsBoil WHERE ParNo = '" & flxDetails.Item(1, intRow).Value & "' AND PktNo = '" & flxDetails.Item(2, intRow).Value & "' AND IssueID = '" & CDbl(flxDetails.Item(5, intRow).Value) & "' AND Sec = 1", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    blnSave = True
                    AdoCN.Execute("INSERT INTO tblMixReturnsBoil(IssueID,ParNo,PktNo,Sec,RetPcs,EmpNo,RetDate,RetTime,EmpNo2) " & _
                                  "VALUES('" & CDbl(flxDetails.Item(5, intRow).Value) & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(2, intRow).Value & "',1," & CInt(flxDetails.Item(3, intRow).Value) & ",'" & flxDetails.Item(6, intRow).Value & "'," & _
                                    "'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & PBUser_EmpNo & "')")

                    If chkTrf.Checked = True Then
                        AdoCN.Execute("INSERT INTO tblMixIssuesBoil(ParNo,PktNo,Sec,IssPcs,EmpNo,IssDate,IssTime,EmpNo2) " & _
                                      "VALUES('" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(2, intRow).Value & "',4," & CInt(flxDetails.Item(3, intRow).Value) & ",'" & Mid(Trim(txtEmp.Text), 1, 6) & "'," & _
                                        "'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm") & "','" & PBUser_EmpNo & "')")
                    End If
                End If
                rsComSql = Nothing
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
                flxDetails.Item(4, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(4, intRow).Value = False
            Next
        End If
        txtIssPcs.Text = CalTotalPcs()
    End Sub

    Private Sub flxDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellContentClick
        If e.ColumnIndex = 4 Then
            txtIssPcs.Text = CalTotalPcs()
        End If
    End Sub

    Private Sub frm_MixBoilingReturnManual_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub
End Class