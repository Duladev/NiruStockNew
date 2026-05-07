Public Class frm_GRDRnd_AssortComp

    ' FORM LOAD
    Private Sub frm_Grading_AssortComp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.StartPosition = FormStartPosition.CenterScreen
            Load_Assortments()
        Catch ex As Exception
            MsgBox("Error in Form_Load : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    Public Sub NumericOnly(ByVal e As KeyPressEventArgs, ByVal CurrentText As String)
        If Not (Char.IsDigit(e.KeyChar) OrElse Asc(e.KeyChar) = 8 OrElse Asc(e.KeyChar) = 46) Then
            e.Handled = True
        Else
            If e.KeyChar = "." AndAlso CurrentText.Contains(".") Then
                e.Handled = True
            End If
        End If
    End Sub

    ' LOAD ASSORTMENTS
    Private Sub Load_Assortments()
        Dim rsAssort As New ADODB.Recordset
        Try
            cmbAssort1.Items.Clear()
            cmbAssort2.Items.Clear()

            Dim sql As String = "SELECT AssortNo FROM tblGrading_RndSizeListNew ORDER BY AssortNo"
            rsAssort.Open(sql, AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

            Do While Not rsAssort.EOF
                Dim assort As String = rsAssort.Fields("AssortNo").Value.ToString().Trim()
                cmbAssort1.Items.Add(assort)
                cmbAssort2.Items.Add(assort)
                rsAssort.MoveNext()
            Loop

        Catch ex As Exception
            MsgBox("Error in Load_Assortments : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rsAssort.State = ADODB.ObjectStateEnum.adStateOpen Then rsAssort.Close()
            rsAssort = Nothing
        End Try
    End Sub


    Private Sub txtCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCts.KeyPress
        NumericOnly(e, txtCts.Text)
    End Sub

    ' CALCULATE BUTTON

    Private Sub cmdCalc_Click(sender As Object, e As EventArgs) Handles cmdCalc.Click
        Dim rsVal As New ADODB.Recordset
        Try
            txtValue1.Text = "0.00"
            txtValue2.Text = "0.00"

            If txtCts.Text.Trim() = "" OrElse
               cmbAssort1.Text.Trim() = "" OrElse
               cmbAssort2.Text.Trim() = "" Then
                MessageBox.Show("Invalid Cts or Assortment", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim cts As Double = Convert.ToDouble(txtCts.Text.Trim())

            ' --- Value 1 ---
            Dim sql1 As String = "SELECT Price FROM tblGrading_RndSizeListNew " &
                                 "WHERE AssortNo='" & cmbAssort1.Text.Trim() & "'"
            rsVal.Open(sql1, AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

            If Not rsVal.EOF AndAlso Not IsDBNull(rsVal.Fields("Price").Value) Then
                txtValue1.Text = Format(cts * Convert.ToDouble(rsVal.Fields("Price").Value), "#0.00")
            End If

            If rsVal.State = ADODB.ObjectStateEnum.adStateOpen Then rsVal.Close()

            ' --- Value 2 ---
            Dim sql2 As String = "SELECT Price FROM tblGrading_RndSizeListNew " &
                                 "WHERE AssortNo='" & cmbAssort2.Text.Trim() & "'"
            rsVal.Open(sql2, AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

            If Not rsVal.EOF AndAlso Not IsDBNull(rsVal.Fields("Price").Value) Then
                txtValue2.Text = Format(cts * Convert.ToDouble(rsVal.Fields("Price").Value), "#0.00")
            End If

        Catch ex As Exception
            MsgBox("Error in cmdCalc_Click : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rsVal.State = ADODB.ObjectStateEnum.adStateOpen Then rsVal.Close()
            rsVal = Nothing
        End Try
    End Sub

    ' EXIT
    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub


    Private Sub cmbAssort1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAssort1.SelectedIndexChanged
    End Sub

    Private Sub cmbAssort2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAssort2.SelectedIndexChanged
    End Sub

    Private Sub txtValue1_TextChanged(sender As Object, e As EventArgs) Handles txtValue1.TextChanged
    End Sub

End Class