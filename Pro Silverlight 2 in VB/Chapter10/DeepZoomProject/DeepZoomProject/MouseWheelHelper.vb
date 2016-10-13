Imports System.Windows.Browser

' Courtesy of Pete Blois
Public Class MouseWheelEventArgs
    Inherits EventArgs
    Private _delta As Double
    Private _handled As Boolean = False

    Public Sub New(ByVal delta As Double)
        Me._delta = delta
    End Sub

    Public ReadOnly Property Delta() As Double
        Get
            Return Me._delta
        End Get
    End Property

    ' Use handled to prevent the default browser behavior!
    Public Property Handled() As Boolean
        Get
            Return Me._handled
        End Get
        Set(ByVal value As Boolean)
            Me._handled = value
        End Set
    End Property
End Class

Public Class MouseWheelHelper

    Public Event Moved As EventHandler(Of MouseWheelEventArgs)
    Private Shared _worker As Worker
    Private isMouseOver As Boolean = False

    Public Sub New(ByVal element As FrameworkElement)

        If _worker Is Nothing Then
            _worker = New Worker()
        End If

        AddHandler _worker.Moved, AddressOf HandleMouseWheel

        AddHandler element.MouseEnter, AddressOf HandleMouseEnter
        AddHandler element.MouseLeave, AddressOf HandleMouseLeave
        AddHandler element.MouseMove, AddressOf HandleMouseMove
    End Sub

    Private Sub HandleMouseWheel(ByVal sender As Object, ByVal args As MouseWheelEventArgs)
        If Me.isMouseOver Then
            RaiseEvent Moved(Me, args)
        End If
    End Sub

    Private Sub HandleMouseEnter(ByVal sender As Object, ByVal e As EventArgs)
        Me.isMouseOver = True
    End Sub

    Private Sub HandleMouseLeave(ByVal sender As Object, ByVal e As EventArgs)
        Me.isMouseOver = False
    End Sub

    Private Sub HandleMouseMove(ByVal sender As Object, ByVal e As EventArgs)
        Me.isMouseOver = True
    End Sub


    Private Class Worker

        Public Event Moved As EventHandler(Of MouseWheelEventArgs)

        Public Sub New()

            If HtmlPage.IsEnabled Then
                HtmlPage.Window.AttachEvent("DOMMouseScroll", AddressOf Me.HandleMouseWheel)
                HtmlPage.Window.AttachEvent("onmousewheel", AddressOf Me.HandleMouseWheel)
                HtmlPage.Document.AttachEvent("onmousewheel", AddressOf Me.HandleMouseWheel)
            End If

        End Sub

        Private Sub HandleMouseWheel(ByVal sender As Object, ByVal args As HtmlEventArgs)
            Dim delta As Double = 0

            Dim eventObj As ScriptObject = args.EventObject

            If Not eventObj.GetProperty("wheelDelta") Is Nothing Then
                delta = (CDbl(eventObj.GetProperty("wheelDelta"))) / 120


                If Not HtmlPage.Window.GetProperty("opera") Is Nothing Then
                    delta = -delta
                End If
            ElseIf Not eventObj.GetProperty("detail") Is Nothing Then
                delta = -(CDbl(eventObj.GetProperty("detail"))) / 3

                If HtmlPage.BrowserInformation.UserAgent.IndexOf("Macintosh") <> -1 Then
                    delta = delta * 3
                End If
            End If

            If delta <> 0 AndAlso Not Me.MovedEvent Is Nothing Then
                Dim wheelArgs As New MouseWheelEventArgs(delta)
                RaiseEvent Moved(Me, wheelArgs)

                If wheelArgs.Handled Then
                    args.PreventDefault()
                End If
            End If
        End Sub
    End Class
End Class
