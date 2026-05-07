
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RprPrice

    Private Sub frm_RprPrice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        If PBUser_EmpNo = "D06975" Or PBUser_EmpNo = "D02429" Then
            cmdSave.Enabled = True
        Else
            cmdSave.Enabled = False
        End If

        If PBUser_EmpNo = "D06975" Or PBUser_EmpNo = "D02429" Then
            cmdDelete.Enabled = True
        Else
            cmdDelete.Enabled = False
        End If

        Load_Size()
    End Sub

    Private Sub Load_Size()
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblRgfSize ORDER BY SizeDec", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("SizeDec").Value,
                                    rsComSql.Fields("Price2").Value,
                                    rsComSql.Fields("Price3").Value,
                                    rsComSql.Fields("Height").Value,
                                    rsComSql.Fields("Pcs").Value,
                                    rsComSql.Fields("LenRange").Value,
                                    rsComSql.Fields("WidRange").Value,
                                    rsComSql.Fields("NewName").Value,
                                    rsComSql.Fields("Dept").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub ClearFields()
        txtSize.Text = ""
        txtPrice.Text = ""
        txtPrice2.Text = ""
        txtHeight.Text = ""
        txtPcs.Text = ""
        txtLen.Text = ""
        txtWid.Text = ""
        txtPriority.Text = ""
        cmbDept.Text = ""
    End Sub

    Private Sub txtSize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSize.KeyPress
        If Asc(e.KeyChar) = 13 Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblRgfSize WHERE SizeDec = '" & txtSize.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                txtSize.Text = rsComSql.Fields("SizeDec").Value
                txtPrice.Text = rsComSql.Fields("Price2").Value
                txtPrice2.Text = rsComSql.Fields("Price3").Value
                txtHeight.Text = rsComSql.Fields("Height").Value
                txtPcs.Text = rsComSql.Fields("Pcs").Value
                txtLen.Text = rsComSql.Fields("LenRange").Value
                txtWid.Text = rsComSql.Fields("WidRange").Value
                txtPriority.Text = rsComSql.Fields("NewName").Value
                cmbDept.Text = rsComSql.Fields("Dept").Value
            Else
                MsgBox("Invalid Size", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtPrice.Text = ""
                txtPrice2.Text = ""
                txtHeight.Text = ""
                txtPcs.Text = ""
                txtLen.Text = ""
                txtWid.Text = ""
                txtPriority.Text = ""
                cmbDept.Text = ""
            End If
            rsComSql = Nothing
            txtPrice.Focus()
        End If
    End Sub

    Private Sub txtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrice.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtPrice.Text)
        If Asc(e.KeyChar) = 13 Then
            txtPrice2.Focus()
        End If
    End Sub

    Private Sub txtPrice2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrice2.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtPrice2.Text)
        If Asc(e.KeyChar) = 13 Then
            txtHeight.Focus()
        End If
    End Sub

    Private Sub Save()
        If txtSize.Text = "" Then MsgBox("Invalid Size", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtPrice.Text = "" Then MsgBox("Invalid Price", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtPrice2.Text = "" Then MsgBox("Invalid Price", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtHeight.Text = "" Then MsgBox("Invalid Height", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtPcs.Text = "" Then MsgBox("Invalid Pcs", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If txtPriority.Text = "" Then MsgBox("Invalid Priority", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbDept.Text = "" Then MsgBox("Invalid Dept", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM tblRgfSize WHERE SizeDec = '" & Trim(txtSize.Text) & "'", AdoCN, 1, 1)
        If rsComSql.RecordCount = 0 Then
            AdoCN.Execute("INSERT INTO tblRgfSize(SizeDec,Price,Price2,Price3,Price4,Dept,Height,Pcs,LenRange,WidRange,NewName) " & _
                          "VALUES('" & Trim(txtSize.Text) & "','" & CDbl(txtPrice.Text) & "','" & CDbl(txtPrice.Text) & "'," & _
                            "'" & CDbl(txtPrice2.Text) & "','" & CDbl(txtPrice2.Text) & "','" & cmbDept.Text & "','" & CDbl(txtHeight.Text) & "','" & CDbl(txtPcs.Text) & "','" & Trim(txtLen.Text) & "','" & Trim(txtWid.Text) & "','" & txtPriority.Text & "')")

            MsgBox("Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        Else
            PBResponse = MsgBox("Already entered. Do you want to Update?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                AdoCN.Execute("UPDATE tblRgfSize SET Price = '" & CDbl(txtPrice.Text) & "',Price2 = '" & CDbl(txtPrice.Text) & "',Dept = '" & cmbDept.Text & "'," & _
                                "Price3 = '" & CDbl(txtPrice2.Text) & "',Price4 = '" & CDbl(txtPrice2.Text) & "',Pcs = '" & CDbl(txtPcs.Text) & "',LenRange = '" & Trim(txtLen.Text) & "',WidRange = '" & Trim(txtWid.Text) & "',NewName = '" & txtPriority.Text & "' " & _
                              "WHERE SizeDec = '" & Trim(txtSize.Text) & "'")

                MsgBox("Updated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
            'MsgBox("Already entered", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If

        ClearFields()
        Load_Size()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            Save()
        End If
    End Sub

    Private Sub txtHeight_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHeight.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtHeight.Text)
        If Asc(e.KeyChar) = 13 Then
            txtPcs.Focus()
        End If
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub txtPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        txtSize.Text = flxDetails.Item(0, flxDetails.CurrentRow.Index).Value
        txtPrice.Text = flxDetails.Item(1, flxDetails.CurrentRow.Index).Value
        txtPrice2.Text = flxDetails.Item(2, flxDetails.CurrentRow.Index).Value
        txtHeight.Text = flxDetails.Item(3, flxDetails.CurrentRow.Index).Value
        txtPcs.Text = flxDetails.Item(4, flxDetails.CurrentRow.Index).Value
        txtLen.Text = flxDetails.Item(5, flxDetails.CurrentRow.Index).Value & ""
        txtWid.Text = flxDetails.Item(6, flxDetails.CurrentRow.Index).Value & ""
        txtPriority.Text = flxDetails.Item(7, flxDetails.CurrentRow.Index).Value & ""
        cmbDept.Text = flxDetails.Item(8, flxDetails.CurrentRow.Index).Value & ""
    End Sub

    Private Sub Delete()
        Dim rstPacket As ADODB.Recordset

        If txtSize.Text <> "" Then
            PBResponse = MsgBox("Are you sure to Delete this Size?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, Me.Text)
            If PBResponse = MsgBoxResult.Yes Then
                rstPacket = New ADODB.Recordset
                rstPacket.Open("SELECT * FROM tblRgfSize WHERE SizeDec = '" & txtSize.Text & "'", AdoCN, 1, 1)
                If rstPacket.RecordCount Then
                    AdoCN.Execute("DELETE FROM tblRgfSize WHERE SizeDec = '" & txtSize.Text & "'")

                    MsgBox("Size Deleted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

                    ClearFields()
                    Load_Size()
                Else
                    MsgBox("Invalid Size", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                End If
                rstPacket = Nothing
            End If
        Else
            MsgBox("Please select the Size", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Delete()
    End Sub

    Private Sub txtPriority_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPriority.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub
End Class