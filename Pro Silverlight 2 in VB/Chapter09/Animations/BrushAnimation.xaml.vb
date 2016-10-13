Partial Public Class BrushAnimation
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub UserControl_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
        ellipseStoryboard.Begin()
    End Sub
End Class
