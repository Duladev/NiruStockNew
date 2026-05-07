#Region " Imports "

Imports System.Drawing.Drawing2D
Imports System.ComponentModel

#End Region

'|------DO-NOT-REMOVE------|

' Creator: HazelDev
' Site   : HazelDev.co.nr
' Created: 10.Apr.2014
' Changed: 18.Jun.2014
' Version: 1.2.6

'|------DO-NOT-REMOVE------|

MustInherit Class Theme
    Inherits ContainerControl

#Region " Initialization "

    Protected G As Graphics
    MustOverride Sub PaintHook()
    Private _Rectangle As Rectangle
    Private _Gradient As LinearGradientBrush

    Sub New()
        SetStyle(DirectCast(139270, ControlStyles), True)
    End Sub
    Protected NotOverridable Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        If Width = 0 OrElse Height = 0 Then Return
        G = e.Graphics
        PaintHook()
    End Sub
    Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
        MyBase.OnHandleCreated(e)
    End Sub
    Protected Sub DrawGradient(ByVal c1 As Color, ByVal c2 As Color, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal angle As Single)
        _Rectangle = New Rectangle(x, y, width, height)
        _Gradient = New LinearGradientBrush(_Rectangle, c1, c2, angle)
        G.FillRectangle(_Gradient, _Rectangle)
    End Sub
    Protected Sub DrawBorders(ByVal p1 As Pen, ByVal p2 As Pen, ByVal rect As Rectangle)
        G.DrawRectangle(p1, rect.X, rect.Y, rect.Width - 1, rect.Height - 1)
        G.DrawRectangle(p2, rect.X + 1, rect.Y + 1, rect.Width - 3, rect.Height - 3)
    End Sub

#Region " Properties "

    Private _TransparencyKey As Color
    Property TransparencyKey() As Color
        Get
            Return _TransparencyKey
        End Get
        Set(ByVal v As Color)
            _TransparencyKey = v
            Invalidate()
        End Set
    End Property

#End Region

#End Region

End Class

#Region " Theme Container "

Class HazelDev_ThemeContainer
    Inherits Theme

#Region " Variables "

    Private cWidth, cHeight As Integer
    Dim myBrush As New SolidBrush(Color.FromArgb(113, 113, 113))
    Dim myPen As New Pen(myBrush)

#End Region
#Region " Custom Properties "

    Private _DrawBottomLine As Boolean
    Property DrawBottomLine() As Boolean
        Get
            Return _DrawBottomLine
        End Get
        Set(ByVal v As Boolean)
            _DrawBottomLine = v
            Invalidate()
        End Set
    End Property

#End Region

    Sub New()
        Dock = DockStyle.Fill
        Me.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Regular)
    End Sub

    Public Overrides Sub PaintHook()
        cWidth = Width : cHeight = Height
        G.Clear(SystemColors.Control)
        DrawGradient(Color.FromArgb(66, 157, 209), Color.FromArgb(35, 131, 207), 0, 0, Width, 55, 90S)
        DrawGradient(Color.White, Color.White, 0, 50, Width, Height - 50, 90S)

        G.DrawString(Text, New Font("Tahoma", 12, FontStyle.Bold), Brushes.White, New Rectangle(10, 15, cWidth, cHeight), New StringFormat() With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Near})
        If _DrawBottomLine = True Then
            G.DrawLine(myPen, 0, 50, Width, 50)
        End If
    End Sub

End Class

#End Region
#Region " Header Label "
Class HazelDev_HeaderLabel
    Inherits Label

    Sub New()
        Font = New Font("Tahoma", 12, FontStyle.Bold)
        ForeColor = Color.White
        BackColor = Color.Transparent
    End Sub
End Class

#End Region
#Region " Label "

Class HazelDev_Label
    Inherits Label

    Sub New()
        Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular)
        ForeColor = Color.FromArgb(54, 54, 54)
        BackColor = Color.Transparent
    End Sub
End Class

#End Region
#Region " Link Label "
Class HazelDev_LinkLabel
    Inherits LinkLabel

    Sub New()
        Font = New Font("Tahoma", 9, FontStyle.Regular)
        BackColor = Color.Transparent
        LinkColor = Color.FromArgb(66, 157, 209)
        ActiveLinkColor = Color.FromArgb(66, 157, 209)
        VisitedLinkColor = Color.FromArgb(66, 157, 209)
        LinkBehavior = Windows.Forms.LinkBehavior.HoverUnderline
    End Sub
End Class

#End Region
#Region " Panel "

