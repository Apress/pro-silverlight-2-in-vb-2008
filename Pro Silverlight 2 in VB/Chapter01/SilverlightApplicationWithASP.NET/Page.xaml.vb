Partial Public Class Page
    Inherits UserControl

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub cmdClickMe_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        lblMessage.Text = "Goodbye, cruel world."
    End Sub
End Class