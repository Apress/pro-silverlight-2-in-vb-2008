Partial Public Class Page
    Inherits UserControl

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub UserControl_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        lblViewMode.Text = "Current view mode: " & CType(Application.Current, App).ViewMode.ToString()
    End Sub
End Class