Partial Public Class FullScreen
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Application.Current.Host.Content.IsFullScreen = True
    End Sub

    Private Sub LayoutRoot_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        lbl.Text = "Pressed " & e.Key & " at " & DateTime.Now.ToLongTimeString()
    End Sub
End Class
