
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLMonthEnd

    Private Sub frm_DCLMonthEnd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        If dbConnDiaStock.State = 1 Then
            dbConnDiaStock.Close()
        End If
        dbConnDiaStock.ConnectionString = "Provider=SQLOLEDB;Data Source='" & strServerName & "';Connect Timeout=60;Initial Catalog=DiaStock;Integrated Security=SSPI"
        dbConnDiaStock.Open()

        If dbConnDiaSales.State = 1 Then
            dbConnDiaSales.Close()
        End If
        dbConnDiaSales.ConnectionString = "Provider=SQLOLEDB;Data Source='" & strServerName & "';Connect Timeout=60;Initial Catalog=DiaSales;Integrated Security=SSPI"
        dbConnDiaSales.Open()

        Load_Month()
        Load_RoughClose()
        Load_PolishClose()
        Load_PolishBoxClose()
        Load_ApcuBoxClose()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_Month()
        Dim strMaxYear As String

        strMaxYear = ""
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(PrevYear1) AS Year1 FROM tblDCLAAMonthEnd", dbConnDiaStock, 1, 1)
        If rsComSql.RecordCount Then
            strMaxYear = rsComSql.Fields("Year1").Value
        End If
        rsComSql = Nothing
        txtYear.Text = strMaxYear

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT MAX(PrevMonth1) AS Month1 FROM tblDCLAAMonthEnd WHERE PrevYear1 = '" & strMaxYear & "'", dbConnDiaStock, 1, 1)
        If rsComSql.RecordCount Then
            txtMonth.Text = rsComSql.Fields("Month1").Value
        End If
        rsComSql = Nothing

        txtNextYear.Text = txtYear.Text
        txtNextMonth.Text = CDbl(txtMonth.Text) + 1
        If Val(txtNextMonth.Text) > 12 Then
            txtNextYear.Text = CDbl(txtYear.Text) + 1
            txtNextMonth.Text = "01"
        End If
        txtNextMonth.Text = CInt(txtNextMonth.Text).ToString("00")

        txtNewYear.Text = txtNextYear.Text
        txtNewMonth.Text = CDbl(txtNextMonth.Text) + 1
        If Val(txtNewMonth.Text) > 12 Then
            txtNewYear.Text = CDbl(txtNewYear.Text) + 1
            txtNewMonth.Text = "01"
        End If
        txtNewMonth.Text = CInt(txtNewMonth.Text).ToString("00")
    End Sub

    Private Sub Load_RoughClose()
        txtRghDcl.Text = "0"
        txtRghNle.Text = "0"

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT (OpenValue + ImpValue + ImpAsrwValue + Labour + LabourE) - (ApcuValue + PolBoxValue + RoughValue + AsrwValue) AS CloseValue " & _
                      "FROM dbo.VW_DCLAARoughStock " & _
                      "WHERE (PrevYear1 = '" & txtYear.Text & "') AND (PrevMonth1 = '" & txtMonth.Text & "') AND (ParcelType = 'Rough') AND (CompCode = 'DCL')", dbConnDiaStock, 1, 1)
        If rsComSql.RecordCount Then
            txtRghDcl.Text = Math.Round(rsComSql.Fields("CloseValue").Value, 2)
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT (OpenValue + ImpValue + ImpAsrwValue + Labour + LabourE) - (ApcuValue + PolBoxValue + RoughValue + AsrwValue) AS CloseValue " & _
                      "FROM dbo.VW_DCLAARoughStock " & _
                      "WHERE (PrevYear1 = '" & txtYear.Text & "') AND (PrevMonth1 = '" & txtMonth.Text & "') AND (ParcelType = 'Rough') AND (CompCode = 'NLE')", dbConnDiaStock, 1, 1)
        If rsComSql.RecordCount Then
            txtRghNle.Text = Math.Round(rsComSql.Fields("CloseValue").Value, 2)
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_PolishClose()
        txtPolDcl.Text = "0"
        txtPolNle.Text = "0"

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT (OpenValue + ImpValue + ImpArejValue + ImpIntValue + Labour) - (PolBoxValue + PolBoxValue2 + ApcuValue + RoughValue + ArejValue + RoughValueInt) AS CloseValue " & _
                      "FROM dbo.VW_DCLAAPolishStock " & _
                      "WHERE (PrevYear1 = '" & txtYear.Text & "') AND (PrevMonth1 = '" & txtMonth.Text & "') AND (CompCode = 'DCL') AND (ParcelType = 'Polished')", dbConnDiaStock, 1, 1)
        If rsComSql.RecordCount Then
            txtPolDcl.Text = Math.Round(rsComSql.Fields("CloseValue").Value, 2)
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT (OpenValue + ImpValue + ImpArejValue + ImpIntValue + Labour) - (PolBoxValue + PolBoxValue2 + ApcuValue + RoughValue + ArejValue + RoughValueInt) AS CloseValue " & _
                      "FROM dbo.VW_DCLAAPolishStock " & _
                      "WHERE (PrevYear1 = '" & txtYear.Text & "') AND (PrevMonth1 = '" & txtMonth.Text & "') AND (CompCode = 'NLE') AND (ParcelType = 'Polished')", dbConnDiaStock, 1, 1)
        If rsComSql.RecordCount Then
            txtPolNle.Text = Math.Round(rsComSql.Fields("CloseValue").Value, 2)
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_PolishBoxClose()
        txtBoxDcl.Text = "0"
        txtBoxNle.Text = "0"

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT (StockValue + InValue + IntValue) - (SalesValue + TrfValue) AS CloseValue " & _
                      "FROM dbo.VW_DCLABPolBoxStock " & _
                      "WHERE (Year1 = '" & txtYear.Text & "') AND (Month1 = '" & txtMonth.Text & "') AND (CompCode = 'DCL')", dbConnDiaSales, 1, 1)
        If rsComSql.RecordCount Then
            txtBoxDcl.Text = Math.Round(rsComSql.Fields("CloseValue").Value, 2)
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT (StockValue + InValue + IntValue) - (SalesValue + TrfValue) AS CloseValue " & _
                      "FROM dbo.VW_DCLABPolBoxStock " & _
                      "WHERE (Year1 = '" & txtYear.Text & "') AND (Month1 = '" & txtMonth.Text & "') AND (CompCode = 'NLE')", dbConnDiaSales, 1, 1)
        If rsComSql.RecordCount Then
            txtBoxNle.Text = Math.Round(rsComSql.Fields("CloseValue").Value, 2)
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_ApcuBoxClose()
        txtApcuBox.Text = "0"

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT StockValue + InValue - OutValue AS CloseValue " & _
                      "FROM dbo.VW_MixCloseStock " & _
                      "WHERE (Year1 = '" & txtYear.Text & "') AND (Month1 = '" & txtMonth.Text & "')", dbConnDiaSales, 1, 1)
        If rsComSql.RecordCount Then
            txtApcuBox.Text = Math.Round(rsComSql.Fields("CloseValue").Value, 2)
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Save()
        'Rough Stock
        dbConnDiaStock.Execute("INSERT INTO tblDCLAAMonthEnd(Year1,Month1,ParcelType,OpenValue,PrevYear1,PrevMonth1,CompCode) " & _
                               "VALUES('" & txtNewYear.Text & "','" & txtNewMonth.Text & "','Rough'," & CDbl(txtRghDcl.Text) & ",'" & txtNextYear.Text & "','" & txtNextMonth.Text & "','DCL')")

        dbConnDiaStock.Execute("INSERT INTO tblDCLAAMonthEnd(Year1,Month1,ParcelType,OpenValue,PrevYear1,PrevMonth1,CompCode) " & _
                               "VALUES('" & txtNewYear.Text & "','" & txtNewMonth.Text & "','Rough'," & CDbl(txtRghNle.Text) & ",'" & txtNextYear.Text & "','" & txtNextMonth.Text & "','NLE')")

        'Polish Stock
        dbConnDiaStock.Execute("INSERT INTO tblDCLAAMonthEnd(Year1,Month1,ParcelType,OpenValue,PrevYear1,PrevMonth1,CompCode) " & _
                               "VALUES('" & txtNewYear.Text & "','" & txtNewMonth.Text & "','Polished'," & CDbl(txtPolDcl.Text) & ",'" & txtNextYear.Text & "','" & txtNextMonth.Text & "','DCL')")

        dbConnDiaStock.Execute("INSERT INTO tblDCLAAMonthEnd(Year1,Month1,ParcelType,OpenValue,PrevYear1,PrevMonth1,CompCode) " & _
                               "VALUES('" & txtNewYear.Text & "','" & txtNewMonth.Text & "','Polished'," & CDbl(txtPolNle.Text) & ",'" & txtNextYear.Text & "','" & txtNextMonth.Text & "','NLE')")

        'Polish Box
        dbConnDiaSales.Execute("INSERT INTO tblPOLCloseStock(Year1,Month1,StockPcs,StockCts,StockValue,CompCode) " & _
                               "VALUES('" & txtNextYear.Text & "','" & txtNextMonth.Text & "',0,0," & CDbl(txtBoxDcl.Text) & ",'DCL')")

        dbConnDiaSales.Execute("INSERT INTO tblPOLCloseStock(Year1,Month1,StockPcs,StockCts,StockValue,CompCode) " & _
                               "VALUES('" & txtNextYear.Text & "','" & txtNextMonth.Text & "',0,0," & CDbl(txtBoxNle.Text) & ",'NLE')")

        'APCU Box
        dbConnDiaSales.Execute("INSERT INTO tblMixCloseStock(Year1,Month1,StockPcs,StockCts,StockValue) " & _
                               "VALUES('" & txtNextYear.Text & "','" & txtNextMonth.Text & "',0,0," & CDbl(txtApcuBox.Text) & ")")

        MsgBox("Month End Process Done", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub ClearFields()
        txtYear.Text = ""
        txtMonth.Text = ""
        txtNextYear.Text = ""
        txtNextMonth.Text = ""
        txtNewYear.Text = ""
        txtNewMonth.Text = ""

        txtRghDcl.Text = "0"
        txtRghNle.Text = "0"
        txtPolDcl.Text = "0"
        txtPolNle.Text = "0"
        txtBoxDcl.Text = "0"
        txtBoxNle.Text = "0"
        txtApcuBox.Text = "0"
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
        Load_Month()
        Load_RoughClose()
        Load_PolishClose()
        Load_PolishBoxClose()
        Load_ApcuBoxClose()
    End Sub
End Class