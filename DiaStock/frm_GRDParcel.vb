
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDParcel
    Private Sub Load_Parcels()
        Dim dblPCUPending As Double

        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblGrading_Parcel.ParNo, " & _
                            "ISNULL(SUM(dbo.tblGrading_CheckingReturns.ExPcs + dbo.tblGrading_CheckingReturns.VgPcs + dbo.tblGrading_CheckingReturns.BlPcs + dbo.tblGrading_CheckingReturns.ScPcs " & _
                            "+ dbo.tblGrading_CheckingReturns.SzPcs + dbo.tblGrading_CheckingReturns.OkPcs), 0) AS OkPcs, ISNULL(SUM(dbo.tblGrading_CheckingReturns.RepPcs), 0) " & _
                            "AS RepPcs, ISNULL(SUM(dbo.tblGrading_CheckingReturns.PsPcs), 0) AS PsPcs, dbo.tblGrading_Parcel.SystemDateTime, dbo.tblGrading_Parcel.Transfer " & _
                      "FROM dbo.tblGrading_Parcel LEFT OUTER JOIN dbo.tblGrading_CheckingReturns ON dbo.tblGrading_Parcel.ParNo = LEFT(dbo.tblGrading_CheckingReturns.ParNo, 6) " & _
                      "WHERE (dbo.tblGrading_Parcel.Complete = 0) AND (dbo.tblGrading_Parcel.Dept = 'Grading') " & _
                      "GROUP BY dbo.tblGrading_Parcel.ParNo, dbo.tblGrading_Parcel.SystemDateTime, dbo.tblGrading_Parcel.Transfer " & _
                      "ORDER BY dbo.tblGrading_Parcel.ParNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                dblPCUPending = 0
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT TOP (100) PERCENT LEFT(ParNo, 6) AS ParNo, SUM(Pcs) AS Pcs " & _
                                "FROM dbo.tblExpSizingTypes " & _
                                "WHERE (OK = 0) " & _
                                "GROUP BY LEFT(ParNo, 6) " & _
                                "HAVING (LEFT(ParNo, 6) = '" & rsComSql.Fields("ParNo").Value & "') " & _
                                "ORDER BY ParNo", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                        dblPCUPending = rsComSql_1.Fields("Pcs").Value
                    End If
                End If
                rsComSql_1 = Nothing

                flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                    False,
                                    rsComSql.Fields("Transfer").Value,
                                    Format(rsComSql.Fields("SystemDateTime").Value, "yyyy/MM/dd"),
                                    rsComSql.Fields("OkPcs").Value,
                                    rsComSql.Fields("RepPcs").Value,
                                    rsComSql.Fields("PsPcs").Value,
                                    dblPCUPending)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblGrading_Parcel.ParNo, SUM(dbo.tblGrading_CheckingReturns.ExPcs + dbo.tblGrading_CheckingReturns.VgPcs + dbo.tblGrading_CheckingReturns.BlPcs + dbo.tblGrading_CheckingReturns.ScPcs " & _
                            "+ dbo.tblGrading_CheckingReturns.SzPcs + dbo.tblGrading_CheckingReturns.OkPcs) AS OkPcs, SUM(dbo.tblGrading_CheckingReturns.RepPcs) AS RepPcs, SUM(dbo.tblGrading_CheckingReturns.PsPcs) AS PsPcs, " & _
                            "dbo.tblGrading_Parcel.SystemDateTime, dbo.tblGrading_Parcel.Transfer " & _
                      "FROM dbo.tblGrading_Parcel INNER JOIN dbo.tblGrading_CheckingReturns ON dbo.tblGrading_Parcel.ParNo = dbo.tblGrading_CheckingReturns.ParNo " & _
                      "WHERE (dbo.tblGrading_Parcel.Complete = 0) AND (dbo.tblGrading_Parcel.Dept = 'Grading') AND (LEN(dbo.tblGrading_Parcel.ParNo) > 6) AND (dbo.tblGrading_CheckingReturns.Department LIKE 'GradingPCU%') " & _
                      "GROUP BY dbo.tblGrading_Parcel.ParNo, dbo.tblGrading_Parcel.SystemDateTime, dbo.tblGrading_Parcel.Transfer " & _
                      "ORDER BY dbo.tblGrading_Parcel.ParNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                dblPCUPending = 0
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT TOP (100) PERCENT ParNo ParNo, SUM(Pcs) AS Pcs " & _
                                "FROM dbo.tblExpSizingTypes " & _
                                "WHERE (OK = 0) " & _
                                "GROUP BY ParNo " & _
                                "HAVING (ParNo = '" & rsComSql.Fields("ParNo").Value & "') " & _
                                "ORDER BY ParNo", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("Pcs").Value) Then
                        dblPCUPending = rsComSql_1.Fields("Pcs").Value
                    End If
                End If
                rsComSql_1 = Nothing

                flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                    False,
                                    rsComSql.Fields("Transfer").Value,
                                    Format(rsComSql.Fields("SystemDateTime").Value, "yyyy/MM/dd"),
                                    rsComSql.Fields("OkPcs").Value,
                                    rsComSql.Fields("RepPcs").Value,
                                    rsComSql.Fields("PsPcs").Value,
                                    dblPCUPending)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

    End Sub

    Private Sub frm_GRDParcel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        Load_Parcels()
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean

        blnSave = False
        For intRow = 0 To flxDetails.Rows.Count - 1
            If flxDetails.Item(1, intRow).Value = "1" Then
                AdoCN.Execute("UPDATE tblGrading_Parcel SET Complete = 1, SaveBy = '" & PBUser_EmpNo & "',SaveDate = '" & Date.Now & "' WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND Dept = 'Grading'")
                blnSave = True
            End If

            If flxDetails.Item(2, intRow).Value = "1" Then
                AdoCN.Execute("UPDATE tblGrading_Parcel SET Transfer = 1 WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND Dept = 'Grading'")
                blnSave = True
            End If
        Next

        If blnSave = True Then
            Load_Parcels()
        End If

    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub
End Class