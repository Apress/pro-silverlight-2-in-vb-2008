Partial Public Class ImageWipe
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        wipeStoryboard.Begin()
    End Sub
End Class
