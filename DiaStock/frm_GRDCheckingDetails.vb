
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDCheckingDetails
    Dim strType As String

    Private Sub frm_GRDCheckingDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Department(cmbDept)
    End Sub

    Private Sub Load_GradingTypes(ByVal cmbSample As System.Windows.Forms.ComboBox, ByVal intSec As Integer)
        Dim rsGrdType As New ADODB.Recordset

        cmbSample.Items.Clear()
        rsGrdType = New ADODB.Recordset
        rsGrdType.Open("SELECT * FROM tblGrading_RndTypes WHERE Sec = " & intSec & " ORDER BY Type", AdoCN, 1, 1)
        If rsGrdType.RecordCount Then
            rsGrdType.MoveFirst()
            While Not rsGrdType.EOF
                cmbSample.Items.Add(rsGrdType.Fields("Type").Value)
                rsGrdType.MoveNext()
            End While
        End If
        rsGrdType = Nothing

    End Sub

    Private Sub ClearFields()

        txtParNo.Text = ""
        txtPktNo.Text = ""
        txtTotPcs2.Text = ""
        txtTotCts2.Text = ""
        flxDetails.Rows.Clear()

        cmdParPkt.Focus()
    End Sub

    Private Sub cmdParPkt_Click(sender As Object, e As EventArgs) Handles cmdParPkt.Click
        Instring = UCase(InputBox("Enter Par/Pkt Number"))
        ParcelLen = Len(Instring)
        If ParcelLen >= 10 Then
            Select Case cmbDept.Text
                Case "Baguettes"
                    txtParNo.Text = Mid(Instring, 1, ParcelLen - 4)
                    txtPktNo.Text = strRight(Instring, 4)
                Case "Rounds"
                    txtParNo.Text = Mid(Instring, 1, ParcelLen - 3)
                    txtPktNo.Text = strRight(Instring, 3)
                Case Else
                    txtParNo.Text = Mid(Instring, 1, ParcelLen - 3)
                    txtPktNo.Text = strRight(Instring, 3)
            End Select

            txtPktNo.Focus()
            Load_Packet()
        Else
            txtParPkt.Text = ""
            cmdParPkt.Focus()
        End If
    End Sub

    Private Sub Load_Packet()
        If txtParNo.Text <> "" And txtPktNo.Text <> "" Then
            txtParNo.Text = UCase(txtParNo.Text)
            txtPktNo.Text = UCase(txtPktNo.Text)
            If ParcelFound(cmbDept.Text, txtParNo.Text) = True Then
                Load_ParcelDetails()
            Else
                MsgBox("Department and Parcel No. not matching", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                txtParNo.Text = ""
                txtPktNo.Text = ""
                txtParNo.Focus()
            End If
        End If
    End Sub

    Private Function ParcelFound(ByVal strDept As String, ByVal strParceNo As String) As Boolean

        rsComSql_1 = New ADODB.Recordset
        Select Case strDept
            Case "Princess"
                If Len(txtParNo.Text) <> 7 Then
                    ParcelFound = False
                    Exit Function
                End If
                rsComSql_1.Open("SELECT * FROM tblPRPacket WHERE ParNo = '" & strParceNo & "'", AdoCN, 1, 1)
            Case "Baguettes"
                If Len(txtParNo.Text) <> 7 Then
                    ParcelFound = False
                    Exit Function
                End If
                rsComSql_1.Open("SELECT * FROM tblBAGPacket WHERE ParNo = '" & strParceNo & "'", AdoCN, 1, 1)
            Case "Rounds"
                If Len(txtParNo.Text) <> 8 Then
                    ParcelFound = False
                    Exit Function
                End If
                rsComSql_1.Open("SELECT * FROM tblRndPacket WHERE ParNo = '" & strParceNo & "'", AdoCN, 1, 1)
            Case "Niru"
                If Len(txtParNo.Text) <> 8 Then
                    ParcelFound = False
                    Exit Function
                End If
                rsComSql_1.Open("SELECT * FROM tblNiruPacket WHERE ParNo = '" & strParceNo & "'", AdoCN, 1, 1)
            Case "Rounds3", "Rounds4", "Rounds6", "Rounds7", "RoundsNLE", "Emerald", "Emerald2", "Emerald3", "Opening", "Lamour", "Davinci", "Princess2", "Baguettes2", "Baguettes3", "Carrer"
                If Len(txtParNo.Text) < 7 Then
                    ParcelFound = False
                    Exit Function
                End If
                rsComSql_1.Open("SELECT * FROM tblExtPacket WHERE ParNo = '" & strParceNo & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
            Case "Direct Import", "Rounds Direct", "Mix", "GradingMix", "GradingPCU", "GradingPCU_N", "Grading Checking"
                rsComSql_1.Open("SELECT * FROM tblGradingTrf WHERE ParcelNo = '" & strParceNo & "' AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
            Case "Grading Export"
                rsComSql_1.Open("SELECT * FROM tblRghIssues WHERE ParNo = '" & strParceNo & "' AND SecName = 'Grading Export'", AdoCN, 1, 1)
            Case Else
                ParcelFound = False
                Exit Function
        End Select

        If rsComSql_1.RecordCount Then
            ParcelFound = True
            If cmbDept.Text = "Direct Import" Or cmbDept.Text = "Grading Checking" Or cmbDept.Text = "Grading Export" Or cmbDept.Text = "GradingPCU_N" Then
                strType = "Baguettes"
            Else
                strType = cmbDept.Text
            End If
        Else
            ParcelFound = False
        End If
        rsComSql_1 = Nothing

    End Function

    Private Sub Load_ParcelDetails()

        flxDetails.Rows.Clear()
        rsComSql_1 = New ADODB.Recordset
        rsComSql_1.Open("SELECT * FROM tblGrading_CheckingDetails WHERE ParNo = '" & txtParNo.Text & "' AND OrgPktNo = '" & txtPktNo.Text & "' AND Department = '" & cmbDept.Text & "' ORDER BY ID", AdoCN, 1, 1)
        If rsComSql_1.RecordCount Then
            rsComSql_1.MoveFirst()
            While Not rsComSql_1.EOF
                flxDetails.Rows.Add(rsComSql_1.Fields("Color").Value,
                                    rsComSql_1.Fields("Clarity").Value,
                                    rsComSql_1.Fields("Make").Value,
                                    rsComSql_1.Fields("Diameter").Value,
                                    rsComSql_1.Fields("Pcs").Value,
                                    rsComSql_1.Fields("Cts").Value,
                                    rsComSql_1.Fields("ID").Value,
                                    rsComSql_1.Fields("PktNo").Value,
                                    rsComSql_1.Fields("Cut").Value,
                                    rsComSql_1.Fields("Symmetry").Value,
                                    rsComSql_1.Fields("Polish").Value,
                                    rsComSql_1.Fields("Assortment").Value,
                                    rsComSql_1.Fields("Price").Value)

                rsComSql_1.MoveNext()
            End While
        End If
        rsComSql_1 = Nothing

        txtTotPcs2.Text = CalTotalPcs(flxDetails)
        txtTotCts2.Text = CalTotalCts(flxDetails)
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(4, intRow).Value)
        Next
    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(5, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub Save()
        Dim intRow As Integer
        Dim strCode As String
        Dim strAssortment As String
        Dim dblPrice As Double

        For intRow = 0 To flxDetails.Rows.Count - 1
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_RndTypes WHERE Type = '" & flxDetails.Item(0, intRow).Value & "' AND Sec = 1", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Color", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_RndTypes WHERE Type = '" & flxDetails.Item(1, intRow).Value & "' AND Sec = 4", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Clarity", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_RndTypes WHERE Type = '" & flxDetails.Item(2, intRow).Value & "' AND Sec = 2", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Make", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_RndTypes WHERE Type = '" & flxDetails.Item(8, intRow).Value & "' AND Sec = 5", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Cut", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_RndTypes WHERE Type = '" & flxDetails.Item(9, intRow).Value & "' AND Sec = 6", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Symmetry", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_RndTypes WHERE Type = '" & flxDetails.Item(10, intRow).Value & "' AND Sec = 7", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Polish", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            If Not IsNumeric(flxDetails.Item(3, intRow).Value) Then
                MsgBox("Invalid Diameter", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If CDbl(flxDetails.Item(3, intRow).Value) <= 0 Then
                MsgBox("Invalid Diameter", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If

            If Not IsNumeric(flxDetails.Item(5, intRow).Value) Then
                MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            If CDbl(flxDetails.Item(5, intRow).Value) <= 0 Then
                MsgBox("Invalid Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
        Next

        For intRow = 0 To flxDetails.Rows.Count - 1
            strCode = ""
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM tblGrading_RndSizingCodes WHERE Color = '" & flxDetails.Item(0, intRow).Value & "' AND Clarity = '" & flxDetails.Item(1, intRow).Value & "' AND Make = '" & flxDetails.Item(2, intRow).Value & "'", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                strCode = rsComSql.Fields("Code").Value
            End If
            rsComSql = Nothing

            strAssortment = ""
            dblPrice = 0
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT TOP (100) PERCENT DiaSales.dbo.tblDCLPermanents.ItemName, DiaSales.dbo.tblDCLPermanents.ListCost " & _
                          "FROM DiaSales.dbo.tblDCLPermanents INNER JOIN dbo.tblGrading_RndSizeList ON DiaSales.dbo.tblDCLPermanents.ItemName = dbo.tblGrading_RndSizeList.AssortNo " & _
                          "WHERE (DiaSales.dbo.tblDCLPermanents.LengthFrom <= '" & CDbl(flxDetails.Item(3, intRow).Value) & "') AND (DiaSales.dbo.tblDCLPermanents.LengthTo >= '" & CDbl(flxDetails.Item(3, intRow).Value) & "') AND (DiaSales.dbo.tblDCLPermanents.MainAssort = '" & strCode & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                strAssortment = rsComSql.Fields("ItemName").Value
                dblPrice = rsComSql.Fields("ListCost").Value
            End If
            rsComSql = Nothing

            AdoCN.Execute("UPDATE tblGrading_CheckingDetails SET Color = '" & UCase(flxDetails.Item(0, intRow).Value) & "',Clarity = '" & UCase(flxDetails.Item(1, intRow).Value) & "'," & _
                            "Make = '" & UCase(flxDetails.Item(2, intRow).Value) & "',Diameter = '" & CDbl(flxDetails.Item(3, intRow).Value) & "',Cts = '" & CDbl(flxDetails.Item(5, intRow).Value) & "'," & _
                            "Cut = '" & UCase(flxDetails.Item(8, intRow).Value) & "',Symmetry = '" & UCase(flxDetails.Item(9, intRow).Value) & "',Polish = '" & UCase(flxDetails.Item(10, intRow).Value) & "'," & _
                            "Assortment = '" & strAssortment & "',Price = '" & dblPrice & "' " & _
                          "WHERE ID = '" & flxDetails.Item(6, intRow).Value & "'")
        Next
        ClearFields()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub txtPktNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktNo.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Load_Packet()
        End If
    End Sub

    Private Sub txtPktNo_TextChanged(sender As Object, e As EventArgs)

    End Sub
End Class