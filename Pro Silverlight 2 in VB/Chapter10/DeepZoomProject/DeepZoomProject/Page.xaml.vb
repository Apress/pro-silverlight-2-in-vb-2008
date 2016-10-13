Partial Public Class Page
    Inherits UserControl
    '
    ' Based on prior work done by Lutz Gerhard, Peter Blois, and Scott Hanselman
    '
    Private lastMousePos As New Point()

    Private _zoom As Double = 1
    Private mouseButtonPressed As Boolean = False
    Private mouseIsDragging As Boolean = False
    Private dragOffset As Point
    Private currentPosition As Point

    Public Property ZoomFactor() As Double
        Get
            Return _zoom
        End Get
        Set(ByVal value As Double)
            _zoom = value
        End Set
    End Property

    Public Sub New()
        InitializeComponent()

        '
        ' We are setting the source here, but you should be able to set the Source property via
        '
        Me.msi.Source = New DeepZoomImageTileSource(New Uri("GeneratedImages/dzc_output.xml", UriKind.Relative))

        '
        ' Firing an event when the MultiScaleImage is Loaded
        '
        AddHandler msi.Loaded, AddressOf msi_Loaded

        '
        ' Firing an event when all of the images have been Loaded
        '
        AddHandler msi.ImageOpenSucceeded, AddressOf msi_ImageOpenSucceeded

        '
        ' Handling all of the mouse and keyboard functionality
        '
        AddHandler Me.MouseMove, AddressOf AnonymousMethod1

        AddHandler Me.MouseLeftButtonDown, AddressOf AnonymousMethod2

        AddHandler Me.msi.MouseLeave, AddressOf AnonymousMethod3

        AddHandler Me.MouseLeftButtonUp, AddressOf AnonymousMethod4

        AddHandler Me.MouseMove, AddressOf AnonymousMethod5

        AddHandler CType(New MouseWheelHelper(Me), MouseWheelHelper).Moved, AddressOf AnonymousMethod6
    End Sub
    Private Sub AnonymousMethod1(ByVal sender As Object, ByVal e As MouseEventArgs)
        If mouseButtonPressed Then
            mouseIsDragging = True
        End If
        Me.lastMousePos = e.GetPosition(Me.msi)
    End Sub
    Private Sub AnonymousMethod2(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        mouseButtonPressed = True
        mouseIsDragging = False
        dragOffset = e.GetPosition(Me)
        currentPosition = msi.ViewportOrigin
    End Sub
    Private Sub AnonymousMethod3(ByVal sender As Object, ByVal e As MouseEventArgs)
        mouseIsDragging = False
    End Sub
    Private Sub AnonymousMethod4(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        mouseButtonPressed = False
        If mouseIsDragging = False Then
            Dim shiftDown As Boolean = (Keyboard.Modifiers And ModifierKeys.Shift) = ModifierKeys.Shift
            ZoomFactor = 2.0
            If shiftDown Then
                ZoomFactor = 0.5
            End If
            Zoom(ZoomFactor, Me.lastMousePos)
        End If
        mouseIsDragging = False
    End Sub
    Private Sub AnonymousMethod5(ByVal sender As Object, ByVal e As MouseEventArgs)
        If mouseIsDragging Then
            Dim newOrigin As New Point()
            newOrigin.X = currentPosition.X - (((e.GetPosition(msi).X - dragOffset.X) / msi.ActualWidth) * msi.ViewportWidth)
            newOrigin.Y = currentPosition.Y - (((e.GetPosition(msi).Y - dragOffset.Y) / msi.ActualHeight) * msi.ViewportWidth)
            msi.ViewportOrigin = newOrigin
        End If
    End Sub
    Private Sub AnonymousMethod6(ByVal sender As Object, ByVal e As MouseWheelEventArgs)
        e.Handled = True
        If e.Delta > 0 Then
            ZoomFactor = 1.2
        Else
            ZoomFactor = 0.8
        End If
        Zoom(ZoomFactor, Me.lastMousePos)
    End Sub

    Private Sub msi_ImageOpenSucceeded(ByVal sender As Object, ByVal e As RoutedEventArgs)
        'If collection, this gets you a list of all of the MultiScaleSubImages
        '
        'foreach (MultiScaleSubImage subImage in msi.SubImages)
        '{
        '    // Do something
        '}
    End Sub

    Private Sub msi_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
        ' Hook up any events you want when the image has successfully been opened
    End Sub

    Public Sub Zoom(ByVal zoom As Double, ByVal pointToZoom As Point)
        Dim logicalPoint As Point = Me.msi.ElementToLogicalPoint(pointToZoom)
        Me.msi.ZoomAboutLogicalPoint(zoom, logicalPoint.X, logicalPoint.Y)
    End Sub

    '        
    '         *  Sample event handlerrs tied to the Click of event of various buttons for 
    '         *  showing all images, zooming in, and zooming out!
    '         * 
    '        private void ShowAllClick(object sender, RoutedEventArgs e)
    '        {
    '            this.msi.ViewportOrigin = new Point(0, 0);
    '            this.msi.ViewportWidth = 1;
    '            ZoomFactor = 1;
    '        }
    '
    '        private void zoomInClick(object sender, RoutedEventArgs e)
    '        {
    '            Zoom(1.2, new Point(this.ActualWidth / 2, this.ActualHeight / 2));
    '        }
    '
    '        private void zoomOutClick(object sender, RoutedEventArgs e)
    '        {
    '            Zoom(.8, new Point(this.ActualWidth / 2, this.ActualHeight / 2));
    '        }
    '         * 
End Class