Class HazelDev_Panel
    Inherits Theme
    Public Sub New()
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        SetStyle(ControlStyles.Opaque, False)
    End Sub

    Public Overrides Sub PaintHook()
        Me.Font = New Font("Tahoma", 9)
        Me.ForeColor = Color.FromArgb(54, 54, 54)
        Me.BackColor = Color.White
        G.SmoothingMode = SmoothingMode.AntiAlias
        G.FillRectangle(New SolidBrush(Color.White), New Rectangle(0, 0, Width, Height))
        G.FillRectangle(New SolidBrush(Color.White), New Rectangle(0, 0, Width - 1, Height - 1))
        G.DrawRectangle(New Pen(Color.FromArgb(202, 202, 202)), 0, 0, Width - 1, Height - 1)
    End Sub
End Class

#End Region
#Region " Button "
Class HazelDev_Button
    Inherits Button

    Sub New()
        Size = New Point(110, 30)
        Font = New Font("Tahoma", 12, FontStyle.Regular)
        AutoEllipsis = False
        ForeColor = Color.White
        BackColor = Color.FromArgb(66, 157, 209)
        FlatStyle = Windows.Forms.FlatStyle.Flat
        FlatAppearance.BorderSize = 0
        FlatAppearance.BorderColor = Color.FromArgb(66, 157, 209)
        FlatAppearance.MouseDownBackColor = Color.FromArgb(37, 127, 179)
        FlatAppearance.MouseOverBackColor = Color.FromArgb(59, 141, 188)
    End Sub

End Class

#End Region
#Region " TabControl "

'|------DO-NOT-REMOVE------|
'|---------CREDITS---------|

' Original Des.: Mephobia
' Modified Des.: HazelDev

'|---------CREDITS---------|
'|------DO-NOT-REMOVE------|

Public Class HazelDevTabControl
    Inherits TabControl

#Region " Variables "

    Private cActiveTab As Color

