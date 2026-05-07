
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_HRDIncentiveNew
    Dim strYear As String
    Dim strMonth As String

    Private Sub Load_DeptInc()
        cmbDept.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT DepartmentName FROM dbo.VW_EMP_MASTER_SMALL2 WHERE (DEACTIVATE = 0) GROUP BY DepartmentName ORDER BY DepartmentName", dbConn, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbDept.Items.Add(rsComSql.Fields("DepartmentName").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_PayMonth()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT PAY_DATE FROM PAYROLL.dbo.PAY_SYS_PAR WHERE COMP_CODE = 'DC'", dbConn, 1, 1)
        If rsComSql.RecordCount = 1 Then
            dtpMonth.Value = Format(rsComSql.Fields("PAY_DATE").Value, "yyyy/MM")
            'dtpMonth.Value = Format(CDate("05/01/2019"), "yyyy/MM")
            strMonth = Format(dtpMonth.Value, "MM")
            strYear = Format(dtpMonth.Value, "yyyy")
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmbDept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDept.SelectedIndexChanged
        cmbGrp.Items.Clear()
        cmbSection.Items.Clear()

        cmbGrp.Text = ""
        cmbSection.Text = ""
        flxDetails.Rows.Clear()

        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT TOP (100) PERCENT GRP_DESC FROM dbo.VW_EMP_MASTER_SMALL2 WHERE (DEACTIVATE = 0) AND (DepartmentName = '" & cmbDept.Text & "') GROUP BY GRP_DESC ORDER BY GRP_DESC", dbConn, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                cmbGrp.Items.Add(rsComSql_1.Fields("GRP_DESC").Value)

                rsComSql_1.MoveNext()
            End While
        End If
        rsComSql_1 = Nothing

        cmbGrp.Focus()
    End Sub

    Private Sub cmbGrp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbGrp.SelectedIndexChanged
        cmbSection.Items.Clear()

        cmbSection.Text = ""
        flxDetails.Rows.Clear()

        rsComSql_2 = New ADODB.Recordset
        rsComSql_2.Open("SELECT TOP (100) PERCENT SECTION_DESC FROM dbo.VW_EMP_MASTER_SMALL2 WHERE (DEACTIVATE = 0) AND (DepartmentName = '" & cmbDept.Text & "') AND (GRP_DESC = '" & cmbGrp.Text & "') GROUP BY SECTION_DESC ORDER BY SECTION_DESC", dbConn, 1, 1)
        If rsComSql_2.RecordCount Then
            rsComSql_2.MoveFirst()
            While Not rsComSql_2.EOF
                cmbSection.Items.Add(rsComSql_2.Fields("SECTION_DESC").Value)

                rsComSql_2.MoveNext()
            End While
        End If
        rsComSql_2 = Nothing

        cmbSection.Focus()
    End Sub

    Private Sub ClearFields()
        flxDetails.Rows.Clear()
        txtTotMarks.Text = ""
        txtTotMarks2.Text = ""
        txtAmount.Text = ""
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdProcess_Click(sender As Object, e As EventArgs) Handles cmdProcess.Click

    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxSummary)
    End Sub

    Private Sub Load_Emp()
        Dim dblMarks1 As Double
        Dim dblMarks2 As Double
        Dim dblMarks3 As Double
        Dim dblMarks4 As Double
        Dim dblMarks5 As Double
        Dim dblMarks6 As Double
        Dim dblMarks7 As Double

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbGrp.Text = "" Then MsgBox("Invalid Group", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        flxDetails.Rows.Clear()

        rsComSql_2 = New ADODB.Recordset
        If cmbSection.Text = "" Then
            rsComSql_2.Open("SELECT TOP (100) PERCENT FullEmpNo, Name, SECTION_DESC, CATEGORY, GRADE, DESIGNATION FROM dbo.VW_EMP_MASTER_SMALL2 WHERE (DEACTIVATE = 0) AND (DepartmentName = '" & cmbDept.Text & "') AND (GRP_DESC = '" & cmbGrp.Text & "') AND (IncCat <> 'NO') ORDER BY CATEGORY, SECTION_DESC, FullEmpNo", dbConn, 1, 1)
        Else
            rsComSql_2.Open("SELECT TOP (100) PERCENT FullEmpNo, Name, SECTION_DESC, CATEGORY, GRADE, DESIGNATION FROM dbo.VW_EMP_MASTER_SMALL2 WHERE (DEACTIVATE = 0) AND (DepartmentName = '" & cmbDept.Text & "') AND (GRP_DESC = '" & cmbGrp.Text & "') AND (SECTION_DESC = '" & cmbSection.Text & "') AND (IncCat <> 'NO') ORDER BY CATEGORY, SECTION_DESC, FullEmpNo", dbConn, 1, 1)
        End If
        If rsComSql_2.RecordCount Then
            rsComSql_2.MoveFirst()
            While Not rsComSql_2.EOF
                dblMarks1 = 0
                dblMarks2 = 0
                dblMarks3 = 0
                dblMarks4 = 0
                dblMarks5 = 0
                dblMarks6 = 0
                dblMarks7 = 0

                rsComSql_4 = New ADODB.Recordset
                rsComSql_4.Open("SELECT * FROM tblHR_MarksInc WHERE Year1 = '" & strYear & "' AND Month1 = '" & strMonth & "' AND EmpNo = '" & rsComSql_2.Fields("FullEmpNo").Value & "'", dbConn, 1, 1)
                If rsComSql_4.RecordCount Then
                    dblMarks1 = rsComSql_4.Fields("Marks1").Value
                    dblMarks2 = rsComSql_4.Fields("Marks2").Value
                    dblMarks3 = rsComSql_4.Fields("Marks3").Value
                    dblMarks4 = rsComSql_4.Fields("Marks4").Value
                    dblMarks5 = rsComSql_4.Fields("Marks5").Value
                    dblMarks6 = rsComSql_4.Fields("Marks6").Value
                    dblMarks7 = rsComSql_4.Fields("Marks7").Value
                End If
                rsComSql_4 = Nothing

                flxDetails.Rows.Add(rsComSql_2.Fields("FullEmpNo").Value,
                                    rsComSql_2.Fields("Name").Value,
                                    rsComSql_2.Fields("SECTION_DESC").Value,
                                    rsComSql_2.Fields("CATEGORY").Value,
                                    rsComSql_2.Fields("GRADE").Value,
                                    rsComSql_2.Fields("DESIGNATION").Value,
                                    dblMarks1,
                                    dblMarks2,
                                    dblMarks3,
                                    dblMarks4,
                                    dblMarks5,
                                    dblMarks6,
                                    dblMarks7)

                rsComSql_2.MoveNext()
            End While
        End If
        rsComSql_2 = Nothing
    End Sub

    Private Sub cmbSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSection.SelectedIndexChanged
        Load_Emp()
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean

        blnSave = False

        For intRow = 0 To flxDetails.Rows.Count - 1
            If Not IsNumeric(flxDetails.Item(6, intRow).Value) = True Then
                MsgBox("Invalid Quality - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(6, intRow).Value) < 0 Then
                MsgBox("Invalid Quality - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(6, intRow).Value) > 30 Then
                MsgBox("Invalid Quality - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Not IsNumeric(flxDetails.Item(7, intRow).Value) = True Then
                MsgBox("Invalid Quantity - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(7, intRow).Value) < 0 Then
                MsgBox("Invalid Quantity - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(7, intRow).Value) > 20 Then
                MsgBox("Invalid Quantity - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Not IsNumeric(flxDetails.Item(8, intRow).Value) = True Then
                MsgBox("Invalid Mgr Comments - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(8, intRow).Value) < 0 Then
                MsgBox("Invalid Mgr Comments - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(8, intRow).Value) > 20 Then
                MsgBox("Invalid Mgr Comments - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Not IsNumeric(flxDetails.Item(9, intRow).Value) = True Then
                MsgBox("Invalid Discipline - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(9, intRow).Value) < 0 Then
                MsgBox("Invalid Discipline - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(9, intRow).Value) > 10 Then
                MsgBox("Invalid Discipline - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Not IsNumeric(flxDetails.Item(10, intRow).Value) = True Then
                MsgBox("Invalid Commitment - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(10, intRow).Value) < 0 Then
                MsgBox("Invalid Commitment - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(10, intRow).Value) > 10 Then
                MsgBox("Invalid Commitment - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Not IsNumeric(flxDetails.Item(11, intRow).Value) = True Then
                MsgBox("Invalid LTO - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(11, intRow).Value) < 0 Then
                MsgBox("Invalid LTO - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(11, intRow).Value) > 20 Then
                MsgBox("Invalid LTO - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Not IsNumeric(flxDetails.Item(12, intRow).Value) = True Then
                MsgBox("Invalid Sp Con - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(12, intRow).Value) < 0 Then
                MsgBox("Invalid Sp Con - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If CDbl(flxDetails.Item(12, intRow).Value) > 20 Then
                MsgBox("Invalid Sp Con - " & flxDetails.Item(0, intRow).Value, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        For intRow = 0 To flxDetails.Rows.Count - 1
            blnSave = True
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblHR_MarksInc WHERE Year1 = '" & strYear & "' AND Month1 = '" & strMonth & "' AND EmpNo = '" & flxDetails.Item(0, intRow).Value & "'", dbConn, 1, 1)
            If rsComSql.RecordCount = 0 Then
                dbConn.Execute("INSERT INTO tblHR_MarksInc(Year1, Month1, EmpNo, Marks1, Marks2, Marks3, Marks4, Marks5, Marks6, Marks7, UserID) " & _
                               "VALUES('" & strYear & "','" & strMonth & "','" & flxDetails.Item(0, intRow).Value & "','" & CDbl(flxDetails.Item(6, intRow).Value) & "'," & _
                                "'" & CDbl(flxDetails.Item(7, intRow).Value) & "','" & CDbl(flxDetails.Item(8, intRow).Value) & "','" & CDbl(flxDetails.Item(9, intRow).Value) & "'," & _
                                "'" & CDbl(flxDetails.Item(10, intRow).Value) & "','" & CDbl(flxDetails.Item(11, intRow).Value) & "','" & CDbl(flxDetails.Item(12, intRow).Value) & "','" & PBUser_EmpNo & "')")
            Else
                dbConn.Execute("UPDATE tblHR_MarksInc SET Marks1 = '" & CDbl(flxDetails.Item(6, intRow).Value) & "', Marks2 = '" & CDbl(flxDetails.Item(7, intRow).Value) & "', Marks3 = '" & CDbl(flxDetails.Item(8, intRow).Value) & "', " & _
                                "Marks4 = '" & CDbl(flxDetails.Item(9, intRow).Value) & "', Marks5 = '" & CDbl(flxDetails.Item(10, intRow).Value) & "', Marks6 = '" & CDbl(flxDetails.Item(11, intRow).Value) & "', Marks7 = '" & CDbl(flxDetails.Item(12, intRow).Value) & "' " & _
                               "WHERE Year1 = '" & strYear & "' AND Month1 = '" & strMonth & "' AND EmpNo = '" & flxDetails.Item(0, intRow).Value & "'")
            End If
            rsComSql = Nothing
        Next

        If blnSave = True Then
            MsgBox("Details Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            ClearFields()
        End If

    End Sub

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Load_Emp()
    End Sub

    Private Sub frm_HRDIncentiveNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_DeptInc()
        Load_PayMonth()
    End Sub
End Class