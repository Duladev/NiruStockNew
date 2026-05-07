Module mdlNiruBridge

    ' ── ErrorLog ─────────────────────────────────────────────────
    Public Sub ErrorLog(ByVal strFormName As String,
                        ByVal strProcName As String)
        Try
            System.Diagnostics.Debug.WriteLine(
                "ERROR in [" & strFormName & "] Proc: [" & strProcName & "]")
        Catch
        End Try
    End Sub

    ' ── Log_book ─────────────────────────────────────────────────
    Public Sub Log_book(ByVal p1 As String,
                        ByVal p2 As String,
                        Optional ByVal p3 As String = "",
                        Optional ByVal p4 As String = "",
                        Optional ByVal p5 As String = "",
                        Optional ByVal p6 As String = "")
        Try
            System.Diagnostics.Debug.WriteLine(
                "LOG: " & p1 & " | " & p2 & " | " & p3 & " | " & p4)
        Catch
        End Try
    End Sub

    ' ── ShellEx ──────────────────────────────────────────────────
    Public Sub ShellEx(ByVal strFilePath As String)
        Try
            System.Diagnostics.Process.Start(
                New System.Diagnostics.ProcessStartInfo(strFilePath) With {
                    .UseShellExecute = True
                })
        Catch ex As Exception
            MessageBox.Show("Cannot open file: " & strFilePath,
                            "File Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
        End Try
    End Sub

    ' ── Missing Variables ─────────────────────────────────────────
    Public Prod_CODE As String
    Public Prod_WAN_CODE As String
    Public Prod_SITE_CODE As String
    Public Prod_SITE_NAME As String

End Module

' ── CompanyItem class ─────────────────────────────────────────────
Public Class CompanyItem
    Public Property Code As String = ""
    Public Property Name As String = ""
    'P'ublic Property Prod_CODE As String = ""

    Public Sub New(ByVal code As String, ByVal name As String)
        Me.Code = code
        Me.Name = name
    End Sub

    Public Overrides Function ToString() As String
        Return Name
    End Function
End Class