#End Region

    Public Function RoundRect(ByVal Rectangle As Rectangle, ByVal Curve As Integer) As GraphicsPath
        Dim P As GraphicsPath = New GraphicsPath()
        Dim ArcRectangleWidth As Integer = Curve * 2
        P.AddArc(New Rectangle(Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -180, 90)
        P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -90, 90)
        P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 0, 90)
        P.AddArc(New Rectangle(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 90, 90)
        P.AddLine(New Point(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y), New Point(Rectangle.X, Curve + Rectangle.Y))
        Return P
    End Function

    Sub New()
        MyBase.New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.ResizeRedraw Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.SupportsTransparentBackColor, True)
        DoubleBuffered = True
        BackColor = Color.White
        ForeColor = Color.Gray
        cActiveTab = Color.FromArgb(66, 157, 209)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)
        MyBase.OnPaint(e)

        G.SmoothingMode = SmoothingMode.HighQuality
        G.Clear(Color.White)

        For i As Integer = 0 To TabPages.Count - 1
            Dim TabRect As New Rectangle(GetTabRect(i).X + 3, GetTabRect(i).Y, GetTabRect(i).Width - 5, GetTabRect(i).Height)
            G.FillRectangle(New SolidBrush(Color.White), TabRect)
            G.DrawString(TabPages(i).Text, New Font("Tahoma", 9, FontStyle.Bold), New SolidBrush(ForeColor), TabRect, New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Center})
        Next

        G.FillRectangle(New SolidBrush(Color.White), 0, ItemSize.Height, Width, Height)

        If Not SelectedIndex = -1 Then
            Dim TabRect As New Rectangle(GetTabRect(SelectedIndex).X + 3, GetTabRect(SelectedIndex).Y, GetTabRect(SelectedIndex).Width - 5, GetTabRect(SelectedIndex).Height)
            G.FillPath(New SolidBrush(cActiveTab), RoundRect(TabRect, 4))
            G.DrawPath(New Pen(New SolidBrush(Color.FromArgb(66, 157, 209))), RoundRect(TabRect, 4))
            G.DrawString(TabPages(SelectedIndex).Text, New Font("Tahoma", 9, FontStyle.Bold), New SolidBrush(Color.White), TabRect, New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
        End If

        e.Graphics.DrawImage(B, New Point(0, 0))
        G.Dispose() : B.Dispose()
    End Sub
End Class

#End Region
#Region " Progress Bar "

Class HazelDev_ProgressBar
    Inherits Theme

#Region " Variables "

    Private _Maximum As Integer = 100
    Private _Value As Integer = 0
    Private Percent As Double
    Private ALN As Alignment

#End Region
#Region " Properties "

    Public Property Value() As Integer
        Get
            Return _Value
        End Get
        Set(ByVal value As Integer)
            If value > _Maximum Then value = _Maximum
            If value < 1 Then value = 1
            _Value = value
            Invalidate()
        End Set
    End Property
    Public Property Maximum() As Integer
        Get
            Return _Maximum
        End Get
        Set(ByVal value As Integer)
            If value < 1 Then value = 1
            If value < _Value Then _Value = value
            _Maximum = value
            Invalidate()
        End Set
    End Property

    Public Property ValueAlignment() As Alignment
        Get
            Return ALN
        End Get
        Set(ByVal value As Alignment)
            ALN = value
            Invalidate()
        End Set
    End Property

#End Region
#Region " Enums "

    Public Enum Alignment
        Right
        Center
    End Enum

#End Region

    Sub New()
        Size = New Size(162, 27)
        ValueAlignment = Alignment.Right
    End Sub

    Overrides Sub PaintHook()
        G.Clear(Color.White)
        Dim myBrush As New SolidBrush(Color.FromArgb(202, 202, 202))
        Dim myPen As New Pen(myBrush)

        Percent = (_Value / _Maximum) * 100
        If (_Value > 0) Then
            DrawGradient(Color.FromArgb(66, 157, 209), Color.FromArgb(66, 157, 209), 3, 3, CInt((_Value / _Maximum) * Width) - 6, Height - 6, 90)
        End If
        G.DrawRectangle(myPen, 0, 0, Width - 1, Height - 1)
        DrawBorders(myPen, Pens.Transparent, ClientRectangle)

        Dim DrawString As String = CStr(CInt(Percent)) & "%"
        Dim textX As Integer = Me.Width - G.MeasureString(DrawString, Font).Width - 5
        Dim textY As Integer = (Me.Height / 2) - (G.MeasureString(DrawString, Font).Height / 2)

        Select Case ValueAlignment
            Case Alignment.Right
                G.DrawString(DrawString, Font, Brushes.DimGray, New Point(textX, textY))
            Case Alignment.Center
                G.DrawString(DrawString, Font, New SolidBrush(Color.DimGray), New Rectangle(0, 0, Width - 1, Height - 1), New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
        End Select

    End Sub
End Class

#End Region
#Region " Radio Button "

Class HazelDev_RadioButton
    Inherits Theme

#Region " Variables "

    Private X As Integer
    Private _Checked As Boolean

#End Region
#Region " Properties "

    Property Checked() As Boolean
        Get
            Return _Checked
        End Get
        Set(ByVal value As Boolean)
            _Checked = value
            InvalidateControls()
            RaiseEvent CheckedChanged(Me)
            Invalidate()
        End Set
    End Property

#End Region
#Region " EventArgs "

    Event CheckedChanged(ByVal sender As Object)

    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        If Not _Checked Then Checked = True
        MyBase.OnMouseDown(e)
    End Sub
    Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseMove(e)
        X = e.X
        Invalidate()
    End Sub
    Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
        MyBase.OnTextChanged(e)
        Dim textSize As Integer
        textSize = Me.CreateGraphics.MeasureString(Text, Font).Width
        Me.Width = 20 + textSize
    End Sub

#End Region

    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
        InvalidateControls()
    End Sub

    Public Sub New()
        Width = 161
        Height = 18
        BackColor = Color.White
    End Sub

    Private Sub InvalidateControls()
        If Not IsHandleCreated OrElse Not _Checked Then Return

        For Each C As Control In Parent.Controls
            If C IsNot Me AndAlso TypeOf C Is HazelDev_RadioButton Then
                DirectCast(C, HazelDev_RadioButton).Checked = False
            End If
        Next
    End Sub

    Overrides Sub PaintHook()
        Dim myBrush As New SolidBrush(Color.FromArgb(54, 54, 54))
        Dim myBrush2 As New SolidBrush(Color.FromArgb(202, 202, 202))
        Dim myPen As New Pen(myBrush2)
        G.Clear(BackColor)
        G.SmoothingMode = SmoothingMode.HighQuality
        If _Checked Then
            G.DrawEllipse(myPen, New Rectangle(0, 0, 16, 16))
            G.FillEllipse(New SolidBrush(Color.FromArgb(66, 157, 209)), New Rectangle(4, 4, 8, 8))
        Else
            G.DrawEllipse(myPen, New Rectangle(0, 0, 16, 16))
        End If
        G.DrawString(Text, Font, myBrush, New Point(20, 1))
    End Sub
End Class

#End Region
#Region " CheckBox "

<DefaultEvent("CheckedChanged")> _
Class HazelDev_CheckBox
    Inherits Theme

#Region " Variables "

    Private X As Integer
    Private TextColor, BG As Color
    Private _Checked As Boolean

#End Region
#Region " Properties "

    Property Checked() As Boolean
        Get
            Return _Checked
        End Get
        Set(ByVal value As Boolean)
            _Checked = value
            Invalidate()
        End Set
    End Property

#End Region
#Region " EventArgs "

    Event CheckedChanged(ByVal sender As Object)

    Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseMove(e)
        X = e.Location.X
        Invalidate()
    End Sub
    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        _Checked = Not _Checked
        RaiseEvent CheckedChanged(Me)
        MyBase.OnMouseDown(e)
    End Sub

#End Region

    Sub New()
        Width = 148
        Height = 16
        Font = New Font("Microsoft Sans Serif", 9)
    End Sub

    Overrides Sub PaintHook()
        G.Clear(Color.White)
        Dim myBrush As New SolidBrush(Color.FromArgb(54, 54, 54))
        If _Checked Then
            G.FillRectangle(New SolidBrush(Color.FromArgb(202, 202, 202)), New Rectangle(0, 0, 16, 16))
            G.FillRectangle(New SolidBrush(Color.White), New Rectangle(1, 1, 16 - 2, 16 - 2))
        Else
            G.FillRectangle(New SolidBrush(Color.FromArgb(202, 202, 202)), New Rectangle(0, 0, 16, 16))
            G.FillRectangle(New SolidBrush(Color.White), New Rectangle(1, 1, 16 - 2, 16 - 2))
        End If

        If _Checked Then G.DrawString("a", New Font("Marlett", 16), New SolidBrush(Color.FromArgb(66, 157, 209)), New Point(-5, -3))

        G.DrawString(Text, Font, myBrush, New Point(20, 0))
    End Sub
End Class
#End Region
#Region " Toggle ON/OFF "

<DefaultEvent("CheckedChanged")> _
Class HazelDev_Toggle
    Inherits Control

#Region " Variables "

    Private cWidth, cHeight As Integer
    Private _Toggled As Boolean = False

#End Region
#Region " Properties "

    Public Property Toggled As Boolean
        Get
            Return _Toggled
        End Get
        Set(value As Boolean)
            _Toggled = value
            Invalidate()
        End Set
    End Property

#End Region
#Region " EventArgs "

    Public Event CheckedChanged(ByVal sender As Object)
    Protected Overrides Sub OnClick(e As EventArgs)
        MyBase.OnClick(e)
        _Toggled = Not _Toggled
        RaiseEvent CheckedChanged(Me)
    End Sub
    Protected Overrides Sub OnTextChanged(e As EventArgs)
        MyBase.OnTextChanged(e) : Invalidate()
    End Sub
    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Width = 76
        Height = 27
    End Sub
    Protected Overrides Sub OnMouseEnter(ByVal e As System.EventArgs)
        MyBase.OnMouseEnter(e)
        Invalidate()
    End Sub
    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDown(e)
        Invalidate()
    End Sub
    Protected Overrides Sub OnMouseLeave(ByVal e As System.EventArgs)
        MyBase.OnMouseLeave(e)
        Invalidate()
    End Sub
    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseUp(e)
        Invalidate()
    End Sub

#End Region

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer Or _
                 ControlStyles.SupportsTransparentBackColor, True)
        DoubleBuffered = True
        Cursor = Cursors.Hand
        Font = New Font("Microsoft Sans Serif", 10)
        Size = New Size(27, 33)
    End Sub

    'This fucntion was not created by HazelDev.
    Public Function RoundRec(ByVal Rectangle As Rectangle, ByVal Curve As Integer) As GraphicsPath
        Dim P As GraphicsPath = New GraphicsPath()
        Dim ArcRectangleWidth As Integer = Curve * 1
        P.AddArc(New Rectangle(Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -180, 90)
        P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -90, 90)
        P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 0, 90)
        P.AddArc(New Rectangle(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 90, 90)
        P.AddLine(New Point(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y), New Point(Rectangle.X, Curve + Rectangle.Y))
        Return P
    End Function

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim B As Bitmap
        Dim Gfx As Graphics
        B = New Bitmap(Width, Height)
        Gfx = Graphics.FromImage(B)

        cWidth = Width
        cHeight = Height

        Dim GraphicsPath1, GraphicsPath2 As New GraphicsPath
        Dim RectToDraw As New Rectangle(0, 0, cWidth, cHeight), RectToToggle As New Rectangle(CInt(cWidth \ 2), 0, 39, cHeight)

        With Gfx
            .TextRenderingHint = 5
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .Clear(BackColor)

            GraphicsPath1 = RoundRec(RectToDraw, 1)
            GraphicsPath2 = RoundRec(RectToToggle, 1)
            .FillPath(New SolidBrush(Color.WhiteSmoke), GraphicsPath1)
            .FillPath(New SolidBrush(Color.FromArgb(66, 157, 209)), GraphicsPath2)
            .DrawString("OFF", Font, New SolidBrush(Color.WhiteSmoke), New Rectangle(19, 1, cWidth, cHeight), New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})

            If Toggled Then
                GraphicsPath1 = RoundRec(RectToDraw, 1)
                GraphicsPath2 = RoundRec(RectToToggle, 1)

                .FillPath(New SolidBrush(Color.FromArgb(66, 157, 209)), GraphicsPath1)
                .FillPath(New SolidBrush(Color.WhiteSmoke), GraphicsPath2)
                .DrawString("ON", Font, New SolidBrush(Color.WhiteSmoke), New Rectangle(6, 6, cWidth, cHeight), New StringFormat() With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Near})
            End If

        End With

        MyBase.OnPaint(e)
        Gfx.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub
