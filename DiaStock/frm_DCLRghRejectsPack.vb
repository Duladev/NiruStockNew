
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLRghRejectsPack

    Private Sub frm_DCLRghRejectsPack_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        optReject.Checked = True
        GetPackNo()
        Load_Parcels()
    End Sub

    Private Sub GetPackNo()
        rsComSql = New ADODB.Recordset
        If optReject.Checked = True Then
            rsComSql.Open("SELECT MAX(PackNo) AS MaxNo FROM tblParcelDetails", AdoCN, 1, 1)
        Else
            If optSales.Checked = True Then
                rsComSql.Open("SELECT MAX(PackNo) AS MaxNo FROM tblParcelRghSales", AdoCN, 1, 1)
            Else
                rsComSql.Open("SELECT MAX(PackNo) AS MaxNo FROM tblParcelReturns", AdoCN, 1, 1)
            End If
        End If
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

    Private Sub Load_Parcels()
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        If optReject.Checked = True Then
            rsComSql.Open("SELECT * FROM tblParcelDetails WHERE PackNo = 0 ORDER BY ParcelNo", AdoCN, 1, 1)
        Else
            If optSales.Checked = True Then
                rsComSql.Open("SELECT * FROM tblParcelRghSales WHERE PackNo = 0 ORDER BY ParcelNo", AdoCN, 1, 1)
            Else
                rsComSql.Open("SELECT * FROM tblParcelReturns WHERE PackNo = 0 ORDER BY ParcelNo", AdoCN, 1, 1)
            End If
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("ParcelNo").Value,
                                    rsComSql.Fields("PktPcs").Value,
                                    rsComSql.Fields("PktCts").Value, False)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Save()
        Dim intRow As Integer
        Dim blnSave As Boolean

        blnSave = False
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(3, intRow).Value = True Then
                    blnSave = True

                    If optReject.Checked = True Then
                        AdoCN.Execute("UPDATE tblParcelDetails SET PackNo = " & CDbl(txtPackNo.Text) & " " & _
                                      "WHERE ParcelNo = '" & flxDetails.Item(0, intRow).Value & "'")
                    Else
                        If optSales.Checked = True Then
                            AdoCN.Execute("UPDATE tblParcelRghSales SET PackNo = " & CDbl(txtPackNo.Text) & " " & _
                                 "WHERE ParcelNo = '" & flxDetails.Item(0, intRow).Value & "'")
                        Else
                            AdoCN.Execute("UPDATE tblParcelReturns SET PackNo = " & CDbl(txtPackNo.Text) & " " & _
                                 "WHERE ParcelNo = '" & flxDetails.Item(0, intRow).Value & "'")
                        End If
                    End If
                End If
            Next
            If blnSave = True Then
                MsgBox("Updated Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Else
                MsgBox("No Records to Add", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            Load_Parcels()
            GetPackNo()
        End If
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        GetPackNo()
        Load_Parcels()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub optReject_Click(sender As Object, e As EventArgs) Handles optReject.Click
        GetPackNo()
        Load_Parcels()
    End Sub

    Private Sub optContract_Click(sender As Object, e As EventArgs) Handles optContract.Click
        GetPackNo()
        Load_Parcels()
    End Sub

    Private Sub optSales_Click(sender As Object, e As EventArgs) Handles optSales.Click
        GetPackNo()
        Load_Parcels()
    End Sub
End Class