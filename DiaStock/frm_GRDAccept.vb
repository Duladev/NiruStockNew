
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_GRDAccept

    Private Sub frm_GRDAccept_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        Load_DepartmentProd(cmbDepartment)
    End Sub

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ClearFields()
        cmbDepartment.Text = ""
        txtSearch.Text = ""
        flxParcel.Rows.Clear()
        flxAvailable.Rows.Clear()
        flxSelected.Rows.Clear()
        txtPcs.Text = ""
        txtCts.Text = ""
        txtActPcs.Text = ""
        txtActCts.Text = ""
        txtGroup.Text = ""
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
    End Sub

    Private Sub cmbDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDepartment.SelectedIndexChanged
        Load_Parcels()
    End Sub

    Private Sub Load_Parcels()
        If cmbDepartment.Text = "" Then Exit Sub

        flxParcel.Rows.Clear()
        flxAvailable.Rows.Clear()
        flxSelected.Rows.Clear()

        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT ParcelNo, MAX(Trfdate) AS Date1 " & _
                      "FROM dbo.tblGradingTrf WHERE Department = '" & cmbDepartment.Text & "' AND Status = 0 " & _
                      "GROUP BY ParcelNo ORDER BY ParcelNo", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxParcel.Rows.Add(rsComSql.Fields("ParcelNo").Value,
                                   Format(rsComSql.Fields("Date1").Value, "yyyy/MM/dd"))
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub flxParcel_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxParcel.CellClick
        Dim selected_parno As String

        If flxParcel.Rows.Count > 0 Then
            selected_parno = flxParcel.Item(0, flxParcel.CurrentRow.Index).Value
            flxAvailable.Rows.Clear()
            flxSelected.Rows.Clear()

            txtSearch.Text = selected_parno
            rsComSql = New ADODB.Recordset
            If txtGroup.Text = "" Then
                rsComSql.Open("SELECT ParcelNo,PktNo,Trf_Pcs,ROUND(Trf_Cts, 3) AS Trf_Cts,Rgh_Pcs,ROUND(Rgh_Cts, 3) AS Rgh_Cts,Assort1,Grp " & _
                              "FROM dbo.tblGradingTrf WHERE Department = '" & cmbDepartment.Text & "' AND ParcelNo = '" & selected_parno & "' AND Status = 0 " & _
                              "ORDER BY ParcelNo,PktNo", AdoCN, 1, 1)
            Else
                rsComSql.Open("SELECT ParcelNo,PktNo,Trf_Pcs,ROUND(Trf_Cts, 3) AS Trf_Cts,Rgh_Pcs,ROUND(Rgh_Cts, 3) AS Rgh_Cts,Assort1,Grp " & _
                              "FROM dbo.tblGradingTrf WHERE Department = '" & cmbDepartment.Text & "' AND ParcelNo = '" & selected_parno & "' AND Status = 0 AND Grp = '" & txtGroup.Text & "' " & _
                              "ORDER BY ParcelNo,PktNo", AdoCN, 1, 1)
            End If
            
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    flxAvailable.Rows.Add(rsComSql.Fields("ParcelNo").Value,
                                          rsComSql.Fields("PktNo").Value,
                                          rsComSql.Fields("Trf_Pcs").Value,
                                          rsComSql.Fields("Trf_Cts").Value,
                                          rsComSql.Fields("Rgh_Pcs").Value,
                                          rsComSql.Fields("Rgh_Cts").Value,
                                          rsComSql.Fields("Grp").Value,
                                          rsComSql.Fields("Assort1").Value)
                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing
        End If
        
    End Sub

    Private Sub flxAvailable_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxAvailable.CellClick
        Dim intRow As Integer

        If cmbDepartment.Text <> "" Then
            For intRow = 0 To flxSelected.Rows.Count - 1
                If flxAvailable.Item(0, flxAvailable.CurrentRow.Index).Value = flxSelected.Item(0, intRow).Value And flxAvailable.Item(1, flxAvailable.CurrentRow.Index).Value = flxSelected.Item(1, intRow).Value Then
                    MsgBox("Already Selected", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                    Exit Sub
                End If
            Next

            flxSelected.Rows.Add(flxAvailable.Item(0, flxAvailable.CurrentRow.Index).Value,
                                 flxAvailable.Item(1, flxAvailable.CurrentRow.Index).Value,
                                 flxAvailable.Item(2, flxAvailable.CurrentRow.Index).Value,
                                 flxAvailable.Item(3, flxAvailable.CurrentRow.Index).Value,
                                 flxAvailable.Item(4, flxAvailable.CurrentRow.Index).Value,
                                 flxAvailable.Item(5, flxAvailable.CurrentRow.Index).Value,
                                 flxAvailable.Item(6, flxAvailable.CurrentRow.Index).Value,
                                 flxAvailable.Item(7, flxAvailable.CurrentRow.Index).Value)

            flxAvailable.Rows.RemoveAt(flxAvailable.CurrentRow.Index)
            txtPcs.Text = CalTotalPcs(flxSelected)
            txtCts.Text = CalTotalCts(flxSelected)
            txtActPcs.Text = txtPcs.Text
            txtActCts.Text = txtCts.Text
        End If
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

    Private Sub flxSelected_DoubleClick(sender As Object, e As EventArgs) Handles flxSelected.DoubleClick
        PBResponse = MsgBox("Are you sure to Remove?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            flxSelected.Rows.RemoveAt(flxSelected.CurrentRow.Index)
            txtPcs.Text = CalTotalPcs(flxSelected)
            txtCts.Text = CalTotalCts(flxSelected)
        End If
    End Sub

    Private Sub SaveAccept()
        Dim u As Integer
        Dim dblMaxID As Double
        Dim strPktNo As String
        Dim dblDifCts As Double
        Dim dblPrice As Double
        Dim blnFound As Boolean
        Dim intOpen As Integer
        Dim intSec As Integer
        Dim strOrderNo As String
        Dim strAssortment As String

        If txtActPcs.Text <> "" And txtActCts.Text <> "" Then
            If CDbl(txtActCts.Text) = 0 Then MsgBox("Invalid Actual Cts", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
            If flxSelected.Rows.Count = 0 Then
                MsgBox("Need to Select a Parcel First", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, Me.Text)
            Else
                If strDBName = "DiaSales" Then
                    If chkSize1.Checked = True Then
                        MsgBox("Wrong Selection of 1st Sizing", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                        Exit Sub
                    End If
                End If

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT * FROM tblGradingTrf_Diff WHERE Depart = '" & cmbDepartment.Text & "' AND ParcelNo = '" & txtSearch.Text & "' AND TrDate = '" & Format(Date.Now, "MM/dd/yyyy") & "' AND Pcs = " & CDbl(txtPcs.Text) & "", AdoCN, 1, 1)
                If rsComSql.RecordCount = 0 Then
                    AdoCN.Execute("INSERT INTO tblGradingTrf_Diff(Depart,ParcelNo,Pcs,Cts,ActPcs,ActCts,TrDate) " & _
                                  "VALUES('" & cmbDepartment.Text & "','" & txtSearch.Text & "','" & CDbl(txtPcs.Text) & "','" & CDbl(txtCts.Text) & "','" & CDbl(txtActPcs.Text) & "','" & CDbl(txtActCts.Text) & "','" & Format(Date.Now, "MM/dd/yyyy") & "')")
                End If
                rsComSql = Nothing

                rsComSql = New ADODB.Recordset
                rsComSql.Open("SELECT MAX(ID) AS MaxID FROM tblGradingTrf_Diff", AdoCN, 1, 1)
                dblMaxID = rsComSql.Fields("MaxID").Value
                rsComSql = Nothing

                dblDifCts = CDbl(txtActCts.Text) - CDbl(txtCts.Text)
                dblDifCts = Math.Round(dblDifCts, 3)
                Select Case cmbDepartment.Text
                    Case "Rounds"
                        If Len(flxSelected.Item(1, 0).Value) = 3 Then
                            rsComSql = New ADODB.Recordset
                            rsComSql.Open("SELECT * FROM tblRndReturns " & _
                                          "WHERE ParNo = '" & flxSelected.Item(0, 0).Value & "' AND " & _
                                                "PktNo = '" & flxSelected.Item(1, 0).Value & "' AND Sec = 25 AND Gra_Trf = 1", AdoCN, 1, 1)
                            If rsComSql.RecordCount = 1 Then
                                AdoCN.Execute("UPDATE tblRndReturns SET RetCts = RetCts + " & dblDifCts & " " & _
                                              "WHERE ParNo = '" & flxSelected.Item(0, 0).Value & "' AND " & _
                                                    "PktNo = '" & flxSelected.Item(1, 0).Value & "' AND Sec = 25 AND Gra_Trf = 1")
                            End If
                            rsComSql = Nothing
                        End If

                    Case "RoundsNLE", "Emerald", "Lamour", "Davinci", "Opening", "Carrer", "Asscher", "Radiant"
                        If Len(flxSelected.Item(1, 0).Value) = 3 Then
                            rsComSql = New ADODB.Recordset
                            rsComSql.Open("SELECT * FROM tblExtReturns " & _
                                          "WHERE ParNo = '" & flxSelected.Item(0, 0).Value & "' AND " & _
                                                "PktNo = '" & flxSelected.Item(1, 0).Value & "' AND Sec = 25 AND Gra_Trf = 1 AND Department = '" & cmbDepartment.Text & "'", AdoCN, 1, 1)
                            If rsComSql.RecordCount = 1 Then
                                AdoCN.Execute("UPDATE tblExtReturns SET RetCts = RetCts + " & dblDifCts & " " & _
                                              "WHERE ParNo = '" & flxSelected.Item(0, 0).Value & "' AND " & _
                                                    "PktNo = '" & flxSelected.Item(1, 0).Value & "' AND Sec = 25 AND Gra_Trf = 1 AND Department = '" & cmbDepartment.Text & "'")
                            End If
                            rsComSql = Nothing
                        End If

                    Case "Baguettes"
                        If Mid(flxSelected.Item(1, 0).Value, 1, 1) <> "P" Then
                            rsComSql = New ADODB.Recordset
                            rsComSql.Open("SELECT * FROM tblBagReturns " & _
                                          "WHERE ParNo = '" & flxSelected.Item(0, 0).Value & "' AND " & _
                                                "PktNo = '" & flxSelected.Item(1, 0).Value & "' AND Sec = 10 AND Gra_Trf = 1", AdoCN, 1, 1)
                            If rsComSql.RecordCount = 1 Then
                                AdoCN.Execute("UPDATE tblBagReturns SET RetCts = RetCts + " & dblDifCts & " " & _
                                              "WHERE ParNo = '" & flxSelected.Item(0, 0).Value & "' AND " & _
                                                    "PktNo = '" & flxSelected.Item(1, 0).Value & "' AND Sec = 10 AND Gra_Trf = 1")
                            End If
                            rsComSql = Nothing
                        End If

                    Case "Princess"
                        If Len(flxSelected.Item(1, 0).Value) = 3 Then
                            rsComSql = New ADODB.Recordset
                            rsComSql.Open("SELECT * FROM tblPRReturns " & _
                                          "WHERE ParNo = '" & flxSelected.Item(0, 0).Value & "' AND " & _
                                                "PktNo = '" & flxSelected.Item(1, 0).Value & "' AND Sec = 9 AND Gra_Trf = 1", AdoCN, 1, 1)
                            If rsComSql.RecordCount = 1 Then
                                AdoCN.Execute("UPDATE tblPRReturns SET RetCtsP = RetCtsP + " & dblDifCts & " " & _
                                              "WHERE ParNo = '" & flxSelected.Item(0, 0).Value & "' AND " & _
                                                    "PktNo = '" & flxSelected.Item(1, 0).Value & "' AND Sec = 9 AND Gra_Trf = 1")
                            End If
                            rsComSql = Nothing
                        End If

                End Select

                If chkSize1.Checked = True Then
                    intOpen = 1
                Else
                    intOpen = 0
                End If

                For u = 0 To flxSelected.Rows.Count - 1
                    GradingAcceptations(flxSelected.Item(0, u).Value, flxSelected.Item(1, u).Value, dblMaxID, intOpen)

                    If cmbDepartment.Text <> "Mix" And cmbDepartment.Text <> "GradingMix" And cmbDepartment.Text <> "GradingPCU" And cmbDepartment.Text <> "GradingPCU_N" And cmbDepartment.Text <> "Opening" Then
                        If Mid(flxSelected.Item(1, u).Value, 1, 1) = "P" Or Mid(flxSelected.Item(1, u).Value, 1, 1) = "V" Or Mid(flxSelected.Item(1, u).Value, 1, 1) = "K" Or Mid(flxSelected.Item(1, u).Value, 1, 1) = "G" Then
                            If chkBoiling.Checked = True Or chkChecking.Checked = True Then
                                rsComSql = New ADODB.Recordset
                                rsComSql.Open("SELECT * FROM tblGrading_BoilingIssues WHERE Department = '" & cmbDepartment.Text & "' AND ParNo = '" & flxSelected.Item(0, u).Value & "' AND PktNo = '" & flxSelected.Item(1, u).Value & "'", AdoCN, 1, 1)
                                If rsComSql.RecordCount = 0 Then
                                    'Boiling Issues
                                    AdoCN.Execute("INSERT INTO tblGrading_BoilingIssues(Department,ParNo,PktNo,EmpNo,IssPcs,IssCts,IssDate,IssTime,UserName,Remarks) " & _
                                                  "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "','" & PBUser_EmpNo & "'," & CInt(flxSelected.Item(2, u).Value) & "," & _
                                                         "" & CDbl(flxSelected.Item(3, u).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "','')")

                                    rsComSql_1 = New ADODB.Recordset
                                    rsComSql_1.Open("SELECT * FROM tblGrading_Parcel WHERE ParNo = '" & Mid(flxSelected.Item(0, u).Value, 1, 6) & "' AND Dept = 'Grading'", AdoCN, 1, 1)
                                    If rsComSql_1.RecordCount = 0 Then
                                        AdoCN.Execute("INSERT INTO tblGrading_Parcel(ParNo,Complete,Dept) VALUES('" & Mid(flxSelected.Item(0, u).Value, 1, 6) & "',0,'Grading')")
                                    End If
                                    rsComSql_1 = Nothing

                                    If chkChecking.Checked = True Then
                                        'Boiling Returns
                                        AdoCN.Execute("INSERT INTO tblGrading_BoilingReturns(Department,ParNo,PktNo,EmpNo,RetPcs,RetCts, " & _
                                                              "LostPcs,LostCts,RejPcs,RejCts,RetDate,RetTime,UserName,Trf) " & _
                                                        "VALUES ('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "','" & PBUser_EmpNo & "'," & CInt(flxSelected.Item(2, u).Value) & "," & CDbl(flxSelected.Item(3, u).Value) & ",0,0,0" & _
                                                              ",0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "',0)")

                                        If Mid(flxSelected.Item(1, u).Value, 1, 1) = "G" Then
                                            'Checking Issues
                                            AdoCN.Execute("INSERT INTO tblGrading_CheckingIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime,UserName) " & _
                                                        "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "',2,'" & PBUser_EmpNo & "','" & CInt(flxSelected.Item(2, u).Value) & "'," & _
                                                               "'" & CDbl(flxSelected.Item(3, u).Value) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "')")

                                            'Checking Returns
                                            AdoCN.Execute("INSERT INTO tblGrading_CheckingReturns(Department,ParNo,PktNo,Sec,EmpNo,ExPcs,ExCts,VgPcs,VgCts," & _
                                                            "BlPcs,BlCts,PsPcs,PsCts,ScPcs,ScCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts,UserName,SzPcs,SzCts,OkPcs,OkCts) " & _
                                                        "VALUES ('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "',2,'" & PBUser_EmpNo & "'," & CInt(flxSelected.Item(2, u).Value) & "," & CDbl(flxSelected.Item(3, u).Value) & ",0,0,0,0,0,0,0,0,0,0," & _
                                                              "0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,'" & PBUser_EmpNo & "',0,0,0,0)")

                                            'Color Issues
                                            AdoCN.Execute("INSERT INTO tblGrading_Issues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                                        "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "',1,'" & PBUser_EmpNo & "','" & CInt(flxSelected.Item(2, u).Value) & "'," & _
                                                               "'" & CDbl(flxSelected.Item(3, u).Value) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")

                                            'Color Returns
                                            AdoCN.Execute("INSERT INTO tblGrading_Returns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                                                        "VALUES ('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "',1,'" & PBUser_EmpNo & "'," & CInt(flxSelected.Item(2, u).Value) & "," & CDbl(flxSelected.Item(3, u).Value) & ",0,0," & _
                                                              "0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0)")

                                            'Color Details
                                            AdoCN.Execute("INSERT INTO tblGrading_ReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                                                        "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "',1,'COLLECTION'," & CInt(flxSelected.Item(2, u).Value) & "," & CDbl(flxSelected.Item(3, u).Value) & ")")

                                            'Sizing Packet
                                            AdoCN.Execute("INSERT INTO tblGrading_SizingPacket(Department, ParNo, PktNo, PktPcs, PktCts, ReturnType, PktDate) " & _
                                                        "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "'," & CInt(flxSelected.Item(2, u).Value) & "," & CDbl(flxSelected.Item(3, u).Value) & ",'COLLECTION','" & Format(Date.Now, "MM/dd/yyyy") & "')")

                                            'Sizing Issues
                                            AdoCN.Execute("INSERT INTO tblGrading_SizingIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                                        "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "',1,'" & PBUser_EmpNo & "','" & CInt(flxSelected.Item(2, u).Value) & "'," & _
                                                               "'" & CDbl(flxSelected.Item(3, u).Value) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")

                                            'Sizing Returns
                                            AdoCN.Execute("INSERT INTO tblGrading_SizingReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts," & _
                                                              "LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts,RghPcs,RghCts) " & _
                                                        "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "',1,'" & PBUser_EmpNo & "'" & _
                                                              "," & CInt(flxSelected.Item(2, u).Value) & "," & CDbl(flxSelected.Item(3, u).Value) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0)")

                                            'Sizing Details
                                            AdoCN.Execute("INSERT INTO tblGrading_SizingTypes(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts,OK) " & _
                                                        "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "',1," & _
                                                          "'" & flxSelected.Item(6, u).Value & "'," & CInt(flxSelected.Item(2, u).Value) & "," & CDbl(flxSelected.Item(3, u).Value) & ",0)")

                                        End If
                                    End If
                                End If
                            End If
                            rsComSql = Nothing
                        Else
                            If cmbDepartment.Text = "Rounds" Or cmbDepartment.Text = "Baguettes" Or cmbDepartment.Text = "Princess" Or cmbDepartment.Text = "Emerald" Or cmbDepartment.Text = "Baguettes2" Or cmbDepartment.Text = "Asscher" Or cmbDepartment.Text = "Emerald2" Or cmbDepartment.Text = "Carrer" Or cmbDepartment.Text = "Radiant" Then
                                If chkBoiling.Checked = True Then
                                    rsComSql = New ADODB.Recordset
                                    rsComSql.Open("SELECT * FROM tblGrading_BoilingIssues WHERE Department = '" & cmbDepartment.Text & "' AND ParNo = '" & flxSelected.Item(0, u).Value & "' AND PktNo = '" & flxSelected.Item(1, u).Value & "'", AdoCN, 1, 1)
                                    If rsComSql.RecordCount = 0 Then
                                        'Boiling Issues
                                        AdoCN.Execute("INSERT INTO tblGrading_BoilingIssues(Department,ParNo,PktNo,EmpNo,IssPcs,IssCts,IssDate,IssTime,UserName,Remarks,Grp) " & _
                                                      "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "','" & PBUser_EmpNo & "'," & CInt(flxSelected.Item(2, u).Value) & "," & _
                                                             "" & CDbl(flxSelected.Item(3, u).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "','','" & flxSelected.Item(6, u).Value & "')")

                                        rsComSql_1 = New ADODB.Recordset
                                        rsComSql_1.Open("SELECT * FROM tblGrading_Parcel WHERE ParNo = '" & Mid(flxSelected.Item(0, u).Value, 1, 6) & "' AND Dept = 'Grading'", AdoCN, 1, 1)
                                        If rsComSql_1.RecordCount = 0 Then
                                            AdoCN.Execute("INSERT INTO tblGrading_Parcel(ParNo,Complete,Dept) VALUES('" & Mid(flxSelected.Item(0, u).Value, 1, 6) & "',0,'Grading')")
                                        End If
                                        rsComSql_1 = Nothing

                                    End If
                                    rsComSql = Nothing
                                End If
                            End If
                        End If
                    End If

                    If cmbDepartment.Text = "Opening" Then
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT * FROM tblGrading_BoilingIssues WHERE Department = '" & cmbDepartment.Text & "' AND ParNo = '" & flxSelected.Item(0, u).Value & "' AND PktNo = '" & flxSelected.Item(1, u).Value & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount = 0 Then
                            'Boiling Issues
                            AdoCN.Execute("INSERT INTO tblGrading_BoilingIssues(Department,ParNo,PktNo,EmpNo,IssPcs,IssCts,IssDate,IssTime,UserName,Remarks) " & _
                                          "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "','D08411','" & CInt(flxSelected.Item(2, u).Value) & "'," & _
                                                 "'" & CDbl(flxSelected.Item(3, u).Value) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "','')")

                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT * FROM tblGrading_Parcel WHERE ParNo = '" & Mid(flxSelected.Item(0, u).Value, 1, 6) & "' AND Dept = 'Grading'", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount = 0 Then
                                AdoCN.Execute("INSERT INTO tblGrading_Parcel(ParNo,Complete,Dept) VALUES('" & Mid(flxSelected.Item(0, u).Value, 1, 6) & "',0,'Grading')")
                            End If
                            rsComSql_1 = Nothing

                            If chkSorting.Checked = True Then
                                'Boiling Returns
                                AdoCN.Execute("INSERT INTO tblGrading_BoilingReturns(Department,ParNo,PktNo,EmpNo,RetPcs,RetCts, " & _
                                                  "LostPcs,LostCts,RejPcs,RejCts,RetDate,RetTime,UserName,Trf) " & _
                                              "VALUES ('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "','D08411'," & CInt(flxSelected.Item(2, u).Value) & "," & CDbl(flxSelected.Item(3, u).Value) & ",0,0,0" & _
                                                  ",0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "',1)")

                                'Exp Packet
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT * FROM tblExpPacket WHERE Department = '" & cmbDepartment.Text & "' AND ParNo = '" & flxSelected.Item(0, u).Value & "' AND PktNo = '" & flxSelected.Item(1, u).Value & "'", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount = 0 Then
                                    AdoCN.Execute("INSERT INTO tblExpPacket(Department,ParNo,PktNo,PktPcs,PktCts,PktType,PktDate,AParNo,AMS2,YAH) " & _
                                                  "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "'," & CInt(flxSelected.Item(2, u).Value) & "," & CDbl(flxSelected.Item(3, u).Value) & ",'','" & Format(Date.Now, "MM/dd/yyyy") & "','" & flxSelected.Item(0, u).Value & "',1,1)")
                                End If
                                rsComSql_1 = Nothing
                            End If

                            If chkSizeFinish.Checked = True Then
                                'Boiling Returns
                                AdoCN.Execute("INSERT INTO tblGrading_BoilingReturns(Department,ParNo,PktNo,EmpNo,RetPcs,RetCts, " & _
                                                  "LostPcs,LostCts,RejPcs,RejCts,RetDate,RetTime,UserName,Trf) " & _
                                              "VALUES ('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "','D08411'," & CInt(flxSelected.Item(2, u).Value) & "," & CDbl(flxSelected.Item(3, u).Value) & ",0,0,0" & _
                                                  ",0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "',1)")

                                'Exp Packet
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT * FROM tblExpPacket WHERE Department = '" & cmbDepartment.Text & "' AND ParNo = '" & flxSelected.Item(0, u).Value & "' AND PktNo = '" & flxSelected.Item(1, u).Value & "'", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount = 0 Then
                                    AdoCN.Execute("INSERT INTO tblExpPacket(Department,ParNo,PktNo,PktPcs,PktCts,PktType,PktDate,AParNo,AMS2,YAH) " & _
                                                  "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "'," & CInt(flxSelected.Item(2, u).Value) & "," & CDbl(flxSelected.Item(3, u).Value) & ",'','" & Format(Date.Now, "MM/dd/yyyy") & "','" & flxSelected.Item(0, u).Value & "',1,1)")
                                End If
                                rsComSql_1 = Nothing

                                For intSec = 1 To 3
                                    rsComSql_1 = New ADODB.Recordset
                                    rsComSql_1.Open("SELECT * FROM tblExpIssues WHERE Department = '" & cmbDepartment.Text & "' AND ParNo = '" & flxSelected.Item(0, u).Value & "' AND PktNo = '" & flxSelected.Item(1, u).Value & "' AND Sec = " & intSec & "", AdoCN, 1, 1)
                                    If rsComSql_1.RecordCount = 0 Then
                                        AdoCN.Execute("INSERT INTO tblExpIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                                      "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "'," & intSec & ",'" & PBUser_EmpNo & "'," & CInt(flxSelected.Item(2, u).Value) & "," & _
                                                             "" & CDbl(flxSelected.Item(3, u).Value) & ",'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")
                                    Else
                                        MsgBox("Already Issued", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                                        Exit Sub
                                    End If
                                    rsComSql_1 = Nothing

                                    rsComSql_1 = New ADODB.Recordset
                                    rsComSql_1.Open("SELECT * FROM tblExpReturns WHERE Department = '" & cmbDepartment.Text & "' AND ParNo = '" & flxSelected.Item(0, u).Value & "' AND PktNo = '" & flxSelected.Item(1, u).Value & "' AND Sec = " & intSec & "", AdoCN, 1, 1)
                                    If rsComSql_1.RecordCount = 0 Then
                                        AdoCN.Execute("INSERT INTO tblExpReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts,ColPcs,ColCts,FloPcs,FloCts,IncPcs,IncCts) " & _
                                                      "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "'," & intSec & ",'" & PBUser_EmpNo & "'," & _
                                                            "" & CInt(flxSelected.Item(2, u).Value) & "," & CDbl(flxSelected.Item(3, u).Value) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0,0,0,0,0)")
                                    End If
                                    rsComSql_1 = Nothing

                                    AdoCN.Execute("DELETE FROM tblExpReturnDetails WHERE Department = '" & cmbDepartment.Text & "' AND ParNo = '" & flxSelected.Item(0, u).Value & "' AND PktNo = '" & flxSelected.Item(1, u).Value & "' AND Sec = " & intSec & "")
                                    AdoCN.Execute("INSERT INTO tblExpReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                                                  "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "'," & intSec & ",'MIX'," & CInt(flxSelected.Item(2, u).Value) & "," & CDbl(flxSelected.Item(3, u).Value) & ")")
                                Next

                                strPktNo = "K001"
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT MAX(RIGHT(PktNo, 3)) AS MaxPktNo FROM tblExpSizingPacket WHERE ParNo = '" & flxSelected.Item(0, u).Value & "' AND Department = '" & cmbDepartment.Text & "' AND LEFT(PktNo, 1) = 'K'", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount Then
                                    If Not IsDBNull(rsComSql_1.Fields("MaxPktNo").Value) Then
                                        strPktNo = "K" & Format(rsComSql_1.Fields("MaxPktNo").Value + 1, "000")
                                    Else
                                        strPktNo = "K001"
                                    End If
                                End If
                                rsComSql_1 = Nothing

                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT * FROM tblExpSizingPacket WHERE ParNo = '" & flxSelected.Item(0, u).Value & "' AND PktNo = '" & strPktNo & "' AND Department = '" & cmbDepartment.Text & "'", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount = 0 Then
                                    AdoCN.Execute("INSERT INTO tblExpSizingPacket(Department, ParNo, PktNo, PktPcs, PktCts, ReturnType, PktNo2) " & _
                                                  "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & strPktNo & "'," & CInt(flxSelected.Item(2, u).Value) & "," & CDbl(flxSelected.Item(3, u).Value) & ",'" & flxSelected.Item(1, u).Value & "','" & flxSelected.Item(1, u).Value & "')")

                                    AdoCN.Execute("INSERT INTO tblExpSizingIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                                  "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & UCase(strPktNo) & "',1,'" & PBUser_EmpNo & "','" & CInt(flxSelected.Item(2, u).Value) & "'," & _
                                                             "'" & CDbl(flxSelected.Item(3, u).Value) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")

                                    AdoCN.Execute("INSERT INTO tblExpSizingReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts," & _
                                                                "LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                                                          "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & UCase(strPktNo) & "',1,'" & PBUser_EmpNo & "'" & _
                                                                "," & CInt(flxSelected.Item(2, u).Value) & "," & CDbl(flxSelected.Item(3, u).Value) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0)")

                                    AdoCN.Execute("DELETE FROM tblExpSizingTypes WHERE Department = '" & cmbDepartment.Text & "' AND ParNo = '" & flxSelected.Item(0, u).Value & "' AND PktNo = '" & strPktNo & "' AND Sec = 1")

                                    strOrderNo = ""
                                    dblPrice = 0
                                    rsComSql_2 = New ADODB.Recordset
                                    rsComSql_2.Open("SELECT * FROM tblExtPacket WHERE Department = '" & cmbDepartment.Text & "' AND ParNo = '" & flxSelected.Item(0, u).Value & "' AND PktNo = '" & flxSelected.Item(1, u).Value & "'", AdoCN, 1, 1)
                                    If rsComSql_2.RecordCount Then
                                        strOrderNo = rsComSql_2.Fields("Sieve").Value
                                        dblPrice = Math.Round(rsComSql_2.Fields("PlanVal").Value / rsComSql_2.Fields("PktCts").Value, 2)
                                    End If
                                    rsComSql_2 = Nothing

                                    strAssortment = ""
                                    rsComSql_2 = New ADODB.Recordset
                                    rsComSql_2.Open("SELECT Assortment FROM tblNoneOrders WHERE OrderNo = '" & strOrderNo & "'", AdoCN, 1, 1)
                                    If rsComSql_2.RecordCount Then
                                        strAssortment = "R" & rsComSql_2.Fields("Assortment").Value
                                    End If
                                    rsComSql_2 = Nothing

                                    AdoCN.Execute("INSERT INTO tblExpSizingTypes(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts,OK,BasePrice,PlanAssort,PlanBasePrice,EstCts) " & _
                                                  "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & strPktNo & "',1,'" & strAssortment & "'," & _
                                                    "" & CInt(flxSelected.Item(2, u).Value) & "," & CDbl(flxSelected.Item(3, u).Value) & ",0," & dblPrice & ",'" & strOrderNo & "',0," & CDbl(flxSelected.Item(3, u).Value) & ")")

                                End If
                                rsComSql_1 = Nothing
                            End If
                        End If
                        rsComSql = Nothing
                    End If

                    If cmbDepartment.Text = "Baguettes" And strRight(flxSelected.Item(0, u).Value, 1) = "S" Then
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT * FROM tblGrading_BoilingIssues WHERE Department = '" & cmbDepartment.Text & "' AND ParNo = '" & flxSelected.Item(0, u).Value & "' AND PktNo = '" & flxSelected.Item(1, u).Value & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount = 0 Then
                            'Boiling Issues
                            AdoCN.Execute("INSERT INTO tblGrading_BoilingIssues(Department,ParNo,PktNo,EmpNo,IssPcs,IssCts,IssDate,IssTime,UserName,Remarks) " & _
                                          "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "','D08411','" & CInt(flxSelected.Item(2, u).Value) & "'," & _
                                                 "'" & CDbl(flxSelected.Item(3, u).Value) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "','')")

                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT * FROM tblGrading_Parcel WHERE ParNo = '" & Mid(flxSelected.Item(0, u).Value, 1, 6) & "' AND Dept = 'Grading'", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount = 0 Then
                                AdoCN.Execute("INSERT INTO tblGrading_Parcel(ParNo,Complete,Dept) VALUES('" & Mid(flxSelected.Item(0, u).Value, 1, 6) & "',0,'Grading')")
                            End If
                            rsComSql_1 = Nothing

                            If chkSorting.Checked = True Then
                                'Boiling Returns
                                AdoCN.Execute("INSERT INTO tblGrading_BoilingReturns(Department,ParNo,PktNo,EmpNo,RetPcs,RetCts, " & _
                                                  "LostPcs,LostCts,RejPcs,RejCts,RetDate,RetTime,UserName,Trf) " & _
                                              "VALUES ('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "','D08411'," & CInt(flxSelected.Item(2, u).Value) & "," & CDbl(flxSelected.Item(3, u).Value) & ",0,0,0" & _
                                                  ",0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "',1)")

                                'Exp Packet
                                rsComSql_1 = New ADODB.Recordset
                                rsComSql_1.Open("SELECT * FROM tblExpPacket WHERE Department = '" & cmbDepartment.Text & "' AND ParNo = '" & flxSelected.Item(0, u).Value & "' AND PktNo = '" & flxSelected.Item(1, u).Value & "'", AdoCN, 1, 1)
                                If rsComSql_1.RecordCount = 0 Then
                                    AdoCN.Execute("INSERT INTO tblExpPacket(Department,ParNo,PktNo,PktPcs,PktCts,PktType,PktDate,AParNo,AMS2,YAH) " & _
                                                  "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "'," & CInt(flxSelected.Item(2, u).Value) & "," & CDbl(flxSelected.Item(3, u).Value) & ",'','" & Format(Date.Now, "MM/dd/yyyy") & "','" & flxSelected.Item(0, u).Value & "',1,1)")
                                End If
                                rsComSql_1 = Nothing
                            End If
                        End If
                        rsComSql = Nothing
                    End If
                Next

                If cmbDepartment.Text = "Mix" Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT MAX(PktNo) AS MaxPkt FROM tblGrading_BoilingIssues WHERE Department = '" & cmbDepartment.Text & "' AND ParNo = '" & txtSearch.Text & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount Then
                        If Not IsDBNull(rsComSql.Fields("MaxPkt").Value) Then
                            strPktNo = Format(rsComSql.Fields("MaxPkt").Value + 1, "000")
                        Else
                            strPktNo = "001"
                        End If
                    Else
                        strPktNo = "001"
                    End If
                    rsComSql = Nothing

                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblGrading_BoilingIssues WHERE Department = '" & cmbDepartment.Text & "' AND ParNo = '" & txtSearch.Text & "' AND PktNo = '" & strPktNo & "'", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        'Boiling Issues
                        AdoCN.Execute("INSERT INTO tblGrading_BoilingIssues(Department,ParNo,PktNo,EmpNo,IssPcs,IssCts,IssDate,IssTime,UserName,Remarks) " & _
                                      "VALUES('" & cmbDepartment.Text & "','" & txtSearch.Text & "','" & strPktNo & "','" & PBUser_EmpNo & "','" & CInt(txtPcs.Text) & "'," & _
                                             "'" & CDbl(txtCts.Text) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "','')")

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM tblGrading_Parcel WHERE ParNo = '" & Mid(txtSearch.Text, 1, 6) & "' AND Dept = 'Grading'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            AdoCN.Execute("INSERT INTO tblGrading_Parcel(ParNo,Complete,Dept) VALUES('" & Mid(txtSearch.Text, 1, 6) & "',0,'Grading')")
                        End If
                        rsComSql_1 = Nothing

                        'Boiling Returns
                        AdoCN.Execute("INSERT INTO tblGrading_BoilingReturns(Department,ParNo,PktNo,EmpNo,RetPcs,RetCts, " & _
                                            "LostPcs,LostCts,RejPcs,RejCts,RetDate,RetTime,UserName,Trf) " & _
                                      "VALUES ('" & cmbDepartment.Text & "','" & txtSearch.Text & "','" & strPktNo & "','" & PBUser_EmpNo & "'," & CInt(txtPcs.Text) & "," & CDbl(txtCts.Text) & ",0,0,0" & _
                                            ",0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "',0)")

                        'Checking Issues
                        AdoCN.Execute("INSERT INTO tblGrading_CheckingIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime,UserName) " & _
                                      "VALUES('" & cmbDepartment.Text & "','" & txtSearch.Text & "','" & strPktNo & "',2,'" & PBUser_EmpNo & "','" & CInt(txtPcs.Text) & "'," & _
                                             "'" & CDbl(txtCts.Text) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "')")

                        'Checking Returns
                        AdoCN.Execute("INSERT INTO tblGrading_CheckingReturns(Department,ParNo,PktNo,Sec,EmpNo,ExPcs,ExCts,VgPcs,VgCts," & _
                                            "BlPcs,BlCts,PsPcs,PsCts,ScPcs,ScCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts,UserName,SzPcs,SzCts,OkPcs,OkCts) " & _
                                      "VALUES ('" & cmbDepartment.Text & "','" & txtSearch.Text & "','" & strPktNo & "',2,'" & PBUser_EmpNo & "'," & CInt(txtPcs.Text) & "," & CDbl(txtCts.Text) & ",0,0,0,0,0,0,0,0,0,0," & _
                                            "0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,'" & PBUser_EmpNo & "',0,0,0,0)")

                        'Color Issues
                        AdoCN.Execute("INSERT INTO tblGrading_Issues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                      "VALUES('" & cmbDepartment.Text & "','" & txtSearch.Text & "','" & strPktNo & "',1,'" & PBUser_EmpNo & "','" & CInt(txtPcs.Text) & "'," & _
                                             "'" & CDbl(txtCts.Text) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")

                        'Color Returns
                        AdoCN.Execute("INSERT INTO tblGrading_Returns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                                      "VALUES ('" & cmbDepartment.Text & "','" & txtSearch.Text & "','" & strPktNo & "',1,'" & PBUser_EmpNo & "'," & CInt(txtPcs.Text) & "," & CDbl(txtCts.Text) & ",0,0," & _
                                            "0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0)")

                        'Color Details
                        AdoCN.Execute("INSERT INTO tblGrading_ReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                                      "VALUES('" & cmbDepartment.Text & "','" & txtSearch.Text & "','" & strPktNo & "',1,'COLLECTION'," & CInt(txtPcs.Text) & "," & CDbl(txtCts.Text) & ")")

                        'Sizing Packet
                        AdoCN.Execute("INSERT INTO tblGrading_SizingPacket(Department, ParNo, PktNo, PktPcs, PktCts, ReturnType, PktDate) " & _
                                      "VALUES('" & cmbDepartment.Text & "','" & txtSearch.Text & "','" & strPktNo & "'," & CInt(txtPcs.Text) & "," & CDbl(txtCts.Text) & ",'COLLECTION','" & Format(Date.Now, "MM/dd/yyyy") & "')")

                    End If
                    rsComSql = Nothing
                End If

                If cmbDepartment.Text = "GradingMix" Then
                    rsComSql = New ADODB.Recordset
                    rsComSql.Open("SELECT * FROM tblGrading_BoilingIssues WHERE Department = '" & cmbDepartment.Text & "' AND ParNo = '" & txtSearch.Text & "' AND PktNo = '001'", AdoCN, 1, 1)
                    If rsComSql.RecordCount = 0 Then
                        'Boiling Issues
                        AdoCN.Execute("INSERT INTO tblGrading_BoilingIssues(Department,ParNo,PktNo,EmpNo,IssPcs,IssCts,IssDate,IssTime,UserName,Remarks) " & _
                                      "VALUES('" & cmbDepartment.Text & "','" & txtSearch.Text & "','001','" & PBUser_EmpNo & "','" & CInt(txtPcs.Text) & "'," & _
                                             "'" & CDbl(txtCts.Text) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "','')")

                        rsComSql_1 = New ADODB.Recordset
                        rsComSql_1.Open("SELECT * FROM tblGrading_Parcel WHERE ParNo = '" & txtSearch.Text & "' AND Dept = 'Grading'", AdoCN, 1, 1)
                        If rsComSql_1.RecordCount = 0 Then
                            AdoCN.Execute("INSERT INTO tblGrading_Parcel(ParNo,Complete,Dept) VALUES('" & txtSearch.Text & "',0,'Grading')")
                        End If
                        rsComSql_1 = Nothing

                        'Boiling Returns
                        AdoCN.Execute("INSERT INTO tblGrading_BoilingReturns(Department,ParNo,PktNo,EmpNo,RetPcs,RetCts, " & _
                                        "LostPcs,LostCts,RejPcs,RejCts,RetDate,RetTime,UserName,Trf) " & _
                                  "VALUES ('" & cmbDepartment.Text & "','" & txtSearch.Text & "','001','" & PBUser_EmpNo & "'," & CInt(txtPcs.Text) & "," & CDbl(txtCts.Text) & ",0,0,0" & _
                                        ",0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "',0)")

                        'Checking Issues
                        AdoCN.Execute("INSERT INTO tblGrading_CheckingIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime,UserName) " & _
                                  "VALUES('" & cmbDepartment.Text & "','" & txtSearch.Text & "','001',2,'" & PBUser_EmpNo & "','" & CInt(txtPcs.Text) & "'," & _
                                         "'" & CDbl(txtCts.Text) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "')")

                    End If
                    rsComSql = Nothing
                End If

                If cmbDepartment.Text = "GradingPCU" Or cmbDepartment.Text = "GradingPCU_N" Then
                    For u = 0 To flxSelected.Rows.Count - 1
                        rsComSql = New ADODB.Recordset
                        rsComSql.Open("SELECT * FROM tblGrading_BoilingIssues WHERE Department = '" & cmbDepartment.Text & "' AND ParNo = '" & flxSelected.Item(0, u).Value & "' AND PktNo = '" & flxSelected.Item(1, u).Value & "'", AdoCN, 1, 1)
                        If rsComSql.RecordCount = 0 Then
                            'Boiling Issues
                            AdoCN.Execute("INSERT INTO tblGrading_BoilingIssues(Department,ParNo,PktNo,EmpNo,IssPcs,IssCts,IssDate,IssTime,UserName,Remarks,Grp) " & _
                                          "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "','" & PBUser_EmpNo & "','" & CInt(flxSelected.Item(2, u).Value) & "'," & _
                                                 "'" & CDbl(flxSelected.Item(3, u).Value) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "','','" & flxSelected.Item(6, u).Value & "')")

                            rsComSql_1 = New ADODB.Recordset
                            rsComSql_1.Open("SELECT * FROM tblGrading_Parcel WHERE ParNo = '" & flxSelected.Item(0, u).Value & "' AND Dept = 'Grading'", AdoCN, 1, 1)
                            If rsComSql_1.RecordCount = 0 Then
                                AdoCN.Execute("INSERT INTO tblGrading_Parcel(ParNo,Complete,Dept) VALUES('" & flxSelected.Item(0, u).Value & "',0,'Grading')")
                            End If
                            rsComSql_1 = Nothing

                            If chkSize.Checked = True Then
                                'Boiling Returns
                                AdoCN.Execute("INSERT INTO tblGrading_BoilingReturns(Department,ParNo,PktNo,EmpNo,RetPcs,RetCts, " & _
                                                "LostPcs,LostCts,RejPcs,RejCts,RetDate,RetTime,UserName,Trf,Grp) " & _
                                              "VALUES ('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "','" & PBUser_EmpNo & "'," & CInt(flxSelected.Item(2, u).Value) & "," & CDbl(flxSelected.Item(3, u).Value) & ",0,0,0" & _
                                                ",0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "',0,'" & flxSelected.Item(6, u).Value & "')")

                                'Checking Issues
                                AdoCN.Execute("INSERT INTO tblGrading_CheckingIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime,UserName,Grp) " & _
                                              "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "',2,'" & PBUser_EmpNo & "','" & CInt(flxSelected.Item(2, u).Value) & "'," & _
                                                "'" & CDbl(flxSelected.Item(3, u).Value) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & PBUser_EmpNo & "','" & flxSelected.Item(6, u).Value & "')")

                                'Checking Returns
                                AdoCN.Execute("INSERT INTO tblGrading_CheckingReturns(Department,ParNo,PktNo,Sec,EmpNo,ExPcs,ExCts,VgPcs,VgCts," & _
                                                "BlPcs,BlCts,PsPcs,PsCts,ScPcs,ScCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts,UserName,SzPcs,SzCts,OkPcs,OkCts,Grp) " & _
                                              "VALUES ('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "',2,'" & PBUser_EmpNo & "'," & CInt(flxSelected.Item(2, u).Value) & "," & CDbl(flxSelected.Item(3, u).Value) & ",0,0,0,0,0,0,0,0,0,0," & _
                                                "0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,'" & PBUser_EmpNo & "',0,0,0,0,'" & flxSelected.Item(6, u).Value & "')")

                                'Color Issues
                                AdoCN.Execute("INSERT INTO tblGrading_Issues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                              "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "',1,'" & PBUser_EmpNo & "','" & CInt(flxSelected.Item(2, u).Value) & "'," & _
                                                "'" & CDbl(flxSelected.Item(3, u).Value) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")

                                'Color Returns
                                AdoCN.Execute("INSERT INTO tblGrading_Returns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " & _
                                              "VALUES ('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "',1,'" & PBUser_EmpNo & "'," & CInt(flxSelected.Item(2, u).Value) & "," & CDbl(flxSelected.Item(3, u).Value) & ",0,0," & _
                                                "0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0)")

                                'Color Details
                                AdoCN.Execute("INSERT INTO tblGrading_ReturnDetails(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts) " & _
                                              "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & flxSelected.Item(1, u).Value & "',1,'COLLECTION'," & CInt(flxSelected.Item(2, u).Value) & "," & CDbl(flxSelected.Item(3, u).Value) & ")")


                                rsComSql_2 = New ADODB.Recordset
                                rsComSql_2.Open("SELECT * FROM tblGradingTrf WHERE Department = '" & cmbDepartment.Text & "' AND ParcelNo = '" & flxSelected.Item(0, u).Value & "' AND PktNo = '" & flxSelected.Item(1, u).Value & "'", AdoCN, 1, 1)
                                If rsComSql_2.RecordCount Then

                                    strPktNo = "Q001"
                                    rsComSql_1 = New ADODB.Recordset
                                    rsComSql_1.Open("SELECT MAX(RIGHT(PktNo, 3)) AS MaxPktNo FROM tblGrading_SizingPacket WHERE ParNo = '" & flxSelected.Item(0, u).Value & "' AND Department = '" & cmbDepartment.Text & "' AND LEFT(PktNo, 1) = 'Q'", AdoCN, 1, 1)
                                    If rsComSql_1.RecordCount Then
                                        If Not IsDBNull(rsComSql_1.Fields("MaxPktNo").Value) Then
                                            strPktNo = "Q" & Format(rsComSql_1.Fields("MaxPktNo").Value + 1, "000")
                                        Else
                                            strPktNo = "Q001"
                                        End If
                                    End If
                                    rsComSql_1 = Nothing

                                    'Sizing Packet
                                    AdoCN.Execute("INSERT INTO tblGrading_SizingPacket(Department, ParNo, PktNo, PktPcs, PktCts, ReturnType, PktDate, OrderNo, RefNo, Side, RateCode) " & _
                                                  "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & strPktNo & "'," & CInt(flxSelected.Item(2, u).Value) & "," & CDbl(flxSelected.Item(3, u).Value) & ",'COLLECTION'," & _
                                                    "'" & Format(Date.Now, "MM/dd/yyyy") & "','" & rsComSql_2.Fields("OrderNo").Value & "','" & rsComSql_2.Fields("RefNo").Value & "','" & rsComSql_2.Fields("Side").Value & "','" & rsComSql_2.Fields("RateCode").Value & "')")

                                    'Sizing Issues
                                    AdoCN.Execute("INSERT INTO tblGrading_SizingIssues(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " & _
                                                  "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & strPktNo & "',1,'" & PBUser_EmpNo & "','" & CInt(flxSelected.Item(2, u).Value) & "'," & _
                                                    "'" & CDbl(flxSelected.Item(3, u).Value) & "','" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "')")

                                    If chkFinish.Checked = True Then
                                        'Sizing Returns
                                        AdoCN.Execute("INSERT INTO tblGrading_SizingReturns(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts," & _
                                                          "LostPcs,LostCts,RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts,RghPcs,RghCts) " & _
                                                      "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & strPktNo & "',1,'" & PBUser_EmpNo & "'" & _
                                                          "," & CInt(flxSelected.Item(2, u).Value) & "," & CDbl(flxSelected.Item(3, u).Value) & ",0,0,0,0,'" & Format(Date.Now, "MM/dd/yyyy") & "','" & Format(Date.Now, "HH:mm:ss") & "',0,0,0,0)")

                                        'Sizing Types
                                        AdoCN.Execute("INSERT INTO tblGrading_SizingTypes(Department,ParNo,PktNo,Sec,ReturnType,Pcs,Cts,OK,Color,Clarity) " & _
                                                      "VALUES('" & cmbDepartment.Text & "','" & flxSelected.Item(0, u).Value & "','" & strPktNo & "',1," & _
                                                        "'" & rsComSql_2.Fields("Assort1").Value & "'," & CInt(flxSelected.Item(2, u).Value) & "," & CDbl(flxSelected.Item(3, u).Value) & ",0,'','')")

                                        blnFound = False
                                        rsComSql_3 = New ADODB.Recordset
                                        rsComSql_3.Open("SELECT * FROM tblGrading_SizingList WHERE NAME = '" & rsComSql_2.Fields("Assort1").Value & "'", AdoCN, 1, 1)
                                        If rsComSql_3.RecordCount Then
                                            blnFound = True
                                        End If
                                        rsComSql_3 = Nothing

                                        If blnFound = False Then
                                            dblPrice = 0
                                            rsComSql_3 = New ADODB.Recordset
                                            rsComSql_3.Open("SELECT * FROM tblAssortList WHERE Assortment = '" & rsComSql_2.Fields("Assort1").Value & "'", AdoCN, 1, 1)
                                            If rsComSql_3.RecordCount Then
                                                dblPrice = rsComSql_3.Fields("MarketPrice").Value
                                            End If
                                            rsComSql_3 = Nothing

                                            If dblPrice = 0 Then
                                                rsComSql_3 = New ADODB.Recordset
                                                rsComSql_3.Open("SELECT * FROM tblDCLPermanents WHERE ItemName = '" & rsComSql_2.Fields("Assort1").Value & "'", AdoCN, 1, 1)
                                                If rsComSql_3.RecordCount Then
                                                    dblPrice = rsComSql_3.Fields("ListCost").Value
                                                End If
                                                rsComSql_3 = Nothing
                                            End If

                                            If dblPrice = 0 Then
                                                rsComSql_3 = New ADODB.Recordset
                                                rsComSql_3.Open("SELECT * FROM tblDep_Trf WHERE DCLParcelNo = '" & flxSelected.Item(0, u).Value & "'", AdoCN, 1, 1)
                                                If rsComSql_3.RecordCount Then
                                                    dblPrice = rsComSql_3.Fields("ItemCost").Value
                                                End If
                                                rsComSql_3 = Nothing
                                            End If

                                            rsComSql_3 = New ADODB.Recordset
                                            rsComSql_3.Open("SELECT * FROM tblGrading_SizingList WHERE NAME = '" & rsComSql_2.Fields("Assort1").Value & "'", AdoCN, 1, 1)
                                            If rsComSql_3.RecordCount = 0 Then
                                                AdoCN.Execute("INSERT INTO tblGrading_SizingList(NAME,OLDNAME,PRICE,COLOR,CLARITY,CUT,SHAPE,MODEL,LFROM,LTO,WFROM,WTO,TYPE) " & _
                                                              "VALUES('" & rsComSql_2.Fields("Assort1").Value & "',''," & dblPrice & ",'','','','','','','','','',1)")
                                            End If
                                            rsComSql_3 = Nothing
                                        End If
                                    End If
                                End If
                                rsComSql_2 = Nothing
                            End If
                        End If
                        rsComSql = Nothing
                    Next
                End If

                MsgBox("Data Added Successfully", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Grading Transfer Acceptation")
                flxSelected.Rows.Clear()
                flxParcel.Rows.Clear()
                flxAvailable.Rows.Clear()
                txtPcs.Text = ""
                txtCts.Text = ""
                txtActPcs.Text = "0"
                txtActCts.Text = "0"
                txtSearch.Text = ""
                Load_Parcels()
            End If
        Else
            MsgBox("Invalid Values", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
    End Sub

    Private Sub txtActPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtActPcs.KeyPress
        e.Handled = IntegerOnly(Asc(e.KeyChar))
    End Sub

    Private Sub txtActCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtActCts.KeyPress
        e.Handled = NumericOnly(Asc(e.KeyChar), txtActCts.Text)
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        SaveAccept()
    End Sub

    Private Sub chkSizeFinish_CheckedChanged(sender As Object, e As EventArgs) Handles chkSizeFinish.CheckedChanged
        If chkSizeFinish.Checked = True Then
            chkBoiling.Checked = False
            chkChecking.Checked = False
            chkSorting.Checked = False
            chkSize.Checked = False
            chkFinish.Checked = False
            chkSize1.Checked = False
        End If
    End Sub
End Class