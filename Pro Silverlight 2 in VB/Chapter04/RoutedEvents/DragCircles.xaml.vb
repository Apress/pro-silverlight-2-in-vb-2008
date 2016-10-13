Partial Public Class DragCircles
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub canvas_Click(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        ' Create an ellipse (unless the user is in the process
        ' of dragging another one).
        If (Not isDragging) Then
            ' Give the ellipse a 50-pixel diameter and a red fill.
            Dim ellipse As New Ellipse()
            ellipse.Fill = New SolidColorBrush(Colors.Red)
            ellipse.Width = 50
            ellipse.Height = 50

            ' Use the current mouse position for the center of
            ' the ellipse.                
            Dim point As Point = e.GetPosition(Me)
            ellipse.SetValue(Canvas.TopProperty, point.Y - ellipse.Height / 2)
            ellipse.SetValue(Canvas.LeftProperty, point.X - ellipse.Width / 2)

            ' Watch for left-button clicks.
            AddHandler ellipse.MouseLeftButtonDown, AddressOf ellipse_MouseDown

            ' Add the ellipse to the Canvas.
            parentCanvas.Children.Add(ellipse)
        End If
    End Sub

    ' Keep track of when an ellipse is being dragged.
    Private isDragging As Boolean = False

    ' When an ellipse is clicked, record the exact position
    ' where the click is made.
    Private mouseOffset As Point

    Private Sub ellipse_MouseDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        ' Dragging mode begins.
        isDragging = True
        Dim ellipse As Ellipse = CType(sender, Ellipse)

        ' Get the position of the click relative to the ellipse
        ' (so the top-left corner of the ellipse is (0,0).
        mouseOffset = e.GetPosition(ellipse)

        ' Change the ellipse color.
        ellipse.Fill = New SolidColorBrush(Colors.Green)

        ' Watch this ellipse for more mouse events.
        AddHandler ellipse.MouseMove, AddressOf ellipse_MouseMove
        AddHandler ellipse.MouseLeftButtonUp, AddressOf ellipse_MouseUp

        ' Capture the mouse. This way you'll keep receiveing
        ' the MouseMove event even if the user jerks the mouse
        ' off the ellipse.
        ellipse.CaptureMouse()
    End Sub

    Private Sub ellipse_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
        If isDragging Then
            Dim ellipse As Ellipse = CType(sender, Ellipse)

            ' Get the position of the ellipse relative to the Canvas.
            Dim point As Point = e.GetPosition(Me)

            ' Move the ellipse.
            ellipse.SetValue(Canvas.TopProperty, point.Y - mouseOffset.Y)
            ellipse.SetValue(Canvas.LeftProperty, point.X - mouseOffset.X)
        End If
    End Sub

    Private Sub ellipse_MouseUp(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        If isDragging Then
            Dim ellipse As Ellipse = CType(sender, Ellipse)

            ' Change the ellipse color.
            ellipse.Fill = New SolidColorBrush(Colors.Orange)

            ' Don't watch the mouse events any longer.
            RemoveHandler ellipse.MouseMove, AddressOf ellipse_MouseMove
            RemoveHandler ellipse.MouseLeftButtonUp, AddressOf ellipse_MouseUp
            ellipse.ReleaseMouseCapture()

            isDragging = False
        End If
    End Sub




End Class
