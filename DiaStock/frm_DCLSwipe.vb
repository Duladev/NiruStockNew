
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_DCLSwipe

    Dim ParcelNo As String
    Dim PacketNo As String

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Datavalid = False
        Me.Close()
    End Sub

    Private Sub frm_DCLSwipe_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Parcel Then
            Me.Text = "Parcel & Packet"
        Else
            Me.Text = "IC No"
        End If
    End Sub

    Private Sub cmdSwipe_Click(sender As Object, e As EventArgs) Handles cmdSwipe.Click
        cmdSwipe.Enabled = False
        Instring = UCase(InputBox("Enter " & "Par/Pkt Number or Emp No"))
        EntryCheckdata()
        lblLabel3.Text = Instring
    End Sub

    Private Sub EntryCheckdata()
        On Error Resume Next
        Dim Rs As ADODB.Recordset

        Dim ParcelLen As String

        If Parcel = False Then
            'Get IC No
            Rs = New ADODB.Recordset
            mStrSQL = ("SELECT * FROM VW_EMP_MASTER_SMALL3 WHERE FullEmpNo = '" & Mid(Instring, 1, 6) & "'")
            Rs.Open(mStrSQL, AdoCN, 1, 1)
            ICNo = ""
            If Not Rs.EOF Then
                Datavalid = True
                ICNo = UCase(Trim(Instring))
            Else
                MsgBox("Invalid IC No", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, Me.Text)
                Datavalid = False
                ICNo = ""
            End If
            Rs = Nothing
        Else
            'Get Parcel/Packet No
            ParcelLen = Len(Instring)
            ParcelNo = Mid(Instring, 1, ParcelLen - 3) 'First 3 chars
            PacketNo = strRight(Instring, 3) 'Next 3 chars

            Datavalid = True

        End If
        Me.Close()
    End Sub
End Class