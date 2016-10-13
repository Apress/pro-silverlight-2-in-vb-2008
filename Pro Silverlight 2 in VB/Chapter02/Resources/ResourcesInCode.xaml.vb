Partial Public Class ResourcesInCode
    Inherits UserControl

    Public Sub New 
        InitializeComponent()
    End Sub

    Private Sub cmdChange_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim brush As LinearGradientBrush = CType(Me.Resources("ButtonFace"), LinearGradientBrush)

        ' Swap the color order.
        Dim color As Color = brush.GradientStops(0).Color
        brush.GradientStops(0).Color = brush.GradientStops(2).Color
        brush.GradientStops(2).Color = color
    End Sub

    Private Sub cmdReplace_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim brush As New SolidColorBrush(Colors.Yellow)
        Me.Resources("ButtonFace") = brush
    End Sub

End Class
