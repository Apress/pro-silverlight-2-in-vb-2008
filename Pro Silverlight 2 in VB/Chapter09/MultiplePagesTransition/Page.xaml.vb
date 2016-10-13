Partial Public Class Page
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim transition As New WipeTransition()
        transition.Navigate(New Page2())
    End Sub
End Class
