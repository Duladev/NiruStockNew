Imports System.Data.SqlClient   ' DiaStock — via ADODB wrapper
Imports System.Data


Public Class frm_GRDRnd_Sizing

    ' -----------------------------------------------------------------------
    '  Module-level state variables
    ' -----------------------------------------------------------------------
    Private issued As Boolean
    Private Checked As Boolean
    Private frmnew As Boolean
    Private Section As Integer
    Private ICNo2 As String

    Private Const GRID_ASSORT As Integer = 0
    Private Const GRID_SIZE As Integer = 1
    Private Const GRID_PCS As Integer = 2
    Private Const GRID_CTS As Integer = 3
    Private Const GRID_CODE As Integer = 4

    Private Const EMP_NO As Integer = 0
    Private Const EMP_PCS As Integer = 1

#Region "Form Load / Initialise"

    Private Sub frm_Grading_Sizing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Top = (Screen.PrimaryScreen.WorkingArea.Height - Me.Height) \ 3
        Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) \ 2
        SetupGridColumns()
        SetupEmpGridColumns()
        Load_Section()
        LoadDepartments()
        frmInitialze()
    End Sub

    Private Sub SetupGridColumns()
        dgvGrid.Columns.Clear()
        Dim colDefs As (name As String, header As String, width As Integer, [readOnly] As Boolean)() = {
            ("Assortment", "Assortment", 150, True),
            ("SizeRange", "Size Range", 100, True),
            ("Pcs", "Pcs", 60, False),
            ("Cts", "Cts", 60, False),
            ("Code", "Code", 70, True)
        }
        For Each def In colDefs
            Dim col As New DataGridViewTextBoxColumn() With {
                .Name = def.name,
                .HeaderText = def.header,
                .Width = def.width,
                .ReadOnly = def.[readOnly]
            }
            dgvGrid.Columns.Add(col)
        Next
        StyleGridHeader(dgvGrid)
    End Sub

    Private Sub SetupEmpGridColumns()
        dgvEmp.Columns.Clear()
        dgvEmp.Columns.Add(New DataGridViewTextBoxColumn() With {
            .Name = "EmpNo", .HeaderText = "Emp No", .Width = 75, .ReadOnly = True
        })
        dgvEmp.Columns.Add(New DataGridViewTextBoxColumn() With {
            .Name = "Pcs", .HeaderText = "Pcs", .Width = 60,
            .DefaultCellStyle = New DataGridViewCellStyle() With {
                .Alignment = DataGridViewContentAlignment.MiddleRight
            }
        })
        StyleGridHeader(dgvEmp)
    End Sub

    Private Sub StyleGridHeader(dgv As DataGridView)
        With dgv.ColumnHeadersDefaultCellStyle
            .BackColor = System.Drawing.Color.FromArgb(50, 100, 160)
            .ForeColor = System.Drawing.Color.White
            .Font = New System.Drawing.Font("Tahoma", 8.0F, System.Drawing.FontStyle.Bold)
        End With
        dgv.EnableHeadersVisualStyles = False
        dgv.AlternatingRowsDefaultCellStyle.BackColor =
            System.Drawing.Color.FromArgb(235, 245, 255)
    End Sub

    Private Sub frmInitialze()
        txtParNo.Text = ""
        txtPktNo.Text = ""
        txtemp.Text = ""
        txtRetCts.Text = ""
        txtRetTap.Text = ""
        txtIssCts.Text = ""
        txtIssTap.Text = ""
        txtRej.Text = ""
        txtLostPcs.Text = ""
        frmnew = True
        txtIssDate.Text = ""
        txtIssTime.Text = ""
        txtRetTime.Text = ""
        txtRetDate.Text = ""
        txtRejCts.Text = ""
        txtLostCts.Text = ""
        txtRepPcs.Text = ""
        txtRepCts.Text = ""
        If cmbSection.Items.Count > 0 Then cmbSection.SelectedIndex = 0
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
        grpIssues.Visible = False
        grpReturns.Visible = False
        grpSizingTypes.Visible = False
        grpEmp.Visible = False
        txtType1.Text = ""
        txtPktType.Text = ""
        dgvGrid.Rows.Clear()
        txtEmpNo.Text = ""
        txtEmpPcs.Text = ""
        dgvEmp.Rows.Clear()
        txtAssortment.Text = ""
        txtTypePcs.Text = ""
        txtTypeCts.Text = ""
        cmbSize.Items.Clear()
        cmbSize.Text = ""
        issued = False
        Checked = False
        ICNo2 = ""
        Section = 0
    End Sub

#End Region