End Class

#End Region
#Region " TextBox "

Public Class HazelDev_TextBox
    Inherits Control

#Region " Variables "

    Public WithEvents HazelDevTB As New TextBox
    Private _maxchars As Integer = 32767
    Private _ReadOnly As Boolean
    Private _Multiline As Boolean
    Private ALNType As HorizontalAlignment
    Private isPasswordMasked As Boolean = False

#End Region
#Region " Properties "

    Public Shadows Property TextAlignment() As HorizontalAlignment
        Get
            Return ALNType
        End Get
        Set(ByVal Val As HorizontalAlignment)
            ALNType = Val
            Invalidate()
        End Set
    End Property
    Public Shadows Property MaxLength() As Integer
        Get
            Return _maxchars
        End Get
        Set(ByVal Val As Integer)
            _maxchars = Val
            HazelDevTB.MaxLength = MaxLength
            Invalidate()
        End Set
    End Property
    Public Shadows Property UseSystemPasswordChar() As Boolean
        Get
            Return isPasswordMasked
        End Get
        Set(ByVal Val As Boolean)
            HazelDevTB.UseSystemPasswordChar = UseSystemPasswordChar
            isPasswordMasked = Val
            Invalidate()
        End Set
    End Property
    Property [ReadOnly]() As Boolean
        Get
            Return _ReadOnly
        End Get
        Set(ByVal value As Boolean)
            _ReadOnly = value
            If HazelDevTB IsNot Nothing Then
                HazelDevTB.ReadOnly = value
            End If
        End Set
    End Property
    Property Multiline() As Boolean
        Get
            Return _Multiline
        End Get
        Set(ByVal value As Boolean)
            _Multiline = value
            If HazelDevTB IsNot Nothing Then
                HazelDevTB.Multiline = value

                If value Then
                    HazelDevTB.Height = Height - 10
                Else
                    Height = HazelDevTB.Height + 10
                End If
            End If
        End Set
    End Property

