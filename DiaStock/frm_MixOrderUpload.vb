
Imports System.Data
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frm_MixOrderUpload
    Dim strFolderPath As String

    Private Sub ClearText()
        flxDetails.Rows.Clear()
        chkUpdate.Checked = True
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

        If dbConnEZOnline.State = 1 Then
            dbConnEZOnline.Close()
        End If
        dbConnEZOnline.ConnectionString = "Provider=SQLOLEDB;Data Source='192.168.10.42';Connect Timeout=60;Initial Catalog=EZOnline;User ID=Chameera;Password=987321Cm!;"
        dbConnEZOnline.Open()

        blnAccess = False

        ExpProgress.Minimum = 0
        ExpProgress.Visible = True
        ExpProgress.Maximum = flxDetails.Rows.Count

        For intRow = 0 To flxDetails.Rows.Count - 1
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT LineNumber FROM PrecisionOrders WHERE LineNumber = '" & flxDetails.Item(0, intRow).Value & "'", dbConnEZOnline, 1, 1)
            If rsComSql.RecordCount = 0 Then
                dbConnEZOnline.Execute("INSERT INTO PrecisionOrders(LineNumber,OrderNumberIL,OrderType,ClientCode,ClientOrderNumber,OrderItem,DateOfOrder,Client,CustomerReference,CustomerStoneRef," & _
                                        "NumberOfStonesPerSet,NumberOfStones,TotalAmountStones,CostPerStone,TotalCostDiamonds,QtyForLaser,QtyForGrooving,QtyForSetting,TotalCostServices,TotalCostOrder," & _
                                        "DueDate,Length,Width,MaxCostPerStone,Details,BalanceToSupply,Pieces,Status,Color,Clarity,Cancelled) " & _
                                       "VALUES('" & flxDetails.Item(0, intRow).Value & "','" & flxDetails.Item(1, intRow).Value & "','DCL','','" & flxDetails.Item(2, intRow).Value & "','" & flxDetails.Item(3, intRow).Value & "'," & _
                                        "'" & flxDetails.Item(4, intRow).Value & "','" & flxDetails.Item(5, intRow).Value & "','" & flxDetails.Item(6, intRow).Value & "','" & Replace(flxDetails.Item(7, intRow).Value, "'", "''") & "'," & _
                                        "'" & flxDetails.Item(8, intRow).Value & "','" & flxDetails.Item(9, intRow).Value & "','" & flxDetails.Item(10, intRow).Value & "','" & flxDetails.Item(11, intRow).Value & "'," & _
                                        "'" & flxDetails.Item(12, intRow).Value & "','" & flxDetails.Item(13, intRow).Value & "','" & flxDetails.Item(14, intRow).Value & "',0,'" & flxDetails.Item(15, intRow).Value & "'," & _
                                        "'" & flxDetails.Item(16, intRow).Value & "','" & flxDetails.Item(17, intRow).Value & "','" & flxDetails.Item(18, intRow).Value & "','" & flxDetails.Item(19, intRow).Value & "'," & _
                                        "'" & flxDetails.Item(20, intRow).Value & "','','" & flxDetails.Item(10, intRow).Value & "',0,'Open','" & flxDetails.Item(22, intRow).Value & "','" & flxDetails.Item(23, intRow).Value & "'," & _
                                        "'" & flxDetails.Item(25, intRow).Value & "')")
            Else
                dbConnEZOnline.Execute("UPDATE PrecisionOrders SET OrderNumberIL = '" & flxDetails.Item(1, intRow).Value & "',ClientOrderNumber = '" & flxDetails.Item(2, intRow).Value & "'," & _
                                        "OrderItem = '" & flxDetails.Item(3, intRow).Value & "',DateOfOrder = '" & flxDetails.Item(4, intRow).Value & "',Client = '" & flxDetails.Item(5, intRow).Value & "'," & _
                                        "CustomerReference = '" & flxDetails.Item(6, intRow).Value & "',CustomerStoneRef = '" & Replace(flxDetails.Item(7, intRow).Value, "'", "''") & "'," & _
                                        "NumberOfStonesPerSet = '" & flxDetails.Item(8, intRow).Value & "',NumberOfStones = '" & flxDetails.Item(9, intRow).Value & "',TotalAmountStones = '" & flxDetails.Item(10, intRow).Value & "'," & _
                                        "CostPerStone = '" & flxDetails.Item(11, intRow).Value & "',TotalCostDiamonds = '" & flxDetails.Item(12, intRow).Value & "',QtyForLaser = '" & flxDetails.Item(13, intRow).Value & "'," & _
                                        "QtyForGrooving = '" & flxDetails.Item(14, intRow).Value & "',TotalCostServices = '" & flxDetails.Item(15, intRow).Value & "',TotalCostOrder = '" & flxDetails.Item(16, intRow).Value & "'," & _
                                        "DueDate = '" & flxDetails.Item(17, intRow).Value & "',Length = '" & flxDetails.Item(18, intRow).Value & "',Width = '" & flxDetails.Item(19, intRow).Value & "'," & _
                                        "MaxCostPerStone = '" & flxDetails.Item(20, intRow).Value & "',Details = '',BalanceToSupply = '" & flxDetails.Item(10, intRow).Value & "',Pieces = 0,Color = '" & flxDetails.Item(22, intRow).Value & "'," & _
                                        "Clarity = '" & flxDetails.Item(23, intRow).Value & "',Cancelled = '" & flxDetails.Item(25, intRow).Value & "' " & _
                                       "WHERE LineNumber = '" & flxDetails.Item(0, intRow).Value & "'")
            End If
            rsComSql = Nothing

            If chkUpdate.Checked = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT LineNumber FROM PrecisionOrders WHERE LineNumber = '" & flxDetails.Item(0, intRow).Value & "'", dbConnEZOnline, 1, 1)
                If rsComSql.RecordCount Then
                    AdoCN.Execute("UPDATE tblOrdersDtlsO SET Upload = 1 WHERE ID = " & flxDetails.Item(21, intRow).Value & "")
                End If
                rsComSql = Nothing
            End If

            ExpProgress.Value = intRow + 1
        Next
        ExpProgress.Visible = False

        MsgBox("Offers Uploaded Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        Load_OrderDetails()
    End Sub

    Private Sub Load_OrderDetails()
        Dim blnFound As Boolean
        Dim dblTotPcs As Double
        Dim dblTotalSales As Double
        Dim dblTotalService As Double
        Dim dblTotalCostDiamond As Double
        Dim dblTotalCostOrder As Double
        Dim strPrice As String
        Dim strMaxCost As String

        ClearText()
        blnFound = False

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM VW_MixOrderOfferEZ ORDER BY NorderNo, OrderItem, ID", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                dblTotPcs = rsComSql.Fields("Sets").Value * rsComSql.Fields("PCs").Value
                dblTotalSales = dblTotPcs * rsComSql.Fields("SalesPrice").Value
                dblTotalService = (dblTotPcs * rsComSql.Fields("GrCount").Value * rsComSql.Fields("GrRate").Value) + (dblTotPcs * rsComSql.Fields("Laser").Value * rsComSql.Fields("LaserRate").Value)

                If strRight(rsComSql.Fields("Niruref").Value, 3) = "164" And rsComSql.Fields("MaxType").Value = "C" Then
                    dblTotalCostOrder = 0
                    dblTotalCostDiamond = 0
                    strPrice = "@" & rsComSql.Fields("SalesPrice").Value
                    strMaxCost = "@" & rsComSql.Fields("MaxCost").Value

                ElseIf strRight(rsComSql.Fields("Niruref").Value, 3) = "151" And rsComSql.Fields("MaxType").Value = "C" Then
                    dblTotalCostOrder = 0
                    dblTotalCostDiamond = 0
                    strPrice = "@" & rsComSql.Fields("SalesPrice").Value
                    strMaxCost = "@" & rsComSql.Fields("MaxCost").Value

                ElseIf strRight(rsComSql.Fields("Niruref").Value, 3) = "215" And rsComSql.Fields("MaxType").Value = "C" Then
                    dblTotalCostOrder = rsComSql.Fields("SalesPrice").Value
                    dblTotalCostDiamond = rsComSql.Fields("SalesPrice").Value
                    strPrice = "@" & rsComSql.Fields("SalesPrice").Value
                    strMaxCost = "@" & rsComSql.Fields("MaxCost").Value

                Else
                    dblTotalCostOrder = dblTotalSales + dblTotalService
                    dblTotalCostDiamond = dblTotalSales
                    strPrice = rsComSql.Fields("SalesPrice").Value
                    strMaxCost = rsComSql.Fields("MaxCost").Value
                End If

                dblTotalCostOrder = Math.Round(dblTotalCostOrder, 2)
                dblTotalCostDiamond = Math.Round(dblTotalCostDiamond, 2)

                flxDetails.Rows.Add(rsComSql.Fields("NLineNo").Value, rsComSql.Fields("NorderNo").Value,
                                    rsComSql.Fields("COMMANDE").Value, rsComSql.Fields("OrderItem").Value,
                                    Format(rsComSql.Fields("OrdDate").Value, "yyyy/MM/dd"), strRight(rsComSql.Fields("Niruref").Value, 3),
                                    rsComSql.Fields("Subject").Value, rsComSql.Fields("RefNo").Value,
                                    rsComSql.Fields("Sets").Value, rsComSql.Fields("PCs").Value,
                                    dblTotPcs, strPrice, dblTotalCostDiamond,
                                    rsComSql.Fields("Laser").Value, rsComSql.Fields("GrCount").Value,
                                    dblTotalService, dblTotalCostOrder,
                                    Format(rsComSql.Fields("DueDate").Value, "yyyy/MM/dd"),
                                    rsComSql.Fields("Length").Value, rsComSql.Fields("Width").Value,
                                    strMaxCost, rsComSql.Fields("ID").Value,
                                    rsComSql.Fields("Color").Value, rsComSql.Fields("Clarity").Value,
                                    rsComSql.Fields("Flo").Value, Format(rsComSql.Fields("EstDueDate").Value, "yyyy/MM/dd"),
                                    rsComSql.Fields("OrderType").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub CloseOrder()
        Dim intRow As Integer
        Dim blnAccess As Boolean

        blnAccess = False

        ExpProgress.Minimum = 0
        ExpProgress.Visible = True
        ExpProgress.Maximum = flxDetails.Rows.Count

        For intRow = 0 To flxDetails.Rows.Count - 1
            AdoCN.Execute("UPDATE tblOrdersDtlsO SET Upload = 1 WHERE ID = " & flxDetails.Item(21, intRow).Value & "")

            ExpProgress.Value = intRow + 1
        Next
        ExpProgress.Visible = False

        MsgBox("Offers Closed Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        Load_OrderDetails()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        PBResponse = MsgBox("Are you sure to Upload?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            SaveOrder()
        End If
    End Sub

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub frm_MixOrderUpload_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
        PBResponse = MsgBox("Are you sure to Close?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            CloseOrder()
        End If
    End Sub
End Class