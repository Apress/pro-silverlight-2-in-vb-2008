Partial Public Class Scaling
    Inherits UserControl
    Public Sub New()
        InitializeComponent()
        AddHandler SizeChanged, AddressOf Page_SizeChanged
    End Sub

    Private idealPageSize As New Size(200, 225)
    Private Sub Page_SizeChanged(ByVal sender As Object, ByVal e As SizeChangedEventArgs)
        ' Compare the current window to the ideal dimensions.
        Dim heightRatio As Double = Me.ActualHeight / idealPageSize.Height
        Dim widthRatio As Double = Me.ActualWidth / idealPageSize.Width

        ' Create the transform.
        Dim scale As New ScaleTransform()

        ' Determine the smallest dimension.
        ' This preserves the aspect ratio.
        If heightRatio < widthRatio Then
            scale.ScaleX = heightRatio
            scale.ScaleY = heightRatio
        Else
            scale.ScaleX = widthRatio
            scale.ScaleY = widthRatio
        End If

        ' Apply the transform.
        Me.RenderTransform = scale
    End Sub
End Class