#End Region
#Region " EventArgs "

    Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
        MyBase.OnTextChanged(e)
        Invalidate()
    End Sub
    Protected Overrides Sub OnBackColorChanged(ByVal e As System.EventArgs)
        MyBase.OnBackColorChanged(e)
        HazelDevTB.BackColor = BackColor
        Invalidate()
    End Sub
    Protected Overrides Sub OnForeColorChanged(ByVal e As System.EventArgs)
        MyBase.OnForeColorChanged(e)
        HazelDevTB.ForeColor = ForeColor
        Invalidate()
    End Sub
    Protected Overrides Sub OnFontChanged(ByVal e As System.EventArgs)
        MyBase.OnFontChanged(e)
        HazelDevTB.Font = Font
    End Sub
    Private Sub _OnKeyDown(ByVal Obj As Object, ByVal e As KeyEventArgs)
        If e.Control AndAlso e.KeyCode = Keys.A Then
            HazelDevTB.SelectAll()
            e.SuppressKeyPress = True
        End If
        If e.Control AndAlso e.KeyCode = Keys.C Then
            HazelDevTB.Copy()
            e.SuppressKeyPress = True
        End If
    End Sub
    Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
        MyBase.OnResize(e)
        If _Multiline Then
            HazelDevTB.Height = Height - 10
        Else
            Height = HazelDevTB.Height + 10
        End If
    End Sub
    Protected Overrides Sub OnGotFocus(ByVal e As System.EventArgs)
        MyBase.OnGotFocus(e)
        HazelDevTB.Focus()
    End Sub
    Sub _TextChanged() Handles HazelDevTB.TextChanged
        Text = HazelDevTB.Text
    End Sub
    Sub _BaseTextChanged() Handles MyBase.TextChanged
        HazelDevTB.Text = Text
    End Sub

#End Region

    Sub AddTextBox()
        With HazelDevTB
            .BackColor = BackColor
            .ForeColor = ForeColor
            .Size = New Size(Width - 10, 27)
            .Location = New Point(7, 5)
            .Text = String.Empty
            .BorderStyle = BorderStyle.None
            .TextAlign = HorizontalAlignment.Left
            .Font = New Font("Tahoma", 10)
            .UseSystemPasswordChar = UseSystemPasswordChar
            .Multiline = False
        End With
        AddHandler HazelDevTB.KeyDown, AddressOf _OnKeyDown
    End Sub

    Sub New()
        MyBase.New()

        AddTextBox()
        Controls.Add(HazelDevTB)

        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        SetStyle(ControlStyles.UserPaint, True)

        BackColor = Color.White
        ForeColor = Color.FromArgb(54, 54, 54)
        Text = Nothing
        Font = New Font("Tahoma", 10)
        Size = New Size(135, 27)
        DoubleBuffered = True ' Cut off border flicker
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)
        G.SmoothingMode = SmoothingMode.HighQuality
        'Height = 27

        With HazelDevTB
            .Width = Width - 10
            .TextAlign = TextAlignment
            .UseSystemPasswordChar = UseSystemPasswordChar
            '.Height = 27
        End With

        G.Clear(Color.Transparent)
        G.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(202, 202, 202))), New Rectangle(0, 0, Width - 1, Height - 1))
        e.Graphics.DrawImage(B.Clone(), 0, 0)
        G.Dispose() : B.Dispose()
    End Sub
