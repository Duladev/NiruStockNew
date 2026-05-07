
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLPacketAccept
    Dim dbName As String
    Dim ParcelNo As String
    Dim PacketNo As String

    Private Sub frm_DCLPacketAccept_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_DepartmentProd(cmbDept)
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_Packets()
        flxDetails.Rows.Clear()
        txtParNo.Text = UCase(txtParNo.Text)
        rsComSql = New ADODB.Recordset
        If cmbDept.Text = "Baguettes" Then
            dbName = "tblBAGPacket"
            rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblBAGPacket WHERE ParNo = '" & txtParNo.Text & "' AND DelDate IS NOT NULL AND AccDate IS NULL ORDER BY PktNo", AdoCN, 1, 1)
        ElseIf cmbDept.Text = "Princess" Then
            dbName = "tblPRPacket"
            rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblPRPacket WHERE ParNo = '" & txtParNo.Text & "'  AND DelDate IS NOT NULL AND AccDate IS NULL ORDER BY PktNo", AdoCN, 1, 1)
        ElseIf cmbDept.Text = "Rounds" Then
            dbName = "tblRndPacket"
            rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblRndPacket WHERE ParNo = '" & txtParNo.Text & "'  AND DelDate IS NOT NULL AND AccDate IS NULL ORDER BY CONVERT(NUMERIC, PktNo)", AdoCN, 1, 1)
        ElseIf cmbDept.Text = "Niru" Then

        ElseIf cmbDept.Text = "Rounds3" Or cmbDept.Text = "Rounds4" Or cmbDept.Text = "RoundsNLE" Or cmbDept.Text = "Emerald" Or cmbDept.Text = "Opening" Or cmbDept.Text = "Lamour" Or cmbDept.Text = "Davinci" Or cmbDept.Text = "Princess2" Or cmbDept.Text = "Emerald2" Or cmbDept.Text = "Emerald3" Or cmbDept.Text = "Baguettes2" Or cmbDept.Text = "Baguettes3" Or cmbDept.Text = "Carrer" Or cmbDept.Text = "Asscher" Or cmbDept.Text = "Radiant" Then
            dbName = "tblExtPacket"
            rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblExtPacket WHERE ParNo = '" & txtParNo.Text & "'  AND DelDate IS NOT NULL AND AccDate IS NULL AND Department = '" & cmbDept.Text & "' ORDER BY PktNo", AdoCN, 1, 1)
        ElseIf cmbDept.Text = "RoughBruting" Then
            dbName = "tblRprPacket"
            rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblRprPacket WHERE ParNo = '" & txtParNo.Text & "'  AND DelDate IS NOT NULL AND AccDate IS NULL AND Department = '" & cmbDept.Text & "' ORDER BY PktNo", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("ParNo").Value,
                                    rsComSql.Fields("PktNo").Value,
                                    rsComSql.Fields("PktPcs").Value,
                                    rsComSql.Fields("PktCts").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        flxSelected.Rows.Clear()
        txtCount.Text = ""
        txtPcs.Text = ""
    End Sub

    Private Sub Load_AllPackets()
        Dim dblPcs As Double

        flxSelected.Rows.Clear()
        txtPcs.Text = ""
        dblPcs = 0
        txtParNo.Text = UCase(txtParNo.Text)
        rsComSql = New ADODB.Recordset
        If cmbDept.Text = "Baguettes" Then
            dbName = "tblBAGPacket"
            rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblBAGPacket WHERE ParNo = '" & txtParNo.Text & "' AND DelDate IS NOT NULL AND AccDate IS NULL ORDER BY PktNo", AdoCN, 1, 1)

        ElseIf cmbDept.Text = "Princess" Then
            dbName = "tblPRPacket"
            rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblPRPacket WHERE ParNo = '" & txtParNo.Text & "' AND DelDate IS NOT NULL AND AccDate IS NULL ORDER BY PktNo", AdoCN, 1, 1)

        ElseIf cmbDept.Text = "Rounds" Then
            dbName = "tblRndPacket"
            rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblRndPacket WHERE ParNo = '" & txtParNo.Text & "' AND DelDate IS NOT NULL AND AccDate IS NULL ORDER BY PktNo", AdoCN, 1, 1)

        ElseIf cmbDept.Text = "Niru" Then

        ElseIf cmbDept.Text = "Rounds3" Or cmbDept.Text = "Rounds4" Or cmbDept.Text = "RoundsNLE" Or cmbDept.Text = "Emerald" Or cmbDept.Text = "Opening" Or cmbDept.Text = "Lamour" Or cmbDept.Text = "Davinci" Or cmbDept.Text = "Princess2" Or cmbDept.Text = "Emerald2" Or cmbDept.Text = "Emerald3" Or cmbDept.Text = "Baguettes2" Or cmbDept.Text = "Baguettes3" Or cmbDept.Text = "Carrer" Or cmbDept.Text = "Asscher" Or cmbDept.Text = "Radiant" Then
            dbName = "tblExtPacket"
            rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblExtPacket WHERE ParNo = '" & txtParNo.Text & "' AND DelDate IS NOT NULL AND AccDate IS NULL AND Department = '" & cmbDept.Text & "' ORDER BY PktNo", AdoCN, 1, 1)

        ElseIf cmbDept.Text = "RoughBruting" Then
            dbName = "tblRprPacket"
            rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblRPrPacket WHERE ParNo = '" & txtParNo.Text & "' AND DelDate IS NOT NULL AND AccDate IS NULL AND Department = '" & cmbDept.Text & "' ORDER BY PktNo", AdoCN, 1, 1)
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxSelected.Rows.Add(rsComSql.Fields("ParNo").Value,
                                     rsComSql.Fields("PktNo").Value,
                                     rsComSql.Fields("PktPcs").Value,
                                     rsComSql.Fields("PktCts").Value)
                dblPcs = dblPcs + rsComSql.Fields("PktPcs").Value
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        flxDetails.Rows.Clear()
        txtCount.Text = flxSelected.RowCount
        txtPcs.Text = dblPcs

    End Sub

    Private Sub cmdSelect_Click(sender As Object, e As EventArgs) Handles cmdSelect.Click
        Load_AllPackets()
    End Sub

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If Asc(e.KeyChar) = 13 And txtParNo.Text <> "" Then
            Load_Packets()
        End If
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        Dim intRow As Integer

        If cmbDept.Text <> "" And txtParNo.Text <> "" Then

            For intRow = 0 To flxSelected.Rows.Count - 1
                If flxDetails.Item(0, flxDetails.CurrentRow.Index).Value = flxSelected.Item(0, intRow).Value And _
                    flxDetails.Item(1, flxDetails.CurrentRow.Index).Value = flxSelected.Item(1, intRow).Value Then
                    MsgBox("Already Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Next

            flxSelected.Rows.Add(flxDetails.Item(0, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(1, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(2, flxDetails.CurrentRow.Index).Value,
                                 flxDetails.Item(3, flxDetails.CurrentRow.Index).Value)

            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)

            txtCount.Text = flxSelected.RowCount
        End If
    End Sub

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Dim intRow As Integer

        Datavalid = False
        Parcel = True
        Instring = UCase(InputBox("Enter " & "Par/Pkt Number"))
        ParcelLen = Len(Instring)
        If ParcelLen > 10 Then
            Datavalid = True

            ParcelNo = Mid(Instring, 1, ParcelLen - 4)
            PacketNo = strRight(Instring, 4)
        End If

        If Datavalid = True Then
            For intRow = 0 To flxSelected.Rows.Count - 1
                If ParcelNo = flxSelected.Item(0, intRow).Value And PacketNo = flxSelected.Item(1, intRow).Value Then
                    MsgBox("Already Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Next

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblRprPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND DelDate IS NOT NULL AND AccDate IS NULL AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                flxSelected.Rows.Add(rsComSql.Fields("ParNo").Value,
                                     rsComSql.Fields("PktNo").Value,
                                     rsComSql.Fields("PktPcs").Value,
                                     rsComSql.Fields("PktCts").Value)
            End If
            rsComSql = Nothing

            txtCount.Text = flxSelected.RowCount
        Else
            MsgBox("Invalid Parcel No./Packet No.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub Save()
        Dim y As Integer

        For y = 0 To flxSelected.Rows.Count - 1
            If cmbDept.Text = "RoughBruting" Then
                mStrSQL = "UPDATE tblRprPacket SET AccDate = '" & Format(Date.Now, "MM/dd/yyyy") & "',AccTime = '" & Format(Date.Now, "HH:mm:ss") & "' " & _
                          "WHERE ParNo = '" & flxSelected.Item(0, y).Value & "' AND PktNo = '" & flxSelected.Item(1, y).Value & "' AND Department = '" & cmbDept.Text & "'"
                AdoCN.Execute(mStrSQL)

            ElseIf cmbDept.Text = "Rounds3" Or cmbDept.Text = "Rounds4" Or cmbDept.Text = "RoundsNLE" Or cmbDept.Text = "Emerald" Or cmbDept.Text = "Opening" Or cmbDept.Text = "Lamour" Or cmbDept.Text = "Davinci" Or cmbDept.Text = "Princess2" Or cmbDept.Text = "Emerald2" Or cmbDept.Text = "Emerald3" Or cmbDept.Text = "Baguettes2" Or cmbDept.Text = "Baguettes3" Or cmbDept.Text = "Carrer" Or cmbDept.Text = "Asscher" Or cmbDept.Text = "Radiant" Then
                mStrSQL = "UPDATE tblExtPacket SET AccDate = '" & Format(Date.Now, "MM/dd/yyyy") & "',AccTime = '" & Format(Date.Now, "HH:mm:ss") & "' " & _
                          "WHERE ParNo = '" & flxSelected.Item(0, y).Value & "' AND PktNo = '" & flxSelected.Item(1, y).Value & "' AND Department = '" & cmbDept.Text & "'"
                AdoCN.Execute(mStrSQL)

            Else
                mStrSQL = "UPDATE " & dbName & " SET AccDate = '" & Format(Date.Now, "MM/dd/yyyy") & "',AccTime = '" & Format(Date.Now, "HH:mm:ss") & "' " & _
                          "WHERE ParNo = '" & flxSelected.Item(0, y).Value & "' AND PktNo = '" & flxSelected.Item(1, y).Value & "'"
                AdoCN.Execute(mStrSQL)

            End If
        Next

        MsgBox("Packets Accepted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        ClearText()

    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearText()
    End Sub

    Private Sub ClearText()
        flxDetails.Rows.Clear()
        flxSelected.Rows.Clear()
        dbName = ""
        txtParNo.Text = ""
        txtCount.Text = ""
        txtPcs.Text = ""
    End Sub

    Private Sub flxSelected_DoubleClick(sender As Object, e As EventArgs) Handles flxSelected.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxSelected.Rows.RemoveAt(flxSelected.CurrentRow.Index)
            txtCount.Text = flxSelected.RowCount
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub
End Class