Imports System.Threading

Public Partial Class UsingTheDispatcher
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub cmdBreakRules_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim thread As New Thread(AddressOf UpdateTextWrong)
        thread.Start()
    End Sub

    Private Sub UpdateTextWrong()
        ' Simulate some work taking place with a five-second delay.
        Thread.Sleep(TimeSpan.FromSeconds(5))

        txt.Text = "Here is some new text."
    End Sub

    Private Sub cmdFollowRules_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim thread As New Thread(AddressOf UpdateTextRight)
        thread.Start()
    End Sub

    Private Sub UpdateTextRight()
        ' Simulate some work taking place with a five-second delay.
        Thread.Sleep(TimeSpan.FromSeconds(5))

        ' Get the dispatcher from the current page, and use it to invoke
        ' the update code.
        Me.Dispatcher.BeginInvoke(AddressOf SetText)
    End Sub

    Private Sub SetText()
        txt.Text = "Here is some new text."
    End Sub


End Class
