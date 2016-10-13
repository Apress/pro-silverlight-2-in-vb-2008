Partial Public Class StatusPage
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
        lbl.Text = "This application started at " & DateTime.Now.ToLongTimeString()
    End Sub
End Class
