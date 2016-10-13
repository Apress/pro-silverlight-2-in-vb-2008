Partial Public Class SimpleCanvas
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Canvas_MouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        CType((CType(sender, Canvas)).Background, SolidColorBrush).Color = Colors.Yellow
    End Sub
End Class
