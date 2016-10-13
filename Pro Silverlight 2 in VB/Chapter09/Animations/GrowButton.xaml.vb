Partial Public Class GrowButton
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        storyboard.Begin()
    End Sub

    Private Sub UserControl_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)

    End Sub
End Class
