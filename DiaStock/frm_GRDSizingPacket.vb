
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDSizingPacket

    Private Sub frm_GRDSizingPacket_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        ClearFields()
        Load_Department(cmbDept)
        Load_GradingTypes()
    End Sub

    Private Sub ClearFields()
        cmbDept.Text = ""
        txtParNo.Text = ""
        flxPacket.Rows.Clear()
        txtPktNo.Text = ""
        txtPktPcs.Text = ""
        txtPktCts.Text = ""
        cmbType.Text = ""
        flxDetails.Rows.Clear()
        txtBalPcs.Text = ""
        txtBalCts.Text = ""
        txtTotBalPcs.Text = ""
        txtTotBalCts.Text = ""
        txtRefNo.Text = ""
        txtSide.Text = ""
        txtGiaNo.Text = ""
        txtPktNo3.Text = ""
        txtRate.Text = ""
        txtPlanValue.Text = ""
    End Sub

    Private Sub Load_GradingTypes()
        Dim rsGrdType As New ADODB.Recordset

        rsGrdType = New ADODB.Recordset
        rsGrdType.Open("SELECT * FROM tblGrading_Types WHERE Sec = 1 ORDER BY Type", AdoCN, 1, 1)
        If rsGrdType.RecordCount Then
            rsGrdType.MoveFirst()
            While Not rsGrdType.EOF
                cmbType.Items.Add(rsGrdType.Fields("Type").Value)
                rsGrdType.MoveNext()
            End While
        End If
        rsGrdType = Nothing

    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            txtParNo.Text = UCase(txtParNo.Text)
            If ParcelFound(txtParNo.Text) = True Then
                If cmbDept.Text = "GradingPCU_N" Or cmbDept.Text = "GradingPCU" Then
                    GetNewPacket(2)
                Else
                    GetNewPacket(1)
                End If
            Else
                MsgBox("Invalid Parcel", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                ClearFields()
                txtParNo.Focus()
            End If
        End If
    End Sub

    Private Function ParcelFound(ByVal strParceNo As String) As Boolean
        ParcelFound = True
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblGrading_Returns WHERE ParNo = '" & strParceNo & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
        If rsComSql_1.RecordCount > 0 Then
            ParcelFound = True
        Else
            ParcelFound = False
        End If
        rsComSql_1 = Nothing
        Return ParcelFound
    End Function

    Private Sub GetNewPacket(ByVal intSec As Integer)
        Dim intIssPcs As Integer
        Dim dblIssCts As Double
        Dim dblPlanValue As Double
        Dim intPktLen As Integer

        If intSec = 1 Or intSec = 2 Then
            intPktLen = 4
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT MAX(LEN(PktNo)) AS PktLen FROM tblGrading_SizingPacket WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND LEFT(PktNo, 1) = 'Q'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                If Not IsDBNull(rsComSql.Fields("PktLen").Value) Then
                    intPktLen = rsComSql.Fields("PktLen").Value
                End If
            End If
            rsComSql = Nothing

            If intPktLen = 4 Then
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT MAX(RIGHT(PktNo, 3)) AS MaxPktNo FROM tblGrading_SizingPacket WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND LEFT(PktNo, 1) = 'Q' AND LEN(PktNo) = 4", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("MaxPktNo").Value) Then
                        txtPktNo.Text = "Q" & Format(rsComSql_1.Fields("MaxPktNo").Value + 1, "000")
                    Else
                        txtPktNo.Text = "Q001"
                    End If
                End If
                rsComSql_1 = Nothing

            ElseIf intPktLen = 5 Then
                rsComSql_1 = New ADODB.Recordset
                rsComSql_1.Open("SELECT MAX(RIGHT(PktNo, 4)) AS MaxPktNo FROM tblGrading_SizingPacket WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND LEFT(PktNo, 1) = 'Q' AND LEN(PktNo) = 5", AdoCN, 1, 1)
                If rsComSql_1.RecordCount Then
                    If Not IsDBNull(rsComSql_1.Fields("MaxPktNo").Value) Then
                        txtPktNo.Text = "Q" & Format(rsComSql_1.Fields("MaxPktNo").Value + 1, "0000")
                    Else
                        txtPktNo.Text = "Q0001"
                    End If
                End If
                rsComSql_1 = Nothing

            Else

            End If
        End If

        flxPacket.Rows.Clear()
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        txtTotBalPcs.Text = "0"
        txtTotBalCts.Text = "0"
        rsComSql_1 = New ADODB.Recordset
        If intSec = 1 Then
            'rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.tblGrading_Types.TypeCat, SUM(dbo.tblGrading_ReturnDetails.Pcs) AS TotPcs, ROUND(SUM(dbo.tblGrading_ReturnDetails.Cts), 3) AS TotCts " & _
            '                "FROM dbo.tblGrading_ReturnDetails INNER JOIN dbo.tblGrading_Types ON dbo.tblGrading_ReturnDetails.ReturnType = dbo.tblGrading_Types.Type " & _
            '                "WHERE dbo.tblGrading_ReturnDetails.ParNo = '" & txtParNo.Text & "' AND dbo.tblGrading_ReturnDetails.Sec = 1 AND dbo.tblGrading_ReturnDetails.Department = '" & cmbDept.Text & "' " & _
            '                "GROUP BY dbo.tblGrading_Types.TypeCat " & _
            '                "ORDER BY dbo.tblGrading_Types.TypeCat", AdoCN, 1, 1)

            'rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.tblGrading_ReturnDetails.PktNo, SUM(dbo.tblGrading_ReturnDetails.Pcs) AS TotPcs, ROUND(SUM(dbo.tblGrading_ReturnDetails.Cts), 3) AS TotCts " & _
            '                "FROM dbo.tblGrading_ReturnDetails INNER JOIN dbo.tblGrading_Types ON dbo.tblGrading_ReturnDetails.ReturnType = dbo.tblGrading_Types.Type " & _
            '                "WHERE dbo.tblGrading_ReturnDetails.ParNo = '" & txtParNo.Text & "' AND dbo.tblGrading_ReturnDetails.Sec = 1 AND dbo.tblGrading_ReturnDetails.Department = '" & cmbDept.Text & "' " & _
            '                "GROUP BY dbo.tblGrading_ReturnDetails.PktNo " & _
            '                "ORDER BY dbo.tblGrading_ReturnDetails.PktNo", AdoCN, 1, 1)

            'rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.tblGrading_ReturnDetails.PktNo, SUM(dbo.tblGrading_ReturnDetails.Pcs) AS TotPcs, ROUND(SUM(dbo.tblGrading_ReturnDetails.Cts), 3) AS TotCts, " & _
            '                    "ISNULL(dbo.tblGrading_RepairParcelsA.PktNo2, dbo.tblGrading_ReturnDetails.PktNo) AS PktNo2 " & _
            '                "FROM dbo.tblGrading_ReturnDetails INNER JOIN dbo.tblGrading_Types ON dbo.tblGrading_ReturnDetails.ReturnType = dbo.tblGrading_Types.Type LEFT OUTER JOIN " & _
            '                    "dbo.tblGrading_RepairParcelsA ON dbo.tblGrading_ReturnDetails.Department = dbo.tblGrading_RepairParcelsA.Department AND " & _
            '                    "dbo.tblGrading_ReturnDetails.ParNo = dbo.tblGrading_RepairParcelsA.ParNo And dbo.tblGrading_ReturnDetails.PktNo = dbo.tblGrading_RepairParcelsA.PktNo " & _
            '                "WHERE (dbo.tblGrading_ReturnDetails.ParNo = '" & txtParNo.Text & "') AND (dbo.tblGrading_ReturnDetails.Sec = 1) AND (dbo.tblGrading_ReturnDetails.Department = '" & cmbDept.Text & "') " & _
            '                "GROUP BY dbo.tblGrading_ReturnDetails.PktNo, dbo.tblGrading_RepairParcelsA.PktNo2 " & _
            '                "ORDER BY dbo.tblGrading_ReturnDetails.PktNo", AdoCN, 1, 1)


            rsComSql_1.Open("SELECT TOP (100) PERCENT dbo.VW_GradingReturnsPkt.Department, dbo.VW_GradingReturnsPkt.ParNo, dbo.VW_GradingReturnsPkt.PktNo, dbo.VW_GradingReturnsPkt.TotPcs ,ISNULL(dbo.VW_GradingSizingPacket.PktPcs, 0) AS PktPcs, " & _
                                "dbo.VW_GradingReturnsPkt.TotCts, ISNULL(dbo.VW_GradingSizingPacket.PktCts, 0) AS PktCts, dbo.VW_GradingReturnsPkt.PktNo2, dbo.VW_GradingSizingPacket.PlanValue " & _
                            "FROM dbo.VW_GradingReturnsPkt LEFT OUTER JOIN dbo.VW_GradingSizingPacket ON dbo.VW_GradingReturnsPkt.PktNo = dbo.VW_GradingSizingPacket.PktNo2 AND dbo.VW_GradingReturnsPkt.Department = dbo.VW_GradingSizingPacket.Department AND  " & _
                                "dbo.VW_GradingReturnsPkt.ParNo = dbo.VW_GradingSizingPacket.ParNo " & _
                            "WHERE (dbo.VW_GradingReturnsPkt.Department = '" & cmbDept.Text & "') AND (dbo.VW_GradingReturnsPkt.ParNo = '" & txtParNo.Text & "') " & _
                            "ORDER BY dbo.VW_GradingReturnsPkt.PktNo", AdoCN, 1, 1)

        ElseIf intSec = 2 Then
            rsComSql_1.Open("SELECT TOP (100) PERCENT SUM(dbo.tblGrading_ReturnDetails.Pcs) AS TotPcs, SUM(dbo.tblGrading_ReturnDetails.Cts) AS TotCts, " & _
                                "dbo.tblGradingTrf.OrderNo, dbo.tblGradingTrf.RefNo, dbo.tblGradingTrf.Side, dbo.tblGradingTrf.GiaNo, dbo.tblGradingTrf.RateCode, SUM(dbo.tblGradingTrf.PlanValue) AS PlanValue " & _
                            "FROM dbo.tblGrading_ReturnDetails INNER JOIN dbo.tblGradingTrf ON dbo.tblGrading_ReturnDetails.Department = dbo.tblGradingTrf.Department AND " & _
                                "dbo.tblGrading_ReturnDetails.ParNo = dbo.tblGradingTrf.ParcelNo And dbo.tblGrading_ReturnDetails.PktNo = dbo.tblGradingTrf.PktNo " & _
                            "WHERE (dbo.tblGrading_ReturnDetails.ParNo = '" & txtParNo.Text & "') AND (dbo.tblGrading_ReturnDetails.Sec = 1) AND (dbo.tblGrading_ReturnDetails.Department = '" & cmbDept.Text & "') " & _
                            "GROUP BY dbo.tblGradingTrf.OrderNo, dbo.tblGradingTrf.RefNo, dbo.tblGradingTrf.Side, dbo.tblGradingTrf.GiaNo, dbo.tblGradingTrf.RateCode " & _
                            "ORDER BY dbo.tblGradingTrf.OrderNo, dbo.tblGradingTrf.RefNo, dbo.tblGradingTrf.Side, dbo.tblGradingTrf.GiaNo", AdoCN, 1, 1)
        End If
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                intIssPcs = 0
                dblIssCts = 0
                dblPlanValue = 0

                If intSec = 1 Then
                    'rsComSql_2.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktCts), 3) AS PktCts, SUM(PlanValue) AS PlanValue FROM tblGrading_SizingPacket " & _
                    '                "WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND PktNo2 = '" & rsComSql_1.Fields("PktNo").Value & "'", AdoCN, 1, 1)
                ElseIf intSec = 2 Then
                    rsComSql_2 = New ADODB.Recordset
                    rsComSql_2.Open("SELECT SUM(PktPcs) AS PktPcs, ROUND(SUM(PktCts), 3) AS PktCts, SUM(PlanValue) AS PlanValue FROM tblGrading_SizingPacket " & _
                                    "WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' AND OrderNo = '" & rsComSql_1.Fields("OrderNo").Value & "' AND " & _
                                        "RefNo = '" & rsComSql_1.Fields("RefNo").Value & "' AND Side = '" & rsComSql_1.Fields("Side").Value & "' AND " & _
                                        "GiaNo = '" & rsComSql_1.Fields("GiaNo").Value & "' AND RateCode = '" & rsComSql_1.Fields("RateCode").Value & "'", AdoCN, 1, 1)
                    If Not IsDBNull(rsComSql_2.Fields("PktPcs").Value) Then
                        intIssPcs = rsComSql_2.Fields("PktPcs").Value
                        dblIssCts = Math.Round(rsComSql_2.Fields("PktCts").Value, 3)
                        dblPlanValue = Math.Round(rsComSql_2.Fields("PlanValue").Value, 2)
                    End If
                    rsComSql_2 = Nothing
                End If
                

                If intSec = 1 Then
                    flxPacket.Rows.Add(rsComSql_1.Fields("PktNo").Value,
                                       rsComSql_1.Fields("TotPcs").Value,
                                       Format(Math.Round(rsComSql_1.Fields("TotCts").Value, 3), "#0.000"),
                                       rsComSql_1.Fields("TotPcs").Value - rsComSql_1.Fields("PktPcs").Value,
                                       Format(Math.Round(rsComSql_1.Fields("TotCts").Value - rsComSql_1.Fields("PktCts").Value, 3), "#0.000"),
                                       "", "", "", rsComSql_1.Fields("PktNo2").Value, "", "0")
                ElseIf intSec = 2 Then
                    flxPacket.Rows.Add(rsComSql_1.Fields("OrderNo").Value,
                                       rsComSql_1.Fields("TotPcs").Value,
                                       Format(Math.Round(rsComSql_1.Fields("TotCts").Value, 3), "#0.000"),
                                       rsComSql_1.Fields("TotPcs").Value - intIssPcs,
                                       Format(Math.Round(rsComSql_1.Fields("TotCts").Value - dblIssCts, 3), "#0.000"),
                                       rsComSql_1.Fields("RefNo").Value,
                                       rsComSql_1.Fields("Side").Value,
                                       rsComSql_1.Fields("GiaNo").Value,
                                       rsComSql_1.Fields("OrderNo").Value,
                                       rsComSql_1.Fields("RateCode").Value,
                                       rsComSql_1.Fields("PlanValue").Value - dblPlanValue)
                End If

                txtTotPcs.Text = CInt(txtTotPcs.Text) + rsComSql_1.Fields("TotPcs").Value
                txtTotCts.Text = Format(Math.Round(CDbl(txtTotCts.Text) + rsComSql_1.Fields("TotCts").Value, 3), "#0.000")

                txtTotBalPcs.Text = CInt(txtTotBalPcs.Text) + (rsComSql_1.Fields("TotPcs").Value - intIssPcs)
                txtTotBalCts.Text = Format(Math.Round(CDbl(txtTotBalCts.Text) + rsComSql_1.Fields("TotCts").Value - dblIssCts, 3), "#0.000")
                rsComSql_1.MoveNext()
            End While
        End If
        rsComSql_1 = Nothing

        flxDetails.Rows.Clear()
        txtTPktPcs.Text = "0"
        txtTPktCts.Text = "0"
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblGrading_SizingPacket WHERE ParNo = '" & txtParNo.Text & "' AND Department = '" & cmbDept.Text & "' ORDER BY PktNo", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                flxDetails.Rows.Add(rsComSql_1.Fields("PktNo").Value,
                                    rsComSql_1.Fields("ReturnType").Value,
                                    rsComSql_1.Fields("PktPcs").Value,
                                    Format(Math.Round(rsComSql_1.Fields("PktCts").Value, 3), "#0.000"),
                                    rsComSql_1.Fields("RefNo").Value,
                                    rsComSql_1.Fields("Side").Value,
                                    rsComSql_1.Fields("GiaNo").Value,
                                    rsComSql_1.Fields("PktNo2").Value,
                                    rsComSql_1.Fields("RateCode").Value,
                                    rsComSql_1.Fields("PlanValue").Value)

                txtTPktPcs.Text = CInt(txtTPktPcs.Text) + rsComSql_1.Fields("PktPcs").Value
                txtTPktCts.Text = Format(Math.Round(CDbl(txtTPktCts.Text) + rsComSql_1.Fields("PktCts").Value, 3), "#0.000")
                rsComSql_1.MoveNext()
            End While
        End If
        rsComSql_1 = Nothing

        txtBalPcs.Text = CInt(txtTotPcs.Text) - CInt(txtTPktPcs.Text)
        txtBalCts.Text = Format(CDbl(txtTotCts.Text) - CDbl(txtTPktCts.Text), "#0.000")

    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub Save()
        If cmbDept.Text = "" Then
            MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtParNo.Text = "" Then
            MsgBox("Invalid Parcel No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtPktNo.Text = "" Then
            MsgBox("Invalid Packet No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If cmbType.Text = "" Then
            MsgBox("Invalid Clarity", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtPktPcs.Text = "" Then
            MsgBox("Invalid Packet Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If CInt(txtPktPcs.Text) <= 0 Then
            MsgBox("Invalid Packet Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If txtPktCts.Text = "" Then
            MsgBox("Invalid Packet Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If CDbl(txtPktCts.Text) <= 0 Then
            MsgBox("Invalid Packet Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If CInt(txtPktPcs.Text) > CInt(txtBalPcs.Text) Then
            MsgBox("Invalid Packet Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If CDbl(txtPktCts.Text) > CDbl(txtBalCts.Text) Then
            MsgBox("Invalid Packet Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblGrading_SizingPacket WHERE ParNo = '" & txtParNo.Text & "' AND PktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            AdoCN.Execute("INSERT INTO tblGrading_SizingPacket(Department, ParNo, PktNo, PktPcs, PktCts, ReturnType, OrderNo, RefNo, Side, PktDate, GiaNo, PktNo2, RateCode, PlanValue) " & _
                          "VALUES('" & cmbDept.Text & "','" & txtParNo.Text & "','" & txtPktNo.Text & "'," & CInt(txtPktPcs.Text) & "," & CDbl(txtPktCts.Text) & ",'" & cmbType.Text & "'," & _
                            "'" & cmbType.Text & "','" & txtRefNo.Text & "','" & txtSide.Text & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & txtGiaNo.Text & "','" & txtPktNo3.Text & "','" & txtRate.Text & "','" & CDbl(txtPlanValue.Text) & "')")
        Else
            MsgBox("Already Entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        rsComSql = Nothing

        txtPktPcs.Text = ""
        txtPktCts.Text = ""
        cmbType.Text = ""
        txtBalPcs.Text = ""
        txtBalCts.Text = ""
        txtTotBalPcs.Text = ""
        txtTotBalCts.Text = ""
        txtRefNo.Text = ""
        txtSide.Text = ""
        txtGiaNo.Text = ""
        txtRate.Text = ""
        txtPlanValue.Text = ""

        If cmbDept.Text = "GradingPCU_N" Or cmbDept.Text = "GradingPCU" Then
            GetNewPacket(2)
        Else
            GetNewPacket(1)
        End If

    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub flxPacket_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxPacket.CellClick
        If flxPacket.Rows.Count > 0 Then
            cmbType.Text = flxPacket.Item(8, flxPacket.CurrentRow.Index).Value
            txtPktPcs.Text = flxPacket.Item(3, flxPacket.CurrentRow.Index).Value
            txtPktCts.Text = flxPacket.Item(4, flxPacket.CurrentRow.Index).Value

            txtRefNo.Text = flxPacket.Item(5, flxPacket.CurrentRow.Index).Value
            txtSide.Text = flxPacket.Item(6, flxPacket.CurrentRow.Index).Value
            txtGiaNo.Text = flxPacket.Item(7, flxPacket.CurrentRow.Index).Value

            txtPktNo3.Text = flxPacket.Item(0, flxPacket.CurrentRow.Index).Value
            txtRate.Text = flxPacket.Item(9, flxPacket.CurrentRow.Index).Value
            txtPlanValue.Text = flxPacket.Item(10, flxPacket.CurrentRow.Index).Value
        End If
    End Sub

    Private Sub txtPktPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
        If Asc(e.KeyChar) = 13 Then
            txtPktCts.Focus()
        End If
    End Sub

    Private Sub txtPktCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtPktCts.Text)
    End Sub
End Class