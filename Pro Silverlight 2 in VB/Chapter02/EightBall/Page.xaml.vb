Partial Public Class Page
    Inherits UserControl

    Public Sub New 
        InitializeComponent()
    End Sub

    Private Sub cmdAnswer_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim generator As New AnswerGenerator()
        txtAnswer.Text = generator.GetRandomAnswer(txtQuestion.Text)
        
        Dim brush As New LinearGradientBrush()

        Dim gradientStop1 As New GradientStop()
        gradientStop1.Offset = 0
        gradientStop1.Color = Colors.Yellow
        brush.GradientStops.Add(gradientStop1)

        Dim gradientStop2 As New GradientStop()
        gradientStop2.Offset = 0.5
        gradientStop2.Color = Colors.White
        brush.GradientStops.Add(gradientStop2)

        Dim gradientStop3 As New GradientStop()
        gradientStop3.Offset = 1
        gradientStop3.Color = Colors.Purple
        brush.GradientStops.Add(gradientStop3)

        grid1.Background = brush
    End Sub
End Class
