
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing.Imaging

Public Class frm_MixOrderImage

    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub ClearFields()
        cmbSubject.Text = ""
        cmbRef.Text = ""
        cmbRef.Items.Clear()
        cmbSide.Text = ""
        cmbSide.Items.Clear()
        picBox.Image = Nothing
        flxDetails.Rows.Clear()
    End Sub

    Private Sub Load_Subject()
        cmbSubject.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT Subject AS SubjectFull FROM dbo.tblOrders WHERE (Complete = N'N') GROUP BY Subject ORDER BY SubjectFull", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbSubject.Items.Add(rsComSql.Fields("SubjectFull").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub Load_DetailsAll()
        flxDetails.Rows.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT * FROM dbo.tblOrdersImage ORDER BY Subject, RefNo, Side", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                flxDetails.Rows.Add(rsComSql.Fields("Subject").Value,
                                    rsComSql.Fields("RefNo").Value,
                                    rsComSql.Fields("Side").Value,
                                    rsComSql.Fields("Pic").Value)
                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearFields()
        Load_DetailsAll()
    End Sub

    Private Sub cmdExcel_Click(sender As Object, e As EventArgs) Handles cmdExcel.Click
        ExportToExcel(flxDetails)
    End Sub

    Private Sub Save()
        If cmbSubject.Text = "" Then MsgBox("Invalid Subject", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbRef.Text = "" Then MsgBox("Invalid Reference", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        PBResponse = MsgBox("Are you sure?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM dbo.tblOrders WHERE (Subject = '" & cmbSubject.Text & "') AND (Complete = N'N')", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                MsgBox("Invalid Order", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
                Exit Sub
            End If
            rsComSql = Nothing

            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM dbo.tblOrdersImage WHERE (Subject = '" & cmbSubject.Text & "') AND (RefNo = '" & Replace(cmbRef.Text, "'", "''") & "') AND (Side = '" & cmbSide.Text & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount = 0 Then
                AdoCN.Execute("INSERT INTO tblOrdersImage(Subject, RefNo, Side) VALUES('" & cmbSubject.Text & "','" & Replace(cmbRef.Text, "'", "''") & "','" & cmbSide.Text & "')")
            End If
            rsComSql = Nothing

            If Not picBox.Image Is Nothing Then
                Save_Photo(cmbSubject.Text, cmbRef.Text, cmbSide.Text, picBox.Image)

                MsgBox("Successfully Saved", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            End If
        End If
    End Sub

    Private Sub Delete()
        If cmbSubject.Text = "" Then MsgBox("Invalid Subject", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbRef.Text = "" Then MsgBox("Invalid Reference", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub
        If cmbSide.Text = "" Then MsgBox("Invalid Side", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text) : Exit Sub

        PBResponse = MsgBox("Are you sure to Delete?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, Me.Text)
        If PBResponse = MsgBoxResult.Yes Then
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT * FROM dbo.tblOrdersImage WHERE (Subject = '" & cmbSubject.Text & "') AND (RefNo = '" & Replace(cmbRef.Text, "'", "''") & "') AND (Side = '" & cmbSide.Text & "')", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                AdoCN.Execute("DELETE FROM dbo.tblOrdersImage WHERE (Subject = '" & cmbSubject.Text & "') AND (RefNo = '" & Replace(cmbRef.Text, "'", "''") & "') AND (Side = '" & cmbSide.Text & "')")
            End If
            rsComSql = Nothing

            MsgBox("Successfully Deleted", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
            cmbSubject.Text = ""
            cmbRef.Text = ""
            cmbRef.Items.Clear()
            cmbSide.Text = ""
            cmbSide.Items.Clear()
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Save()
    End Sub

    Private Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Delete()
    End Sub

    Private Sub flxDetails_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles flxDetails.CellClick
        cmbSubject.Text = flxDetails.Item(0, flxDetails.CurrentRow.Index).Value
        cmbRef.Text = flxDetails.Item(1, flxDetails.CurrentRow.Index).Value
        cmbSide.Text = flxDetails.Item(2, flxDetails.CurrentRow.Index).Value

        If cmbSubject.Text <> "" And cmbRef.Text <> "" Then
            Show_Photo_DB(cmbSubject.Text, cmbRef.Text, cmbSide.Text)
        End If
    End Sub

    Private Sub cmdOpen_Click(sender As Object, e As EventArgs) Handles cmdOpen.Click
        OpenFileDialog1.Filter = "Image Files|*.jpg;*.bmp;*.png"
        OpenFileDialog1.InitialDirectory = "C:\"
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName <> "" Then
            picBox.Image = Image.FromFile(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Function Save_Photo(strSubject As String, strRefNo As String, strSide As String, Pic As Image) As Integer
        Dim Result As Integer = 0
        Dim SQL As String = "UPDATE tblOrdersImage SET Pic = @photo WHERE Subject = '" & strSubject & "' AND RefNo = '" & Replace(strRefNo, "'", "''") & "' AND Side = '" & strSide & "'"

        Using SQLConn As New SqlClient.SqlConnection("Data Source=" & strServerName & ";Integrated Security=SSPI;database=" & strDBName & "")
            Try
                SQLConn.Open()
                Using SQLCmd As New SqlClient.SqlCommand(SQL, SQLConn)
                    Dim PhotoParameter As New SqlClient.SqlParameter("@photo", SqlDbType.Image)
                    Dim MS As New IO.MemoryStream()

                    If Pic IsNot Nothing Then
                        Pic.Save(MS, Imaging.ImageFormat.Bmp)
                    End If
                    PhotoParameter.SqlValue = MS.GetBuffer
                    SQLCmd.Parameters.Add(PhotoParameter)

                    Result = SQLCmd.ExecuteNonQuery()
                End Using

            Catch ex As Exception
                Dim Msg As String = "Unable to save Order Drawing"

                If ex IsNot Nothing Then
                    Msg &= ":" & ControlChars.NewLine & ControlChars.NewLine & ex.ToString
                End If
                MessageBox.Show(Msg, My.Application.Info.AssemblyName, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Finally
                If Not SQLConn.State = ConnectionState.Closed Then
                    SQLConn.Close()
                End If
            End Try
        End Using

        Return Result
    End Function

    Private Sub Show_Photo_DB(ByVal strSubject As String, ByVal strRefNo As String, ByVal strSide As String)
        Dim cn As New SqlConnection()
        cn.ConnectionString = "Data Source='" & strServerName & "';Connect Timeout=60;Initial Catalog='" & strDBName & "';Integrated Security=SSPI"
        cn.Open()
        Dim cmd As New SqlCommand("SELECT Pic FROM tblOrdersImage WHERE Subject = '" & strSubject & "' AND RefNo = '" & Replace(strRefNo, "'", "''") & "' AND Side = '" & strSide & "'", cn)
        Dim dr As SqlDataReader = cmd.ExecuteReader()
        If dr.HasRows Then
            dr.Read()
            If Not IsDBNull(dr("Pic")) Then
                Dim data As Byte() = DirectCast(dr("Pic"), Byte())
                Dim ms As New MemoryStream(data)
                picBox.Image = Image.FromStream(ms)
                data = Nothing
                ms = Nothing
                dr = Nothing
            End If
        End If
        cn.Close()
    End Sub

    Private Sub frm_MixOrderImage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GetUserRights(Me.Name) = False Then
            MsgBox("Access Denied", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "No Access")
            Me.Close()
            Exit Sub
        End If

        'Load_Subject()
        ClearFields()
        Load_DetailsAll()
    End Sub

    Private Sub cmdShow_Click(sender As Object, e As EventArgs) Handles cmdShow.Click
        If cmbSubject.Text <> "" And cmbRef.Text <> "" Then
            Show_Photo_DB(cmbSubject.Text, cmbRef.Text, cmbSide.Text)
        End If
    End Sub

    Private Sub cmbSubject_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbSubject.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cmbRef.Text = ""
            cmbRef.Items.Clear()
            cmbSide.Text = ""
            cmbSide.Items.Clear()
            rsComSql = New ADODB.Recordset
            rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblOrdersDtls.RefNo " & _
                          "FROM dbo.tblOrders INNER JOIN dbo.tblOrdersDtls ON dbo.tblOrders.OrderNo = dbo.tblOrdersDtls.OrderNo " & _
                          "WHERE (dbo.tblOrders.Complete = N'N') AND (dbo.tblOrders.Subject = '" & cmbSubject.Text & "') " & _
                          "GROUP BY dbo.tblOrdersDtls.RefNo " & _
                          "ORDER BY dbo.tblOrdersDtls.RefNo", AdoCN, 1, 1)
            If rsComSql.RecordCount Then
                rsComSql.MoveFirst()
                While Not rsComSql.EOF
                    cmbRef.Items.Add(rsComSql.Fields("RefNo").Value)

                    rsComSql.MoveNext()
                End While
            End If
            rsComSql = Nothing
        End If
    End Sub

    Private Sub cmbRef_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRef.SelectedIndexChanged
        cmbSide.Text = ""
        cmbSide.Items.Clear()
        rsComSql = New ADODB.Recordset
        rsComSql.Open("SELECT TOP (100) PERCENT dbo.tblOrdersDtls.Side " & _
                      "FROM dbo.tblOrders INNER JOIN dbo.tblOrdersDtls ON dbo.tblOrders.OrderNo = dbo.tblOrdersDtls.OrderNo " & _
                      "WHERE (dbo.tblOrders.Complete = N'N') AND (dbo.tblOrders.Subject = '" & cmbSubject.Text & "') AND (dbo.tblOrdersDtls.RefNo = '" & Replace(cmbRef.Text, "'", "''") & "') " & _
                      "GROUP BY dbo.tblOrdersDtls.Side " & _
                      "ORDER BY dbo.tblOrdersDtls.Side", AdoCN, 1, 1)
        If rsComSql.RecordCount Then
            rsComSql.MoveFirst()
            While Not rsComSql.EOF
                cmbSide.Items.Add(rsComSql.Fields("Side").Value)

                rsComSql.MoveNext()
            End While
        End If
        rsComSql = Nothing
    End Sub
End Class