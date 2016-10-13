Partial Public Class MultipleSounds
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub cmdPlay_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim media As New MediaElement()
        media.Source = New Uri("test.mp3", UriKind.Relative)
        AddHandler media.MediaEnded, AddressOf media_MediaEnded
        MediaContainer.Children.Add(media)
        lblStatus.Text = MediaContainer.Children.Count & " media files playing."
    End Sub

    Private Sub media_MediaEnded(ByVal sender As Object, ByVal e As RoutedEventArgs)
        MediaContainer.Children.Remove(CType(sender, MediaElement))
        lblStatus.Text = MediaContainer.Children.Count & " media files playing."
    End Sub
End Class
