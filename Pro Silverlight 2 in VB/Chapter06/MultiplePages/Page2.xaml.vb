Partial Public Class Page2
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        App.Navigate(Pages.Page)
    End Sub
End Class
