
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_ExpSizingIssue
    Private Sub ClearFields()
        flxDetails.Rows.Clear()
        txtIssPcs.Text = ""
        txtIssCts.Text = ""
        txtRetPcs.Text = ""
        txtRetCts.Text = ""
        txtEmp.Text = ""
    End Sub

    Private Sub cmdEmp_Click(sender As Object, e As EventArgs) Handles cmdEmp.Click
        Datavalid = False
        Parcel = False
        Instring = UCase(InputBox("Enter Emp No"))
        ParcelLen = Len(Instring)
        If ParcelLen = 6 Then
            Datavalid = True

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT FullEmpNo FROM VW_EMP_MASTER_SMALL3 WHERE (DepartmentName LIKE 'GRADING%' OR DepartmentName LIKE 'PCU%') AND (Pay = 1) AND (FullEmpNo = '" & Trim(Instring) & "')", AdoCN, 1, 1)
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
        Dim dblIssPcs As Double
        Dim dblIssCts As Double

        flxDetails.Rows.Clear()

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        dblIssPcs = 0
        dblIssCts = 0
        rsComSql = New ADODB.Recordset
        If strDBName = "DiaStock" Then
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblExpSizingPacket.Department, dbo.tblExpSizingPacket.ParNo, dbo.tblExpSizingPacket.PktNo, " & _
                            "dbo.tblExpSizingPacket.PktPcs, dbo.tblExpSizingPacket.PktCts " & _
                          "FROM dbo.tblParcel INNER JOIN dbo.tblExpSizingPacket ON dbo.tblParcel.Depart = dbo.tblExpSizingPacket.Department AND  " & _
                            "dbo.tblParcel.GrpParNo = dbo.tblExpSizingPacket.ParNo LEFT OUTER JOIN " & _
                            "dbo.tblExpSizingIssues ON dbo.tblExpSizingPacket.Department = dbo.tblExpSizingIssues.Department AND " & _
                            "dbo.tblExpSizingPacket.ParNo = dbo.tblExpSizingIssues.ParNo And dbo.tblExpSizingPacket.PktNo = dbo.tblExpSizingIssues.PktNo " & _
                          "WHERE (dbo.tblExpSizingPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblExpSizingIssues.PktNo IS NULL) AND (dbo.tblParcel.Complete = 0) " & _
                          "ORDER BY dbo.tblExpSizingPacket.Department, dbo.tblExpSizingPacket.ParNo, dbo.tblExpSizingPacket.PktNo", AdoCN, 1, 1)
        Else
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblExpSizingPacket.Department, dbo.tblExpSizingPacket.ParNo, dbo.tblExpSizingPacket.PktNo, dbo.tblExpSizingPacket.PktPcs, dbo.tblExpSizingPacket.PktCts " & _
                          "FROM dbo.tblExpSizingPacket LEFT OUTER JOIN dbo.tblExpSizingIssues ON dbo.tblExpSizingPacket.Department = dbo.tblExpSizingIssues.Department AND dbo.tblExpSizingPacket.ParNo = dbo.tblExpSizingIssues.ParNo AND " & _
                            "dbo.tblExpSizingPacket.PktNo = dbo.tblExpSizingIssues.PktNo " & _
                          "WHERE (dbo.tblExpSizingPacket.Department = '" & cmbDept.Text & "') AND (dbo.tblExpSizingIssues.PktNo IS NULL) " & _
                          "ORDER BY dbo.tblExpSizingPacket.Department, dbo.tblExpSizingPacket.ParNo, dbo.tblExpSizingPacket.PktNo", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("Department").Value,
                                    rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("PktPcs").Value,
                                    rsComSql.Fields("PktCts").Value,
                                    False)

                dblIssPcs = dblIssPcs + rsComSql.Fields("PktPcs").Value
                dblIssCts = dblIssCts + rsComSql.Fields("PktCts").Value
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        txtIssPcs.Text = dblIssPcs
        txtIssCts.Text = Format(dblIssCts, "#0.000")
        txtRetPcs.Text = CalTotalPcs()
        txtRetCts.Text = Format(CalTotalCts, "#0.000")
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        Load_Details()
    End Sub

    Private Function CalTotalPcs() As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(5, intRow).Value = True Then
                CalTotalPcs = CalTotalPcs + CInt(flxDetails.Item(3, intRow).Value)
            End If
        Next
        Return CalTotalPcs
    End Function

    Private Function CalTotalCts() As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(5, intRow).Value = True Then
                CalTotalCts = CalTotalCts + CDbl(flxDetails.Item(4, intRow).Value)
            End If
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
        Return CalTotalCts
    End Function

    Private Sub chkSelect_CheckedChanged(sender As Object) Handles chkSelect.CheckedChanged
        Dim intRow As Integer

        If chkSelect.Checked = True Then
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(5, intRow).Value = True
            Next
        Else
            For intRow = 0 To flxDetails.RowCount - 1
                flxDetails.Item(5, intRow).Value = False
            Next
        End If
        txtRetPcs.Text = CalTotalPcs()
        txtRetCts.Text = CalTotalCts()
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean
        Dim intSec As Integer

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtEmp.Text = "" Then MsgBox("Invalid Employee No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        blnSave = False

        intSec = 1
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(5, intRow).Value = "1" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblExpSizingIssues WHERE Department = '" & flxDetails.Item(0, intRow).Value & "' AND ParNo = '" & flxDetails.Item(1, intRow).Value & "' AND PktNo = '" & flxDetails.Item(2, intRow).Value & "' AND Sec = " & intSec & "", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                    MsgBox("Already Issued - " & flxDetails.Item(1, intRow).Value & "/" & flxDetails.Item(2, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql = Nothing
            End If
        Next

        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(5, intRow).Value = "1" Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblExpSizingIssues WHERE Department = '" & flxDetails.Item(0, intRow).Value & "' AND ParNo = '" & flxDetails.Item(1, intRow).Value & "' AND PktNo = '" & flxDetails.Item(2, intRow).Value & "' AND Sec = " & intSec & "", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    blnSave = True
                    AdoCN.Execute("INSERT INTO tblExpSizingIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                  "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','" & flxDetails.Item(2, intRow).Value & "'," & intSec & ",'" & Mid(Trim(txtEmp.Text), 1, 6) & "','" & CInt(flxDetails.Item(3, intRow).Value) & "'," & _
                                         "'" & CDbl(flxDetails.Item(4, intRow).Value) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")
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

    Private Sub flxDetails_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellValueChanged
        txtRetPcs.Text = CalTotalPcs()
        txtRetCts.Text = CalTotalCts()
    End Sub

    Private Sub frm_ExpSizingIssue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_DepartmentProd(cmbDept)
    End Sub
End Class