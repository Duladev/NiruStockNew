
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLLotApproval

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub frm_DCLLotApproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Lock_Buttons()
        Load_Data()
    End Sub

    Private Sub Lock_Buttons()
        cmdSaveFan.Enabled = False
        cmdSaveRgh.Enabled = False
        cmdSaveRnd.Enabled = False
        If PBUser_EmpNo = "D02429" Then
            cmdSaveRgh.Enabled = True
        End If
        If PBUser_EmpNo = "D02437" Or PBUser_EmpNo = "D08411" Then
            cmdSaveFan.Enabled = True
        End If
        If PBUser_EmpNo = "D08353" Then
            cmdSaveRnd.Enabled = True
        End If
        If PBUser_EmpNo = "D06975" Then
            cmdSaveFan.Enabled = True
            cmdSaveRgh.Enabled = True
            cmdSaveRnd.Enabled = True
        End If
    End Sub

    Private Sub Load_Data()
        txtLotNo.Text = ""

        flxRough.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblDCLLotApproval WHERE Type = 0 AND Approval = 0 ORDER BY LotNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxRough.Rows.Add(rsComSql.Fields("LotNo").Value,
                                  rsComSql.Fields("ReCheck").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        flxFancy.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblDCLLotApproval WHERE Type = 1 AND Approval = 0 ORDER BY LotNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxFancy.Rows.Add(rsComSql.Fields("LotNo").Value,
                                  rsComSql.Fields("ReCheck").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        flxRounds.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblDCLLotApproval WHERE Type = 2 AND Approval = 0 ORDER BY LotNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxRounds.Rows.Add(rsComSql.Fields("LotNo").Value,
                                   rsComSql.Fields("ReCheck").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub txtLotNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLotNo.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub cmdSaveRgh_Click(sender As Object, e As EventArgs) Handles cmdSaveRgh.Click
        Save_Rough()
    End Sub

    Private Sub Save_Rough()
        If txtLotNo.Text = "" Then MsgBox("Invalid Lot No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT LotNo FROM tblImport WHERE LotNo = " & CDbl(txtLotNo.Text) & "", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Lot No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT LotNo FROM tblDCLLotApproval WHERE Type = 0 AND LotNo = " & CDbl(txtLotNo.Text) & "", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            AdoCN.Execute("INSERT INTO tblDCLLotApproval(LotNo,Type,Approval) VALUES(" & CDbl(txtLotNo.Text) & ",0,0)")
        Else
            MsgBox("Already Requested for Rough", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        Load_Data()
    End Sub

    Private Sub Save_Fancy()
        If txtLotNo.Text = "" Then MsgBox("Invalid Lot No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT LotNo FROM tblImport WHERE LotNo = " & CDbl(txtLotNo.Text) & "", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Lot No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT LotNo FROM tblDCLLotApproval WHERE Type = 1 AND LotNo = " & CDbl(txtLotNo.Text) & "", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            AdoCN.Execute("INSERT INTO tblDCLLotApproval(LotNo,Type,Approval) VALUES(" & CDbl(txtLotNo.Text) & ",1,0)")
        Else
            MsgBox("Already Requested for Fancy", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        Load_Data()
    End Sub

    Private Sub Save_Rounds()
        If txtLotNo.Text = "" Then MsgBox("Invalid Lot No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT LotNo FROM tblImport WHERE LotNo = " & CDbl(txtLotNo.Text) & "", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            MsgBox("Invalid Lot No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT LotNo FROM tblDCLLotApproval WHERE Type = 2 AND LotNo = " & CDbl(txtLotNo.Text) & "", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            AdoCN.Execute("INSERT INTO tblDCLLotApproval(LotNo,Type,Approval) VALUES(" & CDbl(txtLotNo.Text) & ",2,0)")
        Else
            MsgBox("Already Requested for Rounds", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If
        rsComSql = Nothing

        Load_Data()
    End Sub

    Private Sub cmdSaveFan_Click(sender As Object, e As EventArgs) Handles cmdSaveFan.Click
        Save_Fancy()
    End Sub

    Private Sub cmdSaveRnd_Click(sender As Object, e As EventArgs) Handles cmdSaveRnd.Click
        Save_Rounds()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        Load_Data()
    End Sub
End Class