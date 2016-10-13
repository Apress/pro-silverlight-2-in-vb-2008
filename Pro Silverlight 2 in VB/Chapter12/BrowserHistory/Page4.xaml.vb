Partial Public Class Page4
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        App.Navigate(New Page1())
    End Sub
End Class
