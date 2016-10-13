Partial Public Class VideoWithMarkers
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub media_MarkerReached(ByVal sender As Object, ByVal e As TimelineMarkerRoutedEventArgs)
        lblMarker.Text = e.Marker.Text & " at " & e.Marker.Time.TotalSeconds & " seconds"
    End Sub

    Private Sub media_MediaOpened(ByVal sender As Object, ByVal e As RoutedEventArgs)
        ' Use data binding.
        ' The MarkerInfo wrapper is needed because you can't bind TimelineMarker objects directly.
        lstMarkers.DisplayMemberPath = "DisplayText"
        For Each marker As TimelineMarker In media.Markers
            lstMarkers.Items.Add(New MarkerInfo(marker))
        Next
    End Sub

    Private Sub lstMarkers_SelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
        Dim marker As MarkerInfo = CType(lstMarkers.SelectedItem, MarkerInfo)
        media.Position = marker.Marker.Time
        media.Play()
    End Sub
End Class

Public Class MarkerInfo
    Private _marker As TimelineMarker
    Public Property Marker() As TimelineMarker
        Get
            Return _marker
        End Get
        Set(ByVal value As TimelineMarker)
            _marker = value
        End Set
    End Property

    Public ReadOnly Property DisplayText() As String
        Get
            Return Marker.Text & " (" & Marker.Time.Minutes & ":" & Marker.Time.Seconds & ":" & Marker.Time.Milliseconds & ")"
        End Get
    End Property

    Public Sub New(ByVal marker As TimelineMarker)
        Me.Marker = marker
    End Sub
End Class
