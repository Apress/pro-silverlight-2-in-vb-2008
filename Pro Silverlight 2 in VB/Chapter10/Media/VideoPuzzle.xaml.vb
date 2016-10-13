Partial Public Class VideoPuzzle
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)

    End Sub

    Private previousRectangle As Rectangle

    ' Move the clicked square.
    Private Sub rect_MouseLeftButtonDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        ' Get the square.
        Dim rectangle As Rectangle = CType(sender, Rectangle)

        ' You need to stop the storyboard because the same storyboard is being reused for each piece.
        ' The alternative is to dynamically create a new storyboard after each click.
        If previousRectangle IsNot Nothing Then MoveToNewPosition(previousRectangle, squareMoveStoryboard)
        Storyboard.SetTarget(squareMoveStoryboard, rectangle)

        ' Choose a random direction and movement amount.
        Dim rand As New Random()
        Dim sign As Integer = 1
        If rand.Next(0, 2) = 0 Then
            sign = -1
        End If
        leftAnimation.To = Canvas.GetLeft(rectangle) + rand.Next(60, 150) * sign
        topAnimation.To = Canvas.GetTop(rectangle) + rand.Next(60, 150) * sign

        previousRectangle = rectangle

        ' Start the animation.
        squareMoveStoryboard.Begin()
    End Sub

    ' Make the video loop.
    Private Sub videoClip_MediaEnded(ByVal sender As Object, ByVal e As RoutedEventArgs)
        videoClip.Stop()
        videoClip.Play()
    End Sub

    Private Sub cmdGeneratePuzzle_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        ' Get the requested dimensions.
        Dim rows As Integer
        Dim cols As Integer
        Int32.TryParse(txtRows.Text, rows)
        Int32.TryParse(txtCols.Text, cols)

        If (rows < 1) OrElse (cols < 1) Then
            Return
        End If

        ' Clear the surface.
        puzzleSurface.Children.Clear()

        ' Determine the rectangle size.
        Dim squareWidth As Double = puzzleSurface.ActualWidth / cols
        Dim squareHeight As Double = puzzleSurface.ActualHeight / rows

        ' Create the brush for the MediaElement named videoClip.
        Dim brush As New VideoBrush()
        brush.SetSource(videoClip)

        ' Create the rectangles.
        Dim top As Double = 0
        Dim left As Double = 0
        For row As Integer = 0 To rows - 1
            For col As Integer = 0 To cols - 1
                ' Create the rectangle. Every rectangle is sized to match the Canvas.
                Dim rect As New Rectangle()
                rect.Width = puzzleSurface.ActualWidth
                rect.Height = puzzleSurface.ActualHeight

                rect.Fill = brush
                Dim rectBrush As New SolidColorBrush(Colors.DarkGray)
                rect.StrokeThickness = 3
                rect.Stroke = rectBrush

                ' Clip the rectangle to fit its portion of the puzzle.
                Dim r As New RectangleGeometry()
                ' 1-pixel correction factor ensures we never get lines in between.
                r.Rect = New Rect(left, top, squareWidth + 1, squareHeight + 1)
                rect.Clip = r

                ' Handle rectangle clicks.
                AddHandler rect.MouseLeftButtonDown, AddressOf rect_MouseLeftButtonDown

                puzzleSurface.Children.Add(rect)

                ' Go to the next column.
                left += squareWidth
            Next
            ' Go to the next row.
            left = 0
            top += squareHeight
        Next

        ' (Video is on autostart, and is already playing. If not, play here.)
        'videoClip.Play();
    End Sub

    Private Sub MoveToNewPosition(ByVal rectangle As UIElement, ByVal squareMoveStoryboard As Storyboard)
        Dim left As Double = Canvas.GetLeft(rectangle)
        Dim top As Double = Canvas.GetTop(rectangle)
        squareMoveStoryboard.Stop()
        Canvas.SetLeft(rectangle, left)
        Canvas.SetTop(rectangle, top)
    End Sub

End Class
