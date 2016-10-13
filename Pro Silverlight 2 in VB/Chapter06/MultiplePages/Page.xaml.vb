Partial Public Class Page
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        App.Navigate(Pages.Page2)
    End Sub
End Class