#Region "Load Reference Data"

    '  LOAD SECTION COMBO
    Private Sub Load_Section()
        Try
            cmbSection.Items.Clear()
            Dim rsComSql As New ADODB.Recordset()
            rsComSql.Open(
                "SELECT SecName FROM tblGrading_RndSections WHERE Seq > 5 ORDER BY Seq",
                AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                ADODB.LockTypeEnum.adLockOptimistic)

            Do While Not rsComSql.EOF
                cmbSection.Items.Add(rsComSql.Fields("SecName").Value.ToString())
                rsComSql.MoveNext()
            Loop
            rsComSql.Close()
            rsComSql = Nothing

            If cmbSection.Items.Count > 0 Then cmbSection.SelectedIndex = 0
        Catch ex As Exception
            MsgBox("Error in Load_Section : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub


    '  LOAD DEPARTMENTS
    Private Sub LoadDepartments()
        Try
            cmbDept.Items.Clear()
            Dim rsComSql As New ADODB.Recordset()
            rsComSql.Open(
                "SELECT Department FROM tblGrading_RndDepartment ORDER BY Department",
                AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                ADODB.LockTypeEnum.adLockOptimistic)

            Do While Not rsComSql.EOF
                cmbDept.Items.Add(rsComSql.Fields("Department").Value.ToString())
                rsComSql.MoveNext()
            Loop
            rsComSql.Close()
            rsComSql = Nothing

            If cmbDept.Items.Count > 0 Then cmbDept.SelectedIndex = 0
        Catch ex As Exception
            MsgBox("Error in LoadDepartments : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub


    '  LOAD GRADING TYPES GRID
    Private Sub Load_GradingTypes(ByVal strType1 As String, ByVal strType2 As String)
        Try
            Dim intType As Integer = If(chkNew.Checked, 1, 0)
            dgvGrid.Rows.Clear()

            Dim suffix As String = ""
            Select Case strType2
                Case "N"
                    suffix = "AND RIGHT(AssortNo, 2) <> '_B' AND RIGHT(AssortNo, 2) <> '_C' " &
                             "AND RIGHT(AssortNo, 2) <> '_M' "
                Case "B" : suffix = "AND RIGHT(AssortNo, 2) = '_B' "
                Case "C" : suffix = "AND RIGHT(AssortNo, 2) = '_C' "
                Case "M" : suffix = "AND RIGHT(AssortNo, 2) = '_M' "
                Case Else : Return
            End Select

            Dim sql As String =
                "SELECT * FROM tblGrading_RndSizeListRange " &
                "WHERE MainAssort='" & strType1 & "' " & suffix &
                "AND Type=" & intType & " ORDER BY Size"

            Dim rsComSql As New ADODB.Recordset()
            rsComSql.Open(sql, AdoCN,
                          ADODB.CursorTypeEnum.adOpenKeyset,
                          ADODB.LockTypeEnum.adLockOptimistic)

            Do While Not rsComSql.EOF
                dgvGrid.Rows.Add(
                    rsComSql.Fields("AssortNo").Value.ToString(),
                    rsComSql.Fields("Size").Value.ToString(),
                    "0", "0",
                    rsComSql.Fields("MainAssort").Value.ToString()
                )
                rsComSql.MoveNext()
            Loop
            rsComSql.Close()
            rsComSql = Nothing
        Catch ex As Exception
            MsgBox("Error in Load_GradingTypes : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

#End Region

#Region "Packet Loading"


    '  PARCEL FOUND CHECK
    Private Function ParcelFound(ByVal strDept As String, ByVal strParcelNo As String) As Boolean
        Try
            Dim rsComSql As New ADODB.Recordset()
            rsComSql.Open(
                "SELECT COUNT(*) AS CNT FROM tblGrading_RndInvoice WHERE ParcelNo='" & strParcelNo & "'",
                AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                ADODB.LockTypeEnum.adLockOptimistic)
            Dim found As Boolean = CInt(rsComSql.Fields("CNT").Value) > 0
            rsComSql.Close()
            rsComSql = Nothing
            Return found
        Catch ex As Exception
            MsgBox("Error in ParcelFound : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return False
        End Try
    End Function


    '  GET NEW PACKET (loads pcs/cts for a new issue)
    Private Sub GetNewPacket()
        Try
            Dim rsComSql As New ADODB.Recordset()
            rsComSql.Open(
                "SELECT * FROM tblGrading_RndSizingPacket " &
                "WHERE ParNo='" & txtParNo.Text & "' AND PktNo='" & txtPktNo.Text & "'",
                AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                ADODB.LockTypeEnum.adLockOptimistic)

            If Not rsComSql.EOF Then
                txtIssTap.Text = rsComSql.Fields("PktPcs").Value.ToString()
                txtIssCts.Text = CDbl(rsComSql.Fields("PktCts").Value).ToString("#0.000")
                grpIssues.Visible = True
                grpReturns.Visible = False
                issued = True
                Checked = False
            Else
                MessageBox.Show("Invalid Packet", Me.Text,
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                frmInitialze()
                txtParNo.Focus()
            End If
            rsComSql.Close()
            rsComSql = Nothing
        Catch ex As Exception
            MsgBox("Error in GetNewPacket : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub


    '  LOAD PARCEL DETAILS (issues + returns history)

    Private Sub Load_ParcelDetails()
        Try
            issued = True
            Checked = False
            Dim intIssPcsC As Double = 0

            ' --- Check issues table ---
            Dim rsIss As New ADODB.Recordset()
            rsIss.Open(
                "SELECT * FROM tblGrading_RndSizingIssues " &
                "WHERE ParNo='" & txtParNo.Text & "' AND PktNo='" & txtPktNo.Text & "' " &
                "AND Department='" & cmbDept.Text & "' ORDER BY ID DESC",
                AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                ADODB.LockTypeEnum.adLockOptimistic)

            If Not rsIss.EOF Then
                Checked = True
                intIssPcsC = CDbl(rsIss.Fields("IssPcs").Value)
                cmbSection.SelectedIndex = CInt(rsIss.Fields("Sec").Value) - 1
                Section = CInt(rsIss.Fields("Sec").Value)
            Else
                rsIss.Close()
                grpIssues.Visible = True
                txtIssDate.Text = Format(Date.Today, "dd/MM/yyyy")
                txtIssTime.Text = Format(Date.Now, "HH:mm")
                txtIssTap.Enabled = False
                txtIssCts.Enabled = False
                GetNewPacket()
                Return
            End If
            rsIss.Close()
            rsIss = Nothing

            ' --- Load packet type info ---
            Dim rsPkt As New ADODB.Recordset()
            rsPkt.Open(
                "SELECT * FROM tblGrading_RndSizingPacket " &
                "WHERE ParNo='" & txtParNo.Text & "' AND PktNo='" & txtPktNo.Text & "'",
                AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                ADODB.LockTypeEnum.adLockOptimistic)

            If Not rsPkt.EOF Then
                txtType1.Text = rsPkt.Fields("SizeCode").Value.ToString()
                txtPktType.Text = rsPkt.Fields("PktType").Value.ToString()
            End If
            rsPkt.Close()
            rsPkt = Nothing

            Load_GradingTypes(txtType1.Text, txtPktType.Text)
            txtRetTap.ReadOnly = (Section <> 7)
            txtRetCts.ReadOnly = (Section <> 7)

            ' --- Check last return ---
            Dim rsRet As New ADODB.Recordset()
            rsRet.Open(
                "SELECT * FROM tblGrading_RndSizingReturns " &
                "WHERE ParNo='" & txtParNo.Text & "' AND PktNo='" & txtPktNo.Text & "' " &
                "AND Department='" & cmbDept.Text & "' ORDER BY ID DESC",
                AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                ADODB.LockTypeEnum.adLockOptimistic)
            rsRet.Close()
            rsRet = Nothing

            ' --- Determine next action ---
            If issued AndAlso Checked Then
                Dim rsRetSec As New ADODB.Recordset()
                rsRetSec.Open(
                    "SELECT * FROM tblGrading_RndSizingReturns " &
                    "WHERE ParNo='" & txtParNo.Text & "' AND PktNo='" & txtPktNo.Text & "' " &
                    "AND Department='" & cmbDept.Text & "' AND Sec=" & Section,
                    AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                    ADODB.LockTypeEnum.adLockOptimistic)

                If Not rsRetSec.EOF Then
                    Dim retTotal As Double =
                        CDbl(rsRetSec.Fields("RetPcs").Value) +
                        CDbl(rsRetSec.Fields("RepPcs").Value) +
                        CDbl(rsRetSec.Fields("LostPcs").Value) +
                        CDbl(rsRetSec.Fields("RejPcs").Value)

                    If intIssPcsC = retTotal Then
                        If Section <> 1 Then
                            txtIssTap.Text = rsRetSec.Fields("RetPcs").Value.ToString()
                            txtIssCts.Text = rsRetSec.Fields("RetCts").Value.ToString()
                            txtIssTap.Enabled = False
                            txtIssCts.Enabled = False
                            txtIssDate.Text = Format(Date.Today, "dd/MM/yyyy")
                            txtIssTime.Text = Format(Date.Now, "HH:mm")
                            ICNo2 = ""
                            txtemp.Text = ""
                            grpIssues.Visible = True
                            grpReturns.Visible = False
                            grpSizingTypes.Visible = False
                            grpEmp.Visible = False
                            cmbSection.SelectedIndex = Section
                            Section = Section + 1
                            btnEmpNo.Focus()
                            Checked = False
                        Else
                            rsRetSec.Close()
                            MessageBox.Show("Packet Finished", Me.Text,
                                            MessageBoxButtons.OK, MessageBoxIcon.Information)
                            frmInitialze()
                            Return
                        End If
                    End If
                    rsRetSec.Close()
                Else
                    rsRetSec.Close()

                    ' Load existing issue record for this section
                    Dim rsIssSec As New ADODB.Recordset()
                    rsIssSec.Open(
                        "SELECT * FROM tblGrading_RndSizingIssues " &
                        "WHERE ParNo='" & txtParNo.Text & "' AND PktNo='" & txtPktNo.Text & "' " &
                        "AND Department='" & cmbDept.Text & "' AND Sec=" & Section,
                        AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                        ADODB.LockTypeEnum.adLockOptimistic)

                    If Not rsIssSec.EOF Then
                        txtIssTap.Text = rsIssSec.Fields("IssPcs").Value.ToString()
                        txtIssCts.Text = rsIssSec.Fields("IssCts").Value.ToString()
                        txtIssTap.Enabled = False
                        txtIssCts.Enabled = False
                        txtIssDate.Text = Format(CDate(rsIssSec.Fields("IssDate").Value), "dd/MM/yyyy")
                        txtIssTime.Text = Format(CDate(rsIssSec.Fields("IssTime").Value), "HH:mm")
                        ICNo2 = rsIssSec.Fields("EmpNo").Value.ToString()
                        txtemp.Text = ICNo2
                        grpIssues.Visible = True
                        grpReturns.Visible = True
                        grpSizingTypes.Visible = True
                        grpEmp.Visible = True
                        btnEmpNo.Focus()
                    End If
                    rsIssSec.Close()
                    rsIssSec = Nothing
                End If
            End If
        Catch ex As Exception
            MsgBox("Error in Load_ParcelDetails : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

#End Region

#Region "Calculate Totals"

    Private Sub Calculate()
        Try
            Dim totPcs As Double = 0
            Dim totCts As Double = 0

            For Each row As DataGridViewRow In dgvGrid.Rows
                If row.IsNewRow Then Continue For
                If IsNumeric(row.Cells(GRID_PCS).Value) Then totPcs += CDbl(row.Cells(GRID_PCS).Value)
                If IsNumeric(row.Cells(GRID_CTS).Value) Then totCts += CDbl(row.Cells(GRID_CTS).Value)
            Next

            If CDbl(If(txtIssTap.Text = "", "0", txtIssTap.Text)) > 0 Then
                If totPcs > CDbl(txtIssTap.Text) Then
                    MessageBox.Show("Invalid Pcs", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
                End If
                If totCts > CDbl(If(txtIssCts.Text = "", "0", txtIssCts.Text)) + 0.1 Then
                    MessageBox.Show("Invalid Cts", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
                End If
            End If

            txtTotPcs.Text = totPcs.ToString()
            txtTotCts.Text = totCts.ToString("#0.000")
            txtRetTap.Text = totPcs.ToString()
            txtRetCts.Text = totCts.ToString("#0.000")
        Catch ex As Exception
            MsgBox("Error in Calculate : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

#End Region

#Region "Add Type Row"


    Private Sub btnAddType_Click(sender As Object, e As EventArgs) Handles btnAddType.Click
        If txtAssortment.Text = "" Then
            MessageBox.Show("Invalid Assortment", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End If
        If cmbSize.SelectedItem Is Nothing OrElse cmbSize.Text = "" Then
            MessageBox.Show("Invalid Size Range", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End If
        If txtTypePcs.Text = "" Then
            MessageBox.Show("Invalid Pcs", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End If
        If txtTypeCts.Text = "" Then
            MessageBox.Show("Invalid Cts", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End If

        ' Validate assortment against DB
        Dim strMainAssort As String = ""
        Try
            Dim rsComSql As New ADODB.Recordset()
            rsComSql.Open(
                "SELECT MainAssort FROM tblGrading_RndSizeListRange " &
                "WHERE AssortNo='" & txtAssortment.Text.Trim() & "' AND Size='" & cmbSize.Text & "'",
                AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                ADODB.LockTypeEnum.adLockOptimistic)

            If rsComSql.EOF Then
                rsComSql.Close()
                MessageBox.Show("Invalid Assortment", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            strMainAssort = rsComSql.Fields("MainAssort").Value.ToString()
            rsComSql.Close()
            rsComSql = Nothing
        Catch ex As Exception
            MsgBox("Error in btnAddType_Click (DB validate) : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return
        End Try

        Dim currentTotPcs As Double = CDblSafe(txtTotPcs.Text)
        If currentTotPcs + CDblSafe(txtTypePcs.Text) > CDblSafe(txtIssTap.Text) Then
            MessageBox.Show("Invalid Pcs", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End If
        If currentTotPcs + CDblSafe(txtTotCts.Text) > CDblSafe(txtIssCts.Text) + 0.1 Then
            MessageBox.Show("Invalid Cts", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End If

        ' Find existing row or add new
        Dim foundRow As DataGridViewRow = Nothing
        For Each row As DataGridViewRow In dgvGrid.Rows
            If row.IsNewRow Then Continue For
            If row.Cells(GRID_ASSORT).Value?.ToString() = txtAssortment.Text AndAlso
               row.Cells(GRID_SIZE).Value?.ToString() = cmbSize.Text Then
                foundRow = row : Exit For
            End If
        Next

        If foundRow Is Nothing Then
            dgvGrid.Rows.Add(txtAssortment.Text.ToUpper(), cmbSize.Text,
                             txtTypePcs.Text, txtTypeCts.Text, strMainAssort)
        Else
            foundRow.Cells(GRID_PCS).Value =
                (CDblSafe(foundRow.Cells(GRID_PCS).Value?.ToString()) + CDblSafe(txtTypePcs.Text)).ToString()
            foundRow.Cells(GRID_CTS).Value =
                (CDblSafe(foundRow.Cells(GRID_CTS).Value?.ToString()) + CDblSafe(txtTypeCts.Text)).ToString("#0.000")
        End If

        Calculate()
        txtAssortment.Text = ""
        cmbSize.Items.Clear()
        cmbSize.Text = ""
        txtTypePcs.Text = ""
        txtTypeCts.Text = ""
        txtAssortment.Focus()
    End Sub

#End Region

#Region "Employee Grid"

    Private Sub btnEmpAdd_Click(sender As Object, e As EventArgs) Handles btnEmpAdd.Click
        If txtEmpNo.Text = "" OrElse txtEmpPcs.Text = "" Then
            MessageBox.Show("Invalid Emp. No./Pcs", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End If
        If txtEmpNo.Text.Length <> 6 Then
            MessageBox.Show("Invalid Emp No", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End If
        If CDblSafe(txtEmpPcs.Text) <= 0 Then
            MessageBox.Show("Invalid Pcs", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End If

        For Each row As DataGridViewRow In dgvEmp.Rows
            If row.IsNewRow Then Continue For
            If row.Cells(EMP_NO).Value?.ToString() = txtEmpNo.Text Then
                MessageBox.Show("Already Entered", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
        Next

        Dim dblTotPcs As Double = 0
        For Each row As DataGridViewRow In dgvEmp.Rows
            If row.IsNewRow Then Continue For
            dblTotPcs += CDblSafe(row.Cells(EMP_PCS).Value?.ToString())
        Next

        If CDblSafe(txtTotPcs.Text) >= dblTotPcs + CDblSafe(txtEmpPcs.Text) Then
            dgvEmp.Rows.Add(txtEmpNo.Text, txtEmpPcs.Text)
            txtEmpNo.Text = ""
            txtEmpPcs.Text = ""
            txtEmpNo.Focus()
        Else
            MessageBox.Show("Invalid Pcs", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub dgvEmp_DoubleClick(sender As Object, e As EventArgs) Handles dgvEmp.DoubleClick
        If dgvEmp.Rows.Count > 0 AndAlso dgvEmp.CurrentRow IsNot Nothing Then
            If MessageBox.Show("Are you sure you want to Delete?", Me.Text,
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                dgvEmp.Rows.Remove(dgvEmp.CurrentRow)
            End If
        End If
    End Sub

#End Region

#Region "Save Logic"

    Private Sub Recsave()
        If cmbDept.Text = "" Then
            MessageBox.Show("Invalid Department", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End If
        If txtemp.Text = "" OrElse txtemp.Text.Trim().Length <> 6 Then
            MessageBox.Show("Invalid Emp. No.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End If
        If txtParNo.Text = "" Then
            MessageBox.Show("Invalid Parcel No", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End If
        If txtPktNo.Text = "" Then
            MessageBox.Show("Invalid Packet No", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
        End If

        ' Validate packet exists
        Try
            Dim rsPkt As New ADODB.Recordset()
            rsPkt.Open(
                "SELECT COUNT(*) AS CNT FROM tblGrading_RndSizingPacket " &
                "WHERE ParNo='" & txtParNo.Text & "' AND PktNo='" & txtPktNo.Text & "'",
                AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                ADODB.LockTypeEnum.adLockOptimistic)
            Dim exists As Boolean = CInt(rsPkt.Fields("CNT").Value) > 0
            rsPkt.Close()
            rsPkt = Nothing
            If Not exists Then
                MessageBox.Show("Invalid Packet", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                frmInitialze()
                Return
            End If
        Catch ex As Exception
            MsgBox("Error in Recsave (packet check) : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return
        End Try

        If issued AndAlso Not Checked Then
            If txtIssTap.Text = "" OrElse CDblSafe(txtIssTap.Text) <= 0 Then
                MessageBox.Show("Invalid Pcs", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If txtIssCts.Text = "" OrElse CDblSafe(txtIssCts.Text) <= 0 Then
                MessageBox.Show("Invalid Cts", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If
            If txtIssDate.Text.Length < 2 Then Return

        ElseIf issued AndAlso Checked Then
            If txtRej.Text = "" Then txtRej.Text = "0"
            If txtRejCts.Text = "" Then txtRejCts.Text = "0"
            If txtLostPcs.Text = "" Then txtLostPcs.Text = "0"
            If txtLostCts.Text = "" Then txtLostCts.Text = "0"
            If txtRepPcs.Text = "" Then txtRepPcs.Text = "0"
            If txtRepCts.Text = "" Then txtRepCts.Text = "0"
            If txtRetTap.Text = "" Then txtRetTap.Text = "0"
            If txtRetCts.Text = "" Then txtRetCts.Text = "0"

            Dim stiss As Double = CDblSafe(txtIssTap.Text)
            Dim stret As Double = CDblSafe(txtRetTap.Text) + CDblSafe(txtRej.Text) +
                                   CDblSafe(txtLostPcs.Text) + CDblSafe(txtRepPcs.Text)
            If stiss <> stret Then
                MessageBox.Show("Stones Issued " & stiss & "   Stones Returned " & stret,
                                Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            Dim ciss As Single = CSng(txtIssCts.Text) + 0.1F
            Dim cret As Single = CSng(Math.Round(
                CDblSafe(txtRetCts.Text) + CDblSafe(txtRejCts.Text) +
                CDblSafe(txtLostCts.Text) + CDblSafe(txtRepCts.Text), 3))
            If ciss < cret Then
                MessageBox.Show("Carets issued " & ciss.ToString("##.###") &
                                "   Carets returned " & cret.ToString("##.###"),
                                Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
            End If

            If Section < 7 Then
                Dim intTotPcs As Double = 0
                Dim dblTotCts As Double = 0
                For Each row As DataGridViewRow In dgvGrid.Rows
                    If row.IsNewRow Then Continue For
                    intTotPcs += CDblSafe(row.Cells(GRID_PCS).Value?.ToString())
                    dblTotCts += CDblSafe(row.Cells(GRID_CTS).Value?.ToString())
                Next

                If CDblSafe(txtIssTap.Text) <>
                   intTotPcs + CDblSafe(txtRepPcs.Text) + CDblSafe(txtRej.Text) + CDblSafe(txtLostPcs.Text) Then
                    MessageBox.Show("Pcs not matching", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
                End If
                If Math.Round(CDblSafe(txtIssCts.Text) + 0.1, 3) <
                   Math.Round(dblTotCts + CDblSafe(txtRepCts.Text) + CDblSafe(txtRejCts.Text) + CDblSafe(txtLostCts.Text), 3) Then
                    MessageBox.Show("Cts not matching", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
                End If

                For Each row As DataGridViewRow In dgvGrid.Rows
                    If row.IsNewRow Then Continue For
                    Dim rowPcs As Double = CDblSafe(row.Cells(GRID_PCS).Value?.ToString())
                    Dim rowCts As Double = CDblSafe(row.Cells(GRID_CTS).Value?.ToString())
                    If (rowPcs > 0 AndAlso rowCts <= 0) OrElse (rowPcs <= 0 AndAlso rowCts > 0) Then
                        MessageBox.Show("Invalid Data in " & row.Cells(GRID_ASSORT).Value?.ToString(),
                                        Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
                    End If
                Next

                If dgvEmp.Rows.Count = 0 Then dgvEmp.Rows.Add(txtemp.Text, txtRetTap.Text)

                Dim empTotPcs As Double = 0
                For Each row As DataGridViewRow In dgvEmp.Rows
                    If row.IsNewRow Then Continue For
                    empTotPcs += CDblSafe(row.Cells(EMP_PCS).Value?.ToString())
                Next
                If CDblSafe(txtRetTap.Text) <> empTotPcs Then
                    MessageBox.Show("Emp Pcs not matching", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information) : Return
                End If
            End If
        Else
            If txtIssDate.Text.Length < 2 Then Return
        End If

        DataSave()
    End Sub


    '  DATA SAVE — Issue or Return INSERT

    Private Sub DataSave()
        Try
            Dim rsComSql As New ADODB.Recordset()

            If issued AndAlso Not Checked Then
                ' Duplicate check
                rsComSql.Open(
                    "SELECT COUNT(*) AS CNT FROM tblGrading_RndSizingIssues " &
                    "WHERE Department='" & cmbDept.Text & "' AND ParNo='" & txtParNo.Text & "' " &
                    "AND PktNo='" & txtPktNo.Text & "' AND Sec=" & (cmbSection.SelectedIndex + 1),
                    AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                    ADODB.LockTypeEnum.adLockOptimistic)
                If CInt(rsComSql.Fields("CNT").Value) > 0 Then
                    rsComSql.Close()
                    MessageBox.Show("Already Entered", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If
                rsComSql.Close()

                ' INSERT Issue
                rsComSql.Open(
                    "INSERT INTO tblGrading_RndSizingIssues" &
                    "(Department,ParNo,PktNo,Sec,EmpNo,IssPcs,IssCts,IssDate,IssTime) " &
                    "VALUES('Colombo','" & txtParNo.Text & "','" & txtPktNo.Text & "'," &
                    (cmbSection.SelectedIndex + 1) & ",'" & txtemp.Text.Trim().Left(6) & "'," &
                    CDblSafe(txtIssTap.Text) & "," & CDblSafe(txtIssCts.Text) & "," &
                    "'" & Date.Today.ToString("MM/dd/yyyy") & "','" & txtIssTime.Text & "')",
                    AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                    ADODB.LockTypeEnum.adLockOptimistic)
                rsComSql.Close()

            ElseIf issued AndAlso Checked Then
                ' Duplicate check
                rsComSql.Open(
                    "SELECT COUNT(*) AS CNT FROM tblGrading_RndSizingReturns " &
                    "WHERE Department='" & cmbDept.Text & "' AND ParNo='" & txtParNo.Text & "' " &
                    "AND PktNo='" & txtPktNo.Text & "' AND Sec=" & (cmbSection.SelectedIndex + 1),
                    AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                    ADODB.LockTypeEnum.adLockOptimistic)
                If CInt(rsComSql.Fields("CNT").Value) > 0 Then
                    rsComSql.Close()
                    MessageBox.Show("Already entered", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If
                rsComSql.Close()

                ' INSERT Return
                rsComSql.Open(
                    "INSERT INTO tblGrading_RndSizingReturns" &
                    "(Department,ParNo,PktNo,Sec,EmpNo,RetPcs,RetCts,LostPcs,LostCts," &
                    "RepPcs,RepCts,RetDate,RetTime,RejPcs,RejCts) " &
                    "VALUES('Colombo','" & txtParNo.Text & "','" & txtPktNo.Text & "'," &
                    (cmbSection.SelectedIndex + 1) & ",'" & txtemp.Text.Trim().Left(6) & "'," &
                    CDblSafe(txtRetTap.Text) & "," & CDblSafe(txtRetCts.Text) & "," &
                    CDblSafe(txtLostPcs.Text) & "," & CDblSafe(txtLostCts.Text) & "," &
                    CDblSafe(txtRepPcs.Text) & "," & CDblSafe(txtRepCts.Text) & "," &
                    "'" & Date.Today.ToString("MM/dd/yyyy") & "','" & txtRetTime.Text & "'," &
                    CDblSafe(txtRej.Text) & "," & CDblSafe(txtRejCts.Text) & ")",
                    AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                    ADODB.LockTypeEnum.adLockOptimistic)
                rsComSql.Close()

                Save_GradingTypes(txtParNo.Text, txtPktNo.Text, cmbSection.SelectedIndex + 1)
            End If

            rsComSql = Nothing
            frmInitialze()
        Catch ex As Exception
            MsgBox("Error in DataSave : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub


    '  SAVE GRADING TYPES + EMPLOYEE DETAILS

    Private Sub Save_GradingTypes(strParcelNo As String, strPktNo As String, intSection As Integer)
        Try
            Dim rsComSql As New ADODB.Recordset()

            ' DELETE existing sizing types for this packet/section
            rsComSql.Open(
                "DELETE FROM tblGrading_RndSizingTypes " &
                "WHERE Department='" & cmbDept.Text & "' AND ParNo='" & strParcelNo & "' " &
                "AND PktNo='" & strPktNo & "' AND Sec=" & intSection,
                AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                ADODB.LockTypeEnum.adLockOptimistic)
            rsComSql.Close()

            ' INSERT each grid row where Pcs > 0
            For Each row As DataGridViewRow In dgvGrid.Rows
                If row.IsNewRow Then Continue For
                If CDblSafe(row.Cells(GRID_PCS).Value?.ToString()) > 0 Then
                    rsComSql.Open(
                        "INSERT INTO tblGrading_RndSizingTypes" &
                        "(Department,ParNo,PktNo,Sec,ReturnType1,ReturnType2,ReturnType3,ReturnType4,ReturnType5,Pcs,Cts) " &
                        "VALUES('" & cmbDept.Text & "','" & strParcelNo & "','" & strPktNo & "'," & intSection & "," &
                        "'" & row.Cells(GRID_CODE).Value?.ToString() & "'," &
                        "'" & row.Cells(GRID_ASSORT).Value?.ToString() & "'," &
                        "'" & row.Cells(GRID_SIZE).Value?.ToString() & "','',''," &
                        CDblSafe(row.Cells(GRID_PCS).Value?.ToString()) & "," &
                        CDblSafe(row.Cells(GRID_CTS).Value?.ToString()) & ")",
                        AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                        ADODB.LockTypeEnum.adLockOptimistic)
                    rsComSql.Close()
                End If
            Next

            ' DELETE existing sizing details for this packet/section
            rsComSql.Open(
                "DELETE FROM tblGrading_RndSizingDetails " &
                "WHERE Department='" & cmbDept.Text & "' AND ParNo='" & strParcelNo & "' " &
                "AND PktNo='" & strPktNo & "' AND Sec=" & intSection,
                AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                ADODB.LockTypeEnum.adLockOptimistic)
            rsComSql.Close()

            ' INSERT employee rows
            For Each row As DataGridViewRow In dgvEmp.Rows
                If row.IsNewRow Then Continue For
                rsComSql.Open(
                    "INSERT INTO tblGrading_RndSizingDetails" &
                    "(Department,ParNo,PktNo,Sec,EmpNo,Pcs,RetDate) " &
                    "VALUES('" & cmbDept.Text & "','" & strParcelNo & "','" & strPktNo & "'," & intSection & "," &
                    "'" & row.Cells(EMP_NO).Value?.ToString() & "'," &
                    CDblSafe(row.Cells(EMP_PCS).Value?.ToString()) & "," &
                    "'" & Date.Today.ToString("MM/dd/yyyy") & "')",
                    AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                    ADODB.LockTypeEnum.adLockOptimistic)
                rsComSql.Close()
            Next

            rsComSql = Nothing
        Catch ex As Exception
            MsgBox("Error in Save_GradingTypes : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

#End Region

#Region "TextBox Key Events"

    Private Sub txtParNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtParNo.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True
            txtParNo.Text = txtParNo.Text.ToUpper()
            If ParcelFound(cmbDept.Text, txtParNo.Text) Then
                cmbSection.SelectedIndex = 0
                txtPktNo.Text = ""
                txtPktNo.Focus()
            Else
                MessageBox.Show("Invalid Parcel No.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtParNo.Text = ""
                txtPktNo.Text = ""
                txtParNo.Focus()
            End If
        End If
    End Sub

    Private Sub txtPktNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPktNo.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True
            If txtParNo.Text <> "" AndAlso txtPktNo.Text <> "" Then
                txtParNo.Text = txtParNo.Text.ToUpper()
                If ParcelFound(cmbDept.Text, txtParNo.Text) Then
                    Load_ParcelDetails()
                    btnEmpNo.Focus()
                Else
                    MessageBox.Show("Invalid Parcel No.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtParNo.Text = ""
                    txtPktNo.Text = ""
                    txtParNo.Focus()
                End If
            End If
        End If
    End Sub


    '  ASSORTMENT KEY PRESS
    Private Sub txtAssortment_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAssortment.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True
            If txtAssortment.Text = "" Then Return
            Try
                ' Validate assortment
                Dim rsComSql As New ADODB.Recordset()
                rsComSql.Open(
                    "SELECT COUNT(*) AS CNT FROM tblGrading_RndSizeListNew " &
                    "WHERE AssortNo='" & txtAssortment.Text.Trim() & "'",
                    AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                    ADODB.LockTypeEnum.adLockOptimistic)
                Dim valid As Boolean = CInt(rsComSql.Fields("CNT").Value) > 0
                rsComSql.Close()

                If Not valid Then
                    MessageBox.Show("Invalid Assortment", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtAssortment.Focus()
                    cmbSize.Items.Clear()
                    cmbSize.Text = ""
                    Return
                End If

                ' Load sizes
                txtAssortment.Text = txtAssortment.Text.ToUpper()
                cmbSize.Items.Clear()
                rsComSql.Open(
                    "SELECT Size FROM tblGrading_RndSizeListRange " &
                    "WHERE AssortNo='" & txtAssortment.Text.Trim() & "' ORDER BY Size",
                    AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                    ADODB.LockTypeEnum.adLockOptimistic)
                Do While Not rsComSql.EOF
                    cmbSize.Items.Add(rsComSql.Fields("Size").Value.ToString())
                    rsComSql.MoveNext()
                Loop
                rsComSql.Close()
                rsComSql = Nothing

                If cmbSize.Items.Count > 0 Then cmbSize.SelectedIndex = 0
                cmbSize.Focus()
            Catch ex As Exception
                MsgBox("Error in txtAssortment_KeyPress : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
            End Try
        End If
    End Sub


    '  EMP NO KEY PRESS — validate employee

    Private Sub txtEmpNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEmpNo.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True
            If txtEmpNo.Text.Length = 6 Then
                Try
                    Dim rsComSql As New ADODB.Recordset()
                    rsComSql.Open(
                        "SELECT COUNT(*) AS CNT FROM VW_EMP_MASTER WHERE EmpNo='" & txtEmpNo.Text.Trim() & "'",
                        AdoCN, ADODB.CursorTypeEnum.adOpenKeyset,
                        ADODB.LockTypeEnum.adLockOptimistic)
                    Dim valid As Boolean = CInt(rsComSql.Fields("CNT").Value) > 0
                    rsComSql.Close()
                    rsComSql = Nothing

                    If valid Then
                        txtEmpPcs.Focus()
                    Else
                        MessageBox.Show("Invalid Employee", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        txtEmpNo.Text = ""
                        txtEmpNo.Focus()
                    End If
                Catch ex As Exception
                    MsgBox("Error in txtEmpNo_KeyPress : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
                End Try
            Else
                MessageBox.Show("Invalid Employee", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtEmpNo.Text = ""
                txtEmpNo.Focus()
            End If
        End If
    End Sub

    ' ── remaining key/leave handlers — no DB calls, unchanged ──
    Private Sub txtIssTap_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIssTap.KeyPress
        AllowNumeric(e)
        If e.KeyChar = Chr(13) Then e.Handled = True : txtIssCts.Focus()
    End Sub
    Private Sub txtIssTap_Leave(sender As Object, e As EventArgs) Handles txtIssTap.Leave
        If Not IsNumeric(txtIssTap.Text) Then txtIssTap.Text = "0" : txtIssTap.Focus()
    End Sub
    Private Sub txtIssCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIssCts.KeyPress
        AllowNumeric(e)
    End Sub
    Private Sub txtTypePcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTypePcs.KeyPress
        AllowInteger(e)
        If e.KeyChar = Chr(13) AndAlso txtTypePcs.Text <> "" Then e.Handled = True : txtTypeCts.Focus()
    End Sub
    Private Sub txtTypeCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTypeCts.KeyPress
        AllowNumeric(e)
        If e.KeyChar = Chr(13) AndAlso txtTypeCts.Text <> "" Then e.Handled = True : btnAddType.Focus()
    End Sub
    Private Sub txtRej_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRej.KeyPress
        If e.KeyChar = Chr(13) Then e.Handled = True : txtRejCts.Focus()
    End Sub
    Private Sub txtRej_Leave(sender As Object, e As EventArgs) Handles txtRej.Leave
        If Not IsNumeric(txtRej.Text) Then txtRej.Text = "0"
    End Sub
    Private Sub txtRejCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRejCts.KeyPress
        If e.KeyChar = Chr(13) Then e.Handled = True : txtLostPcs.Focus()
    End Sub
    Private Sub txtRejCts_Leave(sender As Object, e As EventArgs) Handles txtRejCts.Leave
        If Not IsNumeric(txtRejCts.Text) Then txtRejCts.Text = "0"
    End Sub
    Private Sub txtLostPcs_Leave(sender As Object, e As EventArgs) Handles txtLostPcs.Leave
        If Not IsNumeric(txtLostPcs.Text) Then txtLostPcs.Text = "0"
    End Sub
    Private Sub txtLostCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLostCts.KeyPress
        If e.KeyChar = Chr(13) Then e.Handled = True : txtRepPcs.Focus()
    End Sub
    Private Sub txtLostCts_Leave(sender As Object, e As EventArgs) Handles txtLostCts.Leave
        If txtLostCts.Text = "" Then txtLostCts.Text = "0"
    End Sub
    Private Sub txtRepPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRepPcs.KeyPress
        If e.KeyChar = Chr(13) Then e.Handled = True : txtRepCts.Focus()
    End Sub
    Private Sub txtRepPcs_Leave(sender As Object, e As EventArgs) Handles txtRepPcs.Leave
        If txtRepPcs.Text = "" Then txtRepPcs.Text = "0"
    End Sub
    Private Sub txtRepCts_Leave(sender As Object, e As EventArgs) Handles txtRepCts.Leave
        If txtRepCts.Text = "" Then txtRepCts.Text = "0"
    End Sub
    Private Sub txtRetTap_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRetTap.KeyPress
        If e.KeyChar = Chr(13) Then e.Handled = True : txtRetCts.Focus()
    End Sub
    Private Sub txtRetTap_Leave(sender As Object, e As EventArgs) Handles txtRetTap.Leave
        If Not IsNumeric(txtRetTap.Text) Then txtRetTap.Text = "0" : txtRetTap.Focus()
    End Sub
    Private Sub txtRetCts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRetCts.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True
            If txtRej.Text = "" Then txtRej.Text = "0"
            If txtRejCts.Text = "" Then txtRejCts.Text = "0"
            If txtLostPcs.Text = "" Then txtLostPcs.Text = "0"
            If txtLostCts.Text = "" Then txtLostCts.Text = "0"
            txtRej.Focus()
        End If
    End Sub
    Private Sub txtRetCts_Leave(sender As Object, e As EventArgs) Handles txtRetCts.Leave
        If Not IsNumeric(txtRetCts.Text) Then txtRetCts.Text = "0" : txtRetCts.Focus()
    End Sub
    Private Sub txtEmpPcs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEmpPcs.KeyPress
        AllowInteger(e)
        If e.KeyChar = Chr(13) Then
            e.Handled = True
            If txtEmpPcs.Text <> "" AndAlso CDblSafe(txtEmpPcs.Text) > 0 Then
                btnEmpAdd.Focus()
            Else
                MessageBox.Show("Invalid Pcs", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtEmpPcs.Text = ""
                txtEmpPcs.Focus()
            End If
        End If
    End Sub

#End Region

#Region "dgvGrid Cell Edit"

    Private Sub dgvGrid_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvGrid.CellValueChanged
        If e.ColumnIndex = GRID_PCS OrElse e.ColumnIndex = GRID_CTS Then Calculate()
    End Sub

    Private Sub dgvGrid_EditingControlShowing(sender As Object,
        e As DataGridViewEditingControlShowingEventArgs) Handles dgvGrid.EditingControlShowing
        If dgvGrid.CurrentCell IsNot Nothing Then
            Dim col As Integer = dgvGrid.CurrentCell.ColumnIndex
            Dim tb As TextBox = TryCast(e.Control, TextBox)
            If tb IsNot Nothing Then
                RemoveHandler tb.KeyPress, AddressOf Grid_NumericKeyPress
                RemoveHandler tb.KeyPress, AddressOf Grid_IntegerKeyPress
                If col = GRID_PCS Then
                    AddHandler tb.KeyPress, AddressOf Grid_IntegerKeyPress
                ElseIf col = GRID_CTS Then
                    AddHandler tb.KeyPress, AddressOf Grid_NumericKeyPress
                End If
            End If
        End If
    End Sub

    Private Sub Grid_NumericKeyPress(sender As Object, e As KeyPressEventArgs)
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c AndAlso e.KeyChar <> Chr(8) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Grid_IntegerKeyPress(sender As Object, e As KeyPressEventArgs)
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> Chr(8) Then e.Handled = True
    End Sub

#End Region

#Region "Button Bar"

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        If txtParNo.Text <> "" Then
            txtParNo.Text = txtParNo.Text.ToUpper()
            GetNewPacket()
            btnEmpNo.Focus()
        Else
            MessageBox.Show("Please enter the Parcel No.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtParNo.Focus()
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If MessageBox.Show("Are you sure?", "Deleting...",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question Or MessageBoxIcon.Warning) = DialogResult.Yes Then
            ' TODO: implement delete logic if required
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Recsave()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        frmInitialze()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnEmpNo_Click(sender As Object, e As EventArgs) Handles btnEmpNo.Click
        Dim empNo As String = InputBox("Enter Employee No (6 digits):", "Employee No")
        If empNo = "" Then Return

        If issued AndAlso Checked Then
            If ICNo2 <> "" AndAlso ICNo2.Trim() <> empNo.Trim() Then
                Dim resp As DialogResult =
                    MessageBox.Show("IC Numbers do not match. Proceed Anyway?",
                                    Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If resp = DialogResult.Yes Then
                    txtemp.Text = empNo
                    txtRetDate.Text = Format(Date.Today, "dd/MM/yyyy")
                    txtRetTime.Text = Format(Date.Now, "HH:mm")
                Else
                    txtemp.Text = ""
                End If
            Else
                txtemp.Text = empNo
                txtRetDate.Text = Format(Date.Today, "dd/MM/yyyy")
                txtRetTime.Text = Format(Date.Now, "HH:mm")
                If Section >= 5 Then txtRetTap.Focus()
            End If
        ElseIf issued AndAlso Not Checked Then
            txtemp.Text = empNo
            txtRetDate.Text = Format(Date.Today, "dd/MM/yyyy")
            txtRetTime.Text = Format(Date.Now, "HH:mm")
        Else
            txtemp.Text = empNo
            txtIssDate.Text = Format(Date.Today, "dd/MM/yyyy")
            txtIssTime.Text = Format(Date.Now, "HH:mm")
            txtIssTap.Focus()
        End If
    End Sub

    Private Sub chkNew_CheckedChanged(sender As Object, e As EventArgs) Handles chkNew.CheckedChanged
        If txtType1.Text <> "" AndAlso txtPktType.Text <> "" Then
            Load_GradingTypes(txtType1.Text, txtPktType.Text)
        End If
    End Sub

    Private Sub cmbSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSection.SelectedIndexChanged
        txtTotPcs.Text = "0"
        txtTotCts.Text = "0"
    End Sub

#End Region

#Region "Helper Utilities"

    Private Function CDblSafe(value As String) As Double
        If String.IsNullOrWhiteSpace(value) Then Return 0
        Dim result As Double
        If Double.TryParse(value.Replace(",", ""), result) Then Return result
        Return 0
    End Function

    Private Sub AllowNumeric(e As KeyPressEventArgs)
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c AndAlso
           e.KeyChar <> Chr(8) AndAlso e.KeyChar <> Chr(13) Then
            e.Handled = True
        End If
    End Sub

    Private Sub AllowInteger(e As KeyPressEventArgs)
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> Chr(8) AndAlso e.KeyChar <> Chr(13) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtParNo_TextChanged(sender As Object, e As EventArgs) Handles txtParNo.TextChanged

    End Sub

    Private Sub txtRetTime_TextChanged(sender As Object, e As EventArgs) Handles txtRetTime.TextChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub cmbDept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDept.SelectedIndexChanged

    End Sub

#End Region

End Class

'──────────────────────────────────────────────────────────────────
'  String extension helper — replaces VB6 Left() function (unchanged)
'──────────────────────────────────────────────────────────────────
Module StringExtensions
    <System.Runtime.CompilerServices.Extension()>
    Public Function Left(s As String, length As Integer) As String
        If s Is Nothing OrElse length <= 0 Then Return ""
        If s.Length <= length Then Return s
        Return s.Substring(0, length)
    End Function
End Module