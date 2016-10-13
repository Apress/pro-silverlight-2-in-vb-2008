Partial Public Class Page2
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim transition As New WipeTransition()
        transition.Navigate(New Page())
    End Sub
End Class