End Class

#End Region
#Region " RichTextBox "

Public Class HazelDev_RichTextBox
    Inherits Control

#Region " Variables "

    Public WithEvents HazelDevRTB As New RichTextBox
    Private _ReadOnly As Boolean
    Private _WordWrap As Boolean
    Private _AutoWordSelection As Boolean

#End Region
#Region " Properties "

    Overrides Property Text As String
        Get
            Return HazelDevRTB.Text
        End Get
        Set(value As String)
            HazelDevRTB.Text = value
            Invalidate()
        End Set
    End Property
    Property [ReadOnly]() As Boolean
        Get
            Return _ReadOnly
        End Get
        Set(ByVal value As Boolean)
            _ReadOnly = value
            If HazelDevRTB IsNot Nothing Then
                HazelDevRTB.ReadOnly = value
            End If
        End Set
    End Property
    Property [WordWrap]() As Boolean
        Get
            Return _WordWrap
        End Get
        Set(ByVal value As Boolean)
            _WordWrap = value
            If HazelDevRTB IsNot Nothing Then
                HazelDevRTB.WordWrap = value
            End If
        End Set
    End Property
    Property [AutoWordSelection]() As Boolean
        Get
            Return _AutoWordSelection
        End Get
        Set(ByVal value As Boolean)
            _AutoWordSelection = value
            If HazelDevRTB IsNot Nothing Then
                HazelDevRTB.AutoWordSelection = value
            End If
        End Set
    End Property
#End Region
#Region " EventArgs "

    Protected Overrides Sub OnBackColorChanged(ByVal e As System.EventArgs)
        MyBase.OnBackColorChanged(e)
        HazelDevRTB.BackColor = BackColor
        Invalidate()
    End Sub
    Protected Overrides Sub OnForeColorChanged(ByVal e As System.EventArgs)
        MyBase.OnForeColorChanged(e)
        HazelDevRTB.ForeColor = ForeColor
        Invalidate()
    End Sub
    Protected Overrides Sub OnFontChanged(ByVal e As System.EventArgs)
        MyBase.OnFontChanged(e)
        HazelDevRTB.Font = Font
    End Sub
    Protected Overrides Sub OnSizeChanged(ByVal e As System.EventArgs)
        MyBase.OnSizeChanged(e)
        HazelDevRTB.Size = New Size(Width - 10, Height - 11)
    End Sub
    Sub _TextChanged() Handles MyBase.TextChanged
        HazelDevRTB.Text = Text
    End Sub

#End Region

    Sub AddRichTextBox()
        With HazelDevRTB
            .BackColor = BackColor
            .ForeColor = ForeColor
            .Size = New Size(Width - 10, 100)
            .Location = New Point(7, 5)
            .Text = String.Empty
            .BorderStyle = BorderStyle.None
            .Font = New Font("Tahoma", 10)
            .Multiline = True
        End With
    End Sub

    Sub New()
        MyBase.New()

        AddRichTextBox()
        Controls.Add(HazelDevRTB)

        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        SetStyle(ControlStyles.UserPaint, True)

        BackColor = Color.White
        ForeColor = Color.FromArgb(54, 54, 54)
        Text = Nothing
        Font = New Font("Tahoma", 10)
        Size = New Size(150, 100)
        WordWrap = True
        AutoWordSelection = False
        DoubleBuffered = True ' Cut off border flicker
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim B As New Bitmap(Width, Height)
        Dim G = Graphics.FromImage(B)
        Dim Base As New Rectangle(0, 0, Width - 1, Height - 1)

        G.TextRenderingHint = Drawing.Text.TextRenderingHint.ClearTypeGridFit
        G.SmoothingMode = SmoothingMode.HighQuality
        G.PixelOffsetMode = PixelOffsetMode.HighQuality
        G.Clear(Color.White)
        G.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(150, 150, 150))), ClientRectangle)

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub
End Class

#End Region
#Region " ComboBox "

Public Class HazelDevComboBox
    Inherits ComboBox

#Region " Properties "

    Private _StartIndex As Integer = 0
    Private Property StartIndex As Integer
        Get
            Return _StartIndex
        End Get
        Set(ByVal value As Integer)
            _StartIndex = value
            Try
                MyBase.SelectedIndex = value
            Catch
            End Try
            Invalidate()
        End Set
    End Property

