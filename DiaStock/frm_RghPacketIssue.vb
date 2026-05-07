
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_RghPacketIssue
    Dim dbName As String
    Dim ParcelNo As String
    Dim PacketNo As String

    Private Sub frm_RghPacketIssue_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_Department(cmbDept)
        Load_Model()
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub Load_Model()
        Dim rsGrdType As New ADODB.Recordset

        cmbModel.Items.Clear()
        rsGrdType = New ADODB.Recordset
        rsGrdType.Open("SELECT * FROM tblRghTypes WHERE Sec = 5 ORDER BY Type", AdoCN, 1, 1)
        If rsGrdType.RecordCount Then
            rsGrdType.MoveFirst()
            While Not rsGrdType.EOF
                cmbModel.Items.Add(rsGrdType.Fields("Type").Value)
                rsGrdType.MoveNext()
            End While
        End If
        rsGrdType = Nothing
    End Sub

    Private Sub Load_Packets()
        flxDetails.Rows.Clear()
        txtParNo.Text = UCase(txtParNo.Text)
        rsComSql = New ADODB.Recordset
        If cmbDept.Text = "Baguettes" Then
            dbName = "tblBAGPacket"
            rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblBAGPacket WHERE ParNo = '" & txtParNo.Text & "' AND DelDate IS NULL ORDER BY PktNo", AdoCN, 1, 1)

        ElseIf cmbDept.Text = "Princess" Then
            dbName = "tblPRPacket"
            rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblPRPacket WHERE ParNo = '" & txtParNo.Text & "'  AND DelDate IS NULL ORDER BY PktNo", AdoCN, 1, 1)

        ElseIf cmbDept.Text = "Rounds" Then
            dbName = "tblRndPacket"
            rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblRndPacket WHERE ParNo = '" & txtParNo.Text & "'  AND DelDate IS NULL ORDER BY CONVERT(NUMERIC, PktNo)", AdoCN, 1, 1)

        ElseIf cmbDept.Text = "Niru" Then
            dbName = "tblNiruPacket"
            rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblNiruPacket WHERE ParNo = '" & txtParNo.Text & "'  AND DelDate IS NULL ORDER BY PktNo", AdoCN, 1, 1)

        ElseIf cmbDept.Text = "Precision" Then
            dbName = "tblPacket"
            rsComSql.Open("SELECT PktOrdNo AS ParNo,PktNo,PktPcs,PktCts FROM tblPacket WHERE PktOrdNo = '" & txtParNo.Text & "'  AND DelDate IS NULL ORDER BY PktNo", AdoCN, 1, 1)

        ElseIf cmbDept.Text = "Rounds3" Or cmbDept.Text = "Rounds4" Or cmbDept.Text = "RoundsNLE" Or cmbDept.Text = "Emerald" Or cmbDept.Text = "Opening" Or cmbDept.Text = "Lamour" Or cmbDept.Text = "Davinci" Or cmbDept.Text = "Princess2" Or cmbDept.Text = "Emerald2" Or cmbDept.Text = "Emerald3" Or cmbDept.Text = "Baguettes2" Or cmbDept.Text = "Baguettes3" Or cmbDept.Text = "Carrer" Or cmbDept.Text = "Asscher" Or cmbDept.Text = "Radiant" Then
            dbName = "tblExtPacket"
            rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblExtPacket WHERE ParNo = '" & txtParNo.Text & "'  AND DelDate IS NULL AND Department = '" & cmbDept.Text & "' ORDER BY PktNo", AdoCN, 1, 1)

        ElseIf Mid(cmbDept.Text, 1, 5) = "Rough" Then
            dbName = "tblRprPacket"
            rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblRprPacket WHERE ParNo = '" & txtParNo.Text & "'  AND DelDate IS NULL AND Department = '" & cmbDept.Text & "' ORDER BY PktNo", AdoCN, 1, 1)
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
        txtIssPcs.Text = CalTotalPcs(flxDetails)
        txtIssCts.Text = CalTotalCts(flxDetails)
    End Sub

    Private Sub Load_AllPackets()
        flxSelected.Rows.Clear()
        txtParNo.Text = UCase(txtParNo.Text)
        rsComSql = New ADODB.Recordset
        If cmbDept.Text = "Baguettes" Then
            dbName = "tblBAGPacket"
            rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblBAGPacket WHERE ParNo = '" & txtParNo.Text & "' AND DelDate IS NULL ORDER BY PktNo", AdoCN, 1, 1)

        ElseIf cmbDept.Text = "Princess" Then
            dbName = "tblPRPacket"
            rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblPRPacket WHERE ParNo = '" & txtParNo.Text & "' AND DelDate IS NULL ORDER BY PktNo", AdoCN, 1, 1)

        ElseIf cmbDept.Text = "Rounds" Then
            dbName = "tblRndPacket"
            rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblRndPacket WHERE ParNo = '" & txtParNo.Text & "' AND DelDate IS NULL ORDER BY PktNo", AdoCN, 1, 1)

        ElseIf cmbDept.Text = "Niru" Then
            dbName = "tblNiruPacket"
            rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblNiruPacket WHERE ParNo = '" & txtParNo.Text & "' AND DelDate IS NULL ORDER BY PktNo", AdoCN, 1, 1)

        ElseIf cmbDept.Text = "Precision" Then
            dbName = "tblPacket"
            rsComSql.Open("SELECT PktOrdNo AS ParNo,PktNo,PktPcs,PktCts FROM tblPacket WHERE PktOrdNo = '" & txtParNo.Text & "'  AND DelDate IS NULL ORDER BY PktNo", AdoCN, 1, 1)

        ElseIf cmbDept.Text = "Rounds3" Or cmbDept.Text = "Rounds4" Or cmbDept.Text = "RoundsNLE" Or cmbDept.Text = "Emerald" Or cmbDept.Text = "Opening" Or cmbDept.Text = "Lamour" Or cmbDept.Text = "Davinci" Or cmbDept.Text = "Princess2" Or cmbDept.Text = "Emerald2" Or cmbDept.Text = "Emerald3" Or cmbDept.Text = "Baguettes2" Or cmbDept.Text = "Baguettes3" Or cmbDept.Text = "Carrer" Or cmbDept.Text = "Asscher" Or cmbDept.Text = "Radiant" Then
            dbName = "tblExtPacket"
            rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblExtPacket WHERE ParNo = '" & txtParNo.Text & "' AND DelDate IS NULL AND Department = '" & cmbDept.Text & "' ORDER BY PktNo", AdoCN, 1, 1)

        ElseIf Mid(cmbDept.Text, 1, 5) = "Rough" Then
            dbName = "tblRprPacket"
            If cmbModel.Text <> "" Then
                rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblRprPacket WHERE ParNo = '" & txtParNo.Text & "' AND DelDate IS NULL AND Department = '" & cmbDept.Text & "' AND Model = '" & cmbModel.Text & "' ORDER BY PktNo", AdoCN, 1, 1)
            Else
                rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblRprPacket WHERE ParNo = '" & txtParNo.Text & "' AND DelDate IS NULL AND Department = '" & cmbDept.Text & "' ORDER BY PktNo", AdoCN, 1, 1)
            End If
        End If
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxSelected.Rows.Add(rsComSql.Fields("ParNo").Value,
                                     rsComSql.Fields("PktNo").Value,
                                     rsComSql.Fields("PktPcs").Value,
                                     rsComSql.Fields("PktCts").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing

        flxDetails.Rows.Clear()
        txtCount.Text = flxSelected.RowCount
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
            If cmbDept.Text = "Baguettes" Then
                dbName = "tblBAGPacket"
                rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblBAGPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND DelDate IS NULL ORDER BY PktNo", AdoCN, 1, 1)

            ElseIf cmbDept.Text = "Rounds3" Or cmbDept.Text = "Rounds4" Or cmbDept.Text = "RoundsNLE" Or cmbDept.Text = "Emerald" Or cmbDept.Text = "Opening" Or cmbDept.Text = "Lamour" Or cmbDept.Text = "Davinci" Or cmbDept.Text = "Princess2" Or cmbDept.Text = "Emerald2" Or cmbDept.Text = "Emerald3" Or cmbDept.Text = "Baguettes2" Or cmbDept.Text = "Baguettes3" Or cmbDept.Text = "Carrer" Or cmbDept.Text = "Asscher" Or cmbDept.Text = "Radiant" Then
                dbName = "tblExtPacket"
                rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblExtPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND DelDate IS NULL AND Department = '" & cmbDept.Text & "' ORDER BY PktNo", AdoCN, 1, 1)

            Else
                rsComSql.Open("SELECT ParNo,PktNo,PktPcs,PktCts FROM tblRprPacket WHERE ParNo = '" & ParcelNo & "' AND PktNo = '" & PacketNo & "' AND DelDate IS NULL AND Department = '" & cmbDept.Text & "'", AdoCN, 1, 1)
            End If

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
        Dim mFlow As String

        If txtEmpNo.Text = "" Or Len(txtEmpNo.Text) < 6 Then
            MsgBox("Please enter a Valied Employee No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        If Len(txtEmpNo.Text) > 6 Then
            MsgBox("Please enter a Valied Employee No", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            Exit Sub
        End If

        'If Mid(cmbDept.Text, 1, 5) = "Rough" Then
        '    rsComSql = New ADODB.Recordset
        '    rsComSql.Open("SELECT MAX(DATEDIFF(D, dbo.tblRPrIssues.IssDate, GETDATE())) AS Days " & _
        '                  "FROM dbo.tblRPrIssues INNER JOIN dbo.tblRPrPacket ON dbo.tblRPrIssues.Department = dbo.tblRPrPacket.Department AND dbo.tblRPrIssues.ParNo = dbo.tblRPrPacket.ParNo AND  " & _
        '                        "dbo.tblRPrIssues.PktNo = dbo.tblRPrPacket.PktNo LEFT OUTER JOIN " & _
        '                        "dbo.tblRPrReturns ON dbo.tblRPrIssues.Department = dbo.tblRPrReturns.Department AND dbo.tblRPrIssues.ParNo = dbo.tblRPrReturns.ParNo AND  " & _
        '                        "dbo.tblRPrIssues.PktNo = dbo.tblRPrReturns.PktNo And dbo.tblRPrIssues.Sec = dbo.tblRPrReturns.Sec " & _
        '                  "WHERE (dbo.tblRPrReturns.Department IS NULL) AND (dbo.tblRPrIssues.EmpNo = '" & txtEmpNo.Text & "')", AdoCN, 1, 1)
        '    If Not IsDBNull(rsComSql.Fields("Days").Value) Then
        '        If rsComSql.Fields("Days").Value > 6 Then
        '            PBResponse = MsgBox("Expire Packets Found. Do you want to Proceed?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        '            If PBResponse  = MsgBoxResult.Yes Then

        '            Else
        '                Exit Sub
        '            End If
        '        End If
        '    End If
        '    rsComSql = Nothing
        'End If

        If Mid(cmbDept.Text, 1, 5) = "Rough" Then
            If cmbDept.Text = "RoughBruting" Then

            Else
                rsComSql_2 = New ADODB.Recordset
                rsComSql_2.Open("SELECT EmpNo FROM tblRprReturns WHERE EmpNo = '" & txtEmpNo.Text & "' AND LostPcs > 0 AND Active = 1", AdoCN, 1, 1)
                If rsComSql_2.RecordCount Then
                    MsgBox("Have a Lost to this Employee", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
                rsComSql_2 = Nothing
            End If
        End If

        For y = 0 To flxSelected.Rows.Count - 1
            If Mid(cmbDept.Text, 1, 5) = "Rough" Then

                mStrSQL = "UPDATE tblRPrPacket SET DelDate = '" & Format(Date.Now, "MM/dd/yyyy") & "',DelEmp = '" & UCase(txtEmpNo.Text) & "',DelBy = '" & PBUser_ID & "',DelTime = '" & Format(Date.Now, "HH:mm:ss") & "' " & _
                          "WHERE ParNo = '" & flxSelected.Item(0, y).Value & "' AND PktNo = '" & flxSelected.Item(1, y).Value & "' AND Department = '" & cmbDept.Text & "'"
                AdoCN.Execute(mStrSQL)

                If cmbDept.Text = "RoughBruting" Then

                Else
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT PktFlow FROM tblRPrPacket WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & flxSelected.Item(0, y).Value & "' AND PktNo = '" & flxSelected.Item(1, y).Value & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        mFlow = rsComSql.Fields("PktFlow").Value

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT PktNo FROM tblRPrIssues WHERE Department = '" & cmbDept.Text & "' AND ParNo = '" & flxSelected.Item(0, y).Value & "' AND PktNo = '" & flxSelected.Item(1, y).Value & "'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            mStrSQL = "INSERT INTO tblRPrIssues(Department,ParNo,PktNo,Flow,EmpNo,IssPcsT,IssPcsB,IssCts,IssDate,IssTime,Sec,SecCount) " & _
                                      "VALUES('" & cmbDept.Text & "','" & flxSelected.Item(0, y).Value & "','" & flxSelected.Item(1, y).Value & "','" & mFlow & "','" & Mid(Trim(txtEmpNo.Text), 1, 6) & "'," & CDbl(flxSelected.Item(2, y).Value) & "," & _
                                        "0," & CSng(flxSelected.Item(3, y).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',1,1)"

                            AdoCN.Execute(mStrSQL)
                        End If
                        rsComSql_1 = Nothing

                    End If
                    rsComSql = Nothing
                End If

            ElseIf cmbDept.Text = "Rounds3" Or cmbDept.Text = "Rounds4" Or cmbDept.Text = "RoundsNLE" Or cmbDept.Text = "Emerald" Or cmbDept.Text = "Opening" Or cmbDept.Text = "Lamour" Or cmbDept.Text = "Davinci" Or cmbDept.Text = "Princess2" Or cmbDept.Text = "Emerald2" Or cmbDept.Text = "Emerald3" Or cmbDept.Text = "Baguettes2" Or cmbDept.Text = "Baguettes3" Or cmbDept.Text = "Carrer" Or cmbDept.Text = "Asscher" Or cmbDept.Text = "Radiant" Then
                mStrSQL = "UPDATE tblExtPacket SET DelDate = '" & Format(Date.Now, "MM/dd/yyyy") & "',DelEmp = '" & UCase(txtEmpNo.Text) & "',DelBy = '" & PBUser_ID & "',DelTime = '" & Format(Date.Now, "HH:mm:ss") & "' " & _
                          "WHERE ParNo = '" & flxSelected.Item(0, y).Value & "' AND PktNo = '" & flxSelected.Item(1, y).Value & "' AND Department = '" & cmbDept.Text & "'"
                AdoCN.Execute(mStrSQL)

            ElseIf cmbDept.Text = "Precision" Then
                mStrSQL = "UPDATE tblPacket SET DelDate = '" & Format(Date.Now, "MM/dd/yyyy") & "',DelEmp = '" & UCase(txtEmpNo.Text) & "',DelBy = '" & PBUser_ID & "',DelTime = '" & Format(Date.Now, "HH:mm:ss") & "' " & _
                          "WHERE PktOrdNo = '" & flxSelected.Item(0, y).Value & "' AND PktNo = '" & flxSelected.Item(1, y).Value & "'"
                AdoCN.Execute(mStrSQL)

            Else
                mStrSQL = "UPDATE " & dbName & " SET DelDate = '" & Format(Date.Now, "MM/dd/yyyy") & "',DelEmp = '" & UCase(txtEmpNo.Text) & "',DelBy = '" & PBUser_ID & "',DelTime = '" & Format(Date.Now, "HH:mm:ss") & "' " & _
                          "WHERE ParNo = '" & flxSelected.Item(0, y).Value & "' AND PktNo = '" & flxSelected.Item(1, y).Value & "'"
                AdoCN.Execute(mStrSQL)

            End If
        Next

        MsgBox("Packets Issued", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)

        ClearText()

    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearText()
    End Sub

    Private Sub ClearText()
        flxDetails.Rows.Clear()
        flxSelected.Rows.Clear()
        dbName = ""
        txtEmpNo.Text = ""
        txtParNo.Text = ""
        txtCount.Text = ""
        cmbModel.Text = ""
        txtIssPcs.Text = ""
        txtIssCts.Text = ""
    End Sub

    Private Sub flxSelected_DoubleClick(sender As Object, e As EventArgs) Handles flxSelected.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse  = MsgBoxResult.Yes Then
            flxSelected.Rows.RemoveAt(flxSelected.CurrentRow.Index)
            txtCount.Text = flxSelected.RowCount
        End If
    End Sub

    Private Sub cmdRounds_Click(sender As Object, e As EventArgs) Handles cmdRounds.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRndProdIss_DateWise.rpt"
        strReportPath = PBReportPath & "RoundsFullFlow\" & mReportName
        objForm.Show()
    End Sub

    Private Sub cmdBag_Click(sender As Object, e As EventArgs) Handles cmdBag.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptBagProdIss_DateWise.rpt"
        strReportPath = PBReportPath & "Baguettes\" & mReportName
        objForm.Show()
    End Sub

    Private Sub cmdExt_Click(sender As Object, e As EventArgs) Handles cmdExt.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptExtProdIss_ParWise.rpt"
        strReportPath = PBReportPath & "Ext\" & mReportName
        objForm.Show()
    End Sub

    Private Sub cmdBrut_Click(sender As Object, e As EventArgs) Handles cmdBrut.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptRprProdIss_DateWise.rpt"
        strReportPath = PBReportPath & "Rpr\" & mReportName
        objForm.Show()
    End Sub

    Private Function CalTotalPcs(ByVal flxSample As System.Windows.Forms.DataGridView) As Integer
        Dim intRow As Integer

        CalTotalPcs = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalPcs = CalTotalPcs + Val(flxSample.Item(2, intRow).Value)
        Next

    End Function

    Private Function CalTotalCts(ByVal flxSample As System.Windows.Forms.DataGridView) As Double
        Dim intRow As Integer

        CalTotalCts = 0
        For intRow = 0 To flxSample.Rows.Count - 1
            CalTotalCts = CalTotalCts + Val(flxSample.Item(3, intRow).Value)
        Next
        CalTotalCts = Math.Round(CalTotalCts, 3)
    End Function

    Private Sub HazelDev_Button1_Click(sender As Object, e As EventArgs) Handles HazelDev_Button1.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPRProdIss_DateWise.rpt"
        strReportPath = PBReportPath & "Princess\" & mReportName
        objForm.Show()
    End Sub

    Private Sub HazelDev_Button2_Click(sender As Object, e As EventArgs) Handles HazelDev_Button2.Click
        objForm = New frm_DCLReportViewer
        mReportName = "crptPCUModIss.rpt"
        strReportPath = PBReportPath & "Precision\" & mReportName
        objForm.Show()
    End Sub
End Class