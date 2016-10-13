Imports System.Windows.Browser

Public Partial Class Page1
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        App.Navigate(New Page2())
    End Sub
End Class