#End Region
#Region " EventArgs "

    Protected Overrides Sub OnTextChanged(e As System.EventArgs)
        MyBase.OnTextChanged(e)
        Invalidate()
    End Sub
    Protected Overrides Sub OnSelectedItemChanged(ByVal e As System.EventArgs)
        MyBase.OnSelectedItemChanged(e)
        Invalidate()
    End Sub
    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        MyBase.Invalidate()
        MyBase.OnMouseClick(e)
    End Sub
    Protected Overrides Sub OnMouseUp(e As System.Windows.Forms.MouseEventArgs)
        MyBase.Invalidate()
        MyBase.OnMouseUp(e)
    End Sub

#End Region

    Sub New()
        MyBase.New() ' initializer
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.ResizeRedraw Or ControlStyles.UserPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.SupportsTransparentBackColor, True)
        DrawMode = Windows.Forms.DrawMode.OwnerDrawFixed
        ForeColor = Color.Black
        BackColor = Color.White
        Size = New Size(135, 27)
        DropDownStyle = ComboBoxStyle.DropDownList
        Font = New Font("Tahoma", 10)
        DoubleBuffered = True
        StartIndex = 0
        ItemHeight = 21

    End Sub

    Sub ReplaceItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles Me.DrawItem
        e.DrawBackground()
        Try
            If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(66, 157, 209)), e.Bounds)
                e.Graphics.DrawString(MyBase.GetItemText(MyBase.Items(e.Index)), e.Font, Brushes.White, e.Bounds)
            Else

                e.Graphics.FillRectangle(New SolidBrush(Color.White), e.Bounds)
                e.Graphics.DrawString(MyBase.GetItemText(MyBase.Items(e.Index)), e.Font, Brushes.Black, e.Bounds)
            End If
        Catch
        End Try
        e.DrawFocusRectangle()
        Me.Invalidate() 'Writes the selected item to the box without actually selecting it
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)

        G.SmoothingMode = SmoothingMode.HighQuality

        G.Clear(Color.White)
        G.DrawLine(New Pen(Color.FromArgb(160, 160, 160), 2), New Point(Width - 18, 10), New Point(Width - 14, 14))
        G.DrawLine(New Pen(Color.FromArgb(160, 160, 160), 2), New Point(Width - 14, 14), New Point(Width - 10, 10))
        G.DrawLine(New Pen(Color.FromArgb(160, 160, 160)), New Point(Width - 14, 15), New Point(Width - 14, 14))

        G.DrawRectangle(New Pen(Color.FromArgb(202, 202, 202)), New Rectangle(0, 0, Width - 1, Height - 1))
        G.DrawString(Text, Font, Brushes.Black, New Rectangle(7, 0, Width - 1, Height - 1), New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Near})

        e.Graphics.DrawImage(B.Clone(), 0, 0)
        G.Dispose()
        B.Dispose()
    End Sub
End Class

#End Region
#Region " Separator "

Public Class HazelDev_Separator
    Inherits Control

#Region " Variables "

    Private ALN As Alignment
    Private _Color As Color = Color.FromArgb(66, 157, 209)

#End Region
#Region " Properties "

    Public Property Align() As Alignment
        Get
            Return ALN
        End Get
        Set(ByVal value As Alignment)
            ALN = value
            Invalidate()
        End Set
    End Property
    Public Property Color As Color
        Get
            Return _Color
        End Get
        Set(value As Color)
            _Color = value
            Invalidate()
        End Set
    End Property

#End Region
#Region " Enums "

    Public Enum Alignment
        Vertical
        Horizontal
    End Enum

#End Region

    Public Sub New()
        BackColor = Color.White
        Height = 5
        Width = 75
        Align = Alignment.Horizontal
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)
        Dim myBrush As New SolidBrush(_Color)
        Dim myPen As New Pen(myBrush)

        Select Case Align
            Case Alignment.Vertical
                G.DrawLine(myPen, New Point(Width \ 2, 0), New Point(Width \ 2, Height))
            Case Alignment.Horizontal
                G.DrawLine(myPen, New Point(0, Height \ 2), New Point(Width, Height \ 2))
        End Select

        e.Graphics.DrawImage(B, 0, 0)
        G.Dispose() : B.Dispose()
    End Sub
End Class

#End Region
#Region " NumericUpDown "

Class HazelDev_NumericUpDown
    Inherits Control

#Region " Variables "

    Friend G As Graphics, B As Bitmap
    Private cWidth, cHeight As Integer
    Private State As MouseState = MouseState.None
    Private Xval, Yval As Integer
    Private _Value, _Minimum, _Maximum As Long
    Private Bool As Boolean

