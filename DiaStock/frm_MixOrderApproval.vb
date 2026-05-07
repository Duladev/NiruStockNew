
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_MixOrderApproval
    Dim strFolderPath As String

    Private Sub ClearText()
        flxDetails.Rows.Clear()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearText()
        Load_OrderDetails()
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
            If Len(flxDetails.Item(22, intRow).Value) > 0 Then
                AdoCN.Execute("UPDATE tblOrdersDtlsO SET Status = '" & flxDetails.Item(22, intRow).Value & "' WHERE ID = " & flxDetails.Item(21, intRow).Value & "")
            End If

            ExpProgress.Value = intRow + 1
        Next
        ExpProgress.Visible = False

        MsgBox("Orders Updated Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        Load_OrderDetails()
    End Sub

    Private Sub CheckOrder()
        Dim intRow As Integer

        If dbConnEZOnline.State = 1 Then
            dbConnEZOnline.Close()
        End If
        'dbConnEZOnline.ConnectionString = "Provider=SQLOLEDB;Data Source='192.168.10.42';Connect Timeout=60;Initial Catalog=EZOnline;User ID=Chameera;Password=987321Cm!;"
        'dbConnEZOnline.ConnectionString = "Provider=SQLOLEDB;Data Source='192.168.10.42';Connect Timeout=60;Initial Catalog=EZOnline;User ID=Chameera;Password=987321Cm!;"
        'dbConnEZOnline.ConnectionString = "Provider=SQLOLEDB;Data Source='82.166.139.82:1455';Connect Timeout=60;Initial Catalog=EZOnline;User ID=SA;Password=716ezonline95$;"
        dbConnEZOnline.ConnectionString = "Provider=SQLOLEDB;Data Source='82.166.139.82,1455';Connect Timeout=60;Initial Catalog=EZOnline;User ID=SA;Password=716ezonline95$;"
        dbConnEZOnline.Open()

        ExpProgress.Minimum = 0
        ExpProgress.Visible = True
        ExpProgress.Maximum = flxDetails.Rows.Count

        For intRow = 0 To flxDetails.Rows.Count - 1
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT Details, Link FROM PrecisionOrders WHERE LineNumber = '" & flxDetails.Item(0, intRow).Value & "'", dbConnEZOnline, 1, 1)
            If rsComSql.RecordCount Then
                flxDetails.Item(22, intRow).Value = rsComSql.Fields("Details").Value
                flxDetails.Item(23, intRow).Value = rsComSql.Fields("Link").Value
            End If
            rsComSql = Nothing

            ExpProgress.Value = intRow + 1
        Next
        ExpProgress.Visible = False
    End Sub

    Private Sub Load_OrderDetails()
        Dim blnFound As Boolean
        Dim dblTotPcs As Double
        Dim dblTotalSales As Double
        Dim dblTotalService As Double

        ClearText()
        blnFound = False

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM VW_MixOrderOfferEZ1 ORDER BY NorderNo, OrderItem, ID", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                dblTotPcs = rsComSql.Fields("Sets").Value * rsComSql.Fields("PCs").Value
                dblTotalSales = dblTotPcs * rsComSql.Fields("SalesPrice").Value
                dblTotalService = (dblTotPcs * rsComSql.Fields("GrCount").Value * rsComSql.Fields("GrRate").Value) + (dblTotPcs * rsComSql.Fields("Laser").Value * rsComSql.Fields("LaserRate").Value)

                flxDetails.Rows.Add(rsComSql.Fields("NLineNo").Value, rsComSql.Fields("NorderNo").Value,
                                    rsComSql.Fields("COMMANDE").Value, rsComSql.Fields("OrderItem").Value,
                                    Format(rsComSql.Fields("EnterDate").Value, "yyyy/MM/dd"), strRight(rsComSql.Fields("Niruref").Value, 3),
                                    rsComSql.Fields("Subject").Value, rsComSql.Fields("RefNo").Value,
                                    rsComSql.Fields("Sets").Value, rsComSql.Fields("PCs").Value,
                                    dblTotPcs, rsComSql.Fields("SalesPrice").Value, dblTotalSales,
                                    rsComSql.Fields("Laser").Value, rsComSql.Fields("GrCount").Value,
                                    dblTotalService, dblTotalSales + dblTotalService,
                                    Format(rsComSql.Fields("DueDate").Value, "yyyy/MM/dd"),
                                    rsComSql.Fields("Length").Value, rsComSql.Fields("Width").Value,
                                    rsComSql.Fields("MaxCost").Value, rsComSql.Fields("ID").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        PBResponse = MsgBox("Are you sure to Update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            SaveOrder()
        End If
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub frm_MixOrderApproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub cmdCheck_Click(sender As Object, e As EventArgs) Handles cmdCheck.Click
        CheckOrder()
    End Sub
End Class