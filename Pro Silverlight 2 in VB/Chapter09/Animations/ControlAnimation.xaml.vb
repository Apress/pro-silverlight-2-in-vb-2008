Partial Public Class ControlAnimation
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub cmdStart_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        fadeStoryboard.Begin()
    End Sub

    Private Sub cmdPause_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        fadeStoryboard.Pause()
    End Sub

    Private Sub cmdResume_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        fadeStoryboard.Resume()
    End Sub

    Private Sub cmdStop_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        fadeStoryboard.Stop()
    End Sub

    Private Sub cmdMiddle_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        fadeStoryboard.Begin()
        fadeStoryboard.Seek(TimeSpan.FromSeconds(fadeAnimation.Duration.TimeSpan.TotalSeconds / 2))
    End Sub

    Private Sub sldSpeed_ValueChanged(ByVal sender As Object, ByVal e As RoutedPropertyChangedEventArgs(Of Double))
        ' Avoid problems when page is first parsed.
        If sldSpeed Is Nothing Then
            Return
        End If

        ' This also restarts the animation if it's currently underway.
        fadeStoryboard.SpeedRatio = sldSpeed.Value
        lblSpeed.Text = sldSpeed.Value.ToString("0.0")
    End Sub
End Class