#End Region
#Region " Properties "

    Public Property Value As Long
        Get
            Return _Value
        End Get
        Set(value As Long)
            If value <= _Maximum And value >= _Minimum Then _Value = value
            Invalidate()
        End Set
    End Property

    Public Property Minimum As Long
        Get
            Return _Minimum
        End Get
        Set(value As Long)
            If value < _Maximum Then _Minimum = value
            If _Value < _Minimum Then _Value = Minimum
            Invalidate()
        End Set
    End Property

    Public Property Maximum As Long
        Get
            Return _Maximum
        End Get
        Set(value As Long)
            If value > _Minimum Then _Maximum = value
            If _Value > _Maximum Then _Value = _Maximum
            Invalidate()
        End Set
    End Property

#End Region
#Region " EventArgs "

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        Xval = e.Location.X
        Yval = e.Location.Y
        Invalidate()
        If e.X < Width - 23 Then Cursor = Cursors.IBeam Else Cursor = Cursors.Hand
    End Sub
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        If Xval > Width - 21 AndAlso Xval < Width - 3 Then
            If Yval < 15 Then
                If (Value + 1) <= _Maximum Then _Value += 1
            Else
                If (Value - 1) >= _Minimum Then _Value -= 1
            End If
        Else
            Bool = Not Bool
            Focus()
        End If
        Invalidate()
    End Sub
    Protected Overrides Sub OnKeyPress(e As KeyPressEventArgs)
        MyBase.OnKeyPress(e)
        Try
            If Bool Then _Value = CStr(CStr(_Value) & e.KeyChar.ToString())
            If _Value > _Maximum Then _Value = _Maximum
            Invalidate()
        Catch : End Try
    End Sub
    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        MyBase.OnKeyDown(e)
        If e.KeyCode = Keys.Back Then
            Value = 0
        End If
    End Sub
    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Height = 27
    End Sub

#End Region
#Region " Enums "

    Enum MouseState As Byte
        None = 0
        Over = 1
        Down = 2
        Block = 3
    End Enum

#End Region

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
        ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer Or _
        ControlStyles.SupportsTransparentBackColor, True)
        DoubleBuffered = True
        Font = New Font("Segoe UI", 10)
        BackColor = Color.White
        ForeColor = Color.Black
        _Minimum = 0
        _Maximum = 100
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        B = New Bitmap(Width, Height) : G = Graphics.FromImage(B)
        cWidth = Width : cHeight = Height

        Dim CurrentRect As New Rectangle(0, 0, cWidth, cHeight)

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            .FillRectangle(New SolidBrush(Color.White), CurrentRect)

            REM  For arrows
            REM .DrawString("5", New Font("Marlett", 9, FontStyle.Bold), Brushes.White, New Point(Width - 11, 10), New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
            REM .DrawString("6", New Font("Marlett", 9, FontStyle.Bold), Brushes.White, New Point(Width - 11, 19), New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})

            .DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(202, 202, 202))), 0, 0, cWidth - 1, cHeight)
            .FillRectangle(New SolidBrush(Color.FromArgb(66, 157, 209)), New Rectangle(Width - 24, 0, 24, cHeight))

            .DrawString("+", New Font("Tahoma", 9, FontStyle.Bold), Brushes.White, New Point(Width - 11, 10), New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
            .DrawString("-", New Font("Tahoma", 9, FontStyle.Bold), Brushes.White, New Point(Width - 11, 20), New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})

            .DrawString(Value, Font, Brushes.Black, New Rectangle(5, 1, cWidth, cHeight), New StringFormat() With {.LineAlignment = StringAlignment.Center})

        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()
    End Sub

End Class

#End Region
#Region " Status "

Class HazelDev_Status
    Inherits Theme

#Region " Variables "

    Dim myBrush As New SolidBrush(Color.FromArgb(130, 130, 130))
    Dim myPen As New Pen(myBrush)
    Private cWidth, cHeight As Integer
    Private TxtHeight As Integer = 5

#End Region
#Region " Custom Properties "

    Private _DrawUpperLine As Boolean
    Property DrawUpperLine() As Boolean
        Get
            Return _DrawUpperLine
        End Get
        Set(ByVal v As Boolean)
            _DrawUpperLine = v
            Invalidate()
        End Set
    End Property

#End Region
    Public Sub New()
        Dock = DockStyle.Bottom
        Height = 26
        Width = 372
        Font = New Font("Microsoft Sans Serif", 9, FontStyle.Regular)
    End Sub

    Overrides Sub PaintHook()
        cWidth = Width : cHeight = Height
        G.Clear(Color.White)

        If _DrawUpperLine = True Then
            G.DrawLine(myPen, New Point(0, 1), New Point(Width, 1))
            TxtHeight = 4
        Else
            TxtHeight = 5
        End If

        DrawGradient(Color.FromArgb(66, 157, 209), Color.FromArgb(35, 131, 207), 0, 2, Width, Height, 90S)
        G.DrawString(Text, Font, Brushes.White, New Rectangle(10, TxtHeight, cWidth, cHeight), New StringFormat() With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Near})
    End Sub
End Class

#End Region