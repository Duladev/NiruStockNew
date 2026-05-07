Public Class frm_GRDRnd_HOT_Company

    Private ValidateOk As Boolean = False

    ' ── FORM LOAD ───────────────────────────────────────────────────
    Private Sub frmHOT_Company_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.StartPosition = FormStartPosition.CenterScreen
            Load_Hotel_Name()
        Catch ex As Exception
            MsgBox("Error in Form_Load : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    ' ── LOAD COMPANIES INTO COMBO BOX ───────────────────────────────
    ' Fix: Prod_CODE → Prod_WK_ID  (existing DiaStock variable)
    Private Sub Load_Hotel_Name()
        Dim rs As New ADODB.Recordset
        Try
            cmbHotel.Items.Clear()
            rs.Open("SELECT WAN_CODE, WAN_NAME FROM tblGrading_RndWAN_LOCA ORDER BY WAN_NAME",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

            Do While Not rs.EOF
                Dim code As String = rs.Fields("WAN_CODE").Value.ToString().Trim()
                Dim name As String = rs.Fields("WAN_NAME").Value.ToString().Trim()

                ' Prod_WK_ID = "XX" means show all companies (super-admin)
                ' Prod_WK_ID = specific code means restrict to that company only
                If Prod_WK_ID = "XX" Then
                    cmbHotel.Items.Add(New CompanyItem(code, name))
                ElseIf Prod_WK_ID = code Then
                    Dim item As New CompanyItem(code, name)
                    cmbHotel.Items.Add(item)
                    cmbHotel.SelectedItem = item
                End If
                rs.MoveNext()
            Loop

            ' Safety: if nothing loaded (Prod_WK_ID not matched), load all
            If cmbHotel.Items.Count = 0 Then
                If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
                rs.Open("SELECT WAN_CODE, WAN_NAME FROM tblGrading_RndWAN_LOCA ORDER BY WAN_NAME",
                        AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
                Do While Not rs.EOF
                    cmbHotel.Items.Add(New CompanyItem(
                        rs.Fields("WAN_CODE").Value.ToString().Trim(),
                        rs.Fields("WAN_NAME").Value.ToString().Trim()))
                    rs.MoveNext()
                Loop
            End If

        Catch ex As Exception
            MsgBox("Error No : " & ex.HResult &
                   vbCrLf & "Description : " & ex.Message &
                   vbCrLf & "Function : Load Company Name",
                   MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub

    ' ── COMPANY SELECTED FROM COMBO — LOAD FIELDS ───────────────────
    ' Fix: Prod_SITE_CODE → WAN_LOCA  (existing DiaStock variable)
    '      Prod_SITE_NAME → PBCompName (existing DiaStock variable)
    Private Sub cmbHotel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbHotel.SelectedIndexChanged
        Dim rs As New ADODB.Recordset
        Try
            If cmbHotel.SelectedItem Is Nothing Then Return

            Dim selected As CompanyItem = CType(cmbHotel.SelectedItem, CompanyItem)
            Dim codeQ As String = selected.Code.Replace("'", "''")

            rs.Open("SELECT * FROM tblGrading_RndWAN_LOCA WHERE WAN_CODE='" & codeQ & "'",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

            If Not rs.EOF Then
                ' Store in existing DiaStock session variables
                WAN_LOCA = rs.Fields("WAN_CODE").Value.ToString().Trim()   ' was Prod_SITE_CODE
                PBCompName = rs.Fields("WAN_NAME").Value.ToString().Trim()   ' was Prod_SITE_NAME

                txtCompany.Text = rs.Fields("WAN_NAME").Value.ToString().Trim()
                txtCompanyCode.Text = rs.Fields("WAN_CODE").Value.ToString().Trim()
                txtStreet.Text = rs.Fields("STREET").Value.ToString().Trim()
                txtCity.Text = rs.Fields("CITY").Value.ToString().Trim()
                txtCountry.Text = rs.Fields("COUNTRY").Value.ToString().Trim()
                txtTelephone.Text = rs.Fields("TELEPHONE").Value.ToString().Trim()
                txtFax.Text = rs.Fields("FAX").Value.ToString().Trim()
                txtEmail.Text = rs.Fields("EMAIL").Value.ToString().Trim()
                txtEPF.Text = rs.Fields("EPFRegNo").Value.ToString().Trim()
                txtETF.Text = rs.Fields("ETFRegNo").Value.ToString().Trim()
                txtBankAC.Text = rs.Fields("BankACNo").Value.ToString().Trim()
            End If

        Catch ex As Exception
            MsgBox("Error in cmbHotel_SelectedIndexChanged : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
        End Try
    End Sub

    ' ── NEW BUTTON — CLEAR FORM ──────────────────────────────────────
    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click
        ClearAll()
    End Sub

    ' ── SAVE BUTTON — INSERT OR UPDATE ──────────────────────────────
    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Dim rs As New ADODB.Recordset
        Dim cmd As New ADODB.Command
        Try
            Validate()
            If Not ValidateOk Then
                MessageBox.Show("1.  Enter the Company Name" & vbCrLf & vbCrLf &
                                "2.  Enter the Company Code",
                                Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim codeQ As String = txtCompanyCode.Text.Trim().Replace("'", "''")
            Dim nameQ As String = txtCompany.Text.Trim().Replace("'", "''")
            Dim streetQ As String = txtStreet.Text.Trim().Replace("'", "''")
            Dim cityQ As String = txtCity.Text.Trim().Replace("'", "''")
            Dim countryQ As String = txtCountry.Text.Trim().Replace("'", "''")
            Dim telQ As String = txtTelephone.Text.Trim().Replace("'", "''")
            Dim faxQ As String = txtFax.Text.Trim().Replace("'", "''")
            Dim emailQ As String = txtEmail.Text.Trim().Replace("'", "''")
            Dim epfQ As String = txtEPF.Text.Trim().Replace("'", "''")
            Dim etfQ As String = txtETF.Text.Trim().Replace("'", "''")
            Dim bankQ As String = txtBankAC.Text.Trim().Replace("'", "''")
            Dim dateStr As String = Date.Now.ToString("MM/dd/yyyy")

            ' Check if company already exists
            rs.Open("SELECT COUNT(*) AS Cnt FROM tblGrading_RndWAN_LOCA WHERE WAN_CODE='" & codeQ & "'",
                    AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
            Dim exists As Boolean = Not rs.EOF AndAlso (CInt(rs.Fields("Cnt").Value) > 0)
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()

            cmd.ActiveConnection = AdoCN

            If Not exists Then
                ' --- INSERT new company into WAN_LOCA ---
                ' Column order matches WAN_LOCA schema:
                ' WAN_NAME, WAN_CODE, RESERV_CODE, ACTIVE, STREET, CITY, STATE,
                ' COUNTRY, TELEPHONE, FAX, EMAIL, USER_ID_1, PAY_DATE,
                ' EPFRegNo, BankACNo, BankBranchNo (blank), ETFRegNo
                cmd.CommandText =
                    "INSERT INTO tblGrading_RndWAN_LOCA(" &
                    "WAN_NAME,WAN_CODE,RESERV_CODE,ACTIVE,STREET,CITY,STATE," &
                    "COUNTRY,TELEPHONE,FAX,EMAIL,USER_ID_1,PAY_DATE," &
                    "EPFRegNo,BankACNo,ETFRegNo) VALUES(" &
                    "'" & nameQ & "','" & codeQ & "','S',1,'" &
                    streetQ & "','" & cityQ & "','','" &
                    countryQ & "','" & telQ & "','" & faxQ & "','" &
                    emailQ & "','','" & dateStr & "','" &
                    epfQ & "','" & bankQ & "','" & etfQ & "')"
                cmd.Execute()

                ' Also insert into PAY_SYS_PAR if not exists
                rs.Open("SELECT COUNT(*) AS Cnt FROM PAY_SYS_PAR WHERE COMP_CODE='" & codeQ & "'",
                        AdoCN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
                Dim parExists As Boolean = Not rs.EOF AndAlso (CInt(rs.Fields("Cnt").Value) > 0)
                If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()

                If Not parExists Then
                    cmd.CommandText =
                        "INSERT INTO PAY_SYS_PAR VALUES('" & codeQ & "','" & dateStr & "')"
                    cmd.Execute()
                End If

            Else
                ' --- UPDATE existing company ---
                Dim confirm As DialogResult =
                    MessageBox.Show("Do you want to amend " & txtCompanyCode.Text & " - " & txtCompany.Text & " ?",
                                    Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If confirm = DialogResult.Yes Then
                    cmd.CommandText =
                        "UPDATE WAN_LOCA SET " &
                        "WAN_NAME='" & nameQ & "'," &
                        "STREET='" & streetQ & "'," &
                        "CITY='" & cityQ & "'," &
                        "COUNTRY='" & countryQ & "'," &
                        "TELEPHONE='" & telQ & "'," &
                        "FAX='" & faxQ & "'," &
                        "EMAIL='" & emailQ & "'," &
                        "EPFRegNo='" & epfQ & "'," &
                        "ETFRegNo='" & etfQ & "'," &
                        "BankACNo='" & bankQ & "' " &
                        "WHERE WAN_CODE='" & codeQ & "'"
                    cmd.Execute()
                End If
            End If

            ClearAll()
            Load_Hotel_Name()

        Catch ex As Exception
            MsgBox("Error in cmdSave_Click : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        Finally
            If rs.State = ADODB.ObjectStateEnum.adStateOpen Then rs.Close()
            rs = Nothing
            cmd = Nothing
        End Try
    End Sub

    ' ── VALIDATE ────────────────────────────────────────────────────
    Private Sub Validate()
        ValidateOk = True
        If txtCompany.Text.Trim().Length = 0 Then
            ValidateOk = False
        ElseIf txtCompanyCode.Text.Trim().Length = 0 Then
            ValidateOk = False
        End If
    End Sub

    ' ── CLEAR ALL FIELDS (no DB — unchanged) ────────────────────────
    Private Sub ClearAll()
        txtCompany.Text = ""
        txtCompanyCode.Text = ""
        txtStreet.Text = ""
        txtCity.Text = ""
        txtCountry.Text = ""
        txtTelephone.Text = ""
        txtFax.Text = ""
        txtEmail.Text = ""
        txtEPF.Text = ""
        txtETF.Text = ""
        txtBankAC.Text = ""
    End Sub

    ' ── EXIT BUTTON ─────────────────────────────────────────────────
    Private Sub cmdExit_Click(sender As Object, e As EventArgs) Handles cmdExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            MsgBox("Error in cmdExit_Click : " & ex.Message, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

End Class
