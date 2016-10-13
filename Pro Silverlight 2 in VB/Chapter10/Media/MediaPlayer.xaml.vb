Imports System.Windows.Threading

Partial Public Class MediaPlayer
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
        timer.Interval = TimeSpan.FromSeconds(0.1)
        AddHandler timer.Tick, AddressOf timer_Tick
    End Sub

    Private Sub timer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        lblStatus.Text = media.Position.ToString().TrimEnd(New Char() {"0"c})
        sliderPositionBackground.Value = media.Position.TotalSeconds
    End Sub

    Private timer As New DispatcherTimer()
    Private Sub cmdPlay_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        media.Stop()
        media.Play()
        timer.Start()
    End Sub
    Private Sub cmdPause_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        media.Pause()
        timer.Stop()
    End Sub
    Private Sub cmdStop_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        media.Stop()
        timer.Stop()
    End Sub
    Private Sub media_MediaOpened(ByVal sender As Object, ByVal e As RoutedEventArgs)
        sliderPosition.Maximum = media.NaturalDuration.TimeSpan.TotalSeconds
    End Sub
    Private Sub sliderPosition_ValueChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
        media.Pause()
        media.Position = TimeSpan.FromSeconds(sliderPosition.Value)
        media.Play()
    End Sub

    Private Sub sliderBalance_ValueChanged(ByVal sender As Object, ByVal e As RoutedPropertyChangedEventArgs(Of Double))
        media.Balance = sliderBalance.Value
    End Sub

    Private Sub sliderVolume_ValueChanged(ByVal sender As Object, ByVal e As RoutedPropertyChangedEventArgs(Of Double))
        If Not media Is Nothing Then
            media.Volume = sliderVolume.Value
        End If
    End Sub

    Private Sub chkMute_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        media.IsMuted = CBool(chkMute.IsChecked)
    End Sub

    Private Sub media_MediaFailed(ByVal sender As Object, ByVal e As ExceptionRoutedEventArgs)
        lblStatus.Text = e.ErrorException.Message
    End Sub

    Private Sub media_MediaEnded(ByVal sender As Object, ByVal e As RoutedEventArgs)
        If CBool(chkLoop.IsChecked) Then
            media.Position = TimeSpan.Zero
            media.Play()
        Else
            timer.Stop()
        End If
    End Sub

    Private Sub media_CurrentStateChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
        lblStatus.Text = media.CurrentState.ToString()
    End Sub

End Class
