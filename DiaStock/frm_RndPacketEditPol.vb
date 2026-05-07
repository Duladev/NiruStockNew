
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RndPacketEditPol
    Dim ParcelNo As String
    Dim PacketNo As String

    Private Sub frm_RndPacketEditPol_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        cmbDept.Items.Clear()
        cmbDept.Items.Add("Rounds")

        ClearText()
    End Sub

    Private Sub ClearText()
        flxDetails.Rows.Clear()
        txtCount.Text = "0"
    End Sub

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Dim intRow As Integer
        Dim blnFound As Boolean
        Dim dblIssPcs As Double
        Dim dblPktCts As Double
        Dim strGroup As String
        Dim strDiameter As String

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        Datavalid = False
        Parcel = True
        Instring = UCase(InputBox("Enter Par/Pkt Number"))
        ParcelLen = Len(Instring)
        If ParcelLen >= 10 Then
            Datavalid = True

            Select Case cmbDept.Text
                Case "Rounds"
                    If ParcelLen = 11 Then
                        ParcelNo = Mid(Instring, 1, ParcelLen - 3)
                        PacketNo = strRight(Instring, 3)
                    Else
                        ParcelNo = Mid(Instring, 1, ParcelLen - 4)
                        PacketNo = strRight(Instring, 4)
                    End If
                Case "Baguettes"
                    ParcelNo = Mid(Instring, 1, ParcelLen - 4)
                    PacketNo = strRight(Instring, 4)
                Case "Davinci"
                    ParcelNo = Mid(Instring, 1, ParcelLen - 3)
                    PacketNo = strRight(Instring, 3)
                Case "Princess"
                    ParcelNo = Mid(Instring, 1, ParcelLen - 3)
                    PacketNo = strRight(Instring, 3)
            End Select

        End If

        If Datavalid = True Then
            For intRow = 0 To flxDetails.Rows.Count - 1
                If flxDetails.Item(0, intRow).Value = ParcelNo And flxDetails.Item(1, intRow).Value = PacketNo Then
                    MsgBox("Already Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    cmdParPkt.Focus()
                    Exit Sub
                End If
            Next

            strGroup = ""
            strDiameter = ""
            dblIssPcs = 0
            dblPktCts = 0
            blnFound = False
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT ParNo,Grp,PktPcs,PktCts,ActDiameter FROM tblRndPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND DelDate IS NOT NULL AND AccDate IS NOT NULL", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                strGroup = rsComSql.Fields("Grp").Value
                dblIssPcs = rsComSql.Fields("PktPcs").Value
                dblPktCts = rsComSql.Fields("PktCts").Value
                strDiameter = rsComSql.Fields("ActDiameter").Value
                blnFound = True
            Else
                blnFound = False
            End If
            rsComSql = Nothing
            If blnFound = False Then MsgBox("Invalid Packet", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : cmdParPkt.Focus() : Exit Sub

            If blnFound = True Then
                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT PktNo FROM tblRndReturns WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND Sec = 25", AdoCN, 1, 1)
                If rsComSql.RecordCount Then
                Else
                    blnFound = False
                End If
                rsComSql = Nothing
                If blnFound = False Then MsgBox("Invalid Polishing Finish", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : cmdParPkt.Focus() : Exit Sub
            End If

            If blnFound = True Then
                flxDetails.Rows.Add(ParcelNo,
                                    PacketNo,
                                    dblIssPcs,
                                    Math.Round(dblPktCts, 3),
                                    strGroup,
                                    strDiameter)

                txtCount.Text = flxDetails.Rows.Count

                cmdParPkt.Focus()
            End If
        End If
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearText()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub flxDetails_DoubleClick(sender As Object, e As EventArgs) Handles flxDetails.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxDetails.Rows.RemoveAt(flxDetails.CurrentRow.Index)
            txtCount.Text = flxDetails.RowCount
        End If
    End Sub

    Private Sub Save()
        Dim intRow As Integer

        If cmbDept.Text = "" Then MsgBox("Invalid Department", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        For intRow = 0 To flxDetails.Rows.Count - 1
            If Len(flxDetails.Item(5, intRow).Value) = 0 Then
                MsgBox("Invalid Diameter - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value)
            End If
            If Not IsNumeric(flxDetails.Item(5, intRow).Value) = True Then
                MsgBox("Invalid Diameter - " & flxDetails.Item(0, intRow).Value & "/" & flxDetails.Item(1, intRow).Value)
            End If
        Next

        For intRow = 0 To flxDetails.Rows.Count - 1
            AdoCN.Execute("UPDATE tblRndPacket SET ActDiameter = '" & CDbl(flxDetails.Item(5, intRow).Value) & "' WHERE ParNo = '" & flxDetails.Item(0, intRow).Value & "' AND PktNo = '" & flxDetails.Item(1, intRow).Value & "'")
        Next

        MsgBox("Saved Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        ClearText()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            Save()
        End If
    End Sub
End Class