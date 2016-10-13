Partial Public Class SimultaneousAnimations
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        storyboard.Begin()
    End Sub
End Class
