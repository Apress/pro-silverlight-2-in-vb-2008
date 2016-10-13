Partial Public Class SimpleButtonTemplate
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        CType(sender, Button).Content = "You clicked me."
    End Sub
End Class
